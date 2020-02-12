using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;
using DataGridViewAutoFilter;
using System.IO;

namespace UI
{
    public partial class UpdateSUb : Form
    {
        #region 变量
        /// <summary>
        /// 储存Excel表格数据
        /// </summary>
        private DataTable Exceldt = null;
        /// <summary>
        /// 记录耗时
        /// </summary>
        private Stopwatch sw = new Stopwatch();
        /// <summary>
        /// 记录Excel的完整路径
        /// </summary>
        private string path;
        /// <summary>
        /// 储存示例数据的列标题
        /// </summary>
        List<string> Colsarr = new List<string>();
        /// <summary>
        /// 实例化方法组
        /// </summary>
        BLL.ExamineeManMethod EMFMethod = new BLL.ExamineeManMethod("tb_stumanage");
        /// <summary>
        /// 实例化mysql工具
        /// </summary>
        DAL.MySqlDBTool MySqlDB = new DAL.MySqlDBTool();
        /// <summary>
        /// dgv控件添加筛选功能
        /// </summary>
        DataGridViewFunction Get = new DataGridViewFunction();
        #endregion
        #region 构造函数
        public UpdateSUb()
        {
            InitializeComponent();
        }
        #endregion
        #region 返回
        private void Return_Click(object sender, EventArgs e)
        {
            this.Close();

        }
        #endregion
        #region 修改
        private void Complete_Click(object sender, EventArgs e)
        {
            // 判断通过的条件
            if (textBox1.Text.Trim() == string.Empty)
            {
                MessageBox.Show("套题名称不能为空");
                return;
            }

            #region 添加数据
            // 获取选中行的第一列的值
            int SelIndex = fajuanFrm.fF.dataSource.CurrentCell.RowIndex;

            // 实例MySql工具
            DAL.MySqlDBTool MySqltool = new DAL.MySqlDBTool();

            // 添加要修改的数据
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("ID", comboBox1.Text);
            dic.Add("Name", textBox1.Text);
            dic.Add("Type", comboBox2.Text);
            dic.Add("ofLesson", comboBox3.Text);

            // 添加条件
            Dictionary<string, string> Sourcedic = new Dictionary<string, string>();
            Sourcedic.Add("ID", fajuanFrm.fF.dataSource.Rows[SelIndex].Cells[0].Value.ToString());

            // 所属课程可为空
            if (comboBox3.Text == string.Empty)
                dic.Remove("ofLesson");

            if (MySqltool.ParMySqlIDUCode("tb_taoti", dic, Sourcedic) > 0)// 判断SQL语句是否输入正确
            {
                MySqltool.Display(fajuanFrm.fF.dataSource, "tb_taoti");// 刷新数据表
            }
            else
            {
                MessageBox.Show("数据修改失败，请重试。");
                return;
            }

            // 修改完毕退出
            MessageBox.Show("成功修改一条数据。");
            this.Close();
            #endregion
        }

        #endregion
        #region 读取课程
        private void comboBox3_DropDown(object sender, EventArgs e)
        {
            comboBox3.Items.Clear();
            // 添加所属课程
            string sql = "select `Name` from `tb_lesson`";
            DataTable LessonDT = DAL.MySqlDBTool.MySqlCodeByDT(sql);
            foreach (DataRow item in LessonDT.Rows)
                comboBox3.Items.Add(item.ItemArray[0].ToString());
        }
        #endregion
        #region 判断由哪个状态调用该窗体
        private void UpdateSUb_Load(object sender, EventArgs e)
        {
            switch (this.Tag.ToString())
            {
                #region 发卷_题库管理
                case "0":
                    // 设置窗体状态
                    panel0.Visible = true;
                    this.Size = new Size(968, 326);
                    this.Text = "修改套题";

                    int SelIndex = fajuanFrm.fF.dataSource.CurrentCell.RowIndex;
                    // 清空缓存
                    comboBox1.Items.Clear();

                    // 读取数据
                    comboBox1.Items.Add(SelIndex + 1);
                    comboBox1.SelectedIndex = 0;
                    textBox1.Text = fajuanFrm.fF.dataSource.Rows[SelIndex].Cells[1].Value.ToString();
                    comboBox2.Items.Add(fajuanFrm.fF.dataSource.Rows[SelIndex].Cells[2].Value);
                    comboBox2.SelectedIndex = 0;
                    if (fajuanFrm.fF.dataSource.Rows[SelIndex].Cells[3].Value.ToString() != string.Empty)
                    {
                        comboBox3.Items.Add(fajuanFrm.fF.dataSource.Rows[SelIndex].Cells[3].Value);
                        comboBox3.SelectedIndex = 0;
                    }
                    break;
                #endregion

                #region  发卷_考生管理[Excel导入名单[读取指定列]]
                case "1":
                    // 设置窗体状态
                    panel1.Visible = true;
                    this.Size = new Size(1200, 600);
                    this.Text = "Excel表格读取指定列";

                    EMFMethod.DGVStyleOfkaosheng(dataGridView1);// 示例数据列标题

                    #region 示例数据
                    List<string> AddNewRowData = new List<string>() { "1", "20180302138", "温狄荣", "男", "2018", "计算机网络技术", "计算机系", "2018级计算机网络技术1班" };
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[0].Height = 30;
                    for (int i = 0; i < 6; i++)
                        dataGridView1.Columns[i].FillWeight = 60;
                    for (int i = 0; i < AddNewRowData.Count; i++)
                    {
                        dataGridView1.Rows[0].Cells[i].Value = AddNewRowData[i];
                    }
                    #endregion

                    // 打开文件
                    OpenFileDialog file = new OpenFileDialog();
                    file.Filter = "Excel文件| *.xls*";
                    //file.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    file.Multiselect = false;

                    if (file.ShowDialog() == DialogResult.Cancel)
                        this.Close();

                    //判断文件后缀
                    path = file.FileName;
                    string filesuffix = Path.GetExtension(path);
                    if (string.IsNullOrEmpty(filesuffix))
                        this.Close();

                    // 调用读取Excel方法，返回Excel表格ToDataTable
                    Exceldt = DAL.ExcelDBTool.ReadExcelToTable(file, false);

                    foreach (DataGridViewColumn item in dataGridView1.Columns)
                        Colsarr.Add(item.HeaderText);
                    Colsarr.RemoveAt(0);

                    // 调用CreateArrToDGV方法。在dgv控件的每一列都创建CheckBox控件
                    if (Exceldt != null)
                    {
                        EMFMethod.CreateArrToDGV(flowLayoutPanel1, datagv, Exceldt.Columns.Count, Colsarr.ToArray());

                        DataTable TempExceldt = Exceldt.Copy();

                        // 只读取前10行数据
                        while (TempExceldt.Rows.Count > 10)
                            TempExceldt.Rows.RemoveAt(10);

                        // 读取Excel表格
                        datagv.DataSource = null;
                        datagv.DataSource = TempExceldt;
                    }
                    break;
                #endregion

                #region  发卷_考生管理[修改]
                case "2":
                    // 设置窗体状态
                    panel6.Visible = true;
                    this.Size = new Size(600, 500);
                    this.Text = "修改数据";

                    #region 下拉列表初始值
                    com2.Text = ExamineeManFrm.EMF.dataSource.CurrentRow.Cells[1].Value.ToString().ToString();
                    com3.Text = ExamineeManFrm.EMF.dataSource.CurrentRow.Cells[2].Value.ToString().ToString();
                    com6.Text = ExamineeManFrm.EMF.dataSource.CurrentRow.Cells[5].Value.ToString();
                    com8.Text = ExamineeManFrm.EMF.dataSource.CurrentRow.Cells[7].Value.ToString();

                    #region 性别
                    com4.Items.Add(ExamineeManFrm.EMF.dataSource.CurrentRow.Cells[3].Value);
                    if (com4.Items.Contains("男")) com4.Items.Add("女");
                    else com4.Items.Add("男");
                    com4.SelectedIndex = 0;
                    #endregion
                    #region 编号
                    com1.Items.Add(ExamineeManFrm.EMF.dataSource.CurrentRow.Cells[0].Value);
                    com1.SelectedIndex = 0;
                    #endregion
                    #region 年级
                    List<string> lst = new List<string>();
                    List<string> newlst = new List<string>();
                    for (int i = 0; i < ExamineeManFrm.EMF.dataSource.Rows.Count; i++)
                    {
                        if (ExamineeManFrm.EMF.dataSource.Rows[i].Cells[4].Value.ToString().Trim() == string.Empty)
                            continue;
                        lst.Add(ExamineeManFrm.EMF.dataSource.Rows[i].Cells[4].Value.ToString().Trim());
                    }

                    // 去重
                    newlst = lst.Distinct().ToList();

                    // 添加数据(年级)
                    foreach (var item in newlst)
                        com5.Items.Add(item);

                    foreach (var item in com5.Items)
                    {
                        if (item.ToString() == ExamineeManFrm.EMF.dataSource.CurrentRow.Cells[4].Value.ToString())
                        {
                            com5.SelectedItem = item;
                        }
                    }

                    #endregion

                    #region 院系
                    lst.Clear();
                    newlst.Clear();
                    for (int i = 0; i < ExamineeManFrm.EMF.dataSource.Rows.Count; i++)
                    {
                        if (ExamineeManFrm.EMF.dataSource.Rows[i].Cells[6].Value.ToString().Trim() == string.Empty)
                            continue;
                        lst.Add(ExamineeManFrm.EMF.dataSource.Rows[i].Cells[6].Value.ToString().Trim());
                    }

                    // 去重
                    newlst = lst.Distinct().ToList();

                    // 添加数据(院系)
                    foreach (var item in newlst)
                        com7.Items.Add(item);

                    foreach (var item in com7.Items)
                    {
                        if (item.ToString() == ExamineeManFrm.EMF.dataSource.CurrentRow.Cells[6].Value.ToString())
                        {
                            com7.SelectedItem = item;
                        }
                    }
                    #endregion
                    #region 专业
                    lst.Clear();
                    newlst.Clear();
                    for (int i = 0; i < ExamineeManFrm.EMF.dataSource.Rows.Count; i++)
                    {
                        if (ExamineeManFrm.EMF.dataSource.Rows[i].Cells[5].Value.ToString().Trim() == string.Empty)
                            continue;
                        lst.Add(ExamineeManFrm.EMF.dataSource.Rows[i].Cells[5].Value.ToString().Trim());
                    }

                    // 去重
                    newlst = lst.Distinct().ToList();

                    // 添加数据(专业)
                    foreach (var item in newlst)
                        com6.Items.Add(item);
                    #endregion
                    #region 班级
                    lst.Clear();
                    newlst.Clear();
                    for (int i = 0; i < ExamineeManFrm.EMF.dataSource.Rows.Count; i++)
                    {
                        if (ExamineeManFrm.EMF.dataSource.Rows[i].Cells[7].Value.ToString().Trim() == string.Empty)
                            continue;
                        lst.Add(ExamineeManFrm.EMF.dataSource.Rows[i].Cells[7].Value.ToString().Trim());
                    }

                    // 去重
                    newlst = lst.Distinct().ToList();

                    // 添加数据(班级)
                    foreach (var item in newlst)
                        com8.Items.Add(item);

                    foreach (var item in com8.Items)
                    {
                        if (item.ToString() == ExamineeManFrm.EMF.dataSource.CurrentRow.Cells[7].Value.ToString())
                        {
                            com8.SelectedItem = item;
                        }
                    }
                    #endregion
                    #endregion
                    break;
                #endregion

                #region  发卷_考生管理[批量删除]
                case "3":
                    // 设置窗体状态
                    panel5.Visible = true;
                    this.Size = new Size(680, 180);
                    this.Text = "批量删除数据";


                    break;
                #endregion

                #region  发卷_考生管理[批量添加至发卷名单]
                case "4":
                    // 设置窗体状态
                    panel5.Visible = true;
                    this.Size = new Size(680, 180);
                    this.Text = "批量添加数据至发卷名单";
                    this.button1.Text = "添加";
                    this.label8.Text = "说明：\r\n对数据进行批量添加。若添加单条数据，请设置开始值与结束值相等。";

                    break;
                    #endregion
            }
        }
        #endregion

        #region 读取Excel表格到dgv控件
        private void OverToLoadExcel_Click(object sender, EventArgs e)
        {
            bool IsOver = true;
            // 判断通过的条件
            foreach (ComboBox item in flowLayoutPanel1.Controls)
            {
                if (item.Items.Count > 2)
                {
                    if (item.Items.Contains("班级名称")) break;
                    MessageBox.Show("未完全选取指定列，请重试。");
                    IsOver = false;
                    return;
                }
            }

            if (IsOver)
            {
                // 获取选中列组的列标题
                List<string> SelColumns = new List<string>();
                // 获取年级所在列的索引
                int Gradeindex = 0;
                for (int i = 0; i < flowLayoutPanel1.Controls.Count; i++)
                {
                    if (flowLayoutPanel1.Controls[i].Text == "年级") Gradeindex = i;
                }
                for (int CA = 0; CA < Colsarr.Count; CA++)
                {
                    for (int FLP = 0; FLP < flowLayoutPanel1.Controls.Count; FLP++)
                        if (flowLayoutPanel1.Controls[FLP].Text == Colsarr[CA])
                            SelColumns.Add(datagv.Columns[FLP].DataPropertyName);
                }
                int ret = 0;// 返回影响行数
                string TxtPath = Path.GetDirectoryName(path) + @"\tmp.txt";// txt文件完整路径

                using (FileStream txtfile = new FileStream(TxtPath, FileMode.Create, FileAccess.Write))// 在本地创建txt文件，用于转换excel表格数据
                {
                    using (StreamWriter sw = new StreamWriter(txtfile, Encoding.UTF8))// 写入流，向txt文件写入数据
                    {
                        List<string> lst = new List<string>();// 存储Excel表格数据
                        lst.Add("null");// 填充作用。遍历的索引从1开始
                        foreach (DataRow dr in Exceldt.Rows)
                        {
                            lst.Add(string.Empty);
                            for (int i = 0; i < SelColumns.Count; i++)
                            {
                                if (i == 3) lst.Add(dr[SelColumns[i]].ToString().Substring(0, 4));
                                else lst.Add(dr[SelColumns[i]].ToString());
                            }
                            if (SelColumns.Count == 6) lst.Add(string.Empty);
                        }

                        for (int i = 1; i < lst.Count; i++)
                        {
                            if (i % 8 == 0)
                            {
                                sw.WriteLine(lst[i]);
                                continue;
                            }
                            sw.Write(lst[i] + "\t");
                        }
                        sw.Close();
                        txtfile.Close();
                    }
                }

                StringBuilder sqlsb = new StringBuilder();
                sqlsb.AppendFormat(@"LOAD DATA LOCAL INFILE '{0}' INTO TABLE {1} FIELDS TERMINATED BY '\t' LINES TERMINATED BY '\r\n' ;", TxtPath.Replace(@"\", "/"), EMFMethod._tbname);
                Debug.Print(sqlsb.ToString());
                sw.Restart();
                ret = MySqlDB.ImportDataToMySql(sqlsb.ToString());// 填充导入的Excel数据到表格并返回影响行数
                // ID重新排序
                //MySqlDB.ImportDataToMySql("ALTER TABLE `tb_stumanage`  DROP `ID` ");
                //MySqlDB.ImportDataToMySql("ALTER TABLE `tb_stumanage` ADD `ID` BIGINT(8) NOT NULL AUTO_INCREMENT FIRST, ADD PRIMARY KEY(`ID`) ");
                sw.Stop();

                switch (ret)// 判断返回的影响行数
                {
                    case -1:// 行数为 - 1，导入0条数据
                        MessageBox.Show("导入失败。导入数据与数据库重复 或 Excel表格没有数据。");
                        break;
                    case 0:
                        MessageBox.Show("未导入数据，数据库重复。");
                        break;
                    default:// 行数 > 0 导入成功
                            // 点击ok
                        MessageBox.Show(
                            string.Format("成功导入 {0} 条数据。耗时 {1} 秒", ret, sw.Elapsed.TotalSeconds)
                            , "导入");

                        // 将成功导入的数据填充到dgv控件
                        sw.Restart();
                        string sql = "select * from `tb_stumanage`";
                        DataTable newdt = DAL.MySqlDBTool.MySqlCodeByDT(sql);// 实例化Table表，可进行筛选数据
                        BindingSource bs = new BindingSource(newdt, null);
                        ExamineeManFrm.EMF.dataSource.DataSource = bs;
                        sw.Stop();
                        ExamineeManFrm.EMF.toolStripLabel1.Text = string.Format("执行此查询操作耗时： {0} 秒", sw.Elapsed.TotalSeconds);
                        this.Close();
                        break;
                }

            }
        }
        #endregion

        #region 批量删除/添加
        private void button1_Click(object sender, EventArgs e)
        {
            // 判断通过的条件
            if (textBox2.Text.Trim() == "" || textBox3.Text.Trim() == "")
                MessageBox.Show("序号填写错误或未填写，请重试。");
            else
            {
                int startdel = 0;
                int enddel = 0;
                try
                {
                    int.TryParse(textBox2.Text, out startdel);
                    int.TryParse(textBox3.Text, out enddel);
                }
                catch { MessageBox.Show("序号必须为数字，请重试。"); return; }
                if (startdel > enddel)
                {
                    MessageBox.Show("起始值必须小于或等于终止值");
                    return;
                }

                if (button1.Text == "删  除")
                {
                    string sql = null;
                    if (startdel == enddel) sql = string.Format(@"DELETE FROM `tb_stumanage` WHERE `ID` = {0} ", startdel);
                    else if (enddel > startdel) sql = string.Format(@"DELETE FROM `tb_stumanage` WHERE `ID` BETWEEN {0} AND {1} ", startdel, enddel);
                    int delcount = MySqlDB.ImportDataToMySql(sql);
                    if (delcount > 0)
                    {
                        for (int i = startdel; i <= enddel; i++)
                        {
                            for (int rowsindex = 0; rowsindex < ExamineeManFrm.EMF.dataSource.Rows.Count; rowsindex++)
                            {
                                if (ExamineeManFrm.EMF.dataSource.Rows[rowsindex].Cells[0].Value.ToString() == i.ToString())
                                {
                                    ExamineeManFrm.EMF.dataSource.Rows.RemoveAt(rowsindex);
                                    break;
                                }
                            }
                        }
                        sw.Restart();
                        DataTable newdt = EMFMethod.GetDgvToTable(ExamineeManFrm.EMF.dataSource);
                        BindingSource bs = new BindingSource(newdt, null);
                        ExamineeManFrm.EMF.dataSource.DataSource = bs;
                        sw.Stop();
                        ExamineeManFrm.EMF.toolStripLabel1.Text = string.Format("执行此查询操作耗时： {0} 秒", sw.Elapsed.TotalSeconds);

                        MessageBox.Show(string.Format(@"成功删除 {0} 条数据。请检查。", delcount));
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("删除操作失败，请检查要删除的序号是否在数据表内。");
                        return;
                    }
                }
                else// 添加
                {
                    List<string> newrow = new List<string>();
                    int TaotiIndex = ExamineeManFrm.TaotiIndex;
                    DataTable TaotiDT = ExamineeManFrm.TaotiDT;

                    DataTable newdt = BLL.Method.GetDgvToTable(ExamineeManFrm.EMF.dataGridView1).Clone();
                    for (int i = startdel; i <= enddel; i++)
                    {
                        for (int rowsindex = 0; rowsindex < ExamineeManFrm.EMF.dataSource.Rows.Count; rowsindex++)
                        {
                            if (ExamineeManFrm.EMF.dataSource.Rows[rowsindex].Cells[0].Value.ToString() == i.ToString())
                            {
                                newrow.Add(ExamineeManFrm.EMF.dataSource.Rows[rowsindex].Cells[1].Value.ToString());// 学号
                                newrow.Add(ExamineeManFrm.EMF.dataSource.Rows[rowsindex].Cells[2].Value.ToString());// 姓名
                                newrow.Add(ExamineeManFrm.EMF.dataSource.Rows[rowsindex].Cells[5].Value.ToString());// 专业
                                newrow.Add(ExamineeManFrm.EMF.dataSource.Rows[rowsindex].Cells[7].Value.ToString());// 班级名称
                                newrow.Add(TaotiDT.Rows[TaotiIndex].ItemArray[3].ToString());// 课程
                                newrow.Add(TaotiDT.Rows[TaotiIndex].ItemArray[1].ToString());// 套题名称
                                newrow.Add(TaotiDT.Rows[TaotiIndex].ItemArray[2].ToString());// 套题类型
                                newdt.Rows.Add(newrow.ToArray());
                                newrow.Clear();
                                break;
                            }
                        }
                    }

                    ExamineeManFrm.EMF.dataGridView1.DataSource = null;
                    Get.GridViewDataLoad(newdt, ExamineeManFrm.EMF.dataGridView1, ExamineeManFrm.EMF.bindingNavigator2);// 填充dataGridView1
                    Get.GridViewHeaderFilter(ExamineeManFrm.EMF.dataGridView1);// 标题添加ComBox并返回行数
                    if (newdt.Rows.Count > 0)
                        MessageBox.Show(string.Format(@"成功添加 {0} 条数据至发卷名单", newdt.Rows.Count));
                    else
                        MessageBox.Show(@"添加失败。只能对当前可见的编号进行添加");
                    this.Close();
                }
            }
        }

        #endregion
        #region 考生管理[修改]
        private void button3_Click(object sender, EventArgs e)
        {
            // 判断通过的条件
            int miss = 0;
            foreach (Control item in groupBox1.Controls)
            {
                if (item is ComboBox)
                {
                    if ((item as ComboBox).Text == "") miss++;
                }
            }
            foreach (Control item in groupBox1.Controls)
            {
                if (item is TextBox)
                {
                    if ((item as TextBox).Text == "") miss++;
                }
            }
            if (miss > 0) { MessageBox.Show("信息不完整，请重试。"); return; }
            #region 添加数据
            // 获取选中行的第一列的值
            int SelIndex = fajuanFrm.fF.dataSource.CurrentCell.RowIndex;

            // 实例MySql工具
            DAL.MySqlDBTool MySqltool = new DAL.MySqlDBTool();

            // 添加要修改的数据
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("ID", com1.Text);
            dic.Add("StuID", com2.Text);
            dic.Add("Name", com3.Text);
            dic.Add("Sex", com4.Text);
            dic.Add("Grade", com5.Text);
            dic.Add("Profession_name", com6.Text);
            dic.Add("Department", com7.Text);
            dic.Add("ClassName", com8.Text);

            // 添加条件
            Dictionary<string, string> Sourcedic = new Dictionary<string, string>();
            Sourcedic.Add("ID", ExamineeManFrm.EMF.dataSource.CurrentRow.Cells[0].Value.ToString());

            // 班级名称可为空
            if (comboBox3.Text == string.Empty)
                dic.Remove("ClassName");

            if (MySqltool.ParMySqlIDUCode("tb_stumanage", dic, Sourcedic) > 0)// 判断SQL语句是否输入正确
            {
                MySqltool.BindingDisplay(ExamineeManFrm.EMF.dataSource, "tb_stumanage", ExamineeManFrm.EMF.bindingNavigator1);
            }
            else
            {
                MessageBox.Show("数据修改失败，请重试。");
                return;
            }

            // 修改完毕退出
            MessageBox.Show("成功修改一条数据。");
            this.Close();
            #endregion
        }
        #endregion
    }
}

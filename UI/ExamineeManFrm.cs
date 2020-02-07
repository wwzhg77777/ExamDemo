using DataGridViewAutoFilter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using System.Diagnostics;
using System.IO;

namespace UI
{
    public partial class ExamineeManFrm : Form
    {
        #region 变量
        /// <summary>
        /// 实例化考生管理的方法组对象
        /// </summary>
        ExamineeManMethod EMFMethod = new ExamineeManMethod();

        /// <summary>
        /// 实例MySql工具
        /// </summary>
        DAL.MySqlDBTool MySqlDB = new DAL.MySqlDBTool();

        /// <summary>
        /// 原考生信息表
        /// </summary>
        DataTable ksdt;
        
        /// <summary>
        ///  字段数组
        /// </summary>
        string[] Loaddata = new string[] { "Department", "Grade", "Profession_name", "ClassName" };

        /// <summary>
        /// MDI主窗体，用于判断该窗体是否已创建
        /// </summary>
        ExamMainTea m1;

        /// <summary>
        /// 记录查询操作的耗时
        /// </summary>
        Stopwatch sw = new Stopwatch();

        #endregion
        public ExamineeManFrm(ExamMainTea mm)
        {
            InitializeComponent();
            m1 = mm;
            string sql = "select * from `tb_stumanage`";
            ksdt = DAL.MySqlDBTool.MySqlCodeByDT(sql);// 实例化Table表，可进行筛选数据
        }


        DataGridViewFunction Get = new DataGridViewFunction();
        /// <summary>
        /// 窗体加载事件-并加载数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExamineeManFrm_Load(object sender, EventArgs e)
        {
            #region 加载窗体事件
            KEY.ExamineeManFrmkey = "1";
            comboBox6.SelectedIndex = 2;
            EMFMethod.DGVStyleOfkaosheng(dataSource);

            #endregion

            #region dgv控件实现筛选功能
            sw.Start();
            Get.GridViewDataLoad(ksdt, dataSource, bindingNavigator1);// 填充dataSource
            sw.Stop();
            toolStripLabel1.Text = string.Format("执行此查询操作耗时： {0} 毫秒", sw.Elapsed.TotalMilliseconds);
            Get.GridViewHeaderFilter(dataSource);// 标题添加ComBox并返回行数

            EMFMethod.DGVStyleOffajuan(dataGridView1, bindingNavigator2);// 填充dataGridView1
        }

        private void dataSource_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            Get.GridViewDataCount(dataSource);//筛选时动态加载行数

        }

        #endregion

        private void ExamineeManFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            KEY.ExamineeManFrmkey = "";
        }

        #region 筛选条件

        private void comboBox1_DropDown(object sender, EventArgs e)// 院系
        {
            
            string lastvalue = null;
            if (comboBox1.Text != string.Empty) lastvalue = comboBox1.Text;
            comboBox1.Items.Clear();

            // 读取数据表指定列的数据并去除重复项
            List<string> lst = new List<string>();
            lst.Add("（全部）");
            for (int i = 0; i < dataSource.RowCount; i++)
            {
                if (dataSource.Rows[i].Cells["Department"].Value.ToString().Trim() == string.Empty)
                    continue;
                lst.Add(dataSource.Rows[i].Cells["Department"].Value.ToString().Trim());
            }
            List<string> Newlst = lst.Distinct().ToList();

            // 添加数据
            foreach (var item in Newlst)
            {
                comboBox1.Items.Add(item);
            }
            comboBox1.SelectedItem = lastvalue;
        }

        private void comboBox2_DropDown(object sender, EventArgs e)// 年级
        {
            string lastvalue = null;
            if (comboBox2.Text != string.Empty) lastvalue = comboBox2.Text;
            comboBox2.Items.Clear();

            // 读取数据表指定列的数据并去除重复项
            List<string> lst = new List<string>();
            lst.Add("（全部）");
            for (int i = 0; i < dataSource.RowCount; i++)
            {
                if (dataSource.Rows[i].Cells["Grade"].Value.ToString().Trim() == string.Empty)
                    continue;
                lst.Add(dataSource.Rows[i].Cells["Grade"].Value.ToString().Trim());
            }
            List<string> Newlst = lst.Distinct().ToList();

            // 添加数据
            foreach (var item in Newlst)
            {
                comboBox2.Items.Add(item);
            }
            comboBox2.SelectedItem = lastvalue;
        }

        private void comboBox3_DropDown(object sender, EventArgs e)// 专业
        {
            string lastvalue = null;
            if (comboBox3.Text != string.Empty) lastvalue = comboBox3.Text;
            comboBox3.Items.Clear();

            // 读取数据表指定列的数据并去除重复项
            List<string> lst = new List<string>();
            lst.Add("（全部）");
            for (int i = 0; i < dataSource.RowCount; i++)
            {
                if (dataSource.Rows[i].Cells["Profession_name"].Value.ToString().Trim() == string.Empty)
                    continue;
                lst.Add(dataSource.Rows[i].Cells["Profession_name"].Value.ToString().Trim());
            }
            List<string> Newlst = lst.Distinct().ToList();

            // 添加数据
            foreach (var item in Newlst)
            {
                comboBox3.Items.Add(item);
            }
            comboBox3.SelectedItem = lastvalue;
        }

        private void comboBox4_DropDown(object sender, EventArgs e)// 班级
        {
            string lastvalue = null;
            if (comboBox4.Text != string.Empty) lastvalue = comboBox4.Text;
            comboBox4.Items.Clear();

            // 读取数据表指定列的数据并去除重复项
            List<string> lst = new List<string>();
            lst.Add("（全部）");
            for (int i = 0; i < dataSource.RowCount; i++)
            {
                if (dataSource.Rows[i].Cells["ClassName"].Value.ToString().Trim() == string.Empty)
                    continue;
                lst.Add(dataSource.Rows[i].Cells["ClassName"].Value.ToString().Trim());
            }
            List<string> Newlst = lst.Distinct().ToList();

            // 添加数据
            foreach (var item in Newlst)
            {
                comboBox4.Items.Add(item);
            }
            comboBox4.SelectedItem = lastvalue;
        }

        #endregion

        #region 筛选数据

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)// 院系
        {
            
            var MaxCount = 0;// 第一列行数

            // 实例化添加要查询的条件
            Dictionary<string, string> dic = new Dictionary<string, string>();

            if (comboBox2.Text == "（全部）" && comboBox3.Text == "（全部）" && comboBox4.Text == "（全部）")
            {
                Get.GridViewDataLoad(ksdt, dataSource, bindingNavigator1);// 填充dataSource
                return;
            }
            
            MaxCount = dataSource.Rows.Count;

            dic.Clear();
            int Sourceindex = 0;
            // 添加要查询的条件
            for (int i = 1; i < 5; i++)
            {
                if (groupBox1.Controls["comboBox" + i].Text != "（全部）"&& groupBox1.Controls["comboBox" + i].Text != "")
                {
                    dic.Add(Loaddata[i-1], groupBox1.Controls["comboBox" + i].Text);
                    Sourceindex = i;
                    break;
                }
            }
            for (int i = 1; i < 5; i++)
            {
                if (i == Sourceindex) continue;
                if (groupBox1.Controls["comboBox" + i].Text != "（全部）" && groupBox1.Controls["comboBox" + i].Text != "")
                    dic.Add(Loaddata[i-1], groupBox1.Controls["comboBox" + i].Text.Trim());
            }
            sw.Restart();
            Get.GridViewDataLoad(DAL.MySqlDBTool.MySqlCodeByDT(DAL.MySqlDBTool.ParGetSelectMySql("tb_stumanage",dic)), dataSource, bindingNavigator1);// 填充dataSource
            sw.Stop();
            toolStripLabel1.Text = string.Format("执行此查询操作耗时： {0} 毫秒", sw.Elapsed.TotalMilliseconds);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)// 年级
        {

            var MaxCount = 0;// 第一列行数

            // 实例化添加要查询的条件
            Dictionary<string, string> dic = new Dictionary<string, string>();

            if (comboBox1.Text == "（全部）" && comboBox3.Text == "（全部）" && comboBox4.Text == "（全部）")
            {
                Get.GridViewDataLoad(ksdt, dataSource, bindingNavigator1);// 填充dataSource
                return;
            }
            
            MaxCount = dataSource.Rows.Count;;

            dic.Clear();
            int Sourceindex = 0;
            // 添加要查询的条件
            for (int i = 1; i < 5; i++)
            {
                if (groupBox1.Controls["comboBox" + i].Text != "（全部）" && groupBox1.Controls["comboBox" + i].Text != "")
                {
                    dic.Add(Loaddata[i - 1], groupBox1.Controls["comboBox" + i].Text);
                    Sourceindex = i;
                    break;
                }
            }
            for (int i = 1; i < 5; i++)
            {
                if (i == Sourceindex) continue;
                if (groupBox1.Controls["comboBox" + i].Text != "（全部）" && groupBox1.Controls["comboBox" + i].Text != "")
                    dic.Add(Loaddata[i - 1], groupBox1.Controls["comboBox" + i].Text.Trim());
            }
            sw.Restart();
            Get.GridViewDataLoad(DAL.MySqlDBTool.MySqlCodeByDT(DAL.MySqlDBTool.ParGetSelectMySql("tb_stumanage", dic)), dataSource, bindingNavigator1);// 填充dataSource
            sw.Stop();
            toolStripLabel1.Text = string.Format("执行此查询操作耗时： {0} 毫秒", sw.Elapsed.TotalMilliseconds);
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)// 专业
        {

            var MaxCount = 0;// 第一列行数

            // 实例化添加要查询的条件
            Dictionary<string, string> dic = new Dictionary<string, string>();

            if (comboBox1.Text == "（全部）" && comboBox2.Text == "（全部）" && comboBox4.Text == "（全部）")
            {
                Get.GridViewDataLoad(ksdt, dataSource, bindingNavigator1);// 填充dataSource
                return;
            }
            
            MaxCount = dataSource.Rows.Count; ;

            dic.Clear();
            int Sourceindex = 0;
            // 添加要查询的条件
            for (int i = 1; i < 5; i++)
            {
                if (groupBox1.Controls["comboBox" + i].Text != "（全部）" && groupBox1.Controls["comboBox" + i].Text != "")
                {
                    dic.Add(Loaddata[i - 1], groupBox1.Controls["comboBox" + i].Text);
                    Sourceindex = i;
                    break;
                }
            }
            for (int i = 1; i < 5; i++)
            {
                if (i == Sourceindex) continue;
                if (groupBox1.Controls["comboBox" + i].Text != "（全部）" && groupBox1.Controls["comboBox" + i].Text != "")
                    dic.Add(Loaddata[i - 1], groupBox1.Controls["comboBox" + i].Text.Trim());
            }
            sw.Restart();
            Get.GridViewDataLoad(DAL.MySqlDBTool.MySqlCodeByDT(DAL.MySqlDBTool.ParGetSelectMySql("tb_stumanage", dic)), dataSource, bindingNavigator1);// 填充dataSource
            sw.Stop();
            toolStripLabel1.Text = string.Format("执行此查询操作耗时： {0} 毫秒", sw.Elapsed.TotalMilliseconds);
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)// 班级
        {

            var MaxCount = 0;// 第一列行数

            // 实例化添加要查询的条件
            Dictionary<string, string> dic = new Dictionary<string, string>();

            if (comboBox1.Text == "（全部）" && comboBox2.Text == "（全部）" && comboBox3.Text == "（全部）")
            {
                Get.GridViewDataLoad(ksdt, dataSource, bindingNavigator1);// 填充dataSource
                return;
            }
            
            MaxCount = dataSource.Rows.Count; ;

            dic.Clear();
            int Sourceindex = 0;
            // 添加要查询的条件
            for (int i = 1; i < 5; i++)
            {
                if (groupBox1.Controls["comboBox" + i].Text != "（全部）" && groupBox1.Controls["comboBox" + i].Text != "")
                {
                    dic.Add(Loaddata[i - 1], groupBox1.Controls["comboBox" + i].Text);
                    Sourceindex = i;
                    break;
                }
            }
            for (int i = 1; i < 5; i++)
            {
                if (i == Sourceindex) continue;
                if (groupBox1.Controls["comboBox" + i].Text != "（全部）" && groupBox1.Controls["comboBox" + i].Text != "")
                    dic.Add(Loaddata[i - 1], groupBox1.Controls["comboBox" + i].Text.Trim());
            }
            sw.Restart();
            Get.GridViewDataLoad(DAL.MySqlDBTool.MySqlCodeByDT(DAL.MySqlDBTool.ParGetSelectMySql("tb_stumanage", dic)), dataSource, bindingNavigator1);// 填充dataSource
            sw.Stop();
            toolStripLabel1.Text = string.Format("执行此查询操作耗时： {0} 毫秒", sw.Elapsed.TotalMilliseconds);
        }
        #endregion

        #region  Excel导入数据

        private void btnAddEx_Click(object sender, EventArgs e)
        {
            // 打开文件
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "Excel文件| *.xls*";
            file.Filter = "Excel(*.xls)|*.xls";
            //file.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            file.Multiselect = false;
            if (file.ShowDialog() == DialogResult.Cancel)
                return;

            //判断文件后缀
            var path = file.FileName;
            var filename = Path.GetFileName(file.FileName);
            string filesuffix = Path.GetExtension(path);
            if (string.IsNullOrEmpty(filesuffix))
                return;


            string sql = @"LOAD DATA LOCAL INFILE 'C:/Users/Administrator/Desktop/1.txt' INTO TABLE tb_stumanage FIELDS TERMINATED BY '\t' LINES TERMINATED BY '\r\n' ;";
            sw.Start();
            MySqlDB.Customsql(sql);
            sw.Stop();
            toolStripLabel1.Text =string.Format("执行此查询操作耗时： {0} 毫秒",sw.Elapsed.TotalMilliseconds);
        }

        #endregion

        #region 修改
        private void btnRevise_Click(object sender, EventArgs e)
        {
            if (dataSource.CurrentCell == null)
                MessageBox.Show("未选中或无数据，请添加。");
            else
            {
                UpdateSUb US = new UpdateSUb();
                if (US.Tag.ToString() != "1")
                {
                    US.Tag = "1";
                    US.ShowDialog();
                }
            }
        }
        #endregion
    }
}
/*
 * 
 * switch (comboBox4.Text)
            {
                case "（全部）":
                    if ((comboBox1.Text == "（全部）" || comboBox1.Text == string.Empty) &&
                        (comboBox2.Text == "（全部）" || comboBox2.Text == string.Empty) &&
                        (comboBox3.Text == "（全部）" || comboBox3.Text == string.Empty) &&
                        (comboBox4.Text == "（全部）" || comboBox4.Text == string.Empty))
                    {
                        Get.GridViewDataLoad(ksdt, dataSource, bindingNavigator1);// 填充dataSource
                        comboBox4.BackColor = SystemColors.ScrollBar;
                        return;
                    }
                    Get.GridViewDataLoad(Newksdt, dataSource, bindingNavigator1);// 填充dataSource
                    comboBox4.BackColor = SystemColors.ScrollBar;
                    break;

                default:// 其他
                    if (ManyScreenksdt != null)// 判断是否多次筛选
                    {
                        foreach (DataRow dr in Newksdt.Rows) // 遍历dt
                        {
                            if (!string.IsNullOrEmpty(dr["ClassName"].ToString()))//判断值是否为空
                            {
                                DataRow[] tmpdrs = Newksdt.Select(string.Format("ClassName='{0}'", comboBox4.Text)); //筛选数据
                                if (tmpdrs.Length != 0)// 判断筛选之后的数据不为空
                                {
                                    ManyScreenksdt = Newksdt.Clone();
                                    for (int i = 0; i < tmpdrs.Length; i++)
                                    {
                                        ManyScreenksdt.ImportRow(tmpdrs[i]);// ImportRow 是复制
                                    }
                                    Get.GridViewDataLoad(ManyScreenksdt, dataSource, bindingNavigator1);// 填充dataSource
                                    Newksdt = ManyScreenksdt;
                                }
                            }
                        }
                    }
                    else
                    {
                        StringBuilder sb = new StringBuilder(255);
                        sb.AppendFormat("ClassName='{0}'", comboBox4.Text);
                        if (comboBox1.Text != "（全部）")
                            sb.AppendFormat(" and Department='{0}'", comboBox1.Text);
                        if (comboBox2.Text != "（全部）")
                            sb.AppendFormat(" and Grade='{0}'", comboBox2.Text);
                        if (comboBox3.Text != "（全部）")
                            sb.AppendFormat(" and Profession_name='{0}'", comboBox3.Text);
                        MessageBox.Show(sb.ToString());
                        foreach (DataRow dr in ksdt.Rows) // 遍历dt
                        {
                            if (!string.IsNullOrEmpty(dr["ClassName"].ToString()))//判断值是否为空
                            {
                                DataRow[] tmpdrs = ksdt.Select(sb.ToString()); //筛选数据
                                if (tmpdrs.Length != 0)// 判断筛选之后的数据不为空
                                {
                                    ManyScreenksdt = new DataTable();
                                    Newksdt = ksdt.Clone();
                                    for (int i = 0; i < tmpdrs.Length; i++)
                                    {
                                        Newksdt.ImportRow(tmpdrs[i]);// ImportRow 是复制
                                    }
                                    Get.GridViewDataLoad(Newksdt, dataSource, bindingNavigator1);// 填充dataSource

                                }
                            }
                        }
                    }
                    break;
            }
            */
/*
 * 
 * foreach (DataRow dr in ksdt.Rows) // 遍历dt
            {
                if (string.IsNullOrEmpty(dr["Department"].ToString()))//判断是否存在该院系，存在继续执行
                {
                    DataRow[] tmpdr = null;
                    tmpdr = ksdt.Select(string.Format("Department='{0}'", dr["Department"].ToString())); //筛选数据
                    if (tmpdr.Length != 0)
                    {
                        DataRow[] tmpDrs2 = null;
                        tmpDrs2 = ksdt.Select(string.Format("ID='{0}'", tmpdr[0].ItemArray[0]));
                        for (int i = 0; i < tmpDrs2.Length - 1; i++) // 修改产生余料编号
                        {
                            DataRow dRow = tmpDrs2[i];
                            dRow.BeginEdit(); //开始编辑
                            dRow["Department"] = tmpdr[0].ItemArray[5];
                            dRow.EndEdit(); //结束编辑
                            ksdt.AcceptChanges(); // 保存修改的结果
                        }
                    }
                }
            }
            */
//        #region 窗体加载事件
//        private void ExamineeManFrm_Load(object sender, EventArgs e)
//        {
//            #region 添加用户列表
//            listView1.Items.Add("管理员用户");
//            listView1.Items.Add("教师用户");
//            listView1.Items.Add("学生用户");
//            for (int i = 0; i < 3; i++)
//                listView1.Items[i].ImageIndex = i;
//            #endregion

//            #region 隐藏控件
//            for (int i = 10; i < 18; i++)
//                panel2.Controls["label" + i].Visible = false;
//            #endregion
//        }
//        #endregion

//        #region 设置侧边栏效果
//        private void panel3_Click(object sender, EventArgs e)
//        {
//            if (listView1.Visible == true)
//            {
//                listView1.Visible = false;
//                label0.Text = "》";
//            }
//            else
//            {
//                listView1.Visible = true;
//                label0.Text = "《";
//            }
//        }

//        #endregion

//        #region 数据库查询操作
//        private void FindDB_Click(object sender, EventArgs e)
//        {
//            if (listView1.SelectedItems.Count == 0)
//            {
//                MessageBox.Show("请选择其中一项进行查询");
//                return;
//            }

//            switch (listView1.SelectedItems[0].Text)
//            {
//                case "管理员用户":
//                    Login.DAL.TeaDAO.DataGridViewStyleAd(dataGridView1);
//                    Login.DAL.TeaDAO.Display(dataGridView1, "tb_administrator");
//                    // 只显示需要的控件
//                    for (int i = 1; i < 9; i++)
//                    {
//                        if (i < 3)
//                        {
//                            panel2.Controls["label" + i].Visible = true;
//                            panel2.Controls["text" + i].Visible = true;
//                        }
//                        if (i >= 3)
//                        {
//                            panel2.Controls["label" + i].Visible = false;
//                            panel2.Controls["text" + i].Visible = false;
//                        }
//                    }
//                    break;
//                case "教师用户":
//                    Login.DAL.TeaDAO.DataGridViewStyleTe(dataGridView1);
//                    Login.DAL.TeaDAO.Display(dataGridView1, "tb_teacher");
//                    // 只显示需要的控件
//                    for (int i = 1; i < 10; i++)
//                    {
//                        if (i < 8)
//                        {
//                            panel2.Controls["label" + i].Visible = true;
//                            panel2.Controls["text" + i].Visible = true;
//                        }
//                        if (i >= 8)
//                        {
//                            panel2.Controls["label" + i].Visible = false;
//                            panel2.Controls["text" + i].Visible = false;
//                        }
//                    }
//                    break;
//                case "学生用户":
//                    Login.DAL.TeaDAO.DataGridViewStyleSt(dataGridView1);
//                    Login.DAL.TeaDAO.Display(dataGridView1, "tb_student");
//                    // 只显示需要的控件
//                    for (int i = 1; i < 10; i++)
//                    {
//          
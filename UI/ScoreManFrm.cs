using BLL;
using DataGridViewAutoFilter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI
{
    public partial class ScoreManFrm : Form
    {
        #region 变量
        static DataTable dtForlesson;// 课程table

        static DataTable dtForDepartment;// 院系table

        static DataTable dtForProfession;// 专业table

        ExamMainTea m1;// 主窗体

        /// <summary>
        /// 实例MySql工具
        /// </summary>
        DAL.MySqlDBTool MySqlDB = new DAL.MySqlDBTool();

        /// <summary>
        ///  字段数组lesson
        /// </summary>
        string[] Loaddatalesson = new string[] { "ID", "Name", "ofProfession" };

        /// <summary>
        ///  字段数组department
        /// </summary>
        string[] Loaddatadepartment = new string[] { "ID", "Name" };

        /// <summary>
        ///  字段数组profession
        /// </summary>
        string[] Loaddataprofession = new string[] { "ID", "Name", "ofDepartment" };

        /// <summary>
        /// dgv控件添加筛选功能
        /// </summary>
        DataGridViewFunction Get = new DataGridViewFunction();

        public string sql = "";// 查询字符串
        #endregion
        #region 构造函数
        public ScoreManFrm(ExamMainTea mm)
        {
            InitializeComponent();
            m1 = mm;
            sql = "SELECT * FROM `tb_lesson`";
            dtForlesson = DAL.MySqlDBTool.MySqlCodeByDT(sql);

            sql = "SELECT * FROM `tb_department`";
            dtForDepartment = DAL.MySqlDBTool.MySqlCodeByDT(sql);

            sql = "SELECT * FROM `tb_profession`";
            dtForProfession = DAL.MySqlDBTool.MySqlCodeByDT(sql);

        }
        #endregion
        #region 窗体加载事件
        private void ScoreManFrm_Load(object sender, EventArgs e)
        {
            BLL.KEY.ScoreManFrmkey = "1";
            // 课程
            BLL.Method.DGVStyleToLesson(dataGridView1);
            Get.GridViewDataLoad(dtForlesson, dataGridView1, bindingNavigator1);// 填充dataSource
            Get.GridViewHeaderFilter(dataGridView1);
            BLL.Method.SetListViewSpacing(listView1, 80, 120);// 设置图标之间的间距      
            listView1.Clear();
            listView1.LargeImageList = imageList2;
            listView1.Items.Add("查询信息", 0);
            listView1.Items.Add("添加信息", 2);
            listView1.Items.Add("删除信息", 1);
            listView1.Items.Add("修改信息", 3);

            // 院系
            BLL.Method.DGVStyleToDepartment(dataGridView2);
            Get.GridViewDataLoad(dtForDepartment, dataGridView2, bindingNavigator1);// 填充dataSource
            Get.GridViewHeaderFilter(dataGridView2);
            BLL.Method.SetListViewSpacing(listView2, 80, 120);// 设置图标之间的间距      
            listView2.Clear();
            listView2.LargeImageList = imageList2;
            listView2.Items.Add("查询信息", 0);
            listView2.Items.Add("添加信息", 2);
            listView2.Items.Add("删除信息", 1);
            listView2.Items.Add("修改信息", 3);

            // 专业
            BLL.Method.DGVStyleToProfession(dataGridView3);
            Get.GridViewDataLoad(dtForProfession, dataGridView3, bindingNavigator1);// 填充dataSource
            Get.GridViewHeaderFilter(dataGridView3);
            BLL.Method.SetListViewSpacing(listView3, 80, 120);// 设置图标之间的间距      
            listView3.Clear();
            listView3.LargeImageList = imageList2;
            listView3.Items.Add("查询信息", 0);
            listView3.Items.Add("添加信息", 2);
            listView3.Items.Add("删除信息", 1);
            listView3.Items.Add("修改信息", 3);
            // 专业列表
            ProfessionList(list1);
            // 院系列表
            DepartmentList(list2);
        }

        private void ScoreManFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            BLL.KEY.ScoreManFrmkey = "";
        }
        #endregion
        #region 导航栏刷新

        private void toolStripButton1_Click(object sender, EventArgs e)// 课程[刷新]
        {
            MySqlDB.BindingDisplay(dataGridView1, "tb_lesson", bindingNavigator1);
        }

        private void toolStripButton8_Click(object sender, EventArgs e)// 院系[刷新]
        {
            MySqlDB.BindingDisplay(dataGridView2, "tb_department", bindingNavigator2);
        }

        private void toolStripButton11_Click(object sender, EventArgs e)// 专业[刷新]
        {

            MySqlDB.BindingDisplay(dataGridView3, "tb_profession", bindingNavigator3);
        }
        #endregion
        #region datatable表内筛选数据
        private BindingSource SelectToDataTable(Dictionary<string, string> dic, DataTable dt, BindingNavigator bn)
        {
            if (dic.Count > 0)
            {
                StringBuilder sb = new StringBuilder(Model.ExamInfo.BufferSize);
                sb.AppendFormat("{0} like '%{1}%' ", dic.Keys.ToList()[0], dic.Values.ToList()[0]);
                // 添加条件
                if (dic != null && dic.Count > 0)
                {// Department = '计算机系' and Grade = '2018'
                 // 第一个条件
                    for (int i = 1; i < dic.Count; i++)
                        sb.AppendFormat("AND {0} like '%{1}%' ", dic.Keys.ToList()[i], dic.Values.ToList()[i]);
                }
                MessageBox.Show(sb.ToString());
                DataRow[] drs = dt.Select(sb.ToString());
                DataTable newdt = drs.CopyToDataTable();
                BindingSource bs = new BindingSource(newdt, null);
                bn.BindingSource = bs;
                return bs;
            }
            else
            {
                BindingSource bs = new BindingSource(dt, null);
                bn.BindingSource = bs;
                return bs;
            }
        }
        #endregion
        #region 专业列表
        private void ProfessionList(ListBox list)
        {
            list.Items.Clear();
            List<string> lst = new List<string>();
            List<string> newlst = new List<string>();
            for (int i = 0; i < dataGridView3.Rows.Count; i++)
            {
                if (dataGridView3.Rows[i].Cells[1].Value.ToString().Trim() == string.Empty)
                    continue;
                lst.Add(dataGridView3.Rows[i].Cells[1].Value.ToString().Trim());
            }

            // 去重
            newlst = lst.Distinct().ToList();

            // 添加数据
            foreach (var item in newlst)
                list.Items.Add(item);
        }
        #endregion
        #region 院系列表
        private void DepartmentList(ListBox list)
        {
            list.Items.Clear();
            List<string> lst = new List<string>();
            List<string> newlst = new List<string>();
            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                if (dataGridView2.Rows[i].Cells[1].Value.ToString().Trim() == string.Empty)
                    continue;
                lst.Add(dataGridView2.Rows[i].Cells[1].Value.ToString().Trim());
            }

            // 去重
            newlst = lst.Distinct().ToList();

            // 添加数据
            foreach (var item in newlst)
                list.Items.Add(item);
        }
        #endregion
        #region 列表框

        private void button5_Click(object sender, EventArgs e)
        {
            ProfessionList(list1);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DepartmentList(list2);
        }
        #endregion
        #region 判断选中项[课程管理]
        private void listView1_Click(object sender, EventArgs e)
        {
            MySqlDB.BindingDisplay(dataGridView1, "tb_lesson", bindingNavigator1);
            switch (((ListView)sender).SelectedItems[0].Index)
            {
                case 0:// 查询
                    // 初始状态
                    this.label1.Text = "按条件查询:";
                    this.btn1.Text = "查 询";

                    break;
                case 1:// 添加
                    // 初始状态
                    this.label1.Text = "填写信息添加:";
                    this.btn1.Text = "添 加";
                    for (int i = 1; i < 4; i++)
                        ((ComboBox)panel5.Controls["com" + i]).Items.Clear();
                    break;
                case 2:// 删除
                    // 初始状态
                    this.label1.Text = "按指定编号删除:";
                    this.btn1.Text = "删 除";

                    break;
                case 3:// 修改
                    // 初始状态
                    this.label1.Text = "按指定编号修改:";
                    this.btn1.Text = "修 改";
                    break;
            }

        }
        #endregion
        #region 判断选中项[院系管理]
        private void listView2_Click(object sender, EventArgs e)
        {
            MySqlDB.BindingDisplay(dataGridView2, "tb_department", bindingNavigator2);
            switch (((ListView)sender).SelectedItems[0].Index)
            {
                case 0:// 查询
                    // 初始状态
                    this.label2.Text = "按条件查询:";
                    this.btn2.Text = "查 询";

                    break;
                case 1:// 添加
                    // 初始状态
                    this.label2.Text = "填写信息添加:";
                    this.btn2.Text = "添 加";
                    for (int i = 4; i < 6; i++)
                        ((ComboBox)panel6.Controls["com" + i]).Items.Clear();

                    break;
                case 2:// 删除
                    // 初始状态
                    this.label2.Text = "按指定编号删除:";
                    this.btn2.Text = "删 除";

                    break;
                case 3:// 修改
                    // 初始状态
                    this.label2.Text = "按指定编号修改:";
                    this.btn2.Text = "修 改";
                    break;
            }
        }
        #endregion
        #region 判断选中项[专业管理]
        private void listView3_Click(object sender, EventArgs e)
        {
            MySqlDB.BindingDisplay(dataGridView3, "tb_profession", bindingNavigator3);
            switch (((ListView)sender).SelectedItems[0].Index)
            {
                case 0:// 查询
                    // 初始状态
                    this.label5.Text = "按条件查询:";
                    this.button3.Text = "查 询";

                    break;
                case 1:// 添加
                    // 初始状态
                    this.label5.Text = "填写信息添加:";
                    this.button3.Text = "添 加";
                    for (int i = 1; i < 4; i++)
                        ((ComboBox)panel7.Controls["comboBox" + i]).Items.Clear();

                    break;
                case 2:// 删除
                    // 初始状态
                    this.label5.Text = "按指定编号删除:";
                    this.button3.Text = "删 除";

                    break;
                case 3:// 修改
                    // 初始状态
                    this.label5.Text = "按指定编号修改:";
                    this.button3.Text = "修 改";
                    break;
            }
        }
        #endregion

        #region 课程管理[读取数据下拉状态]
        private void com1_DropDown(object sender, EventArgs e)// 编号
        {
            int MaxCount = 0;
            switch (label1.Text)
            {
                case "填写信息添加:":// 添加
                    ((ComboBox)sender).Items.Clear();
                    MaxCount = dataGridView1.Rows.Count;
                    if (MaxCount > 0)
                    {
                        Model.ExamInfo.MySqlInsIndex = ((long)dataGridView1.Rows[MaxCount - 1].Cells[0].Value) + 1L;
                        ((ComboBox)sender).Items.Add(Model.ExamInfo.MySqlInsIndex);
                    }
                    else
                    {
                        Model.ExamInfo.MySqlInsIndex = 1;
                        ((ComboBox)sender).Items.Add(1);
                    }
                    ((ComboBox)sender).SelectedIndex = 0;

                    break;
                case "按条件查询:":
                    // 读取编号
                    ((ComboBox)sender).Items.Clear();
                    ((ComboBox)sender).Items.Add("（全部）");
                    ((ComboBox)sender).SelectedIndex = 0;
                    break;
                default:// 其他
                    // 读取编号
                    ((ComboBox)sender).Items.Clear();
                    foreach (DataGridViewRow item in dataGridView1.Rows)
                        ((ComboBox)sender).Items.Add(item.Cells[0].Value);
                    break;
            }
        }

        private void com2_DropDown(object sender, EventArgs e)// 名称
        {
            switch (label1.Text)
            {
                case "按条件查询:":
                    ((ComboBox)sender).Items.Clear();
                    ((ComboBox)sender).Items.Add("（全部）");
                    foreach (DataGridViewRow item in dataGridView1.Rows)
                        ((ComboBox)sender).Items.Add(item.Cells[1].Value);
                    break;
                case "填写信息添加:":// 添加

                    break;
                case "按指定编号删除:":// 删除

                    break;
                case "按指定编号修改:":// 修改

                    break;
            }
        }

        private void com3_DropDown(object sender, EventArgs e)// 专业
        {
            List<string> lst = new List<string>();
            List<string> Newlst = null;
            switch (label1.Text)
            {
                case "按条件查询:":// 查询
                    ((ComboBox)sender).Items.Clear();
                    lst.Add("（全部）");
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        if (dataGridView1.Rows[i].Cells[2].Value.ToString().Trim() == string.Empty)
                            continue;
                        lst.Add(dataGridView1.Rows[i].Cells[2].Value.ToString().Trim());
                    }

                    // 去重
                    Newlst = lst.Distinct().ToList();

                    // 添加数据
                    foreach (var item in Newlst)
                    {
                        ((ComboBox)sender).Items.Add(item);
                    }
                    break;

                case "填写信息添加:":// 添加
                    ((ComboBox)sender).Items.Clear();
                    // 读取专业
                    for (int i = 0; i < dataGridView3.Rows.Count; i++)
                    {
                        if (dataGridView3.Rows[i].Cells[1].Value.ToString().Trim() == string.Empty)
                            continue;
                        lst.Add(dataGridView3.Rows[i].Cells[1].Value.ToString().Trim());
                    }

                    // 去重
                    Newlst = lst.Distinct().ToList();

                    // 添加数据
                    foreach (var item in Newlst)
                    {
                        ((ComboBox)sender).Items.Add(item);
                    }
                    break;
                case "按指定编号删除:":// 删除
                    ((ComboBox)sender).Items.Clear();
                    break;
                case "按指定编号修改:":// 修改

                    ((ComboBox)sender).Items.Clear();
                    // 读取专业
                    for (int i = 0; i < dataGridView3.Rows.Count; i++)
                    {
                        if (dataGridView3.Rows[i].Cells[1].Value.ToString().Trim() == string.Empty)
                            continue;
                        lst.Add(dataGridView3.Rows[i].Cells[1].Value.ToString().Trim());
                    }

                    // 去重
                    Newlst = lst.Distinct().ToList();

                    // 添加数据
                    foreach (var item in Newlst)
                    {
                        ((ComboBox)sender).Items.Add(item);
                    }
                    break;
            }
        }
        #endregion
        #region 课程管理[读取数据提交状态]
        private void com1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            switch (label1.Text)
            {
                case "按条件查询:":// 查询
                    break;

                case "填写信息添加:":// 添加
                    break;
                case "按指定编号删除:":// 删除

                    break;
                case "按指定编号修改:":// 修改
                    int RowsIndex = 0;
                    int.TryParse(com1.Text, out RowsIndex);
                    com2.Items.Clear();
                    com2.Text = dataGridView1.Rows[RowsIndex - 1].Cells[1].Value.ToString();
                    com3.Items.Clear();
                    com3.Items.Add(dataGridView1.Rows[RowsIndex - 1].Cells[2].Value.ToString());
                    com3.SelectedIndex = 0;
                    break;
            }
        }

        #endregion
        #region 课程管理[增删改查]
        private void btn1_Click(object sender, EventArgs e)
        {
            Stopwatch sw = new Stopwatch();
            int miss = 0;
            int MaxCount = 0;
            // 实例化添加要查询的条件
            Dictionary<string, string> dic = new Dictionary<string, string>();
            Dictionary<string, string> Sourcedic = new Dictionary<string, string>();
            StringBuilder sb = new StringBuilder();
            switch (label1.Text)
            {
                case "按条件查询:":// 查询
                    if (dataGridView1.Rows.Count == 0) { MessageBox.Show("数据表内没有数据，请添加。"); return; }
                    if (com1.Text == "（全部）" && com2.Text == "（全部）" && com3.Text == "（全部）")
                    {
                        sw.Restart();
                        this.sql = "SELECT * FROM `tb_lesson`";
                        dtForlesson = DAL.MySqlDBTool.MySqlCodeByDT(this.sql);
                        Get.GridViewDataLoad(dtForlesson, dataGridView1, bindingNavigator1);// 填充dataSource
                        sw.Stop();
                        toolStripLabel1.Text = string.Format("执行此查询操作耗时： {0} 秒", sw.Elapsed.TotalSeconds);
                        return;
                    }

                    dic.Clear();
                    int Sourceindex = 0;
                    // 添加要查询的条件
                    for (int i = 1; i < 4; i++)
                    {
                        if (panel5.Controls["com" + i].Text != "（全部）" && panel5.Controls["com" + i].Text != "")
                        {
                            dic.Add(Loaddatalesson[i - 1], panel5.Controls["com" + i].Text.Trim());
                            Sourceindex = i;
                            break;
                        }
                    }
                    for (int i = 1; i < 4; i++)
                    {
                        if (i == Sourceindex) continue;
                        if (panel5.Controls["com" + i].Text != "（全部）" && panel5.Controls["com" + i].Text != "")
                            dic.Add(Loaddatalesson[i - 1], panel5.Controls["com" + i].Text.Trim());
                    }
                    dataGridView1.DataSource = null;
                    try
                    {
                        sw.Restart();
                        this.sql = "SELECT * FROM `tb_lesson`";
                        dtForlesson = DAL.MySqlDBTool.MySqlCodeByDT(this.sql);
                        dataGridView1.DataSource = SelectToDataTable(dic, dtForlesson, bindingNavigator1);
                        sw.Stop();
                        toolStripLabel1.Text = string.Format("执行此查询操作耗时： {0} 秒", sw.Elapsed.TotalSeconds);

                    }
                    catch
                    {
                        MessageBox.Show("查询为空");

                        MySqlDB.BindingDisplay(dataGridView1, "tb_lesson", bindingNavigator1);
                    }
                    break;

                case "填写信息添加:":// 添加
                    #region 添加数据
                    miss = 0;
                    for (int i = 1; i < 3; i++)
                    {
                        if (panel5.Controls["com" + i].Text == "")
                            miss++;
                    }
                    if (miss > 0) { MessageBox.Show("信息不完整，请重试。1"); return; }

                    MaxCount = dataGridView1.Rows.Count;
                    if (MaxCount > 0)
                        Model.ExamInfo.MySqlInsIndex = ((long)dataGridView1.Rows[MaxCount - 1].Cells[0].Value) + 1L;
                    else
                        Model.ExamInfo.MySqlInsIndex = 1;

                    // 添加要插入的数据
                    dic.Clear();
                    dic.Add("ID", Model.ExamInfo.MySqlInsIndex.ToString());
                    dic.Add("Name", com2.Text);
                    dic.Add("ofProfession", com3.Text);

                    // 所属专业可为空
                    if (com3.Text == string.Empty)
                        dic.Remove("ofProfession");

                    if (MySqlDB.ParMySqlIDUCode("tb_lesson", dic) > 0)// 判断SQL语句是否输入正确
                    {
                        MySqlDB.BindingDisplay(dataGridView1, "tb_lesson", bindingNavigator1);
                        MessageBox.Show("成功录入一条数据");
                    }
                    else
                    {
                        MessageBox.Show("数据添加失败，请重试。");
                        return;
                    }
                    int NextIndex = 0;
                    int.TryParse(com1.Text, out NextIndex);
                    com1.Items.Clear();
                    com1.Items.Add(NextIndex + 1);
                    com1.SelectedIndex = 0;
                    #endregion
                    break;
                case "按指定编号删除:":// 删除
                    MaxCount = dataGridView1.Rows.Count;
                    if (MaxCount > 0)
                        Model.ExamInfo.MySqlInsIndex = ((long)dataGridView1.Rows[MaxCount - 1].Cells[0].Value) + 1L;
                    else
                        Model.ExamInfo.MySqlInsIndex = 1;

                    int RemoveIndex = 0;
                    int.TryParse(com1.Text, out RemoveIndex);

                    if (com1.Text.Trim() == "") { MessageBox.Show("请选择编号进行删除"); return; }
                    if (MessageBox.Show("确认删除编号为 " + com1.Text + " 的数据吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2) != DialogResult.OK)
                        return;

                    string sql = string.Format(@"DELETE FROM `{0}` WHERE `ID` = {1} ", "tb_lesson", RemoveIndex);
                    if (MySqlDB.ImportDataToMySql(sql) > 0)
                    {
                        foreach (DataGridViewRow item in dataGridView1.Rows)
                            if (item.Cells[0].Value.ToString() == com1.Text) { dataGridView1.Rows.Remove(item); break; }

                        if (Model.ExamInfo.MySqlInsIndex == (long)RemoveIndex)// 刷新数据表
                        {
                            MySqlDB.BindingDisplay(dataGridView1, "tb_lesson", bindingNavigator1);
                            return;
                        }

                        // 删除ID为n，将 n + 1 的ID值 - 1
                        dic.Clear();
                        dic.Add("ID", "ID-1");
                        string cusapp = @" ID BETWEEN " + RemoveIndex + @" AND " + Model.ExamInfo.MySqlInsIndex + " ORDER BY " + dic.Keys.ToList()[0] + " ASC";

                        MySqlDB.CusMySqlIDUCode("tb_lesson", cusapp, dic);
                        MySqlDB.BindingDisplay(dataGridView1, "tb_lesson", bindingNavigator1);
                    }
                    else { MessageBox.Show("数据删除失败，请重试"); return; }
                    com1.Items.Clear();
                    break;
                case "按指定编号修改:":// 修改
                    #region 修改数据
                    if (com1.Text.Trim() == "") { MessageBox.Show("请选择编号进行修改"); return; }
                    miss = 0;
                    for (int i = 1; i < 3; i++)
                    {
                        if (panel5.Controls["com" + i].Text == "")
                            miss++;
                    }
                    if (miss > 0) { MessageBox.Show("信息不完整，请重试。2"); return; }

                    // 添加要修改的数据
                    dic.Clear();
                    dic.Add("Name", com2.Text);
                    dic.Add("ofProfession", com3.Text);

                    // 添加条件
                    Sourcedic.Clear();
                    Sourcedic.Add("ID", com1.Text);

                    // 所属专业可为空
                    if (com3.Text == string.Empty)
                        dic.Remove("ofProfession");

                    if (MySqlDB.ParMySqlIDUCode("tb_lesson", dic, Sourcedic) > 0)// 判断SQL语句是否输入正确
                    {
                        MySqlDB.BindingDisplay(dataGridView1, "tb_lesson", bindingNavigator1);
                        // 修改完毕退出
                        MessageBox.Show("成功修改一条数据。");
                        com1.Items.Clear();
                    }
                    else
                    {
                        MessageBox.Show("数据修改失败，请重试。");
                        return;
                    }

                    #endregion
                    break;
            }
        }
        private void toolStripButton16_Click(object sender, EventArgs e)// 清空数据表
        {
            if (dataGridView1.Rows.Count == 0)
                MessageBox.Show("该数据表没有数据，请添加数据。");
            else
            {
                if (MessageBox.Show("确认清空该数据表吗？", "警告", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    try
                    {
                        if (MySqlDB.ImportDataToMySql("DELETE FROM `tb_lesson` WHERE 1 ") > 0)
                        {
                            DAL.MySqlDBTool.MySqlCodeByDT(this.sql);// 清空数据表
                            MySqlDB.BindingDisplay(dataGridView1, "tb_lesson", bindingNavigator1);
                            MessageBox.Show("数据表已清空。");
                            com1.Items.Clear();
                        }
                    }
                    catch (Exception et)
                    {
                        MessageBox.Show(et.Message);
                    }
                }
            }
        }
        #endregion

        #region 院系管理[读取数据下拉状态]
        private void com4_DropDown(object sender, EventArgs e)// 编号
        {
            int MaxCount = 0;
            switch (label2.Text)
            {
                case "填写信息添加:":// 添加
                    ((ComboBox)sender).Items.Clear();
                    MaxCount = dataGridView2.Rows.Count;
                    if (MaxCount > 0)
                    {
                        Model.ExamInfo.MySqlInsIndex = ((long)dataGridView2.Rows[MaxCount - 1].Cells[0].Value) + 1L;
                        ((ComboBox)sender).Items.Add(Model.ExamInfo.MySqlInsIndex);
                    }
                    else
                    {
                        Model.ExamInfo.MySqlInsIndex = 1;
                        ((ComboBox)sender).Items.Add(1);
                    }
                    ((ComboBox)sender).SelectedIndex = 0;

                    break;
                case "按条件查询:":
                    // 读取编号
                    ((ComboBox)sender).Items.Clear();
                    ((ComboBox)sender).Items.Add("（全部）");
                    ((ComboBox)sender).SelectedIndex = 0;
                    break;
                default:// 其他
                    // 读取编号
                    ((ComboBox)sender).Items.Clear();
                    foreach (DataGridViewRow item in dataGridView2.Rows)
                        ((ComboBox)sender).Items.Add(item.Cells[0].Value);
                    break;
            }
        }

        private void com5_DropDown(object sender, EventArgs e)// 名称
        {
            switch (label2.Text)
            {
                case "按条件查询:":
                    ((ComboBox)sender).Items.Clear();
                    ((ComboBox)sender).Items.Add("（全部）");
                    foreach (DataGridViewRow item in dataGridView2.Rows)
                        ((ComboBox)sender).Items.Add(item.Cells[1].Value);
                    break;
                case "填写信息添加:":// 添加

                    break;
                case "按指定编号删除:":// 删除

                    break;
                case "按指定编号修改:":// 修改

                    break;
            }
        }
        #endregion
        #region 院系管理[读取数据提交状态]
        private void com4_SelectionChangeCommitted(object sender, EventArgs e)
        {
            switch (label2.Text)
            {
                case "按条件查询:":// 查询
                    break;

                case "填写信息添加:":// 添加
                    break;
                case "按指定编号删除:":// 删除

                    break;
                case "按指定编号修改:":// 修改
                    int RowsIndex = 0;
                    int.TryParse(com4.Text, out RowsIndex);
                    com5.Items.Clear();
                    com5.Items.Add(dataGridView2.Rows[RowsIndex - 1].Cells[1].Value.ToString());
                    com5.SelectedIndex = 0;
                    break;
            }
        }
        #endregion
        #region 院系管理[增删改查]
        private void btn2_Click(object sender, EventArgs e)
        {
            Stopwatch sw = new Stopwatch();
            int miss = 0;
            int MaxCount = 0;
            // 实例化添加要查询的条件
            Dictionary<string, string> dic = new Dictionary<string, string>();
            Dictionary<string, string> Sourcedic = new Dictionary<string, string>();
            StringBuilder sb = new StringBuilder();
            switch (label2.Text)
            {
                case "按条件查询:":// 查询
                    if (dataGridView2.Rows.Count == 0) { MessageBox.Show("数据表内没有数据，请添加。"); return; }
                    if (com4.Text == "（全部）" && com5.Text == "（全部）")
                    {
                        sw.Restart();
                        this.sql = "SELECT * FROM `tb_department`";
                        dtForDepartment = DAL.MySqlDBTool.MySqlCodeByDT(this.sql);
                        Get.GridViewDataLoad(dtForDepartment, dataGridView2, bindingNavigator2);// 填充dataSource
                        sw.Stop();
                        toolStripLabel3.Text = string.Format("执行此查询操作耗时： {0} 秒", sw.Elapsed.TotalSeconds);
                        return;
                    }

                    dic.Clear();
                    int Sourceindex = 0;
                    // 添加要查询的条件
                    for (int i = 4; i < 6; i++)
                    {
                        if (panel6.Controls["com" + i].Text != "（全部）" && panel6.Controls["com" + i].Text != "")
                        {
                            dic.Add(Loaddatadepartment[i - 4], panel6.Controls["com" + i].Text.Trim());
                            Sourceindex = i;
                            break;
                        }
                    }
                    for (int i = 4; i < 6; i++)
                    {
                        if (i == Sourceindex) continue;
                        if (panel6.Controls["com" + i].Text != "（全部）" && panel6.Controls["com" + i].Text != "")
                            dic.Add(Loaddatadepartment[i - 4], panel6.Controls["com" + i].Text.Trim());
                    }
                    dataGridView2.DataSource = null;
                    try
                    {
                        sw.Restart();
                        this.sql = "SELECT * FROM `tb_department`";
                        dtForDepartment = DAL.MySqlDBTool.MySqlCodeByDT(this.sql);
                        dataGridView2.DataSource = SelectToDataTable(dic, dtForDepartment, bindingNavigator2);
                        sw.Stop();
                        toolStripLabel3.Text = string.Format("执行此查询操作耗时： {0} 秒", sw.Elapsed.TotalSeconds);

                    }
                    catch
                    {
                        MessageBox.Show("查询为空");

                        MySqlDB.BindingDisplay(dataGridView2, "tb_department", bindingNavigator2);
                    }
                    break;

                case "填写信息添加:":// 添加
                    #region 添加数据
                    miss = 0;
                    for (int i = 4; i < 6; i++)
                    {
                        if (panel6.Controls["com" + i].Text == "")
                            miss++;
                    }
                    if (miss > 0) { MessageBox.Show("信息不完整，请重试。3"); return; }

                    MaxCount = dataGridView2.Rows.Count;
                    if (MaxCount > 0)
                        Model.ExamInfo.MySqlInsIndex = ((long)dataGridView2.Rows[MaxCount - 1].Cells[0].Value) + 1L;
                    else
                        Model.ExamInfo.MySqlInsIndex = 1;

                    // 添加要插入的数据
                    dic.Clear();
                    dic.Add("ID", Model.ExamInfo.MySqlInsIndex.ToString());
                    dic.Add("Name", com5.Text);

                    if (MySqlDB.ParMySqlIDUCode("tb_department", dic) > 0)// 判断SQL语句是否输入正确
                    {
                        MySqlDB.BindingDisplay(dataGridView2, "tb_department", bindingNavigator2);
                        MessageBox.Show("成功录入一条数据");
                    }
                    else
                    {
                        MessageBox.Show("数据添加失败，请重试。");
                        return;
                    }
                    int NextIndex = 0;
                    int.TryParse(com4.Text, out NextIndex);
                    com4.Items.Clear();
                    com4.Items.Add(NextIndex + 1);
                    com4.SelectedIndex = 0;
                    #endregion
                    break;
                case "按指定编号删除:":// 删除
                    MaxCount = dataGridView2.Rows.Count;
                    if (MaxCount > 0)
                        Model.ExamInfo.MySqlInsIndex = ((long)dataGridView2.Rows[MaxCount - 1].Cells[0].Value) + 1L;
                    else
                        Model.ExamInfo.MySqlInsIndex = 1;

                    int RemoveIndex = 0;
                    int.TryParse(com4.Text, out RemoveIndex);

                    if (com4.Text.Trim() == "") { MessageBox.Show("请选择编号进行删除"); return; }
                    if (MessageBox.Show("确认删除编号为 " + com4.Text + " 的数据吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2) != DialogResult.OK)
                        return;

                    string sql = string.Format(@"DELETE FROM `{0}` WHERE `ID` = {1} ", "tb_department", RemoveIndex);
                    if (MySqlDB.ImportDataToMySql(sql) > 0)
                    {
                        foreach (DataGridViewRow item in dataGridView2.Rows)
                            if (item.Cells[0].Value.ToString() == com4.Text) { dataGridView2.Rows.Remove(item); break; }

                        if (Model.ExamInfo.MySqlInsIndex == (long)RemoveIndex)// 刷新数据表
                        {
                            MySqlDB.BindingDisplay(dataGridView2, "tb_department", bindingNavigator2);
                            return;
                        }

                        // 删除ID为n，将 n + 1 的ID值 - 1
                        dic.Clear();
                        dic.Add("ID", "ID-1");
                        string cusapp = @" ID BETWEEN " + RemoveIndex + @" AND " + Model.ExamInfo.MySqlInsIndex + " ORDER BY " + dic.Keys.ToList()[0] + " ASC";

                        MySqlDB.CusMySqlIDUCode("tb_department", cusapp, dic);
                        MySqlDB.BindingDisplay(dataGridView2, "tb_department", bindingNavigator2);
                    }
                    else { MessageBox.Show("数据删除失败，请重试"); return; }
                    com4.Items.Clear();
                    break;
                case "按指定编号修改:":// 修改
                    #region 修改数据
                    if (com4.Text.Trim() == "") { MessageBox.Show("请选择编号进行修改"); return; }
                    miss = 0;
                    for (int i = 4; i < 6; i++)
                    {
                        if (panel6.Controls["com" + i].Text == "")
                            miss++;
                    }
                    if (miss > 0) { MessageBox.Show("信息不完整，请重试。4"); return; }

                    // 添加要修改的数据
                    dic.Clear();
                    dic.Add("Name", com5.Text);

                    // 添加条件
                    Sourcedic.Clear();
                    Sourcedic.Add("ID", com4.Text);

                    if (MySqlDB.ParMySqlIDUCode("tb_department", dic, Sourcedic) > 0)// 判断SQL语句是否输入正确
                    {
                        MySqlDB.BindingDisplay(dataGridView2, "tb_department", bindingNavigator2);
                        // 修改完毕退出
                        MessageBox.Show("成功修改一条数据。");
                        com4.Items.Clear();
                    }
                    else
                    {
                        MessageBox.Show("数据修改失败，请重试。");
                        return;
                    }

                    #endregion
                    break;
            }
        }
        private void toolStripButton15_Click(object sender, EventArgs e)// 清空数据表
        {
            if (dataGridView2.Rows.Count == 0)
                MessageBox.Show("该数据表没有数据，请添加数据。");
            else
            {
                if (MessageBox.Show("确认清空该数据表吗？", "警告", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    try
                    {
                        if (MySqlDB.ImportDataToMySql("DELETE FROM `tb_department` WHERE 1 ") > 0)
                        {
                            DAL.MySqlDBTool.MySqlCodeByDT(this.sql);// 清空数据表
                            MySqlDB.BindingDisplay(dataGridView2, "tb_department", bindingNavigator2);
                            MessageBox.Show("数据表已清空。");
                            com4.Items.Clear();
                        }
                    }
                    catch (Exception et)
                    {
                        MessageBox.Show(et.Message);
                    }
                }
            }
        }
        #endregion

        #region 专业管理[读取数据下拉状态]
        private void comboBox1_DropDown(object sender, EventArgs e)// 编号
        {

            int MaxCount = 0;
            switch (label5.Text)
            {
                case "填写信息添加:":// 添加
                    ((ComboBox)sender).Items.Clear();
                    MaxCount = dataGridView3.Rows.Count;
                    if (MaxCount > 0)
                    {
                        Model.ExamInfo.MySqlInsIndex = ((long)dataGridView3.Rows[MaxCount - 1].Cells[0].Value) + 1L;
                        ((ComboBox)sender).Items.Add(Model.ExamInfo.MySqlInsIndex);
                    }
                    else
                    {
                        Model.ExamInfo.MySqlInsIndex = 1;
                        ((ComboBox)sender).Items.Add(1);
                    }
                    ((ComboBox)sender).SelectedIndex = 0;

                    break;
                case "按条件查询:":
                    // 读取编号
                    ((ComboBox)sender).Items.Clear();
                    ((ComboBox)sender).Items.Add("（全部）");
                    ((ComboBox)sender).SelectedIndex = 0;
                    break;
                default:// 其他
                    // 读取编号
                    ((ComboBox)sender).Items.Clear();
                    foreach (DataGridViewRow item in dataGridView3.Rows)
                        ((ComboBox)sender).Items.Add(item.Cells[0].Value);
                    break;
            }
        }

        private void comboBox2_DropDown(object sender, EventArgs e)// 名称
        {

            switch (label5.Text)
            {
                case "按条件查询:":
                    ((ComboBox)sender).Items.Clear();
                    ((ComboBox)sender).Items.Add("（全部）");
                    foreach (DataGridViewRow item in dataGridView3.Rows)
                        ((ComboBox)sender).Items.Add(item.Cells[1].Value);
                    break;
                case "填写信息添加:":// 添加

                    break;
                case "按指定编号删除:":// 删除

                    break;
                case "按指定编号修改:":// 修改

                    break;
            }
        }

        private void comboBox3_DropDown(object sender, EventArgs e)// 院系
        {
            List<string> lst = new List<string>();
            List<string> Newlst = null;
            switch (label5.Text)
            {
                case "按条件查询:":// 查询
                    ((ComboBox)sender).Items.Clear();
                    lst.Add("（全部）");
                    for (int i = 0; i < dataGridView3.Rows.Count; i++)
                    {
                        if (dataGridView3.Rows[i].Cells[2].Value.ToString().Trim() == string.Empty)
                            continue;
                        lst.Add(dataGridView3.Rows[i].Cells[2].Value.ToString().Trim());
                    }

                    // 去重
                    Newlst = lst.Distinct().ToList();

                    // 添加数据
                    foreach (var item in Newlst)
                    {
                        ((ComboBox)sender).Items.Add(item);
                    }
                    break;

                case "填写信息添加:":// 添加
                    ((ComboBox)sender).Items.Clear();
                    // 读取院系
                    for (int i = 0; i < dataGridView2.Rows.Count; i++)
                    {
                        if (dataGridView2.Rows[i].Cells[1].Value.ToString().Trim() == string.Empty)
                            continue;
                        lst.Add(dataGridView2.Rows[i].Cells[1].Value.ToString().Trim());
                    }

                    // 去重
                    Newlst = lst.Distinct().ToList();

                    // 添加数据
                    foreach (var item in Newlst)
                    {
                        ((ComboBox)sender).Items.Add(item);
                    }
                    break;
                case "按指定编号删除:":// 删除
                    ((ComboBox)sender).Items.Clear();
                    break;
                case "按指定编号修改:":// 修改
                    ((ComboBox)sender).Items.Clear();
                    // 读取院系
                    for (int i = 0; i < dataGridView2.Rows.Count; i++)
                    {
                        if (dataGridView2.Rows[i].Cells[1].Value.ToString().Trim() == string.Empty)
                            continue;
                        lst.Add(dataGridView2.Rows[i].Cells[1].Value.ToString().Trim());
                    }

                    // 去重
                    Newlst = lst.Distinct().ToList();

                    // 添加数据
                    foreach (var item in Newlst)
                    {
                        ((ComboBox)sender).Items.Add(item);
                    }
                    break;
            }
        }

        #endregion
        #region 专业管理[读取数据提交状态]
        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {

            switch (label5.Text)
            {
                case "按条件查询:":// 查询
                    break;

                case "填写信息添加:":// 添加
                    break;
                case "按指定编号删除:":// 删除

                    break;
                case "按指定编号修改:":// 修改
                    int RowsIndex = 0;
                    int.TryParse(comboBox1.Text, out RowsIndex);
                    comboBox2.Items.Clear();
                    comboBox2.Text = dataGridView3.Rows[RowsIndex - 1].Cells[1].Value.ToString();
                    comboBox3.Items.Clear();
                    comboBox3.Items.Add(dataGridView3.Rows[RowsIndex - 1].Cells[2].Value.ToString());
                    comboBox3.SelectedIndex = 0;
                    break;
            }
        }
        #endregion
        #region 专业管理[增删改查]
        private void button3_Click(object sender, EventArgs e)
        {

            Stopwatch sw = new Stopwatch();
            int miss = 0;
            int MaxCount = 0;
            // 实例化添加要查询的条件
            Dictionary<string, string> dic = new Dictionary<string, string>();
            Dictionary<string, string> Sourcedic = new Dictionary<string, string>();
            StringBuilder sb = new StringBuilder();
            switch (label5.Text)
            {
                case "按条件查询:":// 查询
                    if (dataGridView3.Rows.Count == 0) { MessageBox.Show("数据表内没有数据，请添加。"); return; }
                    if (comboBox1.Text == "（全部）" && comboBox2.Text == "（全部）" && comboBox3.Text == "（全部）")
                    {
                        sw.Restart();
                        this.sql = "SELECT * FROM `tb_profession`";
                        dtForProfession = DAL.MySqlDBTool.MySqlCodeByDT(this.sql);
                        Get.GridViewDataLoad(dtForProfession, dataGridView3, bindingNavigator3);// 填充dataSource
                        sw.Stop();
                        toolStripLabel5.Text = string.Format("执行此查询操作耗时： {0} 秒", sw.Elapsed.TotalSeconds);
                        return;
                    }

                    dic.Clear();
                    int Sourceindex = 0;
                    // 添加要查询的条件
                    for (int i = 1; i < 4; i++)
                    {
                        if (panel7.Controls["comboBox" + i].Text != "（全部）" && panel7.Controls["comboBox" + i].Text != "")
                        {
                            dic.Add(Loaddatalesson[i - 1], panel7.Controls["comboBox" + i].Text.Trim());
                            Sourceindex = i;
                            break;
                        }
                    }
                    for (int i = 1; i < 4; i++)
                    {
                        if (i == Sourceindex) continue;
                        if (panel7.Controls["comboBox" + i].Text != "（全部）" && panel7.Controls["comboBox" + i].Text != "")
                            dic.Add(Loaddatalesson[i - 1], panel7.Controls["comboBox" + i].Text.Trim());
                    }
                    dataGridView3.DataSource = null;
                    try
                    {
                        sw.Restart();
                        this.sql = "SELECT * FROM `tb_profession`";
                        dtForProfession = DAL.MySqlDBTool.MySqlCodeByDT(this.sql);
                        dataGridView3.DataSource = SelectToDataTable(dic, dtForProfession, bindingNavigator3);
                        sw.Stop();
                        toolStripLabel5.Text = string.Format("执行此查询操作耗时： {0} 秒", sw.Elapsed.TotalSeconds);

                    }
                    catch
                    {
                        MessageBox.Show("查询为空");

                        MySqlDB.BindingDisplay(dataGridView3, "tb_profession", bindingNavigator3);
                    }
                    break;

                case "填写信息添加:":// 添加
                    #region 添加数据
                    miss = 0;
                    for (int i = 1; i < 3; i++)
                    {
                        if (panel7.Controls["comboBox" + i].Text == "")
                            miss++;
                    }
                    if (miss > 0) { MessageBox.Show("信息不完整，请重试。5"); return; }

                    MaxCount = dataGridView3.Rows.Count;
                    if (MaxCount > 0)
                        Model.ExamInfo.MySqlInsIndex = ((long)dataGridView3.Rows[MaxCount - 1].Cells[0].Value) + 1L;
                    else
                        Model.ExamInfo.MySqlInsIndex = 1;

                    // 添加要插入的数据
                    dic.Clear();
                    dic.Add("ID", Model.ExamInfo.MySqlInsIndex.ToString());
                    dic.Add("Name", comboBox2.Text);
                    dic.Add("ofDepartment", comboBox3.Text);

                    // 所属院系可为空
                    if (comboBox3.Text == string.Empty)
                        dic.Remove("ofDepartment");

                    if (MySqlDB.ParMySqlIDUCode("tb_profession", dic) > 0)// 判断SQL语句是否输入正确
                    {
                        MySqlDB.BindingDisplay(dataGridView3, "tb_profession", bindingNavigator3);
                        MessageBox.Show("成功录入一条数据");
                    }
                    else
                    {
                        MessageBox.Show("数据添加失败，请重试。");
                        return;
                    }
                    int NextIndex = 0;
                    int.TryParse(comboBox1.Text, out NextIndex);
                    comboBox1.Items.Clear();
                    comboBox1.Items.Add(NextIndex + 1);
                    comboBox1.SelectedIndex = 0;
                    #endregion
                    break;
                case "按指定编号删除:":// 删除
                    MaxCount = dataGridView3.Rows.Count;
                    if (MaxCount > 0)
                        Model.ExamInfo.MySqlInsIndex = ((long)dataGridView3.Rows[MaxCount - 1].Cells[0].Value) + 1L;
                    else
                        Model.ExamInfo.MySqlInsIndex = 1;

                    int RemoveIndex = 0;
                    int.TryParse(comboBox1.Text, out RemoveIndex);

                    if (comboBox1.Text.Trim() == "") { MessageBox.Show("请选择编号进行删除"); return; }
                    if (MessageBox.Show("确认删除编号为 " + comboBox1.Text + " 的数据吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2) != DialogResult.OK)
                        return;

                    string sql = string.Format(@"DELETE FROM `{0}` WHERE `ID` = {1} ", "tb_profession", RemoveIndex);
                    if (MySqlDB.ImportDataToMySql(sql) > 0)
                    {
                        foreach (DataGridViewRow item in dataGridView3.Rows)
                            if (item.Cells[0].Value.ToString() == comboBox1.Text) { dataGridView3.Rows.Remove(item); break; }

                        if (Model.ExamInfo.MySqlInsIndex == (long)RemoveIndex)// 刷新数据表
                        {
                            MySqlDB.BindingDisplay(dataGridView3, "tb_profession", bindingNavigator3);
                            return;
                        }

                        // 删除ID为n，将 n + 1 的ID值 - 1
                        dic.Clear();
                        dic.Add("ID", "ID-1");
                        string cusapp = @" ID BETWEEN " + RemoveIndex + @" AND " + Model.ExamInfo.MySqlInsIndex + " ORDER BY " + dic.Keys.ToList()[0] + " ASC";

                        MySqlDB.CusMySqlIDUCode("tb_profession", cusapp, dic);
                        MySqlDB.BindingDisplay(dataGridView3, "tb_profession", bindingNavigator3);
                    }
                    else { MessageBox.Show("数据删除失败，请重试"); return; }
                    comboBox1.Items.Clear();
                    break;
                case "按指定编号修改:":// 修改
                    #region 修改数据
                    if (comboBox1.Text.Trim() == "") { MessageBox.Show("请选择编号进行修改"); return; }
                    miss = 0;
                    for (int i = 1; i < 3; i++)
                    {
                        if (panel7.Controls["comboBox" + i].Text == "")
                            miss++;
                    }
                    if (miss > 0) { MessageBox.Show("信息不完整，请重试。6"); return; }

                    // 添加要修改的数据
                    dic.Clear();
                    dic.Add("Name", comboBox2.Text);
                    dic.Add("ofDepartment", comboBox3.Text);

                    // 添加条件
                    Sourcedic.Clear();
                    Sourcedic.Add("ID", comboBox1.Text);

                    // 所属院系可为空
                    if (comboBox3.Text == string.Empty)
                        dic.Remove("ofDepartment");

                    if (MySqlDB.ParMySqlIDUCode("tb_profession", dic, Sourcedic) > 0)// 判断SQL语句是否输入正确
                    {
                        MySqlDB.BindingDisplay(dataGridView3, "tb_profession", bindingNavigator3);
                        // 修改完毕退出
                        MessageBox.Show("成功修改一条数据。");
                        comboBox1.Items.Clear();
                    }
                    else
                    {
                        MessageBox.Show("数据修改失败，请重试。");
                        return;
                    }

                    #endregion
                    break;
            }
        }
        private void toolStripButton14_Click(object sender, EventArgs e)// 清空数据表
        {
            if (dataGridView3.Rows.Count == 0)
                MessageBox.Show("该数据表没有数据，请添加数据。");
            else
            {
                if (MessageBox.Show("确认清空该数据表吗？", "警告", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    try
                    {
                        if (MySqlDB.ImportDataToMySql("DELETE FROM `tb_profession` WHERE 1 ") > 0)
                        {
                            DAL.MySqlDBTool.MySqlCodeByDT(this.sql);// 清空数据表
                            MySqlDB.BindingDisplay(dataGridView3, "tb_profession", bindingNavigator3);
                            MessageBox.Show("数据表已清空。");
                            comboBox1.Items.Clear();
                        }
                    }
                    catch (Exception et)
                    {
                        MessageBox.Show(et.Message);
                    }
                }
            }

        }




        #endregion
        #region 同步数据
        private void toolStripButton1_Click_1(object sender, EventArgs e)// 专业
        {

        }

        private void toolStripButton13_Click(object sender, EventArgs e)// 院系
        {

        }
        #endregion
    }
}
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
using System.Reflection;

namespace UI
{
    public partial class ExamineeManFrm : Form
    {
        #region 变量
        /// <summary>
        /// 实例化考生管理的方法组对象
        /// </summary>
        ExamineeManMethod EMFMethod = new ExamineeManMethod("tb_stumanage");

        /// <summary>
        /// 实例MySql工具
        /// </summary>
        DAL.MySqlDBTool MySqlDB = new DAL.MySqlDBTool();

        /// <summary>
        /// 原考生信息表
        /// </summary>
        public DataTable ksdt;

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

        /// <summary>
        /// dgv控件添加筛选功能
        /// </summary>
        DataGridViewFunction Get = new DataGridViewFunction();

        public static ExamineeManFrm EMF;// 当前窗体，用于传递
        #endregion
        #region 构造函数
        public ExamineeManFrm(ExamMainTea mm)
        {
            InitializeComponent();
            m1 = mm;
            string sql = "select * from `tb_stumanage`";
            ksdt = DAL.MySqlDBTool.MySqlCodeByDT(sql);// 实例化Table表，可进行筛选数据
            EMF = this;
        }
        #endregion

        #region 加载窗体事件
        /// <summary>
        /// 窗体加载事件-并加载数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExamineeManFrm_Load(object sender, EventArgs e)
        {
            #region 初始状态
            ExamMainTea.EMTFrm.BottomSidebar.Visible = true;
            KEY.ExamineeManFrmkey = "1";
            EMFMethod.DGVStyleOfkaosheng(dataSource);// 考生列标题
            EMFMethod.DGVStyleTofajuan(dataGridView1);// 发卷列标题
            for (int i = 1; i < 5; i++)
            {
                ((ComboBox)splitContainer2.Panel1.Controls["comboBox" + i]).Items.Add("（全部）");
                ((ComboBox)splitContainer2.Panel1.Controls["comboBox" + i]).SelectedItem = "（全部）";
            }

            #endregion

            #region dgv控件实现筛选功能
            Get.GridViewDataLoad(ksdt, dataSource, bindingNavigator1);// 填充dataSource
            sw.Start();
            Get.GridViewHeaderFilter(dataSource);// 标题添加ComBox并返回行数
            sw.Stop();
            toolStripLabel1.Text = string.Format("执行此查询操作耗时： {0} 秒", sw.Elapsed.TotalSeconds);

            #endregion
        }

        private void dataSource_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            Get.GridViewDataCount(dataSource);//筛选时动态加载行数
        }

        private void ExamineeManFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            KEY.ExamineeManFrmkey = "";
        }

        private void ExamineeManFrm_Enter(object sender, EventArgs e)
        {
            if (dataSource.Rows.Count == 0)
                MessageBox.Show("数据库没有数据，请点击 Excel导入名单 添加。");
        }

        #endregion
        #region 窗体激活事件
        private void ExamineeManFrm_Activated(object sender, EventArgs e)
        {
            if (Tag.ToString() == "1")// 不是发卷状态
            {
                splitContainer1.Panel2Collapsed = true;
                splitContainer2.Panel2Collapsed = true;
            }
            else// 发卷状态
            {
                splitContainer1.Panel2Collapsed = false;
                splitContainer2.Panel2Collapsed = false;
                ExamMainTea.EMTFrm.PreviousStep.Text = "上一步";
                ExamMainTea.EMTFrm.NextStep.Text = "下一步";
                #region 取消上一次操作添加的事件
                string[] prevstep = BLL.Method.GetBindingMethod(ExamMainTea.EMTFrm.PreviousStep, "Click");
                string[] nextstep = BLL.Method.GetBindingMethod(ExamMainTea.EMTFrm.NextStep, "Click");
                if (prevstep != null)
                {
                    for (int i = 0; i < prevstep.Count(); i++)
                    {
                        if (prevstep[i] == "Previous_fajuan") ExamMainTea.EMTFrm.PreviousStep.Click -= fajuanFrm.fF.Previous_fajuan;
                        if (prevstep[i] == "Previous_kaosheng") ExamMainTea.EMTFrm.PreviousStep.Click -= ExamineeManFrm.EMF.Previous_kaosheng;
                        if (prevstep[i] == "Previous_setting") ExamMainTea.EMTFrm.PreviousStep.Click - = SettingFrm.SF.Previous_setting;
                    }
                }
                if (nextstep != null)
                {
                    for (int i = 0; i < nextstep.Count(); i++)
                    {
                        if (nextstep[i] == "Next_fajuan") ExamMainTea.EMTFrm.NextStep.Click -= fajuanFrm.fF.Next_fajuan;
                        if (nextstep[i] == "Next_kaosheng") ExamMainTea.EMTFrm.NextStep.Click -= ExamineeManFrm.EMF.Next_kaosheng;
                        if (nextstep[i] == "Next_setting") ExamMainTea.EMTFrm.NextStep.Click - = SettingFrm.SF.Next_setting;
                    }
                }
                #endregion
                ExamMainTea.EMTFrm.PreviousStep.Click += Previous_kaosheng;// 单击事件
                ExamMainTea.EMTFrm.NextStep.Click += Next_kaosheng;// 单击事件
            }
        }
        #endregion
        #region 上一步
        public void Previous_kaosheng(object sender, EventArgs e)
        {
            Login.BLL.TeaManager.ActiveFrm(MainFrm.fF);
        }
        #endregion
        #region 下一步
        public void Next_kaosheng(object sender, EventArgs e)
        {
            // 判断通过的条件
            if (dataGridView1.Rows.Count == 0) { MessageBox.Show("发卷名单为空"); return; };
            if (dataGridView1.Rows[0].Cells[7].Value.ToString() == "") { MessageBox.Show("请为发卷名单分配AB卷"); return; }

            if (BLL.KEY.SettingFrmkey != "1")// 设置窗体关闭状态
            {
                ExamMainTea.SF = new SettingFrm((ExamMainTea)ActiveForm);
                ExamMainTea.SF.MdiParent = ActiveForm;  // 使父窗体成为子窗体的MDI容器
                ExamMainTea.SF.Show();
                ExamMainTea.SF.WindowState = FormWindowState.Maximized;
            }
            else// 激活窗体
            {
                Login.BLL.TeaManager.ActiveFrm(ExamMainTea.SF);
            }
            ExamMainTea.EMTFrm.checkBox2.Checked = true;// 考生通过
            ExamMainTea.EMTFrm.checkBox3.Checked = true;// 组卷通过
            SettingFrm.KSNextToSet(BLL.Method.GetDgvToTable(dataGridView1));// 传值
        }
        #endregion

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

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)// 院系
        {
            if (comboBox1.Text == "（全部）" && comboBox2.Text == "（全部）" && comboBox3.Text == "（全部）" && comboBox4.Text == "（全部）")
            {
                sw.Restart();
                Get.GridViewDataLoad(ksdt, dataSource, bindingNavigator1);// 填充dataSource
                sw.Stop();
                toolStripLabel1.Text = string.Format("执行此查询操作耗时： {0} 秒", sw.Elapsed.TotalSeconds);
                return;
            }

            // 实例化添加要查询的条件
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Clear();
            int Sourceindex = 0;
            // 添加要查询的条件
            for (int i = 1; i < 5; i++)
            {
                if (splitContainer2.Panel1.Controls["comboBox" + i].Text != "（全部）" && splitContainer2.Panel1.Controls["comboBox" + i].Text != "")
                {
                    dic.Add(Loaddata[i - 1], splitContainer2.Panel1.Controls["comboBox" + i].Text);
                    Sourceindex = i;
                    break;
                }
            }
            for (int i = 1; i < 5; i++)
            {
                if (i == Sourceindex) continue;
                if (splitContainer2.Panel1.Controls["comboBox" + i].Text != "（全部）" && splitContainer2.Panel1.Controls["comboBox" + i].Text != "")
                    dic.Add(Loaddata[i - 1], splitContainer2.Panel1.Controls["comboBox" + i].Text.Trim());
            }
            dataSource.DataSource = null;
            dataSource.DataSource = ExamineeManMethod.SelectToDataTable(dic, ksdt, sw, toolStripLabel1, bindingNavigator1);
        }

        private void comboBox2_SelectionChangeCommitted(object sender, EventArgs e)// 年级
        {
            if (comboBox1.Text == "（全部）" && comboBox2.Text == "（全部）" && comboBox3.Text == "（全部）" && comboBox4.Text == "（全部）")
            {
                sw.Restart();
                Get.GridViewDataLoad(ksdt, dataSource, bindingNavigator1);// 填充dataSource
                sw.Stop();
                toolStripLabel1.Text = string.Format("执行此查询操作耗时： {0} 秒", sw.Elapsed.TotalSeconds);
                return;
            }

            // 实例化添加要查询的条件
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Clear();
            int Sourceindex = 0;
            // 添加要查询的条件
            for (int i = 1; i < 5; i++)
            {
                if (splitContainer2.Panel1.Controls["comboBox" + i].Text != "（全部）" && splitContainer2.Panel1.Controls["comboBox" + i].Text != "")
                {
                    dic.Add(Loaddata[i - 1], splitContainer2.Panel1.Controls["comboBox" + i].Text);
                    Sourceindex = i;
                    break;
                }
            }
            for (int i = 1; i < 5; i++)
            {
                if (i == Sourceindex) continue;
                if (splitContainer2.Panel1.Controls["comboBox" + i].Text != "（全部）" && splitContainer2.Panel1.Controls["comboBox" + i].Text != "")
                    dic.Add(Loaddata[i - 1], splitContainer2.Panel1.Controls["comboBox" + i].Text.Trim());
            }
            dataSource.DataSource = null;
            dataSource.DataSource = ExamineeManMethod.SelectToDataTable(dic, ksdt, sw, toolStripLabel1, bindingNavigator1);
        }

        private void comboBox3_SelectionChangeCommitted(object sender, EventArgs e)// 专业
        {
            if (comboBox1.Text == "（全部）" && comboBox2.Text == "（全部）" && comboBox3.Text == "（全部）" && comboBox4.Text == "（全部）")
            {
                sw.Restart();
                Get.GridViewDataLoad(ksdt, dataSource, bindingNavigator1);// 填充dataSource
                sw.Stop();
                toolStripLabel1.Text = string.Format("执行此查询操作耗时： {0} 秒", sw.Elapsed.TotalSeconds);
                return;
            }

            // 实例化添加要查询的条件
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Clear();
            int Sourceindex = 0;
            // 添加要查询的条件
            for (int i = 1; i < 5; i++)
            {
                if (splitContainer2.Panel1.Controls["comboBox" + i].Text != "（全部）" && splitContainer2.Panel1.Controls["comboBox" + i].Text != "")
                {
                    dic.Add(Loaddata[i - 1], splitContainer2.Panel1.Controls["comboBox" + i].Text);
                    Sourceindex = i;
                    break;
                }
            }
            for (int i = 1; i < 5; i++)
            {
                if (i == Sourceindex) continue;
                if (splitContainer2.Panel1.Controls["comboBox" + i].Text != "（全部）" && splitContainer2.Panel1.Controls["comboBox" + i].Text != "")
                    dic.Add(Loaddata[i - 1], splitContainer2.Panel1.Controls["comboBox" + i].Text.Trim());
            }
            dataSource.DataSource = null;
            dataSource.DataSource = ExamineeManMethod.SelectToDataTable(dic, ksdt, sw, toolStripLabel1, bindingNavigator1);
        }

        private void comboBox4_SelectionChangeCommitted(object sender, EventArgs e)// 班级
        {
            if (comboBox1.Text == "（全部）" && comboBox2.Text == "（全部）" && comboBox3.Text == "（全部）" && comboBox4.Text == "（全部）")
            {
                sw.Restart();
                Get.GridViewDataLoad(ksdt, dataSource, bindingNavigator1);// 填充dataSource
                sw.Stop();
                toolStripLabel1.Text = string.Format("执行此查询操作耗时： {0} 秒", sw.Elapsed.TotalSeconds);
                return;
            }

            // 实例化添加要查询的条件
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Clear();
            int Sourceindex = 0;
            // 添加要查询的条件
            for (int i = 1; i < 5; i++)
            {
                if (splitContainer2.Panel1.Controls["comboBox" + i].Text != "（全部）" && splitContainer2.Panel1.Controls["comboBox" + i].Text != "")
                {
                    dic.Add(Loaddata[i - 1], splitContainer2.Panel1.Controls["comboBox" + i].Text);
                    Sourceindex = i;
                    break;
                }
            }
            for (int i = 1; i < 5; i++)
            {
                if (i == Sourceindex) continue;
                if (splitContainer2.Panel1.Controls["comboBox" + i].Text != "（全部）" && splitContainer2.Panel1.Controls["comboBox" + i].Text != "")
                    dic.Add(Loaddata[i - 1], splitContainer2.Panel1.Controls["comboBox" + i].Text.Trim());
            }
            dataSource.DataSource = null;
            dataSource.DataSource = ExamineeManMethod.SelectToDataTable(dic, ksdt, sw, toolStripLabel1, bindingNavigator1);
        }

        #endregion

        #region  Excel导入数据

        private void btnAddEx_Click(object sender, EventArgs e)
        {
            // 调用Method
            UpdateSUb US = new UpdateSUb();
            US.Tag = "1";
            US.ShowDialog();
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
                US.Tag = "2";
                US.ShowDialog();
            }
        }
        #endregion

        #region 清空数据库
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (dataSource.Rows.Count == 0)
                MessageBox.Show("该数据表没有数据，点击Excel导入添加数据。");
            else
            {
                if (MessageBox.Show("确认清空该数据表吗？", "警告", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    sw.Restart();
                    if (MySqlDB.ImportDataToMySql("DELETE FROM `tb_stumanage` WHERE 1 ") > 0)
                    {
                        string sql = "select * from `tb_stumanage`";
                        ksdt = DAL.MySqlDBTool.MySqlCodeByDT(sql);// 实例化Table表，可进行筛选数据
                        BindingSource bs = new BindingSource(ksdt, null);
                        dataSource.DataSource = bs;
                        sw.Stop();
                        toolStripLabel1.Text = string.Format("执行此查询操作耗时： {0} 秒", sw.Elapsed.TotalSeconds);
                        MessageBox.Show("数据表已清空，点击Excel导入新数据。");
                    }
                }
            }
        }

        #endregion

        #region 批量删除
        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            UpdateSUb US = new UpdateSUb();
            US.Tag = "3";
            US.ShowDialog();
        }
        #endregion

        #region ID重新排序
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            sw.Restart();
            MySqlDB.ImportDataToMySql("ALTER TABLE `tb_stumanage`  DROP `ID` ");
            MySqlDB.ImportDataToMySql("ALTER TABLE `tb_stumanage` ADD `ID` BIGINT(8) NOT NULL AUTO_INCREMENT FIRST, ADD PRIMARY KEY(`ID`) ");
            string sql = "select * from `tb_stumanage`";
            DataTable newdt = DAL.MySqlDBTool.MySqlCodeByDT(sql);// 实例化Table表，可进行筛选数据
            BindingSource bs = new BindingSource(newdt, null);
            ExamineeManFrm.EMF.dataSource.DataSource = bs;
            sw.Stop();
            toolStripLabel1.Text = string.Format("执行此查询操作耗时： {0} 秒", sw.Elapsed.TotalSeconds);
        }
        #endregion

        #region 右键批量添加至发卷名单
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (dataSource.CurrentCell == null) return;
            DataTable NewDT = Method.GetDgvToTable(dataGridView1).Clone();
            object[] newrow = new object[7];
            // 遍历所有选中项
            for (int i = 0; i < dataSource.SelectedRows.Count; i++)
            {
                newrow[0] = dataSource.SelectedRows[i].Cells[1].Value;// 学号
                newrow[1] = dataSource.SelectedRows[i].Cells[2].Value;// 姓名
                newrow[2] = dataSource.SelectedRows[i].Cells[5].Value;// 专业
                newrow[3] = dataSource.SelectedRows[i].Cells[7].Value;// 班级名称
                newrow[4] = TaotiDT.Rows[TaotiIndex].ItemArray[3];// 课程
                newrow[5] = TaotiDT.Rows[TaotiIndex].ItemArray[1];// 套题名称
                newrow[6] = TaotiDT.Rows[TaotiIndex].ItemArray[2];// 套题类型
                NewDT.Rows.Add(newrow);
            }
            dataGridView1.DataSource = null;
            Get.GridViewDataLoad(NewDT, dataGridView1, bindingNavigator2);// 填充dataGridView1
            Get.GridViewHeaderFilter(dataGridView1);// 标题添加ComBox并返回行数
            MessageBox.Show(string.Format(@"成功添加 {0} 条数据至发卷名单", dataSource.SelectedRows.Count));
        }

        #endregion
        #region 弹窗批量数据添加至发卷名单
        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            UpdateSUb US = new UpdateSUb();
            US.Tag = "4";
            US.ShowDialog();
        }
        #endregion

        #region 发卷选项_清空
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            while (dataGridView1.Rows.Count > 0)
                dataGridView1.Rows.RemoveAt(0);
        }
        #endregion
        #region 发卷_题库管理[传值入]
        public static DataTable TaotiDT;
        public static int TaotiIndex;
        /// <summary>
        /// 将上一个窗体的值传入此窗体
        /// </summary>
        /// <param name="TaotiID">选中的套题编号</param>
        public static void SubNextToKS(DataTable _dt, int _TaotiIndex)
        {
            TaotiDT = _dt;
            TaotiIndex = _TaotiIndex;
        }
        #endregion
        #region 右键全选
        private void 全选ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataSource.SelectAll();
        }
        #endregion
        #region AB卷下拉框
        private void com1_DropDown(object sender, EventArgs e)// A卷
        {
            ((ComboBox)sender).Items.Clear();
            List<string> lst = new List<string>();
            List<string> Newlst = new List<string>();
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells[3].Value.ToString().Trim() == string.Empty)
                    continue;
                lst.Add(dataGridView1.Rows[i].Cells[3].Value.ToString().Trim());
            }

            // 去重
            Newlst = lst.Distinct().ToList();

            // 添加数据
            foreach (var item in Newlst)
            {
                ((ComboBox)sender).Items.Add(item);
                com2.Items.Add(item);
            }
        }
        private void com2_DropDown(object sender, EventArgs e)// B卷
        {
            ((ComboBox)sender).Items.Clear();
            List<string> lst = new List<string>();
            List<string> Newlst = new List<string>();
            ((ComboBox)sender).Items.Clear();
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells[3].Value.ToString().Trim() == string.Empty)
                    continue;
                lst.Add(dataGridView1.Rows[i].Cells[3].Value.ToString().Trim());
            }

            // 去重
            Newlst = lst.Distinct().ToList();

            // 添加数据
            foreach (var item in Newlst)
            {
                ((ComboBox)sender).Items.Add(item);
                com1.Items.Add(item);
            }
        }
        #endregion
        #region 设置AB卷
        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0) { MessageBox.Show("发卷名单为空，请添加"); return; }
            if (com1.Text == "") { MessageBox.Show("请选择B卷"); return; }
            if (com2.Text == "") { MessageBox.Show("请选择A卷"); return; }
            if (com1.Text == com2.Text) { MessageBox.Show("AB卷分配的班级不能相同。"); return; }
            dataGridView1.DataSource = null;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells[3].Value.ToString() == com1.Text)
                {
                    dataGridView1.Rows[i].Cells[7].Value = com1.Text;
                }
                if (dataGridView1.Rows[i].Cells[3].Value.ToString() == com2.Text)
                {
                    dataGridView1.Rows[i].Cells[7].Value = com2.Text;
                }
            }
            DataTable newdt = BLL.Method.GetDgvToTable(dataGridView1);
            Get.GridViewDataLoad(newdt, dataGridView1, bindingNavigator2);
            Get.GridViewHeaderFilter(dataGridView1);
        }
        #endregion

    }
}
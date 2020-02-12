using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI
{
    public partial class fajuanFrm : Form
    {
        #region 变量

        MainFrm m1;
        public static fajuanFrm fF;
        #endregion
        #region 构造函数
        public fajuanFrm(MainFrm mm)
        {
            InitializeComponent();
            //BLL.Manager.ShadowEffect(pictureBox2.Handle);// 显示窗体阴影效果
            m1 = mm;
            fF = this;
        }
        #endregion
        #region 变量、对象
        // 控件复选状态
        bool CheckAuto = false;
        // 控件加载状态(上传套卷)
        bool CheckUpLoad = false;
        // 控件加载状态(自定义)
        bool CheckCustom = false;
        #endregion

        #region 组卷模式(AB、学号、模板）
        private void ABjuanMode(object sender, EventArgs e)
        {
            // 清空窗体控件
            panel2.Visible = false;

            // 显示指定控件
            LeftSidebar.Visible = true;
            RightSidebar.Visible = true;
            dataSource.Visible = true;
            dataSource.BringToFront();
        }

        private void StuIDMode(object sender, EventArgs e)
        {

        }

        private void TemplateMode(object sender, EventArgs e)
        {

        }

        #endregion
        #region 窗体加载
        private void fajuanFrm_Load(object sender, EventArgs e)
        {
            BLL.KEY.fajuanFrmkey = "1";
        }
        private void fajuanFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            BLL.KEY.fajuanFrmkey = "";
        }
        #endregion
        #region 窗体激活
        private void fajuanFrm_Activated(object sender, EventArgs e)
        {
            ExamMainTea.EMTFrm.BottomSidebar.Visible = true;
            ExamMainTea.EMTFrm.PreviousStep.Text = "返回主页";
            ExamMainTea.EMTFrm.NextStep.Text = "下一步";
            #region 取消上一次操作添加的事件
            string[] prevstep = BLL.Method.GetBindingMethod(ExamMainTea.EMTFrm.PreviousStep,"Click");
            string[] nextstep = BLL.Method.GetBindingMethod(ExamMainTea.EMTFrm.NextStep,"Click");
            if (prevstep != null)
            {
                for (int i = 0; i < prevstep.Count(); i++)
                {
                    if (prevstep[i] == "Previous_fajuan") ExamMainTea.EMTFrm.PreviousStep.Click -= fajuanFrm.fF.Previous_fajuan;
                    if (prevstep[i] == "Previous_kaosheng") ExamMainTea.EMTFrm.PreviousStep.Click -= ExamineeManFrm.EMF.Previous_kaosheng;
                }
            }
            if (nextstep != null)
            {
                for (int i = 0; i < nextstep.Count(); i++)
                {
                    if (nextstep[i] == "Next_fajuan") ExamMainTea.EMTFrm.NextStep.Click -= fajuanFrm.fF.Next_fajuan;
                    if (nextstep[i] == "Next_kaosheng") ExamMainTea.EMTFrm.NextStep.Click -= ExamineeManFrm.EMF.Next_kaosheng;
                }
            }
            #endregion
            ExamMainTea.EMTFrm.PreviousStep.Click += Previous_fajuan;// 单击事件
            ExamMainTea.EMTFrm.NextStep.Click += Next_fajuan;// 单击事件
        }
        #endregion
        #region 返回主页
        public void Previous_fajuan(object sender, EventArgs e)
        {
            if (BLL.KEY.MainFrmkey != "1")// 题库窗体关闭状态
            {
                ExamMainTea.MF = new MainFrm((ExamMainTea)ActiveForm);
                ExamMainTea.MF.MdiParent = (ExamMainTea)ActiveForm;  // 使父窗体成为子窗体的MDI容器
                ExamMainTea.MF.Show();
                ExamMainTea.MF.WindowState = FormWindowState.Maximized;
            }
            else// 激活窗体
            {
                Login.BLL.TeaManager.ActiveFrm(ExamMainTea.MF);
            }
            ExamMainTea.EMTFrm.BottomSidebar.Visible = false;
        }
        #endregion
        #region 下一步
        public void Next_fajuan(object sender, EventArgs e)
        {
            // 判断通过的条件
            if (dataSource.CurrentCell == null) { MessageBox.Show("未选中套题"); return; };

            if (BLL.KEY.ExamineeManFrmkey != "1")// 考生窗体关闭状态
            {
                ExamMainTea.EMF = new ExamineeManFrm((ExamMainTea)ActiveForm);
                ExamMainTea.EMF.MdiParent = ActiveForm;  // 使父窗体成为子窗体的MDI容器
                ExamMainTea.EMF.Tag = "0";// 发卷状态
                ExamMainTea.EMF.Show();
                ExamMainTea.EMF.WindowState = FormWindowState.Maximized;
            }
            else// 激活窗体
            {
                Login.BLL.TeaManager.ActiveFrm(ExamMainTea.EMF);
                ExamMainTea.EMF.Tag = "0";// 发卷状态
            }
            ExamMainTea.EMTFrm.checkBox1.Checked = true;// 题库通过
            ExamineeManFrm.SubNextToKS(BLL.Method.GetDgvToTable(dataSource), (int)dataSource.CurrentRow.Index);// 传值
        }
        #endregion

        #region 取消复选状态
        private void CustomPa_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckAuto == true && CustomPa.Checked == false)
            {
                CustomPa.Checked = true;
                return;
            }
            if (CustomPa.Checked == true)
                UploadPa.Checked = false;
        }

        private void UploadPa_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckAuto == true && UploadPa.Checked == false)
            {
                UploadPa.Checked = true;
                return;
            }
            if (UploadPa.Checked == true)
                CustomPa.Checked = false;
        }

        private void CustomPa_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && CustomPa.Checked == true)
                CheckAuto = true;
            if (e.Button == MouseButtons.Left && CustomPa.Checked == false)
                CheckAuto = false;
        }

        private void UploadPa_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && UploadPa.Checked == true)
                CheckAuto = true;
            if (e.Button == MouseButtons.Left && UploadPa.Checked == false)
                CheckAuto = false;
        }

        #endregion

        #region 自定义试卷
        private void CustomPa_Click(object sender, EventArgs e)
        {
            CheckUpLoad = false;// 上传套卷未加载
            if (CheckCustom == false)
            {
                // 清空缓存
                dataSource.DataSource = null;
                dataSource.Columns.Clear();

                // 更改控件
                btnAdd.Text = "添加套卷题目";
                btnRevise.Text = "修改套卷题目";
                btnDel.Text = "删除套卷题目";
                btn1.Visible = true;
                btn1.BringToFront();
            }
            // 自定义已加载
            CheckCustom = true;
        }

        public void btn1_Click(object sender, EventArgs e)
        {
            dataSource.DataSource = null;// 每次打开都清空
            //dataSource.DataSource = DAL.ExcelDBTool.ReadExcelToTable();
            btn1.Visible = false;
        }

        #endregion

        #region 上传套卷

        private void UploadPa_Click(object sender, EventArgs e)
        {
            CheckCustom = false;// 自定义未加载
            CheckAuto = true;
            if (CheckUpLoad == false)
            {
                // 清空缓存
                dataSource.DataSource = null;
                dataSource.Columns.Clear();

                // 加载数据表  
                BLL.Method.DGVStyleToSub(dataSource);
                DAL.DAO.Display(dataSource, "tb_taoti");
                dataSource.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                // 更改控件
                btnAdd.Text = "上传套卷";
                btnRevise.Text = "修改套卷";
                btnDel.Text = "删除套卷";
                btn1.Visible = false;
                // 上传套卷已加载
                CheckUpLoad = true;
            }
        }
        #endregion
        #region 添加题目（上传、自定义Custom)
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (btnAdd.Text == "上传套卷")
            {
                UploadSub US = new UploadSub();
                US.ShowDialog();
            }
            else
            {
                AddSub add = new AddSub();
                add.ShowDialog();
            }
        }
        #endregion
        #region 刷新
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (UploadPa.Checked == true)// 刷新套题数据表
            {
                DAL.DAO.Display(dataSource, "tb_taoti");
            }
        }
        #endregion
        #region 删除
        private void btnDel_Click(object sender, EventArgs e)
        {
            if (dataSource.CurrentCell == null)
            {
                MessageBox.Show("未选中或无数据，请添加。");
                return;
            }

            if (MessageBox.Show("确认删除编号为 " + dataSource.Rows[dataSource.CurrentCell.RowIndex].Cells[0].Value + " 的数据吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2) != DialogResult.OK)
                return;

            // 实例MySql工具
            DAL.MySqlDBTool MySqlDB = new DAL.MySqlDBTool(); ;
            int SelIndex = dataSource.CurrentCell.RowIndex;

            var MinCount = dataSource.Rows[SelIndex].Cells[0].Value;// 删除行的行索引
            var MaxCount = dataSource.Rows.Count;// 第一列行数
            Model.ExamInfo.MySqlInsIndex = (long)dataSource.Rows[MaxCount - 1].Cells[0].Value;

            // 设置要删除的数据
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("ID", dataSource.Rows[SelIndex].Cells[0].Value.ToString());

            if (MySqlDB.ParMySqlIDUCode("tb_taoti", dic, 0) > 0)// 判断SQL语句是否输入正确
            {
                if (Model.ExamInfo.MySqlInsIndex == (long)MinCount)// 刷新数据表
                {
                    MySqlDB.Display(dataSource, "tb_taoti");
                    return;
                }

                // 删除ID为n，将 n + 1 的ID值 - 1
                dic.Clear();
                dic.Add("ID", "ID-1");
                string cusapp = @" ID BETWEEN " + MinCount + @" AND " + Model.ExamInfo.MySqlInsIndex + " ORDER BY " + dic.Keys.ToList()[0] + " ASC";

                MySqlDB.CusMySqlIDUCode("tb_taoti", cusapp, dic);
                MySqlDB.Display(dataSource, "tb_taoti");// 刷新数据表
            }
            else
            {
                MessageBox.Show("数据添加失败，请重试。0");
                return;
            }
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
                US.Tag = "0";
                US.ShowDialog();
            }
        }

        #endregion
        #region 单元格变化，决定下一步
        private void dataSource_CurrentCellChanged(object sender, EventArgs e)
        {
            if (((DataGridView)sender).CurrentCell == null) return;
            this.label1.Text = "选定套题编号：" + (((DataGridView)sender).CurrentCell.RowIndex + 1);
        }
        #endregion
    }
}
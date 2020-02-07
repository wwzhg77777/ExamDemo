using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI
{
    public partial class fajuanFrm : Form
    {
        #region 静态变量
        public static fajuanFrm fF;
        #endregion

        MainFrm m1;
        public fajuanFrm(MainFrm mm)
        {
            InitializeComponent();
            //BLL.Manager.ShadowEffect(pictureBox2.Handle);// 显示窗体阴影效果
            m1 = mm;
            fF = this;
        }

        #region 变量、对象
        // 控件复选状态
        bool CheckAuto = false;
        // 控件加载状态(上传套卷)
        bool CheckUpLoad = false;
        // 控件加载状态(自定义)
        bool CheckCustom = false;
        #endregion

        #region 组卷模式
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

        private void fajuanFrm_Load(object sender, EventArgs e)
        {
            BLL.KEY.fajuanFrmkey = "1";
        }

        private void PreviousStep_Click(object sender, EventArgs e)
        {
        }

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
            dataSource.DataSource = DAL.ExcelDBTool.ReadExcelToTable();
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
                DAL.DAO.DataGridViewStyle(dataSource);
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

        private void fajuanFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            BLL.KEY.fajuanFrmkey = "";
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (UploadPa.Checked == true)// 刷新套题数据表
            {
                DAL.DAO.Display(dataSource, "tb_taoti");
            }
        }

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

            if (MySqlDB.ParMySqlIDUCode(dic, 0) > 0)// 判断SQL语句是否输入正确
            {
                if (Model.ExamInfo.MySqlInsIndex == (long)MinCount)
                    MySqlDB.Display(dataSource, "tb_taoti");// 刷新数据表

                // DAL.DAO.DataGridViewStyle(dataSource);// 查询不需要再次加载列标题
                dic.Clear();
                dic.Add("ID", "ID-1");
                string cusapp = @" ID BETWEEN " + MinCount + @" AND " + Model.ExamInfo.MySqlInsIndex + " ORDER BY " + dic.Keys.ToList()[0] + " ASC";
                // 删除ID为n，将 n + 1 的ID值 - 1
                if (MySqlDB.CusMySqlIDUCode(cusapp, dic) > 0)// 判断SQL语句是否输入正确
                {
                    MySqlDB.Display(dataSource, "tb_taoti");// 刷新数据表
                    MessageBox.Show("成功删除一条数据。");
                }
                else
                {
                    MessageBox.Show("数据添加失败，请重试。1");
                    return;
                }
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
                if (US.Tag.ToString() != "0")
                {
                    US.Tag = "0";
                    US.ShowDialog();
                }
            }
        }

        #endregion

        #region 下一步
        private void NextStep_Click(object sender, EventArgs e)
        {


        }

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog file = new FolderBrowserDialog();
            file.ShowDialog();
            string txtfilepath = file.SelectedPath + @"\test.txt";
            Debug.Print("txtfilepath:" + file.SelectedPath);
            Stopwatch stopw = new Stopwatch();
            using (FileStream txtfile = new FileStream(txtfilepath, FileMode.Create, FileAccess.Write))
            {
                using (StreamWriter sw = new StreamWriter(txtfile,Encoding.UTF8))
                {
                    List<string> lst = new List<string>();
                    lst.Add("null");
                    foreach (DataGridViewRow item in dataSource.Rows)
                    {
                        foreach (DataGridViewCell item1 in item.Cells)
                        {
                            lst.Add(item1.Value.ToString());
                            Debug.Print(item1.Value.ToString());

                        }
                    }
                    stopw.Restart();
                    for (int i = 1; i < lst.Count; i++)
                    {
                        if (i % 3 == 0)
                        {
                            sw.WriteLine(lst[i]);
                            continue;
                        }
                        sw.Write(lst[i] + "\t");
                    }
                    stopw.Stop();
                    MessageBox.Show(stopw.Elapsed.TotalMilliseconds.ToString());
                    sw.Close();
                    txtfile.Close();
                }
            }
            string sql = @"LOAD DATA LOCAL INFILE '" + txtfilepath.Replace(@"\","/") + @"' INTO TABLE tb_test FIELDS TERMINATED BY '\t' LINES TERMINATED BY '\r\n' ;";
            Debug.Print(sql);
            stopw.Restart();
            DAL.MySqlDBTool MySqlDB = new DAL.MySqlDBTool();
            MySqlDB.Customsql(sql);
            stopw.Stop();
            MessageBox.Show(stopw.Elapsed.TotalMilliseconds.ToString());
        }
    }
}

/*
 * // 判断通过的条件
            if (dataSource.CurrentCell == null)
                return;
            int SelIndex = dataSource.CurrentCell.RowIndex;
            label1.Text = "选定\r\n套题编号: " + dataSource.Rows[SelIndex].Cells[0].Value.ToString();
            */

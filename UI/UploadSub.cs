using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI
{
    public partial class UploadSub : Form
    {
        public UploadSub()
        {
            InitializeComponent();
        }

        private void Return_Click(object sender, EventArgs e)
        {
            if (listView1.Items[0].Text != "添加A卷" || listView1.Items[1].Text != "添加B卷")
            {
                if (MessageBox.Show("试卷已上传到系统缓存，返回将不保存试卷文件。\r\n确定返回吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    this.Close();
                else { }

            }
            else
                this.Close();

        }

        #region 窗体加载事件
        private void UploadSub_Load(object sender, EventArgs e)
        {
            // 默认状态
            comboBox1.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            btnClear.Enabled = false;

            // AB卷添加数据
            listView1.Items.Clear();
            listView1.Items.Add("添加A卷");
            listView1.Items.Add("添加B卷");
            listView1.LargeImageList = imageList1;// 图像列表

            // 添加默认显示的图像
            for (int i = 0; i < 2; i++)
                listView1.Items[i].ImageIndex = 0;

            // 添加所属课程
            string sql = "select `Name` from `tb_lesson`";
            DataTable LessonDT= DAL.MySqlDBTool.MySqlCodeByDT(sql);
            foreach (DataRow item in LessonDT.Rows)
                comboBox2.Items.Add(item.ItemArray[0].ToString());
        }
        #endregion

        #region  插入数据
        private void Complete_Click(object sender, EventArgs e)
        {
            // 实例MySql工具
            DAL.MySqlDBTool MySqlDB = null;


            var MinCount = 0;// 插入行的行索引
            var MaxCount = 0;// 第一列行数

            // 添加要插入的数据
            Dictionary<string, string> dic = new Dictionary<string, string>();
            Dictionary<string, string> Sourcedic = new Dictionary<string, string>();

            // 判断通过的条件
            switch (comboBox3.SelectedIndex)
            {
                case 0: // 添加套题
                    if (comboBox4.Text == string.Empty ||
                        listView1.Items[0].Text == "添加A卷" || listView1.Items[1].Text == "添加B卷")
                    {
                        MessageBox.Show("信息填写不完整或试卷未上传，请检查。0");
                        return;
                    }

                    #region 添加数据
                    MySqlDB = new DAL.MySqlDBTool();
                    MaxCount = fajuanFrm.fF.dataSource.Rows.Count;
                    if (MaxCount > 0)
                        Model.ExamInfo.MySqlInsIndex = ((long)fajuanFrm.fF.dataSource.Rows[MaxCount - 1].Cells[0].Value) + 1L;
                    else
                        Model.ExamInfo.MySqlInsIndex = 1;

                    // 添加要插入的数据
                    dic.Clear();
                    dic.Add("ID", Model.ExamInfo.MySqlInsIndex.ToString());
                    dic.Add("Name", comboBox4.Text);
                    dic.Add("Type", comboBox1.Text);
                    dic.Add("ofLesson", comboBox2.Text);

                    // 所属课程可为空
                    if (comboBox2.Text == string.Empty)
                        dic.Remove("ofLesson");

                    if (MySqlDB.ParMySqlIDUCode("tb_taoti",dic) > 0)// 判断SQL语句是否输入正确
                    {
                        MySqlDB.Display(fajuanFrm.fF.dataSource, "tb_taoti");// 刷新数据表
                        MessageBox.Show("成功录入一条数据");
                    }
                    else
                    {
                        MessageBox.Show("数据添加失败，请重试。");
                        return;
                    }
                    #endregion
                    break;

                case 1: // 插入套题
                    if (comboBox4.Text == string.Empty || combInsRecord.Text == string.Empty ||
                        listView1.Items[0].Text == "添加A卷" || listView1.Items[1].Text == "添加B卷")
                    {
                        MessageBox.Show("信息填写不完整或试卷未上传，请检查。1");
                        return;
                    }

                    #region 添加数据
                    MySqlDB = new DAL.MySqlDBTool();
                    MaxCount = fajuanFrm.fF.dataSource.Rows.Count;
                    if (MaxCount > 0)
                        Model.ExamInfo.MySqlInsIndex = (long)fajuanFrm.fF.dataSource.Rows[MaxCount - 1].Cells[0].Value;
                    else
                        Model.ExamInfo.MySqlInsIndex = 1;

                    // 添加要插入的数据
                    dic.Clear();
                    dic.Add("ID", "ID+1");

                    // 从n处插入，将 n + 1 的ID值 + 1
                    int.TryParse(combInsRecord.Text, out MinCount);
                    string cusapp = @" ID BETWEEN " + MinCount + @" AND " + Model.ExamInfo.MySqlInsIndex + " ORDER BY "+dic.Keys.ToList()[0]+" DESC";

                    if (MySqlDB.CusMySqlIDUCode("tb_taoti",cusapp,dic) > 0)// 判断SQL语句是否输入正确
                    {
                        dic.Clear();
                        dic.Add("ID", MinCount.ToString());
                        dic.Add("Name", comboBox4.Text);
                        dic.Add("Type", comboBox1.Text);
                        dic.Add("ofLesson", comboBox2.Text);

                        // 所属课程可为空
                        if (comboBox2.Text == string.Empty)
                            dic.Remove("ofLesson");

                        if (MySqlDB.ParMySqlIDUCode("tb_taoti",dic) > 0)// 判断SQL语句是否输入正确
                        {
                            MySqlDB.Display(fajuanFrm.fF.dataSource, "tb_taoti");// 刷新数据表
                            MessageBox.Show("成功插入一条数据");
                        }
                        else
                        {
                            MessageBox.Show("数据添加失败，请重试。0");
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("数据添加失败，请重试。1");
                        return;
                    }

                    #endregion
                    break;
            }

            // 录入成功清除缓存，恢复默认状态
            comboBox1.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            comboBox4.Items.Clear();
            comboBox4.Text = "";
            combInsRecord.SelectedIndex = -1;

            btnClear.Enabled = false;
            listView1.Items[0].Text = "添加A卷";
            listView1.Items[1].Text = "添加B卷";
            listView1.LargeImageList = imageList1;// 图像列表
            comboBox4.Items.Clear();

            // 添加默认显示的图像
            for (int i = 0; i < 2; i++)
                listView1.Items[i].ImageIndex = 0;
        }

        #endregion
        #region 打开文件
        private void listView1_Click(object sender, EventArgs e)
        {
            // 打开文件
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "Word文件| *.doc*";
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

            #region 判断选中项
            // 添加图像列表
            ImageList imgLst = new ImageList();
            imgLst.ImageSize = new Size(70, 70);
            imgLst.ColorDepth = ColorDepth.Depth24Bit;
            switch (listView1.SelectedItems[0].Index)
            {
                case 0: // A卷
                    imgLst.Images.Clear();
                    imgLst.Images.Add(BLL.Method.GetLargeIcon(path));
                    imgLst.Images.Add(listView1.Items[0].ImageList.Images[1]);
                    listView1.LargeImageList = imgLst;
                    for (int i = 0; i < 2; i++)
                        listView1.Items[i].ImageIndex = i;

                    // 添加名称
                    listView1.SelectedItems[0].Text = filename + " （A卷） ";
                    comboBox4.Items.Add(Path.GetFileNameWithoutExtension(path));
                    // 开启清空文件按钮
                    btnClear.Enabled = true;
                    break;

                case 1: // B卷
                    imgLst.Images.Clear();
                    imgLst.Images.Add(listView1.Items[0].ImageList.Images[0]);
                    imgLst.Images.Add(BLL.Method.GetLargeIcon(path));
                    listView1.LargeImageList = imgLst;
                    for (int i = 0; i < 2; i++)
                        listView1.Items[i].ImageIndex = i;

                    // 更改选中项名称
                    listView1.SelectedItems[0].Text = filename + " （B卷） ";
                    comboBox4.Items.Add(Path.GetFileNameWithoutExtension(path));

                    // 开启清空文件按钮
                    btnClear.Enabled = true;
                    break;
            }
            #endregion
        }
        #endregion
        #region 插入方式
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.SelectedIndex == 1)
                pelInsSub.Visible = true;
            else
                pelInsSub.Visible = false;
        }
        #endregion
        #region 显示数据表的序号
        private void combInsRecord_Click(object sender, EventArgs e)
        {
            // 清空列表缓存
            combInsRecord.Items.Clear();
            // 读取数据表第一列数据
            for (int i = 0; i < fajuanFrm.fF.dataSource.RowCount; i++)
            {
                combInsRecord.Items.Add(fajuanFrm.fF.dataSource.Rows[i].Cells[0].Value);
            }
        }
        #endregion
        #region 清空文件
        private void btnClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定清空当前缓存文件？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                // 恢复默认状态
                btnClear.Enabled = false;
                listView1.Items[0].Text = "添加A卷";
                listView1.Items[1].Text = "添加B卷";
                listView1.LargeImageList = imageList1;// 图像列表
                comboBox4.Items.Clear();

                // 添加默认显示的图像
                for (int i = 0; i < 2; i++)
                    listView1.Items[i].ImageIndex = 0;
            }
            else { }
        }
        #endregion
    }
}

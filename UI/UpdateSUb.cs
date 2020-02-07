using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI
{
    public partial class UpdateSUb : Form
    {
        public static fajuanFrm fajuanDGV;
        public UpdateSUb()
        {
            InitializeComponent();
        }

        private void Return_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        #region 修改
        private void Complete_Click(object sender, EventArgs e)
        {
            // 判断通过的条件
            if(textBox1.Text.Trim()==string.Empty)
            {
                MessageBox.Show("套题名称不能为空");
                return;
            }

            #region 添加数据
            // 获取选中行的第一列的值
            int SelIndex = fajuanFrm.fF.dataSource.CurrentCell.RowIndex;

            // 实例MySql工具
            DAL.MySqlDBTool MySqlDB = new DAL.MySqlDBTool();

            // 添加要修改的数据
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("ID",comboBox1.Text );
            dic.Add("Name",textBox1.Text);
            dic.Add("Type",comboBox2.Text);
            dic.Add("LessonID", comboBox3.Text);

            // 添加条件
            Dictionary<string, string> Sourcedic = new Dictionary<string, string>();
            Sourcedic.Add("ID", fajuanFrm.fF.dataSource.Rows[SelIndex].Cells[0].Value.ToString());

            // 所属课程可为空
            if (comboBox3.Text == string.Empty)
                dic.Remove("LessonID");

            if (MySqlDB.ParMySqlIDUCode(dic, Sourcedic) > 0)// 判断SQL语句是否输入正确
            {
                // DAL.DAO.DataGridViewStyle(fajuanFrm.dataSource);// 查询不需要再次加载列标题
                MySqlDB.Display(fajuanFrm.fF.dataSource, "tb_taoti");// 刷新数据表
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

        #region 窗体加载事件
        private void UpdateSUb_Load(object sender, EventArgs e)
        {
            #region 多窗体判断
            switch (this.Tag.ToString())
            {
                case "0":// 发卷_题库管理
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

                case "1":// 发卷_考生管理
                    panel1.Visible = true;

                    break;
            }

            #endregion
        }

        #endregion

    }
}

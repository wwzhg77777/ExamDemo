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
    public partial class AddSub : Form
    {
        #region 构造函数
        public AddSub()
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
        #region 窗体加载
        private void AddSub_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
        }
        #endregion

        #region 完成
        private void Complete_Click(object sender, EventArgs e)
        {
            // 判断通过的条件
        }
        #endregion

        #region
        #endregion

        #region
        #endregion

        #region
        #endregion

        #region
        #endregion

        #region
        #endregion

        #region
        #endregion

    }
}

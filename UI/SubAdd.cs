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
    public partial class SubAdd : Form
    {
        public SubAdd()
        {
            InitializeComponent();
        }
        #region 窗体加载：判断由哪个状态调用该窗体
        private void SubAdd_Load(object sender, EventArgs e) { }
        #endregion

        // 返回
        private void Ruturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region 修改/添加
        private void AddorRevise_Click(object sender, EventArgs e)
        {

        }
        #endregion
    }
}

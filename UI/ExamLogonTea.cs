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
    public partial class ExamLogonTea : Form
    {
        public ExamLogonTea()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLogon_Click(object sender, EventArgs e)
        {
            // 判断通过的条件

            // 密码与确认密码必须相同

            // 查询mysql并执行注册

            // 显示注册成功，返回
            this.Close();
        }
    }
}

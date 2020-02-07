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
    public partial class ExamLoginTea : Form
    {
        public ExamLoginTea()
        {
            InitializeComponent();
        }

        #region 登录系统
        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string Name = txtAdminName.Text.Trim();   //取出用户界面的数据
                string MD5_PWD = DAL.Method.GetMd5(txtAdminPWD.Text);
                Login.BLL.TeaManager mgr = new Login.BLL.TeaManager();
                Login.Model.TeaInfo Teacher = mgr.TeaLogin(Name, MD5_PWD);   //使用用户界面数据 进行查找
                //如果没有问题，则登陆成功
                BLL.KEY.mLogin = "1";
                this.Close();
                Login.Model.TeaInfo.userlab = Name;
                Login.Model.TeaInfo.powerlab = "管理员";
                Login.Model.TeaInfo.numberlab = "1";
            }
            catch    //如果登陆有异常 则登陆失败
            {
                MessageBox.Show("登录失败，用户名或密码错误","错误",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            }
        }
        #endregion

        private void btnExit_Click(object sender, EventArgs e)// 退出系统
        {
            this.Close();
        }

        private void btnLogon_Click(object sender, EventArgs e)
        {
            ExamLogonTea ELT = new ExamLogonTea();
            //this.Hide();
            ELT.ShowDialog();
        }
    }
}

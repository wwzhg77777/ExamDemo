using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Login.BLL    //业务逻辑层
{
    public class TeaManager
    {
        #region 登录操作
        public Login.Model.TeaInfo TeaLogin(string Name, string PWD)
        {
            ///throw new NotImplementedException();
            Login.DAL.TeaDAO uDAO = new Login.DAL.TeaDAO();  //创建一个user
            Login.Model.TeaInfo Teacher = uDAO.SelectUser(Name, PWD);  //通过ui中填写的内容 返回来相应的数据

            if (Teacher != null)        //如果数据库中没有数据，即为首次登陆了。
            {
                return Teacher;
            }
            else       //如果数据库中没有该用户名，则登陆失败
            {
                throw new Exception("登陆失败");
            }
        }
        #endregion

        #region 激活窗体
        public static void ActiveFrm(Form Frm)
        {
            Frm.Activate();
            Frm.TopMost = true;
            Frm.WindowState = FormWindowState.Maximized;
        }
        #endregion
    }
}

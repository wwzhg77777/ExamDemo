using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login.Model  // 实体层
{
    public class TeaInfo
    {
        #region 字段、变量

        // 管理员ID
        public int ID { get; set; }

        // 管理员名称
        public string Name { get; set; }

        // 管理员密码
        public string MD5_PWD { get; set; }

        // 注册时间
        public string JoinTime { get; set; }

        // 显示登录账户
        public static string userlab { get; set; }

        // 显示登录账户的权限
        public static string powerlab { get; set; }

        // 显示登录账户的编号
        public static string numberlab { get; set; }

        #endregion
    }
}

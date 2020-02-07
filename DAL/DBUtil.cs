using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace DAL
{
    public class DBUtil  //用于保存 链接服务器的sql语句
    {
        public static string ConnString = ConfigurationManager.ConnectionStrings["mysqlconn_ExamDemo"].ConnectionString;
    }
}

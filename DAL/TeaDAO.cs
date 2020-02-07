using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Forms;
using DAL;
using System.Drawing;

namespace Login.DAL     //数据访问层
{
    public class TeaDAO
    {
        #region 登录操作
        public Login.Model.TeaInfo SelectUser(string Name, string MD5_PWD)   //根据 ui 选择返回一个user
        {
            using (MySqlConnection conn = new MySqlConnection(DBUtil.ConnString))
            {
                //创建一个命令对象，并添加命令
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = @"SELECT * FROM tb_administrator WHERE Name=@UserName AND MD5_PWD=@Password";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.Add(new MySqlParameter("@userName", Name));
                cmd.Parameters.Add(new MySqlParameter("@Password", MD5_PWD));

                conn.Open();        //打开数据链接
                MySqlDataReader reader = cmd.ExecuteReader();

                Login.Model.TeaInfo user = null;     //用于保存读取的数据

                while (reader.Read())       //开始读取数据
                {
                    if (user == null)     //如果没有，则重新生成一个
                    {
                        user = new Login.Model.TeaInfo();
                    }
                    user.ID = reader.GetInt32(0);
                    user.Name = reader.GetString(1);
                    user.MD5_PWD = reader.GetString(2);
                    if (!reader.IsDBNull(3))         //不要求一定要有jointime，也可以返回
                    {
                        user.JoinTime = reader.GetString(3);
                    }
                }
                return user;
            }

        }
        #endregion

        #region 考生管理_数据库操作

        /// <summary>
        /// 管理员——添加列标题
        /// </summary>
        /// <param name="dgv">要显示的数据表</param>
        public static void DataGridViewStyleAd(DataGridView dgv)
        {
            dgv.AutoGenerateColumns = false;// 取消连接数据库后的自动创建列
            DataGridViewTextBoxColumn ID = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn Name = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn MD5_PWD = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn PWD = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn JoinTime = new DataGridViewTextBoxColumn();

            ID.Name = "ID";
            ID.DataPropertyName = "ID";
            ID.HeaderText = "管理员编号";

            Name.Name = "Name";
            Name.DataPropertyName = "Name";
            Name.HeaderText = "管理员名称";

            MD5_PWD.Name = "MD5_PWD";
            MD5_PWD.DataPropertyName = "MD5_PWD";
            MD5_PWD.HeaderText = "MD5管理员密码";

            PWD.Name = "PWD";
            PWD.DataPropertyName = "PWD";
            PWD.HeaderText = "管理员密码";

            JoinTime.Name = "JoinTime";
            JoinTime.DataPropertyName = "JoinTime";
            JoinTime.HeaderText = "注册时间";

            dgv.Columns.Clear();// 清空列表头

            // 数组的形式添加列标题
            dgv.Columns.AddRange(ID, Name, PWD, JoinTime, MD5_PWD);
            dgv.Font = new Font("GB2312", 11);
        }

        /// <summary>
        /// 教师——添加列标题
        /// </summary>
        /// <param name="dgv">要显示的数据表</param>
        public static void DataGridViewStyleTe(DataGridView dgv)
        {
            dgv.AutoGenerateColumns = false;// 取消连接数据库后的自动创建列
            DataGridViewTextBoxColumn ID = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn Name = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn PWD = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn JoinTime = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn Sex = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn Question = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn Answer = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn IPAddress = new DataGridViewTextBoxColumn();

            ID.Name = "ID";
            ID.DataPropertyName = "ID";
            ID.HeaderText = "教师编号";

            Name.Name = "Name";
            Name.DataPropertyName = "Name";
            Name.HeaderText = "教师名称";

            PWD.Name = "PWD";
            PWD.DataPropertyName = "PWD";
            PWD.HeaderText = "教师密码";

            JoinTime.Name = "JoinTime";
            JoinTime.DataPropertyName = "JoinTime";
            JoinTime.HeaderText = "注册时间";

            Sex.Name = "Sex";
            Sex.DataPropertyName = "Sex";
            Sex.HeaderText = "教师性别";

            Question.Name = "Question";
            Question.DataPropertyName = "Question";
            Question.HeaderText = "提示问题";

            Answer.Name = "Answer";
            Answer.DataPropertyName = "Answer";
            Answer.HeaderText = "问题答案";

            IPAddress.Name = "IPAddress";
            IPAddress.DataPropertyName = "IPAddress";
            IPAddress.HeaderText = "注册IP地址";

            dgv.Columns.Clear();// 清空列表头

            // 数组的形式添加列标题
            dgv.Columns.AddRange(ID, Name, PWD, JoinTime, Sex, Question, Answer, IPAddress);
            dgv.Font = new Font("GB2312", 11);
        }

        /// <summary>
        /// 学生——添加列标题
        /// </summary>
        /// <param name="dgv">要显示的数据表</param>
        public static void DataGridViewStyleSt(DataGridView dgv)
        {
            dgv.AutoGenerateColumns = false;// 取消连接数据库后的自动创建列
            DataGridViewTextBoxColumn ID = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn Name = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn PWD = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn JoinTime = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn Sex = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn Question = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn Answer = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn Profession = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn IPAddress = new DataGridViewTextBoxColumn();

            ID.Name = "ID";
            ID.DataPropertyName = "ID";
            ID.HeaderText = "学生学号";

            Name.Name = "Name";
            Name.DataPropertyName = "Name";
            Name.HeaderText = "学生名称";

            PWD.Name = "PWD";
            PWD.DataPropertyName = "PWD";
            PWD.HeaderText = "学生密码";

            JoinTime.Name = "JoinTime";
            JoinTime.DataPropertyName = "JoinTime";
            JoinTime.HeaderText = "注册时间";

            Sex.Name = "Sex";
            Sex.DataPropertyName = "Sex";
            Sex.HeaderText = "学生性别";

            Question.Name = "Question";
            Question.DataPropertyName = "Question";
            Question.HeaderText = "提示问题";

            Answer.Name = "Answer";
            Answer.DataPropertyName = "Answer";
            Answer.HeaderText = "问题答案";

            Profession.Name = "Profession";
            Profession.DataPropertyName = "Profession";
            Profession.HeaderText = "所属专业";

            IPAddress.Name = "IPAddress";
            IPAddress.DataPropertyName = "IPAddress";
            IPAddress.HeaderText = "注册IP地址";

            dgv.Columns.Clear();// 清空列表头

            // 数组的形式添加列标题
            dgv.Columns.AddRange(ID, Name, PWD, JoinTime, Sex, Question, Answer, Profession, IPAddress);
            dgv.Font = new Font("GB2312", 11);
        }

        /// <summary>
        /// 填充数据库的数据到表格
        /// </summary>
        /// <param name="dgv">要填充数据的表格</param>
        /// <param name="DBtable">数据表名称</param>
        public static void Display(DataGridView dgv, string DBtable)
        {
            string mysql = "SELECT * FROM " + DBtable;
            dgv.DataSource = null;
            dgv.DataSource = MySqlDBTool.MySqlCodeByDS(mysql, DBtable).Tables[DBtable];
        }
        
        /// <summary>
        /// 管理员_插入数据
        /// </summary>
        /// <param name="dbtool"></param>
        /// <param name="UserDatas"></param>
        /// <returns></returns>
        public static string AdminAdd(params TextBox[] UserDatas)
        {
            string Insertsql = "INSERT INTO `tb_administrator` (`ID`, `Name`, `MD5_PWD`, `PWD`, `JoinTime`) ";
            Insertsql += "VALUES (NULL, ";
            Insertsql += "'" + UserDatas[0].Text.Trim() + "',";
            Insertsql += "'" + Method.GetMd5(UserDatas[1].Text.Trim()) + "',";
            Insertsql += "'" + UserDatas[1].Text.Trim() + "',";
            Insertsql += "'" + UserDatas[2].Text.Trim() + "') ";
            return Insertsql;
        }

        /// <summary>
        /// 教师_插入数据
        /// </summary>
        /// <param name="dbtool"></param>
        /// <param name="UserDatas"></param>
        /// <returns></returns>
        public static string TeaAdd(  params TextBox[] UserDatas)
        {
            string Insertsql = "INSERT INTO `tb_administrator` (`ID`, `Name`, `MD5_PWD`, `PWD`, `JoinTime`) ";
            Insertsql += "VALUES (NULL, ";
            Insertsql += "'" + UserDatas[0].Text.Trim() + "',";
            Insertsql += "'" + UserDatas[1].Text.Trim() + "',";
            Insertsql += "'" + UserDatas[2].Text.Trim() + "',";
            Insertsql += "'" + UserDatas[3].Text.Trim() + "',";
            Insertsql += "'" + UserDatas[4].Text.Trim() + "',";
            Insertsql += "'" + UserDatas[5].Text.Trim() + "',";
            Insertsql += "'" + UserDatas[6].Text.Trim() + "') ";
            return Insertsql;
        }

        /// <summary>
        /// 学生_插入数据
        /// </summary>
        /// <param name="dbtool"></param>
        /// <param name="UserDatas"></param>
        /// <returns></returns>
        public static string StuAdd( params TextBox[] UserDatas)
        {
            string Insertsql = "INSERT INTO `tb_administrator` (`ID`, `Name`, `MD5_PWD`, `PWD`, `JoinTime`) ";
            Insertsql += "VALUES (NULL, ";
            Insertsql += "'" + UserDatas[0].Text.Trim() + "',";
            Insertsql += "'" + UserDatas[1].Text.Trim() + "',";
            Insertsql += "'" + UserDatas[2].Text.Trim() + "',";
            Insertsql += "'" + UserDatas[3].Text.Trim() + "',";
            Insertsql += "'" + UserDatas[4].Text.Trim() + "',";
            Insertsql += "'" + UserDatas[5].Text.Trim() + "',";
            Insertsql += "'" + UserDatas[6].Text.Trim() + "') ";
            return Insertsql;
        }
        #endregion
    }

}
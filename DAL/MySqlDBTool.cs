using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DAL
{
    /// <summary>
    /// 定义MySql查询语句的类型
    /// </summary>
    public enum IDU
    {
        /// <summary>
        /// 插入
        /// </summary>
        Insert = 0,
        /// <summary>
        /// 查询
        /// </summary>
        Select = 1,
        /// <summary>
        /// 更新
        /// </summary>
        Update = 2,
        /// <summary>
        /// 删除
        /// </summary>
        Delete = 3
    }

    public class MySqlDBTool
    {
        private static MySqlConnection conn = null;// 定义连接数据库的对象

        public MySqlDBTool()
        {
            conn = new MySqlConnection(DBUtil.ConnString);
        }

        /// <summary>
        /// 自定义sql语句
        /// </summary>
        /// <param name="dgv">要填充数据的表格</param>
        /// <param name="tbname">数据表名称</param>
        public int Customsql(string sql)
        {
            int r = 0;
            try
            {
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Connection.Open();
                    r = (int)cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception rt)
            {
                MessageBox.Show(rt.Message);
                conn.Close();
            }
            return r;
        }

        /// <summary>
        /// 使用Load Data Local Infile语句执行mysql查询
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public int ImportDataToMySql(string cmd)
        {
            try
            {
                conn.Open();
                int a = MySqlHelper.ExecuteNonQuery(conn, cmd);
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                return a;
            }
            catch
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                return -1;
            }
        }

        /// <summary>
        /// 填充数据库的数据到表格
        /// </summary>
        /// <param name="dgv">要填充数据的表格</param>
        /// <param name="tbname">数据表名称</param>
        public void Display(DataGridView dgv, string tbname)
        {
            string sql = "SELECT * FROM " + tbname;
            dgv.DataSource = null;
            dgv.DataSource = MySqlCodeByDS(sql, tbname).Tables[tbname];
        }
      
        /// <summary>
        /// 填充数据库的数据到表格
        /// </summary>
        /// <param name="dgv">要填充数据的表格</param>
        /// <param name="tbname">数据表名称</param>
        /// <param name="bn">导航栏</param>
        public void BindingDisplay(DataGridView dgv,   string tbname, BindingNavigator bn)
        {
            string sql = "SELECT * FROM " + tbname;
            BindingSource bs = new BindingSource(MySqlCodeByDS(sql, tbname).Tables[tbname], null);
            bn.BindingSource = bs;
            dgv.DataSource = null;
            dgv.DataSource = bs;
        }

        /// <summary>
        /// MySQL查询语句的方法
        /// 返回DataSet数据表
        /// </summary>
        /// <param name="sql">要查询的语句</param>
        /// <param name="dbname"></param>
        /// <returns></returns>
        public static DataSet MySqlCodeByDS(string sql, string table)
        {
            DataSet ds = new DataSet();
            try
            {
                using (conn = new MySqlConnection(DBUtil.ConnString))
                {
                    MySqlDataAdapter msda = new MySqlDataAdapter(sql, conn);
                    conn.Open();
                    msda.Fill(ds, table);
                }
            }
            catch (Exception EC)
            {
                MessageBox.Show(EC.Message);
                conn.Close();
                ds = null;
            }
            return ds;
        }

        /// <summary>
        /// MySQL查询语句的方法
        /// 返回DataTable数据表
        /// </summary>
        /// <param name="sql">要查询的语句</param>
        /// <param name="dbname"></param>
        /// <returns></returns>
        public static DataTable MySqlCodeByDT(string sql)
        {
            DataTable dt = new DataTable();
            try
            {
                using (conn = new MySqlConnection(DBUtil.ConnString))
                {
                    MySqlDataAdapter msda = new MySqlDataAdapter(sql, conn);
                    conn.Open();
                    msda.Fill(dt);
                }
            }
            catch (Exception EC)
            {
                MessageBox.Show(EC.Message);
                conn.Close();
                dt = null;
            }
            return dt;
        }

        /// <summary>
        /// 执行 insert、delete、update SQL语句的方法
        /// 返回Int值，若返回值大于0，则语句正确
        /// </summary>
        /// <param name="sql">要查询的语句</param>
        /// <returns></returns>
        public static int MySqlIDUCode(string sql)
        {
            int r = 0;
            try
            {
                MySqlCommand sc = new MySqlCommand(sql, conn);
                sc.Connection.Open();
                r = (int)sc.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception RT)
            {
                MessageBox.Show(RT.Message);
                conn.Close();
                r = 0;
            }
            return r;
        }

        #region 执行带参数的IDU语句的方法

        /// <summary>
        /// 执行带参数的 Select SQL语句的方法
        /// 返回Int值，若返回值大于0，则语句正确
        /// </summary>
        /// <param name="tbname">查询的表或视图名称</param>
        /// <param name="dic">查询条件（字段名和数据）。默认为null值，即无条件查询。当有多个条件时，各条件之间是与(and)关系</param>
        /// <param name="idu">查询类型</param>
        /// <param name="addCond">无条件查询时的自定义Where语句</param>
        /// <returns></returns>
        public int ParMySqlSelCode(string tbname, Dictionary<string, string> dic, IDU idu = IDU.Select, string addCond = "")
        {
            int r = 0;
            try
            {
                string sql = ParGetSelectMySql(tbname, dic, addCond);
                List<MySqlParameter> parameters = new List<MySqlParameter>();// 参数

                // 动态添加参数
                for (int i = 0; i < dic.Count; i++)
                    parameters.Add(new MySqlParameter("@" + dic.Keys.ToList()[i], dic.Values.ToList()[i]));

                MySqlCommand sc = new MySqlCommand(sql, conn);
                foreach (var item in parameters)
                    sc.Parameters.Add(item);
                MessageBox.Show(sc.CommandText);
                Debug.Print(sc.CommandText);
                // 开启数据库引擎
                sc.Connection.Open();
                // 返回执行的行数
                r = (int)sc.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception RT)
            {
                MessageBox.Show(RT.Message);
                conn.Close();
                r = 0;
            }
            return r;
        }

        /// <summary>
        /// 执行带参数的 insert SQL语句的方法
        /// 返回Int值，若返回值大于0，则语句正确
        /// </summary>
        /// <param name="tbname">数据表</param>
        /// <param name="dic">字典，key键对应字段，value值对应要插入的新数据</param>
        /// <param name="idu">查询的类型</param>
        /// <param name="StrAtt">附加条件</param>
        /// <returns></returns>
        public int ParMySqlIDUCode(string tbname,Dictionary<string, string> dic, IDU idu = IDU.Insert, string StrAtt = "")
        {
            int r = 0;
            try
            {
                string sql = ParGetInsertMySql(tbname, dic, StrAtt);
                List<MySqlParameter> parameters = new List<MySqlParameter>();// 参数

                // 动态添加参数
                for (int i = 0; i < dic.Count; i++)
                    parameters.Add(new MySqlParameter("@" + dic.Keys.ToList()[i], dic.Values.ToList()[i]));

                MySqlCommand sc = new MySqlCommand(sql, conn);
                foreach (var item in parameters)
                    sc.Parameters.Add(item);
                MessageBox.Show(sc.CommandText);
                // 开启数据库引擎
                if(conn.State==ConnectionState.Closed)
                sc.Connection.Open();
                // 返回执行查询的结果
                r = (int)sc.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception RT)
            {
                MessageBox.Show(RT.Message);
                conn.Close();
                r = 0;
            }
            return r;
        }

        /// <summary>
        /// 执行带参数的 update SQL语句的方法
        /// 返回Int值，若返回值大于0，则语句正确
        /// </summary>
        /// <param name="dic">字典，key键对应字段，value值对应要更新的数据（第一个key为ID）</param>
        /// <param name="Sourcedic">条件（键值对为更新之前的数据，第一个key为ID，value值为原ID）</param>
        /// <param name="idu">查询的类型</param>
        /// <param name="StrAtt">附加条件</param>
        /// <returns></returns>
        public int ParMySqlIDUCode(string tbname,Dictionary<string, string> dic, Dictionary<string, string> Sourcedic, IDU idu = IDU.Update, string StrAtt = "")
        {
            int r = 0;
            try
            {
                string sql = ParGetUpdateMySql(tbname, dic, Sourcedic, StrAtt);
                List<MySqlParameter> parameters = new List<MySqlParameter>();// 参数

                // 动态添加参数
                for (int i = 0; i < dic.Count; i++)
                    parameters.Add(new MySqlParameter("@" + dic.Keys.ToList()[i], dic.Values.ToList()[i]));

                MySqlCommand sc = new MySqlCommand(sql, conn);
                foreach (var item in parameters)
                    sc.Parameters.Add(item);
                MessageBox.Show(sc.CommandText);
                // 开启数据库引擎
                sc.Connection.Open();
                // 返回执行查询的结果
                r = (int)sc.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception RT)
            {
                MessageBox.Show(RT.Message);
                conn.Close();
                r = 0;
            }
            return r;
        }

        /// <summary>
        /// 执行带参数的 delete SQL语句的方法
        /// 返回Int值，若返回值大于0，则语句正确
        /// </summary>
        /// <param name="dic">字典，key键对应字段，value值对应要删除的条件(第一个key为ID）</param>
        /// <param name="nothing">填充，区分重写方法</param>
        /// <param name="idu">查询的类型</param>
        /// <param name="StrAtt">附加条件</param>
        /// <returns></returns>
        public int ParMySqlIDUCode(string tbname,Dictionary<string, string> dic, int nothing, IDU idu = IDU.Delete, string StrAtt = "")
        {
            int r = 0;
            try
            {
                string sql = ParGetDeleteMySql(tbname, dic, StrAtt);
                List<MySqlParameter> parameters = new List<MySqlParameter>();// 参数

                // 动态添加参数
                for (int i = 0; i < dic.Count; i++)
                    parameters.Add(new MySqlParameter("@" + dic.Keys.ToList()[i], dic.Values.ToList()[i]));

                MySqlCommand sc = new MySqlCommand(sql, conn);
                foreach (var item in parameters)
                    sc.Parameters.Add(item);
                //MessageBox.Show(sc.CommandText);
                // 开启数据库引擎
                sc.Connection.Open();
                // 返回执行查询的结果
                r = (int)sc.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception RT)
            {
                MessageBox.Show(RT.Message);
                conn.Close();
                r = 0;
            }
            return r;
        }


        /// <summary>
        /// 执行带参数的 update SQL语句的方法，自定义WHERE语句
        /// 返回Int值，若返回值大于0，则语句正确
        /// </summary>
        /// <param name="CusApp">自定义WHERE语句</param>
        /// <param name="dic">字典，key键对应字段，value值对应要更新的数据（第一个key为ID）</param>
        /// <param name="idu">查询的类型</param>
        /// <returns></returns>
        public int CusMySqlIDUCode(string tbname,string CusApp, Dictionary<string, string> dic, IDU idu = IDU.Update)
        {
            int r = 0;
            try
            {
                string sql = CusGetUpdateMySql(tbname, dic, CusApp);
                List<MySqlParameter> parameters = new List<MySqlParameter>();// 参数

                // 动态添加参数
                int diccount = 0;
                if (dic.Count > 1)
                    diccount = 1;
                else
                    diccount = 0;
                for (int i = diccount; i < dic.Count; i++)
                    parameters.Add(new MySqlParameter("@" + dic.Keys.ToList()[i], dic.Values.ToList()[i]));

                MySqlCommand sc = new MySqlCommand(sql, conn);
                foreach (var item in parameters)
                    sc.Parameters.Add(item);
                //MessageBox.Show(sc.CommandText);
                // 开启数据库引擎
                sc.Connection.Open();
                // 返回执行查询的结果
                r = (int)sc.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception RT)
            {
                MessageBox.Show(RT.Message);
                conn.Close();
                r = 0;
            }
            return r;
        }
        #endregion

        #region 带参数的IDU语句

        /// <summary>
        /// 生成带参数的select语句
        /// </summary>
        /// <param name="tbname">查询的表或视图名称</param>
        /// <param name="dic">查询条件（字段名和数据）。默认为null值，即无条件查询。当有多个条件时，各条件之间是与(and)关系</param>
        /// <param name="addCond">无条件查询时的自定义Where语句</param>
        /// <returns></returns>
        public static string ParGetSelectMySql(string tbname, Dictionary<string, string> dic, string addCond = "")
        {
            StringBuilder sb = new StringBuilder(Model.ExamInfo.BufferSize);
            sb.AppendFormat("SELECT * FROM `{0}` ", tbname);
            // 添加条件
            if (dic != null && dic.Count > 0)
            {// select * from `tb_stumanage` where `Department` = '计算机系'
                // 第一个条件
                sb.AppendFormat("WHERE `{0}` = '{1}'", dic.Keys.ToList()[0], dic.Values.ToList()[0]);
                int count = dic.Count;
                for (int i = 1; i < count; i++)
                {
                    sb.AppendFormat(" AND `{0}` = '{1}'", dic.Keys.ToList()[i], dic.Values.ToList()[i]);
                }
                // 附加条件
                if (addCond != "")
                    sb.AppendFormat(" AND ({0})", addCond);
            }
            else if (addCond != "")
            {
                // 只添加附加条件
                sb.Append(addCond);
            }
            return sb.ToString();
        }

        /// <summary>
        /// 生成带参数的Insert语句
        /// </summary>
        /// <param name="tbname">要插入的Table</param>
        /// <param name="dic">字典，key键对应字段，value值对应要插入的新数据（第一个key为ID）</param>
        /// <param name="StrAtt">附加条件</param>
        /// <returns></returns>
        private static string ParGetInsertMySql(string tbname, Dictionary<string, string> dic, string StrAtt = "")
        {
            // 无数据项返回空串
            if (dic == null || dic.Count < 1) return "";

            // 记录前段语句
            StringBuilder sb = new StringBuilder(Model.ExamInfo.BufferSize);
            sb.AppendFormat("INSERT INTO `{0}` (`", tbname);

            // 第一个数据项
            string sName = dic.Keys.ToList()[0];
            sb.Append(sName);

            // 记录后段语句
            StringBuilder sbValue = new StringBuilder(Model.ExamInfo.BufferSize);
            sbValue.Append(") VALUES (");
            sbValue.AppendFormat("@{0}", sName);// 第一个参数

            // 其他数据项
            for (int i = 1; i < dic.Count; i++)
            {
                sName = dic.Keys.ToList()[i];
                sb.AppendFormat("`, `{0}", sName);
                sbValue.AppendFormat(", @{0}", sName);
            }
            sb.AppendFormat("`{0})", sbValue.ToString());

            // 附加条件
            sb.AppendFormat(" {0}", StrAtt);
            return sb.ToString();
        }

        /// <summary>
        /// 生成带参数的Update语句
        /// </summary>
        /// <param name="tbname">要插入的Table</param>
        /// <param name="dic">字典，key键对应字段，value值对应要更新的数据（第一个key为ID）</param>
        /// <param name="Sourcedic">条件（键值对为更新之前的数据，第一个key为ID，value值为原ID）</param>
        /// <param name="StrAtt">附加条件</param>
        /// <returns></returns>
        private static string ParGetUpdateMySql(string tbname, Dictionary<string, string> dic, Dictionary<string, string> Sourcedic, string StrAtt = "")
        {
            // 无数据项返回空串
            if (dic == null || dic.Count < 1) return "";

            // 数据，第一个
            StringBuilder sb = new StringBuilder(Model.ExamInfo.BufferSize);
            sb.AppendFormat("UPDATE `{0}` SET `{1}` = @{1}", tbname, dic.Keys.ToList()[0]);

            // 其他数据
            for (int i = 1; i < dic.Count; i++)
                sb.AppendFormat(", `{0}` = @{0}", dic.Keys.ToList()[i]);

            // 条件，第一个
            sb.AppendFormat(" WHERE {0} = '{1}'", Sourcedic.Keys.ToList()[0], Sourcedic.Values.ToList()[0]);

            // 其他条件
            for (int i = 1; i < Sourcedic.Count; i++)
                sb.AppendFormat(" AND {0} = '{1}'", Sourcedic.Keys.ToList()[i], Sourcedic.Values.ToList()[i]);

            // 附加条件
            sb.AppendFormat(" {0}", StrAtt);
            return sb.ToString();
        }

        /// <summary>
        /// 生成带参数的Delete语句
        /// </summary>
        /// <param name="tbname">要插入的Table</param>
        /// <param name="dic">字典，key键对应字段，value值对应要删除的条件(第一个key为ID）</param>
        /// <param name="StrAtt">附加条件</param>
        /// <returns></returns>
        private static string ParGetDeleteMySql(string tbname, Dictionary<string, string> dic, string StrAtt = "")
        {
            // 无数据项返回空串
            if (dic == null || dic.Count < 1) return "";

            StringBuilder sb = new StringBuilder(Model.ExamInfo.BufferSize);
            sb.AppendFormat("DELETE FROM `{0}` WHERE ", tbname);

            // 第一个条件
            string sName = dic.Keys.ToList()[0];
            sb.AppendFormat("`{0}`.`{1}` = @{1}", tbname, sName, sName);

            // 其他条件
            for (int i = 1; i < dic.Count; i++)
            {
                sName = dic.Keys.ToList()[i];
                sb.AppendFormat(" AND `{0}`=@{0}", sName);
            }

            // 附加条件
            sb.AppendFormat(" {0}", StrAtt);
            return sb.ToString();
        }

        #endregion

        #region 带参数的IDU语句，自定义WHERE语句
        /// <summary>
        /// 生成带参数的Update语句，自定义WHERE语句
        /// </summary>
        /// <param name="tbname">要插入的Table</param>
        /// <param name="dic">字典，key键对应字段，value值对应要更新的数据（第一个key为ID）</param>
        /// <param name="CusApp">自定义WHERE语句</param>
        /// <returns></returns>
        private static string CusGetUpdateMySql(string tbname, Dictionary<string, string> dic, string CusApp)
        {
            // 无数据项返回空串
            if (dic == null || dic.Count < 1) return "";

            // 数据，第一个
            StringBuilder sb = new StringBuilder(Model.ExamInfo.BufferSize);
            sb.AppendFormat("UPDATE `{0}` SET `{1}` = {2}", tbname, dic.Keys.ToList()[0], dic.Values.ToList()[0]);

            // 其他数据
            for (int i = 1; i < dic.Count; i++)
                sb.AppendFormat(", `{0}` = @{0}", dic.Keys.ToList()[i]);

            // 自定义WHERE语句
            sb.AppendFormat(" WHERE {0}", CusApp);
            return sb.ToString();
        }

        /// <summary>
        /// 生成带参数的Delete语句，自定义WHERE语句
        /// </summary>
        /// <param name="tbname">要插入的Table</param>
        /// <param name="CusApp">自定义WHERE语句</param>
        /// <returns></returns>
        private static string CusGetDeleteMySql(string tbname, string CusApp)
        {
            StringBuilder sb = new StringBuilder(Model.ExamInfo.BufferSize);
            sb.AppendFormat("DELETE FROM `{0}` WHERE", tbname);

            // 自定义WHERE语句
            sb.AppendFormat(" {0}", CusApp);
            return sb.ToString();
        }

        #endregion
    }
}

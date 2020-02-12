using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;
using System.Windows.Forms;

namespace DAL
{
    #region Excel表格操作
    public static class ExcelDBTool
    {

        /// <summary>
        /// 读取Excel表格
        /// </summary>
        /// <param name="file">文件对话框</param>
        /// <param name="OpenOrNo">指示是否由该事件打开对话框,true为是，false为否</param>
        /// <returns></returns>
        public static DataTable ReadExcelToTable(OpenFileDialog file,bool OpenOrNo)
        {
            if (OpenOrNo)// true。由该事件打开对话框
            {
                // 打开文件
                file = new OpenFileDialog();
                file.Filter = "Excel文件| *.xls*";
                //file.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                file.Multiselect = false;

                if (file.ShowDialog() == DialogResult.Cancel)
                    return null;
            }

            //判断文件后缀
            var path = file.FileName;
            string filesuffix = System.IO.Path.GetExtension(path);
            if (string.IsNullOrEmpty(filesuffix))
                return null;

            using (DataSet ds = new DataSet())
            {
                // Excel连接字符串
                string connString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties='Excel 12.0;HDR=YES;IMAX=1'";

                using (OleDbConnection conn = new OleDbConnection(connString))
                {
                    conn.Open();
                    DataTable sheetsName = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "Table" }); //得到所有sheet的名字
                    string firstSheetName = sheetsName.Rows[0][2].ToString(); //得到第一个sheet的名字
                    string sql = string.Format("SELECT * FROM [{0}]", firstSheetName); //查询字符串
                    OleDbDataAdapter ada = new OleDbDataAdapter(sql, connString);
                    ada.Fill(ds);
                    return ds.Tables[0];
                }
            }
        }

    }
    #endregion
}

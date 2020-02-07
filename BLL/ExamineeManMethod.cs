using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BLL
{
    public class ExamineeManMethod
    {
        public static DataTable dt;
        #region 发卷的dgv控件添加数据表

        /// <summary>
        /// dgv控件添加列标题
        /// </summary>
        /// <param name="dgv">要添加列标题的控件</param>
        /// <param name="bn">导航栏</param>
        public void DGVStyleOffajuan(DataGridView dgv, BindingNavigator bn)
        {
            dt = new DataTable("fajuanDT");
            DataColumn[] dc = new DataColumn[]
            {
                new DataColumn("学生学号", Type.GetType("System.String")),
                new DataColumn("学生姓名", Type.GetType("System.String")),
                new DataColumn("所属专业", Type.GetType("System.String")),
                new DataColumn("选定课程", Type.GetType("System.String")),
                new DataColumn("发卷套题名称", Type.GetType("System.String")),
                new DataColumn("发卷套题类型", Type.GetType("System.String")),
                new DataColumn("发卷套题编号", Type.GetType("System.String"))
            };
            dt.Columns.AddRange(dc);
            // 以上代码完成了DataTable的构架，但是里面是没有任何数据的
            for (int i = 0; i < 10; i++)
            {
                DataRow dr = dt.NewRow();
                dr["学生学号"] = "20180302138";
                dr["学生姓名"] = "温";
                dr["所属专业"] = "计算机网络技术";
                dr["选定课程"] = "测试课程1";
                dr["发卷套题名称"] = "套题名称1";
                dr["发卷套题类型"] = "AB卷";
                dr["发卷套题编号"] = "1";
                dt.Rows.Add(dr);
            }
            dgv.DataSource = null;
            BindingSource bs = new BindingSource(dt, null);
            // bindingnavigator控件绑定数据源
            bn.BindingSource = bs;
            // dgv控件绑定数据源
            dgv.DataSource = bs;
            dgv.AutoGenerateColumns = false;// 取消连接数据库后的自动创建列
            dgv.Font = new Font("GB2312", 11);
        }

        /// <summary>
        /// 添加数据到发卷的table表
        /// </summary>
        /// <param name="ID">第一个字段ID，int类型</param>
        /// <param name="lst">第2~6个字段，string类型</param>
        public void AddOffajuan(int ID, List<string> lst)
        {
            DataRow dr = dt.NewRow();
            dr["学生学号"] = ID;
            dr["学生姓名"] = lst[0];
            dr["所属专业"] = lst[1];
            dr["选定课程"] = lst[2];
            dr["发卷套题名称"] = lst[3];
            dr["发卷套题类型"] = lst[4];
            dr["发卷套题编号"] = lst[5];
            dt.Rows.Add(dr);

        }

        #endregion

        #region 考生的dgv控件添加列标题

        /// <summary>
        /// dgv控件添加列标题
        /// </summary>
        /// <param name="dgv">要添加列标题的控件</param>
        public void DGVStyleOfkaosheng(DataGridView dgv)
        {
            dgv.AutoGenerateColumns = false;// 取消连接数据库后的自动创建列
            DataGridViewTextBoxColumn ID = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn Name = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn Sex = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn Grade = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn Profession_name = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn Department = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn ClassName = new DataGridViewTextBoxColumn();

            ID.Name = "ID";
            ID.DataPropertyName = "ID";
            ID.HeaderText = "学生学号";

            Name.Name = "Name";
            Name.DataPropertyName = "Name";
            Name.HeaderText = "学生姓名";

            Sex.Name = "Sex";
            Sex.DataPropertyName = "Sex";
            Sex.HeaderText = "学生性别";

            Grade.Name = "Grade";
            Grade.DataPropertyName = "Grade";
            Grade.HeaderText = "学生年级";

            Profession_name.Name = "Profession_name";
            Profession_name.DataPropertyName = "Profession_name";
            Profession_name.HeaderText = "专  业";

            Department.Name = "Department";
            Department.DataPropertyName = "Department";
            Department.HeaderText = "院  系";

            ClassName.Name = "ClassName";
            ClassName.DataPropertyName = "ClassName";
            ClassName.HeaderText = "班  级";

            dgv.Columns.Clear();// 清空列表头

            // 数组的形式添加列标题
            dgv.Columns.AddRange(ID, Name, Sex, Grade, Profession_name, Department, ClassName);
            dgv.Font = new Font("GB2312", 11);
        }
        
        #endregion
    }
}

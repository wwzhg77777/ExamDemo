using DataGridViewAutoFilter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BLL
{
    public class ExamineeManMethod
    {
        #region 变量
        /// <summary>
        /// 由该方法组生成的公共Table表
        /// </summary>
        public static DataTable dt;
        /// <summary>
        /// 列表框组
        /// </summary>
        private static ComboBox[] combos = null;
        /// <summary>
        /// 控制父窗体的Scroll
        /// </summary>
        private static FlowLayoutPanel flowlayoupanelcontrol;
        /// <summary>
        /// 控制父窗体的Scroll
        /// </summary>
        private static DataGridView dgvcontrol;
        /// <summary>
        /// 记录上一次操作的comboBox.text
        /// </summary>
        private static string Lasttimestr = "";
        /// <summary>
        /// 记录要导入的数据表名称
        /// </summary>
        public string _tbname;

        private static string[] CombosArr;
        #endregion

        #region 构造函数
        /// <summary>
        /// 该方法组的构造函数，查询Table表用于后续操作
        /// </summary>
        /// <param name="tbname"></param>
        public ExamineeManMethod(string tbname)
        {
            if (tbname == null) return;
            _tbname = tbname;
            string sql = string.Format("select * from `{0}`", tbname);
            dt = DAL.MySqlDBTool.MySqlCodeByDT(sql);// 实例化Table表，可进行筛选数据
        }

        #endregion

        #region 发卷的dgv控件添加数据表

        /// <summary>
        /// dgv控件添加列标题
        /// </summary>
        /// <param name="dgv">要添加列标题的控件</param>
        /// <param name="bn">导航栏</param>
        public void DGVStyleTofajuan(DataGridView dgv)
        {
            dgv.AutoGenerateColumns = false;// 取消连接数据库后的自动创建列
            DataGridViewTextBoxColumn StuID = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn Name = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn ofProfession = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn ClassName = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn ChoiceLesson = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn Taoti_name = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn Taoti_type = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn ABjuanMode = new DataGridViewTextBoxColumn();

            StuID.Name = "StuID";
            StuID.DataPropertyName = "StuID";
            StuID.HeaderText = "学号";

            Name.Name = "Name";
            Name.DataPropertyName = "Name";
            Name.HeaderText = "姓名";

            ofProfession.Name = "ofProfession";
            ofProfession.DataPropertyName = "ofProfession";
            ofProfession.HeaderText = "所属专业";

            ClassName.Name = "ClassName";
            ClassName.DataPropertyName = "ClassName";
            ClassName.HeaderText = "班级名称";

            ChoiceLesson.Name = "ChoiceLesson";
            ChoiceLesson.DataPropertyName = "ChoiceLesson";
            ChoiceLesson.HeaderText = "选定课程";

            Taoti_name.Name = "Taoti_name";
            Taoti_name.DataPropertyName = "Taoti_name";
            Taoti_name.HeaderText = "发卷套题名称";

            Taoti_type.Name = "Taoti_type";
            Taoti_type.DataPropertyName = "Taoti_type";
            Taoti_type.HeaderText = "发卷套题类型";

            ABjuanMode.Name = "ABjuanMode";
            ABjuanMode.DataPropertyName = "ABjuanMode";
            ABjuanMode.HeaderText = "指定AB卷";

            dgv.Columns.Clear();// 清空列表头

            // 数组的形式添加列标题
            dgv.Columns.AddRange(StuID, Name, ofProfession,ClassName, ChoiceLesson, Taoti_name, Taoti_type, ABjuanMode);
            dgv.Font = new Font("GB2312", 11);
        }

        /// <summary>
        /// 添加数据到发卷的table表
        /// </summary>
        /// <param name="ID">第一个字段ID，int类型</param>
        /// <param name="lst">第2~6个字段，string类型</param>
        public void AddTofajuan(int ID, List<string> lst, DataGridView dgv)
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
            DataGridViewTextBoxColumn StuID = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn Name = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn Sex = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn Grade = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn Profession_name = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn Department = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn ClassName = new DataGridViewTextBoxColumn();

            ID.Name = "ID";
            ID.DataPropertyName = "ID";
            ID.HeaderText = "编号";

            StuID.Name = "StuID";
            StuID.DataPropertyName = "StuID";
            StuID.HeaderText = "学号";

            Name.Name = "Name";
            Name.DataPropertyName = "Name";
            Name.HeaderText = "姓名";

            Sex.Name = "Sex";
            Sex.DataPropertyName = "Sex";
            Sex.HeaderText = "性别";

            Grade.Name = "Grade";
            Grade.DataPropertyName = "Grade";
            Grade.HeaderText = "年级";

            Profession_name.Name = "Profession_name";
            Profession_name.DataPropertyName = "Profession_name";
            Profession_name.HeaderText = "专业名称";

            Department.Name = "Department";
            Department.DataPropertyName = "Department";
            Department.HeaderText = "院系名称";

            ClassName.Name = "ClassName";
            ClassName.DataPropertyName = "ClassName";
            ClassName.HeaderText = "班级名称";

            dgv.Columns.Clear();// 清空列表头

            // 数组的形式添加列标题
            dgv.Columns.AddRange(ID, StuID, Name, Sex, Grade, Profession_name, Department, ClassName);
            dgv.Font = new Font("GB2312", 11);
        }

        #endregion

        #region 发卷_考生管理的表内筛选数据

        /// <summary>
        /// DataTable表内筛选数据
        /// <para>返回BindingSource(封装窗体的数据源)</para>
        /// </summary>
        /// <param name="dic">查询条件（字段名和数据）。默认为null值，即无条件查询。当有多个条件时，各条件之间是与(and)关系</param>
        /// <param name="dt">原DataTable</param>
        /// <param name="sw">计时器</param>
        /// <param name="tsl">记录耗时</param>
        /// <param name="bn">导航栏</param>
        /// <returns></returns>
        public static BindingSource SelectToDataTable(Dictionary<string, string> dic, DataTable dt, Stopwatch sw, ToolStripLabel tsl, BindingNavigator bn)
        {
            if (dic.Count > 0)
            {
                StringBuilder sb = new StringBuilder(Model.ExamInfo.BufferSize);
                sb.AppendFormat("{0} = '{1}' ", dic.Keys.ToList()[0], dic.Values.ToList()[0]);
                // 添加条件
                if (dic != null && dic.Count > 0)
                {// Department = '计算机系' and Grade = '2018'
                 // 第一个条件
                    for (int i = 1; i < dic.Count; i++)
                        sb.AppendFormat("AND {0} = '{1}' ", dic.Keys.ToList()[i], dic.Values.ToList()[i]);
                }
                MessageBox.Show(sb.ToString());

                DataRow[] drs = dt.Select(sb.ToString());
                sw.Restart();
                DataTable newdt = drs.CopyToDataTable();
                sw.Stop();
                tsl.Text = string.Format("执行此查询操作耗时： {0} 秒", sw.Elapsed.TotalSeconds);
                BindingSource bs = new BindingSource(newdt, null);
                // 绑定数据源
                bn.BindingSource = bs;
                return bs;
            }
            else
            {
                BindingSource bs = new BindingSource(dt, null);
                // 绑定数据源
                bn.BindingSource = bs;
                return bs;
            }
        }

        #endregion

        #region Excel表格导入数据

        #region 发卷_题库管理的dgv控件添加列标题

        /// <summary>
        /// 添加列标题
        /// </summary>
        /// <param name="dgv">要显示的数据表</param>
        public static DataTable DataGridViewStyle(DataGridView dgv)
        {
            dt = new DataTable("DT");
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
            // dgv控件绑定数据源
            BindingSource bs = new BindingSource(dt, null);
            dgv.DataSource = bs;
            dgv.AutoGenerateColumns = false;// 取消连接数据库后的自动创建列
            dgv.Font = new Font("GB2312", 11);
            return dt;
        }

        #endregion

        #region 批量创建ComboBox控件并添加到dgv控件
        /// <summary> 
        /// 根据读取的Excel表格列数创建列表框
        /// </summary>
        /// <param name="ParentControl">该控件组的父容器</param>
        /// <param name="dgv">要填充Excel表格数据的dgv控件</param>
        /// <param name="dt">储存Excel表格数据的DataTable</param>
        /// <param name="ColsCount">读取Excel表格列的数量</param>
        /// <param name="ColsName">示例数据的列名称</param>
        public void CreateArrToDGV(Control ParentControl, DataGridView dgv, int ColsCount, string[] ColsName)
        {
            combos = new ComboBox[ColsCount];
            for (int i = ColsCount - 1; i > -1; i--)// 设置ComboBox控件的属性  
            {
                // 实例化
                combos[i] = new ComboBox();
                // 定义控件名称
                combos[i].Name = i.ToString();
                // 定义字体大小
                combos[i].Font = new Font("宋体", 13f);
                combos[i].Size = new Size(100, 25);
                // 定义控件外间距
                combos[i].Margin = new Padding(0);
                // 定义编辑模式
                combos[i].DropDownStyle = ComboBoxStyle.DropDownList;

                // 定义item属性，可以用string数组初始化为指定值
                combos[i].Items.Add("(空)");
                combos[i].Items.AddRange(ColsName);
                CombosArr = ColsName;
                // 注：如果不指定父容器，则坐标是相对于主窗体的
                combos[i].Parent = ParentControl;
                // 添加控件
                //ParentControl.Controls.Add(combos[i]);
                // 置于图层顶层
                combos[i].BringToFront();
                // 批量添加事件
                combos[i].SelectionChangeCommitted += combos_SelectionChangeCommitted;
                combos[i].DropDown += combos_DropDown;
            }
            // 添加事件
            dgv.ColumnWidthChanged += Dgv_ColumnWidthChanged;
            ((FlowLayoutPanel)ParentControl).Scroll += FlowLayoutPanel_Scroll;
            dgv.Scroll += Dgv_Scroll;
            dgvcontrol = dgv;
            flowlayoupanelcontrol = (FlowLayoutPanel)ParentControl;
        }

        #region 动态删除已选中的项和恢复未选中的项
        private static void combos_DropDown(object sender, EventArgs e)
        {
            Lasttimestr = ((ComboBox)sender).Text;
        }

        private static void combos_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (((ComboBox)sender).Text != "(空)")
            {
                List<string> lst = new List<string>();
                lst.Add("(空)");
                lst.Add(((ComboBox)sender).Text);
                string[] lstarr = lst.ToArray();

                ((ComboBox)sender).Items.Clear();
                ((ComboBox)sender).Items.AddRange(lstarr);
                ((ComboBox)sender).SelectedItem = lstarr[1];
                foreach (var item in combos)
                {
                    if (item.Name != ((ComboBox)sender).Name)
                        if (item.Items.Contains(((ComboBox)sender).Text))
                            item.Items.Remove(((ComboBox)sender).Text);
                }
            }
            else if (((ComboBox)sender).Text == "(空)")
            {
                List<string> lst = new List<string>();
                lst.Add("(空)");
                lst.AddRange(CombosArr);
                foreach (var item in combos)
                {
                    if (item.Text != "" && item.Text != "(空)")
                        if (lst.Contains(item.Text))
                            lst.Remove(item.Text);
                }
                string[] lstarr = lst.ToArray();
                if (((ComboBox)sender).Items.Count != 1)
                {
                    ((ComboBox)sender).Items.Clear();
                    ((ComboBox)sender).Items.AddRange(lstarr);
                    ((ComboBox)sender).SelectedItem = lstarr[0];
                }
                foreach (var item in combos)
                {
                    if (item.Name != ((ComboBox)sender).Name && Lasttimestr != "")
                        if (!item.Items.Contains(Lasttimestr))
                            item.Items.Add(Lasttimestr);
                }
            }

        }
        #endregion 

        #region 同步滚动条
        private static void FlowLayoutPanel_Scroll(object sender, ScrollEventArgs e)
        {
            dgvcontrol.HorizontalScrollingOffset = e.NewValue;
        }

        private static void Dgv_Scroll(object sender, ScrollEventArgs e)
        {
            flowlayoupanelcontrol.HorizontalScroll.Value = e.NewValue;
        }

        #endregion

        #region 同步ComboBox控件与dgv控件Cell的宽度
        private static void Dgv_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            combos[e.Column.Index].Size = e.Column.HeaderCell.Size;
        }

        #endregion

        #endregion

        #endregion

        #region dgv转dt
        /// <summary>
        /// dgv转dt
        /// </summary>
        /// <param name="dgv"></param>
        /// <returns></returns>
        public DataTable GetDgvToTable(DataGridView dgv)
        {
            DataTable dt = new DataTable();

            // 列强制转换
            for (int count = 0; count < dgv.Columns.Count; count++)
            {
                DataColumn dc = new DataColumn(dgv.Columns[count].Name.ToString());
                dt.Columns.Add(dc);
            }

            // 循环行
            for (int count = 0; count < dgv.Rows.Count; count++)
            {
                DataRow dr = dt.NewRow();
                for (int countsub = 0; countsub < dgv.Columns.Count; countsub++)
                {
                    dr[countsub] = Convert.ToString(dgv.Rows[count].Cells[countsub].Value);
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }
        #endregion
    }
}

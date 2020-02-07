﻿using System;
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

namespace DAL     //数据访问层
{
    public class DAO
    {

        #region 题库管理_数据库操作

        /// <summary>
        /// 添加列标题
        /// </summary>
        /// <param name="dgv">要显示的数据表</param>
        public static void DataGridViewStyle(DataGridView dgv)
        {
            dgv.AutoGenerateColumns = false;// 取消连接数据库后的自动创建列
            DataGridViewTextBoxColumn ID = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn Name = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn Type = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn LessonID = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn JoinTime = new DataGridViewTextBoxColumn();

            ID.Name = "ID";
            ID.DataPropertyName = "ID";
            ID.HeaderText = "套题编号";
            ID.FillWeight = 30;

            Name.Name = "Name";
            Name.DataPropertyName = "Name";
            Name.HeaderText = "套题名称";

            Type.Name = "Type";
            Type.DataPropertyName = "Type";
            Type.HeaderText = "套题类型";
            Type.FillWeight = 40;

            LessonID.Name = "LessonID";
            LessonID.DataPropertyName = "LessonID";
            LessonID.HeaderText = "所属课程";

            JoinTime.Name = "JoinTime";
            JoinTime.DataPropertyName = "JoinTime";
            JoinTime.HeaderText = "添加时间";
            JoinTime.FillWeight = 40;

            dgv.Columns.Clear();// 清空列表头

            // 数组的形式添加列标题
            dgv.Columns.AddRange(ID, Name,Type, LessonID, JoinTime);
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
        /// 插入数据
        /// </summary>
        /// <param name="dbtool"></param>
        /// <param name="UserDatas"></param>
        /// <returns></returns>
        public static string SqlInsert(string tbname,params TextBox[] UserDatas)
        {
            string Insertsql = "INSERT INTO `"+tbname+"` (`ID`, `Name`, `MD5_PWD`, `PWD`, `JoinTime`) ";
            Insertsql += "VALUES (NULL, ";
            Insertsql += "'" + UserDatas[0].Text.Trim() + "',";
            Insertsql += "'" + Method.GetMd5(UserDatas[1].Text.Trim()) + "',";
            Insertsql += "'" + UserDatas[1].Text.Trim() + "',";
            Insertsql += "'" + UserDatas[2].Text.Trim() + "') ";
            return Insertsql;
        }
        #endregion
    }

}
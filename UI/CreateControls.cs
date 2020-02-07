using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI
{
    /// <summary>
    /// 指定常数操作控件所在图层
    /// </summary>
    public enum TopOfFrm
    {
        /// <summary>
        /// 不操作
        /// </summary>
        None = 0,
        /// <summary>
        /// 置于顶层
        /// </summary>
        Top = 1,
        /// <summary>
        /// 置于底层
        /// </summary>
        Bottom = 2
    }

    public class CreateControls
    {
        #region 创建控件对象

        // 创建dataGridView控件
        private static System.Windows.Forms.DataGridView DGV1;
        #endregion

        #region DGV1控件
        /// <summary>
        /// 创建控件到窗体
        /// </summary>
        /// <param name="ActiveFrm">窗体</param>
        /// <param name="topoffrm">指定图层顺序</param>
        /// <param name="ds">指定控件的位置</param>
        public static void CreateDGV1(Form ActiveFrm, TopOfFrm topoffrm, DockStyle ds)
        {
            // 实例化控件对象
            DGV1 = new System.Windows.Forms.DataGridView();

            // 初始化控件
            ((System.ComponentModel.ISupportInitialize)(DGV1)).BeginInit();

            // 添加控件到窗体
            ActiveFrm.Controls.Add(DGV1);

            // 设置dataGridView1控件属性
            DGV1.AllowUserToAddRows = false;
            DGV1.AllowUserToDeleteRows = false;
            DGV1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            DGV1.ColumnHeadersHeight = 30;
            DGV1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            DGV1.GridColor = System.Drawing.SystemColors.ActiveCaptionText;
            DGV1.Location = new System.Drawing.Point(150, 150);
            DGV1.Dock = ds;
            DGV1.Name = "DGV1";
            DGV1.RowTemplate.Height = 27;
            DGV1.Size = new System.Drawing.Size(50, 50);
            DGV1.TabIndex = 0;

            // 初始化控件完毕
            ((System.ComponentModel.ISupportInitialize)(DGV1)).EndInit();

            switch (topoffrm)
            {
                case TopOfFrm.None:
                    // 不操作;
                    break;
                case TopOfFrm.Top:
                    // 增加Z顺序（图层）
                    DGV1.BringToFront();
                    break;
                case TopOfFrm.Bottom:
                    // 减少Z顺序（图层）
                    DGV1.SendToBack();
                    break;
                default:
                    break;
            }
        }
        #endregion
        
    }
    
}

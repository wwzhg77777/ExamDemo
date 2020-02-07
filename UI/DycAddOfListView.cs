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
    public class DycAddOfListView
    {
        #region AB卷批量添加PictureBox控件

        /// <summary>
        /// 动态添加PictureBox控件
        /// </summary>
        /// <param name="c">要添加的控件容器</param>
        public static void CreatePicAB(Control c)
        {
            // 实例化控件对象
            PictureBox PicA = new PictureBox();

            // 初始化控件
            ((System.ComponentModel.ISupportInitialize)PicA).BeginInit();

            // 设置Pic控件属性
            PicA.BorderStyle = BorderStyle.FixedSingle;
            PicA.Cursor = Cursors.Hand;
            PicA.Image = Properties.Resources.AddSubA;
            PicA.Margin = new Padding(10, 10, 0, 0);
            PicA.Name = "PicA";
            PicA.Size = new Size(100, 100);
            PicA.SizeMode = PictureBoxSizeMode.StretchImage;
            PicA.TabStop = false;

            // 添加单击事件
            PicA.Click += new EventHandler(PicA_Click);

            // 添加控件到窗体
            c.Controls.Add(PicA);

            // 初始化控件完毕
            ((System.ComponentModel.ISupportInitialize)PicA).EndInit();

            // 实例化控件对象
            PictureBox PicB = new PictureBox();

            // 初始化控件
            ((System.ComponentModel.ISupportInitialize)PicB).BeginInit();

            // 设置Pic控件属性
            PicB.BorderStyle = BorderStyle.FixedSingle;
            PicB.Cursor = Cursors.Hand;
            PicB.Image = Properties.Resources.AddSubB;
            PicB.Margin = new Padding(10, 10, 0, 0);
            PicB.Name = "PicB";
            PicB.Size = new Size(100, 100);
            PicB.SizeMode = PictureBoxSizeMode.StretchImage;
            PicB.TabStop = false;

            // 添加单击事件
            PicB.Click += new EventHandler(PicB_Click);

            // 添加控件到窗体
            c.Controls.Add(PicB);

            // 初始化控件完毕
            ((System.ComponentModel.ISupportInitialize)PicB).EndInit();
        }
        #endregion

        #region AB卷单击事件
        private static void PicA_Click(object sender, EventArgs e)
        {
            // 打开文件
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "Word文件| *.doc*";
            file.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            file.Multiselect = false;
            if (file.ShowDialog() == DialogResult.Cancel)
                return;

            //判断文件后缀
            var path = file.FileName;
            string filesuffix = Path.GetExtension(path);
            if (string.IsNullOrEmpty(filesuffix))
                return;

        }

        private static void PicB_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region 学号卷批量添加PictureBox控件

        /// <summary>
        /// 动态添加PictureBox控件
        /// </summary>
        /// <param name="c">要添加的控件容器</param>
        /// <param name="max">添加数量</param>
        public static void CreatePicID(Control c, int max)
        {
            for (int i = 0; i < max; i++)
            {
                // 实例化控件对象
                PictureBox Pic = new PictureBox();

                // 初始化控件
                ((System.ComponentModel.ISupportInitialize)Pic).BeginInit();

                // 设置Pic控件属性
                Pic.BorderStyle = BorderStyle.FixedSingle;
                Pic.Cursor = Cursors.Hand;
                Pic.Image = Properties.Resources.AddSub;
                Pic.Margin = new Padding(10, 10, 0, 0);
                Pic.Name = "Pic" + i;
                Pic.Size = new Size(100, 100);
                Pic.SizeMode = PictureBoxSizeMode.StretchImage;
                Pic.TabStop = false;
                // 添加控件到窗体
                c.Controls.Add(Pic);
                // 初始化控件完毕
                ((System.ComponentModel.ISupportInitialize)Pic).EndInit();
            }
        }
        #endregion
    }
}

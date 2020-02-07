using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BLL
{
    public class Method
    {
        #region 窗体边框阴影效果
        const int CS_DropSHADOW = 0x20000;
        const int GCL_STYLE = (-26);

        public static void ShadowEffect(IntPtr hwnd)
        {
            //API函数加载，实现窗体边框阴影效果
            Win32.SetClassLong(hwnd, GCL_STYLE, Win32.GetClassLong(hwnd, GCL_STYLE) | CS_DropSHADOW);
        }
        #endregion

        #region ListView图标间距
        const int LVM_FIRST = 0x1000;
        const int LVM_SETICONSPACING = LVM_FIRST + 53;

        public static void SetListViewSpacing(ListView lst, int x, int y)
        {
            Win32.SendMessage(lst.Handle, LVM_SETICONSPACING, 0, x * 65536 + y);
        }

        #endregion

        #region 获取数据并添加到 List 强类型链表内
        public static List<string> GetLt(params TextBox[] Arrays)
        {
            List<string> _List = (new string[]
            {
                Arrays[0].Text.Trim(),
                Arrays[1].Text.Trim()
            }).ToList();
            return _List;
        }
        #endregion

        #region 控制控件未响应时的文字颜色
        const int GWL_STYLE = -16;
        const int WS_DISABLED = 0x8000000;

        public static void SetControlEnabled(Control c, bool enabled, Color forecolor)
        {
            if (enabled)
            { Win32.SetWindowLong(c.Handle, GWL_STYLE, (~WS_DISABLED) & Win32.GetWindowLong(c.Handle, GWL_STYLE)); }
            else
            {
                Win32.SetWindowLong(c.Handle, GWL_STYLE, WS_DISABLED + Win32.GetWindowLong(c.Handle, GWL_STYLE));
                c.ForeColor = forecolor;
            }
        }
        #endregion

        #region 根据文件路径获取图标
        [StructLayout(LayoutKind.Sequential)]
        public struct SHFILEINFO
        {
            public IntPtr hIcon;
            public IntPtr iIcon;
            public uint dwAttributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;
        };

        [DllImport("shell32.dll")]
        public static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, uint cbSizeFileInfo, uint uFlags);
        public const uint SHGFI_ICON = 0x100;
        public const uint SHGFI_LARGEICON = 0x0;
        public const uint SHGFI_SMALLICON = 0x1;

        /// <summary>
        /// 根据文件路径获取小图标
        /// </summary>
        /// <param name="fileName">文件路径(例如：F:\,F:\Images,F:\Images\Bg.jpg)</param>
        /// <returns>Icon图标</returns>
        public static Icon GetSmallIcon(string fileName)
        {
            IntPtr hImgSmall;
            SHFILEINFO shinfo = new SHFILEINFO();

            hImgSmall = SHGetFileInfo(fileName, 0, ref shinfo, (uint)Marshal.SizeOf(shinfo), SHGFI_ICON | SHGFI_SMALLICON);

            return System.Drawing.Icon.FromHandle(shinfo.hIcon);
        }

        /// <summary>
        /// 根据文件路径获取大图标
        /// </summary>
        /// <param name="fileName">文件路径(例如：F:\,F:\Images,F:\Images\Bg.jpg)</param>
        /// <returns>Icon图标</returns>
        public static Icon GetLargeIcon(string fileName)
        {
            IntPtr hImgLarge; // list
          SHFILEINFO shinfo = new SHFILEINFO();

            hImgLarge = SHGetFileInfo(fileName, 0, ref shinfo, (uint)Marshal.SizeOf(shinfo), SHGFI_ICON | SHGFI_LARGEICON);
            return System.Drawing.Icon.FromHandle(shinfo.hIcon);
        }

        /// <summary>
        /// 从文件扩展名得到文件关联图标
        /// </summary>
        /// <param name="fileName">文件名或文件扩展名</param>
        /// <param name="smallIcon">是否是获取小图标，否则是大图标</param>
        /// <returns>Icon图标</returns>
        static public Icon GetFileIcon(string fileName, bool smallIcon)
        {
            SHFILEINFO fi = new SHFILEINFO();
            Icon ic = null;

            int iTotal = (int)SHGetFileInfo(fileName, 100, ref fi, 0, (uint)(smallIcon ? 273 : 272));
            if (iTotal > 0)
            {
                ic = Icon.FromHandle(fi.hIcon);
            }
            return ic;
        }
        #endregion
    }
    #region ListView获取文件图标

    public class FileInfoList
    {
        public List<FileInfoWithIcon> list;
        public ImageList imageListLargeIcon;
        public ImageList imageListSmallIcon;

        /// <summary>
        /// 根据文件路径获取生成文件信息，并提取文件的图标
        /// </summary>
        /// <param name="filespath"></param>
        public FileInfoList(string[] filespath)
        {
            list = new List<FileInfoWithIcon>();
            imageListLargeIcon = new ImageList();
            imageListLargeIcon.ImageSize = new Size(32, 32);
            imageListSmallIcon = new ImageList();
            imageListSmallIcon.ImageSize = new Size(16, 16);
            foreach (string path in filespath)
            {
                FileInfoWithIcon file = new FileInfoWithIcon(path);
                imageListLargeIcon.Images.Add(file.largeIcon);
                imageListSmallIcon.Images.Add(file.smallIcon);
                file.iconIndex = imageListLargeIcon.Images.Count - 1;
                list.Add(file);
            }
        }
    }

    public class FileInfoWithIcon
    {
        public FileInfo fileInfo;
        public Icon largeIcon;
        public Icon smallIcon;
        public int iconIndex;
        public FileInfoWithIcon(string path)
        {
            fileInfo = new FileInfo(path);
            largeIcon = GetSystemIcon.GetIconByFileName(path, true);
            if (largeIcon == null)
                largeIcon = GetSystemIcon.GetIconByFileType(Path.GetExtension(path), true);

            smallIcon = GetSystemIcon.GetIconByFileName(path, false);
            if (smallIcon == null)
                smallIcon = GetSystemIcon.GetIconByFileType(Path.GetExtension(path), false);
        }
    }

    /*
     
    GetSystemIcon：
    说明：定义两种图标获取方式，从文件提取和从文件关联的系统资源中提取。

     */

    public static class GetSystemIcon
    {
        /// <summary>
        /// 依据文件名读取图标，若指定文件不存在，则返回空值。  
        /// </summary>
        /// <param name="fileName">文件路径</param>
        /// <param name="isLarge">是否返回大图标</param>
        /// <returns></returns>
        public static Icon GetIconByFileName(string fileName, bool isLarge = true)
        {
            int[] phiconLarge = new int[1];
            int[] phiconSmall = new int[1];
            //文件名 图标索引 
            Win32.ExtractIconEx(fileName, 0, phiconLarge, phiconSmall, 1);
            IntPtr IconHnd = new IntPtr(isLarge ? phiconLarge[0] : phiconSmall[0]);

            if (IconHnd.ToString() == "0")
                return null;
            return Icon.FromHandle(IconHnd);
        }


        /// <summary>  
        /// 根据文件扩展名（如:.*），返回与之关联的图标。
        /// 若不以"."开头则返回文件夹的图标。  
        /// </summary>  
        /// <param name="fileType">文件扩展名</param>  
        /// <param name="isLarge">是否返回大图标</param>  
        /// <returns></returns>  
        public static Icon GetIconByFileType(string fileType, bool isLarge)
        {
            if (fileType == null || fileType.Equals(string.Empty)) return null;


            RegistryKey regVersion = null;
            string regFileType = null;
            string regIconString = null;
            string systemDirectory = Environment.SystemDirectory + "\\";


            if (fileType[0] == '.')
            {
                //读系统注册表中文件类型信息  
                regVersion = Registry.ClassesRoot.OpenSubKey(fileType, false);
                if (regVersion != null)
                {
                    regFileType = regVersion.GetValue("") as string;
                    regVersion.Close();
                    regVersion = Registry.ClassesRoot.OpenSubKey(regFileType + @"\DefaultIcon", false);
                    if (regVersion != null)
                    {
                        regIconString = regVersion.GetValue("") as string;
                        regVersion.Close();
                    }
                }
                if (regIconString == null)
                {
                    // 没有读取到文件类型注册信息，指定为未知文件类型的图标  
                    regIconString = systemDirectory + "shell32.dll,0";
                }
            }
            else
            {
                // 直接指定为文件夹图标  
                regIconString = systemDirectory + "shell32.dll,3";
            }
            string[] fileIcon = regIconString.Split(new char[] { ',' });
            if (fileIcon.Length != 2)
            {
                // 系统注册表中注册的标图不能直接提取，则返回可执行文件的通用图标  
                fileIcon = new string[] { systemDirectory + "shell32.dll", "2" };
            }
            Icon resultIcon = null;
            try
            {
                // 调用API方法读取图标  
                int[] phiconLarge = new int[1];
                int[] phiconSmall = new int[1];
                uint count = Win32.ExtractIconEx(fileIcon[0], Int32.Parse(fileIcon[1]), phiconLarge, phiconSmall, 1);
                IntPtr IconHnd = new IntPtr(isLarge ? phiconLarge[0] : phiconSmall[0]);
                resultIcon = Icon.FromHandle(IconHnd);
            }
            catch { }
            return resultIcon;
        }
    }
    #endregion

    #region ListView获取文件图标实例：
    /*
     Form1.cs：
public partial class Form1 : Form
    {
        FileInfoList fileList;


        public Form1()
        {
            InitializeComponent();
        }


        private void 加载文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string[] filespath = Directory.GetFiles(dlg.SelectedPath);
                fileList = new FileInfoList(filespath);
                InitListView();
            }
        }


        private void InitListView()
        {
            listView1.Items.Clear();
            this.listView1.BeginUpdate();
            foreach (FileInfoWithIcon file in fileList.list)
            {
                ListViewItem item = new ListViewItem();
                item.Text = file.fileInfo.Name.Split('.')[0];
                item.ImageIndex = file.iconIndex;
                item.SubItems.Add(file.fileInfo.LastWriteTime.ToString());
                item.SubItems.Add(file.fileInfo.Extension.Replace(".",""));
                item.SubItems.Add(string.Format(("{0:N0}"), file.fileInfo.Length));
                listView1.Items.Add(item);
            }
            listView1.LargeImageList = fileList.imageListLargeIcon;
            listView1.SmallImageList = fileList.imageListSmallIcon;
            listView1.Show();
            this.listView1.EndUpdate();
        }


        private void 大图标ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.View = View.LargeIcon;
        }


        private void 小图标ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.View = View.SmallIcon;
        }


        private void 平铺ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.View = View.Tile;
        }


        private void 列表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.View = View.List;
        }


        private void 详细信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.View = View.Details;
        }
    } 
     */
    #endregion
}
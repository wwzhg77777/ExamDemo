using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    /// <summary>  
    /// 定义调用的API方法  
    /// </summary>  
    public class Win32
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SetClassLong(IntPtr hwnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetClassLong(IntPtr hwnd, int nIndex);

        [DllImport("user32.dll ")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int wndproc);

        [DllImport("user32.dll ")]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        /// <summary>
        /// 控制控件子项之间的距离
        /// </summary>
        /// <param name="Handle"></param>
        /// <param name="wMsg"></param>
        /// <param name="wParam">水平间距</param>
        /// <param name="lParam">垂直间距</param>
        /// <returns></returns>
        [DllImport("User32.dll")]
        public static extern int SendMessage(IntPtr Handle, int wMsg, int wParam, int lParam);

        [DllImport("shell32.dll")]
        public static extern uint ExtractIconEx(string lpszFile, int nIconIndex, int[] phiconLarge, int[] phiconSmall, uint nIcons);

    }
}

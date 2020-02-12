using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI
{
    public partial class MainFrm : Form
    {
        ExamMainTea m1;
        public MainFrm(ExamMainTea mm)
        {
            InitializeComponent();
            m1 = mm;
        }

        private void MainFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            BLL.KEY.MainFrmkey = "";
        }

        #region 加载窗体事件
        private void MainFrm_Load(object sender, EventArgs e)
        {
            BLL.KEY.MainFrmkey = "1";
            listView1.Items.Add("课程 / 院系 / 专业 管理");
            listView1.Items.Add("题库管理");
            listView1.Items.Add("考生管理");
            listView1.Items.Add("抽题组卷");
            listView1.Items.Add("设  置");
            listView1.Items.Add("");
            listView1.LargeImageList = imageList1;
            for (int i = 0; i < 5; i++)
                listView1.Items[i].ImageIndex = i;
            BLL.Method.SetListViewSpacing(listView1, 260, 200);// 设置图标之间的间距        
        }
        #endregion

        #region 创建窗体
       public static fajuanFrm fF = null;// 发卷
      public static  ScoreManFrm SMF = null;
        #endregion

        private void fajuan(object sender, EventArgs e)
        {
            ExamMainTea.EMTFrm.BottomSidebar.Visible = true;
            if (BLL.KEY.fajuanFrmkey != "1")// 发卷窗体关闭状态
            {
                fF = new fajuanFrm(this);
                fF.MdiParent = ActiveForm;  // 使父窗体成为子窗体的MDI容器
                fF.Show();
                fF.WindowState = FormWindowState.Maximized;
            }
            else// 激活窗体
            {
                Login.BLL.TeaManager.ActiveFrm(fF);
            }
        }

        private void gaijuan(object sender, EventArgs e)
        {

        }

        private void fajuanCheck(object sender, EventArgs e)
        {

        }

        private void gaijuanCheck(object sender, EventArgs e)
        {

        }
        
        private void listView1_DoubleClick(object sender, EventArgs e)
        {

            #region 判断选中项
            switch (listView1.SelectedItems[0].ImageIndex)
            {
                case 0:// 课程管理/院系管理

                    if (BLL.KEY.ScoreManFrmkey != "1")// 发卷窗体关闭状态
                    {
                        SMF = new ScoreManFrm((ExamMainTea)ActiveForm);
                        SMF.MdiParent = ActiveForm;  // 使父窗体成为子窗体的MDI容器
                        SMF.Show();
                        SMF.WindowState = FormWindowState.Maximized;
                    }
                    else// 激活窗体
                    {
                        Login.BLL.TeaManager.ActiveFrm(SMF);
                    }
                    break;

                case 1:// 题库管理

                    break;
                case 2:// 考生管理


                    break;
                case 3:// 抽题组卷


                    break;
                case 4:// 设置


                    break;
                    #endregion
            }
        }
    }
}
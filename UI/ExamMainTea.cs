using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI
{
    public partial class ExamMainTea : Form
    {
        public delegate void Send();// 定义委托。  用于另一个窗体向此窗体传递信息
        public static event Send Mysend;// 定义事件

        public static void SendFunction()// 定义公开的静态方法，另一个窗体方法引用此方法时，触发mysend()方法
        {
            if (Mysend != null)// 判断另一个窗体是否引用此方法
            {
                Mysend();
            }
        }

        #region 创建窗体
        MainFrm MF = null;// 主页
        QuestionManFrm QMF = null;// 题库管理
        ExamineeManFrm EMF = null;// 考生管理
        TestPaperModeFrm TPMF = null;// 抽题组卷
        SettingFrm SF = null;// 设置
        About A = null;// 关于
        //ScoreManFrm SMF = null;// 课程管理

        #endregion 

        public ExamMainTea()
        {
            InitializeComponent();
            Mysend = _Mysend;/* 注册事件到"_send"方法。
                                注意： += 是累加， = 是赋值。
                                如果此窗体不是应用程序主窗体，会出现累加事件的bug
                                第一次事件绑定此窗体获取了值，第二次事件因为第一次事件占用了此窗体的资源
                                所以第二次事件没有绑定任何窗体，对应代码的值为null，引发System.ArgumentOutOfRangeException 类型异常
                                解决方案： 改为直接赋值  +=  --->  =
                              */
            // 设置实时更新的系统时间
            new System.Threading.Thread(() =>
            {
                while (true)
                {
                    try { label1.BeginInvoke(new MethodInvoker(() => label1.Text = DateTime.Now.ToString("yyyy年MM月dd日 HH时mm分ss"))); }
                    catch { }
                    System.Threading.Thread.Sleep(1000);
                }
            })
            { IsBackground = true }.Start();
        }

        private void _Mysend()//另一个窗体引用SendFunction方法触发Send()委托的事件mysend()，mysend()注册事件到_send()方法并执行_send()方法
        {
            if (BLL.KEY.MainFrmkey != "1")// 主页窗体关闭状态
            {
                MF = new MainFrm(this);
                MF.MdiParent = this;  // 使父窗体成为子窗体的MDI容器
                MF.Show();
                MF.WindowState = FormWindowState.Maximized;
            }
            else// 激活主页窗体
            {
                MF.Activate();
                MF.TopMost = true;
                MF.WindowState = FormWindowState.Maximized;
            }
        }

        private void menuStrip1_ItemAdded(object sender, ToolStripItemEventArgs e)
        {
            // || e.Item.Text == "关闭(&C)"
            if (e.Item.Text.Length == 0 || e.Item.Text == "还原(&R)" || e.Item.Text == "最小化(&N)" )//隐藏图标、最小化、最大化
            {
                e.Item.Visible = false;
            }
        }

        #region 窗体加载事件
        private void Main_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;// 窗口最大化
            ExamLoginTea mylogin = new ExamLoginTea();// 生成登录窗口
            mylogin.ShowDialog();// 调出登录窗口  Show()为显示窗体，不置顶。ShowDialog()为置顶窗体
            if (BLL.KEY.mLogin != "1")
            {
                Application.Exit();// 关闭应用程序
            }
            else if (BLL.KEY.mLogin == "1")// 打开主页窗体
            {
                MF = new MainFrm(this);
                MF.MdiParent = this;  // 使父窗体成为子窗体的MDI容器
                MF.Show();
                MF.WindowState = FormWindowState.Maximized;
            }
            Userlab.Text = Login.Model.TeaInfo.userlab;
            Powerlab.Text = Login.Model.TeaInfo.powerlab;
            Numberlab.Text = Login.Model.TeaInfo.numberlab;
        }
        #endregion

        #region 窗体激活事件
        private void 主页_Click(object sender, EventArgs e)
        {
            ExamMainTea.SendFunction();// 向Main窗体传递信息，触发委托事件
        }

        private void 题库管理_Click(object sender, EventArgs e)
        {
            if (BLL.KEY.QuestionManFrmkey != "1")// 题库窗体关闭状态
            {
                QMF = new QuestionManFrm(this);
                QMF.MdiParent = this;  // 使父窗体成为子窗体的MDI容器
                QMF.Show();
                QMF.WindowState = FormWindowState.Maximized;
            }
            else// 激活窗体
            {
                Login.BLL.TeaManager.ActiveFrm(QMF);
            }
        }

        private void About_Click(object sender, EventArgs e)
        {
            A = new About();
            A.ShowDialog();
        }

        private void 考生管理_Click(object sender, EventArgs e)
        {
            if (BLL.KEY.ExamineeManFrmkey != "1")// 考生窗体关闭状态
            {
                EMF = new ExamineeManFrm(this);
                EMF.MdiParent = this;  // 使父窗体成为子窗体的MDI容器
                EMF.Show();
                EMF.WindowState = FormWindowState.Maximized;
            }
            else// 激活窗体
            {
                Login.BLL.TeaManager.ActiveFrm(EMF);
            }
        }

        private void 抽题组卷_Click(object sender, EventArgs e)
        {
            if (BLL.KEY.TestPaperModeFrmkey != "1")// 组卷窗体关闭状态
            {
                TPMF = new TestPaperModeFrm(this);
                TPMF.MdiParent = this;  // 使父窗体成为子窗体的MDI容器
                TPMF.Show();
                TPMF.WindowState = FormWindowState.Maximized;
            }
            else// 激活窗体
            {
                Login.BLL.TeaManager.ActiveFrm(TPMF);
            }
        }

        private void 设置_Click(object sender, EventArgs e)
        {
            if (BLL.KEY.SettingFrmkey != "1")// 设置窗体关闭状态
            {
                SF = new SettingFrm(this);
                SF.MdiParent = this;  // 使父窗体成为子窗体的MDI容器
                SF.Show();
                SF.WindowState = FormWindowState.Maximized;
            }
            else// 激活窗体
            {
                Login.BLL.TeaManager.ActiveFrm(SF);
            }
        }
        #endregion

    }
}

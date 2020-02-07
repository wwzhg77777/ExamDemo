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
    public partial class QuestionManFrm : Form
    {
        ExamMainTea m1;
        public QuestionManFrm(ExamMainTea mm)
        {
            InitializeComponent();
            m1 = mm;
        }

        private void QuestionManFrm_Load(object sender, EventArgs e)
        {
            BLL.KEY.QuestionManFrmkey = "1";
        }

        private void QuestionManFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            BLL.KEY.QuestionManFrmkey = "";
        }
    }
}

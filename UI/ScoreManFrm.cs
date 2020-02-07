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
    public partial class ScoreManFrm : Form
    {
        ExamMainTea m1;
        public ScoreManFrm(ExamMainTea mm)
        {
            InitializeComponent();
            m1 = mm;
        }

        private void ScoreManFrm_Load(object sender, EventArgs e)
        {
            //BLL.KEY.ScoreManFrmkey = "1";
        }

        private void ScoreManFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //BLL.KEY.ScoreManFrmkey = "";
        }
    }
}

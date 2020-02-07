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
    public partial class TestPaperModeFrm : Form
    {
        ExamMainTea m1;
        public TestPaperModeFrm(ExamMainTea mm)
        {
            InitializeComponent();
            m1 = mm;
        }

        private void TestPaperModeFrm_Load(object sender, EventArgs e)
        {
            BLL.KEY.TestPaperModeFrmkey = "1";
        }

        private void TestPaperModeFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            BLL.KEY.TestPaperModeFrmkey = "";
        }
    }
}

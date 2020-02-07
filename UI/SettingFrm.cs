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
    public partial class SettingFrm : Form
    {
        ExamMainTea m1;
        public SettingFrm(ExamMainTea mm)
        {
            InitializeComponent();
            m1 = mm;
        }

        private void SettingFrm_Load(object sender, EventArgs e)
        {
            BLL.KEY.SettingFrmkey = "1";
        }

        private void SettingFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            BLL.KEY.SettingFrmkey = "";
        }
    }
}

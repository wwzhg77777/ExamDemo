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
    public partial class test : Form
    {
        public test()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<string> lst = new List<string>();
            for (int i = 0; i <= 10; i++)
            {
                lst.Add("test" + i);
                lst.Add("test" + i);
            }
            foreach (var item in lst)
            {
                listBox1.Items.Add(item);
            }
            List<string> newlst = lst.Distinct().ToList();
            foreach (var item in newlst)
            {
                listBox1.Items.Add(item);
            }
        }
    }
}

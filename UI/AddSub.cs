﻿using System;
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
    public partial class AddSub : Form
    {
        public AddSub()
        {
            InitializeComponent();
        }

        private void Return_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddSub_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
        }
    }
}

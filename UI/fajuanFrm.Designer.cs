namespace UI
{
    partial class fajuanFrm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnABMode = new System.Windows.Forms.Button();
            this.btnStuIDMode = new System.Windows.Forms.Button();
            this.btnTemplateMode = new System.Windows.Forms.Button();
            this.LeftSidebar = new System.Windows.Forms.Panel();
            this.UploadPa = new System.Windows.Forms.CheckBox();
            this.CustomPa = new System.Windows.Forms.CheckBox();
            this.RightSidebar = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnDel = new System.Windows.Forms.Button();
            this.btnRevise = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.dataSource = new System.Windows.Forms.DataGridView();
            this.pictureBox8 = new System.Windows.Forms.PictureBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox10 = new System.Windows.Forms.PictureBox();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.pictureBox7 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btn1 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.LeftSidebar.SuspendLayout();
            this.RightSidebar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 11F);
            this.label2.Location = new System.Drawing.Point(120, 403);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(187, 30);
            this.label2.TabIndex = 1;
            this.label2.Text = "按班级分配套卷的组卷模式\r\n需要两套试卷。";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 11F);
            this.label4.Location = new System.Drawing.Point(482, 403);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(247, 30);
            this.label4.TabIndex = 1;
            this.label4.Text = "按学生的学号分配套卷的组卷模式\r\n需要一套试卷。随机抽题组成试卷。";
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 11F);
            this.label6.Location = new System.Drawing.Point(844, 403);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(142, 15);
            this.label6.TabIndex = 1;
            this.label6.Text = "自由组合学生的套卷";
            // 
            // btnABMode
            // 
            this.btnABMode.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnABMode.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Bold);
            this.btnABMode.Location = new System.Drawing.Point(159, 502);
            this.btnABMode.Margin = new System.Windows.Forms.Padding(2);
            this.btnABMode.Name = "btnABMode";
            this.btnABMode.Size = new System.Drawing.Size(129, 40);
            this.btnABMode.TabIndex = 2;
            this.btnABMode.Text = "AB卷";
            this.btnABMode.UseVisualStyleBackColor = true;
            this.btnABMode.Click += new System.EventHandler(this.ABjuanMode);
            // 
            // btnStuIDMode
            // 
            this.btnStuIDMode.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnStuIDMode.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Bold);
            this.btnStuIDMode.Location = new System.Drawing.Point(522, 502);
            this.btnStuIDMode.Margin = new System.Windows.Forms.Padding(2);
            this.btnStuIDMode.Name = "btnStuIDMode";
            this.btnStuIDMode.Size = new System.Drawing.Size(129, 40);
            this.btnStuIDMode.TabIndex = 2;
            this.btnStuIDMode.Text = "按学号组卷";
            this.btnStuIDMode.UseVisualStyleBackColor = true;
            this.btnStuIDMode.Click += new System.EventHandler(this.StuIDMode);
            // 
            // btnTemplateMode
            // 
            this.btnTemplateMode.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnTemplateMode.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Bold);
            this.btnTemplateMode.Location = new System.Drawing.Point(885, 502);
            this.btnTemplateMode.Margin = new System.Windows.Forms.Padding(2);
            this.btnTemplateMode.Name = "btnTemplateMode";
            this.btnTemplateMode.Size = new System.Drawing.Size(129, 40);
            this.btnTemplateMode.TabIndex = 2;
            this.btnTemplateMode.Text = "模板组卷";
            this.btnTemplateMode.UseVisualStyleBackColor = true;
            this.btnTemplateMode.Click += new System.EventHandler(this.TemplateMode);
            // 
            // LeftSidebar
            // 
            this.LeftSidebar.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.LeftSidebar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LeftSidebar.Controls.Add(this.UploadPa);
            this.LeftSidebar.Controls.Add(this.CustomPa);
            this.LeftSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.LeftSidebar.Location = new System.Drawing.Point(0, 0);
            this.LeftSidebar.Margin = new System.Windows.Forms.Padding(2);
            this.LeftSidebar.Name = "LeftSidebar";
            this.LeftSidebar.Size = new System.Drawing.Size(85, 712);
            this.LeftSidebar.TabIndex = 6;
            this.LeftSidebar.Visible = false;
            // 
            // UploadPa
            // 
            this.UploadPa.Appearance = System.Windows.Forms.Appearance.Button;
            this.UploadPa.BackColor = System.Drawing.SystemColors.ControlLight;
            this.UploadPa.FlatAppearance.BorderSize = 2;
            this.UploadPa.FlatAppearance.CheckedBackColor = System.Drawing.Color.DarkOrange;
            this.UploadPa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.UploadPa.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.UploadPa.Location = new System.Drawing.Point(7, 148);
            this.UploadPa.Margin = new System.Windows.Forms.Padding(2, 0, 0, 0);
            this.UploadPa.Name = "UploadPa";
            this.UploadPa.Size = new System.Drawing.Size(68, 72);
            this.UploadPa.TabIndex = 11;
            this.UploadPa.Text = "上传套卷";
            this.UploadPa.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.UploadPa.UseVisualStyleBackColor = false;
            this.UploadPa.CheckedChanged += new System.EventHandler(this.UploadPa_CheckedChanged);
            this.UploadPa.Click += new System.EventHandler(this.UploadPa_Click);
            this.UploadPa.MouseDown += new System.Windows.Forms.MouseEventHandler(this.UploadPa_MouseDown);
            // 
            // CustomPa
            // 
            this.CustomPa.Appearance = System.Windows.Forms.Appearance.Button;
            this.CustomPa.BackColor = System.Drawing.SystemColors.ControlLight;
            this.CustomPa.FlatAppearance.BorderSize = 2;
            this.CustomPa.FlatAppearance.CheckedBackColor = System.Drawing.Color.DarkOrange;
            this.CustomPa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CustomPa.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.CustomPa.Location = new System.Drawing.Point(7, 342);
            this.CustomPa.Margin = new System.Windows.Forms.Padding(2, 0, 0, 0);
            this.CustomPa.Name = "CustomPa";
            this.CustomPa.Size = new System.Drawing.Size(68, 72);
            this.CustomPa.TabIndex = 11;
            this.CustomPa.Text = "自定义试卷";
            this.CustomPa.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.CustomPa.UseVisualStyleBackColor = false;
            this.CustomPa.CheckedChanged += new System.EventHandler(this.CustomPa_CheckedChanged);
            this.CustomPa.Click += new System.EventHandler(this.CustomPa_Click);
            this.CustomPa.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CustomPa_MouseDown);
            // 
            // RightSidebar
            // 
            this.RightSidebar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RightSidebar.Controls.Add(this.button1);
            this.RightSidebar.Controls.Add(this.label1);
            this.RightSidebar.Controls.Add(this.btnDel);
            this.RightSidebar.Controls.Add(this.btnRevise);
            this.RightSidebar.Controls.Add(this.btnRefresh);
            this.RightSidebar.Controls.Add(this.btnAdd);
            this.RightSidebar.Dock = System.Windows.Forms.DockStyle.Right;
            this.RightSidebar.Location = new System.Drawing.Point(1256, 0);
            this.RightSidebar.Margin = new System.Windows.Forms.Padding(2);
            this.RightSidebar.Name = "RightSidebar";
            this.RightSidebar.Size = new System.Drawing.Size(140, 712);
            this.RightSidebar.TabIndex = 8;
            this.RightSidebar.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(3, 599);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 32);
            this.label1.TabIndex = 10;
            this.label1.Text = "选定\r\n套题编号：";
            // 
            // btnDel
            // 
            this.btnDel.Font = new System.Drawing.Font("宋体", 12F);
            this.btnDel.Location = new System.Drawing.Point(18, 257);
            this.btnDel.Margin = new System.Windows.Forms.Padding(2);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(105, 37);
            this.btnDel.TabIndex = 0;
            this.btnDel.Text = "删除题目";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnRevise
            // 
            this.btnRevise.Font = new System.Drawing.Font("宋体", 12F);
            this.btnRevise.Location = new System.Drawing.Point(18, 182);
            this.btnRevise.Margin = new System.Windows.Forms.Padding(2);
            this.btnRevise.Name = "btnRevise";
            this.btnRevise.Size = new System.Drawing.Size(105, 37);
            this.btnRevise.TabIndex = 0;
            this.btnRevise.Text = "修改题目";
            this.btnRevise.UseVisualStyleBackColor = true;
            this.btnRevise.Click += new System.EventHandler(this.btnRevise_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Font = new System.Drawing.Font("宋体", 12F);
            this.btnRefresh.Location = new System.Drawing.Point(18, 27);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(2);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(105, 37);
            this.btnRefresh.TabIndex = 0;
            this.btnRefresh.Text = "刷  新";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("宋体", 12F);
            this.btnAdd.Location = new System.Drawing.Point(18, 105);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(2);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(105, 37);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "添加题目";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // dataSource
            // 
            this.dataSource.AllowUserToAddRows = false;
            this.dataSource.AllowUserToDeleteRows = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Gainsboro;
            this.dataSource.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dataSource.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataSource.ColumnHeadersHeight = 30;
            this.dataSource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataSource.GridColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.dataSource.Location = new System.Drawing.Point(85, 0);
            this.dataSource.Margin = new System.Windows.Forms.Padding(2);
            this.dataSource.MultiSelect = false;
            this.dataSource.Name = "dataSource";
            this.dataSource.ReadOnly = true;
            this.dataSource.RowTemplate.Height = 27;
            this.dataSource.Size = new System.Drawing.Size(1171, 712);
            this.dataSource.TabIndex = 7;
            this.dataSource.Visible = false;
            // 
            // pictureBox8
            // 
            this.pictureBox8.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox8.Location = new System.Drawing.Point(830, 386);
            this.pictureBox8.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox8.Name = "pictureBox8";
            this.pictureBox8.Size = new System.Drawing.Size(236, 103);
            this.pictureBox8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox8.TabIndex = 0;
            this.pictureBox8.TabStop = false;
            // 
            // pictureBox5
            // 
            this.pictureBox5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox5.Location = new System.Drawing.Point(469, 386);
            this.pictureBox5.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(236, 103);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox5.TabIndex = 0;
            this.pictureBox5.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox3.BackColor = System.Drawing.Color.Gray;
            this.pictureBox3.Location = new System.Drawing.Point(471, 389);
            this.pictureBox3.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(235, 102);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 3;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox2.Location = new System.Drawing.Point(110, 386);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(236, 103);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 0;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox10
            // 
            this.pictureBox10.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox10.BackColor = System.Drawing.Color.Gray;
            this.pictureBox10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox10.Location = new System.Drawing.Point(113, 389);
            this.pictureBox10.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox10.Name = "pictureBox10";
            this.pictureBox10.Size = new System.Drawing.Size(235, 103);
            this.pictureBox10.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox10.TabIndex = 0;
            this.pictureBox10.TabStop = false;
            this.pictureBox10.Click += new System.EventHandler(this.ABjuanMode);
            // 
            // pictureBox6
            // 
            this.pictureBox6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox6.BackColor = System.Drawing.Color.Gray;
            this.pictureBox6.Location = new System.Drawing.Point(831, 389);
            this.pictureBox6.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(235, 102);
            this.pictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox6.TabIndex = 4;
            this.pictureBox6.TabStop = false;
            // 
            // pictureBox7
            // 
            this.pictureBox7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox7.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox7.Image = global::UI.Properties.Resources._1_21b;
            this.pictureBox7.Location = new System.Drawing.Point(830, 177);
            this.pictureBox7.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox7.Name = "pictureBox7";
            this.pictureBox7.Size = new System.Drawing.Size(235, 210);
            this.pictureBox7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox7.TabIndex = 0;
            this.pictureBox7.TabStop = false;
            this.pictureBox7.Click += new System.EventHandler(this.TemplateMode);
            // 
            // pictureBox4
            // 
            this.pictureBox4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox4.Image = global::UI.Properties.Resources._1_21b;
            this.pictureBox4.Location = new System.Drawing.Point(469, 177);
            this.pictureBox4.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(235, 210);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox4.TabIndex = 0;
            this.pictureBox4.TabStop = false;
            this.pictureBox4.Click += new System.EventHandler(this.StuIDMode);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Image = global::UI.Properties.Resources._1_21b;
            this.pictureBox1.Location = new System.Drawing.Point(110, 177);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(235, 210);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.ABjuanMode);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.pictureBox7);
            this.panel2.Controls.Add(this.btnTemplateMode);
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Controls.Add(this.btnStuIDMode);
            this.panel2.Controls.Add(this.pictureBox4);
            this.panel2.Controls.Add(this.btnABMode);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.pictureBox2);
            this.panel2.Controls.Add(this.pictureBox10);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.pictureBox8);
            this.panel2.Controls.Add(this.pictureBox5);
            this.panel2.Controls.Add(this.pictureBox3);
            this.panel2.Controls.Add(this.pictureBox6);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(85, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1171, 712);
            this.panel2.TabIndex = 9;
            // 
            // btn1
            // 
            this.btn1.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Bold);
            this.btn1.Location = new System.Drawing.Point(594, 338);
            this.btn1.Name = "btn1";
            this.btn1.Size = new System.Drawing.Size(178, 58);
            this.btn1.TabIndex = 0;
            this.btn1.Text = "添加套卷";
            this.btn1.UseVisualStyleBackColor = true;
            this.btn1.Visible = false;
            this.btn1.Click += new System.EventHandler(this.btn1_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(21, 425);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 88);
            this.button1.TabIndex = 5;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // fajuanFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1396, 712);
            this.Controls.Add(this.btn1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.dataSource);
            this.Controls.Add(this.RightSidebar);
            this.Controls.Add(this.LeftSidebar);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "fajuanFrm";
            this.Text = "发卷";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.fajuanFrm_FormClosed);
            this.Load += new System.EventHandler(this.fajuanFrm_Load);
            this.LeftSidebar.ResumeLayout(false);
            this.RightSidebar.ResumeLayout(false);
            this.RightSidebar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBox7;
        private System.Windows.Forms.PictureBox pictureBox8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.PictureBox pictureBox10;
        private System.Windows.Forms.Button btnABMode;
        private System.Windows.Forms.Button btnStuIDMode;
        private System.Windows.Forms.Button btnTemplateMode;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox6;
        private System.Windows.Forms.Panel LeftSidebar;
        private System.Windows.Forms.Panel RightSidebar;
        private System.Windows.Forms.CheckBox UploadPa;
        private System.Windows.Forms.CheckBox CustomPa;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Button btnRevise;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btn1;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.DataGridView dataSource;
        private System.Windows.Forms.Button button1;
    }
}
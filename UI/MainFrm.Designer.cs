namespace UI
{
    partial class MainFrm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainFrm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.btnfajuan = new System.Windows.Forms.Button();
            this.btngaijuan = new System.Windows.Forms.Button();
            this.btnfajuanCheck = new System.Windows.Forms.Button();
            this.btngaijuanCheck = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.listView1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold);
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(327, 698);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "系统管理";
            // 
            // listView1
            // 
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.Font = new System.Drawing.Font("宋体", 11F);
            this.listView1.LargeImageList = this.imageList1;
            this.listView1.Location = new System.Drawing.Point(2, 25);
            this.listView1.Margin = new System.Windows.Forms.Padding(2);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(323, 671);
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.Click += new System.EventHandler(this.listView1_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "3.jpg");
            this.imageList1.Images.SetKeyName(1, "4.jpg");
            this.imageList1.Images.SetKeyName(2, "8.jpg");
            this.imageList1.Images.SetKeyName(3, "11bradfieldpark1920x1200.jpg");
            // 
            // pictureBox4
            // 
            this.pictureBox4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox4.Image = global::UI.Properties.Resources._8;
            this.pictureBox4.Location = new System.Drawing.Point(562, 379);
            this.pictureBox4.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(270, 112);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox4.TabIndex = 1;
            this.pictureBox4.TabStop = false;
            this.pictureBox4.Click += new System.EventHandler(this.gaijuanCheck);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Image = global::UI.Properties.Resources._2560x1600;
            this.pictureBox1.Location = new System.Drawing.Point(164, 155);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(270, 128);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.fajuan);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox2.Image = global::UI.Properties.Resources._1;
            this.pictureBox2.Location = new System.Drawing.Point(562, 155);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(270, 128);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.gaijuan);
            // 
            // pictureBox3
            // 
            this.pictureBox3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox3.Image = global::UI.Properties.Resources._4;
            this.pictureBox3.Location = new System.Drawing.Point(164, 379);
            this.pictureBox3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(270, 112);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 1;
            this.pictureBox3.TabStop = false;
            this.pictureBox3.Click += new System.EventHandler(this.fajuanCheck);
            // 
            // btnfajuan
            // 
            this.btnfajuan.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnfajuan.Font = new System.Drawing.Font("微软雅黑", 16F, System.Drawing.FontStyle.Bold);
            this.btnfajuan.Location = new System.Drawing.Point(225, 296);
            this.btnfajuan.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnfajuan.Name = "btnfajuan";
            this.btnfajuan.Size = new System.Drawing.Size(137, 35);
            this.btnfajuan.TabIndex = 3;
            this.btnfajuan.Text = "发   卷";
            this.btnfajuan.UseVisualStyleBackColor = true;
            this.btnfajuan.Click += new System.EventHandler(this.fajuan);
            // 
            // btngaijuan
            // 
            this.btngaijuan.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btngaijuan.Font = new System.Drawing.Font("微软雅黑", 16F, System.Drawing.FontStyle.Bold);
            this.btngaijuan.Location = new System.Drawing.Point(638, 296);
            this.btngaijuan.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btngaijuan.Name = "btngaijuan";
            this.btngaijuan.Size = new System.Drawing.Size(137, 35);
            this.btngaijuan.TabIndex = 3;
            this.btngaijuan.Text = "改   卷";
            this.btngaijuan.UseVisualStyleBackColor = true;
            this.btngaijuan.Click += new System.EventHandler(this.gaijuan);
            // 
            // btnfajuanCheck
            // 
            this.btnfajuanCheck.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnfajuanCheck.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.btnfajuanCheck.Location = new System.Drawing.Point(225, 504);
            this.btnfajuanCheck.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnfajuanCheck.Name = "btnfajuanCheck";
            this.btnfajuanCheck.Size = new System.Drawing.Size(148, 35);
            this.btnfajuanCheck.TabIndex = 3;
            this.btnfajuanCheck.Text = "查看以往发卷情况";
            this.btnfajuanCheck.UseVisualStyleBackColor = true;
            this.btnfajuanCheck.Click += new System.EventHandler(this.fajuanCheck);
            // 
            // btngaijuanCheck
            // 
            this.btngaijuanCheck.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btngaijuanCheck.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.btngaijuanCheck.Location = new System.Drawing.Point(627, 504);
            this.btngaijuanCheck.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btngaijuanCheck.Name = "btngaijuanCheck";
            this.btngaijuanCheck.Size = new System.Drawing.Size(148, 35);
            this.btngaijuanCheck.TabIndex = 3;
            this.btngaijuanCheck.Text = "查看以往改卷情况";
            this.btngaijuanCheck.UseVisualStyleBackColor = true;
            this.btngaijuanCheck.Click += new System.EventHandler(this.gaijuanCheck);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.btngaijuanCheck);
            this.panel1.Controls.Add(this.pictureBox3);
            this.panel1.Controls.Add(this.btnfajuanCheck);
            this.panel1.Controls.Add(this.pictureBox2);
            this.panel1.Controls.Add(this.btngaijuan);
            this.panel1.Controls.Add(this.pictureBox4);
            this.panel1.Controls.Add(this.btnfajuan);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(327, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(994, 698);
            this.panel1.TabIndex = 4;
            // 
            // MainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1321, 698);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.Name = "MainFrm";
            this.Text = "主页";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainFrm_FormClosed);
            this.Load += new System.EventHandler(this.MainFrm_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button btnfajuan;
        private System.Windows.Forms.Button btngaijuan;
        private System.Windows.Forms.Button btnfajuanCheck;
        private System.Windows.Forms.Button btngaijuanCheck;
        private System.Windows.Forms.Panel panel1;
    }
}
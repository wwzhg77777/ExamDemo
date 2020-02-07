namespace UI
{
    partial class ExamLoginTea
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExamLoginTea));
            this.txtAdminName = new System.Windows.Forms.TextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.labAdminName = new System.Windows.Forms.Label();
            this.labAdminPWD = new System.Windows.Forms.Label();
            this.txtAdminPWD = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnLogon = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtAdminName
            // 
            this.txtAdminName.Font = new System.Drawing.Font("宋体", 11F);
            this.txtAdminName.Location = new System.Drawing.Point(238, 39);
            this.txtAdminName.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtAdminName.Name = "txtAdminName";
            this.txtAdminName.Size = new System.Drawing.Size(139, 24);
            this.txtAdminName.TabIndex = 1;
            this.txtAdminName.Text = "admin";
            // 
            // btnLogin
            // 
            this.btnLogin.Font = new System.Drawing.Font("宋体", 9.5F);
            this.btnLogin.Location = new System.Drawing.Point(132, 148);
            this.btnLogin.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(98, 28);
            this.btnLogin.TabIndex = 0;
            this.btnLogin.Text = "登录系统";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // labAdminName
            // 
            this.labAdminName.AutoSize = true;
            this.labAdminName.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Bold);
            this.labAdminName.Location = new System.Drawing.Point(151, 42);
            this.labAdminName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labAdminName.Name = "labAdminName";
            this.labAdminName.Size = new System.Drawing.Size(71, 15);
            this.labAdminName.TabIndex = 2;
            this.labAdminName.Text = "用户名：";
            // 
            // labAdminPWD
            // 
            this.labAdminPWD.AutoSize = true;
            this.labAdminPWD.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Bold);
            this.labAdminPWD.Location = new System.Drawing.Point(149, 98);
            this.labAdminPWD.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labAdminPWD.Name = "labAdminPWD";
            this.labAdminPWD.Size = new System.Drawing.Size(73, 15);
            this.labAdminPWD.TabIndex = 2;
            this.labAdminPWD.Text = "密  码：";
            // 
            // txtAdminPWD
            // 
            this.txtAdminPWD.Font = new System.Drawing.Font("宋体", 11F);
            this.txtAdminPWD.Location = new System.Drawing.Point(238, 96);
            this.txtAdminPWD.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtAdminPWD.Name = "txtAdminPWD";
            this.txtAdminPWD.PasswordChar = '*';
            this.txtAdminPWD.Size = new System.Drawing.Size(139, 24);
            this.txtAdminPWD.TabIndex = 2;
            this.txtAdminPWD.Text = "admin";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(538, 135);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtAdminName);
            this.groupBox1.Controls.Add(this.txtAdminPWD);
            this.groupBox1.Controls.Add(this.labAdminPWD);
            this.groupBox1.Controls.Add(this.btnLogin);
            this.groupBox1.Controls.Add(this.labAdminName);
            this.groupBox1.Controls.Add(this.btnLogon);
            this.groupBox1.Controls.Add(this.btnExit);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("宋体", 10F);
            this.groupBox1.Location = new System.Drawing.Point(0, 135);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Size = new System.Drawing.Size(538, 213);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "登录窗口";
            // 
            // btnLogon
            // 
            this.btnLogon.Font = new System.Drawing.Font("宋体", 9.5F);
            this.btnLogon.Location = new System.Drawing.Point(409, 39);
            this.btnLogon.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnLogon.Name = "btnLogon";
            this.btnLogon.Size = new System.Drawing.Size(98, 28);
            this.btnLogon.TabIndex = 3;
            this.btnLogon.Text = "注册教师账号";
            this.btnLogon.UseVisualStyleBackColor = true;
            this.btnLogon.Click += new System.EventHandler(this.btnLogon_Click);
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("宋体", 9.5F);
            this.btnExit.Location = new System.Drawing.Point(337, 148);
            this.btnExit.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(98, 28);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "退出";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // ExamLoginTea
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(538, 348);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pictureBox1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "ExamLoginTea";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "无纸化考试系统教师端管理入口";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtAdminName;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Label labAdminName;
        private System.Windows.Forms.Label labAdminPWD;
        private System.Windows.Forms.TextBox txtAdminPWD;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnLogon;
    }
}
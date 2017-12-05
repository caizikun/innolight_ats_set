namespace TestPlan
{
    partial class Login
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.chkSQLlib = new System.Windows.Forms.CheckBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.lblPWD = new System.Windows.Forms.Label();
            this.txtPWD = new System.Windows.Forms.TextBox();
            this.lblUserID = new System.Windows.Forms.Label();
            this.txtUserID = new System.Windows.Forms.TextBox();
            this.timerDate = new System.Windows.Forms.Timer(this.components);
            this.btnChangePWD = new System.Windows.Forms.Button();
            this.lblNewPWD = new System.Windows.Forms.Label();
            this.txtNewPWD = new System.Windows.Forms.TextBox();
            this.lblConfrimPWD = new System.Windows.Forms.Label();
            this.txtConfrimPWD = new System.Windows.Forms.TextBox();
            this.chkChangePWD = new System.Windows.Forms.CheckBox();
            this.grpDBSel = new System.Windows.Forms.GroupBox();
            this.rdoEDVTHome = new System.Windows.Forms.RadioButton();
            this.rdoATSHome = new System.Windows.Forms.RadioButton();
            this.grpLogin = new System.Windows.Forms.GroupBox();
            this.txtStates = new System.Windows.Forms.TextBox();
            this.grpDBSel.SuspendLayout();
            this.grpLogin.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkSQLlib
            // 
            this.chkSQLlib.AutoSize = true;
            this.chkSQLlib.Checked = true;
            this.chkSQLlib.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSQLlib.Location = new System.Drawing.Point(8, 12);
            this.chkSQLlib.Name = "chkSQLlib";
            this.chkSQLlib.Size = new System.Drawing.Size(108, 16);
            this.chkSQLlib.TabIndex = 11;
            this.chkSQLlib.Text = "使用SQL数据库?";
            this.chkSQLlib.UseVisualStyleBackColor = true;
            this.chkSQLlib.CheckedChanged += new System.EventHandler(this.chkSQLlib_CheckedChanged);
            // 
            // btnLogin
            // 
            this.btnLogin.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnLogin.Location = new System.Drawing.Point(169, 162);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(73, 28);
            this.btnLogin.TabIndex = 10;
            this.btnLogin.Text = "登  入";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // lblPWD
            // 
            this.lblPWD.AutoSize = true;
            this.lblPWD.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPWD.Location = new System.Drawing.Point(4, 69);
            this.lblPWD.Name = "lblPWD";
            this.lblPWD.Size = new System.Drawing.Size(49, 19);
            this.lblPWD.TabIndex = 9;
            this.lblPWD.Text = "密   码";
            // 
            // txtPWD
            // 
            this.txtPWD.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPWD.Location = new System.Drawing.Point(71, 66);
            this.txtPWD.Name = "txtPWD";
            this.txtPWD.PasswordChar = '*';
            this.txtPWD.Size = new System.Drawing.Size(186, 26);
            this.txtPWD.TabIndex = 8;
            this.txtPWD.Text = "terry";
            this.txtPWD.TextChanged += new System.EventHandler(this.txtPWD_TextChanged);
            // 
            // lblUserID
            // 
            this.lblUserID.AutoSize = true;
            this.lblUserID.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblUserID.Location = new System.Drawing.Point(4, 37);
            this.lblUserID.Name = "lblUserID";
            this.lblUserID.Size = new System.Drawing.Size(49, 19);
            this.lblUserID.TabIndex = 7;
            this.lblUserID.Text = "用   户";
            // 
            // txtUserID
            // 
            this.txtUserID.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtUserID.Location = new System.Drawing.Point(71, 34);
            this.txtUserID.Name = "txtUserID";
            this.txtUserID.Size = new System.Drawing.Size(186, 26);
            this.txtUserID.TabIndex = 6;
            this.txtUserID.Text = "terry.yin";
            this.txtUserID.TextChanged += new System.EventHandler(this.txtUserID_TextChanged);
            // 
            // timerDate
            // 
            this.timerDate.Interval = 1000;
            this.timerDate.Tick += new System.EventHandler(this.timerDate_Tick);
            // 
            // btnChangePWD
            // 
            this.btnChangePWD.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnChangePWD.Location = new System.Drawing.Point(27, 162);
            this.btnChangePWD.Name = "btnChangePWD";
            this.btnChangePWD.Size = new System.Drawing.Size(73, 28);
            this.btnChangePWD.TabIndex = 13;
            this.btnChangePWD.Text = "修改密码";
            this.btnChangePWD.UseVisualStyleBackColor = true;
            this.btnChangePWD.Click += new System.EventHandler(this.btnChangePWD_Click);
            // 
            // lblNewPWD
            // 
            this.lblNewPWD.AutoSize = true;
            this.lblNewPWD.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblNewPWD.Location = new System.Drawing.Point(4, 101);
            this.lblNewPWD.Name = "lblNewPWD";
            this.lblNewPWD.Size = new System.Drawing.Size(51, 19);
            this.lblNewPWD.TabIndex = 15;
            this.lblNewPWD.Text = "新密码";
            // 
            // txtNewPWD
            // 
            this.txtNewPWD.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtNewPWD.Location = new System.Drawing.Point(71, 98);
            this.txtNewPWD.Name = "txtNewPWD";
            this.txtNewPWD.PasswordChar = '*';
            this.txtNewPWD.Size = new System.Drawing.Size(186, 26);
            this.txtNewPWD.TabIndex = 14;
            this.txtNewPWD.TextChanged += new System.EventHandler(this.txtNewPWD_TextChanged);
            // 
            // lblConfrimPWD
            // 
            this.lblConfrimPWD.AutoSize = true;
            this.lblConfrimPWD.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblConfrimPWD.Location = new System.Drawing.Point(4, 133);
            this.lblConfrimPWD.Name = "lblConfrimPWD";
            this.lblConfrimPWD.Size = new System.Drawing.Size(65, 19);
            this.lblConfrimPWD.TabIndex = 17;
            this.lblConfrimPWD.Text = "确认密码";
            // 
            // txtConfrimPWD
            // 
            this.txtConfrimPWD.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtConfrimPWD.Location = new System.Drawing.Point(71, 130);
            this.txtConfrimPWD.Name = "txtConfrimPWD";
            this.txtConfrimPWD.PasswordChar = '*';
            this.txtConfrimPWD.Size = new System.Drawing.Size(186, 26);
            this.txtConfrimPWD.TabIndex = 16;
            this.txtConfrimPWD.TextChanged += new System.EventHandler(this.txtConfrimPWD_TextChanged);
            // 
            // chkChangePWD
            // 
            this.chkChangePWD.AutoSize = true;
            this.chkChangePWD.Location = new System.Drawing.Point(155, 12);
            this.chkChangePWD.Name = "chkChangePWD";
            this.chkChangePWD.Size = new System.Drawing.Size(102, 16);
            this.chkChangePWD.TabIndex = 18;
            this.chkChangePWD.Text = "是否修改密码?";
            this.chkChangePWD.UseVisualStyleBackColor = true;
            this.chkChangePWD.CheckedChanged += new System.EventHandler(this.chkChangePWD_CheckedChanged);
            // 
            // grpDBSel
            // 
            this.grpDBSel.Controls.Add(this.rdoEDVTHome);
            this.grpDBSel.Controls.Add(this.rdoATSHome);
            this.grpDBSel.Location = new System.Drawing.Point(31, 12);
            this.grpDBSel.Name = "grpDBSel";
            this.grpDBSel.Size = new System.Drawing.Size(268, 59);
            this.grpDBSel.TabIndex = 19;
            this.grpDBSel.TabStop = false;
            this.grpDBSel.Text = "请进行数据源选择";
            // 
            // rdoEDVTHome
            // 
            this.rdoEDVTHome.AutoSize = true;
            this.rdoEDVTHome.Location = new System.Drawing.Point(10, 37);
            this.rdoEDVTHome.Name = "rdoEDVTHome";
            this.rdoEDVTHome.Size = new System.Drawing.Size(71, 16);
            this.rdoEDVTHome.TabIndex = 1;
            this.rdoEDVTHome.Text = "EDVTHome";
            this.rdoEDVTHome.UseVisualStyleBackColor = true;
            this.rdoEDVTHome.CheckedChanged += new System.EventHandler(this.rdoEDVTHome_CheckedChanged);
            // 
            // rdoATSHome
            // 
            this.rdoATSHome.AutoSize = true;
            this.rdoATSHome.Location = new System.Drawing.Point(10, 20);
            this.rdoATSHome.Name = "rdoATSHome";
            this.rdoATSHome.Size = new System.Drawing.Size(65, 16);
            this.rdoATSHome.TabIndex = 0;
            this.rdoATSHome.Text = "ATSHome";
            this.rdoATSHome.UseVisualStyleBackColor = true;
            this.rdoATSHome.CheckedChanged += new System.EventHandler(this.rdoATSHome_CheckedChanged);
            // 
            // grpLogin
            // 
            this.grpLogin.Controls.Add(this.chkChangePWD);
            this.grpLogin.Controls.Add(this.lblConfrimPWD);
            this.grpLogin.Controls.Add(this.txtConfrimPWD);
            this.grpLogin.Controls.Add(this.lblNewPWD);
            this.grpLogin.Controls.Add(this.txtNewPWD);
            this.grpLogin.Controls.Add(this.btnChangePWD);
            this.grpLogin.Controls.Add(this.chkSQLlib);
            this.grpLogin.Controls.Add(this.btnLogin);
            this.grpLogin.Controls.Add(this.lblPWD);
            this.grpLogin.Controls.Add(this.txtPWD);
            this.grpLogin.Controls.Add(this.lblUserID);
            this.grpLogin.Controls.Add(this.txtUserID);
            this.grpLogin.Enabled = false;
            this.grpLogin.Location = new System.Drawing.Point(31, 75);
            this.grpLogin.Name = "grpLogin";
            this.grpLogin.Size = new System.Drawing.Size(267, 198);
            this.grpLogin.TabIndex = 20;
            this.grpLogin.TabStop = false;
            // 
            // txtStates
            // 
            this.txtStates.Location = new System.Drawing.Point(12, 279);
            this.txtStates.Multiline = true;
            this.txtStates.Name = "txtStates";
            this.txtStates.Size = new System.Drawing.Size(315, 38);
            this.txtStates.TabIndex = 22;
            this.txtStates.Visible = false;
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 322);
            this.Controls.Add(this.txtStates);
            this.Controls.Add(this.grpLogin);
            this.Controls.Add(this.grpDBSel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(350, 360);
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.Load += new System.EventHandler(this.Login_Load);
            this.VisibleChanged += new System.EventHandler(this.Login_VisibleChanged);
            this.grpDBSel.ResumeLayout(false);
            this.grpDBSel.PerformLayout();
            this.grpLogin.ResumeLayout(false);
            this.grpLogin.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkSQLlib;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Label lblPWD;
        private System.Windows.Forms.TextBox txtPWD;
        private System.Windows.Forms.Label lblUserID;
        private System.Windows.Forms.TextBox txtUserID;
        private System.Windows.Forms.Timer timerDate;
        private System.Windows.Forms.Button btnChangePWD;
        private System.Windows.Forms.Label lblNewPWD;
        private System.Windows.Forms.TextBox txtNewPWD;
        private System.Windows.Forms.Label lblConfrimPWD;
        private System.Windows.Forms.TextBox txtConfrimPWD;
        private System.Windows.Forms.CheckBox chkChangePWD;
        private System.Windows.Forms.GroupBox grpDBSel;
        private System.Windows.Forms.RadioButton rdoEDVTHome;
        private System.Windows.Forms.RadioButton rdoATSHome;
        private System.Windows.Forms.GroupBox grpLogin;
        private System.Windows.Forms.TextBox txtStates;
    }
}


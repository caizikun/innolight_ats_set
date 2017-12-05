namespace GlobalInfo
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
            this.btnLogin = new System.Windows.Forms.Button();
            this.txtPWD = new System.Windows.Forms.TextBox();
            this.txtUserID = new System.Windows.Forms.TextBox();
            this.timerDate = new System.Windows.Forms.Timer(this.components);
            this.txtStates = new System.Windows.Forms.TextBox();
            this.btnCreatAccessDB = new System.Windows.Forms.Button();
            this.grpDBSel = new System.Windows.Forms.GroupBox();
            this.rdoEDVTHome = new System.Windows.Forms.RadioButton();
            this.rdoATSHome = new System.Windows.Forms.RadioButton();
            this.grpLogin = new System.Windows.Forms.GroupBox();
            this.lblPWD = new System.Windows.Forms.Label();
            this.lblUserID = new System.Windows.Forms.Label();
            this.chkSQLlib = new System.Windows.Forms.CheckBox();
            this.grpDBSel.SuspendLayout();
            this.grpLogin.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnLogin
            // 
            this.btnLogin.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnLogin.Location = new System.Drawing.Point(191, 86);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(105, 33);
            this.btnLogin.TabIndex = 10;
            this.btnLogin.Text = "Login Now";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // txtPWD
            // 
            this.txtPWD.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPWD.Location = new System.Drawing.Point(137, 43);
            this.txtPWD.Name = "txtPWD";
            this.txtPWD.PasswordChar = '*';
            this.txtPWD.Size = new System.Drawing.Size(159, 26);
            this.txtPWD.TabIndex = 8;
            this.txtPWD.Text = "terry";
            this.txtPWD.TextChanged += new System.EventHandler(this.txtPWD_TextChanged);
            this.txtPWD.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtPWD_KeyUp);
            // 
            // txtUserID
            // 
            this.txtUserID.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtUserID.Location = new System.Drawing.Point(137, 11);
            this.txtUserID.Name = "txtUserID";
            this.txtUserID.Size = new System.Drawing.Size(159, 26);
            this.txtUserID.TabIndex = 6;
            this.txtUserID.Text = "terry.yin";
            this.txtUserID.TextChanged += new System.EventHandler(this.txtUserID_TextChanged);
            this.txtUserID.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtUserID_KeyUp);
            // 
            // timerDate
            // 
            this.timerDate.Interval = 1000;
            this.timerDate.Tick += new System.EventHandler(this.timerDate_Tick);
            // 
            // txtStates
            // 
            this.txtStates.Location = new System.Drawing.Point(18, 222);
            this.txtStates.Multiline = true;
            this.txtStates.Name = "txtStates";
            this.txtStates.Size = new System.Drawing.Size(323, 78);
            this.txtStates.TabIndex = 12;
            this.txtStates.Visible = false;
            // 
            // btnCreatAccessDB
            // 
            this.btnCreatAccessDB.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnCreatAccessDB.Location = new System.Drawing.Point(29, 86);
            this.btnCreatAccessDB.Name = "btnCreatAccessDB";
            this.btnCreatAccessDB.Size = new System.Drawing.Size(105, 33);
            this.btnCreatAccessDB.TabIndex = 13;
            this.btnCreatAccessDB.Text = "Export ";
            this.btnCreatAccessDB.UseVisualStyleBackColor = true;
            this.btnCreatAccessDB.Click += new System.EventHandler(this.btnCreatAccessDB_Click);
            // 
            // grpDBSel
            // 
            this.grpDBSel.Controls.Add(this.rdoEDVTHome);
            this.grpDBSel.Controls.Add(this.rdoATSHome);
            this.grpDBSel.Location = new System.Drawing.Point(18, 24);
            this.grpDBSel.Name = "grpDBSel";
            this.grpDBSel.Size = new System.Drawing.Size(323, 67);
            this.grpDBSel.TabIndex = 14;
            this.grpDBSel.TabStop = false;
            this.grpDBSel.Text = "Pls select the a DataSource first";
            // 
            // rdoEDVTHome
            // 
            this.rdoEDVTHome.AutoSize = true;
            this.rdoEDVTHome.Location = new System.Drawing.Point(7, 40);
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
            this.rdoATSHome.Location = new System.Drawing.Point(7, 20);
            this.rdoATSHome.Name = "rdoATSHome";
            this.rdoATSHome.Size = new System.Drawing.Size(65, 16);
            this.rdoATSHome.TabIndex = 0;
            this.rdoATSHome.Text = "ATSHome";
            this.rdoATSHome.UseVisualStyleBackColor = true;
            this.rdoATSHome.CheckedChanged += new System.EventHandler(this.rdoATSHome_CheckedChanged);
            // 
            // grpLogin
            // 
            this.grpLogin.Controls.Add(this.lblPWD);
            this.grpLogin.Controls.Add(this.lblUserID);
            this.grpLogin.Controls.Add(this.btnCreatAccessDB);
            this.grpLogin.Controls.Add(this.btnLogin);
            this.grpLogin.Controls.Add(this.txtPWD);
            this.grpLogin.Controls.Add(this.txtUserID);
            this.grpLogin.Enabled = false;
            this.grpLogin.Location = new System.Drawing.Point(18, 91);
            this.grpLogin.Name = "grpLogin";
            this.grpLogin.Size = new System.Drawing.Size(323, 125);
            this.grpLogin.TabIndex = 15;
            this.grpLogin.TabStop = false;
            // 
            // lblPWD
            // 
            this.lblPWD.AutoSize = true;
            this.lblPWD.Font = new System.Drawing.Font("新宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPWD.Location = new System.Drawing.Point(26, 49);
            this.lblPWD.Name = "lblPWD";
            this.lblPWD.Size = new System.Drawing.Size(71, 14);
            this.lblPWD.TabIndex = 15;
            this.lblPWD.Text = "Password";
            // 
            // lblUserID
            // 
            this.lblUserID.AutoSize = true;
            this.lblUserID.Font = new System.Drawing.Font("新宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblUserID.Location = new System.Drawing.Point(26, 17);
            this.lblUserID.Name = "lblUserID";
            this.lblUserID.Size = new System.Drawing.Size(87, 14);
            this.lblUserID.TabIndex = 14;
            this.lblUserID.Text = "Login Name";
            // 
            // chkSQLlib
            // 
            this.chkSQLlib.AutoSize = true;
            this.chkSQLlib.Location = new System.Drawing.Point(18, 2);
            this.chkSQLlib.Name = "chkSQLlib";
            this.chkSQLlib.Size = new System.Drawing.Size(162, 16);
            this.chkSQLlib.TabIndex = 16;
            this.chkSQLlib.Text = "Connect with SQLServer?";
            this.chkSQLlib.UseVisualStyleBackColor = true;
            this.chkSQLlib.CheckedChanged += new System.EventHandler(this.chkSQLlib_CheckedChanged);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(351, 312);
            this.Controls.Add(this.chkSQLlib);
            this.Controls.Add(this.grpLogin);
            this.Controls.Add(this.grpDBSel);
            this.Controls.Add(this.txtStates);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(367, 350);
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

        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.TextBox txtPWD;
        private System.Windows.Forms.TextBox txtUserID;
        private System.Windows.Forms.Timer timerDate;
        private System.Windows.Forms.TextBox txtStates;
        private System.Windows.Forms.Button btnCreatAccessDB;
        private System.Windows.Forms.GroupBox grpDBSel;
        private System.Windows.Forms.RadioButton rdoEDVTHome;
        private System.Windows.Forms.RadioButton rdoATSHome;
        private System.Windows.Forms.GroupBox grpLogin;
        private System.Windows.Forms.CheckBox chkSQLlib;
        private System.Windows.Forms.Label lblPWD;
        private System.Windows.Forms.Label lblUserID;
    }
}


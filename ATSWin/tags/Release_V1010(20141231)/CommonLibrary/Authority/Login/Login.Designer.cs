namespace Authority
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
            this.grpLogin = new System.Windows.Forms.GroupBox();
            this.btnChangePwd = new System.Windows.Forms.Button();
            this.chkChangePWD = new System.Windows.Forms.CheckBox();
            this.lblConfrimPWD = new System.Windows.Forms.Label();
            this.txtconfirmPWD = new System.Windows.Forms.TextBox();
            this.lblNewPWD = new System.Windows.Forms.Label();
            this.txtNewPWD = new System.Windows.Forms.TextBox();
            this.lblPWD = new System.Windows.Forms.Label();
            this.lblUserID = new System.Windows.Forms.Label();
            this.grpLogin.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnLogin
            // 
            this.btnLogin.Font = new System.Drawing.Font("新宋体", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnLogin.Location = new System.Drawing.Point(232, 158);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(70, 33);
            this.btnLogin.TabIndex = 10;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // txtPWD
            // 
            this.txtPWD.Font = new System.Drawing.Font("新宋体", 10.5F, System.Drawing.FontStyle.Bold);
            this.txtPWD.Location = new System.Drawing.Point(116, 69);
            this.txtPWD.Name = "txtPWD";
            this.txtPWD.PasswordChar = '*';
            this.txtPWD.Size = new System.Drawing.Size(186, 23);
            this.txtPWD.TabIndex = 8;
            this.txtPWD.TextChanged += new System.EventHandler(this.txtPWD_TextChanged);
            this.txtPWD.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtPWD_KeyUp);
            // 
            // txtUserID
            // 
            this.txtUserID.Font = new System.Drawing.Font("新宋体", 10.5F, System.Drawing.FontStyle.Bold);
            this.txtUserID.Location = new System.Drawing.Point(116, 37);
            this.txtUserID.Name = "txtUserID";
            this.txtUserID.Size = new System.Drawing.Size(186, 23);
            this.txtUserID.TabIndex = 6;
            this.txtUserID.Text = "Terry.Yin";
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
            this.txtStates.Location = new System.Drawing.Point(12, 218);
            this.txtStates.Multiline = true;
            this.txtStates.Name = "txtStates";
            this.txtStates.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtStates.Size = new System.Drawing.Size(330, 49);
            this.txtStates.TabIndex = 12;
            // 
            // btnCreatAccessDB
            // 
            this.btnCreatAccessDB.Font = new System.Drawing.Font("新宋体", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnCreatAccessDB.Location = new System.Drawing.Point(25, 158);
            this.btnCreatAccessDB.Name = "btnCreatAccessDB";
            this.btnCreatAccessDB.Size = new System.Drawing.Size(70, 33);
            this.btnCreatAccessDB.TabIndex = 13;
            this.btnCreatAccessDB.Text = "Export ";
            this.btnCreatAccessDB.UseVisualStyleBackColor = true;
            this.btnCreatAccessDB.Click += new System.EventHandler(this.btnCreatAccessDB_Click);
            // 
            // grpLogin
            // 
            this.grpLogin.Controls.Add(this.btnChangePwd);
            this.grpLogin.Controls.Add(this.chkChangePWD);
            this.grpLogin.Controls.Add(this.lblConfrimPWD);
            this.grpLogin.Controls.Add(this.txtconfirmPWD);
            this.grpLogin.Controls.Add(this.lblNewPWD);
            this.grpLogin.Controls.Add(this.txtNewPWD);
            this.grpLogin.Controls.Add(this.lblPWD);
            this.grpLogin.Controls.Add(this.lblUserID);
            this.grpLogin.Controls.Add(this.btnCreatAccessDB);
            this.grpLogin.Controls.Add(this.btnLogin);
            this.grpLogin.Controls.Add(this.txtPWD);
            this.grpLogin.Controls.Add(this.txtUserID);
            this.grpLogin.Location = new System.Drawing.Point(12, 12);
            this.grpLogin.Name = "grpLogin";
            this.grpLogin.Size = new System.Drawing.Size(330, 200);
            this.grpLogin.TabIndex = 15;
            this.grpLogin.TabStop = false;
            // 
            // btnChangePwd
            // 
            this.btnChangePwd.Font = new System.Drawing.Font("新宋体", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnChangePwd.Location = new System.Drawing.Point(132, 158);
            this.btnChangePwd.Name = "btnChangePwd";
            this.btnChangePwd.Size = new System.Drawing.Size(70, 33);
            this.btnChangePwd.TabIndex = 23;
            this.btnChangePwd.Text = "Save";
            this.btnChangePwd.UseVisualStyleBackColor = true;
            this.btnChangePwd.Click += new System.EventHandler(this.btnChangePwd_Click);
            // 
            // chkChangePWD
            // 
            this.chkChangePWD.AutoSize = true;
            this.chkChangePWD.Font = new System.Drawing.Font("新宋体", 10.5F, System.Drawing.FontStyle.Bold);
            this.chkChangePWD.Location = new System.Drawing.Point(25, 13);
            this.chkChangePWD.Name = "chkChangePWD";
            this.chkChangePWD.Size = new System.Drawing.Size(154, 18);
            this.chkChangePWD.TabIndex = 18;
            this.chkChangePWD.Text = "Change Password?";
            this.chkChangePWD.UseVisualStyleBackColor = true;
            this.chkChangePWD.Visible = false;
            this.chkChangePWD.CheckedChanged += new System.EventHandler(this.chkChangePWD_CheckedChanged);
            // 
            // lblConfrimPWD
            // 
            this.lblConfrimPWD.AutoSize = true;
            this.lblConfrimPWD.Font = new System.Drawing.Font("新宋体", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblConfrimPWD.Location = new System.Drawing.Point(22, 133);
            this.lblConfrimPWD.Name = "lblConfrimPWD";
            this.lblConfrimPWD.Size = new System.Drawing.Size(63, 14);
            this.lblConfrimPWD.TabIndex = 22;
            this.lblConfrimPWD.Text = "Confirm";
            this.lblConfrimPWD.Visible = false;
            // 
            // txtconfirmPWD
            // 
            this.txtconfirmPWD.Font = new System.Drawing.Font("新宋体", 10.5F, System.Drawing.FontStyle.Bold);
            this.txtconfirmPWD.Location = new System.Drawing.Point(116, 124);
            this.txtconfirmPWD.Name = "txtconfirmPWD";
            this.txtconfirmPWD.PasswordChar = '*';
            this.txtconfirmPWD.Size = new System.Drawing.Size(186, 23);
            this.txtconfirmPWD.TabIndex = 20;
            this.txtconfirmPWD.Visible = false;
            // 
            // lblNewPWD
            // 
            this.lblNewPWD.AutoSize = true;
            this.lblNewPWD.Font = new System.Drawing.Font("新宋体", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblNewPWD.Location = new System.Drawing.Point(22, 101);
            this.lblNewPWD.Name = "lblNewPWD";
            this.lblNewPWD.Size = new System.Drawing.Size(63, 14);
            this.lblNewPWD.TabIndex = 21;
            this.lblNewPWD.Text = "New PWD";
            this.lblNewPWD.Visible = false;
            // 
            // txtNewPWD
            // 
            this.txtNewPWD.Font = new System.Drawing.Font("新宋体", 10.5F, System.Drawing.FontStyle.Bold);
            this.txtNewPWD.Location = new System.Drawing.Point(116, 98);
            this.txtNewPWD.Name = "txtNewPWD";
            this.txtNewPWD.PasswordChar = '*';
            this.txtNewPWD.Size = new System.Drawing.Size(186, 23);
            this.txtNewPWD.TabIndex = 19;
            this.txtNewPWD.Visible = false;
            // 
            // lblPWD
            // 
            this.lblPWD.AutoSize = true;
            this.lblPWD.Font = new System.Drawing.Font("新宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPWD.Location = new System.Drawing.Point(22, 72);
            this.lblPWD.Name = "lblPWD";
            this.lblPWD.Size = new System.Drawing.Size(71, 14);
            this.lblPWD.TabIndex = 15;
            this.lblPWD.Text = "Password";
            // 
            // lblUserID
            // 
            this.lblUserID.AutoSize = true;
            this.lblUserID.Font = new System.Drawing.Font("新宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblUserID.Location = new System.Drawing.Point(22, 40);
            this.lblUserID.Name = "lblUserID";
            this.lblUserID.Size = new System.Drawing.Size(87, 14);
            this.lblUserID.TabIndex = 14;
            this.lblUserID.Text = "Login Name";
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(351, 269);
            this.Controls.Add(this.grpLogin);
            this.Controls.Add(this.txtStates);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.Load += new System.EventHandler(this.Login_Load);
            this.VisibleChanged += new System.EventHandler(this.Login_VisibleChanged);
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
        private System.Windows.Forms.GroupBox grpLogin;
        private System.Windows.Forms.Label lblPWD;
        private System.Windows.Forms.Label lblUserID;
        private System.Windows.Forms.Button btnChangePwd;
        private System.Windows.Forms.CheckBox chkChangePWD;
        private System.Windows.Forms.Label lblConfrimPWD;
        private System.Windows.Forms.TextBox txtconfirmPWD;
        private System.Windows.Forms.Label lblNewPWD;
        private System.Windows.Forms.TextBox txtNewPWD;
    }
}


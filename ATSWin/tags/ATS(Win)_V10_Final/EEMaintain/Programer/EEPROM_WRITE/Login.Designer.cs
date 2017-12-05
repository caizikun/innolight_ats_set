namespace Maintain
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
            this.grpLogin = new System.Windows.Forms.GroupBox();
            this.chkChangePWD = new System.Windows.Forms.CheckBox();
            this.lblConfrimPWD = new System.Windows.Forms.Label();
            this.txtConfrimPWD = new System.Windows.Forms.TextBox();
            this.lblNewPWD = new System.Windows.Forms.Label();
            this.txtNewPWD = new System.Windows.Forms.TextBox();
            this.btnChangePWD = new System.Windows.Forms.Button();
            this.btnLogin = new System.Windows.Forms.Button();
            this.lblPWD = new System.Windows.Forms.Label();
            this.txtPWD = new System.Windows.Forms.TextBox();
            this.lblUserID = new System.Windows.Forms.Label();
            this.txtUserID = new System.Windows.Forms.TextBox();
            this.txtStates = new System.Windows.Forms.TextBox();
            this.timerDate = new System.Windows.Forms.Timer(this.components);
            this.grpLogin.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpLogin
            // 
            this.grpLogin.Controls.Add(this.chkChangePWD);
            this.grpLogin.Controls.Add(this.lblConfrimPWD);
            this.grpLogin.Controls.Add(this.txtConfrimPWD);
            this.grpLogin.Controls.Add(this.lblNewPWD);
            this.grpLogin.Controls.Add(this.txtNewPWD);
            this.grpLogin.Controls.Add(this.btnChangePWD);
            this.grpLogin.Controls.Add(this.btnLogin);
            this.grpLogin.Controls.Add(this.lblPWD);
            this.grpLogin.Controls.Add(this.txtPWD);
            this.grpLogin.Controls.Add(this.lblUserID);
            this.grpLogin.Controls.Add(this.txtUserID);
            this.grpLogin.Location = new System.Drawing.Point(25, 20);
            this.grpLogin.Name = "grpLogin";
            this.grpLogin.Size = new System.Drawing.Size(310, 223);
            this.grpLogin.TabIndex = 21;
            this.grpLogin.TabStop = false;
            // 
            // chkChangePWD
            // 
            this.chkChangePWD.AutoSize = true;
            this.chkChangePWD.Location = new System.Drawing.Point(8, 15);
            this.chkChangePWD.Name = "chkChangePWD";
            this.chkChangePWD.Size = new System.Drawing.Size(126, 16);
            this.chkChangePWD.TabIndex = 18;
            this.chkChangePWD.Text = "Change Password ?";
            this.chkChangePWD.UseVisualStyleBackColor = true;
            this.chkChangePWD.CheckedChanged += new System.EventHandler(this.chkChangePWD_CheckedChanged);
            // 
            // lblConfrimPWD
            // 
            this.lblConfrimPWD.AutoSize = true;
            this.lblConfrimPWD.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblConfrimPWD.Location = new System.Drawing.Point(10, 140);
            this.lblConfrimPWD.Name = "lblConfrimPWD";
            this.lblConfrimPWD.Size = new System.Drawing.Size(66, 19);
            this.lblConfrimPWD.TabIndex = 17;
            this.lblConfrimPWD.Text = "Confirm";
            // 
            // txtConfrimPWD
            // 
            this.txtConfrimPWD.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtConfrimPWD.Location = new System.Drawing.Point(102, 137);
            this.txtConfrimPWD.Name = "txtConfrimPWD";
            this.txtConfrimPWD.PasswordChar = '*';
            this.txtConfrimPWD.Size = new System.Drawing.Size(186, 26);
            this.txtConfrimPWD.TabIndex = 16;
            this.txtConfrimPWD.TextChanged += new System.EventHandler(this.txtConfrimPWD_TextChanged);
            // 
            // lblNewPWD
            // 
            this.lblNewPWD.AutoSize = true;
            this.lblNewPWD.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblNewPWD.Location = new System.Drawing.Point(10, 108);
            this.lblNewPWD.Name = "lblNewPWD";
            this.lblNewPWD.Size = new System.Drawing.Size(80, 19);
            this.lblNewPWD.TabIndex = 15;
            this.lblNewPWD.Text = "New PWD";
            // 
            // txtNewPWD
            // 
            this.txtNewPWD.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtNewPWD.Location = new System.Drawing.Point(102, 105);
            this.txtNewPWD.Name = "txtNewPWD";
            this.txtNewPWD.PasswordChar = '*';
            this.txtNewPWD.Size = new System.Drawing.Size(186, 26);
            this.txtNewPWD.TabIndex = 14;
            this.txtNewPWD.TextChanged += new System.EventHandler(this.txtNewPWD_TextChanged);
            // 
            // btnChangePWD
            // 
            this.btnChangePWD.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnChangePWD.Location = new System.Drawing.Point(54, 181);
            this.btnChangePWD.Name = "btnChangePWD";
            this.btnChangePWD.Size = new System.Drawing.Size(73, 28);
            this.btnChangePWD.TabIndex = 13;
            this.btnChangePWD.Text = "Save";
            this.btnChangePWD.UseVisualStyleBackColor = true;
            this.btnChangePWD.Click += new System.EventHandler(this.btnChangePWD_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnLogin.Location = new System.Drawing.Point(178, 181);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(73, 28);
            this.btnLogin.TabIndex = 10;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // lblPWD
            // 
            this.lblPWD.AutoSize = true;
            this.lblPWD.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPWD.Location = new System.Drawing.Point(10, 76);
            this.lblPWD.Name = "lblPWD";
            this.lblPWD.Size = new System.Drawing.Size(76, 19);
            this.lblPWD.TabIndex = 9;
            this.lblPWD.Text = "Password";
            // 
            // txtPWD
            // 
            this.txtPWD.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPWD.Location = new System.Drawing.Point(102, 73);
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
            this.lblUserID.Location = new System.Drawing.Point(10, 44);
            this.lblUserID.Name = "lblUserID";
            this.lblUserID.Size = new System.Drawing.Size(91, 19);
            this.lblUserID.TabIndex = 7;
            this.lblUserID.Text = "User Name ";
            // 
            // txtUserID
            // 
            this.txtUserID.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtUserID.Location = new System.Drawing.Point(102, 41);
            this.txtUserID.Name = "txtUserID";
            this.txtUserID.Size = new System.Drawing.Size(186, 26);
            this.txtUserID.TabIndex = 6;
            this.txtUserID.Text = "Terry.Yin";
            this.txtUserID.TextChanged += new System.EventHandler(this.txtUserID_TextChanged);
            // 
            // txtStates
            // 
            this.txtStates.Location = new System.Drawing.Point(23, 248);
            this.txtStates.Multiline = true;
            this.txtStates.Name = "txtStates";
            this.txtStates.Size = new System.Drawing.Size(315, 38);
            this.txtStates.TabIndex = 23;
            this.txtStates.Visible = false;
            // 
            // timerDate
            // 
            this.timerDate.Interval = 1000;
            this.timerDate.Tick += new System.EventHandler(this.timerDate_Tick);
            // 
            // Login
            // 
            this.AcceptButton = this.btnLogin;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 292);
            this.Controls.Add(this.txtStates);
            this.Controls.Add(this.grpLogin);
            this.MaximumSize = new System.Drawing.Size(380, 330);
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login[EEPROM_WRITE]";
            this.Load += new System.EventHandler(this.Login_Load);
            this.VisibleChanged += new System.EventHandler(this.Login_VisibleChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Login_KeyDown);
            this.grpLogin.ResumeLayout(false);
            this.grpLogin.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpLogin;
        private System.Windows.Forms.CheckBox chkChangePWD;
        private System.Windows.Forms.Label lblConfrimPWD;
        private System.Windows.Forms.Button btnChangePWD;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Label lblPWD;
        private System.Windows.Forms.TextBox txtPWD;
        private System.Windows.Forms.Label lblUserID;
        private System.Windows.Forms.TextBox txtUserID;
        private System.Windows.Forms.TextBox txtStates;
        private System.Windows.Forms.Timer timerDate;
        private System.Windows.Forms.Label lblNewPWD;
        private System.Windows.Forms.TextBox txtConfrimPWD;
        private System.Windows.Forms.TextBox txtNewPWD;
    }
}


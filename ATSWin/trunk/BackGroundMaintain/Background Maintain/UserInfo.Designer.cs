namespace Maintain
{
    partial class UserInfo
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
            this.btnExit = new System.Windows.Forms.Button();
            this.txtStates = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.lblGlobal = new System.Windows.Forms.Label();
            this.currlst = new System.Windows.Forms.ListBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.grpFunc = new System.Windows.Forms.GroupBox();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.lblRemark = new System.Windows.Forms.Label();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTrueName = new System.Windows.Forms.TextBox();
            this.lblPWD = new System.Windows.Forms.Label();
            this.txtPWD = new System.Windows.Forms.TextBox();
            this.lblUserID = new System.Windows.Forms.Label();
            this.grpUserRole = new System.Windows.Forms.GroupBox();
            this.lstTotalRole = new System.Windows.Forms.ListBox();
            this.lstUserRole = new System.Windows.Forms.ListBox();
            this.btnRemoveRole = new System.Windows.Forms.Button();
            this.btnAddRole = new System.Windows.Forms.Button();
            this.grpFunc.SuspendLayout();
            this.grpUserRole.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnExit.Location = new System.Drawing.Point(247, 216);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 28);
            this.btnExit.TabIndex = 22;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // txtStates
            // 
            this.txtStates.Location = new System.Drawing.Point(2, 250);
            this.txtStates.Multiline = true;
            this.txtStates.Name = "txtStates";
            this.txtStates.Size = new System.Drawing.Size(747, 27);
            this.txtStates.TabIndex = 23;
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnAdd.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAdd.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnAdd.Location = new System.Drawing.Point(2, 214);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(69, 30);
            this.btnAdd.TabIndex = 53;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.BackColor = System.Drawing.Color.Fuchsia;
            this.btnRemove.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnRemove.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnRemove.Location = new System.Drawing.Point(74, 214);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(64, 30);
            this.btnRemove.TabIndex = 52;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = false;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // lblGlobal
            // 
            this.lblGlobal.AutoSize = true;
            this.lblGlobal.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblGlobal.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblGlobal.Location = new System.Drawing.Point(41, 2);
            this.lblGlobal.Name = "lblGlobal";
            this.lblGlobal.Size = new System.Drawing.Size(70, 19);
            this.lblGlobal.TabIndex = 51;
            this.lblGlobal.Text = "User List";
            // 
            // currlst
            // 
            this.currlst.FormattingEnabled = true;
            this.currlst.HorizontalScrollbar = true;
            this.currlst.ItemHeight = 12;
            this.currlst.Location = new System.Drawing.Point(2, 24);
            this.currlst.Name = "currlst";
            this.currlst.ScrollAlwaysVisible = true;
            this.currlst.Size = new System.Drawing.Size(136, 184);
            this.currlst.TabIndex = 50;
            this.currlst.SelectedIndexChanged += new System.EventHandler(this.currlst_SelectedIndexChanged);
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnOK.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOK.Location = new System.Drawing.Point(103, 149);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 31);
            this.btnOK.TabIndex = 54;
            this.btnOK.Text = "Save";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // grpFunc
            // 
            this.grpFunc.Controls.Add(this.txtUser);
            this.grpFunc.Controls.Add(this.lblRemark);
            this.grpFunc.Controls.Add(this.txtRemark);
            this.grpFunc.Controls.Add(this.label1);
            this.grpFunc.Controls.Add(this.txtTrueName);
            this.grpFunc.Controls.Add(this.lblPWD);
            this.grpFunc.Controls.Add(this.txtPWD);
            this.grpFunc.Controls.Add(this.lblUserID);
            this.grpFunc.Controls.Add(this.btnOK);
            this.grpFunc.Font = new System.Drawing.Font("新宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grpFunc.Location = new System.Drawing.Point(144, 15);
            this.grpFunc.Name = "grpFunc";
            this.grpFunc.Size = new System.Drawing.Size(271, 196);
            this.grpFunc.TabIndex = 55;
            this.grpFunc.TabStop = false;
            this.grpFunc.Text = "User Info";
            // 
            // txtUser
            // 
            this.txtUser.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtUser.Location = new System.Drawing.Point(96, 22);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(163, 26);
            this.txtUser.TabIndex = 62;
            // 
            // lblRemark
            // 
            this.lblRemark.AutoSize = true;
            this.lblRemark.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblRemark.Location = new System.Drawing.Point(7, 121);
            this.lblRemark.Name = "lblRemark";
            this.lblRemark.Size = new System.Drawing.Size(63, 19);
            this.lblRemark.TabIndex = 61;
            this.lblRemark.Text = "Remark";
            // 
            // txtRemark
            // 
            this.txtRemark.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtRemark.Location = new System.Drawing.Point(96, 118);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(164, 26);
            this.txtRemark.TabIndex = 60;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(7, 89);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 19);
            this.label1.TabIndex = 59;
            this.label1.Text = "RealName";
            // 
            // txtTrueName
            // 
            this.txtTrueName.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtTrueName.Location = new System.Drawing.Point(96, 86);
            this.txtTrueName.Name = "txtTrueName";
            this.txtTrueName.Size = new System.Drawing.Size(164, 26);
            this.txtTrueName.TabIndex = 58;
            // 
            // lblPWD
            // 
            this.lblPWD.AutoSize = true;
            this.lblPWD.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPWD.Location = new System.Drawing.Point(7, 57);
            this.lblPWD.Name = "lblPWD";
            this.lblPWD.Size = new System.Drawing.Size(76, 19);
            this.lblPWD.TabIndex = 57;
            this.lblPWD.Text = "Password";
            // 
            // txtPWD
            // 
            this.txtPWD.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPWD.Location = new System.Drawing.Point(96, 54);
            this.txtPWD.Name = "txtPWD";
            this.txtPWD.PasswordChar = '*';
            this.txtPWD.Size = new System.Drawing.Size(164, 26);
            this.txtPWD.TabIndex = 56;
            this.txtPWD.Text = "terry";
            // 
            // lblUserID
            // 
            this.lblUserID.AutoSize = true;
            this.lblUserID.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblUserID.Location = new System.Drawing.Point(7, 25);
            this.lblUserID.Name = "lblUserID";
            this.lblUserID.Size = new System.Drawing.Size(83, 19);
            this.lblUserID.TabIndex = 55;
            this.lblUserID.Text = "UserName";
            // 
            // grpUserRole
            // 
            this.grpUserRole.Controls.Add(this.lstTotalRole);
            this.grpUserRole.Controls.Add(this.lstUserRole);
            this.grpUserRole.Controls.Add(this.btnRemoveRole);
            this.grpUserRole.Controls.Add(this.btnAddRole);
            this.grpUserRole.Font = new System.Drawing.Font("新宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grpUserRole.Location = new System.Drawing.Point(421, 15);
            this.grpUserRole.Name = "grpUserRole";
            this.grpUserRole.Size = new System.Drawing.Size(328, 229);
            this.grpUserRole.TabIndex = 56;
            this.grpUserRole.TabStop = false;
            this.grpUserRole.Text = "User-Role";
            // 
            // lstTotalRole
            // 
            this.lstTotalRole.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.lstTotalRole.Font = new System.Drawing.Font("新宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lstTotalRole.FormattingEnabled = true;
            this.lstTotalRole.ItemHeight = 14;
            this.lstTotalRole.Location = new System.Drawing.Point(196, 23);
            this.lstTotalRole.Name = "lstTotalRole";
            this.lstTotalRole.Size = new System.Drawing.Size(124, 200);
            this.lstTotalRole.TabIndex = 31;
            this.lstTotalRole.SelectedIndexChanged += new System.EventHandler(this.lstTotalRole_SelectedIndexChanged);
            // 
            // lstUserRole
            // 
            this.lstUserRole.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.lstUserRole.Font = new System.Drawing.Font("新宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lstUserRole.FormattingEnabled = true;
            this.lstUserRole.ItemHeight = 14;
            this.lstUserRole.Location = new System.Drawing.Point(9, 22);
            this.lstUserRole.Name = "lstUserRole";
            this.lstUserRole.Size = new System.Drawing.Size(125, 200);
            this.lstUserRole.TabIndex = 30;
            this.lstUserRole.SelectedIndexChanged += new System.EventHandler(this.lstUserRole_SelectedIndexChanged);
            // 
            // btnRemoveRole
            // 
            this.btnRemoveRole.Enabled = false;
            this.btnRemoveRole.Font = new System.Drawing.Font("新宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnRemoveRole.Location = new System.Drawing.Point(140, 114);
            this.btnRemoveRole.Name = "btnRemoveRole";
            this.btnRemoveRole.Size = new System.Drawing.Size(50, 28);
            this.btnRemoveRole.TabIndex = 29;
            this.btnRemoveRole.Text = "Del";
            this.btnRemoveRole.UseVisualStyleBackColor = true;
            this.btnRemoveRole.Click += new System.EventHandler(this.btnRemoveRole_Click);
            // 
            // btnAddRole
            // 
            this.btnAddRole.Enabled = false;
            this.btnAddRole.Font = new System.Drawing.Font("新宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAddRole.Location = new System.Drawing.Point(140, 80);
            this.btnAddRole.Name = "btnAddRole";
            this.btnAddRole.Size = new System.Drawing.Size(50, 28);
            this.btnAddRole.TabIndex = 28;
            this.btnAddRole.Text = "Add";
            this.btnAddRole.UseVisualStyleBackColor = true;
            this.btnAddRole.Click += new System.EventHandler(this.btnAddRole_Click);
            // 
            // UserInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(750, 282);
            this.Controls.Add(this.grpUserRole);
            this.Controls.Add(this.grpFunc);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.txtStates);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.lblGlobal);
            this.Controls.Add(this.currlst);
            this.MinimumSize = new System.Drawing.Size(766, 320);
            this.Name = "UserInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UserInfo";
            this.Load += new System.EventHandler(this.UserInfo_Load);
            this.grpFunc.ResumeLayout(false);
            this.grpFunc.PerformLayout();
            this.grpUserRole.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.TextBox txtStates;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Label lblGlobal;
        private System.Windows.Forms.ListBox currlst;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.GroupBox grpFunc;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.Label lblRemark;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTrueName;
        private System.Windows.Forms.Label lblPWD;
        private System.Windows.Forms.TextBox txtPWD;
        private System.Windows.Forms.Label lblUserID;
        private System.Windows.Forms.GroupBox grpUserRole;
        private System.Windows.Forms.ListBox lstTotalRole;
        private System.Windows.Forms.ListBox lstUserRole;
        private System.Windows.Forms.Button btnRemoveRole;
        private System.Windows.Forms.Button btnAddRole;
    }
}
namespace Maintain
{
    partial class UserRoleFunctionInfo
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
            this.grpUserRole = new System.Windows.Forms.GroupBox();
            this.lstTotalRole = new System.Windows.Forms.ListBox();
            this.lstUserRole = new System.Windows.Forms.ListBox();
            this.btnRemoveRole = new System.Windows.Forms.Button();
            this.btnAddRole = new System.Windows.Forms.Button();
            this.cboUser = new System.Windows.Forms.ComboBox();
            this.grpRoleFunc = new System.Windows.Forms.GroupBox();
            this.lstTotalFunc = new System.Windows.Forms.ListBox();
            this.lstRoleFunc = new System.Windows.Forms.ListBox();
            this.btnRemoveFunc = new System.Windows.Forms.Button();
            this.btnAddFunc = new System.Windows.Forms.Button();
            this.cboRole = new System.Windows.Forms.ComboBox();
            this.grpUserRole.SuspendLayout();
            this.grpRoleFunc.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("新宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnExit.Location = new System.Drawing.Point(261, 260);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(140, 28);
            this.btnExit.TabIndex = 22;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // txtStates
            // 
            this.txtStates.Location = new System.Drawing.Point(3, 294);
            this.txtStates.Multiline = true;
            this.txtStates.Name = "txtStates";
            this.txtStates.Size = new System.Drawing.Size(663, 20);
            this.txtStates.TabIndex = 23;
            // 
            // grpUserRole
            // 
            this.grpUserRole.Controls.Add(this.lstTotalRole);
            this.grpUserRole.Controls.Add(this.lstUserRole);
            this.grpUserRole.Controls.Add(this.btnRemoveRole);
            this.grpUserRole.Controls.Add(this.btnAddRole);
            this.grpUserRole.Controls.Add(this.cboUser);
            this.grpUserRole.Font = new System.Drawing.Font("新宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grpUserRole.Location = new System.Drawing.Point(4, 5);
            this.grpUserRole.Name = "grpUserRole";
            this.grpUserRole.Size = new System.Drawing.Size(328, 249);
            this.grpUserRole.TabIndex = 30;
            this.grpUserRole.TabStop = false;
            this.grpUserRole.Text = "User-Role";
            // 
            // lstTotalRole
            // 
            this.lstTotalRole.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.lstTotalRole.Font = new System.Drawing.Font("新宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lstTotalRole.FormattingEnabled = true;
            this.lstTotalRole.ItemHeight = 14;
            this.lstTotalRole.Location = new System.Drawing.Point(196, 20);
            this.lstTotalRole.Name = "lstTotalRole";
            this.lstTotalRole.Size = new System.Drawing.Size(124, 228);
            this.lstTotalRole.TabIndex = 31;
            this.lstTotalRole.SelectedIndexChanged += new System.EventHandler(this.lstTotalRole_SelectedIndexChanged);
            // 
            // lstUserRole
            // 
            this.lstUserRole.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.lstUserRole.Font = new System.Drawing.Font("新宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lstUserRole.FormattingEnabled = true;
            this.lstUserRole.ItemHeight = 14;
            this.lstUserRole.Location = new System.Drawing.Point(9, 47);
            this.lstUserRole.Name = "lstUserRole";
            this.lstUserRole.Size = new System.Drawing.Size(125, 200);
            this.lstUserRole.TabIndex = 30;
            this.lstUserRole.SelectedIndexChanged += new System.EventHandler(this.lstRole_SelectedIndexChanged);
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
            // cboUser
            // 
            this.cboUser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cboUser.Font = new System.Drawing.Font("新宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboUser.FormattingEnabled = true;
            this.cboUser.Location = new System.Drawing.Point(10, 20);
            this.cboUser.Name = "cboUser";
            this.cboUser.Size = new System.Drawing.Size(124, 22);
            this.cboUser.TabIndex = 27;
            this.cboUser.SelectedIndexChanged += new System.EventHandler(this.cboUser_SelectedIndexChanged);
            // 
            // grpRoleFunc
            // 
            this.grpRoleFunc.Controls.Add(this.lstTotalFunc);
            this.grpRoleFunc.Controls.Add(this.lstRoleFunc);
            this.grpRoleFunc.Controls.Add(this.btnRemoveFunc);
            this.grpRoleFunc.Controls.Add(this.btnAddFunc);
            this.grpRoleFunc.Controls.Add(this.cboRole);
            this.grpRoleFunc.Font = new System.Drawing.Font("新宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grpRoleFunc.Location = new System.Drawing.Point(343, 5);
            this.grpRoleFunc.Name = "grpRoleFunc";
            this.grpRoleFunc.Size = new System.Drawing.Size(323, 249);
            this.grpRoleFunc.TabIndex = 31;
            this.grpRoleFunc.TabStop = false;
            this.grpRoleFunc.Text = "Role-Function";
            // 
            // lstTotalFunc
            // 
            this.lstTotalFunc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lstTotalFunc.Font = new System.Drawing.Font("新宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lstTotalFunc.FormattingEnabled = true;
            this.lstTotalFunc.ItemHeight = 14;
            this.lstTotalFunc.Location = new System.Drawing.Point(196, 19);
            this.lstTotalFunc.Name = "lstTotalFunc";
            this.lstTotalFunc.Size = new System.Drawing.Size(124, 228);
            this.lstTotalFunc.TabIndex = 31;
            this.lstTotalFunc.SelectedIndexChanged += new System.EventHandler(this.lstTotalFunc_SelectedIndexChanged);
            // 
            // lstRoleFunc
            // 
            this.lstRoleFunc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.lstRoleFunc.Font = new System.Drawing.Font("新宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lstRoleFunc.FormattingEnabled = true;
            this.lstRoleFunc.ItemHeight = 14;
            this.lstRoleFunc.Location = new System.Drawing.Point(9, 47);
            this.lstRoleFunc.Name = "lstRoleFunc";
            this.lstRoleFunc.Size = new System.Drawing.Size(125, 200);
            this.lstRoleFunc.TabIndex = 30;
            this.lstRoleFunc.SelectedIndexChanged += new System.EventHandler(this.lstRoleFunc_SelectedIndexChanged);
            // 
            // btnRemoveFunc
            // 
            this.btnRemoveFunc.Enabled = false;
            this.btnRemoveFunc.Font = new System.Drawing.Font("新宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnRemoveFunc.Location = new System.Drawing.Point(140, 114);
            this.btnRemoveFunc.Name = "btnRemoveFunc";
            this.btnRemoveFunc.Size = new System.Drawing.Size(50, 28);
            this.btnRemoveFunc.TabIndex = 29;
            this.btnRemoveFunc.Text = "Del";
            this.btnRemoveFunc.UseVisualStyleBackColor = true;
            this.btnRemoveFunc.Click += new System.EventHandler(this.btnRemoveFunc_Click);
            // 
            // btnAddFunc
            // 
            this.btnAddFunc.Enabled = false;
            this.btnAddFunc.Font = new System.Drawing.Font("新宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAddFunc.Location = new System.Drawing.Point(140, 80);
            this.btnAddFunc.Name = "btnAddFunc";
            this.btnAddFunc.Size = new System.Drawing.Size(50, 28);
            this.btnAddFunc.TabIndex = 28;
            this.btnAddFunc.Text = "Add";
            this.btnAddFunc.UseVisualStyleBackColor = true;
            this.btnAddFunc.Click += new System.EventHandler(this.btnAddFunc_Click);
            // 
            // cboRole
            // 
            this.cboRole.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.cboRole.Font = new System.Drawing.Font("新宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboRole.FormattingEnabled = true;
            this.cboRole.Location = new System.Drawing.Point(10, 20);
            this.cboRole.Name = "cboRole";
            this.cboRole.Size = new System.Drawing.Size(124, 22);
            this.cboRole.TabIndex = 27;
            this.cboRole.SelectedIndexChanged += new System.EventHandler(this.cboRole_SelectedIndexChanged);
            // 
            // UserRoleFunctionInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(669, 313);
            this.Controls.Add(this.grpRoleFunc);
            this.Controls.Add(this.grpUserRole);
            this.Controls.Add(this.txtStates);
            this.Controls.Add(this.btnExit);
            this.MaximumSize = new System.Drawing.Size(685, 351);
            this.Name = "UserRoleFunctionInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UserRoleFunctionInfo";
            this.Load += new System.EventHandler(this.FunctionInfo_Load);
            this.grpUserRole.ResumeLayout(false);
            this.grpRoleFunc.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.TextBox txtStates;
        private System.Windows.Forms.GroupBox grpUserRole;
        private System.Windows.Forms.ListBox lstTotalRole;
        private System.Windows.Forms.ListBox lstUserRole;
        private System.Windows.Forms.Button btnRemoveRole;
        private System.Windows.Forms.Button btnAddRole;
        private System.Windows.Forms.ComboBox cboUser;
        private System.Windows.Forms.GroupBox grpRoleFunc;
        private System.Windows.Forms.ListBox lstTotalFunc;
        private System.Windows.Forms.ListBox lstRoleFunc;
        private System.Windows.Forms.Button btnRemoveFunc;
        private System.Windows.Forms.Button btnAddFunc;
        private System.Windows.Forms.ComboBox cboRole;
    }
}
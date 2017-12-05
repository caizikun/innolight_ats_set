namespace Maintain
{
    partial class RoleInfo
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
            this.lblUserID = new System.Windows.Forms.Label();
            this.lblRemark = new System.Windows.Forms.Label();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.txtStates = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.lblGlobal = new System.Windows.Forms.Label();
            this.currlst = new System.Windows.Forms.ListBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.grpFunc = new System.Windows.Forms.GroupBox();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.grpRoleFunc = new System.Windows.Forms.GroupBox();
            this.lstTotalFunc = new System.Windows.Forms.ListBox();
            this.lstRoleFunc = new System.Windows.Forms.ListBox();
            this.btnRemoveFunc = new System.Windows.Forms.Button();
            this.btnAddFunc = new System.Windows.Forms.Button();
            this.grpFunc.SuspendLayout();
            this.grpRoleFunc.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblUserID
            // 
            this.lblUserID.AutoSize = true;
            this.lblUserID.Font = new System.Drawing.Font("新宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblUserID.Location = new System.Drawing.Point(3, 21);
            this.lblUserID.Name = "lblUserID";
            this.lblUserID.Size = new System.Drawing.Size(70, 14);
            this.lblUserID.TabIndex = 11;
            this.lblUserID.Text = "Role Name";
            // 
            // lblRemark
            // 
            this.lblRemark.AutoSize = true;
            this.lblRemark.Font = new System.Drawing.Font("新宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblRemark.Location = new System.Drawing.Point(3, 67);
            this.lblRemark.Name = "lblRemark";
            this.lblRemark.Size = new System.Drawing.Size(84, 14);
            this.lblRemark.TabIndex = 17;
            this.lblRemark.Text = "Description";
            // 
            // txtRemark
            // 
            this.txtRemark.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtRemark.Location = new System.Drawing.Point(6, 84);
            this.txtRemark.Multiline = true;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtRemark.Size = new System.Drawing.Size(200, 65);
            this.txtRemark.TabIndex = 16;
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnExit.Location = new System.Drawing.Point(214, 214);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 30);
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
            this.txtStates.Size = new System.Drawing.Size(688, 27);
            this.txtStates.TabIndex = 23;
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnAdd.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAdd.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnAdd.Location = new System.Drawing.Point(2, 214);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(64, 30);
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
            this.lblGlobal.Size = new System.Drawing.Size(69, 19);
            this.lblGlobal.TabIndex = 51;
            this.lblGlobal.Text = "Role List";
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
            this.btnOK.Location = new System.Drawing.Point(70, 155);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 31);
            this.btnOK.TabIndex = 54;
            this.btnOK.Text = "Save";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // grpFunc
            // 
            this.grpFunc.Controls.Add(this.btnOK);
            this.grpFunc.Controls.Add(this.txtUser);
            this.grpFunc.Controls.Add(this.lblRemark);
            this.grpFunc.Controls.Add(this.txtRemark);
            this.grpFunc.Controls.Add(this.lblUserID);
            this.grpFunc.Font = new System.Drawing.Font("新宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grpFunc.Location = new System.Drawing.Point(144, 16);
            this.grpFunc.Name = "grpFunc";
            this.grpFunc.Size = new System.Drawing.Size(212, 192);
            this.grpFunc.TabIndex = 55;
            this.grpFunc.TabStop = false;
            this.grpFunc.Text = "Role Info";
            // 
            // txtUser
            // 
            this.txtUser.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtUser.Location = new System.Drawing.Point(6, 38);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(200, 26);
            this.txtUser.TabIndex = 19;
            // 
            // grpRoleFunc
            // 
            this.grpRoleFunc.Controls.Add(this.lstTotalFunc);
            this.grpRoleFunc.Controls.Add(this.lstRoleFunc);
            this.grpRoleFunc.Controls.Add(this.btnRemoveFunc);
            this.grpRoleFunc.Controls.Add(this.btnAddFunc);
            this.grpRoleFunc.Font = new System.Drawing.Font("新宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grpRoleFunc.Location = new System.Drawing.Point(362, 12);
            this.grpRoleFunc.Name = "grpRoleFunc";
            this.grpRoleFunc.Size = new System.Drawing.Size(328, 229);
            this.grpRoleFunc.TabIndex = 56;
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
            this.lstTotalFunc.Size = new System.Drawing.Size(124, 200);
            this.lstTotalFunc.TabIndex = 31;
            this.lstTotalFunc.SelectedIndexChanged += new System.EventHandler(this.lstTotalFunc_SelectedIndexChanged);
            // 
            // lstRoleFunc
            // 
            this.lstRoleFunc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.lstRoleFunc.Font = new System.Drawing.Font("新宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lstRoleFunc.FormattingEnabled = true;
            this.lstRoleFunc.ItemHeight = 14;
            this.lstRoleFunc.Location = new System.Drawing.Point(9, 19);
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
            // RoleInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(691, 282);
            this.Controls.Add(this.grpRoleFunc);
            this.Controls.Add(this.grpFunc);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.txtStates);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.lblGlobal);
            this.Controls.Add(this.currlst);
            this.MinimumSize = new System.Drawing.Size(707, 320);
            this.Name = "RoleInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RoleInfo";
            this.Load += new System.EventHandler(this.RoleInfo_Load);
            this.grpFunc.ResumeLayout(false);
            this.grpFunc.PerformLayout();
            this.grpRoleFunc.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblUserID;
        private System.Windows.Forms.Label lblRemark;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.TextBox txtStates;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Label lblGlobal;
        private System.Windows.Forms.ListBox currlst;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.GroupBox grpFunc;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.GroupBox grpRoleFunc;
        private System.Windows.Forms.ListBox lstTotalFunc;
        private System.Windows.Forms.ListBox lstRoleFunc;
        private System.Windows.Forms.Button btnRemoveFunc;
        private System.Windows.Forms.Button btnAddFunc;
    }
}
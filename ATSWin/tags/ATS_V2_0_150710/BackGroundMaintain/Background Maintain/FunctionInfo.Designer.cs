namespace Maintain
{
    partial class FunctionInfo
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
            this.lblPWD = new System.Windows.Forms.Label();
            this.txtPWD = new System.Windows.Forms.TextBox();
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
            this.txtItemName = new System.Windows.Forms.TextBox();
            this.lblItemName = new System.Windows.Forms.Label();
            this.txtAliasName = new System.Windows.Forms.TextBox();
            this.lblAliasName = new System.Windows.Forms.Label();
            this.lblBlockType = new System.Windows.Forms.Label();
            this.cboType = new System.Windows.Forms.ComboBox();
            this.grpFunc.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblPWD
            // 
            this.lblPWD.AutoSize = true;
            this.lblPWD.Font = new System.Drawing.Font("新宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPWD.Location = new System.Drawing.Point(14, 53);
            this.lblPWD.Name = "lblPWD";
            this.lblPWD.Size = new System.Drawing.Size(91, 14);
            this.lblPWD.TabIndex = 13;
            this.lblPWD.Text = "FunctionCode";
            // 
            // txtPWD
            // 
            this.txtPWD.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPWD.Location = new System.Drawing.Point(123, 50);
            this.txtPWD.Name = "txtPWD";
            this.txtPWD.Size = new System.Drawing.Size(159, 26);
            this.txtPWD.TabIndex = 12;
            this.txtPWD.Text = "1";
            // 
            // lblUserID
            // 
            this.lblUserID.AutoSize = true;
            this.lblUserID.Font = new System.Drawing.Font("新宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblUserID.Location = new System.Drawing.Point(14, 21);
            this.lblUserID.Name = "lblUserID";
            this.lblUserID.Size = new System.Drawing.Size(91, 14);
            this.lblUserID.TabIndex = 11;
            this.lblUserID.Text = "FunctionName";
            this.lblUserID.DoubleClick += new System.EventHandler(this.lblUserID_DoubleClick);
            // 
            // lblRemark
            // 
            this.lblRemark.AutoSize = true;
            this.lblRemark.Font = new System.Drawing.Font("新宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblRemark.Location = new System.Drawing.Point(14, 85);
            this.lblRemark.Name = "lblRemark";
            this.lblRemark.Size = new System.Drawing.Size(84, 14);
            this.lblRemark.TabIndex = 17;
            this.lblRemark.Text = "Description";
            // 
            // txtRemark
            // 
            this.txtRemark.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtRemark.Location = new System.Drawing.Point(17, 102);
            this.txtRemark.Multiline = true;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtRemark.Size = new System.Drawing.Size(265, 41);
            this.txtRemark.TabIndex = 16;
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnExit.Location = new System.Drawing.Point(293, 303);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 28);
            this.btnExit.TabIndex = 22;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // txtStates
            // 
            this.txtStates.Location = new System.Drawing.Point(2, 336);
            this.txtStates.Multiline = true;
            this.txtStates.Name = "txtStates";
            this.txtStates.Size = new System.Drawing.Size(481, 27);
            this.txtStates.TabIndex = 23;
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnAdd.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAdd.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnAdd.Location = new System.Drawing.Point(2, 300);
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
            this.btnRemove.Location = new System.Drawing.Point(105, 300);
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
            this.lblGlobal.Size = new System.Drawing.Size(99, 19);
            this.lblGlobal.TabIndex = 51;
            this.lblGlobal.Text = "Function List";
            // 
            // currlst
            // 
            this.currlst.FormattingEnabled = true;
            this.currlst.HorizontalScrollbar = true;
            this.currlst.ItemHeight = 12;
            this.currlst.Items.AddRange(new object[] {
            "Function1",
            "Function2",
            "...",
            "FunctionN"});
            this.currlst.Location = new System.Drawing.Point(2, 28);
            this.currlst.Name = "currlst";
            this.currlst.ScrollAlwaysVisible = true;
            this.currlst.Size = new System.Drawing.Size(167, 268);
            this.currlst.TabIndex = 50;
            this.currlst.SelectedIndexChanged += new System.EventHandler(this.currlst_SelectedIndexChanged);
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnOK.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOK.Location = new System.Drawing.Point(103, 257);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 31);
            this.btnOK.TabIndex = 54;
            this.btnOK.Text = "Save";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // grpFunc
            // 
            this.grpFunc.Controls.Add(this.cboType);
            this.grpFunc.Controls.Add(this.lblBlockType);
            this.grpFunc.Controls.Add(this.txtAliasName);
            this.grpFunc.Controls.Add(this.lblAliasName);
            this.grpFunc.Controls.Add(this.txtItemName);
            this.grpFunc.Controls.Add(this.lblItemName);
            this.grpFunc.Controls.Add(this.btnOK);
            this.grpFunc.Controls.Add(this.txtUser);
            this.grpFunc.Controls.Add(this.lblRemark);
            this.grpFunc.Controls.Add(this.txtRemark);
            this.grpFunc.Controls.Add(this.lblPWD);
            this.grpFunc.Controls.Add(this.txtPWD);
            this.grpFunc.Controls.Add(this.lblUserID);
            this.grpFunc.Font = new System.Drawing.Font("新宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grpFunc.Location = new System.Drawing.Point(190, 2);
            this.grpFunc.Name = "grpFunc";
            this.grpFunc.Size = new System.Drawing.Size(293, 294);
            this.grpFunc.TabIndex = 55;
            this.grpFunc.TabStop = false;
            this.grpFunc.Text = "Function Info";
            // 
            // txtUser
            // 
            this.txtUser.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtUser.Location = new System.Drawing.Point(123, 18);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(158, 26);
            this.txtUser.TabIndex = 19;
            // 
            // txtItemName
            // 
            this.txtItemName.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtItemName.Location = new System.Drawing.Point(123, 149);
            this.txtItemName.Name = "txtItemName";
            this.txtItemName.Size = new System.Drawing.Size(158, 26);
            this.txtItemName.TabIndex = 56;
            // 
            // lblItemName
            // 
            this.lblItemName.AutoSize = true;
            this.lblItemName.Font = new System.Drawing.Font("新宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblItemName.Location = new System.Drawing.Point(14, 152);
            this.lblItemName.Name = "lblItemName";
            this.lblItemName.Size = new System.Drawing.Size(63, 14);
            this.lblItemName.TabIndex = 55;
            this.lblItemName.Text = "ItemName";
            // 
            // txtAliasName
            // 
            this.txtAliasName.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtAliasName.Location = new System.Drawing.Point(123, 181);
            this.txtAliasName.Name = "txtAliasName";
            this.txtAliasName.Size = new System.Drawing.Size(158, 26);
            this.txtAliasName.TabIndex = 58;
            // 
            // lblAliasName
            // 
            this.lblAliasName.AutoSize = true;
            this.lblAliasName.Font = new System.Drawing.Font("新宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblAliasName.Location = new System.Drawing.Point(14, 184);
            this.lblAliasName.Name = "lblAliasName";
            this.lblAliasName.Size = new System.Drawing.Size(70, 14);
            this.lblAliasName.TabIndex = 57;
            this.lblAliasName.Text = "AliasName";
            // 
            // lblBlockType
            // 
            this.lblBlockType.AutoSize = true;
            this.lblBlockType.Font = new System.Drawing.Font("新宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblBlockType.Location = new System.Drawing.Point(14, 216);
            this.lblBlockType.Name = "lblBlockType";
            this.lblBlockType.Size = new System.Drawing.Size(84, 14);
            this.lblBlockType.TabIndex = 59;
            this.lblBlockType.Text = "BlockTypeID";
            // 
            // cboType
            // 
            this.cboType.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboType.FormattingEnabled = true;
            this.cboType.Location = new System.Drawing.Point(123, 213);
            this.cboType.Name = "cboType";
            this.cboType.Size = new System.Drawing.Size(158, 28);
            this.cboType.TabIndex = 60;
            // 
            // FunctionInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 362);
            this.Controls.Add(this.grpFunc);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.txtStates);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.lblGlobal);
            this.Controls.Add(this.currlst);
            this.MinimumSize = new System.Drawing.Size(500, 320);
            this.Name = "FunctionInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FunctionInfo";
            this.Load += new System.EventHandler(this.FunctionInfo_Load);
            this.grpFunc.ResumeLayout(false);
            this.grpFunc.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblPWD;
        private System.Windows.Forms.TextBox txtPWD;
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
        private System.Windows.Forms.TextBox txtAliasName;
        private System.Windows.Forms.Label lblAliasName;
        private System.Windows.Forms.TextBox txtItemName;
        private System.Windows.Forms.Label lblItemName;
        private System.Windows.Forms.Label lblBlockType;
        private System.Windows.Forms.ComboBox cboType;
    }
}
namespace Maintain
{
    partial class NewPlanName
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
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblItemName = new System.Windows.Forms.Label();
            this.txtNewName = new System.Windows.Forms.TextBox();
            this.chkChangeDS = new System.Windows.Forms.CheckBox();
            this.rdoATSHome = new System.Windows.Forms.RadioButton();
            this.rdoATSDebug = new System.Windows.Forms.RadioButton();
            this.rdoLocal = new System.Windows.Forms.RadioButton();
            this.grpDS = new System.Windows.Forms.GroupBox();
            this.cboCtrlName = new System.Windows.Forms.ComboBox();
            this.lblCtrl = new System.Windows.Forms.Label();
            this.cboPlanName = new System.Windows.Forms.ComboBox();
            this.lblPlanName = new System.Windows.Forms.Label();
            this.cboPN = new System.Windows.Forms.ComboBox();
            this.lblPN = new System.Windows.Forms.Label();
            this.lblType = new System.Windows.Forms.Label();
            this.cboItemType = new System.Windows.Forms.ComboBox();
            this.grpDS.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(23, 224);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(81, 26);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(155, 224);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(81, 26);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblItemName
            // 
            this.lblItemName.AutoSize = true;
            this.lblItemName.Location = new System.Drawing.Point(22, 42);
            this.lblItemName.Name = "lblItemName";
            this.lblItemName.Size = new System.Drawing.Size(47, 12);
            this.lblItemName.TabIndex = 2;
            this.lblItemName.Text = "NewName";
            // 
            // txtNewName
            // 
            this.txtNewName.Location = new System.Drawing.Point(99, 39);
            this.txtNewName.Name = "txtNewName";
            this.txtNewName.Size = new System.Drawing.Size(137, 21);
            this.txtNewName.TabIndex = 3;
            this.txtNewName.TextChanged += new System.EventHandler(this.txtNewName_TextChanged);
            // 
            // chkChangeDS
            // 
            this.chkChangeDS.AutoSize = true;
            this.chkChangeDS.Location = new System.Drawing.Point(23, 13);
            this.chkChangeDS.Name = "chkChangeDS";
            this.chkChangeDS.Size = new System.Drawing.Size(204, 16);
            this.chkChangeDS.TabIndex = 4;
            this.chkChangeDS.Text = "Get Plan from other DataSource";
            this.chkChangeDS.UseVisualStyleBackColor = true;
            this.chkChangeDS.CheckedChanged += new System.EventHandler(this.chkChangeDS_CheckedChanged);
            // 
            // rdoATSHome
            // 
            this.rdoATSHome.AutoSize = true;
            this.rdoATSHome.Location = new System.Drawing.Point(5, 15);
            this.rdoATSHome.Name = "rdoATSHome";
            this.rdoATSHome.Size = new System.Drawing.Size(65, 16);
            this.rdoATSHome.TabIndex = 0;
            this.rdoATSHome.TabStop = true;
            this.rdoATSHome.Text = "ATSHome";
            this.rdoATSHome.UseVisualStyleBackColor = true;
            this.rdoATSHome.CheckedChanged += new System.EventHandler(this.rdoATSHome_CheckedChanged);
            // 
            // rdoATSDebug
            // 
            this.rdoATSDebug.AutoSize = true;
            this.rdoATSDebug.Location = new System.Drawing.Point(76, 15);
            this.rdoATSDebug.Name = "rdoATSDebug";
            this.rdoATSDebug.Size = new System.Drawing.Size(71, 16);
            this.rdoATSDebug.TabIndex = 1;
            this.rdoATSDebug.TabStop = true;
            this.rdoATSDebug.Text = "ATSDebug";
            this.rdoATSDebug.UseVisualStyleBackColor = true;
            this.rdoATSDebug.CheckedChanged += new System.EventHandler(this.rdoATSDebug_CheckedChanged);
            // 
            // rdoLocal
            // 
            this.rdoLocal.AutoSize = true;
            this.rdoLocal.Location = new System.Drawing.Point(153, 15);
            this.rdoLocal.Name = "rdoLocal";
            this.rdoLocal.Size = new System.Drawing.Size(53, 16);
            this.rdoLocal.TabIndex = 2;
            this.rdoLocal.TabStop = true;
            this.rdoLocal.Text = "Local";
            this.rdoLocal.UseVisualStyleBackColor = true;
            this.rdoLocal.CheckedChanged += new System.EventHandler(this.rdoLocal_CheckedChanged);
            // 
            // grpDS
            // 
            this.grpDS.Controls.Add(this.cboCtrlName);
            this.grpDS.Controls.Add(this.lblCtrl);
            this.grpDS.Controls.Add(this.cboPlanName);
            this.grpDS.Controls.Add(this.lblPlanName);
            this.grpDS.Controls.Add(this.cboPN);
            this.grpDS.Controls.Add(this.lblPN);
            this.grpDS.Controls.Add(this.lblType);
            this.grpDS.Controls.Add(this.cboItemType);
            this.grpDS.Controls.Add(this.rdoLocal);
            this.grpDS.Controls.Add(this.rdoATSDebug);
            this.grpDS.Controls.Add(this.rdoATSHome);
            this.grpDS.Location = new System.Drawing.Point(23, 66);
            this.grpDS.Name = "grpDS";
            this.grpDS.Size = new System.Drawing.Size(213, 142);
            this.grpDS.TabIndex = 5;
            this.grpDS.TabStop = false;
            this.grpDS.Text = "DataSourceSetting";
            // 
            // cboCtrlName
            // 
            this.cboCtrlName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCtrlName.Enabled = false;
            this.cboCtrlName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboCtrlName.FormattingEnabled = true;
            this.cboCtrlName.Location = new System.Drawing.Point(45, 114);
            this.cboCtrlName.Name = "cboCtrlName";
            this.cboCtrlName.Size = new System.Drawing.Size(162, 20);
            this.cboCtrlName.TabIndex = 22;
            // 
            // lblCtrl
            // 
            this.lblCtrl.AutoSize = true;
            this.lblCtrl.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCtrl.Location = new System.Drawing.Point(2, 114);
            this.lblCtrl.Name = "lblCtrl";
            this.lblCtrl.Size = new System.Drawing.Size(28, 17);
            this.lblCtrl.TabIndex = 21;
            this.lblCtrl.Text = "Ctrl";
            this.lblCtrl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboPlanName
            // 
            this.cboPlanName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPlanName.Enabled = false;
            this.cboPlanName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboPlanName.FormattingEnabled = true;
            this.cboPlanName.Location = new System.Drawing.Point(45, 88);
            this.cboPlanName.Name = "cboPlanName";
            this.cboPlanName.Size = new System.Drawing.Size(162, 20);
            this.cboPlanName.TabIndex = 20;
            this.cboPlanName.SelectedIndexChanged += new System.EventHandler(this.cboPlanName_SelectedIndexChanged);
            // 
            // lblPlanName
            // 
            this.lblPlanName.AutoSize = true;
            this.lblPlanName.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPlanName.Location = new System.Drawing.Point(2, 88);
            this.lblPlanName.Name = "lblPlanName";
            this.lblPlanName.Size = new System.Drawing.Size(32, 17);
            this.lblPlanName.TabIndex = 19;
            this.lblPlanName.Text = "Plan";
            this.lblPlanName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboPN
            // 
            this.cboPN.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPN.Enabled = false;
            this.cboPN.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboPN.FormattingEnabled = true;
            this.cboPN.Location = new System.Drawing.Point(44, 62);
            this.cboPN.Name = "cboPN";
            this.cboPN.Size = new System.Drawing.Size(162, 20);
            this.cboPN.TabIndex = 18;
            this.cboPN.SelectedIndexChanged += new System.EventHandler(this.cboPN_SelectedIndexChanged);
            // 
            // lblPN
            // 
            this.lblPN.AutoSize = true;
            this.lblPN.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPN.Location = new System.Drawing.Point(2, 62);
            this.lblPN.Name = "lblPN";
            this.lblPN.Size = new System.Drawing.Size(29, 17);
            this.lblPN.TabIndex = 17;
            this.lblPN.Text = "P N";
            this.lblPN.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblType.Location = new System.Drawing.Point(2, 37);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(36, 17);
            this.lblType.TabIndex = 16;
            this.lblType.Text = "Type";
            this.lblType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboItemType
            // 
            this.cboItemType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboItemType.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboItemType.FormattingEnabled = true;
            this.cboItemType.Items.AddRange(new object[] {
            "QSFP",
            "XFP",
            "CFP",
            "SFP+"});
            this.cboItemType.Location = new System.Drawing.Point(44, 37);
            this.cboItemType.Name = "cboItemType";
            this.cboItemType.Size = new System.Drawing.Size(162, 20);
            this.cboItemType.TabIndex = 15;
            this.cboItemType.SelectedIndexChanged += new System.EventHandler(this.cboItemType_SelectedIndexChanged);
            // 
            // NewPlanName
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(256, 262);
            this.Controls.Add(this.grpDS);
            this.Controls.Add(this.chkChangeDS);
            this.Controls.Add(this.txtNewName);
            this.Controls.Add(this.lblItemName);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(272, 300);
            this.MinimizeBox = false;
            this.Name = "NewPlanName";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NewPlanName";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.NewPlanName_FormClosed);
            this.Load += new System.EventHandler(this.NewPlanName_Load);
            this.grpDS.ResumeLayout(false);
            this.grpDS.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblItemName;
        public System.Windows.Forms.TextBox txtNewName;
        private System.Windows.Forms.CheckBox chkChangeDS;
        private System.Windows.Forms.RadioButton rdoATSHome;
        private System.Windows.Forms.RadioButton rdoATSDebug;
        private System.Windows.Forms.RadioButton rdoLocal;
        private System.Windows.Forms.GroupBox grpDS;
        private System.Windows.Forms.ComboBox cboPlanName;
        private System.Windows.Forms.Label lblPlanName;
        private System.Windows.Forms.ComboBox cboPN;
        private System.Windows.Forms.Label lblPN;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.ComboBox cboItemType;
        private System.Windows.Forms.Label lblCtrl;
        public System.Windows.Forms.ComboBox cboCtrlName;
    }
}
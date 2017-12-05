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
            this.lblPNType = new System.Windows.Forms.Label();
            this.cboPNType = new System.Windows.Forms.ComboBox();
            this.lblItem = new System.Windows.Forms.Label();
            this.cboItem = new System.Windows.Forms.ComboBox();
            this.cboMCoef = new System.Windows.Forms.ComboBox();
            this.lblMCoef = new System.Windows.Forms.Label();
            this.grpDS.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(23, 187);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(81, 26);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(155, 187);
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
            this.txtNewName.Location = new System.Drawing.Point(85, 39);
            this.txtNewName.Name = "txtNewName";
            this.txtNewName.Size = new System.Drawing.Size(151, 21);
            this.txtNewName.TabIndex = 3;
            // 
            // chkChangeDS
            // 
            this.chkChangeDS.AutoSize = true;
            this.chkChangeDS.Enabled = false;
            this.chkChangeDS.Location = new System.Drawing.Point(23, 13);
            this.chkChangeDS.Name = "chkChangeDS";
            this.chkChangeDS.Size = new System.Drawing.Size(204, 16);
            this.chkChangeDS.TabIndex = 4;
            this.chkChangeDS.Text = "Get data from other DataSource";
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
            this.grpDS.Controls.Add(this.cboMCoef);
            this.grpDS.Controls.Add(this.lblMCoef);
            this.grpDS.Controls.Add(this.lblPNType);
            this.grpDS.Controls.Add(this.cboPNType);
            this.grpDS.Controls.Add(this.lblItem);
            this.grpDS.Controls.Add(this.cboItem);
            this.grpDS.Controls.Add(this.rdoLocal);
            this.grpDS.Controls.Add(this.rdoATSDebug);
            this.grpDS.Controls.Add(this.rdoATSHome);
            this.grpDS.Location = new System.Drawing.Point(23, 66);
            this.grpDS.Name = "grpDS";
            this.grpDS.Size = new System.Drawing.Size(213, 115);
            this.grpDS.TabIndex = 5;
            this.grpDS.TabStop = false;
            this.grpDS.Text = "DataSourceSetting";
            // 
            // lblPNType
            // 
            this.lblPNType.AutoSize = true;
            this.lblPNType.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPNType.Location = new System.Drawing.Point(6, 37);
            this.lblPNType.Name = "lblPNType";
            this.lblPNType.Size = new System.Drawing.Size(36, 17);
            this.lblPNType.TabIndex = 18;
            this.lblPNType.Text = "Type";
            this.lblPNType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboPNType
            // 
            this.cboPNType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPNType.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboPNType.FormattingEnabled = true;
            this.cboPNType.Items.AddRange(new object[] {
            "QSFP",
            "XFP",
            "CFP",
            "SFP+"});
            this.cboPNType.Location = new System.Drawing.Point(62, 37);
            this.cboPNType.Name = "cboPNType";
            this.cboPNType.Size = new System.Drawing.Size(144, 20);
            this.cboPNType.TabIndex = 17;
            this.cboPNType.SelectedIndexChanged += new System.EventHandler(this.cboPNType_SelectedIndexChanged);
            // 
            // lblItem
            // 
            this.lblItem.AutoSize = true;
            this.lblItem.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblItem.Location = new System.Drawing.Point(6, 64);
            this.lblItem.Name = "lblItem";
            this.lblItem.Size = new System.Drawing.Size(34, 17);
            this.lblItem.TabIndex = 16;
            this.lblItem.Text = "Item";
            this.lblItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboItem
            // 
            this.cboItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboItem.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboItem.FormattingEnabled = true;
            this.cboItem.Items.AddRange(new object[] {
            "QSFP",
            "XFP",
            "CFP",
            "SFP+"});
            this.cboItem.Location = new System.Drawing.Point(62, 64);
            this.cboItem.Name = "cboItem";
            this.cboItem.Size = new System.Drawing.Size(144, 20);
            this.cboItem.TabIndex = 15;
            this.cboItem.SelectedIndexChanged += new System.EventHandler(this.cboItem_SelectedIndexChanged);
            // 
            // cboMCoef
            // 
            this.cboMCoef.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMCoef.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboMCoef.FormattingEnabled = true;
            this.cboMCoef.Location = new System.Drawing.Point(62, 87);
            this.cboMCoef.Name = "cboMCoef";
            this.cboMCoef.Size = new System.Drawing.Size(144, 22);
            this.cboMCoef.TabIndex = 34;
            // 
            // lblMCoef
            // 
            this.lblMCoef.AutoSize = true;
            this.lblMCoef.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblMCoef.Location = new System.Drawing.Point(7, 92);
            this.lblMCoef.Name = "lblMCoef";
            this.lblMCoef.Size = new System.Drawing.Size(35, 12);
            this.lblMCoef.TabIndex = 33;
            this.lblMCoef.Text = "MCoef";
            // 
            // NewPlanName
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(256, 219);
            this.Controls.Add(this.grpDS);
            this.Controls.Add(this.chkChangeDS);
            this.Controls.Add(this.txtNewName);
            this.Controls.Add(this.lblItemName);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewPlanName";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NewName";
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
        private System.Windows.Forms.Label lblItem;
        private System.Windows.Forms.ComboBox cboItem;
        private System.Windows.Forms.Label lblPNType;
        private System.Windows.Forms.ComboBox cboPNType;
        private System.Windows.Forms.ComboBox cboMCoef;
        private System.Windows.Forms.Label lblMCoef;
    }
}
namespace Maintain
{
    partial class CopyLstFNmItem
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
            this.lblPNType = new System.Windows.Forms.Label();
            this.cboPNType = new System.Windows.Forms.ComboBox();
            this.cboPN = new System.Windows.Forms.ComboBox();
            this.lblPN = new System.Windows.Forms.Label();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.lblFileName = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.lstFileName = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblPNType
            // 
            this.lblPNType.AutoSize = true;
            this.lblPNType.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPNType.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblPNType.Location = new System.Drawing.Point(12, 9);
            this.lblPNType.Name = "lblPNType";
            this.lblPNType.Size = new System.Drawing.Size(43, 19);
            this.lblPNType.TabIndex = 31;
            this.lblPNType.Text = "Type";
            // 
            // cboPNType
            // 
            this.cboPNType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPNType.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboPNType.ForeColor = System.Drawing.Color.DarkBlue;
            this.cboPNType.FormattingEnabled = true;
            this.cboPNType.Location = new System.Drawing.Point(62, 5);
            this.cboPNType.Name = "cboPNType";
            this.cboPNType.Size = new System.Drawing.Size(186, 28);
            this.cboPNType.TabIndex = 29;
            this.cboPNType.SelectedIndexChanged += new System.EventHandler(this.cboPNType_SelectedIndexChanged);
            // 
            // cboPN
            // 
            this.cboPN.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPN.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboPN.ForeColor = System.Drawing.Color.DarkBlue;
            this.cboPN.FormattingEnabled = true;
            this.cboPN.Location = new System.Drawing.Point(61, 39);
            this.cboPN.Name = "cboPN";
            this.cboPN.Size = new System.Drawing.Size(186, 28);
            this.cboPN.TabIndex = 30;
            this.cboPN.SelectedIndexChanged += new System.EventHandler(this.cboPN_SelectedIndexChanged);
            // 
            // lblPN
            // 
            this.lblPN.AutoSize = true;
            this.lblPN.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPN.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblPN.Location = new System.Drawing.Point(12, 43);
            this.lblPN.Name = "lblPN";
            this.lblPN.Size = new System.Drawing.Size(30, 19);
            this.lblPN.TabIndex = 32;
            this.lblPN.Text = "PN";
            // 
            // txtFileName
            // 
            this.txtFileName.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtFileName.Location = new System.Drawing.Point(16, 201);
            this.txtFileName.MaxLength = 30;
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(231, 26);
            this.txtFileName.TabIndex = 36;
            // 
            // lblFileName
            // 
            this.lblFileName.AutoSize = true;
            this.lblFileName.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblFileName.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblFileName.Location = new System.Drawing.Point(12, 179);
            this.lblFileName.Name = "lblFileName";
            this.lblFileName.Size = new System.Drawing.Size(115, 19);
            this.lblFileName.TabIndex = 35;
            this.lblFileName.Text = "New File Name";
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCancel.Location = new System.Drawing.Point(144, 233);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(71, 30);
            this.btnCancel.TabIndex = 34;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOK.Location = new System.Drawing.Point(36, 233);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(71, 30);
            this.btnOK.TabIndex = 33;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lstFileName
            // 
            this.lstFileName.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lstFileName.FormattingEnabled = true;
            this.lstFileName.ItemHeight = 20;
            this.lstFileName.Items.AddRange(new object[] {
            "Test1",
            "Test2"});
            this.lstFileName.Location = new System.Drawing.Point(16, 92);
            this.lstFileName.Name = "lstFileName";
            this.lstFileName.Size = new System.Drawing.Size(232, 84);
            this.lstFileName.TabIndex = 37;
            this.lstFileName.SelectedIndexChanged += new System.EventHandler(this.lstFileName_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.DarkBlue;
            this.label1.Location = new System.Drawing.Point(12, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(138, 19);
            this.label1.TabIndex = 38;
            this.label1.Text = "Existing File Name";
            // 
            // CopyLstFNmItem
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(266, 273);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lstFileName);
            this.Controls.Add(this.txtFileName);
            this.Controls.Add(this.lblFileName);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lblPNType);
            this.Controls.Add(this.cboPNType);
            this.Controls.Add(this.cboPN);
            this.Controls.Add(this.lblPN);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(282, 311);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(282, 311);
            this.Name = "CopyLstFNmItem";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Copy To...";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblPNType;
        private System.Windows.Forms.ComboBox cboPNType;
        private System.Windows.Forms.ComboBox cboPN;
        private System.Windows.Forms.Label lblPN;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.Label lblFileName;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.ListBox lstFileName;
        private System.Windows.Forms.Label label1;
    }
}
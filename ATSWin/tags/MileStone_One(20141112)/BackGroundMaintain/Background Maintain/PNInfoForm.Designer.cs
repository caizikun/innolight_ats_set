namespace GlobalInfo
{
    partial class PNInfoForm
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
            this.lblTypeName = new System.Windows.Forms.Label();
            this.grpType = new System.Windows.Forms.GroupBox();
            this.cboTsensors = new System.Windows.Forms.ComboBox();
            this.cboVoltages = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cboMGroup = new System.Windows.Forms.ComboBox();
            this.txtAuxAttribles = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtItemName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cboChannels = new System.Windows.Forms.ComboBox();
            this.txtPN = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.currlst = new System.Windows.Forms.ListBox();
            this.btnPreviousPage = new System.Windows.Forms.Button();
            this.btnInitInfo = new System.Windows.Forms.Button();
            this.grpType.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTypeName
            // 
            this.lblTypeName.AutoSize = true;
            this.lblTypeName.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTypeName.Location = new System.Drawing.Point(71, 2);
            this.lblTypeName.Name = "lblTypeName";
            this.lblTypeName.Size = new System.Drawing.Size(63, 14);
            this.lblTypeName.TabIndex = 26;
            this.lblTypeName.Text = "PN List";
            // 
            // grpType
            // 
            this.grpType.Controls.Add(this.cboTsensors);
            this.grpType.Controls.Add(this.cboVoltages);
            this.grpType.Controls.Add(this.label9);
            this.grpType.Controls.Add(this.label4);
            this.grpType.Controls.Add(this.cboMGroup);
            this.grpType.Controls.Add(this.txtAuxAttribles);
            this.grpType.Controls.Add(this.label8);
            this.grpType.Controls.Add(this.label7);
            this.grpType.Controls.Add(this.txtItemName);
            this.grpType.Controls.Add(this.label2);
            this.grpType.Controls.Add(this.cboChannels);
            this.grpType.Controls.Add(this.txtPN);
            this.grpType.Controls.Add(this.btnOK);
            this.grpType.Controls.Add(this.label6);
            this.grpType.Controls.Add(this.label5);
            this.grpType.Controls.Add(this.label3);
            this.grpType.Controls.Add(this.label1);
            this.grpType.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grpType.Location = new System.Drawing.Point(219, 2);
            this.grpType.Name = "grpType";
            this.grpType.Size = new System.Drawing.Size(234, 309);
            this.grpType.TabIndex = 25;
            this.grpType.TabStop = false;
            this.grpType.Text = "PNInfo";
            // 
            // cboTsensors
            // 
            this.cboTsensors.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboTsensors.FormattingEnabled = true;
            this.cboTsensors.Location = new System.Drawing.Point(88, 154);
            this.cboTsensors.Name = "cboTsensors";
            this.cboTsensors.Size = new System.Drawing.Size(140, 22);
            this.cboTsensors.TabIndex = 37;
            this.cboTsensors.Leave += new System.EventHandler(this.cboTsensors_Leave);
            // 
            // cboVoltages
            // 
            this.cboVoltages.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboVoltages.FormattingEnabled = true;
            this.cboVoltages.Location = new System.Drawing.Point(88, 125);
            this.cboVoltages.Name = "cboVoltages";
            this.cboVoltages.Size = new System.Drawing.Size(140, 22);
            this.cboVoltages.TabIndex = 36;
            this.cboVoltages.Leave += new System.EventHandler(this.cboVoltages_Leave);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(8, 159);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 35;
            this.label9.Text = "Tsensors";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(7, 130);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 33;
            this.label4.Text = "Voltages";
            // 
            // cboMGroup
            // 
            this.cboMGroup.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboMGroup.FormattingEnabled = true;
            this.cboMGroup.Location = new System.Drawing.Point(88, 182);
            this.cboMGroup.Name = "cboMGroup";
            this.cboMGroup.Size = new System.Drawing.Size(140, 22);
            this.cboMGroup.TabIndex = 32;
            this.cboMGroup.SelectedIndexChanged += new System.EventHandler(this.cboMGroup_SelectedIndexChanged);
            // 
            // txtAuxAttribles
            // 
            this.txtAuxAttribles.Font = new System.Drawing.Font("新宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtAuxAttribles.Location = new System.Drawing.Point(10, 229);
            this.txtAuxAttribles.Multiline = true;
            this.txtAuxAttribles.Name = "txtAuxAttribles";
            this.txtAuxAttribles.Size = new System.Drawing.Size(218, 45);
            this.txtAuxAttribles.TabIndex = 31;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(7, 215);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 12);
            this.label8.TabIndex = 30;
            this.label8.Text = "AuxAttribles";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(8, 187);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 28;
            this.label7.Text = "MCoefGroup";
            // 
            // txtItemName
            // 
            this.txtItemName.Location = new System.Drawing.Point(88, 68);
            this.txtItemName.Name = "txtItemName";
            this.txtItemName.Size = new System.Drawing.Size(140, 23);
            this.txtItemName.TabIndex = 25;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(7, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 24;
            this.label2.Text = "ItemName";
            // 
            // cboChannels
            // 
            this.cboChannels.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboChannels.FormattingEnabled = true;
            this.cboChannels.Location = new System.Drawing.Point(88, 97);
            this.cboChannels.Name = "cboChannels";
            this.cboChannels.Size = new System.Drawing.Size(140, 22);
            this.cboChannels.TabIndex = 23;
            // 
            // txtPN
            // 
            this.txtPN.Location = new System.Drawing.Point(88, 39);
            this.txtPN.Name = "txtPN";
            this.txtPN.Size = new System.Drawing.Size(140, 23);
            this.txtPN.TabIndex = 22;
            // 
            // btnOK
            // 
            this.btnOK.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOK.Location = new System.Drawing.Point(88, 280);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 21;
            this.btnOK.Text = "Save";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(112, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 9;
            this.label6.Text = "内容";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(7, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "项  目";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(6, 102);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "Channels";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(7, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "P  N";
            // 
            // currlst
            // 
            this.currlst.FormattingEnabled = true;
            this.currlst.HorizontalScrollbar = true;
            this.currlst.ItemHeight = 12;
            this.currlst.Items.AddRange(new object[] {
            "TN-X-X-X",
            "TN-2-X-X"});
            this.currlst.Location = new System.Drawing.Point(6, 21);
            this.currlst.Name = "currlst";
            this.currlst.ScrollAlwaysVisible = true;
            this.currlst.Size = new System.Drawing.Size(207, 292);
            this.currlst.TabIndex = 24;
            this.currlst.SelectedIndexChanged += new System.EventHandler(this.currlst_SelectedIndexChanged);
            // 
            // btnPreviousPage
            // 
            this.btnPreviousPage.Location = new System.Drawing.Point(113, 317);
            this.btnPreviousPage.Name = "btnPreviousPage";
            this.btnPreviousPage.Size = new System.Drawing.Size(100, 28);
            this.btnPreviousPage.TabIndex = 27;
            this.btnPreviousPage.Text = "Return";
            this.btnPreviousPage.UseVisualStyleBackColor = true;
            this.btnPreviousPage.Click += new System.EventHandler(this.btnPreviousPage_Click);
            // 
            // btnInitInfo
            // 
            this.btnInitInfo.Enabled = false;
            this.btnInitInfo.Location = new System.Drawing.Point(271, 317);
            this.btnInitInfo.Name = "btnInitInfo";
            this.btnInitInfo.Size = new System.Drawing.Size(100, 28);
            this.btnInitInfo.TabIndex = 28;
            this.btnInitInfo.Text = "ChipsetInfo";
            this.btnInitInfo.UseVisualStyleBackColor = true;
            this.btnInitInfo.Click += new System.EventHandler(this.btnInitInfo_Click);
            // 
            // PNInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(465, 349);
            this.Controls.Add(this.btnInitInfo);
            this.Controls.Add(this.btnPreviousPage);
            this.Controls.Add(this.lblTypeName);
            this.Controls.Add(this.grpType);
            this.Controls.Add(this.currlst);
            this.Name = "PNInfoForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PNInfoForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PNInfoForm_FormClosing);
            this.Load += new System.EventHandler(this.PNInfoForm_Load);
            this.grpType.ResumeLayout(false);
            this.grpType.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTypeName;
        private System.Windows.Forms.GroupBox grpType;
        private System.Windows.Forms.TextBox txtPN;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox currlst;
        private System.Windows.Forms.ComboBox cboChannels;
        private System.Windows.Forms.Button btnPreviousPage;
        private System.Windows.Forms.TextBox txtAuxAttribles;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtItemName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboMGroup;
        private System.Windows.Forms.ComboBox cboTsensors;
        private System.Windows.Forms.ComboBox cboVoltages;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnInitInfo;
    }
}
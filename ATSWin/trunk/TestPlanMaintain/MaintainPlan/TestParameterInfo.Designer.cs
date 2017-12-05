namespace Maintain
{
    partial class TestParameterInfo
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
            this.label9 = new System.Windows.Forms.Label();
            this.grpItem = new System.Windows.Forms.GroupBox();
            this.cboValue = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtItemName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cboItemType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cboItemName = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.currlst = new System.Windows.Forms.ListBox();
            this.grpGlobalPmrtr = new System.Windows.Forms.GroupBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.txtGlobalPrmtrValue = new System.Windows.Forms.TextBox();
            this.txtGlobalPrmtrType = new System.Windows.Forms.TextBox();
            this.txtGlobalPrmtrName = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.txtGlobalModelName = new System.Windows.Forms.TextBox();
            this.btnFinish = new System.Windows.Forms.Button();
            this.lblChanged = new System.Windows.Forms.Label();
            this.chkOK = new System.Windows.Forms.CheckBox();
            this.txtPrmtrDescription = new System.Windows.Forms.TextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.grpItem.SuspendLayout();
            this.grpGlobalPmrtr.SuspendLayout();
            this.SuspendLayout();
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label9.Location = new System.Drawing.Point(69, 2);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(143, 14);
            this.label9.TabIndex = 51;
            this.label9.Text = "CurrParameterList";
            // 
            // grpItem
            // 
            this.grpItem.Controls.Add(this.cboValue);
            this.grpItem.Controls.Add(this.label4);
            this.grpItem.Controls.Add(this.label10);
            this.grpItem.Controls.Add(this.txtItemName);
            this.grpItem.Controls.Add(this.label6);
            this.grpItem.Controls.Add(this.label5);
            this.grpItem.Controls.Add(this.cboItemType);
            this.grpItem.Controls.Add(this.label2);
            this.grpItem.Controls.Add(this.cboItemName);
            this.grpItem.Controls.Add(this.label1);
            this.grpItem.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grpItem.Location = new System.Drawing.Point(244, 4);
            this.grpItem.Name = "grpItem";
            this.grpItem.Size = new System.Drawing.Size(285, 245);
            this.grpItem.TabIndex = 47;
            this.grpItem.TabStop = false;
            this.grpItem.Text = "CurrParameters(TopoTable)";
            // 
            // cboValue
            // 
            this.cboValue.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboValue.FormattingEnabled = true;
            this.cboValue.Location = new System.Drawing.Point(72, 166);
            this.cboValue.Name = "cboValue";
            this.cboValue.Size = new System.Drawing.Size(206, 20);
            this.cboValue.TabIndex = 17;
            this.cboValue.Leave += new System.EventHandler(this.cboValue_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(3, 169);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 16;
            this.label4.Text = "ItemValue";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label10.Location = new System.Drawing.Point(3, 23);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(59, 12);
            this.label10.TabIndex = 13;
            this.label10.Text = "ModelName";
            // 
            // txtItemName
            // 
            this.txtItemName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtItemName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtItemName.Enabled = false;
            this.txtItemName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtItemName.Location = new System.Drawing.Point(72, 20);
            this.txtItemName.Name = "txtItemName";
            this.txtItemName.Size = new System.Drawing.Size(189, 21);
            this.txtItemName.TabIndex = 12;
            this.txtItemName.Text = "ModelName";
            this.txtItemName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label6.Location = new System.Drawing.Point(163, 57);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 14);
            this.label6.TabIndex = 9;
            this.label6.Text = "Value";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label5.Location = new System.Drawing.Point(16, 57);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 14);
            this.label5.TabIndex = 8;
            this.label5.Text = "Item";
            // 
            // cboItemType
            // 
            this.cboItemType.Enabled = false;
            this.cboItemType.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboItemType.FormattingEnabled = true;
            this.cboItemType.Location = new System.Drawing.Point(73, 120);
            this.cboItemType.Name = "cboItemType";
            this.cboItemType.Size = new System.Drawing.Size(206, 20);
            this.cboItemType.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(7, 123);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "DataType";
            // 
            // cboItemName
            // 
            this.cboItemName.Enabled = false;
            this.cboItemName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboItemName.FormattingEnabled = true;
            this.cboItemName.Location = new System.Drawing.Point(73, 79);
            this.cboItemName.Name = "cboItemName";
            this.cboItemName.Size = new System.Drawing.Size(206, 20);
            this.cboItemName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(7, 82);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "ItemName";
            // 
            // currlst
            // 
            this.currlst.FormattingEnabled = true;
            this.currlst.HorizontalScrollbar = true;
            this.currlst.ItemHeight = 12;
            this.currlst.Items.AddRange(new object[] {
            "Parameter1",
            "Parameter2",
            "Parameter3",
            "Parameter4",
            "...",
            "Parameter*"});
            this.currlst.Location = new System.Drawing.Point(36, 19);
            this.currlst.Name = "currlst";
            this.currlst.ScrollAlwaysVisible = true;
            this.currlst.Size = new System.Drawing.Size(202, 304);
            this.currlst.TabIndex = 46;
            this.currlst.SelectedIndexChanged += new System.EventHandler(this.currlst_SelectedIndexChanged);
            // 
            // grpGlobalPmrtr
            // 
            this.grpGlobalPmrtr.Controls.Add(this.txtPrmtrDescription);
            this.grpGlobalPmrtr.Controls.Add(this.label28);
            this.grpGlobalPmrtr.Controls.Add(this.label16);
            this.grpGlobalPmrtr.Controls.Add(this.label24);
            this.grpGlobalPmrtr.Controls.Add(this.label25);
            this.grpGlobalPmrtr.Controls.Add(this.label26);
            this.grpGlobalPmrtr.Controls.Add(this.label27);
            this.grpGlobalPmrtr.Controls.Add(this.txtGlobalPrmtrValue);
            this.grpGlobalPmrtr.Controls.Add(this.txtGlobalPrmtrType);
            this.grpGlobalPmrtr.Controls.Add(this.txtGlobalPrmtrName);
            this.grpGlobalPmrtr.Controls.Add(this.label23);
            this.grpGlobalPmrtr.Controls.Add(this.txtGlobalModelName);
            this.grpGlobalPmrtr.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grpGlobalPmrtr.Location = new System.Drawing.Point(535, 4);
            this.grpGlobalPmrtr.Name = "grpGlobalPmrtr";
            this.grpGlobalPmrtr.Size = new System.Drawing.Size(278, 321);
            this.grpGlobalPmrtr.TabIndex = 52;
            this.grpGlobalPmrtr.TabStop = false;
            this.grpGlobalPmrtr.Text = "DefaultParameters(GlobalTable)";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label16.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label16.Location = new System.Drawing.Point(9, 169);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(59, 12);
            this.label16.TabIndex = 44;
            this.label16.Text = "ItemValue";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label24.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label24.Location = new System.Drawing.Point(167, 57);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(47, 14);
            this.label24.TabIndex = 42;
            this.label24.Text = "Value";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label25.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label25.Location = new System.Drawing.Point(17, 57);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(39, 14);
            this.label25.TabIndex = 41;
            this.label25.Text = "Item";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label26.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label26.Location = new System.Drawing.Point(9, 123);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(53, 12);
            this.label26.TabIndex = 40;
            this.label26.Text = "DataType";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label27.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label27.Location = new System.Drawing.Point(8, 82);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(53, 12);
            this.label27.TabIndex = 39;
            this.label27.Text = "ItemName";
            // 
            // txtGlobalPrmtrValue
            // 
            this.txtGlobalPrmtrValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtGlobalPrmtrValue.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtGlobalPrmtrValue.Location = new System.Drawing.Point(73, 157);
            this.txtGlobalPrmtrValue.Multiline = true;
            this.txtGlobalPrmtrValue.Name = "txtGlobalPrmtrValue";
            this.txtGlobalPrmtrValue.ReadOnly = true;
            this.txtGlobalPrmtrValue.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtGlobalPrmtrValue.Size = new System.Drawing.Size(200, 36);
            this.txtGlobalPrmtrValue.TabIndex = 32;
            this.txtGlobalPrmtrValue.Text = "Parameter";
            this.txtGlobalPrmtrValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtGlobalPrmtrType
            // 
            this.txtGlobalPrmtrType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtGlobalPrmtrType.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtGlobalPrmtrType.Location = new System.Drawing.Point(73, 120);
            this.txtGlobalPrmtrType.Name = "txtGlobalPrmtrType";
            this.txtGlobalPrmtrType.ReadOnly = true;
            this.txtGlobalPrmtrType.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtGlobalPrmtrType.Size = new System.Drawing.Size(200, 21);
            this.txtGlobalPrmtrType.TabIndex = 30;
            this.txtGlobalPrmtrType.Text = "Parameter";
            this.txtGlobalPrmtrType.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtGlobalPrmtrName
            // 
            this.txtGlobalPrmtrName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtGlobalPrmtrName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtGlobalPrmtrName.Location = new System.Drawing.Point(73, 79);
            this.txtGlobalPrmtrName.Name = "txtGlobalPrmtrName";
            this.txtGlobalPrmtrName.ReadOnly = true;
            this.txtGlobalPrmtrName.Size = new System.Drawing.Size(200, 21);
            this.txtGlobalPrmtrName.TabIndex = 29;
            this.txtGlobalPrmtrName.Text = "Parameter";
            this.txtGlobalPrmtrName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label23.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label23.Location = new System.Drawing.Point(3, 23);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(59, 12);
            this.label23.TabIndex = 13;
            this.label23.Text = "ModelName";
            // 
            // txtGlobalModelName
            // 
            this.txtGlobalModelName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGlobalModelName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtGlobalModelName.Enabled = false;
            this.txtGlobalModelName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtGlobalModelName.Location = new System.Drawing.Point(73, 20);
            this.txtGlobalModelName.Name = "txtGlobalModelName";
            this.txtGlobalModelName.Size = new System.Drawing.Size(188, 21);
            this.txtGlobalModelName.TabIndex = 12;
            this.txtGlobalModelName.Text = "GlobalModelName";
            this.txtGlobalModelName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnFinish
            // 
            this.btnFinish.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnFinish.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnFinish.Location = new System.Drawing.Point(317, 289);
            this.btnFinish.Name = "btnFinish";
            this.btnFinish.Size = new System.Drawing.Size(171, 36);
            this.btnFinish.TabIndex = 53;
            this.btnFinish.Text = "Finish";
            this.btnFinish.UseVisualStyleBackColor = false;
            this.btnFinish.Click += new System.EventHandler(this.btnFinish_Click);
            // 
            // lblChanged
            // 
            this.lblChanged.AutoSize = true;
            this.lblChanged.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblChanged.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblChanged.Location = new System.Drawing.Point(4, 4);
            this.lblChanged.Name = "lblChanged";
            this.lblChanged.Size = new System.Drawing.Size(59, 12);
            this.lblChanged.TabIndex = 57;
            this.lblChanged.Text = "WithData?";
            // 
            // chkOK
            // 
            this.chkOK.Enabled = false;
            this.chkOK.Location = new System.Drawing.Point(18, 19);
            this.chkOK.Name = "chkOK";
            this.chkOK.Size = new System.Drawing.Size(14, 11);
            this.chkOK.TabIndex = 56;
            this.chkOK.UseVisualStyleBackColor = true;
            // 
            // txtPrmtrDescription
            // 
            this.txtPrmtrDescription.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtPrmtrDescription.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPrmtrDescription.Location = new System.Drawing.Point(10, 228);
            this.txtPrmtrDescription.Multiline = true;
            this.txtPrmtrDescription.Name = "txtPrmtrDescription";
            this.txtPrmtrDescription.ReadOnly = true;
            this.txtPrmtrDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtPrmtrDescription.Size = new System.Drawing.Size(263, 87);
            this.txtPrmtrDescription.TabIndex = 87;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label28.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label28.Location = new System.Drawing.Point(8, 213);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(71, 12);
            this.label28.TabIndex = 86;
            this.label28.Text = "Description";
            // 
            // TestParameterInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(816, 332);
            this.Controls.Add(this.lblChanged);
            this.Controls.Add(this.chkOK);
            this.Controls.Add(this.btnFinish);
            this.Controls.Add(this.grpGlobalPmrtr);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.grpItem);
            this.Controls.Add(this.currlst);
            this.MaximumSize = new System.Drawing.Size(832, 370);
            this.Name = "TestParameterInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TestParameter";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TestParameterInfo_FormClosing);
            this.Load += new System.EventHandler(this.TestParameterInfo_Load);
            this.grpItem.ResumeLayout(false);
            this.grpItem.PerformLayout();
            this.grpGlobalPmrtr.ResumeLayout(false);
            this.grpGlobalPmrtr.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox grpItem;
        private System.Windows.Forms.ComboBox cboValue;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtItemName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboItemType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboItemName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox currlst;
        private System.Windows.Forms.GroupBox grpGlobalPmrtr;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox txtGlobalModelName;
        private System.Windows.Forms.TextBox txtGlobalPrmtrValue;
        private System.Windows.Forms.TextBox txtGlobalPrmtrType;
        private System.Windows.Forms.TextBox txtGlobalPrmtrName;
        private System.Windows.Forms.Button btnFinish;
        private System.Windows.Forms.Label lblChanged;
        private System.Windows.Forms.CheckBox chkOK;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TextBox txtPrmtrDescription;
        private System.Windows.Forms.Label label28;
    }
}
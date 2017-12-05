namespace Maintain
{
    partial class TestPlanForm
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
            this.currlst = new System.Windows.Forms.ListBox();
            this.grpItem = new System.Windows.Forms.GroupBox();
            this.lblDesc = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.chkMConfigInit = new System.Windows.Forms.CheckBox();
            this.lblMConfigInit = new System.Windows.Forms.Label();
            this.chkIgnortPlan = new System.Windows.Forms.CheckBox();
            this.lblIgnortPlan = new System.Windows.Forms.Label();
            this.chkIgnoreCoef = new System.Windows.Forms.CheckBox();
            this.lblIgnoreCoef = new System.Windows.Forms.Label();
            this.chkIsChipInit = new System.Windows.Forms.CheckBox();
            this.lblIsChipInit = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.txtTestPlanName = new System.Windows.Forms.TextBox();
            this.cboSNCheck = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cboUSBPort = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cboHWVersion = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboSWVersion = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cboItemName = new System.Windows.Forms.ComboBox();
            this.lblItemName = new System.Windows.Forms.Label();
            this.btnNextPage = new System.Windows.Forms.Button();
            this.btnPreviousPage = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.btnMConfigInit = new System.Windows.Forms.Button();
            this.grpItem.SuspendLayout();
            this.SuspendLayout();
            // 
            // currlst
            // 
            this.currlst.FormattingEnabled = true;
            this.currlst.HorizontalScrollbar = true;
            this.currlst.ItemHeight = 12;
            this.currlst.Items.AddRange(new object[] {
            "TestPlan1",
            "TestPlan2",
            "TestPlan3",
            "...",
            "TestPlan*"});
            this.currlst.Location = new System.Drawing.Point(5, 19);
            this.currlst.Name = "currlst";
            this.currlst.ScrollAlwaysVisible = true;
            this.currlst.Size = new System.Drawing.Size(150, 340);
            this.currlst.TabIndex = 3;
            this.currlst.SelectedIndexChanged += new System.EventHandler(this.currlst_SelectedIndexChanged);
            // 
            // grpItem
            // 
            this.grpItem.Controls.Add(this.lblDesc);
            this.grpItem.Controls.Add(this.txtDescription);
            this.grpItem.Controls.Add(this.chkMConfigInit);
            this.grpItem.Controls.Add(this.lblMConfigInit);
            this.grpItem.Controls.Add(this.chkIgnortPlan);
            this.grpItem.Controls.Add(this.lblIgnortPlan);
            this.grpItem.Controls.Add(this.chkIgnoreCoef);
            this.grpItem.Controls.Add(this.lblIgnoreCoef);
            this.grpItem.Controls.Add(this.chkIsChipInit);
            this.grpItem.Controls.Add(this.lblIsChipInit);
            this.grpItem.Controls.Add(this.btnOK);
            this.grpItem.Controls.Add(this.label10);
            this.grpItem.Controls.Add(this.txtTestPlanName);
            this.grpItem.Controls.Add(this.cboSNCheck);
            this.grpItem.Controls.Add(this.label7);
            this.grpItem.Controls.Add(this.cboUSBPort);
            this.grpItem.Controls.Add(this.label4);
            this.grpItem.Controls.Add(this.cboHWVersion);
            this.grpItem.Controls.Add(this.label3);
            this.grpItem.Controls.Add(this.cboSWVersion);
            this.grpItem.Controls.Add(this.label2);
            this.grpItem.Controls.Add(this.cboItemName);
            this.grpItem.Controls.Add(this.lblItemName);
            this.grpItem.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grpItem.Location = new System.Drawing.Point(164, 12);
            this.grpItem.Name = "grpItem";
            this.grpItem.Size = new System.Drawing.Size(290, 347);
            this.grpItem.TabIndex = 4;
            this.grpItem.TabStop = false;
            this.grpItem.Text = "TestPlan Info";
            // 
            // lblDesc
            // 
            this.lblDesc.AutoSize = true;
            this.lblDesc.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDesc.Location = new System.Drawing.Point(3, 285);
            this.lblDesc.Name = "lblDesc";
            this.lblDesc.Size = new System.Drawing.Size(71, 12);
            this.lblDesc.TabIndex = 34;
            this.lblDesc.Text = "Description";
            // 
            // txtDescription
            // 
            this.txtDescription.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtDescription.Location = new System.Drawing.Point(92, 265);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescription.Size = new System.Drawing.Size(192, 48);
            this.txtDescription.TabIndex = 33;
            // 
            // chkMConfigInit
            // 
            this.chkMConfigInit.AutoSize = true;
            this.chkMConfigInit.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkMConfigInit.ForeColor = System.Drawing.Color.Blue;
            this.chkMConfigInit.Location = new System.Drawing.Point(92, 204);
            this.chkMConfigInit.Name = "chkMConfigInit";
            this.chkMConfigInit.Size = new System.Drawing.Size(199, 16);
            this.chkMConfigInit.TabIndex = 32;
            this.chkMConfigInit.Text = "<MConfigInit is Disabled>";
            this.chkMConfigInit.UseVisualStyleBackColor = true;
            this.chkMConfigInit.CheckedChanged += new System.EventHandler(this.chkMConfigInit_CheckedChanged);
            // 
            // lblMConfigInit
            // 
            this.lblMConfigInit.AutoSize = true;
            this.lblMConfigInit.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblMConfigInit.Location = new System.Drawing.Point(3, 204);
            this.lblMConfigInit.Name = "lblMConfigInit";
            this.lblMConfigInit.Size = new System.Drawing.Size(83, 12);
            this.lblMConfigInit.TabIndex = 31;
            this.lblMConfigInit.Text = "IsMConfigInit";
            // 
            // chkIgnortPlan
            // 
            this.chkIgnortPlan.AutoSize = true;
            this.chkIgnortPlan.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkIgnortPlan.ForeColor = System.Drawing.Color.Blue;
            this.chkIgnortPlan.Location = new System.Drawing.Point(92, 243);
            this.chkIgnortPlan.Name = "chkIgnortPlan";
            this.chkIgnortPlan.Size = new System.Drawing.Size(171, 16);
            this.chkIgnortPlan.TabIndex = 30;
            this.chkIgnortPlan.Text = "<CurrItem is Enabled>";
            this.chkIgnortPlan.UseVisualStyleBackColor = true;
            this.chkIgnortPlan.CheckedChanged += new System.EventHandler(this.chkIgnortPlan_CheckedChanged);
            // 
            // lblIgnortPlan
            // 
            this.lblIgnortPlan.AutoSize = true;
            this.lblIgnortPlan.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblIgnortPlan.Location = new System.Drawing.Point(3, 243);
            this.lblIgnortPlan.Name = "lblIgnortPlan";
            this.lblIgnortPlan.Size = new System.Drawing.Size(83, 12);
            this.lblIgnortPlan.TabIndex = 29;
            this.lblIgnortPlan.Text = "PlanDisabled?";
            // 
            // chkIgnoreCoef
            // 
            this.chkIgnoreCoef.AutoSize = true;
            this.chkIgnoreCoef.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkIgnoreCoef.ForeColor = System.Drawing.Color.Blue;
            this.chkIgnoreCoef.Location = new System.Drawing.Point(92, 223);
            this.chkIgnoreCoef.Name = "chkIgnoreCoef";
            this.chkIgnoreCoef.Size = new System.Drawing.Size(185, 16);
            this.chkIgnoreCoef.TabIndex = 30;
            this.chkIgnoreCoef.Text = "<BackupCoef is Enabled>";
            this.chkIgnoreCoef.UseVisualStyleBackColor = true;
            this.chkIgnoreCoef.CheckedChanged += new System.EventHandler(this.chkIgnoreCoef_CheckedChanged);
            // 
            // lblIgnoreCoef
            // 
            this.lblIgnoreCoef.AutoSize = true;
            this.lblIgnoreCoef.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblIgnoreCoef.Location = new System.Drawing.Point(3, 223);
            this.lblIgnoreCoef.Name = "lblIgnoreCoef";
            this.lblIgnoreCoef.Size = new System.Drawing.Size(71, 12);
            this.lblIgnoreCoef.TabIndex = 29;
            this.lblIgnoreCoef.Text = "IgnoreCoef?";
            // 
            // chkIsChipInit
            // 
            this.chkIsChipInit.AutoSize = true;
            this.chkIsChipInit.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkIsChipInit.ForeColor = System.Drawing.Color.Blue;
            this.chkIsChipInit.Location = new System.Drawing.Point(92, 185);
            this.chkIsChipInit.Name = "chkIsChipInit";
            this.chkIsChipInit.Size = new System.Drawing.Size(178, 16);
            this.chkIsChipInit.TabIndex = 28;
            this.chkIsChipInit.Text = "<ChipInit is Disabled>";
            this.chkIsChipInit.UseVisualStyleBackColor = true;
            this.chkIsChipInit.CheckedChanged += new System.EventHandler(this.chkIsChipInit_CheckedChanged);
            // 
            // lblIsChipInit
            // 
            this.lblIsChipInit.AutoSize = true;
            this.lblIsChipInit.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblIsChipInit.Location = new System.Drawing.Point(3, 185);
            this.lblIsChipInit.Name = "lblIsChipInit";
            this.lblIsChipInit.Size = new System.Drawing.Size(65, 12);
            this.lblIsChipInit.TabIndex = 27;
            this.lblIsChipInit.Text = "IsChipInit";
            // 
            // btnOK
            // 
            this.btnOK.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOK.Location = new System.Drawing.Point(92, 319);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 14;
            this.btnOK.Text = "Save";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Enabled = false;
            this.label10.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(3, 23);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 12);
            this.label10.TabIndex = 13;
            this.label10.Text = "  P  N";
            // 
            // txtTestPlanName
            // 
            this.txtTestPlanName.Enabled = false;
            this.txtTestPlanName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtTestPlanName.Location = new System.Drawing.Point(92, 20);
            this.txtTestPlanName.Name = "txtTestPlanName";
            this.txtTestPlanName.Size = new System.Drawing.Size(192, 21);
            this.txtTestPlanName.TabIndex = 12;
            // 
            // cboSNCheck
            // 
            this.cboSNCheck.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSNCheck.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboSNCheck.FormattingEnabled = true;
            this.cboSNCheck.Items.AddRange(new object[] {
            "True",
            "False"});
            this.cboSNCheck.Location = new System.Drawing.Point(92, 159);
            this.cboSNCheck.Name = "cboSNCheck";
            this.cboSNCheck.Size = new System.Drawing.Size(192, 20);
            this.cboSNCheck.TabIndex = 11;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(6, 162);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 12);
            this.label7.TabIndex = 10;
            this.label7.Text = "SNCheck";
            // 
            // cboUSBPort
            // 
            this.cboUSBPort.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboUSBPort.FormattingEnabled = true;
            this.cboUSBPort.Location = new System.Drawing.Point(92, 133);
            this.cboUSBPort.Name = "cboUSBPort";
            this.cboUSBPort.Size = new System.Drawing.Size(192, 20);
            this.cboUSBPort.TabIndex = 7;
            this.cboUSBPort.Leave += new System.EventHandler(this.cboUSBPort_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(6, 136);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "USBPortNo";
            // 
            // cboHWVersion
            // 
            this.cboHWVersion.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboHWVersion.FormattingEnabled = true;
            this.cboHWVersion.Location = new System.Drawing.Point(92, 107);
            this.cboHWVersion.Name = "cboHWVersion";
            this.cboHWVersion.Size = new System.Drawing.Size(192, 20);
            this.cboHWVersion.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(6, 110);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "HWVersion";
            // 
            // cboSWVersion
            // 
            this.cboSWVersion.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboSWVersion.FormattingEnabled = true;
            this.cboSWVersion.Location = new System.Drawing.Point(92, 81);
            this.cboSWVersion.Name = "cboSWVersion";
            this.cboSWVersion.Size = new System.Drawing.Size(192, 20);
            this.cboSWVersion.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(6, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "SWVersion";
            // 
            // cboItemName
            // 
            this.cboItemName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboItemName.FormattingEnabled = true;
            this.cboItemName.Location = new System.Drawing.Point(92, 55);
            this.cboItemName.Name = "cboItemName";
            this.cboItemName.Size = new System.Drawing.Size(192, 20);
            this.cboItemName.TabIndex = 1;
            this.cboItemName.Leave += new System.EventHandler(this.cboItemName_Leave);
            // 
            // lblItemName
            // 
            this.lblItemName.AutoSize = true;
            this.lblItemName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.lblItemName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblItemName.Location = new System.Drawing.Point(6, 58);
            this.lblItemName.Name = "lblItemName";
            this.lblItemName.Size = new System.Drawing.Size(53, 12);
            this.lblItemName.TabIndex = 0;
            this.lblItemName.Text = "PlanName";
            this.lblItemName.DoubleClick += new System.EventHandler(this.lblItemName_DoubleClick);
            // 
            // btnNextPage
            // 
            this.btnNextPage.Location = new System.Drawing.Point(164, 370);
            this.btnNextPage.Name = "btnNextPage";
            this.btnNextPage.Size = new System.Drawing.Size(100, 27);
            this.btnNextPage.TabIndex = 7;
            this.btnNextPage.Text = "Next<Equip>";
            this.btnNextPage.UseVisualStyleBackColor = true;
            this.btnNextPage.Click += new System.EventHandler(this.btnNextPage_Click);
            // 
            // btnPreviousPage
            // 
            this.btnPreviousPage.Location = new System.Drawing.Point(26, 370);
            this.btnPreviousPage.Name = "btnPreviousPage";
            this.btnPreviousPage.Size = new System.Drawing.Size(100, 27);
            this.btnPreviousPage.TabIndex = 8;
            this.btnPreviousPage.Text = "Return";
            this.btnPreviousPage.UseVisualStyleBackColor = true;
            this.btnPreviousPage.Click += new System.EventHandler(this.btnPreviousPage_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(23, 2);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(111, 14);
            this.label9.TabIndex = 10;
            this.label9.Text = "TestPlan List";
            // 
            // btnMConfigInit
            // 
            this.btnMConfigInit.Location = new System.Drawing.Point(310, 370);
            this.btnMConfigInit.Name = "btnMConfigInit";
            this.btnMConfigInit.Size = new System.Drawing.Size(120, 27);
            this.btnMConfigInit.TabIndex = 7;
            this.btnMConfigInit.Text = "Next<MConfigInit>";
            this.btnMConfigInit.UseVisualStyleBackColor = true;
            this.btnMConfigInit.Click += new System.EventHandler(this.btnMConfigInit_Click);
            // 
            // TestPlanForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(454, 412);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.btnPreviousPage);
            this.Controls.Add(this.btnMConfigInit);
            this.Controls.Add(this.btnNextPage);
            this.Controls.Add(this.grpItem);
            this.Controls.Add(this.currlst);
            this.MaximumSize = new System.Drawing.Size(470, 450);
            this.Name = "TestPlanForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TestPlan";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TestPlanForm_FormClosing);
            this.Load += new System.EventHandler(this.TestPlan_Load);
            this.grpItem.ResumeLayout(false);
            this.grpItem.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox currlst;
        private System.Windows.Forms.GroupBox grpItem;
        private System.Windows.Forms.ComboBox cboSNCheck;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cboUSBPort;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboHWVersion;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboSWVersion;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboItemName;
        private System.Windows.Forms.Label lblItemName;
        private System.Windows.Forms.Button btnNextPage;
        private System.Windows.Forms.Button btnPreviousPage;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtTestPlanName;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.CheckBox chkIsChipInit;
        private System.Windows.Forms.Label lblIsChipInit;
        private System.Windows.Forms.CheckBox chkIgnortPlan;
        private System.Windows.Forms.Label lblIgnortPlan;
        private System.Windows.Forms.CheckBox chkIgnoreCoef;
        private System.Windows.Forms.Label lblIgnoreCoef;
        private System.Windows.Forms.CheckBox chkMConfigInit;
        private System.Windows.Forms.Label lblMConfigInit;
        private System.Windows.Forms.Button btnMConfigInit;
        private System.Windows.Forms.Label lblDesc;
        private System.Windows.Forms.TextBox txtDescription;
    }
}
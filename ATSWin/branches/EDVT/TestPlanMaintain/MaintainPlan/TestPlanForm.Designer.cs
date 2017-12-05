namespace TestPlan
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
            this.btnOK = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.txtTestPlanName = new System.Windows.Forms.TextBox();
            this.cboAuxAttribles = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
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
            this.currlst.Size = new System.Drawing.Size(150, 268);
            this.currlst.TabIndex = 3;
            this.currlst.SelectedIndexChanged += new System.EventHandler(this.currlst_SelectedIndexChanged);
            // 
            // grpItem
            // 
            this.grpItem.Controls.Add(this.btnOK);
            this.grpItem.Controls.Add(this.label10);
            this.grpItem.Controls.Add(this.txtTestPlanName);
            this.grpItem.Controls.Add(this.cboAuxAttribles);
            this.grpItem.Controls.Add(this.label7);
            this.grpItem.Controls.Add(this.label6);
            this.grpItem.Controls.Add(this.label5);
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
            this.grpItem.Size = new System.Drawing.Size(270, 275);
            this.grpItem.TabIndex = 4;
            this.grpItem.TabStop = false;
            this.grpItem.Text = "项目资料";
            // 
            // btnOK
            // 
            this.btnOK.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOK.Location = new System.Drawing.Point(100, 246);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 14;
            this.btnOK.Text = "保存";
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
            this.label10.Size = new System.Drawing.Size(53, 12);
            this.label10.TabIndex = 13;
            this.label10.Text = "父项名称";
            // 
            // txtTestPlanName
            // 
            this.txtTestPlanName.Enabled = false;
            this.txtTestPlanName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtTestPlanName.Location = new System.Drawing.Point(79, 20);
            this.txtTestPlanName.Name = "txtTestPlanName";
            this.txtTestPlanName.Size = new System.Drawing.Size(180, 21);
            this.txtTestPlanName.TabIndex = 12;
            // 
            // cboAuxAttribles
            // 
            this.cboAuxAttribles.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboAuxAttribles.FormattingEnabled = true;
            this.cboAuxAttribles.Location = new System.Drawing.Point(79, 188);
            this.cboAuxAttribles.Name = "cboAuxAttribles";
            this.cboAuxAttribles.Size = new System.Drawing.Size(180, 20);
            this.cboAuxAttribles.TabIndex = 11;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(6, 191);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 10;
            this.label7.Text = "其他属性";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(154, 54);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 9;
            this.label6.Text = "内容";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(6, 54);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "项目";
            // 
            // cboUSBPort
            // 
            this.cboUSBPort.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboUSBPort.FormattingEnabled = true;
            this.cboUSBPort.Location = new System.Drawing.Point(79, 156);
            this.cboUSBPort.Name = "cboUSBPort";
            this.cboUSBPort.Size = new System.Drawing.Size(180, 20);
            this.cboUSBPort.TabIndex = 7;
            this.cboUSBPort.Leave += new System.EventHandler(this.cboUSBPort_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(6, 159);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "USB端口号";
            // 
            // cboHWVersion
            // 
            this.cboHWVersion.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboHWVersion.FormattingEnabled = true;
            this.cboHWVersion.Location = new System.Drawing.Point(79, 130);
            this.cboHWVersion.Name = "cboHWVersion";
            this.cboHWVersion.Size = new System.Drawing.Size(180, 20);
            this.cboHWVersion.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(6, 133);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "硬件版本";
            // 
            // cboSWVersion
            // 
            this.cboSWVersion.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboSWVersion.FormattingEnabled = true;
            this.cboSWVersion.Location = new System.Drawing.Point(79, 104);
            this.cboSWVersion.Name = "cboSWVersion";
            this.cboSWVersion.Size = new System.Drawing.Size(180, 20);
            this.cboSWVersion.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(6, 107);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "软件版本";
            // 
            // cboItemName
            // 
            this.cboItemName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboItemName.FormattingEnabled = true;
            this.cboItemName.Location = new System.Drawing.Point(79, 78);
            this.cboItemName.Name = "cboItemName";
            this.cboItemName.Size = new System.Drawing.Size(180, 20);
            this.cboItemName.TabIndex = 1;
            this.cboItemName.Leave += new System.EventHandler(this.cboItemName_Leave);
            // 
            // lblItemName
            // 
            this.lblItemName.AutoSize = true;
            this.lblItemName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.lblItemName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblItemName.Location = new System.Drawing.Point(6, 81);
            this.lblItemName.Name = "lblItemName";
            this.lblItemName.Size = new System.Drawing.Size(53, 12);
            this.lblItemName.TabIndex = 0;
            this.lblItemName.Text = "名    称";
            this.lblItemName.DoubleClick += new System.EventHandler(this.lblItemName_DoubleClick);
            // 
            // btnNextPage
            // 
            this.btnNextPage.Location = new System.Drawing.Point(247, 309);
            this.btnNextPage.Name = "btnNextPage";
            this.btnNextPage.Size = new System.Drawing.Size(100, 20);
            this.btnNextPage.TabIndex = 7;
            this.btnNextPage.Text = "下一步";
            this.btnNextPage.UseVisualStyleBackColor = true;
            this.btnNextPage.Click += new System.EventHandler(this.btnNextPage_Click);
            // 
            // btnPreviousPage
            // 
            this.btnPreviousPage.Location = new System.Drawing.Point(90, 309);
            this.btnPreviousPage.Name = "btnPreviousPage";
            this.btnPreviousPage.Size = new System.Drawing.Size(100, 20);
            this.btnPreviousPage.TabIndex = 8;
            this.btnPreviousPage.Text = "完成并返回";
            this.btnPreviousPage.UseVisualStyleBackColor = true;
            this.btnPreviousPage.Click += new System.EventHandler(this.btnPreviousPage_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(6, 4);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(97, 14);
            this.label9.TabIndex = 10;
            this.label9.Text = "测试计划列表";
            // 
            // TestPlanForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(437, 341);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.btnPreviousPage);
            this.Controls.Add(this.btnNextPage);
            this.Controls.Add(this.grpItem);
            this.Controls.Add(this.currlst);
            this.MaximumSize = new System.Drawing.Size(453, 379);
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
        private System.Windows.Forms.ComboBox cboAuxAttribles;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
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
    }
}
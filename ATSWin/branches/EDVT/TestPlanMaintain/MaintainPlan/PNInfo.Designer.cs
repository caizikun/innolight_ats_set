namespace TestPlan
{
    partial class PNInfo
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PNInfo));
            this.grpMain = new System.Windows.Forms.GroupBox();
            this.btnShowMSADefine = new System.Windows.Forms.Button();
            this.btnShowMemory = new System.Windows.Forms.Button();
            this.cboPN = new System.Windows.Forms.ComboBox();
            this.lblPN = new System.Windows.Forms.Label();
            this.lblType = new System.Windows.Forms.Label();
            this.cboItemType = new System.Windows.Forms.ComboBox();
            this.cklTestPlan = new System.Windows.Forms.CheckedListBox();
            this.cmsTestPlan = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmEditTestPlan = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmAddTestPlan = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmDeleteTestPlan = new System.Windows.Forms.ToolStripMenuItem();
            this.ssrRunMsg = new System.Windows.Forms.StatusStrip();
            this.sslRunMsg = new System.Windows.Forms.ToolStripStatusLabel();
            this.spbInfo = new System.Windows.Forms.ToolStripProgressBar();
            this.sslInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsuserInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssTimelbl = new System.Windows.Forms.ToolStripStatusLabel();
            this.mspMain = new System.Windows.Forms.MenuStrip();
            this.tsmUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmCancel = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmExportPlan = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmLoadExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmCopyPlan = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmExit = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPNtype = new System.Windows.Forms.TabControl();
            this.tabPageTestCtrl = new System.Windows.Forms.TabPage();
            this.dgvTestCtrl = new System.Windows.Forms.DataGridView();
            this.dataGridViewButtonColumn1 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.tabPageTestEquip = new System.Windows.Forms.TabPage();
            this.dgvTestEquip = new System.Windows.Forms.DataGridView();
            this.dataGridViewButtonColumn2 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.timerDate = new System.Windows.Forms.Timer(this.components);
            this.grpMain.SuspendLayout();
            this.cmsTestPlan.SuspendLayout();
            this.ssrRunMsg.SuspendLayout();
            this.mspMain.SuspendLayout();
            this.tabPNtype.SuspendLayout();
            this.tabPageTestCtrl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTestCtrl)).BeginInit();
            this.tabPageTestEquip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTestEquip)).BeginInit();
            this.SuspendLayout();
            // 
            // grpMain
            // 
            this.grpMain.Controls.Add(this.btnShowMSADefine);
            this.grpMain.Controls.Add(this.btnShowMemory);
            this.grpMain.Controls.Add(this.cboPN);
            this.grpMain.Controls.Add(this.lblPN);
            this.grpMain.Controls.Add(this.lblType);
            this.grpMain.Controls.Add(this.cboItemType);
            this.grpMain.Controls.Add(this.cklTestPlan);
            this.grpMain.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grpMain.Location = new System.Drawing.Point(2, 28);
            this.grpMain.Name = "grpMain";
            this.grpMain.Size = new System.Drawing.Size(196, 443);
            this.grpMain.TabIndex = 12;
            this.grpMain.TabStop = false;
            this.grpMain.Text = "机种信息";
            // 
            // btnShowMSADefine
            // 
            this.btnShowMSADefine.Enabled = false;
            this.btnShowMSADefine.Font = new System.Drawing.Font("宋体", 7.5F);
            this.btnShowMSADefine.Location = new System.Drawing.Point(163, 17);
            this.btnShowMSADefine.Name = "btnShowMSADefine";
            this.btnShowMSADefine.Size = new System.Drawing.Size(30, 23);
            this.btnShowMSADefine.TabIndex = 18;
            this.btnShowMSADefine.Text = "...";
            this.btnShowMSADefine.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnShowMSADefine.UseVisualStyleBackColor = true;
            this.btnShowMSADefine.Click += new System.EventHandler(this.btnShowMSADefine_Click);
            // 
            // btnShowMemory
            // 
            this.btnShowMemory.Enabled = false;
            this.btnShowMemory.Font = new System.Drawing.Font("宋体", 7.5F);
            this.btnShowMemory.Location = new System.Drawing.Point(163, 44);
            this.btnShowMemory.Name = "btnShowMemory";
            this.btnShowMemory.Size = new System.Drawing.Size(30, 23);
            this.btnShowMemory.TabIndex = 17;
            this.btnShowMemory.Text = "...";
            this.btnShowMemory.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnShowMemory.UseVisualStyleBackColor = true;
            this.btnShowMemory.Click += new System.EventHandler(this.btnShowMemory_Click);
            // 
            // cboPN
            // 
            this.cboPN.Enabled = false;
            this.cboPN.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboPN.FormattingEnabled = true;
            this.cboPN.Location = new System.Drawing.Point(36, 45);
            this.cboPN.Name = "cboPN";
            this.cboPN.Size = new System.Drawing.Size(126, 20);
            this.cboPN.TabIndex = 14;
            this.cboPN.SelectedIndexChanged += new System.EventHandler(this.cboPN_SelectedIndexChanged);
            // 
            // lblPN
            // 
            this.lblPN.AutoSize = true;
            this.lblPN.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPN.Location = new System.Drawing.Point(4, 49);
            this.lblPN.Name = "lblPN";
            this.lblPN.Size = new System.Drawing.Size(29, 17);
            this.lblPN.TabIndex = 13;
            this.lblPN.Text = "P N";
            this.lblPN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblType.Location = new System.Drawing.Point(2, 22);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(36, 17);
            this.lblType.TabIndex = 12;
            this.lblType.Text = "Type";
            // 
            // cboItemType
            // 
            this.cboItemType.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboItemType.FormattingEnabled = true;
            this.cboItemType.Items.AddRange(new object[] {
            "QSFP",
            "XFP",
            "CFP",
            "SFP+"});
            this.cboItemType.Location = new System.Drawing.Point(36, 20);
            this.cboItemType.Name = "cboItemType";
            this.cboItemType.Size = new System.Drawing.Size(126, 20);
            this.cboItemType.TabIndex = 11;
            this.cboItemType.SelectedIndexChanged += new System.EventHandler(this.cboItemType_SelectedIndexChanged);
            // 
            // cklTestPlan
            // 
            this.cklTestPlan.CheckOnClick = true;
            this.cklTestPlan.ContextMenuStrip = this.cmsTestPlan;
            this.cklTestPlan.Enabled = false;
            this.cklTestPlan.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cklTestPlan.FormattingEnabled = true;
            this.cklTestPlan.Items.AddRange(new object[] {
            "TestPlan1",
            "TestPlan2",
            "TestPlan3"});
            this.cklTestPlan.Location = new System.Drawing.Point(6, 71);
            this.cklTestPlan.Name = "cklTestPlan";
            this.cklTestPlan.Size = new System.Drawing.Size(184, 356);
            this.cklTestPlan.TabIndex = 15;
            this.cklTestPlan.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.cklTestPlan_ItemCheck);
            this.cklTestPlan.SelectedIndexChanged += new System.EventHandler(this.cklTestPlan_SelectedIndexChanged);
            this.cklTestPlan.MouseDown += new System.Windows.Forms.MouseEventHandler(this.cklTestPlan_MouseDown);
            // 
            // cmsTestPlan
            // 
            this.cmsTestPlan.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmEditTestPlan,
            this.tsmAddTestPlan,
            this.tsmDeleteTestPlan});
            this.cmsTestPlan.Name = "cmsTestPlan";
            this.cmsTestPlan.Size = new System.Drawing.Size(125, 70);
            // 
            // tsmEditTestPlan
            // 
            this.tsmEditTestPlan.Name = "tsmEditTestPlan";
            this.tsmEditTestPlan.Size = new System.Drawing.Size(124, 22);
            this.tsmEditTestPlan.Text = "查看编辑";
            this.tsmEditTestPlan.Click += new System.EventHandler(this.tsmEditTestPlan_Click);
            // 
            // tsmAddTestPlan
            // 
            this.tsmAddTestPlan.Name = "tsmAddTestPlan";
            this.tsmAddTestPlan.Size = new System.Drawing.Size(124, 22);
            this.tsmAddTestPlan.Text = "新增";
            this.tsmAddTestPlan.Click += new System.EventHandler(this.tsmAddTestPlan_Click);
            // 
            // tsmDeleteTestPlan
            // 
            this.tsmDeleteTestPlan.Name = "tsmDeleteTestPlan";
            this.tsmDeleteTestPlan.Size = new System.Drawing.Size(124, 22);
            this.tsmDeleteTestPlan.Text = "删除";
            this.tsmDeleteTestPlan.Click += new System.EventHandler(this.tsmDeleteTestPlan_Click);
            // 
            // ssrRunMsg
            // 
            this.ssrRunMsg.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sslRunMsg,
            this.spbInfo,
            this.sslInfo,
            this.tsuserInfo,
            this.tssTimelbl});
            this.ssrRunMsg.Location = new System.Drawing.Point(0, 486);
            this.ssrRunMsg.Name = "ssrRunMsg";
            this.ssrRunMsg.Size = new System.Drawing.Size(966, 22);
            this.ssrRunMsg.TabIndex = 21;
            this.ssrRunMsg.Text = "statusStrip1";
            // 
            // sslRunMsg
            // 
            this.sslRunMsg.AutoSize = false;
            this.sslRunMsg.Enabled = false;
            this.sslRunMsg.Name = "sslRunMsg";
            this.sslRunMsg.Size = new System.Drawing.Size(560, 17);
            this.sslRunMsg.Text = "sslRunMsg";
            this.sslRunMsg.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // spbInfo
            // 
            this.spbInfo.Name = "spbInfo";
            this.spbInfo.Size = new System.Drawing.Size(100, 16);
            // 
            // sslInfo
            // 
            this.sslInfo.AutoSize = false;
            this.sslInfo.Name = "sslInfo";
            this.sslInfo.Size = new System.Drawing.Size(60, 17);
            this.sslInfo.Text = "TestPlan";
            // 
            // tsuserInfo
            // 
            this.tsuserInfo.AutoSize = false;
            this.tsuserInfo.Name = "tsuserInfo";
            this.tsuserInfo.Size = new System.Drawing.Size(100, 17);
            this.tsuserInfo.Text = "User:Terry.Yin";
            this.tsuserInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tssTimelbl
            // 
            this.tssTimelbl.Name = "tssTimelbl";
            this.tssTimelbl.Size = new System.Drawing.Size(126, 17);
            this.tssTimelbl.Text = "2014/05/28 14:39:00";
            // 
            // mspMain
            // 
            this.mspMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmUpdate,
            this.tsmCancel,
            this.tsmRefresh,
            this.tsmExportPlan,
            this.tsmLoadExcel,
            this.tsmCopyPlan,
            this.tsmExit});
            this.mspMain.Location = new System.Drawing.Point(0, 0);
            this.mspMain.Name = "mspMain";
            this.mspMain.Size = new System.Drawing.Size(966, 25);
            this.mspMain.TabIndex = 26;
            this.mspMain.Text = "mspMain";
            // 
            // tsmUpdate
            // 
            this.tsmUpdate.Image = global::TestPlan.Properties.Resources.save;
            this.tsmUpdate.Name = "tsmUpdate";
            this.tsmUpdate.Size = new System.Drawing.Size(108, 21);
            this.tsmUpdate.Text = "更新到数据库";
            this.tsmUpdate.Click += new System.EventHandler(this.tsmUpdate_Click);
            // 
            // tsmCancel
            // 
            this.tsmCancel.Image = global::TestPlan.Properties.Resources.Cancel;
            this.tsmCancel.Name = "tsmCancel";
            this.tsmCancel.Size = new System.Drawing.Size(108, 21);
            this.tsmCancel.Text = "取消所有修改";
            this.tsmCancel.Click += new System.EventHandler(this.tsmCancel_Click);
            // 
            // tsmRefresh
            // 
            this.tsmRefresh.Image = global::TestPlan.Properties.Resources.Refresh;
            this.tsmRefresh.Name = "tsmRefresh";
            this.tsmRefresh.Size = new System.Drawing.Size(84, 21);
            this.tsmRefresh.Text = "刷新查询";
            this.tsmRefresh.Click += new System.EventHandler(this.tsmRefresh_Click);
            // 
            // tsmExportPlan
            // 
            this.tsmExportPlan.Name = "tsmExportPlan";
            this.tsmExportPlan.Size = new System.Drawing.Size(133, 21);
            this.tsmExportPlan.Text = "导出测试计划到Excel";
            this.tsmExportPlan.Click += new System.EventHandler(this.tsmExportPlan_Click);
            // 
            // tsmLoadExcel
            // 
            this.tsmLoadExcel.Name = "tsmLoadExcel";
            this.tsmLoadExcel.Size = new System.Drawing.Size(121, 21);
            this.tsmLoadExcel.Text = "Excel导入测试计划";
            this.tsmLoadExcel.Click += new System.EventHandler(this.tsmLoadExcel_Click);
            // 
            // tsmCopyPlan
            // 
            this.tsmCopyPlan.Name = "tsmCopyPlan";
            this.tsmCopyPlan.Size = new System.Drawing.Size(92, 21);
            this.tsmCopyPlan.Text = "复制测试计划";
            this.tsmCopyPlan.Click += new System.EventHandler(this.tsmCopyPlan_Click);
            // 
            // tsmExit
            // 
            this.tsmExit.Image = global::TestPlan.Properties.Resources.Exit;
            this.tsmExit.Name = "tsmExit";
            this.tsmExit.Size = new System.Drawing.Size(68, 21);
            this.tsmExit.Text = "退  出";
            this.tsmExit.Click += new System.EventHandler(this.tsmExit_Click);
            // 
            // tabPNtype
            // 
            this.tabPNtype.Controls.Add(this.tabPageTestCtrl);
            this.tabPNtype.Controls.Add(this.tabPageTestEquip);
            this.tabPNtype.Enabled = false;
            this.tabPNtype.Location = new System.Drawing.Point(204, 28);
            this.tabPNtype.Name = "tabPNtype";
            this.tabPNtype.SelectedIndex = 0;
            this.tabPNtype.Size = new System.Drawing.Size(763, 443);
            this.tabPNtype.TabIndex = 31;
            // 
            // tabPageTestCtrl
            // 
            this.tabPageTestCtrl.Controls.Add(this.dgvTestCtrl);
            this.tabPageTestCtrl.Location = new System.Drawing.Point(4, 22);
            this.tabPageTestCtrl.Name = "tabPageTestCtrl";
            this.tabPageTestCtrl.Size = new System.Drawing.Size(755, 417);
            this.tabPageTestCtrl.TabIndex = 2;
            this.tabPageTestCtrl.Text = "测试流程信息";
            this.tabPageTestCtrl.UseVisualStyleBackColor = true;
            // 
            // dgvTestCtrl
            // 
            this.dgvTestCtrl.AllowUserToAddRows = false;
            this.dgvTestCtrl.AllowUserToDeleteRows = false;
            this.dgvTestCtrl.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTestCtrl.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewButtonColumn1});
            this.dgvTestCtrl.Location = new System.Drawing.Point(3, 4);
            this.dgvTestCtrl.Name = "dgvTestCtrl";
            this.dgvTestCtrl.ReadOnly = true;
            this.dgvTestCtrl.RowHeadersVisible = false;
            this.dgvTestCtrl.RowTemplate.Height = 23;
            this.dgvTestCtrl.Size = new System.Drawing.Size(749, 411);
            this.dgvTestCtrl.TabIndex = 3;
            this.dgvTestCtrl.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvTestCtrl_CellMouseClick);
            // 
            // dataGridViewButtonColumn1
            // 
            this.dataGridViewButtonColumn1.HeaderText = "操作";
            this.dataGridViewButtonColumn1.Name = "dataGridViewButtonColumn1";
            this.dataGridViewButtonColumn1.ReadOnly = true;
            this.dataGridViewButtonColumn1.Width = 40;
            // 
            // tabPageTestEquip
            // 
            this.tabPageTestEquip.Controls.Add(this.dgvTestEquip);
            this.tabPageTestEquip.Location = new System.Drawing.Point(4, 22);
            this.tabPageTestEquip.Name = "tabPageTestEquip";
            this.tabPageTestEquip.Size = new System.Drawing.Size(755, 417);
            this.tabPageTestEquip.TabIndex = 3;
            this.tabPageTestEquip.Text = "测试设备信息";
            this.tabPageTestEquip.UseVisualStyleBackColor = true;
            // 
            // dgvTestEquip
            // 
            this.dgvTestEquip.AllowUserToAddRows = false;
            this.dgvTestEquip.AllowUserToDeleteRows = false;
            this.dgvTestEquip.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTestEquip.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewButtonColumn2});
            this.dgvTestEquip.Location = new System.Drawing.Point(3, 0);
            this.dgvTestEquip.Name = "dgvTestEquip";
            this.dgvTestEquip.ReadOnly = true;
            this.dgvTestEquip.RowHeadersVisible = false;
            this.dgvTestEquip.RowTemplate.Height = 23;
            this.dgvTestEquip.Size = new System.Drawing.Size(749, 400);
            this.dgvTestEquip.TabIndex = 5;
            this.dgvTestEquip.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvTestEquip_CellMouseClick);
            // 
            // dataGridViewButtonColumn2
            // 
            this.dataGridViewButtonColumn2.FillWeight = 80F;
            this.dataGridViewButtonColumn2.HeaderText = "操作";
            this.dataGridViewButtonColumn2.Name = "dataGridViewButtonColumn2";
            this.dataGridViewButtonColumn2.ReadOnly = true;
            this.dataGridViewButtonColumn2.Width = 40;
            // 
            // timerDate
            // 
            this.timerDate.Interval = 1000;
            this.timerDate.Tick += new System.EventHandler(this.timerDate_Tick);
            // 
            // PNInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(966, 508);
            this.Controls.Add(this.tabPNtype);
            this.Controls.Add(this.mspMain);
            this.Controls.Add(this.ssrRunMsg);
            this.Controls.Add(this.grpMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(982, 546);
            this.Name = "PNInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PNInfo";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PNInfo_FormClosing);
            this.Load += new System.EventHandler(this.PNInfo_Load);
            this.grpMain.ResumeLayout(false);
            this.grpMain.PerformLayout();
            this.cmsTestPlan.ResumeLayout(false);
            this.ssrRunMsg.ResumeLayout(false);
            this.ssrRunMsg.PerformLayout();
            this.mspMain.ResumeLayout(false);
            this.mspMain.PerformLayout();
            this.tabPNtype.ResumeLayout(false);
            this.tabPageTestCtrl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTestCtrl)).EndInit();
            this.tabPageTestEquip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTestEquip)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion


        private System.Windows.Forms.GroupBox grpMain;
        private System.Windows.Forms.ComboBox cboPN;
        private System.Windows.Forms.Label lblPN;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.ComboBox cboItemType;
        private System.Windows.Forms.StatusStrip ssrRunMsg;
        private System.Windows.Forms.ToolStripStatusLabel sslRunMsg;
        private System.Windows.Forms.ToolStripProgressBar spbInfo;
        private System.Windows.Forms.ToolStripStatusLabel sslInfo;
        private System.Windows.Forms.ToolStripStatusLabel tsuserInfo;
        private System.Windows.Forms.Button btnShowMemory;
        private System.Windows.Forms.ContextMenuStrip cmsTestPlan;
        private System.Windows.Forms.ToolStripMenuItem tsmEditTestPlan;
        private System.Windows.Forms.ToolStripMenuItem tsmAddTestPlan;
        private System.Windows.Forms.ToolStripMenuItem tsmDeleteTestPlan;
        private System.Windows.Forms.Button btnShowMSADefine;
        private System.Windows.Forms.MenuStrip mspMain;
        private System.Windows.Forms.ToolStripMenuItem tsmUpdate;
        private System.Windows.Forms.ToolStripMenuItem tsmCancel;
        private System.Windows.Forms.ToolStripMenuItem tsmRefresh;
        private System.Windows.Forms.ToolStripMenuItem tsmExit;
        private System.Windows.Forms.TabControl tabPNtype;
        private System.Windows.Forms.TabPage tabPageTestCtrl;
        private System.Windows.Forms.TabPage tabPageTestEquip;
        private System.Windows.Forms.DataGridView dgvTestCtrl;
        private System.Windows.Forms.DataGridView dgvTestEquip;
        public System.Windows.Forms.CheckedListBox cklTestPlan;
        private System.Windows.Forms.DataGridViewButtonColumn dataGridViewButtonColumn1;
        private System.Windows.Forms.DataGridViewButtonColumn dataGridViewButtonColumn2;
        private System.Windows.Forms.ToolStripStatusLabel tssTimelbl;
        private System.Windows.Forms.Timer timerDate;
        private System.Windows.Forms.ToolStripMenuItem tsmExportPlan;
        private System.Windows.Forms.ToolStripMenuItem tsmCopyPlan;
        private System.Windows.Forms.ToolStripMenuItem tsmLoadExcel;
    }
}
namespace Maintain
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.grpFilterString = new System.Windows.Forms.GroupBox();
            this.cklRowState = new System.Windows.Forms.CheckedListBox();
            this.lblLoginName = new System.Windows.Forms.Label();
            this.lblAppType = new System.Windows.Forms.Label();
            this.btnReset = new System.Windows.Forms.Button();
            this.lblEndTime = new System.Windows.Forms.Label();
            this.dateTimePickerEndTime = new System.Windows.Forms.DateTimePicker();
            this.lblStartTime = new System.Windows.Forms.Label();
            this.dateTimePickerStartTime = new System.Windows.Forms.DateTimePicker();
            this.cboLoginName = new System.Windows.Forms.ComboBox();
            this.cboAppType = new System.Windows.Forms.ComboBox();
            this.btnQuery = new System.Windows.Forms.Button();
            this.ssrRunMsg = new System.Windows.Forms.StatusStrip();
            this.sslRunMsg = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssTimelbl = new System.Windows.Forms.ToolStripStatusLabel();
            this.sslInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.timerDate = new System.Windows.Forms.Timer(this.components);
            this.cmsLog = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmCopyAll = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmCopySelect = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmSaveAll = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmSaveSelect = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.grpLogs = new System.Windows.Forms.GroupBox();
            this.rtxt = new System.Windows.Forms.RichTextBox();
            this.grpOPLst = new System.Windows.Forms.GroupBox();
            this.lstOPType = new System.Windows.Forms.ListBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.rtxtLoginlog = new System.Windows.Forms.RichTextBox();
            this.grpFilterString.SuspendLayout();
            this.ssrRunMsg.SuspendLayout();
            this.cmsLog.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.grpLogs.SuspendLayout();
            this.grpOPLst.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpFilterString
            // 
            this.grpFilterString.Controls.Add(this.cklRowState);
            this.grpFilterString.Controls.Add(this.lblLoginName);
            this.grpFilterString.Controls.Add(this.lblAppType);
            this.grpFilterString.Controls.Add(this.btnReset);
            this.grpFilterString.Controls.Add(this.lblEndTime);
            this.grpFilterString.Controls.Add(this.dateTimePickerEndTime);
            this.grpFilterString.Controls.Add(this.lblStartTime);
            this.grpFilterString.Controls.Add(this.dateTimePickerStartTime);
            this.grpFilterString.Controls.Add(this.cboLoginName);
            this.grpFilterString.Controls.Add(this.cboAppType);
            this.grpFilterString.Controls.Add(this.btnQuery);
            this.grpFilterString.Font = new System.Drawing.Font("新宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grpFilterString.Location = new System.Drawing.Point(10, 2);
            this.grpFilterString.Name = "grpFilterString";
            this.grpFilterString.Size = new System.Drawing.Size(193, 502);
            this.grpFilterString.TabIndex = 1;
            this.grpFilterString.TabStop = false;
            this.grpFilterString.Text = "FileterSetting";
            // 
            // cklRowState
            // 
            this.cklRowState.Font = new System.Drawing.Font("新宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cklRowState.FormattingEnabled = true;
            this.cklRowState.Location = new System.Drawing.Point(15, 261);
            this.cklRowState.Name = "cklRowState";
            this.cklRowState.Size = new System.Drawing.Size(168, 88);
            this.cklRowState.TabIndex = 14;
            this.cklRowState.MouseDown += new System.Windows.Forms.MouseEventHandler(this.cklRowState_MouseDown);
            // 
            // lblLoginName
            // 
            this.lblLoginName.AutoSize = true;
            this.lblLoginName.Location = new System.Drawing.Point(12, 57);
            this.lblLoginName.Name = "lblLoginName";
            this.lblLoginName.Size = new System.Drawing.Size(70, 14);
            this.lblLoginName.TabIndex = 13;
            this.lblLoginName.Text = "LoginName";
            // 
            // lblAppType
            // 
            this.lblAppType.AutoSize = true;
            this.lblAppType.Location = new System.Drawing.Point(12, 17);
            this.lblAppType.Name = "lblAppType";
            this.lblAppType.Size = new System.Drawing.Size(84, 14);
            this.lblAppType.TabIndex = 13;
            this.lblAppType.Text = "AppTypeName";
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(15, 365);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(65, 29);
            this.btnReset.TabIndex = 12;
            this.btnReset.Text = "Refresh";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // lblEndTime
            // 
            this.lblEndTime.BackColor = System.Drawing.Color.Aquamarine;
            this.lblEndTime.Location = new System.Drawing.Point(16, 179);
            this.lblEndTime.Name = "lblEndTime";
            this.lblEndTime.Size = new System.Drawing.Size(116, 21);
            this.lblEndTime.TabIndex = 11;
            this.lblEndTime.Text = "LoginEnd";
            this.lblEndTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dateTimePickerEndTime
            // 
            this.dateTimePickerEndTime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dateTimePickerEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerEndTime.Location = new System.Drawing.Point(15, 203);
            this.dateTimePickerEndTime.Name = "dateTimePickerEndTime";
            this.dateTimePickerEndTime.ShowUpDown = true;
            this.dateTimePickerEndTime.Size = new System.Drawing.Size(172, 23);
            this.dateTimePickerEndTime.TabIndex = 10;
            this.dateTimePickerEndTime.Value = new System.DateTime(2000, 1, 1, 12, 0, 0, 0);
            this.dateTimePickerEndTime.ValueChanged += new System.EventHandler(this.dateTimePickerEndTime_ValueChanged);
            this.dateTimePickerEndTime.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dateTimePickerEndTime_MouseDown);
            // 
            // lblStartTime
            // 
            this.lblStartTime.BackColor = System.Drawing.Color.Aquamarine;
            this.lblStartTime.Location = new System.Drawing.Point(15, 125);
            this.lblStartTime.Name = "lblStartTime";
            this.lblStartTime.Size = new System.Drawing.Size(117, 21);
            this.lblStartTime.TabIndex = 9;
            this.lblStartTime.Text = "LoginStart";
            this.lblStartTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dateTimePickerStartTime
            // 
            this.dateTimePickerStartTime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dateTimePickerStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerStartTime.Location = new System.Drawing.Point(15, 149);
            this.dateTimePickerStartTime.Name = "dateTimePickerStartTime";
            this.dateTimePickerStartTime.ShowUpDown = true;
            this.dateTimePickerStartTime.Size = new System.Drawing.Size(172, 23);
            this.dateTimePickerStartTime.TabIndex = 8;
            this.dateTimePickerStartTime.Value = new System.DateTime(2000, 1, 1, 12, 0, 0, 0);
            this.dateTimePickerStartTime.ValueChanged += new System.EventHandler(this.dateTimePickerStartTime_ValueChanged);
            this.dateTimePickerStartTime.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dateTimePickerStartTime_MouseDown);
            // 
            // cboLoginName
            // 
            this.cboLoginName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoginName.Font = new System.Drawing.Font("新宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboLoginName.FormattingEnabled = true;
            this.cboLoginName.Location = new System.Drawing.Point(15, 72);
            this.cboLoginName.Name = "cboLoginName";
            this.cboLoginName.Size = new System.Drawing.Size(172, 24);
            this.cboLoginName.TabIndex = 3;
            // 
            // cboAppType
            // 
            this.cboAppType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAppType.Font = new System.Drawing.Font("新宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboAppType.FormattingEnabled = true;
            this.cboAppType.Location = new System.Drawing.Point(15, 32);
            this.cboAppType.Name = "cboAppType";
            this.cboAppType.Size = new System.Drawing.Size(172, 24);
            this.cboAppType.TabIndex = 2;
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(118, 365);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(65, 29);
            this.btnQuery.TabIndex = 1;
            this.btnQuery.Text = "Query";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // ssrRunMsg
            // 
            this.ssrRunMsg.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sslRunMsg,
            this.tssTimelbl,
            this.sslInfo});
            this.ssrRunMsg.Location = new System.Drawing.Point(0, 507);
            this.ssrRunMsg.Name = "ssrRunMsg";
            this.ssrRunMsg.Size = new System.Drawing.Size(888, 22);
            this.ssrRunMsg.TabIndex = 22;
            this.ssrRunMsg.Text = "statusStrip1";
            // 
            // sslRunMsg
            // 
            this.sslRunMsg.AutoSize = false;
            this.sslRunMsg.Enabled = false;
            this.sslRunMsg.Name = "sslRunMsg";
            this.sslRunMsg.Size = new System.Drawing.Size(627, 17);
            this.sslRunMsg.Spring = true;
            this.sslRunMsg.Text = "sslRunMsg";
            this.sslRunMsg.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tssTimelbl
            // 
            this.tssTimelbl.Name = "tssTimelbl";
            this.tssTimelbl.Size = new System.Drawing.Size(126, 17);
            this.tssTimelbl.Text = "2014/05/28 14:39:00";
            // 
            // sslInfo
            // 
            this.sslInfo.AutoSize = false;
            this.sslInfo.Name = "sslInfo";
            this.sslInfo.Size = new System.Drawing.Size(120, 17);
            this.sslInfo.Text = "QueryModifiedLogs";
            // 
            // timerDate
            // 
            this.timerDate.Interval = 1000;
            this.timerDate.Tick += new System.EventHandler(this.timerDate_Tick);
            // 
            // cmsLog
            // 
            this.cmsLog.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmSelectAll,
            this.tsmCopyAll,
            this.tsmCopySelect,
            this.tsmSaveAll,
            this.tsmSaveSelect});
            this.cmsLog.Name = "cmsLog";
            this.cmsLog.Size = new System.Drawing.Size(166, 114);
            // 
            // tsmSelectAll
            // 
            this.tsmSelectAll.Name = "tsmSelectAll";
            this.tsmSelectAll.Size = new System.Drawing.Size(165, 22);
            this.tsmSelectAll.Text = "SelectAll";
            this.tsmSelectAll.Click += new System.EventHandler(this.tsmSelectAll_Click);
            // 
            // tsmCopyAll
            // 
            this.tsmCopyAll.Name = "tsmCopyAll";
            this.tsmCopyAll.Size = new System.Drawing.Size(165, 22);
            this.tsmCopyAll.Text = "CopyAll";
            this.tsmCopyAll.Click += new System.EventHandler(this.tsmCopyAll_Click);
            // 
            // tsmCopySelect
            // 
            this.tsmCopySelect.Name = "tsmCopySelect";
            this.tsmCopySelect.Size = new System.Drawing.Size(165, 22);
            this.tsmCopySelect.Text = "CopyCurrSelect";
            this.tsmCopySelect.Click += new System.EventHandler(this.tsmCopySelect_Click);
            // 
            // tsmSaveAll
            // 
            this.tsmSaveAll.Name = "tsmSaveAll";
            this.tsmSaveAll.Size = new System.Drawing.Size(165, 22);
            this.tsmSaveAll.Text = "SaveAll";
            this.tsmSaveAll.Click += new System.EventHandler(this.tsmSaveAll_Click);
            // 
            // tsmSaveSelect
            // 
            this.tsmSaveSelect.Name = "tsmSaveSelect";
            this.tsmSaveSelect.Size = new System.Drawing.Size(165, 22);
            this.tsmSaveSelect.Text = "SaveCurrSelect";
            this.tsmSaveSelect.Click += new System.EventHandler(this.tsmSaveSelect_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(208, 8);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(682, 496);
            this.tabControl1.TabIndex = 23;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.grpLogs);
            this.tabPage1.Controls.Add(this.grpOPLst);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(674, 470);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "OperationLog";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // grpLogs
            // 
            this.grpLogs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grpLogs.Controls.Add(this.rtxt);
            this.grpLogs.Font = new System.Drawing.Font("新宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grpLogs.Location = new System.Drawing.Point(1, 165);
            this.grpLogs.Name = "grpLogs";
            this.grpLogs.Size = new System.Drawing.Size(670, 302);
            this.grpLogs.TabIndex = 27;
            this.grpLogs.TabStop = false;
            this.grpLogs.Text = "DetailLogs";
            // 
            // rtxt
            // 
            this.rtxt.BackColor = System.Drawing.Color.Snow;
            this.rtxt.ContextMenuStrip = this.cmsLog;
            this.rtxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxt.Font = new System.Drawing.Font("新宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rtxt.Location = new System.Drawing.Point(3, 19);
            this.rtxt.Name = "rtxt";
            this.rtxt.Size = new System.Drawing.Size(664, 280);
            this.rtxt.TabIndex = 2;
            this.rtxt.Text = "DetailLogs";
            // 
            // grpOPLst
            // 
            this.grpOPLst.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grpOPLst.Controls.Add(this.lstOPType);
            this.grpOPLst.Font = new System.Drawing.Font("新宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grpOPLst.Location = new System.Drawing.Point(4, 0);
            this.grpOPLst.Name = "grpOPLst";
            this.grpOPLst.Size = new System.Drawing.Size(667, 159);
            this.grpOPLst.TabIndex = 26;
            this.grpOPLst.TabStop = false;
            this.grpOPLst.Text = "OptypeList";
            // 
            // lstOPType
            // 
            this.lstOPType.BackColor = System.Drawing.SystemColors.Info;
            this.lstOPType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstOPType.Font = new System.Drawing.Font("新宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lstOPType.FormattingEnabled = true;
            this.lstOPType.ItemHeight = 14;
            this.lstOPType.Location = new System.Drawing.Point(3, 19);
            this.lstOPType.Name = "lstOPType";
            this.lstOPType.Size = new System.Drawing.Size(661, 137);
            this.lstOPType.TabIndex = 1;
            this.lstOPType.SelectedIndexChanged += new System.EventHandler(this.lstOPType_SelectedIndexChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.rtxtLoginlog);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(674, 470);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "LoginLog";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // rtxtLoginlog
            // 
            this.rtxtLoginlog.BackColor = System.Drawing.Color.Snow;
            this.rtxtLoginlog.ContextMenuStrip = this.cmsLog;
            this.rtxtLoginlog.Font = new System.Drawing.Font("新宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rtxtLoginlog.Location = new System.Drawing.Point(3, 4);
            this.rtxtLoginlog.Name = "rtxtLoginlog";
            this.rtxtLoginlog.Size = new System.Drawing.Size(673, 459);
            this.rtxtLoginlog.TabIndex = 4;
            this.rtxtLoginlog.Text = "Loginlog";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(888, 529);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.ssrRunMsg);
            this.Controls.Add(this.grpFilterString);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "QueryLogs";
            this.Load += new System.EventHandler(this.QueryLogs_Load);
            this.grpFilterString.ResumeLayout(false);
            this.grpFilterString.PerformLayout();
            this.ssrRunMsg.ResumeLayout(false);
            this.ssrRunMsg.PerformLayout();
            this.cmsLog.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.grpLogs.ResumeLayout(false);
            this.grpOPLst.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpFilterString;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.StatusStrip ssrRunMsg;
        private System.Windows.Forms.ToolStripStatusLabel sslRunMsg;
        private System.Windows.Forms.ToolStripStatusLabel tssTimelbl;
        private System.Windows.Forms.ToolStripStatusLabel sslInfo;
        private System.Windows.Forms.Timer timerDate;
        private System.Windows.Forms.Label lblEndTime;
        private System.Windows.Forms.DateTimePicker dateTimePickerEndTime;
        private System.Windows.Forms.Label lblStartTime;
        private System.Windows.Forms.DateTimePicker dateTimePickerStartTime;
        private System.Windows.Forms.ComboBox cboLoginName;
        private System.Windows.Forms.ComboBox cboAppType;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Label lblLoginName;
        private System.Windows.Forms.Label lblAppType;
        private System.Windows.Forms.CheckedListBox cklRowState;
        private System.Windows.Forms.ContextMenuStrip cmsLog;
        private System.Windows.Forms.ToolStripMenuItem tsmSelectAll;
        private System.Windows.Forms.ToolStripMenuItem tsmCopyAll;
        private System.Windows.Forms.ToolStripMenuItem tsmCopySelect;
        private System.Windows.Forms.ToolStripMenuItem tsmSaveAll;
        private System.Windows.Forms.ToolStripMenuItem tsmSaveSelect;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox grpLogs;
        private System.Windows.Forms.RichTextBox rtxt;
        private System.Windows.Forms.GroupBox grpOPLst;
        private System.Windows.Forms.ListBox lstOPType;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.RichTextBox rtxtLoginlog;

    }
}


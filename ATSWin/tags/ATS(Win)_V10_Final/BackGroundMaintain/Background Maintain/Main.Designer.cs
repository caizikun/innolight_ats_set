namespace Maintain
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.ssrRunMsg = new System.Windows.Forms.StatusStrip();
            this.sslRunMsg = new System.Windows.Forms.ToolStripStatusLabel();
            this.sslInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsuserInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssTimelbl = new System.Windows.Forms.ToolStripStatusLabel();
            this.mspMain = new System.Windows.Forms.MenuStrip();
            this.tsmUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmCancel = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmUserInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmRoleInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmFuncInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmExit = new System.Windows.Forms.ToolStripMenuItem();
            this.timerDate = new System.Windows.Forms.Timer(this.components);
            this.grpType = new System.Windows.Forms.GroupBox();
            this.btnShowMSA = new System.Windows.Forms.Button();
            this.cboType = new System.Windows.Forms.ComboBox();
            this.grpPN = new System.Windows.Forms.GroupBox();
            this.btnShowMemory = new System.Windows.Forms.Button();
            this.btnPNDelete = new System.Windows.Forms.Button();
            this.btnPNEdit = new System.Windows.Forms.Button();
            this.btnPNAdd = new System.Windows.Forms.Button();
            this.listPN = new System.Windows.Forms.ListBox();
            this.btnTypeDelete = new System.Windows.Forms.Button();
            this.btnTypeEdit = new System.Windows.Forms.Button();
            this.btnTypeAdd = new System.Windows.Forms.Button();
            this.grpMSA = new System.Windows.Forms.GroupBox();
            this.btnMSADelete = new System.Windows.Forms.Button();
            this.btnMSAEdit = new System.Windows.Forms.Button();
            this.btnMSAAdd = new System.Windows.Forms.Button();
            this.ListMSA = new System.Windows.Forms.ListBox();
            this.grpAppModel = new System.Windows.Forms.GroupBox();
            this.btnAppDelete = new System.Windows.Forms.Button();
            this.btnAppEdit = new System.Windows.Forms.Button();
            this.btnAppAdd = new System.Windows.Forms.Button();
            this.listApp = new System.Windows.Forms.ListBox();
            this.grpEquip = new System.Windows.Forms.GroupBox();
            this.btnEquipDelete = new System.Windows.Forms.Button();
            this.btnEquipEdit = new System.Windows.Forms.Button();
            this.btnEquipAdd = new System.Windows.Forms.Button();
            this.listEquip = new System.Windows.Forms.ListBox();
            this.grpMCoefGroup = new System.Windows.Forms.GroupBox();
            this.btnMCoefGroupDelete = new System.Windows.Forms.Button();
            this.btnMCoefGroupEdit = new System.Windows.Forms.Button();
            this.btnMCoefGroupAdd = new System.Windows.Forms.Button();
            this.listMCoefGroup = new System.Windows.Forms.ListBox();
            this.ssrRunMsg.SuspendLayout();
            this.mspMain.SuspendLayout();
            this.grpType.SuspendLayout();
            this.grpPN.SuspendLayout();
            this.grpMSA.SuspendLayout();
            this.grpAppModel.SuspendLayout();
            this.grpEquip.SuspendLayout();
            this.grpMCoefGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // ssrRunMsg
            // 
            this.ssrRunMsg.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sslRunMsg,
            this.sslInfo,
            this.tsuserInfo,
            this.tssTimelbl});
            this.ssrRunMsg.Location = new System.Drawing.Point(0, 372);
            this.ssrRunMsg.Name = "ssrRunMsg";
            this.ssrRunMsg.Size = new System.Drawing.Size(816, 22);
            this.ssrRunMsg.TabIndex = 21;
            this.ssrRunMsg.Text = "statusStrip1";
            // 
            // sslRunMsg
            // 
            this.sslRunMsg.Name = "sslRunMsg";
            this.sslRunMsg.Size = new System.Drawing.Size(526, 17);
            this.sslRunMsg.Spring = true;
            this.sslRunMsg.Text = "sslRunMsg";
            this.sslRunMsg.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.sslRunMsg.TextChanged += new System.EventHandler(this.sslRunMsg_TextChanged);
            // 
            // sslInfo
            // 
            this.sslInfo.Name = "sslInfo";
            this.sslInfo.Size = new System.Drawing.Size(56, 17);
            this.sslInfo.Text = "TestPlan";
            // 
            // tsuserInfo
            // 
            this.tsuserInfo.Name = "tsuserInfo";
            this.tsuserInfo.Size = new System.Drawing.Size(62, 17);
            this.tsuserInfo.Text = "User:XXX";
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
            this.tsmUserInfo,
            this.tsmRoleInfo,
            this.tsmFuncInfo,
            this.tsmExit});
            this.mspMain.Location = new System.Drawing.Point(0, 0);
            this.mspMain.Name = "mspMain";
            this.mspMain.Size = new System.Drawing.Size(816, 25);
            this.mspMain.TabIndex = 26;
            this.mspMain.Text = "mspMain";
            // 
            // tsmUpdate
            // 
            this.tsmUpdate.Image = global::Maintain.Properties.Resources.save;
            this.tsmUpdate.Name = "tsmUpdate";
            this.tsmUpdate.Size = new System.Drawing.Size(79, 21);
            this.tsmUpdate.Text = "Update";
            this.tsmUpdate.Click += new System.EventHandler(this.tsmUpdate_Click);
            // 
            // tsmCancel
            // 
            this.tsmCancel.Image = global::Maintain.Properties.Resources.Cancel;
            this.tsmCancel.Name = "tsmCancel";
            this.tsmCancel.Size = new System.Drawing.Size(74, 21);
            this.tsmCancel.Text = "Cancel";
            this.tsmCancel.Click += new System.EventHandler(this.tsmCancel_Click);
            // 
            // tsmRefresh
            // 
            this.tsmRefresh.Image = global::Maintain.Properties.Resources.Refresh;
            this.tsmRefresh.Name = "tsmRefresh";
            this.tsmRefresh.Size = new System.Drawing.Size(80, 21);
            this.tsmRefresh.Text = "Refresh";
            this.tsmRefresh.Click += new System.EventHandler(this.tsmRefresh_Click);
            // 
            // tsmUserInfo
            // 
            this.tsmUserInfo.Name = "tsmUserInfo";
            this.tsmUserInfo.Size = new System.Drawing.Size(114, 21);
            this.tsmUserInfo.Text = "ManageAccount";
            this.tsmUserInfo.Click += new System.EventHandler(this.tsmUserInfo_Click);
            // 
            // tsmRoleInfo
            // 
            this.tsmRoleInfo.Name = "tsmRoleInfo";
            this.tsmRoleInfo.Size = new System.Drawing.Size(94, 21);
            this.tsmRoleInfo.Text = "ManageRole";
            this.tsmRoleInfo.Click += new System.EventHandler(this.tsmRoleInfo_Click);
            // 
            // tsmFuncInfo
            // 
            this.tsmFuncInfo.Name = "tsmFuncInfo";
            this.tsmFuncInfo.Size = new System.Drawing.Size(116, 21);
            this.tsmFuncInfo.Text = "ManageFunction";
            this.tsmFuncInfo.Click += new System.EventHandler(this.tsmFuncInfo_Click);
            // 
            // tsmExit
            // 
            this.tsmExit.Image = global::Maintain.Properties.Resources.Exit;
            this.tsmExit.Name = "tsmExit";
            this.tsmExit.Size = new System.Drawing.Size(56, 21);
            this.tsmExit.Text = "Exit";
            this.tsmExit.Click += new System.EventHandler(this.tsmExit_Click);
            // 
            // timerDate
            // 
            this.timerDate.Interval = 1000;
            this.timerDate.Tick += new System.EventHandler(this.timerDate_Tick);
            // 
            // grpType
            // 
            this.grpType.Controls.Add(this.btnShowMSA);
            this.grpType.Controls.Add(this.cboType);
            this.grpType.Controls.Add(this.grpPN);
            this.grpType.Controls.Add(this.btnTypeDelete);
            this.grpType.Controls.Add(this.btnTypeEdit);
            this.grpType.Controls.Add(this.btnTypeAdd);
            this.grpType.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grpType.Location = new System.Drawing.Point(214, 24);
            this.grpType.Name = "grpType";
            this.grpType.Size = new System.Drawing.Size(196, 342);
            this.grpType.TabIndex = 12;
            this.grpType.TabStop = false;
            this.grpType.Text = "PNType";
            // 
            // btnShowMSA
            // 
            this.btnShowMSA.Enabled = false;
            this.btnShowMSA.Location = new System.Drawing.Point(156, 63);
            this.btnShowMSA.Name = "btnShowMSA";
            this.btnShowMSA.Size = new System.Drawing.Size(34, 32);
            this.btnShowMSA.TabIndex = 30;
            this.btnShowMSA.Text = "...";
            this.btnShowMSA.UseVisualStyleBackColor = true;
            this.btnShowMSA.Click += new System.EventHandler(this.btnShowMSA_Click);
            // 
            // cboType
            // 
            this.cboType.Font = new System.Drawing.Font("新宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboType.FormattingEnabled = true;
            this.cboType.Location = new System.Drawing.Point(6, 66);
            this.cboType.Name = "cboType";
            this.cboType.Size = new System.Drawing.Size(144, 22);
            this.cboType.TabIndex = 29;
            this.cboType.SelectedIndexChanged += new System.EventHandler(this.cboType_SelectedIndexChanged);
            // 
            // grpPN
            // 
            this.grpPN.Controls.Add(this.btnShowMemory);
            this.grpPN.Controls.Add(this.btnPNDelete);
            this.grpPN.Controls.Add(this.btnPNEdit);
            this.grpPN.Controls.Add(this.btnPNAdd);
            this.grpPN.Controls.Add(this.listPN);
            this.grpPN.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grpPN.Location = new System.Drawing.Point(6, 99);
            this.grpPN.Name = "grpPN";
            this.grpPN.Size = new System.Drawing.Size(184, 243);
            this.grpPN.TabIndex = 28;
            this.grpPN.TabStop = false;
            this.grpPN.Text = "PN List";
            // 
            // btnShowMemory
            // 
            this.btnShowMemory.Enabled = false;
            this.btnShowMemory.Location = new System.Drawing.Point(144, 25);
            this.btnShowMemory.Name = "btnShowMemory";
            this.btnShowMemory.Size = new System.Drawing.Size(34, 32);
            this.btnShowMemory.TabIndex = 28;
            this.btnShowMemory.Text = "...";
            this.btnShowMemory.UseVisualStyleBackColor = true;
            this.btnShowMemory.Click += new System.EventHandler(this.btnShowMemory_Click_1);
            // 
            // btnPNDelete
            // 
            this.btnPNDelete.Enabled = false;
            this.btnPNDelete.Location = new System.Drawing.Point(99, 25);
            this.btnPNDelete.Name = "btnPNDelete";
            this.btnPNDelete.Size = new System.Drawing.Size(40, 32);
            this.btnPNDelete.TabIndex = 27;
            this.btnPNDelete.Text = "Del";
            this.btnPNDelete.UseVisualStyleBackColor = true;
            this.btnPNDelete.Click += new System.EventHandler(this.btnPNDelete_Click);
            // 
            // btnPNEdit
            // 
            this.btnPNEdit.Enabled = false;
            this.btnPNEdit.Location = new System.Drawing.Point(54, 25);
            this.btnPNEdit.Name = "btnPNEdit";
            this.btnPNEdit.Size = new System.Drawing.Size(40, 32);
            this.btnPNEdit.TabIndex = 26;
            this.btnPNEdit.Text = "Edit";
            this.btnPNEdit.UseVisualStyleBackColor = true;
            this.btnPNEdit.Click += new System.EventHandler(this.btnPNEdit_Click);
            // 
            // btnPNAdd
            // 
            this.btnPNAdd.Location = new System.Drawing.Point(6, 25);
            this.btnPNAdd.Name = "btnPNAdd";
            this.btnPNAdd.Size = new System.Drawing.Size(44, 32);
            this.btnPNAdd.TabIndex = 25;
            this.btnPNAdd.Text = "Add";
            this.btnPNAdd.UseVisualStyleBackColor = true;
            this.btnPNAdd.Click += new System.EventHandler(this.btnPNAdd_Click);
            // 
            // listPN
            // 
            this.listPN.Font = new System.Drawing.Font("新宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listPN.FormattingEnabled = true;
            this.listPN.ItemHeight = 14;
            this.listPN.Items.AddRange(new object[] {
            "TN-XX-XX",
            "TN-X1-X1"});
            this.listPN.Location = new System.Drawing.Point(6, 65);
            this.listPN.Name = "listPN";
            this.listPN.Size = new System.Drawing.Size(172, 172);
            this.listPN.TabIndex = 21;
            this.listPN.SelectedIndexChanged += new System.EventHandler(this.listPN_SelectedIndexChanged);
            // 
            // btnTypeDelete
            // 
            this.btnTypeDelete.Enabled = false;
            this.btnTypeDelete.Location = new System.Drawing.Point(140, 25);
            this.btnTypeDelete.Name = "btnTypeDelete";
            this.btnTypeDelete.Size = new System.Drawing.Size(50, 32);
            this.btnTypeDelete.TabIndex = 27;
            this.btnTypeDelete.Text = "Del";
            this.btnTypeDelete.UseVisualStyleBackColor = true;
            this.btnTypeDelete.Click += new System.EventHandler(this.btnTypeDelete_Click);
            // 
            // btnTypeEdit
            // 
            this.btnTypeEdit.Enabled = false;
            this.btnTypeEdit.Location = new System.Drawing.Point(74, 25);
            this.btnTypeEdit.Name = "btnTypeEdit";
            this.btnTypeEdit.Size = new System.Drawing.Size(50, 32);
            this.btnTypeEdit.TabIndex = 26;
            this.btnTypeEdit.Text = "Edit";
            this.btnTypeEdit.UseVisualStyleBackColor = true;
            this.btnTypeEdit.Click += new System.EventHandler(this.btnTypeEdit_Click);
            // 
            // btnTypeAdd
            // 
            this.btnTypeAdd.Location = new System.Drawing.Point(6, 25);
            this.btnTypeAdd.Name = "btnTypeAdd";
            this.btnTypeAdd.Size = new System.Drawing.Size(50, 32);
            this.btnTypeAdd.TabIndex = 25;
            this.btnTypeAdd.Text = "Add";
            this.btnTypeAdd.UseVisualStyleBackColor = true;
            this.btnTypeAdd.Click += new System.EventHandler(this.btnTypeAdd_Click);
            // 
            // grpMSA
            // 
            this.grpMSA.Controls.Add(this.btnMSADelete);
            this.grpMSA.Controls.Add(this.btnMSAEdit);
            this.grpMSA.Controls.Add(this.btnMSAAdd);
            this.grpMSA.Controls.Add(this.ListMSA);
            this.grpMSA.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grpMSA.Location = new System.Drawing.Point(12, 24);
            this.grpMSA.Name = "grpMSA";
            this.grpMSA.Size = new System.Drawing.Size(196, 166);
            this.grpMSA.TabIndex = 27;
            this.grpMSA.TabStop = false;
            this.grpMSA.Text = "MSA Info";
            // 
            // btnMSADelete
            // 
            this.btnMSADelete.Location = new System.Drawing.Point(140, 25);
            this.btnMSADelete.Name = "btnMSADelete";
            this.btnMSADelete.Size = new System.Drawing.Size(50, 32);
            this.btnMSADelete.TabIndex = 26;
            this.btnMSADelete.Text = "Del";
            this.btnMSADelete.UseVisualStyleBackColor = true;
            this.btnMSADelete.Click += new System.EventHandler(this.btnMSADelete_Click);
            // 
            // btnMSAEdit
            // 
            this.btnMSAEdit.Location = new System.Drawing.Point(73, 25);
            this.btnMSAEdit.Name = "btnMSAEdit";
            this.btnMSAEdit.Size = new System.Drawing.Size(50, 32);
            this.btnMSAEdit.TabIndex = 25;
            this.btnMSAEdit.Text = "Edit";
            this.btnMSAEdit.UseVisualStyleBackColor = true;
            this.btnMSAEdit.Click += new System.EventHandler(this.btnMSAEdit_Click);
            // 
            // btnMSAAdd
            // 
            this.btnMSAAdd.Location = new System.Drawing.Point(6, 25);
            this.btnMSAAdd.Name = "btnMSAAdd";
            this.btnMSAAdd.Size = new System.Drawing.Size(50, 32);
            this.btnMSAAdd.TabIndex = 24;
            this.btnMSAAdd.Text = "Add";
            this.btnMSAAdd.UseVisualStyleBackColor = true;
            this.btnMSAAdd.Click += new System.EventHandler(this.btnMSAAdd_Click);
            // 
            // ListMSA
            // 
            this.ListMSA.Font = new System.Drawing.Font("新宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ListMSA.FormattingEnabled = true;
            this.ListMSA.ItemHeight = 14;
            this.ListMSA.Items.AddRange(new object[] {
            "SFF-8636",
            "CFP MSA",
            "SFF-8472",
            "SFF-8077"});
            this.ListMSA.Location = new System.Drawing.Point(6, 61);
            this.ListMSA.Name = "ListMSA";
            this.ListMSA.Size = new System.Drawing.Size(184, 102);
            this.ListMSA.TabIndex = 20;
            this.ListMSA.SelectedIndexChanged += new System.EventHandler(this.ListMSA_SelectedIndexChanged);
            // 
            // grpAppModel
            // 
            this.grpAppModel.Controls.Add(this.btnAppDelete);
            this.grpAppModel.Controls.Add(this.btnAppEdit);
            this.grpAppModel.Controls.Add(this.btnAppAdd);
            this.grpAppModel.Controls.Add(this.listApp);
            this.grpAppModel.Enabled = false;
            this.grpAppModel.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grpAppModel.Location = new System.Drawing.Point(416, 24);
            this.grpAppModel.Name = "grpAppModel";
            this.grpAppModel.Size = new System.Drawing.Size(196, 342);
            this.grpAppModel.TabIndex = 28;
            this.grpAppModel.TabStop = false;
            this.grpAppModel.Text = "APP+ Model Info";
            // 
            // btnAppDelete
            // 
            this.btnAppDelete.Location = new System.Drawing.Point(141, 25);
            this.btnAppDelete.Name = "btnAppDelete";
            this.btnAppDelete.Size = new System.Drawing.Size(50, 32);
            this.btnAppDelete.TabIndex = 33;
            this.btnAppDelete.Text = "Del";
            this.btnAppDelete.UseVisualStyleBackColor = true;
            this.btnAppDelete.Click += new System.EventHandler(this.btnAppDelete_Click);
            // 
            // btnAppEdit
            // 
            this.btnAppEdit.Location = new System.Drawing.Point(75, 25);
            this.btnAppEdit.Name = "btnAppEdit";
            this.btnAppEdit.Size = new System.Drawing.Size(50, 32);
            this.btnAppEdit.TabIndex = 32;
            this.btnAppEdit.Text = "Edit";
            this.btnAppEdit.UseVisualStyleBackColor = true;
            this.btnAppEdit.Click += new System.EventHandler(this.btnAppEdit_Click);
            // 
            // btnAppAdd
            // 
            this.btnAppAdd.Location = new System.Drawing.Point(7, 25);
            this.btnAppAdd.Name = "btnAppAdd";
            this.btnAppAdd.Size = new System.Drawing.Size(50, 32);
            this.btnAppAdd.TabIndex = 31;
            this.btnAppAdd.Text = "Add";
            this.btnAppAdd.UseVisualStyleBackColor = true;
            this.btnAppAdd.Click += new System.EventHandler(this.btnAppAdd_Click);
            // 
            // listApp
            // 
            this.listApp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.listApp.Font = new System.Drawing.Font("新宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listApp.FormattingEnabled = true;
            this.listApp.ItemHeight = 14;
            this.listApp.Items.AddRange(new object[] {
            "APP_TX_CAL",
            "APP_TX_FMT",
            "APP_RX_CAL",
            "APP_RX_FMT",
            "APP_DUT_CAL",
            "APP_DUT_FMT",
            "APP_EDVT",
            "APP_EEPROM",
            "APP_DUT_Prepare"});
            this.listApp.Location = new System.Drawing.Point(7, 66);
            this.listApp.Name = "listApp";
            this.listApp.Size = new System.Drawing.Size(184, 270);
            this.listApp.TabIndex = 22;
            this.listApp.SelectedIndexChanged += new System.EventHandler(this.listApp_SelectedIndexChanged);
            // 
            // grpEquip
            // 
            this.grpEquip.Controls.Add(this.btnEquipDelete);
            this.grpEquip.Controls.Add(this.btnEquipEdit);
            this.grpEquip.Controls.Add(this.btnEquipAdd);
            this.grpEquip.Controls.Add(this.listEquip);
            this.grpEquip.Enabled = false;
            this.grpEquip.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grpEquip.Location = new System.Drawing.Point(618, 24);
            this.grpEquip.Name = "grpEquip";
            this.grpEquip.Size = new System.Drawing.Size(196, 342);
            this.grpEquip.TabIndex = 29;
            this.grpEquip.TabStop = false;
            this.grpEquip.Text = "Equipment Info";
            // 
            // btnEquipDelete
            // 
            this.btnEquipDelete.Location = new System.Drawing.Point(140, 25);
            this.btnEquipDelete.Name = "btnEquipDelete";
            this.btnEquipDelete.Size = new System.Drawing.Size(50, 32);
            this.btnEquipDelete.TabIndex = 30;
            this.btnEquipDelete.Text = "Del";
            this.btnEquipDelete.UseVisualStyleBackColor = true;
            this.btnEquipDelete.Click += new System.EventHandler(this.btnEquipDelete_Click);
            // 
            // btnEquipEdit
            // 
            this.btnEquipEdit.Location = new System.Drawing.Point(74, 25);
            this.btnEquipEdit.Name = "btnEquipEdit";
            this.btnEquipEdit.Size = new System.Drawing.Size(50, 32);
            this.btnEquipEdit.TabIndex = 29;
            this.btnEquipEdit.Text = "Edit";
            this.btnEquipEdit.UseVisualStyleBackColor = true;
            this.btnEquipEdit.Click += new System.EventHandler(this.btnEquipEdit_Click);
            // 
            // btnEquipAdd
            // 
            this.btnEquipAdd.Location = new System.Drawing.Point(6, 25);
            this.btnEquipAdd.Name = "btnEquipAdd";
            this.btnEquipAdd.Size = new System.Drawing.Size(50, 32);
            this.btnEquipAdd.TabIndex = 28;
            this.btnEquipAdd.Text = "Add";
            this.btnEquipAdd.UseVisualStyleBackColor = true;
            this.btnEquipAdd.Click += new System.EventHandler(this.btnEquipAdd_Click);
            // 
            // listEquip
            // 
            this.listEquip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.listEquip.Font = new System.Drawing.Font("新宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listEquip.FormattingEnabled = true;
            this.listEquip.ItemHeight = 14;
            this.listEquip.Items.AddRange(new object[] {
            "Powersupply",
            "PPG",
            "ErrorDetector",
            "Attennuator",
            "Scope",
            "Thermocontroller",
            "OpticalSwitch",
            "ElectricalSwitch"});
            this.listEquip.Location = new System.Drawing.Point(6, 65);
            this.listEquip.Name = "listEquip";
            this.listEquip.Size = new System.Drawing.Size(184, 270);
            this.listEquip.TabIndex = 22;
            this.listEquip.SelectedIndexChanged += new System.EventHandler(this.listEquip_SelectedIndexChanged);
            // 
            // grpMCoefGroup
            // 
            this.grpMCoefGroup.Controls.Add(this.btnMCoefGroupDelete);
            this.grpMCoefGroup.Controls.Add(this.btnMCoefGroupEdit);
            this.grpMCoefGroup.Controls.Add(this.btnMCoefGroupAdd);
            this.grpMCoefGroup.Controls.Add(this.listMCoefGroup);
            this.grpMCoefGroup.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grpMCoefGroup.Location = new System.Drawing.Point(12, 200);
            this.grpMCoefGroup.Name = "grpMCoefGroup";
            this.grpMCoefGroup.Size = new System.Drawing.Size(196, 166);
            this.grpMCoefGroup.TabIndex = 30;
            this.grpMCoefGroup.TabStop = false;
            this.grpMCoefGroup.Text = "MCoefGroup";
            // 
            // btnMCoefGroupDelete
            // 
            this.btnMCoefGroupDelete.Location = new System.Drawing.Point(140, 25);
            this.btnMCoefGroupDelete.Name = "btnMCoefGroupDelete";
            this.btnMCoefGroupDelete.Size = new System.Drawing.Size(50, 32);
            this.btnMCoefGroupDelete.TabIndex = 26;
            this.btnMCoefGroupDelete.Text = "Del";
            this.btnMCoefGroupDelete.UseVisualStyleBackColor = true;
            this.btnMCoefGroupDelete.Click += new System.EventHandler(this.btnMCoefGroupDelete_Click);
            // 
            // btnMCoefGroupEdit
            // 
            this.btnMCoefGroupEdit.Location = new System.Drawing.Point(73, 25);
            this.btnMCoefGroupEdit.Name = "btnMCoefGroupEdit";
            this.btnMCoefGroupEdit.Size = new System.Drawing.Size(50, 32);
            this.btnMCoefGroupEdit.TabIndex = 25;
            this.btnMCoefGroupEdit.Text = "Edit";
            this.btnMCoefGroupEdit.UseVisualStyleBackColor = true;
            this.btnMCoefGroupEdit.Click += new System.EventHandler(this.btnMCoefGroupEdit_Click);
            // 
            // btnMCoefGroupAdd
            // 
            this.btnMCoefGroupAdd.Location = new System.Drawing.Point(6, 25);
            this.btnMCoefGroupAdd.Name = "btnMCoefGroupAdd";
            this.btnMCoefGroupAdd.Size = new System.Drawing.Size(50, 32);
            this.btnMCoefGroupAdd.TabIndex = 24;
            this.btnMCoefGroupAdd.Text = "Add";
            this.btnMCoefGroupAdd.UseVisualStyleBackColor = true;
            this.btnMCoefGroupAdd.Click += new System.EventHandler(this.btnMCoefGroupAdd_Click);
            // 
            // listMCoefGroup
            // 
            this.listMCoefGroup.Font = new System.Drawing.Font("新宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listMCoefGroup.FormattingEnabled = true;
            this.listMCoefGroup.ItemHeight = 14;
            this.listMCoefGroup.Items.AddRange(new object[] {
            "x1-1",
            "x2-2"});
            this.listMCoefGroup.Location = new System.Drawing.Point(6, 60);
            this.listMCoefGroup.Name = "listMCoefGroup";
            this.listMCoefGroup.Size = new System.Drawing.Size(184, 102);
            this.listMCoefGroup.TabIndex = 20;
            this.listMCoefGroup.SelectedIndexChanged += new System.EventHandler(this.listMCoefGroup_SelectedIndexChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(816, 394);
            this.Controls.Add(this.grpMCoefGroup);
            this.Controls.Add(this.grpEquip);
            this.Controls.Add(this.grpAppModel);
            this.Controls.Add(this.grpMSA);
            this.Controls.Add(this.mspMain);
            this.Controls.Add(this.ssrRunMsg);
            this.Controls.Add(this.grpType);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(832, 432);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ssrRunMsg.ResumeLayout(false);
            this.ssrRunMsg.PerformLayout();
            this.mspMain.ResumeLayout(false);
            this.mspMain.PerformLayout();
            this.grpType.ResumeLayout(false);
            this.grpPN.ResumeLayout(false);
            this.grpMSA.ResumeLayout(false);
            this.grpAppModel.ResumeLayout(false);
            this.grpEquip.ResumeLayout(false);
            this.grpMCoefGroup.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip ssrRunMsg;
        private System.Windows.Forms.ToolStripStatusLabel sslRunMsg;
        private System.Windows.Forms.ToolStripStatusLabel sslInfo;
        private System.Windows.Forms.ToolStripStatusLabel tsuserInfo;
        private System.Windows.Forms.MenuStrip mspMain;
        private System.Windows.Forms.ToolStripMenuItem tsmUpdate;
        private System.Windows.Forms.ToolStripMenuItem tsmCancel;
        private System.Windows.Forms.ToolStripMenuItem tsmRefresh;
        private System.Windows.Forms.ToolStripMenuItem tsmExit;
        private System.Windows.Forms.ToolStripStatusLabel tssTimelbl;
        private System.Windows.Forms.Timer timerDate;
        private System.Windows.Forms.GroupBox grpType;
        private System.Windows.Forms.GroupBox grpMSA;
        private System.Windows.Forms.ListBox ListMSA;
        private System.Windows.Forms.GroupBox grpAppModel;
        private System.Windows.Forms.ListBox listApp;
        private System.Windows.Forms.GroupBox grpEquip;
        private System.Windows.Forms.ListBox listEquip;
        private System.Windows.Forms.Button btnMSADelete;
        private System.Windows.Forms.Button btnMSAEdit;
        private System.Windows.Forms.Button btnMSAAdd;
        private System.Windows.Forms.GroupBox grpPN;
        private System.Windows.Forms.Button btnPNDelete;
        private System.Windows.Forms.Button btnPNEdit;
        private System.Windows.Forms.Button btnPNAdd;
        private System.Windows.Forms.ListBox listPN;
        private System.Windows.Forms.Button btnTypeDelete;
        private System.Windows.Forms.Button btnTypeEdit;
        private System.Windows.Forms.Button btnTypeAdd;
        private System.Windows.Forms.Button btnAppDelete;
        private System.Windows.Forms.Button btnAppEdit;
        private System.Windows.Forms.Button btnAppAdd;
        private System.Windows.Forms.Button btnEquipEdit;
        private System.Windows.Forms.ComboBox cboType;
        private System.Windows.Forms.Button btnShowMSA;
        private System.Windows.Forms.Button btnShowMemory;
        private System.Windows.Forms.Button btnEquipDelete;
        private System.Windows.Forms.Button btnEquipAdd;
        private System.Windows.Forms.ToolStripMenuItem tsmUserInfo;
        private System.Windows.Forms.ToolStripMenuItem tsmRoleInfo;
        private System.Windows.Forms.ToolStripMenuItem tsmFuncInfo;
        private System.Windows.Forms.GroupBox grpMCoefGroup;
        private System.Windows.Forms.Button btnMCoefGroupDelete;
        private System.Windows.Forms.Button btnMCoefGroupEdit;
        private System.Windows.Forms.Button btnMCoefGroupAdd;
        private System.Windows.Forms.ListBox listMCoefGroup;
    }
}
namespace Maintain
{
    partial class CtrlInfo
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
            this.lblTestPlanName = new System.Windows.Forms.Label();
            this.grpItem = new System.Windows.Forms.GroupBox();
            this.txtTempOffset = new System.Windows.Forms.TextBox();
            this.lblTempOffset = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.chkIgnoreFlag = new System.Windows.Forms.CheckBox();
            this.lblIgnore = new System.Windows.Forms.Label();
            this.txtAuxAttribles = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.cboDataRate = new System.Windows.Forms.ComboBox();
            this.lblDataRate = new System.Windows.Forms.Label();
            this.cboPattent = new System.Windows.Forms.ComboBox();
            this.lblPattent = new System.Windows.Forms.Label();
            this.cboVcc = new System.Windows.Forms.ComboBox();
            this.lblVcc = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtTestPlanName = new System.Windows.Forms.TextBox();
            this.lblAuxAttribles = new System.Windows.Forms.Label();
            this.cboTemp = new System.Windows.Forms.ComboBox();
            this.lblTemp = new System.Windows.Forms.Label();
            this.cboChannel = new System.Windows.Forms.ComboBox();
            this.lblChannel = new System.Windows.Forms.Label();
            this.cboItemName = new System.Windows.Forms.ComboBox();
            this.lblItemName = new System.Windows.Forms.Label();
            this.currlst = new System.Windows.Forms.ListBox();
            this.btnFinish = new System.Windows.Forms.Button();
            this.txtShowInfo = new System.Windows.Forms.TextBox();
            this.cboPrmtrItem = new System.Windows.Forms.ComboBox();
            this.btnPrmtrSave = new System.Windows.Forms.Button();
            this.lblPrmtrValue = new System.Windows.Forms.Label();
            this.lblPrmtrItem = new System.Windows.Forms.Label();
            this.cboPrmtrValue = new System.Windows.Forms.ComboBox();
            this.grpPrmtr = new System.Windows.Forms.GroupBox();
            this.btnPrmtrAdd = new System.Windows.Forms.Button();
            this.btnPrmtrDelete = new System.Windows.Forms.Button();
            this.txtSaveResult = new System.Windows.Forms.TextBox();
            this.dgvPrmtr = new System.Windows.Forms.DataGridView();
            this.txtItem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtItemValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cboCtrlType = new System.Windows.Forms.ComboBox();
            this.lblCtrlType = new System.Windows.Forms.Label();
            this.grpItem.SuspendLayout();
            this.grpPrmtr.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPrmtr)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTestPlanName
            // 
            this.lblTestPlanName.AutoSize = true;
            this.lblTestPlanName.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTestPlanName.Location = new System.Drawing.Point(12, 17);
            this.lblTestPlanName.Name = "lblTestPlanName";
            this.lblTestPlanName.Size = new System.Drawing.Size(143, 14);
            this.lblTestPlanName.TabIndex = 23;
            this.lblTestPlanName.Text = "Flow Control List";
            // 
            // grpItem
            // 
            this.grpItem.Controls.Add(this.txtTempOffset);
            this.grpItem.Controls.Add(this.lblTempOffset);
            this.grpItem.Controls.Add(this.cboCtrlType);
            this.grpItem.Controls.Add(this.lblCtrlType);
            this.grpItem.Controls.Add(this.lblDescription);
            this.grpItem.Controls.Add(this.txtDescription);
            this.grpItem.Controls.Add(this.chkIgnoreFlag);
            this.grpItem.Controls.Add(this.lblIgnore);
            this.grpItem.Controls.Add(this.txtAuxAttribles);
            this.grpItem.Controls.Add(this.btnOK);
            this.grpItem.Controls.Add(this.cboDataRate);
            this.grpItem.Controls.Add(this.lblDataRate);
            this.grpItem.Controls.Add(this.cboPattent);
            this.grpItem.Controls.Add(this.lblPattent);
            this.grpItem.Controls.Add(this.cboVcc);
            this.grpItem.Controls.Add(this.lblVcc);
            this.grpItem.Controls.Add(this.label10);
            this.grpItem.Controls.Add(this.txtTestPlanName);
            this.grpItem.Controls.Add(this.lblAuxAttribles);
            this.grpItem.Controls.Add(this.cboTemp);
            this.grpItem.Controls.Add(this.lblTemp);
            this.grpItem.Controls.Add(this.cboChannel);
            this.grpItem.Controls.Add(this.lblChannel);
            this.grpItem.Controls.Add(this.cboItemName);
            this.grpItem.Controls.Add(this.lblItemName);
            this.grpItem.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grpItem.Location = new System.Drawing.Point(170, 17);
            this.grpItem.Name = "grpItem";
            this.grpItem.Size = new System.Drawing.Size(295, 372);
            this.grpItem.TabIndex = 20;
            this.grpItem.TabStop = false;
            this.grpItem.Text = "FlowControl Info";
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDescription.Location = new System.Drawing.Point(6, 317);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(71, 12);
            this.lblDescription.TabIndex = 64;
            this.lblDescription.Text = "Description";
            // 
            // txtDescription
            // 
            this.txtDescription.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtDescription.Location = new System.Drawing.Point(94, 308);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescription.Size = new System.Drawing.Size(193, 32);
            this.txtDescription.TabIndex = 63;
            // 
            // chkIgnoreFlag
            // 
            this.chkIgnoreFlag.AutoSize = true;
            this.chkIgnoreFlag.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkIgnoreFlag.ForeColor = System.Drawing.Color.Blue;
            this.chkIgnoreFlag.Location = new System.Drawing.Point(105, 257);
            this.chkIgnoreFlag.Name = "chkIgnoreFlag";
            this.chkIgnoreFlag.Size = new System.Drawing.Size(171, 16);
            this.chkIgnoreFlag.TabIndex = 26;
            this.chkIgnoreFlag.Text = "<Item is Enabled now>";
            this.chkIgnoreFlag.UseVisualStyleBackColor = true;
            this.chkIgnoreFlag.CheckedChanged += new System.EventHandler(this.chkIgnoreFlag_CheckedChanged);
            // 
            // lblIgnore
            // 
            this.lblIgnore.AutoSize = true;
            this.lblIgnore.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblIgnore.Location = new System.Drawing.Point(6, 258);
            this.lblIgnore.Name = "lblIgnore";
            this.lblIgnore.Size = new System.Drawing.Size(95, 12);
            this.lblIgnore.TabIndex = 25;
            this.lblIgnore.Text = "Ignore currItem";
            // 
            // txtAuxAttribles
            // 
            this.txtAuxAttribles.Location = new System.Drawing.Point(93, 279);
            this.txtAuxAttribles.Name = "txtAuxAttribles";
            this.txtAuxAttribles.ReadOnly = true;
            this.txtAuxAttribles.Size = new System.Drawing.Size(194, 23);
            this.txtAuxAttribles.TabIndex = 22;
            this.txtAuxAttribles.MouseDown += new System.Windows.Forms.MouseEventHandler(this.txtAuxAttribles_MouseDown);
            // 
            // btnOK
            // 
            this.btnOK.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOK.Location = new System.Drawing.Point(106, 343);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 21;
            this.btnOK.Text = "Save";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // cboDataRate
            // 
            this.cboDataRate.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboDataRate.FormattingEnabled = true;
            this.cboDataRate.Location = new System.Drawing.Point(107, 179);
            this.cboDataRate.Name = "cboDataRate";
            this.cboDataRate.Size = new System.Drawing.Size(182, 20);
            this.cboDataRate.TabIndex = 20;
            this.cboDataRate.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cboDataRate_MouseClick);
            // 
            // lblDataRate
            // 
            this.lblDataRate.AutoSize = true;
            this.lblDataRate.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDataRate.Location = new System.Drawing.Point(6, 182);
            this.lblDataRate.Name = "lblDataRate";
            this.lblDataRate.Size = new System.Drawing.Size(53, 12);
            this.lblDataRate.TabIndex = 19;
            this.lblDataRate.Text = "DataRate";
            // 
            // cboPattent
            // 
            this.cboPattent.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboPattent.FormattingEnabled = true;
            this.cboPattent.Location = new System.Drawing.Point(107, 153);
            this.cboPattent.Name = "cboPattent";
            this.cboPattent.Size = new System.Drawing.Size(182, 20);
            this.cboPattent.TabIndex = 18;
            this.cboPattent.Leave += new System.EventHandler(this.cboPattent_Leave);
            this.cboPattent.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cboPattent_MouseClick);
            // 
            // lblPattent
            // 
            this.lblPattent.AutoSize = true;
            this.lblPattent.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPattent.Location = new System.Drawing.Point(6, 156);
            this.lblPattent.Name = "lblPattent";
            this.lblPattent.Size = new System.Drawing.Size(47, 12);
            this.lblPattent.TabIndex = 17;
            this.lblPattent.Text = "Pattern";
            // 
            // cboVcc
            // 
            this.cboVcc.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboVcc.FormattingEnabled = true;
            this.cboVcc.Location = new System.Drawing.Point(107, 127);
            this.cboVcc.Name = "cboVcc";
            this.cboVcc.Size = new System.Drawing.Size(182, 20);
            this.cboVcc.TabIndex = 16;
            this.cboVcc.Leave += new System.EventHandler(this.cboVcc_Leave);
            this.cboVcc.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cboVcc_MouseClick);
            // 
            // lblVcc
            // 
            this.lblVcc.AutoSize = true;
            this.lblVcc.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblVcc.Location = new System.Drawing.Point(6, 130);
            this.lblVcc.Name = "lblVcc";
            this.lblVcc.Size = new System.Drawing.Size(47, 12);
            this.lblVcc.TabIndex = 15;
            this.lblVcc.Text = "Voltage";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(3, 23);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(77, 12);
            this.label10.TabIndex = 13;
            this.label10.Text = "TestPlanName";
            // 
            // txtTestPlanName
            // 
            this.txtTestPlanName.Enabled = false;
            this.txtTestPlanName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtTestPlanName.Location = new System.Drawing.Point(107, 20);
            this.txtTestPlanName.Name = "txtTestPlanName";
            this.txtTestPlanName.Size = new System.Drawing.Size(182, 21);
            this.txtTestPlanName.TabIndex = 12;
            this.txtTestPlanName.Text = "TestPlanName";
            this.txtTestPlanName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblAuxAttribles
            // 
            this.lblAuxAttribles.AutoSize = true;
            this.lblAuxAttribles.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblAuxAttribles.Location = new System.Drawing.Point(6, 284);
            this.lblAuxAttribles.Name = "lblAuxAttribles";
            this.lblAuxAttribles.Size = new System.Drawing.Size(77, 12);
            this.lblAuxAttribles.TabIndex = 10;
            this.lblAuxAttribles.Text = "AuxAttribles";
            // 
            // cboTemp
            // 
            this.cboTemp.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboTemp.FormattingEnabled = true;
            this.cboTemp.Location = new System.Drawing.Point(107, 101);
            this.cboTemp.Name = "cboTemp";
            this.cboTemp.Size = new System.Drawing.Size(182, 20);
            this.cboTemp.TabIndex = 7;
            this.cboTemp.Leave += new System.EventHandler(this.cboTemp_Leave);
            this.cboTemp.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cboTemp_MouseClick);
            // 
            // lblTemp
            // 
            this.lblTemp.AutoSize = true;
            this.lblTemp.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTemp.Location = new System.Drawing.Point(6, 104);
            this.lblTemp.Name = "lblTemp";
            this.lblTemp.Size = new System.Drawing.Size(71, 12);
            this.lblTemp.TabIndex = 6;
            this.lblTemp.Text = "Temperature";
            // 
            // cboChannel
            // 
            this.cboChannel.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboChannel.FormattingEnabled = true;
            this.cboChannel.Location = new System.Drawing.Point(107, 75);
            this.cboChannel.Name = "cboChannel";
            this.cboChannel.Size = new System.Drawing.Size(182, 20);
            this.cboChannel.TabIndex = 5;
            this.cboChannel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cboChannel_MouseClick);
            // 
            // lblChannel
            // 
            this.lblChannel.AutoSize = true;
            this.lblChannel.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblChannel.Location = new System.Drawing.Point(6, 78);
            this.lblChannel.Name = "lblChannel";
            this.lblChannel.Size = new System.Drawing.Size(95, 12);
            this.lblChannel.TabIndex = 4;
            this.lblChannel.Text = "ModuleChannelNo";
            // 
            // cboItemName
            // 
            this.cboItemName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboItemName.FormattingEnabled = true;
            this.cboItemName.Location = new System.Drawing.Point(107, 47);
            this.cboItemName.Name = "cboItemName";
            this.cboItemName.Size = new System.Drawing.Size(182, 20);
            this.cboItemName.TabIndex = 1;
            this.cboItemName.Leave += new System.EventHandler(this.cboItemName_Leave);
            this.cboItemName.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cboItemName_MouseClick);
            // 
            // lblItemName
            // 
            this.lblItemName.AutoSize = true;
            this.lblItemName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.lblItemName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblItemName.Location = new System.Drawing.Point(6, 50);
            this.lblItemName.Name = "lblItemName";
            this.lblItemName.Size = new System.Drawing.Size(77, 12);
            this.lblItemName.TabIndex = 0;
            this.lblItemName.Text = "CurrItemName";
            this.lblItemName.DoubleClick += new System.EventHandler(this.lblItemName_DoubleClick);
            // 
            // currlst
            // 
            this.currlst.FormattingEnabled = true;
            this.currlst.HorizontalScrollbar = true;
            this.currlst.ItemHeight = 12;
            this.currlst.Items.AddRange(new object[] {
            "Condition1",
            "Condition2",
            "Condition3",
            "...",
            "Condition*"});
            this.currlst.Location = new System.Drawing.Point(6, 37);
            this.currlst.Name = "currlst";
            this.currlst.ScrollAlwaysVisible = true;
            this.currlst.Size = new System.Drawing.Size(158, 352);
            this.currlst.TabIndex = 19;
            this.currlst.SelectedIndexChanged += new System.EventHandler(this.currlst_SelectedIndexChanged);
            // 
            // btnFinish
            // 
            this.btnFinish.Enabled = false;
            this.btnFinish.Location = new System.Drawing.Point(196, 421);
            this.btnFinish.Name = "btnFinish";
            this.btnFinish.Size = new System.Drawing.Size(75, 22);
            this.btnFinish.TabIndex = 24;
            this.btnFinish.Text = "完成";
            this.btnFinish.UseVisualStyleBackColor = true;
            this.btnFinish.Click += new System.EventHandler(this.btnFinish_Click);
            // 
            // txtShowInfo
            // 
            this.txtShowInfo.AcceptsReturn = true;
            this.txtShowInfo.Location = new System.Drawing.Point(0, 391);
            this.txtShowInfo.Multiline = true;
            this.txtShowInfo.Name = "txtShowInfo";
            this.txtShowInfo.ReadOnly = true;
            this.txtShowInfo.Size = new System.Drawing.Size(465, 26);
            this.txtShowInfo.TabIndex = 25;
            // 
            // cboPrmtrItem
            // 
            this.cboPrmtrItem.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboPrmtrItem.FormattingEnabled = true;
            this.cboPrmtrItem.Location = new System.Drawing.Point(34, 184);
            this.cboPrmtrItem.Name = "cboPrmtrItem";
            this.cboPrmtrItem.Size = new System.Drawing.Size(132, 20);
            this.cboPrmtrItem.TabIndex = 50;
            // 
            // btnPrmtrSave
            // 
            this.btnPrmtrSave.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPrmtrSave.Location = new System.Drawing.Point(153, 220);
            this.btnPrmtrSave.Name = "btnPrmtrSave";
            this.btnPrmtrSave.Size = new System.Drawing.Size(75, 22);
            this.btnPrmtrSave.TabIndex = 57;
            this.btnPrmtrSave.Text = "Save Aux";
            this.btnPrmtrSave.UseVisualStyleBackColor = true;
            this.btnPrmtrSave.Click += new System.EventHandler(this.btnPrmtrSave_Click);
            // 
            // lblPrmtrValue
            // 
            this.lblPrmtrValue.AutoSize = true;
            this.lblPrmtrValue.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPrmtrValue.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblPrmtrValue.Location = new System.Drawing.Point(172, 187);
            this.lblPrmtrValue.Name = "lblPrmtrValue";
            this.lblPrmtrValue.Size = new System.Drawing.Size(35, 12);
            this.lblPrmtrValue.TabIndex = 55;
            this.lblPrmtrValue.Text = "Value";
            // 
            // lblPrmtrItem
            // 
            this.lblPrmtrItem.AutoSize = true;
            this.lblPrmtrItem.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPrmtrItem.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblPrmtrItem.Location = new System.Drawing.Point(9, 189);
            this.lblPrmtrItem.Name = "lblPrmtrItem";
            this.lblPrmtrItem.Size = new System.Drawing.Size(23, 12);
            this.lblPrmtrItem.TabIndex = 51;
            this.lblPrmtrItem.Text = "Key";
            // 
            // cboPrmtrValue
            // 
            this.cboPrmtrValue.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboPrmtrValue.FormattingEnabled = true;
            this.cboPrmtrValue.Location = new System.Drawing.Point(213, 184);
            this.cboPrmtrValue.Name = "cboPrmtrValue";
            this.cboPrmtrValue.Size = new System.Drawing.Size(150, 20);
            this.cboPrmtrValue.TabIndex = 52;
            // 
            // grpPrmtr
            // 
            this.grpPrmtr.Controls.Add(this.btnPrmtrSave);
            this.grpPrmtr.Controls.Add(this.cboPrmtrItem);
            this.grpPrmtr.Controls.Add(this.btnPrmtrAdd);
            this.grpPrmtr.Controls.Add(this.btnPrmtrDelete);
            this.grpPrmtr.Controls.Add(this.lblPrmtrValue);
            this.grpPrmtr.Controls.Add(this.txtSaveResult);
            this.grpPrmtr.Controls.Add(this.dgvPrmtr);
            this.grpPrmtr.Controls.Add(this.cboPrmtrValue);
            this.grpPrmtr.Controls.Add(this.lblPrmtrItem);
            this.grpPrmtr.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grpPrmtr.Location = new System.Drawing.Point(471, 17);
            this.grpPrmtr.Name = "grpPrmtr";
            this.grpPrmtr.Size = new System.Drawing.Size(369, 244);
            this.grpPrmtr.TabIndex = 48;
            this.grpPrmtr.TabStop = false;
            this.grpPrmtr.Text = "AuxAttribles Setting";
            this.grpPrmtr.Visible = false;
            // 
            // btnPrmtrAdd
            // 
            this.btnPrmtrAdd.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnPrmtrAdd.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPrmtrAdd.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnPrmtrAdd.Location = new System.Drawing.Point(53, 219);
            this.btnPrmtrAdd.Name = "btnPrmtrAdd";
            this.btnPrmtrAdd.Size = new System.Drawing.Size(70, 23);
            this.btnPrmtrAdd.TabIndex = 61;
            this.btnPrmtrAdd.Text = "AddNew";
            this.btnPrmtrAdd.UseVisualStyleBackColor = false;
            this.btnPrmtrAdd.Click += new System.EventHandler(this.btnPrmtrAdd_Click);
            // 
            // btnPrmtrDelete
            // 
            this.btnPrmtrDelete.BackColor = System.Drawing.Color.Fuchsia;
            this.btnPrmtrDelete.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPrmtrDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnPrmtrDelete.Location = new System.Drawing.Point(267, 219);
            this.btnPrmtrDelete.Name = "btnPrmtrDelete";
            this.btnPrmtrDelete.Size = new System.Drawing.Size(65, 22);
            this.btnPrmtrDelete.TabIndex = 60;
            this.btnPrmtrDelete.Text = "Delete";
            this.btnPrmtrDelete.UseVisualStyleBackColor = false;
            this.btnPrmtrDelete.Click += new System.EventHandler(this.btnPrmtrDelete_Click);
            // 
            // txtSaveResult
            // 
            this.txtSaveResult.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtSaveResult.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtSaveResult.Location = new System.Drawing.Point(6, 372);
            this.txtSaveResult.Multiline = true;
            this.txtSaveResult.Name = "txtSaveResult";
            this.txtSaveResult.ReadOnly = true;
            this.txtSaveResult.Size = new System.Drawing.Size(357, 24);
            this.txtSaveResult.TabIndex = 23;
            // 
            // dgvPrmtr
            // 
            this.dgvPrmtr.AllowUserToAddRows = false;
            this.dgvPrmtr.AllowUserToDeleteRows = false;
            this.dgvPrmtr.AllowUserToResizeColumns = false;
            this.dgvPrmtr.AllowUserToResizeRows = false;
            this.dgvPrmtr.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvPrmtr.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPrmtr.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.txtItem,
            this.txtItemValue});
            this.dgvPrmtr.Location = new System.Drawing.Point(6, 17);
            this.dgvPrmtr.Name = "dgvPrmtr";
            this.dgvPrmtr.ReadOnly = true;
            this.dgvPrmtr.RowHeadersVisible = false;
            this.dgvPrmtr.RowTemplate.Height = 23;
            this.dgvPrmtr.Size = new System.Drawing.Size(357, 163);
            this.dgvPrmtr.TabIndex = 3;
            this.dgvPrmtr.CurrentCellChanged += new System.EventHandler(this.dgvPrmtr_CurrentCellChanged);
            this.dgvPrmtr.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgvPrmtr_MouseClick);
            // 
            // txtItem
            // 
            this.txtItem.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.txtItem.HeaderText = "Key";
            this.txtItem.Name = "txtItem";
            this.txtItem.ReadOnly = true;
            this.txtItem.Width = 160;
            // 
            // txtItemValue
            // 
            this.txtItemValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.txtItemValue.HeaderText = "Value";
            this.txtItemValue.Name = "txtItemValue";
            this.txtItemValue.ReadOnly = true;
            this.txtItemValue.Width = 186;
            // 
            // cboCtrlType
            // 
            this.cboCtrlType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCtrlType.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboCtrlType.FormattingEnabled = true;
            this.cboCtrlType.Items.AddRange(new object[] {
            "LP",
            "FMT",
            "Both"});
            this.cboCtrlType.Location = new System.Drawing.Point(107, 205);
            this.cboCtrlType.Name = "cboCtrlType";
            this.cboCtrlType.Size = new System.Drawing.Size(182, 20);
            this.cboCtrlType.TabIndex = 66;
            // 
            // lblCtrlType
            // 
            this.lblCtrlType.AutoSize = true;
            this.lblCtrlType.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCtrlType.Location = new System.Drawing.Point(3, 208);
            this.lblCtrlType.Name = "lblCtrlType";
            this.lblCtrlType.Size = new System.Drawing.Size(71, 12);
            this.lblCtrlType.TabIndex = 65;
            this.lblCtrlType.Text = "ControlType";
            // 
            // txtTempOffset
            // 
            this.txtTempOffset.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtTempOffset.Location = new System.Drawing.Point(107, 231);
            this.txtTempOffset.Name = "txtTempOffset";
            this.txtTempOffset.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtTempOffset.Size = new System.Drawing.Size(182, 21);
            this.txtTempOffset.TabIndex = 70;
            this.txtTempOffset.Leave += new System.EventHandler(this.txtTempOffset_Leave);
            // 
            // lblTempOffset
            // 
            this.lblTempOffset.AutoSize = true;
            this.lblTempOffset.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTempOffset.Location = new System.Drawing.Point(3, 234);
            this.lblTempOffset.Name = "lblTempOffset";
            this.lblTempOffset.Size = new System.Drawing.Size(65, 12);
            this.lblTempOffset.TabIndex = 69;
            this.lblTempOffset.Text = "TempOffset";
            // 
            // CtrlInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(843, 442);
            this.Controls.Add(this.grpPrmtr);
            this.Controls.Add(this.txtShowInfo);
            this.Controls.Add(this.btnFinish);
            this.Controls.Add(this.lblTestPlanName);
            this.Controls.Add(this.grpItem);
            this.Controls.Add(this.currlst);
            this.MaximumSize = new System.Drawing.Size(859, 480);
            this.MinimumSize = new System.Drawing.Size(489, 450);
            this.Name = "CtrlInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CtrlInfo";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CtrlInfo_FormClosing);
            this.Load += new System.EventHandler(this.CtrlInfo_Load);
            this.grpItem.ResumeLayout(false);
            this.grpItem.PerformLayout();
            this.grpPrmtr.ResumeLayout(false);
            this.grpPrmtr.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPrmtr)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTestPlanName;
        private System.Windows.Forms.GroupBox grpItem;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtTestPlanName;
        private System.Windows.Forms.Label lblAuxAttribles;
        private System.Windows.Forms.ComboBox cboTemp;
        private System.Windows.Forms.Label lblTemp;
        private System.Windows.Forms.ComboBox cboChannel;
        private System.Windows.Forms.Label lblChannel;
        private System.Windows.Forms.ComboBox cboItemName;
        private System.Windows.Forms.Label lblItemName;
        private System.Windows.Forms.ListBox currlst;
        private System.Windows.Forms.ComboBox cboDataRate;
        private System.Windows.Forms.Label lblDataRate;
        private System.Windows.Forms.ComboBox cboPattent;
        private System.Windows.Forms.Label lblPattent;
        private System.Windows.Forms.ComboBox cboVcc;
        private System.Windows.Forms.Label lblVcc;
        private System.Windows.Forms.Button btnFinish;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TextBox txtShowInfo;
        private System.Windows.Forms.ComboBox cboPrmtrItem;
        private System.Windows.Forms.Button btnPrmtrSave;
        private System.Windows.Forms.Label lblPrmtrValue;
        private System.Windows.Forms.Label lblPrmtrItem;
        private System.Windows.Forms.ComboBox cboPrmtrValue;
        private System.Windows.Forms.GroupBox grpPrmtr;
        private System.Windows.Forms.Button btnPrmtrAdd;
        private System.Windows.Forms.Button btnPrmtrDelete;
        private System.Windows.Forms.TextBox txtSaveResult;
        private System.Windows.Forms.DataGridView dgvPrmtr;
        private System.Windows.Forms.TextBox txtAuxAttribles;
        private System.Windows.Forms.CheckBox chkIgnoreFlag;
        private System.Windows.Forms.Label lblIgnore;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtItemValue;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.ComboBox cboCtrlType;
        private System.Windows.Forms.Label lblCtrlType;
        private System.Windows.Forms.TextBox txtTempOffset;
        private System.Windows.Forms.Label lblTempOffset;
    }
}
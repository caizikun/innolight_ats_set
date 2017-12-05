namespace GlobalInfo
{
    partial class ModelInfo
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
            this.btnFinish = new System.Windows.Forms.Button();
            this.grpAPP = new System.Windows.Forms.GroupBox();
            this.globaApplistName = new System.Windows.Forms.ListBox();
            this.grpModel = new System.Windows.Forms.GroupBox();
            this.btnModelAdd = new System.Windows.Forms.Button();
            this.btnModelDelete = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.currlst = new System.Windows.Forms.ListBox();
            this.btnRemove = new System.Windows.Forms.Button();
            this.grpPrmtr = new System.Windows.Forms.GroupBox();
            this.txtPrmtrDescription = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.cboDataRecord = new System.Windows.Forms.ComboBox();
            this.cboLogRecord = new System.Windows.Forms.ComboBox();
            this.cboFailbreak = new System.Windows.Forms.ComboBox();
            this.cboItemSpecific = new System.Windows.Forms.ComboBox();
            this.btnPrmtrOK = new System.Windows.Forms.Button();
            this.cboSpecMax = new System.Windows.Forms.ComboBox();
            this.cboSpecMin = new System.Windows.Forms.ComboBox();
            this.cboItemValue = new System.Windows.Forms.ComboBox();
            this.cboItemType = new System.Windows.Forms.ComboBox();
            this.cboDirection = new System.Windows.Forms.ComboBox();
            this.cboItemName = new System.Windows.Forms.ComboBox();
            this.grpEquipPrmtr = new System.Windows.Forms.GroupBox();
            this.btnPrmtrDelete = new System.Windows.Forms.Button();
            this.btnPrmtrAdd = new System.Windows.Forms.Button();
            this.txtSaveResult = new System.Windows.Forms.TextBox();
            this.dgvPrmtr = new System.Windows.Forms.DataGridView();
            this.grpItem = new System.Windows.Forms.GroupBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblEquipDescription = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.cboModelName = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.grpAPP.SuspendLayout();
            this.grpModel.SuspendLayout();
            this.grpPrmtr.SuspendLayout();
            this.grpEquipPrmtr.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPrmtr)).BeginInit();
            this.grpItem.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnFinish
            // 
            this.btnFinish.Location = new System.Drawing.Point(194, 381);
            this.btnFinish.Name = "btnFinish";
            this.btnFinish.Size = new System.Drawing.Size(116, 31);
            this.btnFinish.TabIndex = 28;
            this.btnFinish.Text = "Finish";
            this.btnFinish.UseVisualStyleBackColor = true;
            this.btnFinish.Click += new System.EventHandler(this.btnFinish_Click);
            // 
            // grpAPP
            // 
            this.grpAPP.Controls.Add(this.globaApplistName);
            this.grpAPP.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grpAPP.Location = new System.Drawing.Point(3, 9);
            this.grpAPP.Name = "grpAPP";
            this.grpAPP.Size = new System.Drawing.Size(141, 326);
            this.grpAPP.TabIndex = 53;
            this.grpAPP.TabStop = false;
            this.grpAPP.Text = "App List";
            // 
            // globaApplistName
            // 
            this.globaApplistName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.globaApplistName.FormattingEnabled = true;
            this.globaApplistName.HorizontalScrollbar = true;
            this.globaApplistName.ItemHeight = 12;
            this.globaApplistName.Items.AddRange(new object[] {
            "APP_TX_CAL",
            "APP_TX_FMT",
            "APP_RX_CAL",
            "APP_RX_FMT",
            "APP_DUT_CAL",
            "APP_DUT_FMT",
            "APP_EDVT",
            "APP_EEPROM",
            "APP_DUT_Prepare"});
            this.globaApplistName.Location = new System.Drawing.Point(8, 17);
            this.globaApplistName.Name = "globaApplistName";
            this.globaApplistName.ScrollAlwaysVisible = true;
            this.globaApplistName.Size = new System.Drawing.Size(126, 304);
            this.globaApplistName.TabIndex = 53;
            this.globaApplistName.SelectedIndexChanged += new System.EventHandler(this.globaApplistName_SelectedIndexChanged);
            // 
            // grpModel
            // 
            this.grpModel.Controls.Add(this.btnModelAdd);
            this.grpModel.Controls.Add(this.btnModelDelete);
            this.grpModel.Controls.Add(this.btnAdd);
            this.grpModel.Controls.Add(this.currlst);
            this.grpModel.Controls.Add(this.btnRemove);
            this.grpModel.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grpModel.Location = new System.Drawing.Point(150, 9);
            this.grpModel.Name = "grpModel";
            this.grpModel.Size = new System.Drawing.Size(193, 172);
            this.grpModel.TabIndex = 54;
            this.grpModel.TabStop = false;
            this.grpModel.Text = "Model List";
            // 
            // btnModelAdd
            // 
            this.btnModelAdd.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnModelAdd.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnModelAdd.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnModelAdd.Location = new System.Drawing.Point(31, 140);
            this.btnModelAdd.Name = "btnModelAdd";
            this.btnModelAdd.Size = new System.Drawing.Size(56, 23);
            this.btnModelAdd.TabIndex = 63;
            this.btnModelAdd.Text = "Add";
            this.btnModelAdd.UseVisualStyleBackColor = false;
            this.btnModelAdd.Click += new System.EventHandler(this.btnModelAdd_Click);
            // 
            // btnModelDelete
            // 
            this.btnModelDelete.BackColor = System.Drawing.Color.Fuchsia;
            this.btnModelDelete.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnModelDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnModelDelete.Location = new System.Drawing.Point(104, 140);
            this.btnModelDelete.Name = "btnModelDelete";
            this.btnModelDelete.Size = new System.Drawing.Size(56, 23);
            this.btnModelDelete.TabIndex = 62;
            this.btnModelDelete.Text = "Delete";
            this.btnModelDelete.UseVisualStyleBackColor = false;
            this.btnModelDelete.Click += new System.EventHandler(this.btnModelDelete_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnAdd.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnAdd.Location = new System.Drawing.Point(31, 308);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(64, 23);
            this.btnAdd.TabIndex = 52;
            this.btnAdd.Text = "添加=>";
            this.btnAdd.UseVisualStyleBackColor = false;
            // 
            // currlst
            // 
            this.currlst.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.currlst.FormattingEnabled = true;
            this.currlst.HorizontalScrollbar = true;
            this.currlst.ItemHeight = 12;
            this.currlst.Items.AddRange(new object[] {
            "Model1",
            "Model2",
            "Model3",
            "Model4",
            "...",
            "ModelN"});
            this.currlst.Location = new System.Drawing.Point(12, 18);
            this.currlst.Name = "currlst";
            this.currlst.ScrollAlwaysVisible = true;
            this.currlst.Size = new System.Drawing.Size(174, 112);
            this.currlst.TabIndex = 51;
            this.currlst.SelectedIndexChanged += new System.EventHandler(this.currlst_SelectedIndexChanged);
            // 
            // btnRemove
            // 
            this.btnRemove.BackColor = System.Drawing.Color.Fuchsia;
            this.btnRemove.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnRemove.Location = new System.Drawing.Point(119, 308);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(64, 23);
            this.btnRemove.TabIndex = 50;
            this.btnRemove.Text = "<=移除";
            this.btnRemove.UseVisualStyleBackColor = false;
            // 
            // grpPrmtr
            // 
            this.grpPrmtr.Controls.Add(this.txtPrmtrDescription);
            this.grpPrmtr.Controls.Add(this.label2);
            this.grpPrmtr.Controls.Add(this.label8);
            this.grpPrmtr.Controls.Add(this.label11);
            this.grpPrmtr.Controls.Add(this.label12);
            this.grpPrmtr.Controls.Add(this.label13);
            this.grpPrmtr.Controls.Add(this.label14);
            this.grpPrmtr.Controls.Add(this.label15);
            this.grpPrmtr.Controls.Add(this.label16);
            this.grpPrmtr.Controls.Add(this.label17);
            this.grpPrmtr.Controls.Add(this.label26);
            this.grpPrmtr.Controls.Add(this.label27);
            this.grpPrmtr.Controls.Add(this.cboDataRecord);
            this.grpPrmtr.Controls.Add(this.cboLogRecord);
            this.grpPrmtr.Controls.Add(this.cboFailbreak);
            this.grpPrmtr.Controls.Add(this.cboItemSpecific);
            this.grpPrmtr.Controls.Add(this.btnPrmtrOK);
            this.grpPrmtr.Controls.Add(this.cboSpecMax);
            this.grpPrmtr.Controls.Add(this.cboSpecMin);
            this.grpPrmtr.Controls.Add(this.cboItemValue);
            this.grpPrmtr.Controls.Add(this.cboItemType);
            this.grpPrmtr.Controls.Add(this.cboDirection);
            this.grpPrmtr.Controls.Add(this.cboItemName);
            this.grpPrmtr.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grpPrmtr.Location = new System.Drawing.Point(724, 9);
            this.grpPrmtr.Name = "grpPrmtr";
            this.grpPrmtr.Size = new System.Drawing.Size(237, 402);
            this.grpPrmtr.TabIndex = 59;
            this.grpPrmtr.TabStop = false;
            this.grpPrmtr.Text = "Current Parameter Item";
            // 
            // txtPrmtrDescription
            // 
            this.txtPrmtrDescription.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPrmtrDescription.Location = new System.Drawing.Point(8, 294);
            this.txtPrmtrDescription.Multiline = true;
            this.txtPrmtrDescription.Name = "txtPrmtrDescription";
            this.txtPrmtrDescription.Size = new System.Drawing.Size(223, 72);
            this.txtPrmtrDescription.TabIndex = 85;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(6, 279);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 12);
            this.label2.TabIndex = 84;
            this.label2.Text = "Description";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label8.Location = new System.Drawing.Point(6, 255);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 12);
            this.label8.TabIndex = 82;
            this.label8.Text = "SaveFMTdata?";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label11.Location = new System.Drawing.Point(5, 229);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(71, 12);
            this.label11.TabIndex = 81;
            this.label11.Text = "Fail break?";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label12.Location = new System.Drawing.Point(6, 203);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(65, 12);
            this.label12.TabIndex = 80;
            this.label12.Text = "Save Logs?";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label13.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label13.Location = new System.Drawing.Point(6, 177);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(77, 12);
            this.label13.TabIndex = 79;
            this.label13.Text = "Within spec?";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label14.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label14.Location = new System.Drawing.Point(6, 151);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(47, 12);
            this.label14.TabIndex = 78;
            this.label14.Text = "SpecMax";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label15.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label15.Location = new System.Drawing.Point(5, 125);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(47, 12);
            this.label15.TabIndex = 77;
            this.label15.Text = "SpecMin";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label16.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label16.Location = new System.Drawing.Point(6, 99);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(59, 12);
            this.label16.TabIndex = 76;
            this.label16.Text = "ItemValue";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label17.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label17.Location = new System.Drawing.Point(6, 73);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(41, 12);
            this.label17.TabIndex = 75;
            this.label17.Text = "in|out";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label26.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label26.Location = new System.Drawing.Point(6, 46);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(59, 12);
            this.label26.TabIndex = 74;
            this.label26.Text = "ValueType";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label27.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label27.Location = new System.Drawing.Point(6, 21);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(53, 12);
            this.label27.TabIndex = 73;
            this.label27.Text = "ItemName";
            // 
            // cboDataRecord
            // 
            this.cboDataRecord.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDataRecord.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboDataRecord.FormattingEnabled = true;
            this.cboDataRecord.Location = new System.Drawing.Point(85, 250);
            this.cboDataRecord.Name = "cboDataRecord";
            this.cboDataRecord.Size = new System.Drawing.Size(148, 20);
            this.cboDataRecord.TabIndex = 72;
            // 
            // cboLogRecord
            // 
            this.cboLogRecord.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLogRecord.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboLogRecord.FormattingEnabled = true;
            this.cboLogRecord.Location = new System.Drawing.Point(85, 198);
            this.cboLogRecord.Name = "cboLogRecord";
            this.cboLogRecord.Size = new System.Drawing.Size(148, 20);
            this.cboLogRecord.TabIndex = 72;
            // 
            // cboFailbreak
            // 
            this.cboFailbreak.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFailbreak.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboFailbreak.FormattingEnabled = true;
            this.cboFailbreak.Location = new System.Drawing.Point(85, 224);
            this.cboFailbreak.Name = "cboFailbreak";
            this.cboFailbreak.Size = new System.Drawing.Size(148, 20);
            this.cboFailbreak.TabIndex = 70;
            // 
            // cboItemSpecific
            // 
            this.cboItemSpecific.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboItemSpecific.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboItemSpecific.FormattingEnabled = true;
            this.cboItemSpecific.Location = new System.Drawing.Point(85, 172);
            this.cboItemSpecific.Name = "cboItemSpecific";
            this.cboItemSpecific.Size = new System.Drawing.Size(148, 20);
            this.cboItemSpecific.TabIndex = 70;
            // 
            // btnPrmtrOK
            // 
            this.btnPrmtrOK.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPrmtrOK.Location = new System.Drawing.Point(99, 372);
            this.btnPrmtrOK.Name = "btnPrmtrOK";
            this.btnPrmtrOK.Size = new System.Drawing.Size(50, 26);
            this.btnPrmtrOK.TabIndex = 68;
            this.btnPrmtrOK.Text = "Save";
            this.btnPrmtrOK.UseVisualStyleBackColor = true;
            this.btnPrmtrOK.Click += new System.EventHandler(this.btnPrmtrOK_Click);
            // 
            // cboSpecMax
            // 
            this.cboSpecMax.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboSpecMax.FormattingEnabled = true;
            this.cboSpecMax.Location = new System.Drawing.Point(84, 146);
            this.cboSpecMax.Name = "cboSpecMax";
            this.cboSpecMax.Size = new System.Drawing.Size(148, 20);
            this.cboSpecMax.TabIndex = 65;
            // 
            // cboSpecMin
            // 
            this.cboSpecMin.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboSpecMin.FormattingEnabled = true;
            this.cboSpecMin.Location = new System.Drawing.Point(85, 122);
            this.cboSpecMin.Name = "cboSpecMin";
            this.cboSpecMin.Size = new System.Drawing.Size(148, 20);
            this.cboSpecMin.TabIndex = 63;
            // 
            // cboItemValue
            // 
            this.cboItemValue.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboItemValue.FormattingEnabled = true;
            this.cboItemValue.Location = new System.Drawing.Point(85, 96);
            this.cboItemValue.Name = "cboItemValue";
            this.cboItemValue.Size = new System.Drawing.Size(148, 20);
            this.cboItemValue.TabIndex = 61;
            // 
            // cboItemType
            // 
            this.cboItemType.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboItemType.FormattingEnabled = true;
            this.cboItemType.Location = new System.Drawing.Point(85, 44);
            this.cboItemType.Name = "cboItemType";
            this.cboItemType.Size = new System.Drawing.Size(148, 20);
            this.cboItemType.TabIndex = 50;
            // 
            // cboDirection
            // 
            this.cboDirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDirection.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboDirection.FormattingEnabled = true;
            this.cboDirection.Location = new System.Drawing.Point(85, 70);
            this.cboDirection.Name = "cboDirection";
            this.cboDirection.Size = new System.Drawing.Size(148, 20);
            this.cboDirection.TabIndex = 54;
            // 
            // cboItemName
            // 
            this.cboItemName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboItemName.FormattingEnabled = true;
            this.cboItemName.Location = new System.Drawing.Point(85, 18);
            this.cboItemName.Name = "cboItemName";
            this.cboItemName.Size = new System.Drawing.Size(148, 20);
            this.cboItemName.TabIndex = 52;
            // 
            // grpEquipPrmtr
            // 
            this.grpEquipPrmtr.Controls.Add(this.btnPrmtrDelete);
            this.grpEquipPrmtr.Controls.Add(this.btnPrmtrAdd);
            this.grpEquipPrmtr.Controls.Add(this.txtSaveResult);
            this.grpEquipPrmtr.Controls.Add(this.dgvPrmtr);
            this.grpEquipPrmtr.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grpEquipPrmtr.Location = new System.Drawing.Point(349, 9);
            this.grpEquipPrmtr.Name = "grpEquipPrmtr";
            this.grpEquipPrmtr.Size = new System.Drawing.Size(369, 402);
            this.grpEquipPrmtr.TabIndex = 58;
            this.grpEquipPrmtr.TabStop = false;
            this.grpEquipPrmtr.Text = "ModelParameter";
            // 
            // btnPrmtrDelete
            // 
            this.btnPrmtrDelete.BackColor = System.Drawing.Color.Fuchsia;
            this.btnPrmtrDelete.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPrmtrDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnPrmtrDelete.Location = new System.Drawing.Point(219, 341);
            this.btnPrmtrDelete.Name = "btnPrmtrDelete";
            this.btnPrmtrDelete.Size = new System.Drawing.Size(59, 25);
            this.btnPrmtrDelete.TabIndex = 62;
            this.btnPrmtrDelete.Text = "Delete";
            this.btnPrmtrDelete.UseVisualStyleBackColor = false;
            this.btnPrmtrDelete.Click += new System.EventHandler(this.btnPrmtrDelete_Click);
            // 
            // btnPrmtrAdd
            // 
            this.btnPrmtrAdd.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnPrmtrAdd.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPrmtrAdd.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnPrmtrAdd.Location = new System.Drawing.Point(95, 341);
            this.btnPrmtrAdd.Name = "btnPrmtrAdd";
            this.btnPrmtrAdd.Size = new System.Drawing.Size(59, 25);
            this.btnPrmtrAdd.TabIndex = 61;
            this.btnPrmtrAdd.Text = "Add";
            this.btnPrmtrAdd.UseVisualStyleBackColor = false;
            this.btnPrmtrAdd.Click += new System.EventHandler(this.btnPrmtrAdd_Click);
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
            this.dgvPrmtr.Location = new System.Drawing.Point(6, 20);
            this.dgvPrmtr.Name = "dgvPrmtr";
            this.dgvPrmtr.ReadOnly = true;
            this.dgvPrmtr.RowHeadersVisible = false;
            this.dgvPrmtr.RowTemplate.Height = 23;
            this.dgvPrmtr.Size = new System.Drawing.Size(357, 318);
            this.dgvPrmtr.TabIndex = 3;
            this.dgvPrmtr.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvPrmtr_CellMouseClick);
            // 
            // grpItem
            // 
            this.grpItem.Controls.Add(this.txtDescription);
            this.grpItem.Controls.Add(this.lblEquipDescription);
            this.grpItem.Controls.Add(this.btnOK);
            this.grpItem.Controls.Add(this.cboModelName);
            this.grpItem.Controls.Add(this.label1);
            this.grpItem.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grpItem.Location = new System.Drawing.Point(150, 187);
            this.grpItem.Name = "grpItem";
            this.grpItem.Size = new System.Drawing.Size(193, 188);
            this.grpItem.TabIndex = 57;
            this.grpItem.TabStop = false;
            this.grpItem.Text = "Model Info";
            // 
            // txtDescription
            // 
            this.txtDescription.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtDescription.Location = new System.Drawing.Point(9, 92);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(178, 56);
            this.txtDescription.TabIndex = 52;
            // 
            // lblEquipDescription
            // 
            this.lblEquipDescription.AutoSize = true;
            this.lblEquipDescription.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblEquipDescription.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblEquipDescription.Location = new System.Drawing.Point(7, 77);
            this.lblEquipDescription.Name = "lblEquipDescription";
            this.lblEquipDescription.Size = new System.Drawing.Size(71, 12);
            this.lblEquipDescription.TabIndex = 51;
            this.lblEquipDescription.Text = "Description";
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.LightGreen;
            this.btnOK.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOK.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnOK.Location = new System.Drawing.Point(71, 154);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(49, 23);
            this.btnOK.TabIndex = 50;
            this.btnOK.Text = "Save";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // cboModelName
            // 
            this.cboModelName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboModelName.FormattingEnabled = true;
            this.cboModelName.Location = new System.Drawing.Point(6, 46);
            this.cboModelName.Name = "cboModelName";
            this.cboModelName.Size = new System.Drawing.Size(181, 20);
            this.cboModelName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(7, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "ModelName";
            // 
            // ModelInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(973, 430);
            this.Controls.Add(this.grpPrmtr);
            this.Controls.Add(this.grpItem);
            this.Controls.Add(this.grpModel);
            this.Controls.Add(this.grpAPP);
            this.Controls.Add(this.btnFinish);
            this.Controls.Add(this.grpEquipPrmtr);
            this.MaximumSize = new System.Drawing.Size(989, 468);
            this.Name = "ModelInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ModelInfo";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ModelInfo_FormClosing);
            this.Load += new System.EventHandler(this.ModelInfo_Load);
            this.grpAPP.ResumeLayout(false);
            this.grpModel.ResumeLayout(false);
            this.grpPrmtr.ResumeLayout(false);
            this.grpPrmtr.PerformLayout();
            this.grpEquipPrmtr.ResumeLayout(false);
            this.grpEquipPrmtr.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPrmtr)).EndInit();
            this.grpItem.ResumeLayout(false);
            this.grpItem.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnFinish;
        private System.Windows.Forms.GroupBox grpAPP;
        private System.Windows.Forms.ListBox globaApplistName;
        private System.Windows.Forms.GroupBox grpModel;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.ListBox currlst;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnModelAdd;
        private System.Windows.Forms.Button btnModelDelete;
        private System.Windows.Forms.GroupBox grpPrmtr;
        private System.Windows.Forms.Button btnPrmtrOK;
        private System.Windows.Forms.ComboBox cboSpecMax;
        private System.Windows.Forms.ComboBox cboSpecMin;
        private System.Windows.Forms.ComboBox cboItemValue;
        private System.Windows.Forms.ComboBox cboItemType;
        private System.Windows.Forms.ComboBox cboDirection;
        private System.Windows.Forms.ComboBox cboItemName;
        private System.Windows.Forms.GroupBox grpEquipPrmtr;
        private System.Windows.Forms.Button btnPrmtrDelete;
        private System.Windows.Forms.Button btnPrmtrAdd;
        private System.Windows.Forms.TextBox txtSaveResult;
        private System.Windows.Forms.DataGridView dgvPrmtr;
        private System.Windows.Forms.GroupBox grpItem;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.ComboBox cboModelName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboDataRecord;
        private System.Windows.Forms.ComboBox cboLogRecord;
        private System.Windows.Forms.ComboBox cboFailbreak;
        private System.Windows.Forms.ComboBox cboItemSpecific;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label lblEquipDescription;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPrmtrDescription;
    }
}
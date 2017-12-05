namespace Semi_Automatic_AdjustEye
{
    partial class Semi_Automatic_AdjustEye
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
            this.comboBoxPType = new System.Windows.Forms.ComboBox();
            this.comboBoxPN = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btSet = new System.Windows.Forms.Button();
            this.SelectItme = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.Config = new System.Windows.Forms.Button();
            this.gbItems = new System.Windows.Forms.GroupBox();
            this.chk_EA = new System.Windows.Forms.CheckBox();
            this.chK_LosD = new System.Windows.Forms.CheckBox();
            this.chk_Los = new System.Windows.Forms.CheckBox();
            this.chk_Vc = new System.Windows.Forms.CheckBox();
            this.chK_APD = new System.Windows.Forms.CheckBox();
            this.chk_BIAS = new System.Windows.Forms.CheckBox();
            this.chK_VLD = new System.Windows.Forms.CheckBox();
            this.chk_Mod = new System.Windows.Forms.CheckBox();
            this.chk_VG = new System.Windows.Forms.CheckBox();
            this.chk_Cross = new System.Windows.Forms.CheckBox();
            this.chk_Jitter = new System.Windows.Forms.CheckBox();
            this.chk_Tec = new System.Windows.Forms.CheckBox();
            this.chk_Mask = new System.Windows.Forms.CheckBox();
            this.btConfirm = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.gbDCAOffset = new System.Windows.Forms.GroupBox();
            this.btOffsetConfig = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tbOffset3 = new System.Windows.Forms.TextBox();
            this.tbOffset2 = new System.Windows.Forms.TextBox();
            this.tbOffset1 = new System.Windows.Forms.TextBox();
            this.tbOffset4 = new System.Windows.Forms.TextBox();
            this.btWriteToTable = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbTempHigh = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbTempAMB = new System.Windows.Forms.TextBox();
            this.cbTempLow = new System.Windows.Forms.CheckBox();
            this.label12 = new System.Windows.Forms.Label();
            this.tbTempHigh = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cbChannel = new System.Windows.Forms.ComboBox();
            this.cbTempAMB = new System.Windows.Forms.CheckBox();
            this.tbTempLow = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btRun = new System.Windows.Forms.Button();
            this.btAutoScale = new System.Windows.Forms.Button();
            this.cbPowerClose = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btAPCControl = new System.Windows.Forms.Button();
            this.cbXstreamUpDown = new System.Windows.Forms.ComboBox();
            this.gbDACBar = new System.Windows.Forms.GroupBox();
            this.DataGridView = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.cbFitChannel = new System.Windows.Forms.ComboBox();
            this.DgvData = new System.Windows.Forms.DataGridView();
            this.btFitting = new System.Windows.Forms.Button();
            this.SelectItme.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.gbItems.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.gbDCAOffset.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.DataGridView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvData)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBoxPType
            // 
            this.comboBoxPType.FormattingEnabled = true;
            this.comboBoxPType.Location = new System.Drawing.Point(41, 52);
            this.comboBoxPType.Name = "comboBoxPType";
            this.comboBoxPType.Size = new System.Drawing.Size(162, 20);
            this.comboBoxPType.TabIndex = 0;
            this.comboBoxPType.SelectedIndexChanged += new System.EventHandler(this.comboBoxPType_SelectedIndexChanged);
            // 
            // comboBoxPN
            // 
            this.comboBoxPN.FormattingEnabled = true;
            this.comboBoxPN.Location = new System.Drawing.Point(235, 52);
            this.comboBoxPN.Name = "comboBoxPN";
            this.comboBoxPN.Size = new System.Drawing.Size(162, 20);
            this.comboBoxPN.TabIndex = 1;
            this.comboBoxPN.SelectedIndexChanged += new System.EventHandler(this.comboBoxPN_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "ProductionType";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(233, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "ProductionName";
            // 
            // btSet
            // 
            this.btSet.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btSet.Location = new System.Drawing.Point(157, 48);
            this.btSet.Name = "btSet";
            this.btSet.Size = new System.Drawing.Size(70, 27);
            this.btSet.TabIndex = 4;
            this.btSet.Text = "Set";
            this.btSet.UseVisualStyleBackColor = true;
            this.btSet.Click += new System.EventHandler(this.btSet_Click);
            // 
            // SelectItme
            // 
            this.SelectItme.Controls.Add(this.tabPage1);
            this.SelectItme.Controls.Add(this.tabPage2);
            this.SelectItme.Controls.Add(this.DataGridView);
            this.SelectItme.Location = new System.Drawing.Point(41, 78);
            this.SelectItme.Name = "SelectItme";
            this.SelectItme.SelectedIndex = 0;
            this.SelectItme.Size = new System.Drawing.Size(717, 392);
            this.SelectItme.TabIndex = 5;
            this.SelectItme.Selected += new System.Windows.Forms.TabControlEventHandler(this.SelectItme_Selected);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.Config);
            this.tabPage1.Controls.Add(this.gbItems);
            this.tabPage1.Controls.Add(this.btConfirm);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(709, 366);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Items";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // Config
            // 
            this.Config.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Config.Location = new System.Drawing.Point(340, 27);
            this.Config.Name = "Config";
            this.Config.Size = new System.Drawing.Size(99, 40);
            this.Config.TabIndex = 6;
            this.Config.Text = "EquipConfig";
            this.Config.UseVisualStyleBackColor = true;
            this.Config.Click += new System.EventHandler(this.Config_Click);
            // 
            // gbItems
            // 
            this.gbItems.Controls.Add(this.chk_EA);
            this.gbItems.Controls.Add(this.chK_LosD);
            this.gbItems.Controls.Add(this.chk_Los);
            this.gbItems.Controls.Add(this.chk_Vc);
            this.gbItems.Controls.Add(this.chK_APD);
            this.gbItems.Controls.Add(this.chk_BIAS);
            this.gbItems.Controls.Add(this.chK_VLD);
            this.gbItems.Controls.Add(this.chk_Mod);
            this.gbItems.Controls.Add(this.chk_VG);
            this.gbItems.Controls.Add(this.chk_Cross);
            this.gbItems.Controls.Add(this.chk_Jitter);
            this.gbItems.Controls.Add(this.chk_Tec);
            this.gbItems.Controls.Add(this.chk_Mask);
            this.gbItems.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gbItems.Location = new System.Drawing.Point(38, 18);
            this.gbItems.Name = "gbItems";
            this.gbItems.Size = new System.Drawing.Size(215, 253);
            this.gbItems.TabIndex = 10;
            this.gbItems.TabStop = false;
            this.gbItems.Text = "AjustItem";
            // 
            // chk_EA
            // 
            this.chk_EA.AutoSize = true;
            this.chk_EA.Enabled = false;
            this.chk_EA.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chk_EA.Location = new System.Drawing.Point(130, 127);
            this.chk_EA.Name = "chk_EA";
            this.chk_EA.Size = new System.Drawing.Size(43, 20);
            this.chk_EA.TabIndex = 13;
            this.chk_EA.Text = "EA";
            this.chk_EA.UseVisualStyleBackColor = true;
            // 
            // chK_LosD
            // 
            this.chK_LosD.AutoSize = true;
            this.chK_LosD.Enabled = false;
            this.chK_LosD.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chK_LosD.Location = new System.Drawing.Point(23, 205);
            this.chK_LosD.Name = "chK_LosD";
            this.chK_LosD.Size = new System.Drawing.Size(59, 20);
            this.chK_LosD.TabIndex = 12;
            this.chK_LosD.Text = "LosD";
            this.chK_LosD.UseVisualStyleBackColor = true;
            // 
            // chk_Los
            // 
            this.chk_Los.AutoSize = true;
            this.chk_Los.Enabled = false;
            this.chk_Los.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chk_Los.Location = new System.Drawing.Point(23, 179);
            this.chk_Los.Name = "chk_Los";
            this.chk_Los.Size = new System.Drawing.Size(59, 20);
            this.chk_Los.TabIndex = 11;
            this.chk_Los.Text = "LosA";
            this.chk_Los.UseVisualStyleBackColor = true;
            // 
            // chk_Vc
            // 
            this.chk_Vc.AutoSize = true;
            this.chk_Vc.Enabled = false;
            this.chk_Vc.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chk_Vc.Location = new System.Drawing.Point(130, 48);
            this.chk_Vc.Name = "chk_Vc";
            this.chk_Vc.Size = new System.Drawing.Size(43, 20);
            this.chk_Vc.TabIndex = 6;
            this.chk_Vc.Text = "VC";
            this.chk_Vc.UseVisualStyleBackColor = true;
            // 
            // chK_APD
            // 
            this.chK_APD.AutoSize = true;
            this.chK_APD.Enabled = false;
            this.chK_APD.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chK_APD.Location = new System.Drawing.Point(23, 153);
            this.chK_APD.Name = "chK_APD";
            this.chK_APD.Size = new System.Drawing.Size(51, 20);
            this.chK_APD.TabIndex = 9;
            this.chK_APD.Text = "APD";
            this.chK_APD.UseVisualStyleBackColor = true;
            // 
            // chk_BIAS
            // 
            this.chk_BIAS.AutoSize = true;
            this.chk_BIAS.Enabled = false;
            this.chk_BIAS.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chk_BIAS.Location = new System.Drawing.Point(23, 23);
            this.chk_BIAS.Name = "chk_BIAS";
            this.chk_BIAS.Size = new System.Drawing.Size(59, 20);
            this.chk_BIAS.TabIndex = 0;
            this.chk_BIAS.Text = "BIAS";
            this.chk_BIAS.UseVisualStyleBackColor = true;
            // 
            // chK_VLD
            // 
            this.chK_VLD.AutoSize = true;
            this.chK_VLD.Enabled = false;
            this.chK_VLD.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chK_VLD.Location = new System.Drawing.Point(130, 101);
            this.chK_VLD.Name = "chK_VLD";
            this.chK_VLD.Size = new System.Drawing.Size(51, 20);
            this.chK_VLD.TabIndex = 8;
            this.chK_VLD.Text = "VLD";
            this.chK_VLD.UseVisualStyleBackColor = true;
            // 
            // chk_Mod
            // 
            this.chk_Mod.AutoSize = true;
            this.chk_Mod.Enabled = false;
            this.chk_Mod.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chk_Mod.Location = new System.Drawing.Point(23, 48);
            this.chk_Mod.Name = "chk_Mod";
            this.chk_Mod.Size = new System.Drawing.Size(51, 20);
            this.chk_Mod.TabIndex = 1;
            this.chk_Mod.Text = "MOD";
            this.chk_Mod.UseVisualStyleBackColor = true;
            // 
            // chk_VG
            // 
            this.chk_VG.AutoSize = true;
            this.chk_VG.Enabled = false;
            this.chk_VG.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chk_VG.Location = new System.Drawing.Point(130, 75);
            this.chk_VG.Name = "chk_VG";
            this.chk_VG.Size = new System.Drawing.Size(43, 20);
            this.chk_VG.TabIndex = 7;
            this.chk_VG.Text = "VG";
            this.chk_VG.UseVisualStyleBackColor = true;
            // 
            // chk_Cross
            // 
            this.chk_Cross.AutoSize = true;
            this.chk_Cross.Enabled = false;
            this.chk_Cross.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chk_Cross.Location = new System.Drawing.Point(23, 127);
            this.chk_Cross.Name = "chk_Cross";
            this.chk_Cross.Size = new System.Drawing.Size(67, 20);
            this.chk_Cross.TabIndex = 2;
            this.chk_Cross.Text = "Cross";
            this.chk_Cross.UseVisualStyleBackColor = true;
            // 
            // chk_Jitter
            // 
            this.chk_Jitter.AutoSize = true;
            this.chk_Jitter.Enabled = false;
            this.chk_Jitter.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chk_Jitter.Location = new System.Drawing.Point(23, 101);
            this.chk_Jitter.Name = "chk_Jitter";
            this.chk_Jitter.Size = new System.Drawing.Size(75, 20);
            this.chk_Jitter.TabIndex = 3;
            this.chk_Jitter.Text = "Jitter";
            this.chk_Jitter.UseVisualStyleBackColor = true;
            // 
            // chk_Tec
            // 
            this.chk_Tec.AutoSize = true;
            this.chk_Tec.Enabled = false;
            this.chk_Tec.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chk_Tec.Location = new System.Drawing.Point(130, 23);
            this.chk_Tec.Name = "chk_Tec";
            this.chk_Tec.Size = new System.Drawing.Size(51, 20);
            this.chk_Tec.TabIndex = 5;
            this.chk_Tec.Text = "TEC";
            this.chk_Tec.UseVisualStyleBackColor = true;
            // 
            // chk_Mask
            // 
            this.chk_Mask.AutoSize = true;
            this.chk_Mask.Enabled = false;
            this.chk_Mask.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chk_Mask.Location = new System.Drawing.Point(23, 75);
            this.chk_Mask.Name = "chk_Mask";
            this.chk_Mask.Size = new System.Drawing.Size(59, 20);
            this.chk_Mask.TabIndex = 4;
            this.chk_Mask.Text = "Mask";
            this.chk_Mask.UseVisualStyleBackColor = true;
            // 
            // btConfirm
            // 
            this.btConfirm.Location = new System.Drawing.Point(99, 286);
            this.btConfirm.Name = "btConfirm";
            this.btConfirm.Size = new System.Drawing.Size(59, 23);
            this.btConfirm.TabIndex = 10;
            this.btConfirm.Text = "确认";
            this.btConfirm.UseVisualStyleBackColor = true;
            this.btConfirm.Click += new System.EventHandler(this.btConfirm_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.gbDCAOffset);
            this.tabPage2.Controls.Add(this.btWriteToTable);
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Controls.Add(this.cbPowerClose);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.btAPCControl);
            this.tabPage2.Controls.Add(this.cbXstreamUpDown);
            this.tabPage2.Controls.Add(this.gbDACBar);
            this.tabPage2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabPage2.ForeColor = System.Drawing.Color.Black;
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(709, 366);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Adjust";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // gbDCAOffset
            // 
            this.gbDCAOffset.Controls.Add(this.btOffsetConfig);
            this.gbDCAOffset.Controls.Add(this.label11);
            this.gbDCAOffset.Controls.Add(this.label9);
            this.gbDCAOffset.Controls.Add(this.label8);
            this.gbDCAOffset.Controls.Add(this.label5);
            this.gbDCAOffset.Controls.Add(this.tbOffset3);
            this.gbDCAOffset.Controls.Add(this.tbOffset2);
            this.gbDCAOffset.Controls.Add(this.tbOffset1);
            this.gbDCAOffset.Controls.Add(this.tbOffset4);
            this.gbDCAOffset.Location = new System.Drawing.Point(12, 11);
            this.gbDCAOffset.Name = "gbDCAOffset";
            this.gbDCAOffset.Size = new System.Drawing.Size(196, 104);
            this.gbDCAOffset.TabIndex = 15;
            this.gbDCAOffset.TabStop = false;
            this.gbDCAOffset.Text = "DCA Offset";
            // 
            // btOffsetConfig
            // 
            this.btOffsetConfig.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btOffsetConfig.Location = new System.Drawing.Point(50, 73);
            this.btOffsetConfig.Name = "btOffsetConfig";
            this.btOffsetConfig.Size = new System.Drawing.Size(109, 26);
            this.btOffsetConfig.TabIndex = 5;
            this.btOffsetConfig.Text = "OffsetConfig";
            this.btOffsetConfig.UseVisualStyleBackColor = true;
            this.btOffsetConfig.Click += new System.EventHandler(this.btOffsetConfig_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(100, 23);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(23, 12);
            this.label11.TabIndex = 16;
            this.label11.Text = "CH2";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(11, 51);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(23, 12);
            this.label9.TabIndex = 15;
            this.label9.Text = "CH3";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(100, 51);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(23, 12);
            this.label8.TabIndex = 14;
            this.label8.Text = "CH4";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(23, 12);
            this.label5.TabIndex = 13;
            this.label5.Text = "CH1";
            // 
            // tbOffset3
            // 
            this.tbOffset3.Location = new System.Drawing.Point(36, 47);
            this.tbOffset3.Name = "tbOffset3";
            this.tbOffset3.Size = new System.Drawing.Size(50, 21);
            this.tbOffset3.TabIndex = 12;
            this.tbOffset3.Text = "0";
            // 
            // tbOffset2
            // 
            this.tbOffset2.Location = new System.Drawing.Point(126, 20);
            this.tbOffset2.Name = "tbOffset2";
            this.tbOffset2.Size = new System.Drawing.Size(54, 21);
            this.tbOffset2.TabIndex = 11;
            this.tbOffset2.Text = "0";
            // 
            // tbOffset1
            // 
            this.tbOffset1.Location = new System.Drawing.Point(36, 20);
            this.tbOffset1.Name = "tbOffset1";
            this.tbOffset1.Size = new System.Drawing.Size(50, 21);
            this.tbOffset1.TabIndex = 10;
            this.tbOffset1.Text = "0";
            // 
            // tbOffset4
            // 
            this.tbOffset4.Location = new System.Drawing.Point(126, 47);
            this.tbOffset4.Name = "tbOffset4";
            this.tbOffset4.Size = new System.Drawing.Size(54, 21);
            this.tbOffset4.TabIndex = 9;
            this.tbOffset4.Text = "0";
            // 
            // btWriteToTable
            // 
            this.btWriteToTable.Location = new System.Drawing.Point(591, 337);
            this.btWriteToTable.Name = "btWriteToTable";
            this.btWriteToTable.Size = new System.Drawing.Size(82, 23);
            this.btWriteToTable.TabIndex = 11;
            this.btWriteToTable.Text = "WriteToDgv";
            this.btWriteToTable.UseVisualStyleBackColor = true;
            this.btWriteToTable.Click += new System.EventHandler(this.btWriteToTable_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbTempHigh);
            this.groupBox1.Controls.Add(this.btSet);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.tbTempAMB);
            this.groupBox1.Controls.Add(this.cbTempLow);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.tbTempHigh);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.cbChannel);
            this.groupBox1.Controls.Add(this.cbTempAMB);
            this.groupBox1.Controls.Add(this.tbTempLow);
            this.groupBox1.Location = new System.Drawing.Point(229, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(256, 87);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // cbTempHigh
            // 
            this.cbTempHigh.AutoSize = true;
            this.cbTempHigh.Location = new System.Drawing.Point(10, 60);
            this.cbTempHigh.Name = "cbTempHigh";
            this.cbTempHigh.Size = new System.Drawing.Size(48, 16);
            this.cbTempHigh.TabIndex = 25;
            this.cbTempHigh.Text = "High";
            this.cbTempHigh.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(99, 17);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "℃";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(138, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "Channel";
            // 
            // tbTempAMB
            // 
            this.tbTempAMB.Location = new System.Drawing.Point(60, 34);
            this.tbTempAMB.Name = "tbTempAMB";
            this.tbTempAMB.Size = new System.Drawing.Size(39, 21);
            this.tbTempAMB.TabIndex = 15;
            this.tbTempAMB.Text = "20";
            // 
            // cbTempLow
            // 
            this.cbTempLow.AutoSize = true;
            this.cbTempLow.Location = new System.Drawing.Point(10, 14);
            this.cbTempLow.Name = "cbTempLow";
            this.cbTempLow.Size = new System.Drawing.Size(42, 16);
            this.cbTempLow.TabIndex = 1;
            this.cbTempLow.Text = "Low";
            this.cbTempLow.UseVisualStyleBackColor = true;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(99, 63);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(17, 12);
            this.label12.TabIndex = 21;
            this.label12.Text = "℃";
            // 
            // tbTempHigh
            // 
            this.tbTempHigh.Location = new System.Drawing.Point(60, 58);
            this.tbTempHigh.Name = "tbTempHigh";
            this.tbTempHigh.Size = new System.Drawing.Size(39, 21);
            this.tbTempHigh.TabIndex = 16;
            this.tbTempHigh.Text = "70";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(99, 39);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(17, 12);
            this.label10.TabIndex = 19;
            this.label10.Text = "℃";
            // 
            // cbChannel
            // 
            this.cbChannel.FormattingEnabled = true;
            this.cbChannel.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4"});
            this.cbChannel.Location = new System.Drawing.Point(185, 13);
            this.cbChannel.Name = "cbChannel";
            this.cbChannel.Size = new System.Drawing.Size(59, 20);
            this.cbChannel.TabIndex = 6;
            this.cbChannel.Text = "1";
            // 
            // cbTempAMB
            // 
            this.cbTempAMB.AutoSize = true;
            this.cbTempAMB.Location = new System.Drawing.Point(10, 36);
            this.cbTempAMB.Name = "cbTempAMB";
            this.cbTempAMB.Size = new System.Drawing.Size(42, 16);
            this.cbTempAMB.TabIndex = 24;
            this.cbTempAMB.Text = "AMB";
            this.cbTempAMB.UseVisualStyleBackColor = true;
            // 
            // tbTempLow
            // 
            this.tbTempLow.Location = new System.Drawing.Point(60, 12);
            this.tbTempLow.Name = "tbTempLow";
            this.tbTempLow.Size = new System.Drawing.Size(39, 21);
            this.tbTempLow.TabIndex = 8;
            this.tbTempLow.Text = "0";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btRun);
            this.groupBox2.Controls.Add(this.btAutoScale);
            this.groupBox2.Location = new System.Drawing.Point(502, 15);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(161, 47);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "DCA";
            // 
            // btRun
            // 
            this.btRun.Location = new System.Drawing.Point(7, 15);
            this.btRun.Name = "btRun";
            this.btRun.Size = new System.Drawing.Size(67, 23);
            this.btRun.TabIndex = 13;
            this.btRun.Text = "Run";
            this.btRun.UseVisualStyleBackColor = true;
            this.btRun.Click += new System.EventHandler(this.btRun_Click);
            // 
            // btAutoScale
            // 
            this.btAutoScale.Location = new System.Drawing.Point(84, 15);
            this.btAutoScale.Name = "btAutoScale";
            this.btAutoScale.Size = new System.Drawing.Size(67, 23);
            this.btAutoScale.TabIndex = 12;
            this.btAutoScale.Text = "AutoScale";
            this.btAutoScale.UseVisualStyleBackColor = true;
            this.btAutoScale.Click += new System.EventHandler(this.btAutoScale_Click);
            // 
            // cbPowerClose
            // 
            this.cbPowerClose.AutoSize = true;
            this.cbPowerClose.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbPowerClose.Location = new System.Drawing.Point(567, 76);
            this.cbPowerClose.Name = "cbPowerClose";
            this.cbPowerClose.Size = new System.Drawing.Size(96, 18);
            this.cbPowerClose.TabIndex = 11;
            this.cbPowerClose.Text = "PowerClose";
            this.cbPowerClose.UseVisualStyleBackColor = true;
            this.cbPowerClose.CheckedChanged += new System.EventHandler(this.cbPowerClose_CheckedChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(228, 11);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(95, 12);
            this.label7.TabIndex = 14;
            this.label7.Text = "Xstream Up/Down";
            // 
            // btAPCControl
            // 
            this.btAPCControl.Location = new System.Drawing.Point(504, 74);
            this.btAPCControl.Name = "btAPCControl";
            this.btAPCControl.Size = new System.Drawing.Size(48, 23);
            this.btAPCControl.TabIndex = 6;
            this.btAPCControl.Text = "APC";
            this.btAPCControl.UseVisualStyleBackColor = true;
            this.btAPCControl.Click += new System.EventHandler(this.btAPCControl_Click);
            // 
            // cbXstreamUpDown
            // 
            this.cbXstreamUpDown.FormattingEnabled = true;
            this.cbXstreamUpDown.Items.AddRange(new object[] {
            "UP",
            "DOWN"});
            this.cbXstreamUpDown.Location = new System.Drawing.Point(324, 8);
            this.cbXstreamUpDown.Name = "cbXstreamUpDown";
            this.cbXstreamUpDown.Size = new System.Drawing.Size(59, 20);
            this.cbXstreamUpDown.TabIndex = 13;
            this.cbXstreamUpDown.SelectedIndexChanged += new System.EventHandler(this.cbXstreamUpDown_SelectedIndexChanged);
            // 
            // gbDACBar
            // 
            this.gbDACBar.Location = new System.Drawing.Point(12, 121);
            this.gbDACBar.Name = "gbDACBar";
            this.gbDACBar.Size = new System.Drawing.Size(661, 210);
            this.gbDACBar.TabIndex = 5;
            this.gbDACBar.TabStop = false;
            this.gbDACBar.Text = "DAC";
            // 
            // DataGridView
            // 
            this.DataGridView.Controls.Add(this.label3);
            this.DataGridView.Controls.Add(this.cbFitChannel);
            this.DataGridView.Controls.Add(this.DgvData);
            this.DataGridView.Controls.Add(this.btFitting);
            this.DataGridView.Location = new System.Drawing.Point(4, 22);
            this.DataGridView.Name = "DataGridView";
            this.DataGridView.Padding = new System.Windows.Forms.Padding(3);
            this.DataGridView.Size = new System.Drawing.Size(709, 366);
            this.DataGridView.TabIndex = 2;
            this.DataGridView.Text = "DataGridView";
            this.DataGridView.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(592, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "Channel";
            // 
            // cbFitChannel
            // 
            this.cbFitChannel.FormattingEnabled = true;
            this.cbFitChannel.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4"});
            this.cbFitChannel.Location = new System.Drawing.Point(640, 16);
            this.cbFitChannel.Name = "cbFitChannel";
            this.cbFitChannel.Size = new System.Drawing.Size(58, 20);
            this.cbFitChannel.TabIndex = 8;
            // 
            // DgvData
            // 
            this.DgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvData.Location = new System.Drawing.Point(18, 16);
            this.DgvData.Name = "DgvData";
            this.DgvData.RowTemplate.Height = 23;
            this.DgvData.Size = new System.Drawing.Size(568, 317);
            this.DgvData.TabIndex = 1;
            // 
            // btFitting
            // 
            this.btFitting.Location = new System.Drawing.Point(613, 53);
            this.btFitting.Name = "btFitting";
            this.btFitting.Size = new System.Drawing.Size(75, 23);
            this.btFitting.TabIndex = 0;
            this.btFitting.Text = "Fitting";
            this.btFitting.UseVisualStyleBackColor = true;
            this.btFitting.Click += new System.EventHandler(this.btFitting_Click);
            // 
            // Semi_Automatic_AdjustEye
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(817, 492);
            this.Controls.Add(this.SelectItme);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxPN);
            this.Controls.Add(this.comboBoxPType);
            this.Name = "Semi_Automatic_AdjustEye";
            this.Text = "Semi-Automatic-AdjustEye";
            this.Load += new System.EventHandler(this.Semi_Automatic_AdjustEye_Load);
            this.SelectItme.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.gbItems.ResumeLayout(false);
            this.gbItems.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.gbDCAOffset.ResumeLayout(false);
            this.gbDCAOffset.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.DataGridView.ResumeLayout(false);
            this.DataGridView.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxPType;
        private System.Windows.Forms.ComboBox comboBoxPN;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btSet;
        private System.Windows.Forms.TabControl SelectItme;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.CheckBox chK_VLD;
        private System.Windows.Forms.CheckBox chk_VG;
        private System.Windows.Forms.CheckBox chk_Vc;
        private System.Windows.Forms.CheckBox chk_Tec;
        private System.Windows.Forms.CheckBox chk_Mask;
        private System.Windows.Forms.CheckBox chk_Jitter;
        private System.Windows.Forms.CheckBox chk_Cross;
        private System.Windows.Forms.CheckBox chk_Mod;
        private System.Windows.Forms.CheckBox chk_BIAS;
        private System.Windows.Forms.CheckBox chK_APD;
        private System.Windows.Forms.GroupBox gbItems;
        private System.Windows.Forms.Button btConfirm;
        private System.Windows.Forms.CheckBox chK_LosD;
        private System.Windows.Forms.CheckBox chk_Los;
        private System.Windows.Forms.CheckBox chk_EA;
        private System.Windows.Forms.CheckBox cbPowerClose;
        private System.Windows.Forms.GroupBox gbDACBar;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbChannel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbTempLow;
        private System.Windows.Forms.TabPage DataGridView;
        private System.Windows.Forms.DataGridView DgvData;
        private System.Windows.Forms.Button btFitting;
        private System.Windows.Forms.Button btWriteToTable;
        private System.Windows.Forms.Button btAPCControl;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbFitChannel;
        private System.Windows.Forms.Button btAutoScale;
        private System.Windows.Forms.Button Config;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbXstreamUpDown;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tbTempHigh;
        private System.Windows.Forms.TextBox tbTempAMB;
        private System.Windows.Forms.CheckBox cbTempHigh;
        private System.Windows.Forms.CheckBox cbTempAMB;
        private System.Windows.Forms.CheckBox cbTempLow;
        private System.Windows.Forms.Button btRun;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox gbDCAOffset;
        private System.Windows.Forms.TextBox tbOffset3;
        private System.Windows.Forms.TextBox tbOffset2;
        private System.Windows.Forms.TextBox tbOffset1;
        private System.Windows.Forms.TextBox tbOffset4;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btOffsetConfig;
    }
}


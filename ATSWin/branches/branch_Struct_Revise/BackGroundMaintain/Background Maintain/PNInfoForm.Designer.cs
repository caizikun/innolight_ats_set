namespace Maintain
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
            this.lblCurv = new System.Windows.Forms.Label();
            this.cboNickName = new System.Windows.Forms.ComboBox();
            this.lblNickName = new System.Windows.Forms.Label();
            this.cboPublish_PN = new System.Windows.Forms.ComboBox();
            this.lblPublish_PN = new System.Windows.Forms.Label();
            this.cboMaxRate = new System.Windows.Forms.ComboBox();
            this.lblMaxRate = new System.Windows.Forms.Label();
            this.cboBER = new System.Windows.Forms.ComboBox();
            this.lblBER = new System.Windows.Forms.Label();
            this.cboAPC_Type = new System.Windows.Forms.ComboBox();
            this.lblAPC_Type = new System.Windows.Forms.Label();
            this.cboCouple_Type = new System.Windows.Forms.ComboBox();
            this.lblCouple_Type = new System.Windows.Forms.Label();
            this.cboTEC_Present = new System.Windows.Forms.ComboBox();
            this.lblTEC_Present = new System.Windows.Forms.Label();
            this.cboAPCStyle = new System.Windows.Forms.ComboBox();
            this.lblAPCStyle = new System.Windows.Forms.Label();
            this.cboOldDriver = new System.Windows.Forms.ComboBox();
            this.lblOldDriver = new System.Windows.Forms.Label();
            this.cboIgnore = new System.Windows.Forms.ComboBox();
            this.lblIgnore = new System.Windows.Forms.Label();
            this.cboTsensors = new System.Windows.Forms.ComboBox();
            this.cboVoltages = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cboMGroup = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtModFml = new System.Windows.Forms.TextBox();
            this.lblMod = new System.Windows.Forms.Label();
            this.txtBiasFml = new System.Windows.Forms.TextBox();
            this.lblIbias = new System.Windows.Forms.Label();
            this.txtItemName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cboChannels = new System.Windows.Forms.ComboBox();
            this.txtPN = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.currlst = new System.Windows.Forms.ListBox();
            this.btnPreviousPage = new System.Windows.Forms.Button();
            this.btnInitInfo = new System.Windows.Forms.Button();
            this.btnSpec = new System.Windows.Forms.Button();
            this.cboRxOverLoadDBm = new System.Windows.Forms.ComboBox();
            this.lblRxOverLoadDBm = new System.Windows.Forms.Label();
            this.cboUsingTempAD = new System.Windows.Forms.ComboBox();
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
            this.grpType.Controls.Add(this.cboUsingTempAD);
            this.grpType.Controls.Add(this.cboRxOverLoadDBm);
            this.grpType.Controls.Add(this.lblRxOverLoadDBm);
            this.grpType.Controls.Add(this.lblCurv);
            this.grpType.Controls.Add(this.cboNickName);
            this.grpType.Controls.Add(this.lblNickName);
            this.grpType.Controls.Add(this.cboPublish_PN);
            this.grpType.Controls.Add(this.lblPublish_PN);
            this.grpType.Controls.Add(this.cboMaxRate);
            this.grpType.Controls.Add(this.lblMaxRate);
            this.grpType.Controls.Add(this.cboBER);
            this.grpType.Controls.Add(this.lblBER);
            this.grpType.Controls.Add(this.cboAPC_Type);
            this.grpType.Controls.Add(this.lblAPC_Type);
            this.grpType.Controls.Add(this.cboCouple_Type);
            this.grpType.Controls.Add(this.lblCouple_Type);
            this.grpType.Controls.Add(this.cboTEC_Present);
            this.grpType.Controls.Add(this.lblTEC_Present);
            this.grpType.Controls.Add(this.cboAPCStyle);
            this.grpType.Controls.Add(this.lblAPCStyle);
            this.grpType.Controls.Add(this.cboOldDriver);
            this.grpType.Controls.Add(this.lblOldDriver);
            this.grpType.Controls.Add(this.cboIgnore);
            this.grpType.Controls.Add(this.lblIgnore);
            this.grpType.Controls.Add(this.cboTsensors);
            this.grpType.Controls.Add(this.cboVoltages);
            this.grpType.Controls.Add(this.label9);
            this.grpType.Controls.Add(this.label4);
            this.grpType.Controls.Add(this.cboMGroup);
            this.grpType.Controls.Add(this.label7);
            this.grpType.Controls.Add(this.txtModFml);
            this.grpType.Controls.Add(this.lblMod);
            this.grpType.Controls.Add(this.txtBiasFml);
            this.grpType.Controls.Add(this.lblIbias);
            this.grpType.Controls.Add(this.txtItemName);
            this.grpType.Controls.Add(this.label2);
            this.grpType.Controls.Add(this.cboChannels);
            this.grpType.Controls.Add(this.txtPN);
            this.grpType.Controls.Add(this.btnOK);
            this.grpType.Controls.Add(this.label3);
            this.grpType.Controls.Add(this.label1);
            this.grpType.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grpType.Location = new System.Drawing.Point(219, 2);
            this.grpType.Name = "grpType";
            this.grpType.Size = new System.Drawing.Size(275, 551);
            this.grpType.TabIndex = 25;
            this.grpType.TabStop = false;
            this.grpType.Text = "PNInfo";
            // 
            // lblCurv
            // 
            this.lblCurv.AutoSize = true;
            this.lblCurv.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCurv.Location = new System.Drawing.Point(8, 410);
            this.lblCurv.Name = "lblCurv";
            this.lblCurv.Size = new System.Drawing.Size(71, 12);
            this.lblCurv.TabIndex = 61;
            this.lblCurv.Text = "UsingTempAD";
            // 
            // cboNickName
            // 
            this.cboNickName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboNickName.FormattingEnabled = true;
            this.cboNickName.Location = new System.Drawing.Point(91, 326);
            this.cboNickName.Name = "cboNickName";
            this.cboNickName.Size = new System.Drawing.Size(180, 20);
            this.cboNickName.TabIndex = 60;
            // 
            // lblNickName
            // 
            this.lblNickName.AutoSize = true;
            this.lblNickName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblNickName.Location = new System.Drawing.Point(7, 329);
            this.lblNickName.Name = "lblNickName";
            this.lblNickName.Size = new System.Drawing.Size(53, 12);
            this.lblNickName.TabIndex = 59;
            this.lblNickName.Text = "NickName";
            // 
            // cboPublish_PN
            // 
            this.cboPublish_PN.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboPublish_PN.FormattingEnabled = true;
            this.cboPublish_PN.Location = new System.Drawing.Point(91, 303);
            this.cboPublish_PN.Name = "cboPublish_PN";
            this.cboPublish_PN.Size = new System.Drawing.Size(180, 20);
            this.cboPublish_PN.TabIndex = 60;
            // 
            // lblPublish_PN
            // 
            this.lblPublish_PN.AutoSize = true;
            this.lblPublish_PN.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPublish_PN.Location = new System.Drawing.Point(8, 306);
            this.lblPublish_PN.Name = "lblPublish_PN";
            this.lblPublish_PN.Size = new System.Drawing.Size(65, 12);
            this.lblPublish_PN.TabIndex = 59;
            this.lblPublish_PN.Text = "Publish_PN";
            // 
            // cboMaxRate
            // 
            this.cboMaxRate.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboMaxRate.FormattingEnabled = true;
            this.cboMaxRate.Location = new System.Drawing.Point(91, 281);
            this.cboMaxRate.Name = "cboMaxRate";
            this.cboMaxRate.Size = new System.Drawing.Size(180, 20);
            this.cboMaxRate.TabIndex = 60;
            // 
            // lblMaxRate
            // 
            this.lblMaxRate.AutoSize = true;
            this.lblMaxRate.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblMaxRate.Location = new System.Drawing.Point(8, 284);
            this.lblMaxRate.Name = "lblMaxRate";
            this.lblMaxRate.Size = new System.Drawing.Size(47, 12);
            this.lblMaxRate.TabIndex = 59;
            this.lblMaxRate.Text = "MaxRate";
            // 
            // cboBER
            // 
            this.cboBER.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBER.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboBER.FormattingEnabled = true;
            this.cboBER.Location = new System.Drawing.Point(91, 232);
            this.cboBER.Name = "cboBER";
            this.cboBER.Size = new System.Drawing.Size(180, 20);
            this.cboBER.TabIndex = 58;
            // 
            // lblBER
            // 
            this.lblBER.AutoSize = true;
            this.lblBER.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblBER.Location = new System.Drawing.Point(8, 238);
            this.lblBER.Name = "lblBER";
            this.lblBER.Size = new System.Drawing.Size(53, 12);
            this.lblBER.TabIndex = 57;
            this.lblBER.Text = "BER(exp)";
            // 
            // cboAPC_Type
            // 
            this.cboAPC_Type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAPC_Type.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboAPC_Type.FormattingEnabled = true;
            this.cboAPC_Type.Location = new System.Drawing.Point(91, 209);
            this.cboAPC_Type.Name = "cboAPC_Type";
            this.cboAPC_Type.Size = new System.Drawing.Size(180, 20);
            this.cboAPC_Type.TabIndex = 58;
            // 
            // lblAPC_Type
            // 
            this.lblAPC_Type.AutoSize = true;
            this.lblAPC_Type.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblAPC_Type.Location = new System.Drawing.Point(8, 214);
            this.lblAPC_Type.Name = "lblAPC_Type";
            this.lblAPC_Type.Size = new System.Drawing.Size(53, 12);
            this.lblAPC_Type.TabIndex = 57;
            this.lblAPC_Type.Text = "APC_Type";
            // 
            // cboCouple_Type
            // 
            this.cboCouple_Type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCouple_Type.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboCouple_Type.FormattingEnabled = true;
            this.cboCouple_Type.Location = new System.Drawing.Point(91, 185);
            this.cboCouple_Type.Name = "cboCouple_Type";
            this.cboCouple_Type.Size = new System.Drawing.Size(180, 20);
            this.cboCouple_Type.TabIndex = 58;
            // 
            // lblCouple_Type
            // 
            this.lblCouple_Type.AutoSize = true;
            this.lblCouple_Type.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCouple_Type.Location = new System.Drawing.Point(9, 190);
            this.lblCouple_Type.Name = "lblCouple_Type";
            this.lblCouple_Type.Size = new System.Drawing.Size(71, 12);
            this.lblCouple_Type.TabIndex = 57;
            this.lblCouple_Type.Text = "Couple_Type";
            // 
            // cboTEC_Present
            // 
            this.cboTEC_Present.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTEC_Present.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboTEC_Present.FormattingEnabled = true;
            this.cboTEC_Present.Location = new System.Drawing.Point(91, 161);
            this.cboTEC_Present.Name = "cboTEC_Present";
            this.cboTEC_Present.Size = new System.Drawing.Size(180, 20);
            this.cboTEC_Present.TabIndex = 58;
            // 
            // lblTEC_Present
            // 
            this.lblTEC_Present.AutoSize = true;
            this.lblTEC_Present.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTEC_Present.Location = new System.Drawing.Point(9, 166);
            this.lblTEC_Present.Name = "lblTEC_Present";
            this.lblTEC_Present.Size = new System.Drawing.Size(71, 12);
            this.lblTEC_Present.TabIndex = 57;
            this.lblTEC_Present.Text = "TEC_Present";
            // 
            // cboAPCStyle
            // 
            this.cboAPCStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAPCStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboAPCStyle.FormattingEnabled = true;
            this.cboAPCStyle.Location = new System.Drawing.Point(91, 138);
            this.cboAPCStyle.Name = "cboAPCStyle";
            this.cboAPCStyle.Size = new System.Drawing.Size(180, 20);
            this.cboAPCStyle.TabIndex = 58;
            // 
            // lblAPCStyle
            // 
            this.lblAPCStyle.AutoSize = true;
            this.lblAPCStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblAPCStyle.Location = new System.Drawing.Point(8, 142);
            this.lblAPCStyle.Name = "lblAPCStyle";
            this.lblAPCStyle.Size = new System.Drawing.Size(53, 12);
            this.lblAPCStyle.TabIndex = 57;
            this.lblAPCStyle.Text = "APCStyle";
            // 
            // cboOldDriver
            // 
            this.cboOldDriver.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboOldDriver.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboOldDriver.FormattingEnabled = true;
            this.cboOldDriver.Location = new System.Drawing.Point(91, 255);
            this.cboOldDriver.Name = "cboOldDriver";
            this.cboOldDriver.Size = new System.Drawing.Size(180, 20);
            this.cboOldDriver.TabIndex = 58;
            // 
            // lblOldDriver
            // 
            this.lblOldDriver.AutoSize = true;
            this.lblOldDriver.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblOldDriver.Location = new System.Drawing.Point(8, 262);
            this.lblOldDriver.Name = "lblOldDriver";
            this.lblOldDriver.Size = new System.Drawing.Size(59, 12);
            this.lblOldDriver.TabIndex = 57;
            this.lblOldDriver.Text = "OldDriver";
            // 
            // cboIgnore
            // 
            this.cboIgnore.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboIgnore.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboIgnore.FormattingEnabled = true;
            this.cboIgnore.Location = new System.Drawing.Point(91, 496);
            this.cboIgnore.Name = "cboIgnore";
            this.cboIgnore.Size = new System.Drawing.Size(180, 20);
            this.cboIgnore.TabIndex = 56;
            // 
            // lblIgnore
            // 
            this.lblIgnore.AutoSize = true;
            this.lblIgnore.BackColor = System.Drawing.SystemColors.Control;
            this.lblIgnore.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblIgnore.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblIgnore.Location = new System.Drawing.Point(8, 499);
            this.lblIgnore.Name = "lblIgnore";
            this.lblIgnore.Size = new System.Drawing.Size(71, 12);
            this.lblIgnore.TabIndex = 55;
            this.lblIgnore.Text = "IgnoreItem?";
            // 
            // cboTsensors
            // 
            this.cboTsensors.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboTsensors.FormattingEnabled = true;
            this.cboTsensors.Location = new System.Drawing.Point(91, 91);
            this.cboTsensors.Name = "cboTsensors";
            this.cboTsensors.Size = new System.Drawing.Size(180, 20);
            this.cboTsensors.TabIndex = 37;
            this.cboTsensors.Leave += new System.EventHandler(this.cboTsensors_Leave);
            // 
            // cboVoltages
            // 
            this.cboVoltages.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboVoltages.FormattingEnabled = true;
            this.cboVoltages.Location = new System.Drawing.Point(91, 68);
            this.cboVoltages.Name = "cboVoltages";
            this.cboVoltages.Size = new System.Drawing.Size(180, 20);
            this.cboVoltages.TabIndex = 36;
            this.cboVoltages.Leave += new System.EventHandler(this.cboVoltages_Leave);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(9, 96);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 35;
            this.label9.Text = "Tsensors";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(8, 73);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 33;
            this.label4.Text = "Voltages";
            // 
            // cboMGroup
            // 
            this.cboMGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMGroup.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboMGroup.FormattingEnabled = true;
            this.cboMGroup.Location = new System.Drawing.Point(91, 114);
            this.cboMGroup.Name = "cboMGroup";
            this.cboMGroup.Size = new System.Drawing.Size(180, 20);
            this.cboMGroup.TabIndex = 32;
            this.cboMGroup.SelectedIndexChanged += new System.EventHandler(this.cboMGroup_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(9, 119);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 28;
            this.label7.Text = "MCoefGroup";
            // 
            // txtModFml
            // 
            this.txtModFml.Location = new System.Drawing.Point(91, 376);
            this.txtModFml.Multiline = true;
            this.txtModFml.Name = "txtModFml";
            this.txtModFml.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtModFml.Size = new System.Drawing.Size(180, 26);
            this.txtModFml.TabIndex = 25;
            // 
            // lblMod
            // 
            this.lblMod.AutoSize = true;
            this.lblMod.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblMod.Location = new System.Drawing.Point(8, 382);
            this.lblMod.Name = "lblMod";
            this.lblMod.Size = new System.Drawing.Size(71, 12);
            this.lblMod.TabIndex = 24;
            this.lblMod.Text = "IModFormula";
            // 
            // txtBiasFml
            // 
            this.txtBiasFml.Location = new System.Drawing.Point(91, 348);
            this.txtBiasFml.Multiline = true;
            this.txtBiasFml.Name = "txtBiasFml";
            this.txtBiasFml.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtBiasFml.Size = new System.Drawing.Size(180, 26);
            this.txtBiasFml.TabIndex = 25;
            // 
            // lblIbias
            // 
            this.lblIbias.AutoSize = true;
            this.lblIbias.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblIbias.Location = new System.Drawing.Point(8, 353);
            this.lblIbias.Name = "lblIbias";
            this.lblIbias.Size = new System.Drawing.Size(77, 12);
            this.lblIbias.TabIndex = 24;
            this.lblIbias.Text = "IbiasFormula";
            // 
            // txtItemName
            // 
            this.txtItemName.Location = new System.Drawing.Point(91, 434);
            this.txtItemName.Multiline = true;
            this.txtItemName.Name = "txtItemName";
            this.txtItemName.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtItemName.Size = new System.Drawing.Size(178, 36);
            this.txtItemName.TabIndex = 25;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(7, 442);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 12);
            this.label2.TabIndex = 24;
            this.label2.Text = "Description";
            // 
            // cboChannels
            // 
            this.cboChannels.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboChannels.FormattingEnabled = true;
            this.cboChannels.Location = new System.Drawing.Point(91, 45);
            this.cboChannels.Name = "cboChannels";
            this.cboChannels.Size = new System.Drawing.Size(180, 20);
            this.cboChannels.TabIndex = 23;
            // 
            // txtPN
            // 
            this.txtPN.Location = new System.Drawing.Point(91, 19);
            this.txtPN.Name = "txtPN";
            this.txtPN.Size = new System.Drawing.Size(180, 23);
            this.txtPN.TabIndex = 22;
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnOK.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOK.Location = new System.Drawing.Point(91, 522);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 21;
            this.btnOK.Text = "Save";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(7, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "Channels";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(8, 24);
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
            this.currlst.Size = new System.Drawing.Size(207, 328);
            this.currlst.TabIndex = 24;
            this.currlst.SelectedIndexChanged += new System.EventHandler(this.currlst_SelectedIndexChanged);
            // 
            // btnPreviousPage
            // 
            this.btnPreviousPage.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnPreviousPage.Location = new System.Drawing.Point(22, 552);
            this.btnPreviousPage.Name = "btnPreviousPage";
            this.btnPreviousPage.Size = new System.Drawing.Size(100, 28);
            this.btnPreviousPage.TabIndex = 27;
            this.btnPreviousPage.Text = "Return";
            this.btnPreviousPage.UseVisualStyleBackColor = false;
            this.btnPreviousPage.Click += new System.EventHandler(this.btnPreviousPage_Click);
            // 
            // btnInitInfo
            // 
            this.btnInitInfo.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnInitInfo.Enabled = false;
            this.btnInitInfo.Location = new System.Drawing.Point(360, 552);
            this.btnInitInfo.Name = "btnInitInfo";
            this.btnInitInfo.Size = new System.Drawing.Size(100, 28);
            this.btnInitInfo.TabIndex = 28;
            this.btnInitInfo.Text = "ChipsetInfo";
            this.btnInitInfo.UseVisualStyleBackColor = false;
            this.btnInitInfo.Click += new System.EventHandler(this.btnInitInfo_Click);
            // 
            // btnSpec
            // 
            this.btnSpec.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnSpec.Enabled = false;
            this.btnSpec.Location = new System.Drawing.Point(193, 552);
            this.btnSpec.Name = "btnSpec";
            this.btnSpec.Size = new System.Drawing.Size(100, 28);
            this.btnSpec.TabIndex = 29;
            this.btnSpec.Text = "ConfigSpecItem";
            this.btnSpec.UseVisualStyleBackColor = false;
            this.btnSpec.Click += new System.EventHandler(this.btnSpec_Click);
            // 
            // cboRxOverLoadDBm
            // 
            this.cboRxOverLoadDBm.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboRxOverLoadDBm.FormattingEnabled = true;
            this.cboRxOverLoadDBm.Location = new System.Drawing.Point(91, 474);
            this.cboRxOverLoadDBm.Name = "cboRxOverLoadDBm";
            this.cboRxOverLoadDBm.Size = new System.Drawing.Size(180, 20);
            this.cboRxOverLoadDBm.TabIndex = 64;
            // 
            // lblRxOverLoadDBm
            // 
            this.lblRxOverLoadDBm.AutoSize = true;
            this.lblRxOverLoadDBm.BackColor = System.Drawing.SystemColors.Control;
            this.lblRxOverLoadDBm.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblRxOverLoadDBm.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblRxOverLoadDBm.Location = new System.Drawing.Point(8, 477);
            this.lblRxOverLoadDBm.Name = "lblRxOverLoadDBm";
            this.lblRxOverLoadDBm.Size = new System.Drawing.Size(83, 12);
            this.lblRxOverLoadDBm.TabIndex = 63;
            this.lblRxOverLoadDBm.Text = "RxOverLoadDBm";
            // 
            // cboUsingTempAD
            // 
            this.cboUsingTempAD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboUsingTempAD.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboUsingTempAD.FormattingEnabled = true;
            this.cboUsingTempAD.Location = new System.Drawing.Point(91, 408);
            this.cboUsingTempAD.Name = "cboUsingTempAD";
            this.cboUsingTempAD.Size = new System.Drawing.Size(180, 20);
            this.cboUsingTempAD.TabIndex = 65;
            // 
            // PNInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 582);
            this.Controls.Add(this.btnSpec);
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
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox currlst;
        private System.Windows.Forms.ComboBox cboChannels;
        private System.Windows.Forms.Button btnPreviousPage;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtItemName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboMGroup;
        private System.Windows.Forms.ComboBox cboTsensors;
        private System.Windows.Forms.ComboBox cboVoltages;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnInitInfo;
        private System.Windows.Forms.ComboBox cboIgnore;
        private System.Windows.Forms.Label lblIgnore;
        private System.Windows.Forms.ComboBox cboNickName;
        private System.Windows.Forms.Label lblNickName;
        private System.Windows.Forms.ComboBox cboPublish_PN;
        private System.Windows.Forms.Label lblPublish_PN;
        private System.Windows.Forms.ComboBox cboMaxRate;
        private System.Windows.Forms.Label lblMaxRate;
        private System.Windows.Forms.ComboBox cboBER;
        private System.Windows.Forms.Label lblBER;
        private System.Windows.Forms.ComboBox cboAPC_Type;
        private System.Windows.Forms.Label lblAPC_Type;
        private System.Windows.Forms.ComboBox cboCouple_Type;
        private System.Windows.Forms.Label lblCouple_Type;
        private System.Windows.Forms.ComboBox cboTEC_Present;
        private System.Windows.Forms.Label lblTEC_Present;
        private System.Windows.Forms.ComboBox cboAPCStyle;
        private System.Windows.Forms.Label lblAPCStyle;
        private System.Windows.Forms.ComboBox cboOldDriver;
        private System.Windows.Forms.Label lblOldDriver;
        private System.Windows.Forms.Button btnSpec;
        private System.Windows.Forms.TextBox txtModFml;
        private System.Windows.Forms.Label lblMod;
        private System.Windows.Forms.TextBox txtBiasFml;
        private System.Windows.Forms.Label lblIbias;
        private System.Windows.Forms.Label lblCurv;
        private System.Windows.Forms.ComboBox cboRxOverLoadDBm;
        private System.Windows.Forms.Label lblRxOverLoadDBm;
        private System.Windows.Forms.ComboBox cboUsingTempAD;
    }
}
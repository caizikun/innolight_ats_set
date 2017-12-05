namespace Maintain
{
    partial class MCoefGroup
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
            this.grpEquipPrmtr = new System.Windows.Forms.GroupBox();
            this.btnPrmtrDelete = new System.Windows.Forms.Button();
            this.btnPrmtrAdd = new System.Windows.Forms.Button();
            this.txtSaveResult = new System.Windows.Forms.TextBox();
            this.dgvPrmtr = new System.Windows.Forms.DataGridView();
            this.btnPreviousPage = new System.Windows.Forms.Button();
            this.grpItem = new System.Windows.Forms.GroupBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cboName = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.currlst = new System.Windows.Forms.ListBox();
            this.lblGlobal = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.cboItemName = new System.Windows.Forms.ComboBox();
            this.cboChannel = new System.Windows.Forms.ComboBox();
            this.lblPrmtrItem = new System.Windows.Forms.Label();
            this.lblPrmtrValue = new System.Windows.Forms.Label();
            this.lblSlaveAddr = new System.Windows.Forms.Label();
            this.cboItemType = new System.Windows.Forms.ComboBox();
            this.cboPage = new System.Windows.Forms.ComboBox();
            this.lblPage = new System.Windows.Forms.Label();
            this.cboStartAddr = new System.Windows.Forms.ComboBox();
            this.lblStartAddr = new System.Windows.Forms.Label();
            this.cboLength = new System.Windows.Forms.ComboBox();
            this.lblLength = new System.Windows.Forms.Label();
            this.cboFormat = new System.Windows.Forms.ComboBox();
            this.lblFormat = new System.Windows.Forms.Label();
            this.grpPrmtr = new System.Windows.Forms.GroupBox();
            this.btnPrmtrOK = new System.Windows.Forms.Button();
            this.cboIgnore = new System.Windows.Forms.ComboBox();
            this.lblIgnore = new System.Windows.Forms.Label();
            this.lblType = new System.Windows.Forms.Label();
            this.cboType = new System.Windows.Forms.ComboBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.grpEquipPrmtr.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPrmtr)).BeginInit();
            this.grpItem.SuspendLayout();
            this.grpPrmtr.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpEquipPrmtr
            // 
            this.grpEquipPrmtr.Controls.Add(this.btnPrmtrDelete);
            this.grpEquipPrmtr.Controls.Add(this.btnPrmtrAdd);
            this.grpEquipPrmtr.Controls.Add(this.txtSaveResult);
            this.grpEquipPrmtr.Controls.Add(this.dgvPrmtr);
            this.grpEquipPrmtr.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grpEquipPrmtr.Location = new System.Drawing.Point(236, 11);
            this.grpEquipPrmtr.Name = "grpEquipPrmtr";
            this.grpEquipPrmtr.Size = new System.Drawing.Size(369, 402);
            this.grpEquipPrmtr.TabIndex = 51;
            this.grpEquipPrmtr.TabStop = false;
            this.grpEquipPrmtr.Text = "ManufactureCoefficientsParameter";
            // 
            // btnPrmtrDelete
            // 
            this.btnPrmtrDelete.BackColor = System.Drawing.Color.Fuchsia;
            this.btnPrmtrDelete.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPrmtrDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnPrmtrDelete.Location = new System.Drawing.Point(224, 341);
            this.btnPrmtrDelete.Name = "btnPrmtrDelete";
            this.btnPrmtrDelete.Size = new System.Drawing.Size(54, 25);
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
            this.btnPrmtrAdd.Location = new System.Drawing.Point(100, 341);
            this.btnPrmtrAdd.Name = "btnPrmtrAdd";
            this.btnPrmtrAdd.Size = new System.Drawing.Size(54, 25);
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
            this.dgvPrmtr.CurrentCellChanged += new System.EventHandler(this.dgvPrmtr_CurrentCellChanged);
            // 
            // btnPreviousPage
            // 
            this.btnPreviousPage.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnPreviousPage.Location = new System.Drawing.Point(359, 435);
            this.btnPreviousPage.Name = "btnPreviousPage";
            this.btnPreviousPage.Size = new System.Drawing.Size(114, 24);
            this.btnPreviousPage.TabIndex = 50;
            this.btnPreviousPage.Text = "Finish";
            this.btnPreviousPage.UseVisualStyleBackColor = true;
            this.btnPreviousPage.Click += new System.EventHandler(this.btnPreviousPage_Click);
            // 
            // grpItem
            // 
            this.grpItem.Controls.Add(this.txtDescription);
            this.grpItem.Controls.Add(this.lblDescription);
            this.grpItem.Controls.Add(this.cboType);
            this.grpItem.Controls.Add(this.lblType);
            this.grpItem.Controls.Add(this.cboIgnore);
            this.grpItem.Controls.Add(this.lblIgnore);
            this.grpItem.Controls.Add(this.btnOK);
            this.grpItem.Controls.Add(this.label6);
            this.grpItem.Controls.Add(this.label5);
            this.grpItem.Controls.Add(this.cboName);
            this.grpItem.Controls.Add(this.label1);
            this.grpItem.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grpItem.Location = new System.Drawing.Point(5, 259);
            this.grpItem.Name = "grpItem";
            this.grpItem.Size = new System.Drawing.Size(225, 200);
            this.grpItem.TabIndex = 49;
            this.grpItem.TabStop = false;
            this.grpItem.Text = "MCoefGroup Info";
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.LightGreen;
            this.btnOK.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOK.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnOK.Location = new System.Drawing.Point(79, 176);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(54, 23);
            this.btnOK.TabIndex = 50;
            this.btnOK.Text = "Save";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label6.Location = new System.Drawing.Point(114, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 12);
            this.label6.TabIndex = 9;
            this.label6.Text = "Value";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label5.Location = new System.Drawing.Point(7, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(33, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "Item";
            // 
            // cboName
            // 
            this.cboName.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboName.FormattingEnabled = true;
            this.cboName.Location = new System.Drawing.Point(79, 44);
            this.cboName.Name = "cboName";
            this.cboName.Size = new System.Drawing.Size(140, 22);
            this.cboName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(7, 47);
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
            "QSFP_xx"});
            this.currlst.Location = new System.Drawing.Point(5, 30);
            this.currlst.Name = "currlst";
            this.currlst.ScrollAlwaysVisible = true;
            this.currlst.Size = new System.Drawing.Size(225, 196);
            this.currlst.TabIndex = 48;
            this.currlst.SelectedIndexChanged += new System.EventHandler(this.currlst_SelectedIndexChanged);
            // 
            // lblGlobal
            // 
            this.lblGlobal.AutoSize = true;
            this.lblGlobal.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblGlobal.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblGlobal.Location = new System.Drawing.Point(52, 8);
            this.lblGlobal.Name = "lblGlobal";
            this.lblGlobal.Size = new System.Drawing.Size(128, 19);
            this.lblGlobal.TabIndex = 53;
            this.lblGlobal.Text = "MCoefGroup List";
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnAdd.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAdd.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnAdd.Location = new System.Drawing.Point(56, 232);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(56, 23);
            this.btnAdd.TabIndex = 55;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.BackColor = System.Drawing.Color.Fuchsia;
            this.btnRemove.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnRemove.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnRemove.Location = new System.Drawing.Point(126, 232);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(56, 23);
            this.btnRemove.TabIndex = 54;
            this.btnRemove.Text = "Delete";
            this.btnRemove.UseVisualStyleBackColor = false;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // cboItemName
            // 
            this.cboItemName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboItemName.FormattingEnabled = true;
            this.cboItemName.Location = new System.Drawing.Point(60, 47);
            this.cboItemName.Name = "cboItemName";
            this.cboItemName.Size = new System.Drawing.Size(155, 20);
            this.cboItemName.TabIndex = 52;
            // 
            // cboChannel
            // 
            this.cboChannel.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboChannel.FormattingEnabled = true;
            this.cboChannel.Items.AddRange(new object[] {
            "0XA0",
            "0XA2",
            "0X100"});
            this.cboChannel.Location = new System.Drawing.Point(60, 76);
            this.cboChannel.Name = "cboChannel";
            this.cboChannel.Size = new System.Drawing.Size(155, 20);
            this.cboChannel.TabIndex = 54;
            // 
            // lblPrmtrItem
            // 
            this.lblPrmtrItem.AutoSize = true;
            this.lblPrmtrItem.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPrmtrItem.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblPrmtrItem.Location = new System.Drawing.Point(3, 24);
            this.lblPrmtrItem.Name = "lblPrmtrItem";
            this.lblPrmtrItem.Size = new System.Drawing.Size(53, 12);
            this.lblPrmtrItem.TabIndex = 51;
            this.lblPrmtrItem.Text = "ItemType";
            // 
            // lblPrmtrValue
            // 
            this.lblPrmtrValue.AutoSize = true;
            this.lblPrmtrValue.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPrmtrValue.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblPrmtrValue.Location = new System.Drawing.Point(3, 50);
            this.lblPrmtrValue.Name = "lblPrmtrValue";
            this.lblPrmtrValue.Size = new System.Drawing.Size(53, 12);
            this.lblPrmtrValue.TabIndex = 55;
            this.lblPrmtrValue.Text = "ItemName";
            // 
            // lblSlaveAddr
            // 
            this.lblSlaveAddr.AutoSize = true;
            this.lblSlaveAddr.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblSlaveAddr.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblSlaveAddr.Location = new System.Drawing.Point(3, 79);
            this.lblSlaveAddr.Name = "lblSlaveAddr";
            this.lblSlaveAddr.Size = new System.Drawing.Size(59, 12);
            this.lblSlaveAddr.TabIndex = 49;
            this.lblSlaveAddr.Text = "ChannelNo";
            // 
            // cboItemType
            // 
            this.cboItemType.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboItemType.FormattingEnabled = true;
            this.cboItemType.Location = new System.Drawing.Point(60, 21);
            this.cboItemType.Name = "cboItemType";
            this.cboItemType.Size = new System.Drawing.Size(155, 20);
            this.cboItemType.TabIndex = 50;
            // 
            // cboPage
            // 
            this.cboPage.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboPage.FormattingEnabled = true;
            this.cboPage.Location = new System.Drawing.Point(60, 102);
            this.cboPage.Name = "cboPage";
            this.cboPage.Size = new System.Drawing.Size(155, 20);
            this.cboPage.TabIndex = 61;
            // 
            // lblPage
            // 
            this.lblPage.AutoSize = true;
            this.lblPage.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPage.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblPage.Location = new System.Drawing.Point(3, 105);
            this.lblPage.Name = "lblPage";
            this.lblPage.Size = new System.Drawing.Size(41, 12);
            this.lblPage.TabIndex = 60;
            this.lblPage.Text = "PageNo";
            // 
            // cboStartAddr
            // 
            this.cboStartAddr.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboStartAddr.FormattingEnabled = true;
            this.cboStartAddr.Items.AddRange(new object[] {
            "0X80",
            "0X00"});
            this.cboStartAddr.Location = new System.Drawing.Point(60, 128);
            this.cboStartAddr.Name = "cboStartAddr";
            this.cboStartAddr.Size = new System.Drawing.Size(155, 20);
            this.cboStartAddr.TabIndex = 63;
            // 
            // lblStartAddr
            // 
            this.lblStartAddr.AutoSize = true;
            this.lblStartAddr.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblStartAddr.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblStartAddr.Location = new System.Drawing.Point(3, 131);
            this.lblStartAddr.Name = "lblStartAddr";
            this.lblStartAddr.Size = new System.Drawing.Size(59, 12);
            this.lblStartAddr.TabIndex = 62;
            this.lblStartAddr.Text = "StartAddr";
            // 
            // cboLength
            // 
            this.cboLength.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboLength.FormattingEnabled = true;
            this.cboLength.Location = new System.Drawing.Point(59, 152);
            this.cboLength.Name = "cboLength";
            this.cboLength.Size = new System.Drawing.Size(155, 20);
            this.cboLength.TabIndex = 65;
            // 
            // lblLength
            // 
            this.lblLength.AutoSize = true;
            this.lblLength.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblLength.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblLength.Location = new System.Drawing.Point(2, 155);
            this.lblLength.Name = "lblLength";
            this.lblLength.Size = new System.Drawing.Size(41, 12);
            this.lblLength.TabIndex = 64;
            this.lblLength.Text = "Length";
            // 
            // cboFormat
            // 
            this.cboFormat.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboFormat.FormattingEnabled = true;
            this.cboFormat.Location = new System.Drawing.Point(60, 178);
            this.cboFormat.Name = "cboFormat";
            this.cboFormat.Size = new System.Drawing.Size(155, 20);
            this.cboFormat.TabIndex = 67;
            // 
            // lblFormat
            // 
            this.lblFormat.AutoSize = true;
            this.lblFormat.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblFormat.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblFormat.Location = new System.Drawing.Point(3, 181);
            this.lblFormat.Name = "lblFormat";
            this.lblFormat.Size = new System.Drawing.Size(59, 12);
            this.lblFormat.TabIndex = 66;
            this.lblFormat.Text = "ValueType";
            // 
            // grpPrmtr
            // 
            this.grpPrmtr.Controls.Add(this.btnPrmtrOK);
            this.grpPrmtr.Controls.Add(this.lblFormat);
            this.grpPrmtr.Controls.Add(this.cboFormat);
            this.grpPrmtr.Controls.Add(this.lblLength);
            this.grpPrmtr.Controls.Add(this.cboLength);
            this.grpPrmtr.Controls.Add(this.lblStartAddr);
            this.grpPrmtr.Controls.Add(this.cboStartAddr);
            this.grpPrmtr.Controls.Add(this.lblPage);
            this.grpPrmtr.Controls.Add(this.cboPage);
            this.grpPrmtr.Controls.Add(this.cboItemType);
            this.grpPrmtr.Controls.Add(this.lblSlaveAddr);
            this.grpPrmtr.Controls.Add(this.lblPrmtrValue);
            this.grpPrmtr.Controls.Add(this.lblPrmtrItem);
            this.grpPrmtr.Controls.Add(this.cboChannel);
            this.grpPrmtr.Controls.Add(this.cboItemName);
            this.grpPrmtr.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grpPrmtr.Location = new System.Drawing.Point(611, 11);
            this.grpPrmtr.Name = "grpPrmtr";
            this.grpPrmtr.Size = new System.Drawing.Size(220, 402);
            this.grpPrmtr.TabIndex = 52;
            this.grpPrmtr.TabStop = false;
            this.grpPrmtr.Text = "Current Parameter Item";
            // 
            // btnPrmtrOK
            // 
            this.btnPrmtrOK.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnPrmtrOK.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPrmtrOK.Location = new System.Drawing.Point(90, 221);
            this.btnPrmtrOK.Name = "btnPrmtrOK";
            this.btnPrmtrOK.Size = new System.Drawing.Size(42, 22);
            this.btnPrmtrOK.TabIndex = 68;
            this.btnPrmtrOK.Text = "Save";
            this.btnPrmtrOK.UseVisualStyleBackColor = false;
            this.btnPrmtrOK.Click += new System.EventHandler(this.btnPrmtrOK_Click);
            // 
            // cboIgnore
            // 
            this.cboIgnore.BackColor = System.Drawing.SystemColors.Window;
            this.cboIgnore.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboIgnore.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboIgnore.FormattingEnabled = true;
            this.cboIgnore.Location = new System.Drawing.Point(79, 143);
            this.cboIgnore.Name = "cboIgnore";
            this.cboIgnore.Size = new System.Drawing.Size(140, 22);
            this.cboIgnore.TabIndex = 58;
            // 
            // lblIgnore
            // 
            this.lblIgnore.AutoSize = true;
            this.lblIgnore.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblIgnore.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblIgnore.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblIgnore.Location = new System.Drawing.Point(7, 146);
            this.lblIgnore.Name = "lblIgnore";
            this.lblIgnore.Size = new System.Drawing.Size(71, 12);
            this.lblIgnore.TabIndex = 57;
            this.lblIgnore.Text = "IgnoreItem?";
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblType.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblType.Location = new System.Drawing.Point(7, 73);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(29, 12);
            this.lblType.TabIndex = 57;
            this.lblType.Text = "Type";
            // 
            // cboType
            // 
            this.cboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboType.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboType.FormattingEnabled = true;
            this.cboType.Location = new System.Drawing.Point(79, 70);
            this.cboType.Name = "cboType";
            this.cboType.Size = new System.Drawing.Size(140, 22);
            this.cboType.TabIndex = 58;
            // 
            // txtDescription
            // 
            this.txtDescription.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtDescription.Location = new System.Drawing.Point(79, 93);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescription.Size = new System.Drawing.Size(140, 44);
            this.txtDescription.TabIndex = 60;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDescription.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblDescription.Location = new System.Drawing.Point(7, 111);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(71, 12);
            this.lblDescription.TabIndex = 59;
            this.lblDescription.Text = "Description";
            // 
            // MCoefGroup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(836, 471);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.lblGlobal);
            this.Controls.Add(this.grpPrmtr);
            this.Controls.Add(this.grpEquipPrmtr);
            this.Controls.Add(this.btnPreviousPage);
            this.Controls.Add(this.grpItem);
            this.Controls.Add(this.currlst);
            this.MaximumSize = new System.Drawing.Size(852, 509);
            this.Name = "MCoefGroup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MCoefGroup";
            this.Load += new System.EventHandler(this.MCoefGroup_Load);
            this.grpEquipPrmtr.ResumeLayout(false);
            this.grpEquipPrmtr.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPrmtr)).EndInit();
            this.grpItem.ResumeLayout(false);
            this.grpItem.PerformLayout();
            this.grpPrmtr.ResumeLayout(false);
            this.grpPrmtr.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpEquipPrmtr;
        private System.Windows.Forms.TextBox txtSaveResult;
        private System.Windows.Forms.DataGridView dgvPrmtr;
        private System.Windows.Forms.Button btnPreviousPage;
        private System.Windows.Forms.GroupBox grpItem;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox currlst;
        private System.Windows.Forms.Label lblGlobal;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnPrmtrDelete;
        private System.Windows.Forms.Button btnPrmtrAdd;
        private System.Windows.Forms.ComboBox cboItemName;
        private System.Windows.Forms.ComboBox cboChannel;
        private System.Windows.Forms.Label lblPrmtrItem;
        private System.Windows.Forms.Label lblPrmtrValue;
        private System.Windows.Forms.Label lblSlaveAddr;
        private System.Windows.Forms.ComboBox cboItemType;
        private System.Windows.Forms.ComboBox cboPage;
        private System.Windows.Forms.Label lblPage;
        private System.Windows.Forms.ComboBox cboStartAddr;
        private System.Windows.Forms.Label lblStartAddr;
        private System.Windows.Forms.ComboBox cboLength;
        private System.Windows.Forms.Label lblLength;
        private System.Windows.Forms.ComboBox cboFormat;
        private System.Windows.Forms.Label lblFormat;
        private System.Windows.Forms.GroupBox grpPrmtr;
        private System.Windows.Forms.Button btnPrmtrOK;
        private System.Windows.Forms.ComboBox cboType;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.ComboBox cboIgnore;
        private System.Windows.Forms.Label lblIgnore;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label lblDescription;
    }
}
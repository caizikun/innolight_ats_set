namespace Maintain
{
    partial class MSAInfo
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
            this.cboAddress = new System.Windows.Forms.ComboBox();
            this.lblEquipDescription = new System.Windows.Forms.Label();
            this.cboInterface = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cboType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.currlst = new System.Windows.Forms.ListBox();
            this.lblGlobal = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.cboChannel = new System.Windows.Forms.ComboBox();
            this.cboSlaveAddr = new System.Windows.Forms.ComboBox();
            this.lblPrmtrItem = new System.Windows.Forms.Label();
            this.lblPrmtrValue = new System.Windows.Forms.Label();
            this.lblSlaveAddr = new System.Windows.Forms.Label();
            this.cboFieldName = new System.Windows.Forms.ComboBox();
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
            this.grpEquipPrmtr.Size = new System.Drawing.Size(369, 418);
            this.grpEquipPrmtr.TabIndex = 51;
            this.grpEquipPrmtr.TabStop = false;
            this.grpEquipPrmtr.Text = "MSA Parameters";
            // 
            // btnPrmtrDelete
            // 
            this.btnPrmtrDelete.BackColor = System.Drawing.Color.Fuchsia;
            this.btnPrmtrDelete.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPrmtrDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnPrmtrDelete.Location = new System.Drawing.Point(218, 341);
            this.btnPrmtrDelete.Name = "btnPrmtrDelete";
            this.btnPrmtrDelete.Size = new System.Drawing.Size(66, 25);
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
            this.btnPrmtrAdd.Location = new System.Drawing.Point(91, 341);
            this.btnPrmtrAdd.Name = "btnPrmtrAdd";
            this.btnPrmtrAdd.Size = new System.Drawing.Size(68, 25);
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
            this.txtSaveResult.Size = new System.Drawing.Size(357, 40);
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
            this.grpItem.Controls.Add(this.btnOK);
            this.grpItem.Controls.Add(this.cboAddress);
            this.grpItem.Controls.Add(this.lblEquipDescription);
            this.grpItem.Controls.Add(this.cboInterface);
            this.grpItem.Controls.Add(this.label2);
            this.grpItem.Controls.Add(this.cboType);
            this.grpItem.Controls.Add(this.label1);
            this.grpItem.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grpItem.Location = new System.Drawing.Point(5, 259);
            this.grpItem.Name = "grpItem";
            this.grpItem.Size = new System.Drawing.Size(225, 170);
            this.grpItem.TabIndex = 49;
            this.grpItem.TabStop = false;
            this.grpItem.Text = "MSA Info";
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.LightGreen;
            this.btnOK.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOK.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnOK.Location = new System.Drawing.Point(93, 122);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(43, 23);
            this.btnOK.TabIndex = 50;
            this.btnOK.Text = "Save";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // cboAddress
            // 
            this.cboAddress.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboAddress.FormattingEnabled = true;
            this.cboAddress.Location = new System.Drawing.Point(93, 96);
            this.cboAddress.Name = "cboAddress";
            this.cboAddress.Size = new System.Drawing.Size(127, 20);
            this.cboAddress.TabIndex = 49;
            // 
            // lblEquipDescription
            // 
            this.lblEquipDescription.AutoSize = true;
            this.lblEquipDescription.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblEquipDescription.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblEquipDescription.Location = new System.Drawing.Point(7, 96);
            this.lblEquipDescription.Name = "lblEquipDescription";
            this.lblEquipDescription.Size = new System.Drawing.Size(83, 12);
            this.lblEquipDescription.TabIndex = 46;
            this.lblEquipDescription.Text = "Slave address";
            // 
            // cboInterface
            // 
            this.cboInterface.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboInterface.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboInterface.FormattingEnabled = true;
            this.cboInterface.Location = new System.Drawing.Point(93, 70);
            this.cboInterface.Name = "cboInterface";
            this.cboInterface.Size = new System.Drawing.Size(127, 20);
            this.cboInterface.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(7, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "Interface";
            // 
            // cboType
            // 
            this.cboType.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboType.FormattingEnabled = true;
            this.cboType.Location = new System.Drawing.Point(93, 44);
            this.cboType.Name = "cboType";
            this.cboType.Size = new System.Drawing.Size(127, 20);
            this.cboType.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(7, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "MsaName";
            // 
            // currlst
            // 
            this.currlst.FormattingEnabled = true;
            this.currlst.HorizontalScrollbar = true;
            this.currlst.ItemHeight = 12;
            this.currlst.Items.AddRange(new object[] {
            "SFF-8636",
            "CFP MSA",
            "SFF-8472",
            "SFF-8077"});
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
            this.lblGlobal.Location = new System.Drawing.Point(70, 8);
            this.lblGlobal.Name = "lblGlobal";
            this.lblGlobal.Size = new System.Drawing.Size(71, 19);
            this.lblGlobal.TabIndex = 53;
            this.lblGlobal.Text = "MSA List";
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnAdd.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAdd.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnAdd.Location = new System.Drawing.Point(56, 232);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(54, 23);
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
            this.btnRemove.Size = new System.Drawing.Size(54, 23);
            this.btnRemove.TabIndex = 54;
            this.btnRemove.Text = "Delete";
            this.btnRemove.UseVisualStyleBackColor = false;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // cboChannel
            // 
            this.cboChannel.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboChannel.FormattingEnabled = true;
            this.cboChannel.Location = new System.Drawing.Point(68, 47);
            this.cboChannel.Name = "cboChannel";
            this.cboChannel.Size = new System.Drawing.Size(147, 20);
            this.cboChannel.TabIndex = 52;
            // 
            // cboSlaveAddr
            // 
            this.cboSlaveAddr.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboSlaveAddr.FormattingEnabled = true;
            this.cboSlaveAddr.Location = new System.Drawing.Point(68, 76);
            this.cboSlaveAddr.Name = "cboSlaveAddr";
            this.cboSlaveAddr.Size = new System.Drawing.Size(147, 20);
            this.cboSlaveAddr.TabIndex = 54;
            // 
            // lblPrmtrItem
            // 
            this.lblPrmtrItem.AutoSize = true;
            this.lblPrmtrItem.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPrmtrItem.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblPrmtrItem.Location = new System.Drawing.Point(3, 24);
            this.lblPrmtrItem.Name = "lblPrmtrItem";
            this.lblPrmtrItem.Size = new System.Drawing.Size(59, 12);
            this.lblPrmtrItem.TabIndex = 51;
            this.lblPrmtrItem.Text = "FieldName";
            // 
            // lblPrmtrValue
            // 
            this.lblPrmtrValue.AutoSize = true;
            this.lblPrmtrValue.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPrmtrValue.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblPrmtrValue.Location = new System.Drawing.Point(3, 50);
            this.lblPrmtrValue.Name = "lblPrmtrValue";
            this.lblPrmtrValue.Size = new System.Drawing.Size(47, 12);
            this.lblPrmtrValue.TabIndex = 55;
            this.lblPrmtrValue.Text = "Channel";
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
            this.lblSlaveAddr.Text = "SlaveAddr";
            // 
            // cboFieldName
            // 
            this.cboFieldName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboFieldName.FormattingEnabled = true;
            this.cboFieldName.Location = new System.Drawing.Point(68, 21);
            this.cboFieldName.Name = "cboFieldName";
            this.cboFieldName.Size = new System.Drawing.Size(147, 20);
            this.cboFieldName.TabIndex = 50;
            // 
            // cboPage
            // 
            this.cboPage.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboPage.FormattingEnabled = true;
            this.cboPage.Location = new System.Drawing.Point(68, 102);
            this.cboPage.Name = "cboPage";
            this.cboPage.Size = new System.Drawing.Size(147, 20);
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
            this.cboStartAddr.Location = new System.Drawing.Point(68, 128);
            this.cboStartAddr.Name = "cboStartAddr";
            this.cboStartAddr.Size = new System.Drawing.Size(147, 20);
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
            this.cboLength.Location = new System.Drawing.Point(67, 152);
            this.cboLength.Name = "cboLength";
            this.cboLength.Size = new System.Drawing.Size(147, 20);
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
            this.cboFormat.Location = new System.Drawing.Point(68, 178);
            this.cboFormat.Name = "cboFormat";
            this.cboFormat.Size = new System.Drawing.Size(147, 20);
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
            this.grpPrmtr.Controls.Add(this.cboFieldName);
            this.grpPrmtr.Controls.Add(this.lblSlaveAddr);
            this.grpPrmtr.Controls.Add(this.lblPrmtrValue);
            this.grpPrmtr.Controls.Add(this.lblPrmtrItem);
            this.grpPrmtr.Controls.Add(this.cboSlaveAddr);
            this.grpPrmtr.Controls.Add(this.cboChannel);
            this.grpPrmtr.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grpPrmtr.Location = new System.Drawing.Point(611, 11);
            this.grpPrmtr.Name = "grpPrmtr";
            this.grpPrmtr.Size = new System.Drawing.Size(220, 418);
            this.grpPrmtr.TabIndex = 52;
            this.grpPrmtr.TabStop = false;
            this.grpPrmtr.Text = "Current Parameter Item";
            // 
            // btnPrmtrOK
            // 
            this.btnPrmtrOK.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPrmtrOK.Location = new System.Drawing.Point(90, 221);
            this.btnPrmtrOK.Name = "btnPrmtrOK";
            this.btnPrmtrOK.Size = new System.Drawing.Size(42, 22);
            this.btnPrmtrOK.TabIndex = 68;
            this.btnPrmtrOK.Text = "Save";
            this.btnPrmtrOK.UseVisualStyleBackColor = true;
            this.btnPrmtrOK.Click += new System.EventHandler(this.btnPrmtrOK_Click);
            // 
            // MSAInfo
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
            this.Name = "MSAInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MSAInfo";
            this.Load += new System.EventHandler(this.MSAInfo_Load);
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
        private System.Windows.Forms.Label lblEquipDescription;
        private System.Windows.Forms.ComboBox cboInterface;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox currlst;
        private System.Windows.Forms.Label lblGlobal;
        private System.Windows.Forms.ComboBox cboAddress;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnPrmtrDelete;
        private System.Windows.Forms.Button btnPrmtrAdd;
        private System.Windows.Forms.ComboBox cboChannel;
        private System.Windows.Forms.ComboBox cboSlaveAddr;
        private System.Windows.Forms.Label lblPrmtrItem;
        private System.Windows.Forms.Label lblPrmtrValue;
        private System.Windows.Forms.Label lblSlaveAddr;
        private System.Windows.Forms.ComboBox cboFieldName;
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
    }
}
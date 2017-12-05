namespace Maintain
{
    partial class InitInfoForm
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
            this.btnPreviousPage = new System.Windows.Forms.Button();
            this.tabInit = new System.Windows.Forms.TabControl();
            this.tabChipsetControl = new System.Windows.Forms.TabPage();
            this.grpPrmtr = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboEndianness = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnChipCtrlOK = new System.Windows.Forms.Button();
            this.lblLength = new System.Windows.Forms.Label();
            this.cboLength = new System.Windows.Forms.ComboBox();
            this.lblStartAddr = new System.Windows.Forms.Label();
            this.cboStartAddr = new System.Windows.Forms.ComboBox();
            this.lblDriveType = new System.Windows.Forms.Label();
            this.cboDriveType = new System.Windows.Forms.ComboBox();
            this.cboItemName = new System.Windows.Forms.ComboBox();
            this.lblChipLine = new System.Windows.Forms.Label();
            this.lblPrmtrValue = new System.Windows.Forms.Label();
            this.lblPrmtrItem = new System.Windows.Forms.Label();
            this.cboChipLine = new System.Windows.Forms.ComboBox();
            this.cboModelLine = new System.Windows.Forms.ComboBox();
            this.grpEquipPrmtr = new System.Windows.Forms.GroupBox();
            this.btnChipCtrlDelete = new System.Windows.Forms.Button();
            this.btnChipCtrlAdd = new System.Windows.Forms.Button();
            this.txtSaveResult = new System.Windows.Forms.TextBox();
            this.dgvChipCtrl = new System.Windows.Forms.DataGridView();
            this.tabChipsetInitialize = new System.Windows.Forms.TabPage();
            this.grpChipInit = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cboChipInitEndianness = new System.Windows.Forms.ComboBox();
            this.btnChipInitOK = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.cboChipInitItemVaule = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cboChipInitLength = new System.Windows.Forms.ComboBox();
            this.cboChipInitDriveType = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.cboChipInitAddress = new System.Windows.Forms.ComboBox();
            this.cboChipInitChipLine = new System.Windows.Forms.ComboBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnRemoveChipInit = new System.Windows.Forms.Button();
            this.btnAddChipInit = new System.Windows.Forms.Button();
            this.txtChipInit = new System.Windows.Forms.TextBox();
            this.dgvChipInit = new System.Windows.Forms.DataGridView();
            this.chkChipsetControl = new System.Windows.Forms.CheckBox();
            this.chkChipsetInitialize = new System.Windows.Forms.CheckBox();
            this.tabInit.SuspendLayout();
            this.tabChipsetControl.SuspendLayout();
            this.grpPrmtr.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.grpEquipPrmtr.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChipCtrl)).BeginInit();
            this.tabChipsetInitialize.SuspendLayout();
            this.grpChipInit.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChipInit)).BeginInit();
            this.SuspendLayout();
            // 
            // btnPreviousPage
            // 
            this.btnPreviousPage.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnPreviousPage.Location = new System.Drawing.Point(276, 479);
            this.btnPreviousPage.Name = "btnPreviousPage";
            this.btnPreviousPage.Size = new System.Drawing.Size(114, 24);
            this.btnPreviousPage.TabIndex = 50;
            this.btnPreviousPage.Text = "Finish";
            this.btnPreviousPage.UseVisualStyleBackColor = true;
            this.btnPreviousPage.Click += new System.EventHandler(this.btnPreviousPage_Click);
            // 
            // tabInit
            // 
            this.tabInit.Controls.Add(this.tabChipsetControl);
            this.tabInit.Controls.Add(this.tabChipsetInitialize);
            this.tabInit.Location = new System.Drawing.Point(12, 31);
            this.tabInit.Name = "tabInit";
            this.tabInit.SelectedIndex = 0;
            this.tabInit.Size = new System.Drawing.Size(616, 442);
            this.tabInit.TabIndex = 54;
            // 
            // tabChipsetControl
            // 
            this.tabChipsetControl.Controls.Add(this.grpPrmtr);
            this.tabChipsetControl.Controls.Add(this.grpEquipPrmtr);
            this.tabChipsetControl.Location = new System.Drawing.Point(4, 22);
            this.tabChipsetControl.Name = "tabChipsetControl";
            this.tabChipsetControl.Padding = new System.Windows.Forms.Padding(3);
            this.tabChipsetControl.Size = new System.Drawing.Size(608, 416);
            this.tabChipsetControl.TabIndex = 0;
            this.tabChipsetControl.Text = "ChipsetControl";
            this.tabChipsetControl.UseVisualStyleBackColor = true;
            // 
            // grpPrmtr
            // 
            this.grpPrmtr.Controls.Add(this.label1);
            this.grpPrmtr.Controls.Add(this.cboEndianness);
            this.grpPrmtr.Controls.Add(this.groupBox1);
            this.grpPrmtr.Controls.Add(this.btnChipCtrlOK);
            this.grpPrmtr.Controls.Add(this.lblLength);
            this.grpPrmtr.Controls.Add(this.cboLength);
            this.grpPrmtr.Controls.Add(this.lblStartAddr);
            this.grpPrmtr.Controls.Add(this.cboStartAddr);
            this.grpPrmtr.Controls.Add(this.lblDriveType);
            this.grpPrmtr.Controls.Add(this.cboDriveType);
            this.grpPrmtr.Controls.Add(this.cboItemName);
            this.grpPrmtr.Controls.Add(this.lblChipLine);
            this.grpPrmtr.Controls.Add(this.lblPrmtrValue);
            this.grpPrmtr.Controls.Add(this.lblPrmtrItem);
            this.grpPrmtr.Controls.Add(this.cboChipLine);
            this.grpPrmtr.Controls.Add(this.cboModelLine);
            this.grpPrmtr.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grpPrmtr.Location = new System.Drawing.Point(382, 7);
            this.grpPrmtr.Name = "grpPrmtr";
            this.grpPrmtr.Size = new System.Drawing.Size(220, 402);
            this.grpPrmtr.TabIndex = 54;
            this.grpPrmtr.TabStop = false;
            this.grpPrmtr.Text = "Current Item";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(3, 181);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 71;
            this.label1.Text = "Endianness?";
            // 
            // cboEndianness
            // 
            this.cboEndianness.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEndianness.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboEndianness.FormattingEnabled = true;
            this.cboEndianness.Location = new System.Drawing.Point(90, 178);
            this.cboEndianness.Name = "cboEndianness";
            this.cboEndianness.Size = new System.Drawing.Size(125, 20);
            this.cboEndianness.TabIndex = 72;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(-371, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(369, 402);
            this.groupBox1.TabIndex = 53;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "协议参数信息";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Fuchsia;
            this.button1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.button1.Location = new System.Drawing.Point(236, 341);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(42, 25);
            this.button1.TabIndex = 62;
            this.button1.Text = "移除";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.button2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.button2.Location = new System.Drawing.Point(112, 341);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(42, 25);
            this.button2.TabIndex = 61;
            this.button2.Text = "添加";
            this.button2.UseVisualStyleBackColor = false;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.HighlightText;
            this.textBox1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox1.Location = new System.Drawing.Point(6, 372);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(357, 24);
            this.textBox1.TabIndex = 23;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(6, 20);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(357, 318);
            this.dataGridView1.TabIndex = 3;
            // 
            // btnChipCtrlOK
            // 
            this.btnChipCtrlOK.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnChipCtrlOK.Location = new System.Drawing.Point(89, 225);
            this.btnChipCtrlOK.Name = "btnChipCtrlOK";
            this.btnChipCtrlOK.Size = new System.Drawing.Size(42, 22);
            this.btnChipCtrlOK.TabIndex = 68;
            this.btnChipCtrlOK.Text = "Save";
            this.btnChipCtrlOK.UseVisualStyleBackColor = true;
            this.btnChipCtrlOK.Click += new System.EventHandler(this.btnChipCtrlOK_Click);
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
            this.lblLength.Text = "length";
            // 
            // cboLength
            // 
            this.cboLength.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboLength.FormattingEnabled = true;
            this.cboLength.Location = new System.Drawing.Point(89, 152);
            this.cboLength.Name = "cboLength";
            this.cboLength.Size = new System.Drawing.Size(125, 20);
            this.cboLength.TabIndex = 65;
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
            // cboStartAddr
            // 
            this.cboStartAddr.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboStartAddr.FormattingEnabled = true;
            this.cboStartAddr.Location = new System.Drawing.Point(90, 128);
            this.cboStartAddr.Name = "cboStartAddr";
            this.cboStartAddr.Size = new System.Drawing.Size(125, 20);
            this.cboStartAddr.TabIndex = 63;
            // 
            // lblDriveType
            // 
            this.lblDriveType.AutoSize = true;
            this.lblDriveType.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDriveType.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblDriveType.Location = new System.Drawing.Point(3, 105);
            this.lblDriveType.Name = "lblDriveType";
            this.lblDriveType.Size = new System.Drawing.Size(59, 12);
            this.lblDriveType.TabIndex = 60;
            this.lblDriveType.Text = "DriveType";
            // 
            // cboDriveType
            // 
            this.cboDriveType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDriveType.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboDriveType.FormattingEnabled = true;
            this.cboDriveType.Location = new System.Drawing.Point(90, 102);
            this.cboDriveType.Name = "cboDriveType";
            this.cboDriveType.Size = new System.Drawing.Size(125, 20);
            this.cboDriveType.TabIndex = 61;
            // 
            // cboItemName
            // 
            this.cboItemName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboItemName.FormattingEnabled = true;
            this.cboItemName.Location = new System.Drawing.Point(90, 21);
            this.cboItemName.Name = "cboItemName";
            this.cboItemName.Size = new System.Drawing.Size(125, 20);
            this.cboItemName.TabIndex = 50;
            // 
            // lblChipLine
            // 
            this.lblChipLine.AutoSize = true;
            this.lblChipLine.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblChipLine.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblChipLine.Location = new System.Drawing.Point(3, 79);
            this.lblChipLine.Name = "lblChipLine";
            this.lblChipLine.Size = new System.Drawing.Size(53, 12);
            this.lblChipLine.TabIndex = 49;
            this.lblChipLine.Text = "ChipLine";
            // 
            // lblPrmtrValue
            // 
            this.lblPrmtrValue.AutoSize = true;
            this.lblPrmtrValue.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPrmtrValue.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblPrmtrValue.Location = new System.Drawing.Point(3, 50);
            this.lblPrmtrValue.Name = "lblPrmtrValue";
            this.lblPrmtrValue.Size = new System.Drawing.Size(59, 12);
            this.lblPrmtrValue.TabIndex = 55;
            this.lblPrmtrValue.Text = "ChannelNo";
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
            this.lblPrmtrItem.Text = "ItemName";
            // 
            // cboChipLine
            // 
            this.cboChipLine.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboChipLine.FormattingEnabled = true;
            this.cboChipLine.Location = new System.Drawing.Point(90, 76);
            this.cboChipLine.Name = "cboChipLine";
            this.cboChipLine.Size = new System.Drawing.Size(125, 20);
            this.cboChipLine.TabIndex = 54;
            // 
            // cboModelLine
            // 
            this.cboModelLine.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboModelLine.FormattingEnabled = true;
            this.cboModelLine.Location = new System.Drawing.Point(90, 47);
            this.cboModelLine.Name = "cboModelLine";
            this.cboModelLine.Size = new System.Drawing.Size(125, 20);
            this.cboModelLine.TabIndex = 52;
            // 
            // grpEquipPrmtr
            // 
            this.grpEquipPrmtr.Controls.Add(this.btnChipCtrlDelete);
            this.grpEquipPrmtr.Controls.Add(this.btnChipCtrlAdd);
            this.grpEquipPrmtr.Controls.Add(this.txtSaveResult);
            this.grpEquipPrmtr.Controls.Add(this.dgvChipCtrl);
            this.grpEquipPrmtr.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grpEquipPrmtr.Location = new System.Drawing.Point(7, 7);
            this.grpEquipPrmtr.Name = "grpEquipPrmtr";
            this.grpEquipPrmtr.Size = new System.Drawing.Size(369, 402);
            this.grpEquipPrmtr.TabIndex = 53;
            this.grpEquipPrmtr.TabStop = false;
            this.grpEquipPrmtr.Text = "ChipSet Info";
            // 
            // btnChipCtrlDelete
            // 
            this.btnChipCtrlDelete.BackColor = System.Drawing.Color.Fuchsia;
            this.btnChipCtrlDelete.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnChipCtrlDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnChipCtrlDelete.Location = new System.Drawing.Point(216, 341);
            this.btnChipCtrlDelete.Name = "btnChipCtrlDelete";
            this.btnChipCtrlDelete.Size = new System.Drawing.Size(62, 25);
            this.btnChipCtrlDelete.TabIndex = 62;
            this.btnChipCtrlDelete.Text = "Delete";
            this.btnChipCtrlDelete.UseVisualStyleBackColor = false;
            this.btnChipCtrlDelete.Click += new System.EventHandler(this.btnChipCtrlDelete_Click);
            // 
            // btnChipCtrlAdd
            // 
            this.btnChipCtrlAdd.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnChipCtrlAdd.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnChipCtrlAdd.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnChipCtrlAdd.Location = new System.Drawing.Point(92, 341);
            this.btnChipCtrlAdd.Name = "btnChipCtrlAdd";
            this.btnChipCtrlAdd.Size = new System.Drawing.Size(62, 25);
            this.btnChipCtrlAdd.TabIndex = 61;
            this.btnChipCtrlAdd.Text = "Add";
            this.btnChipCtrlAdd.UseVisualStyleBackColor = false;
            this.btnChipCtrlAdd.Click += new System.EventHandler(this.btnChipCtrlAdd_Click);
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
            // dgvChipCtrl
            // 
            this.dgvChipCtrl.AllowUserToAddRows = false;
            this.dgvChipCtrl.AllowUserToDeleteRows = false;
            this.dgvChipCtrl.AllowUserToResizeColumns = false;
            this.dgvChipCtrl.AllowUserToResizeRows = false;
            this.dgvChipCtrl.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvChipCtrl.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvChipCtrl.Location = new System.Drawing.Point(6, 20);
            this.dgvChipCtrl.Name = "dgvChipCtrl";
            this.dgvChipCtrl.ReadOnly = true;
            this.dgvChipCtrl.RowHeadersVisible = false;
            this.dgvChipCtrl.RowTemplate.Height = 23;
            this.dgvChipCtrl.Size = new System.Drawing.Size(357, 318);
            this.dgvChipCtrl.TabIndex = 3;
            this.dgvChipCtrl.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvChipCtrl_CellMouseClick);
            // 
            // tabChipsetInitialize
            // 
            this.tabChipsetInitialize.Controls.Add(this.grpChipInit);
            this.tabChipsetInitialize.Controls.Add(this.groupBox4);
            this.tabChipsetInitialize.Location = new System.Drawing.Point(4, 22);
            this.tabChipsetInitialize.Name = "tabChipsetInitialize";
            this.tabChipsetInitialize.Padding = new System.Windows.Forms.Padding(3);
            this.tabChipsetInitialize.Size = new System.Drawing.Size(608, 416);
            this.tabChipsetInitialize.TabIndex = 1;
            this.tabChipsetInitialize.Text = "ChipsetInitialize";
            this.tabChipsetInitialize.UseVisualStyleBackColor = true;
            // 
            // grpChipInit
            // 
            this.grpChipInit.Controls.Add(this.label2);
            this.grpChipInit.Controls.Add(this.cboChipInitEndianness);
            this.grpChipInit.Controls.Add(this.btnChipInitOK);
            this.grpChipInit.Controls.Add(this.label10);
            this.grpChipInit.Controls.Add(this.cboChipInitItemVaule);
            this.grpChipInit.Controls.Add(this.label11);
            this.grpChipInit.Controls.Add(this.cboChipInitLength);
            this.grpChipInit.Controls.Add(this.cboChipInitDriveType);
            this.grpChipInit.Controls.Add(this.label12);
            this.grpChipInit.Controls.Add(this.label13);
            this.grpChipInit.Controls.Add(this.label14);
            this.grpChipInit.Controls.Add(this.cboChipInitAddress);
            this.grpChipInit.Controls.Add(this.cboChipInitChipLine);
            this.grpChipInit.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grpChipInit.Location = new System.Drawing.Point(382, 7);
            this.grpChipInit.Name = "grpChipInit";
            this.grpChipInit.Size = new System.Drawing.Size(220, 402);
            this.grpChipInit.TabIndex = 56;
            this.grpChipInit.TabStop = false;
            this.grpChipInit.Text = "Current Initialize Item";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(3, 157);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 12);
            this.label2.TabIndex = 73;
            this.label2.Text = "Endianness?";
            // 
            // cboChipInitEndianness
            // 
            this.cboChipInitEndianness.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboChipInitEndianness.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboChipInitEndianness.FormattingEnabled = true;
            this.cboChipInitEndianness.Location = new System.Drawing.Point(90, 154);
            this.cboChipInitEndianness.Name = "cboChipInitEndianness";
            this.cboChipInitEndianness.Size = new System.Drawing.Size(125, 20);
            this.cboChipInitEndianness.TabIndex = 74;
            // 
            // btnChipInitOK
            // 
            this.btnChipInitOK.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnChipInitOK.Location = new System.Drawing.Point(90, 196);
            this.btnChipInitOK.Name = "btnChipInitOK";
            this.btnChipInitOK.Size = new System.Drawing.Size(42, 22);
            this.btnChipInitOK.TabIndex = 68;
            this.btnChipInitOK.Text = "Save";
            this.btnChipInitOK.UseVisualStyleBackColor = true;
            this.btnChipInitOK.Click += new System.EventHandler(this.btnChipInitOK_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label10.Location = new System.Drawing.Point(3, 131);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(59, 12);
            this.label10.TabIndex = 62;
            this.label10.Text = "ItemValue";
            // 
            // cboChipInitItemVaule
            // 
            this.cboChipInitItemVaule.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboChipInitItemVaule.FormattingEnabled = true;
            this.cboChipInitItemVaule.Location = new System.Drawing.Point(90, 128);
            this.cboChipInitItemVaule.Name = "cboChipInitItemVaule";
            this.cboChipInitItemVaule.Size = new System.Drawing.Size(125, 20);
            this.cboChipInitItemVaule.TabIndex = 63;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label11.Location = new System.Drawing.Point(3, 105);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(41, 12);
            this.label11.TabIndex = 60;
            this.label11.Text = "Length";
            // 
            // cboChipInitLength
            // 
            this.cboChipInitLength.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboChipInitLength.FormattingEnabled = true;
            this.cboChipInitLength.Location = new System.Drawing.Point(90, 102);
            this.cboChipInitLength.Name = "cboChipInitLength";
            this.cboChipInitLength.Size = new System.Drawing.Size(125, 20);
            this.cboChipInitLength.TabIndex = 61;
            // 
            // cboChipInitDriveType
            // 
            this.cboChipInitDriveType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboChipInitDriveType.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboChipInitDriveType.FormattingEnabled = true;
            this.cboChipInitDriveType.Location = new System.Drawing.Point(90, 21);
            this.cboChipInitDriveType.Name = "cboChipInitDriveType";
            this.cboChipInitDriveType.Size = new System.Drawing.Size(125, 20);
            this.cboChipInitDriveType.TabIndex = 50;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label12.Location = new System.Drawing.Point(3, 79);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(47, 12);
            this.label12.TabIndex = 49;
            this.label12.Text = "Address";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label13.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label13.Location = new System.Drawing.Point(3, 50);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(53, 12);
            this.label13.TabIndex = 55;
            this.label13.Text = "ChipLine";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label14.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label14.Location = new System.Drawing.Point(3, 24);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(59, 12);
            this.label14.TabIndex = 51;
            this.label14.Text = "DriveType";
            // 
            // cboChipInitAddress
            // 
            this.cboChipInitAddress.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboChipInitAddress.FormattingEnabled = true;
            this.cboChipInitAddress.Location = new System.Drawing.Point(90, 76);
            this.cboChipInitAddress.Name = "cboChipInitAddress";
            this.cboChipInitAddress.Size = new System.Drawing.Size(125, 20);
            this.cboChipInitAddress.TabIndex = 54;
            // 
            // cboChipInitChipLine
            // 
            this.cboChipInitChipLine.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboChipInitChipLine.FormattingEnabled = true;
            this.cboChipInitChipLine.Location = new System.Drawing.Point(90, 47);
            this.cboChipInitChipLine.Name = "cboChipInitChipLine";
            this.cboChipInitChipLine.Size = new System.Drawing.Size(125, 20);
            this.cboChipInitChipLine.TabIndex = 52;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnRemoveChipInit);
            this.groupBox4.Controls.Add(this.btnAddChipInit);
            this.groupBox4.Controls.Add(this.txtChipInit);
            this.groupBox4.Controls.Add(this.dgvChipInit);
            this.groupBox4.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox4.Location = new System.Drawing.Point(7, 7);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(369, 402);
            this.groupBox4.TabIndex = 55;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Initialize Info";
            // 
            // btnRemoveChipInit
            // 
            this.btnRemoveChipInit.BackColor = System.Drawing.Color.Fuchsia;
            this.btnRemoveChipInit.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnRemoveChipInit.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnRemoveChipInit.Location = new System.Drawing.Point(221, 341);
            this.btnRemoveChipInit.Name = "btnRemoveChipInit";
            this.btnRemoveChipInit.Size = new System.Drawing.Size(57, 25);
            this.btnRemoveChipInit.TabIndex = 62;
            this.btnRemoveChipInit.Text = "Delete";
            this.btnRemoveChipInit.UseVisualStyleBackColor = false;
            this.btnRemoveChipInit.Click += new System.EventHandler(this.btnRemoveChipInit_Click);
            // 
            // btnAddChipInit
            // 
            this.btnAddChipInit.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnAddChipInit.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAddChipInit.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnAddChipInit.Location = new System.Drawing.Point(97, 341);
            this.btnAddChipInit.Name = "btnAddChipInit";
            this.btnAddChipInit.Size = new System.Drawing.Size(57, 25);
            this.btnAddChipInit.TabIndex = 61;
            this.btnAddChipInit.Text = "Add";
            this.btnAddChipInit.UseVisualStyleBackColor = false;
            this.btnAddChipInit.Click += new System.EventHandler(this.btnAddChipInit_Click);
            // 
            // txtChipInit
            // 
            this.txtChipInit.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtChipInit.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtChipInit.Location = new System.Drawing.Point(6, 372);
            this.txtChipInit.Multiline = true;
            this.txtChipInit.Name = "txtChipInit";
            this.txtChipInit.ReadOnly = true;
            this.txtChipInit.Size = new System.Drawing.Size(357, 24);
            this.txtChipInit.TabIndex = 23;
            // 
            // dgvChipInit
            // 
            this.dgvChipInit.AllowUserToAddRows = false;
            this.dgvChipInit.AllowUserToDeleteRows = false;
            this.dgvChipInit.AllowUserToResizeColumns = false;
            this.dgvChipInit.AllowUserToResizeRows = false;
            this.dgvChipInit.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvChipInit.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvChipInit.Location = new System.Drawing.Point(6, 20);
            this.dgvChipInit.Name = "dgvChipInit";
            this.dgvChipInit.ReadOnly = true;
            this.dgvChipInit.RowHeadersVisible = false;
            this.dgvChipInit.RowTemplate.Height = 23;
            this.dgvChipInit.Size = new System.Drawing.Size(357, 318);
            this.dgvChipInit.TabIndex = 3;
            this.dgvChipInit.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvChipInit_CellMouseClick);
            // 
            // chkChipsetControl
            // 
            this.chkChipsetControl.AutoSize = true;
            this.chkChipsetControl.Enabled = false;
            this.chkChipsetControl.Location = new System.Drawing.Point(15, 5);
            this.chkChipsetControl.Name = "chkChipsetControl";
            this.chkChipsetControl.Size = new System.Drawing.Size(186, 16);
            this.chkChipsetControl.TabIndex = 55;
            this.chkChipsetControl.Text = "ChipsetControl data existed";
            this.chkChipsetControl.UseVisualStyleBackColor = true;
            // 
            // chkChipsetInitialize
            // 
            this.chkChipsetInitialize.AutoSize = true;
            this.chkChipsetInitialize.Enabled = false;
            this.chkChipsetInitialize.Location = new System.Drawing.Point(232, 5);
            this.chkChipsetInitialize.Name = "chkChipsetInitialize";
            this.chkChipsetInitialize.Size = new System.Drawing.Size(204, 16);
            this.chkChipsetInitialize.TabIndex = 56;
            this.chkChipsetInitialize.Text = "ChipsetInitialize data existed";
            this.chkChipsetInitialize.UseVisualStyleBackColor = true;
            // 
            // InitInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(642, 504);
            this.Controls.Add(this.chkChipsetInitialize);
            this.Controls.Add(this.chkChipsetControl);
            this.Controls.Add(this.tabInit);
            this.Controls.Add(this.btnPreviousPage);
            this.MaximumSize = new System.Drawing.Size(658, 542);
            this.Name = "InitInfoForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "InitInfoForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.InitInfoForm_FormClosing);
            this.Load += new System.EventHandler(this.InitInfoForm_Load);
            this.tabInit.ResumeLayout(false);
            this.tabChipsetControl.ResumeLayout(false);
            this.grpPrmtr.ResumeLayout(false);
            this.grpPrmtr.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.grpEquipPrmtr.ResumeLayout(false);
            this.grpEquipPrmtr.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChipCtrl)).EndInit();
            this.tabChipsetInitialize.ResumeLayout(false);
            this.grpChipInit.ResumeLayout(false);
            this.grpChipInit.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChipInit)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnPreviousPage;
        private System.Windows.Forms.TabControl tabInit;
        private System.Windows.Forms.TabPage tabChipsetControl;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox grpPrmtr;
        private System.Windows.Forms.Button btnChipCtrlOK;
        private System.Windows.Forms.Label lblLength;
        private System.Windows.Forms.ComboBox cboLength;
        private System.Windows.Forms.Label lblStartAddr;
        private System.Windows.Forms.ComboBox cboStartAddr;
        private System.Windows.Forms.Label lblDriveType;
        private System.Windows.Forms.ComboBox cboDriveType;
        private System.Windows.Forms.ComboBox cboItemName;
        private System.Windows.Forms.Label lblChipLine;
        private System.Windows.Forms.Label lblPrmtrValue;
        private System.Windows.Forms.Label lblPrmtrItem;
        private System.Windows.Forms.ComboBox cboChipLine;
        private System.Windows.Forms.ComboBox cboModelLine;
        private System.Windows.Forms.GroupBox grpEquipPrmtr;
        private System.Windows.Forms.Button btnChipCtrlDelete;
        private System.Windows.Forms.Button btnChipCtrlAdd;
        private System.Windows.Forms.TextBox txtSaveResult;
        private System.Windows.Forms.DataGridView dgvChipCtrl;
        private System.Windows.Forms.TabPage tabChipsetInitialize;
        private System.Windows.Forms.GroupBox grpChipInit;
        private System.Windows.Forms.Button btnChipInitOK;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cboChipInitItemVaule;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cboChipInitLength;
        private System.Windows.Forms.ComboBox cboChipInitDriveType;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox cboChipInitAddress;
        private System.Windows.Forms.ComboBox cboChipInitChipLine;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnRemoveChipInit;
        private System.Windows.Forms.Button btnAddChipInit;
        private System.Windows.Forms.TextBox txtChipInit;
        private System.Windows.Forms.DataGridView dgvChipInit;
        private System.Windows.Forms.CheckBox chkChipsetControl;
        private System.Windows.Forms.CheckBox chkChipsetInitialize;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboEndianness;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboChipInitEndianness;
    }
}
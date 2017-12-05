namespace GlobalInfo
{
    partial class EquipmentForm
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
            this.btnPrmtrAdd = new System.Windows.Forms.Button();
            this.btnPrmtrDelete = new System.Windows.Forms.Button();
            this.txtSaveResult = new System.Windows.Forms.TextBox();
            this.dgvEquipPrmtr = new System.Windows.Forms.DataGridView();
            this.lblGlobal = new System.Windows.Forms.Label();
            this.grpItem = new System.Windows.Forms.GroupBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtEquipDescription = new System.Windows.Forms.TextBox();
            this.lblEquipDescription = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cboName = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cboType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnPrmtrSave = new System.Windows.Forms.Button();
            this.lblPrmtrValue = new System.Windows.Forms.Label();
            this.cboPrmtrType = new System.Windows.Forms.ComboBox();
            this.lblPrmtrDescription = new System.Windows.Forms.Label();
            this.cboPrmtrValue = new System.Windows.Forms.ComboBox();
            this.lblPrmtrItem = new System.Windows.Forms.Label();
            this.cboPrmtrItem = new System.Windows.Forms.ComboBox();
            this.lblPrmtrType = new System.Windows.Forms.Label();
            this.txtEquipPrmtrDescription = new System.Windows.Forms.TextBox();
            this.currlst = new System.Windows.Forms.ListBox();
            this.btnPreviousPage = new System.Windows.Forms.Button();
            this.grpPrmtr = new System.Windows.Forms.GroupBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.grpEquipPrmtr.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEquipPrmtr)).BeginInit();
            this.grpItem.SuspendLayout();
            this.grpPrmtr.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpEquipPrmtr
            // 
            this.grpEquipPrmtr.Controls.Add(this.btnPrmtrAdd);
            this.grpEquipPrmtr.Controls.Add(this.btnPrmtrDelete);
            this.grpEquipPrmtr.Controls.Add(this.txtSaveResult);
            this.grpEquipPrmtr.Controls.Add(this.dgvEquipPrmtr);
            this.grpEquipPrmtr.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grpEquipPrmtr.Location = new System.Drawing.Point(247, 12);
            this.grpEquipPrmtr.Name = "grpEquipPrmtr";
            this.grpEquipPrmtr.Size = new System.Drawing.Size(369, 402);
            this.grpEquipPrmtr.TabIndex = 45;
            this.grpEquipPrmtr.TabStop = false;
            this.grpEquipPrmtr.Text = "EquipmentParameters";
            this.grpEquipPrmtr.Enter += new System.EventHandler(this.grpEquipPrmtr_Enter);
            // 
            // btnPrmtrAdd
            // 
            this.btnPrmtrAdd.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnPrmtrAdd.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPrmtrAdd.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnPrmtrAdd.Location = new System.Drawing.Point(58, 340);
            this.btnPrmtrAdd.Name = "btnPrmtrAdd";
            this.btnPrmtrAdd.Size = new System.Drawing.Size(100, 26);
            this.btnPrmtrAdd.TabIndex = 61;
            this.btnPrmtrAdd.Text = "AddPrmtr";
            this.btnPrmtrAdd.UseVisualStyleBackColor = false;
            this.btnPrmtrAdd.Click += new System.EventHandler(this.btnPrmtrAdd_Click);
            // 
            // btnPrmtrDelete
            // 
            this.btnPrmtrDelete.BackColor = System.Drawing.Color.Fuchsia;
            this.btnPrmtrDelete.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPrmtrDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnPrmtrDelete.Location = new System.Drawing.Point(208, 340);
            this.btnPrmtrDelete.Name = "btnPrmtrDelete";
            this.btnPrmtrDelete.Size = new System.Drawing.Size(95, 26);
            this.btnPrmtrDelete.TabIndex = 60;
            this.btnPrmtrDelete.Text = "RemovePrmtr";
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
            // dgvEquipPrmtr
            // 
            this.dgvEquipPrmtr.AllowUserToAddRows = false;
            this.dgvEquipPrmtr.AllowUserToDeleteRows = false;
            this.dgvEquipPrmtr.AllowUserToResizeColumns = false;
            this.dgvEquipPrmtr.AllowUserToResizeRows = false;
            this.dgvEquipPrmtr.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvEquipPrmtr.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEquipPrmtr.Location = new System.Drawing.Point(6, 20);
            this.dgvEquipPrmtr.Name = "dgvEquipPrmtr";
            this.dgvEquipPrmtr.ReadOnly = true;
            this.dgvEquipPrmtr.RowHeadersVisible = false;
            this.dgvEquipPrmtr.RowTemplate.Height = 23;
            this.dgvEquipPrmtr.Size = new System.Drawing.Size(357, 315);
            this.dgvEquipPrmtr.TabIndex = 3;
            this.dgvEquipPrmtr.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvEquipPrmtr_CellMouseClick);
            this.dgvEquipPrmtr.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvEquipPrmtr_ColumnHeaderMouseClick);
            // 
            // lblGlobal
            // 
            this.lblGlobal.AutoSize = true;
            this.lblGlobal.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblGlobal.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblGlobal.Location = new System.Drawing.Point(74, 9);
            this.lblGlobal.Name = "lblGlobal";
            this.lblGlobal.Size = new System.Drawing.Size(114, 19);
            this.lblGlobal.TabIndex = 44;
            this.lblGlobal.Text = "Equipment List";
            // 
            // grpItem
            // 
            this.grpItem.Controls.Add(this.btnSave);
            this.grpItem.Controls.Add(this.txtEquipDescription);
            this.grpItem.Controls.Add(this.lblEquipDescription);
            this.grpItem.Controls.Add(this.label6);
            this.grpItem.Controls.Add(this.label5);
            this.grpItem.Controls.Add(this.cboName);
            this.grpItem.Controls.Add(this.label2);
            this.grpItem.Controls.Add(this.cboType);
            this.grpItem.Controls.Add(this.label1);
            this.grpItem.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grpItem.Location = new System.Drawing.Point(16, 257);
            this.grpItem.Name = "grpItem";
            this.grpItem.Size = new System.Drawing.Size(225, 174);
            this.grpItem.TabIndex = 37;
            this.grpItem.TabStop = false;
            this.grpItem.Text = "Equipment Info";
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnSave.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSave.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnSave.Location = new System.Drawing.Point(94, 146);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(48, 23);
            this.btnSave.TabIndex = 48;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // txtEquipDescription
            // 
            this.txtEquipDescription.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtEquipDescription.Location = new System.Drawing.Point(78, 96);
            this.txtEquipDescription.Multiline = true;
            this.txtEquipDescription.Name = "txtEquipDescription";
            this.txtEquipDescription.Size = new System.Drawing.Size(142, 44);
            this.txtEquipDescription.TabIndex = 47;
            // 
            // lblEquipDescription
            // 
            this.lblEquipDescription.AutoSize = true;
            this.lblEquipDescription.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblEquipDescription.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblEquipDescription.Location = new System.Drawing.Point(7, 96);
            this.lblEquipDescription.Name = "lblEquipDescription";
            this.lblEquipDescription.Size = new System.Drawing.Size(71, 12);
            this.lblEquipDescription.TabIndex = 46;
            this.lblEquipDescription.Text = "Description";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label6.Location = new System.Drawing.Point(107, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 12);
            this.label6.TabIndex = 9;
            this.label6.Text = "Value";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label5.Location = new System.Drawing.Point(7, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "Item";
            // 
            // cboName
            // 
            this.cboName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboName.FormattingEnabled = true;
            this.cboName.Location = new System.Drawing.Point(78, 70);
            this.cboName.Name = "cboName";
            this.cboName.Size = new System.Drawing.Size(142, 20);
            this.cboName.TabIndex = 3;
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
            this.label2.Text = "EquipName";
            // 
            // cboType
            // 
            this.cboType.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboType.FormattingEnabled = true;
            this.cboType.Location = new System.Drawing.Point(78, 44);
            this.cboType.Name = "cboType";
            this.cboType.Size = new System.Drawing.Size(142, 20);
            this.cboType.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(7, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "EquipType";
            // 
            // btnPrmtrSave
            // 
            this.btnPrmtrSave.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPrmtrSave.Location = new System.Drawing.Point(73, 234);
            this.btnPrmtrSave.Name = "btnPrmtrSave";
            this.btnPrmtrSave.Size = new System.Drawing.Size(83, 22);
            this.btnPrmtrSave.TabIndex = 57;
            this.btnPrmtrSave.Text = "SavePrmtr";
            this.btnPrmtrSave.UseVisualStyleBackColor = true;
            this.btnPrmtrSave.Click += new System.EventHandler(this.btnEditPrmtrOK_Click);
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
            this.lblPrmtrValue.Text = "ItemValue";
            // 
            // cboPrmtrType
            // 
            this.cboPrmtrType.Enabled = false;
            this.cboPrmtrType.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboPrmtrType.FormattingEnabled = true;
            this.cboPrmtrType.Location = new System.Drawing.Point(61, 76);
            this.cboPrmtrType.Name = "cboPrmtrType";
            this.cboPrmtrType.Size = new System.Drawing.Size(154, 20);
            this.cboPrmtrType.TabIndex = 54;
            // 
            // lblPrmtrDescription
            // 
            this.lblPrmtrDescription.AutoSize = true;
            this.lblPrmtrDescription.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPrmtrDescription.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblPrmtrDescription.Location = new System.Drawing.Point(3, 109);
            this.lblPrmtrDescription.Name = "lblPrmtrDescription";
            this.lblPrmtrDescription.Size = new System.Drawing.Size(71, 12);
            this.lblPrmtrDescription.TabIndex = 53;
            this.lblPrmtrDescription.Text = "Description";
            // 
            // cboPrmtrValue
            // 
            this.cboPrmtrValue.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboPrmtrValue.FormattingEnabled = true;
            this.cboPrmtrValue.Location = new System.Drawing.Point(61, 47);
            this.cboPrmtrValue.Name = "cboPrmtrValue";
            this.cboPrmtrValue.Size = new System.Drawing.Size(154, 20);
            this.cboPrmtrValue.TabIndex = 52;
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
            this.lblPrmtrItem.Text = "PrmtrName";
            // 
            // cboPrmtrItem
            // 
            this.cboPrmtrItem.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboPrmtrItem.FormattingEnabled = true;
            this.cboPrmtrItem.Location = new System.Drawing.Point(61, 21);
            this.cboPrmtrItem.Name = "cboPrmtrItem";
            this.cboPrmtrItem.Size = new System.Drawing.Size(154, 20);
            this.cboPrmtrItem.TabIndex = 50;
            // 
            // lblPrmtrType
            // 
            this.lblPrmtrType.AutoSize = true;
            this.lblPrmtrType.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPrmtrType.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblPrmtrType.Location = new System.Drawing.Point(3, 79);
            this.lblPrmtrType.Name = "lblPrmtrType";
            this.lblPrmtrType.Size = new System.Drawing.Size(59, 12);
            this.lblPrmtrType.TabIndex = 49;
            this.lblPrmtrType.Text = "EquipType";
            // 
            // txtEquipPrmtrDescription
            // 
            this.txtEquipPrmtrDescription.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtEquipPrmtrDescription.Location = new System.Drawing.Point(6, 124);
            this.txtEquipPrmtrDescription.Multiline = true;
            this.txtEquipPrmtrDescription.Name = "txtEquipPrmtrDescription";
            this.txtEquipPrmtrDescription.Size = new System.Drawing.Size(205, 104);
            this.txtEquipPrmtrDescription.TabIndex = 19;
            // 
            // currlst
            // 
            this.currlst.FormattingEnabled = true;
            this.currlst.HorizontalScrollbar = true;
            this.currlst.ItemHeight = 12;
            this.currlst.Items.AddRange(new object[] {
            "Equip1",
            "Equip2",
            "Equip3",
            "Equip4",
            "...",
            "EquipNew"});
            this.currlst.Location = new System.Drawing.Point(16, 31);
            this.currlst.Name = "currlst";
            this.currlst.ScrollAlwaysVisible = true;
            this.currlst.Size = new System.Drawing.Size(225, 196);
            this.currlst.TabIndex = 36;
            this.currlst.SelectedIndexChanged += new System.EventHandler(this.currlst_SelectedIndexChanged);
            // 
            // btnPreviousPage
            // 
            this.btnPreviousPage.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnPreviousPage.Location = new System.Drawing.Point(370, 436);
            this.btnPreviousPage.Name = "btnPreviousPage";
            this.btnPreviousPage.Size = new System.Drawing.Size(114, 24);
            this.btnPreviousPage.TabIndex = 40;
            this.btnPreviousPage.Text = "Finish";
            this.btnPreviousPage.UseVisualStyleBackColor = true;
            this.btnPreviousPage.Click += new System.EventHandler(this.btnPreviousPage_Click);
            // 
            // grpPrmtr
            // 
            this.grpPrmtr.Controls.Add(this.cboPrmtrItem);
            this.grpPrmtr.Controls.Add(this.btnPrmtrSave);
            this.grpPrmtr.Controls.Add(this.txtEquipPrmtrDescription);
            this.grpPrmtr.Controls.Add(this.lblPrmtrType);
            this.grpPrmtr.Controls.Add(this.lblPrmtrValue);
            this.grpPrmtr.Controls.Add(this.lblPrmtrItem);
            this.grpPrmtr.Controls.Add(this.cboPrmtrType);
            this.grpPrmtr.Controls.Add(this.cboPrmtrValue);
            this.grpPrmtr.Controls.Add(this.lblPrmtrDescription);
            this.grpPrmtr.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grpPrmtr.Location = new System.Drawing.Point(622, 12);
            this.grpPrmtr.Name = "grpPrmtr";
            this.grpPrmtr.Size = new System.Drawing.Size(220, 402);
            this.grpPrmtr.TabIndex = 47;
            this.grpPrmtr.TabStop = false;
            this.grpPrmtr.Text = "CurrentParameter";
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnAdd.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAdd.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnAdd.Location = new System.Drawing.Point(47, 228);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(69, 23);
            this.btnAdd.TabIndex = 49;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.BackColor = System.Drawing.Color.Fuchsia;
            this.btnRemove.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnRemove.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnRemove.Location = new System.Drawing.Point(152, 228);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(64, 23);
            this.btnRemove.TabIndex = 48;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = false;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // EquipmentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(845, 462);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.grpPrmtr);
            this.Controls.Add(this.grpEquipPrmtr);
            this.Controls.Add(this.lblGlobal);
            this.Controls.Add(this.btnPreviousPage);
            this.Controls.Add(this.grpItem);
            this.Controls.Add(this.currlst);
            this.MaximumSize = new System.Drawing.Size(1400, 500);
            this.Name = "EquipmentForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Equipment";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EquipmentForm_FormClosing);
            this.Load += new System.EventHandler(this.EquipmentForm_Load);
            this.grpEquipPrmtr.ResumeLayout(false);
            this.grpEquipPrmtr.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEquipPrmtr)).EndInit();
            this.grpItem.ResumeLayout(false);
            this.grpItem.PerformLayout();
            this.grpPrmtr.ResumeLayout(false);
            this.grpPrmtr.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpEquipPrmtr;
        private System.Windows.Forms.DataGridView dgvEquipPrmtr;
        private System.Windows.Forms.Label lblGlobal;
        private System.Windows.Forms.GroupBox grpItem;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox currlst;
        private System.Windows.Forms.TextBox txtEquipPrmtrDescription;
        private System.Windows.Forms.TextBox txtSaveResult;
        private System.Windows.Forms.TextBox txtEquipDescription;
        private System.Windows.Forms.Label lblEquipDescription;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblPrmtrValue;
        private System.Windows.Forms.ComboBox cboPrmtrType;
        private System.Windows.Forms.Label lblPrmtrDescription;
        private System.Windows.Forms.ComboBox cboPrmtrValue;
        private System.Windows.Forms.Label lblPrmtrItem;
        private System.Windows.Forms.ComboBox cboPrmtrItem;
        private System.Windows.Forms.Label lblPrmtrType;
        private System.Windows.Forms.Button btnPrmtrSave;
        private System.Windows.Forms.Button btnPreviousPage;
        private System.Windows.Forms.GroupBox grpPrmtr;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnPrmtrAdd;
        private System.Windows.Forms.Button btnPrmtrDelete;
    }
}
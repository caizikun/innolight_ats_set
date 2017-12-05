namespace TestPlan
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
            this.txtSaveResult = new System.Windows.Forms.TextBox();
            this.txtDGVItem = new System.Windows.Forms.TextBox();
            this.btnEditPrmtrOK = new System.Windows.Forms.Button();
            this.dgvEquipPrmtr = new System.Windows.Forms.DataGridView();
            this.lblGlobal = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.globalistName = new System.Windows.Forms.ListBox();
            this.lblEquip = new System.Windows.Forms.Label();
            this.btnPreviousPage = new System.Windows.Forms.Button();
            this.btnNextPage = new System.Windows.Forms.Button();
            this.grpItem = new System.Windows.Forms.GroupBox();
            this.cboFunc = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblMyType = new System.Windows.Forms.Label();
            this.lblPrmtrType = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtTestPlanName = new System.Windows.Forms.TextBox();
            this.cboItemName = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cboItemType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.currlst = new System.Windows.Forms.ListBox();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.grpEquipPrmtr.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEquipPrmtr)).BeginInit();
            this.grpItem.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpEquipPrmtr
            // 
            this.grpEquipPrmtr.Controls.Add(this.txtSaveResult);
            this.grpEquipPrmtr.Controls.Add(this.txtDGVItem);
            this.grpEquipPrmtr.Controls.Add(this.btnEditPrmtrOK);
            this.grpEquipPrmtr.Controls.Add(this.dgvEquipPrmtr);
            this.grpEquipPrmtr.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grpEquipPrmtr.Location = new System.Drawing.Point(569, 17);
            this.grpEquipPrmtr.Name = "grpEquipPrmtr";
            this.grpEquipPrmtr.Size = new System.Drawing.Size(322, 347);
            this.grpEquipPrmtr.TabIndex = 45;
            this.grpEquipPrmtr.TabStop = false;
            this.grpEquipPrmtr.Text = "Parameter Info of Equipment";
            // 
            // txtSaveResult
            // 
            this.txtSaveResult.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtSaveResult.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtSaveResult.Location = new System.Drawing.Point(76, 279);
            this.txtSaveResult.Multiline = true;
            this.txtSaveResult.Name = "txtSaveResult";
            this.txtSaveResult.ReadOnly = true;
            this.txtSaveResult.Size = new System.Drawing.Size(240, 62);
            this.txtSaveResult.TabIndex = 23;
            // 
            // txtDGVItem
            // 
            this.txtDGVItem.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtDGVItem.Location = new System.Drawing.Point(123, 23);
            this.txtDGVItem.Multiline = true;
            this.txtDGVItem.Name = "txtDGVItem";
            this.txtDGVItem.Size = new System.Drawing.Size(124, 40);
            this.txtDGVItem.TabIndex = 22;
            this.txtDGVItem.Visible = false;
            this.txtDGVItem.Leave += new System.EventHandler(this.txtDGVItem_Leave);
            // 
            // btnEditPrmtrOK
            // 
            this.btnEditPrmtrOK.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnEditPrmtrOK.Location = new System.Drawing.Point(6, 295);
            this.btnEditPrmtrOK.Name = "btnEditPrmtrOK";
            this.btnEditPrmtrOK.Size = new System.Drawing.Size(64, 23);
            this.btnEditPrmtrOK.TabIndex = 20;
            this.btnEditPrmtrOK.Text = "Save";
            this.btnEditPrmtrOK.UseVisualStyleBackColor = true;
            this.btnEditPrmtrOK.Click += new System.EventHandler(this.btnEditPrmtrOK_Click);
            // 
            // dgvEquipPrmtr
            // 
            this.dgvEquipPrmtr.AllowUserToAddRows = false;
            this.dgvEquipPrmtr.AllowUserToDeleteRows = false;
            this.dgvEquipPrmtr.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEquipPrmtr.Location = new System.Drawing.Point(6, 20);
            this.dgvEquipPrmtr.Name = "dgvEquipPrmtr";
            this.dgvEquipPrmtr.ReadOnly = true;
            this.dgvEquipPrmtr.RowHeadersVisible = false;
            this.dgvEquipPrmtr.RowTemplate.Height = 23;
            this.dgvEquipPrmtr.Size = new System.Drawing.Size(312, 253);
            this.dgvEquipPrmtr.TabIndex = 3;
            this.dgvEquipPrmtr.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvEquipPrmtr_CellMouseClick);
            this.dgvEquipPrmtr.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvEquipPrmtr_ColumnHeaderMouseClick);
            this.dgvEquipPrmtr.RowHeightChanged += new System.Windows.Forms.DataGridViewRowEventHandler(this.dgvEquipPrmtr_RowHeightChanged);
            this.dgvEquipPrmtr.Scroll += new System.Windows.Forms.ScrollEventHandler(this.dgvEquipPrmtr_Scroll);
            // 
            // lblGlobal
            // 
            this.lblGlobal.AutoSize = true;
            this.lblGlobal.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblGlobal.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblGlobal.Location = new System.Drawing.Point(-1, 2);
            this.lblGlobal.Name = "lblGlobal";
            this.lblGlobal.Size = new System.Drawing.Size(170, 19);
            this.lblGlobal.TabIndex = 44;
            this.lblGlobal.Text = "Global Equipments List";
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnAdd.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnAdd.Location = new System.Drawing.Point(158, 101);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(64, 23);
            this.btnAdd.TabIndex = 43;
            this.btnAdd.Text = "Add=>";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // globalistName
            // 
            this.globalistName.FormattingEnabled = true;
            this.globalistName.HorizontalScrollbar = true;
            this.globalistName.ItemHeight = 12;
            this.globalistName.Location = new System.Drawing.Point(12, 24);
            this.globalistName.Name = "globalistName";
            this.globalistName.ScrollAlwaysVisible = true;
            this.globalistName.Size = new System.Drawing.Size(143, 340);
            this.globalistName.TabIndex = 42;
            // 
            // lblEquip
            // 
            this.lblEquip.AutoSize = true;
            this.lblEquip.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblEquip.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblEquip.Location = new System.Drawing.Point(221, 2);
            this.lblEquip.Name = "lblEquip";
            this.lblEquip.Size = new System.Drawing.Size(155, 19);
            this.lblEquip.TabIndex = 41;
            this.lblEquip.Text = "Curr Equipments List";
            // 
            // btnPreviousPage
            // 
            this.btnPreviousPage.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnPreviousPage.Location = new System.Drawing.Point(292, 383);
            this.btnPreviousPage.Name = "btnPreviousPage";
            this.btnPreviousPage.Size = new System.Drawing.Size(114, 33);
            this.btnPreviousPage.TabIndex = 40;
            this.btnPreviousPage.Text = "Finish";
            this.btnPreviousPage.UseVisualStyleBackColor = true;
            this.btnPreviousPage.Click += new System.EventHandler(this.btnPreviousPage_Click);
            // 
            // btnNextPage
            // 
            this.btnNextPage.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnNextPage.Location = new System.Drawing.Point(469, 383);
            this.btnNextPage.Name = "btnNextPage";
            this.btnNextPage.Size = new System.Drawing.Size(104, 33);
            this.btnNextPage.TabIndex = 39;
            this.btnNextPage.Text = "Next <FlowCtrl>";
            this.btnNextPage.UseVisualStyleBackColor = true;
            this.btnNextPage.Click += new System.EventHandler(this.btnNextPage_Click);
            // 
            // grpItem
            // 
            this.grpItem.Controls.Add(this.cboFunc);
            this.grpItem.Controls.Add(this.label3);
            this.grpItem.Controls.Add(this.txtDescription);
            this.grpItem.Controls.Add(this.lblMyType);
            this.grpItem.Controls.Add(this.lblPrmtrType);
            this.grpItem.Controls.Add(this.label10);
            this.grpItem.Controls.Add(this.txtTestPlanName);
            this.grpItem.Controls.Add(this.cboItemName);
            this.grpItem.Controls.Add(this.label2);
            this.grpItem.Controls.Add(this.cboItemType);
            this.grpItem.Controls.Add(this.label1);
            this.grpItem.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grpItem.Location = new System.Drawing.Point(374, 17);
            this.grpItem.Name = "grpItem";
            this.grpItem.Size = new System.Drawing.Size(189, 347);
            this.grpItem.TabIndex = 37;
            this.grpItem.TabStop = false;
            this.grpItem.Text = "ItemInfo";
            // 
            // cboFunc
            // 
            this.cboFunc.BackColor = System.Drawing.Color.Yellow;
            this.cboFunc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFunc.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboFunc.FormattingEnabled = true;
            this.cboFunc.Items.AddRange(new object[] {
            "NA",
            "TX",
            "RX"});
            this.cboFunc.Location = new System.Drawing.Point(56, 115);
            this.cboFunc.Name = "cboFunc";
            this.cboFunc.Size = new System.Drawing.Size(124, 20);
            this.cboFunc.TabIndex = 49;
            this.cboFunc.SelectedIndexChanged += new System.EventHandler(this.cboFunc_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(3, 118);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 48;
            this.label3.Text = "Role";
            // 
            // txtDescription
            // 
            this.txtDescription.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtDescription.Location = new System.Drawing.Point(6, 156);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ReadOnly = true;
            this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtDescription.Size = new System.Drawing.Size(174, 187);
            this.txtDescription.TabIndex = 19;
            this.txtDescription.Text = "ItemDescription";
            // 
            // lblMyType
            // 
            this.lblMyType.AutoSize = true;
            this.lblMyType.Font = new System.Drawing.Font("新宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblMyType.Location = new System.Drawing.Point(98, 135);
            this.lblMyType.Name = "lblMyType";
            this.lblMyType.Size = new System.Drawing.Size(71, 16);
            this.lblMyType.TabIndex = 18;
            this.lblMyType.Text = "strType";
            // 
            // lblPrmtrType
            // 
            this.lblPrmtrType.AutoSize = true;
            this.lblPrmtrType.Font = new System.Drawing.Font("新宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPrmtrType.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblPrmtrType.Location = new System.Drawing.Point(1, 135);
            this.lblPrmtrType.Name = "lblPrmtrType";
            this.lblPrmtrType.Size = new System.Drawing.Size(107, 16);
            this.lblPrmtrType.TabIndex = 17;
            this.lblPrmtrType.Text = "PrmtrType: ";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label10.Location = new System.Drawing.Point(3, 23);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(77, 12);
            this.label10.TabIndex = 13;
            this.label10.Text = "TestPlanName";
            // 
            // txtTestPlanName
            // 
            this.txtTestPlanName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTestPlanName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtTestPlanName.Enabled = false;
            this.txtTestPlanName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtTestPlanName.Location = new System.Drawing.Point(80, 20);
            this.txtTestPlanName.Name = "txtTestPlanName";
            this.txtTestPlanName.Size = new System.Drawing.Size(89, 21);
            this.txtTestPlanName.TabIndex = 12;
            this.txtTestPlanName.Text = "TestPlan";
            this.txtTestPlanName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // cboItemName
            // 
            this.cboItemName.Enabled = false;
            this.cboItemName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboItemName.FormattingEnabled = true;
            this.cboItemName.Location = new System.Drawing.Point(56, 92);
            this.cboItemName.Name = "cboItemName";
            this.cboItemName.Size = new System.Drawing.Size(124, 20);
            this.cboItemName.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(3, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "ItemName";
            // 
            // cboItemType
            // 
            this.cboItemType.Enabled = false;
            this.cboItemType.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboItemType.FormattingEnabled = true;
            this.cboItemType.Location = new System.Drawing.Point(56, 66);
            this.cboItemType.Name = "cboItemType";
            this.cboItemType.Size = new System.Drawing.Size(124, 20);
            this.cboItemType.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(3, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "ItemType";
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
            this.currlst.Location = new System.Drawing.Point(228, 24);
            this.currlst.Name = "currlst";
            this.currlst.ScrollAlwaysVisible = true;
            this.currlst.Size = new System.Drawing.Size(140, 340);
            this.currlst.TabIndex = 36;
            this.currlst.SelectedIndexChanged += new System.EventHandler(this.currlst_SelectedIndexChanged);
            // 
            // btnRemove
            // 
            this.btnRemove.BackColor = System.Drawing.Color.Fuchsia;
            this.btnRemove.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnRemove.Location = new System.Drawing.Point(158, 258);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(64, 23);
            this.btnRemove.TabIndex = 35;
            this.btnRemove.Text = "<=Remove";
            this.btnRemove.UseVisualStyleBackColor = false;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnDown
            // 
            this.btnDown.Location = new System.Drawing.Point(185, 197);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(31, 23);
            this.btnDown.TabIndex = 49;
            this.btnDown.Text = "∨";
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // btnUp
            // 
            this.btnUp.Location = new System.Drawing.Point(185, 153);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(31, 23);
            this.btnUp.TabIndex = 48;
            this.btnUp.Text = "∧";
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // EquipmentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(893, 420);
            this.Controls.Add(this.btnDown);
            this.Controls.Add(this.btnUp);
            this.Controls.Add(this.grpEquipPrmtr);
            this.Controls.Add(this.lblGlobal);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.globalistName);
            this.Controls.Add(this.lblEquip);
            this.Controls.Add(this.btnPreviousPage);
            this.Controls.Add(this.btnNextPage);
            this.Controls.Add(this.grpItem);
            this.Controls.Add(this.currlst);
            this.Controls.Add(this.btnRemove);
            this.MaximumSize = new System.Drawing.Size(909, 500);
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
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpEquipPrmtr;
        private System.Windows.Forms.DataGridView dgvEquipPrmtr;
        private System.Windows.Forms.Label lblGlobal;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.ListBox globalistName;
        private System.Windows.Forms.Label lblEquip;
        private System.Windows.Forms.Button btnPreviousPage;
        private System.Windows.Forms.Button btnNextPage;
        private System.Windows.Forms.GroupBox grpItem;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtTestPlanName;
        private System.Windows.Forms.ComboBox cboItemName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboItemType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox currlst;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnEditPrmtrOK;
        private System.Windows.Forms.TextBox txtDGVItem;
        private System.Windows.Forms.Label lblPrmtrType;
        private System.Windows.Forms.Label lblMyType;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.TextBox txtSaveResult;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.ComboBox cboFunc;
        private System.Windows.Forms.Label label3;
    }
}
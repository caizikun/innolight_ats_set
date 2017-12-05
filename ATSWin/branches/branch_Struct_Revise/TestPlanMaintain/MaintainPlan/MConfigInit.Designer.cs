namespace Maintain
{
    partial class MConfigInit
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
            this.grpMConfigInit = new System.Windows.Forms.GroupBox();
            this.btnMConfigInitDelete = new System.Windows.Forms.Button();
            this.btnMConfigInitAdd = new System.Windows.Forms.Button();
            this.txtSaveResult = new System.Windows.Forms.TextBox();
            this.dgvMConfigInit = new System.Windows.Forms.DataGridView();
            this.btnPreviousPage = new System.Windows.Forms.Button();
            this.grpParams = new System.Windows.Forms.GroupBox();
            this.btnEEPROMInitOK = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.CboMConfigInitValue = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.CboMConfigInitLen = new System.Windows.Forms.ComboBox();
            this.CboMConfigInitSlaveAddr = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.CboMConfigInitStartAddr = new System.Windows.Forms.ComboBox();
            this.CboMConfigInitPage = new System.Windows.Forms.ComboBox();
            this.grpMConfigInit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMConfigInit)).BeginInit();
            this.grpParams.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpMConfigInit
            // 
            this.grpMConfigInit.Controls.Add(this.btnMConfigInitDelete);
            this.grpMConfigInit.Controls.Add(this.btnMConfigInitAdd);
            this.grpMConfigInit.Controls.Add(this.txtSaveResult);
            this.grpMConfigInit.Controls.Add(this.dgvMConfigInit);
            this.grpMConfigInit.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grpMConfigInit.Location = new System.Drawing.Point(3, 3);
            this.grpMConfigInit.Name = "grpMConfigInit";
            this.grpMConfigInit.Size = new System.Drawing.Size(369, 402);
            this.grpMConfigInit.TabIndex = 60;
            this.grpMConfigInit.TabStop = false;
            this.grpMConfigInit.Text = "ChipSet Info";
            // 
            // btnMConfigInitDelete
            // 
            this.btnMConfigInitDelete.BackColor = System.Drawing.Color.Fuchsia;
            this.btnMConfigInitDelete.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnMConfigInitDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMConfigInitDelete.Location = new System.Drawing.Point(216, 341);
            this.btnMConfigInitDelete.Name = "btnMConfigInitDelete";
            this.btnMConfigInitDelete.Size = new System.Drawing.Size(62, 25);
            this.btnMConfigInitDelete.TabIndex = 62;
            this.btnMConfigInitDelete.Text = "Delete";
            this.btnMConfigInitDelete.UseVisualStyleBackColor = false;
            this.btnMConfigInitDelete.Click += new System.EventHandler(this.btnMConfigInitDelete_Click);
            // 
            // btnMConfigInitAdd
            // 
            this.btnMConfigInitAdd.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnMConfigInitAdd.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnMConfigInitAdd.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMConfigInitAdd.Location = new System.Drawing.Point(92, 341);
            this.btnMConfigInitAdd.Name = "btnMConfigInitAdd";
            this.btnMConfigInitAdd.Size = new System.Drawing.Size(62, 25);
            this.btnMConfigInitAdd.TabIndex = 61;
            this.btnMConfigInitAdd.Text = "Add";
            this.btnMConfigInitAdd.UseVisualStyleBackColor = false;
            this.btnMConfigInitAdd.Click += new System.EventHandler(this.btnMConfigInitAdd_Click);
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
            // dgvMConfigInit
            // 
            this.dgvMConfigInit.AllowUserToAddRows = false;
            this.dgvMConfigInit.AllowUserToDeleteRows = false;
            this.dgvMConfigInit.AllowUserToResizeColumns = false;
            this.dgvMConfigInit.AllowUserToResizeRows = false;
            this.dgvMConfigInit.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvMConfigInit.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMConfigInit.Location = new System.Drawing.Point(6, 20);
            this.dgvMConfigInit.Name = "dgvMConfigInit";
            this.dgvMConfigInit.ReadOnly = true;
            this.dgvMConfigInit.RowHeadersVisible = false;
            this.dgvMConfigInit.RowTemplate.Height = 23;
            this.dgvMConfigInit.Size = new System.Drawing.Size(357, 318);
            this.dgvMConfigInit.TabIndex = 3;
            this.dgvMConfigInit.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvMConfigInit_CellMouseClick);
            this.dgvMConfigInit.CurrentCellChanged += new System.EventHandler(this.dgvMConfigInit_CurrentCellChanged);
            // 
            // btnPreviousPage
            // 
            this.btnPreviousPage.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnPreviousPage.Location = new System.Drawing.Point(255, 431);
            this.btnPreviousPage.Name = "btnPreviousPage";
            this.btnPreviousPage.Size = new System.Drawing.Size(114, 24);
            this.btnPreviousPage.TabIndex = 59;
            this.btnPreviousPage.Text = "Finish";
            this.btnPreviousPage.UseVisualStyleBackColor = true;
            this.btnPreviousPage.Click += new System.EventHandler(this.btnPreviousPage_Click);
            // 
            // grpParams
            // 
            this.grpParams.Controls.Add(this.btnEEPROMInitOK);
            this.grpParams.Controls.Add(this.label4);
            this.grpParams.Controls.Add(this.CboMConfigInitValue);
            this.grpParams.Controls.Add(this.label5);
            this.grpParams.Controls.Add(this.CboMConfigInitLen);
            this.grpParams.Controls.Add(this.CboMConfigInitSlaveAddr);
            this.grpParams.Controls.Add(this.label6);
            this.grpParams.Controls.Add(this.label7);
            this.grpParams.Controls.Add(this.label8);
            this.grpParams.Controls.Add(this.CboMConfigInitStartAddr);
            this.grpParams.Controls.Add(this.CboMConfigInitPage);
            this.grpParams.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grpParams.Location = new System.Drawing.Point(378, 3);
            this.grpParams.Name = "grpParams";
            this.grpParams.Size = new System.Drawing.Size(220, 402);
            this.grpParams.TabIndex = 61;
            this.grpParams.TabStop = false;
            this.grpParams.Text = "Current Item";
            // 
            // btnEEPROMInitOK
            // 
            this.btnEEPROMInitOK.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnEEPROMInitOK.Location = new System.Drawing.Point(90, 196);
            this.btnEEPROMInitOK.Name = "btnEEPROMInitOK";
            this.btnEEPROMInitOK.Size = new System.Drawing.Size(42, 22);
            this.btnEEPROMInitOK.TabIndex = 68;
            this.btnEEPROMInitOK.Text = "Save";
            this.btnEEPROMInitOK.UseVisualStyleBackColor = true;
            this.btnEEPROMInitOK.Click += new System.EventHandler(this.btnEEPROMInitOK_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(3, 131);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 62;
            this.label4.Text = "ItemValue";
            // 
            // CboMConfigInitValue
            // 
            this.CboMConfigInitValue.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CboMConfigInitValue.FormattingEnabled = true;
            this.CboMConfigInitValue.Location = new System.Drawing.Point(90, 128);
            this.CboMConfigInitValue.Name = "CboMConfigInitValue";
            this.CboMConfigInitValue.Size = new System.Drawing.Size(125, 20);
            this.CboMConfigInitValue.TabIndex = 63;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label5.Location = new System.Drawing.Point(3, 105);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 60;
            this.label5.Text = "Length";
            // 
            // CboMConfigInitLen
            // 
            this.CboMConfigInitLen.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CboMConfigInitLen.FormattingEnabled = true;
            this.CboMConfigInitLen.Location = new System.Drawing.Point(90, 102);
            this.CboMConfigInitLen.Name = "CboMConfigInitLen";
            this.CboMConfigInitLen.Size = new System.Drawing.Size(125, 20);
            this.CboMConfigInitLen.TabIndex = 61;
            // 
            // CboMConfigInitSlaveAddr
            // 
            this.CboMConfigInitSlaveAddr.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CboMConfigInitSlaveAddr.FormattingEnabled = true;
            this.CboMConfigInitSlaveAddr.Location = new System.Drawing.Point(90, 21);
            this.CboMConfigInitSlaveAddr.Name = "CboMConfigInitSlaveAddr";
            this.CboMConfigInitSlaveAddr.Size = new System.Drawing.Size(125, 20);
            this.CboMConfigInitSlaveAddr.TabIndex = 50;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label6.Location = new System.Drawing.Point(3, 79);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 12);
            this.label6.TabIndex = 49;
            this.label6.Text = "StartAddress";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label7.Location = new System.Drawing.Point(3, 50);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 55;
            this.label7.Text = "Page";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label8.Location = new System.Drawing.Point(3, 24);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 12);
            this.label8.TabIndex = 51;
            this.label8.Text = "SlaveAddress";
            // 
            // CboMConfigInitStartAddr
            // 
            this.CboMConfigInitStartAddr.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CboMConfigInitStartAddr.FormattingEnabled = true;
            this.CboMConfigInitStartAddr.Location = new System.Drawing.Point(90, 76);
            this.CboMConfigInitStartAddr.Name = "CboMConfigInitStartAddr";
            this.CboMConfigInitStartAddr.Size = new System.Drawing.Size(125, 20);
            this.CboMConfigInitStartAddr.TabIndex = 54;
            // 
            // CboMConfigInitPage
            // 
            this.CboMConfigInitPage.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CboMConfigInitPage.FormattingEnabled = true;
            this.CboMConfigInitPage.Location = new System.Drawing.Point(90, 47);
            this.CboMConfigInitPage.Name = "CboMConfigInitPage";
            this.CboMConfigInitPage.Size = new System.Drawing.Size(125, 20);
            this.CboMConfigInitPage.TabIndex = 52;
            // 
            // MConfigInit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(598, 463);
            this.Controls.Add(this.grpParams);
            this.Controls.Add(this.grpMConfigInit);
            this.Controls.Add(this.btnPreviousPage);
            this.MaximumSize = new System.Drawing.Size(614, 501);
            this.Name = "MConfigInit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MConfigInit";
            this.Load += new System.EventHandler(this.MConfigInit_Load);
            this.grpMConfigInit.ResumeLayout(false);
            this.grpMConfigInit.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMConfigInit)).EndInit();
            this.grpParams.ResumeLayout(false);
            this.grpParams.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpMConfigInit;
        private System.Windows.Forms.Button btnMConfigInitDelete;
        private System.Windows.Forms.Button btnMConfigInitAdd;
        private System.Windows.Forms.TextBox txtSaveResult;
        private System.Windows.Forms.DataGridView dgvMConfigInit;
        private System.Windows.Forms.Button btnPreviousPage;
        private System.Windows.Forms.GroupBox grpParams;
        private System.Windows.Forms.Button btnEEPROMInitOK;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox CboMConfigInitValue;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox CboMConfigInitLen;
        private System.Windows.Forms.ComboBox CboMConfigInitSlaveAddr;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox CboMConfigInitStartAddr;
        private System.Windows.Forms.ComboBox CboMConfigInitPage;
    }
}
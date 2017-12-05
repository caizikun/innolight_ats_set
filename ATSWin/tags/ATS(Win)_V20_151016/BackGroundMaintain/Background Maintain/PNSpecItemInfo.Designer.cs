namespace Maintain
{
    partial class PNSpecItemInfo
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
            this.grpPrmtr = new System.Windows.Forms.GroupBox();
            this.lblSpecMax = new System.Windows.Forms.Label();
            this.cboSpecMax = new System.Windows.Forms.ComboBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblSpecMin = new System.Windows.Forms.Label();
            this.lblTypical = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.btnPrmtrOK = new System.Windows.Forms.Button();
            this.cboSpecMin = new System.Windows.Forms.ComboBox();
            this.cboTypical = new System.Windows.Forms.ComboBox();
            this.cboItemName = new System.Windows.Forms.ComboBox();
            this.grpPNSpec = new System.Windows.Forms.GroupBox();
            this.btnPrmtrDelete = new System.Windows.Forms.Button();
            this.btnPrmtrAdd = new System.Windows.Forms.Button();
            this.txtSaveResult = new System.Windows.Forms.TextBox();
            this.dgvPrmtr = new System.Windows.Forms.DataGridView();
            this.grpPrmtr.SuspendLayout();
            this.grpPNSpec.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPrmtr)).BeginInit();
            this.SuspendLayout();
            // 
            // btnFinish
            // 
            this.btnFinish.Location = new System.Drawing.Point(267, 387);
            this.btnFinish.Name = "btnFinish";
            this.btnFinish.Size = new System.Drawing.Size(116, 31);
            this.btnFinish.TabIndex = 28;
            this.btnFinish.Text = "Finish";
            this.btnFinish.UseVisualStyleBackColor = true;
            this.btnFinish.Click += new System.EventHandler(this.btnFinish_Click);
            // 
            // grpPrmtr
            // 
            this.grpPrmtr.Controls.Add(this.lblSpecMax);
            this.grpPrmtr.Controls.Add(this.cboSpecMax);
            this.grpPrmtr.Controls.Add(this.txtDescription);
            this.grpPrmtr.Controls.Add(this.label2);
            this.grpPrmtr.Controls.Add(this.lblSpecMin);
            this.grpPrmtr.Controls.Add(this.lblTypical);
            this.grpPrmtr.Controls.Add(this.label27);
            this.grpPrmtr.Controls.Add(this.btnPrmtrOK);
            this.grpPrmtr.Controls.Add(this.cboSpecMin);
            this.grpPrmtr.Controls.Add(this.cboTypical);
            this.grpPrmtr.Controls.Add(this.cboItemName);
            this.grpPrmtr.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grpPrmtr.Location = new System.Drawing.Point(439, 2);
            this.grpPrmtr.Name = "grpPrmtr";
            this.grpPrmtr.Size = new System.Drawing.Size(237, 364);
            this.grpPrmtr.TabIndex = 59;
            this.grpPrmtr.TabStop = false;
            this.grpPrmtr.Text = "Current Item";
            // 
            // lblSpecMax
            // 
            this.lblSpecMax.AutoSize = true;
            this.lblSpecMax.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblSpecMax.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblSpecMax.Location = new System.Drawing.Point(6, 99);
            this.lblSpecMax.Name = "lblSpecMax";
            this.lblSpecMax.Size = new System.Drawing.Size(47, 12);
            this.lblSpecMax.TabIndex = 87;
            this.lblSpecMax.Text = "SpecMax";
            // 
            // cboSpecMax
            // 
            this.cboSpecMax.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboSpecMax.FormattingEnabled = true;
            this.cboSpecMax.Location = new System.Drawing.Point(85, 96);
            this.cboSpecMax.Name = "cboSpecMax";
            this.cboSpecMax.Size = new System.Drawing.Size(148, 20);
            this.cboSpecMax.TabIndex = 86;
            // 
            // txtDescription
            // 
            this.txtDescription.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtDescription.Location = new System.Drawing.Point(8, 162);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ReadOnly = true;
            this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescription.Size = new System.Drawing.Size(223, 105);
            this.txtDescription.TabIndex = 85;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(6, 128);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 12);
            this.label2.TabIndex = 84;
            this.label2.Text = "Description";
            // 
            // lblSpecMin
            // 
            this.lblSpecMin.AutoSize = true;
            this.lblSpecMin.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblSpecMin.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblSpecMin.Location = new System.Drawing.Point(6, 73);
            this.lblSpecMin.Name = "lblSpecMin";
            this.lblSpecMin.Size = new System.Drawing.Size(47, 12);
            this.lblSpecMin.TabIndex = 76;
            this.lblSpecMin.Text = "SpecMin";
            // 
            // lblTypical
            // 
            this.lblTypical.AutoSize = true;
            this.lblTypical.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTypical.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblTypical.Location = new System.Drawing.Point(6, 46);
            this.lblTypical.Name = "lblTypical";
            this.lblTypical.Size = new System.Drawing.Size(77, 12);
            this.lblTypical.TabIndex = 74;
            this.lblTypical.Text = "TypicalValue";
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
            // btnPrmtrOK
            // 
            this.btnPrmtrOK.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPrmtrOK.Location = new System.Drawing.Point(98, 293);
            this.btnPrmtrOK.Name = "btnPrmtrOK";
            this.btnPrmtrOK.Size = new System.Drawing.Size(50, 26);
            this.btnPrmtrOK.TabIndex = 68;
            this.btnPrmtrOK.Text = "Save";
            this.btnPrmtrOK.UseVisualStyleBackColor = true;
            this.btnPrmtrOK.Click += new System.EventHandler(this.btnPrmtrOK_Click);
            // 
            // cboSpecMin
            // 
            this.cboSpecMin.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboSpecMin.FormattingEnabled = true;
            this.cboSpecMin.Location = new System.Drawing.Point(85, 70);
            this.cboSpecMin.Name = "cboSpecMin";
            this.cboSpecMin.Size = new System.Drawing.Size(148, 20);
            this.cboSpecMin.TabIndex = 61;
            // 
            // cboTypical
            // 
            this.cboTypical.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboTypical.FormattingEnabled = true;
            this.cboTypical.Location = new System.Drawing.Point(85, 44);
            this.cboTypical.Name = "cboTypical";
            this.cboTypical.Size = new System.Drawing.Size(148, 20);
            this.cboTypical.TabIndex = 50;
            // 
            // cboItemName
            // 
            this.cboItemName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboItemName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboItemName.FormattingEnabled = true;
            this.cboItemName.Location = new System.Drawing.Point(85, 18);
            this.cboItemName.Name = "cboItemName";
            this.cboItemName.Size = new System.Drawing.Size(148, 20);
            this.cboItemName.TabIndex = 52;
            this.cboItemName.SelectedIndexChanged += new System.EventHandler(this.cboItemName_SelectedIndexChanged);
            // 
            // grpPNSpec
            // 
            this.grpPNSpec.Controls.Add(this.btnPrmtrDelete);
            this.grpPNSpec.Controls.Add(this.btnPrmtrAdd);
            this.grpPNSpec.Controls.Add(this.txtSaveResult);
            this.grpPNSpec.Controls.Add(this.dgvPrmtr);
            this.grpPNSpec.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grpPNSpec.Location = new System.Drawing.Point(1, 2);
            this.grpPNSpec.Name = "grpPNSpec";
            this.grpPNSpec.Size = new System.Drawing.Size(432, 366);
            this.grpPNSpec.TabIndex = 58;
            this.grpPNSpec.TabStop = false;
            this.grpPNSpec.Text = "PNSpecItems";
            // 
            // btnPrmtrDelete
            // 
            this.btnPrmtrDelete.BackColor = System.Drawing.Color.Fuchsia;
            this.btnPrmtrDelete.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPrmtrDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnPrmtrDelete.Location = new System.Drawing.Point(266, 305);
            this.btnPrmtrDelete.Name = "btnPrmtrDelete";
            this.btnPrmtrDelete.Size = new System.Drawing.Size(59, 25);
            this.btnPrmtrDelete.TabIndex = 66;
            this.btnPrmtrDelete.Text = "Delete";
            this.btnPrmtrDelete.UseVisualStyleBackColor = false;
            this.btnPrmtrDelete.Click += new System.EventHandler(this.btnPrmtrDelete_Click);
            // 
            // btnPrmtrAdd
            // 
            this.btnPrmtrAdd.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnPrmtrAdd.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPrmtrAdd.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnPrmtrAdd.Location = new System.Drawing.Point(107, 305);
            this.btnPrmtrAdd.Name = "btnPrmtrAdd";
            this.btnPrmtrAdd.Size = new System.Drawing.Size(59, 25);
            this.btnPrmtrAdd.TabIndex = 65;
            this.btnPrmtrAdd.Text = "Add";
            this.btnPrmtrAdd.UseVisualStyleBackColor = false;
            this.btnPrmtrAdd.Click += new System.EventHandler(this.btnPrmtrAdd_Click);
            // 
            // txtSaveResult
            // 
            this.txtSaveResult.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtSaveResult.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtSaveResult.Location = new System.Drawing.Point(6, 340);
            this.txtSaveResult.Multiline = true;
            this.txtSaveResult.Name = "txtSaveResult";
            this.txtSaveResult.ReadOnly = true;
            this.txtSaveResult.Size = new System.Drawing.Size(420, 24);
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
            this.dgvPrmtr.Size = new System.Drawing.Size(420, 279);
            this.dgvPrmtr.TabIndex = 3;
            this.dgvPrmtr.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvPrmtr_CellMouseClick);
            this.dgvPrmtr.CurrentCellChanged += new System.EventHandler(this.dgvPrmtr_CurrentCellChanged);
            // 
            // PNSpecItemInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(676, 430);
            this.Controls.Add(this.grpPrmtr);
            this.Controls.Add(this.btnFinish);
            this.Controls.Add(this.grpPNSpec);
            this.MaximumSize = new System.Drawing.Size(692, 468);
            this.Name = "PNSpecItemInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PNSpecItemInfo";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SpecItemInfo_FormClosing);
            this.Load += new System.EventHandler(this.SpecItemInfo_Load);
            this.grpPrmtr.ResumeLayout(false);
            this.grpPrmtr.PerformLayout();
            this.grpPNSpec.ResumeLayout(false);
            this.grpPNSpec.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPrmtr)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnFinish;
        private System.Windows.Forms.GroupBox grpPrmtr;
        private System.Windows.Forms.Button btnPrmtrOK;
        private System.Windows.Forms.ComboBox cboSpecMin;
        private System.Windows.Forms.ComboBox cboTypical;
        private System.Windows.Forms.ComboBox cboItemName;
        private System.Windows.Forms.GroupBox grpPNSpec;
        private System.Windows.Forms.TextBox txtSaveResult;
        private System.Windows.Forms.DataGridView dgvPrmtr;
        private System.Windows.Forms.Label lblSpecMin;
        private System.Windows.Forms.Label lblTypical;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label lblSpecMax;
        private System.Windows.Forms.ComboBox cboSpecMax;
        private System.Windows.Forms.Button btnPrmtrDelete;
        private System.Windows.Forms.Button btnPrmtrAdd;
    }
}
namespace Maintain
{
    partial class TypeForm
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
            this.cboMSAName = new System.Windows.Forms.ComboBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.currlst = new System.Windows.Forms.ListBox();
            this.btnPreviousPage = new System.Windows.Forms.Button();
            this.grpType.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTypeName
            // 
            this.lblTypeName.AutoSize = true;
            this.lblTypeName.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTypeName.Location = new System.Drawing.Point(64, 2);
            this.lblTypeName.Name = "lblTypeName";
            this.lblTypeName.Size = new System.Drawing.Size(79, 14);
            this.lblTypeName.TabIndex = 26;
            this.lblTypeName.Text = "Type List";
            // 
            // grpType
            // 
            this.grpType.Controls.Add(this.cboMSAName);
            this.grpType.Controls.Add(this.txtName);
            this.grpType.Controls.Add(this.btnOK);
            this.grpType.Controls.Add(this.label3);
            this.grpType.Controls.Add(this.label1);
            this.grpType.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grpType.Location = new System.Drawing.Point(219, 2);
            this.grpType.Name = "grpType";
            this.grpType.Size = new System.Drawing.Size(207, 155);
            this.grpType.TabIndex = 25;
            this.grpType.TabStop = false;
            this.grpType.Text = "Type Info";
            // 
            // cboMSAName
            // 
            this.cboMSAName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMSAName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboMSAName.FormattingEnabled = true;
            this.cboMSAName.Location = new System.Drawing.Point(66, 81);
            this.cboMSAName.Name = "cboMSAName";
            this.cboMSAName.Size = new System.Drawing.Size(135, 20);
            this.cboMSAName.TabIndex = 23;
            this.cboMSAName.SelectedIndexChanged += new System.EventHandler(this.cboMSAName_SelectedIndexChanged);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(66, 39);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(135, 23);
            this.txtName.TabIndex = 22;
            // 
            // btnOK
            // 
            this.btnOK.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOK.Location = new System.Drawing.Point(66, 126);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 21;
            this.btnOK.Text = "Save";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(7, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "MSAType";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(7, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "TypeName";
            // 
            // currlst
            // 
            this.currlst.FormattingEnabled = true;
            this.currlst.HorizontalScrollbar = true;
            this.currlst.ItemHeight = 12;
            this.currlst.Items.AddRange(new object[] {
            "QSFP",
            "CFP",
            "SFP",
            "XFP"});
            this.currlst.Location = new System.Drawing.Point(6, 21);
            this.currlst.Name = "currlst";
            this.currlst.ScrollAlwaysVisible = true;
            this.currlst.Size = new System.Drawing.Size(207, 136);
            this.currlst.TabIndex = 24;
            this.currlst.SelectedIndexChanged += new System.EventHandler(this.currlst_SelectedIndexChanged);
            // 
            // btnPreviousPage
            // 
            this.btnPreviousPage.Location = new System.Drawing.Point(158, 169);
            this.btnPreviousPage.Name = "btnPreviousPage";
            this.btnPreviousPage.Size = new System.Drawing.Size(100, 27);
            this.btnPreviousPage.TabIndex = 27;
            this.btnPreviousPage.Text = "Return";
            this.btnPreviousPage.UseVisualStyleBackColor = true;
            this.btnPreviousPage.Click += new System.EventHandler(this.btnPreviousPage_Click);
            // 
            // TypeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(430, 201);
            this.Controls.Add(this.btnPreviousPage);
            this.Controls.Add(this.lblTypeName);
            this.Controls.Add(this.grpType);
            this.Controls.Add(this.currlst);
            this.MaximumSize = new System.Drawing.Size(446, 239);
            this.Name = "TypeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TypeForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TypePNForm_FormClosing);
            this.Load += new System.EventHandler(this.TypePNForm_Load);
            this.grpType.ResumeLayout(false);
            this.grpType.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTypeName;
        private System.Windows.Forms.GroupBox grpType;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox currlst;
        private System.Windows.Forms.ComboBox cboMSAName;
        private System.Windows.Forms.Button btnPreviousPage;
    }
}
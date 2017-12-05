namespace GlobalInfo
{
    partial class MASDefine
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
            this.grpMSADefine = new System.Windows.Forms.GroupBox();
            this.dgvMSADefine = new System.Windows.Forms.DataGridView();
            this.grpMSADefine.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMSADefine)).BeginInit();
            this.SuspendLayout();
            // 
            // grpMSADefine
            // 
            this.grpMSADefine.Controls.Add(this.dgvMSADefine);
            this.grpMSADefine.Location = new System.Drawing.Point(2, 2);
            this.grpMSADefine.Name = "grpMSADefine";
            this.grpMSADefine.Size = new System.Drawing.Size(732, 443);
            this.grpMSADefine.TabIndex = 35;
            this.grpMSADefine.TabStop = false;
            this.grpMSADefine.Text = "MSADefine";
            // 
            // dgvMSADefine
            // 
            this.dgvMSADefine.AllowUserToAddRows = false;
            this.dgvMSADefine.AllowUserToDeleteRows = false;
            this.dgvMSADefine.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMSADefine.Location = new System.Drawing.Point(7, 15);
            this.dgvMSADefine.MultiSelect = false;
            this.dgvMSADefine.Name = "dgvMSADefine";
            this.dgvMSADefine.ReadOnly = true;
            this.dgvMSADefine.RowHeadersVisible = false;
            this.dgvMSADefine.RowTemplate.Height = 23;
            this.dgvMSADefine.Size = new System.Drawing.Size(719, 422);
            this.dgvMSADefine.TabIndex = 21;
            // 
            // MASDefine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(736, 446);
            this.Controls.Add(this.grpMSADefine);
            this.MaximumSize = new System.Drawing.Size(752, 484);
            this.Name = "MASDefine";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "MASDefine";
            this.Load += new System.EventHandler(this.MASDefine_Load);
            this.grpMSADefine.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMSADefine)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.DataGridView dgvMSADefine;
        public System.Windows.Forms.GroupBox grpMSADefine;
    }
}
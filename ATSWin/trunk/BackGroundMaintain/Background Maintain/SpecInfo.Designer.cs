namespace Maintain
{
    partial class SpecInfo
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
            this.lblTestPlanName = new System.Windows.Forms.Label();
            this.grpItem = new System.Windows.Forms.GroupBox();
            this.txtUnit = new System.Windows.Forms.TextBox();
            this.lblUnit = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.currlst = new System.Windows.Forms.ListBox();
            this.btnFinish = new System.Windows.Forms.Button();
            this.grpItem.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTestPlanName
            // 
            this.lblTestPlanName.AutoSize = true;
            this.lblTestPlanName.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTestPlanName.Location = new System.Drawing.Point(12, 17);
            this.lblTestPlanName.Name = "lblTestPlanName";
            this.lblTestPlanName.Size = new System.Drawing.Size(135, 14);
            this.lblTestPlanName.TabIndex = 23;
            this.lblTestPlanName.Text = "Specs Items list";
            // 
            // grpItem
            // 
            this.grpItem.Controls.Add(this.txtUnit);
            this.grpItem.Controls.Add(this.lblUnit);
            this.grpItem.Controls.Add(this.txtDescription);
            this.grpItem.Controls.Add(this.txtName);
            this.grpItem.Controls.Add(this.btnOK);
            this.grpItem.Controls.Add(this.label6);
            this.grpItem.Controls.Add(this.label5);
            this.grpItem.Controls.Add(this.label3);
            this.grpItem.Controls.Add(this.label1);
            this.grpItem.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grpItem.Location = new System.Drawing.Point(188, 17);
            this.grpItem.Name = "grpItem";
            this.grpItem.Size = new System.Drawing.Size(298, 323);
            this.grpItem.TabIndex = 20;
            this.grpItem.TabStop = false;
            this.grpItem.Text = "Item Info";
            // 
            // txtUnit
            // 
            this.txtUnit.Location = new System.Drawing.Point(79, 105);
            this.txtUnit.Name = "txtUnit";
            this.txtUnit.Size = new System.Drawing.Size(210, 23);
            this.txtUnit.TabIndex = 25;
            // 
            // lblUnit
            // 
            this.lblUnit.AutoSize = true;
            this.lblUnit.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblUnit.Location = new System.Drawing.Point(6, 110);
            this.lblUnit.Name = "lblUnit";
            this.lblUnit.Size = new System.Drawing.Size(29, 12);
            this.lblUnit.TabIndex = 24;
            this.lblUnit.Text = "Unit";
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(79, 134);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescription.Size = new System.Drawing.Size(210, 71);
            this.txtDescription.TabIndex = 23;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(79, 76);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(210, 23);
            this.txtName.TabIndex = 22;
            // 
            // btnOK
            // 
            this.btnOK.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOK.Location = new System.Drawing.Point(122, 236);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 21;
            this.btnOK.Text = "Save";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(162, 57);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 14);
            this.label6.TabIndex = 9;
            this.label6.Text = "Vaule";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(6, 57);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 14);
            this.label5.TabIndex = 8;
            this.label5.Text = "Item";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(6, 148);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "Description";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(6, 81);
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
            "AP",
            "ER",
            "..."});
            this.currlst.Location = new System.Drawing.Point(11, 36);
            this.currlst.Name = "currlst";
            this.currlst.ScrollAlwaysVisible = true;
            this.currlst.Size = new System.Drawing.Size(161, 304);
            this.currlst.TabIndex = 19;
            this.currlst.SelectedIndexChanged += new System.EventHandler(this.currlst_SelectedIndexChanged);
            // 
            // btnFinish
            // 
            this.btnFinish.Enabled = false;
            this.btnFinish.Location = new System.Drawing.Point(188, 346);
            this.btnFinish.Name = "btnFinish";
            this.btnFinish.Size = new System.Drawing.Size(75, 34);
            this.btnFinish.TabIndex = 24;
            this.btnFinish.Text = "Finish";
            this.btnFinish.UseVisualStyleBackColor = true;
            this.btnFinish.Click += new System.EventHandler(this.btnFinish_Click);
            // 
            // SpecInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 382);
            this.Controls.Add(this.btnFinish);
            this.Controls.Add(this.lblTestPlanName);
            this.Controls.Add(this.grpItem);
            this.Controls.Add(this.currlst);
            this.MaximumSize = new System.Drawing.Size(500, 420);
            this.Name = "SpecInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SpecInfo";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CtrlInfo_FormClosing);
            this.Load += new System.EventHandler(this.CtrlInfo_Load);
            this.grpItem.ResumeLayout(false);
            this.grpItem.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTestPlanName;
        private System.Windows.Forms.GroupBox grpItem;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox currlst;
        private System.Windows.Forms.Button btnFinish;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtUnit;
        private System.Windows.Forms.Label lblUnit;
    }
}
namespace GlobalInfo
{
    partial class PlanNameForm
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
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.ItemName = new System.Windows.Forms.Label();
            this.cboPN = new System.Windows.Forms.ComboBox();
            this.lblPN = new System.Windows.Forms.Label();
            this.cboPlanName = new System.Windows.Forms.ComboBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.listPNPlan = new System.Windows.Forms.ListBox();
            this.chkAllPlan = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(269, 60);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(81, 26);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "确认";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(269, 128);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(81, 26);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "取消并退出";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // ItemName
            // 
            this.ItemName.AutoSize = true;
            this.ItemName.Location = new System.Drawing.Point(27, 42);
            this.ItemName.Name = "ItemName";
            this.ItemName.Size = new System.Drawing.Size(53, 12);
            this.ItemName.TabIndex = 2;
            this.ItemName.Text = "测试计划";
            // 
            // cboPN
            // 
            this.cboPN.FormattingEnabled = true;
            this.cboPN.Location = new System.Drawing.Point(86, 6);
            this.cboPN.Name = "cboPN";
            this.cboPN.Size = new System.Drawing.Size(121, 20);
            this.cboPN.TabIndex = 3;
            this.cboPN.SelectedIndexChanged += new System.EventHandler(this.cboPN_SelectedIndexChanged);
            // 
            // lblPN
            // 
            this.lblPN.AutoSize = true;
            this.lblPN.Location = new System.Drawing.Point(27, 9);
            this.lblPN.Name = "lblPN";
            this.lblPN.Size = new System.Drawing.Size(53, 12);
            this.lblPN.TabIndex = 4;
            this.lblPN.Text = "机种名称";
            // 
            // cboPlanName
            // 
            this.cboPlanName.FormattingEnabled = true;
            this.cboPlanName.Location = new System.Drawing.Point(86, 39);
            this.cboPlanName.Name = "cboPlanName";
            this.cboPlanName.Size = new System.Drawing.Size(121, 20);
            this.cboPlanName.TabIndex = 5;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(111, 69);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(52, 26);
            this.btnAdd.TabIndex = 6;
            this.btnAdd.Text = "添加>>";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(172, 69);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(52, 26);
            this.btnRemove.TabIndex = 7;
            this.btnRemove.Text = "<<移除";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // listPNPlan
            // 
            this.listPNPlan.FormattingEnabled = true;
            this.listPNPlan.ItemHeight = 12;
            this.listPNPlan.Location = new System.Drawing.Point(12, 101);
            this.listPNPlan.Name = "listPNPlan";
            this.listPNPlan.Size = new System.Drawing.Size(212, 220);
            this.listPNPlan.TabIndex = 8;
            this.listPNPlan.SelectedIndexChanged += new System.EventHandler(this.listPNPlan_SelectedIndexChanged);
            // 
            // chkAllPlan
            // 
            this.chkAllPlan.AutoSize = true;
            this.chkAllPlan.Location = new System.Drawing.Point(29, 75);
            this.chkAllPlan.Name = "chkAllPlan";
            this.chkAllPlan.Size = new System.Drawing.Size(72, 16);
            this.chkAllPlan.TabIndex = 9;
            this.chkAllPlan.Text = "复制所有";
            this.chkAllPlan.UseVisualStyleBackColor = true;
            this.chkAllPlan.CheckedChanged += new System.EventHandler(this.chkAllPlan_CheckedChanged);
            // 
            // PlanNameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(396, 332);
            this.Controls.Add(this.chkAllPlan);
            this.Controls.Add(this.listPNPlan);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.cboPlanName);
            this.Controls.Add(this.lblPN);
            this.Controls.Add(this.cboPN);
            this.Controls.Add(this.ItemName);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Name = "PlanNameForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PlanName";
            this.Load += new System.EventHandler(this.PlanNameForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label ItemName;
        private System.Windows.Forms.Label lblPN;
        public System.Windows.Forms.ComboBox cboPN;
        public System.Windows.Forms.ComboBox cboPlanName;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.ListBox listPNPlan;
        private System.Windows.Forms.CheckBox chkAllPlan;
    }
}
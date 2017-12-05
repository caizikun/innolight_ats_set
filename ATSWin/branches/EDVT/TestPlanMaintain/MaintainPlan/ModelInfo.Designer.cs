﻿namespace TestPlan
{
    partial class ModelInfo
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
            this.btnNextPage = new System.Windows.Forms.Button();
            this.grpItem = new System.Windows.Forms.GroupBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtEquipLst = new System.Windows.Forms.TextBox();
            this.btnAddEquip = new System.Windows.Forms.Button();
            this.btnRemoveEquip = new System.Windows.Forms.Button();
            this.lstSelectEquip = new System.Windows.Forms.ListBox();
            this.lstEquip = new System.Windows.Forms.ListBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtTestCtrlName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cboItemName = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cboAppModeID = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.globalistName = new System.Windows.Forms.ListBox();
            this.label13 = new System.Windows.Forms.Label();
            this.currlst = new System.Windows.Forms.ListBox();
            this.btnRemove = new System.Windows.Forms.Button();
            this.grpItem.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnNextPage
            // 
            this.btnNextPage.Location = new System.Drawing.Point(360, 382);
            this.btnNextPage.Name = "btnNextPage";
            this.btnNextPage.Size = new System.Drawing.Size(116, 31);
            this.btnNextPage.TabIndex = 28;
            this.btnNextPage.Text = "完成";
            this.btnNextPage.UseVisualStyleBackColor = true;
            this.btnNextPage.Click += new System.EventHandler(this.btnNextPage_Click);
            // 
            // grpItem
            // 
            this.grpItem.Controls.Add(this.btnOK);
            this.grpItem.Controls.Add(this.label7);
            this.grpItem.Controls.Add(this.label4);
            this.grpItem.Controls.Add(this.txtEquipLst);
            this.grpItem.Controls.Add(this.btnAddEquip);
            this.grpItem.Controls.Add(this.btnRemoveEquip);
            this.grpItem.Controls.Add(this.lstSelectEquip);
            this.grpItem.Controls.Add(this.lstEquip);
            this.grpItem.Controls.Add(this.label10);
            this.grpItem.Controls.Add(this.txtTestCtrlName);
            this.grpItem.Controls.Add(this.label6);
            this.grpItem.Controls.Add(this.label5);
            this.grpItem.Controls.Add(this.label3);
            this.grpItem.Controls.Add(this.cboItemName);
            this.grpItem.Controls.Add(this.label2);
            this.grpItem.Controls.Add(this.cboAppModeID);
            this.grpItem.Controls.Add(this.label1);
            this.grpItem.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grpItem.Location = new System.Drawing.Point(429, 9);
            this.grpItem.Name = "grpItem";
            this.grpItem.Size = new System.Drawing.Size(359, 367);
            this.grpItem.TabIndex = 26;
            this.grpItem.TabStop = false;
            this.grpItem.Text = "项目资料";
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnOK.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOK.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnOK.Location = new System.Drawing.Point(158, 322);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(56, 24);
            this.btnOK.TabIndex = 56;
            this.btnOK.Text = "保存";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // label7
            // 
            this.label7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label7.Location = new System.Drawing.Point(209, 180);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(133, 19);
            this.label7.TabIndex = 55;
            this.label7.Text = "已存在设备列表";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(8, 180);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(133, 19);
            this.label4.TabIndex = 54;
            this.label4.Text = "备选设备列表";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtEquipLst
            // 
            this.txtEquipLst.Enabled = false;
            this.txtEquipLst.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtEquipLst.Location = new System.Drawing.Point(92, 130);
            this.txtEquipLst.Multiline = true;
            this.txtEquipLst.Name = "txtEquipLst";
            this.txtEquipLst.Size = new System.Drawing.Size(250, 47);
            this.txtEquipLst.TabIndex = 53;
            this.txtEquipLst.Text = "txtEquipLst1\r\ntxtEquipLst2\r\ntxtEquipLst3";
            // 
            // btnAddEquip
            // 
            this.btnAddEquip.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnAddEquip.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAddEquip.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnAddEquip.Location = new System.Drawing.Point(158, 238);
            this.btnAddEquip.Name = "btnAddEquip";
            this.btnAddEquip.Size = new System.Drawing.Size(56, 23);
            this.btnAddEquip.TabIndex = 52;
            this.btnAddEquip.Text = "添加=>";
            this.btnAddEquip.UseVisualStyleBackColor = false;
            this.btnAddEquip.Click += new System.EventHandler(this.btnAddEquip_Click);
            // 
            // btnRemoveEquip
            // 
            this.btnRemoveEquip.BackColor = System.Drawing.Color.Fuchsia;
            this.btnRemoveEquip.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnRemoveEquip.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnRemoveEquip.Location = new System.Drawing.Point(158, 278);
            this.btnRemoveEquip.Name = "btnRemoveEquip";
            this.btnRemoveEquip.Size = new System.Drawing.Size(56, 23);
            this.btnRemoveEquip.TabIndex = 51;
            this.btnRemoveEquip.Text = "<=移除";
            this.btnRemoveEquip.UseVisualStyleBackColor = false;
            this.btnRemoveEquip.Click += new System.EventHandler(this.btnRemoveEquip_Click);
            // 
            // lstSelectEquip
            // 
            this.lstSelectEquip.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lstSelectEquip.FormattingEnabled = true;
            this.lstSelectEquip.HorizontalScrollbar = true;
            this.lstSelectEquip.ItemHeight = 12;
            this.lstSelectEquip.Location = new System.Drawing.Point(214, 207);
            this.lstSelectEquip.Name = "lstSelectEquip";
            this.lstSelectEquip.ScrollAlwaysVisible = true;
            this.lstSelectEquip.Size = new System.Drawing.Size(139, 160);
            this.lstSelectEquip.TabIndex = 50;
            // 
            // lstEquip
            // 
            this.lstEquip.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lstEquip.FormattingEnabled = true;
            this.lstEquip.HorizontalScrollbar = true;
            this.lstEquip.ItemHeight = 12;
            this.lstEquip.Location = new System.Drawing.Point(8, 207);
            this.lstEquip.Name = "lstEquip";
            this.lstEquip.ScrollAlwaysVisible = true;
            this.lstEquip.Size = new System.Drawing.Size(144, 160);
            this.lstEquip.TabIndex = 49;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(3, 23);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 12);
            this.label10.TabIndex = 13;
            this.label10.Text = "父项名称";
            // 
            // txtTestCtrlName
            // 
            this.txtTestCtrlName.Enabled = false;
            this.txtTestCtrlName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtTestCtrlName.Location = new System.Drawing.Point(96, 20);
            this.txtTestCtrlName.Name = "txtTestCtrlName";
            this.txtTestCtrlName.Size = new System.Drawing.Size(180, 21);
            this.txtTestCtrlName.TabIndex = 12;
            this.txtTestCtrlName.Text = "TestCtrlName";
            this.txtTestCtrlName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(208, 57);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 9;
            this.label6.Text = "内容";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(6, 57);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "项目";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(6, 149);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "设备清单";
            // 
            // cboItemName
            // 
            this.cboItemName.Enabled = false;
            this.cboItemName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboItemName.FormattingEnabled = true;
            this.cboItemName.Location = new System.Drawing.Point(92, 78);
            this.cboItemName.Name = "cboItemName";
            this.cboItemName.Size = new System.Drawing.Size(257, 20);
            this.cboItemName.TabIndex = 3;
            this.cboItemName.Text = "ItemName";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(6, 107);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "程序编号";
            // 
            // cboAppModeID
            // 
            this.cboAppModeID.Enabled = false;
            this.cboAppModeID.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboAppModeID.FormattingEnabled = true;
            this.cboAppModeID.Location = new System.Drawing.Point(92, 104);
            this.cboAppModeID.Name = "cboAppModeID";
            this.cboAppModeID.Size = new System.Drawing.Size(257, 20);
            this.cboAppModeID.TabIndex = 1;
            this.cboAppModeID.Text = "AppModeID";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(6, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "名称";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label12.Location = new System.Drawing.Point(24, 2);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(121, 19);
            this.label12.TabIndex = 50;
            this.label12.Text = "默认测试模型列表";
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnAdd.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnAdd.Location = new System.Drawing.Point(176, 87);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(64, 23);
            this.btnAdd.TabIndex = 49;
            this.btnAdd.Text = "添加=>";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // globalistName
            // 
            this.globalistName.FormattingEnabled = true;
            this.globalistName.HorizontalScrollbar = true;
            this.globalistName.ItemHeight = 12;
            this.globalistName.Location = new System.Drawing.Point(3, 24);
            this.globalistName.Name = "globalistName";
            this.globalistName.ScrollAlwaysVisible = true;
            this.globalistName.Size = new System.Drawing.Size(167, 352);
            this.globalistName.TabIndex = 48;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label13.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label13.Location = new System.Drawing.Point(268, 2);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(121, 19);
            this.label13.TabIndex = 47;
            this.label13.Text = "已有测试模型列表";
            // 
            // currlst
            // 
            this.currlst.FormattingEnabled = true;
            this.currlst.HorizontalScrollbar = true;
            this.currlst.ItemHeight = 12;
            this.currlst.Items.AddRange(new object[] {
            "Model1",
            "Model2",
            "Model3",
            "Model4",
            "...",
            "ModelN"});
            this.currlst.Location = new System.Drawing.Point(243, 24);
            this.currlst.Name = "currlst";
            this.currlst.ScrollAlwaysVisible = true;
            this.currlst.Size = new System.Drawing.Size(178, 352);
            this.currlst.TabIndex = 46;
            this.currlst.SelectedIndexChanged += new System.EventHandler(this.currlst_SelectedIndexChanged);
            // 
            // btnRemove
            // 
            this.btnRemove.BackColor = System.Drawing.Color.Fuchsia;
            this.btnRemove.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnRemove.Location = new System.Drawing.Point(173, 185);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(64, 23);
            this.btnRemove.TabIndex = 45;
            this.btnRemove.Text = "<=移除";
            this.btnRemove.UseVisualStyleBackColor = false;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // ModelInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(791, 419);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.globalistName);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.currlst);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnNextPage);
            this.Controls.Add(this.grpItem);
            this.MaximumSize = new System.Drawing.Size(807, 457);
            this.Name = "ModelInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ModelInfo";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ModelInfo_FormClosing);
            this.Load += new System.EventHandler(this.ModelInfo_Load);
            this.grpItem.ResumeLayout(false);
            this.grpItem.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnNextPage;
        private System.Windows.Forms.GroupBox grpItem;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtTestCtrlName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboItemName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboAppModeID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.ListBox globalistName;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ListBox currlst;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnAddEquip;
        private System.Windows.Forms.Button btnRemoveEquip;
        private System.Windows.Forms.ListBox lstSelectEquip;
        private System.Windows.Forms.ListBox lstEquip;
        private System.Windows.Forms.TextBox txtEquipLst;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnOK;
    }
}
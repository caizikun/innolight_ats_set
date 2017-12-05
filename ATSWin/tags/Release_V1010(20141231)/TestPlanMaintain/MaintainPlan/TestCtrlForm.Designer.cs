namespace Maintain
{
    partial class TestCtrlForm
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
            this.grpTestPrmtr = new System.Windows.Forms.GroupBox();
            this.btnEditPrmtr = new System.Windows.Forms.Button();
            this.dgvTestPrmtr = new System.Windows.Forms.DataGridView();
            this.crpTestModel = new System.Windows.Forms.GroupBox();
            this.btnModelDelete = new System.Windows.Forms.Button();
            this.btnModelEdit = new System.Windows.Forms.Button();
            this.btnModelAdd = new System.Windows.Forms.Button();
            this.btnModelDown = new System.Windows.Forms.Button();
            this.btnModelUp = new System.Windows.Forms.Button();
            this.dgvTestModel = new System.Windows.Forms.DataGridView();
            this.grpTestCtrl = new System.Windows.Forms.GroupBox();
            this.btnCtrlDelete = new System.Windows.Forms.Button();
            this.btnCtrlEdit = new System.Windows.Forms.Button();
            this.btnCtrlAdd = new System.Windows.Forms.Button();
            this.btnCtrlDown = new System.Windows.Forms.Button();
            this.btnCtrlUp = new System.Windows.Forms.Button();
            this.dgvTestCtrl = new System.Windows.Forms.DataGridView();
            this.grpTestPrmtr.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTestPrmtr)).BeginInit();
            this.crpTestModel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTestModel)).BeginInit();
            this.grpTestCtrl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTestCtrl)).BeginInit();
            this.SuspendLayout();
            // 
            // btnFinish
            // 
            this.btnFinish.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnFinish.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnFinish.Location = new System.Drawing.Point(330, 417);
            this.btnFinish.Name = "btnFinish";
            this.btnFinish.Size = new System.Drawing.Size(104, 24);
            this.btnFinish.TabIndex = 33;
            this.btnFinish.Text = "Finish";
            this.btnFinish.UseVisualStyleBackColor = false;
            this.btnFinish.Click += new System.EventHandler(this.btnFinish_Click);
            // 
            // grpTestPrmtr
            // 
            this.grpTestPrmtr.Controls.Add(this.btnEditPrmtr);
            this.grpTestPrmtr.Controls.Add(this.dgvTestPrmtr);
            this.grpTestPrmtr.Location = new System.Drawing.Point(526, 1);
            this.grpTestPrmtr.Name = "grpTestPrmtr";
            this.grpTestPrmtr.Size = new System.Drawing.Size(223, 410);
            this.grpTestPrmtr.TabIndex = 31;
            this.grpTestPrmtr.TabStop = false;
            this.grpTestPrmtr.Text = "TestParameters";
            // 
            // btnEditPrmtr
            // 
            this.btnEditPrmtr.Location = new System.Drawing.Point(84, 381);
            this.btnEditPrmtr.Name = "btnEditPrmtr";
            this.btnEditPrmtr.Size = new System.Drawing.Size(50, 20);
            this.btnEditPrmtr.TabIndex = 17;
            this.btnEditPrmtr.Text = "Edit";
            this.btnEditPrmtr.UseVisualStyleBackColor = true;
            this.btnEditPrmtr.Click += new System.EventHandler(this.btnEditPrmtr_Click);
            // 
            // dgvTestPrmtr
            // 
            this.dgvTestPrmtr.AllowUserToAddRows = false;
            this.dgvTestPrmtr.AllowUserToDeleteRows = false;
            this.dgvTestPrmtr.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTestPrmtr.Location = new System.Drawing.Point(6, 19);
            this.dgvTestPrmtr.Name = "dgvTestPrmtr";
            this.dgvTestPrmtr.ReadOnly = true;
            this.dgvTestPrmtr.RowHeadersVisible = false;
            this.dgvTestPrmtr.RowTemplate.Height = 23;
            this.dgvTestPrmtr.Size = new System.Drawing.Size(211, 358);
            this.dgvTestPrmtr.TabIndex = 3;
            // 
            // crpTestModel
            // 
            this.crpTestModel.Controls.Add(this.btnModelDelete);
            this.crpTestModel.Controls.Add(this.btnModelEdit);
            this.crpTestModel.Controls.Add(this.btnModelAdd);
            this.crpTestModel.Controls.Add(this.btnModelDown);
            this.crpTestModel.Controls.Add(this.btnModelUp);
            this.crpTestModel.Controls.Add(this.dgvTestModel);
            this.crpTestModel.Location = new System.Drawing.Point(263, 1);
            this.crpTestModel.Name = "crpTestModel";
            this.crpTestModel.Size = new System.Drawing.Size(257, 410);
            this.crpTestModel.TabIndex = 30;
            this.crpTestModel.TabStop = false;
            this.crpTestModel.Text = "TestModel setting";
            // 
            // btnModelDelete
            // 
            this.btnModelDelete.Location = new System.Drawing.Point(167, 381);
            this.btnModelDelete.Name = "btnModelDelete";
            this.btnModelDelete.Size = new System.Drawing.Size(50, 20);
            this.btnModelDelete.TabIndex = 18;
            this.btnModelDelete.Text = "Delete";
            this.btnModelDelete.UseVisualStyleBackColor = true;
            this.btnModelDelete.Click += new System.EventHandler(this.btnModelDelete_Click);
            // 
            // btnModelEdit
            // 
            this.btnModelEdit.Location = new System.Drawing.Point(88, 381);
            this.btnModelEdit.Name = "btnModelEdit";
            this.btnModelEdit.Size = new System.Drawing.Size(50, 20);
            this.btnModelEdit.TabIndex = 17;
            this.btnModelEdit.Text = "Edit";
            this.btnModelEdit.UseVisualStyleBackColor = true;
            this.btnModelEdit.Click += new System.EventHandler(this.btnModelEdit_Click);
            // 
            // btnModelAdd
            // 
            this.btnModelAdd.Location = new System.Drawing.Point(5, 381);
            this.btnModelAdd.Name = "btnModelAdd";
            this.btnModelAdd.Size = new System.Drawing.Size(50, 20);
            this.btnModelAdd.TabIndex = 16;
            this.btnModelAdd.Text = "Add";
            this.btnModelAdd.UseVisualStyleBackColor = true;
            this.btnModelAdd.Click += new System.EventHandler(this.btnModelAdd_Click);
            // 
            // btnModelDown
            // 
            this.btnModelDown.Location = new System.Drawing.Point(223, 210);
            this.btnModelDown.Name = "btnModelDown";
            this.btnModelDown.Size = new System.Drawing.Size(31, 23);
            this.btnModelDown.TabIndex = 14;
            this.btnModelDown.Text = "∨";
            this.btnModelDown.UseVisualStyleBackColor = true;
            this.btnModelDown.Click += new System.EventHandler(this.btnModelDown_Click);
            // 
            // btnModelUp
            // 
            this.btnModelUp.Location = new System.Drawing.Point(223, 166);
            this.btnModelUp.Name = "btnModelUp";
            this.btnModelUp.Size = new System.Drawing.Size(31, 23);
            this.btnModelUp.TabIndex = 13;
            this.btnModelUp.Text = "∧";
            this.btnModelUp.UseVisualStyleBackColor = true;
            this.btnModelUp.Click += new System.EventHandler(this.btnModelUp_Click);
            // 
            // dgvTestModel
            // 
            this.dgvTestModel.AllowUserToAddRows = false;
            this.dgvTestModel.AllowUserToDeleteRows = false;
            this.dgvTestModel.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTestModel.Location = new System.Drawing.Point(6, 19);
            this.dgvTestModel.Name = "dgvTestModel";
            this.dgvTestModel.ReadOnly = true;
            this.dgvTestModel.RowHeadersVisible = false;
            this.dgvTestModel.RowTemplate.Height = 23;
            this.dgvTestModel.Size = new System.Drawing.Size(211, 358);
            this.dgvTestModel.TabIndex = 3;
            this.dgvTestModel.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvTestModel_CellMouseClick);
            // 
            // grpTestCtrl
            // 
            this.grpTestCtrl.Controls.Add(this.btnCtrlDelete);
            this.grpTestCtrl.Controls.Add(this.btnCtrlEdit);
            this.grpTestCtrl.Controls.Add(this.btnCtrlAdd);
            this.grpTestCtrl.Controls.Add(this.btnCtrlDown);
            this.grpTestCtrl.Controls.Add(this.btnCtrlUp);
            this.grpTestCtrl.Controls.Add(this.dgvTestCtrl);
            this.grpTestCtrl.Location = new System.Drawing.Point(2, 1);
            this.grpTestCtrl.Name = "grpTestCtrl";
            this.grpTestCtrl.Size = new System.Drawing.Size(255, 410);
            this.grpTestCtrl.TabIndex = 29;
            this.grpTestCtrl.TabStop = false;
            this.grpTestCtrl.Text = "Folw Control setting";
            // 
            // btnCtrlDelete
            // 
            this.btnCtrlDelete.Location = new System.Drawing.Point(167, 381);
            this.btnCtrlDelete.Name = "btnCtrlDelete";
            this.btnCtrlDelete.Size = new System.Drawing.Size(50, 20);
            this.btnCtrlDelete.TabIndex = 15;
            this.btnCtrlDelete.Text = "Delete";
            this.btnCtrlDelete.UseVisualStyleBackColor = true;
            this.btnCtrlDelete.Click += new System.EventHandler(this.btnCtrlDelete_Click);
            // 
            // btnCtrlEdit
            // 
            this.btnCtrlEdit.Location = new System.Drawing.Point(89, 381);
            this.btnCtrlEdit.Name = "btnCtrlEdit";
            this.btnCtrlEdit.Size = new System.Drawing.Size(50, 20);
            this.btnCtrlEdit.TabIndex = 14;
            this.btnCtrlEdit.Text = "Edit";
            this.btnCtrlEdit.UseVisualStyleBackColor = true;
            this.btnCtrlEdit.Click += new System.EventHandler(this.btnCtrlEdit_Click);
            // 
            // btnCtrlAdd
            // 
            this.btnCtrlAdd.Location = new System.Drawing.Point(6, 381);
            this.btnCtrlAdd.Name = "btnCtrlAdd";
            this.btnCtrlAdd.Size = new System.Drawing.Size(50, 20);
            this.btnCtrlAdd.TabIndex = 13;
            this.btnCtrlAdd.Text = "Add";
            this.btnCtrlAdd.UseVisualStyleBackColor = true;
            this.btnCtrlAdd.Click += new System.EventHandler(this.btnCtrlAdd_Click);
            // 
            // btnCtrlDown
            // 
            this.btnCtrlDown.Location = new System.Drawing.Point(221, 210);
            this.btnCtrlDown.Name = "btnCtrlDown";
            this.btnCtrlDown.Size = new System.Drawing.Size(31, 23);
            this.btnCtrlDown.TabIndex = 12;
            this.btnCtrlDown.Text = "∨";
            this.btnCtrlDown.UseVisualStyleBackColor = true;
            this.btnCtrlDown.Click += new System.EventHandler(this.btnCtrlDown_Click);
            // 
            // btnCtrlUp
            // 
            this.btnCtrlUp.Location = new System.Drawing.Point(221, 166);
            this.btnCtrlUp.Name = "btnCtrlUp";
            this.btnCtrlUp.Size = new System.Drawing.Size(31, 23);
            this.btnCtrlUp.TabIndex = 11;
            this.btnCtrlUp.Text = "∧";
            this.btnCtrlUp.UseVisualStyleBackColor = true;
            this.btnCtrlUp.Click += new System.EventHandler(this.btnCtrlUp_Click);
            // 
            // dgvTestCtrl
            // 
            this.dgvTestCtrl.AllowUserToAddRows = false;
            this.dgvTestCtrl.AllowUserToDeleteRows = false;
            this.dgvTestCtrl.Location = new System.Drawing.Point(6, 19);
            this.dgvTestCtrl.Name = "dgvTestCtrl";
            this.dgvTestCtrl.ReadOnly = true;
            this.dgvTestCtrl.RowHeadersVisible = false;
            this.dgvTestCtrl.RowTemplate.Height = 23;
            this.dgvTestCtrl.Size = new System.Drawing.Size(211, 358);
            this.dgvTestCtrl.TabIndex = 3;
            this.dgvTestCtrl.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvTestCtrl_CellMouseClick);
            // 
            // TestCtrlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(755, 444);
            this.Controls.Add(this.btnFinish);
            this.Controls.Add(this.grpTestPrmtr);
            this.Controls.Add(this.crpTestModel);
            this.Controls.Add(this.grpTestCtrl);
            this.MaximumSize = new System.Drawing.Size(771, 482);
            this.Name = "TestCtrlForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TestCtrlForm";
            this.Load += new System.EventHandler(this.TestCtrlForm_Load);
            this.grpTestPrmtr.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTestPrmtr)).EndInit();
            this.crpTestModel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTestModel)).EndInit();
            this.grpTestCtrl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTestCtrl)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnFinish;
        private System.Windows.Forms.GroupBox grpTestPrmtr;
        private System.Windows.Forms.Button btnEditPrmtr;
        private System.Windows.Forms.DataGridView dgvTestPrmtr;
        private System.Windows.Forms.GroupBox crpTestModel;
        private System.Windows.Forms.Button btnModelDelete;
        private System.Windows.Forms.Button btnModelEdit;
        private System.Windows.Forms.Button btnModelAdd;
        private System.Windows.Forms.Button btnModelDown;
        private System.Windows.Forms.Button btnModelUp;
        private System.Windows.Forms.DataGridView dgvTestModel;
        private System.Windows.Forms.GroupBox grpTestCtrl;
        private System.Windows.Forms.Button btnCtrlDelete;
        private System.Windows.Forms.Button btnCtrlEdit;
        private System.Windows.Forms.Button btnCtrlAdd;
        private System.Windows.Forms.Button btnCtrlDown;
        private System.Windows.Forms.Button btnCtrlUp;
        private System.Windows.Forms.DataGridView dgvTestCtrl;
    }
}
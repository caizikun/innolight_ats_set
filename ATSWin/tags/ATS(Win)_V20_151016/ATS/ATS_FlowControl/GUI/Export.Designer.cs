namespace ATS
{
    partial class Export
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.buttonEnter = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.labelShow = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.dateTimePickerStartTime = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerEndTime = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonGetInf = new System.Windows.Forms.Button();
            this.radioButton_FMT = new System.Windows.Forms.RadioButton();
            this.radioButton_LP = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(8, 97);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(706, 260);
            this.dataGridView1.TabIndex = 0;
            // 
            // buttonEnter
            // 
            this.buttonEnter.Location = new System.Drawing.Point(198, 379);
            this.buttonEnter.Name = "buttonEnter";
            this.buttonEnter.Size = new System.Drawing.Size(98, 28);
            this.buttonEnter.TabIndex = 1;
            this.buttonEnter.Text = "Export";
            this.buttonEnter.UseVisualStyleBackColor = true;
            this.buttonEnter.Click += new System.EventHandler(this.buttonEnter_Click);
            // 
            // labelShow
            // 
            this.labelShow.BackColor = System.Drawing.Color.Aquamarine;
            this.labelShow.Location = new System.Drawing.Point(25, 420);
            this.labelShow.Name = "labelShow";
            this.labelShow.Size = new System.Drawing.Size(677, 23);
            this.labelShow.TabIndex = 3;
            this.labelShow.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(376, 390);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(338, 17);
            this.progressBar1.TabIndex = 4;
            // 
            // dateTimePickerStartTime
            // 
            this.dateTimePickerStartTime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dateTimePickerStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerStartTime.Location = new System.Drawing.Point(89, 29);
            this.dateTimePickerStartTime.Name = "dateTimePickerStartTime";
            this.dateTimePickerStartTime.Size = new System.Drawing.Size(161, 21);
            this.dateTimePickerStartTime.TabIndex = 5;
            this.dateTimePickerStartTime.ValueChanged += new System.EventHandler(this.dateTimePickerStartTime_ValueChanged);
            // 
            // dateTimePickerEndTime
            // 
            this.dateTimePickerEndTime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dateTimePickerEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerEndTime.Location = new System.Drawing.Point(355, 28);
            this.dateTimePickerEndTime.Name = "dateTimePickerEndTime";
            this.dateTimePickerEndTime.Size = new System.Drawing.Size(161, 21);
            this.dateTimePickerEndTime.TabIndex = 6;
            this.dateTimePickerEndTime.ValueChanged += new System.EventHandler(this.dateTimePickerEndTime_ValueChanged);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Aquamarine;
            this.label1.Location = new System.Drawing.Point(16, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 21);
            this.label1.TabIndex = 7;
            this.label1.Text = "起始时间:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Aquamarine;
            this.label2.Location = new System.Drawing.Point(268, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 21);
            this.label2.TabIndex = 8;
            this.label2.Text = "截止时间:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonGetInf
            // 
            this.buttonGetInf.Location = new System.Drawing.Point(27, 379);
            this.buttonGetInf.Name = "buttonGetInf";
            this.buttonGetInf.Size = new System.Drawing.Size(98, 28);
            this.buttonGetInf.TabIndex = 9;
            this.buttonGetInf.TabStop = false;
            this.buttonGetInf.Text = "GetInf";
            this.buttonGetInf.UseVisualStyleBackColor = true;
            this.buttonGetInf.Click += new System.EventHandler(this.buttonGetInf_Click);
            // 
            // radioButton_FMT
            // 
            this.radioButton_FMT.AutoSize = true;
            this.radioButton_FMT.Checked = true;
            this.radioButton_FMT.Location = new System.Drawing.Point(23, 13);
            this.radioButton_FMT.Name = "radioButton_FMT";
            this.radioButton_FMT.Size = new System.Drawing.Size(41, 16);
            this.radioButton_FMT.TabIndex = 10;
            this.radioButton_FMT.TabStop = true;
            this.radioButton_FMT.Text = "FMT";
            this.radioButton_FMT.UseVisualStyleBackColor = true;
            // 
            // radioButton_LP
            // 
            this.radioButton_LP.AutoSize = true;
            this.radioButton_LP.Location = new System.Drawing.Point(71, 13);
            this.radioButton_LP.Name = "radioButton_LP";
            this.radioButton_LP.Size = new System.Drawing.Size(35, 16);
            this.radioButton_LP.TabIndex = 11;
            this.radioButton_LP.Text = "LP";
            this.radioButton_LP.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton_FMT);
            this.groupBox1.Controls.Add(this.radioButton_LP);
            this.groupBox1.Location = new System.Drawing.Point(535, 18);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(115, 35);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "DataType";
            // 
            // Export
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(726, 452);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonGetInf);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateTimePickerEndTime);
            this.Controls.Add(this.dateTimePickerStartTime);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.labelShow);
            this.Controls.Add(this.buttonEnter);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Export";
            this.Text = "Export";
            this.Load += new System.EventHandler(this.Export_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button buttonEnter;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Label labelShow;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.DateTimePicker dateTimePickerStartTime;
        private System.Windows.Forms.DateTimePicker dateTimePickerEndTime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonGetInf;
        private System.Windows.Forms.RadioButton radioButton_FMT;
        private System.Windows.Forms.RadioButton radioButton_LP;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}
namespace ATS
{
    partial class FormATS
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label12 = new System.Windows.Forms.Label();
            this.buttonEq = new System.Windows.Forms.Button();
            this.btTestModel = new System.Windows.Forms.Button();
            this.skinEngine1 = new Sunisoft.IrisSkin.SkinEngine();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(237)))), ((int)(((byte)(247)))));
            this.label12.Font = new System.Drawing.Font("宋体", 42F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.ForeColor = System.Drawing.Color.Blue;
            this.label12.Location = new System.Drawing.Point(146, 9);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(568, 80);
            this.label12.TabIndex = 85;
            this.label12.Text = "DebugSystem";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonEq
            // 
            this.buttonEq.Location = new System.Drawing.Point(64, 225);
            this.buttonEq.Name = "buttonEq";
            this.buttonEq.Size = new System.Drawing.Size(75, 35);
            this.buttonEq.TabIndex = 86;
            this.buttonEq.Text = "Equipment";
            this.buttonEq.UseVisualStyleBackColor = true;
            this.buttonEq.Click += new System.EventHandler(this.buttonEq_Click);
            // 
            // btTestModel
            // 
            this.btTestModel.Location = new System.Drawing.Point(64, 309);
            this.btTestModel.Name = "btTestModel";
            this.btTestModel.Size = new System.Drawing.Size(75, 34);
            this.btTestModel.TabIndex = 87;
            this.btTestModel.Text = "Testmodel";
            this.btTestModel.UseVisualStyleBackColor = true;
            this.btTestModel.Click += new System.EventHandler(this.btTestModel_Click);
            // 
            // skinEngine1
            // 
            this.skinEngine1.@__DrawButtonFocusRectangle = true;
            this.skinEngine1.DisabledButtonTextColor = System.Drawing.Color.Gray;
            this.skinEngine1.DisabledMenuFontColor = System.Drawing.SystemColors.GrayText;
            this.skinEngine1.InactiveCaptionColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.skinEngine1.SerialNumber = "";
            this.skinEngine1.SkinFile = null;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(409, 225);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(352, 297);
            this.richTextBox1.TabIndex = 88;
            this.richTextBox1.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(487, 189);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(159, 19);
            this.label1.TabIndex = 89;
            this.label1.Text = "TestModelOutPut";
            // 
            // FormATS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(237)))), ((int)(((byte)(247)))));
            this.ClientSize = new System.Drawing.Size(883, 665);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.btTestModel);
            this.Controls.Add(this.buttonEq);
            this.Controls.Add(this.label12);
            this.Name = "FormATS";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CodeDebug";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.FormATS_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button buttonEq;
        private System.Windows.Forms.Button btTestModel;
        private Sunisoft.IrisSkin.SkinEngine skinEngine1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label1;
    }
}


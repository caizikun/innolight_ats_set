namespace ATS
{
    partial class Form1
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
            this.button_Test = new System.Windows.Forms.Button();
            this.button_Config = new System.Windows.Forms.Button();
            this.comboBoxPN = new System.Windows.Forms.ComboBox();
            this.comboBoxPType = new System.Windows.Forms.ComboBox();
            this.dataGridViewTotalData = new System.Windows.Forms.DataGridView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.richInterfaceLog = new System.Windows.Forms.RichTextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.richSqlLog = new System.Windows.Forms.RichTextBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.label12 = new System.Windows.Forms.Label();
            this.IccOffset = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.VccOffset = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.AttOffset4 = new System.Windows.Forms.TextBox();
            this.AttOffset3 = new System.Windows.Forms.TextBox();
            this.AttOffset2 = new System.Windows.Forms.TextBox();
            this.AttOffset1 = new System.Windows.Forms.TextBox();
            this.ScopeOffset4 = new System.Windows.Forms.TextBox();
            this.ScopeOffset3 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.ScopeOffset2 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.ScopeOffset1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxTestPlan = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxSN = new System.Windows.Forms.TextBox();
            this.labelResult = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.button8 = new System.Windows.Forms.Button();
            this.labelShow = new System.Windows.Forms.Label();
            this.progress = new System.Windows.Forms.ProgressBar();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTotalData)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_Test
            // 
            this.button_Test.BackColor = System.Drawing.SystemColors.Control;
            this.button_Test.Location = new System.Drawing.Point(233, 509);
            this.button_Test.Name = "button_Test";
            this.button_Test.Size = new System.Drawing.Size(77, 28);
            this.button_Test.TabIndex = 42;
            this.button_Test.Text = "Test";
            this.button_Test.UseVisualStyleBackColor = false;
            this.button_Test.Click += new System.EventHandler(this.button_Test_Click);
            // 
            // button_Config
            // 
            this.button_Config.BackColor = System.Drawing.SystemColors.Control;
            this.button_Config.Location = new System.Drawing.Point(48, 509);
            this.button_Config.Name = "button_Config";
            this.button_Config.Size = new System.Drawing.Size(73, 28);
            this.button_Config.TabIndex = 41;
            this.button_Config.Text = "Config";
            this.button_Config.UseVisualStyleBackColor = false;
            this.button_Config.Click += new System.EventHandler(this.button_Config_Click);
            // 
            // comboBoxPN
            // 
            this.comboBoxPN.FormattingEnabled = true;
            this.comboBoxPN.Location = new System.Drawing.Point(153, 30);
            this.comboBoxPN.Name = "comboBoxPN";
            this.comboBoxPN.Size = new System.Drawing.Size(116, 20);
            this.comboBoxPN.TabIndex = 58;
            this.comboBoxPN.SelectedIndexChanged += new System.EventHandler(this.comboBoxPN_SelectedIndexChanged);
            this.comboBoxPN.SelectionChangeCommitted += new System.EventHandler(this.comboBoxPN_SelectionChangeCommitted);
            this.comboBoxPN.Click += new System.EventHandler(this.comboBoxPN_Click);
            // 
            // comboBoxPType
            // 
            this.comboBoxPType.FormattingEnabled = true;
            this.comboBoxPType.Location = new System.Drawing.Point(21, 30);
            this.comboBoxPType.Name = "comboBoxPType";
            this.comboBoxPType.Size = new System.Drawing.Size(116, 20);
            this.comboBoxPType.TabIndex = 61;
            this.comboBoxPType.SelectionChangeCommitted += new System.EventHandler(this.comboBoxPType_SelectionChangeCommitted);
            // 
            // dataGridViewTotalData
            // 
            this.dataGridViewTotalData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTotalData.Location = new System.Drawing.Point(6, 6);
            this.dataGridViewTotalData.Name = "dataGridViewTotalData";
            this.dataGridViewTotalData.RowTemplate.Height = 23;
            this.dataGridViewTotalData.Size = new System.Drawing.Size(676, 336);
            this.dataGridViewTotalData.TabIndex = 52;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(21, 121);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(698, 386);
            this.tabControl1.TabIndex = 62;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.richInterfaceLog);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(690, 360);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Log";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // richInterfaceLog
            // 
            this.richInterfaceLog.BackColor = System.Drawing.SystemColors.Control;
            this.richInterfaceLog.Location = new System.Drawing.Point(6, 6);
            this.richInterfaceLog.Name = "richInterfaceLog";
            this.richInterfaceLog.Size = new System.Drawing.Size(676, 339);
            this.richInterfaceLog.TabIndex = 0;
            this.richInterfaceLog.Text = "";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dataGridViewTotalData);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(690, 360);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Result";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.richSqlLog);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(690, 360);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "LoGData";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // richSqlLog
            // 
            this.richSqlLog.BackColor = System.Drawing.Color.Yellow;
            this.richSqlLog.Location = new System.Drawing.Point(15, 8);
            this.richSqlLog.Name = "richSqlLog";
            this.richSqlLog.Size = new System.Drawing.Size(667, 339);
            this.richSqlLog.TabIndex = 1;
            this.richSqlLog.Text = "";
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.label12);
            this.tabPage4.Controls.Add(this.IccOffset);
            this.tabPage4.Controls.Add(this.label11);
            this.tabPage4.Controls.Add(this.button2);
            this.tabPage4.Controls.Add(this.VccOffset);
            this.tabPage4.Controls.Add(this.label10);
            this.tabPage4.Controls.Add(this.AttOffset4);
            this.tabPage4.Controls.Add(this.AttOffset3);
            this.tabPage4.Controls.Add(this.AttOffset2);
            this.tabPage4.Controls.Add(this.AttOffset1);
            this.tabPage4.Controls.Add(this.ScopeOffset4);
            this.tabPage4.Controls.Add(this.ScopeOffset3);
            this.tabPage4.Controls.Add(this.label9);
            this.tabPage4.Controls.Add(this.label8);
            this.tabPage4.Controls.Add(this.label7);
            this.tabPage4.Controls.Add(this.label6);
            this.tabPage4.Controls.Add(this.ScopeOffset2);
            this.tabPage4.Controls.Add(this.label5);
            this.tabPage4.Controls.Add(this.ScopeOffset1);
            this.tabPage4.Controls.Add(this.label4);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(690, 360);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Offset";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.Aquamarine;
            this.label12.Location = new System.Drawing.Point(396, 114);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(74, 23);
            this.label12.TabIndex = 19;
            this.label12.Text = "IccOffset:";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // IccOffset
            // 
            this.IccOffset.Location = new System.Drawing.Point(491, 116);
            this.IccOffset.Name = "IccOffset";
            this.IccOffset.Size = new System.Drawing.Size(72, 21);
            this.IccOffset.TabIndex = 18;
            this.IccOffset.Text = "0";
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.Aquamarine;
            this.label11.Location = new System.Drawing.Point(396, 75);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(74, 23);
            this.label11.TabIndex = 17;
            this.label11.Text = "VccOffset:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(458, 290);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(105, 28);
            this.button2.TabIndex = 16;
            this.button2.Text = "ConfigOffSet";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // VccOffset
            // 
            this.VccOffset.Location = new System.Drawing.Point(491, 77);
            this.VccOffset.Name = "VccOffset";
            this.VccOffset.Size = new System.Drawing.Size(72, 21);
            this.VccOffset.TabIndex = 15;
            this.VccOffset.Text = "0";
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.Aquamarine;
            this.label10.Location = new System.Drawing.Point(489, 38);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(74, 23);
            this.label10.TabIndex = 14;
            this.label10.Text = "Powersupply";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AttOffset4
            // 
            this.AttOffset4.Location = new System.Drawing.Point(213, 216);
            this.AttOffset4.Name = "AttOffset4";
            this.AttOffset4.Size = new System.Drawing.Size(72, 21);
            this.AttOffset4.TabIndex = 13;
            this.AttOffset4.Text = "-6.7";
            // 
            // AttOffset3
            // 
            this.AttOffset3.Location = new System.Drawing.Point(213, 172);
            this.AttOffset3.Name = "AttOffset3";
            this.AttOffset3.Size = new System.Drawing.Size(72, 21);
            this.AttOffset3.TabIndex = 12;
            this.AttOffset3.Text = "-5.5";
            // 
            // AttOffset2
            // 
            this.AttOffset2.Location = new System.Drawing.Point(213, 129);
            this.AttOffset2.Name = "AttOffset2";
            this.AttOffset2.Size = new System.Drawing.Size(72, 21);
            this.AttOffset2.TabIndex = 11;
            this.AttOffset2.Text = "-5.3";
            // 
            // AttOffset1
            // 
            this.AttOffset1.Location = new System.Drawing.Point(213, 86);
            this.AttOffset1.Name = "AttOffset1";
            this.AttOffset1.Size = new System.Drawing.Size(72, 21);
            this.AttOffset1.TabIndex = 10;
            this.AttOffset1.Text = "-5.8";
            // 
            // ScopeOffset4
            // 
            this.ScopeOffset4.Location = new System.Drawing.Point(101, 216);
            this.ScopeOffset4.Name = "ScopeOffset4";
            this.ScopeOffset4.Size = new System.Drawing.Size(72, 21);
            this.ScopeOffset4.TabIndex = 9;
            this.ScopeOffset4.Text = "3";
            // 
            // ScopeOffset3
            // 
            this.ScopeOffset3.Location = new System.Drawing.Point(101, 174);
            this.ScopeOffset3.Name = "ScopeOffset3";
            this.ScopeOffset3.Size = new System.Drawing.Size(72, 21);
            this.ScopeOffset3.TabIndex = 8;
            this.ScopeOffset3.Text = "3";
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.Aquamarine;
            this.label9.Location = new System.Drawing.Point(21, 214);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(74, 23);
            this.label9.TabIndex = 7;
            this.label9.Text = "Channel4:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.Aquamarine;
            this.label8.Location = new System.Drawing.Point(21, 172);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(74, 23);
            this.label8.TabIndex = 6;
            this.label8.Text = "Channel3:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Aquamarine;
            this.label7.Location = new System.Drawing.Point(21, 127);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(74, 23);
            this.label7.TabIndex = 5;
            this.label7.Text = "Channel2:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Aquamarine;
            this.label6.Location = new System.Drawing.Point(21, 86);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 23);
            this.label6.TabIndex = 4;
            this.label6.Text = "Channel1:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ScopeOffset2
            // 
            this.ScopeOffset2.Location = new System.Drawing.Point(101, 127);
            this.ScopeOffset2.Name = "ScopeOffset2";
            this.ScopeOffset2.Size = new System.Drawing.Size(72, 21);
            this.ScopeOffset2.TabIndex = 3;
            this.ScopeOffset2.Text = "3.1";
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Aquamarine;
            this.label5.Location = new System.Drawing.Point(211, 47);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 23);
            this.label5.TabIndex = 2;
            this.label5.Text = "ATT";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ScopeOffset1
            // 
            this.ScopeOffset1.Location = new System.Drawing.Point(101, 86);
            this.ScopeOffset1.Name = "ScopeOffset1";
            this.ScopeOffset1.Size = new System.Drawing.Size(72, 21);
            this.ScopeOffset1.TabIndex = 1;
            this.ScopeOffset1.Text = "3.1";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Aquamarine;
            this.label4.Location = new System.Drawing.Point(99, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 23);
            this.label4.TabIndex = 0;
            this.label4.Text = "Scope";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // comboBoxTestPlan
            // 
            this.comboBoxTestPlan.FormattingEnabled = true;
            this.comboBoxTestPlan.Location = new System.Drawing.Point(275, 30);
            this.comboBoxTestPlan.Name = "comboBoxTestPlan";
            this.comboBoxTestPlan.Size = new System.Drawing.Size(116, 20);
            this.comboBoxTestPlan.TabIndex = 64;
   
            this.comboBoxTestPlan.SelectionChangeCommitted += new System.EventHandler(this.comboBoxTestPlan_SelectionChangeCommitted);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(30, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 65;
            this.label1.Text = "ProductType:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.Control;
            this.label2.Location = new System.Drawing.Point(169, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 66;
            this.label2.Text = "ProductName:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.Control;
            this.label3.Location = new System.Drawing.Point(282, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 67;
            this.label3.Text = "TestPlan:";
            // 
            // textBoxSN
            // 
            this.textBoxSN.Enabled = false;
            this.textBoxSN.Location = new System.Drawing.Point(21, 63);
            this.textBoxSN.Name = "textBoxSN";
            this.textBoxSN.Size = new System.Drawing.Size(116, 21);
            this.textBoxSN.TabIndex = 73;
            this.textBoxSN.Text = "A01";
            // 
            // labelResult
            // 
            this.labelResult.BackColor = System.Drawing.SystemColors.Control;
            this.labelResult.Font = new System.Drawing.Font("宋体", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelResult.Location = new System.Drawing.Point(503, 9);
            this.labelResult.Name = "labelResult";
            this.labelResult.Size = new System.Drawing.Size(204, 112);
            this.labelResult.TabIndex = 76;
            this.labelResult.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(746, 274);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(75, 23);
            this.button8.TabIndex = 78;
            this.button8.Text = "Export";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // labelShow
            // 
            this.labelShow.BackColor = System.Drawing.SystemColors.Control;
            this.labelShow.Location = new System.Drawing.Point(23, 545);
            this.labelShow.Name = "labelShow";
            this.labelShow.Size = new System.Drawing.Size(420, 23);
            this.labelShow.TabIndex = 79;
            this.labelShow.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // progress
            // 
            this.progress.BackColor = System.Drawing.SystemColors.Control;
            this.progress.Location = new System.Drawing.Point(449, 543);
            this.progress.Name = "progress";
            this.progress.Size = new System.Drawing.Size(266, 23);
            this.progress.TabIndex = 80;
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(745, 366);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 81;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(746, 205);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 82;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(746, 450);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 83;
            this.button4.Text = "button4";
            this.button4.UseVisualStyleBackColor = true;
        
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(858, 574);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.progress);
            this.Controls.Add(this.labelShow);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.labelResult);
            this.Controls.Add(this.textBoxSN);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxTestPlan);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.comboBoxPType);
            this.Controls.Add(this.comboBoxPN);
            this.Controls.Add(this.button_Test);
            this.Controls.Add(this.button_Config);
            this.Name = "Form1";
            this.Text = "Test";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTotalData)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_Test;
        private System.Windows.Forms.Button button_Config;
        private System.Windows.Forms.ComboBox comboBoxPN;
        private System.Windows.Forms.ComboBox comboBoxPType;
        private System.Windows.Forms.DataGridView dataGridViewTotalData;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.RichTextBox richInterfaceLog;
        private System.Windows.Forms.ComboBox comboBoxTestPlan;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox VccOffset;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox AttOffset4;
        private System.Windows.Forms.TextBox AttOffset3;
        private System.Windows.Forms.TextBox AttOffset2;
        private System.Windows.Forms.TextBox AttOffset1;
        private System.Windows.Forms.TextBox ScopeOffset4;
        private System.Windows.Forms.TextBox ScopeOffset3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox ScopeOffset2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox ScopeOffset1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxSN;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox IccOffset;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.RichTextBox richSqlLog;
        private System.Windows.Forms.Label labelResult;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Label labelShow;
        private System.Windows.Forms.ProgressBar progress;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
    }
}


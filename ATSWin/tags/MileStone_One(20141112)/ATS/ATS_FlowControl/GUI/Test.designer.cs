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
            this.textBoxSN = new System.Windows.Forms.TextBox();
            this.comboBoxPN = new System.Windows.Forms.ComboBox();
            this.comboBoxPType = new System.Windows.Forms.ComboBox();
            this.comboBoxTestPlan = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.progress = new System.Windows.Forms.ProgressBar();
            this.labelShow = new System.Windows.Forms.Label();
            this.button_Config = new System.Windows.Forms.Button();
            this.button_Test = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.richInterfaceLog = new System.Windows.Forms.RichTextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dataGridViewTotalData = new System.Windows.Forms.DataGridView();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.buttonOffset = new System.Windows.Forms.Button();
            this.AttOffset4 = new System.Windows.Forms.TextBox();
            this.AttOffset3 = new System.Windows.Forms.TextBox();
            this.AttOffset2 = new System.Windows.Forms.TextBox();
            this.AttOffset1 = new System.Windows.Forms.TextBox();
            this.ScopeOffset4 = new System.Windows.Forms.TextBox();
            this.ScopeOffset3 = new System.Windows.Forms.TextBox();
            this.ScopeOffset2 = new System.Windows.Forms.TextBox();
            this.ScopeOffset1 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.labelProgress = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBoxFW = new System.Windows.Forms.TextBox();
            this.labelFw = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.buttonStop = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTotalData)).BeginInit();
            this.tabPage4.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
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
            this.label12.Location = new System.Drawing.Point(132, 19);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(626, 67);
            this.label12.TabIndex = 85;
            this.label12.Text = "Auto Test System";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxSN
            // 
            this.textBoxSN.Enabled = false;
            this.textBoxSN.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxSN.Location = new System.Drawing.Point(199, 106);
            this.textBoxSN.Name = "textBoxSN";
            this.textBoxSN.Size = new System.Drawing.Size(192, 26);
            this.textBoxSN.TabIndex = 73;
            this.textBoxSN.Text = "A01234567891234";
            // 
            // comboBoxPN
            // 
            this.comboBoxPN.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBoxPN.FormattingEnabled = true;
            this.comboBoxPN.Location = new System.Drawing.Point(154, 66);
            this.comboBoxPN.Name = "comboBoxPN";
            this.comboBoxPN.Size = new System.Drawing.Size(116, 22);
            this.comboBoxPN.TabIndex = 58;
            this.comboBoxPN.TextChanged += new System.EventHandler(this.comboBoxPN_TextChanged);
            // 
            // comboBoxPType
            // 
            this.comboBoxPType.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBoxPType.FormattingEnabled = true;
            this.comboBoxPType.Location = new System.Drawing.Point(24, 66);
            this.comboBoxPType.Name = "comboBoxPType";
            this.comboBoxPType.Size = new System.Drawing.Size(121, 22);
            this.comboBoxPType.TabIndex = 61;
            this.comboBoxPType.TextChanged += new System.EventHandler(this.comboBoxPType_TextChanged);
            // 
            // comboBoxTestPlan
            // 
            this.comboBoxTestPlan.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBoxTestPlan.FormattingEnabled = true;
            this.comboBoxTestPlan.Location = new System.Drawing.Point(276, 66);
            this.comboBoxTestPlan.Name = "comboBoxTestPlan";
            this.comboBoxTestPlan.Size = new System.Drawing.Size(116, 22);
            this.comboBoxTestPlan.TabIndex = 64;
            this.comboBoxTestPlan.TextChanged += new System.EventHandler(this.comboBoxTestPlan_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Window;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(21, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 16);
            this.label1.TabIndex = 65;
            this.label1.Text = "ProductType:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.Window;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(154, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 16);
            this.label2.TabIndex = 66;
            this.label2.Text = "ProductName:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.Window;
            this.label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(297, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 16);
            this.label3.TabIndex = 67;
            this.label3.Text = "TestPlan:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(221)))), ((int)(((byte)(238)))));
            this.label10.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(158, 110);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(35, 16);
            this.label10.TabIndex = 81;
            this.label10.Text = "SN:";
            // 
            // progress
            // 
            this.progress.BackColor = System.Drawing.Color.Aqua;
            this.progress.Location = new System.Drawing.Point(559, 5);
            this.progress.Name = "progress";
            this.progress.Size = new System.Drawing.Size(179, 27);
            this.progress.TabIndex = 80;
            // 
            // labelShow
            // 
            this.labelShow.BackColor = System.Drawing.SystemColors.Control;
            this.labelShow.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelShow.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelShow.Location = new System.Drawing.Point(7, 7);
            this.labelShow.Name = "labelShow";
            this.labelShow.Size = new System.Drawing.Size(546, 23);
            this.labelShow.TabIndex = 79;
            this.labelShow.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button_Config
            // 
            this.button_Config.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Config.BackColor = System.Drawing.SystemColors.Control;
            this.button_Config.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_Config.ForeColor = System.Drawing.Color.Blue;
            this.button_Config.Location = new System.Drawing.Point(448, 66);
            this.button_Config.Name = "button_Config";
            this.button_Config.Size = new System.Drawing.Size(104, 31);
            this.button_Config.TabIndex = 41;
            this.button_Config.Text = "Config";
            this.button_Config.UseVisualStyleBackColor = false;
            this.button_Config.Click += new System.EventHandler(this.button_Config_Click);
            // 
            // button_Test
            // 
            this.button_Test.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Test.BackColor = System.Drawing.SystemColors.Control;
            this.button_Test.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_Test.ForeColor = System.Drawing.Color.Blue;
            this.button_Test.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_Test.Location = new System.Drawing.Point(573, 66);
            this.button_Test.Name = "button_Test";
            this.button_Test.Size = new System.Drawing.Size(104, 31);
            this.button_Test.TabIndex = 42;
            this.button_Test.Text = "Test";
            this.button_Test.UseVisualStyleBackColor = false;
            this.button_Test.Click += new System.EventHandler(this.button_Test_Click);
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnExport.ForeColor = System.Drawing.Color.Blue;
            this.btnExport.Location = new System.Drawing.Point(702, 66);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(104, 31);
            this.btnExport.TabIndex = 78;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabControl1.Location = new System.Drawing.Point(24, 151);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(798, 312);
            this.tabControl1.TabIndex = 63;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.richInterfaceLog);
            this.tabPage1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(790, 282);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Log";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // richInterfaceLog
            // 
            this.richInterfaceLog.BackColor = System.Drawing.SystemColors.Control;
            this.richInterfaceLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richInterfaceLog.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.richInterfaceLog.Location = new System.Drawing.Point(3, 3);
            this.richInterfaceLog.Name = "richInterfaceLog";
            this.richInterfaceLog.Size = new System.Drawing.Size(784, 276);
            this.richInterfaceLog.TabIndex = 0;
            this.richInterfaceLog.Text = "";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dataGridViewTotalData);
            this.tabPage2.Location = new System.Drawing.Point(4, 26);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(790, 282);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Result";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dataGridViewTotalData
            // 
            this.dataGridViewTotalData.BackgroundColor = System.Drawing.Color.LightCyan;
            this.dataGridViewTotalData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTotalData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewTotalData.Location = new System.Drawing.Point(3, 3);
            this.dataGridViewTotalData.Name = "dataGridViewTotalData";
            this.dataGridViewTotalData.RowTemplate.Height = 23;
            this.dataGridViewTotalData.Size = new System.Drawing.Size(784, 276);
            this.dataGridViewTotalData.TabIndex = 52;
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage4.Controls.Add(this.buttonOffset);
            this.tabPage4.Controls.Add(this.AttOffset4);
            this.tabPage4.Controls.Add(this.AttOffset3);
            this.tabPage4.Controls.Add(this.AttOffset2);
            this.tabPage4.Controls.Add(this.AttOffset1);
            this.tabPage4.Controls.Add(this.ScopeOffset4);
            this.tabPage4.Controls.Add(this.ScopeOffset3);
            this.tabPage4.Controls.Add(this.ScopeOffset2);
            this.tabPage4.Controls.Add(this.ScopeOffset1);
            this.tabPage4.Controls.Add(this.label9);
            this.tabPage4.Controls.Add(this.label8);
            this.tabPage4.Controls.Add(this.label7);
            this.tabPage4.Controls.Add(this.label6);
            this.tabPage4.Controls.Add(this.label5);
            this.tabPage4.Controls.Add(this.label4);
            this.tabPage4.Font = new System.Drawing.Font("华文彩云", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabPage4.Location = new System.Drawing.Point(4, 26);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(790, 282);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Offset";
            // 
            // buttonOffset
            // 
            this.buttonOffset.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonOffset.ForeColor = System.Drawing.Color.Blue;
            this.buttonOffset.Location = new System.Drawing.Point(394, 199);
            this.buttonOffset.Name = "buttonOffset";
            this.buttonOffset.Size = new System.Drawing.Size(130, 39);
            this.buttonOffset.TabIndex = 16;
            this.buttonOffset.Text = "ConfigOffSet";
            this.buttonOffset.UseVisualStyleBackColor = true;
            this.buttonOffset.Click += new System.EventHandler(this.buttonOffset_Click);
            // 
            // AttOffset4
            // 
            this.AttOffset4.BackColor = System.Drawing.Color.LightCyan;
            this.AttOffset4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AttOffset4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.AttOffset4.Location = new System.Drawing.Point(253, 199);
            this.AttOffset4.Name = "AttOffset4";
            this.AttOffset4.Size = new System.Drawing.Size(91, 26);
            this.AttOffset4.TabIndex = 13;
            this.AttOffset4.Text = "-6.7";
            this.AttOffset4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // AttOffset3
            // 
            this.AttOffset3.BackColor = System.Drawing.Color.LightCyan;
            this.AttOffset3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AttOffset3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.AttOffset3.Location = new System.Drawing.Point(253, 169);
            this.AttOffset3.Name = "AttOffset3";
            this.AttOffset3.Size = new System.Drawing.Size(91, 26);
            this.AttOffset3.TabIndex = 12;
            this.AttOffset3.Text = "-5.5";
            this.AttOffset3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // AttOffset2
            // 
            this.AttOffset2.BackColor = System.Drawing.Color.LightCyan;
            this.AttOffset2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AttOffset2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.AttOffset2.Location = new System.Drawing.Point(253, 139);
            this.AttOffset2.Name = "AttOffset2";
            this.AttOffset2.Size = new System.Drawing.Size(91, 26);
            this.AttOffset2.TabIndex = 11;
            this.AttOffset2.Text = "-5.3";
            this.AttOffset2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // AttOffset1
            // 
            this.AttOffset1.BackColor = System.Drawing.Color.LightCyan;
            this.AttOffset1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AttOffset1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.AttOffset1.Location = new System.Drawing.Point(253, 109);
            this.AttOffset1.Name = "AttOffset1";
            this.AttOffset1.Size = new System.Drawing.Size(91, 26);
            this.AttOffset1.TabIndex = 10;
            this.AttOffset1.Text = "-5.8";
            this.AttOffset1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ScopeOffset4
            // 
            this.ScopeOffset4.BackColor = System.Drawing.Color.LightCyan;
            this.ScopeOffset4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ScopeOffset4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ScopeOffset4.Location = new System.Drawing.Point(155, 199);
            this.ScopeOffset4.Name = "ScopeOffset4";
            this.ScopeOffset4.Size = new System.Drawing.Size(91, 26);
            this.ScopeOffset4.TabIndex = 9;
            this.ScopeOffset4.Text = "3";
            this.ScopeOffset4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ScopeOffset3
            // 
            this.ScopeOffset3.BackColor = System.Drawing.Color.LightCyan;
            this.ScopeOffset3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ScopeOffset3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ScopeOffset3.Location = new System.Drawing.Point(155, 169);
            this.ScopeOffset3.Name = "ScopeOffset3";
            this.ScopeOffset3.Size = new System.Drawing.Size(91, 26);
            this.ScopeOffset3.TabIndex = 8;
            this.ScopeOffset3.Text = "3";
            this.ScopeOffset3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ScopeOffset2
            // 
            this.ScopeOffset2.BackColor = System.Drawing.Color.LightCyan;
            this.ScopeOffset2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ScopeOffset2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ScopeOffset2.Location = new System.Drawing.Point(155, 139);
            this.ScopeOffset2.Name = "ScopeOffset2";
            this.ScopeOffset2.Size = new System.Drawing.Size(91, 26);
            this.ScopeOffset2.TabIndex = 3;
            this.ScopeOffset2.Text = "3.1";
            this.ScopeOffset2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ScopeOffset1
            // 
            this.ScopeOffset1.BackColor = System.Drawing.Color.LightCyan;
            this.ScopeOffset1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ScopeOffset1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ScopeOffset1.Location = new System.Drawing.Point(155, 109);
            this.ScopeOffset1.Name = "ScopeOffset1";
            this.ScopeOffset1.Size = new System.Drawing.Size(91, 26);
            this.ScopeOffset1.TabIndex = 1;
            this.ScopeOffset1.Text = "3.1";
            this.ScopeOffset1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(243)))), ((int)(((byte)(251)))));
            this.label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label9.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(59, 199);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(91, 27);
            this.label9.TabIndex = 7;
            this.label9.Text = "Channel4";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(243)))), ((int)(((byte)(251)))));
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label8.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(59, 169);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(91, 27);
            this.label8.TabIndex = 6;
            this.label8.Text = "Channel3";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(243)))), ((int)(((byte)(251)))));
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label7.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(59, 139);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(91, 27);
            this.label7.TabIndex = 5;
            this.label7.Text = "Channel2";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(243)))), ((int)(((byte)(251)))));
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(59, 109);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(91, 27);
            this.label6.TabIndex = 4;
            this.label6.Text = "Channel1";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(243)))), ((int)(((byte)(251)))));
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(248, 75);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 27);
            this.label5.TabIndex = 2;
            this.label5.Text = "lightsource";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(243)))), ((int)(((byte)(251)))));
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(154, 75);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 27);
            this.label4.TabIndex = 0;
            this.label4.Text = "Scope";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelProgress
            // 
            this.labelProgress.AutoSize = true;
            this.labelProgress.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelProgress.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelProgress.Location = new System.Drawing.Point(744, 8);
            this.labelProgress.Name = "labelProgress";
            this.labelProgress.Size = new System.Drawing.Size(31, 21);
            this.labelProgress.TabIndex = 0;
            this.labelProgress.Text = "0%";
            this.labelProgress.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(221)))), ((int)(((byte)(238)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.buttonStop);
            this.panel1.Controls.Add(this.textBoxFW);
            this.panel1.Controls.Add(this.labelFw);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.comboBoxPType);
            this.panel1.Controls.Add(this.btnExport);
            this.panel1.Controls.Add(this.comboBoxPN);
            this.panel1.Controls.Add(this.button_Test);
            this.panel1.Controls.Add(this.button_Config);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.comboBoxTestPlan);
            this.panel1.Controls.Add(this.textBoxSN);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Location = new System.Drawing.Point(21, 96);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(834, 534);
            this.panel1.TabIndex = 87;
            // 
            // textBoxFW
            // 
            this.textBoxFW.Enabled = false;
            this.textBoxFW.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxFW.Location = new System.Drawing.Point(89, 106);
            this.textBoxFW.Name = "textBoxFW";
            this.textBoxFW.Size = new System.Drawing.Size(56, 26);
            this.textBoxFW.TabIndex = 83;
            this.textBoxFW.Text = "A012";
            // 
            // labelFw
            // 
            this.labelFw.AutoSize = true;
            this.labelFw.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(221)))), ((int)(((byte)(238)))));
            this.labelFw.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelFw.Location = new System.Drawing.Point(21, 110);
            this.labelFw.Name = "labelFw";
            this.labelFw.Size = new System.Drawing.Size(62, 16);
            this.labelFw.TabIndex = 84;
            this.labelFw.Text = "FwRev:";
            // 
            // panel2
            // 
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.progress);
            this.panel2.Controls.Add(this.labelShow);
            this.panel2.Controls.Add(this.labelProgress);
            this.panel2.Location = new System.Drawing.Point(24, 483);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(798, 39);
            this.panel2.TabIndex = 82;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("黑体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.Location = new System.Drawing.Point(710, 54);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 20);
            this.label11.TabIndex = 88;
            this.label11.Text = "V1.0";
            // 
            // buttonStop
            // 
            this.buttonStop.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonStop.BackColor = System.Drawing.SystemColors.Control;
            this.buttonStop.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonStop.ForeColor = System.Drawing.Color.Blue;
            this.buttonStop.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonStop.Location = new System.Drawing.Point(573, 110);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(104, 31);
            this.buttonStop.TabIndex = 85;
            this.buttonStop.Text = "Stop";
            this.buttonStop.UseVisualStyleBackColor = false;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // FormATS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(237)))), ((int)(((byte)(247)))));
            this.ClientSize = new System.Drawing.Size(883, 665);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label12);
            this.Name = "FormATS";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ATS";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.FormATS_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTotalData)).EndInit();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textBoxSN;
        private System.Windows.Forms.ComboBox comboBoxPN;
        private System.Windows.Forms.ComboBox comboBoxPType;
        private System.Windows.Forms.ComboBox comboBoxTestPlan;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ProgressBar progress;
        private System.Windows.Forms.Label labelShow;
        private System.Windows.Forms.Button button_Config;
        private System.Windows.Forms.Button button_Test;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.RichTextBox richInterfaceLog;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dataGridViewTotalData;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Button buttonOffset;
        private System.Windows.Forms.TextBox AttOffset4;
        private System.Windows.Forms.TextBox AttOffset3;
        private System.Windows.Forms.TextBox AttOffset2;
        private System.Windows.Forms.TextBox AttOffset1;
        private System.Windows.Forms.TextBox ScopeOffset4;
        private System.Windows.Forms.TextBox ScopeOffset3;
        private System.Windows.Forms.TextBox ScopeOffset2;
        private System.Windows.Forms.TextBox ScopeOffset1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelProgress;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBoxFW;
        private System.Windows.Forms.Label labelFw;
        private System.Windows.Forms.Button buttonStop;
    }
}


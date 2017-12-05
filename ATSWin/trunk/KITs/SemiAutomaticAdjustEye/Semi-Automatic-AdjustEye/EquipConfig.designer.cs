namespace Semi_Automatic_AdjustEye
{
    partial class EquipConfig
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
            this.lableGPIB = new System.Windows.Forms.Label();
            this.tbPowerGPIB = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbPower = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chk_Power = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.chk_OpticalSwitch = new System.Windows.Forms.CheckBox();
            this.tbAQSwitchSlot = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbAQSwitchGPIB = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.chk_Scope = new System.Windows.Forms.CheckBox();
            this.tbDCADataRate = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.cbTxDCAFilterSwitch = new System.Windows.Forms.CheckBox();
            this.tbTxDCAoMask = new System.Windows.Forms.TextBox();
            this.label97 = new System.Windows.Forms.Label();
            this.cbTxDCAwavelength = new System.Windows.Forms.ComboBox();
            this.label90 = new System.Windows.Forms.Label();
            this.label91 = new System.Windows.Forms.Label();
            this.tbTxDCAoChannel = new System.Windows.Forms.TextBox();
            this.label94 = new System.Windows.Forms.Label();
            this.tbFLEX86100GPIB = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.tbTPOGPIB = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.chk_TPO4300 = new System.Windows.Forms.CheckBox();
            this.btConfirmConfig = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // lableGPIB
            // 
            this.lableGPIB.AutoSize = true;
            this.lableGPIB.Location = new System.Drawing.Point(11, 66);
            this.lableGPIB.Name = "lableGPIB";
            this.lableGPIB.Size = new System.Drawing.Size(29, 12);
            this.lableGPIB.TabIndex = 2;
            this.lableGPIB.Text = "GPIB";
            // 
            // tbPowerGPIB
            // 
            this.tbPowerGPIB.Location = new System.Drawing.Point(44, 61);
            this.tbPowerGPIB.Name = "tbPowerGPIB";
            this.tbPowerGPIB.Size = new System.Drawing.Size(69, 21);
            this.tbPowerGPIB.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cbPower);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.tbPowerGPIB);
            this.panel1.Controls.Add(this.chk_Power);
            this.panel1.Controls.Add(this.lableGPIB);
            this.panel1.Location = new System.Drawing.Point(21, 16);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(130, 91);
            this.panel1.TabIndex = 4;
            // 
            // cbPower
            // 
            this.cbPower.FormattingEnabled = true;
            this.cbPower.Items.AddRange(new object[] {
            "E3631",
            "DP811A"});
            this.cbPower.Location = new System.Drawing.Point(44, 35);
            this.cbPower.Name = "cbPower";
            this.cbPower.Size = new System.Drawing.Size(69, 20);
            this.cbPower.TabIndex = 166;
            this.cbPower.SelectedIndexChanged += new System.EventHandler(this.cbPower_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 165;
            this.label1.Text = "电源";
            // 
            // chk_Power
            // 
            this.chk_Power.AutoSize = true;
            this.chk_Power.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chk_Power.Location = new System.Drawing.Point(10, 11);
            this.chk_Power.Name = "chk_Power";
            this.chk_Power.Size = new System.Drawing.Size(67, 20);
            this.chk_Power.TabIndex = 11;
            this.chk_Power.Text = "Power";
            this.chk_Power.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.chk_OpticalSwitch);
            this.panel2.Controls.Add(this.tbAQSwitchSlot);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.tbAQSwitchGPIB);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Location = new System.Drawing.Point(308, 16);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(260, 91);
            this.panel2.TabIndex = 5;
            // 
            // chk_OpticalSwitch
            // 
            this.chk_OpticalSwitch.AutoSize = true;
            this.chk_OpticalSwitch.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chk_OpticalSwitch.Location = new System.Drawing.Point(18, 14);
            this.chk_OpticalSwitch.Name = "chk_OpticalSwitch";
            this.chk_OpticalSwitch.Size = new System.Drawing.Size(131, 20);
            this.chk_OpticalSwitch.TabIndex = 14;
            this.chk_OpticalSwitch.Text = "OpticalSwitch";
            this.chk_OpticalSwitch.UseVisualStyleBackColor = true;
            // 
            // tbAQSwitchSlot
            // 
            this.tbAQSwitchSlot.Location = new System.Drawing.Point(167, 48);
            this.tbAQSwitchSlot.Name = "tbAQSwitchSlot";
            this.tbAQSwitchSlot.Size = new System.Drawing.Size(61, 21);
            this.tbAQSwitchSlot.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(136, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "槽位";
            // 
            // tbAQSwitchGPIB
            // 
            this.tbAQSwitchGPIB.Location = new System.Drawing.Point(51, 48);
            this.tbAQSwitchGPIB.Name = "tbAQSwitchGPIB";
            this.tbAQSwitchGPIB.Size = new System.Drawing.Size(72, 21);
            this.tbAQSwitchGPIB.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "GPIB";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.chk_Scope);
            this.panel3.Controls.Add(this.tbDCADataRate);
            this.panel3.Controls.Add(this.label13);
            this.panel3.Controls.Add(this.cbTxDCAFilterSwitch);
            this.panel3.Controls.Add(this.tbTxDCAoMask);
            this.panel3.Controls.Add(this.label97);
            this.panel3.Controls.Add(this.cbTxDCAwavelength);
            this.panel3.Controls.Add(this.label90);
            this.panel3.Controls.Add(this.label91);
            this.panel3.Controls.Add(this.tbTxDCAoChannel);
            this.panel3.Controls.Add(this.label94);
            this.panel3.Controls.Add(this.tbFLEX86100GPIB);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Location = new System.Drawing.Point(21, 113);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(547, 141);
            this.panel3.TabIndex = 6;
            // 
            // chk_Scope
            // 
            this.chk_Scope.AutoSize = true;
            this.chk_Scope.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chk_Scope.Location = new System.Drawing.Point(14, 13);
            this.chk_Scope.Name = "chk_Scope";
            this.chk_Scope.Size = new System.Drawing.Size(99, 20);
            this.chk_Scope.TabIndex = 12;
            this.chk_Scope.Text = "FLEX86100";
            this.chk_Scope.UseVisualStyleBackColor = true;
            // 
            // tbDCADataRate
            // 
            this.tbDCADataRate.Location = new System.Drawing.Point(194, 77);
            this.tbDCADataRate.Name = "tbDCADataRate";
            this.tbDCADataRate.Size = new System.Drawing.Size(79, 21);
            this.tbDCADataRate.TabIndex = 164;
            this.tbDCADataRate.Text = "1";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(125, 81);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(65, 12);
            this.label13.TabIndex = 163;
            this.label13.Text = "示波器速率";
            // 
            // cbTxDCAFilterSwitch
            // 
            this.cbTxDCAFilterSwitch.AutoSize = true;
            this.cbTxDCAFilterSwitch.Location = new System.Drawing.Point(23, 80);
            this.cbTxDCAFilterSwitch.Name = "cbTxDCAFilterSwitch";
            this.cbTxDCAFilterSwitch.Size = new System.Drawing.Size(60, 16);
            this.cbTxDCAFilterSwitch.TabIndex = 162;
            this.cbTxDCAFilterSwitch.Text = "滤波？";
            this.cbTxDCAFilterSwitch.UseVisualStyleBackColor = true;
            // 
            // tbTxDCAoMask
            // 
            this.tbTxDCAoMask.Location = new System.Drawing.Point(81, 106);
            this.tbTxDCAoMask.Name = "tbTxDCAoMask";
            this.tbTxDCAoMask.Size = new System.Drawing.Size(446, 21);
            this.tbTxDCAoMask.TabIndex = 154;
            this.tbTxDCAoMask.Text = "C:\\Program Files\\Keysight\\FlexDCA\\Demo\\Masks\\Ethernet\\025.78125 - 100GBASE-LR4_Tx" +
                "_Optical_D31.mskx";
            // 
            // label97
            // 
            this.label97.AutoSize = true;
            this.label97.Location = new System.Drawing.Point(12, 110);
            this.label97.Name = "label97";
            this.label97.Size = new System.Drawing.Size(65, 12);
            this.label97.TabIndex = 153;
            this.label97.Text = "光眼图模板";
            // 
            // cbTxDCAwavelength
            // 
            this.cbTxDCAwavelength.FormattingEnabled = true;
            this.cbTxDCAwavelength.Items.AddRange(new object[] {
            "850",
            "1310",
            "1550"});
            this.cbTxDCAwavelength.Location = new System.Drawing.Point(310, 46);
            this.cbTxDCAwavelength.Name = "cbTxDCAwavelength";
            this.cbTxDCAwavelength.Size = new System.Drawing.Size(67, 20);
            this.cbTxDCAwavelength.TabIndex = 152;
            // 
            // label90
            // 
            this.label90.AutoSize = true;
            this.label90.Location = new System.Drawing.Point(382, 52);
            this.label90.Name = "label90";
            this.label90.Size = new System.Drawing.Size(17, 12);
            this.label90.TabIndex = 150;
            this.label90.Text = "nm";
            // 
            // label91
            // 
            this.label91.AutoSize = true;
            this.label91.Location = new System.Drawing.Point(276, 50);
            this.label91.Name = "label91";
            this.label91.Size = new System.Drawing.Size(29, 12);
            this.label91.TabIndex = 151;
            this.label91.Text = "波长";
            // 
            // tbTxDCAoChannel
            // 
            this.tbTxDCAoChannel.Location = new System.Drawing.Point(194, 46);
            this.tbTxDCAoChannel.Name = "tbTxDCAoChannel";
            this.tbTxDCAoChannel.Size = new System.Drawing.Size(66, 21);
            this.tbTxDCAoChannel.TabIndex = 149;
            this.tbTxDCAoChannel.Text = "1A";
            // 
            // label94
            // 
            this.label94.AutoSize = true;
            this.label94.Location = new System.Drawing.Point(136, 50);
            this.label94.Name = "label94";
            this.label94.Size = new System.Drawing.Size(53, 12);
            this.label94.TabIndex = 148;
            this.label94.Text = "光口通道";
            // 
            // tbFLEX86100GPIB
            // 
            this.tbFLEX86100GPIB.Location = new System.Drawing.Point(44, 46);
            this.tbFLEX86100GPIB.Name = "tbFLEX86100GPIB";
            this.tbFLEX86100GPIB.Size = new System.Drawing.Size(61, 21);
            this.tbFLEX86100GPIB.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 51);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 2;
            this.label7.Text = "GPIB";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.tbTPOGPIB);
            this.panel4.Controls.Add(this.label9);
            this.panel4.Controls.Add(this.chk_TPO4300);
            this.panel4.Location = new System.Drawing.Point(169, 16);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(117, 91);
            this.panel4.TabIndex = 7;
            // 
            // tbTPOGPIB
            // 
            this.tbTPOGPIB.Location = new System.Drawing.Point(46, 49);
            this.tbTPOGPIB.Name = "tbTPOGPIB";
            this.tbTPOGPIB.Size = new System.Drawing.Size(61, 21);
            this.tbTPOGPIB.TabIndex = 3;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(15, 52);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 12);
            this.label9.TabIndex = 2;
            this.label9.Text = "GPIB";
            // 
            // chk_TPO4300
            // 
            this.chk_TPO4300.AutoSize = true;
            this.chk_TPO4300.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chk_TPO4300.Location = new System.Drawing.Point(17, 14);
            this.chk_TPO4300.Name = "chk_TPO4300";
            this.chk_TPO4300.Size = new System.Drawing.Size(83, 20);
            this.chk_TPO4300.TabIndex = 13;
            this.chk_TPO4300.Text = "TPO4300";
            this.chk_TPO4300.UseVisualStyleBackColor = true;
            // 
            // btConfirmConfig
            // 
            this.btConfirmConfig.Location = new System.Drawing.Point(229, 276);
            this.btConfirmConfig.Name = "btConfirmConfig";
            this.btConfirmConfig.Size = new System.Drawing.Size(75, 23);
            this.btConfirmConfig.TabIndex = 8;
            this.btConfirmConfig.Text = "配置";
            this.btConfirmConfig.UseVisualStyleBackColor = true;
            this.btConfirmConfig.Click += new System.EventHandler(this.btConfirmConfig_Click);
            // 
            // EquipConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(607, 307);
            this.Controls.Add(this.btConfirmConfig);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "EquipConfig";
            this.Text = "EquipConfig";
            this.Load += new System.EventHandler(this.EquipConfig_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lableGPIB;
        private System.Windows.Forms.TextBox tbPowerGPIB;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox tbAQSwitchSlot;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbAQSwitchGPIB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox tbFLEX86100GPIB;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox cbTxDCAFilterSwitch;
        private System.Windows.Forms.TextBox tbTxDCAoMask;
        private System.Windows.Forms.Label label97;
        private System.Windows.Forms.ComboBox cbTxDCAwavelength;
        private System.Windows.Forms.Label label90;
        private System.Windows.Forms.Label label91;
        private System.Windows.Forms.TextBox tbTxDCAoChannel;
        private System.Windows.Forms.Label label94;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox tbTPOGPIB;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btConfirmConfig;
        private System.Windows.Forms.TextBox tbDCADataRate;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.CheckBox chk_OpticalSwitch;
        private System.Windows.Forms.CheckBox chk_Power;
        private System.Windows.Forms.CheckBox chk_Scope;
        private System.Windows.Forms.CheckBox chk_TPO4300;
        private System.Windows.Forms.ComboBox cbPower;
        private System.Windows.Forms.Label label1;
    }
}
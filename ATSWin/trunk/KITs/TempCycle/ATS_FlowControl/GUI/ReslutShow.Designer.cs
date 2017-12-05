namespace ATS
{
    partial class ReslutShow
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
            this.pictureBoxResult = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxResult)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxResult
            // 
            this.pictureBoxResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxResult.Image = global::ATS.Properties.Resources.ok;
            this.pictureBoxResult.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxResult.Name = "pictureBoxResult";
            this.pictureBoxResult.Size = new System.Drawing.Size(602, 381);
            this.pictureBoxResult.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxResult.TabIndex = 0;
            this.pictureBoxResult.TabStop = false;
            this.pictureBoxResult.Click += new System.EventHandler(this.pictureBoxResult_Click);
            // 
            // ReslutShow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(602, 381);
            this.Controls.Add(this.pictureBoxResult);
            this.Name = "ReslutShow";
            this.Text = "ReslutShow";
            this.Click += new System.EventHandler(this.ReslutShow_Click);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxResult)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxResult;

    }
}
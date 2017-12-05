namespace Maintain
{
    partial class ShowUpdateState
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShowUpdateState));
            this.picShowUpdateState = new System.Windows.Forms.PictureBox();
            this.imgUpdateState = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.picShowUpdateState)).BeginInit();
            this.SuspendLayout();
            // 
            // picShowUpdateState
            // 
            this.picShowUpdateState.Location = new System.Drawing.Point(0, 0);
            this.picShowUpdateState.MaximumSize = new System.Drawing.Size(250, 200);
            this.picShowUpdateState.MinimumSize = new System.Drawing.Size(250, 200);
            this.picShowUpdateState.Name = "picShowUpdateState";
            this.picShowUpdateState.Size = new System.Drawing.Size(250, 200);
            this.picShowUpdateState.TabIndex = 0;
            this.picShowUpdateState.TabStop = false;
            // 
            // imgUpdateState
            // 
            this.imgUpdateState.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgUpdateState.ImageStream")));
            this.imgUpdateState.TransparentColor = System.Drawing.Color.Transparent;
            this.imgUpdateState.Images.SetKeyName(0, "success.bmp");
            this.imgUpdateState.Images.SetKeyName(1, "failure.bmp");
            // 
            // ShowUpdateState
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(250, 200);
            this.Controls.Add(this.picShowUpdateState);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(266, 238);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(266, 238);
            this.Name = "ShowUpdateState";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ShowUpdateState";
            ((System.ComponentModel.ISupportInitialize)(this.picShowUpdateState)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.PictureBox picShowUpdateState;
        public System.Windows.Forms.ImageList imgUpdateState;
    }
}
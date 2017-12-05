using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace ATS
{
    public delegate void ChangeFormColor(int progessValue);  
    public partial class ProgressForm : Form
    {
        public int i=0;
        private int progressValue=0;
        public ProgressForm()
        {
            InitializeComponent();
           // timer1.Enabled = true;
        }
        private void ProgressForm_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;

        }
        public void ShowProgressValue(int i)
        {
           
        }

        //private void timer1_Tick(object sender, EventArgs e)
        //{
        //    progressValue = i;
        //    progressBar1.Value = progressValue;
        //    progressBar1.Refresh();
        //    this.Text = progressValue + "%";
        //    Thread.Sleep(500);

        //    if (progressValue == 100)
        //    {
        //        this.Close();
        //    }
        //}
       
    }
}

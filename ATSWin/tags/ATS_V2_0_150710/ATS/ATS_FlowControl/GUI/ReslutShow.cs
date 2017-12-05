using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ATS
{
    public partial class ReslutShow : Form
    {
        private bool Resultflag = false;
        public ReslutShow()
        {
            InitializeComponent();
        }
        public void ShowReslut(bool Result)
        {
           
            Resultflag = Result;

            if (Resultflag)
            {
                pictureBoxResult.Image = Properties.Resources.ok;
            }
            else
            {
                pictureBoxResult.Image = ATS.Properties.Resources.ng;
            }
            pictureBoxResult.Refresh();
        }

        private void pictureBoxResult_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ReslutShow_Click(object sender, EventArgs e)
        {

        }

      
    }
}

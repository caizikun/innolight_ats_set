using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TestPlan
{
    public partial class NewPlanName : Form
    {
        public bool blnCancelNewPlan = false;
        public NewPlanName()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (PNInfo.checkItemLength("NewPlanName", txtNewName.Text, 30))
                {
                    return;
                }
                if (txtNewName.Text.ToString().Trim().Length == 0)
                {
                    MessageBox.Show("当前输入新的测试计划的名称为空!请确认!");
                }
                else
                {
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            blnCancelNewPlan = true;
            this.Close();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Maintain
{
    public partial class NewFileName : Form
    {
        string[] fileNameItems;
        public string name = "";
        public bool isNewFileNameOK = false;
        private bool isExistFileName = false;
        ToolTip myNewTip = new ToolTip();
        public NewFileName()
        {
            InitializeComponent();
        }

        public NewFileName(string[] fileName)
        {
            InitializeComponent();
            fileNameItems = fileName;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                isExistFileName = false;

                if (txtFileName.Text.ToString().Trim().Length == 0)
                {
                    MessageBox.Show("The new EEPROM file name is empty! Pls confirm!");
                }
                else if (txtFileName.Text.ToString().Trim().Length > 30)
                {
                    MessageBox.Show("The new EEPROM file name exceed default length! Pls confirm!");
                }
                else
                {
                    foreach (string temp in fileNameItems)
                    {
                        if (txtFileName.Text.ToString().ToUpper().Trim() == temp)
                        {
                            isExistFileName = true;
                            break;
                        }
                    }

                    if (isExistFileName)
                    {
                        MessageBox.Show("Error!\nThe new file name already exists! ");
                    }
                    else
                    {
                        name = txtFileName.Text.ToString().Trim();
                        isNewFileNameOK = true;
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            
            //sslRunMsg.Text = "已经重新载入服务器资料...时间:[" + DateTime.Now.ToString() + "]";
            //sslRunMsg.BackColor = Color.Yellow;
            //runMsg.Refresh();
        }

        ///// <summary>
        ///// 检测文本框输入
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void txtFileName_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (true)
        //    {
        //        e.Handled = false;
        //    }
        //    else
        //    {
        //        e.Handled = true;
        //    }
        //}

        private void txtFileName_Enter(object sender, EventArgs e)
        {
            myNewTip.Show("The maximum length of the new file name is 30!", txtFileName);
        }
    }
}

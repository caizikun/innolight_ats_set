using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ATSDataBase;

namespace GlobalInfo
{
    public partial class PlanNameForm : Form
    {
        public bool blnCancelNewPlan = false;
        public bool blnAllPlans = false;
        DataIO mySQL = new SqlManager(Login.ServerName, Login.DBName, Login.DBUser, Login.DBPassword);   //140917_2    //140911_0
        public string[,] ArrayPNPlan ;
        public PlanNameForm()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkAllPlan.Checked == false)    //140710_0
                {
                    //if (cboPlanName.Text.ToString().Trim().Length == 0)
                    int listCount = listPNPlan.Items.Count;
                    blnAllPlans = false;
                    if (listCount == 0)
                    {
                        MessageBox.Show("Please choose at least one testplan! ");
                        return;
                    }
                    else
                    {
                        ArrayPNPlan = new string[listCount, 2];
                        for (int k = 0; k < listCount; k++)
                        {
                            string ss = listPNPlan.Items[k].ToString();
                            int myindex = ss.IndexOf(":");
                            ArrayPNPlan[k, 0] = ss.Substring(0, myindex);
                            ArrayPNPlan[k, 1] = ss.Substring(myindex + 1, ss.Length - (myindex + 1));
                        }                        
                    }
                }
                else
                {
                    blnAllPlans = true;
                }
                this.Close();
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

        private void PlanNameForm_Load(object sender, EventArgs e)
        {
            try
            {
                cboPlanName.Items.Clear();
                cboPlanName.Text = "";
                cboPlanName.Items.Add("");

                cboPN.Items.Clear();
                cboPN.Text = "";
                cboPN.Items.Add("");
                DataTable pPNdt = new DataTable();
                pPNdt = mySQL.GetDataTable("Select * from GlobalProductionName", "GlobalProductionName");
                for (int i = 0; i < pPNdt.Rows.Count; i++)
                {
                    cboPN.Items.Add(pPNdt.Rows[i]["PN"].ToString());
                }
                //if (pPNdt.Rows.Count > 0)
                //{
                //    cboPN.SelectedItem = cboPN.Items[0];
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void cboPN_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cboPlanName.Items.Clear();
                DataTable pPlandt = new DataTable();
                pPlandt = mySQL.GetDataTable("Select * from TopoTestPlan where pid in "
                    + "(select id from GlobalProductionName where PN='" + cboPN.Text + "')", "topotestplan");  // ", "TopoTestPlan"); //        
                for (int i = 0; i < pPlandt.Rows.Count; i++)
                {
                    cboPlanName.Items.Add(pPlandt.Rows[i]["ItemName"].ToString());
                }
                cboPlanName.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void chkAllPlan_CheckedChanged(object sender, EventArgs e)
        {
            btnState(!chkAllPlan.Checked);
        }

        void btnState(bool state)
        {
            try
            {
                listPNPlan.Enabled = state;
                btnAdd.Enabled = state;
                btnRemove.Enabled = state;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            setbtnRemvoeState(false);
            string myPNPlan="";

            if (cboPN.Text.Trim().Length >0 && cboPlanName.Text.Trim().Length>0)
            {
                myPNPlan = cboPN.Text.Trim().ToUpper() + ":" + cboPlanName.Text.Trim().ToUpper();
                for (int i = 0; i < listPNPlan.Items.Count; i++)
                {
                    if (myPNPlan.ToUpper().Trim() == listPNPlan.Items[i].ToString().ToUpper().Trim())
                    {
                        MessageBox.Show("Error,This testplan already exists:-> " + myPNPlan);
                        return;
                    } 
                }
                listPNPlan.Items.Add(myPNPlan);
            }
        }

        private void listPNPlan_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (listPNPlan.SelectedIndex != -1)
                {
                    setbtnRemvoeState(true);
                }
                else
                {
                    setbtnRemvoeState(false);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void setbtnRemvoeState(bool state)
        {
            btnRemove.Enabled = state;
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                if (listPNPlan.SelectedIndex != -1)
                {
                    listPNPlan.Items.RemoveAt(listPNPlan.SelectedIndex);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}

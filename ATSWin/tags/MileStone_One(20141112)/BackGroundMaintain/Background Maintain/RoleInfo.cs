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
    public partial class RoleInfo : Form
    {
        DataIO mySqlIO; //140919 = new SqlManager(Login.ServerName, Login.DBName, Login.DBUser, Login.DBPassword);   //140917_2    //140911_0
        DataTable dt;
        public RoleInfo()
        {
            InitializeComponent();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                btnEdit.Enabled = false;
                DataTable myInfo = mySqlIO.GetDataTable("select * from RolesTable where RoleName='" + cboUser.Text + "'", "RolesTable");

                DataRow[] myRows = myInfo.Select();
                if (myRows.Length == 1)
                {
                    myRows[0].BeginEdit();
                    myRows[0]["Remarks"] = this.txtRemark.Text.ToString();
                    myRows[0].EndEdit();

                    mySqlIO.UpdateDataTable("select * from RolesTable where RoleName='" + cboUser.Text + "'", myInfo, false);
                    txtStates.Text = "Edit Role successful::" + cboUser.Text + "";
                }
                else
                {
                    MessageBox.Show("Error: Role->" + cboUser.Text + " was not existed!");
                }
                refreshForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                btnEdit.Enabled = true;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                btnAdd.Enabled = false;
                DataTable myInfo = mySqlIO.GetDataTable("select * from RolesTable where RoleName='" + cboUser.Text + "'", "RolesTable");

                DataRow[] myRows = myInfo.Select();
                if (myRows.Length == 0)
                {
                    DataRow myRow = myInfo.NewRow();
                    myRow.BeginEdit();
                    myRow["RoleName"] = cboUser.Text.Trim();
                    myRow["Remarks"] = this.txtRemark.Text.ToString();
                    myRow.EndEdit();
                    myInfo.Rows.Add(myRow);

                    mySqlIO.UpdateDataTable("select * from RolesTable where RoleName='" + cboUser.Text + "'", myInfo, false);
                    txtStates.Text = "Add Role successful:" + cboUser.Text + "";
                }
                else
                {
                    MessageBox.Show("Add Role failed: ->" + cboUser.Text + " has been existed!");
                }
                refreshForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                btnAdd.Enabled = true;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                btnDelete.Enabled = false;
                DataTable myInfo = mySqlIO.GetDataTable("select * from RolesTable where RoleName='" + cboUser.Text + "'", "RolesTable");

                DataRow[] myRows = myInfo.Select();
                if (myRows.Length == 1)
                {
                    myRows[0].Delete();
                    mySqlIO.UpdateDataTable("select * from RolesTable where RoleName='" + cboUser.Text + "'", myInfo, false);
                    txtStates.Text = "Delete Role successful::" + cboUser.Text + "";
                }
                else
                {
                    MessageBox.Show("Error! Role : ->" + cboUser.Text + " was not existed!");
                }
                refreshForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                btnDelete.Enabled = true;
            }
        }


        private void RolesTable_Load(object sender, EventArgs e)
        {
            refreshForm();
        }

        void refreshForm()
        {
            try
            {
                if (Login.blnISDBSQLserver)
                {
                    mySqlIO = new SqlManager(Login.ServerName, Login.DBName, Login.DBUser, Login.DBPassword);   //140918_0 //140722_2   //140912_0
                }
                else
                {
                    mySqlIO = new AccessManager(Login.AccessFilePath);  //140714_1
                }
                cboUser.Items.Clear();
                txtRemark.Clear();
                
                dt = mySqlIO.GetDataTable("select * from RolesTable", "RolesTable");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    this.cboUser.Items.Add(dt.Rows[i]["RoleName"].ToString());
                    cboUser.SelectedIndex = 0;
                }

                txtRemark.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void btnStatus(bool status)
        {
            btnAdd.Enabled = status;
            btnEdit.Enabled = status;
            btnDelete.Enabled = status;
        }

        private void cboUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataRow[] drs = dt.Select("RoleName='" + cboUser.Text.ToString() + "'");
                if (drs.Length == 1)
                {
                    //cboUser.Text = drs[0]["RoleName"].ToString();
                    txtRemark.Text = drs[0]["Remarks"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void cboUser_Leave(object sender, EventArgs e)
        {
            try
            {
                if (cboUser.Text.Trim().Length == 0)
                {
                    cboUser.Focus();
                    MessageBox.Show("RoleName data is invalid ...");
                    btnStatus(false);
                }
                else
                {
                    btnStatus(true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            GC.Collect();
        }

    }
}

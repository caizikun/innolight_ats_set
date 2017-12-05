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
    public partial class UserInfo : Form
    {      
        DataIO mySqlIO;
        DataTable dt;
        public UserInfo()
        {
            InitializeComponent();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (MainForm.checkItemLength("Title", cboUser.Text, 50)
                    || MainForm.checkItemLength("LoginPassword", txtRemark.Text, 50)
                    || MainForm.checkItemLength("TrueName", txtRemark.Text, 20)
                    || MainForm.checkItemLength("Remarks", txtRemark.Text, 50))
                {
                    return;
                }
                btnEdit.Enabled = false;
                DataTable myInfo = mySqlIO.GetDataTable("select * from UserInfo where LoginName='" + cboUser.Text + "'", "UserInfo");

                DataRow[] myRows = myInfo.Select();
                if (myRows.Length == 1)
                {
                    myRows[0].BeginEdit();
                    myRows[0]["LoginPassword"] = this.txtPWD.Text.ToString();
                    myRows[0]["TrueName"] = this.txtTrueName.Text.ToString();
                    myRows[0]["Remarks"] = this.txtRemark.Text.ToString();
                    myRows[0].EndEdit();

                    mySqlIO.UpdateDataTable("select * from UserInfo where LoginName='" + cboUser.Text + "'", myInfo, false);
                    txtStates.Text = "Edit UserInfo successful:" + cboUser.Text + "";
                }
                else
                {
                    MessageBox.Show("Error: UserInfo->" + cboUser.Text + " was not existed!");
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
                if (MainForm.checkItemLength("Title", cboUser.Text, 50)
                    || MainForm.checkItemLength("LoginPassword", txtRemark.Text, 50)
                    || MainForm.checkItemLength("TrueName", txtRemark.Text, 20)
                    || MainForm.checkItemLength("Remarks", txtRemark.Text, 50))
                {
                    return;
                }
                btnAdd.Enabled = false;
                DataTable myInfo = mySqlIO.GetDataTable("select * from UserInfo where LoginName='" + cboUser.Text + "'", "UserInfo");

                DataRow[] myRows = myInfo.Select();
                if (myRows.Length == 0)
                {
                    DataRow myRow = myInfo.NewRow();
                    myRow.BeginEdit();
                    myRow["LoginName"] = cboUser.Text.Trim();
                    myRow["LoginPassword"] = this.txtPWD.Text.ToString();
                    myRow["TrueName"] = this.txtTrueName.Text.ToString();
                    myRow["Remarks"] = this.txtRemark.Text.ToString();
                    myRow.EndEdit();
                    myInfo.Rows.Add(myRow);

                    mySqlIO.UpdateDataTable("select * from UserInfo where LoginName='" + cboUser.Text + "'", myInfo, false);
                    txtStates.Text = "Add UserInfo successful:" + cboUser.Text + "";
                }
                else
                {
                    MessageBox.Show("Add UserInfo failed: ->" + cboUser.Text + " has been existed!");
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
                DataTable myInfo = mySqlIO.GetDataTable("select * from UserInfo where LoginName='" + cboUser.Text + "'", "UserInfo");

                DataRow[] myRows = myInfo.Select();
                if (myRows.Length == 1)
                {
                    myRows[0].Delete();
                    mySqlIO.UpdateDataTable("select * from UserInfo where LoginName='" + cboUser.Text + "'", myInfo, false);
                    txtStates.Text = "Delete UserInfo successful::" + cboUser.Text + "";
                }
                else
                {
                    MessageBox.Show("Error! UserInfo : ->" + cboUser.Text + " was not existed!");
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


        private void UserInfo_Load(object sender, EventArgs e)
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
                txtPWD.Clear();
                txtRemark.Clear();
                txtTrueName.Clear();
                dt = mySqlIO.GetDataTable("select * from UserInfo", "UserInfo");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    this.cboUser.Items.Add(dt.Rows[i]["LoginName"].ToString());
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
                DataRow[] drs = dt.Select("LoginName='" + cboUser.Text.ToString() + "'");
                if (drs.Length == 1)
                {
                    //cboUser.Text = drs[0]["LoginName"].ToString();
                    txtPWD.Text = drs[0]["LoginPassword"].ToString();
                    txtTrueName.Text = drs[0]["TrueName"].ToString();
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
                    MessageBox.Show("UserName data is invalid ......");
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

        private void txtPWD_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtPWD.Text.Trim().Length == 0)
                {
                    txtPWD.Focus();
                    MessageBox.Show("Password data is invalid ...");
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

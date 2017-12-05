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
    public partial class UserRoleFunctionInfo : Form
    {
        DataIO mySqlIO; //140919 = new SqlManager(Login.ServerName, Login.DBName, Login.DBUser, Login.DBPassword);   //140917_2    //140911_0
        DataTable funcdt;
        DataTable roledt;
        DataTable userdt;
        //DataTable userRoledt;
        //DataTable roleFuncdt;
        bool isLoadFormOK = false;
        string tempUser = "";
        string tempRole = "";
        string currUserID, currRoleID, currFuncID;

        public UserRoleFunctionInfo()
        {
            InitializeComponent();
        }
                

        private void FunctionInfo_Load(object sender, EventArgs e)
        {
            if (Login.blnISDBSQLserver)
            {
                mySqlIO = new SqlManager(Login.ServerName, Login.DBName, Login.DBUser, Login.DBPassword);   //140918_0 //140722_2   //140912_0
            }
            else
            {
                mySqlIO = new AccessManager(Login.AccessFilePath);  //140714_1
            }
            refreshForm();
            isLoadFormOK = true;
        }

        void refreshForm()
        {
            try
            {
                cboRole.Items.Clear();
                cboUser.Items.Clear();
                lstTotalFunc.Items.Clear();
                lstTotalRole.Items.Clear();
                lstUserRole.Items.Clear();
                lstRoleFunc.Items.Clear();

                currUserID =""; currRoleID=""; currFuncID="";

                userdt = mySqlIO.GetDataTable("select * from UserInfo", "UserInfo");
                for (int i = 0; i < userdt.Rows.Count; i++)
                {
                    this.cboUser.Items.Add(userdt.Rows[i]["LoginName"].ToString());                    
                }

                funcdt = mySqlIO.GetDataTable("select * from FunctionTable", "FunctionTable");
                for (int i = 0; i < funcdt.Rows.Count; i++)
                {
                    this.lstTotalFunc.Items.Add(funcdt.Rows[i]["Remarks"].ToString());                    
                }

                roledt = mySqlIO.GetDataTable("select * from RolesTable", "RolesTable");
                for (int i = 0; i < roledt.Rows.Count; i++)
                {
                    this.lstTotalRole.Items.Add(roledt.Rows[i]["Remarks"].ToString());
                    this.cboRole.Items.Add(roledt.Rows[i]["Remarks"].ToString());
                }

                if (isLoadFormOK)
                {
                    cboUser.Text = tempUser;
                    cboRole.Text = tempRole;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void btnStatus(bool status)
        {
            this.btnAddFunc.Enabled = status;
            this.btnAddRole.Enabled = status;
            this.btnRemoveFunc.Enabled = status;
            this.btnRemoveRole.Enabled = status;
        }

        private void cboUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboUser.SelectedIndex != -1)
                {
                    tempUser = cboUser.Text.ToString();
                    currUserID = MainForm.getDTColumnInfo(userdt, "ID", "loginName='" + cboUser.Text + "'");
                    lstUserRole.Items.Clear();
                    string queryCMD = "select * from RolesTable where id in "
                                        + "(select RoleID from UserRoleTable where userID in"
                                            + "(select ID from userInfo where loginName = '" + this.cboUser.Text + "'))";

                    DataTable dt = mySqlIO.GetDataTable(queryCMD, "userRoledt");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string myRoleName = dt.Rows[i]["Remarks"].ToString();
                        lstUserRole.Items.Add(myRoleName);
                    }
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

        private void btnAddRole_Click(object sender, EventArgs e)
        {
            try
            {
                btnAddRole.Enabled = false;
                if (cboUser.Text.Trim().Length==0)
                {
                    MessageBox.Show("Pls choose a user first...");
                    if (cboUser.Enabled)
                    {
                        cboUser.Focus();
                    }
                }
                else if (lstTotalRole.SelectedIndex == -1)
                {
                    MessageBox.Show("Pls choose a role first for add role!");
                    if (lstTotalRole.Enabled)
                    {
                        lstTotalRole.Focus();
                    }
                }
                else
                {
                    
                    for (int i = 0; i < lstUserRole.Items.Count; i++)
                    {
                        if (lstTotalRole.SelectedItem.ToString().ToUpper() == lstUserRole.Items[i].ToString().ToUpper())
                        {
                            MessageBox.Show("The user has already got this role: " + lstTotalRole.SelectedItem.ToString());
                            return;
                        }
                    }

                    currRoleID = MainForm.getDTColumnInfo(roledt, "ID", "Remarks='" + lstTotalRole.SelectedItem.ToString() + "'");
                    DataTable userRoledt = new DataTable();
                    userRoledt = mySqlIO.GetDataTable("select * from userRoleTable", "userRoleTable");

                    DataRow[] myRows = userRoledt.Select("UserID= " + currUserID + " and RoleID=" + currRoleID);
                    if (myRows.Length == 0)
                    {
                        DataRow myRow = userRoledt.NewRow();
                        myRow.BeginEdit();
                        myRow["UserID"] = currUserID;
                        myRow["RoleID"] = currRoleID;
                        myRow.EndEdit();
                        userRoledt.Rows.Add(myRow);
                        
                        mySqlIO.UpdateDataTable("select * from userRoleTable where UserID= " + currUserID + " and RoleID=" + currRoleID, userRoledt, false);
                        txtStates.Text = "Add successful: user=" + this.cboUser.Text + " -> Role: " + lstTotalRole.SelectedItem.ToString() +" ";
                    }
                    else
                    {
                        MessageBox.Show("Add Failed: \nThe user has already got this role: ->" + lstTotalRole.SelectedItem.ToString() + "");
                    }
                    refreshForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                btnAddRole.Enabled = true;
            }
        }

        private void btnRemoveRole_Click(object sender, EventArgs e)
        {
            try
            {
                btnRemoveRole.Enabled = false;

                if (cboUser.Text.Trim().Length==0)
                {
                    MessageBox.Show("Pls choose a user first...");
                    if (cboUser.Enabled)
                    {
                        cboUser.Focus();
                    }
                }
                else if (lstUserRole.SelectedIndex == -1)
                {
                    MessageBox.Show("Pls choose a role first for delete!");
                    if (lstUserRole.Enabled)
                    {
                        lstUserRole.Focus();
                    }
                }
                else
                {
                    currRoleID = MainForm.getDTColumnInfo(roledt, "ID", "Remarks='" + lstUserRole.SelectedItem.ToString() + "'");
                    DataTable userRoledt = new DataTable();
                    userRoledt = mySqlIO.GetDataTable("select * from userRoleTable", "userRoleTable");
                    DataRow[] myRows = userRoledt.Select("UserID= " + currUserID + " and RoleID=" + currRoleID);
                    if (myRows.Length == 1)
                    {
                        myRows[0].Delete();

                        mySqlIO.UpdateDataTable("select * from userRoleTable where UserID= " + currUserID + " and RoleID=" + currRoleID, userRoledt, false);
                        txtStates.Text = "Delete successful: user=" + cboUser.Text + " ->role: " + lstUserRole.SelectedItem.ToString() + "";
                        tempRole = "";
                        cboRole.Refresh();
                    }
                    else
                    {
                        MessageBox.Show("Delete failed:\n role: ->" + lstTotalRole.SelectedItem.ToString() + " not existed.");
                    }
                }
                refreshForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                btnRemoveRole.Enabled = true;
            }
        }

        private void btnAddFunc_Click(object sender, EventArgs e)
        {
            try
            {
                btnAddFunc.Enabled = false;
                if (cboRole.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Pls choose a role first...");
                    if (cboRole.Enabled)
                    {
                        cboRole.Focus();
                    }
                }
                else if (lstTotalFunc.SelectedIndex == -1)
                {
                    MessageBox.Show("Pls choose a function first for add...");
                    if (lstTotalFunc.Enabled)
                    {
                        lstTotalFunc.Focus();
                    }
                }
                else
                {
                    for (int i = 0; i < lstRoleFunc.Items.Count; i++)
                    {
                        if (lstTotalFunc.SelectedItem.ToString().ToUpper() == lstRoleFunc.Items[i].ToString().ToUpper())
                        {
                            MessageBox.Show("The role has already got this function: " + lstTotalFunc.SelectedItem.ToString());
                            return;
                        }
                    }

                    currFuncID = MainForm.getDTColumnInfo(funcdt, "ID", "Remarks='" + this.lstTotalFunc.SelectedItem.ToString() + "'");
                    DataTable roleFuncdt = new DataTable();
                    roleFuncdt = mySqlIO.GetDataTable("select * from RoleFunctionTable", "RoleFunctionTable");
                    DataRow[] myRows = roleFuncdt.Select("FunctionID= " + currFuncID + " and RoleID=" + currRoleID);
                    if (myRows.Length == 0)
                    {
                        DataRow myRow = roleFuncdt.NewRow();
                        myRow.BeginEdit();
                        myRow["FunctionID"] = currFuncID;
                        myRow["RoleID"] = currRoleID;
                        myRow.EndEdit();
                        roleFuncdt.Rows.Add(myRow);

                        mySqlIO.UpdateDataTable("select * from RoleFunctionTable where FunctionID= " + currFuncID + " and RoleID=" + currRoleID, roleFuncdt, false);
                        txtStates.Text = "Add function successful : role=" + cboRole.Text + " -> " + lstTotalFunc.SelectedItem.ToString() + "";
                    }
                    else
                    {
                        MessageBox.Show("Add function failed :\nThe role has already got this function: ->" + lstTotalFunc.SelectedItem.ToString() + "!");
                    }
                    refreshForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                btnAddFunc.Enabled = true;
            }
        }

        private void btnRemoveFunc_Click(object sender, EventArgs e)
        {
            try
            {
                btnRemoveFunc.Enabled = false;

                if (cboRole.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Pls choose a role first ...");
                    if (cboRole.Enabled)
                    {
                        cboRole.Focus();
                    }
                }
                else if (lstRoleFunc.SelectedIndex == -1)
                {
                    MessageBox.Show("Pls choose a function first for delete");
                    if (lstRoleFunc.Enabled)
                    {
                        lstRoleFunc.Focus();
                    }
                }
                else
                {
                    currFuncID = MainForm.getDTColumnInfo(funcdt, "ID", "Remarks='" + this.lstRoleFunc.SelectedItem.ToString() + "'");
                    DataTable roleFuncdt = new DataTable();
                    roleFuncdt = mySqlIO.GetDataTable("select * from RoleFunctionTable", "RoleFunctionTable");
                    DataRow[] myRows = roleFuncdt.Select("FunctionID= " + currFuncID + " and RoleID=" + currRoleID);
                    if (myRows.Length == 1)
                    {
                        myRows[0].Delete();

                        mySqlIO.UpdateDataTable("select * from RoleFunctionTable where FunctionID= " + currFuncID + " and RoleID=" + currRoleID, roleFuncdt, false);
                        txtStates.Text = "Delete function successful: Role=" + cboRole.Text + " -> " + lstRoleFunc.SelectedItem.ToString() + "";
                        
                    }
                    else
                    {
                        MessageBox.Show("Delete function failed:\n function = ->" + lstTotalFunc.SelectedItem.ToString() + " was not existed.");
                    }
                }
                refreshForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                btnRemoveFunc.Enabled = true;
            }
        }

        void isAddRoleState(bool state)
        {
            btnAddRole.Enabled = state;
            btnRemoveRole.Enabled = !state;
        }

        void isAddFuncState(bool state)
        {
            btnAddFunc.Enabled = state;
            btnRemoveFunc.Enabled = !state;
        }

        private void lstRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (lstUserRole.SelectedIndex != -1)
                {
                    cboRole.Text = lstUserRole.SelectedItem.ToString();
                    cboRole.Refresh();
                    isAddRoleState(false);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void lstTotalRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (lstTotalRole.SelectedIndex != -1)
                {
                    cboRole.Text = lstTotalRole.SelectedItem.ToString();
                    cboRole.Refresh();
                    isAddRoleState(true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void cboRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboRole.SelectedIndex != -1)
                {
                    tempRole = this.cboRole.Text.ToString();
                    this.lstRoleFunc.Items.Clear();
                    currRoleID = MainForm.getDTColumnInfo(roledt, "ID", "Remarks='" + cboRole.Text + "'");

                    string queryCMD = "select * from FunctionTable where id in "
                                        + "(select FunctionID from RoleFunctionTable where RoleID in"
                                            + "(select ID from RolesTable where Remarks = '" + this.cboRole.Text + "'))";

                    DataTable mydt = mySqlIO.GetDataTable(queryCMD, "roleFuncdt");

                    for (int i = 0; i < mydt.Rows.Count; i++)
                    {
                        string myFuncName = mydt.Rows[i]["Remarks"].ToString();
                        lstRoleFunc.Items.Add(myFuncName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void lstRoleFunc_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (lstRoleFunc.SelectedIndex != -1)
                {
                    isAddFuncState(false);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void lstTotalFunc_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (lstTotalFunc.SelectedIndex != -1)
                {
                    isAddFuncState(true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

    }
}

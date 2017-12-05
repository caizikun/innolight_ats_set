using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ATSDataBase;
using Authority;

namespace Maintain
{
    public partial class RoleInfo : Form
    {
        DataIO mySqlIO; //140919 = new SqlManager(myLoginInfoStruct.ServerName, myLoginInfoStruct.DBName, myLoginInfoStruct.DBUser, myLoginInfoStruct.DBPassword);   //140917_2    //140911_0
        DataTable roledt;
        DataTable funcdt;        
        int currID = -1;
        bool blnAddNew = false;
        string currRoleID, currFuncID;
        string tempItemName = "";
        int tempLstIndex = -1;

        LoginInfoStruct myLoginInfoStruct;
        public RoleInfo(LoginInfoStruct pLoginInfoStruct)
        {
            myLoginInfoStruct = pLoginInfoStruct;
            InitializeComponent();
        }

        bool updateData()
        {
            bool result = false;
            try
            {
                if (blnAddNew)
                {
                    DataTable myInfo = mySqlIO.GetDataTable("select * from RolesTable where RoleName='" + txtUser.Text + "'", "RolesTable");
                    
                    DataRow[] myRows = myInfo.Select();
                    if (myRows.Length == 0)
                    {
                        DataRow myRow = myInfo.NewRow();
                        myRow.BeginEdit();
                        myRow["RoleName"] = txtUser.Text.Trim();
                        myRow["Remarks"] = this.txtRemark.Text.ToString();
                        myRow.EndEdit();
                        myInfo.Rows.Add(myRow);

                        mySqlIO.UpdateDataTable("select * from RolesTable where RoleName='" + txtUser.Text + "'", myInfo, false);
                        txtStates.Text = "Add role successful:" + txtUser.Text + "";
                        blnAddNew = false;
                        result = true;
                    }
                    else
                    {
                        MessageBox.Show("Add role failed: ->" + txtUser.Text + " has been existed!");
                    }
                    
                }
                else
                {
                    DataTable myInfo = mySqlIO.GetDataTable("select * from RolesTable where ID=" + currID, "RolesTable");

                    if (tempItemName != txtUser.Text.ToUpper().Trim() && mySqlIO.GetDataTable("select * from RolesTable where RoleName='" + txtUser.Text + "'", "RolesTable").Rows.Count > 0)
                    {
                        MessageBox.Show("role failed: ->" + txtUser.Text + " has been existed!");
                        return false;
                    }                    

                    DataRow[] myRows = myInfo.Select();
                    if (myRows.Length == 1)
                    {
                        myRows[0].BeginEdit();
                        myRows[0]["RoleName"] = this.txtUser.Text.ToString();
                        myRows[0]["Remarks"] = this.txtRemark.Text.ToString();
                        myRows[0].EndEdit();

                        mySqlIO.UpdateDataTable("select * from RolesTable where  ID=" + currID, myInfo, false);
                        txtStates.Text = "Edit role successful:" + txtUser.Text + "";
                        result = true;
                    }
                    else
                    {
                        MessageBox.Show("Error: role->" + txtUser.Text + " was not existed!");
                    }
                }
                
                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return result;
            }
            
        }

        void showMyTip()
        {
            try
            {
                ToolTip mytip = new ToolTip();

                mytip.SetToolTip(btnAdd, "Add new role item");
                mytip.SetToolTip(btnRemove, "Delete a role item");
                mytip.SetToolTip(btnOK, "Save role data");

                mytip.SetToolTip(this.currlst, "List of server role items");                
                mytip.SetToolTip(this.txtRemark, "Description of role item");
                mytip.SetToolTip(this.txtUser, "Role item name");
                mytip.SetToolTip(this.txtStates, "Operation logs");

                mytip.SetToolTip(this.btnAddFunc, "Add new function item for current role");
                mytip.SetToolTip(this.btnRemoveFunc, "Delete a function item for current role");

                mytip.SetToolTip(this.lstRoleFunc, "Functon list of current role name");
                mytip.SetToolTip(this.lstTotalFunc, "Total functon list of current server");

                mytip.ShowAlways = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        
        private void RoleInfo_Load(object sender, EventArgs e)
        {
            if (myLoginInfoStruct.blnISDBSQLserver)
            {
                mySqlIO = new SqlManager(myLoginInfoStruct.ServerName, myLoginInfoStruct.DBName, myLoginInfoStruct.DBUser, myLoginInfoStruct.DBPassword);   //140918_0 //140722_2   //140912_0
            }
            else
            {
                mySqlIO = new AccessManager(myLoginInfoStruct.AccessFilePath);  //140714_1
            }
            refreshForm();
            showMyTip();
        }

        void refreshForm()
        {
            try
            {
                grpFunc.Enabled = false;
                tempItemName = "";
                currlst.Items.Clear();
                txtUser.Text = "";
                txtRemark.Clear();
                roledt = mySqlIO.GetDataTable("select * from RolesTable", "RolesTable");
                for (int i = 0; i < roledt.Rows.Count; i++)
                {
                    currlst.Items.Add(roledt.Rows[i]["RoleName"].ToString());                    
                }          
                //141205------------------                
                lstTotalFunc.Items.Clear();                
                lstRoleFunc.Items.Clear();
                currRoleID = ""; currFuncID = "";
                
                funcdt = mySqlIO.GetDataTable("select * from FunctionTable", "FunctionTable");
                for (int i = 0; i < funcdt.Rows.Count; i++)
                {
                    this.lstTotalFunc.Items.Add(funcdt.Rows[i]["Remarks"].ToString());
                }


                if (tempLstIndex != -1 && currlst.Items.Count >= tempLstIndex)
                {
                    currlst.SelectedIndex = -1;
                    currlst.SelectedIndex = tempLstIndex;
                }
                //------------------------
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

        void getInfoFromDT(DataTable mydt, int currIndex)
        {
            try
            { 
                //ID RoleName roleCode Remarks
                string filterString = currlst.SelectedItem.ToString();
                tempItemName = filterString.ToUpper().Trim();
                DataRow[] myROWS = mydt.Select("RoleName='" + filterString + "'");
                if (myROWS.Length == 1)
                {
                    currID = Convert.ToInt32(myROWS[0]["ID"]);
                    this.txtUser.Text = myROWS[0]["RoleName"].ToString();
                    this.txtRemark.Text = myROWS[0]["Remarks"].ToString();
                    tempLstIndex = currlst.SelectedIndex;
                }
                else
                {
                    currID = -1;
                    grpFunc.Enabled = false;
                    MessageBox.Show("Error! \n " + myROWS.Length + " records existed; \n filterString--> RoleName='" + filterString + "'" );
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }
        }
        void getRoleFunction()
        {
            try
            {
                this.lstRoleFunc.Items.Clear();
                currRoleID = MainForm.getDTColumnInfo(roledt, "ID", "RoleName='" + currlst.Text + "'");

                string queryCMD = "select * from FunctionTable where id in "
                                    + "(select FunctionID from RoleFunctionTable where RoleID in"
                                        + "(select ID from RolesTable where RoleName = '" + this.currlst.Text + "'))";

                DataTable mydt = mySqlIO.GetDataTable(queryCMD, "roleFuncdt");

                for (int i = 0; i < mydt.Rows.Count; i++)
                {
                    string myFuncName = mydt.Rows[i]["Remarks"].ToString();
                    lstRoleFunc.Items.Add(myFuncName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void currlst_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (currlst.SelectedIndex != -1)
                {
                    runMsgState((byte)MsgState.Edit);
                    getInfoFromDT(roledt, currlst.SelectedIndex);
                    getRoleFunction();
                    if (this.txtUser.Text.ToString().ToUpper() == "Admin".ToUpper())
                    {
                        this.txtUser.Enabled = false;
                        btnRemove.Enabled = false;
                    }
                    else
                    {
                        this.txtUser.Enabled = true;
                        btnRemove.Enabled = true;
                    }
                    this.btnAddFunc.Enabled = false;
                    this.btnRemoveFunc.Enabled = false;
                    this.lstRoleFunc.SelectedIndex = -1;
                    this.lstTotalFunc.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        
        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                if (blnAddNew)  //若为新增则移除新增
                {
                    blnAddNew = false;
                    clearPrmtrInfo();
                    runMsgState((byte)MsgState.Clear);
                    return;
                }
                else
                {
                    //------------------------------
                    
                    DialogResult drst = new DialogResult();
                    string myDeleteItemName = this.currlst.SelectedItem.ToString();
                    drst = (MessageBox.Show("Delete Item -->" + myDeleteItemName + "\n \n Pls choose 'Y' (是) to continue?",
                        "Notice", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2));

                    if (drst == DialogResult.Yes)
                    {
                        btnRemove.Enabled = false;
                        DataTable myInfo = mySqlIO.GetDataTable("select * from RolesTable where RoleName='" + myDeleteItemName + "'", "RolesTable");

                        DataRow[] myRows = myInfo.Select();
                        if (myRows.Length == 1)
                        {
                            myRows[0].Delete();
                            mySqlIO.UpdateDataTable("select * from RolesTable where RoleName='" + myDeleteItemName + "'", myInfo, false);
                            tempLstIndex = -1;
                            refreshForm();
                            
                            runMsgState((byte)MsgState.Delete);
                            txtStates.Text = "Delete role successful:" + myDeleteItemName + "";
                        }
                        else
                        {
                            MessageBox.Show("Error! role : ->" + myDeleteItemName + " was not existed!");
                            refreshForm();
                            txtStates.Text = "Delete role failed:" + myDeleteItemName + "";
                        }
                    }
                    else
                    {
                        txtStates.Text = "Cancel operation :" + myDeleteItemName + "";
                    }                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                this.btnRemove.Enabled = true;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            runMsgState((byte)MsgState.AddNew);
        }

        void clearPrmtrInfo()
        {
            try
            {
                this.txtUser.Text = "";

                this.txtRemark.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void runMsgState(byte state)
        {
            try
            {
                this.currlst.Enabled = true;
                this.txtUser.Enabled = true;               
                this.grpFunc.Enabled = true;

                this.btnAddFunc.Enabled = false;
                this.btnRemoveFunc.Enabled = false;
                this.lstTotalFunc.Enabled = false;
                this.lstRoleFunc.Enabled = false;                
         
                if (state == (byte)MsgState.AddNew)
                {
                    this.txtStates.Text = "Add a new role...";
                    this.txtStates.BackColor = Color.Yellow;
                    this.txtUser.BackColor = Color.Yellow;
                    blnAddNew = true;   
                    this.currlst.Enabled = false;
                    currlst.SelectedIndex = -1;
                    clearPrmtrInfo();    //140917_0
                    lstRoleFunc.Items.Clear();
                }
                else if (state == (byte)MsgState.Edit)
                {
                    this.txtStates.Text = "Edit role item...";
                    this.txtStates.BackColor = Color.GreenYellow;
                    this.txtUser.BackColor = Color.GreenYellow;
                    blnAddNew = false;
                    this.lstTotalFunc.Enabled = true;
                    this.lstRoleFunc.Enabled = true;
                }
                else if (state == (byte)MsgState.SaveOK)
                {
                    this.txtStates.Text = "role item " + this.txtUser.Text + " has been saved successful!";
                    this.txtStates.BackColor = Color.YellowGreen;
                    this.txtUser.BackColor = Color.YellowGreen;
                    blnAddNew = false;
                    this.lstTotalFunc.Enabled = true;
                    this.lstRoleFunc.Enabled = true;
                }
                else if (state == (byte)MsgState.Delete)
                {
                    this.txtStates.Text = "role item " + this.txtUser.Text + " has been deleted successful!";
                    this.txtStates.BackColor = Color.Pink;
                    this.txtUser.BackColor = Color.Pink;
                    blnAddNew = false;
                    lstRoleFunc.Items.Clear();
                }
                else if (state == (byte)MsgState.Clear)
                {
                    clearPrmtrInfo();
                    this.txtStates.Text = "";
                    this.txtStates.BackColor = Color.White;
                    this.txtUser.BackColor = Color.White;
                    blnAddNew = false;
                    lstRoleFunc.Items.Clear();
                }
                if (this.txtUser.Text.ToString().ToUpper() == "Admin".ToUpper())
                {
                    this.txtUser.Enabled = false;
                    btnRemove.Enabled = false;                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        enum MsgState
        {
            SaveOK = 0,
            Edit = 1,
            AddNew = 2,
            Delete = 3,
            Clear = 4
        }
        
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtUser.Text.Trim().Length<=0)
            {
                MessageBox.Show("Any data is invalid or Null)");
                return;
            }

            if (MainForm.checkItemLength("RoleName", txtUser.Text, 25)
                || MainForm.checkItemLength("Remarks", txtRemark.Text, 25))
            {                
                return;
            }

            if (updateData())
            {
                refreshForm();
                runMsgState((byte)MsgState.SaveOK);
            }
            else
            {
                refreshForm();
            }            
        }

        private void btnAddFunc_Click(object sender, EventArgs e)
        {

            try
            {
                btnAddFunc.Enabled = false;
                if (this.currlst.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Pls choose a role first...");
                    if (currlst.Enabled)
                    {
                        currlst.Focus();
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
                    string tempItem = this.lstTotalFunc.SelectedItem.ToString();
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
                        refreshForm();
                        txtStates.Text = "Add function successful : role=" + currlst.Text + " -> " + tempItem + "";
                    }
                    else
                    {
                        MessageBox.Show("Add function failed :\nThe role has already got this function: ->" + tempItem + "!");
                        refreshForm();
                    }
                    
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

                if (currlst.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Pls choose a role first ...");
                    if (currlst.Enabled)
                    {
                        currlst.Focus();
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
                    string tempItem = this.lstRoleFunc.SelectedItem.ToString();
                    currFuncID = MainForm.getDTColumnInfo(funcdt, "ID", "Remarks='" + this.lstRoleFunc.SelectedItem.ToString() + "'");
                    
                    if (this.txtUser.Text.ToUpper() == "ADMIN".ToUpper())
                    {
                        string functionCode = MainForm.getDTColumnInfo(funcdt, "functionCode", "Remarks='" + this.lstRoleFunc.SelectedItem.ToString() + "'");
                        if (functionCode == "128")
                        {                            
                            MessageBox.Show("Role=<ADMIN> and Function Code=<128> can not be deleted, \nThis is a role=BackGroundOwner");
                            return;
                        }
                    }
                    DataTable roleFuncdt = new DataTable();
                    roleFuncdt = mySqlIO.GetDataTable("select * from RoleFunctionTable", "RoleFunctionTable");
                    DataRow[] myRows = roleFuncdt.Select("FunctionID= " + currFuncID + " and RoleID=" + currRoleID);
                    if (myRows.Length == 1)
                    {
                        myRows[0].Delete();

                        mySqlIO.UpdateDataTable("select * from RoleFunctionTable where FunctionID= " + currFuncID + " and RoleID=" + currRoleID, roleFuncdt, false);
                        refreshForm();
                        txtStates.Text = "Delete function successful: Role=" + currlst.Text + " -> " + tempItem + "";

                    }
                    else
                    {
                        MessageBox.Show("Delete function failed:\n function = ->" + tempItem + " was not existed.");
                        refreshForm();
                    }
                }
                
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
        void isAddFuncState(bool state)
        {
            btnAddFunc.Enabled = state;
            btnRemoveFunc.Enabled = !state;
        }
    }
}

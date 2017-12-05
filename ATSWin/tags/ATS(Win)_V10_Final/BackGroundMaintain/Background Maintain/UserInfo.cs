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
    public partial class UserInfo : Form
    {
        DataIO mySqlIO; //140919 = new SqlManager(myLoginInfoStruct.ServerName, myLoginInfoStruct.DBName, myLoginInfoStruct.DBUser, myLoginInfoStruct.DBPassword);   //140917_2    //140911_0
        DataTable userDT;
        DataTable roleDT;
        int currID = -1;
        string currUserID, currRoleID;
        bool blnAddNew = false;
        LoginInfoStruct myLoginInfoStruct ;
        string tempItemName = "";
        int tempLstIndex = -1;

        public UserInfo(LoginInfoStruct pLoginInfoStruct)
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
                    DataTable myInfo = mySqlIO.GetDataTable("select * from UserInfo where LoginName='" + txtUser.Text + "'", "UserInfo");
                    
                    DataRow[] myRows = myInfo.Select();
                    if (myRows.Length == 0)
                    {
                        DataRow myRow = myInfo.NewRow();
                        myRow.BeginEdit();
                        myRow["LoginName"] = this.txtUser.Text.ToString();
                        myRow["LoginPassword"] = this.txtPWD.Text.ToString();
                        myRow["TrueName"] = this.txtTrueName.Text.ToString();
                        myRow["Remarks"] = this.txtRemark.Text.ToString();
                        myRow.EndEdit();
                        myInfo.Rows.Add(myRow);

                        mySqlIO.UpdateDataTable("select * from UserInfo where LoginName='" + txtUser.Text + "'", myInfo, false);
                        txtStates.Text = "Add User successful:" + txtUser.Text + "";
                        blnAddNew = false;
                        result = true;
                    }
                    else
                    {
                        MessageBox.Show("Add User failed: ->" + txtUser.Text + " has been existed!");
                    }
                    
                }
                else
                {
                    DataTable myInfo = mySqlIO.GetDataTable("select * from UserInfo where ID=" + currID, "UserInfo");

                    if (tempItemName != txtUser.Text.ToUpper().Trim() && mySqlIO.GetDataTable("select * from UserInfo where LoginName='" + txtUser.Text + "'", "UserInfo").Rows.Count > 0)
                    {
                        MessageBox.Show("User failed: ->" + txtUser.Text + " has been existed!");
                        return false;
                    }                    

                    DataRow[] myRows = myInfo.Select();
                    if (myRows.Length == 1)
                    {
                        myRows[0].BeginEdit();
                        myRows[0]["LoginName"] = this.txtUser.Text.ToString();
                        myRows[0]["LoginPassword"] = this.txtPWD.Text.ToString();
                        myRows[0]["TrueName"] = this.txtTrueName.Text.ToString();
                        myRows[0]["Remarks"] = this.txtRemark.Text.ToString();
                        myRows[0].EndEdit();

                        mySqlIO.UpdateDataTable("select * from UserInfo where  ID=" + currID, myInfo, false);
                        txtStates.Text = "Edit User successful:" + txtUser.Text + "";
                        result = true;
                    }
                    else
                    {
                        MessageBox.Show("Error: User->" + txtUser.Text + " was not existed!");
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

                mytip.SetToolTip(btnAdd, "Add new Account item");
                mytip.SetToolTip(btnRemove, "Delete a Account item");
                mytip.SetToolTip(btnOK, "Save Account data");

                mytip.SetToolTip(this.currlst, "List of server Account items");
                mytip.SetToolTip(this.txtRemark, "Description of Account item");
                mytip.SetToolTip(this.txtUser, "Account item name");
                mytip.SetToolTip(this.txtPWD, "Account password");
                mytip.SetToolTip(this.txtTrueName, "Realname of Account");
                mytip.SetToolTip(this.txtStates, "Operation logs");

                mytip.SetToolTip(this.btnAddRole, "Add new role item for current user");
                mytip.SetToolTip(this.btnRemoveRole, "Delete a role item for current user");

                mytip.SetToolTip(this.lstUserRole, "role list of current user name");
                mytip.SetToolTip(this.lstTotalRole, "Total role list of current server");

                mytip.ShowAlways = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        
        private void UserInfo_Load(object sender, EventArgs e)
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
                txtPWD.Clear();
                txtTrueName.Clear();
                txtRemark.Clear();
                userDT = mySqlIO.GetDataTable("select * from UserInfo", "UserInfo");
                for (int i = 0; i < userDT.Rows.Count; i++)
                {
                    currlst.Items.Add(userDT.Rows[i]["LoginName"].ToString());                   
                }
                //141250--------------------------------
                lstTotalRole.Items.Clear();
                lstUserRole.Items.Clear();                

                currUserID = ""; currRoleID = "";

                roleDT = mySqlIO.GetDataTable("select * from RolesTable", "RolesTable");
                for (int i = 0; i < roleDT.Rows.Count; i++)
                {
                    this.lstTotalRole.Items.Add(roleDT.Rows[i]["Remarks"].ToString());                    
                }

                if (tempLstIndex != -1 && currlst.Items.Count >=tempLstIndex )
                {
                    currlst.SelectedIndex = -1;
                    currlst.SelectedIndex = tempLstIndex;
                }
                //141250--------------------------------
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
                //ID LoginName LoginPassword Remarks
                string filterString = currlst.SelectedItem.ToString();
                tempItemName = filterString.ToUpper().Trim();
                DataRow[] myROWS = mydt.Select("LoginName='" + filterString + "'");
                if (myROWS.Length == 1)
                {
                    currID = Convert.ToInt32(myROWS[0]["ID"]);
                    this.txtUser.Text = myROWS[0]["LoginName"].ToString();
                    this.txtPWD.Text = myROWS[0]["LoginPassword"].ToString();
                    this.txtTrueName.Text = myROWS[0]["TrueName"].ToString();
                    this.txtRemark.Text = myROWS[0]["Remarks"].ToString();
                    tempLstIndex = currlst.SelectedIndex;
                }
                else
                {
                    currID = -1;
                    grpFunc.Enabled = false;
                    MessageBox.Show("Error! \n " + myROWS.Length + " records existed; \n filterString--> LoginName='" + filterString + "'" );
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }
        }
        void getUserRoles()
        {
            currUserID = MainForm.getDTColumnInfo(userDT, "ID", "loginName='" + this.currlst.Text + "'");
            lstUserRole.Items.Clear();
            string queryCMD = "select * from RolesTable where id in "
                                + "(select RoleID from UserRoleTable where userID in"
                                    + "(select ID from userInfo where loginName = '" + this.currlst.Text.ToString() + "'))";

            DataTable userRoledt = mySqlIO.GetDataTable(queryCMD, "userRoledt");
            for (int i = 0; i < userRoledt.Rows.Count; i++)
            {
                string myRoleName = userRoledt.Rows[i]["Remarks"].ToString();
                lstUserRole.Items.Add(myRoleName);
            }
        }

        private void currlst_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (currlst.SelectedIndex != -1)
                {
                    runMsgState((byte)MsgState.Edit);
                    getInfoFromDT(userDT, currlst.SelectedIndex);
                    getUserRoles();

                    if (this.txtUser.Text.ToString().ToUpper() == "Admin".ToUpper())
                    {
                        this.txtUser.Enabled = false;
                        btnRemove.Enabled = false;
                        this.lstUserRole.Enabled = false;
                    }
                    else
                    {
                        this.txtUser.Enabled = true;
                        btnRemove.Enabled = true;
                        this.lstUserRole.Enabled = true;
                    }
                    this.btnRemoveRole.Enabled = false;
                    this.btnAddRole.Enabled = false;
                    this.lstTotalRole.SelectedIndex = -1;
                    this.lstUserRole.SelectedIndex = -1;
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
                        DataTable myInfo = mySqlIO.GetDataTable("select * from UserInfo where LoginName='" + myDeleteItemName + "'", "UserInfo");

                        DataRow[] myRows = myInfo.Select();
                        if (myRows.Length == 1)
                        {
                            myRows[0].Delete();
                            mySqlIO.UpdateDataTable("select * from UserInfo where LoginName='" + myDeleteItemName + "'", myInfo, false);
                            tempLstIndex = -1;
                            refreshForm();
                            txtStates.Text = "Delete User successful:" + myDeleteItemName + "";
                            runMsgState((byte)MsgState.Delete);
                        }
                        else
                        {
                            MessageBox.Show("Error! User : ->" + myDeleteItemName + " was not existed!");
                            refreshForm();
                            txtStates.Text = "Delete User failed:" + myDeleteItemName + "";
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
                this.txtPWD.Text = "";
                this.txtTrueName.Text = "";
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
                this.btnAddRole.Enabled = false;
                this.btnRemoveRole.Enabled = false;
                this.currlst.Enabled = true;
                this.txtUser.Enabled = true;               
                this.grpFunc.Enabled = true;
                this.lstTotalRole.Enabled = false;
                this.lstUserRole.Enabled = false;                
                
                if (state == (byte)MsgState.AddNew)
                {
                    this.txtStates.Text = "Add a new User...";
                    this.txtStates.BackColor = Color.Yellow;
                    this.txtUser.BackColor = Color.Yellow;
                    blnAddNew = true;   
                    this.currlst.Enabled = false;
                    currlst.SelectedIndex = -1;
                    clearPrmtrInfo();    //140917_0
                    lstUserRole.Items.Clear();
                }
                else if (state == (byte)MsgState.Edit)
                {
                    this.txtStates.Text = "Edit User item...";
                    this.txtStates.BackColor = Color.GreenYellow;
                    this.txtUser.BackColor = Color.GreenYellow;
                    blnAddNew = false;
                    this.lstTotalRole.Enabled = true;
                    this.lstUserRole.Enabled = true;                  
                }
                else if (state == (byte)MsgState.SaveOK)
                {
                    this.txtStates.Text = "User item " + this.txtUser.Text + " has been saved successful!";
                    this.txtStates.BackColor = Color.YellowGreen;
                    this.txtUser.BackColor = Color.YellowGreen;
                    blnAddNew = false;
                    this.lstTotalRole.Enabled = true;
                    this.lstUserRole.Enabled = true;                                 
                }
                else if (state == (byte)MsgState.Delete)
                {
                    this.txtStates.Text = "User item " + this.txtUser.Text + " has been deleted successful!";
                    this.txtStates.BackColor = Color.Pink;
                    this.txtUser.BackColor = Color.Pink;
                    blnAddNew = false;
                    lstUserRole.Items.Clear();
                }
                else if (state == (byte)MsgState.Clear)
                {
                    clearPrmtrInfo();
                    this.txtStates.Text = "";
                    this.txtStates.BackColor = Color.White;
                    this.txtUser.BackColor = Color.White;
                    blnAddNew = false;
                    lstUserRole.Items.Clear();
                }

                if (this.txtUser.Text.ToString().ToUpper() == "Admin".ToUpper())
                {
                    this.txtUser.Enabled = false;
                    btnRemove.Enabled = false;
                    this.lstUserRole.Enabled = false;
                    btnRemoveRole.Enabled = false;
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
            if (txtUser.Text.Trim().Length <= 0                
                || txtPWD.Text.Trim().Length <= 0
                || txtTrueName.Text.Trim().Length <= 0)
            {
                MessageBox.Show("Any data is invalid or Null)");
                return;
            }

            if (MainForm.checkItemLength("LoginName", txtUser.Text, 50)
                || MainForm.checkItemLength("LoginPassword", this.txtPWD.Text, 50)
                || MainForm.checkItemLength("TrueName", this.txtTrueName.Text, 20)                
                || MainForm.checkItemLength("Remark", txtRemark.Text, 50))
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

        void btnStatus(bool status)
        {            
            this.btnAddRole.Enabled = status;            
            this.btnRemoveRole.Enabled = status;
        }

        private void btnAddRole_Click(object sender, EventArgs e)
        {
            try
            {                
                btnAddRole.Enabled = false;
                if (this.currlst.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Pls choose a user first...");
                    if (this.currlst.Enabled)
                    {
                        this.currlst.Focus();
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
                    string tempItem = lstTotalRole.SelectedItem.ToString();
                    currRoleID = MainForm.getDTColumnInfo(roleDT, "ID", "Remarks='" + lstTotalRole.SelectedItem.ToString() + "'");
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
                        refreshForm();
                        txtStates.Text = "Add successful: user=" + this.currlst.Text + " -> Role: " + tempItem + " ";
                    }
                    else
                    {
                        MessageBox.Show("Add Failed: \nThe user has already got this role: ->" + tempItem + "");
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
                btnAddRole.Enabled = true;
            }
        }

        private void btnRemoveRole_Click(object sender, EventArgs e)
        {
            try
            {
                btnRemoveRole.Enabled = false;
                
                if (currlst.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Pls choose a user first...");
                    if (currlst.Enabled)
                    {
                        currlst.Focus();
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
                    string tempItem = lstUserRole.SelectedItem.ToString();
                    currRoleID = MainForm.getDTColumnInfo(roleDT, "ID", "Remarks='" + lstUserRole.SelectedItem.ToString() + "'");
                    DataTable userRoledt = new DataTable();
                    userRoledt = mySqlIO.GetDataTable("select * from userRoleTable", "userRoleTable");
                    DataRow[] myRows = userRoledt.Select("UserID= " + currUserID + " and RoleID=" + currRoleID);
                    if (myRows.Length == 1)
                    {
                        myRows[0].Delete();

                        mySqlIO.UpdateDataTable("select * from userRoleTable where UserID= " + currUserID + " and RoleID=" + currRoleID, userRoledt, false);
                        refreshForm();
                        txtStates.Text = "Delete successful: user=" + currlst.Text + " ->role: " + tempItem + "";
                        
                    }
                    else
                    {
                        MessageBox.Show("Delete failed:\n role: ->" + tempItem + " not existed.");
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
                btnRemoveRole.Enabled = true;
            }
        }

        private void lstUserRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (lstUserRole.SelectedIndex != -1)
                {                    
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
                    isAddRoleState(true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void isAddRoleState(bool state)
        {
            btnAddRole.Enabled = state;
            btnRemoveRole.Enabled = !state;
        }
    }
}

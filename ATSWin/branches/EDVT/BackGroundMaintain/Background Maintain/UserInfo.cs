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
        DataTable dt;
        int currID = -1;
        bool blnAddNew = false;
        LoginInfoStruct myLoginInfoStruct ;
        string tempItemName = "";
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
                dt = mySqlIO.GetDataTable("select * from UserInfo", "UserInfo");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    currlst.Items.Add(dt.Rows[i]["LoginName"].ToString());                   
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

        private void currlst_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (currlst.SelectedIndex != -1)
                {
                    runMsgState((byte)MsgState.Edit);
                    getInfoFromDT(dt, currlst.SelectedIndex);
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
                        DataTable myInfo = mySqlIO.GetDataTable("select * from UserInfo where LoginName='" + txtUser.Text + "'", "UserInfo");

                        DataRow[] myRows = myInfo.Select();
                        if (myRows.Length == 1)
                        {
                            myRows[0].Delete();
                            mySqlIO.UpdateDataTable("select * from UserInfo where LoginName='" + txtUser.Text + "'", myInfo, false);
                            txtStates.Text = "Delete User successful:" + txtUser.Text + "";
                            runMsgState((byte)MsgState.Delete);
                        }
                        else
                        {
                            MessageBox.Show("Error! User : ->" + txtUser.Text + " was not existed!");
                            txtStates.Text = "Delete User failed:" + txtUser.Text + "";
                        }
                    }
                    else
                    {
                        txtStates.Text = "Cancel operation :" + txtUser.Text + "";
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
                this.currlst.Enabled = true;   
                this.txtUser.Enabled = false;               
                this.grpFunc.Enabled = true;  
         
                if (state == (byte)MsgState.AddNew)
                {
                    this.txtStates.Text = "Add a new User...";
                    this.txtStates.BackColor = Color.Yellow;
                    this.txtUser.BackColor = Color.Yellow;
                    blnAddNew = true;   
                    this.currlst.Enabled = false;   
                    this.txtUser.Enabled = true;
                    clearPrmtrInfo();    //140917_0
                }
                else if (state == (byte)MsgState.Edit)
                {
                    this.txtStates.Text = "Edit User item...";
                    this.txtStates.BackColor = Color.GreenYellow;
                    this.txtUser.BackColor = Color.GreenYellow;
                    blnAddNew = false;    
                    
                }
                else if (state == (byte)MsgState.SaveOK)
                {
                    this.txtStates.Text = "User item " + this.txtUser.Text + " has been saved successful!";
                    this.txtStates.BackColor = Color.YellowGreen;
                    this.txtUser.BackColor = Color.YellowGreen;
                    blnAddNew = false;   
                    
                }
                else if (state == (byte)MsgState.Delete)
                {
                    this.txtStates.Text = "User item " + this.txtUser.Text + " has been deleted successful!";
                    this.txtStates.BackColor = Color.Pink;
                    this.txtUser.BackColor = Color.Pink;
                    blnAddNew = false;   
                    
                }
                else if (state == (byte)MsgState.Clear)
                {
                    clearPrmtrInfo();
                    this.txtStates.Text = "";
                    this.txtStates.BackColor = Color.White;
                    this.txtUser.BackColor = Color.White;
                    blnAddNew = false;  
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

        private void lblUserID_DoubleClick(object sender, EventArgs e)
        {
            if (txtUser.Enabled == false)
            {
                txtUser.Enabled = true;
            }
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
                runMsgState((byte)MsgState.SaveOK);
            }
            else
            {
                
            }
            refreshForm();
        }
    }
}

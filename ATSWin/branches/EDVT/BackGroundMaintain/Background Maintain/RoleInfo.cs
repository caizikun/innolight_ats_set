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
        DataTable dt;
        int currID = -1;
        bool blnAddNew = false;
        string tempItemName = "";
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
                        txtStates.Text = "Add role successful:" + cboUser.Text + "";
                        blnAddNew = false;
                        result = true;
                    }
                    else
                    {
                        MessageBox.Show("Add role failed: ->" + cboUser.Text + " has been existed!");
                    }
                    
                }
                else
                {
                    DataTable myInfo = mySqlIO.GetDataTable("select * from RolesTable where ID=" + currID, "RolesTable");

                    if (tempItemName != cboUser.Text.ToUpper().Trim() && mySqlIO.GetDataTable("select * from RolesTable where RoleName='" + cboUser.Text + "'", "RolesTable").Rows.Count > 0)
                    {
                        MessageBox.Show("role failed: ->" + cboUser.Text + " has been existed!");
                        return false;
                    }                    

                    DataRow[] myRows = myInfo.Select();
                    if (myRows.Length == 1)
                    {
                        myRows[0].BeginEdit();
                        myRows[0]["RoleName"] = this.cboUser.Text.ToString();
                        myRows[0]["Remarks"] = this.txtRemark.Text.ToString();
                        myRows[0].EndEdit();

                        mySqlIO.UpdateDataTable("select * from RolesTable where  ID=" + currID, myInfo, false);
                        txtStates.Text = "Edit role successful:" + cboUser.Text + "";
                        result = true;
                    }
                    else
                    {
                        MessageBox.Show("Error: role->" + cboUser.Text + " was not existed!");
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
                mytip.SetToolTip(this.cboUser, "Role item name");
                mytip.SetToolTip(this.txtStates, "Operation logs");

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
                cboUser.Text = "";
                txtRemark.Clear();
                dt = mySqlIO.GetDataTable("select * from RolesTable", "RolesTable");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    currlst.Items.Add(dt.Rows[i]["RoleName"].ToString());                    
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
                //ID RoleName roleCode Remarks
                string filterString = currlst.SelectedItem.ToString();
                tempItemName = filterString.ToUpper().Trim();
                DataRow[] myROWS = mydt.Select("RoleName='" + filterString + "'");
                if (myROWS.Length == 1)
                {
                    currID = Convert.ToInt32(myROWS[0]["ID"]);
                    this.cboUser.Text = myROWS[0]["RoleName"].ToString();
                    this.txtRemark.Text = myROWS[0]["Remarks"].ToString();     
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
                        DataTable myInfo = mySqlIO.GetDataTable("select * from RolesTable where RoleName='" + cboUser.Text + "'", "RolesTable");

                        DataRow[] myRows = myInfo.Select();
                        if (myRows.Length == 1)
                        {
                            myRows[0].Delete();
                            mySqlIO.UpdateDataTable("select * from RolesTable where RoleName='" + cboUser.Text + "'", myInfo, false);
                            txtStates.Text = "Delete role successful:" + cboUser.Text + "";
                            runMsgState((byte)MsgState.Delete);
                        }
                        else
                        {
                            MessageBox.Show("Error! role : ->" + cboUser.Text + " was not existed!");
                            txtStates.Text = "Delete role failed:" + cboUser.Text + "";
                        }
                    }
                    else
                    {
                        txtStates.Text = "Cancel operation :" + cboUser.Text + "";
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
                this.cboUser.Text = "";

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
                this.cboUser.Enabled = false;               
                this.grpFunc.Enabled = true;  
         
                if (state == (byte)MsgState.AddNew)
                {
                    this.txtStates.Text = "Add a new role...";
                    this.txtStates.BackColor = Color.Yellow;
                    this.cboUser.BackColor = Color.Yellow;
                    blnAddNew = true;   
                    this.currlst.Enabled = false;   
                    this.cboUser.Enabled = true;
                    clearPrmtrInfo();    //140917_0
                }
                else if (state == (byte)MsgState.Edit)
                {
                    this.txtStates.Text = "Edit role item...";
                    this.txtStates.BackColor = Color.GreenYellow;
                    this.cboUser.BackColor = Color.GreenYellow;
                    blnAddNew = false;    
                    
                }
                else if (state == (byte)MsgState.SaveOK)
                {
                    this.txtStates.Text = "role item " + this.cboUser.Text + " has been saved successful!";
                    this.txtStates.BackColor = Color.YellowGreen;
                    this.cboUser.BackColor = Color.YellowGreen;
                    blnAddNew = false;   
                    
                }
                else if (state == (byte)MsgState.Delete)
                {
                    this.txtStates.Text = "role item " + this.cboUser.Text + " has been deleted successful!";
                    this.txtStates.BackColor = Color.Pink;
                    this.cboUser.BackColor = Color.Pink;
                    blnAddNew = false;   
                    
                }
                else if (state == (byte)MsgState.Clear)
                {
                    clearPrmtrInfo();
                    this.txtStates.Text = "";
                    this.txtStates.BackColor = Color.White;
                    this.cboUser.BackColor = Color.White;
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
            if (cboUser.Enabled == false)
            {
                cboUser.Enabled = true;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (cboUser.Text.Trim().Length<=0)
            {
                MessageBox.Show("Any data is invalid or Null)");
                return;
            }

            if (MainForm.checkItemLength("RoleName", cboUser.Text, 25)
                || MainForm.checkItemLength("Remarks", txtRemark.Text, 25))
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

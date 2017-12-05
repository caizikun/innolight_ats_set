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
    public partial class FunctionInfo : Form
    {
        DataIO mySqlIO; //140919 = new SqlManager(myLoginInfoStruct.ServerName, myLoginInfoStruct.DBName, myLoginInfoStruct.DBUser, myLoginInfoStruct.DBPassword);   //140917_2    //140911_0
        DataTable dt;
        int currID = -1;
        bool blnAddNew = false;
        string tempItemName = "", tempCode = "";
        LoginInfoStruct myLoginInfoStruct;

        public FunctionInfo(LoginInfoStruct pLoginInfoStruct)
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
                    DataTable myInfo = mySqlIO.GetDataTable("select * from FunctionTable where Title='" + txtUser.Text + "'", "FunctionTable");
                    if (mySqlIO.GetDataTable("select * from FunctionTable where FunctionCode='" + this.txtPWD.Text + "'", "FunctionTable").Rows.Count > 0)
                    {
                        MessageBox.Show("FunctionCode failed: ->" + this.txtPWD.Text + " has been existed!");
                        return false;
                    }
                    DataRow[] myRows = myInfo.Select();
                    if (myRows.Length == 0)
                    {
                        DataRow myRow = myInfo.NewRow();
                        myRow.BeginEdit();
                        myRow["Title"] = txtUser.Text.Trim();
                        myRow["FunctionCode"] = this.txtPWD.Text.ToString();
                        myRow["Remarks"] = this.txtRemark.Text.ToString();
                        myRow.EndEdit();
                        myInfo.Rows.Add(myRow);

                        mySqlIO.UpdateDataTable("select * from FunctionTable where Title='" + txtUser.Text + "'", myInfo, false);
                        txtStates.Text = "Add function successful:" + txtUser.Text + "";
                        blnAddNew = false;
                        result = true;
                    }
                    else
                    {
                        MessageBox.Show("Add function failed: ->" + txtUser.Text + " has been existed!");
                    }

                }
                else
                {
                    DataTable myInfo = mySqlIO.GetDataTable("select * from FunctionTable where ID=" + currID, "FunctionTable");

                    if (tempItemName != txtUser.Text.ToUpper().Trim() && mySqlIO.GetDataTable("select * from FunctionTable where Title='" + txtUser.Text + "'", "FunctionTable").Rows.Count > 0)
                    {
                        MessageBox.Show("Function failed: ->" + txtUser.Text + " has been existed!");
                        return false;
                    }
                    if (tempCode != txtPWD.Text.ToUpper().Trim() && mySqlIO.GetDataTable("select * from FunctionTable where FunctionCode='" + this.txtPWD.Text + "'", "FunctionTable").Rows.Count > 0)
                    {
                        MessageBox.Show("FunctionCode failed: ->" + this.txtPWD.Text + " has been existed!");
                        return false;
                    }

                    DataRow[] myRows = myInfo.Select();
                    if (myRows.Length == 1)
                    {
                        myRows[0].BeginEdit();
                        myRows[0]["Title"] = this.txtUser.Text.ToString();
                        myRows[0]["FunctionCode"] = this.txtPWD.Text.ToString();
                        myRows[0]["Remarks"] = this.txtRemark.Text.ToString();
                        myRows[0].EndEdit();

                        mySqlIO.UpdateDataTable("select * from FunctionTable where  ID=" + currID, myInfo, false);
                        txtStates.Text = "Edit function successful:" + txtUser.Text + "";
                        result = true;
                    }
                    else
                    {
                        MessageBox.Show("Error: function->" + txtUser.Text + " was not existed!");
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

                mytip.SetToolTip(btnAdd, "Add new function item");
                mytip.SetToolTip(btnRemove, "Delete a function item");
                mytip.SetToolTip(btnOK, "Save function data");

                mytip.SetToolTip(this.currlst, "List of server function items");
                mytip.SetToolTip(this.txtPWD, "FunctionCode of function item");
                mytip.SetToolTip(this.txtRemark, "Description of function item");
                mytip.SetToolTip(this.txtUser, "Title of function item");
                mytip.SetToolTip(this.txtStates, "Operation logs");

                mytip.ShowAlways = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void FunctionInfo_Load(object sender, EventArgs e)
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
                tempCode = "";
                currlst.Items.Clear();
                txtUser.Text = "";
                txtPWD.Clear();
                txtRemark.Clear();
                dt = mySqlIO.GetDataTable("select * from FunctionTable", "FunctionTable");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    currlst.Items.Add(dt.Rows[i]["Title"].ToString());
                    //currlst.SelectedIndex = 0;
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
                //ID Title FunctionCode Remarks
                string filterString = currlst.SelectedItem.ToString();
                tempItemName = filterString.ToUpper().Trim();
                tempCode = "";
                DataRow[] myROWS = mydt.Select("Title='" + filterString + "'");
                if (myROWS.Length == 1)
                {
                    currID = Convert.ToInt32(myROWS[0]["ID"]);
                    this.txtUser.Text = myROWS[0]["Title"].ToString();
                    this.txtPWD.Text = myROWS[0]["FunctionCode"].ToString();
                    tempCode = this.txtPWD.Text.ToUpper().Trim();
                    this.txtRemark.Text = myROWS[0]["Remarks"].ToString();
                }
                else
                {
                    currID = -1;
                    grpFunc.Enabled = false;
                    MessageBox.Show("Error! \n " + myROWS.Length + " records existed; \n filterString--> Title='" + filterString + "'");
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
                    if (this.txtPWD.Text.ToString() == "128")
                    {
                        this.txtPWD.Enabled = false;
                        btnRemove.Enabled = false;
                    }
                    else
                    {
                        this.txtPWD.Enabled = true;
                        btnRemove.Enabled = true;
                    }
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
                    if (currlst.SelectedIndex == -1)
                    {
                        MessageBox.Show("Pls choose a item first!");
                        return;
                    }
                    DialogResult drst = new DialogResult();
                    string myDeleteItemName = this.currlst.SelectedItem.ToString();
                    drst = (MessageBox.Show("Delete Item -->" + myDeleteItemName + "\n \n Pls choose 'Y' (是) to continue?",
                        "Notice", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2));

                    if (drst == DialogResult.Yes)
                    {
                        btnRemove.Enabled = false;
                        DataTable myInfo = mySqlIO.GetDataTable("select * from FunctionTable where Title='" + txtUser.Text + "'", "FunctionTable");

                        DataRow[] myRows = myInfo.Select();
                        if (myRows.Length == 1)
                        {
                            myRows[0].Delete();
                            mySqlIO.UpdateDataTable("select * from FunctionTable where Title='" + txtUser.Text + "'", myInfo, false);
                            txtStates.Text = "Delete function successful:" + txtUser.Text + "";
                            runMsgState((byte)MsgState.Delete);
                        }
                        else
                        {
                            MessageBox.Show("Error! function : ->" + txtUser.Text + " was not existed!");
                            txtStates.Text = "Delete function failed:" + txtUser.Text + "";
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
                    this.txtStates.Text = "Add a new Function...";
                    this.txtStates.BackColor = Color.Yellow;
                    this.txtUser.BackColor = Color.Yellow;
                    blnAddNew = true;
                    this.currlst.Enabled = false;
                    this.txtUser.Enabled = true;
                    clearPrmtrInfo();    //140917_0
                }
                else if (state == (byte)MsgState.Edit)
                {
                    this.txtStates.Text = "Edit Function item...";
                    this.txtStates.BackColor = Color.GreenYellow;
                    this.txtUser.BackColor = Color.GreenYellow;
                    blnAddNew = false;

                }
                else if (state == (byte)MsgState.SaveOK)
                {
                    this.txtStates.Text = "Function item " + this.txtUser.Text + " has been saved successful!";
                    this.txtStates.BackColor = Color.YellowGreen;
                    this.txtUser.BackColor = Color.YellowGreen;
                    blnAddNew = false;

                }
                else if (state == (byte)MsgState.Delete)
                {
                    this.txtStates.Text = "Function item " + this.txtUser.Text + " has been deleted successful!";
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
                || txtRemark.Text.Trim().Length <= 0
                || txtPWD.Text.Trim().Length <= 0)
            {
                MessageBox.Show("Any data is invalid or Null)");
                return;
            }

            if (MainForm.checkItemLength("Title", txtUser.Text, 25)
                || MainForm.checkItemLength("Remarks", txtRemark.Text, 25))
            {
                return;
            }
            string ss = Math.Log(Convert.ToInt32(txtPWD.Text), 2).ToString();

            for (int i = 0; i < ss.Length; i++)
            {
                if (!Char.IsNumber(ss, i))
                {
                    MessageBox.Show("FunctionCode data is invalid...Must be a power of 2!");
                    txtPWD.Focus();
                    return;
                }
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

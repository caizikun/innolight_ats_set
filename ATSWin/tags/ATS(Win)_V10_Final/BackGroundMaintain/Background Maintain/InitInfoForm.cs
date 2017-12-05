using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Maintain
{
    public partial class InitInfoForm : Form
    {

        public string PNName = "";

        bool blnAddChipCtrl = false;
        bool blnAddChipInit = false;

        public string PID = "";
        public string currChipCtrlID = "";
        public string currChipInitID = "";
        public string currEEPROMInitID = "";

        string tempChipCtrlItemName = "";
        string tempChipInitAddress = "";

        public InitInfoForm()
        {
            InitializeComponent();
        }
        void ShowMyTip()
        {
            try
            {
                ToolTip mytip = new ToolTip();
                //ChipCtrl
                mytip.SetToolTip(btnChipCtrlOK, "Save data");
                mytip.SetToolTip(btnChipCtrlAdd, "Add new data");
                mytip.SetToolTip(btnChipCtrlDelete, "Delete data");

                mytip.SetToolTip(txtSaveResult, "Operate log...");

                mytip.SetToolTip(this.cboItemName, "ItemName");
                mytip.SetToolTip(this.cboModelLine, "ModelChannelNo");
                mytip.SetToolTip(cboLength, "Length");
                mytip.SetToolTip(cboDriveType, "DriverType");
                mytip.SetToolTip(cboChipLine, "ChipLine");
                mytip.SetToolTip(cboStartAddr, "StartAddr <Decimal Number>");
                mytip.SetToolTip(cboEndianness, "Is Endianness-->default: 'false' ");

                //ChipInit 
                //DriveType ChipLine RegisterAddress Length ItemVaule

                mytip.SetToolTip(this.btnChipInitOK, "Save data");
                mytip.SetToolTip(this.btnAddChipInit, "Add new data");
                mytip.SetToolTip(this.btnRemoveChipInit, "Delete data");

                mytip.SetToolTip(txtChipInit, "Operate log...");

                mytip.SetToolTip(this.cboChipInitDriveType, "DriverType");
                mytip.SetToolTip(this.cboChipInitAddress, "ChipLine");
                mytip.SetToolTip(this.cboChipInitAddress, "StartAddr <Decimal Number>");
                mytip.SetToolTip(this.cboChipInitLength, "Length");
                mytip.SetToolTip(this.cboChipInitItemVaule, "ItemValue");
                mytip.SetToolTip(this.cboChipInitEndianness, "Is Endianness-->default: 'false' ");
                mytip.ShowAlways = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void InitInfoForm_Load(object sender, EventArgs e)
        {
            try
            {
                ShowMyTip();
                clearChipCtrlInfo();
                clearChipInitInfo();
                showAllDGVs();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void showAllDGVs()
        {
            try
            {
                MainForm.showTablefilterStrInfo(MainForm.GlobalDS.Tables["GlobalManufactureChipsetControl"], dgvChipCtrl, "PID=" + PID, "DriveType,RegisterAddress,ItemName,ModuleLine,ChipLine");
                MainForm.showTablefilterStrInfo(MainForm.GlobalDS.Tables["GlobalManufactureChipsetInitialize"], dgvChipInit, "PID=" + PID, "DriveType,RegisterAddress,ChipLine");
                //141009_0  //MainForm.showTablefilterStrInfo(MainForm.GlobalDS.Tables["TopoMSAEEPROMSet"], dgvEEPROMInit, "PID=" + PID);
                showAllChkStates();
                runChipCtrlMsgState((byte)MsgState.Clear);
                runChipInitMsgState((byte)MsgState.Clear);
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

        void runChipCtrlMsgState(byte state)
        {
            try
            {
                this.dgvChipCtrl.Enabled = true;
                this.cboItemName.Enabled = true;    //150206_0
                if (state == (byte)MsgState.AddNew)
                {
                    this.txtSaveResult.Text = "Add a new item...";
                    this.txtSaveResult.BackColor = Color.Yellow;
                    this.cboItemName.BackColor = Color.Yellow;
                    //this.cboItemName.Enabled = true;
                    //140917_0  this.dgvChipCtrl.Enabled = false;
                    blnAddChipCtrl = true;  //140917_0 
                    //dgvPrmtr.Enabled = false;
                    clearChipCtrlInfo();
                }
                else if (state == (byte)MsgState.Edit)
                {
                    this.txtSaveResult.Text = "Edit current item...";
                    this.txtSaveResult.BackColor = Color.GreenYellow;
                    this.cboItemName.BackColor = Color.GreenYellow;
                    //this.cboItemName.Enabled = false;
                    blnAddChipCtrl = false;  //140917_0 
                }
                else if (state == (byte)MsgState.SaveOK)
                {
                    this.txtSaveResult.Text = "Item: " + this.cboItemName.Text + " has been saved successful!";
                    this.txtSaveResult.BackColor = Color.YellowGreen;
                    this.cboItemName.BackColor = Color.YellowGreen;
                    this.cboItemName.Enabled = false;
                    blnAddChipCtrl = false;  //140917_0
                }
                else if (state == (byte)MsgState.Delete)
                {
                    this.txtSaveResult.Text = "Item: " + this.cboItemName.Text + " has been deleted successful!";
                    this.txtSaveResult.BackColor = Color.Pink;
                    this.cboItemName.BackColor = Color.Pink;
                    this.cboItemName.Enabled = false;
                    blnAddChipCtrl = false;  //140917_0
                    clearChipCtrlInfo();
                }
                else if (state == (byte)MsgState.Clear)
                {
                    this.txtSaveResult.Text = "";
                    this.txtSaveResult.BackColor = Color.White;
                    this.cboItemName.BackColor = Color.White;
                    this.cboItemName.Enabled = false;
                    blnAddChipCtrl = false;  //140917_0   
                    clearChipCtrlInfo();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void runChipInitMsgState(byte state)
        {
            try
            {
                this.dgvChipInit.Enabled = true;
                this.cboChipInitDriveType.Enabled = true;    //150206_0
                if (state == (byte)MsgState.AddNew)
                {
                    this.txtChipInit.Text = "Add a new item...";
                    this.txtChipInit.BackColor = Color.Yellow;
                    this.cboChipInitDriveType.BackColor = Color.Yellow;
                    //this.cboChipInitDriveType.Enabled = true;
                    //140917_0  this.dgvChipInit.Enabled = false;
                    blnAddChipInit = true;  //140917_0
                    //dgvPrmtr.Enabled = false;
                    clearChipInitInfo();
                }
                else if (state == (byte)MsgState.Edit)
                {
                    this.txtChipInit.Text = "Edit current item...";
                    this.txtChipInit.BackColor = Color.GreenYellow;
                    this.cboChipInitDriveType.BackColor = Color.GreenYellow;
                    // this.cboChipInitDriveType.Enabled = false;
                    blnAddChipInit = false;  //140917_0
                }
                else if (state == (byte)MsgState.SaveOK)
                {
                    this.txtChipInit.Text = "Item: " + this.cboChipInitDriveType.Text + " has been saved successful!";
                    this.txtChipInit.BackColor = Color.YellowGreen;
                    this.cboChipInitDriveType.BackColor = Color.YellowGreen;
                    this.cboChipInitDriveType.Enabled = false;
                    blnAddChipInit = false;  //140917_0
                }
                else if (state == (byte)MsgState.Delete)
                {
                    this.txtChipInit.Text = "Item: " + this.cboChipInitDriveType.Text + " has been deleted successful!";
                    this.txtChipInit.BackColor = Color.Pink;
                    this.cboChipInitDriveType.BackColor = Color.Pink;
                    this.cboChipInitDriveType.Enabled = false;
                    blnAddChipInit = false;  //140917_0
                    clearChipInitInfo();
                }
                else if (state == (byte)MsgState.Clear)
                {
                    this.txtChipInit.Text = "";
                    this.txtChipInit.BackColor = Color.White;
                    this.cboChipInitDriveType.BackColor = Color.White;
                    this.cboChipInitDriveType.Enabled = false;
                    blnAddChipInit = false;  //140917_0
                    clearChipInitInfo();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void showAllChkStates()
        {
            try
            {
                if (this.dgvChipCtrl.Rows.Count > 0)
                {
                    this.chkChipsetControl.CheckState = CheckState.Checked;
                }
                else
                {
                    this.chkChipsetControl.CheckState = CheckState.Unchecked;
                }

                if (this.dgvChipInit.Rows.Count > 0)
                {
                    this.chkChipsetInitialize.CheckState = CheckState.Checked;
                }
                else
                {
                    this.chkChipsetInitialize.CheckState = CheckState.Unchecked;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void getChipCtrldgvRowsInfo()
        {
            try
            {
                runChipCtrlMsgState((byte)MsgState.Clear);
                if (dgvChipCtrl.CurrentRow != null && this.Visible)
                {
                    runChipCtrlMsgState((byte)MsgState.Edit);
                    //ItemName ModuleLine ChipLine DriveType RegisterAddress Length
                    currChipCtrlID = dgvChipCtrl.CurrentRow.Cells["ID"].Value.ToString();
                    string myPID = dgvChipCtrl.CurrentRow.Cells["PID"].Value.ToString();
                    if (myPID.ToUpper() != PID)
                    {
                        MessageBox.Show("Parent PID Error! \n Current PID = " + myPID + " \n System PID=" + PID);
                    }
                    tempChipCtrlItemName = dgvChipCtrl.CurrentRow.Cells["ItemName"].Value.ToString();
                    this.cboItemName.Text = dgvChipCtrl.CurrentRow.Cells["ItemName"].Value.ToString();
                    this.cboModelLine.Text = dgvChipCtrl.CurrentRow.Cells["ModuleLine"].Value.ToString();
                    this.cboChipLine.Text = dgvChipCtrl.CurrentRow.Cells["ChipLine"].Value.ToString();
                    this.cboDriveType.SelectedIndex = Convert.ToInt32(dgvChipCtrl.CurrentRow.Cells["DriveType"].Value);

                    this.cboStartAddr.Text = dgvChipCtrl.CurrentRow.Cells["RegisterAddress"].Value.ToString();
                    this.cboLength.Text = dgvChipCtrl.CurrentRow.Cells["Length"].Value.ToString();
                    this.cboEndianness.Text = dgvChipCtrl.CurrentRow.Cells["Endianness"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        void getChipInitdgvRowsInfo()
        {
            //DriveType //ChipLine //RegisterAddress //Length //ItemVaule
            try
            {
                runChipInitMsgState((byte)MsgState.Clear);
                if (dgvChipInit.CurrentRow != null && this.Visible)
                {
                    runChipInitMsgState((byte)MsgState.Edit);
                    currChipInitID = this.dgvChipInit.CurrentRow.Cells["ID"].Value.ToString();
                    string myPID = dgvChipInit.CurrentRow.Cells["PID"].Value.ToString();
                    if (myPID.ToUpper() != PID)
                    {
                        MessageBox.Show("Parent PID Error! \n Current PID = " + myPID + " \n System PID=" + PID);
                    }
                    tempChipInitAddress = dgvChipInit.CurrentRow.Cells["RegisterAddress"].Value.ToString();
                    this.cboChipInitDriveType.SelectedIndex = Convert.ToInt32(dgvChipInit.CurrentRow.Cells["DriveType"].Value);
                    this.cboChipInitChipLine.Text = dgvChipInit.CurrentRow.Cells["ChipLine"].Value.ToString();
                    this.cboChipInitAddress.Text = dgvChipInit.CurrentRow.Cells["RegisterAddress"].Value.ToString();
                    this.cboChipInitLength.Text = dgvChipInit.CurrentRow.Cells["Length"].Value.ToString();
                    this.cboChipInitItemVaule.Text = dgvChipInit.CurrentRow.Cells["ItemValue"].Value.ToString();
                    this.cboChipInitEndianness.Text = dgvChipInit.CurrentRow.Cells["Endianness"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        enum DriveType
        {
            //0: LDD        1: AMP        2: DAC        3: CDR"
            LDD = 0,
            AMP = 1,
            DAC = 2,
            CDR = 3
        }

        void clearChipCtrlInfo()
        {
            try
            {
                //string myID = dgvPrmtr.CurrentRow.Cells["ID"].Value.ToString();
                this.cboEndianness.Items.Clear();
                this.cboEndianness.Items.Add("False");
                this.cboEndianness.Items.Add("True");

                this.cboDriveType.Items.Clear();
                this.cboDriveType.Items.Add(DriveType.LDD.ToString());
                this.cboDriveType.Items.Add(DriveType.AMP.ToString());
                this.cboDriveType.Items.Add(DriveType.DAC.ToString());
                this.cboDriveType.Items.Add(DriveType.CDR.ToString());

                DataRow[] drs = MainForm.GlobalDS.Tables["GlobalManufactureChipsetControl"].Select();
                foreach (DataRow dr in drs)
                {
                    if (this.cboItemName.Items.Contains(dr["ItemName"]) == false)
                    {
                        cboItemName.Items.Add(dr["ItemName"]);
                    }
                }

                this.cboItemName.Text = "";
                this.cboModelLine.Text = "";
                this.cboChipLine.Text = "";
                this.cboDriveType.Text = "";
                this.cboStartAddr.Text = "";
                this.cboLength.Text = "";
                this.cboEndianness.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void clearChipInitInfo()
        {
            try
            {

                this.cboChipInitDriveType.Items.Clear();
                this.cboChipInitDriveType.Items.Add(DriveType.LDD.ToString());
                this.cboChipInitDriveType.Items.Add(DriveType.AMP.ToString());
                this.cboChipInitDriveType.Items.Add(DriveType.DAC.ToString());
                this.cboChipInitDriveType.Items.Add(DriveType.CDR.ToString());

                this.cboChipInitAddress.Text = "";
                this.cboChipInitChipLine.Text = "";
                this.cboChipInitDriveType.Text = "";
                this.cboChipInitItemVaule.Text = "";
                this.cboChipInitLength.Text = "";
                this.cboChipLine.Text = "";

                this.cboChipInitEndianness.Items.Clear();
                this.cboChipInitEndianness.Items.Add("False");
                this.cboChipInitEndianness.Items.Add("True");
                this.cboChipInitEndianness.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnChipCtrlOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (MainForm.checkItemLength("ItemName", cboItemName.Text, 20))
                {
                    return;
                }
                if (
                        this.cboItemName.Text.ToString().Trim().Length == 0 ||
                        this.cboModelLine.Text.ToString().Trim().Length == 0 ||
                        this.cboChipLine.Text.ToString().Trim().Length == 0 ||
                        this.cboDriveType.Text.ToString().Trim().Length == 0 ||
                        this.cboStartAddr.Text.ToString().Trim().Length == 0 ||
                        this.cboLength.Text.ToString().Trim().Length == 0 ||
                        this.cboEndianness.Text.ToString().Trim().Length == 0
                    )   //140923_0  this.cboEndianness.Text.ToString().Trim().Length == 0
                {
                    MessageBox.Show("Data is incomplete,Pls confirm again?");
                    return;
                }
                if (blnAddChipCtrl || (tempChipCtrlItemName.Length > 0 && tempChipCtrlItemName.ToUpper() != cboItemName.Text.ToString().ToUpper().Trim()))  //140922_0
                {
                    if (MainForm.currPrmtrCountExisted(MainForm.GlobalDS.Tables["GlobalManufactureChipsetControl"], "PID=" + PID
                        + " and ItemName ='" + this.cboItemName.Text.ToString() + "'"
                        + " and DriveType= " + this.cboDriveType.SelectedIndex.ToString()
                        + " and ModuleLine= " + this.cboModelLine.Text
                        + " and RegisterAddress= " + this.cboStartAddr.Text) > 0)
                    {
                        //" and DriveType= " + this.cboDriveType.SelectedIndex.ToString() 140706_2
                        //140704 约束 PID+FieldName+Channel
                        MessageBox.Show("The data of ItemName has existed! <Violate unique rule>");
                        return;
                    }
                }
                bool result = EditInfoForChipCtrlDT(MainForm.GlobalDS.Tables["GlobalManufactureChipsetControl"]);
                showAllChkStates();
                if (result)
                {
                    MainForm.ISNeedUpdateflag = true;
                    //新增OK后需要置位!
                    if (blnAddChipCtrl)
                    {
                        blnAddChipCtrl = false; //140817_0 
                        //140817_0 blnISAddCtrlStates(false);  //140706_3
                    }
                    runChipCtrlMsgState((byte)MsgState.SaveOK);
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        bool EditInfoForChipCtrlDT(DataTable mydt)  //140704_5 TBD
        {
            bool result = false;
            try
            {
                //ItemName ModuleLine ChipLine DriveType RegisterAddress Length
                string filterString = "ID=" + currChipCtrlID + " AND PID=" + PID;
                DataRow[] myROWS = mydt.Select(filterString);
                if (myROWS.Length == 1)
                {
                    if (this.blnAddChipCtrl)
                    {
                        MessageBox.Show("Add new data error! \n " + myROWS.Length + " records existed; \n filterString-->  " + filterString);
                        return result;
                    }
                    myROWS[0].BeginEdit();
                    myROWS[0]["PID"] = this.PID;
                    myROWS[0]["ItemName"] = this.cboItemName.Text.ToString();
                    myROWS[0]["ModuleLine"] = this.cboModelLine.Text.ToString();
                    myROWS[0]["ChipLine"] = this.cboChipLine.Text.ToString();
                    myROWS[0]["DriveType"] = this.cboDriveType.SelectedIndex.ToString();// 按索引保存 int
                    myROWS[0]["RegisterAddress"] = this.cboStartAddr.Text.ToString();
                    myROWS[0]["Length"] = this.cboLength.Text.ToString();
                    myROWS[0]["Endianness"] = this.cboEndianness.Text.ToString();
                    myROWS[0].EndEdit();
                    result = true;
                }
                else if (this.blnAddChipCtrl)
                {
                    DataRow myNewRow = mydt.NewRow();
                    myNewRow.BeginEdit();
                    myNewRow["ID"] = currChipCtrlID;
                    myNewRow["PID"] = this.PID;
                    myNewRow["ItemName"] = this.cboItemName.Text.ToString();
                    myNewRow["ModuleLine"] = this.cboModelLine.Text.ToString();
                    myNewRow["ChipLine"] = this.cboChipLine.Text.ToString();
                    myNewRow["DriveType"] = this.cboDriveType.SelectedIndex.ToString(); // 按索引保存 int
                    myNewRow["RegisterAddress"] = this.cboStartAddr.Text.ToString();
                    myNewRow["Length"] = this.cboLength.Text.ToString();
                    myNewRow["Endianness"] = this.cboEndianness.Text.ToString();
                    mydt.Rows.Add(myNewRow);

                    myNewRow.EndEdit();
                    MainForm.setDgvCurrCell(dgvChipCtrl, "ID", myNewRow["ID"].ToString());
                    MainForm.mylastIDGlobalChipCtrl++;
                    MainForm.myAddCountGlobalChipCtrl++;
                    result = true;
                    blnAddChipCtrl = false;
                }
                else
                {
                    MessageBox.Show("Error! \n " + myROWS.Length + " records existed; \n filterString-->" + filterString);

                }

                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return result;
            }
        }

        private void btnChipCtrlAdd_Click(object sender, EventArgs e)
        {
            try
            {

                currChipCtrlID = (MainForm.mylastIDGlobalChipCtrl + 1).ToString();
                runChipCtrlMsgState((byte)MsgState.AddNew); //140917_0 .AddNew
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void btnChipCtrlDelete_Click(object sender, EventArgs e)
        {
            if (blnAddChipCtrl) //140917_0 新增误点新增取消新增状态!并直接返回
            {
                blnAddChipCtrl = false;
                runChipCtrlMsgState((byte)MsgState.Clear);
                return;
            }
            if (dgvChipCtrl.CurrentRow == null || dgvChipCtrl.CurrentRow.Index == -1)  //140710  
            {
                MessageBox.Show("Pls choose a item first!");
                return;
            }
            else
            {
                try
                {
                    DialogResult drst = new DialogResult();
                    string myDeleteItemName = dgvChipCtrl.CurrentRow.Cells["ItemName"].Value.ToString()
                        + "-->" + dgvChipCtrl.CurrentRow.Cells["DriveType"].Value.ToString()
                        + "-->" + dgvChipCtrl.CurrentRow.Cells["RegisterAddress"].Value.ToString();
                    string myID = dgvChipCtrl.CurrentRow.Cells["ID"].Value.ToString();
                    drst = (MessageBox.Show("Delete Item -->" + myDeleteItemName + "\n \n Pls choose 'Y' (是) to continue?",
                        "Notice", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2));

                    if (drst == DialogResult.Yes)
                    {
                        bool result = MainForm.DeleteItemForDT(MainForm.GlobalDS.Tables["GlobalManufactureChipsetControl"],
                            "PID=" + PID + "and ID=" + myID);   //141112_0
                        if (result)
                        {
                            MainForm.ISNeedUpdateflag = true;
                            MessageBox.Show("Item: " + myDeleteItemName + " has been deleted successful!");
                            runChipCtrlMsgState((byte)MsgState.Delete);

                        }
                        else
                        {
                            MessageBox.Show("Item: " + myDeleteItemName + " delete Failed!");
                        }
                    }
                    this.cboItemName.BackColor = Color.White;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void btnPreviousPage_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        bool EditInfoForChipInitDT(DataTable mydt)  //140704_5 TBD
        {
            bool result = false;
            try
            {
                //DriveType //ChipLine //RegisterAddress //Length //ItemVaule
                string filterString = "ID=" + currChipInitID + " AND PID=" + PID;
                DataRow[] myROWS = mydt.Select(filterString);
                if (myROWS.Length == 1)
                {
                    if (this.blnAddChipInit)
                    {
                        MessageBox.Show("Add new data error! \n " + myROWS.Length + " records existed; \n filterString-->  " + filterString);
                        return result;
                    }
                    myROWS[0].BeginEdit();
                    myROWS[0]["PID"] = this.PID;
                    myROWS[0]["DriveType"] = this.cboChipInitDriveType.SelectedIndex.ToString();// 按索引保存 int;
                    myROWS[0]["ChipLine"] = this.cboChipInitChipLine.Text.ToString();
                    myROWS[0]["RegisterAddress"] = this.cboChipInitAddress.Text.ToString();
                    myROWS[0]["Length"] = this.cboChipInitLength.Text.ToString();
                    myROWS[0]["ItemValue"] = this.cboChipInitItemVaule.Text.ToString();
                    myROWS[0]["Endianness"] = this.cboChipInitEndianness.Text.ToString();   //141105_0
                    myROWS[0].EndEdit();
                    result = true;
                }
                else if (this.blnAddChipInit)
                {
                    DataRow myNewRow = mydt.NewRow();
                    myNewRow.BeginEdit();
                    myNewRow["ID"] = currChipInitID;
                    myNewRow["PID"] = this.PID;
                    myNewRow["DriveType"] = this.cboChipInitDriveType.SelectedIndex.ToString(); // 按索引保存 int;
                    myNewRow["ChipLine"] = this.cboChipInitChipLine.Text.ToString();
                    myNewRow["RegisterAddress"] = this.cboChipInitAddress.Text.ToString();
                    myNewRow["Length"] = this.cboChipInitLength.Text.ToString();
                    myNewRow["ItemValue"] = this.cboChipInitItemVaule.Text.ToString();
                    myNewRow["Endianness"] = this.cboChipInitEndianness.Text.ToString(); //141105_0
                    mydt.Rows.Add(myNewRow);

                    myNewRow.EndEdit();
                    MainForm.setDgvCurrCell(dgvChipInit, "ID", myNewRow["ID"].ToString());
                    MainForm.mylastIDGlobalChipInit++;    //141022_0 FIX Bug
                    MainForm.myAddCountGlobalChipInit++;    //141022_0 FIX Bug
                    result = true;
                    blnAddChipInit = false;
                }
                else
                {
                    MessageBox.Show("Error! " + myROWS.Length + (" records existed; \n filterString--> " + filterString));

                }

                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return result;
            }
        }

        private void btnChipInitOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (
                        this.cboChipInitAddress.Text.ToString().Trim().Length == 0 ||
                        this.cboChipInitChipLine.Text.ToString().Trim().Length == 0 ||
                        this.cboChipInitDriveType.Text.ToString().Trim().Length == 0 ||
                        this.cboChipInitItemVaule.ToString().Trim().Length == 0 ||
                        this.cboChipInitLength.ToString().Trim().Length == 0 ||
                        this.cboChipInitEndianness.ToString().Trim().Length == 0
                    )
                {
                    MessageBox.Show("Some data is invalid ");
                    return;
                }
                if (blnAddChipInit || (tempChipInitAddress.Length > 0 && tempChipInitAddress.ToUpper() != cboChipInitAddress.Text.ToString().ToUpper().Trim()))  //140922_0
                {
                    //"PID=" + PID + " and DriveType =" + this.cboChipInitDriveType.SelectedIndex.ToString() 140706_2
                    //DriveType //ChipLine //RegisterAddress //Length //ItemVaule
                    if (MainForm.currPrmtrCountExisted(MainForm.GlobalDS.Tables["GlobalManufactureChipsetInitialize"],
                        "PID=" + PID + " and DriveType =" + this.cboChipInitDriveType.SelectedIndex.ToString()
                        + " and RegisterAddress= " + this.cboChipInitAddress.Text
                        + " and ChipLine= " + this.cboChipInitChipLine.Text) > 0)
                    {
                        //140704_4 约束 
                        MessageBox.Show("Edit data error! \n This item has been existed!<unique>");
                        return;
                    }
                }
                bool result = EditInfoForChipInitDT(MainForm.GlobalDS.Tables["GlobalManufactureChipsetInitialize"]);
                showAllChkStates();
                if (result)
                {
                    MainForm.ISNeedUpdateflag = true;
                    //新增OK后需要置位!
                    if (blnAddChipInit)
                    {
                        blnAddChipInit = false;
                        //140917_0  blnISAddChipInitStates(false);
                    }
                    runChipInitMsgState((byte)MsgState.SaveOK);
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void btnRemoveChipInit_Click(object sender, EventArgs e)
        {
            if (blnAddChipInit) //140917_0 新增误点新增取消新增状态!并直接返回
            {
                blnAddChipInit = false;
                runChipInitMsgState((byte)MsgState.Clear);
                return;
            }

            if (dgvChipInit.CurrentRow == null || this.dgvChipInit.CurrentRow.Index == -1)  //140710
            {
                MessageBox.Show("Pls choose a item first!");
                return;
            }
            else
            {
                try
                {
                    DialogResult drst = new DialogResult();
                    //DriveType //ChipLine //RegisterAddress //Length //ItemVaule
                    string myDeleteItemName = dgvChipInit.CurrentRow.Cells["DriveType"].Value.ToString()
                        + "-->" + dgvChipInit.CurrentRow.Cells["ChipLine"].Value.ToString()
                        + "-->" + dgvChipInit.CurrentRow.Cells["RegisterAddress"].Value.ToString();
                    string myID = dgvChipInit.CurrentRow.Cells["ID"].Value.ToString();

                    drst = (MessageBox.Show("Delete Item -->" + myDeleteItemName + "\n \n Pls choose 'Y' (是) to continue?",
                        "Notice", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2));

                    if (drst == DialogResult.Yes)
                    {
                        bool result = MainForm.DeleteItemForDT(MainForm.GlobalDS.Tables["GlobalManufactureChipsetInitialize"],
                            "PID=" + PID + "and ID=" + myID);
                        if (result)
                        {
                            MainForm.ISNeedUpdateflag = true;
                            MessageBox.Show("Item: " + myDeleteItemName + " has been deleted successful!");
                            runChipInitMsgState((byte)MsgState.Delete);
                        }
                        else
                        {
                            MessageBox.Show("Item: " + myDeleteItemName + " delete Failed!");
                        }
                    }
                    this.cboChipInitDriveType.BackColor = Color.White;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void btnAddChipInit_Click(object sender, EventArgs e)
        {
            try
            {

                currChipInitID = (MainForm.mylastIDGlobalChipInit + 1).ToString();

                runChipInitMsgState((byte)MsgState.AddNew);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void InitInfoForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (chkChipsetControl.Checked == false ||
                chkChipsetInitialize.Checked == false)
            {
                MessageBox.Show("Incomplete data <ChipsetControl or ChipsetInitialize maybe not existed>"
                    , "Notice"
                    , MessageBoxButtons.OK);
            }
        }

        private void dgvChipCtrl_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            getChipCtrldgvRowsInfo();
        }

        private void dgvChipInit_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            getChipInitdgvRowsInfo();
        }

        private void dgvChipCtrl_CurrentCellChanged(object sender, EventArgs e) //150206_0
        {
            getChipCtrldgvRowsInfo();
        }

        private void dgvChipInit_CurrentCellChanged(object sender, EventArgs e) //150206_0
        {
            getChipInitdgvRowsInfo();
        }
    }
}

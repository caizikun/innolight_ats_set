using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Maintain
{
    public partial class MConfigInit : Form
    {
        public bool blnAddMConfigInit = false;
        public string TestPlanName = "";
        public long PID = -1;
        string currMConfigInitID = "";
                
        public MConfigInit()
        {
            InitializeComponent();
        }

        private void btnEEPROMInitOK_Click(object sender, EventArgs e)
        {
            //SlaveAddress Page StartAddress Length ItemValue
            try
            {
                if (
                        this.CboMConfigInitLen.Text.ToString().Trim().Length == 0 ||
                        this.CboMConfigInitPage.Text.ToString().Trim().Length == 0 ||
                        this.CboMConfigInitSlaveAddr.Text.ToString().Trim().Length == 0 ||
                        this.CboMConfigInitStartAddr.Text.ToString().Trim().Length == 0 ||
                        this.CboMConfigInitValue.Text.ToString().Trim().Length == 0
                    )
                {
                    MessageBox.Show("Data is incomplete,Pls confirm again?");
                    return;
                }
                if (blnAddMConfigInit)
                {
                    if (MainForm.currPrmtrCountExisted(MainForm.TopoToatlDS.Tables["TopoManufactureConfigInit"], "PID=" + PID
                        + " and SlaveAddress ='" + this.CboMConfigInitSlaveAddr.Text.ToString() + "'"
                        + " and Page= " + this.CboMConfigInitPage.Text.ToString()
                        + " and StartAddress= " + this.CboMConfigInitStartAddr.Text) > 0)
                    {
                        //约束 PID+SlaveAddress+Page+StartAddress
                        MessageBox.Show("The new data of ItemName has existed! <Violate unique rule>");
                        return;
                    }
                }
                bool result = EditInfoForMConfigInitDT(MainForm.TopoToatlDS.Tables["TopoManufactureConfigInit"]);

                if (result)
                {
                    MainForm.ISNeedUpdateflag = true;
                    //新增OK后需要置位!
                    if (blnAddMConfigInit)
                    {
                        blnAddMConfigInit = false;
                    }
                    runMConfigInitMsgState((byte)MsgState.SaveOK);
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

        void ShowMyTip()
        {
            try
            {
                ToolTip mytip = new ToolTip();
                //MConfigInit
                mytip.SetToolTip(btnEEPROMInitOK, "Save data");
                mytip.SetToolTip(btnMConfigInitAdd, "Add new data");
                mytip.SetToolTip(btnMConfigInitDelete, "Delete data");
                mytip.SetToolTip(txtSaveResult, "Operate log...");

                mytip.SetToolTip(this.CboMConfigInitSlaveAddr, "SlaveAddr");
                mytip.SetToolTip(this.CboMConfigInitStartAddr, "StartAddr");
                mytip.SetToolTip(CboMConfigInitValue, "ItemValue");
                mytip.SetToolTip(CboMConfigInitPage, "Page");
                mytip.SetToolTip(CboMConfigInitLen, "Length of ItemValue");

                mytip.ShowAlways = true;
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

        void runMConfigInitMsgState(byte state)
        {
            try
            {
                this.dgvMConfigInit.Enabled = true;
                this.CboMConfigInitStartAddr.Enabled = true;
                if (state == (byte)MsgState.AddNew)
                {
                    this.txtSaveResult.Text = "Add a new item...";
                    this.txtSaveResult.BackColor = Color.Yellow;
                    this.CboMConfigInitStartAddr.BackColor = Color.Yellow;
                    
                    blnAddMConfigInit = true;
                    clearMConfigInitInfo();
                }
                else if (state == (byte)MsgState.Edit)
                {
                    this.txtSaveResult.Text = "Edit current item...";
                    this.txtSaveResult.BackColor = Color.GreenYellow;
                    this.CboMConfigInitStartAddr.BackColor = Color.GreenYellow;
                    blnAddMConfigInit = false;  
                }
                else if (state == (byte)MsgState.SaveOK)
                {
                    this.txtSaveResult.Text = "StartAddr: " + this.CboMConfigInitStartAddr.Text + " has been saved successful!";
                    this.txtSaveResult.BackColor = Color.YellowGreen;
                    this.CboMConfigInitStartAddr.BackColor = Color.YellowGreen;                   
                    blnAddMConfigInit = false;
                    this.CboMConfigInitStartAddr.Enabled = false;
                }
                else if (state == (byte)MsgState.Delete)
                {
                    this.txtSaveResult.Text = "StartAddr: " + this.CboMConfigInitStartAddr.Text + " has been deleted successful!";
                    this.txtSaveResult.BackColor = Color.Pink;
                    this.CboMConfigInitStartAddr.BackColor = Color.Pink;                   
                    blnAddMConfigInit = false;
                    this.CboMConfigInitStartAddr.Enabled = false;
                    clearMConfigInitInfo();
                }
                else if (state == (byte)MsgState.Clear)
                {
                    this.txtSaveResult.Text = "";
                    this.txtSaveResult.BackColor = Color.White;
                    this.CboMConfigInitStartAddr.BackColor = Color.White;                    
                    blnAddMConfigInit = false;
                    this.CboMConfigInitStartAddr.Enabled = false;
                    clearMConfigInitInfo();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
                
        void getMConfigInitdgvRowsInfo()
        {
            try
            {
                runMConfigInitMsgState((byte)MsgState.Clear);
                //SlaveAddress Page StartAddress Length ItemValue
                if (dgvMConfigInit.CurrentRow != null && this.Visible)
                {
                    currMConfigInitID = dgvMConfigInit.CurrentRow.Cells["ID"].Value.ToString();
                    string myPID = dgvMConfigInit.CurrentRow.Cells["PID"].Value.ToString();
                    if (myPID.ToUpper() != PID.ToString().ToUpper())
                    {
                        MessageBox.Show("Parent PID Error! \n Current PID = " + myPID + " \n System PID=" + PID);
                    }
                    this.CboMConfigInitSlaveAddr.Text = dgvMConfigInit.CurrentRow.Cells["SlaveAddress"].Value.ToString();
                    this.CboMConfigInitPage.Text = dgvMConfigInit.CurrentRow.Cells["Page"].Value.ToString();
                    this.CboMConfigInitStartAddr.Text = dgvMConfigInit.CurrentRow.Cells["StartAddress"].Value.ToString();
                    this.CboMConfigInitLen.Text = dgvMConfigInit.CurrentRow.Cells["Length"].Value.ToString();
                    this.CboMConfigInitValue.Text = dgvMConfigInit.CurrentRow.Cells["ItemValue"].Value.ToString();
                    runMConfigInitMsgState((byte)MsgState.Edit);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        
        void clearMConfigInitInfo()
        {
            try
            {
                this.CboMConfigInitLen.Items.Clear();
                this.CboMConfigInitSlaveAddr.Items.Clear();
                this.CboMConfigInitSlaveAddr.Items.Add("160");
                this.CboMConfigInitSlaveAddr.Items.Add("162");

                this.CboMConfigInitLen.Items.Add("1");
                this.CboMConfigInitLen.Items.Add("2");
                this.CboMConfigInitLen.Items.Add("4");

                this.CboMConfigInitLen.Text = "";
                this.CboMConfigInitPage.Text = "";
                this.CboMConfigInitSlaveAddr.Text = "";
                this.CboMConfigInitStartAddr.Text = "";
                this.CboMConfigInitValue.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
       
        bool EditInfoForMConfigInitDT(DataTable mydt)  
        {
            bool result = false;
            try
            {
                //SlaveAddress Page StartAddress Length ItemValue
                string filterString = "ID=" + currMConfigInitID + " AND PID=" + PID;
                DataRow[] myROWS = mydt.Select(filterString);
                if (myROWS.Length == 1)
                {
                    if (this.blnAddMConfigInit)
                    {
                        MessageBox.Show("Add new data error! \n " + myROWS.Length + " records existed; \n filterString-->  " + filterString );
                        return result;
                    }
                    myROWS[0].BeginEdit();
                    myROWS[0]["PID"] = this.PID;
                    myROWS[0]["SlaveAddress"] = this.CboMConfigInitSlaveAddr.Text.ToString();
                    myROWS[0]["Page"] = this.CboMConfigInitPage.Text.ToString();
                    myROWS[0]["StartAddress"] = this.CboMConfigInitStartAddr.Text.ToString();
                    myROWS[0]["Length"] = this.CboMConfigInitLen.Text.ToString();
                    myROWS[0]["ItemValue"] = this.CboMConfigInitValue.Text.ToString();
                    myROWS[0].EndEdit();
                    result = true;
                }
                else if (this.blnAddMConfigInit)
                {
                    DataRow myNewRow = mydt.NewRow();
                    myNewRow.BeginEdit();
                    myNewRow["ID"] = currMConfigInitID;
                    myNewRow["PID"] = this.PID;
                    myNewRow["PID"] = this.PID;
                    myNewRow["SlaveAddress"] = this.CboMConfigInitSlaveAddr.Text.ToString();
                    myNewRow["Page"] = this.CboMConfigInitPage.Text.ToString();
                    myNewRow["StartAddress"] = this.CboMConfigInitStartAddr.Text.ToString();
                    myNewRow["Length"] = this.CboMConfigInitLen.Text.ToString();
                    myNewRow["ItemValue"] = this.CboMConfigInitValue.Text.ToString();
                    mydt.Rows.Add(myNewRow);

                    myNewRow.EndEdit();
                    MainForm.setDgvCurrCell(dgvMConfigInit, "ID", myNewRow["ID"].ToString());
                    MainForm.mylastIDMConfigInit++;
                    MainForm.myAddCountMConfigInit++;             
                    result = true;
                    blnAddMConfigInit = false;
                }
                else
                {
                    MessageBox.Show("Error! \n " + myROWS.Length + " records existed; \n filterString-->" + filterString );

                }
                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return result;
            }
        }

        private void btnMConfigInitAdd_Click(object sender, EventArgs e)
        {
            try
            {
                currMConfigInitID = (MainForm.mylastIDMConfigInit + 1).ToString();
                runMConfigInitMsgState((byte)MsgState.AddNew); 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
                      
        }

        private void btnMConfigInitDelete_Click(object sender, EventArgs e)
        {
            if (blnAddMConfigInit) //新增误点新增取消新增状态!并直接返回
            {
                blnAddMConfigInit = false;
                runMConfigInitMsgState((byte)MsgState.Clear);
                return;
            } 
            if (dgvMConfigInit.CurrentRow == null || dgvMConfigInit.CurrentRow.Index == -1)  //140710  
            {
                MessageBox.Show("Pls choose a item first!");
            }
            else
            {
                try
                {   //SlaveAddress Page StartAddress Length ItemValue
                    DialogResult drst = new DialogResult();
                    string myDeleteItemName = dgvMConfigInit.CurrentRow.Cells["SlaveAddress"].Value.ToString()
                        + "-->" + dgvMConfigInit.CurrentRow.Cells["Page"].Value.ToString()
                        + "-->" + dgvMConfigInit.CurrentRow.Cells["StartAddress"].Value.ToString(); 
                    string myID = dgvMConfigInit.CurrentRow.Cells["ID"].Value.ToString();
                    drst = (MessageBox.Show("Delete Item -->" + myDeleteItemName + "\n \n Pls choose 'Y' (是) to continue?",
                        "Notice", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2));

                    if (drst == DialogResult.Yes)
                    {
                        bool result = MainForm.DeleteItemForDT(MainForm.TopoToatlDS.Tables["TopoManufactureConfigInit"],
                            "PID=" + PID + "and ID=" + myID);   //141112_0
                        if (result)
                        {
                            MainForm.ISNeedUpdateflag = true;
                            MessageBox.Show("Item: " + myDeleteItemName + " has been deleted successful!");
                            runMConfigInitMsgState((byte)MsgState.Delete);
                        }
                        else
                        {
                            MessageBox.Show("Item: " + myDeleteItemName + " delete Failed!");
                        }
                    }
                    this.CboMConfigInitStartAddr.BackColor = Color.White;
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
        
        private void dgvMConfigInit_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            getMConfigInitdgvRowsInfo();
        }

        private void dgvMConfigInit_CurrentCellChanged(object sender, EventArgs e)
        {
            getMConfigInitdgvRowsInfo();
        }
        private void MConfigInit_Load(object sender, EventArgs e)
        {
            try
            {
                ShowMyTip();
                clearMConfigInitInfo();
                MainForm.showTablefilterStrInfo(MainForm.TopoToatlDS.Tables["TopoManufactureConfigInit"], dgvMConfigInit, "PID=" + PID, "SlaveAddress,Page,StartAddress");


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

    }
}

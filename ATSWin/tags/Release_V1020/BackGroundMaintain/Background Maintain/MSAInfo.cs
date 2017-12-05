using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Maintain
{
    public partial class MSAInfo : Form
    {

        public string MSAName = "";
        public bool blnAddNew = false;

        bool blnAddPrmtr = false;
        string currMSAID, currPrmtrID;

        public MSAInfo()
        {
            InitializeComponent();
        }
        void ShowMyTip()
        {
            try
            {
                ToolTip mytip = new ToolTip();

                mytip.SetToolTip(btnAdd, "Add new MSA");
                mytip.SetToolTip(btnRemove, "Delete a MSA");
                mytip.SetToolTip(btnOK, "Save");

                mytip.SetToolTip(btnPrmtrOK, "Save");
                mytip.SetToolTip(btnPrmtrAdd, "Add new parameter");
                mytip.SetToolTip(btnPrmtrDelete, "Delete a parameter");

                mytip.SetToolTip(txtSaveResult, "Operate logs...");

                mytip.SetToolTip(cboType, "MSAName");
                mytip.SetToolTip(cboInterface, " communication mode: I2C or MDIO");
                mytip.SetToolTip(cboAddress, "Slave Addr <Decimal Number>");
                //mytip.SetToolTip(cboAddress, "从机地址 0x** ,16进制");

                mytip.SetToolTip(this.cboFieldName, "FieldName");
                mytip.SetToolTip(this.cboChannel, "Channel");
                mytip.SetToolTip(cboFormat, "ValueType: U16,IEEE754,etc...");
                mytip.SetToolTip(cboLength, "Length");
                mytip.SetToolTip(cboPage, "Page");
                mytip.SetToolTip(cboSlaveAddr, "Slave Addr <Decimal Number>");
                mytip.SetToolTip(cboStartAddr, "Start Addr  <Decimal Number>");
                //mytip.SetToolTip(cboSlaveAddr, "从机地址 0x** ,16进制");
                //mytip.SetToolTip(cboStartAddr, "起始地址 0x** ,16进制");
                mytip.ShowAlways = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void MSAInfo_Load(object sender, EventArgs e)
        {
            try
            {
                MainForm.ResfeshList(MainForm.GlobalDS.Tables["GlobalMSA"], "", currlst, "ItemName");
                ShowMyTip();
                initAddr();
                initInterface();
                clearPrmtrInfo();
                if (blnAddNew)
                {
                    this.currlst.Enabled = false;
                    currMSAID = (MainForm.mylastIDGlobalMSA + 1).ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void initAddr()
        {
            cboAddress.Items.Clear();
            this.cboAddress.Text = "";
            this.cboAddress.Items.Add("160");
            this.cboAddress.Items.Add("162");
            this.cboAddress.Items.Add("32768");
            //this.cboAddress.Items.Add("0xA0");
            //this.cboAddress.Items.Add("0xA2");
            //this.cboAddress.Items.Add("0x100");
        }

        void initInterface()
        {
            cboInterface.Items.Clear();
            this.cboInterface.Text = "";
            this.cboInterface.Items.Add("I2C");
            this.cboInterface.Items.Add("MIDO");
        }

        enum MsgState
        {
            SaveOK = 0,
            Edit = 1,
            AddNew = 2,
            Delete = 3,
            Clear = 4
        }
        void runPrmtrMsgState(byte state)
        {
            try
            {
                dgvPrmtr.Enabled = true;
                this.cboFieldName.Enabled = true;   //140917_0 
                if (state == (byte)MsgState.AddNew)
                {
                    this.txtSaveResult.Text = "Add a new parameter item...";
                    this.txtSaveResult.BackColor = Color.Yellow;
                    this.cboFieldName.BackColor = Color.Yellow;
                    this.cboFieldName.Enabled = true;
                    //dgvPrmtr.Enabled = false;
                    blnAddPrmtr = true; //140917_0 
                }
                else if (state == (byte)MsgState.Edit)
                {
                    this.txtSaveResult.Text = "Edit parameter item...";
                    this.txtSaveResult.BackColor = Color.GreenYellow;
                    this.cboFieldName.BackColor = Color.GreenYellow;
                    this.cboFieldName.Enabled = false;
                    blnAddPrmtr = false; //140917_0 
                }
                else if (state == (byte)MsgState.SaveOK)
                {
                    this.txtSaveResult.Text = "Parameter : " + this.cboFieldName.Text + " has been saved successful!";
                    this.txtSaveResult.BackColor = Color.YellowGreen;
                    this.cboFieldName.BackColor = Color.YellowGreen;
                    this.cboFieldName.Enabled = false;
                    blnAddPrmtr = false; //140917_0 
                }
                else if (state == (byte)MsgState.Delete)
                {
                    this.txtSaveResult.Text = "Parameter : " + this.cboFieldName.Text + " has been deleted successful!";
                    this.txtSaveResult.BackColor = Color.Pink;
                    this.cboFieldName.BackColor = Color.Pink;
                    this.cboFieldName.Enabled = false;
                    blnAddPrmtr = false; //140917_0 
                }
                else if (state == (byte)MsgState.Clear)
                {
                    this.txtSaveResult.Text = "";
                    this.txtSaveResult.BackColor = Color.White;
                    this.cboFieldName.BackColor = Color.White;
                    this.cboFieldName.Enabled = false;
                    blnAddPrmtr = false; //140917_0 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void runMSAMsgState(byte state)
        {
            try
            {
                this.cboType.Enabled = false;
                this.grpEquipPrmtr.Enabled = true;  //140917_0 
                this.grpPrmtr.Enabled = true;       //140917_0 
                if (state == (byte)MsgState.AddNew)
                {
                    this.txtSaveResult.Text = "Add a new MSA item...";
                    this.txtSaveResult.BackColor = Color.Yellow;
                    this.cboType.BackColor = Color.Yellow;
                    this.cboType.Enabled = true;
                    blnAddNew = true; //140917_0
                    dgvPrmtr.DataSource = null; //140917_0
                    this.grpEquipPrmtr.Enabled = false;  //140917_0 
                    this.grpPrmtr.Enabled = false;       //140917_0 
                    clearPrmtrInfo();    //140917_0
                }
                else if (state == (byte)MsgState.Edit)
                {
                    this.txtSaveResult.Text = "Edit MSA item...";
                    this.txtSaveResult.BackColor = Color.GreenYellow;
                    this.cboType.BackColor = Color.GreenYellow;
                    blnAddNew = false; //140917_0
                }
                else if (state == (byte)MsgState.SaveOK)
                {
                    this.txtSaveResult.Text = "MSA item " + this.cboType.Text + " has been saved successful!";
                    this.txtSaveResult.BackColor = Color.YellowGreen;
                    this.cboType.BackColor = Color.YellowGreen;
                    blnAddNew = false; //140917_0
                }
                else if (state == (byte)MsgState.Delete)
                {
                    this.txtSaveResult.Text = "MSA item " + this.cboType.Text + " has been deleted successful!";
                    this.txtSaveResult.BackColor = Color.Pink;
                    this.cboType.BackColor = Color.Pink;
                    blnAddNew = false; //140917_0
                }
                else if (state == (byte)MsgState.Clear)
                {
                    this.cboType.Text = "";
                    this.cboInterface.Text = "";
                    this.cboAddress.Text = "";
                    this.txtSaveResult.Text = "";
                    this.txtSaveResult.BackColor = Color.White;
                    this.cboType.BackColor = Color.White;
                    blnAddNew = false; //140917_0
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void getInfoFromDT(DataTable mydt, int currIndex)
        {
            try
            {
                string filterString = currlst.SelectedItem.ToString();
                DataRow[] myROWS = mydt.Select("ItemName='" + filterString + "'");
                if (myROWS.Length == 1)
                {
                    this.cboType.Text = myROWS[0]["ItemName"].ToString();
                    this.cboInterface.Text = myROWS[0]["AccessInterface"].ToString();
                    this.cboAddress.Text = myROWS[0]["SlaveAddress"].ToString();
                    //this.cboAddress.Text = "0x" + string.Format("{0:X2}", myROWS[0]["SlaveAddress"]).ToUpper();
                }
                else
                {
                    MessageBox.Show(" error! \n " + myROWS.Length + " records existed; \n filterString--> ItemName='" + filterString + "'");
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
                    this.cboFieldName.BackColor = Color.White;
                    this.txtSaveResult.BackColor = Color.White;
                    clearPrmtrInfo();

                    getInfoFromDT(MainForm.GlobalDS.Tables["GlobalMSA"], currlst.SelectedIndex);
                    currMSAID = MainForm.getDTColumnInfo(MainForm.GlobalDS.Tables["GlobalMSA"], "ID", "ItemName='" + currlst.SelectedItem.ToString() + "'");
                    MainForm.showTablefilterStrInfo(MainForm.GlobalDS.Tables["GlobalMSADefintionInf"], dgvPrmtr, "PID=" + currMSAID);
                    runMSAMsgState((byte)MsgState.Edit);
                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                currMSAID = (MainForm.mylastIDGlobalMSA + 1).ToString();
                blnAddNew = true;
                this.currlst.Enabled = false;
                this.cboType.Text = "";
                initInterface();
                initAddr();
                runMSAMsgState((byte)MsgState.AddNew);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (blnAddNew) //140917_0 新增误点新增取消新增状态!并直接返回
            {
                blnAddNew = false;
                runMSAMsgState((byte)MsgState.Clear);
                currlst.Enabled = true;
                currlst.SelectedItem = null;
                if (currlst.Items.Count > 0)
                {
                    currlst.SelectedIndex = 0;
                }
                return;
            } 

            if (this.currlst.SelectedIndex == -1)
            {
                MessageBox.Show("Pls choose a item first!");
            }
            else
            {
                try
                {
                    DialogResult drst = new DialogResult();
                    string myDeleteItemName = this.currlst.SelectedItem.ToString();
                    drst = (MessageBox.Show("Delete Item -->" + myDeleteItemName + "\n \n Pls choose 'Y' (是) to continue?",
                        "Notice", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2));

                    if (drst == DialogResult.Yes)
                    {
                        bool result = MainForm.DeleteItemForDT(MainForm.GlobalDS.Tables["GlobalMSA"], "ID=" + currMSAID + "and ItemName='" + myDeleteItemName + "'");
                        if (result)
                        {
                            MainForm.ISNeedUpdateflag = true;
                            MessageBox.Show("Item : " + myDeleteItemName + "  has been deleted successful!!");
                            MainForm.ResfeshList(MainForm.GlobalDS.Tables["GlobalMSA"], "", currlst, "ItemName");
                            runMSAMsgState((byte)MsgState.Delete);
                        }
                        else
                        {
                            MessageBox.Show("Item: " + myDeleteItemName + " delete Failed!");
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
        
        void getdgvRowsInfo()
        {
            try
            {
                currPrmtrID = dgvPrmtr.CurrentRow.Cells["ID"].Value.ToString();
                string myPID = dgvPrmtr.CurrentRow.Cells["PID"].Value.ToString();
                if (myPID.ToUpper() != currMSAID)
                {
                    MessageBox.Show("Parent PID Error! \n Current MSAID = " + myPID + " \n System MSAID=" + currMSAID);
                }
                this.cboFieldName.Text = dgvPrmtr.CurrentRow.Cells["FieldName"].Value.ToString();
                this.cboChannel.Text = dgvPrmtr.CurrentRow.Cells["Channel"].Value.ToString();
                this.cboSlaveAddr.Text = dgvPrmtr.CurrentRow.Cells["SlaveAddress"].Value.ToString();
                //this.cboSlaveAddr.Text = "0x" + string.Format("{0:X2}", dgvPrmtr.CurrentRow.Cells["SlaveAddress"].Value).ToUpper();
                this.cboPage.Text = dgvPrmtr.CurrentRow.Cells["Page"].Value.ToString();
                this.cboStartAddr.Text = dgvPrmtr.CurrentRow.Cells["StartAddress"].Value.ToString();
                //this.cboStartAddr.Text = "0x" + string.Format("{0:X2}", dgvPrmtr.CurrentRow.Cells["StartAddress"].Value).ToUpper();
                this.cboLength.Text = dgvPrmtr.CurrentRow.Cells["Length"].Value.ToString();
                this.cboFormat.Text = dgvPrmtr.CurrentRow.Cells["Format"].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void clearPrmtrInfo()
        {
            try
            {
                //string myID = dgvPrmtr.CurrentRow.Cells["ID"].Value.ToString();
                this.cboFieldName.Text = "";
                this.cboChannel.Text = "";
                this.cboSlaveAddr.Text = "";
                this.cboPage.Text = "";
                this.cboStartAddr.Text = "";
                this.cboLength.Text = "";
                this.cboFormat.Text = "";

                DataRow[] drs = MainForm.GlobalDS.Tables["GlobalMSADefintionInf"].Select();
                foreach (DataRow dr in drs)
                {
                    if (this.cboFieldName.Items.Contains(dr["FieldName"]) == false)    //140917_1 Fix Bug
                    {
                        cboFieldName.Items.Add(dr["FieldName"]);
                    }
                    if (this.cboSlaveAddr.Items.Contains(dr["SlaveAddress"]) == false)    //140917_1 Fix Bug
                    {
                        cboSlaveAddr.Items.Add(dr["SlaveAddress"]);
                    }
                    if (this.cboFormat.Items.Contains(dr["Format"]) == false)    //140917_1 Fix Bug
                    {
                        cboFormat.Items.Add(dr["Format"]);
                    }
                    //SlaveAddress
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void dgvPrmtr_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            getdgvRowsInfo();
            runPrmtrMsgState((byte)MsgState.Edit);
        }

        private void btnPrmtrOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (MainForm.checkItemLength("FieldName", this.cboFieldName.Text, 30)
                       || MainForm.checkItemLength("ValueType", this.cboFormat.Text, 10)
                       )
                {
                    return;
                }

                if (
                        this.cboFieldName.Text.ToString().Trim().Length == 0 ||
                        this.cboChannel.Text.ToString().Trim().Length == 0 ||
                        this.cboSlaveAddr.Text.ToString().Trim().Length == 0 ||
                        this.cboPage.ToString().Trim().Length == 0 ||
                        this.cboStartAddr.ToString().Trim().Length == 0 ||
                        this.cboLength.ToString().Trim().Length == 0 ||
                        this.cboFormat.ToString().Trim().Length == 0
                    )
                {
                    MessageBox.Show("Data is incomplete,Pls confirm again?");
                    return;
                }
                if (blnAddPrmtr)
                {
                    if (MainForm.currPrmtrCountExisted(MainForm.GlobalDS.Tables["GlobalMSADefintionInf"], "PID=" + currMSAID + " and FieldName ='" + this.cboFieldName.Text.ToString() + "' and Channel= " + this.cboChannel.Text) > 0)
                    {
                        //140704 约束 PID+FieldName+Channel
                        MessageBox.Show("The new data of ItemName has existed! <Violate unique rule>");
                        return;
                    }
                }
                bool result = EditInfoForDT(MainForm.GlobalDS.Tables["GlobalMSADefintionInf"]);

                if (result)
                {
                    MainForm.ISNeedUpdateflag = true;
                    //新增OK后需要置位!
                    if (blnAddPrmtr)
                    {
                        blnAddPrmtr = false;
                        this.cboFieldName.BackColor = Color.White;
                    }
                    runPrmtrMsgState((byte)MsgState.SaveOK);
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

        bool EditInfoForDT(DataTable mydt)
        {
            bool result = false;
            try
            {
                string filterString = "ID='" + currPrmtrID + "' AND PID=" + currMSAID;
                DataRow[] myROWS = mydt.Select(filterString);
                if (myROWS.Length == 1)
                {
                    if (this.blnAddPrmtr)
                    {
                        MessageBox.Show("Add new data error! \n " + myROWS.Length + " records existed; \n filterString-->  " + filterString);
                        return result;
                    }
                    myROWS[0].BeginEdit();
                    myROWS[0]["PID"] = this.currMSAID;
                    myROWS[0]["FieldName"] = this.cboFieldName.Text.ToString();
                    myROWS[0]["Channel"] = this.cboChannel.Text.ToString();
                    myROWS[0]["SlaveAddress"] =this.cboSlaveAddr.Text.ToString();  //140703_3
                    //myROWS[0]["SlaveAddress"] = Convert.ToInt64(this.cboSlaveAddr.Text.ToString(), 16);  //140703_3
                    myROWS[0]["Page"] = this.cboPage.Text.ToString();
                    myROWS[0]["StartAddress"] = this.cboStartAddr.Text.ToString();  //140703_3
                    //myROWS[0]["StartAddress"] = Convert.ToInt64(this.cboStartAddr.Text.ToString(), 16);  //140703_3
                    myROWS[0]["Length"] = this.cboLength.Text.ToString();
                    myROWS[0]["Format"] = this.cboFormat.Text.ToString();
                    myROWS[0].EndEdit();
                    result = true;
                }
                else if (this.blnAddPrmtr && myROWS.Length == 0)
                {
                    DataRow myNewRow = mydt.NewRow();
                    myNewRow.BeginEdit();
                    myNewRow["ID"] = currPrmtrID;
                    myNewRow["PID"] = this.currMSAID;
                    myNewRow["FieldName"] = this.cboFieldName.Text.ToString();
                    myNewRow["Channel"] = this.cboChannel.Text.ToString();
                    myNewRow["SlaveAddress"] = Convert.ToInt64(this.cboSlaveAddr.Text.ToString());//140922_0 delete,16 不再转为16进制     //140703_3
                    myNewRow["Page"] = this.cboPage.Text.ToString();
                    myNewRow["StartAddress"] = Convert.ToInt64(this.cboStartAddr.Text.ToString());//140922_0 delete,16 不再转为16进制     //140703_3
                    myNewRow["Length"] = this.cboLength.Text.ToString();
                    myNewRow["Format"] = this.cboFormat.Text.ToString();
                    mydt.Rows.Add(myNewRow);

                    myNewRow.EndEdit();
                    MainForm.mylastIDGlobalMSAPrmtr++;
                    MainForm.myAddCountMSAPrmtr++;             
                    result = true;
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

        private void btnPrmtrAdd_Click(object sender, EventArgs e)
        {
            try
            {
                clearPrmtrInfo();
                currPrmtrID = (MainForm.mylastIDGlobalMSAPrmtr + 1).ToString();
                //140917_0 blnAddPrmtr = true;
                runPrmtrMsgState((byte)MsgState.AddNew);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnPrmtrDelete_Click(object sender, EventArgs e)
        {
            if (blnAddPrmtr) //140917_0 新增误点新增取消新增状态!并直接返回
            {
                blnAddPrmtr = false;
                runPrmtrMsgState((byte)MsgState.Clear);
                return;
            } 


            if (dgvPrmtr.CurrentRow == null || dgvPrmtr.CurrentRow.Index == -1)
            {
                MessageBox.Show("Pls choose a item first!");
            }
            else
            {
                try
                {   
                    DialogResult drst = new DialogResult();
                    string myDeleteItemName = dgvPrmtr.CurrentRow.Cells["FieldName"].Value.ToString()
                       + "-->" + dgvPrmtr.CurrentRow.Cells["Channel"].Value.ToString();
                    string myID = dgvPrmtr.CurrentRow.Cells["ID"].Value.ToString();

                    drst = (MessageBox.Show("Delete Item -->" + myDeleteItemName + "\n \n Pls choose 'Y' (是) to continue?",
                        "Notice", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2));

                    if (drst == DialogResult.Yes)
                    {
                        bool result = MainForm.DeleteItemForDT(MainForm.GlobalDS.Tables["GlobalMSADefintionInf"]
                            , "PID=" + currMSAID + "and ID=" + myID );  //141111_1 依据ID删除
                        if (result)
                        {
                            MainForm.ISNeedUpdateflag = true;
                            MessageBox.Show("Item: " + myDeleteItemName + " has been deleted successful!");
                            runPrmtrMsgState((byte)MsgState.Delete);
                            clearPrmtrInfo();
                        }
                        else
                        {
                            MessageBox.Show("Item: " + myDeleteItemName + " delete Failed!");
                        }
                    }
                    this.cboFieldName.BackColor = Color.White;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (MainForm.checkItemLength("ItemName", this.cboType.Text, 25)
                    || MainForm.checkItemLength("AccessInterface", this.cboInterface.Text, 25)
                    )
                {
                    return;
                }
                if (
                        this.cboType.Text.ToString().Trim().Length == 0 ||
                        this.cboInterface.Text.ToString().Trim().Length == 0 ||
                        this.cboAddress.Text.ToString().Trim().Length == 0
                    )
                {
                    MessageBox.Show("Data is incomplete,Pls confirm again?");
                    return;
                }
                if (this.blnAddNew) //140922_0
                {
                    if (MainForm.currPrmtrCountExisted(MainForm.GlobalDS.Tables["GlobalMSA"], "ItemName ='" + this.cboType.Text.ToString() + "'") > 0)
                    {   //140704_1
                        MessageBox.Show("The new data of ItemName has existed! <Violate unique rule>");
                        return;
                    }
                }

                bool result = EditInfoForMSADT(MainForm.GlobalDS.Tables["GlobalMSA"]);
                MainForm.ResfeshList(MainForm.GlobalDS.Tables["GlobalMSA"], "", currlst, "ItemName");
                if (result)
                {
                    MainForm.ISNeedUpdateflag = true;
                    this.currlst.Enabled = true;
                    //新增OK后需要置位!
                    if (blnAddNew)
                    {
                        blnAddNew = false;
                        this.currlst.Enabled = true;

                    }
                    currlst.SelectedItem = this.cboType.Text.ToString();
                    runMSAMsgState((byte)MsgState.SaveOK);
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

        bool EditInfoForMSADT(DataTable mydt)
        {
            bool result = false;
            try
            {
                string filterString = "ID=" + currMSAID;
                DataRow[] myROWS = mydt.Select(filterString);
                if (myROWS.Length == 1)
                {
                    if (this.blnAddNew)
                    {
                        MessageBox.Show("Add new data error! \n " + myROWS.Length + " records existed; \n filterString-->  " + filterString);
                        return result;
                    }
                    myROWS[0].BeginEdit();
                    myROWS[0]["ItemName"] = this.cboType.Text.ToString();
                    myROWS[0]["AccessInterface"] = this.cboInterface.Text.ToString();
                    myROWS[0]["SlaveAddress"] = Convert.ToInt64(this.cboAddress.Text.ToString());//140922_0 delete,16 不再转为16进制 //140703_3 需要转换为 int
                    myROWS[0].EndEdit();
                    result = true;
                }
                else if (this.blnAddNew)
                {
                    DataRow myNewRow = mydt.NewRow();
                    myNewRow.BeginEdit();
                    myNewRow["ID"] = MainForm.mylastIDGlobalMSA + 1;    //currMSAID;
                    myNewRow["ItemName"] = this.cboType.Text.ToString();
                    myNewRow["AccessInterface"] = this.cboInterface.Text.ToString();
                    myNewRow["SlaveAddress"] = Convert.ToInt64(this.cboAddress.Text.ToString());//140922_0 delete,16 不再转为16进制 //140703_3 需要转换为 int

                    mydt.Rows.Add(myNewRow);

                    myNewRow.EndEdit();
                    MainForm.mylastIDGlobalMSA++;
                    MainForm.myAddCountMSA++;
                    result = true;
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

        private void btnPreviousPage_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}

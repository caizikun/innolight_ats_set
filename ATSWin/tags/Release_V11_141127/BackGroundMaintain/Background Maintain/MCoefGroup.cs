using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GlobalInfo
{
    public partial class MCoefGroup : Form
    {

        public string MCoefGroupName = "";
        public bool blnAddNew = false;

        bool blnAddPrmtr = false;
        string currMCoefGroupID, currPrmtrID;

        public MCoefGroup()
        {
            InitializeComponent();
        }
        void ShowMyTip()
        {
            try
            {
                ToolTip mytip = new ToolTip();

                mytip.SetToolTip(cboType, "GroupName");

                mytip.SetToolTip(this.cboItemType, "ItemType");
                mytip.SetToolTip(this.cboItemName, "ItemName");
                mytip.SetToolTip(cboFormat, "ValueType: Format U16,IEEE754,etc");
                mytip.SetToolTip(cboLength, "Length");
                mytip.SetToolTip(cboPage, "Page");
                mytip.SetToolTip(cboChannel, "ChannelNo");
                mytip.SetToolTip(cboStartAddr, "StartAddr <Decimal Number>");
                //mytip.SetToolTip(cboStartAddr, "起始地址 0x** ,16进制");
                mytip.ShowAlways = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void MCoefGroup_Load(object sender, EventArgs e)
        {
            try
            {
                MainForm.ResfeshList(MainForm.GlobalDS.Tables["GlobalManufactureCoefficientsGroup"], "", currlst, "ItemName");
                ShowMyTip();
                clearPrmtrInfo();
                if (blnAddNew)
                {
                    this.currlst.Enabled = false;
                    currMCoefGroupID = (MainForm.mylastIDGlobalMCoefGroup + 1).ToString();
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
        void runPrmtrMsgState(byte state)
        {
            try
            {
                dgvPrmtr.Enabled = true;
                this.cboItemName.Enabled = false;
                this.cboItemType.Enabled = false;
                if (state == (byte)MsgState.AddNew)
                {
                    this.txtSaveResult.Text = "Add a new parameter item...";
                    this.txtSaveResult.BackColor = Color.Yellow;
                    this.cboItemName.BackColor = Color.Yellow;
                    this.cboItemName.Enabled = true;
                    this.cboItemType.BackColor = Color.Yellow;
                    this.cboItemType.Enabled = true;    //140917_0
                    blnAddPrmtr = true; //140917_0  
                    //dgvPrmtr.Enabled = false;
                }
                else if (state == (byte)MsgState.Edit)
                {
                    this.txtSaveResult.Text = "Edit parameter item...";
                    this.txtSaveResult.BackColor = Color.GreenYellow;
                    this.cboItemName.BackColor = Color.GreenYellow;
                    this.cboItemType.BackColor = Color.GreenYellow;
                    blnAddPrmtr = false; //140917_0  
                    
                }
                else if (state == (byte)MsgState.SaveOK)
                {
                    this.txtSaveResult.Text = "Parameter : " + this.cboItemName.Text + " has been saved successful!";
                    this.txtSaveResult.BackColor = Color.YellowGreen;
                    this.cboItemName.BackColor = Color.YellowGreen;
                    this.cboItemType.BackColor = Color.YellowGreen;
                    blnAddPrmtr = false; //140917_0  
                }
                else if (state == (byte)MsgState.Delete)
                {
                    this.txtSaveResult.Text = "Parameter : " + this.cboItemName.Text + " has been deleted successful!";
                    this.txtSaveResult.BackColor = Color.Pink;
                    this.cboItemName.BackColor = Color.Pink;
                    this.cboItemType.BackColor = Color.Pink;
                    blnAddPrmtr = false; //140917_0  
                    clearPrmtrInfo();
                }
                else if (state == (byte)MsgState.Clear)
                {
                    this.txtSaveResult.Text = "";
                    this.txtSaveResult.BackColor = Color.White;
                    this.cboItemName.BackColor = Color.White;
                    this.cboItemType.BackColor = Color.White;
                    blnAddPrmtr = false; //140917_0  
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void runMCoefGroupMsgState(byte state)
        {
            try
            {
                this.currlst.Enabled = true;   //140917_0 
                this.cboType.Enabled = true;
                dgvPrmtr.Enabled = true;    //140917_0 
                this.grpEquipPrmtr.Enabled = true;  //140917_0 
                this.grpPrmtr.Enabled = true;       //140917_0 
                if (state == (byte)MsgState.AddNew)
                {
                    this.txtSaveResult.Text = "Add a new Group item...";
                    this.txtSaveResult.BackColor = Color.Yellow;
                    this.cboType.BackColor = Color.Yellow;
                    blnAddNew = true;   //140917_0 
                    this.currlst.Enabled = false;   //140917_0 
                    dgvPrmtr.Enabled = false;   //140917_0 
                    dgvPrmtr.DataSource = null; //140917_0 
                    this.grpEquipPrmtr.Enabled = false;  //140917_0 
                    this.grpPrmtr.Enabled = false;       //140917_0 
                    clearPrmtrInfo();    //140917_0
                }
                else if (state == (byte)MsgState.Edit)
                {
                    this.txtSaveResult.Text = "Edit Group item...";
                    this.txtSaveResult.BackColor = Color.GreenYellow;
                    this.cboType.BackColor = Color.GreenYellow;
                    blnAddNew = false;   //140917_0 
                    dgvPrmtr.Enabled = true;    //140917_0 
                }
                else if (state == (byte)MsgState.SaveOK)
                {
                    this.txtSaveResult.Text = "Group item " + this.cboType.Text + " has been saved successful!";
                    this.txtSaveResult.BackColor = Color.YellowGreen;
                    this.cboType.BackColor = Color.YellowGreen;
                    blnAddNew = false;   //140917_0 
                    dgvPrmtr.Enabled = true;    //140917_0 
                }
                else if (state == (byte)MsgState.Delete)
                {
                    this.txtSaveResult.Text = "Group item " + this.cboType.Text + " has been deleted successful!";
                    this.txtSaveResult.BackColor = Color.Pink;
                    this.cboType.BackColor = Color.Pink;
                    blnAddNew = false;   //140917_0 
                    dgvPrmtr.Enabled = true;    //140917_0 
                }
                else if (state == (byte)MsgState.Clear)
                {
                    this.cboType.Text = "";
                    this.txtSaveResult.Text = "";
                    this.txtSaveResult.BackColor = Color.White;
                    this.cboType.BackColor = Color.White;
                    blnAddNew = false;   //140917_0 
                    dgvPrmtr.Enabled = true;    //140917_0 
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
                    this.cboItemType.BackColor = Color.White;
                    this.txtSaveResult.BackColor = Color.White;
                    clearPrmtrInfo();

                    getInfoFromDT(MainForm.GlobalDS.Tables["GlobalManufactureCoefficientsGroup"], currlst.SelectedIndex);
                    currMCoefGroupID = MainForm.getDTColumnInfo(MainForm.GlobalDS.Tables["GlobalManufactureCoefficientsGroup"], "ID", "ItemName='" + currlst.SelectedItem.ToString() + "'");
                    MainForm.showTablefilterStrInfo(MainForm.GlobalDS.Tables["GlobalManufactureCoefficients"], dgvPrmtr, "PID=" + currMCoefGroupID);
                    runMCoefGroupMsgState((byte)MsgState.Edit);
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
                currMCoefGroupID = (MainForm.mylastIDGlobalMCoefGroup + 1).ToString();
                //140917_0 blnAddNew = true;
                //140917_0 this.currlst.Enabled = false;
                this.cboType.Text = "";
                runMCoefGroupMsgState((byte)MsgState.AddNew);
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
                runMCoefGroupMsgState((byte)MsgState.Clear);
                currlst.Enabled = true;
                currlst.SelectedItem = null;
                
                clearPrmtrInfo();   //140922_0
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
                        bool result = MainForm.DeleteItemForDT(MainForm.GlobalDS.Tables["GlobalManufactureCoefficientsGroup"], "ID=" + currMCoefGroupID + "and ItemName='" + myDeleteItemName + "'");
                        if (result)
                        {
                            MainForm.ISNeedUpdateflag = true;
                            MessageBox.Show("Item : " + myDeleteItemName + "  has been deleted successful!!");
                            MainForm.ResfeshList(MainForm.GlobalDS.Tables["GlobalManufactureCoefficientsGroup"], "", currlst, "ItemName");
                            runMCoefGroupMsgState((byte)MsgState.Delete);
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
                if (myPID.ToUpper() != currMCoefGroupID)
                {
                    MessageBox.Show("Parent PID Error! \n Current PID = " + myPID + " \n System PID=" + currMCoefGroupID);
                }
                this.cboItemType.Text = dgvPrmtr.CurrentRow.Cells["ItemTYPE"].Value.ToString();
                this.cboItemName.Text = dgvPrmtr.CurrentRow.Cells["ItemName"].Value.ToString();
                this.cboChannel.Text =  dgvPrmtr.CurrentRow.Cells["Channel"].Value.ToString();
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
                cboItemType.Items.Clear();
                cboFormat.Items.Clear();
                cboChannel.Items.Clear();

                this.cboItemType.Items.Clear();
                this.cboItemName.Items.Clear();
                this.cboChannel.Items.Clear();
                this.cboPage.Items.Clear();
                this.cboStartAddr.Items.Clear();
                this.cboLength.Items.Clear();
                this.cboFormat.Items.Clear();

                //cboItemType.Items.Add("Firmware");
                //cboItemType.Items.Add("ADC");
                //cboItemType.Items.Add("Coefficient");

                //cboFormat.Items.Add("U16");
                //cboFormat.Items.Add("IEEE754");

                DataRow[] drs = MainForm.GlobalDS.Tables["GlobalManufactureCoefficients"].Select("");

                for (int i = 0; i < drs.Length; i++)
                {
                    if (this.cboItemType.Items.Contains(drs[i]["ItemType"]) == false)
                    {
                        this.cboItemType.Items.Add(drs[i]["ItemType"]);
                    }
                    if (cboFormat.Items.Contains(drs[i]["Format"]) == false)
                    {
                        this.cboFormat.Items.Add(drs[i]["Format"]);
                    }

                    if (cboItemName.Items.Contains(drs[i]["ItemName"]) == false)
                    {
                        this.cboItemName.Items.Add(drs[i]["ItemName"]);
                    }
                    if (this.cboPage.Items.Contains(drs[i]["Page"]) == false)
                    {
                        this.cboPage.Items.Add(drs[i]["Page"]);
                    }
                    if (this.cboLength.Items.Contains(drs[i]["Length"]) == false)
                    {
                        this.cboLength.Items.Add(drs[i]["Length"]);
                    }
                    if (this.cboStartAddr.Items.Contains(drs[i]["StartAddress"]) == false)
                    {
                        this.cboStartAddr.Items.Add(drs[i]["StartAddress"]);
                    }     
                }

                cboChannel.Items.Add("0");
                cboChannel.Items.Add("1");
                cboChannel.Items.Add("2");
                cboChannel.Items.Add("3");
                cboChannel.Items.Add("4");

                this.cboItemType.Text = "";
                this.cboItemName.Text = "";
                this.cboChannel.Text = "";
                this.cboPage.Text = "";
                this.cboStartAddr.Text = "";
                this.cboLength.Text = "";
                this.cboFormat.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void dgvPrmtr_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                getdgvRowsInfo();
                runPrmtrMsgState((byte)MsgState.Edit);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnPrmtrOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (MainForm.checkItemLength("ItemType", cboItemType.Text, 30)
                    || MainForm.checkItemLength("ItemName", cboItemName.Text, 30)
                    || MainForm.checkItemLength("ValueType", cboFormat.Text, 25)
                    )
                {
                    return;
                }
                if (
                        this.cboItemType.Text.ToString().Trim().Length == 0 ||
                        this.cboItemName.Text.ToString().Trim().Length == 0 ||
                        this.cboChannel.Text.ToString().Trim().Length == 0 ||
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
                    if (MainForm.currPrmtrCountExisted(MainForm.GlobalDS.Tables["GlobalManufactureCoefficients"],
                        "PID= " + currMCoefGroupID + " and ItemName ='" + this.cboItemName.Text.ToString() + "' and Channel= " + this.cboChannel.Text) > 0)
                    {
                        //140704_1 约束PID+Name+Channel
                        MessageBox.Show("The new data of ItemName has existed! <Violate unique rule>");
                        return;
                    }
                }

                bool result = EditInfoForDT(MainForm.GlobalDS.Tables["GlobalManufactureCoefficients"]);

                if (result)
                {
                    MainForm.ISNeedUpdateflag = true;
                    //新增OK后需要置位!
                    if (blnAddPrmtr)
                    {
                        blnAddPrmtr = false;
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
                string filterString = "ID='" + currPrmtrID + "' AND PID=" + currMCoefGroupID;
                DataRow[] myROWS = mydt.Select(filterString);
                if (myROWS.Length == 1)
                {
                    if (this.blnAddPrmtr)
                    {
                        MessageBox.Show("Add new data error! \n " + myROWS.Length + " records existed; \n filterString-->  " + filterString);
                        return result;
                    }
                    myROWS[0].BeginEdit();
                    myROWS[0]["PID"] = this.currMCoefGroupID;
                    myROWS[0]["ItemTYPE"] = this.cboItemType.Text.ToString();
                    myROWS[0]["ItemName"] = this.cboItemName.Text.ToString();
                    myROWS[0]["Channel"] = this.cboChannel.Text.ToString();  //140703_3
                    myROWS[0]["Page"] = this.cboPage.Text.ToString();
                    myROWS[0]["StartAddress"] = this.cboStartAddr.Text.ToString();  //140703_3
                    //myROWS[0]["StartAddress"] = Convert.ToInt64(this.cboStartAddr.Text.ToString(), 16);  //140703_3
                    myROWS[0]["Length"] = this.cboLength.Text.ToString();
                    myROWS[0]["Format"] = this.cboFormat.Text.ToString();
                    myROWS[0].EndEdit();
                    result = true;
                }
                else if (this.blnAddPrmtr)
                {
                    DataRow myNewRow = mydt.NewRow();
                    myNewRow.BeginEdit();
                    myNewRow["ID"] = currPrmtrID;
                    myNewRow["PID"] = this.currMCoefGroupID;
                    myNewRow["ItemTYPE"] = this.cboItemType.Text.ToString();
                    myNewRow["ItemName"] = this.cboItemName.Text.ToString();
                    myNewRow["Channel"] = this.cboChannel.Text.ToString(); //140703_3
                    myNewRow["Page"] = this.cboPage.Text.ToString();
                    myNewRow["StartAddress"] = this.cboStartAddr.Text.ToString();   //140703_3
                    //myNewRow["StartAddress"] = Convert.ToInt64(this.cboStartAddr.Text.ToString(), 16);   //140703_3
                    myNewRow["Length"] = this.cboLength.Text.ToString();
                    myNewRow["Format"] = this.cboFormat.Text.ToString();
                    mydt.Rows.Add(myNewRow);

                    myNewRow.EndEdit();
                    MainForm.mylastIDGlobalMCoefPrmtr++;
                    MainForm.myAddCountMSAPrmtr++;             
                    result = true;
                    blnAddPrmtr = false;
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
                currPrmtrID = (MainForm.mylastIDGlobalMCoefPrmtr + 1).ToString();
                //140917_0  blnAddPrmtr = true;
                runPrmtrMsgState((byte)MsgState.AddNew);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnPrmtrDelete_Click(object sender, EventArgs e)
        {
            if (blnAddPrmtr)    //140917_0 新增误点新增取消新增状态!并直接返回
            {
                blnAddPrmtr = false;
                runPrmtrMsgState((byte)MsgState.Clear);
                return;
            } 

            if (dgvPrmtr.CurrentRow == null || dgvPrmtr.CurrentRow.Index == -1)   //140710/
            {
                MessageBox.Show("Pls choose a item first!");
            }
            else
            {
                try
                {   
                    DialogResult drst = new DialogResult();
                    string myDeleteItemName = dgvPrmtr.CurrentRow.Cells["ItemType"].Value.ToString()
                        + "-->" + dgvPrmtr.CurrentRow.Cells["ItemName"].Value.ToString()
                        + "-->" + dgvPrmtr.CurrentRow.Cells["Channel"].Value.ToString();
                    string myID = dgvPrmtr.CurrentRow.Cells["ID"].Value.ToString();
                   
                    drst = (MessageBox.Show("Delete Item -->" + myDeleteItemName + "\n \n Pls choose 'Y' (是) to continue?",
                        "Notice", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2));


                    if (drst == DialogResult.Yes)
                    {
                        bool result = MainForm.DeleteItemForDT(MainForm.GlobalDS.Tables["GlobalManufactureCoefficients"],
                            "PID=" + currMCoefGroupID + "and ID= " + myID); //141111_1 多个条件但ID是唯一
                        if (result)
                        {
                            MainForm.ISNeedUpdateflag = true;
                            MessageBox.Show("Item: " + myDeleteItemName + " has been deleted successful!");
                            runPrmtrMsgState((byte)MsgState.Delete);
                        }
                        else
                        {
                            MessageBox.Show("Item: " + myDeleteItemName + " delete Failed!");
                        }
                    }
                    this.cboItemType.BackColor = Color.White;
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
                if (MainForm.checkItemLength("ItemName", cboType.Text, 30))
                {
                    return;
                }
                if (
                        this.cboType.Text.ToString().Trim().Length == 0
                    )
                {
                    MessageBox.Show("Data is incomplete,Pls confirm again?");
                    return;
                }

                if (blnAddNew)  //140922_0 修正无法编辑BUG
                {
                    if (MainForm.currPrmtrCountExisted(MainForm.GlobalDS.Tables["GlobalManufactureCoefficientsGroup"], "ItemName ='" + this.cboType.Text.ToString() + "'") > 0)
                    {   //140704_1
                        MessageBox.Show("The new data of ItemName has existed! <Violate unique rule>");
                        return;
                    }
                }

                bool result = EditInfoForMSADT(MainForm.GlobalDS.Tables["GlobalManufactureCoefficientsGroup"]);
                MainForm.ResfeshList(MainForm.GlobalDS.Tables["GlobalManufactureCoefficientsGroup"], "", currlst, "ItemName");
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
                    runMCoefGroupMsgState((byte)MsgState.SaveOK);
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
                string filterString = "ID=" + currMCoefGroupID;
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
                    myROWS[0].EndEdit();
                    result = true;
                }
                else if (this.blnAddNew)
                {
                    DataRow myNewRow = mydt.NewRow();
                    myNewRow.BeginEdit();
                    myNewRow["ID"] = currMCoefGroupID;
                    myNewRow["ItemName"] = this.cboType.Text.ToString();
                    mydt.Rows.Add(myNewRow);

                    myNewRow.EndEdit();
                    MainForm.mylastIDGlobalMCoefGroup++;
                    MainForm.myAddCountMSA++;
                    result = true;
                    blnAddNew = false;
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

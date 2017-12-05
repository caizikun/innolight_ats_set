using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;

namespace Maintain
{
    public partial class TestPlanForm : Form
    {
        public TestPlanForm()
        {
            InitializeComponent();
        }
        public bool blnAddNew = false;
        public string  PName = "";
        public long PID = -1;
        string myTestPlanID="";
        string tempTestPlanName = "";

        private void TestPlanForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.blnAddNew) //140604_1 Add
            {
                blnAddNew = false;//141030_1
                return; //141030_1  不再删除未保存资料<万一字符串为已存在资料 可能误删>                
            }
        }
        
        private void TestPlan_Load(object sender, EventArgs e)
        {
            try
            {
                // Login.AccessFilePath
                if (MainForm.TopoToatlDS.Tables["TopoTestPlan"].Columns.Contains("IsChipInitialize"))
                {
                    lblIsChipInit.Visible = true;
                    chkIsChipInit.Visible = true;
                }
                else
                {
                    lblIsChipInit.Visible = false;
                    chkIsChipInit.Visible = false;
                }

                if (MainForm.TopoToatlDS.Tables["TopoTestPlan"].Columns.Contains("IgnoreBackupCoef"))
                {
                    lblIgnoreCoef.Visible = true;
                    chkIgnoreCoef.Visible = true;
                }
                else
                {
                    lblIgnoreCoef.Visible = false;
                    chkIgnoreCoef.Visible = false;
                }
                if (MainForm.TopoToatlDS.Tables["TopoTestPlan"].Columns.Contains("ItemDescription"))  //150411_0
                {
                    this.lblDesc.Visible = true;
                    this.txtDescription.Visible = true;                    
                    btnOK.Location = new Point(92, 319);
                }
                else
                {
                    this.lblDesc.Visible = false;
                    this.txtDescription.Visible = false;
                    btnOK.Location = new Point(92, 276);
                }
                RefreshList();
                ShowMyTip();
                btnOK.Visible = (MainForm.blnWritable ? true : false);
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
                mytip.SetToolTip(currlst, "TestPlan List");
                mytip.SetToolTip(cboItemName, "TestPlan Name[unique]");
                mytip.SetToolTip(cboSWVersion, "SWVersion");
                mytip.SetToolTip(cboHWVersion, "HWVersion");
                mytip.SetToolTip(cboUSBPort, "USBPortNo,default: 0");
                mytip.SetToolTip(cboAuxAttribles, "AuxAttribles Setting...");
                mytip.SetToolTip(chkIsChipInit, "Is Need Chip Initialize Setting?");
                mytip.SetToolTip(chkIgnoreCoef, "Is Need Ignore Backup Coef Addr value?true=Ignore Backup:false=Enable Backup");
                mytip.SetToolTip(chkIgnortPlan, "Is Need Ignore current TestPlan?true=Ignore TestPlan:false=Enable TestPlan");

                mytip.SetToolTip(lblItemName, "After double click '" + lblItemName.Text + " ' then you can change '" + lblItemName.Text + "'");
                
                mytip.ShowAlways = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        void RefreshList()
        {
            try
            {
                currlst.Refresh();
                currlst.Items.Clear();
                txtTestPlanName.Text = this.PName;
                cboItemName.Enabled = false;
                tempTestPlanName = "";

                //150202_0 -------------------
                this.cboHWVersion.Items.Clear();
                this.cboSWVersion.Items.Clear();                
                this.cboUSBPort.Items.Clear();
                this.cboAuxAttribles.Items.Clear();
                this.cboHWVersion.Items.Add("NA");
                this.cboSWVersion.Items.Add("NA");
                this.cboAuxAttribles.Items.Add("SN_CHECK = 0;"); //140606 Add
                this.cboUSBPort.Items.Add("0"); //140606 Add

                if (blnAddNew)
                {
                    chkIgnortPlan.Visible = false;  //150114_0
                    lblIgnortPlan.Visible = false;  //150114_0
                    myTestPlanID = (MainForm.mylastIDTestPlan + 1).ToString();
                    MainForm.myTestPlanISNewFlag = true;                    
                   
                    cboItemName.Enabled = true;
                    cboItemName.BackColor = Color.Yellow;   //140709_2
                    this.currlst.Enabled = false;
                    btnNextPage.Enabled = false;

                    this.cboHWVersion.Text = "NA";  //150202_0
                    this.cboSWVersion.Text = "NA";  //150202_0
                    
                }
                else
                {                    
                    this.currlst.Enabled = true;
                    btnNextPage.Enabled = false; //140604
                    cboItemName.BackColor = Color.White;    //140709_2                    
                }
                //140703_1>>>
                DataRow[] drs = MainForm.TopoToatlDS.Tables["TopoTestPlan"].Select("PID=" + PID);

                for (int i = 0; i < drs.Length; i++)
                    if (drs[i].RowState != DataRowState.Deleted)
                    {
                        currlst.Items.Add(drs[i]["ItemName"].ToString());
                    }
                //140703_1<<<
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (MainForm.checkItemLength("PlanName", cboItemName.Text, 30)
                       || MainForm.checkItemLength("SWVersion ", cboSWVersion.Text, 30)
                       || MainForm.checkItemLength("HWVersion ", cboHWVersion.Text, 30)
                       || MainForm.checkItemLength("AuxAttribles ", cboAuxAttribles.Text, 255))
                {
                    return;
                }
                if (MainForm.TopoToatlDS.Tables["TopoTestPlan"].Columns.Contains("ItemDescription"))  //150411_0
                {
                    if (MainForm.checkItemLength("ItemDescription", this.txtDescription.Text, 200))
                    {
                        return;
                    }
                }
                if( this.PID.ToString().Trim().Length ==0 || 
                this.cboItemName.Text.ToString().Trim().Length ==0 || 
                this.cboSWVersion.Text.ToString().Trim().Length ==0 || 
                this.cboHWVersion.Text.ToString().Trim().Length ==0 || 
                this.cboUSBPort.Text.ToString().Trim().Length ==0
                //150104_0 //|| this.cboAuxAttribles.Text.ToString().Trim().Length ==0 
                ) //140606 Add
                {
                    MessageBox.Show("Some data information is null!Please confirm!");
                    return;
                }

                bool result = EditInfoForDT(MainForm.TopoToatlDS.Tables[MainForm.ConstTestPlanTables[1]]);

                if (result)
                {
                    MainForm.ISNeedUpdateflag = true; //140603_2
                    btnNextPage.Enabled = true;
                    cboItemName.Enabled = false;
                    cboItemName.BackColor = Color.White;    //140709_2

                    if (MainForm.myTestPlanISNewFlag)
                    {
                        //150203_1 --------------------------------
                        //需要由用户进行下一步维护资料的选择!
                        //EquipmentForm myEquip = new EquipmentForm();
                        //myEquip.blnAddNew = true;
                        //MainForm.myTestEquipAddOKFlag = false;        
                        //MainForm.myTestEquipPrmtrAddOKFlag = false;   
                        //myEquip.TestPlanName = currlst.SelectedItem.ToString();
                        //string filterstring = "ItemName='" + myEquip.TestPlanName.Trim() + "' and PID=" + PID + "";
                        //myEquip.PID = MainForm.getNextTablePIDFromDT(MainForm.TopoToatlDS.Tables[MainForm.ConstTestPlanTables[1]], filterstring);

                        //myEquip.ShowDialog();   //show NextForm...
                        blnAddNew = false;
                        MainForm.myTestPlanAddOKFlag = true; //140530_0
                        //this.Close(); 
                        //-------------------------------------------
                    }
                    else
                    {
                        MainForm.myTestPlanAddOKFlag = true; //140530_0
                    }
                }
                else //140604_1
                {
                    btnPreviousPage.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnPreviousPage_Click(object sender, EventArgs e)
        {            
            this.Close();          //141104_0
        }

        private void btnNextPage_Click(object sender, EventArgs e)
        {
            try
            {
                if (currlst.SelectedIndex != -1)
                {                    
                    MainForm.myTestPlanAddOKFlag = true; //140529_1
                    EquipmentForm myEquip = new EquipmentForm();
                    myEquip.blnAddNew = blnAddNew;  //140706_1 
                    myEquip.TestPlanName = currlst.SelectedItem.ToString();
                    //string filterstring = "Select ID from " + MainForm.ConstTestPlanTables[1] + " where ItemName='" + myEquip.TestPlanName.Trim() + "' and PID='" + PID + "'";
                    string filterstring = "ItemName='" + myEquip.TestPlanName.Trim() + "' and PID=" + PID + "";
                    myEquip.PID = MainForm.getNextTablePIDFromDT(MainForm.TopoToatlDS.Tables[MainForm.ConstTestPlanTables[1]], filterstring);

                    myEquip.ShowDialog();   //show NextForm...
                }
                else
                {
                    MessageBox.Show("Pls choose a testplan item first!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        private void btnMConfigInit_Click(object sender, EventArgs e)
        {
            try
            {
                if (currlst.SelectedIndex != -1)
                {
                    MainForm.myTestPlanAddOKFlag = true; //140529_1
                    MConfigInit myMConfigInit = new MConfigInit();
                    myMConfigInit.blnAddMConfigInit = blnAddNew;  //140706_1 
                    myMConfigInit.TestPlanName = currlst.SelectedItem.ToString();
                    //string filterstring = "Select ID from " + MainForm.ConstTestPlanTables[1] + " where ItemName='" + myEquip.TestPlanName.Trim() + "' and PID='" + PID + "'";
                    string filterstring = "ItemName='" + myMConfigInit.TestPlanName.Trim() + "' and PID=" + PID + "";
                    myMConfigInit.PID = MainForm.getNextTablePIDFromDT(MainForm.TopoToatlDS.Tables[MainForm.ConstTestPlanTables[1]], filterstring);

                    myMConfigInit.ShowDialog();   //show NextForm...
                }
                else
                {
                    MessageBox.Show("Pls choose a testplan item first!");
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
                if (currlst.SelectedIndex != -1)    //140714_0
                {
                    getInfoFromDT(MainForm.TopoToatlDS.Tables[MainForm.ConstTestPlanTables[1]], currlst.SelectedIndex);
                    btnNextPage.Enabled = true; //140706_1 //可以直接选择进入设备和流程
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        void getInfoFromDT(DataTable mydt,int currIndex)
        {
            try
            {
                cboItemName.Enabled = false;
                string filterString = currlst.SelectedItem.ToString();
                tempTestPlanName = filterString;
                DataRow[] myROWS = mydt.Select("ItemName='" + filterString + "' and PID=" + PID + "");
                if (myROWS.Length == 1)
                {
                    myTestPlanID = myROWS[0]["ID"].ToString();
                    this.cboItemName.Text = myROWS[0]["ItemName"].ToString();
                    this.cboSWVersion.Text = myROWS[0]["SWVersion"].ToString();
                    this.cboHWVersion.Text = myROWS[0]["HWVersion"].ToString();
                    this.cboUSBPort.Text = myROWS[0]["USBPort"].ToString();
                    this.cboAuxAttribles.Text = myROWS[0]["AuxAttribles"].ToString();
                    if (mydt.Columns.Contains("IsChipInitialize"))  //150104_0
                    {
                        this.chkIsChipInit.Checked = Convert.ToBoolean(myROWS[0]["IsChipInitialize"].ToString());
                    }
                    if (mydt.Columns.Contains("IsEEPROMInitialize"))  //150104_0
                    {
                        this.chkMConfigInit.Checked = Convert.ToBoolean(myROWS[0]["IsEEPROMInitialize"].ToString());
                    }
                    if (mydt.Columns.Contains("IgnoreBackupCoef"))  //150114_0
                    {
                        this.chkIgnoreCoef.Checked = Convert.ToBoolean(myROWS[0]["IgnoreBackupCoef"].ToString());
                    }

                    if (mydt.Columns.Contains("ItemDescription"))  //150411_0
                    {
                        this.txtDescription.Text = myROWS[0]["ItemDescription"].ToString();                        
                    }

                    //150114_0
                    this.chkIgnortPlan.Checked = Convert.ToBoolean(myROWS[0]["IgnoreFlag"].ToString());
                    lblIgnortPlan.Visible = this.chkIgnortPlan.Checked;
                    chkIgnortPlan.Visible = this.chkIgnortPlan.Checked;
                    
                }
                else
                {
                    MessageBox.Show("Error... There were" + myROWS.Length + (" records existed; \n filterString-->ItemName='" + filterString + "' and PID=" + PID + ""));
                    return;
                }
            }
            catch (Exception ex) 
            { 
                MessageBox.Show(ex.ToString ());
                return;
            }
        }

        bool  EditInfoForDT(DataTable mydt)
        {
            bool result = false;
            try
            {
                string filterString = "ID=" + myTestPlanID; // "ItemName='" + this.cboItemName.Text.ToString() + "' and PID=" + PID + "";
                //DataRow[] myROWS = mydt.Select(filterString); //140709_1
                DataRow[] myROWS = mydt.Select("ID=" + myTestPlanID); //因为是修改TestPlan名称,所以必须用ID
                
                if (myROWS.Length == 1)
                {
                    if (this.blnAddNew)
                    {
                        MessageBox.Show("Add a new record error!Pls confirm the ItemName!!! \n--> filterString=" + filterString);
                        return result;
                    }                  
                    myROWS[0].BeginEdit();
                    myROWS[0]["PID"] = this.PID.ToString();
                    myROWS[0]["ItemName"] = this.cboItemName.Text.ToString();
                    myROWS[0]["SWVersion"] = this.cboSWVersion.Text.ToString();
                    myROWS[0]["HWVersion"] = this.cboHWVersion.Text.ToString();
                    myROWS[0]["USBPort"] = this.cboUSBPort.Text.ToString();
                    myROWS[0]["AuxAttribles"] = this.cboAuxAttribles.Text.ToString();
                    myROWS[0]["IgnoreFlag"] = this.chkIgnortPlan.Checked.ToString();   //150104_0
                    if (mydt.Columns.Contains("IsChipInitialize"))
                    {
                        myROWS[0]["IsChipInitialize"] = this.chkIsChipInit.Checked.ToString();    //150104_0
                    }
                    if (mydt.Columns.Contains("IsEEPROMInitialize"))
                    {
                        myROWS[0]["IsEEPROMInitialize"] = this.chkMConfigInit.Checked.ToString();    //150104_0
                    }
                    if (mydt.Columns.Contains("IgnoreBackupCoef"))
                    {
                        myROWS[0]["IgnoreBackupCoef"] = (this.chkIgnoreCoef.Checked).ToString();   //150114_0
                    }
                    if (mydt.Columns.Contains("ItemDescription"))  //150411_0
                    {
                        myROWS[0]["ItemDescription"] = this.txtDescription.Text;
                    }
                    myROWS[0].EndEdit();
                    RefreshList();
                    result=true;
                    
                }
                else if (this.blnAddNew && myROWS.Length == 0)  //141106_0
                {
                    DataRow myNewRow = mydt.NewRow();
                    myNewRow.BeginEdit();
                    myNewRow["ID"] = MainForm.mylastIDTestPlan+1;
                    myNewRow["PID"] = this.PID.ToString();
                    myNewRow["ItemName"] = this.cboItemName.Text.ToString();
                    myNewRow["SWVersion"] = this.cboSWVersion.Text.ToString();
                    myNewRow["HWVersion"] = this.cboHWVersion.Text.ToString();
                    myNewRow["USBPort"] = this.cboUSBPort.Text.ToString();
                    myNewRow["AuxAttribles"] = this.cboAuxAttribles.Text.ToString();
                    myNewRow["IgnoreFlag"] = "false";  //141104_0
                    if (mydt.Columns.Contains("IsChipInitialize"))
                    {
                        myNewRow["IsChipInitialize"] = this.chkIsChipInit.Checked.ToString();    //150104_0
                    }
                    if (mydt.Columns.Contains("IsEEPROMInitialize"))
                    {
                        myNewRow["IsEEPROMInitialize"] = this.chkMConfigInit.Checked.ToString();    //150104_0
                    }
                    if (mydt.Columns.Contains("IgnoreBackupCoef"))
                    {
                        myNewRow["IgnoreBackupCoef"] = (this.chkIgnoreCoef.Checked).ToString();   //150114_0
                    }
                    if (mydt.Columns.Contains("ItemDescription"))  //150411_0
                    {
                        myNewRow["ItemDescription"] = this.txtDescription.Text;
                    }
                    mydt.Rows.Add(myNewRow);

                    myNewRow.EndEdit();
                    MainForm.mylastIDTestPlan++;
                    MainForm.myAddCountTestPlan ++;
                    RefreshList();
                    int myNewRowIndex= (currlst.Items.Count - 1);
                    
                    currlst.Enabled = true;
                    currlst.Focus();
                    currlst.SelectedIndex = myNewRowIndex;
                    
                   
                    blnAddNew = false;  //新增一条记录后将新增标志改为false;    140530_1                
                    result = true;
                }
                else
                {
                    MessageBox.Show("Error!Pls confirm the ItemName!!! \n--> filterString=" + filterString);
                }
                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return result;
            }
        }

        private void cboUSBPort_Leave(object sender, EventArgs e)
        {
            try
            {
                if (MainForm.ISNotInSpec("USBPortNo", cboUSBPort.Text.ToString(), 0, 10))
                {
                    cboUSBPort.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void lblItemName_DoubleClick(object sender, EventArgs e)
        {
            cboItemName.Enabled = true;
        }

        private void cboItemName_Leave(object sender, EventArgs e)
        {
            try
            {
                if (cboItemName.Enabled && cboItemName.Text.ToString() != tempTestPlanName)
                {
                    //先判定当前资料是否有空资料!
                    if (NotFoundName(cboItemName.Text.ToString()) == false)
                    {
                        MessageBox.Show("The new testplan name is not unique!");
                        if (this.cboItemName.Enabled)
                        {
                            this.cboItemName.Focus();
                            this.cboItemName.Text = ""; //140709_1 //重复则清空名称
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        bool NotFoundName(string NewName)
        {
            bool result = false;
            try
            {

                for (int i = 0; i < currlst.Items.Count; i++)
                {
                    if (NewName.ToUpper().Trim() == currlst.Items[i].ToString().ToUpper().Trim())
                    {
                        MessageBox.Show("The testplan name is already  existed! " + NewName);

                        return result;
                    }
                }
                result = true;

                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return result;
            }
        }

        //void showInitFlag(bool state)
        //{
        //    if (state)
        //    {
        //        lblIsChipInit.Visible = true;
        //        chkIsChipInit.Visible = true;
        //        lblIsChipInit.Location = new Point(6, 183);
        //        chkIsChipInit.Location = new Point(83, 183);

        //        label7.Location = new Point(6, 208);
        //        cboAuxAttribles.Location = new Point(79, 205);
        //    }
        //    else
        //    {
        //        lblIsChipInit.Visible = false;
        //        chkIsChipInit.Visible = false;

        //        label7.Location = new Point(6, 191);
        //        cboAuxAttribles.Location = new Point(79, 188);
        //    }
        //}

        private void chkIsChipInit_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIsChipInit.Checked)
            {
                chkIsChipInit.Text = "<ChipInit is Enabled>";
            }
            else
            {
                chkIsChipInit.Text = "<ChipInit is Disabled>";
            }
        }

        private void chkIgnoreCoef_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIgnoreCoef.Checked)
            {
                chkIgnoreCoef.Text = "<BackupCoef is Disabled>";
            }
            else
            {
                chkIgnoreCoef.Text = "<BackupCoef is Enabled>";
            }
        }

        private void chkIgnortPlan_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIgnortPlan.Checked)
            {
                chkIgnortPlan.Text = "<CurrItem is Disabled>";
            }
            else
            {
                chkIgnortPlan.Text = "<CurrItem is Enabled>";
            }
        }

        private void chkMConfigInit_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMConfigInit.Checked)
            {
                chkMConfigInit.Text = "<MConfigInit is Enabled>";
            }
            else
            {
                chkMConfigInit.Text = "<MConfigInit is Disabled>";
            }
        }


    }
}

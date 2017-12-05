﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;

namespace TestPlan
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
                RefreshList();
                ShowMyTip();
                btnOK.Visible = (PNInfo.blnWritable ? true : false);
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

                if (blnAddNew)
                {
                    myTestPlanID = (PNInfo.mylastIDTestPlan + 1).ToString();
                    PNInfo.myTestPlanISNewFlag = true;
                    this.cboAuxAttribles.Items.Add("SN_CHECK = 0;"); //140606 Add
                    this.cboUSBPort.Items.Add("0"); //140606 Add
                    cboItemName.Enabled = true;
                    cboItemName.BackColor = Color.Yellow;   //140709_2
                    this.currlst.Enabled = false;
                    btnNextPage.Enabled = false;
                }
                else
                {                    
                    this.currlst.Enabled = true;
                    btnNextPage.Enabled = false; //140604
                    cboItemName.BackColor = Color.White;    //140709_2
                    this.cboAuxAttribles.Items.Add("SN_CHECK = 0;"); //140606 Add
                    this.cboUSBPort.Items.Add("0"); //140606 Add
                }
                //140703_1>>>
                DataRow[] drs = PNInfo.TopoToatlDS.Tables["TopoTestPlan"].Select("PID=" + PID);

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
                if (PNInfo.checkItemLength("PlanName", cboItemName.Text, 30)
                       || PNInfo.checkItemLength("SWVersion ", cboSWVersion.Text, 30)
                       || PNInfo.checkItemLength("HWVersion ", cboHWVersion.Text, 30)
                       || PNInfo.checkItemLength("AuxAttribles ", cboAuxAttribles.Text, 255))
                {
                    return;
                }
                if( this.PID.ToString().Trim().Length ==0 || 
                this.cboItemName.Text.ToString().Trim().Length ==0 || 
                this.cboSWVersion.Text.ToString().Trim().Length ==0 || 
                this.cboHWVersion.Text.ToString().Trim().Length ==0 || 
                this.cboUSBPort.Text.ToString().Trim().Length ==0 || 
                this.cboAuxAttribles.Text.ToString().Trim().Length ==0 
                ) //140606 Add
                {
                    MessageBox.Show("Some data information is null!Please confirm!");
                    return;
                }

                bool result = EditInfoForDT(PNInfo.TopoToatlDS.Tables[PNInfo.ConstTestPlanTables[1]]);

                if (result)
                {
                    PNInfo.ISNeedUpdateflag = true; //140603_2
                    btnNextPage.Enabled = true;
                    cboItemName.Enabled = false;
                    cboItemName.BackColor = Color.White;    //140709_2

                    if (PNInfo.myTestPlanISNewFlag)
                    {
                        EquipmentForm myEquip = new EquipmentForm();
                        myEquip.blnAddNew = true;
                        PNInfo.myTestEquipAddOKFlag = false;        
                        PNInfo.myTestEquipPrmtrAddOKFlag = false;   
                        myEquip.TestPlanName = currlst.SelectedItem.ToString();
                        string filterstring = "ItemName='" + myEquip.TestPlanName.Trim() + "' and PID=" + PID + "";
                        myEquip.PID = PNInfo.getNextTablePIDFromDT(PNInfo.TopoToatlDS.Tables[PNInfo.ConstTestPlanTables[1]], filterstring);

                        myEquip.ShowDialog();   //show NextForm...
                        blnAddNew = false;
                        PNInfo.myTestPlanAddOKFlag = true; //140530_0
                        this.Close(); 
                    }
                    else
                    {
                        PNInfo.myTestPlanAddOKFlag = true; //140530_0
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
                    PNInfo.myTestPlanAddOKFlag = true; //140529_1
                    EquipmentForm myEquip = new EquipmentForm();
                    myEquip.blnAddNew = blnAddNew;  //140706_1 
                    myEquip.TestPlanName = currlst.SelectedItem.ToString();
                    //string filterstring = "Select ID from " + PNInfo.ConstTestPlanTables[1] + " where ItemName='" + myEquip.TestPlanName.Trim() + "' and PID='" + PID + "'";
                    string filterstring = "ItemName='" + myEquip.TestPlanName.Trim() + "' and PID=" + PID + "";
                    myEquip.PID = PNInfo.getNextTablePIDFromDT(PNInfo.TopoToatlDS.Tables[PNInfo.ConstTestPlanTables[1]], filterstring);

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

        private void currlst_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (currlst.SelectedIndex != -1)    //140714_0
                {
                    getInfoFromDT(PNInfo.TopoToatlDS.Tables[PNInfo.ConstTestPlanTables[1]], currlst.SelectedIndex);
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
                string filterString = currlst.SelectedItem.ToString();
                DataRow[] myROWS = mydt.Select("ItemName='" + filterString + "' and PID=" + PID + "");
                if (myROWS.Length == 1)
                {
                    myTestPlanID = myROWS[0]["ID"].ToString();
                    this.cboItemName.Text = myROWS[0]["ItemName"].ToString();
                    this.cboSWVersion.Text = myROWS[0]["SWVersion"].ToString();
                    this.cboHWVersion.Text = myROWS[0]["HWVersion"].ToString();
                    this.cboUSBPort.Text = myROWS[0]["USBPort"].ToString();
                    this.cboAuxAttribles.Text = myROWS[0]["AuxAttribles"].ToString();
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
                    myROWS[0]["IgnoreFlag"] = "false";  //141104_0
                    myROWS[0].EndEdit();
                    RefreshList();
                    result=true;
                    
                }
                else if (this.blnAddNew && myROWS.Length == 0)  //141106_0
                {
                    DataRow myNewRow = mydt.NewRow();
                    myNewRow.BeginEdit();
                    myNewRow["ID"] = PNInfo.mylastIDTestPlan+1;
                    myNewRow["PID"] = this.PID.ToString();
                    myNewRow["ItemName"] = this.cboItemName.Text.ToString();
                    myNewRow["SWVersion"] = this.cboSWVersion.Text.ToString();
                    myNewRow["HWVersion"] = this.cboHWVersion.Text.ToString();
                    myNewRow["USBPort"] = this.cboUSBPort.Text.ToString();
                    myNewRow["AuxAttribles"] = this.cboAuxAttribles.Text.ToString();
                    myNewRow["IgnoreFlag"] = "false";  //141104_0
                    mydt.Rows.Add(myNewRow);

                    myNewRow.EndEdit();
                    PNInfo.mylastIDTestPlan++;
                    PNInfo.myAddCountTestPlan ++;
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
                if (PNInfo.ISNotInSpec("USBPortNo", cboUSBPort.Text.ToString(), 0, 10))
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
                //先判定当前资料是否有空资料!
                if (blnAddNew && NotFoundName(cboItemName.Text.ToString()) == false)
                {
                    MessageBox.Show("The new testplan name is not unique!");
                    if (this.cboItemName.Enabled)
                    {
                        this.cboItemName.Focus();
                        this.cboItemName.Text = ""; //140709_1 //重复则清空名称
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
        
    }
}

﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace TestPlan
{
    public partial class EquipmentForm : Form
    {
        public bool blnAddNew = false;
        public bool blnAddNewEquip = false;
        public string TestPlanName = "";
        public long PID = -1;
        public long myPrmtrPID = -1;
        public long myGlobalEquipID = -1;
        public string myEquipName = "";
        public string myQueryEquipName = "";

        long origmylastIDTestEquip=PNInfo.mylastIDTestEquip;
        long origmynewIDTestEquip = PNInfo.mynewIDTestEquip;
        long origmyDeletedCountTestEquip = PNInfo.myDeletedCountTestEquip;

        long origmylastIDEquipPrmtr = PNInfo.mylastIDTestEquipPrmtr;
        long origmynewIDEquipPrmtr = PNInfo.mynewIDTestEquipPrmtr;
        long origmyDeletedCountEquipPrmtr = PNInfo.myDeletedCountTestEquipPrmtr;


        string currGlobalItemName = "";
        string currGlobalItemType = "";
        int myNewEquipcount = -1;
        int mylastIndex=-1;
        int currSeq = -1;
        int mylastDGVRowIndex=-1, mylastDGVColumnIndex=-1;  //140714_1
        bool blnUnloadFormflag=false;
        bool AddErr = false;
        //ConstGlobalListTables = new string[] { "GlobalAllEquipmentList", "GlobalAllEquipmentParamterList", "GlobalAllAppModelList", "GlobalAllTestModelList", "GlobalTestModelParamterList" };

        public EquipmentForm()
        {
            InitializeComponent();
        }

        private void EquipmentForm_Load(object sender, EventArgs e)
        {
            try
            {
                mylastIndex = -1;   //140603_2
                RefreshEquipInfo();
                ShowMyTip();
                
                //140626_1 Add权限控制
                btnAdd.Visible = (PNInfo.blnAddable ? true : false);                
                btnEditPrmtrOK.Visible = (PNInfo.blnWritable ? true : false);
                btnRemove.Visible = (PNInfo.blnDeletable ? true : false);
                
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

                mytip.SetToolTip(btnAdd, "Add a new equip which is choosed from Global Equipment List");
                mytip.SetToolTip(btnRemove, "Remove a equip from Current Equipment List");
                
                mytip.SetToolTip(btnEditPrmtrOK, "save all Equipment parameters");
                mytip.SetToolTip(btnNextPage, "Next goto TestControl settings");
                mytip.SetToolTip(btnPreviousPage, "Return PreviousPage");
                mytip.SetToolTip(txtDescription, "Operator states...(Read Only)");
                mytip.SetToolTip(cboFunc, "Modify the role of Equipment setting...");
                mytip.SetToolTip(btnUp, "Set a equipment SEQ Up");
                mytip.SetToolTip(btnDown, "Set a equipment SEQ Down");
                mytip.ShowAlways = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        void RefreshEquipInfo()
        {
            try
            {
                //initinal GlobalEquioList ~ Current List
                currlst.Items.Clear();
                globalistName.Items.Clear();
                cboItemName.Text = "";
                cboItemType.Text="";
                txtSaveResult.Text = "";
                txtTestPlanName.Text = "";
                txtDescription.Text = "";
                lblPrmtrType.Text = "PrmtrType:";
                dgvEquipPrmtr.DataSource = null;
                txtDGVItem.Visible = false;

                DataRow[] GlobalEquipLst = PNInfo.GlobalTotalDS.Tables[0].Select();
                // = PNInfo.mySqlIO.GetDataTable("", "");
                foreach (DataRow dr in GlobalEquipLst)
                {
                    globalistName.Items.Add(dr["ItemType"] + ":" + dr["ItemName"]);
                }

                DataRow[] CurrEquipLst = PNInfo.TopoToatlDS.Tables["TopoEquipment"].Select("PID=" + PID , "Seq,ID ASC");
                // = PNInfo.mySqlIO.GetDataTable("", "");
                int m = 1;
                foreach (DataRow dr in CurrEquipLst)
                {
                    currlst.Items.Add(dr["ItemName"]);
                    dr["seq"] = m++;
                }
                myNewEquipcount = CurrEquipLst.Length;
                txtTestPlanName.Text = TestPlanName;
                if (mylastIndex != -1)  //140530_2
                {
                    currlst.SelectedIndex = mylastIndex;
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
                if (this.globalistName.SelectedIndex != -1)
                {
                    //当点击Add 按钮后讲globalistName选中的项目新增到currlst[先移除冒号!]
                    int myIndex = cboFunc.SelectedIndex;
                    string tempString = globalistName.SelectedItem.ToString();
                    int findChar = tempString.LastIndexOf(":");
                    currGlobalItemName = tempString.Substring(findChar + 1, tempString.Length - (findChar + 1)).ToString();
                    currGlobalItemType = tempString.Substring(0, findChar).ToString();

                    //141029_2string NewName = currGlobalItemName +"_" + cboFunc.Text + "_" + currGlobalItemType;
                    myEquipName = currGlobalItemName;
                    //141030_0
                    //int myCount = PNInfo.currPrmtrCountExisted(PNInfo.TopoToatlDS.Tables["TopoEquipment"], "ItemName='" + myEquipName + "' and PID=" + PID + " and Role =" + myIndex);
                    //if (myCount > 0)
                    //{
                    //    MessageBox.Show("已经存在该设备名称记录...,请确认!\n 名称=" + myEquipName + " 且 角色="+ cboFunc.Text);
                    //    return;
                    //}

                    enableAddNew(false);

                    blnAddNewEquip = true;
                    PNInfo.myTestEquipAddOKFlag = false;        //140529_1
                    PNInfo.myTestEquipPrmtrAddOKFlag = false;   //140529_1

                    myNewEquipcount++;
                    currlst.Items.Add(myEquipName);
                    this.cboItemName.Text = myEquipName;
                    this.cboItemType.Text = currGlobalItemType;

                    if (myEquipName.Contains("_"))
                    {
                        int lastChar = myEquipName.IndexOf("_");
                        myQueryEquipName = myEquipName.Substring(0, lastChar);
                    }
                    else
                    {
                        myQueryEquipName = myEquipName;
                    } 
                    
                    int myNewRowIndex = (currlst.Items.Count - 1);
                    currlst.SelectedIndex = myNewRowIndex;

                    btnAdd.Enabled = false;
                    currlst.Enabled = false;
                    grpEquipPrmtr.Enabled = false;
                    btnNextPage.Enabled = false;

                    //AddOrEdit myEquipTable
                    bool EditEquipResult = EditInfoForDT(PNInfo.TopoToatlDS.Tables["TopoEquipment"], (PNInfo.mylastIDTestEquip + 1).ToString());
                     

                    if (EditEquipResult)
                    {
                        if (blnAddNewEquip == true)
                        {
                            grpEquipPrmtr.Enabled = true; //新增仪器参数不允许编辑
                            this.btnEditPrmtrOK.Enabled = true;
                        }
                        else
                        {
                            grpEquipPrmtr.Enabled = false; //新增仪器参数不允许编辑
                            this.btnEditPrmtrOK.Enabled = false;
                        }
                    }


                }
                else
                {
                    MessageBox.Show("Please select a new instruments first and then click the 'Add' button!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        enum EquipRole
        {
            NA=0,TX=1,RX=2
        }
        bool getInfoFromDT(DataTable mydt, int currIndex)
        {
            try
            {
                string filterString = "ItemName='" + currlst.SelectedItem.ToString() + "' and PID=" + PID + " and Seq=" + currSeq;  // +" and   Seq=" + currSeq;//currlst.SelectedItem.ToString();
                DataRow[] myROWS = mydt.Select(filterString);
                if (myROWS.Length == 1)
                {                    
                    this.cboItemType.Text = myROWS[0]["ItemType"].ToString();
                    this.cboItemName.Text = myROWS[0]["ItemName"].ToString();
                    cboFunc.SelectedIndex  = System.Convert.ToInt32(myROWS[0]["Role"]);
                    myPrmtrPID = System.Convert.ToInt64(myROWS[0]["ID"]);
                    return true;
                }
                else
                {
                    MessageBox.Show("Error!" + myROWS.Length + (" records existed; \n filterString--> ItemName='" + filterString + "' and PID=" + PID + " and   Seq=" + currSeq));
                    return false;
                }
            }
            catch (Exception ex) 
            { 
                MessageBox.Show(ex.ToString ());
                return false;
            }
        }

        bool EditInfoForDT(DataTable mydt,string ID)
        {
            bool resultValue = false;
            try
            {
                string filterString = "ID=" + ID; //141030_0//= "ItemName='" + this.cboItemName.Text.ToString() + "' and PID=" + PID + "" ;
                DataRow[] myROWS = mydt.Select(filterString);
                if (myROWS.Length == 1)
                {
                    if (this.blnAddNewEquip)
                    {
                        MessageBox.Show("Add New Error!" + myROWS.Length + (" records existed; \n filterString--> " + filterString));
                        return resultValue;
                    }                    
                    //myROWS[0]["PID"] = this.PID.ToString();
                    myROWS[0]["ItemType"] = this.cboItemType.Text.ToString();
                    myROWS[0]["ItemName"] = this.cboItemName.Text.ToString();
                    myROWS[0]["Role"] = this.cboFunc.SelectedIndex;
                    resultValue = true;
                }
                else if (this.blnAddNewEquip)
                {
                    DataRow myNewRow = mydt.NewRow();
                    myNewRow.BeginEdit();
                    myNewRow["ID"] = ID;// PNInfo.mylastIDTestEquip + 1;
                    myNewRow["PID"] = this.PID.ToString();                    
                    myNewRow["ItemType"] = this.cboItemType.Text.ToString();
                    myNewRow["ItemName"] = this.cboItemName.Text.ToString();
                    myNewRow["SEQ"] = this.currlst.Items.Count;
                    myNewRow["Role"] = this.cboFunc.SelectedIndex;
                    mydt.Rows.Add(myNewRow);

                    myNewRow.EndEdit();

                    //if (myNewEquipcount > PNInfo.mylastIDTestEquip) //当执行一次添加后并未点击OK更新资料!则不进行PNInfo的ID++
                    //{
                        PNInfo.mylastIDTestEquip++;
                        PNInfo.myAddCountTestEquip++;
                    //}
                    
                    //RefreshList();
                    int myNewRowIndex = (currlst.Items.Count - 1);
                    
                    currlst.SelectedIndex = myNewRowIndex;
                    //140709_3
                    if (dgvEquipPrmtr.Columns["Item"].Width + dgvEquipPrmtr.Columns["ItemValue"].Width < dgvEquipPrmtr.Size.Width)
                    {
                        dgvEquipPrmtr.Columns["ItemValue"].Width = dgvEquipPrmtr.Size.Width - dgvEquipPrmtr.Columns["Item"].Width;
                    }

                    //blnAddNewEquip = false;  //新增一条记录后将新增标志改为false; 
                    resultValue = true;
                }
                else
                {
                    MessageBox.Show(" Error!" + myROWS.Length + (" records existed; \n filterString--> " + filterString));
                    resultValue = false;
                }
                return resultValue;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return resultValue;
            }
        }

        private void currlst_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtDGVItem.Visible = false;
            try
            {                
                if (currlst.SelectedIndex != -1)
                {                    
                    txtSaveResult.Text = "";
                    //141030_0 移动到载入资料后再变更 mylastIndex = currlst.SelectedIndex;//140530_1
                    currSeq = currlst.SelectedIndex + 1;
                    myEquipName = this.currlst.SelectedItem.ToString();
                    if (myEquipName.Contains("_"))
                    {
                        int lastChar = myEquipName.IndexOf("_");
                        myQueryEquipName = myEquipName.Substring(0, lastChar);
                    }
                    else
                    {
                        myQueryEquipName = myEquipName;
                    }

                    myGlobalEquipID = Convert.ToInt64(PNInfo.getDTColumnInfo(PNInfo.GlobalTotalDS.Tables["GlobalAllEquipmentList"], "ID", "ItemName='" + myQueryEquipName + "'"));

                    DataTable myPrmtrDT = new DataTable();
                    if (!blnAddNewEquip)
                    {
                        btnEditPrmtrOK.Enabled = true;
                        if (getInfoFromDT(PNInfo.TopoToatlDS.Tables["TopoEquipment"], currlst.SelectedIndex) == false)
                        {
                            return;
                        }

                        PNInfo.showTablefilterStrInfo(PNInfo.TopoToatlDS.Tables["TopoEquipmentParameter"], dgvEquipPrmtr, "PID=" + myPrmtrPID);
                        
                        //140709_3
                        if (dgvEquipPrmtr.Columns["Item"].Width + dgvEquipPrmtr.Columns["ItemValue"].Width < dgvEquipPrmtr.Size.Width)
                        {
                            dgvEquipPrmtr.Columns["ItemValue"].Width = dgvEquipPrmtr.Size.Width - dgvEquipPrmtr.Columns["Item"].Width;
                        }

                        int myTopoEquipPrmtrCount = dgvEquipPrmtr.Rows.Count;
                        int myGlobalEquipPrmtrCount = PNInfo.currPrmtrCountExisted(PNInfo.GlobalTotalDS.Tables["GlobalAllEquipmentParamterList"], "PID=" + +myGlobalEquipID);

                        string[,] myValues = new string[myGlobalEquipPrmtrCount, 2];

                        for (int i = 0; i < myGlobalEquipPrmtrCount; i++)  
                        {
                            if (i < myTopoEquipPrmtrCount)
                            {
                                myValues[i, 0] = dgvEquipPrmtr.Rows[i].Cells["Item"].Value.ToString();
                                myValues[i, 1] = dgvEquipPrmtr.Rows[i].Cells["ItemValue"].Value.ToString();
                            }
                            else   //140707_0
	                        {
                                myValues[i, 0] = "";
                                myValues[i, 1] = "";
	                        }
                        }

                        if (myTopoEquipPrmtrCount != myGlobalEquipPrmtrCount)
                        {
                            MessageBox.Show("The records of TopoPrmtrTable=" + myTopoEquipPrmtrCount + " ,\n but the records of GlobalPrmtrTable=" + myGlobalEquipPrmtrCount
                                + ";\n Now system will add " + (myGlobalEquipPrmtrCount - myTopoEquipPrmtrCount) + "  records of EquipmentParamter");
                            PNInfo.showTablefilterStrInfo(PNInfo.GlobalTotalDS.Tables["GlobalAllEquipmentParamterList"], dgvEquipPrmtr, "PID=" + myGlobalEquipID);
                            //若Topo表中存在部分资料则需要覆盖掉此部分Global的默认参数!

                            //140709_3
                            if (dgvEquipPrmtr.Columns["Item"].Width + dgvEquipPrmtr.Columns["ItemValue"].Width < dgvEquipPrmtr.Size.Width)
                            {
                                dgvEquipPrmtr.Columns["ItemValue"].Width = dgvEquipPrmtr.Size.Width - dgvEquipPrmtr.Columns["Item"].Width;
                            }

                            for (int i = 0; i < myGlobalEquipPrmtrCount; i++)
                            {
                                if (myValues[i, 0] == dgvEquipPrmtr.Rows[i].Cells["Item"].Value.ToString())
                                {
                                    dgvEquipPrmtr.Rows[i].Cells["ItemValue"].Value = myValues[i, 1];
                                }
                            }

                        }
                    }
                    else
                    {
                        btnEditPrmtrOK.Enabled = false;

                        PNInfo.showTablefilterStrInfo(PNInfo.GlobalTotalDS.Tables["GlobalAllEquipmentParamterList"], dgvEquipPrmtr, "PID=" + myGlobalEquipID);

                        myPrmtrPID = PNInfo.mylastIDTestEquip + 1;
                    }
                    PNInfo.hideMyIDPID(dgvEquipPrmtr);
                    
                    mylastIndex = currlst.SelectedIndex;    //141030_0 

                }
                else
                {
                    mylastIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void enableAddNew(bool state)
        {
            this.globalistName.Enabled = state;
            this.btnUp.Enabled = state;
            this.btnDown.Enabled = state;
            cboFunc.Enabled = state;
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult drst = new DialogResult();

                drst = (MessageBox.Show("Delete Item --> " + currlst.SelectedItem.ToString() +
                    "\n \n Pls choose 'Y' (是) to continue?",
                    "Notice", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2));

                if (drst == DialogResult.Yes)
                {

                    //若为新增的项目则必须制定当前的选择的Item
                    int CurrIndex = currlst.SelectedIndex;
                    string sName = currlst.Items[CurrIndex].ToString();
                    long myPID = PID;

                    //DataTable资料移除部分待新增!!!

                    bool result = PNInfo.DeleteItemForDT(PNInfo.TopoToatlDS.Tables["TopoEquipment"], "ID=" + myPrmtrPID);   // "PID=" + myPID + "and ItemName='" + sName + "'");
                    bool resultPrmtr = PNInfo.DeleteItemForDT(PNInfo.TopoToatlDS.Tables["TopoEquipmentParameter"], "PID=" + myPrmtrPID);
                    currlst.Items.RemoveAt(CurrIndex);
                    if (result && resultPrmtr)
                    {
                        PNInfo.ISNeedUpdateflag = true; //140603_2
                        PNInfo.myDeletedCountTestEquip++;
                        PNInfo.myDeletedCountTestEquipPrmtr++;

                        // 若为新增后未保存参数再删除会出现错误! 
                        if (blnAddNewEquip)
                        {
                            PNInfo.mylastIDTestEquip--;
                            PNInfo.mynewIDTestEquip--;
                            PNInfo.myTestEquipAddOKFlag = true;         //140529_1
                            PNInfo.myTestEquipPrmtrAddOKFlag = true;    //140529_1                            
                        }
                        enableAddNew(true);
                        myNewEquipcount--;
                        //this.blnAddNew = false;
                        this.blnAddNewEquip = false;
                        this.btnAdd.Enabled = true;
                        grpEquipPrmtr.Enabled = true;
                        btnNextPage.Enabled = true;
                       
                        currlst.Enabled = true;
                        currlst.Focus();
                        
                        MessageBox.Show("ItemNO: " + CurrIndex + ";Name =" + sName + "has been deleted!");
                    }
                    else
                    {
                        MessageBox.Show("ItemNO: " + CurrIndex + "!Name =" + sName + "deleted Fail...");
                    }
                }
                mylastIndex = -1;   //140603_2
                RefreshEquipInfo();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        
        private void btnPreviousPage_Click(object sender, EventArgs e)
        {
            this.Close();          
        }

        private void btnNextPage_Click(object sender, EventArgs e)
        {
            try
            {   
                //140530_1714 Deleted
                //DialogResult drst = new DialogResult();
                //drst = (MessageBox.Show("即将进入TestPlan Name=" + this.TestPlanName + "的TestControl维护界面,是否继续? \n 选择是继续 \n 选择 否 继续编辑!", "提示", MessageBoxButtons.YesNo));
                //if (drst == DialogResult.Yes)
                //{
                    TestCtrlForm myTestCtrlForm = new TestCtrlForm();
                    myTestCtrlForm.Name = "TestCtrl";
                    myTestCtrlForm.blnAddNewCtrl = blnAddNew;   //140607_1
                    myTestCtrlForm.TestPlanName = this.TestPlanName;
                    string filterstring = "ItemName='" + myTestCtrlForm.TestPlanName.Trim() + "' and ID=" + PID + "";

                    myTestCtrlForm.myCtrlPID = PNInfo.getNextTablePIDFromDT(PNInfo.TopoToatlDS.Tables[PNInfo.ConstTestPlanTables[1]], filterstring);

                    myTestCtrlForm.ShowDialog();   //show NextForm...
                    this.Close();   //141104_0

                //}
                //else if (drst == DialogResult.No)
                //{

                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        
        private void btnEditPrmtrOK_Click(object sender, EventArgs e)
        {
            try
            {
                showResultBackColor(2);

                //ADD/ Edit value for TopoEquipmentParameter Table
                bool editResult = EditPrmtrInfoForDT(PNInfo.TopoToatlDS.Tables["TopoEquipmentParameter"]);
                //修改颜色还原! txt隐藏!
                txtDGVItem.Visible = false;
                if (PNInfo.currPrmtrCountExisted(PNInfo.TopoToatlDS.Tables["TopoEquipmentParameter"], "PID=" + myPrmtrPID)
                            == PNInfo.currPrmtrCountExisted(PNInfo.GlobalTotalDS.Tables["GlobalAllEquipmentParamterList"], "PID=" + +myGlobalEquipID)
                            )
                {
                    if (editResult && AddErr==false)
                    {
                        PNInfo.ISNeedUpdateflag = true; //140603_2
                        showResultBackColor(0);
                        for (int i = 0; i < dgvEquipPrmtr.Rows.Count; i++)
                        {
                            for (int j = 0; j < dgvEquipPrmtr.Columns.Count; j++)
                            {
                                if (dgvEquipPrmtr.Rows[i].Cells[j].Style.BackColor != Color.White)
                                    dgvEquipPrmtr.Rows[i].Cells[j].Style.BackColor = Color.White;
                            }
                        }

                        if (!currlst.Enabled)
                        {
                            currlst.Enabled = true;
                            currlst.Focus();
                        }

                        PNInfo.myTestEquipAddOKFlag = true;         //140529_1
                        PNInfo.myTestEquipPrmtrAddOKFlag = true;    //140529_1
                        PNInfo.myTestEquipISNewFlag = false;        //140530_2
                        PNInfo.myTestEquipPrmtrISNewFlag = false;   //140530_2
                        btnAdd.Enabled = true;
                        btnNextPage.Enabled = true;
                        
                    }
                    else
                    {
                        //blnAddNewEquip = true;
                        currlst.Enabled = false;//140604_0
                        showResultBackColor(1);
                        btnNextPage.Enabled = false;
                        if (this.blnAddNewEquip) btnAdd.Enabled = false;
                    }

                    enableAddNew(true);

                }
                else
                {
                    MessageBox.Show("The data is incomplete!Pls confirm again!");
                    showResultBackColor(1);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                showResultBackColor(1);
            }
            
        }

        void showResultBackColor(int levle)
        {
            try
            {
                if (levle == 0)
                {
                    txtSaveResult.ForeColor = Color.Green;
                    txtSaveResult.Text = "Save OK!";
                    txtSaveResult.Refresh();
                }
                else if (levle == 1)
                {
                    txtSaveResult.ForeColor = Color.Red;
                    txtSaveResult.Text = "Save Fail!";
                    txtSaveResult.Refresh();
                }
                else if (levle == 2)
                {
                    txtSaveResult.ForeColor = Color.Blue;
                    txtSaveResult.Text = "Data preservation... Please wait...";
                    txtSaveResult.Refresh(); ;
                    Thread.Sleep(20);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            } 
        }

        bool EditPrmtrInfoForDT(DataTable mydt)
        {
            bool myEditResult = false;
            AddErr = false; //140604_0
            try
            {   
                int myRowIndex=-1;
                

                for (int i = 0; i < this.dgvEquipPrmtr.Rows.Count; i++)
                {
                    myRowIndex = i;
                    string myTempstr = this.dgvEquipPrmtr.Rows[myRowIndex].Cells["ItemValue"].Value.ToString().Trim();
                    string filterString = this.dgvEquipPrmtr.Rows[myRowIndex].Cells["Item"].Value.ToString();
                    if (myTempstr.Length == 0 )
                    {
                        if (AddErr == false)
                        {
                            MessageBox.Show("The data is incomplete!Pls confirm all parameters is OK?");
                            AddErr = true;
                            currlst.Enabled = false;

                        }
                        else
                        {                            
                            currlst.Enabled = true;
                        }
                        //return myEditResult;
                    }

                    
                    DataRow[] myROWS = mydt.Select("Item='" + filterString + "' and PID='" + myPrmtrPID + "'");
                    if (myROWS.Length== 1)
                    {
                        //Need check Rows Count is Equals GlobalEquipRows? TBD at 14519
                        if (this.blnAddNewEquip)
                        {
                            MessageBox.Show("Add New Error!" + myROWS.Length + (" records existed; \n filterString--> Item='" + filterString + "' and PID='" + myPrmtrPID + "'"));
                            return myEditResult;
                        }
                        
                        //myROWS[0]["PID"] = this.PID.ToString();
                        myROWS[0]["Item"] = this.dgvEquipPrmtr.Rows[myRowIndex].Cells["Item"].Value.ToString();
                        myROWS[0]["ItemValue"] = this.dgvEquipPrmtr.Rows[myRowIndex].Cells["ItemValue"].Value.ToString();
                    }
                    else //if (this.blnAddNewEquip) 140531_2 找不到则默认为新增
                    {
                        DataRow myNewRow = mydt.NewRow();
                        myNewRow.BeginEdit();
                        myNewRow["ID"] = PNInfo.mylastIDTestEquipPrmtr + 1;
                        myNewRow["PID"] = this.myPrmtrPID.ToString();
                        myNewRow["Item"] = this.dgvEquipPrmtr.Rows[myRowIndex].Cells["Item"].Value.ToString();
                        myNewRow["ItemValue"] = this.dgvEquipPrmtr.Rows[myRowIndex].Cells["ItemValue"].Value.ToString();
                        mydt.Rows.Add(myNewRow);

                        myNewRow.EndEdit();

                        PNInfo.mylastIDTestEquipPrmtr++;
                        PNInfo.myAddCountTestEquipPrmtr++;
                        blnAddNewEquip = false;  //140530_1
                    }
                    //else 140531_2找不到则默认为新增
                    //{
                    //    MessageBox.Show("资料有误!请确认!!! 共有" + myROWS.Length + ("条记录; \n 条件--> ItemName='" + filterString + "' and PID=" + PID + ""));
                    //    return myEditResult;
                    //}
                }
                if (blnAddNewEquip)
                {
                    blnAddNewEquip = false; //更新完EquipParameter才能解除锁定,确认当前状态为新增资料
                }
                RefreshEquipInfo();
                
                myEditResult = true;
                return myEditResult;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return myEditResult;
            }            
        }

        private void dgvEquipPrmtr_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (dgvEquipPrmtr.CurrentRow != null && dgvEquipPrmtr.CurrentRow.Index != -1) //140714_0
                {
                    string descriptionString = PNInfo.getDTColumnInfo(PNInfo.GlobalTotalDS.Tables["GlobalAllEquipmentParamterList"]
                                , "ItemDescription", "Item='" + dgvEquipPrmtr.CurrentRow.Cells["Item"].Value.ToString() + "' and PID='" + myGlobalEquipID + "'");
                    string prmtrType = PNInfo.getDTColumnInfo(PNInfo.GlobalTotalDS.Tables["GlobalAllEquipmentParamterList"]
                                , "ItemType", "Item='" + dgvEquipPrmtr.CurrentRow.Cells["Item"].Value.ToString() + "' and PID='" + myGlobalEquipID + "'");

                    txtSaveResult.Text = "Current Item: \r\n" + dgvEquipPrmtr.CurrentCell.Value.ToString();    //140709_2

                    this.lblPrmtrType.Text = "PrmtrType: ";
                    this.lblMyType.Text = prmtrType;
                    if (this.lblMyType.Text.Length == 0)
                        this.lblMyType.Text = "Not Found ParameterType";
                    this.txtDescription.Text = descriptionString;

                    if (this.txtDescription.Text.Length == 0)
                        this.txtDescription.Text = "Not Found ParameterDescription";

                    if (dgvEquipPrmtr.CurrentCell.Value.ToString().Length>15)   //140714_1
                    {
                        dgvEquipPrmtr.Rows[dgvEquipPrmtr.CurrentRow.Index].Height = 50;
                    }

                    //获取当前的DGV.X,Y;新增txtBox
                    if (dgvEquipPrmtr.RowCount > 0 &&
                        (
                        dgvEquipPrmtr.Columns[dgvEquipPrmtr.CurrentCell.ColumnIndex].HeaderText == "ItemValue"
                        || dgvEquipPrmtr.Columns[dgvEquipPrmtr.CurrentCell.ColumnIndex].HeaderText == "内容"
                        ))
                    {
                        showTxtItem();
                        //dgvEquipPrmtr.CurrentCell.OwningRow.Cells[e.ColumnIndex - 1].Style.BackColor = Color.Yellow;
                        txtDGVItem.Text = dgvEquipPrmtr.CurrentCell.Value.ToString();
                        mylastDGVRowIndex = e.RowIndex;
                        mylastDGVColumnIndex = e.ColumnIndex;
                    }
                    else
                    {
                        txtDGVItem.Visible = false;
                    }

                    if (txtDGVItem.Visible && txtDGVItem.Enabled)   //140530_1
                    {
                        txtDGVItem.Focus();
                        txtDGVItem.BackColor = Color.YellowGreen;
                        txtDGVItem.Refresh();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void showTxtItem()
        {
            try
            {

                txtDGVItem.Size = new Size(dgvEquipPrmtr.Rows[dgvEquipPrmtr.CurrentCell.RowIndex].Cells["ItemValue"].Size.Width, dgvEquipPrmtr.CurrentCell.Size.Height);
                //txtDGVItem.BackColor = Color.Yellow;
                if (txtDGVItem.Visible == false)
                {
                    txtDGVItem.Visible = true;
                }
                int x = dgvEquipPrmtr.GetCellDisplayRectangle(dgvEquipPrmtr.Rows[dgvEquipPrmtr.CurrentCell.RowIndex].Cells["ItemValue"].ColumnIndex, dgvEquipPrmtr.CurrentCell.RowIndex, false).X;//(ColumnIndex, RowIndex, false).X;
                int y = dgvEquipPrmtr.GetCellDisplayRectangle(dgvEquipPrmtr.CurrentCell.ColumnIndex, dgvEquipPrmtr.CurrentCell.RowIndex, false).Y;
                txtDGVItem.Location = new Point(x + 5, y + 20);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        private void txtDGVItem_Leave(object sender, EventArgs e)
        {
            try
            {

                lock (this)
                {
                    if (blnUnloadFormflag == false) //Unload时不再执行,否则报错!
                    {
                        if (PNInfo.checkTypeOK(txtDGVItem.Text.ToString(), lblMyType.Text.ToString()))
                        {
                            string tempStr = "";
                            if (dgvEquipPrmtr.Columns.Contains("ItemValue"))
                            {
                                if (mylastDGVRowIndex != -1)    //140714_1
                                {
                                    tempStr = dgvEquipPrmtr.Rows[mylastDGVRowIndex].Cells["ItemValue"].Value.ToString();
                                    dgvEquipPrmtr.Rows[mylastDGVRowIndex].Cells["ItemValue"].Value = txtDGVItem.Text.ToString().Trim();
                                }
                            }
                            else if (dgvEquipPrmtr.Columns.Contains("内容"))
                            {
                                if (mylastDGVRowIndex != -1)    //140714_1
                                {
                                    tempStr = dgvEquipPrmtr.Rows[mylastDGVRowIndex].Cells["内容"].Value.ToString();
                                    dgvEquipPrmtr.Rows[mylastDGVRowIndex].Cells["内容"].Value = txtDGVItem.Text.ToString().Trim();
                                }
                            }
                            txtDGVItem.Visible = false;
                        }
                        else
                        {
                            txtDGVItem.Focus();
                            return;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void dgvEquipPrmtr_Scroll(object sender, ScrollEventArgs e)
        {
            txtDGVItem.Visible = false;
        }

        private void dgvEquipPrmtr_RowHeightChanged(object sender, DataGridViewRowEventArgs e)
        {
            try
            {
                showTxtItem();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void EquipmentForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            blnUnloadFormflag = true;
            if (this.blnAddNewEquip || AddErr) //140603_1712 Add 判定AddErr-->是否在添加过程中出现错误!
            {
                DialogResult myResult = MessageBox.Show(
                    "Data is incomplete, the system will delete the current maintenance item information !",
                    "Notice:",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button1);
                try
                {
                    //先删除TopoEquipmentParameter资料!
                    string queryCMD = "PID=" + this.myPrmtrPID;
                    int myExistCount = PNInfo.currPrmtrCountExisted(PNInfo.TopoToatlDS.Tables["TopoEquipmentParameter"], queryCMD);

                    if (myResult == DialogResult.OK)
                    {
                        if (myExistCount > 0)
                        {
                            PNInfo.DeleteItemForDT(PNInfo.TopoToatlDS.Tables["TopoEquipmentParameter"], queryCMD);
                            PNInfo.mylastIDTestEquipPrmtr = PNInfo.mylastIDTestEquipPrmtr - myExistCount;
                        }


                        //再删除TopoEquipment资料!
                        //141030_3 因为修改了设备名称:出现重复将误删,修改查询条件
                        //queryCMD = "ItemName='" + this.myEquipName + "' And PID=" + this.PID;
                        queryCMD = "ID=" + this.myPrmtrPID; //141030_3 

                        myExistCount = PNInfo.currPrmtrCountExisted(PNInfo.TopoToatlDS.Tables["TopoEquipment"], queryCMD);

                        if (myExistCount > 0)
                        {
                            PNInfo.DeleteItemForDT(PNInfo.TopoToatlDS.Tables["TopoEquipment"], queryCMD);
                            PNInfo.mylastIDTestEquip = PNInfo.mylastIDTestEquip - myExistCount;
                        }
                        blnAddNewEquip = false;
                        PNInfo.myTestEquipPrmtrAddOKFlag = true;    //140529_1
                        PNInfo.myTestEquipAddOKFlag = true;         //140529_1
                        mylastIndex = -1;   //140603_2
                        RefreshEquipInfo();
                        this.Dispose();
                    }
                    else //141104_0
                    {
                        e.Cancel = true;
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    RefreshEquipInfo();
                    //return;
                }
            }
            else
            {   //140612_3   
                PNInfo.myTestEquipPrmtrAddOKFlag = true;    //140529_1
                PNInfo.myTestEquipAddOKFlag = true;         //140529_1
            }
        }

        private void dgvEquipPrmtr_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                dgvEquipPrmtr.Columns[e.ColumnIndex].SortMode = DataGridViewColumnSortMode.NotSortable;//SortOrder.None;     
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void ChangeSEQ(int direction, ListBox myList, DataTable dt, string filterStr1, string filterStr2)
        {
            int myCurrRowSEQ = -1, myPrevRowSEQ = -1;
            try
            {
                myCurrRowSEQ = Convert.ToInt32(PNInfo.getDTColumnInfo(PNInfo.TopoToatlDS.Tables["TopoEquipment"], "SEQ", filterStr1));
                myPrevRowSEQ = Convert.ToInt32(PNInfo.getDTColumnInfo(PNInfo.TopoToatlDS.Tables["TopoEquipment"], "SEQ", filterStr2));
                
                DataRow[] dr1 = dt.Select(filterStr1);
                DataRow[] dr2 = dt.Select(filterStr2);
                if (dr1.Length == 1 && dr2.Length == 1)
                {
                    dr1[0]["SEQ"] = myPrevRowSEQ;
                    dr2[0]["SEQ"] = myCurrRowSEQ;
                }
                else
                {
                    MessageBox.Show("Data is not uniqe! " + dr1.Length + " records existed!");
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //141027_0
        private void btnUp_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (this.currlst.SelectedIndex >0)
                {
                    string tempIndexValue = this.currlst.Items[currlst.SelectedIndex].ToString();
                    string str1 = "PID=" + this.PID + " and ItemName='" + this.currlst.Items[currlst.SelectedIndex].ToString() + "' and Seq =" +currSeq ;
                    string str2 = "PID=" + this.PID + " and ItemName='" + this.currlst.Items[currlst.SelectedIndex - 1].ToString() + "' and Seq =" + (currSeq -1);
                    ChangeSEQ(1, currlst, PNInfo.TopoToatlDS.Tables["TopoEquipment"], str1, str2);
                    RefreshEquipInfo();
                    currlst.SelectedItem = tempIndexValue;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            try
            {

                if (this.currlst.SelectedIndex > -1 && this.currlst.SelectedIndex < this.currlst.Items.Count - 1)
                {
                    string tempIndexValue = this.currlst.Items[currlst.SelectedIndex].ToString();
                    string str1 = "PID=" + this.PID + " and ItemName='" + this.currlst.Items[currlst.SelectedIndex].ToString() + "' and Seq =" + currSeq;
                    string str2 = "PID=" + this.PID + " and ItemName='" + this.currlst.Items[currlst.SelectedIndex + 1].ToString() + "' and Seq =" + (currSeq+1);
                    ChangeSEQ(1, currlst, PNInfo.TopoToatlDS.Tables["TopoEquipment"], str1, str2);
                    RefreshEquipInfo();
                    currlst.SelectedItem = tempIndexValue;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void cboFunc_SelectedIndexChanged(object sender, EventArgs e)
        {            
            try
            {
                if (currlst.SelectedIndex != -1 && cboFunc.SelectedIndex != -1 && currlst.SelectedIndex == this.mylastIndex)
                {
                    int myIndex = cboFunc.SelectedIndex;
                    string tempEQName = "";
                    if (cboItemName.Text.Contains("_"))
                    {
                        int lastChar = cboItemName.Text.ToString().IndexOf("_");
                        tempEQName = myQueryEquipName + "_" + cboFunc.Text + "_" + cboItemType.Text;
                    }
                    else
                    {
                        tempEQName = myQueryEquipName;
                    }
                    //141029_3
                    //int myCount = PNInfo.currPrmtrCountExisted(PNInfo.TopoToatlDS.Tables["TopoEquipment"], "ItemName='" + tempEQName + "' and PID=" + PID + " and Role ="+ myIndex);

                    //if (myCount > 0)
                    //{
                    //    MessageBox.Show("已经存在该设备名称记录...,请确认!\n" + "ItemName='" + tempEQName + "' and PID=" + PID + " and Role =" + cboFunc.Text );
                    //    return;
                    //}
                    //string filterString = currlst.SelectedItem.ToString();
                    string filterString = "ID=" + myPrmtrPID;
                    DataRow[] myROWS = PNInfo.TopoToatlDS.Tables["TopoEquipment"].Select(filterString); //141029_3 "ItemName='" + tempEQName + "' and PID=" + PID + " and SEQ =" + currlst.SelectedIndex +1
                    if (myROWS.Length == 1)
                    {
                        //myROWS[0]["ItemName"] = tempEQName;
                        this.cboItemType.Text = myROWS[0]["ItemType"].ToString();
                        this.cboItemName.Text = myROWS[0]["ItemName"].ToString();
                        myROWS[0]["Role"] = myIndex;
                        //myPrmtrPID = System.Convert.ToInt64(myROWS[0]["ID"]);

                        txtSaveResult.Text = "The role of current equipment changes ok!-> " + cboFunc.Text  ;
                        txtSaveResult.ForeColor = Color.Blue;
                    }
                    else
                    {
                        MessageBox.Show("Error!" + myROWS.Length + (" records existed!; \n filterString--> " + filterString));
                        return;
                    }
                    //RefreshEquipInfo();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
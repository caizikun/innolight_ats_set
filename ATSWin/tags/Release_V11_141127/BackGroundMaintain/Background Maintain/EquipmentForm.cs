using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace GlobalInfo
{
    public partial class EquipmentForm : Form
    {
        public bool blnAddNewEquip = false;
        private bool blnIsAddEquipPrmtr = false;
        private long myPrmtrPID = -1;
        public string myEquipName = "";
        public string myQueryEquipName = "";

        long origmylastIDGlobalEquip=MainForm.mylastIDGlobalEquip;
        long origmynewIDGlobalEquip = MainForm.mynewIDGlobalEquip;
        long origmyDeletedCountClobalEquip = MainForm.myDeletedCountClobalEquip;

        long origmylastIDEquipPrmtr = MainForm.mylastIDGlobalEquipPrmtr;
        long origmynewIDEquipPrmtr = MainForm.mynewIDGlobalEquipPrmtr;
        long origmyDeletedCountEquipPrmtr = MainForm.myDeletedCountClobalEquipPrmtr;


        string currGlobalItemName = "";
        string currGlobalItemType = "";
        int myNewEquipcount = -1;
        int mylastEquipIndex=-1;

        public EquipmentForm()
        {
            InitializeComponent();
        }

        private void EquipmentForm_Load(object sender, EventArgs e)
        {
            try
            {
                mylastEquipIndex = -1;   //140603_2
                RefreshEquipInfo();
                ShowMyTip();
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

                mytip.SetToolTip(btnAdd, "Add a new equipment");
                mytip.SetToolTip(btnRemove, "Delete a equipment from equipment list");
                mytip.SetToolTip(btnSave, "Save equipment Info");

                mytip.SetToolTip(btnPrmtrSave, "Save parameter of current equipment");
                mytip.SetToolTip(btnPrmtrAdd, "Add a new parameter of current equipment");
                mytip.SetToolTip(btnPrmtrDelete, "Delete a new parameter of current equipment");

                mytip.SetToolTip(btnPreviousPage, "Return Mainform");
                mytip.SetToolTip(txtEquipPrmtrDescription, "Operate logs...");
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
                cboName.Text = "";
                cboType.Text="";
                txtSaveResult.Text = "";
                dgvEquipPrmtr.DataSource = null;

                cboPrmtrItem.Text = "";
                cboPrmtrType.Text = "";
                cboPrmtrValue.Text = "";
                txtEquipPrmtrDescription.Text = "";

                cboPrmtrType.Items.Clear();
                
                this.currlst.Enabled = (!this.blnAddNewEquip); //140707_0

                DataRow[] CurrEquipLst = MainForm.GlobalDS.Tables["GlobalAllEquipmentList"].Select();
                // = PNInfo.mySqlIO.GetDataTable("", "");
                foreach (DataRow dr in CurrEquipLst)
                {
                    currlst.Items.Add(dr["ItemType"] + ":" + dr["ItemName"]);
                }
                DataRow[] CurrEquipTypeLst = MainForm.GlobalDS.Tables["GlobalAllEquipmentList"].Select();
                foreach (DataRow dr in CurrEquipTypeLst)
                {
                    if (cboType.Items.Contains(dr["ItemType"]) == false)    //140917_1 Fix Bug
                    {
                        cboType.Items.Add(dr["ItemType"]);
                    }
                }

                DataRow[] drs = MainForm.GlobalDS.Tables["GlobalAllEquipmentParamterList"].Select();
                foreach (DataRow dr in drs)
                {
                    if (cboPrmtrType.Items.Contains(dr["ItemType"]) == false)    //140917_1 Fix Bug
                    {
                        cboPrmtrType.Items.Add(dr["ItemType"]);
                    }
                }
                if (mylastEquipIndex != -1)  //140530_2
                {
                    currlst.SelectedIndex = mylastEquipIndex;
                }
                setInfoIsAddNewEquip(blnAddNewEquip);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        bool EditOrAddEquip(bool isNewEquip)
        {
            bool opResult = false;

            try
            {
                btnAdd.Enabled = false;
                btnSave.Enabled = false;
                currlst.Enabled = false;

                string tempNameString = this.cboName.Text.ToString().Trim();
                string tempTypeString = this.cboType.Text.ToString().Trim();
                string tempEquipDescriptionString = this.txtEquipDescription.Text.ToString().Trim();

                myEquipName = currGlobalItemName;
                myQueryEquipName = myEquipName;
                ;
                MainForm.myGlobalEquipAddOKFlag = false;        //140529_1
                MainForm.myGlobalEquipPrmtrAddOKFlag = false;   //140529_1

                if (tempNameString.Length > 0 && tempTypeString.Length > 0 && tempEquipDescriptionString.Length > 0)
                {
                    currGlobalItemName = tempNameString;
                    currGlobalItemType = tempTypeString;


                    //AddOrEdit myEquipTable
                    bool EditEquipResult = EditInfoForDT(MainForm.GlobalDS.Tables["GlobalAllEquipmentList"]);

                    if (EditEquipResult)
                    {
                        runEquipMsgState((byte)MsgState.SaveOK); 
                        if (isNewEquip)
                        {      
                            opResult = true;
                            blnAddNewEquip = false;
                            myNewEquipcount++;
                            currlst.Items.Add(currGlobalItemType + ":" + currGlobalItemName);
                            int myNewRowIndex = (currlst.Items.Count - 1);
                            this.currlst.SelectedIndex = myNewRowIndex;
                            this.grpEquipPrmtr.Enabled = true;
                            this.btnPrmtrSave.Enabled = true;
                            btnRemove.Enabled = true;
                        }
                        else
                        {              
                            opResult = false;
                        }
                    }
                    return opResult;
                }
                else
                {
                    MessageBox.Show("Equipment Data is incomplete!");
                    return opResult;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return opResult;
            }
            finally
            {
                btnSave.Enabled = true;
                btnRemove.Enabled = true;
                btnAdd.Enabled = true;
                currlst.Enabled = true;
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (MainForm.checkItemLength("EquipName", cboName.Text, 30)
                    || MainForm.checkItemLength("EquipType", cboType.Text, 30)
                    || MainForm.checkItemLength("Description", this.txtEquipDescription.Text, 50))
                {
                    return;
                }
                
                if (this.cboName.Text.Length == 0
                    || this.cboType.Text.Length == 0
                    || this.txtEquipDescription.Text.Length == 0)
                {
                    MessageBox.Show("Parameter Data is incomplete,Pls confirm again?");
                    currlst.Enabled = false;
                    return;
                }
                else
                {
                    currlst.Enabled = true;
                }

                if (blnAddNewEquip)
                {
                    if (MainForm.currPrmtrCountExisted(MainForm.GlobalDS.Tables["GlobalAllEquipmentList"],
                                "ItemName ='" + this.cboName.Text.ToString() + "'") > 0)
                    {
                        //140707_0
                        MessageBox.Show("The new data of ItemName ='" + this.cboName.Text.ToString() + "' has existed! <Violate unique rule>");
                        return;
                    }
                }

                EditOrAddEquip(blnAddNewEquip);
                setInfoIsAddNewEquip(false);
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
                myPrmtrPID = MainForm.mylastIDGlobalEquip + 1;
                setInfoIsAddNewEquip(true);
                runEquipMsgState((byte)MsgState.AddNew);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void setInfoIsAddNewEquip(bool state)
        {
            try
            {
                blnAddNewEquip = state;
                currlst.Enabled = (!state);
                btnPrmtrAdd.Enabled = (!state);
                btnPrmtrDelete.Enabled = (!state);
                btnPrmtrSave.Enabled = (!state);                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
       

        void getInfoFromDT(DataTable mydt, string myQueryItemName)
        {
            try
            {
                string filterString = myQueryItemName;
                DataRow[] myROWS = mydt.Select("ItemName='" + filterString + "'");
                if (myROWS.Length == 1)
                {
                    this.cboType.Text = myROWS[0]["ItemType"].ToString();
                    this.cboName.Text = myROWS[0]["ItemName"].ToString();
                    this.txtEquipDescription.Text = myROWS[0]["ItemDescription"].ToString();
                    myPrmtrPID = System.Convert.ToInt64(myROWS[0]["ID"]);
                    //this.cboPrmtrType.Text = myROWS[0]["ItemType"].ToString();    //140709_0
                }
                else
                {
                    MessageBox.Show("Error! " + myROWS.Length + " records existed; \n filterString--> ItemName='" + filterString + "'");
                    return;
                }
            }
            catch (Exception ex) 
            { 
                MessageBox.Show(ex.ToString ());
                return;
            }
        }

        bool EditInfoForDT(DataTable mydt)
        {
            bool ResultValue = false;
            try
            {
                string filterString = this.cboName.Text.ToString();
                DataRow[] myROWS = mydt.Select("ItemName='" + filterString + "'");
                if (myROWS.Length == 1)
                {
                    if (this.blnAddNewEquip)
                    {
                        MessageBox.Show("Add new data error! \n" + myROWS.Length + " records existed; \n filterString--> ItemName='" + filterString + "'");
                        return ResultValue;
                    }                    
                    
                    myROWS[0]["ItemDescription"] = this.txtEquipDescription.ToString();
                    myROWS[0]["ItemType"] = this.cboType.Text.ToString();
                    myROWS[0]["ItemName"] = this.cboName.Text.ToString();
                    ResultValue = true;
                }
                else if (this.blnAddNewEquip && myROWS.Length == 0)
                {
                    DataRow myNewRow = mydt.NewRow();
                    myNewRow.BeginEdit();
                    myNewRow["ID"] = MainForm.mylastIDGlobalEquip+1 ;
                    myNewRow["ItemDescription"] = this.txtEquipDescription.Text.ToString();                    
                    myNewRow["ItemType"] = this.cboType.Text.ToString();
                    myNewRow["ItemName"] = this.cboName.Text.ToString();
                    mydt.Rows.Add(myNewRow);
                    myNewRow.EndEdit();

                    //if (myNewEquipcount > PNInfo.mylastIDGlobalEquip) //当执行一次添加后并未点击OK更新资料!则不进行PNInfo的ID++
                    //{
                        MainForm.mylastIDGlobalEquip++;
                        MainForm.myAddCountGlobalEquip++;
                    //}
                    
                    //RefreshList();
                    int myNewRowIndex = (currlst.Items.Count - 1);
                    
                    currlst.SelectedIndex = myNewRowIndex;
                    

                    //blnAddNewEquip = false;  //新增一条记录后将新增标志改为false; 
                    ResultValue = true;
                }
                else
                {
                    MessageBox.Show("Error! \n " + myROWS.Length + " records existed; \n filterString--> ItemName='" + filterString + "'");
                    ResultValue = false;
                }
                return ResultValue;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return ResultValue;
            }
        }

        private void currlst_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                clearPrmtr();   //140917_0
                if (currlst.SelectedIndex != -1)
                {
                    txtSaveResult.Text = "";
                    mylastEquipIndex = currlst.SelectedIndex;
                    myEquipName = this.currlst.SelectedItem.ToString();
                    
                    DataTable myPrmtrDT = new DataTable();
                    if (!blnAddNewEquip)
                    {
                        int lastChar = myEquipName.IndexOf(":");
                        myQueryEquipName = myEquipName.Substring(lastChar + 1, myEquipName.Length - (lastChar + 1));
                        myPrmtrPID = Convert.ToInt64(MainForm.getDTColumnInfo(MainForm.GlobalDS.Tables["GlobalAllEquipmentList"], "ID", "ItemName='" + myQueryEquipName + "'"));

                        btnPrmtrSave.Enabled = true;
                        getInfoFromDT(MainForm.GlobalDS.Tables["GlobalAllEquipmentList"], myQueryEquipName);

                        MainForm.showTablefilterStrInfo(MainForm.GlobalDS.Tables["GlobalAllEquipmentParamterList"], dgvEquipPrmtr, "PID=" + myPrmtrPID);
                        dgvEquipPrmtr.DataSource = MainForm.GlobalDS.Tables["GlobalAllEquipmentParamterList"].DefaultView;
                        runEquipMsgState((byte)MsgState.Edit);                
                    }
                    else
                    {                        
                        myQueryEquipName = myEquipName;
                        myPrmtrPID = MainForm.mylastIDGlobalEquip + 1;
                        MainForm.showTablefilterStrInfo(MainForm.GlobalDS.Tables["GlobalAllEquipmentParamterList"], dgvEquipPrmtr, "PID=" + myPrmtrPID);
                        runEquipMsgState((byte)MsgState.AddNew); 
                    }
                }
                else
                {
                    mylastEquipIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (blnAddNewEquip)    //140917_0 新增误点新增取消新增状态!并直接返回
            {
                blnAddNewEquip = false;
                runEquipMsgState((byte)MsgState.Clear);
                currlst.Enabled = true;
                setInfoIsAddNewEquip(false);
                currlst.SelectedItem = null;
                if (currlst.Items.Count > 0)
                {
                    currlst.SelectedIndex = 0;
                }
                return;
            } 

            try
            {
                DialogResult drst = new DialogResult();
                string sName = this.cboName.Text.ToString();
                string sType = this.cboType.Text.ToString();
                drst = (MessageBox.Show("Delete ItemName--> " + this.cboName.Text.ToString() +
                    "\n \n Choose  'Y' (是) to continue?",
                    "Notice", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2));

                if (drst == DialogResult.Yes)
                {
                    bool result = MainForm.DeleteItemForDT(MainForm.GlobalDS.Tables["GlobalAllEquipmentList"], "ItemName='" + sName + "' and ID=" + myPrmtrPID);
                    bool resultPrmtr = MainForm.DeleteItemForDT(MainForm.GlobalDS.Tables["GlobalAllEquipmentParamterList"], "PID=" + myPrmtrPID);
                    
                    if (result && resultPrmtr)
                    {
                        MainForm.ISNeedUpdateflag = true; //140603_2
                        MainForm.myDeletedCountClobalEquip++;
                        MainForm.myDeletedCountClobalEquipPrmtr++;
                        
                        myNewEquipcount--;
                        this.blnAddNewEquip = false;
                        this.btnAdd.Enabled = true;
                        grpEquipPrmtr.Enabled = true;
                        if (!currlst.Enabled)
                        {
                            currlst.Enabled = true;
                            currlst.Focus();
                        }
                        MessageBox.Show("Item :" + sName + " deleted OK!");
                        runEquipMsgState((byte)MsgState.Delete); 
                    }
                    else
                    {
                        MessageBox.Show("Item :" + sName + " deleted Fail!");
                    }
                }
                mylastEquipIndex = -1;   //140603_2
                RefreshEquipInfo();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        void exitForm()
        {
            this.Close();
        }
        private void btnPreviousPage_Click(object sender, EventArgs e)
        {
            exitForm();  
        }
        
        private void btnEditPrmtrOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (MainForm.checkItemLength("PrmtrName", cboPrmtrItem.Text, 30)
                     || MainForm.checkItemLength("ItemValue", cboPrmtrValue.Text, 255)
                     || MainForm.checkItemLength("ValueType", cboPrmtrType.Text, 10)
                     || MainForm.checkItemLength("Description", this.txtEquipPrmtrDescription.Text, 500))
                {
                    return;
                }
                showResultBackColor(2);

                string myItemString = cboPrmtrItem.Text.ToString();
                string myValueString = cboPrmtrValue.Text.ToString();
                string myDescriptionString = txtEquipPrmtrDescription.Text.ToString();
                string myTypeString = cboPrmtrType.Text.ToString();


                if (myValueString.Length == 0 || myItemString.Length == 0
                    || myTypeString.Length == 0)
                {
                    MessageBox.Show("Incomplete information, please confirm!");
                    currlst.Enabled = false;
                    return;
                }
                else
                {
                    currlst.Enabled = true;
                }

                if (blnIsAddEquipPrmtr)
                {
                    if (MainForm.currPrmtrCountExisted(MainForm.GlobalDS.Tables["GlobalAllEquipmentParamterList"],
                        "PID= " + myPrmtrPID + " and Item ='" + this.cboPrmtrItem.Text.ToString() + "'" )> 0)
                    {
                        //140704_1 约束PID+Name+Channel
                        MessageBox.Show("Add a new item error! \n The item has existed; \n");
                        return;
                    }
                }

                bool editResult = EditPrmtrInfoForDT(MainForm.GlobalDS.Tables["GlobalAllEquipmentParamterList"]);
                if (editResult )
                {
                    MainForm.ISNeedUpdateflag = true; //140603_2
                    showResultBackColor(0);
                    runPrmtrMsgState((byte)MsgState.SaveOK);
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
                    txtSaveResult.Text = "Save fail!";
                    txtSaveResult.Refresh();
                }
                else if (levle == 2)
                {
                    txtSaveResult.ForeColor = Color.Blue;
                    txtSaveResult.Text = "Data preservation... Please wait...";
                    txtSaveResult.Refresh(); ;
                    Thread.Sleep(100);
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
            try
            {                     
                    string myItemString = cboPrmtrItem.Text.ToString();
                    string myValueString = cboPrmtrValue.Text.ToString();
                    string myDescriptionString = txtEquipPrmtrDescription.Text.ToString();
                    string myTypeString = cboPrmtrType.Text.ToString();

                    DataRow[] myROWS = mydt.Select("Item='" + myItemString + "' and PID=" + myPrmtrPID);
                    if (myROWS.Length== 1)
                    {
                        //Need check Rows Count is Equals GlobalEquipRows? TBD at 14519
                        if (this.blnAddNewEquip)
                        {
                            MessageBox.Show("Add new data error! \n " + myROWS.Length + " records existed; \n filterString--> Item='" + myItemString + "' and PID=" + myPrmtrPID );
                            return myEditResult;
                        }
                        
                        //myROWS[0]["PID"] = this.PID.ToString();
                        myROWS[0]["Item"] = myItemString;
                        myROWS[0]["ItemValue"] = myValueString;
                        myROWS[0]["ItemType"] = myTypeString;
                        myROWS[0]["ItemDescription"] = myDescriptionString;
                    }
                    else //if (this.blnAddNewEquip) 140531_2 找不到则默认为新增
                    {
                        DataRow myNewRow = mydt.NewRow();
                        myNewRow.BeginEdit();
                        myNewRow["ID"] = MainForm.mylastIDGlobalEquipPrmtr + 1;
                        myNewRow["PID"] = this.myPrmtrPID.ToString();
                        myNewRow["Item"] = myItemString;
                        myNewRow["ItemValue"] = myValueString;
                        myNewRow["ItemType"] = myTypeString;
                        myNewRow["ItemDescription"] = myDescriptionString;
                        mydt.Rows.Add(myNewRow);

                        myNewRow.EndEdit();

                        MainForm.mylastIDGlobalEquipPrmtr++;
                        MainForm.myAddCountGlobalEquipPrmtr++;
                        blnAddNewEquip = false;  //140530_1
                    }
                //} 140609_3
                if (blnAddNewEquip)
                {
                    blnAddNewEquip = false; //更新完EquipParameter才能解除锁定,确认当前状态为新增资料
                }
                
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
                this.txtEquipPrmtrDescription.Text = dgvEquipPrmtr.CurrentRow.Cells["ItemDescription"].Value.ToString();
                this.cboPrmtrType.Text = dgvEquipPrmtr.CurrentRow.Cells["ItemType"].Value.ToString();
                this.cboPrmtrItem.Text = dgvEquipPrmtr.CurrentRow.Cells["Item"].Value.ToString();
                this.cboPrmtrValue.Text = dgvEquipPrmtr.CurrentRow.Cells["ItemValue"].Value.ToString();

                if (this.txtEquipPrmtrDescription.Text.Length == 0)
                {
                    this.txtEquipPrmtrDescription.Text = "Not Found ParameterDescription";
                }
                runPrmtrMsgState((byte)MsgState.Edit);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        
        private void EquipmentForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.blnAddNewEquip) //140603_1712 Add 判定AddErr-->是否在添加过程中出现错误!
            {
               
                blnAddNewEquip = false;//141030_1
                return; //141030_1  不再删除未保存资料<万一字符串为已存在资料 可能误删>                
            }
        }

        private void dgvEquipPrmtr_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                lock (this)
                {
                    if (e.ColumnIndex < this.dgvEquipPrmtr.Columns.Count)
                    {
                        dgvEquipPrmtr.Columns[e.ColumnIndex].SortMode = DataGridViewColumnSortMode.NotSortable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void globalistName_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnPrmtrAdd_Click(object sender, EventArgs e)
        {
            try
            {
                 runPrmtrMsgState((byte)MsgState.AddNew);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        bool AddPrmtrInfotoDT(DataTable mydt,string myItemString)
        {
            bool myEditResult=false;

            try
            {
                DataRow[] myROWS = mydt.Select("Item='" + myItemString + "' and PID=" + myPrmtrPID );

                if (myROWS.Length== 1)
                {
                    MessageBox.Show("Add new data error! \n " + myROWS.Length + " records existed; \n filterString--> Item='" + myItemString + "' and PID=" + myPrmtrPID + "");
                    return myEditResult;                                          
                }
                else 
                {
                    DataRow myNewRow = mydt.NewRow();
                    myNewRow.BeginEdit();
                    myNewRow["ID"] = MainForm.mylastIDGlobalEquipPrmtr + 1;
                    myNewRow["PID"] = myPrmtrPID.ToString();
                    myNewRow["Item"] = cboPrmtrItem.Text.ToString();
                    myNewRow["ItemValue"] = cboPrmtrValue.Text.ToString();
                    myNewRow["ItemType"] = cboPrmtrType.Text.ToString();
                    myNewRow["ItemDescription"] = txtEquipPrmtrDescription.Text.ToString();
                    mydt.Rows.Add(myNewRow);
                    
                    myNewRow.EndEdit();

                    MainForm.mylastIDGlobalEquipPrmtr++;
                    MainForm.myAddCountGlobalEquipPrmtr++;

                    btnAdd.Enabled = true;
                    btnSave.Enabled = true;
                    currlst.Enabled = true;
                    
                    myEditResult =true ;
                    return myEditResult;
                }
                
            }
             catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return myEditResult;
            }
        
        }

        private void btnPrmtrDelete_Click(object sender, EventArgs e)
        {
            if (blnIsAddEquipPrmtr)    //140917_0 新增误点新增取消新增状态!并直接返回
            {
                blnIsAddEquipPrmtr = false;
                runPrmtrMsgState((byte)MsgState.Clear);
                return;
            } 

            try
            {
                DialogResult drst = new DialogResult();

                string myPrmtrItem = this.dgvEquipPrmtr.CurrentRow.Cells["Item"].Value.ToString();  //140917_0 修正显示问题
                drst = (MessageBox.Show("Delete Item -->  " + myPrmtrItem +
                    "\n \n Pls choose 'Y' (是) to continue?",
                    "Notice", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2));

                if (drst == DialogResult.Yes)
                {
                    //若为新增的项目则必须制定当前的选择的Item
                    bool resultPrmtr = MainForm.DeleteItemForDT(MainForm.GlobalDS.Tables["GlobalAllEquipmentParamterList"], "PID=" + myPrmtrPID + " and Item ='" + myPrmtrItem + "'");
                   
                    if (resultPrmtr)
                    {
                        MainForm.ISNeedUpdateflag = true; //140603_2
                        MainForm.myDeletedCountClobalEquipPrmtr++;

                        this.btnPrmtrAdd.Enabled = true;
                        MessageBox.Show("ItemName =" + myPrmtrItem + " has been deleted successful!");
                        runPrmtrMsgState((byte)MsgState.Delete);
                    }
                    else
                    {
                        MessageBox.Show("ItemName =" + myPrmtrItem + " deleted fail!");
                    }
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
                this.dgvEquipPrmtr.Enabled = true;
                this.cboPrmtrItem.Enabled = false;
                this.cboType.Enabled = false;
                this.cboPrmtrType.Enabled = true;
                if (state == (byte)MsgState.AddNew)
                {
                    this.txtSaveResult.Text = "Add a new parameter!";
                    this.txtSaveResult.BackColor = Color.Yellow;
                    this.cboPrmtrItem.BackColor = Color.Yellow;
                    this.cboPrmtrType.BackColor = Color.Yellow;
                    this.cboPrmtrValue.BackColor = Color.Yellow;
                    this.cboPrmtrItem.Enabled = true;
                    blnIsAddEquipPrmtr = true;  //140917_0
                    //dgvPrmtr.Enabled = false;
                }
                else if (state == (byte)MsgState.Edit)
                {
                    this.txtSaveResult.Text = "Edit parameters!";
                    this.txtSaveResult.BackColor = Color.GreenYellow;
                    this.cboPrmtrItem.BackColor = Color.GreenYellow;
                    this.cboPrmtrType.BackColor = Color.GreenYellow;
                    this.cboPrmtrValue.BackColor = Color.GreenYellow;
                    blnIsAddEquipPrmtr = false;  //140917_0
                }
                else if (state == (byte)MsgState.SaveOK)
                {
                    this.txtSaveResult.Text = "Parameter : " + this.cboPrmtrItem.Text + " has been saved successful!!";
                    this.txtSaveResult.BackColor = Color.YellowGreen;
                    this.cboPrmtrItem.BackColor = Color.YellowGreen;
                    this.cboPrmtrType.BackColor = Color.YellowGreen;
                    this.cboPrmtrValue.BackColor = Color.YellowGreen;
                    blnIsAddEquipPrmtr = false;  //140917_0
                }
                else if (state == (byte)MsgState.Delete)
                {
                    this.txtSaveResult.Text = "Parameter :  " + this.cboPrmtrItem.Text + " has been deleted successful!";
                    this.txtSaveResult.BackColor = Color.Pink;
                    this.cboPrmtrItem.BackColor = Color.Pink;
                    this.cboPrmtrType.BackColor = Color.Pink;
                    this.cboPrmtrValue.BackColor = Color.Pink;
                    blnIsAddEquipPrmtr = false;  //140917_0
                }
                else if (state == (byte)MsgState.Clear)
                {
                    this.txtSaveResult.Text = "";
                    this.txtSaveResult.BackColor = Color.White;
                    this.cboPrmtrItem.BackColor = Color.White;
                    this.cboPrmtrType.BackColor = Color.White;
                    this.cboPrmtrValue.BackColor = Color.White;
                    blnIsAddEquipPrmtr = false;  //140917_0
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void clearPrmtr()
        {
            cboPrmtrItem.Text = ""; //140917_0
            cboPrmtrType.Text = ""; //140917_0
            cboPrmtrValue.Text = "";    //140917_0
            txtEquipPrmtrDescription.Text = ""; //140917_0}
        }

        void runEquipMsgState(byte state)
        {
            try
            {
                this.cboType.Enabled = false;
                this.cboName.Enabled = true;
                this.grpEquipPrmtr.Enabled = true;  //140917_0 
                this.grpPrmtr.Enabled = true;       //140917_0
                if (state == (byte)MsgState.AddNew)
                {
                    this.txtSaveResult.Text = "Add a new equipment!";
                    this.txtSaveResult.BackColor = Color.Yellow;
                    this.cboType.Enabled = true;
                    this.cboType.BackColor = Color.Yellow;
                    this.cboName.BackColor = Color.Yellow;
                    this.dgvEquipPrmtr.DataSource = null; //140917_0
                    this.grpEquipPrmtr.Enabled = false;  //140917_0 
                    this.grpPrmtr.Enabled = false;       //140917_0
                    clearPrmtr();   //140917_0
                     
                }
                else if (state == (byte)MsgState.Edit)
                {
                    this.txtSaveResult.Text = "Edit equipment info!";
                    this.txtSaveResult.BackColor = Color.GreenYellow;
                    this.cboType.BackColor = Color.GreenYellow;
                    this.cboName.BackColor = Color.GreenYellow;
                }
                else if (state == (byte)MsgState.SaveOK)
                {
                    this.txtSaveResult.Text = "Equipment : " + this.cboType.Text + " has been saved successful!";
                    this.txtSaveResult.BackColor = Color.YellowGreen;
                    this.cboType.BackColor = Color.YellowGreen;
                    this.cboName.BackColor = Color.YellowGreen;
                }
                else if (state == (byte)MsgState.Delete)
                {
                    this.txtSaveResult.Text = "Equipment : " + this.cboType.Text + " has been deleted successful!";
                    this.txtSaveResult.BackColor = Color.Pink;
                    this.cboType.BackColor = Color.Pink;
                    this.cboName.BackColor = Color.Pink;
                }
                else if (state == (byte)MsgState.Clear)
                {
                    this.cboType.Text = "";
                    this.txtSaveResult.Text = "";
                    this.txtSaveResult.BackColor = Color.White;
                    this.cboType.BackColor = Color.White;
                    this.cboName.BackColor = Color.White;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void grpEquipPrmtr_Enter(object sender, EventArgs e)
        {

        }
    }
}

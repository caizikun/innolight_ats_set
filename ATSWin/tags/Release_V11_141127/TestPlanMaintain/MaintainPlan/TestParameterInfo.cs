using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;

namespace TestPlan
{
    public partial class TestParameterInfo : Form
    {
        bool[] MyPrmtrIndexOK;
        public bool blnAddNew = false;        
        public string ItemName = "";       
        public long myPrmtrPID = -1;
        public long myGlobalPrmtrPID = -1; 

        long origmylastIDModelPrmtr = PNInfo.mylastIDTestPrmtr;
        long origmynewIDModelPrmtr = PNInfo.mynewIDTestPrmtr;
        long origmyDeletedCountModelPrmtr = PNInfo.myDeletedCountTestPrmtr;

        int mylastIndex = -1;

        bool blnFstIndexChangeOK = false;
        //ConstGlobalListTables = new string[] { "GlobalAllEquipmentList", "GlobalAllEquipmentParamterList", "GlobalAllAppModelList", "GlobalAllTestModelList", "GlobalTestModelParamterList" };
        CheckBox[] myChkArry;

        public TestParameterInfo()
        {
            InitializeComponent();
        }

        private void TestParameterInfo_Load(object sender, EventArgs e)
        {
            try
            {
                //initinal Current List
                currlst.Items.Clear();
                this.btnFinish.Enabled = true; //140603_0
                txtItemName.Text = ItemName;
                txtGlobalModelName.Text = ItemName;
                myGlobalPrmtrPID = Convert.ToInt64(PNInfo.getDTColumnInfo(PNInfo.GlobalTotalDS.Tables[3], "ID", "ItemName = '" + ItemName + "'"));

                DataRow[] CurrModelLst;

                CurrModelLst = PNInfo.GlobalTotalDS.Tables["GlobalTestModelParamterList"].Select("PID=" + myGlobalPrmtrPID); //myPrmtrPID 140528
                MyPrmtrIndexOK = new bool[CurrModelLst.Length];
                if (blnAddNew)
                {                    
                    for (int i = 0; i < CurrModelLst.Length; i++)
                    {
                        MyPrmtrIndexOK[i] = false;
                        PNInfo.myTestPrmtrAddOKFlag = false;
                    }                    
                }
                else
                {   
                    //所有的testparameter必须从Global表中载入!
                    //若Global中有删除部分parameter则需要将TopoTestParameter中的对应部分删除 140529 TBD
                    //CurrModelLst = PNInfo.TopoToatlDS.Tables["TopoTestParameter"].Select("PID=" + myPrmtrPID);
                    for (int i = 0; i < CurrModelLst.Length; i++) //需要判定是否Global有新增项目!
                    {
                        MyPrmtrIndexOK[i] = currPrmtrExisted(PNInfo.TopoToatlDS.Tables["TopoTestParameter"], CurrModelLst[i]["ItemName"].ToString());
                        
                    }
                }
                foreach (DataRow dr in CurrModelLst)
                {
                    currlst.Items.Add(dr["ItemName"]);
                }
                //myNewModelcount = CurrModelLst.Length;


                AddChkArry(CurrModelLst.Length); // 140530_1
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
                mytip.SetToolTip(currlst, "The prmtrs of current testModel");
                mytip.SetToolTip(lblChanged, "The prmtrs has existed?-> V=existed!");
                mytip.SetToolTip(btnFinish, "Finish and return");
                mytip.SetToolTip(cboValue, "set the itemValue!");
                mytip.SetToolTip(cboLowLimit, "Specmin,default=-32768");
                mytip.SetToolTip(cboUpperLimit, "Specmax,default=32767");
                mytip.SetToolTip(cboFailbreak, "Stop test when result is fail?0:not stop,1:stop");
                mytip.SetToolTip(cboLogRecord, "Save test logs?0:Not Save,1:Save");
                mytip.SetToolTip(cboDataRecord, "Save test result to server?0:Not Save,1:Save");
                mytip.SetToolTip(cboSpecific, "Verify result is in spec range?0:Not Verify,1:Verify");

                mytip.ShowAlways = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void AddChkArry(int Length)
        {
            try
            {
                this.chkOK.Visible = false;
                this.SuspendLayout();
                myChkArry = new CheckBox[Length];
                int x = 18, y = 21;
                for (int i = 0; i < Length; i++)
                {
                    myChkArry[i] = new CheckBox();
                    myChkArry[i].Location = new Point(x, y + 12 * i);
                    myChkArry[i].Enabled = false;

                    myChkArry[i].Name = "chkOK" + i.ToString();
                    myChkArry[i].Size = new System.Drawing.Size(12, 10);
                    this.Controls.Add(myChkArry[i]);

                    if (MyPrmtrIndexOK[i])  //140530_4
                    {
                        myChkArry[i].CheckState = CheckState.Checked;
                    }
                }
                this.ResumeLayout(false);
                this.PerformLayout();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
        bool currPrmtrExisted(DataTable mydt,string filterString)
        { 
            bool result=false;
            try
            {
                DataRow[] myROWS = mydt.Select("ItemName='" + filterString + "' and PID=" + myPrmtrPID);
                if (myROWS.Length == 1)
                {
                    result = true;
                }
                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return result;
            }
        }

        private void currlst_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (currlst.SelectedIndex != -1)
                {
                    if (blnFstIndexChangeOK==false)
                    {
                        mylastIndex = currlst.SelectedIndex;
                        blnFstIndexChangeOK = true;
                    }
                    if (mylastIndex != currlst.SelectedIndex && mylastIndex !=-1)
                    {
                        saveCurrPrmtr(); //若当前选择的Inde改变,则先保存前个资料!
                        mylastIndex = currlst.SelectedIndex;
                    }
                }
            }             
            catch (Exception ex)
            {
                MessageBox.Show( "Error! \n" +ex.ToString());                
            }
            
            try
            {
                if (currlst.SelectedIndex != -1)
                {
                    getInfoFromDT(PNInfo.TopoToatlDS.Tables["TopoTestParameter"], currlst.SelectedIndex);
                }
                else
                {
                    MessageBox.Show("Pls select a item first!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Get papameter data fail! \n" + ex.ToString());
            }
        }
        void gerGlobalDTInfo(string filterString)
        {
            try
            {
                DataRow[] GlobalModelLst = PNInfo.GlobalTotalDS.Tables["GlobalTestModelParamterList"].Select(filterString);

                if (GlobalModelLst.Length == 1)
                {
                    this.txtGlobalPrmtrName.Text = GlobalModelLst[0]["ItemName"].ToString();
                    this.txtGlobalPrmtrType.Text = GlobalModelLst[0]["ItemType"].ToString();
                    this.txtGlobalPrmtrDirection.Text = GlobalModelLst[0]["Direction"].ToString();
                    this.txtGlobalPrmtrValue.Text = GlobalModelLst[0]["ItemValue"].ToString();
                    this.txtGlobalPrmtrLowLimit.Text = GlobalModelLst[0]["SpecMin"].ToString();
                    this.txtGlobalPrmtrUpperLimit.Text = GlobalModelLst[0]["SpecMax"].ToString();
                    this.txtGlobalPrmtrSpecific.Text = GlobalModelLst[0]["ItemSpecific"].ToString();
                    this.txtGlobalPrmtrLogRecord.Text = GlobalModelLst[0]["LogRecord"].ToString();
                    this.txtGlobalPrmtrFailbreak.Text = GlobalModelLst[0]["Failbreak"].ToString();
                    this.txtGlobalPrmtrDataRecord.Text = GlobalModelLst[0]["DataRecord"].ToString();

                    this.cboItemName.Items.Add(GlobalModelLst[0]["ItemName"].ToString());
                    this.cboItemType.Items.Add(GlobalModelLst[0]["ItemType"].ToString());
                    this.cboDirection.Items.Add(GlobalModelLst[0]["Direction"].ToString());
                    this.cboValue.Items.Add(GlobalModelLst[0]["ItemValue"].ToString());
                    this.cboLowLimit.Items.Add(GlobalModelLst[0]["SpecMin"].ToString());
                    this.cboUpperLimit.Items.Add(GlobalModelLst[0]["SpecMax"].ToString());
                    this.cboSpecific.Items.Add(GlobalModelLst[0]["ItemSpecific"].ToString());
                    this.cboLogRecord.Items.Add(GlobalModelLst[0]["LogRecord"].ToString());
                    this.cboFailbreak.Items.Add(GlobalModelLst[0]["Failbreak"].ToString());
                    this.cboDataRecord.Items.Add(GlobalModelLst[0]["DataRecord"].ToString());
                    if (PNInfo.GlobalTotalDS.Tables["GlobalTestModelParamterList"].Columns.Contains("ItemDescription")) //141126_0
                    {
                        txtPrmtrDescription.Text = GlobalModelLst[0]["ItemDescription"].ToString();
                    }
                }
                else
                {
                    MessageBox.Show("Get Global papameter data fail!" + GlobalModelLst.Length + (" records existed; \n filterString--> " + filterString));
                    clearGlobalLst();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        void clearGlobalLst()
        {
            try
            {
                this.txtGlobalPrmtrName.Clear();
                this.txtGlobalPrmtrType.Clear(); ;
                this.txtGlobalPrmtrDirection.Clear();
                this.txtGlobalPrmtrValue.Clear();
                this.txtGlobalPrmtrLowLimit.Clear();
                this.txtGlobalPrmtrUpperLimit.Clear();
                this.txtGlobalPrmtrSpecific.Clear();
                this.txtGlobalPrmtrLogRecord.Clear();
                this.txtGlobalPrmtrFailbreak.Clear();
                this.txtGlobalPrmtrDataRecord.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void clearCurrLst()
        {
            try
            {
                this.cboItemName.Items.Clear();
                this.cboItemType.Items.Clear();
                this.cboDirection.Items.Clear();
                this.cboValue.Items.Clear();
                this.cboLowLimit.Items.Clear();
                this.cboUpperLimit.Items.Clear();
                this.cboSpecific.Items.Clear();
                this.cboLogRecord.Items.Clear();
                this.cboFailbreak.Items.Clear();
                this.cboDataRecord.Items.Clear();
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
                long GlobalPrmtrInfoPID ;

                clearGlobalLst();
                clearCurrLst();

                string PIDString = PNInfo.getDTColumnInfo(PNInfo.GlobalTotalDS.Tables["GlobalAllTestModelList"], "ID", "ItemName='" + ItemName + "'");
                if (PIDString.Trim().Length > 0)
                {
                    GlobalPrmtrInfoPID = Convert.ToInt64(PIDString);
                    gerGlobalDTInfo("ItemName='" + filterString + "' and PID=" + GlobalPrmtrInfoPID); //先载入Global信息!
                }
                else
                {
                    
                }
                
                DataRow[] myROWS = mydt.Select("ItemName='" + filterString + "' and PID=" + myPrmtrPID );
                if (myROWS.Length == 1)
                {
                    this.cboItemName.Items.Add(myROWS[0]["ItemName"].ToString());
                    this.cboItemType.Items.Add(myROWS[0]["ItemType"].ToString());
                    this.cboDirection.Items.Add(myROWS[0]["Direction"].ToString());
                    this.cboValue.Items.Add(myROWS[0]["ItemValue"].ToString());
                    this.cboLowLimit.Items.Add(myROWS[0]["SpecMin"].ToString());
                    this.cboUpperLimit.Items.Add(myROWS[0]["SpecMax"].ToString());
                    this.cboSpecific.Items.Add(myROWS[0]["ItemSpecific"].ToString());
                    this.cboLogRecord.Items.Add(myROWS[0]["LogRecord"].ToString());
                    this.cboFailbreak.Items.Add(myROWS[0]["Failbreak"].ToString());
                    this.cboDataRecord.Items.Add(myROWS[0]["DataRecord"].ToString());
                    
                }
                else
                {
                    if (blnAddNew)
                    {
                        //myNewID= PNInfo.mylastIDTestPrmtr + 1;
                    }
                    else
                    {
                        MessageBox.Show("Data of current item not existed!Pls confirm! System will add this itemName as a new one!"
                            + myROWS.Length + (" records existed; \n filterString--> ItemName='" + filterString + "' and PID=" + myPrmtrPID + ""));
                        //clearCurrLst();
                        //return;
                    }
                }
                if (this.cboItemName.Items.Count > 0)
                {
                    this.cboItemName.SelectedIndex = this.cboItemName.Items.Count - 1;
                    this.cboItemType.SelectedIndex = this.cboItemType.Items.Count - 1;
                    this.cboDirection.SelectedIndex = this.cboDirection.Items.Count - 1;
                    this.cboValue.SelectedIndex = this.cboValue.Items.Count - 1;
                    this.cboLowLimit.SelectedIndex = this.cboLowLimit.Items.Count - 1;
                    this.cboUpperLimit.SelectedIndex = this.cboUpperLimit.Items.Count - 1;
                    this.cboSpecific.SelectedIndex = this.cboSpecific.Items.Count - 1;
                    this.cboLogRecord.SelectedIndex = this.cboLogRecord.Items.Count - 1;
                    this.cboFailbreak.SelectedIndex = this.cboFailbreak.Items.Count - 1;
                    this.cboDataRecord.SelectedIndex = this.cboDataRecord.Items.Count - 1;
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                clearGlobalLst();
                clearCurrLst();
                return;
            }
        }

        bool EditInfoForDT(DataTable mydt)
        {
            bool retrueValue = false;
            try
            {
                string filterString = this.cboItemName.Text.ToString();
                if (PNInfo.checkItemLength(filterString + "-->ItemValue", cboValue.Text, 50))
                {
                    MyPrmtrIndexOK[mylastIndex] = false;
                    myChkArry[mylastIndex].CheckState = CheckState.Unchecked;
                    return false;
                }
                
                DataRow[] myROWS = mydt.Select("ItemName='" + filterString + "' and PID=" + myPrmtrPID + "");
                if (myROWS.Length == 1)
                {
                    //blnAddNew = false;

                    //ID	PID	Name	Type	Direction	Value	SpecMin	SpecMax	
                    //Specific	LogRecord	Failbreak	DataRecord
                    //无需编辑信息,固定值!
                    //myROWS[0]["ID"] = "";
                    //myROWS[0]["PID"] = "";
                    myROWS[0]["ItemName"] = this.cboItemName.Text.ToString();
                    myROWS[0]["ItemType"] = this.cboItemType.Text.ToString();
                    myROWS[0]["Direction"] = this.cboDirection.Text.ToString();
                    myROWS[0]["ItemValue"] = this.cboValue.Text.ToString();
                    myROWS[0]["SpecMin"] = this.cboLowLimit.Text.ToString();
                    myROWS[0]["SpecMax"] = this.cboUpperLimit.Text.ToString();
                    myROWS[0]["ItemSpecific"] = this.cboSpecific.Text.ToString();
                    myROWS[0]["LogRecord"] = this.cboLogRecord.Text.ToString();
                    myROWS[0]["Failbreak"] = this.cboFailbreak.Text.ToString();
                    myROWS[0]["DataRecord"] = this.cboDataRecord.Text.ToString();
                    retrueValue = true;
                    if ((cboValue.Text.ToString().Trim().Length == 0
                        || this.cboLowLimit.Text.ToString().Trim().Length == 0
                        || this.cboUpperLimit.Text.ToString().Trim().Length == 0)                       
                        && cboDirection.Text.ToString().ToUpper().Trim()=="INPUT".ToUpper()                        
                        )
                    {   
                        //140604_2
                        MyPrmtrIndexOK[mylastIndex] = false;
                        myChkArry[mylastIndex].CheckState = CheckState.Unchecked;
                    }
                    else
                    {
                        MyPrmtrIndexOK[mylastIndex] = true;
                        myChkArry[mylastIndex].CheckState = CheckState.Checked;
                    }
                    return retrueValue;
                }
                else if  (myROWS.Length == 0)
                {
                    //blnAddNew = true;

                    DataRow myNewRow = mydt.NewRow();
                    myNewRow.BeginEdit();
                    myNewRow["ID"] = PNInfo.mylastIDTestPrmtr+1;
                    myNewRow["PID"] = this.myPrmtrPID.ToString();
                    myNewRow["ItemName"] = this.cboItemName.Text.ToString();
                    myNewRow["ItemType"] = this.cboItemType.Text.ToString();
                    myNewRow["Direction"] = this.cboDirection.Text.ToString();
                    myNewRow["ItemValue"] = this.cboValue.Text.ToString();
                    myNewRow["SpecMin"] = this.cboLowLimit.Text.ToString();
                    myNewRow["SpecMax"] = this.cboUpperLimit.Text.ToString();
                    myNewRow["ItemSpecific"] = this.cboSpecific.Text.ToString();
                    myNewRow["LogRecord"] = this.cboLogRecord.Text.ToString();
                    myNewRow["Failbreak"] = this.cboFailbreak.Text.ToString();
                    myNewRow["DataRecord"] = this.cboDataRecord.Text.ToString();
                    mydt.Rows.Add(myNewRow);

                    myNewRow.EndEdit();
                    PNInfo.mylastIDTestPrmtr++;
                    PNInfo.myAddCountTestPrmtr++;

                    if ((cboValue.Text.ToString().Trim().Length == 0
                       || this.cboLowLimit.Text.ToString().Trim().Length == 0
                       || this.cboUpperLimit.Text.ToString().Trim().Length == 0)
                       && cboDirection.Text.ToString().ToUpper().Trim() == "INPUT".ToUpper()
                       )
                    {
                        //140604_2
                        MyPrmtrIndexOK[mylastIndex] = false;
                        myChkArry[mylastIndex].CheckState = CheckState.Unchecked;
                    }
                    else
                    {
                        MyPrmtrIndexOK[mylastIndex] = true;
                        myChkArry[mylastIndex].CheckState = CheckState.Checked;
                    }
                    retrueValue = true;
                    return retrueValue;
                }
                else
                {
                    MessageBox.Show("Error !" + myROWS.Length + (" records existed; \n filterString--> ItemName='" + filterString + "' and PID=" + myPrmtrPID + ""));
                    return retrueValue;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return retrueValue;
            }
        }

        void ISAddNewModelOK()
        {
            try
            {
                this.btnFinish.Enabled = true;
                for (int i = 0; i < this.currlst.Items.Count; i++)
                {
                    if (MyPrmtrIndexOK[i] == false)
                    {
                        //this.btnFinish.Enabled = false;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void saveCurrPrmtr()
        {
            try
            {
                bool result = EditInfoForDT(PNInfo.TopoToatlDS.Tables["TopoTestParameter"]);
                if (result)
                {
                    PNInfo.ISNeedUpdateflag = true; //140603_2
                    ISAddNewModelOK();  //判定是否每个参数新增完成!
                }
                else
                {
                    //this.btnFinish.Enabled = false; //140603_0
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        
        private void btnFinish_Click(object sender, EventArgs e)
        {
            try
            {
                //先保存最后一笔资料
                //然后判断所有资料是否已经完成
                //是否执行关闭form
                if (currlst.SelectedIndex != -1 && mylastIndex != -1)
                {
                    saveCurrPrmtr();
                }

                for (int i = 0; i < currlst.Items.Count; i++)
                {
                    if (MyPrmtrIndexOK[i] = false || myChkArry[i].CheckState != CheckState.Checked)
                    {
                        MessageBox.Show("Some data is incomplete! Pls confirm [WithData?] <All item must be with 'V'>-->" + currlst.Items[i]);
                        return;
                    }
                }


                blnAddNew = false;
                PNInfo.myTestModelAddOKFlag = true; //140530_0
                PNInfo.myTestPrmtrAddOKFlag = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void TestParameterInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.blnAddNew)
            {
                DialogResult myResult = MessageBox.Show(
                    "Data is incomplete, the system will delete the current maintenance item information !",
                    "Notice:",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button1);
                try
                {
                    string queryCMD = "PID=" + this.myPrmtrPID;
                    int myExistCount = PNInfo.currPrmtrCountExisted(PNInfo.TopoToatlDS.Tables["TopoTestParameter"], queryCMD);
                    if (myResult == DialogResult.OK)
                    {
                        if (myExistCount > 0)
                        {
                            PNInfo.DeleteItemForDT(PNInfo.TopoToatlDS.Tables["TopoTestParameter"], queryCMD);
                            PNInfo.mylastIDTestPrmtr = PNInfo.mylastIDTestPrmtr - myExistCount;
                        }
                        blnAddNew = false;

                        PNInfo.myTestPrmtrAddOKFlag = true;    //140529_1
                        
                        this.Dispose();
                    }
                    else 
                    {
                        e.Cancel = true;    //141104_0
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }            
        }

        private void cboUpperLimit_Leave(object sender, EventArgs e)
        {   //byte double UInt16

            try
            {
                if (cboDirection.Text.ToString().ToUpper().Trim() == "INPUT".ToUpper()
                    &&
                    (cboItemType.Text.ToString().ToUpper() == "UInt16".ToUpper()
                    || cboItemType.Text.ToString().ToUpper() == "double".ToUpper()
                    || cboItemType.Text.ToString().ToUpper() == "byte".ToUpper()
                    ))
                {
                    if (PNInfo.checkTypeOK(cboUpperLimit.Text.ToString(), this.cboItemType.Text.ToString()) == false)
                    {
                        cboUpperLimit.Focus();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void cboLowLimit_Leave(object sender, EventArgs e)
        {
            try
            {
                if (cboDirection.Text.ToString().ToUpper().Trim() == "INPUT".ToUpper()
                    &&
                    (cboItemType.Text.ToString().ToUpper() == "UInt16".ToUpper()
                    || cboItemType.Text.ToString().ToUpper() == "double".ToUpper()
                    || cboItemType.Text.ToString().ToUpper() == "byte".ToUpper()
                    ))
                {
                    if (PNInfo.checkTypeOK(cboLowLimit.Text.ToString(), this.cboItemType.Text.ToString()) == false)
                    {
                        cboLowLimit.Focus();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void cboValue_Leave(object sender, EventArgs e)
        {
            try
            {
                if (cboDirection.Text.ToString().ToUpper().Trim() == "INPUT".ToUpper()
                    &&
                    (cboItemType.Text.ToString().ToUpper() == "UInt16".ToUpper()
                    || cboItemType.Text.ToString().ToUpper() == "double".ToUpper()
                    || cboItemType.Text.ToString().ToUpper() == "byte".ToUpper()
                    ))
                {
                    if (PNInfo.checkTypeOK(cboValue.Text.ToString(), this.cboItemType.Text.ToString()) == false)
                    {
                        cboValue.Focus();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void cboSpecific_Leave(object sender, EventArgs e)  //140604_3
        {
            try
            {
                if (PNInfo.ISNotInSpec("Within spec?", cboSpecific.Text.ToString(), 0, 1))
                {
                    cboSpecific.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void cboLogRecord_Leave(object sender, EventArgs e) //140604_3
        {
            try
            {
                if (PNInfo.ISNotInSpec("Save Logs?", cboLogRecord.Text.ToString(), 0, 1))
                {
                    cboLogRecord.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void cboFailbreak_Leave(object sender, EventArgs e) //140604_3
        {
            try
            {
                if (PNInfo.ISNotInSpec("Fail break?", cboFailbreak.Text.ToString(), 0, 1))
                {
                    cboFailbreak.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void cboDataRecord_Leave(object sender, EventArgs e)    //140604_3
        {
            try
            {
                if (PNInfo.ISNotInSpec("Save FMTdata?", cboDataRecord.Text.ToString(), 0, 1))
                {
                    cboDataRecord.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}

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
                mytip.SetToolTip(currlst, "当前参数列表信息");
                mytip.SetToolTip(lblChanged, "当前参数是否已经存在-> V 表示已经存在值!");
                mytip.SetToolTip(btnFinish, "当前参数维护完成准备关闭该页面");
                mytip.SetToolTip(cboValue, "设置的参数值!");
                mytip.SetToolTip(cboLowLimit, "当前参数信息预设规格下限值,若无则默认为-32768");
                mytip.SetToolTip(cboUpperLimit, "当前参数信息预设规格下限值,若无则默认为-32768");
                mytip.SetToolTip(cboFailbreak, "当运行中出现调整/测试失败停止运行,0表示不停止,1表示停止");
                mytip.SetToolTip(cboLogRecord, "保存当前的调整信息,0表示不保存,1表示保存");
                mytip.SetToolTip(cboDataRecord, "保存当前的测试结果到服务器,0表示不保存,1表示保存");
                mytip.SetToolTip(cboSpecific, "是否对规格上下限进行比对,若不在规格内则结果为失败,0表示不比对,1表示比对");

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
                MessageBox.Show( "执行保存时发生错误! \n" +ex.ToString());                
            }
            
            try
            {
                if (currlst.SelectedIndex != -1)
                {
                    getInfoFromDT(PNInfo.TopoToatlDS.Tables["TopoTestParameter"], currlst.SelectedIndex);
                }
                else
                {
                    MessageBox.Show("请选择对应内容!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("执行获取数据时发生错误! \n" + ex.ToString());
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
                    this.txtGlobalPrmtrLowLimit.Text = GlobalModelLst[0]["DefaultLowLimit"].ToString();
                    this.txtGlobalPrmtrUpperLimit.Text = GlobalModelLst[0]["DefaultUpperLimit"].ToString();
                    this.txtGlobalPrmtrSpecific.Text = GlobalModelLst[0]["ItemSpecific"].ToString();
                    this.txtGlobalPrmtrLogRecord.Text = GlobalModelLst[0]["LogRecord"].ToString();
                    this.txtGlobalPrmtrFailbreak.Text = GlobalModelLst[0]["Failbreak"].ToString();
                    this.txtGlobalPrmtrDataRecord.Text = GlobalModelLst[0]["DataRecord"].ToString();

                    this.cboItemName.Items.Add(GlobalModelLst[0]["ItemName"].ToString());
                    this.cboItemType.Items.Add(GlobalModelLst[0]["ItemType"].ToString());
                    this.cboDirection.Items.Add(GlobalModelLst[0]["Direction"].ToString());
                    this.cboValue.Items.Add(GlobalModelLst[0]["ItemValue"].ToString());
                    this.cboLowLimit.Items.Add(GlobalModelLst[0]["DefaultLowLimit"].ToString());
                    this.cboUpperLimit.Items.Add(GlobalModelLst[0]["DefaultUpperLimit"].ToString());
                    this.cboSpecific.Items.Add(GlobalModelLst[0]["ItemSpecific"].ToString());
                    this.cboLogRecord.Items.Add(GlobalModelLst[0]["LogRecord"].ToString());
                    this.cboFailbreak.Items.Add(GlobalModelLst[0]["Failbreak"].ToString());
                    this.cboDataRecord.Items.Add(GlobalModelLst[0]["DataRecord"].ToString());
                }
                else
                {
                    MessageBox.Show("载入Global资料有误!请确认!!! 共有" + GlobalModelLst.Length + ("条记录; \n 条件--> " + filterString));
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
                    this.cboLowLimit.Items.Add(myROWS[0]["DefaultLowLimit"].ToString());
                    this.cboUpperLimit.Items.Add(myROWS[0]["DefaultUpperLimit"].ToString());
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
                        MessageBox.Show("未发现当前选择资料!请确认是否为新增的参数项目!!! 共有" 
                            + myROWS.Length + ("条记录; \n 条件--> ItemName='" + filterString + "' and PID=" + myPrmtrPID + ""));
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
                DataRow[] myROWS = mydt.Select("ItemName='" + filterString + "' and PID=" + myPrmtrPID + "");
                if (myROWS.Length == 1)
                {
                    //blnAddNew = false;

                    //ID	PID	Name	Type	Direction	Value	DefaultLowLimit	DefaultUpperLimit	
                    //Specific	LogRecord	Failbreak	DataRecord
                    //无需编辑信息,固定值!
                    //myROWS[0]["ID"] = "";
                    //myROWS[0]["PID"] = "";
                    myROWS[0]["ItemName"] = this.cboItemName.Text.ToString();
                    myROWS[0]["ItemType"] = this.cboItemType.Text.ToString();
                    myROWS[0]["Direction"] = this.cboDirection.Text.ToString();
                    myROWS[0]["ItemValue"] = this.cboValue.Text.ToString();
                    myROWS[0]["DefaultLowLimit"] = this.cboLowLimit.Text.ToString();
                    myROWS[0]["DefaultUpperLimit"] = this.cboUpperLimit.Text.ToString();
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
                    myNewRow["DefaultLowLimit"] = this.cboLowLimit.Text.ToString();
                    myNewRow["DefaultUpperLimit"] = this.cboUpperLimit.Text.ToString();
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
                    MessageBox.Show("资料有误!请确认!!! 共有" + myROWS.Length + ("条记录; \n 条件--> ItemName='" + filterString + "' and PID=" + myPrmtrPID + ""));
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
                        MessageBox.Show("发现项目参数未保存! 请确认-->" + currlst.Items[i]);
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
                    "尚未完成资料维护!提前退出将可能无法保证资料完整,系统将自动删除当前维护项目资料!",
                    "注意:",
                    MessageBoxButtons.OK,
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
                    else //140604_1
                    {
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
                if (PNInfo.ISNotInSpec("比 对 规 格", cboSpecific.Text.ToString(), 0, 1))
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
                if (PNInfo.ISNotInSpec("存储调整信息", cboLogRecord.Text.ToString(), 0, 1))
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
                if (PNInfo.ISNotInSpec("超出规格停止", cboFailbreak.Text.ToString(), 0, 1))
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
                if (PNInfo.ISNotInSpec("测试结果存档", cboDataRecord.Text.ToString(), 0, 1))
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

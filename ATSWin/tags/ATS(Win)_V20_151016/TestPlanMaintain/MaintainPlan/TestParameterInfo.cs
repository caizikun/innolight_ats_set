using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;

namespace Maintain
{
    public partial class TestParameterInfo : Form
    {
        bool[] MyPrmtrIndexOK;
        public bool blnAddNew = false;        
        public string ItemName = "";       
        public long myPrmtrPID = -1;
        public long myGlobalPrmtrPID = -1; 

        long origmylastIDModelPrmtr = MainForm.mylastIDTestPrmtr;
        long origmynewIDModelPrmtr = MainForm.mynewIDTestPrmtr;
        long origmyDeletedCountModelPrmtr = MainForm.myDeletedCountTestPrmtr;

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
                myGlobalPrmtrPID = Convert.ToInt64(MainForm.getDTColumnInfo(MainForm.GlobalTotalDS.Tables[3], "ID", "ItemName = '" + ItemName + "'"));
                
                DataRow[] CurrModelLst;

                CurrModelLst = MainForm.GlobalTotalDS.Tables["GlobalTestModelParamterList"].Select("PID=" + myGlobalPrmtrPID); //myPrmtrPID 140528
                MyPrmtrIndexOK = new bool[CurrModelLst.Length];
                if (blnAddNew)
                {                    
                    for (int i = 0; i < CurrModelLst.Length; i++)
                    {
                        MyPrmtrIndexOK[i] = false;
                        MainForm.myTestPrmtrAddOKFlag = false;
                    }                    
                }
                else
                {   
                    //所有的testparameter必须从Global表中载入!
                    //若Global中有删除部分parameter则需要将TopoTestParameter中的对应部分删除 140529 TBD
                    //CurrModelLst = MainForm.TopoToatlDS.Tables["TopoTestParameter"].Select("PID=" + myPrmtrPID);
                    for (int i = 0; i < CurrModelLst.Length; i++) //需要判定是否Global有新增项目!
                    {
                        MyPrmtrIndexOK[i] = currPrmtrExisted(MainForm.TopoToatlDS.Tables["TopoTestParameter"], CurrModelLst[i]["ItemName"].ToString());
                        
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
                    getInfoFromDT(MainForm.TopoToatlDS.Tables["TopoTestParameter"], currlst.SelectedIndex);
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
        void getGlobalDTInfo(string filterString)
        {
            try
            {
                DataRow[] GlobalModelLst = MainForm.GlobalTotalDS.Tables["GlobalTestModelParamterList"].Select(filterString);

                if (GlobalModelLst.Length == 1)
                {
                    this.txtGlobalPrmtrName.Text = GlobalModelLst[0]["ItemName"].ToString();
                    this.txtGlobalPrmtrType.Text = GlobalModelLst[0]["ItemType"].ToString();                    
                    this.txtGlobalPrmtrValue.Text = GlobalModelLst[0]["ItemValue"].ToString();                    
                    this.cboItemName.Items.Add(GlobalModelLst[0]["ItemName"].ToString());
                    this.cboItemType.Items.Add(GlobalModelLst[0]["ItemType"].ToString());                    
                    this.cboValue.Items.Add(GlobalModelLst[0]["ItemValue"].ToString());
                    
                    if (MainForm.GlobalTotalDS.Tables["GlobalTestModelParamterList"].Columns.Contains("ItemDescription")) //141126_0
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
                this.txtGlobalPrmtrType.Clear();               
                this.txtGlobalPrmtrValue.Clear();
                if (MainForm.GlobalTotalDS.Tables["GlobalTestModelParamterList"].Columns.Contains("ItemDescription")) //141126_0
                {
                    txtPrmtrDescription.Clear();
                }
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
                this.cboValue.Items.Clear();
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

                string PIDString = MainForm.getDTColumnInfo(MainForm.GlobalTotalDS.Tables["GlobalAllTestModelList"], "ID", "ItemName='" + ItemName + "'");
                if (PIDString.Trim().Length > 0)
                {
                    GlobalPrmtrInfoPID = Convert.ToInt64(PIDString);
                    getGlobalDTInfo("ItemName='" + filterString + "' and PID=" + GlobalPrmtrInfoPID); //先载入Global信息!
                }
                else
                {
                    
                }
                
                DataRow[] myROWS = mydt.Select("ItemName='" + filterString + "' and PID=" + myPrmtrPID );
                if (myROWS.Length == 1)
                {
                    this.cboItemName.Items.Add(myROWS[0]["ItemName"].ToString());
                    this.cboItemType.Items.Add(myROWS[0]["ItemType"].ToString());
                    this.cboValue.Items.Add(myROWS[0]["ItemValue"].ToString());
                                        
                }
                else
                {
                    if (blnAddNew)
                    {
                        //myNewID= MainForm.mylastIDTestPrmtr + 1;
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
                    this.cboValue.SelectedIndex = this.cboValue.Items.Count - 1;                
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
                if (MainForm.checkItemLength(filterString + "-->ItemValue", cboValue.Text, 50))
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
                    myROWS[0]["ItemValue"] = this.cboValue.Text.ToString();
                    
                    retrueValue = true;
                    if (cboValue.Text.ToString().Trim().Length == 0)
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
                    myNewRow["ID"] = MainForm.mylastIDTestPrmtr+1;
                    myNewRow["PID"] = this.myPrmtrPID.ToString();
                    myNewRow["ItemName"] = this.cboItemName.Text.ToString();
                    myNewRow["ItemType"] = this.cboItemType.Text.ToString();
                    myNewRow["ItemValue"] = this.cboValue.Text.ToString();
                   
                    mydt.Rows.Add(myNewRow);

                    myNewRow.EndEdit();
                    MainForm.mylastIDTestPrmtr++;
                    MainForm.myAddCountTestPrmtr++;

                    if (cboValue.Text.ToString().Trim().Length == 0)
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
                bool result = EditInfoForDT(MainForm.TopoToatlDS.Tables["TopoTestParameter"]);
                if (result)
                {
                    MainForm.ISNeedUpdateflag = true; //140603_2
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
                MainForm.myTestModelAddOKFlag = true; //140530_0
                MainForm.myTestPrmtrAddOKFlag = true;
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
                    int myExistCount = MainForm.currPrmtrCountExisted(MainForm.TopoToatlDS.Tables["TopoTestParameter"], queryCMD);
                    if (myResult == DialogResult.OK)
                    {
                        if (myExistCount > 0)
                        {
                            MainForm.DeleteItemForDT(MainForm.TopoToatlDS.Tables["TopoTestParameter"], queryCMD);
                            MainForm.mylastIDTestPrmtr = MainForm.mylastIDTestPrmtr - myExistCount;
                        }
                        blnAddNew = false;

                        MainForm.myTestPrmtrAddOKFlag = true;    //140529_1
                        
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

      
        private void cboValue_Leave(object sender, EventArgs e)
        {
            try
            {
                if ((cboItemType.Text.ToString().ToUpper() == "UInt16".ToUpper()
                    || cboItemType.Text.ToString().ToUpper() == "double".ToUpper()
                    || cboItemType.Text.ToString().ToUpper() == "byte".ToUpper()
                    || cboItemType.Text.ToString().ToUpper() == "UInt32".ToUpper()
                    ))
                {
                    if (MainForm.checkTypeOK(this.cboValue.Text.ToString(), this.cboItemType.Text.ToString()) == false)
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
    }
}

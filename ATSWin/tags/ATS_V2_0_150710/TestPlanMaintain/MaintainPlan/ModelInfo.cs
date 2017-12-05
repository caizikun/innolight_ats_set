using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;

namespace Maintain
{
    public partial class ModelInfo : Form
    {
        public bool blnAddNewModel = false;
        public string TestCtrlName = "";
        public long PID = -1;
        public long myPrmtrPID = -1;
        public long myGlobalModelID = -1;
        public string myModelName = "";
        public long TestPlanPID = -1;

        //141030_3 未使用 删除
        //long origmylastIDTestModel = MainForm.mylastIDTestModel;
        //long origmynewIDTestModel = MainForm.mynewIDTestModel;
        //long origmyDeletedCountTestModel = MainForm.myDeletedCountTestModel;

        //long origmylastIDModelPrmtr = MainForm.mylastIDTestPrmtr;
        //long origmynewIDModelPrmtr = MainForm.mynewIDTestPrmtr;
        //long origmyDeletedCountModelPrmtr = MainForm.myDeletedCountTestPrmtr;

        int myNewModelcount = -1;
        //ConstGlobalListTables = new string[] { "GlobalAllEquipmentList", "GlobalAllEquipmentParamterList", "GlobalAllAppModelList", "GlobalAllTestModelList", "GlobalTestModelParamterList" };

        public ModelInfo()
        {
            InitializeComponent();
        }
        
        private void ModelInfo_Load(object sender, EventArgs e)
        {
            try
            {
                RefreshModelInfo();
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
               
                mytip.SetToolTip(btnAdd, "Add a new test model");
                mytip.SetToolTip(btnRemove, "Delete a test model");
                mytip.SetToolTip(btnNextPage, "Retun last form");  //"Goto Config the parameter of testModel");
                mytip.SetToolTip(btnAddEquip, "");
                mytip.SetToolTip(btnRemoveEquip, "");
                mytip.SetToolTip(btnOK, "Save Data");
                mytip.SetToolTip(chkIgnoreFlag, "<V>: this testModel will be ignored...");
                mytip.ShowAlways = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        void RefreshModelInfo()
        {
            try
            {
                //initinal GlobalEquioList ~ Current List
                txtDescription.Text = "";
                currlst.Items.Clear();
                globalistName.Items.Clear();
                cboItemName.Text = "";
                cboAppModeID.Text = "";
                txtEquipLst.Text = "";
                lstEquip.Items.Clear();
                lstSelectEquip.Items.Clear();
                txtTestCtrlName.Text = TestCtrlName;

                if (MainForm.TopoToatlDS.Tables["TopoTestModel"].Columns.Contains("FailBreak"))  //150411_0
                {
                    this.chkFailBreak.Visible = true;
                }
                else
                {
                    this.chkFailBreak.Visible = false;
                }

                DataRow[] GlobalModelLst = MainForm.GlobalTotalDS.Tables["GlobalAllTestModelList"].Select();
                // = MainForm.mySqlIO.GetDataTable("", "");
                foreach (DataRow dr in GlobalModelLst)
                {
                    globalistName.Items.Add(dr["ItemName"]);
                }

                DataRow[] CurrModelLst = MainForm.TopoToatlDS.Tables["TopoTestModel"].Select("PID=" + PID + "");
                // = MainForm.mySqlIO.GetDataTable("", "");
                foreach (DataRow dr in CurrModelLst)
                {
                    currlst.Items.Add(dr["ItemName"]);
                }
                myNewModelcount = CurrModelLst.Length;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //******************************************************
        //TBD~
        private void btnAddEquip_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.lstEquip.SelectedIndex != -1)
                {
                    //当点击Add 按钮后讲globalistName选中的项目新增到currlst[先移除冒号!]
                    string NewName = lstEquip.SelectedItem.ToString();

                    for (int i = 0; i < this.lstSelectEquip.Items.Count; i++)
                    {
                        if (NewName.ToUpper().Trim() == lstSelectEquip.Items[i].ToString().ToUpper().Trim())
                        {
                            MessageBox.Show("The item has been existed :-> " + NewName);
                            return;
                        }
                    }

                    this.lstSelectEquip.Items.Add(NewName);

                    //txt Update and Refresh
                    RefreshTXTEquipLst();
                }
                else
                {
                    MessageBox.Show("Pls choose a Equipment from the Equiplist first!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void RefreshTXTEquipLst()
        {
            try
            {
                string tempEquipLst = "";
                for (int i = 0; i < this.lstSelectEquip.Items.Count; i++)
                {
                    if (i + 1 == this.lstSelectEquip.Items.Count)
                    {
                        tempEquipLst = tempEquipLst + this.lstSelectEquip.Items[i].ToString();
                    }
                    else
                    {
                        tempEquipLst = tempEquipLst + this.lstSelectEquip.Items[i].ToString() + ",";
                    }
                }
                this.txtEquipLst.Text = tempEquipLst;
                this.txtEquipLst.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void btnRemoveEquip_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.lstSelectEquip.SelectedIndex != -1)
                {
                    //当点击Add 按钮后讲globalistName选中的项目新增到currlst[先移除冒号!]
                    string NewName = lstSelectEquip.SelectedItem.ToString();
                    this.lstSelectEquip.Items.Remove(NewName);
                    //txt Update and Refresh
                    RefreshTXTEquipLst();
                    //RefreshModelInfo();  140703_1
                }
                else
                {
                    MessageBox.Show("Pls choose a Equipment from the ModelEquiplist first!");
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
                    string NewName = globalistName.SelectedItem.ToString();

                    //140527_2127 因为存在相同的仪器,故不再对此部分进行约束!
                    for (int i=0;i<currlst.Items.Count;i++)
                    {
                        if (NewName.ToUpper().Trim() == currlst.Items[i].ToString().ToUpper().Trim())
                        {
                            MessageBox.Show("this TestModel has been existed:-> " + NewName);

                            return;
                        }
                    }

                    blnAddNewModel = true;
                    MainForm.myTestModelAddOKFlag = false;
                    myNewModelcount++;
                    currlst.Items.Add(NewName);

                    this.cboItemName.Text = NewName;
                    this.cboAppModeID.Text = MainForm.getNextTablePIDFromDT(MainForm.GlobalTotalDS.Tables["GlobalAllTestModelList"], "ItemName='" + NewName + "'").ToString();
                    this.txtDescription.Text = MainForm.getDTColumnInfo(MainForm.GlobalTotalDS.Tables["GlobalAllTestModelList"], "ItemDescription", "ItemName='" + NewName + "'").ToString();
                    int myNewRowIndex = (currlst.Items.Count - 1);
                    currlst.SelectedIndex = myNewRowIndex;

                    btnAdd.Enabled = false;
                    currlst.Enabled = false;
                    btnOK.Enabled = true;
                    btnNextPage.Enabled = false;
                }
                else
                {   
                    MessageBox.Show("Pls choose a Model name from GlobalModelList first");
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
                DataRow[] myROWS = mydt.Select("ItemName='" + filterString + "' and PID=" + PID + "");
                if (myROWS.Length == 1)
                {
                    this.cboItemName.Text = myROWS[0]["ItemName"].ToString();
                    this.cboAppModeID.Text = myROWS[0]["AppModeID"].ToString();
                    this.chkIgnoreFlag.Checked =Convert.ToBoolean(myROWS[0]["IgnoreFlag"].ToString());  //141028_0
                    myPrmtrPID = System.Convert.ToInt64(myROWS[0]["ID"]);
                    if (mydt.Columns.Contains("FailBreak"))  //150411_0
                    {
                        this.chkFailBreak.Checked = Convert.ToBoolean(myROWS[0]["FailBreak"].ToString());
                    }
                    this.txtDescription.Text = MainForm.getDTColumnInfo(MainForm.GlobalTotalDS.Tables["GlobalAllTestModelList"], "ItemDescription", "ItemName='" + filterString + "'").ToString();                    
                }
                else
                {
                    MessageBox.Show("Error! " + myROWS.Length + (" records existed; \n filterString--> ItemName='" + filterString + "' and PID=" + PID + ""));
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }
        }

        bool EditInfoForDT(DataTable mydt)
        {
            bool result = false;
            try
            {
                string filterString = this.cboItemName.Text.ToString();
                DataRow[] myROWS = mydt.Select("ItemName='" + filterString + "' and PID=" + PID + "");
                if (myROWS.Length == 1)
                {
                    if (this.blnAddNewModel)
                    {
                        MessageBox.Show("Add new Error! " + myROWS.Length + (" records existed; \n filterString-->--> ItemName='" + filterString + "' and PID=" + PID + ""));
                        return result;
                    }
                    //无需编辑信息,固定值!
                    //myROWS[0]["ItemName"] = this.cboItemName.Text.ToString();
                    //myROWS[0]["AppModeID"] = this.cboAppModeID.Text.ToString();
                    myROWS[0]["EquipmentList"] = this.txtEquipLst.Text.ToString();
                    myROWS[0]["IgnoreFlag"] = this.chkIgnoreFlag.Checked.ToString();    //141028_0
                    if (mydt.Columns.Contains("FailBreak"))  //150411_0
                    {
                        myROWS[0]["FailBreak"] = this.chkFailBreak.Checked;
                    }
                    result = true;
                }
                else if (this.blnAddNewModel && myROWS.Length == 0)
                {   
                    string MyNewSEQ=(MainForm.getMAXColumnsItem(MainForm.TopoToatlDS.Tables["TopoTestModel"], "SEQ", "PID=" + PID) + 1).ToString();
                    DataRow myNewRow = mydt.NewRow();
                    myNewRow.BeginEdit();
                    myNewRow["ID"] = MainForm.mylastIDTestModel + 1;
                    myNewRow["PID"] = this.PID.ToString();
                    myNewRow["ItemName"] = this.cboItemName.Text.ToString();
                    myNewRow["AppModeID"] = this.cboAppModeID.Text.ToString();
                    myNewRow["EquipmentList"] = this.txtEquipLst.Text.ToString();
                    myNewRow["Seq"] = MyNewSEQ;
                    myNewRow["IgnoreFlag"] = this.chkIgnoreFlag.Checked.ToString(); //141028_0
                    if (mydt.Columns.Contains("FailBreak"))  //150411_0
                    {
                        myNewRow["FailBreak"] = this.chkFailBreak.Checked;
                    }
                    mydt.Rows.Add(myNewRow);
                   
                    myNewRow.EndEdit();                    
                    MainForm.mylastIDTestModel++;
                    MainForm.myAddCountTestModel++;

                    //RefreshList();
                    int myNewRowIndex = (currlst.Items.Count - 1);
                    if (!currlst.Enabled) //140529 新增TestModel后需要将其参数维护后才能选择!
                    {
                        currlst.Enabled = true;
                        currlst.Focus();
                        currlst.SelectedIndex = myNewRowIndex;
                    }
                    //blnAddNewModel = false;  //140530_1 //140703_2
                    result = true;
                }
                else
                {
                    MessageBox.Show("Error! " + myROWS.Length + (" records existed; \n filterString-->ItemName='" + filterString + "' and PID=" + PID + ""));
                }
                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return result;
            }
        }
        
        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.currlst.SelectedIndex != -1)
                {
                    DialogResult drst = new DialogResult();

                    drst = (MessageBox.Show("Delete Model --> " + currlst.SelectedItem.ToString() +
                        "\n \n Pls choose 'Y' (是) to continue?",
                        "Notice", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2));

                    if (drst == DialogResult.Yes)
                    {
                        //若为新增的项目则必须指定当前的选择的Item
                        int CurrIndex = currlst.SelectedIndex;
                        string sName = currlst.Items[CurrIndex].ToString();
                        long myPID = PID;

                        bool resultPrmtr = MainForm.DeleteItemForDT(MainForm.TopoToatlDS.Tables["TopoTestParameter"], "PID=" + myPrmtrPID);
                        bool result = MainForm.DeleteItemForDT(MainForm.TopoToatlDS.Tables["TopoTestModel"], "PID=" + myPID + " and ItemName='" + sName + "'");
                        
                        if (result && resultPrmtr)
                        {
                            MainForm.ISNeedUpdateflag = true; //140603_2
                            currlst.Items.RemoveAt(CurrIndex);
                            MainForm.myDeletedCountTestModel++;
                            MainForm.myDeletedCountTestPrmtr++;  //140521@子信息的数据需要重新查询...TBD
                            myNewModelcount--;

                            this.blnAddNewModel = false;
                            this.btnAdd.Enabled = true;
                            currlst.Enabled = true;
                            btnOK.Enabled = true;
                            btnNextPage.Enabled = true;
                            MessageBox.Show("ItemNO: " + CurrIndex + ";Name =" + sName + "has been deleted!");
                            
                        }
                        else
                        {
                            MessageBox.Show("ItemNO: " + CurrIndex + "!Name =" + sName + "deleted Fail...");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Pls choose a TestModel first!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
       
        void getEquipLstInfo(long TestPlanPID)
        {
            try
            {
                if (TestPlanPID != -1)
                {
                    this.lstEquip.Items.Clear();
                    this.lstSelectEquip.Items.Clear();
                    DataRow[] CurrEquipLst = MainForm.TopoToatlDS.Tables["TopoEquipment"].Select("PID=" + TestPlanPID);
                    foreach (DataRow dr in CurrEquipLst)
                    {
                        this.lstEquip.Items.Add(dr["ItemName"]);
                    }
                    DataRow[] CurrSelectEquipLst = MainForm.TopoToatlDS.Tables["TopoTestModel"].Select("PID=" +PID + " and ItemName='" + myModelName + "'");
                    foreach (DataRow dr in CurrSelectEquipLst)
                    {
                        string[] SelectEquipLst = dr["EquipmentList"].ToString().Split(',');
                        foreach (string s in SelectEquipLst)
                        {
                            this.lstSelectEquip.Items.Add(s);
                        }
                    }
                    
                    RefreshTXTEquipLst();
                }
                else
                {
                    MessageBox.Show("PID= " + PID + ", not found Equipment list info!");
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
                    DataTable myPrmtrDT = new DataTable();
                    myModelName = this.currlst.SelectedItem.ToString();
                    myGlobalModelID = Convert.ToInt32(MainForm.getDTColumnInfo(MainForm.GlobalTotalDS.Tables["GlobalAllTestModelList"], "ID", "ItemName= '" + myModelName + "'")); //140529_2

                    if (!blnAddNewModel)
                    {
                        btnOK.Enabled = true;
                        btnNextPage.Enabled = true;

                        getInfoFromDT(MainForm.TopoToatlDS.Tables["TopoTestModel"], currlst.SelectedIndex);

                        //未存放至Server 此处需进行查询本机未更新的DS
                        myPrmtrPID = MainForm.getNextTablePIDFromDT(MainForm.TopoToatlDS.Tables["TopoTestModel"], "ItemName= '" + currlst.SelectedItem.ToString() + "' and PID=" + PID);

                        DataRow[] myRows = MainForm.TopoToatlDS.Tables["TopoTestParameter"].Select("PID=" + myPrmtrPID); //;("Select * from TopoTestParameter where PID=" + myPrmtrPID, "TopoTestParameter");
                        if (myRows.Length > 0)
                        {
                            TestPlanPID = Convert.ToInt32(MainForm.getDTColumnInfo(MainForm.TopoToatlDS.Tables["TopoTestControl"], "PID", "ID= " + PID));

                            //141104_1//getEquipLstInfo(TestPlanPID);
                        }
                        else
                        {
                            MessageBox.Show("Not found TopoTestParameter info of current TestModel !");
                            return;
                        }
                    }
                    else
                    {
                        btnOK.Enabled = true;
                        btnNextPage.Enabled = false;

                        myPrmtrPID = MainForm.mylastIDTestModel + 1;
                        TestPlanPID = Convert.ToInt32(MainForm.getDTColumnInfo(MainForm.TopoToatlDS.Tables["TopoTestControl"], "PID", "ID= " + PID));

                        //141104_1//getEquipLstInfo(TestPlanPID);
                        //ADD Description:......
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnNextPage_Click(object sender, EventArgs e)
        {
            this.Close();  
        }
        
        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                bool result = EditInfoForDT(MainForm.TopoToatlDS.Tables["TopoTestModel"]);

                if (result)
                {
                    MainForm.ISNeedUpdateflag = true; //140603_2
                    MainForm.myTestModelISNewFlag = false;        //140530_2
                    btnNextPage.Enabled = true;
                    MainForm.myTestModelAddOKFlag = true; //140530_0
                    //if (blnAddNewModel)   //140530_1
                    if( MainForm.myTestModelAddOKFlag == true)
                    {
                        if (blnAddNewModel) //140703_2 若为新增Model则强制维护TestParameter >>>
                        {
                            TestParameterInfo myTestParameterInfo = new TestParameterInfo();
                            myTestParameterInfo.ItemName = currlst.SelectedItem.ToString();
                            myTestParameterInfo.myPrmtrPID = Convert.ToInt64(MainForm.getDTColumnInfo(MainForm.TopoToatlDS.Tables[3], "ID", "ItemName = '" + currlst.SelectedItem.ToString() + "' and PID=" + this.PID));
                            myPrmtrPID = myTestParameterInfo.myPrmtrPID;
                            myModelName = myTestParameterInfo.ItemName;
                            myTestParameterInfo.blnAddNew = true;
                            //新增Model 对应的Prmtr的PID部分相互处理 //140529 OK
                            myTestParameterInfo.ShowDialog();

                            if (MainForm.currPrmtrCountExisted(MainForm.TopoToatlDS.Tables["TopoTestParameter"], "PID=" + myPrmtrPID)
                                == MainForm.currPrmtrCountExisted(MainForm.GlobalTotalDS.Tables["GlobalTestModelParamterList"], "PID=" + myGlobalModelID)
                                )
                            {
                                blnAddNewModel = false; //140527_00
                                btnAdd.Enabled = true;
                                currlst.Enabled = true;
                                MainForm.myTestPrmtrISNewFlag = false;        //140530_2
                                //this.Close(); //140530_4 //140703_2
                            }
                            else
                            {
                                MessageBox.Show("The records of TopoTestParameter is diffenent from The records of GlobalTestModelParamterList,data lost!" + 
                                    " \n The system will delete this TestModel"
                                    , "Notice!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                deleteNewModelONErr(); 
                            }
                        }   //140703_2 若为新增Model则强制维护TestParameter <<<
                    }
                    //this.Close();   //140530_3 //140703_2
                }
                else
                {
                    btnNextPage.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        void deleteNewModelONErr()
        {
            try
            {
                string queryCMD = "ItemName='" + this.myModelName + "' And PID=" + this.PID;
                int myExistCount = MainForm.currPrmtrCountExisted(MainForm.TopoToatlDS.Tables["TopoTestModel"], queryCMD);

                if (myExistCount > 0)
                {
                    MainForm.DeleteItemForDT(MainForm.TopoToatlDS.Tables["TopoTestModel"], queryCMD);
                    MainForm.mylastIDTestModel = MainForm.mylastIDTestModel - myExistCount;
                }
                MainForm.myTestModelAddOKFlag = true;
                MainForm.myTestPrmtrAddOKFlag = true; //140605_2
                blnAddNewModel = false; //140527_00
                btnAdd.Enabled = true;
                currlst.Enabled = true;
                RefreshModelInfo();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                RefreshModelInfo();
            }
        }

        private void ModelInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.blnAddNewModel)
            {
                blnAddNewModel = false;//141030_1
                return; //141030_1  不再删除未保存资料<万一字符串为已存在资料 可能误删>                
            }

            
        }

        private void lstEquip_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstEquip.SelectedIndex != -1)
            {
                btnAddEquip.Enabled = true;
                btnRemoveEquip.Enabled = false;
            }
        }

        private void lstSelectEquip_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnAddEquip.Enabled = false;
            btnRemoveEquip.Enabled = true;
        }

        private void chkIgnoreFlag_CheckedChanged(object sender, EventArgs e)   //141028_0
        {
            if (chkIgnoreFlag.Checked)
            {
                chkIgnoreFlag.Text = "<item is ignored now>";
            }
            else
            {
                chkIgnoreFlag.Text = "<Item is Enabled now>";
            } 
        }

        private void chkFailBreak_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFailBreak.Checked)
            {
                chkFailBreak.Text = "<FailBreak is Enabled>";
            }
            else
            {
                chkFailBreak.Text = "<FailBreak is disabled>";
            } 
        }
        
        //******************************************************
    }

}

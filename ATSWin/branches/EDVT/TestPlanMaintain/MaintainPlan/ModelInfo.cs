using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;

namespace TestPlan
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

        long origmylastIDTestModel = PNInfo.mylastIDTestModel;
        long origmynewIDTestModel = PNInfo.mynewIDTestModel;
        long origmyDeletedCountTestModel = PNInfo.myDeletedCountTestModel;

        //long origmylastIDModelPrmtr = PNInfo.mylastIDTestPrmtr;
        //long origmynewIDModelPrmtr = PNInfo.mynewIDTestPrmtr;
        //long origmyDeletedCountModelPrmtr = PNInfo.myDeletedCountTestPrmtr;

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
               
                mytip.SetToolTip(btnAdd, "新增新的测试模型");
                mytip.SetToolTip(btnRemove, "移除选择的测试模型");
                mytip.SetToolTip(btnNextPage, "进入下个窗体页面");
                mytip.SetToolTip(btnAddEquip, "新增左边列表所选仪器到测试模型");
                mytip.SetToolTip(btnRemoveEquip, "从测试模型删除右边列表所选仪器");
                mytip.SetToolTip(btnOK, "保存当前的测试模型信息");

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
                currlst.Items.Clear();
                globalistName.Items.Clear();
                cboItemName.Text = "";
                cboAppModeID.Text = "";
                txtEquipLst.Text = "";
                lstEquip.Items.Clear();
                lstSelectEquip.Items.Clear();
                txtTestCtrlName.Text = TestCtrlName;

                DataRow[] GlobalModelLst = PNInfo.GlobalTotalDS.Tables["GlobalAllTestModelList"].Select();
                // = PNInfo.mySqlIO.GetDataTable("", "");
                foreach (DataRow dr in GlobalModelLst)
                {
                    globalistName.Items.Add(dr["ItemName"]);
                }

                DataRow[] CurrModelLst = PNInfo.TopoToatlDS.Tables["TopoTestModel"].Select("PID=" + PID + "");
                // = PNInfo.mySqlIO.GetDataTable("", "");
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
                            MessageBox.Show("已存在该仪器项目:-> " + NewName);
                            return;
                        }
                    }

                    this.lstSelectEquip.Items.Add(NewName);

                    //txt Update and Refresh
                    RefreshTXTEquipLst();
                }
                else
                {
                    MessageBox.Show("请选择需要新增的Equip后再点击按钮!");
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
                    MessageBox.Show("请选择需要移除的Equip后再点击按钮!");
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
                            MessageBox.Show("已存在TestModel项目:-> " + NewName);

                            return;
                        }
                    }

                    blnAddNewModel = true;
                    PNInfo.myTestModelAddOKFlag = false;
                    myNewModelcount++;
                    currlst.Items.Add(NewName);

                    this.cboItemName.Text = NewName;
                    this.cboAppModeID.Text = PNInfo.getNextTablePIDFromDT(PNInfo.GlobalTotalDS.Tables["GlobalAllTestModelList"], "ItemName='" + NewName + "'").ToString();

                    int myNewRowIndex = (currlst.Items.Count - 1);
                    currlst.SelectedIndex = myNewRowIndex;

                    btnAdd.Enabled = false;
                    currlst.Enabled = false;
                    btnOK.Enabled = true;
                    btnNextPage.Enabled = false;
                }
                else
                {
                    MessageBox.Show("请选择需要新增的TestModel后再点击'Add'按钮!");
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
                    myPrmtrPID = System.Convert.ToInt64(myROWS[0]["ID"]);
                }
                else
                {
                    MessageBox.Show("资料有误!请确认!!! 共有" + myROWS.Length + ("条记录; \n 条件--> ItemName='" + filterString + "' and PID=" + PID + ""));
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
                        MessageBox.Show("新增资料有误!请确认!!! 已有" + myROWS.Length + ("条记录; \n 条件--> ItemName='" + filterString + "' and PID=" + PID + ""));
                        return result;
                    }
                    //无需编辑信息,固定值!
                    //myROWS[0]["ItemName"] = this.cboItemName.Text.ToString();
                    //myROWS[0]["AppModeID"] = this.cboAppModeID.Text.ToString();
                    myROWS[0]["EquipmentList"] = this.txtEquipLst.Text.ToString();
                    result = true;
                }
                else if (this.blnAddNewModel)
                {   
                    string MyNewSEQ=(PNInfo.getMAXColumnsItem(PNInfo.TopoToatlDS.Tables["TopoTestModel"], "SEQ", "PID=" + PID) + 1).ToString();
                    DataRow myNewRow = mydt.NewRow();
                    myNewRow.BeginEdit();
                    myNewRow["ID"] = PNInfo.mylastIDTestModel + 1;
                    myNewRow["PID"] = this.PID.ToString();
                    myNewRow["ItemName"] = this.cboItemName.Text.ToString();
                    myNewRow["AppModeID"] = this.cboAppModeID.Text.ToString();
                    myNewRow["EquipmentList"] = this.txtEquipLst.Text.ToString();
                    myNewRow["Seq"] = MyNewSEQ;
                    
                    mydt.Rows.Add(myNewRow);
                   
                    myNewRow.EndEdit();                    
                    PNInfo.mylastIDTestModel++;
                    PNInfo.myAddCountTestModel++;

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
                    MessageBox.Show("资料有误!请确认!!! 共有" + myROWS.Length + ("条记录; \n 条件--> ItemName='" + filterString + "' and PID=" + PID + ""));
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

                    drst = (MessageBox.Show("即将进行删除资料TestModel 以及对应的TestParameter--> " + currlst.SelectedItem.ToString()
                                + "\n \n 选择 'Y' (是) 继续?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2));

                    if (drst == DialogResult.Yes)
                    {
                        //若为新增的项目则必须指定当前的选择的Item
                        int CurrIndex = currlst.SelectedIndex;
                        string sName = currlst.Items[CurrIndex].ToString();
                        long myPID = PID;

                        bool resultPrmtr = PNInfo.DeleteItemForDT(PNInfo.TopoToatlDS.Tables["TopoTestParameter"], "PID=" + myPrmtrPID);
                        bool result = PNInfo.DeleteItemForDT(PNInfo.TopoToatlDS.Tables["TopoTestModel"], "PID=" + myPID + " and ItemName='" + sName + "'");
                        
                        if (result && resultPrmtr)
                        {
                            PNInfo.ISNeedUpdateflag = true; //140603_2
                            currlst.Items.RemoveAt(CurrIndex);
                            PNInfo.myDeletedCountTestModel++;
                            PNInfo.myDeletedCountTestPrmtr++;  //140521@子信息的数据需要重新查询...TBD
                            myNewModelcount--;

                            this.blnAddNewModel = false;
                            this.btnAdd.Enabled = true;
                            currlst.Enabled = true;
                            btnOK.Enabled = true;
                            btnNextPage.Enabled = true;
                            MessageBox.Show("项目资料 序号为: " + CurrIndex + ";ItemName =" + sName + "已经移除!");
                            
                        }
                        else
                        {
                            MessageBox.Show("项目资料 序号为: " + CurrIndex + "!ItemName =" + sName + "移除失败!");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("请选择需要删除的TestModel后再点击按钮!");
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
                    DataRow[] CurrEquipLst = PNInfo.TopoToatlDS.Tables["TopoEquipment"].Select("PID=" + TestPlanPID);
                    foreach (DataRow dr in CurrEquipLst)
                    {
                        this.lstEquip.Items.Add(dr["ItemName"]);
                    }
                    DataRow[] CurrSelectEquipLst = PNInfo.TopoToatlDS.Tables["TopoTestModel"].Select("PID=" +PID + " and ItemName='" + myModelName + "'");
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
                    MessageBox.Show("PID= " + PID + ",无符合该条件的Equipment 列表资料!请确认Euqipment是否有正确维护?");
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
                    myGlobalModelID = Convert.ToInt32(PNInfo.getDTColumnInfo(PNInfo.GlobalTotalDS.Tables["GlobalAllTestModelList"], "ID", "ItemName= '" + myModelName + "'")); //140529_2

                    if (!blnAddNewModel)
                    {
                        btnOK.Enabled = true;
                        btnNextPage.Enabled = true;

                        getInfoFromDT(PNInfo.TopoToatlDS.Tables["TopoTestModel"], currlst.SelectedIndex);

                        //未存放至Server 此处需进行查询本机未更新的DS
                        myPrmtrPID = PNInfo.getNextTablePIDFromDT(PNInfo.TopoToatlDS.Tables["TopoTestModel"], "ItemName= '" + currlst.SelectedItem.ToString() + "' and PID=" + PID);

                        DataRow[] myRows = PNInfo.TopoToatlDS.Tables["TopoTestParameter"].Select("PID=" + myPrmtrPID); //;("Select * from TopoTestParameter where PID=" + myPrmtrPID, "TopoTestParameter");
                        if (myRows.Length > 0)
                        {
                            TestPlanPID = Convert.ToInt32(PNInfo.getDTColumnInfo(PNInfo.TopoToatlDS.Tables["TopoTestControl"], "PID", "ID= " + PID));

                            getEquipLstInfo(TestPlanPID);
                        }
                        else
                        {
                            MessageBox.Show("未发现Topo表中有当前选择Model的参数,资料丢失!");
                            return;
                        }
                    }
                    else
                    {
                        btnOK.Enabled = true;
                        btnNextPage.Enabled = false;

                        myPrmtrPID = PNInfo.mylastIDTestModel + 1;
                        TestPlanPID = Convert.ToInt32(PNInfo.getDTColumnInfo(PNInfo.TopoToatlDS.Tables["TopoTestControl"], "PID", "ID= " + PID));
                        getEquipLstInfo(TestPlanPID);
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
                bool result = EditInfoForDT(PNInfo.TopoToatlDS.Tables["TopoTestModel"]);

                if (result)
                {
                    PNInfo.ISNeedUpdateflag = true; //140603_2
                    PNInfo.myTestModelISNewFlag = false;        //140530_2
                    btnNextPage.Enabled = true;
                    PNInfo.myTestModelAddOKFlag = true; //140530_0
                    //if (blnAddNewModel)   //140530_1
                    if( PNInfo.myTestModelAddOKFlag == true)
                    {
                        if (blnAddNewModel) //140703_2 若为新增Model则强制维护TestParameter >>>
                        {
                            TestParameterInfo myTestParameterInfo = new TestParameterInfo();
                            myTestParameterInfo.ItemName = currlst.SelectedItem.ToString();
                            myTestParameterInfo.myPrmtrPID = Convert.ToInt64(PNInfo.getDTColumnInfo(PNInfo.TopoToatlDS.Tables[3], "ID", "ItemName = '" + currlst.SelectedItem.ToString() + "' and PID=" + this.PID));
                            myPrmtrPID = myTestParameterInfo.myPrmtrPID;
                            myModelName = myTestParameterInfo.ItemName;
                            myTestParameterInfo.blnAddNew = true;
                            //新增Model 对应的Prmtr的PID部分相互处理 //140529 OK
                            myTestParameterInfo.ShowDialog();

                            if (PNInfo.currPrmtrCountExisted(PNInfo.TopoToatlDS.Tables["TopoTestParameter"], "PID=" + myPrmtrPID)
                                == PNInfo.currPrmtrCountExisted(PNInfo.GlobalTotalDS.Tables["GlobalTestModelParamterList"], "PID=" + myGlobalModelID)
                                )
                            {
                                blnAddNewModel = false; //140527_00
                                btnAdd.Enabled = true;
                                currlst.Enabled = true;
                                PNInfo.myTestPrmtrISNewFlag = false;        //140530_2
                                //this.Close(); //140530_4 //140703_2
                            }
                            else
                            {
                                MessageBox.Show("未发现Topo表中有当前选择Model的参数与Global参数一致,资料丢失!不允许保存! \n 系统将自动删除未完成的TestModel", "注意!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                deleteNewModelONErr(); //140530_3 TBD?
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
                int myExistCount = PNInfo.currPrmtrCountExisted(PNInfo.TopoToatlDS.Tables["TopoTestModel"], queryCMD);

                if (myExistCount > 0)
                {
                    PNInfo.DeleteItemForDT(PNInfo.TopoToatlDS.Tables["TopoTestModel"], queryCMD);
                    PNInfo.mylastIDTestModel = PNInfo.mylastIDTestModel - myExistCount;
                }
                PNInfo.myTestModelAddOKFlag = true;
                PNInfo.myTestPrmtrAddOKFlag = true; //140605_2
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
                DialogResult myResult = MessageBox.Show(
                    "尚未完成资料维护!提前退出将可能无法保证资料完整,系统将自动删除当前维护项目资料!",
                    "注意:",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button1);
                try
                {
                    if (myResult == DialogResult.OK)
                    {
                        deleteNewModelONErr();
                        this.Dispose();
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

            
        }
        
        //******************************************************
    }

}

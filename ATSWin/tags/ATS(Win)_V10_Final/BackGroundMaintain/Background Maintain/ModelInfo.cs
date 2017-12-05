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
        public string GlobalAPPName = "";
        public long PID = -1;

        bool blnAddNewPrmtr = false;
        long currModelID = -1;
        long currPrmtrID = -1;
        long myNewModelcount = -1;
        string tempModelName = "";
        string tempParamName = "";
        //ConstGlobalListTables = new string[] { "GlobalAllEquipmentList", "GlobalAllEquipmentParamterList", "GlobalAllAppModelList", "GlobalAllTestModelList", "GlobalTestModelParamterList" };

        public ModelInfo()
        {
            InitializeComponent();
        }

        private void ModelInfo_Load(object sender, EventArgs e)
        {
            try
            {
                refreshAPPlst();
                refreshModelInfo(false);
                ShowMyTip();
                setModelState(true);
                runModelMsgState((byte)MsgState.Clear);
                runPrmtrMsgState((byte)MsgState.Clear);
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

                mytip.SetToolTip(btnAdd, "Add a new model");
                mytip.SetToolTip(btnRemove, "Delete a model");
                mytip.SetToolTip(btnFinish, "Return");

                mytip.ShowAlways = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void refreshAPPlst()
        {
            try
            {
                globaApplistName.Items.Clear();
                currlst.Items.Clear();
                DataRow[] GlobalAppLst = MainForm.GlobalDS.Tables["GlobalAllAppModelList"].Select();
                setModelState(true);
                foreach (DataRow dr in GlobalAppLst)
                {
                    globaApplistName.Items.Add(dr["ItemName"]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        void refreshModelInfo(bool state)
        {
            try
            {
                currlst.Items.Clear();
                clearCboItems();    //140916_1
                setModelState(false);

                DataRow[] CurrModelLst = MainForm.GlobalDS.Tables["GlobalAllTestModelList"].Select();

                if (state)
                {
                    CurrModelLst = MainForm.GlobalDS.Tables["GlobalAllTestModelList"].Select("PID=" + PID);
                }
                else
                {
                    CurrModelLst = MainForm.GlobalDS.Tables["GlobalAllTestModelList"].Select();
                }
                foreach (DataRow dr in CurrModelLst)
                {
                    currlst.Items.Add(dr["ItemName"]);
                }
                myNewModelcount = CurrModelLst.Length;
                if (currlst.Items.Contains(cboModelName.Text))
                {
                    currlst.SelectedItem = cboModelName.Text;
                }
                else
                {
                    cboModelName.Enabled = false;
                    cboModelName.Text = "";
                    txtDescription.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //******************************************************

        void getInfoFromDT(DataTable mydt, int currIndex)
        {
            try
            {
                string filterString = currlst.SelectedItem.ToString();
                DataRow[] myROWS = mydt.Select("ItemName='" + filterString + "' and PID=" + PID);
                if (myROWS.Length == 1)
                {
                    tempModelName = myROWS[0]["ItemName"].ToString();   //150310_1
                    cboModelName.Enabled = true;    //150310_1
                    this.cboModelName.Text = myROWS[0]["ItemName"].ToString();
                    this.txtDescription.Text = myROWS[0]["ItemDescription"].ToString();
                    currModelID = System.Convert.ToInt64(myROWS[0]["ID"]);
                }
                else
                {
                    MessageBox.Show("Error! \n " + myROWS.Length + " records existed; \n filterString-->ItemName='" + filterString + "' and PID=" + PID);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }
        }

        void clearCboItems()
        {
            try
            {
                this.cboItemName.Text = "";
                this.cboItemType.Text = "";
                this.cboDirection.Text = "";
                this.cboItemValue.Text = "";
                this.cboSpecMin.Text = "";
                this.cboSpecMax.Text = "";
                this.cboItemSpecific.Text = "";
                this.cboLogRecord.Text = "";
                this.cboDataRecord.Text = "";

                this.cboItemName.Items.Clear();
                this.cboItemType.Items.Clear();
                this.cboDirection.Items.Clear();
                this.cboItemValue.Items.Clear();
                this.cboSpecMin.Items.Clear();
                this.cboSpecMax.Items.Clear();
                this.cboItemSpecific.Items.Clear();
                this.cboLogRecord.Items.Clear();
                this.cboDataRecord.Items.Clear();

                DataRow[] drs = MainForm.GlobalDS.Tables["GlobalTestModelParamterList"].Select("");

                for (int i = 0; i < drs.Length; i++)
                {
                    if (this.cboItemName.Items.Contains(drs[i]["ItemName"]) == false)
                    {
                        this.cboItemName.Items.Add(drs[i]["ItemName"]);
                    }
                    if (cboDirection.Items.Contains(drs[i]["Direction"]) == false)
                    {
                        this.cboDirection.Items.Add(drs[i]["Direction"]);
                    }

                    if (cboItemType.Items.Contains(drs[i]["ItemType"]) == false)
                    {
                        this.cboItemType.Items.Add(drs[i]["ItemType"]);
                    }
                }

                this.cboSpecMin.Items.Add("-32768");
                this.cboSpecMax.Items.Add("32767");

                this.cboItemSpecific.Items.Add("0");
                this.cboLogRecord.Items.Add("0");
                this.cboDataRecord.Items.Add("0");

                this.cboItemSpecific.Items.Add("1");
                this.cboLogRecord.Items.Add("1");
                this.cboDataRecord.Items.Add("1");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void getInfoFromPrmtrDT(DataTable mydt)
        {
            try
            {
                runPrmtrMsgState((byte)MsgState.Clear);
                if (dgvPrmtr.CurrentRow != null && this.Visible)
                {
                    runPrmtrMsgState((byte)MsgState.Edit);
                    currPrmtrID = Convert.ToInt64(this.dgvPrmtr.CurrentRow.Cells["ID"].Value); //140916_0 修正不能编辑的BUG
                    string myID = this.dgvPrmtr.CurrentRow.Cells["ID"].Value.ToString();
                    string filterString = this.dgvPrmtr.CurrentRow.Cells["ItemName"].Value.ToString();

                    DataRow[] myROWS = mydt.Select("ID=" + myID + " and PID=" + currModelID);
                    if (myROWS.Length == 1)
                    {
                        tempParamName = dgvPrmtr.CurrentRow.Cells["ItemName"].Value.ToString();    //150310_1
                    
                        this.cboItemName.Text = myROWS[0]["ItemName"].ToString();
                        this.cboItemType.Text = myROWS[0]["ItemType"].ToString();
                        this.cboDirection.Text = myROWS[0]["Direction"].ToString();
                        this.cboItemValue.Text = myROWS[0]["ItemValue"].ToString();
                        this.cboSpecMin.Text = myROWS[0]["SpecMin"].ToString();
                        this.cboSpecMax.Text = myROWS[0]["SpecMax"].ToString();
                        this.cboItemSpecific.Text = myROWS[0]["ItemSpecific"].ToString();
                        this.cboLogRecord.Text = myROWS[0]["LogRecord"].ToString();
                        this.cboDataRecord.Text = myROWS[0]["DataRecord"].ToString();
                        if (mydt.Columns.Contains("ItemDescription")) //141125_0
                        {
                            txtPrmtrDescription.Text = myROWS[0]["ItemDescription"].ToString();
                        }

                        if ((myROWS[0]["ItemType"].ToString().ToUpper().Contains("double".ToUpper())
                            || myROWS[0]["ItemType"].ToString().ToUpper().Contains("single".ToUpper()))
                             && myROWS[0]["SpecMin"].ToString().Contains(".")
                            && myROWS[0]["SpecMin"].ToString().Contains("E") == false)
                        {
                            this.cboSpecMin.Text = Convert.ToDouble(myROWS[0]["SpecMin"]).ToString("0.0000");
                        }
                        if ((myROWS[0]["ItemType"].ToString().ToUpper().Contains("double".ToUpper())
                            || myROWS[0]["ItemType"].ToString().ToUpper().Contains("single".ToUpper()))
                             && myROWS[0]["SpecMax"].ToString().Contains(".")
                            && myROWS[0]["SpecMax"].ToString().Contains("E") == false)
                        {
                            this.cboSpecMax.Text = Convert.ToDouble(myROWS[0]["SpecMax"]).ToString("0.0000");
                        }
                        if ((myROWS[0]["ItemType"].ToString().ToUpper().Contains("double".ToUpper())
                            || myROWS[0]["ItemType"].ToString().ToUpper().Contains("single".ToUpper()))
                             && myROWS[0]["ItemValue"].ToString().Contains(".")
                            && myROWS[0]["ItemValue"].ToString().Contains("E") == false)
                        {
                            this.cboItemValue.Text = Convert.ToDouble(myROWS[0]["ItemValue"]).ToString("0.0000");
                        }
                    }
                    else
                    {
                        if (blnAddNewModel)
                        {

                        }
                        else
                        {
                            MessageBox.Show("Parameters was not existed!"
                                + myROWS.Length + (" records existed; \n filterString--> ItemName='" + filterString + "' and PID=" + currModelID + ""));

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }
        }

        bool EditInfoForModelDT(DataTable mydt)
        {
            bool result = false;
            try
            {
                string filterString = currModelID.ToString();   //150310_0
                DataRow[] myROWS = mydt.Select("ID=" + filterString + " and PID=" + PID + ""); //150310_0
                if (myROWS.Length == 1)
                {
                    if (this.blnAddNewModel)
                    {
                        MessageBox.Show("Add new data error! \n " + myROWS.Length + " records existed; \n filterString--> ItemName='"
                            + filterString + "' and PID=" + PID + "");
                        return result;
                    }
                    //无需编辑信息,固定值!
                    myROWS[0]["ItemName"] = this.cboModelName.Text.ToString();
                    myROWS[0]["ItemDescription"] = this.txtDescription.Text.ToString();
                    result = true;
                }
                else if (this.blnAddNewModel && myROWS.Length == 0)
                {
                    DataRow myNewRow = mydt.NewRow();
                    myNewRow.BeginEdit();
                    myNewRow["ID"] = MainForm.mylastIDTestModel + 1;
                    myNewRow["PID"] = this.PID.ToString();
                    myNewRow["ItemName"] = this.cboModelName.Text.ToString();
                    myNewRow["ItemDescription"] = this.txtDescription.Text.ToString();

                    mydt.Rows.Add(myNewRow);

                    myNewRow.EndEdit();
                    MainForm.mylastIDTestModel++;
                    MainForm.myAddCountTestModel++;

                    refreshModelInfo(true);

                    int myNewRowIndex = (currlst.Items.Count - 1);

                    currlst.Enabled = true;
                    currlst.Focus();
                    currlst.SelectedIndex = myNewRowIndex;

                    blnAddNewModel = false;
                    result = true;
                }
                else
                {
                    MessageBox.Show("Error! \n " + myROWS.Length + " records existed; \n filterString--> ID=" + filterString + " and PID=" + PID + "");
                }
                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return result;
            }
        }

        private void btnFinish_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (MainForm.checkItemLength("ItemName", cboModelName.Text, 30)
                    || MainForm.checkItemLength("ItemDescription", this.txtDescription.Text, 50)
                    )
                {
                    return;
                }
                if (this.blnAddNewModel || (tempModelName.Length > 0 && tempModelName.ToUpper() != this.cboModelName.Text.ToString().ToUpper().Trim()))
                {
                    if (MainForm.currPrmtrCountExisted(MainForm.GlobalDS.Tables["GlobalAllTestModelList"], "ItemName ='" + this.cboModelName.Text.ToString() + "'") > 0)
                    {
                        MessageBox.Show("The data of ItemName has existed! <Violate unique rule>");
                        return;
                    }
                }

                bool result = EditInfoForModelDT(MainForm.GlobalDS.Tables["GlobalAllTestModelList"]);

                if (result)
                {
                    refreshModelInfo(true);
                    setPrmtrState(false);                    
                    MainForm.ISNeedUpdateflag = true;
                    MainForm.myGlobalModelISNewFlag = false;
                    btnFinish.Enabled = true;
                    MainForm.myGlobalModelAddOKFlag = true;
                    runModelMsgState((byte)MsgState.SaveOK);
                }
                else
                {
                    btnFinish.Enabled = false;
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
                string queryCMD = "ID = " + this.currModelID + " And PID=" + this.PID;
                int myExistCount = MainForm.currPrmtrCountExisted(MainForm.GlobalDS.Tables["GlobalAllTestModelList"], queryCMD);

                if (myExistCount > 0)
                {
                    MainForm.DeleteItemForDT(MainForm.GlobalDS.Tables["GlobalAllTestModelList"], queryCMD);
                    MainForm.mylastIDTestModel = MainForm.mylastIDTestModel - myExistCount;
                }
                MainForm.myGlobalModelAddOKFlag = true;
                MainForm.myGlobalPrmtrAddOKFlag = true; //140605_2
                blnAddNewModel = false; //140527_00
                btnAdd.Enabled = true;
                currlst.Enabled = true;
                refreshModelInfo(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                refreshModelInfo(true);
            }
        }

        private void ModelInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.blnAddNewModel)
            {
                blnAddNewModel = false; //141030_1
                return; //141030_1  不再删除未保存资料<万一字符串为已存在资料 可能误删>                
            }
        }

        void setModelState(bool state)
        {
            try
            {
                grpModel.Enabled = !state;
                btnOK.Enabled = !state;
                btnAdd.Enabled = !state;
                btnRemove.Enabled = !state;

                if (state)
                {
                    grpModel.Text = "Model List";
                }
                else
                {
                    grpModel.Text = "Model List(EditMode)";

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void globaApplistName_SelectedIndexChanged(object sender, EventArgs e)  //140707_2
        {
            try
            {
                if (globaApplistName.SelectedIndex != -1)
                {
                    setModelState(false);
                    PID = Convert.ToInt64(
                        MainForm.getDTColumnInfo(
                        MainForm.GlobalDS.Tables["GlobalAllAppModelList"]
                        , "ID", "ItemName ='" + globaApplistName.SelectedItem.ToString() + "'"));
                    refreshModelInfo(true);
                    clearCboItems();    //141125_0
                    dgvPrmtr.DataSource = null;
                }
                else
                {
                    //setModelState(false); //140922_0
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        void setPrmtrState(bool state)
        {
            try
            {
                btnPrmtrOK.Enabled = !state;
                btnPrmtrDelete.Enabled = !state;
                btnPrmtrAdd.Enabled = !state;
                dgvPrmtr.Enabled = !state;

                setgrpPrmtr(!state);

                if (state)
                {
                    cboModelName.BackColor = Color.Yellow;
                    txtDescription.BackColor = Color.Yellow;
                }
                else
                {
                    cboModelName.BackColor = Color.White;
                    txtDescription.BackColor = Color.White;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        private void btnModelAdd_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult drt = MessageBox.Show("Do you want to copy a new ModelInfo item and ModelInfoPatameters from other data source?"
                    , "Notice", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);

                if (drt == System.Windows.Forms.DialogResult.Yes)
                {
                    NewPlanName myNewPlanName = new NewPlanName("ModelInfo");
                    myNewPlanName.myPID = PID;
                    myNewPlanName.ShowDialog();
                    refreshAPPlst();
                    refreshModelInfo(true);
                    return;
                }
                currlst.Enabled = false;
                currModelID = MainForm.mylastIDTestModel + 1;
                blnAddNewModel = true;
                this.currlst.Enabled = false;
                this.cboModelName.Text = "";

                setPrmtrState(true);
                runModelMsgState((byte)MsgState.AddNew);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnModelDelete_Click(object sender, EventArgs e)
        {
            try
            {
                setPrmtrState(false);

                if (blnAddNewModel) //140917_0 新增误点新增取消新增状态!并直接返回
                {
                    blnAddNewModel = false;
                    runModelMsgState((byte)MsgState.Clear);
                    currlst.Enabled = true;
                    currlst.SelectedItem = null;
                    if (currlst.Items.Count > 0)
                    {
                        currlst.SelectedIndex = 0;
                    }
                    return;
                }

                if (this.currlst.SelectedIndex == -1)
                {
                    MessageBox.Show("Pls choose a item first!");
                    return;
                }
                else
                {
                    try
                    {
                        DialogResult drst = new DialogResult();
                        string myDeleteItemName = currlst.SelectedItem.ToString();
                        drst = (MessageBox.Show("Delete Item -->" + myDeleteItemName + "\n \n Pls choose 'Y' (是) to continue?",
                        "Notice", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2));

                        if (drst == DialogResult.Yes)
                        {
                            bool result = MainForm.DeleteItemForDT(MainForm.GlobalDS.Tables["GlobalAllTestModelList"], "PID=" + PID + "and ItemName='" + myDeleteItemName + "'");
                            refreshModelInfo(true);
                            if (result)
                            {
                                MainForm.ISNeedUpdateflag = true;
                                MessageBox.Show("Item: " + myDeleteItemName + " has been deleted successful!");
                                runModelMsgState((byte)MsgState.Delete);
                            }
                            else
                            {
                                MessageBox.Show("Item: " + myDeleteItemName + " delete Failed!");
                            }
                        }
                        this.cboModelName.BackColor = Color.White;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void setgrpPrmtr(bool state)
        {
            try
            {
                grpPrmtr.Visible = state;
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
                if (state == (byte)MsgState.AddNew)
                {
                    this.txtSaveResult.Text = "Add a new item...";
                    this.txtSaveResult.BackColor = Color.Yellow;
                    this.cboItemType.BackColor = Color.Yellow;
                    this.cboItemName.Enabled = true;
                    blnAddNewPrmtr = true;  //140917_0 
                    clearCboItems();
                    //dgvPrmtr.Enabled = false;
                }
                else if (state == (byte)MsgState.Edit)
                {
                    this.txtSaveResult.Text = "Edit current item...";
                    this.txtSaveResult.BackColor = Color.GreenYellow;
                    this.cboItemType.BackColor = Color.GreenYellow;
                    this.cboItemName.Enabled = true;
                    blnAddNewPrmtr = false;  //140917_0 
                }
                else if (state == (byte)MsgState.SaveOK)
                {
                    this.txtSaveResult.Text = "Item: " + this.cboItemName.Text + " has been saved successful!"; ;     //140917_0 cboItemType
                    this.txtSaveResult.BackColor = Color.YellowGreen;
                    this.cboItemType.BackColor = Color.YellowGreen;
                    this.cboItemName.Enabled = false;
                    blnAddNewPrmtr = false;  //140917_0 
                }
                else if (state == (byte)MsgState.Delete)
                {
                    this.txtSaveResult.Text = "Item: " + this.cboItemName.Text + " has been deleted successful!";   //140917_0 cboItemType
                    this.txtSaveResult.BackColor = Color.Pink;
                    this.cboItemType.BackColor = Color.Pink;
                    this.cboItemName.Enabled = false;
                    blnAddNewPrmtr = false;  //140917_0 
                    clearCboItems();
                }
                else if (state == (byte)MsgState.Clear)
                {
                    this.txtSaveResult.Text = "";
                    this.txtSaveResult.BackColor = Color.White;
                    this.cboItemType.BackColor = Color.White;
                    this.cboItemName.Enabled = false;
                    blnAddNewPrmtr = false;  //140917_0 
                    clearCboItems();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void runModelMsgState(byte state)
        {
            try
            {
                clearCboItems();
                this.cboModelName.Enabled = true;
                this.grpEquipPrmtr.Enabled = true;  //140917_0 
                this.grpPrmtr.Enabled = true;       //140917_0 
                if (state == (byte)MsgState.AddNew)
                {
                    this.txtSaveResult.Text = "Add a new model...";
                    this.txtSaveResult.BackColor = Color.Yellow;
                    this.cboItemType.BackColor = Color.Yellow;
                    this.cboModelName.Enabled = true;
                    blnAddNewModel = true;  //140917_0
                    dgvPrmtr.DataSource = null; //140917_0
                    this.grpEquipPrmtr.Enabled = false;  //140917_0 
                    this.grpPrmtr.Enabled = false;       //140917_0 

                    //dgvPrmtr.Enabled = false;
                }
                else if (state == (byte)MsgState.Edit)
                {
                    this.txtSaveResult.Text = "Edit current model...";
                    this.txtSaveResult.BackColor = Color.GreenYellow;
                    this.cboModelName.BackColor = Color.GreenYellow;
                    blnAddNewModel = false;  //140917_0
                }
                else if (state == (byte)MsgState.SaveOK)
                {
                    this.txtSaveResult.Text = "Model " + this.cboModelName.Text + " has been saved successful!";
                    this.txtSaveResult.BackColor = Color.YellowGreen;
                    this.cboModelName.BackColor = Color.YellowGreen;
                    blnAddNewModel = false;  //140917_0
                    cboModelName.Enabled = false;   //150310_1
                }
                else if (state == (byte)MsgState.Delete)
                {
                    this.txtSaveResult.Text = "Model " + this.cboModelName.Text + " has been deleted successful!";
                    this.txtSaveResult.BackColor = Color.Pink;
                    this.cboModelName.BackColor = Color.Pink;
                    blnAddNewModel = false;  //140917_0
                    cboModelName.Enabled = false;   //150310_1
                }
                else if (state == (byte)MsgState.Clear)
                {
                    this.cboModelName.Text = "";
                    this.txtDescription.Text = "";
                    this.txtSaveResult.Text = "";
                    this.txtSaveResult.BackColor = Color.White;
                    this.cboModelName.BackColor = Color.White;
                    blnAddNewModel = false;  //140917_0
                    cboModelName.Enabled = false;   //150310_1
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnPrmtrAdd_Click(object sender, EventArgs e)
        {
            try
            {
                setgrpPrmtr(true);
                currPrmtrID = MainForm.mylastIDTestPrmtr + 1;
                //140917_0 blnAddNewPrmtr = true;  
                runPrmtrMsgState((byte)MsgState.AddNew);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnPrmtrDelete_Click(object sender, EventArgs e)
        {

            if (blnAddNewPrmtr)    //140917_0 新增误点新增取消新增状态!并直接返回
            {
                blnAddNewPrmtr = false;
                runPrmtrMsgState((byte)MsgState.Clear);
                return;
            }

            setgrpPrmtr(false);
            if (dgvPrmtr.CurrentRow == null || dgvPrmtr.CurrentRow.Index == -1)
            {
                MessageBox.Show("Pls choose a item first!");
                return;
            }
            else
            {
                try
                {
                    DialogResult drst = new DialogResult();
                    string myDeleteItemName = dgvPrmtr.CurrentRow.Cells["ItemName"].Value.ToString();
                    drst = (MessageBox.Show("Delete Item -->" + myDeleteItemName + "\n \n Pls choose 'Y' (是) to continue?",
                        "Notice", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2));

                    if (drst == DialogResult.Yes)
                    {
                        bool result = MainForm.DeleteItemForDT(MainForm.GlobalDS.Tables["GlobalTestModelParamterList"], "PID=" + currModelID + "and ItemName='" + myDeleteItemName + "'");
                        if (result)
                        {
                            blnAddNewPrmtr = false;
                            MainForm.ISNeedUpdateflag = true;
                            MessageBox.Show("Item: " + myDeleteItemName + " has been deleted successful!");
                            runPrmtrMsgState((byte)MsgState.Delete);

                        }
                        else
                        {
                            MessageBox.Show("Item: " + myDeleteItemName + " delete Failed!");
                        }
                    }
                    //140917_0 runPrmtrMsgState((byte)MsgState.SaveOK);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        bool EditInfoForPrmtrDT(DataTable mydt)  //140704_5 TBD
        {
            bool result = false;
            try
            {
                //ItemName ModuleLine ChipLine DriveType RegisterAddress Length
                string filterString = "ID=" + currPrmtrID + " AND PID=" + currModelID;
                string pItemName = cboItemName.Text;
                DataRow[] myROWS = mydt.Select(filterString);
                if (myROWS.Length == 1)
                {
                    if (blnAddNewPrmtr)
                    {
                        MessageBox.Show("Add new data error! \n " + myROWS.Length + " records existed; \n filterString-->  " + filterString);
                        return result;
                    }
                    myROWS[0].BeginEdit();
                    myROWS[0]["ItemName"] = this.cboItemName.Text.ToString();   //140718_0
                    myROWS[0]["ItemType"] = this.cboItemType.Text.ToString();
                    myROWS[0]["Direction"] = this.cboDirection.Text.ToString();
                    myROWS[0]["ItemValue"] = this.cboItemValue.Text.ToString();
                    myROWS[0]["SpecMin"] = this.cboSpecMin.Text.ToString();
                    myROWS[0]["SpecMax"] = this.cboSpecMax.Text.ToString();
                    myROWS[0]["ItemSpecific"] = this.cboItemSpecific.Text.ToString();
                    myROWS[0]["LogRecord"] = this.cboLogRecord.Text.ToString();
                    myROWS[0]["DataRecord"] = this.cboDataRecord.Text.ToString();
                    if (mydt.Columns.Contains("ItemDescription")) //141125_0
                    {
                        myROWS[0]["ItemDescription"] = txtPrmtrDescription.Text;
                    }
                    myROWS[0].EndEdit();
                    result = true;
                }
                else if (blnAddNewPrmtr)
                {
                    DataRow myNewRow = mydt.NewRow();
                    myNewRow.BeginEdit();
                    myNewRow["ID"] = currPrmtrID;   //MainForm.mylastIDTestPrmtr + 1; 
                    myNewRow["PID"] = currModelID;  //140718_0 this.PID;
                    myNewRow["ItemName"] = this.cboItemName.Text.ToString();   //140718_0
                    myNewRow["ItemType"] = this.cboItemType.Text.ToString();
                    myNewRow["Direction"] = this.cboDirection.Text.ToString();
                    myNewRow["ItemValue"] = this.cboItemValue.Text.ToString();
                    myNewRow["SpecMin"] = this.cboSpecMin.Text.ToString();
                    myNewRow["SpecMax"] = this.cboSpecMax.Text.ToString();
                    myNewRow["ItemSpecific"] = this.cboItemSpecific.Text.ToString();
                    myNewRow["LogRecord"] = this.cboLogRecord.Text.ToString();
                    myNewRow["DataRecord"] = this.cboDataRecord.Text.ToString();
                    if (mydt.Columns.Contains("ItemDescription"))   //141125_0
                    {
                        myNewRow["ItemDescription"] = txtPrmtrDescription.Text;
                    }
                    mydt.Rows.Add(myNewRow);

                    myNewRow.EndEdit();
                    MainForm.setDgvCurrCell(dgvPrmtr, "ID", myNewRow["ID"].ToString());
                    MainForm.mylastIDTestPrmtr++;
                    MainForm.myAddCountTestPrmtr++;
                    result = true;
                    //blnAddNewPrmtr = false; 140708_0
                }
                else
                {
                    MessageBox.Show("Error! " + myROWS.Length + (" records existed; \n filterString--> " + filterString));
                }

                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return result;
            }
        }

        private void btnPrmtrOK_Click(object sender, EventArgs e)
        {
            try
            {
                setgrpPrmtr(true);
                if (MainForm.checkItemLength("ItemName", cboItemName.Text, 30)
                    || MainForm.checkItemLength("ItemType", this.cboItemType.Text, 10)
                    || MainForm.checkItemLength("ItemValue", this.cboItemValue.Text, 50)
                    || MainForm.checkItemLength("ItemDescription", this.txtPrmtrDescription.Text, 200)
                    )
                {
                    return;
                }
                if (
                        this.cboItemType.Text.ToString().Trim().Length == 0 ||
                        this.cboItemName.Text.ToString().Trim().Length == 0 ||
                        this.cboDirection.Text.ToString().Trim().Length == 0 ||
                        this.cboItemValue.ToString().Trim().Length == 0 ||
                        this.cboSpecMin.ToString().Trim().Length == 0 ||
                        this.cboSpecMax.ToString().Trim().Length == 0 ||
                        this.cboItemSpecific.ToString().Trim().Length == 0 ||
                        this.cboLogRecord.ToString().Trim().Length == 0 ||
                        this.cboDataRecord.ToString().Trim().Length == 0
                    )
                {
                    MessageBox.Show("Some data is invalid !");
                    return;
                }

                if (blnAddNewPrmtr || (tempParamName.Length > 0 && tempParamName.ToUpper() != this.cboItemName.Text.ToString().ToUpper().Trim()))
                {
                    if (MainForm.currPrmtrCountExisted(MainForm.GlobalDS.Tables["GlobalTestModelParamterList"],
                        "PID= " + currModelID + " and ItemName ='" + this.cboItemName.Text.ToString() + "'") > 0)
                    {
                        //140704_1 约束PID+Name
                        MessageBox.Show("Edit data error! \n This item has been existed!<unique>");
                        return;
                    }
                }

                bool result = EditInfoForPrmtrDT(MainForm.GlobalDS.Tables["GlobalTestModelParamterList"]);

                if (result)
                {
                    MainForm.ISNeedUpdateflag = true;
                    runPrmtrMsgState((byte)MsgState.SaveOK);
                    //新增OK后需要置位!
                    if (blnAddNewPrmtr)
                    {
                        blnAddNewPrmtr = false;
                        this.cboItemType.BackColor = Color.White;
                        this.txtSaveResult.Text = "Item: " + this.cboItemType.Text + "Add OK!";
                    }
                    else
                    {
                        this.txtSaveResult.Text = "Item: " + this.cboItemType.Text + "Save OK!";
                    }
                    this.txtSaveResult.BackColor = Color.YellowGreen;
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

        private void currlst_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (currlst.SelectedIndex != -1)
                {
                    getInfoFromDT(MainForm.GlobalDS.Tables["GlobalAllTestModelList"], currlst.SelectedIndex);
                    MainForm.showTablefilterStrInfo(MainForm.GlobalDS.Tables["GlobalTestModelParamterList"], dgvPrmtr, "PID=" + currModelID);
                    blnAddNewPrmtr = false;
                    clearCboItems();    //140917_0
                }
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
                setgrpPrmtr(true);
                getInfoFromPrmtrDT(MainForm.GlobalDS.Tables["GlobalTestModelParamterList"]);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void dgvPrmtr_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                setgrpPrmtr(true);
                getInfoFromPrmtrDT(MainForm.GlobalDS.Tables["GlobalTestModelParamterList"]);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //******************************************************
    }

}

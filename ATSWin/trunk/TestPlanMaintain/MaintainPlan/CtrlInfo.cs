using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;

namespace Maintain
{
    public partial class CtrlInfo : Form
    {
        public CtrlInfo()
        {
            InitializeComponent();
        }
        string tempTestPlanName = "";
        public bool blnAddNew = false;
        public string  TestPlanName = "";
        public long PID = -1;
        string myTestCtrlID="";
        string TestCtrlName = "";
        string  mySEQ = "-1";
        bool blnIsAddAux = false;   //140811_1

        private void CtrlInfo_Load(object sender, EventArgs e)
        {
            try
            {
                if (MainForm.TopoToatlDS.Tables["TopoTestControl"].Columns.Contains("ItemDescription"))  //150411_0
                {
                    txtDescription.Visible = true;
                    lblDescription.Visible = true;
                }
                else
                {
                    txtDescription.Visible = false;
                    lblDescription.Visible = false;
                }
                if (MainForm.TopoToatlDS.Tables["TopoTestControl"].Columns.Contains("CtrlType"))  //150414_0
                {
                    cboCtrlType.Visible = true;
                    lblCtrlType.Visible = true;
                }
                else
                {
                    cboCtrlType.Text = "FMT";
                    cboCtrlType.Visible = false;
                    lblCtrlType.Visible = false;
                }


                btnFinish.Text = "Return"; 
                setAuxshow(false);   //140811_1
                RefreshList();
                ShowMyTip();
                this.txtTestPlanName.Text = TestPlanName;

                //140709_1>>>>>>>>>>
                this.cboDataRate.Items.Clear();
                //140811_2 this.txtAuxAttribles.Items.Clear(); 
                //140811_2 this.txtAuxAttribles.Items.Add("DUT_Soak_Time= 指定值; ReadTempFromXStream= 指定值"); //140606
                DataRow[] drs = MainForm.TopoToatlDS.Tables["TopoTestControl"].Select("");

                for (int i = 0; i < drs.Length; i++)
                {
                    if (this.cboDataRate.Items.Contains(drs[i]["DataRate"]) == false)
                    {
                        this.cboDataRate.Items.Add(drs[i]["DataRate"]);
                    }

                    if (this.cboChannel.Items.Contains(drs[i]["Channel"]) == false)
                    {
                        this.cboChannel.Items.Add(drs[i]["Channel"]);
                    }

                    if (this.cboPattent.Items.Contains(drs[i]["Pattent"]) == false)
                    {
                        this.cboPattent.Items.Add(drs[i]["Pattent"]);
                    }

                    if (this.cboTemp.Items.Contains(drs[i]["Temp"]) == false)
                    {
                        this.cboTemp.Items.Add(drs[i]["Temp"]);
                    }

                    if (this.cboVcc.Items.Contains(drs[i]["Vcc"]) == false)
                    {
                        this.cboVcc.Items.Add(drs[i]["Vcc"]);
                    }
                }
                
                //140709_1<<<<<<<<<<

                if (blnAddNew)
                {
                    
                    mySEQ = (MainForm.getMAXColumnsItem(MainForm.TopoToatlDS.Tables["TopoTestControl"], "SEQ", "PID=" + PID) + 1).ToString();
                    myTestCtrlID = (MainForm.mylastIDTestCtrl + 1).ToString();

                    MainForm.myTestCtrlAddOKFlag = false;
                    cboItemName.BackColor = Color.Yellow;
                    currlst.Enabled = false;
                    btnOK.Enabled = true;
                    MainForm.myTestCtrlISNewFlag = true;        //140530_2
                }
                else
                {
                    btnOK.Enabled = false;
                    btnFinish.Enabled = true;
                    MainForm.myTestCtrlISNewFlag = false;        //140530_2
                }
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
                mytip.SetToolTip(currlst, "List of Flow control...");
                mytip.SetToolTip(cboItemName, "ItemName of current Flow control[unique]");
                mytip.SetToolTip(cboChannel, "Module Channel setting: [0,1,2,3,4]:0 All channel No will test in this Flow Control!");
                mytip.SetToolTip(cboVcc, "Module Voltage setting: 3.3V or others...");
                mytip.SetToolTip(cboTemp, "Module Temperature setting:");
                mytip.SetToolTip(cboDataRate, "DataRate(unit: bit/s)");
                mytip.SetToolTip(cboPattent, "Pattern select: 7,15,23,31 or others...");
                mytip.SetToolTip(txtAuxAttribles, "AuxAttribles: The maintain area will be shown after the left mouse button click");
                mytip.SetToolTip(lblItemName, "Pls double click the left mouse button at '" + lblItemName.Text + " ' then  the '" + lblItemName.Text + "' will be able to be changed");
                mytip.SetToolTip(chkIgnoreFlag, "<V>: this flow control will be ignored...");
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
                tempTestPlanName = "";
                cboItemName.Enabled = false;
                //txtTestPlanName.Text = this.PName;
                if (blnAddNew)
                {
                    this.currlst.Enabled = false;
                    runPrmtrMsgState((byte)MsgState.AddNew); //140709_2
                }
                else
                {
                    this.currlst.Enabled = true;

                    runPrmtrMsgState((byte)MsgState.Clear); //140709_2
                }

                DataRow[] CurrEquipLst = MainForm.TopoToatlDS.Tables["TopoTestControl"].Select("PID=" + PID + "");
                // = MainForm.mySqlIO.GetDataTable("", "");
                foreach (DataRow dr in CurrEquipLst)
                {
                    currlst.Items.Add(dr["ItemName"]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        
        void getInfoFromDT(DataTable mydt, int currIndex)
        {   //ID	PID	Name	SEQ	Channel	Temp	Vcc	Pattent	DataRate	AuxAttribles
            try
            {
                this.txtTempOffset.Text = "";
                this.txtDescription.Text = "";
                cboItemName.Enabled = false;
                string filterString = currlst.SelectedItem.ToString();
                tempTestPlanName = filterString;
                DataRow[] myROWS = mydt.Select("ItemName='" + filterString + "' and PID=" + PID + "");
                if (myROWS.Length == 1)
                {
                    myTestCtrlID = myROWS[0]["ID"].ToString();
                    this.cboItemName.Text = myROWS[0]["ItemName"].ToString();
                    mySEQ = myROWS[0]["SEQ"].ToString();
                    this.cboChannel.Text=myROWS[0]["Channel"].ToString();
                    this.cboTemp.Text=myROWS[0]["Temp"].ToString();
                    this.cboVcc.Text=myROWS[0]["Vcc"].ToString();
                    this.cboPattent.Text=myROWS[0]["Pattent"].ToString();
                    this.cboDataRate.Text=myROWS[0]["DataRate"].ToString();
                    this.txtAuxAttribles.Text=myROWS[0]["AuxAttribles"].ToString();
                    this.chkIgnoreFlag.Checked = Convert.ToBoolean(myROWS[0]["IgnoreFlag"].ToString()); //141028_0
                    this.txtTempOffset.Text = myROWS[0]["TempOffset"].ToString();   //150512
                        
                    if (MainForm.TopoToatlDS.Tables["TopoTestControl"].Columns.Contains("ItemDescription"))  //150411_0
                    {
                        this.txtDescription.Text = myROWS[0]["ItemDescription"].ToString();
                    }

                    if (MainForm.TopoToatlDS.Tables["TopoTestControl"].Columns.Contains("CtrlType"))  //150414_0
                    {   
                        if (myROWS[0]["CtrlType"].ToString()=="2")
                        {
                            this.cboCtrlType.Text = "FMT";
                        }
                        else if (myROWS[0]["CtrlType"].ToString()=="1")
                        {
                            this.cboCtrlType.Text = "LP";
                        }
                        else if (myROWS[0]["CtrlType"].ToString() == "3")
                        {
                            this.cboCtrlType.Text = "Both";
                        }
                        else
                        {
                            this.cboCtrlType.Text = "FMT";
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Error... There were" + myROWS.Length + (" records existed; \n filterString--> ItemName='" + filterString + "' and PID=" + PID + ""));
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
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
                        MessageBox.Show("Current ItemName has been existed:-> " + NewName);

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

        bool EditInfoForDT(DataTable mydt)
        {
            bool Result = false;
            string[] myColumnLst = new string[] 
            { "ID", "PID", "ItemName", "SEQ", "Channel", "Temp", "Vcc", "Pattent", "DataRate", "AuxAttribles" };

            try
            {   
                //先判定当前资料是否有空资料!
                if (blnAddNew && NotFoundName(cboItemName.Text.ToString()) == false)
                {
                    MessageBox.Show("Add a new record Error!Pls confirm the ItemName!!! ");
                    if (this.cboItemName.Enabled)
                    {
                        this.cboItemName.Focus();
                        this.cboItemName.Text = ""; //140709_1 //重复则清空名称
                    }
                    return Result;
                }
                else
                {
                    if (this.cboItemName.Text.ToString().Trim().Length == 0 ||                        
                        this.cboChannel.Text.ToString().Trim().Length == 0 ||
                        this.cboTemp.Text.ToString().Trim().Length == 0 ||
                        this.cboVcc.Text.ToString().Trim().Length == 0 ||
                        this.cboPattent.Text.ToString().Trim().Length == 0 ||
                        this.cboDataRate.Text.ToString().Trim().Length == 0 ||
						this.txtTempOffset.Text.ToString().Trim().Length == 0) //150504 || this.txtAuxAttribles.Text.ToString().Trim().Length == 0
                    {
                        MessageBox.Show("Incomplete information, please confirm!");
                        return Result;
                    }
                    else
                    {
                        string filterString = this.cboItemName.Text.ToString();
                        //DataRow[] myROWS = mydt.Select("ItemName='" + filterString + "' and PID=" + PID );
                        DataRow[] myROWS = mydt.Select("ID=" + myTestCtrlID);
                        
                        if (myROWS.Length == 1)
                        {
                            if (this.blnAddNew)
                            {
                                MessageBox.Show("Add a new record Error!Pls confirm the ItemName!!! " + myROWS.Length + (" records existed; \n filterString--> ItemName='" + filterString + "' and PID=" + PID + ""));
                                return Result;
                            }
                            //"ID", "PID", "ItemName", "SEQ", "Channel", "Temp", "Vcc", "Pattent", "DataRate", "AuxAttribles"
                            myROWS[0].BeginEdit();
                            myROWS[0]["ItemName"] = this.cboItemName.Text.ToString();
                            myROWS[0]["SEQ"] = mySEQ;
                            myROWS[0]["Channel"] = this.cboChannel.Text.ToString();
                            myROWS[0]["Temp"] = this.cboTemp.Text.ToString();
                            myROWS[0]["Vcc"] = this.cboVcc.Text.ToString();
                            myROWS[0]["Pattent"] = this.cboPattent.Text.ToString();
                            myROWS[0]["DataRate"] = this.cboDataRate.Text.ToString();
                            myROWS[0]["TempOffset"] = this.txtTempOffset.Text.ToString();
                            myROWS[0]["AuxAttribles"] = this.txtAuxAttribles.Text.ToString();
                            myROWS[0]["IgnoreFlag"] = this.chkIgnoreFlag.Checked.ToString();    //141028_0
                            if (MainForm.TopoToatlDS.Tables["TopoTestControl"].Columns.Contains("ItemDescription"))  //150411_0
                            {
                                myROWS[0]["ItemDescription"] = this.txtDescription.Text;
                            }
                            if (MainForm.TopoToatlDS.Tables["TopoTestControl"].Columns.Contains("CtrlType"))  //150411_0
                            {
                                if (this.cboCtrlType.Text.ToUpper() == "FMT")
                                {
                                    myROWS[0]["CtrlType"] = 2;
                                }
                                else if (this.cboCtrlType.Text.ToUpper() == "LP")
                                {
                                    myROWS[0]["CtrlType"] = 1;
                                }
                                else if (this.cboCtrlType.Text.ToUpper() == "BOTH")
                                {
                                    myROWS[0]["CtrlType"] = 3;
                                }
                                else
                                {
                                    myROWS[0]["CtrlType"] = 2;
                                }
                            }
                            myROWS[0].EndEdit();
                            Result = true;
                            RefreshList();  //此方法会执行刷新ListItem的动作~
                        }
                        else if (this.blnAddNew && myROWS.Length == 0)  //141106_0
                        {
                            DataRow myNewRow = mydt.NewRow();
                            myNewRow.BeginEdit();
                            myNewRow["ID"] = myTestCtrlID; //140709_1
                            myNewRow["PID"] = this.PID.ToString();
                            myNewRow["ItemName"] = this.cboItemName.Text.ToString();
                            myNewRow["SEQ"] = mySEQ;
                            myNewRow["Channel"] = this.cboChannel.Text.ToString();
                            myNewRow["Temp"] = this.cboTemp.Text.ToString();
                            myNewRow["Vcc"] = this.cboVcc.Text.ToString();
                            myNewRow["Pattent"] = this.cboPattent.Text.ToString();
                            myNewRow["DataRate"] = this.cboDataRate.Text.ToString();
                            myNewRow["TempOffset"] = this.txtTempOffset.Text.ToString();
                            myNewRow["AuxAttribles"] = this.txtAuxAttribles.Text.ToString();    //140811_2
                            myNewRow["IgnoreFlag"] = this.chkIgnoreFlag.Checked.ToString(); //141028_0
                            if (MainForm.TopoToatlDS.Tables["TopoTestControl"].Columns.Contains("ItemDescription"))  //150411_0
                            {
                                myNewRow["ItemDescription"] = this.txtDescription.Text;
                            }
                            if (MainForm.TopoToatlDS.Tables["TopoTestControl"].Columns.Contains("CtrlType"))  //150411_0
                            {
                                if (this.cboCtrlType.Text.ToUpper() == "FMT")
                                {
                                    myNewRow["CtrlType"] = 2;
                                }
                                else if (this.cboCtrlType.Text.ToUpper() == "LP")
                                {
                                    myNewRow["CtrlType"] = 1;
                                }
                                else if (this.cboCtrlType.Text.ToUpper() == "BOTH")
                                {
                                    myNewRow["CtrlType"] = 3;
                                }
                                else
                                {
                                    myNewRow["CtrlType"] = 2;
                                }
                            }
                            mydt.Rows.Add(myNewRow);

                            myNewRow.EndEdit();
                            MainForm.mylastIDTestCtrl++;
                            MainForm.myAddCountTestCtrl++;
                            RefreshList();  //此方法会执行刷新ListItem的动作~
                            blnAddNew = false;  //140530_1
                            int myNewRowIndex = (currlst.Items.Count - 1);
                            
                            currlst.Enabled = true;
                            currlst.Focus();
                            currlst.SelectedIndex = myNewRowIndex;
                           

                            //blnAddNew = false;  //新增一条记录后将新增标志改为false;    140528 TBD 新增需要维护完全信息                 
                            Result = true;
                        }
                        else
                        {
                            MessageBox.Show("Add a new record Error!Pls confirm the ItemName!!! About " + myROWS.Length + (" records existed; \n filterString--> ItemName='" + filterString + "' and PID=" + PID + ""));
                        }
                    }
                    RefreshList(); //140530_1
                    return Result;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return Result;
            }
        }

        private void currlst_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (currlst.SelectedIndex != -1)
                {
                    btnOK.Enabled = true;
                    getInfoFromDT(MainForm.TopoToatlDS.Tables["TopoTestControl"], currlst.SelectedIndex);
                    runPrmtrMsgState((byte)MsgState.Edit); //140709_2
                    TestCtrlName = currlst.SelectedItem.ToString();
                }
                else
                {
                    TestCtrlName = "";
                    btnOK.Enabled = false;
                    MessageBox.Show("Pls choose a item first");
                }
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
                if (MainForm.checkItemLength("CurrItemName", cboItemName.Text, 30)
                    || MainForm.checkItemLength("DataRate ", cboDataRate.Text, 50)
                    || MainForm.checkItemLength("AuxAttribles ", txtAuxAttribles.Text, 1024))
                {
                    return;
                }

                if (MainForm.TopoToatlDS.Tables["TopoTestControl"].Columns.Contains("ItemDescription"))  //150411_0
                {
                    if (MainForm.checkItemLength("ItemDescription", this.txtDescription.Text, 200))
                    {
                        return;
                    }
                }

                if (this.cboItemName.Text.ToString().Trim().Length == 0 ||
                    this.cboChannel.Text.ToString().Trim().Length == 0 ||
                    this.cboTemp.Text.ToString().Trim().Length == 0 ||
                    this.cboVcc.Text.ToString().Trim().Length == 0 ||
                    this.cboPattent.Text.ToString().Trim().Length == 0 ||
                    this.cboDataRate.Text.ToString().Trim().Length == 0 ||
                    this.txtTempOffset.Text.ToString().Trim().Length == 0)   //||this.txtAuxAttribles.Text.ToString().Trim().Length == 0
                    //140606 Add
                {
                    MessageBox.Show("Some data information is null!Please confirm!");
                    return ;
                }
                bool Result = EditInfoForDT(MainForm.TopoToatlDS.Tables["TopoTestControl"]);

                if (Result)
                {
                    runPrmtrMsgState((byte)MsgState.SaveOK);    //140709_2
                    setAuxshow(false);  //140811_0
                    MainForm.ISNeedUpdateflag = true; //140603_2
                    cboItemName.BackColor = Color.White;
                    cboItemName.Enabled = false;    //140530_4
                    btnFinish.Enabled = true;
                    MainForm.myTestCtrlAddOKFlag = true; //140529_1
                    //if (blnAddNew)
                    if (blnAddNew)  //140706_1  //(MainForm.myTestCtrlAddOKFlag == true) //140530_1
                    {
                        if (TestCtrlName.Length >0)
                        {
                            ModelInfo myModelInfo = new ModelInfo();
                            myModelInfo.blnAddNewModel = blnAddNew; //140430_1 TBD
                            MainForm.myTestModelAddOKFlag = false;
                            myModelInfo.PID = Convert.ToInt64(MainForm.getDTColumnInfo(MainForm.TopoToatlDS.Tables["TopoTestControl"], "ID", "PID=" + PID + " and ItemName='" + TestCtrlName + "'"));
                            myModelInfo.TestCtrlName = TestCtrlName;
                            myModelInfo.ShowDialog();       //show NextForm...

                            blnAddNew = false;  //新增一条记录后将新增标志改为false;    140528 TBD 新增需要维护完全信息
                            this.Close(); //140530_4
                        }
                    }
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

        private void btnFinish_Click(object sender, EventArgs e)
        {            
            this.Close();
        }

        private void CtrlInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.blnAddNew)
            {
                blnAddNew = false;//141030_1
                return; 
            }
        }

        private void cboChannel_Leave(object sender, EventArgs e)
        {
            if (MainForm.ISNotInSpec("Module Channel No", cboChannel.Text.ToString(), 0, 4))
            {
                cboChannel.Focus();
                return;
            }            
        }

        private void cboTemp_Leave(object sender, EventArgs e)
        {
            if (MainForm.ISNotInSpec("temperature", cboTemp.Text.ToString(), -120, 200))
            {
                cboTemp.Focus();
                return;
            }
        }

        private void cboVcc_Leave(object sender, EventArgs e)
        {
            if (MainForm.ISNotInSpec("Voltage", cboVcc.Text.ToString(), 0, 12))
            {
                cboVcc.Focus();
                return;
            }
        }

        private void cboPattent_Leave(object sender, EventArgs e)
        {
            if (MainForm.ISNotInSpec("Pattern", cboPattent.Text.ToString(), 0, 31))
            {
                cboVcc.Focus();
                return;
            }
        }

        private void lblItemName_DoubleClick(object sender, EventArgs e)
        {
            cboItemName.Enabled = true;
        }


        private void txtTempOffset_Leave(object sender, EventArgs e)
        {
            if (!MainForm.checkTypeOK(this.txtTempOffset.Text,"Single"))
            {
                txtTempOffset.Focus();
                return;
            }
        }

        private void cboItemName_Leave(object sender, EventArgs e)  //140709_2 防止资料未完整时删错资料!
        {           
            //先判定当前资料是否有空资料!
            if (cboItemName.Enabled && cboItemName.Text.ToString() != tempTestPlanName)
            {
                if (NotFoundName(cboItemName.Text.ToString()) == false)
                {
                    MessageBox.Show("The New data is incorrect:Name has been existed !Please confirm!");
                    if (this.cboItemName.Enabled)
                    {
                        this.cboItemName.Focus();
                        this.cboItemName.Text = ""; //140709_1 //重复则清空名称
                    }
                }
            }
        }

        enum MsgState //140709_2
        {
            SaveOK = 0,
            Edit = 1,
            AddNew = 2,
            Delete = 3,
            Clear = 4
        }
        void runPrmtrMsgState(byte state) //140709_2
        {
            try
            {
                this.cboItemName.Enabled = false;
                if (state == (byte)MsgState.AddNew)
                {
                    this.txtShowInfo.Text = "Operator state : Add New!";
                    this.txtShowInfo.BackColor = Color.Yellow;
                    this.cboItemName.BackColor = Color.Yellow;
                    this.cboItemName.Enabled = true;
                }
                else if (state == (byte)MsgState.Edit)
                {
                    this.txtShowInfo.Text = "Operator state : Edit!";
                    this.txtShowInfo.BackColor = Color.GreenYellow;
                    this.cboItemName.BackColor = Color.GreenYellow;
                }
                else if (state == (byte)MsgState.SaveOK)
                {
                    this.txtShowInfo.Text = "ItemName = " + this.cboItemName.Text + " Save OK!";
                    this.txtShowInfo.BackColor = Color.YellowGreen;
                    this.cboItemName.BackColor = Color.YellowGreen;
                }
                else if (state == (byte)MsgState.Delete)
                {
                    this.txtShowInfo.Text = "ItemName = " + this.cboItemName.Text + " has been deleted!";
                    this.txtShowInfo.BackColor = Color.Pink;
                    this.cboItemName.BackColor = Color.Pink;
                }
                else if (state == (byte)MsgState.Clear)
                {
                    this.txtShowInfo.Text = "";
                    this.txtShowInfo.BackColor = Color.White;
                    this.cboItemName.BackColor = Color.White;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void cboDataRate_MouseClick(object sender, MouseEventArgs e)
        {
            txtShowInfo.Text = lblDataRate.Text+ ": " + cboDataRate.Text;
        }

        private void cboItemName_MouseClick(object sender, MouseEventArgs e)
        {
            txtShowInfo.Text = lblItemName.Text + ": " + cboItemName.Text;
        }

        private void cboPattent_MouseClick(object sender, MouseEventArgs e)
        {
            txtShowInfo.Text = lblPattent.Text + ": " + cboPattent.Text;
        }

        private void cboTemp_MouseClick(object sender, MouseEventArgs e)
        {
            txtShowInfo.Text = lblTemp.Text + ": " + cboTemp.Text;
        }

        private void cboVcc_MouseClick(object sender, MouseEventArgs e)
        {
            txtShowInfo.Text = lblVcc.Text + ": " + cboVcc.Text;
        }
        private void cboChannel_MouseClick(object sender, MouseEventArgs e)
        {
            txtShowInfo.Text = lblChannel.Text + ": " + cboChannel.Text;
        }

        //140811
        //=====================================================>>>
        void setAuxshow(bool state) //140811_0
        {
            try
            {
                grpPrmtr.Visible = state;
                if (state)
                {
                    this.Size = new Size(859, 480);
                }
                else
                {
                    this.Size = new Size(489, 480);
                }
                this.Refresh();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void getDGVInfo()
        {
            try
            {
                string ss = this.txtAuxAttribles.Text;
                dgvPrmtr.Rows.Clear();
                if (ss.Length > 0)
                {
                    string[] myKeyValue = ss.Split(';');
                    for (int i = 0; i < myKeyValue.Length; i++)
                    {
                        string[] sss = myKeyValue[i].Split('=');
                        dgvPrmtr.Rows.Add(sss);
                        if (i == 0)
                        {
                            this.cboPrmtrItem.Text = sss[0];
                            if (sss.Length >= 2)
                            {
                                this.cboPrmtrValue.Text = sss[1];
                            }
                        }
                    }
                    
                }
                else
                {
                    btnPrmtrDelete.Enabled = false;
                    btnPrmtrSave.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void showAuxTxt()
        {
            string ss = "";
            try
            {
                for (int i = 0; i < dgvPrmtr.RowCount; i++)
                {
                    if (i + 1 < dgvPrmtr.RowCount)
                    {
                        ss = ss + dgvPrmtr.Rows[i].Cells[0].Value.ToString() + "=" + dgvPrmtr.Rows[i].Cells[1].Value.ToString() + ";";
                    }
                    else
                    {
                        ss = ss + dgvPrmtr.Rows[i].Cells[0].Value.ToString() + "=" + dgvPrmtr.Rows[i].Cells[1].Value.ToString();
                        break;
                    }
                }
                //140811_2 this.txtAuxAttribles.DropDownStyle = ComboBoxStyle.DropDown;
                this.txtAuxAttribles.Text = ss;
                if (dgvPrmtr.Rows.Count <=0 )
                {
                    btnPrmtrDelete.Enabled = false;
                    btnPrmtrSave.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void runAuxMsgState(byte state) //140811_1
        {
            try
            {
                blnIsAddAux = true;
                if (state == (byte)MsgState.AddNew)
                {
                    this.txtShowInfo.Text = "Operator state : AddNew AuxAttribles !";
                    this.txtShowInfo.BackColor = Color.Yellow;
                    this.cboPrmtrItem.BackColor = Color.Yellow;
                    this.cboPrmtrValue.BackColor = Color.Yellow;
                    btnPrmtrSave.Enabled = true;
                    btnPrmtrDelete.Enabled = true;
                    cboPrmtrItem.Text = "";
                    cboPrmtrValue.Text = "";
                }
                else if (state == (byte)MsgState.Edit)
                {
                    this.txtShowInfo.Text = "Operator state : Edit AuxAttribles !";
                    this.txtShowInfo.BackColor = Color.GreenYellow;
                    this.cboPrmtrItem.BackColor = Color.GreenYellow;
                    this.cboPrmtrValue.BackColor = Color.GreenYellow;
                    blnIsAddAux = false;
                }
                else if (state == (byte)MsgState.SaveOK)
                {
                    this.txtShowInfo.Text = "The AuxAttribles of " + this.cboItemName.Text + " Save OK!";  
                    this.txtShowInfo.BackColor = Color.YellowGreen;
                    this.cboPrmtrItem.BackColor = Color.YellowGreen;
                    this.cboPrmtrValue.BackColor = Color.YellowGreen;
                    blnIsAddAux = false;                    
                }
                else if (state == (byte)MsgState.Delete)
                {
                    this.txtShowInfo.Text = "The AuxAttribles of " + this.cboItemName.Text + " has been deleted!";
                    this.txtShowInfo.BackColor = Color.Pink;
                    this.cboPrmtrItem.BackColor = Color.Pink;
                    this.cboPrmtrValue.BackColor = Color.Pink;
                    cboPrmtrItem.Text = "";
                    cboPrmtrValue.Text = "";
                    blnIsAddAux = false;
                }
                else if (state == (byte)MsgState.Clear)
                {
                    this.txtShowInfo.Text = "";
                    this.txtShowInfo.BackColor = Color.White;
                    this.cboPrmtrItem.BackColor = Color.White;
                    this.cboPrmtrValue.BackColor = Color.White;
                    cboPrmtrItem.Text = "";
                    cboPrmtrValue.Text = "";
                    blnIsAddAux = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        bool auxItemExisted(string findString)
        {
            bool result = false;
            try
            {
                string ss = this.txtAuxAttribles.Text;
                
                if (ss.Length > 0)
                {
                    string[] myKeyValue = ss.Split(';');
                    for (int i = 0; i < myKeyValue.Length; i++)
                    {
                        string[] sss = myKeyValue[i].Split('=');
                        if (sss[0].ToUpper() == findString.ToUpper())
                        {
                            result = true;
                        }
                    }
                }
                
                return result;
            }
            catch (Exception ex)
            {                
                MessageBox.Show(ex.ToString());
                return result;
            }
            
        }


        private void btnPrmtrAdd_Click(object sender, EventArgs e)
        {
            try
            {
                blnIsAddAux = true;
                runAuxMsgState((byte)MsgState.AddNew);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnPrmtrSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (blnIsAddAux && auxItemExisted(cboPrmtrItem.Text.ToString()))
                {
                    MessageBox.Show("The key <" + cboPrmtrItem.Text.ToString() + ">has existed, Pls choose a new name!!!");
                    return;
                }
                else if (cboPrmtrItem.Text.ToString().Length > 0)
                {
                    if (blnIsAddAux)
                    {
                        string[] ss = new string[2] { cboPrmtrItem.Text.ToString(), cboPrmtrValue.Text.ToString() };
                        dgvPrmtr.Rows.Add(ss);
                    }
                    else
                    {
                        dgvPrmtr.CurrentRow.Cells[0].Value = cboPrmtrItem.Text.ToString();
                        dgvPrmtr.CurrentRow.Cells[1].Value = cboPrmtrValue.Text.ToString();
                    }
                    dgvPrmtr.CurrentCell = dgvPrmtr.CurrentRow.Cells[0];
                    runAuxMsgState((byte)MsgState.SaveOK);
                    showAuxTxt();
                }
                else
                {
                    MessageBox.Show("Something is null!Pls confirm again!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnPrmtrDelete_Click(object sender, EventArgs e)
        {
            if (blnIsAddAux)
            {
                blnIsAddAux = false;
                MessageBox.Show("Cancel:Add new aux.");
                runAuxMsgState((byte)MsgState.Clear);
               
                return;
            }

            if (dgvPrmtr.CurrentRow == null || dgvPrmtr.CurrentRow.Index == -1)
            {
                MessageBox.Show("Pls choose a item first!");
            }
            else
            {
                try
                {
                    DialogResult drst = new DialogResult();
                    string myDeleteItemName = dgvPrmtr.CurrentRow.Cells[0].Value.ToString();
                    drst = (MessageBox.Show("Delete item: -->" + myDeleteItemName + "\n \n Choose 'Y' (是) to continue?", "Notice",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2));

                    if (drst == DialogResult.Yes)
                    {
                        dgvPrmtr.Rows.RemoveAt(dgvPrmtr.CurrentRow.Index);
                        blnIsAddAux = false;
                        MessageBox.Show("ItemName : " + myDeleteItemName + "has been deleted!");
                        
                        showAuxTxt();
                        runAuxMsgState((byte)MsgState.Delete);
                    }
                    else
                    {
                        MessageBox.Show("ItemName : " + myDeleteItemName + " deleted fail..."); ;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }            
        }

        private void dgvPrmtr_MouseClick(object sender, MouseEventArgs e)
        {
            if (dgvPrmtr.CurrentRow != null)
            {
                this.cboPrmtrItem.Text = dgvPrmtr.CurrentRow.Cells[0].Value.ToString();
                this.cboPrmtrValue.Text = dgvPrmtr.CurrentRow.Cells[1].Value.ToString();
                runAuxMsgState((byte)MsgState.Edit);
            }
        }

        private void txtAuxAttribles_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                txtShowInfo.Text = lblAuxAttribles.Text + ": " + txtAuxAttribles.Text;
                setAuxshow(true);
                getDGVInfo();

                if (txtAuxAttribles.Text.Length <=0)
                {
                    blnIsAddAux = true;
                    runAuxMsgState((byte)MsgState.AddNew);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void chkIgnoreFlag_CheckedChanged(object sender, EventArgs e)   
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

        private void dgvPrmtr_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dgvPrmtr.CurrentRow != null)
            {
                this.cboPrmtrItem.Text = dgvPrmtr.CurrentRow.Cells[0].Value.ToString();
                this.cboPrmtrValue.Text = dgvPrmtr.CurrentRow.Cells[1].Value.ToString();
                runAuxMsgState((byte)MsgState.Edit);
            }
        }

        //=====================================================<<<
    }
}

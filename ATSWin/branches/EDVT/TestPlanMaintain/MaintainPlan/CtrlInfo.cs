using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;

namespace TestPlan
{
    public partial class CtrlInfo : Form
    {
        public CtrlInfo()
        {
            InitializeComponent();
        }

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
                setAuxshow(false);   //140811_1
                RefreshList();
                ShowMyTip();
                this.txtTestPlanName.Text = TestPlanName;

                //140709_1>>>>>>>>>>
                this.cboDataRate.Items.Clear();
                //140811_2 this.txtAuxAttribles.Items.Clear(); 
                //140811_2 this.txtAuxAttribles.Items.Add("DUT_Soak_Time= 指定值; ReadTempFromXStream= 指定值"); //140606
                DataRow[] drs = PNInfo.TopoToatlDS.Tables["TopoTestControl"].Select("");

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
                    
                    mySEQ = (PNInfo.getMAXColumnsItem(PNInfo.TopoToatlDS.Tables["TopoTestControl"], "SEQ", "PID=" + PID) + 1).ToString();
                    myTestCtrlID = (PNInfo.mylastIDTestCtrl + 1).ToString();

                    PNInfo.myTestCtrlAddOKFlag = false;
                    cboItemName.BackColor = Color.Yellow;
                    currlst.Enabled = false;
                    btnOK.Enabled = true;
                    PNInfo.myTestCtrlISNewFlag = true;        //140530_2
                }
                else
                {
                    btnOK.Enabled = false;
                    btnFinish.Enabled = true;
                    PNInfo.myTestCtrlISNewFlag = false;        //140530_2
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
                mytip.SetToolTip(currlst, "TestPlan列表信息");
                mytip.SetToolTip(cboItemName, "测试流程的名称[唯一]");
                mytip.SetToolTip(cboChannel, "模块通道号[0,1,2,3,4],0表示全部测试");
                mytip.SetToolTip(cboVcc, "模块电压设定,一般为3.3V");
                mytip.SetToolTip(cboTemp, "模块温度设定");
                mytip.SetToolTip(cboDataRate, "模块速率设置(单位bit/s)");
                mytip.SetToolTip(cboPattent, "PPG码型(PRBS),一般为 7,15,23,31.");
                mytip.SetToolTip(txtAuxAttribles, "其他属性设置: 点击后显示维护界面!");
                mytip.SetToolTip(lblItemName, "双击 '" + lblItemName.Text + " '将启用修改 '" + lblItemName.Text + "'");
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

                DataRow[] CurrEquipLst = PNInfo.TopoToatlDS.Tables["TopoTestControl"].Select("PID=" + PID + "");
                // = PNInfo.mySqlIO.GetDataTable("", "");
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
                string filterString = currlst.SelectedItem.ToString();
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

        bool NotFoundName(string NewName)
        {
            bool result = false;
            try
            {
                for (int i = 0; i < currlst.Items.Count; i++)
                {
                    if (NewName.ToUpper().Trim() == currlst.Items[i].ToString().ToUpper().Trim())
                    {
                        MessageBox.Show("已存在TestModel项目:-> " + NewName);

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
                    MessageBox.Show("新增资料有误Name重复!请确认!!! ");
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
                        this.txtAuxAttribles.Text.ToString().Trim().Length == 0) //140811_2 
                    {
                        MessageBox.Show("新增资料有误资料为空白!请确认!!! ");
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
                                MessageBox.Show("新增资料有误!请确认!!! 已有" + myROWS.Length + ("条记录; \n 条件--> ItemName='" + filterString + "' and PID=" + PID + ""));
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
                            myROWS[0]["AuxAttribles"] = this.txtAuxAttribles.Text.ToString();
                            myROWS[0].EndEdit();
                            Result = true;
                            RefreshList();  //此方法会执行刷新ListItem的动作~
                        }
                        else if (this.blnAddNew)
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
                            myNewRow["AuxAttribles"] = this.txtAuxAttribles.Text.ToString();    //140811_2
                            mydt.Rows.Add(myNewRow);

                            myNewRow.EndEdit();
                            PNInfo.mylastIDTestCtrl++;
                            PNInfo.myAddCountTestCtrl++;
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
                            MessageBox.Show("资料有误!请确认!!! 共有" + myROWS.Length + ("条记录; \n 条件--> ItemName='" + filterString + "' and PID=" + PID + ""));
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
                    getInfoFromDT(PNInfo.TopoToatlDS.Tables["TopoTestControl"], currlst.SelectedIndex);
                    runPrmtrMsgState((byte)MsgState.Edit); //140709_2
                    TestCtrlName = currlst.SelectedItem.ToString();
                }
                else
                {
                    TestCtrlName = "";
                    btnOK.Enabled = false;
                    MessageBox.Show("请选择对应内容!");
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
                if (this.cboItemName.Text.ToString().Trim().Length == 0 ||
                    this.cboChannel.Text.ToString().Trim().Length == 0 ||
                    this.cboTemp.Text.ToString().Trim().Length == 0 ||
                    this.cboVcc.Text.ToString().Trim().Length == 0 ||
                    this.cboPattent.Text.ToString().Trim().Length == 0 ||
                    this.cboDataRate.Text.ToString().Trim().Length == 0 ||
                    this.txtAuxAttribles.Text.ToString().Trim().Length == 0)
                    //140606 Add
                {
                    MessageBox.Show("部分资料资料为空白!请确认后再保存!!! ");
                    return ;
                }
                bool Result = EditInfoForDT(PNInfo.TopoToatlDS.Tables["TopoTestControl"]);

                if (Result)
                {
                    runPrmtrMsgState((byte)MsgState.SaveOK);    //140709_2
                    setAuxshow(false);  //140811_0
                    PNInfo.ISNeedUpdateflag = true; //140603_2
                    cboItemName.BackColor = Color.White;
                    cboItemName.Enabled = false;    //140530_4
                    btnFinish.Enabled = true;
                    PNInfo.myTestCtrlAddOKFlag = true; //140529_1
                    //if (blnAddNew)
                    if (blnAddNew)  //140706_1  //(PNInfo.myTestCtrlAddOKFlag == true) //140530_1
                    {
                        if (TestCtrlName.Length >0)
                        {
                            ModelInfo myModelInfo = new ModelInfo();
                            myModelInfo.blnAddNewModel = blnAddNew; //140430_1 TBD
                            PNInfo.myTestModelAddOKFlag = false;
                            myModelInfo.PID = Convert.ToInt64(PNInfo.getDTColumnInfo(PNInfo.TopoToatlDS.Tables["TopoTestControl"], "ID", "PID=" + PID + " and ItemName='" + TestCtrlName + "'"));
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
                DialogResult myResult = MessageBox.Show(
                    "尚未完成资料维护!提前退出将可能无法保证资料完整,系统将自动删除当前维护项目资料!",
                    "注意:",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button1);
                try
                {
                    string queryCMD = "ItemName='" + this.cboItemName.Text.ToString() + "' And PID=" + this.PID;
                    int myExistCount = PNInfo.currPrmtrCountExisted(PNInfo.TopoToatlDS.Tables["TopoTestControl"], queryCMD);
                    if (myResult == DialogResult.OK)
                    {
                        if (myExistCount > 0)
                        {
                            PNInfo.DeleteItemForDT(PNInfo.TopoToatlDS.Tables["TopoTestControl"], queryCMD);
                            PNInfo.mylastIDTestCtrl = PNInfo.mylastIDTestCtrl - myExistCount;
                        }
                        blnAddNew = false;
                        PNInfo.myTestCtrlAddOKFlag = true;
                        this.Dispose();
                    }
                    //else //140604_1
                    //{
                    //    return; 
                    //}
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void cboChannel_Leave(object sender, EventArgs e)
        {
            if (PNInfo.ISNotInSpec("模块通道号", cboChannel.Text.ToString(), 0, 4))
            {
                cboChannel.Focus();
                return;
            }            
        }

        private void cboTemp_Leave(object sender, EventArgs e)
        {
            if ( PNInfo.ISNotInSpec("温度", cboTemp.Text.ToString(), -120, 200))
            {
                cboTemp.Focus();
                return;
            }
        }

        private void cboVcc_Leave(object sender, EventArgs e)
        {
            if (PNInfo.ISNotInSpec("电压", cboVcc.Text.ToString(), 0, 12))
            {
                cboVcc.Focus();
                return;
            }
        }

        private void cboPattent_Leave(object sender, EventArgs e)
        {
            if (PNInfo.ISNotInSpec("码型", cboPattent.Text.ToString(), 0, 31))
            {
                cboVcc.Focus();
                return;
            }
        }

        private void lblItemName_DoubleClick(object sender, EventArgs e)
        {
            cboItemName.Enabled = true;
        }

        private void cboItemName_Leave(object sender, EventArgs e)  //140709_2 防止资料未完整时删错资料!
        {           
            //先判定当前资料是否有空资料!
            if (blnAddNew && NotFoundName(cboItemName.Text.ToString()) == false)
            {
                MessageBox.Show("新增资料有误Name重复!请确认!!! ");
                if (this.cboItemName.Enabled)
                {
                    this.cboItemName.Focus();
                    this.cboItemName.Text = ""; //140709_1 //重复则清空名称
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
                    this.txtShowInfo.Text = "目前为新增参数状态,请在界面确认资料信息!";
                    this.txtShowInfo.BackColor = Color.Yellow;
                    this.cboItemName.BackColor = Color.Yellow;
                    this.cboItemName.Enabled = true;
                }
                else if (state == (byte)MsgState.Edit)
                {
                    this.txtShowInfo.Text = "目前为编辑参数状态,请在界面确认资料信息!";
                    this.txtShowInfo.BackColor = Color.GreenYellow;
                    this.cboItemName.BackColor = Color.GreenYellow;
                }
                else if (state == (byte)MsgState.SaveOK)
                {
                    this.txtShowInfo.Text = "参数项目资料 " + this.cboItemName.Text + "已经维护OK!";
                    this.txtShowInfo.BackColor = Color.YellowGreen;
                    this.cboItemName.BackColor = Color.YellowGreen;
                }
                else if (state == (byte)MsgState.Delete)
                {
                    this.txtShowInfo.Text = "参数资料信息已被删除! " + this.cboItemName.Text;
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
                    this.Size = new Size(859, 430);
                }
                else
                {
                    this.Size = new Size(489, 430);
                }
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
                    this.txtShowInfo.Text = "目前为新增其他属性参数状态,请在界面确认资料信息!";
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
                    this.txtShowInfo.Text = "目前为编辑其他属性参数状态,请在界面确认资料信息!";
                    this.txtShowInfo.BackColor = Color.GreenYellow;
                    this.cboPrmtrItem.BackColor = Color.GreenYellow;
                    this.cboPrmtrValue.BackColor = Color.GreenYellow;
                    blnIsAddAux = false;
                }
                else if (state == (byte)MsgState.SaveOK)
                {
                    this.txtShowInfo.Text = "其他属性参数项目资料 " + this.cboItemName.Text + "已经维护OK!";
                    this.txtShowInfo.BackColor = Color.YellowGreen;
                    this.cboPrmtrItem.BackColor = Color.YellowGreen;
                    this.cboPrmtrValue.BackColor = Color.YellowGreen;
                    blnIsAddAux = false;                    
                }
                else if (state == (byte)MsgState.Delete)
                {
                    this.txtShowInfo.Text = "其他属性参数资料信息已被删除! " + this.cboItemName.Text;
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
                    MessageBox.Show("项目资料 " + cboPrmtrItem.Text.ToString() + " 已经存在, 不允许保存!!!");
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
                    MessageBox.Show("项目资料 为空 不允许保存!!!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnPrmtrDelete_Click(object sender, EventArgs e)
        {
            if (dgvPrmtr.CurrentRow == null || dgvPrmtr.CurrentRow.Index == -1)
            {
                MessageBox.Show("请先选择需要删除的资料后再执行该操作!");
            }
            else
            {
                try
                {
                    DialogResult drst = new DialogResult();
                    string myDeleteItemName = dgvPrmtr.CurrentRow.Cells[0].Value.ToString();
                    drst = (MessageBox.Show("即将进行删除资料 -->" + myDeleteItemName + "\n \n 选择 'Y' (是) 继续?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2));

                    if (drst == DialogResult.Yes)
                    {
                        dgvPrmtr.Rows.RemoveAt(dgvPrmtr.CurrentRow.Index);
                        blnIsAddAux = false;
                        MessageBox.Show("项目资料 " + myDeleteItemName + "已经移除!");
                        
                        showAuxTxt();
                        runAuxMsgState((byte)MsgState.Delete);
                    }
                    else
                    {
                        MessageBox.Show("项目资料 " + myDeleteItemName + "移除失败!");
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
            this.cboPrmtrItem.Text = dgvPrmtr.CurrentRow.Cells[0].Value.ToString();
            this.cboPrmtrValue.Text = dgvPrmtr.CurrentRow.Cells[1].Value.ToString();
            runAuxMsgState((byte)MsgState.Edit);
        }

        private void txtAuxAttribles_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                txtShowInfo.Text = lblAuxAttribles.Text + ": " + txtAuxAttribles.Text;
                setAuxshow(true);
                getDGVInfo();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        //=====================================================<<<
    }
}

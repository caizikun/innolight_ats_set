using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Maintain
{
    public partial class PNInfoForm : Form
    {
        public PNInfoForm()
        {
            InitializeComponent();
        }

        public bool blnAddNew = false;
        public string PN_Name = "";
        public long PID = -1;
        string currMGroupID = "";
        string currPNID = "";
        string tempPN = "";

        void initType()
        {
            this.cboIgnore.Visible = false;
            this.lblIgnore.Visible = false;

            this.cboRxOverLoadDBm.Items.Clear();
            this.cboRxOverLoadDBm.Items.Add("0");

            this.cboUsingTempAD.Items.Clear();
            this.cboUsingTempAD.Items.Add("False");
            this.cboUsingTempAD.Items.Add("True");

            this.cboIgnore.Items.Clear();
            this.cboIgnore.Items.Add("False");
            this.cboIgnore.Items.Add("True");
            this.cboIgnore.Text = "False";

            this.cboAPC_Type.Items.Clear();
            for (int i = 0; i < 4; i++)
            {
                this.cboAPC_Type.Items.Add((APC_Type)i).ToString();
            }

            this.cboTEC_Present.Items.Clear();
            for (int i = 0; i < 5; i++)
            {
                this.cboTEC_Present.Items.Add((TECPresent)i).ToString();
            }

            this.cboBER.Items.Clear();
            for (int i = 0; i < 13; i++)
            {
                this.cboBER.Items.Add("1E-" + i).ToString();
            }

            this.cboCouple_Type.Items.Clear();
            for (int i = 0; i < 2; i++)
            {
                this.cboCouple_Type.Items.Add((Couple_Type)i).ToString();
            }

            this.cboAPCStyle.Items.Clear();
            for (int i = 0; i < 2; i++)
            {
                this.cboAPCStyle.Items.Add(i).ToString();
            }

            this.cboOldDriver.Items.Clear();
            for (int i = 0; i < 2; i++)
            {
                this.cboOldDriver.Items.Add(i).ToString();
            }

            this.cboMaxRate.Items.Clear();
            this.cboMaxRate.Items.Add("1");
            this.cboMaxRate.Items.Add("10");
            this.cboMaxRate.Items.Add("14");
            this.cboMaxRate.Items.Add("25");
            this.cboMaxRate.Items.Add("28");
        }

        private void PNInfoForm_Load(object sender, EventArgs e)
        {
            try
            {
                initType(); //150413_1
                RefreshList();
                ShowMyTip();

                this.Show();
                if (!blnAddNew && currlst.Items.Contains(PN_Name))
                {
                    currlst.SelectedItem = PN_Name;
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
                mytip.SetToolTip(currlst, "PN List");
                mytip.SetToolTip(this.cboVoltages, "Voltages <calibration>");
                mytip.SetToolTip(this.cboTsensors, "Tsensors <calibration>");
                mytip.SetToolTip(this.txtPN, "ProductionName");
                mytip.SetToolTip(this.txtItemName, "ItemName");
                mytip.SetToolTip(cboChannels, "Total Channels");
                mytip.SetToolTip(this.cboMGroup, "ManufactureCoefficientsGroup");
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
                cboChannels.Items.Clear();
                cboChannels.Text = "";
                txtItemName.Clear();
                txtPN.Clear();
                cboTsensors.Items.Clear();
                cboTsensors.Text = "";
                cboVoltages.Items.Clear();
                cboVoltages.Text = "";
                for (int i = 0; i < 5; i++)
                {
                    cboChannels.Items.Add(i.ToString());
                }

                for (int i = 0; i < 4; i++)
                {
                    cboVoltages.Items.Add(i.ToString());
                }
                for (int i = 0; i < 3; i++)
                {
                    cboTsensors.Items.Add(i.ToString());
                }

                if (blnAddNew)
                {
                    this.currlst.Enabled = false;
                    this.txtPN.Enabled = true;
                    this.btnOK.Enabled = true;
                    currPNID = (MainForm.mylastIDGlobalPN + 1).ToString();  //150310_0
                }
                else
                {
                    this.currlst.Enabled = true;
                    this.txtPN.Enabled = false;
                    this.btnOK.Enabled = true;
                }


                string sqlCondition = "PID=" + PID;

                DataRow[] mrDRs = MainForm.GlobalDS.Tables["GlobalProductionName"].Select(sqlCondition);
                for (int i = 0; i < mrDRs.Length; i++)
                {
                    this.currlst.Items.Add(mrDRs[i]["PN"].ToString());
                }
                                
                int MGroupIDCount = MainForm.GlobalDS.Tables["GlobalManufactureCoefficientsGroup"].Rows.Count;
                this.cboMGroup.Items.Clear();
                this.cboMGroup.Items.Add("");
                for (int i = 0; i < MGroupIDCount; i++)
                {   //140530_4
                    if (MainForm.GlobalDS.Tables["GlobalManufactureCoefficientsGroup"].Rows[i].RowState != DataRowState.Deleted)
                    {
                        if (MainForm.GlobalDS.Tables["GlobalManufactureCoefficientsGroup"].Rows[i]["TypeID"].ToString() == PID.ToString())
                        {
                            this.cboMGroup.Items.Add(MainForm.GlobalDS.Tables["GlobalManufactureCoefficientsGroup"].Rows[i]["ItemName"].ToString());
                        }
                    }
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
                if ( MainForm.checkItemLength("PN", this.txtPN.Text, 35)
                    || MainForm.checkItemLength("ItemName", this.txtItemName.Text, 200))
                {
                    return;
                }

                if (this.cboAPCStyle.Text.Trim().Length == 0 ||
                    this.cboAPC_Type.Text.Trim().Length == 0 ||
                    this.cboBER.Text.Trim().Length == 0 ||
                    this.cboCouple_Type.Text.Trim().Length == 0 ||
                    this.cboMaxRate.Text.Trim().Length == 0 ||
                    this.cboNickName.Text.Trim().Length == 0 ||
                    this.cboOldDriver.Text.Trim().Length == 0 ||
                    this.cboPublish_PN.Text.Trim().Length == 0 ||
                    this.cboTEC_Present.Text.Trim().Length == 0 ||

                    this.txtPN.Text.ToString().Trim().Length == 0 ||
                    this.txtItemName.Text.ToString().Trim().Length == 0 ||
                    this.cboVoltages.Text.ToString().Trim().Length == 0 ||
                    this.cboTsensors.Text.ToString().Trim().Length == 0 ||
                    this.cboMGroup.Text.ToString().Trim().Length == 0 ||
                    this.cboChannels.Text.ToString().Trim().Length == 0
                ) //140630_1
                {
                    MessageBox.Show("Data is incomplete,Pls confirm again?");
                    return;
                }

                if (blnAddNew || (tempPN.Length > 0 && tempPN.ToUpper() != txtPN.Text.ToString().ToUpper().Trim()))
                {
                    if (MainForm.currPrmtrCountExisted(MainForm.GlobalDS.Tables["GlobalProductionName"], "PID=" + PID + " and PN ='" + this.txtPN.Text.ToString() + "'") > 0)
                    {   //140704_1
                        MessageBox.Show("The data of PN has existed! <Violate unique rule>");
                        return;
                    }
                }

                bool result = EditInfoForDT(MainForm.GlobalDS.Tables["GlobalProductionName"]);

                if (result)
                {
                    MainForm.ISNeedUpdateflag = true; //140603_2
                    cboChannels.Enabled = true;
                    txtPN.Enabled = false;
                    if (MainForm.myGlobalPNISNewFlag)
                    {
                        //140704_2>>>>>>>>>
                        InitInfoForm myInitInfoForm = new InitInfoForm();
                        myInitInfoForm.PID = MainForm.getDTColumnInfo(MainForm.GlobalDS.Tables["GlobalProductionName"], "ID", "PN='" + txtPN.Text.ToString() + "'");
                        myInitInfoForm.PNName = txtPN.Text.ToString();
                        myInitInfoForm.ShowDialog();
                        //140704_2<<<<<<<<<

                        //myEquip.ShowDialog();   //show NextForm...
                        blnAddNew = false;
                        MainForm.myGlobalPNAddOKFlag = true; //140530_0
                        this.Close(); //140530_4
                    }
                    else
                    {
                        MainForm.myGlobalPNAddOKFlag = true; //140530_0
                    }
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
                    getInfoFromDT(MainForm.GlobalDS.Tables["GlobalProductionName"], currlst.SelectedIndex);
                    btnInitInfo.Enabled = true;    //140704_4
                    btnSpec.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        enum TECPresent
        {
            NoTECpresent = 0, OneTECpresent = 1, TwoTECpresent = 2, ThreeTECpresent = 3, FourTECpresent = 4
        }
        enum Couple_Type
        {//"0: DC 1: AC X: Reserved"
            DC = 0, AC = 1
        }

        enum APC_Type
        {//"0: None 1: Open-Loop 2: Close-Loop3: PID Close-Loop X: Reserved"
            None = 0, Open_Loop = 1,Close_Loop=2,PID_Close_Loop=3
        }

        //enum BER
        //{//"0: 1E-0 1: 1E-1 2: 1E-2 3: 1E-3 4: 1E-4 5: 1E-5 6: 1E-6 7: 1E-7 8: 1E-8 9: 1E-9 10: 1E-10 11: 1E-11 12: 1E-12 XX: Reserved"
        //    
        //}

        void getInfoFromDT(DataTable mydt, int currIndex)
        {
            try
            {
                //150413_2----------------------------------
                this.cboIgnore.Visible = false;
                this.lblIgnore.Visible = false;
                this.cboIgnore.Text = false.ToString();
                //------------------------------------------
                string filterString = currlst.SelectedItem.ToString();
                DataRow[] myROWS = mydt.Select("PN='" + filterString + "' and PID=" + PID);
                if (myROWS.Length == 1)
                {
                    currPNID = myROWS[0]["ID"].ToString();  //150310_0
                    tempPN = myROWS[0]["PN"].ToString();  //150310_0
                    this.txtPN.Enabled = true;              //150310_0
                    this.txtPN.Text = myROWS[0]["PN"].ToString();
                    this.txtItemName.Text = myROWS[0]["ItemName"].ToString();
                    this.cboChannels.Text = myROWS[0]["Channels"].ToString();
                    this.cboAPCStyle.Text = myROWS[0]["APCStyle"].ToString();   //0,1
                    this.cboTEC_Present.Text = ((TECPresent)Convert.ToInt32(myROWS[0]["TEC_Present"])).ToString();
                    this.cboCouple_Type.Text = ((Couple_Type) Convert.ToInt32(myROWS[0]["Couple_Type"])).ToString();
                    this.cboAPC_Type.Text = ((APC_Type)Convert.ToInt32(myROWS[0]["APC_Type"])).ToString();
                    this.cboBER.SelectedIndex = Convert.ToInt32(myROWS[0]["BER"]);
                    this.cboOldDriver.Text = myROWS[0]["OldDriver"].ToString(); //0,1
                    this.cboMaxRate.Text = myROWS[0]["MaxRate"].ToString();     
                    this.cboPublish_PN.Text = myROWS[0]["Publish_PN"].ToString();
                    this.cboNickName.Text = myROWS[0]["NickName"].ToString();
                    this.txtBiasFml.Text = myROWS[0]["IbiasFormula"].ToString();
                    this.txtModFml.Text = myROWS[0]["IModFormula"].ToString();
                    this.cboUsingTempAD.Text = myROWS[0]["UsingTempAD"].ToString(); 
                    string currMGroupName = MainForm.getDTColumnInfo(MainForm.GlobalDS.Tables["GlobalManufactureCoefficientsGroup"], "ItemName", "ID=" + myROWS[0]["MCoefsID"].ToString());
                    this.cboMGroup.Text = currMGroupName;

                    this.cboVoltages.Text = myROWS[0]["Voltages"].ToString(); //140703_2
                    this.cboTsensors.Text = myROWS[0]["Tsensors"].ToString(); //140703_2
                    this.cboRxOverLoadDBm.Text = myROWS[0]["RxOverLoadDBm"].ToString();
                    if (Convert.ToBoolean(myROWS[0]["IgnoreFlag"]))
                    {
                        this.cboIgnore.Visible = true;
                        this.lblIgnore.Visible = true;
                        this.cboIgnore.Text = myROWS[0]["IgnoreFlag"].ToString();
                    }
                }
                else
                {
                    MessageBox.Show("Error! \n " + myROWS.Length + " records existed; \n filterString--> PN='" + filterString + "' and PID=" + PID + "");
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
                string filterString = currPNID; //150310_0
                DataRow[] myROWS = mydt.Select("ID=" + filterString + " AND PID=" + PID); //150310_0
                if (myROWS.Length == 1)
                {
                    if (this.blnAddNew)
                    {
                        MessageBox.Show("Add new data error! \n " + myROWS.Length + " records existed; \n filterString--ID" + filterString + " and PID=" + PID + "");
                        return result;
                    }
                    myROWS[0].BeginEdit();
                    myROWS[0]["PID"] = this.PID;
                    myROWS[0]["PN"] = this.txtPN.Text.ToString();
                    myROWS[0]["ItemName"] = this.txtItemName.Text.ToString();
                    myROWS[0]["Channels"] = this.cboChannels.Text.ToString();

                    myROWS[0]["Voltages"] = this.cboVoltages.Text.ToString(); //140703_2
                    myROWS[0]["Tsensors"] = this.cboTsensors.Text.ToString(); //140703_2

                    currMGroupID = MainForm.getDTColumnInfo(MainForm.GlobalDS.Tables["GlobalManufactureCoefficientsGroup"], "ID", "ItemName='" + this.cboMGroup.Text + "'");
                    myROWS[0]["MCoefsID"] = currMGroupID;
                    myROWS[0]["IgnoreFlag"] = this.cboIgnore.Text;

                    myROWS[0]["APCStyle"] = this.cboAPCStyle.Text;
                    myROWS[0]["TEC_Present"] = (TECPresent)this.cboTEC_Present.SelectedIndex;
                    myROWS[0]["Couple_Type"] = (Couple_Type)this.cboCouple_Type.SelectedIndex;
                    myROWS[0]["APC_Type"] = (APC_Type)this.cboAPC_Type.SelectedIndex;
                    myROWS[0]["BER"] = this.cboBER.SelectedIndex;
                    myROWS[0]["OldDriver"] = this.cboOldDriver.Text;
                    myROWS[0]["MaxRate"] = this.cboMaxRate.Text;
                    myROWS[0]["Publish_PN"] = this.cboPublish_PN.Text;
                    myROWS[0]["NickName"] = this.cboNickName.Text;
                    myROWS[0]["IbiasFormula"] = this.txtBiasFml.Text;
                    myROWS[0]["IModFormula"] = this.txtModFml.Text;
                    myROWS[0]["UsingTempAD"] = this.cboUsingTempAD.Text;
                    myROWS[0]["RxOverLoadDBm"] = this.cboRxOverLoadDBm.Text;
                    myROWS[0].EndEdit();
                    result = true;
                    MainForm.myGlobalPNISNewFlag = false;
                    RefreshList();
                    if (currlst.Items.Contains(myROWS[0]["PN"]))
                    {
                        currlst.SelectedItem = myROWS[0]["PN"];
                    }
                }
                else if (this.blnAddNew && myROWS.Length == 0)
                {
                    DataRow myNewRow = mydt.NewRow();
                    myNewRow.BeginEdit();
                    myNewRow["ID"] = MainForm.mylastIDGlobalPN + 1;
                    myNewRow["PID"] = this.PID;
                    myNewRow["PN"] = this.txtPN.Text.ToString();
                    myNewRow["ItemName"] = this.txtItemName.Text.ToString();
                    myNewRow["Channels"] = this.cboChannels.Text.ToString();
                    myNewRow["Voltages"] = this.cboVoltages.Text.ToString(); //140703_2
                    myNewRow["Tsensors"] = this.cboTsensors.Text.ToString(); //140703_2
                    currMGroupID = MainForm.getDTColumnInfo(MainForm.GlobalDS.Tables["GlobalManufactureCoefficientsGroup"], "ID", "ItemName='" + this.cboMGroup.Text + "'");
                    myNewRow["MCoefsID"] = currMGroupID;
                    myNewRow["IgnoreFlag"] = false.ToString();
                    myNewRow["APCStyle"] = this.cboAPCStyle.Text;
                    myNewRow["TEC_Present"] = (TECPresent)this.cboTEC_Present.SelectedIndex;
                    myNewRow["Couple_Type"] = (Couple_Type)this.cboCouple_Type.SelectedIndex;
                    myNewRow["APC_Type"] = (APC_Type)this.cboAPC_Type.SelectedIndex;
                    myNewRow["BER"] = this.cboBER.SelectedIndex;
                    myNewRow["OldDriver"] = this.cboOldDriver.Text;
                    myNewRow["MaxRate"] = this.cboMaxRate.Text;
                    myNewRow["Publish_PN"] = this.cboPublish_PN.Text;
                    myNewRow["NickName"] = this.cboNickName.Text;
                    myNewRow["IbiasFormula"] = this.txtBiasFml.Text;
                    myNewRow["IModFormula"] = this.txtModFml.Text;
                    myNewRow["UsingTempAD"] = this.cboUsingTempAD.Text;
                    myNewRow["RxOverLoadDBm"] = this.cboRxOverLoadDBm.Text;
                    mydt.Rows.Add(myNewRow);

                    myNewRow.EndEdit();
                    MainForm.mylastIDGlobalPN++;
                    MainForm.myAddCountGlobalPN++;
                    RefreshList();
                    int myNewRowIndex = (currlst.Items.Count - 1);
                    if (!currlst.Enabled)
                    {
                        currlst.Enabled = true;
                        currlst.Focus();
                        currlst.SelectedIndex = myNewRowIndex;
                    }
                    MainForm.myGlobalPNISNewFlag = true;
                    blnAddNew = false;  //新增一条记录后将新增标志改为false;    140530_1                
                    result = true;
                }
                else
                {
                    MessageBox.Show("Error! \n " + myROWS.Length + " records existed; \n filterString-->ID=" + filterString + " and PID=" + PID + "");

                }
                
                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return result;
            }
        }

        private void btnPreviousPage_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PNInfoForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.blnAddNew) //140604_1 Add
            {
                blnAddNew = false;//141030_1
                return; //141030_1  不再删除未保存资料<万一字符串为已存在资料 可能误删>                
            }
        }

        private void cboMGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.currMGroupID = MainForm.getDTColumnInfo(MainForm.GlobalDS.Tables["GlobalManufactureCoefficientsGroup"], "ID", "ItemName='" + this.cboMGroup.Text + "'");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void cboTsensors_Leave(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt64(cboTsensors.Text) > 2 || Convert.ToInt64(cboTsensors.Text) < 0)
                {
                    MessageBox.Show("Invalid data:Must be a Number: 0-2!");
                    cboTsensors.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void cboVoltages_Leave(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt64(cboVoltages.Text) > 3 || Convert.ToInt64(cboVoltages.Text) < 0)
                {
                    MessageBox.Show("Invalid data:Must be a Number: 0-2!");
                    cboVoltages.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnInitInfo_Click(object sender, EventArgs e)
        {
            try
            {
                InitInfoForm myInitInfoForm = new InitInfoForm();

                myInitInfoForm.PNName = currlst.SelectedItem.ToString();
                myInitInfoForm.PID = MainForm.getDTColumnInfo(MainForm.GlobalDS.Tables["GlobalProductionName"], "ID", "PN='" + this.currlst.SelectedItem.ToString() + "'");
                myInitInfoForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void btnSpec_Click(object sender, EventArgs e)
        {
            try
            {
                PNSpecItemInfo mySpecItemInfo = new PNSpecItemInfo();

                mySpecItemInfo.currPNID = MainForm.getDTColumnInfo(MainForm.GlobalDS.Tables["GlobalProductionName"], "ID", "PN='" + this.currlst.SelectedItem.ToString() + "'");
                mySpecItemInfo.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}

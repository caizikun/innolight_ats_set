using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GlobalInfo
{
    public partial class PNInfoForm : Form
    {
        public PNInfoForm()
        {
            InitializeComponent();
        }

        public bool blnAddNew = false;
        public string PNameName = "";
        public long PID = -1;
        string currMGroupID = "";

        private void PNInfoForm_Load(object sender, EventArgs e)
        {
            RefreshList();
            ShowMyTip();
        }

        void ShowMyTip()
        {
            try
            {
                ToolTip mytip = new ToolTip();
                mytip.SetToolTip(currlst, "PN List");
                mytip.SetToolTip(this.txtAuxAttribles, "AuxAttribles");
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
                txtItemName.Clear();
                txtPN.Clear();
                cboTsensors.Items.Clear();
                cboTsensors.Text = "";
                cboVoltages.Items.Clear();
                cboVoltages.Text = "";
                txtAuxAttribles.Clear();
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
                }
                else
                {
                    this.currlst.Enabled = true;
                    this.txtPN.Enabled = false;
                    this.btnOK.Enabled = true;
                }
                this.txtPN.Text = PNameName;

                string sqlCondition = "PID=" + PID;

                DataRow[] mrDRs =MainForm.GlobalDS.Tables["GlobalProductionName"].Select(sqlCondition);
                for (int i = 0; i < mrDRs.Length; i++)
                {
                    this.currlst.Items.Add(mrDRs[i]["PN"].ToString());
                }
                                

                int MGroupIDCount = MainForm.GlobalDS.Tables["GlobalManufactureCoefficientsGroup"].Rows.Count;
                this.cboMGroup.Items.Add("");
                for (int i = 0; i < MGroupIDCount; i++)
                {   //140530_4
                    if (MainForm.GlobalDS.Tables["GlobalManufactureCoefficientsGroup"].Rows[i].RowState != DataRowState.Deleted)
                    {
                        this.cboMGroup.Items.Add(MainForm.GlobalDS.Tables["GlobalManufactureCoefficientsGroup"].Rows[i]["ItemName"].ToString());
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
                if (MainForm.checkItemLength("AuxAttribles", this.txtAuxAttribles.Text, 255)
                       || MainForm.checkItemLength("PN", this.txtPN.Text, 35)
                    || MainForm.checkItemLength("ItemName", this.txtItemName.Text, 35))               
                {
                    return;
                }

                if (
                    this.txtPN.Text.ToString().Trim().Length == 0 ||
                    this.txtItemName.Text.ToString().Trim().Length == 0 ||
                    this.cboVoltages.Text.ToString().Trim().Length == 0 ||
                    this.cboTsensors.Text.ToString().Trim().Length == 0 ||
                    this.cboMGroup.ToString().Trim().Length == 0 ||
                    this.cboChannels.Text.ToString().Trim().Length == 0 
                ) //140630_1
                {
                    MessageBox.Show("Data is incomplete,Pls confirm again?");
                    return;
                }

                if (blnAddNew)
                {
                    if (MainForm.currPrmtrCountExisted(MainForm.GlobalDS.Tables["GlobalProductionName"], "PID=" + PID + " and PN ='" + this.txtPN.Text.ToString() + "'") > 0)
                    {   //140704_1
                        MessageBox.Show("The new data of PN has existed! <Violate unique rule>");
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
                        myInitInfoForm.PNName = currlst.SelectedItem.ToString();
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
                DataRow[] myROWS = mydt.Select("PN='" + filterString + "' and PID=" +PID);
                if (myROWS.Length == 1)
                {                    
                    this.txtPN.Text=myROWS[0]["PN"].ToString();
                    this.txtItemName.Text=myROWS[0]["ItemName"].ToString();
                    this.cboChannels.Text=myROWS[0]["Channels"].ToString();
                    this.txtAuxAttribles.Text = myROWS[0]["AuxAttribles"].ToString();

                    string currMGroupName = MainForm.getDTColumnInfo(MainForm.GlobalDS.Tables["GlobalManufactureCoefficientsGroup"], "ItemName", "ID=" + myROWS[0]["MCoefsID"].ToString());
                    this.cboMGroup.Text = currMGroupName;

                    this.cboVoltages.Text = myROWS[0]["Voltages"].ToString(); //140703_2
                    this.cboTsensors.Text = myROWS[0]["Tsensors"].ToString(); //140703_2
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
                string filterString = this.txtPN.Text.ToString();
                DataRow[] myROWS = mydt.Select("PN='" + filterString + "' AND PID="+PID);
                if (myROWS.Length == 1)
                {
                    if (this.blnAddNew)
                    {
                        MessageBox.Show("Add new data error! \n " + myROWS.Length + " records existed; \n filterString-->PN='" + filterString + "' and PID=" + PID + "");
                        return result;
                    }
                    myROWS[0].BeginEdit();
                    myROWS[0]["PID"] = this.PID;
                    myROWS[0]["PN"] = this.txtPN.Text.ToString();
                    myROWS[0]["ItemName"] = this.txtItemName.Text.ToString();
                    myROWS[0]["AuxAttribles"] = this.txtAuxAttribles.Text.ToString();
                    myROWS[0]["Channels"] = this.cboChannels.Text.ToString();

                    myROWS[0]["Voltages"]=this.cboVoltages.Text.ToString(); //140703_2
                    myROWS[0]["Tsensors"]=this.cboTsensors.Text .ToString(); //140703_2

                    currMGroupID = MainForm.getDTColumnInfo(MainForm.GlobalDS.Tables["GlobalManufactureCoefficientsGroup"], "ID", "ItemName='" + this.cboMGroup.Text +"'");
                    myROWS[0]["MCoefsID"] = currMGroupID;
                    myROWS[0].EndEdit();
                    result = true;
                }
                else if (this.blnAddNew && myROWS.Length == 0)
                {
                    DataRow myNewRow = mydt.NewRow();
                    myNewRow.BeginEdit();
                    myNewRow["ID"] = MainForm.mylastIDGlobalPN + 1;
                    myNewRow["PID"] = this.PID;
                    myNewRow["PN"] = this.txtPN.Text.ToString();
                    myNewRow["ItemName"] = this.txtItemName.Text.ToString();
                    myNewRow["AuxAttribles"] = this.txtAuxAttribles.Text.ToString();
                    myNewRow["Channels"] = this.cboChannels.Text.ToString();
                    myNewRow["Voltages"] = this.cboVoltages.Text.ToString(); //140703_2
                    myNewRow["Tsensors"] = this.cboTsensors.Text.ToString(); //140703_2
                    currMGroupID = MainForm.getDTColumnInfo(MainForm.GlobalDS.Tables["GlobalManufactureCoefficientsGroup"], "ID", "ItemName='" + this.cboMGroup.Text +"'");
                    myNewRow["MCoefsID"] = currMGroupID;
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

                    blnAddNew = false;  //新增一条记录后将新增标志改为false;    140530_1                
                    result = true;
                }
                else
                {
                    MessageBox.Show("Error! \n " + myROWS.Length + " records existed; \n filterString-->PN='" + filterString + "' and PID=" + PID + "");

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
                    MessageBox.Show("Invalid data:Must be a Number: 0-3!");
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
    }
}

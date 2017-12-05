using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;

namespace TestPlan
{
    public partial class TestPlanForm : Form
    {
        public TestPlanForm()
        {
            InitializeComponent();
        }
        public bool blnAddNew = false;
        public string  PName = "";
        public long PID = -1;
        string myTestPlanID="";


        private void TestPlanForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.blnAddNew) //140604_1 Add
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
                    int myExistCount = PNInfo.currPrmtrCountExisted(PNInfo.TopoToatlDS.Tables["TopoTestPlan"], queryCMD);
                    if (myResult == DialogResult.OK)
                    {
                        if (myExistCount > 0)
                        {
                            PNInfo.DeleteItemForDT(PNInfo.TopoToatlDS.Tables["TopoTestPlan"], queryCMD);
                            PNInfo.mylastIDTestPlan = PNInfo.mylastIDTestPlan - myExistCount;
                        }
                        blnAddNew = false;
                        
                        PNInfo.myTestPlanAddOKFlag = true;
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
        
        private void TestPlan_Load(object sender, EventArgs e)
        {
            try
            {
                // Login.AccessFilePath
                RefreshList();
                ShowMyTip();
                btnOK.Visible = (PNInfo.blnWritable ? true : false);
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
                mytip.SetToolTip(currlst, "当前存在的TestPlan列表信息");
                mytip.SetToolTip(cboItemName, "当前存在的TestPlan的名称[唯一]");
                mytip.SetToolTip(cboSWVersion, "当前的TestPlan的软件版本");
                mytip.SetToolTip(cboHWVersion, "当前的TestPlan的硬件版本");
                mytip.SetToolTip(cboUSBPort, "设置的USB端口号,一般为 0 ");
                mytip.SetToolTip(cboAuxAttribles, "其他属性设置...");
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
                txtTestPlanName.Text = this.PName;
                cboItemName.Enabled = false;

                if (blnAddNew)
                {
                    myTestPlanID = (PNInfo.mylastIDTestPlan + 1).ToString();
                    PNInfo.myTestPlanISNewFlag = true;
                    this.cboAuxAttribles.Items.Add("SN_CHECK = 0;"); //140606 Add
                    this.cboUSBPort.Items.Add("0"); //140606 Add
                    cboItemName.Enabled = true;
                    cboItemName.BackColor = Color.Yellow;   //140709_2
                    this.currlst.Enabled = false;
                    btnNextPage.Enabled = false;
                }
                else
                {                    
                    this.currlst.Enabled = true;
                    btnNextPage.Enabled = false; //140604
                    cboItemName.BackColor = Color.White;    //140709_2
                    this.cboAuxAttribles.Items.Add("SN_CHECK = 0;"); //140606 Add
                    this.cboUSBPort.Items.Add("0"); //140606 Add
                }
                //140703_1>>>
                DataRow[] drs = PNInfo.TopoToatlDS.Tables["TopoTestPlan"].Select("PID=" + PID);

                for (int i = 0; i < drs.Length; i++)
                    if (drs[i].RowState != DataRowState.Deleted)
                    {
                        currlst.Items.Add(drs[i]["ItemName"].ToString());
                    }
                //140703_1<<<
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
                if( this.PID.ToString().Trim().Length ==0 || 
                this.cboItemName.Text.ToString().Trim().Length ==0 || 
                this.cboSWVersion.Text.ToString().Trim().Length ==0 || 
                this.cboHWVersion.Text.ToString().Trim().Length ==0 || 
                this.cboUSBPort.Text.ToString().Trim().Length ==0 || 
                this.cboAuxAttribles.Text.ToString().Trim().Length ==0 
                ) //140606 Add
                {
                    MessageBox.Show("当前部分项目资料为空,请确认后再保存资料!!!");
                    return;
                }

                bool result = EditInfoForDT(PNInfo.TopoToatlDS.Tables[PNInfo.ConstTestPlanTables[1]]);

                if (result)
                {
                    PNInfo.ISNeedUpdateflag = true; //140603_2
                    btnNextPage.Enabled = true;
                    cboItemName.Enabled = false;
                    cboItemName.BackColor = Color.White;    //140709_2

                    if (PNInfo.myTestPlanISNewFlag)
                    {
                        //PNInfo.myTestPlanAddOKFlag = true;
                        EquipmentForm myEquip = new EquipmentForm();
                        myEquip.blnAddNew = true;
                        PNInfo.myTestEquipAddOKFlag = false;        //140529_1
                        PNInfo.myTestEquipPrmtrAddOKFlag = false;   //140529_1
                        myEquip.TestPlanName = currlst.SelectedItem.ToString();
                        //string filterstring = "Select ID from " + PNInfo.ConstTestPlanTables[1] + " where ItemName='" + myEquip.TestPlanName.Trim() + "' and PID='" + PID + "'";
                        string filterstring = "ItemName='" + myEquip.TestPlanName.Trim() + "' and PID=" + PID + "";
                        myEquip.PID = PNInfo.getNextTablePIDFromDT(PNInfo.TopoToatlDS.Tables[PNInfo.ConstTestPlanTables[1]], filterstring);

                        myEquip.ShowDialog();   //show NextForm...
                        blnAddNew = false;
                        PNInfo.myTestPlanAddOKFlag = true; //140530_0
                        this.Close(); //140530_4
                    }
                    else
                    {
                        PNInfo.myTestPlanAddOKFlag = true; //140530_0
                    }
                }
                else //140604_1
                {
                    btnPreviousPage.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnPreviousPage_Click(object sender, EventArgs e)
        {            
            exitForm(); //尚未保存怎么提示? //140603            
        }

        private void btnNextPage_Click(object sender, EventArgs e)
        {
            try
            {
                if (currlst.SelectedIndex != -1)
                {                    
                    PNInfo.myTestPlanAddOKFlag = true; //140529_1
                    EquipmentForm myEquip = new EquipmentForm();
                    myEquip.blnAddNew = blnAddNew;  //140706_1 
                    myEquip.TestPlanName = currlst.SelectedItem.ToString();
                    //string filterstring = "Select ID from " + PNInfo.ConstTestPlanTables[1] + " where ItemName='" + myEquip.TestPlanName.Trim() + "' and PID='" + PID + "'";
                    string filterstring = "ItemName='" + myEquip.TestPlanName.Trim() + "' and PID=" + PID + "";
                    myEquip.PID = PNInfo.getNextTablePIDFromDT(PNInfo.TopoToatlDS.Tables[PNInfo.ConstTestPlanTables[1]], filterstring);

                    myEquip.ShowDialog();   //show NextForm...
                }
                else
                {
                    MessageBox.Show("请选择对应项目后再点击按钮!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void exitForm()
        {
            try
            {
                if (this.blnAddNew)
                {
                    DialogResult myResult = MessageBox.Show("尚未完成 TestPlanName= " + this.cboItemName.Text
                        + "的资料维护!提前退出将可能无法保证资料完整,系统将自动删除当前维护项目资料!",
                        "注意:",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning,
                        MessageBoxDefaultButton.Button1);

                    if (myResult == DialogResult.OK)
                    {

                        string queryCMD = "ItemName= '" + this.cboItemName.Text.ToString() + "' And PID=" + this.PID;
                        int myExistCount = PNInfo.currPrmtrCountExisted(PNInfo.TopoToatlDS.Tables["TopoTestPlan"], queryCMD);
                        if (myExistCount > 0)
                        {
                            PNInfo.DeleteItemForDT(PNInfo.TopoToatlDS.Tables["TopoTestPlan"], queryCMD);
                            PNInfo.mylastIDTestPlan = PNInfo.mylastIDTestPlan - myExistCount;
                        }
                        blnAddNew = false;
                        PNInfo.myTestPlanAddOKFlag = true; //140529_1
                        this.Close();
                        Application.OpenForms["PNInfo"].Show();
                    }
                }
                else
                {
                    this.Close();
                    Application.OpenForms["PNInfo"].Show();
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
                if (currlst.SelectedIndex != -1)    //140714_0
                {
                    getInfoFromDT(PNInfo.TopoToatlDS.Tables[PNInfo.ConstTestPlanTables[1]], currlst.SelectedIndex);
                    btnNextPage.Enabled = true; //140706_1 //可以直接选择进入设备和流程
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        void getInfoFromDT(DataTable mydt,int currIndex)
        {
            try
            {
                string filterString = currlst.SelectedItem.ToString();
                DataRow[] myROWS = mydt.Select("ItemName='" + filterString + "' and PID=" + PID + "");
                if (myROWS.Length == 1)
                {
                    myTestPlanID = myROWS[0]["ID"].ToString();
                    this.cboItemName.Text = myROWS[0]["ItemName"].ToString();
                    this.cboSWVersion.Text = myROWS[0]["SWVersion"].ToString();
                    this.cboHWVersion.Text = myROWS[0]["HWVersion"].ToString();
                    this.cboUSBPort.Text = myROWS[0]["USBPort"].ToString();
                    this.cboAuxAttribles.Text = myROWS[0]["AuxAttribles"].ToString();
                }
                else
                {
                    MessageBox.Show("资料有误!请确认!!! 共有" + myROWS.Length + ("条记录; \n 条件--> ItemName='" + filterString + "' and PID=" + PID + ""));
                    return;
                }
            }
            catch (Exception ex) 
            { 
                MessageBox.Show(ex.ToString ());
                return;
            }
        }

        bool  EditInfoForDT(DataTable mydt)
        {
            bool result = false;
            try
            {   
                
                string filterString = this.cboItemName.Text.ToString();

                //DataRow[] myROWS = mydt.Select("ItemName='" + filterString + "' and PID=" + PID + ""); //140709_1
                DataRow[] myROWS = mydt.Select("ID=" + myTestPlanID);
                
                if (myROWS.Length == 1)
                {
                    if (this.blnAddNew)
                    {
                        MessageBox.Show("新增资料有误!请确认!!! 已有" + myROWS.Length + ("条记录; \n 条件--> ItemName='" + filterString + "' and PID=" + PID + ""));
                        return result;
                    }                  
                    myROWS[0].BeginEdit();
                    myROWS[0]["PID"] = this.PID.ToString();
                    myROWS[0]["ItemName"] = this.cboItemName.Text.ToString();
                    myROWS[0]["SWVersion"] = this.cboSWVersion.Text.ToString();
                    myROWS[0]["HWVersion"] = this.cboHWVersion.Text.ToString();
                    myROWS[0]["USBPort"] = this.cboUSBPort.Text.ToString();
                    myROWS[0]["AuxAttribles"] = this.cboAuxAttribles.Text.ToString();
                    myROWS[0].EndEdit();
                    RefreshList();
                    result=true;
                    
                }
                else if (this.blnAddNew)
                {
                    DataRow myNewRow = mydt.NewRow();
                    myNewRow.BeginEdit();
                    myNewRow["ID"] = PNInfo.mylastIDTestPlan+1;
                    myNewRow["PID"] = this.PID.ToString();
                    myNewRow["ItemName"] = this.cboItemName.Text.ToString();
                    myNewRow["SWVersion"] = this.cboSWVersion.Text.ToString();
                    myNewRow["HWVersion"] = this.cboHWVersion.Text.ToString();
                    myNewRow["USBPort"] = this.cboUSBPort.Text.ToString();
                    myNewRow["AuxAttribles"] = this.cboAuxAttribles.Text.ToString();
                    mydt.Rows.Add(myNewRow);

                    myNewRow.EndEdit();
                    PNInfo.mylastIDTestPlan++;
                    PNInfo.myAddCountTestPlan ++;
                    RefreshList();
                    int myNewRowIndex= (currlst.Items.Count - 1);
                    
                    currlst.Enabled = true;
                    currlst.Focus();
                    currlst.SelectedIndex = myNewRowIndex;
                    
                   
                    blnAddNew = false;  //新增一条记录后将新增标志改为false;    140530_1                
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

        private void cboUSBPort_Leave(object sender, EventArgs e)
        {
            try
            {
                if (PNInfo.ISNotInSpec("USB端口号", cboUSBPort.Text.ToString(), 0, 10))
                {
                    cboUSBPort.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void lblItemName_DoubleClick(object sender, EventArgs e)
        {
            cboItemName.Enabled = true;
        }

        private void cboItemName_Leave(object sender, EventArgs e)
        {
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
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
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
                        MessageBox.Show("已存在TestPlan项目:-> " + NewName);

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
        
    }
}

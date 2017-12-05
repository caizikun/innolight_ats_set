using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;

namespace Maintain
{
    public partial class SpecInfo : Form
    {
        public SpecInfo()
        {
            InitializeComponent();
        }

        public bool blnAddNew = false;
        public string GlobalSpecName = "";
        string tempSpecName = "";  //150310_0
        string currAppID = "";      //150310_0

        private void CtrlInfo_Load(object sender, EventArgs e)
        {
            try
            {
                RefreshList();
                ShowMyTip();

                if (blnAddNew)
                {
                    currAppID = (MainForm.mylastIDGlobalSpecs + 1).ToString();
                    txtName.Enabled = true;
                    MainForm.myGlobalSpecsAddOKFlag = false;
                    txtName.BackColor = Color.Yellow;
                    currlst.Enabled = false;
                    btnOK.Enabled = true;
                    btnFinish.Enabled = false;
                    MainForm.myGlobalSpecsISNewFlag = true;        //140530_2
                }
                else
                {
                    txtName.Enabled = false;
                    btnOK.Enabled = false;
                    btnFinish.Enabled = true;
                    MainForm.myGlobalSpecsISNewFlag = false;        //140530_2

                    this.Show();
                    if (currlst.Items.Contains(GlobalSpecName))
                    {
                        currlst.SelectedItem = GlobalSpecName;
                    }

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
                mytip.SetToolTip(currlst, "All Specs Items List");
                mytip.SetToolTip(txtDescription, "Description of current App Item...");
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
                }
                else
                {
                    this.currlst.Enabled = true;
                }

                DataRow[] CurrEquipLst = MainForm.GlobalDS.Tables["GlobalSpecs"].Select("");
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
        {
            try
            {
                string filterString = currlst.SelectedItem.ToString();
                DataRow[] myROWS = mydt.Select("ItemName='" + filterString + "'");
                if (myROWS.Length == 1)
                {
                    txtName.Enabled = true;
                    currAppID = myROWS[0]["ID"].ToString();
                    tempSpecName = myROWS[0]["ItemName"].ToString();
                    this.txtName.Text = myROWS[0]["ItemName"].ToString();
                    this.txtUnit.Text = myROWS[0]["Unit"].ToString();
                    this.txtDescription.Text = myROWS[0]["ItemDescription"].ToString();
                }
                else
                {
                    MessageBox.Show("Error! " + myROWS.Length + " records existed; \n filterString--> ItemName='" + filterString + "'");
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
            bool Result = false;
            try
            {
                //先判定当前资料是否有空资料!                
                if (this.txtName.Text.ToString().Trim().Length == 0 ) //this.txtUnit.Text.ToString().Trim().Length == 0 ||this.txtDescription.Text.ToString().Trim().Length == 0
                {
                    MessageBox.Show("Error!Null data existed!");
                    return Result;
                }
                else
                {
                    string filterString = "ID=" + currAppID;
                    DataRow[] myROWS = mydt.Select(filterString);
                    if (myROWS.Length == 1)
                    {
                        if (this.blnAddNew)
                        {
                            MessageBox.Show("Add new data Error! " + myROWS.Length + (" records existed; \n filterString--> " + filterString));
                            return Result;
                        }
                        myROWS[0].BeginEdit();
                        myROWS[0]["ItemName"] = this.txtName.Text.ToString();
                        myROWS[0]["Unit"] = this.txtUnit.Text.ToString();
                        myROWS[0]["ItemDescription"] = this.txtDescription.Text.ToString();
                        myROWS[0].EndEdit();
                        RefreshList();
                        Result = true;
                    }
                    else if (this.blnAddNew && myROWS.Length == 0)
                    {
                        DataRow myNewRow = mydt.NewRow();
                        myNewRow.BeginEdit();
                        myNewRow["ID"] = MainForm.mylastIDGlobalSpecs + 1;
                        myNewRow["ItemName"] = this.txtName.Text.ToString();
                        myNewRow["Unit"] = this.txtUnit.Text.ToString();
                        myNewRow["ItemDescription"] = this.txtDescription.Text.ToString();
                        mydt.Rows.Add(myNewRow);

                        myNewRow.EndEdit();
                        MainForm.mylastIDGlobalSpecs++;
                        MainForm.myAddCountGlobalSpecs++;
                        RefreshList();  //此方法会执行刷新ListItem的动作~
                        int myNewRowIndex = (currlst.Items.Count - 1);
                        blnAddNew = true;
                        currlst.Enabled = true;
                        currlst.Focus();
                        currlst.SelectedIndex = myNewRowIndex;

                        Result = true;
                    }
                    else
                    {
                        MessageBox.Show("Error! " + myROWS.Length + " records existed; \n filterString--> " + filterString);
                    }
                }
                
                return Result;
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
                    getInfoFromDT(MainForm.GlobalDS.Tables["GlobalSpecs"], currlst.SelectedIndex);                    
                }
                else
                {
                    
                    btnOK.Enabled = false;
                    MessageBox.Show("Pls choose a item first!");
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
                if (MainForm.checkItemLength("ItemName", txtName.Text, 100)
                    || MainForm.checkItemLength("Unit", txtUnit.Text, 50)
                    || MainForm.checkItemLength("ItemDescription", this.txtDescription.Text, 4000))
                {
                    return;
                }
                if (this.blnAddNew || (tempSpecName.Length > 0 && tempSpecName.ToUpper() != this.txtName.Text.ToString().ToUpper().Trim()))
                {
                    if (MainForm.currPrmtrCountExisted(MainForm.GlobalDS.Tables["GlobalSpecs"], "ItemName ='" + this.txtName.Text.ToString() + "'") > 0)
                    {
                        MessageBox.Show("The data of ItemName has existed! <Violate unique rule>");
                        return;
                    }
                }
                bool Result = EditInfoForDT(MainForm.GlobalDS.Tables["GlobalSpecs"]);

                if (Result)
                {
                    MainForm.ISNeedUpdateflag = true; //140603_2
                    txtName.BackColor = Color.White;
                    txtName.Enabled = false;    //140530_4
                    btnFinish.Enabled = true;
                    MainForm.myGlobalSpecsAddOKFlag = true; //140529_1
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
                blnAddNew = false;
                return; //141030_1  不再删除未保存资料<万一字符串为已存在资料 可能误删>                
            }
        }
    }
}

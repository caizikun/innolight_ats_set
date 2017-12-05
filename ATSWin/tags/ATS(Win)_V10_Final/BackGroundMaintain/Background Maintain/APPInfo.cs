using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;

namespace Maintain
{
    public partial class APPInfo : Form
    {
        public APPInfo()
        {
            InitializeComponent();
        }

        public bool blnAddNew = false;
        public string GlobalAppName = "";
        long origmylastIDGlobalAPP;
        long origmynewIDGlobalAPP;
        long origmyDeletedCountGlobalAPP;
        string tempAPPName = "";  //150310_0
        string currAppID = "";      //150310_0

        private void CtrlInfo_Load(object sender, EventArgs e)
        {
            try
            {
                RefreshList();
                ShowMyTip();
                origmylastIDGlobalAPP = MainForm.mylastIDGlobalType;
                origmynewIDGlobalAPP = MainForm.mynewIDGlobalAPP;
                origmyDeletedCountGlobalAPP = MainForm.myDeletedCountGlobalAPP;

                if (blnAddNew)
                {
                    currAppID = (MainForm.mylastIDGlobalAPP + 1).ToString();
                    txtName.Enabled = true;
                    MainForm.myGlobalAPPAddOKFlag = false;
                    txtName.BackColor = Color.Yellow;
                    currlst.Enabled = false;
                    btnOK.Enabled = true;
                    btnFinish.Enabled = false;
                    MainForm.myGlobalAPPlISNewFlag = true;        //140530_2
                }
                else
                {
                    txtName.Enabled = false;
                    btnOK.Enabled = false;
                    btnFinish.Enabled = true;
                    MainForm.myGlobalAPPlISNewFlag = false;        //140530_2

                    this.Show();
                    if (currlst.Items.Contains(GlobalAppName))
                    {
                        currlst.SelectedItem = GlobalAppName;
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
                mytip.SetToolTip(currlst, "All App List");
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

                DataRow[] CurrEquipLst = MainForm.GlobalDS.Tables["GlobalAllAppModelList"].Select("");
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
        {
            try
            {
                string filterString = currlst.SelectedItem.ToString();
                DataRow[] myROWS = mydt.Select("ItemName='" + filterString + "'");
                if (myROWS.Length == 1)
                {
                    txtName.Enabled = true;
                    currAppID = myROWS[0]["ID"].ToString();
                    tempAPPName = myROWS[0]["ItemName"].ToString();
                    this.txtName.Text = myROWS[0]["ItemName"].ToString();
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
            string[] myColumnLst = new string[] { "ID", "ItemName", "ItemDescription" };

            try
            {
                //先判定当前资料是否有空资料!                
                if (this.txtName.Text.ToString().Trim().Length == 0 ||
                    this.txtDescription.Text.ToString().Trim().Length == 0)
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
                        myROWS[0]["ItemDescription"] = this.txtDescription.Text.ToString();
                        myROWS[0].EndEdit();
                        Result = true;
                    }
                    else if (this.blnAddNew && myROWS.Length == 0)
                    {
                        DataRow myNewRow = mydt.NewRow();
                        myNewRow.BeginEdit();
                        myNewRow["ID"] = MainForm.mylastIDGlobalAPP + 1;
                        myNewRow["ItemName"] = this.txtName.Text.ToString();
                        myNewRow["ItemDescription"] = this.txtDescription.Text.ToString();
                        mydt.Rows.Add(myNewRow);

                        myNewRow.EndEdit();
                        MainForm.mylastIDGlobalAPP++;
                        MainForm.myAddCountGlobalAPP++;
                        RefreshList();  //此方法会执行刷新ListItem的动作~
                        int myNewRowIndex = (currlst.Items.Count - 1);

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
                RefreshList(); //140530_1
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
                    getInfoFromDT(MainForm.GlobalDS.Tables["GlobalAllAppModelList"], currlst.SelectedIndex);
                    
                    if (blnAddNew)
                    {
                        btnModelInfo.Enabled = false;
                    }
                    else
                    {
                        btnModelInfo.Enabled = true;
                    }

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
                if (MainForm.checkItemLength("ItemName", txtName.Text, 30)
                    || MainForm.checkItemLength("ItemDescription", this.txtDescription.Text, 50))
                {
                    return;
                }
                if (this.blnAddNew || (tempAPPName.Length > 0 && tempAPPName.ToUpper() != this.txtName.Text.ToString().ToUpper().Trim()))
                {
                    if (MainForm.currPrmtrCountExisted(MainForm.GlobalDS.Tables["GlobalAllAppModelList"], "ItemName ='" + this.txtName.Text.ToString() + "'") > 0)
                    {
                        MessageBox.Show("The data of ItemName has existed! <Violate unique rule>");
                        return;
                    }
                }
                bool Result = EditInfoForDT(MainForm.GlobalDS.Tables["GlobalAllAppModelList"]);

                if (Result)
                {
                    MainForm.ISNeedUpdateflag = true; //140603_2
                    txtName.BackColor = Color.White;
                    txtName.Enabled = false;    //140530_4
                    btnFinish.Enabled = true;
                    btnModelInfo.Enabled = true;
                    MainForm.myGlobalAPPAddOKFlag = true; //140529_1

                    if (blnAddNew)
                    {
                        if (this.txtName.Text.Length > 0)
                        {
                            ModelInfo myModelInfo = new ModelInfo();
                            myModelInfo.blnAddNewModel = blnAddNew; //140430_1 TBD
                            MainForm.myGlobalModelAddOKFlag = false;
                            myModelInfo.PID = Convert.ToInt64(MainForm.getDTColumnInfo(MainForm.GlobalDS.Tables["GlobalAllAppModelList"], "ID", "ItemName='" + tempAPPName + "'"));
                            myModelInfo.GlobalAPPName = tempAPPName;
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
                blnAddNew = false;
                return; //141030_1  不再删除未保存资料<万一字符串为已存在资料 可能误删>                
            }
        }

        private void btnModelInfo_Click(object sender, EventArgs e)
        {
            try
            {
                ModelInfo myModelInfo = new ModelInfo();
                myModelInfo.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
    }
}

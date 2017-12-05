using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Maintain
{
    public partial class TypeForm : Form
    {
        public TypeForm()
        {
            InitializeComponent();
        }

        public bool blnAddNew = false;
        public string PTypeName = "";
        public long PID = -1;
        string currMSAID = "";
        string currTypeID = "";
        string tempTypeName = "";

        private void TypePNForm_Load(object sender, EventArgs e)
        {
            try
            {
                initType(); //150413_1
                RefreshList();
                ShowMyTip();

                this.Show();
                if (!blnAddNew && currlst.Items.Contains(PTypeName))
                {
                    currlst.SelectedItem = PTypeName;
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
                mytip.SetToolTip(currlst, "Type List");
                mytip.SetToolTip(cboMSAName, "MSA Name");
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
                cboMSAName.Items.Clear();
                if (blnAddNew)
                {
                    this.currlst.Enabled = false;
                    this.txtName.Enabled = true;
                    this.btnOK.Enabled = true;
                    txtName.BackColor = Color.Yellow;
                    currTypeID = (MainForm.mylastIDGlobalType + 1).ToString();  //150310_0                    
                }
                else
                {
                    this.currlst.Enabled = true;
                    this.txtName.Enabled = false;
                    this.btnOK.Enabled = true;
                    txtName.BackColor = Color.White;
                }

                int typeCount = MainForm.GlobalDS.Tables["GlobalProductionType"].Rows.Count;
                for (int i = 0; i < typeCount; i++)
                {   //140530_4
                    if (MainForm.GlobalDS.Tables["GlobalProductionType"].Rows[i].RowState != DataRowState.Deleted)
                    {
                        currlst.Items.Add(MainForm.GlobalDS.Tables["GlobalProductionType"].Rows[i]["ItemName"].ToString());
                    }
                }
                int MSAIDCount = MainForm.GlobalDS.Tables["GlobalMSA"].Rows.Count;
                this.cboMSAName.Items.Add("");
                for (int i = 0; i < MSAIDCount; i++)
                {   //140530_4
                    if (MainForm.GlobalDS.Tables["GlobalMSA"].Rows[i].RowState != DataRowState.Deleted)
                    {
                        this.cboMSAName.Items.Add(MainForm.GlobalDS.Tables["GlobalMSA"].Rows[i]["ItemName"].ToString());
                    }
                }
                if (!blnAddNew && currlst.Items.Contains(txtName.Text))
                {
                    currlst.SelectedItem = txtName.Text;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void TypePNForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.blnAddNew) //140604_1 Add
            {
                blnAddNew = false;//141030_1
                return; //141030_1  不再删除未保存资料<万一字符串为已存在资料 可能误删>               
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (MainForm.checkItemLength("ItemName", this.txtName.Text, 25))
                {
                    return;
                }
                if (this.cboMSAName.Text.ToString().Trim().Length == 0 ||
                this.txtName.Text.ToString().Trim().Length == 0
                ) //140606 Add
                {
                    MessageBox.Show("Data is incomplete,Pls confirm again?");
                    return;
                }

                if (blnAddNew || (tempTypeName.Length > 0 && tempTypeName.ToUpper() != txtName.Text.ToString().ToUpper().Trim()))  //140922_0
                {
                    if (MainForm.currPrmtrCountExisted(MainForm.GlobalDS.Tables["GlobalProductionType"], "ItemName ='" + this.txtName.Text.ToString() + "'") > 0)
                    {   //140704_1
                        MessageBox.Show("The data of 'ProductionType' has existed! <Violate unique rule>");
                        return;
                    }
                }

                bool result = EditInfoForDT(MainForm.GlobalDS.Tables["GlobalProductionType"]);

                if (result)
                {
                    MainForm.ISNeedUpdateflag = true;
                    cboMSAName.Enabled = true;
                    txtName.Enabled = false;
                    if (MainForm.myGlobalTypeISNewFlag)
                    {
                        PNInfoForm myPNInfoForm = new PNInfoForm();
                        myPNInfoForm.blnAddNew = true;
                        myPNInfoForm.PID = Convert.ToInt64(MainForm.getDTColumnInfo(MainForm.GlobalDS.Tables["GlobalProductionType"], "ID", "ItemName='" + txtName.Text.ToString() + "'"));

                        myPNInfoForm.ShowDialog();   //show NextForm...
                        blnAddNew = false;
                        MainForm.myGlobalTypeAddOKFlag = true;
                        this.Close();
                    }
                    else
                    {
                        MainForm.myGlobalTypeAddOKFlag = true;
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
                    getInfoFromDT(MainForm.GlobalDS.Tables["GlobalProductionType"], currlst.SelectedIndex);
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
                //150413_2----------------------------------
                this.cboIgnore.Visible = false;
                this.lblIgnore.Visible = false;
                this.cboIgnore.Text = false.ToString(); 
                //------------------------------------------

                string filterString = currlst.SelectedItem.ToString();
                DataRow[] myROWS = mydt.Select("ItemName='" + filterString + "'");
                if (myROWS.Length == 1)
                {
                    currTypeID = myROWS[0]["ID"].ToString();
                    tempTypeName = myROWS[0]["ItemName"].ToString();
                    this.txtName.Text = myROWS[0]["ItemName"].ToString();
                    string MSAID = myROWS[0]["MSAID"].ToString();
                    string MSAIDValue = MainForm.getDTColumnInfo(MainForm.GlobalDS.Tables["GlobalMSA"], "ItemName", "id=" + MSAID);
                    this.cboMSAName.Text = MSAIDValue;
                    this.txtName.Enabled = true;    //150310_1
                    if (mydt.Columns.Contains("IgnoreFlag"))
                    {
                        if (Convert.ToBoolean(myROWS[0]["IgnoreFlag"]))
                        {
                            this.cboIgnore.Visible = true;
                            this.lblIgnore.Visible = true;
                            this.cboIgnore.Text = myROWS[0]["IgnoreFlag"].ToString();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Error! \n " + myROWS.Length + " records existed; \n filterString--> ItemName='" + filterString);
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
                string filterString = "ID=" + currTypeID;   //150310_1
                DataRow[] myROWS = mydt.Select(filterString);
                if (myROWS.Length == 1)
                {
                    if (this.blnAddNew)
                    {
                        MessageBox.Show("Add new data error! \n " + myROWS.Length + " records existed; \n filterString--> " + filterString);
                        return result;
                    }
                    myROWS[0].BeginEdit();
                    myROWS[0]["ItemName"] = this.txtName.Text.ToString();
                    currMSAID = MainForm.getDTColumnInfo(MainForm.GlobalDS.Tables["GlobalMSA"], "ID", "ItemName='" + cboMSAName.Text + "'");   //140923_0
                    myROWS[0]["MSAID"] = currMSAID;
                    if (mydt.Columns.Contains("IgnoreFlag"))
                    {
                        myROWS[0]["IgnoreFlag"] = this.cboIgnore.Text;
                    }
                    myROWS[0].EndEdit();
                    result = true;
                }
                else if (this.blnAddNew)
                {
                    DataRow myNewRow = mydt.NewRow();
                    myNewRow.BeginEdit();
                    myNewRow["ID"] = MainForm.mylastIDGlobalType + 1;
                    myNewRow["ItemName"] = this.txtName.Text.ToString();
                    currMSAID = MainForm.getDTColumnInfo(MainForm.GlobalDS.Tables["GlobalMSA"], "ID", "ItemName='" + cboMSAName.Text + "'");
                    myNewRow["MSAID"] = currMSAID;
                    if (mydt.Columns.Contains("IgnoreFlag"))
                    {
                        myNewRow["IgnoreFlag"] = false.ToString();
                    }
                    mydt.Rows.Add(myNewRow);

                    myNewRow.EndEdit();
                    MainForm.mylastIDGlobalType++;
                    MainForm.myAddCountGlobalType++;
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
                    this.txtName.Enabled = false;    //150310_1
                    
                }
                else
                {
                    MessageBox.Show("Error! \n " + myROWS.Length + " records existed; \n filterString--> " + filterString);

                }
                RefreshList();
                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return result;
            }
        }

        private void cboMSAName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                currMSAID = MainForm.getDTColumnInfo(MainForm.GlobalDS.Tables["GlobalMSA"], "ID", "ItemName='" + cboMSAName.Text + "'");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnPreviousPage_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        void initType()
        {
            this.cboIgnore.Visible = false;
            this.lblIgnore.Visible = false;

            this.cboIgnore.Items.Clear();
            this.cboIgnore.Items.Add("False");
            this.cboIgnore.Items.Add("True");
            this.cboIgnore.Text = "False";
        }
    }
}

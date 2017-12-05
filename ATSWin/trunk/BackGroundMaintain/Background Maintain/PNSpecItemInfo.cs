﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;

namespace Maintain
{
    public partial class PNSpecItemInfo : Form
    {
        public bool blnAddNew = false;
        public string currPNID = "-1";

        string SID = "-1";
        bool blnAddNewPrmtr = false;

        long currPrmtrID = -1;

        string tempParamName = "";

        public PNSpecItemInfo()
        {
            InitializeComponent();
        }

        void resizeDGV(DataGridView dgv)
        {
            int mySize = 0;
            int j = 0;
            for (int i = 0; i < dgv.Columns.Count; i++)
            {
                if (dgv.Columns[i].Visible)
                {
                    j++;
                    mySize += dgv.Columns[i].Width;
                }
            }
            if (dgv.RowHeadersVisible)
            {
                mySize += dgv.RowHeadersWidth;
            }
            for (int i = 0; i < dgv.Columns.Count; i++)
            {
                if (mySize < dgv.Width)
                {
                    if (dgv.Columns[i].Visible)
                    {
                        dgv.Columns[i].Width += (dgv.Width - mySize) / j;
                    }
                }
            }
        }

        void RefreshDgvInfo()
        {
            try
            {
                DataTable newDt = MainForm.GlobalDS.Tables["TopoPNSpecsParams"].Copy();
                newDt.Columns.Add("ItemName");
                for (int i = 0; i < MainForm.GlobalDS.Tables["TopoPNSpecsParams"].Rows.Count; i++)
                {
                    if (newDt.Rows[i].RowState != DataRowState.Deleted)
                    {
                        newDt.Rows[i]["ItemName"] = MainForm.getDTColumnInfo(MainForm.GlobalDS.Tables["GlobalSpecs"], "ItemName", "ID=" + newDt.Rows[i]["SID"]);
                    }
                }
                MainForm.showTablefilterStrInfo(newDt, dgvPrmtr, "PID=" + currPNID);
                MainForm.hideMyColumn(dgvPrmtr, "SID");
                dgvPrmtr.Columns["ItemName"].DisplayIndex = 0;
                resizeDGV(dgvPrmtr);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void SpecItemInfo_Load(object sender, EventArgs e)
        {
            try
            {
                RefreshDgvInfo();
                refreshSpeclst();
                ShowMyTip();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void SpecItemInfo_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        void ShowMyTip()
        {
            try
            {
                ToolTip mytip = new ToolTip();

                mytip.SetToolTip(btnPrmtrAdd, "Add a new SpecItem");
                mytip.SetToolTip(btnPrmtrDelete, "Delete a SpecItem");
                mytip.SetToolTip(btnFinish, "Return");

                mytip.ShowAlways = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void refreshSpeclst()
        {
            try
            {
                cboItemName.Items.Clear();
                DataRow[] GlobalSpecItemsLst = MainForm.GlobalDS.Tables["GlobalSpecs"].Select();
                foreach (DataRow dr in GlobalSpecItemsLst)
                {
                    cboItemName.Items.Add(dr["ItemName"]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        //******************************************************               

        void clearCboItems()
        {
            try
            {
                this.cboTypical.Items.Clear();
                this.cboSpecMin.Items.Clear();
                this.cboSpecMax.Items.Clear();
                this.cboItemName.SelectedIndex = -1;
                this.cboTypical.Text = "";
                this.cboSpecMin.Text = "";
                this.cboSpecMax.Text = "";
                this.txtDescription.Text = "";
                this.cboSpecMin.Items.Add("-32768");
                this.cboSpecMax.Items.Add("32767");
                this.cboTypical.Items.Add("32767");
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
                    SID = this.dgvPrmtr.CurrentRow.Cells["SID"].Value.ToString();
                    DataRow[] myROWS = mydt.Select("ID=" + myID + " and PID=" + currPNID);
                    if (myROWS.Length == 1)
                    {
                        this.cboItemName.Text = MainForm.getDTColumnInfo(MainForm.GlobalDS.Tables["GlobalSpecs"], "ItemName", "ID=" + myROWS[0]["SID"].ToString());
                        this.cboTypical.Text = myROWS[0]["Typical"].ToString();
                        this.cboSpecMin.Text = myROWS[0]["SpecMin"].ToString();
                        this.cboSpecMax.Text = myROWS[0]["SpecMax"].ToString();
                        this.txtDescription.Text = MainForm.getDTColumnInfo(MainForm.GlobalDS.Tables["GlobalSpecs"], "ItemDescription", "ID=" + myROWS[0]["SID"].ToString());
                        tempParamName = this.cboItemName.Text;
                    }
                    else
                    {
                        if (blnAddNew)
                        {

                        }
                        else
                        {
                            MessageBox.Show("Parameters was not existed!");
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

        private void btnFinish_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void globaListName_SelectedIndexChanged(object sender, EventArgs e)  //140707_2
        {

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
                    txtDescription.BackColor = Color.Yellow;
                }
                else
                {
                    txtDescription.BackColor = Color.White;
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
                    this.cboTypical.BackColor = Color.Yellow;
                    this.cboItemName.Enabled = true;
                    blnAddNewPrmtr = true;  //140917_0 
                    clearCboItems();
                    //dgvPrmtr.Enabled = false;
                }
                else if (state == (byte)MsgState.Edit)
                {
                    this.txtSaveResult.Text = "Edit current item...";
                    this.txtSaveResult.BackColor = Color.GreenYellow;
                    this.cboTypical.BackColor = Color.GreenYellow;
                    this.cboItemName.Enabled = true;
                    blnAddNewPrmtr = false;  //140917_0 
                }
                else if (state == (byte)MsgState.SaveOK)
                {
                    this.txtSaveResult.Text = "Item: " + this.cboItemName.Text + " has been saved successful!"; ;     //140917_0 cboTypical
                    this.txtSaveResult.BackColor = Color.YellowGreen;
                    this.cboTypical.BackColor = Color.YellowGreen;
                    this.cboItemName.Enabled = false;
                    blnAddNewPrmtr = false;  //140917_0 
                }
                else if (state == (byte)MsgState.Delete)
                {
                    this.txtSaveResult.Text = "Item: " + this.cboItemName.Text + " has been deleted successful!";   //140917_0 cboTypical
                    this.txtSaveResult.BackColor = Color.Pink;
                    this.cboTypical.BackColor = Color.Pink;
                    this.cboItemName.Enabled = false;
                    blnAddNewPrmtr = false;  //140917_0 
                    clearCboItems();
                }
                else if (state == (byte)MsgState.Clear)
                {
                    this.txtSaveResult.Text = "";
                    this.txtSaveResult.BackColor = Color.White;
                    this.cboTypical.BackColor = Color.White;
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

        private void btnPrmtrAdd_Click(object sender, EventArgs e)
        {
            try
            {
                setgrpPrmtr(true);
                currPrmtrID = MainForm.mylastIDTopoPNSpecParams + 1;
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
                    string myDeleteItemName = dgvPrmtr.CurrentRow.Cells["ID"].Value.ToString();
                    drst = (MessageBox.Show("Delete Item -->" + myDeleteItemName + "\n \n Pls choose 'Y' (是) to continue?",
                        "Notice", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2));

                    if (drst == DialogResult.Yes)
                    {
                        bool result = MainForm.DeleteItemForDT(MainForm.GlobalDS.Tables["TopoPNSpecsParams"], "PID=" + currPNID + "and ID=" + myDeleteItemName);
                        if (result)
                        {
                            RefreshDgvInfo();
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
                string filterString = "ID=" + currPrmtrID + " AND PID=" + currPNID;
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

                    myROWS[0]["SID"] = MainForm.getDTColumnInfo(MainForm.GlobalDS.Tables["GlobalSpecs"], "ID", "ItemName='" + this.cboItemName.Text + "'");
                    myROWS[0]["Typical"] = this.cboTypical.Text.ToString();
                    myROWS[0]["SpecMin"] = this.cboSpecMin.Text.ToString();
                    myROWS[0]["SpecMax"] = this.cboSpecMax.Text.ToString();
                    myROWS[0].EndEdit();
                    RefreshDgvInfo();
                    MainForm.setDgvCurrCell(dgvPrmtr, "ID", myROWS[0]["ID"].ToString());
                    result = true;
                }
                else if (blnAddNewPrmtr)
                {
                    DataRow myNewRow = mydt.NewRow();
                    myNewRow.BeginEdit();
                    myNewRow["ID"] = currPrmtrID;   //MainForm.mylastIDTestPrmtr + 1; 
                    myNewRow["PID"] = currPNID;  //140718_0 this.PID;
                    myNewRow["SID"] = MainForm.getDTColumnInfo(MainForm.GlobalDS.Tables["GlobalSpecs"], "ID", "ItemName='" + this.cboItemName.Text + "'");
                    myNewRow["Typical"] = this.cboTypical.Text.ToString();
                    myNewRow["SpecMin"] = this.cboSpecMin.Text.ToString();
                    myNewRow["SpecMax"] = this.cboSpecMax.Text.ToString();
                    mydt.Rows.Add(myNewRow);
                    myNewRow.EndEdit();
                    RefreshDgvInfo();
                    MainForm.setDgvCurrCell(dgvPrmtr, "ID", myNewRow["ID"].ToString());
                    MainForm.mylastIDTopoPNSpecParams++;
                    MainForm.myAddCountTopoPNSpecParams++;
                    result = true;
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
                if (!(MainForm.checkTypeOK(this.cboTypical.Text, "Double")
                        && MainForm.checkTypeOK(this.cboSpecMin.Text,"Double")
                        && MainForm.checkTypeOK(this.cboSpecMax.Text,"Double")) )                    
                {
                    return;
                }


                if (
                        this.cboTypical.Text.ToString().Trim().Length == 0 ||
                        this.cboItemName.Text.ToString().Trim().Length == 0 ||
                        this.cboSpecMin.Text.ToString().Trim().Length == 0 ||
                        this.cboSpecMax.Text.ToString().Trim().Length == 0
                    )
                {
                    MessageBox.Show("Some data is invalid !");
                    return;
                }

                if (blnAddNewPrmtr || (tempParamName.Length > 0 && tempParamName.ToUpper() != this.cboItemName.Text.ToString().ToUpper().Trim()))
                {
                    string tempSID = MainForm.getDTColumnInfo(MainForm.GlobalDS.Tables["GlobalSpecs"], "ID", "ItemName='" + this.cboItemName.Text + "'");
                    if (MainForm.currPrmtrCountExisted(MainForm.GlobalDS.Tables["TopoPNSpecsParams"],
                        "PID= " + currPNID + " and SID =" + tempSID) > 0)
                    {
                        //140704_1 约束PID+Name
                        MessageBox.Show("Edit data error! \n This item has been existed!<unique>");
                        return;
                    }
                }

                bool result = EditInfoForPrmtrDT(MainForm.GlobalDS.Tables["TopoPNSpecsParams"]);

                if (result)
                {
                    MainForm.ISNeedUpdateflag = true;
                    runPrmtrMsgState((byte)MsgState.SaveOK);
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

        private void dgvPrmtr_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                setgrpPrmtr(true);
                getInfoFromPrmtrDT(MainForm.GlobalDS.Tables["TopoPNSpecsParams"]);
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
                getInfoFromPrmtrDT(MainForm.GlobalDS.Tables["TopoPNSpecsParams"]);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void cboItemName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboItemName.SelectedIndex != -1)
            {
                this.txtDescription.Text = MainForm.getDTColumnInfo(MainForm.GlobalDS.Tables["GlobalSpecs"], "ItemDescription", "ItemName='" + this.cboItemName.Text +"'");
            }
        }

        //******************************************************
    }

}

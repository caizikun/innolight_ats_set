using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Maintain
{
    public partial class TestCtrlForm : Form
    {        
        public string TestPlanName = "";
        public string TestCtrlName = "";
        public string TestPrmtrName = "";

        public bool blnAddNewCtrl = false;
        public bool blnAddNewModel = false;
        public bool blnAddNewParameter = false;
               
        public long myCtrlPID = -1;
        public long myModelPID = -1;
        public long myPrmtrPID = -1;

        int currTestCtrlIndex = -1;
        int currTestModelIndex = -1;

        public TestCtrlForm()
        {
            InitializeComponent();
        }
        void showBtns(int level,bool ISNeedShow)    //140530_3
        {
            try
            {
                if (level == 0)
                {
                    this.btnCtrlDelete.Enabled = ISNeedShow;
                    this.btnCtrlEdit.Enabled = ISNeedShow;
                    this.btnCtrlDown.Enabled = ISNeedShow;
                    this.btnCtrlUp.Enabled = ISNeedShow;
                }

                if (level == 1)
                {
                    this.btnModelDelete.Enabled = ISNeedShow;
                    this.btnModelEdit.Enabled = ISNeedShow;
                    this.btnModelDown.Enabled = ISNeedShow;
                    this.btnModelUp.Enabled = ISNeedShow;
                }

                if (level == 2)
                {
                    this.btnEditPrmtr.Enabled = ISNeedShow;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void TestCtrlForm_Load(object sender, EventArgs e)
        {
            RefreshMyInfo(true);
            ShowMyTip();

            //140626_1 Add权限控制
            this.btnCtrlAdd.Visible = (MainForm.blnAddable ? true : false);
            this.btnModelAdd.Visible = (MainForm.blnAddable ? true : false);

            this.btnCtrlEdit.Visible = (MainForm.blnWritable ? true : false);
            this.btnModelEdit.Visible = (MainForm.blnWritable ? true : false);
            this.btnEditPrmtr.Visible = (MainForm.blnWritable ? true : false);

            this.btnCtrlDelete.Visible = (MainForm.blnDeletable ? true : false);
            this.btnModelDelete.Visible = (MainForm.blnDeletable ? true : false);

            this.btnCtrlDown.Visible = (MainForm.blnWritable ? true : false);
            this.btnCtrlUp.Visible = (MainForm.blnWritable ? true : false);
            this.btnModelDown.Visible = (MainForm.blnWritable ? true : false);
            this.btnModelUp.Visible = (MainForm.blnWritable ? true : false);
        }
        void ShowMyTip()
        {
            try
            {
                ToolTip mytip = new ToolTip();
                mytip.SetToolTip(btnEditPrmtr, "Modify TestModel Parameters");
                mytip.SetToolTip(btnFinish, "Finish");
                //mytip.SetToolTip(btnPrvpage, "返回上个窗体");

                mytip.SetToolTip(btnCtrlAdd, "Add a new flow control");
                mytip.SetToolTip(btnCtrlEdit, "Modify flow control Info");
                mytip.SetToolTip(btnCtrlDelete, "delete a flow control");
                mytip.SetToolTip(btnCtrlDown, "Move the SEQ of  flow control  Down");
                mytip.SetToolTip(btnCtrlUp, "Move the SEQ of  flow control  Up");

                mytip.SetToolTip(btnModelAdd, "Add a new test Model ");
                mytip.SetToolTip(btnModelEdit, "Modify test Model Info");
                mytip.SetToolTip(btnModelDelete, "delete a test Model");
                mytip.SetToolTip(btnModelDown, "Move the SEQ of test Model Down");
                mytip.SetToolTip(btnModelUp, "Move the SEQ of test Model Up"); 
                mytip.ShowAlways = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        void RefreshMyInfo(bool isFormLoad)
        {
            try
            {
                MainForm.showTablefilterStrInfo(MainForm.TopoToatlDS.Tables["TopoTestControl"], dgvTestCtrl, "PID=" + this.myCtrlPID);
                if (dgvTestCtrl.Rows.Count > 0)
                {
                    myModelPID = Convert.ToInt64(this.dgvTestCtrl.Rows[0].Cells["ID"].Value);
                    MainForm.showTablefilterStrInfo(MainForm.TopoToatlDS.Tables["TopoTestModel"], dgvTestModel, "PID=" + this.myModelPID);
                    crpTestModel.Enabled = true;
                    showBtns(0, true);
                }
                else
                {
                    showBtns(0, false);
                    crpTestModel.Enabled = false;
                    grpTestPrmtr.Enabled = false;
                }

                if (isFormLoad)     //140714_1 ADD
                {
                    if (dgvTestModel.Rows.Count > 0) 
                    {
                        myPrmtrPID = Convert.ToInt64(this.dgvTestModel.Rows[0].Cells["ID"].Value);
                        MainForm.showTablefilterStrInfo(MainForm.TopoToatlDS.Tables["TopoTestParameter"], dgvTestPrmtr, "PID=" + this.myPrmtrPID);
                        grpTestPrmtr.Enabled = true;
                        showBtns(1, true);
                        showBtns(2, true);
                    }
                    else
                    {
                        showBtns(1, false);
                        showBtns(2, false);
                        grpTestPrmtr.Enabled = false;
                        this.dgvTestPrmtr.DataSource = null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnFinish_Click(object sender, EventArgs e)
        {
            exitForm();
        }

        void exitForm()
        {
            this.Close();
        }

        private void dgvTestCtrl_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (dgvTestCtrl.CurrentRow != null && dgvTestCtrl.CurrentRow.Index != -1 && e.RowIndex != -1)  //140714_0
                {
                    if (!blnAddNewCtrl)
                    {
                        currTestCtrlIndex = e.RowIndex;
                        if (dgvTestCtrl.Rows.Count > 0)
                        {
                            myModelPID = MainForm.getNextPIDFromdgv(dgvTestCtrl, currTestCtrlIndex);
                            MainForm.showTablefilterStrInfo(MainForm.TopoToatlDS.Tables["TopoTestModel"], dgvTestModel, "PID=" + this.myModelPID);
                            crpTestModel.Enabled =true;
                            showBtns(0, true);
                        }
                        else
                        {
                            showBtns(0, false);
                            crpTestModel.Enabled = false;
                            grpTestPrmtr.Enabled = false;
                        }

                        if (dgvTestModel.Rows.Count > 0)
                        {
                            myPrmtrPID = Convert.ToInt64(this.dgvTestModel.Rows[0].Cells["ID"].Value);
                            MainForm.showTablefilterStrInfo(MainForm.TopoToatlDS.Tables["TopoTestParameter"], dgvTestPrmtr, "PID=" + this.myPrmtrPID);
                            grpTestPrmtr.Enabled = true;
                            showBtns(1, true);
                            showBtns(2, true);
                            DataGridViewCell cell = dgvTestModel.Rows[0].Cells["SEQ"];
                            dgvTestModel.CurrentCell = cell;
                        }
                        else
                        {
                            grpTestPrmtr.Enabled = false;
                            showBtns(1, false);
                            showBtns(2, false);
                            this.dgvTestPrmtr.DataSource = null;
                        }
                    }
                    else
                    {
                        if (dgvTestCtrl.Rows.Count > 0)
                        {
                            currTestCtrlIndex = dgvTestCtrl.Rows.Count - 1;
                            myModelPID = MainForm.getNextPIDFromdgv(dgvTestCtrl, currTestCtrlIndex);
                            MainForm.showTablefilterStrInfo(MainForm.TopoToatlDS.Tables["TopoTestModel"], dgvTestModel, "PID=" + this.myModelPID);
                            crpTestModel.Enabled = true;
                            showBtns(0, true);
                        }
                        else
                        {
                            crpTestModel.Enabled = false;
                            grpTestPrmtr.Enabled = false;
                            showBtns(0, false);
                        }

                        if (dgvTestModel.Rows.Count > 0)
                        {
                            myPrmtrPID = MainForm.mylastIDTestModel + 1;
                            MainForm.showTablefilterStrInfo(MainForm.TopoToatlDS.Tables["TopoTestParameter"], dgvTestPrmtr, "PID=" + this.myPrmtrPID);
                            grpTestPrmtr.Enabled = true;
                            showBtns(1, true);
                            showBtns(2, true);
                        }
                        else
                        {                            
                            grpTestPrmtr.Enabled = false;
                            showBtns(1, false);
                            showBtns(2, false);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

        private void dgvTestModel_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (dgvTestModel.CurrentRow != null && dgvTestModel.CurrentRow.Index != -1 && e.RowIndex != -1)  //140714_0 
                {
                    currTestModelIndex = e.RowIndex;    // Convert.ToInt32(dgvTestModel.Rows[e.RowIndex].Cells["id"].Value);

                    myPrmtrPID = MainForm.getNextPIDFromdgv(dgvTestModel, currTestModelIndex);
                    MainForm.showTablefilterStrInfo(MainForm.TopoToatlDS.Tables["TopoTestParameter"], dgvTestPrmtr, "PID=" + this.myPrmtrPID);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnCtrlAdd_Click(object sender, EventArgs e)
        {
           EditCtrl(true);
        }
        
        private void btnEditPrmtr_Click(object sender, EventArgs e)
        {
            try
            {
                if (myPrmtrPID !=-1)    //140530_3
                {
                    MainForm.myTestPrmtrISNewFlag = false; 
                    TestParameterInfo myTestParameterInfo = new TestParameterInfo();
                    if (myPrmtrPID == -1)
                    {
                        myTestParameterInfo.myPrmtrPID = Convert.ToInt64(this.dgvTestModel.CurrentRow.Cells["ID"].Value);
                    }
                    else
                    {
                        myTestParameterInfo.myPrmtrPID = myPrmtrPID;
                    }

                    myTestParameterInfo.ItemName = MainForm.getDTColumnInfo(MainForm.TopoToatlDS.Tables["TopoTestModel"], "ItemName", "ID=" + myPrmtrPID);

                    myTestParameterInfo.ShowDialog();

                }
                else
                {
                    MessageBox.Show("Pls choose a TestModel first!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnModelAdd_Click(object sender, EventArgs e)
        {
            EditModel(true);
        }
        void EditCtrl(bool isNewCtrl)
        {
            try
            {
                btnCtrlAdd.Enabled = false;
                btnCtrlEdit.Enabled = false;

                blnAddNewCtrl = isNewCtrl;
                CtrlInfo myCtrlInfo = new CtrlInfo();
                myCtrlInfo.blnAddNew = isNewCtrl;
                myCtrlInfo.TestPlanName = this.TestPlanName;

                string filterstring = "ItemName='" + myCtrlInfo.TestPlanName.Trim() + "' and PID=" + myCtrlPID + "";

                myCtrlInfo.PID = this.myCtrlPID;
                myCtrlInfo.ShowDialog();
                RefreshMyInfo(false);
                //blnAddNew = false;  //140530_1
                blnAddNewCtrl = false;
                btnCtrlAdd.Enabled = true;
                btnCtrlEdit.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void EditModel(bool isNewModel)
        {
            try
            {
                btnModelAdd.Enabled = false;
                btnModelEdit.Enabled = false;

                blnAddNewModel = isNewModel;

                ModelInfo myModelInfo = new ModelInfo();

                myModelInfo.PID = this.myModelPID;
                myModelInfo.TestCtrlName = MainForm.getDTColumnInfo(MainForm.TopoToatlDS.Tables["TopoTestControl"], "ItemName", "ID=" + myModelPID);   // MainForm.TopoToatlDS.Tables["TopoTestControl"].Rows[
                myModelInfo.ShowDialog();       //show NextForm...

                RefreshMyInfo(false);
                btnModelAdd.Enabled = true;
                btnModelEdit.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnCtrlEdit_Click(object sender, EventArgs e)
        {
            EditCtrl(false);
        }

        private void btnModelEdit_Click(object sender, EventArgs e)
        {
            EditModel(false);
        }

        private void btnModelDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvTestModel.CurrentRow !=null && dgvTestModel.CurrentRow.Index != -1)  //140710_2
                {
                    int CurrIndex = this.dgvTestModel.CurrentRow.Index;
                    DialogResult drst = new DialogResult();
                    drst = (MessageBox.Show("Delete Item -->" + dgvTestModel.CurrentRow.Cells["ItemName"].Value.ToString() + "\n \n Choose 'Y' (是)  to continue?"
                        , "Notice", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2));

                    if (drst == DialogResult.Yes)
                    {
                        string sName = dgvTestModel.CurrentRow.Cells["ItemName"].Value.ToString();
                        long myPID = Convert.ToInt32(dgvTestModel.CurrentRow.Cells["PID"].Value);
                        if (myPID != myModelPID)
                        {
                            MessageBox.Show("Item No: " + CurrIndex + "!ItemName =" + sName + " Data is not correct!Can't do this!");
                            return;
                        }
                        //DataTable资料移除部分待新增!!!

                        bool result = MainForm.DeleteItemForDT(MainForm.TopoToatlDS.Tables[3], "PID=" + myPID + "and ItemName='" + sName + "'");
                        if (result)
                        {
                            MainForm.ISNeedUpdateflag = true; //140603_2
                            MessageBox.Show("Item No: " + CurrIndex + ";ItemName =" + sName + " deleted OK!");
                        }
                        else
                        {
                            MessageBox.Show("Item No: " + CurrIndex + "!ItemName =" + sName + " deleted Fail!!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void btnCtrlDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvTestCtrl.CurrentRow !=null && dgvTestCtrl.CurrentRow.Index != -1)    //140710_2
                {
                    int CurrIndex = this.dgvTestCtrl.CurrentRow.Index;
                    DialogResult drst = new DialogResult();
                    drst = (MessageBox.Show("Delete Item -->" + dgvTestCtrl.CurrentRow.Cells["ItemName"].Value.ToString() +
                        "\n \n Choose 'Y' (是)  to continue?", "Notice", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2));

                    if (drst == DialogResult.Yes)
                    {   
                        string sName = dgvTestCtrl.CurrentRow.Cells["ItemName"].Value.ToString();
                        long myPID = this.myCtrlPID;

                        //DataTable资料移除部分待新增!!!

                        bool result = MainForm.DeleteItemForDT(MainForm.TopoToatlDS.Tables[2], "PID=" + myPID + "and ItemName='" + sName + "'");
                        if (result)
                        {
                            MainForm.ISNeedUpdateflag = true; //140603_2
                            MessageBox.Show("Item No: " + CurrIndex + ";ItemName =" + sName + " deleted OK!");
                        }
                        else
                        {
                            MessageBox.Show("Item No: " + CurrIndex + "!ItemName =" + sName + " deleted Fail!!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

        private void btnCtrlUp_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvTestCtrl.CurrentRow.Index > 0)
                {
                    string str1 = "PID=" + this.myCtrlPID + " and ItemName='" + dgvTestCtrl.CurrentRow.Cells["ItemName"].Value.ToString() + "'";
                    string str2 = "PID=" + this.myCtrlPID + " and ItemName='" + dgvTestCtrl.Rows[dgvTestCtrl.CurrentRow.Index - 1].Cells["ItemName"].Value.ToString() + "'";
                    changeSEQ(1, dgvTestCtrl, MainForm.TopoToatlDS.Tables["TopoTestControl"], str1, str2);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnCtrlDown_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvTestCtrl.CurrentRow.Index >= 0 && dgvTestCtrl.CurrentRow.Index < dgvTestCtrl.Rows.Count-1)   //141028_0 
                {
                    string str1 = "PID=" + this.myCtrlPID + " and ItemName='" + dgvTestCtrl.CurrentRow.Cells["ItemName"].Value.ToString() + "'";
                    string str2 = "PID=" + this.myCtrlPID + " and ItemName='" + dgvTestCtrl.Rows[dgvTestCtrl.CurrentRow.Index + 1].Cells["ItemName"].Value.ToString() + "'";
                    changeSEQ(-1, dgvTestCtrl, MainForm.TopoToatlDS.Tables["TopoTestControl"], str1, str2);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnModelDown_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvTestModel.CurrentRow.Index >= 0 && dgvTestModel.CurrentRow.Index < dgvTestModel.Rows.Count - 1)   //141028_0 
                {
                    string str1 = "PID=" + this.myModelPID + " and ItemName='" + dgvTestModel.CurrentRow.Cells["ItemName"].Value.ToString() + "'";
                    string str2 = "PID=" + this.myModelPID + " and ItemName='" + dgvTestModel.Rows[dgvTestModel.CurrentRow.Index + 1].Cells["ItemName"].Value.ToString() + "'";
                    changeSEQ(-1, dgvTestModel, MainForm.TopoToatlDS.Tables["TopoTestModel"], str1, str2);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnModelUp_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvTestModel.CurrentRow.Index > 0)
                {
                    string str1 = "PID=" + this.myModelPID + " and ItemName='" + dgvTestModel.CurrentRow.Cells["ItemName"].Value.ToString() + "'";
                    string str2 = "PID=" + this.myModelPID + " and ItemName='" + dgvTestModel.Rows[dgvTestModel.CurrentRow.Index - 1].Cells["ItemName"].Value.ToString() + "'";
                    changeSEQ(1, dgvTestModel, MainForm.TopoToatlDS.Tables["TopoTestModel"], str1, str2);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void changeSEQ(int direction, DataGridView dgv, DataTable dt, string filterStr1, string filterStr2)
        {
            int myCurrRowSEQ = -1, myPrevRowSEQ = -1;
            try
            {
                if (direction == 1)
                {
                    myCurrRowSEQ = Convert.ToInt32(dgv.CurrentRow.Cells["SEQ"].Value);
                    myPrevRowSEQ = Convert.ToInt32(dgv.Rows[dgv.CurrentRow.Index - 1].Cells["SEQ"].Value);
                }
                else if (direction == -1)
                {
                    myCurrRowSEQ = Convert.ToInt32(dgv.CurrentRow.Cells["SEQ"].Value);
                    myPrevRowSEQ = Convert.ToInt32(dgv.Rows[dgv.CurrentRow.Index + 1].Cells["SEQ"].Value);
                }
                else
                {
                    MessageBox.Show("direction must be +1 or -1");
                    return;
                }

                DataRow[] dr1 = dt.Select(filterStr1);

                if (dr1.Length == 1)
                {
                    dr1[0]["SEQ"] = myPrevRowSEQ;
                }
                else
                {
                    MessageBox.Show("Data is not unique!  " + dr1.Length + " records existed!");
                }

                DataRow[] dr2 = dt.Select(filterStr2);

                if (dr2.Length == 1)
                {
                    dr2[0]["SEQ"] = myCurrRowSEQ;
                }
                else
                {
                    MessageBox.Show("Data is not unique!  " + dr2.Length + " records existed!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}

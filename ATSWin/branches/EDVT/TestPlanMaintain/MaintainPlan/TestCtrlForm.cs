using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace TestPlan
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
            this.btnCtrlAdd.Visible = (PNInfo.blnAddable ? true : false);
            this.btnModelAdd.Visible = (PNInfo.blnAddable ? true : false);

            this.btnCtrlEdit.Visible = (PNInfo.blnWritable ? true : false);
            this.btnModelEdit.Visible = (PNInfo.blnWritable ? true : false);
            this.btnEditPrmtr.Visible = (PNInfo.blnWritable ? true : false);

            this.btnCtrlDelete.Visible = (PNInfo.blnDeletable ? true : false);
            this.btnModelDelete.Visible = (PNInfo.blnDeletable ? true : false);

            this.btnCtrlDown.Visible = (PNInfo.blnWritable ? true : false);
            this.btnCtrlUp.Visible = (PNInfo.blnWritable ? true : false);
            this.btnModelDown.Visible = (PNInfo.blnWritable ? true : false);
            this.btnModelUp.Visible = (PNInfo.blnWritable ? true : false);
        }
        void ShowMyTip()
        {
            try
            {
                ToolTip mytip = new ToolTip();
                mytip.SetToolTip(btnEditPrmtr, "编辑TestModel参数");
                mytip.SetToolTip(btnFinish, "完成维护并关闭窗体");
                //mytip.SetToolTip(btnPrvpage, "返回上个窗体");

                mytip.SetToolTip(btnCtrlAdd, "新增新的测试流程");                
                mytip.SetToolTip(btnCtrlEdit, "编辑选择的测试流程");
                mytip.SetToolTip(btnCtrlDelete, "删除选择的测试流程");
                mytip.SetToolTip(btnCtrlDown, "将当前测试流程的顺序下移");
                mytip.SetToolTip(btnCtrlUp, "将当前测试流程的顺序上移");

                mytip.SetToolTip(btnModelAdd, "新增新的测试模型");
                mytip.SetToolTip(btnModelEdit, "编辑选择的测试模型");
                mytip.SetToolTip(btnModelDelete, "删除选择的测试模型");
                mytip.SetToolTip(btnModelDown, "将当前测试模型的顺序下移");
                mytip.SetToolTip(btnModelUp, "将当前测试模型的顺序上移"); 
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
                PNInfo.showTablefilterStrInfo(PNInfo.TopoToatlDS.Tables["TopoTestControl"], dgvTestCtrl, "PID=" + this.myCtrlPID);
                if (dgvTestCtrl.Rows.Count > 0)
                {
                    myModelPID = Convert.ToInt64(this.dgvTestCtrl.Rows[0].Cells["ID"].Value);
                    PNInfo.showTablefilterStrInfo(PNInfo.TopoToatlDS.Tables["TopoTestModel"], dgvTestModel, "PID=" + this.myModelPID);
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
                        PNInfo.showTablefilterStrInfo(PNInfo.TopoToatlDS.Tables["TopoTestParameter"], dgvTestPrmtr, "PID=" + this.myPrmtrPID);
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
                            myModelPID = PNInfo.getNextPIDFromdgv(dgvTestCtrl, currTestCtrlIndex);
                            PNInfo.showTablefilterStrInfo(PNInfo.TopoToatlDS.Tables["TopoTestModel"], dgvTestModel, "PID=" + this.myModelPID);
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
                            PNInfo.showTablefilterStrInfo(PNInfo.TopoToatlDS.Tables["TopoTestParameter"], dgvTestPrmtr, "PID=" + this.myPrmtrPID);
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
                            myModelPID = PNInfo.getNextPIDFromdgv(dgvTestCtrl, currTestCtrlIndex);
                            PNInfo.showTablefilterStrInfo(PNInfo.TopoToatlDS.Tables["TopoTestModel"], dgvTestModel, "PID=" + this.myModelPID);
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
                            myPrmtrPID = PNInfo.mylastIDTestModel + 1;
                            PNInfo.showTablefilterStrInfo(PNInfo.TopoToatlDS.Tables["TopoTestParameter"], dgvTestPrmtr, "PID=" + this.myPrmtrPID);
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

                    myPrmtrPID = PNInfo.getNextPIDFromdgv(dgvTestModel, currTestModelIndex);
                    PNInfo.showTablefilterStrInfo(PNInfo.TopoToatlDS.Tables["TopoTestParameter"], dgvTestPrmtr, "PID=" + this.myPrmtrPID);
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
                    PNInfo.myTestPrmtrISNewFlag = false; 
                    TestParameterInfo myTestParameterInfo = new TestParameterInfo();
                    if (myPrmtrPID == -1)
                    {
                        myTestParameterInfo.myPrmtrPID = Convert.ToInt64(this.dgvTestModel.CurrentRow.Cells["ID"].Value);
                    }
                    else
                    {
                        myTestParameterInfo.myPrmtrPID = myPrmtrPID;
                    }

                    myTestParameterInfo.ItemName = PNInfo.getDTColumnInfo(PNInfo.TopoToatlDS.Tables["TopoTestModel"], "ItemName", "ID=" + myPrmtrPID);

                    myTestParameterInfo.ShowDialog();

                }
                else
                {
                    MessageBox.Show("请选择TestModel后再点击按钮!");
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
                myModelInfo.TestCtrlName = PNInfo.getDTColumnInfo(PNInfo.TopoToatlDS.Tables["TopoTestControl"], "ItemName", "ID=" + myModelPID);   // PNInfo.TopoToatlDS.Tables["TopoTestControl"].Rows[
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
                    drst = (MessageBox.Show("即将进行删除资料TestCtrl -->" + dgvTestModel.CurrentRow.Cells["ItemName"].Value.ToString() + "\n \n 选择 'Y' (是) 继续?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2));

                    if (drst == DialogResult.Yes)
                    {
                        string sName = dgvTestModel.CurrentRow.Cells["ItemName"].Value.ToString();
                        long myPID = Convert.ToInt32(dgvTestModel.CurrentRow.Cells["PID"].Value);
                        if (myPID != myModelPID)
                        {
                            MessageBox.Show("项目资料 序号为: " + CurrIndex + "!ItemName =" + sName + "的资料PID有问题!暂不允许删除!!!");
                            return;
                        }
                        //DataTable资料移除部分待新增!!!

                        bool result = PNInfo.DeleteItemForDT(PNInfo.TopoToatlDS.Tables[3], "PID=" + myPID + "and ItemName='" + sName + "'");
                        if (result)
                        {
                            PNInfo.ISNeedUpdateflag = true; //140603_2
                            MessageBox.Show("项目资料 序号为: " + CurrIndex + ";ItemName =" + sName + "已经移除!");
                        }
                        else
                        {
                            MessageBox.Show("项目资料 序号为: " + CurrIndex + "!ItemName =" + sName + "移除失败!");
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
                    drst = (MessageBox.Show("即将进行删除资料TestCtrl -->" + dgvTestCtrl.CurrentRow.Cells["ItemName"].Value.ToString() + "\n \n 选择 'Y' (是) 继续?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2));

                    if (drst == DialogResult.Yes)
                    {   
                        string sName = dgvTestCtrl.CurrentRow.Cells["ItemName"].Value.ToString();
                        long myPID = this.myCtrlPID;

                        //DataTable资料移除部分待新增!!!

                        bool result = PNInfo.DeleteItemForDT(PNInfo.TopoToatlDS.Tables[2], "PID=" + myPID + "and ItemName='" + sName + "'");
                        if (result)
                        {
                            PNInfo.ISNeedUpdateflag = true; //140603_2
                            MessageBox.Show("项目资料 序号为: " + CurrIndex + ";Name =" + sName + "已经移除!");
                        }
                        else
                        {
                            MessageBox.Show("项目资料 序号为: " + CurrIndex + "!Name =" + sName + "移除失败!");
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
                    PNInfo.ChangeSEQ(1, dgvTestCtrl, PNInfo.TopoToatlDS.Tables["TopoTestControl"], str1, str2);
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
                if (dgvTestCtrl.CurrentRow.Index >= 0)
                {
                    string str1 = "PID=" + this.myCtrlPID + " and ItemName='" + dgvTestCtrl.CurrentRow.Cells["ItemName"].Value.ToString() + "'";
                    string str2 = "PID=" + this.myCtrlPID + " and ItemName='" + dgvTestCtrl.Rows[dgvTestCtrl.CurrentRow.Index + 1].Cells["ItemName"].Value.ToString() + "'";
                    PNInfo.ChangeSEQ(-1, dgvTestCtrl, PNInfo.TopoToatlDS.Tables["TopoTestControl"], str1, str2);
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
                if (dgvTestModel.CurrentRow.Index >= 0)
                {
                    string str1 = "PID=" + this.myModelPID + " and ItemName='" + dgvTestModel.CurrentRow.Cells["ItemName"].Value.ToString() + "'";
                    string str2 = "PID=" + this.myModelPID + " and ItemName='" + dgvTestModel.Rows[dgvTestModel.CurrentRow.Index + 1].Cells["ItemName"].Value.ToString() + "'";
                    PNInfo.ChangeSEQ(-1, dgvTestModel, PNInfo.TopoToatlDS.Tables["TopoTestModel"], str1, str2);

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
                    PNInfo.ChangeSEQ(1, dgvTestModel, PNInfo.TopoToatlDS.Tables["TopoTestModel"], str1, str2);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}

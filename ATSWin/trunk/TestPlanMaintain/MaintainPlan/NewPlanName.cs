using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Authority;
using ATSDataBase;

namespace Maintain
{
    public partial class NewPlanName : Form
    {
        public bool blnCancelNewPlan = false;
        public LoginInfoStruct newPlanLoginInfoStruct=new LoginInfoStruct();
        DataIO pDataIO;
        string[] ConstTestPlanTables = new string[] { "GlobalProductionName", "TopoTestPlan", "TopoTestControl", "TopoTestModel", "TopoTestParameter", "TopoEquipment", "TopoEquipmentParameter" };
        public long currTypeID = -1, currPNID = -1, currTestPlanID = -1, currTestCtrlID = -1, currTestModelID = -1, currTestEquipID = -1;
        public DataSet ds;
        DataTable GlobalTypeDT;
        bool addNewCtrl = false;

        public bool isChangedDataSource = false;

        public NewPlanName()
        {
            InitializeComponent();
        }

        public NewPlanName(bool isAddCtrl)
        {            
            InitializeComponent();            
            this.chkChangeDS.Visible = false;
            addNewCtrl = isAddCtrl;
            this.Text = "NewFollowControl";
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (MainForm.checkItemLength("NewPlanName", txtNewName.Text, 30))
                {
                    return;
                }
                if (txtNewName.Text.ToString().Trim().Length == 0)
                {
                    MessageBox.Show("NewPlanName is null!Pls try again!");
                }
                else
                {
                    if (chkChangeDS.Checked)
                    {
                        if (this.cboPlanName.SelectedIndex != -1)
                        {
                            isChangedDataSource = true;
                            ds = new DataSet();
                            ds = getCurrTestPlanDS(this.cboPlanName.SelectedItem.ToString());
                        }
                        else
                        {
                            isChangedDataSource = true;
                            MessageBox.Show("PlanName of DataSource is null!Pls try again!");
                            ds = null;
                            return;
                        }
                    }
                    else
                    {
                        isChangedDataSource = false;
                    } 
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            blnCancelNewPlan = true;
            this.Close();
        }

        private void chkChangeDS_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkChangeDS.Checked)
            {
                this.Size = new Size(272, 158);
                grpDS.Visible = false;
                btnOK.Location = new Point(23, 76);
                btnCancel.Location = new Point(155, 76);                
            }
            else
            {
                this.Size = new Size(272, 300);
                grpDS.Visible = true;
                cboItemType.Enabled = false;

                btnOK.Location = new Point(23, 224);
                btnCancel.Location = new Point(155, 224);                
            }
        }        

        private void NewPlanName_Load(object sender, EventArgs e)
        {
            lblCtrl.Visible = addNewCtrl;       //150411_1
            cboCtrlName.Visible = addNewCtrl;   //150411_1
            chkChangeDS.Checked = false;
            chkChangeDS_CheckedChanged(sender, e);

            initType();
            cboItemType.Enabled = false;
            ConfigXmlIO pConfigXmlIO = new ConfigXmlIO(Application.StartupPath + @"\Config.xml");
            newPlanLoginInfoStruct.ServerName = pConfigXmlIO.ServerName; ;
            newPlanLoginInfoStruct.DBName = pConfigXmlIO.ATSDBName;
            newPlanLoginInfoStruct.DBUser = pConfigXmlIO.ATSUser;
            newPlanLoginInfoStruct.DBPassword = pConfigXmlIO.ATSPWD;
        }

        void rdoCheckd()
        {
            try
            {
                ds = new DataSet();                
                if (rdoATSDebug.Checked || rdoATSHome.Checked || rdoLocal.Checked)
                {                    
                    cboItemType.Enabled = true;

                    if (rdoATSDebug.Checked)
                    {
                        newPlanLoginInfoStruct.blnISDBSQLserver = true;
                        newPlanLoginInfoStruct.AccessFilePath = "SQL Server";  
                        newPlanLoginInfoStruct.DBName = "ATSDebug";
                        pDataIO = new SqlManager(@"INPCSZ0518\ATS_Home", newPlanLoginInfoStruct.DBName, newPlanLoginInfoStruct.DBUser, newPlanLoginInfoStruct.DBPassword);
                    }
                    else if (rdoATSHome.Checked)
                    {
                        newPlanLoginInfoStruct.blnISDBSQLserver = true;
                        newPlanLoginInfoStruct.AccessFilePath = "SQL Server"; 
                        newPlanLoginInfoStruct.DBName = "ATSHome";
                        pDataIO = new SqlManager(@"INPCSZ0518\ATS_Home", newPlanLoginInfoStruct.DBName, newPlanLoginInfoStruct.DBUser, newPlanLoginInfoStruct.DBPassword);
                    }
                    else if (rdoLocal.Checked)
                    {
                        newPlanLoginInfoStruct.blnISDBSQLserver = false;

                        OpenFileDialog openFileDialog = new OpenFileDialog();
                        openFileDialog.InitialDirectory = Application.StartupPath;//注意这里写路径时要用c:\\而不是c:\
                        //openFileDialog.Filter = "文本文件|*.*|C#文件|*.cs|所有文件|*.*";
                        openFileDialog.Filter = "Access文件|*.mdb|Access文件|*.accdb|所有文件|*.*";
                        openFileDialog.RestoreDirectory = true;
                        openFileDialog.FilterIndex = 2;
                        DialogResult blnISselected = openFileDialog.ShowDialog();
                        if (openFileDialog.FileName.Length != 0 && blnISselected == DialogResult.OK)
                        {
                            newPlanLoginInfoStruct.AccessFilePath = openFileDialog.FileName.Trim();
                        }
                        else
                        {
                            MessageBox.Show("Pls choose a path of the Local Accdb Path!", "The path of Accdb file is not existed!", MessageBoxButtons.OK);
                            rdoLocal.Checked = false;
                            return;
                        }   
                        pDataIO = new AccessManager(newPlanLoginInfoStruct.AccessFilePath);
                    }

                    if (pDataIO.OpenDatabase(true))
                    {
                        pDataIO.OpenDatabase(false);
                    }

                    string StrTableName = "GlobalProductionType";
                    string StrSelectconditions = " order by ID";
                    GlobalTypeDT = GetTestPlanInfo(StrTableName, StrSelectconditions);

                    for (int i = 0; i < GlobalTypeDT.Rows.Count; i++)
                    {
                        this.cboItemType.Items.Add(GlobalTypeDT.Rows[i]["ItemName"].ToString());
                        cboItemType.SelectedIndex = 0;
                    }                
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void initType()
        {
            cboItemType.Items.Clear();
            initPN();
            cboPN.Enabled = false;
        }

        void initPN()
        {
            cboPN.Items.Clear();
            initPlan();
            cboPlanName.Enabled = false;
        }

        void initPlan()
        {
            cboPlanName.Items.Clear();
            cboCtrlName.Items.Clear();
            cboCtrlName.Enabled = false;
        }

        private void rdoLocal_CheckedChanged(object sender, EventArgs e)
        {
            initType();
            if (rdoLocal.Checked)
            {
                rdoCheckd();
            }
        }

        private void rdoATSDebug_CheckedChanged(object sender, EventArgs e)
        {
            initType();
            if (rdoATSDebug.Checked)
            {
                rdoCheckd();
            }
        }

        private void rdoATSHome_CheckedChanged(object sender, EventArgs e)
        {
            initType();
            if (rdoATSHome.Checked)
            {
                rdoCheckd();
            }
        }

        //以DataTable方式获取信息
        DataTable GetTestPlanInfo(string StrTableName, string sqlQueryCmd)
        {
            try
            {
                DataTable mydt = new DataTable(StrTableName);
                string StrSelectconditions = "select * from " + StrTableName + " " + sqlQueryCmd;
                mydt = pDataIO.GetDataTable(StrSelectconditions, StrTableName);// 获得环境的DataTable
                return mydt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }
        DataSet getCurrTestPlanDS(string TestPlanName)
        {
            DataSet myNewDS = new DataSet();
            try
            {
                if (TestPlanName.Trim().Length == 0)
                {
                    MessageBox.Show("Pls choose a TestPlan first!");
                    return null;
                }
                else
                {
                    for (int i = 0; i < ConstTestPlanTables.Length; i++)
                    {   //{ "GlobalProductionName", "TopoTestPlan", "TopoTestControl", "TopoTestModel", "TopoTestParameter", "TopoEquipment", "TopoEquipmentParameter" };
                        if (i == 0)
                        {
                            myNewDS.Tables.Add(GetTestPlanInfo(ConstTestPlanTables[i], " where PN='" + cboPN.Text.ToString() + "'"));
                        }
                        else if (i == 1)
                        {
                            myNewDS.Tables.Add(GetTestPlanInfo(ConstTestPlanTables[i],
                                " where ItemName='" + this.cboPlanName.SelectedItem + "' and PID=" + currPNID));
                        }
                        else if (i == 2)
                        {
                            myNewDS.Tables.Add(GetTestPlanInfo(ConstTestPlanTables[i], " where PID = " + currTestPlanID));
                        }
                        else if (i == 3)
                        {
                            myNewDS.Tables.Add(GetTestPlanInfo(ConstTestPlanTables[i],
                                " where PID in ( select id from TopoTestControl  where PID=" + currTestPlanID + ")"));
                        }
                        else if (i == 4)
                        {
                            myNewDS.Tables.Add(GetTestPlanInfo(ConstTestPlanTables[i],
                                " where PID in ( select id from TopoTestModel where PID in ( select id from TopoTestControl  where PID=" + currTestPlanID + "))"));
                        }
                        else if (i == 5)
                        {
                            myNewDS.Tables.Add(GetTestPlanInfo(ConstTestPlanTables[i], " where PID = " + currTestPlanID));
                        }
                        else if (i == 6)
                        {
                            myNewDS.Tables.Add(GetTestPlanInfo(ConstTestPlanTables[i],
                                " where PID in ( select id from TopoEquipment where PID=" + currTestPlanID + ")"));
                        }
                    }
                    myNewDS.Tables[1].PrimaryKey = new DataColumn[] { myNewDS.Tables[1].Columns["ID"] };
                    myNewDS.Tables[2].PrimaryKey = new DataColumn[] { myNewDS.Tables[2].Columns["ID"] };
                    myNewDS.Tables[3].PrimaryKey = new DataColumn[] { myNewDS.Tables[3].Columns["ID"] };
                    myNewDS.Tables[4].PrimaryKey = new DataColumn[] { myNewDS.Tables[4].Columns["ID"] };
                    myNewDS.Tables[5].PrimaryKey = new DataColumn[] { myNewDS.Tables[5].Columns["ID"] };
                    myNewDS.Tables[6].PrimaryKey = new DataColumn[] { myNewDS.Tables[6].Columns["ID"] };

                    myNewDS.Relations.Add("relation1", myNewDS.Tables[0].Columns["id"], myNewDS.Tables[1].Columns["pid"]);
                    myNewDS.Relations.Add("relation2", myNewDS.Tables[1].Columns["id"], myNewDS.Tables[2].Columns["pid"]);
                    myNewDS.Relations.Add("relation3", myNewDS.Tables[2].Columns["id"], myNewDS.Tables[3].Columns["pid"]);
                    myNewDS.Relations.Add("relation4", myNewDS.Tables[3].Columns["id"], myNewDS.Tables[4].Columns["pid"]);
                    myNewDS.Relations.Add("relation5", myNewDS.Tables[1].Columns["id"], myNewDS.Tables[5].Columns["pid"]);
                    myNewDS.Relations.Add("relation6", myNewDS.Tables[5].Columns["id"], myNewDS.Tables[6].Columns["pid"]);

                    return myNewDS;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }
                
        private void cboItemType_SelectedIndexChanged(object sender, EventArgs e)
        {
            currTypeID = -1;
            try
            {
                if (cboItemType.SelectedIndex != -1)
                {
                    initPN();
                    for (int i = 0; i < this.GlobalTypeDT.Rows.Count; i++)
                    {
                        if (GlobalTypeDT.Rows[i]["ItemName"].ToString().ToUpper() == cboItemType.Text.ToString().ToUpper())
                        {
                            currTypeID = Convert.ToInt64(GlobalTypeDT.Rows[i]["ID"]);
                            break;
                        }
                    }
                    if (currTypeID == -1)
                    {
                        MessageBox.Show("Data not existed :" + cboItemType.Text.ToString().ToUpper() + "!");                        
                        return;
                    }
                    else
                    {
                        initPN();
                        string sqlCondition = "PID=" + currTypeID;

                        DataRow[] mrDRs = pDataIO.GetDataTable("Select * from GlobalProductionName where " + sqlCondition, "GlobalProductionName").Select(sqlCondition);
                        for (int i = 0; i < mrDRs.Length; i++)
                        {
                            cboPN.Items.Add(mrDRs[i]["PN"].ToString());
                            cboPN.Enabled = true;                            
                        }
                        if (cboPN.Items.Count > 0)
                        {
                            cboPN.SelectedIndex = 0;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Pls choose a Type first!");                    
                }
            }
            catch (Exception ex)
            {
                initPN();
                MessageBox.Show(" Error,Pls Check Again \n" + ex.ToString());
            }
        }

        private void cboPN_SelectedIndexChanged(object sender, EventArgs e)
        {
            currPNID = -1;
            try
            {
                if (cboPN.SelectedIndex != -1)
                {
                    initPlan();
                    string sqlCondition = "PID=" + currTypeID;
                    DataRow[] mrDRs = pDataIO.GetDataTable("Select * from GlobalProductionName where " + sqlCondition, "GlobalProductionName").Select(sqlCondition);
                    for (int i = 0; i < mrDRs.Length; i++)
                    {
                        if (mrDRs[i]["PN"].ToString().ToUpper() == cboPN.Text.ToString().ToUpper())
                        {
                            currPNID = Convert.ToInt64(mrDRs[i]["ID"]);
                            break;
                        }
                    } 
                    if (currPNID == -1)
                    {
                        MessageBox.Show("Data not existed :" + cboPN.Text.ToString().ToUpper() + "!");                        
                        return;
                    }
                    else
                    {
                        sqlCondition = "PID=" + currPNID ;
                        cboPlanName.Enabled = true;
                        initPlan();

                        DataRow[] mrPlanDRs = pDataIO.GetDataTable("Select * from TopoTestPlan where " + sqlCondition, "TopoTestPlan").Select(sqlCondition);
                        for (int i = 0; i < mrPlanDRs.Length; i++)
                        {
                            cboPlanName.Items.Add(mrPlanDRs[i]["ItemName"].ToString());
                        }
                    }        
                }
                else
                {
                    MessageBox.Show("Pls choose a PN first!");                    
                }
            }
            catch (Exception ex)
            {
                initPlan();
                MessageBox.Show(" Error,Pls Check Again!\n" + ex.ToString());
            }
        }


        private void cboPlanName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboPlanName.SelectedIndex != -1)
                {
                    currTestPlanID = Convert.ToInt32(pDataIO.GetDataTable("select * from topoTestPlan where PID=" + currPNID + " and ItemName ='" + cboPlanName.SelectedItem.ToString() + "'", "topoTestPlan").Rows[0]["ID"].ToString());
                    this.cboCtrlName.Items.Clear();                    

                    if (addNewCtrl)
                    {
                        string sqlCondition = "PID=" + currTestPlanID;
                        DataRow[] myDRs = pDataIO.GetDataTable("Select * from TopoTestControl where " + sqlCondition, "TopoTestControl").Select(sqlCondition);
                        for (int i = 0; i < myDRs.Length; i++)
                        {
                            this.cboCtrlName.Items.Add(myDRs[i]["ItemName"].ToString());
                            this.cboCtrlName.Enabled = true;
                        }
                        if (cboCtrlName.Items.Count > 0)
                        {
                            cboCtrlName.SelectedIndex = 0;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Pls choose a PlanName first!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(" Error,Pls Check Again!\n" + ex.ToString());
            }
        }
        private void NewPlanName_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void txtNewName_TextChanged(object sender, EventArgs e)
        {
            if (addNewCtrl && this.chkChangeDS.Checked == false)
            {
                this.chkChangeDS.Checked = true;
            }
        }

    }
}

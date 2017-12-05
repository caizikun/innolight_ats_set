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
        string[] ConstEquip = new string[] { "GlobalAllEquipmentList", "GlobalAllEquipmentParamterList" };
        string[] ConstModel = new string[] { "GlobalAllTestModelList", "GlobalTestModelParamterList" };
        string[] ConstMSAItem = new string[] { "GlobalMSA", "GlobalMSADefintionInf" };
        string[] ConstMCoefGroup = new string[] { "GlobalManufactureCoefficientsGroup", "GlobalManufactureCoefficients" };
        string[] ConstPNChip = new string[] { "GlobalProductionName", "GlobalManufactureChipsetControl", "GlobalManufactureChipsetInitialize" };       

        DataTable GlobalTypeDT;
        public bool blnCancelNewPlan = false;
        public LoginInfoStruct newPlanLoginInfoStruct=new LoginInfoStruct();
        DataIO pDataIO;
        
        public long currPNTypeID = -1, currItemID = -1, currChildItemPID = -1, currChildItemID = -1;
        public DataSet ds;

        public long myPID = -1;
        string myFunctionName = "";
        public bool isChangedDataSource = false;

        public NewPlanName(string functionName)
        {
            myFunctionName = functionName;
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (MainForm.checkItemLength("NewName", txtNewName.Text, 25))
                {
                    return;
                }
                if (txtNewName.Text.ToString().Trim().Length == 0)
                {
                    MessageBox.Show("NewName is null!Pls try again!");
                }
                else
                {
                    if (chkChangeDS.Checked)
                    {
                        if (this.cboItem.SelectedIndex != -1)
                        {
                            isChangedDataSource = true;
                            ds = new DataSet();
                            //MSAInfo MCoefGroup PNInfoForm ModelInfo EquipmentForm
                            if (myFunctionName.ToUpper() == "MSAInfo".ToUpper())
                            {
                                ds = getCurrTestPlanDS(this.cboItem.SelectedItem.ToString(), ConstMSAItem);
                                copyOtherDSPlan(this.txtNewName.Text, ConstMSAItem, ds);
                            }
                            else if (myFunctionName.ToUpper() == "MCoefGroup".ToUpper())
                            {
                                ds = getCurrTestPlanDS(this.cboItem.SelectedItem.ToString(), ConstMCoefGroup);
                                copyOtherDSPlan(this.txtNewName.Text, ConstMCoefGroup, ds);
                            }
                            else if (myFunctionName.ToUpper() == "PNInfoForm".ToUpper())
                            {
                                if (this.cboMCoef.SelectedIndex==-1)
                                {
                                    MessageBox.Show("MCoef is null!Pls try again!");
                                    return;
                                }
                                ds = getCurrTestPlanDS(this.cboItem.SelectedItem.ToString(), ConstPNChip);
                                copyOtherDSPlan(this.txtNewName.Text, ConstPNChip, ds);
                            }
                            else if (myFunctionName.ToUpper() == "ModelInfo".ToUpper())
                            {
                                ds = getCurrTestPlanDS(this.cboItem.SelectedItem.ToString(), ConstModel);
                                copyOtherDSPlan(this.txtNewName.Text, ConstModel, ds);
                            }
                            else if (myFunctionName.ToUpper() == "EquipmentForm".ToUpper())
                            {
                                ds = getCurrTestPlanDS(this.cboItem.SelectedItem.ToString(), ConstEquip);
                                copyOtherDSPlan(this.txtNewName.Text, ConstEquip, ds);
                            }
                            ds = null;
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
                this.Size = new Size(272, 257);
                grpDS.Visible = true;
                cboItem.Enabled = false;

                btnOK.Location = new Point(23, 182);
                btnCancel.Location = new Point(155, 182);                
            }
        }        

        private void NewPlanName_Load(object sender, EventArgs e)
        {
            this.cboPNType.Visible = false;
            this.lblPNType.Visible = false;
            this.cboMCoef.Visible = false;
            this.lblMCoef.Visible = false;

            if (myFunctionName.ToUpper() == "MSAInfo".ToUpper())
            {
                lblItem.Text = "MSA";
                this.Text = "New " + lblItem.Text;
            }
            else if (myFunctionName.ToUpper() == "MCoefGroup".ToUpper())
            {
                lblItem.Text = "MCoef";
                this.Text = "New " + lblItem.Text;
            }
            else if (myFunctionName.ToUpper() == "PNInfoForm".ToUpper())
            {
                lblItem.Text = "PN";
                this.Text = "New " + lblItem.Text;
                this.cboPNType.Visible = true;                
                this.cboMCoef.Visible = true;
                lblMCoef.Visible = true;
                lblPNType.Visible = true;
            }
            else if (myFunctionName.ToUpper() == "ModelInfo".ToUpper())
            {
                lblItem.Text = "Model";
                this.Text = "New " + lblItem.Text;
            }
            else if (myFunctionName.ToUpper() == "EquipmentForm".ToUpper())
            {
                lblItem.Text = "Equip";
                this.Text = "New " + lblItem.Text;
            }
            else
            {
                MessageBox.Show("Error:Not support item-->" + myFunctionName);
                this.Close();
            }
            chkChangeDS.Checked = true;
            chkChangeDS_CheckedChanged(sender, e);

            initType();
            cboItem.Enabled = false;
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
                if (rdoATS_V2.Checked || rdoATSHome.Checked || rdoLocal.Checked)
                {                    
                    cboItem.Enabled = true;
                    cboPNType.Items.Clear();
                    cboMCoef.Items.Clear();
                    GlobalTypeDT = null; 
                    if (rdoATS_V2.Checked)
                    {
                        newPlanLoginInfoStruct.blnISDBSQLserver = true;
                        newPlanLoginInfoStruct.AccessFilePath = "SQL Server";
                        newPlanLoginInfoStruct.DBName = "ATS_V2";
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
                        if (myFunctionName.ToUpper() == "PNInfoForm".ToUpper())
                        {
                            string StrTableName = "GlobalProductionType";
                            string StrSelectconditions = " order by ID";
                            GlobalTypeDT = GetTestPlanInfo(StrTableName, StrSelectconditions);

                            for (int i = 0; i < GlobalTypeDT.Rows.Count; i++)
                            {
                                this.cboPNType.Items.Add(GlobalTypeDT.Rows[i]["ItemName"].ToString());
                                cboPNType.SelectedIndex = 0;
                            }
                        }
                        else if (myFunctionName.ToUpper() == "MSAInfo".ToUpper())
                        {
                            string StrTableName = ConstMSAItem[0];
                            string StrSelectconditions = " order by ID";
                            GlobalTypeDT = GetTestPlanInfo(StrTableName, StrSelectconditions);

                            for (int i = 0; i < GlobalTypeDT.Rows.Count; i++)
                            {
                                this.cboItem.Items.Add(GlobalTypeDT.Rows[i]["ItemName"].ToString());
                                
                            }
                        }
                        else if (myFunctionName.ToUpper() == "MCoefGroup".ToUpper())
                        {
                            string StrTableName = this.ConstMCoefGroup[0];
                            string StrSelectconditions = " order by ID";
                            GlobalTypeDT = GetTestPlanInfo(StrTableName, StrSelectconditions);

                            for (int i = 0; i < GlobalTypeDT.Rows.Count; i++)
                            {
                                this.cboItem.Items.Add(GlobalTypeDT.Rows[i]["ItemName"].ToString());
                                
                            }
                        }
                        else if (myFunctionName.ToUpper() == "ModelInfo".ToUpper())
                        {
                            string StrTableName = this.ConstModel[0];
                            string StrSelectconditions = " order by ID";
                            GlobalTypeDT = GetTestPlanInfo(StrTableName, StrSelectconditions);

                            for (int i = 0; i < GlobalTypeDT.Rows.Count; i++)
                            {
                                this.cboItem.Items.Add(GlobalTypeDT.Rows[i]["ItemName"].ToString());
                                
                            }
                        }
                        else if (myFunctionName.ToUpper() == "EquipmentForm".ToUpper())
                        {
                            string StrTableName = this.ConstEquip[0];
                            string StrSelectconditions = " order by ID";
                            GlobalTypeDT = GetTestPlanInfo(StrTableName, StrSelectconditions);

                            for (int i = 0; i < GlobalTypeDT.Rows.Count; i++)
                            {
                                this.cboItem.Items.Add(GlobalTypeDT.Rows[i]["ItemName"].ToString());
                                
                            }
                        }
                        else 
                        {
                            MessageBox.Show("Not support current item...");
                        }
                        pDataIO.OpenDatabase(false);
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
            cboPNType.Items.Clear();  
            cboItem.Items.Clear();
            cboMCoef.Items.Clear();
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
            if (rdoATS_V2.Checked)
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

        DataSet getCurrTestPlanDS(string sourceName, string[] tableNames)
        {
            DataSet myNewDS = new DataSet();
            try
            {
                if (sourceName.Trim().Length == 0)
                {
                    MessageBox.Show("Pls choose a TestPlan first!");
                    return null;
                }
                else
                {
                    for (int i = 0; i < tableNames.Length; i++)
                    {  
                        if (i == 0)
                        {
                            if (myFunctionName.ToUpper() == "PNInfoForm".ToUpper())
                            {
                                myNewDS.Tables.Add(GetTestPlanInfo(tableNames[0], " where PN='" + sourceName + "'"));
                            }
                            else
                            {
                                myNewDS.Tables.Add(GetTestPlanInfo(tableNames[0], " where ItemName='" + sourceName + "'"));
                            }
                            
                        }
                        else if (i == 1)
                        {
                            myNewDS.Tables.Add(GetTestPlanInfo(tableNames[i],
                                " where PID=" + currChildItemPID));
                        }
                        else if (tableNames.Length == 3 && i == 2 && tableNames[2].ToUpper()==ConstPNChip[2].ToUpper())
                        {
                            myNewDS.Tables.Add(GetTestPlanInfo(tableNames[i],
                                " where PID=" + currChildItemPID));
                        }
                    }
                    myNewDS.Tables[0].PrimaryKey = new DataColumn[] { myNewDS.Tables[0].Columns["ID"] };
                    myNewDS.Tables[1].PrimaryKey = new DataColumn[] { myNewDS.Tables[1].Columns["ID"] };                    

                    myNewDS.Relations.Add("relation1", myNewDS.Tables[0].Columns["id"], myNewDS.Tables[1].Columns["pid"]);

                    if (tableNames.Length == 3 && tableNames[2].ToUpper() == ConstPNChip[2].ToUpper())
                    {
                        myNewDS.Tables[2].PrimaryKey = new DataColumn[] { myNewDS.Tables[2].Columns["ID"] };
                        myNewDS.Relations.Add("relation2", myNewDS.Tables[0].Columns["id"], myNewDS.Tables[2].Columns["pid"]);
                    }

                    return myNewDS;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }
                
        private void cboItem_SelectedIndexChanged(object sender, EventArgs e)
        {            
            try
            {
                currItemID = -1;
                if (cboItem.SelectedIndex != -1)
                {
                    if (myFunctionName.ToUpper() == "PNInfoForm".ToUpper())
                    {
                        getCurrFuncID("PN= '" + cboItem.SelectedItem.ToString() + "'", ConstPNChip[0]);
                        
                    }
                    else if (myFunctionName.ToUpper() == "MSAInfo".ToUpper())                                       
                    {
                        getCurrFuncID("ItemName= '" + cboItem.SelectedItem.ToString() + "'", this.ConstMSAItem[0]);
                    }
                    else if (myFunctionName.ToUpper() == "MCoefGroup".ToUpper())
                    {
                        getCurrFuncID("ItemName= '" + cboItem.SelectedItem.ToString() + "'", this.ConstMCoefGroup[0]);
                    }
                    else if (myFunctionName.ToUpper() == "ModelInfo".ToUpper())
                    {
                        getCurrFuncID("ItemName= '" + cboItem.SelectedItem.ToString() + "'", this.ConstModel[0]);
                    }
                    else if (myFunctionName.ToUpper() == "EquipmentForm".ToUpper())
                    {
                        getCurrFuncID("ItemName= '" + cboItem.SelectedItem.ToString() + "'", this.ConstEquip[0]);
                    }
                }
                else
                {
                    MessageBox.Show("Pls choose a item first!");                    
                }
            }
            catch (Exception ex)
            {
                currItemID = -1;
                MessageBox.Show(" Error,Pls Check Again \n" + ex.ToString());
            }
        }

        bool getCurrFuncID(string queryCMD,string tableName)
        {
            bool reslut = false;
            try
            {
                if (cboItem.SelectedIndex != -1)
                {
                    DataRow[] DRs = pDataIO.GetDataTable("Select * from " + tableName + " where " + queryCMD, tableName).Select(queryCMD);
                    if (DRs.Length == 1)
                    {
                        currItemID = Convert.ToInt32(DRs[0]["ID"].ToString());
                        currChildItemPID = currItemID;
                        reslut = true;
                    }
                    else
                    {
                        MessageBox.Show("Data not existed :" + cboItem.Text.ToString().ToUpper() + "!");
                    }
                }
                else
                {
                    MessageBox.Show("Pls choose a item first!");
                }
                return reslut;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return reslut;
            }
            finally
            {
                btnOK.Enabled = reslut;
            }
        }

        private void NewPlanName_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void cboPNType_SelectedIndexChanged(object sender, EventArgs e)
        {            
            try
            {
                if (cboPNType.SelectedIndex != -1)
                {
                    cboMCoef.Items.Clear(); //150414_0
                    cboItem.Items.Clear();
                    currPNTypeID = -1;
                      
                    for (int i = 0; i < this.GlobalTypeDT.Rows.Count; i++)
                    {
                        if (GlobalTypeDT.Rows[i]["ItemName"].ToString().ToUpper() == cboPNType.Text.ToString().ToUpper())
                        {
                            currPNTypeID = Convert.ToInt64(GlobalTypeDT.Rows[i]["ID"]);
                            break;
                        }
                    }
                    if (currPNTypeID == -1)
                    {
                        MessageBox.Show("Data not existed :" + cboPNType.Text.ToString().ToUpper() + "!");
                        return;
                    }
                    else
                    {
                        string sqlCondition = "PID=" + currPNTypeID;

                        DataRow[] mrDRs = pDataIO.GetDataTable("Select * from GlobalProductionName where " + sqlCondition, "GlobalProductionName").Select(sqlCondition);
                        for (int i = 0; i < mrDRs.Length; i++)
                        {
                            cboItem.Items.Add(mrDRs[i]["PN"].ToString());
                            cboItem.Enabled = true;
                        }
                        if (cboItem.Items.Count > 0)
                        {
                            cboItem.SelectedIndex = 0;
                        }

                        //150414_0
                        DataRow[] MCoefDRs = MainForm.GlobalDS.Tables["GlobalManufactureCoefficientsGroup"].Select("IgnoreFlag='false' and TypeID=" + currPNTypeID);
                        for (int i = 0; i < MCoefDRs.Length; i++)
                        {
                            this.cboMCoef.Items.Add(MCoefDRs[i]["ItemName"].ToString());
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
                MessageBox.Show(" Error,Pls Check Again \n" + ex.ToString());
            }
        }

        DataRow convertNewDR(DataRow sourceDR, DataTable destDT, int myRowIndex, long myPID, long myID)
        {
            try
            {
                DataRow dr = destDT.NewRow();
                for (int n = 0; n < destDT.Columns.Count; n++)
                {
                    if (destDT.Columns[n].ColumnName.ToUpper() == "ID")
                    {
                        dr["ID"] = myID;
                    }
                    else if (destDT.Columns[n].ColumnName.ToUpper() == "PID")
                    {
                        if (destDT.Columns.Contains("PID"))
                        {
                            dr["PID"] = myPID;
                        }
                    }
                    else
                    {
                        dr[n] = sourceDR[n].ToString();
                    }
                }
                return dr;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }

        bool copyOtherDSPlan(string NewName,string [] tableNames, DataSet myDs)
        {
            try
            {               
                //Start----------->
                string myFilterString = "";
                if (myFunctionName.ToUpper() == "PNInfoForm".ToUpper())
                {
                    myFilterString = "PN ='" + this.cboItem.SelectedItem.ToString() + "'";
                }
                else
                {
                    myFilterString = "ItemName ='" + this.cboItem.SelectedItem.ToString() + "'";
                }
                DataRow[] DRSTopoTestPlan = myDs.Tables[tableNames[0]].Select(myFilterString);

                //string[] ConstEquip = new string[] { "GlobalAllEquipmentList", "GlobalAllEquipmentParamterList" };
                //string[] ConstModel = new string[] { "GlobalAllTestModelList", "GlobalTestModelParamterList" };
                //string[] ConstMSAItem = new string[] { "GlobalMSA", "GlobalMSADefintionInf" };
                //string[] ConstMCoefGroup = new string[] { "GlobalManufactureCoefficientsGroup", "GlobalManufactureCoefficients" };
                //string[] ConstPNChip = new string[] { "GlobalProductionName", "GlobalManufactureChipsetControl" ,"GlobalManufactureChipsetInitialize"};
                
                for (int i = 0; i < DRSTopoTestPlan.Length; i++)
                {
                    if (tableNames[0].ToUpper() == "GlobalAllEquipmentList".ToUpper())
                    {
                        MainForm.mylastIDGlobalEquip++;
                        currItemID = MainForm.mylastIDGlobalEquip;
                    }
                    else if (tableNames[0].ToUpper() == "GlobalAllTestModelList".ToUpper())
                    {
                        MainForm.mylastIDTestModel++;
                        currItemID = MainForm.mylastIDTestModel;
                    }
                    else if (tableNames[0].ToUpper() == "GlobalMSA".ToUpper())
                    {
                        MainForm.mylastIDGlobalMSA++;
                        currItemID = MainForm.mylastIDGlobalMSA;
                    }
                    else if (tableNames[0].ToUpper() == "GlobalManufactureCoefficientsGroup".ToUpper())
                    {
                        MainForm.mylastIDGlobalMCoefGroup++;
                        currItemID = MainForm.mylastIDGlobalMCoefGroup;
                    }
                    else if (tableNames[0].ToUpper() == "GlobalProductionName".ToUpper())
                    {
                        MainForm.mylastIDGlobalPN++;
                        currItemID = MainForm.mylastIDGlobalPN;
                    }

                    DataRow drPItem = MainForm.GlobalDS.Tables[tableNames[0]].NewRow();
                    drPItem = convertNewDR(DRSTopoTestPlan[i], MainForm.GlobalDS.Tables[tableNames[0]], i, myPID, currItemID);
                    if (myFunctionName.ToUpper() == "PNInfoForm".ToUpper())
                    {
                        drPItem["PN"] = NewName; //myNewPlanName 需要手动指定;
                        string currMGroupID = MainForm.getDTColumnInfo(MainForm.GlobalDS.Tables["GlobalManufactureCoefficientsGroup"], "ID", "ItemName='" + this.cboMCoef.Text + "'");
                        drPItem["MCoefsID"] = currMGroupID;
                    }
                    else
                    {
                        drPItem["ItemName"] = NewName; //myNewPlanName 需要手动指定;
                    }
                    MainForm.GlobalDS.Tables[tableNames[0]].Rows.Add(drPItem);
                    
                    myFilterString = "PID=" + DRSTopoTestPlan[i]["ID"];

                    # region childItem
                    DataRow[] drsChildItem = myDs.Tables[tableNames[1]].Select(myFilterString);
                    for (int m = 0; m < drsChildItem.Length; m++)
                    {
                        if (tableNames[1].ToUpper() == "GlobalAllEquipmentParamterList".ToUpper())
                        {
                            MainForm.mylastIDGlobalEquipPrmtr++;
                            currChildItemID = MainForm.mylastIDGlobalEquipPrmtr;
                        }
                        else if (tableNames[1].ToUpper() == "GlobalTestModelParamterList".ToUpper())
                        {
                            MainForm.mylastIDTestPrmtr++;
                            currChildItemID = MainForm.mylastIDTestPrmtr;
                        }
                        else if (tableNames[1].ToUpper() == "GlobalMSADefintionInf".ToUpper())
                        {
                            MainForm.mylastIDGlobalMSAPrmtr++;
                            currChildItemID = MainForm.mylastIDGlobalMSAPrmtr;
                        }
                        else if (tableNames[1].ToUpper() == "GlobalManufactureCoefficients".ToUpper())
                        {
                            MainForm.mylastIDGlobalMCoefPrmtr++;
                            currChildItemID = MainForm.mylastIDGlobalMCoefPrmtr;
                        }
                        else if (tableNames[1].ToUpper() == "GlobalManufactureChipsetControl".ToUpper())
                        {
                            MainForm.mylastIDGlobalChipCtrl++;
                            currChildItemID = MainForm.mylastIDGlobalChipCtrl;
                        }                          
                        DataRow drChildItem = MainForm.GlobalDS.Tables[tableNames[1]].NewRow();
                        drChildItem = convertNewDR(drsChildItem[m], MainForm.GlobalDS.Tables[tableNames[1]], m, currItemID, currChildItemID);
                        MainForm.GlobalDS.Tables[tableNames[1]].Rows.Add(drChildItem);                                             
                    }

                    if (tableNames.Length == 3 && tableNames[2].ToUpper() == "GlobalManufactureChipsetInitialize".ToUpper())
                    {
                        DataRow[] drsChildItem2 = myDs.Tables[tableNames[2]].Select(myFilterString);
                    
                        for (int m = 0; m < drsChildItem2.Length; m++)
                        {                            
                            MainForm.mylastIDGlobalChipInit++;
                            currChildItemID = MainForm.mylastIDGlobalChipInit;

                            DataRow drChildItem2 = MainForm.GlobalDS.Tables[tableNames[2]].NewRow();
                            drChildItem2 = convertNewDR(drsChildItem2[m], MainForm.GlobalDS.Tables[tableNames[2]], m, currItemID, currChildItemID);
                            MainForm.GlobalDS.Tables[tableNames[1]].Rows.Add(drChildItem2);
                        }
                    }
                    #endregion
                }
                MessageBox.Show(NewName + " created successful...-->dataSource= " + this.cboItem.SelectedItem.ToString());
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }
    }
}

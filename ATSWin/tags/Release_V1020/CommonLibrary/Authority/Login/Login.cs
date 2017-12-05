using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using ATSDataBase;
//using ADOX;
using System.Data.OleDb;
using Maintain; //需要引用其他的命名空间


namespace Authority
{
    public struct LoginInfoStruct
    {
        public string ServerName;
        public string DBName;
        public string DBUser;
        public string DBPassword;

        public string UserName;
        public string AccessFilePath;
        public bool blnISDBSQLserver;
        public long myAccessCode;

    }
    
    public partial class Login : Form
    {
        LoginInfoStruct myLoginInfoStruct = new LoginInfoStruct();
        ConfigXmlIO myConfigXmlIO;      
        
        string ServerName = "";   
        string DBName = "";       
        string DBUser = "";     
        string DBPassword = "";      

        string UserName = "";
        string AccessFilePath = "";
        bool blnISDBSQLserver = false;    
        long myAccessCode = -1;
        bool isExportData = false;

        DataIO mySQL;                       
        DataTable myInfo;
        ToolTip mytip = new ToolTip();
        
        //----------------------------
        //140612
        private string[] ConstTestPlanTables = new string[] { "TopoTestPlan", "TopoTestControl", "TopoTestModel", "TopoTestParameter", "TopoEquipment", "TopoEquipmentParameter" };
        private string[] ConstGlobalListTables = new string[] { "GlobalAllEquipmentList", "GlobalAllEquipmentParamterList", "GlobalAllAppModelList", "GlobalAllTestModelList", "GlobalTestModelParamterList" };
        private string[] ConstMSAItemTables = new string[] { "GlobalProductionType", "GlobalMSADefintionInf", "GlobalMSADTable" };
        private string[] ConstGlobalPNInfo = new string[] { "GlobalProductionType", "GlobalProductionName", "GlobalManufactureCoefficientsGroup", "GlobalManufactureCoefficients" };

        //ADOX.CatalogClass MyAccess = new CatalogClass();
        DataIO myAccessIO;
        string[] mySQLTables;
        string testPlanQueryCMD = "";
        //----------------------------
        int startTime = 0;
        int loginTimes = 0;

        public Login()
        {
            InitializeComponent();
        }
                
        private void Login_Load(object sender, EventArgs e)
        {
            try
            {
                timerDate.Enabled = false; //默认为False,当现实时自动回切换为True;
                mySQL = null;
                myConfigXmlIO = new ConfigXmlIO(Application.StartupPath + @"\Config.xml");
                grpLogin.Enabled = true;
                startTime = 0;
                ServerName = myConfigXmlIO.ServerName;
                DBName = myConfigXmlIO.ATSDBName;
                DBUser = myConfigXmlIO.ATSUser;
                DBPassword = myConfigXmlIO.ATSPWD;

                blnISDBSQLserver = (ServerName.ToUpper().Trim() == "Local".ToUpper().Trim() ? false : true);

                myLoginInfoStruct.ServerName = ServerName;
                myLoginInfoStruct.DBName = DBName;
                myLoginInfoStruct.DBUser = DBUser;
                myLoginInfoStruct.DBPassword = DBPassword;
                myLoginInfoStruct.blnISDBSQLserver = blnISDBSQLserver;                

                this.btnLogin.Visible = myConfigXmlIO.showBtnLogin;
                txtStates.Visible = false;
                
                if (blnISDBSQLserver)   //必须是SQLServer才显示ChangePWD
                {
                    if (Application.ProductName.ToUpper() == "Background Maintain".ToUpper())
                    {
                        this.btnCreatAccessDB.Visible = myConfigXmlIO.showBtnExport;
                    }

                    this.chkChangePWD.Visible = myConfigXmlIO.showBtnChangePwd;

                    this.chkChangePWD_CheckedChanged(sender, e);

                    if (DBName == null)
                    {
                        DBName = "ATSHome";
                        myConfigXmlIO.ATSDBName = DBName;
                    }
                    mySQL = new SqlManager(ServerName, DBName, DBUser, DBPassword);   //140917_2

                    this.Text = "Login[" + Application.ProductName + "](DS = " + DBName + ")";
                }
                else
                {
                    string currAccdbPath = "";
                    OpenFileDialog openFileDialog = new OpenFileDialog();                    
                    openFileDialog.InitialDirectory = Application.StartupPath;//注意这里写路径时要用c:\\而不是c:\
                    //openFileDialog.Filter = "文本文件|*.*|C#文件|*.cs|所有文件|*.*";
                    openFileDialog.Filter = "Access文件|*.mdb|Access文件|*.accdb|所有文件|*.*";
                    openFileDialog.Title = "Pls choose a path of local database file!";
                    openFileDialog.RestoreDirectory = true;
                    openFileDialog.FilterIndex = 2;
                    DialogResult blnISselected = openFileDialog.ShowDialog();
                    if (openFileDialog.FileName.Length != 0 && blnISselected == DialogResult.OK)
                    {
                        currAccdbPath = openFileDialog.FileName.Trim();
                        AccessFilePath = currAccdbPath;
                    }
                    else
                    {
                        AccessFilePath = "";
                        MessageBox.Show("Pls choose a path of the Local Accdb Path!", "The path of Accdb file is not existed!", MessageBoxButtons.OK);
                        this.Close();
                    }
                    mySQL = new AccessManager(currAccdbPath);
                    this.btnCreatAccessDB.Visible = myConfigXmlIO.showBtnExport;
                    this.chkChangePWD.Visible = false;
                    this.chkChangePWD_CheckedChanged(sender, e);
                    this.Text = "Login[" + Application.ProductName + "](LocalAccdb)";
                }
                               
                startTime = 1;
                showMyFormTip();
                mytip.ShowAlways = true;

                this.Show();

                if (this.txtPWD.TextLength > 0)
                {
                    txtPWD.SelectionStart = txtPWD.TextLength;
                    txtPWD.Focus();
                }
                else if (txtUserID.TextLength > 0)
                {
                    txtPWD.Focus();
                }
                else
                {
                    txtUserID.Focus();
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                this.Close();
            }
        }

        void showMyFormTip()
        {
            ToolTip myFormTip = new ToolTip();

            myFormTip.SetToolTip(this.btnChangePwd, "ChangePwd");
            myFormTip.SetToolTip(this.btnCreatAccessDB, "Export the server data to local Accdb...");

            myFormTip.SetToolTip(this.btnLogin, "Login system");
            myFormTip.SetToolTip(this.txtStates, "Operation state...");
            myFormTip.SetToolTip(this.chkChangePWD, "Pls check here when you want to change password!");
            myFormTip.ShowAlways = true;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            startTime = 0;
            try
            {
                UserName = this.txtUserID.Text;
                myLoginInfoStruct.UserName = UserName;
                btnLogin.Enabled = false;

                if (blnISDBSQLserver == false)
                {
                    if (isExportData)
                    {
                        OpenFileDialog openFileDialog = new OpenFileDialog();
                        openFileDialog.InitialDirectory = Application.StartupPath;//注意这里写路径时要用c:\\而不是c:\
                        //openFileDialog.Filter = "文本文件|*.*|C#文件|*.cs|所有文件|*.*";
                        openFileDialog.Filter = "Access文件|*.mdb|Access文件|*.accdb|所有文件|*.*";
                        openFileDialog.RestoreDirectory = true;
                        openFileDialog.Title = "Pls choose a path of local database file!";                        
                        openFileDialog.FilterIndex = 2;
                        DialogResult blnISselected = openFileDialog.ShowDialog();
                        if (openFileDialog.FileName.Length != 0 && blnISselected == DialogResult.OK)
                        {
                            AccessFilePath = openFileDialog.FileName.Trim();
                            showAccess();
                        }
                        else
                        {
                            MessageBox.Show("Pls choose a path of the Local Accdb Path!", "The path of Accdb file is not existed!", MessageBoxButtons.OK);
                        }
                    }
                    else if (AccessFilePath.Length != 0)
                    {
                        showAccess();
                    }
                    else
                    {
                        MessageBox.Show("The path of Accdb file is not existed!", "Error", MessageBoxButtons.OK);
                    }
                }
                else
                {
                    if (this.txtPWD.Text.Trim().Length == 0 || this.txtUserID.Text.Trim().Length == 0)
                    {
                        MessageBox.Show("Please enter a Login Name and enter a Password!", "Login Name or Password is Null", MessageBoxButtons.OK);
                    }
                    else
                    {
                        AccessFilePath = "SQL Server";
                        showAccess();
                    }
                }
                btnLogin.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Existed-->" + ex.Message + ";Source: " + ex.Source);
                btnLogin.Enabled = true;
            }
        }

        void showAccess()
        {
            try
            {                
                bool PWDOKFlag = false;
                
                string pUserID = "";

                #region SQLserver
                if (blnISDBSQLserver)
                {
                    //getLoginStutes(); 141031_00 取消检查登陆状态...
                    myInfo = mySQL.GetDataTable("select * from UserInfo where LoginName='" + this.txtUserID.Text.ToString() + "'", "Username");

                    DataRow[] myInfoRows = myInfo.Select("LoginName='" + this.txtUserID.Text.ToString() + "'");
                    if (myInfoRows.Length == 1)
                    {
                        //140612_2 Password-->UserPassword
                        if (myInfoRows[0]["LoginPassword"].ToString() == this.txtPWD.Text.ToString())
                        {
                            pUserID = myInfoRows[0]["ID"].ToString();
                            PWDOKFlag = true;
                        }
                        else
                        {
                            PWDOKFlag = false;
                        }
                    }

                    if (!PWDOKFlag)
                    {
                        loginTimes++;
                        if (loginTimes < 3)
                        {
                            MessageBox.Show("Sorry, either Login Name or Password is not correct. Please try again."
                                , " Login Name or Password is not correct", MessageBoxButtons.OK);
                            return;
                        }
                        else
                        {
                            MessageBox.Show("Sorry, either Login Name or Password is not correct more than 3 times!"
                                , "Warning!", MessageBoxButtons.OK);
                            this.Close();
                        }
                    }
                    else
                    {
                        //140626 获取权限--> 正常来讲一个账号角色分配为1个...
                        string queryCMD = "select sum(FunctionCode) from functiontable where id in "
                            + "(select RoleFunctionTable.FunctionID from RoleFunctionTable where RoleFunctionTable.RoleID  in "
                            + "(select roleID from UserRoleTable where UserRoleTable.UserID = " + pUserID + "))";
                        //+ pUserID + "))";--> ToString+ "(select id from UserInfo	where UserInfo.LoginName='" + this.txtUserID.Text.ToString() + "')))";
                        DataTable myFunctions = mySQL.GetDataTable(queryCMD, "UserRoleFuncTable");
                        
                        
                        if (myFunctions.Rows.Count > 0)
                        {
                            myAccessCode = Convert.ToInt64(myFunctions.Rows[0][0].ToString());
                        }
                        else
                        {
                            myAccessCode = -1;
                        }
                    }
                    mySQL.OpenDatabase(false);
                }
                #endregion 
                
                this.txtPWD.Text = "";
                this.Hide();
                myLoginInfoStruct.AccessFilePath = AccessFilePath;
                myLoginInfoStruct.myAccessCode = myAccessCode;

                MainForm myPNInfo = new MainForm(myLoginInfoStruct);                              
                myPNInfo.ShowDialog();

                loginTimes = 0;
                System.Threading.Thread.Sleep(100);
                this.Close();   
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }
        }

        /// <summary>
        /// 进行加密和解密操作!!!
        /// </summary>
        /// <param name="str">待加密或解密的字符串!</param>
        /// <param name="decode">解密? true =解密|false=加密</param>
        /// <param name="Codelength">1位密码的位元长度</param>
        /// <returns>返回结果 string</returns>
        string SetPWDCode(string str, bool decode, int Codelength)  //140918_0 增加加密和解密部分!
        {
            string resultStr = "";
            try
            {
                if (decode)
                {
                    int leftStr = str.Length % Codelength;
                    int arrayNum = str.Length / Codelength;
                    if (str.Length % Codelength != 0)
                    {
                        arrayNum++;
                        str = str.PadLeft(arrayNum * Codelength, '0');
                    }

                    string[] myCode = new string[arrayNum];
                    String[] pduCodeArray = new String[arrayNum]; //此字符串数组，分隔好的字符串

                    for (int i = 0; i < arrayNum; i++)
                    {
                        pduCodeArray[i] = str.Substring(0, Codelength);
                        myCode[i] = ((Convert.ToInt32(pduCodeArray[i], 16) + 3 + i) / 0x0f).ToString();// Convert.ToString(pduCodeArray[i], 16);
                        str = str.Substring(Codelength);
                    }

                    string tempStr = "";
                    for (int i = 0; i < myCode.Length; i++)
                    {
                        tempStr += Convert.ToString((char)(int.Parse(myCode[i])));

                    }
                    resultStr = tempStr;
                }
                else
                {
                    int arrayNum = str.Length;
                    string[] myCode = new string[arrayNum];
                    for (int i = 0; i < arrayNum; i++)
                    {
                        int charCode = char.Parse(str.Substring(0, 1));
                        if (charCode > 126 || charCode < 32)
                        {
                            MessageBox.Show("Chinese characters were existed in the current password string,Pls enter the password again!");
                            return null;
                        }
                        myCode[i] = Convert.ToString(char.Parse(str.Substring(0, 1)) * 0x0f - 3 - i, 16).PadLeft(Codelength, '0');
                        str = str.Substring(1);
                    }
                    string tempStr = "";
                    for (int i = 0; i < myCode.Length; i++)
                    {
                        tempStr += myCode[i];
                    }
                    resultStr = tempStr;
                }
                return resultStr;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return resultStr;
            }

        }
        
        
        private void timerDate_Tick(object sender, EventArgs e)
        {
            startTime++;
            mytip.SetToolTip(this, " The Application has been idle more than " + startTime + "S...\r\nIf more than 150 s, the system will be automatically exited!");
            if (startTime >= 150 && startTime > 0)
            {
                timerDate.Enabled = false;
                //MessageBox.Show("程式已经处于闲置状态超过60S,系统自动退出!","退出警告!",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                this.Close();
            }
            else if (startTime >= 60)
            {
                if (txtStates.Visible == false)
                {
                    txtStates.Visible = true;
                }
                else
                {
                    txtStates.Text = " The Application has been idle more than " + startTime + "S...\r\nIf more than 150 s, the system will be automatically exited!";
                    txtStates.ForeColor = Color.Red;
                    txtStates.Refresh();
                }
            }
            else if (btnCreatAccessDB.Visible == false)
            {
                txtStates.Text = "";
                txtStates.Visible = false;
            }
        }

        private void Login_VisibleChanged(object sender, EventArgs e)
        {
            if (timerDate.Enabled == true)
            {
                timerDate.Enabled = false;
            }
            else
            {
                timerDate.Enabled = true;
            }
        }

        private void txtUserID_TextChanged(object sender, EventArgs e)
        {
            startTime = 0;
        }

        private void txtPWD_TextChanged(object sender, EventArgs e)
        {
            startTime = 0;
        }
        
        private void btnCreatAccessDB_Click(object sender, EventArgs e)
        {
            startTime = 0;
            if (timerDate.Enabled)
            {
                timerDate.Enabled = false;
            }
            btnLogin.Enabled = false;
            btnCreatAccessDB.Enabled = false;
            btnChangePwd.Enabled = false;

            txtStates.Text = "";
            txtStates.Visible = true;
            string newFileName = "NewSQL.accdb";
            string myDbPath = Application.StartupPath+@"\" + newFileName;
            if (myDbPath == AccessFilePath)
            {
                MessageBox.Show("The path of datasource and the path of NewSQL.accdb are the same...\n "
                    + "NewFile will be renamed as A_" + newFileName);
                newFileName = "A_" + newFileName;
                myDbPath = Application.StartupPath + @"\" + newFileName;
            }
            try
            {
                if (System.IO.File.Exists(myDbPath))
                {
                    System.IO.File.Move(myDbPath, Application.StartupPath + @"\" + DateTime.Now.ToString("yyMMdd_HHmmss") + newFileName);
                    MessageBox.Show("The path " + myDbPath + " has been found '" + newFileName + "'  !\n Now will make the original file renamed as "
                        + DateTime.Now.ToString("yyMMdd_HHmmss") + newFileName + " \n");
                    //System.IO.File.Delete(dbName);
                    
                }

                System.Threading.Thread.Sleep(10);
                txtStates.Text = "System will create the new '" + newFileName + "', please wait...";
                txtStates.Refresh();

                System.Threading.Thread.Sleep(50);
                //MyAccess.Create("Provider =Microsoft.Jet.OLEDB.4.0;Data Source =" + dbName + ";"); //140709_1
                //MyAccess.Create("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + dbName );
                Microsoft.Office.Interop.Access.Application MyAccess = new Microsoft.Office.Interop.Access.Application();
                MyAccess.NewCurrentDatabase(myDbPath,
                    Microsoft.Office.Interop.Access.AcNewDatabaseFormat.acNewDatabaseFormatAccess2007);
                MyAccess.CloseCurrentDatabase();                
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(MyAccess);
                MyAccess = null;

                txtStates.Text = " System created the new '" + newFileName + "' OK!\r\n Next to get data of the tables form SQL Server!";
                txtStates.Refresh();

                DialogResult mySelect = MessageBox.Show("Please choose whether to import designated models specified testplan ?",
                        "Import TopoTable Data?", 
                        MessageBoxButtons.YesNo, 
                        MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button2);
                bool isNeedExportTestPlan = false;
               
                if (mySelect == DialogResult.Yes)
                {
                    PlanNameForm myPlan = new PlanNameForm(myLoginInfoStruct);

                    myPlan.ShowDialog();
                    
                    if (myPlan.blnAllPlans == true
                        || myPlan.ArrayPNPlan.Length > 0)   
                    {                        
                        isNeedExportTestPlan = true;
                        if (myPlan.blnAllPlans)
                        {
                            testPlanQueryCMD = "";
                        }
                        else if (myPlan.ArrayPNPlan.Length > 0)
                        {
                            testPlanQueryCMD=" where id in (";
                            int looptime = myPlan.ArrayPNPlan.Length / 2;
                            for (int k = 0; k < looptime; k++)
                            {
                                string queryCMD = "select id from TopoTestPlan where ItemName ='"
                                    + myPlan.ArrayPNPlan[k,1] +"' and PID in (select id from GlobalProductionName where PN='"
                                    + myPlan.ArrayPNPlan[k,0] +"')";
                                DataTable idDT = mySQL.GetDataTable(queryCMD, "IDNo");
                                if (idDT.Rows.Count > 0)
                                {
                                    string myPlanID = idDT.Rows[0][0].ToString();
                                    if (myPlanID != "0" && (k == looptime - 1))
                                    {
                                        testPlanQueryCMD = testPlanQueryCMD + myPlanID + ")";
                                    }
                                    else
                                    {
                                        testPlanQueryCMD = testPlanQueryCMD + myPlanID + ",";
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("The test plan data is not existed?", "Error");
                                }
                            }
                        }
                    }
                }
                else
                {
                    isNeedExportTestPlan = false;
                }

                myAccessIO = new AccessManager(myDbPath);
                if (!blnISDBSQLserver)
                {
                    mySQLTables = mySQL.GetCurrTablesName(AccessFilePath);
                }
                else
                {
                    mySQLTables = mySQL.GetCurrTablesName(ServerName, DBName, DBUser, DBPassword);
                }
                //140922_0 -------------------

                for (int i = 0; i < mySQLTables.Length; i++)
                {
                    #region 先创建表
                    if (mySQLTables[i].ToUpper().Contains("GLOBAL") || mySQLTables[i].ToUpper().Contains("TOPO"))
                    {
                        DataTable mydt = new DataTable(mySQLTables[i]);
                        mydt = mySQL.GetDataTable("select * from " + mySQLTables[i], mySQLTables[i]);

                        int Count = mydt.Columns.Count;

                        string[] ColumnsName = new string[Count];
                        string[] ColumnsType = new string[Count];
                        string[] ColumnsSize = new string[Count];
                        string[] AllowNull = new string[Count];
                        string[] ColumnsDefaultValue = new string[Count];
                        string[] isAUTOINCREMENT = new string[Count];
                        string ColumnInfo = "";
                        string Sql = "";
                        #region 获取Column资料
                        for (int j = 0; j < Count; j++)
                        {

                            ColumnsName[j] = mydt.Columns[j].ColumnName.ToString();
                            if (mydt.Columns[j].DataType.ToString().ToUpper().Contains("INT32"))
                            {
                                ColumnsType[j] = "Int";
                                ColumnsSize[j] = "";
                            }
                            else if (mydt.Columns[j].DataType.ToString().ToUpper().Contains("INT16"))   //141031_0 
                            {
                                ColumnsType[j] = "Int";
                                ColumnsSize[j] = "";
                            }
                            else if (mydt.Columns[j].DataType.ToString().ToUpper().Contains("STRING"))
                            {
                                //141010_0 防止长度超过255 统一改为Memo
                                //ColumnsType[j] = "text";
                                //ColumnsSize[j] = "(255)";
                                
                                ColumnsType[j] = "Memo";
                                ColumnsSize[j] = "";

                            }
                            else if (mydt.Columns[j].DataType.ToString().ToUpper().Contains("DOUBLE"))
                            {
                                ColumnsType[j] = "Float";
                                ColumnsSize[j] = "";

                            }
                            else if (mydt.Columns[j].DataType.ToString().ToUpper().Contains("BYTE"))
                            {
                                ColumnsType[j] = "TinyInt";
                                ColumnsSize[j] = "";

                            }
                            else if (mydt.Columns[j].DataType.ToString().ToUpper().Contains("DATETIME"))
                            {
                                ColumnsType[j] = "DateTime";
                                ColumnsSize[j] = "";

                            }
                            else if (mydt.Columns[j].DataType.ToString().ToUpper().Contains("SINGLE"))
                            {
                                ColumnsType[j] = "Real";
                                ColumnsSize[j] = "";

                            }
                            else if (mydt.Columns[j].DataType.ToString().ToUpper().Contains("BOOLEAN"))
                            {
                                ColumnsType[j] = "Bit";
                                ColumnsSize[j] = "";

                            }
                            else if (mydt.Columns[j].DataType.ToString().ToUpper().Contains("NTEXT"))
                            {
                                ColumnsType[j] = "Memo";
                                ColumnsSize[j] = "";

                            }
                            else
                            {
                                MessageBox.Show("This type does not support->" + mydt.Columns[j].DataType.ToString().ToUpper());
                                return;
                            }                     

                            //if (mydt.Columns[j].AllowDBNull == false)
                            //{
                            //不允许为空!                                
                            //AllowNull[j] = "NOT NULL";
                            //(mydt.Columns[j].AllowDBNull == false ? "NOT NULL" : "");
                            //}
                            //else
                            //{
                                AllowNull[j] = "";
                            //}
                            ColumnsDefaultValue[j] = mydt.Columns[j].DefaultValue.ToString(); //需要再后面添加

                            //if (ColumnsName[j].ToUpper()=="ID") //创建表时设置自增会报错!
                            //{
                            //    isAUTOINCREMENT[j] = "AUTOINCREMENT";
                            //}
                            //else
                            //{
                            //    isAUTOINCREMENT[j] = "";
                            //}
                            if (j != Count - 1)
                            {
                                ColumnInfo = ColumnInfo + ColumnsName[j] + " " + isAUTOINCREMENT[j] + " "
                                    + ColumnsType[j] + ColumnsSize[j] + " " + AllowNull[j] + " " + ColumnsDefaultValue[j] + ",";
                            }
                            else
                            {
                                ColumnInfo = ColumnInfo + ColumnsName[j] + " " + isAUTOINCREMENT[j] + " "
                                    + ColumnsType[j] + ColumnsSize[j] + " " + AllowNull[j] + " " + ColumnsDefaultValue[j] + ")";
                            }
                        }
                        #endregion
                        Sql = "CREATE TABLE " + mySQLTables[i] + "(" + ColumnInfo;
                        //Sql = "CREATE TABLE GlobalAllAppModelList(ID text(5),Name text(255),Description text (255)  )";
                        if (mySQLTables[i].ToUpper() == "TopoRunRecordTable".ToUpper()
                            || mySQLTables[i].ToUpper() == "TopoLogRecord".ToUpper()
                            || mySQLTables[i].ToUpper() == "TopoTestData".ToUpper()
                            || mySQLTables[i].ToUpper() == "TopoTestCoefBackup".ToUpper()
                            )   //140707_0 对于存档的资料部分需要设置ID自动递增
                        {
                            if (creatTable(myDbPath, Sql, mySQLTables[i], true) == false)
                            {
                                MessageBox.Show("Creat table Error :" + myDbPath + "-->" + mySQLTables[i]);
                                txtStates.Text = "Creat table Error :" + myDbPath + "-->" + mySQLTables[i];
                            }
                        }
                        else
                        {
                            if (creatTable(myDbPath, Sql, mySQLTables[i], false) == false)
                            {
                                MessageBox.Show("Creat table Error :" + myDbPath + "-->" + mySQLTables[i]);
                                txtStates.Text = "Creat table Error :" + myDbPath + "-->" + mySQLTables[i];
                            }
                            else
                            {
                                txtStates.Text = "Creat table  :" + myDbPath + "-->" + mySQLTables[i];
                                txtStates.Refresh();
                            }
                        }
                        System.Threading.Thread.Sleep(50);
                        //更新信息的资料
                        if (mySQLTables[i].ToUpper().Contains("GLOBAL"))
                        {
                            txtStates.Refresh();
                            System.Threading.Thread.Sleep(10);
                            myAccessIO.UpdateDataTable("select * from " + mySQLTables[i], mydt, true);
                            txtStates.Text = " Export GlobalTable:" + mySQLTables[i] + " data successful...";
                        }
                        else if (isNeedExportTestPlan && mySQLTables[i].ToUpper()== "TopotestPlan".ToUpper() )
                        {                            
                            txtStates.Refresh();
                            System.Threading.Thread.Sleep(10);
                           
                            string sqlCmd = "select * from " + "TopotestPlan" + testPlanQueryCMD; 
                            DataTable pmydt = mySQL.GetDataTable(sqlCmd, mySQLTables[i]);
                            myAccessIO.UpdateDataTable("select * from " + mySQLTables[i], pmydt, true);
                            txtStates.Text = "Export TopoTable:" + mySQLTables[i] + " data successful...";
                        }
                        else if (isNeedExportTestPlan && mySQLTables[i].ToUpper() == "TopoTestControl".ToUpper())
                        {
                            txtStates.Refresh();
                            System.Threading.Thread.Sleep(10);
                           
                            string sqlCmd = "select * from " + "TopoTestControl" + " Where PID in"
                                + "(select id from " + "TopotestPlan" + testPlanQueryCMD + ")"; 

                            DataTable pmydt = mySQL.GetDataTable(sqlCmd, mySQLTables[i]);
                            myAccessIO.UpdateDataTable("select * from " + mySQLTables[i], pmydt, true);
                            txtStates.Text = " Export TopoTable:" + mySQLTables[i] + " data successful...";
                        }
                        else if (isNeedExportTestPlan && mySQLTables[i].ToUpper() == "TopotestModel".ToUpper())
                        {
                            txtStates.Refresh();
                            System.Threading.Thread.Sleep(10);
                            
                            string sqlCmd = "select * from " + "TopotestModel" + " Where PID in"
                                + "(select id from " + "TopoTestControl" + " Where PID in"
                                + "(select id from " + "TopotestPlan" + testPlanQueryCMD + "))";    

                            DataTable pmydt = mySQL.GetDataTable(sqlCmd, mySQLTables[i]);
                            myAccessIO.UpdateDataTable("select * from " + mySQLTables[i], pmydt, true);
                            txtStates.Text = "Export TopoTable:" + mySQLTables[i] + " data successful...";
                        }
                        else if (isNeedExportTestPlan && mySQLTables[i].ToUpper() == "Topotestparameter".ToUpper())
                        {
                            txtStates.Refresh();
                            System.Threading.Thread.Sleep(10);
                            
                            string sqlCmd = "select * from " + "Topotestparameter" + " Where PID in"
                                + "(select id from " + "TopotestModel" + " Where PID in"
                                + "(select id from " + "TopoTestControl" + " Where PID in"
                                + "(select id from " + "TopotestPlan" + testPlanQueryCMD + ")))";   

                            DataTable pmydt = mySQL.GetDataTable(sqlCmd, mySQLTables[i]);
                            myAccessIO.UpdateDataTable("select * from " + mySQLTables[i], pmydt, true);
                            txtStates.Text = "Export TopoTable:" + mySQLTables[i] + " data successful...";
                        }
                        else if (isNeedExportTestPlan && mySQLTables[i].ToUpper() == "TopoEquipment".ToUpper())
                        {
                            txtStates.Refresh();
                            System.Threading.Thread.Sleep(10);
                            
                            string sqlCmd = "select * from " + "TopoEquipment" + " Where PID in"
                                + "(select id from " + "TopotestPlan" + testPlanQueryCMD + ")"; 

                            DataTable pmydt = mySQL.GetDataTable(sqlCmd, mySQLTables[i]);
                            myAccessIO.UpdateDataTable("select * from " + mySQLTables[i], pmydt, true);
                            txtStates.Text = "Export TopoTable:" + mySQLTables[i] + " data successful...";
                        }
                        else if (isNeedExportTestPlan && mySQLTables[i].ToUpper() == "TopoEquipmentParameter".ToUpper())
                        {
                            txtStates.Refresh();
                            System.Threading.Thread.Sleep(10);
                            
                            string sqlCmd = "select * from " + "TopoEquipmentParameter" + " Where PID in"
                                + "(select id from " + "TopoEquipment" + " Where PID in"
                                + "(select id from " + "TopotestPlan" + testPlanQueryCMD + "))";   

                            DataTable pmydt = mySQL.GetDataTable(sqlCmd, mySQLTables[i]);
                            myAccessIO.UpdateDataTable("select * from " + mySQLTables[i], pmydt, true);
                            txtStates.Text = "Export TopoTable:" + mySQLTables[i] + " data successful...";                            
                        }
                    }
                    #endregion
                }
                isExportData = true;
                btnCreatAccessDB.Text = "Finish";
                txtStates.Text = "Creat '" + newFileName + "' Finished ,Path is located in the application directory";
                txtStates.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                btnCreatAccessDB.Text = "Creat Failed";
                txtStates.Text = "Creat '" + newFileName + "' Failed...";
                txtStates.Refresh();                
            }
            finally
            {
                btnCreatAccessDB.Enabled = false;
                btnLogin.Enabled = true;  
                btnChangePwd.Enabled = true;
                startTime = 0;
                myAccessIO.OpenDatabase(false);
                mySQL.OpenDatabase(false);
                myAccessIO = null;
                
                GC.Collect();
                if (timerDate.Enabled == false) 
                {
                    timerDate.Enabled = true;
                }
            }
        }
        
        bool creatTable(string accesspath, string queryCMD, string tableName, bool setIDIncrement)
        {
            OleDbConnection myconn = new OleDbConnection("Provider = Microsoft.ACE.OLEDB.12.0;" + @"Data Source=" + accesspath);
            startTime = 0;  
            try
            {
                if (myconn.State != ConnectionState.Open)
                    myconn.Open();
                OleDbCommand cmd = new OleDbCommand(queryCMD);
                cmd.Connection = myconn;
                cmd.ExecuteNonQuery();
                //"Alter TABLE tablename ADD CONSTRAINT PRIMARY_KEY PRIMARY KEY" CONSTRAINT multifieldindex
                string setPK = "Alter TABLE  " + tableName + " ADD CONSTRAINT  PRIMARY_KEY" + tableName + "  PRIMARY KEY" + " (ID)";
                OleDbCommand setPKcmd = new OleDbCommand(setPK);
                setPKcmd.Connection = myconn;
                setPKcmd.ExecuteNonQuery();

                if (setIDIncrement)
                {
                    //ALTER TABLE [user] ALTER COLUMN [id] COUNTER (1, 1)
                    string strIncrement = "Alter TABLE  " + tableName + " ALTER COLUMN [ID]  COUNTER (1, 1)";
                    OleDbCommand setIncrement = new OleDbCommand(strIncrement);
                    setIncrement.Connection = myconn;
                    setIncrement.ExecuteNonQuery();
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
            finally
            {
                if (myconn != null && myconn.State != ConnectionState.Closed)
                    myconn.Close();
                myconn = null;
            }
        }
        
        private void txtUserID_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter && btnLogin.Visible)
            {
                btnLogin_Click(sender, e);
            }
        }

        private void txtPWD_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter && btnLogin.Visible)
            {
                btnLogin_Click(sender, e);
            }
        }

        private void btnChangePwd_Click(object sender, EventArgs e)
        {
            try
            {
                startTime = 0;
                if (txtNewPWD.Text.Length == 0)
                {
                    MessageBox.Show("The New Password is Null!");
                    txtNewPWD.Focus();
                    return;
                }
                if (txtNewPWD.Text.Length > 0 && txtNewPWD.Text.Length == 0)
                {
                    MessageBox.Show("The confirm Password is Null!");
                    txtconfirmPWD.Focus();
                    return;
                }
                else if (txtNewPWD.Text.ToString() != txtconfirmPWD.Text.ToString())
                {
                    MessageBox.Show("Confirm password is different from the new password!!!");
                    txtconfirmPWD.BackColor = Color.Yellow;
                    txtconfirmPWD.Focus();
                    return;
                }
                else
                {
                    if (blnChangePWDOK(txtUserID.Text.Trim(), txtPWD.Text.Trim(), txtNewPWD.Text.Trim()))
                    {
                        txtPWD.Text = txtNewPWD.Text.Trim();
                        MessageBox.Show("the new password changed OK!\n And the new password has been automatically fill to the password char!!!Pls Login");
                        chkChangePWD.Checked = false;   
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        bool blnChangePWDOK(string UserName, string oldPWD, string newPWD)
        {
            bool result = false;
            try
            {                       
                string pUserID = "";
                string sqlCMD = "select * from UserInfo where LoginName='" + UserName + "'";
                DataTable myPwdInfo = mySQL.GetDataTable(sqlCMD, "myPwdInfo");

                DataRow[] myInfoRows = myPwdInfo.Select("LoginName='" + UserName + "'");
                if (myInfoRows.Length == 1)
                {
                    if (myInfoRows[0]["LoginPassword"].ToString().ToUpper().Trim() == oldPWD.ToUpper().Trim())
                    {
                        pUserID = myInfoRows[0]["ID"].ToString();
                        myInfoRows[0]["LoginPassword"] = newPWD;

                        if (mySQL.UpdateDataTable(sqlCMD, myPwdInfo, false))
                        {
                            result = true;
                        }
                    }
                    else
                    {
                        MessageBox.Show("The current password is  not correct!", "Notice", MessageBoxButtons.OK);
                    }
                }
                else
                {
                    MessageBox.Show("The Login Name is  not correct!!", "Notice", MessageBoxButtons.OK);
                }
                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return result;
            }
        }
        void NewPWDState(bool state)
        {
            try
            {
                txtNewPWD.Visible = state;
                txtconfirmPWD.Visible = state;
                lblConfrimPWD.Visible = state;
                lblNewPWD.Visible = state;
                btnChangePwd.Visible = state;

                if (state)
                {
                    if (btnCreatAccessDB.Visible)
                    {
                        btnChangePwd.Location = new Point(132, 158);
                        btnCreatAccessDB.Location = new Point(25, 158);
                    }
                    else
                    {
                        btnChangePwd.Location = new Point(25, 158);
                    }
                    btnLogin.Location = new Point(232, 158);
                    
                    txtNewPWD.BackColor = Color.LightYellow;
                    txtconfirmPWD.BackColor = Color.LightYellow;
                    
                    grpLogin.Size = new Size(330, 200);
                    txtStates.Location = new Point(12, 218);
                    this.Size = new Size(367, 307);
                    txtNewPWD.Focus();
                }
                else
                {                    
                    btnChangePwd.Visible = false;
                    if (this.btnCreatAccessDB.Visible)
                    {
                        btnCreatAccessDB.Location = new Point(25, 104);
                        btnLogin.Location = new Point(232, 104);
                    }
                    else
                    {
                        btnLogin.Location = new Point(132, 104);
                    }
                    
                    txtNewPWD.BackColor = Color.White;
                    txtconfirmPWD.BackColor = Color.White;
                    grpLogin.Size = new Size(330, 140);
                    txtStates.Location = new Point(12, 160);
                    this.Size = new Size(367, 260);
                    txtPWD.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }       

        private void chkChangePWD_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                startTime = 0;
                if (chkChangePWD.Checked)
                {
                    NewPWDState(true);
                }
                else
                {
                    NewPWDState(false);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }

    public enum LoginAppName
    {
        EEPROMMaintain = 0, EEPROMProgram = 1, MaintainATSPlan = 2, BackGround = 3, QueryLogs = 4
    }

    public enum CheckAccess
    {
        ViewATSPlan = 0, MofifyATSPlan = 1, AddATSPlan = 2,
        DeleteATSPlan = 3, DuplicateATSPlan = 4, ExportATSPlan = 5, ImportATSPlan = 7,
        BackGroundOwner = 7, MofifyEEPROM = 8, ProgramEEPROM = 9, ViewEEPROM = 10, ViewQueryLogs = 11
    }
    public class ValidationRule
    {
        public bool CheckLoginAccess(LoginAppName pAppName, CheckAccess pCheckAccess, long accessCode)
        {
            bool checkResult = false;
            if (pAppName == LoginAppName.BackGround)
            {
                if (pCheckAccess == CheckAccess.BackGroundOwner)
                {
                    checkResult = (accessCode & 0x080) == 0x80 ? true : false;
                }
            }
            else if (pAppName == LoginAppName.MaintainATSPlan)
            {
                if (pCheckAccess == CheckAccess.ViewATSPlan)
                {
                    checkResult = (accessCode & 0x01) == 0x01 ? true : false;
                }
                else if (pCheckAccess == CheckAccess.MofifyATSPlan)
                {
                    checkResult = (accessCode & 0x02) == 0x02 ? true : false;
                }
                else if (pCheckAccess == CheckAccess.AddATSPlan)
                {
                    checkResult = (accessCode & 0x04) == 0x04 ? true : false;
                }
                else if (pCheckAccess == CheckAccess.DeleteATSPlan)
                {
                    checkResult = (accessCode & 0x08) == 0x08 ? true : false;
                }
                else if (pCheckAccess == CheckAccess.DuplicateATSPlan)
                {
                    checkResult = (accessCode & 0x10) == 0x10 ? true : false;
                }
                else if (pCheckAccess == CheckAccess.ExportATSPlan)
                {
                    checkResult = (accessCode & 0x20) == 0x20 ? true : false;
                }
                else if (pCheckAccess == CheckAccess.ImportATSPlan)
                {
                    checkResult = (accessCode & 0x40) == 0x40 ? true : false;
                }
            }
            else if (pAppName == LoginAppName.EEPROMProgram)
            {
                if (pCheckAccess == CheckAccess.ProgramEEPROM)
                {
                    checkResult = (accessCode & 0x200) == 0x200 ? true : false;
                }
            }
            else if (pAppName == LoginAppName.EEPROMMaintain)
            {
                if (pCheckAccess == CheckAccess.MofifyEEPROM)
                {
                    checkResult = (accessCode & 0x100) == 0x100 ? true : false;
                }
                else if (pCheckAccess == CheckAccess.ViewEEPROM)
                {
                    checkResult = (accessCode & 0x400) == 0x400 ? true : false;
                }
            }
            else if (pAppName == LoginAppName.QueryLogs)
            {
                if (pCheckAccess == CheckAccess.ViewQueryLogs)
                {
                    checkResult = (accessCode & 0x800) == 0x800 ? true : false;
                }
            }
            return checkResult;
        }
    }
    //Export PlanNames
    public class PlanNameForm : Form
    {
        LoginInfoStruct myLoginInfoStruct;
        #region partial PlanNameForm
        public bool blnCancelNewPlan = false;
        public bool blnAllPlans = false;
        DataIO mySQL;
        public string[,] ArrayPNPlan;
        public PlanNameForm(LoginInfoStruct pLoginInfoStruct)
        {
            myLoginInfoStruct = pLoginInfoStruct;
            mySQL = new SqlManager(myLoginInfoStruct.ServerName, myLoginInfoStruct.DBName, myLoginInfoStruct.DBUser, myLoginInfoStruct.DBPassword);   
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkAllPlan.Checked == false)    //140710_0
                {
                    //if (cboPlanName.Text.ToString().Trim().Length == 0)
                    int listCount = listPNPlan.Items.Count;
                    blnAllPlans = false;
                    if (listCount == 0)
                    {
                        MessageBox.Show("Please choose at least one testplan! ");
                        return;
                    }
                    else
                    {
                        ArrayPNPlan = new string[listCount, 2];
                        for (int k = 0; k < listCount; k++)
                        {
                            string ss = listPNPlan.Items[k].ToString();
                            int myindex = ss.IndexOf(":");
                            ArrayPNPlan[k, 0] = ss.Substring(0, myindex);
                            ArrayPNPlan[k, 1] = ss.Substring(myindex + 1, ss.Length - (myindex + 1));
                        }
                    }
                }
                else
                {
                    blnAllPlans = true;
                }
                this.Close();
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

        private void PlanNameForm_Load(object sender, EventArgs e)
        {
            try
            {
                cboPlanName.Items.Clear();
                cboPlanName.Text = "";
                cboPlanName.Items.Add("");

                cboPN.Items.Clear();
                cboPN.Text = "";
                cboPN.Items.Add("");
                DataTable pPNdt = new DataTable();
                pPNdt = mySQL.GetDataTable("Select * from GlobalProductionName", "GlobalProductionName");
                for (int i = 0; i < pPNdt.Rows.Count; i++)
                {
                    cboPN.Items.Add(pPNdt.Rows[i]["PN"].ToString());
                }
                //if (pPNdt.Rows.Count > 0)
                //{
                //    cboPN.SelectedItem = cboPN.Items[0];
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void cboPN_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cboPlanName.Items.Clear();
                DataTable pPlandt = new DataTable();
                pPlandt = mySQL.GetDataTable("Select * from TopoTestPlan where pid in "
                    + "(select id from GlobalProductionName where PN='" + cboPN.Text + "')", "topotestplan");  // ", "TopoTestPlan"); //        
                for (int i = 0; i < pPlandt.Rows.Count; i++)
                {
                    cboPlanName.Items.Add(pPlandt.Rows[i]["ItemName"].ToString());
                }
                cboPlanName.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void chkAllPlan_CheckedChanged(object sender, EventArgs e)
        {
            btnState(!chkAllPlan.Checked);
        }

        void btnState(bool state)
        {
            try
            {
                listPNPlan.Enabled = state;
                btnAdd.Enabled = state;
                btnRemove.Enabled = state;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            setbtnRemvoeState(false);
            string myPNPlan = "";

            if (cboPN.Text.Trim().Length > 0 && cboPlanName.Text.Trim().Length > 0)
            {
                myPNPlan = cboPN.Text.Trim().ToUpper() + ":" + cboPlanName.Text.Trim().ToUpper();
                for (int i = 0; i < listPNPlan.Items.Count; i++)
                {
                    if (myPNPlan.ToUpper().Trim() == listPNPlan.Items[i].ToString().ToUpper().Trim())
                    {
                        MessageBox.Show("Error,This testplan already exists:-> " + myPNPlan);
                        return;
                    }
                }
                listPNPlan.Items.Add(myPNPlan);
            }
        }

        private void listPNPlan_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (listPNPlan.SelectedIndex != -1)
                {
                    setbtnRemvoeState(true);
                }
                else
                {
                    setbtnRemvoeState(false);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void setbtnRemvoeState(bool state)
        {
            btnRemove.Enabled = state;
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                if (listPNPlan.SelectedIndex != -1)
                {
                    listPNPlan.Items.RemoveAt(listPNPlan.SelectedIndex);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        #endregion


        #region Designer
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.ItemName = new System.Windows.Forms.Label();
            this.cboPN = new System.Windows.Forms.ComboBox();
            this.lblPN = new System.Windows.Forms.Label();
            this.cboPlanName = new System.Windows.Forms.ComboBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.listPNPlan = new System.Windows.Forms.ListBox();
            this.chkAllPlan = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(269, 60);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(81, 26);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(269, 128);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(81, 26);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // ItemName
            // 
            this.ItemName.AutoSize = true;
            this.ItemName.Location = new System.Drawing.Point(27, 42);
            this.ItemName.Name = "ItemName";
            this.ItemName.Size = new System.Drawing.Size(77, 12);
            this.ItemName.TabIndex = 2;
            this.ItemName.Text = "TestPlanName";
            // 
            // cboPN
            // 
            this.cboPN.FormattingEnabled = true;
            this.cboPN.Location = new System.Drawing.Point(122, 6);
            this.cboPN.Name = "cboPN";
            this.cboPN.Size = new System.Drawing.Size(121, 20);
            this.cboPN.TabIndex = 3;
            this.cboPN.SelectedIndexChanged += new System.EventHandler(this.cboPN_SelectedIndexChanged);
            // 
            // lblPN
            // 
            this.lblPN.AutoSize = true;
            this.lblPN.Location = new System.Drawing.Point(27, 9);
            this.lblPN.Name = "lblPN";
            this.lblPN.Size = new System.Drawing.Size(89, 12);
            this.lblPN.TabIndex = 4;
            this.lblPN.Text = "ProductionName";
            // 
            // cboPlanName
            // 
            this.cboPlanName.FormattingEnabled = true;
            this.cboPlanName.Location = new System.Drawing.Point(122, 39);
            this.cboPlanName.Name = "cboPlanName";
            this.cboPlanName.Size = new System.Drawing.Size(121, 20);
            this.cboPlanName.TabIndex = 5;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(101, 69);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(65, 26);
            this.btnAdd.TabIndex = 6;
            this.btnAdd.Text = "Add>>";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(172, 69);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(69, 26);
            this.btnRemove.TabIndex = 7;
            this.btnRemove.Text = "<<Delete";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // listPNPlan
            // 
            this.listPNPlan.FormattingEnabled = true;
            this.listPNPlan.ItemHeight = 12;
            this.listPNPlan.Location = new System.Drawing.Point(29, 101);
            this.listPNPlan.Name = "listPNPlan";
            this.listPNPlan.Size = new System.Drawing.Size(212, 220);
            this.listPNPlan.TabIndex = 8;
            this.listPNPlan.SelectedIndexChanged += new System.EventHandler(this.listPNPlan_SelectedIndexChanged);
            // 
            // chkAllPlan
            // 
            this.chkAllPlan.AutoSize = true;
            this.chkAllPlan.Location = new System.Drawing.Point(29, 75);
            this.chkAllPlan.Name = "chkAllPlan";
            this.chkAllPlan.Size = new System.Drawing.Size(66, 16);
            this.chkAllPlan.TabIndex = 9;
            this.chkAllPlan.Text = "CopyAll";
            this.chkAllPlan.UseVisualStyleBackColor = true;
            this.chkAllPlan.CheckedChanged += new System.EventHandler(this.chkAllPlan_CheckedChanged);
            // 
            // PlanNameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(396, 332);
            this.Controls.Add(this.chkAllPlan);
            this.Controls.Add(this.listPNPlan);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.cboPlanName);
            this.Controls.Add(this.lblPN);
            this.Controls.Add(this.cboPN);
            this.Controls.Add(this.ItemName);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.MaximumSize = new System.Drawing.Size(412, 370);
            this.Name = "PlanNameForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PlanName";
            this.Load += new System.EventHandler(this.PlanNameForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label ItemName;
        private System.Windows.Forms.Label lblPN;
        public System.Windows.Forms.ComboBox cboPN;
        public System.Windows.Forms.ComboBox cboPlanName;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.ListBox listPNPlan;
        private System.Windows.Forms.CheckBox chkAllPlan;
        #endregion
    }
}

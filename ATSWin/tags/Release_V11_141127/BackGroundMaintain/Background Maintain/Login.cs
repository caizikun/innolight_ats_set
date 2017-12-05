using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using ATSDataBase;
using ADOX;//140612
using System.Data.OleDb;
using TestPlan;

namespace GlobalInfo
{
    public partial class Login : Form
    {
        ConfigXmlIO myConfigXmlIO;              //140718_1
        public static string ServerName = "";   //140725_1
        public static string DBName = "";       //140911_0
        public static string DBUser = "";     //140917_2
        public static string DBPassword = "";      //140917_2

        public static string UserName = "";
        public static string AccessFilePath = "";
        public static bool blnISDBSQLserver = false;    
        public static bool blnOnlyToReadFlag = false;
        DataIO mySQL;                       //140718_1 = new SqlManager();
        DataTable myInfo;
        ToolTip mytip = new ToolTip();
        string myLoginID = "";  //141112_1
        //----------------------------
        //140612
        private string[] ConstTestPlanTables = new string[] { "TopoTestPlan", "TopoTestControl", "TopoTestModel", "TopoTestParameter", "TopoEquipment", "TopoEquipmentParameter" };
        private string[] ConstGlobalListTables = new string[] { "GlobalAllEquipmentList", "GlobalAllEquipmentParamterList", "GlobalAllAppModelList", "GlobalAllTestModelList", "GlobalTestModelParamterList" };
        private string[] ConstMSAItemTables = new string[] { "GlobalProductionType", "GlobalMSADefintionInf", "GlobalMSADTable" };
        private string[] ConstGlobalPNInfo = new string[] { "GlobalProductionType", "GlobalProductionName", "GlobalManufactureCoefficientsGroup", "GlobalManufactureCoefficients" };

        ADOX.CatalogClass MyAccess = new CatalogClass();
        DataIO myAccessIO;
        string[] mySQLTables;
        string testPlanQueryCMD = "";
        //----------------------------
        int loginTimes = 0;

        public Login()
        {
            InitializeComponent();
        }

        public static int startTime = 0;

        private void btnLogin_Click(object sender, EventArgs e)
        {
            startTime = 0;
            try
            {
                btnLogin.Enabled = false;
                if (this.txtPWD.Text.Trim().Length == 0 || this.txtUserID.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Please enter a Login Name and enter a Password!", "Login Name or Password is Null", MessageBoxButtons.OK);
                }
                else
                {
                    if (this.chkSQLlib.Checked == false)
                    {
                        blnISDBSQLserver = false;
                        OpenFileDialog openFileDialog = new OpenFileDialog();
                        openFileDialog.InitialDirectory = Application.StartupPath;//注意这里写路径时要用c:\\而不是c:\
                        //openFileDialog.Filter = "文本文件|*.*|C#文件|*.cs|所有文件|*.*";
                        openFileDialog.Filter = "Access文件|*.mdb|Access文件|*.accdb|所有文件|*.*";
                        openFileDialog.RestoreDirectory = true;
                        openFileDialog.FilterIndex = 2;
                        DialogResult blnISselected = openFileDialog.ShowDialog();
                        if (openFileDialog.FileName.Length != 0 && blnISselected == DialogResult.OK)
                        {
                            AccessFilePath = openFileDialog.FileName.Trim();
                            showAccess();
                        }
                        else
                        {
                            MessageBox.Show("Pls choose a path of the Local Accdb Path!", "The path of Accdb file is not Found!", MessageBoxButtons.OK);
                        }
                    }
                    else
                    {
                        blnISDBSQLserver = true;
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
                UserName = this.txtUserID.Text;
                bool PWDOKFlag = false;
                bool blnDBOwner = false;
                string pUserID = "";
                if (this.chkSQLlib.CheckState == CheckState.Checked)
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
                        string queryCMD = "select * from functiontable where id in "
                            + "(select RoleFunctionTable.FunctionID from RoleFunctionTable where RoleFunctionTable.RoleID  in "
                            + "(select roleID from UserRoleTable where UserRoleTable.UserID = " + pUserID + "))";
                        //+ pUserID + "))";--> ToString+ "(select id from UserInfo	where UserInfo.LoginName='" + this.txtUserID.Text.ToString() + "')))";
                        DataTable myFunctions = mySQL.GetDataTable(queryCMD, "UserRoleFuncTable");

                        for (int i = 0; i < myFunctions.Rows.Count; i++)
                        {
                            if (myFunctions.Rows[i]["Title"].ToString().ToUpper() == "DBOwner".ToUpper()) //140709_0 ->DBOwner
                            {
                                blnDBOwner = true;
                                break;
                            }
                        }

                        if (blnDBOwner != true)
                        {
                            MessageBox.Show("Access denied!This user is not a administrator!");
                            return;
                        }
                        else
                        {
                            updateStutes(true, myInfo);                            
                            myLoginID = updateUserLoginInfo();  //141112_1
                        }
                    }
                }
                mySQL.OpenDatabase(false);
                
                this.txtPWD.Text = "";
                this.Hide();
                MainForm myPNInfo = new MainForm();
                myPNInfo.myLoginID = myLoginID;                
                myPNInfo.ShowDialog();
                loginTimes = 0;
                System.Threading.Thread.Sleep(100);
                this.Show();    //1411106_1
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }
        }
        string updateUserLoginInfo()
        {
            string myID = "";
            try
            {
                DataTable userLoginInfoDt = mySQL.GetDataTable("select * from UserLoginInfo", "UserLoginInfo");
                DataRow dr = userLoginInfoDt.NewRow();
                string hostname = System.Net.Dns.GetHostName(); //主机
                System.Net.IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(hostname);//网卡IP地址集合
                string IP6 = ipEntry.AddressList[0].ToString();//取一个IP
                string IP4 = ipEntry.AddressList[ipEntry.AddressList.Length - 1].ToString();//取一个IP
                string currTime = mySQL.GetCurrTime().ToString();
                dr["UserName"] = Login.UserName;
                dr["LoginOntime"] = currTime;
                dr["LoginOfftime"] = "2000-1-1 12:00:00";
                dr["Apptype"] = Application.ProductName;
                dr["LoginInfo"] = "Login Name =" + Login.UserName + " login successfully at computer=" + hostname + "[" + IP4 + "]" + ";";
                dr["OPLogs"] = "";
                userLoginInfoDt.Rows.Add(dr);
                mySQL.UpdateDataTable("select * from UserLoginInfo", userLoginInfoDt);
                myID = mySQL.GetDataTable("select * from UserLoginInfo where LoginOntime='" + currTime + "' and UserName ='" + Login.UserName + "'", "UserLoginInfo").Rows[0]["ID"].ToString();

                return myID;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return mySQL.GetLastInsertData("UserLoginInfo").ToString();
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
                        if (charCode > 126 | charCode < 32)
                        {
                            MessageBox.Show("当前的密码字符串中存在汉字,可能导致解密失败,请重新输入密码!!!");
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

        #region 141031_00 取消检查登陆状态...
        void getLoginStutes()   //140911_0
        {
            try
            {                
                string lastLoginInfo =
                    "select spid,dbid,loginame,hostname,login_time,last_batch,status,program_name,hostprocess from master..sysprocesses " +
                    "where   dbid=db_id('" + Login.DBName + "') and loginame ='" + SetPWDCode(Login.DBUser,true,4) + "'";   //141031_00
                //string lastLoginInfo = "SELECT * FROM sys.dm_exec_sessions WHERE login_name ='background' order by login_time";
                DataTable myLoginInfo = mySQL.GetDataTable(lastLoginInfo, "myLoginInfo");
                if (myLoginInfo.Rows.Count > 1)// 已有用户登入...不允许编辑信息!
                {                    
                    string loginTime = myLoginInfo.Rows[0]["login_time"].ToString();
                    //string last_request_start_time = myLoginInfo.Rows[0]["last_request_start_time"].ToString();
                    string last_request_start_time = myLoginInfo.Rows[0]["last_batch"].ToString();
                    string lastComputerName = myLoginInfo.Rows[0]["host_Name"].ToString(); //= System.Environment.MachineName.ToString()

                    if (lastComputerName.ToUpper().Trim() != System.Environment.MachineName.ToString().ToUpper().Trim())
                    {
                        //MessageBox.Show("当前已经存在用户登入,请知悉!-->"
                        //    + "\n 登入电脑名称: " + lastComputerName
                        //    + "\n 登入时间: " + loginTime
                        //    + "\n 最后一次处理时间: " + last_request_start_time
                        //    + "\n 由于多个用户可能导致资料紊乱,故限制此次登入只能读取,不能进行更新资料到数据库! "
                        //    + "\n 如需要使用请联系已经登入的使用者!!!或稍后再登入!!!"
                        //    , "注意!"
                        //    , MessageBoxButtons.OK
                        //    , MessageBoxIcon.Information
                        //     );
                        blnOnlyToReadFlag = true;
                    }
                    else
                    {
                        blnOnlyToReadFlag = false;
                    }
                }
                else
                {
                    blnOnlyToReadFlag = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show( "Connecting Error!\n " +ex.ToString() + "!");
                blnOnlyToReadFlag = true;
            }
        }
        #endregion

        private void Login_Load(object sender, EventArgs e)
        {
            chkSQLlib.Checked = true;   //140922_0
            chkSQLlib.Visible = false;  //140922_0 默认为隐藏!

            grpLogin.Enabled = false;
            grpDBSel.Enabled = true;
            myConfigXmlIO = new ConfigXmlIO(Application.StartupPath + @"\Config.xml"); //140704_1
            ServerName = myConfigXmlIO.ServerName; //140725_1            
            rdoATSHome.Checked = false;     //140911_1
            rdoEDVTHome.Checked = false;    //140911_1
            //mySQL = new SqlManager(Login.ServerName, Login.DBName, "BackGround", "2014#07");    //140911_0
            timerDate.Enabled = false; //默认为False,当现实时自动回切换为True;
            startTime = 1;
            mytip.ShowAlways = true;
        }
        void updateStutes(bool value, DataTable mydt) //140604_2
        {
            try
            {
                DataRow[] myRows = mydt.Select();
                if (myRows.Length == 1)
                {                    
                    string hostname = System.Net.Dns.GetHostName(); //主机
                    System.Net.IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(hostname);//网卡IP地址集合
                    string IP6 = ipEntry.AddressList[0].ToString();//取一个IP
                    string IP4 = ipEntry.AddressList[ipEntry.AddressList.Length-1].ToString();//取一个IP //140630_0
                    myRows[0].BeginEdit();
                    //myRows[0]["ISLogin"] = value;
                    myRows[0]["lastLoginONTime"] = DateTime.Now.ToString();     //140605_0
                    myRows[0]["lastComputerName"] = System.Environment.MachineName.ToString();
                    myRows[0]["lastIP"] = IP4;
                    myRows[0].EndEdit();

                    mySQL.UpdateDataTable("select * from UserInfo where LoginName='" + this.txtUserID.Text.ToString() + "'", mydt, false);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
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
            if (timerDate.Enabled) //140922_1
            {
                timerDate.Enabled = false;
            }
            btnLogin.Enabled = false;
            btnCreatAccessDB.Enabled = false;
            txtStates.Text = "";
            txtStates.Visible = true;
            string dbName = Application.StartupPath + @"\SQL.accdb";           
            try
            {
                

                if (System.IO.File.Exists(dbName))
                {
                    System.IO.File.Move(dbName, Application.StartupPath + @"\" + DateTime.Now.ToString("yyMMdd_HHmmss") + @"SQL.accdb");
                    MessageBox.Show("The path " + dbName + " has been found 'SQL.accdb'  !\n Now will make the original file renamed as "
                        + DateTime.Now.ToString("yyMMdd_HHmmss") + @"SQL.accdb" + " \n");
                    //System.IO.File.Delete(dbName);
                    System.Threading.Thread.Sleep(10);
                    txtStates.Text = "System is to create the new 'SQL.accdb', please wait...";
                    txtStates.Refresh();
                }

                System.Threading.Thread.Sleep(50);
                //MyAccess.Create("Provider =Microsoft.Jet.OLEDB.4.0;Data Source =" + dbName + ";"); //140709_1
                MyAccess.Create("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + dbName );
                txtStates.Text = " System is to create the new 'SQL.accdb' OK!\r\n Next to get data of the tables form SQL Server!";
                txtStates.Refresh();

                DialogResult mySelect = MessageBox.Show("Please choose whether to import designated models specified testplan ?",
                        "Import TopoTable Data?", 
                        MessageBoxButtons.YesNo, 
                        MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button2);
                bool isNeedExportTestPlan = false;
                //string pPN = "";
                //string pPlanName = "";
                if (mySelect == DialogResult.Yes)
                {
                    PlanNameForm myPlan = new PlanNameForm();

                    myPlan.ShowDialog();
                    //if (myPlan.cboPlanName.Text.Trim().Length > 0)    //140710_0
                    //{
                    //    isNeedExportTestPlan = true;
                    //    pPN = myPlan.cboPN.Text;
                    //    pPlanName = myPlan.cboPlanName.Text;
                    //}
                    if (myPlan.blnAllPlans == true
                        || myPlan.ArrayPNPlan.Length > 0)   //140710_0
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

                AccessFilePath = dbName;

                myAccessIO = new AccessManager(dbName);

                //140922_0--------------------
                //string pserverName = ServerName;    //140722
                //string pdbName = Login.DBName;
                //string puser = "";
                //const string ppwd = "";

                mySQLTables = mySQL.GetCurrTablesName(Login.ServerName, Login.DBName, Login.DBUser, Login.DBPassword);
                //140922_0 -------------------

                for (int i = 0; i < mySQLTables.Length; i++)
                {
                    //先创建表//

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

                        for (int m = 0; m < Count; m++)
                        {
                            if (mydt.Columns[m].ColumnName.ToUpper() == "ItemValue")
                            {
                                mydt.Columns[m].ColumnName = "MyValue";
                            }
                        }
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

                            //特别处理 TopoLogRecord
                            if (mySQLTables[i].ToString().ToUpper() == "TopoLogRecord".ToUpper())
                            {
                                if (mydt.Columns[j].ColumnName.ToString().ToUpper() == "TestLog".ToUpper())
                                {
                                    ColumnsType[j] = "Memo";
                                    ColumnsSize[j] = "";
                                }
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

                        Sql = "CREATE TABLE " + mySQLTables[i] + "(" + ColumnInfo;
                        //Sql = "CREATE TABLE GlobalAllAppModelList(ID text(5),Name text(255),Description text (255)  )";
                        if (mySQLTables[i].ToUpper() == "TopoRunRecordTable".ToUpper()
                            || mySQLTables[i].ToUpper() == "TopoLogRecord".ToUpper()
                            || mySQLTables[i].ToUpper() == "TopoTestData".ToUpper()
                            )   //140707_0 对于存档的资料部分需要设置ID自动递增
                        {
                            if (creatTable(AccessFilePath, Sql, mySQLTables[i], true) == false)
                            {
                                MessageBox.Show("Creat table Error :" + AccessFilePath + "-->" + mySQLTables[i]);
                                txtStates.Text = "Creat table Error :" + AccessFilePath + "-->" + mySQLTables[i];
                            }
                        }
                        else
                        {
                            if (creatTable(AccessFilePath, Sql, mySQLTables[i], false) == false)
                            {
                                MessageBox.Show("Creat table Error :" + AccessFilePath + "-->" + mySQLTables[i]);
                                txtStates.Text = "Creat table Error :" + AccessFilePath + "-->" + mySQLTables[i];
                            }
                            else
                            {
                                txtStates.Text = "Creat table Finish :" + AccessFilePath + "-->" + mySQLTables[i];
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
                            //string sqlCmd= "select * from " + "TopotestPlan" + " Where itemName='" + pPlanName + "' and PID = "
                            //    + "(Select id from " +  "GlobalProductionName Where PN='" +pPN + "')";
                            string sqlCmd = "select * from " + "TopotestPlan" + testPlanQueryCMD; //140710_0
                            DataTable pmydt = mySQL.GetDataTable(sqlCmd, mySQLTables[i]);
                            myAccessIO.UpdateDataTable("select * from " + mySQLTables[i], pmydt, true);
                            txtStates.Text = "Export TopoTable:" + mySQLTables[i] + " data successful...";
                        }
                        else if (isNeedExportTestPlan && mySQLTables[i].ToUpper() == "TopoTestControl".ToUpper())
                        {
                            txtStates.Refresh();
                            System.Threading.Thread.Sleep(10);
                            //string sqlCmd = "select * from " + "TopoTestControl" + " Where PID in" 
                            //    + "(select id from " + "TopotestPlan" + " Where itemName='" + pPlanName + "' and PID = "
                            //    + "(Select id from " + "GlobalProductionName Where PN='" + pPN + "'))";
                            string sqlCmd = "select * from " + "TopoTestControl" + " Where PID in"
                                + "(select id from " + "TopotestPlan" + testPlanQueryCMD + ")"; //140710_0

                            DataTable pmydt = mySQL.GetDataTable(sqlCmd, mySQLTables[i]);
                            myAccessIO.UpdateDataTable("select * from " + mySQLTables[i], pmydt, true);
                            txtStates.Text = " Export TopoTable:" + mySQLTables[i] + " data successful...";
                        }
                        else if (isNeedExportTestPlan && mySQLTables[i].ToUpper() == "TopotestModel".ToUpper())
                        {
                            txtStates.Refresh();
                            System.Threading.Thread.Sleep(10);
                            //string sqlCmd =  "select * from " + "TopotestModel" + " Where PID in" 
                            //    + "(select id from " + "TopoTestControl" + " Where PID in"
                            //    + "(select id from " + "TopotestPlan" + " Where itemName='" + pPlanName + "' and PID = "
                            //    + "(Select id from " + "GlobalProductionName Where PN='" + pPN + "')))";
                            string sqlCmd = "select * from " + "TopotestModel" + " Where PID in"
                                + "(select id from " + "TopoTestControl" + " Where PID in"
                                + "(select id from " + "TopotestPlan" + testPlanQueryCMD + "))";    //140710_0

                            DataTable pmydt = mySQL.GetDataTable(sqlCmd, mySQLTables[i]);
                            myAccessIO.UpdateDataTable("select * from " + mySQLTables[i], pmydt, true);
                            txtStates.Text = "Export TopoTable:" + mySQLTables[i] + " data successful...";
                        }
                        else if (isNeedExportTestPlan && mySQLTables[i].ToUpper() == "Topotestparameter".ToUpper())
                        {
                            txtStates.Refresh();
                            System.Threading.Thread.Sleep(10);
                            //string sqlCmd = "select * from " + "Topotestparameter" + " Where PID in"
                            //    + "(select id from " + "TopotestModel" + " Where PID in"
                            //    + "(select id from " + "TopoTestControl" + " Where PID in"
                            //    + "(select id from " + "TopotestPlan" + " Where itemName='" + pPlanName + "' and PID = "
                            //    + "(Select id from " + "GlobalProductionName Where PN='" + pPN + "'))))";
                            string sqlCmd = "select * from " + "Topotestparameter" + " Where PID in"
                                + "(select id from " + "TopotestModel" + " Where PID in"
                                + "(select id from " + "TopoTestControl" + " Where PID in"
                                + "(select id from " + "TopotestPlan" + testPlanQueryCMD + ")))";   //140710_0

                            DataTable pmydt = mySQL.GetDataTable(sqlCmd, mySQLTables[i]);
                            myAccessIO.UpdateDataTable("select * from " + mySQLTables[i], pmydt, true);
                            txtStates.Text = "Export TopoTable:" + mySQLTables[i] + " data successful...";
                        }
                        else if (isNeedExportTestPlan && mySQLTables[i].ToUpper() == "TopoEquipment".ToUpper())
                        {
                            txtStates.Refresh();
                            System.Threading.Thread.Sleep(10);
                            //string sqlCmd = "select * from " + "TopoEquipment" + " Where PID in"
                            //    + "(select id from " + "TopotestPlan" + " Where itemName='" + pPlanName + "' and PID = "
                            //    + "(Select id from " + "GlobalProductionName Where PN='" + pPN + "'))";
                            string sqlCmd = "select * from " + "TopoEquipment" + " Where PID in"
                                + "(select id from " + "TopotestPlan" + testPlanQueryCMD + ")"; //140710_0

                            DataTable pmydt = mySQL.GetDataTable(sqlCmd, mySQLTables[i]);
                            myAccessIO.UpdateDataTable("select * from " + mySQLTables[i], pmydt, true);
                            txtStates.Text = "Export TopoTable:" + mySQLTables[i] + " data successful...";
                        }
                        else if (isNeedExportTestPlan && mySQLTables[i].ToUpper() == "TopoEquipmentParameter".ToUpper())
                        {
                            txtStates.Refresh();
                            System.Threading.Thread.Sleep(10);
                            //string sqlCmd = "select * from " + "TopoEquipmentParameter" + " Where PID in"
                            //    + "(select id from " + "TopoEquipment" + " Where PID in"
                            //    + "(select id from " + "TopotestPlan" + " Where itemName='" + pPlanName + "' and PID = "
                            //    + "(Select id from " + "GlobalProductionName Where PN='" + pPN + "')))";
                            string sqlCmd = "select * from " + "TopoEquipmentParameter" + " Where PID in"
                                + "(select id from " + "TopoEquipment" + " Where PID in"
                                + "(select id from " + "TopotestPlan" + testPlanQueryCMD + "))";   //140710_0

                            DataTable pmydt = mySQL.GetDataTable(sqlCmd, mySQLTables[i]);
                            myAccessIO.UpdateDataTable("select * from " + mySQLTables[i], pmydt, true);
                            txtStates.Text = "Export TopoTable:" + mySQLTables[i] + " data successful...";                            
                        }
                    }                    
                }
                btnCreatAccessDB.Text = "Creat Finish";
                txtStates.Text = "Creat 'SQL.accdb' Finish ,Path is located in the application directory";
                txtStates.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                btnCreatAccessDB.Text = "Creat Failed";
                txtStates.Text = "Creat 'SQL.accdb' Failed...";
                txtStates.Refresh();                
            }
            finally
            {
                btnCreatAccessDB.Enabled = false;
                btnLogin.Enabled = true;                
                startTime = 0;
                myAccessIO.OpenDatabase(false);
                mySQL.OpenDatabase(false);
                myAccessIO = null;
                MyAccess = null;
                GC.Collect();
                if (timerDate.Enabled == false) //140922_1
                {
                    timerDate.Enabled = true;
                }
            }
        }
        #region creatTable(string accesspath, string queryCMD, string tableName)
        //bool creatTable(string accesspath, string queryCMD, string tableName)
        //{
        //    OleDbConnection myconn = new OleDbConnection("Provider = Microsoft.ACE.OLEDB.12.0;" + @"Data Source=" + accesspath);

        //    try
        //    {
        //        if (myconn.State != ConnectionState.Open)
        //            myconn.Open();
        //        OleDbCommand cmd = new OleDbCommand(queryCMD);
        //        cmd.Connection = myconn;
        //        cmd.ExecuteNonQuery();
        //        //"Alter TABLE tablename ADD CONSTRAINT PRIMARY_KEY PRIMARY KEY" CONSTRAINT multifieldindex
        //        string setPK = "Alter TABLE  " + tableName + " ADD CONSTRAINT  PRIMARY_KEY" + tableName + "  PRIMARY KEY" + " (ID)";
        //        OleDbCommand setPKcmd = new OleDbCommand(setPK);
        //        setPKcmd.Connection = myconn;
        //        setPKcmd.ExecuteNonQuery();

        //        //ALTER TABLE [user] ALTER COLUMN [id] COUNTER (1, 1)
        //        //string strIncrement = "Alter TABLE  " + tableName + " ALTER COLUMN [ID]  COUNTER (1, 1)";
        //        //OleDbCommand setIncrement = new OleDbCommand(strIncrement);
        //        //setIncrement.Connection = myconn;
        //        //setIncrement.ExecuteNonQuery();
                
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //        return false;
        //    }
        //    finally
        //    {
        //        if (myconn != null && myconn.State != ConnectionState.Closed)
        //            myconn.Close();
        //        myconn = null;
        //    }
        //}
        #endregion

        bool creatTable(string accesspath, string queryCMD, string tableName, bool setIDIncrement)
        {
            OleDbConnection myconn = new OleDbConnection("Provider = Microsoft.ACE.OLEDB.12.0;" + @"Data Source=" + accesspath);
            startTime = 0;  //140922_0
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

        private void rdoATSHome_CheckedChanged(object sender, EventArgs e)  //140911_0
        {
            try
            {
                mySQL = null;
                startTime = 0;  //140922_0
                if (rdoATSHome.Checked)
                {
                    Login.DBName = myConfigXmlIO.ATSDBName;
                    Login.DBUser = myConfigXmlIO.ATSUser;       //140918_0
                    Login.DBPassword = myConfigXmlIO.ATSPWD;    //140918_0
                    if (Login.DBName == null)
                    {
                        Login.DBName = "ATSHome";                       
                        myConfigXmlIO.ATSDBName = Login.DBName;
                    }
                    
                    mySQL = new SqlManager(Login.ServerName, Login.DBName, Login.DBUser, Login.DBPassword);   //140917_2
                    this.Text = "Login(DataSource = " + Login.DBName + ")";
                    //grpDBSel.Text = "Pls select the a DataSource first";
                    grpLogin.Enabled = true;
                    txtPWD.Focus();
                    txtPWD.SelectionStart = txtPWD.TextLength;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void rdoEDVTHome_CheckedChanged(object sender, EventArgs e) //140911_0
        {
            try
            {
                mySQL = null;
                startTime = 0;  //140922_0
                if (rdoEDVTHome.Checked)
                {
                    Login.DBName = myConfigXmlIO.EDVTDBName;
                    Login.DBUser = myConfigXmlIO.EDVTUser;      //140918_0
                    Login.DBPassword = myConfigXmlIO.EDVTPWD;   //140918_0
                    if (Login.DBName == null)
                    {
                        Login.DBName = "EDVTHome";
                        myConfigXmlIO.EDVTDBName = Login.DBName;
                    }

                    mySQL = new SqlManager(Login.ServerName, Login.DBName, Login.DBUser, Login.DBPassword);   //140917_2
                    this.Text = "Login(datasource = " + Login.DBName + ")";
                    //grpDBSel.Text = "Pls select the a DataSource first";
                    grpLogin.Enabled = true;
                    txtPWD.Focus();
                    txtPWD.SelectionStart = txtPWD.TextLength;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void chkSQLlib_CheckedChanged(object sender, EventArgs e)
        {
            startTime = 0;  //140922_0
        }

        private void Login_KeyPress(object sender, KeyPressEventArgs e) //140922_0 按?号 可启用本地数据库!
        {
            if (
                 e.KeyChar == (char)'?')
            {
                MessageBox.Show("You can choose the datasource of the Local database now !");
                this.chkSQLlib.Visible = true;
            }
        }

        private void txtUserID_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter && grpLogin.Enabled)
            {
                btnLogin_Click(sender, e);
            }
        }

        private void txtPWD_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter && grpLogin.Enabled)
            {
                btnLogin_Click(sender, e);
            }
        }
    }
}

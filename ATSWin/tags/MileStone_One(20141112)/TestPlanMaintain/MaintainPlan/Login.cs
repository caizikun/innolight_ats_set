using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using ATSDataBase;

namespace TestPlan
{
    public partial class Login : Form
    {
        public static string UserName = "";
        public static string AccessFilePath = "";
        public static  bool  blnISDBSQLserver=false ;
        public static bool blnOnlyToReadFlag = false;
        DataIO mySQL ;  
        DataTable myInfo;
        ToolTip mytip = new ToolTip();
        ConfigXmlIO myConfigXmlIO;

        public static string ServerName = "";
        public static string DBName = "";   
        public static string DBUser = "";     
        public static string DBPassword = "";      

        string myLoginID = "";  //141022_0

        public Login()
        {
            InitializeComponent();
        }

        public static int startTime = 0;

        private void btnLogin_Click(object sender, EventArgs e)
        {
            startTime = 1;
            try
            {   
                btnLogin.Enabled = true;
                if (this.txtPWD.Text.Trim().Length == 0 || this.txtUserID.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Please enter a Login Name and enter a Password!", "Login Name or Password is Null", MessageBoxButtons.OK);
                }
                else
                {
                   
                    if (blnISDBSQLserver==false)
                    {
                        
                        OpenFileDialog openFileDialog = new OpenFileDialog();
                        openFileDialog.InitialDirectory = Application.StartupPath;  //注意这里写路径时要用c:\\而不是c:\
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

        void NewPWDState(bool state)
        {
            try
            {
                txtNewPWD.Visible = state;
                txtconfirmPWD.Visible = state;
                lblconfirmPWD.Visible = state;
                lblNewPWD.Visible = state;
                btnChangePWD.Visible = state;

                if (state)
                {
                    btnLogin.Size = new Size(143, 28);
                    lblUserID.Location = new Point(4, 37);
                    lblPWD.Location = new Point(4, 69);
                    btnChangePWD.Location = new Point(25, 184);
                    btnLogin.Location = new Point(195, 184);
                    txtUserID.Location = new Point(149, 31);
                    txtPWD.Location = new Point(149, 63);
                    txtNewPWD.BackColor = Color.LightYellow;
                    txtconfirmPWD.BackColor = Color.LightYellow;
                    txtNewPWD.Focus();
                    grpLogin.Size = new Size(360, 227);
                    txtStates.Location = new Point(12, 308);
                    this.Size = new Size(400, 400);
                }
                else
                {
                    btnLogin.Size = new Size(120, 28);
                    btnChangePWD.Location = new Point(25, 120);
                    btnLogin.Location = new Point(120, 120);
                    
                    lblUserID.Location = new Point(25, 37);
                    lblPWD.Location = new Point(25, 69);
                    txtUserID.Location = new Point(119, 31);
                    txtPWD.Location = new Point(119, 63);
                    txtNewPWD.BackColor = Color.White;
                    txtconfirmPWD.BackColor = Color.White;
                    grpLogin.Size = new Size(360, 160);
                    txtStates.Location = new Point(12, 248);
                    this.Size = new Size(400, 350);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void showAccess()
        {
            try
            {                
                bool PWDOKFlag = false;

                bool blnReadable=false;
                bool blnWritable=false;
                bool blnAddable=false;
                bool blnDeletable=false;
                bool blnDuplicable=false;
                bool blnExportable=false;
                bool blnImportable=false;

                string pUserID="";
                UserName = this.txtUserID.Text;
               
                if (blnISDBSQLserver)
                {
                    #region User SQL Server
                    mySQL = new SqlManager(Login.ServerName, Login.DBName, Login.DBUser, Login.DBPassword);   //140918_0 //140722_2   //140912_0
                    //getLoginStutes(); 141031_00 不再检查登入状态
                    //Username -->LoginName
                    myInfo = mySQL.GetDataTable("select * from UserInfo where LoginName='" + this.txtUserID.Text.ToString() + "'", "UserInfo");

                    DataRow[] myInfoRows = myInfo.Select("LoginName='" + this.txtUserID.Text.ToString() + "'");
                    if (myInfoRows.Length == 1)
                    {
                        
                        if (myInfoRows[0]["LoginPassword"].ToString().ToUpper().Trim() == this.txtPWD.Text.ToString().ToUpper().Trim())
                        {
                            PWDOKFlag = true;
                            pUserID = myInfoRows[0]["ID"].ToString();
                        }
                        else
                        {
                            PWDOKFlag = false;
                        }
                    }

                    if (!PWDOKFlag)
                    {
                        MessageBox.Show("Sorry, either Login Name or Password is not correct. Please try again."
                            , " Login Name or Password is not correct", MessageBoxButtons.OK);
                        return;
                    }
                    else
                    {
                        //140626 获取权限--> 正常来讲一个账号角色分配为1个...
                        string queryCMD = "select sum(FunctionCode) from functiontable where id in "
                            + "(select RoleFunctionTable.FunctionID from RoleFunctionTable where RoleFunctionTable.RoleID  in "
                            + "(select roleID from UserRoleTable where UserRoleTable.UserID = " + pUserID + "))";
                            //+ pUserID + "))";--> ToString+ "(select id from UserInfo	where UserInfo.LoginName='" + this.txtUserID.Text.ToString() + "')))";
                        DataTable myFunctions = mySQL.GetDataTable(queryCMD, "UserRoleFuncTable");
                        
                        
                        long myAccessCode = -1;
                        if (myFunctions.Rows.Count > 0)
                        {
                            myAccessCode= Convert.ToInt64(myFunctions.Rows[0][0].ToString());
                        }
                        else
                        {
                            myAccessCode = -1;
                        }
                        blnReadable = ((myAccessCode & 0x01) == 0x01 ? true:false );
                        blnWritable = ((myAccessCode & 0x02) == 0x02 ? true : false);
                        blnAddable = ((myAccessCode & 0x04) == 0x04 ? true : false);
                        blnDeletable = ((myAccessCode & 0x08) == 0x08 ? true : false);
                        blnDuplicable = ((myAccessCode & 0x10) == 0x10 ? true : false);
                        blnExportable = ((myAccessCode & 0x20) == 0x20 ? true : false);
                        blnImportable = ((myAccessCode & 0x40) == 0x40 ? true : false);
                        //TBD = ((myAccessCode & 0x80) == 0x80 ? true : false);

                        updateStutes(true, myInfo);
                        myLoginID = updateUserLoginInfo();
                        
                    }                                         

                    mySQL.OpenDatabase(false);
                }
                #endregion 
                
               
                this.txtPWD.Text = "";
                this.Hide();

                PNInfo myPNInfo = new PNInfo();
                PNInfo.blnReadable = blnReadable;
                PNInfo.blnWritable = blnWritable;
                PNInfo.blnAddable = blnAddable;
                PNInfo.blnDeletable = blnDeletable;
                PNInfo.blnDuplicable = blnDuplicable;
                PNInfo.blnExportable = blnExportable;
                PNInfo.blnImportable = blnImportable;
                myPNInfo.myLoginID = myLoginID;
                myPNInfo.ShowDialog();
                this.Show();    //141031_0 部分时候无法再次返回主界面 修正
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
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

        #region 141031_00 取消检查登入状况...
        void getLoginStutes()   //140912_0
        {
            try
            {
                string lastLoginInfo =
                    "select spid,dbid,loginame,hostname,login_time,last_batch,status,program_name,hostprocess from master..sysprocesses " +
                    "where   dbid=db_id('" + Login.DBName + "') and loginame ='" + SetPWDCode(Login.DBUser, true, 4) + "'";
               
                DataTable myLoginInfo = mySQL.GetDataTable(lastLoginInfo, "myLoginInfo");
                //141031_0 此部分待再次确认...是否取消[目前无需判定设备ID号]
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
                MessageBox.Show(ex.ToString() + "\n Connecting Error!");
                blnOnlyToReadFlag = true;
            }
        }
        #endregion

        private void Login_Load(object sender, EventArgs e)
        {
            try
            {
                timerDate.Enabled = false; //默认为False,当现实时自动回切换为True;
                startTime = 1;
                mytip.ShowAlways = true;
                myConfigXmlIO = new ConfigXmlIO(Application.StartupPath + @"\Config.xml"); 
                ServerName = myConfigXmlIO.ServerName; 
                if (ServerName.ToUpper().Trim() == "Local".ToUpper().Trim())
                {
                    blnISDBSQLserver = false;
                }
                else
                {
                    blnISDBSQLserver = true;
                }
                chkChangePWD.Visible = blnISDBSQLserver;
                
                NewPWDState(false);
                rdoATSHome.Checked = false;     
	            rdoEDVTHome.Checked = false;    
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        void updateStutes(bool value, DataTable mydt) 
        {
            try
            {
                DataRow[] myRows = mydt.Select();
                if (myRows.Length == 1)
                {
                    string hostname = System.Net.Dns.GetHostName(); //主机
                    System.Net.IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(hostname);//网卡IP地址集合
                    string IP6 = ipEntry.AddressList[0].ToString();//取一个IP
                    string IP4 = ipEntry.AddressList[ipEntry.AddressList.Length-1].ToString();//取一个IP
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
            try
            {
                startTime++;
                mytip.SetToolTip(this, " The Application has been idle more than " + startTime + "S...\r\nIf more than 150 s, the system will be automatically exited!");
                if (startTime >= 150 && startTime > 0)
                {
                    timerDate.Enabled = false;
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
                else 
                {
                    txtStates.Visible = false;
                    txtStates.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Login_VisibleChanged(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
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

        bool creatTable(string accesspath, string queryCMD, string tableName)
        {
            OleDbConnection myconn = new OleDbConnection("Provider = Microsoft.ACE.OLEDB.12.0;" + @"Data Source=" + accesspath);

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

                //ALTER TABLE [user] ALTER COLUMN [id] COUNTER (1, 1)
                //string strIncrement = "Alter TABLE  " + tableName + " ALTER COLUMN [ID]  COUNTER (1, 1)";
                //OleDbCommand setIncrement = new OleDbCommand(strIncrement);
                //setIncrement.Connection = myconn;
                //setIncrement.ExecuteNonQuery();

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
            }
        }

        private void btnChangePWD_Click(object sender, EventArgs e)
        {
            try
            {
                startTime = 0;
                if (txtNewPWD.Text.Length == 0)
                {
                    MessageBox.Show("The New Password is Null!");
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
                        chkChangePWD.Checked = false;   //140714_0
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        bool blnChangePWDOK(string UserName,string oldPWD,string newPWD)
        {
            bool result = false;
            try
            {
                mySQL = new SqlManager(Login.ServerName, Login.DBName, Login.DBUser, Login.DBPassword);   //140918_0 //140722_2   //140912_0            
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

        private void txtNewPWD_TextChanged(object sender, EventArgs e)
        {
            startTime = 0;
        }

        private void txtconfirmPWD_TextChanged(object sender, EventArgs e)
        {
            startTime = 0;
        }


        private void rdoATSHome_CheckedChanged(object sender, EventArgs e)  
        {
            try
            {
                mySQL = null;
                if (rdoATSHome.Checked)
                {
                    Login.DBName = myConfigXmlIO.ATSDBName;
                    Login.DBUser = myConfigXmlIO.ATSUser;       
                    Login.DBPassword = myConfigXmlIO.ATSPWD;    
                    if (Login.DBName == null)
                    {
                        Login.DBName = "ATSHome";
                        myConfigXmlIO.ATSDBName = Login.DBName;
                    }

                    mySQL = new SqlManager(Login.ServerName, Login.DBName, Login.DBUser, Login.DBPassword);   
                    this.Text = "Login(DataSource = " + Login.DBName + ")";
                    grpDBSel.Text = "Choose the DataSource";
                    grpLogin.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void rdoEDVTHome_CheckedChanged(object sender, EventArgs e) 
        {
            try
            {
                mySQL = null;
                if (rdoEDVTHome.Checked)
                {
                    Login.DBName = myConfigXmlIO.EDVTDBName;
                    Login.DBUser = myConfigXmlIO.EDVTUser;     
                    Login.DBPassword = myConfigXmlIO.EDVTPWD;   
                    if (Login.DBName == null)
                    {
                        Login.DBName = "EDVTHome";
                        myConfigXmlIO.EDVTDBName = Login.DBName;
                    }

                    mySQL = new SqlManager(Login.ServerName, Login.DBName, Login.DBUser, Login.DBPassword);  
                    this.Text = "Login(DataSource = " + Login.DBName + ")";
                    grpDBSel.Text = "Choose the DataSource";
                    grpLogin.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}

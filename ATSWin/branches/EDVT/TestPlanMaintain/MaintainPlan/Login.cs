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
        DataIO mySQL ;  //140704_1= new SqlManager(); 
        DataTable myInfo;
        ToolTip mytip = new ToolTip();
        ConfigXmlIO myConfigXmlIO;

        public static string ServerName = "";
        public static string DBName = "";   //140912_0
        public static string DBUser = "";     //140917_2
        public static string DBPassword = "";      //140917_2

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
                    MessageBox.Show("请输入用户和密码后登入!", "用户名或密码未输入", MessageBoxButtons.OK);
                }
                else
                {
                    if (this.chkSQLlib.Checked == false)
                    {
                        blnISDBSQLserver = false;
                        OpenFileDialog openFileDialog = new OpenFileDialog();
                        openFileDialog.InitialDirectory = "d:\\";//注意这里写路径时要用c:\\而不是c:\
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
                            MessageBox.Show("请选择对应的Access文件!", "路径未选择", MessageBoxButtons.OK);
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
                MessageBox.Show("Err-->" + ex.Message + ";发生于" + ex.Source);
                btnLogin.Enabled = true;
            }
        }

        void NewPWDState(bool state)
        {
            try
            {
                txtNewPWD.Visible = state;
                txtConfrimPWD.Visible = state;
                lblConfrimPWD.Visible = state;
                lblNewPWD.Visible = state;
                btnChangePWD.Visible = state;

                if (state)
                {
                    btnChangePWD.Location = new Point(27, 162);
                    btnLogin.Location = new Point(169, 162);
                    txtNewPWD.BackColor = Color.LightYellow;
                    txtConfrimPWD.BackColor = Color.LightYellow;
                    txtNewPWD.Focus();
                    grpLogin.Size = new Size(267, 198);
                    txtStates.Location = new Point(12, 273);
                    this.Size = new Size(350, 360);
                }
                else
                {
                    btnChangePWD.Location = new Point(169, 98);
                    btnLogin.Location = new Point(100, 98);
                    txtNewPWD.BackColor = Color.White;
                    txtConfrimPWD.BackColor = Color.White;
                    grpLogin.Size = new Size(267, 130);
                    txtStates.Location = new Point(12, 210);
                    this.Size = new Size(350, 310);
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
                if (this.chkSQLlib.CheckState == CheckState.Checked)
                {
                    #region User SQL Server
                    mySQL = new SqlManager(Login.ServerName, Login.DBName, Login.DBUser, Login.DBPassword);   //140918_0 //140722_2   //140912_0
                    getLoginStutes();
                    //Username -->LoginName
                    myInfo = mySQL.GetDataTable("select * from UserInfo where LoginName='" + this.txtUserID.Text.ToString() + "'", "UserInfo");

                    DataRow[] myInfoRows = myInfo.Select("LoginName='" + this.txtUserID.Text.ToString() + "'");
                    if (myInfoRows.Length == 1)
                    {
                        //140612_2 Password-->UserPassword
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
                        MessageBox.Show("用户或密码不正确!", "用户名或密码错误", MessageBoxButtons.OK);
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
                        
                        //140630_0 修改为读取sum(FunctionCode) 来体现,原为判断Title>>>>>>>>>>>>>>>>>
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

                        #region 原为判断Title>
                        //for (int i = 0; i < myFunctions.Rows.Count; i++)
                        //{
                        //    if (myFunctions.Rows[i]["Title"].ToString().ToUpper() == "Readable".ToUpper())
                        //    {
                        //        blnReadable = true;
                        //    }
                        //    else if (myFunctions.Rows[i]["Title"].ToString().ToUpper() == "Writable".ToUpper())
                        //    {
                        //        blnWritable = true;
                        //    }
                        //    else if (myFunctions.Rows[i]["Title"].ToString().ToUpper() == "Addable".ToUpper())
                        //    {
                        //        blnAddable = true;
                        //    }
                        //    else if (myFunctions.Rows[i]["Title"].ToString().ToUpper() == "Deletable".ToUpper())
                        //    {
                        //        blnDeletable = true;
                        //    }
                        //    else if (myFunctions.Rows[i]["Title"].ToString().ToUpper() == "Duplicable".ToUpper())
                        //    {
                        //        blnDuplicable = true;
                        //    }
                        //    else if (myFunctions.Rows[i]["Title"].ToString().ToUpper() == "Exportable".ToUpper())
                        //    {
                        //        blnExportable = true;
                        //    }
                        //    else if (myFunctions.Rows[i]["Title"].ToString().ToUpper() == "Importable".ToUpper())
                        //    {
                        //        blnImportable = true;
                        //    }
                        //}
                        #endregion

                        //140630_0 修改为读取sum(FunctionCode) 来体现,原为判断Title<<<<<<<<<<<<<<<<<<<
                        updateStutes(true, myInfo);
                    }                                         

                    mySQL.OpenDatabase(false);
                }
                #endregion 
                
                UserName = this.txtUserID.Text;
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
                myPNInfo.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void getLoginStutes()   //140912_0
        {
            try
            {
                string lastLoginInfo =
                    "select spid,dbid,loginame,hostname,login_time,last_batch,status,program_name,hostprocess from master..sysprocesses " +
                    "where   dbid=db_id('" + Login.DBName + "') and loginame ='BackGround'";
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
                        MessageBox.Show("当前已经存在用户登入,请知悉!-->"
                            + "\n 登入电脑名称: " + lastComputerName
                            + "\n 登入时间: " + loginTime
                            + "\n 最后一次处理时间: " + last_request_start_time
                            + "\n 由于多个用户可能导致资料紊乱,故限制此次登入只能读取,不能进行更新资料到数据库! "
                            + "\n 如需要使用请联系已经登入的使用者!!!或稍后再登入!!!"
                            , "注意!"
                            , MessageBoxButtons.OK
                            , MessageBoxIcon.Information
                             );
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
                MessageBox.Show(ex.ToString() + "\n 连接发生问题,可能导致资料紊乱,故限制此次登入只能读取,不能进行更新资料到数据库!");
                blnOnlyToReadFlag = true;
            }
        }
       
        private void chkSQLlib_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                startTime = 0;
                if (chkSQLlib.Checked)
                {
                    blnISDBSQLserver = true;
                    chkChangePWD.Visible = true;
                }
                else
                {
                    blnISDBSQLserver = false;
                    chkChangePWD.Visible = false;
                    chkChangePWD.Checked = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
            try
            {
                timerDate.Enabled = false; //默认为False,当现实时自动回切换为True;
                startTime = 1;
                mytip.ShowAlways = true;
                myConfigXmlIO = new ConfigXmlIO(Application.StartupPath + @"\Config.xml"); //140704_1
                ServerName = myConfigXmlIO.ServerName; //140704_1
                NewPWDState(false);
                rdoATSHome.Checked = false;     //140912_0
	            rdoEDVTHome.Checked = false;    //140912_0
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        void updateStutes(bool value, DataTable mydt) //140604_2 //userName --> LoginName 140626
        {
            //myInfo = mySQL.GetDataTable("select * from UserInfo where Username='" + this.txtUserID.Text.ToString() + "'", "Username");
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
                mytip.SetToolTip(this, "程式已经被开启且处于闲置状态: " + startTime + " S;\n"
                    + "超过150S未动作则将自动退出!");
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
                        txtStates.Text = " 程式已闲置超过 " + startTime + "S,若超过150S,系统自动退出!";
                        txtStates.ForeColor = Color.Red;
                        txtStates.Refresh();
                    }
                }
                else //140716_0
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
                    MessageBox.Show("新密码不能为空!");
                    return;
                }
                if (txtNewPWD.Text.Length > 0 && txtNewPWD.Text.Length == 0)
                {
                    MessageBox.Show("确认密码不能为空!");
                    txtConfrimPWD.Focus();
                    return;
                }
                else if (txtNewPWD.Text.ToString() != txtConfrimPWD.Text.ToString())
                {
                    MessageBox.Show("确认密码与新密码不同!!!");
                    txtConfrimPWD.BackColor = Color.Yellow;
                    txtConfrimPWD.Focus();
                    return;
                }
                else
                {
                    if (blnChangePWDOK(txtUserID.Text.Trim(), txtPWD.Text.Trim(), txtNewPWD.Text.Trim()))
                    {
                        txtPWD.Text = txtNewPWD.Text.Trim();
                        MessageBox.Show("密码已经修改完成\n 新密码已经自动填充到密码处! \n 可直接登入!!!");
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
                    //140612_2 Password-->UserPassword
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
                        MessageBox.Show("用户旧密码不正确!", "用户名或密码错误", MessageBoxButtons.OK);
                    }
                }
                else
                {
                    MessageBox.Show("用户名称不正确!", "用户名错误", MessageBoxButtons.OK);
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

        private void txtConfrimPWD_TextChanged(object sender, EventArgs e)
        {
            startTime = 0;
        }


        private void rdoATSHome_CheckedChanged(object sender, EventArgs e)  //140911_0
        {
            try
            {
                mySQL = null;
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
                    this.Text = "Login(DataSoure = " + Login.DBName + ")";
                    grpDBSel.Text = "数据源选择";
                    grpLogin.Enabled = true;
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
                    this.Text = "Login(DataSoure = " + Login.DBName + ")";
                    grpDBSel.Text = "数据源选择";
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

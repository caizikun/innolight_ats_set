using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using ATSDataBase;
using ATS;

namespace Maintain
{
    public partial class Login : Form
    {
        public string UserName = "";
        public string AccessFilePath = "";
        //public bool blnISDBSQLserver = false;
        //public bool blnOnlyToReadFlag = false;
        DataIO mySQL;  //140704_1= new SqlManager(); 
        DataTable myInfo;
        ToolTip mytip = new ToolTip();
        ConfigXmlIO myConfigXmlIO;

        public string ServerName = "";
        public string DBName = "";   //140912_0
        public string DBUser = "";     //140917_2
        public string DBPassword = "";      //140917_2
        //private bool isMaintainUser = false;
        public static int startTime = 0;
        string myLoginID = "";
        int LoginNum = 0;


        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            try
            {
                chkChangePWD.Visible = false;
                timerDate.Enabled = false; //默认为False,当现实时自动回切换为True;
                startTime = 1;
                mytip.ShowAlways = true;
                myConfigXmlIO = new ConfigXmlIO(Application.StartupPath + @"\Config.xml"); //140704_1
                ServerName = myConfigXmlIO.ServerName; //140704_1

                DBName = myConfigXmlIO.ATSDBName;
                DBUser = myConfigXmlIO.ATSUser;       //140918_0
                DBPassword = myConfigXmlIO.ATSPWD;    //140918_0
                if (DBName == null)
                {
                    DBName = "ATSHome";
                    myConfigXmlIO.ATSDBName = DBName;
                }

                mySQL = new SqlManager(ServerName, DBName, DBUser, DBPassword);   //140917_2
                this.Text = "Login[EEPROM_WRITE](DataSoure = " + DBName + ")";
                //grpDBSel.Text = "数据源选择";
                grpLogin.Enabled = true ;

                NewPWDState(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            startTime = 1;
            
            try
            {
                btnLogin.Enabled = false;

                if (this.txtUserID.Text.Trim().Length == 0 || this.txtPWD.Text.Trim().Length == 0)
                {
                    MessageBox.Show("请输入用户名和密码后登入!", "用户名或密码未输入", MessageBoxButtons.OK);
                    //MessageBox.Show("Please input UserName and Password to login!", "UserName or Password Not Input", MessageBoxButtons.OK);
                }
                else
                {
                        AccessFilePath = "SQL Server";
                        showAccess();                   
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
                    btnChangePWD.Location = new Point(54, 181);
                    btnLogin.Location = new Point(178, 181);
                    txtNewPWD.BackColor = Color.LightYellow;
                    txtConfrimPWD.BackColor = Color.LightYellow;
                    txtNewPWD.Focus();
                    grpLogin.Size = new Size(310, 223);
                    txtStates.Location = new Point(23, 248);
                    this.Size = new Size(380, 330);
                }
                else
                {
                    btnChangePWD.Location = new Point(191, 109);
                    btnLogin.Location = new Point(122, 109);
                    txtNewPWD.BackColor = Color.White;
                    txtConfrimPWD.BackColor = Color.White;
                    grpLogin.Size = new Size(310, 150);
                    txtStates.Location = new Point(23, 170);
                    this.Size = new Size(380, 252);
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
                bool isAdmin = false;

                string pUserID = "";
                UserName = this.txtUserID.Text;

                //if (blnISDBSQLserver && isMaintainUser)
                //{
                    #region User SQL Server
                    mySQL = new SqlManager(ServerName, DBName, DBUser, DBPassword);   //140918_0 //140722_2   //140912_0
                    //141009_0  //getLoginStutes();
                    //Username -->LoginName
                    myInfo = mySQL.GetDataTable("select * from UserInfo where LoginName='" + this.txtUserID.Text.ToString() + "'", "UserInfo");

                    DataRow[] myInfoRows = myInfo.Select("LoginName='" + this.txtUserID.Text.ToString() + "'");
                    
                    if (myInfoRows.Length == 1)
                    {
                        //140612_2 Password-->UserPassword
                        if (myInfoRows[0]["LoginPassword"].ToString() == this.txtPWD.Text.ToString())
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

                        if (LoginNum < 2)
                        {
                            int num = 2 - LoginNum;
                            MessageBox.Show("             用户或密码不正确！      \r\n             ( 剩余输入次数：" + num + " )", "用户名或密码错误", MessageBoxButtons.OK);
                            LoginNum++;
                            return;
                        }
                        else
                        {
                            this.Close();
                        }
                                               
                        //MessageBox.Show("UserName or Password Error is wrong!", "UserName or Password Error", MessageBoxButtons.OK);

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
                            myAccessCode = Convert.ToInt64(myFunctions.Rows[0][0].ToString());
                        }
                        else
                        {
                            myAccessCode = -1;
                        }

                        isAdmin = ((myAccessCode & 0x100) == 0x100 ? true : false);

                    }

                    if (!isAdmin)
                    {
                        MessageBox.Show("当前用户并未取得维护出货资料的权限,请确认!");
                        return;
                    }
                    else
                    {
                    //    myLoginID = updateUserLoginInfo();  //141027_0
                    }

                    mySQL.OpenDatabase(false);
                    mySQL.Dispose();
                //}
                    #endregion

                this.txtPWD.Text = "";
                this.Hide();

                Mainform myMainform = new Mainform();
                //myMainform.isMaintainUser = isAdmin;
                //myMainform.blnISDBSQLserver =blnISDBSQLserver;

                myMainform.ServerName = ServerName;
                myMainform.DbName = DBName;
                myMainform.DbUser = DBUser;
                myMainform.DbPassword = DBPassword;
                myMainform.AccessFilePath = AccessFilePath;
                myMainform.user = this.txtUserID.Text;
                myMainform.myLoginID = myLoginID;   //141027_0
                myMainform.ShowDialog();

                LoginNum = 0;

                this.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //void updateStutes(bool value, DataTable mydt) //140604_2 //userName --> LoginName 140626
        //{
        //    //myInfo = mySQL.GetDataTable("select * from UserInfo where Username='" + this.txtUserID.Text.ToString() + "'", "Username");
        //    try
        //    {
        //        DataRow[] myRows = mydt.Select();
        //        if (myRows.Length == 1)
        //        {
        //            string hostname = System.Net.Dns.GetHostName(); //主机
        //            System.Net.IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(hostname);//网卡IP地址集合
        //            string IP6 = ipEntry.AddressList[0].ToString();//取一个IP
        //            string IP4 = ipEntry.AddressList[ipEntry.AddressList.Length - 1].ToString();//取一个IP
        //            myRows[0].BeginEdit();
        //            //myRows[0]["ISLogin"] = value;
        //            myRows[0]["lastLoginONTime"] = DateTime.Now.ToString();     //140605_0
        //            myRows[0]["lastComputerName"] = System.Environment.MachineName.ToString();
        //            myRows[0]["lastIP"] = IP4;
        //            myRows[0].EndEdit();

        //            mySQL.UpdateDataTable("select * from UserInfo where LoginName='" + this.txtUserID.Text.ToString() + "'", mydt, false);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //    }
        //}

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

                // 有问题！！，2014-10-30 16:27:03
                if (txtNewPWD.Text.Length > 0 && txtNewPWD.Text.Length == 0)
                {
                    MessageBox.Show("确认密码不能为空!");
                    txtConfrimPWD.Focus();
                    return;
                }
                else if (txtNewPWD.Text.ToString() != txtConfrimPWD.Text.ToString())
                {
                    MessageBox.Show("确认密码与新密码不同!");
                    txtConfrimPWD.BackColor = Color.Yellow;
                    txtConfrimPWD.Focus();
                    return;
                }
                else
                {
                    if (blnChangePWDOK(txtUserID.Text.Trim(), txtPWD.Text.Trim(), txtNewPWD.Text.Trim()))
                    {
                        txtPWD.Text = txtNewPWD.Text.Trim();
                        MessageBox.Show("密码已经修改完成\n 新密码已经自动填充到密码处! \n 可直接登入!");
                        chkChangePWD.Checked = false;   //140714_0
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
                mySQL = new SqlManager(ServerName, DBName, DBUser, DBPassword);   //140918_0 //140722_2   //140912_0            
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

        private void txtConfrimPWD_TextChanged(object sender, EventArgs e)
        {
            startTime = 0;
        }

        private void txtNewPWD_TextChanged(object sender, EventArgs e)
        {
            startTime = 0;
        }

        private void Login_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin_Click(sender, e);
            }
        }

    }
}

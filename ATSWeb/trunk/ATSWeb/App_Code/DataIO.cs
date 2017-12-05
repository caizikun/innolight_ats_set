using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data.Common;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ATSDataBase
{
    public class DataIO : IDisposable //基类 虚方法!
    {
        public DataIO()
        {
        }

        #region Leo
        /// <summary>
        /// 打开或关闭数据连接
        /// </summary>
        /// <param name="Swith">true=打开;false=关闭</param>
        /// <returns>true=操作成功;否则失败</returns>
        public virtual bool OpenDatabase(bool Swith) { return true; }
        public virtual bool WriterLog(int ConditionID, int SNID, string StrInf, string StartTime, string EndTime, float Temp, float Voltage, byte Channel, bool Resultlflag, out int logid) { logid = -1; return true; }
        public virtual bool WriteResult(int StrInterfaceLogid, DataTable ResultDataTabel) { return true; }
        public virtual DataTable GetDataTable(string sqlCmd, string StrTableName)
        {
            return null;
        }
        public virtual bool WriterSN(int TestPlanID, string StrSN, string StrFwRev, string StrIPaddress, string strLightSourceMessage, out int snid) { snid = -1; return true; }


        #region  Dispose()

        // Public implementation of Dispose pattern callable by consumers.
        bool disposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here.
                //
            }

            // Free any unmanaged objects here.
            //
            disposed = true;
        }
        #endregion

        #endregion
        /// <summary>
        /// 获取数据表中最后一笔记录的ID
        /// </summary>
        /// <param name="tableName">指定的数据表</param>
        /// <returns>返回-1表示获取失败,否则成功!</returns>
        public virtual long GetLastInsertData(string tableName)
        {
            return -1;
        }
        /// <summary>
        /// ATSWeb不再使用此方法
        /// </summary>
        /// <returns>false:操作失败</returns>
        public virtual bool UpdateDT()
        {
            return false;
        }

        /// <summary>
        /// 更新Web操作日志
        /// </summary>
        /// <param name="loginID">[Default.aspx]用户登入时在UserLoginInfo中生成的ID号*Session["UserLoginID"]*</param>
        /// <param name="blockTypeName">用户操作的模块名称</param>
        /// <param name="dt">被改变的DataTable</param>
        /// <param name="dtName">被改变的DataTable名称</param>
        /// <param name="OPItem">操作项目的名称[eg:修改PN='A'的TestPlan='Tp0'的资料,OPItem="A"]</param>
        /// <param name="OPChildItem">操作子项目的名称[eg:修改PN='A'的TestPlan='Tp0'的资料,OPChildItem="Tp0"]</param>
        /// <param name="sqlCmd">查询命令[默认为string sqlCmd = "Select * from OperationLogs where ID=-1"]</param>
        /// <returns>操作结果=true 成功!</returns>
        public virtual bool UpdateOperateLog(string loginID, string blockTypeName, string tracingInfo, DataTable dt, string dtName, string sqlCmd = "Select * from OperationLogs where ID=-1") //150414 Add
        {
            return false;
        }

        /// <summary>
        /// 更新数据表资料
        /// </summary>
        /// <param name="sqlCmd">查询字符串</param>
        /// <param name="dt">新的数据表(有修改的表)</param>
        /// <returns>返回true表示成功;否则操作失败</returns>
        public virtual bool UpdateDataTable(string sqlCmd, DataTable dt) //140722重新启用 //140612_1
        {
            return true;
        }

        /// <summary>
        /// 更新数据表资料
        /// </summary>
        /// <param name="sqlCmd">查询字符串</param>
        /// <param name="dt">新的数据表(有修改的表)</param>
        /// <returns>返回true表示成功;否则操作失败,可以返回最后一个插入的记录的lastID</returns>
        public virtual bool UpdateDataTable(string sqlCmd, DataTable dt, out long LastID) //140722重新启用 //140612_1
        {
            LastID = -1;
            return true;
        }
        /// <summary>
        /// 返回PID
        /// </summary>
        /// <param name="sqlCmd">查询条件</param>
        /// <returns>返回-1操作失败</returns>
        public virtual long GetPID(string sqlCmd)
        {
            return -1;
        }
        /// <summary>
        /// 从数据库中查询是否包含这个表名
        /// </summary>
        /// <param name="tableName">查询的表名</param>
        /// <returns>返回true成功;否则失败</returns>
        public virtual bool BlnISExistTable(string tableName)
        {
            return true;
        }
        /// <summary>
        /// 返回SQL数据库中的所有表名
        /// </summary>
        /// <param name="serverName"></param>
        /// <param name="dbName"></param>
        /// <param name="userName"></param>
        /// <param name="pwd"></param>
        /// <returns>所有表名 string[]</returns>
        public virtual string[] GetCurrTablesName(string serverName, string dbName, string userName, string pwd)
        {
            return null;
        }
        /// <summary>
        /// 返回Accdb数据库中的所有表名
        /// </summary>
        /// <param name="Accesspath">Accdb</param>
        /// <returns>所有表名 string[]</returns>
        public virtual string[] GetCurrTablesName(string Accesspath)
        {
            return null;
        }
        /// <summary>
        /// 更新数据表资料
        /// </summary>
        /// <param name="sqlCmd">查询字符串</param>
        /// <param name="dt">新的数据表(有修改的表)</param>        
        /// <param name="IsAddNewData">是否修改所有数据状态为新增(若行的状态不为Unchanged则引发异常)</param>
        /// <returns>返回true表示成功;否则操作失败</returns>
        public virtual bool UpdateDataTable(string sqlCmd, DataTable dt, bool IsAddNewData)
        {
            return true;
        }

        /// <summary>
        /// 返回服务器当前的时间
        /// </summary>
        /// <returns></returns>
        public virtual DateTime GetCurrTime()
        {
            return DateTime.Parse("2000/1/1 12:00:00");
        }

        /// <summary>
        /// 基类方法:使用DbDataReader进行数据获取
        /// </summary>
        /// <param name="queryString">需要查询的条件</param>
        /// <returns>DbDataReader</returns>
        public virtual DbDataReader getDbDataReader(string queryString, CommandType cmdType = CommandType.Text)
        {
            return null;
        }

        /// <summary>
        /// WriteErrorLogs 默认是在UploadFile文件中
        /// </summary>        
        /// <param name="ss">需要写入的字符串信息</param>
        /// <param name="fileName">文件名若不指定则写入默认指定文档中</param>
        /// <param name="ssFilePath">ssFilePath默认为空路径,系统将分配至UploadFile文件夹</param>  
        public virtual void WriteErrorLogs(string ss, string fileName = "", string ssFilePath = "")
        {
        }

        public virtual void WriteLogs(string ss, string fileName = "", string ssFilePath = "")
        {
        }

        /// <summary>
        /// 返回执行命令后的第一行第一列资料 object
        /// </summary>
        /// <param name="queryString">命令字符串</param>
        /// <param name="cmdType">cmdType</param>
        /// <returns>-1表示失败</returns>
        public virtual object getDbCmdExecuteScalar(string queryString, CommandType cmdType = CommandType.Text)
        {
            return -1;
        }

        /// <summary>
        /// 返回执行命令后的受影响的行数
        /// </summary>
        /// <param name="queryString">命令字符串</param>
        /// <param name="cmdType">cmdType</param>
        /// <returns>结果数目:int;如为-1则表示执行失败</returns>
        public virtual int getDbCmdExecuteNonQuery(string queryString, CommandType cmdType = CommandType.Text)
        {
            return -1;
        }

        public virtual DataSet ExecuteDS(string sqlStr)
        {
            return null;
        }

        /// <summary>
        /// 向目标数据库复制数据
        /// </summary>
        /// <param name="server","database","uid","password">目标数据库信息</param>
        /// <param name="tableName">表名</param>
        public virtual void BulkCopyTo(string server, string database, string uid, string password, string tableName)
        {
        }
        
        /// <summary>
        /// 使用存储过程更新数据表资料(不包含参数表资料)
        /// </summary>
        /// <param name="tableName">更新的资料表名称</param>
        /// <param name="dt">待更新的datatable</param>
        /// <param name="queryCMD">查询字符串</param>
        /// <param name="opItemName">操作的项目</param>
        /// <param name="opChildItemName">操作的子项目</param>
        /// <param name="tracingInfo">追溯信息(eg:Type->PN->TestPlan->...)</param>
        /// <param name="databaseName">数据库DBname,默认是"ATS_V2"</param>
        /// <returns>返回值大于0表示成功(返回的是ID号),否则失败!</returns>
        public virtual int UpdateWithProc(string tableName, DataTable dt, string queryCMD,
            string tracingInfo, string databaseName)
        {
            return -1;
        }

        /// <summary>
        /// 使用存储过程更新数据表资料(包含参数资料)For TopoTestModelWithParams + TopoEquipmentWithParams
        /// </summary>
        /// <param name="tableName">更新的资料表名称</param>
        /// <param name="dt">待更新的datatable</param>
        /// <param name="paramsDt">待更新的params datatable</param>
        /// <param name="opItemName">操作的项目</param>
        /// <param name="opChildItemName">操作的子项目</param>
        /// <param name="tracingInfo">追溯信息(eg:Type->PN->TestPlan->...)</param>
        /// <param name="databaseName">数据库DBname,默认是"ATS_V2"</param>
        /// <returns>返回值大于0表示成功(返回的是ID号),否则失败!</returns>
        public virtual int UpdateWithProc(string tableName, DataTable dt, DataTable paramsDt,
            string tracingInfo, string databaseName = "ATS_V2")
        {
            return -1;
        }

        /// <summary>
        /// 复制TestPlan
        /// </summary>
        /// <param name="sourcePlanID">源TestPlan ID</param>
        /// <param name="NewPlanName">新生成的TestPlan名称</param>
        /// <param name="newPlanPID">新生成的TestPlanPID</param>
        /// <param name="tracingInfo">追溯信息</param>
        /// <param name="errMsg">返回的错误信息</param>
        /// <returns>结果>0则成功,否则失败!实际的错误代码为 abs(Result);</returns>
        public virtual int CopyTestPlan(int sourcePlanID, string newPlanName, int newPlanPID, string tracingInfo, out string errMsg)
        {
            errMsg = "";
            return -1;
        }

        /// <summary>
        /// 复制FlowCtrl
        /// </summary>
        /// <param name="sourceCtrlID">源FlowCtrl ID</param>
        /// <param name="NewCtrlName">新生成的FlowCtrl名称</param>
        /// <param name="newCtrlPID">新生成的FlowCtrlPID</param>
        /// <param name="tracingInfo">追溯信息</param>
        /// <param name="errMsg">返回的错误信息</param>
        /// <returns>结果>0则成功,否则失败!实际的错误代码为 abs(Result);</returns>
        public virtual int CopyFlowCtrl(int sourceCtrlID, string newCtrlName, int newCtrlPID, string tracingInfo, out string errMsg)
        {
            errMsg = "";
            return -1;
        }

        /// <summary>
        /// 复制MCoef
        /// </summary>
        /// <param name="sourceMCoefID">源MCoef ID</param>
        /// <param name="NewMCoefName">新生成的MCoef名称</param>
        /// <param name="NewMCoefDescription">新生成的MCoef描述</param>
        /// <param name="newMCoefPID">新生成的MCoefPID</param>
        /// <param name="tracingInfo">追溯信息</param>
        /// <param name="errMsg">返回的错误信息</param>
        /// <returns>结果>0则成功,否则失败!实际的错误代码为 abs(Result);</returns>
        public virtual int CopyMCoef(int sourceMCoefID, string newMCoefName, string NewMCoefDescription, int newMCoefPID, string tracingInfo, out string errMsg)
        {
            errMsg = "";
            return -1;
        }

        /// <summary>
        /// 自定义弹出窗口内容
        /// </summary>
        /// <param name="msg"></param>
        public void AlertMsgShow(string msg)
        {
            msg = msg.Replace("'", @"""").Replace("\r", "").Replace("\n", "\\n").Replace("\t", "\\t");
            //HttpContext.Current.Response.Write("<Script>alert('" + msg + "')</Script>");
            ScriptManager.RegisterStartupScript((System.Web.UI.Page)HttpContext.Current.CurrentHandler, typeof(System.Web.UI.Page), "", "<script>alert('" + msg + "');</script>", false);
        }

        /// <summary>
        /// 自定义弹出窗口内容并转向一个新的页面
        /// </summary>
        /// <param name="msg">自定义消息</param>
        /// <param name="Url">需要转到的新页面</param>
        public void AlertMsgShow(string msg, string Url)
        {
            msg = msg.Replace("'", @"""").Replace("\r", "").Replace("\n", "\\n").Replace("\t", "\\t");
            //HttpContext.Current.Response.Write("<script>alert('" + msg + "');javascript:location='"+Url+"';</script>");
            ScriptManager.RegisterStartupScript((System.Web.UI.Page)HttpContext.Current.CurrentHandler, typeof(System.Web.UI.Page), "aaa", "<script>alert('" + msg + "');javascript:location='" + Url + "';</script>", false);

        }
        /// <summary>
        /// 自定义弹出窗口内容，自定义是否关闭当前页面
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="close"></param>
        public void AlertMsgShow(string msg, bool close)
        {
            msg = msg.Replace("'", @"""").Replace("\r", "").Replace("\n", "\\n").Replace("\t", "\\t");
            if (close)
            {
                //HttpContext.Current.Response.Write("<script>alert('" + msg + "');javascript:window.close();</script>");
                ScriptManager.RegisterStartupScript((System.Web.UI.Page)HttpContext.Current.CurrentHandler, typeof(System.Web.UI.Page), "aaa", "<script>alert('" + msg + "');javascript:window.close();</script>", false);
            }
            else
            {
                //HttpContext.Current.Response.Write("<script>alert('" + msg + "');</script>");
                ScriptManager.RegisterStartupScript((System.Web.UI.Page)HttpContext.Current.CurrentHandler, typeof(System.Web.UI.Page), "aaa", "<script>alert('" + msg + "');</script>", false);
            }
        }

    }


    //完全的公共部分开始!++++++++++++++++++++++++++++++++++++++  //140722
    public class ServerDatabaseIO : DataIO	//SQL子类 2个公共的方法!
    {
        protected SqlConnection conn;
        /// <summary>
        /// WriteErrorLogs 默认是在UploadFile文件中
        /// </summary>        
        /// <param name="ss">需要写入的字符串信息</param>
        /// <param name="fileName">文件名若不指定则写入默认指定文档:SQLChangeErrorLogs.txt</param>
        /// <param name="ssFilePath">ssFilePath默认为空路径,系统将分配至UploadFile文件夹</param>
        public override void WriteErrorLogs(string ss, string fileName = "", string ssFilePath = "")
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(ss))
                {
                    if (string.IsNullOrWhiteSpace(fileName))
                    {
                        fileName = "SQLChangeErrorLogs.txt";
                    }

                    if (string.IsNullOrWhiteSpace(ssFilePath))
                    {
                        if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/") + "UploadFile"))
                        {
                            Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/") + "UploadFile");
                        }
                        ssFilePath = HttpContext.Current.Server.MapPath("~/") + "UploadFile";
                    }
                    else
                    {
                        if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/") + ssFilePath))
                        {
                            Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/") + ssFilePath);
                        }
                        ssFilePath = HttpContext.Current.Server.MapPath("~/") + ssFilePath;
                    }

                    System.IO.FileStream fs = new System.IO.FileStream(ssFilePath + "/" + fileName, System.IO.FileMode.Append);
                    System.IO.StreamWriter sw = new System.IO.StreamWriter(fs, Encoding.Default);
                    sw.WriteLine("=====Error=====\r\n" +
                        DateTime.Now.ToString() + "-->" + System.Environment.UserName + ":" + System.Environment.MachineName
                        + ";IP:" + HttpContext.Current.Request.UserHostAddress
                        + ";Browser:" + HttpContext.Current.Request.Browser.Browser + "\r\n" + ss);
                    sw.Close();
                    fs.Close();
                }
            }
            catch
            { }
        }

        public override void WriteLogs(string ss, string fileName = "", string ssFilePath = "")
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(ss))
                {
                    if (string.IsNullOrWhiteSpace(fileName))
                    {
                        fileName = "SQLChangeErrorLogs.txt";
                    }

                    if (string.IsNullOrWhiteSpace(ssFilePath))
                    {
                        if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/") + "UploadFile"))
                        {
                            Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/") + "UploadFile");
                        }
                        ssFilePath = HttpContext.Current.Server.MapPath("~/") + "UploadFile";
                    }
                    else
                    {
                        if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/") + ssFilePath))
                        {
                            Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/") + ssFilePath);
                        }
                        ssFilePath = HttpContext.Current.Server.MapPath("~/") + ssFilePath;
                    }

                    System.IO.FileStream fs = new System.IO.FileStream(ssFilePath + "/" + fileName, System.IO.FileMode.Append);
                    System.IO.StreamWriter sw = new System.IO.StreamWriter(fs, Encoding.Default);
                    sw.WriteLine(ss);
                    sw.Close();
                    fs.Close();
                }
            }
            catch
            { }
        }

        /// <summary>
        /// 进行加密和解密操作!!!
        /// </summary>
        /// <param name="str">待加密或解密的字符串!</param>
        /// <param name="decode">解密? true =解密|false=加密</param>
        /// <param name="Codelength">1位密码的位元长度</param>
        /// <returns>返回结果 string</returns>
        protected  string SetPWDCode(string str, bool decode, int Codelength)  //140918_0 增加加密和解密部分!
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
                            throw new Exception("当前的密码字符串中存在汉字,可能导致解密失败,请重新输入密码!!!");     //MessageBox.Show("当前的密码字符串中存在汉字,可能导致解密失败,请重新输入密码!!!");
                            //return null;
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
                ////throw ex;     //MessageBox.Show(ex.ToString());
                return resultStr;
            }

        }

        public ServerDatabaseIO(string serverName) //读取XML后配置此部分
        {
            string pserverName = serverName;
            string pdbName = "ATS_V2";
            string puser = "ATSUser";
            string ppwd = "ats#inno2014";

            //140917_0 Add
            //string pdbName = "EDVTHOME";  
            //string puser = "EDVTUser";
            //string ppwd = "edvt#inno2014";

            string strConn = "Server=" + pserverName + ";Initial Catalog=" + pdbName + ";" + "user id=" + puser + ";password=" + ppwd + ";";
            conn = new SqlConnection(strConn);
        }

        public ServerDatabaseIO(string serverName, string dbName, string user, string pwd) 
        {
            string user_id = SetPWDCode(user, true, 4);
            string password = SetPWDCode(pwd, true, 4);
            string strConn = "Server=" + serverName + ";Initial Catalog=" + dbName + ";" + "user id=" + user_id + ";password=" + password + ";";
            conn = new SqlConnection(strConn);
        }

        /// <summary>
        /// 打开或关闭数据连接
        /// </summary>
        /// <param name="Swith">true=打开;false=关闭</param>
        /// <returns>true=操作成功;否则失败</returns>
        public override bool OpenDatabase(bool state)
        {
            bool result = false;
            try
            {
                if (state)
                {
                    if (conn == null) conn.Open();
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }
                }
                else
                {
                    if (conn != null && conn.State != ConnectionState.Closed) //140625_0
                    {
                        conn.Close();
                    }
                }

                result = true;
                return result;
            }
            catch (Exception ex)
            {
                WriteErrorLogs(ex.ToString());
                AlertMsgShow(ex.Message);
                return result;
            }
        }

        public override DataTable GetDataTable(string sqlCmd, string StrTableName)
        {
            SqlCommand cm = new SqlCommand(sqlCmd, conn);
            SqlDataAdapter da = new SqlDataAdapter(cm);
            DataTable mydt = new DataTable(StrTableName);
            try
            {

                if (conn == null) conn.Open();
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                da.Fill(mydt);
            }
            catch (Exception ex)
            {
                WriteErrorLogs(ex.ToString());
                AlertMsgShow(ex.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return mydt;
        }

        /// <summary>
        /// 返回服务器当前的时间
        /// </summary>
        /// <returns></returns>
        public override DateTime GetCurrTime()
        {
            DateTime servertime = DateTime.Now; //默认为本机时间,若获取服务器时间成功则为服务器时间!
            try
            {
                OpenDatabase(true);
                //执行存储过程 
                System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand("getcurrservertime ", conn);
                //说明命令要执行的是存储过程     
                command.CommandType = CommandType.StoredProcedure;
                //向存储过程中传递参数   

                command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@currtime", SqlDbType.DateTime));

                command.Parameters["@currtime"].Direction = ParameterDirection.Output;
                command.UpdatedRowSource = UpdateRowSource.None;
                command.ExecuteNonQuery();
                servertime = DateTime.Parse(command.Parameters["@currtime"].Value.ToString());
                OpenDatabase(false);
                return servertime;
            }
            catch (Exception ex)
            {
                WriteErrorLogs(ex.ToString());
                AlertMsgShow(ex.Message);
                return servertime;
            }

        }

        /// <summary>
        /// ServerDatabaseIO方法:使用DbDataReader进行数据获取;
        /// 返回DbDataReader后若完成操作,需要执行DbDataReader.Close()方法
        /// 且需要执行conn.Close()-->OpenDatabase(false);
        /// </summary>
        /// <param name="queryString">需要查询的条件</param>
        /// <returns>DbDataReader</returns>
        public override DbDataReader getDbDataReader(string queryString, CommandType cmdType = CommandType.Text)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(queryString, conn);
                cmd.CommandType = cmdType;
                OpenDatabase(true);
                SqlDataReader reader = cmd.ExecuteReader();                
                return reader;
            }
            catch (Exception ex)
            {
                WriteErrorLogs(ex.ToString());
                AlertMsgShow(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// 返回执行命令后的第一行第一列资料 object
        /// </summary>
        /// <param name="queryString">命令字符串</param>
        /// <param name="cmdType">cmdType</param>
        /// <returns>-1表示失败</returns>
        public override object getDbCmdExecuteScalar(string queryString, CommandType cmdType = CommandType.Text)
        {
            object obj = new object();
            try
            {
                SqlCommand cmd = new SqlCommand(queryString, conn);
                cmd.CommandText = queryString;
                cmd.CommandType = cmdType;
                OpenDatabase(true);
                obj = cmd.ExecuteScalar();
                if (obj ==null)
                {
                    obj="Not Found Data";
                }
                return obj;
            }
            catch (Exception ex)
            {
                WriteErrorLogs(ex.ToString());
                AlertMsgShow(ex.Message);
                return -1;
            }
        }

        /// <summary>
        /// 返回执行命令后的受影响的行数
        /// </summary>
        /// <param name="queryString">命令字符串</param>
        /// <param name="cmdType">cmdType</param>
        /// <returns>结果数目:int;如为-1则表示执行失败</returns>
        public override int getDbCmdExecuteNonQuery(string queryString, CommandType cmdType = CommandType.Text)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(queryString, conn);
                cmd.CommandText = queryString;
                cmd.CommandType = cmdType;
                OpenDatabase(true);

                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                WriteErrorLogs(ex.ToString());
                AlertMsgShow(ex.Message);
                return -1;
            }
        }

        public override DataSet ExecuteDS(string sqlStr)
        {
            DataSet ds = new DataSet();
            OpenDatabase(true);
            SqlDataAdapter sda = new SqlDataAdapter(sqlStr, conn);
            sda.Fill(ds);
            return ds;
        }

        /// <summary>
        /// 向目标数据库复制数据
        /// </summary>
        /// <param name="server","database","uid","password">目标数据库信息</param>
        /// <param name="tableName">表名</param>
        public override void BulkCopyTo(string server, string database, string uid, string password, string tableName)
        {
            uid = SetPWDCode(uid, true, 4);
            password = SetPWDCode(password, true, 4);
            string connectionString = "Server=" + server + ";Database=" + database + ";User Id=" + uid + ";Password=" + password;
            SqlConnection destinationConnector = new SqlConnection(connectionString);

            OpenDatabase(true);
            destinationConnector.Open();

            SqlTransaction tr;
            tr = destinationConnector.BeginTransaction(IsolationLevel.RepeatableRead);
            SqlBulkCopy bulkData = new SqlBulkCopy(destinationConnector, SqlBulkCopyOptions.KeepIdentity, tr);
            bulkData.DestinationTableName = tableName;

            //读取源数据库表数据
            SqlCommand cmd = new SqlCommand("SELECT * FROM " + tableName, conn);
            SqlDataReader reader = cmd.ExecuteReader();

            try
            {
                bulkData.WriteToServer(reader);
                tr.Commit();
            }
            catch (Exception ex)
            {
                tr.Rollback();
                AlertMsgShow(ex.Message);
                WriteErrorLogs(ex.ToString());                
            }
            finally
            {
                bulkData.Close();
                destinationConnector.Close();
                conn.Close();
            }
        }
    }

    public class LocalDatabaseIO : DataIO //本地数据库子类 2个公共的方法!
    {
        protected OleDbConnection conn;
        /// <summary>
        /// WriteErrorLogs 默认是在UploadFile文件中
        /// </summary>        
        /// <param name="ss">需要写入的字符串信息</param>
        /// <param name="fileName">文件名若不指定则写入默认指定文档:AccdbChangeErrorLogs.txt</param>
        /// <param name="ssFilePath">ssFilePath默认为空路径,系统将分配至UploadFile文件夹</param>
        public override void WriteErrorLogs(string ss, string fileName = "", string ssFilePath = "")
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(ss))
                {
                    if (string.IsNullOrWhiteSpace(fileName))
                    {
                        fileName = "AccdbChangeErrorLogs.txt";
                    }

                    if (string.IsNullOrWhiteSpace(ssFilePath))
                    {
                        if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/") + "UploadFile"))
                        {
                            Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/") + "UploadFile");
                        }
                        ssFilePath = HttpContext.Current.Server.MapPath("~/") + "UploadFile";
                    }
                    else
                    {
                        if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/") + ssFilePath))
                        {
                            Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/") + ssFilePath);
                        }
                        ssFilePath = HttpContext.Current.Server.MapPath("~/") + ssFilePath;
                    }

                    System.IO.FileStream fs = new System.IO.FileStream(ssFilePath + "/" + fileName, System.IO.FileMode.Append);
                    System.IO.StreamWriter sw = new System.IO.StreamWriter(fs, Encoding.Default);
                    sw.WriteLine("=====Error=====\r\n" +
                        DateTime.Now.ToString() + "-->" + System.Environment.UserName + ":" + System.Environment.MachineName
                        + ";IP:" + HttpContext.Current.Request.UserHostAddress
                        + ";Browser:" + HttpContext.Current.Request.Browser.Browser + "\r\n" + ss);
                    sw.Close();
                    fs.Close();
                }
            }
            catch
            { }
        }
        
        public LocalDatabaseIO(string AccessFilePath)
        {
            string strConn = "";
            if (AccessFilePath.ToUpper().Contains(".accdb".ToUpper()))
            {
                strConn = "Provider = Microsoft.ACE.OLEDB.12.0;" + @"Data Source=" + AccessFilePath;
            }
            else
            {
                strConn = "Provider=Microsoft.Jet.OleDb.4.0;" + @"Data Source=" + AccessFilePath;
            }
            conn = new OleDbConnection(strConn);
        }

        /// <summary>
        /// 打开或关闭数据连接
        /// </summary>
        /// <param name="Swith">true=打开;false=关闭</param>
        /// <returns>true=操作成功;否则失败</returns>
        public override bool OpenDatabase(bool state)
        {
            bool result = false;
            try
            {
                if (state)
                {
                    if (conn == null) conn.Open();
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }
                }
                else
                {
                    if (conn != null && conn.State != ConnectionState.Closed)   //140625_0
                    {
                        conn.Close();
                    }
                }
                result = true;
                return result;
            }
            catch (Exception ex)
            {
                WriteErrorLogs(ex.ToString());
                AlertMsgShow(ex.Message);
                return result;
            }
        }

        public override DataTable GetDataTable(string sqlCmd, string StrTableName)
        {
            OleDbDataAdapter da = new OleDbDataAdapter(sqlCmd, conn);
            DataTable mydt = new DataTable(StrTableName);
            try
            {

                if (conn == null) conn.Open();
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                da.Fill(mydt);
            }
            catch (Exception ex)
            {
                WriteErrorLogs(ex.ToString());
                AlertMsgShow(ex.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return mydt;
        }

        /// <summary>
        /// 返回服务器当前的时间
        /// </summary>
        /// <returns></returns>
        public override DateTime GetCurrTime()
        {
            return DateTime.Now; //默认为本机时间,若获取服务器时间成功则为服务器时间!
        }
        /// <summary>
        /// LocalDatabaseIO方法:使用DbDataReader进行数据获取;
        /// 返回DbDataReader后若完成操作,需要执行DbDataReader.Close()方法
        /// 且需要执行conn.Close()-->OpenDatabase(false);
        /// </summary>
        /// <param name="queryString">需要查询的条件</param>
        /// <returns>DbDataReader</returns>
        public override DbDataReader getDbDataReader(string queryString, CommandType cmdType = CommandType.Text)
        {
            try
            {
                OleDbCommand cmd = new OleDbCommand(queryString,conn);
                cmd.CommandText = queryString;
                cmd.CommandType = cmdType;
                OpenDatabase(true);
                OleDbDataReader reader = cmd.ExecuteReader();
                
                return reader;
            }
            catch (Exception ex)
            {
                WriteErrorLogs(ex.ToString());
                AlertMsgShow(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// 返回执行命令后的第一行第一列资料 object
        /// </summary>
        /// <param name="queryString">命令字符串</param>
        /// <param name="cmdType">cmdType</param>
        /// <returns>-1表示失败</returns>
        public override object getDbCmdExecuteScalar(string queryString, CommandType cmdType = CommandType.Text)
        {
            object obj = new object();
            try
            {
                OleDbCommand cmd = new OleDbCommand(queryString, conn);
                cmd.CommandText = queryString;
                cmd.CommandType = cmdType;
                OpenDatabase(true);
                obj = cmd.ExecuteScalar();
                if (obj ==null)
                {
                    obj="Not Found Data";
                }
                return obj;
            }
            catch (Exception ex)
            {
                AlertMsgShow(ex.Message);
                WriteErrorLogs(ex.ToString());
                return -1;
            }
        }

        /// <summary>
        /// 返回执行命令后的受影响的行数
        /// </summary>
        /// <param name="queryString">命令字符串</param>
        /// <param name="cmdType">cmdType</param>
        /// <returns>结果数目:int;如为-1则表示执行失败</returns>
        public override int getDbCmdExecuteNonQuery(string queryString, CommandType cmdType = CommandType.Text)
        {
            try
            {
                OleDbCommand cmd = new OleDbCommand(queryString, conn);
                cmd.CommandText = queryString;
                cmd.CommandType = cmdType;
                OpenDatabase(true);

                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                AlertMsgShow(ex.Message);
                WriteErrorLogs(ex.ToString());
                return -1;
            }
        }
    }

    //以上为完全的公共部分!++++++++++++++++++++++++++++++++++++++
}


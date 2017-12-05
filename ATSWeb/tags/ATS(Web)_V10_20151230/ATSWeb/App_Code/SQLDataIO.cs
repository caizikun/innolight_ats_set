using System;
using System.Collections.Generic;

using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data.Common;
using ATSDataBase;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Linq;
using System.Data;
using System.Configuration;

namespace ATSDataBase
{
    public class SqlManager : ServerDatabaseIO
    {
        string serverName = "";
        string dbName =  "";
        string userId = "";
        string pwd = "";
        public SqlManager(string serverName)
            : base(serverName) //读取XML后配置此部分
        {
        }
        public SqlManager(string serverName, string dbName, string user, string pwd)
            : base(serverName, dbName, user, pwd)   //140722
        {

        }
        public void GetXLMInfor()
        { 
            serverName = ConfigurationManager.AppSettings["ServerName"].ToString();
         dbName = ConfigurationManager.AppSettings["DbName"].ToString();
         userId = ConfigurationManager.AppSettings["UserId"].ToString();
         pwd = ConfigurationManager.AppSettings["Pwd"].ToString();
        }
        public override long GetLastInsertData(string TableName)
        {
            long myValue = 0;
            try
            {
                if (conn == null) conn.Open();
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                string sqlCMD1 = "Select count(id) from " + TableName;
                SqlCommand mySQLcmd1 = new SqlCommand(sqlCMD1, conn);
                long myValue1 = Convert.ToInt64(mySQLcmd1.ExecuteScalar());

                string sqlCMD = "Select Ident_Current('" + TableName + "')";
                SqlCommand mySQLcmd = new SqlCommand(sqlCMD, conn);

                //141021_1
                string ss = mySQLcmd.ExecuteScalar().ToString();
                long myCurrIdent = 0;
                if (ss.Length > 0)
                {
                    myCurrIdent = Convert.ToInt64(ss);
                }
                else
                {
                    string sqlCMD2 = "Select max(id) from " + TableName;
                    SqlCommand mySQLcmd2 = new SqlCommand(sqlCMD2, conn);
                    string ss2 = mySQLcmd2.ExecuteScalar().ToString();
                    if (ss2.Length > 0)
                    {
                        myCurrIdent = Convert.ToInt64(ss2);
                    }
                }
                //--------

                if (myCurrIdent > 1)
                {
                    myValue = myCurrIdent;
                }
                else if (myValue1 > 0 && myCurrIdent == 1)
                {
                    myValue = myCurrIdent;
                }
                else
                {
                    myValue = 0;
                }

                return myValue;
            }
            catch (Exception ex)
            {
                WriteErrorLogs(ex.ToString());
                AlertMsgShow(ex.Message); return myValue;
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
            }
        }

        long GetLastInsertData(string TableName, SqlTransaction mytr)
        {
            long myValue = 0;
            try
            {
                string sqlCMD1 = "Select count(id) from " + TableName;
                SqlCommand mySQLcmd1 = new SqlCommand(sqlCMD1, conn);
                mySQLcmd1.Transaction = mytr;
                long myValue1 = Convert.ToInt64(mySQLcmd1.ExecuteScalar());

                string sqlCMD = "Select Ident_Current('" + TableName + "')";
                SqlCommand mySQLcmd = new SqlCommand(sqlCMD, conn);
                mySQLcmd.Transaction = mytr;
                string ss = mySQLcmd.ExecuteScalar().ToString();
                long myCurrIdent = 0;
                if (ss.Length > 0)
                {
                    myCurrIdent = Convert.ToInt64(ss);
                }
                else
                {
                    string sqlCMD2 = "Select max(id) from " + TableName;
                    SqlCommand mySQLcmd2 = new SqlCommand(sqlCMD2, conn);
                    mySQLcmd2.Transaction = mytr;
                    string ss2 = mySQLcmd2.ExecuteScalar().ToString();
                    if (ss2.Length > 0)
                    {
                        myCurrIdent = Convert.ToInt64(ss2);
                    }
                }

                if (myCurrIdent > 1)
                {
                    myValue = myCurrIdent;
                }
                else if (myValue1 > 0 && myCurrIdent == 1)
                {
                    myValue = myCurrIdent;
                }
                else
                {
                    myValue = 0;
                }

                return myValue;
            }
            catch (Exception ex)
            {
                WriteErrorLogs(ex.ToString());
                AlertMsgShow(ex.Message); return myValue;
            }
        }

        public override bool UpdateDT()
        {
            WriteErrorLogs("Not support UpdateDT() method~");
            return false;
        }

        public override bool UpdateDataTable(string SQLCmd, DataTable NewChangeDT)
        {
            bool result = false;

            SqlTransaction tr;

            if (conn == null) conn.Open();
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            tr = conn.BeginTransaction(IsolationLevel.RepeatableRead);

            try
            {
                SqlCommand cm = new SqlCommand(SQLCmd, conn);
                SqlDataAdapter da = new SqlDataAdapter(cm);
                SqlCommandBuilder cb = new SqlCommandBuilder(da);
                DataTable mydt = new DataTable();
                da.SelectCommand.Transaction = tr;
                mydt = NewChangeDT.GetChanges();
                if (mydt != null)
                {
                    da.Update(mydt);
                }
                tr.Commit();
                result = true;
                WriteErrorLogs(DTGetChanges(NewChangeDT), "dtchanges.txt");    //150605 Debug使用获取修改的资料
                return result;
            }
            catch (Exception ex)
            {
                try
                {
                    //141031_00----------
                    if (ex.Message.ToUpper().Contains("违反并发性"))
                    {
                        WriteErrorLogs("Update data failed:\n" + "Maybe Other users have already deleted the target data records\n" + ex.Message);
                    }
                    else
                    {
                        WriteErrorLogs(ex.ToString());
                    }
                    AlertMsgShow(ex.Message);
                    //WriteErrorLogs(ex.ToString());
                    //-------------------
                    tr.Rollback();
                    if (conn.State.ToString().ToUpper() == "OPEN") conn.Close();
                    return result;
                }
                catch (System.Exception TransactionEx)
                {
                    //Handle Exception
                    WriteErrorLogs(TransactionEx.ToString());
                    if (conn.State.ToString().ToUpper() == "OPEN") conn.Close();
                    return result;
                }
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        public override bool UpdateDataTable(string SQLCmd, DataTable NewChangeDT, out long LstID)
        {
            bool result = false;
            LstID = -1;
            SqlTransaction tr;

            if (conn == null) conn.Open();
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            tr = conn.BeginTransaction(IsolationLevel.RepeatableRead);

            try
            {
                SqlCommand cm = new SqlCommand(SQLCmd, conn);
                SqlDataAdapter da = new SqlDataAdapter(cm);
                SqlCommandBuilder cb = new SqlCommandBuilder(da);
                DataTable mydt = new DataTable();
                da.SelectCommand.Transaction = tr;
                mydt = NewChangeDT.GetChanges();
                if (mydt != null)
                {
                    da.Update(mydt);
                    LstID = GetLastInsertData(NewChangeDT.TableName, tr);
                }
                tr.Commit();
                result = true;
                 WriteErrorLogs(DTGetChanges(NewChangeDT), "dtchanges.txt");    //150605 Debug使用获取修改的资料
                return result;
            }
            catch (Exception ex)
            {
                try
                {
                    //141031_00----------
                    if (ex.Message.ToUpper().Contains("违反并发性"))
                    {
                        WriteErrorLogs("Update data failed:\n" + "Maybe Other users have already deleted the target data records\n" + ex.Message);
                    }
                    else
                    {
                        WriteErrorLogs(ex.ToString());
                    }
                    AlertMsgShow(ex.Message);
                    //WriteErrorLogs(ex.ToString());
                    //-------------------
                    tr.Rollback();
                    if (conn.State.ToString().ToUpper() == "OPEN") conn.Close();
                    return result;
                }
                catch (System.Exception TransactionEx)
                {
                    //Handle Exception
                    WriteErrorLogs(TransactionEx.ToString());
                    if (conn.State.ToString().ToUpper() == "OPEN") conn.Close();
                    return result;
                }
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        public override long GetPID(string sqlCmd)
        {
            try
            {

                if (conn == null) conn.Open();
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                SqlCommand getPID = new SqlCommand(sqlCmd, conn);
                return Convert.ToInt64(getPID.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                WriteErrorLogs(ex.ToString());
                return -1;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        public override bool BlnISExistTable(string tabName)
        {
            try
            {
                if (conn == null) conn.Open();          //140625_0
                if (conn.State != ConnectionState.Open) //140625_0
                {
                    conn.Open();
                }

                bool existTab = false;

                DataTable dt = conn.GetSchema("Tables");
                int n = dt.Rows.Count;
                int m = dt.Columns.IndexOf("TABLE_NAME");

                string[] tabsName = new string[n];
                for (int i = 0; i < n; i++)
                {
                    tabsName[i] = dt.Rows[i].ItemArray.GetValue(m).ToString();
                    if (tabName == tabsName[i])
                    {
                        existTab = true;
                        break;
                    }
                }

                if (existTab)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                WriteErrorLogs("Can't find this table:  " + tabName + "\n" + ex.Message);
                return false;
            }
        }

        public override string[] GetCurrTablesName(string ServerName, string DBName, string userName, string pwd)
        {
            string strConnection = @"Data Source=" + ServerName + ";Initial Catalog=" + DBName + ";Provider=SQLOLEDB.1;user id = " + userName + ";password=" + pwd + ";";
            //string strConnection = "Provider = Microsoft.ACE.OLEDB.12.0;" + @"Data Source=" + Login.AccessFilePath; 
            OleDbConnection myConn = new OleDbConnection(strConnection);
            try
            {
                if (myConn == null) myConn.Open();
                if (myConn.State != ConnectionState.Open)
                {
                    myConn.Open();
                }
                //DataTable cnSch = myAccessIO.Conn.GetSchema("Tables");

                DataTable cnSch = myConn.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });

                int n = cnSch.Rows.Count;
                int m = cnSch.Columns.IndexOf("TABLE_NAME");

                string[] tabsName = new string[n];
                for (int i = 0; i < n; i++)
                {
                    tabsName[i] = cnSch.Rows[i].ItemArray.GetValue(m).ToString();
                }
                return tabsName;
            }

            catch (Exception ex)
            {
                WriteErrorLogs(ex.Message);
                return null;
            }
            finally
            {
                if (myConn.State.ToString().ToUpper() == "OPEN")
                    myConn.Close();
            }
        }

        public override string[] GetCurrTablesName(string Accesspath)
        {
            string strConnection = "";
            if (Accesspath.ToUpper().Contains(".accdb".ToUpper()))
            {
                strConnection = "Provider = Microsoft.ACE.OLEDB.12.0;" + @"Data Source=" + Accesspath;
            }
            else
            {
                strConnection = "Provider=Microsoft.Jet.OleDb.4.0;" + @"Data Source=" + Accesspath;
            }
            OleDbConnection myConn = new OleDbConnection(strConnection);
            try
            {
                if (myConn == null) myConn.Open();
                if (myConn.State != ConnectionState.Open)
                {
                    myConn.Open();
                }
                //DataTable cnSch = myAccessIO.Conn.GetSchema("Tables");

                DataTable cnSch = myConn.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });

                int n = cnSch.Rows.Count;
                int m = cnSch.Columns.IndexOf("TABLE_NAME");

                string[] tabsName = new string[n];
                for (int i = 0; i < n; i++)
                {
                    tabsName[i] = cnSch.Rows[i].ItemArray.GetValue(m).ToString();
                }
                return tabsName;
            }

            catch (Exception ex)
            {
                WriteErrorLogs(ex.Message);
                return null;
            }
            finally
            {
                if (myConn.State.ToString().ToUpper() == "OPEN")
                    myConn.Close();
            }
        }

        public override bool UpdateDataTable(string SQLCmd, DataTable NewChangeDT, bool IsAddNewData)
        {
            bool result = false;

            if (conn == null) conn.Open();
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            SqlTransaction tr;
            tr = conn.BeginTransaction(IsolationLevel.RepeatableRead);

            try
            {
                SqlCommand cm = new SqlCommand(SQLCmd, conn);
                SqlDataAdapter da = new SqlDataAdapter(cm);
                SqlCommandBuilder cb = new SqlCommandBuilder(da);
                DataTable mydt = new DataTable();
                da.SelectCommand.Transaction = tr;

                if (IsAddNewData)
                {
                    for (int i = 0; i < NewChangeDT.Rows.Count; i++)
                    {
                        NewChangeDT.Rows[i].SetAdded();
                    }
                }
                if (NewChangeDT.GetChanges() != null)
                {
                    da.Update(NewChangeDT);
                }

                tr.Commit();

                result = true;
                 WriteErrorLogs(DTGetChanges(NewChangeDT), "dtchanges.txt");    //150605 Debug使用获取修改的资料
                return result;
            }
            catch (Exception ex)
            {
                try
                {
                    //141031_00----------
                    if (ex.Message.ToUpper().Contains("违反并发性"))
                    {
                        WriteErrorLogs("Update data failed:\n" + "Maybe Other users have already deleted the target data records\n" + ex.Message);
                    }
                    else
                    {
                        WriteErrorLogs(ex.ToString());
                    }
                    AlertMsgShow(ex.Message);
                    //WriteErrorLogs(ex.ToString());
                    //-------------------
                    tr.Rollback();
                    if (conn.State.ToString().ToUpper() == "OPEN") conn.Close();
                    return result;
                }
                catch (System.Exception TransactionEx)
                {
                    //Handle Exception
                    WriteErrorLogs(TransactionEx.ToString());
                    if (conn.State.ToString().ToUpper() == "OPEN") conn.Close();
                    return result;
                }
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        public override bool UpdateOperateLog(string loginID, string blockTypeName, string tracingInfo, DataTable dt, string dtName, string sqlCmd = "Select * from OperationLogs where ID=-1") //150414 Add
        {
            bool result = false;
            string[] pLogs = new string[3];
            string[] pOpType = new string[3];
            if (conn == null) conn.Open();
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }

            try
            {
                CommCtrl pCommCtrl = new CommCtrl();
                SqlCommand cm = new SqlCommand(sqlCmd, conn);
                SqlDataAdapter da = new SqlDataAdapter(cm);
                SqlCommandBuilder cb = new SqlCommandBuilder(da);
                DataTable mydt = new DataTable();
                da.Fill(mydt);
                DataTable addDt = dt.GetChanges(DataRowState.Added);
                DataTable delelteDt = dt.GetChanges(DataRowState.Deleted);
                DataTable modifyDt = dt.GetChanges(DataRowState.Modified);
                if (addDt != null)
                {
                    pLogs = pCommCtrl.getOpLogs(addDt, dtName, out pOpType);

                    if (pOpType[0].Length > 0)
                    {
                        DataRow dr = mydt.NewRow();
                        dr["PID"] = loginID;
                        dr["ModifyTime"] = GetCurrTime();
                        dr["BlockType"] = blockTypeName;
                        dr["OpType"] = pOpType[0];
                        dr["TracingInfo"] = tracingInfo;
                        dr["DetailLogs"] = pLogs[0];
                        mydt.Rows.Add(dr);
                    }
                }

                if (delelteDt != null)
                {
                    pLogs = pCommCtrl.getOpLogs(delelteDt, dtName, out pOpType);
                    if (pOpType[1].Length > 0)
                    {
                        DataRow dr = mydt.NewRow();
                        dr["PID"] = loginID;
                        dr["ModifyTime"] = GetCurrTime();
                        dr["BlockType"] = blockTypeName;
                        dr["Optype"] = pOpType[1];
                        dr["TracingInfo"] = tracingInfo;
                        dr["DetailLogs"] = pLogs[1];
                        mydt.Rows.Add(dr);
                    }
                }

                if (modifyDt != null)
                {
                    pLogs = pCommCtrl.getOpLogs(modifyDt, dtName, out pOpType);
                    if (pOpType[2].Length > 0)
                    {
                        DataRow dr = mydt.NewRow();
                        dr["PID"] = loginID;
                        dr["ModifyTime"] = GetCurrTime();
                        dr["BlockType"] = blockTypeName;
                        dr["Optype"] = pOpType[2];
                        dr["TracingInfo"] = tracingInfo;
                        dr["DetailLogs"] = pLogs[2];
                        mydt.Rows.Add(dr);
                    }
                }

                if (mydt.GetChanges() != null)
                {
                    UpdateDataTable(sqlCmd, mydt);
                }

                result = true;

                return result;
            }
            catch (Exception ex)
            {
                #region Err
                try
                {
                    //141031_00----------
                    if (ex.Message.ToUpper().Contains("违反并发性"))
                    {
                        WriteErrorLogs("Update data failed:\n" + "Maybe Other users have already deleted the target data records\n" + ex.Message);
                    }
                    else
                    {
                        WriteErrorLogs(ex.ToString());
                    }
                    AlertMsgShow(ex.Message);
                    //WriteErrorLogs(ex.ToString());
                    if (conn.State.ToString().ToUpper() == "OPEN") conn.Close();
                    return result;
                }
                catch (System.Exception TransactionEx)
                {
                    //Handle Exception
                    WriteErrorLogs(TransactionEx.ToString());
                    if (conn.State.ToString().ToUpper() == "OPEN") conn.Close();
                    return result;
                }
                #endregion
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        #region Pro_GlobalAllEquipmentList
        //存储过程 Pro_GlobalAllEquipmentList			
        //保存GlobalAllEquipmentList的资料			
        //参数	类型	方向	描述
        //@ID	int	OUTPUT	
        //@ItemName	nvarchar(30)		
        //@ItemType	nvarchar(30)		
        //@ItemDescription	nvarchar(50)		
        //@RowState	tinyint		
        //@OPItemName	nvarchar(100)		
        //@OPChildItemName	nvarchar(100)		
        //@OPlogPID	int		
        //@TracingInfo	nvarchar(max)		
        //@myErr	int	OUTPUT	
        private int Pro_GlobalAllEquipmentList(int id, string itemName, string showName, string itemType, string itemDescription,
            byte rowStatus, int opLogPID, string TracingInfo, out int myErr)
        {
            myErr = -1; string errMsg = ""; SqlCommand command = new SqlCommand("Pro_GlobalAllEquipmentList", conn);            
            try
            {
                OpenDatabase(true);
                //执行存储过程 
                //SqlCommand command = new SqlCommand("Pro_GlobalAllEquipmentList", conn);
                //说明命令要执行的是存储过程     
                command.CommandType = CommandType.StoredProcedure;
                //向存储过程中传递参数   
                command.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@ItemName", SqlDbType.NVarChar, 30));
                command.Parameters.Add(new SqlParameter("@ShowName", SqlDbType.NVarChar, 30));
                command.Parameters.Add(new SqlParameter("@ItemType", SqlDbType.NVarChar, 30));
                command.Parameters.Add(new SqlParameter("@ItemDescription", SqlDbType.NVarChar, 50));

                command.Parameters.Add(new SqlParameter("@RowState", SqlDbType.TinyInt));


                command.Parameters.Add(new SqlParameter("@OPlogPID", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@TracingInfo", SqlDbType.NVarChar, 4000));
                command.Parameters.Add(new SqlParameter("@myErr", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@myErrMsg", SqlDbType.NVarChar, 4000));
                command.UpdatedRowSource = UpdateRowSource.None;

                command.Parameters["@ID"].Value = id;
                command.Parameters["@ItemName"].Value = itemName;
                command.Parameters["@ShowName"].Value = showName; 
                command.Parameters["@ItemType"].Value = itemType;
                command.Parameters["@ItemDescription"].Value = itemDescription;

                command.Parameters["@RowState"].Value = rowStatus;


                command.Parameters["@OPlogPID"].Value = opLogPID;
                command.Parameters["@TracingInfo"].Value = TracingInfo;
                command.Parameters["@ID"].Direction = ParameterDirection.InputOutput;
                command.Parameters["@myErr"].Direction = ParameterDirection.Output;
                command.Parameters["@myErrMsg"].Direction = ParameterDirection.Output;
                command.ExecuteNonQuery();
                id = Convert.ToInt32(command.Parameters["@ID"].Value);
                myErr = Convert.ToInt32(command.Parameters["@myErr"].Value);
                errMsg = command.Parameters["@myErrMsg"].Value.ToString();    //OpenDatabase(false);
                if (myErr == 0)
                {
                    return id;
                }
                else
                {
                    return myErr;
                }
            }
            catch (Exception ex)
            {
                if (command.Parameters["@myErrMsg"].Value != null)
                {
                    errMsg = command.Parameters["@myErrMsg"].Value.ToString();
                    myErr = Convert.ToInt32(command.Parameters["@myErr"].Value);
                    WriteErrorLogs("Err:" + myErr + ";" + errMsg);                  
                }
                else
                {
                    WriteErrorLogs(ex.ToString());
                    errMsg = ex.ToString();
                }

                AlertMsgShow("Error:" + myErr + ";" + errMsg);
                return myErr;
            }
            finally
            {
                OpenDatabase(false);
            }
        }
        #endregion

        #region Pro_GlobalAllEquipmentParamterList
        //存储过程 Pro_GlobalAllEquipmentParamterList			
        //保存_GlobalAllEquipmentParamterList的资料			
        //参数	类型	方向	描述
        //@ID	int	OUTPUT	
        //@PID int ,			
        //@ItemName	nvarchar(30)		
        //@ItemType	nvarchar(10)		
        //@ItemValue	nvarchar(255)		
        //@NeedSelect	bit		
        //@Optionalparams	nvarchar(max)		
        //@ItemDescription	nvarchar(200)		
        //@RowState	tinyint		
        //@OPlogPID	int		
        //@TracingInfo	nvarchar(max)		
        //@myErr	int	OUTPUT	
        private int Pro_GlobalAllEquipmentParamterList(int id, int pid, string itemName, string showName, string itemType, string itemValue, bool needSelect, string optionalparams, string itemDescription,
            byte rowStatus, int opLogPID, string TracingInfo, out int myErr)
        {
            myErr = -1; string errMsg = ""; SqlCommand command = new SqlCommand("Pro_GlobalAllEquipmentParamterList", conn);
            try
            {
                OpenDatabase(true);
                //执行存储过程 
                //SqlCommand command = new SqlCommand("Pro_GlobalAllEquipmentParamterList ", conn);
                //说明命令要执行的是存储过程     
                command.CommandType = CommandType.StoredProcedure;
                //向存储过程中传递参数   
                command.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@PID", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@ItemName", SqlDbType.NVarChar, 30));
                command.Parameters.Add(new SqlParameter("@ShowName", SqlDbType.NVarChar, 30));
                command.Parameters.Add(new SqlParameter("@ItemType", SqlDbType.NVarChar, 10));
                command.Parameters.Add(new SqlParameter("@ItemValue", SqlDbType.NVarChar, 255));
                command.Parameters.Add(new SqlParameter("@NeedSelect", SqlDbType.Bit));
                command.Parameters.Add(new SqlParameter("@Optionalparams", SqlDbType.NVarChar, 255));
                command.Parameters.Add(new SqlParameter("@ItemDescription", SqlDbType.NVarChar, 500));

                command.Parameters.Add(new SqlParameter("@RowState", SqlDbType.TinyInt));


                command.Parameters.Add(new SqlParameter("@OPlogPID", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@TracingInfo", SqlDbType.NVarChar, 4000));
                command.Parameters.Add(new SqlParameter("@myErr", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@myErrMsg", SqlDbType.NVarChar, 4000));
                command.UpdatedRowSource = UpdateRowSource.None;

                command.Parameters["@ID"].Value = id;
                command.Parameters["@PID"].Value = pid;
                command.Parameters["@ItemName"].Value = itemName;
                command.Parameters["@ShowName"].Value = showName;
                command.Parameters["@ItemType"].Value = itemType;
                command.Parameters["@ItemValue"].Value = itemValue;
                command.Parameters["@NeedSelect"].Value = needSelect;
                command.Parameters["@Optionalparams"].Value = optionalparams;
                command.Parameters["@ItemDescription"].Value = itemDescription;

                command.Parameters["@RowState"].Value = rowStatus;


                command.Parameters["@OPlogPID"].Value = opLogPID;
                command.Parameters["@TracingInfo"].Value = TracingInfo;
                command.Parameters["@ID"].Direction = ParameterDirection.InputOutput;
                command.Parameters["@myErr"].Direction = ParameterDirection.Output;
                command.Parameters["@myErrMsg"].Direction = ParameterDirection.Output;
                command.ExecuteNonQuery();
                id = Convert.ToInt32(command.Parameters["@ID"].Value);
                myErr = Convert.ToInt32(command.Parameters["@myErr"].Value);
                errMsg = command.Parameters["@myErrMsg"].Value.ToString();    //OpenDatabase(false);
                if (myErr == 0)
                {
                    return id;
                }
                else
                {
                    return myErr;
                }
            }
            catch (Exception ex)
            {
                if (command.Parameters["@myErrMsg"].Value != null)
                {
                    errMsg = command.Parameters["@myErrMsg"].Value.ToString();
                    myErr = Convert.ToInt32(command.Parameters["@myErr"].Value);
                    WriteErrorLogs("Err:" + myErr + ";" + errMsg);
                }
                else
                {
                    WriteErrorLogs(ex.ToString());
                    errMsg = ex.ToString();
                }

                AlertMsgShow("Error:" + myErr + ";" + errMsg);
                return myErr;
            }
            finally
            {
                OpenDatabase(false);
            }
        }
        #endregion

        #region Pro_GlobalAllAppModelList
        //存储过程 Pro_GlobalAllAppModelList			
        //保存_GlobalAllAppModelList的资料			
        //参数	类型	方向	描述
        //@ID	int	OUTPUT	
        //@ItemName	nvarchar(30)		
        //@ItemDescription	nvarchar(50)		
        //@RowState	tinyint		
        //@OPlogPID	int		
        //@TracingInfo	nvarchar(max)		
        //@myErr	int	OUTPUT	
        private int Pro_GlobalAllAppModelList(int id, string itemName, string itemDescription,
            byte rowStatus, int opLogPID, string TracingInfo, out int myErr)
        {
            myErr = -1; string errMsg = ""; SqlCommand command = new SqlCommand("Pro_GlobalAllAppModelList", conn);
            try
            {
                OpenDatabase(true);
                //执行存储过程 
                //SqlCommand command = new SqlCommand("Pro_GlobalAllAppModelList", conn);
                //说明命令要执行的是存储过程     
                command.CommandType = CommandType.StoredProcedure;
                //向存储过程中传递参数   
                command.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@ItemName", SqlDbType.NVarChar, 30));
                command.Parameters.Add(new SqlParameter("@ItemDescription", SqlDbType.NVarChar, 50));

                command.Parameters.Add(new SqlParameter("@RowState", SqlDbType.TinyInt));


                command.Parameters.Add(new SqlParameter("@OPlogPID", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@TracingInfo", SqlDbType.NVarChar, 4000));
                command.Parameters.Add(new SqlParameter("@myErr", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@myErrMsg", SqlDbType.NVarChar, 4000));
                command.UpdatedRowSource = UpdateRowSource.None;

                command.Parameters["@ID"].Value = id;
                command.Parameters["@ItemName"].Value = itemName;
                command.Parameters["@ItemDescription"].Value = itemDescription;

                command.Parameters["@RowState"].Value = rowStatus;


                command.Parameters["@OPlogPID"].Value = opLogPID;
                command.Parameters["@TracingInfo"].Value = TracingInfo;
                command.Parameters["@ID"].Direction = ParameterDirection.InputOutput;
                command.Parameters["@myErr"].Direction = ParameterDirection.Output;
                command.Parameters["@myErrMsg"].Direction = ParameterDirection.Output;
                command.ExecuteNonQuery();
                id = Convert.ToInt32(command.Parameters["@ID"].Value);
                myErr = Convert.ToInt32(command.Parameters["@myErr"].Value);
                errMsg = command.Parameters["@myErrMsg"].Value.ToString();    //OpenDatabase(false);
                if (myErr == 0)
                {
                    return id;
                }
                else
                {
                    return myErr;
                }
            }
            catch (Exception ex)
            {
                if (command.Parameters["@myErrMsg"].Value != null)
                {
                    errMsg = command.Parameters["@myErrMsg"].Value.ToString();
                    myErr = Convert.ToInt32(command.Parameters["@myErr"].Value);
                    WriteErrorLogs("Err:" + myErr + ";" + errMsg);
                }
                else
                {
                    WriteErrorLogs(ex.ToString());
                    errMsg = ex.ToString();
                }

                AlertMsgShow("Error:" + myErr + ";" + errMsg);
                return myErr;
            }
            finally
            {
                OpenDatabase(false);
            }
        }
        #endregion

        #region Pro_GlobalAllTestModelList
        //存储过程 Pro_GlobalAllTestModelList			
        //保存_GlobalAllTestModelList的资料			
        //参数	类型	方向	描述
        //@ID	int	OUTPUT	
        //@PID	int		
        //@ItemName	nvarchar(30)		
        //@ShowName	nvarchar(30)		
        //@ItemDescription	nvarchar(50)		
        //@RowState	tinyint		
        //@OPlogPID	int		
        //@TracingInfo	nvarchar(max)		
        //@myErr	int	OUTPUT	
        private int Pro_GlobalAllTestModelList(int id, int pid, string itemName, string showName, string itemDescription,
            byte rowStatus, int opLogPID, string TracingInfo, out int myErr)
        {
            myErr = -1; string errMsg = ""; SqlCommand command = new SqlCommand("Pro_GlobalAllTestModelList", conn);
            try
            {
                OpenDatabase(true);
                //执行存储过程 
                //SqlCommand command = new SqlCommand("Pro_GlobalAllTestModelList", conn);
                //说明命令要执行的是存储过程     
                command.CommandType = CommandType.StoredProcedure;
                //向存储过程中传递参数   
                command.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@PID", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@ItemName", SqlDbType.NVarChar, 30));
                command.Parameters.Add(new SqlParameter("@ShowName", SqlDbType.NVarChar, 30));
                command.Parameters.Add(new SqlParameter("@ItemDescription", SqlDbType.NVarChar, 50));

                command.Parameters.Add(new SqlParameter("@RowState", SqlDbType.TinyInt));


                command.Parameters.Add(new SqlParameter("@OPlogPID", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@TracingInfo", SqlDbType.NVarChar, 4000));
                command.Parameters.Add(new SqlParameter("@myErr", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@myErrMsg", SqlDbType.NVarChar, 4000));
                command.UpdatedRowSource = UpdateRowSource.None;

                command.Parameters["@ID"].Value = id;
                command.Parameters["@PID"].Value = pid;
                command.Parameters["@ItemName"].Value = itemName;
                command.Parameters["@ShowName"].Value = showName;
                command.Parameters["@ItemDescription"].Value = itemDescription;

                command.Parameters["@RowState"].Value = rowStatus;


                command.Parameters["@OPlogPID"].Value = opLogPID;
                command.Parameters["@TracingInfo"].Value = TracingInfo;
                command.Parameters["@ID"].Direction = ParameterDirection.InputOutput;
                command.Parameters["@myErr"].Direction = ParameterDirection.Output;
                command.Parameters["@myErrMsg"].Direction = ParameterDirection.Output;
                command.ExecuteNonQuery();
                id = Convert.ToInt32(command.Parameters["@ID"].Value);
                myErr = Convert.ToInt32(command.Parameters["@myErr"].Value);
                errMsg = command.Parameters["@myErrMsg"].Value.ToString();    //OpenDatabase(false);
                if (myErr == 0)
                {
                    return id;
                }
                else
                {
                    return myErr;
                }
            }
            catch (Exception ex)
            {
                if (command.Parameters["@myErrMsg"].Value != null)
                {
                    errMsg = command.Parameters["@myErrMsg"].Value.ToString();
                    myErr = Convert.ToInt32(command.Parameters["@myErr"].Value);
                    WriteErrorLogs("Err:" + myErr + ";" + errMsg);
                }
                else
                {
                    WriteErrorLogs(ex.ToString());
                    errMsg = ex.ToString();
                }

                AlertMsgShow("Error:" + myErr + ";" + errMsg);
                return myErr;
            }
            finally
            {
                OpenDatabase(false);
            }
        }
        #endregion

        #region Pro_GlobalTestModelParamterList
        //存储过程 Pro_GlobalTestModelParamterList			
        //保存_GlobalTestModelParamterList的资料			
        //参数	类型	方向	描述
        //@ID	int	OUTPUT	
        //@PID	int		
        //@ItemName	nvarchar(30)		
        //@ItemType	nvarchar(10)		
        //@ItemValue	nvarchar(255)		
        //@NeedSelect 	bit		
        //@Optionalparams	nvarchar(max)		
        //@ItemDescription	nvarchar(200)		
        //@RowState	tinyint		
        //@OPlogPID	int		
        //@TracingInfo	nvarchar(max)		
        //@myErr	int	OUTPUT	
        private int Pro_GlobalTestModelParamterList(int id, int pid, string itemName, string showName, string itemType, string itemValue, bool needSelect, string optionalparams, string itemDescription,
            byte rowStatus, int opLogPID, string TracingInfo, out int myErr)
        {
            myErr = -1; string errMsg = ""; SqlCommand command = new SqlCommand("Pro_GlobalTestModelParamterList", conn);
            try
            {
                OpenDatabase(true);
                //执行存储过程 
                //SqlCommand command = new SqlCommand("Pro_GlobalTestModelParamterList", conn);
                //说明命令要执行的是存储过程     
                command.CommandType = CommandType.StoredProcedure;
                //向存储过程中传递参数   
                command.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@PID", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@ItemName", SqlDbType.NVarChar, 30));
                command.Parameters.Add(new SqlParameter("@ShowName", SqlDbType.NVarChar, 30));
                command.Parameters.Add(new SqlParameter("@ItemType", SqlDbType.NVarChar, 10));
                command.Parameters.Add(new SqlParameter("@ItemValue", SqlDbType.NVarChar, 255));
                command.Parameters.Add(new SqlParameter("@NeedSelect", SqlDbType.Bit));
                command.Parameters.Add(new SqlParameter("@Optionalparams", SqlDbType.NVarChar, 4000));
                command.Parameters.Add(new SqlParameter("@ItemDescription", SqlDbType.NVarChar, 200));

                command.Parameters.Add(new SqlParameter("@RowState", SqlDbType.TinyInt));


                command.Parameters.Add(new SqlParameter("@OPlogPID", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@TracingInfo", SqlDbType.NVarChar, 4000));
                command.Parameters.Add(new SqlParameter("@myErr", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@myErrMsg", SqlDbType.NVarChar, 4000));
                command.UpdatedRowSource = UpdateRowSource.None;

                command.Parameters["@ID"].Value = id;
                command.Parameters["@PID"].Value = pid;
                command.Parameters["@ItemName"].Value = itemName;
                command.Parameters["@ShowName"].Value = showName;
                command.Parameters["@ItemType"].Value = itemType;
                command.Parameters["@ItemValue"].Value = itemValue;
                command.Parameters["@NeedSelect"].Value = needSelect;
                command.Parameters["@Optionalparams"].Value = optionalparams;
                command.Parameters["@ItemDescription"].Value = itemDescription;

                command.Parameters["@RowState"].Value = rowStatus;


                command.Parameters["@OPlogPID"].Value = opLogPID;
                command.Parameters["@TracingInfo"].Value = TracingInfo;
                command.Parameters["@ID"].Direction = ParameterDirection.InputOutput;
                command.Parameters["@myErr"].Direction = ParameterDirection.Output;
                command.Parameters["@myErrMsg"].Direction = ParameterDirection.Output;
                command.ExecuteNonQuery();
                id = Convert.ToInt32(command.Parameters["@ID"].Value);
                myErr = Convert.ToInt32(command.Parameters["@myErr"].Value);
                errMsg = command.Parameters["@myErrMsg"].Value.ToString();    //OpenDatabase(false);
                if (myErr == 0)
                {
                    return id;
                }
                else
                {
                    return myErr;
                }
            }
            catch (Exception ex)
            {
                if (command.Parameters["@myErrMsg"].Value != null)
                {
                    errMsg = command.Parameters["@myErrMsg"].Value.ToString();
                    myErr = Convert.ToInt32(command.Parameters["@myErr"].Value);
                    WriteErrorLogs("Err:" + myErr + ";" + errMsg);                  
                }
                else
                {
                    WriteErrorLogs(ex.ToString());
                    errMsg = ex.ToString();
                }

                AlertMsgShow("Error:" + myErr + ";" + errMsg);
                return myErr;
            }
            finally
            {
                OpenDatabase(false);
            }
        }
        #endregion

        #region Pro_GlobalMSA
        //存储过程 Pro_GlobalMSA			
        //保存_GlobalMSA的资料			
        //参数	类型	方向	描述
        //@ID	int	OUTPUT	
        //@ItemName	nvarchar(25)		
        //@AccessInterface	nvarchar(25)		
        //@SlaveAddress	int		
        //@IgnoreFlag	bit		
        //@RowState	tinyint		
        //@OPlogPID	int		
        //@TracingInfo	nvarchar(max)		
        //@myErr	int	OUTPUT	
        private int Pro_GlobalMSA(int id, string itemName, string accessInterface, int slaveAddress, bool ignoreFlag,
            byte rowStatus, int opLogPID, string TracingInfo, out int myErr)
        {
            myErr = -1; string errMsg = ""; SqlCommand command = new SqlCommand("Pro_GlobalMSA", conn);
            try
            {
                OpenDatabase(true);
                //执行存储过程 
                //SqlCommand command = new SqlCommand("Pro_GlobalMSA ", conn);
                //说明命令要执行的是存储过程     
                command.CommandType = CommandType.StoredProcedure;
                //向存储过程中传递参数   
                command.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@ItemName", SqlDbType.NVarChar, 25));
                command.Parameters.Add(new SqlParameter("@AccessInterface", SqlDbType.NVarChar, 25));
                command.Parameters.Add(new SqlParameter("@SlaveAddress", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@IgnoreFlag", SqlDbType.Bit));

                command.Parameters.Add(new SqlParameter("@RowState", SqlDbType.TinyInt));


                command.Parameters.Add(new SqlParameter("@OPlogPID", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@TracingInfo", SqlDbType.NVarChar, 4000));
                command.Parameters.Add(new SqlParameter("@myErr", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@myErrMsg", SqlDbType.NVarChar, 4000));
                command.UpdatedRowSource = UpdateRowSource.None;

                command.Parameters["@ID"].Value = id;

                command.Parameters["@ItemName"].Value = itemName;
                command.Parameters["@AccessInterface"].Value = accessInterface;
                command.Parameters["@SlaveAddress"].Value = slaveAddress;
                command.Parameters["@IgnoreFlag"].Value = ignoreFlag;

                command.Parameters["@RowState"].Value = rowStatus;


                command.Parameters["@OPlogPID"].Value = opLogPID;
                command.Parameters["@TracingInfo"].Value = TracingInfo;

                command.Parameters["@ID"].Direction = ParameterDirection.InputOutput;
                command.Parameters["@myErr"].Direction = ParameterDirection.Output;
                command.Parameters["@myErrMsg"].Direction = ParameterDirection.Output;
                command.ExecuteNonQuery();

                id = Convert.ToInt32(command.Parameters["@ID"].Value);
                myErr = Convert.ToInt32(command.Parameters["@myErr"].Value);
                errMsg = command.Parameters["@myErrMsg"].Value.ToString();    //OpenDatabase(false);
                if (myErr == 0)
                {
                    return id;
                }
                else
                {
                    return myErr;
                }
            }
            catch (Exception ex)
            {
                if (command.Parameters["@myErrMsg"].Value != null)
                {
                    errMsg = command.Parameters["@myErrMsg"].Value.ToString();
                    myErr = Convert.ToInt32(command.Parameters["@myErr"].Value);
                    WriteErrorLogs("Err:" + myErr + ";" + errMsg);
                }
                else
                {
                    WriteErrorLogs(ex.ToString());
                    errMsg = ex.ToString();
                }

                AlertMsgShow("Error:" + myErr + ";" + errMsg);
                return myErr;
            }
            finally
            {
                OpenDatabase(false);
            }
        }
        #endregion

        #region Pro_GlobalMSADefintionInf
        //存储过程 Pro_GlobalMSADefintionInf			
        //保存_GlobalMSADefintionInf的资料			
        //参数	类型	方向	描述
        //@ID	int	OUTPUT	
        //@PID	int		
        //@FieldName	nvarchar(30)		
        //@Channel	tinyint		
        //@SlaveAddress	int		
        //@Page	tinyint		
        //@StartAddress	int		
        //@Length	tinyint		
        //@Format	nvarchar(10)		
        //@RowState	tinyint		
        //@OPlogPID	int		
        //@TracingInfo	nvarchar(max)		
        //@myErr	int	OUTPUT	
        private int Pro_GlobalMSADefintionInf(int id, int pid, string fieldName, byte channel, int slaveAddress, byte page, int startAddress, byte length, string format,
            byte rowStatus, int opLogPID, string TracingInfo, out int myErr)
        {
            myErr = -1; string errMsg = ""; SqlCommand command = new SqlCommand("Pro_GlobalMSADefintionInf", conn);
            try
            {
                OpenDatabase(true);
                //执行存储过程 
                //SqlCommand command = new SqlCommand("Pro_GlobalMSADefintionInf", conn);
                //说明命令要执行的是存储过程     
                command.CommandType = CommandType.StoredProcedure;
                //向存储过程中传递参数   
                command.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@PID", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@FieldName", SqlDbType.NVarChar, 30));
                command.Parameters.Add(new SqlParameter("@Channel", SqlDbType.TinyInt));
                command.Parameters.Add(new SqlParameter("@SlaveAddress", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@Page", SqlDbType.TinyInt));
                command.Parameters.Add(new SqlParameter("@StartAddress", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@Length", SqlDbType.TinyInt));
                command.Parameters.Add(new SqlParameter("@Format", SqlDbType.NVarChar, 10));

                command.Parameters.Add(new SqlParameter("@RowState", SqlDbType.TinyInt));


                command.Parameters.Add(new SqlParameter("@OPlogPID", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@TracingInfo", SqlDbType.NVarChar, 4000));
                command.Parameters.Add(new SqlParameter("@myErr", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@myErrMsg", SqlDbType.NVarChar, 4000));
                command.UpdatedRowSource = UpdateRowSource.None;

                command.Parameters["@ID"].Value = id;
                command.Parameters["@PID"].Value = pid;
                command.Parameters["@FieldName"].Value = fieldName;
                command.Parameters["@Channel"].Value = channel;
                command.Parameters["@SlaveAddress"].Value = slaveAddress;
                command.Parameters["@Page"].Value = page;
                command.Parameters["@StartAddress"].Value = startAddress;
                command.Parameters["@Length"].Value = length;
                command.Parameters["@Format"].Value = format;

                command.Parameters["@RowState"].Value = rowStatus;


                command.Parameters["@OPlogPID"].Value = opLogPID;
                command.Parameters["@TracingInfo"].Value = TracingInfo;
                command.Parameters["@ID"].Direction = ParameterDirection.InputOutput;
                command.Parameters["@myErr"].Direction = ParameterDirection.Output;
                command.Parameters["@myErrMsg"].Direction = ParameterDirection.Output;
                command.ExecuteNonQuery();
                id = Convert.ToInt32(command.Parameters["@ID"].Value);
                myErr = Convert.ToInt32(command.Parameters["@myErr"].Value);
                errMsg = command.Parameters["@myErrMsg"].Value.ToString();    //OpenDatabase(false);
                if (myErr == 0)
                {
                    return id;
                }
                else
                {
                    return myErr;
                }
            }
            catch (Exception ex)
            {
                if (command.Parameters["@myErrMsg"].Value != null)
                {
                    errMsg = command.Parameters["@myErrMsg"].Value.ToString();
                    myErr = Convert.ToInt32(command.Parameters["@myErr"].Value);
                    WriteErrorLogs("Err:" + myErr + ";" + errMsg);
                }
                else
                {
                    WriteErrorLogs(ex.ToString());
                    errMsg = ex.ToString();
                }

                AlertMsgShow("Error:" + myErr + ";" + errMsg);
                return myErr;
            }
            finally
            {
                OpenDatabase(false);
            }
        }
        #endregion

        #region Pro_GlobalManufactureCoefficientsGroup
        //存储过程 Pro_GlobalManufactureCoefficientsGroup			
        //保存_GlobalManufactureCoefficientsGroup的资料			
        //参数	类型	方向	描述
        //@ID	int	OUTPUT	
        //@ItemName	nvarchar(30)		
        //@TypeID	int		
        //@ItemDescription	nvarchar(200)		
        //@IgnoreFlag	bit		
        //@RowState	tinyint		
        //@OPlogPID	int		
        //@TracingInfo	nvarchar(max)		
        //@myErr	int	OUTPUT	
        private int Pro_GlobalManufactureCoefficientsGroup(int id, string itemName, int typeID, string itemDescription, bool ignoreFlag,
            byte rowStatus, int opLogPID, string TracingInfo, out int myErr)
        {
            myErr = -1; string errMsg = ""; SqlCommand command = new SqlCommand("Pro_GlobalManufactureCoefficientsGroup", conn);
            try
            {
                OpenDatabase(true);
                //执行存储过程 
                //SqlCommand command = new SqlCommand("Pro_GlobalManufactureCoefficientsGroup", conn);
                //说明命令要执行的是存储过程     
                command.CommandType = CommandType.StoredProcedure;
                //向存储过程中传递参数   
                command.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@ItemName", SqlDbType.NVarChar, 30));
                command.Parameters.Add(new SqlParameter("@TypeID", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@ItemDescription", SqlDbType.NVarChar, 200));
                command.Parameters.Add(new SqlParameter("@IgnoreFlag", SqlDbType.Bit));

                command.Parameters.Add(new SqlParameter("@RowState", SqlDbType.TinyInt));


                command.Parameters.Add(new SqlParameter("@OPlogPID", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@TracingInfo", SqlDbType.NVarChar, 4000));
                command.Parameters.Add(new SqlParameter("@myErr", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@myErrMsg", SqlDbType.NVarChar, 4000));
                command.UpdatedRowSource = UpdateRowSource.None;

                command.Parameters["@ID"].Value = id;
                command.Parameters["@ItemName"].Value = itemName;
                command.Parameters["@TypeID"].Value = typeID;
                command.Parameters["@ItemDescription"].Value = itemDescription;
                command.Parameters["@IgnoreFlag"].Value = ignoreFlag;

                command.Parameters["@RowState"].Value = rowStatus;


                command.Parameters["@OPlogPID"].Value = opLogPID;
                command.Parameters["@TracingInfo"].Value = TracingInfo;
                command.Parameters["@ID"].Direction = ParameterDirection.InputOutput;
                command.Parameters["@myErr"].Direction = ParameterDirection.Output;
                command.Parameters["@myErrMsg"].Direction = ParameterDirection.Output;
                command.ExecuteNonQuery();
                id = Convert.ToInt32(command.Parameters["@ID"].Value);
                myErr = Convert.ToInt32(command.Parameters["@myErr"].Value);
                errMsg = command.Parameters["@myErrMsg"].Value.ToString();    //OpenDatabase(false);
                if (myErr == 0)
                {
                    return id;
                }
                else
                {
                    return myErr;
                }
            }
            catch (Exception ex)
            {
                if (command.Parameters["@myErrMsg"].Value != null)
                {
                    errMsg = command.Parameters["@myErrMsg"].Value.ToString();
                    myErr = Convert.ToInt32(command.Parameters["@myErr"].Value);
                    WriteErrorLogs("Err:" + myErr + ";" + errMsg);
                }
                else
                {
                    WriteErrorLogs(ex.ToString());
                    errMsg = ex.ToString();
                }

                AlertMsgShow("Error:" + myErr + ";" + errMsg);
                return myErr;
            }
            finally
            {
                OpenDatabase(false);
            }
        }
        #endregion

        #region Pro_GlobalManufactureCoefficients
        //存储过程 Pro_GlobalManufactureCoefficients			
        //保存_GlobalManufactureCoefficients的资料			
        //参数	类型	方向	描述
        //@ID	int	OUTPUT	
        //@PID int			
        //@ItemTYPE	nvarchar(30)		
        //@ItemName	nvarchar(30)		
        //@Channel	tinyint		
        //@Page	tinyint		
        //@StartAddress	int		
        //@Length	tinyint		
        //@Format	nvarchar(25)		
        //@RowState 	tinyint		
        //@OPlogPID 	int		
        //@TracingInfo	nvarchar(max)		
        //@myErr	int	OUTPUT	
        private int Pro_GlobalManufactureCoefficients(int id, int pid, string itemTYPE, string itemName, byte channel, byte page, int startAddress, byte length, string format,
            byte rowStatus, int opLogPID, string TracingInfo, out int myErr)
        {
            myErr = -1; string errMsg = ""; SqlCommand command = new SqlCommand("Pro_GlobalManufactureCoefficients", conn);
            try
            {
                OpenDatabase(true);
                //执行存储过程 
                //SqlCommand command = new SqlCommand("Pro_GlobalManufactureCoefficients", conn);
                //说明命令要执行的是存储过程     
                command.CommandType = CommandType.StoredProcedure;
                //向存储过程中传递参数   
                command.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@PID", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@ItemTYPE", SqlDbType.NVarChar, 30));
                command.Parameters.Add(new SqlParameter("@ItemName", SqlDbType.NVarChar, 30));
                command.Parameters.Add(new SqlParameter("@Channel", SqlDbType.TinyInt));
                command.Parameters.Add(new SqlParameter("@Page", SqlDbType.TinyInt));
                command.Parameters.Add(new SqlParameter("@StartAddress", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@Length", SqlDbType.TinyInt));
                command.Parameters.Add(new SqlParameter("@Format", SqlDbType.NVarChar, 25));

                command.Parameters.Add(new SqlParameter("@RowState", SqlDbType.TinyInt));


                command.Parameters.Add(new SqlParameter("@OPlogPID", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@TracingInfo", SqlDbType.NVarChar, 4000));
                command.Parameters.Add(new SqlParameter("@myErr", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@myErrMsg", SqlDbType.NVarChar, 4000));
                command.UpdatedRowSource = UpdateRowSource.None;

                command.Parameters["@ID"].Value = id;
                command.Parameters["@PID"].Value = pid;
                command.Parameters["@ItemTYPE"].Value = itemTYPE;
                command.Parameters["@ItemName"].Value = itemName;
                command.Parameters["@Channel"].Value = channel;
                command.Parameters["@Page"].Value = page;
                command.Parameters["@StartAddress"].Value = startAddress;
                command.Parameters["@Length"].Value = length;
                command.Parameters["@Format"].Value = format;

                command.Parameters["@RowState"].Value = rowStatus;
                command.Parameters["@OPlogPID"].Value = opLogPID;
                command.Parameters["@TracingInfo"].Value = TracingInfo;
                command.Parameters["@ID"].Direction = ParameterDirection.InputOutput;
                command.Parameters["@myErr"].Direction = ParameterDirection.Output;
                command.Parameters["@myErrMsg"].Direction = ParameterDirection.Output;
                command.ExecuteNonQuery();
                id = Convert.ToInt32(command.Parameters["@ID"].Value);
                myErr = Convert.ToInt32(command.Parameters["@myErr"].Value);
                errMsg = command.Parameters["@myErrMsg"].Value.ToString();    //OpenDatabase(false);
                if (myErr == 0)
                {
                    return id;
                }
                else
                {
                    return myErr;
                }
            }
            catch (Exception ex)
            {
                if (command.Parameters["@myErrMsg"].Value != null)
                {
                    errMsg = command.Parameters["@myErrMsg"].Value.ToString();
                    myErr = Convert.ToInt32(command.Parameters["@myErr"].Value);
                    WriteErrorLogs("Err:" + myErr + ";" + errMsg);
                }
                else
                {
                    WriteErrorLogs(ex.ToString());
                    errMsg = ex.ToString();
                }

                AlertMsgShow("Error:" + myErr + ";" + errMsg);
                return myErr;
            }
            finally
            {
                OpenDatabase(false);
            }
        }
        #endregion

        #region Pro_GlobalSpecs
        //存储过程 Pro_GlobalSpecs			
        //保存_GlobalSpecs的资料			
        //参数	类型	方向	描述
        //@ID	int	OUTPUT	
        //@ItemName	nvarchar(30)		
        //@Unit	nvarchar(50)		
        //@ItemDescription	nvarchar(4000)		
        //@RowState	tinyint		
        //@OPlogPID	int		
        //@TracingInfo	nvarchar(max)		
        //@myErr	int	OUTPUT	
        private int Pro_GlobalSpecs(int id, string itemName, string unit, string itemDescription,
            byte rowStatus, int opLogPID, string TracingInfo, out int myErr)
        {
            myErr = -1; string errMsg = ""; SqlCommand command = new SqlCommand("Pro_GlobalSpecs", conn);
            try
            {
                OpenDatabase(true);
                //执行存储过程 
                //SqlCommand command = new SqlCommand("Pro_GlobalSpecs", conn);
                //说明命令要执行的是存储过程     
                command.CommandType = CommandType.StoredProcedure;
                //向存储过程中传递参数   
                command.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@ItemName", SqlDbType.NVarChar, 30));
                command.Parameters.Add(new SqlParameter("@Unit", SqlDbType.NVarChar, 50));
                command.Parameters.Add(new SqlParameter("@ItemDescription", SqlDbType.NVarChar, 4000));

                command.Parameters.Add(new SqlParameter("@RowState", SqlDbType.TinyInt));


                command.Parameters.Add(new SqlParameter("@OPlogPID", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@TracingInfo", SqlDbType.NVarChar, 4000));
                command.Parameters.Add(new SqlParameter("@myErr", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@myErrMsg", SqlDbType.NVarChar, 4000));
                command.UpdatedRowSource = UpdateRowSource.None;

                command.Parameters["@ID"].Value = id;
                command.Parameters["@ItemName"].Value = itemName;
                command.Parameters["@Unit"].Value = unit;
                command.Parameters["@ItemDescription"].Value = itemDescription;

                command.Parameters["@RowState"].Value = rowStatus;


                command.Parameters["@OPlogPID"].Value = opLogPID;
                command.Parameters["@TracingInfo"].Value = TracingInfo;
                command.Parameters["@ID"].Direction = ParameterDirection.InputOutput;
                command.Parameters["@myErr"].Direction = ParameterDirection.Output;
                command.Parameters["@myErrMsg"].Direction = ParameterDirection.Output;
                command.ExecuteNonQuery();
                id = Convert.ToInt32(command.Parameters["@ID"].Value);
                myErr = Convert.ToInt32(command.Parameters["@myErr"].Value);
                errMsg = command.Parameters["@myErrMsg"].Value.ToString();    //OpenDatabase(false);
                if (myErr == 0)
                {
                    return id;
                }
                else
                {
                    return myErr;
                }
            }
            catch (Exception ex)
            {
                if (command.Parameters["@myErrMsg"].Value != null)
                {
                    errMsg = command.Parameters["@myErrMsg"].Value.ToString();
                    myErr = Convert.ToInt32(command.Parameters["@myErr"].Value);
                    WriteErrorLogs("Err:" + myErr + ";" + errMsg);
                }
                else
                {
                    WriteErrorLogs(ex.ToString());
                    errMsg = ex.ToString();
                }

                AlertMsgShow("Error:" + myErr + ";" + errMsg);
                return myErr;
            }
            finally
            {
                OpenDatabase(false);
            }
        }
        #endregion

        #region Pro_GlobalProductionType
        //存储过程 Pro_GlobalProductionType			
        //保存_GlobalProductionType的资料			
        //参数	类型	方向	描述
        //@ID	int	OUTPUT	
        //@ItemName	nvarchar(25)		
        //@MSAID	int		
        //@IgnoreFlag	bit		
        //@RowState	tinyint		
        //@OPlogPID	int		
        //@TracingInfo	nvarchar(max)		
        //@myErr	int	OUTPUT		
        private int Pro_GlobalProductionType(int id, string itemName, int msaID, bool ignoreFlag,
            byte rowStatus, int opLogPID, string TracingInfo, out int myErr)
        {
            myErr = -1; string errMsg = ""; SqlCommand command = new SqlCommand("Pro_GlobalProductionType", conn);
            try
            {
                OpenDatabase(true);
                //执行存储过程 
                //SqlCommand command = new SqlCommand("Pro_GlobalProductionType", conn);
                //说明命令要执行的是存储过程     
                command.CommandType = CommandType.StoredProcedure;
                //向存储过程中传递参数   
                command.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@ItemName", SqlDbType.NVarChar, 25));
                command.Parameters.Add(new SqlParameter("@MSAID", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@IgnoreFlag", SqlDbType.Bit));

                command.Parameters.Add(new SqlParameter("@RowState", SqlDbType.TinyInt));


                command.Parameters.Add(new SqlParameter("@OPlogPID", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@TracingInfo", SqlDbType.NVarChar, 4000));
                command.Parameters.Add(new SqlParameter("@myErr", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@myErrMsg", SqlDbType.NVarChar, 4000));
                command.UpdatedRowSource = UpdateRowSource.None;

                command.Parameters["@ID"].Value = id;
                command.Parameters["@ItemName"].Value = itemName;
                command.Parameters["@MSAID"].Value = msaID;
                command.Parameters["@IgnoreFlag"].Value = ignoreFlag;

                command.Parameters["@RowState"].Value = rowStatus;


                command.Parameters["@OPlogPID"].Value = opLogPID;
                command.Parameters["@TracingInfo"].Value = TracingInfo;
                command.Parameters["@ID"].Direction = ParameterDirection.InputOutput;
                command.Parameters["@myErr"].Direction = ParameterDirection.Output;
                command.Parameters["@myErrMsg"].Direction = ParameterDirection.Output;
                command.ExecuteNonQuery();
                id = Convert.ToInt32(command.Parameters["@ID"].Value);
                myErr = Convert.ToInt32(command.Parameters["@myErr"].Value);
                errMsg = command.Parameters["@myErrMsg"].Value.ToString();    //OpenDatabase(false);
                if (myErr == 0)
                {
                    return id;
                }
                else
                {
                    return myErr;
                }
            }
            catch (Exception ex)
            {
                if (command.Parameters["@myErrMsg"].Value != null)
                {
                    errMsg = command.Parameters["@myErrMsg"].Value.ToString();
                    myErr = Convert.ToInt32(command.Parameters["@myErr"].Value);
                    WriteErrorLogs("Err:" + myErr + ";" + errMsg);
                }
                else
                {
                    WriteErrorLogs(ex.ToString());
                    errMsg = ex.ToString();
                }

                AlertMsgShow("Error:" + myErr + ";" + errMsg);
                return myErr;
            }
            finally
            {
                OpenDatabase(false);
            }
        }
        #endregion

        #region Pro_GlobalProductionName
        //存储过程 Pro_GlobalProductionName			
        //保存_GlobalProductionName的资料			
        //参数	类型	方向	描述
        //@ID	int	OUTPUT	
        //@PID	int		
        //@PN	nvarchar(35)		
        //@ItemName	nvarchar(200)		
        //@Channels	tinyint		
        //@Voltages	tinyint		
        //@Tsensors	tinyint		
        //@MCoefsID	int		
        //@IgnoreFlag	bit		
        //@OldDriver	tinyint		
        //@TEC_Present	tinyint		
        //@Couple_Type	tinyint		
        //@APC_Type	tinyint		
        //@BER	tinyint		
        //@MaxRate	tinyint		
        //@Publish_PN	nvarchar(50)		
        //@NickName	nvarchar(50)		
        //@IbiasFormula	nvarchar(max)		
        //@IModFormula	nvarchar(max)		
        //@UsingCelsiusTemp	bit		
        //@RxOverLoadDBm	real		
        //@RowState	tinyint		
        //@OPlogPID	int		
        //@TracingInfo	nvarchar(max)		
        //@myErr	int	OUTPUT	
        private int Pro_GlobalProductionName(int id, int pid, string pn, string itemName, byte channels, byte voltages,
            byte tsensors, int mCoefsID, bool ignoreFlag, byte oldDriver, byte tec_Present, byte couple_Type,
            byte apc_Type, byte ber, byte maxRate, string publish_PN, string nickName, string ibiasFormula, string iModFormula, bool usingCelsiusTemp, float rxOverLoadDBm,
            byte rowStatus, int opLogPID, string TracingInfo, out int myErr)    // byte apcStyle,
        {
            myErr = -1; string errMsg = ""; SqlCommand command = new SqlCommand("Pro_GlobalProductionName", conn);
            try
            {
                OpenDatabase(true);
                //执行存储过程 
                //SqlCommand command = new SqlCommand("Pro_GlobalProductionName", conn);
                //说明命令要执行的是存储过程     
                command.CommandType = CommandType.StoredProcedure;
                //向存储过程中传递参数
                command.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@PID", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@PN", SqlDbType.NVarChar, 35));
                command.Parameters.Add(new SqlParameter("@ItemName", SqlDbType.NVarChar, 200));
                command.Parameters.Add(new SqlParameter("@Channels", SqlDbType.TinyInt));
                command.Parameters.Add(new SqlParameter("@Voltages", SqlDbType.TinyInt));
                command.Parameters.Add(new SqlParameter("@Tsensors", SqlDbType.TinyInt));
                command.Parameters.Add(new SqlParameter("@MCoefsID", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@IgnoreFlag", SqlDbType.Bit));
                command.Parameters.Add(new SqlParameter("@OldDriver", SqlDbType.TinyInt));
                //command.Parameters.Add(new SqlParameter("@APCStyle", SqlDbType.TinyInt));
                command.Parameters.Add(new SqlParameter("@TEC_Present", SqlDbType.TinyInt));
                command.Parameters.Add(new SqlParameter("@Couple_Type", SqlDbType.TinyInt));
                command.Parameters.Add(new SqlParameter("@APC_Type", SqlDbType.TinyInt));
                command.Parameters.Add(new SqlParameter("@BER", SqlDbType.TinyInt));
                command.Parameters.Add(new SqlParameter("@MaxRate", SqlDbType.TinyInt));
                command.Parameters.Add(new SqlParameter("@Publish_PN", SqlDbType.NVarChar, 50));
                command.Parameters.Add(new SqlParameter("@NickName", SqlDbType.NVarChar, 50));
                command.Parameters.Add(new SqlParameter("@IbiasFormula", SqlDbType.NVarChar, 4000));
                command.Parameters.Add(new SqlParameter("@IModFormula", SqlDbType.NVarChar, 4000));
                command.Parameters.Add(new SqlParameter("@UsingCelsiusTemp", SqlDbType.Bit));
                command.Parameters.Add(new SqlParameter("@RxOverLoadDBm", SqlDbType.Real));

                command.Parameters.Add(new SqlParameter("@RowState", SqlDbType.TinyInt));


                command.Parameters.Add(new SqlParameter("@OPlogPID", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@TracingInfo", SqlDbType.NVarChar, 4000));
                command.Parameters.Add(new SqlParameter("@myErr", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@myErrMsg", SqlDbType.NVarChar, 4000));
                command.UpdatedRowSource = UpdateRowSource.None;
                //@ID	@PID	@PN	@ItemName	@Channels	@Voltages	@Tsensors	@MCoefsID	@IgnoreFlag	@OldDriver	@APCStyle	@TEC_Present	@Couple_Type
                //@APC_Type	@BER	@MaxRate	@Publish_PN	@NickName	@IbiasFormula	@IModFormula	@UsingCelsiusTemp	@RxOverLoadDBm	@RowState	@OPItemName	@OPChildItemName	@OPlogPID	@TracingInfo	@myErr

                command.Parameters["@ID"].Value = id;
                command.Parameters["@PID"].Value = pid;
                command.Parameters["@PN"].Value = pn;
                command.Parameters["@ItemName"].Value = itemName;
                command.Parameters["@Channels"].Value = channels;
                command.Parameters["@Voltages"].Value = voltages;
                command.Parameters["@Tsensors"].Value = tsensors;
                command.Parameters["@MCoefsID"].Value = mCoefsID;
                command.Parameters["@IgnoreFlag"].Value = ignoreFlag;
                command.Parameters["@OldDriver"].Value = oldDriver;
                //command.Parameters["@APCStyle"].Value = apcStyle;
                command.Parameters["@TEC_Present"].Value = tec_Present;
                command.Parameters["@Couple_Type"].Value = couple_Type;
                command.Parameters["@APC_Type"].Value = apc_Type;
                command.Parameters["@BER"].Value = ber;
                command.Parameters["@MaxRate"].Value = maxRate;
                command.Parameters["@Publish_PN"].Value = publish_PN;
                command.Parameters["@NickName"].Value = nickName;
                command.Parameters["@IbiasFormula"].Value = ibiasFormula;
                command.Parameters["@IModFormula"].Value = iModFormula;
                command.Parameters["@UsingCelsiusTemp"].Value = usingCelsiusTemp;
                command.Parameters["@RxOverLoadDBm"].Value = rxOverLoadDBm;

                command.Parameters["@RowState"].Value = rowStatus;


                command.Parameters["@OPlogPID"].Value = opLogPID;
                command.Parameters["@TracingInfo"].Value = TracingInfo;
                command.Parameters["@ID"].Direction = ParameterDirection.InputOutput;
                command.Parameters["@myErr"].Direction = ParameterDirection.Output;
                command.Parameters["@myErrMsg"].Direction = ParameterDirection.Output;
                command.ExecuteNonQuery();
                id = Convert.ToInt32(command.Parameters["@ID"].Value);
                myErr = Convert.ToInt32(command.Parameters["@myErr"].Value);
                errMsg = command.Parameters["@myErrMsg"].Value.ToString();    //OpenDatabase(false);
                if (myErr == 0)
                {
                    return id;
                }
                else
                {
                    return myErr;
                }
            }
            catch (Exception ex)
            {
                if (command.Parameters["@myErrMsg"].Value != null)
                {
                    errMsg = command.Parameters["@myErrMsg"].Value.ToString();
                    myErr = Convert.ToInt32(command.Parameters["@myErr"].Value);
                    WriteErrorLogs("Err:" + myErr + ";" + errMsg);
                }
                else
                {
                    WriteErrorLogs(ex.ToString());
                    errMsg = ex.ToString();
                }

                AlertMsgShow("Error:" + myErr + ";" + errMsg);
                return myErr;
            }
            finally
            {
                OpenDatabase(false);
            }
        }
        #endregion

        #region Pro_GlobalManufactureChipsetInitialize
        //存储过程 Pro_GlobalManufactureChipsetInitialize			
        //保存_GlobalManufactureChipsetInitialize的资料			
        //参数	类型	方向	描述
        //@ID	int	OUTPUT	
        //@PID	int		
        //@DriveType	tinyint		
        //@ChipLine	tinyint		
        //@RegisterAddress	int		
        //@Length	tinyint		
        //@ItemValue	int		
        //@Endianness	bit		
        //@RowState	tinyint		
        //@OPlogPID	int		
        //@TracingInfo	nvarchar(max)		
        //@myErr	int	OUTPUT	
        private int Pro_GlobalManufactureChipsetInitialize(int id, int pid, byte driveType, byte chipLine,
            int registerAddress, byte length, int itemValue, bool endianness,
            byte rowStatus, int opLogPID, string TracingInfo, out int myErr)
        {
            myErr = -1; string errMsg = ""; SqlCommand command = new SqlCommand("Pro_GlobalManufactureChipsetInitialize", conn);
            try
            {
                OpenDatabase(true);
                //执行存储过程 
                //SqlCommand command = new SqlCommand("Pro_GlobalManufactureChipsetInitialize", conn);
                //说明命令要执行的是存储过程     
                command.CommandType = CommandType.StoredProcedure;
                //向存储过程中传递参数 
                command.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@PID", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@DriveType", SqlDbType.TinyInt));
                command.Parameters.Add(new SqlParameter("@ChipLine", SqlDbType.TinyInt));
                command.Parameters.Add(new SqlParameter("@RegisterAddress", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@Length", SqlDbType.TinyInt));
                command.Parameters.Add(new SqlParameter("@ItemValue", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@Endianness", SqlDbType.Bit));

                command.Parameters.Add(new SqlParameter("@RowState", SqlDbType.TinyInt));


                command.Parameters.Add(new SqlParameter("@OPlogPID", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@TracingInfo", SqlDbType.NVarChar, 4000));
                command.Parameters.Add(new SqlParameter("@myErr", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@myErrMsg", SqlDbType.NVarChar, 4000));
                command.UpdatedRowSource = UpdateRowSource.None;

                command.Parameters["@ID"].Value = id;
                command.Parameters["@PID"].Value = pid;
                command.Parameters["@DriveType"].Value = driveType;
                command.Parameters["@ChipLine"].Value = chipLine;
                command.Parameters["@RegisterAddress"].Value = registerAddress;
                command.Parameters["@Length"].Value = length;
                command.Parameters["@ItemValue"].Value = itemValue;
                command.Parameters["@Endianness"].Value = endianness;

                command.Parameters["@RowState"].Value = rowStatus;


                command.Parameters["@OPlogPID"].Value = opLogPID;
                command.Parameters["@TracingInfo"].Value = TracingInfo;
                command.Parameters["@ID"].Direction = ParameterDirection.InputOutput;
                command.Parameters["@myErr"].Direction = ParameterDirection.Output;
                command.Parameters["@myErrMsg"].Direction = ParameterDirection.Output;
                command.ExecuteNonQuery();
                id = Convert.ToInt32(command.Parameters["@ID"].Value);
                myErr = Convert.ToInt32(command.Parameters["@myErr"].Value);
                errMsg = command.Parameters["@myErrMsg"].Value.ToString();    //OpenDatabase(false);
                if (myErr == 0)
                {
                    return id;
                }
                else
                {
                    return myErr;
                }
            }
            catch (Exception ex)
            {
                if (command.Parameters["@myErrMsg"].Value != null)
                {
                    errMsg = command.Parameters["@myErrMsg"].Value.ToString();
                    myErr = Convert.ToInt32(command.Parameters["@myErr"].Value);
                    WriteErrorLogs("Err:" + myErr + ";" + errMsg);
                }
                else
                {
                    WriteErrorLogs(ex.ToString());
                    errMsg = ex.ToString();
                }

                AlertMsgShow("Error:" + myErr + ";" + errMsg);
                return myErr;
            }
            finally
            {
                OpenDatabase(false);
            }
        }
        #endregion

        #region Pro_GlobalManufactureChipsetControl
        //存储过程 Pro_GlobalManufactureChipsetControl			
        //保存_GlobalManufactureChipsetControl的资料			
        //参数	类型	方向	描述
        //@ID	int	OUTPUT	
        //@PID	int		
        //@ItemName	nvarchar(20)		
        //@ModuleLine	tinyint		
        //@ChipLine	tinyint		
        //@DriveType	tinyint		
        //@RegisterAddress	int		
        //@Length	tinyint		
        //@Endianness	bit		
        //@RowState	tinyint		
        //@OPlogPID	int		
        //@TracingInfo	nvarchar(max)		
        //@myErr	int	OUTPUT		
        private int Pro_GlobalManufactureChipsetControl(int id, int pid, string itemName, byte moduleLine, byte chipLine, byte driveType,
            int registerAddress, byte length, bool endianness,
            byte rowStatus, int opLogPID, string TracingInfo, out int myErr)
        {
            myErr = -1; string errMsg = ""; SqlCommand command = new SqlCommand("Pro_GlobalManufactureChipsetControl", conn);
            try
            {
                OpenDatabase(true);
                //执行存储过程 
                //SqlCommand command = new SqlCommand("Pro_GlobalManufactureChipsetControl", conn);
                //说明命令要执行的是存储过程     
                command.CommandType = CommandType.StoredProcedure;
                //向存储过程中传递参数                
                command.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@PID", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@ItemName", SqlDbType.NVarChar, 20));
                command.Parameters.Add(new SqlParameter("@ModuleLine", SqlDbType.TinyInt));
                command.Parameters.Add(new SqlParameter("@ChipLine", SqlDbType.TinyInt));
                command.Parameters.Add(new SqlParameter("@DriveType", SqlDbType.TinyInt));
                command.Parameters.Add(new SqlParameter("@RegisterAddress", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@Length", SqlDbType.TinyInt));
                command.Parameters.Add(new SqlParameter("@Endianness", SqlDbType.Bit));

                command.Parameters.Add(new SqlParameter("@RowState", SqlDbType.TinyInt));


                command.Parameters.Add(new SqlParameter("@OPlogPID", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@TracingInfo", SqlDbType.NVarChar, 4000));
                command.Parameters.Add(new SqlParameter("@myErr", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@myErrMsg", SqlDbType.NVarChar, 4000));
                command.UpdatedRowSource = UpdateRowSource.None;
                //@ID	@PID	@ItemName	@ModuleLine	@ChipLine	@DriveType	@RegisterAddress	@Length	@Endianness
                //int	int	nvarchar(20)	tinyint	tinyint	tinyint	int	tinyint	bit
                command.Parameters["@ID"].Value = id;
                command.Parameters["@PID"].Value = pid;
                command.Parameters["@ItemName"].Value = itemName;
                command.Parameters["@ModuleLine"].Value = moduleLine;
                command.Parameters["@ChipLine"].Value = chipLine;
                command.Parameters["@DriveType"].Value = driveType;
                command.Parameters["@RegisterAddress"].Value = registerAddress;
                command.Parameters["@Length"].Value = length;
                command.Parameters["@Endianness"].Value = endianness;

                command.Parameters["@RowState"].Value = rowStatus;


                command.Parameters["@OPlogPID"].Value = opLogPID;
                command.Parameters["@TracingInfo"].Value = TracingInfo;
                command.Parameters["@ID"].Direction = ParameterDirection.InputOutput;
                command.Parameters["@myErr"].Direction = ParameterDirection.Output;
                command.Parameters["@myErrMsg"].Direction = ParameterDirection.Output;
                command.ExecuteNonQuery();
                id = Convert.ToInt32(command.Parameters["@ID"].Value);
                myErr = Convert.ToInt32(command.Parameters["@myErr"].Value);
                errMsg = command.Parameters["@myErrMsg"].Value.ToString();    //OpenDatabase(false);
                if (myErr == 0)
                {
                    return id;
                }
                else
                {
                    return myErr;
                }
            }
            catch (Exception ex)
            {
                if (command.Parameters["@myErrMsg"].Value != null)
                {
                    errMsg = command.Parameters["@myErrMsg"].Value.ToString();
                    myErr = Convert.ToInt32(command.Parameters["@myErr"].Value);
                    WriteErrorLogs("Err:" + myErr + ";" + errMsg);
                }
                else
                {
                    WriteErrorLogs(ex.ToString());
                    errMsg = ex.ToString();
                }

                AlertMsgShow("Error:" + myErr + ";" + errMsg);
                return myErr;
            }
            finally
            {
                OpenDatabase(false);
            }
        }
        #endregion

        #region Pro_TopoTestPlan
        //存储过程 Pro_TopoTestPlan			
        //保存_TopoTestPlan的资料			
        //参数	类型	方向	描述
        //@ID	int	OUTPUT	
        //@PID	int		
        //@ItemName	nvarchar(30)		
        //@SWVersion	nvarchar(30)		
        //@HWVersion	nvarchar(30)		
        //@USBPort	tinyint		
        //@IsChipInitialize	bit		
        //@IsEEPROMInitialize	bit		
        //@IgnoreBackupCoef	bit		
        //@SNCheck	bit		
        //@IgnoreFlag	bit		
        //@ItemDescription	nvarchar(200)		
        //@RowState	tinyint		
        //@OPlogPID	int		
        //@TracingInfo	nvarchar(max)		
        //@myErr	int	OUTPUT	
        private int Pro_TopoTestPlan(int id, int pid, string itemName, string swVersion, string hwVersion, byte usbPort, bool isChipInitialize,
            bool isEEPROMInitialize, bool ignoreBackupCoef, bool snCheck, bool ignoreFlag, string itemDescription,
            byte rowStatus, int opLogPID, string TracingInfo, out int myErr)
        {
            myErr = -1; string errMsg = ""; SqlCommand command = new SqlCommand("Pro_TopoTestPlan", conn);
            try
            {
                OpenDatabase(true);
                //执行存储过程 
                //SqlCommand command = new SqlCommand("Pro_TopoTestPlan", conn);
                //说明命令要执行的是存储过程     
                command.CommandType = CommandType.StoredProcedure;
                //向存储过程中传递参数   

                command.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@PID", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@ItemName", SqlDbType.NVarChar, 30));
                command.Parameters.Add(new SqlParameter("@SWVersion", SqlDbType.NVarChar, 30));
                command.Parameters.Add(new SqlParameter("@HWVersion", SqlDbType.NVarChar, 30));
                command.Parameters.Add(new SqlParameter("@USBPort", SqlDbType.TinyInt));
                command.Parameters.Add(new SqlParameter("@IsChipInitialize", SqlDbType.Bit));
                command.Parameters.Add(new SqlParameter("@IsEEPROMInitialize", SqlDbType.Bit));
                command.Parameters.Add(new SqlParameter("@IgnoreBackupCoef", SqlDbType.Bit));
                command.Parameters.Add(new SqlParameter("@SNCheck", SqlDbType.Bit));
                command.Parameters.Add(new SqlParameter("@IgnoreFlag", SqlDbType.Bit));
                command.Parameters.Add(new SqlParameter("@ItemDescription", SqlDbType.NVarChar, 50));

                command.Parameters.Add(new SqlParameter("@RowState", SqlDbType.TinyInt));


                command.Parameters.Add(new SqlParameter("@OPlogPID", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@TracingInfo", SqlDbType.NVarChar, 4000));
                command.Parameters.Add(new SqlParameter("@myErr", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@myErrMsg", SqlDbType.NVarChar, 4000));
                command.UpdatedRowSource = UpdateRowSource.None;
                command.Parameters["@ID"].Value = id;
                command.Parameters["@PID"].Value = pid;
                command.Parameters["@ItemName"].Value = itemName;
                command.Parameters["@SWVersion"].Value = swVersion;
                command.Parameters["@HWVersion"].Value = hwVersion;
                command.Parameters["@USBPort"].Value = usbPort;
                command.Parameters["@IsChipInitialize"].Value = isChipInitialize;
                command.Parameters["@IsEEPROMInitialize"].Value = isEEPROMInitialize;
                command.Parameters["@IgnoreBackupCoef"].Value = ignoreBackupCoef;
                command.Parameters["@SNCheck"].Value = snCheck;
                command.Parameters["@IgnoreFlag"].Value = ignoreFlag;
                command.Parameters["@ItemDescription"].Value = itemDescription;

                command.Parameters["@RowState"].Value = rowStatus;


                command.Parameters["@OPlogPID"].Value = opLogPID;
                command.Parameters["@TracingInfo"].Value = TracingInfo;
                command.Parameters["@ID"].Direction = ParameterDirection.InputOutput;
                command.Parameters["@myErr"].Direction = ParameterDirection.Output;
                command.Parameters["@myErrMsg"].Direction = ParameterDirection.Output;
                command.ExecuteNonQuery();
                id = Convert.ToInt32(command.Parameters["@ID"].Value);
                myErr = Convert.ToInt32(command.Parameters["@myErr"].Value);
                errMsg = command.Parameters["@myErrMsg"].Value.ToString();    //OpenDatabase(false);
                if (myErr == 0)
                {
                    return id;
                }
                else
                {
                    return myErr;
                }
            }
            catch (Exception ex)
            {
                if (command.Parameters["@myErrMsg"].Value != null)
                {
                    errMsg = command.Parameters["@myErrMsg"].Value.ToString();
                    myErr = Convert.ToInt32(command.Parameters["@myErr"].Value);
                    WriteErrorLogs("Err:" + myErr + ";" + errMsg);
                }
                else
                {
                    WriteErrorLogs(ex.ToString());
                    errMsg = ex.ToString();
                }

                AlertMsgShow("Error:" + myErr + ";" + errMsg);
                return myErr;
            }
            finally
            {
                OpenDatabase(false);
            }
        }
        #endregion

        #region Pro_TopoTestControl
        //存储过程 Pro_TopoTestControl			
        //保存_TopoTestControl的资料			
        //参数	类型	方向	描述
        //@ID	int	OUTPUT	
        //@PID	int		
        //@ItemName	nvarchar(50)		
        //@SEQ	int		
        //@Channel	tinyint		
        //@Temp	real		
        //@Vcc	real		
        //@Pattent	tinyint		
        //@DataRate 	nvarchar(50)		
        //@CtrlType	tinyint		
        //@TempOffset	real		
        //@TempWaitTimes 	real		
        //@IgnoreFlag	bit		
        //@ItemDescription	nvarchar(200)		
        //@RowState	tinyint		
        //@OPlogPID	int		
        //@TracingInfo	nvarchar(max)		
        //@myErr	int	OUTPUT	
        private int Pro_TopoTestControl(int id, int pid, string itemName, int seq, byte channel, float temp, float vcc, byte pattent,
            string dataRate, byte ctrlType, float tempOffset, float tempWaitTimes, bool ignoreFlag, string itemDescription,
            byte rowStatus, int opLogPID, string TracingInfo, out int myErr)
        {
            myErr = -1; string errMsg = ""; SqlCommand command = new SqlCommand("Pro_TopoTestControl", conn);
            try
            {
                OpenDatabase(true);
                //执行存储过程 
                //SqlCommand command = new SqlCommand("Pro_TopoTestControl", conn);
                //说明命令要执行的是存储过程     
                command.CommandType = CommandType.StoredProcedure;
                //向存储过程中传递参数   

                command.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@PID", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@ItemName", SqlDbType.NVarChar, 50));
                command.Parameters.Add(new SqlParameter("@SEQ", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@Channel", SqlDbType.TinyInt));
                command.Parameters.Add(new SqlParameter("@Temp", SqlDbType.Real));
                command.Parameters.Add(new SqlParameter("@Vcc", SqlDbType.Real));
                command.Parameters.Add(new SqlParameter("@Pattent", SqlDbType.TinyInt));
                command.Parameters.Add(new SqlParameter("@DataRate", SqlDbType.NVarChar, 50));
                command.Parameters.Add(new SqlParameter("@CtrlType", SqlDbType.TinyInt));
                command.Parameters.Add(new SqlParameter("@TempOffset", SqlDbType.Real));
                command.Parameters.Add(new SqlParameter("@TempWaitTimes", SqlDbType.Real));
                command.Parameters.Add(new SqlParameter("@IgnoreFlag", SqlDbType.Bit));
                command.Parameters.Add(new SqlParameter("@ItemDescription", SqlDbType.NVarChar, 200));

                command.Parameters.Add(new SqlParameter("@RowState", SqlDbType.TinyInt));


                command.Parameters.Add(new SqlParameter("@OPlogPID", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@TracingInfo", SqlDbType.NVarChar, 4000));
                command.Parameters.Add(new SqlParameter("@myErr", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@myErrMsg", SqlDbType.NVarChar, 4000));
                command.UpdatedRowSource = UpdateRowSource.None;

                command.Parameters["@ID"].Value = id;
                command.Parameters["@PID"].Value = pid;
                command.Parameters["@ItemName"].Value = itemName;
                command.Parameters["@SEQ"].Value = seq;
                command.Parameters["@Channel"].Value = channel;
                command.Parameters["@Temp"].Value = temp;
                command.Parameters["@Vcc"].Value = vcc;
                command.Parameters["@Pattent"].Value = pattent;
                command.Parameters["@DataRate"].Value = dataRate;
                command.Parameters["@CtrlType"].Value = ctrlType;
                command.Parameters["@TempOffset"].Value = tempOffset;
                command.Parameters["@TempWaitTimes"].Value = tempWaitTimes;
                command.Parameters["@IgnoreFlag"].Value = ignoreFlag;
                command.Parameters["@ItemDescription"].Value = itemDescription;

                command.Parameters["@RowState"].Value = rowStatus;


                command.Parameters["@OPlogPID"].Value = opLogPID;
                command.Parameters["@TracingInfo"].Value = TracingInfo;
                command.Parameters["@ID"].Direction = ParameterDirection.InputOutput;
                command.Parameters["@myErr"].Direction = ParameterDirection.Output;
                command.Parameters["@myErrMsg"].Direction = ParameterDirection.Output;
                command.ExecuteNonQuery();
                id = Convert.ToInt32(command.Parameters["@ID"].Value);
                myErr = Convert.ToInt32(command.Parameters["@myErr"].Value);
                errMsg = command.Parameters["@myErrMsg"].Value.ToString();    //OpenDatabase(false);
                if (myErr == 0)
                {
                    return id;
                }
                else
                {
                    return myErr;
                }
            }
            catch (Exception ex)
            {
                if (command.Parameters["@myErrMsg"].Value != null)
                {
                    errMsg = command.Parameters["@myErrMsg"].Value.ToString();
                    myErr = Convert.ToInt32(command.Parameters["@myErr"].Value);
                    WriteErrorLogs("Err:" + myErr + ";" + errMsg);
                }
                else
                {
                    WriteErrorLogs(ex.ToString());
                    errMsg = ex.ToString();
                }

                AlertMsgShow("Error:" + myErr + ";" + errMsg);
                return myErr;
            }
            finally
            {
                OpenDatabase(false);
            }
        }
        #endregion

        #region Pro_TopoTestModelWithParams
        //存储过程 Pro_TopoTestModelWithParams			
        //保存_TopoTestModelWithParams的资料			
        //参数	类型	方向	描述
        //@ID	int	OUTPUT	
        //@PID	int		
        //@GID	int		
        //@Seq	int		
        //@IgnoreFlag	bit		
        //@Failbreak	bit		
        //@Params	varchar(Max)		
        //@RowState	tinyint		
        //@OPlogPID	int		
        //@TracingInfo	nvarchar(max)		
        //@myErr	int	OUTPUT	
        private int Pro_TopoTestModelWithParams(int id, int pid, int gid, int seq, bool ignoreFlag, bool failbreak, string ModelParams,
            byte rowStatus, int opLogPID, string TracingInfo, out int myErr)
        {
            myErr = -1; string errMsg = ""; SqlCommand command = new SqlCommand("Pro_TopoTestModelWithParams", conn);
            try
            {
                OpenDatabase(true);
                //执行存储过程 
                //SqlCommand command = new SqlCommand("Pro_TopoTestModelWithParams", conn);
                //说明命令要执行的是存储过程     
                command.CommandType = CommandType.StoredProcedure;
                //向存储过程中传递参数   
                command.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@PID", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@GID", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@SEQ", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@IgnoreFlag", SqlDbType.Bit));
                command.Parameters.Add(new SqlParameter("@Failbreak", SqlDbType.Bit));
                command.Parameters.Add(new SqlParameter("@Params", SqlDbType.VarChar, 4000));
                command.Parameters.Add(new SqlParameter("@RowState", SqlDbType.TinyInt));


                command.Parameters.Add(new SqlParameter("@OPlogPID", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@TracingInfo", SqlDbType.NVarChar, 4000));
                command.Parameters.Add(new SqlParameter("@myErr", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@myErrMsg", SqlDbType.NVarChar, 4000));
                command.UpdatedRowSource = UpdateRowSource.None;

                command.Parameters["@ID"].Value = id;
                command.Parameters["@PID"].Value = pid;
                command.Parameters["@GID"].Value = gid;
                command.Parameters["@SEQ"].Value = seq;
                command.Parameters["@IgnoreFlag"].Value = ignoreFlag;
                command.Parameters["@Failbreak"].Value = failbreak;
                command.Parameters["@Params"].Value = ModelParams;
                command.Parameters["@RowState"].Value = rowStatus;


                command.Parameters["@OPlogPID"].Value = opLogPID;
                command.Parameters["@TracingInfo"].Value = TracingInfo;
                command.Parameters["@ID"].Direction = ParameterDirection.InputOutput;
                command.Parameters["@myErr"].Direction = ParameterDirection.Output;
                command.Parameters["@myErrMsg"].Direction = ParameterDirection.Output;
                command.ExecuteNonQuery();
                id = Convert.ToInt32(command.Parameters["@ID"].Value);
                myErr = Convert.ToInt32(command.Parameters["@myErr"].Value);
                errMsg = command.Parameters["@myErrMsg"].Value.ToString();    //OpenDatabase(false);
                if (myErr == 0)
                {
                    return id;
                }
                else
                {
                    return myErr;
                }
            }
            catch (Exception ex)
            {
                if (command.Parameters["@myErrMsg"].Value != null)
                {
                    errMsg = command.Parameters["@myErrMsg"].Value.ToString();
                    myErr = Convert.ToInt32(command.Parameters["@myErr"].Value);
                    WriteErrorLogs("Err:" + myErr + ";" + errMsg);
                }
                else
                {
                    WriteErrorLogs(ex.ToString());
                    errMsg = ex.ToString();
                }

                AlertMsgShow("Error:" + myErr + ";" + errMsg);
                return myErr;
            }
            finally
            {
                OpenDatabase(false);
            }
        }
        #endregion

        #region Pro_TopoEquipmentWithParams
        //存储过程 Pro_TopoEquipmentWithParams			
        //保存_TopoEquipmentWithParams的资料			
        //参数	类型	方向	描述
        //@ID	int	OUTPUT	
        //@PID 	int		
        //@GID	int		
        //@SEQ	int		
        //@Role	tinyint		
        //@Params 	varchar(max)		
        //@RowState	tinyint		
        //@OPlogPID	int		
        //@TracingInfo	nvarchar(max)		
        //@myErr	int	OUTPUT	
        private int Pro_TopoEquipmentWithParams(int id, int pid, int gid, int seq, byte role, string eqParams,
            byte rowStatus, int opLogPID, string TracingInfo, out int myErr)
        {
            myErr = -1; string errMsg = ""; SqlCommand command = new SqlCommand("Pro_TopoEquipmentWithParams", conn);
            try
            {
                OpenDatabase(true);
                //执行存储过程 
                //SqlCommand command = new SqlCommand("Pro_TopoEquipmentWithParams", conn);
                //说明命令要执行的是存储过程     
                command.CommandType = CommandType.StoredProcedure;
                //向存储过程中传递参数   
                command.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@PID", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@GID", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@SEQ", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@Role", SqlDbType.TinyInt));
                command.Parameters.Add(new SqlParameter("@Params", SqlDbType.VarChar, 8000));
                command.Parameters.Add(new SqlParameter("@RowState", SqlDbType.TinyInt));


                command.Parameters.Add(new SqlParameter("@OPlogPID", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@TracingInfo", SqlDbType.NVarChar, 4000));
                command.Parameters.Add(new SqlParameter("@myErr", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@myErrMsg", SqlDbType.NVarChar, 4000));
                command.UpdatedRowSource = UpdateRowSource.None;

                command.Parameters["@ID"].Value = id;
                command.Parameters["@PID"].Value = pid;
                command.Parameters["@gid"].Value = gid;
                command.Parameters["@seq"].Value = seq;
                command.Parameters["@role"].Value = role;
                command.Parameters["@Params"].Value = eqParams;
                command.Parameters["@RowState"].Value = rowStatus;


                command.Parameters["@OPlogPID"].Value = opLogPID;
                command.Parameters["@TracingInfo"].Value = TracingInfo;
                command.Parameters["@ID"].Direction = ParameterDirection.InputOutput;
                command.Parameters["@myErr"].Direction = ParameterDirection.Output;
                command.Parameters["@myErrMsg"].Direction = ParameterDirection.Output;
                command.ExecuteNonQuery();
                id = Convert.ToInt32(command.Parameters["@ID"].Value);
                myErr = Convert.ToInt32(command.Parameters["@myErr"].Value);
                errMsg = command.Parameters["@myErrMsg"].Value.ToString();    //OpenDatabase(false);
                if (myErr == 0)
                {
                    return id;
                }
                else
                {
                    return myErr;
                }
            }
            catch (Exception ex)
            {
                if (command.Parameters["@myErrMsg"].Value != null)
                {
                    errMsg = command.Parameters["@myErrMsg"].Value.ToString();
                    myErr = Convert.ToInt32(command.Parameters["@myErr"].Value);
                    WriteErrorLogs("Err:" + myErr + ";" + errMsg);
                }
                else
                {
                    WriteErrorLogs(ex.ToString());
                    errMsg = ex.ToString();
                }

                AlertMsgShow("Error:" + myErr + ";" + errMsg);
                return myErr;
            }
            finally
            {
                OpenDatabase(false);
            }
        }
        #endregion

        #region Pro_TopoPNSpecsParams
        //存储过程 Pro_TopoPNSpecsParams			
        //保存_TopoPNSpecsParams的资料			
        //参数	类型	方向	描述
        //@ID	int	OUTPUT	
        //@PID	int		
        //@SID	int		
        //@Typical	float		
        //@SpecMin	float		
        //@SpecMax	float	
        //@Channel	tinyint     
        //@RowState	tinyint		
        //@OPlogPID	int		
        //@TracingInfo	nvarchar(max)		
        //@myErr	int	OUTPUT		
        private int Pro_TopoPNSpecsParams(int id, int pid, int sID, double typical, double specMin, double specMax, byte channel,
            byte rowStatus, int opLogPID, string TracingInfo, out int myErr)
        {
            myErr = -1; string errMsg = ""; SqlCommand command = new SqlCommand("Pro_TopoPNSpecsParams", conn);
            try
            {
                OpenDatabase(true);
                //执行存储过程 
                //SqlCommand command = new SqlCommand("Pro_TopoPNSpecsParams", conn);
                //说明命令要执行的是存储过程     
                command.CommandType = CommandType.StoredProcedure;
                //向存储过程中传递参数   
                //@ID	@PID	@SID	@Typical	@SpecMin	@SpecMax  @Channel
                //int	int	int	float	float	float   byte

                command.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@PID", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@SID", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@Typical", SqlDbType.Float));
                command.Parameters.Add(new SqlParameter("@SpecMin", SqlDbType.Float));
                command.Parameters.Add(new SqlParameter("@SpecMax", SqlDbType.Float));
                command.Parameters.Add(new SqlParameter("@Channel", SqlDbType.TinyInt));

                command.Parameters.Add(new SqlParameter("@RowState", SqlDbType.TinyInt));


                command.Parameters.Add(new SqlParameter("@OPlogPID", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@TracingInfo", SqlDbType.NVarChar, 4000));
                command.Parameters.Add(new SqlParameter("@myErr", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@myErrMsg", SqlDbType.NVarChar, 4000));
                command.UpdatedRowSource = UpdateRowSource.None;
                command.Parameters["@ID"].Value = id;
                command.Parameters["@PID"].Value = pid;
                command.Parameters["@SID"].Value = sID;
                command.Parameters["@Typical"].Value = typical;
                command.Parameters["@SpecMin"].Value = specMin;
                command.Parameters["@SpecMax"].Value = specMax;
                command.Parameters["@Channel"].Value = channel;
                command.Parameters["@RowState"].Value = rowStatus;


                command.Parameters["@OPlogPID"].Value = opLogPID;
                command.Parameters["@TracingInfo"].Value = TracingInfo;
                command.Parameters["@ID"].Direction = ParameterDirection.InputOutput;
                command.Parameters["@myErr"].Direction = ParameterDirection.Output;
                command.Parameters["@myErrMsg"].Direction = ParameterDirection.Output;
                command.ExecuteNonQuery();
                id = Convert.ToInt32(command.Parameters["@ID"].Value);
                myErr = Convert.ToInt32(command.Parameters["@myErr"].Value);
                errMsg = command.Parameters["@myErrMsg"].Value.ToString();    //OpenDatabase(false);
                if (myErr == 0)
                {
                    return id;
                }
                else
                {
                    return myErr;
                }
            }
            catch (Exception ex)
            {
                if (command.Parameters["@myErrMsg"].Value != null)
                {
                    errMsg = command.Parameters["@myErrMsg"].Value.ToString();
                    myErr = Convert.ToInt32(command.Parameters["@myErr"].Value);
                    WriteErrorLogs("Err:" + myErr + ";" + errMsg);
                }
                else
                {
                    WriteErrorLogs(ex.ToString());
                    errMsg = ex.ToString();
                }

                AlertMsgShow("Error:" + myErr + ";" + errMsg);
                return myErr;
            }
            finally
            {
                OpenDatabase(false);
            }
        }
        #endregion

        #region Pro_TopoManufactureConfigInit
        //存储过程 Pro_TopoManufactureConfigInit			
        //保存_TopoManufactureConfigInit的资料			
        //参数	类型	方向	描述
        //@ID	int	OUTPUT	
        //@PID	int		
        //@SlaveAddress	int		
        //@Page	tinyint		
        //@StartAddress	int		
        //@Length	tinyint		
        //@ItemValue	int		
        //@RowState	tinyint		
        //@OPlogPID	int		
        //@TracingInfo	nvarchar(max)		
        //@myErr	int	OUTPUT	
        private int Pro_TopoManufactureConfigInit(int id, int pid, int slaveAddress, byte page, int startAddress, byte length, int itemValue,
            byte rowStatus, int opLogPID, string TracingInfo, out int myErr)
        {
            myErr = -1; string errMsg = ""; SqlCommand command = new SqlCommand("Pro_TopoManufactureConfigInit", conn);
            try
            {
                OpenDatabase(true);
                //执行存储过程 
                //SqlCommand command = new SqlCommand("Pro_TopoManufactureConfigInit", conn);
                //说明命令要执行的是存储过程     
                command.CommandType = CommandType.StoredProcedure;
                //向存储过程中传递参数   
                //@ID	@PID	@SlaveAddress	@Page	@StartAddress	@Length	@ItemValue	@RowState	@OPlogPID	@TracingInfo	@myErr
                //int	int	int	tinyint	int	tinyint	int	tinyint	int	nvarchar(max)	int

                command.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@PID", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@SlaveAddress", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@Page", SqlDbType.TinyInt));
                command.Parameters.Add(new SqlParameter("@StartAddress", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@Length", SqlDbType.TinyInt));
                command.Parameters.Add(new SqlParameter("@ItemValue", SqlDbType.Int));

                command.Parameters.Add(new SqlParameter("@RowState", SqlDbType.TinyInt));


                command.Parameters.Add(new SqlParameter("@OPlogPID", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@TracingInfo", SqlDbType.NVarChar, 4000));
                command.Parameters.Add(new SqlParameter("@myErr", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@myErrMsg", SqlDbType.NVarChar, 4000));
                command.UpdatedRowSource = UpdateRowSource.None;
                command.Parameters["@ID"].Value = id;
                command.Parameters["@PID"].Value = pid;
                command.Parameters["@SlaveAddress"].Value = slaveAddress;
                command.Parameters["@Page"].Value = page;
                command.Parameters["@StartAddress"].Value = startAddress;
                command.Parameters["@Length"].Value = length;
                command.Parameters["@ItemValue"].Value = itemValue;

                command.Parameters["@RowState"].Value = rowStatus;


                command.Parameters["@OPlogPID"].Value = opLogPID;
                command.Parameters["@TracingInfo"].Value = TracingInfo;
                command.Parameters["@ID"].Direction = ParameterDirection.InputOutput;
                command.Parameters["@myErr"].Direction = ParameterDirection.Output;
                command.Parameters["@myErrMsg"].Direction = ParameterDirection.Output;
                command.ExecuteNonQuery();
                id = Convert.ToInt32(command.Parameters["@ID"].Value);
                myErr = Convert.ToInt32(command.Parameters["@myErr"].Value);
                errMsg = command.Parameters["@myErrMsg"].Value.ToString();    //OpenDatabase(false);
                if (myErr == 0)
                {
                    return id;
                }
                else
                {
                    return myErr;
                }
            }
            catch (Exception ex)
            {
                if (command.Parameters["@myErrMsg"].Value != null)
                {
                    errMsg = command.Parameters["@myErrMsg"].Value.ToString();
                    myErr = Convert.ToInt32(command.Parameters["@myErr"].Value);
                    WriteErrorLogs("Err:" + myErr + ";" + errMsg);
                }
                else
                {
                    WriteErrorLogs(ex.ToString());
                    errMsg = ex.ToString();
                }

                AlertMsgShow("Error:" + myErr + ";" + errMsg);
                return myErr;
            }
            finally
            {
                OpenDatabase(false);
            }
        }
        #endregion

        #region Pro_TopoMSAEEPROMSet
        //存储过程 Pro_TopoMSAEEPROMSet			
        //保存_TopoMSAEEPROMSet的资料			
        //参数	类型	方向	描述
        //@ID int	OUTPUT,
        //@PID	int ,
        //@ItemName	nvarchar(50),
        //@Data0	nvarchar(512),
        //@CRCData0	tinyint,
        //@Data1	nvarchar(512),
        //@CRCData1	tinyint,
        //@Data2	nvarchar(512),
        //@CRCData2	tinyint,
        //@Data3	nvarchar(512),
        //@CRCData3	tinyint,
        //@RowState	tinyint,
        //@OPlogPID	int,
        //@TracingInfo	nvarchar(max),
        //@myErr	int	OUTPUT
        private int Pro_TopoMSAEEPROMSet(int id, int pid, string itemName, string Data0, byte CRCData0,
            string Data1, byte CRCData1, string Data2, byte CRCData2, string Data3, byte CRCData3,
            byte rowStatus, int opLogPID, string TracingInfo, out int myErr)
        {
            myErr = -1; string errMsg = ""; SqlCommand command = new SqlCommand("Pro_TopoMSAEEPROMSet", conn);
            try
            {
                OpenDatabase(true);
                //执行存储过程 
                //SqlCommand command = new SqlCommand("Pro_TopoMSAEEPROMSet", conn);
                //说明命令要执行的是存储过程     
                command.CommandType = CommandType.StoredProcedure;
                //向存储过程中传递参数   

                command.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@PID", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@ItemName", SqlDbType.NVarChar, 50));
                command.Parameters.Add(new SqlParameter("@Data0", SqlDbType.NVarChar, 512));
                command.Parameters.Add(new SqlParameter("@CRCData0", SqlDbType.TinyInt));
                command.Parameters.Add(new SqlParameter("@Data1", SqlDbType.NVarChar, 512));
                command.Parameters.Add(new SqlParameter("@CRCData1", SqlDbType.TinyInt));
                command.Parameters.Add(new SqlParameter("@Data2", SqlDbType.NVarChar, 512));
                command.Parameters.Add(new SqlParameter("@CRCData2", SqlDbType.TinyInt));
                command.Parameters.Add(new SqlParameter("@Data3", SqlDbType.NVarChar, 512));
                command.Parameters.Add(new SqlParameter("@CRCData3", SqlDbType.TinyInt));

                command.Parameters.Add(new SqlParameter("@RowState", SqlDbType.TinyInt));


                command.Parameters.Add(new SqlParameter("@OPlogPID", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@TracingInfo", SqlDbType.NVarChar, 4000));
                command.Parameters.Add(new SqlParameter("@myErr", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@myErrMsg", SqlDbType.NVarChar, 4000));
                command.UpdatedRowSource = UpdateRowSource.None;
                command.Parameters["@ID"].Value = id;
                command.Parameters["@PID"].Value = pid;
                command.Parameters["@ItemName"].Value = itemName;
                command.Parameters["@Data0"].Value = Data0;
                command.Parameters["@CRCData0"].Value = CRCData0;
                command.Parameters["@Data1"].Value = Data1;
                command.Parameters["@CRCData1"].Value = CRCData1;
                command.Parameters["@Data2"].Value = Data2;
                command.Parameters["@CRCData2"].Value = CRCData2;
                command.Parameters["@Data3"].Value = Data3;
                command.Parameters["@CRCData3"].Value = CRCData3;

                command.Parameters["@RowState"].Value = rowStatus;


                command.Parameters["@OPlogPID"].Value = opLogPID;
                command.Parameters["@TracingInfo"].Value = TracingInfo;
                command.Parameters["@ID"].Direction = ParameterDirection.InputOutput;
                command.Parameters["@myErr"].Direction = ParameterDirection.Output;
                command.Parameters["@myErrMsg"].Direction = ParameterDirection.Output;
                command.ExecuteNonQuery();
                id = Convert.ToInt32(command.Parameters["@ID"].Value);
                myErr = Convert.ToInt32(command.Parameters["@myErr"].Value);
                errMsg = command.Parameters["@myErrMsg"].Value.ToString();    //OpenDatabase(false);
                if (myErr == 0)
                {
                    return id;
                }
                else
                {
                    return myErr;
                }
            }
            catch (Exception ex)
            {
                if (command.Parameters["@myErrMsg"].Value != null)
                {
                    errMsg = command.Parameters["@myErrMsg"].Value.ToString();
                    myErr = Convert.ToInt32(command.Parameters["@myErr"].Value);
                    WriteErrorLogs("Err:" + myErr + ";" + errMsg);
                }
                else
                {
                    WriteErrorLogs(ex.ToString());
                    errMsg = ex.ToString();
                }

                AlertMsgShow("Error:" + myErr + ";" + errMsg);
                return myErr;
            }
            finally
            {
                OpenDatabase(false);
            }
        }
        #endregion

        #region Pro_UserPNAction
        //存储过程 Pro_UserPNAction			
        //保存UserPNAction的资料			
        //参数	类型	方向	描述
        //@ID int	OUTPUT,
        //@UserID	int,
        //@PNID	int,
        //@AddPlan	bit,
        //@ModifyPN	bit,
        //@RowState	tinyint,
        //@OPlogPID	int,
        //@TracingInfo	nvarchar(max),
        //@myErr	int	OUTPUT,
        //@myErrMsg	nvarchar(max)	OUTPUT		
        private int Pro_UserPNAction(int id, int userID, int pnID, bool addPlan, bool modifyPN,
            byte rowStatus, int opLogPID, string TracingInfo, out int myErr)
        {
            myErr = -1; string errMsg = ""; SqlCommand command = new SqlCommand("Pro_UserPNAction", conn);
            try
            {
                OpenDatabase(true);
                //执行存储过程 
                //SqlCommand command = new SqlCommand("Pro_UserPNAction", conn);
                //说明命令要执行的是存储过程     
                command.CommandType = CommandType.StoredProcedure;
                //向存储过程中传递参数   int id, int userID, int pnID, bool addPlan, bool modifyPN

                command.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@PNID", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@AddPlan", SqlDbType.Bit));
                command.Parameters.Add(new SqlParameter("@ModifyPN", SqlDbType.Bit));

                command.Parameters.Add(new SqlParameter("@RowState", SqlDbType.TinyInt));


                command.Parameters.Add(new SqlParameter("@OPlogPID", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@TracingInfo", SqlDbType.NVarChar, 4000));
                command.Parameters.Add(new SqlParameter("@myErr", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@myErrMsg", SqlDbType.NVarChar, 4000));
                command.UpdatedRowSource = UpdateRowSource.None;
                command.Parameters["@ID"].Value = id;
                command.Parameters["@UserID"].Value = userID;
                command.Parameters["@PNID"].Value = pnID;
                command.Parameters["@AddPlan"].Value = addPlan;
                command.Parameters["@ModifyPN"].Value = modifyPN;
                command.Parameters["@RowState"].Value = rowStatus;


                command.Parameters["@OPlogPID"].Value = opLogPID;
                command.Parameters["@TracingInfo"].Value = TracingInfo;
                command.Parameters["@ID"].Direction = ParameterDirection.InputOutput;
                command.Parameters["@myErr"].Direction = ParameterDirection.Output;
                command.Parameters["@myErrMsg"].Direction = ParameterDirection.Output;
                command.ExecuteNonQuery();
                id = Convert.ToInt32(command.Parameters["@ID"].Value);
                myErr = Convert.ToInt32(command.Parameters["@myErr"].Value);
                errMsg = command.Parameters["@myErrMsg"].Value.ToString();    //OpenDatabase(false);
                if (myErr == 0)
                {
                    return id;
                }
                else
                {
                    return myErr;
                }
            }
            catch (Exception ex)
            {
                if (command.Parameters["@myErrMsg"].Value != null)
                {
                    errMsg = command.Parameters["@myErrMsg"].Value.ToString();
                    myErr = Convert.ToInt32(command.Parameters["@myErr"].Value);
                    WriteErrorLogs("Err:" + myErr + ";" + errMsg);
                }
                else
                {
                    WriteErrorLogs(ex.ToString());
                    errMsg = ex.ToString();
                }

                AlertMsgShow("Error:" + myErr + ";" + errMsg);
                return myErr;
            }
            finally
            {
                OpenDatabase(false);
            }
        }
        #endregion

        #region Pro_UserPlanAction
        //存储过程 Pro_UserPlanAction			
        //保存UserPlanAction的资料			
        //参数	类型	方向	描述
        //@ID int	OUTPUT,
        //@UserID	int,
        //@PlanID	int,
        //@ModifyPlan	bit,
        //@DeletePlan	bit,
        //@RunPlan	bit,
        //@RowState	tinyint,
        //@OPlogPID	int,
        //@TracingInfo	nvarchar(max),
        //@myErr	int	OUTPUT,
        //@myErrMsg	nvarchar(max)	OUTPUT		
        private int Pro_UserPlanAction(int id, int userID, int planID, bool modifyPlan, bool deletePlan, bool runPlan,
            byte rowStatus, int opLogPID, string TracingInfo, out int myErr)
        {
            myErr = -1; string errMsg = ""; SqlCommand command = new SqlCommand("Pro_UserPlanAction", conn);
            try
            {
                OpenDatabase(true);
                //执行存储过程 
                //SqlCommand command = new SqlCommand("Pro_UserPlanAction", conn);
                //说明命令要执行的是存储过程     
                command.CommandType = CommandType.StoredProcedure;
                //向存储过程中传递参数   int id, int userID, int planID, bool modifyPlan, bool deletePlan, bool runPlan

                command.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@PlanID", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@ModifyPlan", SqlDbType.Bit));
                command.Parameters.Add(new SqlParameter("@DeletePlan", SqlDbType.Bit));
                command.Parameters.Add(new SqlParameter("@RunPlan", SqlDbType.Bit));

                command.Parameters.Add(new SqlParameter("@RowState", SqlDbType.TinyInt));


                command.Parameters.Add(new SqlParameter("@OPlogPID", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@TracingInfo", SqlDbType.NVarChar, 4000));
                command.Parameters.Add(new SqlParameter("@myErr", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@myErrMsg", SqlDbType.NVarChar, 4000));
                command.UpdatedRowSource = UpdateRowSource.None;
                command.Parameters["@ID"].Value = id;
                command.Parameters["@UserID"].Value = userID;
                command.Parameters["@PlanID"].Value = planID;
                command.Parameters["@ModifyPlan"].Value = modifyPlan;
                command.Parameters["@DeletePlan"].Value = deletePlan;
                command.Parameters["@RunPlan"].Value = runPlan;
                command.Parameters["@RowState"].Value = rowStatus;


                command.Parameters["@OPlogPID"].Value = opLogPID;
                command.Parameters["@TracingInfo"].Value = TracingInfo;
                command.Parameters["@ID"].Direction = ParameterDirection.InputOutput;
                command.Parameters["@myErr"].Direction = ParameterDirection.Output;
                command.Parameters["@myErrMsg"].Direction = ParameterDirection.Output;
                command.ExecuteNonQuery();
                id = Convert.ToInt32(command.Parameters["@ID"].Value);
                myErr = Convert.ToInt32(command.Parameters["@myErr"].Value);
                errMsg = command.Parameters["@myErrMsg"].Value.ToString();    //OpenDatabase(false);
                if (myErr == 0)
                {
                    return id;
                }
                else
                {
                    return myErr;
                }
            }
            catch (Exception ex)
            {
                if (command.Parameters["@myErrMsg"].Value != null)
                {
                    errMsg = command.Parameters["@myErrMsg"].Value.ToString();
                    myErr = Convert.ToInt32(command.Parameters["@myErr"].Value);
                    WriteErrorLogs("Err:" + myErr + ";" + errMsg);
                }
                else
                {
                    WriteErrorLogs(ex.ToString());
                    errMsg = ex.ToString();
                }

                AlertMsgShow("Error:" + myErr + ";" + errMsg);
                return myErr;
            }
            finally
            {
                OpenDatabase(false);
            }
        }
        #endregion

        #region Pro_PNChipMap
        //存储过程 Pro_PNChipMap			
        //保存PNChipMap的资料			
        //参数	类型	方向	描述
        //@ID int	OUTPUT,
        //@PNID	int,
        //@ChipID	int,
        //@ChipRoleID	int,
        //@ChipDirection	tinyint,
        //@RowState	tinyint,
        //@OPlogPID	int,
        //@TracingInfo	nvarchar(max),
        //@myErr	int	OUTPUT,
        //@myErrMsg	nvarchar(max)	OUTPUT		
        private int Pro_PNChipMap(int id, int pnID, int chipID, int chipRoleID, byte chipDirection,
            byte rowStatus, int opLogPID, string TracingInfo, out int myErr)
        {
            myErr = -1; string errMsg = ""; SqlCommand command = new SqlCommand("Pro_PNChipMap", conn);
            try
            {
                OpenDatabase(true);
                //执行存储过程 
                //SqlCommand command = new SqlCommand("Pro_PNChipMap", conn);
                //说明命令要执行的是存储过程     
                command.CommandType = CommandType.StoredProcedure;
                //向存储过程中传递参数   int id, int pnID, int chipID, int chipRoleID, byte chipDirection

                command.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@PNID", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@ChipID", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@ChipRoleID", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@ChipDirection", SqlDbType.TinyInt));

                command.Parameters.Add(new SqlParameter("@RowState", SqlDbType.TinyInt));


                command.Parameters.Add(new SqlParameter("@OPlogPID", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@TracingInfo", SqlDbType.NVarChar, 4000));
                command.Parameters.Add(new SqlParameter("@myErr", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@myErrMsg", SqlDbType.NVarChar, 4000));
                command.UpdatedRowSource = UpdateRowSource.None;
                command.Parameters["@ID"].Value = id;
                command.Parameters["@PNID"].Value = pnID;
                command.Parameters["@ChipID"].Value = chipID;
                command.Parameters["@ChipRoleID"].Value = chipRoleID;
                command.Parameters["@ChipDirection"].Value = chipDirection;
                command.Parameters["@RowState"].Value = rowStatus;


                command.Parameters["@OPlogPID"].Value = opLogPID;
                command.Parameters["@TracingInfo"].Value = TracingInfo;
                command.Parameters["@ID"].Direction = ParameterDirection.InputOutput;
                command.Parameters["@myErr"].Direction = ParameterDirection.Output;
                command.Parameters["@myErrMsg"].Direction = ParameterDirection.Output;
                command.ExecuteNonQuery();
                id = Convert.ToInt32(command.Parameters["@ID"].Value);
                myErr = Convert.ToInt32(command.Parameters["@myErr"].Value);
                errMsg = command.Parameters["@myErrMsg"].Value.ToString();    //OpenDatabase(false);
                if (myErr == 0)
                {
                    return id;
                }
                else
                {
                    return myErr;
                }
            }
            catch (Exception ex)
            {
                if (command.Parameters["@myErrMsg"].Value != null)
                {
                    errMsg = command.Parameters["@myErrMsg"].Value.ToString();
                    myErr = Convert.ToInt32(command.Parameters["@myErr"].Value);
                    WriteErrorLogs("Err:" + myErr + ";" + errMsg);
                }
                else
                {
                    WriteErrorLogs(ex.ToString());
                    errMsg = ex.ToString();
                }

                AlertMsgShow("Error:" + myErr + ";" + errMsg);
                return myErr;
            }
            finally
            {
                OpenDatabase(false);
            }
        }
        #endregion

        #region Pro_RegisterFormula
        ////存储过程 Pro_RegisterFormula			
        ////保存RegisterFormula的资料			
        ////参数	类型	方向	描述
        ////@ID int	OUTPUT,
        ////@ChipID	int,
        ////@ItemName	nvarchar(50),
        ////@WriteFormula	nvarchar(200),
        ////@AnalogueUnit	nvarchar(20),
        ////@ReadFormula	nvarchar(200),
        ////@RowState	tinyint,
        ////@OPlogPID	int,
        ////@TracingInfo	nvarchar(max),
        ////@myErr	int	OUTPUT,
        ////@myErrMsg	nvarchar(max)	OUTPUT		
        //private int Pro_RegisterFormula(int id, int chipID, string itemName, string writeFormula, string analogueUnit,string readFormula,
        //    byte rowStatus, int opLogPID, string TracingInfo, out int myErr)
        //{
        //    myErr = -1; string errMsg = ""; SqlCommand command = new SqlCommand("Pro_RegisterFormula", conn);
        //    try
        //    {
        //        OpenDatabase(true);
        //        //执行存储过程 
        //        //SqlCommand command = new SqlCommand("Pro_RegisterFormula", conn);
        //        //说明命令要执行的是存储过程     
        //        command.CommandType = CommandType.StoredProcedure;
        //        //向存储过程中传递参数   int id, int chipID, string itemName, string writeFormula, string analogueUnit,string readFormula

        //        command.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
        //        command.Parameters.Add(new SqlParameter("@ChipID", SqlDbType.Int));
        //        command.Parameters.Add(new SqlParameter("@ItemName", SqlDbType.NVarChar, 50));
        //        command.Parameters.Add(new SqlParameter("@WriteFormula", SqlDbType.NVarChar, 200));
        //        command.Parameters.Add(new SqlParameter("@AnalogueUnit", SqlDbType.NVarChar, 20));
        //        command.Parameters.Add(new SqlParameter("@ReadFormula", SqlDbType.NVarChar,200));

        //        command.Parameters.Add(new SqlParameter("@RowState", SqlDbType.TinyInt));


        //        command.Parameters.Add(new SqlParameter("@OPlogPID", SqlDbType.Int));
        //        command.Parameters.Add(new SqlParameter("@TracingInfo", SqlDbType.NVarChar, 4000));
        //        command.Parameters.Add(new SqlParameter("@myErr", SqlDbType.Int));
        //        command.Parameters.Add(new SqlParameter("@myErrMsg", SqlDbType.NVarChar, 4000));
        //        command.UpdatedRowSource = UpdateRowSource.None;
        //        command.Parameters["@ID"].Value = id;
        //        command.Parameters["@ChipID"].Value = chipID;
        //        command.Parameters["@ItemName"].Value = itemName;
        //        command.Parameters["@WriteFormula"].Value = writeFormula;
        //        command.Parameters["@AnalogueUnit"].Value = analogueUnit;
        //        command.Parameters["@ReadFormula"].Value = readFormula;
        //        command.Parameters["@RowState"].Value = rowStatus;


        //        command.Parameters["@OPlogPID"].Value = opLogPID;
        //        command.Parameters["@TracingInfo"].Value = TracingInfo;
        //        command.Parameters["@ID"].Direction = ParameterDirection.InputOutput;
        //        command.Parameters["@myErr"].Direction = ParameterDirection.Output;
        //        command.Parameters["@myErrMsg"].Direction = ParameterDirection.Output;
        //        command.ExecuteNonQuery();
        //        id = Convert.ToInt32(command.Parameters["@ID"].Value);
        //        myErr = Convert.ToInt32(command.Parameters["@myErr"].Value);
        //        errMsg = command.Parameters["@myErrMsg"].Value.ToString();    //OpenDatabase(false);
        //        if (myErr == 0)
        //        {
        //            return id;
        //        }
        //        else
        //        {
        //            return myErr;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        if (command.Parameters["@myErrMsg"].Value != null)
        //        {
        //            errMsg = command.Parameters["@myErrMsg"].Value.ToString();
        //            myErr = Convert.ToInt32(command.Parameters["@myErr"].Value);
        //            WriteErrorLogs("Err:" + myErr + ";" + errMsg);
        //        }
        //        else
        //        {
        //            WriteErrorLogs(ex.ToString());
        //            errMsg = ex.ToString();
        //        }

        //        AlertMsgShow("Error:" + myErr + ";" + errMsg);
        //        return myErr;
        //    }
        //    finally
        //    {
        //        OpenDatabase(false);
        //    }
        //}
        #endregion

        #region Pro_ChipRegisterList
        //存储过程 Pro_ChipRegisterList			
        //保存Pro_ChipRegisterList的资料
        //参数	类型	方向	描述
        //@ID int	OUTPUT,
        //@ChipID	int,
        //@ItemName	nvarchar(50),
        //@WriteFormula	nvarchar(200),
        //@AnalogueUnit	nvarchar(20),
        //@ReadFormula	nvarchar(200),
        //@EndBit	int,
        //@Address	int,
        //@StartBit	int,
        //@UnitLength	int,
        //@ChipLine	int,
        //@RowState	tinyint,
        //@OPlogPID	int,
        //@TracingInfo	nvarchar(max),
        //@myErr	int	OUTPUT,
        //@myErrMsg	nvarchar(max)	OUTPUT		
        private int Pro_ChipRegisterList(int id, int chipID, string itemName, string writeFormula, string analogueUnit, string readFormula, int endBit, int address, int startBit, int unitLength, int chipLine,
            byte rowStatus, int opLogPID, string TracingInfo, out int myErr)
        {
            myErr = -1; string errMsg = ""; SqlCommand command = new SqlCommand("Pro_ChipRegisterList", conn);
            try
            {
                OpenDatabase(true);
                //执行存储过程 
                //SqlCommand command = new SqlCommand("Pro_ChipRegisterList", conn);
                //说明命令要执行的是存储过程     
                command.CommandType = CommandType.StoredProcedure;
                //向存储过程中传递参数   int id, int chipID, string itemName, string writeFormula, string analogueUnit,string readFormula, int endBit, int address, int startBit, int unitLength,int chipLine

                command.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@ChipID", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@ItemName", SqlDbType.NVarChar, 50));
                command.Parameters.Add(new SqlParameter("@WriteFormula", SqlDbType.NVarChar, 200));
                command.Parameters.Add(new SqlParameter("@AnalogueUnit", SqlDbType.NVarChar, 20));
                command.Parameters.Add(new SqlParameter("@ReadFormula", SqlDbType.NVarChar, 200));
                command.Parameters.Add(new SqlParameter("@EndBit", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@Address", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@StartBit", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@UnitLength", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@ChipLine", SqlDbType.Int));

                command.Parameters.Add(new SqlParameter("@RowState", SqlDbType.TinyInt));


                command.Parameters.Add(new SqlParameter("@OPlogPID", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@TracingInfo", SqlDbType.NVarChar, 4000));
                command.Parameters.Add(new SqlParameter("@myErr", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@myErrMsg", SqlDbType.NVarChar, 4000));
                command.UpdatedRowSource = UpdateRowSource.None;

                command.Parameters["@ID"].Value = id;
                command.Parameters["@ChipID"].Value = chipID;
                command.Parameters["@ItemName"].Value = itemName;
                command.Parameters["@WriteFormula"].Value = writeFormula;
                command.Parameters["@AnalogueUnit"].Value = analogueUnit;
                command.Parameters["@ReadFormula"].Value = readFormula;
                command.Parameters["@EndBit"].Value = endBit;
                command.Parameters["@Address"].Value = address;
                command.Parameters["@StartBit"].Value = startBit;
                command.Parameters["@UnitLength"].Value = unitLength;
                command.Parameters["@ChipLine"].Value = chipLine;
                command.Parameters["@RowState"].Value = rowStatus;


                command.Parameters["@OPlogPID"].Value = opLogPID;
                command.Parameters["@TracingInfo"].Value = TracingInfo;
                command.Parameters["@ID"].Direction = ParameterDirection.InputOutput;
                command.Parameters["@myErr"].Direction = ParameterDirection.Output;
                command.Parameters["@myErrMsg"].Direction = ParameterDirection.Output;
                command.ExecuteNonQuery();
                id = Convert.ToInt32(command.Parameters["@ID"].Value);
                myErr = Convert.ToInt32(command.Parameters["@myErr"].Value);
                errMsg = command.Parameters["@myErrMsg"].Value.ToString();    //OpenDatabase(false);
                if (myErr == 0)
                {
                    return id;
                }
                else
                {
                    return myErr;
                }
            }
            catch (Exception ex)
            {
                if (command.Parameters["@myErrMsg"].Value != null)
                {
                    errMsg = command.Parameters["@myErrMsg"].Value.ToString();
                    myErr = Convert.ToInt32(command.Parameters["@myErr"].Value);
                    WriteErrorLogs("Err:" + myErr + ";" + errMsg);
                }
                else
                {
                    WriteErrorLogs(ex.ToString());
                    errMsg = ex.ToString();
                }

                AlertMsgShow("Error:" + myErr + ";" + errMsg);
                return myErr;
            }
            finally
            {
                OpenDatabase(false);
            }
        }
        #endregion

        #region Pro_ChipBaseInfo
        //存储过程 Pro_ChipBaseInfo			
        //保存ChipBaseInfo的资料			
        //参数	类型	方向	描述
        //@ID int	OUTPUT,
        //@ItemName	nvarchar(50),
        //@Channels	tinyint,
        //@Description nvarchar(500),
        //@Width	tinyint,
        //@LittleEndian	bit,
        //@RowState	tinyint,
        //@OPlogPID	int,
        //@TracingInfo	nvarchar(max),
        //@myErr	int	OUTPUT,
        //@myErrMsg	nvarchar(max)	OUTPUT		
        private int Pro_ChipBaseInfo(int id, string itemName, byte channels, string description, byte width,bool littleEndian,
            byte rowStatus, int opLogPID, string TracingInfo, out int myErr)
        {
            myErr = -1; string errMsg = ""; SqlCommand command = new SqlCommand("Pro_ChipBaseInfo", conn);
            try
            {
                OpenDatabase(true);
                //执行存储过程 
                //SqlCommand command = new SqlCommand("Pro_ChipBaseInfo", conn);
                //说明命令要执行的是存储过程     
                command.CommandType = CommandType.StoredProcedure;
                //向存储过程中传递参数   int id, string ItemName, byte Channels, string Description, byte Width,bool LittleEndian

                command.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@ItemName", SqlDbType.NVarChar, 50));
                command.Parameters.Add(new SqlParameter("@Channels", SqlDbType.TinyInt));
                command.Parameters.Add(new SqlParameter("@Description", SqlDbType.NVarChar, 500));
                command.Parameters.Add(new SqlParameter("@Width", SqlDbType.TinyInt));
                command.Parameters.Add(new SqlParameter("@LittleEndian", SqlDbType.Bit));

                command.Parameters.Add(new SqlParameter("@RowState", SqlDbType.TinyInt));


                command.Parameters.Add(new SqlParameter("@OPlogPID", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@TracingInfo", SqlDbType.NVarChar, 4000));
                command.Parameters.Add(new SqlParameter("@myErr", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@myErrMsg", SqlDbType.NVarChar, 4000));
                command.UpdatedRowSource = UpdateRowSource.None;
                command.Parameters["@ID"].Value = id;
                command.Parameters["@ItemName"].Value = itemName;
                command.Parameters["@Channels"].Value = channels;
                command.Parameters["@Description"].Value = description;
                command.Parameters["@Width"].Value = width;
                command.Parameters["@LittleEndian"].Value = littleEndian;
                command.Parameters["@RowState"].Value = rowStatus;


                command.Parameters["@OPlogPID"].Value = opLogPID;
                command.Parameters["@TracingInfo"].Value = TracingInfo;
                command.Parameters["@ID"].Direction = ParameterDirection.InputOutput;
                command.Parameters["@myErr"].Direction = ParameterDirection.Output;
                command.Parameters["@myErrMsg"].Direction = ParameterDirection.Output;
                command.ExecuteNonQuery();
                id = Convert.ToInt32(command.Parameters["@ID"].Value);
                myErr = Convert.ToInt32(command.Parameters["@myErr"].Value);
                errMsg = command.Parameters["@myErrMsg"].Value.ToString();    //OpenDatabase(false);
                if (myErr == 0)
                {
                    return id;
                }
                else
                {
                    return myErr;
                }
            }
            catch (Exception ex)
            {
                if (command.Parameters["@myErrMsg"].Value != null)
                {
                    errMsg = command.Parameters["@myErrMsg"].Value.ToString();
                    myErr = Convert.ToInt32(command.Parameters["@myErr"].Value);
                    WriteErrorLogs("Err:" + myErr + ";" + errMsg);
                }
                else
                {
                    WriteErrorLogs(ex.ToString());
                    errMsg = ex.ToString();
                }

                AlertMsgShow("Error:" + myErr + ";" + errMsg);
                return myErr;
            }
            finally
            {
                OpenDatabase(false);
            }
        }
        #endregion

        #region Pro_ChannelMap
        //存储过程 Pro_ChannelMap			
        //保存ChannelMap的资料
        //参数	类型	方向	描述
        //@ID int	OUTPUT,
        //@PNChipID	int,
        //@ModuleLine	int,
        //@ChipLine	int,
        //@DebugLine    int,
        //@RowState	tinyint,
        //@OPlogPID	int,
        //@TracingInfo	nvarchar(max),
        //@myErr	int	OUTPUT,
        //@myErrMsg	nvarchar(max)	OUTPUT		
        private int Pro_ChannelMap(int id, int pnChipID, int moduleLine, int chipLine, int debugLine,
            byte rowStatus, int opLogPID, string TracingInfo, out int myErr)
        {
            myErr = -1; string errMsg = ""; SqlCommand command = new SqlCommand("Pro_ChannelMap", conn);
            try
            {
                OpenDatabase(true);
                //执行存储过程 
                //SqlCommand command = new SqlCommand("Pro_ChannelMap", conn);
                //说明命令要执行的是存储过程     
                command.CommandType = CommandType.StoredProcedure;
                //向存储过程中传递参数   int id, int PNChipID, int ModuleLine, int ChipLine, int debugLine

                command.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@PNChipID", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@ModuleLine", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@ChipLine", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@DebugLine", SqlDbType.Int));

                command.Parameters.Add(new SqlParameter("@RowState", SqlDbType.TinyInt));


                command.Parameters.Add(new SqlParameter("@OPlogPID", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@TracingInfo", SqlDbType.NVarChar, 4000));
                command.Parameters.Add(new SqlParameter("@myErr", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@myErrMsg", SqlDbType.NVarChar, 4000));
                command.UpdatedRowSource = UpdateRowSource.None;
                command.Parameters["@ID"].Value = id;
                command.Parameters["@PNChipID"].Value = pnChipID;
                command.Parameters["@ModuleLine"].Value = moduleLine;
                command.Parameters["@ChipLine"].Value = chipLine;
                command.Parameters["@DebugLine"].Value = debugLine;
                command.Parameters["@RowState"].Value = rowStatus;


                command.Parameters["@OPlogPID"].Value = opLogPID;
                command.Parameters["@TracingInfo"].Value = TracingInfo;
                command.Parameters["@ID"].Direction = ParameterDirection.InputOutput;
                command.Parameters["@myErr"].Direction = ParameterDirection.Output;
                command.Parameters["@myErrMsg"].Direction = ParameterDirection.Output;
                command.ExecuteNonQuery();
                id = Convert.ToInt32(command.Parameters["@ID"].Value);
                myErr = Convert.ToInt32(command.Parameters["@myErr"].Value);
                errMsg = command.Parameters["@myErrMsg"].Value.ToString();    //OpenDatabase(false);
                if (myErr == 0)
                {
                    return id;
                }
                else
                {
                    return myErr;
                }
            }
            catch (Exception ex)
            {
                if (command.Parameters["@myErrMsg"].Value != null)
                {
                    errMsg = command.Parameters["@myErrMsg"].Value.ToString();
                    myErr = Convert.ToInt32(command.Parameters["@myErr"].Value);
                    WriteErrorLogs("Err:" + myErr + ";" + errMsg);
                }
                else
                {
                    WriteErrorLogs(ex.ToString());
                    errMsg = ex.ToString();
                }

                AlertMsgShow("Error:" + myErr + ";" + errMsg);
                return myErr;
            }
            finally
            {
                OpenDatabase(false);
            }
        }
        #endregion

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
        public override int UpdateWithProc(string tableName, DataTable dt, string queryCMD,
            string tracingInfo, string databaseName = "ATS_V2")
        {
            int result = -1;
            int myErr = 0;
            int opLogPID = -1;
            GetXLMInfor();
            databaseName = dbName;
            try
            {
                #region get Session["UserLoginID"]
                if (HttpContext.Current.Session["UserLoginID"] != null)
                {
                    opLogPID = Convert.ToInt32(HttpContext.Current.Session["UserLoginID"].ToString());
                }
                else
                {
                    AlertMsgShow("Can not find any UserLoginID Info!");
                    return -100;
                    //throw new Exception("Can not find any UserLoginID Info!");
                }
                #endregion

                if (dt.GetChanges() != null)
                {
                    DataTable tempDt = dt.GetChanges();
                    #region "ATS_V2"
                    if (databaseName.ToUpper().Trim() == "ATS_V2" || databaseName.ToUpper().Trim() == dbName)
                    {
                        if (tableName.Trim().ToUpper() == "GlobalAllAppModelList".ToUpper())
                        {
                            #region Pro_GlobalAllAppModelList
                            if (tempDt != null)
                            {
                                foreach (DataRow dr in tempDt.Rows)
                                {
                                    int id; string itemName; string itemDescription;
                                    byte rowState = 1;

                                    if (dr.RowState == DataRowState.Deleted)
                                    {
                                        id = Convert.ToInt32(dr["ID", DataRowVersion.Original]);
                                        itemName = dr["ItemName", DataRowVersion.Original].ToString();
                                        itemDescription = dr["ItemDescription", DataRowVersion.Original].ToString();
                                        rowState = 2;
                                    }
                                    else
                                    {
                                        if (dr.RowState == DataRowState.Added)
                                        {
                                            rowState = 0;
                                        }
                                        else if (dr.RowState == DataRowState.Modified)
                                        {
                                            rowState = 1;
                                        }
                                        id = Convert.ToInt32(dr["ID"]);
                                        itemName = dr["ItemName"].ToString();
                                        itemDescription = dr["ItemDescription"].ToString();
                                    }
                                    result = Pro_GlobalAllAppModelList(id, itemName, itemDescription,
                                        rowState, opLogPID, tracingInfo, out myErr);
                                    if (myErr != 0)
                                    {
                                        break;
                                    }
                                }
                            }
                            #endregion
                        }
                        else if (tableName.Trim().ToUpper() == "GlobalAllTestModelList".ToUpper())
                        {
                            #region Pro_GlobalAllTestModelList
                            if (tempDt != null)
                            {
                                foreach (DataRow dr in tempDt.Rows)
                                {
                                    int id; int pid; string itemName; string showName; string itemDescription;
                                    byte rowState = 1;

                                    if (dr.RowState == DataRowState.Deleted)
                                    {
                                        id = Convert.ToInt32(dr["ID", DataRowVersion.Original]);
                                        pid = Convert.ToInt32(dr["PID", DataRowVersion.Original]);
                                        itemName = dr["ItemName", DataRowVersion.Original].ToString();
                                        showName = dr["ShowName", DataRowVersion.Original].ToString();
                                        itemDescription = dr["ItemDescription", DataRowVersion.Original].ToString();
                                        rowState = 2;
                                    }
                                    else
                                    {
                                        if (dr.RowState == DataRowState.Added)
                                        {
                                            rowState = 0;
                                        }
                                        else if (dr.RowState == DataRowState.Modified)
                                        {
                                            rowState = 1;
                                        }
                                        id = Convert.ToInt32(dr["ID"]);
                                        pid = Convert.ToInt32(dr["PID"]);
                                        itemName = dr["ItemName"].ToString();
                                        showName = dr["ShowName"].ToString();
                                        itemDescription = dr["ItemDescription"].ToString();
                                    }
                                    result = Pro_GlobalAllTestModelList(id, pid, itemName,showName, itemDescription,
                                        rowState, opLogPID, tracingInfo, out myErr);
                                    if (myErr != 0)
                                    {
                                        break;
                                    }
                                }
                            }
                            #endregion
                        }
                        else if (tableName.Trim().ToUpper() == "GlobalTestModelParamterList".ToUpper())
                        {
                            #region  Pro_GlobalTestModelParamterList
                            //private int Pro_GlobalTestModelParamterList(int id, int pid, string itemName, string itemType, string itemValue, bool needSelect, string optionalparams, string itemDescription,
                            //byte rowStatus, int opLogPID, string TracingInfo, out int myErr)
                            if (tempDt != null)
                            {
                                foreach (DataRow dr in tempDt.Rows)
                                {
                                    int id; int pid; string itemName; string showName; string itemType; string itemValue; bool needSelect; string optionalparams; string itemDescription;

                                    byte rowState = 1;
                                    if (dr.RowState == DataRowState.Deleted)
                                    {
                                        id = Convert.ToInt32(dr["ID", DataRowVersion.Original]);
                                        pid = Convert.ToInt32(dr["pid", DataRowVersion.Original]);
                                        itemName = dr["itemName", DataRowVersion.Original].ToString();
                                        showName = dr["showName", DataRowVersion.Original].ToString();
                                        itemType = dr["itemType", DataRowVersion.Original].ToString();
                                        itemValue = dr["itemValue", DataRowVersion.Original].ToString();
                                        needSelect = Convert.ToBoolean(dr["needSelect", DataRowVersion.Original]);
                                        optionalparams = dr["optionalparams", DataRowVersion.Original].ToString();
                                        itemDescription = dr["itemDescription", DataRowVersion.Original].ToString();
                                        rowState = 2;
                                    }
                                    else
                                    {
                                        if (dr.RowState == DataRowState.Added)
                                        {
                                            rowState = 0;
                                        }
                                        else if (dr.RowState == DataRowState.Modified)
                                        {
                                            rowState = 1;
                                        }
                                        id = Convert.ToInt32(dr["ID"]);
                                        pid = Convert.ToInt32(dr["pid"]);
                                        itemName = dr["itemName"].ToString();
                                        showName = dr["showName"].ToString();
                                        itemType = dr["itemType"].ToString();
                                        itemValue = dr["itemValue"].ToString();
                                        needSelect = Convert.ToBoolean(dr["needSelect"]);
                                        optionalparams = dr["optionalparams"].ToString();
                                        itemDescription = dr["itemDescription"].ToString();
                                    }
                                    result = Pro_GlobalTestModelParamterList(id, pid, itemName, showName, itemType, itemValue, needSelect, optionalparams, itemDescription,
                                        rowState, opLogPID, tracingInfo, out myErr);
                                    if (myErr != 0)
                                    {
                                        break;
                                    }
                                }
                            }
                            #endregion
                        }
                        else if (tableName.Trim().ToUpper() == "GlobalAllEquipmentList".ToUpper())
                        {
                            #region Pro_GlobalAllEquipmentList
                            //private int Pro_GlobalAllEquipmentList(int id, string itemName, string itemType, string itemDescription,
                            //byte rowStatus, int opLogPID, string TracingInfo, out int myErr)
                            if (tempDt != null)
                            {
                                foreach (DataRow dr in tempDt.Rows)
                                {
                                    int id; string itemName; string showName; string itemType; string itemDescription;

                                    byte rowState = 1;
                                    if (dr.RowState == DataRowState.Deleted)
                                    {
                                        id = Convert.ToInt32(dr["ID", DataRowVersion.Original]);
                                        itemName = dr["ItemName", DataRowVersion.Original].ToString();
                                        showName = dr["ShowName", DataRowVersion.Original].ToString();
                                        itemType = dr["itemType", DataRowVersion.Original].ToString();
                                        itemDescription = dr["ItemDescription", DataRowVersion.Original].ToString();
                                        rowState = 2;
                                    }
                                    else
                                    {
                                        if (dr.RowState == DataRowState.Added)
                                        {
                                            rowState = 0;
                                        }
                                        else if (dr.RowState == DataRowState.Modified)
                                        {
                                            rowState = 1;
                                        }
                                        id = Convert.ToInt32(dr["ID"]);
                                        itemName = dr["ItemName"].ToString();
                                        showName = dr["ShowName"].ToString();
                                        itemType = dr["itemType"].ToString();
                                        itemDescription = dr["ItemDescription"].ToString();
                                    }
                                    result = Pro_GlobalAllEquipmentList(id, itemName,showName, itemType, itemDescription,
                                        rowState, opLogPID, tracingInfo, out myErr);
                                    if (myErr != 0)
                                    {
                                        break;
                                    }
                                }
                            }
                            #endregion
                        }
                        else if (tableName.Trim().ToUpper() == "GlobalAllEquipmentParamterList".ToUpper())
                        {
                            #region  Pro_GlobalAllEquipmentParamterList
                            //private int Pro_GlobalAllEquipmentParamterList(int id, int pid, string itemName, string itemType, string itemValue, bool needSelect, string optionalparams, string itemDescription,
                            //byte rowStatus, int opLogPID, string TracingInfo, out int myErr)
                            if (tempDt != null)
                            {
                                foreach (DataRow dr in tempDt.Rows)
                                {
                                    int id; int pid; string itemName; string showName; string itemType; string itemValue; bool needSelect; string optionalparams; string itemDescription;

                                    byte rowState = 1;
                                    if (dr.RowState == DataRowState.Deleted)
                                    {
                                        id = Convert.ToInt32(dr["ID", DataRowVersion.Original]);
                                        pid = Convert.ToInt32(dr["pid", DataRowVersion.Original]);
                                        itemName = dr["itemName", DataRowVersion.Original].ToString();
                                        showName = dr["showName", DataRowVersion.Original].ToString();
                                        itemType = dr["itemType", DataRowVersion.Original].ToString();
                                        itemValue = dr["itemValue", DataRowVersion.Original].ToString();
                                        needSelect = Convert.ToBoolean(dr["needSelect", DataRowVersion.Original]);
                                        optionalparams = dr["optionalparams", DataRowVersion.Original].ToString();
                                        itemDescription = dr["itemDescription", DataRowVersion.Original].ToString();
                                        rowState = 2;
                                    }
                                    else
                                    {
                                        if (dr.RowState == DataRowState.Added)
                                        {
                                            rowState = 0;
                                        }
                                        else if (dr.RowState == DataRowState.Modified)
                                        {
                                            rowState = 1;
                                        }
                                        id = Convert.ToInt32(dr["ID"]);
                                        pid = Convert.ToInt32(dr["pid"]);
                                        itemName = dr["itemName"].ToString();
                                        showName = dr["showName"].ToString();
                                        itemType = dr["itemType"].ToString();
                                        itemValue = dr["itemValue"].ToString();
                                        needSelect = Convert.ToBoolean(dr["needSelect"]);
                                        optionalparams = dr["optionalparams"].ToString();
                                        itemDescription = dr["itemDescription"].ToString();
                                    }
                                    result = Pro_GlobalAllEquipmentParamterList(id, pid, itemName, showName, itemType, itemValue, needSelect, optionalparams, itemDescription,
                                        rowState, opLogPID, tracingInfo, out myErr);
                                    if (myErr != 0)
                                    {
                                        break;
                                    }
                                }
                            }
                            #endregion
                        }
                        else if (tableName.Trim().ToUpper() == "GlobalMSA".ToUpper())
                        {
                            #region Pro_GlobalMSA
                            //private int Pro_GlobalMSA(int id, string itemName, string accessInterface, int slaveAddress, bool ignoreFlag,
                            //byte rowStatus, int opLogPID, string TracingInfo, out int myErr)
                            if (tempDt != null)
                            {
                                foreach (DataRow dr in tempDt.Rows)
                                {
                                    int ID = -1, slaveAddress;
                                    bool ignoreFlag;
                                    byte rowState = 1;
                                    string itemName, accessInterface;
                                    if (dr.RowState == DataRowState.Deleted)
                                    {
                                        ID = Convert.ToInt32(dr["ID", DataRowVersion.Original]);
                                        itemName = dr["ItemName", DataRowVersion.Original].ToString();
                                        accessInterface = dr["accessInterface", DataRowVersion.Original].ToString();
                                        slaveAddress = Convert.ToInt32(dr["slaveAddress", DataRowVersion.Original]);
                                        ignoreFlag = Convert.ToBoolean(dr["ignoreFlag", DataRowVersion.Original]);
                                        rowState = 2;
                                    }
                                    else
                                    {
                                        if (dr.RowState == DataRowState.Added)
                                        {
                                            rowState = 0;
                                        }
                                        else if (dr.RowState == DataRowState.Modified)
                                        {
                                            rowState = 1;
                                            ID = Convert.ToInt32(dr["ID"]);
                                        }

                                        itemName = dr["ItemName"].ToString();
                                        accessInterface = dr["accessInterface"].ToString();
                                        slaveAddress = Convert.ToInt32(dr["slaveAddress"]);
                                        ignoreFlag = Convert.ToBoolean(dr["ignoreFlag"]);
                                    }
                                    result = Pro_GlobalMSA(ID, itemName, accessInterface, slaveAddress, ignoreFlag,
                                        rowState, opLogPID, tracingInfo, out myErr);
                                    if (myErr != 0)
                                    {
                                        break;
                                    }
                                }
                            }
                            #endregion
                        }
                        else if (tableName.Trim().ToUpper() == "GlobalMSADefintionInf".ToUpper())
                        {
                            #region Pro_GlobalMSADefintionInf
                            //private int Pro_GlobalMSADefintionInf(int id, int pid, string fieldName, byte channel,
                            //int slaveAddress, byte page, int startAddress, byte length, string format,
                            //byte rowStatus, int opLogPID, string TracingInfo, out int myErr)
                            if (tempDt != null)
                            {
                                foreach (DataRow dr in tempDt.Rows)
                                {
                                    int ID, pid, slaveAddress, startAddress;
                                    //bool ignoreFlag;
                                    byte channel, page, length;
                                    string fieldName, format;

                                    byte rowState = 1;
                                    if (dr.RowState == DataRowState.Deleted)
                                    {
                                        ID = Convert.ToInt32(dr["ID", DataRowVersion.Original]);
                                        pid = Convert.ToInt32(dr["pid", DataRowVersion.Original]);
                                        fieldName = dr["fieldName", DataRowVersion.Original].ToString();
                                        channel = Convert.ToByte(dr["channel", DataRowVersion.Original]);
                                        slaveAddress = Convert.ToInt32(dr["slaveAddress", DataRowVersion.Original]);
                                        page = Convert.ToByte(dr["page", DataRowVersion.Original]);
                                        startAddress = Convert.ToInt32(dr["startAddress", DataRowVersion.Original]);
                                        length = Convert.ToByte(dr["length", DataRowVersion.Original]);
                                        format = dr["format", DataRowVersion.Original].ToString();
                                        rowState = 2;
                                    }
                                    else
                                    {
                                        if (dr.RowState == DataRowState.Added)
                                        {
                                            rowState = 0;
                                        }
                                        else if (dr.RowState == DataRowState.Modified)
                                        {
                                            rowState = 1;
                                        }
                                        ID = Convert.ToInt32(dr["ID"]);
                                        pid = Convert.ToInt32(dr["pid"]);
                                        fieldName = dr["fieldName"].ToString();
                                        channel = Convert.ToByte(dr["channel"]);
                                        slaveAddress = Convert.ToInt32(dr["slaveAddress"]);
                                        page = Convert.ToByte(dr["page"]);
                                        startAddress = Convert.ToInt32(dr["startAddress"]);
                                        length = Convert.ToByte(dr["length"]);
                                        format = dr["format"].ToString();
                                    }
                                    result = Pro_GlobalMSADefintionInf(ID, pid, fieldName, channel, slaveAddress, page, startAddress, length, format,
                                        rowState, opLogPID, tracingInfo, out myErr);
                                    if (myErr != 0)
                                    {
                                        break;
                                    }
                                }
                            }
                            #endregion
                        }
                        else if (tableName.Trim().ToUpper() == "GlobalProductionType".ToUpper())
                        {
                            #region Pro_GlobalProductionType
                            //private int Pro_GlobalProductionType(int id, string itemName, int msaID, bool ignoreFlag,
                            //byte rowStatus, int opLogPID, string TracingInfo, out int myErr)
                            if (tempDt != null)
                            {
                                foreach (DataRow dr in tempDt.Rows)
                                {
                                    int ID = -1, msaID;
                                    bool ignoreFlag;
                                    byte rowState = 1;
                                    string itemName;
                                    if (dr.RowState == DataRowState.Deleted)
                                    {
                                        ID = Convert.ToInt32(dr["ID", DataRowVersion.Original]);
                                        itemName = dr["ItemName", DataRowVersion.Original].ToString();
                                        msaID = Convert.ToInt32(dr["msaID", DataRowVersion.Original].ToString());
                                        ignoreFlag = Convert.ToBoolean(dr["ignoreFlag", DataRowVersion.Original]);
                                        rowState = 2;
                                    }
                                    else
                                    {
                                        if (dr.RowState == DataRowState.Added)
                                        {
                                            rowState = 0;
                                        }
                                        else if (dr.RowState == DataRowState.Modified)
                                        {
                                            rowState = 1;
                                            ID = Convert.ToInt32(dr["ID"]);
                                        }

                                        itemName = dr["ItemName"].ToString();
                                        msaID = Convert.ToInt32(dr["msaID"].ToString());
                                        ignoreFlag = Convert.ToBoolean(dr["ignoreFlag"]);
                                    }
                                    result = Pro_GlobalProductionType(ID, itemName, msaID, ignoreFlag,
                                        rowState, opLogPID, tracingInfo, out myErr);
                                    if (myErr != 0)
                                    {
                                        break;
                                    }
                                }
                            }
                            #endregion
                        }
                        else if (tableName.Trim().ToUpper() == "GlobalProductionName".ToUpper())
                        {
                            #region Pro_GlobalProductionName
                            //private int Pro_GlobalProductionName(int id, int pid, string pn, string itemName, byte channels, byte voltages,
                            //byte tsensors, int mCoefsID, bool ignoreFlag, byte oldDriver, byte apcStyle, byte tec_Present, byte couple_Type,
                            //byte apc_Type, byte ber, byte maxRate, string publish_PN, string nickName, string ibiasFormula, string iModFormula, bool usingCelsiusTemp, float rxOverLoadDBm, 
                            //byte rowStatus, int opLogPID, string TracingInfo, out int myErr)
                            if (tempDt != null)
                            {
                                foreach (DataRow dr in tempDt.Rows)
                                {
                                    int id; int pid; string pn; string itemName; byte channels; byte voltages;
                                    byte tsensors; int mCoefsID; bool ignoreFlag; byte oldDriver;  byte tec_Present; byte couple_Type;
                                    byte apc_Type; byte ber; byte maxRate; string publish_PN; string nickName; string ibiasFormula; string iModFormula; bool usingCelsiusTemp; float rxOverLoadDBm;
                                    //byte apcStyle;
                                    byte rowState = 1;
                                    if (dr.RowState == DataRowState.Deleted)
                                    {
                                        id = Convert.ToInt32(dr["ID", DataRowVersion.Original]);
                                        pid = Convert.ToInt32(dr["pid", DataRowVersion.Original]);
                                        pn = dr["pn", DataRowVersion.Original].ToString();
                                        itemName = dr["itemName", DataRowVersion.Original].ToString();
                                        channels = Convert.ToByte(dr["channels", DataRowVersion.Original]);
                                        voltages = Convert.ToByte(dr["voltages", DataRowVersion.Original]);
                                        tsensors = Convert.ToByte(dr["tsensors", DataRowVersion.Original]);
                                        mCoefsID = Convert.ToInt32(dr["mCoefsID", DataRowVersion.Original]);
                                        ignoreFlag = Convert.ToBoolean(dr["ignoreFlag", DataRowVersion.Original]);
                                        oldDriver = Convert.ToByte(dr["oldDriver", DataRowVersion.Original]);
                                        //apcStyle = Convert.ToByte(dr["apcStyle", DataRowVersion.Original]);
                                        tec_Present = Convert.ToByte(dr["tec_Present", DataRowVersion.Original]);
                                        couple_Type = Convert.ToByte(dr["couple_Type", DataRowVersion.Original]);
                                        apc_Type = Convert.ToByte(dr["apc_Type", DataRowVersion.Original]);
                                        ber = Convert.ToByte(dr["ber", DataRowVersion.Original]);
                                        maxRate = Convert.ToByte(dr["maxRate", DataRowVersion.Original]);

                                        publish_PN = dr["publish_PN", DataRowVersion.Original].ToString();
                                        nickName = dr["nickName", DataRowVersion.Original].ToString();
                                        ibiasFormula = dr["ibiasFormula", DataRowVersion.Original].ToString();
                                        iModFormula = dr["iModFormula", DataRowVersion.Original].ToString();
                                        usingCelsiusTemp = Convert.ToBoolean(dr["usingCelsiusTemp", DataRowVersion.Original]);
                                        rxOverLoadDBm = Convert.ToSingle(dr["rxOverLoadDBm", DataRowVersion.Original]);
                                        rowState = 2;
                                    }
                                    else
                                    {
                                        if (dr.RowState == DataRowState.Added)
                                        {
                                            rowState = 0;
                                        }
                                        else if (dr.RowState == DataRowState.Modified)
                                        {
                                            rowState = 1;
                                        }
                                        id = Convert.ToInt32(dr["ID"]);
                                        pid = Convert.ToInt32(dr["pid"]);
                                        pn = dr["pn"].ToString();
                                        itemName = dr["itemName"].ToString();
                                        channels = Convert.ToByte(dr["channels"]);
                                        voltages = Convert.ToByte(dr["voltages"]);
                                        tsensors = Convert.ToByte(dr["tsensors"]);
                                        mCoefsID = Convert.ToInt32(dr["mCoefsID"]);
                                        ignoreFlag = Convert.ToBoolean(dr["ignoreFlag"]);
                                        oldDriver = Convert.ToByte(dr["oldDriver"]);
                                        //apcStyle = Convert.ToByte(dr["apcStyle"]);
                                        tec_Present = Convert.ToByte(dr["tec_Present"]);
                                        couple_Type = Convert.ToByte(dr["couple_Type"]);
                                        apc_Type = Convert.ToByte(dr["apc_Type"]);
                                        ber = Convert.ToByte(dr["ber"]);
                                        maxRate = Convert.ToByte(dr["maxRate"]);

                                        publish_PN = dr["publish_PN"].ToString();
                                        nickName = dr["nickName"].ToString();
                                        ibiasFormula = dr["ibiasFormula"].ToString();
                                        iModFormula = dr["iModFormula"].ToString();
                                        usingCelsiusTemp = Convert.ToBoolean(dr["usingCelsiusTemp"]);
                                        rxOverLoadDBm = Convert.ToSingle(dr["rxOverLoadDBm"]);
                                    }
                                    result = Pro_GlobalProductionName(id, pid, pn, itemName, channels, voltages, tsensors, mCoefsID, ignoreFlag,
                                        oldDriver,  tec_Present, couple_Type, apc_Type, ber, maxRate, publish_PN, nickName, ibiasFormula, iModFormula, usingCelsiusTemp, rxOverLoadDBm,
                                        rowState, opLogPID, tracingInfo, out myErr);    //apcStyle,
                                    if (myErr != 0)
                                    {
                                        break;
                                    }
                                }
                            }
                            #endregion
                        }
                        else if (tableName.Trim().ToUpper() == "GlobalManufactureChipsetInitialize".ToUpper())
                        {
                            #region Pro_GlobalManufactureChipsetInitialize
                            //private int Pro_GlobalManufactureChipsetInitialize(int id, int pid, byte driveType, byte chipLine,
                            //int registerAddress, byte length, int itemValue, bool endianness,
                            //byte rowStatus, int opLogPID, string TracingInfo, out int myErr)
                            if (tempDt != null)
                            {
                                foreach (DataRow dr in tempDt.Rows)
                                {
                                    int id; int pid; byte driveType; byte chipLine;
                                    int registerAddress; byte length; int itemValue; bool endianness;

                                    byte rowState = 1;
                                    if (dr.RowState == DataRowState.Deleted)
                                    {
                                        id = Convert.ToInt32(dr["ID", DataRowVersion.Original]);
                                        pid = Convert.ToInt32(dr["pid", DataRowVersion.Original]);
                                        driveType = Convert.ToByte(dr["driveType", DataRowVersion.Original]);
                                        chipLine = Convert.ToByte(dr["chipLine", DataRowVersion.Original]);
                                        registerAddress = Convert.ToInt32(dr["registerAddress", DataRowVersion.Original]);
                                        length = Convert.ToByte(dr["length", DataRowVersion.Original]);
                                        itemValue = Convert.ToInt32(dr["itemValue", DataRowVersion.Original]);
                                        endianness = Convert.ToBoolean(dr["endianness", DataRowVersion.Original]);
                                        rowState = 2;
                                    }
                                    else
                                    {
                                        if (dr.RowState == DataRowState.Added)
                                        {
                                            rowState = 0;
                                        }
                                        else if (dr.RowState == DataRowState.Modified)
                                        {
                                            rowState = 1;
                                        }
                                        id = Convert.ToInt32(dr["ID"]);
                                        pid = Convert.ToInt32(dr["pid"]);
                                        driveType = Convert.ToByte(dr["driveType"]);
                                        chipLine = Convert.ToByte(dr["chipLine"]);
                                        registerAddress = Convert.ToInt32(dr["registerAddress"]);
                                        length = Convert.ToByte(dr["length"]);
                                        itemValue = Convert.ToInt32(dr["itemValue"]);
                                        endianness = Convert.ToBoolean(dr["endianness"]);
                                    }
                                    result = Pro_GlobalManufactureChipsetInitialize(id, pid, driveType, chipLine, registerAddress, length, itemValue, endianness,
                                        rowState, opLogPID, tracingInfo, out myErr);
                                    if (myErr != 0)
                                    {
                                        break;
                                    }
                                }
                            }
                            #endregion
                        }
                        else if (tableName.Trim().ToUpper() == "GlobalManufactureChipsetControl".ToUpper())
                        {
                            #region Pro_GlobalManufactureChipsetControl
                            //private int Pro_GlobalManufactureChipsetControl(int id, int pid, string itemName, byte moduleLine, byte chipLine, byte driveType,
                            //int registerAddress, byte length, bool endianness,
                            //byte rowStatus, int opLogPID, string TracingInfo, out int myErr)
                            if (tempDt != null)
                            {
                                foreach (DataRow dr in tempDt.Rows)
                                {
                                    int id; int pid; string itemName; byte moduleLine; byte chipLine; byte driveType;
                                    int registerAddress; byte length; bool endianness;

                                    byte rowState = 1;
                                    if (dr.RowState == DataRowState.Deleted)
                                    {
                                        id = Convert.ToInt32(dr["ID", DataRowVersion.Original]);
                                        pid = Convert.ToInt32(dr["pid", DataRowVersion.Original]);
                                        itemName = dr["itemName", DataRowVersion.Original].ToString();
                                        moduleLine = Convert.ToByte(dr["moduleLine", DataRowVersion.Original]);
                                        chipLine = Convert.ToByte(dr["chipLine", DataRowVersion.Original]);
                                        driveType = Convert.ToByte(dr["driveType", DataRowVersion.Original]);
                                        registerAddress = Convert.ToInt32(dr["registerAddress", DataRowVersion.Original]);
                                        length = Convert.ToByte(dr["length", DataRowVersion.Original]);
                                        endianness = Convert.ToBoolean(dr["endianness", DataRowVersion.Original]);
                                        rowState = 2;
                                    }
                                    else
                                    {
                                        if (dr.RowState == DataRowState.Added)
                                        {
                                            rowState = 0;
                                        }
                                        else if (dr.RowState == DataRowState.Modified)
                                        {
                                            rowState = 1;
                                        }
                                        id = Convert.ToInt32(dr["ID"]);
                                        pid = Convert.ToInt32(dr["pid"]);
                                        itemName = dr["itemName"].ToString();
                                        moduleLine = Convert.ToByte(dr["moduleLine"]);
                                        chipLine = Convert.ToByte(dr["chipLine"]);
                                        driveType = Convert.ToByte(dr["driveType"]);
                                        registerAddress = Convert.ToInt32(dr["registerAddress"]);
                                        length = Convert.ToByte(dr["length"]);
                                        endianness = Convert.ToBoolean(dr["endianness"]);
                                    }
                                    result = Pro_GlobalManufactureChipsetControl(id, pid, itemName, moduleLine, chipLine, driveType, registerAddress, length, endianness,
                                        rowState, opLogPID, tracingInfo, out myErr);
                                    if (myErr != 0)
                                    {
                                        break;
                                    }
                                }
                            }
                            #endregion
                        }
                        else if (tableName.Trim().ToUpper() == "TopoMSAEEPROMSet".ToUpper())
                        {
                            #region  Pro_TopoMSAEEPROMSet
                            //private int Pro_TopoMSAEEPROMSet(int id, int pid, string itemName, string Data0, byte CRCData0,
                            //string Data1, byte CRCData1, string Data2, byte CRCData2, string Data3, byte CRCData3,
                            //byte rowStatus, int opLogPID, string TracingInfo, out int myErr)
                            if (tempDt != null)
                            {
                                foreach (DataRow dr in tempDt.Rows)
                                {
                                    int id; int pid; string itemName; string Data0; byte CRCData0;
                                    string Data1; byte CRCData1; string Data2; byte CRCData2; string Data3; byte CRCData3;

                                    byte rowState = 1;
                                    if (dr.RowState == DataRowState.Deleted)
                                    {
                                        id = Convert.ToInt32(dr["ID", DataRowVersion.Original]);
                                        pid = Convert.ToInt32(dr["pid", DataRowVersion.Original]);
                                        itemName = dr["itemName", DataRowVersion.Original].ToString();
                                        Data0 = dr["Data0", DataRowVersion.Original].ToString();                                        
                                        CRCData0 = Convert.ToByte(dr["CRCData0", DataRowVersion.Original]);
                                        Data1 = dr["Data1", DataRowVersion.Original].ToString();
                                        CRCData1 = Convert.ToByte(dr["CRCData1", DataRowVersion.Original]);
                                        Data2 = dr["Data2", DataRowVersion.Original].ToString();
                                        CRCData2 = Convert.ToByte(dr["CRCData2", DataRowVersion.Original]);
                                        Data3 = dr["Data3", DataRowVersion.Original].ToString();
                                        CRCData3 = Convert.ToByte(dr["CRCData3", DataRowVersion.Original]);
                                        rowState = 2;
                                    }
                                    else
                                    {
                                        if (dr.RowState == DataRowState.Added)
                                        {
                                            rowState = 0;
                                        }
                                        else if (dr.RowState == DataRowState.Modified)
                                        {
                                            rowState = 1;
                                        }
                                        id = Convert.ToInt32(dr["ID"]);
                                        pid = Convert.ToInt32(dr["pid"]);
                                        itemName = dr["itemName"].ToString();
                                        Data0 = dr["Data0"].ToString();
                                        CRCData0 = Convert.ToByte(dr["CRCData0"]);
                                        Data1 = dr["Data1"].ToString();
                                        CRCData1 = Convert.ToByte(dr["CRCData1"]);
                                        Data2 = dr["Data2"].ToString();
                                        CRCData2 = Convert.ToByte(dr["CRCData2"]);
                                        Data3 = dr["Data3"].ToString();
                                        CRCData3 = Convert.ToByte(dr["CRCData3"]);
                                    }
                                    result = Pro_TopoMSAEEPROMSet(id, pid, itemName
                                        , Data0, CRCData0
                                        , Data1, CRCData1
                                        , Data2, CRCData2
                                        , Data3, CRCData3,
                                        rowState, opLogPID, tracingInfo, out myErr);
                                    if (myErr != 0)
                                    {
                                        break;
                                    }
                                }
                            }
                            #endregion
                        }
                        else if (tableName.Trim().ToUpper() == "TopoTestPlan".ToUpper())
                        {
                            #region  Pro_TopoTestPlan
                            //private int Pro_TopoTestPlan(int id, int pid, string itemName, string swVersion, string hwVersion, byte usbPort, bool isChipInitialize,
                            //bool isEEPROMInitialize, bool ignoreBackupCoef, bool snCheck, bool ignoreFlag, string itemDescription,
                            //byte rowStatus, int opLogPID, string TracingInfo, out int myErr)
                            if (tempDt != null)
                            {
                                foreach (DataRow dr in tempDt.Rows)
                                {
                                    int id; int pid; string itemName; string swVersion; string hwVersion; byte usbPort; bool isChipInitialize;
                                    bool isEEPROMInitialize; bool ignoreBackupCoef; bool snCheck; bool ignoreFlag; string itemDescription;

                                    byte rowState = 1;
                                    if (dr.RowState == DataRowState.Deleted)
                                    {
                                        id = Convert.ToInt32(dr["ID", DataRowVersion.Original]);
                                        pid = Convert.ToInt32(dr["pid", DataRowVersion.Original]);
                                        itemName = dr["itemName", DataRowVersion.Original].ToString();
                                        swVersion = dr["swVersion", DataRowVersion.Original].ToString();
                                        hwVersion = dr["hwVersion", DataRowVersion.Original].ToString();
                                        usbPort = Convert.ToByte(dr["usbPort", DataRowVersion.Original]);
                                        isChipInitialize = Convert.ToBoolean(dr["isChipInitialize", DataRowVersion.Original]);
                                        isEEPROMInitialize = Convert.ToBoolean(dr["isEEPROMInitialize", DataRowVersion.Original]);
                                        ignoreBackupCoef = Convert.ToBoolean(dr["ignoreBackupCoef", DataRowVersion.Original]);
                                        snCheck = Convert.ToBoolean(dr["snCheck", DataRowVersion.Original]);
                                        ignoreFlag = Convert.ToBoolean(dr["ignoreFlag", DataRowVersion.Original]);
                                        itemDescription = dr["itemDescription", DataRowVersion.Original].ToString();
                                        rowState = 2;
                                    }
                                    else
                                    {
                                        if (dr.RowState == DataRowState.Added)
                                        {
                                            rowState = 0;
                                        }
                                        else if (dr.RowState == DataRowState.Modified)
                                        {
                                            rowState = 1;
                                        }
                                        id = Convert.ToInt32(dr["ID"]);
                                        pid = Convert.ToInt32(dr["pid"]);
                                        itemName = dr["itemName"].ToString();
                                        swVersion = dr["swVersion"].ToString();
                                        hwVersion = dr["hwVersion"].ToString();
                                        usbPort = Convert.ToByte(dr["usbPort"]);
                                        isChipInitialize = Convert.ToBoolean(dr["isChipInitialize"]);
                                        isEEPROMInitialize = Convert.ToBoolean(dr["isEEPROMInitialize"]);
                                        ignoreBackupCoef = Convert.ToBoolean(dr["ignoreBackupCoef"]);
                                        snCheck = Convert.ToBoolean(dr["snCheck"]);
                                        ignoreFlag = Convert.ToBoolean(dr["ignoreFlag"]);
                                        itemDescription = dr["itemDescription"].ToString();
                                    }
                                    result = Pro_TopoTestPlan(id, pid, itemName, swVersion, hwVersion, usbPort,
                                        isChipInitialize, isEEPROMInitialize, ignoreBackupCoef, snCheck, ignoreFlag, itemDescription,
                                        rowState, opLogPID, tracingInfo, out myErr);
                                    if (myErr != 0)
                                    {
                                        break;
                                    }
                                }
                            }
                            #endregion
                        }
                        else if (tableName.Trim().ToUpper() == "TopoTestControl".ToUpper())
                        {
                            #region  Pro_TopoTestControl
                            //private int Pro_TopoTestControl(int id, int pid, string itemName, string seq, byte channel, float temp, float vcc, byte pattent,
                            //string dataRate, byte ctrlType, float tempOffset, float tempWaitTimes,bool ignoreFlag, string itemDescription,
                            //byte rowStatus, int opLogPID, string TracingInfo, out int myErr)
                            if (tempDt != null)
                            {
                                foreach (DataRow dr in tempDt.Rows)
                                {
                                    int id; int pid; string itemName; int seq; byte channel; float temp; float vcc; byte pattent;
                                    string dataRate; byte ctrlType; float tempOffset; float tempWaitTimes; bool ignoreFlag; string itemDescription;

                                    byte rowState = 1;
                                    if (dr.RowState == DataRowState.Deleted)
                                    {
                                        id = Convert.ToInt32(dr["ID", DataRowVersion.Original]);
                                        pid = Convert.ToInt32(dr["pid", DataRowVersion.Original]);
                                        itemName = dr["itemName", DataRowVersion.Original].ToString();
                                        seq = Convert.ToInt32(dr["seq", DataRowVersion.Original]);
                                        channel = Convert.ToByte(dr["channel", DataRowVersion.Original]);
                                        temp = Convert.ToSingle(dr["temp", DataRowVersion.Original]);
                                        vcc = Convert.ToSingle(dr["vcc", DataRowVersion.Original]);
                                        pattent = Convert.ToByte(dr["pattent", DataRowVersion.Original]);

                                        dataRate = dr["dataRate", DataRowVersion.Original].ToString();
                                        ctrlType = Convert.ToByte(dr["ctrlType", DataRowVersion.Original]);
                                        tempOffset = Convert.ToSingle(dr["tempOffset", DataRowVersion.Original]);
                                        tempWaitTimes = Convert.ToSingle(dr["tempWaitTimes", DataRowVersion.Original]);
                                        ignoreFlag = Convert.ToBoolean(dr["ignoreFlag", DataRowVersion.Original]);
                                        itemDescription = dr["itemDescription", DataRowVersion.Original].ToString();
                                        rowState = 2;
                                    }
                                    else
                                    {
                                        if (dr.RowState == DataRowState.Added)
                                        {
                                            rowState = 0;
                                        }
                                        else if (dr.RowState == DataRowState.Modified)
                                        {
                                            rowState = 1;
                                        }
                                        id = Convert.ToInt32(dr["ID"]);
                                        pid = Convert.ToInt32(dr["pid"]);
                                        itemName = dr["itemName"].ToString();
                                        seq = Convert.ToInt32(dr["seq"]);
                                        channel = Convert.ToByte(dr["channel"]);
                                        temp = Convert.ToSingle(dr["temp"]);
                                        vcc = Convert.ToSingle(dr["vcc"]);
                                        pattent = Convert.ToByte(dr["pattent"]);

                                        dataRate = dr["dataRate"].ToString();
                                        ctrlType = Convert.ToByte(dr["ctrlType"]);
                                        tempOffset = Convert.ToSingle(dr["tempOffset"]);
                                        tempWaitTimes = Convert.ToSingle(dr["tempWaitTimes"]);
                                        ignoreFlag = Convert.ToBoolean(dr["ignoreFlag"]);
                                        itemDescription = dr["itemDescription"].ToString();
                                    }
                                    result = Pro_TopoTestControl(id, pid, itemName, seq, channel, temp, vcc, pattent,
                                        dataRate, ctrlType, tempOffset, tempWaitTimes, ignoreFlag, itemDescription,
                                        rowState, opLogPID, tracingInfo, out myErr);
                                    if (myErr != 0)
                                    {
                                        break;
                                    }
                                }
                            }
                            #endregion
                        }
                        else if (tableName.Trim().ToUpper() == "TopoPNSpecsParams".ToUpper())
                        {
                            #region  Pro_TopoPNSpecsParams
                            //private int Pro_TopoPNSpecsParams(int id, int pid, int sID, double typical, double specMin, double specMax, byte channel,
                            //byte rowStatus, int opLogPID, string TracingInfo, out int myErr)
                            if (tempDt != null)
                            {
                                foreach (DataRow dr in tempDt.Rows)
                                {
                                    int id; int pid; int sID; double typical; double specMin; double specMax; byte channel = 0;

                                    byte rowState = 1;
                                    if (dr.RowState == DataRowState.Deleted)
                                    {
                                        id = Convert.ToInt32(dr["ID", DataRowVersion.Original]);
                                        pid = Convert.ToInt32(dr["pid", DataRowVersion.Original]);
                                        sID = Convert.ToInt32(dr["sID", DataRowVersion.Original]);
                                        typical = Convert.ToDouble(dr["typical", DataRowVersion.Original]);
                                        specMin = Convert.ToDouble(dr["specMin", DataRowVersion.Original]);
                                        specMax = Convert.ToDouble(dr["specMax", DataRowVersion.Original]);
                                        channel = Convert.ToByte(dr["channel", DataRowVersion.Original]);
                                        rowState = 2;
                                    }
                                    else
                                    {
                                        if (dr.RowState == DataRowState.Added)
                                        {
                                            rowState = 0;
                                        }
                                        else if (dr.RowState == DataRowState.Modified)
                                        {
                                            rowState = 1;
                                        }
                                        id = Convert.ToInt32(dr["ID"]);
                                        pid = Convert.ToInt32(dr["pid"]);
                                        sID = Convert.ToInt32(dr["sID"]);
                                        typical = Convert.ToDouble(dr["typical"]);
                                        specMin = Convert.ToDouble(dr["specMin"]);
                                        specMax = Convert.ToDouble(dr["specMax"]);
                                        channel = Convert.ToByte(dr["channel"]);
                                    }
                                    result = Pro_TopoPNSpecsParams(id, pid, sID, typical, specMin, specMax, channel,
                                        rowState, opLogPID, tracingInfo, out myErr);
                                    if (myErr != 0)
                                    {
                                        break;
                                    }
                                }
                            }
                            #endregion
                        }
                        else if (tableName.Trim().ToUpper() == "TopoManufactureConfigInit".ToUpper())
                        {
                            #region  Pro_TopoManufactureConfigInit
                            //private int Pro_TopoManufactureConfigInit(int id, int pid, int slaveAddress, byte page, int startAddress, byte length, int itemValue,
                            //byte rowStatus, int opLogPID, string TracingInfo, out int myErr)
                            if (tempDt != null)
                            {
                                foreach (DataRow dr in tempDt.Rows)
                                {
                                    int id; int pid; int slaveAddress; byte page; int startAddress; byte length; int itemValue;

                                    byte rowState = 1;
                                    if (dr.RowState == DataRowState.Deleted)
                                    {
                                        id = Convert.ToInt32(dr["ID", DataRowVersion.Original]);
                                        pid = Convert.ToInt32(dr["pid", DataRowVersion.Original]);
                                        slaveAddress = Convert.ToInt32(dr["slaveAddress", DataRowVersion.Original]);
                                        page = Convert.ToByte(dr["page", DataRowVersion.Original]);
                                        startAddress = Convert.ToInt32(dr["startAddress", DataRowVersion.Original]);
                                        length = Convert.ToByte(dr["length", DataRowVersion.Original]);
                                        itemValue = Convert.ToInt32(dr["itemValue", DataRowVersion.Original]);

                                        rowState = 2;
                                    }
                                    else
                                    {
                                        if (dr.RowState == DataRowState.Added)
                                        {
                                            rowState = 0;
                                        }
                                        else if (dr.RowState == DataRowState.Modified)
                                        {
                                            rowState = 1;
                                        }
                                        id = Convert.ToInt32(dr["ID"]);
                                        pid = Convert.ToInt32(dr["pid"]);
                                        slaveAddress = Convert.ToInt32(dr["slaveAddress"]);
                                        page = Convert.ToByte(dr["page"]);
                                        startAddress = Convert.ToInt32(dr["startAddress"]);
                                        length = Convert.ToByte(dr["length"]);
                                        itemValue = Convert.ToInt32(dr["itemValue"]);
                                    }
                                    result = Pro_TopoManufactureConfigInit(id, pid, slaveAddress, page, startAddress, length, itemValue,
                                        rowState, opLogPID, tracingInfo, out myErr);
                                    if (myErr != 0)
                                    {
                                        break;
                                    }
                                }
                            }
                            #endregion
                        }
                        else if (tableName.Trim().ToUpper() == "GlobalSpecs".ToUpper())
                        {
                            #region  Pro_GlobalSpecs
                            //private int Pro_GlobalSpecs(int id, string itemName, string unit, string itemDescription,
                            //byte rowStatus, int opLogPID, string TracingInfo, out int myErr)
                            if (tempDt != null)
                            {
                                foreach (DataRow dr in tempDt.Rows)
                                {
                                    int id; string itemName; string unit; string itemDescription;

                                    byte rowState = 1;
                                    if (dr.RowState == DataRowState.Deleted)
                                    {
                                        id = Convert.ToInt32(dr["ID", DataRowVersion.Original]);
                                        itemName = dr["itemName", DataRowVersion.Original].ToString();
                                        unit = dr["unit", DataRowVersion.Original].ToString();
                                        itemDescription = dr["itemDescription", DataRowVersion.Original].ToString();
                                        rowState = 2;
                                    }
                                    else
                                    {
                                        if (dr.RowState == DataRowState.Added)
                                        {
                                            rowState = 0;
                                        }
                                        else if (dr.RowState == DataRowState.Modified)
                                        {
                                            rowState = 1;
                                        }
                                        id = Convert.ToInt32(dr["ID"]);
                                        itemName = dr["itemName"].ToString();
                                        unit = dr["unit"].ToString();
                                        itemDescription = dr["itemDescription"].ToString();
                                    }
                                    result = Pro_GlobalSpecs(id, itemName, unit, itemDescription,
                                        rowState, opLogPID, tracingInfo, out myErr);
                                    if (myErr != 0)
                                    {
                                        break;
                                    }
                                }
                            }
                            #endregion
                        }
                        else if (tableName.Trim().ToUpper() == "GlobalManufactureCoefficients".ToUpper())
                        {
                            #region  Pro_GlobalManufactureCoefficients
                            //private int Pro_GlobalManufactureCoefficients(int id, int pid, string itemTYPE, string itemName, byte channel, byte page, int startAddress, byte length, string format,
                            //byte rowStatus, int opLogPID, string TracingInfo, out int myErr)
                            if (tempDt != null)
                            {
                                foreach (DataRow dr in tempDt.Rows)
                                {
                                    int id; int pid; string itemTYPE; string itemName;
                                    byte channel; byte page; int startAddress; byte length; string format;

                                    byte rowState = 1;
                                    if (dr.RowState == DataRowState.Deleted)
                                    {
                                        id = Convert.ToInt32(dr["ID", DataRowVersion.Original]);
                                        pid = Convert.ToInt32(dr["pid", DataRowVersion.Original]);
                                        itemTYPE = dr["itemTYPE", DataRowVersion.Original].ToString();
                                        itemName = dr["itemName", DataRowVersion.Original].ToString();
                                        channel = Convert.ToByte(dr["channel", DataRowVersion.Original]);
                                        page = Convert.ToByte(dr["page", DataRowVersion.Original]);
                                        startAddress = Convert.ToInt32(dr["startAddress", DataRowVersion.Original]);
                                        length = Convert.ToByte(dr["length", DataRowVersion.Original]);
                                        format = dr["format", DataRowVersion.Original].ToString();

                                        rowState = 2;
                                    }
                                    else
                                    {
                                        if (dr.RowState == DataRowState.Added)
                                        {
                                            rowState = 0;
                                        }
                                        else if (dr.RowState == DataRowState.Modified)
                                        {
                                            rowState = 1;
                                        }
                                        id = Convert.ToInt32(dr["ID"]);
                                        pid = Convert.ToInt32(dr["pid"]);
                                        itemTYPE = dr["itemTYPE"].ToString();
                                        itemName = dr["itemName"].ToString();
                                        channel = Convert.ToByte(dr["channel"]);
                                        page = Convert.ToByte(dr["page"]);
                                        startAddress = Convert.ToInt32(dr["startAddress"]);
                                        length = Convert.ToByte(dr["length"]);
                                        format = dr["format"].ToString();
                                    }
                                    result = Pro_GlobalManufactureCoefficients(id, pid, itemTYPE, itemName, channel, page, startAddress, length, format,
                                        rowState, opLogPID, tracingInfo, out myErr);
                                    if (myErr != 0)
                                    {
                                        break;
                                    }
                                }
                            }
                            #endregion
                        }
                        else if (tableName.Trim().ToUpper() == "GlobalManufactureCoefficientsGroup".ToUpper())
                        {
                            #region  Pro_GlobalManufactureCoefficientsGroup
                            //private int Pro_GlobalManufactureCoefficientsGroup(int id, string itemName, int typeID, string itemDescription,bool ignoreFlag,
                            //byte rowStatus, int opLogPID, string TracingInfo, out int myErr)
                            if (tempDt != null)
                            {
                                foreach (DataRow dr in tempDt.Rows)
                                {
                                    int id; string itemName; int typeID; string itemDescription; bool ignoreFlag;

                                    byte rowState = 1;
                                    if (dr.RowState == DataRowState.Deleted)
                                    {
                                        id = Convert.ToInt32(dr["ID", DataRowVersion.Original]);
                                        itemName = dr["itemName", DataRowVersion.Original].ToString();
                                        typeID = Convert.ToInt32(dr["typeID", DataRowVersion.Original]);
                                        itemDescription = dr["itemDescription", DataRowVersion.Original].ToString();
                                        ignoreFlag = Convert.ToBoolean(dr["ignoreFlag", DataRowVersion.Original]);
                                        rowState = 2;
                                    }
                                    else
                                    {
                                        if (dr.RowState == DataRowState.Added)
                                        {
                                            rowState = 0;
                                        }
                                        else if (dr.RowState == DataRowState.Modified)
                                        {
                                            rowState = 1;
                                        }
                                        id = Convert.ToInt32(dr["ID"]);
                                        itemName = dr["itemName"].ToString();
                                        typeID = Convert.ToInt32(dr["typeID"]);
                                        itemDescription = dr["itemDescription"].ToString();
                                        ignoreFlag = Convert.ToBoolean(dr["ignoreFlag"]);
                                    }
                                    result = Pro_GlobalManufactureCoefficientsGroup(id, itemName, typeID, itemDescription, ignoreFlag,
                                        rowState, opLogPID, tracingInfo, out myErr);
                                    if (myErr != 0)
                                    {
                                        break;
                                    }
                                }
                            }
                            #endregion
                        }
                        else if (tableName.Trim().ToUpper() == "UserPNAction".ToUpper())
                        {
                            #region  Pro_UserPNAction
                            //private int Pro_UserPNAction(int id, int userID, int pnID, bool addPlan, bool modifyPN,
                            //byte rowStatus, int opLogPID, string TracingInfo, out int myErr)
                            if (tempDt != null)
                            {
                                foreach (DataRow dr in tempDt.Rows)
                                {
                                    int id; int userID; int pnID; bool addPlan; bool modifyPN;

                                    byte rowState = 1;
                                    if (dr.RowState == DataRowState.Deleted)
                                    {
                                        id = Convert.ToInt32(dr["ID", DataRowVersion.Original]);
                                        userID = Convert.ToInt32(dr["UserID", DataRowVersion.Original]);
                                        pnID = Convert.ToInt32(dr["PNID", DataRowVersion.Original]);
                                        addPlan = Convert.ToBoolean(dr["AddPlan", DataRowVersion.Original]);
                                        modifyPN = Convert.ToBoolean(dr["ModifyPN", DataRowVersion.Original]);
                                        rowState = 2;
                                    }
                                    else
                                    {
                                        if (dr.RowState == DataRowState.Added)
                                        {
                                            rowState = 0;
                                        }
                                        else if (dr.RowState == DataRowState.Modified)
                                        {
                                            rowState = 1;
                                        }
                                        id = Convert.ToInt32(dr["ID"]);
                                        userID = Convert.ToInt32(dr["UserID"]);
                                        pnID = Convert.ToInt32(dr["PNID"]);
                                        addPlan = Convert.ToBoolean(dr["AddPlan"]);
                                        modifyPN = Convert.ToBoolean(dr["ModifyPN"]);
                                    }
                                    result = Pro_UserPNAction(id, userID, pnID, addPlan, modifyPN,
                                        rowState, opLogPID, tracingInfo, out myErr);
                                    if (myErr != 0)
                                    {
                                        break;
                                    }
                                }
                            }
                            #endregion
                        }
                        else if (tableName.Trim().ToUpper() == "UserPlanAction".ToUpper())
                        {
                            #region  Pro_UserPlanAction
                            //private int Pro_UserPlanAction(int id, int userID, int planID, bool modifyPlan, bool deletePlan, bool runPlan,
                            //byte rowStatus, int opLogPID, string TracingInfo, out int myErr)
                            if (tempDt != null)
                            {
                                foreach (DataRow dr in tempDt.Rows)
                                {
                                    int id; int userID; int planID; bool modifyPlan; bool deletePlan; bool runPlan;

                                    byte rowState = 1;
                                    if (dr.RowState == DataRowState.Deleted)
                                    {
                                        id = Convert.ToInt32(dr["ID", DataRowVersion.Original]);
                                        userID = Convert.ToInt32(dr["UserID", DataRowVersion.Original]);
                                        planID = Convert.ToInt32(dr["PlanID", DataRowVersion.Original]);
                                        modifyPlan = Convert.ToBoolean(dr["ModifyPlan", DataRowVersion.Original]);
                                        deletePlan = Convert.ToBoolean(dr["DeletePlan", DataRowVersion.Original]);
                                        runPlan = Convert.ToBoolean(dr["RunPlan", DataRowVersion.Original]);
                                        rowState = 2;
                                    }
                                    else
                                    {
                                        if (dr.RowState == DataRowState.Added)
                                        {
                                            rowState = 0;
                                        }
                                        else if (dr.RowState == DataRowState.Modified)
                                        {
                                            rowState = 1;
                                        }
                                        id = Convert.ToInt32(dr["ID"]);
                                        userID = Convert.ToInt32(dr["UserID"]);
                                        planID = Convert.ToInt32(dr["PlanID"]);
                                        modifyPlan = Convert.ToBoolean(dr["ModifyPlan"]);
                                        deletePlan = Convert.ToBoolean(dr["DeletePlan"]);
                                        runPlan = Convert.ToBoolean(dr["RunPlan"]);
                                    }
                                    result = Pro_UserPlanAction(id, userID, planID, modifyPlan, deletePlan,runPlan,
                                        rowState, opLogPID, tracingInfo, out myErr);
                                    if (myErr != 0)
                                    {
                                        break;
                                    }
                                }
                            }
                            #endregion
                        }
                        else if (tableName.Trim().ToUpper() == "ChannelMap".ToUpper())
                        {
                            #region  Pro_ChannelMap
                            //private int Pro_ChannelMap(int id, int pnChipID, int moduleLine, int chipLine, int debugLine,
                            //byte rowStatus, int opLogPID, string TracingInfo, out int myErr)
                            if (tempDt != null)
                            {
                                foreach (DataRow dr in tempDt.Rows)
                                {
                                    int id; int pnChipID; int moduleLine; int chipLine; int debugLine;

                                    byte rowState = 1;
                                    if (dr.RowState == DataRowState.Deleted)
                                    {
                                        id = Convert.ToInt32(dr["ID", DataRowVersion.Original]);
                                        pnChipID = Convert.ToInt32(dr["pnChipID", DataRowVersion.Original]);
                                        moduleLine = Convert.ToInt32(dr["moduleLine", DataRowVersion.Original]);
                                        chipLine = Convert.ToInt32(dr["chipLine", DataRowVersion.Original]);
                                        debugLine = Convert.ToInt32(dr["debugLine", DataRowVersion.Original]);
                                        rowState = 2;
                                    }
                                    else
                                    {
                                        if (dr.RowState == DataRowState.Added)
                                        {
                                            rowState = 0;
                                        }
                                        else if (dr.RowState == DataRowState.Modified)
                                        {
                                            rowState = 1;
                                        }
                                        id = Convert.ToInt32(dr["ID"]);
                                        pnChipID = Convert.ToInt32(dr["pnChipID"]);
                                        moduleLine = Convert.ToInt32(dr["moduleLine"]);
                                        chipLine = Convert.ToInt32(dr["chipLine"]);
                                        debugLine = Convert.ToInt32(dr["debugLine"]);
                                    }
                                    result = Pro_ChannelMap(id, pnChipID, moduleLine, chipLine, debugLine,
                                        rowState, opLogPID, tracingInfo, out myErr);
                                    if (myErr != 0)
                                    {
                                        break;
                                    }
                                }
                            }
                            #endregion
                        }
                        else if (tableName.Trim().ToUpper() == "ChipBaseInfo".ToUpper())
                        {
                            #region  Pro_ChipBaseInfo
                            //private int Pro_ChipBaseInfo(int id, string itemName, byte channels, string description, byte width,bool littleEndian,
                            //byte rowStatus, int opLogPID, string TracingInfo, out int myErr)
                            if (tempDt != null)
                            {
                                foreach (DataRow dr in tempDt.Rows)
                                {
                                    int id; string itemName; byte channels; string description; byte width;bool littleEndian;

                                    byte rowState = 1;
                                    if (dr.RowState == DataRowState.Deleted)
                                    {
                                        id = Convert.ToInt32(dr["ID", DataRowVersion.Original]);
                                        itemName = Convert.ToString(dr["itemName", DataRowVersion.Original]);
                                        channels = Convert.ToByte(dr["channels", DataRowVersion.Original]);
                                        description = Convert.ToString(dr["description", DataRowVersion.Original]);
                                        width = Convert.ToByte(dr["width", DataRowVersion.Original]);
                                        littleEndian = Convert.ToBoolean(dr["littleEndian", DataRowVersion.Original]);
                                        rowState = 2;
                                    }
                                    else
                                    {
                                        if (dr.RowState == DataRowState.Added)
                                        {
                                            rowState = 0;
                                        }
                                        else if (dr.RowState == DataRowState.Modified)
                                        {
                                            rowState = 1;
                                        }
                                        id = Convert.ToInt32(dr["ID"]);
                                        itemName = Convert.ToString(dr["itemName"]);
                                        channels = Convert.ToByte(dr["channels"]);
                                        description = Convert.ToString(dr["description"]);
                                        width = Convert.ToByte(dr["width"]);
                                        littleEndian = Convert.ToBoolean(dr["littleEndian"]);
                                    }
                                    result = Pro_ChipBaseInfo(id, itemName, channels, description, width, littleEndian,
                                        rowState, opLogPID, tracingInfo, out myErr);
                                    if (myErr != 0)
                                    {
                                        break;
                                    }
                                }
                            }
                            #endregion
                        }
                        else if (tableName.Trim().ToUpper() == "ChipRegisterList".ToUpper())
                        {
                            #region  Pro_ChipRegisterList
                            //private int Pro_ChipRegisterList( int id, int chipID, string itemName, string writeFormula, string analogueUnit, string readFormula, int endBit, int address, int startBit, int unitLength,int chipLine,
                            //byte rowStatus, int opLogPID, string TracingInfo, out int myErr)
                            if (tempDt != null)
                            {
                                foreach (DataRow dr in tempDt.Rows)
                                {
                                    int id; int chipID; string itemName; string writeFormula; string analogueUnit; string readFormula; int endBit; int address; int startBit; int unitLength; int chipLine;

                                    byte rowState = 1;
                                    if (dr.RowState == DataRowState.Deleted)
                                    {
                                        id = Convert.ToInt32(dr["ID", DataRowVersion.Original]);
                                        chipID = Convert.ToInt32(dr["chipID", DataRowVersion.Original]);
                                        itemName = Convert.ToString(dr["itemName", DataRowVersion.Original]);
                                        writeFormula = Convert.ToString(dr["writeFormula", DataRowVersion.Original]);
                                        analogueUnit = Convert.ToString(dr["analogueUnit", DataRowVersion.Original]);
                                        readFormula = Convert.ToString(dr["readFormula", DataRowVersion.Original]);
                                        endBit = Convert.ToInt32(dr["endBit", DataRowVersion.Original]);
                                        address = Convert.ToInt32(dr["address", DataRowVersion.Original]);
                                        startBit = Convert.ToInt32(dr["startBit", DataRowVersion.Original]);
                                        unitLength = Convert.ToInt32(dr["unitLength", DataRowVersion.Original]);
                                        chipLine = Convert.ToInt32(dr["chipLine", DataRowVersion.Original]);
                                        rowState = 2;
                                    }
                                    else
                                    {
                                        if (dr.RowState == DataRowState.Added)
                                        {
                                            rowState = 0;
                                        }
                                        else if (dr.RowState == DataRowState.Modified)
                                        {
                                            rowState = 1;
                                        }
                                        id = Convert.ToInt32(dr["ID"]);
                                        chipID = Convert.ToInt32(dr["chipID"]);
                                        itemName = Convert.ToString(dr["itemName"]);
                                        writeFormula = Convert.ToString(dr["writeFormula"]);
                                        analogueUnit = Convert.ToString(dr["analogueUnit"]);
                                        readFormula = Convert.ToString(dr["readFormula"]);
                                        endBit = Convert.ToInt32(dr["endBit"]);
                                        address = Convert.ToInt32(dr["address"]);
                                        startBit = Convert.ToInt32(dr["startBit"]);
                                        unitLength = Convert.ToInt32(dr["unitLength"]);
                                        chipLine = Convert.ToInt32(dr["chipLine"]);
                                    }
                                    result = Pro_ChipRegisterList(id, chipID, itemName, writeFormula, analogueUnit, readFormula, endBit, address, startBit, unitLength, chipLine,
                                        rowState, opLogPID, tracingInfo, out myErr);
                                    if (myErr != 0)
                                    {
                                        break;
                                    }
                                }
                            }
                            #endregion
                        }
                        else if (tableName.Trim().ToUpper() == "PNChipMap".ToUpper())
                        {
                            #region  Pro_PNChipMap
                            //private int Pro_PNChipMap(int id, int pnID, int chipID, int chipRoleID, byte chipDirection,
                            //byte rowStatus, int opLogPID, string TracingInfo, out int myErr)
                            if (tempDt != null)
                            {
                                foreach (DataRow dr in tempDt.Rows)
                                {
                                    int id; int pnID; int chipID; int chipRoleID; byte chipDirection;

                                    byte rowState = 1;
                                    if (dr.RowState == DataRowState.Deleted)
                                    {
                                        id = Convert.ToInt32(dr["ID", DataRowVersion.Original]);
                                        pnID = Convert.ToInt32(dr["pnID", DataRowVersion.Original]);
                                        chipID = Convert.ToInt32(dr["chipID", DataRowVersion.Original]);
                                        chipRoleID = Convert.ToInt32(dr["chipRoleID", DataRowVersion.Original]);
                                        chipDirection = Convert.ToByte(dr["chipDirection", DataRowVersion.Original]);                                        
                                        rowState = 2;
                                    }
                                    else
                                    {
                                        if (dr.RowState == DataRowState.Added)
                                        {
                                            rowState = 0;
                                        }
                                        else if (dr.RowState == DataRowState.Modified)
                                        {
                                            rowState = 1;
                                        }
                                        id = Convert.ToInt32(dr["ID"]);
                                        pnID = Convert.ToInt32(dr["pnID"]);
                                        chipID = Convert.ToInt32(dr["chipID"]);
                                        chipRoleID = Convert.ToInt32(dr["chipRoleID"]);
                                        chipDirection = Convert.ToByte(dr["chipDirection"]);
                                    }
                                    result = Pro_PNChipMap(id, pnID, chipID, chipRoleID, chipDirection,
                                        rowState, opLogPID, tracingInfo, out myErr);
                                    if (myErr != 0)
                                    {
                                        break;
                                    }
                                }
                            }
                            #endregion
                        }
                        #region 151112 架构更新 取消RegisterFormula
                        //else if (tableName.Trim().ToUpper() == "RegisterFormula".ToUpper())
                        //{
                        //    #region  Pro_RegisterFormula
                        //    //private int Pro_RegisterFormula(int id, int chipID, string itemName, string writeFormula, string analogueUnit,string readFormula,
                        //    //byte rowStatus, int opLogPID, string TracingInfo, out int myErr)
                        //    if (tempDt != null)
                        //    {
                        //        foreach (DataRow dr in tempDt.Rows)
                        //        {
                        //            int id; int chipID; string itemName; string writeFormula; string analogueUnit;string readFormula;

                        //            byte rowState = 1;
                        //            if (dr.RowState == DataRowState.Deleted)
                        //            {
                        //                id = Convert.ToInt32(dr["ID", DataRowVersion.Original]);
                        //                chipID = Convert.ToInt32(dr["chipID", DataRowVersion.Original]);
                        //                itemName = Convert.ToString(dr["itemName", DataRowVersion.Original]);
                        //                writeFormula = Convert.ToString(dr["writeFormula", DataRowVersion.Original]);
                        //                analogueUnit = Convert.ToString(dr["analogueUnit", DataRowVersion.Original]);
                        //                readFormula = Convert.ToString(dr["readFormula", DataRowVersion.Original]);
                        //                rowState = 2;
                        //            }
                        //            else
                        //            {
                        //                if (dr.RowState == DataRowState.Added)
                        //                {
                        //                    rowState = 0;
                        //                }
                        //                else if (dr.RowState == DataRowState.Modified)
                        //                {
                        //                    rowState = 1;
                        //                }
                        //                id = Convert.ToInt32(dr["ID"]);
                        //                chipID = Convert.ToInt32(dr["chipID"]);
                        //                itemName = Convert.ToString(dr["itemName"]);
                        //                writeFormula = Convert.ToString(dr["writeFormula"]);
                        //                analogueUnit = Convert.ToString(dr["analogueUnit"]);
                        //                readFormula = Convert.ToString(dr["readFormula"]);
                        //            }
                        //            result = Pro_RegisterFormula(id, chipID, itemName, writeFormula, analogueUnit,readFormula,
                        //                rowState, opLogPID, tracingInfo, out myErr);
                        //            if (myErr != 0)
                        //            {
                        //                break;
                        //            }
                        //        }
                        //    }
                        //    #endregion
                        //}
                        #endregion
                        //更多的其他表资料
                        else
                        {
                            long myID = 0;
                            UpdateDataTable(queryCMD, dt, out myID);
                            if (myID < Int32.MaxValue)
                            {
                                result = Convert.ToInt32(myID);
                            }
                        }
                    }
                    #endregion
                    #region For Other DataBase TBD
                    else
                    {
                        return 0;
                    }
                    #endregion

                    if (myErr == 0)
                    {
                        return result;  //即ID号
                    }
                    else
                    {
                        return myErr;   //即Error
                    }
                }
                else
                {
                    AlertMsgShow("Can not find any datatable changes Info!");
                    return -101;
                    //throw new Exception("Can not find any datatable changes Info!");
                }
            }
            catch (Exception ex)
            {
                WriteErrorLogs(ex.ToString());
                AlertMsgShow(ex.Message);
                return -1;
                
            }
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
        public override int UpdateWithProc(string tableName, DataTable dt, DataTable paramsDt,
            string tracingInfo, string databaseName = "ATS_V2")
        {
            int result = -1;
            int myErr = 0;
            int opLogPID = -1;
            GetXLMInfor();
            databaseName = dbName;
            try
            {
                #region get Session["UserLoginID"]
                if (HttpContext.Current.Session["UserLoginID"] != null)
                {
                    opLogPID = Convert.ToInt32(HttpContext.Current.Session["UserLoginID"].ToString());
                }
                else
                {
                    AlertMsgShow("Can not find any UserLoginID Info!");
                    return -100;
                    //throw new Exception("Can not find any UserLoginID Info!");
                }
                #endregion

                if (dt.GetChanges() != null || paramsDt.GetChanges() != null)
                {
                    DataTable tempDt = dt.GetChanges();
                    DataTable tempParamsDt = paramsDt.GetChanges();

                    #region "ATS_V2"
                    if (databaseName.ToUpper().Trim() == "ATS_V2" || databaseName.ToUpper().Trim() == dbName)
                    {
                        //修改了Model信息
                        if (dt.GetChanges() != null)
                        {
                            if (tableName.Trim().ToUpper() == "TopoTestModel".ToUpper())
                            {
                                #region Pro_TopoTestModelWithParams

                                if (tempDt != null)
                                {
                                    foreach (DataRow dr in tempDt.Rows)
                                    {
                                        int ID; int PID; int GID; int Seq; string paramsStr = ""; bool Failbreak; bool IgnoreFlag;

                                        byte rowState = 1;
                                        if (dr.RowState == DataRowState.Deleted)
                                        {
                                            ID = Convert.ToInt32(dr["ID", DataRowVersion.Original]);
                                            PID = Convert.ToInt32(dr["PID", DataRowVersion.Original]);
                                            GID = Convert.ToInt32(dr["GID", DataRowVersion.Original]);
                                            Seq = Convert.ToInt32(dr["Seq", DataRowVersion.Original]);
                                            IgnoreFlag = Convert.ToBoolean(dr["IgnoreFlag", DataRowVersion.Original]);
                                            Failbreak = Convert.ToBoolean(dr["Failbreak", DataRowVersion.Original]);
                                            rowState = 2;
                                        }
                                        else
                                        {
                                            ID = Convert.ToInt32(dr["ID"]);
                                            PID = Convert.ToInt32(dr["PID"]);
                                            GID = Convert.ToInt32(dr["GID"]);
                                            Seq = Convert.ToInt32(dr["Seq"]);
                                            IgnoreFlag = Convert.ToBoolean(dr["IgnoreFlag"]);
                                            Failbreak = Convert.ToBoolean(dr["Failbreak"]);
                                            if (paramsDt.GetChanges() != null)
                                            {
                                                foreach (DataRow paramDr in paramsDt.Select("PID=" + ID))
                                                {
                                                    paramsStr += paramDr["GID"].ToString() + "#" + paramDr["ItemValue"].ToString() + "|";
                                                }
                                            }
                                            if (dr.RowState == DataRowState.Added)
                                            {
                                                rowState = 0;
                                            }
                                            else if (dr.RowState == DataRowState.Modified)
                                            {
                                                rowState = 1;
                                            }
                                        }
                                        result = Pro_TopoTestModelWithParams(ID, PID, GID, Seq, IgnoreFlag, Failbreak, paramsStr, rowState,
                                                opLogPID, tracingInfo, out myErr);
                                        if (myErr != 0)
                                        {
                                            break;
                                        }
                                    }
                                }
                                else if (paramsDt.GetChanges() != null)
                                {
                                    #region 未修改Model信息,而只修改某个参数信息
                                    if (paramsDt.DefaultView.ToTable(true, "PID").Rows.Count != 1)
                                    {
                                        throw new Exception("paramsDt的PID不为唯一,取消更新资料!");
                                    }
                                    else
                                    {
                                        int ID = Convert.ToInt32(paramsDt.Rows[0]["PID"]);   //因为只有一条ModelID的资料故未详细处理其他状况
                                        DataRow dr = dt.Select("ID=" + ID)[0];
                                        int PID = Convert.ToInt32(dr["PID"]);
                                        int GID = Convert.ToInt32(dr["GID"]);
                                        int Seq = Convert.ToInt32(dr["Seq"]);
                                        bool IgnoreFlag = Convert.ToBoolean(dr["IgnoreFlag"]);
                                        bool Failbreak = Convert.ToBoolean(dr["Failbreak"]);
                                        string paramsStr = "";
                                        if (paramsDt.GetChanges() != null)
                                        {
                                            foreach (DataRow paramDr in paramsDt.Select("PID=" + ID))
                                            {
                                                paramsStr += paramDr["GID"].ToString() + "#" + paramDr["ItemValue"].ToString() + "|";
                                            }
                                        }
                                        result = Pro_TopoTestModelWithParams(ID, PID, GID, Seq, IgnoreFlag, Failbreak, paramsStr, 1,
                                                opLogPID, tracingInfo, out myErr);
                                    }
                                    #endregion
                                }
                                #endregion
                            }
                            if (tableName.Trim().ToUpper() == "TopoEquipment".ToUpper())
                            {
                                #region Pro_TopoEquipmentWithParams

                                if (tempDt != null)
                                {
                                    foreach (DataRow dr in tempDt.Rows)
                                    {
                                        int ID; int PID; int GID; int Seq; byte Role; string paramsStr = "";

                                        byte rowState = 1;
                                        if (dr.RowState == DataRowState.Deleted)
                                        {
                                            ID = Convert.ToInt32(dr["ID", DataRowVersion.Original]);
                                            PID = Convert.ToInt32(dr["PID", DataRowVersion.Original]);
                                            GID = Convert.ToInt32(dr["GID", DataRowVersion.Original]);
                                            Seq = Convert.ToInt32(dr["Seq", DataRowVersion.Original]);
                                            Role = Convert.ToByte(dr["Role", DataRowVersion.Original]);
                                            rowState = 2;
                                        }
                                        else
                                        {
                                            ID = Convert.ToInt32(dr["ID"]);
                                            PID = Convert.ToInt32(dr["PID"]);
                                            GID = Convert.ToInt32(dr["GID"]);
                                            Seq = Convert.ToInt32(dr["Seq"]);
                                            Role = Convert.ToByte(dr["Role"]);
                                            if (paramsDt.GetChanges() != null)
                                            {
                                                foreach (DataRow paramDr in paramsDt.Select("PID=" + ID))
                                                {
                                                    paramsStr += paramDr["GID"].ToString() + "#" + paramDr["ItemValue"].ToString() + "|";
                                                }
                                            }
                                            if (dr.RowState == DataRowState.Added)
                                            {
                                                rowState = 0;
                                            }
                                            else if (dr.RowState == DataRowState.Modified)
                                            {
                                                rowState = 1;
                                            }
                                        }
                                        result = Pro_TopoEquipmentWithParams(ID, PID, GID, Seq, Role, paramsStr, rowState,
                                                opLogPID, tracingInfo, out myErr);
                                        if (myErr != 0)
                                        {
                                            break;
                                        }
                                    }
                                }
                                else if (paramsDt.GetChanges() != null)
                                {
                                    #region 未修改Eq信息,而只修改某个参数信息
                                    if (paramsDt.DefaultView.ToTable(true, "PID").Rows.Count != 1)
                                    {
                                        throw new Exception("paramsDt的PID不为唯一,取消更新资料!");
                                    }
                                    else
                                    {
                                        int ID = Convert.ToInt32(paramsDt.Rows[0]["PID"]);   //因为只有一条ModelID的资料故未详细处理其他状况
                                        DataRow dr = dt.Select("ID=" + ID)[0];
                                        int PID = Convert.ToInt32(dr["PID"]);
                                        int GID = Convert.ToInt32(dr["GID"]);
                                        int Seq = Convert.ToInt32(dr["Seq"]);
                                        byte Role = Convert.ToByte(dr["Role"]);
                                        string paramsStr = "";
                                        if (paramsDt.GetChanges() != null)
                                        {
                                            foreach (DataRow paramDr in paramsDt.Select("PID=" + ID))
                                            {
                                                paramsStr += paramDr["GID"].ToString() + "#" + paramDr["ItemValue"].ToString() + "|";
                                            }
                                        }
                                        result = Pro_TopoEquipmentWithParams(ID, PID, GID, Seq, Role, paramsStr, 1,
                                                opLogPID, tracingInfo, out myErr);
                                    }
                                    #endregion
                                }
                                #endregion
                            }
                        }
                    }
                    #endregion
                    #region For Other DataBase TBD
                    else
                    {
                        return 0;
                    }
                    #endregion

                    if (myErr == 0)
                    {
                        return result;  //即ID号
                    }
                    else
                    {
                        return myErr;   //即Error
                    }
                }
                else
                {
                    AlertMsgShow("Can not find any datatable changes Info!");
                    return -101;
                    //throw new Exception("Can not find any datatable changes Info!");
                }
            }
            catch (Exception ex)
            {
                WriteErrorLogs(ex.ToString());
                AlertMsgShow(ex.Message);
                return -1;
                
            }
        }

        string DTGetChanges(DataTable dt)
        {
            string SS = ""; //"==User :" + HttpContext.Current.Session["UserName"] + " Operation Logs at " + DateTime.Now.ToString() + " ==\r\n";
            try
            {
                if (dt.GetChanges() != null)
                {
                    //SS += "**[" + dt.TableName + "]**\r\n";
                    DataTable myDeletedDt = dt.GetChanges(DataRowState.Deleted);
                    DataTable myAddDt = dt.GetChanges(DataRowState.Added);
                    DataTable myChangeDt = dt.GetChanges(DataRowState.Modified);

                    #region 每行的资料
                    if (myChangeDt != null)
                    {
                        for (int j = 0; j < myChangeDt.Rows.Count; j++)
                        {
                            //DataRow dataRow = GlobalDS.Tables[i].Rows[j];
                            DataRow dataRow = myChangeDt.Rows[j];
                            string ss1 = "";
                            string ss2 = "";

                            for (int k = 0; k < dataRow.ItemArray.Length; k++)
                            {
                                if (dataRow[k, DataRowVersion.Current].ToString() != dataRow[k, DataRowVersion.Original].ToString())
                                {
                                    if (ss1.Length <= 0) ss1 += "OriginalData:" + "ID=" + dataRow["ID"].ToString() + ",";
                                    if (ss2.Length <= 0) ss2 += "New     Data:" + "ID=" + dataRow["ID"].ToString() + ",";
                                    ss1 += "[" + dt.Columns[k].ColumnName + "]:" + dataRow[k, DataRowVersion.Original].ToString() + ";";
                                    ss2 += "[" + dt.Columns[k].ColumnName + "]:" + dataRow[k, DataRowVersion.Current].ToString() + ";";
                                }
                            }
                            if (!(string.IsNullOrWhiteSpace(ss1) || string.IsNullOrWhiteSpace(ss2)))
                            {
                                SS += ss1 + "\r\n";
                                SS += ss2 + "\r\n";
                            }
                        }
                    }
                    if (myDeletedDt != null)
                    {
                        for (int j = 0; j < myDeletedDt.Rows.Count; j++)
                        {
                            string ss1 = "Data Deleted:ID=";
                            DataRow dataRow = myDeletedDt.Rows[j];
                            ;
                            for (int k = 0; k < myDeletedDt.Columns.Count; k++)
                            {
                                ss1 += dataRow[k, DataRowVersion.Original].ToString() + ";";
                            }

                            SS += ss1 + "\r\n";
                        }
                    }
                    if (myAddDt != null)
                    {
                        for (int j = 0; j < myAddDt.Rows.Count; j++)
                        {
                            string ss2 = "Data Added:ID=";
                            DataRow dataRow = myAddDt.Rows[j];
                            for (int k = 0; k < dataRow.ItemArray.Length; k++)
                            {
                                ss2 += dataRow[k, DataRowVersion.Current].ToString() + ";";
                            }
                            SS += ss2 + "\r\n";
                        }
                    }
                    #endregion
                }
                if (!string.IsNullOrWhiteSpace(SS))
                {
                    SS = "==User :" + HttpContext.Current.Session["UserName"] + " Operation Logs at " + DateTime.Now.ToString() + " ==\r\n" + "**[" + dt.TableName + "]**\r\n" + SS;
                }
                return SS;
            }
            catch (Exception ex)
            {
                return SS + "\r\n[Error=" + ex.ToString() + "]";
            }
        }

        #region Pro_CopyTestPlan
        //存储过程 Pro_CopyTestPlan			
        //复制测试计划的资料			
        //参数	类型	方向	描述
        //@sourcePlanID int,
        //@NewPlanName	nvarchar(30),
        //@NewPlanPID	int,
        //@OPlogPID	int,
        //@TracingInfo nvarchar(max),
        //@myErr	nvarchar(max)	OUTPUT,
        //@myErrMsg	nvarchar(max)	OUTPUT
        private int Pro_CopyTestPlan(int sourcePlanID, string newPlanName, int newPlanPID,
           int opLogPID, string TracingInfo, out string errMsg)
        {
            int myErr = -1;
            errMsg = "";
            SqlCommand command = new SqlCommand("Pro_CopyTestPlan", conn);
            try
            {
                OpenDatabase(true);
                //执行存储过程 

                //说明命令要执行的是存储过程     
                command.CommandType = CommandType.StoredProcedure;
                //向存储过程中传递参数   

                command.Parameters.Add(new SqlParameter("@SourcePlanID", SqlDbType.Int, 50));
                command.Parameters.Add(new SqlParameter("@NewPlanName", SqlDbType.NVarChar, 50));
                command.Parameters.Add(new SqlParameter("@NewPlanPID", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@OPlogPID", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@TracingInfo", SqlDbType.NVarChar, 4000));
                command.Parameters.Add(new SqlParameter("@myErr", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@myErrMsg", SqlDbType.NVarChar, 4000));
                command.UpdatedRowSource = UpdateRowSource.None;
                command.Parameters["@SourcePlanID"].Value = sourcePlanID;
                command.Parameters["@NewPlanPID"].Value = newPlanPID;
                command.Parameters["@NewPlanName"].Value = newPlanName;

                command.Parameters["@OPlogPID"].Value = opLogPID;
                command.Parameters["@TracingInfo"].Value = TracingInfo;
                command.Parameters["@myErrMsg"].Direction = ParameterDirection.Output;
                command.Parameters["@myErr"].Direction = ParameterDirection.Output;

                command.ExecuteNonQuery();
                errMsg = command.Parameters["@myErrMsg"].Value.ToString();
                myErr = Convert.ToInt32(command.Parameters["@myErr"].Value);
                //OpenDatabase(false);

                return myErr;
            }
            catch (Exception ex)
            {
                if (command.Parameters["@myErrMsg"].Value != null)
                {
                    errMsg = command.Parameters["@myErrMsg"].Value.ToString();
                    myErr = Convert.ToInt32(command.Parameters["@myErr"].Value);
                    WriteErrorLogs("Err:" + myErr + ";" + errMsg);
                    //HttpContext.Current.Response.Write("<Script>alert('Error:" + myErr + ";" + errMsg.Replace("'", @"""") + "')</Script>");                   
                }
                else
                {
                    WriteErrorLogs(ex.ToString());
                    errMsg = ex.ToString();
                    //HttpContext.Current.Response.Write("<Script>alert('Error:" + ex.ToString().Replace("'", @"""").Replace("\r\n","\\n") + "')</Script>"); 
                }
                return myErr;
            }
            finally
            {
                OpenDatabase(false);
            }
        }
        #endregion

        public override int CopyTestPlan(int sourcePlanID, string newPlanName, int newPlanPID, string tracingInfo, out string errMsg)
        {
            int result = -1;
            int opLogPID = -1;
            errMsg = "";
            try
            {
                #region get Session["UserLoginID"]
                if (HttpContext.Current.Session["UserLoginID"] != null)
                {
                    opLogPID = Convert.ToInt32(HttpContext.Current.Session["UserLoginID"].ToString());
                }
                else
                {
                    errMsg = "Can not find any UserLoginID Info!";
                    //HttpContext.Current.Response.Write("<Script>alert('Error:" + errMsg.Replace("'", @"""") + "')</Script>");
                    return -100;
                }
                #endregion

                #region  Pro_CopyTestPlan
                result = Pro_CopyTestPlan(sourcePlanID, newPlanName, newPlanPID, opLogPID, tracingInfo, out errMsg);
                #endregion

                return result;   //即Error               
            }
            catch (Exception ex)
            {
                WriteErrorLogs(ex.ToString());
                return -1;
                
            }
        }
        
        #region Pro_CopyFlowCtrl
        //存储过程 Pro_CopyFlowCtrl			
        //复制测试流程的资料			
        //参数	类型	方向	描述
        //@sourceCtrlID int,
        //@NewCtrlName	nvarchar(30),
        //@NewCtrlPID	int,
        //@OPlogPID	int,
        //@TracingInfo nvarchar(max),
        //@myErr	nvarchar(max)	OUTPUT,
        //@myErrMsg	nvarchar(max)	OUTPUT
        private int Pro_CopyFlowCtrl(int sourceCtrlID, string newCtrlName, int newCtrlPID,
           int opLogPID, string TracingInfo, out string errMsg)
        {
            int myErr = -1;
            errMsg = "";
            SqlCommand command = new SqlCommand("Pro_CopyFlowCtrl", conn);
            try
            {
                OpenDatabase(true);
                //执行存储过程 

                //说明命令要执行的是存储过程     
                command.CommandType = CommandType.StoredProcedure;
                //向存储过程中传递参数   

                command.Parameters.Add(new SqlParameter("@SourceCtrlID", SqlDbType.Int, 50));
                command.Parameters.Add(new SqlParameter("@NewCtrlName", SqlDbType.NVarChar, 50));
                command.Parameters.Add(new SqlParameter("@NewCtrlPID", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@OPlogPID", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@TracingInfo", SqlDbType.NVarChar, 4000));
                command.Parameters.Add(new SqlParameter("@myErr", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@myErrMsg", SqlDbType.NVarChar, 4000));
                command.UpdatedRowSource = UpdateRowSource.None;
                command.Parameters["@SourceCtrlID"].Value = sourceCtrlID;
                command.Parameters["@NewCtrlPID"].Value = newCtrlPID;
                command.Parameters["@NewCtrlName"].Value = newCtrlName;

                command.Parameters["@OPlogPID"].Value = opLogPID;
                command.Parameters["@TracingInfo"].Value = TracingInfo;
                command.Parameters["@myErrMsg"].Direction = ParameterDirection.Output;
                command.Parameters["@myErr"].Direction = ParameterDirection.Output;

                command.ExecuteNonQuery();
                errMsg = command.Parameters["@myErrMsg"].Value.ToString();
                myErr = Convert.ToInt32(command.Parameters["@myErr"].Value);
                //OpenDatabase(false);

                return myErr;
            }
            catch (Exception ex)
            {
                if (command.Parameters["@myErrMsg"].Value != null)
                {
                    errMsg = command.Parameters["@myErrMsg"].Value.ToString();
                    myErr = Convert.ToInt32(command.Parameters["@myErr"].Value);
                    WriteErrorLogs("Err:" + myErr + ";" + errMsg);
                    //HttpContext.Current.Response.Write("<Script>alert('Error:" + myErr + ";" + errMsg.Replace("'", @"""") + "')</Script>");                   
                }
                else
                {
                    WriteErrorLogs(ex.ToString());
                    errMsg = ex.ToString();
                    //HttpContext.Current.Response.Write("<Script>alert('Error:" + ex.ToString().Replace("'", @"""").Replace("\r\n","\\n") + "')</Script>"); 
                }
                return myErr;
            }
            finally
            {
                OpenDatabase(false);
            }
        }
        #endregion

        public override int CopyFlowCtrl(int sourceCtrlID, string newCtrlName, int newCtrlPID, string tracingInfo, out string errMsg)
        {
            int result = -1;
            int opLogPID = -1;
            errMsg = "";
            try
            {
                #region get Session["UserLoginID"]
                if (HttpContext.Current.Session["UserLoginID"] != null)
                {
                    opLogPID = Convert.ToInt32(HttpContext.Current.Session["UserLoginID"].ToString());
                }
                else
                {
                    errMsg = "Can not find any UserLoginID Info!";
                    //HttpContext.Current.Response.Write("<Script>alert('Error:" + errMsg.Replace("'", @"""") + "')</Script>");
                    return -100;
                }
                #endregion

                #region  Pro_CopyFlowCtrl
                result = Pro_CopyFlowCtrl(sourceCtrlID, newCtrlName, newCtrlPID, opLogPID, tracingInfo, out errMsg);
                #endregion

                return result;   //即Error               
            }
            catch (Exception ex)
            {
                WriteErrorLogs(ex.ToString());
                return -1;

            }
        }
    
    }

    public class AccessManager : LocalDatabaseIO     //140722
    {
        public AccessManager(string AccessFilePath)
            : base(AccessFilePath)
        {
        }

        public override long GetLastInsertData(string TableName)
        {
            long myValue = 0;
            try
            {
                if (conn == null) conn.Open();
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                string sqlCMD1 = "Select count(ID) From " + TableName;
                OleDbCommand mySQLcmd1 = new OleDbCommand(sqlCMD1, conn);
                long myValue1 = Convert.ToInt64(mySQLcmd1.ExecuteScalar());

                if (myValue1 > 0)
                {
                    string sqlCMD = "Select MAX(ID) From " + TableName;
                    OleDbCommand mySQLcmd = new OleDbCommand(sqlCMD, conn);
                    myValue = Convert.ToInt64(mySQLcmd.ExecuteScalar());
                }

                return myValue;
            }

            catch (Exception ex)
            {
                WriteErrorLogs(ex.ToString());
                AlertMsgShow(ex.Message); return myValue;
            }

            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        public override long GetPID(string sqlCmd)
        {
            try
            {
                if (conn == null) conn.Open();
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                OleDbCommand getPID = new OleDbCommand(sqlCmd, conn);
                return Convert.ToInt64(getPID.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                WriteErrorLogs(ex.ToString());
                return -1;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        long GetLastInsertData(string TableName, OleDbTransaction mytr)
        {
            long myValue = 0;
            try
            {
                string sqlCMD1 = "Select count(ID) From " + TableName;
                OleDbCommand mySQLcmd1 = new OleDbCommand(sqlCMD1, conn);
                mySQLcmd1.Transaction = mytr;
                long myValue1 = Convert.ToInt64(mySQLcmd1.ExecuteScalar());

                if (myValue1 > 0)
                {
                    string sqlCMD = "Select MAX(ID) From " + TableName;
                    OleDbCommand mySQLcmd = new OleDbCommand(sqlCMD, conn);
                    mySQLcmd.Transaction = mytr;
                    myValue = Convert.ToInt64(mySQLcmd.ExecuteScalar());
                }

                return myValue;
            }

            catch (Exception ex)
            {
                WriteErrorLogs(ex.ToString());
                AlertMsgShow(ex.Message); return myValue;
            }

        }

        public override bool UpdateDT()
        {
            WriteErrorLogs("Not support UpdateDT() method~");
            return false;
        }

        public override bool UpdateDataTable(string SQLCmd, DataTable NewChangeDT)
        {
            bool result = false;
            OleDbTransaction tr;
            if (conn == null) conn.Open();

            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            tr = conn.BeginTransaction(IsolationLevel.RepeatableRead);
            try
            {
                OleDbCommand cm = new OleDbCommand(SQLCmd, conn);
                OleDbDataAdapter da = new OleDbDataAdapter(cm);
                OleDbCommandBuilder cb = new OleDbCommandBuilder(da);
                da.SelectCommand.Transaction = tr;
                if (NewChangeDT.GetChanges() != null)
                {
                    da.Update(NewChangeDT);
                }
                tr.Commit();
                result = true;

                return result;
            }
            catch (Exception ex)
            {
                try
                {
                    //141031_00----------
                    if (ex.Message.ToUpper().Contains("违反并发性"))
                    {
                        WriteErrorLogs("Update data failed:\n" + "Maybe Other users have already deleted the target data records\n" + ex.Message);
                    }
                    else
                    {
                        WriteErrorLogs(ex.ToString());
                    }
                    //WriteErrorLogs(ex.ToString());
                    //-------------------
                    tr.Rollback();
                    if (conn.State == ConnectionState.Open) conn.Close();
                    return result;
                }
                catch (System.Exception TransactionEx)
                {
                    //Handle Exception
                    WriteErrorLogs(TransactionEx.ToString());
                    if (conn.State == ConnectionState.Open) conn.Close();
                    return result;
                }
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        public override bool UpdateDataTable(string SQLCmd, DataTable NewChangeDT, out long LstID)
        {
            bool result = false;
            LstID = -1;
            OleDbTransaction tr;
            if (conn == null) conn.Open();

            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            tr = conn.BeginTransaction(IsolationLevel.RepeatableRead);
            try
            {
                OleDbCommand cm = new OleDbCommand(SQLCmd, conn);
                OleDbDataAdapter da = new OleDbDataAdapter(cm);
                OleDbCommandBuilder cb = new OleDbCommandBuilder(da);
                da.SelectCommand.Transaction = tr;
                if (NewChangeDT.GetChanges() != null)
                {
                    da.Update(NewChangeDT);
                    LstID = GetLastInsertData(NewChangeDT.TableName);
                }
                tr.Commit();
                result = true;

                return result;
            }
            catch (Exception ex)
            {
                try
                {
                    //141031_00----------
                    if (ex.Message.ToUpper().Contains("违反并发性"))
                    {
                        WriteErrorLogs("Update data failed:\n" + "Maybe Other users have already deleted the target data records\n" + ex.Message);
                    }
                    else
                    {
                        WriteErrorLogs(ex.ToString());
                    }
                    //WriteErrorLogs(ex.ToString());
                    //-------------------
                    tr.Rollback();
                    if (conn.State == ConnectionState.Open) conn.Close();
                    return result;
                }
                catch (System.Exception TransactionEx)
                {
                    //Handle Exception
                    WriteErrorLogs(TransactionEx.ToString());
                    if (conn.State == ConnectionState.Open) conn.Close();
                    return result;
                }
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }
        public override bool BlnISExistTable(string tabName)
        {
            try
            {
                if (conn == null) conn.Open();
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                bool existTab = false;

                DataTable dt = conn.GetSchema("Tables");
                int n = dt.Rows.Count;
                int m = dt.Columns.IndexOf("TABLE_NAME");

                string[] tabsName = new string[n];
                for (int i = 0; i < n; i++)
                {
                    tabsName[i] = dt.Rows[i].ItemArray.GetValue(m).ToString();
                    if (tabName == tabsName[i])
                    {
                        existTab = true;
                        break;
                    }
                }

                if (existTab)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                WriteErrorLogs("Can't find this table:  " + tabName + "\n" + ex.Message);
                return false;
            }
            finally
            {
                if (conn.State.ToString().ToUpper() == "OPEN")
                    conn.Close();
            }
        }

        public override string[] GetCurrTablesName(string ServerName, string DBName, string userName, string pwd)
        {
            string strConnection = @"Data Source=" + ServerName + ";Initial Catalog=" + DBName + ";Provider=SQLOLEDB.1;user id = " + userName + ";password=" + pwd + ";";
            //string strConnection = "Provider = Microsoft.ACE.OLEDB.12.0;" + @"Data Source=" + Login.AccessFilePath; 
            OleDbConnection myConn = new OleDbConnection(strConnection);
            try
            {
                if (myConn == null) myConn.Open();
                if (myConn.State != ConnectionState.Open)
                {
                    myConn.Open();
                }
                //DataTable cnSch = myAccessIO.Conn.GetSchema("Tables");

                DataTable cnSch = myConn.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });

                int n = cnSch.Rows.Count;
                int m = cnSch.Columns.IndexOf("TABLE_NAME");

                string[] tabsName = new string[n];
                for (int i = 0; i < n; i++)
                {
                    tabsName[i] = cnSch.Rows[i].ItemArray.GetValue(m).ToString();
                }
                return tabsName;
            }

            catch (Exception ex)
            {
                WriteErrorLogs(ex.Message);
                return null;
            }
            finally
            {
                if (myConn.State.ToString().ToUpper() == "OPEN")
                    myConn.Close();
            }
        }

        public override string[] GetCurrTablesName(string Accesspath) //
        {
            string strConnection = "";
            if (Accesspath.ToUpper().Contains(".accdb".ToUpper()))
            {
                strConnection = "Provider = Microsoft.ACE.OLEDB.12.0;" + @"Data Source=" + Accesspath;
            }
            else
            {
                strConnection = "Provider=Microsoft.Jet.OleDb.4.0;" + @"Data Source=" + Accesspath;
            }
            OleDbConnection myConn = new OleDbConnection(strConnection);
            try
            {
                if (myConn == null) myConn.Open();
                if (myConn.State != ConnectionState.Open)
                {
                    myConn.Open();
                }
                //DataTable cnSch = myAccessIO.Conn.GetSchema("Tables");

                DataTable cnSch = myConn.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });

                int n = cnSch.Rows.Count;
                int m = cnSch.Columns.IndexOf("TABLE_NAME");

                string[] tabsName = new string[n];
                for (int i = 0; i < n; i++)
                {
                    tabsName[i] = cnSch.Rows[i].ItemArray.GetValue(m).ToString();
                }
                return tabsName;
            }

            catch (Exception ex)
            {
                WriteErrorLogs(ex.Message);
                return null;
            }
            finally
            {
                if (myConn.State.ToString().ToUpper() == "OPEN")
                    myConn.Close();
            }
        }

        public override bool UpdateDataTable(string SQLCmd, DataTable NewChangeDT, bool IsAddNewData)
        {
            bool result = false;
            OleDbTransaction tr;
            if (conn == null) conn.Open();

            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            tr = conn.BeginTransaction(IsolationLevel.RepeatableRead);
            try
            {
                OleDbCommand cm = new OleDbCommand(SQLCmd, conn);
                OleDbDataAdapter da = new OleDbDataAdapter(cm);
                OleDbCommandBuilder cb = new OleDbCommandBuilder(da);
                da.SelectCommand.Transaction = tr;
                if (IsAddNewData)
                {
                    for (int i = 0; i < NewChangeDT.Rows.Count; i++)
                    {
                        NewChangeDT.Rows[i].SetAdded();
                    }
                }
                if (NewChangeDT.GetChanges() != null)
                {
                    da.Update(NewChangeDT);
                }
                tr.Commit();
                result = true;

                return result;
            }
            catch (Exception ex)
            {
                try
                {
                    //141031_00----------
                    if (ex.Message.ToUpper().Contains("违反并发性"))
                    {
                        WriteErrorLogs("Update data failed:\n" + "Maybe Other users have already deleted the target data records\n" + ex.Message);
                    }
                    else
                    {
                        WriteErrorLogs(ex.ToString());
                    }
                    //WriteErrorLogs(ex.ToString());
                    //-------------------
                    tr.Rollback();
                    if (conn.State == ConnectionState.Open) conn.Close();
                    return result;
                }
                catch (System.Exception TransactionEx)
                {
                    //Handle Exception
                    WriteErrorLogs(TransactionEx.ToString());
                    if (conn.State == ConnectionState.Open) conn.Close();
                    return result;
                }
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        public override bool UpdateOperateLog(string loginID, string blockTypeName, string tracingInfo, DataTable dt, string dtName, string sqlCmd = "Select * from OperationLogs where ID=-1") //150414 Add
        {
            bool result = false;
            string[] pLogs = new string[3];
            string[] pOpType = new string[3];
            if (conn == null) conn.Open();
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }

            try
            {
                CommCtrl pCommCtrl = new CommCtrl();
                OleDbCommand cm = new OleDbCommand(sqlCmd, conn);
                OleDbDataAdapter da = new OleDbDataAdapter(cm);
                OleDbCommandBuilder cb = new OleDbCommandBuilder(da);
                DataTable mydt = new DataTable();
                da.Fill(mydt);
                DataTable addDt = dt.GetChanges(DataRowState.Added);
                DataTable delelteDt = dt.GetChanges(DataRowState.Deleted);
                DataTable modifyDt = dt.GetChanges(DataRowState.Modified);
                if (addDt != null)
                {
                    pLogs = pCommCtrl.getOpLogs(addDt, dtName, out pOpType);

                    if (pOpType[0].Length > 0)
                    {
                        DataRow dr = mydt.NewRow();
                        dr["PID"] = loginID;
                        dr["ModifyTime"] = GetCurrTime();
                        dr["BlockType"] = blockTypeName;
                        dr["Optype"] = pOpType[0];
                        dr["TracingInfo"] = tracingInfo;
                        dr["DetailLogs"] = pLogs[0];
                        mydt.Rows.Add(dr);
                    }
                }

                if (delelteDt != null)
                {
                    pLogs = pCommCtrl.getOpLogs(delelteDt, dtName, out pOpType);
                    if (pOpType[1].Length > 0)
                    {
                        DataRow dr = mydt.NewRow();
                        dr["PID"] = loginID;
                        dr["ModifyTime"] = GetCurrTime();
                        dr["BlockType"] = blockTypeName;
                        dr["Optype"] = pOpType[1];
                        dr["TracingInfo"] = tracingInfo;
                        dr["DetailLogs"] = pLogs[1];
                        mydt.Rows.Add(dr);
                    }
                }

                if (modifyDt != null)
                {
                    pLogs = pCommCtrl.getOpLogs(modifyDt, dtName, out pOpType);
                    if (pOpType[2].Length > 0)
                    {
                        DataRow dr = mydt.NewRow();
                        dr["PID"] = loginID;
                        dr["ModifyTime"] = GetCurrTime();
                        dr["BlockType"] = blockTypeName;
                        dr["Optype"] = pOpType[2];
                        dr["TracingInfo"] = tracingInfo;
                        dr["DetailLogs"] = pLogs[2];
                        mydt.Rows.Add(dr);
                    }
                }

                if (mydt.GetChanges() != null)
                {
                    UpdateDataTable(sqlCmd, mydt);
                }

                result = true;

                return result;
            }
            catch (Exception ex)
            {
                #region Err
                try
                {
                    //141031_00----------
                    if (ex.Message.ToUpper().Contains("违反并发性"))
                    {
                        WriteErrorLogs("Update data failed:\n" + "Maybe Other users have already deleted the target data records\n" + ex.Message);
                    }
                    else
                    {
                        WriteErrorLogs(ex.ToString());
                    }
                    //WriteErrorLogs(ex.ToString());
                    if (conn.State.ToString().ToUpper() == "OPEN") conn.Close();
                    return result;
                }
                catch (System.Exception TransactionEx)
                {
                    //Handle Exception
                    WriteErrorLogs(TransactionEx.ToString());
                    if (conn.State.ToString().ToUpper() == "OPEN") conn.Close();
                    return result;
                }
                #endregion
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

    }
}

using System;
using System.Collections.Generic;

using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;
using System.Data.SqlClient;
using ATSDataBase;

namespace Maintain
{
    public class SqlManager : ServerDatabaseIO   //140722
    {
        public SqlManager(string serverName)
            : base(serverName) //读取XML后配置此部分
        {
        }
        //public ServerDatabaseIO(string serverName,string dbName,string user,string pwd)
        public SqlManager(string serverName, string dbName, string user, string pwd)
            : base(serverName, dbName, user, pwd)   //140722
        {
        }

        void WriteErrorLogs(string ss)  //141031_00
        {
            try
            {
                System.IO.FileStream fs = new System.IO.FileStream(Application.StartupPath + @"\SQLChangeErrorLogs.txt", System.IO.FileMode.Append);
                System.IO.StreamWriter sw = new System.IO.StreamWriter(fs, Encoding.Default);
                sw.WriteLine("=====Error=====\r\n" + DateTime.Now.ToString() + "\r\n" + ss);
                sw.Close();
                fs.Close();
            }
            catch
            { }
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

                if (myCurrIdent > 1)    //140707_2
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
                MessageBox.Show(ex.ToString());
                return 0;
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
            }
        }

        long GetLastInsertData(string TableName, SqlTransaction mytr)    //141021_1
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

                if (myCurrIdent > 1)    //140707_2
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
                MessageBox.Show(ex.ToString());
                return 0;
            }
        }

        public override bool UpdateDT()
        {
            return false;
        }

        //140612_1 
        public override bool UpdateDataTable(string SQLCmd, DataTable NewChangeDT)
        {
            bool result = false;

            SqlTransaction tr;

            if (conn == null) conn.Open();
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            tr = conn.BeginTransaction(IsolationLevel.ReadCommitted);

            try
            {
                SqlCommand cm = new SqlCommand(SQLCmd, conn);
                SqlDataAdapter da = new SqlDataAdapter(cm);
                SqlCommandBuilder cb = new SqlCommandBuilder(da);
                DataTable mydt = new DataTable();
                da.SelectCommand.Transaction = tr;
                mydt = NewChangeDT.GetChanges();
                da.Update(mydt);
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
                        MessageBox.Show("Update data failed:\n" + "Maybe Other users have already deleted the target data records\n" + ex.Message);
                    }
                    else
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    WriteErrorLogs(ex.ToString());
                    //-------------------
                    tr.Rollback();
                    if (conn.State.ToString().ToUpper() == "OPEN") conn.Close();
                    return result;
                }
                catch (System.Exception TransactionEx)
                {
                    //Handle Exception
                    MessageBox.Show(TransactionEx.ToString());
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
                MessageBox.Show(ex.ToString());
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
                MessageBox.Show("Can't find this table:  " + tabName + "\n" + ex.Message);
                return false;
            }
        }

        public override string[] GetCurrTablesName(string ServerName, string DBName, string userName, string pwd)
        {
            //140610_2227
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
                MessageBox.Show(ex.Message);
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
                MessageBox.Show(ex.Message);
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
            tr = conn.BeginTransaction(IsolationLevel.ReadCommitted);

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
                da.Update(NewChangeDT);
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
                        MessageBox.Show("Update data failed:\n" + "Maybe Other users have already deleted the target data records\n" + ex.Message);
                    }
                    else
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    WriteErrorLogs(ex.ToString());
                    //-------------------
                    tr.Rollback();
                    if (conn.State.ToString().ToUpper() == "OPEN") conn.Close();
                    return result;
                }
                catch (System.Exception TransactionEx)
                {
                    //Handle Exception
                    MessageBox.Show(TransactionEx.ToString());
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
    }

    public class AccessManager : LocalDatabaseIO     //140722
    {
        public AccessManager(string AccessFilePath)
            : base(AccessFilePath)
        {
        }

        void WriteErrorLogs(string ss)  //141031_00
        {
            try
            {
                System.IO.FileStream fs = new System.IO.FileStream(Application.StartupPath + @"\AccdbChangeErrorLogs.txt", System.IO.FileMode.Append);
                System.IO.StreamWriter sw = new System.IO.StreamWriter(fs, Encoding.Default);
                sw.WriteLine("=====Error=====\r\n" + DateTime.Now.ToString() + "\r\n" + ss);
                sw.Close();
                fs.Close();
            }
            catch
            { }
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
                MessageBox.Show(ex.ToString());
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
                MessageBox.Show(ex.ToString());
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
                MessageBox.Show(ex.ToString());
                return -1;
            }

        }

        public override bool UpdateDT()
        {
            return false;
        }

        //140612_1
        public override bool UpdateDataTable(string SQLCmd, DataTable NewChangeDT)
        {
            bool result = false;
            OleDbTransaction tr;
            if (conn == null) conn.Open();

            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            tr = conn.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {
                OleDbCommand cm = new OleDbCommand(SQLCmd, conn);
                OleDbDataAdapter da = new OleDbDataAdapter(cm);
                OleDbCommandBuilder cb = new OleDbCommandBuilder(da);
                da.SelectCommand.Transaction = tr;

                da.Update(NewChangeDT);
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
                        MessageBox.Show("Update data failed:\n" + "Maybe Other users have already deleted the target data records\n" + ex.Message);
                    }
                    else
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    WriteErrorLogs(ex.ToString());
                    //-------------------
                    tr.Rollback();
                    if (conn.State == ConnectionState.Open) conn.Close();
                    return result;
                }
                catch (System.Exception TransactionEx)
                {
                    //Handle Exception
                    MessageBox.Show(TransactionEx.ToString());
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
                MessageBox.Show("Can't find this table:  " + tabName + "\n" + ex.Message);
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
            //140610_2227
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
                MessageBox.Show(ex.Message);
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
                MessageBox.Show(ex.Message);
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
            tr = conn.BeginTransaction(IsolationLevel.ReadCommitted);
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

                da.Update(NewChangeDT);
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
                        MessageBox.Show("Update data failed:\n" + "Maybe Other users have already deleted the target data records\n" + ex.Message);
                    }
                    else
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    WriteErrorLogs(ex.ToString());
                    //-------------------
                    tr.Rollback();
                    if (conn.State == ConnectionState.Open) conn.Close();
                    return result;
                }
                catch (System.Exception TransactionEx)
                {
                    //Handle Exception
                    MessageBox.Show(TransactionEx.ToString());
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
    }
}

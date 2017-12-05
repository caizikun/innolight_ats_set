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
        //140722_1
        //public override bool OpenDatabase(bool state)
        //{
        //    return base.OpenDatabase(state);
        //}

        //public override DataTable GetDataTable(string sqlCmd, string StrTableName)
        //{
        //    return base.GetDataTable(sqlCmd, StrTableName);
        //}

        public override long GetLastInsertData(string TableName)    //140710_0
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
                long myCurrIdent = Convert.ToInt64(mySQLcmd.ExecuteScalar());

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
                if (mydt!=null )
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
                        MessageBox.Show("更新资料失败:\n" + "找不到需要更新的目标资料!可能已经被其他用户已经删除该目标资料记录...\n" + ex.Message);
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
                SqlCommand getPID=new SqlCommand(sqlCmd,conn);
                return Convert.ToInt64(getPID.ExecuteScalar().ToString ());
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

                string [] tabsName = new string[n];
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
                MessageBox.Show("在当前Database中查询名称为:  " + tabName + " 的表失败;请确认! \n" + ex.Message);
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
                MessageBox.Show("在当前Database中查询表失败;请确认! \n" + ex.Message);
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
            string strConnection = "Provider = Microsoft.ACE.OLEDB.12.0;" + @"Data Source=" + Accesspath;
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
                MessageBox.Show("在当前Database中查询表失败;请确认! \n" + ex.Message);
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
                        MessageBox.Show("更新资料失败:\n" + "找不到需要更新的目标资料!可能已经被其他用户已经删除该目标资料记录...\n" + ex.Message);
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

        void WriteErrorLogs(string ss)
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
    }

    public class AccessManager : LocalDatabaseIO     //140722
    {    
        public AccessManager(string AccessFilePath): base(AccessFilePath)
        {
        }

        //140722_1
        //public override bool OpenDatabase(bool state)
        //{
        //    return base.OpenDatabase(state);
        //}

        //public override DataTable GetDataTable(string sqlCmd, string StrTableName)
        //{
        //    return base.GetDataTable(sqlCmd, StrTableName);
        //}

        public override long GetLastInsertData(string TableName)
        {
            long myValue=0;
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

        //public override bool UpdateDT()
        //{
        //    bool myOPresult = false;

        //    long myNewlastIDTestPlan = GetLastInsertData(PNInfo.ConstTestPlanTables[1]);
        //    long myNewlastIDTestCtrl = GetLastInsertData(PNInfo.ConstTestPlanTables[2]);
        //    long myNewlastIDTestModel = GetLastInsertData(PNInfo.ConstTestPlanTables[3]);
        //    long myNewlastIDTestPrmtr = GetLastInsertData(PNInfo.ConstTestPlanTables[4]);
        //    long myNewlastIDTestEquip = GetLastInsertData(PNInfo.ConstTestPlanTables[5]);
        //    long myNewlastIDTestEquipPrmtr = GetLastInsertData(PNInfo.ConstTestPlanTables[6]);
        //    if (conn == null) conn.Open();

        //    if (conn.State != ConnectionState.Open)
        //    {
        //        conn.Open();
        //    }
        //    OleDbTransaction tr;
        //    tr = conn.BeginTransaction(IsolationLevel.ReadCommitted);

        //    try
        //    {
        //        OleDbCommand cm1 = new OleDbCommand("select * from  " + PNInfo.ConstTestPlanTables[1], conn);
        //        OleDbDataAdapter da1 = new OleDbDataAdapter(cm1);
        //        OleDbCommandBuilder cb1 = new OleDbCommandBuilder(da1);
        //        DataTable dt1 = new DataTable();
        //        da1.SelectCommand.Transaction = tr; //140612_1

        //        OleDbCommand cm2 = new OleDbCommand("select * from  " + PNInfo.ConstTestPlanTables[2], conn);
        //        OleDbDataAdapter da2 = new OleDbDataAdapter(cm2);
        //        OleDbCommandBuilder cb2 = new OleDbCommandBuilder(da2);
        //        DataTable dt2 = new DataTable();
        //        da2.SelectCommand.Transaction = tr; //140612_1

        //        OleDbCommand cm3 = new OleDbCommand("select * from  " + PNInfo.ConstTestPlanTables[3], conn);
        //        OleDbDataAdapter da3 = new OleDbDataAdapter(cm3);
        //        OleDbCommandBuilder cb3 = new OleDbCommandBuilder(da3);
        //        DataTable dt3 = new DataTable();
        //        da3.SelectCommand.Transaction = tr; //140612_1

        //        OleDbCommand cm4 = new OleDbCommand("select * from  " + PNInfo.ConstTestPlanTables[4], conn);
        //        OleDbDataAdapter da4 = new OleDbDataAdapter(cm4);
        //        OleDbCommandBuilder cb4 = new OleDbCommandBuilder(da4);
        //        DataTable dt4 = new DataTable();
        //        da4.SelectCommand.Transaction = tr; //140612_1

        //        OleDbCommand cm5 = new OleDbCommand("select * from  " + PNInfo.ConstTestPlanTables[5], conn);
        //        OleDbDataAdapter da5 = new OleDbDataAdapter(cm5);
        //        OleDbCommandBuilder cb5 = new OleDbCommandBuilder(da5);
        //        DataTable dt5 = new DataTable();
        //        da5.SelectCommand.Transaction = tr; //140612_1

        //        OleDbCommand cm6 = new OleDbCommand("select * from  " + PNInfo.ConstTestPlanTables[6], conn);
        //        OleDbDataAdapter da6 = new OleDbDataAdapter(cm6);
        //        OleDbCommandBuilder cb6 = new OleDbCommandBuilder(da6);
        //        DataTable dt6 = new DataTable();
        //        da6.SelectCommand.Transaction = tr; //140612_1

        //        for (int i = 0; i < PNInfo.TopoToatlDS.Tables[1].Rows.Count; i++)
        //        {
        //            if ((PNInfo.TopoToatlDS.Tables[1].Rows[i].RowState == DataRowState.Added)
        //                && (Convert.ToInt64(PNInfo.TopoToatlDS.Tables[1].Rows[i]["ID"]) > PNInfo.origIDTestPlan)
        //                )
        //            {
        //                PNInfo.TopoToatlDS.Tables[1].Rows[i]["ID"] = myNewlastIDTestPlan + 1;
        //                myNewlastIDTestPlan++;
        //            }
        //        }

        //        for (int i = 0; i < PNInfo.TopoToatlDS.Tables[2].Rows.Count; i++)
        //        {
        //            if ((PNInfo.TopoToatlDS.Tables[2].Rows[i].RowState == DataRowState.Added)
        //                 && (Convert.ToInt64(PNInfo.TopoToatlDS.Tables[2].Rows[i]["ID"]) > PNInfo.origIDTestCtrl)
        //               )
        //            {
        //                PNInfo.TopoToatlDS.Tables[2].Rows[i]["ID"] = myNewlastIDTestCtrl + 1;
        //                myNewlastIDTestCtrl++;
        //            }
        //        }

        //        for (int i = 0; i < PNInfo.TopoToatlDS.Tables[3].Rows.Count; i++)
        //        {
        //            if (
        //                 (PNInfo.TopoToatlDS.Tables[3].Rows[i].RowState == DataRowState.Added)
        //                 &&(Convert.ToInt64(PNInfo.TopoToatlDS.Tables[3].Rows[i]["ID"]) > PNInfo.origIDTestModel)
        //                )
        //            {
        //                PNInfo.TopoToatlDS.Tables[3].Rows[i]["ID"] = myNewlastIDTestModel + 1;
        //                myNewlastIDTestModel++;
        //            }
        //        }

        //        for (int i = 0; i < PNInfo.TopoToatlDS.Tables[4].Rows.Count; i++)
        //        {
        //            if ((PNInfo.TopoToatlDS.Tables[4].Rows[i].RowState == DataRowState.Added)
        //                && (Convert.ToInt64(PNInfo.TopoToatlDS.Tables[4].Rows[i]["ID"]) > PNInfo.origIDTestPrmtr)
        //                )
        //            {
        //                PNInfo.TopoToatlDS.Tables[4].Rows[i]["ID"] = myNewlastIDTestPrmtr + 1;
        //                myNewlastIDTestPrmtr++;
        //            }
        //        }

        //        for (int i = 0; i < PNInfo.TopoToatlDS.Tables[5].Rows.Count; i++)
        //        {
        //            if ( (PNInfo.TopoToatlDS.Tables[5].Rows[i].RowState == DataRowState.Added)
        //                &&(Convert.ToInt64(PNInfo.TopoToatlDS.Tables[5].Rows[i]["ID"]) > PNInfo.origIDTestEquip)
        //                )
        //            {
        //                PNInfo.TopoToatlDS.Tables[5].Rows[i]["ID"] = myNewlastIDTestEquip + 1;
        //                myNewlastIDTestEquip++;
        //            }
        //        }
                
        //        for (int i = 0; i < PNInfo.TopoToatlDS.Tables[6].Rows.Count; i++)
        //        {
        //            if (
        //                (PNInfo.TopoToatlDS.Tables[6].Rows[i].RowState == DataRowState.Added)
        //                && (Convert.ToInt64(PNInfo.TopoToatlDS.Tables[6].Rows[i]["ID"]) > PNInfo.origIDTestEquipPrmtr))
        //            {
        //                PNInfo.TopoToatlDS.Tables[6].Rows[i]["ID"] = myNewlastIDTestEquipPrmtr + 1;
        //                myNewlastIDTestEquipPrmtr++;
        //            }
        //        }

        //        dt1 = PNInfo.TopoToatlDS.Tables[1];
        //        dt2 = PNInfo.TopoToatlDS.Tables[2];
        //        dt3 = PNInfo.TopoToatlDS.Tables[3];
        //        dt4 = PNInfo.TopoToatlDS.Tables[4];
        //        dt5 = PNInfo.TopoToatlDS.Tables[5];
        //        dt6 = PNInfo.TopoToatlDS.Tables[6];

        //        DataTable dtAdded1 = new DataTable(); //存放主表 新增 的数据
        //        DataTable dtMidified1 = new DataTable(); //存储主表 编辑 的行记录
        //        DataTable dtDeleted1 = new DataTable(); //存储主表 删除 的行记录

        //        dtDeleted1 = dt1.GetChanges(DataRowState.Deleted);
        //        dtMidified1 = dt1.GetChanges(DataRowState.Modified);
        //        dtAdded1 = dt1.GetChanges(DataRowState.Added);

        //        DataTable dtAdded2 = new DataTable(); //存放主表 新增 的数据
        //        DataTable dtMidified2 = new DataTable(); //存储主表 编辑 的行记录
        //        DataTable dtDeleted2 = new DataTable(); //存储主表 删除 的行记录

        //        dtDeleted2 = dt2.GetChanges(DataRowState.Deleted);
        //        dtMidified2 = dt2.GetChanges(DataRowState.Modified);
        //        dtAdded2 = dt2.GetChanges(DataRowState.Added);

        //        DataTable dtAdded3 = new DataTable(); //存放主表 新增 的数据
        //        DataTable dtMidified3 = new DataTable(); //存储主表 编辑 的行记录
        //        DataTable dtDeleted3 = new DataTable(); //存储主表 删除 的行记录

        //        dtDeleted3 = dt3.GetChanges(DataRowState.Deleted);
        //        dtMidified3 = dt3.GetChanges(DataRowState.Modified);
        //        dtAdded3 = dt3.GetChanges(DataRowState.Added);

        //        DataTable dtAdded4 = new DataTable(); //存放主表 新增 的数据
        //        DataTable dtMidified4 = new DataTable(); //存储主表 编辑 的行记录
        //        DataTable dtDeleted4 = new DataTable(); //存储主表 删除 的行记录

        //        dtDeleted4 = dt4.GetChanges(DataRowState.Deleted);
        //        dtMidified4 = dt4.GetChanges(DataRowState.Modified);
        //        dtAdded4 = dt4.GetChanges(DataRowState.Added);

        //        DataTable dtAdded5 = new DataTable(); //存放主表 新增 的数据
        //        DataTable dtMidified5 = new DataTable(); //存储主表 编辑 的行记录
        //        DataTable dtDeleted5 = new DataTable(); //存储主表 删除 的行记录

        //        dtDeleted5 = dt5.GetChanges(DataRowState.Deleted);
        //        dtMidified5 = dt5.GetChanges(DataRowState.Modified);
        //        dtAdded5 = dt5.GetChanges(DataRowState.Added);

        //        DataTable dtAdded6 = new DataTable(); //存放主表 新增 的数据
        //        DataTable dtMidified6 = new DataTable(); //存储主表 编辑 的行记录
        //        DataTable dtDeleted6 = new DataTable(); //存储主表 删除 的行记录

        //        dtDeleted6 = dt6.GetChanges(DataRowState.Deleted);
        //        dtMidified6 = dt6.GetChanges(DataRowState.Modified);
        //        dtAdded6 = dt6.GetChanges(DataRowState.Added);

        //        if (dtDeleted6 != null) da6.Update(dtDeleted6);
        //        if (dtDeleted5 != null) da5.Update(dtDeleted5);
        //        if (dtDeleted4 != null) da4.Update(dtDeleted4);
        //        if (dtDeleted3 != null) da3.Update(dtDeleted3);
        //        if (dtDeleted2 != null) da2.Update(dtDeleted2);
        //        if (dtDeleted1 != null) da1.Update(dtDeleted1);


        //        if (dtAdded1 != null) da1.Update(dtAdded1);
        //        if (dtMidified1 != null) da1.Update(dtMidified1);

        //        if (dtAdded2 != null) da2.Update(dtAdded2);
        //        if (dtMidified2 != null) da2.Update(dtMidified2);

        //        if (dtAdded3 != null) da3.Update(dtAdded3);
        //        if (dtMidified3 != null) da3.Update(dtMidified3);


        //        if (dtAdded4 != null) da4.Update(dtAdded4);
        //        if (dtMidified4 != null) da4.Update(dtMidified4);

        //        if (dtAdded5 != null) da5.Update(dtAdded5);
        //        if (dtMidified5 != null) da5.Update(dtMidified5);                

        //        if (dtAdded6 != null) da6.Update(dtAdded6);
        //        if (dtMidified6 != null) da6.Update(dtMidified6);

        //        //tr.Rollback();
        //        tr.Commit();

        //        myOPresult = true;
        //        PNInfo.TopoToatlDSAcceptChanges();
        //        return myOPresult;
        //    }
        //    catch (Exception ex)
        //    {
        //        try
        //        {
        //            MessageBox.Show(ex.ToString());
        //            tr.Rollback();
        //            if (conn.State == ConnectionState.Open) conn.Close();
        //            return myOPresult;
        //        }
        //        catch (System.Exception TransactionEx)
        //        {
        //            //Handle Exception
        //            MessageBox.Show(TransactionEx.ToString());
        //            if (conn.State == ConnectionState.Open) conn.Close();
        //            return myOPresult;
        //        }
        //    }
        //    finally
        //    {
        //        if (conn.State.ToString().ToUpper() == "OPEN")
        //            conn.Close();
        //    }
        //}

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
                        MessageBox.Show("更新资料失败:\n" + "找不到需要更新的目标资料!可能已经被其他用户已经删除该目标资料记录...\n" + ex.Message);
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
                MessageBox.Show("在当前Database中查询名称为:  " + tabName + " 的表失败;请确认! \n" + ex.Message);
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
                MessageBox.Show("在当前Database中查询表失败;请确认! \n" + ex.Message);
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
            string strConnection = "Provider = Microsoft.ACE.OLEDB.12.0;" + @"Data Source=" + Accesspath;
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
                MessageBox.Show("在当前Database中查询表失败;请确认! \n" + ex.Message);
                return null;
            }
            finally
            {
                if (myConn.State.ToString().ToUpper() == "OPEN")
                    myConn.Close();
            }
        }

        public override bool UpdateDataTable(string SQLCmd, DataTable NewChangeDT,bool IsAddNewData)
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
                        MessageBox.Show("更新资料失败:\n" + "找不到需要更新的目标资料!可能已经被其他用户已经删除该目标资料记录...\n" + ex.Message);
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

        void WriteErrorLogs(string ss)
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
    }
}

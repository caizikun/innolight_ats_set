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

                //141021_1
                string ss = mySQLcmd.ExecuteScalar().ToString();
                long myCurrIdent = 0;
                if (ss.Length > 0)
                {
                    myCurrIdent = Convert.ToInt64(ss);
                }
                else
                {
                    string sqlCMD2 = "Select max(id) from " + TableName ;
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
                    string sqlCMD2 = "Select max(id) from " + TableName ;
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
            bool myOPresult = false;
            
            if (conn == null) conn.Open();
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }

            System.Data.SqlClient.SqlTransaction tr;
            tr = conn.BeginTransaction(IsolationLevel.Serializable);
            try
            {
                long myNewlastIDTestPlan = GetLastInsertData(MainForm.ConstTestPlanTables[1], tr);
                long myNewlastIDTestCtrl = GetLastInsertData(MainForm.ConstTestPlanTables[2], tr);
                long myNewlastIDTestModel = GetLastInsertData(MainForm.ConstTestPlanTables[3], tr);
                long myNewlastIDTestPrmtr = GetLastInsertData(MainForm.ConstTestPlanTables[4], tr);
                long myNewlastIDTestEquip = GetLastInsertData(MainForm.ConstTestPlanTables[5], tr);
                long myNewlastIDTestEquipPrmtr = GetLastInsertData(MainForm.ConstTestPlanTables[6], tr);
                long myNewlastIDMConfigInit = GetLastInsertData(MainForm.ConstTestPlanTables[7], tr);

                SqlCommand cm1 = new SqlCommand("select * from  " + MainForm.ConstTestPlanTables[1], conn);
                SqlDataAdapter da1 = new SqlDataAdapter(cm1);
                SqlCommandBuilder cb1 = new SqlCommandBuilder(da1);
                DataTable dt1 = new DataTable();
                da1.SelectCommand.Transaction = tr;
                //da1.Fill(dt1);

                SqlCommand cm2 = new SqlCommand("select * from  " + MainForm.ConstTestPlanTables[2], conn);
                SqlDataAdapter da2 = new SqlDataAdapter(cm2);
                SqlCommandBuilder cb2 = new SqlCommandBuilder(da2);
                DataTable dt2 = new DataTable();
                da2.SelectCommand.Transaction = tr;
                //da2.Fill(dt2);

                SqlCommand cm3 = new SqlCommand("select * from  " + MainForm.ConstTestPlanTables[3], conn);
                SqlDataAdapter da3 = new SqlDataAdapter(cm3);
                SqlCommandBuilder cb3 = new SqlCommandBuilder(da3);
                DataTable dt3 = new DataTable();
                da3.SelectCommand.Transaction = tr;
                //da3.Fill(dt3);

                SqlCommand cm4 = new SqlCommand("select * from  " + MainForm.ConstTestPlanTables[4], conn);
                SqlDataAdapter da4 = new SqlDataAdapter(cm4);
                SqlCommandBuilder cb4 = new SqlCommandBuilder(da4);
                DataTable dt4 = new DataTable();
                da4.SelectCommand.Transaction = tr;
                //da4.Fill(dt4);

                SqlCommand cm5 = new SqlCommand("select * from  " + MainForm.ConstTestPlanTables[5], conn);
                SqlDataAdapter da5 = new SqlDataAdapter(cm5);
                SqlCommandBuilder cb5 = new SqlCommandBuilder(da5);
                DataTable dt5 = new DataTable();
                da5.SelectCommand.Transaction = tr;
                //da5.Fill(dt5);

                SqlCommand cm6 = new SqlCommand("select * from  " + MainForm.ConstTestPlanTables[6], conn);
                SqlDataAdapter da6 = new SqlDataAdapter(cm6);
                SqlCommandBuilder cb6 = new SqlCommandBuilder(da6);
                DataTable dt6 = new DataTable();
                da6.SelectCommand.Transaction = tr;
                //da6.Fill(dt6);

                SqlCommand cm7 = new SqlCommand("select * from  " + MainForm.ConstTestPlanTables[7], conn);
                SqlDataAdapter da7 = new SqlDataAdapter(cm7);
                SqlCommandBuilder cb7 = new SqlCommandBuilder(da7);
                DataTable dt7 = new DataTable();
                da7.SelectCommand.Transaction = tr;
                //da7.Fill(dt7);

                //MainForm.GlobalTotalDS.Tables["TopoPNSpecsParams"]  //只会有编辑的资料
                SqlCommand cm8 = new SqlCommand("select * from  TopoPNSpecsParams", conn);
                SqlDataAdapter da8 = new SqlDataAdapter(cm8);
                SqlCommandBuilder cb8 = new SqlCommandBuilder(da8);
                DataTable dt8 = new DataTable();
                da8.SelectCommand.Transaction = tr;
                #region ReCheck ID
                for (int i = 0; i < MainForm.TopoToatlDS.Tables[1].Rows.Count; i++)
                {
                    if ((MainForm.TopoToatlDS.Tables[1].Rows[i].RowState == DataRowState.Added)
                        && (Convert.ToInt64(MainForm.TopoToatlDS.Tables[1].Rows[i]["ID"]) > MainForm.origIDTestPlan)
                       )
                    {
                        MainForm.TopoToatlDS.Tables[1].Rows[i]["ID"] = myNewlastIDTestPlan + 1;
                        myNewlastIDTestPlan++;
                    }
                }

                for (int i = 0; i < MainForm.TopoToatlDS.Tables[2].Rows.Count; i++)
                {
                    if ((MainForm.TopoToatlDS.Tables[2].Rows[i].RowState == DataRowState.Added)
                        && (Convert.ToInt64(MainForm.TopoToatlDS.Tables[2].Rows[i]["ID"]) > MainForm.origIDTestCtrl))
                    {
                        MainForm.TopoToatlDS.Tables[2].Rows[i]["ID"] = myNewlastIDTestCtrl + 1;
                        myNewlastIDTestCtrl++;
                    }
                }

                for (int i = 0; i < MainForm.TopoToatlDS.Tables[3].Rows.Count; i++)
                {
                    if ((MainForm.TopoToatlDS.Tables[3].Rows[i].RowState == DataRowState.Added)
                        && (Convert.ToInt64(MainForm.TopoToatlDS.Tables[3].Rows[i]["ID"]) > MainForm.origIDTestModel)
                       )
                    {
                        MainForm.TopoToatlDS.Tables[3].Rows[i]["ID"] = myNewlastIDTestModel + 1;
                        myNewlastIDTestModel++;
                    }
                }

                for (int i = 0; i < MainForm.TopoToatlDS.Tables[4].Rows.Count; i++)
                {
                    if ((MainForm.TopoToatlDS.Tables[4].Rows[i].RowState == DataRowState.Added)
                        && (Convert.ToInt64(MainForm.TopoToatlDS.Tables[4].Rows[i]["ID"]) > MainForm.origIDTestPrmtr)
                       )
                    {
                        MainForm.TopoToatlDS.Tables[4].Rows[i]["ID"] = myNewlastIDTestPrmtr + 1;
                        myNewlastIDTestPrmtr++;
                    }
                }


                //141021_1 Modify...
                for (int i = 0; i < MainForm.TopoToatlDS.Tables[5].Rows.Count; i++)
                {
                    if ((MainForm.TopoToatlDS.Tables[5].Rows[i].RowState == DataRowState.Added)
                        && (Convert.ToInt64(MainForm.TopoToatlDS.Tables[5].Rows[i]["ID"]) > MainForm.origIDTestEquip)
                       )
                    {
                        //还需要判定是否在同一个TestPlan下...

                        //Check Equip Info
                        string myDTstring = "_" + MainForm.TopoToatlDS.Tables[5].Rows[i]["ID"] + "_" + MainForm.TopoToatlDS.Tables[5].Rows[i]["ItemType"];
                        MainForm.TopoToatlDS.Tables[5].Rows[i]["ID"] = myNewlastIDTestEquip + 1;
                        myNewlastIDTestEquip++;

                        #region 设备名称变更: //141028_2 目前无需再次确认
                        //int myNewID = Convert.ToInt32(MainForm.TopoToatlDS.Tables[5].Rows[i]["ID"]);
                        //string myItemType = MainForm.TopoToatlDS.Tables[5].Rows[i]["ItemType"].ToString();
                        //string myItemName = MainForm.TopoToatlDS.Tables[5].Rows[i]["ItemName"].ToString();
                        //string tempEquipName = myItemName.Replace(myDTstring, "_" + myNewID + "_" + myItemType);

                       
                        //if (myItemName != tempEquipName) //141028_2 目前无需再次
                        //{
                        //    string myEquipTestPlanID = MainForm.TopoToatlDS.Tables[5].Rows[i]["PID"].ToString();

                        //    //141028_2 目前无需再次修改该设备名称    //MainForm.TopoToatlDS.Tables[5].Rows[i]["ItemName"] = tempEquipName;

                        //    //Check TopoTestModel Info
                        //    DataRow[] drs = MainForm.TopoToatlDS.Tables[MainForm.ConstTestPlanTables[3]].Select("EquipmentList like '%" + myDTstring + "%'");
                        //    for (int k = 0; k < drs.Length; k++)
                        //    {
                        //        int myModelID = Convert.ToInt32(drs[k]["ID"]);
                        //        string myModelTestPlanID = MainForm.getDTColumnInfo(MainForm.TopoToatlDS.Tables[MainForm.ConstTestPlanTables[2]], "PID", ("ID = " + drs[k]["PID"].ToString()));
                        //        if (myModelTestPlanID == myEquipTestPlanID)
                        //        {
                        //            string myOrigEquipmentList = drs[k]["EquipmentList"].ToString();
                        //            string tempSS = drs[k]["EquipmentList"].ToString().Replace(myItemName, tempEquipName);
                        //            drs[k]["EquipmentList"] = tempSS;
                        //        }
                        //    }
                        //}
                        #endregion
                    }
                    #region 设备名称变更: //141028_2 目前无需再次确认
                    ////141028_2 目前无需再次修改该设备名称
                    //else if (MainForm.TopoToatlDS.Tables[5].Rows[i].RowState != DataRowState.Deleted)
                    //{
                    //    //Check Equip Info

                    //    int myNewID = Convert.ToInt32(MainForm.TopoToatlDS.Tables[5].Rows[i]["ID"]);
                    //    string myItemType = MainForm.TopoToatlDS.Tables[5].Rows[i]["ItemType"].ToString();
                    //    string myItemName = MainForm.TopoToatlDS.Tables[5].Rows[i]["ItemName"].ToString();

                    //    int findChar = myItemName.IndexOf('_');
                    //    string myDTstring = myItemName.Substring(findChar);
                    //    string tempEquipName = myItemName.Replace(myDTstring, "_" + myNewID + "_" + myItemType);

                    //    //TopoTestModel //141028_2 目前无需再次确认
                    //    //if (myItemName != tempEquipName)
                    //    //{
                    //    //    //141028_2 目前无需再次修改该设备名称    //MainForm.TopoToatlDS.Tables[5].Rows[i]["ItemName"] = tempEquipName;

                    //    //    string myEquipTestPlanID = MainForm.TopoToatlDS.Tables[5].Rows[i]["PID"].ToString();
                    //    //    //Check TopoTestModel Info
                    //    //    DataRow[] drs = MainForm.TopoToatlDS.Tables[MainForm.ConstTestPlanTables[3]].Select("EquipmentList like '%" + myDTstring + "%'");
                    //    //    for (int k = 0; k < drs.Length; k++)
                    //    //    {
                    //    //        string myModelTestPlanID = MainForm.getDTColumnInfo(MainForm.TopoToatlDS.Tables[MainForm.ConstTestPlanTables[2]], "PID", ("ID = " + drs[k]["PID"].ToString()));
                    //    //        if (myModelTestPlanID == myEquipTestPlanID)
                    //    //        {
                    //    //            int myModelID = Convert.ToInt32(drs[k]["ID"]);
                    //    //            string myOrigEquipmentList = drs[k]["EquipmentList"].ToString();
                    //    //            string tempSS = drs[k]["EquipmentList"].ToString().Replace(myItemName, tempEquipName);
                    //    //            if (myOrigEquipmentList != tempSS)
                    //    //            {
                    //    //                drs[k]["EquipmentList"] = tempSS;
                    //    //            }
                    //    //        }
                    //    //    }
                    //    //}
                    //}
                    #endregion
                }

                for (int i = 0; i < MainForm.TopoToatlDS.Tables[6].Rows.Count; i++)
                {
                    if ((MainForm.TopoToatlDS.Tables[6].Rows[i].RowState == DataRowState.Added)
                        && (Convert.ToInt64(MainForm.TopoToatlDS.Tables[6].Rows[i]["ID"]) > MainForm.origIDTestEquipPrmtr)
                       )
                    {
                        MainForm.TopoToatlDS.Tables[6].Rows[i]["ID"] = myNewlastIDTestEquipPrmtr + 1;
                        myNewlastIDTestEquipPrmtr++;
                    }
                }

                for (int i = 0; i < MainForm.TopoToatlDS.Tables[7].Rows.Count; i++)
                {
                    if ((MainForm.TopoToatlDS.Tables[7].Rows[i].RowState == DataRowState.Added)
                        && (Convert.ToInt64(MainForm.TopoToatlDS.Tables[7].Rows[i]["ID"]) > MainForm.origIDMConfigInit)
                       )
                    {
                        MainForm.TopoToatlDS.Tables[7].Rows[i]["ID"] = myNewlastIDMConfigInit + 1;
                        myNewlastIDMConfigInit++;
                    }
                }
                #endregion

                dt1 = MainForm.TopoToatlDS.Tables[1];
                dt2 = MainForm.TopoToatlDS.Tables[2];
                dt3 = MainForm.TopoToatlDS.Tables[3];
                dt4 = MainForm.TopoToatlDS.Tables[4];
                dt5 = MainForm.TopoToatlDS.Tables[5];
                dt6 = MainForm.TopoToatlDS.Tables[6];
                dt7 = MainForm.TopoToatlDS.Tables[7];
                dt8 = MainForm.GlobalTotalDS.Tables["TopoPNSpecsParams"];   //只会有编辑的资料

                DataTable dtAdded1 = new DataTable(); //存放主表 新增 的数据
                DataTable dtMidified1 = new DataTable(); //存储主表 编辑 的行记录
                DataTable dtDeleted1 = new DataTable(); //存储主表 删除 的行记录

                dtDeleted1 = dt1.GetChanges(DataRowState.Deleted);
                dtMidified1 = dt1.GetChanges(DataRowState.Modified);
                dtAdded1 = dt1.GetChanges(DataRowState.Added);

                DataTable dtAdded2 = new DataTable(); //存放主表 新增 的数据
                DataTable dtMidified2 = new DataTable(); //存储主表 编辑 的行记录
                DataTable dtDeleted2 = new DataTable(); //存储主表 删除 的行记录

                dtDeleted2 = dt2.GetChanges(DataRowState.Deleted);
                dtMidified2 = dt2.GetChanges(DataRowState.Modified);
                dtAdded2 = dt2.GetChanges(DataRowState.Added);

                DataTable dtAdded3 = new DataTable(); //存放主表 新增 的数据
                DataTable dtMidified3 = new DataTable(); //存储主表 编辑 的行记录
                DataTable dtDeleted3 = new DataTable(); //存储主表 删除 的行记录

                dtDeleted3 = dt3.GetChanges(DataRowState.Deleted);
                dtMidified3 = dt3.GetChanges(DataRowState.Modified);
                dtAdded3 = dt3.GetChanges(DataRowState.Added);

                DataTable dtAdded4 = new DataTable(); //存放主表 新增 的数据
                DataTable dtMidified4 = new DataTable(); //存储主表 编辑 的行记录
                DataTable dtDeleted4 = new DataTable(); //存储主表 删除 的行记录

                dtDeleted4 = dt4.GetChanges(DataRowState.Deleted);
                dtMidified4 = dt4.GetChanges(DataRowState.Modified);
                dtAdded4 = dt4.GetChanges(DataRowState.Added);

                DataTable dtAdded5 = new DataTable(); //存放主表 新增 的数据
                DataTable dtMidified5 = new DataTable(); //存储主表 编辑 的行记录
                DataTable dtDeleted5 = new DataTable(); //存储主表 删除 的行记录

                dtDeleted5 = dt5.GetChanges(DataRowState.Deleted);
                dtMidified5 = dt5.GetChanges(DataRowState.Modified);
                dtAdded5 = dt5.GetChanges(DataRowState.Added);

                DataTable dtAdded6 = new DataTable(); //存放主表 新增 的数据
                DataTable dtMidified6 = new DataTable(); //存储主表 编辑 的行记录
                DataTable dtDeleted6 = new DataTable(); //存储主表 删除 的行记录

                dtDeleted6 = dt6.GetChanges(DataRowState.Deleted);
                dtMidified6 = dt6.GetChanges(DataRowState.Modified);
                dtAdded6 = dt6.GetChanges(DataRowState.Added);

                DataTable dtAdded7 = new DataTable(); //存放主表 新增 的数据
                DataTable dtMidified7 = new DataTable(); //存储主表 编辑 的行记录
                DataTable dtDeleted7 = new DataTable(); //存储主表 删除 的行记录

                dtDeleted7 = dt7.GetChanges(DataRowState.Deleted);
                dtMidified7 = dt7.GetChanges(DataRowState.Modified);
                dtAdded7 = dt7.GetChanges(DataRowState.Added);

                DataTable dtAdded8 = new DataTable(); //存放主表 新增 的数据
                DataTable dtMidified8 = new DataTable(); //存储主表 编辑 的行记录
                DataTable dtDeleted8 = new DataTable(); //存储主表 删除 的行记录

                dtDeleted8 = dt8.GetChanges(DataRowState.Deleted);
                dtMidified8 = dt8.GetChanges(DataRowState.Modified);
                dtAdded8 = dt8.GetChanges(DataRowState.Added);

                if (dtDeleted8 != null) da8.Update(dtDeleted8);
                if (dtDeleted7 != null) da7.Update(dtDeleted7);
                if (dtDeleted6 != null) da6.Update(dtDeleted6);
                if (dtDeleted5 != null) da5.Update(dtDeleted5);
                if (dtDeleted4 != null) da4.Update(dtDeleted4);
                if (dtDeleted3 != null) da3.Update(dtDeleted3);
                if (dtDeleted2 != null) da2.Update(dtDeleted2);
                if (dtDeleted1 != null) da1.Update(dtDeleted1);


                if (dtAdded1 != null) da1.Update(dtAdded1);
                if (dtMidified1 != null) da1.Update(dtMidified1);

                if (dtAdded2 != null) da2.Update(dtAdded2);
                if (dtMidified2 != null) da2.Update(dtMidified2);

                if (dtAdded3 != null) da3.Update(dtAdded3);
                if (dtMidified3 != null) da3.Update(dtMidified3);


                if (dtAdded4 != null) da4.Update(dtAdded4);
                if (dtMidified4 != null) da4.Update(dtMidified4);

                if (dtAdded5 != null) da5.Update(dtAdded5);
                if (dtMidified5 != null) da5.Update(dtMidified5);

                if (dtAdded6 != null) da6.Update(dtAdded6);
                if (dtMidified6 != null) da6.Update(dtMidified6);

                if (dtAdded7 != null) da7.Update(dtAdded7);
                if (dtMidified7 != null) da7.Update(dtMidified7);

                if (dtAdded8 != null) da8.Update(dtAdded8);
                if (dtMidified8 != null) da8.Update(dtMidified8);

                tr.Commit();
                myOPresult = true;
                //MainForm.TopoToatlDSAcceptChanges();
                return myOPresult;
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
                }
                catch (System.Exception TransactionEx)
                {
                    //Handle Exception
                    MessageBox.Show(TransactionEx.ToString());
                    if (conn.State.ToString().ToUpper() == "OPEN") conn.Close();
                }
                return myOPresult;
            }
            finally
            {
                if (conn.State.ToString().ToUpper() == "OPEN")
                    conn.Close();                
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
                if (mydt != null)
                {
                    da.Update(mydt);
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
        public AccessManager(string AccessFilePath): base(AccessFilePath)
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
            bool myOPresult = false;

            if (conn == null) conn.Open();

            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            OleDbTransaction tr;
            tr = conn.BeginTransaction(IsolationLevel.ReadCommitted);

            try
            {
                //141021_1
                long myNewlastIDTestPlan = GetLastInsertData(MainForm.ConstTestPlanTables[1],tr);
                long myNewlastIDTestCtrl = GetLastInsertData(MainForm.ConstTestPlanTables[2], tr);
                long myNewlastIDTestModel = GetLastInsertData(MainForm.ConstTestPlanTables[3], tr);
                long myNewlastIDTestPrmtr = GetLastInsertData(MainForm.ConstTestPlanTables[4], tr);
                long myNewlastIDTestEquip = GetLastInsertData(MainForm.ConstTestPlanTables[5], tr);
                long myNewlastIDTestEquipPrmtr = GetLastInsertData(MainForm.ConstTestPlanTables[6], tr);
                long myNewlastIDMConfigInit = GetLastInsertData(MainForm.ConstTestPlanTables[7], tr);

                OleDbCommand cm1 = new OleDbCommand("select * from  " + MainForm.ConstTestPlanTables[1], conn);
                OleDbDataAdapter da1 = new OleDbDataAdapter(cm1);
                OleDbCommandBuilder cb1 = new OleDbCommandBuilder(da1);
                DataTable dt1 = new DataTable();
                da1.SelectCommand.Transaction = tr; //140612_1

                OleDbCommand cm2 = new OleDbCommand("select * from  " + MainForm.ConstTestPlanTables[2], conn);
                OleDbDataAdapter da2 = new OleDbDataAdapter(cm2);
                OleDbCommandBuilder cb2 = new OleDbCommandBuilder(da2);
                DataTable dt2 = new DataTable();
                da2.SelectCommand.Transaction = tr; //140612_1

                OleDbCommand cm3 = new OleDbCommand("select * from  " + MainForm.ConstTestPlanTables[3], conn);
                OleDbDataAdapter da3 = new OleDbDataAdapter(cm3);
                OleDbCommandBuilder cb3 = new OleDbCommandBuilder(da3);
                DataTable dt3 = new DataTable();
                da3.SelectCommand.Transaction = tr; //140612_1

                OleDbCommand cm4 = new OleDbCommand("select * from  " + MainForm.ConstTestPlanTables[4], conn);
                OleDbDataAdapter da4 = new OleDbDataAdapter(cm4);
                OleDbCommandBuilder cb4 = new OleDbCommandBuilder(da4);
                DataTable dt4 = new DataTable();
                da4.SelectCommand.Transaction = tr; //140612_1

                OleDbCommand cm5 = new OleDbCommand("select * from  " + MainForm.ConstTestPlanTables[5], conn);
                OleDbDataAdapter da5 = new OleDbDataAdapter(cm5);
                OleDbCommandBuilder cb5 = new OleDbCommandBuilder(da5);
                DataTable dt5 = new DataTable();
                da5.SelectCommand.Transaction = tr; //140612_1

                OleDbCommand cm6 = new OleDbCommand("select * from  " + MainForm.ConstTestPlanTables[6], conn);
                OleDbDataAdapter da6 = new OleDbDataAdapter(cm6);
                OleDbCommandBuilder cb6 = new OleDbCommandBuilder(da6);
                DataTable dt6 = new DataTable();
                da6.SelectCommand.Transaction = tr; //140612_1

                OleDbCommand cm7 = new OleDbCommand("select * from  " + MainForm.ConstTestPlanTables[7], conn);
                OleDbDataAdapter da7 = new OleDbDataAdapter(cm7);
                OleDbCommandBuilder cb7 = new OleDbCommandBuilder(da7);
                DataTable dt7 = new DataTable();
                da7.SelectCommand.Transaction = tr; //150203_1

                //MainForm.GlobalTotalDS.Tables["TopoPNSpecsParams"]    //只会有编辑的资料
                OleDbCommand cm8 = new OleDbCommand("select * from  TopoPNSpecsParams", conn);
                OleDbDataAdapter da8 = new OleDbDataAdapter(cm8);
                OleDbCommandBuilder cb8 = new OleDbCommandBuilder(da8);
                DataTable dt8 = new DataTable();
                da8.SelectCommand.Transaction = tr;
                #region ReCheck ID
                for (int i = 0; i < MainForm.TopoToatlDS.Tables[1].Rows.Count; i++)
                {
                    if ((MainForm.TopoToatlDS.Tables[1].Rows[i].RowState == DataRowState.Added)
                        && (Convert.ToInt64(MainForm.TopoToatlDS.Tables[1].Rows[i]["ID"]) > MainForm.origIDTestPlan)
                       )
                    {
                        MainForm.TopoToatlDS.Tables[1].Rows[i]["ID"] = myNewlastIDTestPlan + 1;
                        myNewlastIDTestPlan++;
                    }
                }

                for (int i = 0; i < MainForm.TopoToatlDS.Tables[2].Rows.Count; i++)
                {
                    if ((MainForm.TopoToatlDS.Tables[2].Rows[i].RowState == DataRowState.Added)
                        && (Convert.ToInt64(MainForm.TopoToatlDS.Tables[2].Rows[i]["ID"]) > MainForm.origIDTestCtrl))
                    {
                        MainForm.TopoToatlDS.Tables[2].Rows[i]["ID"] = myNewlastIDTestCtrl + 1;
                        myNewlastIDTestCtrl++;
                    }
                }

                for (int i = 0; i < MainForm.TopoToatlDS.Tables[3].Rows.Count; i++)
                {
                    if ((MainForm.TopoToatlDS.Tables[3].Rows[i].RowState == DataRowState.Added)
                        && (Convert.ToInt64(MainForm.TopoToatlDS.Tables[3].Rows[i]["ID"]) > MainForm.origIDTestModel)
                       )
                    {
                        MainForm.TopoToatlDS.Tables[3].Rows[i]["ID"] = myNewlastIDTestModel + 1;
                        myNewlastIDTestModel++;
                    }
                }

                for (int i = 0; i < MainForm.TopoToatlDS.Tables[4].Rows.Count; i++)
                {
                    if ((MainForm.TopoToatlDS.Tables[4].Rows[i].RowState == DataRowState.Added)
                        && (Convert.ToInt64(MainForm.TopoToatlDS.Tables[4].Rows[i]["ID"]) > MainForm.origIDTestPrmtr)
                       )
                    {
                        MainForm.TopoToatlDS.Tables[4].Rows[i]["ID"] = myNewlastIDTestPrmtr + 1;
                        myNewlastIDTestPrmtr++;
                    }
                }


                //141021_1 Modify...
                for (int i = 0; i < MainForm.TopoToatlDS.Tables[5].Rows.Count; i++)
                {
                    if ((MainForm.TopoToatlDS.Tables[5].Rows[i].RowState == DataRowState.Added)
                        && (Convert.ToInt64(MainForm.TopoToatlDS.Tables[5].Rows[i]["ID"]) > MainForm.origIDTestEquip)
                       )
                    {
                        //还需要判定是否在同一个TestPlan下...

                        //Check Equip Info
                        string myDTstring = "_" + MainForm.TopoToatlDS.Tables[5].Rows[i]["ID"] + "_" + MainForm.TopoToatlDS.Tables[5].Rows[i]["ItemType"];
                        MainForm.TopoToatlDS.Tables[5].Rows[i]["ID"] = myNewlastIDTestEquip + 1;
                        myNewlastIDTestEquip++;

                        #region 设备名称变更: //141028_2 目前无需再次确认
                        //int myNewID = Convert.ToInt32(MainForm.TopoToatlDS.Tables[5].Rows[i]["ID"]);
                        //string myItemType = MainForm.TopoToatlDS.Tables[5].Rows[i]["ItemType"].ToString();
                        //string myItemName = MainForm.TopoToatlDS.Tables[5].Rows[i]["ItemName"].ToString();
                        //string tempEquipName = myItemName.Replace(myDTstring, "_" + myNewID + "_" + myItemType);


                        //if (myItemName != tempEquipName) //141028_2 目前无需再次
                        //{
                        //    string myEquipTestPlanID = MainForm.TopoToatlDS.Tables[5].Rows[i]["PID"].ToString();

                        //    //141028_2 目前无需再次修改该设备名称    //MainForm.TopoToatlDS.Tables[5].Rows[i]["ItemName"] = tempEquipName;

                        //    //Check TopoTestModel Info
                        //    DataRow[] drs = MainForm.TopoToatlDS.Tables[MainForm.ConstTestPlanTables[3]].Select("EquipmentList like '%" + myDTstring + "%'");
                        //    for (int k = 0; k < drs.Length; k++)
                        //    {
                        //        int myModelID = Convert.ToInt32(drs[k]["ID"]);
                        //        string myModelTestPlanID = MainForm.getDTColumnInfo(MainForm.TopoToatlDS.Tables[MainForm.ConstTestPlanTables[2]], "PID", ("ID = " + drs[k]["PID"].ToString()));
                        //        if (myModelTestPlanID == myEquipTestPlanID)
                        //        {
                        //            string myOrigEquipmentList = drs[k]["EquipmentList"].ToString();
                        //            string tempSS = drs[k]["EquipmentList"].ToString().Replace(myItemName, tempEquipName);
                        //            drs[k]["EquipmentList"] = tempSS;
                        //        }
                        //    }
                        //}
                        #endregion
                    }
                    #region 设备名称变更: //141028_2 目前无需再次确认
                    ////141028_2 目前无需再次修改该设备名称
                    //else if (MainForm.TopoToatlDS.Tables[5].Rows[i].RowState != DataRowState.Deleted)
                    //{
                    //    //Check Equip Info

                    //    int myNewID = Convert.ToInt32(MainForm.TopoToatlDS.Tables[5].Rows[i]["ID"]);
                    //    string myItemType = MainForm.TopoToatlDS.Tables[5].Rows[i]["ItemType"].ToString();
                    //    string myItemName = MainForm.TopoToatlDS.Tables[5].Rows[i]["ItemName"].ToString();

                    //    int findChar = myItemName.IndexOf('_');
                    //    string myDTstring = myItemName.Substring(findChar);
                    //    string tempEquipName = myItemName.Replace(myDTstring, "_" + myNewID + "_" + myItemType);

                    //    //TopoTestModel //141028_2 目前无需再次确认
                    //    //if (myItemName != tempEquipName)
                    //    //{
                    //    //    //141028_2 目前无需再次修改该设备名称    //MainForm.TopoToatlDS.Tables[5].Rows[i]["ItemName"] = tempEquipName;

                    //    //    string myEquipTestPlanID = MainForm.TopoToatlDS.Tables[5].Rows[i]["PID"].ToString();
                    //    //    //Check TopoTestModel Info
                    //    //    DataRow[] drs = MainForm.TopoToatlDS.Tables[MainForm.ConstTestPlanTables[3]].Select("EquipmentList like '%" + myDTstring + "%'");
                    //    //    for (int k = 0; k < drs.Length; k++)
                    //    //    {
                    //    //        string myModelTestPlanID = MainForm.getDTColumnInfo(MainForm.TopoToatlDS.Tables[MainForm.ConstTestPlanTables[2]], "PID", ("ID = " + drs[k]["PID"].ToString()));
                    //    //        if (myModelTestPlanID == myEquipTestPlanID)
                    //    //        {
                    //    //            int myModelID = Convert.ToInt32(drs[k]["ID"]);
                    //    //            string myOrigEquipmentList = drs[k]["EquipmentList"].ToString();
                    //    //            string tempSS = drs[k]["EquipmentList"].ToString().Replace(myItemName, tempEquipName);
                    //    //            if (myOrigEquipmentList != tempSS)
                    //    //            {
                    //    //                drs[k]["EquipmentList"] = tempSS;
                    //    //            }
                    //    //        }
                    //    //    }
                    //    //}
                    //}
                    #endregion
                }

                for (int i = 0; i < MainForm.TopoToatlDS.Tables[6].Rows.Count; i++)
                {
                    if ((MainForm.TopoToatlDS.Tables[6].Rows[i].RowState == DataRowState.Added)
                        && (Convert.ToInt64(MainForm.TopoToatlDS.Tables[6].Rows[i]["ID"]) > MainForm.origIDTestEquipPrmtr)
                       )
                    {
                        MainForm.TopoToatlDS.Tables[6].Rows[i]["ID"] = myNewlastIDTestEquipPrmtr + 1;
                        myNewlastIDTestEquipPrmtr++;
                    }
                }

                for (int i = 0; i < MainForm.TopoToatlDS.Tables[7].Rows.Count; i++)
                {
                    if ((MainForm.TopoToatlDS.Tables[7].Rows[i].RowState == DataRowState.Added)
                        && (Convert.ToInt64(MainForm.TopoToatlDS.Tables[7].Rows[i]["ID"]) > MainForm.origIDMConfigInit)
                       )
                    {
                        MainForm.TopoToatlDS.Tables[7].Rows[i]["ID"] = myNewlastIDMConfigInit + 1;
                        myNewlastIDMConfigInit++;
                    }
                }
                #endregion

                dt1 = MainForm.TopoToatlDS.Tables[1];
                dt2 = MainForm.TopoToatlDS.Tables[2];
                dt3 = MainForm.TopoToatlDS.Tables[3];
                dt4 = MainForm.TopoToatlDS.Tables[4];
                dt5 = MainForm.TopoToatlDS.Tables[5];
                dt6 = MainForm.TopoToatlDS.Tables[6];
                dt7 = MainForm.TopoToatlDS.Tables[7];
                dt8 = MainForm.GlobalTotalDS.Tables["TopoPNSpecsParams"];   //只会有编辑的资料

                DataTable dtAdded1 = new DataTable(); //存放主表 新增 的数据
                DataTable dtMidified1 = new DataTable(); //存储主表 编辑 的行记录
                DataTable dtDeleted1 = new DataTable(); //存储主表 删除 的行记录

                dtDeleted1 = dt1.GetChanges(DataRowState.Deleted);
                dtMidified1 = dt1.GetChanges(DataRowState.Modified);
                dtAdded1 = dt1.GetChanges(DataRowState.Added);

                DataTable dtAdded2 = new DataTable(); //存放主表 新增 的数据
                DataTable dtMidified2 = new DataTable(); //存储主表 编辑 的行记录
                DataTable dtDeleted2 = new DataTable(); //存储主表 删除 的行记录

                dtDeleted2 = dt2.GetChanges(DataRowState.Deleted);
                dtMidified2 = dt2.GetChanges(DataRowState.Modified);
                dtAdded2 = dt2.GetChanges(DataRowState.Added);

                DataTable dtAdded3 = new DataTable(); //存放主表 新增 的数据
                DataTable dtMidified3 = new DataTable(); //存储主表 编辑 的行记录
                DataTable dtDeleted3 = new DataTable(); //存储主表 删除 的行记录

                dtDeleted3 = dt3.GetChanges(DataRowState.Deleted);
                dtMidified3 = dt3.GetChanges(DataRowState.Modified);
                dtAdded3 = dt3.GetChanges(DataRowState.Added);

                DataTable dtAdded4 = new DataTable(); //存放主表 新增 的数据
                DataTable dtMidified4 = new DataTable(); //存储主表 编辑 的行记录
                DataTable dtDeleted4 = new DataTable(); //存储主表 删除 的行记录

                dtDeleted4 = dt4.GetChanges(DataRowState.Deleted);
                dtMidified4 = dt4.GetChanges(DataRowState.Modified);
                dtAdded4 = dt4.GetChanges(DataRowState.Added);

                DataTable dtAdded5 = new DataTable(); //存放主表 新增 的数据
                DataTable dtMidified5 = new DataTable(); //存储主表 编辑 的行记录
                DataTable dtDeleted5 = new DataTable(); //存储主表 删除 的行记录

                dtDeleted5 = dt5.GetChanges(DataRowState.Deleted);
                dtMidified5 = dt5.GetChanges(DataRowState.Modified);
                dtAdded5 = dt5.GetChanges(DataRowState.Added);

                DataTable dtAdded6 = new DataTable(); //存放主表 新增 的数据
                DataTable dtMidified6 = new DataTable(); //存储主表 编辑 的行记录
                DataTable dtDeleted6 = new DataTable(); //存储主表 删除 的行记录

                dtDeleted6 = dt6.GetChanges(DataRowState.Deleted);
                dtMidified6 = dt6.GetChanges(DataRowState.Modified);
                dtAdded6 = dt6.GetChanges(DataRowState.Added);

                DataTable dtAdded7 = new DataTable(); //存放主表 新增 的数据
                DataTable dtMidified7 = new DataTable(); //存储主表 编辑 的行记录
                DataTable dtDeleted7 = new DataTable(); //存储主表 删除 的行记录

                dtDeleted7 = dt7.GetChanges(DataRowState.Deleted);
                dtMidified7 = dt7.GetChanges(DataRowState.Modified);
                dtAdded7 = dt7.GetChanges(DataRowState.Added);

                DataTable dtAdded8 = new DataTable(); //存放主表 新增 的数据
                DataTable dtMidified8 = new DataTable(); //存储主表 编辑 的行记录
                DataTable dtDeleted8 = new DataTable(); //存储主表 删除 的行记录

                dtDeleted8 = dt8.GetChanges(DataRowState.Deleted);
                dtMidified8 = dt8.GetChanges(DataRowState.Modified);
                dtAdded8 = dt8.GetChanges(DataRowState.Added);

                if (dtDeleted8 != null) da8.Update(dtDeleted8);
                if (dtDeleted7 != null) da7.Update(dtDeleted7);
                if (dtDeleted6 != null) da6.Update(dtDeleted6);
                if (dtDeleted5 != null) da5.Update(dtDeleted5);
                if (dtDeleted4 != null) da4.Update(dtDeleted4);
                if (dtDeleted3 != null) da3.Update(dtDeleted3);
                if (dtDeleted2 != null) da2.Update(dtDeleted2);
                if (dtDeleted1 != null) da1.Update(dtDeleted1);


                if (dtAdded1 != null) da1.Update(dtAdded1);
                if (dtMidified1 != null) da1.Update(dtMidified1);

                if (dtAdded2 != null) da2.Update(dtAdded2);
                if (dtMidified2 != null) da2.Update(dtMidified2);

                if (dtAdded3 != null) da3.Update(dtAdded3);
                if (dtMidified3 != null) da3.Update(dtMidified3);


                if (dtAdded4 != null) da4.Update(dtAdded4);
                if (dtMidified4 != null) da4.Update(dtMidified4);

                if (dtAdded5 != null) da5.Update(dtAdded5);
                if (dtMidified5 != null) da5.Update(dtMidified5);

                if (dtAdded6 != null) da6.Update(dtAdded6);
                if (dtMidified6 != null) da6.Update(dtMidified6);

                if (dtAdded7 != null) da7.Update(dtAdded7);
                if (dtMidified7 != null) da7.Update(dtMidified7);

                if (dtAdded8 != null) da8.Update(dtAdded8);
                if (dtMidified8 != null) da8.Update(dtMidified8);


                tr.Commit();

                myOPresult = true;
                //MainForm.TopoToatlDSAcceptChanges();
                return myOPresult;
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
                    return myOPresult;
                }
                catch (System.Exception TransactionEx)
                {
                    //Handle Exception
                    MessageBox.Show(TransactionEx.ToString());
                    if (conn.State == ConnectionState.Open) conn.Close();
                    return myOPresult;
                }
            }
            finally
            {
                if (conn.State.ToString().ToUpper() == "OPEN")
                    conn.Close();
            }
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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Xml.Linq;
using System.Windows.Forms;
using System.Threading;
using ATS_Framework;
using System.IO;
using ATSDataBase;

namespace ATS
{
    #region Sql Database
    public class SqlDatabase : ServerDatabaseIO

    {
       // private SqlConnection conn = null;

        public string strConnection = "";//= "Provider = Microsoft.ACE.OLEDB.12.0;" + @"Data Source=" + Application.StartupPath + "\\SQL.accdb";

        //  private DataSet ds;

        private SqlDataAdapter da;
        private SqlCommandBuilder cb;
        private DataTable DT;
      
        public SqlDatabase(string serverName) :base(serverName)
        {
           
        }
        public SqlDatabase(string serverName, string dbName, string user, string pwd)
            : base(serverName, dbName, user, pwd)
        {

        }
         
        public override bool OpenDatabase(bool Swith)
        {

            bool result = false;
            try
            {
                if (Swith)
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
                return result;
            }

        }

        override public DataTable GetDataTable(string StrSelectconditions, string StrTabelName)
        {
            DataTable dt = new DataTable();

            if (OpenDatabase(true))
            {
                SqlDataAdapter da = new SqlDataAdapter(StrSelectconditions, conn);

                SqlCommandBuilder cb = new SqlCommandBuilder(da);

                //  da.UpdateCommand = cb.GetUpdateCommand();

                DataSet ds = new DataSet(StrTabelName);

                da.Fill(ds, StrTabelName);

                dt = ds.Tables[StrTabelName];
            }
            OpenDatabase(false);
            return dt;
        }// 获得当前的Datatable

        public override bool WriterLog(int ConditionID, int SNID, string StrInf, string StartTime, string EndTime, float Temp, float Voltage, byte Channel, bool Resultlflag, out int logid)
        {
            logid = -1;
            try
            {
                //SQL Server连接路径的部分根据实际需要再修改
                //System.Data.SqlClient.SqlConnection spConn = new System.Data.SqlClient.SqlConnection(strConnection);
                //spConn.Open();
                OpenDatabase(true);
                //执行存储过程 
                System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand("InsertLogRecord ", conn);
                //说明命令要执行的是存储过程     
                command.CommandType = CommandType.StoredProcedure;
                //向存储过程中传递参数    @ID int OUTPUT,@PID int, @RunRecordID int,  @StartTime datetime, @TestLog ntext, @Result bit

                command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ID", SqlDbType.Int));
                command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PID", SqlDbType.Int));
                command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RunRecordID", SqlDbType.Int));
                command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@StartTime", SqlDbType.NVarChar, 30));

                command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@EndTime", SqlDbType.NVarChar, 30));
                command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TestLog", SqlDbType.NText));
                command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Temp", SqlDbType.Real));
                command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Voltage", SqlDbType.Real));
                command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Channel", SqlDbType.TinyInt));
                command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Result", SqlDbType.Bit));

                command.UpdatedRowSource = UpdateRowSource.None;

                command.Parameters["@PID"].Value = ConditionID;   //当前的PID
                command.Parameters["@RunRecordID"].Value = SNID;
                command.Parameters["@StartTime"].Value = StartTime;
                command.Parameters["@EndTime"].Value = EndTime;
                command.Parameters["@TestLog"].Value = StrInf;
                command.Parameters["@Result"].Value = Resultlflag;
                command.Parameters["@Temp"].Value = Temp;
                command.Parameters["@Voltage"].Value = Voltage;
                command.Parameters["@Channel"].Value = Channel;
                command.Parameters["@ID"].Direction = ParameterDirection.Output;
                command.ExecuteNonQuery();
                logid = Convert.ToInt32(command.Parameters["@ID"].Value);
                OpenDatabase(false);
                return true;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
                return false;
            }
        }

        override public bool WriteResult(int StrInterfaceLogid, DataTable ResultDataTabel)
        {
            string strSelectconditions = "Select * from TopoTestData Where pid=" + StrInterfaceLogid + " order by ID";//有风险

            string StrTableName = "TopoTestData";
            // SqlDataAdapter
            string StrItemName=null;
            try
            {
                OpenDatabase(true);

                SqlDataAdapter da = new SqlDataAdapter(strSelectconditions, conn);

                SqlCommandBuilder cb = new SqlCommandBuilder(da);
                da.UpdateCommand = cb.GetUpdateCommand();
                DataSet ds = new DataSet(StrTableName);

                da.Fill(ds, StrTableName);


                int i = ds.Tables[StrTableName].Rows.Count;

                int J = ResultDataTabel.Rows.Count;
                foreach (DataRow dr in ResultDataTabel.Rows)
                {
                    if (dr["DataRecord"].ToString().Trim() == "1")
                    {
                         StrItemName=dr["ItemName"].ToString();
                        DataRow pdr = ds.Tables[StrTableName].NewRow();
                        pdr["PID"] = StrInterfaceLogid;
                        pdr["ItemName"] = dr["ItemName"].ToString();
                       
                        try//当测试结果的数据未非法的时候，需要对其进行处理，否则存档出错会影响到后面的数据存放
                        {
                            double DItemValue = Convert.ToDouble(dr["Value"]);
                            if (DItemValue > -32767 && DItemValue < 32767)
                            {
                                pdr["ItemValue"] = DItemValue;
                            }
                            else
                            {
                                pdr["ItemValue"] = 9E+10;
                            }


                        }
                        catch (System.Exception ex)
                        {
                            pdr["ItemValue"] = 9E+10;
                        }
                       // pdr["ItemValue"] = dr["Value"].ToString();
                        pdr["SpecMin"] = dr["SpecMin"];
                        pdr["SpecMax"] = dr["SpecMax"];
                      //  pdr["Result"] = dr["Result"];
                        if (dr["Result"].ToString().ToUpper()=="PASS")
                        {
                            pdr["Result"] = true;
                        }
                        else
                        {
                            pdr["Result"] = false;
                        }
                       // pdr["Result"] = "PASS";
                        ds.Tables[StrTableName].Rows.Add(pdr);
                        da.Update(ds, StrTableName);
                    }
                }

                return true;

            }
            catch
            {
                MessageBox.Show(StrItemName + " Save Data Error!");
                return false;

            }
        }
        public override bool WriterSN(int TestPlanID, string StrSN,string StrFwRev,string Strip ,out int snid)
        {


            snid = -1;
            try
            {
                string StartTime = GetCurrTime().ToString("yyyy/MM/dd HH:mm:ss");
                OpenDatabase(true);
                //执行存储过程 
                SqlCommand command = new SqlCommand("InsertRunRecord ", conn);
                //说明命令要执行的是存储过程     
                command.CommandType = CommandType.StoredProcedure;
                //向存储过程中传递参数   
                command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ID", SqlDbType.Int));
                command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PID", SqlDbType.Int));
                command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SN", SqlDbType.NVarChar, 30));
                command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@StartTime", SqlDbType.DateTime));
                command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@EndTime", SqlDbType.DateTime));
                command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FWRev", SqlDbType.NVarChar, 5));
                command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IP", SqlDbType.NVarChar, 50));
                
                command.UpdatedRowSource = UpdateRowSource.None;

                command.Parameters["@PID"].Value = TestPlanID;   //当前的PID
                command.Parameters["@SN"].Value = StrSN;
                command.Parameters["@StartTime"].Value = StartTime;

                command.Parameters["@EndTime"].Value = "2000/1/1 08:00:00";
                command.Parameters["@FWRev"].Value = StrFwRev;
                command.Parameters["@IP"].Value = Strip;
                command.Parameters["@ID"].Direction = ParameterDirection.Output;
                
                command.ExecuteNonQuery();
                snid = Convert.ToInt32(command.Parameters["@ID"].Value);
                OpenDatabase(false);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }

        }
        public override bool UpdateDataTable(string SQLCmd, DataTable NewChangeDT)
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


                da.Update(NewChangeDT);
                tr.Commit();

                result = true;

                return result;
            }
            catch (Exception ex)
            {
                try
                {
                    MessageBox.Show(ex.ToString());
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
    #endregion 
}

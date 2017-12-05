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
    public class SqlManager : ServerDatabaseIO  //140722_2  
    {
        //public string ServerName, DBName, userName, pwd;

        public SqlManager(string serverName)
            : base(serverName) //读取XML后配置此部分
        {    
                  
        }
        public SqlManager(string serverName, string dbName, string user, string pwd)
            : base(serverName, dbName, user, pwd) //140722_2  
        {

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
                long myCurrIdent = Convert.ToInt64(mySQLcmd.ExecuteScalar());

                if (myCurrIdent > 1 )    //140707_2
                {
                    myValue = myCurrIdent;
                }
                else if (myValue1 > 0 && myCurrIdent ==1)
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

        public override bool UpdateDT()
        {
            bool myOPresult = false;

            long myNewlastIDGlobalType = GetLastInsertData("GlobalProductionType");
            long myNewlastIDGlobalPN = GetLastInsertData("GlobalProductionName");
            long myNewlastIDGlobalMSA = GetLastInsertData("GlobalMSA");
            long myNewlastIDGlobalMSAPrmtr = GetLastInsertData("GlobalMSADefintionInf");

            long myNewlastIDGlobalMCoef = GetLastInsertData("GlobalManufactureCoefficientsGroup");  //140704_4
            long myNewlastIDGlobalMCoefPrmtr = GetLastInsertData("GlobalManufactureCoefficients");  //140704_4

            long myNewlastIDGlobalChipCtrl = GetLastInsertData("GlobalManufactureChipsetControl");  //140706_0
            long myNewlastIDGlobalChipInit = GetLastInsertData("GlobalManufactureChipsetInitialize");//140706_0
            long myNewlastIDGlobalEEPROMInit = GetLastInsertData("GlobalMSAEEPROMInitialize");      //140706_0

            long myNewlastIDTestEquip = GetLastInsertData("GlobalAllEquipmentList");                //140707_0
            long myNewlastIDTestEquipPrmtr = GetLastInsertData("GlobalAllEquipmentParamterList");   //140707_0

            long myNewlastIDGlobalAPP = GetLastInsertData("GlobalAllAppModelList");                 //140707_1
            long myNewlastIDTestModel = GetLastInsertData("GlobalAllTestModelList");                //140707_1
            long myNewlastIDTestPrmtr = GetLastInsertData("GlobalTestModelParamterList");           //140707_1
            

            if (conn == null) conn.Open();
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }

            System.Data.SqlClient.SqlTransaction tr;
            tr = conn.BeginTransaction(IsolationLevel.ReadCommitted);

            try
            {

                SqlCommand cm1 = new SqlCommand("select * from  " + "GlobalProductionType", conn);
                SqlDataAdapter da1 = new SqlDataAdapter(cm1);
                SqlCommandBuilder cb1 = new SqlCommandBuilder(da1);
                DataTable dt1 = new DataTable();
                da1.SelectCommand.Transaction = tr;
                da1.Fill(dt1);

                SqlCommand cm2 = new SqlCommand("select * from  " + "GlobalProductionName", conn);
                SqlDataAdapter da2 = new SqlDataAdapter(cm2);
                SqlCommandBuilder cb2 = new SqlCommandBuilder(da2);
                DataTable dt2 = new DataTable();
                da2.SelectCommand.Transaction = tr;
                da2.Fill(dt2);

                SqlCommand cm3 = new SqlCommand("select * from  " + "GlobalMSA", conn);
                SqlDataAdapter da3 = new SqlDataAdapter(cm3);
                SqlCommandBuilder cb3 = new SqlCommandBuilder(da3);
                DataTable dt3 = new DataTable();
                da3.SelectCommand.Transaction = tr;
                da3.Fill(dt3);

                SqlCommand cm4 = new SqlCommand("select * from  " + "GlobalMSADefintionInf", conn);
                SqlDataAdapter da4 = new SqlDataAdapter(cm4);
                SqlCommandBuilder cb4 = new SqlCommandBuilder(da4);
                DataTable dt4 = new DataTable();
                da4.SelectCommand.Transaction = tr;
                da4.Fill(dt4);

                //140704>>>GlobalManufactureCoefficientsGroup
                SqlCommand cm5 = new SqlCommand("select * from  " + "GlobalManufactureCoefficientsGroup", conn);
                SqlDataAdapter da5 = new SqlDataAdapter(cm5);
                SqlCommandBuilder cb5 = new SqlCommandBuilder(da5);
                DataTable dt5 = new DataTable();
                da5.SelectCommand.Transaction = tr;
                da5.Fill(dt5);

                //140704>>>GlobalManufactureCoefficients
                SqlCommand cm6 = new SqlCommand("select * from  " + "GlobalManufactureCoefficients", conn);
                SqlDataAdapter da6 = new SqlDataAdapter(cm6);
                SqlCommandBuilder cb6 = new SqlCommandBuilder(da6);
                DataTable dt6 = new DataTable();
                da6.SelectCommand.Transaction = tr;
                da6.Fill(dt6);

                //140706_0>>>>>>>>>>>>>>>>>>                
                SqlCommand cm7 = new SqlCommand("select * from  " + "GlobalManufactureChipsetControl", conn);
                SqlDataAdapter da7 = new SqlDataAdapter(cm7);
                SqlCommandBuilder cb7 = new SqlCommandBuilder(da7);
                DataTable dt7 = new DataTable();
                da7.SelectCommand.Transaction = tr;
                da7.Fill(dt7);

                SqlCommand cm8 = new SqlCommand("select * from  " + "GlobalManufactureChipsetInitialize", conn);
                SqlDataAdapter da8 = new SqlDataAdapter(cm8);
                SqlCommandBuilder cb8 = new SqlCommandBuilder(da8);
                DataTable dt8 = new DataTable();
                da8.SelectCommand.Transaction = tr;
                da8.Fill(dt8);

                SqlCommand cm9 = new SqlCommand("select * from  " + "GlobalMSAEEPROMInitialize", conn);
                SqlDataAdapter da9 = new SqlDataAdapter(cm9);
                SqlCommandBuilder cb9 = new SqlCommandBuilder(da9);
                DataTable dt9 = new DataTable();
                da9.SelectCommand.Transaction = tr;
                da9.Fill(dt9);

                SqlCommand cm10 = new SqlCommand("select * from  " + "GlobalAllEquipmentList", conn);
                SqlDataAdapter da10 = new SqlDataAdapter(cm10);
                SqlCommandBuilder cb10 = new SqlCommandBuilder(da10);
                DataTable dt10 = new DataTable();
                da10.SelectCommand.Transaction = tr;
                da10.Fill(dt10);

                SqlCommand cm11 = new SqlCommand("select * from  " + "GlobalAllEquipmentParamterList", conn);
                SqlDataAdapter da11 = new SqlDataAdapter(cm11);
                SqlCommandBuilder cb11 = new SqlCommandBuilder(da11);
                DataTable dt11 = new DataTable();
                da11.SelectCommand.Transaction = tr;
                da11.Fill(dt11);


                SqlCommand cm12 = new SqlCommand("select * from  " + "GlobalAllAppModelList", conn);
                SqlDataAdapter da12 = new SqlDataAdapter(cm12);
                SqlCommandBuilder cb12 = new SqlCommandBuilder(da12);
                DataTable dt12 = new DataTable();
                da12.SelectCommand.Transaction = tr;
                da12.Fill(dt12);

                SqlCommand cm13 = new SqlCommand("select * from  " + "GlobalAllTestModelList", conn);
                SqlDataAdapter da13 = new SqlDataAdapter(cm13);
                SqlCommandBuilder cb13 = new SqlCommandBuilder(da13);
                DataTable dt13 = new DataTable();
                da13.SelectCommand.Transaction = tr;
                da13.Fill(dt13);

                SqlCommand cm14 = new SqlCommand("select * from  " + "GlobalTestModelParamterList", conn);
                SqlDataAdapter da14 = new SqlDataAdapter(cm14);
                SqlCommandBuilder cb14 = new SqlCommandBuilder(da14);
                DataTable dt14 = new DataTable();
                da14.SelectCommand.Transaction = tr;
                da14.Fill(dt14);

                //140707_0>>>>>>>>>>>>>>>>>>>>>
                //myNewlastIDTestEquip  myNewlastIDTestEquipPrmtr [GlobalAllEquipmentList][GlobalAllEquipmentParamterList]   
                for (int i = 0; i < MainForm.GlobalDS.Tables["GlobalAllEquipmentList"].Rows.Count; i++)
                {
                    if ((MainForm.GlobalDS.Tables["GlobalAllEquipmentList"].Rows[i].RowState == DataRowState.Added)
                        && (Convert.ToInt64(MainForm.GlobalDS.Tables["GlobalAllEquipmentList"].Rows[i]["ID"]) > MainForm.origIDGlobalEquip)
                       )
                    {
                        MainForm.GlobalDS.Tables["GlobalAllEquipmentList"].Rows[i]["ID"] = myNewlastIDTestEquip + 1;
                        myNewlastIDTestEquip++;
                    }
                }

                for (int i = 0; i < MainForm.GlobalDS.Tables["GlobalAllEquipmentParamterList"].Rows.Count; i++)
                {
                    if ((MainForm.GlobalDS.Tables["GlobalAllEquipmentParamterList"].Rows[i].RowState == DataRowState.Added)
                        && (Convert.ToInt64(MainForm.GlobalDS.Tables["GlobalAllEquipmentParamterList"].Rows[i]["ID"]) > MainForm.origIDGlobalEquipPrmtr)
                       )
                    {
                        MainForm.GlobalDS.Tables["GlobalAllEquipmentParamterList"].Rows[i]["ID"] = myNewlastIDTestEquipPrmtr + 1;
                        myNewlastIDTestEquipPrmtr++;
                    }
                }
                //140707_0<<<<<<<<<<<<<<<<<<

                //140707_1>>>>>>>>>>>>>>>>>>>>>  
                for (int i = 0; i < MainForm.GlobalDS.Tables["GlobalAllAppModelList"].Rows.Count; i++)
                {
                    if ((MainForm.GlobalDS.Tables["GlobalAllAppModelList"].Rows[i].RowState == DataRowState.Added)  //140917_1 FIX BUG
                        && (Convert.ToInt64(MainForm.GlobalDS.Tables["GlobalAllAppModelList"].Rows[i]["ID"]) > MainForm.origIDGlobalAPP)    //140917_1 FIX BUG
                       )
                    {
                        MainForm.GlobalDS.Tables["GlobalAllAppModelList"].Rows[i]["ID"] = myNewlastIDGlobalAPP + 1; //140917_1 FIX BUG
                        myNewlastIDGlobalAPP++;
                    }
                }

                for (int i = 0; i < MainForm.GlobalDS.Tables["GlobalAllTestModelList"].Rows.Count; i++)
                {
                    if ((MainForm.GlobalDS.Tables["GlobalAllTestModelList"].Rows[i].RowState == DataRowState.Added)
                        && (Convert.ToInt64(MainForm.GlobalDS.Tables["GlobalAllTestModelList"].Rows[i]["ID"]) > MainForm.origIDGlobalModel)
                       )
                    {
                        MainForm.GlobalDS.Tables["GlobalAllTestModelList"].Rows[i]["ID"] = myNewlastIDTestModel + 1;
                        myNewlastIDTestModel++;
                    }
                }

                for (int i = 0; i < MainForm.GlobalDS.Tables["GlobalTestModelParamterList"].Rows.Count; i++)
                {
                    if ((MainForm.GlobalDS.Tables["GlobalTestModelParamterList"].Rows[i].RowState == DataRowState.Added)
                        && (Convert.ToInt64(MainForm.GlobalDS.Tables["GlobalTestModelParamterList"].Rows[i]["ID"]) > MainForm.origIDGlobalPrmtr)
                       )
                    {
                        MainForm.GlobalDS.Tables["GlobalTestModelParamterList"].Rows[i]["ID"] = myNewlastIDTestPrmtr + 1;
                        myNewlastIDTestPrmtr++;
                    }
                }
                //140707_1<<<<<<<<<<<<<<<<<<

                //myNewlastIDGlobalChipCtrl   myNewlastIDGlobalChipInit   myNewlastIDGlobalEEPROMInit
                for (int i = 0; i < MainForm.GlobalDS.Tables["GlobalManufactureChipsetControl"].Rows.Count; i++)
                {
                    if ((MainForm.GlobalDS.Tables["GlobalManufactureChipsetControl"].Rows[i].RowState == DataRowState.Added)
                        && (Convert.ToInt64(MainForm.GlobalDS.Tables["GlobalManufactureChipsetControl"].Rows[i]["ID"]) > MainForm.origIDChipCtrl)
                       )
                    {
                        MainForm.GlobalDS.Tables["GlobalManufactureChipsetControl"].Rows[i]["ID"] = myNewlastIDGlobalChipCtrl + 1;
                        myNewlastIDGlobalChipCtrl++;
                    }
                }

                for (int i = 0; i < MainForm.GlobalDS.Tables["GlobalManufactureChipsetInitialize"].Rows.Count; i++)
                {
                    if ((MainForm.GlobalDS.Tables["GlobalManufactureChipsetInitialize"].Rows[i].RowState == DataRowState.Added)
                        && (Convert.ToInt64(MainForm.GlobalDS.Tables["GlobalManufactureChipsetInitialize"].Rows[i]["ID"]) > MainForm.origIDChipInit)
                       )
                    {
                        MainForm.GlobalDS.Tables["GlobalManufactureChipsetInitialize"].Rows[i]["ID"] = myNewlastIDGlobalChipInit + 1;
                        myNewlastIDGlobalChipInit++;
                    }
                }

                for (int i = 0; i < MainForm.GlobalDS.Tables["GlobalMSAEEPROMInitialize"].Rows.Count; i++)
                {
                    if ((MainForm.GlobalDS.Tables["GlobalMSAEEPROMInitialize"].Rows[i].RowState == DataRowState.Added)
                        && (Convert.ToInt64(MainForm.GlobalDS.Tables["GlobalMSAEEPROMInitialize"].Rows[i]["ID"]) > MainForm.origIDEEPROMInit)
                       )
                    {
                        MainForm.GlobalDS.Tables["GlobalMSAEEPROMInitialize"].Rows[i]["ID"] = myNewlastIDGlobalEEPROMInit + 1;
                        myNewlastIDGlobalEEPROMInit++;
                    }
                }

                //140706_0<<<<<<<<<<<<<<<<<<


                for (int i = 0; i < MainForm.GlobalDS.Tables["GlobalProductionType"].Rows.Count; i++)
                {
                    if ((MainForm.GlobalDS.Tables["GlobalProductionType"].Rows[i].RowState == DataRowState.Added)
                        && (Convert.ToInt64(MainForm.GlobalDS.Tables["GlobalProductionType"].Rows[i]["ID"]) > MainForm.origIDGlobalType)
                       )
                    {
                        MainForm.GlobalDS.Tables["GlobalProductionType"].Rows[i]["ID"] = myNewlastIDGlobalType + 1;
                        myNewlastIDGlobalType++;
                    }
                }



                for (int i = 0; i < MainForm.GlobalDS.Tables["GlobalProductionName"].Rows.Count; i++)
                {
                    if ((MainForm.GlobalDS.Tables["GlobalProductionName"].Rows[i].RowState == DataRowState.Added)
                        && (Convert.ToInt64(MainForm.GlobalDS.Tables[2].Rows[i]["ID"]) > MainForm.origIDGlobalPN))
                    {
                        MainForm.GlobalDS.Tables["GlobalProductionName"].Rows[i]["ID"] = myNewlastIDGlobalPN + 1;
                        myNewlastIDGlobalPN++;
                    }
                }



                for (int i = 0; i < MainForm.GlobalDS.Tables["GlobalMSA"].Rows.Count; i++)
                {
                    if ((MainForm.GlobalDS.Tables["GlobalMSA"].Rows[i].RowState == DataRowState.Added)
                        && (Convert.ToInt64(MainForm.GlobalDS.Tables["GlobalMSA"].Rows[i]["ID"]) > MainForm.origIDGlobalMSA)
                       )
                    {
                        MainForm.GlobalDS.Tables["GlobalMSA"].Rows[i]["ID"] = myNewlastIDGlobalMSA + 1;
                        myNewlastIDGlobalMSA++;
                    }
                }



                for (int i = 0; i < MainForm.GlobalDS.Tables["GlobalMSADefintionInf"].Rows.Count; i++)
                {
                    if ((MainForm.GlobalDS.Tables["GlobalMSADefintionInf"].Rows[i].RowState == DataRowState.Added)
                        && (Convert.ToInt64(MainForm.GlobalDS.Tables["GlobalMSADefintionInf"].Rows[i]["ID"]) > MainForm.origIDGlobalMSAPrmtr)
                       )
                    {
                        MainForm.GlobalDS.Tables["GlobalMSADefintionInf"].Rows[i]["ID"] = myNewlastIDGlobalMSAPrmtr + 1;
                        myNewlastIDGlobalMSAPrmtr++;
                    }
                }
                //140704>>>GlobalManufactureCoefficientsGroup
                for (int i = 0; i < MainForm.GlobalDS.Tables["GlobalManufactureCoefficientsGroup"].Rows.Count; i++)
                {
                    if ((MainForm.GlobalDS.Tables["GlobalManufactureCoefficientsGroup"].Rows[i].RowState == DataRowState.Added)
                        && (Convert.ToInt64(MainForm.GlobalDS.Tables["GlobalManufactureCoefficientsGroup"].Rows[i]["ID"]) > MainForm.origIDGlobalMCoefGroup)
                       )
                    {
                        MainForm.GlobalDS.Tables["GlobalManufactureCoefficientsGroup"].Rows[i]["ID"] = myNewlastIDGlobalMCoef + 1;
                        myNewlastIDGlobalMCoef++;
                    }
                }
                //140704>>>GlobalManufactureCoefficients
                for (int i = 0; i < MainForm.GlobalDS.Tables["GlobalManufactureCoefficients"].Rows.Count; i++)
                {
                    if ((MainForm.GlobalDS.Tables["GlobalManufactureCoefficients"].Rows[i].RowState == DataRowState.Added)
                        && (Convert.ToInt64(MainForm.GlobalDS.Tables["GlobalManufactureCoefficients"].Rows[i]["ID"]) > MainForm.origIDMCoefPrmtr)
                       )
                    {
                        MainForm.GlobalDS.Tables["GlobalManufactureCoefficients"].Rows[i]["ID"] = myNewlastIDGlobalMCoefPrmtr + 1;
                        myNewlastIDGlobalMCoefPrmtr++;
                    }
                }


                dt1 = MainForm.GlobalDS.Tables["GlobalProductionType"];
                dt2 = MainForm.GlobalDS.Tables["GlobalProductionName"];

                dt3 = MainForm.GlobalDS.Tables["GlobalMSA"];
                dt4 = MainForm.GlobalDS.Tables["GlobalMSADefintionInf"];

                //140704>>>
                dt5 = MainForm.GlobalDS.Tables["GlobalManufactureCoefficientsGroup"];
                dt6 = MainForm.GlobalDS.Tables["GlobalManufactureCoefficients"];


                //140707_0 >>>>>>>>>>>>>>
                dt10 = MainForm.GlobalDS.Tables["GlobalAllEquipmentList"];         
                dt11 = MainForm.GlobalDS.Tables["GlobalAllEquipmentParamterList"];


                //140707_1 >>>>>>>>>>>>>>
                dt12 = MainForm.GlobalDS.Tables["GlobalAllAppModelList"];
                dt13 = MainForm.GlobalDS.Tables["GlobalAllTestModelList"];
                dt14 = MainForm.GlobalDS.Tables["GlobalTestModelParamterList"];

                DataTable dtAdded10 = new DataTable(); //存放主表 新增 的数据
                DataTable dtMidified10 = new DataTable(); //存储主表 编辑 的行记录
                DataTable dtDeleted10 = new DataTable(); //存储主表 删除 的行记录

                dtDeleted10 = dt10.GetChanges(DataRowState.Deleted);
                dtMidified10 = dt10.GetChanges(DataRowState.Modified);
                dtAdded10 = dt10.GetChanges(DataRowState.Added);

                DataTable dtAdded11 = new DataTable(); //存放主表 新增 的数据
                DataTable dtMidified11 = new DataTable(); //存储主表 编辑 的行记录
                DataTable dtDeleted11 = new DataTable(); //存储主表 删除 的行记录

                dtDeleted11 = dt11.GetChanges(DataRowState.Deleted);
                dtMidified11 = dt11.GetChanges(DataRowState.Modified);
                dtAdded11 = dt11.GetChanges(DataRowState.Added);
                //140707_0 <<<<<<<<<<<<<<

                DataTable dtAdded12 = new DataTable(); //存放主表 新增 的数据
                DataTable dtMidified12 = new DataTable(); //存储主表 编辑 的行记录
                DataTable dtDeleted12 = new DataTable(); //存储主表 删除 的行记录

                dtDeleted12 = dt12.GetChanges(DataRowState.Deleted);
                dtMidified12 = dt12.GetChanges(DataRowState.Modified);
                dtAdded12 = dt12.GetChanges(DataRowState.Added);

                DataTable dtAdded13 = new DataTable(); //存放主表 新增 的数据
                DataTable dtMidified13 = new DataTable(); //存储主表 编辑 的行记录
                DataTable dtDeleted13 = new DataTable(); //存储主表 删除 的行记录

                dtDeleted13 = dt13.GetChanges(DataRowState.Deleted);
                dtMidified13 = dt13.GetChanges(DataRowState.Modified);
                dtAdded13 = dt13.GetChanges(DataRowState.Added);

                DataTable dtAdded14 = new DataTable(); //存放主表 新增 的数据
                DataTable dtMidified14 = new DataTable(); //存储主表 编辑 的行记录
                DataTable dtDeleted14 = new DataTable(); //存储主表 删除 的行记录

                dtDeleted14 = dt14.GetChanges(DataRowState.Deleted);
                dtMidified14 = dt14.GetChanges(DataRowState.Modified);
                dtAdded14 = dt14.GetChanges(DataRowState.Added);
                //140707_1 <<<<<<<<<<<<<<

                //140706_0>>>>>>>>>>>>>>>>>> 均为 GlobalProductionName的子表项目
                dt7 = MainForm.GlobalDS.Tables["GlobalManufactureChipsetControl"];
                dt8 = MainForm.GlobalDS.Tables["GlobalManufactureChipsetInitialize"];
                dt9 = MainForm.GlobalDS.Tables["GlobalMSAEEPROMInitialize"];

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

                DataTable dtAdded9 = new DataTable(); //存放主表 新增 的数据
                DataTable dtMidified9 = new DataTable(); //存储主表 编辑 的行记录
                DataTable dtDeleted9 = new DataTable(); //存储主表 删除 的行记录

                dtDeleted9 = dt9.GetChanges(DataRowState.Deleted);
                dtMidified9 = dt9.GetChanges(DataRowState.Modified);
                dtAdded9 = dt9.GetChanges(DataRowState.Added);
                //140706_0<<<<<<<<<<<<<<<<<<

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

                //===============================================================
                //删除资料开始
                //此处必须注意删除顺序 由子表删除后再删除主表
                //GlobalProductionName-->GlobalProductionType
                //GlobalMSADefintionInf-->GlobalManufactureCoefficientsGroup
                //GlobalMSADefintionInf-->GlobalMSA
                //dt1 = MainForm.GlobalDS.Tables["GlobalProductionType"];
                //dt2 = MainForm.GlobalDS.Tables["GlobalProductionName"];
                //dt3 = MainForm.GlobalDS.Tables["GlobalMSA"];
                //dt4 = MainForm.GlobalDS.Tables["GlobalMSADefintionInf"];                
                //dt5 = MainForm.GlobalDS.Tables["GlobalManufactureCoefficientsGroup"];
                //dt6 = MainForm.GlobalDS.Tables["GlobalManufactureCoefficients"];
                //140706_0>>>>>>>>>>>>>>>>>> 均为 GlobalProductionName的子表项目
                //dt7 = MainForm.GlobalDS.Tables["GlobalManufactureChipsetControl"];
                //dt8 = MainForm.GlobalDS.Tables["GlobalManufactureChipsetInitialize"];
                //dt9 = MainForm.GlobalDS.Tables["GlobalMSAEEPROMInitialize"];
                //140707_0 >>>>>>>>>>>>>>
                //dt10 = MainForm.GlobalDS.Tables["GlobalAllEquipmentList"];
                //dt11 = MainForm.GlobalDS.Tables["GlobalAllEquipmentParamterList"];
                ////140707_1 >>>>>>>>>>>>>>
                //dt12 = MainForm.GlobalDS.Tables["GlobalAllAppModelList"];
                //dt13 = MainForm.GlobalDS.Tables["GlobalAllTestModelList"];
                //dt14 = MainForm.GlobalDS.Tables["GlobalTestModelParamterList"];

                //140706_0>>>>>>>>>>>>>>>>>> 均为 GlobalProductionName的子表项目
                if (dtDeleted7 != null) da7.Update(dtDeleted7);
                if (dtDeleted8 != null) da8.Update(dtDeleted8);
                if (dtDeleted9 != null) da9.Update(dtDeleted9);
                //140706_0<<<<<<<<<<<<<<<<<< 

                //140707_0>>>>>>>>>>>>>>>>>>  //设备资料 独立       
                if (dtDeleted11 != null) da11.Update(dtDeleted11);  //140911_2 调整顺序[删子后删主]
                if (dtDeleted10 != null) da10.Update(dtDeleted10);  //140911_2 调整顺序[删子后删主]                
                //140707_0<<<<<<<<<<<<<<<<<< 

                //140707_0>>>>>>>>>>>>>>>>>>  //APP+Model+Prmtr资料 独立               
                if (dtDeleted14 != null) da14.Update(dtDeleted14);
                if (dtDeleted13 != null) da13.Update(dtDeleted13);
                if (dtDeleted12 != null) da12.Update(dtDeleted12);
                //140707_0<<<<<<<<<<<<<<<<<< 


                //Type + PN
                if (dtDeleted2 != null) da2.Update(dtDeleted2);
                if (dtDeleted1 != null) da1.Update(dtDeleted1);
                if (dtDeleted4 != null) da4.Update(dtDeleted4);
                if (dtDeleted3 != null) da3.Update(dtDeleted3);
                if (dtDeleted6 != null) da6.Update(dtDeleted6);
                if (dtDeleted5 != null) da5.Update(dtDeleted5);
                //删除资料结束
                //===============================================================


                //===============================================================
                //新增资料开始
                //此处必须注意更新顺序 由主表-->子表
                if (dtAdded5 != null) da5.Update(dtAdded5);
                if (dtMidified5 != null) da5.Update(dtMidified5);

                if (dtAdded6 != null) da6.Update(dtAdded6);
                if (dtMidified6 != null) da6.Update(dtMidified6);

                if (dtAdded3 != null) da3.Update(dtAdded3);
                if (dtMidified3 != null) da3.Update(dtMidified3);

                if (dtAdded4 != null) da4.Update(dtAdded4);
                if (dtMidified4 != null) da4.Update(dtMidified4);
                if (dtAdded1 != null) da1.Update(dtAdded1);
                if (dtMidified1 != null) da1.Update(dtMidified1);

                if (dtAdded2 != null) da2.Update(dtAdded2);
                if (dtMidified2 != null) da2.Update(dtMidified2);

                //140706_0>>>>>>>>>>>>>>>>>> 均为 GlobalProductionName的子表项目
                if (dtAdded7 != null) da7.Update(dtAdded7);
                if (dtMidified7 != null) da7.Update(dtMidified7);

                if (dtAdded8 != null) da8.Update(dtAdded8);
                if (dtMidified8 != null) da8.Update(dtMidified8);

                if (dtAdded9 != null) da9.Update(dtAdded9);
                if (dtMidified9 != null) da9.Update(dtMidified9);
                //140706_0<<<<<<<<<<<<<<<<<< 

                //140707_0<<<<<<<<<<<<<<<<<< 
                if (dtAdded10 != null) da10.Update(dtAdded10);
                if (dtMidified10 != null) da10.Update(dtMidified10);

                if (dtAdded11 != null) da11.Update(dtAdded11);
                if (dtMidified11 != null) da11.Update(dtMidified11);
                //140707_0<<<<<<<<<<<<<<<<<< 

                //140707_0<<<<<<<<<<<<<<<<<< 
                if (dtAdded12 != null) da12.Update(dtAdded12);
                if (dtMidified12 != null) da12.Update(dtMidified12);

                if (dtAdded13 != null) da13.Update(dtAdded13);
                if (dtMidified13 != null) da13.Update(dtMidified13);

                if (dtAdded14 != null) da14.Update(dtAdded14);
                if (dtMidified14 != null) da14.Update(dtMidified14);
                //新增资料结束
                //===============================================================

                //140707_0<<<<<<<<<<<<<<<<<< 
                tr.Commit();
                myOPresult = true;

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
                //DataTable mydt = new DataTable();
                da.SelectCommand.Transaction = tr;
                //mydt = NewChangeDT.GetChanges();
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
                    if (conn.State != ConnectionState.Closed) conn.Close();
                    return result;
                }
                catch (System.Exception TransactionEx)
                {
                    //Handle Exception
                    MessageBox.Show(TransactionEx.ToString());
                    if (conn.State != ConnectionState.Closed) conn.Close();
                    return result;
                }
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
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
                if (conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
            }       
        }

        public override bool BlnISExistTable(string tabName)
        {
            try
            {
                if (conn.State != ConnectionState.Closed)
                    conn.Close();
                conn.Open();

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
            string strConnection = @"Data Source=" + ServerName + ";Initial Catalog=" + DBName + ";Provider=SQLOLEDB.1;user id = " + SetPWDCode( userName,true,4) + ";password=" + SetPWDCode( pwd,true,4) + ";";
            OleDbConnection Conn = new OleDbConnection(strConnection);
            try
            {
                if (conn == null) conn.Open();
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                //DataTable cnSch = myAccessIO.Conn.GetSchema("Tables");
                Conn.Open();
                DataTable cnSch = Conn.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });

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
                MessageBox.Show( ex.Message);
                return null;
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                    conn.Close();
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

    }

    public class AccessManager : LocalDatabaseIO //140722_2     
    {
        public AccessManager(string AccessFilePath)
            : base(AccessFilePath)    //140722_2        
        {
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
                    
        public override long GetLastInsertData(string TableName)    //140919_0
        {
            try
            {
                if (conn == null) conn.Open();
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                long myValue;
                string sqlCMD1 = "Select count(id) from " + TableName;
                OleDbCommand mySQLcmd1 = new OleDbCommand(sqlCMD1, conn);
                long myValue1 = Convert.ToInt64(mySQLcmd1.ExecuteScalar());

                string sqlCMD = "Select MAX(ID) From " + TableName;
                OleDbCommand mySQLcmd = new OleDbCommand(sqlCMD, conn);
                long myCurrIdent;
                if (mySQLcmd.ExecuteScalar().ToString().Trim().Length >0)   
                {
                    myCurrIdent = Convert.ToInt64(mySQLcmd.ExecuteScalar());
                }
                else
                {
                    myCurrIdent = 0;
                }


                if (myValue1 > 1)    //140707_2
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
                return -1;
            }

            finally
            {
                if (conn.State != ConnectionState.Closed)
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
                if (conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
            }
        }

        //public override bool UpdateDT()
        //{
        //    bool myOPresult = false;

        //    long myNewlastIDGlobalType = GetLastInsertData("GlobalProductionType");
        //    long myNewlastIDGlobalPN = GetLastInsertData("GlobalProductionName");
        //    long myNewlastIDGlobalMSA = GetLastInsertData("GlobalMSA");
        //    long myNewlastIDGlobalMSAPrmtr = GetLastInsertData("GlobalMSADefintionInf");

        //    long myNewlastIDGlobalMCoef = GetLastInsertData("GlobalManufactureCoefficientsGroup");  //140704_4
        //    long myNewlastIDGlobalMCoefPrmtr = GetLastInsertData("GlobalManufactureCoefficients");  //140704_4

        //    long myNewlastIDGlobalChipCtrl = GetLastInsertData("GlobalManufactureChipsetControl");  //140706_0
        //    long myNewlastIDGlobalChipInit = GetLastInsertData("GlobalManufactureChipsetInitialize");//140706_0
        //    long myNewlastIDGlobalEEPROMInit = GetLastInsertData("GlobalMSAEEPROMInitialize");      //140706_0

        //    long myNewlastIDTestEquip = GetLastInsertData("GlobalAllEquipmentList");                //140707_0
        //    long myNewlastIDTestEquipPrmtr = GetLastInsertData("GlobalAllEquipmentParamterList");   //140707_0

        //    long myNewlastIDGlobalAPP = GetLastInsertData("GlobalAllAppModelList");                 //140707_1
        //    long myNewlastIDTestModel = GetLastInsertData("GlobalAllTestModelList");                //140707_1
        //    long myNewlastIDTestPrmtr = GetLastInsertData("GlobalTestModelParamterList");           //140707_1


        //    if (conn == null) conn.Open();
        //    if (conn.State != ConnectionState.Open)
        //    {
        //        conn.Open();
        //    }

        //    System.Data.OleDb.OleDbTransaction tr;
        //    tr = conn.BeginTransaction(IsolationLevel.ReadCommitted);

        //    try
        //    {

        //        OleDbCommand cm1 = new OleDbCommand("select * from  " + "GlobalProductionType", conn);
        //        OleDbDataAdapter da1 = new OleDbDataAdapter(cm1);
        //        OleDbCommandBuilder cb1 = new OleDbCommandBuilder(da1);
        //        DataTable dt1 = new DataTable();
        //        da1.SelectCommand.Transaction = tr;
        //        da1.Fill(dt1);

        //        OleDbCommand cm2 = new OleDbCommand("select * from  " + "GlobalProductionName", conn);
        //        OleDbDataAdapter da2 = new OleDbDataAdapter(cm2);
        //        OleDbCommandBuilder cb2 = new OleDbCommandBuilder(da2);
        //        DataTable dt2 = new DataTable();
        //        da2.SelectCommand.Transaction = tr;
        //        da2.Fill(dt2);

        //        OleDbCommand cm3 = new OleDbCommand("select * from  " + "GlobalMSA", conn);
        //        OleDbDataAdapter da3 = new OleDbDataAdapter(cm3);
        //        OleDbCommandBuilder cb3 = new OleDbCommandBuilder(da3);
        //        DataTable dt3 = new DataTable();
        //        da3.SelectCommand.Transaction = tr;
        //        da3.Fill(dt3);

        //        OleDbCommand cm4 = new OleDbCommand("select * from  " + "GlobalMSADefintionInf", conn);
        //        OleDbDataAdapter da4 = new OleDbDataAdapter(cm4);
        //        OleDbCommandBuilder cb4 = new OleDbCommandBuilder(da4);
        //        DataTable dt4 = new DataTable();
        //        da4.SelectCommand.Transaction = tr;
        //        da4.Fill(dt4);

        //        //140704>>>GlobalManufactureCoefficientsGroup
        //        OleDbCommand cm5 = new OleDbCommand("select * from  " + "GlobalManufactureCoefficientsGroup", conn);
        //        OleDbDataAdapter da5 = new OleDbDataAdapter(cm5);
        //        OleDbCommandBuilder cb5 = new OleDbCommandBuilder(da5);
        //        DataTable dt5 = new DataTable();
        //        da5.SelectCommand.Transaction = tr;
        //        da5.Fill(dt5);

        //        //140704>>>GlobalManufactureCoefficients
        //        OleDbCommand cm6 = new OleDbCommand("select * from  " + "GlobalManufactureCoefficients", conn);
        //        OleDbDataAdapter da6 = new OleDbDataAdapter(cm6);
        //        OleDbCommandBuilder cb6 = new OleDbCommandBuilder(da6);
        //        DataTable dt6 = new DataTable();
        //        da6.SelectCommand.Transaction = tr;
        //        da6.Fill(dt6);

        //        //140706_0>>>>>>>>>>>>>>>>>>                
        //        OleDbCommand cm7 = new OleDbCommand("select * from  " + "GlobalManufactureChipsetControl", conn);
        //        OleDbDataAdapter da7 = new OleDbDataAdapter(cm7);
        //        OleDbCommandBuilder cb7 = new OleDbCommandBuilder(da7);
        //        DataTable dt7 = new DataTable();
        //        da7.SelectCommand.Transaction = tr;
        //        da7.Fill(dt7);

        //        OleDbCommand cm8 = new OleDbCommand("select * from  " + "GlobalManufactureChipsetInitialize", conn);
        //        OleDbDataAdapter da8 = new OleDbDataAdapter(cm8);
        //        OleDbCommandBuilder cb8 = new OleDbCommandBuilder(da8);
        //        DataTable dt8 = new DataTable();
        //        da8.SelectCommand.Transaction = tr;
        //        da8.Fill(dt8);

        //        OleDbCommand cm9 = new OleDbCommand("select * from  " + "GlobalMSAEEPROMInitialize", conn);
        //        OleDbDataAdapter da9 = new OleDbDataAdapter(cm9);
        //        OleDbCommandBuilder cb9 = new OleDbCommandBuilder(da9);
        //        DataTable dt9 = new DataTable();
        //        da9.SelectCommand.Transaction = tr;
        //        da9.Fill(dt9);

        //        OleDbCommand cm10 = new OleDbCommand("select * from  " + "GlobalAllEquipmentList", conn);
        //        OleDbDataAdapter da10 = new OleDbDataAdapter(cm10);
        //        OleDbCommandBuilder cb10 = new OleDbCommandBuilder(da10);
        //        DataTable dt10 = new DataTable();
        //        da10.SelectCommand.Transaction = tr;
        //        da10.Fill(dt10);

        //        OleDbCommand cm11 = new OleDbCommand("select * from  " + "GlobalAllEquipmentParamterList", conn);
        //        OleDbDataAdapter da11 = new OleDbDataAdapter(cm11);
        //        OleDbCommandBuilder cb11 = new OleDbCommandBuilder(da11);
        //        DataTable dt11 = new DataTable();
        //        da11.SelectCommand.Transaction = tr;
        //        da11.Fill(dt11);


        //        OleDbCommand cm12 = new OleDbCommand("select * from  " + "GlobalAllAppModelList", conn);
        //        OleDbDataAdapter da12 = new OleDbDataAdapter(cm12);
        //        OleDbCommandBuilder cb12 = new OleDbCommandBuilder(da12);
        //        DataTable dt12 = new DataTable();
        //        da12.SelectCommand.Transaction = tr;
        //        da12.Fill(dt12);

        //        OleDbCommand cm13 = new OleDbCommand("select * from  " + "GlobalAllTestModelList", conn);
        //        OleDbDataAdapter da13 = new OleDbDataAdapter(cm13);
        //        OleDbCommandBuilder cb13 = new OleDbCommandBuilder(da13);
        //        DataTable dt13 = new DataTable();
        //        da13.SelectCommand.Transaction = tr;
        //        da13.Fill(dt13);

        //        OleDbCommand cm14 = new OleDbCommand("select * from  " + "GlobalTestModelParamterList", conn);
        //        OleDbDataAdapter da14 = new OleDbDataAdapter(cm14);
        //        OleDbCommandBuilder cb14 = new OleDbCommandBuilder(da14);
        //        DataTable dt14 = new DataTable();
        //        da14.SelectCommand.Transaction = tr;
        //        da14.Fill(dt14);

        //        //140707_0>>>>>>>>>>>>>>>>>>>>>
        //        //myNewlastIDTestEquip  myNewlastIDTestEquipPrmtr [GlobalAllEquipmentList][GlobalAllEquipmentParamterList]   
        //        for (int i = 0; i < MainForm.GlobalDS.Tables["GlobalAllEquipmentList"].Rows.Count; i++)
        //        {
        //            if ((MainForm.GlobalDS.Tables["GlobalAllEquipmentList"].Rows[i].RowState == DataRowState.Added)
        //                && (Convert.ToInt64(MainForm.GlobalDS.Tables["GlobalAllEquipmentList"].Rows[i]["ID"]) > MainForm.origIDGlobalEquip)
        //               )
        //            {
        //                MainForm.GlobalDS.Tables["GlobalAllEquipmentList"].Rows[i]["ID"] = myNewlastIDTestEquip + 1;
        //                myNewlastIDTestEquip++;
        //            }
        //        }

        //        for (int i = 0; i < MainForm.GlobalDS.Tables["GlobalAllEquipmentParamterList"].Rows.Count; i++)
        //        {
        //            if ((MainForm.GlobalDS.Tables["GlobalAllEquipmentParamterList"].Rows[i].RowState == DataRowState.Added)
        //                && (Convert.ToInt64(MainForm.GlobalDS.Tables["GlobalAllEquipmentParamterList"].Rows[i]["ID"]) > MainForm.origIDGlobalEquipPrmtr)
        //               )
        //            {
        //                MainForm.GlobalDS.Tables["GlobalAllEquipmentParamterList"].Rows[i]["ID"] = myNewlastIDTestEquipPrmtr + 1;
        //                myNewlastIDTestEquipPrmtr++;
        //            }
        //        }
        //        //140707_0<<<<<<<<<<<<<<<<<<

        //        //140707_1>>>>>>>>>>>>>>>>>>>>>  
        //        for (int i = 0; i < MainForm.GlobalDS.Tables["GlobalAllAppModelList"].Rows.Count; i++)
        //        {
        //            if ((MainForm.GlobalDS.Tables["GlobalAllEquipmentList"].Rows[i].RowState == DataRowState.Added)
        //                && (Convert.ToInt64(MainForm.GlobalDS.Tables["GlobalAllEquipmentList"].Rows[i]["ID"]) > MainForm.origIDGlobalAPP)
        //               )
        //            {
        //                MainForm.GlobalDS.Tables["GlobalAllEquipmentList"].Rows[i]["ID"] = myNewlastIDGlobalAPP + 1;
        //                myNewlastIDGlobalAPP++;
        //            }
        //        }

        //        for (int i = 0; i < MainForm.GlobalDS.Tables["GlobalAllTestModelList"].Rows.Count; i++)
        //        {
        //            if ((MainForm.GlobalDS.Tables["GlobalAllEquipmentParamterList"].Rows[i].RowState == DataRowState.Added)
        //                && (Convert.ToInt64(MainForm.GlobalDS.Tables["GlobalAllEquipmentParamterList"].Rows[i]["ID"]) > MainForm.origIDGlobalModel)
        //               )
        //            {
        //                MainForm.GlobalDS.Tables["GlobalAllEquipmentParamterList"].Rows[i]["ID"] = myNewlastIDTestModel + 1;
        //                myNewlastIDTestModel++;
        //            }
        //        }

        //        for (int i = 0; i < MainForm.GlobalDS.Tables["GlobalTestModelParamterList"].Rows.Count; i++)
        //        {
        //            if ((MainForm.GlobalDS.Tables["GlobalTestModelParamterList"].Rows[i].RowState == DataRowState.Added)
        //                && (Convert.ToInt64(MainForm.GlobalDS.Tables["GlobalTestModelParamterList"].Rows[i]["ID"]) > MainForm.origIDGlobalPrmtr)
        //               )
        //            {
        //                MainForm.GlobalDS.Tables["GlobalTestModelParamterList"].Rows[i]["ID"] = myNewlastIDTestPrmtr + 1;
        //                myNewlastIDTestPrmtr++;
        //            }
        //        }
        //        //140707_1<<<<<<<<<<<<<<<<<<

        //        //myNewlastIDGlobalChipCtrl   myNewlastIDGlobalChipInit   myNewlastIDGlobalEEPROMInit
        //        for (int i = 0; i < MainForm.GlobalDS.Tables["GlobalManufactureChipsetControl"].Rows.Count; i++)
        //        {
        //            if ((MainForm.GlobalDS.Tables["GlobalManufactureChipsetControl"].Rows[i].RowState == DataRowState.Added)
        //                && (Convert.ToInt64(MainForm.GlobalDS.Tables["GlobalManufactureChipsetControl"].Rows[i]["ID"]) > MainForm.origIDChipCtrl)
        //               )
        //            {
        //                MainForm.GlobalDS.Tables["GlobalManufactureChipsetControl"].Rows[i]["ID"] = myNewlastIDGlobalChipCtrl + 1;
        //                myNewlastIDGlobalChipCtrl++;
        //            }
        //        }

        //        for (int i = 0; i < MainForm.GlobalDS.Tables["GlobalManufactureChipsetInitialize"].Rows.Count; i++)
        //        {
        //            if ((MainForm.GlobalDS.Tables["GlobalManufactureChipsetInitialize"].Rows[i].RowState == DataRowState.Added)
        //                && (Convert.ToInt64(MainForm.GlobalDS.Tables["GlobalManufactureChipsetInitialize"].Rows[i]["ID"]) > MainForm.origIDChipInit)
        //               )
        //            {
        //                MainForm.GlobalDS.Tables["GlobalManufactureChipsetInitialize"].Rows[i]["ID"] = myNewlastIDGlobalChipInit + 1;
        //                myNewlastIDGlobalChipInit++;
        //            }
        //        }

        //        for (int i = 0; i < MainForm.GlobalDS.Tables["GlobalMSAEEPROMInitialize"].Rows.Count; i++)
        //        {
        //            if ((MainForm.GlobalDS.Tables["GlobalMSAEEPROMInitialize"].Rows[i].RowState == DataRowState.Added)
        //                && (Convert.ToInt64(MainForm.GlobalDS.Tables["GlobalMSAEEPROMInitialize"].Rows[i]["ID"]) > MainForm.origIDEEPROMInit)
        //               )
        //            {
        //                MainForm.GlobalDS.Tables["GlobalMSAEEPROMInitialize"].Rows[i]["ID"] = myNewlastIDGlobalEEPROMInit + 1;
        //                myNewlastIDGlobalEEPROMInit++;
        //            }
        //        }

        //        //140706_0<<<<<<<<<<<<<<<<<<


        //        for (int i = 0; i < MainForm.GlobalDS.Tables["GlobalProductionType"].Rows.Count; i++)
        //        {
        //            if ((MainForm.GlobalDS.Tables["GlobalProductionType"].Rows[i].RowState == DataRowState.Added)
        //                && (Convert.ToInt64(MainForm.GlobalDS.Tables["GlobalProductionType"].Rows[i]["ID"]) > MainForm.origIDGlobalType)
        //               )
        //            {
        //                MainForm.GlobalDS.Tables["GlobalProductionType"].Rows[i]["ID"] = myNewlastIDGlobalType + 1;
        //                myNewlastIDGlobalType++;
        //            }
        //        }



        //        for (int i = 0; i < MainForm.GlobalDS.Tables["GlobalProductionName"].Rows.Count; i++)
        //        {
        //            if ((MainForm.GlobalDS.Tables["GlobalProductionName"].Rows[i].RowState == DataRowState.Added)
        //                && (Convert.ToInt64(MainForm.GlobalDS.Tables[2].Rows[i]["ID"]) > MainForm.origIDGlobalPN))
        //            {
        //                MainForm.GlobalDS.Tables["GlobalProductionName"].Rows[i]["ID"] = myNewlastIDGlobalPN + 1;
        //                myNewlastIDGlobalPN++;
        //            }
        //        }



        //        for (int i = 0; i < MainForm.GlobalDS.Tables["GlobalMSA"].Rows.Count; i++)
        //        {
        //            if ((MainForm.GlobalDS.Tables["GlobalMSA"].Rows[i].RowState == DataRowState.Added)
        //                && (Convert.ToInt64(MainForm.GlobalDS.Tables["GlobalMSA"].Rows[i]["ID"]) > MainForm.origIDGlobalMSA)
        //               )
        //            {
        //                MainForm.GlobalDS.Tables["GlobalMSA"].Rows[i]["ID"] = myNewlastIDGlobalMSA + 1;
        //                myNewlastIDGlobalMSA++;
        //            }
        //        }



        //        for (int i = 0; i < MainForm.GlobalDS.Tables["GlobalMSADefintionInf"].Rows.Count; i++)
        //        {
        //            if ((MainForm.GlobalDS.Tables["GlobalMSADefintionInf"].Rows[i].RowState == DataRowState.Added)
        //                && (Convert.ToInt64(MainForm.GlobalDS.Tables["GlobalMSADefintionInf"].Rows[i]["ID"]) > MainForm.origIDGlobalMSAPrmtr)
        //               )
        //            {
        //                MainForm.GlobalDS.Tables["GlobalMSADefintionInf"].Rows[i]["ID"] = myNewlastIDGlobalMSAPrmtr + 1;
        //                myNewlastIDGlobalMSAPrmtr++;
        //            }
        //        }
        //        //140704>>>GlobalManufactureCoefficientsGroup
        //        for (int i = 0; i < MainForm.GlobalDS.Tables["GlobalManufactureCoefficientsGroup"].Rows.Count; i++)
        //        {
        //            if ((MainForm.GlobalDS.Tables["GlobalManufactureCoefficientsGroup"].Rows[i].RowState == DataRowState.Added)
        //                && (Convert.ToInt64(MainForm.GlobalDS.Tables["GlobalManufactureCoefficientsGroup"].Rows[i]["ID"]) > MainForm.origIDGlobalMCoefGroup)
        //               )
        //            {
        //                MainForm.GlobalDS.Tables["GlobalManufactureCoefficientsGroup"].Rows[i]["ID"] = myNewlastIDGlobalMCoef + 1;
        //                myNewlastIDGlobalMCoef++;
        //            }
        //        }
        //        //140704>>>GlobalManufactureCoefficients
        //        for (int i = 0; i < MainForm.GlobalDS.Tables["GlobalManufactureCoefficients"].Rows.Count; i++)
        //        {
        //            if ((MainForm.GlobalDS.Tables["GlobalManufactureCoefficients"].Rows[i].RowState == DataRowState.Added)
        //                && (Convert.ToInt64(MainForm.GlobalDS.Tables["GlobalManufactureCoefficients"].Rows[i]["ID"]) > MainForm.origIDMCoefPrmtr)
        //               )
        //            {
        //                MainForm.GlobalDS.Tables["GlobalManufactureCoefficients"].Rows[i]["ID"] = myNewlastIDGlobalMCoefPrmtr + 1;
        //                myNewlastIDGlobalMCoefPrmtr++;
        //            }
        //        }


        //        dt1 = MainForm.GlobalDS.Tables["GlobalProductionType"];
        //        dt2 = MainForm.GlobalDS.Tables["GlobalProductionName"];

        //        dt3 = MainForm.GlobalDS.Tables["GlobalMSA"];
        //        dt4 = MainForm.GlobalDS.Tables["GlobalMSADefintionInf"];

        //        //140704>>>
        //        dt5 = MainForm.GlobalDS.Tables["GlobalManufactureCoefficientsGroup"];
        //        dt6 = MainForm.GlobalDS.Tables["GlobalManufactureCoefficients"];


        //        //140707_0 >>>>>>>>>>>>>>
        //        dt10 = MainForm.GlobalDS.Tables["GlobalAllEquipmentList"];
        //        dt11 = MainForm.GlobalDS.Tables["GlobalAllEquipmentParamterList"];


        //        //140707_1 >>>>>>>>>>>>>>
        //        dt12 = MainForm.GlobalDS.Tables["GlobalAllAppModelList"];
        //        dt13 = MainForm.GlobalDS.Tables["GlobalAllTestModelList"];
        //        dt14 = MainForm.GlobalDS.Tables["GlobalTestModelParamterList"];

        //        DataTable dtAdded10 = new DataTable(); //存放主表 新增 的数据
        //        DataTable dtMidified10 = new DataTable(); //存储主表 编辑 的行记录
        //        DataTable dtDeleted10 = new DataTable(); //存储主表 删除 的行记录

        //        dtDeleted10 = dt10.GetChanges(DataRowState.Deleted);
        //        dtMidified10 = dt10.GetChanges(DataRowState.Modified);
        //        dtAdded10 = dt10.GetChanges(DataRowState.Added);

        //        DataTable dtAdded11 = new DataTable(); //存放主表 新增 的数据
        //        DataTable dtMidified11 = new DataTable(); //存储主表 编辑 的行记录
        //        DataTable dtDeleted11 = new DataTable(); //存储主表 删除 的行记录

        //        dtDeleted11 = dt11.GetChanges(DataRowState.Deleted);
        //        dtMidified11 = dt11.GetChanges(DataRowState.Modified);
        //        dtAdded11 = dt11.GetChanges(DataRowState.Added);
        //        //140707_0 <<<<<<<<<<<<<<

        //        DataTable dtAdded12 = new DataTable(); //存放主表 新增 的数据
        //        DataTable dtMidified12 = new DataTable(); //存储主表 编辑 的行记录
        //        DataTable dtDeleted12 = new DataTable(); //存储主表 删除 的行记录

        //        dtDeleted12 = dt12.GetChanges(DataRowState.Deleted);
        //        dtMidified12 = dt12.GetChanges(DataRowState.Modified);
        //        dtAdded12 = dt12.GetChanges(DataRowState.Added);

        //        DataTable dtAdded13 = new DataTable(); //存放主表 新增 的数据
        //        DataTable dtMidified13 = new DataTable(); //存储主表 编辑 的行记录
        //        DataTable dtDeleted13 = new DataTable(); //存储主表 删除 的行记录

        //        dtDeleted13 = dt13.GetChanges(DataRowState.Deleted);
        //        dtMidified13 = dt13.GetChanges(DataRowState.Modified);
        //        dtAdded13 = dt13.GetChanges(DataRowState.Added);

        //        DataTable dtAdded14 = new DataTable(); //存放主表 新增 的数据
        //        DataTable dtMidified14 = new DataTable(); //存储主表 编辑 的行记录
        //        DataTable dtDeleted14 = new DataTable(); //存储主表 删除 的行记录

        //        dtDeleted14 = dt14.GetChanges(DataRowState.Deleted);
        //        dtMidified14 = dt14.GetChanges(DataRowState.Modified);
        //        dtAdded14 = dt14.GetChanges(DataRowState.Added);
        //        //140707_1 <<<<<<<<<<<<<<

        //        //140706_0>>>>>>>>>>>>>>>>>> 均为 GlobalProductionName的子表项目
        //        dt7 = MainForm.GlobalDS.Tables["GlobalManufactureChipsetControl"];
        //        dt8 = MainForm.GlobalDS.Tables["GlobalManufactureChipsetInitialize"];
        //        dt9 = MainForm.GlobalDS.Tables["GlobalMSAEEPROMInitialize"];

        //        DataTable dtAdded7 = new DataTable(); //存放主表 新增 的数据
        //        DataTable dtMidified7 = new DataTable(); //存储主表 编辑 的行记录
        //        DataTable dtDeleted7 = new DataTable(); //存储主表 删除 的行记录

        //        dtDeleted7 = dt7.GetChanges(DataRowState.Deleted);
        //        dtMidified7 = dt7.GetChanges(DataRowState.Modified);
        //        dtAdded7 = dt7.GetChanges(DataRowState.Added);

        //        DataTable dtAdded8 = new DataTable(); //存放主表 新增 的数据
        //        DataTable dtMidified8 = new DataTable(); //存储主表 编辑 的行记录
        //        DataTable dtDeleted8 = new DataTable(); //存储主表 删除 的行记录

        //        dtDeleted8 = dt8.GetChanges(DataRowState.Deleted);
        //        dtMidified8 = dt8.GetChanges(DataRowState.Modified);
        //        dtAdded8 = dt8.GetChanges(DataRowState.Added);

        //        DataTable dtAdded9 = new DataTable(); //存放主表 新增 的数据
        //        DataTable dtMidified9 = new DataTable(); //存储主表 编辑 的行记录
        //        DataTable dtDeleted9 = new DataTable(); //存储主表 删除 的行记录

        //        dtDeleted9 = dt9.GetChanges(DataRowState.Deleted);
        //        dtMidified9 = dt9.GetChanges(DataRowState.Modified);
        //        dtAdded9 = dt9.GetChanges(DataRowState.Added);
        //        //140706_0<<<<<<<<<<<<<<<<<<

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

        //        //===============================================================
        //        //删除资料开始
        //        //此处必须注意删除顺序 由子表删除后再删除主表
        //        //GlobalProductionName-->GlobalProductionType
        //        //GlobalMSADefintionInf-->GlobalManufactureCoefficientsGroup
        //        //GlobalMSADefintionInf-->GlobalMSA
        //        //dt1 = MainForm.GlobalDS.Tables["GlobalProductionType"];
        //        //dt2 = MainForm.GlobalDS.Tables["GlobalProductionName"];
        //        //dt3 = MainForm.GlobalDS.Tables["GlobalMSA"];
        //        //dt4 = MainForm.GlobalDS.Tables["GlobalMSADefintionInf"];                
        //        //dt5 = MainForm.GlobalDS.Tables["GlobalManufactureCoefficientsGroup"];
        //        //dt6 = MainForm.GlobalDS.Tables["GlobalManufactureCoefficients"];


        //        //140706_0>>>>>>>>>>>>>>>>>> 均为 GlobalProductionName的子表项目
        //        if (dtDeleted7 != null) da7.Update(dtDeleted7);
        //        if (dtDeleted8 != null) da8.Update(dtDeleted8);
        //        if (dtDeleted9 != null) da9.Update(dtDeleted9);
        //        //140706_0<<<<<<<<<<<<<<<<<< 

        //        //140707_0>>>>>>>>>>>>>>>>>>  //设备资料 独立          
        //        if (dtDeleted10 != null) da10.Update(dtDeleted10);
        //        if (dtDeleted11 != null) da11.Update(dtDeleted11);
        //        //140707_0<<<<<<<<<<<<<<<<<< 

        //        //140707_0>>>>>>>>>>>>>>>>>>  //APP+Model+Prmtr资料 独立               
        //        if (dtDeleted14 != null) da14.Update(dtDeleted14);
        //        if (dtDeleted13 != null) da13.Update(dtDeleted13);
        //        if (dtDeleted12 != null) da12.Update(dtDeleted12);
        //        //140707_0<<<<<<<<<<<<<<<<<< 


        //        //Type + PN
        //        if (dtDeleted2 != null) da2.Update(dtDeleted2);
        //        if (dtDeleted1 != null) da1.Update(dtDeleted1);
        //        if (dtDeleted4 != null) da4.Update(dtDeleted4);
        //        if (dtDeleted3 != null) da3.Update(dtDeleted3);
        //        if (dtDeleted6 != null) da6.Update(dtDeleted6);
        //        if (dtDeleted5 != null) da5.Update(dtDeleted5);
        //        //删除资料结束
        //        //===============================================================


        //        //===============================================================
        //        //新增资料开始
        //        //此处必须注意更新顺序 由主表-->子表
        //        if (dtAdded5 != null) da5.Update(dtAdded5);
        //        if (dtMidified5 != null) da5.Update(dtMidified5);

        //        if (dtAdded6 != null) da6.Update(dtAdded6);
        //        if (dtMidified6 != null) da6.Update(dtMidified6);

        //        if (dtAdded3 != null) da3.Update(dtAdded3);
        //        if (dtMidified3 != null) da3.Update(dtMidified3);

        //        if (dtAdded4 != null) da4.Update(dtAdded4);
        //        if (dtMidified4 != null) da4.Update(dtMidified4);
        //        if (dtAdded1 != null) da1.Update(dtAdded1);
        //        if (dtMidified1 != null) da1.Update(dtMidified1);

        //        if (dtAdded2 != null) da2.Update(dtAdded2);
        //        if (dtMidified2 != null) da2.Update(dtMidified2);

        //        //140706_0>>>>>>>>>>>>>>>>>> 均为 GlobalProductionName的子表项目
        //        if (dtAdded7 != null) da7.Update(dtAdded7);
        //        if (dtMidified7 != null) da7.Update(dtMidified7);

        //        if (dtAdded8 != null) da8.Update(dtAdded8);
        //        if (dtMidified8 != null) da8.Update(dtMidified8);

        //        if (dtAdded9 != null) da9.Update(dtAdded9);
        //        if (dtMidified9 != null) da9.Update(dtMidified9);
        //        //140706_0<<<<<<<<<<<<<<<<<< 

        //        //140707_0<<<<<<<<<<<<<<<<<< 
        //        if (dtAdded10 != null) da10.Update(dtAdded10);
        //        if (dtMidified10 != null) da10.Update(dtMidified10);

        //        if (dtAdded11 != null) da11.Update(dtAdded11);
        //        if (dtMidified11 != null) da11.Update(dtMidified11);
        //        //140707_0<<<<<<<<<<<<<<<<<< 

        //        //140707_0<<<<<<<<<<<<<<<<<< 
        //        if (dtAdded12 != null) da12.Update(dtAdded12);
        //        if (dtMidified12 != null) da12.Update(dtMidified12);

        //        if (dtAdded13 != null) da13.Update(dtAdded13);
        //        if (dtMidified13 != null) da13.Update(dtMidified13);

        //        if (dtAdded14 != null) da14.Update(dtAdded14);
        //        if (dtMidified14 != null) da14.Update(dtMidified14);
        //        //新增资料结束
        //        //===============================================================

        //        //140707_0<<<<<<<<<<<<<<<<<< 
        //        tr.Commit();
        //        myOPresult = true;

        //        return myOPresult;
        //    }
        //    catch (Exception ex)
        //    {
        //        try
        //        {
        //            MessageBox.Show(ex.ToString());
        //            tr.Rollback();
        //            if (conn.State.ToString().ToUpper() == "OPEN") conn.Close();
        //        }
        //        catch (System.Exception TransactionEx)
        //        {
        //            //Handle Exception
        //            MessageBox.Show(TransactionEx.ToString());
        //            if (conn.State.ToString().ToUpper() == "OPEN") conn.Close();
        //        }
        //        return myOPresult;
        //    }
        //    finally
        //    {
        //        if (conn.State.ToString().ToUpper() == "OPEN")
        //            conn.Close();

        //    }
        //}

        //140612_1
        public override bool UpdateDT()
        {
            bool myOPresult = false;

            long myNewlastIDGlobalType = GetLastInsertData("GlobalProductionType");
            long myNewlastIDGlobalPN = GetLastInsertData("GlobalProductionName");
            long myNewlastIDGlobalMSA = GetLastInsertData("GlobalMSA");
            long myNewlastIDGlobalMSAPrmtr = GetLastInsertData("GlobalMSADefintionInf");

            long myNewlastIDGlobalMCoef = GetLastInsertData("GlobalManufactureCoefficientsGroup");  //140704_4
            long myNewlastIDGlobalMCoefPrmtr = GetLastInsertData("GlobalManufactureCoefficients");  //140704_4

            long myNewlastIDGlobalChipCtrl = GetLastInsertData("GlobalManufactureChipsetControl");  //140706_0
            long myNewlastIDGlobalChipInit = GetLastInsertData("GlobalManufactureChipsetInitialize");//140706_0
            long myNewlastIDGlobalEEPROMInit = GetLastInsertData("GlobalMSAEEPROMInitialize");      //140706_0

            long myNewlastIDTestEquip = GetLastInsertData("GlobalAllEquipmentList");                //140707_0
            long myNewlastIDTestEquipPrmtr = GetLastInsertData("GlobalAllEquipmentParamterList");   //140707_0

            long myNewlastIDGlobalAPP = GetLastInsertData("GlobalAllAppModelList");                 //140707_1
            long myNewlastIDTestModel = GetLastInsertData("GlobalAllTestModelList");                //140707_1
            long myNewlastIDTestPrmtr = GetLastInsertData("GlobalTestModelParamterList");           //140707_1


            if (conn == null) conn.Open();
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }

            System.Data.OleDb.OleDbTransaction tr;
            tr = conn.BeginTransaction(IsolationLevel.ReadCommitted);

            try
            {

                OleDbCommand cm1 = new OleDbCommand("select * from  " + "GlobalProductionType", conn);
                OleDbDataAdapter da1 = new OleDbDataAdapter(cm1);
                OleDbCommandBuilder cb1 = new OleDbCommandBuilder(da1);
                DataTable dt1 = new DataTable();
                da1.SelectCommand.Transaction = tr;
                da1.Fill(dt1);

                OleDbCommand cm2 = new OleDbCommand("select * from  " + "GlobalProductionName", conn);
                OleDbDataAdapter da2 = new OleDbDataAdapter(cm2);
                OleDbCommandBuilder cb2 = new OleDbCommandBuilder(da2);
                DataTable dt2 = new DataTable();
                da2.SelectCommand.Transaction = tr;
                da2.Fill(dt2);

                OleDbCommand cm3 = new OleDbCommand("select * from  " + "GlobalMSA", conn);
                OleDbDataAdapter da3 = new OleDbDataAdapter(cm3);
                OleDbCommandBuilder cb3 = new OleDbCommandBuilder(da3);
                DataTable dt3 = new DataTable();
                da3.SelectCommand.Transaction = tr;
                da3.Fill(dt3);

                OleDbCommand cm4 = new OleDbCommand("select * from  " + "GlobalMSADefintionInf", conn);
                OleDbDataAdapter da4 = new OleDbDataAdapter(cm4);
                OleDbCommandBuilder cb4 = new OleDbCommandBuilder(da4);
                DataTable dt4 = new DataTable();
                da4.SelectCommand.Transaction = tr;
                da4.Fill(dt4);

                //140704>>>GlobalManufactureCoefficientsGroup
                OleDbCommand cm5 = new OleDbCommand("select * from  " + "GlobalManufactureCoefficientsGroup", conn);
                OleDbDataAdapter da5 = new OleDbDataAdapter(cm5);
                OleDbCommandBuilder cb5 = new OleDbCommandBuilder(da5);
                DataTable dt5 = new DataTable();
                da5.SelectCommand.Transaction = tr;
                da5.Fill(dt5);

                //140704>>>GlobalManufactureCoefficients
                OleDbCommand cm6 = new OleDbCommand("select * from  " + "GlobalManufactureCoefficients", conn);
                OleDbDataAdapter da6 = new OleDbDataAdapter(cm6);
                OleDbCommandBuilder cb6 = new OleDbCommandBuilder(da6);
                DataTable dt6 = new DataTable();
                da6.SelectCommand.Transaction = tr;
                da6.Fill(dt6);

                //140706_0>>>>>>>>>>>>>>>>>>                
                OleDbCommand cm7 = new OleDbCommand("select * from  " + "GlobalManufactureChipsetControl", conn);
                OleDbDataAdapter da7 = new OleDbDataAdapter(cm7);
                OleDbCommandBuilder cb7 = new OleDbCommandBuilder(da7);
                DataTable dt7 = new DataTable();
                da7.SelectCommand.Transaction = tr;
                da7.Fill(dt7);

                OleDbCommand cm8 = new OleDbCommand("select * from  " + "GlobalManufactureChipsetInitialize", conn);
                OleDbDataAdapter da8 = new OleDbDataAdapter(cm8);
                OleDbCommandBuilder cb8 = new OleDbCommandBuilder(da8);
                DataTable dt8 = new DataTable();
                da8.SelectCommand.Transaction = tr;
                da8.Fill(dt8);

                OleDbCommand cm9 = new OleDbCommand("select * from  " + "GlobalMSAEEPROMInitialize", conn);
                OleDbDataAdapter da9 = new OleDbDataAdapter(cm9);
                OleDbCommandBuilder cb9 = new OleDbCommandBuilder(da9);
                DataTable dt9 = new DataTable();
                da9.SelectCommand.Transaction = tr;
                da9.Fill(dt9);

                OleDbCommand cm10 = new OleDbCommand("select * from  " + "GlobalAllEquipmentList", conn);
                OleDbDataAdapter da10 = new OleDbDataAdapter(cm10);
                OleDbCommandBuilder cb10 = new OleDbCommandBuilder(da10);
                DataTable dt10 = new DataTable();
                da10.SelectCommand.Transaction = tr;
                da10.Fill(dt10);

                OleDbCommand cm11 = new OleDbCommand("select * from  " + "GlobalAllEquipmentParamterList", conn);
                OleDbDataAdapter da11 = new OleDbDataAdapter(cm11);
                OleDbCommandBuilder cb11 = new OleDbCommandBuilder(da11);
                DataTable dt11 = new DataTable();
                da11.SelectCommand.Transaction = tr;
                da11.Fill(dt11);


                OleDbCommand cm12 = new OleDbCommand("select * from  " + "GlobalAllAppModelList", conn);
                OleDbDataAdapter da12 = new OleDbDataAdapter(cm12);
                OleDbCommandBuilder cb12 = new OleDbCommandBuilder(da12);
                DataTable dt12 = new DataTable();
                da12.SelectCommand.Transaction = tr;
                da12.Fill(dt12);

                OleDbCommand cm13 = new OleDbCommand("select * from  " + "GlobalAllTestModelList", conn);
                OleDbDataAdapter da13 = new OleDbDataAdapter(cm13);
                OleDbCommandBuilder cb13 = new OleDbCommandBuilder(da13);
                DataTable dt13 = new DataTable();
                da13.SelectCommand.Transaction = tr;
                da13.Fill(dt13);

                OleDbCommand cm14 = new OleDbCommand("select * from  " + "GlobalTestModelParamterList", conn);
                OleDbDataAdapter da14 = new OleDbDataAdapter(cm14);
                OleDbCommandBuilder cb14 = new OleDbCommandBuilder(da14);
                DataTable dt14 = new DataTable();
                da14.SelectCommand.Transaction = tr;
                da14.Fill(dt14);

                //140707_0>>>>>>>>>>>>>>>>>>>>>
                //myNewlastIDTestEquip  myNewlastIDTestEquipPrmtr [GlobalAllEquipmentList][GlobalAllEquipmentParamterList]   
                for (int i = 0; i < MainForm.GlobalDS.Tables["GlobalAllEquipmentList"].Rows.Count; i++)
                {
                    if ((MainForm.GlobalDS.Tables["GlobalAllEquipmentList"].Rows[i].RowState == DataRowState.Added)
                        && (Convert.ToInt64(MainForm.GlobalDS.Tables["GlobalAllEquipmentList"].Rows[i]["ID"]) > MainForm.origIDGlobalEquip)
                       )
                    {
                        MainForm.GlobalDS.Tables["GlobalAllEquipmentList"].Rows[i]["ID"] = myNewlastIDTestEquip + 1;
                        myNewlastIDTestEquip++;
                    }
                }

                for (int i = 0; i < MainForm.GlobalDS.Tables["GlobalAllEquipmentParamterList"].Rows.Count; i++)
                {
                    if ((MainForm.GlobalDS.Tables["GlobalAllEquipmentParamterList"].Rows[i].RowState == DataRowState.Added)
                        && (Convert.ToInt64(MainForm.GlobalDS.Tables["GlobalAllEquipmentParamterList"].Rows[i]["ID"]) > MainForm.origIDGlobalEquipPrmtr)
                       )
                    {
                        MainForm.GlobalDS.Tables["GlobalAllEquipmentParamterList"].Rows[i]["ID"] = myNewlastIDTestEquipPrmtr + 1;
                        myNewlastIDTestEquipPrmtr++;
                    }
                }
                //140707_0<<<<<<<<<<<<<<<<<<

                //140707_1>>>>>>>>>>>>>>>>>>>>>  
                for (int i = 0; i < MainForm.GlobalDS.Tables["GlobalAllAppModelList"].Rows.Count; i++)
                {
                    if ((MainForm.GlobalDS.Tables["GlobalAllAppModelList"].Rows[i].RowState == DataRowState.Added)  //140917_1 FIX BUG
                        && (Convert.ToInt64(MainForm.GlobalDS.Tables["GlobalAllAppModelList"].Rows[i]["ID"]) > MainForm.origIDGlobalAPP)    //140917_1 FIX BUG
                       )
                    {
                        MainForm.GlobalDS.Tables["GlobalAllAppModelList"].Rows[i]["ID"] = myNewlastIDGlobalAPP + 1; //140917_1 FIX BUG
                        myNewlastIDGlobalAPP++;
                    }
                }

                for (int i = 0; i < MainForm.GlobalDS.Tables["GlobalAllTestModelList"].Rows.Count; i++)
                {
                    if ((MainForm.GlobalDS.Tables["GlobalAllTestModelList"].Rows[i].RowState == DataRowState.Added)
                        && (Convert.ToInt64(MainForm.GlobalDS.Tables["GlobalAllTestModelList"].Rows[i]["ID"]) > MainForm.origIDGlobalModel)
                       )
                    {
                        MainForm.GlobalDS.Tables["GlobalAllTestModelList"].Rows[i]["ID"] = myNewlastIDTestModel + 1;
                        myNewlastIDTestModel++;
                    }
                }

                for (int i = 0; i < MainForm.GlobalDS.Tables["GlobalTestModelParamterList"].Rows.Count; i++)
                {
                    if ((MainForm.GlobalDS.Tables["GlobalTestModelParamterList"].Rows[i].RowState == DataRowState.Added)
                        && (Convert.ToInt64(MainForm.GlobalDS.Tables["GlobalTestModelParamterList"].Rows[i]["ID"]) > MainForm.origIDGlobalPrmtr)
                       )
                    {
                        MainForm.GlobalDS.Tables["GlobalTestModelParamterList"].Rows[i]["ID"] = myNewlastIDTestPrmtr + 1;
                        myNewlastIDTestPrmtr++;
                    }
                }
                //140707_1<<<<<<<<<<<<<<<<<<

                //myNewlastIDGlobalChipCtrl   myNewlastIDGlobalChipInit   myNewlastIDGlobalEEPROMInit
                for (int i = 0; i < MainForm.GlobalDS.Tables["GlobalManufactureChipsetControl"].Rows.Count; i++)
                {
                    if ((MainForm.GlobalDS.Tables["GlobalManufactureChipsetControl"].Rows[i].RowState == DataRowState.Added)
                        && (Convert.ToInt64(MainForm.GlobalDS.Tables["GlobalManufactureChipsetControl"].Rows[i]["ID"]) > MainForm.origIDChipCtrl)
                       )
                    {
                        MainForm.GlobalDS.Tables["GlobalManufactureChipsetControl"].Rows[i]["ID"] = myNewlastIDGlobalChipCtrl + 1;
                        myNewlastIDGlobalChipCtrl++;
                    }
                }

                for (int i = 0; i < MainForm.GlobalDS.Tables["GlobalManufactureChipsetInitialize"].Rows.Count; i++)
                {
                    if ((MainForm.GlobalDS.Tables["GlobalManufactureChipsetInitialize"].Rows[i].RowState == DataRowState.Added)
                        && (Convert.ToInt64(MainForm.GlobalDS.Tables["GlobalManufactureChipsetInitialize"].Rows[i]["ID"]) > MainForm.origIDChipInit)
                       )
                    {
                        MainForm.GlobalDS.Tables["GlobalManufactureChipsetInitialize"].Rows[i]["ID"] = myNewlastIDGlobalChipInit + 1;
                        myNewlastIDGlobalChipInit++;
                    }
                }

                for (int i = 0; i < MainForm.GlobalDS.Tables["GlobalMSAEEPROMInitialize"].Rows.Count; i++)
                {
                    if ((MainForm.GlobalDS.Tables["GlobalMSAEEPROMInitialize"].Rows[i].RowState == DataRowState.Added)
                        && (Convert.ToInt64(MainForm.GlobalDS.Tables["GlobalMSAEEPROMInitialize"].Rows[i]["ID"]) > MainForm.origIDEEPROMInit)
                       )
                    {
                        MainForm.GlobalDS.Tables["GlobalMSAEEPROMInitialize"].Rows[i]["ID"] = myNewlastIDGlobalEEPROMInit + 1;
                        myNewlastIDGlobalEEPROMInit++;
                    }
                }

                //140706_0<<<<<<<<<<<<<<<<<<


                for (int i = 0; i < MainForm.GlobalDS.Tables["GlobalProductionType"].Rows.Count; i++)
                {
                    if ((MainForm.GlobalDS.Tables["GlobalProductionType"].Rows[i].RowState == DataRowState.Added)
                        && (Convert.ToInt64(MainForm.GlobalDS.Tables["GlobalProductionType"].Rows[i]["ID"]) > MainForm.origIDGlobalType)
                       )
                    {
                        MainForm.GlobalDS.Tables["GlobalProductionType"].Rows[i]["ID"] = myNewlastIDGlobalType + 1;
                        myNewlastIDGlobalType++;
                    }
                }



                for (int i = 0; i < MainForm.GlobalDS.Tables["GlobalProductionName"].Rows.Count; i++)
                {
                    if ((MainForm.GlobalDS.Tables["GlobalProductionName"].Rows[i].RowState == DataRowState.Added)
                        && (Convert.ToInt64(MainForm.GlobalDS.Tables[2].Rows[i]["ID"]) > MainForm.origIDGlobalPN))
                    {
                        MainForm.GlobalDS.Tables["GlobalProductionName"].Rows[i]["ID"] = myNewlastIDGlobalPN + 1;
                        myNewlastIDGlobalPN++;
                    }
                }



                for (int i = 0; i < MainForm.GlobalDS.Tables["GlobalMSA"].Rows.Count; i++)
                {
                    if ((MainForm.GlobalDS.Tables["GlobalMSA"].Rows[i].RowState == DataRowState.Added)
                        && (Convert.ToInt64(MainForm.GlobalDS.Tables["GlobalMSA"].Rows[i]["ID"]) > MainForm.origIDGlobalMSA)
                       )
                    {
                        MainForm.GlobalDS.Tables["GlobalMSA"].Rows[i]["ID"] = myNewlastIDGlobalMSA + 1;
                        myNewlastIDGlobalMSA++;
                    }
                }



                for (int i = 0; i < MainForm.GlobalDS.Tables["GlobalMSADefintionInf"].Rows.Count; i++)
                {
                    if ((MainForm.GlobalDS.Tables["GlobalMSADefintionInf"].Rows[i].RowState == DataRowState.Added)
                        && (Convert.ToInt64(MainForm.GlobalDS.Tables["GlobalMSADefintionInf"].Rows[i]["ID"]) > MainForm.origIDGlobalMSAPrmtr)
                       )
                    {
                        MainForm.GlobalDS.Tables["GlobalMSADefintionInf"].Rows[i]["ID"] = myNewlastIDGlobalMSAPrmtr + 1;
                        myNewlastIDGlobalMSAPrmtr++;
                    }
                }
                //140704>>>GlobalManufactureCoefficientsGroup
                for (int i = 0; i < MainForm.GlobalDS.Tables["GlobalManufactureCoefficientsGroup"].Rows.Count; i++)
                {
                    if ((MainForm.GlobalDS.Tables["GlobalManufactureCoefficientsGroup"].Rows[i].RowState == DataRowState.Added)
                        && (Convert.ToInt64(MainForm.GlobalDS.Tables["GlobalManufactureCoefficientsGroup"].Rows[i]["ID"]) > MainForm.origIDGlobalMCoefGroup)
                       )
                    {
                        MainForm.GlobalDS.Tables["GlobalManufactureCoefficientsGroup"].Rows[i]["ID"] = myNewlastIDGlobalMCoef + 1;
                        myNewlastIDGlobalMCoef++;
                    }
                }
                //140704>>>GlobalManufactureCoefficients
                for (int i = 0; i < MainForm.GlobalDS.Tables["GlobalManufactureCoefficients"].Rows.Count; i++)
                {
                    if ((MainForm.GlobalDS.Tables["GlobalManufactureCoefficients"].Rows[i].RowState == DataRowState.Added)
                        && (Convert.ToInt64(MainForm.GlobalDS.Tables["GlobalManufactureCoefficients"].Rows[i]["ID"]) > MainForm.origIDMCoefPrmtr)
                       )
                    {
                        MainForm.GlobalDS.Tables["GlobalManufactureCoefficients"].Rows[i]["ID"] = myNewlastIDGlobalMCoefPrmtr + 1;
                        myNewlastIDGlobalMCoefPrmtr++;
                    }
                }


                dt1 = MainForm.GlobalDS.Tables["GlobalProductionType"];
                dt2 = MainForm.GlobalDS.Tables["GlobalProductionName"];

                dt3 = MainForm.GlobalDS.Tables["GlobalMSA"];
                dt4 = MainForm.GlobalDS.Tables["GlobalMSADefintionInf"];

                //140704>>>
                dt5 = MainForm.GlobalDS.Tables["GlobalManufactureCoefficientsGroup"];
                dt6 = MainForm.GlobalDS.Tables["GlobalManufactureCoefficients"];


                //140707_0 >>>>>>>>>>>>>>
                dt10 = MainForm.GlobalDS.Tables["GlobalAllEquipmentList"];
                dt11 = MainForm.GlobalDS.Tables["GlobalAllEquipmentParamterList"];


                //140707_1 >>>>>>>>>>>>>>
                dt12 = MainForm.GlobalDS.Tables["GlobalAllAppModelList"];
                dt13 = MainForm.GlobalDS.Tables["GlobalAllTestModelList"];
                dt14 = MainForm.GlobalDS.Tables["GlobalTestModelParamterList"];

                DataTable dtAdded10 = new DataTable(); //存放主表 新增 的数据
                DataTable dtMidified10 = new DataTable(); //存储主表 编辑 的行记录
                DataTable dtDeleted10 = new DataTable(); //存储主表 删除 的行记录

                dtDeleted10 = dt10.GetChanges(DataRowState.Deleted);
                dtMidified10 = dt10.GetChanges(DataRowState.Modified);
                dtAdded10 = dt10.GetChanges(DataRowState.Added);

                DataTable dtAdded11 = new DataTable(); //存放主表 新增 的数据
                DataTable dtMidified11 = new DataTable(); //存储主表 编辑 的行记录
                DataTable dtDeleted11 = new DataTable(); //存储主表 删除 的行记录

                dtDeleted11 = dt11.GetChanges(DataRowState.Deleted);
                dtMidified11 = dt11.GetChanges(DataRowState.Modified);
                dtAdded11 = dt11.GetChanges(DataRowState.Added);
                //140707_0 <<<<<<<<<<<<<<

                DataTable dtAdded12 = new DataTable(); //存放主表 新增 的数据
                DataTable dtMidified12 = new DataTable(); //存储主表 编辑 的行记录
                DataTable dtDeleted12 = new DataTable(); //存储主表 删除 的行记录

                dtDeleted12 = dt12.GetChanges(DataRowState.Deleted);
                dtMidified12 = dt12.GetChanges(DataRowState.Modified);
                dtAdded12 = dt12.GetChanges(DataRowState.Added);

                DataTable dtAdded13 = new DataTable(); //存放主表 新增 的数据
                DataTable dtMidified13 = new DataTable(); //存储主表 编辑 的行记录
                DataTable dtDeleted13 = new DataTable(); //存储主表 删除 的行记录

                dtDeleted13 = dt13.GetChanges(DataRowState.Deleted);
                dtMidified13 = dt13.GetChanges(DataRowState.Modified);
                dtAdded13 = dt13.GetChanges(DataRowState.Added);

                DataTable dtAdded14 = new DataTable(); //存放主表 新增 的数据
                DataTable dtMidified14 = new DataTable(); //存储主表 编辑 的行记录
                DataTable dtDeleted14 = new DataTable(); //存储主表 删除 的行记录

                dtDeleted14 = dt14.GetChanges(DataRowState.Deleted);
                dtMidified14 = dt14.GetChanges(DataRowState.Modified);
                dtAdded14 = dt14.GetChanges(DataRowState.Added);
                //140707_1 <<<<<<<<<<<<<<

                //140706_0>>>>>>>>>>>>>>>>>> 均为 GlobalProductionName的子表项目
                dt7 = MainForm.GlobalDS.Tables["GlobalManufactureChipsetControl"];
                dt8 = MainForm.GlobalDS.Tables["GlobalManufactureChipsetInitialize"];
                dt9 = MainForm.GlobalDS.Tables["GlobalMSAEEPROMInitialize"];

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

                DataTable dtAdded9 = new DataTable(); //存放主表 新增 的数据
                DataTable dtMidified9 = new DataTable(); //存储主表 编辑 的行记录
                DataTable dtDeleted9 = new DataTable(); //存储主表 删除 的行记录

                dtDeleted9 = dt9.GetChanges(DataRowState.Deleted);
                dtMidified9 = dt9.GetChanges(DataRowState.Modified);
                dtAdded9 = dt9.GetChanges(DataRowState.Added);
                //140706_0<<<<<<<<<<<<<<<<<<

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

                //===============================================================
                //删除资料开始
                //此处必须注意删除顺序 由子表删除后再删除主表
                //GlobalProductionName-->GlobalProductionType
                //GlobalMSADefintionInf-->GlobalManufactureCoefficientsGroup
                //GlobalMSADefintionInf-->GlobalMSA
                //dt1 = MainForm.GlobalDS.Tables["GlobalProductionType"];
                //dt2 = MainForm.GlobalDS.Tables["GlobalProductionName"];
                //dt3 = MainForm.GlobalDS.Tables["GlobalMSA"];
                //dt4 = MainForm.GlobalDS.Tables["GlobalMSADefintionInf"];                
                //dt5 = MainForm.GlobalDS.Tables["GlobalManufactureCoefficientsGroup"];
                //dt6 = MainForm.GlobalDS.Tables["GlobalManufactureCoefficients"];
                //140706_0>>>>>>>>>>>>>>>>>> 均为 GlobalProductionName的子表项目
                //dt7 = MainForm.GlobalDS.Tables["GlobalManufactureChipsetControl"];
                //dt8 = MainForm.GlobalDS.Tables["GlobalManufactureChipsetInitialize"];
                //dt9 = MainForm.GlobalDS.Tables["GlobalMSAEEPROMInitialize"];
                //140707_0 >>>>>>>>>>>>>>
                //dt10 = MainForm.GlobalDS.Tables["GlobalAllEquipmentList"];
                //dt11 = MainForm.GlobalDS.Tables["GlobalAllEquipmentParamterList"];
                ////140707_1 >>>>>>>>>>>>>>
                //dt12 = MainForm.GlobalDS.Tables["GlobalAllAppModelList"];
                //dt13 = MainForm.GlobalDS.Tables["GlobalAllTestModelList"];
                //dt14 = MainForm.GlobalDS.Tables["GlobalTestModelParamterList"];

                //140706_0>>>>>>>>>>>>>>>>>> 均为 GlobalProductionName的子表项目
                if (dtDeleted7 != null) da7.Update(dtDeleted7);
                if (dtDeleted8 != null) da8.Update(dtDeleted8);
                if (dtDeleted9 != null) da9.Update(dtDeleted9);
                //140706_0<<<<<<<<<<<<<<<<<< 

                //140707_0>>>>>>>>>>>>>>>>>>  //设备资料 独立       
                if (dtDeleted11 != null) da11.Update(dtDeleted11);  //140911_2 调整顺序[删子后删主]
                if (dtDeleted10 != null) da10.Update(dtDeleted10);  //140911_2 调整顺序[删子后删主]                
                //140707_0<<<<<<<<<<<<<<<<<< 

                //140707_0>>>>>>>>>>>>>>>>>>  //APP+Model+Prmtr资料 独立               
                if (dtDeleted14 != null) da14.Update(dtDeleted14);
                if (dtDeleted13 != null) da13.Update(dtDeleted13);
                if (dtDeleted12 != null) da12.Update(dtDeleted12);
                //140707_0<<<<<<<<<<<<<<<<<< 


                //Type + PN
                if (dtDeleted2 != null) da2.Update(dtDeleted2);
                if (dtDeleted1 != null) da1.Update(dtDeleted1);
                if (dtDeleted4 != null) da4.Update(dtDeleted4);
                if (dtDeleted3 != null) da3.Update(dtDeleted3);
                if (dtDeleted6 != null) da6.Update(dtDeleted6);
                if (dtDeleted5 != null) da5.Update(dtDeleted5);
                //删除资料结束
                //===============================================================


                //===============================================================
                //新增资料开始
                //此处必须注意更新顺序 由主表-->子表
                if (dtAdded5 != null) da5.Update(dtAdded5);
                if (dtMidified5 != null) da5.Update(dtMidified5);

                if (dtAdded6 != null) da6.Update(dtAdded6);
                if (dtMidified6 != null) da6.Update(dtMidified6);

                if (dtAdded3 != null) da3.Update(dtAdded3);
                if (dtMidified3 != null) da3.Update(dtMidified3);

                if (dtAdded4 != null) da4.Update(dtAdded4);
                if (dtMidified4 != null) da4.Update(dtMidified4);
                if (dtAdded1 != null) da1.Update(dtAdded1);
                if (dtMidified1 != null) da1.Update(dtMidified1);

                if (dtAdded2 != null) da2.Update(dtAdded2);
                if (dtMidified2 != null) da2.Update(dtMidified2);

                //140706_0>>>>>>>>>>>>>>>>>> 均为 GlobalProductionName的子表项目
                if (dtAdded7 != null) da7.Update(dtAdded7);
                if (dtMidified7 != null) da7.Update(dtMidified7);

                if (dtAdded8 != null) da8.Update(dtAdded8);
                if (dtMidified8 != null) da8.Update(dtMidified8);

                if (dtAdded9 != null) da9.Update(dtAdded9);
                if (dtMidified9 != null) da9.Update(dtMidified9);
                //140706_0<<<<<<<<<<<<<<<<<< 

                //140707_0<<<<<<<<<<<<<<<<<< 
                if (dtAdded10 != null) da10.Update(dtAdded10);
                if (dtMidified10 != null) da10.Update(dtMidified10);

                if (dtAdded11 != null) da11.Update(dtAdded11);
                if (dtMidified11 != null) da11.Update(dtMidified11);
                //140707_0<<<<<<<<<<<<<<<<<< 

                //140707_0<<<<<<<<<<<<<<<<<< 
                if (dtAdded12 != null) da12.Update(dtAdded12);
                if (dtMidified12 != null) da12.Update(dtMidified12);

                if (dtAdded13 != null) da13.Update(dtAdded13);
                if (dtMidified13 != null) da13.Update(dtMidified13);

                if (dtAdded14 != null) da14.Update(dtAdded14);
                if (dtMidified14 != null) da14.Update(dtMidified14);
                //新增资料结束
                //===============================================================

                //140707_0<<<<<<<<<<<<<<<<<< 
                tr.Commit();
                myOPresult = true;

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
                    if (conn.State != ConnectionState.Closed) conn.Close();
                    return result;
                }
                catch (System.Exception TransactionEx)
                {
                    //Handle Exception
                    MessageBox.Show(TransactionEx.ToString());
                    if (conn.State != ConnectionState.Closed) conn.Close();
                    return result;
                }
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
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
                if (conn.State != ConnectionState.Closed)
                    conn.Close();
            }
        }

        public override string[] GetCurrTablesName(string ServerName, string DBName, string userName, string pwd)
        {
            string strConnection = @"Data Source=" + ServerName + ";Initial Catalog=" + DBName + ";Provider=SQLOLEDB.1;user id = " + userName + ";password=" + pwd + ";";
            OleDbConnection Conn = new OleDbConnection(strConnection);
            try
            {
                if (conn == null) conn.Open();
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                //DataTable cnSch = myAccessIO.Conn.GetSchema("Tables");
                Conn.Open();
                DataTable cnSch = Conn.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });

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
                MessageBox.Show( ex.Message);
                return null;
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                    conn.Close();
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

                //DataTable cnSch = myConn.GetSchema("Tables");
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

    }
}

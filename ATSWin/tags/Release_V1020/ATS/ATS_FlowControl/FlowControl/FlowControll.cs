using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Security;
using System.Data.OleDb;
using System.IO;
using System.Xml.Linq;
using ATS_Driver;
using ATS_Framework;
using System.Collections;
using System.Threading;
using System.Timers;
using ATSDataBase;
namespace ATS
{



    public class CurrentTestModelEquipmentList : EquipmentList
    {

    }
    public class FlowControll// Flow Controll
    {
        #region Interface

       public  LogQueue<string> QueueInterfaceLog= new LogQueue<string> { };
       public  LogQueue<string> QueueShow = new LogQueue<string> { };
       public logManager MyLogManager;
       public string CurrentSN;
       public string CurrentFwRev;
       
      #endregion

        #region FlowControll

        //----------------
        public byte TotalChannel = 0;
        public byte TotalTempCount = 0;
        public byte TotalVccCount = 0;
        public string StrAuxAttribles = "";
        public byte ApcStyle = 0;
        public String StrLightSourceErArray="0,0,0,0";
        public string StrTempADCTolerance = "10";
        //----------------
        public int ProductionTypeId;
        public int ProductionNameId;
        public int TestPlanId=-1;
        public int SNID=-1;
        public int MCoefsID = -1;
        public int MSAID = -1;
       // private int CondtionID;
        public string ProductionTypeName;
        public string StrProductionName;
       // public string SerialNO;

        public string[] StrProductionTypeList;
       // private DataTable DtMyTestPlan;// 放入Condition Table的所有数据
        public DataTable DtMyConditionDataTable;// 放入Condition Table的所有数据
       // private DataTable DtMyTestModel;// 放入 各个条件下 TestModel 的列表,
        private DataTable DtMyTestModelParameter;// 各个TestModel的参数
        private DataTable DtMyDutInf;
        public bool StopFlag = false;// 标志位适用于立即中断测试

        public bool TotalResultFlag = true;//全部测试结果的标志位
        private bool CurrentTestModelResultflag = true;//当前TestMode 测试结果标志位
        public bool CurrentConditionResultflag = true;//当前condition 测试结果标志位
       // public DataTable DtLogData;

       // private DataTable dtCurrentConditionTestData;
        private DataTable dtCurrentTestModelTestData;
        public DataTable dtCurrentConditionResultData;
        private DataTable dtCurrentTestModelList;
        private DataTable dtCurrentTestModeParameterlList;
        public String StrStartTime;

        private DataRow drCondition;
        private DataRow drTestModel;
        public bool FlagFlowControllEnd = false;
        public int prosess;
        public DataTable TotalTestData;
        private bool FlagDrvierInitializeOK=true;
        public bool FlagDrvierNeedInitialize = false;
        public bool FlagIgnoreRecordCoef = true;
        //----------------------------------
       // public bool ReadyforReadflag = false;
        public string StrInterfaceLog;
        public string StrSqlLog;
        public string StrInputLog;
        public string StrOutputLog;
        

        public EnvironmentalControll pEnvironmentcontroll;

        public int CurrentConditionId = 0;
        public int CurrentLogId =0;
        public double StrCurrentTemp;
        public double StrCurrentVcc;
        public byte CurrentChannel;
        private String StrCurrentConditionName;
        public string StrIpAddress="";

       // private TestModeEquipmentParameters[] TestModelOutput;
        private TestModeEquipmentParameters[] TestModelInputArray;// TestModel 输入数组
        private TestModeEquipmentParameters[] TestModelOutputArray;// TestModel 输出数组

        //---------------------数据处理中的一些临时变量
        private DataTable DT=new DataTable();

        //---------------------------Write Log
      //  private WriteTxT pWriteTxt;
        //-------------------
        #endregion
        #region AlgorithModel

        // public TxAlgorithmModel pAppTx;
        public RxAlgorithmModel pAppRx;

        #endregion
        #region Definition Equipment
      //  public CurrentTestModelEquipmentList MyCurrentTestModelEquipmentList;
        public DUT pDut;


        #endregion

        public EquipmentList MyEquipList;
        public EquipmentManage MyEquipmentManage;
        public TestModelList MyTestModelList;
        public TestModelManage MyTestModelManage;
        public SortedList<string, string> EquipmenNameArray;
        public globalParameters pGlobalParameters = new globalParameters();
        public string StrElecEyeDiagramPath;
        public string StrOptEyeDiagramPath;
        //------------------------------
        public DataIO MyDataio;

        public FlowControll(ConfigXmlIO MyXml, logManager alogManager)
        {
            MyLogManager = alogManager;
            MyEquipmentManage = new EquipmentManage(alogManager);
            MyEquipList = new EquipmentList();
            MyTestModelList = new TestModelList();
            MyTestModelManage = new TestModelManage();
            EquipmenNameArray = new SortedList<string, string>();
         //   EquipmenNameArray = GetEquipmentNameList(TestPlanId);
            MyLogManager.AdapterLogString(1, "The EXE Start......");
           

            if (MyXml.DatabaseType.ToUpper() == "LOCATIONDATABASE")
            {
                MyDataio = new LocalDatabase(MyXml.DatabasePath);
            }
            else//SqlDatabase
            {
              //  MyXml.DatabasePath = @"INPCSZ0518\ATS_HOME";
               // MyDataio = new SqlDatabase(MyXml.DatabasePath);
                MyDataio = new SqlDatabase(MyXml.DatabasePath, MyXml.DbName, MyXml.Username, MyXml.PWD);
            }


            dtCurrentTestModelTestData = new DataTable();
            dtCurrentConditionResultData = new DataTable();
            dtCurrentTestModelList = new DataTable();
            dtCurrentTestModeParameterlList = new DataTable();
            //-----------------------------------

            TotalTestData = new DataTable();
            DtMyConditionDataTable = new DataTable();
            DtMyTestModelParameter = new DataTable();
            DtMyDutInf = new DataTable();
        }
        //
        // 作用:
        //     建立测试列表。
        public bool buildConditionList()
   
        {
           
            DataTable dt = new DataTable();
            DataRow dr;
            DtMyConditionDataTable.Clear();
            DtMyTestModelParameter.Clear();
            //---------------------------------------------------------Condition List
            string StrTableName = "TopoTestControl";
            //string StrSelectconditions = "select * from " + StrTableName + " where PID=" + TestPlanId + " order by SEQ ASC";
            string StrSelectconditions = "select * from " + StrTableName + " where PID=" + TestPlanId + " and IgnoreFlag=0" + " order by SEQ ASC";
            
            dt = MyDataio.GetDataTable(StrSelectconditions, StrTableName);// 获得环境的DataTable

            //---------------------------组件全新的DtMyConditionDataTable
           if(DtMyConditionDataTable.Columns.Count==0)
           {
                for(int i=0;i<dt.Columns.Count;i++)
                {
                     DtMyConditionDataTable.Columns.Add(dt.Columns[i].ColumnName.ToString());
                }
            }
          
           for(int RowNo=0;RowNo<dt.Rows.Count;RowNo++)
            {
                if (dt.Rows[RowNo]["Channel"].ToString() == "0")
                {
                    for (int j = 0; j < TotalChannel; j++)
                    {
                         dr = DtMyConditionDataTable.NewRow();
                        for(int k=0;k<dt.Columns.Count;k++)
                        {
                            dr[k] = dt.Rows[RowNo][k];
                        }
                        dr["Channel"] = j + 1;
                        DtMyConditionDataTable.Rows.Add(dr);
                    }
                }
                else
                {
                    dr = DtMyConditionDataTable.NewRow();
                    for (int k = 0; k < dt.Columns.Count; k++)
                    {
                        dr[k] = dt.Rows[RowNo][k];
                    }
                    DtMyConditionDataTable.Rows.Add(dr);
                }
               //---------------------------给DtMyConditionDataTable中的Condition 排序 
            }
           for (int RowNo1 = 0; RowNo1 < DtMyConditionDataTable.Rows.Count; RowNo1++)
            {
                DtMyConditionDataTable.Rows[RowNo1]["SEQ"] = RowNo1 + 1;
            }

            return true;
        }

   

        public DutStruct[] GetDutManufactureCoefficients()
        {
            DutStruct[] DutInfStruct;
            int i = 0;
            // protected DutStruct[] MyDutStruct;
            DtMyDutInf = new DataTable();

            string StrTableName = "GlobalManufactureCoefficients";

            string StrSelectconditions = "select * from " + StrTableName + " where PID= " + MCoefsID + " order by ID";
            DtMyDutInf = MyDataio.GetDataTable(StrSelectconditions, StrTableName);// 获得环境的DataTable

            DutInfStruct = new DutStruct[DtMyDutInf.Rows.Count];
           // MyDutStructArray.
            foreach (DataRow dr in DtMyDutInf.Rows)
            {
                DutStruct dutStruct = new DutStruct();

                dutStruct.FiledName = Convert.ToString(dr["ItemName"]).Trim().ToUpper();
                dutStruct.Channel = Convert.ToByte(dr["Channel"]);
                // string jj = dr["SlaveAddress"].ToString();

             //   dutStruct.SlaveAdress = Convert.ToInt32(dr["SlaveAddress"].ToString(), 16);
              string  StrItemType = dr["ItemTYPE"].ToString();

                if (StrItemType.ToUpper() == "COEFFICIENT")
                {
                    dutStruct.CoefFlag = true;
                }
                else
                {
                    dutStruct.CoefFlag = false;
                }  

                dutStruct.EngPage = Convert.ToByte(dr["Page"]);
                dutStruct.StartAddress = Convert.ToInt32(dr["StartAddress"]);
                dutStruct.Length = Convert.ToByte(dr["Length"]);

                switch (Convert.ToString(dr["Format"]))
                {
                    case "IEEE754":
                        dutStruct.Format = 1;
                        break;
                    case "U16":
                        dutStruct.Format = 2;
                        break;
                    case "U8":
                        dutStruct.Format = 3;
                        break;
                    default:
                        break;

                }

                //dutStruct.TempSelect = Convert.ToByte(dr["TempSelect"]);
                //dutStruct.VccSelect = Convert.ToByte(dr["VccSelect"]);
                //dutStruct.DebugStartAddress = Convert.ToByte(dr["DebugStartAddress"]);
                //dutStruct.ChipLine = Convert.ToByte(dr["ChipLine"]);
                DutInfStruct[i] = dutStruct;

                i++;
            }

            return DutInfStruct;
        }
        public DriverInitializeStruct[] GetManufactureChipsetInitialize()
        {
            DriverInitializeStruct[] MyStruct;
            int i = 0;
            // protected DutStruct[] MyDutStruct;
            DtMyDutInf = new DataTable();

            string StrTableName = "GlobalManufactureChipsetInitialize";

            string StrSelectconditions = "select * from " + StrTableName + " where PID= " + ProductionNameId + " order by ID";
            DtMyDutInf = MyDataio.GetDataTable(StrSelectconditions, StrTableName);// 获得环境的DataTable

            MyStruct = new DriverInitializeStruct[DtMyDutInf.Rows.Count];
            // MyDutStructArray.
            foreach (DataRow dr in DtMyDutInf.Rows)
            {
                DriverInitializeStruct dutDriverStruct = new DriverInitializeStruct();
             
                dutDriverStruct.ChipLine = Convert.ToByte(dr["ChipLine"]);
                dutDriverStruct.DriverType = Convert.ToByte(dr["DriveType"]);
                dutDriverStruct.RegisterAddress = Convert.ToInt16(dr["RegisterAddress"]);//         RegisterAddress
                dutDriverStruct.Length = Convert.ToByte(dr["Length"]);//         RegisterAddress
                dutDriverStruct.ItemValue = dr["ItemValue"];
                dutDriverStruct.Endianness = Convert.ToBoolean(dr["Endianness"]);
                MyStruct[i] = dutDriverStruct;

                i++;
            }

            return MyStruct;
        }
        public DriverStruct[] GetManufactureChipsetControl()
        {
            DriverStruct[] MyDriverStruct;
            int i = 0;
            // protected DutStruct[] MyDutStruct;
            DtMyDutInf = new DataTable();

            string StrTableName = "GlobalManufactureChipsetControl";

            string StrSelectconditions = "select * from " + StrTableName + " where PID= " + ProductionNameId + " order by ID";
            DtMyDutInf = MyDataio.GetDataTable(StrSelectconditions, StrTableName);// 获得环境的DataTable

            MyDriverStruct = new DriverStruct[DtMyDutInf.Rows.Count];
            // MyDutStructArray.
            foreach (DataRow dr in DtMyDutInf.Rows)
            {
                DriverStruct dutDriverStruct = new DriverStruct();
                dutDriverStruct.FiledName = Convert.ToString(dr["ItemName"]).Trim().ToUpper();
                dutDriverStruct.MoudleLine = Convert.ToByte(dr["ModuleLine"]);
                dutDriverStruct.ChipLine = Convert.ToByte(dr["ChipLine"]);
                dutDriverStruct.DriverType = Convert.ToByte(dr["DriveType"]);
                dutDriverStruct.RegisterAddress = Convert.ToInt16(dr["RegisterAddress"]);//         RegisterAddress
                dutDriverStruct.Length = Convert.ToByte(dr["Length"]);//         RegisterAddress
                MyDriverStruct[i] = dutDriverStruct;

                i++;
            }

            return MyDriverStruct;
        }
    
        public bool StartTest()
        {

               #region      进入测试前的准备

            pEnvironmentcontroll.LastTemp = -1000;
            StopFlag = false;
            
            dtCurrentTestModelTestData.Clear();
            dtCurrentConditionResultData.Clear();
            dtCurrentTestModelList.Clear();
            dtCurrentTestModeParameterlList.Clear();

            for (int i=0;i<MyEquipList.Count;i++)// 开启所有仪器开关
            {
                MyEquipList[MyEquipList.Keys[i]].Switch(true);
            }


         


         
            pGlobalParameters.TotalTempCount = TotalTempCount;
            pGlobalParameters.TotalVccCount = TotalVccCount;
            pGlobalParameters.CurrentSN = CurrentSN;
            pGlobalParameters.PN = StrProductionName;
            pGlobalParameters.StrPathEEyeDiagram = StrElecEyeDiagramPath;
            pGlobalParameters.StrPathOEyeDiagram = StrOptEyeDiagramPath;
            pGlobalParameters.OpticalSourseERArray = StrLightSourceErArray;
        
            if (StrAuxAttribles.ToUpper().Contains("APCSTYLE"))
            {

                if (StrAuxAttribles.ToUpper().Contains("APCSTYLE=1"))
                {
                    pGlobalParameters.ApcStyle = 1;
                }
                else
                {
                    pGlobalParameters.ApcStyle = 0;
                }
            }
            else
            {
                MessageBox.Show("数据库填写的额外属性不包含APCStyle");
                TotalResultFlag = false;
                return false;
            }


            TotalTestData.Clear();
            if (TotalTestData.Columns.Count == 0)
            {
                TotalTestData.Columns.Add("Temp");
                TotalTestData.Columns.Add("Vcc");
                TotalTestData.Columns.Add("Channel");
                TotalTestData.Columns.Add("ItemName");
                TotalTestData.Columns.Add("Value");
                TotalTestData.Columns.Add("SpecMax");
                TotalTestData.Columns.Add("SpecMin");
                TotalTestData.Columns.Add("ItemSpecific");
                TotalTestData.Columns.Add("LogRecord"); //Failbreak;DataRecord
                TotalTestData.Columns.Add("Failbreak");
                TotalTestData.Columns.Add("DataRecord");
                TotalTestData.Columns.Add("Result");
            }


            prosess = 0;
           


            MyTestModelManage.SetDutObject(pDut, MyLogManager);

           // QueueShow.Enqueue("Start Testing........");


            buildConditionList();//从数据库读取FlowControl
            MyTestModelList.Clear();
#endregion
            try
            {

               
                #region Driver 初始化

                if (FlagDrvierNeedInitialize && pDut.DriverInitStruct.Length>0)
                {
                    QueueShow.Enqueue("Driver  Initializing........");

                    try
                    {
                        FlagDrvierInitializeOK = true;
                        FlagDrvierInitializeOK = pDut.DriverInitialize();

                    }

                    catch
                    {
                        FlagDrvierInitializeOK = false;
                        MessageBox.Show("Driver Initialize  Error");
                        Exception ex = new Exception("Driver Initialize  Error");
                        throw ex;
                    }
                }
                QueueShow.Enqueue("Start Testing........"); 
#endregion
                #region Condition 遍历Conditions

                for (int ConditionSEQ = 0; ConditionSEQ < DtMyConditionDataTable.Rows.Count; ConditionSEQ++)//遍历测试环境条件
                {

                    #region           对于当前FlwoControl数据的解析和Log记载

                    CurrentConditionResultflag = true;
                    dtCurrentConditionResultData.Clear();
                    dtCurrentTestModelList.Clear();
                    dtCurrentTestModeParameterlList.Clear();

                    if (dtCurrentConditionResultData.Columns.Count == 0)
                    {

                        for (int i = 0; i < TotalTestData.Columns.Count; i++)// Add Colums
                        {
                            dtCurrentConditionResultData.Columns.Add(TotalTestData.Columns[i].ColumnName);
                        }
                    }

                   // DateTime now = DateTime.Now;
                     StrStartTime = MyDataio.GetCurrTime().ToString("yyyy/MM/dd HH:mm:ss");

                    drCondition = DtMyConditionDataTable.Rows[ConditionSEQ];
                    CurrentConditionId = Convert.ToInt32(drCondition["ID"]);// 记录当前ConditionID号码
                    StrSqlLog = "";
                   // ReadyforReadflag = false;
                    string ss = drCondition["SEQ"].ToString();
                    CurrentChannel = Convert.ToByte(drCondition["Channel"]);
                    StrCurrentTemp = Convert.ToDouble(drCondition["Temp"]);
                    StrCurrentVcc = Convert.ToDouble(drCondition["Vcc"]);
                    StrCurrentConditionName = drCondition["ItemName"].ToString();

                    QueueShow.Enqueue("Change Environment....." + "Temp= " + StrCurrentTemp + " ** VCC= " + StrCurrentVcc + " ** Channel=" + CurrentChannel);

                    //  if (StrCurrentTemp != "25") goto Error;
                    pEnvironmentcontroll.SelectEquipment(MyEquipList);

                    string StrEnvironmentAuxAttribles = drCondition["AuxAttribles"].ToString();
                    if (!pEnvironmentcontroll.ConfigEnvironmental(Convert.ToDouble(StrCurrentTemp), Convert.ToDouble(StrCurrentVcc), Convert.ToByte(CurrentChannel), StrEnvironmentAuxAttribles))// 设定当前的环境 以及告诉DUT 当前为通道几
                    {
                        MessageBox.Show("环境切换的时候出错```");
                        FlagFlowControllEnd = true;
                        return false;
                    }
                    if (StopFlag)
                    {
                        TotalResultFlag = false;
                        break;
                    }
                    QueueInterfaceLog.Enqueue("Temp= " + StrCurrentTemp + " ** VCC= " + StrCurrentVcc + " ** Channel=" + CurrentChannel + "****" + StrCurrentConditionName);
                    // QueueSqlLog.Enqueue( "Temp= " + StrCurrentTemp + " ** VCC= " + StrCurrentVcc + " ** Channel=" + StrCurrentChannel + "****" + StrCurrentConditionName );
                    StrSqlLog += "Temp= " + StrCurrentTemp + " ** VCC= " + StrCurrentVcc + " ** Channel=" + CurrentChannel + "****" + StrCurrentConditionName + "**";
                 #endregion
                    #region TestModel  运行当前Condition中的所有TestModel

                    string StrTestModelTableName = "TopoTestModel";
                    string StrTestModelSelectconditions = "select * from " + StrTestModelTableName + " where PID=" + CurrentConditionId + " and IgnoreFlag=0" + " order by SEQ ASC";
                    dtCurrentTestModelList = MyDataio.GetDataTable(StrTestModelSelectconditions, StrTestModelTableName);// 获得环境的DataTable

                    for (int TestModelNo = 0; TestModelNo < dtCurrentTestModelList.Rows.Count; TestModelNo++)// 遍历Condition中的TestModel
                   {
                          
                     

                        Thread.Sleep(200);
                        StrInputLog = "";
                        StrOutputLog = "";
                       // CurrentConditionResultflag = false;
                        CurrentTestModelResultflag = true;
                        //  pWriteTxt.Write("Temp=" + StrCurrentTemp + "**" + "VCC=" + StrCurrentVcc + "**" + "Channel=" + StrCurrentChannel );

                        MyLogManager.AdapterLogString(1, "Temp=" + StrCurrentTemp + "**" + "VCC=" + StrCurrentVcc + "**" + "Channel=" + CurrentChannel);

                        drTestModel = dtCurrentTestModelList.Rows[TestModelNo];
                        string CurrentTestModelId = drTestModel["ID"].ToString();
                        string StrTestModelName = drTestModel["ItemName"].ToString();
                        MyLogManager.AdapterLogString(1, StrTestModelName + "TestModelID=" + CurrentTestModelId);
                        //  pWriteTxt.Write(StrTestModelName + "TestModelID="+CurrentTestModelId);
                        QueueInterfaceLog.Enqueue("Temp=" + StrCurrentTemp + "**" + "VCC=" + StrCurrentVcc + "**" + "Channel=" + CurrentChannel + "**" + StrTestModelName);

                        Thread.Sleep(1000);
                        try
                        {

                            if (!RunTestModel(StrTestModelName, CurrentTestModelId)) CurrentConditionResultflag=false;
                        }
                        catch (Exception EE)
                        {
                            CurrentConditionResultflag = false;
                            MyLogManager.AdapterLogString(1, StrTestModelName + EE.Message);

                        }
                        finally
                        {
                            //MyLogManager.AdapterLogString(1, StrTestModelName+"Code Error!");
                           
                        }
                        if (StopFlag)
                        {
                            TotalResultFlag = false;
                            break;
                        }
                    }

                    #endregion
     Error:
                    #region 将当前数据填充到TotalTestData,并且完成当前Condition的数据存档
                  
                    for (int i = 0; i < dtCurrentConditionResultData.Rows.Count; i++)
                    {
                        DataRow dr = TotalTestData.NewRow();

                        for (int Colum = 0; Colum < dtCurrentConditionResultData.Columns.Count; Colum++)
                        {
                            dr[dtCurrentConditionResultData.Columns[Colum].ColumnName] = dtCurrentConditionResultData.Rows[i][dtCurrentConditionResultData.Columns[Colum].ColumnName];
                        }


                        TotalTestData.Rows.Add(dr);
                    }
                     if (dtCurrentConditionResultData.Rows.Count==0)
                    {
                        CurrentConditionResultflag = false;
                    }
#region 从当前ConditionTestData中挑选出错误项
                    DataRow[] DrErrorItem = dtCurrentConditionResultData.Select("ItemSpecific='1' and Result='fail'");
                   
                    String StrErrorItemMessage = "";

                     foreach (DataRow drFail in DrErrorItem)
                     {
                         double ItemValue, SpecMax;
                         ItemValue = Convert.ToDouble(drFail["Value"]);
                         SpecMax = Convert.ToDouble(drFail["SpecMax"]);
                         if (ItemValue > SpecMax)
                         {
                             StrErrorItemMessage += drFail["ItemName"].ToString() + " > " + SpecMax.ToString() + ",";
                         }
                         else
                         {
                             StrErrorItemMessage += drFail["ItemName"].ToString() + " < " + drFail["SpecMin"].ToString() + ",";
                         }


                        // drFail["ItemName"]
                      }

#endregion
                    string strEndTime = MyDataio.GetCurrTime().ToString("yyyy/MM/dd HH:mm:ss");

                    MyDataio.WriterLog(CurrentConditionId, SNID, StrErrorItemMessage, StrStartTime, strEndTime, Convert.ToSingle(StrCurrentTemp), Convert.ToSingle(StrCurrentVcc), CurrentChannel, CurrentConditionResultflag, out CurrentLogId);
                    
                    Thread.Sleep(3000);
                    if (dtCurrentConditionResultData.Rows.Count > 0) MyDataio.WriteResult(CurrentLogId, dtCurrentConditionResultData);
                    prosess = (ConditionSEQ + 1) * 100 / DtMyConditionDataTable.Rows.Count;

                    GC.Collect();


                    if (StopFlag)
                    {
                        TotalResultFlag = false;
                        break;
                    }

       #endregion
                }
                #endregion

                #region  存档Coef

                if (!StopFlag)
                {

                   
                    if (!FlagIgnoreRecordCoef)
                    { 
                        QueueShow.Enqueue("Record Coef....,Wait for a Moment");
                        DutCoefValueStuct[] CoefStruct;
                        if (!pDut.ReadALLcoef(out CoefStruct))
                        {
                            MyLogManager.AdapterLogString(3, "Dut Read All Coef Error");

                            MessageBox.Show("Recorde Coef Error");
                            QueueShow.Enqueue("Record Coef Error");

                        }
                        else
                        {
                            if (!Record_DutCoef(SNID, CoefStruct))
                            {
                                MessageBox.Show("Recorde Coef Error");
                                TotalResultFlag = false;
                                QueueShow.Enqueue("Record Coef Error");
                            }
                        }
                    }
                }
                #endregion

                WriterEndTimeToSNTable();
                // MessageBox.Show("FlowControl End");

                pEnvironmentcontroll.Dispose();

                for (int k=0;k<MyEquipList.Count;k++)
                {
                    MyEquipList[MyEquipList.Keys[k].ToString()].Referenced_Times = 0;
                }
               
                MyLogManager.AdapterLogString(1, "Test End------------------------");
                FlagFlowControllEnd = true;
                pEnvironmentcontroll.LastTemp = -100;
                return true;
            }
            catch(Exception ex)
            {
                WriterEndTimeToSNTable();
                // MessageBox.Show("FlowControl End");
                pEnvironmentcontroll.LastTemp = -100;
                pEnvironmentcontroll.Dispose();
                MyLogManager.AdapterLogString(1, "Test End------------------------");
                FlagFlowControllEnd = true;
                return false;
            }
        }


        public bool RunTestModel(string StrTestModelName, String StrCurrentTestModelId)
        {
            CurrentTestModelResultflag = true;
            try
            {

                QueueShow.Enqueue("Temp=" + StrCurrentTemp + "**" + "VCC=" + StrCurrentVcc + "**" + "Channel=" + CurrentChannel + "**" + StrTestModelName + "Testing....");

                string StrartTime = MyDataio.GetCurrTime().ToString("yyyy/MM/dd HH:mm:ss");
               

                TestModelBase pCurrentTestModel;
                string[] ArrayEquipmenName;

                dtCurrentTestModelTestData.Clear();



                pGlobalParameters.CurrentChannel = Convert.ToByte(CurrentChannel);
                pGlobalParameters.CurrentTemp = Convert.ToUInt16(StrCurrentTemp);
                pGlobalParameters.CurrentVcc = Convert.ToDouble(StrCurrentVcc);

                

                SelcetTestModelParameter(StrCurrentTestModelId);

                if (!MyTestModelList.Keys.Contains(StrTestModelName.ToUpper().Trim()))
                {
                    pCurrentTestModel = MyTestModelManage.Createobject(StrTestModelName.ToUpper().Trim());
                    MyTestModelList.Add(StrTestModelName.ToUpper().Trim(), pCurrentTestModel);
                }
           
                MyTestModelList[StrTestModelName.ToUpper().Trim()].SelectEquipment(MyEquipList);// 将其要使用的仪器 加入当前的Testmodel 自己的 sortlist

                StrInputLog += "Temp=" + StrCurrentTemp + "**" + "VCC=" + StrCurrentVcc + "**" + "Channel=" + CurrentChannel + "**" + StrTestModelName + "\r\n";

                WriteInputLog();

                MyTestModelList[StrTestModelName.ToUpper().Trim()].SetinputParameters = TestModelInputArray;
                MyTestModelList[StrTestModelName.ToUpper().Trim()].SetoutputParameters = TestModelOutputArray;

                MyTestModelList[StrTestModelName.ToUpper().Trim()].SetGlobalParameters = pGlobalParameters;
                bool CurrentTestModelflag = false;
                //try
                //{
                     CurrentTestModelflag = MyTestModelList[StrTestModelName.ToUpper().Trim()].RunTest();
             
                //}
                //catch (System.Exception ex)
                //{
                //    //MyTestModelList[StrTestModelName.ToUpper().Trim()].PostTest();

                //    CurrentTestModelflag = false;
               // }
             //   bool CurrentTestModelflag = MyTestModelList[StrTestModelName.ToUpper().Trim()].RunTest();
             
                  MyLogManager.AdapterLogString(1,MyTestModelList[StrTestModelName.ToUpper().Trim()].GetLogInfor);
                // QueueInterfaceLog.Enqueue(MyTestModelList[StrTestModelName.ToUpper().Trim()].GetLogInfor);
                  QueueInterfaceLog.Enqueue("Current TestModel:" + CurrentTestModelflag.ToString());
                  StrInputLog += "Current TestModel:" + CurrentTestModelflag.ToString() + "\r\n";
                  string TestEndTime = MyDataio.GetCurrTime().ToString("yyyy/MM/dd HH:mm:ss");
                  string StrSN = CurrentSN + "\r\n"; ;


                TestModelOutputArray = MyTestModelList[StrTestModelName.ToUpper().Trim()].GetoutputParameters;

                Thread.Sleep(2000);
                JudgeSpec MyJudgeSpec = new JudgeSpec();

                bool IsBreakflag=false;
                MyJudgeSpec.Judge(dtCurrentTestModelTestData, TestModelOutputArray, CurrentTestModelflag, out dtCurrentTestModelTestData, out IsBreakflag, out CurrentTestModelResultflag);

                if (IsBreakflag)
                {
                    StopFlag = true;
                }
                if (!CurrentTestModelflag)
                {
                    CurrentTestModelResultflag = false;
                }
                if (!CurrentTestModelResultflag)
                {
                    CurrentConditionResultflag = false;
                    TotalResultFlag = false;
                }

                for (int i = 0; i < dtCurrentTestModelTestData.Rows.Count; i++)
                {
                    DataRow dr = dtCurrentConditionResultData.NewRow();

                    for (int Colums = 0; Colums < dtCurrentConditionResultData.Columns.Count; Colums++)
                    {
                        dr[dtCurrentConditionResultData.Columns[Colums].ColumnName] = dtCurrentTestModelTestData.Rows[i][dtCurrentConditionResultData.Columns[Colums].ColumnName];
                    }
                    dtCurrentConditionResultData.Rows.Add(dr);

                }

                WriteOutputLog();


                QueueShow.Enqueue("Temp=" + StrCurrentTemp + "**" + "VCC=" + StrCurrentVcc + "**" + "Channel=" + CurrentChannel + "**" + StrTestModelName + "Testing End.....");

                MyLogManager.AdapterLogString(1, "Temp=" + StrCurrentTemp + "**" + "VCC=" + StrCurrentVcc + "**" + "Channel=" + CurrentChannel + "**" + StrTestModelName + "Testing End.....");
              //  MyLogManager.AdapterLogString(1,StrartTime+StrInputLog + MyTestModelList[StrTestModelName.ToUpper().Trim()].GetLogInfor + StrOutputLog, TestEndTime);

                return CurrentTestModelResultflag;



            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                CurrentTestModelResultflag = false;
                TotalResultFlag = false;
                return false;
            }
        }

        private void WriteInputLog()
        {
            StrInputLog += "InputLog--------------------"+"\r\n";

            for (int i = 0; i < TestModelInputArray.Length; i++)
            {
                StrInputLog += TestModelInputArray[i].FiledName + "=" + TestModelInputArray[i].DefaultValue+"\r\n";
            }
            MyLogManager.AdapterLogString(2, StrInputLog);
           // MyLogManager.AdapterLogString(1,StrInputLog);
        }

        private void WriteOutputLog()
        {
            StrOutputLog += "OutputLog----------------------------------" + "\r\n";

            for (int i = 0; i < TestModelOutputArray.Length; i++)
            {
                StrOutputLog += TestModelOutputArray[i].FiledName + "=" + TestModelOutputArray[i].DefaultValue + "\r\n";
                QueueInterfaceLog.Enqueue(TestModelOutputArray[i].FiledName + "=" + TestModelOutputArray[i].DefaultValue + "\n");
              
            }
            MyLogManager.AdapterLogString(2, StrOutputLog);
           // pWriteTxt.Write(StrOutputLog);
        }

        public bool SelcetTestModelParameter(string StrCurrentTestModelId)
        {
            SortedList<string, string> pTestModelInputArray = new SortedList<string, string>();
            SortedList<string, string> pTestModeloutputArray = new SortedList<string, string>();

            string StrTestModelParameterTableName = "TopoTestParameter";
            string StrTestModelParameterSelectconditions = "select * from " + StrTestModelParameterTableName + " where PID=" + StrCurrentTestModelId + " order by ID";
            dtCurrentTestModeParameterlList = MyDataio.GetDataTable(StrTestModelParameterSelectconditions, StrTestModelParameterTableName);// 获得环境的DataTable

            if (dtCurrentTestModelTestData.Columns.Count == 0)
            {


                for (int i = 0; i < dtCurrentConditionResultData.Columns.Count; i++)
                {
                    dtCurrentTestModelTestData.Columns.Add(dtCurrentConditionResultData.Columns[i].ColumnName);
                }
            }
           


            foreach (DataRow drTestModelParameter in dtCurrentTestModeParameterlList.Rows)// 遍历当前Testmodel 所需要的所有参数
            {

                if (drTestModelParameter["Direction"].ToString().ToUpper().Trim() == "INPUT")
                {
                    pTestModelInputArray.Add(drTestModelParameter["ItemName"].ToString().ToUpper().Trim(), drTestModelParameter["ItemValue"].ToString());
                }
                if (drTestModelParameter["Direction"].ToString().ToUpper().Trim() == "OUTPUT" || drTestModelParameter["ItemSpecific"].ToString().ToUpper().Trim() == "1")
                {
                    pTestModeloutputArray.Add(drTestModelParameter["ItemName"].ToString().ToUpper().Trim(), null);

                    DataRow dr = dtCurrentTestModelTestData.NewRow();
                    
                    dr["Temp"] = StrCurrentTemp;
                    dr["Vcc"] = StrCurrentVcc;
                    dr["Channel"] = CurrentChannel;
                    dr["ItemName"] = drTestModelParameter["ItemName"];
                    dr["Value"] = drTestModelParameter["ItemValue"];
                    dr["SpecMin"] = drTestModelParameter["SpecMin"];
                    dr["SpecMax"] = drTestModelParameter["SpecMax"];
                    dr["LogRecord"] = drTestModelParameter["LogRecord"];
                    dr["Failbreak"] = drTestModelParameter["Failbreak"];
                    dr["DataRecord"] = drTestModelParameter["DataRecord"];
                    dr["ItemSpecific"] = drTestModelParameter["ItemSpecific"];
                    dr["Result"] = "";

                    dtCurrentTestModelTestData.Rows.Add(dr);
                }
            }

            TestModelInputArray = new TestModeEquipmentParameters[pTestModelInputArray.Count];
            TestModelOutputArray = new TestModeEquipmentParameters[pTestModeloutputArray.Count];
            for (int i = 0; i < pTestModelInputArray.Count; i++)
            {
                TestModelInputArray[i].FiledName = pTestModelInputArray.Keys[i].ToString();
                TestModelInputArray[i].DefaultValue = pTestModelInputArray.Values[i].ToString();
            }

            for (int i = 0; i < pTestModeloutputArray.Count; i++)
            {
                TestModelOutputArray[i].FiledName = pTestModeloutputArray.Keys[i].ToString();
                TestModelOutputArray[i].DefaultValue = null;
            }
            return true;
        }
        private bool Record_DutCoef(int RunRecord_ID, DutCoefValueStuct[] StuctCoef)
        {
            DataTable dtCoef = new DataTable();
            string strSelectconditions = "Select * from TopoTestCoefBackup  where PID=" + SNID;
            try
            {
                dtCoef.Clear();

                dtCoef.Columns.Add("id");
                dtCoef.Columns.Add("PID");
                dtCoef.Columns.Add("StartAddr");
                dtCoef.Columns.Add("Page");
                dtCoef.Columns.Add("ItemSize");
                dtCoef.Columns.Add("ItemValue");

                dtCoef.PrimaryKey = new DataColumn[] { dtCoef.Columns["ID"] };
                for (int i = 0; i < StuctCoef.Length; i++)
                {
                    DataRow dr = dtCoef.NewRow();


                    dr["id"] = i + 1;
                    dr["PID"] = RunRecord_ID;
                    dr["StartAddr"] = StuctCoef[i].StartAddress;
                    dr["Page"] = StuctCoef[i].Page;
                    dr["ItemSize"] = StuctCoef[i].Length;
                    dr["ItemValue"] = StuctCoef[i].CoefValue;

                    dtCoef.Rows.Add(dr);
                }
                //MyDataio.UpdateDataTable(strSelectconditions, DT);
                if (!MyDataio.UpdateDataTable(strSelectconditions, dtCoef, false)) return false;

                return true;

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        #region Add  数据解析,从DataIo 中挪过来的

        public string[] ReadProductionTpye()
        {
           


            if (MyDataio.OpenDatabase(true))
            {
                string StrTableName = "GlobalProductionType";

                string Selectconditions = "select * from " + StrTableName + " order by ID";

                DT.Clear();

                DT = MyDataio.GetDataTable(Selectconditions, StrTableName);

                string StrNameList = DT.Rows[0]["ItemName"].ToString();

                string[] arry = new string[DT.Rows.Count];


                for (int j = 0; j < DT.Rows.Count; j++)
                {
                    arry[j] = DT.Rows[j]["ItemName"].ToString();

                }


                return arry;
            }
            else
            {
                return null;
            }

        }

        public string[,] ReadProductionName(int idProductionType)
        {

            //int Row_No = 0;



            if (MyDataio.OpenDatabase(true))
            {

                string StrTableName = "GlobalProductionName";

                string Selectconditions = "select * from " + StrTableName+" where PID="+idProductionType+ " order by ID";


                DT = MyDataio.GetDataTable(Selectconditions, StrTableName);




               // string ss = DT.Rows[0]["ID"].ToString();



                string[,] arry = new string[DT.Rows.Count, 2];


                for (int j = 0; j < DT.Rows.Count; j++)
                {
                    arry[j, 0] = DT.Rows[j]["ID"].ToString();
                    arry[j, 1] = DT.Rows[j]["PN"].ToString();
                }


                return arry;

            }
            else
            {
                return null;
            }

        }

        public bool GetIdFromTpyeTable(string StrProductionTypeName, out int TypeId,out int IMsaId)// 获得产品类型名称,用于Manufacture Address
        {

            TypeId = -1;
            IMsaId = -1;

            string StrTableName = "GlobalProductionType";

            DT.Clear();


            try
            {


                string StrSelectconditions = "select * from " + StrTableName + " where ItemName='" + StrProductionTypeName + "' order by ID";


              //  string StrSelectconditions ="Select GlobalProductionType.id as ProductTypeID , GlobalMSA.id as MSAID ,  GlobalMSA.SlaveAddress  as SlaveAddress from GlobalMSA  Where GlobalMSA.id in();

                DT = MyDataio.GetDataTable(StrSelectconditions, StrTableName);

                if (DT != null)
                {

                    //int s = (int)DT.Rows[0]["ID"];

                   // string ss = s.GetType().ToString();//MSAID
                    TypeId = Convert.ToInt32(DT.Rows[0]["ID"]);
                    IMsaId = Convert.ToInt32(DT.Rows[0]["MSAID"]);
                   // IMsaId = MSAID;
                    if (TypeId == -1)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }


        }

        public TestModeEquipmentParameters[] GetCurrentEquipmentInf(string EquipmentID)
        {

            TestModeEquipmentParameters[] pEquipmentStruct;
            DataTable DtEquipmentParameter = new DataTable();


            string Selectconditions = null;
            string StrTableName = "TopoEquipment";


            StrTableName = "TopoEquipmentParameter";

            Selectconditions = "select * from " + StrTableName + " where PID= " + EquipmentID + " order by ID";

            DtEquipmentParameter = MyDataio.GetDataTable(Selectconditions, StrTableName);

            pEquipmentStruct = new TestModeEquipmentParameters[DtEquipmentParameter.Rows.Count];

            SortedList<string, string> MyEquipmentInfList = new SortedList<string, string>();

            int i = 0;
            foreach (DataRow drEquipmentParameter in DtEquipmentParameter.Rows)
            {
                pEquipmentStruct[i].FiledName = drEquipmentParameter["Item"].ToString().Trim().ToUpper();
                pEquipmentStruct[i].DefaultValue = drEquipmentParameter["ItemValue"].ToString().Trim().ToUpper();
                i++;
            }
            return pEquipmentStruct;
        }
        public bool GetIdFromProductionNameTable(string StrProductionName, out int ProductionId, out byte TotalChannel, out Byte TotalVccCount,out byte TotalTempCount , out string StrAuxAttribles)
        {

               ProductionId = -1;

          

                string StrTableName = "GlobalProductionName";

                string Selectconditions = "select * from " + StrTableName + " where PN='" + StrProductionName + "' order by ID";
                // string Select_conditions = "select * from " + Str_Table_Name + " where Production_Name=" + Str_Production_Name + " order by ID";

                DT.Clear();
                DT = MyDataio.GetDataTable(Selectconditions, StrTableName);


                int s = (int)DT.Rows[0]["ID"];

                string ss = s.GetType().ToString();
                ProductionId = (int)DT.Rows[0]["ID"];
                TotalChannel = Convert.ToByte(DT.Rows[0]["Channels"]);
                TotalVccCount = Convert.ToByte(DT.Rows[0]["Voltages"]);
                TotalTempCount = Convert.ToByte(DT.Rows[0]["Tsensors"]);
                StrAuxAttribles = DT.Rows[0]["AuxAttribles"].ToString();
                MCoefsID = Convert.ToInt32(DT.Rows[0]["MCoefsID"]);
               


            if (ProductionId == -1)
            {
                return false;
            }
            else
            {
                return true;
            }



        }
        public string[,]GetEquipmentNameList(int idTestplan)
        {
            int i = 0;
            string StrTableName = "TopoEquipment";

            string Selectconditions = "select * from " + StrTableName + " where PID=" + idTestplan + " order by Seq";

            DT.Clear();

            DT = MyDataio.GetDataTable(Selectconditions, StrTableName);
            string[,] pEquipment = new string[DT.Rows.Count, 2];

            for (i = 0; i < DT.Rows.Count; i++)
            {
                switch (DT.Rows[i]["Role"].ToString().ToUpper())// 0=NA,1=TX,2=RX
                {
                    case "2":
                        pEquipment[i, 0] = DT.Rows[i]["ItemName"].ToString().ToUpper()+"_RX";
                        break;
                    case "1":
                        pEquipment[i, 0] = DT.Rows[i]["ItemName"].ToString().ToUpper() + "_TX";
                        break;
                    default:
                        pEquipment[i, 0] = DT.Rows[i]["ItemName"].ToString().ToUpper() + "_NA";
                        break;


                }
                pEquipment[i, 0] +="_"+ DT.Rows[i]["ItemType"].ToString().ToUpper();
                pEquipment[i,1]= DT.Rows[i]["ID"].ToString().ToUpper();
              
            }
            return pEquipment;
        }

        public bool WriterEndTimeToSNTable()
        {
           
            string StrTableName = "TopoRunRecordTable";
            string strSelectconditions = "Select * from TopoRunRecordTable  where ID=" + SNID;
            string StrDataioType = MyDataio.GetType().ToString().ToUpper();
            try
            {
                DT = MyDataio.GetDataTable(strSelectconditions, StrTableName);
                if (DT.Rows.Count == 1)
                {
                    if (StrDataioType == "ATS.LOCALDATABASE")
                    {
                        DT.Rows[0]["EndTime"] = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"); ;     //140605_0
                    }
                    else
                    {
                        DT.Rows[0]["EndTime"] = MyDataio.GetCurrTime().ToString("yyyy/MM/dd HH:mm:ss");      //140605_0
                    }
                    MyDataio.UpdateDataTable(strSelectconditions, DT);
                }
                return true;

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        #endregion
#region  Lock Queue

        
       //public  Queue<string> QueueInterfaceLog= new Queue<string> { };
       //public  Queue<string> QueueSqlLog = new Queue<string> { };
       //public  Queue<string> QueueShow = new Queue<string> { };

      

#endregion

    }
   
}
      
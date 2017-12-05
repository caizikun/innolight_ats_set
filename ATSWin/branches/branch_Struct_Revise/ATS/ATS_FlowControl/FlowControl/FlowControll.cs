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
using System.Threading.Tasks;
namespace ATS
{



    public class CurrentTestModelEquipmentList : EquipmentList
    {

    }
    public struct TestplanPrameter
    {
        public int Id;
        public string FwVersion;
        public string HwVersion;
        public static byte DutUsbPort;
        public bool Flag_NeedInitializeDrvier;
        public bool Flag_Need_EEPROMInitialize;
        public bool Flag_IgnoreRecordCoef;
        public bool flag_NeedCheckSn;
        public string TestplanDescription;
        public int TestplanVersion;
        public bool IsCDRon ;

    }
    public struct PnPrameter
    {
        public int Id;
        public int pnName;
        public string ItemName;
        public int MCoefsID;
        public bool OldDriver;
        public double MaxRate;
        public String  Publish_PN;
        public string NickName;
       
      

    }
    public class FlowControll// Flow Controll
    {
       public  TestplanPrameter  StuctTestplanPrameter=new TestplanPrameter();
       public PnPrameter StuctPnPrameter = new PnPrameter();

      #region Interface

       private JudgeSpec MyJudgeSpec;
       public  LogQueue<string> QueueInterfaceLog= new LogQueue<string> { };
       public  LogQueue<string> QueueShow = new LogQueue<string> { };
       public LogQueue<string> ErrorQueue = new LogQueue<string> { };// ErrorLog
       //public logManager MyLogManager;
       //public logManager MyErrorLogManager;
       public string CurrentSN;
       public string Current_DutFwRev;

       public bool flag_Normal_Lab = true;//true=正常实验产品,false=测试过程中穿擦进去的
       public bool flag_NeedCheckFw = false;//是否需要坚持Fw版本号
       public bool flag_FwVerOK=false;    // Fw版本号是否正确
       public bool flag_SNOK = false;    //序列号是否正常
       public bool flag_EvbOK = false;
       public bool Flag_NeedAutoCheck_EvBVoltage = false;
       private bool flag_CurrentTestModel_FailBreak = false;//当前Testmodel 是否要FailBreak
       //public bool FlagEvbVcAdcCal = false;//True:表示EVBVcAdc 被Cal过,false则表示未曾Cal
       
      #endregion
      #region FlowControll

        //----------------E:\WORK\正在进行中的项目\ATS Project\ATS Code\ATS(Win)\branches\branch_Struct_Revise\ATS\ATS_Framework\TestModel\AdjustAPD.cs
        public byte TotalChannel = 0;
        public byte TotalTempCount = 0;
        public byte TotalVccCount = 0;
        public string StrAuxAttribles = "";
       // public byte ApcStyle = 0;
        public String StrLightSourceErArray="0,0,0,0";
        public String StrLightSourceVecpArray = "0,0,0,0";
        public string StrTempADCTolerance = "10";
        //----------------
       // public int ProductionTypeId;
        public int ProductionNameId;
      
        public int SNID=-1;
       // public int MCoefsID = -1;
        public int MSAID = -1;
       // private int CondtionID;
        public string ProductionTypeName;
        public string StrProductionName;
       // public string SerialNO;

        public string[] StrProductionTypeList;
       // private DataTable DtMyTestPlan;// 放入Condition Table的所有数据
        public DataTable DtTotalConditionList;// 放入Condition Table的所有数据
        public DataTable DtTotalTestModelList;// 放入TestModel Table的所有数据
        public DataTable DtTotalTestModelParameterList;// 放入TestModel Table的所有数据
       // private DataTable DtMyTestModel;// 放入 各个条件下 TestModel 的列表,
        private DataTable DtMyTestModelParameter;// 各个TestModel的参数
        private DataTable DtMyDutInf;
        public bool StopFlag = false;// 标志位适用于立即中断测试

        public bool TotalResultFlag = true;//全部测试结果的标志位
        private bool CurrentTestModelResultflag = true;//当前TestMode 测试结果标志位
        public bool CurrentConditionResultflag = true;//当前condition 测试结果标志位
       // public DataTable DtLogData;

       // private DataTable dtCurrentConditionTestData;
        //private DataTable dtCurrentTestModelTestData;
        public DataTable dtCurrentConditionResultData;
        private DataTable dtCurrentTestModelList;
        private DataTable dtCurrentTestModeParameterlList;
        public String StrStartTime;

        private DataRow drCondition;
        //private DataRow drTestModel;
        public bool FlagFlowControllEnd = false;
        public int prosess;
        public DataTable TotalTestData;
        public DataTable dtLPTotalTestData;
        private bool FlagDrvierInitializeOK=true;
        private bool Flag_EEPROM_InitializeOK = true;
     //   public bool FlagDrvierNeedInitialize = false;
        private int TempWaitTime;
		private bool IsChangeTempEnd = false;
      //  public bool FlagIgnoreRecordCoef = true;
      //  public bool Flag_Need_EEPROMInitialize = false;
        //public string StrSqlSwVersion = "NA";

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
        public double DutDataRate;
        public byte DutPRBS;
        public int CurrentCtrlType;       //1:LP  2:FMT  3:LP + FMT 
        private String StrCurrentConditionName;
        public string StrIpAddress="";

       // private TestModeEquipmentParameters[] TestModelOutput;
        private TestModeEquipmentParameters[] TestModelInputArray;// TestModel 输入数组
        private TestModeEquipmentParameters[] TestModelOutputArray;// TestModel 输出数组

        //---------------------数据处理中的一些临时变量

        //-------------------
        #endregion
      
	  #region Algorithmodel

       
        public RxAlgorithmModel pAppRx;

        #endregion
      #region Definition Equipment
           public DUT pDut;
           public Powersupply pPS;
           public Thermocontroller pThermocontroller;
        #endregion

        public EquipmentList MyEquipList;
        public EquipmentManage MyEquipmentManage;
        public TestModelList MyTestModelList;
        public TestModelManage MyTestModelManage;
        public SortedList<string, string> EquipmenNameArray;
        public globalParameters pGlobalParameters = new globalParameters();
        public string StrElecEyeDiagramPath;
        public string StrOptEyeDiagramPath;
        public string StrPathPolarityEyeDiagram;
        public string StrPathErrorLog;

#region   Local Xml

        public string StrPathLocalDirectory;
        public string StrPathBackUpirectory;
        public string StrPathLocalXml;
        public  LocatDataXml  MyLocatDataXml;

        CancellationTokenSource tokenSource = new CancellationTokenSource();
        CancellationToken token ;//= tokenSource.Token;

        //token= tokenSource.Token
        //public XElement NodeSN;
        //public XElement NodeTopoLog;
        //public XElement NodeTestModel;

        //public XElement NodeTestModelProcessData;
        //public XElement NodeTestModelTestData;

#endregion
        //------------------------------
        public string StrErrorItemMessage;
        public DataIO MyDataio;
      //  public DataTable dtPnInf=new DataTable();
        public DataTable dtPnSpec = new DataTable();
        public DataTable dtEquipmentInf = new DataTable();

        public TimeSpan TimeSpanStart;
        public double StartTemp=25;
        public DataTable dtCurrentConditionPrameter;
        public FlowControll(ConfigXmlIO MyXml)
        {

            MyEquipmentManage = new EquipmentManage();
            MyEquipList = new EquipmentList();
            MyTestModelList = new TestModelList();
            MyTestModelManage = new TestModelManage();
            EquipmenNameArray = new SortedList<string, string>();
         //   EquipmenNameArray = GetEquipmentNameList(TestPlanId);
            Log.SaveLogToTxt("The EXE Start......");
           
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


            //dtCurrentTestModelTestData = new DataTable();
            dtCurrentConditionResultData = new DataTable();
            dtCurrentTestModelList = new DataTable();
            dtCurrentTestModeParameterlList = new DataTable();
            //-----------------------------------

            TotalTestData = new DataTable();
			
            DtTotalConditionList = new DataTable();
            DtMyTestModelParameter = new DataTable();
            DtMyDutInf = new DataTable();
            MyJudgeSpec = new JudgeSpec();
            dtLPTotalTestData = new DataTable();
        }

         //
        // 作用:
        // 建立测试列表。
      
        
		public bool GetAllConditionList()
        {

            DataTable dt = new DataTable();
            DataRow dr;
            DtTotalConditionList.Clear();
            DtMyTestModelParameter.Clear();
            //---------------------------------------------------------Condition List
            string StrTableName = "TopoTestControl";
            //string StrSelectconditions = "select * from " + StrTableName + " where PID=" + TestPlanId + " order by SEQ ASC";
            string StrSelectconditions = "select * from " + StrTableName + " where PID=" + StuctTestplanPrameter.Id + " and IgnoreFlag=0" + " order by SEQ ASC";

            dt = MyDataio.GetDataTable(StrSelectconditions, StrTableName);// 获得环境的DataTable

            //---------------------------组件全新的DtTotalConditionList
            if (DtTotalConditionList.Columns.Count == 0)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    DtTotalConditionList.Columns.Add(dt.Columns[i].ColumnName.ToString());
                }
            }

            for (int RowNo = 0; RowNo < dt.Rows.Count; RowNo++)
            {
                if (dt.Rows[RowNo]["Channel"].ToString() == "0")
                {
                    for (int j = 0; j < pGlobalParameters.TotalChCount; j++)
                    {
                        dr = DtTotalConditionList.NewRow();
                        for (int k = 0; k < dt.Columns.Count; k++)
                        {
                            dr[k] = dt.Rows[RowNo][k];
                        }
                        dr["Channel"] = j + 1;
                        DtTotalConditionList.Rows.Add(dr);
                    }
                }
                else
                {
                    dr = DtTotalConditionList.NewRow();
                    for (int k = 0; k < dt.Columns.Count; k++)
                    {
                        dr[k] = dt.Rows[RowNo][k];
                    }
                    DtTotalConditionList.Rows.Add(dr);
                }
                //---------------------------给DtTotalConditionList中的Condition 排序 
            }
            for (int RowNo1 = 0; RowNo1 < DtTotalConditionList.Rows.Count; RowNo1++)
            {
                DtTotalConditionList.Rows[RowNo1]["SEQ"] = RowNo1 + 1;
            }

            return true;
        }

        public bool GetAllTestModelList()
        {

            DtTotalTestModelList = null;

           string StrTableName = "TopoTestControl";
            string StrSelectconditions = "select B.ItemName,A.*,C.* , A.ID AS TestmodelID,c.ItemName as AppType from  TopoTestModel A,GlobalAllTestModelList B,GlobalAllAppModelList C "
            +" where C.ID=B.PID and A.IgnoreFlag=0  AND A.GID=B.ID AND "
            + "exists (select ID from TopoTestControl C where C.PID = "+StuctTestplanPrameter.Id +"and C.ID=A.PID and C.IgnoreFlag =0) "
            +" order by SEQ ASC";

            DtTotalTestModelList = MyDataio.GetDataTable(StrSelectconditions, StrTableName);// 获得环境的DataTable

            if (DtTotalTestModelList==null|| DtTotalConditionList.Columns.Count == 0)
            {
                return false;
            }

            return true;
        }

        public bool GetAllTestModelParamterList()
        {

            DtTotalTestModelParameterList = null;

            string StrTableName = "TopoTestControl";
            string StrSelectconditions = "SELECT  TopoTestParameter.PID as TestmodelId,* FROM TopoTestParameter,GlobalTestModelParamterList "
                                       + "Where TopoTestParameter.GID=GlobalTestModelParamterList.ID and  "
                                           + " exists( select  id from TopoTestModel M where M.ID =TopoTestParameter.PID and M.IgnoreFlag=0 and "
                                                    + "exists (select ID from TopoTestControl C where C.PID =" + StuctTestplanPrameter.Id + " and C.ID=M.PID and C.IgnoreFlag =0))";

            DtTotalTestModelParameterList = MyDataio.GetDataTable(StrSelectconditions, StrTableName);// 获得环境的DataTable

            if (DtTotalTestModelParameterList == null || DtTotalTestModelParameterList.Columns.Count == 0)
            {
                return false;
            }

            return true;
        }

		
        private DataTable GetCurrentConditionPrameter(DataTable dtTestModelList)
        {
            DataTable dt=new DataTable();
            if (dtTestModelList.Rows.Count>0)
            {
                String SS="";
                for (int i = 0; i < dtTestModelList.Rows.Count;i++ )
                {
                    SS += dtTestModelList.Rows[i]["id"].ToString();
                    if (i < dtTestModelList.Rows.Count-1)
                    {
                        SS += ",";
                    }
                }

                //String StrMaxid=dtTestModelList.Rows[dtTestModelList.Rows.Count-1]["ID"].ToString();
                //String StrMinid = dtTestModelList.Rows[0]["ID"].ToString();
                string Str = "SELECT  TopoTestParameter.PID as TestmodelId,* FROM TopoTestParameter,GlobalTestModelParamterList Where  TopoTestParameter.GID=GlobalTestModelParamterList.ID and  (TopoTestParameter.PID in ( "+SS+ "))";
                dt = MyDataio.GetDataTable(Str, "TopoTestParameter");// 获得环境的DataTable
            }
            return dt;
        }

        private DataTable SelectTestModelList( )
        {
            DataTable dt = DtTotalTestModelList.Clone();

            try
            {
                //String StrMaxid=dtTestModelList.Rows[dtTestModelList.Rows.Count-1]["ID"].ToString();
                //String StrMinid = dtTestModelList.Rows[0]["ID"].ToString();
                DataRow[] drArray = DtTotalTestModelList.Select("PID='" + CurrentConditionId + "'");

                for (int i = 0; i < drArray.Length; i++)
                {
                    dt.Rows.Add(drArray[i].ItemArray);
                }

                return dt;
            }
            catch
            {
                return dt;
            }
        }
		
        public bool StartTest()
        {

               #region      进入测试前的准备

           // TimeSpan TStartTime = new TimeSpan(DateTime.Now.Ticks);
           
            MyJudgeSpec.dtSpec = dtPnSpec;
            pEnvironmentcontroll.LastTemp = -1000;
            pEnvironmentcontroll.LastVoltage = -3.3;
            StopFlag = false;
            
            //dtCurrentTestModelTestData.Clear();
            dtCurrentConditionResultData.Clear();
            dtCurrentTestModelList.Clear();
            dtCurrentTestModeParameterlList.Clear();
            dtLPTotalTestData.Clear();
            for (int i=0;i<MyEquipList.Count;i++)// 开启所有仪器开关
            {
                MyEquipList[MyEquipList.Keys[i]].OutPutSwitch(true);
            }
          
           pDut.FullFunctionEnable();
            TimeSpanUse.DateDiff(TimeSpanStart, "FlowControl Start 至 Change Channel 完成 ", out TimeSpanStart);

            //pGlobalParameters.TotalTempCount = TotalTempCount;
            //pGlobalParameters.TotalVccCount = TotalVccCount;
            pGlobalParameters.CurrentSN = CurrentSN;
            pGlobalParameters.PN = StrProductionName;
            pGlobalParameters.StrPathEEyeDiagram = StrElecEyeDiagramPath;
            pGlobalParameters.StrPathOEyeDiagram = StrOptEyeDiagramPath;
            pGlobalParameters.StrPathPolarityEyeDiagram = StrPathPolarityEyeDiagram;
            pGlobalParameters.OpticalSourseERArray = StrLightSourceErArray;
            pDut.GetRegistValueLimmit(0, out pGlobalParameters.IbiasRegistDacValueMax);
            pDut.GetRegistValueLimmit(1, out pGlobalParameters.ImodRegistDacValueMax);
            TotalTestData.Clear();
            if (TotalTestData.Columns.Count == 0)
            {
                TotalTestData.Columns.Add("Temp");
                TotalTestData.Columns.Add("Vcc");
                TotalTestData.Columns.Add("Channel");
                TotalTestData.Columns.Add("ItemName");
                TotalTestData.Columns.Add("ItemValue");
                TotalTestData.Columns.Add("SpecMax");
                TotalTestData.Columns.Add("SpecMin");
                TotalTestData.Columns.Add("Result");
            }
            dtLPTotalTestData.Clear();
            dtLPTotalTestData = TotalTestData.Clone();
            prosess = 0;
            MyTestModelManage.SetDutObject(pDut);
            //buildConditionList();//从数据库读取FlowControl
            MyTestModelList.Clear();

            TimeSpanUse.DateDiff(TimeSpanStart, "Change Channel 完成 到 Driver Initialize 之前", out TimeSpanStart);
#endregion

            try
            {


              //  TStartTime = new TimeSpan(DateTime.Now.Ticks);

                #region Driver 初始化

                if (StuctTestplanPrameter.Flag_NeedInitializeDrvier && pDut.DriverInitStruct.Length>0)
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

                if (StuctTestplanPrameter.Flag_Need_EEPROMInitialize && pDut.EEpromInitStruct.Length > 0)
                {
                    QueueShow.Enqueue("EEPROM  Initializing........");

                    try
                    {
                        Flag_EEPROM_InitializeOK = true;
                        Flag_EEPROM_InitializeOK = pDut.EEpromInitialize();

                    }

                    catch
                    {
                        Flag_EEPROM_InitializeOK = false;
                       
                    }

                    if (!Flag_EEPROM_InitializeOK)
                    {
                        MessageBox.Show("EEPORM Initialize  Error");
                        Exception ex = new Exception("EEPORM Initialize  Error");
                        throw ex;
                    }
                }


                if (StuctTestplanPrameter.Flag_Need_EEPROMInitialize || StuctTestplanPrameter.Flag_NeedInitializeDrvier)
                {

                    pPS.OutPutSwitch(false);
                    //Thread.Sleep(200);
                    pPS.OutPutSwitch(true);
                    // pDut.SetDutDataRate(DutDataRate);
                    pDut.FullFunctionEnable();
                }
                QueueShow.Enqueue("Start Testing........"); 
#endregion

                
              
                TimeSpanUse.DateDiff(TimeSpanStart, "FlowControl  Driver  Initialize ", out TimeSpanStart);

                #region Condition 遍历Conditions

                for (int ConditionSEQ = 0; ConditionSEQ < DtTotalConditionList.Rows.Count; ConditionSEQ++)//遍历测试环境条件
                {
                    
                    #region           对于当前FlwoControl数据的解析和Log记载
                    TimeSpanStart = new TimeSpan(DateTime.Now.Ticks);
                    CurrentConditionResultflag = true;
                    dtCurrentConditionResultData.Clear();
                    dtCurrentTestModelList.Clear();
                    dtCurrentTestModeParameterlList.Clear();
                    dtCurrentConditionResultData = TotalTestData.Clone();
                   
                    StrStartTime = MyDataio.GetCurrTime().ToString("yyyy/MM/dd HH:mm:ss");
                    drCondition = DtTotalConditionList.Rows[ConditionSEQ];

                    CurrentConditionId = Convert.ToInt32(drCondition["ID"]);// 记录当前ConditionID号码
                    StrSqlLog = "";
                    string ss = drCondition["SEQ"].ToString();
                    CurrentChannel = Convert.ToByte(drCondition["Channel"]);
                    StrCurrentTemp = Convert.ToDouble(drCondition["Temp"]);
                    StrCurrentVcc = Convert.ToDouble(drCondition["Vcc"]);
                    DutDataRate = Convert.ToDouble(drCondition["DataRate"]);
                    DutPRBS = Convert.ToByte(drCondition["Pattent"]);
                    StrCurrentConditionName = drCondition["ItemName"].ToString();
                    CurrentCtrlType = Convert.ToInt32(drCondition["CtrlType"]);           //20150414
                    TempWaitTime = Convert.ToInt32(drCondition["TempWaitTimes"]);
                    double TempOffset=Convert.ToDouble(drCondition["TempOffset"]);
                    pGlobalParameters.CurrentChannel = Convert.ToByte(CurrentChannel);
                    pGlobalParameters.CurrentTemp = Convert.ToDouble(StrCurrentTemp);
                    pGlobalParameters.CurrentVcc = Convert.ToDouble(StrCurrentVcc);

                    if (ConditionSEQ == 0) StartTemp = pGlobalParameters.CurrentTemp;

                    //MyDataio.WriterLog(CurrentCtrlType, SNID, "", StrStartTime, StrStartTime, Convert.ToSingle(StrCurrentTemp), Convert.ToSingle(StrCurrentVcc), CurrentChannel, false, out CurrentLogId);

                    this.MyLocatDataXml.CreartTopoLogNode( StrCurrentTemp.ToString(), StrCurrentVcc.ToString(), CurrentChannel.ToString(), CurrentCtrlType.ToString(), DateTime.Now.ToString());
   
                    QueueShow.Enqueue("Change Environment....." + "Temp= " + StrCurrentTemp + " ** VCC= " + StrCurrentVcc + " ** Channel=" + CurrentChannel);

                    pDut.DutDatarate = DutDataRate;
                    pDut.DutPRBS = DutPRBS;
                    if(pEnvironmentcontroll.pEquipmentList==null) pEnvironmentcontroll.SelectEquipment(MyEquipList);

                    TimeSpanUse.DateDiff(TimeSpanStart, "环境准备前的For 循环", out TimeSpanStart);

                    if (!pEnvironmentcontroll.ConfigEnvironmental(Convert.ToDouble(StrCurrentTemp), Convert.ToDouble(StrCurrentVcc), Convert.ToByte(CurrentChannel), TempOffset, 0, pGlobalParameters.OverLoadPoint))// 设定当前的环境 以及告诉DUT 当前为通道几
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

                   // dtCurrentConditionPrameter = GetCurrentConditionPrameter(CurrentConditionId);
                    dtCurrentTestModelList = SelectTestModelList();

                    QueueShow.Enqueue("Temp Waiting........");

                    if (CurrentCtrlType == 2)
                    {
                        pPS.OutPutSwitch(false);
                        //Thread.Sleep(200);
                        pPS.OutPutSwitch(true);
                    }
                    if (pEnvironmentcontroll.NeedxStreamflag)
                    {

                        if (pEnvironmentcontroll.LastTemp != pGlobalParameters.CurrentTemp)
                        {
                            for (int k = 0; k < TempWaitTime; k++)
                            {
                                Thread.Sleep(1000);
                                prosess = Convert.ToInt16(100 * k / TempWaitTime);

                            }

                            pEnvironmentcontroll.LastTemp = pGlobalParameters.CurrentTemp;
                        }
                        else
                        {
                            Thread.Sleep(1000);
                        }

                    }

                    if (CurrentCtrlType == 2)
                    {
                     
                        pDut.FullFunctionEnable();
                    }
					

#endregion					
                    prosess = (ConditionSEQ ) * 100 / DtTotalConditionList.Rows.Count;// 在温度等待时候，此值被修改，此时被重新赋值
                    QueueInterfaceLog.Enqueue("Temp= " + StrCurrentTemp + " ** VCC= " + StrCurrentVcc + " ** Channel=" + CurrentChannel + "****" + StrCurrentConditionName);
                    StrSqlLog += "Temp= " + StrCurrentTemp + " ** VCC= " + StrCurrentVcc + " ** Channel=" + CurrentChannel + "****" + StrCurrentConditionName + "**";
                  //  MyErrorLogManager.AdapterLogString("Temp= " + StrCurrentTemp + " ** VCC= " + StrCurrentVcc + " ** Channel=" + CurrentChannel + "****" + StrCurrentConditionName + "**");
                 #endregion

                    #region TestModel  运行当前Condition中的所有TestModel

                 
                    TimeSpanUse.DateDiff(TimeSpanStart, "环境准备到TestModel循环", out TimeSpanStart);

                    for (int TestModelNo = 0; TestModelNo < dtCurrentTestModelList.Rows.Count; TestModelNo++)// 遍历Condition中的TestModel
                   {

                       //TStartTime = new TimeSpan(DateTime.Now.Ticks);
                       // Thread.Sleep(200);
                        StrInputLog = "";
                        StrOutputLog = "";

                        CurrentTestModelResultflag = true;
                     
                        Log.SaveLogToTxt("Temp=" + StrCurrentTemp + "**" + "VCC=" + StrCurrentVcc + "**" + "Channel=" + CurrentChannel);

                        DataRow drTestModel = dtCurrentTestModelList.Rows[TestModelNo];
                        string CurrentTestModelId = drTestModel["TestmodelID"].ToString();
                        string StrTestModelName = drTestModel["ItemName"].ToString();
                        string StrTestModelType = drTestModel["AppType"].ToString();

                        flag_CurrentTestModel_FailBreak =Convert.ToBoolean( drTestModel["Failbreak"]);
                        Log.SaveLogToTxt(StrTestModelName + "TestModelID=" + CurrentTestModelId);
                        //  pWriteTxt.Write(StrTestModelName + "TestModelID="+CurrentTestModelId);
                        QueueInterfaceLog.Enqueue("Temp=" + StrCurrentTemp + "**" + "VCC=" + StrCurrentVcc + "**" + "Channel=" + CurrentChannel + "**" + StrTestModelName);
                     //   MyErrorLogManager.AdapterLogString("Temp=" + StrCurrentTemp + "**" + "VCC=" + StrCurrentVcc + "**" + "Channel=" + CurrentChannel + "**" + StrTestModelName);

                        TimeSpanUse.DateDiff(TimeSpanStart, "TestModel循环 到 RunTestModel 之前 " + StrTestModelName, out TimeSpanStart);


                        try
                        {

                            if (!RunTestModel(StrTestModelName, CurrentTestModelId,StrTestModelType)) CurrentConditionResultflag=false;
                        }
                        catch (Exception EE)
                        {
                            CurrentConditionResultflag = false;
                            Log.SaveLogToTxt(StrTestModelName + EE.Message);

                        }
                        finally
                        {
                            //Log.SaveLogToTxt(StrTestModelName+"Code Error!");
                           
                        }
                        //MyErrorLogManager.FlushLogBuffer();
                        TimeSpanUse.DateDiff(TimeSpanStart, StrTestModelName + " RunTime ", out TimeSpanStart);
                       
                        if (StopFlag)
                        {
                            TotalResultFlag = false;
                            break;
                        }



                    }

                    #endregion
     Error:
                    #region 将当前数据填充到TotalTestData,并且完成当前Condition的数据存档

                    TimeSpanStart = new TimeSpan(DateTime.Now.Ticks);

                    TotalTestData.Merge(dtCurrentConditionResultData);

                    if (CurrentCtrlType==2 && dtCurrentConditionResultData.Rows.Count==0)
                    {
                        CurrentConditionResultflag = false;
                    }
                    string strEndTime = MyDataio.GetCurrTime().ToString("yyyy/MM/dd HH:mm:ss");

                   // MyDataio.WriterLog(CurrentCtrlType, SNID, StrErrorItemMessage, StrStartTime, strEndTime, Convert.ToSingle(StrCurrentTemp), Convert.ToSingle(StrCurrentVcc), CurrentChannel, CurrentConditionResultflag, out CurrentLogId);
                    UpdataEndTimeToLogTable();
                   // Thread.Sleep(3000);
                   // if (dtCurrentConditionResultData.Rows.Count > 0) MyDataio.WriteResult(CurrentLogId, dtCurrentConditionResultData);
                    prosess = (ConditionSEQ + 1) * 100 / DtTotalConditionList.Rows.Count;

                    GC.Collect();
                    if (StopFlag)
                    {
                        TotalResultFlag = false;
                        break;
                    }

                    TimeSpanUse.DateDiff(TimeSpanStart, "当前数据填充到TotalTestData ", out TimeSpanStart);
                       
       #endregion
                }


               
                #region  存档Coef && EEPROM

                if (!StopFlag)
                {
                    if (!StuctTestplanPrameter.Flag_IgnoreRecordCoef)
                    { 
                        QueueShow.Enqueue("Record Coef....,Wait for a Moment");
                        DutCoefValueStuct[] CoefStruct;
                        DutEEPROMInitializeStuct[] EepromStruct;
                        //&& pDut.ReadALLEEprom(out EepromStruct)
                        if (!pDut.ReadALLcoef(out CoefStruct) )
                        {
                            Log.SaveLogToTxt("Dut Read All Coef Error");

                            MessageBox.Show("Recorde Coef Error");
                            QueueShow.Enqueue("Record Coef Error");
                            Log.SaveLogToTxt("Recorde Coef Error");
                            TotalResultFlag = false;

                        }
                        else
                        {
                            if (!Record_DutCoef(SNID, CoefStruct))
                            {
                                MessageBox.Show("Recorde Coef Error");
                                TotalResultFlag = false;
                                QueueShow.Enqueue("Record Coef Error");
                                Log.SaveLogToTxt("Recorde Coef Error");
                                TotalResultFlag = false;
                            }
                        }

                        if (!pDut.ReadALLEEprom(out EepromStruct))
                        {
                            Log.SaveLogToTxt("Dut Read All Manufactrue EEPROM Error");

                            MessageBox.Show("Recorde EEPROM Error");
                            QueueShow.Enqueue("Record EEPROM Error");
                            Log.SaveLogToTxt("Recorde EEPROM Error");
                            TotalResultFlag = false;
                        }
                        else
                        {
                            if (!Record_Dut_EEPROM(SNID, EepromStruct))
                            {
                                MessageBox.Show("Recorde EEPROM Error");
                                TotalResultFlag = false;
                                QueueShow.Enqueue("Recorde EEPROM Error");
                                Log.SaveLogToTxt("Recorde EEPROM Error");
                            }
                        }
                    }
                }
                #endregion

                TimeSpanUse.DateDiff(TimeSpanStart, "存档Coef+EEPROM ", out TimeSpanStart);
                   

                QueueShow.Enqueue("Update Local Data To Sql Server ,Wait for a Moment....");

                UpdateLocalXml();
                if(!UpdataEndTimeToSNTable())
                {

                    MessageBox.Show("数据存档失败，请检查网络，并且重新测试当前模块！");
                    TotalResultFlag = false;
                }
                
                // MessageBox.Show("FlowControl End");

                pEnvironmentcontroll.Dispose();

                for (int k=0;k<MyEquipList.Count;k++)
                {
                    MyEquipList[MyEquipList.Keys[k].ToString()].Referenced_Times = 0;
                }
               
                Log.SaveLogToTxt("Test End------------------------");
                FlagFlowControllEnd = true;
                pEnvironmentcontroll.LastTemp = -100;

                TimeSpanUse.DateDiff(TimeSpanStart, "测试收尾 ", out TimeSpanStart);
               // Log.SaveLogToTxt(ex.Message + ex.StackTrace);
                Log.SaveLogToTxt("This Dut Test END-------------------------------------------------------------------------");

                return true;
            }
            catch(Exception ex)
            {
                UpdataEndTimeToSNTable();
                // MessageBox.Show("FlowControl End");
                Log.SaveLogToTxt(ex.Message + ex.StackTrace);
               Log.SaveLogToTxt("This Dut Test END-------------------------------------------------------------------------");


               


                pEnvironmentcontroll.LastTemp = -100;
                pEnvironmentcontroll.Dispose();
                Log.SaveLogToTxt("Test End------------------------");
                FlagFlowControllEnd = true;
                return false;
            }
           
        }

        public bool ParallelStartTest()
        {

            #region      进入测试前的准备

            // TimeSpan TStartTime = new TimeSpan(DateTime.Now.Ticks);

            MyJudgeSpec.dtSpec = dtPnSpec;
            pEnvironmentcontroll.LastTemp = -1000;
            pEnvironmentcontroll.LastVoltage = -3.3;
            StopFlag = false;

            //dtCurrentTestModelTestData.Clear();
            dtCurrentConditionResultData.Clear();
            dtCurrentTestModelList.Clear();
            dtCurrentTestModeParameterlList.Clear();
            dtLPTotalTestData.Clear();
            for (int i = 0; i < MyEquipList.Count; i++)// 开启所有仪器开关
            {
                MyEquipList[MyEquipList.Keys[i]].OutPutSwitch(true);
            }
            pDut.FullFunctionEnable();
            TimeSpanUse.DateDiff(TimeSpanStart, "FlowControl Start 至 Change Channel 完成 ", out TimeSpanStart);

            //pGlobalParameters.TotalTempCount = TotalTempCount;
            //pGlobalParameters.TotalVccCount = TotalVccCount;
            pGlobalParameters.CurrentSN = CurrentSN;
            pGlobalParameters.PN = StrProductionName;
            pGlobalParameters.StrPathEEyeDiagram = StrElecEyeDiagramPath;
            pGlobalParameters.StrPathOEyeDiagram = StrOptEyeDiagramPath;
            pGlobalParameters.StrPathPolarityEyeDiagram = StrPathPolarityEyeDiagram;
            pGlobalParameters.OpticalSourseERArray = StrLightSourceErArray;
            pDut.GetRegistValueLimmit(0, out pGlobalParameters.IbiasRegistDacValueMax);
            pDut.GetRegistValueLimmit(1, out pGlobalParameters.ImodRegistDacValueMax);
            TotalTestData.Clear();
            if (TotalTestData.Columns.Count == 0)
            {
                TotalTestData.Columns.Add("Temp");
                TotalTestData.Columns.Add("Vcc");
                TotalTestData.Columns.Add("Channel");
                TotalTestData.Columns.Add("ItemName");
                TotalTestData.Columns.Add("ItemValue");
                TotalTestData.Columns.Add("SpecMax");
                TotalTestData.Columns.Add("SpecMin");
                TotalTestData.Columns.Add("Result");
            }
            dtLPTotalTestData.Clear();
            dtLPTotalTestData = TotalTestData.Clone();
            prosess = 0;
            MyTestModelManage.SetDutObject(pDut);
            //buildConditionList();//从数据库读取FlowControl
            MyTestModelList.Clear();

            TimeSpanUse.DateDiff(TimeSpanStart, "Change Channel 完成 到 Driver Initialize 之前", out TimeSpanStart);
            #endregion

            try
            {
                pDut.FullFunctionEnable();

                //  TStartTime = new TimeSpan(DateTime.Now.Ticks);

                #region Driver 初始化

                if (StuctTestplanPrameter.Flag_NeedInitializeDrvier && pDut.DriverInitStruct.Length > 0)
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

                if (StuctTestplanPrameter.Flag_Need_EEPROMInitialize && pDut.EEpromInitStruct.Length > 0)
                {
                    QueueShow.Enqueue("EEPROM  Initializing........");

                    try
                    {
                        Flag_EEPROM_InitializeOK = true;
                        Flag_EEPROM_InitializeOK = pDut.EEpromInitialize();

                    }

                    catch
                    {
                        Flag_EEPROM_InitializeOK = false;

                    }

                    if (!Flag_EEPROM_InitializeOK)
                    {
                        MessageBox.Show("EEPORM Initialize  Error");
                        Exception ex = new Exception("EEPORM Initialize  Error");
                        throw ex;
                    }
                }


                if (StuctTestplanPrameter.Flag_Need_EEPROMInitialize || StuctTestplanPrameter.Flag_NeedInitializeDrvier)
                {

                    pPS.OutPutSwitch(false);
                    //Thread.Sleep(200);
                    pPS.OutPutSwitch(true);
                    // pDut.SetDutDataRate(DutDataRate);
                    pDut.FullFunctionEnable();
                }
                QueueShow.Enqueue("Start Testing........");
                #endregion



                TimeSpanUse.DateDiff(TimeSpanStart, "FlowControl  Driver  Initialize ", out TimeSpanStart);

                #region Condition 遍历Conditions

                for (int ConditionSEQ = 0; ConditionSEQ < DtTotalConditionList.Rows.Count; ConditionSEQ++)//遍历测试环境条件
                {

                    #region           对于当前FlwoControl数据的解析和Log记载
                    TimeSpanStart = new TimeSpan(DateTime.Now.Ticks);
                    CurrentConditionResultflag = true;
                    dtCurrentConditionResultData.Clear();
                    dtCurrentTestModelList.Clear();
                    dtCurrentTestModeParameterlList.Clear();
                    dtCurrentConditionResultData = TotalTestData.Clone();

                    StrStartTime = MyDataio.GetCurrTime().ToString("yyyy/MM/dd HH:mm:ss");
                    drCondition = DtTotalConditionList.Rows[ConditionSEQ];

                    CurrentConditionId = Convert.ToInt32(drCondition["ID"]);// 记录当前ConditionID号码
                    StrSqlLog = "";
                    string ss = drCondition["SEQ"].ToString();
                    CurrentChannel = Convert.ToByte(drCondition["Channel"]);
                    StrCurrentTemp = Convert.ToDouble(drCondition["Temp"]);
                    StrCurrentVcc = Convert.ToDouble(drCondition["Vcc"]);
                    DutDataRate = Convert.ToDouble(drCondition["DataRate"]);
                    DutPRBS = Convert.ToByte(drCondition["Pattent"]);
                    StrCurrentConditionName = drCondition["ItemName"].ToString();
                    CurrentCtrlType = Convert.ToInt32(drCondition["CtrlType"]);           //20150414
                    TempWaitTime = Convert.ToInt32(drCondition["TempWaitTimes"]);
                    double TempOffset = Convert.ToDouble(drCondition["TempOffset"]);
                    pGlobalParameters.CurrentChannel = Convert.ToByte(CurrentChannel);
                    pGlobalParameters.CurrentTemp = Convert.ToDouble(StrCurrentTemp);
                    pGlobalParameters.CurrentVcc = Convert.ToDouble(StrCurrentVcc);

                    if (ConditionSEQ == 0) StartTemp = pGlobalParameters.CurrentTemp;

                    //MyDataio.WriterLog(CurrentCtrlType, SNID, "", StrStartTime, StrStartTime, Convert.ToSingle(StrCurrentTemp), Convert.ToSingle(StrCurrentVcc), CurrentChannel, false, out CurrentLogId);
                    QueueShow.Enqueue("Change Environment....." + "Temp= " + StrCurrentTemp + " ** VCC= " + StrCurrentVcc + " ** Channel=" + CurrentChannel);

                    this.MyLocatDataXml.CreartTopoLogNode(StrCurrentTemp.ToString(), StrCurrentVcc.ToString(), CurrentChannel.ToString(), CurrentCtrlType.ToString(), DateTime.Now.ToString());
   
                    pDut.DutDatarate = DutDataRate;
                    pDut.DutPRBS = DutPRBS;
                    if (pEnvironmentcontroll.pEquipmentList == null) pEnvironmentcontroll.SelectEquipment(MyEquipList);

                    TimeSpanUse.DateDiff(TimeSpanStart, "环境准备前的For 循环", out TimeSpanStart);

                    if (!pEnvironmentcontroll.ConfigEnvironmental(Convert.ToDouble(StrCurrentTemp), Convert.ToDouble(StrCurrentVcc), Convert.ToByte(CurrentChannel), TempOffset, 0, pGlobalParameters.OverLoadPoint))// 设定当前的环境 以及告诉DUT 当前为通道几
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

                    // dtCurrentConditionPrameter = GetCurrentConditionPrameter(CurrentConditionId);

                    string StrTestModelTableName = "TopoTestModel";
                    string StrTestModelSelectconditions = "select B.ItemName,A.*,C.* , A.ID AS TestmodelID,c.ItemName as AppType from  TopoTestModel A,GlobalAllTestModelList B,GlobalAllAppModelList C where C.ID=B.PID AND A.PID=" + CurrentConditionId + " and A.IgnoreFlag=0  AND A.GID=B.ID order by SEQ ASC";

                    dtCurrentTestModelList = MyDataio.GetDataTable(StrTestModelSelectconditions, StrTestModelTableName);// 获得TestModelList
                    dtCurrentConditionPrameter = GetCurrentConditionPrameter(dtCurrentTestModelList);



                    QueueShow.Enqueue("Temp Waiting........");


                    if (pEnvironmentcontroll.NeedxStreamflag)
                    {

                        if (pEnvironmentcontroll.LastTemp != pGlobalParameters.CurrentTemp)
                        {
                            for (int k = 0; k < TempWaitTime; k++)
                            {
                                Thread.Sleep(1000);
                                prosess = Convert.ToInt16(100 * k / TempWaitTime);

                            }

                            pEnvironmentcontroll.LastTemp = pGlobalParameters.CurrentTemp;
                        }
                    }
                    prosess = (ConditionSEQ) * 100 / DtTotalConditionList.Rows.Count;// 在温度等待时候，此值被修改，此时被重新赋值
                    QueueInterfaceLog.Enqueue("Temp= " + StrCurrentTemp + " ** VCC= " + StrCurrentVcc + " ** Channel=" + CurrentChannel + "****" + StrCurrentConditionName);
                    StrSqlLog += "Temp= " + StrCurrentTemp + " ** VCC= " + StrCurrentVcc + " ** Channel=" + CurrentChannel + "****" + StrCurrentConditionName + "**";
                    //  MyErrorLogManager.AdapterLogString("Temp= " + StrCurrentTemp + " ** VCC= " + StrCurrentVcc + " ** Channel=" + CurrentChannel + "****" + StrCurrentConditionName + "**");
                    #endregion

                    #region TestModel  运行当前Condition中的所有TestModel


                    TimeSpanUse.DateDiff(TimeSpanStart, "环境准备到TestModel循环", out TimeSpanStart);

                    if (CurrentCtrlType == 2)
                    {
                        pPS.OutPutSwitch(false);
                        //Thread.Sleep(200);
                        pPS.OutPutSwitch(true);
                        // pDut.SetDutDataRate(DutDataRate);
                        pDut.FullFunctionEnable();

                        Parallel.For(0, dtCurrentTestModelList.Rows.Count, (int TestModelNo, ParallelLoopState pls) =>
                            {
                                //TStartTime = new TimeSpan(DateTime.Now.Ticks);
                                // Thread.Sleep(200);
                                StrInputLog = "";
                                StrOutputLog = "";

                                CurrentTestModelResultflag = true;

                                Log.SaveLogToTxt("Temp=" + StrCurrentTemp + "**" + "VCC=" + StrCurrentVcc + "**" + "Channel=" + CurrentChannel);

                                DataRow drTestModel = dtCurrentTestModelList.Rows[TestModelNo];
                                string CurrentTestModelId = drTestModel["TestmodelID"].ToString();
                                string StrTestModelName = drTestModel["ItemName"].ToString();
                                string StrTestModelType = drTestModel["AppType"].ToString();

                                flag_CurrentTestModel_FailBreak = Convert.ToBoolean(drTestModel["Failbreak"]);
                                Log.SaveLogToTxt(StrTestModelName + "TestModelID=" + CurrentTestModelId);
                                //  pWriteTxt.Write(StrTestModelName + "TestModelID="+CurrentTestModelId);
                                QueueInterfaceLog.Enqueue("Temp=" + StrCurrentTemp + "**" + "VCC=" + StrCurrentVcc + "**" + "Channel=" + CurrentChannel + "**" + StrTestModelName);
                                //   MyErrorLogManager.AdapterLogString("Temp=" + StrCurrentTemp + "**" + "VCC=" + StrCurrentVcc + "**" + "Channel=" + CurrentChannel + "**" + StrTestModelName);

                                TimeSpanUse.DateDiff(TimeSpanStart, "TestModel循环 到 RunTestModel 之前 " + StrTestModelName, out TimeSpanStart);

                                try
                                {

                                    if (!RunTestModel(StrTestModelName, CurrentTestModelId, StrTestModelType)) CurrentConditionResultflag = false;
                                }
                                catch (Exception EE)
                                {
                                    CurrentConditionResultflag = false;
                                    Log.SaveLogToTxt(StrTestModelName + EE.Message);

                                }
                                finally
                                {
                                    //Log.SaveLogToTxt(StrTestModelName+"Code Error!");

                                }
                                //MyErrorLogManager.FlushLogBuffer();
                                TimeSpanUse.DateDiff(TimeSpanStart, StrTestModelName + " RunTime ", out TimeSpanStart);

                                if (StopFlag)
                                {
                                    TotalResultFlag = false;
                                    //pls.Break();
                                }
                            });
                    }
                    else
                    {
                        for (int TestModelNo = 0; TestModelNo < dtCurrentTestModelList.Rows.Count; TestModelNo++)// 遍历Condition中的TestModel
                        {

                            //TStartTime = new TimeSpan(DateTime.Now.Ticks);
                            // Thread.Sleep(200);
                            StrInputLog = "";
                            StrOutputLog = "";

                            CurrentTestModelResultflag = true;

                            Log.SaveLogToTxt("Temp=" + StrCurrentTemp + "**" + "VCC=" + StrCurrentVcc + "**" + "Channel=" + CurrentChannel);

                            DataRow drTestModel = dtCurrentTestModelList.Rows[TestModelNo];
                            string CurrentTestModelId = drTestModel["TestmodelID"].ToString();
                            string StrTestModelName = drTestModel["ItemName"].ToString();
                            string StrTestModelType = drTestModel["AppType"].ToString();

                            flag_CurrentTestModel_FailBreak = Convert.ToBoolean(drTestModel["Failbreak"]);
                            Log.SaveLogToTxt(StrTestModelName + "TestModelID=" + CurrentTestModelId);
                            //  pWriteTxt.Write(StrTestModelName + "TestModelID="+CurrentTestModelId);
                            QueueInterfaceLog.Enqueue("Temp=" + StrCurrentTemp + "**" + "VCC=" + StrCurrentVcc + "**" + "Channel=" + CurrentChannel + "**" + StrTestModelName);
                            //   MyErrorLogManager.AdapterLogString("Temp=" + StrCurrentTemp + "**" + "VCC=" + StrCurrentVcc + "**" + "Channel=" + CurrentChannel + "**" + StrTestModelName);

                            TimeSpanUse.DateDiff(TimeSpanStart, "TestModel循环 到 RunTestModel 之前 " + StrTestModelName, out TimeSpanStart);
                            
                            try
                            {

                                if (!RunTestModel(StrTestModelName, CurrentTestModelId, StrTestModelType)) CurrentConditionResultflag = false;
                            }
                            catch (Exception EE)
                            {
                                CurrentConditionResultflag = false;
                                Log.SaveLogToTxt(StrTestModelName + EE.Message);

                            }
                            finally
                            {
                                //Log.SaveLogToTxt(StrTestModelName+"Code Error!");

                            }
                            //MyErrorLogManager.FlushLogBuffer();
                            TimeSpanUse.DateDiff(TimeSpanStart, StrTestModelName + " RunTime ", out TimeSpanStart);

                            if (StopFlag)
                            {
                                TotalResultFlag = false;
                                break;
                            }
                        }
                    }

                    #endregion
                    prosess = (ConditionSEQ + 1) * 100 / DtTotalConditionList.Rows.Count;                

                Error:
                    
                    #region 将当前数据填充到TotalTestData,并且完成当前Condition的数据存档

                    TimeSpanStart = new TimeSpan(DateTime.Now.Ticks);

                    TotalTestData.Merge(dtCurrentConditionResultData);

                    if (CurrentCtrlType == 2 && dtCurrentConditionResultData.Rows.Count == 0)
                    {
                        CurrentConditionResultflag = false;
                    }
                    string strEndTime = MyDataio.GetCurrTime().ToString("yyyy/MM/dd HH:mm:ss");

                    // MyDataio.WriterLog(CurrentCtrlType, SNID, StrErrorItemMessage, StrStartTime, strEndTime, Convert.ToSingle(StrCurrentTemp), Convert.ToSingle(StrCurrentVcc), CurrentChannel, CurrentConditionResultflag, out CurrentLogId);
                    UpdataEndTimeToLogTable();
                    // Thread.Sleep(3000);
                    // if (dtCurrentConditionResultData.Rows.Count > 0) MyDataio.WriteResult(CurrentLogId, dtCurrentConditionResultData);
                    prosess = (ConditionSEQ + 1) * 100 / DtTotalConditionList.Rows.Count;

                    GC.Collect();
                    if (StopFlag)
                    {
                        TotalResultFlag = false;
                        break;
                    }

                    TimeSpanUse.DateDiff(TimeSpanStart, "当前数据填充到TotalTestData ", out TimeSpanStart);

                    #endregion
                }
                #endregion


                #region  存档Coef && EEPROM

                if (!StopFlag)
                {
                    if (!StuctTestplanPrameter.Flag_IgnoreRecordCoef)
                    {
                        QueueShow.Enqueue("Record Coef....,Wait for a Moment");
                        DutCoefValueStuct[] CoefStruct;
                        DutEEPROMInitializeStuct[] EepromStruct;
                        //&& pDut.ReadALLEEprom(out EepromStruct)
                        if (!pDut.ReadALLcoef(out CoefStruct))
                        {
                            Log.SaveLogToTxt("Dut Read All Coef Error");

                            MessageBox.Show("Recorde Coef Error");
                            QueueShow.Enqueue("Record Coef Error");
                            Log.SaveLogToTxt("Recorde Coef Error");
                            TotalResultFlag = false;

                        }
                        else
                        {
                            if (!Record_DutCoef(SNID, CoefStruct))
                            {
                                MessageBox.Show("Recorde Coef Error");
                                TotalResultFlag = false;
                                QueueShow.Enqueue("Record Coef Error");
                                Log.SaveLogToTxt("Recorde Coef Error");
                                TotalResultFlag = false;
                            }
                        }

                        if (!pDut.ReadALLEEprom(out EepromStruct))
                        {
                            Log.SaveLogToTxt("Dut Read All Manufactrue EEPROM Error");

                            MessageBox.Show("Recorde EEPROM Error");
                            QueueShow.Enqueue("Record EEPROM Error");
                            Log.SaveLogToTxt("Recorde EEPROM Error");
                            TotalResultFlag = false;
                        }
                        else
                        {
                            if (!Record_Dut_EEPROM(SNID, EepromStruct))
                            {
                                MessageBox.Show("Recorde EEPROM Error");
                                TotalResultFlag = false;
                                QueueShow.Enqueue("Recorde EEPROM Error");
                                Log.SaveLogToTxt("Recorde EEPROM Error");
                            }
                        }
                    }
                }
                #endregion

                TimeSpanUse.DateDiff(TimeSpanStart, "存档Coef+EEPROM ", out TimeSpanStart);

                UpdateLocalXml();

                if (!UpdataEndTimeToSNTable())
                {

                    MessageBox.Show("数据存档失败，请检查网络，并且重新测试当前模块！");
                    TotalResultFlag = false;
                }

                // MessageBox.Show("FlowControl End");

                pEnvironmentcontroll.Dispose();

                for (int k = 0; k < MyEquipList.Count; k++)
                {
                    MyEquipList[MyEquipList.Keys[k].ToString()].Referenced_Times = 0;
                }
                    
                Log.SaveLogToTxt("Test End------------------------");
                FlagFlowControllEnd = true;
                pEnvironmentcontroll.LastTemp = -100;

                TimeSpanUse.DateDiff(TimeSpanStart, "测试收尾 ", out TimeSpanStart);
                // Log.SaveLogToTxt(ex.Message + ex.StackTrace);
                Log.SaveLogToTxt("This Dut Test END-------------------------------------------------------------------------");

                return true;
            }
            catch (Exception ex)
            {
                UpdataEndTimeToSNTable();
                // MessageBox.Show("FlowControl End");
                Log.SaveLogToTxt(ex.Message + ex.StackTrace);
                Log.SaveLogToTxt("This Dut Test END-------------------------------------------------------------------------");





                pEnvironmentcontroll.LastTemp = -100;
                pEnvironmentcontroll.Dispose();
                Log.SaveLogToTxt("Test End------------------------");
                FlagFlowControllEnd = true;
                return false;
            }

        }

        public bool RunTestModel(string StrTestModelName, String StrCurrentTestModelId, String StrTestmodelType)
        {
            DataTable dtCurrentTestModelTestData = new DataTable();
            CurrentTestModelResultflag = true;//
            bool CurrentTestModelflag = false;//TestModel 运行过程是否出错,与测试结果无关系;
            TestModelBase pTestModel;
            try
            {
                QueueShow.Enqueue("Temp=" + StrCurrentTemp + "**" + "VCC=" + StrCurrentVcc + "**" + "Channel=" + CurrentChannel + "**" + StrTestModelName + "Testing....");
                string StrartTime = MyDataio.GetCurrTime().ToString("yyyy/MM/dd HH:mm:ss");
                TestModelBase pCurrentTestModel;

                dtCurrentTestModelTestData.Clear();

                if (!MyTestModelList.Keys.Contains(StrTestModelName.ToUpper().Trim()))
                {
                    pCurrentTestModel = MyTestModelManage.Createobject(StrTestModelName.ToUpper().Trim());
                    MyTestModelList.Add(StrTestModelName.ToUpper().Trim(), pCurrentTestModel);
                    // pCurrentTestModel.specParameters = pCurrentTestModel.SetGetSpecParametersDataTable;
                    pCurrentTestModel.SetGlobalParameters = pGlobalParameters;
                    pCurrentTestModel.SetGetSpecParametersDataTable = dtPnSpec;


                }
                pTestModel = MyTestModelList[StrTestModelName.ToUpper().Trim()];

                if (!pTestModel.SelectEquipment(MyEquipList))// 将其要使用的仪器 加入当前的Testmodel 自己的 sortlist
                {
                    MessageBox.Show(StrTestModelName.ToUpper().Trim() + "缺少必须设备,请检查设备...");
                    Log.SaveLogToTxt(StrTestModelName.ToUpper().Trim() + "缺少必须设备,请检查设备.......");
                    CurrentTestModelResultflag = false;
                    CurrentTestModelflag = false;
                    return CurrentTestModelResultflag;
                }


                SelcetTestModelParameter(StrCurrentTestModelId);

                StrInputLog += "Temp=" + StrCurrentTemp + "**" + "VCC=" + StrCurrentVcc + "**" + "Channel=" + CurrentChannel + "**" + StrTestModelName + "\r\n";
                WriteInputLog();
                pTestModel.SetinputParameters = TestModelInputArray;

                pTestModel.SetGlobalParameters = pGlobalParameters;
                TimeSpanUse.DateDiff(TimeSpanStart, "进入RunTestModel 到 TestModel RunTest之前  ", out TimeSpanStart);
				CurrentTestModelflag = MyTestModelList[StrTestModelName.ToUpper().Trim()].RunTest();

               // MyLogManager.AdapterLogString(1, MyTestModelList[StrTestModelName.ToUpper().Trim()].GetLogInfor);
                 QueueInterfaceLog.Enqueue("Current TestModel:" + CurrentTestModelflag.ToString());
                StrInputLog += "Current TestModel:" + CurrentTestModelflag.ToString() + "\r\n";

                string TestEndTime = MyDataio.GetCurrTime().ToString("yyyy/MM/dd HH:mm:ss");
                string StrSN = CurrentSN + "\r\n"; 
                TestModelOutputArray = MyTestModelList[StrTestModelName.ToUpper().Trim()].GetoutputParameters;
               
                if (TestModelOutputArray == null)
                {
                    Log.SaveLogToTxt("CurrentTestModel OutputPrameter is null ");
                }

                //**************************************************************
                //TestModel Log存入数据库，目前我们换不具备这样的数据库表格
                //SaveTestLog(MyTestModelList[StrTestModelName.ToUpper().Trim()].GetLogInfor);

                //***************************************************************
                //Thread.Sleep(2000);

                bool IsBreakflag = false;
                lock (MyJudgeSpec)
                {
                    MyJudgeSpec.MyGlobalParameters = pGlobalParameters;
                    MyJudgeSpec.Judge(dtCurrentConditionResultData.Clone(), TestModelOutputArray, CurrentTestModelflag, flag_CurrentTestModel_FailBreak, out dtCurrentTestModelTestData, out IsBreakflag, out CurrentTestModelResultflag);
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
                    switch (StrTestmodelType.ToUpper().Trim())
                    {
                        case "APPTXCAL":
                        case "APPRXCAL":
                        case "APPDUTCAL":
                            dtLPTotalTestData.Merge(dtCurrentTestModelTestData);//合并数据
                            break;
                        case "APPRXFMT":
                        case "APPTXFMT":
                        case "APPDUTFMT":
                        case "APPEEPROM":
                            dtCurrentConditionResultData.Merge(dtCurrentTestModelTestData);//合并数据
                            break;
                        default:
                            break;
                    }
                    MyLocatDataXml.CreartTestModelNode(StrTestModelName);

                    #region 从当前TestmodelData中挑选出错误项

                    DataRow[] DrErrorItem = dtCurrentTestModelTestData.Select("Result='fail'");

                    String StrErrorItemMessage = "";

                    foreach (DataRow drFail in DrErrorItem)
                    {
                        double ItemValue, SpecMax;
                        ItemValue = Convert.ToDouble(drFail["ItemValue"]);
                        String ss = drFail["SpecMax"].ToString();


                        SpecMax = Convert.ToDouble(drFail["SpecMax"]);
                        if (ItemValue > SpecMax)
                        {
                            StrErrorItemMessage += drFail["ItemName"].ToString() + " > " + SpecMax.ToString() + ",";
                        }
                        else
                        {
                            StrErrorItemMessage += drFail["ItemName"].ToString() + " < " + drFail["SpecMin"].ToString() + ",";
                        }
                    }

                    if (!SaveTestProData(StrTestModelName, MyTestModelList[StrTestModelName.ToUpper().Trim()].GetProcData))
                    {
                        MessageBox.Show("数据存档失败，请检查网络，并且重新测试当前模块！");
                        StopFlag = true;
                    }

                    if (!SaveTestResult(StrTestModelName, dtCurrentTestModelTestData))
                    {
                        //MessageBox.Show("数据存档失败，请检查网络，并且重新测试当前模块！");
                        StopFlag = true;
                    }

                    #endregion

                    WriteOutputLog();

                    QueueShow.Enqueue("Temp=" + StrCurrentTemp + "**" + "VCC=" + StrCurrentVcc + "**" + "Channel=" + CurrentChannel + "**" + StrTestModelName + "Testing End.....");

                    Log.SaveLogToTxt("Temp=" + StrCurrentTemp + "**" + "VCC=" + StrCurrentVcc + "**" + "Channel=" + CurrentChannel + "**" + StrTestModelName + "Testing End.....");

                    return CurrentTestModelResultflag;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                CurrentTestModelResultflag = false;
                TotalResultFlag = false;
                return false;
            }

        }
        private void FitErrorQueue(TestModelBase pTestModel)
        {
         
           
            List<InnoExCeption>PList=pTestModel.GetException();

            if (PList != null&&PList.Count>0)
            {

                for (int i = 0; i < PList.Count; i++)
                {

                    Log.SaveLogToTxt("ErrorCode=" + PList[i].ID + "Reason=" + PList[i].StackTrace);
                }
                
                Log.SaveLogToTxt("Temp= " + pGlobalParameters.CurrentTemp + " ** VCC= " + pGlobalParameters.CurrentVcc + " ** Channel=" + pGlobalParameters.CurrentChannel + "****" + pTestModel.ToString() + "**");
            
            }
        }
        private void WriteInputLog()
        {
            StrInputLog += "InputLog--------------------"+"\r\n";

            for (int i = 0; i < TestModelInputArray.Length; i++)
            {
                StrInputLog += TestModelInputArray[i].FiledName + "=" + TestModelInputArray[i].DefaultValue+"\r\n";
            }
            Log.SaveLogToTxt(StrInputLog);
           // MyLogManager.AdapterLogString(1,StrInputLog);
        }
        private void WriteOutputLog()
        {
            StrOutputLog += "OutputLog----------------------------------" + "\r\n";
            if (TestModelOutputArray != null)
            {

                for (int i = 0; i < TestModelOutputArray.Length; i++)
                {
                    StrOutputLog += TestModelOutputArray[i].FiledName + "=" + TestModelOutputArray[i].DefaultValue + "\r\n";
                    QueueInterfaceLog.Enqueue(TestModelOutputArray[i].FiledName + "=" + TestModelOutputArray[i].DefaultValue + "\n");

                }
                Log.SaveLogToTxt(StrOutputLog);
            }
           // pWriteTxt.Write(StrOutputLog);
        }
        private bool UpdataEndTimeToSNTable()
        {
            try
            {

                MyLocatDataXml.ModifyRunRecordEndTime();

                return true;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        private bool UpdataEndTimeToLogTable()
        {
            try
            {
                byte Result = Convert.ToByte(CurrentConditionResultflag);
                MyLocatDataXml.AddToplogInf(DateTime.Now.ToString(), Result, StrErrorItemMessage);
                return true;

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        private bool UpdataErrorItem(string StrMessage)
        {
            DataTable DT = new DataTable();
            string StrTableName = "TopoLogRecord";
            string strSelectconditions = "Select * from TopoLogRecord  where ID=" + CurrentLogId;
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
                        DT.Rows[0]["Testlog"] = DT.Rows[0]["Testlog"] + StrMessage;      //140605_0
                    }

                    return MyDataio.UpdateDataTable(strSelectconditions, DT);
                }
                return false;

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        private bool SaveTestResult(String SteTestModelName, DataTable dt)
        {
             List<TopoTestDataColums> T=new List<TopoTestDataColums>();

            try
            {


                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    string ItemName = dt.Rows[i]["ItemName"].ToString();
                    string ItemValue = dt.Rows[i]["ItemValue"].ToString();
                    string SpecMin = dt.Rows[i]["SpecMin"].ToString();
                    string SpecMax = dt.Rows[i]["SpecMax"].ToString();

                    byte Result = 0;
                    if (dt.Rows[i]["Result"].ToString().ToUpper() == "PASS")
                    {
                        Result = 1;
                    }
                   

                    T.Add(new TopoTestDataColums(Result, SpecMax, SpecMin, ItemName, ItemValue));


                }

                MyLocatDataXml.CreartTestData( T);

                return true;
            }
            catch
            {
                return false;
            }           
        }
        private bool SaveTestProData(String SteTestModelName,TestModeEquipmentParameters[] log)
        {

            if (log != null)
            {
                 List<ProcessTableColums> T=new List<ProcessTableColums>();

                for (int i = 0; i < log.Length; i++)
                {
                  
                    T.Add(new ProcessTableColums(SteTestModelName,log[i].FiledName,log[i].DefaultValue));
                   
                }

                MyLocatDataXml.CreartProcessData( T);

                return true;
            }
            else
            {
                Log.SaveLogToTxt("OutPrameter is Null");
            
                return true;
            }

           
           
        }

        #region  Load Xml To Sql

        public bool UpdateLocalXml()
        {
            if (!MyDataio.OpenDatabase(true))
            {
                QueueShow.Enqueue("The Net is Lost,Please Check the Net!!!!");
                MessageBox.Show("Net Lost", "Please Chech Net!!!!!!", MessageBoxButtons.OKCancel);

                Thread.Sleep(3000);
                return true;
            }

            tokenSource = new CancellationTokenSource();

            Task<bool> TempTask = new Task<bool>(() => TaskUpdataXmlToSql());

            TempTask.Start();

            TempTask.Wait(50000);//50 S 

            return true;
        }

        private bool TaskUpdataXmlToSql()
        {



            token = tokenSource.Token;

            try
            {

                var files = Directory.GetFiles(StrPathLocalDirectory, "*", SearchOption.AllDirectories).Where(s => s.EndsWith(".xml"));
                //var files = Directory.GetFiles(@"F:\innolight_ats_set\ATS(Win)\branches\branch_Struct_Revise\ATS\ATS_FlowControl\bin\Debug\CatchData");
                //List<string> ListFilesPath = new List<string>();

                //foreach (string K in files)
                //{
                //    ListFilesPath.Add(K);
                //}


                while (!tokenSource.Token.IsCancellationRequested)//Request Stop from Outside
                {
                    foreach (string filesPath in files)
                    {
                        List<string> SqlCommandList = LocatDataXml.GetSqlCmdFromXml(filesPath);

                        if (!MyDataio.SqlExeSqlTran(SqlCommandList))
                        {
                            return false;
                        }

                        if (!MoveXml(filesPath))
                        {
                            return false;
                        }
                    }

                    break;
                }



                return true;
            }
            catch
            {
                return false;
            }

        }

        private bool MoveXml(string OrignFile)
        {
            string NewFile;

            string[] FilePath = OrignFile.Split('\\');
            string XmlName = FilePath[FilePath.Length - 1].ToString();

            bool IsC = XmlName.ToUpper().Contains(".XML");

            if (!IsC)
            {
                return false;
            }

            NewFile = StrPathBackUpirectory + @"\" + XmlName;

            File.Move(OrignFile, NewFile);

            return true;
        }

        private void LoadXml()
        {

        }


        #endregion        
       
        #region  TestModel

         private bool SelcetTestModelParameter(string StrCurrentTestModelId)  
        {
            SortedList<string, string> pTestModelInputArray = new SortedList<string, string>();


            DataRow[] drArray = DtTotalTestModelParameterList.Select("pid='" + StrCurrentTestModelId + "'");


            TestModelInputArray = new TestModeEquipmentParameters[drArray.Length];

            for (int i = 0; i < drArray.Length; i++)
            {
                TestModelInputArray[i].FiledName = drArray[i]["ItemName"].ToString();
                TestModelInputArray[i].DefaultValue = drArray[i]["ItemValue"].ToString();
            }

            return true;
        }
        
         #endregion
        
        #region  Equipment
        public TestModeEquipmentParameters[] GetCurrentEquipmentInf(string EquipmentID)
        {

            TestModeEquipmentParameters[] pEquipmentStruct;
            DataTable DtEquipmentParameter = new DataTable();


            string Selectconditions = null;
            string StrTableName = "TopoEquipment";

            StrTableName = "TopoEquipmentParameter";
            Selectconditions = "select * from " + StrTableName + " where PID= " + EquipmentID + " order by ID";
            DtEquipmentParameter = MyDataio.GetDataTable(Selectconditions, StrTableName);

            DataRow[] daArray = dtEquipmentInf.Select("id='" + EquipmentID + "'");

            pEquipmentStruct = new TestModeEquipmentParameters[daArray.Length];

           // SortedList<string, string> MyEquipmentInfList = new SortedList<string, string>();

            int i = 0;
            foreach (DataRow drEquipmentParameter in daArray)
            {
                pEquipmentStruct[i].FiledName = daArray[i]["ItemName"].ToString();
                pEquipmentStruct[i].DefaultValue = drEquipmentParameter["ItemValue"].ToString().Trim().ToUpper();
                i++;
            }
            return pEquipmentStruct;
        }
     

        public DataTable EquipmentInf(int idTestplan)
        {
            DataTable DT = new DataTable();
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
                        pEquipment[i, 0] = DT.Rows[i]["ItemName"].ToString().ToUpper() + "_RX";
                        break;
                    case "1":
                        pEquipment[i, 0] = DT.Rows[i]["ItemName"].ToString().ToUpper() + "_TX";
                        break;
                    default:
                        pEquipment[i, 0] = DT.Rows[i]["ItemName"].ToString().ToUpper() + "_NA";
                        break;
                }
                pEquipment[i, 0] += "_" + DT.Rows[i]["ItemType"].ToString().ToUpper();
                pEquipment[i, 1] = DT.Rows[i]["ID"].ToString().ToUpper();

            }
            return null;
        }
#endregion

        #region  Dut Opreat

        public DutStruct[] GetDutManufactureCoefficients()
        {
            DutStruct[] DutInfStruct;
            int i = 0;
            // protected DutStruct[] MyDutStruct;
            DtMyDutInf = new DataTable();

            string StrTableName = "GlobalManufactureCoefficients";

            string StrSelectconditions = "select * from " + StrTableName + " where PID= " + StuctPnPrameter.MCoefsID + " order by ID";
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
                string StrItemType = dr["ItemTYPE"].ToString();

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
                dutStruct.AmplifyCoeff = Convert.ToDouble(dr["AmplifyCoeff"]);
                

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
            DtMyDutInf = new DataTable();
            string StrTableName = "GlobalManufactureChipsetInitialize";
            string StrSelectconditions = "select * from " + StrTableName + " where PID= " + ProductionNameId + " order by ID";
            DtMyDutInf = MyDataio.GetDataTable(StrSelectconditions, StrTableName);// 获得环境的DataTable

            #region 从Testplan 中获取芯片配置，重载合并到 PN 下面的芯片配置中

            StrTableName = "TopoManufactureChipsetInitialize";
            StrSelectconditions = "select * from " + StrTableName + " where PID= " + StuctTestplanPrameter.Id + " order by ID";
            DataTable Tempdt = MyDataio.GetDataTable(StrSelectconditions, StrTableName);// 获得环境的DataTable(

            if (Tempdt.Rows.Count > 0)
            {
                DtMyDutInf.Merge(Tempdt);
            }

            #endregion

             // pflowControl.StuctTestplanPrameter.Id
           
          //  StuctTestplanPrameter.Id 

            MyStruct = new DriverInitializeStruct[DtMyDutInf.Rows.Count];
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
                dutDriverStruct.StartBit = Convert.ToByte(dr["StartBit"]);
                dutDriverStruct.EndBit = Convert.ToByte(dr["EndBit"]);
                MyDriverStruct[i] = dutDriverStruct;

                i++;
            }

            return MyDriverStruct;
        }
        public DutEEPROMInitializeStuct[] Get_EEPROM_Init_FromSql()
        {
            DutEEPROMInitializeStuct[] MyStruct;
            int i = 0;
            // protected DutStruct[] MyDutStruct;
            DtMyDutInf = new DataTable();

            string StrTableName = "TopoManufactureConfigInit";

            string StrSelectconditions = "select * from " + StrTableName + " where PID= " + StuctTestplanPrameter.Id + " order by ID";
            DtMyDutInf = MyDataio.GetDataTable(StrSelectconditions, StrTableName);// 获得环境的DataTable

            MyStruct = new DutEEPROMInitializeStuct[DtMyDutInf.Rows.Count];
            // MyDutStructArray.
            foreach (DataRow dr in DtMyDutInf.Rows)
            {
                DutEEPROMInitializeStuct MyDriverStruct = new DutEEPROMInitializeStuct();
                MyDriverStruct.SlaveAddress = Convert.ToInt16(dr["SlaveAddress"]);
                MyDriverStruct.Page = Convert.ToByte(dr["Page"]);
               // int k= Convert.ToInt32(dr["StartAddress"]);
                MyDriverStruct.StartAddress = Convert.ToInt32(dr["StartAddress"]);
                MyDriverStruct.Length = Convert.ToByte(dr["Length"]);//         RegisterAddress
                MyDriverStruct.ItemValue = dr["ItemValue"];
                MyStruct[i] = MyDriverStruct;

                i++;
            }

            return MyStruct;
        }
        private bool Record_Dut_EEPROM(int RunRecord_ID, DutEEPROMInitializeStuct[] StuctCoef)// Don't  Use  in Moment
        {
            //MyLocatDataXml.CreartCoefBackNode();
            try
            {

                //for (int i = 0; i < StuctCoef.Length; i++)
                //{ 

                //    MyLocatDataXml.CreartCoefBackChildNode(StuctCoef[i].ItemValue.ToString(), StuctCoef[i].Length.ToString(), StuctCoef[i].Page.ToString(), StuctCoef[i].StartAddress.ToString());

                //}

                return true;

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        private bool Record_DutCoef(int RunRecord_ID, DutCoefValueStuct[] StuctCoef)
        {
            try
            {

                MyLocatDataXml.CreartCoefBackNode();

                for (int i = 0; i < StuctCoef.Length; i++)
                {


                    MyLocatDataXml.CreartCoefBackChildNode(StuctCoef[i].CoefValue.ToString().ToUpper(), StuctCoef[i].Length.ToString(), StuctCoef[i].Page.ToString(), StuctCoef[i].StartAddress.ToString());
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
  static  public class TimeSpanUse
    {

      static public string DateDiff(TimeSpan tsBegin, string StrMessage,out TimeSpan tsStartTime)
        {
            TimeSpan tsEnd = new TimeSpan(DateTime.Now.Ticks);
            TimeSpan ts = tsEnd.Subtract(tsBegin).Duration();//时间差的绝对值 ，测试你的代码运行了多长时间。
            string Str = (ts.TotalMilliseconds/1000).ToString();
            string Str1 = "TimeSpan:" + StrMessage + "Run Time " + Str;
            Log.SaveLogToTxt(Str1);

            tsStartTime = new TimeSpan(DateTime.Now.Ticks);
            return Str;

              //  ts.TotalMilliseconds.ToString()+" Seconds";
            //return null;
        }
    }
   
}
      
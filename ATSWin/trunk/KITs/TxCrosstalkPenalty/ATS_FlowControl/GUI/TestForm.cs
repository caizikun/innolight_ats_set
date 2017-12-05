using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using  ATSDataBase;
using ATS_Framework;
using ATS_Driver;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

namespace ATS
{
    public partial class TestForm : Form
    {
        private Attribute MyAttribute;

        class Attribute
        {
            public TestFormXml MyXml;
            public DataIO MyDataio;
            public logManager MylogManager = new logManager();
            public bool FlagEquipmentConfigOK = false;
            public string ProductionTypeName;
            public int IdPnType;
            public int MCoefsID;
            public string StrProductionName;
            public bool FlagFlowControllEnd;
            public Thread TestThread;
            public Thread RefreshThread;
            public int TotalChannnel;
            public int prosess;

            public DataTable dtTotalTestData;
            public DataTable dtCurrentTestModelResultData;
            public DataTable dtProductionType=new DataTable();
            public DataTable dtEquipmentParameter = new DataTable();
            public DataTable dtTestModelParameter = new DataTable();

            public string CurrentSN;
            public bool flag_FwVerOK;
            public string Current_DutFwRev;
            public bool flag_SNOK;
            public bool StopFlag;
            public bool TotalResultFlag;
            public bool ConditionEndflag=false;
         
        //    public LogQueue<string> QueueShow = new LogQueue<string> { };
            public OperateTxT MyOperateTxT;
          //  public int CycleTime=2;
            public double StartTemp=25;
            public int StartTempWaitTime = 50;
          //  public int ConditionCycle =1;
         //   public ArrayList ArrayPolarity = new ArrayList();
            public bool IsTestTx = true;
            public string StrOptEyeDiagramPath;
        }

        class ConditionInf//所有关于Condition 信息的类集合
        {
            public DataTable dtCondition;
            public double CurrentVcc;
            public double CurrentTemp;
            public double StartDelay;
            public double EndDelay;
            public double CurrentDelay;
            public double DelayStep;
            public int WaitTempTime;
          
            public string[] ConditionHeardArray;
        }

        class Equipmentstruct
        {
            public EquipmentList pEquipmentList = new EquipmentList();

           // EquipmentList
            public PPG pPPG;
            public ErrorDetector pED;
            public Thermocontroller pTempControl;
            public Powersupply pPowersupply;
            public EquipmentManage MyEquipmentManage;
            public DUT pDut;
            public OpticalSwitch pOpticalSwitch;
            public Attennuator pAtt;
            public Scope pScope;
            public ArrayList EquipementNameArray = new ArrayList();
        }

        class TestResult
        {
            public double AP=0;
            public double ER=0;
            public double Crossing=0;

            public double MaskMargin=0;
            public double JitterRms=0;
            public double JitterPP=0;

            public double FailTime=0;
            public double RiseTime=0;

            public double TxOmA=0;
            public double EyeHeight=0;
            public double BitRate=0;
            public double xMaskMargin2=0;

            public double Csen=0;
            public double CsenOMA=0;
          
        }
        

        class TestModeArray
        {
            //public TestTxEye pTestTxEye;
            //public TestBer pTestBer;

            public TestModelBase pTestModel;
            public globalParameters pglobalParameters = new globalParameters();
            public bool CurrentTestModelflag = false;
        }
        

        #region  委托声明

        public delegate void UpdataDataTable(DataGridView Dgv, DataTable dt);
        public delegate void UpdataLable(Label L1, string label);
        public delegate void UpdataRichBox(RichTextBox R1, string label);
        public delegate void UpdataProcessbar(ProgressBar p1, int process);
        public delegate void UpdataResult(bool ResultFlag);
        public delegate void UpdataButton(Button B1, bool flag, Color c1);
        public delegate void UpdataPanel(Panel B1, bool flag);
        public delegate void UpdataCombox(ComboBox B1, bool flag);
        public delegate void UpdataForm(ReslutShow B1, bool flag);
        public delegate void UpdataResultShow(bool ResultFlag, DataTable dt, bool ShowErrorData);

        private void ButtonUpdata(Button B1, bool flag, Color c1)
        {
            try
            {


                if (B1.InvokeRequired)
                {
                    UpdataButton button = new UpdataButton(ButtonUpdata); ;
                    B1.Invoke(button, new Object[] { B1, flag, c1 });
                    // BeginInvoke()

                }
                else
                {
                    lock (this)
                    {


                        B1.Enabled = flag;

                        if (flag)
                        {
                            B1.Enabled = true;
                        }
                        else
                        {
                            B1.Enabled = false;
                        }
                       // B1.BackColor = c1;
                        B1.Refresh();
                    }

                }
            }
            catch
            {

            }
        }
        private void UpDataStatus(Label L1, string StrStatus)
        {
            if (StrStatus != "")
            {

                if (L1.InvokeRequired)
                {
                    UpdataLable r = new UpdataLable(UpDataStatus); ;
                    L1.BeginInvoke(r, new Object[] { L1, StrStatus });

                }
                else
                {
                    lock (this)
                    {
                        L1.Text = StrStatus;
                        L1.Refresh();
                    }

                }

            }

        }
        private void UpdataShowLog(RichTextBox R1, string StrInf)
        {
            if (StrInf != "")
            {
                if (R1.InvokeRequired)
                {
                    UpdataRichBox d1 = new UpdataRichBox(UpdataShowLog); ;
                    R1.BeginInvoke(d1, new Object[] { R1, StrInf });

                }
                else
                {
                    lock (this)
                    {
                        R1.Text += StrInf;

                        R1.Select(R1.Text.Length, 1);
                        R1.ScrollToCaret();
                        // R1.Refresh();

                    }
                }
            }

        }

        private void UpdataTestData(DataGridView Dgv, DataTable dt)
        {
           
                if (Dgv.InvokeRequired)
                {
                    UpdataDataTable d1 = new UpdataDataTable(UpdataTestData); ;
                    Dgv.BeginInvoke(d1, new Object[] { Dgv, dt });

                }
                else
                {
                    lock (this)
                    {
                        if (Dgv.ColumnCount == 0)
                        {
                            for (int k = 0; k < dt.Columns.Count; k++)
                            {
                                Dgv.Columns.Add(dt.Columns[k].ColumnName, dt.Columns[k].ColumnName);
                                // Dgv.Columns.Add()
                            }
                        }
                        Dgv.Rows.Clear();
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            //string[] TestData = new string[dt.Columns.Count];

                            //for (int j = 0; j < dt.Columns.Count; j++)
                            //{
                            //    TestData[j] = dt.Rows[i][j].ToString();
                            //}

                            Dgv.Rows.Add(dt.Rows[i].ItemArray);
                        }


                    }
                }

            

        }

        private void PanelUpdata(Panel p1, bool flag)
        {

            if (p1.InvokeRequired)
            {
                UpdataPanel d1 = new UpdataPanel(PanelUpdata); ;
                p1.BeginInvoke(d1, new Object[] { p1, flag });

            }
            else
            {
                lock (this)
                {

                    if (flag)
                    {
                        p1.Enabled = true;
                    }
                    else
                    {
                        p1.Enabled = false;
                    }
                    p1.Refresh();
                }
            }
        }

        private void UpDataProcess(ProgressBar p1, int process)
        {
            if (p1.InvokeRequired)
            {
                UpdataProcessbar r = new UpdataProcessbar(UpDataProcess); ;
                p1.BeginInvoke(r, new Object[] { p1, process });

            }
            else
            {
                lock (this)
                {

                    p1.Value = process;
                    labelProgress.Text = process + "%";
                }
            }
        }

        private void FormUpdata(ReslutShow F1, bool flag)
        {
            if (F1.InvokeRequired)
            {
                UpdataProcessbar r = new UpdataProcessbar(UpDataProcess); ;
                F1.BeginInvoke(r, new Object[] { F1, flag });

            }
            else
            {
                lock (this)
                {

                    F1.ShowReslut(flag, null, false);
                    F1.ShowDialog();
                }
            }
        }
        private void RefreshResult(bool Rflag)
        {
            ReslutShow r1 = new ReslutShow();
            if (r1.InvokeRequired)
            {
                UpdataResult d1 = new UpdataResult(RefreshResult);
                this.BeginInvoke(d1, new Object[] { Rflag });

            }
            else
            {
                lock (this)
                {
                    //  public void ShowReslut(bool Result, DataTable dtTestData, bool ShowErrorData)
                    r1.ShowReslut(Rflag, null, false);
                    r1.ShowDialog();
                }
            }
        }

        private void TestResultFormShow(bool Rflag, DataTable dt, bool ShowErrorData)
        {
            ReslutShow r1 = new ReslutShow();
            // UpdataResultShow R1 = new UpdataResultShow();
            if (r1.InvokeRequired)
            {
                UpdataResultShow d1 = new UpdataResultShow(TestResultFormShow);
                this.BeginInvoke(d1, new Object[] { Rflag, dt, ShowErrorData });

            }
            else
            {
                lock (this)
                {
                    r1.ShowReslut(Rflag, dt, ShowErrorData);
                    r1.ShowDialog();
                }
            }

        }

        private void Refresh_GUI()
        {
           
            UpdataResult refreshResultLabel = new UpdataResult(RefreshResult);
            
            string Strlabelshow = "";
            int i = 0;
            while (!MyAttribute.FlagFlowControllEnd)
            {
                
              //  UpDataStatus(this.labelShow, Strlabelshow);
                i++;

                if (i > 5)
                {
                    UpdataTestData(dataGridViewTotalData, MyAttribute.dtTotalTestData);
                    i = 0;
                }
                UpDataProcess(progress, MyAttribute.prosess);
                UpDataStatus(labelProgress, MyAttribute.prosess.ToString());
                Thread.Sleep(5000);
            }
            if (MyAttribute.FlagFlowControllEnd)
            {
                UpDataStatus(labelProgress, MyAttribute.prosess.ToString());
                Strlabelshow = "Test End..........";
                UpDataStatus(this.labelShow, Strlabelshow);
                UpDataStatus(labelProgress, MyAttribute.prosess.ToString());
             //   UpDataStatus(labelShow, "Vcc=" + MyCondition.CurrentVcc + "*Temp=" + MyCondition.CurrentTemp);
                ButtonUpdata(button_Test, true, Color.WhiteSmoke);
                ButtonUpdata(button_Config, true, Color.Green);
                ButtonUpdata(buttonStop, true, Color.WhiteSmoke);
                PanelUpdata(this.panelEquipment, true);
                UpdataResultShow FormShowTestResult = new UpdataResultShow(TestResultFormShow);
                this.Invoke(FormShowTestResult, MyAttribute.TotalResultFlag, null, false);

                Thread.Sleep(5000);
                MyAttribute.TestThread.Abort();
                MyAttribute.RefreshThread.Abort();
                GC.Collect();

            }

        }

        #endregion

        private Equipmentstruct MyEquipmentstruct;
        private ConditionInf MyCondition = new ConditionInf();
        private TestResult MyTestResult = new TestResult();
        private TestModeArray MyTestModeArray = new TestModeArray();

        public TestForm()
        {
            InitializeComponent();

           // MyEquipmentstruct.
          
            MyAttribute = new Attribute();
            MyEquipmentstruct = new Equipmentstruct();
            MyEquipmentstruct.MyEquipmentManage = new EquipmentManage(MyAttribute.MylogManager);
           
            MyEquipmentstruct.pEquipmentList.Clear();
            MyCondition.dtCondition = new DataTable();

            MyAttribute.dtTotalTestData = new DataTable();

            MyCondition.dtCondition.Columns.Add("Sequence");
          //  MyCondition.dtCondition.Columns.Add("Polarity");//Sequence
            MyCondition.dtCondition.Columns.Add("vcc");
            MyCondition.dtCondition.Columns.Add("Temp");//Sequence
            MyCondition.dtCondition.Columns.Add("StartDelay");
            MyCondition.dtCondition.Columns.Add("EndDelay");
            MyCondition.dtCondition.Columns.Add("DelayStep");
            MyCondition.dtCondition.Columns.Add("SleepTime");

            ReadXmlInf();


          
        }

        #region  Funtion

        private DataTable FitEquipemtnInf()
        {
            DataTable dt=new DataTable();

          //  MyAttribute.MyDataio.
            string Selectconditions = "SELECT  GlobalAllEquipmentList.ItemName as EquipmentName,GlobalAllEquipmentParamterList.ItemName as ItemName,GlobalAllEquipmentParamterList.ItemValue as itemValue FROM [ATS_V2].[dbo].[GlobalAllEquipmentList],[ATS_V2].[dbo].GlobalAllEquipmentParamterList where (GlobalAllEquipmentList.ID=GlobalAllEquipmentParamterList.PID) and (GlobalAllEquipmentList.ItemName IN('E3631','TPO4300','MP1800PPG','MP1800ED','FLEX86100','MAP200Atten','MAP200OpticalSwitch')) ORDER BY GlobalAllEquipmentList.ItemName";
            dt = MyAttribute.MyDataio.GetDataTable(Selectconditions, "GlobalAllEquipmentList"); ;

            return dt;

        }

        private DataTable FitTestModelInf()
        {
            DataTable dt = new DataTable();


            string Selectconditions = "SELECT A.ItemName as TestmodelName ,B.ItemName,B.ItemValue FROM [ATS_V2].[dbo].GlobalAllTestModelList A,[ATS_V2].[dbo].GlobalTestModelParamterList B  where  A.ID=B.PID  and A.ItemName in('TestBer','TestTxEye') ";
          
            dt = MyAttribute.MyDataio.GetDataTable(Selectconditions, "GlobalAllEquipmentList"); ;

            return dt;

        }

        private bool ReadXmlInf()
        {
            string[] Array1 = new string[4];
          //  string StrValue = null;

            MyAttribute.MyXml = new TestFormXml(Application.StartupPath + "\\ConfigData.xml");

            MyAttribute.MyDataio = new SqlDatabase(MyAttribute.MyXml.DatabasePath, MyAttribute.MyXml.DbName, MyAttribute.MyXml.Username, MyAttribute.MyXml.PWD);
     
           

             if (!MyAttribute.MyXml.isEquipmentInfExists)
             {
              
                 MyAttribute.dtEquipmentParameter = FitEquipemtnInf();
                 MyAttribute.MyXml.FitEquipmentToXml(MyAttribute.dtEquipmentParameter);
                 MyAttribute.dtTestModelParameter = FitTestModelInf();
                 MyAttribute.MyXml.FitTestModelInfToXml(MyAttribute.dtTestModelParameter);


             }


            MyCondition.dtCondition= MyAttribute.MyXml.GetConditionTable(MyCondition.dtCondition.Clone());
            this.dataGridView_Condition.DataSource = MyCondition.dtCondition;
            this.dataGridView_Condition.Refresh();
            if (this.dataGridView_Condition.Columns.Count>0)
            {
                 this.dataGridView_Condition.Columns[0].ReadOnly = true;
               
            }
          
            return true;
        }

        private string[] ReadProductionTpye()
        {

            try
            {
                if (MyAttribute.MyDataio.OpenDatabase(true))
                {
                    string StrTableName = "GlobalProductionType";
                    string Selectconditions = "select * from " + StrTableName + " Where IgnoreFlag='false' order by ID";
                    MyAttribute.dtProductionType.Clear();
                    MyAttribute.dtProductionType = MyAttribute.MyDataio.GetDataTable(Selectconditions, StrTableName); ;
                    string[] arry = MyAttribute.dtProductionType.AsEnumerable().Select(d => d.Field<string>("ItemName")).ToArray();
                    return arry;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }

        }

        private bool SelectEquipment()
        {
            MyEquipmentstruct.EquipementNameArray.Clear();

            try
            {


                if (this.Rdb_MP1800_PPG.Checked)
                {
                    //MyEquipmentstruct.MyPPG = new MP1800PPG(MyAttribute.MylogManager);
                    //MyEquipmentstruct.MyED = new MP1800ED(MyAttribute.MylogManager);
                    MyEquipmentstruct.EquipementNameArray.Add("PPG_MP1800PPG");
                   
                }
              
               
                if (this.Rdb_MP1800_ED.Checked)
                {
                    MyEquipmentstruct.EquipementNameArray.Add("ErrorDetector_MP1800ED");
                }
               
                if (this.Rdb_E3631.Checked)
                {
             
                    MyEquipmentstruct.EquipementNameArray.Add("PowerSupply_E3631");
                }
               
                else
                {
                    MyEquipmentstruct.pPowersupply = null;
                }

                if (this.Rdb_TPO4300.Checked)
                {
                  //  MyEquipmentstruct.MyTempControl = new TPO4300(MyAttribute.MylogManager);
                    //  MyEquipmentstruct.EquipmentList.Add("TPO4300", MyEquipmentstruct.MyTempControl);
                    MyEquipmentstruct.EquipementNameArray.Add("Thermocontroller_TPO4300");
                }
                else
                {
                    MyEquipmentstruct.pTempControl = null;
                }

                if (this.rdb_Flex86100.Checked)
                {
                    MyEquipmentstruct.EquipementNameArray.Add("Scope_FLEX86100");
                }
                else
                {
                    MyEquipmentstruct.pScope = null;
                }

                if (this.rDB_Att_Map200.Checked)
                {
                    MyEquipmentstruct.EquipementNameArray.Add("Attennuator_MAP200Atten");
                }
                else
                {
                    MyEquipmentstruct.pAtt = null;
                }


                if (this.rdb_Switch_Map200.Checked)
                {
                    MyEquipmentstruct.EquipementNameArray.Add("OpticalSwitch_MAP200OpticalSwitch");
                }
                else
                {
                    MyEquipmentstruct.pOpticalSwitch = null;
                }
                return true;
            }
            catch
            {
                return false;
            }

        }
       
        private void ConfigEquipment()
        {
            MyEquipmentstruct.pPPG = null;
            MyEquipmentstruct.pDut = null;
            MyEquipmentstruct.pED = null;
            MyEquipmentstruct.pPowersupply = null;
            MyEquipmentstruct.pTempControl = null;
        

            string CurrentEquipmentName = "";
            MyAttribute. FlagEquipmentConfigOK = true;

          //UpdataRichBox refreshRichBox = new UpdataRichBox(UpdataShowLog);
          //UpdataDataTable refreshDataTable = new UpdataDataTable(RefreshDataGridView);

            DutStruct[] MyDutManufactureCoefficientsStructArray;
            DriverStruct[] MyManufactureChipsetStructArray;
            DriverInitializeStruct[] MyDutManufactureChipSetInitialize;
            DutEEPROMInitializeStuct[] MyDutEEPROMInitializeStuct;
            int i = 0;
            try
            {

                MyEquipmentstruct.pDut = (DUT)MyEquipmentstruct.MyEquipmentManage.Createobject(MyAttribute.ProductionTypeName.ToUpper() + "DUT");
                MyEquipmentstruct.pDut.deviceIndex = 0;
                MyEquipmentstruct.pDut.ChipsetControll = false;
                MyDutManufactureCoefficientsStructArray = GetDutManufactureCoefficients();
                MyManufactureChipsetStructArray = null;
                MyDutManufactureChipSetInitialize = null; //等待数据库结构统一
                MyDutEEPROMInitializeStuct = null;
                MyEquipmentstruct.pDut.Initialize(MyDutManufactureCoefficientsStructArray, MyManufactureChipsetStructArray, MyDutManufactureChipSetInitialize, MyDutEEPROMInitializeStuct, "");//等待Driver 跟上
                //MyAttribute.pDut.Initialize(MyDutManufactureCoefficientsStructArray, MyManufactureChipsetStructArray, MyAttribute.StrAuxAttribles);
               // MyEquipmentstruct.EquipmentList.Clear();
                Algorithm algorithm = new Algorithm();
                for (i = 0; i < MyEquipmentstruct.EquipementNameArray.Count; i++)
                {
                    TestModeEquipmentParameters[] CurrentEquipmentStruct;
                    string[] StrArray = MyEquipmentstruct.EquipementNameArray[i].ToString().Split('_');

                    CurrentEquipmentName = StrArray[1].ToUpper();

                   CurrentEquipmentStruct = MyAttribute.MyXml.GetEquipmenParameter(CurrentEquipmentName);
                  
                    EquipmentBase CurrentEquipmentObject = (EquipmentBase)MyEquipmentstruct.MyEquipmentManage.Createobject(CurrentEquipmentName);
                  //  CurrentEquipmentObject.Role = Convert.ToByte(dtEquipmenList.Rows[i]["Role"]);// 0=NA,1=TX,2=RX

                    UpDataStatus(labelShow, CurrentEquipmentName + "  Configing.....");

                    if (!CurrentEquipmentObject.Initialize(CurrentEquipmentStruct) || !CurrentEquipmentObject.Configure(1))
                    {
                        MessageBox.Show(CurrentEquipmentName + "Configure Error");
                       MyAttribute. FlagEquipmentConfigOK = false;

                       Exception ex = new Exception(CurrentEquipmentName + "Configure Error");
                        throw ex;
                    }
                    else
                    {
                        MyEquipmentstruct.pEquipmentList.Add(CurrentEquipmentName, CurrentEquipmentObject);


                        if(CurrentEquipmentName.Contains("PPG"))
                        {
                            MyEquipmentstruct.pPPG =(PPG) CurrentEquipmentObject;
                         //   MyEquipmentstruct.pEquipmentList.Add("PPG", MyEquipmentstruct.pPPG);
                        }
                        else if (CurrentEquipmentName.Contains("ED"))
                        {
                            MyEquipmentstruct.pED = (ErrorDetector)CurrentEquipmentObject;

                        }
                        else if (CurrentEquipmentName.Contains("E3631"))
                        {
                            MyEquipmentstruct.pPowersupply = (Powersupply)CurrentEquipmentObject;

                        }
                        else if (CurrentEquipmentName.Contains("TPO4300"))
                        {
                            MyEquipmentstruct.pTempControl = (Thermocontroller)CurrentEquipmentObject;
                        }
                        else if (CurrentEquipmentName.Contains("FLEX86100"))
                        {
                            MyEquipmentstruct.pScope = (Scope)CurrentEquipmentObject;
                        }
                        else if (CurrentEquipmentName.Contains("MAP200Atten"))
                        {
                            MyEquipmentstruct.pAtt = (Attennuator)CurrentEquipmentObject;
                        }
                        else if (CurrentEquipmentName.Contains("MAP200OpticalSwitch"))
                        {
                            MyEquipmentstruct.pOpticalSwitch = (OpticalSwitch)CurrentEquipmentObject;
                        }

                        MyEquipmentstruct.pEquipmentList.Add(MyEquipmentstruct.EquipementNameArray[i].ToString(), CurrentEquipmentObject);
                       // MyEquipmentstruct.EquipementNameArray[i].ToString()
                    }
                    
                    // pThermocontroller
                    CurrentEquipmentObject.OutPutSwitch(false);
                    int ProcessValue = (int)(i + 1) * 100 / (MyEquipmentstruct.EquipementNameArray.Count);
                    UpDataProcess(progress, ProcessValue);
                    UpDataStatus(labelProgress, ProcessValue.ToString());
                    UpDataStatus(labelShow, CurrentEquipmentName + "  ConfigOK");
                    MyAttribute.MylogManager.AdapterLogString(1, CurrentEquipmentName + "  ConfigOK");
                }

               
            }
            catch (Exception EX)
            {
                MyAttribute.MylogManager.AdapterLogString(1, CurrentEquipmentName + "  Config Error");
                MessageBox.Show(EX.Message, "ConfigEquipment Error ,PLS Check Equipment");
              //  UpDataStatus(this.labelShow, "Equipment Config false");
               MyAttribute. FlagEquipmentConfigOK = false;
            }
            finally
            {
              
                // UpdataPanel(this.panelCombox, true);
                if (MyAttribute.FlagEquipmentConfigOK)
                {
                    MessageBox.Show("ConfigEquipment OK");
                    UpDataStatus(this.labelShow, "Equipment Config OK");
                   // ButtonUpdata(this.button_Config, true, Color.Green);
                }
                else
                {
                   // ButtonUpdata(button_Config, true, Color.Red);
                    UpDataStatus(this.labelShow, "Equipment Config false");
                  //  ButtonUpdata(this.button_Config, true, Color.Red);
                }
                ButtonUpdata(this.button_Test, true, Color.WhiteSmoke);
            
                ButtonUpdata(this.buttonStop, true, Color.WhiteSmoke);
            }
        }

        public DutStruct[] GetDutManufactureCoefficients()
        {
            DutStruct[] DutInfStruct;
            int i = 0;
            // protected DutStruct[] MyDutStruct;
           DataTable DtMyDutInf = new DataTable();

            string StrTableName = "GlobalManufactureCoefficients";

            string StrSelectconditions = "select * from " + StrTableName + " where PID= " + MyAttribute.MCoefsID + " order by ID";
            DtMyDutInf = MyAttribute.MyDataio.GetDataTable(StrSelectconditions, StrTableName);// 获得环境的DataTable

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

        private bool GetEquipmentInf()
        {

            for (int i = 0; i < MyEquipmentstruct.pEquipmentList.Count;i++ )
            {
                MyAttribute.MyXml.GetEquipmenParameter(MyEquipmentstruct.pEquipmentList.Keys[i].ToString());
            }
         
            return true;
        }

        private void LoadEquipmentInf()
        {

        }

        private bool ReadyForTest()
        {

            // Powersupply aPS;

            MyAttribute.MylogManager.AdapterLogString(1, "$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$A New Test Start$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$");
        


            MyAttribute.prosess = 0;
            
            if (MyAttribute.FlagEquipmentConfigOK )
            {
                dataGridViewTotalData.Rows.Clear();

                dataGridViewTotalData.Refresh();
                // Thread.Sleep(1000);
                dataGridViewTotalData.Refresh();

                #region  Disable Control

                button_Config.Enabled = false;
             
                panelCombox.Enabled = false;
           
                #endregion

             

                button_Test.Enabled = false;

                if (MyAttribute.dtTotalTestData != null)
                {
                    MyAttribute.dtTotalTestData.Clear();
                }
              
                textBoxSN.Text = "";

                labelShow.Text = "Prepare Read SN....";
                labelShow.Refresh();
                try
                {
                    MyEquipmentstruct.pPowersupply.OutPutSwitch(true);
                    MyEquipmentstruct.pDut.FullFunctionEnable();

                    if (!JudgeSerialNO()) return false;//判定序列号是否正常
                  //  TimeSpanUse.DateDiff(MyAttribute.TimeSpanStart, MyAttribute.MyLogManager, "Prepare Time for Test -JudgeSerialNO ", out MyAttribute.TimeSpanStart);

                    if (!MyEquipmentstruct.pDut.FullFunctionEnable()) return false;

                   // TimeSpanUse.DateDiff(MyAttribute.TimeSpanStart, MyAttribute.MyLogManager, "Prepare Time for Test -JudegEvb ", out MyAttribute.TimeSpanStart);

                    //if(!JudgeSerialNO())return false;//判定序列号是否正常
                    //TimeSpanUse.DateDiff(MyAttribute.TimeSpanStart, MyAttribute.MyLogManager, "Prepare Time for Test -JudgeSerialNO ", out MyAttribute.TimeSpanStart);

                    if (!JudgeFwVer()) return false; ;//判定Fw版本号是否正常
                 
                    return true;

                }
                catch (Exception ex)
                {
                    MessageBox.Show("I2C Read Error", ex.Message + "PLS Check the Connecter");

                    labelShow.Text = "Prepare Test Error";
                    labelShow.Refresh();

                    textBoxSN.Text = "";
                    textBoxSN.Refresh();
                    button_Test.Enabled = true;
                    comboBoxPN.Enabled = true;
                    comboBoxPType.Enabled = true;
                  

                    progress.Value = 0;
                    progress.Refresh();
                    return false;
                }


            }
            else
            {
                MessageBox.Show("PLS Config Equipment and EquipmentOffSet");
                return false;
            }


        }

        private bool JudgeFwVer()
        {
            MyAttribute.flag_FwVerOK = false;



            int i = 0;

            for (i = 0; i < 3; i++)
            {
                MyAttribute.Current_DutFwRev = MyEquipmentstruct.pDut.ReadFWRev().ToString().ToUpper();
                textBoxFW.Text = MyAttribute.Current_DutFwRev;
                textBoxFW.Refresh();
                if (MyAttribute.Current_DutFwRev != "0000" && MyAttribute.Current_DutFwRev != "FFFF")
                {
                   
                        MyAttribute.flag_FwVerOK = true;
                  
                }

            }

            if (!MyAttribute.flag_FwVerOK)
            {
                MessageBox.Show("FwVer  Error");
                button_Test.Enabled = true;
            }
            MyAttribute.Current_DutFwRev = "0x" + MyAttribute.Current_DutFwRev;
            textBoxFW.Text = MyAttribute.Current_DutFwRev;
            textBoxFW.Refresh();

            return MyAttribute.flag_FwVerOK;
        }

        private bool JudgeSerialNO()
        {

            MyAttribute.flag_SNOK = false;

            for (int i = 0; i < 3; i++)
            {
                MyAttribute.CurrentSN = MyEquipmentstruct.pDut.ReadSn().Trim();
                // Thread.Sleep(200);
                textBoxSN.Text = MyAttribute.CurrentSN;
                textBoxSN.Refresh();
                //  Thread.Sleep(200);

                string Str = MyAttribute.CurrentSN.Substring(0, 1);

                if (MyAttribute.CurrentSN.Length >= 3)
                {
                    //if (MyAttribute.CurrentSN.Substring(0,1)=="")
                    //{
                    //}
                    MyAttribute.flag_SNOK = true;
                    break;
                }
            }

            textBoxSN.Text = MyAttribute.CurrentSN;

            if (!MyAttribute.flag_SNOK)
            {
                MessageBox.Show("SN Read Error");
                button_Test.Enabled = true;
            }

            textBoxSN.Refresh();

            return MyAttribute.flag_SNOK;
        }

        private void Test()
        {
            try
            {
              StartTest();
            }
            catch
            {
                MessageBox.Show("TestError~~");
            }

          

        }

        private bool SelectResultHead()
        {
            MyAttribute.dtTotalTestData.Columns.Clear();
            MyAttribute.dtTotalTestData.Columns.Add("NO");
            MyAttribute.dtTotalTestData.Columns.Add("FwVer");
            MyAttribute.dtTotalTestData.Columns.Add("TestTime");
            MyAttribute.dtTotalTestData.Columns.Add("Vcc");
            MyAttribute.dtTotalTestData.Columns.Add("Temp");
        
            MyAttribute.dtTotalTestData.Columns.Add("AP(DBM)");
            MyAttribute.dtTotalTestData.Columns.Add("ER(DB)");
            MyAttribute.dtTotalTestData.Columns.Add("CROSSING(%)");
            MyAttribute.dtTotalTestData.Columns.Add("MASKMARGIN(%)");
            MyAttribute.dtTotalTestData.Columns.Add("JITTERRMS(PS)");
            MyAttribute.dtTotalTestData.Columns.Add("JITTERPP(PS)");
            MyAttribute.dtTotalTestData.Columns.Add("RISETIME(PS)");
            MyAttribute.dtTotalTestData.Columns.Add("FALLTIME(PS)");
            MyAttribute.dtTotalTestData.Columns.Add("TXOMA(DBM)");
            MyAttribute.dtTotalTestData.Columns.Add("EYEHEIGHT(MW)");
            MyAttribute.dtTotalTestData.Columns.Add("BitRate(GB/S)");
            MyAttribute.dtTotalTestData.Columns.Add("XMASKMARGIN2(%)");

            MyAttribute.dtTotalTestData.Columns.Add("CSEN(DBM)");
            MyAttribute.dtTotalTestData.Columns.Add("CSENOMA(DBM)");

            return true;
        }

        public bool StartTest()
        {

            #region      进入测试前的准备
         
            MyAttribute.StopFlag = false;
       
            MyAttribute.dtTotalTestData.Clear();
         
            MyAttribute. prosess = 0;

          #endregion

            try
            {

                MyTestModeArray.pglobalParameters.CurrentSN = MyAttribute.CurrentSN;
                MyTestModeArray.pglobalParameters.StrPathOEyeDiagram = MyAttribute.StrOptEyeDiagramPath;

                for (int TempCount = 0; TempCount < MyCondition.dtCondition.Rows.Count;TempCount++ )
                {
                    MyAttribute.ConditionEndflag = false;
                    DataRow drCondition = MyCondition.dtCondition.Rows[TempCount];

                    MyCondition.CurrentTemp = Convert.ToDouble(drCondition["Temp"]);
                    MyCondition.StartDelay = Convert.ToDouble(drCondition["StartDelay"]);
                    MyCondition.EndDelay = Convert.ToDouble(drCondition["EndDelay"]);
                    MyCondition.DelayStep = Convert.ToDouble(drCondition["DelayStep"]);
                    MyCondition.CurrentVcc = Convert.ToDouble(drCondition["Vcc"]);
                    MyCondition.WaitTempTime = Convert.ToInt16(drCondition["SleepTime"]);

                    int CycleTime = Convert.ToInt16((MyCondition.EndDelay - MyCondition.StartDelay) / MyCondition.DelayStep);

                    MyTestModeArray.pglobalParameters.CurrentTemp = MyCondition.CurrentTemp;
                    MyTestModeArray.pglobalParameters.CurrentVcc = MyCondition.CurrentVcc;
                   // MyTestModeArray.pglobalParameters.
                    SetCondition();

                    for (byte Channel = 1; Channel < 5; Channel++)
                    {
                        MyTestModeArray.pglobalParameters.CurrentChannel = Channel;

                        for (int EquipmentCount = 0; EquipmentCount < MyEquipmentstruct.pEquipmentList.Count; EquipmentCount++)
                        {
                            MyEquipmentstruct.pEquipmentList.Values[EquipmentCount].ChangeChannel(Channel.ToString());
                        }
                        
                        for (int j = 0; j < CycleTime; j++)
                        {
                            MyCondition.CurrentDelay = MyCondition.StartDelay + CycleTime * MyCondition.DelayStep;

                            MyEquipmentstruct.pPPG.SetPPGDelay(MyCondition.CurrentDelay, 0);

                            Task<bool> T1 = new Task<bool>(RunTestModel);

                            T1.Start();

                            T1.Wait(240 * 1000);

                            T1.Dispose();


                            Thread.Sleep(1000);

                            GC.Collect();

                            if (MyAttribute.StopFlag)
                            {
                             
                                break;
                            }
                        }

                        if (MyAttribute.StopFlag)
                        {
                            MyAttribute.TotalResultFlag = false;
                            break;
                        }



                        if (!MyAttribute.StopFlag)
                        {
                            MyAttribute.TotalResultFlag = true;

                        }

                        MyAttribute.prosess = (TempCount + 1) * Channel * 100 / MyCondition.dtCondition.Rows.Count * 4;
                    }


                    if (MyAttribute.StopFlag)
                    {

                        break;
                    } 

                }

                if (MyEquipmentstruct.pTempControl != null)
                {
                    MyEquipmentstruct.pTempControl.SetPositionUPDown("0");

                }

                for (int k = 0; k < MyEquipmentstruct.pEquipmentList.Count; k++)
                {
                    MyEquipmentstruct.pEquipmentList.Values[k].Referenced_Times = 0;
                }

                MyAttribute.MylogManager.AdapterLogString(1, "Test End------------------------");
                MyAttribute.FlagFlowControllEnd = true;

                return true;
            }
            catch (Exception ex)
            {
               
               MyAttribute.MylogManager .AdapterLogString(1, "Test End------------------------");
               MyAttribute. FlagFlowControllEnd = true;
                return false;
            }

        }

        public bool TxTest()
        {


           
            return MyTestModeArray.CurrentTestModelflag;
           
        }

        private bool CombineTestData(TestModeEquipmentParameters[] TempArray, out DataTable dtCurrentTestModelResultData)
        {

            MyTestResult = new TestResult();
            dtCurrentTestModelResultData = null;
            try
            {
                foreach (TestModeEquipmentParameters A in TempArray)
                {
                    switch (A.FiledName.ToString())
                    {
                        case "AP(DBM)":
                            MyTestResult.AP = Convert.ToDouble(A.DefaultValue);
                            break;
                        case "ER(DB)":
                            MyTestResult.ER = Convert.ToDouble(A.DefaultValue);
                            break;
                        case "CROSSING(%)":
                            MyTestResult.Crossing = Convert.ToDouble(A.DefaultValue);
                            break;
                        case "JITTERRMS(PS)":
                            MyTestResult.JitterRms = Convert.ToDouble(A.DefaultValue);
                            break;
                        case "JITTERPP(PS)":
                            MyTestResult.JitterPP = Convert.ToDouble(A.DefaultValue);
                            break;
                        case "MASKMARGIN(%)":
                            MyTestResult.MaskMargin = Convert.ToDouble(A.DefaultValue);
                            break;
                        case "RISETIME(PS)":
                            MyTestResult.RiseTime = Convert.ToDouble(A.DefaultValue);
                            break;
                        case "FALLTIME(PS)":
                            MyTestResult.FailTime = Convert.ToDouble(A.DefaultValue);
                            break;
                        case "TXOMA(DBM)":
                            MyTestResult.TxOmA = Convert.ToDouble(A.DefaultValue);
                            break;
                        case "EYEHEIGHT(MW)":
                            MyTestResult.EyeHeight = Convert.ToDouble(A.DefaultValue);
                            break;
                        case "BitRate(GB/S)":
                            MyTestResult.BitRate = Convert.ToDouble(A.DefaultValue);
                            break;

                        case "XMASKMARGIN2(%)":
                            MyTestResult.xMaskMargin2 = Convert.ToDouble(A.DefaultValue);
                            break;
                        case "CSEN(DBM)":
                            MyTestResult.Csen = Convert.ToDouble(A.DefaultValue);
                            break;

                        case "CSENOMA(DBM)":
                            MyTestResult.CsenOMA = Convert.ToDouble(A.DefaultValue);
                            break;
                        default:
                            break;

                    }
                }

                dtCurrentTestModelResultData = MyAttribute.dtTotalTestData.Clone();

                DataRow dr = dtCurrentTestModelResultData.NewRow();

                 dr["NO"] = MyAttribute.CurrentSN;
                 dr["FwVer"] = MyAttribute.Current_DutFwRev;
                 dr["TestTime"] = DateTime.Now.ToString();

                 dr["Vcc"] = MyTestModeArray.pglobalParameters.CurrentVcc;
                 dr["Temp"] = MyTestModeArray.pglobalParameters.CurrentTemp;


                dr["AP"] = MyTestResult.AP;
                dr["BitRate"] = MyTestResult.BitRate;
                dr["Crossing"] = MyTestResult.Crossing;
                dr["Csen"] = MyTestResult.Csen;
                dr["CsenOMA"] = MyTestResult.CsenOMA;
                dr["ER"] = MyTestResult.ER;
                dr["EyeHeight"] = MyTestResult.EyeHeight;
                dr["FailTime"] = MyTestResult.FailTime;
                dr["JitterPP"] = MyTestResult.JitterPP;
                dr["JitterRms"] = MyTestResult.JitterRms;
                dr["MaskMargin"] = MyTestResult.MaskMargin;
                dr["RiseTime"] = MyTestResult.RiseTime;
                dr["TxOmA"] = MyTestResult.TxOmA;
                dr["xMaskMargin2"] = MyTestResult.xMaskMargin2;

                dtCurrentTestModelResultData.Rows.Add(dr);
            }
            catch
            {
                return false;
            }

      
            return true;
        }
        
     
        //public bool RunTestModel(string StrTestModelName, String StrCurrentTestModelId, String StrTestmodelType)
        //{
        //    CurrentTestModelResultflag = true;//

        //    bool CurrentTestModelflag = false;//TestModel 运行过程是否出错,与测试结果无关系;
        //    try
        //    {
        //        QueueShow.Enqueue("Temp=" + StrCurrentTemp + "**" + "VCC=" + StrCurrentVcc + "**" + "Channel=" + CurrentChannel + "**" + StrTestModelName + "Testing....");
        //        string StrartTime = MyDataio.GetCurrTime().ToString("yyyy/MM/dd HH:mm:ss");
        //        TestModelBase pCurrentTestModel;

        //        dtCurrentTestModelTestData.Clear();

        //        if (!MyTestModelList.Keys.Contains(StrTestModelName.ToUpper().Trim()))
        //        {
        //            pCurrentTestModel = MyTestModelManage.Createobject(StrTestModelName.ToUpper().Trim());
        //            MyTestModelList.Add(StrTestModelName.ToUpper().Trim(), pCurrentTestModel);
        //            // pCurrentTestModel.specParameters = pCurrentTestModel.SetGetSpecParametersDataTable;
        //            pCurrentTestModel.SetGlobalParameters = pGlobalParameters;
        //           // pCurrentTestModel.SetGetSpecParametersDataTable = dtPnSpec;

        //        }


        //        if (!MyTestModelList[StrTestModelName.ToUpper().Trim()].SelectEquipment(MyEquipList))// 将其要使用的仪器 加入当前的Testmodel 自己的 sortlist
        //        {
        //            MessageBox.Show(StrTestModelName.ToUpper().Trim() + "缺少必须设备,请检查设备...");
        //            MyLogManager.AdapterLogString(3, StrTestModelName.ToUpper().Trim() + "缺少必须设备,请检查设备.......");
        //            CurrentTestModelResultflag = false;
        //            CurrentTestModelflag = false;
        //            return CurrentTestModelResultflag;
        //        }


        //        SelcetTestModelParameter(StrCurrentTestModelId);

        //        StrInputLog += "Temp=" + StrCurrentTemp + "**" + "VCC=" + StrCurrentVcc + "**" + "Channel=" + CurrentChannel + "**" + StrTestModelName + "\r\n";
        //        WriteInputLog();
        //        MyTestModelList[StrTestModelName.ToUpper().Trim()].SetinputParameters = TestModelInputArray;

        //        MyTestModelList[StrTestModelName.ToUpper().Trim()].SetGlobalParameters = pGlobalParameters;
        //        TimeSpanUse.DateDiff(TimeSpanStart, MyLogManager, "进入RunTestModel 到 TestModel RunTest之前  ", out TimeSpanStart);
        //        CurrentTestModelflag = MyTestModelList[StrTestModelName.ToUpper().Trim()].RunTest();

        //        // MyLogManager.AdapterLogString(1, MyTestModelList[StrTestModelName.ToUpper().Trim()].GetLogInfor);
        //        QueueInterfaceLog.Enqueue("Current TestModel:" + CurrentTestModelflag.ToString());
        //        StrInputLog += "Current TestModel:" + CurrentTestModelflag.ToString() + "\r\n";

        //        string TestEndTime = MyDataio.GetCurrTime().ToString("yyyy/MM/dd HH:mm:ss");
        //        string StrSN = CurrentSN + "\r\n";
        //        TestModelOutputArray = MyTestModelList[StrTestModelName.ToUpper().Trim()].GetoutputParameters;

        //        if (TestModelOutputArray == null)
        //        {
        //            MyLogManager.AdapterLogString(1, "CurrentTestModel OutputPrameter is null ");
        //        }

        //        //**************************************************************
        //        //TestModel Log存入数据库，目前我们换不具备这样的数据库表格
        //        //SaveTestLog(MyTestModelList[StrTestModelName.ToUpper().Trim()].GetLogInfor);

        //        //***************************************************************
        //        //Thread.Sleep(2000);

        //        bool IsBreakflag = false;
        //        MyJudgeSpec.MyGlobalParameters = pGlobalParameters;
        //        MyJudgeSpec.Judge(dtCurrentConditionResultData.Clone(), TestModelOutputArray, CurrentTestModelflag, flag_CurrentTestModel_FailBreak, out dtCurrentTestModelTestData, out IsBreakflag, out CurrentTestModelResultflag);
        //        if (IsBreakflag)
        //        {
        //            StopFlag = true;
        //        }
        //        if (!CurrentTestModelflag)
        //        {
        //            CurrentTestModelResultflag = false;
        //        }
        //        if (!CurrentTestModelResultflag)
        //        {
        //            CurrentConditionResultflag = false;
        //            TotalResultFlag = false;
        //        }
        //        switch (StrTestmodelType.ToUpper().Trim())
        //        {
        //            case "APPTXCAL":
        //            case "APPRXCAL":
        //            case "APPDUTCAL":
        //                dtLPTotalTestData.Merge(dtCurrentTestModelTestData);//合并数据
        //                break;
        //            case "APPRXFMT":
        //            case "APPTXFMT":
        //            case "APPDUTFMT":
        //            case "APPEEPROM":
        //                dtCurrentConditionResultData.Merge(dtCurrentTestModelTestData);//合并数据
        //                break;
        //            default:
        //                break;
        //        }


        //        #region 从当前TestmodelData中挑选出错误项

        //        DataRow[] DrErrorItem = dtCurrentTestModelTestData.Select("Result='fail'");

        //        String StrErrorItemMessage = "";

        //        foreach (DataRow drFail in DrErrorItem)
        //        {
        //            double ItemValue, SpecMax;
        //            ItemValue = Convert.ToDouble(drFail["ItemValue"]);
        //            String ss = drFail["SpecMax"].ToString();


        //            SpecMax = Convert.ToDouble(drFail["SpecMax"]);
        //            if (ItemValue > SpecMax)
        //            {
        //                StrErrorItemMessage += drFail["ItemName"].ToString() + " > " + SpecMax.ToString() + ",";
        //            }
        //            else
        //            {
        //                StrErrorItemMessage += drFail["ItemName"].ToString() + " < " + drFail["SpecMin"].ToString() + ",";
        //            }
        //        }

        //        if (!SaveTestProData(StrTestModelName, MyTestModelList[StrTestModelName.ToUpper().Trim()].GetProcData))
        //        {
        //            MessageBox.Show("数据存档失败，请检查网络，并且重新测试当前模块！");
        //            StopFlag = true;
        //        }
        //        if (!UpdataErrorItem(StrErrorItemMessage))
        //        {
        //            MessageBox.Show("数据存档失败，请检查网络，并且重新测试当前模块！");
        //            StopFlag = true;
        //        }
        //        if (!SaveTestResult(StrTestModelName, dtCurrentTestModelTestData))
        //        {
        //            MessageBox.Show("数据存档失败，请检查网络，并且重新测试当前模块！");
        //            StopFlag = true;
        //        }
        //        //if (!SaveTestProData(StrTestModelName, MyTestModelList[StrTestModelName.ToUpper().Trim()].GetProcData)

        //        //||!UpdataErrorItem(StrErrorItemMessage)
        //        //||!SaveTestResult(StrTestModelName, dtCurrentTestModelTestData))
        //        //{
        //        //    MessageBox.Show("数据存档失败，请检查网络，并且重新测试当前模块！");
        //        //     StopFlag = true;
        //        //}

        //        #endregion

        //        WriteOutputLog();

        //        QueueShow.Enqueue("Temp=" + StrCurrentTemp + "**" + "VCC=" + StrCurrentVcc + "**" + "Channel=" + CurrentChannel + "**" + StrTestModelName + "Testing End.....");

        //        MyLogManager.AdapterLogString(1, "Temp=" + StrCurrentTemp + "**" + "VCC=" + StrCurrentVcc + "**" + "Channel=" + CurrentChannel + "**" + StrTestModelName + "Testing End.....");

        //        return CurrentTestModelResultflag;

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //        CurrentTestModelResultflag = false;
        //        TotalResultFlag = false;
        //        return false;
        //    }
        //}


        private bool RunTestModel()
        {
          
           
            if (MyAttribute.IsTestTx)
            {
                MyTestModeArray.pTestModel = new TestTxEye(MyEquipmentstruct.pDut, MyAttribute.MylogManager);
            }
            else
            {
                MyTestModeArray.pTestModel = new TestBer(MyEquipmentstruct.pDut, MyAttribute.MylogManager);
            }
            try
            {
                if (!MyTestModeArray.pTestModel.SelectEquipment(MyEquipmentstruct.pEquipmentList))
                {
                    MessageBox.Show(MyTestModeArray.pTestModel.ToString().ToUpper().Trim() + "缺少必须设备,请检查设备...");

                    return false;
                }

                MyTestModeArray.pTestModel.SetGlobalParameters = MyTestModeArray.pglobalParameters;
                //MyTestModeArray.pglobalParameters.
                MyTestModeArray.CurrentTestModelflag = MyTestModeArray.pTestModel.RunTest();

                TestModeEquipmentParameters[] TempArray = MyTestModeArray.pTestModel.GetoutputParameters;

                if (!CombineTestData(TempArray, out MyAttribute.dtCurrentTestModelResultData))
                {
                    return false;

                }

                string[] TestDataArray = Array.ConvertAll<object, string>(MyAttribute.dtCurrentTestModelResultData.Rows[0].ItemArray, Convert.ToString);
                MyAttribute.MyOperateTxT.WriteTxt(TestDataArray);
                MyAttribute.dtTotalTestData.Merge(MyAttribute.dtCurrentTestModelResultData);

            }
            catch
            {
                MyTestModeArray.CurrentTestModelflag = false;

            }


            

            return true;
        }

        private bool SetCondition()
        {

            if (MyEquipmentstruct.pPowersupply != null)
            {
                MyEquipmentstruct.pPowersupply.ConfigVoltageCurrent(MyCondition.CurrentVcc.ToString());
            }

            if (MyEquipmentstruct.pTempControl != null)
            {
                MyEquipmentstruct.pTempControl.SetPointTemp(MyCondition.CurrentTemp);
            }
            return true;
        }

    
        #endregion

        #region  Form Event

        #region 窗体动画

        [System.Runtime.InteropServices.DllImport("user32")]
        private static extern bool AnimateWindow(IntPtr hwnd, int dwTime, int dwFlags);
        /*
         * 函数功能：该函数能在显示与隐藏窗口时能产生特殊的效果。有两种类型的动画效果：滚动动画和滑动动画。
         * 函数原型：BOOL AnimateWindow（HWND hWnd，DWORD dwTime，DWORD dwFlags）；
         * hWnd：指定产生动画的窗口的句柄。
         * dwTime：指明动画持续的时间（以微秒计），完成一个动画的标准时间为200微秒。
         * dwFags：指定动画类型。这个参数可以是一个或多个下列标志的组合。
         * 返回值：如果函数成功，返回值为非零；如果函数失败，返回值为零。
         * 在下列情况下函数将失败：窗口使用了窗口边界；窗口已经可见仍要显示窗口；窗口已经隐藏仍要隐藏窗口。若想获得更多错误信息，请调用GetLastError函数。
         * 备注：可以将AW_HOR_POSITIVE或AW_HOR_NEGTVE与AW_VER_POSITVE或AW_VER_NEGATIVE组合来激活一个窗口。
         * 可能需要在该窗口的窗口过程和它的子窗口的窗口过程中处理WM_PRINT或WM_PRINTCLIENT消息。对话框，控制，及共用控制已处理WM_PRINTCLIENT消息，缺省窗口过程也已处理WM_PRINT消息。
         * 速查：WIDdOWS NT：5.0以上版本：Windows：98以上版本；Windows CE：不支持；头文件：Winuser.h；库文件：user32.lib。
     */
        //标志描述：
        const int AW_SLIDE = 0x40000;//使用滑动类型。缺省则为滚动动画类型。当使用AW_CENTER标志时，这个标志就被忽略。
        const int AW_ACTIVATE = 0x20000;//激活窗口。在使用了AW_HIDE标志后不要使用这个标志。
        const int AW_BLEND = 0x80000;//使用淡出效果。只有当hWnd为顶层窗口的时候才可以使用此标志。
        const int AW_HIDE = 0x10000;//隐藏窗口，缺省则显示窗口。(关闭窗口用)
        const int AW_CENTER = 0x0010;//若使用了AW_HIDE标志，则使窗口向内重叠；若未使用AW_HIDE标志，则使窗口向外扩展。
        const int AW_HOR_POSITIVE = 0x0001;//自左向右显示窗口。该标志可以在滚动动画和滑动动画中使用。当使用AW_CENTER标志时，该标志将被忽略。
        const int AW_VER_POSITIVE = 0x0004;//自顶向下显示窗口。该标志可以在滚动动画和滑动动画中使用。当使用AW_CENTER标志时，该标志将被忽略。
        const int AW_HOR_NEGATIVE = 0x0002;//自右向左显示窗口。该标志可以在滚动动画和滑动动画中使用。当使用AW_CENTER标志时，该标志将被忽略。
        const int AW_VER_NEGATIVE = 0x0008;//自下向上显示窗口。该标志可以在滚动动画和滑动动画中使用。当使用AW_CENTER标志时，该标志将被忽略

        #endregion
        #region 控件跟随界面大小变动

        private float X;

        private float Y;

        private void setTag(Control cons)
        {
            foreach (Control con in cons.Controls)
            {
                con.Tag = con.Width + ":" + con.Height + ":" + con.Left + ":" + con.Top + ":" + con.Font.Size;
                if (con.Controls.Count > 0)
                    setTag(con);
            }
        }
        private void setControls(float newx, float newy, Control cons)
        {
            foreach (Control con in cons.Controls)
            {

                string[] mytag = con.Tag.ToString().Split(new char[] { ':' });
                float a = Convert.ToSingle(mytag[0]) * newx;
                con.Width = (int)a;
                a = Convert.ToSingle(mytag[1]) * newy;
                con.Height = (int)(a);
                a = Convert.ToSingle(mytag[2]) * newx;
                con.Left = (int)(a);
                a = Convert.ToSingle(mytag[3]) * newy;
                con.Top = (int)(a);
                Single currentSize = Convert.ToSingle(mytag[4]) * Math.Min(newx, newy);
                con.Font = new Font(con.Font.Name, currentSize, con.Font.Style, con.Font.Unit);
                if (con.Controls.Count > 0)
                {
                    setControls(newx, newy, con);
                }
            }

        }

        void Form1_Resize(object sender, EventArgs e)
        {
            float newx = (this.Width) / X;
            float newy = this.Height / Y;
            setControls(newx, newy, this);
            // this.Text = this.Width.ToString() + " " + this.Height.ToString();

        }

        #endregion  

        private void labelFw_Click(object sender, EventArgs e)
        {

        }

        private void textBoxFW_TextChanged(object sender, EventArgs e)
        {

        }

        private void TestForm_Load(object sender, EventArgs e)
        {

            //Sunisoft.IrisSkin.SkinEngine skin = new Sunisoft.IrisSkin.SkinEngine();
            //skin.SkinFile = Application.StartupPath + @"\OneBlue.ssk";

            #region 记录窗体信息

            //在Form_Load里面添加:
            this.Resize += new EventHandler(Form1_Resize);
            X = this.Width;
            Y = this.Height;
            setTag(this);
            Form1_Resize(new object(), new EventArgs());//x,y可在实例化时赋值,最后这句是新加的，在MDI时有用

            #endregion

          //  AutoSizeColumnsMode
            //加载皮肤            
            //Sunisoft.IrisSkin.SkinEngine skin = new Sunisoft.IrisSkin.SkinEngine();
          //  skinEngine1.SkinFile = Application.StartupPath + @"\OneBlue.ssk";
           // skinEngine1.SkinFile = System.Environment.CurrentDirectory + "\\Skins\\所选皮肤名(后缀为.ssk)";  //选择皮肤文件
            ////skin.SkinFile = System.Environment.CurrentDirectory + "\\skins\\" + "Emerald.ssk";            
            //skin.Active = true;

        

            this.dataGridView_Condition.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;


            dataGridView_Condition.AllowUserToAddRows = false;  
            this.labelVer.Text = "Version:" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            labelVer.Refresh();

            this.labelbuildTime.Text = "BuildTime:" + System.IO.File.GetLastWriteTime(this.GetType().Assembly.Location).ToString("yyyy/MM/dd HH:mm:ss");

            var ss1 = ReadProductionTpye();

            comboBoxPType.Items.Clear();

            foreach (string s in ss1)
            {
                comboBoxPType.Items.Add(s);
            }
          //  MyAttribute.ConditionCycle = Convert.ToInt16(ConditionCycle.Text);
        }

        private void comboBoxPType_SelectedIndexChanged(object sender, EventArgs e)
        {

            comboBoxPN.Items.Clear();

            #region     Get the id of Current ProductType

            string ProductionTypeName = comboBoxPType.SelectedItem.ToString();

            DataTable dd = new DataTable();
            //  string[] arry = dtProductionType.AsEnumerable().Select(d => d.Field<string>("ItemName")).ToArray();
            DataRow[] dr = MyAttribute.dtProductionType.Select("ItemName='" + ProductionTypeName + "'");
            MyAttribute.IdPnType = Convert.ToInt32(dr[0]["ID"].ToString());

            #endregion
            MyAttribute.ProductionTypeName = ProductionTypeName;
            #region  Fit ProductionName Combox

            string Str = "Select* from GlobalProductionName where PID=" + MyAttribute.IdPnType + " and IgnoreFlag='false' order by id";
            string sTRtb = "GlobalProductionName";
            dd = MyAttribute.MyDataio.GetDataTable(Str, sTRtb);
            comboBoxPN.Items.Clear();
            comboBoxPN.Text = "";
            comboBoxPN.Refresh();
            for (int i = 0; i < dd.Rows.Count; i++)
            {
                comboBoxPN.Items.Add(dd.Rows[i]["PN"].ToString());
            }


            #endregion

        }

        private void comboBoxPN_SelectedIndexChanged(object sender, EventArgs e)
        {


            if (comboBoxPType.Text != null)
            {
                #region    获取当前Pn 以及其对应的ID号
                MyAttribute.StrProductionName = comboBoxPN.SelectedItem.ToString();
                DataTable dd = new DataTable();
                string Str = "Select* from GlobalProductionName where GlobalProductionName.pid=" + MyAttribute.IdPnType + " and GlobalProductionName.IgnoreFlag='false' and GlobalProductionName.PN='" + MyAttribute.StrProductionName + "' order by GlobalProductionName.id";
                DataTable dtProductionName = MyAttribute.MyDataio.GetDataTable(Str, "GlobalProductionName");
                DataRow[] dr = dtProductionName.Select("PN='" + MyAttribute.StrProductionName + "'");
                //  //IdPnName = Convert.ToInt32(dr[0]["ID"].ToString());
                MyAttribute.MCoefsID = Convert.ToInt32(dr[0]["MCoefsID"].ToString());
                MyAttribute.TotalChannnel = Convert.ToInt32(dr[0]["Channels"].ToString());
                //  MyAttribute

                //MyAttribute.pGlobalParameters.

                #endregion



            }
        }

        private void button_Test_Click(object sender, EventArgs e)
        {
          

            if (RdbTX.Checked)
            {
                MyAttribute.IsTestTx = true;
            }
            
            MyCondition.dtCondition = (DataTable)dataGridView_Condition.DataSource;
            //MyAttribute.CycleTime = int.Parse(this.textBox_CycleTime.Text);
           

            this.labelShow.Text = "ReadyForTest.....";
            this.labelShow.Refresh();
            if (ReadyForTest())
            {
                // MyAttribute
              

               //// this.labelShow.Text = "First Temp Waiting.....";
               // this.labelShow.Refresh();

              //  MyAttribute.MylogManager.AdapterLogString(0, "***********************  SN=" + MyAttribute.CurrentSN + " ************************");
                // MyAttribute.MyDataio.WriterSN(MyAttribute.StuctTestplanPrameter.Id, MyAttribute.CurrentSN, MyAttribute.Current_DutFwRev, MyAttribute.StrIpAddress, StrLightSourceMessage, StrReMark, out MyAttribute.SNID);

                button_Test.BackColor = Color.Yellow;
                button_Test.Refresh();

                // TimeSpanUse.DateDiff(MyAttribute.TimeSpanStart, MyAttribute.MyLogManager, "Prepare Time for Test ", out MyAttribute.TimeSpanStart);
                MyAttribute.MylogManager.AdapterLogString(0, "***********************  SN=" + MyAttribute.CurrentSN + " ************************");


                string[] ColumnHeadArray = new string[MyAttribute.dtTotalTestData.Columns.Count];

                for (int i = 0; i < MyAttribute.dtTotalTestData.Columns.Count; i++)
                {
                    ColumnHeadArray[i] = MyAttribute.dtTotalTestData.Columns[i].ColumnName;
                }
                //  string[] TestDataArray = Array.ConvertAll<object, string>(MyAttribute.dtCurrentConditionResultData.Rows[0].ItemArray, Convert.ToString);

                MyAttribute.MyOperateTxT.WriteTxt(ColumnHeadArray);

                //MyAttribute.TotalResultFlag = true;
                panelEquipment.Enabled = false;
                MyAttribute.TestThread = new Thread(Test);
                MyAttribute.TestThread.Start();
                MyAttribute.TestThread.Priority = ThreadPriority.Highest;
                MyAttribute.FlagFlowControllEnd = false;
                MyAttribute.RefreshThread = new Thread(Refresh_GUI);
                MyAttribute.RefreshThread.Start();
                MyAttribute.RefreshThread.Priority = ThreadPriority.Lowest;
                button_Test.BackColor = Color.Yellow;
                button_Test.Refresh();
            }
            else//测试前的准备出错....
            {
                MyAttribute.FlagFlowControllEnd = true;
                MyAttribute.MylogManager.AdapterLogString(3, "测试前的准备出错");
                labelShow.Text = "测试前的准备出错,请检查测试前的准备状况,比如EVB,I2C.....";

                labelShow.Refresh();

                labelShow.Refresh();

                textBoxSN.Text = "";
                textBoxSN.Refresh();
                button_Test.Enabled = true;
                comboBoxPN.Enabled = true;
                comboBoxPType.Enabled = true;

                if (MyEquipmentstruct.pTempControl != null)
                {
                    MyEquipmentstruct.pTempControl.SetPositionUPDown("0");

                }
                progress.Value = 0;
                progress.Refresh();

                panelCombox.Enabled = true;

            }
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {


            if (MessageBox.Show("Do you want to Stop Test?", " Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                MyAttribute.StopFlag = true;
            }
           
        }

        private void button_Config_Click(object sender, EventArgs e)
        {
           
            this.progress.Value = 0;
            this.progress.Refresh();
            this.labelProgress.Text = "0";
            this.labelProgress.Refresh();


            string StrTime = DateTime.Now.ToString("yyyy-MM-dd");
            string StrPathOptEyeDiagram = Application.StartupPath + @"\EyeDiagram\" + comboBoxPType.Text.ToUpper() + "\\" + comboBoxPN.Text.ToUpper() +   "\\" + StrTime + "\\OptEyeDiagram\\";

            if (!Directory.Exists(StrPathOptEyeDiagram))
            {
                Directory.CreateDirectory(StrPathOptEyeDiagram);
            }
            // pflowControl.StrEyeDiagramPath = StrPathEyeDiagram;
            MyAttribute.StrOptEyeDiagramPath=StrPathOptEyeDiagram + "\\";

            SelectResultHead();
            SelectEquipment();

            try
            {
                if (comboBoxPN.Text != "")
                {
                    labelShow.Text = "Equipment Configing.......";
                    labelShow.Refresh();
                    string datatime = DateTime.Now.ToString("yyyy-MM-dd");
                    string filepath = Application.StartupPath + "\\" + MyAttribute.ProductionTypeName + "_" + MyAttribute.StrProductionName.ToUpper() + "_" + datatime + ".TXT";

                    MyAttribute.MyOperateTxT = new OperateTxT(filepath);

                    Thread Config = new Thread(ConfigEquipment);

                    Config.Start();


                   // tabControl1.SelectedIndex = 4;
                }
                else
                {
                    MessageBox.Show("Select ProductionName");
                }
            }
            catch (Exception Me)
            {
                MessageBox.Show("ConfigEquipment Error ,PLS Check Equipment");
                MyAttribute.FlagEquipmentConfigOK = false;
            }




        }

        private void button_Del_Click(object sender, EventArgs e)
        {
            int indexid = dataGridView_Condition.CurrentRow.Index;
            if (indexid > 0)
            {

                DataGridViewRow row = dataGridView_Condition.Rows[indexid];
                dataGridView_Condition.Rows.Remove(row);
            }
            dataGridView_Condition.Refresh();
        }

     

        public DataTable GetDgvToTable(DataGridView dgv)
        {
            DataTable dt = new DataTable();
            for (int count = 0; count < dgv.Columns.Count; count++)
            {
                DataColumn dc = new DataColumn(dgv.Columns[count].Name.ToString());
                dt.Columns.Add(dc);
            }
            for (int count = 0; count < dgv.Rows.Count; count++)
            {
                DataRow dr = dt.NewRow();
                for (int countsub = 0; countsub < dgv.Columns.Count; countsub++)
                {
                    dr[countsub] = Convert.ToString(dgv.Rows[count].Cells[countsub].Value);
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {



            MyAttribute.MyXml.DeleteAllConditionNode();

        }

        private void button_Add_Click(object sender, EventArgs e)
        {
            this.dataGridView_Condition.AllowUserToAddRows = true;
            dataGridView_Condition.Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string datatime = DateTime.Now.ToString("yyyy-MM-dd");

            //MyAttribute.ProductionTypeName.ToUpper()
            string filepath = Application.StartupPath + "\\" + MyAttribute.ProductionTypeName + "_" + MyAttribute.StrProductionName.ToUpper() + "_" + datatime + ".TXT";
            OperateTxT op = new OperateTxT(filepath);
            string[] A = new string[] { "a", "b", "c" };
            op.WriteTxt(A);

        }

        private void dataGridView_Condition_AllowUserToAddRowsChanged(object sender, EventArgs e)
        {

            int RowCount = this.dataGridView_Condition.Rows.Count;
            for (int i = 1; i < RowCount + 1; i++)
            {
                this.dataGridView_Condition.Rows[i - 1].Cells[0].Value = i;
            }


        }

        private void dataGridView_Condition_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.dataGridView_Condition.Rows[e.RowIndex].Cells[0].Value = e.RowIndex + 1;
            this.dataGridView_Condition.Refresh();
        }

        private void TestForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Environment.Exit(System.Environment.ExitCode);
            this.Dispose();
            this.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox_CycleTime_TextChanged(object sender, EventArgs e)
        {

        }

        private void button_SaveCondition_Click(object sender, EventArgs e)
        {

          //  MyAttribute.ConditionCycle = 1;
            MyAttribute.MyXml.DeleteAllConditionNode();
            //  ConditionCycle.Text = "1";
            this.dataGridView_Condition.AllowUserToAddRows = false;
            dataGridView_Condition.Refresh();
            // MyCondition.dtCondition 

            DataTable dt = GetDgvToTable(this.dataGridView_Condition);

            MyCondition.dtCondition.Clear();

           
            MyCondition.dtCondition.Merge(dt);
            

            for (int i = 0; i < MyCondition.dtCondition.Rows.Count; i++)
            {
                MyCondition.dtCondition.Rows[i]["Sequence"] = i;
            }

            this.dataGridView_Condition.DataSource = MyCondition.dtCondition;

            this.dataGridView_Condition.Refresh();

            MyCondition.ConditionHeardArray = new string[MyCondition.dtCondition.Columns.Count];

            for (int i = 0; i < MyCondition.dtCondition.Columns.Count; i++)
            {
                MyCondition.ConditionHeardArray[i] = MyCondition.dtCondition.Columns[i].ToString();
            }

            for (int i = 0; i < MyCondition.dtCondition.Rows.Count; i++)
            {
                DataRow dr = MyCondition.dtCondition.Rows[i];

                // string[] A = dr.ItemArray;
                string[] ConditionArray = Array.ConvertAll<object, string>(dr.ItemArray, Convert.ToString);
                MyAttribute.MyXml.FitConditionInfToXml(i.ToString(), MyCondition.ConditionHeardArray, ConditionArray);
            }


        }


        private void button_SaveCondition_MouseLeave(object sender, EventArgs e)
        {
            this.button_SaveCondition.Location = new Point(this.button_SaveCondition.Location.X - 3, this.button_SaveCondition.Location.Y + 3);

        }

        private void button_SaveCondition_MouseEnter(object sender, EventArgs e)
        {
            this.button_SaveCondition.Location = new Point(this.button_SaveCondition.Location.X + 3, this.button_SaveCondition.Location.Y - 3);

        }

        private void button_SaveCondition_MouseDown(object sender, MouseEventArgs e)
        {
            //  this.button_SaveCondition.Location = new Point(this.button_SaveCondition.Location.X - 3, this.button_SaveCondition.Location.Y + 3);

        }

        private void button_Del_MouseEnter(object sender, EventArgs e)
        {
            this.button_Del.Location = new Point(this.button_Del.Location.X + 3, this.button_Del.Location.Y - 3);

        }

        private void button_Del_MouseLeave(object sender, EventArgs e)
        {
            this.button_Del.Location = new Point(this.button_Del.Location.X - 3, this.button_Del.Location.Y + 3);

        }

        private void button_Add_MouseEnter(object sender, EventArgs e)
        {
            this.button_Add.Location = new Point(this.button_Add.Location.X + 3, this.button_Add.Location.Y - 3);
        }

        private void button_Add_MouseLeave(object sender, EventArgs e)
        {
            this.button_Add.Location = new Point(this.button_Add.Location.X - 3, this.button_Add.Location.Y + 3);
        }

        private void button_Config_MouseEnter(object sender, EventArgs e)
        {
            this.button_Config.Location = new Point(this.button_Config.Location.X + 3, this.button_Config.Location.Y - 3);
        }

        private void button_Config_MouseLeave(object sender, EventArgs e)
        {
            this.button_Config.Location = new Point(this.button_Config.Location.X - 3, this.button_Config.Location.Y + 3);
        }

        private void button_Test_MouseEnter(object sender, EventArgs e)
        {
            this.button_Test.Location = new Point(this.button_Test.Location.X + 3, this.button_Test.Location.Y - 3);
        }

        private void button_Test_MouseLeave(object sender, EventArgs e)
        {
            this.button_Test.Location = new Point(this.button_Test.Location.X - 3, this.button_Test.Location.Y + 3);
        }

        private void buttonStop_MouseEnter(object sender, EventArgs e)
        {
            this.buttonStop.Location = new Point(this.buttonStop.Location.X + 3, this.buttonStop.Location.Y - 3);
        }

        private void buttonStop_MouseLeave(object sender, EventArgs e)
        {
            this.buttonStop.Location = new Point(this.buttonStop.Location.X - 3, this.buttonStop.Location.Y + 3);
        }
    

       #endregion

        private void RdbTX_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void RdbBert_MP1800_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void buttonOffset_Click(object sender, EventArgs e)
        {

            if (MyAttribute.FlagEquipmentConfigOK)
            {
                try
                {


                    //----------------Scope
                    ArrayList ScopeOffsetArray = new ArrayList();
                    ScopeOffsetArray.Clear();
                    ScopeOffsetArray.Add(ScopeOffset1.Text);
                    ScopeOffsetArray.Add(ScopeOffset2.Text);
                    ScopeOffsetArray.Add(ScopeOffset3.Text);
                    ScopeOffsetArray.Add(ScopeOffset4.Text);

                    string StrScopeOffsetArray = ScopeOffsetArray[0].ToString();

                    for (int i = 0; i < 4; i++)
                    {
                        StrScopeOffsetArray += "," + ScopeOffsetArray[i].ToString();
                    }

                    if (MyEquipmentstruct.pScope != null)
                    {
                        for (int i = 1; i < 5;i++ )
                        {
                            MyEquipmentstruct.pScope.configoffset(i.ToString(), ScopeOffsetArray[i-1].ToString());
                        }
                    }

                    //---------------------AttOffset
                    ArrayList AttOffsetArray = new ArrayList();
                    AttOffsetArray.Clear();
                    AttOffsetArray.Add(AttOffset1.Text);
                    AttOffsetArray.Add(AttOffset2.Text);
                    AttOffsetArray.Add(AttOffset3.Text);
                    AttOffsetArray.Add(AttOffset4.Text);

                    string StrAttOffsetArray = AttOffsetArray[0].ToString();

                    for (int i = 1; i < 4; i++)
                    {
                        StrAttOffsetArray += "," + AttOffsetArray[i].ToString();
                    }
                    if (MyEquipmentstruct.pAtt != null)
                    {
                        for (int i = 1; i < 5; i++)
                        {
                            MyEquipmentstruct.pAtt.configoffset(i.ToString(), AttOffsetArray[i - 1].ToString());
                        }
                    }
                    
                    //-----------------------------LightSource
                    ArrayList LightSourceErArray = new ArrayList();
                    LightSourceErArray.Clear();
                    LightSourceErArray.Add(textBoxLsER1.Text);
                    LightSourceErArray.Add(textBoxLsER2.Text);
                    LightSourceErArray.Add(textBoxLsER3.Text);
                    LightSourceErArray.Add(textBoxLsER4.Text);

                    string StrErArray = LightSourceErArray[0].ToString();

                    for (int i = 1; i < 4; i++)
                    {
                        StrErArray += "," + LightSourceErArray[i].ToString();
                    }

                    MyTestModeArray.pglobalParameters.OpticalSourseERArray = StrErArray;

                    //---------------------IccOffset & PsOffset           
                
                    
                    buttonOffset.BackColor = Color.Green;
                    button_Test.Enabled = true;
                }
                catch
                {
                  
                    buttonOffset.BackColor = Color.Red;
                    MessageBox.Show("PLS Config Equipment First");
                }
            }
            else
            {
                MessageBox.Show("PLS Config Equipment First");
            }
        
        
        }
   
    }
    public class hp// 记录原始窗体的尺寸
    {
        public Size s { set; get; }
        public Point p { set; get; }
    }
}

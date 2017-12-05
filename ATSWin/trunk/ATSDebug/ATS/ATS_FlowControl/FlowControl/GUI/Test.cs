using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ATS_Framework;
using System.Runtime.InteropServices;
using ATS_Driver;
using System.Security;
using System.Data.OleDb;
using System.IO;
using System.Xml.Linq;
using System.Threading;
using System.Collections;
namespace ATS
{
    
    public partial class FormATS : Form
    {
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
        #region Thread

        private Thread TestThread;
        private Thread RefreshThread;

#endregion
        


        private string TestStartTime;//为每个一颗产品开始测试时间,后面存放本地Log.TXT 的序号之后
        private bool isDragging = false; //拖中
        //private PictureBox P_PS, P_PS1, P_PS2, P_Scope, P_PPG;
        //private int currentX = 0, currentY = 0; //原来鼠标X,Y坐标


        //-------------------------------

        private EquipmentManage MyEquipmentManage;
        private EquipmentList aEquipList;
        private FlowControll pflowControl;

        //--------------------------
        private logManager plogManager = new logManager();
        private ConfigXmlIO MyXml;
        private ArrayList ScopeOffsetArray = new ArrayList();
        private ArrayList PsOffsetArray = new ArrayList();
        private ArrayList VccOffsetArray = new ArrayList();
        private ArrayList AttOffsetArray = new ArrayList();
        private ArrayList LightSourceErArray = new ArrayList();
        private ArrayList IccOffsetArray = new ArrayList();

       //----------------------------
        private DataTable dtProductionType=new DataTable();
        private DataTable dtProductionName = new DataTable();
        private DataTable dtTestplan = new DataTable();
        private int IdPnName;
        private int IdPnType;
        private int IdTestplan;     
      
        private bool FlagEquipmentConfigOK = false;
        private bool FlagEquipmentOffSetOK = false;
        private bool FlagDrvierInitializeOK = false;
        private int RefreashTime = 0;
        private EquipmentArray MyEquipmentArrayy = new EquipmentArray();
      //  private string[,] EquipmenNameArray;
        private DataTable dtEquipmenList;
        private string StrIP = "";

        private string StrLightSourceMessage = "";
        private  string StrReMark = "";
        private bool flag_ShowErrorData=true;
       // private String StrPathEyeDiagram;
#region  委托声明

        public delegate void UpdataDataTable(DataGridView Dgv, DataTable dt);
        public delegate void UpdataLable(Label L1, string label);
        public delegate void UpdataRichBox(RichTextBox R1, string label);
        public delegate void UpdataProcessbar(ProgressBar p1, int process);
        public delegate void UpdataResult(bool ResultFlag);
        public delegate void UpdataButton(Button B1, bool flag, Color c1);
        public delegate void UpdataPanel(Panel B1, bool flag);
        public delegate void UpdataCombox(ComboBox B1, bool flag);
        public delegate void UpdataResultShow(bool ResultFlag, DataTable dt, bool ShowErrorData);
#endregion
        public FormATS()
        {
           // Control.CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();

           // AnimateWindow(this.Handle, 1000, 0X00080000 + 0X00000002);

          //  AnimateWindow(this.Handle, 1000, AW_CENTER);

            MyEquipmentManage=new EquipmentManage(plogManager);
            aEquipList=new EquipmentList();

         

            ReadXmlInf();

            if (MyXml.DataBaseUserLever == "1")
            {

                if (MessageBox.Show("Do you want to Use Location Database?", "Database Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    OpenFileDialog openFileDialog1 = new OpenFileDialog();
                    openFileDialog1.InitialDirectory = "D:\\Patch";
                    openFileDialog1.Filter = "All files (*.accdb)|*.accdb|All files (*.*)|*.* ";
                    // dlg.Filter = "Image Files(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|All files (*.*)|*.* "
                    openFileDialog1.FilterIndex = 1;
                    openFileDialog1.RestoreDirectory = true;
                    MyXml.DatabaseType = "LocationDatabase";
                    if (openFileDialog1.ShowDialog() == DialogResult.OK)
                        MyXml.DatabasePath = openFileDialog1.FileName;

                    pflowControl = new FlowControll(MyXml, plogManager);
                }
                else
                {
                    MyXml.DatabaseType = "SqlDatabase";
 
                    pflowControl = new FlowControll(MyXml, plogManager);
                }
            }
            else
            {
                MyXml.DatabaseType = "SqlDatabase";
                pflowControl = new FlowControll(MyXml, plogManager);

            }

        }
        //[DllImport("user32.dll")]
        // private static extern bool AnimateWindow(IntPtr hwnd,int dwtime,int dwflag);// 复位CH375设备,返回句柄,出错则无效

        private bool ReadXmlInf()
        {
           string[] Array1 = new string[4];
           string StrValue=null;

            MyXml = new ConfigXmlIO(Application.StartupPath + "\\Config.xml");

            
            //-------------------ScopeOffset
            StrValue = MyXml.ScopeOffset;
            Array1 = StrValue.Split(',');

            ScopeOffsetArray.Clear();

            for (int i = 0; i < Array1.Length; i++)
            {
                ScopeOffsetArray.Add(Array1[i]);
            }

            ScopeOffset1.Text = Array1[0];
            ScopeOffset1.Refresh();
            ScopeOffset2.Text = Array1[1];
            ScopeOffset2.Refresh();
            ScopeOffset3.Text = Array1[2];
            ScopeOffset3.Refresh();
            ScopeOffset4.Text = Array1[3];
            ScopeOffset4.Refresh();

           //------------------AttOffset
            StrValue = MyXml.AttennuatorOffset;
            Array1 = StrValue.Split(',');
            AttOffsetArray.Clear();

            for (int i = 0; i < Array1.Length; i++)
            {
                AttOffsetArray.Add(Array1[i]);
            }
            AttOffset1.Text = Array1[0];
            AttOffset2.Text = Array1[1];
            AttOffset3.Text = Array1[2];
            AttOffset4.Text = Array1[3];

            AttOffset1.Refresh();
            AttOffset2.Refresh();
            AttOffset3.Refresh();
            AttOffset4.Refresh();
            //------------------LightSource
            StrValue = MyXml.LightSourceEr;
            Array1 = StrValue.Split(',');
            LightSourceErArray.Clear();

            for (int i = 0; i < Array1.Length; i++)
            {
                LightSourceErArray.Add(Array1[i]);
            }
            textBoxLsER1.Text = Array1[0];
            textBoxLsER2.Text = Array1[1];
            textBoxLsER3.Text = Array1[2];
            textBoxLsER4.Text = Array1[3];

            textBoxLsER1.Refresh();
            textBoxLsER2.Refresh();
            textBoxLsER3.Refresh();
            textBoxLsER4.Refresh();


            VccOffset.Text = MyXml.VccOffset;
            VccOffset.Refresh();

            IccOffset.Text = MyXml.IccOffset;
            IccOffset.Refresh();
            return true;
        }
        private void ComboxUpdata(ComboBox B1, bool flag)
        {
            try
            {
                if (B1.InvokeRequired)
                {
                    UpdataCombox Box = new UpdataCombox(ComboxUpdata); ;
                    B1.BeginInvoke(Box, new Object[] { B1, flag });
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

                        B1.Refresh();
                    }

                }
            }
            catch (System.Exception ex)
            {
            	
            }
            
        }
        private void PanelUpdata(Panel B1, bool flag)
        {
            try
            {
                if (B1.InvokeRequired)
                {
                    UpdataPanel Box = new UpdataPanel(PanelUpdata); ;
                    B1.BeginInvoke(Box, new Object[] { B1, flag });
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

                       // B1.Refresh();
                    }

                }
            }
            catch (System.Exception ex)
            {

            }

        }
        private void ButtonUpdata(Button B1,bool flag,Color c1)
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
                        B1.BackColor = c1;
                        B1.Refresh();
                    }

                }
            }
            catch
            {

            }
        }

        private bool ReadyForTest()
        {
      
           // Powersupply aPS;

            plogManager.AdapterLogString(1, "$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$A New Test Start$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$");
            plogManager.AdapterLogString(0, "chK_Fw Funtion=" + chK_Fw.CheckState);
            plogManager.AdapterLogString(0, "chk_AbNormal_Lab Funtion=" + chk_AbNormal_Lab.CheckState);
            plogManager.AdapterLogString(0, "chK_Aotu_Evb_Voltage Funtion=" + chK_Auto_Evb_Voltage.CheckState);


            pflowControl.prosess = 0;
            FlagDrvierInitializeOK = true;
            if (FlagEquipmentConfigOK&&FlagEquipmentOffSetOK)
            {
                dataGridViewTotalData.Rows.Clear();
                dataGridViewLPTotalData.Rows.Clear();
                dataGridViewLPTotalData.Refresh();
               // Thread.Sleep(1000);
                dataGridViewTotalData.Refresh();

  #region  Disable Control

                button_Config.Enabled = false;
                btnExport.Enabled = false;
                panelCombox.Enabled = false;
                panelOffset.Enabled = false;
 #endregion

                richInterfaceLog.Focus();

                button_Test.Enabled = false;

                if (pflowControl.TotalTestData != null)
                {
                    pflowControl.TotalTestData.Clear();
                }
                if (pflowControl.dtLPTotalTestData != null)
                {
                    pflowControl.dtLPTotalTestData.Clear();
                }

                TestStartTime = pflowControl.MyDataio.GetCurrTime().ToString("yyyy/MM/dd HH:mm:ss");
                textBoxSN.Text = "";

                labelShow.Text = "Prepare Read SN....";
                labelShow.Refresh();
                try
                {
                    pflowControl.pPS.OutPutSwitch(true);
                   pflowControl.pDut.FullFunctionEnable();

                    if (!JudgeSerialNO()) return false;//判定序列号是否正常
                    TimeSpanUse.DateDiff(pflowControl.TimeSpanStart, pflowControl.MyLogManager, "Prepare Time for Test -JudgeSerialNO ", out pflowControl.TimeSpanStart);

                    if (!pflowControl.pDut.FullFunctionEnable()) return false;

                   if(!JudegEvb())return false;
                   TimeSpanUse.DateDiff(pflowControl.TimeSpanStart, pflowControl.MyLogManager, "Prepare Time for Test -JudegEvb ", out pflowControl.TimeSpanStart);
              
                   //if(!JudgeSerialNO())return false;//判定序列号是否正常
                   //TimeSpanUse.DateDiff(pflowControl.TimeSpanStart, pflowControl.MyLogManager, "Prepare Time for Test -JudgeSerialNO ", out pflowControl.TimeSpanStart);
              
                   if(!JudgeFwVer()) return false; ;//判定Fw版本号是否正常
                   TimeSpanUse.DateDiff(pflowControl.TimeSpanStart, pflowControl.MyLogManager, "Prepare Time for Test -JudgeFwVer ", out pflowControl.TimeSpanStart);
              
                    return true;
                      
                }
                catch(Exception ex)
                {
                    MessageBox.Show("I2C Read Error",ex.Message+"PLS Check the Connecter");

                    labelShow.Text = "Prepare Test Error";
                    labelShow.Refresh(); 

                    textBoxSN.Text = "";
                    textBoxSN.Refresh();
                    button_Test.Enabled = true;
                    comboBoxPN.Enabled = true;
                    comboBoxPType.Enabled = true;
                    comboBoxTestPlan.Enabled = true;

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
        private void button_Test_Click(object sender, EventArgs e)
        {
            buttonStop.Enabled = true;

            if (chk_AbNormal_Lab.CheckState == CheckState.Checked)
            {
                if (Text_Remark.Text.Trim()=="")
                {
                    StrReMark = "非法测试且未填写原因";
                }
                else
                {
                    StrReMark = Text_Remark.Text.Trim();
                }

                StrReMark = Text_Remark.Text.Trim();
            }
            else
            {
                StrReMark = "正常";
            }

            StrLightSourceMessage = "AP(dbm)=" + MyXml.AttennuatorOffset + ";ER=" + MyXml.LightSourceEr;
            plogManager.FlushLogBuffer();

         //   DateTime StartTime = DateTime.Now.ToString();
            pflowControl.TimeSpanStart = new TimeSpan(DateTime.Now.Ticks);

            if (pflowControl.pThermocontroller != null) pflowControl.pThermocontroller.SetPointTemp(pflowControl.StartTemp);

            if (ReadyForTest())
            {
                pflowControl.MyLogManager.AdapterLogString(0, "***********************  SN=" + pflowControl.CurrentSN + " ************************");
                pflowControl.MyDataio.WriterSN(pflowControl.StuctTestplanPrameter.Id, pflowControl.CurrentSN, pflowControl.Current_DutFwRev, pflowControl.StrIpAddress, StrLightSourceMessage, StrReMark, out pflowControl.SNID);
                richInterfaceLog.Text = "";
                richInterfaceLog.Refresh();
                button_Test.BackColor = Color.Yellow;
                button_Test.Refresh();

               // TimeSpanUse.DateDiff(pflowControl.TimeSpanStart, pflowControl.MyLogManager, "Prepare Time for Test ", out pflowControl.TimeSpanStart);
                pflowControl.MyLogManager.AdapterLogString(0, "***********************  SN=" + pflowControl.CurrentSN + " ************************");
                
                pflowControl.TotalResultFlag = true;
               
                TestThread = new Thread(Test);
                TestThread.Start();
                TestThread.Priority = ThreadPriority.Highest;
                pflowControl.FlagFlowControllEnd = false;
                RefreshThread = new Thread(Refresh_GUI);
                RefreshThread.Start();
                RefreshThread.Priority = ThreadPriority.Lowest;
                button_Test.BackColor = Color.Yellow;
                button_Test.Refresh();
            }
            else//测试前的准备出错....
            {
                pflowControl.FlagFlowControllEnd = true;
                plogManager.AdapterLogString(3, "测试前的准备出错");
                labelShow.Text = "测试前的准备出错,请检查测试前的准备状况,比如EVB,I2C.....";

                labelShow.Refresh();

                labelShow.Refresh();

                textBoxSN.Text = "";
                textBoxSN.Refresh();
                button_Test.Enabled = true;
                comboBoxPN.Enabled = true;
                comboBoxPType.Enabled = true;
                comboBoxTestPlan.Enabled = true;

                progress.Value = 0;
                progress.Refresh();

                panelCombox.Enabled = true;

                if (pflowControl.pThermocontroller != null) pflowControl.pThermocontroller.SetPositionUPDown("0");

               // Thread.Sleep(2000);
            }

        }

        private bool JudegEvb()
        {

            pflowControl.pEnvironmentcontroll.Flag_NeedAutoCheck_EvBVoltage = pflowControl.Flag_NeedAutoCheck_EvBVoltage;

            bool flag = false;
            if (pflowControl.Flag_NeedAutoCheck_EvBVoltage)
            {
               // Flag_Auto_Check_EvBVoltage
                flag= pflowControl.pDut.CheckEvbVcADC_Coef_flag();
                plogManager.AdapterLogString(1, "测试板FlagCal=" + flag.ToString());
                if (!flag)//Evb 未曾Cal 系数
                {
                    if (DialogResult.Yes == MessageBox.Show("测试版未被校验,是否要取消自动校验测试班电压?", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                    {
                        chK_Auto_Evb_Voltage.Checked = false;
                        chK_Auto_Evb_Voltage.Refresh();
                        chK_Aotu_Evb_Voltage_CheckedChanged( null,  null);
                        buttonOffset.BackColor = Color.White;
                        FlagEquipmentOffSetOK = false;
                        panelOffset.Enabled = true;
                        MessageBox.Show("请回到Offset界面,重新填写补偿");
                    }
                    else
                    {
                      plogManager.AdapterLogString(3, "测试板未曾校验,测试取消....");
                    }

                    return false;
                }
               
                return true;
               
            }
            
            return true;
        }
        private bool JudgeFwVer()
        {
            pflowControl.flag_FwVerOK = false ;

           
            
            int i = 0;

                for (i = 0; i < 3; i++)
                {
                    pflowControl.Current_DutFwRev = pflowControl.pDut.ReadFWRev().ToString().ToUpper();
                    textBoxFW.Text = pflowControl.Current_DutFwRev;
                    textBoxFW.Refresh();
                    if (pflowControl.Current_DutFwRev != "0000" && pflowControl.Current_DutFwRev != "FFFF")
                    {
                        if (pflowControl.flag_NeedCheckFw)
                        {
                            if (pflowControl.Current_DutFwRev == pflowControl.StuctTestplanPrameter.FwVersion)
                            {
                                pflowControl.flag_FwVerOK = true;
                                break;
                            }
                        }
                        else
                        {
                            pflowControl.flag_FwVerOK = true;
                            break;
                        }
                      
                    }

                }

                if (!pflowControl.flag_FwVerOK)
                {
                    MessageBox.Show("FwVer  Error");
                    button_Test.Enabled = true;
                }
                textBoxFW.Text = pflowControl.Current_DutFwRev;
                textBoxFW.Refresh();
              
                return pflowControl.flag_FwVerOK;
        }
        private bool JudgeSerialNO()
        {

            pflowControl.flag_SNOK = false;

            for (int i = 0; i < 3; i++)
            {
                pflowControl.CurrentSN = pflowControl.pDut.ReadSn().Trim();
               // Thread.Sleep(200);
                textBoxSN.Text = pflowControl.CurrentSN;
                textBoxSN.Refresh();
              //  Thread.Sleep(200);

                string Str=pflowControl.CurrentSN.Substring(0,1);

                if (pflowControl.CurrentSN.Length >= 3)
                {
                    //if (pflowControl.CurrentSN.Substring(0,1)=="")
                    //{
                    //}
                    pflowControl.flag_SNOK = true;
                    break;
                }
            }

            textBoxSN.Text = pflowControl.CurrentSN;

            if (!pflowControl.flag_SNOK)
            {
                MessageBox.Show("SN Read Error");
                button_Test.Enabled = true;
            }
          
            textBoxSN.Refresh();

            return pflowControl.flag_SNOK;
        }
        private bool InitializeDut()
        {
           // IdPnName
               DataTable dd = new DataTable();

               string sTRtb = "GlobalManufactureChipsetInitialize";

               string Str = "Select* from " + sTRtb + " where PID=" + IdPnName + " order by id";
                
               dd = pflowControl.MyDataio.GetDataTable(Str, sTRtb);

               int[] SqlDriverItemValue = new int[dd.Rows.Count];
            
               for (int i=0;i<dd.Rows.Count;i++)
               {
                   byte DriverType,Chipline,RegistAddr,length;
                   int ItemValue;

                   DriverType = Convert.ToByte(dd.Rows[i]["DriverType"]);
                   Chipline = Convert.ToByte(dd.Rows[i]["Chipline"]);
                   RegistAddr = Convert.ToByte(dd.Rows[i]["RegistAddr"]);
                   length = Convert.ToByte(dd.Rows[i]["length"]);

                   ItemValue = Convert.ToInt16(dd.Rows[i]["ItemValue"]);
                   SqlDriverItemValue[i] = ItemValue;

                   byte[] WriteData = new byte[length];

                   for (int k = 0; k < length;k++ )
                   {

                       int WriteValue=ItemValue % 256;
                       WriteData[length-k-1] = (byte)WriteValue;
                       ItemValue = (ItemValue / 256);

                   }
                  


               }

               dataGridViewTotalData.DataSource = dd;
               dataGridViewTotalData.Refresh();
            return false;
        }

        private void Test()
        {
            if (!pflowControl.StartTest())
            {
                pflowControl.TotalResultFlag= false;
            }
             pflowControl.FlagFlowControllEnd = true;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            MessageBox.Show("感谢您的使用！");
            System.Environment.Exit(0);   

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("是否确认关闭?", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {

                if (TestThread != null)
                {
                    if (!pflowControl.FlagFlowControllEnd)
                    {
                       // MessageBox.Show("测试还没结束，关你妹啊！");
                        MessageBox.Show("好吧，那我关了。。。。");
                        plogManager.AdapterLogString(1, "You Have Illicit close the ATS EXE");
                    }
                    TestThread.Abort();
                }
                if (RefreshThread != null) RefreshThread.Abort();
                e.Cancel = false;
            }
            else e.Cancel = true;
        }
        private bool GetTestplanInf()
        {
            DataTable dt = new DataTable();

            string Str = "Select* from TopoTestPlan where ID=" + pflowControl.StuctTestplanPrameter.Id;

            dt = pflowControl.MyDataio.GetDataTable(Str, "TopoTestPlan");

     
            pflowControl.StuctTestplanPrameter.FwVersion = dt.Rows[0]["SWVersion"].ToString();
            pflowControl.StuctTestplanPrameter.DutUsbPort = Convert.ToByte(dt.Rows[0]["USBPort"]);
            pflowControl.StuctTestplanPrameter.Flag_NeedInitializeDrvier = Convert.ToBoolean(dt.Rows[0]["IsChipInitialize"]);
            pflowControl.StuctTestplanPrameter.Flag_Need_EEPROMInitialize = Convert.ToBoolean(dt.Rows[0]["IsEEPROMInitialize"]);
            pflowControl.StuctTestplanPrameter.flag_NeedCheckSn = Convert.ToBoolean(dt.Rows[0]["SNCheck"]);
            pflowControl.StuctTestplanPrameter.Flag_IgnoreRecordCoef = Convert.ToBoolean(dt.Rows[0]["IgnoreBackupCoef"]);
            return true;
        }
        private void button_Config_Click(object sender, EventArgs e)
        {
            pflowControl.pPS = null;

             GetTestplanInf();

            pflowControl.dtPnSpec = GetSpec();
           
            plogManager.AdapterLogString(1, "Config Equipment-------------------");

            FlagEquipmentConfigOK = false;
            FlagEquipmentOffSetOK = false;
            buttonOffset.BackColor = Color.Snow;

#region 创建存放眼图的文件夹位置

            string StrTime = DateTime.Now.ToString("yyyy-MM-dd");
            string StrPathOptEyeDiagram = Application.StartupPath + @"\EyeDiagram\" + comboBoxPType.Text.ToUpper() + "\\" + comboBoxPN.Text.ToUpper() + "\\" + comboBoxTestPlan.Text.ToUpper() + "\\" + StrTime + "\\OptEyeDiagram\\";
            string StrPathElecEyeDiagram = Application.StartupPath + @"\EyeDiagram\" + comboBoxPType.Text.ToUpper() + "\\" + comboBoxPN.Text.ToUpper() + "\\" + comboBoxTestPlan.Text.ToUpper() + "\\" + StrTime + "\\ElecEyeDiagram\\";


            if (!Directory.Exists(StrPathOptEyeDiagram))
            {
                Directory.CreateDirectory(StrPathOptEyeDiagram);
            }
           // pflowControl.StrEyeDiagramPath = StrPathEyeDiagram;
            pflowControl.StrOptEyeDiagramPath = StrPathOptEyeDiagram + "\\";

            if (!Directory.Exists(StrPathElecEyeDiagram))
            {
                Directory.CreateDirectory(StrPathElecEyeDiagram);
            }
            // pflowControl.StrEyeDiagramPath = StrPathEyeDiagram;
            pflowControl.StrElecEyeDiagramPath = StrPathElecEyeDiagram + "\\";



#endregion

#region Disable Control

            panelCombox.Enabled = false;
            panelOffset.Enabled = false;

            button_Config.Enabled = false;
            button_Config.BackColor = Color.Yellow;

            button_Test.Enabled = false;
            btnExport.Enabled = false;
            buttonStop.Enabled = false;

            progress.Value = 0;
            progress.Refresh();
            labelProgress.Text = "0";
            labelProgress.Refresh();

#endregion
           
            try
            {
                if (comboBoxTestPlan.Text != "")
                {
                    labelShow.Text = "Equipment Configing.......";
                    labelShow.Refresh();
               
                    aEquipList = new EquipmentList();
                  
                    pflowControl.CurrentSN = textBoxSN.Text;

                    GetEquipmenInf();



                    Thread Config = new Thread(ConfigEquipment);
                    Config.Start();


                    tabControl1.SelectedIndex = 4;
                }
                else
                {
                    MessageBox.Show("Select ProductionName");
                }
            }
            catch (Exception Me)
            {
                MessageBox.Show("ConfigEquipment Error ,PLS Check Equipment");
                FlagEquipmentConfigOK = false;
            }
        }
        private DataTable GetSpec()
        {
            DataTable dt=new DataTable();
            try
            {
                string Str = "Select* from TopoPNSpecsParams left join GlobalSpecs on TopoPNSpecsParams.SID=GlobalSpecs.ID  where TopoPNSpecsParams.PID=" + pflowControl.StuctTestplanPrameter.Id + "order by TopoPNSpecsParams.id";
                string sTRtb = "TopoPNSpecsParams";
                 dt = pflowControl.MyDataio.GetDataTable(Str, sTRtb);
                //dataGridViewTotalData.DataSource = dt;

                 dt.Columns.Add("FullName");

                 for (int i = 0; i < dt.Rows.Count; i++)
                 {
                     dt.Rows[i]["FullName"] = dt.Rows[i]["ItemName"].ToString() + "(" + dt.Rows[i]["Unit"].ToString() + ")";
                 }
               
            }
            catch
            {

            }
           // dataGridViewTotalData.Refresh();

            return dt;
        }
        private bool GetEquipmenInf()
        {
           // DataTable dt = new DataTable();
            try
            {
                String StrSelect = "SELECT GlobalAllEquipmentList.ItemType AS Type,GlobalAllEquipmentList.ItemName AS Name,TopoEquipment.Role AS Role,TopoEquipment.ID AS ID, GlobalAllEquipmentParamterList.ItemName AS ItemName,TopoEquipmentParameter.ItemValue AS ItemValue" +
                " FROM GlobalAllEquipmentList,TopoEquipment ,TopoEquipmentParameter,GlobalAllEquipmentParamterList  where " +
                " (TopoEquipment.GID=GlobalAllEquipmentList.ID and TopoEquipment.ID=TopoEquipmentParameter.PID and TopoEquipmentParameter.GID=GlobalAllEquipmentParamterList.ID)and TopoEquipment.PID=" + pflowControl.StuctTestplanPrameter.Id;
                pflowControl.dtEquipmentInf = pflowControl.MyDataio.GetDataTable(StrSelect, "TopoEquipment");
                if (pflowControl.dtEquipmentInf!=null)
                {
                    pflowControl.dtEquipmentInf.Columns.Add("FullName");

                    for (int i = 0; i < pflowControl.dtEquipmentInf.Rows.Count;i++ )
                    {
                        String Str_Role="_NA";
                        switch (pflowControl.dtEquipmentInf.Rows[i]["Role"].ToString().ToUpper())// 0=NA,1=TX,2=RX
                        {
                            case "2":
                               Str_Role= "_RX";
                                break;
                            case "1":
                                Str_Role = "_TX";
                                break;
                            default:
                                Str_Role = "_NA";
                                break;
                        }
                        pflowControl.dtEquipmentInf.Rows[i]["FullName"] = pflowControl.dtEquipmentInf.Rows[i]["Name"].ToString().ToUpper() + Str_Role + "_" + pflowControl.dtEquipmentInf.Rows[i]["Type"].ToString().ToUpper();
                    }
                    dtEquipmenList = pflowControl.dtEquipmentInf.Clone();
                    DataView dv=pflowControl.dtEquipmentInf.DefaultView;
                   DataTable dt = dv.ToTable(true, "FullName");
                   string[] num = dt.AsEnumerable().Select(d => d.Field<string>("FullName")).ToArray();
                    //  string s=dt.Rows[0]["FullName"].ToString();
                    //EquipmenNameArray= new string[dt.Rows.Count,2];
                   for (int i = 0; i < num.Length; i++)
                   {
                       DataRow[] drarray = pflowControl.dtEquipmentInf.Select("FullName='" + num[i].ToString()+"'");

                      DataRow dr = dtEquipmenList.NewRow();
                     // dr = drarray[0];
                      for (int j = 0; j < dtEquipmenList.Columns.Count;j++ )
                      {
                          dr[j] = drarray[0][j];
                      }
                      dtEquipmenList.Rows.Add(dr);
                   }
                }
                return true;
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
           
        }

        private void FormATS_Load(object sender, EventArgs e)
        {
           // AnimateWindow(this.Handle, 1000, AW_CENTER);
            this.labelVer.Text = "Version:" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString(); 
            labelVer.Refresh();

            this.labelbuildTime.Text = "BuildTime:" + System.IO.File.GetLastWriteTime(this.GetType().Assembly.Location).ToString("yyyy/MM/dd HH:mm:ss");

            var ss1 = ReadProductionTpye();

            comboBoxPType.Items.Clear();

            foreach (string s in ss1)
            {
                comboBoxPType.Items.Add(s);
            }

            #region 记录窗体信息

            //在Form_Load里面添加:
            this.Resize += new EventHandler(Form1_Resize);
            X = this.Width;
            Y = this.Height;
            setTag(this);
            Form1_Resize(new object(), new EventArgs());//x,y可在实例化时赋值,最后这句是新加的，在MDI时有用

#endregion
            string hostname = System.Net.Dns.GetHostName(); //主机
            System.Net.IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(hostname);//网卡IP地址集合
            //string StrIP1 = ipEntry.AddressList[0].ToString();//取一个IP
            //string StrIP2 = ipEntry.AddressList[ipEntry.AddressList.Length - 1].ToString();//取一个IP
              // string IP4 = "";
                for (int i = 0; i < ipEntry.AddressList.Length; i++)
                {
                    if (ipEntry.AddressList[i].AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        StrIP = ipEntry.AddressList[i].ToString();
                       // IP4
                        break;
                    }
                }
         
           
            if (StrIP=="")
            {
                MessageBox.Show("I Con't Get Ip Address");
                comboBoxPType.Text = "";
                comboBoxPType.Enabled = false;
            }
            pflowControl.StrIpAddress = StrIP;
            this.Text = "ATS_" + MyXml.DbName;


          string[] ProductionTypeArray=  ReadProductionTpye();

 
        }

        private void buttonOffset_Click(object sender, EventArgs e)
        {
            if (FlagEquipmentConfigOK)
            {
                try
                {


                    //----------------Scope
                    ScopeOffsetArray.Clear();
                    ScopeOffsetArray.Add(ScopeOffset1.Text);
                    ScopeOffsetArray.Add(ScopeOffset2.Text);
                    ScopeOffsetArray.Add(ScopeOffset3.Text);
                    ScopeOffsetArray.Add(ScopeOffset4.Text);

                    string StrScopeOffsetArray = ScopeOffsetArray[0].ToString();

                    for (int i = 1; i < 4; i++)
                    {
                        StrScopeOffsetArray += "," + ScopeOffsetArray[i].ToString();
                    }
                    MyXml.ScopeOffset = StrScopeOffsetArray;
                    //---------------------AttOffset
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
                    MyXml.AttennuatorOffset = StrAttOffsetArray;
                    //-----------------------------LightSource
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
                    MyXml.LightSourceEr = StrErArray;
                    pflowControl.StrLightSourceErArray = StrErArray;


                    //---------------------IccOffset & PsOffset           
                
                     double Vccoffset = Convert.ToDouble(VccOffset.Text);
                   // pflowControl.Iccoffset = Convert.ToDouble(IccOffsetArray[0]);

                     MyXml.VccOffset = Vccoffset.ToString();
                    //MyXml.IccOffset = IccOffsetArray[0].ToString();
                    //------------------------Config Equipment
                    string Str= IccOffset.Text.Trim();
                    if (Str == "" || Str==null) IccOffset.Text = "0";
                     pflowControl.pGlobalParameters.StrEvbCurrent = IccOffset.Text.Trim();
                     MyXml.IccOffset = IccOffset.Text.Trim();
                    for (int i = 0; i < pflowControl.MyEquipList.Count; i++)
                    {

                        if (pflowControl.MyEquipList.Keys[i].Contains("ATT"))
                        {

                            pflowControl.MyEquipList.Values[i].offsetlist.Clear();

                            for (byte j = 1; j < AttOffsetArray.Count + 1; j++)
                            {
                                pflowControl.MyEquipList.Values[i].configoffset(j.ToString(), AttOffsetArray[j - 1].ToString().Trim());
                            }

                        }
                        if (pflowControl.MyEquipList.Keys[i].Contains("SCOPE"))
                        {
                            pflowControl.MyEquipList.Values[i].offsetlist.Clear();
                            for (byte j = 1; j < ScopeOffsetArray.Count + 1; j++)
                            {
                                pflowControl.MyEquipList.Values[i].configoffset(j.ToString(), ScopeOffsetArray[j - 1].ToString());
                            }

                        }
                        if (pflowControl.MyEquipList.Keys[i].Contains("POWERSUPPLY"))// 添加了对Vccoffset 补偿
                        {
                            pflowControl.MyEquipList.Values[i].configoffset("1", Vccoffset.ToString());
                        }
                       
                    }
                    FlagEquipmentOffSetOK = true;
                    buttonOffset.BackColor = Color.Green;
                    button_Test.Enabled = true;
                }
                catch
                {
                    FlagEquipmentOffSetOK = false;
                    buttonOffset.BackColor = Color.Red;
                    MessageBox.Show("PLS Config Equipment First");
                }
            }
            else
            {
                MessageBox.Show("PLS Config Equipment First");
            }
        
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
        
           Export ExcelExport = new Export();

           if (IdTestplan != -1)
           {
               ExcelExport.TestPlanID = pflowControl.StuctTestplanPrameter.Id;
               ExcelExport.pflowControl = pflowControl;
               ExcelExport.ShowDialog();
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
            if (dt.Rows.Count > Dgv.Rows.Count)
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

                        for (int i = Dgv.Rows.Count; i < dt.Rows.Count; i++)
                        {
                            string[] TestData = new string[dt.Columns.Count];

                            for (int j = 0; j < dt.Columns.Count; j++)
                            {
                                TestData[j] = dt.Rows[i][j].ToString();
                            }

                            Dgv.Rows.Add(TestData);
                        }

                   
                    }
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
                    r1.ShowReslut(Rflag, null, flag_ShowErrorData );
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
            int i = 0;
            UpdataResult refreshResultLabel = new UpdataResult(RefreshResult);
            UpdataResultShow FormShowTestResult = new UpdataResultShow(TestResultFormShow);
            string Strlabelshow = "";
            string Strichinterface = "";
            while (!pflowControl.FlagFlowControllEnd)
            {
              
                i++;
                while (pflowControl.QueueShow.Count > 0)
                {
                    Strlabelshow = pflowControl.QueueShow.Dequeue() + "\r\n";
                }
                UpDataStatus(this.labelShow, Strlabelshow);
                Strichinterface = "";
                while (pflowControl.QueueInterfaceLog.Count > 0)
                {
                    Strichinterface += pflowControl.QueueInterfaceLog.Dequeue() + "\r\n";
                }
                UpdataShowLog(this.richInterfaceLog, Strichinterface);
                UpdataTestData(dataGridViewTotalData, pflowControl.TotalTestData);
                UpdataTestData(dataGridViewLPTotalData, pflowControl.dtLPTotalTestData);
                UpDataProcess(progress, pflowControl.prosess);
                UpDataStatus(labelProgress, pflowControl.prosess.ToString());
            }
            if (pflowControl.FlagFlowControllEnd)
            {
                while (pflowControl.QueueShow.Count > 0)
                {
                    Strlabelshow = pflowControl.QueueShow.Dequeue() + "\r\n";
                }
                Strlabelshow = "Test End..........";
                UpDataStatus(this.labelShow, Strlabelshow);
                while (pflowControl.QueueInterfaceLog.Count > 0)
                {
                    Strichinterface += pflowControl.QueueInterfaceLog.Dequeue() + "\r\n";
                }

                UpdataShowLog(this.richInterfaceLog, Strichinterface);
               // Thread.Sleep(200);

                ButtonUpdata(button_Test, true, Color.WhiteSmoke);
                ButtonUpdata(button_Config, true, Color.Green);
                ButtonUpdata(btnExport, true, Color.WhiteSmoke);
                ButtonUpdata(buttonStop, true, Color.WhiteSmoke);

                PanelUpdata(this.panelCombox, true);
                PanelUpdata(this.panelOffset, true);
                //ComboxUpdata(comboBoxPN, true);
                //ComboxUpdata(comboBoxPType, true);
                //ComboxUpdata(comboBoxTestPlan, true);

                //this.Invoke(refreshResultLabel, pflowControl.TotalResultFlag);
                this.Invoke(FormShowTestResult, pflowControl.TotalResultFlag, GetDtErrorItem(), flag_ShowErrorData);
                TestThread.Abort();
                RefreshThread.Abort();


            }

        }
        private DataTable GetDtErrorItem()
        {
            DataTable dt = new DataTable();
            try
            {
                String Str = "SELECT TopoLogRecord.TestLog as ErrorItem,Temp,Voltage,Channel FROM  TopoLogRecord jOIN TopoRunRecordTable ON TopoRunRecordTable.ID=TopoLogRecord.RunRecordID   where  TopoLogRecord.CtrlType=2 and TopoLogRecord.Result='False' and TopoRunRecordTable.ID=" + pflowControl.SNID;
                dt = pflowControl.MyDataio.GetDataTable(Str, "TopoLogRecord");
            }
            catch (System.Exception ex)
            {

            }

            return dt;
        }
        public void ShowResult()
        {
            labelShow.Text = "Test End---------";
        }
       
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
#region   Config Equipment thread
      
        // Refresh  Config Equipment
        public delegate void SetCorlor(Color c1);
        private void setCorlor(Color c2)
        {
            button_Config.BackColor = c2;
            button_Config.Refresh();
        }
        private void ConfigEquipment()
        {

            string CurrentEquipmentName = "";
            FlagEquipmentConfigOK = true;

            UpdataRichBox refreshRichBox = new UpdataRichBox(UpdataShowLog);
            // UpdataDataTable refreshDataTable = new UpdataDataTable(RefreshDataGridView);

            DutStruct[] MyDutManufactureCoefficientsStructArray;
            DriverStruct[] MyManufactureChipsetStructArray;
            DriverInitializeStruct[] MyDutManufactureChipSetInitialize;
            DutEEPROMInitializeStuct[] MyDutEEPROMInitializeStuct;
            int i = 0;
            try
            {

                pflowControl.pDut = (DUT)MyEquipmentManage.Createobject(pflowControl.ProductionTypeName.ToUpper() + "DUT");
                pflowControl.pDut.deviceIndex = pflowControl.StuctTestplanPrameter.DutUsbPort;
                pflowControl.pDut.ChipsetControll = pflowControl.StuctPnPrameter.OldDriver;
                MyDutManufactureCoefficientsStructArray = pflowControl.GetDutManufactureCoefficients();
                MyManufactureChipsetStructArray = pflowControl.GetManufactureChipsetControl();
                MyDutManufactureChipSetInitialize = pflowControl.GetManufactureChipsetInitialize(); //等待数据库结构统一
                MyDutEEPROMInitializeStuct = pflowControl.Get_EEPROM_Init_FromSql();
                pflowControl.pDut.Initialize(MyDutManufactureCoefficientsStructArray, MyManufactureChipsetStructArray, MyDutManufactureChipSetInitialize,MyDutEEPROMInitializeStuct, pflowControl.StrAuxAttribles);//等待Driver 跟上
                //pflowControl.pDut.Initialize(MyDutManufactureCoefficientsStructArray, MyManufactureChipsetStructArray, pflowControl.StrAuxAttribles);
                MyEquipmentArrayy.Clear();
                Algorithm algorithm = new Algorithm();
                for (i = 0; i < dtEquipmenList.Rows.Count; i++)
                {
                    TestModeEquipmentParameters[] CurrentEquipmentStruct = pflowControl.GetCurrentEquipmentInf(dtEquipmenList.Rows[i]["id"].ToString());
                   // string[] StrArray = EquipmenNameArray[i, 0].Split('_');
                    CurrentEquipmentName = dtEquipmenList.Rows[i]["Name"].ToString().ToUpper();
                    string CurrentEquipmentFullName = dtEquipmenList.Rows[i]["FullName"].ToString();
                    EquipmentBase CurrentEquipmentObject = (EquipmentBase)MyEquipmentManage.Createobject(CurrentEquipmentName);
                    CurrentEquipmentObject.Role = Convert.ToByte(dtEquipmenList.Rows[i]["Role"]);// 0=NA,1=TX,2=RX

                    UpDataStatus(labelShow, CurrentEquipmentName + "  Configing.....");

                    if (!CurrentEquipmentObject.Initialize(CurrentEquipmentStruct) || !CurrentEquipmentObject.Configure(1))
                    {
                        MessageBox.Show(CurrentEquipmentName + "Configure Error");
                        FlagEquipmentConfigOK = false;

                        Exception ex = new Exception(CurrentEquipmentFullName + "Configure Error");
                        throw ex;
                    }
                    else
                    {
                        MyEquipmentArrayy.Add(CurrentEquipmentFullName, CurrentEquipmentObject);

                    }
                    if (CurrentEquipmentFullName.Contains("POWER"))
                    {
                        pflowControl.pPS = (Powersupply)CurrentEquipmentObject;
                    }
                    if (CurrentEquipmentFullName.Contains("THERMOCONTROLLER"))
                    {
                        pflowControl.pThermocontroller = (Thermocontroller)CurrentEquipmentObject;
                    }
                   // pThermocontroller
                    CurrentEquipmentObject.OutPutSwitch(false);
                    int ProcessValue = (int)(i + 1) * 100 / (dtEquipmenList.Rows.Count);
                    UpDataProcess(progress, ProcessValue);
                    UpDataStatus(labelProgress, ProcessValue.ToString());
                    UpDataStatus(labelShow, CurrentEquipmentName+"  ConfigOK");
                    plogManager.AdapterLogString(1, CurrentEquipmentName + "  ConfigOK");
                }
                
                pflowControl.pEnvironmentcontroll = new EnvironmentalControll(pflowControl.pDut,pflowControl.MyLogManager);
               
                pflowControl.MyEquipList = MyEquipmentArrayy.MyEquipList;

                

                if (pflowControl.pPS == null)
                {
                    MessageBox.Show("缺少电源，请确认Testplan中是否准备了电源！");
                    FlagEquipmentConfigOK = false;
                }
                
            }
            catch (Exception EX)
            {
                plogManager.AdapterLogString(1, CurrentEquipmentName + "  Config Error");

                MessageBox.Show(EX.Message,"ConfigEquipment Error ,PLS Check Equipment");
                UpDataStatus(this.labelShow, "Equipment Config false");
                FlagEquipmentConfigOK = false;
            }
            finally
            {
                PanelUpdata(this.panelCombox, true);
                PanelUpdata(this.panelOffset, true);
               // UpdataPanel(this.panelCombox, true);
                if (FlagEquipmentConfigOK)
                {
                    MessageBox.Show("ConfigEquipment OK");
                    UpDataStatus(this.labelShow, "Equipment Config OK");
                    ButtonUpdata(this.button_Config, true, Color.Green);
                }
                else
                {
                    ButtonUpdata(button_Config, true, Color.Red);
                    UpDataStatus(this.labelShow, "Equipment Config false");
                    ButtonUpdata(this.button_Config, true, Color.Red);
                }
                ButtonUpdata(this.button_Test, true, Color.WhiteSmoke);
                ButtonUpdata(this.btnExport, true, Color.WhiteSmoke);
                ButtonUpdata(this.buttonStop, true, Color.WhiteSmoke);
            }
        }
 #endregion
        private void buttonStop_Click(object sender, EventArgs e)    
        {
         
            if (MessageBox.Show("Do you want to Stop Test?", " Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                pflowControl.StopFlag = true;
                plogManager.AdapterLogString(1, "You Have Stop the ATS EXE");
                buttonStop.Enabled = false;   
            }
           
        }

        private void chK_Fw_CheckedChanged(object sender, EventArgs e)
        {
            if (chK_Fw.Checked)
            {
                pflowControl.flag_NeedCheckFw = true;
            }
            else
            {
                pflowControl.flag_NeedCheckFw = false;
            }
        }

        private void chk_AbNormal_Lab_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_AbNormal_Lab.Checked)
            {
                pflowControl.flag_Normal_Lab = false;
               
                Text_Remark.Visible = true;
                
            }
            else
            {
                pflowControl.flag_Normal_Lab = true;
                Text_Remark.Visible = false;
            }

            Text_Remark.Text = ""; 
            Text_Remark.Refresh();
        }

        private void chK_Aotu_Evb_Voltage_CheckedChanged(object sender, EventArgs e)
        {
            if (chK_Auto_Evb_Voltage.Checked)
            {
                pflowControl.Flag_NeedAutoCheck_EvBVoltage = true;
            }
            else
            {
                pflowControl.Flag_NeedAutoCheck_EvBVoltage = false;
            }
        }

        private void comboBoxPN_SelectionChangeCommitted(object sender, EventArgs e)
        {
           
        }


        #region 获取产品信息

        public string[] ReadProductionTpye()
        {
          
            try
            {
                if (pflowControl.MyDataio.OpenDatabase(true))
                {
                    string StrTableName = "GlobalProductionType";
                    string Selectconditions = "select * from " + StrTableName + " Where IgnoreFlag='false' order by ID";
                    dtProductionType.Clear();
                    dtProductionType = pflowControl.MyDataio.GetDataTable(Selectconditions, StrTableName); ;
                    string[] arry = dtProductionType.AsEnumerable().Select(d => d.Field<string>("ItemName")).ToArray();
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
     
        #endregion

        private void comboBoxPType_SelectionChangeCommitted(object sender, EventArgs e)
        {
          

        }

        private void comboBoxTestPlan_SelectionChangeCommitted(object sender, EventArgs e)
        {
           
        }

        private void comboBoxPType_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxPN.Items.Clear();
            comboBoxTestPlan.Items.Clear();
            #region     Get the id of Current ProductType

            pflowControl.ProductionTypeName = comboBoxPType.SelectedItem.ToString();
            DataTable dd = new DataTable();
            //  string[] arry = dtProductionType.AsEnumerable().Select(d => d.Field<string>("ItemName")).ToArray();
            DataRow[] dr = dtProductionType.Select("ItemName='" + pflowControl.ProductionTypeName + "'");
            IdPnType = Convert.ToInt32(dr[0]["ID"].ToString());

            #endregion

            #region  Fit ProductionName Combox

            string Str = "Select* from GlobalProductionName where PID=" + IdPnType + " and IgnoreFlag='false' order by id";
            string sTRtb = "GlobalProductionName";
            dd = pflowControl.MyDataio.GetDataTable(Str, sTRtb);
            comboBoxPN.Items.Clear();
            comboBoxPN.Text = "";
            comboBoxPN.Refresh();
            for (int i = 0; i < dd.Rows.Count; i++)
            {
                comboBoxPN.Items.Add(dd.Rows[i]["PN"].ToString());
            }

            comboBoxTestPlan.Items.Clear();
            #endregion
        }

        private void comboBoxPN_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxTestPlan.Items.Clear();

            if (comboBoxPType.Text != null)
            {
                #region    获取当前Pn 以及其对应的ID号
                pflowControl.StrProductionName = comboBoxPN.SelectedItem.ToString();
                DataTable dd = new DataTable();
                string Str = "Select* from GlobalProductionName where GlobalProductionName.pid=" + IdPnType + " and GlobalProductionName.IgnoreFlag='false' and GlobalProductionName.PN='" + pflowControl.StrProductionName + "' order by GlobalProductionName.id";
                dtProductionName = pflowControl.MyDataio.GetDataTable(Str, "GlobalProductionName");
                DataRow[] dr = dtProductionName.Select("PN='" + pflowControl.StrProductionName + "'");
                IdPnName = Convert.ToInt32(dr[0]["ID"].ToString());
                pflowControl.ProductionNameId = IdPnName;
                pflowControl.StuctPnPrameter.ItemName = dr[0]["ItemName"].ToString();
                pflowControl.StuctPnPrameter.MCoefsID = Convert.ToInt16(dr[0]["MCoefsID"]);
                pflowControl.StuctPnPrameter.OldDriver = Convert.ToBoolean(dr[0]["OldDriver"]);

                pflowControl.pGlobalParameters.ImodFormula = dr[0]["IModFormula"].ToString();
                pflowControl.pGlobalParameters.IbiasFormula = dr[0]["IbiasFormula"].ToString();

                pflowControl.pGlobalParameters.APCType = Convert.ToByte(dr[0]["APC_Type"]);
                pflowControl.pGlobalParameters.BER_exp = Convert.ToByte(dr[0]["BER"]);
                pflowControl.pGlobalParameters.coupleType = Convert.ToByte(dr[0]["Couple_Type"]);
                pflowControl.pGlobalParameters.TotalChCount = Convert.ToByte(dr[0]["ChannelS"]);
                pflowControl.pGlobalParameters.TotalTempCount = Convert.ToByte(dr[0]["Tsensors"]);
                pflowControl.pGlobalParameters.TotalVccCount = Convert.ToByte(dr[0]["Voltages"]);
                pflowControl.pGlobalParameters.TecPresent = Convert.ToByte(dr[0]["TEC_Present"]);
                #region UsingCelsiusTemp 由于数据库更改，我们对其取反
                pflowControl.pGlobalParameters.UsingCelsiusTemp = Convert.ToBoolean(dr[0]["UsingCelsiusTemp"]);
              //  pflowControl.pGlobalParameters.UsingCelsiusTemp=!pflowControl.pGlobalParameters.UsingCelsiusTemp;
                #endregion
              
                pflowControl.pGlobalParameters.OverLoadPoint = Convert.ToSingle(dr[0]["RxOverLoaddBm"]);
                //pflowControl.pGlobalParameters.

                #endregion



                Str = "Select* from TopoTestPlan where IgnoreFlag='false' and PID=" + IdPnName + " order by id";
                string sTRtb = "TopoTestPlan";
                dd = pflowControl.MyDataio.GetDataTable(Str, "GlobalProductionName");
                comboBoxTestPlan.Items.Clear();

                for (int i = 0; i < dd.Rows.Count; i++)
                {
                    comboBoxTestPlan.Items.Add(dd.Rows[i]["ItemName"].ToString());
                }
            }
        }

        private void comboBoxTestPlan_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            //comboBoxTestPlan.Items.Clear();

            if (comboBoxPN.Text != "")
            {

                #region    Get the id of Current Testplan

                String StrTestplanName = comboBoxTestPlan.SelectedItem.ToString();

                string Str = "Select* from TopoTestPlan where TopoTestPlan.pid=" + IdPnName + " and TopoTestPlan.IgnoreFlag='false' and TopoTestPlan.ItemName='" + StrTestplanName + "' order by TopoTestPlan.id";
               
                dt = pflowControl.MyDataio.GetDataTable(Str, "GlobalProductionName");
                pflowControl.StuctTestplanPrameter.Id = Convert.ToInt32(dt.Rows[0]["id"]);

               

                FlagEquipmentConfigOK = false;

                button_Config.Enabled = true;
                btnExport.Enabled = true;

                button_Test.Enabled = false;
                buttonStop.Enabled = false;

                #endregion

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            C86122 a = new C86122(plogManager);
            a.IOType = "GPIB";
            a.Addr = 3;
            a.WavelengthMax = 1320;
            a.WavelengthMin = 1270;
            a.Connect();
            a.Configure();
        //  double K=  a.GetWavelength();
          textBoxSN.Text = a.GetWavelength().ToString();
        }
        private void button1_Click_1(object sender, EventArgs e)
        {

            if (chk_AbNormal_Lab.CheckState==CheckState.Checked )
            {
                if (Text_Remark.Text.Trim() == "")
                {
                    StrReMark = "非法测试且未填写原因";
                }
                else
                {
                    StrReMark = Text_Remark.Text.Trim();
                }

                StrReMark = Text_Remark.Text.Trim();
            }
            else
            {
                StrReMark = "正常";
            }


        }

        private void checkBox_ShowErrorData_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_ShowErrorData.Checked)
            {
                flag_ShowErrorData = true;
            }
            else
            {
                flag_ShowErrorData = false;
            }
        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

      
    }
    
    public class hp// 记录原始窗体的尺寸
    {
        public Size s { set; get; }
        public Point p { set; get; }
    }
}

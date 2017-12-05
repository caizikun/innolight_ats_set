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

        private Thread TestThread;
        private Thread RefreshThread;
        private string TestStartTime;//为每个一颗产品开始测试时间,后面存放本地Log.TXT 的序号之后

        //private Powersupply_Form Form_PS_TX;
        //private Scope_Form Form_Scope;
        //private PPG_Form Form_PPG;
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
        private int IdTestplan;
        private int IdProductionName;
        private int IdProductionType;
        private bool FlagEquipmentConfigOK = false;
        private bool FlagEquipmentOffSetOK = false;
        private bool FlagDrvierInitializeOK = false;
        private int RefreashTime = 0;
        private EquipmentArray MyEquipmentArrayy = new EquipmentArray();
        private string[,] EquipmenNameArray;
        private string StrIP = "";

        private string StrLightSourceMessage = "";
        private  string StrReMark = "";
        private bool flag_ShowErrorData = true;
        private byte Dut_Usb_Addr = 0;
       // private String StrPathEyeDiagram;
#region  委托声明

        public delegate void UpdataDataTable(DataGridView Dgv, DataTable dt);
        public delegate void UpdataLable(Label L1, string label);
        public delegate void UpdataRichBox(RichTextBox R1, string label);
        public delegate void UpdataProcessbar(ProgressBar p1, int process);
        public delegate void UpdataResult(bool ResultFlag);
        public delegate void UpdataResultShow(bool ResultFlag,DataTable dt,bool ShowErrorData);
        public delegate void UpdataButton(Button B1, bool flag, Color c1);
        public delegate void UpdataPanel(Panel B1, bool flag);
        public delegate void UpdataCombox(ComboBox B1, bool flag);
#endregion
        public FormATS()
        {
           // Control.CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
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
            plogManager.AdapterLogString(0, "chK_Aotu_Evb_Voltage Funtion=" + chK_Aotu_Evb_Voltage.CheckState);


            pflowControl.prosess = 0;
            FlagDrvierInitializeOK = true;
            if (FlagEquipmentConfigOK&&FlagEquipmentOffSetOK)
            {
                dataGridViewTotalData.Rows.Clear();
                Thread.Sleep(1000);
                dataGridViewTotalData.Refresh();

  #region  Disable Control

                button_Config.Enabled = false;
                btnExport.Enabled = false;
                panelCombox.Enabled = false;
                panelOffset.Enabled = false;
 #endregion

                richInterfaceLog.Focus();


                button_Test.Enabled = false;
                #region  开启电源
                for (int i = 0; i < pflowControl.MyEquipList.Count; i++)
                {

                    // TestModeEquipmentParameters[] CurrentEquipmentStruct = GetCurrentEquipmentInf(EquipmenNameArray.Values[i].ToString());
                    string[] List = pflowControl.MyEquipList.Keys[i].ToString().Split('_');
                    string CurrentEquipmentName = List[0];
                    // string CurrentEquipmentID = List[1];
                    string CurrentEquipmentType = List[2];

                    if (pflowControl.MyEquipList.Keys[i].Contains("POWERSUPPLY"))
                    {
                        pflowControl.MyEquipList[pflowControl.MyEquipList.Keys[i].ToString()].Switch(true);
                       // aPS = (Powersupply)pflowControl.MyEquipList[pflowControl.MyEquipList.Keys[i].ToString()];
                        Thread.Sleep(200);
                        break;
                    }
                    
                }
                #endregion

                pflowControl.pDut.FullFunctionEnable();

                if (pflowControl.TotalTestData != null)
                {
                    pflowControl.TotalTestData.Clear();
                }

                TestStartTime = pflowControl.MyDataio.GetCurrTime().ToString("yyyy/MM/dd HH:mm:ss");
                textBoxSN.Text = "";

                labelShow.Text = "Prepare Read SN....";
                labelShow.Refresh();
                try
                {
                 
                   if(!JudegEvb())return false;
                   if(!JudgeSerialNO())return false;//判定序列号是否正常
                   if(!JudgeFwVer()) return false; ;//判定Fw版本号是否正常
                 
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
            if (chk_AbNormal_Lab.CheckState.ToString().ToUpper()=="TRUE")
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

         //  Text_Remark.Text.Trim()
            StrLightSourceMessage = "AP(dbm)=" + MyXml.AttennuatorOffset + ";ER=" + MyXml.LightSourceEr;



            plogManager.FlushLogBuffer();
            if (ReadyForTest())
            {
                pflowControl.MyLogManager.AdapterLogString(0, "***********************  SN=" + pflowControl.CurrentSN + " ************************");
                pflowControl.MyDataio.WriterSN(pflowControl.TestPlanId, pflowControl.CurrentSN, pflowControl.Current_DutFwRev, pflowControl.StrIpAddress, StrLightSourceMessage, StrReMark, out pflowControl.SNID);
                richInterfaceLog.Text = "";
                richInterfaceLog.Refresh();
                button_Test.BackColor = Color.Yellow;
                button_Test.Refresh();

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
                Thread.Sleep(2000);
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
                        chK_Aotu_Evb_Voltage.Checked = false;
                        chK_Aotu_Evb_Voltage.Refresh();
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
                            if (pflowControl.Current_DutFwRev == pflowControl.StrSqlSwVersion)
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
                Thread.Sleep(200);
                return pflowControl.flag_FwVerOK;
        }
        private bool JudgeSerialNO()
        {

            pflowControl.flag_SNOK = false;

            for (int i = 0; i < 3; i++)
            {
                pflowControl.CurrentSN = pflowControl.pDut.ReadSn();
                Thread.Sleep(200);
                textBoxSN.Text = pflowControl.CurrentSN;
                textBoxSN.Refresh();
                Thread.Sleep(200);

                if (pflowControl.CurrentSN.Length >= 5 )
                {
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
           // IdProductionName
               DataTable dd = new DataTable();

               string sTRtb = "GlobalManufactureChipsetInitialize";

               string Str = "Select* from " + sTRtb + " where PID=" + IdProductionName + " order by id";
                
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

        private void button_Config_Click(object sender, EventArgs e)
        {
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
                if (comboBoxPN.Text != "")
                {
                    labelShow.Text = "Equipment Configing.......";
                    labelShow.Refresh();
                    // MyEquipmentManage = new EquipmentManage();
                    aEquipList = new EquipmentList();
                    pflowControl.ProductionTypeId = IdProductionType;
                    pflowControl.TestPlanId = IdTestplan;
                    pflowControl.CurrentSN = textBoxSN.Text;
                    pflowControl.ProductionTypeName = comboBoxPType.Text;

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

        private void comboBoxPN_TextChanged(object sender, EventArgs e)
        {
            comboBoxTestPlan.Items.Clear();
            pflowControl.TestPlanId = 0;
            if (comboBoxPN.Text != null && comboBoxPN.Text != "")
            {
                // pflowControl.GetIdFromProductionNameTable(comboBoxPN.Text, out IdProductionName,out pflowControl.TotalChannel,out pflowControl.TotalVccCount,out pflowControl.TotalTempCount, out pflowControl.StrAuxAttribles);
                pflowControl.GetIdFromProductionNameTable(comboBoxPN.SelectedItem.ToString(), out IdProductionName, out pflowControl.TotalChannel, out pflowControl.TotalVccCount, out pflowControl.TotalTempCount, out pflowControl.StrAuxAttribles);

                pflowControl.ProductionNameId = IdProductionName;
                pflowControl.StrProductionName = comboBoxPN.Text;
                DataTable dd = new DataTable();
              //  string Str = "Select* from TopoTestPlan where PID=" + IdProductionName + " order by id";
                string Str = "Select* from TopoTestPlan where IgnoreFlag='false' and PID=" + IdProductionName + " order by id";
               
                string sTRtb = "TopoTestPlan";
                dd = pflowControl.MyDataio.GetDataTable(Str, sTRtb);

                comboBoxTestPlan.Items.Clear();
                for (int i = 0; i < dd.Rows.Count; i++)
                {
                    comboBoxTestPlan.Items.Add(dd.Rows[i]["ItemName"].ToString());
                }
            }

           
        }

        private void comboBoxPType_TextChanged(object sender, EventArgs e)
        {
            pflowControl.GetIdFromTpyeTable(comboBoxPType.SelectedItem.ToString(), out IdProductionType, out  pflowControl.MSAID);

            DataTable dd = new DataTable();
            //   DataIo.GetDataTable()

            string Str = "Select* from GlobalProductionName where PID=" + IdProductionType + " and IgnoreFlag='false' order by id";
            string sTRtb = "GlobalProductionName";
            dd = pflowControl.MyDataio.GetDataTable(Str, sTRtb);

            comboBoxPN.Items.Clear();
            comboBoxPN.Text = "";
            comboBoxPN.Refresh();
            for (int i = 0; i < dd.Rows.Count; i++)
            {

                comboBoxPN.Items.Add(dd.Rows[i]["PN"].ToString());
            }

            pflowControl.ProductionNameId = 0;


            comboBoxTestPlan.Items.Clear();
            pflowControl.TestPlanId = 0;
        }

        private void comboBoxTestPlan_TextChanged(object sender, EventArgs e)
        {
           if( MyEquipmentArrayy != null)MyEquipmentArrayy.Dispose();

            if (comboBoxPN.Text != null && comboBoxPN.Text != "")
            {
                string Str = "Select* from TopoTestPlan where PID=" + IdProductionName + " and ItemName='" + comboBoxTestPlan.SelectedItem + "' order by ID";
                string sTRtb = "TopoTestPlan";
                DataTable dd = pflowControl.MyDataio.GetDataTable(Str, sTRtb);
               
                IdTestplan = Convert.ToInt16(dd.Rows[0]["Id"]);

                pflowControl.FlagIgnoreRecordCoef = Convert.ToBoolean(dd.Rows[0]["IgnoreBackupCoef"]);
                pflowControl.FlagDrvierNeedInitialize = Convert.ToBoolean(dd.Rows[0]["IsChipInitialize"]);
                pflowControl.Flag_Need_EEPROMInitialize = Convert.ToBoolean(dd.Rows[0]["IsEEPROMInitialize"]);
                pflowControl.StrSqlSwVersion = dd.Rows[0]["SWVersion"].ToString().ToUpper();
                Dut_Usb_Addr = Convert.ToByte(dd.Rows[0]["USBPort"]);
                EquipmenNameArray = pflowControl.GetEquipmentNameList(IdTestplan);
            }
            FlagEquipmentConfigOK = false;
            FlagEquipmentOffSetOK = false;
        }

       

        private void FormATS_Load(object sender, EventArgs e)
        {
           
            this.labelVer.Text = "Version:" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString(); 
            labelVer.Refresh();

            this.labelbuildTime.Text = "BuildTime:" + System.IO.File.GetLastWriteTime(this.GetType().Assembly.Location).ToString("yyyy/MM/dd HH:mm:ss");

            var ss1 = pflowControl.ReadProductionTpye();

            comboBoxPType.Items.Clear();

            foreach (string s in ss1)
            {
                comboBoxPType.Items.Add(s);
            }


            //SetSize(this);// 记录原始界面尺寸
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

            for (int i = 0; i < ipEntry.AddressList.Length; i++)
            {
                string Str = ipEntry.AddressList[i].ToString();
                string[] IpPoint = Str.Split('.');
                if (IpPoint.Length == 4)
                {
                    int Ip1 = Convert.ToInt16(IpPoint[0]);
                    if (Ip1 < 256 && Ip1 > 0)
                    {
                        StrIP = ipEntry.AddressList[i].ToString();
                        break;
                    }

                }
                if (StrIP!="")
                {
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
           // chK_Fw.Checked = true;
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
               ExcelExport.TestPlanID = IdTestplan;
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
        ////  private void RefreshDataGridView(DataTable dt)
        //  {


        //      dataGridViewTotalData.DataSource = dt;
        //      dataGridViewTotalData.Refresh();


        //  }
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
        private void TestResultFormShow(bool Rflag,DataTable dt,bool ShowErrorData)
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
        //private void RefreshResult(bool Rflag)
        //{
        //    ReslutShow r1 = new ReslutShow();
        //    if (r1.InvokeRequired)
        //    {
        //        UpdataResult d1 = new UpdataResult(RefreshResult);
        //        this.BeginInvoke(d1, new Object[] { Rflag });

        //    }
        //    else
        //    {
        //        lock (this)
        //        {
        //            r1.ShowReslut(Rflag,dt,);
        //            r1.ShowDialog();
        //        }
        //    }

        //}
        // private void UpdataLable();
        private void Refresh_GUI()
        {
            int i = 0;

           // UpdataResult refreshResultLabel = new UpdataResult(RefreshResult);
           UpdataResultShow  FormShowTestResult =new UpdataResultShow(TestResultFormShow);
           // TestResultFormShow
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
                //if (pflowControl.prosess==100)
                //{
                //    string ss = "1";
                //}
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
                Thread.Sleep(200);

                ButtonUpdata(button_Test, true, Color.WhiteSmoke);
                ButtonUpdata(button_Config, true, Color.Green);
                ButtonUpdata(btnExport, true, Color.WhiteSmoke);
                ButtonUpdata(buttonStop, true, Color.WhiteSmoke);

                PanelUpdata(this.panelCombox, true);
                PanelUpdata(this.panelOffset, true);
                //ComboxUpdata(comboBoxPN, true);
                //ComboxUpdata(comboBoxPType, true);
                //ComboxUpdata(comboBoxTestPlan, true);

               
               // dt=pflowControl.TotalTestData.Select("Result=fail");

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
                pflowControl.pDut.deviceIndex = Dut_Usb_Addr;
                MyDutManufactureCoefficientsStructArray = pflowControl.GetDutManufactureCoefficients();
                MyManufactureChipsetStructArray = pflowControl.GetManufactureChipsetControl();
                MyDutManufactureChipSetInitialize = pflowControl.GetManufactureChipsetInitialize(); //等待数据库结构统一
                MyDutEEPROMInitializeStuct = pflowControl.Get_EEPROM_Init_FromSql();
                pflowControl.pDut.Initialize(MyDutManufactureCoefficientsStructArray, MyManufactureChipsetStructArray, MyDutManufactureChipSetInitialize,MyDutEEPROMInitializeStuct, pflowControl.StrAuxAttribles);//等待Driver 跟上
                //pflowControl.pDut.Initialize(MyDutManufactureCoefficientsStructArray, MyManufactureChipsetStructArray, pflowControl.StrAuxAttribles);
                MyEquipmentArrayy.Clear();
                Algorithm algorithm = new Algorithm();
                for (i = 0; i < EquipmenNameArray.Length / 2; i++)
                {
                    TestModeEquipmentParameters[] CurrentEquipmentStruct = pflowControl.GetCurrentEquipmentInf(EquipmenNameArray[i, 1]);

                    string[] StrArray = EquipmenNameArray[i, 0].Split('_');
                     CurrentEquipmentName = StrArray[0];

                    string CurrentEquipmentFullName = EquipmenNameArray[i, 0];
                    EquipmentBase CurrentEquipmentObject = (EquipmentBase)MyEquipmentManage.Createobject(CurrentEquipmentName);

                    UpDataStatus(labelShow, CurrentEquipmentName + "  Configing.....");
                    if (!CurrentEquipmentObject.Initialize(CurrentEquipmentStruct) || !CurrentEquipmentObject.Configure())
                    {
                        MessageBox.Show(EquipmenNameArray[i, 0].ToString() + "Configure Error");
                        FlagEquipmentConfigOK = false;

                        Exception ex = new Exception(CurrentEquipmentFullName + "Configure Error");
                        throw ex;
                    }
                    else
                    {
                        MyEquipmentArrayy.Add(CurrentEquipmentFullName, CurrentEquipmentObject);

                    }
                    CurrentEquipmentObject.Switch(false);
                    int ProcessValue = (int)(i + 1) * 100 / (EquipmenNameArray.Length / 2);
                    UpDataProcess(progress, ProcessValue);
                    UpDataStatus(labelProgress, ProcessValue.ToString());
                    UpDataStatus(labelShow, CurrentEquipmentName+"  ConfigOK");
                    plogManager.AdapterLogString(1, CurrentEquipmentName + "  ConfigOK");
                }
               
                pflowControl.pEnvironmentcontroll = new EnvironmentalControll(pflowControl.pDut,pflowControl.MyLogManager);
                //FlagEquipmentConfigOK = true;
               
               // Flag_Auto_Check_EvBVoltage
                pflowControl.MyEquipList = MyEquipmentArrayy.MyEquipList;
                if (EquipmenNameArray.Length == 0)
                {
                    MessageBox.Show("需要配置仪器数为0,请重新检查Testplan");
                    FlagEquipmentConfigOK = false;
                }
            }
            catch (Exception EX)
            {
                plogManager.AdapterLogString(1, CurrentEquipmentName + "  Config Error");

                MessageBox.Show(EX.Message,"ConfigEquipment Error ,PLS Check Equipment");

                UpDataStatus(this.labelShow, "Equipment Config false");
               // ButtonUpdata(this.button_Config, true, Color.Red);

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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

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
            if (chK_Aotu_Evb_Voltage.Checked)
            {
                pflowControl.Flag_NeedAutoCheck_EvBVoltage = true;
            }
            else
            {
                pflowControl.Flag_NeedAutoCheck_EvBVoltage = false;
            }
        }



        private void richInterfaceLog_TextChanged(object sender, EventArgs e)
        {

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

        private void button1_Click(object sender, EventArgs e)
        {
            #region  开启电源
            for (int i = 0; i < pflowControl.MyEquipList.Count; i++)
            {

                // TestModeEquipmentParameters[] CurrentEquipmentStruct = GetCurrentEquipmentInf(EquipmenNameArray.Values[i].ToString());
                string[] List = pflowControl.MyEquipList.Keys[i].ToString().Split('_');
                string CurrentEquipmentName = List[0];
                // string CurrentEquipmentID = List[1];
                string CurrentEquipmentType = List[2];

                if (pflowControl.MyEquipList.Keys[i].Contains("POWERSUPPLY"))
                {
                    pflowControl.MyEquipList[pflowControl.MyEquipList.Keys[i].ToString()].Switch(true);
                    // aPS = (Powersupply)pflowControl.MyEquipList[pflowControl.MyEquipList.Keys[i].ToString()];
                    Thread.Sleep(200);
                    break;
                }

            }
            #endregion

            pflowControl.pDut.FullFunctionEnable();
        }
     
    
    }
    public class hp// 记录原始窗体的尺寸
    {
        public Size s { set; get; }
        public Point p { set; get; }
    }
}

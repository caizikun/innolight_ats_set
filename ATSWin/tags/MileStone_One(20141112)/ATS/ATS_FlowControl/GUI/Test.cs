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
        private ArrayList IccOffsetArray = new ArrayList();
       //----------------------------
        private int IdTestplan;
        private int IdProductionName;
        private int IdProductionType;
        private bool FlagEquipmentConfigOK = false;
        private int RefreashTime = 0;
        private EquipmentArray MyEquipmentArrayy = new EquipmentArray();
        private string[,] EquipmenNameArray;
        
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
           // string resultFile = "";
           

        }


        private bool ReadXmlInf()
        {
           string[] Array1 = new string[4];
           string StrValue=null;

            MyXml = new ConfigXmlIO(Application.StartupPath + "\\Config.xml");
         

#region DataBase
           
#endregion
#region Equipment Offset
            
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

           ////-----------------VccOffset
           // StrValue = MyXml.VccOffset;
           // Array1 = StrValue.Split(',');
           // VccOffset.Clear();
           // VccOffset.Text = Array1[0];
           // VccOffset.Refresh();


           // for (int i = 0; i < Array1.Length; i++)
           // {
           //     VccOffsetArray.Add(Array1[i]);
           // }
           // //-----------------IccOffset
           // StrValue = MyXml.IccOffset;
           // Array1 = StrValue.Split(',');
           // IccOffsetArray.Clear();
           // IccOffset.Text = Array1[0];
           // IccOffset.Refresh();

           // for (int i = 0; i < Array1.Length; i++)
           // {
           //     IccOffsetArray.Add(Array1[i]);
           // }
#endregion

       
            return true;
        }
#region Old Pictrue 

        

       

        #region  Refresh  Config Equipment
        public delegate void SetCorlor(Color c1);
        private void setCorlor(Color c2)
        {
            button_Config.BackColor = c2;
            button_Config.Refresh();
        }
        private void ConfigEquipment()
        {

            FlagEquipmentConfigOK = true;

            UpdataRichBox refreshRichBox = new UpdataRichBox(UpdataShowLog);
            UpdataDataTable refreshDataTable = new UpdataDataTable(RefreshDataGridView);
           
             DutStruct[] MyDutManufactureCoefficientsStructArray;
             DriverStruct[] MyManufactureChipsetStructArray;
             DriverInitializeStruct[] MyDutManufactureChipSetInitialize;
             int i = 0;
             try
             {
                
                 pflowControl.pDut = (DUT)MyEquipmentManage.Createobject(pflowControl.ProductionTypeName.ToUpper() + "DUT");
                 pflowControl.pDut.deviceIndex = 0;
                 MyDutManufactureCoefficientsStructArray = pflowControl.GetDutManufactureCoefficients();
                 MyManufactureChipsetStructArray = pflowControl.GetManufactureChipsetControl();
                 MyDutManufactureChipSetInitialize = pflowControl.GetManufactureChipsetInitialize(); //等待数据库结构统一
                 pflowControl.pDut.Initialize(MyDutManufactureCoefficientsStructArray, MyManufactureChipsetStructArray, MyDutManufactureChipSetInitialize, pflowControl.StrAuxAttribles);//等待Driver 跟上
                 //pflowControl.pDut.Initialize(MyDutManufactureCoefficientsStructArray, MyManufactureChipsetStructArray, pflowControl.StrAuxAttribles);
                 MyEquipmentArrayy.Clear();
                 Algorithm algorithm = new Algorithm();
                 for (i = 0; i < EquipmenNameArray.Length/2; i++)
                 {
                     TestModeEquipmentParameters[] CurrentEquipmentStruct = pflowControl.GetCurrentEquipmentInf(EquipmenNameArray[i, 1]);

                     string[] StrArray = EquipmenNameArray[i, 0].Split('_');
                     string CurrentEquipmentName =StrArray[0];
                     
                     string CurrentEquipmentFullName =EquipmenNameArray[i, 0];
                     EquipmentBase CurrentEquipmentObject = (EquipmentBase)MyEquipmentManage.Createobject(CurrentEquipmentName);

                     if (!CurrentEquipmentObject.Initialize(CurrentEquipmentStruct) || !CurrentEquipmentObject.Configure())
                      {
                          MessageBox.Show(EquipmenNameArray[i,0].ToString() + "Configure Error");
                          FlagEquipmentConfigOK = false;
                         
                          Exception ex = new Exception(CurrentEquipmentFullName + "Configure Error");
                          throw ex;
                      }
                      else
                      {
                          MyEquipmentArrayy.Add(CurrentEquipmentFullName, CurrentEquipmentObject);

                      }
                      int ProcessValue = (int)(i + 1)*100 / (EquipmenNameArray.Length/2);
                      UpDataProcess(progress, ProcessValue);
                      UpDataStatus(labelProgress, ProcessValue.ToString());
                 }
                 pflowControl.pEnvironmentcontroll = new EnvironmentalControll(pflowControl.pDut);
                 //FlagEquipmentConfigOK = true;
                 pflowControl.MyEquipList = MyEquipmentArrayy.MyEquipList;
                 if (EquipmenNameArray.Length==0)
                 {
                     MessageBox.Show("需要配置仪器数为0,请重新检查Testplan");
                     FlagEquipmentConfigOK = false;
                 }
             }  
             catch (Exception EX)
             {
                 MessageBox.Show("ConfigEquipment Error ,PLS Check Equipment");
                 UpDataStatus(this.labelShow, "Equipment Config false");
                 ButtonUpdata(this.button_Config, true, Color.Red);  

                 FlagEquipmentConfigOK = false;
             }
             finally
             {
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

             }

             

           
           
        }
#endregion
        private void ButtonUpdata(Button B1,bool flag,Color c1)
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
        private void button_Test_Click(object sender, EventArgs e)
        {
           
             richInterfaceLog.Focus();
             dataGridViewTotalData.DataSource=null;


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
                     Thread.Sleep(200);
                     break;
                 }
               
             }
            if (pflowControl.TotalTestData!=null)
            {
                pflowControl.TotalTestData.Clear();
            }

            TestStartTime = pflowControl.MyDataio.GetCurrTime().ToString("yyyy/MM/dd HH:mm:ss");
            textBoxSN.Text ="";


           
                textBoxSN.Text = "";
                int powerIndex = 0;
                for (int i = 0; i < pflowControl.MyEquipList.Count; i++)
                {

                    // TestModeEquipmentParameters[] CurrentEquipmentStruct = GetCurrentEquipmentInf(EquipmenNameArray.Values[i].ToString());
                    string[] List = pflowControl.MyEquipList.Keys[i].ToString().Split('_');
                    string CurrentEquipmentName = List[0];
                    string CurrentEquipmentID = List[1];
                    string CurrentEquipmentType = List[2];

                    if (pflowControl.MyEquipList.Keys[i].Contains("POWERSUPPLY"))
                    {
                        pflowControl.MyEquipList[pflowControl.MyEquipList.Keys[i].ToString()].Switch(true);
                        Thread.Sleep(200);
                        break;
                    }

                }
                try
                {
                    #region 读取序号
                    string SN;
                    int KK = 0;

                   

                    int ReadSNTime = 0;
                    for (ReadSNTime = 0; ReadSNTime < 3; ReadSNTime++)
                    {
                        pflowControl.CurrentSN = pflowControl.pDut.ReadSn();
                        Thread.Sleep(200);
                        textBoxSN.Text = pflowControl.CurrentSN;
                        textBoxSN.Refresh();
                        Thread.Sleep(200);

                        pflowControl.CurrentFwRev = pflowControl.pDut.ReadFWRev().ToString().ToUpper();
                        textBoxFW.Text = pflowControl.CurrentFwRev;
                        textBoxFW.Refresh();

                        if (pflowControl.CurrentSN.Length >= 10 && pflowControl.CurrentSN.Substring(0,1)=="I")
                        {
                            break;
                        }
                    }
                    if (pflowControl.CurrentSN.Length < 10 || pflowControl.CurrentSN.Substring(0, 1) != "I")
                    {
                        MessageBox.Show("SN 读取异常请重新读取");
                        textBoxSN.Text = "";
                    }
                    else
                    {
                        textBoxSN.Text = pflowControl.CurrentSN;
                    }
                    textBoxSN.Refresh();

                    pflowControl.CurrentSN = textBoxSN.Text;

                    #endregion

                }
                catch
                {
                    MessageBox.Show("We Cant't Get SN");
                    textBoxSN.Text = "";
                    textBoxSN.Refresh();
                }
           
            if (textBoxSN.Text != "")
            {
                if (FlagEquipmentConfigOK)
                {

                    pflowControl.FlagFlowControllEnd = false;

                    progress.Value = 0;
                    progress.Refresh();
              
                   // 对当前的模块进行Driver初始化,等待DUT 接口


                    labelShow.Text = " Test Start--------------------------";
                    labelShow.Refresh();
                    plogManager.AdapterLogString(1, "A New Record---------------------------");
                    plogManager.AdapterLogString(1, pflowControl.CurrentSN);
                    pflowControl.MyDataio.WriterSN(pflowControl.TestPlanId, pflowControl.CurrentSN, pflowControl.CurrentFwRev, out pflowControl.SNID);
                    
                    dataGridViewTotalData.DataSource = pflowControl.TotalTestData;
                    richInterfaceLog.Text = "";
                    richInterfaceLog.Refresh();
                    button_Test.BackColor = Color.Yellow;
                    button_Test.Refresh();


                    TestThread = new Thread(Test);
                    TestThread.Start();
                    TestThread.Priority = ThreadPriority.Highest;
                   // timer1.Enabled = true;
                    RefreshThread = new Thread(RefreshRichBOX);
                    RefreshThread.Start();
                    //RefreshThread.Priority = ThreadPriority.Lowest;
                    button_Test.BackColor = Color.White;
                    button_Test.Refresh();

               
                }
                else
                {
                    MessageBox.Show("PLS Config Equipment");
                }
                   
           
               
           
            }
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
#endregion

#region  Refresh Interface

        public delegate void UpdataDataTable(DataTable dt);
        public delegate void UpdataLable(Label L1,string label);
        public delegate void UpdataRichBox(RichTextBox R1, string label);
        public delegate void UpdataProcessbar(ProgressBar p1,int process);
        public delegate void UpdataResult(bool ResultFlag);
        public delegate void UpdataButton(Button B1,bool flag,Color c1);

        private void UpDataStatus(Label L1, string StrStatus)
        {
            if (StrStatus != "")
            {

                if (L1.InvokeRequired)
                {
                    UpdataLable r = new UpdataLable(UpDataStatus); ;
                    L1.Invoke(r, new Object[] { L1, StrStatus });

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
                    R1.Invoke(d1, new Object[] { R1, StrInf });

                }
                else
                {
                    lock (this)
                    {
                        R1.Text += StrInf;

                        R1.Select(R1.Text.Length, 1);
                        R1.ScrollToCaret();
                        R1.Refresh();

                    }
                }
            }
            
        }
        private void RefreshDataGridView(DataTable dt)
        {
            dataGridViewTotalData.DataSource = dt;
            dataGridViewTotalData.Refresh();
        }
        private void UpDataProcess(ProgressBar p1,int process)
        {
            if (p1.InvokeRequired)
            {
                UpdataProcessbar r = new UpdataProcessbar(UpDataProcess); ;
                p1.Invoke(r, new Object[] {p1, process });

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
        private void RefreshResult(bool  Rflag)
        {
            ReslutShow r1 = new ReslutShow();
            if (r1.InvokeRequired)  
            {
                UpdataResult d1 = new UpdataResult(RefreshResult);
                BeginInvoke(d1, new Object[] { Rflag });

            }
            else
            {
                lock (this)
                {
                    r1.ShowReslut(Rflag);
                    r1.ShowDialog();
                }
            }

         
           
           
        } 

        private void RefreshRichBOX()
        {
            //RefreashTime
          
            //Thread.Sleep(200);
            int i = 0;

         //   UpdataLable refreshLabel = new UpdataLable(UpDataStatus);
           // UpdataLable refreshLabel = new UpdataLable()
            UpdataRichBox refreshRichBox = new UpdataRichBox(UpdataShowLog);
            UpdataDataTable refreshDataTable = new UpdataDataTable(RefreshDataGridView);
           // RefreshProcess refreshProcess = new RefreshProcess(RefreshProcessBar);
            UpdataResult refreshResultLabel = new UpdataResult(RefreshResult);
           
            string Strlabelshow = "";
             string Strichinterface="";
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
               // if (richinterface!="") this.Invoke(refreshRichBox, richinterface);
                UpdataShowLog(this.richInterfaceLog, Strichinterface);

                if (i > 100)
                {
                    
                    this.Invoke(refreshDataTable, pflowControl.TotalTestData);

                    i = 0;
                }

                UpDataProcess(progress ,pflowControl.prosess);
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

              dataGridViewTotalData.DataSource = pflowControl.TotalTestData;
              Thread.Sleep(200);

              this.Invoke(refreshResultLabel, pflowControl.TotalResultFlag);
              TestThread.Abort();
              RefreshThread.Abort();
              
              
          }

        }

        public void ShowResult()
        {
            

            labelShow.Text = "Test End---------";
        }

#endregion
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
            button_Config.Enabled = false;
            button_Config.BackColor = Color.Yellow;
            progress.Value = 0;
            progress.Refresh();
            labelProgress.Text = "0";
            labelProgress.Refresh();
            try
            {
                if (comboBoxPN.Text != "")
                {
                    labelShow.Text = "Equipment Config ing.......";
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
#region  控件跟随界面大小变动
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
                string Str = "Select* from TopoTestPlan where PID=" + IdProductionName + " order by id";
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

            string Str = "Select* from GlobalProductionName where PID=" + IdProductionType + " order by id";
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
                comboBoxPN.Items.Clear();
                IdTestplan = Convert.ToInt16(dd.Rows[0]["Id"]);
                EquipmenNameArray = pflowControl.GetEquipmentNameList(IdTestplan);
            }
        }

       

        private void FormATS_Load(object sender, EventArgs e)
        {
            System.DateTime CurrentTime = new System.DateTime();
            CurrentTime = System.DateTime.Now;
            string Str_Time = CurrentTime.ToString("yyyy/MM/dd HH:mm:ss");
            string Str_Year = CurrentTime.Year.ToString();
            string Str_Month = CurrentTime.Month.ToString();
            string Str_Day = CurrentTime.Day.ToString();
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
        }

        private void buttonOffset_Click(object sender, EventArgs e)
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

            //---------------------IccOffset & PsOffset           
            //PsOffsetArray.Add(VccOffset.Text);
            //IccOffsetArray.Add(IccOffset.Text);

            //pflowControl.Vccoffset = Convert.ToDouble(PsOffsetArray[0]);
            //pflowControl.Iccoffset = Convert.ToDouble(IccOffsetArray[0]);

            //MyXml.VccOffset = PsOffsetArray[0].ToString();
            //MyXml.IccOffset = IccOffsetArray[0].ToString();
            //------------------------Config Equipment
            if (FlagEquipmentConfigOK)
            {
                for (int i = 0; i < pflowControl.MyEquipList.Count; i++)
                {

                    if (pflowControl.MyEquipList.Keys[i].Contains("ATT"))
                    {
                        for (byte j = 1; j < AttOffsetArray.Count + 1; j++)
                        {
                            pflowControl.MyEquipList.Values[i].configoffset(j.ToString(), AttOffsetArray[j - 1].ToString().Trim());
                        }

                    }
                    if (pflowControl.MyEquipList.Keys[i].Contains("SCOPE"))
                    {
                        for (byte j = 1; j < ScopeOffsetArray.Count + 1; j++)
                        {
                            pflowControl.MyEquipList.Values[i].configoffset(j.ToString(), ScopeOffsetArray[j - 1].ToString());
                        }

                    }
                    //if (pflowControl.MyEquipList.Keys[i].Contains("POWERSUPPLY"))// 添加了对Vccoffset 补偿
                    //{

                    //    pflowControl.MyEquipList.Values[i].configoffset("1", pflowControl.Vccoffset.ToString());
                    //}
                }


                buttonOffset.BackColor = Color.Green;
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

        private void buttonStop_Click(object sender, EventArgs e)    
        {
            try
            {

                if (MessageBox.Show("Do you want to End Thread?", "Database Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                   
                    string strEndTime = pflowControl.MyDataio.GetCurrTime().ToString("yyyy/MM/dd HH:mm:ss");

                    pflowControl.MyDataio.WriterLog(pflowControl.CurrentConditionId, pflowControl.SNID, pflowControl.StrSqlLog, pflowControl.StrStartTime, strEndTime, Convert.ToSingle(pflowControl.StrCurrentTemp), Convert.ToSingle(pflowControl.StrCurrentVcc), pflowControl.CurrentChannel, pflowControl.CurrentConditionResultflag, out pflowControl.CurrentLogId);

                    Thread.Sleep(3000);
                    if (pflowControl.dtCurrentConditionResultData.Rows.Count > 0) pflowControl.MyDataio.WriteResult(pflowControl.CurrentLogId, pflowControl.dtCurrentConditionResultData);
                    pflowControl.prosess = 100;

                    GC.Collect();

                    pflowControl.WriterEndTimeToSNTable();
                    // MessageBox.Show("FlowControl End");
                    pflowControl.TotalResultFlag = false;
                    pflowControl.pEnvironmentcontroll.Dispose();
                    pflowControl.MyLogManager.AdapterLogString(1, "Test End------------------------");
                    pflowControl.FlagFlowControllEnd = true;
                    TestThread.Abort();
                    RefreshThread.Abort();
                }
            }
            catch (System.Exception ex)
            {
                pflowControl.TotalResultFlag = false;
                pflowControl.prosess = 100;
                pflowControl.MyLogManager.AdapterLogString(1, "Test End------------------------");
                pflowControl.FlagFlowControllEnd = true;

                TestThread.Abort();
                RefreshThread.Abort();
            }
            finally
            {
                ReslutShow r1 = new ReslutShow();
                r1.ShowReslut(false);
                r1.ShowDialog();
                 
                
            }

        }
    }
    public class hp// 记录原始窗体的尺寸
    {
        public Size s { set; get; }
        public Point p { set; get; }
    }
}

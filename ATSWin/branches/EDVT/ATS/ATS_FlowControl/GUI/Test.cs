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
    
    public partial class Form1 : Form
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
        private SortedList<string, string> EquipmenNameArray = new SortedList<string, string>();
        
        public Form1()
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

        private void Form1_Load(object sender, EventArgs e)
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

           //-----------------VccOffset
            StrValue = MyXml.VccOffset;
            Array1 = StrValue.Split(',');
            VccOffset.Clear();
            VccOffset.Text = Array1[0];
            VccOffset.Refresh();


            for (int i = 0; i < Array1.Length; i++)
            {
                VccOffsetArray.Add(Array1[i]);
            }
            //-----------------IccOffset
            StrValue = MyXml.IccOffset;
            Array1 = StrValue.Split(',');
            IccOffsetArray.Clear();
            IccOffset.Text = Array1[0];
            IccOffset.Refresh();

            for (int i = 0; i < Array1.Length; i++)
            {
                IccOffsetArray.Add(Array1[i]);
            }
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

            RefreshString refreshLabel = new RefreshString(RefreshInterfaceLabelShow);
            RefreshString refreshRichBox = new RefreshString(RefreshInterfaceRichBox);
            RefreshDataTable refreshDataTable = new RefreshDataTable(RefreshDataGridView);
            EnaButton enaButton = new EnaButton(ControlButton);
            SetCorlor SetButtonColor = new SetCorlor(setCorlor);
            
           // MyEquipmentArrayy.MyEquipList
             DutStruct[] MyDutManufactureCoefficientsStructArray;
             DriverStruct[] MyManufactureChipsetStructArray;
             int i = 0;
             try
             {
                 //if (MyEquipmentArrayy!= null)
                 //{
                 //    MyEquipmentArrayy.MyEquipList.Clear();
                 //}
                 pflowControl.pDut = (DUT)MyEquipmentManage.Createobject(pflowControl.ProductionTypeName.ToUpper() + "DUT");
                 pflowControl.pDut.deviceIndex = 0;
                 MyDutManufactureCoefficientsStructArray = pflowControl.GetDutManufactureCoefficients();
                 MyManufactureChipsetStructArray = pflowControl.GetManufactureChipsetControl();

                 pflowControl.pDut.Initialize(MyDutManufactureCoefficientsStructArray, MyManufactureChipsetStructArray, pflowControl.StrAuxAttribles);
                

                 //------------------

                 for (i = 0; i < EquipmenNameArray.Count; i++)
                 {
                     TestModeEquipmentParameters[] CurrentEquipmentStruct = pflowControl.GetCurrentEquipmentInf(EquipmenNameArray.Values[i].ToString());
                     string[] List = EquipmenNameArray.Keys[i].ToString().Split('_');
                     string CurrentEquipmentName = List[0];
                     string CurrentEquipmentID = List[1];
                     string CurrentEquipmentType = List[2];

                      MyEquipmentArrayy.Add(EquipmenNameArray.Keys[i].ToString(), (EquipmentBase)MyEquipmentManage.Createobject(CurrentEquipmentName));

                      if
                          (!MyEquipmentArrayy.MyEquipList[EquipmenNameArray.Keys[i].ToString()].Initialize(CurrentEquipmentStruct))
                      {

                          MessageBox.Show(EquipmenNameArray.Keys[i].ToString() + "Initialize Error");
                          FlagEquipmentConfigOK = false;
                          Error_Message ex = new Error_Message(EquipmenNameArray.Keys[i].ToString() + "Initialize Error");
                          throw ex;
                      }

                      if (!MyEquipmentArrayy.MyEquipList[EquipmenNameArray.Keys[i].ToString()].Configure())
                      {



                          MessageBox.Show(EquipmenNameArray.Keys[i].ToString() + "Configure Error");
                          FlagEquipmentConfigOK = false;

                          Error_Message ex = new Error_Message(EquipmenNameArray.Keys[i].ToString() + "Configure Error");
                          throw ex;
                      }
                 }
                 pflowControl.pEnvironmentcontroll = new EnvironmentalControll(pflowControl.pDut);
                 FlagEquipmentConfigOK = true;
                 pflowControl.MyEquipList = MyEquipmentArrayy.MyEquipList;
             
             }  
             catch (Exception EX)
             {
                 MessageBox.Show("ConfigEquipment Error ,PLS Check Equipment");
                 MessageBox.Show("ConfigEquipment Error ,PLS Check Equipment");
                 this.Invoke(SetButtonColor, Color.Red);
                 this.Invoke(refreshLabel, "Equipment Config false");
                 FlagEquipmentConfigOK = false;
             }

             

            if ( FlagEquipmentConfigOK )
            {
                MessageBox.Show("ConfigEquipment OK");
                this.Invoke(SetButtonColor, Color.Green);
                this.Invoke(refreshLabel, "Equipment Config OK");
               // this.Invoke(enaButton, true);
                FlagEquipmentConfigOK = true;
            }
            else
            {
                
            }
            this.Invoke(enaButton, true);
        }
#endregion
        private void button_Test_Click(object sender, EventArgs e)
        {
             richInterfaceLog.Focus();
             labelResult.Text = "";
             labelResult.Refresh();
             dataGridViewTotalData.DataSource=null;


             for (int i = 0; i < pflowControl.EquipmenNameArray.Count; i++)
             {
               
                 // TestModeEquipmentParameters[] CurrentEquipmentStruct = GetCurrentEquipmentInf(EquipmenNameArray.Values[i].ToString());
                 string[] List = pflowControl.EquipmenNameArray.Keys[i].ToString().Split('_');
                 string CurrentEquipmentName = List[0];
                 string CurrentEquipmentID = List[1];
                 string CurrentEquipmentType = List[2];

                if (pflowControl.EquipmenNameArray.Keys[i].Contains("POWERSUPPLY"))
                 {
                     pflowControl.MyEquipList[pflowControl.EquipmenNameArray.Keys[i].ToString()].Switch(true);
                 }
               
             }
            if (pflowControl.TotalTestData!=null)
            {
                pflowControl.TotalTestData.Clear();
            }

            TestStartTime = pflowControl.MyDataio.GetCurrTime().ToString("yyyy/MM/dd HH:mm:ss");
            textBoxSN.Text ="";

            try
            {
                textBoxSN.Text = pflowControl.pDut.ReadSn();
            }
            catch 
            {
                MessageBox.Show("We Cant't Get SN");
            }
           
            textBoxSN.Refresh();
            pflowControl.SerialNO = textBoxSN.Text;
            if (textBoxSN.Text != "")
            {

             
                if (FlagEquipmentConfigOK)
                {

                    pflowControl.FlagFlowControllEnd = false;

                    progress.Value = 0;
                    progress.Refresh();
              
                    labelShow.Text = " Test Start--------------------------";
                    labelShow.Refresh();
                    plogManager.AdapterLogString(1, "A New Record---------------------------");
                    plogManager.AdapterLogString(1, pflowControl.SerialNO);
                    pflowControl.MyDataio.WriterSN(pflowControl.TestPlanId, pflowControl.SerialNO, out pflowControl.SNID);
                    
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
            else
            {
                MessageBox.Show("SN 号码未获得~~~~");
            }
        }

#endregion

#region  Refresh Interface

        public delegate void RefreshDataTable(DataTable dt);
        public delegate void RefreshString(string label);
        public delegate void RefreshProcess(int process);
        public delegate void RefreshResultLabel(bool ResultFlag);
        public delegate void EnaButton(bool flag);
        private void RefreshInterfaceLabelShow(string label)
        {
            labelShow.Text = label;
            labelShow.Refresh();
        }
        private void RefreshInterfaceRichBox(string richbox)
        {
            if (richbox!="")
            {
                richInterfaceLog.Text += richbox;

                richInterfaceLog.Select(richInterfaceLog.Text.Length, 1);
                richInterfaceLog.ScrollToCaret();
                richInterfaceLog.Refresh();
            }
            
        }
        private void RefreshDataGridView(DataTable dt)
        {
            dataGridViewTotalData.DataSource = dt;
            dataGridViewTotalData.Refresh();
        }
        private void RefreshProcessBar(int process)
        {
            progress.Value = process;
            progress.Refresh();
        }
        private void RefreshResult(bool  Rflag)
        {
            if (Rflag)
            {
                labelResult.Text = "PASS";
                labelResult.ForeColor = Color.Green;
            }
            else
            {
                labelResult.Text = "FAIL";
                labelResult.ForeColor = Color.Red;
            }
            labelResult.Refresh();
        }
        private void ControlButton(bool flag)
        {
            if (flag)
            {
                button_Config.Enabled = true;
                button_Config.BackColor = Color.Green;
            }
            else
            {
                button_Config.Enabled = false;
                button_Config.BackColor = Color.Red;
            }
            button_Config.Refresh();
        }
        private void RefreshRichBOX()
        {
            //RefreashTime
          
            //Thread.Sleep(200);
            int i = 0;
           
            RefreshString refreshLabel = new RefreshString(RefreshInterfaceLabelShow);
            RefreshString refreshRichBox = new RefreshString(RefreshInterfaceRichBox);
            RefreshDataTable refreshDataTable = new RefreshDataTable(RefreshDataGridView);
            RefreshProcess refreshProcess = new RefreshProcess(RefreshProcessBar);
            RefreshResultLabel refreshResultLabel = new RefreshResultLabel(RefreshResult);
           
            string labelshow = "";
             string richinterface="";
           while (!pflowControl.FlagFlowControllEnd)
            {
                i++;
                richinterface = "";
                while (pflowControl.QueueShow.Count > 0)
                {
                    labelshow = pflowControl.QueueShow.Dequeue() + "\r\n";
                    
                }
                this.Invoke(refreshLabel, labelshow);
              
                while (pflowControl.QueueInterfaceLog.Count > 0)
                {
                    richinterface += pflowControl.QueueInterfaceLog.Dequeue() + "\r\n";

                }
                this.Invoke(refreshRichBox, richinterface);

                if (i > 100)
                {
                    
                    this.Invoke(refreshDataTable, pflowControl.TotalTestData);
                    i = 0;
                }


                this.Invoke(refreshProcess, (int)pflowControl.prosess);
            }
          if (pflowControl.FlagFlowControllEnd)
          {

              while (pflowControl.QueueShow.Count > 0)
              {
                  labelshow = pflowControl.QueueShow.Dequeue() + "\r\n";
              }
              this.Invoke(refreshLabel, labelshow);

              while (pflowControl.QueueInterfaceLog.Count > 0)
              {
                  richinterface += pflowControl.QueueInterfaceLog.Dequeue() + "\r\n";

              }
              this.Invoke(refreshRichBox, richinterface);

              dataGridViewTotalData.DataSource = pflowControl.TotalTestData;
              dataGridViewTotalData.Refresh();
              Thread.Sleep(200);

              this.Invoke(refreshResultLabel, pflowControl.TotalResultFlag);
              TestThread.Abort();
              RefreshThread.Abort();
              
              
          }

            
          

      

        }

        public void ShowResult()
        {
            if (pflowControl.TotalResultFlag)
            {

               labelResult.Text = "PASS";
               labelResult.ForeColor = Color.Green;
            }
            else
            {
                labelResult.Text = "FAIL";
                labelResult.ForeColor = Color.Red;
            }

            labelResult.Refresh();

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
        
       
      
        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void comboBoxPN_Click(object sender, EventArgs e)
        {
            if (comboBoxPType.Text != "" & comboBoxPType.Text != null)
            {

                comboBoxPN.Items.Clear();

                pflowControl.GetIdFromTpyeTable(comboBoxPType.Text, out IdProductionType,out pflowControl.MSAID);

                string[,] ProductNameArray = pflowControl.ReadProductionName(IdProductionType);



                for (int i = 0; i < ProductNameArray.Length / 2; i++)
                {
                    comboBoxPN.Items.Add(ProductNameArray[i, 1]);
                }

                comboBoxPN.SelectedIndex = 0;
            }
        }

        private void comboBoxPN_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (comboBoxPType.Text != null && comboBoxPType.Text != "")
            {
               // pflowControl.GetIdFromProductionNameTable(comboBoxPN.Text, out IdProductionName,out pflowControl.TotalChannel,out pflowControl.TotalVccCount,out pflowControl.TotalTempCount, out pflowControl.StrAuxAttribles);
                 pflowControl.GetIdFromProductionNameTable(comboBoxPN.SelectedItem.ToString(), out IdProductionName,out pflowControl.TotalChannel,out pflowControl.TotalVccCount,out pflowControl.TotalTempCount, out pflowControl.StrAuxAttribles);

                 pflowControl.ProductionNameId = IdProductionName;
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

        private void comboBoxPN_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxPType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            pflowControl.GetIdFromTpyeTable(comboBoxPType.SelectedItem.ToString(), out IdProductionType,out  pflowControl.MSAID);

            DataTable dd = new DataTable();
            //   DataIo.GetDataTable()

            string Str = "Select* from GlobalProductionName where PID=" + IdProductionType + " order by id";
            string sTRtb = "GlobalProductionName";
            dd = pflowControl.MyDataio.GetDataTable(Str, sTRtb);
           
            comboBoxPN.Items.Clear();
            for (int i = 0; i < dd.Rows.Count; i++)
            {
              
                comboBoxPN.Items.Add(dd.Rows[i]["PN"].ToString());
            }
         

        }
        private void comboBoxTestPlan_SelectionChangeCommitted(object sender, EventArgs e)
        {
            MyEquipmentArrayy.Dispose();

            if (comboBoxPN.Text != null && comboBoxPN.Text != "")
            {
                string Str = "Select* from TopoTestPlan where PID=" + IdProductionName + " and ItemName='" + comboBoxTestPlan.SelectedItem + "' order by id";
                string sTRtb = "TopoTestPlan";
                DataTable dd = pflowControl.MyDataio.GetDataTable(Str, sTRtb);
                comboBoxPN.Items.Clear();
                IdTestplan = Convert.ToInt16(dd.Rows[0]["Id"]);
                EquipmenNameArray = pflowControl.GetEquipmentNameList(IdTestplan);
            }

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
           
//----------------Scope
            ScopeOffsetArray.Clear();
            ScopeOffsetArray.Add( ScopeOffset1.Text);
            ScopeOffsetArray.Add( ScopeOffset2.Text);
            ScopeOffsetArray.Add(ScopeOffset3.Text);
            ScopeOffsetArray.Add(ScopeOffset4.Text);

            string StrScopeOffsetArray= ScopeOffsetArray[0].ToString();

            for (int i = 1; i < 4;i++ )
            {
                StrScopeOffsetArray +=","+ ScopeOffsetArray[i].ToString();
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
            PsOffsetArray.Add(VccOffset.Text);
            IccOffsetArray.Add(IccOffset.Text);

            pflowControl.Vccoffset = Convert.ToDouble(PsOffsetArray[0]);
            pflowControl.Iccoffset = Convert.ToDouble(IccOffsetArray[0]);

            MyXml.VccOffset = PsOffsetArray[0].ToString();
            MyXml.IccOffset = IccOffsetArray[0].ToString();
//------------------------Config Equipment
            if (FlagEquipmentConfigOK)
            {
                for (int i = 0; i < pflowControl.MyEquipList.Count;i++ )
                {

                    if (pflowControl.MyEquipList.Keys[i].Contains("ATT"))
                    {
                        for (byte j=1;j<AttOffsetArray.Count+1;j++)
                        {
                            pflowControl.MyEquipList.Values[i].configoffset(j.ToString(), AttOffsetArray[j-1].ToString().Trim());
                        }
                        
                    }
                    if (pflowControl.MyEquipList.Keys[i].Contains("SCOPE"))
                    {
                        for (byte j = 1; j < ScopeOffsetArray.Count+1; j++)
                        {
                            pflowControl.MyEquipList.Values[i].configoffset(j.ToString(), ScopeOffsetArray[j-1].ToString());
                        }

                    }
                }
               button2.BackColor = Color.Green;
            }
            else
            {
                MessageBox.Show("PLS Config Equipment First");
            }
            
        }

        private void button8_Click(object sender, EventArgs e)
        {
           Export ExcelExport = new Export();
           // IdTestplan
           if (IdTestplan != -1)
           {
               ExcelExport.TestPlanID = IdTestplan;
               ExcelExport.pflowControl = pflowControl;
               ExcelExport.ShowDialog();
           }
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



        private void button1_Click(object sender, EventArgs e)
        {
//            FLEX86100 tempScope = (ATS_Driver.FLEX86100)pflowControl.MyEquipList["FLEX86100_7_SCOPE"];
//            tempScope.SetMaskAlignMethod(1);
//            tempScope.SetMode(0);//0="EYE"
//            tempScope.MaskONOFF(false);
//            tempScope.SetRunTilOff();
//            tempScope.RunStop(true);
//            tempScope.OpenOpticalChannel(true)
//;//true is OPTChannel or ELEChannel
//            tempScope.AutoScale();
//            tempScope.CenterEye();
//            tempScope.AutoScale();
//            tempScope.SetruntilMod(700, 1);
//            tempScope.RunStop(true);
//            tempScope.ClearDisplay();
//            tempScope.DisplayER();
//            tempScope.DisplayEyeAmplitude();
//            tempScope.DisplayCrossing();
//            Thread.Sleep(1000);
//            tempScope.ClearDisplay();
//            Thread.Sleep(1000);
//            tempScope.WaitUntilComplete();
//             byte  marginVaulue = tempScope.MaskTest();
//            tempScope.SetruntilMod(0, 0);
//            tempScope.RunStop(false);
//            tempScope.ClearMesures();
//            tempScope.RunStop(true);
//            tempScope.SetruntilMod(0, 0);
//            tempScope.AutoScale();
//            Thread.Sleep(1000);
//          double   apWat = tempScope.GetAveragePowerWatt();
//          double jitterRMS = tempScope.GetJitterRMS();
//          double jitterPP = tempScope.GetJitterPP();
//          double riseTime = tempScope.GetRisetime();
//          double fallTime = tempScope.GetFalltime();
//          double crossing = tempScope.GetCrossing();
//          double er = tempScope.GetEratio();
//          double apDBM = tempScope.GetAveragePowerdbm();


             TestModeEquipmentParameters[] psstruct1 = new TestModeEquipmentParameters[16];
             //MP1800ED ed = new MP1800ED(); 

            psstruct1[0].FiledName = "Addr";
            psstruct1[0].DefaultValue = "1";
            psstruct1[1].FiledName = "IOType";
            psstruct1[1].DefaultValue = "GPIB";
            psstruct1[2].FiledName = "Reset";
            psstruct1[2].DefaultValue = "false";
            psstruct1[3].FiledName = "Name";
            psstruct1[3].DefaultValue = "MP1800ED";
            psstruct1[4].FiledName = "CURRENTCHANNEL";
            psstruct1[4].DefaultValue = "1";
            psstruct1[5].FiledName = "SLOT";
            psstruct1[5].DefaultValue = "3";
            psstruct1[6].FiledName = "TOTALCHANNELS";
            psstruct1[6].DefaultValue = "4";
            psstruct1[7].FiledName = "DATAINPUTINTERFACE";
            psstruct1[7].DefaultValue = "2";
            psstruct1[8].FiledName = "PRBSLENGTH";
            psstruct1[8].DefaultValue = "31";
            psstruct1[9].FiledName = "ERRORRESULTZOOM";
            psstruct1[9].DefaultValue = "1";
            psstruct1[10].FiledName = "EDGATINGMODE";
            psstruct1[10].DefaultValue = "1";
            psstruct1[11].FiledName = "EDGATINGUNIT";
            psstruct1[11].DefaultValue = "0";
            psstruct1[12].FiledName = "EDGATINGTIME";
            psstruct1[12].DefaultValue = "5";

            //ed.Initialize(psstruct1);
            //ed.Configure();
            //ed.ChangeChannel("1");
            //ed.AutoAlaign(true);


        }

        private void button_Config_Click(object sender, EventArgs e)
        {
            button_Config.Enabled = false;
            button_Config.BackColor = Color.Yellow;

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
                    pflowControl.SerialNO = textBoxSN.Text;
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

        private void button3_Click(object sender, EventArgs e)
        {
            int i=0;
            int logid=0;
            pflowControl.MyDataio.WriterSN(5, "AB", out i);
            pflowControl.MyDataio.WriterLog(8, i, "ab", pflowControl.MyDataio.GetCurrTime().ToString(), pflowControl.MyDataio.GetCurrTime().ToString(), 1, false, out logid);
          //  pflowControl.MyDataio.WriteResult()
            DataTable dd = new DataTable();

          
            dd.Columns.Add("ItemName");
            dd.Columns.Add("ItemValue");
            dd.Columns.Add("Result");

            DataRow pdr = dd.NewRow();


           
            pdr["ItemName"] = "AP";
            pdr["ItemValue"] = 3.14;
            pdr["Result"] = false;
            // TotalTestData.Rows.Add(dr);
            dd.Rows.Add(pdr);

            pflowControl.MyDataio.WriteResult(logid, dd);
             
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            DataTable dt=new DataTable();
            //dr["DataRecord"]
            dt.Columns.Add("DataRecord");
            dt.Columns.Add("ItemName");
            dt.Columns.Add("Value");
            dt.Columns.Add("Result");

            DataRow dr= dt.NewRow();


            dr["DataRecord"] = 1;
            dr["ItemName"] = "AP";
            dr["Value"] = 3.14;
            dr["Result"] = false;

            dt.Rows.Add(dr);

            pflowControl.MyDataio.WriteResult(305,dt);
        }
    }
}

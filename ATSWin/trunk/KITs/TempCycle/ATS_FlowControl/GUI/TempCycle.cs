using System;
using System.IO;
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
using System.Threading;
using System.Threading.Tasks;
namespace ATS
{
    struct InputParameter// 
    {
        public double AP;
        public double ER;
        public double TxpowADC;
        public double TempLD;
        public double TempCase;
        public double DAC_Bias;
        public double DAC_Mod;
    }

    struct OutParameter
    {
        public double TE;
        public double SeBias;
        public double SeMod;
        public int ModDAC;
        public int BiasDAC;

    }
    public partial class TempCycle : Form
    {

        InputParameter RT_Input_Parameter;
        InputParameter HT_Input_Parameter;
        InputParameter LT_Input_Parameter;

        OutParameter RT_Output_Parameter;
        OutParameter HT_Output_Parameter;
        OutParameter LT_Output_Parameter;



        private Attribute MyAttribute;

        class Attribute
        {
            public TempCycleXml MyXml;
            public DataIO MyDataio;
            public logManager MylogManager = new logManager();
            public bool FlagEquipmentConfigOK = false;
            public string ProductionTypeName;
            public int IdPnType;
            public int IdPnName;
            public int MCoefsID;
            public string StrProductionName;
            public bool FlagFlowControllEnd;
            public Thread TestThread;
            public Thread RefreshThread;
            public int TotalChannnel;
            public int prosess;

            public DataTable dtTotalTestData;
            public DataTable dtCurrentConditionResultData;
            public DataTable dtProductionType=new DataTable();
            public DataTable dtEquipmentParameter = new DataTable();

            public string CurrentSN;
            public bool flag_FwVerOK;
            public string Current_DutFwRev;
            public bool flag_SNOK;
            public bool StopFlag;
            public bool TotalResultFlag;
            public bool ConditionEndflag=false;
            public bool ChangeTempEndflag = false;
        //    public LogQueue<string> QueueShow = new LogQueue<string> { };
            public OperateTxT MyOperateTxT;
            public int CycleTime=2;
            public double StartTemp=25;
            public int StartTempWaitTime = 50;
            public int ConditionCycle =1;
            public ArrayList ArrayPolarity = new ArrayList();

            public bool IsFXP = false;//true 等于对传,将读取别的产品的信息
           // public 
            #region TrackError
            public DataTable dtTotalTrackErrorData;
               public DataTable dtTrackError;

            #endregion

        }

        class ConditionInf
        {
            public TempCycleXml MyXml;
            public string StrPath;
            public DataTable dtCondition;
            public double CurrentVcc;
            public double CurrentTemp;
            public double StartTemp;
            public double EndTemp;
            public bool Polarity;// 1 为正极性 ,0 为反极性
            public double TempStep;
            public int StayTime;
            public bool LastPolarity;
            public bool IsNeedSetPolarity=true;
            public string[] ConditionHeardArray;
            public ArrayList ConditionList=new ArrayList();
            public String ConditionName;
            public String ConditionFileName;
        }

        class Equipmentstruct
        {
            public SortedList<string, EquipmentBase> EquipmentList = new SortedList<string, EquipmentBase>();
            public PPG MyPPG;
            public ErrorDetector MyED;
            public Thermocontroller MyTempControl;
            public Powersupply MyPowersupply;
            public EquipmentManage MyEquipmentManage;
            public DUT MyDut;
            public DUT MyLightSourceDut;
            public Scope MyScope;
            public ArrayList EquipementNameArray = new ArrayList();
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
                UpDataProcess(progress, MyAttribute.prosess);
              //  UpDataStatus(labelProgress, MyAttribute.prosess.ToString());
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

        public TempCycle()
        {
            InitializeComponent();

          
            MyAttribute = new Attribute();
            MyEquipmentstruct = new Equipmentstruct();
            MyEquipmentstruct.MyEquipmentManage = new EquipmentManage(MyAttribute.MylogManager);
           
            MyEquipmentstruct.EquipmentList.Clear();
            MyCondition.dtCondition = new DataTable();

            MyAttribute.dtTotalTestData = new DataTable();

            MyCondition.dtCondition.Columns.Add("Sequence");
           // MyCondition.dtCondition.Columns.Add("Polarity");//Sequence
            MyCondition.dtCondition.Columns.Add("vcc");
            MyCondition.dtCondition.Columns.Add("StartTemp");
            MyCondition.dtCondition.Columns.Add("EndTemp");
            MyCondition.dtCondition.Columns.Add("TempStep");
            MyCondition.dtCondition.Columns.Add("StayTime");

            ReadXmlInf();
         
        }

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

        private void TempCycle_Load(object sender, EventArgs e)
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

            MyAttribute.ConditionCycle = Convert.ToInt16(ConditionCycle.Text);

           
        
          //  listBox1
        }

        private string GetConditionXml()
        {

            string resultFile = "";


            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = "D:\\Patch";
            openFileDialog1.Filter = "All files (*.*)|*.*|txt files (*.XML)|*.XML";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                resultFile = openFileDialog1.FileName;
            
            return resultFile;
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
               MyAttribute.IdPnName = Convert.ToInt32(dr[0]["ID"].ToString());
                MyAttribute.MCoefsID = Convert.ToInt32(dr[0]["MCoefsID"].ToString());
                MyAttribute.TotalChannnel = Convert.ToInt32(dr[0]["Channels"].ToString());
                //  MyAttribute

                //MyAttribute.pGlobalParameters.

                #endregion



            }
        }

        private void button_Test_Click(object sender, EventArgs e)
        {
            MyAttribute.ArrayPolarity.Clear();

            if (this.chk_Polarity_P.Checked)
            {
                MyAttribute.ArrayPolarity.Add(1);
            }
            if (this.chk_Polarity_N.Checked)
            {
                MyAttribute.ArrayPolarity.Add(0);
            }

            MyCondition.dtCondition = (DataTable)dataGridView_Condition.DataSource;
            MyAttribute.CycleTime = int.Parse(this.textBox_CycleTime.Text);
           

            this.labelShow.Text = "ReadyForTest.....";
            this.labelShow.Refresh();
            if (ReadyForTest())
            {
                // MyAttribute
              

                this.labelShow.Text = "First Temp Waiting.....";
                this.labelShow.Refresh();

                MyAttribute.MylogManager.AdapterLogString(0, "***********************  SN=" + MyAttribute.CurrentSN + " ************************");
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

                if (MyEquipmentstruct.MyTempControl != null)
                {
                    MyEquipmentstruct.MyTempControl.SetPositionUPDown("0");

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

            SelectEquipment();
            SelectResultHead();
           

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


    

       #endregion

        #region  Funtion

        private DataTable FitEquipemtnInf()
        {
            DataTable dt=new DataTable();

          //  MyAttribute.MyDataio.
            string Selectconditions = "SELECT  GlobalAllEquipmentList.ItemName as EquipmentName,GlobalAllEquipmentParamterList.ItemName as ItemName,GlobalAllEquipmentParamterList.ItemValue as itemValue FROM [ATS_V2].[dbo].[GlobalAllEquipmentList],[ATS_V2].[dbo].GlobalAllEquipmentParamterList where (GlobalAllEquipmentList.ID=GlobalAllEquipmentParamterList.PID) and (GlobalAllEquipmentList.ItemName IN('E3631', 'N4960PPG' , 'N4960ED','TPO4300','MP1800PPG','MP1800ED','Inno25GBert_V2_PPG','Inno25GBert_V2_ED','FLEX86100')) ORDER BY GlobalAllEquipmentList.ItemName";
            dt = MyAttribute.MyDataio.GetDataTable(Selectconditions, "GlobalAllEquipmentList"); ;

            return dt;

        }

        private bool ReadXmlInf()
        {
            string[] Array1 = new string[4];
            string StrValue = null;

            MyAttribute.MyXml = new TempCycleXml(Application.StartupPath + "\\TempCycle.xml",0);

            MyAttribute.MyDataio = new SqlDatabase(MyAttribute.MyXml.DatabasePath, MyAttribute.MyXml.DbName, MyAttribute.MyXml.Username, MyAttribute.MyXml.PWD);
     
             if (!MyAttribute.MyXml.isEquipmentInfExists)
             {
              
                 MyAttribute.dtEquipmentParameter = FitEquipemtnInf();
                 MyAttribute.MyXml.FitEquipmentToXml(MyAttribute.dtEquipmentParameter);
             }

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


                if (this.RdbBert_MP1800.Checked)
                {
                    //MyEquipmentstruct.MyPPG = new MP1800PPG(MyAttribute.MylogManager);
                    //MyEquipmentstruct.MyED = new MP1800ED(MyAttribute.MylogManager);
                    MyEquipmentstruct.EquipementNameArray.Add("MP1800PPG");
                    MyEquipmentstruct.EquipementNameArray.Add("MP1800ED");
                }
                else if (this.RdbBert_N4960.Checked)
                {
                    //MyEquipmentstruct.MyPPG = new N4960PPG(MyAttribute.MylogManager);
                    //MyEquipmentstruct.MyED = new N4960ED(MyAttribute.MylogManager);
                    MyEquipmentstruct.EquipementNameArray.Add("N4960PPG");
                    MyEquipmentstruct.EquipementNameArray.Add("N4960ED");

                }
                else if (this.RDB_Innobert_v2.Checked)
                {
                    //MyEquipmentstruct.MyPPG = new Inno25GBert_V2_PPG(MyAttribute.MylogManager);
                    //MyEquipmentstruct.MyED = new Inno25GBert_V2_ED(MyAttribute.MylogManager);


                    MyEquipmentstruct.EquipementNameArray.Add("Inno25GBert_V2_PPG");
                    MyEquipmentstruct.EquipementNameArray.Add("Inno25GBert_V2_ED");


                }
                else
                {
                    MyEquipmentstruct.MyPPG = null;
                    MyEquipmentstruct.MyED = null;
                }

                if (this.Rdb_E3631.Checked)
                {
                  //  MyEquipmentstruct.MyPowersupply = new E3631(MyAttribute.MylogManager);

                    MyEquipmentstruct.EquipementNameArray.Add("E3631");

                }
                else if (this.Rdb_Pow_Null.Checked)
                {
                    MyEquipmentstruct.MyPowersupply = null;
                }
                else
                {
                    MyEquipmentstruct.MyPowersupply = null;
                }
                if (this.Rdb_Scope_86100.Checked)
                {
                    //  MyEquipmentstruct.MyPowersupply = new E3631(MyAttribute.MylogManager);

                    MyEquipmentstruct.EquipementNameArray.Add("FLEX86100");

                }
                else if (this.Rdb_Pow_Null.Checked)
                {
                    MyEquipmentstruct.MyScope = null;
                }
                else
                {
                    MyEquipmentstruct.MyScope = null;
                }

                if (this.Rdb_TPO4300.Checked)
                {
                  //  MyEquipmentstruct.MyTempControl = new TPO4300(MyAttribute.MylogManager);
                    //  MyEquipmentstruct.EquipmentList.Add("TPO4300", MyEquipmentstruct.MyTempControl);
                    MyEquipmentstruct.EquipementNameArray.Add("TPO4300");
                }
                else if (this.Rdb_TempControl_Null.Checked)
                {
                    MyEquipmentstruct.MyTempControl = null;
                }
                else
                {
                    MyEquipmentstruct.MyTempControl = null;
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
            MyEquipmentstruct.MyPPG = null;
            MyEquipmentstruct.MyDut = null;
            MyEquipmentstruct.MyED = null;
            MyEquipmentstruct.MyPowersupply = null;
            MyEquipmentstruct.MyTempControl = null;
        

            string CurrentEquipmentName = "";
            MyAttribute. FlagEquipmentConfigOK = true;

          //  UpdataRichBox refreshRichBox = new UpdataRichBox(UpdataShowLog);
            // UpdataDataTable refreshDataTable = new UpdataDataTable(RefreshDataGridView);

            DutStruct[] MyDutManufactureCoefficientsStructArray;
            DriverStruct[] MyManufactureChipsetStructArray;
            DriverInitializeStruct[] MyDutManufactureChipSetInitialize;
            DutEEPROMInitializeStuct[] MyDutEEPROMInitializeStuct;
            int i = 0;
            try
            {

                MyEquipmentstruct.MyDut = (DUT)MyEquipmentstruct.MyEquipmentManage.Createobject(MyAttribute.ProductionTypeName.ToUpper() + "DUT");
                MyEquipmentstruct.MyDut.deviceIndex = 0;
                MyEquipmentstruct.MyDut.ChipsetControll = false;

              //  MyDutManufactureCoefficientsStructArray = pflowControl.GetDutManufactureCoefficients();
              //  MyManufactureChipsetStructArray = pflowControl.GetManufactureChipsetControl();

                MyDutManufactureCoefficientsStructArray = GetDutManufactureCoefficients();
                MyManufactureChipsetStructArray = GetManufactureChipsetControl();
                MyDutManufactureChipSetInitialize = null; //等待数据库结构统一
                MyDutEEPROMInitializeStuct = null;
                MyEquipmentstruct.MyDut.Initialize(MyDutManufactureCoefficientsStructArray, MyManufactureChipsetStructArray, MyDutManufactureChipSetInitialize, MyDutEEPROMInitializeStuct, "");//等待Driver 跟上
               
                if (MyAttribute.IsFXP)
                {
                    MyEquipmentstruct.MyLightSourceDut = (DUT)MyEquipmentstruct.MyEquipmentManage.Createobject(MyAttribute.ProductionTypeName.ToUpper() + "DUT");
                    MyEquipmentstruct.MyLightSourceDut.deviceIndex = 1;
                    MyEquipmentstruct.MyLightSourceDut.ChipsetControll = false;

                    MyEquipmentstruct.MyLightSourceDut.Initialize(MyDutManufactureCoefficientsStructArray, MyManufactureChipsetStructArray, MyDutManufactureChipSetInitialize, MyDutEEPROMInitializeStuct, "");//等待Driver 跟上
               
                }
                //MyAttribute.pDut.Initialize(MyDutManufactureCoefficientsStructArray, MyManufactureChipsetStructArray, MyAttribute.StrAuxAttribles);
               // MyEquipmentstruct.EquipmentList.Clear();
                Algorithm algorithm = new Algorithm();
                for (i = 0; i < MyEquipmentstruct.EquipementNameArray.Count; i++)
                {
                    TestModeEquipmentParameters[] CurrentEquipmentStruct;
                        // string[] StrArray = EquipmenNameArray[i, 0].Split('_');
                   CurrentEquipmentName = MyEquipmentstruct.EquipementNameArray[i].ToString().ToUpper();

                   CurrentEquipmentStruct = MyAttribute.MyXml.GetEquipmenParameter(CurrentEquipmentName);

                    EquipmentBase CurrentEquipmentObject = (EquipmentBase)MyEquipmentstruct.MyEquipmentManage.Createobject(CurrentEquipmentName);
                  //  CurrentEquipmentObject.Role = Convert.ToByte(dtEquipmenList.Rows[i]["Role"]);// 0=NA,1=TX,2=RX

                    UpDataStatus(labelShow, CurrentEquipmentName + "  Configing.....");

                   // if (!CurrentEquipmentObject.Initialize(CurrentEquipmentStruct) || !CurrentEquipmentObject.Configure(1))
                   if (!CurrentEquipmentObject.Initialize(CurrentEquipmentStruct))
                    {
                        MessageBox.Show(CurrentEquipmentName + "Configure Error");
                       MyAttribute. FlagEquipmentConfigOK = false;

                       Exception ex = new Exception(CurrentEquipmentName + "Configure Error");
                        throw ex;
                    }
                    else
                    {
                        MyEquipmentstruct.EquipmentList.Add(CurrentEquipmentName, CurrentEquipmentObject);


                        if(CurrentEquipmentName.Contains("PPG"))
                        {
                            MyEquipmentstruct.MyPPG =(PPG) CurrentEquipmentObject;
                        }
                        else if (CurrentEquipmentName.Contains("ED"))
                        {
                            MyEquipmentstruct.MyED = (ErrorDetector)CurrentEquipmentObject;

                        }
                        else if (CurrentEquipmentName.Contains("E3631"))
                        {
                            MyEquipmentstruct.MyPowersupply = (Powersupply)CurrentEquipmentObject;

                        }
                        else if (CurrentEquipmentName.Contains("TPO4300"))
                        {
                            MyEquipmentstruct.MyTempControl = (Thermocontroller)CurrentEquipmentObject;

                        }
                        else if (CurrentEquipmentName.Contains("FLEX86100"))
                        {
                            MyEquipmentstruct.MyScope = (Scope)CurrentEquipmentObject;

                        }
                        else if (this.RDB_Innobert_v2.Checked)
                        {
                            //MyEquipmentstruct.MyPPG = new Inno25GBert_V2_PPG(MyAttribute.MylogManager);
                            //MyEquipmentstruct.MyED = new Inno25GBert_V2_ED(MyAttribute.MylogManager);


                            MyEquipmentstruct.EquipementNameArray.Add("Inno25GBert_V2_PPG");
                            MyEquipmentstruct.EquipementNameArray.Add("Inno25GBert_V2_ED");


                        }
                       

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
        public DriverStruct[] GetManufactureChipsetControl()
        {
            DriverStruct[] MyDriverStruct;
            int i = 0;
            // protected DutStruct[] MyDutStruct;
            DataTable DtMyDutInf = new DataTable();

            string StrTableName = "GlobalManufactureChipsetControl";
          
            string StrSelectconditions = "select * from " + StrTableName + " where PID= " + MyAttribute.IdPnName + " order by ID";
            DtMyDutInf = MyAttribute.MyDataio.GetDataTable(StrSelectconditions, StrTableName);// 获得环境的DataTable

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

        private bool GetEquipmentInf()
        {

            for (int i = 0; i < MyEquipmentstruct.EquipmentList.Count;i++ )
            {
                MyAttribute.MyXml.GetEquipmenParameter(MyEquipmentstruct.EquipmentList.Keys[i].ToString());
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
                    MyEquipmentstruct.MyPowersupply.OutPutSwitch(true);
                    MyEquipmentstruct.MyDut.FullFunctionEnable();

                    if (!JudgeSerialNO()) return false;//判定序列号是否正常
                  //  TimeSpanUse.DateDiff(MyAttribute.TimeSpanStart, MyAttribute.MyLogManager, "Prepare Time for Test -JudgeSerialNO ", out MyAttribute.TimeSpanStart);

                    if (!MyEquipmentstruct.MyDut.FullFunctionEnable()) return false;

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
                MyAttribute.Current_DutFwRev = MyEquipmentstruct.MyDut.ReadFWRev().ToString().ToUpper();
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
                MyAttribute.CurrentSN = MyEquipmentstruct.MyDut.ReadSn().Trim();
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

        private void ChangeTemp()
        {
            int Step = 0;

            if (MyCondition.TempStep == 0)
            {
            }
            else
            {
                Step = Convert.ToInt16((MyCondition.EndTemp - MyCondition.StartTemp) / MyCondition.TempStep);

                if (Step < 0)
                {
                    MyCondition.TempStep = MyCondition.TempStep * -1;
                    Step = Step * -1;
                }
            }

                for (int j = 0; j < Step + 1; j++)
                {
                    MyCondition.CurrentTemp = MyCondition.StartTemp + j * MyCondition.TempStep;


                    UpDataStatus(this.labelShow, "Vcc=" + MyCondition.CurrentVcc + "*Temp=" + MyCondition.CurrentTemp);

                    if (MyEquipmentstruct.MyTempControl != null)
                    {
                        MyEquipmentstruct.MyTempControl.SetPointTemp(MyCondition.CurrentTemp);
                    }

                    if (MyAttribute.StopFlag)
                    {
                        MyAttribute.TotalResultFlag = false;
                        break;

                    }

                    Thread.Sleep(MyCondition.StayTime * 1000);

                    if (MyAttribute.StopFlag)
                    {
                        MyAttribute.TotalResultFlag = false;
                        break;

                    }

                }
            
            MyAttribute.ChangeTempEndflag = true;
        }

        private void ReadDut_Inf()
        {
            int K= 0;

            while (!MyAttribute.ChangeTempEndflag)
            {


                MyAttribute.dtCurrentConditionResultData = new DataTable();
                MyAttribute.dtCurrentConditionResultData = MyAttribute.dtTotalTestData.Clone();

                DataRow dr = MyAttribute.dtCurrentConditionResultData.NewRow();


                dr["NO"] = MyAttribute.CurrentSN;
                dr["FwVer"] = MyAttribute.Current_DutFwRev;
                dr["TestTime"] = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"); 
                //TestTime
                dr["Temp"] = MyCondition.CurrentTemp;
                dr["Vcc"] = MyCondition.CurrentVcc;
                if (MyCondition.Polarity)
                {
                    dr["Polarity"] = "+";
                }
                else
                {
                    dr["Polarity"] = "-";
                }
             
                dr["DmiTemp"] = MyEquipmentstruct.MyDut.ReadDmiTemp();
                dr["DmiVcc"] = MyEquipmentstruct.MyDut.ReadDmiVcc();
                if (MyEquipmentstruct.MyScope != null)
                {

                    dr["TxPower_uw"] = MyEquipmentstruct.MyScope.GetAveragePowerWatt();
                }
              

                double[] ErrorArray = MyEquipmentstruct.MyED.RapidErrorCount_AllCH();

                for (int i = 1; i < MyAttribute.TotalChannnel + 1; i++)
                {
                    MyEquipmentstruct.MyDut.ChangeChannel(i.ToString());
                    double TxPowerDmi = MyEquipmentstruct.MyDut.ReadDmiTxp();
                  // double RxPowerDmi = MyEquipmentstruct.MyDut.ReadDmiRxp();

                    if (double.IsInfinity(TxPowerDmi))
                    {
                        TxPowerDmi = -100;
                    }

                    dr["DmiTx" + i] = TxPowerDmi;

                   
                    dr["DmiIBias" + i] = MyEquipmentstruct.MyDut.ReadDmiBias();
                    dr["ECount" + i] = ErrorArray[i - 1];
                    ushort TxpowADC, RxpowADC;
                    MyEquipmentstruct.MyDut.ReadTxpADC(out TxpowADC);
                  


                    if(MyAttribute.IsFXP)
                    {
                        MyEquipmentstruct.MyLightSourceDut.ReadRxpADC(out RxpowADC);
                        dr["RxPADC_LS" + i] = RxpowADC;
                        dr["DmiRx_LS" + i] = MyEquipmentstruct.MyLightSourceDut.ReadDmiRxp();
                        
                    }
                    else
                    {
                        MyEquipmentstruct.MyDut.ReadRxpADC(out RxpowADC);
                         dr["RxPADC" + i] = RxpowADC;
                         dr["DmiRx" + i] = MyEquipmentstruct.MyDut.ReadDmiRxp();
                        // MyEquipmentstruct.MyDut.ReadRxpADC(out RxpowADC);
                    }
                    dr["TxPADC" + i] = TxpowADC;
                  

                    dr["BiasDAC" + i] = MyEquipmentstruct.MyDut.ReadBiasDac();
                    dr["ModDAC" + i] = MyEquipmentstruct.MyDut.ReadModDac();
                  

                }

                MyAttribute.dtCurrentConditionResultData.Rows.Add(dr.ItemArray);


                string[] TestDataArray = Array.ConvertAll<object, string>(MyAttribute.dtCurrentConditionResultData.Rows[0].ItemArray, Convert.ToString);
                MyAttribute.MyOperateTxT.WriteTxt(TestDataArray);
                MyAttribute.dtTotalTestData.Merge(MyAttribute.dtCurrentConditionResultData);

                int EXtralRow=0;

                while (MyAttribute.dtTotalTestData.Rows.Count>10)
                {
                    if (MyAttribute.dtTotalTestData.Rows.Count == 10) break;

                     MyAttribute.dtTotalTestData.Rows.RemoveAt(EXtralRow);
                     EXtralRow++;
                }
               


                Thread.Sleep(MyAttribute.CycleTime * 1000);
                
            }
            MyAttribute.ConditionEndflag = true;
          //  return MyAttribute.dtCurrentConditionResultData;

        }

        private bool SetCondition()
        {

            if (MyEquipmentstruct.MyPowersupply != null)
            {
                MyEquipmentstruct.MyPowersupply.ConfigVoltageCurrent(MyCondition.CurrentVcc.ToString());
            }

           

            if (MyEquipmentstruct.MyTempControl!=null)
            {
                MyEquipmentstruct.MyTempControl.SetPointTemp(MyCondition.CurrentTemp);
            }

            return true;
        }

        #endregion

        #region Control Funtion


        private void TempCycle_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Environment.Exit(System.Environment.ExitCode);
            this.Dispose();
            this.Close();
        }

      

        private void button_SaveCondition_Click(object sender, EventArgs e)
        {
            if (MyCondition.ConditionFileName != textBoxFileName.Text.Trim())
            {
                MyCondition.StrPath = "";
                MyCondition.ConditionFileName = textBoxFileName.Text.Trim();
            }

            if (MyCondition.StrPath != "" && MyCondition.StrPath!=null)
            {

                if (File.Exists(@MyCondition.StrPath))
                {
                    //如果存在则删除
                    File.Delete(@MyCondition.StrPath);
                }
            }
            else
            {
             
                MyCondition.StrPath = Application.StartupPath + "\\" + MyCondition.ConditionFileName + ".xml";
                MyCondition.ConditionFileName = textBoxFileName.Text.Trim();
            }
             MyCondition.MyXml = new TempCycleXml(MyCondition.StrPath, 1);

   

                MyAttribute.ConditionCycle = Convert.ToInt16(ConditionCycle.Text);
               
                ConditionCycle.Text = "1";
                this.dataGridView_Condition.AllowUserToAddRows = false;
                dataGridView_Condition.Refresh();
                // MyCondition.dtCondition 

                DataTable dt = GetDgvToTable(this.dataGridView_Condition);

                MyCondition.dtCondition.Clear();
                for (int i = 0; i < MyAttribute.ConditionCycle; i++)
                {
                    MyCondition.dtCondition.Merge(dt);
                }

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
                    MyCondition.MyXml.FitConditionInfToXml(i.ToString(), MyCondition.ConditionHeardArray, ConditionArray);
                }
               // MyCondition.dtCondition
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
            this.button_Add.Location = new Point(this.button_Add.Location.X -3, this.button_Add.Location.Y + 3);
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
            this.button_Test.Location = new Point(this.button_Test.Location.X - 3, this.button_Test.Location.Y +3);
        }

        private void buttonStop_MouseEnter(object sender, EventArgs e)
        {
            this.buttonStop.Location = new Point(this.buttonStop.Location.X + 3, this.buttonStop.Location.Y - 3);
        }

        private void buttonStop_MouseLeave(object sender, EventArgs e)
        {
            this.buttonStop.Location = new Point(this.buttonStop.Location.X - 3, this.buttonStop.Location.Y +3);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.textBoxSN.Text = MyEquipmentstruct.MyDut.ReadSn();
            this.textBoxSN.Refresh();

            this.textBoxLS_SN.Text = MyEquipmentstruct.MyLightSourceDut.ReadSn();
            this.textBoxLS_SN.Refresh();
        }

        private void chk_FXP_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_FXP.Checked)
            {
                MyAttribute.IsFXP = true;
            }
            else
            {
                MyAttribute.IsFXP = false;
            }
        }

        private void button_loadCondition_Click(object sender, EventArgs e)
        {
            MyCondition.StrPath = GetConditionXml();
            string S = Path.GetFileName(MyCondition.StrPath);
            string[] SA=S.Split('.');
            string FileName = SA[0];
            MyCondition.ConditionFileName = FileName;
            this.textBoxFileName.Text = FileName;
            this.textBoxFileName.Refresh();

            if (MyCondition.StrPath!="")
            {
               MyCondition.MyXml = new TempCycleXml(MyCondition.StrPath, 1);
            }
            MyCondition.dtCondition = MyCondition.MyXml.GetConditionTable(MyCondition.dtCondition);
            this.dataGridView_Condition.DataSource = MyCondition.dtCondition;
            this.dataGridView_Condition.Refresh();
        }

        #endregion

        #region TransferTest

        private void Test()
        {
            try
            {

                if (this.rb_transfer.Checked)
                {
                    TransferTest();
                }
                else
                {
                    TrackErrorTest();
                }

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
            MyAttribute.dtTotalTestData.Columns.Add("Polarity");
            MyAttribute.dtTotalTestData.Columns.Add("Vcc");
            MyAttribute.dtTotalTestData.Columns.Add("Temp");
            MyAttribute.dtTotalTestData.Columns.Add("DmiVcc");
            MyAttribute.dtTotalTestData.Columns.Add("DmiTemp");


            MyAttribute.dtTotalTestData.Columns.Add("TxPower_uw");


            for (int i = 1; i < MyAttribute.TotalChannnel + 1; i++)
            {
                MyAttribute.dtTotalTestData.Columns.Add("DmiIBias" + i);

            }
            for (int i = 1; i < MyAttribute.TotalChannnel + 1; i++)
            {
                MyAttribute.dtTotalTestData.Columns.Add("DmiTx" + i);
            }
            for (int i = 1; i < MyAttribute.TotalChannnel + 1; i++)
            {
                MyAttribute.dtTotalTestData.Columns.Add("TxPAdc" + i);
            }

            if (MyAttribute.IsFXP)
            {
                for (int i = 1; i < MyAttribute.TotalChannnel + 1; i++)
                {
                    MyAttribute.dtTotalTestData.Columns.Add("DmiRx_LS" + i);
                }
                for (int i = 1; i < MyAttribute.TotalChannnel + 1; i++)
                {
                    MyAttribute.dtTotalTestData.Columns.Add("RxPAdc_LS" + i);
                }
            }
            else
            {
                for (int i = 1; i < MyAttribute.TotalChannnel + 1; i++)
                {
                    MyAttribute.dtTotalTestData.Columns.Add("DmiRx" + i);
                }
                for (int i = 1; i < MyAttribute.TotalChannnel + 1; i++)
                {
                    MyAttribute.dtTotalTestData.Columns.Add("RxPAdc" + i);
                }
            }
            for (int i = 1; i < MyAttribute.TotalChannnel + 1; i++)
            {
                MyAttribute.dtTotalTestData.Columns.Add("ECount" + i);
            }

            for (int i = 1; i < MyAttribute.TotalChannnel + 1; i++)
            {
                MyAttribute.dtTotalTestData.Columns.Add("BiasDAC" + i);
            }
            for (int i = 1; i < MyAttribute.TotalChannnel + 1; i++)
            {
                MyAttribute.dtTotalTestData.Columns.Add("ModDAC" + i);
            }
            return true;
        }

        public bool TransferTest()
        {

            #region      进入测试前的准备

            MyAttribute.StopFlag = false;

            MyAttribute.dtTotalTestData.Clear();

            MyAttribute.prosess = 0;

            #endregion

            try
            {
                for (int J = 0; J < MyAttribute.ArrayPolarity.Count; J++)
                {

                    if (Convert.ToInt16(MyAttribute.ArrayPolarity[J]) == 1)
                    {
                        MyCondition.Polarity = true;
                    }
                    else
                    {
                        MyCondition.Polarity = false;
                    }

                    if (MyEquipmentstruct.MyPPG != null)
                    {
                        MyEquipmentstruct.MyPPG.ConfigureOTxPolarity(MyCondition.Polarity);

                    }
                    if (MyEquipmentstruct.MyED != null)
                    {
                        MyEquipmentstruct.MyED.ConfigureERxPolarity(MyCondition.Polarity);
                        MyEquipmentstruct.MyED.RapidErrorCount_AllCH(1, true);//只为清空五码
                    }


                    //if (MyCondition.LastPolarity != MyCondition.Polarity)
                    //{
                    //    MyCondition.IsNeedSetPolarity = true;
                    //}
                    //else
                    //{
                    //    MyCondition.IsNeedSetPolarity = false;
                    //}

                    #region Condition 遍历Conditions

                    for (int i = 0; i < MyCondition.dtCondition.Rows.Count; i++)//遍历测试环境条件
                    {

                        #region           对于当前FlwoControl数据的解析和Log记载

                        DataRow drCondition = MyCondition.dtCondition.Rows[i];

                        MyCondition.CurrentTemp = Convert.ToDouble(drCondition["StartTemp"]);
                        MyCondition.StartTemp = Convert.ToDouble(drCondition["StartTemp"]);
                        MyCondition.EndTemp = Convert.ToDouble(drCondition["EndTemp"]);
                        MyCondition.TempStep = Convert.ToDouble(drCondition["TempStep"]);
                        MyCondition.CurrentVcc = Convert.ToDouble(drCondition["Vcc"]);
                        //  MyCondition.Polarity = Convert.ToBoolean(drCondition["Polarity"]);

                        //if (MyCondition.Polarity.ToString() == "1")
                        //{
                        //    MyCondition.Polarity = true;
                        //}
                        //else
                        //{
                        //    MyCondition.Polarity = false;
                        //}

                        MyCondition.StayTime = Convert.ToInt16(drCondition["StayTime"]);

                        MyCondition.CurrentTemp = MyCondition.StartTemp;



                        //if (i == 0)
                        //{
                        //    MyEquipmentstruct.MyED.RapidErrorCount_AllCH(1, true);//只为清空五码
                        //    MyCondition.LastPolarity = MyCondition.Polarity;
                        //    MyCondition.IsNeedSetPolarity = true;
                        //}
                        //else
                        //{
                        //    if (MyCondition.LastPolarity != MyCondition.Polarity)
                        //    {
                        //        MyCondition.IsNeedSetPolarity = true;
                        //    }
                        //    else
                        //    {
                        //        MyCondition.IsNeedSetPolarity = false;
                        //    }

                        //}

                        if (MyEquipmentstruct.MyScope != null)
                        {

                            //MyEquipmentstruct.MyScope.ChangeChannel("1");
                            MyEquipmentstruct.MyScope.DisplayPowerWatt();

                        }
                        SetCondition();

                        if (i == 0)
                        {

                            Thread.Sleep(10 * 1000);
                        }


                        MyAttribute.ConditionEndflag = false;
                        MyAttribute.ChangeTempEndflag = false;

                        Task TempT1 = new Task(ChangeTemp);
                        TempT1.Start();

                        Task T2 = new Task(ReadDut_Inf);
                        T2.Start();

                        Task.WaitAll(TempT1, T2);

                        if (TempT1.IsCompleted)
                        {
                            TempT1.Dispose();


                        }
                        if (T2.IsCompleted)
                        {
                            T2.Dispose();
                        }

                        Thread.Sleep(2000);

                        GC.Collect();

                        #endregion

                    Error:

                        #region 将当前数据填充到TotalTestData,并且完成当前Condition的数据存档



                        MyAttribute.prosess = (i + 1) * 100 / (MyCondition.dtCondition.Rows.Count);


                        if (MyAttribute.StopFlag)
                        {
                            MyAttribute.TotalResultFlag = false;
                            break;
                        }

                        #endregion
                    }
                    if (!MyAttribute.StopFlag)
                    {
                        MyAttribute.TotalResultFlag = true;

                    }

                    #endregion
                }

                if (MyEquipmentstruct.MyTempControl != null)
                {
                    MyEquipmentstruct.MyTempControl.SetPositionUPDown("0");

                }
                for (int k = 0; k < MyEquipmentstruct.EquipmentList.Count; k++)
                {
                    MyEquipmentstruct.EquipmentList.Values[k].Referenced_Times = 0;
                }

                MyAttribute.MylogManager.AdapterLogString(1, "Test End------------------------");
                MyAttribute.FlagFlowControllEnd = true;

                return true;
            }
            catch (Exception ex)
            {

                MyAttribute.MylogManager.AdapterLogString(1, "Test End------------------------");
                MyAttribute.FlagFlowControllEnd = true;
                return false;
            }

        }
        #endregion


        #region TrackError

        private void HT_DAC_Calculate(double CurrentTempLD,double TempCase,out int BiasDAC,out int ModDAC)
        {
            double TempValue;
            TempValue = RT_Input_Parameter.DAC_Bias * (1 + HT_Output_Parameter.SeBias * (CurrentTempLD - RT_Input_Parameter.TempLD)) * (1 - HT_Output_Parameter.TE * (TempCase - RT_Input_Parameter.TempCase));
            BiasDAC = Convert.ToInt16(TempValue);

            TempValue = RT_Input_Parameter.DAC_Mod * (1 + HT_Output_Parameter.SeMod * (CurrentTempLD - RT_Input_Parameter.TempLD)) * (1 - HT_Output_Parameter.TE * (TempCase - RT_Input_Parameter.TempCase));
            ModDAC = Convert.ToInt16(TempValue);
         }
        private void LT_DAC_Calculate(double CurrentTempLD, double TempCase, out int BiasDAC, out int ModDAC)
        {
            double TempValue;
            TempValue = RT_Input_Parameter.DAC_Bias * (1 + LT_Output_Parameter.SeBias * (CurrentTempLD - RT_Input_Parameter.TempLD)) * (1 - LT_Output_Parameter.TE * (TempCase - RT_Input_Parameter.TempCase));
            //  HT_Output_Parameter.BiasDAC = Convert.ToInt16(d);
            BiasDAC = Convert.ToInt16(TempValue);
            TempValue = RT_Input_Parameter.DAC_Mod * (1 + LT_Output_Parameter.SeMod * (CurrentTempLD - RT_Input_Parameter.TempLD)) * (1 - LT_Output_Parameter.TE * (TempCase - RT_Input_Parameter.TempCase));
            ModDAC = Convert.ToInt16(TempValue);
        }





        

        private void BT_HT_Calculate_Click(object sender, EventArgs e)
        {
            this.HT_Text_TE.Text = ""; this.HT_Text_TE.Refresh();

            this.HT_Text_SeBias.Text = ""; this.HT_Text_SeBias.Refresh();
            this.HT_Text_SeMod.Text = ""; this.HT_Text_SeMod.Refresh();

            this.HT_Text_ModDAC.Text = ""; this.HT_Text_ModDAC.Refresh();
            this.HT_Text_Bias_dac.Text = ""; this.HT_Text_Bias_dac.Refresh();


            if (!CheckInputParameter_HT() || !CheckInputParameter_RT())
            {
                MessageBox.Show("请检查高温以及常温输入参数");
            }
            else
            {
                HT_Output_Parameter.TE = (HT_Input_Parameter.AP / RT_Input_Parameter.AP - HT_Input_Parameter.TxpowADC / RT_Input_Parameter.TxpowADC) / (HT_Input_Parameter.TempCase - RT_Input_Parameter.TempCase);


                HT_Output_Parameter.SeBias = (HT_Input_Parameter.DAC_Bias / RT_Input_Parameter.DAC_Bias - HT_Input_Parameter.TxpowADC / RT_Input_Parameter.TxpowADC) / (HT_Input_Parameter.TempLD - RT_Input_Parameter.TempLD);

                HT_Output_Parameter.SeMod = (HT_Input_Parameter.DAC_Mod / RT_Input_Parameter.DAC_Mod - HT_Input_Parameter.TxpowADC / RT_Input_Parameter.TxpowADC) / (HT_Input_Parameter.TempLD - RT_Input_Parameter.TempLD);

                //double d = RT_Input_Parameter.DAC_Bias * (1 + HT_Output_Parameter.SeBias * (HT_Input_Parameter.TempLD - RT_Input_Parameter.TempLD)) * (1 - HT_Output_Parameter.TE * (HT_Input_Parameter.TempCase - RT_Input_Parameter.TempCase));
                //HT_Output_Parameter.BiasDAC = Convert.ToInt16(d);

                //double d2 = RT_Input_Parameter.DAC_Mod * (1 + HT_Output_Parameter.SeMod * (HT_Input_Parameter.TempLD - RT_Input_Parameter.TempLD)) * (1 - HT_Output_Parameter.TE * (HT_Input_Parameter.TempCase - RT_Input_Parameter.TempCase));
                //HT_Output_Parameter.ModDAC = Convert.ToInt16(d2);

                HT_DAC_Calculate(HT_Input_Parameter.TempLD, HT_Input_Parameter.TempCase, out  HT_Output_Parameter.BiasDAC, out HT_Output_Parameter.ModDAC);


                this.HT_Text_TE.Text = HT_Output_Parameter.TE.ToString(); this.HT_Text_TE.Refresh();

                this.HT_Text_SeBias.Text = HT_Output_Parameter.SeBias.ToString(); this.HT_Text_SeBias.Refresh();
                this.HT_Text_SeMod.Text = HT_Output_Parameter.SeMod.ToString(); this.HT_Text_SeMod.Refresh();

                this.HT_Text_ModDAC.Text = HT_Output_Parameter.ModDAC.ToString(); this.HT_Text_ModDAC.Refresh();
                this.HT_Text_Bias_dac.Text = HT_Output_Parameter.BiasDAC.ToString(); this.HT_Text_Bias_dac.Refresh();

            }






        }

        private void BT_LT_Calculate_Click(object sender, EventArgs e)
        {

            this.LT_Text_TE.Text = ""; this.LT_Text_TE.Refresh();

            this.LT_Text_SeBias.Text = ""; this.LT_Text_SeBias.Refresh();
            this.LT_Text_SeMod.Text = ""; this.LT_Text_SeMod.Refresh();

            this.LT_Text_ModDAC.Text = ""; this.LT_Text_ModDAC.Refresh();
            this.LT_Text_Bias_dac.Text = ""; this.LT_Text_Bias_dac.Refresh();


            if (!CheckInputParameter_LT() || !CheckInputParameter_RT())
            {
                MessageBox.Show("请检查高温以及常温输入参数");
            }
            else
            {
                LT_Output_Parameter.TE = (LT_Input_Parameter.AP / RT_Input_Parameter.AP - LT_Input_Parameter.TxpowADC / RT_Input_Parameter.TxpowADC) / (LT_Input_Parameter.TempCase - RT_Input_Parameter.TempCase);


                LT_Output_Parameter.SeBias = (LT_Input_Parameter.DAC_Bias / RT_Input_Parameter.DAC_Bias - LT_Input_Parameter.TxpowADC / RT_Input_Parameter.TxpowADC) / (LT_Input_Parameter.TempLD - RT_Input_Parameter.TempLD);

                LT_Output_Parameter.SeMod = (LT_Input_Parameter.DAC_Mod / RT_Input_Parameter.DAC_Mod - LT_Input_Parameter.TxpowADC / RT_Input_Parameter.TxpowADC) / (LT_Input_Parameter.TempLD - RT_Input_Parameter.TempLD);

                //double d = RT_Input_Parameter.DAC_Bias * (1 + LT_Output_Parameter.SeBias * (LT_Input_Parameter.TempLD - RT_Input_Parameter.TempLD)) * (1 - LT_Output_Parameter.TE) * (LT_Input_Parameter.TempCase - RT_Input_Parameter.TempCase);
                //LT_Output_Parameter.BiasDAC = Convert.ToInt16(d);

                //double d2 = RT_Input_Parameter.DAC_Mod * (1 + LT_Output_Parameter.SeMod * (LT_Input_Parameter.TempLD - RT_Input_Parameter.TempLD)) * (1 - LT_Output_Parameter.TE) * (LT_Input_Parameter.TempCase - RT_Input_Parameter.TempCase);
                //LT_Output_Parameter.ModDAC = Convert.ToInt16(d2);

                LT_DAC_Calculate(LT_Input_Parameter.TempLD, LT_Input_Parameter.TempCase, out  LT_Output_Parameter.BiasDAC, out LT_Output_Parameter.ModDAC);


                this.LT_Text_TE.Text = LT_Output_Parameter.TE.ToString(); this.LT_Text_TE.Refresh();

                this.LT_Text_SeBias.Text = LT_Output_Parameter.SeBias.ToString(); this.LT_Text_SeBias.Refresh();
                this.LT_Text_SeMod.Text = LT_Output_Parameter.SeMod.ToString(); this.LT_Text_SeMod.Refresh();

                this.LT_Text_ModDAC.Text = LT_Output_Parameter.ModDAC.ToString(); this.LT_Text_ModDAC.Refresh();
                this.LT_Text_Bias_dac.Text = LT_Output_Parameter.BiasDAC.ToString(); this.LT_Text_Bias_dac.Refresh();

            }



        }

        private bool CheckInputParameter_HT()
        {
            HT_Input_Parameter = new InputParameter();

            if (this.HT_textBox_AP.Text.Trim() != "")
            {
                HT_Input_Parameter.AP = Convert.ToDouble(this.HT_textBox_AP.Text.Trim());
            }
            else
            {
                return false;
            }

            if (this.HT_textBox_ER.Text.Trim() != "")
            {
                HT_Input_Parameter.ER = Convert.ToDouble(this.HT_textBox_ER.Text.Trim());
            }
            else
            {
                return false;
            }

            if (this.HT_textBox_DC.Text.Trim() != "")
            {
                HT_Input_Parameter.DAC_Bias = Convert.ToDouble(this.HT_textBox_DC.Text.Trim());
            }
            else
            {
                return false;
            }

            if (this.HT_textBox_MOD.Text.Trim() != "")
            {
                HT_Input_Parameter.DAC_Mod = Convert.ToDouble(this.HT_textBox_MOD.Text.Trim());
            }
            else
            {
                return false;
            }

            if (this.HT_textBox_TxpADC.Text.Trim() != "")
            {
                HT_Input_Parameter.TxpowADC = Convert.ToDouble(this.HT_textBox_TxpADC.Text.Trim());
            }
            else
            {
                return false;
            }

            if (this.HT_textBox_Tcase.Text.Trim() != "")
            {
                HT_Input_Parameter.TempCase = Convert.ToDouble(this.HT_textBox_Tcase.Text.Trim());
            }
            else
            {
                return false;
            }

            if (this.HT_textBox_textBox_TLD.Text.Trim() != "")
            {
                HT_Input_Parameter.TempLD = Convert.ToDouble(this.HT_textBox_textBox_TLD.Text.Trim());
            }
            else
            {
                return false;
            }



            return true;
        }

        private bool CheckInputParameter_RT()
        {
            RT_Input_Parameter = new InputParameter();

            if (this.RT_textBox_AP.Text.Trim() != "")
            {
                RT_Input_Parameter.AP = Convert.ToDouble(this.RT_textBox_AP.Text.Trim());
            }
            else
            {
                return false;
            }

            if (this.RT_textBox_ER.Text.Trim() != "")
            {
                RT_Input_Parameter.ER = Convert.ToDouble(this.RT_textBox_ER.Text.Trim());
            }
            else
            {
                return false;
            }

            if (this.RT_textBox_DC.Text.Trim() != "")
            {
                RT_Input_Parameter.DAC_Bias = Convert.ToDouble(this.RT_textBox_DC.Text.Trim());
            }
            else
            {
                return false;
            }

            if (this.RT_textBox_MOD.Text.Trim() != "")
            {
                RT_Input_Parameter.DAC_Mod = Convert.ToDouble(this.RT_textBox_MOD.Text.Trim());
            }
            else
            {
                return false;
            }

            if (this.RT_textBox_TxpADC.Text.Trim() != "")
            {
                RT_Input_Parameter.TxpowADC = Convert.ToDouble(this.RT_textBox_TxpADC.Text.Trim());
            }
            else
            {
                return false;
            }

            if (this.RT_textBox_Tcase.Text.Trim() != "")
            {
                RT_Input_Parameter.TempCase = Convert.ToDouble(this.RT_textBox_Tcase.Text.Trim());
            }
            else
            {
                return false;
            }

            if (this.RT_textBox_textBox_TLD.Text.Trim() != "")
            {
                RT_Input_Parameter.TempLD = Convert.ToDouble(this.RT_textBox_textBox_TLD.Text.Trim());
            }
            else
            {
                return false;
            }



            return true;
        }

        private bool CheckInputParameter_LT()
        {
            LT_Input_Parameter = new InputParameter();

            if (this.LT_textBox_AP.Text.Trim() != "")
            {
                LT_Input_Parameter.AP = Convert.ToDouble(this.LT_textBox_AP.Text.Trim());
            }
            else
            {
                return false;
            }

            if (this.LT_textBox_ER.Text.Trim() != "")
            {
                LT_Input_Parameter.ER = Convert.ToDouble(this.LT_textBox_ER.Text.Trim());
            }
            else
            {
                return false;
            }

            if (this.LT_textBox_DC.Text.Trim() != "")
            {
                LT_Input_Parameter.DAC_Bias = Convert.ToDouble(this.LT_textBox_DC.Text.Trim());
            }
            else
            {
                return false;
            }

            if (this.LT_textBox_MOD.Text.Trim() != "")
            {
                LT_Input_Parameter.DAC_Mod = Convert.ToDouble(this.LT_textBox_MOD.Text.Trim());
            }
            else
            {
                return false;
            }

            if (this.LT_textBox_TxpADC.Text.Trim() != "")
            {
                LT_Input_Parameter.TxpowADC = Convert.ToDouble(this.LT_textBox_TxpADC.Text.Trim());
            }
            else
            {
                return false;
            }

            if (this.LT_textBox_Tcase.Text.Trim() != "")
            {
                LT_Input_Parameter.TempCase = Convert.ToDouble(this.LT_textBox_Tcase.Text.Trim());
            }
            else
            {
                return false;
            }

            if (this.LT_textBox_textBox_TLD.Text.Trim() != "")
            {
                LT_Input_Parameter.TempLD = Convert.ToDouble(this.LT_textBox_textBox_TLD.Text.Trim());
            }
            else
            {
                return false;
            }



            return true;
        }

        private void button_Run_Click(object sender, EventArgs e)
        {
            MyAttribute.dtTrackError = new DataTable();
            MyCondition.dtCondition = (DataTable)dataGridView_Condition.DataSource;
            MyAttribute.CycleTime = int.Parse(this.textBox_CycleTime.Text);


            this.labelShow.Text = "ReadyForTest.....";
            this.labelShow.Refresh();

            if (ReadyForTest())
            {
                // MyAttribute


                this.labelShow.Text = "First Temp Waiting.....";
                this.labelShow.Refresh();

                MyAttribute.MylogManager.AdapterLogString(0, "***********************  SN=" + MyAttribute.CurrentSN + " ************************");
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

                if (MyEquipmentstruct.MyTempControl != null)
                {
                    MyEquipmentstruct.MyTempControl.SetPositionUPDown("0");

                }
                progress.Value = 0;
                progress.Refresh();

                panelCombox.Enabled = true;

            }
        }

        public bool TrackErrorTest()
        {
            #region      进入测试前的准备

            MyAttribute.StopFlag = false;

            MyAttribute.dtTotalTrackErrorData.Clear();

         

            MyAttribute.prosess = 0;

            #endregion

            try
            {
                


                    #region Condition 遍历Conditions

                    for (int i = 0; i < MyCondition.dtCondition.Rows.Count; i++)//遍历测试环境条件
                    {

                        #region           对于当前FlwoControl数据的解析和Log记载

                        DataRow drCondition = MyCondition.dtCondition.Rows[i];

                        MyCondition.CurrentTemp = Convert.ToDouble(drCondition["StartTemp"]);
                        MyCondition.StartTemp = Convert.ToDouble(drCondition["StartTemp"]);
                        MyCondition.EndTemp = Convert.ToDouble(drCondition["EndTemp"]);
                        MyCondition.TempStep = Convert.ToDouble(drCondition["TempStep"]);
                        MyCondition.CurrentVcc = Convert.ToDouble(drCondition["Vcc"]);


                        MyCondition.StayTime = Convert.ToInt16(drCondition["StayTime"]);

                        MyCondition.CurrentTemp = MyCondition.StartTemp;


                        if (MyEquipmentstruct.MyScope != null)
                        {

                            MyEquipmentstruct.MyScope.DisplayPowerWatt();

                        }

                        SetCondition();

                        if (i == 0)
                        {

                            Thread.Sleep(10 * 1000);
                        }


                        MyAttribute.ConditionEndflag = false;
                        MyAttribute.ChangeTempEndflag = false;

                        Task TempT1 = new Task(ChangeTemp);
                        TempT1.Start();

                        Task T2 = new Task(CalculateTrackError);
                        T2.Start();

                        Task.WaitAll(TempT1, T2);

                        if (TempT1.IsCompleted)
                        {
                            TempT1.Dispose();


                        }
                        if (T2.IsCompleted)
                        {
                            T2.Dispose();
                        }

                        Thread.Sleep(2000);

                        GC.Collect();

                        #endregion

                    Error:

                        #region 将当前数据填充到TotalTestData,并且完成当前Condition的数据存档



                        MyAttribute.prosess = (i + 1) * 100 / (MyCondition.dtCondition.Rows.Count);


                        if (MyAttribute.StopFlag)
                        {
                            MyAttribute.TotalResultFlag = false;
                            break;
                        }

                        #endregion
                    }
                    if (!MyAttribute.StopFlag)
                    {
                        MyAttribute.TotalResultFlag = true;

                    }

                    #endregion
                

                if (MyEquipmentstruct.MyTempControl != null)
                {
                    MyEquipmentstruct.MyTempControl.SetPositionUPDown("0");

                }
                for (int k = 0; k < MyEquipmentstruct.EquipmentList.Count; k++)
                {
                    MyEquipmentstruct.EquipmentList.Values[k].Referenced_Times = 0;
                }

                MyAttribute.MylogManager.AdapterLogString(1, "Test End------------------------");
                MyAttribute.FlagFlowControllEnd = true;

                return true;
            }
            catch (Exception ex)
            {

                MyAttribute.MylogManager.AdapterLogString(1, "Test End------------------------");
                MyAttribute.FlagFlowControllEnd = true;
                return false;
            }



            return false;
        }

        private bool SelectTrackErrorHead()
        {
            MyAttribute.dtTotalTestData.Columns.Clear();
            MyAttribute.dtTotalTestData.Columns.Add("NO");
            MyAttribute.dtTotalTestData.Columns.Add("FwVer");
            MyAttribute.dtTotalTestData.Columns.Add("TestTime");

            MyAttribute.dtTotalTestData.Columns.Add("Vcc");
            MyAttribute.dtTotalTestData.Columns.Add("Temp");
            MyAttribute.dtTotalTestData.Columns.Add("DmiTemp");
            MyAttribute.dtTotalTestData.Columns.Add("TempLD");

            MyAttribute.dtTotalTestData.Columns.Add("TxPower_uw");
            MyAttribute.dtTotalTestData.Columns.Add("AP_dbm");
            MyAttribute.dtTotalTestData.Columns.Add("ER");


            for (int i = 1; i < MyAttribute.TotalChannnel + 1; i++)
            {
                MyAttribute.dtTotalTestData.Columns.Add("TxPAdc" + i);
            }


            for (int i = 1; i < MyAttribute.TotalChannnel + 1; i++)
            {
                MyAttribute.dtTotalTestData.Columns.Add("BiasDAC" + i);
            }
            for (int i = 1; i < MyAttribute.TotalChannnel + 1; i++)
            {
                MyAttribute.dtTotalTestData.Columns.Add("ModDAC" + i);
            }
            return true;
        }

        private void CalculateTrackError()
        {
            int K = 0;

            while (!MyAttribute.ChangeTempEndflag)
            {
                MyAttribute.dtTrackError = new DataTable();
                MyAttribute.dtCurrentConditionResultData = MyAttribute.dtTotalTestData.Clone();

                DataRow dr = MyAttribute.dtCurrentConditionResultData.NewRow();

                dr["NO"] = MyAttribute.CurrentSN;
                dr["FwVer"] = MyAttribute.Current_DutFwRev;
                dr["TestTime"] = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                dr["Temp"] = MyCondition.CurrentTemp;
                dr["Vcc"] = MyCondition.CurrentVcc;
                double DmiTemp=MyEquipmentstruct.MyDut.ReadDmiTemp();
                dr["DmiTemp"] = DmiTemp;
                double TempLd=0;//等待赋值
                MyEquipmentstruct.MyScope.ChangeChannel("1");

                int BiasDAC,ModDAC;
                if (MyCondition.CurrentTemp>25)
                {
                    HT_DAC_Calculate(TempLd, DmiTemp, out BiasDAC, out ModDAC);
                }
                else
                {
                    LT_DAC_Calculate(TempLd, DmiTemp, out BiasDAC, out ModDAC);
                }

                MyEquipmentstruct.MyDut.WriteBiasDac(BiasDAC);
                MyEquipmentstruct.MyDut.WriteBiasDac(ModDAC);

                if (MyEquipmentstruct.MyScope != null)
                {
                    dr["TxPower_uw"] = MyEquipmentstruct.MyScope.GetAveragePowerWatt();
                    dr["Ap_dbm"] = MyEquipmentstruct.MyScope.GetAveragePowerdbm();
                    dr["ER"] = MyEquipmentstruct.MyScope.GetEratio();
                }



                MyEquipmentstruct.MyDut.ChangeChannel("1");
                double TxPowerDmi = MyEquipmentstruct.MyDut.ReadDmiTxp();
                
                if (double.IsInfinity(TxPowerDmi))
                {
                    TxPowerDmi = -100;
                }

                dr["DmiTx"] = TxPowerDmi;
                dr["DmiIBias"] = MyEquipmentstruct.MyDut.ReadDmiBias();
                  
                ushort TxpowADC;

                MyEquipmentstruct.MyDut.ReadTxpADC(out TxpowADC);
                   
                dr["TxPADC"] = TxpowADC;
                dr["BiasDAC"] = MyEquipmentstruct.MyDut.ReadBiasDac();
                dr["ModDAC"] = MyEquipmentstruct.MyDut.ReadModDac();

                MyAttribute.dtTrackError.Rows.Add(dr.ItemArray);


                string[] TestDataArray = Array.ConvertAll<object, string>(MyAttribute.dtTrackError.Rows[0].ItemArray, Convert.ToString);
                MyAttribute.MyOperateTxT.WriteTxt(TestDataArray);
                MyAttribute.dtTotalTrackErrorData.Merge(MyAttribute.dtTrackError);

                int EXtralRow = 0;

                while (MyAttribute.dtTotalTestData.Rows.Count > 10)
                {
                    if (MyAttribute.dtTotalTestData.Rows.Count == 10) break;

                    MyAttribute.dtTotalTestData.Rows.RemoveAt(EXtralRow);
                    EXtralRow++;
                }



                Thread.Sleep(MyAttribute.CycleTime * 1000);

            }
            MyAttribute.ConditionEndflag = true;
        }

  #endregion
     
   

      

       

  




       
    }
    public class hp// 记录原始窗体的尺寸
    {
        public Size s { set; get; }
        public Point p { set; get; }
    }
}

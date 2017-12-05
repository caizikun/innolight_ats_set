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

       
        EquipmentList MyEquipList = new EquipmentList();
        TestModeEquipmentParameters[] TestModelInputArray;// TestModel 输入数组
        FlowControll pflowControl;
        globalParameters pGlobalParameters = new globalParameters();
        //--------------------------
        private logManager plogManager = new logManager();
        private DUT pdut = new DUT();

        TestTxEyeRinOMA pTestModel;


     
      
       
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
            this.skinEngine1.SkinFile = "DiamondBlue.ssk";

   
           // aEquipList=new EquipmentList();

         

           

           

        }
        //[DllImport("user32.dll")]
        // private static extern bool AnimateWindow(IntPtr hwnd,int dwtime,int dwflag);// 复位CH375设备,返回句柄,出错则无效

      
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

     
     

 

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            MessageBox.Show("感谢您的使用！");
            System.Environment.Exit(0);   

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
          
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
               
            }
            pflowControl.StrIpAddress = StrIP;
          //  this.Text = "ATS_" + MyXml.DbName;


          string[] ProductionTypeArray=  ReadProductionTpye();

 
        }

     

        private void btnExport_Click(object sender, EventArgs e)
        {
        
           
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
                  //  labelProgress.Text = process + "%";
                }
            }
        }
        private void RefreshResult(bool Rflag)
        {
            
        }
        private void TestResultFormShow(bool Rflag, DataTable dt, bool ShowErrorData)
        {
           

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
          //  labelShow.Text = "Test End---------";
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
            //button_Config.BackColor = c2;
            //button_Config.Refresh();
        }
        private void ConfigEquipment()
        {

            string CurrentEquipmentName = "";
           

            UpdataRichBox refreshRichBox = new UpdataRichBox(UpdataShowLog);
            // UpdataDataTable refreshDataTable = new UpdataDataTable(RefreshDataGridView);

            DutStruct[] MyDutManufactureCoefficientsStructArray;
            DriverStruct[] MyManufactureChipsetStructArray;
            DriverInitializeStruct[] MyDutManufactureChipSetInitialize;
            DutEEPROMInitializeStuct[] MyDutEEPROMInitializeStuct;
            int i = 0;
            try
            {

               // pflowControl.pDut = (DUT)MyEquipmentManage.Createobject(pflowControl.ProductionTypeName.ToUpper() + "DUT");
               
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
                    EquipmentBase CurrentEquipmentObject = null;
                    CurrentEquipmentObject.Role = Convert.ToByte(dtEquipmenList.Rows[i]["Role"]);// 0=NA,1=TX,2=RX

                 //   UpDataStatus(labelShow, CurrentEquipmentName + "  Configing.....");

                    if (!CurrentEquipmentObject.Initialize(CurrentEquipmentStruct) || !CurrentEquipmentObject.Configure(1))
                    {
                        MessageBox.Show(CurrentEquipmentName + "Configure Error");
                       

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
                    //UpDataProcess(progress, ProcessValue);
                    //UpDataStatus(labelProgress, ProcessValue.ToString());
                    //UpDataStatus(labelShow, CurrentEquipmentName+"  ConfigOK");
                    plogManager.AdapterLogString(1, CurrentEquipmentName + "  ConfigOK");
                }
                
                pflowControl.pEnvironmentcontroll = new EnvironmentalControll(pflowControl.pDut,pflowControl.MyLogManager);
               
                pflowControl.MyEquipList = MyEquipmentArrayy.MyEquipList;

                

                //if (pflowControl.pPS == null)
                //{
                //    MessageBox.Show("缺少电源，请确认Testplan中是否准备了电源！");
                //    FlagEquipmentConfigOK = false;
                //}
                
            }
            catch (Exception EX)
            {
                plogManager.AdapterLogString(1, CurrentEquipmentName + "  Config Error");

                MessageBox.Show(EX.Message,"ConfigEquipment Error ,PLS Check Equipment");
               // UpDataStatus(this.labelShow, "Equipment Config false");
               // FlagEquipmentConfigOK = false;
            }
            finally
            {
                
            }
        }
 #endregion
        private void buttonStop_Click(object sender, EventArgs e)    
        {
         
            if (MessageBox.Show("Do you want to Stop Test?", " Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                pflowControl.StopFlag = true;
                plogManager.AdapterLogString(1, "You Have Stop the ATS EXE");
               // buttonStop.Enabled = false;   
            }
           
        }

  

  

        #region 获取产品信息

        public string[] ReadProductionTpye()
        {

            DataTable dtProductionType = new DataTable();
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

     

        private void buttonEq_Click(object sender, EventArgs e)
        {
        
            TestModeEquipmentParameters[]EquipmentPrameter=new TestModeEquipmentParameters[14];

            EquipmentPrameter[0].FiledName = "Addr";
            EquipmentPrameter[0].DefaultValue = "5";


            EquipmentPrameter[1].FiledName = "IOType";
            EquipmentPrameter[1].DefaultValue = "GPIB";

            EquipmentPrameter[2].FiledName = "Reset";
            EquipmentPrameter[2].DefaultValue = "false";

            EquipmentPrameter[3].FiledName = "Name";
            EquipmentPrameter[3].DefaultValue = "E3631";

            EquipmentPrameter[4].FiledName = "DutChannel";
            EquipmentPrameter[4].DefaultValue = "1";

            EquipmentPrameter[5].FiledName = "OptSourceChannel";
            EquipmentPrameter[5].DefaultValue = "2";

            EquipmentPrameter[6].FiledName = "DutVoltage ";
            EquipmentPrameter[6].DefaultValue = "3.3";

            EquipmentPrameter[7].FiledName = "DutCurrent ";
            EquipmentPrameter[7].DefaultValue = "1.5";

            EquipmentPrameter[8].FiledName = "OptVoltage ";
            EquipmentPrameter[8].DefaultValue = "3.3";

            EquipmentPrameter[9].FiledName = "OptCurrent";
            EquipmentPrameter[9].DefaultValue = "1.0";

            EquipmentPrameter[10].FiledName = "opendelay";
            EquipmentPrameter[10].DefaultValue = "1000";

            EquipmentPrameter[11].FiledName = "closedelay";
            EquipmentPrameter[11].DefaultValue = "1000";


            E3631 PS = new E3631(new logManager());

            PS.Initialize(EquipmentPrameter);
            PS.ReSet();
            PS.Configure(1);
            PS.OutPutSwitch(true);
            PS.OutPutSwitch(false);
            double V = PS.GetVoltage();
            double I = PS.GetCurrent();
            PS.ConfigVoltageCurrent("3.5");
            
        
        }

        private void btTestModel_Click(object sender, EventArgs e)
        {

            if (!Test())
            {
                MessageBox.Show("ERROR!");
               
            }

        }
        private bool Test()
        {
            richTextBox1.Text = "";
            richTextBox1.Refresh();

            try
            {


                pTestModel = new TestTxEyeRinOMA(pdut, plogManager);

                TestModelInputArray = FitTestModelParameter();//填写输入参数

                if ( !FitEquipmentList())return false;//填写它所需要的设备仪器

                if (!pTestModel.SelectEquipment(MyEquipList)) return false;  //确认仪器是否完整


               

                pTestModel.SetinputParameters = TestModelInputArray;

                pTestModel.SetGlobalParameters = pGlobalParameters;//填写Global参数,比如 APCType,TotalChannel,CurrentChannel

                if (!pTestModel.RunTest()) return false;



                for (int i = 0; i < pTestModel.GetoutputParameters.Length;i++ )
                {
                    richTextBox1.Text += pTestModel.GetoutputParameters[i].FiledName + "=" + pTestModel.GetoutputParameters[i].DefaultValue.ToString() + "\r\n";
                }

                // pTestModel.GetoutputParameters;

                return true;

            }
            catch
            {

                return false;
            }
           // return false;
        }

        private TestModeEquipmentParameters[] FitTestModelParameter()// 填写Testmodel 的输入参数
        {
            TestModeEquipmentParameters []Parameters = new TestModeEquipmentParameters[2];

            Parameters[0].FiledName = "PRBS";
            Parameters[0].DefaultValue = "1100";

            Parameters[1].FiledName = "TargetPower";
            Parameters[1].DefaultValue = "-40";
            return Parameters;

        }

        private bool FitEquipmentList()
        {
            try
            {

                MyEquipList.Clear();
                MyEquipList.Add("ATTEN_TX", new Attennuator());
                MyEquipList.Add("SCOPE", new Scope());
                MyEquipList.Add("PowerMeter", new PowerMeter());
                MyEquipList.Add("PPG", new PPG());

                return true;
            }
          catch
            {

                return false;
          }
        }

        private void FitGloalParameter()
        {
            pGlobalParameters.CurrentChannel = 1;
            
        }
    }
    
    public class hp// 记录原始窗体的尺寸
    {
        public Size s { set; get; }
        public Point p { set; get; }
    }
}

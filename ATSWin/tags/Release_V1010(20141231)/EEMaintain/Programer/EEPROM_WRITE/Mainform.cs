using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ATSDataBase;
using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;
using System.IO;
using Authority;

namespace Maintain
{
    public partial class MainForm : Form
    {
        LoginInfoStruct myLoginInfoStruct;
        
        bool isEEPROMWriter = false;
        bool blnISDBSQLserver = false;

        bool loadFormOK = false;
        DataIO myDataIO;
               
        //public bool isMaintainUser = false;
        DataSet GlobalDS;
        EEPROMOperation myEEPROMOperation;
        string EEPROMTabPID = "-1";
        int showFristRowID = 0;

        string[,] dataInfo = new string[2, 256];

        byte[] dataVaules = new byte[256];
        byte[] crcdataVaules = new byte[256];
        private float X;
        private float Y;

        int myLastPageNo = -1;
        int myLastPageNoMol = -1;
        string myLasttype = "";
        string myLastPN = "";
        string myLastItem = "";

        ToolTip mytip = new ToolTip();
        
        string[] ConstGlobalListTables = new string[] { "GlobalProductionType", "GlobalProductionName", "GlobalMSAEEPROMInitialize" };

        byte[] Data0 = new byte[256];   //默认EEPROM 出货资料
        byte[] Data1 = new byte[256];
        byte[] Data2 = new byte[256];
        byte[] Data3 = new byte[256];

        byte CRC0 = 0;
        byte CRC1 = 0;
        byte CRC2 = 0;
        byte CRC3 = 0;

        byte[] ReadData0 = new byte[256];
        byte[] ReadData1 = new byte[256];
        byte[] ReadData2 = new byte[256];
        byte[] ReadData3 = new byte[256];

        int ReadDATA0CS1 = 0, ReadDATA0CS2 = 0;
        int ReadDATA1CS1 = 0, ReadDATA1CS2 = 0;

        int DATA0CS1 = 0, DATA0CS2 = 0;
        int DATA1CS1 = 0, DATA1CS2 = 0;
       
        bool btnflag;
        bool crcflag;
        byte refreshflag = 0;    //0：no refresh  1：refresh write 2：refresh read

        
        public MainForm(LoginInfoStruct pLoginInfoStruct)
        {
            myLoginInfoStruct = pLoginInfoStruct;
            InitializeComponent();
        }

        void setFormState(bool state)
        {
            grpDataInfo.Enabled = state;
            grpServer.Enabled = state;

        }

        // TBL
        private void Mainform_Load(object sender, EventArgs e)
        {
            isEEPROMWriter = ((myLoginInfoStruct.myAccessCode & 0x100 )==0x100 ? true:false);
            blnISDBSQLserver = myLoginInfoStruct.blnISDBSQLserver;

            if (isEEPROMWriter)
            {
            }
            else
            {
                MessageBox.Show("The current user does not have login privilege, please confirm!");
                this.Close();
            }

            formLoad();
            timerDate.Enabled = true;
            //在Form_Load里面添加:
            this.Resize += new EventHandler(Form_Resize);
            X = this.Width;
            Y = this.Height;
            setTag(this);
            Form_Resize(new object(), new EventArgs());//x,y可在实例化时赋值,最后这句是新加的，在MDI时有用            
        } 

        /// <summary>
        /// 初始化界面信息...
        /// </summary>         
        void formLoad()
        {
            try
            {
                setFormState(false);
                grpModule.Enabled = false;
                refreshflag = 0;

                this.Text = Application.ProductName + " Ver:" + Application.ProductVersion;
                this.tsuserInfo.Text = "  User:" + myLoginInfoStruct.UserName + "[Login time:" + DateTime.Now.ToString() + "]  ";

                myDataIO = new SqlManager(myLoginInfoStruct.ServerName, myLoginInfoStruct.DBName, myLoginInfoStruct.DBUser, myLoginInfoStruct.DBPassword);

                this.cboPNType.Items.Clear();
                this.cboPNType.Text = "";
                initPN();

                LoadTabPage();

                if (getDSInfo() == false)
                {
                    MessageBox.Show("Failed to get the information...");
                    sslRunMsg.Text = "Failed to get the information... Time: " + DateTime.Now.ToString();
                    runMsg.Refresh();
                    return;
                }
                else
                {
                    sslRunMsg.Text = "Get the information successful!";
                    runMsg.Refresh();
                }
                cboPN.Enabled = false;
                cboPNItem.Enabled = false;
                tabServer.SelectedIndex = -1;
                tabModule.SelectedIndex = -1;
                
            }
            catch (Exception ex)
            {
                GlobalDS.Tables.Clear();
                MessageBox.Show(ex.ToString());
            }
        } 

        // TBL
        /// <summary>
        /// 获取数据库信息
        /// </summary>
        /// <returns></returns>
        bool getDSInfo()
        {
            try
            {
                GlobalDS = new DataSet("GlobalDS");
                for (int i = 0; i < ConstGlobalListTables.Length; i++)
                {
                    string queryConditions = "select * from " + ConstGlobalListTables[i];
                    GlobalDS.Tables.Add(myDataIO.GetDataTable(queryConditions, ConstGlobalListTables[i]));
                }

                cboPNType.Items.Clear();
                for (int i = 0; i < GlobalDS.Tables["GlobalProductionType"].Rows.Count; i++)
                {
                    this.cboPNType.Items.Add(GlobalDS.Tables["GlobalProductionType"].Rows[i]["ItemName"].ToString());
                }

                if (cboPNType.Items.Count > 0) 
                {
                    cboPNType.SelectedIndex = -1;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                GlobalDS.Tables.Clear();
                MessageBox.Show(ex.ToString());
                return false;
            }
        }   

        /// <summary>
        /// 初始化PN资料
        /// </summary>
        void initPN()
        {
            cboPN.Items.Clear();
            cboPN.Text = "";
            initPNItem();
        }  
        
        /// <summary>
        ///  初始化PNItem资料
        /// </summary>
        void initPNItem()
        {
            cboPNItem.Items.Clear();
            cboPNItem.Text = "";
        }    

        private void cboPNType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboPNType.Text.ToString().Trim().ToUpper() != myLasttype)
                {
                    refreshflag = 0;
                    grpServer.Refresh();
                    grpDataInfo.Refresh();
                    grpModule.Refresh();

                    myLastPN = "";
                    myLastItem = "";
                    sslRunMsg.Text = " ";
                    sslRunMsg.BackColor = Color.FromKnownColor(KnownColor.Control); 

                    btnflag = false;
                    grpModule.Enabled = false;
                    refreshtabInfo();

                    cboPN.Enabled = false;
                    cboPNItem.Enabled = false;
                    tabServer.SelectedIndex = -1;
                    tabModule.SelectedIndex = -1;
                    setFormState(false);                   
                    clearReadInfo();
                    initPN();
                    //string[] ConstGlobalListTables = new string[] { "GlobalProductionType", "GlobalProductionName", "GlobalMSAEEPROMInitialize" };
                    string currTypeID = "-1";
                    for (int i = 0; i < GlobalDS.Tables["GlobalProductionType"].Rows.Count; i++)
                    {
                        if (GlobalDS.Tables["GlobalProductionType"].Rows[i]["ItemName"].ToString().ToUpper() == cboPNType.Text.ToString().ToUpper())
                        {
                            currTypeID = GlobalDS.Tables["GlobalProductionType"].Rows[i]["ID"].ToString();
                            break;
                        }
                    }
                    string sqlCondition = "PID=" + currTypeID;
                    DataRow[] mrDRs = GlobalDS.Tables["GlobalProductionName"].Select(sqlCondition);
                    for (int i = 0; i < mrDRs.Length; i++)
                    {
                        this.cboPN.Items.Add(mrDRs[i]["PN"].ToString());
                    }

                    if (cboPNType.Text.ToString().Trim().ToUpper() == "QSFP")
                    {
                        myLasttype = "QSFP";
                        myEEPROMOperation = new QSFP(0, !loadFormOK);
                        myEEPROMOperation.Data0length = 128;
                        myEEPROMOperation.Data1length = 128;
                        myEEPROMOperation.Data2length = 128;
                        myEEPROMOperation.Data3length = 128;
                        myEEPROMOperation.Data0FristIndex = 128;
                        myEEPROMOperation.Data1FristIndex = 128;
                        myEEPROMOperation.Data2FristIndex = 128;
                        myEEPROMOperation.Data3FristIndex = 128;

                        myEEPROMOperation.Data0Name = "A0H_Page0";
                        myEEPROMOperation.Data1Name = "A0H_Page1";
                        myEEPROMOperation.Data2Name = "A0H_Page2";
                        myEEPROMOperation.Data3Name = "A0H_Page3";

                    }
                    else if (cboPNType.Text.ToString().Trim().ToUpper() == "SFP")
                    {
                        myLasttype = "SFP";
                        myEEPROMOperation = new SFP(0, !loadFormOK);
                        myEEPROMOperation.Data0length = 256;
                        myEEPROMOperation.Data1length = 128;
                        myEEPROMOperation.Data2length = 128;
                        myEEPROMOperation.Data3length = 0;
                        myEEPROMOperation.Data0FristIndex = 0;
                        myEEPROMOperation.Data1FristIndex = 0;
                        myEEPROMOperation.Data2FristIndex = 128;
                        myEEPROMOperation.Data3FristIndex = 128;

                        myEEPROMOperation.Data0Name = "A0H";
                        myEEPROMOperation.Data1Name = "A2HLowMemory";
                        myEEPROMOperation.Data2Name = "A2HPage0";
                        myEEPROMOperation.Data3Name = "N/A";


                    }
                    else if (cboPNType.Text.ToString().Trim().ToUpper() == "XFP")
                    {
                        myLasttype = "XFP";
                        myEEPROMOperation = new XFP(0, !loadFormOK);
                        myEEPROMOperation.Data0length = 128;
                        myEEPROMOperation.Data1length = 128;
                        myEEPROMOperation.Data2length = 128;
                        myEEPROMOperation.Data3length = 0;
                        myEEPROMOperation.Data0FristIndex = 128;
                        myEEPROMOperation.Data1FristIndex = 0;
                        myEEPROMOperation.Data2FristIndex = 128;
                        myEEPROMOperation.Data3FristIndex = 128;

                        myEEPROMOperation.Data0Name = "A0HPage1";
                        myEEPROMOperation.Data1Name = "A0HLowMemory";
                        myEEPROMOperation.Data2Name = "A0HPage2";
                        myEEPROMOperation.Data3Name = "N/A";
                    }
                    else if (cboPNType.Text.ToString().Trim().ToUpper() == "CFP")
                    {
                        myLasttype = "CFP";
                        myEEPROMOperation = new CFP(0, !loadFormOK);
                        myEEPROMOperation.Data0length = 0;
                        myEEPROMOperation.Data1length = 0;
                        myEEPROMOperation.Data2length = 0;
                        myEEPROMOperation.Data3length = 0;
                        myEEPROMOperation.Data0FristIndex = 0;
                        myEEPROMOperation.Data1FristIndex = 0;
                        myEEPROMOperation.Data2FristIndex = 0;
                        myEEPROMOperation.Data3FristIndex = 0;
                        myEEPROMOperation.Data0Name = "N/A";
                        myEEPROMOperation.Data1Name = "N/A";
                        myEEPROMOperation.Data2Name = "N/A";
                        myEEPROMOperation.Data3Name = "N/A";
                    }

                    Data0 = new byte[myEEPROMOperation.Data0length];
                    Data1 = new byte[myEEPROMOperation.Data1length];
                    Data2 = new byte[myEEPROMOperation.Data2length];
                    Data3 = new byte[myEEPROMOperation.Data3length];

                    ReadData0 = new byte[myEEPROMOperation.Data0length];
                    ReadData1 = new byte[myEEPROMOperation.Data1length];
                    ReadData2 = new byte[myEEPROMOperation.Data2length];
                    ReadData3 = new byte[myEEPROMOperation.Data3length];

                    myLastPageNo = -1;
                    myLastPageNoMol = -1;

                    tabModulePage0.Text = myEEPROMOperation.Data0Name;
                    tabModulePage1.Text = myEEPROMOperation.Data1Name;
                    tabModulePage2.Text = myEEPROMOperation.Data2Name;
                    tabModulePage3.Text = myEEPROMOperation.Data3Name;
                    tabServerPage0.Text = myEEPROMOperation.Data0Name;
                    tabServerPage1.Text = myEEPROMOperation.Data1Name;
                    tabServerPage2.Text = myEEPROMOperation.Data2Name;
                    tabServerPage3.Text = myEEPROMOperation.Data3Name;

                    if (cboPN.Items.Count > 0)
                    {
                        cboPN.SelectedIndex = -1;
                        cboPN.Enabled = true;
                    }

                    loadFormOK = true;

                    if (myEEPROMOperation.deviceIndex == -1)
                    {
                        btnWrite.Visible = false;
                        btnRead.Visible = false;
                    }
                    else
                    {
                        btnWrite.Visible = true;
                        btnRead.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                GlobalDS.Tables.Clear();
            }
        }

        byte Crc8(byte[] buffer)
        {
            byte crc = 0;
            for (int j = 0; j < buffer.Length; j++)
            {
                crc ^= buffer[j];
                for (int i = 0; i < 8; i++)
                {
                    if ((crc & 0x01) != 0)
                    {
                        crc >>= 1;
                        crc ^= 0x8c;
                    }
                    else
                    {
                        crc >>= 1;
                    }
                }
            }
            return crc;
        }        

        private void cboPNItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
               if (cboPNItem.Text.ToString().Trim().ToUpper() != myLastItem)
               {
                   sslRunMsg.BackColor = Color.FromKnownColor(KnownColor.Control);
                   sslRunMsg.Text = " ";

                   txtWriteSN.Text = "";

                   btnflag = false;
                   grpModule.Enabled = false;
                   refreshtabInfo();

                   tabModule.SelectedIndex = -1;
                   tabServer.SelectedIndex = -1;
                   if (cboPNItem.Text.Length > 0)
                   {                      
                       setFormState(true);
                       DataRow[] dr = GlobalDS.Tables["GlobalMSAEEPROMInitialize"].Select("PID=" + EEPROMTabPID + " and ItemName ='" + cboPNItem.Text + "'");

                       myLastItem = cboPNItem.Text.ToString().Trim().ToUpper();

                       if (dr.Length == 1)
                       {
                           string DATA0Value = dr[0]["DATA0"].ToString();
                           string DATA1Value = dr[0]["DATA1"].ToString();
                           string DATA2Value = dr[0]["DATA2"].ToString();
                           string DATA3Value = dr[0]["DATA3"].ToString();

                           byte CRCDATA0Value = Convert.ToByte(dr[0]["CRCDATA0"]);
                           byte CRCDATA1Value = Convert.ToByte(dr[0]["CRCDATA1"]);
                           byte CRCDATA2Value = Convert.ToByte(dr[0]["CRCDATA2"]);
                           byte CRCDATA3Value = Convert.ToByte(dr[0]["CRCDATA3"]);

                           CRC0 = 0;
                           CRC1 = 0;
                           CRC2 = 0;
                           CRC3 = 0;

                           for (int i = 0; i < DATA0Value.Length / 2; i++)
                           {
                               Data0[i] = Convert.ToByte("0x" + DATA0Value.Substring(i * 2, 2), 16);
                           }
                           for (int i = 0; i < DATA1Value.Length / 2; i++)
                           {
                               Data1[i] = Convert.ToByte("0x" + DATA1Value.Substring(i * 2, 2), 16);
                           }
                           for (int i = 0; i < DATA2Value.Length / 2; i++)
                           {
                               Data2[i] = Convert.ToByte("0x" + DATA2Value.Substring(i * 2, 2), 16);
                           }
                           for (int i = 0; i < DATA3Value.Length / 2; i++)
                           {
                               Data3[i] = Convert.ToByte("0x" + DATA3Value.Substring(i * 2, 2), 16);
                           }

                           if (myEEPROMOperation.Data0length > 0)
                           {
                               CRC0 = CRCDATA0Value;
                           }
                           if (myEEPROMOperation.Data1length > 0)
                           {
                               CRC1 = CRCDATA1Value;
                           }
                           if (myEEPROMOperation.Data2length > 0)
                           {
                               CRC2 = CRCDATA2Value;
                           }
                           if (myEEPROMOperation.Data3length > 0)
                           {
                               CRC3 = CRCDATA3Value;
                           }

                           //CRC比较===================
                           byte crc0 = Crc8(Data0);
                           byte crc1 = Crc8(Data1);
                           byte crc2 = Crc8(Data2);
                           byte crc3 = Crc8(Data3);

                           if ((crc0 == CRC0) && (crc1 == CRC1) && (crc2 == CRC2) && (crc3 == CRC3))
                           {
                               crcflag = true;

                               tabServer.SelectedIndex = 0;
                               sslRunMsg.Text = "CRC check successful!" + " " + "crc0 = CRC0 =" + crc0 + ", " + "crc1 = CRC1 =" + crc1 + ", " + "crc2 = CRC2 =" + crc2 + ", " + "crc3 = CRC3 =" + crc3;

                               setFormState(true);

                               txtWriteDC.Text = DateTime.Today.ToString("yyMMdd");


                               string myDatecode = txtWriteDC.Text.Trim().PadRight(8, ' ');
                               byte[] arrayDatecode = System.Text.Encoding.ASCII.GetBytes(myDatecode);
                               for (int i = 0; i < 8; i++)
                               {
                                   Data0[i + 0x54] = arrayDatecode[i];
                               }

                               for (int i = 0; i < 15; i++)
                               {
                                   if (((char)Data0[0x44 + i] >= 48 && (char)Data0[0x44 + i] <= 57) || ((char)Data0[0x44 + i] >= 65 && (char)Data0[0x44 + i] <= 90) || ((char)Data0[0x44 + i] >= 97 && (char)Data0[0x44 + i] <= 122) || (char)Data0[0x44 + i] == 0 || (char)Data0[0x44 + i] == 32)
                                   {
                                       txtWriteSN.Text += ((char)Data0[0x44 + i]).ToString();
                                       txtWriteSN.Text = txtWriteSN.Text.Trim();
                                   }
                                   else
                                   {
                                       txtWriteSN.Text = "";
                                   }
                               }

                               btnWrite.Enabled = true ;
                               refreshWriteDGV();

                               refreshflag = 1;
                               grpServer.Refresh();
                               grpDataInfo.Refresh();
                               grpModule.Refresh();
                           }
                           else
                           {
                               txtPN.Text = "";
                               txtRev.Text = "";
                               txtWriteSN.Text = "";
                               txtVendorName.Text = "";
                               txtVOUI.Text = "";

                               crcflag =false;

                               byte[] crcdata = { crc0, crc1, crc2, crc3 };
                               byte[] CRCdata = { CRC0, CRC1, CRC2, CRC3 };

                               for (int i = 0; i < 4; i++)
                               {
                                   if (crcdata[i] != CRCdata[i])
                                   {
                                       sslRunMsg.Text += "crc" + i + ":" + crcdata[i] + " " + "CRC" + i + ":" + CRCdata[i] + "   ";
                                   }
                               }

                               sslRunMsg.BackColor = Color.Yellow;

                               MessageBox.Show("Read data from server error!", "CRC check error");

                               refreshflag = 0;
                               grpServer.Refresh();
                               grpDataInfo.Refresh();
                               grpModule.Refresh();

                               myLastItem = "";
                               grpServer.Enabled = false;
                               btnWrite.Enabled = false;

                           }
                       }
                   }                 
               }               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        // TBL
        int calcCheckSum(byte[] objectData, int startAddr, int endAddr)
        {
            int cs = 0;

            for (int i = startAddr; i <= endAddr; i++)
            {
                cs += objectData[i];
            }

            return (cs & 0xff);
        } 

        // TBL
        void refreshWriteInfo(string mySN)   
        {
            try
            {         
                    mySN = mySN.PadRight(16, ' ');
                    byte[] arraySN = System.Text.Encoding.ASCII.GetBytes(mySN);
                    for (int i = 0; i < 15; i++)
                    {
                        Data0[i + 0x44] = arraySN[i];
                    }              
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void refreshWriteDGV()
        {
            string myPN = "";
            string myVendorName = "";
            string myVendorOUI = "";
            string myRev = "";

            try
            {
                for (int i = 0; i < 15; i++)
                {
                    myVendorName += ((char)Data0[0x14 + i]).ToString();
                }
                for (int i = 0; i < 3; i++)
                {
                    myVendorOUI += (Data0[0x25 + i]).ToString("X").PadLeft(2, '0'); //141027_2
                }
                for (int i = 0; i < 15; i++)
                {
                    myPN += ((char)Data0[0x28 + i]).ToString();
                }
                for (int i = 0; i < 2; i++)
                {
                    myRev += ((char)Data0[0x38 + i]).ToString();
                }


                txtPN.Text = myPN.Trim();
                txtRev.Text = myRev.Trim();
                txtVendorName.Text = myVendorName.Trim();
                txtVOUI.Text = myVendorOUI.Trim();

                DATA0CS1 = 0; DATA0CS2 = 0;
                DATA1CS1 = 0; DATA1CS2 = 0;

                //int DATA2CS1 = 0, DATA2CS2 = 0;
                if (cboPNType.Text.ToUpper() == "QSFP".ToUpper())
                {
                    DATA0CS1 = calcCheckSum(Data0, 0x00, 0x3e);
                    DATA0CS2 = calcCheckSum(Data0, 0x40, 0x5e);
                    Data0[0x3f] = (byte)DATA0CS1;
                    Data0[0x5f] = (byte)DATA0CS2;
                }
                else if (cboPNType.Text.ToUpper() == "SFP".ToUpper())
                {
                    DATA0CS1 = calcCheckSum(Data0, 0x00, 0x5E);
                    DATA0CS2 = calcCheckSum(Data0, 0x60, 0x7E);
                    DATA1CS1 = calcCheckSum(Data1, 0x00, 0x5E);
                    Data0[0x5f] = (byte)DATA0CS1;
                    Data0[0x7f] = (byte)DATA0CS2;
                    Data1[0x5f] = (byte)DATA1CS1;
                }
                else if (cboPNType.Text.ToUpper() == "XFP".ToUpper())
                {
                    DATA0CS1 = calcCheckSum(Data0, 0x00, 0x3e);
                    DATA0CS2 = calcCheckSum(Data0, 0x40, 0x5e);
                    Data0[0x3f] = (byte)DATA0CS1;
                    Data0[0x5f] = (byte)DATA0CS2;
                }
                else if (cboPNType.Text.ToUpper() == "CFP".ToUpper())
                {

                }
                //dgvServer 0X..
                setDGVInfo(dgvServer0, myEEPROMOperation.Data0FristIndex, myEEPROMOperation.Data0length / 8, false);
                setDGVInfo(dgvServer1, myEEPROMOperation.Data1FristIndex, myEEPROMOperation.Data1length / 8, false);
                setDGVInfo(dgvServer2, myEEPROMOperation.Data2FristIndex, myEEPROMOperation.Data2length / 8, false);
                setDGVInfo(dgvServer3, myEEPROMOperation.Data3FristIndex, myEEPROMOperation.Data3length / 8, false);
                //setDGVInfo(dgvModule, showRowsCount / 8, false);

                refreshdgvData(dgvServer0, Data0, myEEPROMOperation.Data0length);
                refreshdgvData(dgvServer1, Data1, myEEPROMOperation.Data1length);
                refreshdgvData(dgvServer2, Data2, myEEPROMOperation.Data2length);
                refreshdgvData(dgvServer3, Data3, myEEPROMOperation.Data3length);
                //141121_2--------
                myEEPROMOperation.Data0 = Data0;
                myEEPROMOperation.Data1 = Data1;
                myEEPROMOperation.Data2 = Data2;
                myEEPROMOperation.Data3 = Data3;

                sslRunMsg.Text = "DATA0: CS1=" + DATA0CS1.ToString("X") + ";   DATA0:  CS2= " + DATA0CS2.ToString("X") +
                ";   DATA1: CS1=" + DATA1CS1.ToString("X") + ";   DATA1:  CS2= " + DATA1CS2.ToString("X");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void refreshWriteALL()
        { 
            txtPN.Text = "";
            txtRev.Text = "";
            txtWriteSN.Text = "";
            txtWriteDC.Text = "";
            txtVendorName.Text = "";
            txtVOUI.Text = "";

            for (int i = 0; i < 15; i++)
            {
                if (((char)Data0[0x44 + i] >= 48 && (char)Data0[0x44 + i] <= 57) || ((char)Data0[0x44 + i] >= 65 && (char)Data0[0x44 + i] <= 90) || ((char)Data0[0x44 + i] >= 97 && (char)Data0[0x44 + i] <= 122) || (char)Data0[0x44 + i] == 0 || (char)Data0[0x44 + i] == 32)
                {
                    txtWriteSN.Text += ((char)Data0[0x44 + i]).ToString();
                    txtWriteSN.Text = txtWriteSN.Text.Trim();
                }
                else
                {
                    txtWriteSN.Text = "";
                }
            }

            for (int i = 0; i < 8; i++)
            {
                txtWriteDC.Text += ((char)Data0[0x54 + i]).ToString();
            }

            for (int i = 0; i < 15; i++)
            {
                txtVendorName.Text += ((char)Data0[0x14 + i]).ToString();
            }
            for (int i = 0; i < 3; i++)
            {
                txtVOUI.Text += (Data0[0x25 + i]).ToString("X").PadLeft(2, '0');  //141027_2
            }
            for (int i = 0; i < 15; i++)
            {
                txtPN.Text += ((char)Data0[0x28 + i]).ToString();
            }
            for (int i = 0; i < 2; i++)
            {
                txtRev.Text += ((char)Data0[0x38 + i]).ToString();
            }

            txtPN.Text = txtPN.Text.Trim();
            txtRev.Text = txtRev.Text.Trim();
            txtWriteDC.Text = txtWriteDC.Text.Trim();
            txtVendorName.Text = txtVendorName.Text.Trim();
            txtVOUI.Text = txtVOUI.Text.Trim();
        }
        

        void refreshReadInfo() 
        {
            string mySN = "";
            string myDatecode = "";
            string myPN = "";
            string myVendorName = "";
            string myVendorOUI = "";
            string myRev = "";

            try
            {
                if (loadFormOK)
                {
                    for (int i = 0; i < 15; i++)
                    {
                        mySN += ((char)ReadData0[0x44 + i]).ToString();
                    }

                    for (int i = 0; i < 8; i++)
                    {
                        myDatecode += ((char)ReadData0[0x54 + i]).ToString();
                    }

                    for (int i = 0; i < 15; i++)
                    {
                        myVendorName += ((char)ReadData0[0x14 + i]).ToString();
                    }
                    for (int i = 0; i < 3; i++)
                    {
                        myVendorOUI += (ReadData0[0x25 + i]).ToString("X").PadLeft(2, '0');  //141027_2
                    }
                    for (int i = 0; i < 15; i++)
                    {
                        myPN += ((char)ReadData0[0x28 + i]).ToString();
                    }
                    for (int i = 0; i < 2; i++)
                    {
                        myRev += ((char)ReadData0[0x38 + i]).ToString();
                    }

                    //int DATA2CS1 = 0, DATA2CS2 = 0;
                    if (cboPNType.Text.ToUpper() == "QSFP".ToUpper())
                    {
                        ReadDATA0CS1 = ReadData0[0x3f];
                        ReadDATA0CS2 = ReadData0[0x5f];
                    }
                    else if (cboPNType.Text.ToUpper() == "SFP".ToUpper())
                    {
                        ReadDATA0CS1 = ReadData0[0x5f];
                        ReadDATA0CS2 = ReadData0[0x7f];
                        ReadDATA1CS1 = ReadData1[0x5f];
                    }
                    else if (cboPNType.Text.ToUpper() == "XFP".ToUpper())
                    {
                        ReadDATA0CS1 = ReadData0[0x3f];
                        ReadDATA0CS2 = ReadData0[0x5f];
                    }
                    else if (cboPNType.Text.ToUpper() == "CFP".ToUpper())
                    {

                    }
                    //dgvModule 0X..                    
                    setDGVInfo(dgvModule0,  myEEPROMOperation.Data0FristIndex, myEEPROMOperation.Data0length / 8, false);
                    setDGVInfo(dgvModule1,  myEEPROMOperation.Data1FristIndex, myEEPROMOperation.Data1length / 8, false);
                    setDGVInfo(dgvModule2,  myEEPROMOperation.Data2FristIndex, myEEPROMOperation.Data2length / 8, false);
                    setDGVInfo(dgvModule3,  myEEPROMOperation.Data3FristIndex, myEEPROMOperation.Data3length / 8, false);

                    if (myEEPROMOperation.ReadData0 != null)
                    {
                        refreshdgvData(dgvModule0, myEEPROMOperation.ReadData0, myEEPROMOperation.Data0length);
                    }
                    if (myEEPROMOperation.ReadData1 != null)
                    {
                        refreshdgvData(dgvModule1, myEEPROMOperation.ReadData1, myEEPROMOperation.Data1length);
                    }
                    if (myEEPROMOperation.ReadData2 != null)
                    {
                        refreshdgvData(dgvModule2, myEEPROMOperation.ReadData2, myEEPROMOperation.Data2length);
                    }
                    if (myEEPROMOperation.ReadData3 != null)
                    {
                        refreshdgvData(dgvModule3, myEEPROMOperation.ReadData3, myEEPROMOperation.Data3length);
                    }
                   
                    txtPN.Text = myPN.Trim();
                    txtRev.Text = myRev.Trim();
                    txtWriteSN.Text = mySN.Trim();
                    txtWriteDC.Text = myDatecode.Trim();
                    txtVendorName.Text = myVendorName.Trim();
                    txtVOUI.Text = myVendorOUI.Trim();
                    sslRunMsg.Text = ("PN=" + myPN.Trim() + ";SN=" + mySN + ";Datecode=" + myDatecode + ";VendorName=" + myVendorName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void refreshtabInfo()
        {
            if (cboPNType.Text.ToString().Trim().ToUpper() == "QSFP")
            {
                tabModulePage0.Text = "A0H_Page0";
                tabModulePage1.Text = "A0H_Page1";
                tabModulePage2.Text = "A0H_Page2";
                tabModulePage3.Text = "A0H_Page3";
            }
            else if (cboPNType.Text.ToString().Trim().ToUpper() == "SFP")
            {
                tabModulePage0.Text = "A0H";
                tabModulePage1.Text = "A2HLowMemory";
                tabModulePage2.Text = "A2HPage0";
                tabModulePage3.Text = "N/A";
            }
            else if (cboPNType.Text.ToString().Trim().ToUpper() == "XFP")
            {
                tabModulePage0.Text = "A0HPage1";
                tabModulePage1.Text = "A0HLowMemory";
                tabModulePage2.Text = "A0HPage2";
                tabModulePage3.Text = "N/A";
            }
            else if (cboPNType.Text.ToString().Trim().ToUpper() == "CFP")
            {

            }
        }

        void refreshdgvData(DataGridView dgv, byte[] dataValues, int myCount)
        {
            try
            {
               
                for (int i = 0; i < myCount / 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        dgv.Rows[i].Cells[j].Value = dataValues[i * 8 + j].ToString("X").PadLeft(2, '0');  //141024_0                    
                    }
                }                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        /// <summary>
        /// 刷新当前时间...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerDate_Tick(object sender, EventArgs e)
        {
            try
            {
                tssTimelbl.Text = System.DateTime.Now.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        } 

        /// <summary>
        /// 获取表的某项符合条件资料[唯一]
        /// </summary>
        /// <param name="dt">表名</param>
        /// <param name="CloumnName">CloumnName</param>
        /// <param name="filterString">条件</param>
        /// <returns></returns>
        string getDTColumnInfo(DataTable dt, string CloumnName, string filterString)
        {
            DataRow[] dr = dt.Select(filterString.Trim());
            string ReturnValue = "";
            try
            {
                if (dr.Length == 1)
                {
                    ReturnValue = dr[0][CloumnName].ToString();
                }
                else
                {
                    MessageBox.Show("Found uncertain records....-->There are " + dr.Length + " records!");
                }
                return ReturnValue;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return ReturnValue;
            }
        } 


        // Q：setDGVInfo方法的用途没看懂，2014-11-5 10:13:25
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="startAddress"></param>
        /// <param name="rowCount">行数</param>
        /// <param name="isFormLoad"></param>
        void setDGVInfo(DataGridView dgv, int startAddress, int rowCount, bool isFormLoad)
        {
            dgv.Rows.Clear();
            int columnCount = 8;
            if (isFormLoad)
            {
                for (int i = 0; i < columnCount; i++)
                {
                    dgv.Columns.Add(i.ToString(), i.ToString("X").ToUpper() + @"/" + ((int)(i + columnCount)).ToString("X").ToUpper());
                    //24. 如何避免用户对列排序?
                    //对于DataGridView 控件，默认情况下，TextBox类型的列会自动排序，
                    //而其它类型的列则不会自动排序。这种自动排序有时会把数据变得比较乱，这时你会想更改这些默认设置。
                    //DataGridViewColumn的属性SortMode决定了列的排序方式，将其设置为
                    //DataGridViewColumnSortMode.NotSortable就可以避免默认的排序行为。
                    dgv.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                    dgv.Columns[i].ReadOnly = true;
                }
            }
            if (rowCount > 0)
            {
                dgv.Rows.Add(rowCount);
                dgv.RowHeadersWidth = 46;

                for (int i = 0; i < rowCount; i++)
                {
                    for (int j = 0; j < columnCount; j++)
                    {
                        string myValue = (i * columnCount + j).ToString("X").ToUpper();
                        if (myValue.Length < 2)
                        {
                            myValue = "0" + myValue;
                        }
                        dgv.Rows[i].Cells[j].Value = myValue;
                        dgv.Columns[j].Width = 32;
                    }
                    if (i % 2 == 0)
                    {
                        dgv.Rows[i].HeaderCell.Value = ((int)(i / 2)).ToString("X").ToUpper() + "0";  //(i + startAddress)
                    }
                    else
                    {
                        dgv.Rows[i].HeaderCell.Value = ((int)(i / 2)).ToString("X").ToUpper() + "8";  //(i + startAddress)
                    }

                }
            }
            if (rowCount <= 128)
            {
                resizeDGV(dgv);
            }
        } 

        void clearReadInfo()
        {
            txtWriteDC.Text = ""; txtWriteDC.Refresh();
            txtWriteSN.Text = ""; txtWriteSN.Refresh();
            txtRev.Text = ""; txtRev.Refresh();
            txtVendorName.Text = ""; txtVendorName.Refresh();
            txtVOUI.Text = ""; txtVOUI.Refresh();
            txtPN.Text = ""; txtPN.Refresh();
            System.Threading.Thread.Sleep(10);
        }     

        void LoadTabPage()
        {
            try
            {
                txtWriteDC.MaxLength = 8; //DateCode

                // ListView 控件中的选定项的从零开始的索引。 默认值为 -1，表示当前未选择项。

                dgvModule0.DataSource = null;
                dgvModule1.DataSource = null;
                dgvModule2.DataSource = null;
                dgvModule3.DataSource = null;
                dgvServer0.DataSource = null;
                dgvServer1.DataSource = null;
                dgvServer2.DataSource = null;
                dgvServer3.DataSource = null;
                setDGVInfo(dgvModule0, showFristRowID, 16, true);
                setDGVInfo(dgvModule1, showFristRowID, 16, true);
                setDGVInfo(dgvModule2, showFristRowID, 16, true);
                setDGVInfo(dgvModule3, showFristRowID, 16, true);
                setDGVInfo(dgvServer0, showFristRowID, 16, true);
                setDGVInfo(dgvServer1, showFristRowID, 16, true);
                setDGVInfo(dgvServer2, showFristRowID, 16, true);
                setDGVInfo(dgvServer3, showFristRowID, 16, true);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        } 

        /// <summary>
        /// 重新设置dgv显示大小
        /// </summary>
        /// <param name="dgv">dgv</param>
        void resizeDGV(DataGridView dgv)
        {
            int mySize = 0;
            int j = 0;
            for (int i = 0; i < dgv.Columns.Count; i++)
            {
                if (dgv.Columns[i].Visible)
                {
                    j++;
                    mySize += dgv.Columns[i].Width;
                }
            }
            if (dgv.RowHeadersVisible)
            {
                mySize += dgv.RowHeadersWidth;
            }
            for (int i = 0; i < dgv.Columns.Count; i++)
            {
                if (mySize < dgv.Width)
                {
                    if (dgv.Columns[i].Visible)
                    {
                        dgv.Columns[i].Width += (dgv.Width - mySize) / j;
                    }
                }
            }
        }   
   
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboPN_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {               
                if (cboPN.Text.ToString().Trim().ToUpper() != myLastPN)
                {
                    refreshflag = 0;
                    grpServer.Refresh();
                    grpDataInfo.Refresh();
                    grpModule.Refresh();

                    myLastItem = "";

                    btnflag = false;
                    grpModule.Enabled = false;
                    refreshtabInfo();

                    setFormState(false);
                    cboPNItem.Enabled = false;
                    tabServer.SelectedIndex = -1;
                    tabModule.SelectedIndex = -1;
                    clearReadInfo();
                    

                    sslRunMsg.Text = " ";
                    sslRunMsg.BackColor = Color.FromKnownColor(KnownColor.Control); 

                    //string[] ConstGlobalListTables = new string[] { "GlobalProductionType", "GlobalProductionName", "GlobalMSAEEPROMInitialize" };
                    EEPROMTabPID = getDTColumnInfo(GlobalDS.Tables["GlobalProductionName"], "ID", "PN='" + this.cboPN.Text + "'");
                    initPNItem();
                    string sqlCondition = "PID=" + EEPROMTabPID;
                    DataRow[] mrDRs = GlobalDS.Tables["GlobalMSAEEPROMInitialize"].Select(sqlCondition);
                    for (int i = 0; i < mrDRs.Length; i++)
                    {
                        this.cboPNItem.Items.Add(mrDRs[i]["ItemName"].ToString());
                    }

                    myLastPN = cboPN.Text.ToString().Trim().ToUpper();

                    if (cboPNItem.Items.Count > 0)
                    {
                        cboPNItem.SelectedIndex = -1;
                        cboPNItem.Enabled = true;
                    }
                    else
                    {
                        sslRunMsg.Text = "No EEPROM information was found!";                                    
                    }
                }               
            }
            catch (Exception ex)
            {
                GlobalDS.Tables.Clear();
                MessageBox.Show(ex.ToString());
            }
        }  

        //void updateUserLoginInfo(string loginOfftime, bool isLoginOFF, string logs)
        //{
        //    try
        //    {
        //        DataTable userLoginInfoDt = myDataIO.GetDataTable("select * from UserLoginInfo", "UserLoginInfo");
        //        DataRow[] dr = userLoginInfoDt.Select("ID=" + myLoginID);
        //        string hostname = System.Net.Dns.GetHostName(); //主机
        //        System.Net.IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(hostname);//网卡IP地址集合
        //        string IP6 = ipEntry.AddressList[0].ToString();//取一个IP
        //        string IP4 = ipEntry.AddressList[ipEntry.AddressList.Length - 1].ToString();//取一个IP
        //        string currTime = myDataIO.GetCurrTime().ToString();
        //        if (dr.Length == 1)
        //        {
        //            if (loginOfftime.Trim().Length > 0)
        //            {
        //                dr[0]["LoginOfftime"] = currTime;
        //            }
        //            if (isLoginOFF)
        //            {
        //                dr[0]["LoginInfo"] = "The user " + this.user + " has logged in from computer " + hostname + "[" + IP4 + "]";
        //            }
        //            if (logs.Trim().Length > 0)
        //            {
        //                dr[0]["OPLogs"] = logs;
        //            }

        //            myDataIO.UpdateDataTable("select * from UserLoginInfo", userLoginInfoDt);
        //        }
        //        else
        //        {
        //            MessageBox.Show("");
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //    }
        //}

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

        void Form_Resize(object sender, EventArgs e)
        {
            try
            {
                float newx = (this.Width) / X;
                float newy = this.Height / Y;
                setControls(newx, newy, this);
                //this.Text = this.Width.ToString() + " " + this.Height.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void tabServer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabServer.SelectedIndex != -1)
            {

                #region 显示资料&长度确认
                if ( tabServer.SelectedIndex == 0)
                {
                    if (myEEPROMOperation.Data0length > 0)
                    {
                        myLastPageNo = 0;
                        if (btnflag)
                        {
                            tabModule.Enabled = true;
                            tabModule.SelectedIndex = 0;
                        }
                        else
                        {
                            tabModule.SelectedIndex = -1;
                        }
                    }
                    else
                    {
                        MessageBox.Show("The page could not be found!");
                        tabServer.SelectedIndex = myLastPageNo;

                        return;
                    }
                }
                else if (tabServer.SelectedIndex == 1)
                {
                    if (myEEPROMOperation.Data1length > 0)
                    {
                        myLastPageNo = 1;
                        if (btnflag)
                        {
                            tabModule.SelectedIndex = 1;
                        }
                        else
                        {
                            tabModule.SelectedIndex = -1;
                        }
                    }
                    else
                    {
                        MessageBox.Show("The page could not be found!");
                        tabServer.SelectedIndex = myLastPageNo;
                        return;
                    }
                }
                else if (tabServer.SelectedIndex == 2)
                {
                    if (myEEPROMOperation.Data2length > 0)
                    {
                        myLastPageNo = 2;
                        if (btnflag)
                        {
                            tabModule.SelectedIndex = 2;
                        }
                        else
                        {
                            tabModule.SelectedIndex = -1;
                        }
                    }
                    else
                    {
                        MessageBox.Show("The page could not be found!");
                        tabServer.SelectedIndex = myLastPageNo;
                        return;
                    }
                }
                else if (tabServer.SelectedIndex == 3)
                {
                    if (myEEPROMOperation.Data3length > 0)
                    {                       
                        myLastPageNo = 3;
                        if (btnflag)
                        {
                            tabModule.SelectedIndex = 3;
                        }
                        else
                        {
                            tabModule.SelectedIndex = -1;
                        }   
                    }
                    else
                    {
                        MessageBox.Show("The page could not be found!");
                        tabServer.SelectedIndex = myLastPageNo;
                        return;
                    }
                }
                #endregion

            }
        }

        private void tabModule_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabModule.SelectedIndex != -1)
            {

                #region 显示资料&长度确认
                if (tabModule.SelectedIndex == 0)
                {
                    if (myEEPROMOperation.Data0length > 0)
                    {
                        myLastPageNoMol = 0;

                        if (crcflag)
                        {
                            tabServer.Enabled = true;
                            tabServer.SelectedIndex = 0;
                        }
                        else
                        {
                            tabServer.SelectedIndex = -1;
                        }
                    }
                    else
                    {
                        MessageBox.Show("The page could not be found!");
                        tabModule.SelectedIndex = myLastPageNoMol;
                        return;
                    }
                }
                else if (tabModule.SelectedIndex == 1)
                {
                    if (myEEPROMOperation.Data1length > 0)
                    {
                        myLastPageNoMol = 1;
                        if (crcflag)
                        {
                            tabServer.Enabled = true;
                            tabServer.SelectedIndex = 1;
                        }
                        else
                        {
                            tabServer.SelectedIndex = -1;
                        }
                    }
                    else
                    {
                        MessageBox.Show("The page could not be found!");
                        tabModule.SelectedIndex = myLastPageNoMol;
                        return;
                    }
                }
                else if (tabModule.SelectedIndex == 2)
                {
                    if (myEEPROMOperation.Data2length > 0)
                    {
                        myLastPageNoMol = 2;

                        if (crcflag)
                        {
                            tabServer.Enabled = true;
                            tabServer.SelectedIndex = 2;
                        }
                        else
                        {
                            tabServer.SelectedIndex = -1;
                        }
                    }
                    else
                    {
                        MessageBox.Show("The page could not be found!");
                        tabModule.SelectedIndex = myLastPageNoMol;
                        return;
                    }
                }
                else if (tabModule.SelectedIndex == 3)
                {
                    if (myEEPROMOperation.Data3length > 0)
                    {
                        myLastPageNoMol = 3;

                        if (crcflag)
                        {
                            tabServer.Enabled = true;
                            tabServer.SelectedIndex = 3;
                        }
                        else
                        {
                            tabServer.SelectedIndex = -1;
                        }
                    }
                    else
                    {
                        MessageBox.Show("The page could not be found!");
                        tabModule.SelectedIndex = myLastPageNoMol;
                        return;
                    }
                }
                #endregion

            }
        }

        void btnWRState(bool state)
        {
            btnWrite.Enabled = state;
            btnRead.Enabled = state;
        } 


        private void btnWrite_Click(object sender, EventArgs e)
        {
            try
            {
                grpModule.Enabled = true;
                string WriteResult = "";

                refreshtabInfo();

                sslRunMsg.Text = "EEPROM information is writing... Please wait... "; 
                runMsg.Refresh();

                refreshWriteInfo(txtWriteSN.Text);
                refreshWriteDGV(); //DATAO-->MYEEPROM.data0               

                btnWRState(false);

                if (myEEPROMOperation.EEPROMWrite() && myEEPROMOperation.EEPROMRead())
                {    
                    sslRunMsg.Text = "Write the EEPROM information successful... ";
                    runMsg.Refresh();

                    WriteResult = "pass";
                    showResult(WriteResult);

                    //if (myEEPROMOperation.EEPROMRead())
                    //{
                        showDatas();
                        refreshReadInfo();

                        sslRunMsg.Text = "EEPROM information is checking... Please wait... ";
                        runMsg.Refresh();

                        if (checkDatas(true))
                        {
                            MessageBox.Show("Write and check the EEPROM information successful!");
                            sslRunMsg.Text = "EEPROM information write and check: success... ";
                            runMsg.Refresh();
                        }
                        else
                        {
                            MessageBox.Show("Write the EEPROM information successful, but check the information error!"); 
                            sslRunMsg.Text = "EEPROM information write and check: fail... ";
                            runMsg.Refresh();
                        }

                        btnflag = true;
                        grpModule.Enabled = true;
                        tabModule.SelectedIndex = myLastPageNo;

                        refreshflag = 1;
                        grpServer.Refresh();
                        grpDataInfo.Refresh();
                        grpModule.Refresh();
                    //}
                    //else
                    //{
                    //    MessageBox.Show("EEPROM 资料烧写成功，读取对比结果出错!");  
                    //    sslRunMsg.Text = "EEPROM 资料烧写+检查对比结果:失败...";
                    //    runMsg.Refresh();
                    //}

                    tabServer.SelectedIndex = myLastPageNo;

                }
                else
                {
                    MessageBox.Show("Failed to write the EEPROM information!"); 
                    sslRunMsg.Text = "Failed to write the EEPROM information... "; 

                    runMsg.Refresh();

                    btnflag = false;
                    tabModule.SelectedIndex = -1;
                    grpModule.Enabled = false;

                    WriteResult = "fail";
                    showResult(WriteResult);

                    refreshflag = 1;
                    grpServer.Refresh();
                    grpDataInfo.Refresh();
                    grpModule.Refresh();
                }


                System.Threading.Thread.Sleep(50);              

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                btnWRState(true);
            }
        }

        void showResult(string Result)
        {
            string s = "--------Time,SN,Result,ProductType,PN,FileName--------\r\n";

            string sn = "";

            for (int i = 0; i < 15; i++)
            {
                sn += ((char)Data0[i + 0x44]).ToString().Trim();
            }  
          
            s += DateTime.Now.ToString() + "  " + sn + "  " + Result + "  " + cboPNType.Text.ToString() + "  " + cboPN.Text.ToString() + "  " + cboPNItem.Text.ToString() + "\r\n";
  

            FileStream f;
            f = new FileStream(Application.StartupPath + @"\WriteResult.txt", FileMode.Append);
            StreamWriter w = new StreamWriter(f, Encoding.Default);

            w.WriteLine(s);
            w.Close();
            f.Close();
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            try
            {
               grpModule.Enabled = true;

                refreshtabInfo();
                sslRunMsg.Text = "EEPROM information is reading... Please wait...";  
                sslRunMsg.BackColor = Color.FromKnownColor(KnownColor.Control); 

                runMsg.Refresh();

                //clearReadInfo();
                btnWRState(false);

                if ( tabServer .Enabled ==true )
                {
                    if ( myEEPROMOperation.EEPROMRead() )
                    {
                        sslRunMsg.Text = "Read the EEPROM information successful... ";
                        runMsg.Refresh();

                        showDatas();
                        refreshReadInfo();
                        refreshflag = 2;
                        grpServer.Refresh();
                        grpDataInfo.Refresh();
                        grpModule.Refresh();

                        sslRunMsg.Text = "EEPROM information is checking... Please wait... ";
                        runMsg.Refresh();

                        if (checkDatas(false))
                        {
                            MessageBox.Show("Read and check the EEPROM information successful!");
                            sslRunMsg.Text = "EEPROM information read and check: success... ";
                            runMsg.Refresh();
                        }
                        else
                        {
                            MessageBox.Show("Read the EEPROM information successful, but check the information error!");
                            sslRunMsg.Text = "EEPROM information read and check: fail... ";
                            runMsg.Refresh();
                        }

                        btnflag = true;
                        grpModule.Enabled = true;
                        tabModule.SelectedIndex = myLastPageNo;
                    }
                    else
                    {
                        MessageBox.Show("Failed to read the EEPROM information!");
                        sslRunMsg.Text = "Failed to read the EEPROM information... ";
                        runMsg.Refresh();

                        btnflag = false;
                        tabModule.SelectedIndex = -1;
                        grpModule.Enabled = false;

                        if (tabServer .Enabled == true)
                        {
                            refreshWriteALL();
                            refreshflag = 1;
                            grpServer.Refresh();
                            grpDataInfo.Refresh();
                            grpModule.Refresh();
                        }
                        else
                        {
                            refreshflag = 0;
                            grpServer.Refresh();
                            grpDataInfo.Refresh();
                            grpModule.Refresh();
                        }                           
                    }
                }
                else
                {
                    if (myEEPROMOperation.EEPROMRead())
                    {
                        showDatas();
                        refreshReadInfo();
                        refreshflag = 2;
                        grpServer.Refresh();
                        grpDataInfo.Refresh();
                        grpModule.Refresh();

                        MessageBox.Show("Read the EEPROM information successful!");
                        sslRunMsg.Text = "Read the EEPROM information successful... ";
                        runMsg.Refresh();

                        btnflag = true;
                        grpModule.Enabled = true;
                        tabModule.SelectedIndex = 0;
                    }
                    else
                    {
                        MessageBox.Show("Failed to read the EEPROM information!");
                        sslRunMsg.Text = "Failed to read the EEPROM information... ";
                        runMsg.Refresh();

                        btnflag = false;
                        tabModule.SelectedIndex = -1;
                        grpModule.Enabled = false;

                        if (tabServer.Enabled == true)
                        {
                            refreshflag = 1;
                            grpServer.Refresh();
                            grpDataInfo.Refresh();
                            grpModule.Refresh();
                        }
                        else
                        {
                            refreshflag = 0;
                            grpServer.Refresh();
                            grpDataInfo.Refresh();
                            grpModule.Refresh();
                        }                            
                    }
                }             
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if (crcflag)
                {
                    btnWRState(true);  
                    //tabModule.SelectedIndex = myLastPageNoMol;
                }
                else
                {
                    btnWrite.Enabled = false ;
                    //tabServer.SelectedIndex = -1;
                    btnRead.Enabled = true ;
                }

            }
        }
    
        void showDatas()
        {
            string ss = "--------DATA0,READDATA0--------\r\n";
            if (myEEPROMOperation.ReadData0 != null)
            {
                ReadData0 = myEEPROMOperation.ReadData0;
                for (int i = 0; i < Data0.Length; i++)
                {
                    ss += i.ToString("X").PadLeft(2, '0') + "  " + Data0[i].ToString("X").PadLeft(2, '0') + ":" + ReadData0[i].ToString("X").PadLeft(2, '0') + "\r\n";
                }
            }
            if (myEEPROMOperation.ReadData1 != null)
            {
                ReadData1 = myEEPROMOperation.ReadData1;

                ss += "--------DATA1,READDATA1--------\r\n";
                for (int i = 0; i < Data1.Length; i++)
                {
                    ss += i.ToString("X").PadLeft(2, '0') + "  " + Data1[i].ToString("X").PadLeft(2, '0') + ":" + ReadData1[i].ToString("X").PadLeft(2, '0') + "\r\n";
                }
            }
            if (myEEPROMOperation.ReadData2 != null)
            {
                ReadData2 = myEEPROMOperation.ReadData2;
                ss += "--------DATA2,READDATA2--------\r\n";
                for (int i = 0; i < Data2.Length; i++)
                {
                    ss += i.ToString("X").PadLeft(2, '0') + "  " + Data2[i].ToString("X").PadLeft(2, '0') + ":" + ReadData2[i].ToString("X").PadLeft(2, '0') + "\r\n";
                }
            }
            if (myEEPROMOperation.ReadData3 != null)
            {
                ReadData3 = myEEPROMOperation.ReadData3;

                ss += "--------DATA3,READDATA3--------\r\n";
                for (int i = 0; i < Data3.Length; i++)
                {
                    ss += i.ToString("X").PadLeft(2, '0') + "  " + Data3[i].ToString("X").PadLeft(2, '0') + ":" + ReadData3[i].ToString("X").PadLeft(2, '0') + "\r\n";
                }
            }

            FileStream fs;
            fs = new FileStream(Application.StartupPath + @"\ALLData.txt", FileMode.Create);
            StreamWriter sw = new StreamWriter(fs, Encoding.Default);

            sw.WriteLine(ss);
            sw.Close();
            fs.Close();            
           
        }


        /// <summary>
        /// 校验EEPROM资料 
        /// </summary>       
        /// <param name="isWriteData">isWriteData = true ? WriteData :ReadData </param>
        bool checkDatas(bool isWriteData)
        {
            grpModule.Enabled = true;
            bool result = false;
            try
            {
                int RowIndex = 0;
                int ColumnIndex = 0;

                #region QSFP
                if (cboPNType.Text.ToString().Trim().ToUpper() == "QSFP")
                {
                        result = true;

                        for (int i = 0; i < 0x80; i++)
                        {
                            if (ReadData1[i] != Data1[i])
                            {
                                RowIndex = i / 8;
                                ColumnIndex = i % 8;
                                dgvModule1.Rows[RowIndex].Cells[ColumnIndex].Style.BackColor = Color.Red;
                                tabModulePage1.Text = "A0H_Page1*";
                                result = false;
                            }
                        }

                        for (int i = 0; i < 0x80; i++)
                        {
                            if (ReadData2[i] != Data2[i])
                            {
                                RowIndex = i / 8;
                                ColumnIndex = i % 8;
                                dgvModule2.Rows[RowIndex].Cells[ColumnIndex].Style.BackColor = Color.Red;
                                tabModulePage2.Text = "A0H_Page2*";
                                result = false;
                            }
                        }

                        for (int i = 0; i < 0x60; i++)
                        {
                            if (ReadData3[i] != Data3[i])
                            {
                                RowIndex = i / 8;
                                ColumnIndex = i % 8;
                                dgvModule3.Rows[RowIndex].Cells[ColumnIndex].Style.BackColor = Color.Red;
                                tabModulePage3.Text = "A0H_Page3*";
                                result = false;
                            }
                        }

                        if (isWriteData)
                        {

                            for (int i = 0; i < 0x80; i++)
                            {
                                if (ReadData0[i] != Data0[i])
                                {
                                    RowIndex = i / 8;
                                    ColumnIndex = i % 8;                                 
                                    dgvModule0.Rows[RowIndex].Cells[ColumnIndex].Style.BackColor = Color.Red;
                                    tabModulePage0.Text = "A0H_Page0*";
                                    result = false;                                  
                                }                    
                            }
                        }
                        else 
                        {
                            for (int i = 0; i < 0x80; i++)
                            {
                                RowIndex = i / 8;
                                ColumnIndex = i % 8;

                                if (i >= 0x44 && i <= 0x5B)
                                {
                                    dgvModule0.Rows[RowIndex].Cells[ColumnIndex].Style.BackColor = Color.LightBlue;
                                    //dgvServer0.Rows[RowIndex].Cells[ColumnIndex].Style.BackColor = Color.LightBlue;
                                }
                                else if (i == 0x3f || i == 0x5f)
                                {
                                    dgvModule0.Rows[RowIndex].Cells[ColumnIndex].Style.BackColor = Color.LightBlue;
                                    //dgvServer0.Rows[RowIndex].Cells[ColumnIndex].Style.BackColor = Color.LightBlue;
                                }
                                else
                                {
                                    if (ReadData0[i] != Data0[i])
                                    {
                                        dgvModule0.Rows[RowIndex].Cells[ColumnIndex].Style.BackColor = Color.Red;
                                        tabModulePage0.Text = "A0H_Page0*";
                                        result = false;
                                    }
                                }                      
                            }
                        }
                }
                #endregion

                #region SFP
                else if(cboPNType.Text.ToString().Trim().ToUpper() == "SFP")
                {
                    result = true;

                    for (int i = 0; i < 0x60; i++)                   
                    {
                        if (ReadData1[i] != Data1[i])
                        {
                            RowIndex = i / 8;
                            ColumnIndex = i % 8;
                            dgvModule1.Rows[RowIndex].Cells[ColumnIndex].Style.BackColor = Color.Red;
                            tabModulePage1.Text = "A2HLowMemory*";
                            result = false;
                        }
                    }

                    for (int i = 0; i < 0x80; i++)
                    {
                        if (ReadData2[i] != Data2[i])
                        {
                            RowIndex = i / 8;
                            ColumnIndex = i % 8;
                            dgvModule2.Rows[RowIndex].Cells[ColumnIndex].Style.BackColor = Color.Red;
                            tabModulePage2.Text = "A2HPage0*";
                            result = false;
                        }
                    }

                    if (isWriteData)
                    {
                        for (int i = 0; i < 0x100; i++)
                        {
                            if (ReadData0[i] != Data0[i])
                            {
                                RowIndex = i / 8;
                                ColumnIndex = i % 8;
                                dgvModule0.Rows[RowIndex].Cells[ColumnIndex].Style.BackColor = Color.Red;
                                tabModulePage0.Text = "A0H*";
                                result = false;
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < 0x100; i++)
                        {
                            RowIndex = i / 8;
                            ColumnIndex = i % 8;

                            if (i >= 0x44 && i <= 0x5B)
                            {
                                dgvModule0.Rows[RowIndex].Cells[ColumnIndex].Style.BackColor = Color.LightBlue;
                                //dgvServer0.Rows[RowIndex].Cells[ColumnIndex].Style.BackColor = Color.LightBlue;
                            }
                            else if (i == 0x3f || i == 0x5f)
                            {
                                dgvModule0.Rows[RowIndex].Cells[ColumnIndex].Style.BackColor = Color.LightBlue;
                                //dgvServer0.Rows[RowIndex].Cells[ColumnIndex].Style.BackColor = Color.LightBlue;
                            }
                            else
                            {
                                if (ReadData0[i] != Data0[i])
                                {
                                    dgvModule0.Rows[RowIndex].Cells[ColumnIndex].Style.BackColor = Color.Red;
                                    tabModulePage0.Text = "A0H*";
                                    result = false;
                                }
                            }
                        }
                    }
                }

                #endregion

                #region XFP
                else if(cboPNType.Text.ToString().Trim().ToUpper() == "XFP")
                {
                    result = true;

                    for (int i = 0; i < 58; i++)
                    {
                        if (ReadData1[i] != Data1[i])
                        {
                            RowIndex = i / 8;
                            ColumnIndex = i % 8;
                            dgvModule1.Rows[RowIndex].Cells[ColumnIndex].Style.BackColor = Color.Red;
                            tabModulePage1.Text = "A0HLowMemory*";
                            result = false;
                        }
                    }

                    for (int i = 0; i < 0x80; i++)
                    {
                        if (ReadData2[i] != Data2[i])
                        {
                            RowIndex = i / 8;
                            ColumnIndex = i % 8;
                            dgvModule2.Rows[RowIndex].Cells[ColumnIndex].Style.BackColor = Color.Red;
                            tabModulePage2.Text = "A0HPage2*";
                            result = false;
                        }
                    }

                    if (isWriteData)
                    {
                        for (int i = 0; i < 0x80; i++)
                        {
                            if (ReadData0[i] != Data0[i])
                            {
                                RowIndex = i / 8;
                                ColumnIndex = i % 8;
                                dgvModule0.Rows[RowIndex].Cells[ColumnIndex].Style.BackColor = Color.Red;
                                tabModulePage0.Text = "A0HPage1*";
                                result = false;
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < 0x80; i++)
                        {
                            RowIndex = i / 8;
                            ColumnIndex = i % 8;

                            if (i >= 0x44 && i <= 0x5B)
                            {
                                dgvModule0.Rows[RowIndex].Cells[ColumnIndex].Style.BackColor = Color.LightBlue;
                                //dgvServer0.Rows[RowIndex].Cells[ColumnIndex].Style.BackColor = Color.LightBlue;
                            }
                            else if (i == 0x3f || i == 0x5f)
                            {
                                dgvModule0.Rows[RowIndex].Cells[ColumnIndex].Style.BackColor = Color.LightBlue;
                                //dgvServer0.Rows[RowIndex].Cells[ColumnIndex].Style.BackColor = Color.LightBlue;
                            }
                            else
                            {
                                if (ReadData0[i] != Data0[i])
                                {
                                    dgvModule0.Rows[RowIndex].Cells[ColumnIndex].Style.BackColor = Color.Red;
                                    tabModulePage0.Text = "A0HPage1*";
                                    result = false;
                                }
                            }
                        }
                    }
                }
                #endregion
                else if (cboPNType.Text.ToString().Trim().ToUpper() == "CFP")
                {
                
                }
              
                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return result;
            }
        }
    
        private void Mainform_FormClosing(object sender, FormClosingEventArgs e)
        {
                //updateUserLoginInfo(myDataIO.GetCurrTime().ToString(), true, "");    //141022_0
        }

        private void txtWriteSN_KeyUp(object sender, KeyEventArgs e)
        {
           byte[] SN = System.Text.Encoding.ASCII.GetBytes(txtWriteSN.Text);
            int length = SN.Length;

            if (length == 0)
            {
                refreshWriteInfo("");
                refreshWriteDGV();
            }


            for (int i = 0; i < length; i++)
            {
                if ((SN[i] >= 48 && SN[i] <= 57) || (SN[i] >= 65 && SN[i] <= 90) || (SN[i] >= 97 && SN[i] <= 122) || SN[i] == 0 || SN[i] == 32)
                {
                    if (txtWriteSN.Text.Trim().Length > 0)
                    {
                        refreshWriteInfo(txtWriteSN.Text.Trim());
                        //refreshWriteDGV();
                        refreshdgvData(dgvServer0, Data0, myEEPROMOperation.Data0length);
                    }
                }
                else
                {
                    MessageBox.Show("The SN you input is not correct, please re-enter!");
                    refreshWriteInfo("");
                    //refreshWriteDGV();
                    txtWriteSN.Text = "";
                    break;
                }
            }
        }

        private void txtWriteSN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnWrite_Click(sender, e);
            }
        }

        private void tabModule_MouseClick(object sender, MouseEventArgs e)
        {
            txtPN.Text = "";
            txtRev.Text = "";
            txtWriteSN.Text = "";
            txtWriteDC.Text = "";
            txtVendorName.Text = "";
            txtVOUI.Text = "";

            for (int i = 0; i < 15; i++)
            {
                txtWriteSN.Text += ((char)ReadData0[0x44 + i]).ToString();
            }

            for (int i = 0; i < 8; i++)
            {
                txtWriteDC.Text += ((char)ReadData0[0x54 + i]).ToString();
            }

            for (int i = 0; i < 15; i++)
            {
                txtVendorName.Text += ((char)ReadData0[0x14 + i]).ToString();
            }
            for (int i = 0; i < 3; i++)
            {
                txtVOUI.Text += (ReadData0[0x25 + i]).ToString("X").PadLeft(2, '0');  //141027_2
            }
            for (int i = 0; i < 15; i++)
            {
                txtPN.Text += ((char)ReadData0[0x28 + i]).ToString();
            }
            for (int i = 0; i < 2; i++)
            {
                txtRev.Text += ((char)ReadData0[0x38 + i]).ToString();
            }

            txtPN.Text = txtPN.Text.Trim();
            txtRev.Text = txtRev.Text.Trim();
            txtWriteSN.Text = txtWriteSN.Text.Trim();
            txtWriteDC.Text = txtWriteDC.Text.Trim();
            txtVendorName.Text = txtVendorName.Text.Trim();
            txtVOUI.Text = txtVOUI.Text.Trim();

            refreshflag = 2;
            grpServer.Refresh();
            grpDataInfo.Refresh();
            grpModule.Refresh();      
        }

        private void tabServer_MouseClick(object sender, MouseEventArgs e)
        {
            if (refreshflag != 1)
            {
                refreshWriteALL();

                refreshflag = 1;
                grpServer.Refresh();
                grpDataInfo.Refresh();
                grpModule.Refresh();
            }
        }

        private void grpDataInfo_Paint(object sender, PaintEventArgs e)
        {
            if (refreshflag == 1)
            {
                e.Graphics.DrawLine(Pens.RoyalBlue, 1, 10, 8, 10);

                e.Graphics.DrawLine(Pens.RoyalBlue, e.Graphics.MeasureString(grpDataInfo.Text, grpDataInfo.Font).Width + 13, 10, grpDataInfo.Width - 2, 10);

                e.Graphics.DrawLine(Pens.RoyalBlue, 1, 10, 1, grpDataInfo.Height - 2);

                e.Graphics.DrawLine(Pens.RoyalBlue, 1, grpDataInfo.Height - 2, grpDataInfo.Width - 2, grpDataInfo.Height - 2);

                e.Graphics.DrawLine(Pens.RoyalBlue, grpDataInfo.Width - 2, 10, grpDataInfo.Width - 2, grpDataInfo.Height - 2); 
            }
            else if (refreshflag == 2)
            {
                e.Graphics.DrawLine(Pens.LimeGreen, 1, 10, 8, 10);

                e.Graphics.DrawLine(Pens.LimeGreen, e.Graphics.MeasureString(grpDataInfo.Text, grpDataInfo.Font).Width + 13, 10, grpDataInfo.Width - 2, 10);

                e.Graphics.DrawLine(Pens.LimeGreen, 1, 10, 1, grpDataInfo.Height - 2);

                e.Graphics.DrawLine(Pens.LimeGreen, 1, grpDataInfo.Height - 2, grpDataInfo.Width - 2, grpDataInfo.Height - 2);

                e.Graphics.DrawLine(Pens.LimeGreen, grpDataInfo.Width - 2, 10, grpDataInfo.Width - 2, grpDataInfo.Height - 2); 
            }
        }

        private void grpServer_Paint(object sender, PaintEventArgs e)
        {
            if (refreshflag == 1)
            {
                e.Graphics.DrawLine(Pens.RoyalBlue, 1, 10, 8, 10);

                e.Graphics.DrawLine(Pens.RoyalBlue, e.Graphics.MeasureString(grpServer.Text, grpServer.Font).Width + 13, 10, grpServer.Width - 2, 10);

                e.Graphics.DrawLine(Pens.RoyalBlue, 1, 10, 1, grpServer.Height - 2);

                e.Graphics.DrawLine(Pens.RoyalBlue, 1, grpServer.Height - 2, grpServer.Width - 2, grpServer.Height - 2);

                e.Graphics.DrawLine(Pens.RoyalBlue, grpServer.Width - 2, 10, grpServer.Width - 2, grpServer.Height - 2);

                
      
            }
        }

        private void grpModule_Paint(object sender, PaintEventArgs e)
        {            
            if (refreshflag == 2)
            {
                e.Graphics.DrawLine(Pens.LimeGreen, 1, 10, 8, 10);

                e.Graphics.DrawLine(Pens.LimeGreen, e.Graphics.MeasureString(grpModule.Text, grpModule.Font).Width + 13, 10, grpModule.Width - 2, 10);

                e.Graphics.DrawLine(Pens.LimeGreen, 1, 10, 1, grpModule.Height - 2);

                e.Graphics.DrawLine(Pens.LimeGreen, 1, grpModule.Height - 2, grpModule.Width - 2, grpModule.Height - 2);

                e.Graphics.DrawLine(Pens.LimeGreen, grpModule.Width - 2, 10, grpModule.Width - 2, grpModule.Height - 2);             
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using ATS;
using ATS_Framework;
using ATS_Driver;
using System.Threading;

namespace Semi_Automatic_AdjustEye
{
    public partial class Semi_Automatic_AdjustEye : Form
    {

        public class ProductionParameter
        {
            public int IdPnType;
            public int IdProductionName;
            public string PnName;
            public string ItemName;
            public int ID_MCoefs;
            public bool OldDriver;
            //public double MaxRate;
            //public String Publish_PN;
            //public string NickName;
            public bool UsingCelsiusTemp;

            //public string SerialNo;
            public string ProductionTypeName;
            public DataTable dtProductionType = new DataTable();
            public DataTable dtProductionName = new DataTable();
        }
        public EquipConfig pEquipConfig;
        public DUT pDut;


        //public class AdjustItem
        //{
        //    public bool IsAdjustDC = false;
        //    public bool IsAdjustMod = false;
        //    public bool IsAdjustCrossing = false;
        //    public bool IsAdjustJitter = false;
        //    public bool IsAdjustMask = false;
        //    public bool IsAdjustTEC = false;
        //    public bool IsAdjustVc = false;
        //    public bool IsAdjustVG = false;
        //    public bool IsAdjustVLD = false;
        //    public bool IsAdjustAPD = false;
        //}

        public ConfigXmlIO MyXml;

        public SqlDatabase MySql;
        
        public logManager MylogManager = new logManager();
        DataTable DtMyDutInf = new DataTable();
        SortedList<string, CombinatControl> NewControl = new SortedList<string, CombinatControl>();
        EquipmentBase MyEquipmentBase = new EquipmentBase();
        private Algorithm Myalgorithm = new Algorithm();
        public ProductionParameter MyProductionParameter = new ProductionParameter();
        public DataTable DtData = new DataTable();
        public DriverStruct[] MyDriverStruct;
        public DutStruct[] DutInfStruct;
        public ArrayList CheckedItemsName = new ArrayList();
        public ArrayList PNItemNames = new ArrayList();
        //byte[] CheckedItemsLengths;
        //public ArrayList PNItemLengths = new ArrayList();
        public int ItemCount;
        public string ItemName;

        CombinatControl C = new CombinatControl();

        public Semi_Automatic_AdjustEye()
        {
            InitializeComponent();
            gbDACBar.Enabled = false;
            gbDCAOffset.Enabled = false;
            cbPowerClose.Enabled = false;
            btAutoScale.Enabled = false;
            btRun.Enabled = false;
            cbXstreamUpDown.Enabled = false;
            cbPowerClose.Checked = false;
        }

        private void Semi_Automatic_AdjustEye_Load(object sender, EventArgs e)
        {

            ReadXml();
            //       public SqlDatabase(string serverName, string dbName, string user, string pwd)
            var ss1 = ReadProductionTpye();

            comboBoxPType.Items.Clear();

            foreach (string s in ss1)
            {
                comboBoxPType.Items.Add(s);
            }

            this.Text = "Semi-Automatic-AdjustEye_" + MyXml.DbName;
            tbOffset1.Text = MyXml.DCAOffset1;
            tbOffset2.Text = MyXml.DCAOffset2;
            tbOffset3.Text = MyXml.DCAOffset3;
            tbOffset4.Text = MyXml.DCAOffset4;

            tbTempLow.Text = MyXml.TempLow;
            tbTempAMB.Text = MyXml.TempAMB;
            tbTempHigh.Text = MyXml.TempHigh;
        }

      #region  Funtion
         public string[] ReadProductionTpye()
        {
          
            try
            {
                if (MySql.OpenDatabase(true))
                {
                    string StrTableName = "GlobalProductionType";
                    string Selectconditions = "select * from " + StrTableName + " Where IgnoreFlag='false' order by ID";
                    MyProductionParameter.dtProductionType.Clear();
                    MyProductionParameter.dtProductionType = MySql.GetDataTable(Selectconditions, StrTableName); ;
                    string[] arry = MyProductionParameter.dtProductionType.AsEnumerable().Select(d => d.Field<string>("ItemName")).ToArray();
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

        private void ReadXml()
        {
            MyXml = new ConfigXmlIO(Application.StartupPath + "\\Config.xml");
            MyXml.DatabaseType = MyXml.DatabaseType;
            MySql = new SqlDatabase(MyXml.DatabasePath, MyXml.DbName, MyXml.Username, MyXml.PWD);

            pEquipConfig = new EquipConfig(MyXml);
        }

         private void comboBoxPType_SelectedIndexChanged(object sender, EventArgs e)
         {
             comboBoxPN.Items.Clear();
          
             #region     Get the id of Current ProductType
              MyProductionParameter.ProductionTypeName = comboBoxPType.SelectedItem.ToString();
             //  MyCommonParameter.MyProductionParameter.ProductionTypeName = comboBoxPType.SelectedItem.ToString();

          //  string  ProductionTypeName = comboBoxPType.SelectedItem.ToString();
             DataTable dd = new DataTable();
             //  string[] arry = dtProductionType.AsEnumerable().Select(d => d.Field<string>("ItemName")).ToArray();
             DataRow[] dr = MyProductionParameter.dtProductionType.Select("ItemName='" + MyProductionParameter.ProductionTypeName + "'");

            MyProductionParameter.IdPnType = Convert.ToInt32(dr[0]["ID"].ToString());
           

             #endregion

             #region  Fit ProductionName Combox

            string Str = "Select* from GlobalProductionName where PID=" + MyProductionParameter.IdPnType + " and IgnoreFlag='false' order by id";
             string sTRtb = "GlobalProductionName";
             dd = MySql.GetDataTable(Str, sTRtb);
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
                MyProductionParameter.PnName = comboBoxPN.SelectedItem.ToString();
                 DataTable dd = new DataTable();
                 string Str = "Select* from GlobalProductionName where GlobalProductionName.pid=" + MyProductionParameter.IdPnType + " and GlobalProductionName.IgnoreFlag='false' and GlobalProductionName.PN='" + MyProductionParameter.PnName + "' order by GlobalProductionName.id";
                 MyProductionParameter.dtProductionName = MySql.GetDataTable(Str, "GlobalProductionName");
                 DataRow[] dr = MyProductionParameter.dtProductionName.Select("PN='" + MyProductionParameter.PnName + "'");//MyCommonParameter.MyProductionParameter.ProductionTypeName
                 MyProductionParameter.IdProductionName = Convert.ToInt32(dr[0]["ID"].ToString());

                 MyProductionParameter.ItemName = dr[0]["ItemName"].ToString();
                 MyProductionParameter.ID_MCoefs = Convert.ToInt16(dr[0]["MCoefsID"]);
                 MyProductionParameter.OldDriver = Convert.ToBoolean(dr[0]["OldDriver"]);
              
                 #region UsingCelsiusTemp 由于数据库更改，我们对其取反
                 MyProductionParameter.UsingCelsiusTemp = Convert.ToBoolean(dr[0]["UsingCelsiusTemp"]);
                 //  pflowControl.pGlobalParameters.UsingCelsiusTemp=!pflowControl.pGlobalParameters.UsingCelsiusTemp;
                 #endregion

            

                 #endregion

               //  DriverStruct[] A = GetManufactureChipsetControl();
                 DtData.Clear();
                 DgvData.Rows.Clear();
                 
                 ConfigDut(); 

                 selectItems();

                 if (pDut.APCON(0x11))
                 {
                     btAPCControl.BackColor = Color.SpringGreen;
                 }
             }
         }

         private bool ConfigDut()
         {
             EquipmentManage MyEquipmentManage = new EquipmentManage(MylogManager);


             DutStruct[] MyDutManufactureCoefficientsStructArray;
             DriverStruct[] MyManufactureChipsetStructArray;
             DriverInitializeStruct[] MyDutManufactureChipSetInitialize;
             DutEEPROMInitializeStuct[] MyDutEEPROMInitializeStuct;

             pDut = (DUT)MyEquipmentManage.Createobject(MyProductionParameter.ProductionTypeName.ToUpper() + "DUT");
             pDut.deviceIndex = 0;
             pDut.ChipsetControll =MyProductionParameter.OldDriver;
             MyDutManufactureCoefficientsStructArray = GetDutManufactureCoefficients();
             MyManufactureChipsetStructArray = GetManufactureChipsetControl();
             MyDutManufactureChipSetInitialize = GetManufactureChipsetInitialize(); //等待数据库结构统一
             MyDutEEPROMInitializeStuct = Get_EEPROM_Init_FromSql();
             return pDut.Initialize(MyDutManufactureCoefficientsStructArray, MyManufactureChipsetStructArray, MyDutManufactureChipSetInitialize, MyDutEEPROMInitializeStuct, "");//等待Driver 跟上

         }
         private DutStruct[] GetDutManufactureCoefficients()
         {
             
             int i = 0;
             // protected DutStruct[] MyDutStruct;
             string StrTableName = "GlobalManufactureCoefficients";

             string StrSelectconditions = "select * from " + StrTableName + " where PID= " + MyProductionParameter.ID_MCoefs + " order by ID";
             DtMyDutInf = MySql.GetDataTable(StrSelectconditions, StrTableName);// 获得环境的DataTable

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
         private DriverInitializeStruct[] GetManufactureChipsetInitialize()
         {
             DriverInitializeStruct[] MyStruct;
             int i = 0;
             string StrTableName = "GlobalManufactureChipsetInitialize";
             string StrSelectconditions = "select * from " + StrTableName + " where PID= " + MyProductionParameter.IdProductionName + " order by ID";
             DtMyDutInf = MySql.GetDataTable(StrSelectconditions, StrTableName);// 获得环境的DataTable

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
         private DriverStruct[] GetManufactureChipsetControl()
         {
             int i = 0;
             // protected DutStruct[] MyDutStruct;

             string StrTableName = "GlobalManufactureChipsetControl";

             string StrSelectconditions = "select * from " + StrTableName + " where PID= " + MyProductionParameter.IdProductionName + " order by ID";
             DtMyDutInf = MySql.GetDataTable(StrSelectconditions, StrTableName);// 获得环境的DataTable

             MyDriverStruct = new DriverStruct[DtMyDutInf.Rows.Count];
             // MyDutStructArray.
             foreach (DataRow dr in DtMyDutInf.Rows)
             {
                 DriverStruct dutDriverStruct = new DriverStruct();
                 dutDriverStruct.FiledName = Convert.ToString(dr["ItemName"]).Trim().ToUpper();
                 dutDriverStruct.MoudleLine = Convert.ToByte(dr["ModuleLine"]);
                 dutDriverStruct.ChipLine = Convert.ToByte(dr["ChipLine"]);
                 dutDriverStruct.DriverType = Convert.ToByte(dr["DriveType"]);
                 dutDriverStruct.RegisterAddress = Convert.ToInt16(dr["RegisterAddress"]);//RegisterAddress

                 dutDriverStruct.Length = Convert.ToByte(dr["Length"]);//         RegisterAddress
                 dutDriverStruct.StartBit = Convert.ToByte(dr["StartBit"]);
                 dutDriverStruct.EndBit = Convert.ToByte(dr["EndBit"]);
                 MyDriverStruct[i] = dutDriverStruct;

                 i++;
             }

             return MyDriverStruct;
         }

         private DutEEPROMInitializeStuct[] Get_EEPROM_Init_FromSql()
         {
             return null;
         }

          private bool DisplayGbox(GroupBox pGbox, HScrollBar pHScrollBar, bool IsDisplay,Point pPoint)
         {
             pGbox.Left = pPoint.X;
             pGbox.Top = pPoint.Y;
             return true;
         }

        private void cbPowerClose_CheckedChanged(object sender, EventArgs e)
        {
            if (cbPowerClose.Checked == true)
            {
                if (pEquipConfig.E3631Power.EquipmentConnectflag == true)
                {
                    pEquipConfig.E3631Power.OutPutSwitch(false);
                }
                else
                {
                    pEquipConfig.DP811APower.OutPutSwitch(false);
                }
            }
            else
            {
                if (pEquipConfig.E3631Power.EquipmentConnectflag == true)
                {
                    pEquipConfig.E3631Power.OutPutSwitch(true);
                }
                else
                {
                    pEquipConfig.DP811APower.OutPutSwitch(true);
                }
            }
        }

        public void btSet_Click(object sender, EventArgs e)
        {
            gbDACBar.Enabled = true;

            pDut.ChangeChannel(cbChannel.Text.ToString());
            if (pEquipConfig.FlagEquipmentConfigOK == true)
            {
                if (pEquipConfig.FlexScope.EquipmentConnectflag == true)
                {
                    pEquipConfig.FlexScope.ChangeChannel(cbChannel.Text.ToString());
                }
                if (pEquipConfig.OptSwitch.EquipmentConnectflag == true)
                {
                    pEquipConfig.OptSwitch.ChangeChannel(cbChannel.Text.ToString(), 1);
                }
                if (pEquipConfig.XSteam.EquipmentConnectflag == true)
                {
                    if(cbTempLow.Checked == true)
                    {
                        pEquipConfig.XSteam.SetPointTemp(Convert.ToDouble(tbTempLow.Text), 1);
                    }
                    else if (cbTempAMB.Checked == true)
                    {
                        pEquipConfig.XSteam.SetPointTemp(Convert.ToDouble(tbTempAMB.Text), 1);
                    }
                    else if (cbTempHigh.Checked == true)
                    {
                        pEquipConfig.XSteam.SetPointTemp(Convert.ToDouble(tbTempHigh.Text), 1);
                    }
                }
            }
            else
            {
                MessageBox.Show("Equipment not Configured.");
            }

            MyXml.TempLow = tbTempLow.Text;
            MyXml.TempAMB = tbTempAMB.Text;
            MyXml.TempHigh = tbTempHigh.Text;

            EnterEngModeAndAPCOff();

            ////读取Dac初始值到TextBox以及Bar;
            for (int i = 0; i < ItemCount; i++)
            {
                ItemName = CheckedItemsName[i].ToString();
                if(NewControl.Keys.Contains(ItemName))
                {
                    int ReadDac = ReadItemDac(ItemName);
                    NewControl[ItemName].ItemHScrollBar.Value = ReadDac;
                    NewControl[ItemName].ItemTextBox.Text = ReadDac.ToString();
                    NewControl[ItemName].ItemTextBox.BackColor = Color.WhiteSmoke;
                    NewControl[ItemName].LastValue = ReadDac;
                }
            }
        }

        private void btConfirm_Click(object sender, EventArgs e)
        {
            NewControl.Clear();
            if (gbDACBar.Controls != null)
            {
                this.gbDACBar.Controls.Clear();
                ItemCount = 0;
            }
            Point P;
            Int32 MaxDACValue = 100;
            int index;
            
            CheckBox[] Items = {chk_BIAS, chk_Mod, chk_Mask, chk_Jitter, chk_Cross, chK_APD, chk_Los, chK_LosD, chk_Tec, chk_Vc, chk_VG ,chK_VLD,chk_EA};
            CheckBox[] CheckedItems = new CheckBox[PNItemNames.Count];
            int checkedCount = 0;
            //CheckedItemsLengths = new byte[PNItemNames.Count];

            for (int i = 0; i < PNItemNames.Count; i++)
            {
                foreach (CheckBox Item in Items)
                {
                    if (Item.Checked && (PNItemNames[i].ToString() == (Item.Name.Split('_')[1].ToUpper()+"DAC")))
                    {
                        CheckedItems[checkedCount] = Item;
                        //CheckedItemsLengths[checkedCount] = Convert.ToByte(PNItemLengths[i]);
                        checkedCount++;
                        CheckedItemsName.Add(PNItemNames[i]);
                        break;
                    }
                }
            }    

            for (int i = 0; i < checkedCount; i++)
            {
                ItemName = CheckedItemsName[i].ToString();
                if (Myalgorithm.FindFileName(MyDriverStruct, ItemName, out index))
                {
                    MaxDACValue = Convert.ToInt32(System.Math.Pow(2, MyDriverStruct[index].Length * 8) - 1);
                }
                
                if(i<4)
                {
                    P = new Point(20, 30 + 50 * ItemCount);
                }
                else
                {
                    P = new Point(350, 30+ 50*(ItemCount - 4));
                }

                switch(ItemName)
                {
                    case"BIASDAC":
                        C = new CombinatControl(ItemName, P, MaxDACValue, pDut.WriteBiasDac);
                        break;
                    case "MODDAC":
                        C = new CombinatControl(ItemName, P, MaxDACValue, pDut.WriteModDac);
                        break;
                    case "MASKDAC":
                        C = new CombinatControl(ItemName, P, MaxDACValue, pDut.WriteMaskDac);
                        break;
                    case "JITTERDAC":
                        //C = new CombinatControl(ItemName, P, MaxDACValue, MyEquipment.pDut.WriteDac);
                        break;
                    case "CROSSDAC":
                        C = new CombinatControl(ItemName, P, MaxDACValue, pDut.WriteCrossDac);
                        break;
                    case "APDDAC":
                        C = new CombinatControl(ItemName, P, MaxDACValue, pDut.WriteAPDDac);
                        break;
                    case "LOSDAC":
                        C = new CombinatControl(ItemName, P, MaxDACValue, pDut.WriteLOSDac);
                        break;
                    case "LOSDDAC":
                        C = new CombinatControl(ItemName, P, MaxDACValue, pDut.WriteLOSDDac);
                        break;
                    case "TECDAC":
                        //C = new CombinatControl(ItemName, P, MaxDACValue, MyEquipment.pDut.WriteDac);
                        break;
                    case "VCDAC":
                        //C = new CombinatControl(ItemName, P, MaxDACValue, MyEquipment.pDut.WriteVC);
                        break;
                    case "VGDAC":
                        //C = new CombinatControl(ItemName, P, MaxDACValue, MyEquipment.pDut.WriteVG(1,));
                        break;
                    case "VLDDAC":
                        //C = new CombinatControl(ItemName, P, MaxDACValue, MyEquipment.pDut.WriteVLD(1,));
                        break;
                    case "EADAC":
                        C = new CombinatControl(ItemName, P, MaxDACValue, pDut.WriteEA);
                        break;
                    default:
                        break;
                }
                if (!NewControl.ContainsKey(ItemName))
                {
                    NewControl.Add(ItemName, C);
                }
                ItemCount++;
                gbDACBar.Controls.Add(C.ItemHScrollBar);
                gbDACBar.Controls.Add(C.ItemLable);
                gbDACBar.Controls.Add(C.ItemTextBox);
                gbDACBar.Controls.Add(C.MaxValueLable);
                            
            }
        }

        private void EnterEngModeAndAPCOff()
        {
            pDut.Engmod(1);
            if (pDut.APCOFF(0x11))
            {
                btAPCControl.BackColor = Color.Red;
            }
        }

        private ArrayList selectItems()
        {
            IEnumerable<Control> query = this.gbItems.Controls.Cast<Control>();
            foreach (Control Control in query)
            {
                Control.Enabled = false;
                PNItemNames.Clear();
            }
            for (int i = 0; i < MyDriverStruct.Length; i++)
            {
                //byte ItemLength = 0;
                string ItemName = "";
                ItemName = MyDriverStruct[i].FiledName;
                if (!PNItemNames.Contains(ItemName))
                {
                    //ItemLength = MyDriverStruct[i].Length;
                    EnableCheckBox(ItemName);
                    PNItemNames.Add(ItemName);
                    //PNItemLengths.Add(ItemLength);
                }
            }
            return PNItemNames;
        }

        private bool EnableCheckBox(string ItemName)
        {
            switch (ItemName.ToUpper().Trim())
            {
                case "BIASDAC":
                    ControlCheckBox(this.chk_BIAS, true);
                    break;
                case "MODDAC":
                    ControlCheckBox(this.chk_Mod, true);
                    break;
                case "MASKDAC":
                    ControlCheckBox(this.chk_Mask, true);
                    break;
                case "JITTERDAC":
                    ControlCheckBox(this.chk_Jitter, true);
                    break;
                case "CROSSDAC":
                    ControlCheckBox(this.chk_Cross, true);
                    break;
                case "APDDAC":
                    ControlCheckBox(this.chK_APD, true);
                    break;
                case "LOSDAC":
                    ControlCheckBox(this.chk_Los, true);
                    break;
                case "LOSDDAC":
                    ControlCheckBox(this.chK_LosD, true);
                    break;
                case "TECDAC":
                    ControlCheckBox(this.chk_Tec, true);
                    break;
                case "VCDAC":
                    ControlCheckBox(this.chk_Vc, true);
                    break;
                case "VGDAC":
                    ControlCheckBox(this.chk_VG, true);
                    break;
                case "VLDDAC":
                    ControlCheckBox(this.chK_VLD, true);
                    break;
                case "EADAC":
                    ControlCheckBox(this.chk_EA, true);
                    break;
                default:
                    break;
            }
            return true;
        }

        private bool ControlCheckBox(CheckBox pbox, bool IsEnable)
        {
            pbox.Enabled = IsEnable;
            //pbox.Refresh();
            return true;
        }

        private void UpdataTestData(DataGridView Dgv, DataTable dt)
        {
            if (Dgv.ColumnCount == 0)
            {
                for (int k = 0; k < dt.Columns.Count; k++)
                {
                    Dgv.Columns.Add(dt.Columns[k].ColumnName, dt.Columns[k].ColumnName);
                    // Dgv.Columns.Add()
                }
            }

            for (int i = (Dgv.Rows.Count-1); i < dt.Rows.Count; i++)
            {
                string[] TestData = new string[dt.Columns.Count];

                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    TestData[j] = dt.Rows[i][j].ToString();
                }
                Dgv.Rows.Add(TestData);
            }

            foreach (DataGridViewColumn cgvc in Dgv.Columns)
            {
                cgvc.Width = 82;
            }
        }

        private void btWriteToTable_Click(object sender, EventArgs e)
        {
            ushort TempADC;
            pDut.ReadTempADC(out TempADC);
            if (DtData.Columns.Count == 0)
            {
                DtData.Columns.Add("Temp");
                DtData.Columns.Add("CH");
                DtData.Columns.Add("ItemName");
                DtData.Columns.Add("ItemValue");
                DtData.Columns.Add("TempName");
                DtData.Columns.Add("TempValue");
            }
            //DataRow drdata = DtData.NewRow();

            for (int i = 0; i < ItemCount; i++)
            {
                DataRow drdata = DtData.NewRow();
                if (cbTempLow.Checked == true)
                {
                    drdata["Temp"] = tbTempLow.Text.ToString();
                }
                else if(cbTempAMB.Checked == true)
                {                    
                    drdata["Temp"] = tbTempAMB.Text.ToString();
                }
                else if(cbTempHigh.Checked == true)
                {
                    drdata["Temp"] = tbTempHigh.Text.ToString();
                }
                else
                {
                    MessageBox.Show("There is no Temp.");
                }

                drdata["CH"] = cbChannel.Text.ToString();
                ItemName = CheckedItemsName[i].ToString();
                drdata["ItemName"] = ItemName;
                drdata["ItemValue"] = ReadItemDac(ItemName);
                drdata["TempName"]= "TempADC";
                drdata["TempValue"] = TempADC;
                DtData.Rows.Add(drdata);
            }
            UpdataTestData(DgvData, DtData);
        }

        private int ReadItemDac(string ItemName)
        {
            int ReadDac =-1;
            switch (ItemName)
            {
                case "BIASDAC":
                    ReadDac = pDut.ReadBiasDac();
                    break;
                case"MODDAC":
                    ReadDac = pDut.ReadModDac();
                    break;
                case "MASKDAC":
                    //ReadDac = MyEquipment.pDut.ReadDac();
                    break;
                case "JITTERDAC":
                    //ReadDac = MyEquipment.pDut.ReadDac();
                    break;
                case "CROSSDAC":
                    //ReadDac = MyEquipment.pDut.ReadCrossDac();
                    break;
                case "APDDAC":
                    ReadDac = pDut.ReadAPDDac();
                    break;
                case "LOSDAC":
                    ReadDac = pDut.ReadLOSDac();
                    break;
                case "LOADDAC":
                    //ReadDac = MyEquipment.pDut.ReadDac();
                    break;
                case "TECDAC":
                    //ReadDac = MyEquipment.pDut.ReadDac();
                    break;
                case "VCDAC":
                    //ReadDac = MyEquipment.pDut.ReadDac();
                    break;
                case "VGDAC":
                    //ReadDac = MyEquipment.pDut.ReadDac();
                    break;
                case "VLDDAC":
                    //ReadDac = MyEquipment.pDut.ReadDac();
                    break;
                case "EADAC":
                    //ReadDac = MyEquipment.pDut.ReadDac();
                    break;
                default:
                    break;
            }
            return ReadDac;
        }

        private DataTable GetFilterTable(string ItemName,string Currentchannel)
        {
            DataTable FiterTable = new DataTable();
            //FiterTable = DtData.Clone();
            //DataRow[] FilterRow = DtData.Select("ItemName='" + ItemName + "'");
            //for (int i = 0; i < FilterRow.Length;i++ )
            //{
            //    FilterRow[i] = FiterTable.NewRow();
            //    FiterTable.Rows.Add(FilterRow[i]);
            //}
            FiterTable = DtData.Copy();
            for (int i = 0; i < FiterTable.Rows.Count; i++)
            {
                if (FiterTable.Rows[i]["ItemName"].ToString() != ItemName)
                {
                    FiterTable.Rows.Remove(FiterTable.Rows[i]);
                }
            }
            for (int i = 0; i < FiterTable.Rows.Count; i++)
            {
                if (FiterTable.Rows[i]["CH"].ToString() != Currentchannel)
                {
                    FiterTable.Rows.Remove(FiterTable.Rows[i]);
                }
            }
            //DtData.DefaultView.RowFilter= ItemName;
            //DtData.DefaultView.RowFilter = channel;
            return FiterTable;
        }

        private double[] GetFittingData(DataTable FilterTable,string ColumnName)
        {
            double []FittingData = new double [FilterTable.Rows.Count];
            for(int i=0;i<FilterTable.Rows.Count;i++)
            {
                FittingData[i] = Convert.ToDouble(FilterTable.Rows[i][ColumnName]);
            }
            return FittingData;
        }

        ///拟 合
        private void Fitting(string Currentchannel)
        {
            double []Coefs = new double [3];
            DataView view =new DataView(DtData);
            string[] columns = { "ItemName","CH" ,"Temp"};
            DataTable ItemNameTable = view.ToTable(true,columns[0]);
            DataTable CHTable = view.ToTable(true,columns[1]);
            DataTable TempTable = view.ToTable(true, columns[2]);
            string[] ItemNameArray = ItemNameTable.AsEnumerable().Select(d => d.Field<string>("ItemName")).ToArray();
            string[] CHArray = CHTable.AsEnumerable().Select(d => d.Field<string>("CH")).ToArray();
            string[] TempArray = TempTable.AsEnumerable().Select(d => d.Field<string>("Temp")).ToArray();

            if (TempArray.Length < 2)
            {
                MessageBox.Show("Fitting conditions are less");
                MylogManager.AdapterLogString(3, "Fitting conditions are less");
            }
            else
            {
                for(int i=0;i<ItemNameArray.Length;i++)
                {
                    DataTable FilterTable = new DataTable();
                    if (CHArray.Contains(Currentchannel))
                    {
                        pDut.ChangeChannel(Currentchannel);
                        FilterTable = GetFilterTable(ItemNameArray[i], Currentchannel);
                        double[] TempValueArray = GetFittingData(FilterTable, "TempValue");
                        double[] ItemValueArray = GetFittingData(FilterTable, "ItemValue");
                        Coefs = Myalgorithm.MultiLine(TempValueArray, ItemValueArray, FilterTable.Rows.Count, 2);
                        Array.Reverse(Coefs);

                        string[] ItemValues = new string[ItemValueArray.Length];
                        string[] TempValues = new string[ItemValueArray.Length];
                        MylogManager.AdapterLogString(0, "The ItemValueArray and TempValueArray"+" in CH" + Currentchannel + " are: \n");
                        for (int k = 0; k < ItemValueArray.Length;k++ )
                        {
                            ItemValues[k] = ItemValueArray[k].ToString();
                            TempValues[k] = TempValueArray[k].ToString();
                            MylogManager.AdapterLogString(0, ItemValues[k] + " "+ TempValues[k] + "\n");
                        }
                        MylogManager.AdapterLogString(0, "The Coefs of " + ItemNameArray[i] + " in CH" + Currentchannel + " are " + Coefs[0] + "," + Coefs[1] + "," + Coefs[2]);
                    
                        if (!WriteCoefsToDUT(ItemNameArray[i], Coefs[0].ToString(), Coefs[1].ToString(), Coefs[2].ToString()))
                        {
                            MessageBox.Show("Coefs Write Error.");
                        }
                    }
                    else if (CHArray.Length == 4 && Currentchannel == "0")
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            Currentchannel = j.ToString();

                            pDut.ChangeChannel(Currentchannel);
                            FilterTable = GetFilterTable(ItemNameArray[i], Currentchannel);
                            double[] TempValueArray = GetFittingData(FilterTable, "TempValue");
                            double[] ItemValueArray = GetFittingData(FilterTable, "ItemValue");
                            Coefs = Myalgorithm.MultiLine(TempValueArray, ItemValueArray, FilterTable.Rows.Count, 2);
                            System.Array.Reverse(Coefs);

                            string[] ItemValues = new string[ItemValueArray.Length];
                            string[] TempValues = new string[ItemValueArray.Length];
                            MylogManager.AdapterLogString(0, "The ItemValueArray and TempValueArray" + " in CH" + Currentchannel + " are: \n");
                            for (int k = 0; k < ItemValueArray.Length; k++)
                            {
                                ItemValues[k] = ItemValueArray[k].ToString();
                                TempValues[k] = TempValueArray[k].ToString();
                                MylogManager.AdapterLogString(0, ItemValues[k] + " " + TempValues[k] + "\n");
                            }
                            MylogManager.AdapterLogString(0, "The Coefs of " + ItemNameArray[i] + " in CH" + Currentchannel + " are " + Coefs[0] + "," + Coefs[1] + "," + Coefs[2]);
                            if (!WriteCoefsToDUT(ItemNameArray[i], Coefs[0].ToString(), Coefs[1].ToString(), Coefs[2].ToString()))
                            {
                                MessageBox.Show("Coefs Write error.");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Channel choose is error.Please choose other channel!");
                        break;
                    }
                }
                MessageBox.Show("Finished!");
            }
        }

        private bool WriteCoefsToDUT(string ItemName, string Coefa, string Coefb, string Coefc)
        {
            bool flag=false;
            switch(ItemName)
            {
                case "BIASDAC":
                    flag = pDut.SetBiasdaccoefa(Coefa)&pDut.SetBiasdaccoefb(Coefb)&pDut.SetBiasdaccoefc(Coefc);
                    break;
                case "MODDAC":
                    flag = pDut.SetModdaccoefa(Coefa) & pDut.SetModdaccoefb(Coefb) & pDut.SetModdaccoefc(Coefc);
                    break;
                case "MASKDAC":
                    flag = pDut.SetMaskcoefa(Convert.ToSingle(Coefa)) & pDut.SetMaskcoefb(Convert.ToSingle(Coefb)) & pDut.SetMaskcoefc(Convert.ToSingle(Coefc));
                    break;
                case "JITTERDAC":
                    //flag = MyEquipment.pDut.SetBiasdaccoefa(Coefa) & MyEquipment.pDut.SetBiasdaccoefa(Coefb) & MyEquipment.pDut.SetBiasdaccoefa(Coefc);
                    break;
                case "CROSSDAC":
                    //flag = MyEquipment.pDut.SetBiasdaccoefa(Coefa) & MyEquipment.pDut.SetBiasdaccoefa(Coefb) & MyEquipment.pDut.SetBiasdaccoefa(Coefc);
                    break;
                case "APDDAC":
                    flag = pDut.SetAPDdaccoefa(Coefa) & pDut.SetAPDdaccoefb(Coefb) & pDut.SetAPDdaccoefc(Coefc);
                    break;
                case "LOSDAC":
                    //flag = MyEquipment.pDut.SetBiasdaccoefa(Coefa) & MyEquipment.pDut.SetBiasdaccoefa(Coefb) & MyEquipment.pDut.SetBiasdaccoefa(Coefc);
                    break;
                case "LOADDAC":
                    //flag = MyEquipment.pDut.SetBiasdaccoefa(Coefa) & MyEquipment.pDut.SetBiasdaccoefa(Coefb) & MyEquipment.pDut.SetBiasdaccoefa(Coefc);
                    break;
                case "TECDAC":
                    flag = pDut.SetTempcoefa(Coefa) & pDut.SetTempcoefb(Coefb) & pDut.SetTempcoefc(Coefc);
                    break;
                case "VCDAC":
                    flag = pDut.SetVcccoefa(Coefa) & pDut.SetVcccoefb(Coefb) & pDut.SetVcccoefc(Coefc);
                    break;
                case "VGDAC":
                    //flag = MyEquipment.pDut.SetBiasdaccoefa(Coefa) & MyEquipment.pDut.SetBiasdaccoefa(Coefb) & MyEquipment.pDut.SetBiasdaccoefa(Coefc);
                    break;
                case "VLDDAC":
                    //flag = MyEquipment.pDut.SetBiasdaccoefa(Coefa) & MyEquipment.pDut.SetBiasdaccoefa(Coefb) & MyEquipment.pDut.SetBiasdaccoefa(Coefc);
                    break;
                case "EADAC":
                    //flag = MyEquipment.pDut.SetBiasdaccoefa(Coefa) & MyEquipment.pDut.SetBiasdaccoefa(Coefb) & MyEquipment.pDut.SetBiasdaccoefa(Coefc);
                    break;
                default:
                    break;
            }
            return flag;
        }

        private void btAPCControl_Click(object sender, EventArgs e)
        {
            if (btAPCControl.BackColor == Color.SpringGreen)
            {
                pDut.APCOFF(0x11);
                btAPCControl.BackColor = Color.Red;
            }
            else
            {
                pDut.APCON(0x11);
                btAPCControl.BackColor = Color.SpringGreen;
            }
        }

        private void btFitting_Click(object sender, EventArgs e)
        {
            string channel = cbFitChannel.Text;
            Fitting(channel);
            pEquipConfig.XSteam.SetPositionUPDown("0");
        }

        private void btAutoScale_Click(object sender, EventArgs e)
        {
            pEquipConfig.FlexScope.AutoScale(0);
        }

        private void btRun_Click(object sender, EventArgs e)
        {
            pEquipConfig.FlexScope.RunStop(true);
        }

        private void Config_Click(object sender, EventArgs e)
        {
            pEquipConfig.ShowDialog();
            MyXml= pEquipConfig.myXml;
        }


        private void cbXstreamUpDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (pEquipConfig.FlagEquipmentConfigOK == true & pEquipConfig.XSteam.EquipmentConnectflag == true)
            {
                pEquipConfig.XSteam.SetPositionUPDown(cbXstreamUpDown.SelectedIndex.ToString(), 0);
            }
            else
            {
                MessageBox.Show("Xsream not exsist Or Config Error.");
            }
        }

        private void SelectItme_Selected(object sender, TabControlEventArgs e)
        {
            if (SelectItme.SelectedIndex == 1 & pEquipConfig.FlagEquipmentConfigOK == true)
            {
                if (pEquipConfig.XSteam.EquipmentConnectflag == true)
                {
                    cbXstreamUpDown.Enabled = true;
                }
                if (pEquipConfig.FlexScope.EquipmentConnectflag == true)
                {
                    gbDCAOffset.Enabled = true;
                    btAutoScale.Enabled = true;
                    btRun.Enabled = true;
                }
                if ((pEquipConfig.E3631Power.EquipmentConnectflag == true) || (pEquipConfig.DP811APower.EquipmentConnectflag == true))
                {
                    cbPowerClose.Enabled = true;
                }
            }
        }

        private void btOffsetConfig_Click(object sender, EventArgs e)
        {
            string[] OffsetArray= {tbOffset1.Text.ToString(),tbOffset2.Text.ToString(),tbOffset3.Text.ToString(),tbOffset4.Text.ToString()};
            for(int i=0;i<4;i++)
            {
                pEquipConfig.FlexScope.configoffset(i.ToString(), OffsetArray[i], 0);
            }

            MyXml.DCAOffset1 = tbOffset1.Text;
            MyXml.DCAOffset2 = tbOffset2.Text;
            MyXml.DCAOffset3 = tbOffset3.Text;
            MyXml.DCAOffset4 = tbOffset4.Text;
        }
        

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    double[] Coefs = new double[3];
        //    double[] TempValueArray = { 7694, 13276, 32864 };

        //    double[] ItemValueArray = { 500, 515, 550 };
        //    Coefs = Myalgorithm.MultiLine(TempValueArray, ItemValueArray, 3, 2);
        //    System.Array.Reverse(Coefs);
        //    if (!WriteCoefsToDUT("BIASDAC", Coefs[0].ToString(), Coefs[1].ToString(), Coefs[2].ToString()))
        //    {
        //        MessageBox.Show("Coefs Write error.");
        //    }
        //}


        //private void chk_BIAS_CheckedChanged(object sender, EventArgs e)
        //{
        //    ArrayList a = new ArrayList();

        //    if (this.chk_BIAS.Checked)
        //    {
        //        a.Add("BiasDac");

        //    }
        //    else
        //    {
        //        if (a.Contains("BiasDac"))
        //        {
        //            a.Remove("BiasDac");
        //        }
               
        //    }
        //}

    }
    public class CombinatControl
    {
       // Form1.Equipment NewEquipment = new Form1.Equipment();

        public delegate bool WriteDriver(object sender);

        public Label ItemLable;

        public HScrollBar ItemHScrollBar;

        public TextBox ItemTextBox;

        public Label MaxValueLable;

        private WriteDriver WriteDAC;

        //Algorithm MyAlgorithm = new Algorithm();

        public int LastValue = -1;

        public CombinatControl()
        {
        }

        public CombinatControl(string ItemName, Point P,Int32 MaxDAC, WriteDriver pWriter)
        {

            WriteDAC = new WriteDriver(pWriter);
            ItemLable = new Label();
            ItemLable.Left = P.X;
            ItemLable.Top = P.Y+10;
            ItemLable.Size = new Size(40, 30);
            ItemLable.Text = ItemName.Substring(0, (ItemName.Length-3));

            ItemHScrollBar = new HScrollBar();
            ItemHScrollBar.Left = ItemLable.Right + 10;
            ItemHScrollBar.Top = P.Y;
            ItemHScrollBar.Size = new Size(200, 30);
            ItemHScrollBar.Maximum = MaxDAC;

            ItemHScrollBar.ValueChanged += new EventHandler(ItemHScrollBarValueChanged);
            ItemHScrollBar.MouseLeave += new EventHandler(ItemHScrollBarMouseLeave);
          
            ItemTextBox = new TextBox();
            ItemTextBox.Left = ItemHScrollBar.Right + 10;
            ItemTextBox.Top = P.Y + 5;
            ItemTextBox.Size = new Size(30, 30);
            ItemTextBox.Text = "00";
            ItemTextBox.Name = ItemName;
            ItemTextBox.KeyDown += new KeyEventHandler(ItemTextBox_KeyDown);
            
            MaxValueLable = new Label();
            MaxValueLable.Left = ItemHScrollBar.Right - 40;
            MaxValueLable.Top = P.Y - 15;
            MaxValueLable.Size = new Size(40, 12);
            MaxValueLable.Text = MaxDAC.ToString();
        }

        private void ItemHScrollBarValueChanged(object sender, EventArgs e)
        {
            ItemTextBox.Text = ItemHScrollBar.Value.ToString();
            ItemTextBox.BackColor = Color.WhiteSmoke;
        }
        
        private void ItemHScrollBarMouseLeave(object sender, EventArgs e)
        {
            if (ItemHScrollBar.Value != LastValue)
            {
                //NewEquipment.pDut.Engmod(1);//dut中每次写dac都已经进入工程模式
                if (!WriteDAC(ItemHScrollBar.Value))
                {
                    ItemTextBox.BackColor = Color.Red;
                }
                else
                {
                    ItemTextBox.BackColor = Color.GreenYellow;
                }
                
                ItemTextBox.Refresh();
               // WriteDAC(ItemHScrollBar.Value);
                LastValue = Convert.ToInt16(ItemHScrollBar.Value);
            }
        }
        private void ItemTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ItemHScrollBar.Value = Convert.ToInt32(ItemTextBox.Text);
                WriteDAC(ItemHScrollBar.Value);
            }
        }
    }
}

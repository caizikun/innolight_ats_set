using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Ivi.Visa.Interop;
using System.Collections;
namespace ATS_Framework
{  
    public class AdjustEye_SFP28 : TestModelBase
    { 
#region Attribute
        private SortedList<string, AdjustEyeTargetValueRecordsStruct> adjustEyeTargetValueRecordsStructArray=new SortedList<string, AdjustEyeTargetValueRecordsStruct>();
        private AdjustEYE_SFP28_Struce AdjustEYE_Struce = new AdjustEYE_SFP28_Struce();       
              
        private SortedList<string, string> tempratureADCArray = new SortedList<string, string>();
        private SortedList<string, string> txTargetLopArray = new SortedList<string, string>();
        private SortedList<string, string> allChannelFixedIMod = new SortedList<string, string>();
        private SortedList<string, string> allChannelFixedIBias= new SortedList<string, string>();

        private ArrayList tempratureADCArrayList = new ArrayList();
        private ArrayList realtempratureArrayList = new ArrayList(); 
        private DataTable txPowerAdcArray = new DataTable();
        private DataTable txPoweruwArray = new DataTable();
        private ArrayList txPowerADC = new ArrayList();
        private ArrayList erortxPowerValueArray = new ArrayList();
        private ArrayList inPutParametersNameArray = new ArrayList();

        private Powersupply pPs;
        private Scope pScope;
       // private DUT pDut;


        private bool isTxPowerAdjustOk=false;
        private bool isErAdjustOk = false;
       
        //......
        private UInt32 ibiasDacTarget = 0;
        private UInt32 imodDacTarget = 0;
        private UInt32 txpowerAdcTarget = 0;
        private double targetLOP = -1;
        private double targetER = -1;
        
        //.....
      
        // cal txpower
            
        private ArrayList openLoopTxPowerCoefArray = new ArrayList();
        private ArrayList closeLoopTxPowerCoefArray = new ArrayList();
        private ArrayList pidTxPowerTempCoefCoefArray = new ArrayList();
        private bool isCalTxPowerOk;

        private ArrayList modulationCoefArray;
       

        DataTable dtAdjustData = new DataTable();
        DataRow Dr_CurrentCondition;
#endregion
        
#region Method
        public AdjustEye_SFP28(DUT inPuDut, logManager logmanager)
        {
            logger = logmanager;
            logoStr = null;
            dut = inPuDut; 
            txPowerAdcArray.Clear();           
            txPoweruwArray.Clear();
            tempratureADCArray.Clear();
            txTargetLopArray.Clear();
            adjustEyeTargetValueRecordsStructArray.Clear();
            openLoopTxPowerCoefArray.Clear();
            closeLoopTxPowerCoefArray.Clear();
            pidTxPowerTempCoefCoefArray.Clear();
            tempratureADCArrayList.Clear();
            realtempratureArrayList.Clear();
            allChannelFixedIMod.Clear();
            allChannelFixedIBias.Clear();
            //int ErCo_1_2;
            AdjustEYE_SFP28_Struce pp = new AdjustEYE_SFP28_Struce();
          // pp.AutoTune.GetType
            inPutParametersNameArray.Clear();
            inPutParametersNameArray.Add("Autotune".ToUpper().Trim());
            inPutParametersNameArray.Add("TxPowTarget(uw)".ToUpper().Trim());
            inPutParametersNameArray.Add("TxPowtolerance(uw)".ToUpper().Trim());

            inPutParametersNameArray.Add("TxErtarget(db)".ToUpper().Trim());
            inPutParametersNameArray.Add("TxErtolerance(db)".ToUpper().Trim());
            inPutParametersNameArray.Add("ibiasmax(mA)".ToUpper().Trim());
            inPutParametersNameArray.Add("Ibiasmin(mA)".ToUpper().Trim());
            inPutParametersNameArray.Add("Ibiasstart(mA)".ToUpper().Trim());
            inPutParametersNameArray.Add("Ibiasstep(mA)".ToUpper().Trim());
            inPutParametersNameArray.Add("IbiasMethod".ToUpper().Trim());

            inPutParametersNameArray.Add("Imodmax(mA)".ToUpper().Trim());
            inPutParametersNameArray.Add("Imodmin(mA)".ToUpper().Trim());

            inPutParametersNameArray.Add("Imodmethod".ToUpper().Trim());
            inPutParametersNameArray.Add("Imodstep".ToUpper().Trim());

            inPutParametersNameArray.Add("Imodstart(mA)".ToUpper().Trim());
            inPutParametersNameArray.Add("Loop_open_close_both".ToUpper().Trim());

            inPutParametersNameArray.Add("ErCoef_1st_2st_Pid".ToUpper().Trim());
            inPutParametersNameArray.Add("TxPowCoef_1st_2st_Pid".ToUpper().Trim());

            inPutParametersNameArray.Add("Is_AC_DC".ToUpper().Trim());
            inPutParametersNameArray.Add("Fixedcrossdac".ToUpper().Trim());

            inPutParametersNameArray.Add("IbiasPidcoefarray".ToUpper().Trim());
            inPutParametersNameArray.Add("Fixedmodarray".ToUpper().Trim());

            inPutParametersNameArray.Add("Fixedibiasarray".ToUpper().Trim());
            inPutParametersNameArray.Add("Sleeptime".ToUpper().Trim());



            //inPutParametersNameArray.Add("AUTOTUNE");
            //inPutParametersNameArray.Add("TXLOPTARGET(UW)");
            //inPutParametersNameArray.Add("TXLOPTOLERANCE(UW)");
            //inPutParametersNameArray.Add("TXERTARGET(DB)");
            //inPutParametersNameArray.Add("TXERTOLERANCE(DB)");

            //inPutParametersNameArray.Add("IBIASMAX(MA)");
            //inPutParametersNameArray.Add("IBIASMIN(MA)");
            //inPutParametersNameArray.Add("IBIASSTART(MA)");
            //inPutParametersNameArray.Add("IBIASSTEP(MA)");
            //inPutParametersNameArray.Add("IBIASMETHOD");
            //inPutParametersNameArray.Add("IMODMAX(MA)");
            //inPutParametersNameArray.Add("IMODMIN(MA)");
            //inPutParametersNameArray.Add("IMODMETHOD");
            //inPutParametersNameArray.Add("IMODSTEP");
            //inPutParametersNameArray.Add("IMODSTART(MA)");

            //inPutParametersNameArray.Add("ISOPENLOOPORCLOSELOOPORBOTH");
            //inPutParametersNameArray.Add("1STOR2STORPIDER");
            //inPutParametersNameArray.Add("1STOR2STORPIDTXLOP");
            //inPutParametersNameArray.Add("DCTODC");
            //inPutParametersNameArray.Add("FIXEDCROSSDAC");
            //inPutParametersNameArray.Add("PIDCOEFARRAY");
            //inPutParametersNameArray.Add("FIXEDMODARRAY");
            //inPutParametersNameArray.Add("FIXEDIBIASARRAY");
            //inPutParametersNameArray.Add("SLEEPTIME");
            //...

            dtAdjustData.Columns.Add("Temp");
            dtAdjustData.Columns.Add("Vcc");
            dtAdjustData.Columns.Add("Channel");
            dtAdjustData.Columns.Add("TargetIbiasDac");
            dtAdjustData.Columns.Add("TargetModDac");
            dtAdjustData.Columns.Add("TargetTxPowerAdc");
            dtAdjustData.Columns.Add("TempAdc");
          //  Dr_CurrentCondition
        }
        override protected bool CheckEquipmentReadiness()
        {
            //check if all equipments are ready for this test; 
            //increase equipment referenced_times if ready
            //for (int i = 0; i < pEquipList.Count; i++)
            //    if (!pEquipList.Values[i].bReady) return false;

            for (int i = 0; i < selectedEquipList.Count; i++)
            {
                if (!selectedEquipList.Values[i].bReady) return false;

            }

            return true;
        }
        override protected bool PrepareTest()
        {//note: for inherited class, they need to do its own preparation process task,
            //then call this base function
            //for (int i = 0; i < pEquipList.Count; i++)
            ////pEquipList.Values[i].IncreaseReferencedTimes();
            //{
            for (int i = 0; i < selectedEquipList.Count; i++)
            {
                selectedEquipList.Values[i].IncreaseReferencedTimes();

            }


            return AssembleEquipment();
        }
        protected override bool ConfigureEquipment(EquipmentList selectedEquipmentList)
        {

            for (int i = 0; i < selectedEquipList.Count; i++)
            {
                if (!selectedEquipList.Values[i].Configure()) return false;

            }

            return true;
        }
        protected override bool AssembleEquipment()
        {
            for (int i = 0; i < selectedEquipList.Count; i++)
            {
                if (!selectedEquipList.Values[i].Switch(true)) return false;
            }
            dut.FullFunctionEnable();
            return true;
        }
        public override bool SelectEquipment(EquipmentList aEquipList)
        {
            selectedEquipList.Clear();
            if (aEquipList.Count == 0)
            {
                selectedEquipList.Clear();
                selectedEquipList.Add("DUT", dut);
                return false;
            }
            else
            {
                bool isOK = false;
                selectedEquipList.Clear();
                IList<string> tempKeys = aEquipList.Keys;
                IList<EquipmentBase> tempValues = aEquipList.Values;
                for (byte i = 0; i < aEquipList.Count; i++)
                {

                    if (tempKeys[i].ToUpper().Contains("SCOPE"))
                    {
                        selectedEquipList.Add("SCOPE", tempValues[i]);
                        isOK = true;
                    }
                    if (tempKeys[i].ToUpper().Contains("POWERSUPPLY"))
                    {
                        selectedEquipList.Add("POWERSUPPLY", tempValues[i]);
                    }
                   
                }
                if (selectedEquipList["SCOPE"] != null && selectedEquipList["POWERSUPPLY"] != null)
                {
                    isOK = true;

                }
                else
                {
                    isOK = false;
                }
                if (isOK)
                {
                    selectedEquipList.Add("DUT", dut);
                    return isOK;
                }
                return isOK;
            }
            
        }

        private bool fillDataTable()
        {
            UInt16 CurrentTempAdc = 0;//同一温度下的TempAdc值,一般为Channel-1 获取到的
            DataRow[] drArray = dtAdjustData.Select("Temp=" + GlobalParameters.CurrentTemp + " and Vcc=" + GlobalParameters.CurrentVcc);
            DataRow dr = dtAdjustData.NewRow();
            if (drArray.Length > 0)
            {
                CurrentTempAdc = Convert.ToUInt16(drArray[0]["TempAdc"]);
                logoStr += logger.AdapterLogString(1, "tempratureADC=" + CurrentTempAdc.ToString());
            }
            else
            {
                UInt16 tempratureADC;
                dut.ReadTempADC(out tempratureADC, 1);
                logoStr += logger.AdapterLogString(1, "tempratureADC=" + tempratureADC.ToString());
                CurrentTempAdc = tempratureADC;
            }

            dr["Temp"] = GlobalParameters.CurrentTemp;
            dr["Vcc"] = GlobalParameters.CurrentVcc;
            dr["Channel"] = GlobalParameters.CurrentChannel;
            dr["TargetIbiasDac"] = 0;
            dr["TargetModDac"] = 0;
            dr["TargetTxPowerAdc"] = 0;
            dr["TempAdc"] = CurrentTempAdc;
            dtAdjustData.Rows.Add(dr);
            Dr_CurrentCondition = dr;

            return true;
        }

        private bool OpenAPC(bool IbiasSwith, bool IModSwith)
        {
            byte ApcFlag=0;

            dut.APCStatus(out ApcFlag);

             if (GlobalParameters.ApcStyle == 0)//APC 共开关 也就是单开关
             {
               
                if (!IbiasSwith||!IModSwith)// 关闭APC
                 {
                      dut.APCOFF(0X11);
                       pPs.Switch(false);
                        
                       pPs.Switch(true);
                       dut.FullFunctionEnable();
                 }
                else
                {
                    dut.APCON(0X11);
                }

             }
             else//APC 分两个开关 也就是双开关
             {
               
                 if (!IbiasSwith || !IModSwith)
                 {
                     if (ApcFlag != 0x11)
                     {
                         dut.APCOFF(0X11);
                         pPs.Switch(false);
                         pPs.Switch(true);
                         dut.FullFunctionEnable();
                     }
                 }

             }
           // dut.APCOFF()
            dut.APCCloseOpen(false);
            return true;
        }

        protected override bool StartTest()
        {
            

            logger.FlushLogBuffer();
            logoStr = "";


            if (Test())
            {
                return true;

            }
            else
            {
                return false;
            }

        }

        private bool Test()
        {
           
            fillDataTable();//添家一条新纪录 Condition

            try
            {
               
                OpenAPC(false, false);


                if (AdjustEYE_Struce.AC_DC_Coupling == 0)
                {
                    AC_Control();
                }
                else
                {
                    DC_Control();
                }

                if (!Fit_Write_Coef())
                {
                    logger.AdapterLogString(3, "系数拟合以及写入失败~~~");
                }
                AnalysisOutputParameters(outputParameters);
                return true;
            }
            catch(Exception ex)
            {
              
                logger.AdapterLogString(3, ex.Message);
                logger.FlushLogBuffer();
               AnalysisOutputParameters(outputParameters);

            }
            return true;
           
        }

        private bool Fit_Write_Coef()
        {
            bool Flag_fitCoef = true;
           

            DataRow[] drArray = dtAdjustData.Select("Channel=" + GlobalParameters.CurrentChannel, "Temp");

            if (!Fit_TxPowerCoef(drArray)) Flag_fitCoef = false;
            if (!Fit_Write_ERCoef(drArray)) Flag_fitCoef = false;
             
            return Flag_fitCoef;
        }
        private bool Fit_TxPowerCoef(DataRow[] drArray)
        {
           
           int dimension = -1;

            double[] TempAdcArray = new double[drArray.Length];
            double[] TargetIbiasDacArray = new double[drArray.Length];
            double[] TargetTxPowerAdcArray = new double[drArray.Length];
            double[] TargetModDacArray = new double[drArray.Length];
            bool flag_TxPowerCoef = true;
            double[] IbiasCoefArray_OpenLoop = null;
            double[] IbiasCoefArray_CloseLoop = null;



            switch (AdjustEYE_Struce.TxPowCoef_1st_2st_Pid)
            {
                case 1:
                    dimension = 1;
                    break;
                case 2:
                    dimension = 2;
                    break;
                default:// PID=0
                    dimension = 0;
                    break;

            }


            if (dimension != 0)// 对于常规的 1-2次系数进行拟合,目前还没有PID的算法先空留
            {
              if (drArray.Length<2)
              {
                  logger.AdapterLogString(0, "Condition Count<2");
                  return flag_TxPowerCoef;
              }

            }
            else// 目前没有PID 算法SFP28
            {
                return false;
            }
           
            for (int i = 0; i < drArray.Length; i++)
            {
                TempAdcArray[i] = Convert.ToDouble(drArray[i]["TempAdc"]);
                TargetIbiasDacArray[i] = Convert.ToDouble(drArray[i]["TargetIbiasDac"]);
                TargetTxPowerAdcArray[i] = Convert.ToDouble(drArray[i]["TargetTxPowerAdc"]);
                TargetModDacArray[i] = Convert.ToDouble(drArray[i]["TargetModDac"]);
            }


          
            switch (AdjustEYE_Struce.Loop_open_close_both)
            {
                case 1://OpenLoop
                    IbiasCoefArray_OpenLoop = algorithm.MultiLine(TempAdcArray, TargetIbiasDacArray,drArray. Length, dimension);
                    break;
                case 2://CloseLoop
                    IbiasCoefArray_CloseLoop = algorithm.MultiLine(TempAdcArray, TargetTxPowerAdcArray, drArray.Length, dimension);
                    break;
                default://Open+Close
                    IbiasCoefArray_OpenLoop = algorithm.MultiLine(TempAdcArray, TargetIbiasDacArray,drArray. Length, dimension);
                    IbiasCoefArray_CloseLoop = algorithm.MultiLine(TempAdcArray, TargetTxPowerAdcArray, drArray.Length, dimension);
                    break;
            }
            if (!WriteIbiasCoef(IbiasCoefArray_OpenLoop, IbiasCoefArray_CloseLoop,dimension)) flag_TxPowerCoef = false;

            return flag_TxPowerCoef;
        
        }

        private bool WriteIbiasCoef(double[] OpencoefArray, double[] CloseCoefArray, int dimension)
        {


            float openLoopTxPowerCoefA=0;
            float openLoopTxPowerCoefB=0;
            float openLoopTxPowerCoefC=0;
            float closeLoopTxPowerCoefA=0;
            float closeLoopTxPowerCoefB=0;
            float closeLoopTxPowerCoefC=0; 

            bool isWriteCloseLoopCoefBOk = true;
            bool isWriteCloseLoopCoefAOk = true;
            bool isWriteCloseLoopCoefCOk = true;

            bool isWriteOpenLoopCoefBOk = true;
            bool isWriteOpenLoopCoefAOk = true;
            bool isWriteOpenLoopCoefCOk = true;



            bool flagCoef = true;


           


            if (OpencoefArray!=null&&OpencoefArray.Length >= 2)
            {
                openLoopTxPowerCoefArray = ArrayList.Adapter(OpencoefArray);

                if (openLoopTxPowerCoefArray.Count==2)
                {
                    openLoopTxPowerCoefB =Convert.ToSingle( openLoopTxPowerCoefArray[1]);
                    openLoopTxPowerCoefC = Convert.ToSingle(openLoopTxPowerCoefArray[0]);
                }
                else
                {
                    openLoopTxPowerCoefA = Convert.ToSingle(openLoopTxPowerCoefArray[2]);
                    openLoopTxPowerCoefB = Convert.ToSingle(openLoopTxPowerCoefArray[1]);
                    openLoopTxPowerCoefC = Convert.ToSingle(openLoopTxPowerCoefArray[0]);
                }

                openLoopTxPowerCoefArray.Reverse();

                for (byte i = 0; i < openLoopTxPowerCoefArray.Count; i++)
                {
                    logoStr += logger.AdapterLogString(1, "openLoopTxPowerCoefArray[" + i.ToString() + "]=" + openLoopTxPowerCoefArray[i].ToString() + " " + algorithm.ByteArraytoString(2, ",", algorithm.FloatToIEE754(openLoopTxPowerCoefArray[i])));
                }
                logoStr += logger.AdapterLogString(0, "Step9...WriteCoef");

                #region W&R Biasdaccoefc
                isWriteOpenLoopCoefCOk = dut.SetBiasdaccoefc(openLoopTxPowerCoefC.ToString());


                if (isWriteOpenLoopCoefCOk)
                {
                    isWriteOpenLoopCoefCOk = true;
                    logoStr += logger.AdapterLogString(1, "isWriteOpenLoopCoefCOk:" + isWriteOpenLoopCoefCOk.ToString());
                }
                else
                {
                    isWriteOpenLoopCoefCOk = false;
                    logoStr += logger.AdapterLogString(3, "isWriteOpenLoopCoefCOk:" + isWriteOpenLoopCoefCOk.ToString());
                }
                #endregion
                #region W&R Biasdaccoefb
                isWriteOpenLoopCoefBOk = dut.SetBiasdaccoefb(openLoopTxPowerCoefB.ToString());

                if (isWriteOpenLoopCoefBOk)
                {
                    isWriteOpenLoopCoefBOk = true;
                    logoStr += logger.AdapterLogString(1, "isWriteOpenLoopCoefBOk:" + isWriteOpenLoopCoefBOk.ToString());
                }
                else
                {
                    isWriteOpenLoopCoefBOk = false;
                    logoStr += logger.AdapterLogString(3, "isWriteOpenLoopCoefBOk:" + isWriteOpenLoopCoefBOk.ToString());
                }
                #endregion
                #region W&R Biasdaccoefa

                if (OpencoefArray.Length > 2)
                {


                    isWriteOpenLoopCoefAOk = dut.SetBiasdaccoefa(openLoopTxPowerCoefA.ToString());


                    if (isWriteOpenLoopCoefAOk)
                    {
                        isWriteOpenLoopCoefAOk = true;
                        logoStr += logger.AdapterLogString(1, "isWriteOpenLoopCoefAOk:" + isWriteOpenLoopCoefAOk.ToString());
                    }
                    else
                    {
                        isWriteOpenLoopCoefAOk = false;
                        logoStr += logger.AdapterLogString(3, "isWriteOpenLoopCoefAOk:" + isWriteOpenLoopCoefAOk.ToString());
                    }
                }
                if (isWriteOpenLoopCoefAOk & isWriteOpenLoopCoefBOk & isWriteOpenLoopCoefCOk)
                {
                    isCalTxPowerOk = true;
                    logoStr += logger.AdapterLogString(1, "isCalTxPowerOk:" + isCalTxPowerOk.ToString());

                }
                else
                {
                    isCalTxPowerOk = false;
                    logoStr += logger.AdapterLogString(3, "isCalTxPowerOk:" + isCalTxPowerOk.ToString());

                }

                #endregion


                if (!isWriteOpenLoopCoefAOk || isWriteOpenLoopCoefBOk || isWriteOpenLoopCoefCOk)
                {
                    flagCoef = false;
                }
            }
            else
            {
                flagCoef = false;
            }
            if (CloseCoefArray!=null&&CloseCoefArray.Length > 1)
            {
                closeLoopTxPowerCoefC = (float)CloseCoefArray[0];
                closeLoopTxPowerCoefB = (float)CloseCoefArray[1];
                closeLoopTxPowerCoefA = (float)CloseCoefArray[2];
                closeLoopTxPowerCoefArray = ArrayList.Adapter(CloseCoefArray);
                closeLoopTxPowerCoefArray.Reverse();
                for (byte i = 0; i < closeLoopTxPowerCoefArray.Count; i++)
                {
                    logoStr += logger.AdapterLogString(1, "closeLoopTxPowerCoefArray[" + i.ToString() + "]=" + closeLoopTxPowerCoefArray[i].ToString() + " " + algorithm.ByteArraytoString(2, ",", algorithm.FloatToIEE754(closeLoopTxPowerCoefArray[i])));

                }
                logoStr += logger.AdapterLogString(0, "Step9...WriteCoef");

                #region W&R TxPowerAdccoefc
                isWriteCloseLoopCoefCOk = dut.SetCloseLoopcoefc(closeLoopTxPowerCoefC.ToString());
                if (isWriteCloseLoopCoefCOk)
                {
                    isWriteCloseLoopCoefCOk = true;
                    logoStr += logger.AdapterLogString(1, "isWriteCloseLoopCoefCOk:" + isWriteCloseLoopCoefCOk.ToString());
                }
                else
                {
                    isWriteCloseLoopCoefCOk = false;
                    logoStr += logger.AdapterLogString(3, "isWriteCloseLoopCoefCOk:" + isWriteCloseLoopCoefCOk.ToString());
                }
                #endregion
                #region W&R TxPowerAdccoefb
                isWriteCloseLoopCoefBOk = dut.SetCloseLoopcoefb(closeLoopTxPowerCoefB.ToString());

                if (isWriteCloseLoopCoefBOk)
                {
                    isWriteCloseLoopCoefBOk = true;
                    logoStr += logger.AdapterLogString(1, "isWriteCloseLoopCoefBOk:" + isWriteCloseLoopCoefBOk.ToString());

                }
                else
                {
                    isWriteCloseLoopCoefBOk = false;
                    logoStr += logger.AdapterLogString(3, "isWriteCloseLoopCoefBOk:" + isWriteCloseLoopCoefBOk.ToString());

                }
                #endregion
                #region W&R TxPowerAdcccoefa
                isWriteCloseLoopCoefAOk = dut.SetCloseLoopcoefa(closeLoopTxPowerCoefA.ToString());

                if (isWriteCloseLoopCoefAOk)
                {
                    isWriteCloseLoopCoefAOk = true;
                    logoStr += logger.AdapterLogString(1, "isWriteCloseLoopCoefAOk:" + isWriteCloseLoopCoefAOk.ToString());

                }
                else
                {
                    isWriteCloseLoopCoefAOk = false;
                    logoStr += logger.AdapterLogString(3, "isWriteCloseLoopCoefAOk:" + isWriteCloseLoopCoefAOk.ToString());
                }
                #endregion

                if (!isWriteCloseLoopCoefAOk || isWriteCloseLoopCoefBOk || isWriteCloseLoopCoefCOk)
                {
                    flagCoef = false;
                }
            }
            else
            {
                flagCoef = false;
            }
            return flagCoef;
        }

        private bool Fit_Write_ERCoef(DataRow[] drArray)
        {
            bool flag_ER_Coef = true;
            bool isWriteERCoefBOk = true;
            bool isWriteERCoefAOk = true;
            bool isWriteERCoefCOk = true;
            int dimension = -1;

            double[] ModDacArray = new double[drArray.Length];
            double[] TempAdcArray = new double[drArray.Length];
            double[] ErCoefArray;
          

            float ERCoefA=0;
            float ERCoefB=0;
            float ERCoefC = 0;
        
        



            switch (AdjustEYE_Struce.ErCoef_1st_2st_Pid)
            {
                case 1:
                    dimension = 1;
                   
                    break;
                case 2:
                    dimension = 2;
                    break;
                default:// PID=0
                    dimension = 0;
                    break;

            }


            if (dimension != 0)// 对于常规的 1-2次系数进行拟合,目前还没有PID的算法先空留
            {
                if (drArray.Length < 2)
                {
                    logger.AdapterLogString(0, "Condition Count<2");
                    return flag_ER_Coef;
                }

            }
            else// 目前没有PID 算法SFP28
            {
                return false;
            }

           
                
                #region fit Coef

            for (int i = 0; i < drArray.Length;i++ )
            {
                ModDacArray[i] = Convert.ToDouble(drArray[i]["TargetModDac"]);
                TempAdcArray[i] = Convert.ToDouble(drArray[i]["TempAdc"]);
            }

           // ErCoefArray=
            ErCoefArray = algorithm.MultiLine(TempAdcArray, ModDacArray, drArray.Length, dimension);
                   
              
                    logoStr += logger.AdapterLogString(0, "Step10...CurveCoef ER");

                    if (AdjustEYE_Struce.ErCoef_1st_2st_Pid == 1)
                    {
                        ERCoefC = (float)ErCoefArray[0];
                        ERCoefB = (float)ErCoefArray[1];
                        ERCoefA = 0;
                        modulationCoefArray = ArrayList.Adapter(ErCoefArray);
                        modulationCoefArray.Reverse();
                        for (byte i = 0; i < modulationCoefArray.Count; i++)
                        {
                            logoStr += logger.AdapterLogString(1, "modulationCoefArray[" + i.ToString() + "]=" + modulationCoefArray[i].ToString() + " " + algorithm.ByteArraytoString(2, ",", algorithm.FloatToIEE754(modulationCoefArray[i])));

                        }
                        logoStr += logger.AdapterLogString(0, "Step11...WriteCoef");
                        #region W&R Moddaccoefc
                        isWriteERCoefCOk = dut.SetModdaccoefc(ERCoefC.ToString());

                        if (isWriteERCoefCOk)
                        {
                            isWriteERCoefCOk = true;
                            logoStr += logger.AdapterLogString(1, "WriteCoefERCoefC:" + isWriteERCoefCOk.ToString());
                        }
                        else
                        {
                            isWriteERCoefCOk = false;
                            logoStr += logger.AdapterLogString(3, "WriteCoefERCoefC:" + isWriteERCoefCOk.ToString());
                        }
                        #endregion
                        #region W&R Moddaccoefb
                        isWriteERCoefBOk = dut.SetModdaccoefb(ERCoefB.ToString());
                        if (isWriteERCoefBOk)
                        {
                            isWriteERCoefBOk = true;
                            logoStr += logger.AdapterLogString(1, "WriteCoefERCoefB:" + isWriteERCoefBOk.ToString());

                        }
                        else
                        {
                            isWriteERCoefBOk = false;
                            logoStr += logger.AdapterLogString(3, "WriteCoefERCoefB:" + isWriteERCoefBOk.ToString());

                        }
                        #endregion
                        #region W&R Moddaccoefa
                        isWriteERCoefAOk = dut.SetModdaccoefa(ERCoefA.ToString());

                        if (isWriteERCoefAOk)
                        {
                            isWriteERCoefAOk = true;
                            logoStr += logger.AdapterLogString(1, "WriteCoefERCoefA:" + isWriteERCoefAOk.ToString());

                        }
                        else
                        {
                            isWriteERCoefAOk = false;
                            logoStr += logger.AdapterLogString(3, "WriteCoefERCoefA:" + isWriteERCoefAOk.ToString());
                        }
                        #endregion
                        if (isWriteERCoefBOk & isWriteERCoefCOk & isWriteERCoefAOk)
                        {
                           // isCalErOk = true;
                            logoStr += logger.AdapterLogString(1, "isCalErOk:" + flag_ER_Coef.ToString());

                        }
                        else
                        {
                            flag_ER_Coef = false;
                            logoStr += logger.AdapterLogString(3, "isCalErOk:" + flag_ER_Coef.ToString());
                        }

                    }
                    else if (AdjustEYE_Struce.ErCoef_1st_2st_Pid == 2)
                    {
                        double[] tempRealTempAdcArray = new double[tempratureADCArray.Count];
                        double[] tempModulationDacArray = new double[adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].imodulaDacArray.Count];

                        for (byte i = 0; i < realtempratureArrayList.Count; i++)
                        {
                            tempRealTempAdcArray[i] = Convert.ToDouble(realtempratureArrayList[i].ToString()) * 256;
                        }
                        for (byte i = 0; i < adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].imodulaDacArray.Count; i++)
                        {
                            tempModulationDacArray[i] = Convert.ToDouble(adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].imodulaDacArray[i].ToString());
                        }
                        double[] coefArray = algorithm.MultiLine(tempRealTempAdcArray, tempModulationDacArray, Math.Min(tempratureADCArray.Count, adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].imodulaDacArray.Count), 2);
                        ERCoefC = (float)coefArray[0];
                        ERCoefB = (float)coefArray[1];
                        ERCoefA = (float)coefArray[2];
                        modulationCoefArray = ArrayList.Adapter(coefArray);
                        modulationCoefArray.Reverse();
                        for (byte i = 0; i < modulationCoefArray.Count; i++)
                        {
                            logoStr += logger.AdapterLogString(1, "modulationCoefArray[" + i.ToString() + "]=" + modulationCoefArray[i].ToString() + " " + algorithm.ByteArraytoString(2, ",", algorithm.FloatToIEE754(modulationCoefArray[i])));
                        }
                        logoStr += logger.AdapterLogString(0, "Step12...WriteCoef");

                        #region W&R Moddaccoefc
                        isWriteERCoefCOk = dut.SetModdaccoefc(ERCoefC.ToString());

                        if (isWriteERCoefCOk)
                        {
                            isWriteERCoefCOk = true;
                            logoStr += logger.AdapterLogString(1, "WriteCoefERCoefC:" + isWriteERCoefCOk.ToString());
                        }
                        else
                        {
                            isWriteERCoefCOk = false;
                            logoStr += logger.AdapterLogString(3, "WriteCoefERCoefC:" + isWriteERCoefCOk.ToString());
                        }
                        #endregion
                        #region W&R Moddaccoefb
                        isWriteERCoefBOk = dut.SetModdaccoefb(ERCoefB.ToString());
                        if (isWriteERCoefBOk)
                        {
                            isWriteERCoefBOk = true;
                            logoStr += logger.AdapterLogString(1, "WriteCoefERCoefB:" + isWriteERCoefBOk.ToString());

                        }
                        else
                        {
                            isWriteERCoefBOk = false;
                            logoStr += logger.AdapterLogString(3, "WriteCoefERCoefB:" + isWriteERCoefBOk.ToString());
                        }
                        #endregion
                        #region W&R Moddaccoefa
                        isWriteERCoefAOk = dut.SetModdaccoefa(ERCoefA.ToString());

                        if (isWriteERCoefAOk)
                        {
                            isWriteERCoefAOk = true;
                            logoStr += logger.AdapterLogString(1, "WriteCoefERCoefA:" + isWriteERCoefAOk.ToString());

                        }
                        else
                        {
                            isWriteERCoefAOk = false;
                            logoStr += logger.AdapterLogString(3, "WriteCoefERCoefA:" + isWriteERCoefAOk.ToString());
                        }
                        #endregion

                        if (isWriteERCoefAOk & isWriteERCoefBOk & isWriteERCoefCOk)
                        {
                            flag_ER_Coef = true;
                            logoStr += logger.AdapterLogString(1, "isCalErOk:" + flag_ER_Coef.ToString());
                        }
                        else
                        {
                            flag_ER_Coef = false;
                            logoStr += logger.AdapterLogString(3, "isCalErOk:" + flag_ER_Coef.ToString());
                        }

                    }
                
                #endregion


         

            return true;
        }
      

        private bool DC_Control()
        {
            bool flagAdjust = true;


            logoStr += logger.AdapterLogString(0, "Step3...Fix ImodValue");
            logoStr += logger.AdapterLogString(0, "Step4...Start Adjust TxPower");
            logoStr += logger.AdapterLogString(0, "Step5...SetScaleOffset");
            #region  CheckTempChange

            if (!tempratureADCArray.ContainsKey(GlobalParameters.CurrentTemp.ToString().Trim().ToUpper()))
            {
                logoStr += logger.AdapterLogString(0, "Step4...TempChanged Read tempratureADC");
                logoStr += logger.AdapterLogString(1, "realtemprature=" + GlobalParameters.CurrentTemp.ToString());

                UInt16 tempratureADC;
                dut.ReadTempADC(out tempratureADC, 1);
                logoStr += logger.AdapterLogString(1, "tempratureADC=" + tempratureADC.ToString());
                tempratureADCArray.Add(GlobalParameters.CurrentTemp.ToString().Trim().ToUpper(), tempratureADC.ToString().Trim());
                tempratureADCArrayList.Add(tempratureADC);
                realtempratureArrayList.Add(GlobalParameters.CurrentTemp);
            }

            #endregion

           // dtAdjustData.Rows.Count = 0;


            DataRow[] drArray = dtAdjustData.Select("Vcc=" + GlobalParameters.CurrentVcc + " and Channel=" + GlobalParameters.CurrentChannel);

            if (drArray.Length < 1)
            {
                OpenAPC(false, false);
                pPs.Switch(false, 1);
                pPs.Switch(true, 1);
                dut.WriteModDac(Convert.ToUInt32(allChannelFixedIMod[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()]));
                dut.WriteBiasDac(Convert.ToUInt32(allChannelFixedIBias[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()]));
                double tempTargetTxPowerDBM = 0;
                double tempTargetTxPowerUW = 0;
                tempTargetTxPowerDBM = dut.ReadDmiTxp();
                tempTargetTxPowerUW = algorithm.ChangeDbmtoUw(tempTargetTxPowerDBM);
                pPs.Switch(false, 1);
                pPs.Switch(true, 1);
            }
            ArrayList tempProcessDate = new ArrayList();

            if (AdjustEYE_Struce.IbiasMethod == 1)//当IbiasMethod==1的时候 在Dc 控制中,AP 和 ER 一起调试
           {
                flagAdjust=  OnesectionMethodERandPower(AdjustEYE_Struce.IbiasStart, AdjustEYE_Struce.ImodStart, AdjustEYE_Struce.IbiasStep, AdjustEYE_Struce.ImodStep, AdjustEYE_Struce.TxLOPTarget, AdjustEYE_Struce.TxLOPTolerance, AdjustEYE_Struce.IbiasMax, AdjustEYE_Struce.IbiasMin, AdjustEYE_Struce.TxErTarget, AdjustEYE_Struce.TxErTolerance, pScope, dut, AdjustEYE_Struce.ImodMax, AdjustEYE_Struce.ImodMin, out ibiasDacTarget, out imodDacTarget, out targetER, out txpowerAdcTarget, out targetLOP, out isTxPowerAdjustOk, out isErAdjustOk);

           }

            Dr_CurrentCondition["TargerIbiasDac"] = ibiasDacTarget;
            Dr_CurrentCondition["TargerModDac"] = imodDacTarget;
            Dr_CurrentCondition["TargerTxPowerAdc"] = txpowerAdcTarget;

           
           
           logoStr += logger.AdapterLogString(0, "isTxPowerAdjustOk=" + isTxPowerAdjustOk.ToString());
           logoStr += logger.AdapterLogString(3, "isErAdjustOk=" + isErAdjustOk.ToString());    
           
            if (isErAdjustOk&&isTxPowerAdjustOk)
            {
                flagAdjust = true;
            }
            else
            {
                 flagAdjust = false;
            }
            
           
            return flagAdjust;
        }
        private bool AC_Control()
        {
            bool flagAdjust = true;

            OpenAPC(false, false);

            logoStr += logger.AdapterLogString(0, "Step3...Fix ImodValue");
            logoStr += logger.AdapterLogString(1, "FixedMod=" + allChannelFixedIMod[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()]);
            dut.WriteModDac(Convert.ToUInt32(allChannelFixedIMod[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()])); // add parameters               

            logoStr += logger.AdapterLogString(1, "SetScaleOffset");
            pScope.SetScaleOffset(AdjustEYE_Struce.TxLOPTarget, 1);

            isTxPowerAdjustOk = SingleAdjustTxPower();

            if (isTxPowerAdjustOk == false)
            {
                logoStr += logger.AdapterLogString(3, "isTxPowerAdjustOk=" + isTxPowerAdjustOk.ToString());
            }

            isErAdjustOk = SingleAdjustER();

            if (isErAdjustOk == false)
            {
                logoStr += logger.AdapterLogString(3, "isErAdjustOk=" + isErAdjustOk.ToString());
            }

            if (isTxPowerAdjustOk == false || isErAdjustOk == false)
            {
               
                flagAdjust = false;
            }
            else if (isTxPowerAdjustOk && isErAdjustOk)
            {     
                flagAdjust = true;
            }

            Dr_CurrentCondition["TargetIbiasDac"] = ibiasDacTarget;
            Dr_CurrentCondition["TargetModDac"] = imodDacTarget;
            Dr_CurrentCondition["TargetTxPowerAdc"] = txpowerAdcTarget;
            logoStr += logger.AdapterLogString(1, "TargetIbiasDac=" + ibiasDacTarget.ToString() + " TargetModDac= " + imodDacTarget + " txpowerAdcTarget=" + txpowerAdcTarget);
            logger.FlushLogBuffer();
            return flagAdjust;
        }
       
        private bool ReadER( out double ValueER)
        {
             ValueER = 0;
             for (byte i = 0; i < 4; i++)
             {
                 pScope.SetScaleOffset(AdjustEYE_Struce.TxLOPTarget, 1);
                 ValueER = pScope.GetEratio();
                 if (ValueER >= 10000000)
                 {
                     pScope.ClearDisplay();
                     Thread.Sleep(AdjustEYE_Struce.SleepTime * 1000);
                     ValueER = pScope.GetEratio();
                 }
                 else
                 {
                     break;
                 }
             }


            return false;
        }

        private bool SingleAdjustER()
        {
            UInt32 terminalValue = 0;
            UInt32 tempTxPowerAdc = 0;
            ArrayList tempProcessDate = new ArrayList();
            isErAdjustOk = OnesectionMethod(AdjustEYE_Struce.ImodStart, AdjustEYE_Struce.ImodStep, AdjustEYE_Struce.TxErTarget, AdjustEYE_Struce.TxErTolerance, AdjustEYE_Struce.ImodMax, AdjustEYE_Struce.ImodMin, AdjustEYE_Struce.Spc_Er_Max, AdjustEYE_Struce.Spc_Er_Min, pScope, dut, 1, out txPowerADC, out erortxPowerValueArray, out tempTxPowerAdc, out tempProcessDate, out terminalValue, out targetER);
            imodDacTarget = terminalValue;
            #region AddAdjustErLogo
            for (byte i = 0; i < tempProcessDate.Count; i++)
            {
                logoStr += logger.AdapterLogString(1, "Modulation:" + tempProcessDate[i].ToString());

            }
            for (byte i = 0; i < erortxPowerValueArray.Count; i++)
            {
                logoStr += logger.AdapterLogString(1, "Er:" + erortxPowerValueArray[i].ToString());
            }

            logoStr += logger.AdapterLogString(1, "TargetIModDac=" + imodDacTarget.ToString());

            logoStr += logger.AdapterLogString(1, isErAdjustOk.ToString());
            Dr_CurrentCondition["TargetModDac"] = imodDacTarget;

            #endregion

            return true;
        }

        private bool SingleAdjustTxPower()
        {

            logoStr += logger.AdapterLogString(0, "Step4...Start Adjust TxPower");

            ArrayList tempProcessDate = new ArrayList();
            UInt32 terminalValue = 0;
            UInt32 tempTxPowerAdc = 0;
            isTxPowerAdjustOk = OnesectionMethod(AdjustEYE_Struce.IbiasStart, AdjustEYE_Struce.IbiasStep, AdjustEYE_Struce.TxLOPTarget, AdjustEYE_Struce.TxLOPTolerance, AdjustEYE_Struce.IbiasMax, AdjustEYE_Struce.IbiasMin, AdjustEYE_Struce.Spc_TxPower_Max, AdjustEYE_Struce.Spc_TxPower_Min, pScope, dut, 0, out txPowerADC, out erortxPowerValueArray, out tempTxPowerAdc, out tempProcessDate, out terminalValue, out targetLOP);
            ibiasDacTarget = terminalValue;
            txpowerAdcTarget = tempTxPowerAdc;

            #region AddAdjustTxPowerLogo 并记录当前 TargetIbiasDac TargetTxPowerAdc 到缓存的Datatable
            for (byte i = 0; i < tempProcessDate.Count; i++)
            {
                logoStr += logger.AdapterLogString(1, "Ibias:" + tempProcessDate[i].ToString());

            }
            for (byte i = 0; i < erortxPowerValueArray.Count; i++)
            {
                logoStr += logger.AdapterLogString(1, "TxPower:" + erortxPowerValueArray[i].ToString());

            }
            for (byte i = 0; i < txPowerADC.Count; i++)
            {
                logoStr += logger.AdapterLogString(1, "TxPowerAdc:" + txPowerADC[i].ToString());
            }
            logoStr += logger.AdapterLogString(1, "TargetIbiasDac=" + ibiasDacTarget.ToString());
            logoStr += logger.AdapterLogString(1, "TargetTxPowerAdc=" + txpowerAdcTarget.ToString());
            logoStr += logger.AdapterLogString(1, isTxPowerAdjustOk.ToString());

            Dr_CurrentCondition["TargetIbiasDac"] = ibiasDacTarget;
            Dr_CurrentCondition["TargetTxPowerAdc"] = txpowerAdcTarget;


            #endregion


            return true;
        }

        protected override bool AnalysisOutputParameters(TestModeEquipmentParameters[] InformationList)
        {          
            if (InformationList.Length == 0)//InformationList is null
            {
                return false;
            }
            else//  InformationList is not null
            {
                int index = -1;
                for (byte i = 0; i < InformationList.Length; i++)
                {
                    if (algorithm.FindFileName(InformationList, "TXLOPTARGET(UW)", out index))
                    {
                        if (AdjustEYE_Struce.AC_DC_Coupling == 1)
                        {
                            if (txTargetLopArray.ContainsKey(GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()))
                            {
                                InformationList[index].DefaultValue = Convert.ToString(txTargetLopArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()]);
                            }                          
                               
                            
                        } 
                        else
                        {
                             InformationList[index].DefaultValue = targetLOP.ToString().Trim();
                        }
                       
                        
                    }
                    if (algorithm.FindFileName(InformationList, "TXERTARGET(DB)", out index))
                    {
                        InformationList[index].DefaultValue = targetER.ToString().Trim();
                       
                    }
                    if (algorithm.FindFileName(InformationList, "ARRAYLISTTXMODCOEF", out index))
                    {
                        InformationList[index].DefaultValue = algorithm.ArrayListToStringArraySegregateByPunctuations(modulationCoefArray, ",");
                        
                    }

                    if (algorithm.FindFileName(InformationList, "ARRAYLISTTXPOWERCOEF", out index))
                    {
                        if (AdjustEYE_Struce.Loop_open_close_both == 0)
                        {
                            InformationList[index].DefaultValue = algorithm.ArrayListToStringArraySegregateByPunctuations(openLoopTxPowerCoefArray, ",") + "," + algorithm.ArrayListToStringArraySegregateByPunctuations(closeLoopTxPowerCoefArray, ",");

                            
                        }
                        if (AdjustEYE_Struce.Loop_open_close_both == 1)
                        {
                            InformationList[index].DefaultValue = algorithm.ArrayListToStringArraySegregateByPunctuations(openLoopTxPowerCoefArray, ",");
                            
                        }
                        else if (AdjustEYE_Struce.Loop_open_close_both == 2)
                        {
                            InformationList[index].DefaultValue = algorithm.ArrayListToStringArraySegregateByPunctuations(closeLoopTxPowerCoefArray, ",");
                           
                        }

                    }
                               

                }
               
                return true;
            }
           
        }
      
        protected override bool AnalysisInputParameters(TestModeEquipmentParameters[] InformationList)
        {
            logoStr += logger.AdapterLogString(0, "Step1...Check InputParameters"); 
            
            if (InformationList.Length < inPutParametersNameArray.Count)
            {
                logoStr += logger.AdapterLogString(4, "InputParameters are not enough!"); 
                return false;
            }
            else
            {
                int index = -1;
                bool isParametersComplete = false;
                for (byte i = 0; i < inPutParametersNameArray.Count; i++)
                {
                    if (algorithm.FindFileName(InformationList, inPutParametersNameArray[i].ToString(), out index) == false)
                    {
                        logoStr += logger.AdapterLogString(4,inPutParametersNameArray[i].ToString() + "is not exist");                           
                        isParametersComplete = false;
                        return isParametersComplete;
                    }
                    else
                    {
                        isParametersComplete = true;
                        continue;
                    }

                }
                if (isParametersComplete)
                {
                    for (byte i = 0; i < InformationList.Length; i++)
                    {
                        if (algorithm.FindFileName(InformationList, "Autotune".ToUpper().Trim(), out index))
                        {
                            AdjustEYE_Struce.AutoTune = Convert.ToBoolean(InformationList[index].DefaultValue);
                            
                        }
                        if (algorithm.FindFileName(InformationList, "TxPowTarget(uw)".ToUpper().Trim(), out index))
                        {
                            AdjustEYE_Struce.TxLOPTarget = Convert.ToDouble(InformationList[index].DefaultValue);
                            
                        }
                        if (algorithm.FindFileName(InformationList, "TxPowtolerance(uw)".ToUpper().Trim(), out index))
                        {
                            AdjustEYE_Struce.TxLOPTolerance = Convert.ToDouble(InformationList[index].DefaultValue);
                           
                        }

                        if (algorithm.FindFileName(InformationList, "ibiasmax(mA)".ToUpper().Trim(), out index))
                        {
                            AdjustEYE_Struce.IbiasMax = Convert.ToUInt32(InformationList[index].DefaultValue);
                           
                        }
                        if (algorithm.FindFileName(InformationList, "Ibiasmin(mA)".ToUpper().Trim(), out index))
                        {
                            AdjustEYE_Struce.IbiasMin = Convert.ToUInt32(InformationList[index].DefaultValue);
                           
                        }
                        if (algorithm.FindFileName(InformationList, "Ibiasstart(mA)".ToUpper().Trim(), out index))
                        {
                            AdjustEYE_Struce.IbiasStart = Convert.ToUInt32(InformationList[index].DefaultValue);
                           
                        }
                        if (algorithm.FindFileName(InformationList, "IbiasMethod".ToUpper().Trim(), out index))
                        {
                            AdjustEYE_Struce.IbiasMethod = Convert.ToByte(InformationList[index].DefaultValue);
                          
                        }
                        if (algorithm.FindFileName(InformationList, "Ibiasstep(mA)".ToUpper().Trim(), out index))
                        {
                            AdjustEYE_Struce.IbiasStep = Convert.ToByte(InformationList[index].DefaultValue);
                           
                        }
                        if (algorithm.FindFileName(InformationList, "Imodmax(mA)".ToUpper().Trim(), out index))
                        {
                            AdjustEYE_Struce.ImodMax = Convert.ToUInt32(InformationList[index].DefaultValue);
                           
                        }
                        if (algorithm.FindFileName(InformationList, "Imodmin(mA)".ToUpper().Trim(), out index))
                        {
                            AdjustEYE_Struce.ImodMin = Convert.ToUInt32(InformationList[index].DefaultValue);
                           
                        }
                        if (algorithm.FindFileName(InformationList, "TxErtarget(db)".ToUpper().Trim(), out index))
                        {
                            AdjustEYE_Struce.TxErTarget = Convert.ToDouble(InformationList[index].DefaultValue);
                            
                        }
                        if (algorithm.FindFileName(InformationList, "TxErtolerance(db)".ToUpper().Trim(), out index))
                        {
                            AdjustEYE_Struce.TxErTolerance = Convert.ToDouble(InformationList[index].DefaultValue);
                           
                        }
                        if (algorithm.FindFileName(InformationList, "Imodstart(mA)".ToUpper().Trim(), out index))
                        {
                            AdjustEYE_Struce.ImodStart = Convert.ToUInt32(InformationList[index].DefaultValue);
                           
                        }
                        if (algorithm.FindFileName(InformationList, "Imodmethod".ToUpper().Trim(), out index))
                        {
                            AdjustEYE_Struce.ImodMethod = Convert.ToByte(InformationList[index].DefaultValue);
                            
                        }
                        if (algorithm.FindFileName(InformationList, "Imodstep".ToUpper().Trim(), out index))
                        {
                            AdjustEYE_Struce.ImodStep = Convert.ToByte(InformationList[index].DefaultValue);
                           
                        }
                        if (algorithm.FindFileName(InformationList, "Loop_open_close_both".ToUpper().Trim(), out index))
                        {
                            AdjustEYE_Struce.Loop_open_close_both = Convert.ToByte(InformationList[index].DefaultValue);
                           
                        }
                        if (algorithm.FindFileName(InformationList, "ErCoef_1st_2st_Pid".ToUpper().Trim(), out index))
                        {
                            AdjustEYE_Struce.ErCoef_1st_2st_Pid = Convert.ToByte(InformationList[index].DefaultValue);
                           
                        }
                        if (algorithm.FindFileName(InformationList, "TxPowCoef_1st_2st_Pid".ToUpper().Trim(), out index))
                        {
                            AdjustEYE_Struce.TxPowCoef_1st_2st_Pid = Convert.ToByte(InformationList[index].DefaultValue);
                            
                        }
                       
                        if (algorithm.FindFileName(InformationList, "AC_DC_Coupling".ToUpper().Trim(), out index))
                        {
                            AdjustEYE_Struce.AC_DC_Coupling = Convert.ToByte(InformationList[index].DefaultValue);

                        }
                        if (algorithm.FindFileName(InformationList, "IbiasPidcoefarray".ToUpper().Trim(), out index))
                        {
                            char[] tempCharArray = new char[] { ',' }; 
                            AdjustEYE_Struce.pidCoefArray = algorithm.StringtoArraylistDeletePunctuations(InformationList[index].DefaultValue, tempCharArray);

                        }
                        if (algorithm.FindFileName(InformationList, "Fixedmodarray".ToUpper().Trim(), out index))
                        {
                            char[] tempCharArray = new char[] { ',' };
                            AdjustEYE_Struce.FixedModArray = algorithm.StringtoArraylistDeletePunctuations(InformationList[index].DefaultValue, tempCharArray);

                        }
                        if (algorithm.FindFileName(InformationList, "Fixedibiasarray".ToUpper().Trim(), out index))
                        {
                            char[] tempCharArray = new char[] { ',' };
                            AdjustEYE_Struce.FixedIBiasArray = algorithm.StringtoArraylistDeletePunctuations(InformationList[index].DefaultValue, tempCharArray);

                        }
                        if (algorithm.FindFileName(InformationList, "Sleeptime".ToUpper().Trim(), out index))
                        {
                            AdjustEYE_Struce.SleepTime = Convert.ToUInt16(InformationList[index].DefaultValue);

                        }
                       
                    }

                }
                logoStr += logger.AdapterLogString(1, "OK!");  
                return true;
            }
        }
     
        protected bool PrepareEnvironment(EquipmentList aEquipList,byte mode=0)
        {
            if (selectedEquipList["SCOPE"] != null)
            {
                Scope tempScope = (Scope)selectedEquipList["SCOPE"];
                if (tempScope.SetMaskAlignMethod(1) &&
                   tempScope.SetMode(mode) &&
                   tempScope.MaskONOFF(false) &&
                   tempScope.SetRunTilOff() &&
                   tempScope.RunStop(true) &&
                   tempScope.OpenOpticalChannel(true) &&
                   tempScope.RunStop(true) &&
                   tempScope.ClearDisplay()
                   )
                {
                    return true;
                } 
                else
                {
                    return false;
                }
            } 
            else
            {
                return false;
            }
           
        }
    
        protected bool OnesectionMethod(UInt32 StartValue, byte step, double targetValue, double tolerence, UInt32 uperLimit, UInt32 lowLimit, double Ulspec, double Llspec, Scope scope, DUT dut, byte IbiasModulation, out ArrayList xArray, out ArrayList yArray, out UInt32 ibiasTargetADC, out ArrayList adjustProcessData, out UInt32 terminalValue, out double targetLOPorERValue)//ibias=0;modulation=1
        {
            ibiasTargetADC = 0;
            adjustProcessData = new ArrayList();
            xArray = new ArrayList();
            yArray = new ArrayList();
            xArray.Clear();
            yArray.Clear();
            adjustProcessData.Clear();
            byte adjustCount = 0;
            byte backUpCount = 0;
            byte totalExponentiationCount = Convert.ToByte(Math.Floor((Math.Log(Convert.ToDouble(step), 2))));
            double currentLOPValue = -1;
            double lastPointLOPValue = -1;
            double TxPowerADC = -1;
            byte[] writeData = new byte[1];
            bool dirDown = false;
            if (adjustCount==0)
            {
                scope.AutoScale(1);
                SetSleep(AdjustEYE_Struce.SleepTime);
            }
            scope.DisplayThreeEyes();
            do
            {
                {
                    switch (IbiasModulation)
                    {
                        case 0:
                            {
                                if (AdjustEYE_Struce.AC_DC_Coupling == 1)
                                {
                                    dut.WriteBiasDac(StartValue);
                                    currentLOPValue = dut.ReadDmiTxp();
                                    currentLOPValue = algorithm.ChangeDbmtoUw(currentLOPValue);
                                    UInt16 Temp;
                                    dut.ReadTxpADC(out Temp);
                                    TxPowerADC = Convert.ToDouble(Temp);
                                    break;
                                }
                                else
                                {
                                    dut.WriteBiasDac(StartValue);
                                    SetSleep(AdjustEYE_Struce.SleepTime);
                                    scope.ClearDisplay();
                                    scope.DisplayPowerWatt();                                   
                                    for (byte i = 0; i < 4; i++)
                                    {
                                        scope.SetScaleOffset( AdjustEYE_Struce.TxLOPTarget,1);
                                        currentLOPValue = scope.GetAveragePowerWatt();
                                        if (currentLOPValue >= 10000000)
                                        {
                                            scope.AutoScale(1);
                                            SetSleep(AdjustEYE_Struce.SleepTime);
                                            currentLOPValue = scope.GetAveragePowerWatt();
                                            if (currentLOPValue >= 10000000)
                                            {
                                                SetSleep(AdjustEYE_Struce.SleepTime);
                                                continue;
                                            }
                                            
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                    if (currentLOPValue >= 10000000)
                                    {
                                        MessageBox.Show("DCA ReadTxPowerError");
                                    }
                                    
                                    UInt16 Temp;
                                    dut.ReadTxpADC(out Temp);
                                    TxPowerADC = Convert.ToDouble(Temp);

                                    break;
                                }
                              
                            }
                        case 1:
                            {
                                dut.WriteModDac(StartValue);
                                SetSleep(AdjustEYE_Struce.SleepTime);
                                scope.ClearDisplay();                                                             
                                scope.DisplayER();                                
                                 ReadER( out currentLOPValue);
                                if (currentLOPValue >= 10000000)
                                {
                                    MessageBox.Show("DCA Read ER Error");
                                }
                                break;
                            }

                        default:
                            {
                                break;
                            }

                    }
                    adjustProcessData.Add(StartValue);
                    if (adjustCount==0)
                    {
                        if (currentLOPValue > ((targetValue + tolerence)))
                        {
                            dirDown = true;
                        }
                        if (currentLOPValue < ((targetValue - tolerence)))
                        {
                            dirDown = false;
                        } 
                    } 
                   
                    if ((StartValue == uperLimit) && (currentLOPValue < ((targetValue - tolerence))) || (StartValue == lowLimit) && (currentLOPValue > ((targetValue + tolerence))))
                    {
                        terminalValue = StartValue;
                        targetLOPorERValue = currentLOPValue;
                        if (currentLOPValue > ((targetValue + tolerence)))
                        {
                            logoStr += logger.AdapterLogString(4, "DataBase input Parameters lowLimit is too large!");
                        }
                        else if (currentLOPValue < ((targetValue - tolerence)))
                        {
                            logoStr += logger.AdapterLogString(4, "DataBase input Parameters uperLimit is too small!");
                        }
                        logger.FlushLogBuffer();
                        return false;
                    }

                    if (dirDown)
                    {
                        if ((currentLOPValue > (targetValue + tolerence)))
                        {
                            int tempValue = (int)((int)(StartValue - (UInt32)Math.Pow(2, totalExponentiationCount - backUpCount)) <= lowLimit ? lowLimit : StartValue - (UInt32)Math.Pow(2, totalExponentiationCount - backUpCount));
                            StartValue = (UInt32)tempValue;
                        }
                        else if ((currentLOPValue < (targetValue -tolerence)))
                        {
                            StartValue += (UInt32)Math.Pow(2, totalExponentiationCount - backUpCount);
                            backUpCount++;
                            byte tempValue = (byte)((backUpCount) >= (byte)(totalExponentiationCount - 1) ? (byte)(totalExponentiationCount - 1) : backUpCount);
                            backUpCount = tempValue;
                            if (backUpCount < (byte)(totalExponentiationCount - 1))
                            {
                                int tempValue2 = (int)((int)(StartValue - (UInt32)Math.Pow(2, totalExponentiationCount - backUpCount)) <= lowLimit ? lowLimit : StartValue - (UInt32)Math.Pow(2, totalExponentiationCount - backUpCount));
                                StartValue = (UInt32)tempValue2;
                            }

                        }
                        if (IbiasModulation == 0)
                        {
                            xArray.Add(TxPowerADC);
                        }

                        yArray.Add(currentLOPValue);
                        lastPointLOPValue = currentLOPValue;
                    }
                    else if (dirDown==false)
                    {
                        if ((currentLOPValue < (targetValue - tolerence)))
                        {
                            int tempValue = (int)(StartValue + (UInt32)Math.Pow(2, totalExponentiationCount - backUpCount) >= uperLimit ? uperLimit : StartValue + (UInt32)Math.Pow(2, totalExponentiationCount - backUpCount));
                            StartValue = (UInt32)tempValue;
                        }
                        else if ((currentLOPValue > (targetValue + tolerence)))
                        {
                            int tempValue1 = (int)((int)(StartValue - (UInt32)Math.Pow(2, totalExponentiationCount - backUpCount)) <= 0 ? 0 : StartValue - (UInt32)Math.Pow(2, totalExponentiationCount - backUpCount));
                            StartValue = (UInt32)tempValue1;
                            backUpCount++;
                            byte tempValue = (byte)((backUpCount) >= (byte)(totalExponentiationCount - 1) ? (byte)(totalExponentiationCount - 1) : backUpCount);
                            backUpCount = tempValue;
                            if (backUpCount < (byte)(totalExponentiationCount - 1))
                            {
                                int tempValue2 = (int)(StartValue + (UInt32)Math.Pow(2, totalExponentiationCount - backUpCount) >= uperLimit ? uperLimit : StartValue + (UInt32)Math.Pow(2, totalExponentiationCount - backUpCount));
                                StartValue = (UInt32)tempValue2;
                            }
                            
                        }
                        if (IbiasModulation == 0)
                        {
                            xArray.Add(TxPowerADC);
                        }

                        yArray.Add(currentLOPValue);
                        lastPointLOPValue = currentLOPValue;
                    }
                    if ((currentLOPValue < (targetValue - tolerence) || currentLOPValue > (targetValue + tolerence)))
                    {
                        adjustCount++;
                    }

                }

            } while (adjustCount <= 30 && (currentLOPValue < (targetValue - tolerence) || currentLOPValue > (targetValue + tolerence)));
            if (IbiasModulation == 0)
            {
                UInt16 Temp;
                dut.ReadTxpADC(out Temp);
                //Convert.ToDouble(Temp);
                ibiasTargetADC = Temp;
            }
            if (StartValue > uperLimit || StartValue < lowLimit)
            {
                if (StartValue > uperLimit)
                {
                    StartValue = uperLimit;
                    if (IbiasModulation == 0)
                    {
                        if (AdjustEYE_Struce.AC_DC_Coupling == 1)
                        {
                            dut.WriteBiasDac(StartValue);
                            currentLOPValue = dut.ReadDmiTxp();
                            currentLOPValue = algorithm.ChangeDbmtoUw(currentLOPValue);
                        }
                        else
                        {
                            dut.WriteBiasDac(StartValue);
                            SetSleep(AdjustEYE_Struce.SleepTime);
                            scope.ClearDisplay();
                            terminalValue = StartValue;
                            scope.DisplayPowerWatt();
                            for (byte i = 0; i < 4; i++)
                            {
                                scope.SetScaleOffset(AdjustEYE_Struce.TxLOPTarget,1);
                                currentLOPValue = scope.GetAveragePowerWatt();
                                if (currentLOPValue >= 10000000)
                                {
                                    scope.AutoScale(1);
                                    SetSleep(AdjustEYE_Struce.SleepTime);
                                    currentLOPValue = scope.GetAveragePowerWatt();
                                    if (currentLOPValue >= 10000000)
                                    {
                                        SetSleep(AdjustEYE_Struce.SleepTime);
                                        continue;
                                    }
                                   
                                }
                                else
                                {
                                    break;
                                }
                            }
                            if (currentLOPValue >= 10000000)
                            {
                                MessageBox.Show("DCA ReadTxPowerError");
                            }
                        }
                       

                    }
                    else
                    {
                        dut.WriteModDac(StartValue);
                        SetSleep(AdjustEYE_Struce.SleepTime);
                        terminalValue = StartValue;
                        scope.ClearDisplay();
                        scope.DisplayER();                        
                        for (byte i = 0; i < 4; i++)
                        {
                            scope.SetScaleOffset(AdjustEYE_Struce.TxLOPTarget,1);                            
                            currentLOPValue = scope.GetEratio();
                            if (currentLOPValue >= 10000000)
                            {
                                scope.AutoScale(1);
                                SetSleep(AdjustEYE_Struce.SleepTime);
                                currentLOPValue = scope.GetEratio();
                                if (currentLOPValue >= 10000000)
                                {
                                    SetSleep(AdjustEYE_Struce.SleepTime);
                                    continue;
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                        if (currentLOPValue >= 10000000)
                        {
                            MessageBox.Show("DCA Read ER Error");
                        }
                    }
                }
                else if (StartValue < lowLimit)
                {
                    StartValue = lowLimit;
                    if (IbiasModulation == 0)
                    {
                        if (AdjustEYE_Struce.AC_DC_Coupling == 1)
                        {
                            dut.WriteBiasDac(StartValue);
                            currentLOPValue = dut.ReadDmiTxp();
                            currentLOPValue = algorithm.ChangeDbmtoUw(currentLOPValue);
                        }
                        else
                        {
                            dut.WriteBiasDac(StartValue);
                            scope.ClearDisplay();                            
                            terminalValue = StartValue;
                            scope.DisplayPowerWatt();

                            for (byte i = 0; i < 4; i++)
                            {
                                scope.SetScaleOffset(AdjustEYE_Struce.TxLOPTarget,1);
                                currentLOPValue = scope.GetAveragePowerWatt();
                                if (currentLOPValue >= 10000000)
                                {
                                    scope.AutoScale(1);
                                    SetSleep(AdjustEYE_Struce.SleepTime);
                                    currentLOPValue = scope.GetAveragePowerWatt();
                                    if (currentLOPValue >= 10000000)
                                    {
                                        SetSleep(AdjustEYE_Struce.SleepTime);
                                        continue;
                                    }
                                }
                                else
                                {
                                    break;
                                }
                            }
                            if (currentLOPValue >= 10000000)
                            {
                                MessageBox.Show("DCA ReadTxPowerError");
                            }
                        }
                        
                    }
                    else
                    {
                        dut.WriteModDac(StartValue);
                        SetSleep(AdjustEYE_Struce.SleepTime);
                        terminalValue = StartValue;
                        scope.ClearDisplay();
                        scope.DisplayER();                        
                        for (byte i = 0; i < 4; i++)
                        {
                            scope.SetScaleOffset(AdjustEYE_Struce.TxLOPTarget,1);                           
                            currentLOPValue = scope.GetEratio();
                            if (currentLOPValue >= 10000000)
                            {
                                scope.AutoScale(1);
                                SetSleep(AdjustEYE_Struce.SleepTime);
                                currentLOPValue = scope.GetEratio();
                                if (currentLOPValue >= 10000000)
                                {
                                    SetSleep(AdjustEYE_Struce.SleepTime);
                                    continue;
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                        if (currentLOPValue >= 10000000)
                        {
                            MessageBox.Show("DCA Read ER Error");
                        }
                    }
                }
            }

            targetLOPorERValue = currentLOPValue;
            if (currentLOPValue >= (targetValue - tolerence) && currentLOPValue <= (targetValue + tolerence))
            {
                terminalValue = StartValue;
                if (IbiasModulation == 0)
                {
                    dut.StoreBiasDac(terminalValue);
                }
                if (IbiasModulation == 1)
                {
                    dut.StoreModDac(terminalValue);
                }
                return true;
            }
            else
            {
                terminalValue = StartValue;
                if (IbiasModulation == 0)
                {
                    dut.StoreBiasDac(terminalValue);
                }
                if (IbiasModulation == 1)
                {
                    dut.StoreModDac(terminalValue);
                }
                return false;
            }

        }       
       
        private bool OnesectionMethodERandPower(UInt32 startValueIbias, UInt32 startValueMod, byte stepIbias, byte stepImod, double targetValueIbias, double tolerenceIbias, UInt32 uperLimitIbias, UInt32 lowLimitIbias, double targetValueIMod, double tolerenceIMod, Scope scope, DUT dut, UInt32 uperLimitIMod, UInt32 lowLimitIMod, out UInt32 ibiasDacTarget, out UInt32 imodDacTarget, out double targetERValue, out UInt32 TxPowerAdc, out double targetLOPValue, out bool isERok, out bool isLopok)//ibias=0;modulation=1
        {
            isERok = false;
            isLopok = false;
            byte adjustCount = 0;
            byte backUpCountLop = 0;
            byte backUpCountEr = 0;
            byte totalExponentiationLopCount = Convert.ToByte(Math.Floor((Math.Log(Convert.ToDouble(stepIbias), 2))));
            byte totalExponentiationERCount = Convert.ToByte(Math.Floor((Math.Log(Convert.ToDouble(stepImod), 2))));
            double currentLOPValue = -1;
            double currentERValue = -1;
            targetLOPValue = -1;
            targetERValue = -1;
            TxPowerAdc = 0;
            byte[] writeData = new byte[1];
            do
            {
                if (startValueIbias > uperLimitIbias)
                {
                    startValueIbias = uperLimitIbias;

                }
                if (startValueMod > uperLimitIMod)
                {
                    startValueMod = uperLimitIMod;
                }
                if (startValueIbias < lowLimitIbias)
                {
                    startValueIbias = lowLimitIbias;

                }
                if (startValueMod < lowLimitIMod)
                {
                    startValueMod = lowLimitIMod;
                }
                {
                    
                        dut.WriteBiasDac(startValueIbias);
                        dut.WriteModDac(startValueMod);
                        scope.SetScaleOffset(AdjustEYE_Struce.TxLOPTarget, 1);
                        scope.ClearDisplay();
                        scope.DisplayPowerWatt();
                        currentLOPValue = scope.GetAveragePowerWatt();
                        scope.DisplayER();
                        currentERValue = scope.GetEratio();

                    if ((startValueIbias == uperLimitIbias) && (currentLOPValue < ((targetLOPValue - tolerenceIbias))) || (startValueIbias == lowLimitIbias) && (currentLOPValue > ((targetLOPValue + tolerenceIbias))))
                    {

                        if (currentLOPValue > ((targetLOPValue + tolerenceIbias)))
                        {
                            logoStr += logger.AdapterLogString(4, "DataBase input Parameters lowLimit is too large!");
                        }
                        else if (currentLOPValue < ((targetLOPValue - tolerenceIbias)))
                        {
                            logoStr += logger.AdapterLogString(4, "DataBase input Parameters uperLimit is too small!");
                        }
                        ibiasDacTarget = startValueIbias;
                        imodDacTarget = startValueMod;
                        logger.FlushLogBuffer();
                        return false;
                    }
                    if ((startValueMod == uperLimitIMod) && (currentERValue < ((targetERValue - tolerenceIMod))) || (startValueMod == lowLimitIMod) && (currentERValue > ((targetERValue + tolerenceIMod))))
                    {
                        if (currentERValue > ((targetERValue + tolerenceIMod)))
                        {
                            logoStr += logger.AdapterLogString(4, "DataBase input Parameters lowLimit is too large!");
                        }
                        else if (currentERValue < ((targetERValue - tolerenceIMod)))
                        {
                            logoStr += logger.AdapterLogString(4, "DataBase input Parameters uperLimit is too small!");
                        }
                        ibiasDacTarget = startValueIbias;
                        imodDacTarget = startValueMod;
                        logger.FlushLogBuffer();
                        return false;
                    }
                    if (adjustCount == 0)
                    {
                        if (currentLOPValue < ((targetValueIbias - tolerenceIbias)))
                        {
                            int tempValue = (int)(startValueIbias + (UInt32)Math.Pow(2, totalExponentiationLopCount - backUpCountLop) >= uperLimitIbias ? uperLimitIbias : startValueIbias + (UInt32)Math.Pow(2, totalExponentiationLopCount - backUpCountLop));
                            startValueIbias = (UInt32)tempValue;
                        }
                        if (currentERValue < ((targetValueIMod - tolerenceIMod)))
                        {
                            int tempValue = (int)(startValueMod + (UInt32)Math.Pow(2, totalExponentiationERCount - backUpCountEr) >= uperLimitIMod ? uperLimitIMod : startValueMod + (UInt32)Math.Pow(2, totalExponentiationERCount - backUpCountEr));
                            startValueMod = (UInt32)tempValue;
                        }
                        if (currentLOPValue > ((targetValueIbias + tolerenceIbias)))
                        {
                            do
                            {
                                int tempValue = (int)((startValueIbias - (UInt32)Math.Pow(2, totalExponentiationLopCount)) >= lowLimitIbias ? (startValueIbias - (UInt32)Math.Pow(2, totalExponentiationLopCount)) : lowLimitIbias);
                                startValueIbias = (UInt32)tempValue;
                                {
                                    dut.WriteBiasDac(startValueIbias);
                                    scope.ClearDisplay();
                                    scope.SetScaleOffset(AdjustEYE_Struce.TxLOPTarget, 1);
                                    scope.DisplayPowerWatt();
                                    currentLOPValue = scope.GetAveragePowerWatt();
                                }

                                if ((startValueIbias == lowLimitIbias) && (currentLOPValue > ((targetLOPValue + tolerenceIbias))))
                                {
                                    logoStr += logger.AdapterLogString(4, "DataBase input Parameters lowLimit is too large!");
                                    logger.FlushLogBuffer();
                                    ibiasDacTarget = startValueIbias;
                                    imodDacTarget = startValueMod;
                                    return false;
                                }
                            } while ((currentLOPValue > ((targetValueIbias + tolerenceIbias))));

                        }
                        if ((currentERValue > ((targetValueIMod + tolerenceIMod))))
                        {
                            do
                            {

                                int tempValue = (int)((startValueMod - (UInt32)Math.Pow(2, totalExponentiationERCount)) >= lowLimitIMod ? (startValueMod - (UInt32)Math.Pow(2, totalExponentiationERCount)) : lowLimitIMod);
                                startValueMod = (UInt32)tempValue;
                                {
                                    dut.WriteModDac(startValueMod);
                                    scope.ClearDisplay();
                                    scope.SetScaleOffset(AdjustEYE_Struce.TxLOPTarget, 1);
                                    scope.DisplayER();

                                    currentERValue = scope.GetEratio();

                                }
                                if ((startValueMod == lowLimitIMod) && (currentERValue > ((targetERValue + tolerenceIMod))))
                                {
                                    logoStr += logger.AdapterLogString(4, "DataBase input Parameters lowLimit is too large!");
                                    ibiasDacTarget = startValueIbias;
                                    imodDacTarget = startValueMod;
                                    logger.FlushLogBuffer();
                                    return false;
                                }
                            } while ((currentERValue > ((targetValueIMod + tolerenceIMod))));

                        }
                    }
                    else
                    {
                        if ((currentLOPValue < (targetValueIbias - tolerenceIbias)))
                        {
                            int tempValue = (int)(startValueIbias + (UInt32)Math.Pow(2, totalExponentiationLopCount - backUpCountLop) >= uperLimitIbias ? uperLimitIbias : startValueIbias + (UInt32)Math.Pow(2, totalExponentiationLopCount - backUpCountLop));
                            startValueIbias = (UInt32)tempValue;
                        }
                        else if ((currentLOPValue > (targetValueIbias + tolerenceIbias)))
                        {
                            int tempValue = (int)((startValueIbias - (UInt32)Math.Pow(2, totalExponentiationLopCount - backUpCountLop)) >= lowLimitIbias ? (startValueIbias - (UInt32)Math.Pow(2, totalExponentiationLopCount - backUpCountLop)) : lowLimitIbias);
                            startValueIbias = (UInt32)tempValue;
                            backUpCountLop++;
                            byte tempValue1 = (byte)((backUpCountLop) >= (byte)(totalExponentiationLopCount - 3) ? (byte)(totalExponentiationLopCount - 3) : backUpCountLop);
                            backUpCountLop = tempValue1;
                            if (backUpCountLop < (byte)(totalExponentiationLopCount - 3))
                            {
                                int tempValue2 = (int)(startValueIbias + (UInt32)Math.Pow(2, totalExponentiationLopCount - backUpCountLop) >= uperLimitIbias ? uperLimitIbias : startValueIbias + (UInt32)Math.Pow(2, totalExponentiationLopCount - backUpCountLop));
                                startValueIbias = (UInt32)tempValue2;
                            }

                        }
                        if ((currentERValue < (targetValueIMod - tolerenceIMod)))
                        {
                            int tempValue = (int)(startValueMod + (UInt32)Math.Pow(2, totalExponentiationERCount - backUpCountEr) >= uperLimitIMod ? uperLimitIMod : startValueMod + (UInt32)Math.Pow(2, totalExponentiationERCount - backUpCountEr));
                            startValueMod = (UInt32)tempValue;

                        }
                        else if ((currentERValue > (targetValueIMod + tolerenceIMod)))
                        {
                            int tempValue = (int)((startValueMod - (UInt32)Math.Pow(2, totalExponentiationERCount - backUpCountEr)) >= lowLimitIMod ? (startValueMod - (UInt32)Math.Pow(2, totalExponentiationERCount - backUpCountEr)) : lowLimitIMod);
                            startValueMod = (UInt32)tempValue;
                            backUpCountEr++;
                            byte tempValue1 = (byte)((backUpCountEr) >= (byte)(totalExponentiationERCount - 3) ? (byte)(totalExponentiationERCount - 3) : backUpCountEr);
                            backUpCountEr = tempValue1;
                            if (backUpCountEr < (byte)(totalExponentiationERCount - 3))
                            {
                                int tempValue2 = (int)(startValueMod + (UInt32)Math.Pow(2, totalExponentiationERCount - backUpCountEr) >= uperLimitIMod ? uperLimitIMod : startValueMod + (UInt32)Math.Pow(2, totalExponentiationERCount - backUpCountEr));
                                startValueMod = (UInt32)tempValue2;
                            }

                        }
                    }
                    if ((currentLOPValue < (targetValueIbias - tolerenceIbias) || currentLOPValue > (targetValueIbias + tolerenceIbias)) || (currentERValue < (targetValueIMod - tolerenceIMod) || currentERValue > (targetValueIMod + tolerenceIMod)))
                    {
                        adjustCount++;
                    }
                }

            } while (adjustCount <= 100 && (currentLOPValue < (targetValueIbias - tolerenceIbias) || currentLOPValue > (targetValueIbias + tolerenceIbias) || currentERValue < (targetValueIMod - tolerenceIMod) || currentERValue > (targetValueIMod + tolerenceIMod)));
            targetLOPValue = currentLOPValue;
            targetERValue = currentERValue;
            UInt16 Temp;
            dut.ReadTxpADC(out Temp);
            TxPowerAdc = Convert.ToUInt32(Temp);

            if (startValueIbias > uperLimitIbias || startValueIbias < lowLimitIbias)
            {
                if (startValueIbias > uperLimitIbias)
                {
                    startValueIbias = uperLimitIbias;
                    {
                        dut.WriteBiasDac(startValueIbias);
                        scope.ClearDisplay();
                        scope.SetScaleOffset(AdjustEYE_Struce.TxLOPTarget, 1);
                        scope.DisplayPowerWatt();
                        ibiasDacTarget = startValueIbias;
                        currentLOPValue = scope.GetAveragePowerWatt();
                    }
                }
                else if (startValueIbias < lowLimitIbias)
                {
                    startValueIbias = lowLimitIbias;
                    {
                        dut.WriteBiasDac(startValueIbias);
                        scope.ClearDisplay();
                        scope.SetScaleOffset(AdjustEYE_Struce.TxLOPTarget, 1);
                        scope.DisplayPowerWatt();
                        ibiasDacTarget = startValueIbias;
                        currentLOPValue = scope.GetAveragePowerWatt();
                    }
                }
            }
            if (startValueMod > uperLimitIMod || startValueMod < lowLimitIMod)
            {
                if (startValueMod > uperLimitIMod)
                {
                    startValueMod = uperLimitIMod;

                    {
                        dut.WriteModDac(startValueMod);
                        imodDacTarget = startValueMod;
                        scope.ClearDisplay();
                        scope.SetScaleOffset(AdjustEYE_Struce.TxLOPTarget, 1);
                        scope.DisplayER();

                        currentERValue = scope.GetEratio();
                        targetERValue = currentERValue;
                    }
                }
                else if (startValueMod < lowLimitIMod)
                {
                    startValueMod = lowLimitIMod;

                    {
                        dut.WriteModDac(startValueMod);
                        imodDacTarget = startValueMod;
                        scope.ClearDisplay();
                        scope.SetScaleOffset(AdjustEYE_Struce.TxLOPTarget, 1);
                        scope.DisplayER();
                        currentERValue = scope.GetEratio();
                        targetERValue = currentERValue;
                    }
                }
            }
            if (currentLOPValue >= (targetValueIbias - tolerenceIbias) && currentLOPValue <= (targetValueIbias + tolerenceIbias))
            {
                ibiasDacTarget = startValueIbias;
                isLopok = true;
            }
            else
            {
                ibiasDacTarget = startValueIbias;
                isLopok = false;
            }
            if (currentERValue >= (targetValueIMod - tolerenceIMod) && currentERValue <= (targetValueIMod + tolerenceIMod))
            {
                imodDacTarget = startValueMod;
                isERok = true;
            }
            else
            {
                imodDacTarget = startValueMod;
                isERok = false;
            }
            if (isERok && isLopok)
            {
                ibiasDacTarget = startValueIbias;
                imodDacTarget = startValueMod;
                dut.ReadTxpADC(out Temp);
                TxPowerAdc = Convert.ToUInt32(Temp);
                return true;
            }
            else
            {

                ibiasDacTarget = startValueIbias;
                imodDacTarget = startValueMod;
                dut.ReadTxpADC(out Temp);
                TxPowerAdc = Convert.ToUInt32(Temp);
                return false;
            }
        }

        private bool writeCurrentChannelPID(DUT inputDut)
        {
            bool isWriteCoefP = false;
            bool isWriteCoefI = false;
            bool isWriteCoefD = false;
            try
            {
                isWriteCoefP = inputDut.SetcoefP(AdjustEYE_Struce.pidCoefArray[0].ToString());
                isWriteCoefI = inputDut.SetcoefI(AdjustEYE_Struce.pidCoefArray[1].ToString());
                isWriteCoefD = inputDut.SetcoefD(AdjustEYE_Struce.pidCoefArray[2].ToString());
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.ToString());
                return false;
            }

            return isWriteCoefP && isWriteCoefI && isWriteCoefD;
        }

        private bool AdapterAllChannelFixedIBiasImod()
        {
            if ((AdjustEYE_Struce.FixedIBiasArray.Count != AdjustEYE_Struce.FixedModArray.Count) || AdjustEYE_Struce.FixedIBiasArray == null || AdjustEYE_Struce.FixedModArray == null || AdjustEYE_Struce.FixedModArray.Count == 0 || AdjustEYE_Struce.FixedIBiasArray.Count == 0)
            {
                return false;
            }
            if (!allChannelFixedIBias.ContainsKey(GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()))
            {

                allChannelFixedIBias.Add(GlobalParameters.CurrentChannel.ToString().Trim().ToUpper(), AdjustEYE_Struce.FixedIBiasArray[allChannelFixedIBias.Count].ToString().Trim());

            }
            else
            {
                allChannelFixedIBias[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()] = AdjustEYE_Struce.FixedIBiasArray[GlobalParameters.CurrentChannel - 1].ToString().Trim();
            }
            if (!allChannelFixedIMod.ContainsKey(GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()))
            {

                allChannelFixedIMod.Add(GlobalParameters.CurrentChannel.ToString().Trim().ToUpper(), AdjustEYE_Struce.FixedModArray[allChannelFixedIMod.Count].ToString().Trim());

            }
            else
            {
                allChannelFixedIMod[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()] = AdjustEYE_Struce.FixedModArray[GlobalParameters.CurrentChannel - 1].ToString().Trim();
            }

            return true;
        }
        private void SetSleep(UInt16 sleeptime = 100)
        {
            Thread.Sleep(sleeptime);
        }
#endregion
    }
}

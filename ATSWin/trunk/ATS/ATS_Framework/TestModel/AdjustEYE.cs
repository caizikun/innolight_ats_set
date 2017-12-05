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
    public enum AdjustEyeSpecs : byte
    {
        AP,
        ER,
        IBias,
        IMod,
        Crossing
    }
    
    public class AdjustEye : TestModelBase
    { 
#region Attribute
        private SortedList<string, AdjustEyeTargetValueRecordsStruct> adjustEyeTargetValueRecordsStructArray=new SortedList<string, AdjustEyeTargetValueRecordsStruct>();
        private AdjustEYEStruce adjustEYEStruce=new AdjustEYEStruce();
        
        private SortedList<string, string> tempratureADCArray = new SortedList<string, string>();
        private SortedList<string, string> txTargetLopArray = new SortedList<string, string>();
        private SortedList<string, string> allChannelFixedIMod = new SortedList<string, string>();
        private SortedList<string, string> allChannelFixedIBias= new SortedList<string, string>();
        private SortedList<string, string> allChannelFixedCrossDAC = new SortedList<string, string>();
        private ArrayList tempratureADCArrayList = new ArrayList();
        private ArrayList realtempratureArrayList = new ArrayList();
        private ArrayList txPowerADC = new ArrayList();
        private ArrayList erortxPowerValueArray = new ArrayList();
        private ArrayList inPutParametersNameArray = new ArrayList();
        private SortedList<byte, string> SpecNameArray = new SortedList<byte, string>();
        //private byte beCalledCount;
        private bool isTxPowerAdjustOk=false;
        private bool isErAdjustOk = false;
       
        //......
        private UInt32 ibiasDacTarget = 0;
        private UInt32 imodDacTarget = 0;
        private UInt32 txpowerAdcTarget = 0;
        private double targetLOP = -1;
        private double targetER = -1;
        private double targetCrossing = -1;
        //.....
      
        // cal txpower
        private float openLoopTxPowerCoefA;
        private float openLoopTxPowerCoefB;
        private float openLoopTxPowerCoefC;
        private float closeLoopTxPowerCoefA;
        private float closeLoopTxPowerCoefB;
        private float closeLoopTxPowerCoefC;       
        private ArrayList openLoopTxPowerCoefArray = new ArrayList();
        private ArrayList closeLoopTxPowerCoefArray = new ArrayList();
        private ArrayList pidTxPowerTempCoefCoefArray = new ArrayList();
        private bool isCalTxPowerOk;
        // cal txpower
        // cal er
        private float erModulationCoefA;
        private float erModulationCoefB;
        private float erModulationCoefC;
        private ArrayList modulationCoefArray = new ArrayList();
        private bool isCalErOk; 
        // cal er           
        // procdata
        private ArrayList procErData = new ArrayList();
        private ArrayList procTxPowerData = new ArrayList();
        private ArrayList procImodDACData = new ArrayList();
        private ArrayList procIbiasDACData = new ArrayList();
        private ArrayList procTxPowerADCData = new ArrayList();
        // procdata
        //pid
        bool isPidPIDCoefOk = false;
        bool isPidPointCoefOk = false;
        //pid
        private Scope ScopeObject;
        private Powersupply tempps;

        //private double IbiasDacStep;
        //private double IbiasDacStart;
        private double BiasCurrentMax;
        private double BiasCurrentMin;
       // private double IbiasDacStep;

        //private double IModDacStep;
        //private double IModDacStart;
        //private double IModMax;
        //private double IModMin;
        //sepcfile
        
#endregion
        
#region Method

        public AdjustEye(DUT inPuDut, logManager logmanager)
        {
            SpecNameArray.Clear();
            logger = logmanager;           
            logoStr = null;
            dut = inPuDut;            
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
            inPutParametersNameArray.Clear();
            procErData.Clear();
            procTxPowerData.Clear();
            procImodDACData.Clear();
            procIbiasDACData.Clear();
            procTxPowerADCData.Clear();
           
            inPutParametersNameArray.Add("IBIASTUNESTEP");
            inPutParametersNameArray.Add("IMODTUNESTEP");
            inPutParametersNameArray.Add("PIDCOEFARRAY");
            inPutParametersNameArray.Add("IMODINITIALIZATIONARRAY");
            inPutParametersNameArray.Add("IBIASINITIALIZATIONARRAY");
            inPutParametersNameArray.Add("CROSSINITIALIZATIONARRAY");
            inPutParametersNameArray.Add("SLEEPTIME");
            inPutParametersNameArray.Add("ADJUSTERUL");
            inPutParametersNameArray.Add("ADJUSTERLL");
            inPutParametersNameArray.Add("ADJUSTTXPOWERUL");
            inPutParametersNameArray.Add("ADJUSTTXPOWERLL");

            inPutParametersNameArray.Add("BiasDacMin");
            inPutParametersNameArray.Add("BiasDacMax");
            inPutParametersNameArray.Add("ModDacMin");
            inPutParametersNameArray.Add("ModDacMax");
            inPutParametersNameArray.Add("TargetCurrent");
            inPutParametersNameArray.Add("TecTemp");
            //TecTemp 
            //...

            SpecNameArray.Add((byte)AdjustEyeSpecs.AP, "AP(dBm)");
            SpecNameArray.Add((byte)AdjustEyeSpecs.ER, "ER(dB)");
            SpecNameArray.Add((byte)AdjustEyeSpecs.IBias, "IBias(mA)");
            SpecNameArray.Add((byte)AdjustEyeSpecs.IMod, "IMod(mA)");
            SpecNameArray.Add((byte)AdjustEyeSpecs.Crossing, "Crossing(%)");
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
                if (!selectedEquipList.Values[i].OutPutSwitch(true)) return false;
            }
            return true;
        }
        public override bool SelectEquipment(EquipmentList aEquipList)
        {
            selectedEquipList.Clear();
            if (aEquipList.Count == 0)
            {               
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
                    if (tempKeys[i].ToUpper().Contains("NA_OPTICALSWITCH"))
                    {
                        tempValues[i].CheckEquipmentRole(1, GlobalParameters.CurrentChannel);
                    }
                   
                }
                tempps = (Powersupply)selectedEquipList["POWERSUPPLY"];
                ScopeObject = (Scope)selectedEquipList["SCOPE"];
                if (tempps != null && ScopeObject != null)
                {
                    isOK = true;

                }
                else
                {
                    if (ScopeObject == null)
                    {
                        logoStr += logger.AdapterLogString(3, "SCOPE =NULL");
                    }                    
                    if (tempps == null)
                    {
                        logoStr += logger.AdapterLogString(3, "POWERSUPPLY =NULL");
                    }
                    isOK = false;
                    OutPutandFlushLog();
                }
                if (isOK)
                {
                    selectedEquipList.Add("DUT", dut);
                    return isOK;
                }
                return isOK;
            }
            
        }
        private void ClearProcessData()
        {
            procImodDACData.Clear();
            procIbiasDACData.Clear();
            procErData.Clear();
            procTxPowerData.Clear();
            procTxPowerADCData.Clear();

        }
        protected override bool StartTest()
        {
            logger.FlushLogBuffer();
            ClearProcessData();
            logoStr = "";
            GenerateSpecList(SpecNameArray);
          
            if (LoadPNSpec()==false||AnalysisInputParameters(inputParameters) == false)
            {
                OutPutandFlushLog();
                return false;
            }
           
            AddCurrentTemprature();
            if (PrepareEnvironment(selectedEquipList) == false)
            {
            
                logger.AdapterLogString(3, "PrepareEnvironment Error!");
                OutPutandFlushLog();
                return false;
            }
            if (AdapterAllChannelFixedIBiasImod() == false )
            {
                OutPutandFlushLog();
                return false;
            }

           // CalculateIbaisandImodDacfromExprssion();           
            
            if (ScopeObject != null && tempps != null)
            {
               
                {
                    CloseandOpenAPC(Convert.ToByte(APCMODE.IBAISandIMODOFF));
                }
                ScopeObject.DisplayThreeEyes(1);



                if (GlobalParameters.coupleType==Convert.ToByte(CoupleType.AC))
                {
                    ACCouple(tempps);                    
                    CollectCurvingParameters();
                    ScopeObject.DisplayCrossing();
                   // targetCrossing = ScopeObject.GetCrossing();
                    if (GlobalParameters.TecPresent==Convert.ToByte(tecpresent.notTec))
                    {
                        switch (GlobalParameters.APCType)
                        {
                            case (byte)apctype.PIDCloseLoop:
                                {
                                    if (!CurveERandWriteCoefs())
                                    {
                                        OutPutandFlushLog();
                                        return false;
                                    }
                                }
                                break;
                            case (byte)apctype.CloseLoop:
                            case (byte)apctype.OpenLoop:
                                {
                                    if (!CurveTxPowerandWriteCoefs())
                                    {
                                        OutPutandFlushLog();
                                        return false;
                                    }
                                    if (!CurveERandWriteCoefs())
                                    {
                                        OutPutandFlushLog();
                                        return false;
                                    }
                                }
                                break;
                                default:break;
                        }
                       
                    }
                   
                }
                else if (GlobalParameters.coupleType == Convert.ToByte(CoupleType.DC))
                {

                    DCCouple(tempps);
                    CollectCurvingParameters();
                    ScopeObject.DisplayCrossing();
                   // targetCrossing = ScopeObject.GetCrossing();
                    if (GlobalParameters.TecPresent == Convert.ToByte(tecpresent.notTec))
                    {
                        switch (GlobalParameters.APCType)
                        {
                            case (byte)apctype.PIDCloseLoop:
                                {
                                    if (!CurveERandWriteCoefs())
                                    {
                                        OutPutandFlushLog();
                                        return false;
                                    }
                                }
                                break;
                            case (byte)apctype.CloseLoop:
                            case (byte)apctype.OpenLoop:
                                {
                                    if (!CurveERandWriteCoefs())
                                    {
                                        OutPutandFlushLog();
                                        return false;
                                    }
                                    if (!CurveTxPowerandWriteCoefs())
                                    {
                                        OutPutandFlushLog();
                                        return false;
                                    }
                                }
                                break;
                                default:break;
                        }
                      
                        
                     }
                  
                   
                   
                }

                OutPutandFlushLog();
            }
            else
            {
                logoStr += logger.AdapterLogString(4, "Equipments are not enough!");
                OutPutandFlushLog();
                return false;
            }
            return true;
        }
        protected override bool AnalysisOutputParameters(TestModeEquipmentParameters[] InformationList)
        {
            try
            {
                outputParameters = new TestModeEquipmentParameters[2];
                //outputParameters[0].FiledName = "CROSSING(%)";
                //outputParameters[0].DefaultValue = Convert.ToString(targetCrossing);
                outputParameters[1].FiledName = "AP(DBM)";
                outputParameters[1].DefaultValue = Convert.ToString(targetLOP);
                outputParameters[0].FiledName = "ER(DB)";
                outputParameters[0].DefaultValue = Convert.ToString(targetER); ;
                return true;

            }
            catch (System.Exception ex)
            {
                throw ex;
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
                bool isParametersComplete = true;
               
                if (isParametersComplete)
                {
                    //for (byte i = 0; i < InformationList.Length; i++)
                    {
                       
                        if (algorithm.FindFileName(InformationList, "IBIASTUNESTEP", out index))
                        {
                            double temp = Convert.ToDouble(InformationList[index].DefaultValue);
                            if (double.IsInfinity(temp) || double.IsNaN(temp))
                            {
                                logoStr += logger.AdapterLogString(4, InformationList[index].FiledName + "is wrong!");
                                adjustEYEStruce.IbiasStep = 8;
                            }
                            if (temp<=0)
                            {
                                adjustEYEStruce.IbiasStep = 8;
                            } 
                            else
                            {
                               // IbiasDacStep =temp;
                                adjustEYEStruce.IbiasStep =Convert.ToByte(temp);
                           
                            }
                           
                        }
                        if (algorithm.FindFileName(InformationList, "IMODTUNESTEP", out index))
                        {
                            double temp = Convert.ToDouble(InformationList[index].DefaultValue);
                            if (double.IsInfinity(temp) || double.IsNaN(temp))
                            {
                                logoStr += logger.AdapterLogString(4, InformationList[index].FiledName + "is wrong!");
                                adjustEYEStruce.ImodStep = 8;
                            }
                            if (temp <= 0)
                            {
                                adjustEYEStruce.ImodStep = 8;
                            }
                            else
                            {
                             //   IModDacStep = temp;
                                adjustEYEStruce.ImodStep = Convert.ToByte(temp);
                            }
                        }
                        if (algorithm.FindFileName(InformationList, "PIDCOEFARRAY", out index))
                        {
                            char[] tempCharArray = new char[] { ',' };
                            ArrayList tempAL = algorithm.StringtoArraylistDeletePunctuations(InformationList[index].DefaultValue, tempCharArray);
                            if (tempAL == null)
                            {
                                adjustEYEStruce.pidCoefArray = new ArrayList();
                                adjustEYEStruce.pidCoefArray.Add(0);
                                adjustEYEStruce.pidCoefArray.Add(0);
                                adjustEYEStruce.pidCoefArray.Add(0);
 
                            } 
                            else
                            {
                                adjustEYEStruce.pidCoefArray = tempAL;
                            }
                            

                        }
                        if (algorithm.FindFileName(InformationList, "IMODINITIALIZATIONARRAY", out index))
                        {
                            char[] tempCharArray = new char[] { ',' };
                            ArrayList tempAL = algorithm.StringtoArraylistDeletePunctuations(InformationList[index].DefaultValue, tempCharArray);
                            if (tempAL == null || tempAL.Count<=0)
                            {
                                logoStr += logger.AdapterLogString(4, InformationList[index].FiledName + "is null!");
                                return false;
                            }
                            else if (tempAL.Count > GlobalParameters.TotalChCount)
                            {
                                adjustEYEStruce.FixedModArray = new ArrayList();
                                for (int i = 0; i < GlobalParameters.TotalChCount;i++)
                                {
                                    adjustEYEStruce.FixedModArray.Add(tempAL[i]);
                                }
                            }
                            else
                            {
                                adjustEYEStruce.FixedModArray = tempAL;
                                adjustEYEStruce.ImodDACStart = Convert.ToUInt32(tempAL[GlobalParameters.CurrentChannel - 1]);
                            }
                           

                        }
                        if (algorithm.FindFileName(InformationList, "IBIASINITIALIZATIONARRAY", out index))
                        {
                            char[] tempCharArray = new char[] { ',' };
                            ArrayList tempAL= algorithm.StringtoArraylistDeletePunctuations(InformationList[index].DefaultValue, tempCharArray);
                            if (tempAL == null || tempAL.Count<=0)
                            {
                                logoStr += logger.AdapterLogString(4, InformationList[index].FiledName + "is null!");
                                return false;
                            }
                            else if (tempAL.Count > GlobalParameters.TotalChCount)
                            {
                                adjustEYEStruce.FixedIBiasArray = new ArrayList();
                                for (int i = 0; i < GlobalParameters.TotalChCount; i++)
                                {
                                    adjustEYEStruce.FixedIBiasArray.Add(tempAL[i]);
                                }
                            }
                            else
                            {
                                adjustEYEStruce.FixedIBiasArray =tempAL;
                                adjustEYEStruce.IbiasDACStart =Convert.ToUInt32( tempAL[GlobalParameters.CurrentChannel - 1]);
                            }
                          //  adjustEYEStruce.IbiasDACStart =  tempAL[GlobalParameters.CurrentChannel - 1];

                        }
                        if (algorithm.FindFileName(InformationList, "CROSSINITIALIZATIONARRAY", out index))
                        {
                            char[] tempCharArray = new char[] { ',' };
                            ArrayList tempAL= algorithm.StringtoArraylistDeletePunctuations(InformationList[index].DefaultValue, tempCharArray);
                            if (tempAL == null || tempAL.Count<=0)
                            {
                                adjustEYEStruce.FixedCrossDacArray = new ArrayList();
                                for (int i = 0; i < GlobalParameters.TotalChCount; i++)
                                {
                                    adjustEYEStruce.FixedCrossDacArray.Add(0);
                                }
                                
                            }
                            else if (tempAL.Count > GlobalParameters.TotalChCount)
                            {
                                adjustEYEStruce.FixedCrossDacArray = new ArrayList();
                                for (int i = 0; i < GlobalParameters.TotalChCount; i++)
                                {
                                    adjustEYEStruce.FixedCrossDacArray.Add(tempAL[i]);
                                }
                            }
                            else
                            {
                                adjustEYEStruce.FixedCrossDacArray = tempAL;

                            }
                            
                        }
                        if (algorithm.FindFileName(InformationList, "TargetCurrent", out index))
                        {
                            char[] tempCharArray = new char[] { ',' };
                            ArrayList tempAL = algorithm.StringtoArraylistDeletePunctuations(InformationList[index].DefaultValue, tempCharArray);
                            if (tempAL == null || tempAL.Count <= 0)
                            {
                                adjustEYEStruce.TargetCurrentArray = new ArrayList();
                                for (int i = 0; i < GlobalParameters.TotalChCount; i++)
                                {
                                    adjustEYEStruce.TargetCurrentArray.Add(0);
                                }

                            }
                            else if (tempAL.Count > GlobalParameters.TotalChCount)
                            {
                                adjustEYEStruce.TargetCurrentArray = new ArrayList();

                                for (int i = 0; i < GlobalParameters.TotalChCount; i++)
                                {
                                    adjustEYEStruce.TargetCurrentArray.Add(tempAL[i]);
                                }
                            }
                            else
                            {
                                adjustEYEStruce.TargetCurrentArray = tempAL;

                            }

                        }
                        if (algorithm.FindFileName(InformationList, "SLEEPTIME", out index))
                        {
                            double temp = Convert.ToDouble(InformationList[index].DefaultValue);
                            if (temp<0)
                            {
                                adjustEYEStruce.SleepTime = 100;
                            } 
                            else
                            {
                                adjustEYEStruce.SleepTime = Convert.ToUInt16(temp);
                            }                            

                        }
                       
                        //...
                        if (algorithm.FindFileName(InformationList, "ADJUSTERUL", out index))
                        {
                            double temp = Convert.ToDouble(InformationList[index].DefaultValue);
                            if (double.IsInfinity(temp) || double.IsNaN(temp))
                            {
                                logoStr += logger.AdapterLogString(4, InformationList[index].FiledName + "is wrong!");
                                return false;
                            }
                            else
                            {
                                adjustEYEStruce.AdjustErUL = temp;
                                if (adjustEYEStruce.TxErUL < adjustEYEStruce.AdjustErUL)
                                {
                                    adjustEYEStruce.AdjustErUL = adjustEYEStruce.TxErUL;
                                }
                                
                            }

                        }
                        if (algorithm.FindFileName(InformationList, "ADJUSTERLL", out index))
                        {
                            double temp = Convert.ToDouble(InformationList[index].DefaultValue);
                            if (double.IsInfinity(temp) || double.IsNaN(temp))
                            {
                                logoStr += logger.AdapterLogString(4, InformationList[index].FiledName + "is wrong!");
                                return false;
                            }
                            else
                            {
                                adjustEYEStruce.AdjustErLL = temp;
                                if (adjustEYEStruce.AdjustErLL < adjustEYEStruce.TxErLL)
                                {
                                    adjustEYEStruce.AdjustErLL = adjustEYEStruce.TxErLL;
                                }
                            }

                        }
                        if (algorithm.FindFileName(InformationList, "ADJUSTTXPOWERUL", out index))
                        {
                            double temp = Convert.ToDouble(InformationList[index].DefaultValue);
                            if (double.IsInfinity(temp) || double.IsNaN(temp))
                            {
                                logoStr += logger.AdapterLogString(4, InformationList[index].FiledName + "is wrong!");
                                return false;
                            }
                            else
                            {
                                adjustEYEStruce.AdjustTxLOPUL = temp;
                                if (adjustEYEStruce.AdjustTxLOPUL > adjustEYEStruce.TxLOPUL)
                                {
                                    adjustEYEStruce.AdjustTxLOPUL = adjustEYEStruce.TxLOPUL;
                                }
                            }

                        }

                        //DCCoupleAdjustMethod 
                        if (algorithm.FindFileName(InformationList, "ADJUSTTXPOWERLL", out index))
                        {
                            double temp = Convert.ToDouble(InformationList[index].DefaultValue);
                            if (double.IsInfinity(temp) || double.IsNaN(temp))
                            {
                                logoStr += logger.AdapterLogString(4, InformationList[index].FiledName + "is wrong!");
                                return false;
                            }
                            else
                            {
                                adjustEYEStruce.AdjustTxLOPLL = temp;
                                if (adjustEYEStruce.AdjustTxLOPLL < adjustEYEStruce.TxLOPLL)
                                {
                                    adjustEYEStruce.AdjustTxLOPLL = adjustEYEStruce.TxLOPLL;
                                }
                            }

                        }
                        if (algorithm.FindFileName(InformationList, "DCCOUPLEADJUSTMETHOD", out index))
                        {
                            double temp = Convert.ToDouble(InformationList[index].DefaultValue);
                            if (double.IsInfinity(temp) || double.IsNaN(temp))
                            {
                                logoStr += logger.AdapterLogString(4, InformationList[index].FiledName + "is wrong!");
                                return false;
                            }
                            else
                            {
                                adjustEYEStruce.DCCouple_AdjustMehtod =Convert.ToByte( temp);
                               
                            }

                        }

                        if (algorithm.FindFileName(InformationList, "BiasDacMin", out index))
                        {
                            UInt32 temp = Convert.ToUInt32(InformationList[index].DefaultValue);
                            if (double.IsInfinity(temp) || double.IsNaN(temp))
                            {
                                logoStr += logger.AdapterLogString(4, InformationList[index].FiledName + "is wrong!");
                                return false;
                            }
                            else
                            {
                                adjustEYEStruce.IbiasDACMin = temp;
                                
                               
                            }

                        }
                        if (algorithm.FindFileName(InformationList, "BiasDacMax", out index))
                        {
                            UInt32 temp = Convert.ToUInt32(InformationList[index].DefaultValue);
                            if (double.IsInfinity(temp) || double.IsNaN(temp))
                            {
                                logoStr += logger.AdapterLogString(4, InformationList[index].FiledName + "is wrong!");
                                return false;
                            }
                            else
                            {
                                adjustEYEStruce.IbiasDACMax = temp;


                            }


                            if (adjustEYEStruce.IbiasDACMax < adjustEYEStruce.IbiasDACMin)
                            {
                                adjustEYEStruce.IbiasDACMax = adjustEYEStruce.IbiasDACMin;
                            }

                        }
                        if (algorithm.FindFileName(InformationList, "ModDacMin", out index))
                        {
                            UInt32 temp = Convert.ToUInt32(InformationList[index].DefaultValue);
                            if (double.IsInfinity(temp) || double.IsNaN(temp))
                            {
                                logoStr += logger.AdapterLogString(4, InformationList[index].FiledName + "is wrong!");
                                return false;
                            }
                            else
                            {
                                adjustEYEStruce.ModDacMin = temp;

                            }

                        }
                        if (algorithm.FindFileName(InformationList, "ModDacMax", out index))
                        {
                            UInt32 temp = Convert.ToUInt32(InformationList[index].DefaultValue);
                            if (double.IsInfinity(temp) || double.IsNaN(temp))
                            {
                                logoStr += logger.AdapterLogString(4, InformationList[index].FiledName + "is wrong!");
                                return false;
                            }
                            else
                            {
                                adjustEYEStruce.ModDacMax = temp;
                            }

                        }
                        if (algorithm.FindFileName(InformationList, "TecTemp", out index))
                        {
                            double temp = Convert.ToDouble(InformationList[index].DefaultValue);
                            if (double.IsInfinity(temp) || double.IsNaN(temp))
                            {
                                logoStr += logger.AdapterLogString(4, InformationList[index].FiledName + "is wrong!");
                                return false;
                            }
                            else
                            {
                                adjustEYEStruce.TecTemp = temp;
                            }

                        }
                        //TecTemp

                    }
                    if (adjustEYEStruce.AdjustTxLOPUL <= adjustEYEStruce.AdjustTxLOPLL || adjustEYEStruce.AdjustErUL <= adjustEYEStruce.AdjustErLL)
                    {
                        logoStr += logger.AdapterLogString(4, "inputParameter wrong");  
                        return false;
                    }
                }
                logoStr += logger.AdapterLogString(1, "OK!");  
                return true;
            }
        }
        protected bool PrepareEnvironment(EquipmentList aEquipList,byte mode=0)
        {
           
            if (ScopeObject != null)
            {

                if (ScopeObject.SetMaskAlignMethod(1) &&
                   ScopeObject.SetMode(mode) &&
                   ScopeObject.MaskONOFF(false) &&
                   ScopeObject.SetRunTilOff() &&
                   ScopeObject.RunStop(true) &&
                   ScopeObject.OpenOpticalChannel(true) &&
                   ScopeObject.RunStop(true) &&
                   ScopeObject.ClearDisplay()&&
                    ScopeObject.EyeTuningDisplay(1)
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
        protected bool OnesectionMethod(UInt32 startValue, byte step, double targetValue, double targetUL,double targetLL, UInt32 uperLimit, UInt32 lowLimit, Scope scope, DUT dut, byte IbiasModulation, out ArrayList xArray, out ArrayList yArray, out UInt32 ibiasTargetADC, out ArrayList adjustProcessData, out UInt32 terminalValue, out double targetLOPorERValue)//ibias=0;modulation=1
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
                SetSleep(adjustEYEStruce.SleepTime);
            }
            scope.DisplayThreeEyes(1);
            do
            {
                {
                    switch (IbiasModulation)
                    {
                        case (byte)AdjustItems.ibias:
                            {
                                if (GlobalParameters.coupleType == Convert.ToByte(CoupleType.DC))
                                {
                                    dut.WriteBiasDac(startValue);
                                    if (GlobalParameters.APCType == Convert.ToByte(apctype.PIDCloseLoop))
                                    {
                                        currentLOPValue = dut.ReadDmiTxp();                                        
                                    }                                   
                                    UInt16 Temp;
                                    dut.ReadTxpADC(out Temp);
                                    TxPowerADC = Convert.ToDouble(Temp);
                                    break;
                                }
                                else
                                {
                                    dut.WriteBiasDac(startValue);
                                    SetSleep(adjustEYEStruce.SleepTime);
                                    scope.ClearDisplay();
                                 
                                    scope.DisplayPowerdbm();
                                    for (byte i = 0; i < 4; i++)
                                    {
                                        scope.SetScaleOffset( adjustEYEStruce.TxLOPTarget,1);
                                        currentLOPValue = scope.GetAveragePowerdbm();
                                        if (currentLOPValue >= 10000000)
                                        {
                                            scope.AutoScale(1);
                                            SetSleep(adjustEYEStruce.SleepTime);
                                            currentLOPValue = scope.GetAveragePowerdbm();
                                            if (currentLOPValue >= 10000000)
                                            {
                                                SetSleep(adjustEYEStruce.SleepTime);
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
                        case  (byte)AdjustItems.imod:
                            {
                                dut.WriteModDac(startValue);
                                SetSleep(adjustEYEStruce.SleepTime);
                                scope.ClearDisplay();                                                             
                                scope.DisplayER();                                
                                for (byte i = 0; i < 4; i++)
                                {
                                    scope.SetScaleOffset(adjustEYEStruce.TxLOPTarget,1);                                    
                                    currentLOPValue = scope.GetEratio();
                                    if (currentLOPValue >= 10000000)
                                    {
                                        scope.AutoScale(1);
                                        SetSleep(adjustEYEStruce.SleepTime);
                                        currentLOPValue = scope.GetEratio();
                                        if (currentLOPValue >= 10000000)
                                        {
                                            SetSleep(adjustEYEStruce.SleepTime);
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
                                break;
                            }

                        default:
                            {
                                break;
                            }

                    }
                    adjustProcessData.Add(startValue);
                    
                    if (adjustCount==0)
                    {
                        if (currentLOPValue >= targetLL && currentLOPValue <= targetUL)
                        {
                            terminalValue = startValue;
                            targetLOPorERValue = currentLOPValue;                            
                            yArray.Add(currentLOPValue);
                            if (IbiasModulation == 0)
                            {
                                UInt16 Temp;
                                dut.ReadTxpADC(out Temp);                               
                                ibiasTargetADC = Temp;
                                xArray.Add(ibiasTargetADC);
                            }
                            
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
                        if (currentLOPValue > ((targetUL)))
                        {
                            dirDown = true;
                        }
                        if (currentLOPValue < ((targetLL)))
                        {
                            dirDown = false;
                        } 
                    }

                    if ((startValue == uperLimit) && (currentLOPValue < ((targetLL))) || (startValue == lowLimit) && (currentLOPValue > ((targetUL))))
                    {
                        terminalValue = startValue;
                        targetLOPorERValue = currentLOPValue;
                        if (currentLOPValue > ((targetUL)))
                        {
                            logoStr += logger.AdapterLogString(4, "DataBase input Parameters lowLimit is too large!");
                        }
                        else if (currentLOPValue < ((targetLL)))
                        {
                            logoStr += logger.AdapterLogString(4, "DataBase input Parameters uperLimit is too small!");
                        }
                        logger.FlushLogBuffer();
                        return false;
                    }

                    if (dirDown)
                    {
                        if ((currentLOPValue > (targetUL)))
                        {
                            int tempValue = (int)((int)(startValue - (UInt32)Math.Pow(2, totalExponentiationCount - backUpCount)) <= lowLimit ? lowLimit : startValue - (UInt32)Math.Pow(2, totalExponentiationCount - backUpCount));
                            startValue = (UInt32)tempValue;
                        }
                        else if ((currentLOPValue < (targetLL)))
                        {
                            startValue += (UInt32)Math.Pow(2, totalExponentiationCount - backUpCount);
                            backUpCount++;
                            byte tempValue = (byte)((backUpCount) >= (byte)(totalExponentiationCount - 1) ? (byte)(totalExponentiationCount - 1) : backUpCount);
                            backUpCount = tempValue;
                            if (backUpCount < (byte)(totalExponentiationCount - 1))
                            {
                                int tempValue2 = (int)((int)(startValue - (UInt32)Math.Pow(2, totalExponentiationCount - backUpCount)) <= lowLimit ? lowLimit : startValue - (UInt32)Math.Pow(2, totalExponentiationCount - backUpCount));
                                startValue = (UInt32)tempValue2;
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
                        if ((currentLOPValue < (targetLL)))
                        {
                            int tempValue = (int)(startValue + (UInt32)Math.Pow(2, totalExponentiationCount - backUpCount) >= uperLimit ? uperLimit : startValue + (UInt32)Math.Pow(2, totalExponentiationCount - backUpCount));
                            startValue = (UInt32)tempValue;
                        }
                        else if ((currentLOPValue > (targetUL)))
                        {
                            int tempValue1 = (int)((int)(startValue - (UInt32)Math.Pow(2, totalExponentiationCount - backUpCount)) <= 0 ? 0 : startValue - (UInt32)Math.Pow(2, totalExponentiationCount - backUpCount));
                            startValue = (UInt32)tempValue1;
                            backUpCount++;
                            byte tempValue = (byte)((backUpCount) >= (byte)(totalExponentiationCount - 1) ? (byte)(totalExponentiationCount - 1) : backUpCount);
                            backUpCount = tempValue;
                            if (backUpCount < (byte)(totalExponentiationCount - 1))
                            {
                                int tempValue2 = (int)(startValue + (UInt32)Math.Pow(2, totalExponentiationCount - backUpCount) >= uperLimit ? uperLimit : startValue + (UInt32)Math.Pow(2, totalExponentiationCount - backUpCount));
                                startValue = (UInt32)tempValue2;
                            }
                            
                        }
                        if (IbiasModulation == 0)
                        {
                            xArray.Add(TxPowerADC);
                        }

                        yArray.Add(currentLOPValue);
                        lastPointLOPValue = currentLOPValue;
                    }
                    if ((currentLOPValue < (targetLL) || currentLOPValue > (targetUL)))
                    {
                        adjustCount++;
                    }

                }

            } while (adjustCount <= 30 && (currentLOPValue < (targetLL) || currentLOPValue > (targetUL)));
            if (IbiasModulation == 0)
            {
                UInt16 Temp;
                dut.ReadTxpADC(out Temp);
                //Convert.ToDouble(Temp);
                ibiasTargetADC = Temp;
            }
            if (startValue > uperLimit || startValue < lowLimit)
            {
                if (startValue > uperLimit)
                {
                    startValue = uperLimit;
                    if (IbiasModulation == 0)
                    {
                        if (GlobalParameters.coupleType == Convert.ToByte(CoupleType.DC))
                        {
                            dut.WriteBiasDac(startValue);
                            if (GlobalParameters.APCType == Convert.ToByte(apctype.PIDCloseLoop))
                            {
                                currentLOPValue = dut.ReadDmiTxp();
                            } 
                        }
                        else
                        {
                            dut.WriteBiasDac(startValue);
                            SetSleep(adjustEYEStruce.SleepTime);
                            scope.ClearDisplay();
                            terminalValue = startValue;
                            scope.DisplayPowerdbm();
                            for (byte i = 0; i < 4; i++)
                            {
                                scope.SetScaleOffset(adjustEYEStruce.TxLOPTarget,1);
                                currentLOPValue = scope.GetAveragePowerdbm();
                                if (currentLOPValue >= 10000000)
                                {
                                    scope.AutoScale(1);
                                    SetSleep(adjustEYEStruce.SleepTime);
                                    currentLOPValue = scope.GetAveragePowerdbm();
                                    if (currentLOPValue >= 10000000)
                                    {
                                        SetSleep(adjustEYEStruce.SleepTime);
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
                        dut.WriteModDac(startValue);
                        SetSleep(adjustEYEStruce.SleepTime);
                        terminalValue = startValue;
                        scope.ClearDisplay();
                        scope.DisplayER();                        
                        for (byte i = 0; i < 4; i++)
                        {
                            scope.SetScaleOffset(adjustEYEStruce.TxLOPTarget,1);                            
                            currentLOPValue = scope.GetEratio();
                            if (currentLOPValue >= 10000000)
                            {
                                scope.AutoScale(1);
                                SetSleep(adjustEYEStruce.SleepTime);
                                currentLOPValue = scope.GetEratio();
                                if (currentLOPValue >= 10000000)
                                {
                                    SetSleep(adjustEYEStruce.SleepTime);
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
                else if (startValue < lowLimit)
                {
                    startValue = lowLimit;
                    if (IbiasModulation == 0)
                    {
                        if (GlobalParameters.coupleType == Convert.ToByte(CoupleType.DC))
                        {
                            dut.WriteBiasDac(startValue);
                            if (GlobalParameters.APCType == Convert.ToByte(apctype.PIDCloseLoop))
                            {
                                currentLOPValue = dut.ReadDmiTxp();
                            }
                        }
                        else
                        {
                            dut.WriteBiasDac(startValue);
                            scope.ClearDisplay();                            
                            terminalValue = startValue;
                            scope.DisplayPowerdbm();

                            for (byte i = 0; i < 4; i++)
                            {
                                scope.SetScaleOffset(adjustEYEStruce.TxLOPTarget,1);
                                currentLOPValue = scope.GetAveragePowerdbm();
                                if (currentLOPValue >= 10000000)
                                {
                                    scope.AutoScale(1);
                                    SetSleep(adjustEYEStruce.SleepTime);
                                    currentLOPValue = scope.GetAveragePowerdbm();
                                    if (currentLOPValue >= 10000000)
                                    {
                                        SetSleep(adjustEYEStruce.SleepTime);
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
                        dut.WriteModDac(startValue);
                        SetSleep(adjustEYEStruce.SleepTime);
                        terminalValue = startValue;
                        scope.ClearDisplay();
                        scope.DisplayER();                        
                        for (byte i = 0; i < 4; i++)
                        {
                            scope.SetScaleOffset(adjustEYEStruce.TxLOPTarget,1);                           
                            currentLOPValue = scope.GetEratio();
                            if (currentLOPValue >= 10000000)
                            {
                                scope.AutoScale(1);
                                SetSleep(adjustEYEStruce.SleepTime);
                                currentLOPValue = scope.GetEratio();
                                if (currentLOPValue >= 10000000)
                                {
                                    SetSleep(adjustEYEStruce.SleepTime);
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
            if (currentLOPValue >= (targetLL) && currentLOPValue <= (targetUL))
            {
                terminalValue = startValue;
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
                terminalValue = startValue;
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
        private bool writeCurrentChannelPID(DUT inputDut)
        {
            bool isWriteCoefP = false;
            bool isWriteCoefI = false;
            bool isWriteCoefD = false;
            try
            {
                isWriteCoefP=inputDut.SetcoefP(adjustEYEStruce.pidCoefArray[0].ToString());
                isWriteCoefI=inputDut.SetcoefI(adjustEYEStruce.pidCoefArray[1].ToString());
                isWriteCoefD=inputDut.SetcoefD(adjustEYEStruce.pidCoefArray[2].ToString());
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.ToString());
                return false;
            }
           
            return isWriteCoefP && isWriteCoefI&&isWriteCoefD;
        }
        private bool AdapterAllChannelFixedIBiasImod()
        {
            if ((adjustEYEStruce.FixedIBiasArray.Count != adjustEYEStruce.FixedModArray.Count) || adjustEYEStruce.FixedIBiasArray == null || adjustEYEStruce.FixedModArray == null || adjustEYEStruce.FixedModArray.Count == 0 || adjustEYEStruce.FixedIBiasArray.Count == 0)
            {
                return false;
            }
            if (!allChannelFixedIBias.ContainsKey(GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()))
            {

                allChannelFixedIBias.Add(GlobalParameters.CurrentChannel.ToString().Trim().ToUpper(), adjustEYEStruce.FixedIBiasArray[allChannelFixedIBias.Count].ToString().Trim());

            }
            else
            {
                allChannelFixedIBias[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()] = adjustEYEStruce.FixedIBiasArray[GlobalParameters.CurrentChannel-1].ToString().Trim();
            }
            if (!allChannelFixedIMod.ContainsKey(GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()))
            {

                allChannelFixedIMod.Add(GlobalParameters.CurrentChannel.ToString().Trim().ToUpper(), adjustEYEStruce.FixedModArray[allChannelFixedIMod.Count].ToString().Trim());

            }
            else
            {
                allChannelFixedIMod[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()] = adjustEYEStruce.FixedModArray[GlobalParameters.CurrentChannel - 1].ToString().Trim();
            }

            return true;
        }
        private bool AdapterAllChannelFixedCrossingDAC()// FitCrossing 停止使用
        {
            //if (adjustEYEStruce.FixedCrossDacArray == null || adjustEYEStruce.FixedCrossDacArray.Count == 0)
            //{
            //    return false;
            //}
            //if (!allChannelFixedCrossDAC.ContainsKey(GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()))
            //{

            //    allChannelFixedCrossDAC.Add(GlobalParameters.CurrentChannel.ToString().Trim().ToUpper(), adjustEYEStruce.FixedCrossDacArray[allChannelFixedCrossDAC.Count].ToString().Trim());

            //}
            //else
            //{
            //    allChannelFixedCrossDAC[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()] = adjustEYEStruce.FixedCrossDacArray[GlobalParameters.CurrentChannel - 1].ToString().Trim();
            //}
           
            return true;
        }
        private void SetSleep(UInt16 sleeptime = 100)
        {
            Thread.Sleep(sleeptime);
        }
      
       
        protected bool LoadPNSpec()
        {            
            
            try
            {
                if (algorithm.FindDataInDataTable(specParameters, SpecTableStructArray, Convert.ToString(GlobalParameters.CurrentChannel)) == null)
                {
                    return false;
                }               

                adjustEYEStruce.TxLOPTarget = SpecTableStructArray[(byte)AdjustEyeSpecs.AP].TypicalValue;
                adjustEYEStruce.TxLOPLL = SpecTableStructArray[(byte)AdjustEyeSpecs.AP].MinValue;
                adjustEYEStruce.TxLOPUL = SpecTableStructArray[(byte)AdjustEyeSpecs.AP].MaxValue;

                adjustEYEStruce.TxErTarget = SpecTableStructArray[(byte)AdjustEyeSpecs.ER].TypicalValue;
                adjustEYEStruce.TxErLL = SpecTableStructArray[(byte)AdjustEyeSpecs.ER].MinValue;
                adjustEYEStruce.TxErUL = SpecTableStructArray[(byte)AdjustEyeSpecs.ER].MaxValue;

               // IbiasDacStart = Convert.ToUInt32(SpecTableStructArray[(byte)AdjustEyeSpecs.IBias].TypicalValue);

                BiasCurrentMin = Convert.ToUInt32(SpecTableStructArray[(byte)AdjustEyeSpecs.IBias].MinValue);
                BiasCurrentMax = Convert.ToUInt32(SpecTableStructArray[(byte)AdjustEyeSpecs.IBias].MaxValue);

                //IModDacStart = Convert.ToUInt32(SpecTableStructArray[(byte)AdjustEyeSpecs.IMod].TypicalValue);
                //IModMin = Convert.ToUInt32(SpecTableStructArray[(byte)AdjustEyeSpecs.IMod].MinValue);
                //IModMax = Convert.ToUInt32(SpecTableStructArray[(byte)AdjustEyeSpecs.IMod].MaxValue);

                adjustEYEStruce.CrossingTarget =(SpecTableStructArray[(byte)AdjustEyeSpecs.Crossing].TypicalValue);
                adjustEYEStruce.CrossingLL =(SpecTableStructArray[(byte)AdjustEyeSpecs.Crossing].MinValue);
                adjustEYEStruce.CrossingUL =(SpecTableStructArray[(byte)AdjustEyeSpecs.Crossing].MaxValue);
                
                return true;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
           
        }
        protected override bool AnalysisOutputProcData(TestModeEquipmentParameters[] InformationList)
        {
            
            try
            {
                procData = new TestModeEquipmentParameters[9];
                procData[0].FiledName = "TEMPADC";
                if (tempratureADCArray == null || tempratureADCArray.Count==0)
                {
                    procData[0].DefaultValue = "";
                } 
                else
                {
                    procData[0].DefaultValue = tempratureADCArray[Convert.ToString(GlobalParameters.CurrentTemp)];
                }
                
                procData[1].FiledName = "TARGETBIASDAC";
                procData[1].DefaultValue = Convert.ToString(ibiasDacTarget);
                procData[2].FiledName = "TARGETIMODDAC";
                procData[2].DefaultValue = Convert.ToString(imodDacTarget);
                procData[3].FiledName = "TARGETTXPOWERADC";
                procData[3].DefaultValue = Convert.ToString(txpowerAdcTarget);
                procData[4].FiledName = "PROCMODDACARRAY";
                procData[4].DefaultValue = algorithm.ArrayListToStringArraySegregateByPunctuations(procImodDACData, ",");
                procData[5].FiledName = "PROCERARRAY";
                procData[5].DefaultValue = algorithm.ArrayListToStringArraySegregateByPunctuations(procErData, ",");
                procData[6].FiledName = "PROCIBIASDACARRAY";
                procData[6].DefaultValue = algorithm.ArrayListToStringArraySegregateByPunctuations(procIbiasDACData, ",");
                procData[7].FiledName = "PROCTXPOWERDCAARRAY";
                procData[7].DefaultValue = algorithm.ArrayListToStringArraySegregateByPunctuations(procTxPowerData, ",");
                procData[8].FiledName = "PROCTXPOWERADC";
                procData[8].DefaultValue = algorithm.ArrayListToStringArraySegregateByPunctuations(procTxPowerADCData, ",");
                return true;

            }
            catch (System.Exception ex)
            {
                throw ex;
            }



        }
        protected bool AddCurrentTemprature()
        {
            string StrCoefB, StrCoefC;
            float Coefb, Coefc;


            try
            {
                #region  CheckTempChange

                if (!tempratureADCArray.ContainsKey(GlobalParameters.CurrentTemp.ToString().Trim().ToUpper()))
                {
                    logoStr += logger.AdapterLogString(0, "Step4...TempChanged Read tempratureADC");
                    logoStr += logger.AdapterLogString(1, "realtemprature=" + GlobalParameters.CurrentTemp.ToString());

                    UInt16 tempratureADC;
                    //   dut.ReadTempADC(out tempratureADC, 1);


                    if (adjustEYEStruce.TecTemp != -100)//表示 需要计算 TempADC
                    {

                        dut.ReadTempcoefb(out StrCoefB);
                        dut.ReadTempcoefc(out StrCoefC);
                        Coefb = Convert.ToSingle(StrCoefB);
                        Coefc = Convert.ToSingle(StrCoefC);

                        if (Coefb == 0)
                        {
                            tempratureADC = Convert.ToUInt16(Coefc);
                        }
                        else
                        {
                            tempratureADC = Convert.ToUInt16((adjustEYEStruce.TecTemp * 256 - Coefc) / Coefb);
                        }

                        logoStr += logger.AdapterLogString(1, "TempCoefB=" + StrCoefB.ToString());
                        logoStr += logger.AdapterLogString(1, "TempCoefc=" + StrCoefC.ToString());
                        logoStr += logger.AdapterLogString(1, "calculateTempADC=" + tempratureADC.ToString());

                    }
                    else
                    {

                        dut.ReadTempADC(out tempratureADC, 1);


                    }
                    logoStr += logger.AdapterLogString(1, "tempratureADC=" + tempratureADC.ToString());

                    tempratureADCArray.Add(GlobalParameters.CurrentTemp.ToString().Trim().ToUpper(), tempratureADC.ToString().Trim());
                    tempratureADCArrayList.Add(tempratureADC);
                    realtempratureArrayList.Add(GlobalParameters.CurrentTemp);
                }
                return true;
                #endregion
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

        }
        protected bool CurveERandWriteCoefs()
        {
            bool isWriteCoefAOk;
            bool  isWriteCoefBOk;
            bool isWriteCoefCOk;
            try
            {
                #region coef er
                if (adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].imodulaDacArray.Count >= 2 && tempratureADCArray.Count >= 2)
                {
                    logoStr += logger.AdapterLogString(0, "Step10...CurveCoef ER");

                    {
                        double[] tempTempArray = new double[tempratureADCArray.Count];
                        double[] tempModulationDacArray = new double[adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].imodulaDacArray.Count];
                      
                        for (byte i = 0; i < tempratureADCArrayList.Count; i++)
                        {
                            if (GlobalParameters.UsingCelsiusTemp)
                            {
                                tempTempArray[i] = Convert.ToDouble(realtempratureArrayList[i].ToString()) * 256;
                            }
                            else if (GlobalParameters.UsingCelsiusTemp == false)
                            {
                                tempTempArray[i] = Convert.ToDouble(tempratureADCArrayList[i].ToString());
                            }                           
                          
                        }
                        
                        for (byte i = 0; i < adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].imodulaDacArray.Count; i++)
                        {
                            tempModulationDacArray[i] = Convert.ToDouble(adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].imodulaDacArray[i].ToString());
                        }
                        for (int i = 0; i < tempTempArray.Length;i++ )
                        {
                            logoStr += logger.AdapterLogString(1, "tempTempArray[" + i.ToString() + "]=" + tempTempArray[i].ToString());
                     
                        }
                        for (int i = 0; i < tempModulationDacArray.Length; i++)
                        {
                            logoStr += logger.AdapterLogString(1, "tempModulationDacArray[" + i.ToString() + "]=" + tempModulationDacArray[i].ToString());

                        }
                        double[] coefArray = algorithm.MultiLine(tempTempArray, tempModulationDacArray, Math.Min(tempratureADCArray.Count, adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].imodulaDacArray.Count), 2);
                        erModulationCoefC = (float)coefArray[0];
                        erModulationCoefB = (float)coefArray[1];
                        erModulationCoefA = (float)coefArray[2];
                        modulationCoefArray = ArrayList.Adapter(coefArray);
                        modulationCoefArray.Reverse();
                        for (byte i = 0; i < modulationCoefArray.Count; i++)
                        {
                            logoStr += logger.AdapterLogString(1, "modulationCoefArray[" + i.ToString() + "]=" + modulationCoefArray[i].ToString() + " " + algorithm.ByteArraytoString(2, ",", algorithm.FloatToIEE754(modulationCoefArray[i])));
                        }
                        logoStr += logger.AdapterLogString(0, "Step12...WriteCoef");

                        #region W&R Moddaccoefc
                        isWriteCoefCOk = dut.SetModdaccoefc(erModulationCoefC.ToString());

                        if (isWriteCoefCOk)
                        {                            
                            logoStr += logger.AdapterLogString(1, "WriteCoeferModulationCoefC:" + isWriteCoefCOk.ToString());
                        }
                        else
                        {                           
                            logoStr += logger.AdapterLogString(3, "WriteCoeferModulationCoefC:" + isWriteCoefCOk.ToString());
                        }
                        #endregion
                        #region W&R Moddaccoefb
                        isWriteCoefBOk = dut.SetModdaccoefb(erModulationCoefB.ToString());
                        if (isWriteCoefBOk)
                        {
                            logoStr += logger.AdapterLogString(1, "WriteCoeferModulationCoefB:" + isWriteCoefBOk.ToString());

                        }
                        else
                        {
                            logoStr += logger.AdapterLogString(3, "WriteCoeferModulationCoefB:" + isWriteCoefBOk.ToString());
                        }
                        #endregion
                        #region W&R Moddaccoefa
                        isWriteCoefAOk = dut.SetModdaccoefa(erModulationCoefA.ToString());

                        if (isWriteCoefAOk)
                        {
                            logoStr += logger.AdapterLogString(1, "WriteCoeferModulationCoefA:" + isWriteCoefAOk.ToString());

                        }
                        else
                        {
                            logoStr += logger.AdapterLogString(3, "WriteCoeferModulationCoefA:" + isWriteCoefAOk.ToString());
                        }
                        #endregion

                        if (isWriteCoefAOk & isWriteCoefBOk & isWriteCoefCOk)
                        {
                            
                            logoStr += logger.AdapterLogString(1, "isCalErOk:" + true.ToString());
                            return true;
                        }
                        else
                        {
                            
                            logoStr += logger.AdapterLogString(3, "isCalErOk:" + false.ToString());
                            return false;
                        }

                    }
                }
                return true;
                #endregion
               
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        protected bool CurveTxPowerandWriteCoefs()
        {
            bool isWriteCoefAOk=false;
            bool isWriteCoefBOk=false;
            bool isWriteCoefCOk = false;
            bool isWriteOpenCoefAOk = false;
            bool isWriteOpenCoefBOk = false;
            bool isWriteOpenCoefCOk = false;
            try
            {
                #region  CurveCoef
                if (adjustEyeTargetValueRecordsStructArray.ContainsKey(GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()))
                {

                    if (adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].targetTxPowerADCArray.Count >= 2 && tempratureADCArray.Count >= 2 ||
                        (adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].ibiasDacArray.Count >= 2 && tempratureADCArray.Count >= 2))
                    {
                        logoStr += logger.AdapterLogString(0, "Step8...CurveCoef current channel");
                        logoStr += logger.AdapterLogString(1, "CurrentChannel=" + GlobalParameters.CurrentChannel.ToString());

                        #region openloop                       
                        {

                            {
                                double[] tempTempArray = new double[tempratureADCArray.Count];

                                double[] tempIbiasDacArray = new double[adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].ibiasDacArray.Count];

                                for (byte i = 0; i < tempratureADCArrayList.Count; i++)
                                {

                                    if (GlobalParameters.UsingCelsiusTemp)
                                    {
                                        tempTempArray[i] = Convert.ToDouble(realtempratureArrayList[i].ToString()) * 256;
                                    }
                                    else if (GlobalParameters.UsingCelsiusTemp == false)
                                    {
                                        tempTempArray[i] = Convert.ToDouble(tempratureADCArrayList[i].ToString());
                                    }   
                                }

                                for (byte i = 0; i < adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].ibiasDacArray.Count; i++)
                                {
                                    tempIbiasDacArray[i] = Convert.ToDouble(adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].ibiasDacArray[i].ToString());
                                }
                                for (int i = 0; i < tempTempArray.Length;i++ )
                                {
                                    logoStr += logger.AdapterLogString(1, "tempTempArray[" + i.ToString() + "]=" + tempTempArray[i].ToString());
                              
                                }

                                for (int i = 0; i < tempIbiasDacArray.Length; i++)
                                {
                                    logoStr += logger.AdapterLogString(1, "tempIbiasDacArray[" + i.ToString() + "]=" + tempIbiasDacArray[i].ToString());

                                }

                                double[] coefArray = algorithm.MultiLine(tempTempArray, tempIbiasDacArray, Math.Min(tempratureADCArray.Count, adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].ibiasDacArray.Count), 2);
                                openLoopTxPowerCoefC = (float)coefArray[0];
                                openLoopTxPowerCoefB = (float)coefArray[1];
                                openLoopTxPowerCoefA = (float)coefArray[2];

                                openLoopTxPowerCoefArray = ArrayList.Adapter(coefArray);
                                openLoopTxPowerCoefArray.Reverse();
                                for (byte i = 0; i < openLoopTxPowerCoefArray.Count; i++)
                                {
                                    logoStr += logger.AdapterLogString(1, "openLoopTxPowerCoefArray[" + i.ToString() + "]=" + openLoopTxPowerCoefArray[i].ToString() + " " + algorithm.ByteArraytoString(2, ",", algorithm.FloatToIEE754(openLoopTxPowerCoefArray[i])));
                                }
                                logoStr += logger.AdapterLogString(0, "Step9...WriteCoef");

                                #region W&R Biasdaccoefc
                                isWriteOpenCoefCOk = dut.SetBiasdaccoefc(openLoopTxPowerCoefC.ToString());


                                if (isWriteOpenCoefCOk)
                                {
                                    logoStr += logger.AdapterLogString(1, "isWriteOpenLoopCoefCOk:" + isWriteOpenCoefCOk.ToString());
                                }
                                else
                                {

                                    logoStr += logger.AdapterLogString(3, "isWriteOpenLoopCoefCOk:" + isWriteOpenCoefCOk.ToString());
                                }
                                #endregion
                                #region W&R Biasdaccoefb
                                isWriteOpenCoefBOk = dut.SetBiasdaccoefb(openLoopTxPowerCoefB.ToString());

                                if (isWriteOpenCoefBOk)
                                {
                                    logoStr += logger.AdapterLogString(1, "isWriteOpenLoopCoefBOk:" + isWriteOpenCoefBOk.ToString());
                                }
                                else
                                {
                                    logoStr += logger.AdapterLogString(3, "isWriteOpenLoopCoefBOk:" + isWriteOpenCoefBOk.ToString());
                                }
                                #endregion
                                #region W&R Biasdaccoefa
                                isWriteOpenCoefAOk = dut.SetBiasdaccoefa(openLoopTxPowerCoefA.ToString());


                                if (isWriteOpenCoefAOk)
                                {
                                    logoStr += logger.AdapterLogString(1, "isWriteOpenLoopCoefAOk:" + isWriteOpenCoefAOk.ToString());
                                }
                                else
                                {

                                    logoStr += logger.AdapterLogString(3, "isWriteOpenLoopCoefAOk:" + isWriteOpenCoefAOk.ToString());
                                }
                          

                                #endregion
                            }



                        }
                        #endregion
                        #region close loop

                        if (GlobalParameters.APCType == Convert.ToByte(apctype.CloseLoop))
                        {

                            {
                                double[] tempTempAdcArray = new double[tempratureADCArray.Count];                               
                                double[] tempTxPowerAdcArray = new double[adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].targetTxPowerADCArray.Count];
                                for (byte i = 0; i < tempratureADCArrayList.Count; i++)
                                {
                                    tempTempAdcArray[i] = Convert.ToDouble(tempratureADCArrayList[i].ToString());
                                }

                                for (byte i = 0; i < adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].targetTxPowerADCArray.Count; i++)
                                {
                                    tempTxPowerAdcArray[i] = Convert.ToDouble(adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].targetTxPowerADCArray[i].ToString());
                                }
                                for (int i = 0; i < tempratureADCArrayList.Count;i++ )
                                {
                                    logoStr += logger.AdapterLogString(1, "tempTempAdcArray[" + i.ToString() + "]=" + tempTempAdcArray[i].ToString() + " " + "tempTxPowerAdcArray[" + i.ToString() + "]=" + tempTxPowerAdcArray[i].ToString());

                                }
                                double[] coefArray1 = algorithm.MultiLine(tempTempAdcArray, tempTxPowerAdcArray, Math.Min(tempratureADCArray.Count, adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].targetTxPowerADCArray.Count), 2);
                                closeLoopTxPowerCoefC = (float)coefArray1[0];
                                closeLoopTxPowerCoefB = (float)coefArray1[1];
                                closeLoopTxPowerCoefA = (float)coefArray1[2];
                                closeLoopTxPowerCoefArray = ArrayList.Adapter(coefArray1);
                                closeLoopTxPowerCoefArray.Reverse();
                                for (byte i = 0; i < closeLoopTxPowerCoefArray.Count; i++)
                                {
                                    logoStr += logger.AdapterLogString(1, "closeLoopTxPowerCoefArray[" + i.ToString() + "]=" + closeLoopTxPowerCoefArray[i].ToString() + " " + algorithm.ByteArraytoString(2, ",", algorithm.FloatToIEE754(closeLoopTxPowerCoefArray[i])));

                                }
                                logoStr += logger.AdapterLogString(0, "Step9...WriteCoef"); 
                                #region W&R TxPowerAdccoefc
                                isWriteCoefCOk = dut.SetCloseLoopcoefc(closeLoopTxPowerCoefC.ToString());

                                if (isWriteCoefCOk)
                                { 
                                    logoStr += logger.AdapterLogString(1, "isWriteCloseLoopCoefCOk:" + isWriteCoefCOk.ToString());

                                }
                                else
                                {
                                    logoStr += logger.AdapterLogString(3, "isWriteCloseLoopCoefCOk:" + isWriteCoefCOk.ToString());
                                }
                                #endregion
                                #region W&R TxPowerAdccoefb
                                isWriteCoefBOk = dut.SetCloseLoopcoefb(closeLoopTxPowerCoefB.ToString());

                                if (isWriteCoefBOk)
                                {
                                    
                                    logoStr += logger.AdapterLogString(1, "isWriteCloseLoopCoefBOk:" + isWriteCoefBOk.ToString());

                                }
                                else
                                {

                                    logoStr += logger.AdapterLogString(3, "isWriteCloseLoopCoefBOk:" + isWriteCoefBOk.ToString());
                                }
                                #endregion
                                #region W&R TxPowerAdcccoefa
                                isWriteCoefAOk = dut.SetCloseLoopcoefa(closeLoopTxPowerCoefA.ToString());

                                if (isWriteCoefAOk)
                                {

                                    logoStr += logger.AdapterLogString(1, "isWriteCloseLoopCoefAOk:" + isWriteCoefAOk.ToString());

                                }
                                else
                                {

                                    logoStr += logger.AdapterLogString(3, "isWriteCloseLoopCoefAOk:" + isWriteCoefAOk.ToString());
                                }
                              

                                #endregion

                                if (isWriteCoefCOk & isWriteCoefBOk & isWriteCoefAOk
                                    & isWriteOpenCoefAOk & isWriteOpenCoefBOk & isWriteOpenCoefCOk)
                                {
                                    logoStr += logger.AdapterLogString(0, "Write Coefs ok");
                                }
                                else
                                {
                                    logoStr += logger.AdapterLogString(0, "Write Coefs fail!");
                                    return false;
                                }
                             
                            }
                           
                        }
                        #endregion
                        if (GlobalParameters.APCType == Convert.ToByte(apctype.OpenLoop))
                        {
                            if (isWriteOpenCoefAOk & isWriteOpenCoefBOk & isWriteOpenCoefCOk)
                            {
                                logoStr += logger.AdapterLogString(0, "Write Coefs ok");
                            }
                            else
                            {
                                logoStr += logger.AdapterLogString(0, "Write Coefs fail!");
                                return false;
                            }
                        }
                    }



                }

                #endregion
                return true;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        private Double ReadAp(out uint TxPowerADC)
        {
            double CurrentValue = -10;
            TxPowerADC = 0;
            SetSleep(adjustEYEStruce.SleepTime);
            ScopeObject.ClearDisplay();
            ScopeObject.DisplayPowerdbm();
            for (byte i = 0; i < 4; i++)
            {
                ScopeObject.SetScaleOffset(adjustEYEStruce.TxLOPTarget, 1);
                CurrentValue = ScopeObject.GetAveragePowerdbm();
                if (CurrentValue >= 10000000)
                {
                    ScopeObject.AutoScale(1);
                    SetSleep(adjustEYEStruce.SleepTime);
                    CurrentValue = ScopeObject.GetAveragePowerdbm();
                    if (CurrentValue >= 10000000)
                    {
                        SetSleep(adjustEYEStruce.SleepTime);
                        continue;
                    }

                }
                else
                {
                    break;
                }
            }
            if (CurrentValue >= 10000000)
            {
                MessageBox.Show("DCA ReadTxPowerError");
            }
            UInt16 Temp;
            dut.ReadTxpADC(out Temp);
            TxPowerADC = Temp;
            procTxPowerData.Add(ibiasDacTarget + ":" + imodDacTarget + "_" + CurrentValue);
            //procTxPowerData.Add(CurrentValue);
            procTxPowerADCData.Add(TxPowerADC);
            return CurrentValue;

        }
        private Double ReadER()
        {
                double CurrentValue = -10;
                SetSleep(adjustEYEStruce.SleepTime);
                ScopeObject.ClearDisplay();

                ScopeObject.DisplayER();

                for (byte i = 0; i < 4; i++)
                {
                    ScopeObject.SetScaleOffset(adjustEYEStruce.TxLOPTarget, 1);
                    CurrentValue = ScopeObject.GetEratio();
                    if (CurrentValue >= 10000000)
                    {
                        ScopeObject.AutoScale(1);
                        SetSleep(adjustEYEStruce.SleepTime);
                        CurrentValue = ScopeObject.GetEratio();
                        if (CurrentValue >= 10000000)
                        {
                            SetSleep(adjustEYEStruce.SleepTime);
                            continue;
                        }

                    }
                    else
                    {
                        break;
                    }
                }
                if (CurrentValue >= 10000000)
                {
                    MessageBox.Show("DCA Read ER Error");
                }
               
            return CurrentValue;

        }
        protected bool ACCouple(Powersupply tempPS)
        {
            try
            {
                #region NODCtoDC

                if (GlobalParameters.coupleType == Convert.ToByte(CoupleType.AC))
                {
                    switch (GlobalParameters.APCType)
                    {
                        case (byte)apctype.CloseLoop:
                        case (byte)apctype.OpenLoop:
                            {
                                logoStr += logger.AdapterLogString(0, "Step3...Fix ImodValue");
                                logoStr += logger.AdapterLogString(1, "FixedMod=" + allChannelFixedIMod[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()]);
                                if (allChannelFixedIMod.ContainsKey(GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()))
                                {
                                    dut.WriteModDac(Convert.ToUInt32(allChannelFixedIMod[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()])); // add parameters  
                                } 
                                else
                                {
                                    dut.WriteModDac(0);
                                }
                                //if (allChannelFixedCrossDAC.ContainsKey(GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()))
                                //    {
                                //        //dut.WriteCrossDac(Convert.ToUInt32(allChannelFixedCrossDAC[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()]));
                                //    } 
                                //    else
                                //    {
                                //        //dut.WriteCrossDac(0);
                                //    }

                                if (allChannelFixedIBias.ContainsKey(GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()))
                                {
                                    dut.WriteBiasDac(Convert.ToUInt32(allChannelFixedIBias[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()]));
                                } 
                                else
                                {
                                    dut.WriteBiasDac(0);
                                }

                                adjustEYEStruce.ImodDACStart = Convert.ToUInt32(allChannelFixedIMod[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()]);
                                adjustEYEStruce.IbiasDACStart = Convert.ToUInt32(allChannelFixedIBias[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()]);
                       
                                logoStr += logger.AdapterLogString(1, "SetScaleOffset");
                                ScopeObject.SetScaleOffset(adjustEYEStruce.TxLOPTarget, 1);
                                logoStr += logger.AdapterLogString(0, "Step4...Start Adjust TxPower");

                                ArrayList tempProcessDate = new ArrayList();
                                UInt32 terminalValue = 0;
                                UInt32 tempTxPowerAdc = 0;

                                targetLOP = ReadAp(out tempTxPowerAdc);
                                if (targetLOP >= adjustEYEStruce.AdjustTxLOPLL && targetLOP <= adjustEYEStruce.AdjustTxLOPUL)
                                {
                                    isTxPowerAdjustOk = true;
                                    terminalValue = Convert.ToUInt32(allChannelFixedIBias[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()]);
                                }
                                else
                                {
                                  //  isTxPowerAdjustOk = OnesectionMethod(adjustEYEStruce.IbiasDACStart, adjustEYEStruce.IbiasStep, adjustEYEStruce.TxLOPTarget, adjustEYEStruce.AdjustTxLOPUL, adjustEYEStruce.AdjustTxLOPLL, adjustEYEStruce.IbiasDACMax, adjustEYEStruce.IbiasDACMin, ScopeObject, dut, 0, out txPowerADC, out erortxPowerValueArray, out tempTxPowerAdc, out tempProcessDate, out terminalValue, out targetLOP);
                                    isTxPowerAdjustOk = OnesectionMethod(adjustEYEStruce.IbiasDACStart, adjustEYEStruce.IbiasStep, adjustEYEStruce.TxLOPTarget, adjustEYEStruce.AdjustTxLOPUL, adjustEYEStruce.AdjustTxLOPLL, adjustEYEStruce.IbiasDACMax, adjustEYEStruce.IbiasDACMin, ScopeObject, dut, 0, out txPowerADC, out erortxPowerValueArray, out tempTxPowerAdc, out tempProcessDate, out terminalValue, out targetLOP);
                                    
                                    if (!isTxPowerAdjustOk)
                                    {
                                        isTxPowerAdjustOk = (targetLOP >= adjustEYEStruce.TxLOPLL && targetLOP <= adjustEYEStruce.TxLOPUL) ? true : false;
                                    }
                                }
                                ibiasDacTarget = terminalValue;
                                txpowerAdcTarget = tempTxPowerAdc;

                                procIbiasDACData = tempProcessDate;
                                procTxPowerADCData = txPowerADC;
                                procTxPowerData = erortxPowerValueArray;
                                #region AddAdjustTxPowerLogo
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
                                #endregion


                                logoStr += logger.AdapterLogString(0, "Step6...StartAdjustEr");
                                targetER = ReadER();
                                if (targetER >= adjustEYEStruce.AdjustErLL && targetER <= adjustEYEStruce.AdjustErUL)
                                {
                                    terminalValue=Convert.ToUInt32(allChannelFixedIMod[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()]);
                                
                                }
                                else
                                {

                                    isErAdjustOk = OnesectionMethod(adjustEYEStruce.ImodDACStart, adjustEYEStruce.ImodStep, adjustEYEStruce.TxErTarget, adjustEYEStruce.AdjustErUL, adjustEYEStruce.AdjustErLL, adjustEYEStruce.ModDacMax, adjustEYEStruce.ModDacMin, ScopeObject, dut, 1, out txPowerADC, out erortxPowerValueArray, out tempTxPowerAdc, out tempProcessDate, out terminalValue, out targetER);
                                    if (!isErAdjustOk)
                                    {
                                        isErAdjustOk = (targetER >= adjustEYEStruce.TxErLL && targetER <= adjustEYEStruce.TxErUL) ? true : false;
                                    }
                                }
                                imodDacTarget = terminalValue;
                                procErData = erortxPowerValueArray;
                                procImodDACData = tempProcessDate;
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
                                #endregion
                                logger.FlushLogBuffer();
                            }
                            break;
                        case (byte)apctype.PIDCloseLoop:
                            {
                                logoStr += logger.AdapterLogString(0, "Step3...Fix ImodValue");
                                logoStr += logger.AdapterLogString(0, "Step4...Start Adjust TxPower");
                                logoStr += logger.AdapterLogString(0, "Step5...SetScaleOffset");

                                if (tempratureADCArray.Count == 1)
                                {
                                    if (allChannelFixedIMod.ContainsKey(GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()))
                                    {
                                        dut.WriteModDac(Convert.ToUInt32(allChannelFixedIMod[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()]));
                                    } 
                                    else
                                    {
                                        dut.WriteModDac(0);
                                    }
                                    if (allChannelFixedIBias.ContainsKey(GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()))
                                    {
                                        dut.WriteBiasDac(Convert.ToUInt32(allChannelFixedIBias[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()]));
                                    } 
                                    else
                                    {
                                        dut.WriteBiasDac(0);
                                    }
                                    adjustEYEStruce.ImodDACStart = Convert.ToUInt32(allChannelFixedIMod[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()]);
                                    adjustEYEStruce.IbiasDACStart = Convert.ToUInt32(allChannelFixedIBias[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()]);
                       
                                    //if (allChannelFixedCrossDAC.ContainsKey(GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()))
                                    //{
                                    //    dut.WriteCrossDac(Convert.ToUInt32(allChannelFixedCrossDAC[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()]));
                                    //} 
                                    //else
                                    //{
                                    //    dut.WriteCrossDac(0);
                                    //}
                                   
                                    double tempTargetTxPowerDBM = 0;
                                    tempTargetTxPowerDBM = dut.ReadDmiTxp();

                                    if (tempTargetTxPowerDBM > adjustEYEStruce.TxLOPUL || tempTargetTxPowerDBM < adjustEYEStruce.TxLOPLL)
                                    {
                                        ArrayList tempProcessDateTemp = new ArrayList();
                                        UInt32 terminalValueTemp = 0;
                                        UInt32 tempTxPowerAdcTemp = 0;
                                      //  isTxPowerAdjustOk = OnesectionMethod(adjustEYEStruce.IbiasDACStart, adjustEYEStruce.IbiasStep, adjustEYEStruce.TxLOPTarget, adjustEYEStruce.AdjustTxLOPUL, adjustEYEStruce.AdjustTxLOPLL, adjustEYEStruce.IbiasDACMax, adjustEYEStruce.IbiasDACMin, ScopeObject, dut, 0, out txPowerADC, out erortxPowerValueArray, out tempTxPowerAdcTemp, out tempProcessDateTemp, out terminalValueTemp, out targetLOP);
                                       
                                        isTxPowerAdjustOk = OnesectionMethod(adjustEYEStruce.IbiasDACStart, adjustEYEStruce.IbiasStep, adjustEYEStruce.TxLOPTarget, adjustEYEStruce.AdjustTxLOPUL, adjustEYEStruce.AdjustTxLOPLL, adjustEYEStruce.IbiasDACMax, adjustEYEStruce.IbiasDACMin,ScopeObject, dut, 0, out txPowerADC, out erortxPowerValueArray, out tempTxPowerAdcTemp, out tempProcessDateTemp, out terminalValueTemp, out targetLOP);
                                       
                                        if (!isTxPowerAdjustOk)
                                        {
                                            isErAdjustOk = (targetLOP >= adjustEYEStruce.TxLOPLL && targetLOP <= adjustEYEStruce.TxLOPUL) ? true : false;
                                        }
                                        ibiasDacTarget = terminalValueTemp;
                                        tempTargetTxPowerDBM = targetLOP;
                                        txpowerAdcTarget = tempTxPowerAdcTemp;

                                        procIbiasDACData = tempProcessDateTemp;
                                        procTxPowerADCData = txPowerADC;
                                        procTxPowerData = erortxPowerValueArray;
                                        #region AddAdjustTxPowerLogo
                                        for (byte i = 0; i < tempProcessDateTemp.Count; i++)
                                        {
                                            logoStr += logger.AdapterLogString(1, "Ibias:" + tempProcessDateTemp[i].ToString());

                                        }
                                        for (byte i = 0; i < erortxPowerValueArray.Count; i++)
                                        {
                                            logoStr += logger.AdapterLogString(1, "TxPower:" + erortxPowerValueArray[i].ToString());

                                        }
                                        for (byte i = 0; i < txPowerADC.Count; i++)
                                        {
                                            logoStr += logger.AdapterLogString(1, "TxPowerAdc:" + txPowerADC[i].ToString());

                                        }
                                        logoStr += logger.AdapterLogString(1, "tempTargetTxPower=" + tempTargetTxPowerDBM.ToString());
                                        logoStr += logger.AdapterLogString(1, "TargetIbiasDac=" + ibiasDacTarget.ToString());
                                        logoStr += logger.AdapterLogString(1, "TargetTxPowerAdc=" + txpowerAdcTarget.ToString());
                                        logoStr += logger.AdapterLogString(1, isTxPowerAdjustOk.ToString());

                                        #endregion
                                        logoStr += logger.AdapterLogString(3, "Adjust TargetTxPower Error");
                                        logoStr += logger.AdapterLogString(3, "CurrentBiasDAC=" + Convert.ToString(terminalValueTemp) + "CurrentTxLOPUW=" + Convert.ToString(targetLOP));

                                    }
                                    if (!txTargetLopArray.ContainsKey(GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()))
                                    {
                                        logoStr += logger.AdapterLogString(1, "txTargetLop=" + tempTargetTxPowerDBM.ToString());
                                        txTargetLopArray.Add(GlobalParameters.CurrentChannel.ToString().Trim().ToUpper(), tempTargetTxPowerDBM.ToString().Trim());
                                    }
                                    else
                                    {
                                        logoStr += logger.AdapterLogString(1, "txTargetLop=" + tempTargetTxPowerDBM.ToString());
                                        txTargetLopArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()] = tempTargetTxPowerDBM.ToString().Trim();
                                    }
                                    logoStr += logger.AdapterLogString(0, "Write PIDTarget:" + Convert.ToString(tempTargetTxPowerDBM) + "dbm");
                                    dut.APCON(0x01);
                                    isPidPointCoefOk = dut.SetPIDSetpoint(Convert.ToString(algorithm.ChangeDbmtoUw(tempTargetTxPowerDBM) * 10));
                                    logoStr += logger.AdapterLogString(3, "Write TargetTxPower" + isPidPointCoefOk.ToString());

                                    isPidPIDCoefOk = writeCurrentChannelPID(dut);
                                    logoStr += logger.AdapterLogString(3, "Write PID" + isPidPIDCoefOk.ToString());
                                    logger.FlushLogBuffer();
                                    tempPS.OutPutSwitch(false, 1);
                                    tempPS.OutPutSwitch(true, 1);
                                    dut.FullFunctionEnable();
                                }
                                if (tempratureADCArray.Count >= 2)
                                {
                                    
                                    {
                                        CloseandOpenAPC(Convert.ToByte(APCMODE.IBIASONandIMODOFF));
                                    }
                                    
                                }
                                ArrayList tempProcessDate = new ArrayList();
                                UInt32 terminalValue = 0;
                                UInt32 tempTxPowerAdc = 0;
                                logoStr += logger.AdapterLogString(0, "Step6...StartAdjustEr");
                                isErAdjustOk = OnesectionMethod(adjustEYEStruce.ImodDACStart, adjustEYEStruce.ImodStep, adjustEYEStruce.TxErTarget, adjustEYEStruce.AdjustErUL, adjustEYEStruce.AdjustErLL, adjustEYEStruce.ModDacMax, adjustEYEStruce.ModDacMin, ScopeObject, dut, 1, out txPowerADC, out erortxPowerValueArray, out tempTxPowerAdc, out tempProcessDate, out terminalValue, out targetER);
                                if (!isErAdjustOk)
                                {
                                    isErAdjustOk = (targetER >= adjustEYEStruce.TxErLL && targetER <= adjustEYEStruce.TxErUL) ? true : false;
                                }
                                imodDacTarget = terminalValue;
                                procErData = erortxPowerValueArray;
                                procImodDACData = tempProcessDate;
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
                                #endregion
                                logoStr += logger.AdapterLogString(3, "isErAdjustOk=" + isErAdjustOk.ToString());
                                logger.FlushLogBuffer(); 
                            }
                            break;
                        default:break;
                    }
                    
                }
                #endregion 
                return true;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        protected void CollectCurvingParameters()
        {
            try
            {
                #region  add current channel
                if (!adjustEyeTargetValueRecordsStructArray.ContainsKey(GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()))
                {
                    logoStr += logger.AdapterLogString(0, "Step5...add current channel records");
                    logoStr += logger.AdapterLogString(1, "GlobalParameters.CurrentChannel=" + GlobalParameters.CurrentChannel.ToString());

                    AdjustEyeTargetValueRecordsStruct tempstruct = new AdjustEyeTargetValueRecordsStruct();
                    tempstruct.ibiasDacArray = new ArrayList();
                    tempstruct.imodulaDacArray = new ArrayList();
                    tempstruct.targetTxPowerADCArray = new ArrayList();
                    adjustEyeTargetValueRecordsStructArray.Add(GlobalParameters.CurrentChannel.ToString().Trim().ToUpper(), tempstruct);
                    adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].targetTxPowerADCArray.Add(txpowerAdcTarget);
                    adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].ibiasDacArray.Add(ibiasDacTarget);
                    adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].imodulaDacArray.Add(imodDacTarget);
                }
                else
                {
                    adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].targetTxPowerADCArray.Add(txpowerAdcTarget);
                    adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].ibiasDacArray.Add(ibiasDacTarget);
                    adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].imodulaDacArray.Add(imodDacTarget);
                }

                #endregion
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        protected bool DCCouple(Powersupply tempPS)
         {
            try
            {
                if (GlobalParameters.coupleType == Convert.ToByte(CoupleType.DC))
                {


                    if (GlobalParameters.APCType == Convert.ToByte(apctype.PIDCloseLoop))
                    {
                        #region Close- PID                  
                        logoStr += logger.AdapterLogString(0, "Step3...Fix ImodValue");
                        logoStr += logger.AdapterLogString(0, "Step4...Start Adjust TxPower");
                        logoStr += logger.AdapterLogString(0, "Step5...SetScaleOffset");

                        if (tempratureADCArray.Count == 1)
                        {
                            if (allChannelFixedIMod.ContainsKey(GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()))
                            {
                                dut.WriteModDac(adjustEYEStruce.ImodDACStart);
                               // dut.WriteModDac(Convert.ToUInt32(allChannelFixedIMod[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()]));
                            } 
                            else
                            {
                                dut.WriteModDac(0);
                            }
                            if (allChannelFixedIBias.ContainsKey(GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()))
                            {
                                dut.WriteBiasDac(adjustEYEStruce.IbiasDACStart);
                                //dut.WriteBiasDac(Convert.ToUInt32(allChannelFixedIBias[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()]));
                            } 
                            else
                            {
                                dut.WriteBiasDac(0);
                            }
                            //adjustEYEStruce.ImodDACStart = Convert.ToUInt32(allChannelFixedIMod[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()]);
                            //adjustEYEStruce.IbiasDACStart = Convert.ToUInt32(allChannelFixedIBias[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()]);
                       
                            //if (allChannelFixedCrossDAC.ContainsKey(GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()))
                            //{
                            //    dut.WriteCrossDac(Convert.ToUInt32(allChannelFixedCrossDAC[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()]));
                            //} 
                            //else
                            //{
                            //    dut.WriteCrossDac(0);
                            //}
                            
                            double tempTargetTxPowerDBM = 0;
                            ScopeObject.DisplayPowerdbm();
                            ibiasDacTarget = adjustEYEStruce.IbiasDACStart;
                            tempTargetTxPowerDBM = dut.ReadDmiTxp();
                            targetLOP = tempTargetTxPowerDBM;

                            if (tempTargetTxPowerDBM > adjustEYEStruce.AdjustTxLOPUL || tempTargetTxPowerDBM < adjustEYEStruce.AdjustTxLOPLL)
                            {
                                ArrayList tempProcessDateTemp = new ArrayList();
                                UInt32 terminalValueTemp = 0;
                                UInt32 tempTxPowerAdcTemp = 0; 
                                isTxPowerAdjustOk = OnesectionMethod(adjustEYEStruce.IbiasDACStart, adjustEYEStruce.IbiasStep, adjustEYEStruce.TxLOPTarget, adjustEYEStruce.AdjustTxLOPUL, adjustEYEStruce.AdjustTxLOPLL, adjustEYEStruce.IbiasDACMax, adjustEYEStruce.IbiasDACMin,ScopeObject, dut, 0, out txPowerADC, out erortxPowerValueArray, out tempTxPowerAdcTemp, out tempProcessDateTemp, out terminalValueTemp, out targetLOP);
                                if (!isTxPowerAdjustOk)
                                {
                                   // isErAdjustOk = (targetLOP >= adjustEYEStruce.TxLOPLL && targetLOP <= adjustEYEStruce.TxLOPUL) ? true : false;
                                    isTxPowerAdjustOk = (targetLOP >= adjustEYEStruce.TxLOPLL && targetLOP <= adjustEYEStruce.TxLOPUL) ? true : false;
                               
                                }
                                ibiasDacTarget = terminalValueTemp;
                                tempTargetTxPowerDBM = targetLOP;
                                txpowerAdcTarget = tempTxPowerAdcTemp;

                                procIbiasDACData = tempProcessDateTemp;
                                procTxPowerADCData = txPowerADC;
                                procTxPowerData = erortxPowerValueArray;
                                #region AddAdjustTxPowerLogo
                                for (byte i = 0; i < tempProcessDateTemp.Count; i++)
                                {
                                    logoStr += logger.AdapterLogString(1, "Ibias:" + tempProcessDateTemp[i].ToString());

                                }
                                for (byte i = 0; i < erortxPowerValueArray.Count; i++)
                                {
                                    logoStr += logger.AdapterLogString(1, "TxPower:" + erortxPowerValueArray[i].ToString());

                                }
                                for (byte i = 0; i < txPowerADC.Count; i++)
                                {
                                    logoStr += logger.AdapterLogString(1, "TxPowerAdc:" + txPowerADC[i].ToString());

                                }
                                logoStr += logger.AdapterLogString(1, "tempTargetTxPower=" + tempTargetTxPowerDBM.ToString());
                                logoStr += logger.AdapterLogString(1, "TargetIbiasDac=" + ibiasDacTarget.ToString());
                                logoStr += logger.AdapterLogString(1, "TargetTxPowerAdc=" + txpowerAdcTarget.ToString());
                                logoStr += logger.AdapterLogString(1, isTxPowerAdjustOk.ToString());

                                #endregion                               
                                logoStr += logger.AdapterLogString(3, "Adjust TargetTxPower Error");
                                logoStr += logger.AdapterLogString(3, "CurrentBiasDAC=" + Convert.ToString(terminalValueTemp) + "CurrentTxLOPUW=" + Convert.ToString(targetLOP));
                               
                            }
                           
                            if (!txTargetLopArray.ContainsKey(GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()))
                            {
                                logoStr += logger.AdapterLogString(1, "txTargetLop=" + tempTargetTxPowerDBM.ToString());
                                txTargetLopArray.Add(GlobalParameters.CurrentChannel.ToString().Trim().ToUpper(), tempTargetTxPowerDBM.ToString().Trim());
                            }
                            else
                            {
                                logoStr += logger.AdapterLogString(1, "txTargetLop=" + tempTargetTxPowerDBM.ToString());
                                txTargetLopArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()] = tempTargetTxPowerDBM.ToString().Trim();
                            }
                            logoStr += logger.AdapterLogString(0, "Write PIDTarget:" + Convert.ToString(tempTargetTxPowerDBM) + "dbm");
                           // dut//.APCON(0x01);
                            isPidPointCoefOk = dut.SetPIDSetpoint(Convert.ToString(algorithm.ChangeDbmtoUw(tempTargetTxPowerDBM) * 10));                            
                            logoStr += logger.AdapterLogString(3, "Write TargetTxPower" + isPidPointCoefOk.ToString());
                              
                            isPidPIDCoefOk = writeCurrentChannelPID(dut);
                            logoStr += logger.AdapterLogString(3, "Write PID" + isPidPIDCoefOk.ToString());
                            logger.FlushLogBuffer();

                            CloseandOpenAPC(Convert.ToByte(APCMODE.IBIASONandIMODOFF));

                            tempPS.OutPutSwitch(false, 1);
                            tempPS.OutPutSwitch(true, 1);
                            dut.FullFunctionEnable();
                        }
                        if (tempratureADCArray.Count >= 2)
                        {
                           
                            {
                                CloseandOpenAPC(Convert.ToByte(APCMODE.IBIASONandIMODOFF));
                            }
                            
                        }
                        ArrayList tempProcessDate = new ArrayList();
                        UInt32 terminalValue = 0;
                        UInt32 tempTxPowerAdc = 0;
                        logoStr += logger.AdapterLogString(0, "Step6...StartAdjustEr");

                        //targetER=ScopeObject.GetEratio();
                        //if (targetER >= adjustEYEStruce.AdjustErLL && targetER <= adjustEYEStruce.AdjustErUL)
                        //{
                        //    imodDacTarget = adjustEYEStruce.ImodDACStart;
                        //    isErAdjustOk = true;
                        //    logoStr += logger.AdapterLogString(0, "targetImodDAC="+imodDacTarget);
                        //    logoStr += logger.AdapterLogString(0, "targetER=" + targetER);


                        //}
                        //else
                        //{


                            isErAdjustOk = OnesectionMethod(adjustEYEStruce.ImodDACStart, adjustEYEStruce.ImodStep, adjustEYEStruce.TxErTarget, adjustEYEStruce.AdjustErUL, adjustEYEStruce.AdjustErLL, adjustEYEStruce.ModDacMax, adjustEYEStruce.ModDacMin, ScopeObject, dut, 1, out txPowerADC, out erortxPowerValueArray, out tempTxPowerAdc, out tempProcessDate, out terminalValue, out targetER);
                            if (!isErAdjustOk)
                            {
                                isErAdjustOk = (targetER >= adjustEYEStruce.TxErLL && targetER <= adjustEYEStruce.TxErUL) ? true : false;
                            }

                            imodDacTarget = terminalValue;
                            procErData = erortxPowerValueArray;
                            procImodDACData = tempProcessDate;

                       // }
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
                        #endregion                       
                        logoStr += logger.AdapterLogString(3, "isErAdjustOk=" + isErAdjustOk.ToString());
                        logger.FlushLogBuffer();
#endregion
                    }
                    //else if (GlobalParameters.APCType == Convert.ToByte(apctype.OpenLoop) || GlobalParameters.APCType == Convert.ToByte(apctype.CloseLoop))
                    else  // 当产品没有 DC 没有 APC 的时候 eg: Rainbow
                   
                    {
                        if (allChannelFixedIMod.ContainsKey(GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()))
                        {
                            dut.WriteModDac(Convert.ToUInt32(allChannelFixedIMod[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()]));
                        } 
                        else
                        {
                            dut.WriteModDac(0);
                        }
                      
                        if (allChannelFixedIBias.ContainsKey(GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()))
                        {
                            dut.WriteBiasDac(Convert.ToUInt32(allChannelFixedIBias[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()]));
                        }
                        else
                        {
                            dut.WriteBiasDac(0);
                        }

                        adjustEYEStruce.ImodDACStart = Convert.ToUInt32(allChannelFixedIMod[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()]);
                        adjustEYEStruce.IbiasDACStart = Convert.ToUInt32(allChannelFixedIBias[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()]);
                       




                        ScopeObject.SetScaleOffset(adjustEYEStruce.TxLOPTarget, 1);
                        logoStr += logger.AdapterLogString(0, "Step4...Start Adjust TxPower");

                       

                        targetLOP = ReadAp(out txpowerAdcTarget);

                        ibiasDacTarget = adjustEYEStruce.IbiasDACStart;
                    //  ibiasDacTarget
                        if (targetLOP >= adjustEYEStruce.AdjustTxLOPLL && targetLOP <= adjustEYEStruce.AdjustTxLOPUL)
                        { 
                           // ibiasDacTarget = Convert.ToUInt32(allChannelFixedIBias[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()]);
                            isTxPowerAdjustOk = true;
                        }
                        else
                        {
                            isTxPowerAdjustOk = false;
                        }

                        targetER = ReadER();
                       // procErData.Add(targetER);
                       // imodDacTarget = Convert.ToUInt32(allChannelFixedIMod[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()]);
                        imodDacTarget = adjustEYEStruce.ImodDACStart;
                        procTxPowerData.Add(imodDacTarget);
                        procErData.Add(targetER);
                        if (targetER>adjustEYEStruce.AdjustErLL&&targetER<adjustEYEStruce.AdjustErUL)
                        { //  imodDacTarget = Convert.ToUInt32(allChannelFixedIMod[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()]);
                            isErAdjustOk = true;
                          
                        }
                        else
                        {
                            isErAdjustOk = false;
                            
                        }

                        logoStr += logger.AdapterLogString(0, "Power=" + targetLOP);
                        logoStr += logger.AdapterLogString(0, "ER=" + targetER);

                        bool adjustEROK = false;
                        bool adjustTxPowerOK = false;

                        if (!isErAdjustOk || !isTxPowerAdjustOk)
                        {
                            #region  如果初始值不满足规格, 需要调整

                            //                       private UInt32 ibiasDacTarget = 0;
                            //private UInt32 imodDacTarget = 0;
                            //  OnesectionMethodERandPower(adjustEYEStruce.IbiasDACStart, adjustEYEStruce.ImodDACStart, adjustEYEStruce.IbiasStep, adjustEYEStruce.ImodStep, adjustEYEStruce.TxLOPTarget, adjustEYEStruce.AdjustTxLOPUL, adjustEYEStruce.AdjustTxLOPLL, adjustEYEStruce.IbiasDACMax, adjustEYEStruce.IbiasDACMin, adjustEYEStruce.TxErTarget, adjustEYEStruce.AdjustErUL, adjustEYEStruce.AdjustErLL, ScopeObject, dut, adjustEYEStruce.ModDacMax, adjustEYEStruce.ModDacMin, out ibiasDacTarget, out imodDacTarget, out targetER, out txpowerAdcTarget, out targetLOP, out adjustEROK, out adjustTxPowerOK, out procTxPowerADCData, out procTxPowerData, out procErData, out procIbiasDACData, out procImodDACData);
                            switch (adjustEYEStruce.DCCouple_AdjustMehtod)
                            {
                                case 1:
                                    if (!OnesectionMethodERandPower(adjustEYEStruce.IbiasDACStart, adjustEYEStruce.ImodDACStart, adjustEYEStruce.IbiasStep, adjustEYEStruce.ImodStep, adjustEYEStruce.TxLOPTarget, adjustEYEStruce.AdjustTxLOPUL, adjustEYEStruce.AdjustTxLOPLL, adjustEYEStruce.IbiasDACMax, adjustEYEStruce.IbiasDACMin, adjustEYEStruce.TxErTarget, adjustEYEStruce.AdjustErUL, adjustEYEStruce.AdjustErLL, ScopeObject, dut, adjustEYEStruce.ModDacMax, adjustEYEStruce.ModDacMin, out ibiasDacTarget, out imodDacTarget, out targetER, out txpowerAdcTarget, out targetLOP, out adjustEROK, out adjustTxPowerOK, out procTxPowerADCData, out procTxPowerData, out procErData, out procIbiasDACData, out procImodDACData))
                                    {
                                        targetLOP = ScopeObject.GetAveragePowerdbm();
                                        targetER = ScopeObject.GetEratio();
                                        isErAdjustOk = false;
                                        isTxPowerAdjustOk = false;
                                    }
                                    break;
                                case 2:
                                    if (!OnesectionMethodERandPower_Method2(adjustEYEStruce.IbiasDACStart, adjustEYEStruce.ImodDACStart, adjustEYEStruce.IbiasStep, adjustEYEStruce.ImodStep, adjustEYEStruce.TxLOPTarget, adjustEYEStruce.AdjustTxLOPUL, adjustEYEStruce.AdjustTxLOPLL, adjustEYEStruce.IbiasDACMax, adjustEYEStruce.IbiasDACMin, adjustEYEStruce.TxErTarget, adjustEYEStruce.AdjustErUL, adjustEYEStruce.AdjustErLL, ScopeObject, dut, adjustEYEStruce.ModDacMax, adjustEYEStruce.ModDacMin, out ibiasDacTarget, out imodDacTarget, out targetER, out txpowerAdcTarget, out targetLOP, out adjustEROK, out adjustTxPowerOK, out procTxPowerADCData, out procTxPowerData, out procErData, out procIbiasDACData, out procImodDACData))
                                    {
                                        isErAdjustOk = false;
                                        isTxPowerAdjustOk = false;
                                    }
                                    break;
                                case 3:
                                    if (!OnesectionMethodERandPower_Method3(adjustEYEStruce.IbiasDACStart, adjustEYEStruce.ImodDACStart, adjustEYEStruce.IbiasStep, adjustEYEStruce.ImodStep, adjustEYEStruce.TxLOPTarget, adjustEYEStruce.AdjustTxLOPUL, adjustEYEStruce.AdjustTxLOPLL, adjustEYEStruce.IbiasDACMax, adjustEYEStruce.IbiasDACMin, adjustEYEStruce.TxErTarget, adjustEYEStruce.AdjustErUL, adjustEYEStruce.AdjustErLL, ScopeObject, dut, adjustEYEStruce.ModDacMax, adjustEYEStruce.ModDacMin, out ibiasDacTarget, out imodDacTarget, out targetER, out txpowerAdcTarget, out targetLOP, out adjustEROK, out adjustTxPowerOK, out procTxPowerADCData, out procTxPowerData, out procErData, out procIbiasDACData, out procImodDACData))
                                    {
                                        isErAdjustOk = false;
                                        isTxPowerAdjustOk = false;
                                    }
                                    break;
                                case 6:
                                    logoStr += logger.AdapterLogString(0, "Enter Method 6------------------->>>>>>");

                                    if (!OnesectionMethodERandPower_Method6(out isTxPowerAdjustOk, out isErAdjustOk))
                                    {
                                        isErAdjustOk = false;
                                        isTxPowerAdjustOk = false;
                                    }

                                    break;
                                case 7:
                                    if (!OnesectionMethodERandPower_Method7(out adjustEROK, out adjustTxPowerOK))
                                    {
                                        isErAdjustOk = false;
                                        isTxPowerAdjustOk = false;
                                    }
                                    break;
                                default:// 0  不调

                                    break;
                            }


                            if (!isErAdjustOk)
                            {
                                adjustEROK = (targetER >= adjustEYEStruce.TxErLL && targetER <= adjustEYEStruce.TxErUL) ? true : false;
                                isErAdjustOk = adjustEROK;
                            }
                            else
                            {
                                isErAdjustOk = true;
                            }
                            if (!isTxPowerAdjustOk)
                            {
                                adjustTxPowerOK = (targetLOP >= adjustEYEStruce.TxLOPLL && targetLOP <= adjustEYEStruce.TxLOPUL) ? true : false;
                                isTxPowerAdjustOk = adjustTxPowerOK;
                            }
                            else
                            {
                                isTxPowerAdjustOk = true;
                            }

                            #endregion
                        }
                        else
                        {
                            dut.StoreBiasDac(ibiasDacTarget);//store initial target 
                            dut.StoreModDac(imodDacTarget);
                        }
                        logoStr += logger.AdapterLogString(1, isTxPowerAdjustOk.ToString());
                        logoStr += logger.AdapterLogString(1, isErAdjustOk.ToString());
                        if (!isErAdjustOk || !isTxPowerAdjustOk)
                        {
                            return false;
                        
                        }
                    }
                }

                return true;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        protected bool OnesectionMethodERandPower(UInt32 startValueIbias, UInt32 startValueMod, byte stepIbias, byte stepImod, double targetValueIbias, double targetTxPowerUL, double targetTxPowerLL, UInt32 uperLimitIbias, UInt32 lowLimitIbias, double targetValueIMod, double targetERUL, double targetERLL, Scope scope, DUT dut, UInt32 uperLimitIMod, UInt32 lowLimitIMod, out UInt32 ibiasDacTarget, out UInt32 imodDacTarget, out double targetERValue, out UInt32 TxPowerAdc, out double targetLOPValue, out bool isERok, out bool isLopok, out ArrayList procTxpoweradcArray, out ArrayList procTxpowerArray, out ArrayList procErArray, out ArrayList procIbiasDacArray, out ArrayList procImodDacArray)//ibias=0;modulation=1
        {
            procTxpoweradcArray = new ArrayList();
            procTxpowerArray = new ArrayList();
            procErArray = new ArrayList();
            procIbiasDacArray = new ArrayList();
            procImodDacArray = new ArrayList();
            procTxpoweradcArray.Clear();
            procTxpowerArray.Clear();
            procErArray.Clear();
            procIbiasDacArray.Clear();
            procImodDacArray.Clear();
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
            UInt16 Temp;           
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
                    procIbiasDacArray.Add(startValueIbias);
                    procImodDACData.Add(startValueMod);
                    scope.SetScaleOffset(adjustEYEStruce.TxLOPTarget, 1);
                    scope.ClearDisplay();
                    scope.DisplayPowerdbm();
                    currentLOPValue = scope.GetAveragePowerdbm();
                    targetLOPValue = currentLOPValue;//Leo 4/14
                    targetERValue = currentERValue;
                    scope.DisplayER();
                    currentERValue = scope.GetEratio();
                    targetERValue = currentERValue;
                    procTxpowerArray.Add(currentLOPValue);
                    procErArray.Add(currentERValue);
                    dut.ReadTxpADC(out Temp);
                    procTxpoweradcArray.Add(Temp);
                    if ((startValueIbias == uperLimitIbias) && (currentLOPValue < ((targetTxPowerLL))) || (startValueIbias == lowLimitIbias) && (currentLOPValue > ((targetTxPowerUL))))
                    {

                        if (currentLOPValue > ((targetTxPowerUL)))
                        {
                            logoStr += logger.AdapterLogString(4, "DataBase input Parameters lowLimit is too large!");
                        }
                        else if (currentLOPValue < ((targetTxPowerLL)))
                        {
                            logoStr += logger.AdapterLogString(4, "DataBase input Parameters uperLimit is too small!");
                        }
                        ibiasDacTarget = startValueIbias;
                        imodDacTarget = startValueMod;
                       
                        logger.FlushLogBuffer();
                        return false;
                    }
                    if ((startValueMod == uperLimitIMod) && (currentERValue < ((targetERLL))) || (startValueMod == lowLimitIMod) && (currentERValue > ((targetERUL))))
                    {
                        if (currentERValue > ((targetERUL)))
                        {
                            logoStr += logger.AdapterLogString(4, "DataBase input Parameters lowLimit is too large!");
                        }
                        else if (currentERValue < ((targetERLL)))
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
                        if (currentLOPValue < ((targetTxPowerLL)))
                        {
                            int tempValue = (int)(startValueIbias + (UInt32)Math.Pow(2, totalExponentiationLopCount - backUpCountLop) >= uperLimitIbias ? uperLimitIbias : startValueIbias + (UInt32)Math.Pow(2, totalExponentiationLopCount - backUpCountLop));
                            
                            startValueIbias = (UInt32)tempValue;
                        }
                        if (currentERValue < ((targetERLL)))
                        {
                            int tempValue = (int)(startValueMod + (UInt32)Math.Pow(2, totalExponentiationERCount - backUpCountEr) >= uperLimitIMod ? uperLimitIMod : startValueMod + (UInt32)Math.Pow(2, totalExponentiationERCount - backUpCountEr));
                            startValueMod = (UInt32)tempValue;
                        }
                        if (currentLOPValue > ((targetTxPowerUL)))
                        {
                            do
                            {
                                int tempValue = (int)((int)(startValueIbias - (UInt32)Math.Pow(2, totalExponentiationLopCount)) >= lowLimitIbias ? (startValueIbias - (UInt32)Math.Pow(2, totalExponentiationLopCount)) : lowLimitIbias);
                                startValueIbias = (UInt32)tempValue;
                                {
                                    dut.WriteBiasDac(startValueIbias);
                                    procIbiasDacArray.Add(startValueIbias);                                    
                                    scope.ClearDisplay();
                                    scope.SetScaleOffset(adjustEYEStruce.TxLOPTarget, 1);
                                    scope.DisplayPowerdbm();
                                    currentLOPValue =  scope.GetAveragePowerdbm();
                                    targetLOPValue = currentLOPValue;//Leo 4/14
                                   
                                    procTxpowerArray.Add(currentLOPValue);                                    
                                    dut.ReadTxpADC(out Temp);
                                    procTxpoweradcArray.Add(Temp);
                                }

                                if ((startValueIbias == lowLimitIbias) && (currentLOPValue > ((targetTxPowerUL))))
                                {
                                    logoStr += logger.AdapterLogString(4, "DataBase input Parameters lowLimit is too large!");
                                    logger.FlushLogBuffer();
                                    ibiasDacTarget = startValueIbias;
                                    imodDacTarget = startValueMod;
                                    return false;
                                }
                            } while ((currentLOPValue > ((targetTxPowerUL))));
                            currentERValue = scope.GetEratio();
                         
                            targetERValue = currentERValue;
                        }
                        if ((currentERValue > ((targetERUL))))
                        {
                            do
                            {

                                int tempValue = (int)((int)(startValueMod - (UInt32)Math.Pow(2, totalExponentiationERCount)) >= lowLimitIMod ? (startValueMod - (UInt32)Math.Pow(2, totalExponentiationERCount)) : lowLimitIMod);
                                startValueMod = (UInt32)tempValue;
                                {
                                    dut.WriteModDac(startValueMod);                                   
                                    procImodDACData.Add(startValueMod);
                                    scope.ClearDisplay();
                                    scope.SetScaleOffset(adjustEYEStruce.TxLOPTarget, 1);
                                    scope.DisplayER();

                                    currentERValue = scope.GetEratio();
                                 
                                    targetERValue = currentERValue;

                                    procErArray.Add(currentERValue);

                                }
                                if ((startValueMod == lowLimitIMod) && (currentERValue > ((targetERUL))))
                                {
                                    logoStr += logger.AdapterLogString(4, "DataBase input Parameters lowLimit is too large!");
                                    ibiasDacTarget = startValueIbias;
                                    imodDacTarget = startValueMod;
                                    logger.FlushLogBuffer();
                                    return false;
                                }
                            } while ((currentERValue > ((targetERUL))));
                            currentLOPValue = scope.GetAveragePowerdbm();
                        }
                    }
                    else
                    {
                        if ((currentLOPValue < (targetTxPowerLL)))
                        {
                            int tempValue = (int)(startValueIbias + (UInt32)Math.Pow(2, totalExponentiationLopCount - backUpCountLop) >= uperLimitIbias ? uperLimitIbias : startValueIbias + (UInt32)Math.Pow(2, totalExponentiationLopCount - backUpCountLop));
                            startValueIbias = (UInt32)tempValue;
                        }
                        else if ((currentLOPValue > (targetTxPowerUL)))
                        {
                            int tempValue = (int)((int)(startValueIbias - (UInt32)Math.Pow(2, totalExponentiationLopCount - backUpCountLop)) >= lowLimitIbias ? (startValueIbias - (UInt32)Math.Pow(2, totalExponentiationLopCount - backUpCountLop)) : lowLimitIbias);
                            startValueIbias = (UInt32)tempValue;
                           // backUpCountLop++;
                            byte tempValue1 = (byte)((backUpCountLop) >= (byte)(totalExponentiationLopCount - 3) ? (byte)(totalExponentiationLopCount - 3) : backUpCountLop);
                            backUpCountLop = tempValue1;
                            if (backUpCountLop < (byte)(totalExponentiationLopCount - 3))
                            {
                                int tempValue2 = (int)(startValueIbias + (UInt32)Math.Pow(2, totalExponentiationLopCount - backUpCountLop) >= uperLimitIbias ? uperLimitIbias : startValueIbias + (UInt32)Math.Pow(2, totalExponentiationLopCount - backUpCountLop));
                                startValueIbias = (UInt32)tempValue2;
                            }

                        }
                        if ((currentERValue < (targetERLL)))
                        {
                            int tempValue = (int)(startValueMod + (UInt32)Math.Pow(2, totalExponentiationERCount - backUpCountEr) >= uperLimitIMod ? uperLimitIMod : startValueMod + (UInt32)Math.Pow(2, totalExponentiationERCount - backUpCountEr));
                            startValueMod = (UInt32)tempValue;

                        }
                        else if ((currentERValue > (targetERUL)))
                        {
                            int tempValue = (int)((int)(startValueMod - (UInt32)Math.Pow(2, totalExponentiationERCount - backUpCountEr)) >= lowLimitIMod ? (startValueMod - (UInt32)Math.Pow(2, totalExponentiationERCount - backUpCountEr)) : lowLimitIMod);
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
                    if ((currentLOPValue < (targetTxPowerLL) || currentLOPValue > (targetTxPowerUL)) || (currentERValue < (targetERLL) || currentERValue > (targetERUL)))
                    {
                        adjustCount++;
                    }
                }

            } while (adjustCount <= 100 && (currentLOPValue < (targetTxPowerLL) || currentLOPValue > (targetTxPowerUL) || currentERValue < (targetERLL) || currentERValue > (targetERUL)));
            targetLOPValue = currentLOPValue;
            targetERValue = currentERValue;        
            dut.ReadTxpADC(out Temp);
            TxPowerAdc = Convert.ToUInt32(Temp);            
            procTxpoweradcArray.Add(Temp);

            if (startValueIbias > uperLimitIbias || startValueIbias < lowLimitIbias)
            {
                if (startValueIbias > uperLimitIbias)
                {
                    startValueIbias = uperLimitIbias;
                    {
                        dut.WriteBiasDac(startValueIbias);
                        procIbiasDacArray.Add(startValueIbias);                        
                        scope.ClearDisplay();
                        scope.SetScaleOffset(adjustEYEStruce.TxLOPTarget, 1);
                        scope.DisplayPowerdbm();
                        ibiasDacTarget = startValueIbias;
                        currentLOPValue = scope.GetAveragePowerdbm();
                        procTxpowerArray.Add(currentLOPValue);
                        dut.ReadTxpADC(out Temp);
                        procTxpoweradcArray.Add(Temp);
                    }
                }
                else if (startValueIbias < lowLimitIbias)
                {
                    startValueIbias = lowLimitIbias;
                    {
                        dut.WriteBiasDac(startValueIbias);
                        procIbiasDacArray.Add(startValueIbias);                       
                        scope.ClearDisplay();
                        scope.SetScaleOffset(adjustEYEStruce.TxLOPTarget, 1);
                        scope.DisplayPowerdbm();
                        ibiasDacTarget = startValueIbias;
                        currentLOPValue = scope.GetAveragePowerdbm();
                        procTxpowerArray.Add(currentLOPValue);
                        dut.ReadTxpADC(out Temp);
                        procTxpoweradcArray.Add(Temp);
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
                        procImodDACData.Add(startValueMod);
                        imodDacTarget = startValueMod;
                        scope.ClearDisplay();
                        scope.SetScaleOffset(adjustEYEStruce.TxLOPTarget, 1);
                        scope.DisplayER();

                        currentERValue = scope.GetEratio();                        
                        procErArray.Add(currentERValue);
                        targetERValue = currentERValue;
                    }
                }
                else if (startValueMod < lowLimitIMod)
                {
                    startValueMod = lowLimitIMod;

                    {
                        dut.WriteModDac(startValueMod);
                        imodDacTarget = startValueMod;                        
                        procImodDACData.Add(startValueMod);
                        scope.ClearDisplay();
                        scope.SetScaleOffset(adjustEYEStruce.TxLOPTarget, 1);
                        scope.DisplayER();
                        currentERValue = scope.GetEratio();                       
                        procErArray.Add(currentERValue);
                        targetERValue = currentERValue;
                    }
                }
            }
            if (currentLOPValue >= (targetTxPowerLL) && currentLOPValue <= (targetTxPowerUL))
            {
                ibiasDacTarget = startValueIbias;

                dut.StoreBiasDac(startValueIbias);
               
                isLopok = true;
            }
            else
            {
                dut.StoreBiasDac(startValueIbias);
                ibiasDacTarget = startValueIbias;
                isLopok = false;
            }
            if (currentERValue >= (targetERLL) && currentERValue <= (targetERUL))
            {
                imodDacTarget = startValueMod;
                dut.StoreModDac(startValueMod);
                isERok = true;
            }
            else
            {
                imodDacTarget = startValueMod;
                dut.StoreModDac(startValueMod);
                isERok = false;
            }
            if (isERok && isLopok)
            {
                ibiasDacTarget = startValueIbias;
                imodDacTarget = startValueMod;
                dut.ReadTxpADC(out Temp);
                TxPowerAdc = Convert.ToUInt32(Temp);                
                procTxpoweradcArray.Add(Temp);
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ItemType">Bias or Imod  0=Ibias 1=Mod</param>
        /// <param name="message">待封装的信息</param>
        /// <param name="className">调用者的类名</param>
        /// <returns>计算步长</returns>
        ///
        private bool CalculateStep(byte ItemType, out byte StepValue)
        {
            StepValue = 2;
            double K1=0,K2=0,Diff;
            try
            {
                switch (ItemType)
                {
                    case 0://iBias
                        if (GlobalParameters.IbiasFormula.Contains("IMODDAC"))
                        {
                            K1 = algorithm.AnalyticalExpression(GlobalParameters.IbiasFormula, adjustEYEStruce.IbiasDACStart, 0, Convert.ToUInt32(allChannelFixedIMod[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()]));
                            K2 = algorithm.AnalyticalExpression(GlobalParameters.IbiasFormula, (adjustEYEStruce.IbiasDACStart + adjustEYEStruce.IbiasStep), 0, Convert.ToUInt32(allChannelFixedIMod[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()]));
                        }
                        else
                        {
                            K1 = algorithm.AnalyticalExpression(GlobalParameters.IbiasFormula, adjustEYEStruce.IbiasDACStart);
                            K2 = algorithm.AnalyticalExpression(GlobalParameters.IbiasFormula, (adjustEYEStruce.IbiasDACStart + adjustEYEStruce.IbiasStep));
                        }
                        break;
                    case 1://Imod
                        if (GlobalParameters.IbiasFormula.Contains("IBIASDAC"))
                        {
                            K1 = algorithm.AnalyticalExpression(GlobalParameters.ImodFormula, adjustEYEStruce.ImodDACStart, Convert.ToUInt32(allChannelFixedIBias[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()]));
                            K2 = algorithm.AnalyticalExpression(GlobalParameters.ImodFormula, (adjustEYEStruce.ImodDACStart + adjustEYEStruce.ImodStep), Convert.ToUInt32(allChannelFixedIBias[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()]));
                        }
                        else
                        {
                            K1 = algorithm.AnalyticalExpression(GlobalParameters.ImodFormula, adjustEYEStruce.ImodDACStart);
                            K2 = algorithm.AnalyticalExpression(GlobalParameters.ImodFormula, (adjustEYEStruce.ImodDACStart + adjustEYEStruce.ImodStep));
                        }
                        break;
                    default:
                        break;
                }

                if (Math.Abs(K1 - K2) > 32)
                {
                    Diff = 32;
                }
                else
                {
                    Diff = Math.Abs(K1 - K2);
                }
                StepValue =Convert.ToByte(Diff);
                return true;
            }
            catch
            {
                StepValue = 2;
                logoStr += logger.AdapterLogString(3, "CalculateStep Error");
                return false;
            }

            //return (byte)Diff;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ItemType">Bias or Imod  0=Ibias 1=Mod</param>
        /// <param name="Limmit">0=Min 2=Max 1=Start </param>
        /// <param name="X">输入参数</param>
        /// <returns>计算结果</returns>
        private bool CalculateRegist(byte ItemType, byte Limmit,double X, out uint Result)
        {
            Result = 0;
            double CalculateValue = 0;
            try
            {
                switch (ItemType)
                {
                    case 0://iBias
                        if (GlobalParameters.IbiasFormula.Contains("IMODDAC"))
                        {
                            CalculateValue = algorithm.AnalyticalExpression(GlobalParameters.IbiasFormula, X, 0, Convert.ToUInt32(allChannelFixedIMod[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()]));
                        }
                        else
                        {
                           CalculateValue = algorithm.AnalyticalExpression(GlobalParameters.IbiasFormula, X);
                        }

                        if (CalculateValue > GlobalParameters.IbiasRegistDacValueMax || CalculateValue<0)
                        {

                            string Str = "";

                            switch (ItemType)
                            {
                                case 0://iBias
                                    Str += "Ibias ";
                                    break;
                                case 1://Imod
                                    Str += "Imod ";
                                    break;
                                default:
                                    break;
                            }

                            switch (Limmit)
                            {
                                case 0://Min
                                    Str += "Min  ";
                                    break;
                                case 1://Start
                                    Str += "Start ";
                                    break;
                                case 2://Max
                                    Str += "Max ";
                                    break;
                                default:
                                    break;
                            }

                            Result = 0;
                            logoStr += logger.AdapterLogString(3, Str + "Calculate Error");

                            return false;
                        }
                        break;
                    case 1://Imod
                        if (GlobalParameters.IbiasFormula.Contains("IBIASDAC"))
                        {
                            CalculateValue = algorithm.AnalyticalExpression(GlobalParameters.ImodFormula, X, Convert.ToUInt32(allChannelFixedIBias[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()]));
                        }
                        else
                        {
                           CalculateValue = algorithm.AnalyticalExpression(GlobalParameters.ImodFormula, X);
                        }

                        if (CalculateValue > GlobalParameters.ImodRegistDacValueMax || CalculateValue < 0)
                        {

                            string Str = "";

                            switch (ItemType)
                            {
                                case 0://iBias
                                    Str += "Ibias ";
                                    break;
                                case 1://Imod
                                    Str += "Imod ";
                                    break;
                                default:
                                    break;
                            }

                            switch (Limmit)
                            {
                                case 0://Min
                                    Str += "Min  ";
                                    break;
                                case 1://Start
                                    Str += "Start ";
                                    break;
                                case 2://Max
                                    Str += "Max ";
                                    break;
                                default:
                                    break;
                            }

                            Result = 0;
                            logoStr += logger.AdapterLogString(3, Str + "Calculate Error");

                            return false;
                        }
                        break;
                    default:
                        break;
                }

                Result =Convert.ToUInt32( CalculateValue);
                return true;
            }
            catch
            {
                string Str = "";

                switch (ItemType)
                {
                    case 0://iBias
                        Str += "Ibias ";
                        break;
                    case 1://Imod
                        Str += "Imod ";
                        break;
                    default:
                        break;
                }

                switch (Limmit)
                {
                    case 0://Min
                        Str += "Min  ";
                        break;
                    case 1://Start
                        Str += "Start ";
                        break;
                    case 2://Max
                        Str += "Max ";
                        break;
                    default:
                        break;
                }

                Result = 0;
                logoStr += logger.AdapterLogString(3, Str+"Calculate Error");
                return false;
            }

            //return (byte)Diff;
        }


        protected bool CalculateIbaisandImodDacfromExprssion()//已经不在使用 ,预留
        {
            try
            {
                // adjustEYEStruce.IbiasDACStart = Convert.ToUInt32(allChannelFixedIBias[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()]);
               // adjustEYEStruce.IbiasDACStart
                //IModDacStart = Convert.ToDouble(allChannelFixedIMod[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()]);

                //adjustEYEStruce.IbiasDACStart = Convert.ToUInt32(allChannelFixedIBias[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()]);
                //adjustEYEStruce.ImodDACStart = Convert.ToUInt32(allChannelFixedIMod[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()]);

                //if (GlobalParameters.IbiasFormula != "" && GlobalParameters.IbiasFormula != null)
                //{
                //    if (!CalculateStep(0, out adjustEYEStruce.IbiasStep)) return false;

                //    if (!CalculateRegist(0, 0, IbiasMin, out adjustEYEStruce.IbiasDACMin)) return false;

                //    if (!CalculateRegist(0, 0, IbiasDacStart, out adjustEYEStruce.IbiasDACStart)) return false;

                //    if (!CalculateRegist(0, 0, IbiasMax, out adjustEYEStruce.IbiasDACMax)) return false;
                //}
                //else
                //{
                //    adjustEYEStruce.IbiasStep = Convert.ToByte(IbiasDacStep);
                //    adjustEYEStruce.IbiasDACMin = Convert.ToUInt32(IbiasMin);
                //    adjustEYEStruce.IbiasDACMax = Convert.ToUInt32(IbiasMax);
                //    adjustEYEStruce.IbiasDACStart = Convert.ToUInt32(IbiasDacStart);
                //    //  adjustEYEStruce.IbiasDACStart = Convert.ToUInt32(allChannelFixedIBias[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()]);
                //}

                // adjustEYEStruce.IbiasDACStart = Convert.ToUInt32(allChannelFixedIBias[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()]);

                //if (GlobalParameters.ImodFormula != "" && GlobalParameters.ImodFormula != null)
                //{
                //    if (!CalculateStep(0, out adjustEYEStruce.ImodStep)) return false;

                //    if (!CalculateRegist(0, 0, IModMin, out adjustEYEStruce.ModDacMin)) return false;
                //    if (!CalculateRegist(0, 0, IModDacStart, out adjustEYEStruce.ImodDACStart)) return false;
                //    if (!CalculateRegist(0, 0, IModMax, out adjustEYEStruce.ModDacMax)) return false;
                //}
                //else
                //{
                //    adjustEYEStruce.ImodStep = Convert.ToByte(IModDacStep);
                //    adjustEYEStruce.ModDacMin = Convert.ToUInt32(IModMin);
                //    adjustEYEStruce.ModDacMax = Convert.ToUInt32(IModMax);
                //    adjustEYEStruce.ImodDACStart = Convert.ToUInt32(IModDacStart);
                //}

                // adjustEYEStruce.ImodDACStart = Convert.ToUInt32(allChannelFixedIMod[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()]);

                return true;

            }
            catch (System.Exception ex)
            {
                return false;
                // throw ex;
            }

        }
        private void OutPutandFlushLog()
        {
            try
            {
                AnalysisOutputParameters(outputParameters);
                AnalysisOutputProcData(procData);
                logger.FlushLogBuffer();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
#region   新的DC 眼图调整

        #region   DC-LOOP-AdjustAP_ER,Method2,先调试 Power 进入规格,再调节ER,调节ER的过程固定Power

        protected bool OnesectionMethodERandPower_Method2(UInt32 startValueIbias, UInt32 startValueMod, byte stepIbias, byte stepImod, double targetValueIbias, double targetTxPowerUL, double targetTxPowerLL, UInt32 uperLimitIbias, UInt32 lowLimitIbias, double targetValueIMod, double targetERUL, double targetERLL, Scope scope, DUT dut, UInt32 uperLimitIMod, UInt32 lowLimitIMod, out UInt32 ibiasDacTarget, out UInt32 imodDacTarget, out double targetERValue, out UInt32 TxPowerAdc, out double targetLOPValue, out bool isERok, out bool isLopok, out ArrayList procTxpoweradcArray, out ArrayList procTxpowerArray, out ArrayList procErArray, out ArrayList procIbiasDacArray, out ArrayList procImodDacArray)//ibias=0;modulation=1
        {
            procTxpoweradcArray = new ArrayList();
            procTxpowerArray = new ArrayList();
            procErArray = new ArrayList();
            procIbiasDacArray = new ArrayList();
            procImodDacArray = new ArrayList();
            procTxpoweradcArray.Clear();
            procTxpowerArray.Clear();
            procErArray.Clear();
            procIbiasDacArray.Clear();
            procImodDacArray.Clear();
            isERok = false;
            isLopok = false;
            //byte adjustCount = 0;
            //byte backUpCountLop = 0;
            //byte backUpCountEr = 0;
            byte totalExponentiationLopCount = Convert.ToByte(Math.Floor((Math.Log(Convert.ToDouble(stepIbias), 2))));
            byte totalExponentiationERCount = Convert.ToByte(Math.Floor((Math.Log(Convert.ToDouble(stepImod), 2))));
            double currentLOPValue = -1;
            double currentERValue = -1;
            targetLOPValue = -1;
            targetERValue = -1;
            TxPowerAdc = 0;
            UInt16 Temp;
            byte[] writeData = new byte[1];



            dut.WriteBiasDac(startValueIbias);
            dut.WriteModDac(startValueMod);
            procIbiasDacArray.Add(startValueIbias);
            procImodDACData.Add(startValueMod);
            scope.SetScaleOffset(adjustEYEStruce.TxLOPTarget, 1);
            scope.ClearDisplay();
            scope.DisplayPowerdbm();
            currentLOPValue = scope.GetAveragePowerdbm();
            scope.DisplayER();
            currentERValue = scope.GetEratio();
            procTxpowerArray.Add(currentLOPValue);
            procErArray.Add(currentERValue);
            dut.ReadTxpADC(out Temp);
            procTxpoweradcArray.Add(Temp);

            #region  AdjustTxPower
            int i = 0;

            do
            {


                dut.WriteBiasDac(startValueIbias);
                procIbiasDacArray.Add(startValueIbias);
                scope.ClearDisplay();
                scope.SetScaleOffset(adjustEYEStruce.TxLOPTarget, 1);
                scope.DisplayPowerdbm();
                currentLOPValue = scope.GetAveragePowerdbm();
                targetLOPValue = currentLOPValue;
                procTxpowerArray.Add(currentLOPValue);
                dut.ReadTxpADC(out Temp);
                procTxpoweradcArray.Add(Temp);

                if (currentLOPValue > targetTxPowerUL)
                {
                    startValueIbias -= stepIbias;
                }
                if (currentLOPValue < targetTxPowerLL)
                {
                    startValueIbias += stepIbias;
                }


                if ((startValueIbias >= uperLimitIbias) && (currentLOPValue < targetTxPowerLL))
                {
                    logoStr += logger.AdapterLogString(4, "DataBase input Parameters HighLimit is too Small!");
                    logger.FlushLogBuffer();
                    ibiasDacTarget = startValueIbias;
                    imodDacTarget = startValueMod;
                    goto Error;
                }
                if ((startValueIbias <= lowLimitIbias) && (currentLOPValue > targetTxPowerUL))
                {
                    logoStr += logger.AdapterLogString(4, "DataBase input Parameters HighLimit is too large!");
                    logger.FlushLogBuffer();
                    ibiasDacTarget = startValueIbias;
                    imodDacTarget = startValueMod;
                    goto Error;
                }

                i++;

            } while ((currentLOPValue > targetTxPowerUL || currentLOPValue < targetTxPowerLL) && i < 20);

            if (currentLOPValue <= targetTxPowerUL && currentLOPValue >= targetTxPowerLL)
            {
                isLopok = true;
            }

            currentERValue = scope.GetEratio();
            ibiasDacTarget = startValueIbias;
            //out UInt32 ibiasDacTarget, out UInt32 imodDacTarget
            #endregion

            #region  AdjustTxER
            i = 0;

            do
            {

                dut.WriteModDac(startValueMod);

                procImodDACData.Add(startValueMod);
                imodDacTarget = startValueMod;

                //if (!FixTxPower(ibiasDacTarget, uperLimitIbias, lowLimitIbias, stepIbias, currentLOPValue, out ibiasDacTarget))
                //{
                //    return false;
                //}
                double TempPower;
                double Step = stepIbias / 2;            //Math.Ceiling(stepIbias/2);
                if (!FixTxPower(ibiasDacTarget, (byte)(Math.Ceiling(Step)), currentLOPValue, out ibiasDacTarget, out TempPower))
                {
                    if (Math.Abs(TempPower - currentLOPValue) > 0.4)
                    {

                        MessageBox.Show("无法固定Power!");
                        goto Error;
                    }
                }
                scope.ClearDisplay();
                scope.SetScaleOffset(adjustEYEStruce.TxLOPTarget, 1);
                scope.DisplayER();
                currentERValue = scope.GetEratio();
                targetERValue = currentERValue;
                procImodDACData.Add(currentLOPValue);

                if ((startValueMod == uperLimitIMod) && (currentERValue < targetERLL))
                {
                    logoStr += logger.AdapterLogString(4, "ER input Parameters HighLimit is too Small!");
                    logger.FlushLogBuffer();


                    imodDacTarget = startValueMod;

                    goto Error;

                }
                if ((startValueMod == lowLimitIMod) && (currentERValue > targetERUL))
                {
                    logoStr += logger.AdapterLogString(4, "DataBase input Parameters HighLimit is too large!");
                    logger.FlushLogBuffer();

                    imodDacTarget = startValueMod;
                    goto Error;
                }

                if (currentERValue > targetERUL)
                {
                    startValueMod -= stepImod;
                }
                if (currentERValue < targetERLL)
                {
                    startValueMod += stepImod;
                }

                i++;

            } while ((currentERValue > targetERUL || currentERValue < targetERLL) && i < 20);

            // currentERValue = scope.GetEratio();
            if (currentERValue <= targetERUL && currentERValue >= targetERLL)
            {
                isERok = true;
            }

            #endregion

   Error:

            currentERValue = scope.GetEratio();
            currentLOPValue = scope.GetAveragePowerdbm();
            dut.ReadTxpADC(out Temp);
            TxPowerAdc = Convert.ToUInt32(Temp);
            procTxpoweradcArray.Add(Temp);

            targetLOPValue = currentLOPValue;
            targetERValue = currentERValue;
            dut.StoreBiasDac(ibiasDacTarget);
            dut.StoreModDac(imodDacTarget);


            if (isERok && isLopok)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool FixTxPower(UInt32 startValueIbias, byte stepIbias, double TxTargetPower, out UInt32 CurrentIbiasDAC,out  double CurrentTxPower)
        {
            bool flagAdjust = false;
       
            CurrentIbiasDAC = startValueIbias;

            bool flagStartingdirectionUp = false; //表示写入初始值时候要调整的方向 true:表示小于最小值,需要向上调;False 表示大于最大值,需要向下调

            dut.WriteBiasDac(startValueIbias);

            CurrentTxPower = ScopeObject.GetAveragePowerdbm();
            if (CurrentTxPower > TxTargetPower + 0.2)
            {
                flagStartingdirectionUp = false;
            }
            else if (CurrentTxPower < TxTargetPower - 0.2)
            {
                flagStartingdirectionUp = true;
            }
            else if (CurrentTxPower < TxTargetPower + 0.2 && CurrentTxPower > TxTargetPower - 0.2)
            {
                return true;
            }

            do
            {
                dut.WriteBiasDac(startValueIbias);

                Thread.Sleep(100);
                CurrentTxPower = ScopeObject.GetAveragePowerdbm();
                //if (flagOpposition)
                //{
                    if (!flagStartingdirectionUp)//光过大
                    {
                       // UInt32 TempValue;
                        if (Step_SearchTargetPoint(startValueIbias, stepIbias, TxTargetPower - 0.2, TxTargetPower + 0.2, out CurrentIbiasDAC))
                        {
                           // CurrentIbiasDAC = TempValue;
                            CurrentTxPower = ScopeObject.GetAveragePowerdbm();
                            return true;
                        }
                        else
                        {
                            //MessageBox.Show("Can't Fix Txpower!");
                            logoStr += logger.AdapterLogString(3, "I Can't Fix Txpower!");
                            return false;
                        }

                    }
                    else//光过小
                    {
                       // UInt32 TempValue;
                        if (Step_SearchTargetPoint(startValueIbias, stepIbias, TxTargetPower - 0.2, TxTargetPower + 0.2, out CurrentIbiasDAC))
                        {
                           // CurrentIbiasDAC = TempValue;
                            CurrentTxPower = ScopeObject.GetAveragePowerdbm();
                            return true;
                        }
                        else
                        {
                           // MessageBox.Show("Can't Fix Txpower!");
                            logoStr += logger.AdapterLogString(3, "I Can't Fix Txpower!");
                            return false;
                        }
                    }


                //}

            } while (!flagAdjust);

            return true;
        }
        protected bool Step_SearchTargetPoint(UInt32 StartDac, UInt32 StepDac, double targetPoerLL, double targetPoerUL, out UInt32 biasDac)
        {

            UInt32 DacValue = StartDac;
            //double currentCense = 0;
            //byte count = 0;

            biasDac = StartDac;

            dut.WriteBiasDac(StartDac);
            Thread.Sleep(100);
            // ScopeObject.ClearDisplay();
            double CurrentTxPower = ScopeObject.GetAveragePowerdbm();

            while ((CurrentTxPower < targetPoerLL || CurrentTxPower > targetPoerUL) && (DacValue<adjustEYEStruce.IbiasDACMax&&DacValue>adjustEYEStruce.IbiasDACMin))
            {
                if (CurrentTxPower < targetPoerLL)
                {
                    DacValue += StepDac;
                }
                else if (CurrentTxPower > targetPoerUL)
                {
                    DacValue -= StepDac;
                }

                dut.WriteBiasDac(DacValue);
                Thread.Sleep(100);
                 CurrentTxPower = ScopeObject.GetAveragePowerdbm();

            }
            biasDac = DacValue;
            if ((CurrentTxPower >= targetPoerLL &&CurrentTxPower <= targetPoerUL) && (DacValue<=adjustEYEStruce.IbiasDACMax&&DacValue>=adjustEYEStruce.IbiasDACMin))
            {
                return true;
            }
            else
            {
               return false;
            }
            

        }  


#endregion

        #region  DC-LOOP-AdjustAP_ER,Method3,先调试ER 后调试 Power,适用于ER 对Power影响大 ,Power 对ER 影响小的 产品,个人认为不是很适用

        protected bool OnesectionMethodERandPower_Method3(UInt32 startValueIbias, UInt32 startValueMod, byte stepIbias, byte stepImod, double targetValueIbias, double targetTxPowerUL, double targetTxPowerLL, UInt32 uperLimitIbias, UInt32 lowLimitIbias, double targetValueIMod, double targetERUL, double targetERLL, Scope scope, DUT dut, UInt32 uperLimitIMod, UInt32 lowLimitIMod, out UInt32 ibiasDacTarget, out UInt32 imodDacTarget, out double targetERValue, out UInt32 TxPowerAdc, out double targetLOPValue, out bool isERok, out bool isLopok, out ArrayList procTxpoweradcArray, out ArrayList procTxpowerArray, out ArrayList procErArray, out ArrayList procIbiasDacArray, out ArrayList procImodDacArray)//ibias=0;modulation=1
        {
            procTxpoweradcArray = new ArrayList();
            procTxpowerArray = new ArrayList();
            procErArray = new ArrayList();
            procIbiasDacArray = new ArrayList();
            procImodDacArray = new ArrayList();
            procTxpoweradcArray.Clear();
            procTxpowerArray.Clear();
            procErArray.Clear();
            procIbiasDacArray.Clear();
            procImodDacArray.Clear();
            isERok = false;
            isLopok = false;
           // byte adjustCount = 0;
            
            byte totalExponentiationLopCount = Convert.ToByte(Math.Floor((Math.Log(Convert.ToDouble(stepIbias), 2))));
            byte totalExponentiationERCount = Convert.ToByte(Math.Floor((Math.Log(Convert.ToDouble(stepImod), 2))));
            double currentLOPValue = -1;
            double currentERValue = -1;
            

            UInt16 CurrentTxpowerAdc;
            UInt32 IbiasDAC = startValueIbias;
            UInt32 IModDAC = startValueMod;

            ibiasDacTarget = startValueIbias;
            imodDacTarget = startValueMod;
          //  IbiasDAC
            targetLOPValue = -1;
            targetERValue = -1;
            TxPowerAdc = 0;

            dut.WriteBiasDac(startValueIbias);
            dut.WriteModDac(startValueMod);
            procIbiasDacArray.Add(startValueIbias);
            procImodDACData.Add(startValueMod);
            scope.SetScaleOffset(adjustEYEStruce.TxLOPTarget, 1);
            scope.ClearDisplay();
            scope.DisplayPowerdbm();
            currentLOPValue = scope.GetAveragePowerdbm();
            scope.DisplayER();
            currentERValue = scope.GetEratio();
            procTxpowerArray.Add(currentLOPValue);
            procErArray.Add(currentERValue);
            dut.ReadTxpADC(out CurrentTxpowerAdc);
            procTxpoweradcArray.Add(CurrentTxpowerAdc);
            int adjustCount = 0;
            {

              #region  AdjustTxER

            if (currentERValue > targetERUL || currentERValue < targetERLL)
            {
                int j = 0;
                do
                {

                    dut.WriteModDac(IModDAC);

                    procImodDACData.Add(IModDAC);
                   // imodDacTarget = startValueMod;

                    scope.ClearDisplay();
                    scope.DisplayER();
                    currentERValue = scope.GetEratio();
                   
                //    targetERValue = currentERValue;
                    procImodDACData.Add(IModDAC);

                    if (IModDAC >= uperLimitIMod && currentERValue < targetERLL)
                    {
                        logoStr += logger.AdapterLogString(4, "ER input Parameters HighLimit is too Small!");
                        logger.FlushLogBuffer();
                        imodDacTarget = IModDAC;

                        goto Error;

                    }
                    if (IModDAC <= lowLimitIMod && currentERValue > targetERUL)
                    {
                        logoStr += logger.AdapterLogString(4, "DataBase input Parameters HighLimit is too large!");
                        logger.FlushLogBuffer();

                        imodDacTarget = IModDAC;
                        goto Error;
                    }

                    if (currentERValue > targetERUL)
                    {
                        IModDAC -= stepImod;
                    }
                    if (currentERValue < targetERLL)
                    {
                        IModDAC += stepImod;
                    }
                    j++;
                } while ((currentERValue > targetERUL || currentERValue < targetERLL) && j < 20);
            }

            targetERValue = currentERValue;
            #endregion

              #region  AdjustTxPower

            if (currentLOPValue > targetTxPowerUL || currentLOPValue < targetTxPowerLL)
            {
                int i = 0;
                do
                {

                    dut.WriteBiasDac(IbiasDAC);
                    ibiasDacTarget = IbiasDAC;
                    procIbiasDacArray.Add(IbiasDAC);
                    scope.SetScaleOffset(adjustEYEStruce.TxLOPTarget, 1);
                    scope.ClearDisplay();
                    scope.DisplayPowerdbm();
                    currentLOPValue = scope.GetAveragePowerdbm();
                    targetLOPValue = currentLOPValue;
                    procTxpowerArray.Add(currentLOPValue);
                    dut.ReadTxpADC(out CurrentTxpowerAdc);
                    procTxpoweradcArray.Add(CurrentTxpowerAdc);

                    if (currentLOPValue > targetTxPowerUL)
                    {
                        IbiasDAC -= stepIbias;
                    }
                    if (currentLOPValue < targetTxPowerLL)
                    {
                        IbiasDAC += stepIbias;
                    }


                    if ((IbiasDAC >= uperLimitIbias) && (currentLOPValue < targetTxPowerLL))
                    {
                        logoStr += logger.AdapterLogString(4, "DataBase input Parameters HighLimit is too Small!");
                        logger.FlushLogBuffer();
                        ibiasDacTarget = IbiasDAC;
                       
                        goto Error;
                    }
                    if ((IbiasDAC <= lowLimitIbias) && (currentLOPValue > targetTxPowerUL))
                    {
                        logoStr += logger.AdapterLogString(4, "DataBase input Parameters HighLimit is too large!");
                        logger.FlushLogBuffer();
                        ibiasDacTarget = IbiasDAC;
                      
                        goto Error;
                    }

                    i++;

                } while ((currentLOPValue > targetTxPowerUL || currentLOPValue < targetTxPowerLL) && i < 20);
            }
            else
            {
                isLopok = true;
            }

            if (currentERValue <= targetERUL && currentERValue >= targetERLL)
            {
                isERok = true;
            }

            if (currentLOPValue <= targetTxPowerUL && currentLOPValue >= targetTxPowerLL)
            {
                isLopok = true;
            }

            #endregion
            adjustCount++;
            } while ((!isERok || !isLopok) && adjustCount < 3) ;

            currentERValue = ReadER();

            if (currentERValue <= targetERUL && currentERValue >= targetERLL)
            {
                isERok = true;
            }
            else
            {
                isERok = false;
            }

            

        Error:

            targetERValue = scope.GetEratio();
            targetLOPValue = scope.GetAveragePowerdbm();
            dut.ReadTxpADC(out CurrentTxpowerAdc);
            TxPowerAdc = Convert.ToUInt32(CurrentTxpowerAdc);
            procTxpoweradcArray.Add(CurrentTxpowerAdc);

            //targetLOPValue = currentLOPValue;
            //targetERValue = currentERValue;
            dut.StoreBiasDac(ibiasDacTarget);
            dut.StoreModDac(imodDacTarget);


            if (isERok && isLopok)
            {
                return true;
            }
            else
            {

                return false;
            }
        }
     
#endregion

        #region  DC-LOOP-AdjustAP_ER Method4 ,Power以初始值确定,,调整ER但固定Ibias 电流 ,不管Power

        protected bool OnesectionMethodERandPower_Method4(UInt32 startValueIbias, UInt32 startValueMod, byte stepIbias, byte stepImod, double targetValueIbias, double IbiasCurrentMax, double IbiasCurrentMin, double targetValueIMod, double targetERUL, double targetERLL, Scope scope, DUT dut, UInt32 uperLimitIMod, UInt32 lowLimitIMod, out UInt32 ibiasDacTarget, out UInt32 imodDacTarget, out double targetERValue, out UInt32 TxPowerAdc, out double targetLOPValue, out bool isERok, out bool isLopok, out ArrayList procTxpoweradcArray, out ArrayList procTxpowerArray, out ArrayList procErArray, out ArrayList procIbiasDacArray, out ArrayList procImodDacArray)//ibias=0;modulation=1
        {
            procTxpoweradcArray = new ArrayList();
            procTxpowerArray = new ArrayList();
            procErArray = new ArrayList();
            procIbiasDacArray = new ArrayList();
            procImodDacArray = new ArrayList();
            procTxpoweradcArray.Clear();
            procTxpowerArray.Clear();
            procErArray.Clear();
            procIbiasDacArray.Clear();
            procImodDacArray.Clear();
            isERok = false;
            isLopok = false;
            //byte adjustCount = 0;
            //byte backUpCountLop = 0;
            //byte backUpCountEr = 0;
            byte totalExponentiationLopCount = Convert.ToByte(Math.Floor((Math.Log(Convert.ToDouble(stepIbias), 2))));
            byte totalExponentiationERCount = Convert.ToByte(Math.Floor((Math.Log(Convert.ToDouble(stepImod), 2))));
            double currentLOPValue = -1;
            double currentERValue = -1;
            targetLOPValue = -1;
            targetERValue = -1;
            TxPowerAdc = 0;
            UInt16 Temp;
            byte[] writeData = new byte[1];

            dut.WriteBiasDac(startValueIbias);
            dut.WriteModDac(startValueMod);
            procIbiasDacArray.Add(startValueIbias);
            procImodDACData.Add(startValueMod);
            scope.SetScaleOffset(adjustEYEStruce.TxLOPTarget, 1);
            scope.ClearDisplay();
            scope.DisplayPowerdbm();
            currentLOPValue = scope.GetAveragePowerdbm();
            scope.DisplayER();
            currentERValue = scope.GetEratio();
            procTxpowerArray.Add(currentLOPValue);
            procErArray.Add(currentERValue);
            dut.ReadTxpADC(out Temp);
            procTxpoweradcArray.Add(Temp);

           int i = 0;

            #region  AdjustTxPower
          
                dut.WriteBiasDac(startValueIbias);
                procIbiasDacArray.Add(startValueIbias);
                scope.ClearDisplay();
                scope.SetScaleOffset(adjustEYEStruce.TxLOPTarget, 1);
                scope.DisplayPowerdbm();
                currentLOPValue = scope.GetAveragePowerdbm();
                targetLOPValue = currentLOPValue;
                procTxpowerArray.Add(currentLOPValue);
                dut.ReadTxpADC(out Temp);
                procTxpoweradcArray.Add(Temp);


                isLopok = true;
                currentERValue = scope.GetEratio();
                ibiasDacTarget = startValueIbias;
           
            #endregion

            #region  AdjustTxER
            i = 0;

            do
            {

                dut.WriteModDac(startValueMod);

                procImodDACData.Add(startValueMod);
                imodDacTarget = startValueMod;

            
                double TempPower;
                double Step = stepIbias / 2;            //Math.Ceiling(stepIbias/2);

                if (!FixTxPower(ibiasDacTarget, (byte)(Math.Ceiling(Step)), currentLOPValue, out ibiasDacTarget, out TempPower))
                {
                    if (Math.Abs(TempPower - currentLOPValue) > 0.4)
                    {

                        MessageBox.Show("无法固定Power!");
                        goto Error;
                    }
                }

          
                if (!FixDmiBias(ibiasDacTarget, (byte)(Math.Ceiling(Step)), IbiasCurrentMax, IbiasCurrentMin, out ibiasDacTarget))
                {
                    MessageBox.Show("无法固定IbiasCurrent!");
                    goto Error;
                }

                scope.ClearDisplay();
                scope.SetScaleOffset(adjustEYEStruce.TxLOPTarget, 1);
                scope.DisplayER();
                currentERValue = scope.GetEratio();
                targetERValue = currentERValue;
                procImodDACData.Add(currentLOPValue);

                if ((startValueMod == uperLimitIMod) && (currentERValue < targetERLL))
                {
                    logoStr += logger.AdapterLogString(4, "ER input Parameters HighLimit is too Small!");
                    logger.FlushLogBuffer();
                    imodDacTarget = startValueMod;
                    goto Error;

                }

                if ((startValueMod == lowLimitIMod) && (currentERValue > targetERUL))
                {
                    logoStr += logger.AdapterLogString(4, "DataBase input Parameters HighLimit is too large!");
                    logger.FlushLogBuffer();
                    imodDacTarget = startValueMod;
                    goto Error;
                }

                if (currentERValue > targetERUL)
                {
                    startValueMod -= stepImod;
                }
                if (currentERValue < targetERLL)
                {
                    startValueMod += stepImod;
                }
                i++;

            } while ((currentERValue > targetERUL || currentERValue < targetERLL) && i < 20);

            if (currentERValue <= targetERUL && currentERValue >= targetERLL)
            {
                isERok = true;
            }

            #endregion

        Error:

            currentERValue = scope.GetEratio();
            currentLOPValue = scope.GetAveragePowerdbm();
            dut.ReadTxpADC(out Temp);
            TxPowerAdc = Convert.ToUInt32(Temp);
            procTxpoweradcArray.Add(Temp);

            targetLOPValue = currentLOPValue;
            targetERValue = currentERValue;
            dut.StoreBiasDac(ibiasDacTarget);
            dut.StoreModDac(imodDacTarget);


            if (isERok && isLopok)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        enum Targetstatus: byte
        {
           Hig,
          
           Low
        }
        private bool FixDmiBias(UInt32 startValueIbias, byte stepIbias, double biasCurrentMax,double biasCurrentMin,out UInt32 CurrentIbiasDAC)
        {


            int Count = 0;
            bool flagAdjust = false;
            double  LastIbiasCurrent = dut.ReadDmiBias();
            double  TxPower = ScopeObject.GetAveragePowerdbm();
            CurrentIbiasDAC = startValueIbias;
            dut.WriteBiasDac(startValueIbias);
            Targetstatus OnsetStatus;//其实状态
            Targetstatus CurrentStatus;//其实状态

            OnsetStatus = Targetstatus.Hig;
            CurrentStatus = Targetstatus.Low;

#region   判定是否跳转

            dut.WriteBiasDac(CurrentIbiasDAC);
            Thread.Sleep(100);
            LastIbiasCurrent = dut.ReadDmiBias();
            TxPower = ScopeObject.GetAveragePowerdbm();
            logoStr += logger.AdapterLogString(4, "CurrentIbiasDAC=" + CurrentIbiasDAC + " DmiIbias=" + LastIbiasCurrent + " TxPower =" + TxPower);

            if (LastIbiasCurrent > biasCurrentMax)
            {
                OnsetStatus = Targetstatus.Hig;
            }
            else if (LastIbiasCurrent < biasCurrentMin)
            {
                OnsetStatus = Targetstatus.Low;
            }
            else if (LastIbiasCurrent < biasCurrentMax && LastIbiasCurrent > biasCurrentMin)
            {
                return true;
            }

#endregion

            do
            {
                dut.WriteBiasDac(CurrentIbiasDAC);
                Thread.Sleep(100);
                LastIbiasCurrent = dut.ReadDmiBias();
                TxPower = ScopeObject.GetAveragePowerdbm();
                logoStr += logger.AdapterLogString(4, "CurrentIbiasDAC=" + CurrentIbiasDAC + " DmiIbias=" + LastIbiasCurrent + " TxPower =" + TxPower);


                if (LastIbiasCurrent > biasCurrentMax)
                {
                    CurrentStatus = Targetstatus.Hig;
                }
                else if (LastIbiasCurrent < biasCurrentMin)
                {
                    CurrentStatus = Targetstatus.Low;
                }
                else if (LastIbiasCurrent < biasCurrentMax && LastIbiasCurrent > biasCurrentMin)
                {
                  
                    break;
                }

                if (OnsetStatus !=CurrentStatus)
                {
                    flagAdjust = true;
                
                }
                
                if (flagAdjust)
                {

                    Count++;
                    stepIbias = 2;

                    if (Count > adjustEYEStruce.IbiasStep)
                    {
                        return false;
                    }
                }

                if (LastIbiasCurrent > biasCurrentMax)
                { 
                    CurrentIbiasDAC-= stepIbias;
                }
                else if (LastIbiasCurrent < biasCurrentMin)
                {
                    CurrentIbiasDAC += stepIbias;
                }
                else if (LastIbiasCurrent < biasCurrentMax && LastIbiasCurrent > biasCurrentMin)
                {
                    flagAdjust = true;
                }

                if (CurrentIbiasDAC > adjustEYEStruce.IbiasDACMax || CurrentIbiasDAC<adjustEYEStruce.IbiasDACMin)
                {
                    return false;
                }

            } while (!flagAdjust);

            return true;
        }
   
        #endregion

        #region  DC-LOOP-AdjustAP_ER Method5: 1->需要将Ibias 电流调入一个小范围,需要BiasDAC && ModDac 共同完成调试,如果调入了范围Power 不满足 FMT 规格,则False.2->调整ER但固定Ibias 电流 ,不管Power

        protected bool OnesectionMethodERandPower_Method5(UInt32 startValueIbias, UInt32 startValueMod, byte stepIbias, byte stepImod, double targetValueIbias, double IbiasCurrentMax, double IbiasCurrentMin, double targetValueIMod, double targetERUL, double targetERLL, Scope scope, DUT dut, UInt32 uperLimitIMod, UInt32 lowLimitIMod, out UInt32 ibiasDacTarget, out UInt32 imodDacTarget, out double targetERValue, out UInt32 TxPowerAdc, out double targetLOPValue, out bool isERok, out bool isLopok, out ArrayList procTxpoweradcArray, out ArrayList procTxpowerArray, out ArrayList procErArray, out ArrayList procIbiasDacArray, out ArrayList procImodDacArray)//ibias=0;modulation=1
        {
            procTxpoweradcArray = new ArrayList();
            procTxpowerArray = new ArrayList();
            procErArray = new ArrayList();
            procIbiasDacArray = new ArrayList();
            procImodDacArray = new ArrayList();
            procTxpoweradcArray.Clear();
            procTxpowerArray.Clear();
            procErArray.Clear();
            procIbiasDacArray.Clear();
            procImodDacArray.Clear();
            isERok = false;
            isLopok = false;
            byte adjustBiasCount = 0;
            //byte backUpCountLop = 0;
            //byte backUpCountEr = 0;
            byte totalExponentiationLopCount = Convert.ToByte(Math.Floor((Math.Log(Convert.ToDouble(stepIbias), 2))));
            byte totalExponentiationERCount = Convert.ToByte(Math.Floor((Math.Log(Convert.ToDouble(stepImod), 2))));
            double currentLOPValue = -1;
            double currentERValue = -1;
            targetLOPValue = -1;
            targetERValue = -1;
            TxPowerAdc = 0;
            UInt16 Temp;
            byte[] writeData = new byte[1];
            double DmiBiasCurrent;
            dut.WriteBiasDac(startValueIbias);
            dut.WriteModDac(startValueMod);
            procIbiasDacArray.Add(startValueIbias);
            procImodDACData.Add(startValueMod);
            scope.SetScaleOffset(adjustEYEStruce.TxLOPTarget, 1);
            scope.ClearDisplay();
            scope.DisplayPowerdbm();
            currentLOPValue = scope.GetAveragePowerdbm();
            scope.DisplayER();
            currentERValue = scope.GetEratio();
            procTxpowerArray.Add(currentLOPValue);
            procErArray.Add(currentERValue);
            dut.ReadTxpADC(out Temp);
            procTxpoweradcArray.Add(Temp);
            

            int i = 0;

            #region  AdjustTxPower

            do 
            {

             dut.WriteModDac(startValueMod);
            dut.WriteBiasDac(startValueIbias);
            procIbiasDacArray.Add(startValueIbias);
            scope.ClearDisplay();
            scope.SetScaleOffset(adjustEYEStruce.TxLOPTarget, 1);
           // scope.DisplayPowerdbm();
            currentLOPValue = scope.GetAveragePowerdbm();
            targetLOPValue = currentLOPValue;
            procTxpowerArray.Add(currentLOPValue);
            dut.ReadTxpADC(out Temp);
            procTxpoweradcArray.Add(Temp);
         
            ibiasDacTarget = startValueIbias;
            imodDacTarget = startValueMod;

             DmiBiasCurrent=dut.ReadDmiBias();
             logoStr += logger.AdapterLogString(1, "IbiasCurrent="+DmiBiasCurrent);

             if (DmiBiasCurrent > IbiasCurrentMax)
                {
                    startValueIbias -= stepIbias;
                    startValueMod += stepIbias;
                }
             else if (DmiBiasCurrent < IbiasCurrentMin)
             {
                 startValueIbias += stepIbias;
                 startValueMod -= stepIbias;
             }
             else
             {
                 isLopok = true;

             }
             adjustBiasCount++;


            } while ((DmiBiasCurrent > IbiasCurrentMax || DmiBiasCurrent < IbiasCurrentMin) && adjustBiasCount<20);


            currentERValue = scope.GetEratio();
            ibiasDacTarget = startValueIbias;

            if (!isLopok)
            {
                goto Error;
            }

            #endregion

            #region  AdjustTxER
            i = 0;

            do
            {

                dut.WriteModDac(startValueMod);

                procImodDACData.Add(startValueMod);
                imodDacTarget = startValueMod;


                double TempPower;
                double Step = stepIbias / 2;            //Math.Ceiling(stepIbias/2);

                //if (!FixTxPower(ibiasDacTarget, (byte)(Math.Ceiling(Step)), currentLOPValue, out ibiasDacTarget, out TempPower))
                //{
                //    if (Math.Abs(TempPower - currentLOPValue) > 0.4)
                //    {

                //        MessageBox.Show("无法固定Power!");
                //        goto Error;
                //    }
                //}


                if (!FixDmiBias(ibiasDacTarget, (byte)(Math.Ceiling(Step)), IbiasCurrentMax, IbiasCurrentMin, out ibiasDacTarget))
                {
                    MessageBox.Show("无法固定IbiasCurrent!");
                    goto Error;
                }

                scope.ClearDisplay();
                scope.SetScaleOffset(adjustEYEStruce.TxLOPTarget, 1);
                scope.DisplayER();
                currentERValue = scope.GetEratio();
                targetERValue = currentERValue;
                procImodDACData.Add(currentLOPValue);

                if ((startValueMod == uperLimitIMod) && (currentERValue < targetERLL))
                {
                    logoStr += logger.AdapterLogString(4, "ER input Parameters HighLimit is too Small!");
                    logger.FlushLogBuffer();
                    imodDacTarget = startValueMod;
                    goto Error;

                }

                if ((startValueMod == lowLimitIMod) && (currentERValue > targetERUL))
                {
                    logoStr += logger.AdapterLogString(4, "DataBase input Parameters HighLimit is too large!");
                    logger.FlushLogBuffer();
                    imodDacTarget = startValueMod;
                    goto Error;
                }

                if (currentERValue > targetERUL)
                {
                    startValueMod -= stepImod;
                }
                if (currentERValue < targetERLL)
                {
                    startValueMod += stepImod;
                }
                i++;

            } while ((currentERValue > targetERUL || currentERValue < targetERLL) && i < 20);

            if (currentERValue <= targetERUL && currentERValue >= targetERLL)
            {
                isERok = true;
            }

            #endregion

        Error:

            currentERValue = scope.GetEratio();
            currentLOPValue = scope.GetAveragePowerdbm();
            dut.ReadTxpADC(out Temp);
            TxPowerAdc = Convert.ToUInt32(Temp);
            procTxpoweradcArray.Add(Temp);

            targetLOPValue = currentLOPValue;
            targetERValue = currentERValue;
            dut.StoreBiasDac(ibiasDacTarget);
            dut.StoreModDac(imodDacTarget);


            if (isERok && isLopok)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
      
      
        #endregion

        #region  DC_LOOP_AdjustAP-ER Method6: ER从规格下限开始调整，写入BiasDAC，根据公式算出ModDAC并写入，读取ER，经过调整使ER进入规格范围

        private bool AdjustAPbyMod()
        {

            bool isAdjustOK = false;
            byte[] writeData = new byte[1];



            UInt32 currentValueIbias = adjustEYEStruce.IbiasDACStart;
            UInt32 currentValueMod = adjustEYEStruce.ImodDACStart;

            uint startValueMod = adjustEYEStruce.ImodDACStart;
            uint startValueIbias = adjustEYEStruce.IbiasDACStart;



            UInt16 Temp;
            double currentLOPValue = -1;
            double currentERValue = -1;
            UInt32 tempTxPowerAdc = 0;


            #region  AdjustTxPower


            double intResult = 0;

            //  CalculateRegist(1, 0, 52, out intResult);

            logoStr += logger.AdapterLogString(1, "ibiasDacTarget=" + ibiasDacTarget + "; " + "TargetCurrent=" + adjustEYEStruce.TargetCurrentArray[GlobalParameters.CurrentChannel - 1]);

            intResult = algorithm.AnalyticalExpression(GlobalParameters.ImodFormula, Convert.ToDouble(adjustEYEStruce.TargetCurrentArray[GlobalParameters.CurrentChannel - 1]), ibiasDacTarget, 0);

            imodDacTarget = Convert.ToUInt16(intResult);
            dut.WriteModDac(imodDacTarget);
            Thread.Sleep(1000);
            procImodDACData.Add(imodDacTarget);
            currentLOPValue = ReadAp(out tempTxPowerAdc);
            //procTxPowerData.Add(currentLOPValue);
            procTxPowerData.Add(ibiasDacTarget + ":" + imodDacTarget + "_" + currentLOPValue);

            if (currentLOPValue <= adjustEYEStruce.TxLOPUL && currentLOPValue >= adjustEYEStruce.TxLOPLL)
            {
                isAdjustOK = true;
            }
            else
            {
                isAdjustOK = false;
            }


            #endregion

            return isAdjustOK;
        }

        private bool AdjustERbyIbias()
        {

            bool isAdjust = false;
            byte[] writeData = new byte[1];
            double intResult = 0;

            UInt32 currentValueIbias = adjustEYEStruce.IbiasDACStart;
            UInt32 currentValueMod = adjustEYEStruce.ImodDACStart;

            uint startValueMod = adjustEYEStruce.ImodDACStart;
            uint startValueIbias = adjustEYEStruce.IbiasDACStart;

            byte stepIbias = adjustEYEStruce.IbiasStep;

            UInt32 uperLimitIbias = adjustEYEStruce.IbiasDACMax;
            UInt32 lowLimitIbias = adjustEYEStruce.IbiasDACMin;

            double currentERValue = -1;

            #region  AdjustTxER

            int i = 0;
            bool erUnderLL = true;       //true：起始ER小于调整下限；false：起始ER大于调整下限
            uint currentstepIbias = 0;   //当前调整步长

            try
            {
                do
                {
                    dut.WriteBiasDac(startValueIbias);
                    ibiasDacTarget = startValueIbias;

                    intResult = algorithm.AnalyticalExpression(GlobalParameters.ImodFormula, Convert.ToDouble(adjustEYEStruce.TargetCurrentArray[GlobalParameters.CurrentChannel - 1]), ibiasDacTarget, 0);
                    imodDacTarget = Convert.ToUInt16(intResult);
                    dut.WriteModDac(imodDacTarget);

                    Thread.Sleep(1000);

                    ScopeObject.ClearDisplay();
                    ScopeObject.SetScaleOffset(adjustEYEStruce.TxLOPTarget, 1);
                    ScopeObject.DisplayER();
                    currentERValue = ReadER();
                    targetER = currentERValue;

                    procIbiasDACData.Add(startValueIbias);
                    //procErData.Add(currentERValue);
                    procErData.Add(ibiasDacTarget + ":" + imodDacTarget + "_" + currentERValue);
                    logoStr += logger.AdapterLogString(1, "BiasDAC=" + ibiasDacTarget + "ModDAC=" + imodDacTarget + "--->" + "CurrentER=" + targetER);

                    if ((startValueIbias == uperLimitIbias) && (currentERValue > adjustEYEStruce.AdjustErUL))
                    {
                        logoStr += logger.AdapterLogString(4, "Start ModDAC is too Small!");
                        logger.FlushLogBuffer();
                        // ibiasDacTarget = startValueIbias;
                        break;
                    }

                    if ((startValueIbias == lowLimitIbias) && (currentERValue < adjustEYEStruce.AdjustErLL))
                    {
                        logoStr += logger.AdapterLogString(4, "Start ModDAC is too large!");
                        logger.FlushLogBuffer();
                        // ibiasDacTarget = startValueMod;
                        break;
                    }

                    if (currentERValue > adjustEYEStruce.AdjustErUL)
                    {
                        if (i == 0)
                        {
                            erUnderLL = false;            //判断起始ER
                            startValueIbias += stepIbias;
                            currentstepIbias = stepIbias;
                        }
                        else
                        {
                            if (erUnderLL == true)      //起始ER小于调整下限，当前ER大于调整上限，减半当前调整步长
                            {
                                if (Convert.ToUInt32(currentstepIbias / 2) == 0)
                                {
                                    startValueIbias += 1;
                                    currentstepIbias = 1;
                                }
                                else
                                {
                                    startValueIbias += Convert.ToUInt32(currentstepIbias / 2);
                                    currentstepIbias = Convert.ToUInt32(currentstepIbias / 2);
                                }
                            }
                            else
                            {
                                startValueIbias += currentstepIbias;
                            }
                        }

                        if (startValueIbias > uperLimitIbias)
                        {
                            startValueIbias = uperLimitIbias;
                        }
                    }

                    if (currentERValue < adjustEYEStruce.AdjustErLL)
                    {
                        if (i == 0)
                        {
                            erUnderLL = true;       //判断起始ER
                            startValueIbias -= stepIbias;
                            currentstepIbias = stepIbias;
                        }
                        else
                        {
                            if (erUnderLL == false)      //起始ER大于调整上限，当前ER小于调整下限，减半当前调整步长
                            {
                                if (Convert.ToUInt32(currentstepIbias / 2) == 0)
                                {
                                    startValueIbias -= 1;
                                    currentstepIbias = 1;
                                }
                                else
                                {
                                    startValueIbias -= Convert.ToUInt32(currentstepIbias / 2);
                                    currentstepIbias = Convert.ToUInt32(currentstepIbias / 2);
                                }
                            }
                            else
                            {
                                startValueIbias -= currentstepIbias;
                            }
                        }

                        if (startValueIbias < lowLimitIbias)
                        {
                            startValueIbias = lowLimitIbias;
                        }
                    }

                    if (currentERValue >= adjustEYEStruce.AdjustErLL && currentERValue <= adjustEYEStruce.AdjustErUL)
                    {
                        isAdjust = true;
                    }

                    i++;

                } while ((currentERValue > adjustEYEStruce.AdjustErUL || currentERValue < adjustEYEStruce.AdjustErLL) && i < 20);

                if (currentERValue <= adjustEYEStruce.TxErUL && currentERValue >= adjustEYEStruce.TxErLL)
                {
                    isAdjust = true;
                }
                else
                {
                    isAdjust = false;
                }

                return isAdjust;
            }
            catch (System.Exception ex)
            {
                return false;
            }
            #endregion

        }

        protected bool OnesectionMethodERandPower_Method6(out bool isAPok, out bool isErok)
        {

            byte[] writeData = new byte[1];

            UInt32 CurrentBiasDAC = adjustEYEStruce.IbiasDACStart;
            UInt32 currentModDAC = adjustEYEStruce.ImodDACStart;

            //uint startValueMod = adjustEYEStruce.ImodDACStart;
            //uint startValueIbias = adjustEYEStruce.IbiasDACStart;

            //byte stepIbias = adjustEYEStruce.IbiasStep;
            //byte stepImod = adjustEYEStruce.ImodStep;
            //double GlobalTxPowerUL = adjustEYEStruce.TxLOPUL;         //AP FMT规格上下限
            //double GlobalTxPowerLL = adjustEYEStruce.TxLOPLL;
            //double targetTxPowerUL = adjustEYEStruce.AdjustTxLOPUL;   //AP 调整规格上下限
            //double targetTxPowerLL = adjustEYEStruce.AdjustTxLOPLL;
            //UInt32 uperLimitIbias = adjustEYEStruce.IbiasDACMax;
            //UInt32 lowLimitIbias = adjustEYEStruce.IbiasDACMin;
            //double GlobalERUL = adjustEYEStruce.TxErUL;            //ER FMT规格上下限
            //double GlobalERLL = adjustEYEStruce.TxErLL;
            //double targetERUL = adjustEYEStruce.AdjustErUL;        //ER 调整规格上下限
            //double targetERLL = adjustEYEStruce.AdjustErLL;
            //UInt32 uperLimitIMod = adjustEYEStruce.ModDacMax;
            //UInt32 lowLimitIMod = adjustEYEStruce.ModDacMin;

            UInt16 Temp;
            double currentLOPValue = -1;
            double currentERValue = -1;
            UInt32 tempTxPowerAdc = 0;

            procTxPowerADCData.Clear();
            procTxPowerData.Clear();
            procErData.Clear();
            procIbiasDACData.Clear();
            procImodDACData.Clear();
            isErok = false;
            isAPok = false;


            targetLOP = -1;
            targetER = -1;
            txpowerAdcTarget = 0;


            // byte[] writeData = new byte[1];

            if (CurrentBiasDAC > adjustEYEStruce.IbiasDACMax)
            {
                CurrentBiasDAC = adjustEYEStruce.IbiasDACMax;
            }
            if (currentModDAC > adjustEYEStruce.ModDacMax)
            {
                currentModDAC = adjustEYEStruce.ModDacMax;
            }
            if (CurrentBiasDAC < adjustEYEStruce.IbiasDACMin)
            {
                CurrentBiasDAC = adjustEYEStruce.IbiasDACMin;
            }
            if (currentModDAC < adjustEYEStruce.ModDacMin)
            {
                currentModDAC = adjustEYEStruce.ModDacMin;
            }

            dut.WriteBiasDac(CurrentBiasDAC);
            dut.WriteModDac(currentModDAC);

            ibiasDacTarget = CurrentBiasDAC;
            imodDacTarget = currentModDAC;

            procIbiasDACData.Add(CurrentBiasDAC);
            procImodDACData.Add(currentModDAC);

            ScopeObject.SetScaleOffset(adjustEYEStruce.TxLOPTarget, 1);
            ScopeObject.ClearDisplay();
            ScopeObject.DisplayPowerdbm();
            currentLOPValue = ReadAp(out tempTxPowerAdc);
            ScopeObject.DisplayER();
            currentERValue = ReadER();
            //procTxPowerData.Add(currentLOPValue);
            //procTxPowerData.Add(ibiasDacTarget + ":" + imodDacTarget + "_" + currentLOPValue);
            //procErData.Add(currentERValue);
            procErData.Add(ibiasDacTarget + ":" + imodDacTarget + "_" + currentERValue);
            dut.ReadTxpADC(out Temp);
            procTxPowerADCData.Add(Temp);


            //int i = 0;


            //for (i = 0; i < 2; i++)
            //{

            isErok = AdjustERbyIbias();

                //isAPok = AdjustAPbyMod();

            //    currentERValue = ReadER();
        //    //procErData.Add(currentERValue);
        //    procErData.Add(ibiasDacTarget + ":" + imodDacTarget + "_" + currentERValue);

            //    isErok = false;

            //    if (currentERValue >= adjustEYEStruce.AdjustErLL && currentERValue <= adjustEYEStruce.AdjustErUL)
        //    {
        //        isErok = true;
        //    }

            //    if (isErok&&isAPok)
        //    {
        //        break;
        //    }

            //}

        Error:

            //currentERValue = ReadER();
            currentLOPValue = ReadAp(out tempTxPowerAdc);

            //procErData.Add(currentERValue);
            //procTxPowerData.Add(currentLOPValue);
            //procTxPowerData.Add(ibiasDacTarget + ":" + imodDacTarget + "_" + currentLOPValue);
            logoStr += logger.AdapterLogString(1, "BiasDAC=" + ibiasDacTarget + "ModDAC=" + imodDacTarget + "--->" + "CurrentLOP=" + currentLOPValue);
            //procErData.Add(ibiasDacTarget + ":" + imodDacTarget + "_" + currentERValue);


            //if (currentERValue >= adjustEYEStruce.TxErLL && currentERValue <= adjustEYEStruce.TxErUL)
            //{
            //    isErok = true;
            //}

            if (currentLOPValue >= adjustEYEStruce.TxLOPLL && currentLOPValue <= adjustEYEStruce.TxLOPUL)
            {
                isAPok = true;
            }



            dut.ReadTxpADC(out Temp);
            // TxPowerAdc = Convert.ToUInt32(Temp);
            txpowerAdcTarget = Convert.ToUInt32(Temp);
            //  procTxpoweradcArray.Add(Temp);
            procTxPowerADCData.Add(Temp);
            targetLOP = currentLOPValue;
            //targetER = currentERValue;
            dut.StoreBiasDac(ibiasDacTarget);
            dut.StoreModDac(imodDacTarget);
            
            if (isErok && isAPok)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion 

#region  DC-LOOP-AdjustAP_ER Method7: 先调BiasDAC使Power进入调整范围；再调ModDAC使ER进入调整范围，如果调整ER出现饱和时，再调整BiasDAC和ModDAC，使Power、ER均满足调整范围
        protected bool OnesectionMethodERandPower_Method7(out bool isERok, out bool isLopok)
        {
            UInt32 currentValueIbias = adjustEYEStruce.IbiasDACStart;
            UInt32 currentValueMod = adjustEYEStruce.ImodDACStart;
            byte stepIbias = adjustEYEStruce.IbiasStep;
            byte stepImod = adjustEYEStruce.ImodStep;
            double GlobalTxPowerUL = adjustEYEStruce.TxLOPUL;         //AP FMT规格上下限
            double GlobalTxPowerLL = adjustEYEStruce.TxLOPLL;
            double targetTxPowerUL = adjustEYEStruce.AdjustTxLOPUL;   //AP 调整规格上下限
            double targetTxPowerLL = adjustEYEStruce.AdjustTxLOPLL;
            UInt32 uperLimitIbias = adjustEYEStruce.IbiasDACMax;
            UInt32 lowLimitIbias = adjustEYEStruce.IbiasDACMin;
            double GlobalERUL = adjustEYEStruce.TxErUL;            //ER FMT规格上下限
            double GlobalERLL = adjustEYEStruce.TxErLL;
            double targetERUL = adjustEYEStruce.AdjustErUL;        //ER 调整规格上下限
            double targetERLL = adjustEYEStruce.AdjustErLL;
            UInt32 uperLimitIMod = adjustEYEStruce.ModDacMax;
            UInt32 lowLimitIMod = adjustEYEStruce.ModDacMin;

            procTxPowerADCData.Clear();
            procTxPowerData.Clear();
            procErData.Clear();
            procIbiasDACData.Clear();
            procImodDACData.Clear();
            isERok = false;
            isLopok = false;

            double currentLOPValue = -1;
            double currentERValue = -1;
            targetLOP = -1;
            targetER = -1;
            txpowerAdcTarget = 0;

            UInt16 Temp;
            byte[] writeData = new byte[1];

            if (currentValueIbias > uperLimitIbias)
            {
                currentValueIbias = uperLimitIbias;
            }
            if (currentValueMod > uperLimitIMod)
            {
                currentValueMod = uperLimitIMod;
            }
            if (currentValueIbias < lowLimitIbias)
            {
                currentValueIbias = lowLimitIbias;
            }
            if (currentValueMod < lowLimitIMod)
            {
                currentValueMod = lowLimitIMod;
            }

            dut.WriteBiasDac(currentValueIbias);
            dut.WriteModDac(currentValueMod);
            procIbiasDACData.Add(currentValueIbias);
            procImodDACData.Add(currentValueMod);

            ScopeObject.SetScaleOffset(adjustEYEStruce.TxLOPTarget, 1);
            ScopeObject.ClearDisplay();
            ScopeObject.DisplayPowerdbm();
            currentLOPValue = ScopeObject.GetAveragePowerdbm();
            ScopeObject.DisplayER();
            currentERValue = ScopeObject.GetEratio();
            procTxPowerData.Add(currentLOPValue);
            procErData.Add(currentERValue);

            dut.ReadTxpADC(out Temp);
            procTxPowerADCData.Add(Temp);

            //if ((currentLOPValue < GlobalTxPowerLL) || (currentLOPValue > GlobalTxPowerUL))   //AP不在FMT规格内，不调
            //{
            //    logoStr += logger.AdapterLogString(4, "AP is not in GlobalSpecs!");
            //    RecordTarget(currentValueIbias, currentValueMod, currentLOPValue, currentERValue);
            //    logger.FlushLogBuffer();
            //    return false;
            //}

#region  adjustAP  OK?
            do
            {
                if ((currentLOPValue >= targetTxPowerLL) && (currentLOPValue <= targetTxPowerUL))
                {
                    ibiasDacTarget = currentValueIbias;
                    isLopok = true;
                }
                else       //调整AP
                {
                    if (currentLOPValue < targetTxPowerLL)       //AP小于下限，增加BiasDAC
                    {
                        int tempValue = (int)((currentValueIbias + stepIbias) >= uperLimitIbias ? uperLimitIbias : (currentValueIbias + stepIbias));
                        currentValueIbias = (UInt32)tempValue;

                        dut.WriteBiasDac(currentValueIbias);
                        procIbiasDACData.Add(currentValueIbias);
                        currentLOPValue = ScopeObject.GetAveragePowerdbm();
                        procTxPowerData.Add(currentLOPValue);
                        dut.ReadTxpADC(out Temp);
                        procTxPowerADCData.Add(Temp);

                        if (currentValueIbias == uperLimitIbias && ((currentLOPValue >= targetTxPowerLL && currentLOPValue <= targetTxPowerUL) || (currentLOPValue > GlobalTxPowerLL && currentLOPValue < GlobalTxPowerUL)))
                        {
                            ibiasDacTarget = currentValueIbias;
                            isLopok = true;
                        }
                    }

                    if (currentLOPValue > targetTxPowerUL)      //AP大于上限，减小BiasDAC
                    {
                        int tempValue = (int)((currentValueIbias - stepIbias) <= lowLimitIbias ? lowLimitIbias : (currentValueIbias - stepIbias));
                        currentValueIbias = (UInt32)tempValue;

                        dut.WriteBiasDac(currentValueIbias);
                        procIbiasDACData.Add(currentValueIbias);
                        currentLOPValue = ScopeObject.GetAveragePowerdbm();
                        procTxPowerData.Add(currentLOPValue);
                        dut.ReadTxpADC(out Temp);
                        procTxPowerADCData.Add(Temp);

                        if (currentValueIbias == lowLimitIbias && ((currentLOPValue >= targetTxPowerLL && currentLOPValue <= targetTxPowerUL) || (currentLOPValue > GlobalTxPowerLL && currentLOPValue < GlobalTxPowerUL)))
                        {
                            ibiasDacTarget = currentValueIbias;
                            isLopok = true;
                        }
                    }
                }
            } while (isLopok == false && currentValueIbias < uperLimitIbias && currentValueIbias > lowLimitIbias);

            if (isLopok == false)
            {
                logoStr += logger.AdapterLogString(4, "AP can't adjust in adjustSpec!");
                RecordTarget(currentValueIbias, currentValueMod, currentLOPValue, currentERValue);
                //ibiasDacTarget = currentValueIbias;
                //imodDacTarget = currentValueMod;
                //dut.ReadTxpADC(out Temp);
                //txpowerAdcTarget = Convert.ToUInt32(Temp);
                //targetLOP = currentLOPValue;
                //targetER = currentERValue;
                logger.FlushLogBuffer();
                return false;
            }
#endregion

            procImodDACData.Add(currentValueMod);
            currentERValue = ScopeObject.GetEratio();
            procErData.Add(currentERValue);

#region  adjustER  OK?
            int count = 0;
            bool satFlag = false;
            do
            {
                if ((currentERValue >= targetERLL) && (currentERValue <= targetERUL))
                {
                    imodDacTarget = currentValueMod;
                    isERok = true;
                }
                else       //调整ER
                {
#region ER小于下限
                    if (currentERValue < targetERLL)       //ER小于下限
                    {
                        satFlag = false;
                        if (count > 1)    //判断是否进入饱和状态
                        {
                            satFlag = checkSAT(procErData);
                        }

                        if (satFlag == false)   //没有进入饱和状态，增加ModDAC
                        {
#region ER没进入饱和状态
                            int tempValue = (int)((currentValueMod + stepImod) >= uperLimitIMod ? uperLimitIMod : (currentValueMod + stepImod));
                            currentValueMod = (UInt32)tempValue;

                            dut.WriteModDac(currentValueMod);
                            procImodDACData.Add(currentValueMod);
                            currentERValue = ScopeObject.GetEratio();
                            procErData.Add(currentERValue);
                            count++;

                            if (currentValueMod == uperLimitIMod && ((currentERValue >= targetERLL && currentERValue <= targetERUL) || (currentERValue > GlobalERLL && currentERValue < GlobalERUL)))
                            {
                                imodDacTarget = currentValueMod;
                                isERok = true;
                            }
                            else if (currentValueMod == uperLimitIMod)   //ModDAC max时，ER仍小于下限，减小BiasDAC
                            {
                                tempValue = (int)((currentValueIbias - stepIbias / 2) <= lowLimitIbias ? lowLimitIbias : (currentValueIbias - stepIbias / 2));
                                currentValueIbias = (UInt32)tempValue;

                                dut.WriteBiasDac(currentValueIbias);
                                procIbiasDACData.Add(currentValueIbias);
                                procImodDACData.Add(currentValueMod);
                                currentLOPValue = ScopeObject.GetAveragePowerdbm();
                                currentERValue = ScopeObject.GetEratio();
                                procTxPowerData.Add(currentLOPValue);
                                procErData.Add(currentERValue);
                                dut.ReadTxpADC(out Temp);
                                procTxPowerADCData.Add(Temp);

                                if ((currentLOPValue < targetTxPowerLL) || (currentLOPValue > targetTxPowerUL))
                                {
                                    RecordTarget(currentValueIbias, currentValueMod, currentLOPValue, currentERValue);
                                    logger.FlushLogBuffer();
                                    return false;
                                }
                                else if (currentValueIbias == lowLimitIbias)
                                {
                                    if ((currentERValue < targetERLL) || (currentERValue > targetERUL))
                                    {
                                        RecordTarget(currentValueIbias, currentValueMod, currentLOPValue, currentERValue);
                                        logger.FlushLogBuffer();
                                        return false;
                                    }
                                }
                            }
#endregion
                        }
                        else    //ER进入饱和状态，减小BiasDAC，ModDAC回到进入饱和状态点
                        {
#region ER进入饱和状态
                            int tempValue = (int)((currentValueIbias - stepIbias * 2) <= lowLimitIbias ? lowLimitIbias : (currentValueIbias - stepIbias * 2));
                            currentValueIbias = (UInt32)tempValue;
                            tempValue = (int)((currentValueMod - stepImod) <= lowLimitIMod ? lowLimitIMod : (currentValueMod - stepImod));
                            currentValueMod = (UInt32)tempValue;

                            dut.WriteBiasDac(currentValueIbias);
                            procIbiasDACData.Add(currentValueIbias);
                            dut.WriteModDac(currentValueMod);
                            procImodDACData.Add(currentValueMod);
                            currentLOPValue = ScopeObject.GetAveragePowerdbm();
                            currentERValue = ScopeObject.GetEratio();
                            procErData.Add(currentERValue);
                            procTxPowerData.Add(currentLOPValue);
                            dut.ReadTxpADC(out Temp);
                            procTxPowerADCData.Add(Temp);

                            do
                            {
                                if (currentLOPValue < targetTxPowerLL)    //减小BiasDAC，AP出下限，小幅度增加BiasDAC
                                {
                                    tempValue = (int)((currentValueIbias + stepIbias / 2) >= uperLimitIbias ? uperLimitIbias : (currentValueIbias + stepIbias / 2));
                                    currentValueIbias = (UInt32)tempValue;

                                    dut.WriteBiasDac(currentValueIbias);
                                    procIbiasDACData.Add(currentValueIbias);
                                    procImodDACData.Add(currentValueMod);
                                    currentLOPValue = ScopeObject.GetAveragePowerdbm();
                                    currentERValue = ScopeObject.GetEratio();
                                    procErData.Add(currentERValue);
                                    procTxPowerData.Add(currentLOPValue);
                                    dut.ReadTxpADC(out Temp);
                                    procTxPowerADCData.Add(Temp);

                                    if (currentValueIbias == uperLimitIbias && (currentLOPValue < targetTxPowerLL || currentLOPValue > targetTxPowerUL))
                                    {
                                        RecordTarget(currentValueIbias, currentValueMod, currentLOPValue, currentERValue);
                                        logger.FlushLogBuffer();
                                        return false;
                                    }
                                }
                                else if (currentLOPValue > targetTxPowerUL)
                                {
                                    RecordTarget(currentValueIbias, currentValueMod, currentLOPValue, currentERValue);
                                    logger.FlushLogBuffer();
                                    return false;
                                }
                            }
                            while (currentLOPValue < targetTxPowerLL || currentLOPValue > targetTxPowerUL);

                            count = 0;

                            if (currentValueIbias == lowLimitIbias && currentValueMod == lowLimitIMod && ((currentLOPValue < targetTxPowerLL || currentLOPValue > targetTxPowerUL) || (currentERValue < targetERLL || currentERValue > targetERUL)))
                            {
                                RecordTarget(currentValueIbias, currentValueMod, currentLOPValue, currentERValue);
                                logger.FlushLogBuffer();
                                return false;
                            }
#endregion
                        }
                    }
#endregion

#region ER大于上限
                    if (currentERValue > targetERUL)       //ER大于上限
                    {
                        satFlag = false;
                        if (count > 1)    //判断是否进入饱和状态
                        {
                            satFlag = checkSAT(procErData);
                        }

                        if (satFlag == false)   //没有进入饱和状态，减小ModDAC
                        {
#region ER没进入饱和状态
                            int tempValue = (int)((currentValueMod - stepImod) <= lowLimitIMod ? lowLimitIMod : (currentValueMod - stepImod));
                            currentValueMod = (UInt32)tempValue;

                            dut.WriteModDac(currentValueMod);
                            procImodDACData.Add(currentValueMod);
                            currentERValue = ScopeObject.GetEratio();
                            procErData.Add(currentERValue);
                            count++;

                            if (currentValueMod == lowLimitIMod && ((currentERValue >= targetERLL && currentERValue <= targetERUL) || (currentERValue > GlobalERLL && currentERValue < GlobalERUL)))
                            {
                                imodDacTarget = currentValueMod;
                                isERok = true;
                            }
                            else if (currentValueMod == lowLimitIMod)   //ModDAC min时，ER仍大于下限，增大BiasDAC
                            {
                                tempValue = (int)((currentValueIbias + stepIbias / 2) >= uperLimitIbias ? uperLimitIbias : (currentValueIbias + stepIbias / 2));
                                currentValueIbias = (UInt32)tempValue;

                                dut.WriteBiasDac(currentValueIbias);
                                procIbiasDACData.Add(currentValueIbias);
                                procImodDACData.Add(currentValueMod);
                                currentLOPValue = ScopeObject.GetAveragePowerdbm();
                                currentERValue = ScopeObject.GetEratio();
                                procTxPowerData.Add(currentLOPValue);
                                procErData.Add(currentERValue);
                                dut.ReadTxpADC(out Temp);
                                procTxPowerADCData.Add(Temp);

                                if ((currentLOPValue < targetTxPowerLL) || (currentLOPValue > targetTxPowerUL))
                                {
                                    RecordTarget(currentValueIbias, currentValueMod, currentLOPValue, currentERValue);
                                    logger.FlushLogBuffer();
                                    return false;
                                }
                                else if (currentValueIbias == uperLimitIbias)
                                {
                                    if ((currentERValue < targetERLL) || (currentERValue > targetERUL))
                                    {
                                        RecordTarget(currentValueIbias, currentValueMod, currentLOPValue, currentERValue);
                                        logger.FlushLogBuffer();
                                        return false;
                                    }
                                }
                            }
#endregion
                        }
                        else    //ER进入饱和状态，增加BiasDAC，ModDAC回到进入饱和状态点
                        {
#region ER进入饱和状态
                            int tempValue = (int)((currentValueIbias + stepIbias * 2) >= uperLimitIbias ? uperLimitIbias : (currentValueIbias + stepIbias * 2));
                            currentValueIbias = (UInt32)tempValue;
                            tempValue = (int)((currentValueMod + stepImod * 2) >= uperLimitIMod ? uperLimitIMod : (currentValueMod + stepImod * 2));
                            currentValueMod = (UInt32)tempValue;

                            dut.WriteBiasDac(currentValueIbias);
                            procIbiasDACData.Add(currentValueIbias);
                            dut.WriteModDac(currentValueMod);
                            procImodDACData.Add(currentValueMod);
                            currentLOPValue = ScopeObject.GetAveragePowerdbm();
                            currentERValue = ScopeObject.GetEratio();
                            procErData.Add(currentERValue);
                            procTxPowerData.Add(currentLOPValue);
                            dut.ReadTxpADC(out Temp);
                            procTxPowerADCData.Add(Temp);

                            do
                            {
                                if (currentLOPValue > targetTxPowerUL)    //增加BiasDAC，AP出上限，小幅度减小BiasDAC
                                {
                                    tempValue = (int)((currentValueIbias - stepIbias / 2) <= lowLimitIbias ? lowLimitIbias : (currentValueIbias - stepIbias / 2));
                                    currentValueIbias = (UInt32)tempValue;

                                    dut.WriteBiasDac(currentValueIbias);
                                    procIbiasDACData.Add(currentValueIbias);
                                    procImodDACData.Add(currentValueMod);
                                    currentLOPValue = ScopeObject.GetAveragePowerdbm();
                                    currentERValue = ScopeObject.GetEratio();
                                    procErData.Add(currentERValue);
                                    procTxPowerData.Add(currentLOPValue);
                                    dut.ReadTxpADC(out Temp);
                                    procTxPowerADCData.Add(Temp);

                                    if (currentValueIbias == lowLimitIbias && (currentLOPValue < targetTxPowerLL || currentLOPValue > targetTxPowerUL))
                                    {
                                        RecordTarget(currentValueIbias, currentValueMod, currentLOPValue, currentERValue);
                                        logger.FlushLogBuffer();
                                        return false;
                                    }
                                }
                                else if (currentLOPValue < targetTxPowerLL)
                                {
                                    RecordTarget(currentValueIbias, currentValueMod, currentLOPValue, currentERValue);
                                    logger.FlushLogBuffer();
                                    return false;
                                }
                            }
                            while (currentLOPValue < targetTxPowerLL || currentLOPValue > targetTxPowerUL);

                            count = 0;

                            if (currentValueIbias == uperLimitIbias && currentValueMod == uperLimitIMod && ((currentLOPValue < targetTxPowerLL || currentLOPValue > targetTxPowerUL) || (currentERValue < targetERLL || currentERValue > targetERUL)))
                            {
                                RecordTarget(currentValueIbias, currentValueMod, currentLOPValue, currentERValue);
                                logger.FlushLogBuffer();
                                return false;
                            }
#endregion
                        }
                    }
#endregion
                }
            } while (isERok == false);
#endregion

            if (isLopok && isERok)
            {
                ibiasDacTarget = currentValueIbias;
                imodDacTarget = currentValueMod;
                dut.ReadTxpADC(out Temp);
                txpowerAdcTarget = Convert.ToUInt32(Temp);
                procTxPowerADCData.Add(Temp);

                targetLOP = currentLOPValue;
                targetER = currentERValue;

                dut.StoreBiasDac(ibiasDacTarget);
                dut.StoreModDac(imodDacTarget);

                return true;
            }
            else
            {
                RecordTarget(currentValueIbias, currentValueMod, currentLOPValue, currentERValue);
                return false;
            }
        }

        protected bool checkSAT(ArrayList procData)    //连续三点差值在0.2内，则为饱和状态
        {
            int length = procData.Count;

            if (Math.Abs(Convert.ToDouble(procData[length - 3]) - Convert.ToDouble(procData[length - 2])) < 0.2 && Math.Abs(Convert.ToDouble(procData[length - 2]) - Convert.ToDouble(procData[length - 1])) < 0.2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected void RecordTarget(uint biasDac, uint modDac, double power, double ER)
        {
            UInt16 Temp;
            ibiasDacTarget = biasDac;
            imodDacTarget = modDac;
            dut.ReadTxpADC(out Temp);
            txpowerAdcTarget = Convert.ToUInt32(Temp);

            targetLOP = power;
            targetER = ER;
        }
#endregion

#endregion
#endregion
        
    }
}

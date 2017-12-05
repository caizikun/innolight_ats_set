using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Collections;
using Ivi.Visa.Interop;
namespace ATS_Framework
{
    public class TestTransfer : TestModelBase
    {
#region Attribute  
        private double Ber=2;
        private double DmiRxPower, DmiTxPower;
        private ArrayList inPutParametersNameArray = new ArrayList();
        private Powersupply pPS;
        private ErrorDetector pED;
       
        private bool flagSingleTest = false;
        private bool flagReadTxpowerDmi = false;
        private bool flagReadRxpowerDmi = false;
        private DataTable DtTestData;
        private double LastTemp = -100;
#endregion

#region Method
        public TestTransfer(DUT inPuDut)
        {
            
            logoStr = null;
            dut = inPuDut;
            DtTestData = new DataTable();

            DtTestData.Columns.Add("Temp");                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      
            DtTestData.Columns.Add("Channel");
            DtTestData.Columns.Add("Vcc");
            DtTestData.Columns.Add("ErrorRate");

            DtTestData.Columns.Add("DmiRxPower");
            DtTestData.Columns.Add("DmiTxPower");

            inPutParametersNameArray.Clear();
          
            inPutParametersNameArray.Add("SINGLEORMULTI"); 
          
           
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
        protected override bool StartTest()
        {
            Ber = 1;
            
            logoStr = "";
           if (!Test())
           {
               OutPutandFlushLog();
               return false;
              
           }
            else
           {
               OutPutandFlushLog();
               return true;
           }
            
        }
        private bool SingleChannelTest()
        {
            Ber = pED.RapidErrorRate(0);
            if (Ber >= 1)
            {
                System.Threading.Thread.Sleep(2000);
                Ber = pED.RapidErrorRate(0);
            }
          if (flagReadTxpowerDmi)   DmiTxPower = dut.ReadDmiTxp();
          if (flagReadRxpowerDmi) DmiRxPower = dut.ReadDmiRxp();
            Log.SaveLogToTxt("Ber= " + Ber.ToString());
            
            return true;
                
            
        }
        private bool MultChannelTest()
        {
            double  dDmiRxPower=-100;
            double dDmiTxPower=-100;
           
            double[]ErrorRateArray=new double[4];
            ErrorRateArray = pED.RapidErrorRate_AllCH();// 
            Ber = ErrorRateArray[GlobalParameters.CurrentChannel-1];
            Log.SaveLogToTxt("Ber= " + Ber.ToString());

            for (int i = 0; i < pED.totalChannels;i++ )
            {
                dut.ChangeChannel((i + 1).ToString());

                DataRow dr = DtTestData.NewRow();
                dr["Temp"] = GlobalParameters.CurrentTemp;
                dr["Vcc"] = GlobalParameters.CurrentVcc;
                dr["Channel"] = i+1;
                dr["ErrorRate"] = ErrorRateArray[i];

                if (flagReadTxpowerDmi)
                {
                    dDmiTxPower = dut.ReadDmiTxp();
                }
                else
                {
                    dDmiTxPower = 0;
                }
               
                if (flagReadRxpowerDmi)
                {
                    dDmiRxPower = dut.ReadDmiRxp();
                }
                else
                {
                    dDmiRxPower = 0;
                }
                dr["DmiRxPower"] = dDmiRxPower;
                dr["DmiTxPower"] = dDmiTxPower;

                if (GlobalParameters.CurrentChannel==i+1)
                {

                    DmiRxPower = dDmiRxPower;
                    DmiTxPower = dDmiTxPower;
                }

                DtTestData.Rows.Add(dr);
            }

            dut.ChangeChannel(GlobalParameters.CurrentChannel.ToString());

            return true;
        }
        public override bool Test()
        {
            if (AnalysisInputParameters(inputParameters) == false)
            {
                return false;
            }
            if (!CloseandOpenAPC(Convert.ToByte(APCMODE.IBAISandIMODON))) return false;//开启APC

            if (ReadyData())
            {
                return true;//已经填充好了数据,后面不用测试

            }
            else// 没提前准备好数据,需要实际测量
            {

                Log.SaveLogToTxt("Step3...SetAttenValue");
                Log.SaveLogToTxt("Step4...AutoAlaign");
                bool isAutoAlaign = pED.AutoAlaign(true);

                if (isAutoAlaign)
                {
                    Log.SaveLogToTxt(isAutoAlaign.ToString());
                }
                else
                {
                    Log.SaveLogToTxt(isAutoAlaign.ToString());
                    Ber = 1;
                    
                    logoStr = "";
                    return isAutoAlaign;
                }
                

                if (flagSingleTest)
                {
                    SingleChannelTest();
                }
                else
                {
                    MultChannelTest();
                }
            }
            return true;
        }
        private bool ReadyData()
        {
            if (flagSingleTest)
            {
                return false;
            }
            else
            {
                
                if (GlobalParameters.CurrentTemp != LastTemp)
                {
                    DtTestData.Clear();
                    LastTemp = GlobalParameters.CurrentTemp;
                }
                DataRow[] drArray;

                string SelectCondition = "Temp=" + GlobalParameters.CurrentTemp + " and Vcc=" + GlobalParameters.CurrentVcc + " and Channel=" + GlobalParameters.CurrentChannel;
                
                drArray = DtTestData.Select(SelectCondition);

                if (drArray.Length == 0)
                {
                    Ber = 1;
                    DmiRxPower = -100;
                    DmiTxPower = -100;
                    return false;
                }
                if (drArray.Length == 1)
                {
                    Ber = Convert.ToDouble(drArray[0]["ErrorRate"]);
               
                    DmiRxPower = Convert.ToDouble(drArray[0]["DmiRxPower"]);
                    DmiTxPower = Convert.ToDouble(drArray[0]["DmiTxPower"]);
                    
                }
              

                return true;
            }
        }
        override protected bool PostTest()
        {//note: for inherited class, they need to call base function first,
            //then do other post-test process task
            bool flag = DeassembleEquipment();

            for (int i = 0; i < selectedEquipList.Count; i++)
            {
                selectedEquipList.Values[i].DecreaseReferencedTimes();

            }
            return flag;
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
            pPS = null;
          
            pED = null;
            selectedEquipList.Clear();
            if (aEquipList.Count == 0)
            {
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
                    if (tempKeys[i].ToUpper().Contains("ERRORDETE"))
                   
                    {
                        selectedEquipList.Add("ERRORDETE", tempValues[i]);
                        pED =(ErrorDetector) tempValues[i];

                    }
                  
                    if (tempKeys[i].ToUpper().Contains("POWERSUPPLY"))
                    {
                        selectedEquipList.Add("POWERSUPPLY", tempValues[i]);
                        pPS = (Powersupply)tempValues[i];
                    }

                }
                if (pPS != null  && pED!= null)
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
                }
                else
                {
                    isOK = false;
                }
                return isOK;
            }
        }
        protected override bool AnalysisOutputParameters(TestModeEquipmentParameters[] InformationList)
        {
        
            try
            {
                int ArrayLength = 1;
                if (flagReadTxpowerDmi)
                {
                    ArrayLength += 1;
                }
                if (flagReadRxpowerDmi)
                {
                    ArrayLength += 1;
                }
                outputParameters = new TestModeEquipmentParameters[ArrayLength];
                outputParameters[0].FiledName = "Tr_ErrorRate(%)";
                Ber = Algorithm.ISNaNorIfinity(Ber);
                outputParameters[0].DefaultValue = Ber.ToString().Trim();

               

                if (flagReadRxpowerDmi)
                { 
                    DmiRxPower = Algorithm.ISNaNorIfinity(DmiRxPower);
                    outputParameters[1].DefaultValue = DmiRxPower.ToString().Trim();
                    outputParameters[1].FiledName = "DmiRxPower";
                   
                }


                if (flagReadTxpowerDmi )
                {
                    if (flagReadRxpowerDmi)
                    {

                        outputParameters[2].FiledName = "DmiTxPower";
                        DmiTxPower = Algorithm.ISNaNorIfinity(DmiTxPower);
                        outputParameters[2].DefaultValue = DmiTxPower.ToString().Trim();
                    }
                    else
                    {
                        outputParameters[1].FiledName = "DmiTxPower";
                        DmiTxPower = Algorithm.ISNaNorIfinity(DmiTxPower);
                        outputParameters[1].DefaultValue = DmiTxPower.ToString().Trim();
                    }
                }

                return true;
            }
            catch (InnoExCeption ex)//from driver
            {
                //Log.SaveLogToTxt(ex.ID + ": " + ex.Message + "\r\n" + ex.StackTrace);
                exceptionList.Add(ex);
                return false;
            }
            catch (Exception error)//from itself
            {
                //one way: deal this exception itself
                InnoExCeption ex = new InnoExCeption(ExceptionDictionary.Code._0x02F04, error.StackTrace);
                //Log.SaveLogToTxt(ex.ID + ": " + ex.Message + "\r\n" + ex.StackTrace);
                exceptionList.Add(ex);
                return false;
                //the other way is: should throw exception, rather than the above three code. see below:
                //throw new InnoExCeption(ExceptionDictionary.Code._0x02F04, error.StackTrace); 
            }
           
           
        }
        protected override bool AnalysisInputParameters(TestModeEquipmentParameters[] InformationList)
        {
                
            Log.SaveLogToTxt("Step1...Check InputParameters");

            if (InformationList.Length < inPutParametersNameArray.Count)
            {
                Log.SaveLogToTxt("InputParameters are not enough!");
                return false;
            }
            else
            {
                int index = -1;
                bool isParametersComplete = false;
                for (byte i = 0; i < inPutParametersNameArray.Count; i++)
                {
                    if (Algorithm.FindFileName(InformationList, inPutParametersNameArray[i].ToString(), out index) == false)
                    {
                        Log.SaveLogToTxt(inPutParametersNameArray[i].ToString() + "is not exist");
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
                   
                        if (Algorithm.FindFileName(InformationList, "SINGLEORMULTI", out index))
                        {
                            flagSingleTest = Convert.ToBoolean(InformationList[index].DefaultValue);
                        }
                        if (Algorithm.FindFileName(InformationList, "FLAGREADRXPOWERDMI", out index))
                        {
                            flagReadRxpowerDmi = Convert.ToBoolean(InformationList[index].DefaultValue);
                        }
                        if (Algorithm.FindFileName(InformationList, "FLAGREADTXPOWERDMI", out index))
                        {
                            flagReadTxpowerDmi = Convert.ToBoolean(InformationList[index].DefaultValue);
                        }
                    

                }
                Log.SaveLogToTxt("OK!");
                return true;
            }
        
        }
       
        private void OutPutandFlushLog()
        {
            try
            {
                AnalysisOutputParameters(outputParameters);
                
            }
            catch (InnoExCeption ex)//from driver
            {
                //Log.SaveLogToTxt(ex.ID + ": " + ex.Message + "\r\n" + ex.StackTrace);
                exceptionList.Add(ex);
            }
            catch (Exception error)//from itself
            {
                //one way: deal this exception itself
                InnoExCeption ex = new InnoExCeption(ExceptionDictionary.Code._0x02FFF, error.StackTrace);
                //Log.SaveLogToTxt(ex.ID + ": " + ex.Message + "\r\n" + ex.StackTrace);
                exceptionList.Add(ex);
                //the other way is: should throw exception, rather than the above three code. see below:
                //throw new InnoExCeption(ExceptionDictionary.Code._0x02FFF, error.StackTrace); 
            }
        }

        public override List<InnoExCeption> GetException()
        {
            return base.GetException();
        }
#endregion
       
      
    }
}

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
    public struct TestTxEyeRinOMAStruct
    {
        public string PRBS;
        public double RinTargetPower;
    }
    public class TestTxEyeRinOMA : TestModelBase
    {
        private TestTxEyeRinOMAStruct pTestTxEyeRinOMAStruct=new TestTxEyeRinOMAStruct();
        private DataTable TestDatad = new DataTable();

        private ArrayList inPutParametersNameArray = new ArrayList();

        private Double TxRinOMA = -1;
        private PPG pPPG;
        private Scope pScope;
        private Attennuator pAtt;
        private PowerMeter pPowerMeter;
        private OpticalSwitch pOpticalSwitch;

       // Algorithm Algorithm = new Algorithm();

        public TestTxEyeRinOMA(DUT inPuDut)
        {
            
            dut = inPuDut;
           logoStr = null;

           TestDatad.Columns.Add("ItemName");
           TestDatad.Columns.Add("ItemValue");

           inPutParametersNameArray.Clear();
           inPutParametersNameArray.Add("PRBSLength");
           inPutParametersNameArray.Add("TargetPower");    
       }


        override protected bool CheckEquipmentReadiness()
        {
            for (int i = 0; i < selectedEquipList.Count; i++)
            {
                if (!selectedEquipList.Values[i].bReady) return false;

            }

            return true;
        }

     


        public override bool SelectEquipment(EquipmentList aEquipList)
        {
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

                    if (tempKeys[i].ToUpper().Contains("TX_ATTEN"))
                    {
                        selectedEquipList.Add("ATTEN_TX", tempValues[i]);
                        isOK = true;
                    }
                  
                    if (tempKeys[i].ToUpper().Contains("POWERMETER"))
                    {
                        selectedEquipList.Add("POWERMETER", tempValues[i]);
                        isOK = true;
                    }
                    if (tempKeys[i].ToUpper().Contains("SCOPE"))
                    {
                        selectedEquipList.Add("SCOPE", tempValues[i]);
                        isOK = true;
                    }
                    if (tempKeys[i].ToUpper().Contains("TX_PPG"))
                    {
                        selectedEquipList.Add("PPG", tempValues[i]);
                        isOK = true;
                    }
                    if (tempKeys[i].ToUpper().Contains("NA_OPTICALSWITCH"))
                    {
                        tempValues[i].CheckEquipmentRole(2, GlobalParameters.CurrentChannel);
                        pOpticalSwitch = (OpticalSwitch)selectedEquipList["NA_OPTICALSWITCH"];
                    }
                }
               // tempps = (Powersupply)selectedEquipList["POWERSUPPLY"];
                pAtt = (Attennuator)selectedEquipList["ATTEN_TX"];
                pPowerMeter = (PowerMeter)selectedEquipList["POWERMETER"];
                pPPG = (PPG)selectedEquipList["PPG"];
                pScope = (Scope)selectedEquipList["SCOPE"];

               // pOpticalSwitch = (OpticalSwitch)selectedEquipList["NA_OPTICALSWITCH"];

                if (pAtt != null && pPowerMeter != null && pPPG != null && pScope!=null)
                {
                    isOK = true;

                }
                else
                {
                    if (pAtt == null)
                    {
                        Log.SaveLogToTxt("ATTEN =NULL");
                    }
                    if (pPowerMeter == null)
                    {
                        Log.SaveLogToTxt("pPowerMeter =NULL");
                    }
                    if (pPPG == null)
                    {
                        Log.SaveLogToTxt("pPPG =NULL");
                    }
                    if (pScope == null)
                    {
                        Log.SaveLogToTxt("pScope =NULL");
                    }





                    OutPutandFlushLog();
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


        protected override bool StartTest()
        {


            TestDatad.Clear();

            
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


        public override bool Test()
        {





            if (AnalysisInputParameters(inputParameters) == false)
            {
                return false;
            }
            if (!CloseandOpenAPC(Convert.ToByte(APCMODE.IBAISandIMODON))) return false;//开启APC

            if (!ConfigureEquipment()) return false;


         

            pScope.AutoScale(1);
            Thread.Sleep(10000);
            TxRinOMA=pScope.GetTxRinOmA();

         
           
            for (int i=0;i <3;i ++)
            {
                pScope.AutoScale(1);
                Thread.Sleep(10000);
                TxRinOMA = pScope.GetTxRinOmA();
                if (TxRinOMA<-10)
                {
                    break;
                }
                if (TxRinOMA>-101&& i>1)
                {
                    Log.SaveLogToTxt("Can't GetRinOma");
                    TxRinOMA = -1;
                }
            }



            fitDataTable("TxRinOMA(db/hz)", TxRinOMA.ToString());

            return true;
            
        }
        protected override bool PrepareTest()
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
       
         protected bool ConfigureEquipment()
        {
            if (!pPPG.ConfigurePrbsLength(Convert.ToByte(pTestTxEyeRinOMAStruct.PRBS))) return false;

            double CurrentTxDmi = dut.ReadDmiTxp();

            if (!SetTargetPower(pTestTxEyeRinOMAStruct.RinTargetPower + CurrentTxDmi)) return false;


            pScope.SetMode(2, 1);//设置Jitter Mode

            return true;
        }
        private bool SetTargetPower(double TargerPower)
        {


          //  double[] PowerArray = new double[] { -30, -43, -41, -40.2 };


            double TempValue;
            double StartPower;

            pAtt.SetAttnValue(0,1);

            StartPower = pPowerMeter.ReadPower();

            pAtt.SetAttnValue((StartPower-TargerPower), 1);

            double AttStep=0;

            TempValue = pPowerMeter.ReadPower();
            int i = 0;
            while (Math.Abs(TargerPower-TempValue)>0.5)
            {
                if ( TempValue -TargerPower> 1)
                {
                    AttStep = 1;
                }
                else if (TempValue - TargerPower <- 1)
                {
                    AttStep = -1;
                }
                else if (TempValue - TargerPower > 0.2)
                {
                    AttStep = 0.2;
                }
                else if (TempValue - TargerPower < -0.2)
                {
                    AttStep = -0.2;
                }

                pAtt.AdjustAttnValue(AttStep, 1);
                TempValue = pPowerMeter.ReadPower();
                //----------------
              //  TempValue = PowerArray[i];
                //----------------
                i++;
                if (i>20)
                {
                    return false;
                }
            }



            return true;

        }
        private bool fitDataTable(string ItemName, string ItemValue)
        {
            try
            {
                DataRow dr = TestDatad.NewRow();

                dr["ItemName"] = ItemName;
                dr["ItemValue"] = ItemValue;
                TestDatad.Rows.Add(dr);

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
                InnoExCeption ex = new InnoExCeption(ExceptionDictionary.Code._0xFFFFF, error.StackTrace);
                //Log.SaveLogToTxt(ex.ID + ": " + ex.Message + "\r\n" + ex.StackTrace);
                exceptionList.Add(ex);
                return false;
                //the other way is: should throw exception, rather than the above three code. see below:
                //throw new InnoExCeption(ExceptionDictionary.Code._0xFFFFF, error.StackTrace); 
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
      
        protected override bool AnalysisOutputParameters(TestModeEquipmentParameters[] InformationList)
        {

            try
            {
                //outputParameters = new TestModeEquipmentParameters[1];
                //outputParameters[0].FiledName = "Wavelength(nm)";
                //outputParameters[0].DefaultValue = Wavelength.ToString();

                outputParameters = new TestModeEquipmentParameters[TestDatad.Rows.Count];

                for (int i = 0; i < TestDatad.Rows.Count; i++)
                {
                    outputParameters[i].FiledName = TestDatad.Rows[i]["ItemName"].ToString();
                    outputParameters[i].DefaultValue = TestDatad.Rows[i]["ItemValue"].ToString();
                }


              //  outputParameters
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
                    if (Algorithm.FindFileName(InformationList, "PRBSLength", out index))
                    {

                       pTestTxEyeRinOMAStruct.PRBS = InformationList[index].DefaultValue.ToString();
                    }
                    else
                    {
                        return false;
                    }
                    if (Algorithm.FindFileName(InformationList, "TargetPower", out index))
                    {

                        pTestTxEyeRinOMAStruct.RinTargetPower = Convert.ToDouble(InformationList[index].DefaultValue);
                    }
                    else
                    {
                        return false;
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

                pPPG.RecallPrbsLength();
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


    }
}

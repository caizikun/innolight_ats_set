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
        
        private ArrayList inPutParametersNameArray = new ArrayList();
        private Powersupply pPS;
        private ErrorDetector pED;
       // private Attennuator pATT;
        private bool flagSingleTest = false;
        private DataTable DtTestData;
#endregion

#region Method
        public TestTransfer(DUT inPuDut, logManager logmanager)
        {
            logger = logmanager;
            logoStr = null;
            dut = inPuDut;
            DtTestData = new DataTable();

            DtTestData.Columns.Add("Temp");                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      
            DtTestData.Columns.Add("Channel");
            DtTestData.Columns.Add("Vcc");
            DtTestData.Columns.Add("ErrorRate");

            inPutParametersNameArray.Clear();
            //inPutParametersNameArray.Add("BER"); 
             inPutParametersNameArray.Add("IsSingleCHTest"); 
            //IsSingleCHTest
           // Array.Sort()
           
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
            logger.FlushLogBuffer();
            logoStr = "";
           if (!Test())//||AnalysisOutputParameters(outputParameters))
           {
               AnalysisOutputParameters(outputParameters);
               return false;
              
           }
            else
           {   AnalysisOutputParameters(outputParameters);
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
            logoStr += logger.AdapterLogString(1, "Ber= " + Ber.ToString());
            
            return true;
                
            
        }

        private bool MultChannelTest()
        {
            double[]ErrorRateArray=new double[4];
            ErrorRateArray=  pED.RapidErrorRate_4CH();

            for (int i = 0; i < 4;i++ )
            {
                logoStr += logger.AdapterLogString(1, "Ber= "+(i+1) + ErrorRateArray[i].ToString());
            }
            logoStr += logger.AdapterLogString(1, "Ber= " + Ber.ToString());

            Ber = ErrorRateArray[GlobalParameters.CurrentChannel-1];
            logoStr += logger.AdapterLogString(1, "Ber= " + Ber.ToString());

            for (int i = 0; i < 4;i++ )
            {
                DataRow dr = DtTestData.NewRow();
                dr["Temp"] = GlobalParameters.CurrentTemp;
                dr["Vcc"] = GlobalParameters.CurrentVcc;
                dr["Channel"] = i+1;
                dr["ErrorRate"] = ErrorRateArray[i];
                DtTestData.Rows.Add(dr);
            }


            return true;
        }
        protected override bool Test()
        {
            if (AnalysisInputParameters(inputParameters) == false)
            {
                return false;
            }
            if (!APCSwith(true)) return false;//开启APC

            if (ReadyData())
            {
                return true;//已经填充好了数据,后面不用测试

            }
            else// 没提前准备好数据,需要实际测量
            {


                logoStr += logger.AdapterLogString(0, "Step3...SetAttenValue");
               // SetAttenValue(pATT, 0);
               // SetAttenValue()
                logoStr += logger.AdapterLogString(0, "Step4...AutoAlaign");
                bool isAutoAlaign = pED.AutoAlaign(true);

                if (isAutoAlaign)
                {
                    logoStr += logger.AdapterLogString(1, isAutoAlaign.ToString());
                }
                else
                {
                    logoStr += logger.AdapterLogString(4, isAutoAlaign.ToString());
                    Ber = 1;
                    logger.FlushLogBuffer();
                    logoStr = "";
                    return isAutoAlaign;
                }
                logger.FlushLogBuffer();

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
                if (GlobalParameters.CurrentChannel==1)
                {
                    DtTestData.Clear();
                }

                DataRow[] drArray;

                string SelectCondition = "Temp=" + GlobalParameters.CurrentTemp + " and Vcc=" + GlobalParameters.CurrentVcc + " and Channel=" + GlobalParameters.CurrentChannel;
                drArray = DtTestData.Select(SelectCondition);
                //if (drArray.Length == 0)
                //{
                //    return false;
                //}
                if (drArray.Length == 1)
                {
                    Ber = Convert.ToDouble(drArray[0]["ErrorRate"]);
                    logger.AdapterLogString(0, "");
                }
                else
                {
                    return false;
                }

                //SelectCondition = "Temp=" + GlobalParameters.CurrentTemp + "and Vcc=" + GlobalParameters.CurrentVcc;
                //drArray = DtTestData.Select(SelectCondition);

                //if (drArray.Length==GlobalParameters.TotalChCount)
                //{
                //    DtTestData.Clear();
                //}

                return true;
            }
        }
        private bool APCSwith(bool OpenSwith)
        {
            byte apcStatus = 0;
            dut.APCStatus(out  apcStatus);
            if (GlobalParameters.ApcStyle == 0)
            {
                if (apcStatus != 0x11)
                {
                    logoStr += logger.AdapterLogString(0, "Step2...Start close apc");
                    dut.APCON(0x11);
                    logoStr += logger.AdapterLogString(0, "Power off");
                    pPS.Switch(false, 1);
                    logoStr += logger.AdapterLogString(0, "Power on");
                    pPS.Switch(true, 1);
                    bool isclosed = dut.APCStatus(out  apcStatus);
                    if (apcStatus == 0x11)
                    {
                        logoStr += logger.AdapterLogString(1, "APC ON");

                    }
                    else
                    {
                        logoStr += logger.AdapterLogString(3, "APC NOT ON");
                        return false;

                    }
                }
            }
            else if (GlobalParameters.ApcStyle == 1)
            {
                if (apcStatus != 0x11)
                {
                    logoStr += logger.AdapterLogString(0, "Step2...Start close apc");
                    dut.APCON(0x11);
                    logoStr += logger.AdapterLogString(0, "Power off");
                    pPS.Switch(false, 1);
                    logoStr += logger.AdapterLogString(0, "Power on");
                    pPS.Switch(true, 1);
                    bool isclosed = dut.APCStatus(out  apcStatus);
                    if (apcStatus == 0x11)
                    {
                        logoStr += logger.AdapterLogString(1, "APC ON");

                    }
                    else
                    {
                        logoStr += logger.AdapterLogString(3, "APC ON Error");
                        return false;
                    }
                }

            }

            return true;
        }
        protected void SetAttenValue(double InputPower)
        {
            //for (int i = 0; i < 4;i++ )
            //{
            //    pATT.ChangeChannel((i + 1).ToString());
            //    pATT.AttnValue(InputPower.ToString(), 1);
            //}
           

           // tempAtt.AttnValue(attValue.ToString(),0);

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
                if (!selectedEquipList.Values[i].Switch(true)) return false;
            }
            dut.FullFunctionEnable();
            return true;
        }
        public override bool SelectEquipment(EquipmentList aEquipList)
        {
            pPS = null;
           // pATT = null;
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
                    //if (tempKeys[i].ToUpper().Contains("ATTEN"))
                    //{
                    //    selectedEquipList.Add("ATTEN", tempValues[i]);
                    //    pATT = (Attennuator)tempValues[i];
                    //}
                    if (tempKeys[i].ToUpper().Contains("POWERSUPPLY"))
                    {
                        selectedEquipList.Add("POWERSUPPLY", tempValues[i]);
                        pPS = (Powersupply)tempValues[i];
                    }

                }
                if (pPS != null && pED!= null)
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
            
            if (InformationList.Length == 0)//InformationList is null
            {
               
                return false;
            }
            else//  InformationList is not null
            {
                int index = -1;
                for (byte i = 0; i < InformationList.Length; i++)
                {

                    if (algorithm.FindFileName(InformationList, "BER", out index))
                    {
                        Ber = algorithm.ISNaNorIfinity(Ber);
                        InformationList[index].DefaultValue = Ber.ToString().Trim();
                      
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
                        logoStr += logger.AdapterLogString(4, inPutParametersNameArray[i].ToString() + "is not exist");
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

                        if (algorithm.FindFileName(InformationList, "ISSINGLECHTEST", out index))
                        {
                            flagSingleTest = Convert.ToBoolean(InformationList[index].DefaultValue);
                        }

                    }

                }
                logoStr += logger.AdapterLogString(1, "OK!");
                return true;
            }
        
        }
      
#endregion
       
      
    }
}

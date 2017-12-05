using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ATS_Framework;
using System.Threading;
using System.Data;
using System.Collections;

namespace ATS_Framework
{

    public class SetBiasAdcOffset: TestModelBase
    {       
        UInt16[] biasADCs;   

        private Powersupply supply;

        private bool result = true;

        public SetBiasAdcOffset(DUT inDut)
        {
            
            dut = inDut;
        }

        protected override bool CheckEquipmentReadiness()
        {
            for (int i = 0; i < selectedEquipList.Count; i++)
            {
                if (!selectedEquipList.Values[i].bReady)
                {
                    return false;
                }
            }
            return true;
        }

        public override bool SelectEquipment(EquipmentList eqList)
        {
            selectedEquipList.Clear();
            if (eqList.Count == 0)
            {
                selectedEquipList.Add("DUT", dut);
                return false;
            }
            else
            {
                bool isOK = false;
                selectedEquipList.Clear();
                IList<string> tempKeys = eqList.Keys;
                IList<EquipmentBase> tempValues = eqList.Values;
                for (byte i = 0; i < eqList.Count; i++)
                {  
                    if (tempKeys[i].ToUpper().Contains("POWERSUPPLY"))
                    {
                        selectedEquipList.Add("POWERSUPPLY", tempValues[i]);
                    }

                    if (tempKeys[i].ToUpper().Contains("NA_OPTICALSWITCH"))
                    {
                        tempValues[i].CheckEquipmentRole(1, GlobalParameters.CurrentChannel);
                    }
                }

                supply = (Powersupply)selectedEquipList["POWERSUPPLY"];

                if (supply != null)
                {
                    isOK = true;
                }
                else
                {                   
                    if (supply == null)
                    {
                        Log.SaveLogToTxt("supply =NULL");
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

        protected override bool StartTest()
        {           
            
            logoStr = "";
            if (!Test())
            {
                //OutPutandFlushLog();
                return false;
            }
            else
            {
                //OutPutandFlushLog();
                return true;
            }
        }

        public override bool Test()
        {
            try
            {
                
                logoStr = "";
                //// 是否要测试需要添加判定

                if (AnalysisInputParameters(inputParameters) == false)
                {
                    OutPutandFlushLog();
                    return false;
                }               

                if (supply != null)
                {
                    // open apc 
                    CloseandOpenAPC(Convert.ToByte(APCMODE.IBAISandIMODON));
                   

                    SetTxBiasAdcOffset();
                    
                    OutPutandFlushLog();
                    return true;
                }
                else
                {
                    Log.SaveLogToTxt("Equipments are not enough!");
                    OutPutandFlushLog();
                    return false;
                }
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

        protected override bool AnalysisInputParameters(TestModeEquipmentParameters[] InformationList)
        {
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

        protected override bool PostTest()
        {//note: for inherited class, they need to call base function first,
            //then do other post-test process task
            bool flag = DeassembleEquipment();
            for (int i = 0; i < selectedEquipList.Count; i++)
            {
                selectedEquipList.Values[i].DecreaseReferencedTimes();

            }
            return flag;
        }

        protected override bool AssembleEquipment()
        {
            for (int i = 0; i < selectedEquipList.Count; i++)
            {
                if (!selectedEquipList.Values[i].OutPutSwitch(true))
                {
                    return false;
                }
            }
            return true;
        }

        protected override bool AnalysisOutputParameters(TestModeEquipmentParameters[] infoList)
        {
            return true;
        }

        protected bool SetTxBiasAdcOffset()
        {
            try
            {
                if (biasADCs == null)
                {
                    biasADCs = new ushort[GlobalParameters.TotalChCount];

                    for (int i = 0; i < GlobalParameters.TotalChCount; i++)
                    {
                        dut.ChangeChannel((i + 1).ToString());
                        dut.SetSoftTxDis();
                        Thread.Sleep(20);
                    }

                    for (int i = 0; i < GlobalParameters.TotalChCount; i++)
                    {
                        dut.ChangeChannel((i + 1).ToString());
                        dut.ReadBiasADC(out biasADCs[i]);
                        Log.SaveLogToTxt("biasADC of this channel is " + biasADCs[i]);

                        if (biasADCs[dut.MoudleChannel - 1] == 65535)
                        {
                            Log.SaveLogToTxt("biasADC of this channel is 65535.");
                            return false;
                        }
                    }
                    dut.TxAllChannelEnable();
                    //return to current channel
                    dut.ChangeChannel(GlobalParameters.CurrentChannel.ToString());

                    int sum = 0;
                    for (int i = 0; i < biasADCs.Length; i++)
                    {
                        sum += biasADCs[i];
                    }
                    ushort avg = (ushort)(sum / biasADCs.Length);
                    dut.SetBiasAdcOffset(avg);
                    Log.SaveLogToTxt("completed to set biasAdcOffset to " + avg);

                    ushort returndata = 0;
                    dut.ReadBiasAdcOffset(out returndata);
                    result = (avg == returndata) ? true : false;
                    return result;
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
                InnoExCeption ex = new InnoExCeption(ExceptionDictionary.Code._0xFFFFF, error.StackTrace);
                //Log.SaveLogToTxt(ex.ID + ": " + ex.Message + "\r\n" + ex.StackTrace);
                exceptionList.Add(ex);
                return false;
                //the other way is: should throw exception, rather than the above three code. see below:
                //throw new InnoExCeption(ExceptionDictionary.Code._0xFFFFF, error.StackTrace); 
            }
        }

        public override List<InnoExCeption> GetException()
        {
            return base.GetException();
        }

    }
}

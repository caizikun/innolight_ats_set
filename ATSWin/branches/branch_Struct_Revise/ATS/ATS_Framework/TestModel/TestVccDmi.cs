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
   public class TestVccDmi : TestModelBase
    {
        #region Attribute
        private double vccDmi;
        private double vccDmiErr;

        private ArrayList inPutParametersNameArray = new ArrayList();
        private Powersupply tempps;
        #endregion
        #region Method
        public TestVccDmi(DUT inPuDut)
        {
            
            dut = inPuDut;
            logoStr = null; 
            inPutParametersNameArray.Clear();
        }
        protected override bool CheckEquipmentReadiness()
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
                    if (tempKeys[i].ToUpper().Contains("POWERSUPPLY"))
                    {
                        selectedEquipList.Add("POWERSUPPLY", tempValues[i]);
                        isOK = true;
                    }
                }
                tempps = (Powersupply)selectedEquipList["POWERSUPPLY"];
                if (tempps != null)
                {
                    isOK = true;

                }
                else
                {
                    if (tempps == null)
                    {
                        Log.SaveLogToTxt("POWERSUPPLY =NULL");
                    }
                    isOK = false;
                    OutPutandFlushLog();
                }
                if (isOK)
                {
                    selectedEquipList.Add("DUT", dut);
                   
                }

                return isOK;
            }         
        }

        protected override bool StartTest()
        {
            
            logoStr = "";
          
            if (tempps != null)
            {                
                // open apc 
                //CloseandOpenAPC(Convert.ToByte(APCMODE.IBAISandIMODON));
                // open apc
                Log.SaveLogToTxt("Step3...Start ReadDmiVcc");
                vccDmi = dut.ReadDmiVcc();
                Log.SaveLogToTxt("DmiVcc:" + vccDmi.ToString());
                vccDmiErr = vccDmi - GlobalParameters.CurrentVcc;
                Log.SaveLogToTxt("vccDmiErr:" + vccDmiErr.ToString());
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
        protected override bool AnalysisOutputParameters(TestModeEquipmentParameters[] InformationList)
        {
            try
            {
                outputParameters = new TestModeEquipmentParameters[2];
                outputParameters[0].FiledName = "DMIVCC(V)";
                vccDmi = Algorithm.ISNaNorIfinity(vccDmi);
                outputParameters[0].DefaultValue = Math.Round(vccDmi, 4).ToString().Trim();
                outputParameters[1].FiledName = "DMIVCCERR(V)";
                vccDmiErr = Algorithm.ISNaNorIfinity(vccDmiErr);
                outputParameters[1].DefaultValue = Math.Round(vccDmiErr, 4).ToString().Trim();

                
                for (int i = 0; i < outputParameters.Length; i++)
                {
                    Log.SaveLogToTxt(outputParameters[i].FiledName + " : " + outputParameters[i].DefaultValue);
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
           
            //if (InformationList.Length == 0)//InformationList is null
            //{
               
            //    return false;
            //}
            //else//  InformationList is not null
            //{
            //   // int index = -1;
            //    for (byte i = 0; i < InformationList.Length; i++)
            //    {

                  
            //    }
            //    return true;
            //}
           
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

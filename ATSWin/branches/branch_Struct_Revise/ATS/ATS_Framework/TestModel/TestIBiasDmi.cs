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
   public class TestIBiasDmi : TestModelBase
    {
#region Attribute
        private TestIBiasDmiStruct testIBiasDmiStruct = new TestIBiasDmiStruct(); 
        private double ibiasDmi;
        private ArrayList inPutParametersNameArray = new ArrayList();
       private Powersupply tempps;
#endregion
#region Method
        public TestIBiasDmi(DUT inPuDut)
        {
            
            dut = inPuDut;
           logoStr = null;        

           inPutParametersNameArray.Clear();
           
       }
       override protected bool CheckEquipmentReadiness()
       {
           //check if all equipments are ready for this test; 
           //increase equipment referenced_times if ready
           //for (int i = 0; i < pEquipList.Count; i++)
           //    if (!pEquipList.Values[i].bReady) return false;

           lock (dut)
           {
               for (int i = 0; i < selectedEquipList.Count; i++)
               {
                   if (!selectedEquipList.Values[i].bReady) return false;

               }

               return true;
           }
       }
       override protected bool PrepareTest()
       {//note: for inherited class, they need to do its own preparation process task,
           //then call this base function
           //for (int i = 0; i < pEquipList.Count; i++)
           ////pEquipList.Values[i].IncreaseReferencedTimes();
           //{
           lock (dut)
           {
               for (int i = 0; i < selectedEquipList.Count; i++)
               {
                   selectedEquipList.Values[i].IncreaseReferencedTimes();

               }
               return AssembleEquipment();
           }
       }
       protected override bool ConfigureEquipment(EquipmentList selectedEquipmentList)
       {
           lock (dut)
           {
               for (int i = 0; i < selectedEquipList.Count; i++)
               {
                   if (!selectedEquipList.Values[i].Configure()) return false;

               }

               return true;
           }
       }
       protected override bool AssembleEquipment()
       {
           lock (dut)
           {
               for (int i = 0; i < selectedEquipList.Count; i++)
               {
                   if (!selectedEquipList.Values[i].OutPutSwitch(true)) return false;
               }
               return true;
           }
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
           lock (dut)
           {
               logoStr = "";

               if (tempps != null)
               {
                   // open apc 

                   {
                       CloseandOpenAPC(Convert.ToByte(APCMODE.IBAISandIMODON));
                   }
                   Thread.Sleep(2000);
                   // open apc
                   Log.SaveLogToTxt("Step3...ReadDmiBias");
                   ibiasDmi = dut.ReadDmiBias();
                   Log.SaveLogToTxt("ibiasDmi=" + ibiasDmi.ToString());
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
       }
       protected override bool AnalysisOutputParameters(TestModeEquipmentParameters[] InformationList)
       {
           lock (dut)
           {
               try
               {
                   outputParameters = new TestModeEquipmentParameters[1];
                   outputParameters[0].FiledName = "IBIASDMI(MA)";
                   ibiasDmi = Algorithm.ISNaNorIfinity(ibiasDmi);
                   outputParameters[0].DefaultValue = Math.Round(ibiasDmi, 4).ToString().Trim();

                   
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
           }
       }

       private void OutPutandFlushLog()
       {
           lock (dut)
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
       }

       public override List<InnoExCeption> GetException()
       {
           lock (dut)
           {
               return base.GetException();
           }
       }
#endregion
    }
}

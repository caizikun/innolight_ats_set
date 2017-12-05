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
        public TestIBiasDmi(DUT inPuDut, logManager logmanager)
        {
            logger = logmanager;
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
                       logoStr += logger.AdapterLogString(3, "POWERSUPPLY =NULL");
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
           logger.FlushLogBuffer();
           logoStr = "";
           
           if (tempps != null)
           {             
               // open apc 
              
               {
                   CloseandOpenAPC(Convert.ToByte(APCMODE.IBAISandIMODON));
               }
               Thread.Sleep(2000);
               // open apc
               logoStr += logger.AdapterLogString(0, "Step3...ReadDmiBias");
               ibiasDmi = dut.ReadDmiBias();
               logoStr += logger.AdapterLogString(1,  "ibiasDmi=" + ibiasDmi.ToString());
               OutPutandFlushLog();
               return true;
           }
           else
           {
               logoStr += logger.AdapterLogString(4, "Equipments are not enough!");
               OutPutandFlushLog();
               return false;
           }
           
       }
       protected override bool AnalysisOutputParameters(TestModeEquipmentParameters[] InformationList)
       {

           try
           {
               outputParameters = new TestModeEquipmentParameters[1];
               outputParameters[0].FiledName = "IBIASDMI(MA)";
               ibiasDmi = algorithm.ISNaNorIfinity(ibiasDmi);
               outputParameters[0].DefaultValue = Math.Round(ibiasDmi, 4).ToString().Trim();  
               return true;

           }
           catch (System.Exception ex)
           {
               throw ex;
           }
       }      
       
       private void OutPutandFlushLog()
       {
           try
           {
               AnalysisOutputParameters(outputParameters);
               
               logger.FlushLogBuffer();
           }
           catch (System.Exception ex)
           {
               throw ex;
           }
       }
#endregion
    }
}

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
               if (!selectedEquipList.Values[i].Switch(true)) return false;
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
               if (selectedEquipList["POWERSUPPLY"] != null )
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

       protected override bool StartTest()
       {
           logger.FlushLogBuffer();
           logoStr = "";
           if (AnalysisInputParameters(inputParameters) == false)
           {
               logger.FlushLogBuffer();
               return false;
           }
           if (selectedEquipList["DUT"] != null && selectedEquipList["POWERSUPPLY"] != null)
           {
               Powersupply tempps = (Powersupply)selectedEquipList["POWERSUPPLY"];
               // open apc 
               byte apcStatus = 0;
               dut.APCStatus(out  apcStatus);
               if (GlobalParameters.ApcStyle == 0)
               {
                   if (apcStatus != 0x11)
                   {
                       logoStr += logger.AdapterLogString(0, "Step2...Start close apc");
                       dut.APCON(0x11);
                       logoStr += logger.AdapterLogString(0, "Power off");
                       tempps.Switch(false);                       
                       logoStr += logger.AdapterLogString(0, "Power on");
                       tempps.Switch(true);                      
                       bool isclosed = dut.APCStatus(out  apcStatus);
                       if (apcStatus == 0x11)
                       {
                           logoStr += logger.AdapterLogString(1, "APC ON");

                       }
                       else
                       {
                           logoStr += logger.AdapterLogString(3, "APC NOT ON");

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
                       tempps.Switch(false);
                       logoStr += logger.AdapterLogString(0, "Power on");
                       tempps.Switch(true);
                       bool isclosed = dut.APCStatus(out  apcStatus);
                       if (apcStatus == 0x11)
                       {
                           logoStr += logger.AdapterLogString(1, "APC ON");

                       }
                       else
                       {
                           logoStr += logger.AdapterLogString(3, "APC ON");

                       }
                   }

               }
               // open apc
               logoStr += logger.AdapterLogString(0, "Step3...ReadDmiBias");
               ibiasDmi = dut.ReadDmiBias();
               logoStr += logger.AdapterLogString(1,  "ibiasDmi=" + ibiasDmi.ToString());
               AnalysisOutputParameters(outputParameters);
               logger.FlushLogBuffer();
               return true;
           }
           else
           {
               logoStr += logger.AdapterLogString(4, "Equipments is not enough!");
               logger.FlushLogBuffer();
               return false;
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

                   if (algorithm.FindFileName(InformationList, "IBIAS(MA)", out index))
                   {
                       InformationList[index].DefaultValue = Math.Round(ibiasDmi,4).ToString().Trim();    
                   }                  
               }
              
               return true;
           }

       }
       protected override bool AnalysisInputParameters(TestModeEquipmentParameters[] InformationList)
       {
           logoStr += logger.AdapterLogString(0, "Step1...Check InputParameters");
           logoStr += logger.AdapterLogString(1, "OK!");
           return true;
          
       }
#endregion
    }
}

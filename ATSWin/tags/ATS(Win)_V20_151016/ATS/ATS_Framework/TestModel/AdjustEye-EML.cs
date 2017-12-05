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
   public class AdjustEye_EML :TestModelBase
    {


        #region Attribute
        private SortedList<string, AdjustEyeTargetValueRecordsStruct> adjustEyeTargetValueRecordsStructArray = new SortedList<string, AdjustEyeTargetValueRecordsStruct>();
        private AdjustEYEStruce adjustEYEStruce = new AdjustEYEStruce();

    

        private double IModDacStart;
        private double IModMax;
        private double IModMin;
        //sepcfile
        private bool flagResult;

        private Powersupply pPower;
        private Scope pScope;
     //   private DUT pDut;
        #endregion

        #region  Assist

        public AdjustEye_EML(DUT inPuDut, logManager logmanager)
        {
           
            logger = logmanager;           
            logoStr = null;
            dut = inPuDut;            
           
        }
        override protected bool CheckEquipmentReadiness()
        {
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

                }
                pPower = (Powersupply)selectedEquipList["POWERSUPPLY"];
                pScope = (Scope)selectedEquipList["SCOPE"];
                if (pPower != null && pScope != null)
                {
                    isOK = true;

                }
                else
                {
                    if (pScope == null)
                    {
                        logoStr += logger.AdapterLogString(3, "SCOPE =NULL");
                    }
                    if (pScope == null)
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
        #endregion

       private bool InitializeDut()
       {
           for (int i = 1; i < 5; i++)
           {
               dut.ChangeChannel(i.ToString());
               dut.WriteBiasDac(0x9f);
               dut.WriteEA(1, 0X28F);
               dut.WriteVC(1, 0X320);
               dut.WriteVG(1, 0X550);
           }
           return true;

       }
      override public bool Test()
        {
            pScope.ChangeChannel("1");
            InitializeDut();
            pScope.AutoScale(1);
            AdjustVLD();
            AdjustiBias();
            AdjustVC();
            AdjustCrossing_EA();
            AdjustEA();
            ReducePowerClass();
            return true;
        }

       #region  AdjustIbias

       private bool AdjustiBias()
       {
           for (int i = 1; i < 2; i++)
           {
               SearchSaturationOpint(i);
           }


           return false;
       }
       private bool SearchSaturationOpint(int i)
       {
           bool flag = false;
           double SaturationAP;
           double MaxAP;
           Int32 StartibiasDAC = 0x9f;
           dut.ChangeChannel(i.ToString());
           dut.WriteBiasDac(0x9f);
           Thread.Sleep(2000);
           MaxAP = pScope.GetAveragePowerdbm();

           while (!flag && StartibiasDAC > 0x80) 
           {
               StartibiasDAC -= 1;
               dut.WriteBiasDac(StartibiasDAC);
               Thread.Sleep(100);
               pScope.DisplayPowerdbm();
               SaturationAP = pScope.GetAveragePowerdbm();
               if (SaturationAP > (MaxAP - 0.15)) flag = false;
               else flag = true;
           } 

           if (flag)
           {
               dut.StoreBiasDac(StartibiasDAC+1);
           }
           else
           {
               dut.StoreBiasDac(0x80);
           }


           return true;
       }

#endregion
      
       #region  VLD
       private bool AdjustVLD()
       {
           bool flagMask = false;
           //   double []Mask=0;
           Int32 DAC_Min = 0x220;
           Int32 DAC_Start = 0x350;

           while (flagMask == false && DAC_Start > 0x220) 
           {
               dut.WriteVLD(1, DAC_Start);
               flagMask = Analyze_Mask_OMA();

               DAC_Start -= 32;

           } 

           if (flagMask)
           {
               dut.WriteVLD(2, DAC_Start);
           }
           else
           {
               dut.WriteVLD(2, 0x220);
           }

           return true;
       }
       private bool Analyze_Mask_OMA()
       {
           double MaskSpecMin = 20;
           double OMASpecMin = 0;
           bool flag = true;

            double[] Mask ;
            double[] OMA;
            pScope.ClearDisplay();
            Thread.Sleep(10000);
            pScope.GetAllChannel_Mask_OMA( 1,out Mask,out OMA);

           // Mask.Length = 1;

           for (int i = 0; i < 1; i++)
           {
               if (Mask[i] < MaskSpecMin) flag = false;
               if (OMA[i] < OMASpecMin) flag = false;
           }
          

           return flag;

       }
       #endregion
      
       #region  AdjustVC
       private bool AdjustVC()
       {
           for (int i = 1; i <2;i++ )
           {
               SearchER(i);
           }

           return true;
       }
       private bool SearchER(int i)
       {
           double ER;
            double ERSpecMin=9;
           bool flag=false;
           Int32 StartDac = 0x300;
           dut.WriteVC(1, StartDac);

           dut.ChangeChannel(i.ToString());
           pScope.DisplayER();
           ER = pScope.GetEratio();
           while (!flag && StartDac < 0x400) 
           {
               StartDac+=0X30;
               dut.WriteVC(1, StartDac);
               pScope.DisplayER();
               Thread.Sleep(200);
               ER = pScope.GetEratio();
              
               
               if(ER<9) flag=true;
           }

           if (flag)
           {
               dut.WriteVC(2, StartDac);
           }
           else
           {
               dut.WriteVC(2, 0x400);
           }


           return true;
       }

#endregion

       #region Crossing_EA

       private bool AdjustCrossing_EA()
       {

           bool flag = true;

           for (int i = 1; i < 5;i++ )
           {
               if (!SearchCrossing_EA(i)) flag = false;
              
           }

           return flag;
       }
       private int InitializeCrossing()
       {//确定Crossing的符号
           double SpecMin = 48, SpecMax = 52,Crossing;
          int StartDac=0;
          dut.WriteCrossDac(0);
         
          Crossing= pScope.GetCrossing();

          if (Crossing > SpecMax)//为负号
          {
              StartDac = 0;
          }
          else if(Crossing<SpecMin)//为正号
          {
              StartDac = 0x08;
          }
           return StartDac;
       }
       private bool SearchCrossing_EA(int Channel)
       {// StartDac  有一位为符号, 即此值带符号
           bool flag=true;
           int StartDAC=0;
           int Step = 8;//由芯片决定,这个是最小的步长
           double SpecMin = 48, SpecMax = 52,Crossing;
           double[] MaskArray = new double[1];
           double[] OmaArray = new double[1];
           double Mask=0, Oma=0;

           dut.ChangeChannel(Channel.ToString());
           pScope.ChangeChannel(Channel.ToString());
          
           StartDAC = InitializeCrossing();

           dut.WriteCrossDac(StartDAC);

           int i = 0;

           dut.WriteCrossDac(StartDAC);

           Crossing = pScope.GetCrossing();

           pScope.GetAllChannel_Mask_OMA(1, out MaskArray, out OmaArray);

           Mask=MaskArray[0];
           //Oma = OmaArray[0];

           while ((!flag && StartDAC < 16 && i < 16)|| (Mask <20)) 
           {
               dut.WriteCrossDac(StartDAC);
               Crossing = pScope.GetCrossing();
             
               pScope.GetAllChannel_Mask_OMA(1, out MaskArray, out OmaArray);

               if (Crossing<SpecMax&&Crossing>SpecMin)
               {
                   flag=true;
               }
               else if (Crossing > SpecMax)
               {
                   StartDAC -= Step;
               }
               else if (Crossing< SpecMin)
               {
                   StartDAC += Step;
               }
               //i++;




           }
           dut.StoreCrossDac(StartDAC);

           return flag;
       }
#endregion

       #region  AdjsutEA

       private bool AdjustEA()
       {

           bool flag = true;

           for (int i = 1; i < 5; i++)
           {
               dut.ChangeChannel(i.ToString());
               if (!SearchMask()) flag = false;

           }

           return flag;
       }
       private bool SearchMask()
       {


           bool flag = false;
          
           byte Step = 0x20;
           double SpecMin = 20, TempMask;

           int EA_DACMax = 400;
           int EA_DACMin = 240;

           int StartDAC = EA_DACMin;

           dut.WriteCrossDac(StartDAC);

           int i = 0;

           while (!flag && StartDAC < EA_DACMax) 
           {
               dut.WriteEA(1, StartDAC);
               Thread.Sleep(2000);
               TempMask = pScope.GetMask();

               if (TempMask > SpecMin)
               {
                   flag = true;
               }
               else if (TempMask < SpecMin)
               {
                   StartDAC += Step;
               }
               i++;
           }
           dut.WriteEA(2, StartDAC);
          // while (!flag && StartDAC < EA_DACMax);

           return flag;
       }
#endregion


       #region  ReducePower

       private bool ReducePowerClass()
       {
           bool flagStop = false;
           int StartDac = 0;
           for (int i = 1; i < 5;i++ )
           {
               byte[]IbiasDac;
               dut.ReadBiasDac(1, out IbiasDac);
               StartDac = IbiasDac[0];
               flagStop = false;
                int Step=1;
                while (!flagStop) 
               {
                  StartDac-= Step;
                  dut.WriteBiasDac(StartDac);
                   if (TestTxEye())
                   {
                       flagStop = false;
                   }
                   else
                   {
                       flagStop = true;
                   }
               }
                dut.StoreBiasDac(StartDac);
           }
           
           return true;
       }
       private bool TestTxEye()
       {
           double AP,Spec_AP_Max=1.5,Spec_AP_Min=0.5;
           double ER,Spec_ER_Max=9,Spec_ER_Min=4;
           double Crossing,Spec_Crossing_Max=40,Spec_Crossing_Min=50;
           double Mask,Spec_Mask_Max=40,Spec_Mask_Min=20;
            double Jitterpp,Spec_Jitterpp_Max=20,Spec_Jitterpp_Min=10;
            double JitterRSM,Spec_JitterRSM_Max=30,Spec_JitterRSM_Min=10;

            bool flag = true;
            pScope.ClearDisplay();
            AP=pScope.GetAveragePowerdbm();
            ER = pScope.GetEratio();
            Crossing = pScope.GetCrossing();
            Mask = pScope.GetMask();
            Jitterpp = pScope.GetJitterPP();
            JitterRSM = pScope.GetJitterRSM();

            if (!JudgValue(AP,Spec_AP_Max,Spec_AP_Min)) flag = false;
            if (!JudgValue(ER, Spec_ER_Max, Spec_ER_Min)) flag = false;
            if (!JudgValue(Crossing, Spec_Crossing_Max, Spec_Crossing_Min)) flag = false;
            if (!JudgValue(Mask, Spec_Mask_Max, Spec_Mask_Min)) flag = false;
            if (!JudgValue(Jitterpp, Spec_Jitterpp_Max, Spec_Jitterpp_Min)) flag = false;
            if (!JudgValue(JitterRSM, Spec_JitterRSM_Max, Spec_JitterRSM_Min)) flag = false;

            return flag;
       }
       private bool JudgValue(double InputValue,double Max,double Min)
       {
           if (InputValue > Max || InputValue<Min)
           {
               return false;
           }
           else
           {
               return true;
           }
       }

       #endregion
       protected override bool StartTest()
       {
           Test();
           OutPutandFlushLog();
           return flagResult;
       }

    }
}

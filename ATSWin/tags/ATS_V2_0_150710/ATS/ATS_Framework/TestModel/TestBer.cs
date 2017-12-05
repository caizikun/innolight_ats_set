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
    public class TestBer : TestModelBase
    {
#region Attribute     
        private TestBerStruct testBerStruct = new TestBerStruct();
          
        private double sensitivityPoint;
        private double ber_erp = 0;
        private double sensOMA;
        private ArrayList inPutParametersNameArray = new ArrayList();
        private ArrayList OSERValueArray = new ArrayList();
        private ArrayList serchAttPoints = new ArrayList();
        private ArrayList serchBerPoints = new ArrayList();
        private ArrayList attPoints = new ArrayList();
        private ArrayList berPoints = new ArrayList();
        private double attPoint = 0;
        //equipments
       private Attennuator tempAtten;
       private ErrorDetector tempED;
       private Powersupply tempps;
#endregion

#region Method
        public TestBer(DUT inPuDut, logManager logmanager)
        {
            logger = logmanager;
            logoStr = null;
            dut = inPuDut;
            OSERValueArray.Clear();
            inPutParametersNameArray.Clear();
            inPutParametersNameArray.Add("CSENALIGNRXPWR(DBM)");
            inPutParametersNameArray.Add("CSENSTARTINGRXPWR(DBM)");
            inPutParametersNameArray.Add("SEARCHTARGETBERUL");
            inPutParametersNameArray.Add("SEARCHTARGETBERLL");
            inPutParametersNameArray.Add("SEARCHTARGETRXPOWERLL");
            inPutParametersNameArray.Add("SEARCHTARGETRXPOWERUL");           
            inPutParametersNameArray.Add("COEFCSENSUBSTEP(DBM)");
            inPutParametersNameArray.Add("COEFCSENADDSTEP(DBM)");
            inPutParametersNameArray.Add("ISBERQUICKTEST");
            inPutParametersNameArray.Add("ISOPTICALSOURCEUNITOMA");
            
           
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
            logger.FlushLogBuffer();
            logoStr = "";
            if (AnalysisInputParameters(inputParameters)==false)
            {
                OutPutandFlushLog();
                return false;
            }
            GlobalBER_EXPtoRealBer();
            if (testBerStruct.IsOpticalSourceUnitOMA)
            {
                testBerStruct.CsenAlignRxPwr = algorithm.CalculateFromOMAtoDBM(testBerStruct.CsenAlignRxPwr, Convert.ToDouble(OSERValueArray[GlobalParameters.CurrentChannel - 1]));
                testBerStruct.CsenStartingRxPwr = algorithm.CalculateFromOMAtoDBM(testBerStruct.CsenStartingRxPwr, Convert.ToDouble(OSERValueArray[GlobalParameters.CurrentChannel - 1]));
                testBerStruct.SearchTargetBerRxpowerUL = algorithm.CalculateFromOMAtoDBM(testBerStruct.SearchTargetBerRxpowerUL, Convert.ToDouble(OSERValueArray[GlobalParameters.CurrentChannel - 1]));
                testBerStruct.SearchTargetBerRxpowerLL = algorithm.CalculateFromOMAtoDBM(testBerStruct.SearchTargetBerRxpowerLL, Convert.ToDouble(OSERValueArray[GlobalParameters.CurrentChannel - 1]));
            }
            
            if (tempAtten != null && tempED != null && tempps != null)
            {                
                // open apc 
              
                {
                    CloseandOpenAPC(Convert.ToByte(APCMODE.IBAISandIMODON));
                }
                
                // open apc
                
                logoStr += logger.AdapterLogString(0, "Step3...SetAttenValue");
                SetAttenValue(tempAtten, testBerStruct.CsenAlignRxPwr);                
                logoStr += logger.AdapterLogString(0, "Step4...AutoAlaign");
                bool isAutoAlaign = tempED.AutoAlaign(true);
                if (isAutoAlaign)
                {
                    logoStr += logger.AdapterLogString(1, isAutoAlaign.ToString());
                } 
                else
                {
                    logoStr += logger.AdapterLogString(4, isAutoAlaign.ToString());
                    sensitivityPoint = -5000;
                    OutPutandFlushLog();
                    return isAutoAlaign;
                }
                if (isAutoAlaign)
                {
                    if (testBerStruct.IsBerQuickTest==true)
                    {
                       QuickTest(tempAtten,tempED);
                       OutPutandFlushLog();
                       return true;
                    } 
                    else
                    {
                        if (!SerchTargetBer(tempAtten, tempED))
                        {
                            OutPutandFlushLog();
                            return true;
                        }
                        GettingCurvingPointsandFitting(tempAtten, tempED);
                        if (double.IsNaN(sensitivityPoint) || double.IsInfinity(sensitivityPoint))
                        {
                            sensitivityPoint = -1000;
                            OutPutandFlushLog();
                        }
                        sensitivityPoint = Math.Round(sensitivityPoint, 2);
                        logoStr += logger.AdapterLogString(1, "sensitivityPoint= " + sensitivityPoint.ToString());
                        OutPutandFlushLog();
                        return true;
                    }
                   
                }
                else
                {
                    AnalysisOutputProcData(procData);
                    AnalysisOutputParameters(outputParameters);
                    logger.FlushLogBuffer();                  
                    return false;
                }
            }
            else
            {
                logoStr += logger.AdapterLogString(4, "Equipments are not enough!");
                AnalysisOutputProcData(procData);
                AnalysisOutputParameters(outputParameters);
                logger.FlushLogBuffer();               
                return false;
            }

        }
        protected void SetAttenValue(Attennuator tempAtt, double attValue)
        {

            tempAtt.AttnValue(attValue.ToString(),0);

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
                    if (tempKeys[i].ToUpper().Contains("ERRORDETE"))
                   
                    {
                        selectedEquipList.Add("ERRORDETE", tempValues[i]);

                    }
                    if (tempKeys[i].ToUpper().Contains("ATTEN"))
                 
                    {
                        selectedEquipList.Add("ATTEN", tempValues[i]);
                    }
                    if (tempKeys[i].ToUpper().Contains("POWERSUPPLY"))
                    {
                        selectedEquipList.Add("POWERSUPPLY", tempValues[i]);
                    }

                }
                 tempAtten = (Attennuator)selectedEquipList["ATTEN"];
                 tempED = (ErrorDetector)selectedEquipList["ERRORDETE"];
                 tempps = (Powersupply)selectedEquipList["POWERSUPPLY"];
                 if (tempAtten != null && tempED != null && tempps != null)
                {
                    isOK = true;

                }
                else
                {
                    if (tempED == null)
                    {
                        logoStr += logger.AdapterLogString(3, "ERRORDETE =NULL");
                    }
                    if (tempAtten == null)
                    {
                        logoStr += logger.AdapterLogString(3, "ATTEN =NULL");
                    }
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
                outputParameters = new TestModeEquipmentParameters[2];
                outputParameters[0].FiledName = "CSEN(DBM)";
                sensitivityPoint = algorithm.ISNaNorIfinity(sensitivityPoint);
                if (testBerStruct.IsBerQuickTest == true)
                {
                    outputParameters[0].DefaultValue = sensitivityPoint.ToString().Trim();
                }
                else
                {
                    outputParameters[0].DefaultValue = Math.Round(sensitivityPoint, 4).ToString().Trim();
                }


                outputParameters[1].FiledName = "CSENOMA(DBM)";
                sensOMA = algorithm.CalculateOMA(sensitivityPoint, Convert.ToDouble(OSERValueArray[GlobalParameters.CurrentChannel - 1]));
                outputParameters[1].DefaultValue = Math.Round(sensOMA, 4).ToString().Trim();
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
                if (GlobalParameters.OpticalSourseERArray == null || GlobalParameters.OpticalSourseERArray=="")
                {
                    return false;
                } 
                else
                {
                    char[] tempCharArray = new char[] { ',' };
                    OSERValueArray = algorithm.StringtoArraylistDeletePunctuations(GlobalParameters.OpticalSourseERArray, tempCharArray);
                }
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

                        if (algorithm.FindFileName(InformationList, "CSENALIGNRXPWR(DBM)", out index))
                        {     
                            double temp=Convert.ToDouble(InformationList[index].DefaultValue);
                            
                             if (double.IsInfinity(temp) || double.IsNaN(temp))
                            {
                                logoStr += logger.AdapterLogString(4, InformationList[index].FiledName + "is wrong!");
                                return false;
                            }
                             else
                             {
                                 if (temp>0)
                                 {
                                     temp = -temp;
                                 }
                                 testBerStruct.CsenAlignRxPwr = temp;
                             }
                            
                           
                        }
                        if (algorithm.FindFileName(InformationList, "CSENSTARTINGRXPWR(DBM)", out index))
                        {
                            double temp = Convert.ToDouble(InformationList[index].DefaultValue);

                            if (double.IsInfinity(temp) || double.IsNaN(temp))
                            {
                                logoStr += logger.AdapterLogString(4, InformationList[index].FiledName + "is wrong!");
                                return false;
                            }
                            else
                            {
                                if (temp>0)
                                {
                                    temp = -temp;
                                }
                                testBerStruct.CsenStartingRxPwr = temp;
                            }
                            
                           
                           
                        }
                        if (algorithm.FindFileName(InformationList, "SEARCHTARGETBERUL", out index))
                        {
                            double temp = Convert.ToDouble(InformationList[index].DefaultValue);

                            if (double.IsInfinity(temp) || double.IsNaN(temp))
                            {
                                logoStr += logger.AdapterLogString(4, InformationList[index].FiledName + "is wrong!");
                                return false;
                            }
                            else
                            {
                                if (temp<0)
                                {
                                    temp = -temp;
                                }
                                testBerStruct.SearchTargetBerUL = temp;
                            }
                        }
                        if (algorithm.FindFileName(InformationList, "SEARCHTARGETBERLL", out index))
                        {                            
                            double temp = Convert.ToDouble(InformationList[index].DefaultValue);

                            if (double.IsInfinity(temp) || double.IsNaN(temp))
                            {
                                logoStr += logger.AdapterLogString(4, InformationList[index].FiledName + "is wrong!");
                                return false;
                            }
                            else
                            {
                                if (temp < 0)
                                {
                                    temp = -temp;
                                }
                                testBerStruct.SearchTargetBerLL = temp;
                            }
                            
                        }
                        if (algorithm.FindFileName(InformationList, "SEARCHTARGETRXPOWERUL", out index))
                        {
                            double temp = Convert.ToDouble(InformationList[index].DefaultValue);

                            if (double.IsInfinity(temp) || double.IsNaN(temp))
                            {
                                logoStr += logger.AdapterLogString(4, InformationList[index].FiledName + "is wrong!");
                                return false;
                            }
                            else
                            {
                                if (temp>0)
                                {
                                    temp = -temp;
                                }
                               testBerStruct.SearchTargetBerRxpowerUL = temp;
                            }
                             
                            
                        }
                        if (algorithm.FindFileName(InformationList, "SEARCHTARGETRXPOWERLL", out index))
                        {
                            double temp = Convert.ToDouble(InformationList[index].DefaultValue);

                            if (double.IsInfinity(temp) || double.IsNaN(temp))
                            {
                                logoStr += logger.AdapterLogString(4, InformationList[index].FiledName + "is wrong!");
                                return false;
                            }
                            else
                            {
                                if (temp > 0)
                                {
                                    temp = -temp;
                                }
                                testBerStruct.SearchTargetBerRxpowerLL  = temp;
                            }
                            
                        }
                       
                        if (algorithm.FindFileName(InformationList, "COEFCSENSUBSTEP(DBM)", out index))
                        {
                            double temp = Convert.ToDouble(InformationList[index].DefaultValue);

                            if (double.IsInfinity(temp) || double.IsNaN(temp))
                            {
                                logoStr += logger.AdapterLogString(4, InformationList[index].FiledName + "is wrong!");
                                testBerStruct.CoefCsenSubStep = 0.5;               
                            }
                            else
                            {
                                if (temp < 0)
                                {
                                    temp = -temp;
                                }
                               testBerStruct.CoefCsenSubStep= temp;
                            }
                            
                        }
                        if (algorithm.FindFileName(InformationList, "COEFCSENADDSTEP(DBM)", out index))
                        {
                            double temp = Convert.ToDouble(InformationList[index].DefaultValue);

                            if (double.IsInfinity(temp) || double.IsNaN(temp))
                            {
                                logoStr += logger.AdapterLogString(4, InformationList[index].FiledName + "is wrong!");
                                testBerStruct.CoefCsenAddStep = 0.5;
                            }
                            else
                            {
                                if (temp < 0)
                                {
                                    temp = -temp;
                                }
                                testBerStruct.CoefCsenAddStep = temp;
                            }
                            
                        }
                        if (algorithm.FindFileName(InformationList, "ISBERQUICKTEST", out index))
                        {
                            string temp = InformationList[index].DefaultValue;
                            if (temp.Trim().ToUpper() == "0" || temp.Trim().ToUpper() == "FALSE")
                            {
                                testBerStruct.IsBerQuickTest=false;
                            } 
                            else
                            {
                                testBerStruct.IsBerQuickTest=true;
                            }                           

                        }
                        if (algorithm.FindFileName(InformationList, "ISOPTICALSOURCEUNITOMA", out index))
                        {
                            string temp = InformationList[index].DefaultValue;
                            if (temp.Trim().ToUpper() == "0" || temp.Trim().ToUpper()=="FALSE")
                            {
                                testBerStruct.IsOpticalSourceUnitOMA  = false;
                            }
                            else
                            {
                                testBerStruct.IsOpticalSourceUnitOMA  = true;
                            }   
                           
                        }
                    }

                }
                if (testBerStruct.SearchTargetBerUL <= testBerStruct.SearchTargetBerLL || testBerStruct.SearchTargetBerRxpowerUL <= testBerStruct.SearchTargetBerRxpowerLL)
                {
                    logoStr += logger.AdapterLogString(4, "inputData is wrong!");
                    return false;
                }
                logoStr += logger.AdapterLogString(1,"OK!");
                return true;
            }
        }
        protected double SerchTargetPoint(Attennuator tempAtt, ErrorDetector tempED, double attValue, double targetBerLL, double targetBerUL, double addStep, double sumStep)
        {
            byte i = 0;
            double currentCense = 0;
            do
            {
                tempAtt.AttnValue(attValue.ToString(),0);
                currentCense = tempED.RapidErrorRate(1);
                if (currentCense > targetBerUL)
                {
                    attValue += addStep;
                }
                else
                {
                    attValue -= sumStep;
                }
                i++;

            } while (i < 20 && (currentCense > targetBerUL || currentCense < targetBerLL));

            return attValue;

        }
        protected bool SerchCoefPointsTarget1E3(Attennuator tempAtt, ErrorDetector tempED, double startAttValue, double targetBer, double addStep, double sumStep, out ArrayList AttenPoints, out ArrayList BerPints)
        {
            byte i = 0;
            double currentBer = 0;
            //AttenPoints[]=
            AttenPoints = new ArrayList();
            BerPints = new ArrayList();
            AttenPoints.Clear();
            BerPints.Clear();
            do
            {
                tempAtt.AttnValue(startAttValue.ToString(),0);
                currentBer = tempED.RapidErrorRate();
                if (currentBer != 0)
                {
                    AttenPoints.Add(startAttValue);
                    BerPints.Add(currentBer);
                }
                if (currentBer > targetBer)
                {
                    startAttValue += addStep;
                }
                else
                {
                    startAttValue -= sumStep;
                }
                i++;

            } while (i < 8 && (currentBer < (targetBer * 1E-1)));

            return true;
        }
        protected bool SerchCoefPoints(Attennuator tempAtt, ErrorDetector tempED, double startAttValue, double targetBer, double addStep, double sumStep, out ArrayList AttenPoints, out ArrayList BerPints)
        {
            byte i = 0;
            double terminativeBer=0;
            bool terminalFlag=false;
            double currentBer = 0;
            //AttenPoints[]=
            AttenPoints = new ArrayList();
            BerPints = new ArrayList();
            AttenPoints.Clear();
            BerPints.Clear();
            if (ber_erp < testBerStruct.SearchTargetBerLL)
            {
                terminativeBer = targetBer * 100;
            }
            else if (ber_erp > testBerStruct.SearchTargetBerUL)
            {
                terminativeBer = targetBer * 1E-1;
            }

            do
            {
                tempAtt.AttnValue(startAttValue.ToString(),0);
                currentBer = tempED.RapidErrorRate();
                if (currentBer != 0)
                {
                    AttenPoints.Add(startAttValue);
                    BerPints.Add(currentBer);
                }
                if (currentBer > targetBer)
                {
                    startAttValue += addStep;
                }
                else
                {
                    startAttValue -= sumStep;
                }
                i++;
                if (ber_erp < testBerStruct.SearchTargetBerLL)
                {
                   
                    terminalFlag = currentBer > terminativeBer;
                }
                else if (ber_erp > testBerStruct.SearchTargetBerUL)
                {
                    
                    terminalFlag =currentBer<terminativeBer;
                   
                }

            } while (i < 8 && (terminalFlag));

            return true;
        }
        protected  bool deleteErrBerPoints(ArrayList inputARR1, ArrayList inputARR2)
        {
            if (inputARR1.Count != inputARR2.Count 
                || inputARR1.Count == 0 
                || inputARR2.Count == 0 
                || inputARR1 == null 
                || inputARR2 == null)
            {
                return false;
            }
            for (int i = inputARR1.Count - 1; i >= 0; i--)
            {
                if (Convert.ToDouble(inputARR1[i]) >= 1
                    || (double.IsInfinity(Convert.ToDouble(inputARR1[i]))) 
                    || (double.IsNaN(Convert.ToDouble(inputARR1[i]))) 
                    || (inputARR1[i] == null)
                    || (inputARR2[i] == null)
                    || (double.IsInfinity(Convert.ToDouble(inputARR2[i])))
                    || (double.IsNaN(Convert.ToDouble(inputARR2[i]))))
                {
                    inputARR1.RemoveAt(i);
                    inputARR2.RemoveAt(i);
                }

            }

            return true;
        }
        protected double binarySearchTargetPoint(double LLimit, double ULimit, double targetBerLL, double targetBerUL, Attennuator tempAtt, ErrorDetector tempED, out ArrayList serchAttPoints, out ArrayList serchBerPoints)
        {
            double low = LLimit;
            double high = ULimit;
            double currentCense = 0;
            byte count = 0;
            serchAttPoints=new  ArrayList();
            serchBerPoints =new ArrayList();
            serchAttPoints.Clear();
            serchBerPoints.Clear();
            while (low <= high && count<=20)
            {
                double mid = low + ((high - low) / 2);
                tempAtt.AttnValue(mid.ToString(),0);
                serchAttPoints.Add(mid);
                currentCense = tempED.RapidErrorRate();
                serchBerPoints.Add(currentCense);
                if (currentCense < targetBerLL)
                {
                    high = mid;
                    count++;
                }
                else if (currentCense > targetBerUL)
                {
                    low = mid;
                    count++;
                }
                else
                    return mid;
            }
            return -10000;

        }
       
       
        protected override bool AnalysisOutputProcData(TestModeEquipmentParameters[] InformationList)
        {

            try
            {
                procData = new TestModeEquipmentParameters[4];
                procData[0].FiledName = "SearTargetBerRxPowerArray";
                procData[0].DefaultValue = algorithm.ArrayListToStringArraySegregateByPunctuations(serchAttPoints, ",");
                procData[1].FiledName = "SearTargetBerArray";
                procData[1].DefaultValue = algorithm.ArrayListToStringArraySegregateByPunctuations(serchBerPoints, ",");
                procData[2].FiledName = "CurvingRxPowerArray";
                procData[2].DefaultValue = algorithm.ArrayListToStringArraySegregateByPunctuations(attPoints, ",");
                procData[3].FiledName = "CurvingBerArray";
                procData[3].DefaultValue = algorithm.ArrayListToStringArraySegregateByPunctuations(berPoints, ",");
                return true;

            }
            catch (System.Exception ex)
            {
                throw ex;
            }



        }
        protected void QuickTest(Attennuator tempAtten, ErrorDetector tempED)
        {
            try
            {
                tempAtten.AttnValue(testBerStruct.CsenStartingRxPwr.ToString(), 0);
                double sensitivity = tempED.RapidErrorRate();
                logoStr += logger.AdapterLogString(1, "SetAtten=" + testBerStruct.CsenStartingRxPwr.ToString());
                logoStr += logger.AdapterLogString(1, "QUICBER=" + sensitivity.ToString());
                if (sensitivity >= 1)
                {
                    sensitivityPoint = 1;
                    AnalysisOutputParameters(outputParameters);
                    AnalysisOutputProcData(procData);
                    logger.FlushLogBuffer();
                    return;
                }
                
                {
                    if (sensitivity <= ber_erp)
                    {
                        sensitivityPoint = testBerStruct.CsenStartingRxPwr;
                    }
                    else
                    {
                        sensitivityPoint = sensitivity;
                        logoStr += logger.AdapterLogString(4, "AttPoint=" + testBerStruct.CsenStartingRxPwr.ToString() + ber_erp.ToString());
                        
                    }
                }
                logoStr += logger.AdapterLogString(1, "sensitivityPoint= " + sensitivityPoint.ToString());
                AnalysisOutputParameters(outputParameters);
                AnalysisOutputProcData(procData);
                logger.FlushLogBuffer();                          
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        protected bool SerchTargetBer(Attennuator tempAtten, ErrorDetector tempED)
        {
            try
            {

                logoStr += logger.AdapterLogString(0, "Step5...SerchTargetPoint");
                attPoint = binarySearchTargetPoint(testBerStruct.SearchTargetBerRxpowerLL, testBerStruct.SearchTargetBerRxpowerUL, testBerStruct.SearchTargetBerLL, testBerStruct.SearchTargetBerUL, tempAtten, tempED, out serchAttPoints, out serchBerPoints);
                logoStr += logger.AdapterLogString(1, "SetAtten=" + attPoint.ToString());
                for (byte i = 0; i < serchAttPoints.Count; i++)
                {
                    logoStr += logger.AdapterLogString(1, "serchAttPoints[ " + i.ToString() + "]" + double.Parse(serchAttPoints[i].ToString()) + "  " + "serchBerPoints[ " + i.ToString() + "]" + double.Parse(serchBerPoints[i].ToString()));
                }
                if (attPoint == -10000)
                {
                    sensitivityPoint = -10000;
                    AnalysisOutputProcData(procData);
                    AnalysisOutputParameters(outputParameters);
                    logger.FlushLogBuffer();
                    return false;
                }
                return true;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        protected void GettingCurvingPointsandFitting(Attennuator tempAtten, ErrorDetector tempED)
        {
            try
            {
                double intercept;
                double slope;
                byte tmepCount = 0;
                logoStr += logger.AdapterLogString(0, "Step6...SerchCoefPoints");
                {
                    SerchCoefPoints(tempAtten, tempED, attPoint, ber_erp, testBerStruct.CoefCsenAddStep, testBerStruct.CoefCsenSubStep, out attPoints, out berPoints);
                    tmepCount = (byte)Math.Min(attPoints.Count, berPoints.Count);
                    double[] tempattPoints = new double[tmepCount];
                    double[] tempberPoints = new double[tmepCount];
                    for (byte i = 0; i < tmepCount; i++)
                    {
                        tempattPoints[i] = double.Parse(attPoints[i].ToString());
                        tempberPoints[i] = double.Parse(berPoints[i].ToString());
                        logoStr += logger.AdapterLogString(1, "attPoints[ " + i.ToString() + "]" + double.Parse(attPoints[i].ToString()) + "  " + "berPoints[ " + i.ToString() + "]" + double.Parse(berPoints[i].ToString()));
                    }
                    algorithm.LinearRegression(algorithm.Getlog10(algorithm.GetNegative(algorithm.Getlog10(tempberPoints))), tempattPoints, out slope, out intercept);
                    sensitivityPoint = slope + (intercept * System.Math.Log10(System.Math.Log10(ber_erp) * (-1)));
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        protected void OutPutandFlushLog()
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
        protected void GlobalBER_EXPtoRealBer()
        {
            try
            {
                switch (GlobalParameters.BER_exp)
                {
                    case (byte)BER_ERP.NegativeZero:
                        {
                            ber_erp = 5E-5;
                            break;
                        }
                    case (byte)BER_ERP.NegativeOne:
                        {
                            ber_erp = 1E-1;
                            break;
                        }
                    case (byte)BER_ERP.NegativeTwo:
                        {
                            ber_erp = 1E-2;
                            break;
                        }
                    case (byte)BER_ERP.NegativeThree:
                        {
                            ber_erp = 1E-3;
                            break;
                        }
                    case (byte)BER_ERP.NegativeFour:
                        {
                            ber_erp = 1E-4;
                            break;
                        }
                    case (byte)BER_ERP.NegativeFive:
                        {
                            ber_erp = 1E-5;
                            break;
                        }
                    case (byte)BER_ERP.NegativeSix:
                        {
                            ber_erp = 1E-6;
                            break;
                        }
                    case (byte)BER_ERP.NegativeSeven:
                        {
                            ber_erp = 1E-7;
                            break;
                        }
                    case (byte)BER_ERP.NegativeEight:
                        {
                            ber_erp = 1E-8;
                            break;
                        }
                    case (byte)BER_ERP.NegativeNine:
                        {
                            ber_erp = 1E-9;
                            break;
                        }
                    case (byte)BER_ERP.NegativeTen:
                        {
                            ber_erp = 1E-10;
                            break;
                        }
                    case (byte)BER_ERP.NegativeEleven:
                        {
                            ber_erp = 1E-11;
                            break;
                        }
                    case (byte)BER_ERP.NegativeTwelve:
                        {
                            ber_erp = 1E-12;
                            break;
                        }
                        default:
                        break;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
#endregion
       
      
    }
}

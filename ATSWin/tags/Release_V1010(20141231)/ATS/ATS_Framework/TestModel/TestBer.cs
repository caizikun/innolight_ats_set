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
        private double sensOMA;
        private ArrayList inPutParametersNameArray = new ArrayList();
        private ArrayList OSERValueArray = new ArrayList();
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
            inPutParametersNameArray.Add("SEARCHTARGETBERADDSTEP");
            inPutParametersNameArray.Add("SEARCHTARGETBERSUBSTEP");
            inPutParametersNameArray.Add("CSENTARGETBER");
            inPutParametersNameArray.Add("COEFCSENSUBSTEP(DBM)");
            inPutParametersNameArray.Add("COEFCSENADDSTEP(DBM)");
            inPutParametersNameArray.Add("ISBERQUICKTEST");
           
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
                return false;
            }
            if (selectedEquipList["ATTEN"] != null && selectedEquipList["ERRORDETE"] != null && selectedEquipList["DUT"] != null && selectedEquipList["POWERSUPPLY"] != null)
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

                        tempps.Switch(false,1);                       
                        logoStr += logger.AdapterLogString(0, "Power on");
                        tempps.Switch(true,1);                        
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
                        tempps.Switch(false,1);                        
                        logoStr += logger.AdapterLogString(0, "Power on");
                        tempps.Switch(true,1);                        
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
                Attennuator tempAtten = (Attennuator)selectedEquipList["ATTEN"];
                ErrorDetector tempED = (ErrorDetector)selectedEquipList["ERRORDETE"];
                logoStr += logger.AdapterLogString(0, "Step3...SetAttenValue");
                SetAttenValue(tempAtten, testBerStruct.CsenAlignRxPwr);
                tempAtten.Switch(true,1);               
                double attPoint = 0;
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
                    AnalysisOutputParameters(outputParameters);
                    logger.FlushLogBuffer();
                    return isAutoAlaign;
                }
               
                if (isAutoAlaign)
                {
                    if (testBerStruct.IsBerQuickTest==true)
                    {
                        tempAtten.AttnValue(testBerStruct.CsenStartingRxPwr.ToString(),0);
                        double sensitivity = tempED.RapidErrorRate();
                        logoStr += logger.AdapterLogString(1, "SetAtten=" + testBerStruct.CsenStartingRxPwr.ToString());
                        logoStr += logger.AdapterLogString(1, "QUICBER=" + sensitivity.ToString());
                        if (sensitivity>=1)
                        {
                            sensitivityPoint =1;
                            AnalysisOutputParameters(outputParameters);
                            logger.FlushLogBuffer();
                            return false;
                        }
                        if (testBerStruct.CsenTargetBER == 1E-3)
                        {
                            if (sensitivity <= 1E-3)
                           {
                               sensitivityPoint = testBerStruct.CsenStartingRxPwr;
                           } 
                           else
                           {
                               sensitivityPoint = sensitivity;
                               logoStr += logger.AdapterLogString(4, "AttPoint="+testBerStruct.CsenStartingRxPwr.ToString() + "CSENCE>1E-3 ");
                               AnalysisOutputParameters(outputParameters);
                               logger.FlushLogBuffer();
                               return false;
                           }
                        }
                        else if (testBerStruct.CsenTargetBER == 1E-12)
                        {
                            if (sensitivity <= 1E-12)
                            {
                                sensitivityPoint = testBerStruct.CsenStartingRxPwr;
                            }
                            else
                            {
                                sensitivityPoint = sensitivity;
                                logoStr += logger.AdapterLogString(4, "AttPoint=" + testBerStruct.CsenStartingRxPwr.ToString() + "CSENCE>1E-12 ");
                                AnalysisOutputParameters(outputParameters);
                                logger.FlushLogBuffer();
                                return false;
                            }
                        }
                        logoStr += logger.AdapterLogString(1, "sensitivityPoint= " + sensitivityPoint.ToString());
                        AnalysisOutputParameters(outputParameters);
                        logger.FlushLogBuffer();
                        return true;
                    } 
                    else
                    {
                        logoStr += logger.AdapterLogString(0, "Step5...SerchTargetPoint");
                        ArrayList serchAttPoints = new ArrayList();
                        ArrayList serchBerPoints = new ArrayList();
                        attPoint = binarySearchTargetPoint(testBerStruct.CsenStartingRxPwr - testBerStruct.SearchTargetBerSubStep, testBerStruct.CsenStartingRxPwr + testBerStruct.SearchTargetBerAddStep, testBerStruct.SearchTargetBerLL, testBerStruct.SearchTargetBerUL, tempAtten, tempED, out serchAttPoints, out serchBerPoints);
                        logoStr += logger.AdapterLogString(1, "SetAtten=" + attPoint.ToString());
                        for (byte i = 0; i < serchAttPoints.Count;i++ )
                        {
                            logoStr += logger.AdapterLogString(1, "serchAttPoints[ " + i.ToString() + "]" + double.Parse(serchAttPoints[i].ToString()) + "  " + "serchBerPoints[ " + i.ToString() + "]" + double.Parse(serchBerPoints[i].ToString()));
                        }
                        if (attPoint == -10000)
                        {
                            sensitivityPoint = -10000;
                            AnalysisOutputParameters(outputParameters);
                            logger.FlushLogBuffer();
                            return false;
                        }
                        ArrayList attPoints;
                        ArrayList berPoints;
                        double intercept;
                        double slope;
                        logoStr += logger.AdapterLogString(0, "Step6...SerchCoefPoints");
                        if (testBerStruct.CsenTargetBER == 1E-3)
                        {
                            SerchCoefPointsTarget1E3(tempAtten, tempED, attPoint, testBerStruct.CsenTargetBER, testBerStruct.CoefCsenAddStep, testBerStruct.CoefCsenSubStep, out attPoints, out berPoints);
                            bool isDeleteOK=deleteErrBerPoints(berPoints, attPoints);
                            if (isDeleteOK==false)
                            {
                                logoStr += logger.AdapterLogString(3, "berPointsCount!=attPointsCount");
                                for (byte i = 0; i < berPoints.Count; i++)
                                {
                                    attPoints[i] = double.Parse(attPoints[i].ToString());
                                    berPoints[i] = double.Parse(berPoints[i].ToString());
                                    logoStr += logger.AdapterLogString(1, "attPoints[ " + i.ToString() + "]" + double.Parse(attPoints[i].ToString()) + "  " + "berPoints[ " + i.ToString() + "]" + double.Parse(berPoints[i].ToString()));
                                }
                                sensitivityPoint = -2000;
                                logger.FlushLogBuffer();
                                return false;
                            }
                            byte tmepCount = (byte)Math.Min(attPoints.Count, berPoints.Count);
                            double[] tempattPoints = new double[tmepCount];
                            double[] tempberPoints = new double[tmepCount];
                            for (byte i = 0; i < tmepCount; i++)
                            {
                                tempattPoints[i] = double.Parse(attPoints[i].ToString());
                                tempberPoints[i] = double.Parse(berPoints[i].ToString());
                                logoStr += logger.AdapterLogString(1, "attPoints[ " + i.ToString() + "]" + double.Parse(attPoints[i].ToString()) + "  " + "berPoints[ " + i.ToString() + "]" + double.Parse(berPoints[i].ToString()));

                            }
                            algorithm.LinearRegression(algorithm.Getlog10(algorithm.GetNegative(algorithm.Getlog10(tempberPoints))), tempattPoints, out slope, out intercept);
                            sensitivityPoint = slope + (intercept * System.Math.Log10(System.Math.Log10(testBerStruct.CsenTargetBER) * (-1)));
                        }
                        else if (testBerStruct.CsenTargetBER == 1E-12)
                        {
                            SerchCoefPoints1E12(tempAtten, tempED, attPoint, testBerStruct.CsenTargetBER, testBerStruct.CoefCsenAddStep, testBerStruct.CoefCsenSubStep, out attPoints, out berPoints);
                            bool isDeleteOK = deleteErrBerPoints(berPoints, attPoints);
                            if (isDeleteOK == false)
                            {
                                logoStr += logger.AdapterLogString(3, "berPointsCount!=attPointsCount");
                                for (byte i = 0; i < berPoints.Count; i++)
                                {
                                    attPoints[i] = double.Parse(attPoints[i].ToString());
                                    berPoints[i] = double.Parse(berPoints[i].ToString());
                                    logoStr += logger.AdapterLogString(1, "attPoints[ " + i.ToString() + "]" + double.Parse(attPoints[i].ToString()) + "  " + "berPoints[ " + i.ToString() + "]" + double.Parse(berPoints[i].ToString()));
                                }
                                sensitivityPoint = -2000;
                                AnalysisOutputParameters(outputParameters);
                                logger.FlushLogBuffer();
                                return false;
                            }
                            byte tmepCount = (byte)Math.Min(attPoints.Count, berPoints.Count);
                            double[] tempattPoints = new double[tmepCount];
                            double[] tempberPoints = new double[tmepCount];
                            for (byte i = 0; i < tmepCount; i++)
                            {
                                tempattPoints[i] = double.Parse(attPoints[i].ToString());
                                tempberPoints[i] = double.Parse(berPoints[i].ToString());
                                logoStr += logger.AdapterLogString(1, "attPoints[ " + i.ToString() + "]" + double.Parse(attPoints[i].ToString()) + "  " + "berPoints[ " + i.ToString() + "]" + double.Parse(berPoints[i].ToString()));
                            }
                            algorithm.LinearRegression(algorithm.Getlog10(algorithm.GetNegative(algorithm.Getlog10(tempberPoints))), tempattPoints, out slope, out intercept);
                            sensitivityPoint = slope + (intercept * System.Math.Log10(System.Math.Log10(testBerStruct.CsenTargetBER) * (-1)));
                        }
                        if (double.IsNaN(sensitivityPoint) || double.IsInfinity(sensitivityPoint))
                        {
                            sensitivityPoint = -1000;
                            AnalysisOutputParameters(outputParameters);
                            logger.FlushLogBuffer();
                            return false;
                        }
                        sensitivityPoint = Math.Round(sensitivityPoint, 2);
                        logoStr += logger.AdapterLogString(1, "sensitivityPoint= " + sensitivityPoint.ToString());
                        AnalysisOutputParameters(outputParameters);
                        logger.FlushLogBuffer();
                        return true;
                    }
                   
                }
                else
                {
                    logger.FlushLogBuffer();
                    return false;
                }
            }
            else
            {
                logoStr += logger.AdapterLogString(4, "Equipments is not enough!");
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
                if (selectedEquipList["ERRORDETE"] != null && selectedEquipList["ATTEN"] != null && selectedEquipList["POWERSUPPLY"] != null)
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

                    if (algorithm.FindFileName(InformationList, "CSEN(DBM)", out index))
                    {
                        if (testBerStruct.IsBerQuickTest == true)
                        {
                            InformationList[index].DefaultValue = sensitivityPoint.ToString().Trim();
                        }
                        else
                        {
                            InformationList[index].DefaultValue = Math.Round(sensitivityPoint, 4).ToString().Trim();
                        }
                       
                      
                    }
                    if (algorithm.FindFileName(InformationList, "CSEN(OMA)", out index))
                    {
                        sensOMA=algorithm.CalculateOMA(sensitivityPoint, Convert.ToDouble(OSERValueArray[GlobalParameters.CurrentChannel - 1]));
                        InformationList[index].DefaultValue = Math.Round(sensOMA, 4).ToString().Trim();

                    }

                }
               
                return true;
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
                bool isParametersComplete = false;
                for (byte i = 0; i < inPutParametersNameArray.Count; i++)
                {
                    if (algorithm.FindFileName(InformationList, inPutParametersNameArray[i].ToString(), out index) == false)
                    {
                        logoStr += logger.AdapterLogString(4,inPutParametersNameArray[i].ToString() + "is not exist");
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

                        if (algorithm.FindFileName(InformationList, "CSENALIGNRXPWR(DBM)", out index))
                        {                            
                            testBerStruct.CsenAlignRxPwr = Convert.ToDouble(InformationList[index].DefaultValue);
                           
                        }
                        if (algorithm.FindFileName(InformationList, "CSENSTARTINGRXPWR(DBM)", out index))
                        {
                            testBerStruct.CsenStartingRxPwr = Convert.ToDouble(InformationList[index].DefaultValue);
                           
                           
                        }
                        if (algorithm.FindFileName(InformationList, "SEARCHTARGETBERUL", out index))
                        {
                            testBerStruct.SearchTargetBerUL = Convert.ToDouble(InformationList[index].DefaultValue);
                           
                            
                        }
                        if (algorithm.FindFileName(InformationList, "SEARCHTARGETBERLL", out index))
                        {
                            testBerStruct.SearchTargetBerLL = Convert.ToDouble(InformationList[index].DefaultValue);
                            
                        }
                        if (algorithm.FindFileName(InformationList, "SEARCHTARGETBERADDSTEP", out index))
                        {
                            testBerStruct.SearchTargetBerAddStep = Convert.ToDouble(InformationList[index].DefaultValue); 
                            
                        }
                        if (algorithm.FindFileName(InformationList, "SEARCHTARGETBERSUBSTEP", out index))
                        {
                            testBerStruct.SearchTargetBerSubStep = Convert.ToDouble(InformationList[index].DefaultValue);
                            
                            
                        }
                        if (algorithm.FindFileName(InformationList, "CSENTARGETBER", out index))
                        {
                            testBerStruct.CsenTargetBER = Convert.ToDouble(InformationList[index].DefaultValue);                            
                           
                        }
                        if (algorithm.FindFileName(InformationList, "COEFCSENSUBSTEP(DBM)", out index))
                        {
                            testBerStruct.CoefCsenSubStep = Convert.ToDouble(InformationList[index].DefaultValue);
                           
                          
                        }
                        if (algorithm.FindFileName(InformationList, "COEFCSENADDSTEP(DBM)", out index))
                        {
                            testBerStruct.CoefCsenAddStep = Convert.ToDouble(InformationList[index].DefaultValue);  
                           
                        }
                        if (algorithm.FindFileName(InformationList, "ISBERQUICKTEST", out index))
                        {
                            testBerStruct.IsBerQuickTest = Convert.ToBoolean(InformationList[index].DefaultValue);

                        }
                    }

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

            } while (i < 8 && (currentBer < (targetBer * 1E-2)));

            return true;
        }
        protected bool SerchCoefPoints1E12(Attennuator tempAtt, ErrorDetector tempED, double startAttValue, double targetBer, double addStep, double sumStep, out ArrayList AttenPoints, out ArrayList BerPints)
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

            } while (i < 8 && (currentBer > (targetBer * 100)));

            return true;
        }
        protected  bool deleteErrBerPoints(ArrayList inputARR1, ArrayList inputARR2)
        {
            if (inputARR1.Count != inputARR2.Count)
            {
                return false;
            }
            for (byte i = 0; i < inputARR1.Count; i++)
            {
                if (Convert.ToDouble(inputARR1[i]) >=1)
                {
                    inputARR1.RemoveAt(i);
                    inputARR2.RemoveAt(i);
                }

            }
            for (byte i = 0; i < inputARR1.Count; i++)
            {
                if (double.IsNaN(Convert.ToDouble(inputARR1[i])) || double.IsInfinity(Convert.ToDouble(inputARR1[i])))
                {
                    inputARR1.RemoveAt(i);
                    inputARR2.RemoveAt(i);
                }               

            }
            for (byte i = 0; i < inputARR1.Count; i++)
            {
                if (inputARR1[i]==null)
                {
                    inputARR1.RemoveAt(i);
                    inputARR2.RemoveAt(i);
                }

            }
            for (byte i = 0; i < inputARR2.Count; i++)
            {
                if (inputARR2[i] == null)
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
        
#endregion
       
      
    }
}

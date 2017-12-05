﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.Threading;

namespace ATS_Framework
{
    class AdjustTxADC : TestModelBase
    {
        private struct InParaOfAdjust
        {
            private byte id;

            public ArrayList ModDACs{ get; set; }

            public ArrayList BiasDACs{ get; set; }    
                    
            public byte MethodID 
            {
                get
                {
                    return this.id;
                }
                set
                {
                    if (value < 0)
                    {
                        throw new IndexOutOfRangeException("MethodID is less than zero.");
                    }
                    else
                    {
                        this.id = value;
                    }
                }
            }
        }

        private Powersupply supply;

        private InParaOfAdjust myStruct;

        private DataTable dt;

        private int countOfTest;

        private double[] matrixOffsetADC;

        private double[,] matrixEachEnADC;

        private bool readyToCalculate = true;

        public AdjustTxADC(DUT inDut)
        {
            
            logoStr = null;
            dut = inDut;           

            dt = new DataTable();
            DataColumn dc = dt.Columns.Add("TestDataID", typeof(int));
            dc.AllowDBNull = false;
            dc.Unique = true;
            dc.AutoIncrement = true;

            dt.Columns.Add("Time", typeof(string));
            dt.Columns.Add("Temp", typeof(double));
            dt.Columns.Add("Channel", typeof(int));
            dt.Columns.Add("TxDAC", typeof(double));
            dt.Columns.Add("Enable", typeof(string));
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
                if (!selectedEquipList.Values[i].Configure())
                {
                    return false;
                }
            }
            return true;
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

        public override bool SelectEquipment(EquipmentList equipmentList)
        {    
            selectedEquipList.Clear();

            if (equipmentList.Count == 0)
            {
                selectedEquipList.Add("DUT", dut);
                return false;
            }
            else
            {
                bool isOK = false;

                for (byte i = 0; i < equipmentList.Count; i++)
                {
                    if (equipmentList.Keys[i].ToUpper().Contains("POWERSUPPLY"))
                    {
                        supply = (Powersupply)equipmentList.Values[i];
                        isOK = true;
                    }                    

                    if (equipmentList.Keys[i].ToUpper().Contains("NA_OPTICALSWITCH"))
                    {
                        equipmentList.Values[i].CheckEquipmentRole(1, GlobalParameters.CurrentChannel);
                    }
                }

                if (supply != null)
                {
                    isOK = true;
                }
                else
                {
                    if (supply == null)
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

        private void OutPutandFlushLog()
        {
            try
            {
                //AnalysisOutputProcData(procData);
                
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

        protected override bool AnalysisInputParameters(TestModeEquipmentParameters[] infoList)
        {
            try
            {
                ArrayList inParaList = new ArrayList();
                inParaList.Clear();

                inParaList.Add("BiasDACArray");
                inParaList.Add("ModDACArray");
                inParaList.Add("AdjustMethod");

                Log.SaveLogToTxt("Step1...Check InputParameters");

                if (infoList.Length < inParaList.Count)
                {
                    Log.SaveLogToTxt("InputParameters are not enough!");
                    return false;
                }
                else
                {
                    int index = -1;
                    bool isParaCompleted = true;

                    if (isParaCompleted)
                    {
                        if (Algorithm.FindFileName(infoList, "BIASDACARRAY", out index))
                        {
                            char[] tempCharArray = new char[] { ',' };
                            ArrayList tempAR = Algorithm.StringtoArraylistDeletePunctuations(infoList[index].DefaultValue, tempCharArray);
                            if (tempAR == null)
                            {
                                Log.SaveLogToTxt(infoList[index].FiledName + "is null!");
                                return false;
                            }
                            else if (tempAR.Count > GlobalParameters.TotalChCount)
                            {
                                myStruct.BiasDACs = new ArrayList();
                                for (int i = 0; i < GlobalParameters.TotalChCount; i++)
                                {
                                    myStruct.BiasDACs.Add(tempAR[i]);
                                }
                            }
                            else
                            {
                                myStruct.BiasDACs = tempAR;
                            }
                        }

                        if (Algorithm.FindFileName(infoList, "MODDACARRAY", out index))
                        {
                            char[] tempCharArray = new char[] { ',' };
                            ArrayList tempAR = Algorithm.StringtoArraylistDeletePunctuations(infoList[index].DefaultValue, tempCharArray);
                            if (tempAR == null)
                            {
                                Log.SaveLogToTxt(infoList[index].FiledName + "is null!");
                                return false;
                            }
                            else if (tempAR.Count > GlobalParameters.TotalChCount)
                            {
                                myStruct.ModDACs = new ArrayList();
                                for (int i = 0; i < GlobalParameters.TotalChCount; i++)
                                {
                                    myStruct.ModDACs.Add(tempAR[i]);
                                }
                            }
                            else
                            {
                                myStruct.ModDACs = tempAR;
                            }
                        }

                        if (Algorithm.FindFileName(infoList, "ADJUSTMETHOD", out index))
                        {
                            double temp = Convert.ToDouble(infoList[index].DefaultValue);
                            if (double.IsInfinity(temp) || double.IsNaN(temp))
                            {
                                Log.SaveLogToTxt(infoList[index].FiledName + "is wrong!");
                                return false;
                            }
                            else
                            {
                                myStruct.MethodID = Convert.ToByte(temp);
                            }
                        }

                        Log.SaveLogToTxt("OK!");
                        return true;
                    }
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
                InnoExCeption ex = new InnoExCeption(ExceptionDictionary.Code._0x02001, error.StackTrace);
                //Log.SaveLogToTxt(ex.ID + ": " + ex.Message + "\r\n" + ex.StackTrace);
                exceptionList.Add(ex);
                return false;
                //the other way is: should throw exception, rather than the above three code. see below:
                //throw new InnoExCeption(ExceptionDictionary.Code._0x02001, error.StackTrace); 
            }
        }

        protected override bool StartTest()
        {
            
            logoStr = "";

            if (supply != null)
            {
                CloseandOpenAPC(Convert.ToByte(APCMODE.IBAISandIMODOFF));
            }

            supply.OutPutSwitch(false);
            Thread.Sleep(20);
            supply.OutPutSwitch(true);

            bool result = Test();

            dut.TxAllChannelEnable();
            OutPutandFlushLog();

            return result;
        }

        public override bool Test()
        {
            if (!this.AnalysisInputParameters(inputParameters))
            {
                return false;
            }

            //initial matrix
            if ((matrixOffsetADC == null) || (matrixEachEnADC == null))
            {
                matrixOffsetADC = new double[GlobalParameters.TotalChCount];
                matrixEachEnADC = new double[GlobalParameters.TotalChCount, GlobalParameters.TotalChCount];
            }

            if (this.countOfTest == 0)
            {
                if (!this.ReadOffsetADC(ref matrixOffsetADC))
                {
                    return false;
                }
            }

            if (!this.ReadEachChEnADC(GlobalParameters.CurrentChannel, ref matrixEachEnADC))
            {                
                return false;
            }

            if ((this.countOfTest == 4) && (readyToCalculate == true))
            {
                readyToCalculate = this.CalculateAndWriteCoeff();
                supply.OutPutSwitch(false);
                Thread.Sleep(20);
                supply.OutPutSwitch(true);
            }

            return readyToCalculate;
        }

        private bool ReadOffsetADC(ref double[] offset)
        {
            try
            { 
                Log.SaveLogToTxt("Read offset");
                if (!ReadTotalChADC("non", 0, out offset))
                {
                    return false;
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
            finally
            {
                dut.ChangeChannel(GlobalParameters.CurrentChannel.ToString());
            }
        }

        private bool ReadEachChEnADC(int channel, ref double[,] dataADC)
        {
            try
            {               

                Log.SaveLogToTxt("Check whether this channel was tested.");                
                string[] enbleInfo = { "onlych1", "onlych2", "onlych3", "onlych4" };                
                string expression = "Enable = " + "'" + enbleInfo[channel - 1] + "'";
                DataRow[] foundRows = dt.Select(expression);

                if (foundRows.Length == 0)
                {
                    Log.SaveLogToTxt("This channel was not tested.");
                    this.countOfTest++;
                }
                else
                {
                    Log.SaveLogToTxt("This channel was tested.");
                }

                Log.SaveLogToTxt("Read Single Enable" + channel);
                double[] readData;
                if (!ReadTotalChADC(enbleInfo[channel - 1], channel, out readData))
                {
                    return false;
                }

                if (readData[channel - 1] < 100)
                {
                    Log.SaveLogToTxt("ADC is less than 100. MPD error");
                    readyToCalculate = false;
                    return false;
                }

                for (int i = 0; i < GlobalParameters.TotalChCount; i++)
                {                    
                    dataADC[i, channel - 1] = readData[i];
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

        private bool ReadTotalChADC(string info, int channel, out double[] dataADC)
        {
            dataADC = new double[GlobalParameters.TotalChCount];
            string time = DateTime.Now.ToString();
            try
            {
                Log.SaveLogToTxt("Try to Clean offset/coeff.");
                if (!this.CleanCoeff())
                {
                    Log.SaveLogToTxt("Failed to Clean offset/coeff.");
                    return false;
                }
                Log.SaveLogToTxt("Clean offset/coeff sucessfully.");

                if (channel == 0)
                {
                    Log.SaveLogToTxt("Disable all channel");
                    for (int i = 0; i < GlobalParameters.TotalChCount; i++)
                    {
                        dut.ChangeChannel((i + 1).ToString());
                        dut.SetSoftTxDis();
                    }
                }
                else
                {
                    Log.SaveLogToTxt("Enable channel " + channel + "and Write bias/mod DAC");
                    dut.SetSingleChannelTxEnable((byte)channel);
                    dut.WriteBiasDac(myStruct.BiasDACs[channel - 1]);
                    dut.WriteModDac(myStruct.ModDACs[channel - 1]);
                }

                for (int i = 0; i < GlobalParameters.TotalChCount; i++)
                {
                    dut.ChangeChannel((i + 1).ToString());
                    ushort data;
                    if (dut.ReadTxpADC(out data))
                    {
                        dataADC[i] = data;
                    }
                    else
                    {
                        Log.SaveLogToTxt("failed to read TxADC of channel" + (i + 1));
                        return false;
                    }

                    DataRow dr = dt.NewRow();
                    dr[1] = time;
                    dr[2] = GlobalParameters.CurrentTemp;
                    dr[3] = dut.MoudleChannel;
                    dr[4] = dataADC[i];
                    dr[5] = info;
                    dt.Rows.Add(dr);
                    string readInfo = time + " " + GlobalParameters.CurrentTemp + " " + dut.MoudleChannel + " " + dataADC[i].ToString("f1") + " " + info;
                    Log.SaveLogToTxt("read info: " + readInfo);
                    dr = null;                    
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
            finally
            {
                dut.ChangeChannel(GlobalParameters.CurrentChannel.ToString());
            }
        }

        private bool CleanCoeff()
        {
            try
            {
                bool result = true;                
                double[] matrixOffsetClosed = new double[GlobalParameters.TotalChCount];
                double[,] matrixCoeffClosed = new double[GlobalParameters.TotalChCount, GlobalParameters.TotalChCount];

                for (int i = 0; i < matrixOffsetClosed.GetLength(0); i++)
                {
                    matrixOffsetClosed[i] = double.NaN;
                    dut.ChangeChannel((i + 1).ToString());
                    result = result && dut.WriterTxAdcBacklightOffset(matrixOffsetClosed[i]);
                }

                for (int i = 0; i < matrixCoeffClosed.GetLength(0); i++)
                {
                    for (int j = 0; j < matrixCoeffClosed.GetLength(1); j++)
                    {
                        matrixCoeffClosed[i, j] = double.NaN;
                    }
                }

                result = result && dut.WriterTxAdcCalibrateMatrix(matrixCoeffClosed);

                supply.OutPutSwitch(false);
                Thread.Sleep(20);
                supply.OutPutSwitch(true);
                //close and open suppler
                return result;
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
            finally
            {
                dut.ChangeChannel(GlobalParameters.CurrentChannel.ToString());
            }
        }

        private bool CalculateAndWriteCoeff()
        {
            try
            {
                bool result = true;

                //减offset
                double[,] matrixX = new double[matrixEachEnADC.GetLength(0), matrixEachEnADC.GetLength(1)];
                for (int i = 0; i < matrixEachEnADC.GetLength(0); i++)
                {
                    string info = "";
                    for (int j = 0; j < matrixEachEnADC.GetLength(1); j++)
                    {
                        info += matrixEachEnADC[i, j].ToString("f1") + "   ";
                        matrixX[i, j] = matrixEachEnADC[i, j] - matrixOffsetADC[i];
                    }
                    //log matrixSingleEnable and offset
                    Log.SaveLogToTxt("matrixSingleEnable: " + info + "matrixOffset: " + matrixOffsetADC[i]);
                }

                //计算系数
                double[,] coeff = Algorithm.CalculateCoeff(matrixX, 0);

                //log and write coeff to DUT
                result = dut.WriterTxAdcCalibrateMatrix(coeff);
                for (int i = 0; i < coeff.GetLength(0); i++)
                {
                    string info = "";
                    for (int j = 0; j < coeff.GetLength(1); j++)
                    {
                        info += coeff[i, j].ToString("f4") + "   ";
                    }
                    Log.SaveLogToTxt("coeff: " + info);
                }
                Log.SaveLogToTxt("Write coeff to dut sucessfully");

                //write offset to DUT;
                for (int i = 0; i < matrixOffsetADC.GetLength(0); i++)
                {
                    dut.ChangeChannel((i + 1).ToString());
                    result = result && dut.WriterTxAdcBacklightOffset(matrixOffsetADC[i]);
                }
                Log.SaveLogToTxt("Write offset to dut sucessfully");

                return result;
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
                InnoExCeption ex = new InnoExCeption(ExceptionDictionary.Code._0x02100, error.StackTrace);
                //Log.SaveLogToTxt(ex.ID + ": " + ex.Message + "\r\n" + ex.StackTrace);
                exceptionList.Add(ex);
                return false;
                //the other way is: should throw exception, rather than the above three code. see below:
                //throw new InnoExCeption(ExceptionDictionary.Code._0x02100, error.StackTrace); 
            }
            finally
            {
                dut.ChangeChannel(GlobalParameters.CurrentChannel.ToString());
            }
        }

        public override List<InnoExCeption> GetException()
        {
            return base.GetException();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ATS_Framework;
namespace ATS
{
    public class JudgeSpec
    {
        public DataTable dtSpec;

        public globalParameters MyGlobalParameters ;
        private static object syncRoot = new object();//used for thread synchronization
        public bool Judge(DataTable dt,TestModeEquipmentParameters[] pOutPutArray,bool TestModelflag,bool flag_TestModelFailBreak, out DataTable Outdt, out bool Breakflag, out bool Resultflag)
        {
            lock (syncRoot)
            {
                double SpecMin = -32767;
                double SpecMax = 32768;
                int i;

                double CurrentValue = 9e+10;
                Resultflag = true;

                Breakflag = false;
                Outdt = dt.Clone();

                if (pOutPutArray == null)
                {

                    return true;
                }

                for (i = 0; i < pOutPutArray.Length; i++)
                {
                    DataRow dr = Outdt.NewRow();

                    dr["Temp"] = MyGlobalParameters.CurrentTemp;
                    dr["Vcc"] = MyGlobalParameters.CurrentVcc;
                    dr["Channel"] = MyGlobalParameters.CurrentChannel;
                    dr["ItemName"] = pOutPutArray[i].FiledName;

                    try
                    {
                        CurrentValue = Convert.ToDouble(pOutPutArray[i].DefaultValue);
                        dr["ItemValue"] = CurrentValue;
                    }
                    catch
                    {
                        dr["ItemValue"] = 9E+10;
                    }
                    if (GetSpec(pOutPutArray[i].FiledName, out  SpecMax, out SpecMin))
                    {
                        if (CurrentValue >= SpecMin && CurrentValue <= SpecMax)
                        {
                            dr["Result"] = "Pass";
                        }
                        else
                        {
                            dr["Result"] = "Fail";
                            Resultflag = false;
                        }
                    }
                    else
                    {
                        dr["Result"] = "Pass";
                    }

                    dr["ItemName"] = pOutPutArray[i].FiledName;
                    dr["SpecMin"] = SpecMin;
                    dr["SpecMax"] = SpecMax;
                    Outdt.Rows.Add(dr);

                }
                if (flag_TestModelFailBreak)
                {
                    if (Resultflag == false || TestModelflag == false)
                    {
                        Breakflag = true;
                    }
                }

                return true;
            }
           // Outdt = Indt;
        }
        public bool GetSpec(string FiledName,out Double Max,out double Min)
        {
            lock (syncRoot)
            {
                Max = 32768;
                Min = -32767;
                byte Channel;
                // "FullName='" + FiledName + "'",
                try
                {
                    DataTable dtSelect;
                    if (GetDataTable(dtSpec, "FullName='" + FiledName + "'", out dtSelect))// 寻找项目名称是否存在输出结构体中
                    {
                        if (dtSelect.Rows.Count == 1)
                        {
                            Channel = Convert.ToByte(dtSelect.Rows[0]["Channel"]);

                            if (Channel == 0 || Channel == MyGlobalParameters.CurrentChannel)
                            {
                                Min = Convert.ToDouble(dtSelect.Rows[0]["SpecMin"]);
                                Max = Convert.ToDouble(dtSelect.Rows[0]["SpecMax"]);
                            }

                        }
                        else
                        {
                            for (int i = 0; i < dtSelect.Rows.Count; i++)
                            {
                                Channel = Convert.ToByte(dtSelect.Rows[i]["Channel"]);

                                if (Channel == MyGlobalParameters.CurrentChannel)
                                {
                                    Min = Convert.ToDouble(dtSelect.Rows[i]["SpecMin"]);
                                    Max = Convert.ToDouble(dtSelect.Rows[i]["SpecMax"]);
                                }
                            }
                        }


                    }
                }
                catch
                {


                }
                finally
                {

                }
                if (Max != 32768 || Min != -32767)
                {
                    return true;
                }
                else
                {
                    return false;
                }


            }

        }

        public bool GetDataTable(DataTable inputDT, string filterExpression, out DataTable dt)
        {
            lock (syncRoot)
            {
                try
                {

                    dt = inputDT.Clone();
                    DataRow[] drData = inputDT.Select(filterExpression);


                    for (int i = 0; i <= drData.Length - 1; i++)
                    {
                        dt.ImportRow((DataRow)drData[i]);
                    }
                    return true;
                }
                catch (System.Exception ex)
                {

                    throw ex;
                }

            }
        }
        private bool GetPrameterId(String TestItemName, out  int IndexdtSpec)
        {
            lock (syncRoot)
            {
                IndexdtSpec = -1;

                try
                {
                    DataRow[] drArray = dtSpec.Select("FullName='" + TestItemName + "'");
                    IndexdtSpec = Convert.ToInt16(drArray[0]["Id"]);
                    if (IndexdtSpec == -1)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }

                }
                catch (System.Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("Select Command Error");
                    return false;
                }

                //return false;
            }
        }
    }

    

}

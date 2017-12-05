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
       
        private logManager MyLogManager;
       private Algorithm pAlgorthm = new Algorithm();
        public JudgeSpec(logManager plogManager)
        {
            MyLogManager = plogManager;
        }
        public bool Judge(DataTable dt,TestModeEquipmentParameters[] pOutPutArray,bool TestModelflag,bool flag_TestModelFailBreak, out DataTable Outdt, out bool Breakflag, out bool Resultflag)
        {
            double SpecMin = -32767;
            double SpecMax = 32768;
            int i;
           
            double CurrentValue=9e+10;
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
                if( GetSpec(pOutPutArray[i].FiledName,out  SpecMax, out SpecMin))
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
                if (Resultflag==false||TestModelflag==false)
                {
                    Breakflag = true;
                }
            }

            return true;
           // Outdt = Indt;
        }
        public bool GetSpec(string FiledName,out Double Max,out double Min)
        {
            Max = 32768;
            Min = -32767;
            // "FullName='" + FiledName + "'",
            try
            {


                DataTable dtSelect;
                if (GetDataTable(dtSpec, "FullName='" + FiledName + "'", out dtSelect))// 寻找项目名称是否存在输出结构体中
                {

                    if (dtSelect.Rows.Count == 1)
                    {
                        DataRow[] drArray = dtSelect.Select("FullName='" + FiledName + "'");
                        if (drArray.Length == 1)
                        {
                            Min = Convert.ToDouble(drArray[0]["SpecMin"]);
                            Max = Convert.ToDouble(drArray[0]["SpecMax"]);
                            return true;
                        }
                        //else
                        //{
                        //    drArray = dtSelect.Select("FullName='" + FiledName + MyGlobalParameters.CurrentChannel.ToString() + "'");
                        //    if (drArray.Length == 1)
                        //    {
                        //        Min = Convert.ToDouble(drArray[0]["SpecMin"]);
                        //        Max = Convert.ToDouble(drArray[0]["SpecMax"]);
                        //    }

                        //}
                    }
                    //else if (dtSelect.Rows.Count > 1)
                    //{
                    //    DataRow[] drArray = dtSelect.Select("FullName='" + FiledName + MyGlobalParameters.CurrentChannel.ToString() + "'");

                    //    if (drArray.Length >= 1)
                    //    {
                    //        Min = Convert.ToDouble(drArray[0]["SpecMin"]);
                    //        Max = Convert.ToDouble(drArray[0]["SpecMax"]);
                    //    }

                    //}

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

        public bool GetDataTable(DataTable inputDT, string filterExpression, out DataTable dt)
        {
            try
            {

                dt = inputDT.Clone();
                DataRow[]  drData = inputDT.Select(filterExpression);


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
        private bool GetPrameterId(String TestItemName, out  int IndexdtSpec)
        {
            IndexdtSpec = -1;

            try
            {
                DataRow[] drArray = dtSpec.Select("FullName='" + TestItemName + "'");
                IndexdtSpec = Convert.ToInt16(drArray[0]["Id"]);
                if (IndexdtSpec==-1)
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

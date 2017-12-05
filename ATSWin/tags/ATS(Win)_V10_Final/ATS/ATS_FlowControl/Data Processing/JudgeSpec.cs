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
        private logManager MyLogManager;

        public JudgeSpec(logManager plogManager)
        {
            MyLogManager = plogManager;
        }
        public void Judge(DataTable Indt, TestModeEquipmentParameters[] pOutPutArray,bool TestModelflag,bool flag_TestModelFailBreak, out DataTable Outdt, out bool Breakflag, out bool Resultflag)
        {
          
            int i;
            int StructIndex;
            double CurrentValue=9e+10;
             Resultflag = true;
            Algorithm pAlgorthm = new Algorithm();
           
            int RowsCount = Indt.Rows.Count;

            Breakflag = false;
            string StrName = null;
         //   foreach (DataRow dr in Indt.Rows)

            for (i = 0; i < RowsCount; i++)
            {
                DataRow dr = Indt.Rows[i];
                if (pAlgorthm.FindFileName(pOutPutArray, dr["ItemName"].ToString().ToUpper().Trim(), out StructIndex))// 寻找项目名称是否存在输出结构体中
                {
                    dr["Value"] = pOutPutArray[StructIndex].DefaultValue;

                    if (dr["ItemSpecific"].ToString().ToUpper().Trim() == "1")//需要比对规格的参数
                    {
                        try
                        {
                            CurrentValue = Convert.ToDouble(pOutPutArray[StructIndex].DefaultValue);
                            dr["Value"] = CurrentValue;
                        }
                        catch
                        {
                            dr["Value"] = 9E+10;
                        }

                        StrName += dr["ItemName"].ToString();



                        double SpecMin = Convert.ToDouble(dr["SpecMin"]);
                        double SpecMax = Convert.ToDouble(dr["SpecMax"]);
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
                    else//不需要规格比对
                    {
                        if (dr["DataRecord"].ToString().ToUpper().Trim() == "1")
                        {
                            dr["Result"] = "Pass";
                            dr["SpecMin"] = -32768;
                            dr["SpecMax"] = 32767;

                        }

                    }
                  
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("I Can't find"+dr["ItemName"].ToString().Trim());

                    dr["Result"] = "Fail";
                    Resultflag = false;
                    MyLogManager.AdapterLogString(3, "I Can't find" + dr["ItemName"].ToString().Trim());
                }
                
            }
            if (flag_TestModelFailBreak)
            {
                if (Resultflag==false||TestModelflag==false)
                {
                    Breakflag = true;
                }
            }


            Outdt = Indt;
        }
    }

    

}

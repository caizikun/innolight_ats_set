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

        public void Judge(DataTable Indt, TestModeEquipmentParameters[] pOutPutArray,bool TestModelflag, out DataTable Outdt, out bool Breakflag, out bool Resultflag)
        {
          
            int i;
            int StructIndex;
            double CurrentValue;
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

                    if (dr["ItemSpecific"].ToString().ToUpper().Trim() == "1" && TestModelflag==true)//需要比对规格的参数
                    {
                        StrName += dr["ItemName"].ToString();
                        CurrentValue = Convert.ToDouble(pOutPutArray[StructIndex].DefaultValue);
                        dr["Value"] = CurrentValue;
                        double SpecMin = Convert.ToDouble(dr["DefaultLowLimit"]);
                        double SpecMax = Convert.ToDouble(dr["DefaultUpperLimit"]);
                        if (CurrentValue > SpecMin && CurrentValue < SpecMax)
                        {
                            dr["Result"]  =true;
                        }
                        else
                        {
                            Resultflag = false;
                        }
                    }
                    else//不需要比对规格的参数,但需要存储
                    {
                        dr["Result"] = true;
                    }
                }
                else
                {
                    dr["Result"] = false;
                    Resultflag = false;
                }
                if (dr["Result"].ToString().ToUpper().Trim() != "TRUE" && dr["Failbreak"].ToString().ToUpper().Trim() == "1")
                {
                    Resultflag = false;
                    Breakflag = true;

                }
            }
            
            Outdt = Indt;
        }
    }

    

}

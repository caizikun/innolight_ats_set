using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Data;

namespace ATS
{
   public class WriteTxT
    {
       //Directory
       public void Write(String StartTime,String StrWrite,string Endtime)
       {
           FileStream pfs;
           StreamWriter sw;
           string pathString = Application.StartupPath+"\\Log";

           string StrYear = DateTime.Now.Year.ToString();
           string StrMonth = DateTime.Now.Month.ToString();
           string StrDay = DateTime.Now.Day.ToString();

           if (!Directory.Exists(pathString))//Log
           {
              Directory.CreateDirectory(pathString);

           }
         pathString += "\\"+StrYear;

        if (!Directory.Exists(pathString))//Year
        {
           
            Directory.CreateDirectory(pathString);
           
        }
        pathString += "\\"+StrMonth;

        if (!Directory.Exists(pathString))//Month
        {
            Directory.CreateDirectory(pathString);

        }
        pathString += "\\"+StrDay+".txt";

        if (!File.Exists(pathString))//Dayc
        {
           sw= File.CreateText(pathString);
           

        }
        else
        {
            sw = File.AppendText(pathString);
        }


        
        sw.WriteLine(StartTime);
        sw.WriteLine(StrWrite);
        sw.WriteLine(Endtime);
        sw.Close();
        sw.Dispose();

       }
       public void Write(String StrWrite)
       {
           FileStream pfs;
           StreamWriter sw;
           string pathString = Application.StartupPath + "\\Log";

           string StrYear = DateTime.Now.Year.ToString();
           string StrMonth = DateTime.Now.Month.ToString();
           string StrDay = DateTime.Now.Day.ToString();

           if (!Directory.Exists(pathString))//Log
           {
               Directory.CreateDirectory(pathString);

           }
           pathString += "\\" + StrYear;

           if (!Directory.Exists(pathString))//Year
           {

               Directory.CreateDirectory(pathString);

           }
           pathString += "\\" + StrMonth;

           if (!Directory.Exists(pathString))//Month
           {
               Directory.CreateDirectory(pathString);

           }
           pathString += "\\" + StrDay + ".txt";

           if (!File.Exists(pathString))//Dayc
           {
               sw = File.CreateText(pathString);


           }
           else
           {
               sw = File.AppendText(pathString);
           }


           sw.WriteLine(StrWrite);
      
           sw.Close();
           sw.Dispose();

       }
    }
}

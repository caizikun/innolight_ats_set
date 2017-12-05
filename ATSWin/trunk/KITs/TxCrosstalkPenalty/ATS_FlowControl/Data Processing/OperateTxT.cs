using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
　using System.IO;

　using　System.Text;
using System.Windows.Forms;


namespace ATS
{
   public class OperateTxT
   {
       private string Myfilepath;
       public OperateTxT(string filepath)
       {
           Myfilepath = filepath;
           FileStream fs = new FileStream(Myfilepath, FileMode.OpenOrCreate);
           StreamWriter sw = new StreamWriter(fs);
          
           sw.Close();
           fs.Close();
        
       }
       //public bool WriteTxt(string filepath, string Content)
       //{
       //    try
       //    {
       //         FileStream fs = new FileStream(filepath, FileMode.OpenOrCreate);
       //         StreamWriter sw = new StreamWriter(fs);
       //         sw.WriteLine(Content);
       //         sw.Close();
       //         fs.Close();
       //         return true;
       //    }
       //    catch 
       //    {
       //        return false;
       //    }
          
       //}
       //public bool WriteTxt(string Content)
       //{
       //    try
       //    {
       //        FileStream fs = new FileStream(Myfilepath, FileMode.Append);
       //        StreamWriter sw = new StreamWriter(fs);
       //        sw.WriteLine(Content);
       //        sw.Close();
       //        fs.Close();
       //        return true;
       //    }
       //    catch
       //    {
       //        return false;
       //    }

       //}

       public bool WriteTxt(string[] Content)
       {
           try
           {
               FileStream fs = new FileStream(Myfilepath, FileMode.Append);
               StreamWriter sw = new StreamWriter(fs);

               string Str = "";

               for (int i = 0; i < Content.Length;i++ )
               {
                   Str += Content[i] + "\t";
               }



               sw.WriteLine(Str);
               sw.Close();
               fs.Close();
               return true;
           }
           catch
           {
               System.Windows.Forms. MessageBox.Show("Write Txt Error");
               return false;
           }

       }
   }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ATS
{
     public class CommonMethod
    {
         public string AnalysisString(string StrIndex, string StrAuxAttribles)
         {

             try
             {
                 string[] StrArray = StrAuxAttribles.ToUpper().Split(';');
                 foreach (string Str in StrArray)
                 {
                     if (Str.Contains(StrIndex.ToUpper()))
                     {
                         string[] Array1 = Str.Split('=');
                         return Array1[1];
                     }
                 }


                 MessageBox.Show("We Can't find " + StrIndex + " in StrAuxAttribles");
                 return null;
             }
             catch
             {
                 return null;
             }

         }

         // Public implementation of Dispose pattern callable by consumers.
         bool disposed = false;
         public void Dispose()
         {
             Dispose(true);
             GC.SuppressFinalize(this);
         }

         // Protected implementation of Dispose pattern.
         private  void Dispose(bool disposing)
         {
             if (disposed)
                 return;

             if (disposing)
             {
                 // Free any other managed objects here.
                 //
             }

             // Free any unmanaged objects here.
             //
             disposed = true;
         }

    }

    }

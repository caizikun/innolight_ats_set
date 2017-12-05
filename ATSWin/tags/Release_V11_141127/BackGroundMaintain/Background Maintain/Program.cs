using System;
using System.Collections.Generic;

using System.Windows.Forms;

namespace GlobalInfo
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]

        static void Main()
        {
            bool createNew;
            try
            {
                using (System.Threading.Mutex m = new System.Threading.Mutex(true, "Global\\" + Application.ProductName, out createNew))
                {
                    if (createNew)
                    {
                        Application.EnableVisualStyles();
                        Application.SetCompatibleTextRenderingDefault(false);
                        Application.Run(new Login());
                    }
                    else
                    {
                        MessageBox.Show(Application.ProductName + ": The program has been running!");
                    }
                }

            }
            catch(Exception ex)
            {
                if (ex != null)
                { 
                    MessageBox.Show(ex.ToString());
                }
                else
                {
                    MessageBox.Show(Application.ProductName + "程式已经运行!!!");
                }
            }
        }
    }
}

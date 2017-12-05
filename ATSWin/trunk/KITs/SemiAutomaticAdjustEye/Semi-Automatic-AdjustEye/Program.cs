using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Semi_Automatic_AdjustEye
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Semi_Automatic_AdjustEye());
        }
    }
}

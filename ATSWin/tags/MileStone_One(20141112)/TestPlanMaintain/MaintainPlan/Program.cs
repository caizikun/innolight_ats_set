using System;
using System.Collections.Generic;

using System.Windows.Forms;

namespace TestPlan
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
                    bool newAppExist = blnServerAPPIsNew();
                    string localAppPath = Application.ExecutablePath;
                    if (createNew || newAppExist)
                    {                        
                        Application.EnableVisualStyles();
                        Application.SetCompatibleTextRenderingDefault(false);

                        if (newAppExist)
                        {
                            //开始自动更新! //140721 TBD                                
                            //System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo(serverAppPath);
                            //System.Diagnostics.Process newProcess = System.Diagnostics.Process.Start(startInfo);
                            
                            new System.Threading.Thread(() =>
                                System.Diagnostics.Process.Start(localAppPath)).Start();
                            //Process.Start(Application.StartupPath + "\\update" + "\\DownLoadUpdate.exe");
                            Environment.Exit(0);
                        }
                        else
                        {
                            Application.Run(new Login());
                        }                        
                    }
                    else
                    {
                        MessageBox.Show(Application.ProductName + "程式已经运行!!!");
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

        static bool blnServerAPPIsNew()
        {        
            bool result=false;
            string serverAppPath = @"\\inpcsz0518\SoftWare\" + Application.ProductName + ".exe";
            string localAppPath = Application.ExecutablePath;

            try{
                if (System.IO.File.Exists(serverAppPath))
                {

                    //获取APP修改时间
                    DateTime serverAppTime = System.IO.File.GetLastWriteTime(serverAppPath);
                    DateTime localAppTime = System.IO.File.GetLastWriteTime(localAppPath);

                    System.Diagnostics.FileVersionInfo serverFile = System.Diagnostics.FileVersionInfo.GetVersionInfo(serverAppPath);
                    string serverAPPVersions = serverFile.ProductVersion;
                    string localAppVersion = Application.ProductVersion.ToString();
                    System.Diagnostics.FileVersionInfo localFile = System.Diagnostics.FileVersionInfo.GetVersionInfo(localAppPath);
                    string localAPPVersions = localFile.ProductVersion;

                    string[] serverAppVerArry = serverAPPVersions.Split('.');
                    string[] localAppVerArry = localAppVersion.Split('.');
                    bool serverAppIsNew = false;

                    if (serverAppVerArry.Length == localAppVerArry.Length)
                    {
                        for (int i = 0; i < serverAppVerArry.Length; i++)
                        {
                            if (Convert.ToInt32(serverAppVerArry[i]) > Convert.ToInt32(localAppVerArry[i]))
                            {
                                serverAppIsNew = true;
                                break;
                            }
                        }
                    }

                    if ((serverAppTime > localAppTime) || serverAppIsNew)
                    {
                        //开始自动更新! //140721 TBD
                        result = true;                        
                    }                    
                }
                return result;
            }
            catch (Exception ex)
            {                
                MessageBox.Show(ex.ToString());
                return result;
            }
        }
    }
}

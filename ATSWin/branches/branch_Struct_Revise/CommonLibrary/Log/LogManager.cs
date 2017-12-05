using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Diagnostics;
namespace ATS_Framework
{
    public class logManager
    {
#region Attribute
        public string[] levelStr = { "Infor", "Debug", "Warn", "Error", "Fatal" };
        int maxbufferSize;
      //  string fileName= DateTime.Now.Year.ToString() + "_" +DateTime.Now.Month.ToString() + "_"+ DateTime.Now.Day.ToString();;
        public string FilePath= @System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase+"Log\\" + DateTime.Now.Year.ToString() + "_" +DateTime.Now.Month.ToString() + "_"+ DateTime.Now.Day.ToString() + ".txt" ;
        string bufferString;
#endregion
#region Method
        public void FlushLogBuffer()
        {
            FileStream fsapp = new FileStream(FilePath, FileMode.Append, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fsapp);
            sw.Write(bufferString);
            sw.Close();
            fsapp.Close();
            bufferString = "";
        }

        public logManager(int buffsize=4096)
        {
            bufferString = "";
            maxbufferSize = buffsize;
          //  fileName = DateTime.Now.Year.ToString() + "_" +DateTime.Now.Month.ToString() + "_"+ DateTime.Now.Day.ToString();
           // fileName = @System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase+"\\Log\\" + fileName + ".txt";        
        }
        public logManager(string StrErrorLogPath,int buffsize = 4096)
        {
            bufferString = "";
            maxbufferSize = buffsize;
            string   fileName = DateTime.Now.Year.ToString() + "_" +DateTime.Now.Month.ToString() + "_"+ DateTime.Now.Day.ToString();
            FilePath = StrErrorLogPath + fileName + ".txt";
           // string StrPathPolarityEyeDiagram = PrtScPath + @"\EyeDiagram\" + comboBoxPType.Text.ToUpper() + "\\" + comboBoxPN.Text.ToUpper() + "\\" + comboBoxTestPlan.Text.ToUpper() + "\\" + StrTime + "\\Polarity\\";

            if (!Directory.Exists(StrErrorLogPath))
            {
                Directory.CreateDirectory(StrErrorLogPath);
            }
        }
        public logManager(string StrErrorLogPath,string SN, int buffsize = 4096)
        {
            bufferString = "";
            maxbufferSize = buffsize;
            string fileName = DateTime.Now.Year.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Day.ToString();
            FilePath = StrErrorLogPath + "\\" + SN + "_" + fileName + ".txt";
            // string StrPathPolarityEyeDiagram = PrtScPath + @"\EyeDiagram\" + comboBoxPType.Text.ToUpper() + "\\" + comboBoxPN.Text.ToUpper() + "\\" + comboBoxTestPlan.Text.ToUpper() + "\\" + StrTime + "\\Polarity\\";
          //  File

            if (!Directory.Exists(StrErrorLogPath))
            {
                Directory.CreateDirectory(StrErrorLogPath);
            }
           
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="level">日志级别  0=infor 1=dubug 2=warn 3=error 4=fatal</param>
        /// <param name="message">待封装的信息</param>
        /// <param name="className">调用者的类名</param>
        /// <returns>封装之后的类</returns>
        ///
        public string AdapterLogString(byte level, string message)
        {
            string datetime = DateTime.Now.ToString();
            string fileName = DateTime.Now.Year.ToString() + "_" + DateTime.Now.Day.ToString();
            string adapterString = "Unkonw";
            int bufferSize = 0;
            if (level<levelStr.Length)
            {
                adapterString = levelStr[level];
            }
            adapterString = "[" + datetime + "][" + adapterString + "][" + GetCallClassMethodName()+"]" + ":" + message + "\r\n";

            bufferString += adapterString;

            bufferSize = System.Text.Encoding.Default.GetByteCount(bufferString);
            
            if (bufferSize >= maxbufferSize)
            {
                FlushLogBuffer();
            }
            return adapterString;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="level">日志级别  0=infor 1=dubug 2=warn 3=error 4=fatal</param>
        /// <param name="message">待封装的信息</param>
        /// <param name="className">调用者的类名</param>
        /// <returns>封装之后的类</returns>
        ///
        public string AdapterLogString(string message)
        {
            string datetime = DateTime.Now.ToString();
            string fileName = DateTime.Now.Year.ToString() + "_" + DateTime.Now.Day.ToString();
            string adapterString = "Unkonw";
            int bufferSize = 0;
           
            adapterString = "[" + datetime + "][" + adapterString + "][" + GetCallClassMethodName() + "]" + ":" + message + "\r\n";

            bufferString += adapterString;

            bufferSize = System.Text.Encoding.Default.GetByteCount(bufferString);

            if (bufferSize >= maxbufferSize)
            {
                FlushLogBuffer();
            }
            return adapterString;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="level">日志级别  0=infor 1=dubug 2=warn 3=error 4=fatal</param>
        /// <param name="message">待封装的信息</param>
        /// <param name="className">调用者的类名</param>
        /// <returns>封装之后的类</returns>
        ///
      
        /// <summary>
        /// 获取调用者的类名和方法名
        /// </summary>
        /// <returns></returns>
        private string GetCallClassMethodName()
        {          
            StackTrace st = new StackTrace();
            StackFrame sf = st.GetFrame(2);
            string methodName = sf.GetMethod().Name;
            string className = sf.GetMethod().ReflectedType.Name;
            sf = null;
            st = null;
            GC.Collect();
            return className + "." + methodName;
        } 
#endregion
    }
}

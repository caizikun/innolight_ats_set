using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Threading;
namespace ATS_Framework
{

    public class CommandInf
    {
        public string IOType;
        public byte Addr;
        public string Name;
        public bool Reset;
    }
    public class EquipmentBase
    {
        //public  Semaphore semaphore;
        public string CurrentChannel;
        public SortedList<string, string> offsetlist = new SortedList<string, string>();
        protected List<InnoExCeption> exceptionList;
        private static object syncRoot = new object();//used for thread synchronization
       // public string log = "";

        public bool EquipmentConnectflag = false;// have used
        public bool EquipmentConfigflag = false;// config ok

        //----------------------Leo Add
        public int Referenced_Times = 0;// 仪器被使用的次数
        public  bool bReusable = false; // 仪器可以被复用
        public bool EquipmentErrorflag = false;// equipment Error
        public byte Role;// 0=NA,1=TX,2=RX

        public string IOType;
        public byte Addr;
        public string Name;
        public bool Reset;
       // public EquipmentErrorCode MyErrorCode;
        public EquipmentErrorQueue<InnoExCeption> MyExceptionQueue = new EquipmentErrorQueue<InnoExCeption>();// 处理后台log

        public bool bReady //equipment is ready to be used by test model.
        {
            get
            {
                if ((Referenced_Times == 0) || bReusable) return true;
                else return false;
            }
        }

        public void IncreaseReferencedTimes()
        {
            lock (syncRoot)
            {
                Referenced_Times++;
            }
        }
        public void DecreaseReferencedTimes()
        {
            lock (syncRoot)
            {
                Referenced_Times--;
                if (Referenced_Times < 0) Referenced_Times = 0;
            }
        }
        public virtual bool OutPutSwitch(bool Switch, int syn = 0) { return true; } 
        //-----------------------
        public virtual bool Configure(int syn=0)
        {
            return true;
        }
        public virtual bool Initialize(TestModeEquipmentParameters[] LIST)
        {
            return true;
        }
        public virtual bool Initialize(DutStruct[] List)
        {
            return true;
        }
        public virtual bool Initialize(DutStruct[] DutList, DriverStruct[] DriverList,string AuxAttribles)
        {
            return true;
        }
        public virtual bool Initialize(DutStruct[] DutList, DriverStruct[] DriverList,DriverInitializeStruct[] DriverinitList, string AuxAttribles)
        {
            return true;
        }
        public virtual bool Initialize(DutStruct[] DutList, DriverStruct[] DriverList, DriverInitializeStruct[] DriverinitList, DutEEPROMInitializeStuct[] EEpromInitList, string AuxAttribles)
        {
            return true;
        }
        /// <summary>
        /// 检查仪器角色与TestModel是否匹配,是否需要切换,比如Bidi
        /// </summary>
        /// <param name="TestModelType">TestModel 类型,Tx:1;RX:2;Tx_Rx:0</param>
        /// <param name="Channel">当前通道号</param>
        /// <returns></returns>
        public virtual bool CheckEquipmentRole(byte TestModelType,byte Channel)
        {
            return true;
        }
        //public string GetLogInfo()
        //{
        //    return log;
        
        //}
        public virtual bool Connect() { return true; }
        public virtual bool ChangeChannel(string channel,int syn=0) { return true; }
        public virtual bool configoffset(string channel, string offset, int syn = 0) { return true; }

        public virtual List<InnoExCeption> GetException()
        {
            return exceptionList;
        }
        public virtual bool ClearException()
        {
            return true;
        }

        protected IOPort myIO = IOPort.GetIOPort();
        protected virtual bool WriteString(string str_Write)
        {
            try
            {
                return myIO.WriteString(IOPort.Type.GPIB, "GPIB0::" + Addr, str_Write);
            }
            catch (InnoExCeption error)
            {
                Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                throw error;
            }

            catch (Exception error)
            {

                Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                throw new InnoExCeption(ExceptionDictionary.Code._Funtion_Fatal_0x05002, error.StackTrace);
                // throw new InnoExCeption(ex);
            }
        }
        protected virtual string ReadString(int count = 0)
        {
            try
            {
                return myIO.ReadString(IOPort.Type.GPIB, "GPIB0::" + Addr, count);
            }
            catch (InnoExCeption error)
            {
                Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                throw error;
            }

            catch (Exception error)
            {

                Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                throw new InnoExCeption(ExceptionDictionary.Code._Funtion_Fatal_0x05002, error.StackTrace);
                // throw new InnoExCeption(ex);
            }
        }
      
    } 
    public class EquipmentErrorQueue<T> : Queue<T>
        {
            // <summary>
            /// 通知的状态机
            /// </summary>
            AutoResetEvent notice = new AutoResetEvent(true);
            /// <summary>
            /// 内部链表
            /// </summary>
            LinkedList<T> list = new LinkedList<T>();
            // ArrayList AA=new ArrayList();

            #region IQueue<T> 成员
            /// <summary>
            /// 清除队列
            /// </summary>
            public void Clear()
            {
                Lock();
                list.Clear();
                UnLock();
            }
            /// <summary>
            /// 队列长度
            /// </summary>
            public int Count
            {
                get
                {
                    Lock();
                    int x = list.Count;
                    UnLock();
                    return x;
                }
            }
            /// <summary>
            /// 出队
            /// </summary>
            /// <returns>出队的元素</returns>
            public T Dequeue()
            {
                Lock();
                //从头取
                T t = list.First.Value;
                //从头删
                list.RemoveFirst();
                UnLock();
                return t;
            }
            /// <summary>
            /// 入队
            /// </summary>
            /// <param name="t">入队元素</param>
            public void Enqueue(T t)
            {
                Lock();
                //加到尾
                //不像单向链表需要遍历整个链表才能添加
                //对于双向链表可以直接添加在链表的尾部
                list.AddLast(t);
                UnLock();
            }
            /// <summary>
            /// 提取元素
            /// </summary>
            /// <returns>提取的元素</returns>
            public T Peek()
            {
                //从头取
                Lock();
                T t = list.First.Value;
                UnLock();
                return t;
            }


            #endregion


            /// <summary>
            /// 锁定
            /// </summary>
            private void Lock()
            {
                Thread.Sleep(100);
                notice.WaitOne();
            }
            /// <summary>
            /// 解锁
            /// </summary>
            private void UnLock()
            {
                Thread.Sleep(100);
                notice.Set();
            }
        }
}

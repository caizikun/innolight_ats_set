using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Data;
using System.Collections;
namespace ATS
{
    public interface IQueue<T>
    {
        void Clear();
        int Count { get; }
        T Dequeue();
        void Enqueue(T t);
        T Peek();
    }
    //public class OLDLogQueue<T> : Queue<T>
    //{


    //    public new void Enqueue(T obj)
    //    {
    //        lock (this)
    //        {
    //            base.Enqueue(obj);
    //        }
    //    }
    //    public new T Dequeue()
    //    {
    //        lock (this)
    //        {
    //            return base.Dequeue();
    //        }
    //    }
    //}
    public class LogQueue<T> : IQueue<T>
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

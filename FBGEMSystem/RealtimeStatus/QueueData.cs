using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using FBGEMSystem;

namespace FBGEMSystem.RealtimeStatus
{
     public class QueueData
    {
     #region
        private Queue<float> buffer;//缓冲区  

        public QueueData(int capacity)
        {
            buffer = new Queue<float>(capacity); 
        }

        public int BufferSize
        {
            get
            {
                return buffer.Count;//缓冲区设定长度
            }
        }

        public float Buffer
        {

            get
            {
                float f = 0;
                //Message msg = new Message();
                // 加锁
                lock (this)
                {
                    while (buffer.Count==0)
                    {
                        Monitor.Wait(this); 
                    }
                    f = buffer.Dequeue();  
                    Monitor.PulseAll(this);
                    // 释放锁
                }//lock
                return f;//返回值
            }

            set
            {
                // 加锁
                lock (this)
                {
                    while (buffer.Count==2001)//缓冲区已满
                    {
                        Monitor.Wait(this); 
                    } 
                    buffer.Enqueue(value);   
                    Monitor.PulseAll(this);
                    // 释放锁
                }
            }
        }
         #endregion 
    } 
   
}
 

using System;
using System.Collections.Generic; 
using System.Threading;
using FBGEMSystem;

namespace FBGEMSystem
{
  
    public class HoldIntegerSynchronizedEle
    { 
        #region
        private Queue<Message_EleDecoded> buffer;//缓冲区  
         
        public HoldIntegerSynchronizedEle(int capacity)
        {
            buffer = new Queue<Message_EleDecoded>(capacity); 
        }

        public int BufferSize
        {
            get
            {
                return buffer.Count;//缓冲区设定长度
            }
        }

        public bool isFull
        {
            get
            {
                if (buffer.Count == Receiver.buffer_capacity) { return true; }
                else { return false; }
            }
        }

        public Message_EleDecoded Buffer
        {

            get
            {
                Message_EleDecoded msg = new Message_EleDecoded();
                // 加锁
                lock (this)
                {
                    while (buffer.Count==0)
                    {
                        Monitor.Wait(this); 
                    }
                    msg = buffer.Dequeue();  
                    Monitor.PulseAll(this);
                    // 释放锁
                }//lock
                return msg;//返回值
            }

            set
            {
                // 加锁
                lock (this)
                {
                    while (buffer.Count==Receiver.buffer_capacity)//缓冲区已满
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
 

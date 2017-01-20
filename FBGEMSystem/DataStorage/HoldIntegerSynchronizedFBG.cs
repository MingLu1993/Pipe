using System;
using System.Collections.Generic;
using System.Threading;
using FBGEMSystem;


namespace FBGEMSystem
{
    public class HoldIntegerSynchronizedFBG
    {
        #region
        private Queue<Message_FBG> buffer;//缓冲区  
        
        public HoldIntegerSynchronizedFBG(int capacity)
        {
            buffer = new Queue<Message_FBG>(capacity);
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

        public Message_FBG Buffer
        {

            get
            {
                Message_FBG msgFBG = new Message_FBG();
                // 加锁
                lock (this)
                {
                    while (buffer.Count == 0)
                    {
                        Monitor.Wait(this);
                    }
                    msgFBG = buffer.Dequeue();
                    Monitor.PulseAll(this);
                    // 释放锁
                }//lock
                return msgFBG;//返回值
            }

            set
            {
                // 加锁
                lock (this)
                {
                    while (buffer.Count == Receiver.buffer_capacity)//缓冲区已满
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

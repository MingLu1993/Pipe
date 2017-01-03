using System;
using System.Collections.Generic;
using System.Threading;
using FBGEMSystem;


namespace FBGEMSystem
{
    public class HoldIntegerSynchronizedElc
    {
        #region
        private Queue<Message_Electric> buffer;//缓冲区  

        public HoldIntegerSynchronizedElc(int capacity)
        {
            buffer = new Queue<Message_Electric>(capacity);
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

        public Message_Electric Buffer
        {

            get
            {
                Message_Electric msgElc = new Message_Electric();
                // 加锁
                lock (this)
                {
                    while (buffer.Count == 0)
                    {
                        Monitor.Wait(this);
                    }
                    msgElc = buffer.Dequeue();
                    Monitor.PulseAll(this);
                    // 释放锁
                }//lock
                return msgElc;//返回值
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

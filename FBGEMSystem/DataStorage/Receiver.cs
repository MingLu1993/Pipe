using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Timers;
using System.Windows.Threading;
//using System.Windows.Forms;


namespace FBGEMSystem
{
    class Receiver
    {

        private UdpClient udpClient;
        private static IPAddress IP = IPAddress.Parse("127.0.0.1");
        //private IPEndPoint iep_FBG = new IPEndPoint(IP, Data.port);
        IPEndPoint remote = null;
        //int kk = 0;

        public static int index = 0;
        public static int buffer_capacity = 4000;
        //原有的
        public static HoldIntegerSynchronized sharedLocation = new HoldIntegerSynchronized(buffer_capacity);//存储缓冲 
        public static HoldIntegerSynchronized sharedLocation1 = new HoldIntegerSynchronized(buffer_capacity);//绘图缓冲  
        public static HoldIntegerSynchronized process_all_msg = new HoldIntegerSynchronized(buffer_capacity);//分析缓冲

        //电类传感器缓冲
        public static HoldIntegerSynchronizedElc sharedDecodeEle = new HoldIntegerSynchronizedElc(buffer_capacity);//解包缓冲 
        //public static HoldIntegerSynchronizedElc sharedLocation1Ele = new HoldIntegerSynchronizedElc(buffer_capacity);//绘图缓冲  
        //public static HoldIntegerSynchronizedElc process_all_msgEle = new HoldIntegerSynchronizedElc(buffer_capacity);//分析缓冲
        public static Message_Electric msgDatashow = new Message_Electric(); //表格界面中显示实时数值从这里取值
        //string[] bufferArray_eddyCurrent = new string[Data.numPerPack_eddyCurrent];

        //Message msg = new Message(); 原有的
        //Message msg2 = new Message();
        
        //用于接收电类传感器数据
        Message_Electric msgEle = new Message_Electric();
        Message_Electric msg2Ele = new Message_Electric();

        byte[] bytes2 = new byte[50000];

        public void TCPClient_Initi()
        {
            udpClient = new UdpClient(Data.port);
            udpClient.Client.ReceiveBufferSize = 1024 * 1024;
        }


        //public void Recv_FBG()
        //{
        //    try
        //    {
        //        while (true)
        //        {
        //            lock (this)
        //            {
        //                bytes2 = udpClient.Receive(ref remote);
        //            }

        //            if (bytes2 != null)
        //            {
        //                msg = ConvertTool.ByteToStructure<Message>(bytes2);
        //                msg2.CH1 = msg.CH1;
        //                msg2.CH2 = msg.CH2;
        //                msg2.CH3 = msg.CH3;
        //                //msg2.CH4 = msg.CH4;
        //                if (sharedLocation.isFull == false)
        //                {
        //                    sharedLocation.Buffer = msg;
        //                }
        //                if (process_all_msg.isFull == false)
        //                {
        //                    process_all_msg.Buffer = msg2;
        //                }
        //                if (Data.IsControl == true)
        //                {
        //                    if (sharedLocation1.isFull == false)
        //                    {
        //                        sharedLocation1.Buffer = msg;
        //                    }
        //                }
        //                if (Data.IsControl2 == true)
        //                {
        //                    if (sharedLocation1.isFull == false)
        //                    {
        //                        sharedLocation1.Buffer = msg;
        //                    }
        //                }
        //                if (Data.IsControl1 == true)
        //                {
        //                    Data.Ch1 = msg.CH1;
        //                    Data.Ch2 = msg.CH2;
        //                    Data.Ch3 = msg.CH3;
        //                    //Data.Ch4 = msg.CH4;
        //                }
        //                index++;
        //            }
        //        }
        //    }

        //    catch (Exception err)
        //    {
        //        MessageBox.Show(err.ToString());
        //    }
        //}

        public void Recv_Electric()
        {
            try
            { 
                while (true)
                {
                    lock (this)
                    {
                        bytes2 = udpClient.Receive(ref remote);
                    }

                    if (bytes2 != null)
                    {
                        msgEle = ConvertTool.ByteToStructure<Message_Electric>(bytes2);
                        msg2Ele.CH1 = msgEle.CH1;
                        msgDatashow.CH1 = msgEle.CH1;  //供表格界面的ViewElecData使用
                        if (sharedDecodeEle.isFull == false)
                        {
                            sharedDecodeEle.Buffer = msgEle;  //放入解包缓存
                        }
                        //if (process_all_msgEle.isFull == false)
                        //{
                        //    process_all_msgEle.Buffer = msg2Ele;
                        //}
                        //if (Data.IsControl == true)
                        //{
                        //    if (sharedLocation1Ele.isFull == false)
                        //    {
                        //        sharedLocation1Ele.Buffer = msgEle;
                        //    }
                        //}
                        //if (Data.IsControl2 == true)
                        //{
                        //    if (sharedLocation1Ele.isFull == false)
                        //    {
                        //        sharedLocation1Ele.Buffer = msgEle;
                        //    }
                        //}
                        //if (Data.IsControl1 == true)
                        //{
                        //    Data.Ele = msgEle.CH1;
                        //}
                        index++;
                        //decode_Electric();
                        //kk = sharedLocation1.BufferSize;
                    }
                }
            }

            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }
        }

        //解包线程，从解包缓存sharedDecodeEle中读取Message_Electric,解包成Message,放入绘图缓存sharedLocation1中
        public void decode_Electric()
        {
            while(true)
            {
                float[] CH1 = new float[Data.num_Sensor * Data.num_Package];
                float[] CH2 = new float[Data.num_Sensor * Data.num_Package];
                float[] CH3 = new float[Data.num_Sensor * Data.num_Package];
                Message_Electric msg = new Message_Electric();
                Message msg_decode = new Message();
                if (sharedDecodeEle.BufferSize != 0)
                {
                    msg = sharedDecodeEle.Buffer;
                    for (int i = 0; i < Data.num_Package; i++)
                    {
                        for (int j = 0; j < Data.num_Sensor; j++)
                        {
                            CH1[i * Data.num_Sensor + j] = msg.CH1[i * Data.num_Sensor * 3 + j];
                        }
                        for (int j = 0; j < Data.num_Sensor; j++)
                        {
                            CH2[i * Data.num_Sensor + j] = msg.CH1[i * Data.num_Sensor * 3 + j + Data.num_Sensor];
                        }
                        for (int j = 0; j < Data.num_Sensor; j++)
                        {
                            CH3[i * Data.num_Sensor + j] = msg.CH1[i * Data.num_Sensor * 3 + j + Data.num_Sensor * 2];
                        }
                    }
                    msg_decode.CH1_Press = CH1;
                    msg_decode.CH2_Temp = CH2;
                    msg_decode.CH3_Vibration = CH3;
                    msg_decode.dataTime = msg.dataTime;
                    if (Data.IsControl2 == true)
                    {
                        if (sharedLocation1.isFull == false)
                        {
                            sharedLocation1.Buffer = msg_decode;
                        }
                    }
                }
            }
            
               
        }
    }
}

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
        

        public static int index = 0;
        public static int buffer_capacity = 4000;
        public static HoldIntegerSynchronized sharedLocation = new HoldIntegerSynchronized(buffer_capacity);//存储缓冲 
        public static HoldIntegerSynchronized sharedLocation1 = new HoldIntegerSynchronized(buffer_capacity);//绘图缓冲  
        public static HoldIntegerSynchronized process_all_msg = new HoldIntegerSynchronized(buffer_capacity);//分析缓冲
        
        

        //string[] bufferArray_eddyCurrent = new string[Data.numPerPack_eddyCurrent];
         
        Message msg = new Message();
        Message msg2 = new Message(); 
        byte[] bytes2 = new byte[50000];

        public void TCPClient_Initi()
        {
            udpClient = new UdpClient(Data.port);
            udpClient.Client.ReceiveBufferSize = 1024 * 1024;
       
           
        }
         
         
        public void Recv_FBG()
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
                        msg = ConvertTool.ByteToStructure<Message>(bytes2);
                        msg2.CH1 = msg.CH1;
                        msg2.CH2 = msg.CH2;
                        msg2.CH3 = msg.CH3;
                        //msg2.CH4 = msg.CH4;
                        if (sharedLocation.isFull == false)
                        {
                            sharedLocation.Buffer = msg;
                        }
                        if (process_all_msg.isFull == false)
                        {
                            process_all_msg.Buffer = msg2;
                        }
                        if (Data.IsControl == true)
                        {
                            if (sharedLocation1.isFull == false)
                            {
                                sharedLocation1.Buffer = msg;
                            }
                        }
                        if (Data.IsControl2 == true)
                        {
                            if (sharedLocation1.isFull == false)
                            {
                                sharedLocation1.Buffer = msg;
                            }
                        }
                        if (Data.IsControl1 == true)
                        {
                            Data.Ch1 = msg.CH1;
                            Data.Ch2 = msg.CH2;
                            Data.Ch3 = msg.CH3;
                            //Data.Ch4 = msg.CH4;
                        }
                        index++;
                    }  
                }
            }

            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }
        }


    }
}

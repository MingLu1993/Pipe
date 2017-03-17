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
using System.Diagnostics; // 测试运行时间
//using System.Windows.Forms;


namespace FBGEMSystem
{
    class Receiver
    {
        //用于FBG TCP
        private TcpClient TcpFBG ;
        public NetworkStream streamtoserver=null;
        private GlobalMembersFBG gmFBG = new GlobalMembersFBG();

        //用于电类UDP
        private UdpClient udpEle;
        private static IPAddress IP = IPAddress.Parse("192.168.1.231");//测试用，应该与TCP的ip相同
        private IPEndPoint UdpEleIEP = null;
        IPEndPoint remote = null;


        public static int index = 0;
        public static int buffer_capacity = 4000;
        //FBG数据缓存
        public static HoldIntegerSynchronizedFBG sharedLocation_FBG = new HoldIntegerSynchronizedFBG(buffer_capacity);//存储缓冲1
        public static HoldIntegerSynchronizedFBG sharedLocation1_FBG = new HoldIntegerSynchronizedFBG(buffer_capacity);//绘图缓冲 
        public static HoldIntegerSynchronizedFBG process_all_msg_FBG = new HoldIntegerSynchronizedFBG(buffer_capacity);//分析缓冲
        //电类数据缓存
        public static HoldIntegerSynchronizedEle sharedLocation_Ele = new HoldIntegerSynchronizedEle(buffer_capacity);//存储缓冲 
        public static HoldIntegerSynchronizedEle sharedLocation1_Ele = new HoldIntegerSynchronizedEle(buffer_capacity);//绘图缓冲  
        public static HoldIntegerSynchronizedEle process_all_msg_Ele = new HoldIntegerSynchronizedEle(buffer_capacity);//分析缓冲

        //表格界面中显示实时数值从这里取值
        public static Message_Electric msgDatashow = new Message_Electric(); 
        //string[] bufferArray_eddyCurrent = new string[Data.numPerPack_eddyCurrent];
        
        //用于接收电类传感器数据
        Message_Electric msgEle = new Message_Electric();   //接收到的电类数据，一个数组
        Message_EleDecoded msgEleDecode = new Message_EleDecoded();   //解包后，3个数组

        byte[] bytesFBG = new byte[10000];    //接收FBG数据
        int nrecvFBG = 0;                    //接收FBG数据的长度
        byte[] bytesEle = new byte[50000];   //接收电类数据

        public void Client_Initi()
        {
            TcpFBG = new TcpClient();
            udpEle = new UdpClient(Data.port);
            udpEle.Client.ReceiveBufferSize = 1024 * 1024;

        }

        //与tcp建立连接，发送"test\n"给udp
        public void SocketConnect()
        {
            UdpEleIEP = new IPEndPoint(Data.remoteIP, Data.port);

           

            byte[] byte1 = new byte[10000];
            try
            {
                TcpFBG.Connect(Data.remoteIP, Data.TCPPort);
                streamtoserver = TcpFBG.GetStream();
                
            }
            catch(Exception err)
            {
                MessageBox.Show("连接失败，请重启软件重新连接！");
            }

            if (TcpFBG.Connected)
            {
                //Text = "连接成功，接收WHUTFBGV1A";

                //接收WHUTFBGV1A
                lock (streamtoserver)
                {
                    streamtoserver.Read(byte1, 0, byte1.Length);
                }
                MessageBox.Show("连接成功,等待接收config");

                //发送"C\n"
                string cmd = "C\n";
                byte[] bytetest;
                bytetest = System.Text.Encoding.Default.GetBytes(cmd);
                lock (streamtoserver)
                {
                    streamtoserver.Write(bytetest, 0, bytetest.Length);
                }
                int config_nRecv = 0;
                Array.Clear(byte1, 0, byte1.Length);
                //接收config
                lock (streamtoserver)
                {
                    config_nRecv = streamtoserver.Read(byte1, 0, byte1.Length);
                }
                gmFBG.DecodeFPGAFlashConfig(config_nRecv, byte1);
                MessageBox.Show("解析config完毕");

                //udp发送"C\n"至下位机，便于下位机获取本机ip及端口号
                //udpEle.Send(bytetest,bytetest.Length,UdpEleIEP);
                //Array.Clear(byte1, 0, byte1.Length);
                //bytesEle = udpEle.Receive(ref remote);
                //MessageBox.Show("TCP、UDP连接完毕");
            }

        }
        //tcp发送"Z\n"，开始指令
        public void SocketStart()
        {
            
                string cmd = "Z\n";
                byte[] bytetest = System.Text.Encoding.Default.GetBytes(cmd);
                lock (streamtoserver)
                {
                    streamtoserver.Write(bytetest, 0, bytetest.Length);
                }         
            
        }
        //光纤光栅数据接收
        public void Recv_FBG()
        {
            try
            {
                while (true)
                {
                   // lock (this)
                    {
                       lock (streamtoserver)
                       {
                            nrecvFBG = streamtoserver.Read(bytesFBG, 0, 8010);
                       }

                        if (bytesFBG != null)
                        {
                            gmFBG.dataDecodingEntry(bytesFBG, nrecvFBG);
                            //为了调试，接一包解一包,如何缓存还需设计
                        }
                    }
                }
            }

            catch (Exception err)
            {
                //MessageBox.Show(err.ToString());
            }
        }

        //电类数据接收
        public void Recv_Electric()
        {
            try
            { 
                while (true)
                {
                    //lock (this)
                    {
                        bytesEle = udpEle.Receive(ref remote);
                    }

                    if (bytesEle != null)
                    {
                        msgEle = ConvertTool.ByteToStructure<Message_Electric>(bytesEle);
                        msgDatashow.CH1 = msgEle.CH1;  //供表格界面的ViewElecData使用
                        //if (sharedDecodeEle.isFull == false)
                        //{
                        //    sharedDecodeEle.Buffer = msgEle;  //放入解包缓存
                        //}不需要解包缓存

                        //接一包解一包
                        msgEleDecode = decode_Electric(msgEle);

                        //如果画波形界面打开，则缓存进绘图缓存
                        if (Data.IsControl2 == true)
                        {
                            if (sharedLocation1_Ele.isFull == false)
                            {
                                sharedLocation1_Ele.Buffer = msgEleDecode;
                            }
                        }
                        //存入存储缓冲
                        if (Data.IsControlSQL == true)
                        {
                            sharedLocation_Ele.Buffer = msgEleDecode;
                        }

                        //if (process_all_msgEle.isFull == false)
                        //{
                        //    process_all_msgEle.Buffer = msg2Ele;
                        //}

                        index++;

                    }
                }
            }

            catch (Exception err)
            {
                //MessageBox.Show(err.ToString());
            }
        }

        //XXXX解包线程，从解包缓存sharedDecodeEle中读取Message_Electric,解包成Message,放入绘图缓存sharedLocation1中X
        //去掉线程，改为接一包解一包20170120
        public Message_EleDecoded decode_Electric(Message_Electric msgele)
        {
            float[] CH1 = new float[Data.num_Sensor * Data.num_Package];
            float[] CH2 = new float[Data.num_Sensor * Data.num_Package];
            float[] CH3 = new float[Data.num_Sensor * Data.num_Package];
            Message_Electric msg = new Message_Electric();
            Message_EleDecoded msg_decode = new Message_EleDecoded();
            msg = msgele;
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
                   
            return msg_decode;
        }

    }
}

using FBGEMSystem.LiveDataShow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace FBGEMSystem
{
    public struct Electric_sensor 
    {
        public bool is_Choose { get; set; }     //判断通道是否使用
        public float Sensitivity { get; set; }  //记录传感器灵敏度
        public float range_high { get; set; }   //量程上限
        public float range_low { get; set; }    //量程下限
    }

    class Data
    {
        
        public static IPAddress remoteIP;
        public static int UDPPort = 8;
        public static int TCPPort = 7;
        //端口数
        public static int port = 8001;
        //public static int port_eddyCurrent = 2001;

        public static int SamplingRate_FBG = 2000;

        //电类传感器类型数
        public const int  type_Sensor= 3;       //add
        //电类传感器个数
        public const int num_Sensor = 8;        //add
        //电类传感器包数
        public const int num_Package = 40;      //add
        //光栅通道数
        public const int FBG_Channel_Num = 4;  
        //FBG一包数据个数
        public const int FBG_numPackage = 40;   //待定！！！！！！！！！！

        public static Electric_sensor[] Pressure = new Electric_sensor[num_Sensor];
        public static Electric_sensor[] Temperature = new Electric_sensor[num_Sensor];
        public static Electric_sensor[] Vibration = new Electric_sensor[num_Sensor];


        public static List<int> PressureIndex = new List<int>();          //使用的压力传感器的通道索引
        public static List<int> TemperatureIndex = new List<int>();       //使用的温度传感器的通道索引
        public static List<int> VibrationIndex = new List<int>();         //使用的振动传感器的通道索引

        public static List<int> FBGChannelIndex = new List<int>();        //使用的光栅通道索引

        public static bool isChannelSetting = false;


        //后添加的FBG所在点数
        public static int point_eddyCurrent = 7;

        //FBG振动中心波长
        public static double wavelength_V = 1290.573;

        //电涡流协议每包数据数目
        //public static int numPerPack_eddyCurrent = 40;

        //电类压力通道数  实际
        private static int chnum1= 0;
        public static int Chnum1
        {
            get { return chnum1; }
            set { chnum1 = value; }
        }
        //电类温度通道数  实际
        private static int chnum2 = 0;
        public static int Chnum2
        {
            get { return chnum2; }
            set { chnum2 = value; }

        }
        //电类振动通道数  实际
        private static int chnum3 = 0;
        public static int Chnum3
        {
            get { return chnum3; }
            set { chnum3 = value; }

        }

        //光纤光栅通道1
        private static int fbgCH1 = 0;
        public static int FBGCH1
        {
            get { return fbgCH1; }
            set { fbgCH1 = value; }
        }
        //光纤光栅通道2
        private static int fbgCH2 = 0;
        public static int FBGCH2
        {
            get { return fbgCH2; }
            set { fbgCH2 = value; }
        }
        //光纤光栅通道3
        private static int fbgCH3 = 0;
        public static int FBGCH3
        {
            get { return fbgCH3; }
            set { fbgCH3 = value; }
        }
        //光纤光栅通道4
        private static int fbgCH4 = 0;
        public static int FBGCH4
        {
            get { return fbgCH4; }
            set { fbgCH4 = value; }
        }


        //控制电类传感器画图曲线的启动
        private static bool isControl2 = false;
        public static bool IsControl2
        {
            get { return Data.isControl2; }
            set { Data.isControl2 = value; }
        }

        //控制FBG传感器信号分析曲线的启动
        private static bool isControlFBG = false;
        public static bool IsControlFBG
        {
            get { return Data.isControlFBG; }
            set { Data.isControlFBG = value; }
        }

        //控制是否存入数据库
        private static bool isControlSQL = false;
        public static bool IsControlSQL
        {
            get { return Data.isControlSQL; }
            set { Data.isControlSQL = value; }
        }

    }
}

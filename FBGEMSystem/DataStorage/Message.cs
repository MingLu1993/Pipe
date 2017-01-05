using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Runtime.InteropServices;

namespace FBGEMSystem
{
        /*
         * 在内存开辟一段连续空间存储该结构体
         * 便于和byte数组相互转换
         */
        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
    
        public struct Message
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = Data.num_Sensor * Data.num_Package)]
            public float[] CH1_Press;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = Data.num_Sensor * Data.num_Package)]
            public float[] CH2_Temp;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = Data.num_Sensor * Data.num_Package)]
            public float[] CH3_Vibration;
            //[MarshalAs(UnmanagedType.ByValArray, SizeConst = 64 * 40)]
            //public float[] CH4;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 25)]
            public string dataTime;   
        }

        public struct Message_Electric
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = (Data.num_Sensor * 3) * Data.num_Package)]
            public float[] CH1;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 25)]
            public string dataTime;
        }
}

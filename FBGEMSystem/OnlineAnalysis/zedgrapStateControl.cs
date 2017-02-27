using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FBGEMSystem.OnlineAnalysis
{
   static class zedgrapStateControl
    {
          //控制曲线的启动
  

        //控制曲线的启动
        private static bool isControl1 = false;
        public static bool IsControl1
        {
            get { return isControl1; }
            set {isControl1 = value; }
        }

        //控制曲线的启动
        private static bool isControl2 = false;
        public static bool IsControl2
        {
            get { return isControl2; }
            set { isControl2 = value; }
        }
                  
        private static bool isControl3=false;
        public static bool IsControl3
        {
            get { return isControl3; }
            set { isControl3 = value; } 
        }
        private static bool isControl4 = false;
        public static bool IsControl4
        {
            get { return isControl4; }
            set { isControl4 = value; }
        }
        private static bool isControl5 = false;
        public static bool IsControl5
        {
            get { return isControl5; }
            set { isControl5 = value; }
        }
        private static bool isControl6= false;
        public static bool IsControl6
        {
            get { return isControl6; }
            set { isControl6 = value; }
        }
        private static bool isControl7 = false;
        public static bool IsControl7
        {
            get { return isControl7; }
            set { isControl7 = value; }
        }
    }
}

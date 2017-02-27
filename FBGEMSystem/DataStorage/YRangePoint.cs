using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FBGEMSystem.DataStorage
{
    class YRangePoint
    {
        private double x; //上限
        private double y;
        private string sensorName;

        public YRangePoint(int x, int y, string sensorName)
        {
            this.x = x;
            this.y = y;
            this.sensorName = sensorName;
        }

        public double getX()
        {
            return x;
        }
        public void setX(double x)
        {
            this.x = x;
        }
        public double getY()
        {
            return y;
        }
        public void setY(double y)
        {
            this.y = y;
        }
        public string getSensorName()
        {
            return sensorName;
        }

        public void setSensorName(string sensorName)
        {
            this.sensorName = sensorName;
        }

    }
}

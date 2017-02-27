using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FBGEMSystem.DataStorage;

namespace FBGEMSystem.DataStorage
{
    class SystemConfig
    {
        private int totalNum;
        private YRangePoint yRange;
        public int getTotalNum()
        {
            return totalNum;
        }
        public void setTotalNum(int totalNum)
        {
            this.totalNum = totalNum;
        }
        public YRangePoint getYRange()
        {
            return yRange;
        }
        public void setYRange(YRangePoint yRange)
        {
            this.yRange = yRange;
        }

        }


    }


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Timers;
using System.Threading;

namespace FBGEMSystem.LiveDataShow
{
    public class ViewElecData : INotifyPropertyChanged
    {
       
        float[] TempdataExist = new float[Data.num_Sensor];//存放温度传感器数据
        float[] PresdataExist = new float[Data.num_Sensor];//存放压力传感器数据
        float[] vibratedataExist=new float[Data.num_Sensor];//存放振动传感器数据
        
        private List<Temp> _temp;
        public List<Temp> Tempshow
        {
            get { return _temp; }
            set { _temp = value; NotifyPropertyChanged("Temp"); }
        }

        private List<Temp> _temp_bind;
        public List<Temp> Temp_bind
        {
            get { return _temp_bind; }
            set { _temp_bind = value; NotifyPropertyChanged("Temp_bind"); }
        }

        private List<Pres> _pres;
        public List<Pres> Presshow
        {
            get { return _pres; }
            set { _pres = value; NotifyPropertyChanged("Pres"); }
        }
        private List<Pres> _pres_bind;
        public List<Pres> Pres_bind
        {
            get { return _pres_bind; }
            set { _pres_bind = value; NotifyPropertyChanged("Pres_bind"); }
        }

        private List<Vibrate> _vibrate;
        public List<Vibrate> vibrateshow
        {
            get { return _vibrate; }
            set { _vibrate = value; NotifyPropertyChanged("Vibrate"); }
        }

        private List<Vibrate> _vibrate_bind;
        public List<Vibrate> Vibrate_bind
        {
            get { return _vibrate_bind; }
            set { _vibrate_bind = value; NotifyPropertyChanged("Vibrate_bind"); }
        }


        #region INotifyPropertyChanged 成员

        public event PropertyChangedEventHandler PropertyChanged;

        // 添加一个触发 PropertyChanged 事件的通用方法
        public void NotifyPropertyChanged(string PropertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
            }
        }
        #endregion

        public Thread datathread;
        public ViewElecData()
        {
           // msg = Receiver.sharedLocation.Buffer;
            _temp = new List<Temp>();
            _pres = new List<Pres>();
            _vibrate = new List<Vibrate>();
            datathread = new Thread(new ThreadStart(DataShow));
            datathread.IsBackground = true;
            //datathread.Start();
        }
        private void DataShow()
        {
            while (true)
            {
                tempDataDelete();
                presDataDelete();
                acceDataDelete(); 
                for (int i = 0; i < Data.num_Sensor; i++)
                {
                    if (Data.Temperature[i].is_Choose == true)
                        TempdataExist[i] = Receiver.msgDatashow.CH1[i];
                    else
                        TempdataExist[i] = 0;

                    if (Data.Pressure[i].is_Choose == true)
                        PresdataExist[i] = Receiver.msgDatashow.CH1[i + 8];
                    else
                        PresdataExist[i] = 0;

                    if (Data.Vibration[i].is_Choose == true)
                        vibratedataExist[i] = Receiver.msgDatashow.CH1[i + 16];
                    else
                        vibratedataExist[i] = 0;
                }
                Tempshow.Add(new Temp
                {

                    Temp1 = TempdataExist[0],
                    Temp2 = TempdataExist[1],
                    Temp3 = TempdataExist[2],
                    Temp4 = TempdataExist[3],
                    Temp5 = TempdataExist[4],
                    Temp6 = TempdataExist[5],
                    Temp7 = TempdataExist[6],
                    Temp8 = TempdataExist[7],
                });
               
        
                Presshow.Add(new Pres
                {
                    Pres1 = PresdataExist[0],
                    Pres2 = PresdataExist[1],
                    Pres3 = PresdataExist[2],
                    Pres4 = PresdataExist[3],
                    Pres5 = PresdataExist[4],
                    Pres6 = PresdataExist[5],
                    Pres7 = PresdataExist[6],
                    Pres8 = PresdataExist[7],
                });
               
                vibrateshow.Add(new Vibrate
                {
                    vibrate1 = vibratedataExist[0],
                    vibrate2 = vibratedataExist[1],
                    vibrate3 = vibratedataExist[2],
                    vibrate4 = vibratedataExist[3],
                    vibrate5 = vibratedataExist[4],
                    vibrate6 = vibratedataExist[5],
                    vibrate7 = vibratedataExist[6],
                    vibrate8 = vibratedataExist[7],
                });

                Temp_bind = Tempshow.ToList();
                Pres_bind = Presshow.ToList();
                Vibrate_bind = vibrateshow.ToList();
             
                Thread.Sleep(1000);
              
            }
        }
        private void tempDataDelete()
        {
            Tempshow.Clear();
            Temp_bind = Tempshow.ToList();
        }

    
        private void presDataDelete()
        {
            Presshow.Clear();
            Pres_bind = Presshow.ToList();
        }

        private void acceDataDelete()
        {
            vibrateshow.Clear();
            Vibrate_bind = vibrateshow.ToList();
        }
        public class Temp
        {
            public double Temp1 { get; set; }
            public double Temp2 { get; set; }
            public double Temp3 { get; set; }
            public double Temp4 { get; set; }
            public double Temp5 { get; set; }
            public double Temp6 { get; set; }
            public double Temp7 { get; set; }
            public double Temp8 { get; set; }
        }
        public class Pres
        {
            public double Pres1 { get; set; }
            public double Pres2 { get; set; }
            public double Pres3 { get; set; }
            public double Pres4 { get; set; }
            public double Pres5 { get; set; }
            public double Pres6 { get; set; }
            public double Pres7 { get; set; }
            public double Pres8 { get; set; }
        }
        public class Vibrate
        {
            public double vibrate1 { get; set; }
            public double vibrate2 { get; set; }
            public double vibrate3 { get; set; }
            public double vibrate4 { get; set; }
            public double vibrate5 { get; set; }
            public double vibrate6 { get; set; }
            public double vibrate7 { get; set; }
            public double vibrate8 { get; set; }
        }
    }
}

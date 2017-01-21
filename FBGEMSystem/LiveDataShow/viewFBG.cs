using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Timers;
using System.Threading;
using FBGEMSystem;
using System.Windows.Media;

namespace FBGEMSystem.LiveDataShow
{
    public class viewFBG : INotifyPropertyChanged
    {
        System.Timers.Timer t1 = new System.Timers.Timer(500);

        System.Timers.Timer t2 = new System.Timers.Timer(800);

        private int _number;

        Random rdm = new Random(Guid.NewGuid().GetHashCode());
        
        public int Number
        {
            get { return _number; }
            set { _number = value; NotifyPropertyChanged("Number"); }
        }

        private List<AllFBG> _allfbg;
        public List<AllFBG> AllFBG
        {
            get { return _allfbg; }
            set { _allfbg = value; NotifyPropertyChanged("AllFBG"); }
        }

        private List<AllFBG> _lst_bind;

        public List<AllFBG> Lst_bind
        {
            get { return _lst_bind; }
            set { _lst_bind = value; NotifyPropertyChanged("Lst_bind"); }
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
        public Thread thread1;
        public viewFBG()
        {
            _allfbg = new List<AllFBG>();
            thread1 = new Thread(new ThreadStart(datashow));
            thread1.IsBackground = true;
            //thread1.Start();
           // mwindow1.label1.Foreground =new SolidColorBrush(Colors.Blue);
            //Thread thread2 = new Thread(new ThreadStart(datadelete));
            //thread2.Start();
            
        }
        public void datashow()
        {
            while (true)
            {
                datadelete();
                AllFBG.Add(new AllFBG { FBG1 =Math.Round(rdm.NextDouble()+1,2), FBG2 = 1.22, FBG3 = 1.22, FBG4 = 1.22, FBG5 = 1.22, FBG6 = 1.22, FBG7 = 1.22, FBG8 = 1.22 });
                Lst_bind = AllFBG.ToList();
                Thread.Sleep(200);
                
            }
        }

        public void datadelete()
        {
           
           // while (true)
            {
                AllFBG.Clear();
                 Lst_bind = AllFBG.ToList();
            }
        }
         
        //数据显示
       
    }

        public class AllFBG
        {
            public double FBG1 { get; set; }
            public double FBG2 { get; set; }
            public double FBG3 { get; set; }
            public double FBG4 { get; set; }
            public double FBG5 { get; set; }
            public double FBG6 { get; set; }
            public double FBG7 { get; set; }
            public double FBG8 { get; set; }
        }
    
}

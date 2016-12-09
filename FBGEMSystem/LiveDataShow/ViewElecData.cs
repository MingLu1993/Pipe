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
        Random rdm_temp = new Random(Guid.NewGuid().GetHashCode());

        Random rdm_pres = new Random(Guid.NewGuid().GetHashCode());

        Random rdm_acce = new Random(Guid.NewGuid().GetHashCode());

        private List<Temp> _temp;
        public List<Temp> Temp1
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
        public List<Pres> Pres1
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

        private List<Acce> _acce;
        public List<Acce> Acce1
        {
            get { return _acce; }
            set { _acce = value; NotifyPropertyChanged("Acce"); }
        }

        private List<Acce> _acce_bind;
        public List<Acce> Acce_bind
        {
            get { return _acce_bind; }
            set { _acce_bind = value; NotifyPropertyChanged("Acce_bind"); }
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

        public ViewElecData()
        {
            _temp = new List<Temp>();
            _pres = new List<Pres>();
            _acce = new List<Acce>();
            Thread temp_thread = new Thread(new ThreadStart(tempDataShow));
            temp_thread.Start();
            Thread pres_thread = new Thread(new ThreadStart(presDataShow));
            pres_thread.Start();
            Thread acce_thread = new Thread(new ThreadStart(acceDataShow));
            acce_thread.Start();
        }
        private void tempDataShow()
        {
            while (true)
            {
                Temp1.Add(new Temp
                {
                    Temp1 = Math.Round(rdm_temp.NextDouble() + 1, 2),
                    Temp2 = Math.Round(rdm_temp.NextDouble() + 1, 2),
                    Temp3 = Math.Round(rdm_temp.NextDouble() + 1, 2),
                    Temp4 = Math.Round(rdm_temp.NextDouble() + 1, 2),
                    Temp5 = Math.Round(rdm_temp.NextDouble() + 1, 2),
                    Temp6 = Math.Round(rdm_temp.NextDouble() + 1, 2),
                    Temp7 = Math.Round(rdm_temp.NextDouble() + 1, 2),
                    Temp8 = Math.Round(rdm_temp.NextDouble() + 1, 2),
                });
                Temp_bind = Temp1.ToList();
                Thread.Sleep(500);
                tempDataDelete();
                Thread.Sleep(300);
            }
        }
        private void tempDataDelete()
        {
            Temp1.Clear();
            Temp_bind = Temp1.ToList();
        }

        private void presDataShow()
        {
            while (true)
            {
                Pres1.Add(new Pres
                {
                    Pres1 = Math.Round(rdm_pres.NextDouble() + 1, 2),
                    Pres2 = Math.Round(rdm_pres.NextDouble() + 1, 2),
                    Pres3 = Math.Round(rdm_pres.NextDouble() + 1, 2),
                    Pres4 = Math.Round(rdm_pres.NextDouble() + 1, 2),
                    Pres5 = Math.Round(rdm_pres.NextDouble() + 1, 2),
                    Pres6 = Math.Round(rdm_pres.NextDouble() + 1, 2),
                    Pres7 = Math.Round(rdm_pres.NextDouble() + 1, 2),
                    Pres8 = Math.Round(rdm_pres.NextDouble() + 1, 2),
                });
                Pres_bind = Pres1.ToList();
                Thread.Sleep(500);
                presDataDelete();
                Thread.Sleep(300);
            }
        }
        private void presDataDelete()
        {
            Pres1.Clear();
            Pres_bind = Pres1.ToList();
        }
        private void acceDataShow()
        {
            while (true)
            {
                Acce1.Add(new Acce
                {
                    Acce1 = Math.Round(rdm_acce.NextDouble() + 1, 2),
                    Acce2 = Math.Round(rdm_acce.NextDouble() + 1, 2),
                    Acce3 = Math.Round(rdm_acce.NextDouble() + 1, 2),
                    Acce4 = Math.Round(rdm_acce.NextDouble() + 1, 2),
                    Acce5 = Math.Round(rdm_acce.NextDouble() + 1, 2),
                    Acce6 = Math.Round(rdm_acce.NextDouble() + 1, 2),
                    Acce7 = Math.Round(rdm_acce.NextDouble() + 1, 2),
                    Acce8 = Math.Round(rdm_acce.NextDouble() + 1, 2),
                });

                Acce_bind = Acce1.ToList();
                Thread.Sleep(500);
                acceDataDelete(); ;
                Thread.Sleep(300);
            }

        }
        private void acceDataDelete()
        {
            Acce1.Clear();
            Acce_bind = Acce1.ToList();
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
        public class Acce
        {
            public double Acce1 { get; set; }
            public double Acce2 { get; set; }
            public double Acce3 { get; set; }
            public double Acce4 { get; set; }
            public double Acce5 { get; set; }
            public double Acce6 { get; set; }
            public double Acce7 { get; set; }
            public double Acce8 { get; set; }
        }
    }
}

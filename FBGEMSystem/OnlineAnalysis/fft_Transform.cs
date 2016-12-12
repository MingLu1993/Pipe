using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBGEMSystem.OnlineAnalysis
{
   public class fft_Transform
    {
        public void DataSort(double[] data_r, double[] data_i)
        {
            if (data_r.Length == 0 || data_i.Length == 0 || data_r.Length != data_i.Length)
                return;
            int len = data_r.Length;
            int[] count = new int[len];
            //计算蝶形图的级数log2(len)
            int M = (int)(Math.Log(len) / Math.Log(2));
            double[] temp_r = new double[len];
            double[] temp_i = new double[len];

            for (int i = 0; i < len; i++)
            {
                temp_r[i] = data_r[i];
                temp_i[i] = data_i[i];
            }
            for (int l = 0; l < M; l++)
            {
                int space = (int)Math.Pow(2, l);
                int add = (int)Math.Pow(2, M - l - 1);
                for (int i = 0; i < len; i++)
                {
                    if ((i / space) % 2 != 0)
                        count[i] += add;
                }
            }
            for (int i = 0; i < len; i++)
            {
                data_r[i] = temp_r[count[i]];//对应的是temp_r中的0,4,2,6,1,5,3,7
                data_i[i] = temp_i[count[i]];//对应的是temp_i中的0,4,2,6,1,5,3,7
            }
        }
        //得到FFT的幅度值
        public void Dit2FFTAmplitude(double[] data_r, ref double[] result_amplitde)
        {
            int len = data_r.Length;
            double[] data_i = new double[len];
            double[] result_r = new double[len];
            double[] result_i = new double[len];

            Dit2_FFT(data_r, data_i, ref result_r, ref result_i);
            for (int i = 0; i < len; i++)
            {
                result_amplitde[i] = Math.Sqrt(Math.Pow(result_r[i], 2) + Math.Pow(result_i[i], 2));
            }

        }

        //FFT算法，按时间抽取的基2FFT算法
        //data_r  原始数据的实部
        //data_i  原始数据的虚部
        //result_r 频率的实部,返回值也是函数的参数，out可以用在一个函数有多个返回值的场所
        //result_i 频率的虚部
        public void Dit2_FFT(double[] data_r, double[] data_i, ref  double[] result_r, ref double[] result_i)
        {
            if (data_r.Length == 0 || data_i.Length == 0 || data_r.Length != data_i.Length)
            {
                return;
            }

            int len = data_r.Length;
            double[] X_r = new double[len];
            double[] X_i = new double[len];
            for (int i = 0; i < len; i++)//将源数据复制副本，避免影响源数据的安全性
            {
                X_r[i] = data_r[i];
                X_i[i] = data_i[i];
            }
            DataSort(X_r, X_i);//位置重排
            double WN_r, WN_i;//旋转因子
            int M = (int)(Math.Log(len) / Math.Log(2));//蝶形图级数
            for (int l = 0; l < M; l++)
            {
                int space = (int)Math.Pow(2, l);
                int num = space;//旋转因子个数
                double temp1_r, temp1_i, temp2_r, temp2_i;
                for (int i = 0; i < num; i++)
                {
                    int p = (int)Math.Pow(2, M - 1 - l);//同一旋转因子有p个蝶
                    //计算WN的实部                   
                    WN_r = Math.Cos(2 * Math.PI / len * p * i);
                    //计算WN的虚部
                    WN_i = -Math.Sin(2 * Math.PI / len * p * i);

                    //每一级蝶形运算的结果
                    for (int j = 0, n = i; j < p; j++, n += (int)Math.Pow(2, l + 1))
                    {
                        temp1_r = X_r[n];
                        temp1_i = X_i[n];
                        temp2_r = X_r[n + space];
                        temp2_i = X_i[n + space];//为蝶形的两个输入数据作副本，对副本进行计算，避免数据被修改后参加下一次计算
                        X_r[n] = temp1_r + temp2_r * WN_r - temp2_i * WN_i;
                        X_i[n] = temp1_i + temp2_i * WN_r + temp2_r * WN_i;
                        X_r[n + space] = temp1_r - temp2_r * WN_r + temp2_i * WN_i;
                        X_i[n + space] = temp1_i - temp2_i * WN_r - temp2_r * WN_i;
                    }
                }
            }
            result_r = X_r;
            result_i = X_i;
        }
        //
        public void ODD(double[] data_r, double[] data_i, double[] result_r, double[] result_i)
        {
            //var sw = Stopwatch.StartNew();

            Dit2_FFT(data_r, data_i, ref result_r, ref result_i);
            //Console.WriteLine("运行时间：" + sw.Elapsed.ToString());
        }
        //
        public void EVEN(double[] data_r, double[] data_i, double[] result_r, double[] result_i)
        {
            //var sw = Stopwatch.StartNew();

            Dit2_FFT(data_r, data_i, ref  result_r, ref  result_i);
            //Console.WriteLine("运行时间：" + sw.Elapsed.ToString());
        }



        //2线程，将N=8的数据分成2组，让2个函数同时运行，提高计算速度
        public void Calculate2FFT(double[] data_r, double[] data_i, double[] fft_r, double[] fft_i)
        {

            int len = data_r.Length;
            //even代表偶序列，odd代表基序列
            double[] even_data_r = new double[len / 2];
            double[] even_data_i = new double[len / 2];
            double[] even_fft_r = new double[len / 2];
            double[] even_fft_i = new double[len / 2];
            double[] odd_data_r = new double[len / 2];
            double[] odd_data_i = new double[len / 2];
            double[] odd_fft_r = new double[len / 2];
            double[] odd_fft_i = new double[len / 2];

            //进行奇偶序列的划分，even为0、2、4、6 ，odd为1、3、5、7
            for (int i = 0; i < len / 2; i++)
            {
                even_data_r[i] = data_r[2 * i];
                even_data_i[i] = data_i[2 * i];
                odd_data_r[i] = data_r[2 * i + 1];
                odd_data_i[i] = data_i[2 * i + 1];
            }
            // 尽可能并行执行提供的每个操作
            Parallel.Invoke(() => EVEN(even_data_r, even_data_i, even_fft_r, even_fft_i),
                            () => ODD(odd_data_r, odd_data_i, odd_fft_r, odd_fft_i));

            //对奇偶序列进行整合，进行DIT-FFT的最后一步
            for (int k = 0; k < len / 2; k++)
            {
                double WN_r = Math.Cos(2 * Math.PI / len * k);
                double WN_i = -Math.Sin(2 * Math.PI / len * k);

                fft_r[k] = even_fft_r[k] + WN_r * odd_fft_r[k] - WN_i * odd_fft_i[k];
                fft_i[k] = even_fft_i[k] + WN_r * odd_fft_i[k] + WN_i * odd_fft_r[k];
                fft_r[k + len / 2] = even_fft_r[k] - WN_r * odd_fft_r[k] + WN_i * odd_fft_i[k];
                fft_i[k + len / 2] = even_fft_i[k] - WN_r * odd_fft_i[k] - WN_i * odd_fft_r[k];

            }


        }

        //4线程，将N=8的数据分成4组，让四个函数同时运行，提高计算速度
        public void Calculate4FFT(double[] data_r, double[] data_i, double[] fft_r, double[] fft_i)
        {

            int len = data_r.Length;
            double[] even_data_1r = new double[len / 4];
            double[] even_data_1i = new double[len / 4];
            double[] even_data_2r = new double[len / 4];
            double[] even_data_2i = new double[len / 4];
            double[] even_fft_1r = new double[len / 4];
            double[] even_fft_1i = new double[len / 4];
            double[] even_fft_2r = new double[len / 4];
            double[] even_fft_2i = new double[len / 4];
            double[] odd_data_1r = new double[len / 4];
            double[] odd_data_1i = new double[len / 4];
            double[] odd_data_2r = new double[len / 4];
            double[] odd_data_2i = new double[len / 4];
            double[] odd_fft_1r = new double[len / 4];
            double[] odd_fft_1i = new double[len / 4];
            double[] odd_fft_2r = new double[len / 4];
            double[] odd_fft_2i = new double[len / 4];
            double[] fft_1r = new double[len / 2];
            double[] fft_1i = new double[len / 2];
            double[] fft_2r = new double[len / 2];
            double[] fft_2i = new double[len / 2];

            //进行奇偶序列的划分
            for (int i = 0; i < len / 4; i++)
            {
                even_data_1r[i] = data_r[4 * i];//0and4
                even_data_1i[i] = data_i[4 * i];
                odd_data_1r[i] = data_r[4 * i + 1];//1and5
                odd_data_1i[i] = data_i[4 * i + 1];
                even_data_2r[i] = data_r[4 * i + 2];//2and6
                even_data_2i[i] = data_i[4 * i + 2];
                odd_data_2r[i] = data_r[4 * i + 3];//3and7
                odd_data_2i[i] = data_i[4 * i + 3];
            }
            // 尽可能并行执行提供的每个操作
            Parallel.Invoke(() => EVEN(even_data_1r, even_data_1i, even_fft_1r, even_fft_1i),
                () => EVEN(even_data_2r, even_data_2i, even_fft_2r, even_fft_2i),
                            () => ODD(odd_data_1r, odd_data_1i, odd_fft_1r, odd_fft_1i),
                             () => ODD(odd_data_2r, odd_data_2i, odd_fft_2r, odd_fft_2i));
            //int M = (int)(Math.Log(len) / Math.Log(2));//蝶形图级数
            //对奇偶序列进行整合
            //对DIF-FFT的最后两步进行操作  
            for (int k = 0; k < len / 4; k++)
            {
                double WN_r = Math.Cos(2 * Math.PI / (len / 2) * k);
                double WN_i = -Math.Sin(2 * Math.PI / (len / 2) * k);


                fft_1r[k] = even_fft_1r[k] + WN_r * even_fft_2r[k] - WN_i * even_fft_2i[k];
                fft_1i[k] = even_fft_1i[k] + WN_r * even_fft_2r[k] + WN_i * even_fft_2i[k];
                fft_1r[k + len / 4] = even_fft_1r[k] - WN_r * even_fft_2r[k] + WN_i * even_fft_2i[k];
                fft_1i[k + len / 4] = even_fft_1r[k] - WN_r * even_fft_2r[k] - WN_i * even_fft_2i[k];
                ////end_r[k] = fft_r[k];
                //end_i[k] = fft_i[k];
            }
            for (int k = 0; k < len / 4; k++)
            {
                double WN_r = Math.Cos(2 * Math.PI / (len / 2) * k);
                double WN_i = -Math.Sin(2 * Math.PI / (len / 2) * k);


                fft_2r[k] = odd_fft_1r[k] + WN_r * odd_fft_2r[k] - WN_i * odd_fft_2i[k];
                fft_2i[k] = odd_fft_1i[k] + WN_r * odd_fft_2r[k] + WN_i * odd_fft_2i[k];
                fft_2r[k + len / 4] = odd_fft_1r[k] - WN_r * odd_fft_2r[k] + WN_i * odd_fft_2i[k];
                fft_2i[k + len / 4] = odd_fft_1r[k] - WN_r * odd_fft_2r[k] - WN_i * odd_fft_2i[k];
                ////end_r[k] = fft_r[k];
                //end_i[k] = fft_i[k];
            }

            //对奇偶序列进行整合
            for (int k = 0; k < len / 2; k++)
            {
                double WN_r = Math.Cos(2 * Math.PI / len * k);
                double WN_i = -Math.Sin(2 * Math.PI / len * k);

                fft_r[k] = fft_1r[k] + WN_r * fft_2r[k] - WN_i * fft_2i[k];
                fft_i[k] = fft_1r[k] + WN_r * fft_2r[k] + WN_i * fft_2i[k];
                fft_r[k + len / 2] = fft_1r[k] - WN_r * fft_2r[k] + WN_i * fft_2i[k];
                fft_i[k + len / 2] = fft_1r[k] - WN_r * fft_2r[k] - WN_i * fft_2i[k];
                ////end_r[k] = fft_r[k];
                //end_i[k] = fft_i[k];
            }

        }
    }
}

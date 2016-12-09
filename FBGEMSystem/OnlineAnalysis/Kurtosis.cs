using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FBGEMSystem.OnlineAnalysis
{
  public  class Kurtosis
    {
      public double kur_get_len(double[] signal, int start, int end, int flag)
      {
          double[] signal1 = new double[end];
          double kurvalue = 0;
          for (int i = start; i < start + end; i++)
          {
              signal1[i - start] = signal[i];

          }
          double[,] signal2 = new double[signal1.Length, 1];
          for (int i = 0; i < signal1.Length; i++)
          {
              signal2[i, 0] = signal1[i];
          }
          double[] kurt =kur(signal2, flag);
         kurvalue = kurt[0];
         return kurvalue;
      }
       public double[] kur(double[,] data, int flag)
        {
            int row = data.GetLength(0);
            int column = data.GetLength(1);
            int dim = 0;

            if (row != 1)
            {
                dim = 1;
            }
            else if ((row == 1) && (column != 1))
            {
                dim = 2;
            }
            else if ((row == 1) && (column == 1))
            {
                dim = 1;
            }
            else
            {
                Console.WriteLine("Error concur!");
            }

            //Console.WriteLine(dim);


            int N = Math.Max(dim, 2);
            double[] tile = new double[N];
            for (int i = 0; i < N; i++)
            {
                tile[i] = 1;
            }
            if (dim == 1)
            {
                tile[dim - 1] = row;
            }
            else
            {
                tile[dim - 1] = column;
            }

            //for (int i = 0; i < tile.Length; i++)
            //{
            //    Console.WriteLine(tile[i]);
            //}


            double[,] temp = new double[row, column];

            if (row == 1)
            {
                double sum = 0;
                int count = 0;
                double mean;

                for (int i = 0; i < column; i++)
                {
                    if (Double.IsNaN(data[row - 1, i]))
                    {
                        count += 1;
                        sum += 0;
                    }
                    else
                        sum += data[row - 1, i];
                }
                mean = sum / (column - count);
                for (int i = 0; i < column; i++)
                {
                    for (int j = 0; j < row; j++)
                    {
                        temp[j, i] = mean;
                    }
                }

            }
            else
            {

                double[] mean = new double[column];
                for (int i = 0; i < column; i++)
                {
                    double sum = 0;
                    int count = 0;
                    for (int j = 0; j < row; j++)
                    {
                        if (Double.IsNaN(data[j, i]))
                        {
                            count += 1;
                            sum += 0;
                        }
                        else
                            sum += data[j, i];
                    }
                    mean[i] = sum / (row - count);
                }

                for (int i = 0; i < column; i++)
                {
                    for (int j = 0; j < row; j++)
                    {
                        temp[j, i] = mean[i];
                    }
                }

            }

            //for (int i = 0; i < temp.GetLength(0); i++)
            //{
            //    string str = "";
            //    for (int j = 0; j < temp.GetLength(1); j++)
            //    {
            //        str = str + Convert.ToString(temp[i, j]) + " ";
            //    }
            //    Console.Write(str);
            //    Console.Write("\n");
            //}


            double[,] x0 = new double[row, column];
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    x0[i, j] = data[i, j] - temp[i, j];
                }
            }


            //for (int i = 0; i < x0.GetLength(0); i++)
            //{
            //    string str = "";
            //    for (int j = 0; j < x0.GetLength(1); j++)
            //    {
            //        str = str + Convert.ToString(x0[i, j]) + " ";
            //    }
            //    Console.Write(str);
            //    Console.Write("\n");
            //}

            double[,] x0_temp1 = new double[row, column];
            double[,] x0_temp2 = new double[row, column];
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    x0_temp1[i, j] = Math.Pow(x0[i, j], 2);
                }
            }

            //for (int i = 0; i < x0_temp1.GetLength(0); i++)
            //{
            //    string str = "";
            //    for (int j = 0; j < x0_temp1.GetLength(1); j++)
            //    {
            //        str = str + Convert.ToString(x0_temp1[i, j]) + " ";
            //    }
            //    Console.Write(str);
            //    Console.Write("\n");
            //}

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    x0_temp2[i, j] = Math.Pow(x0[i, j], 4);
                }
            }

            //for (int i = 0; i < x0_temp2.GetLength(0); i++)
            //{
            //    string str = "";
            //    for (int j = 0; j < x0_temp2.GetLength(1); j++)
            //    {
            //        str = str + Convert.ToString(x0_temp2[i, j]) + " ";
            //    }
            //    Console.Write(str);
            //    Console.Write("\n");
            //}

            double[] result;

            if (dim == 1)
            {
                double[] s2 = new double[column];
                double[] m4 = new double[column];
                for (int i = 0; i < column; i++)
                {
                    double sum = 0;
                    int count = 0;
                    for (int j = 0; j < row; j++)
                    {
                        if (Double.IsNaN(x0_temp1[j, i]))
                        {
                            count += 1;
                            sum += 0;
                        }
                        else
                            sum += x0_temp1[j, i];
                    }
                    s2[i] = Math.Pow(sum / (row - count), 2);
                }
                for (int i = 0; i < column; i++)
                {
                    double sum = 0;
                    int count = 1;
                    for (int j = 0; j < row; j++)
                    {
                        if (Double.IsNaN(x0_temp2[j, i]))
                        {
                            count += 1;
                            sum += 0;
                        }
                        else
                            sum += x0_temp2[j, i];
                    }
                    m4[i] = sum / row;
                }

                //for (int i = 0; i < s2.Length; i++)
                //{
                //    Console.WriteLine(s2[i]);
                //}

                //for (int i = 0; i < m4.Length; i++)
                //{
                //    Console.WriteLine(m4[i]);
                //}
                double[] result_temp = new double[column];
                for (int i = 0; i < column; i++)
                {
                    result_temp[i] = m4[i] / s2[i];
                }

                result = result_temp;
            }
            else
            {
                double[] s2 = new double[row];
                double[] m4 = new double[row];
                for (int i = 0; i < row; i++)
                {
                    double sum = 0;
                    int count = 0;
                    for (int j = 0; j < column; j++)
                    {
                        if (Double.IsNaN(x0_temp1[i, j]))
                        {
                            count += 1;
                            sum += 0;
                        }
                        else
                            sum += x0_temp1[i, j];
                    }
                    s2[i] = Math.Pow(sum / (column - count), 2);
                }
                for (int i = 0; i < row; i++)
                {
                    double sum = 0;
                    int count = 0;
                    for (int j = 0; j < column; j++)
                    {
                        if (Double.IsNaN(x0_temp2[i, j]))
                        {
                            count += 1;
                            sum += 0;
                        }
                        else
                            sum += x0_temp2[i, j];
                    }
                    m4[i] = sum / (column - count);
                }
                //for (int i = 0; i < s2.Length; i++)
                //{
                //    Console.WriteLine(s2[i]);
                //}
                //for (int i = 0; i < m4.Length; i++)
                //{
                //    Console.WriteLine(m4[i]);
                //}
                double[] result_temp = new double[row];
                for (int i = 0; i < row; i++)
                {
                    result_temp[i] = m4[i] / s2[i];
                }

                result = result_temp;
            }
            //for (int i = 0; i < result.Length; i++)
            //{
            //    Console.Write(result[i] + " ");
            //}
            //Console.WriteLine(dim);
            if (flag == 0)
            {
                double[] n;
                if (dim == 1)
                {
                    n = new double[column];
                    double[] count = new double[column];
                    for (int j = 0; j < column; j++)
                    {
                        for (int i = 0; i < row; i++)
                        {
                            if (Double.IsNaN(data[i, j]))
                                count[j] += 1;

                        }
                        n[j] = row - count[j];
                    }

                }
                else
                {
                    n = new double[row];
                    int count = 0;
                    for (int i = 0; i < row; i++)
                    {
                        for (int j = 0; j < column; j++)
                        {
                            if (Double.IsNaN(data[i, j]))
                                count += 1;
                        }

                        n[i] = column - count;
                    }
                }
                //for (int i = 0; i < n.Length; i++)
                //{
                //    Console.WriteLine(n[i]);
                //}

                for (int i = 0; i < n.Length; i++)
                {
                    if (n[i] < 4)
                        n[i] = double.NaN;
                }
                //for (int i = 0; i < n.Length; i++)
                //{
                //    Console.WriteLine(n[i]);
                //}               
                for (int i = 0; i < result.Length; i++)
                {
                    result[i] = ((n[i] + 1) * result[i] - 3 * (n[i] - 1)) * (n[i] - 1) / ((n[i] - 2) * (n[i] - 3)) + 3;

                }

            }

            for (int i = 0; i < result.Length; i++)
            {
                Console.Write(result[i] + " ");
            }

            return result;

        }
    }
}

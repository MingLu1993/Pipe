using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Learn;
using MathWorks.MATLAB.NET.Arrays;

namespace FBGEMSystem
{
    class DiagnosisUser
    {
        private static CLearn learn = new CLearn();

        //用于故障诊断
        
        public static int K = 0;               //进行第k次增量训练
        public static MWCellArray FinalTree = new MWCellArray();   //存储所有树结构
        public static MWCellArray Finalmacro = new MWCellArray();  //存储所有树的权重
        public static MWCellArray FinalMACRO = new MWCellArray();  //存储所有子集成分类器的权重

        public static double Rata = 0.7;       //迭代过程中抽取训练样本的比例
        public static double Inc_node = 5;     //决策树停止训练的条件，表示样本数小于一定阈值结束递归，可设置为5-10
        public static double[] Discrete_dim;   //用于记录每一个特征是否是离散特征，初始化都记为0，代表都是连续特征，
        //public double[,] Trains;        //训练样本，一行为一个样本

        public static int T = 0;           //设置每次增量训练的迭代次数，程序初始化时需设置
        public static int TargetsNum ;  //管路状态个数，类别个数，程序初始化时需设置
        public static object a =new object();

        public static void TRAIN( double[,] Trains)
        {
            lock(a)
            {
                Discrete_dim = new double[Trains.GetLength(1)-1];
                MWNumericArray Trains_M = Trains;
                MWNumericArray discrete_dim_M = Discrete_dim;
                MWArray[] result_train = new MWArray[3];
                //3：           函数返回三个参数
                //finalTree     已有的所有树结构
                //finalmacro    已有所有树的权重
                //finalMACRO    存储已有子集成分类器的权重
                //k             进行第k次增量训练
                //Trains_M        训练样本，一行为一个样本
                //T             每次增量训练的迭代次数
                //rata          迭代过程中抽取训练样本的比例
                //inc_node      决策树停止训练的条件，表示样本数小于一定阈值结束递归，可设置为5-10
                //discrete_dim_M  用于记录每一个特征是否是离散特征，初始化都记为0，代表都是连续特征，
                K++;
                result_train = learn.trainMacro(3, FinalTree, Finalmacro, FinalMACRO, K,
                                           Trains_M, T, Rata, Inc_node, discrete_dim_M);
                FinalTree = (MWCellArray)result_train[0];
                Finalmacro = (MWCellArray)result_train[1];
                FinalMACRO = (MWCellArray)result_train[2];
            } 
        }

        public static int PREDICT(double[,] test_patterns)
        {
            lock(a)
            {
                if(K>0)
                {
                    MWArray[] result_predict=new MWArray[1];
                    MWNumericArray test_patterns_M = test_patterns;
                    Discrete_dim = new double[test_patterns.GetLength(1)];
                    MWNumericArray discrete_dim_M = Discrete_dim;

                    result_predict = learn.PredictMacro(1, K, T, FinalTree, test_patterns_M,
                                                Finalmacro, FinalMACRO, discrete_dim_M, TargetsNum);
                    int result = ((MWNumericArray)(result_predict[0])).ToScalarInteger();
                    return result;
                }
                return -1;
            }
        }//end PREDICT

    }
}

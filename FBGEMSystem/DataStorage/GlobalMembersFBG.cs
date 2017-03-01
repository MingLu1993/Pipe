using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FBGEMSystem
{
    public class DEVICESTATUS //系统工作状态结构体
    {
        //解调仪解调速率
        public float fDemodulateSpeed;
        public int iSampleSpeedCnt;

        //从FPGAFlash读取数据信息
        public bool bIsReadFlash;
        public bool bIsReadConfig;

        //FBG锁定
        public bool bIslocking; // 是否锁定FBG

        //AD解码同步
        public bool bIsADDecodeSyn; // AD解析是否同步 true:已同步 开始解析 false:没有同步 需要同步

        //发送
        public bool bIsUDPSend; // UDP是否开始发送 true:开始发送 false:停止发送
        public bool bIsDBSend; // 远程发送按钮开关标志 true:开始发送 false：停止发送

        //保存文件
        public bool bIsSave; //true:正在保存文件；false:停止保存文件
        public int iSampleSaveSpeed;

        //AD Or Peak数据
        public int iAdPeakChoose; // AD波形数据Or峰值数据 1:AD波形数据 2:峰值数据

        //通信错误计数
        public ushort usiFrameCounter;
        public ushort usiFrameCounterOld;
        public int iframeErrorCnt;
        public int icntSingleError;
        public int icntRefError;
        public int icntPosSingleError;
        public int icntRefNumZeroError;

        // 上位机自动调节相关
        public bool bFPErrorState; // 判断是否有峰值变化的错误
        public int iFPBaseLineNow; // 当前FP驱动电平的中间值 -自动后的
        public int iFPBaseLineRange; // 目前在有效范围内波动的FP驱动电平值，根据中间值而定
        public int iSinglePos; // 设置的自动调节单峰位置
        public int iSinglePosNow; // 当前采样的单峰计数值
        public int iSinglePosWidth; // 单峰阈值
        public int iFPBaseLineRangeAuto; // FP自动调整

        //F-P驱动与单峰自动调节
        public bool bFPBaseLinebUpOrDown; //向上或者下调整FP驱动 true:Up false:Down
        public bool bUpOrDown;
        public int lerrorRefOld; // 用于判断峰值是否有变化使用
        public int lerrorRefNow;

        public bool bIsAutoFP; // true 开始自动调节 false 不自动调节
        public bool bIsAutoSinglePos; // true:正在自动调整FP 达到阈值范围内
        public bool bIsReadyForSinglePos; // 开机自动定单峰位置

        public bool bIsFPCntAction; // 是否开启在峰值变化下自动调整FP

        //温度补偿
        public int igcntRef;
        public bool btempTick;
        public bool btempTickCnt;

        //采集界面参数
        public bool bIsShowCount;
        //X轴参数
        public int igrangX;
        public int igrangXAD;

        //刷新界面
        public int igtime10s;

        //UDP包发送情况
        public bool[] bIsChUDPSend = new bool[4];
        public int[] iChUDPSendNum = new int[4];

        //解调频率设置
        public int setRateState;
    }


    public class DEVICECONFIG //系统设备配置结构体
    {
        //配置用
        public string strConfigFlash;
        public string strVision;
        public bool bIsReadConfig;

        //标定信息
        public float[] fPeaksDefined = new float[100];
        //    ={25,
        //		1282.33, 1284.35, 1286.37, 1288.37, 1290.43, 
        //		1292.44, 1294.51, 1296.56, 1298.62, 1300.69,
        //		1302.76, 1304.84, 1306.92, 1309.02, 1311.13,
        //		1313.23, 1315.35, 1317.47, 1319.60, 1321.72,
        //		1323.80, 1326.03, 1328.19, 1330.36, 1332.53
        //	};
        //	
        public int iDefinedOffsetSingle; // = 14;

        //保存信息
        public string strSaveFilePath;
        public int isizeBin; //二进制文件最大长度
        public int isizeText; //文本文件最大长度
        public bool bisSaveBin; //是否需要保存二进制文件
        public bool bisSaveText; //是否需要保存文本文件
        public string strFilePath; //数据文件保存路径
        public string strCapPath; //截图文件保存路径
        public bool bisPeakValueSave;

        //F-P驱动与单峰值
        public int iDemodulateFPBaseLine; //初始化F-P腔电平驱动使用
        public int iDemodulateSinglePos; //初始化单峰位置使用

        //增益、阈值
        public byte[] uchpinsVGA = new byte[6]; //各通道增益控制顺序
        public float fvga12;
        public float fvga3456; //40db->1v 45db->1.125v
        public float fvgaMax; //最大增益值 80db
        public float fvgaMin; //最小增益值
        public float[] fvga = new float[6];
        public byte[] uchpinsTh = new byte[6]; // 各通道阈值控制顺序
        public float fth123456; //0.1->100mv
        public float fthMax;
        public float fthMin;

        public int fWLRange;
    }

    //sealed用于类时，表示该类不能再被继承
    //internal只有在同一程序集的文件中，内部类型或成员才是可访问的。
    internal sealed class DefineConstantsFunc
    {
        public const int CONFIG_NUMOFOTHERDATA = 3;
    }
    public class GlobalMembersFBG
    {

        public static ushort[,,] gPeaksAll = new ushort[1001, 6, 64]; 
        public static byte[,,] gPosPeaks = new byte[1001, 6, 64];     //保存数据在gPeaksAll中的位置,[i,x,0]为第x通道内的数据个数,
                                                                      //[ , ,i]存储数据在gPeaksAllf中的索引
        public static byte[] bUpPosPeaks = new byte[64];
        public static ushort[] bUpPeaksAll = new ushort[64];
        public static ushort[] bUpSigPeak = new ushort[2];
        public static float[,,] gPeaksAllf = new float[1001, 6, 64]; //Tangible Process Only End
                                                                     //数据存放数组，[i,x,0]为0

        public static float[,] gPeeks1T = new float[6, 64];     //用于显示,程序中在gPeaksAllf取第一行放入
        //0:个数，梳妆。。。
        //1:个数，单峰。。。
        //2:个数，通道1。。。
        //3:个数，通道2。。。
        //4:个数，通道3。。。
        //5:个数，通道4。。。



        public static DEVICECONFIG g_DeviceConfigPara = new DEVICECONFIG();
        public static DEVICESTATUS g_DeviceStatusPara = new DEVICESTATUS();


        //C++ TO C# CONVERTER NOTE: This was formerly a static local variable declaration (not allowed in C#):
        private byte[] gbuf = new byte[1000];
        //C++ TO C# CONVERTER NOTE: This was formerly a static local variable declaration (not allowed in C#):
        private int gbufIndex = 0;

        //C++ TO C# CONVERTER NOTE: This was formerly a static local variable declaration (not allowed in C#):
        private static int dataType = -1;
        //C++ TO C# CONVERTER NOTE: This was formerly a static local variable declaration (not allowed in C#):
        //private static int gADWavePos = 0;
        //C++ TO C# CONVERTER NOTE: This was formerly a static local variable declaration (not allowed in C#):
        private static bool bIsConnect = false;

        //C++ TO C# CONVERTER NOTE: This was formerly a static local variable declaration (not allowed in C#):
        private int iPos = 0;   //在DecodeOneFrame中使用

        //C++ TO C# CONVERTER NOTE: This was formerly a static local variable declaration (not allowed in C#):
        private ushort oldPeakSingle = 0; //在PeaksLock中使用
        private ushort oldnumCh0;        //在PeaksLock中使用
        private bool is_oldnumCh0 = false;  //在PeaksLock中使用，判断oldnumCh0是否赋过初值
        private float[] curPeaksRef = new float[64];   //在PeaksLock中使用

        //public string RecvBuf ;
        public DateTime currentTime1 = new DateTime();
        public string timeNow1;
        public FileStream fs1;
        public StreamWriter sw1;
        public bool iswriting = false;
        public bool isfileClosed = true;

        //记录存进msgFBG的数据的个数，达到40个存入缓存并清零
        int MessageIndex = 0;
        Message_FBG msgFBG = new Message_FBG();

        //用于取byte数组的子数组，
        //allByte数组
        //subindex;从该索引后的子数组
        public byte[] byteSub(byte[] allByte,int subindex)
        {
            byte[] newData;
            if (subindex<allByte.Count())
            {
                newData = new byte[allByte.Count() - subindex];
                Array.Copy(allByte, subindex, newData, 0, allByte.Count() - subindex);
                //sourceArray
                //    Type: System.Array
                //    包含要复制的数据的 Array。
                //sourceIndex
                //    Type: System.Int32
                //    一个 32 位整数，它表示 sourceArray 中复制开始处的索引。
                //destinationArray
                //    Type: System.Array
                //    接收数据的 Array。
                //    destinationIndex
                //    Type: System.Int32
                //    一个 32 位整数，它表示 destinationArray 中存储开始处的索引。
                //length
                //    Type: System.Int32
                //    一个 32 位整数，它表示要复制的元素数目。
                return newData;
            }
            newData =new byte[1] ;
            return newData;
            
        }
        public void initial()
        {
            //g_DeviceStatusPara.bIsReadFlash = true;
        }

        //接收数据后调用该函数！！！！
        //recvBuf：接收到的数据的存放byte数组
        //nRecv：接收到的数据的长度
        //传入接受的数据，将FBG数据解析到gPeaksAllf
        internal int dataDecodingEntry(byte[] recvBuf, int nRecv) 
        {
            // string recvBuf = new string(new char[10000]);
            //C++ TO C# CONVERTER NOTE: This static local variable declaration (not allowed in C#) has been moved just prior to the method:
            //	static int dataType=-1;
            //C++ TO C# CONVERTER NOTE: This static local variable declaration (not allowed in C#) has been moved just prior to the method:
            //	static int gADWavePos=0;
            //C++ TO C# CONVERTER NOTE: This static local variable declaration (not allowed in C#) has been moved just prior to the method:
            //	static bool bIsConnect = false;
            //initial();

            // int nRecv = recv(m_socket, recvBuf, 8010, 0); //套接字接收下位机数据

            if (nRecv < 0)
            {
                return -1;
            }
            //与服务器端建立连接后，接收服务器端发送的"WHUTFBGV1"
            if (nRecv <= 20)
            {
                if (   ('W' == recvBuf[0])
                    && ('H' == recvBuf[1])
                    && ('U' == recvBuf[2])
                    && ('T' == recvBuf[3])
                    && ('F' == recvBuf[4])
                    && ('B' == recvBuf[5])
                    && ('G' == recvBuf[6])
                    && ('V' == recvBuf[7])
                    && ('1' == recvBuf[8]))
                {
                    if ('P' == recvBuf[9])
                    {
                        dataType = 0;
                    }
                    else if ((recvBuf[9] >= 'A') && (recvBuf[9] <= 'F'))
                    {
                        dataType = 1 + recvBuf[9] - 'A';
                    }
                    bIsConnect = true;
                }
            }
            //接收到"WHUTFBGV1"后，发送"C\n"给服务器端，然后接收配置数据
            else if (nRecv <= 1000 && g_DeviceStatusPara.bIsReadFlash)
            {
                if (bIsConnect)
                {
                    DecodeFPGAFlashConfig(nRecv, recvBuf);
                }
                else
                {

                    //DecodeFPGAFlashConfig(nRecv-10,recvBuf+10);
                    DecodeFPGAFlashConfig(nRecv - 10, byteSub(recvBuf,10));
                }
            }

            //发送"Z\n"后，接收FBG数据，并解析
            else
            {
                DecodePeaksRaw(nRecv, recvBuf);
                decodeDataStore();   //测试用
            }



            return 0;
        }

        float[] CH1 = new float[64 * 40];
        float[] CH2 = new float[64 * 40];
        float[] CH3 = new float[64 * 40];
        float[] CH4 = new float[64 * 40];
        //解析后的完整数据，需测试
        private void decodeDataToArray(int tIndex)
        {
            for (int j = 2; j < 6; j++)//取FBG四个通道
            {
                int numChX = gPosPeaks[tIndex, j, 0];

                if (numChX < 64)
                {
                    //gPeeks1T[j, 0] = gPosPeaks[tIndex, j, 0];
                    for (int i = 1; i <= numChX; i++)
                    {
                        int pos = gPosPeaks[tIndex, j, i];
                        //gPeeks1T[j, i] = gPeaksAllf[tIndex, j, pos];
                        switch (j)
                        {
                            case 2:
                                CH1[MessageIndex * 64 + i - 1] = gPeaksAllf[tIndex, j, pos];

                                // msgFBG.CH1[MessageIndex * 64 + i - 1] = gPeaksAllf[tIndex, j, pos];
                                break;
                            case 3:
                                CH2[MessageIndex * 64 + i - 1] = gPeaksAllf[tIndex, j, pos];
                                //msgFBG.CH2[MessageIndex * 64 + i - 1] = gPeaksAllf[tIndex, j, pos];
                                break;
                            case 4:
                                CH3[MessageIndex * 64 + i - 1] = gPeaksAllf[tIndex, j, pos];
                                //msgFBG.CH3[MessageIndex * 64 + i - 1] = gPeaksAllf[tIndex, j, pos];
                                break;
                            case 5:
                                CH4[MessageIndex * 64 + i - 1] = gPeaksAllf[tIndex, j, pos];
                                //msgFBG.CH4[MessageIndex * 64 + i - 1] = gPeaksAllf[tIndex, j, pos];
                                break;
                            default:break;
                        }
                    }
                }
            }
            MessageIndex++;
            if(MessageIndex > 39)
            {
                msgFBG.CH1 = CH1;
                msgFBG.CH2 = CH2;
                msgFBG.CH3 = CH3;
                msgFBG.CH4 = CH4;

                string timenow = System.DateTime.Now.ToLongTimeString().ToString();
                string NowTime = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + 
                                 DateTime.Now.Day.ToString() + "-"+ timenow;
                msgFBG.dataTime = NowTime;
                //存入存储缓冲
                Receiver.sharedLocation_FBG.Buffer = msgFBG;
                if(Data.IsControlFBG == true)
                {
                    Receiver.process_all_msg_FBG.Buffer = msgFBG;
                }
                MessageIndex = 0;
                msgFBG = new Message_FBG();
                CH1 = new float[64 * 40];
                CH2 = new float[64 * 40];
                CH3 = new float[64 * 40];
                CH4 = new float[64 * 40];
            }
        }

        //解析后的显示数据//测试用
        private void decodeDataStore()
        {
            //取梳妆
            int tIndex = gPeaksAll[1000, 0, 0];
            int numCh0 = gPosPeaks[tIndex,0,0];
            if (numCh0 < 64)//参考通道的
            {
                gPeeks1T[0,0] = gPosPeaks[tIndex,0,0];
                for (int i = 1; i <= numCh0; i++)
                {
                    int pos = gPosPeaks[tIndex,0,i];
                    gPeeks1T[0,i] = gPeaksAll[tIndex,0,pos];
                }
            }

            for (int j = 1; j < 6; j++)//其它5通道
            {
                int numChX = gPosPeaks[tIndex,j,0];
                if (numChX < 64)
                {
                    gPeeks1T[j,0] = gPosPeaks[tIndex,j,0];
                    for (int i = 1; i <= numChX; i++)
                    {
                        int pos = gPosPeaks[tIndex,j,i];
                        //if (g_DeviceStatusPara.bIsShowCount)
                        //    gPeeks1T[j,i] = gPeaksAll[tIndex,j,pos];
                        //else
                        gPeeks1T[j,i] = gPeaksAllf[tIndex,j,pos];

                    }
                }
            }
            if(iswriting == true)
            {
                sw1 = new StreamWriter(fs1);

                string writefile = "";
                for (int i = 0; i < 6; i++)
                {
                    for (int k = 0; k < 5; k++)
                    {
                        // if (gPeaksAllf[j, i, k]!=0)

                        // sw1.Write(gPeaksAllf[j, i, k]); 
                        writefile += gPeeks1T[i, k].ToString() + "\t";
                    }

                }
                writefile += "\r\n";
                sw1.Write(writefile);
                sw1.Flush();
            }
            else
            {
                if (isfileClosed == false)
                {
                    sw1.Close();
                    fs1.Close();
                    isfileClosed = true;
                }
            }

        }

        /*******函数名：DecodeFPGAFlashConfig********/
        //解析配置数据，
        //len：传入的数据的长度
        //p：传入的数据的数组
        public int DecodeFPGAFlashConfig(int len, byte[] p)
        {
            //string str;
            string strA = "";
            string strATemp = "";
            string strATemp1 = "";
            int iIndex = 0;
            double fTemp = 0;
            int iTemp = 0;
            int iNumOfSZ = 0;
            //string.IsNullOrEmpty(strA);
            //string.IsNullOrEmpty(strATemp);
            //string.IsNullOrEmpty(strATemp1);

            int iNumOfData = 0;
            while (iIndex < len)
            {
                //strATemp.Format("%c", p[iIndex++]);
                strATemp = ((char)p[iIndex++]).ToString();//????????????可能理解错误
                strATemp1 += strATemp;
                if (strATemp == ";")
                {
                    //单峰位置
                    if (iNumOfData == 0)
                    {
                        iTemp = Convert.ToInt32(strA);
                        g_DeviceConfigPara.iDefinedOffsetSingle = iTemp;
                    }
                    //梳妆个数
                    else if (iNumOfData == 1)
                    {
                        iTemp = Convert.ToInt32(strA);
                        g_DeviceConfigPara.fPeaksDefined[0] = (float)iTemp - DefineConstantsFunc.CONFIG_NUMOFOTHERDATA - 7;
                        iNumOfSZ = iTemp - DefineConstantsFunc.CONFIG_NUMOFOTHERDATA - 7;
                    }
                    //版本号
                    else if (iNumOfData == 2)
                    {
                        g_DeviceConfigPara.strVision = strA;
                    }
                    //梳妆定位
                    else if (iNumOfData < (iNumOfSZ + DefineConstantsFunc.CONFIG_NUMOFOTHERDATA))
                    {
                        fTemp = Convert.ToDouble(strA);
                        g_DeviceConfigPara.fPeaksDefined[iNumOfData - DefineConstantsFunc.CONFIG_NUMOFOTHERDATA + 1] = (float)fTemp;
                    }
                    //F-P
                    else if (iNumOfData == (iNumOfSZ + DefineConstantsFunc.CONFIG_NUMOFOTHERDATA))
                    {
                        fTemp = Convert.ToDouble(strA);
                        g_DeviceConfigPara.iDemodulateFPBaseLine = (int)(fTemp * 10);
                        g_DeviceStatusPara.iFPBaseLineRange = g_DeviceStatusPara.iFPBaseLineNow = g_DeviceConfigPara.iDemodulateFPBaseLine;
                        g_DeviceStatusPara.lerrorRefOld = g_DeviceStatusPara.icntRefError; //cntRefError;
                    }
                    //单峰
                    else if (iNumOfData == (iNumOfSZ + DefineConstantsFunc.CONFIG_NUMOFOTHERDATA + 1))
                    {
                        fTemp = Convert.ToDouble(strA);
                        g_DeviceConfigPara.iDemodulateSinglePos = (int)(fTemp * 10);
                    }
                    //梳妆增益
                    else if (iNumOfData == (iNumOfSZ + DefineConstantsFunc.CONFIG_NUMOFOTHERDATA + 2))
                    {
                        fTemp = Convert.ToDouble(strA);
                        g_DeviceConfigPara.fvga[0] = (float)(fTemp);
                    }
                    //单峰增益
                    else if (iNumOfData == (iNumOfSZ + DefineConstantsFunc.CONFIG_NUMOFOTHERDATA + 3))
                    {
                        fTemp = Convert.ToDouble(strA);
                        g_DeviceConfigPara.fvga[1] = (float)(fTemp);
                    }
                    //CH1增益
                    else if (iNumOfData == (iNumOfSZ + DefineConstantsFunc.CONFIG_NUMOFOTHERDATA + 4))
                    {
                        fTemp = Convert.ToDouble(strA);
                        g_DeviceConfigPara.fvga[2] = (float)(fTemp);
                    }
                    //CH2增益
                    else if (iNumOfData == (iNumOfSZ + DefineConstantsFunc.CONFIG_NUMOFOTHERDATA + 5))
                    {
                        fTemp = Convert.ToDouble(strA);
                        g_DeviceConfigPara.fvga[3] = (float)(fTemp);
                    }
                    //CH3增益
                    else if (iNumOfData == (iNumOfSZ + DefineConstantsFunc.CONFIG_NUMOFOTHERDATA + 6))
                    {
                        fTemp = Convert.ToDouble(strA);
                        g_DeviceConfigPara.fvga[4] = (float)(fTemp);
                    }
                    //CH4增益
                    else if (iNumOfData == (iNumOfSZ + DefineConstantsFunc.CONFIG_NUMOFOTHERDATA + 7))
                    {
                        fTemp = Convert.ToDouble(strA);
                        g_DeviceConfigPara.fvga[5] = (float)(fTemp);
                    }
                    //波段
                    else if (iNumOfData == (iNumOfSZ + DefineConstantsFunc.CONFIG_NUMOFOTHERDATA + 8))
                    {
                        fTemp = Convert.ToDouble(strA);
                        g_DeviceConfigPara.fWLRange = (int)(fTemp);
                    }
                    //strA.empty();
                    strA = "";
                    iNumOfData++;
                }
                else
                {
                    strA += strATemp;
                }
            }
            g_DeviceStatusPara.bIsReadFlash = false;
            g_DeviceConfigPara.strConfigFlash = strATemp1;
            g_DeviceStatusPara.bIsReadConfig = true;
            return iNumOfSZ;
        }

        /*******函数名：DecodePeaksRaw********/
        //解析FBG数据
        //len：传入的数据的长度
        //p：传入的数据的数组
        public byte DecodePeaksRaw(int len, byte[] p)
        {
            //C++ TO C# CONVERTER NOTE: This static local variable declaration (not allowed in C#) has been moved just prior to the method:
            //	static sbyte gbuf[1000];
            byte[] buf = new byte[1000];
            //C++ TO C# CONVERTER NOTE: This static local variable declaration (not allowed in C#) has been moved just prior to the method:
            //	static int gbufIndex = 0;
            byte err = 0;
            int i = 0;
            int tIndex = gPeaksAll[1000, 0, 0];
            byte reg = 0;

            if (p[i] != 0xe0) //如果不是e0头 e0??????
            {
                if (gbufIndex > 0)
                {
                    for (int k = 0; k < gbufIndex; k++) //复制上一数据包残留数据帧到buf中
                    {
                        buf[k] = gbuf[k];
                    }

                    if (gbufIndex + len < (buf[1] - 1) * 2)
                    {
                        for (int k = 0; k < len; k++)
                        {
                            buf[gbufIndex + k] = p[k];
                        }
                        gbufIndex = gbufIndex + len;
                        return 1;
                    }
                    else
                    {
                        for (int k = 0; k < (buf[1] - 1) * 2 - gbufIndex; k++)
                        {
                            buf[gbufIndex + k] = p[k];
                            i++;
                        }
                        if ((buf[1] - 1) * 2 == i + gbufIndex)
                        {

                            //DecodeOneFrame(buf + 1, tIndex);
                            DecodeOneFrame(byteSub(buf, 1), buf.Count(), 1, buf.Count() - 1, tIndex);
                        }
                        else
                        {
                            err |= 0x2; //错误2：拼合的残留数据帧不完整
                            while (p[i] != 0xe0) //丢弃该数据包头的残留帧
                            {
                                i++;
                                i++;
                                if (i > len)
                                    return 255;
                            }
                        }
                        gbufIndex = 0; //gbufIndex清零
                    }
                }
                else
                {
                    err |= 0x1; //错误1：没有吻合残留数据
                    while (p[i] != 0xe0) //丢弃该数据包头的残留帧
                    {
                        i++;
                        i++;
                        if (i > len )   ///!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                            return 255;
                    }
                }
            }
            //else
            {
                //byte totalFrNum = (byte)(*(p + i + 1));
                byte totalFrNum = p[i + 1];
                i--;
                totalFrNum--;
                while ((totalFrNum * 2 + i + 1) <= len)
                {
                    //i++;//移动一格读总帧数
                    //totalFrNum = (unsigned char)(*(p+i));//???

                    i++;
                    //reg = (byte)(*(p + i));
                    reg = p[i];
                    if (reg != 0xe0)
                    {
                        err |= 0x40; //错误3：数据帧头e0错乱
                        while (reg != 0xe0) //丢弃该数据包头的残留帧 ?? to do
                        {
                            //reg = (byte)(*(p + i));
                            reg = p[i];
                            i++;
                            i++;
                            if (i > len)
                                return 255;
                        }
                    }
                    i++; //移动一格读总帧数
                         //totalFrNum = (byte)(*(p + i));
                    totalFrNum = p[i];
                    totalFrNum--;
                    //i += DecodeOneFrame(p[i], tIndex);
                    i += DecodeOneFrame(byteSub(p, i), p.Count() , i , p.Count()-i , tIndex);
                }
                i++;
                if (i < len)
                {
                    gbufIndex = len - i;
                    for (int k = 0; k < gbufIndex; k++)
                    {
                        //gbuf = StringHelper.ChangeCharacter(gbuf, k, (byte)(*(p + i)));
                        gbuf[k] = p[i];
                        i++;
                    }
                }
            }
            return err;

        }


        public int DecodeOneFrame(byte[] p,int len,int dsb,int count, int tIndex)
        {
            //C++ TO C# CONVERTER NOTE: This static local variable declaration (not allowed in C#) has been moved just prior to the method:
            //	static int iPos = 0;
            int i = 0;
            int ret = 0;
            i++;
            //帧计数验证
            g_DeviceStatusPara.usiFrameCounter = p[i];
            i++;
            g_DeviceStatusPara.usiFrameCounter = (ushort)((g_DeviceStatusPara.usiFrameCounter << 8) | p[i]);
            if ((g_DeviceStatusPara.usiFrameCounterOld + 1) != g_DeviceStatusPara.usiFrameCounter)
            {
                g_DeviceStatusPara.iframeErrorCnt++;
                //frameErrorCnt++;
            }
            g_DeviceStatusPara.usiFrameCounterOld = g_DeviceStatusPara.usiFrameCounter;


            //读取一帧数据
            for (int j = 0; j < 6; j++) //遍历所有通道
            {
                i++; //移动一格读f0
                if (p[i] == (byte)(0xf0 + j))
                {
                    i++; //移动一格读 ch0 num
                    byte chNum0 = p[i];
                    //gPeaksAll[tIndex][j][0] = chNum0;
                    gPosPeaks[tIndex, j, 0] = chNum0; //********************************************************************************************
                    for (int k = 1; k <= chNum0; k++)
                    {
                        ushort temp;

                        i++; //移动一格读 ch0 num后数据 高位
                             //gPeaksAll[tIndex][j][k] = (unsigned char)(*(p+i));
                        temp = p[i];
                        i++; //移动一格读 ch0 num后数据 低位
                             //gPeaksAll[tIndex][j][k] = (gPeaksAll[tIndex][j][k] << 8) | (unsigned char)(*(p+i));								
                        temp = (ushort)((temp << 8) | p[i]);

                        int pos = temp * 64 / 50000;

                        if (pos <= iPos)
                        {
                            pos = iPos + 1;
                        }
                        iPos = pos;

                        if (pos >= 0 && pos < 64)
                        {
                            gPeaksAll[tIndex, j, pos] = temp;
                            gPosPeaks[tIndex, j, k] = (byte)pos;
                        }
                    } //end of for
                } //end of if
                iPos = 0;
            } //end of for
            iPos = 0;


            int fbPos = 0;
            int peakNum = gPosPeaks[tIndex, 0, 0];
            if (peakNum >= 10 && gPosPeaks[tIndex, 1, 0] >= 1)
            {
                //求梳状计数值的最大值、最小值
                fbPos = gPosPeaks[tIndex, 0, 1];
                ushort bfMax = gPeaksAll[tIndex, 0, fbPos];
                ushort bfMin = bfMax;
                ushort fbTemp = 0;
                for (int ii = 2; ii <= peakNum; ii++)
                {
                    fbPos = gPosPeaks[tIndex, 0, ii];
                    fbTemp = gPeaksAll[tIndex, 0, fbPos];
                    if (bfMax < fbTemp)
                        bfMax = fbTemp;
                    if (bfMin > fbTemp)
                        bfMin = fbTemp;
                }
                fbPos = gPosPeaks[tIndex, 1, 1];
                ushort bfPeakSig = gPeaksAll[tIndex, 1, fbPos];
                //判断单峰计数值是否在梳状计数值之内，在之内则备份数据
                if (!(bfPeakSig >= bfMax || bfPeakSig <= bfMin))
                {
                    bUpPosPeaks[0] = gPosPeaks[tIndex, 0, 0];
                    for (int ii = 1; ii <= peakNum; ii++)
                    {
                        bUpPosPeaks[ii] = gPosPeaks[tIndex, 0, ii];
                        fbPos = bUpPosPeaks[ii];
                        bUpPeaksAll[fbPos] = gPeaksAll[tIndex, 0, fbPos];
                    }
                    bUpSigPeak[0] = gPosPeaks[tIndex, 1, 0];
                    fbPos = gPosPeaks[tIndex, 1, 1];
                    bUpSigPeak[1] = gPeaksAll[tIndex, 1, fbPos];
                }
                else
                {
                    gPosPeaks[tIndex, 0, 0] = bUpPosPeaks[0];
                    for (int ii = 1; ii <= bUpPosPeaks[0]; ii++)
                    {
                        gPosPeaks[tIndex, 0, ii] = bUpPosPeaks[ii];
                        fbPos = bUpPosPeaks[ii];
                        gPeaksAll[tIndex, 0, fbPos] = bUpPeaksAll[fbPos];
                    }
                    gPosPeaks[tIndex, 1, 0] = (byte)bUpSigPeak[0];
                    gPeaksAll[tIndex, 1, 1] = bUpSigPeak[1];
                }
            }
            //若计数值数据异常，将上一次正常数据拷贝到原数组中
            else
            {
                gPosPeaks[tIndex, 0, 0] = bUpPosPeaks[0];
                for (int ii = 1; ii <= bUpPosPeaks[0]; ii++)
                {
                    gPosPeaks[tIndex, 0, ii] = bUpPosPeaks[ii];
                    fbPos = bUpPosPeaks[ii];
                    gPeaksAll[tIndex, 0, fbPos] = bUpPeaksAll[fbPos];
                }
                gPosPeaks[tIndex, 1, 0] = (byte)bUpSigPeak[0];
                gPeaksAll[tIndex, 1, 1] = bUpSigPeak[1];
            }


            //峰值标定
            PeaksLock(tIndex); //**********************************************************

            //梳妆波长数组存计数值
            int num = gPosPeaks[tIndex, 0, 0];
            for (int ii = 1; ii <= num; ii++)
            {
                int pos = gPosPeaks[tIndex, 0, ii];
                gPeaksAllf[tIndex, 0, pos] = gPeaksAll[tIndex, 0, pos];
            }

            ret = i;
            g_DeviceStatusPara.iSampleSpeedCnt++;

            //补偿(温度)
            /////////////////////////////TempMakeUpProcess(tIndex);

            // 直接标定到UDP发送队列中
            /////////////////////////////UDPDataProcess(tIndex);


            decodeDataToArray(tIndex);//??????????????????????

            gPeaksAll[1000, 0, 0] = (ushort)tIndex; //save the time index
            tIndex++;
            if (tIndex == 1000)
            {
                tIndex = 0;
            }

            return ret;

        }


        public void PeaksLock(int index)
        {
            //C++ TO C# CONVERTER NOTE: This static local variable declaration (not allowed in C#) has been moved just prior to the method:
            //	static ushort oldPeakSingle = 0;
            
            int numPeakch0 = gPosPeaks[index, 0, 0]; //gPeaksAll[index][0][0];
            byte posPeakSingle = gPosPeaks[index, 1, 1];
            int numPeakSingle = gPosPeaks[index, 1, 0];
            ushort curPeakSingle;

            //
            //C++ TO C# CONVERTER NOTE: This static local variable declaration (not allowed in C#) has been moved just prior to the method:
            //	static ushort oldnumCh0 = numPeakch0;
            if(is_oldnumCh0==false)
            {
                oldnumCh0 =(ushort) numPeakch0;
                for(int x=0;x<64;x++)
                {
                    curPeaksRef[x] = 0.0F;
                }
                is_oldnumCh0 = true;
            }

            if (numPeakch0 != oldnumCh0)
            {
                g_DeviceStatusPara.icntRefError++;
                g_DeviceStatusPara.btempTickCnt = false;
                //cntRefError++;
                //tempTickCnt=false;
            }
            oldnumCh0 = (ushort)numPeakch0;

            //
            if (numPeakch0 == 0)
            {
                g_DeviceStatusPara.icntRefNumZeroError++;
                g_DeviceStatusPara.btempTickCnt = false;
                //cntRefNumZeroError++;
                //tempTickCnt=false;
            }

            //判定单峰有无
            if (numPeakSingle < 1)
            {
                curPeakSingle = oldPeakSingle;
                g_DeviceStatusPara.icntSingleError++;
                g_DeviceStatusPara.btempTickCnt = false;
                //cntSingleError++;
                //tempTickCnt=false;
            }
            else if (posPeakSingle < 64)
            {
                curPeakSingle = gPeaksAll[index, 1, posPeakSingle];
                oldPeakSingle = curPeakSingle;

                //g_SinglePosNow  = curPeakSingle;// 记录当前单峰计数值
            }
            else
            {
                curPeakSingle = oldPeakSingle;
            }

            float[] pfPeaksDefined = g_DeviceConfigPara.fPeaksDefined; //梳妆标定数组
            int iDefinedOffsetSingle = g_DeviceConfigPara.iDefinedOffsetSingle; //单峰标定位置

            //C++ TO C# CONVERTER NOTE: This static local variable declaration (not allowed in C#) has been moved just prior to the method:
            //	static float curPeaksRef[64] = {0.0};

            if (curPeakSingle > 0)
            {

                //寻找单峰位置
                int posSingleBetweenCombo = 0;
                for (int i = 1; i <= numPeakch0; i++)
                {
                    int posPeak;
                    posPeak = gPosPeaks[index, 0, i];

                    if (curPeakSingle > gPeaksAll[index, 0, posPeak])
                        posSingleBetweenCombo = i;
                    else
                        break;
                }

                if (posSingleBetweenCombo == numPeakch0)
                {
                    g_DeviceStatusPara.icntPosSingleError++;
                    //cntPosSingleError++;
                }
                //float curPeaksRef[64];


                //重定位梳状峰值
                for (int i = 1; i <= numPeakch0; i++)
                {
                    int indexDefined;
                    indexDefined = iDefinedOffsetSingle - posSingleBetweenCombo + i;
                    if (indexDefined > 0 && indexDefined < g_DeviceConfigPara.fPeaksDefined[0])
                    //if (indexDefined > 0 && indexDefined < pfPeaksDefined[0])
                    {
                        //curPeaksRef[i] = pfPeaksDefined[indexDefined];
                        curPeaksRef[i] = g_DeviceConfigPara.fPeaksDefined[indexDefined];
                    }
                    //			indexDefined = gDefinedOffsetSingle - posSingleBetweenCombo + i;
                    // 			if (indexDefined > 0 && indexDefined < gPeaksDefined[0])
                    // 			{
                    // 				curPeaksRef[i] = gPeaksDefined[indexDefined];
                    // 			}
                }
                //计算单峰
                if (posSingleBetweenCombo < 63)
                {
                    float x;
                    float x2;
                    float x1;
                    float y2;
                    float y1;
                    int posPeak;

                    y2 = curPeaksRef[posSingleBetweenCombo + 1];

                    y1 = curPeaksRef[posSingleBetweenCombo];

                    posPeak = gPosPeaks[index, 0, posSingleBetweenCombo + 1];
                    x2 = gPeaksAll[index, 0, posPeak];

                    posPeak = gPosPeaks[index, 0, posSingleBetweenCombo];
                    x1 = gPeaksAll[index, 0, posPeak];

                    x = curPeakSingle;

                    posPeak = gPosPeaks[index, 1, 1];
                    if (x2 - x1 != 0.0f)
                    {
                        float f = y1 + (x - x1) * (y2 - y1) / (x2 - x1);
                        if (g_DeviceConfigPara.fWLRange == 1300)
                        {
                            if (f < 1200)
                            {
                                f = 1200;
                            }
                            if (f > 1400)
                            {
                                f = 1400;
                            }
                        }
                        else
                        {
                            if (f < 1500)
                            {
                                f = 1500;
                            }
                            if (f > 1600)
                            {
                                f = 1600;
                            }
                        }

                        gPeaksAllf[index, 1, posPeak] = f;
                    }
                }

                //计算四路峰值数据
                for (int ch = 2; ch < 6; ch++)
                    for (int peak = 1; peak <= gPosPeaks[index, ch, 0]; peak++)
                    {
                        int pos = 0;
                        for (int i = 1; i <= numPeakch0; i++)
                        {
                            int posChX;
                            int posCh0;

                            posChX = gPosPeaks[index, ch, peak];
                            posCh0 = gPosPeaks[index, 0, i];
                            if (gPeaksAll[index, ch, posChX] > gPeaksAll[index, 0, posCh0])
                                pos = i;
                            else
                                break;
                        }

                        if (pos < 63)
                        {
                            float x;
                            float x2;
                            float x1;
                            float y2;
                            float y1;
                            int posPeakX2;
                            int posPeakX1;
                            int posPeakX;

                            posPeakX2 = gPosPeaks[index, 0, pos + 1];
                            posPeakX1 = gPosPeaks[index, 0, pos + 0];
                            posPeakX = gPosPeaks[index, ch, peak];

                            y2 = curPeaksRef[pos + 1];
                            y1 = curPeaksRef[pos + 0];
                            x2 = gPeaksAll[index, 0, posPeakX2];
                            x1 = gPeaksAll[index, 0, posPeakX1];
                            x = gPeaksAll[index, ch, posPeakX];

                            float f = 0;
                            if (pos == 0)
                                //f =  y1 - (x1 - x) * (y2 - y1) / (x2 - x1);
                                f = y2 - (x - x2) * 50 / 25000;
                            else if (pos == numPeakch0)
                                //f =  y2 + (x - x1) * (y2 - y1) / (x2 - x1);
                                f = y1 + (x - x1) * 50 / 25000;
                            else
                                f = y1 + (x - x1) * (y2 - y1) / (x2 - x1);
                            if (g_DeviceConfigPara.fWLRange == 1300)
                            {
                                if (f < 1200)
                                {
                                    f = 1200;
                                }
                                if (f > 1400)
                                {
                                    f = 1400;
                                }
                            }
                            else
                            {
                                if (f < 1500)
                                {
                                    f = 1500;
                                }
                                if (f > 1600)
                                {
                                    f = 1600;
                                }
                            }
                            gPeaksAllf[index, ch, posPeakX] = f;

                        } //end of if(pos<63)
                    }
            }
        }
    }
}

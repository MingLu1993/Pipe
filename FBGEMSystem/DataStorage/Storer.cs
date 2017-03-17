using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Windows;
using System.Configuration;
using FBGEMSystem;


namespace FBGEMSystem
{
    class Storer
    {
        //电类数据表名
        public string newTableNamePre = "";
        public string newTableNameTemp = "";
        public string newTableNameVibration = "";

        //FBG数据表名
        public string newTableNameFBG = "";
        //public string newTableName4 = "";
        public string nowTime = "";

        public float[] CH1_Pres = new float[Data.num_Sensor * Data.num_Package];
        public float[] CH2_Temp = new float[Data.num_Sensor * Data.num_Package];
        public float[] CH3_Vibration = new float[Data.num_Sensor * Data.num_Package];

        public float[] CH1_FBG = new float[64*Data.FBG_numPackage];
        public float[] CH2_FBG = new float[64 * Data.FBG_numPackage];
        public float[] CH3_FBG = new float[64 * Data.FBG_numPackage];
        public float[] CH4_FBG = new float[64 * Data.FBG_numPackage];

        //public float[] CH4 = new float[64 * 40];
        public string dateTime = "";
        public string FBGTime = "";
        private SqlConnection conn;
        //更改为电类传感器类型的三张数据表
        public IDataParameter[] parameters = new IDataParameter[Data.type_Sensor];
        public string[] tablename = new string[Data.type_Sensor];

        //  public List<int> CHNum_Ele = new List<int>();
        public int[] CHNum_Ele = new int[Data.type_Sensor];
        public int[] CHNum_FBG = new int[Data.FBG_Channel_Num];

        public List<string> CHType = new List<string>();
        public string connStr = "";

        public Storer()
        {

        }

        public void InitiTb()
        {

            //parameters = CreateTable();
            tablename = CreateTable();
            conn.Close();
            //newTableName1 = "[" + parameters[0].Value.ToString() + "]";
            //newTableName2 = "[" + parameters[1].Value.ToString() + "]";
            //newTableName3 = "[" + parameters[2].Value.ToString() + "]";
            //newTableName4 = "[" + parameters[3].Value.ToString() + "]";
            newTableNamePre = tablename[0];
            newTableNameTemp = tablename[1];
            newTableNameVibration = tablename[2];
            newTableNameFBG = tablename[3];
            //  newTableNameVibrate = tablename[2];
            // newTableName4 = tablename[3];

            nowTime = System.DateTime.Now.Year.ToString() + "-";
            nowTime += System.DateTime.Now.Month.ToString() + "-";
            nowTime += System.DateTime.Now.Day.ToString();
        }


        public void GetConfig()
        {
            //读取配置文件
            Dictionary<string, List<string>> chInfoList = ConfigurationManager.GetSection("CHInfo") as Dictionary<string, List<string>>;
            if (chInfoList != null)
            {
                //List<string> values = new List<string>();
                //CHNum_Ele.Add(int.Parse(chInfoList["CH3"][0]));
                //CHNum_Ele.Add(int.Parse(chInfoList["CH3"][2]));
                //CHNum_Ele.Add(int.Parse(chInfoList["CH3"][4]));

                //CHType.Add(chInfoList["CH3"][1]);
                //CHType.Add(chInfoList["CH3"][3]);
                //CHType.Add(chInfoList["CH3"][5]);
                CHNum_Ele[0] = Data.Chnum1;
                CHNum_Ele[1] = Data.Chnum2;
                CHNum_Ele[2] = Data.Chnum3;

                //Data.Chnum1 = CHNum_Ele[0];
                //Data.Chnum2 = CHNum_Ele[1];
                //Data.Chnum3 = CHNum_Ele[2];  //CHUum_Ele中的值取自于Setting中的索引值

                CHNum_FBG[0] = Data.FBGCH1;
                CHNum_FBG[1] = Data.FBGCH2;
                CHNum_FBG[2] = Data.FBGCH3;
                CHNum_FBG[3] = Data.FBGCH4;
                
                // Data.Chnum4 = CHNum[3];

            }
            connStr = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
        }

        public void Stor()
        {
            int RowsCount_Ele = 0;
            int RowsCount_FBG = 0;
            DataTable dt_Pres = CreateDataTableEle(0);
            DataTable dt_Temp = CreateDataTableEle(1);
            DataTable dt_Vibration = CreateDataTableEle(2);
            //创建FBG内存表
            DataTable dt_FBG = CreateDataTableFBG();
            //DataTable dt4 = CreateDataTable(3);
            Message_EleDecoded msg_Ele = new Message_EleDecoded();
            Message_FBG msg_FBG = new Message_FBG();
            while (true)
            {
                try
                {
                    if(Receiver.sharedLocation_Ele.BufferSize>0)
                    {
                        msg_Ele = Receiver.sharedLocation_Ele.Buffer;

                        CH1_Pres = msg_Ele.CH1_Press;
                        CH2_Temp = msg_Ele.CH2_Temp;
                        CH3_Vibration = msg_Ele.CH3_Vibration;
                        dateTime = msg_Ele.dataTime;

                        RowsCount_Ele = InsertRows_Ele(0, dt_Pres);
                        InsertRows_Ele(1, dt_Temp);
                        InsertRows_Ele(2, dt_Vibration);
                    }

                    if (Receiver.sharedLocation_FBG.BufferSize > 0)
                    {
                        msg_FBG = Receiver.sharedLocation_FBG.Buffer;
                        CH1_FBG = msg_FBG.CH1;
                        CH2_FBG = msg_FBG.CH2;
                        CH3_FBG = msg_FBG.CH3;
                        CH4_FBG = msg_FBG.CH4;
                        FBGTime = msg_FBG.dataTime;
                        RowsCount_FBG = InsertRows_FBG(dt_FBG);
                    }

                    if (RowsCount_Ele == 2000 || (Receiver.sharedLocation_Ele.BufferSize == 0 && dt_Pres != null && dt_Temp != null && dt_Vibration != null))// && dt4 != null
                    {
                        lock (this)
                        {
                            using (conn = new SqlConnection(connStr))
                            {
                                conn.Open();
                                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(conn))
                                {
                                    bulkCopy.BatchSize = 2000;

                                    bulkCopy.DestinationTableName = newTableNamePre;
                                    bulkCopy.WriteToServer(dt_Pres);

                                    bulkCopy.DestinationTableName = newTableNameTemp;
                                    bulkCopy.WriteToServer(dt_Temp);

                                    bulkCopy.DestinationTableName = newTableNameVibration;
                                    bulkCopy.WriteToServer(dt_Vibration);

                                    //bulkCopy.DestinationTableName = newTableName4;
                                    //bulkCopy.WriteToServer(dt4);
                                }
                            }
                        }
                        dt_Pres.Clear();
                        dt_Temp.Clear();
                        dt_Vibration.Clear();
                        RowsCount_Ele = dt_Pres.Rows.Count;
                        //  dt4.Clear();
                    }

                    if (RowsCount_FBG == 2000 || (Receiver.sharedLocation_FBG.BufferSize == 0 && dt_FBG != null))
                    {
                        lock (this)
                        {
                            using (conn = new SqlConnection(connStr))
                            {
                                conn.Open();
                                using (SqlBulkCopy bulkCopy_FBG = new SqlBulkCopy(conn))
                                {
                                    bulkCopy_FBG.BatchSize = 2000;
                                    bulkCopy_FBG.DestinationTableName = newTableNameFBG;
                                    bulkCopy_FBG.WriteToServer(dt_FBG);
                                }
                            }
                        }
                        dt_FBG.Clear();
                        RowsCount_FBG = dt_FBG.Rows.Count;
                    }
                }
                catch (Exception e)
                {
                    //MessageBox.Show(e.ToString());
                }
            }

        }

        #region//创建内存表
        //创建内存表电类
        public DataTable CreateDataTableEle(int k)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(Guid));
            dt.Columns.Add("Time", typeof(string));
            for (int i = 1; i <= CHNum_Ele[k]; i++)
            {
                if(k==0)
                {
                    dt.Columns.Add("Node" + (Data.PressureIndex[i - 1] + 1).ToString(), typeof(float));
                }
               else if (k == 1)
                {
                    dt.Columns.Add("Node" + (Data.TemperatureIndex[i - 1] + 1).ToString(), typeof(float));
                }
                else if (k == 2)
                {
                    dt.Columns.Add("Node" + (Data.VibrationIndex[i - 1] + 1).ToString(), typeof(float));
                }

            }
            return dt;
        }

        //光纤光栅内存表创建
        public DataTable CreateDataTableFBG()
        {
            DataTable FBGdt = new DataTable();
            FBGdt.Columns.Add("ID", typeof(Guid));
            FBGdt.Columns.Add("Time", typeof(string));
            for (int i = 0; i < Data.FBG_Channel_Num; i++)
            {
                if (CHNum_FBG[i] != 0)
                { 
                    for (int j = 0; j < CHNum_FBG[i]; j++)
                     {
                       FBGdt.Columns.Add("Channel" + (i + 1).ToString() + "_" + (j + 1).ToString(), typeof(float));
                     }
                 }
            }
            return FBGdt;

        }
        #endregion

        #region//内存表赋值
        //电类数据内存表赋值
        public int InsertRows_Ele(int k, DataTable dt)
        {
            ///////!!!!!!!!!!!!修改解析方式
            if (CHNum_Ele[k] != 0)
            {
                for (int j = 0; j < Data.num_Package; j++)
                {
                    DataRow row = dt.NewRow();
                    row["Time"] = dateTime;
                    for (int i = 1; i <= CHNum_Ele[k]; i++)
                    {
                        switch (k)
                        {
                            case 0:
                                row["Node" + (Data.PressureIndex[i - 1] + 1).ToString()] = CH1_Pres[i + j * Data.num_Sensor];
                                break;
                            case 1:
                                row["Node" + (Data.TemperatureIndex[i-1] + 1).ToString()] = CH2_Temp[i + j * Data.num_Sensor];
                                break;
                            case 2:
                                row["Node" + (Data.VibrationIndex[i - 1] + 1).ToString()] = CH3_Vibration[i + j * Data.num_Sensor];
                                break;
                                //case 3:
                                //    row["Node" + (i + 1).ToString()] = CH4[i + j * 64];
                                //    break;
                        }
                    }
                    dt.Rows.Add(row.ItemArray);
                }
            }
            int count = dt.Rows.Count;
            return count;
        }

        //光纤光栅内存表赋值
        public int InsertRows_FBG(DataTable FBGdt)
        {

            DataRow row_FBG = FBGdt.NewRow();
            row_FBG["Time"] = FBGTime;

            for(int k=0;k<Data.FBG_numPackage;k++)
            {
                for (int i = 0; i < Data.FBG_Channel_Num; i++)
                {
                    switch (i)
                    {
                        case 0:
                            for (int j = 0; j < CHNum_FBG[i]; j++)
                            {
                                row_FBG["Channel" + (i + 1).ToString() + "_" + (j + 1).ToString()] = CH1_FBG[j + k * 64];
                            }
                            break;
                        case 1:
                            for (int j = 0; j < CHNum_FBG[i]; j++)
                            {
                                row_FBG["Channel" + (i + 1).ToString() + "_" + (j + 1).ToString()] = CH2_FBG[j + k * 64];
                            }
                            break;
                        case 2:
                            for (int j = 0; j < CHNum_FBG[i]; j++)
                            {
                                row_FBG["Channel" + (i + 1).ToString() + "_" + (j + 1).ToString()] = CH3_FBG[j + k * 64];
                            }
                            break;
                        case 3:
                            for (int j = 0; j < CHNum_FBG[i]; j++)
                            {
                                row_FBG["Channel" + (i + 1).ToString() + "_" + (j + 1).ToString()] = CH4_FBG[j + k * 64];
                            }
                            break;
                        default:break;
                    }
                }
                FBGdt.Rows.Add(row_FBG.ItemArray);
            }
            int count_FBG = FBGdt.Rows.Count;
            return count_FBG;
        }
        #endregion


        //public IDataParameter[] CreateTable()
        public string[] CreateTable()
        {
            using (conn = new SqlConnection(connStr))
            {
                //SqlCommand cmd = new SqlCommand();
                //cmd.Connection = conn;
                //cmd.CommandText = "Stored_Procedure_CreateTable";    //存储过程
                //cmd.CommandType = CommandType.StoredProcedure;
                //SqlParameter param1 = new SqlParameter("@newTableName1", SqlDbType.VarChar, 20);
                //SqlParameter param2 = new SqlParameter("@newTableName2", SqlDbType.VarChar, 20);
                //SqlParameter param3 = new SqlParameter("@newTableName3", SqlDbType.VarChar, 20);
                //SqlParameter param4 = new SqlParameter("@newTableName4", SqlDbType.VarChar, 20);
                //param1.Direction = ParameterDirection.Output;
                //param2.Direction = ParameterDirection.Output;
                //param3.Direction = ParameterDirection.Output;
                //param4.Direction = ParameterDirection.Output;
                //IDataParameter[] parameters = { param1, param2, param3, param4 };
                //cmd.Parameters.AddRange(parameters);

                //根据配置文件每个通道的列数建表
                string sqlStr = "create table ";
                //ID为列名，同样可以改为通过从textbox中获取
                //identity(1,1)是标记递增种子
                //primary key定义主键
      
                string[] AllsqlStr = new string[4];
                string[] tableName = new string[4];
                string timenow = System.DateTime.Now.ToLongTimeString().ToString();
                string[] splitTime = timenow.Split(':');
                string NowTime = DateTime.Now.Year.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Day.ToString()+"_"
                                +splitTime[0]+splitTime[1]+splitTime[2];
                
                for (int i = 0; i < Data.type_Sensor; i++)
                {
                    switch (i)
                    {
                        case 0:
                            tableName[i] = "Pipe" + NowTime + "Pressure";
                            break;
                        case 1:
                            tableName[i] = "Pipe" + NowTime + "Temperature";
                            break;
                        case 2:
                            tableName[i] = "Pipe" + NowTime + "Vibration";
                            break;
                        default:
                            break;
                    }
                }

                for (int j = 0; j < Data.type_Sensor; j++)
                {
                    AllsqlStr[j] = sqlStr + tableName[j] + "( " + "ID numeric identity(1,1) primary key,"
                                    + "Time varchar(20),";
                    for (int i = 1; i <= CHNum_Ele[j]; i++)//从data.chnum1取值
                    {
                        if (j == 0)
                        {
                            string colname = "Node" + (Data.PressureIndex[i - 1]+1).ToString();
                            AllsqlStr[j] += colname + " float(10),";
                        }
                        else if (j == 1)
                        {
                            string colname = "Node" + (Data.TemperatureIndex[i - 1]+1).ToString();
                            AllsqlStr[j] += colname + " float(10),";
                        }
                        else if (j == 2)
                        {
                            string colname = "Node" + (Data.VibrationIndex[i - 1] + 1).ToString();
                            AllsqlStr[j] += colname + " float(10),";
                        }
                    }
                    AllsqlStr[j] += ")";
                }

                //for (int j = 0; j < Data.type_Sensor; j++)
                //{
                //    if (j == 0)
                //    {
                //        for (int i = 1; i <= CHNum[j]; i++)//从data.chnum1取值
                //        {
                //            string colname = "["+"Pressure" + "(" + i.ToString() + ")"+"]";
                //            AllsqlStr[0] += colname + " float(10),";
                //        }
                //    }
                //    else if (j == 1)
                //    { 

                //            for (int i = 1; i <= CHNum[j]; i++)//从data.chnum1取值
                //        {
                //            string colname = "["+"Tempration" + "(" + i.ToString() + ")"+"]";
                //            AllsqlStr[0] += colname + " float(10),";
                //        } 
                //    }
                //    else if(j==2)
                //    { 
                //            for (int i = 1; i <= CHNum[j]; i++)//从data.chnum1取值
                //            {
                //                string colname = "["+"Vibration" + "(" + i.ToString() + ")"+"]";
                //                AllsqlStr[0] += colname + " float(10),";
                //            } 
                //    }
                //}
                //AllsqlStr[0] += ")";
               tableName[3]= "Pipe"+ NowTime+ "FBG";
                AllsqlStr[3] = sqlStr + tableName[3] + "( " + "ID numeric identity(1,1) primary key,"
                                 + "Time varchar(20),";

                for (int i=0;i<Data.FBG_Channel_Num;i++)
                 {
                  

                        if (i == 0)
                        {
                            for (int j = 0; j < CHNum_FBG[i]; j++)
                            {
                                string ColName = "["+"Channel1" + "(" + (j+1).ToString() + ")"+"]";
                                AllsqlStr[3] += ColName + "float(10),";
                            }
                        }
                        else if (i == 1)
                        {
                            for (int j = 0; j < CHNum_FBG[i]; j++)
                            {
                                string ColName = "[" + "Channel2" + "(" + (j + 1).ToString() + ")" + "]";
                                AllsqlStr[3] += ColName + "float(10),";
                            }
                        }
                        else if (i == 2)
                        {
                            for (int j = 0; j < CHNum_FBG[i]; j++)
                            {
                                string ColName = "[" + "Channel3" + "(" + (j + 1).ToString() + ")" + "]";
                                AllsqlStr[3] += ColName + "float(10),";
                            }
                        }
                        else
                        {
                            for (int j = 0; j < CHNum_FBG[i]; j++)
                            {
                                string ColName = "[" + "Channel4" + "(" + (j + 1).ToString() + ")" + "]";
                                AllsqlStr[3] += ColName + "float(10),";
                            }

                        }
                    }

                
                AllsqlStr[3] += ")";

                conn.Open();//放在初始化中
                for(int i=0;i<Data.type_Sensor+1;i++)
                {
                    SqlCommand cmd = new SqlCommand(AllsqlStr[i], conn);
                    cmd.ExecuteNonQuery();
                }
                conn .Close();
                //return parameters;
                return tableName;
            }
        }
    }
}

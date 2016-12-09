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

        public string newTableName1 = "";
        public string newTableName2 = "";
        public string newTableName3 = "";
        //public string newTableName4 = "";
        public string nowTime = "";

        public float[] CH1 = new float[Data.num_Sensor* Data.num_Package];
        public float[] CH2 = new float[Data.num_Sensor * Data.num_Package];
        public float[] CH3 = new float[Data.num_Sensor * Data.num_Package];
        //public float[] CH4 = new float[64 * 40];
        public string dateTime = "";

        private SqlConnection conn;
        //更改为电类传感器类型的额三张数据表
        public IDataParameter[] parameters = new IDataParameter[Data.type_Sensor];
        public string[] tablename = new string[Data.type_Sensor];

        public List<int> CHNum = new List<int>();
        public List<string> CHType = new List<string>();
        public string connStr = "";

        public Storer()
        {
        }

        public void InitiTb()
        {
            //parameters = CreateTable();
            tablename = CreateTable();
            //newTableName1 = "[" + parameters[0].Value.ToString() + "]";
            //newTableName2 = "[" + parameters[1].Value.ToString() + "]";
            //newTableName3 = "[" + parameters[2].Value.ToString() + "]";
            //newTableName4 = "[" + parameters[3].Value.ToString() + "]";
            newTableName1 = tablename[0];
            newTableName2 = tablename[1];
            newTableName3 = tablename[2];
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
                List<string> values = new List<string>();
                CHNum.Add(int.Parse(chInfoList["CH3"][0]));
                CHNum.Add(int.Parse(chInfoList["CH3"][2]));
                CHNum.Add(int.Parse(chInfoList["CH3"][4]));
                //CHNum.Add(int.Parse(chInfoList["CH3"][6]));
                CHType.Add(chInfoList["CH3"][1]);
                CHType.Add(chInfoList["CH3"][3]);
                CHType.Add(chInfoList["CH3"][5]);
                //CHType.Add(chInfoList["CH3"][7]);
                Data.Chnum1 = CHNum[0];
                Data.Chnum2 = CHNum[1];
                Data.Chnum3 = CHNum[2];
               // Data.Chnum4 = CHNum[3];

            }
            connStr = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
        }

        public void Stor()
        {
            int RowsCount = 0;
            DataTable dt1 = CreateDataTable(0);
            DataTable dt2 = CreateDataTable(1);
            DataTable dt3 = CreateDataTable(2);
         //   DataTable dt4 = CreateDataTable(3);
            Message msg = new Message();
            while (true)
            {
                try
                {

                    msg = Receiver.sharedLocation.Buffer;
                    CH1 = msg.CH1;
                    CH2 = msg.CH2;
                    CH3 = msg.CH3;
                  //  CH4 = msg.CH4;
                    dateTime = msg.dataTime;

                    ////if (nowTime != dateTime.Substring(0, 9))
                    ////{
                    //    parameters = CreateTable();
                    //    newTableName1 = "[" + parameters[0].Value.ToString() + "]";
                    //    newTableName2 = "[" + parameters[1].Value.ToString() + "]";
                    //    newTableName3 = "[" + parameters[2].Value.ToString() + "]";
                    //    newTableName4 = "[" + parameters[3].Value.ToString() + "]";
                    //    nowTime = dateTime.Substring(0, 9);
                    ////}

                    //插入DataTable 
                    RowsCount = InsertRows(0, dt1);
                    InsertRows(1, dt2);
                    InsertRows(2, dt3);
                  //  InsertRows(3, dt4);


                    if (RowsCount == 80000 || (Receiver.sharedLocation.BufferSize == 0 && dt1 != null && dt2 != null && dt3 != null))// && dt4 != null
                    {
                        lock (this)
                        {
                            using (conn = new SqlConnection(connStr))
                            {
                                conn.Open();
                                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(conn))
                                {
                                    bulkCopy.BatchSize = 80000;
                                    bulkCopy.DestinationTableName = newTableName1;
                                    bulkCopy.WriteToServer(dt1);
                                    bulkCopy.DestinationTableName = newTableName2;
                                    bulkCopy.WriteToServer(dt2);
                                    bulkCopy.DestinationTableName = newTableName3;
                                    bulkCopy.WriteToServer(dt3);
                                    //bulkCopy.DestinationTableName = newTableName4;
                                    //bulkCopy.WriteToServer(dt4);
                                }
                            }
                        }
                        dt1.Clear();
                        dt2.Clear();
                        dt3.Clear();
                      //  dt4.Clear();
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                }
            }

        }

        public DataTable CreateDataTable(int k)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(Guid));
            dt.Columns.Add("Time", typeof(string));
            for (int i = 1; i <= CHNum[k]; i++)
            {
                dt.Columns.Add("Node" + i.ToString(), typeof(float));
            }
            return dt;
        }

        public int InsertRows(int k, DataTable dt)
        {
            ///////!!!!!!!!!!!!修改解析方式
            if (CHNum[k] != 0)
            {
                for (int j = 0; j < Data.num_Package ; j ++)
                {
                    DataRow row = dt.NewRow();
                    row["Time"] = dateTime;
                    for (int i = 0; i < CHNum[k]; i++)
                    {
                        switch (k)
                        {
                            case 0:
                                row["Node" + (i + 1).ToString()] = CH1[i + j * Data.num_Sensor];
                                break;
                            case 1:
                                row["Node" + (i + 1).ToString()] = CH2[i + j * Data.num_Sensor];
                                break;
                            case 2:
                                row["Node" + (i + 1).ToString()] = CH3[i + j * Data.num_Sensor];
                                break;
                            //case 3:
                            //    row["Node" + (i + 1).ToString()] = CH4[i + j * 64];
                            //    break;
                        }
                    }
                    dt.Rows.Add(row);
                }
            }
            int count = dt.Rows.Count;
            return count;
        }

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
                string[] AllsqlStr = new string[Data.type_Sensor];
                string[] tableName = new string[Data.type_Sensor];
                string NowTime = DateTime.Now.Year.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Day.ToString()
                                + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                //string NowTime = DateTime.Now.ToUniversalTime().ToString();
                for (int i = 0; i < Data.type_Sensor;i++ )
                {
                    tableName[i] = "table" + (i + 1).ToString() + NowTime;
                }

                for (int j = 0; j < Data.type_Sensor; j++)
                {
                    AllsqlStr[j] = sqlStr + tableName[j] + "( " + "ID numeric identity(1,1) primary key,"
                                    + "Time varchar(20),";
                    for (int i = 1; i <= CHNum[j]; i++)
                    {
                        string colname = "Node" + i.ToString();
                        AllsqlStr[j] += colname + " float(10),";
                    }
                    AllsqlStr[j] += ")";
                }
  
                conn.Open();
                for(int i=0;i<Data.type_Sensor;i++)
                {
                    SqlCommand cmd = new SqlCommand(AllsqlStr[i], conn);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
                //return parameters;
                return tableName;
            }
        }
    }
}

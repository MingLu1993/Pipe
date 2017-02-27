using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using FBGEMSystem.DataStorage;

namespace FBGEMSystem.DataStorage
{
    class ReadConfig
    {
        public SystemConfig[] p = new SystemConfig[12];

        public SystemConfig [] readConfig(string configName)
        {
            SystemConfig[] p = new SystemConfig[12];
            string sensor = "SensorNo";
            YRangePoint yRange = new YRangePoint(2, -2, "Sensor1");
            SystemConfig systemConfig = new SystemConfig();

            if (!File.Exists(configName))
            {
             //   MessageBox.Show("配置文件不存在，请创建");
            }
            else
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(configName);
                XmlNodeList lis = doc.GetElementsByTagName(sensor);

                foreach (XmlNode xn in lis)
                {
                    XmlNode TotalNum = xn.SelectSingleNode("No");
                   // totalNum = TotalNum.InnerText.ToString();

                }

                //int num = int.Parse(totalNum);
                int num = 12;
                for (int i = 1; i < num + 1; i++)
                {
                    string sensorName = "Sensor" + i.ToString();
                    string ymax;
                    string ymin;
                    double yMax = 0;
                    double yMin = 0;
                    XmlNodeList lis1 = doc.GetElementsByTagName(sensorName);
                    foreach (XmlNode xn in lis1)
                    {
                        XmlNode YMax = xn.SelectSingleNode("YMax");
                        XmlNode YMin = xn.SelectSingleNode("YMin");
                        ymax = YMax.InnerText.ToString();
                        ymin = YMin.InnerText.ToString();
                        yMax = double.Parse(ymax);
                        yMin = double.Parse(ymin);

                    }
                    yRange.setSensorName(sensorName);
                    yRange.setX(yMax);
                    yRange.setY(yMin);
                    systemConfig.setTotalNum(12);
                    systemConfig.setYRange(yRange);
                    p[i - 1] = systemConfig;
                }

            }
            return p;
        }

        public string CreateConfigFile()
        {
            string result = "";

            if (!File.Exists("SystemConfig.xml"))
            {
                try
                {
                    XmlTextWriter writer = new XmlTextWriter("SystemConfig.xml", System.Text.Encoding.UTF8);
                    //使用自动缩进便于阅读
                    writer.Formatting = Formatting.Indented;
                    writer.WriteStartDocument();
                    //书写根元素
                    writer.WriteStartElement("Root");
                    //节点数量
                    writer.WriteStartElement("SensorNo");
                    writer.WriteAttributeString("id", "1");
                    writer.WriteElementString("No", "12");
                    writer.WriteEndElement();
                    //开始一个元素 
                    writer.WriteStartElement("Sensor1");
                    //向先前创建的元素中添加一个属性
                    writer.WriteAttributeString("id", "1");
                    //添加子元素
                    writer.WriteElementString("YMax", "4");
                    writer.WriteElementString("YMin", "-4");
                    writer.WriteEndElement(); // 关闭元素
                    //add node

                    writer.WriteStartElement("Sensor2");
                    writer.WriteAttributeString("id", "2");
                    writer.WriteElementString("YMax", "4");
                    writer.WriteElementString("YMin", "-4");
                    writer.WriteEndElement();
                    writer.WriteStartElement("Sensor3");
                    writer.WriteAttributeString("id", "3");
                    writer.WriteElementString("YMax", "4");
                    writer.WriteElementString("YMin", "-4");
                    writer.WriteEndElement();
                    writer.WriteStartElement("Sensor4");
                    writer.WriteAttributeString("id", "4");
                    writer.WriteElementString("YMax", "4");
                    writer.WriteElementString("YMin", "-4");
                    writer.WriteEndElement();
                    writer.WriteStartElement("Sensor5");
                    writer.WriteAttributeString("id", "5");
                    writer.WriteElementString("YMax", "4");
                    writer.WriteElementString("YMin", "-4");
                    writer.WriteEndElement();
                    writer.WriteStartElement("Sensor6");
                    writer.WriteAttributeString("id", "6");
                    writer.WriteElementString("YMax", "4");
                    writer.WriteElementString("YMin", "-4");
                    writer.WriteEndElement();
                    writer.WriteStartElement("Sensor7");
                    writer.WriteAttributeString("id", "7");
                    writer.WriteElementString("YMax", "4");
                    writer.WriteElementString("YMin", "-4");
                    writer.WriteEndElement();
                    writer.WriteStartElement("Sensor8");
                    writer.WriteAttributeString("id", "8");
                    writer.WriteElementString("YMax", "4");
                    writer.WriteElementString("YMin", "-4");
                    writer.WriteEndElement();
                    writer.WriteStartElement("Sensor9");
                    writer.WriteAttributeString("id", "9");
                    writer.WriteElementString("YMax", "4");
                    writer.WriteElementString("YMin", "-4");
                    writer.WriteEndElement();
                    writer.WriteStartElement("Sensor10");
                    writer.WriteAttributeString("id", "10");
                    writer.WriteElementString("YMax", "4");
                    writer.WriteElementString("YMin", "-4");
                    writer.WriteEndElement();
                    writer.WriteStartElement("Sensor11");
                    writer.WriteAttributeString("id", "11");
                    writer.WriteElementString("YMax", "4");
                    writer.WriteElementString("YMin", "-4");
                    writer.WriteEndElement();
                    writer.WriteStartElement("Sensor12");
                    writer.WriteAttributeString("id", "12");
                    writer.WriteElementString("YMax", "4");
                    writer.WriteElementString("YMin", "-4");
                    writer.WriteEndElement();
                    //关闭item元素
                    writer.WriteEndElement(); // 关闭元素
                    //在节点间添加一些空
                    writer.Close();
                    result = "success";
                }
                catch
                {
                    System.Windows.MessageBox.Show("create config error!");
                }
            }

            else
            {
                result = "wrong";
            }

            return result;

        }

        public string updateConfig(string ymax,string ymin)
        {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load("SystemConfig.xml");
                XmlNodeList nodeList = xmlDoc.SelectSingleNode("Root").ChildNodes;
                foreach (XmlNode xn in nodeList)
                {
                    XmlElement xe = (XmlElement)xn;
                    XmlNodeList nls = xe.ChildNodes;

                    foreach (XmlNode xn1 in nls)
                    {
                        XmlElement xe2 = (XmlElement)xn1;

                        if (xe2.Name == "YMax")
                            xe2.InnerText = ymax;
                        if (xe2.Name == "YMin")
                            xe2.InnerText = ymin;
                    }

                }
                xmlDoc.Save("SystemConfig.xml");
                
                return "success";
            }
                
        

    }
}

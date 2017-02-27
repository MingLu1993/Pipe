using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Xml;

namespace FBGEMSystem
{
    public class MySectionHandler : IConfigurationSectionHandler
    {
        #region IConfigurationSectionHandler Members
        public object Create(object parent, object configContext, XmlNode section)
        {
            List<string> chValuesList = new List<string>();
            Dictionary<string, List<string>> chInfoList = new Dictionary<string, List<string>>();
            int number;
            string nodeType;
            string name;
            foreach (XmlNode childNode in section.ChildNodes)
            {
                if ((childNode.Attributes["Name"] != null && childNode.Attributes["Number"] != null) && (childNode.Attributes["NodeType"] != null))
                {
                    name = childNode.Attributes["Name"].Value;
                    number = int.Parse(childNode.Attributes["Number"].Value);
                    nodeType = childNode.Attributes["NodeType"].Value;
                    if ((!string.IsNullOrEmpty(number.ToString())) && (!string.IsNullOrEmpty(nodeType)))
                    {
                        if (nodeType.Length != number)
                        {
                            System.Windows.MessageBox.Show("通道" + name + "类型个数与通道传感器数目不符！");
                        }
                        else
                        {
                            chValuesList.Add(number.ToString());
                            chValuesList.Add(nodeType);
                            if (!string.IsNullOrEmpty(name))
                            {
                                chInfoList.Add(name, chValuesList);
                            }
                        }
                    }

                }

            }
            return chInfoList;
        }

        #endregion
    }
}
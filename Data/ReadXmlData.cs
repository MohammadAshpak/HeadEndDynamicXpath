using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace HeadEndDynamicXpath.Data
{
    internal class ReadXmlData
    {


        public ReadXmlData() {
        
        
        }


        public  static string getPath(string fileName)
        {

            string filePath= Directory.GetParent(Environment.CurrentDirectory).Parent.Parent + "\\Data\\" + fileName;

            return filePath;

        }

        public static Dictionary<string, string> GetXmlValue(string XmlDocumentName, string PageName=null)
        {
            Dictionary<string, string> Dict = new Dictionary<string, string>();
            XmlDocument doc = new XmlDocument();
            doc.Load(getPath(XmlDocumentName));

            XmlNodeList ChildNode = doc.DocumentElement.ChildNodes;
            for (int j = 0; j < ChildNode.Count; j++)
            {
if(ChildNode[j].Name.Contains("Pages"))
                {
                    XmlNodeList ParentNode = doc.DocumentElement.SelectNodes(ChildNode[j].Name);

                    if (!string.IsNullOrEmpty(PageName))
                    {
                        if (ParentNode[j].Attributes["name"].Value.ToString() == PageName)
                        {
                            foreach (XmlNode node in ParentNode[j])
                            {

                                Console.WriteLine("Name:" + node.LocalName + "Text:" + node.InnerText);
                                Dict.Add(node.LocalName, node.InnerText);
                            }
                        }
                    }
                    else
                    {
                        foreach (XmlNode node in ParentNode[j])
                        {

                            Console.WriteLine("Name:" + node.LocalName + "Text:" + node.InnerText);
                            Dict.Add(node.LocalName, node.InnerText);
                        }
                    }

                }

               
            }


                return Dict;




        }
    }
}

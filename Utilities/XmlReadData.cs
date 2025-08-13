using Automation.SeleniumSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace HeadEndDynamicXpath.Utilities
{
    public class XmlReadData
    {
        public static void delete_xmlfile(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public static string Read_xml(string path, string node1, string element1, string replaceValue, string index = null)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(path);
            XmlNodeList xmlNodeList = ((!string.IsNullOrEmpty(index)) ? xmlDocument.DocumentElement.SelectNodes("(//*[local-name()='" + node1 + "'])[" + index + "]") : xmlDocument.DocumentElement.SelectNodes("//*[local-name()='" + node1 + "']"));
            string text = "";
            foreach (XmlElement item in xmlNodeList)
            {
                XmlAttributeCollection attributes = item.Attributes;
                XmlNode namedItem = attributes.GetNamedItem(element1);
                string value = namedItem.Value;
                string value2 = value.Replace(value, replaceValue);
                namedItem.Value = value2;
            }

            xmlDocument.Save(path);
            return path;
        }

        public static string Read_specificvalue_xml(string path, string node1, string element1)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(path);
            XmlNodeList xmlNodeList = xmlDocument.DocumentElement.SelectNodes("//*[local-name()='" + node1 + "']");
            string result = "";
            foreach (XmlElement item in xmlNodeList)
            {
                XmlAttributeCollection attributes = item.Attributes;
                XmlNode namedItem = attributes.GetNamedItem(element1);
                result = namedItem.Value;
            }

            xmlDocument.Save(path);
            return result;
        }

        public static string Read_specificvalue_xml(string path, string node1, string element1, string index = "1", string subnode = null)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(path);
            XmlNodeList xmlNodeList = ((!string.IsNullOrEmpty(subnode)) ? xmlDocument.DocumentElement.SelectNodes("//*[local-name()='" + node1 + "']//" + subnode + "[" + index + "]") : xmlDocument.DocumentElement.SelectNodes("(//*[local-name()='" + node1 + "'])[" + index + "]"));
            string result = "";
            foreach (XmlElement item in xmlNodeList)
            {
                XmlAttributeCollection attributes = item.Attributes;
                XmlNode namedItem = attributes.GetNamedItem(element1);
                result = namedItem.Value;
            }

            xmlDocument.Save(path);
            return result;
        }

        public static string edit_txt(string path, string esn)
        {
            using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.ReadWrite))
            {
                using StreamWriter streamWriter = new StreamWriter(stream);
                streamWriter.Write(esn);
            }

            return path;
        }

        public static bool Read_xmlattributes(string path, string node1)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(path);
            XmlNodeList xmlNodeList = xmlDocument.SelectNodes("//*");
            if (xmlNodeList.Equals(node1))
            {
                return true;
            }

            return false;
        }

        public static bool Read_xml_multipleattributes(string path, string node1, string attribute1)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(path);
            bool result = false;
            XmlNodeList xmlNodeList = xmlDocument.SelectNodes("//*[local-name()='" + node1 + "']");
            foreach (XmlElement item in xmlNodeList)
            {
                XmlAttributeCollection attributes = item.Attributes;
                foreach (XmlAttribute item2 in attributes)
                {
                    if (item2.Name.Equals(attribute1))
                    {
                        result = true;
                        break;
                    }

                    result = false;
                }
            }

            return result;
        }

        public static string Delete_contents_textfile(string path)
        {
            File.WriteAllText(path, string.Empty);
            return path;
        }

        public static string AddElement(string path, string nameSpace, string elementName, string parentNodeXpath = null, string[] attributes = null, string[] attributeValues = null)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(path);
            XmlNode documentElement = xmlDocument.DocumentElement;
            XmlNode xmlNode = documentElement.SelectSingleNode(parentNodeXpath);
            XmlElement xmlElement = xmlDocument.CreateElement(elementName, nameSpace);
            xmlNode.AppendChild(xmlElement);
            if (attributes != null)
            {
                for (int i = 0; i < attributes.Length; i++)
                {
                    XmlAttribute xmlAttribute = xmlDocument.CreateAttribute(attributes[i]);
                    xmlAttribute.Value = attributeValues[i];
                    xmlElement.Attributes.Append(xmlAttribute);
                }
            }

            xmlDocument.Save(path);
            return path;
        }

        public static string RemoveElement(string path, string nodeXpath, string parentNodeXpath)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(path);
            XmlNode documentElement = xmlDocument.DocumentElement;
            XmlNode oldChild = documentElement.SelectSingleNode(nodeXpath);
            XmlNode xmlNode = documentElement.SelectSingleNode(parentNodeXpath);
            xmlNode.RemoveChild(oldChild);
            xmlDocument.Save(path);
            return path;
        }

        public static bool Read_xml_multipleattributes(string path, string node1, string attribute1, string index = "1")
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(path);
            bool result = false;
            XmlNodeList xmlNodeList = xmlDocument.SelectNodes("(//*[local-name()='" + node1 + "'])[" + index + "]");
            foreach (XmlElement item in xmlNodeList)
            {
                XmlAttributeCollection attributes = item.Attributes;
                foreach (XmlAttribute item2 in attributes)
                {
                    if (item2.Name.Equals(attribute1))
                    {
                        result = true;
                        break;
                    }

                    result = false;
                }
            }

            return result;
        }

        public static string Read_specificvalue_xml(string path, string node1, string element1, string index = "1")
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(path);
            XmlNodeList xmlNodeList = xmlDocument.DocumentElement.SelectNodes("(//*[local-name()='" + node1 + "'])[" + index + "]");
            string result = "";
            foreach (XmlElement item in xmlNodeList)
            {
                XmlAttributeCollection attributes = item.Attributes;
                XmlNode namedItem = attributes.GetNamedItem(element1);
                result = namedItem.Value;
            }

            xmlDocument.Save(path);
            return result;
        }

        public static bool Read_xml_multipleattributes_values(string path, string node1, string attributeVal)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(path);
            bool result = false;
            XmlNodeList xmlNodeList = xmlDocument.SelectNodes("//*[local-name()='" + node1 + "']");
            foreach (XmlElement item in xmlNodeList)
            {
                if (item.HasAttributes)
                {
                    XmlAttributeCollection attributes = item.Attributes;
                    foreach (XmlAttribute item2 in attributes)
                    {
                        if (item2.Value.Equals(attributeVal))
                        {
                            result = true;
                            break;
                        }

                        result = false;
                    }

                    break;
                }

                if (!item.HasChildNodes)
                {
                    continue;
                }

                XmlNodeList childNodes = item.ChildNodes;
                foreach (XmlElement item3 in childNodes)
                {
                    if (item3.InnerText.Equals(attributeVal))
                    {
                        result = true;
                        break;
                    }

                    result = false;
                }
            }

            return result;
        }

        public static string Getcsvfilelocation(string format, string filename)
        {
            string empty = string.Empty;
            string empty2 = string.Empty;
            string environmentVariable = Environment.GetEnvironmentVariable("UserProfile");
            string path = Path.Combine(environmentVariable, "Downloads");
            List<string> list = new List<string>();
            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            FileInfo[] files = directoryInfo.GetFiles("*." + format);
            FileInfo fileInfo = files[0];
            for (int i = 1; i < files.Length; i++)
            {
                if (fileInfo.LastWriteTime < files[i].LastWriteTime)
                {
                    fileInfo = files[i];
                }
            }

            filename = fileInfo.Name;
            return Path.Combine(fileInfo.DirectoryName, fileInfo.Name);
        }

        public static int getRecordsCountInCSV(string csvFileName, string name)
        {
            int num = 0;
            string text = Getcsvfilelocation("csv", name);
            if (csvFileName != null)
            {
                FileInfo fileInfo = new FileInfo(text);
                if (fileInfo.Exists)
                {
                    using StreamReader streamReader = File.OpenText(text);
                    while (streamReader.ReadLine() != null)
                    {
                        num++;
                    }

                    streamReader.Close();
                }

                Automation.SeleniumSupport.Action.ImplicitWait(5000);
            }

            return num;
        }

        public static bool loadCsvFile(string filePath, string value)
        {
            bool flag = false;
            StreamReader streamReader = new StreamReader(File.OpenRead(filePath));
            List<string> list = new List<string>();
            while (!streamReader.EndOfStream)
            {
                string item = streamReader.ReadLine();
                list.Add(item);
            }

            streamReader.Close();
            streamReader.Dispose();
            foreach (string item2 in list)
            {
                if (item2.Contains(value))
                {
                    flag = true;
                    break;
                }
            }

            Assert.IsTrue(flag, "value is not present", false, 3000);
            return flag;
        }

        public static bool CheckFileDownloaded(string filename)
        {
            bool result = false;
            string path = Environment.GetEnvironmentVariable("USERPROFILE") + "\\Downloads";
            string[] files = Directory.GetFiles(path);
            string[] array = files;
            foreach (string text in array)
            {
                if (text.Contains(filename))
                {
                    FileInfo fileInfo = new FileInfo(text);
                    if (fileInfo.LastWriteTime.ToShortTimeString() == DateTime.Now.ToShortTimeString() || fileInfo.LastWriteTime.AddMinutes(1.0).ToShortTimeString() == DateTime.Now.ToShortTimeString() || fileInfo.LastWriteTime.AddMinutes(2.0).ToShortTimeString() == DateTime.Now.ToShortTimeString() || fileInfo.LastWriteTime.AddMinutes(3.0).ToShortTimeString() == DateTime.Now.ToShortTimeString())
                    {
                        result = true;
                        fileInfo.Delete();
                        break;
                    }
                }
            }

            return result;
        }
    }
}

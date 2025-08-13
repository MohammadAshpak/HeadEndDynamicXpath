using AngleSharp.Common;
using HeadEndDynamicXpath.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V117.Debugger;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeadEndDynamicXpath.Pages
{
    internal class AMMDevicePage : CaasBase
    {


        public void RegisterRead()
        {
            List<string> Exp_Reg_List1 = new List<string>();

            IList<IWebElement> reg_list1 = FindElement<IList<IWebElement>>(getLocatorFromResource("ODR_Immediate_List_Key"));


            foreach (IWebElement element in reg_list1)
            {
                Exp_Reg_List1.Add(element.Text);
            }
            HashSet<string> unique_reg_list1 = new HashSet<string>(Exp_Reg_List1);

            Assert.IsTrue(unique_reg_list1.Count.Equals(4), "Both Count should match.");


        }

        public void NewDataRead()
        {


            IList<IWebElement> TableIntervaldata = FindElement<IList<IWebElement>>(getLocatorFromResource("IntervalTimeData"));

            foreach (IWebElement element in TableIntervaldata)
            {
                Console.WriteLine(element.Text);
            }
            int intervalLength = Int32.Parse(TableIntervaldata[2].Text.Split(':')[0].Trim()) - Int32.Parse(TableIntervaldata[1].Text.Split(':')[0].Trim());
            Console.WriteLine("Interval Diff:" + intervalLength);

        }

        public void ConfigurationRead(string RequestType, string ConfigOpt, string status = null, string threshold = null, string period = null, string orientation = null)
        {
            FindElement<IWebElement>(getLocatorFromResource("DeviceOptions", null, getUIFromResource("Configuration")), true).Click();
            FindElement<IWebElement>(getLocatorFromResource("Request_Type", null), true).Click();
            SelectDropDownByText(FindElement<IWebElement>(getLocatorFromResource("Request_Type", null), true), RequestType);
            SelectDropDownByText(FindElement<IWebElement>(getLocatorFromResource("ConfigOptions"),true), ConfigOpt);
            if (RequestType == "Write" && (ConfigOpt == "Reverse Threshold" || ConfigOpt == "Leak Threshold" || ConfigOpt == "Cut Cable Threshold" || ConfigOpt == "High Flow Threshold"))
            {

                if (period != null)
                {
                    FindElement<IWebElement>(getLocatorFromResource("PeriodInput", null, "Period"), true).SendKeys(period);
                    FindElement<IWebElement>(getLocatorFromResource("ThresholdValue"), true).SendKeys(threshold);
                }
                else if (orientation != null)
                {


                    FindElement<IWebElement>(getLocatorFromResource("PeriodInput", null, "Orientation"), true).SendKeys(orientation);
                    FindElement<IWebElement>(getLocatorFromResource("ThresholdValue"), true).SendKeys(threshold);
                }
                else
                {
                    FindElement<IWebElement>(getLocatorFromResource("ThresholdValue"), true).SendKeys(threshold);
                }

            }
            else if (RequestType == "Write" && (ConfigOpt == "Fixed Network Connection Check" || ConfigOpt == "Extended Field Communications"))
            {
                SelectDropDownByText(FindElement<IWebElement>(getLocatorFromResource("Status_Dropdown"), true), status);
            }


        }

        public void DebugGDTTest()
        {


            IList<IWebElement> TableGDTColumn = FindElement<IList<IWebElement>>("//table[@id='odr_results']/tbody/tr/th");
            IList<IWebElement> TableGDTdata = FindElement<IList<IWebElement>>("//table[@id='odr_results']/tbody/tr/td");


            List<string> GDTColumns = new List<string>();
            List<string> GDTData = new List<string>();

            GDTColumns.AddRange(TableGDTColumn.Take(TableGDTColumn.Count).Select(element => element.Text));
            GDTData.AddRange(TableGDTdata.Take(TableGDTdata.Count).Select(element => element.Text));

            string ReadTime = GDTData[GDTColumns.IndexOf("Read Time")].ToString();
            string CumulativeTotal = GDTData[GDTColumns.IndexOf("Cumulative Total")].ToString();

            bool val = ReadTime.Contains("7:00 am");

            // Notes_State.Take(count).Select(d => d.Key).Contains(ProjectName);



        }




        public void NavigateToDeviceDeatilsPage(string macID)
        {

            NavigatetoMenu("Devices", "Device Search");
            deviceQuickSearch(macID);


        }


        void dateSelector(string dateType)
        {



        }

        public void RSMOperation(string ValveState)
        {

            FindElement<IWebElement>(getLocatorFromResource("DeviceOptions", null, getUIFromResource("RSM")), true).Click();
            //Please add code to initiate the Read operation

            //This code just verifying existing results
            IList<IWebElement> RSMHeader = FindElement<IList<IWebElement>>(getLocatorFromResource("RSMTableHeader"));
            int index = 1;
            string value;
            foreach (IWebElement element in RSMHeader)
            {
                switch (element.Text)
                {


                    case "Command Status":

                        value = FindElement<IWebElement>(getLocatorFromResource("RSMTableFirstRowValue", null, index.ToString()), true).Text;
                        Console.WriteLine(element.Text + " :" + value);
                        break;
                    case "Run Time":

                        value = FindElement<IWebElement>(getLocatorFromResource("RSMTableFirstRowValue", null, index.ToString()), true).Text;
                        Console.WriteLine(element.Text + " :" + value);
                        break;
                    case "Command Requested":

                        value = FindElement<IWebElement>(getLocatorFromResource("RSMTableFirstRowValue", null, index.ToString()), true).Text;
                        Console.WriteLine(element.Text + " :" + value);
                        break;
                }
                index++;
            }


        }




        public void ReadReport(string startTime, string endTime)
        {
            FindElement<IWebElement>(getLocatorFromResource("DeviceOptions", null, getUIFromResource("ReadReport")), true).Click();

            Convert_DateTime_Values(startTime, "Start Date", true);

            Convert_DateTime_Values(endTime, "End Date", true);


            // TargetTimestamp: 8 / 14 / 2023 5:28pm


            FindElement<IWebElement>(getLocatorFromResource("DevicePageButton", null, "View Report"), true).Click();


            IList<IWebElement> td_list1 = FindElement<IList<IWebElement>>(getLocatorFromResource("ODR_Result_td"));
            List<string> list = new List<string>();
            foreach (IWebElement td in td_list1)
            {
                if (td.Text.Trim().Contains("/"))
                {
                    int index = 0;
                    //int index1 = 0;
                    string val1 = string.Empty;
                    if (td.Text.Trim().Contains("pm"))
                    {
                        index = td.Text.Trim().IndexOf("-");
                        //index1 = td.Text.Trim().IndexOf("EST");
                        if (td.Text.Contains("EST/EDT"))
                            val1 = td.Text.Trim().Substring(index + 1).Trim().Replace("EST/EDT", "").Trim();
                        else
                            val1 = td.Text.Trim().Substring(index + 1).Trim().Replace("PST/PDT", "").Trim();
                        int ind1 = val1.IndexOf(":");
                        int hours = Convert.ToInt32(val1.Substring(0, ind1).Trim());
                        int hours_24 = 0;
                        if (hours == 12)
                        {

                            hours_24 = hours;
                        }
                        else
                        {
                            hours_24 = hours + 12;
                        }
                        //int hours_24 = hours + 12;
                        string mins = val1.Substring(ind1 + 1, 2).Trim();
                        val1 = hours_24.ToString() + ":" + mins;
                    }
                    else if (td.Text.Trim().Contains("12:00 am") || td.Text.Trim().Contains("12:15 am") || td.Text.Trim().Contains("12:30 am") || td.Text.Trim().Contains("12:45 am"))
                    {
                        index = td.Text.Trim().IndexOf("-");
                        //index1 = td.Text.Trim().IndexOf("EST");
                        if (td.Text.Contains("EST/EDT"))
                            val1 = td.Text.Trim().Substring(index + 1).Trim().Replace("EST/EDT", "").Trim();
                        else
                            val1 = td.Text.Trim().Substring(index + 1).Trim().Replace("PST/PDT", "").Trim();
                        int ind1 = val1.IndexOf(":");
                        //int hours = Convert.ToInt32(val1.Substring(0, ind1).Trim());
                        string hours_24 = "00";
                        string mins = val1.Substring(ind1 + 1, 2).Trim();
                        val1 = hours_24 + ":" + mins;
                    }
                    else
                    {
                        index = td.Text.Trim().IndexOf("-");

                        if (td.Text.Contains("EST/EDT"))
                            val1 = td.Text.Trim().Substring(index + 1).Trim().Replace("EST/EDT", "").Trim();
                        else
                            val1 = td.Text.Trim().Substring(index + 1).Trim().Replace("PST/PDT", "").Trim();
                        if (val1.Contains("am"))
                        {
                            val1 = val1.Trim().Replace("am", "").Trim();
                        }
                        else
                        {
                            val1 = val1.Trim().Replace("pm", "").Trim();
                        }
                    }

                    list.Add(val1);
                }
            }

            for (int i = 0; i < list.Count - 1; i++)
            {


                TimeSpan duration1 = TimeSpan.Parse(list[i]);
                TimeSpan duration2 = TimeSpan.Parse(list[i + 1]);
                //int val = Convert.ToInt32(duration2 - duration1);
                TimeSpan span = duration1.Subtract(duration2);
                Console.WriteLine(duration1 + " - " + duration2 + " = " + span.TotalMinutes.ToString());
                if (list[i + 1].ToString().Contains("23:00") || list[i + 1].ToString().Contains("12:00") || list[i].ToString().Contains("00:00"))
                {

                }
                else
                {
                    Assert.IsTrue(span.TotalMinutes.ToString().Equals(GlobalVariables.LP_IntervalLength), "Interval Length does not match.");
                    // Assert.That(GlobalVariables.LP_IntervalLength, Is.EqualTo(span.TotalMinutes.ToString()));

                }

                //Assert.IsTrue(val.Equals(GlobalVariables.LP_IntervalLength), "Interval Length does not match.");
            }


        }
    }
}

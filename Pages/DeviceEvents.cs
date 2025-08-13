using Automation.SeleniumSupport;
using HeadEndDynamicXpath.Utilities;
using OpenQA.Selenium;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Action = Automation.SeleniumSupport.Action;
using static HeadEndDynamicXpath.Utilities.EnumHelper;
using AngleSharp.Dom;
using System.Transactions;
using System.Collections;

namespace HeadEndDynamicXpath.Pages
{
    class DeviceEvents : AMMDevicePage
    {
        public DeviceEvents()
        {
            NavigatetoPortal("AMM");
        }

        public void VerifyEvent()
        {
            NavigateToDeviceDeatilsPage("00:07:81:71:07:27:17:41");
            FindElement<IWebElement>(getLocatorFromResource("EventsTab")).Click();
            waitForElementVisible(By.XPath(getLocatorFromResource("EventsListTable_rows_Date")), driver);
            IList<IWebElement> EventsListTable_rows_Date = FindElement<IList<IWebElement>>(getLocatorFromResource("EventsListTable_rows_Date"));
            foreach (IWebElement Element in EventsListTable_rows_Date)
            {
                ScrollDownUntilElementVisibleAndClick(driver, null, Element);


            }

            IList<IWebElement> EventsListTable_rows = FindElement<IList<IWebElement>>(getLocatorFromResource("EventsListTable_rows"));

            List<String> EventsListTable = new List<String>();
            EventsListTable= EventsListTable_rows.AsQueryable().Select(x => x.Text.Replace("\r\n", " ")).ToList();
            bool flag = false;int i = 0;
            string eventDateTime;
            Dictionary<string, string> EventSeverity = new Dictionary<string, string>();
            string severityText = null;
            foreach (var eventSeverity in Enum.GetValues(typeof(EventSeverity)).Cast<EventSeverity>())
            {
                EventSeverity.Add(eventSeverity.ToString(), eventSeverity.GetDescription());
            }
            foreach (string events in EventsListTable)
            {
                foreach (var EventNames in Enum.GetValues(typeof(EventNames_AMM_Displayed_BPD)).Cast<EventNames_AMM_Displayed_BPD>())
                {

                     severityText = EventSeverity[EventNames.ToString()];


                    if (events.Contains(EventNames.GetDescription()))
                    {
                       

                        eventDateTime = EventsListTable_rows_Date[i].Text;
                        
                        flag = true;
                        break;
                    }
                }
                i++;

            }

            if (flag)
            {
                Console.WriteLine("Search Found");
            }

        }
    }
}

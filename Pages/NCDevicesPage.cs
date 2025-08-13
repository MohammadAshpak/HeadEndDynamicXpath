using HeadEndDynamicXpath.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HeadEndDynamicXpath.Utilities.EnumHelper;

namespace HeadEndDynamicXpath.Pages
{
    public class NCDevicesPage : CaasBase
    {
        public NCDevicesPage()
        {
            FindElement<IWebElement>(getLocatorFromResource("PortalMenu", "CaasBase", getUIFromResource("NC", "CaasBase")), true).Click();
        }

        public void ImportDevice()
        {
            waitForElementVisible(By.XPath(getLocatorFromResource("SideTabName", null, "Dashboard")), driver);

            FindElement<IWebElement>(getLocatorFromResource("SideTabName", null, "Devices"), true).Click();
            FindElement<IWebElement>(getLocatorFromResource("SideTabName", null, "Import"), true).Click();
            waitForElementVisible(By.XPath(getLocatorFromResource("ImportValidation", null, "Electric_Devices_Mac.txt")), driver);
            bool isPresent = IsElementPresent(driver, By.XPath(getLocatorFromResource("ImportValidation", null, "Electric_Devices_Mac.txt")));
            string errorMsg = "";
            if (isPresent)
            {
                FindElement<IList<IWebElement>>(getLocatorFromResource("ImportValidation", null, "Electric_Devices_Mac.txt"), true).FirstOrDefault(a => a.Size.Width > 0).Click();
                errorMsg = FindElement<IWebElement>(getLocatorFromResource("ErrorMsg"), true).Text;

                Console.WriteLine("Error!!!!!!:" + errorMsg);
            }


        }

        public void VerifyCellSignalQuality()
        {

            foreach (var CellSignalQuality in Enum.GetValues(typeof(CellSignalQuality)).Cast<CellSignalQuality>())
            {
                List<string> max_min_avg = new List<string>();
                IList<IWebElement> Cell_Signal_Quality_Value = FindElement<IList<IWebElement>>(getLocatorFromResource("Cell_Signal_Quality_Value", null, CellSignalQuality.GetDescription()), true);


                List<string> Max_Min_Avg = Cell_Signal_Quality_Value.AsQueryable().Select(x => x.Text.Trim()).ToList();
                foreach (string value in Max_Min_Avg)
                {
                    max_min_avg.Add(value.Split(':')[1].Trim());
                }

                bool isExist = max_min_avg.Exists(x => x == "");
                Assert.That(!isExist, CellSignalQuality.GetDescription() + "Contains Null Value");
                int Count = max_min_avg.FindAll(x => int.Parse(x) == 0).Count;
                Assert.That(Count != max_min_avg.Count, Is.True, CellSignalQuality.GetDescription() + "All Values are Zero");
            }
        }
    }
}

using HeadEndDynamicXpath.Utilities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HeadEndDynamicXpath.Pages
{
    internal class AMMDeviceGroup: CaasBase
    {


        public void NavigatetoAMM()
        {
            waitForElementVisible(By.XPath(getLocatorFromResource("PortalMenu", "CaasBase", getUIFromResource("AMM", "CaasBase"))), driver);

            FindElement<IWebElement>(getLocatorFromResource("PortalMenu", "CaasBase", getUIFromResource("AMM", "CaasBase")), true).Click();

        }
        public void NavigatetoDeviceGroup()
        {
            FindElement<IWebElement>(getLocatorFromResource("MainMenu", "CaasBase", getUIFromResource("Devices", "CaasBase")), true).Click();

            FindElement<IWebElement>(getLocatorFromResource("SubMenu", "CaasBase", getUIFromResource("DeviceGroups", "CaasBase")), true).Click();

        }

        public void verifyDeviceGroupExists()
        {
            NavigatetoAMM();
            NavigatetoDeviceGroup();
            string groupName = "Cellular_500s";
            FindElement<IWebElement>(getLocatorFromResource("DataFilter"), true).Click();
            FindElement<IWebElement>(getLocatorFromResource("GroupName"), true).SendKeys(groupName);
            FindElement<IWebElement>(getLocatorFromResource("Submit"), true).Click();

          if(IsElementPresent(driver,By.XPath(getLocatorFromResource("DeviceGroupName",null,groupName)), true))
            {
                Console.WriteLine("Element is Present");

            }
            else
            {
                Console.WriteLine("Element is Not Present.........");

            }


        }
    }
}

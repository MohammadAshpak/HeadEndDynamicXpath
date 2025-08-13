using HeadEndDynamicXpath.Data;
using HeadEndDynamicXpath.PageXpaths;
using HeadEndDynamicXpath.Utilities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HeadEndDynamicXpath.Utilities.EnumHelper;
using Base = HeadEndDynamicXpath.Utilities.Base;

namespace HeadEndDynamicXpath.Pages
{
    internal class CreateConfig:Base
    {
       public void CreateConfiguration()
        {

            string configName = GlobalVariables.testData["ConfigName"];
            string configDesc = GlobalVariables.testData["ConfigDescription"];
            string deviceClass = GlobalVariables.testData["DeviceClass"];

            FindElement<IWebElement>(getLocatorFromResource("Buttons", "", "New"), true).Click();
            FindElement<IWebElement>(getLocatorFromResource("DeviceClassDropDown", "", ""), true).Click();
            FindElement<IWebElement>(getLocatorFromResource("DeviceClassDropDownElement", "", deviceClass), true).Click();
            waitForElementClickable(By.XPath(getLocatorFromResource("Group Details", "", "Name")),driver);
            FindElement<IWebElement>(getLocatorFromResource("Group Details", "", "Name"), true).SendKeys(configName);
            FindElement<IWebElement>(getLocatorFromResource("Group Details", "", "Description"), true).SendKeys(configDesc);

            FindElement<IWebElement>(getLocatorFromResource("ConfigurationParam", "", "Push Schedule"), true).Click();
            FindElement<IWebElement>(getLocatorFromResource("PlusButton", "",""), true).Click();

            FindElement<IWebElement>(getLocatorFromResource("Buttons", "", "Save"), true).Click();



        }


    }
}

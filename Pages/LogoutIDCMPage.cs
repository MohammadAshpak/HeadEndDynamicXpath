using HeadEndDynamicXpath.Utilities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeadEndDynamicXpath.Pages
{
    internal class LogoutIDCMPage:Base
    {

        public void LogoutIDCM()
        {

            waitForElementVisible(By.XPath(getLocatorFromResource("SignoutArrow")),driver);
            FindElement<IWebElement>(getLocatorFromResource("SignoutArrow"), true).Click();
            FindElement<IWebElement>(getLocatorFromResource("SignoutLink"), true).Click();

            stopHeadEnd();
        }
    }
}

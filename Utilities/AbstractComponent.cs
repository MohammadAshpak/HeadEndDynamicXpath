using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Resources;
using AngleSharp.Dom;
using Automation.SeleniumSupport;

namespace HeadEndDynamicXpath.Utilities
{
    public class AbstractComponent

    {


        private IWebDriver driver
        {
            get
            {
                return HeadEndDynamicXpath.Utilities.CaasBase.driver;
            }
        }

        private static int maxretry = 3;

        public void waitForTextElement(IWebElement element, string text, IWebDriver driver)
        {

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(120));

            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TextToBePresentInElement(element, text));

        }
        public void waitForElementVisible(By by, IWebDriver driver)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(240));

            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(by));
        }
        public void waitForElementClickable(By by, IWebDriver driver)
        {

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(120));

            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(by));


        }

        public void waitForTextInElement(By by, IWebDriver driver,string expectedText)
        {

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                .TextToBePresentInElementLocated(by, expectedText));


        }
        public static string GetText(By locator, IWebDriver driver)
        {
            try
            {
                driver.IsElementPresent(locator);
                IWebElement webElement = driver.FindElement(locator);
                return webElement.Text;
            }
            catch (Exception)
            {
                throw;
            }
        }



        public string getProjectPath()
        {


            return (Directory.GetParent(Environment.CurrentDirectory).Parent.Parent).ToString();


        }




        public static Dictionary<string, string> loadXpath(string settingfile)
        {
            var dic = new Dictionary<string, string>();

            if (File.Exists(settingfile))
            {
                var settingdata = File.ReadAllLines(settingfile);
                for (var i = 0; i < settingdata.Length; i++)
                {
                    var setting = settingdata[i];
                    var sidx = setting.IndexOf("=");
                    if (sidx >= 0)
                    {
                        var skey = setting.Substring(0, sidx);
                        var svalue = setting.Substring(sidx + 1);
                        if (!dic.ContainsKey(skey))
                        {
                            dic.Add(skey, svalue);
                        }
                    }
                }
            }

            return dic;
        }
        public static string GetUploadPath(string contentName, string folderName, string namespaceofproject = "")
        {
            string environmentVariable = Environment.GetEnvironmentVariable("TEMP");
            DirectoryInfo directoryInfo = new DirectoryInfo(environmentVariable);
            string[] files = Directory.GetFiles(directoryInfo.ToString());
            foreach (string text in files)
            {
                try
                {
                    if (text.Equals(contentName))
                    {
                        File.Delete(text);
                    }
                }
                catch (Exception)
                {
                }
            }

            CopyResource(namespaceofproject, contentName, folderName, environmentVariable);
            return environmentVariable + "\\" + contentName;
        }
        public static void CopyResource(string namespaceofproject, string resourceName, string resourceFolder, string outfolder)
        {
            Assembly executingAssembly = Assembly.GetExecutingAssembly();
            string text = (string.IsNullOrEmpty(namespaceofproject) ? executingAssembly.GetName().Name : namespaceofproject);
            using Stream stream = executingAssembly.GetManifestResourceStream(text + "." + resourceFolder + "." + resourceName);
            if (stream == null)
            {
                throw new ArgumentException("No such resource", resourceName);
            }

            using FileStream destination = new FileStream(outfolder + "\\" + resourceName, FileMode.Create);
            stream.CopyTo(destination);
        }

        public void scrolltoElement(IWebElement element, IWebDriver driver)
        {

            Actions actions = new Actions(driver);
            actions.MoveToElement(element);
            actions.Perform();

        }
        public bool IsElementPresent(IWebDriver driver, By locator, bool wait = false, int time = 3000)
        {
            if (wait)
            {
                ImplicitWait(time);
            }

            try
            {
                IWebElement webElement = driver.FindElement(locator);
                if (webElement.Displayed)
                {
                    return true;
                }

                return false;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public void JSExecuter(string query)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript(query);
        }

        public void waitForPage(IWebDriver driver)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(120));

            wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));

        }

        public static string[] ReadFile(string path)
        {
            string[] NMacID = File.ReadAllLines(path);
            return NMacID;
        }
        public T FindElement<T>(string xpath, bool wait = false, int waittime = 3000)
        {

            if (wait)
            {

                ImplicitWait(waittime);

            }
            IList<IWebElement> elementList;
            if (typeof(T) == typeof(IWebElement))

                return (T)driver.FindElement(By.XPath(xpath));


            else
            {
                elementList = driver.FindElements(By.XPath(xpath));
                return (T)elementList;
            }
        }


        public static bool SwitchToNewwindow(IWebDriver driver, string newWindowName)
        {
            try
            {
                driver.SwitchTo().Window(newWindowName);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool SwitchToDefaultwindow(IWebDriver driver, string windowName)
        {
            try
            {
                driver.SwitchTo().Window(windowName);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static string getLocatorFromResource(string key, string classname = null, params string[] dynamic)
        {




            StackFrame frame = new StackFrame(1);
            string classnamex = string.IsNullOrEmpty(classname) ? frame.GetMethod().DeclaringType.Name : classname;
            string filepath = "HeadEndDynamicXpath" + "." + "PageXpaths" + "." + classnamex;
            ResourceManager rm = new ResourceManager(filepath,
        Assembly.GetExecutingAssembly());
            String xpath = rm.GetString(key, CultureInfo.CurrentCulture);
            string strWebsite = xpath;
            if (dynamic.Length > 0)
            {
                foreach (string value in dynamic)
                {
                    Regex reg = new Regex("dynamic");
                    strWebsite = reg.Replace(xpath, value, 1);
                }
            }



            return strWebsite;



        }

        public static string getUIFromResource(string key, string classname = null)
        {




            StackFrame frame = new StackFrame(1);
            string classnamex = string.IsNullOrEmpty(classname) ? frame.GetMethod().DeclaringType.Name : classname;
            string filepath = "HeadEndDynamicXpath" + "." + "PageUI" + "." + classnamex + "UI";
            ResourceManager rm = new ResourceManager(filepath,
        Assembly.GetExecutingAssembly());
            String strWebsite = rm.GetString(key, CultureInfo.CurrentCulture);




            return strWebsite;



        }
        public static void SelectDropDownByText(IWebElement Element, string value)
        {
            //IL_004f: Unknown result type (might be due to invalid IL or missing references)
            int num = 0;
            SelectElement selectElement = new SelectElement(Element);
            while (num <= maxretry)
            {
                try
                {
                    selectElement.SelectByText(value);
                }
                catch (Exception)
                {
                    num++;
                    Thread.Sleep(1000);
                    continue;
                }

                break;
            }

            if (num > maxretry)
            {
                throw new Exception("Control Not Found");
            }
        }

        public static void ScrollWindowToTop(IWebDriver driver)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0, -document.body.scrollHeight)");
        }

        public static void ScrollDownUntilElementVisibleAndClick(IWebDriver driver, By locator = null, IWebElement webElement = null)
        {
            ScrollWindowToTop(driver);
            while (true)
            {
                try
                {
                    if (locator != null)
                        webElement = driver.FindElement(locator);

                    webElement.Click();
                    break;


                }
                catch (Exception)
                {
                    IJavaScriptExecutor javaScriptExecutor = (IJavaScriptExecutor)driver;
                    javaScriptExecutor.ExecuteScript("window.scrollBy(0,250)");
                }
            }
        }

        public static void javaScriptScroll(IWebDriver driver, By locator, bool wait = false, int time = 3000)
        {
            if (wait)
            {
                ImplicitWait(time);
            }

            IWebElement webElement = driver.FindElement(locator);
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView();", webElement);
        }

        public static void ImplicitWait(int timeout)
        {
            // driver.Manage().Timeouts().ImplicitWait=TimeSpan.FromSeconds(timeout);
            Thread.Sleep(timeout);
        }

        public void deviceQuickSearch(string value, string searchBy = "MAC Address", bool isNavigateToDeviceDeatilsPage = true)
        {
            FindElement<IWebElement>(getLocatorFromResource("DeviceQuickSearch", "CaasBase"), true).Click();
            IWebElement quickSearchDrop = FindElement<IWebElement>(getLocatorFromResource("QuickSearchParameterDropDown", "CaasBase"), true);
            quickSearchDrop.Click();
            SelectDropDownByText(quickSearchDrop, searchBy);


            FindElement<IWebElement>(getLocatorFromResource("QuickSearchInput", "CaasBase"), true).SendKeys(value);
            FindElement<IWebElement>(getLocatorFromResource("Button", "CaasBase", "submit"), true).Click();
            if (isNavigateToDeviceDeatilsPage)
            {

                FindElement<IWebElement>(getLocatorFromResource("DeviceSearchResult_FirstRow", "CaasBase"), true).Click();

                waitForElementVisible(By.XPath(getLocatorFromResource("DeviceOptions", "AMMDevicePage", getUIFromResource("DeviceDetails", "AMMDevicePage"))), driver);

            }
        }

        public void NavigatetoMenu(string Menu = null, string SubMenu = null)
        {
            if (!string.IsNullOrEmpty(Menu))
                FindElement<IWebElement>(getLocatorFromResource("MainMenu", "CaasBase", Menu), true).Click();
            ImplicitWait(3000);
            if (!string.IsNullOrEmpty(SubMenu))
                FindElement<IWebElement>(getLocatorFromResource("SubMenu", "CaasBase", SubMenu), true).Click();
        }

        public void NavigatetoPortal(string portal)
        {
            FindElement<IWebElement>(getLocatorFromResource("PortalMenu", "CaasBase", getUIFromResource(portal, "CaasBase")), true).Click();
        }

        public static DateTime RoundUpTime(DateTime currdate, TimeSpan timeSpan)
        {
            return new DateTime((currdate.Ticks + timeSpan.Ticks - 1) / timeSpan.Ticks * timeSpan.Ticks, currdate.Kind);
        }

        public void Convert_DateTime_Values(string val, string date_type, bool IsReadReport)
        {
            //8/8/2023 6:03pm
            //TargetTimestamp: 8/14/2023 5:28pm
            string act_val = val.Replace("TargetTimestamp:", "").Trim(); //8/14/2023 5:28pm
            string AM_PM = val.Substring(val.Length - 2).Trim().ToUpper();
            var actionDate = DateTime.Parse(act_val);
            actionDate = RoundUpTime(actionDate, TimeSpan.FromMinutes(5));
            string Day = actionDate.Day.ToString();
            string Month = actionDate.ToString("MMMM", CultureInfo.InvariantCulture);
            string Year = actionDate.Year.ToString();
            string Hour = actionDate.ToString().Split(' ')[1].Split(':')[0]; //5
            string Min = actionDate.ToString("mm");
            //enter values in Datetime picker


            if (IsReadReport)
            {
                ScrollDownUntilElementVisibleAndClick(driver, By.XPath(getLocatorFromResource("StartDate_EndDate_ReadReport", "AbstractComponent", date_type)));

            }
            else
            {
                //Click on Select Date link
                ScrollDownUntilElementVisibleAndClick(driver, By.XPath(getLocatorFromResource("StartDate_EndDate_Link", "AbstractComponent", date_type)));
            }
            //click on clear
            //Action.ScrollDownUntilElementVisibleAndClick(driver, By.XPath(GetLocatorfromResource("Datepicker_ClearLink", "AbstractComponent")));
            javaScriptScroll(driver, By.XPath(getLocatorFromResource("Datepicker_ClearLink", "AbstractComponent")));
            if (date_type.Equals("Start Date"))
            {
                FindElement<IWebElement>(getLocatorFromResource("Datepicker_ClearLink", "AbstractComponent")).Click();
                FindElement<IWebElement>(getLocatorFromResource("Datepicker_YearCell", "AbstractComponent", Year)).Click();
                FindElement<IWebElement>(getLocatorFromResource("Datepicker_Year", "AbstractComponent", Year)).Click();
                FindElement<IWebElement>(getLocatorFromResource("Datepicker_MonthCell", "AbstractComponent")).Click();
                FindElement<IWebElement>(getLocatorFromResource("Datepicker_Month", "AbstractComponent", Month)).Click();
                FindElement<IWebElement>(getLocatorFromResource("Datepicker_Day", "AbstractComponent", Day)).Click();
                SelectDropDownByText(FindElement<IWebElement>(getLocatorFromResource("Datepicker_hourSelect", "AbstractComponent")), Hour);
                if (Min.Equals("0") || Min.Equals("5"))
                {
                    Min = string.Concat(Min, "0");
                    SelectDropDownByText(FindElement<IWebElement>(getLocatorFromResource("Datepicker_MinSelect", "AbstractComponent")), Min);
                }
                else
                {
                    SelectDropDownByText(FindElement<IWebElement>(getLocatorFromResource("Datepicker_MinSelect", "AbstractComponent")), Min);
                }
                SelectDropDownByText(FindElement<IWebElement>(getLocatorFromResource("Datepicker_AMPMSelect", "AbstractComponent")), AM_PM);

                //click on ok
                FindElement<IWebElement>(getLocatorFromResource("Datepicker_OKLink", "AbstractComponent")).Click();
                ImplicitWait(3000);
            }
            else
            {
                FindElement<IWebElement>(getLocatorFromResource("DatePicker_EndDate_Clear", "AbstractComponent")).Click();
                FindElement<IWebElement>(getLocatorFromResource("DatePicker_YearCell_EndDate", "AbstractComponent", Year)).Click();
                FindElement<IWebElement>(getLocatorFromResource("DatePicker_Year_EndDate", "AbstractComponent", Year)).Click();
                FindElement<IWebElement>(getLocatorFromResource("Datepicker_MonthCell_EndDate", "AbstractComponent", Month)).Click();
                FindElement<IWebElement>(getLocatorFromResource("Datepicker_Month_EndDate", "AbstractComponent", Month)).Click();

                FindElement<IWebElement>(getLocatorFromResource("Datepicker_Day_EndDate", "AbstractComponent", Day)).Click();

                SelectDropDownByText(FindElement<IWebElement>(getLocatorFromResource("Datepicker_hourSelect_EndDate", "AbstractComponent")), Hour);

                SelectDropDownByText(FindElement<IWebElement>(getLocatorFromResource("Datepicker_MinSelect_EndDate", "AbstractComponent")), Min);

                SelectDropDownByText(FindElement<IWebElement>(getLocatorFromResource("Datepicker_AMPMSelect_EndDate", "AbstractComponent")), AM_PM);

                //click on ok
                FindElement<IWebElement>(getLocatorFromResource("Datepicker_OKLink_EndDate", "AbstractComponent")).Click();
                ImplicitWait(3000);
            }

        }


        public void Convert_DateTime_Values(string val, string fieldname)
        {
            string req_val = Get_DatePicker_Val(fieldname);
            //8/8/2023 6:03pm
            //TargetTimestamp: 8/14/2023 5:28pm
            string act_val = val.Replace("TargetTimestamp:", "").Trim(); //8/14/2023 5:28pm
            string AM_PM = val.Substring(val.Length - 2).Trim().ToUpper();
            var actionDate = DateTime.Parse(act_val);
            actionDate = RoundUpTime(actionDate, TimeSpan.FromMinutes(5));
            string Day = actionDate.Day.ToString();
            string Month = actionDate.ToString("MMMM", CultureInfo.InvariantCulture);
            string Year = actionDate.Year.ToString();
            string Hour = actionDate.ToString().Split(' ')[1].Split(':')[0]; //5
            string Min = actionDate.ToString("mm");

            //enter values in Datetime picker

            //Click on Select Date link

            if (fieldname.Equals("RSM Start Time"))
            {

                ScrollDownUntilElementVisibleAndClick(driver, By.XPath(getLocatorFromResource("RSM_Datepicker", "AbstractComponent", fieldname.Replace("RSM Start Time", "Start Time") + ":")));
            }
            else
            {
                ScrollDownUntilElementVisibleAndClick(driver, By.XPath(getLocatorFromResource("ExportField_Datepicker", "AbstractComponent", fieldname + ":")));
            }
            // FindElement<IWebElement>("ExportField_Datepicker", true, 3000, "AbstractComponent", fieldname + ":").Click();

            //click on clear

            FindElement<IWebElement>(getLocatorFromResource("ExportField_Datepicker_ClearBtn", "AbstractComponent", req_val)).Click();

            FindElement<IWebElement>(getLocatorFromResource("ExportField_Datepicker_YearCell", "AbstractComponent", req_val, Year)).Click();
            FindElement<IWebElement>(getLocatorFromResource("ExportField_Datepicker_Year", "AbstractComponent", req_val, Year, Year)).Click();

            FindElement<IWebElement>(getLocatorFromResource("ExportField_Datepicker_MonthCell", "AbstractComponent", req_val)).Click();
            FindElement<IWebElement>(getLocatorFromResource("ExportField_Datepicker_Month", "AbstractComponent", req_val, Month)).Click();

            FindElement<IWebElement>(getLocatorFromResource("ExportField_Datepicker_Day", "AbstractComponent", req_val, Day)).Click();

            SelectDropDownByText(FindElement<IWebElement>(getLocatorFromResource("ExportField_Datepicker_Hour", "AbstractComponent", req_val)), Hour);


            SelectDropDownByText(FindElement<IWebElement>(getLocatorFromResource("ExportField_Datepicker_Min", "AbstractComponent", req_val)), Min);
            SelectDropDownByText(FindElement<IWebElement>(getLocatorFromResource("ExportField_Datepicker_AMPM", "AbstractComponent", req_val)), AM_PM);


            //click on ok
            FindElement<IWebElement>(getLocatorFromResource("ExportField_Datepicker_OKBtn", "AbstractComponent", req_val)).Click();
            ImplicitWait(3000);
        }

        public string Get_DatePicker_Val(string FieldName)
        {
            string val = string.Empty;
            switch (FieldName)
            {
                case "Deploy Date":
                    val = "startDate";
                    break;
                case "RSM Start Time":
                    val = "date_DatePicker";
                    break;
                case "Start Time":
                    val = "oneTimeDateRangeStart";
                    break;
                case "End Time":
                    val = "oneTimeDateRangeEnd";
                    break;
                case "End Date":
                    val = "endDate";
                    break;
            }
            return val;
        }





    }
}

using AngleSharp.Dom;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;
using AutoItX3Lib;
using HeadEndDynamicXpath.Data;
using System.Globalization;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Resources;
using System.Threading;
using Microsoft.Win32;
using System.Net;

namespace HeadEndDynamicXpath.Utilities
{
    public class Base:AbstractComponent
    {

       

        public static IWebDriver driver;
        public IWebElement element;
        public string Version, Build;
        dynamic service = null;


        string signInScriptUrl = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent + "\\ashpaksignin.exe";

      

        string newUrl= "https://idcm2.itrontotaltest.com/";





        [SetUp]

        public void loginHeadEnd()
        {
            

            string? browser = ConfigurationManager.AppSettings["browser"];

            bool hasKeys = ConfigurationManager.AppSettings.HasKeys();

            Launch(newUrl);

           

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Manage().Window.Maximize();


         //   driver.Url = newUrl;

           // Process.Start(signInScriptUrl);
            GlobalVariables.testData = ReadXmlData.GetXmlValue("TestData.xml");

           // updateConfigFile(GlobalVariables.testData, get);
            foreach (KeyValuePair<string, string> item in GlobalVariables.testData)
            {
                if (item.Key == "ESN")

                    GlobalVariables.ESN = item.Value;
                else if(item.Key== "JobName")
                    GlobalVariables.JobName = item.Value;
                        

                }

            

            waitForElementVisible(By.XPath(getLocatorFromResource("IDCMLoginInputBox", "", "loginfmt")),driver);

            FindElement<IWebElement>(getLocatorFromResource("IDCMLoginInputBox","","loginfmt"),false,0).SendKeys(GlobalVariables.Username);
            FindElement<IWebElement>(getLocatorFromResource("IDCMLoginButton", "","Next"),true,5000).Click();
            FindElement<IWebElement>(getLocatorFromResource("IDCMLoginInputBox", "", "passwd"), true).SendKeys(GlobalVariables.Password);
            FindElement<IWebElement>(getLocatorFromResource("IDCMLoginButton", "", "Sign in"), true).Click();
            FindElement<IWebElement>(getLocatorFromResource("IDCMLoginButton", "", "No"), true).Click();

            string xpath = getLocatorFromResource("Title");


            waitForElementVisible(By.XPath(xpath), driver);

            FindElement<IWebElement>(xpath, true, 8000);

            //AutoItX3 AutoItX = new AutoItX3();
            //AutoItX.WinWaitActive("", "Chrome Legacy Window", 120);
            //if(AutoItX.WinExists("", "Chrome Legacy Window")>0)
            //{
            //    AutoItX.Send("ipv6lab\\" + "mashpak");
            //    AutoItX.Send("{TAB}");
            //    AutoItX.Send("ItronGenx123");
            //    AutoItX.Send("{ENTER}");

            //}





        }

       


        public void initBrowser(string browserName)
        {

            switch (browserName)
            {
                case "Chrome":
                    new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());

                    driver = new ChromeDriver();
                    break;
                        case "Edge":
                    new WebDriverManager.DriverManager().SetUpDriver(new EdgeConfig());

                    driver = new EdgeDriver();
                    break;


            }
        }

        public void Launch(string url = null, string SelectBrowser = "Chrome")
        {
            string SourceFolder = null;
            if (SelectBrowser.Equals("Chrome"))
            {
                var chromepath = Registry.GetValue(@"HKEY_CLASSES_ROOT\ChromeHTML\shell\open\command", null, null) as string;
                if (chromepath != null)
                {
                    var split = chromepath.Split('\"');
                    chromepath = split.Length >= 2 ? split[1] : null;
                }
                ChromeOptions options = new ChromeOptions();
                options.PageLoadStrategy = PageLoadStrategy.Normal;
                options.AddUserProfilePreference("disable-popup-blocking", true);
                options.AddUserProfilePreference("safebrowsing.enabled", true);
                string val = options.BinaryLocation;
                var service = ChromeDriverService.CreateDefaultService();
                string versionstring = FileVersionInfo.GetVersionInfo(chromepath).ProductVersion;
                versionstring = versionstring.Split('.')[0];
                int version = Convert.ToInt32(versionstring);

                //string SourceFolder = null;
                if (version > 114)
                {

                    driver = new ChromeDriver(service, options);

                    driver.Navigate().GoToUrl(url);
                }
                else if (version <= 114)
                {
                    var CompatibleBrowserVersion = new ChromeConfig().GetMatchingBrowserVersion();
                    var LatestBrowserVersion = new ChromeConfig().GetLatestVersion();
                    if (!(CompatibleBrowserVersion.ToString() == LatestBrowserVersion.ToString()))
                    {
                        SourceFolder = new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig(), CompatibleBrowserVersion);
                        Console.WriteLine(CompatibleBrowserVersion + " which is Compatible chrome driver for chromeBrowser " + version + " is launching");
                    }
                    else
                    {
                        SourceFolder = new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
                        Console.WriteLine(LatestBrowserVersion + " which is a Latest chrome driver for chromeBrowser " + version + " is launching");
                    }
                    driver = new ChromeDriver(SourceFolder.Replace(@"../../chromedriver.exe", ""), options);
                    driver.Navigate().GoToUrl(url);
                }
                else if (version >= 65)
                {

                    driver = new ChromeDriver(service, options);

                    driver.Navigate().GoToUrl(url);

                }
                else if (version >= 65)
                {
                    var CompatibleBrowserVersion = new ChromeConfig().GetMatchingBrowserVersion();
                    var LatestBrowserVersion = new ChromeConfig().GetLatestVersion();
                    if (!(CompatibleBrowserVersion.ToString() == LatestBrowserVersion.ToString()))
                    {
                        SourceFolder = new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig(), CompatibleBrowserVersion);
                        Console.WriteLine(CompatibleBrowserVersion + " which is Compatible chrome driver for chromeBrowser " + version + " is launching");
                    }
                    else
                    {
                        SourceFolder = new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
                      Console.WriteLine(LatestBrowserVersion + " which is a Latest chrome driver for chromeBrowser " + version + " is launching");
                    }
                    driver = new ChromeDriver(SourceFolder.Replace("\\chromedriver.exe", ""), options);
                    driver.Navigate().GoToUrl(url);

                }
                else
                {
                    ChromeOptions chromeOptions = new ChromeOptions();
                    chromeOptions.AddUserProfilePreference("download.prompt_for_download", false);
                    chromeOptions.AddUserProfilePreference("download.directory_upgrade", true);
                    chromeOptions.AddUserProfilePreference("safebrowsing.enabled", true);
                    GetUploadPath("chromedriver.exe", "External");
                    string environmentVariable = Environment.GetEnvironmentVariable("TEMP");
                     driver = new ChromeDriver(environmentVariable, chromeOptions);
                    driver.Navigate().GoToUrl(url);
                    
                }
            }
            else if (SelectBrowser.Equals("Edge"))
            {
             
                string edgeDriver = "msedgedriver.exe";
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;
                var CompatibleBrowserVersion = new EdgeConfig().GetMatchingBrowserVersion();
                SourceFolder = new WebDriverManager.DriverManager().SetUpDriver(new EdgeConfig(), CompatibleBrowserVersion);

                SourceFolder = SourceFolder.Replace("\\" + edgeDriver, " ").Trim();

                service = EdgeDriverService.CreateDefaultService(SourceFolder, @edgeDriver);

                service.UseVerboseLogging = true;
                //service.UseSpecCompliantProtocol = true;
                service.Start();
                /* var caps = new DesiredCapabilities(new Dictionary<string, object>()
                         {
                             { "ms:edgeOptions", new Dictionary<string, object>() {
                                 { "binary", @"C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe" }
                             }}
                         });*/
                //driver = new RemoteWebDriver(service.ServiceUrl, caps, TimeSpan.FromMinutes(5));
                driver.Navigate().GoToUrl(url);
               
            }


        }


        

       

        [TearDown]
        public static void stopHeadEnd()
        {

            driver.Quit();
        }

        

    }
}

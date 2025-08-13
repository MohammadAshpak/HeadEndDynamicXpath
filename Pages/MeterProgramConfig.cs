using HeadEndDynamicXpath.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeadEndDynamicXpath.Pages
{
    internal class MeterProgramConfig : CaasBase
    {

        public void NavigatetoMPC()
        {


            FindElement<IWebElement>(getLocatorFromResource("PortalMenu", "CaasBase", getUIFromResource("MPC", "CaasBase")), true).Click();

        }
        public void AddProgram()
        {
            NavigatetoMPC();

            FindElement<IWebElement>(getLocatorFromResource("MPCTabs", "", getUIFromResource("Programs")), true).Click();
            FindElement<IWebElement>(getLocatorFromResource("MPCTabs", "", getUIFromResource("AddProgram")), true).Click();


        }
        public void NavigatetoProject()
        {
            NavigatetoMPC();
            string project = "Ashpak_Automation_Genx500W_Project";
            FindElement<IWebElement>(getLocatorFromResource("MPCTabs", "", getUIFromResource("Project")), true).Click();
            FindElement<IWebElement>(getLocatorFromResource("ProjectLink", "", project), true).Click();

        }

        public void VerifyDeviceProjectPage()
        {
            List<string> macIdList = new List<string>();
            string path = "C:\\Users\\mashpak\\OneDrive - Itron\\Documents\\ATB-Automation\\Genx_UIQ\\Devices\\500W_Devices_Mac.txt";
            string[] text;

            text = ReadFile(path);

            macIdList.AddRange(text);
            FindElement<IWebElement>(getLocatorFromResource("ProjectLink", "", getUIFromResource("Device")), true).Click();
            IList<IWebElement> devices = FindElement<IList<IWebElement>>(getLocatorFromResource("DevicesMacIdList", ""), true);

            List<string> metermacIdActual = new List<string>();
            foreach (IWebElement d in devices)
            { metermacIdActual.Add(d.Text); }

            macIdList = macIdList.Except(metermacIdActual).ToList();
            if (macIdList.Count > 0)
            {
                Console.WriteLine("Below Devices are not Added ");
                foreach (string m in macIdList)
                    Console.WriteLine(m);
            }



        }

        public void VerifyDeviceStateMPC()
        {
            string parentWindow = driver.CurrentWindowHandle;
           

            FindElement<IWebElement>(getLocatorFromResource("ProjectLink", "", getUIFromResource("Device")), true).Click();
            IList<IWebElement> devices = FindElement<IList<IWebElement>>(getLocatorFromResource("DevicesMacIdList", ""), true);

            List<string> metermacIdActual = new List<string>();
            foreach (IWebElement d in devices)
            { metermacIdActual.Add(d.Text); }

            foreach (var macID in metermacIdActual)
            {
                FindElement<IWebElement>(getLocatorFromResource("DeviceLinkWithMac", "", macID), true).Click();
                ReadOnlyCollection<string> allWindows = driver.WindowHandles;
                foreach (String newWindow in allWindows)
                {
                    if (!parentWindow.Equals(newWindow))
                    {
                        SwitchToNewwindow(driver, newWindow);
                        string OperationalState = FindElement<IWebElement>(getLocatorFromResource("OpState")).Text;

                        Console.WriteLine("Operational State for macId:"+macID+" Is : "+OperationalState);
                        driver.Close();

                    }




                }
                SwitchToDefaultwindow(driver, parentWindow);
            }
        }
    }
}

using NUnit.Framework;
using OpenQA.Selenium;
using HeadEndDynamicXpath.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeadEndDynamicXpath.Pages
{
    class FWUpgrade : CaasBase
    {
        public FWUpgrade()
        {
            FindElement<IWebElement>(getLocatorFromResource("PortalMenu", "CaasBase", getUIFromResource("FWU", "CaasBase")), true).Click();
        }

        public void AddUpgrade()
        {

            NavigatetoMenu("Upgrades");

            waitForElementVisible(By.XPath(getLocatorFromResource("AddUpgradeLink")), driver);
            FindElement<IWebElement>(getLocatorFromResource("AddUpgradeLink"), true).Click();
            bool isDisplayed = FindElement<IWebElement>(getLocatorFromResource("FWU_AuditJob_Input", null, "Upload image chunk size"), true).Displayed;
            isDisplayed = FindElement<IWebElement>(getLocatorFromResource("FWU_AuditJob_Select", null, "Upload image chunk size"), true).Displayed;
            isDisplayed = FindElement<IWebElement>(getLocatorFromResource("FWU_AuditJob_Select", null, "Auto restart upgrade"), true).Displayed;
            isDisplayed = FindElement<IWebElement>(getLocatorFromResource("FWU_AuditJob_Select", null, "Gateway Priority"), true).Displayed;
        }

        public void AuditHistory(string auditName)
        {
            NavigatetoMenu("Audit Jobs");
            ClickAuditNameLink(auditName);
            ScrollDownUntilElementVisibleAndClick(driver, By.XPath(getLocatorFromResource("AuditHistory")));
            waitForElementVisible(By.XPath(getLocatorFromResource("AudithistoryresultTH")), driver);
            ReadOnlyCollection<IWebElement> TableHeader = FindElement<ReadOnlyCollection<IWebElement>>(getLocatorFromResource("AudithistoryresultTH"), true, 1000);
            int index = TableHeader.IndexOf(TableHeader.FirstOrDefault(a => a.Text == "Status"));
            ReadOnlyCollection<IWebElement> resultrow = FindElement<ReadOnlyCollection<IWebElement>>(getLocatorFromResource("FirstRowOfResultpagetable"), true, 1000);
            string status = resultrow[index].Text;
            //Assert.IsTrue(status.Trim().Contains("In progress"));
            bool statusRes = status.Trim().Contains("In progress") || status.Trim().Contains("Complete");
            Assert.That(statusRes, Is.True);


        }

        public void ClickAuditNameLink(string Auditname)
        {

            SelectDropDownByText(FindElement<IWebElement>(getLocatorFromResource("SelectAllInPage"), true, 1000), "500");
            waitForElementVisible(By.XPath(getLocatorFromResource("AuditNameLink", null, Auditname)), driver);
            ScrollDownUntilElementVisibleAndClick(driver, By.XPath(getLocatorFromResource("AuditNameLink", null, Auditname)));
            waitForTextInElement(By.XPath(getLocatorFromResource("AuditJob_BasicInfo_Fields", null, "Name")), driver, Auditname);
            //waitForElementVisible(By.XPath(getLocatorFromResource("AuditJob_BasicInfo_Fields", null, "Name")), driver);
            string UI_auditname = GetText(By.XPath(getLocatorFromResource("AuditJob_BasicInfo_Fields", null, "Name")), driver);
            Assert.That(UI_auditname, Is.EqualTo(Auditname), "Audit " + Auditname + " Is Not Present In Audit Jobs");


        }

        public void ExportUpgradeDevices()
        {
           
            string devicecount = GetText(By.XPath(getLocatorFromResource("ImportDeviceCount", "FWU")), driver);
            string[] count = devicecount.Split();
            string DevCount = count[0];
            int DeviceCounts = Convert.ToInt32(DevCount);
            if (DeviceCounts >= 1)
            {
               
                FindElement<IWebElement>(getLocatorFromResource("ExportDevices"),true).Click();
                Thread.Sleep(15000);
               
                string fileLocation = XmlReadData.Getcsvfilelocation("csv", "Upgrade");
                string FileName = System.IO.Path.GetFileName(fileLocation);
                int recordCount = XmlReadData.getRecordsCountInCSV(FileName, FileName);
                int ExactRecordCount = recordCount - 2;
                Assert.That(ExactRecordCount, Is.EqualTo(DeviceCounts));
                
            }
            else
            {
                Assert.Fail("Upgrade Device List not Exported since no devices are present in table.");
               
            }
        }
    }
}

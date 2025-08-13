using HeadEndDynamicXpath.Data;
using HeadEndDynamicXpath.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeadEndDynamicXpath.Pages
{
    internal class EditConfigGroup : Base
    {


        public void EditConfigurationGroup()
        {
            string configGroupName = "GenX 500W";
            string section = "Push Schedule";
            string value = "1:00 pm";
            bool select = false;
            string fieldName = "Times to Push";

            FindElement<IWebElement>(getLocatorFromResource("ConfigGroupList", "", configGroupName), true).Click();

            waitForElementVisible(By.XPath(getLocatorFromResource("ConfigPageTitle", "", "")), driver);
            FindElement<IWebElement>(getLocatorFromResource("Buttons", "CreateConfig", "Edit"), true).Click();
            ImplicitWait(5000);

            SelectConfigParameterTab(section);

            switch (fieldName)
            {
                case "Time Drift Lower Threshold":
                case "Time Drift Upper Threshold":
                case "Amount of Data to Push":
                case "Push Window":
                    editSectionTextInput(fieldName, value);
                    break;
     
                case "Power Save Mode":
                case "Enable Data Push":
                case "Collect ERT Data":
                case "Cellular Performance":
                case "Valve Data":
                
                    EditCheckBoxInConfigGroup(fieldName, select, false);
                    break;
                case "Endpoint - Low Battery":
                case "Critical Error":
                case "Flash Data Error":
                case "Entering Network Mode":
                case "Battery Parameters Set":
                case "Manual Time Sync":
                case "Time Outside Automatic Sync Threshold":
                case "Time Outside Automatic Sync Threshold Ended":
                case "Device Reconfigured":
                case "Event Log Cleared":
                case "Configuration Downloaded":
                case "System Reboot":
                case "System Restart":
                case "Improper Installation Detected":
                case "Decryption or Authentication Failure":
                case "Access Control Failed":
                case "RMA Signed Authorization Received":
                case "Register Error":
                case "Entering 100S Mode":
                case "Count Rate Changed":
                case "Rollover Count Changed":
                case "Initial Index":
                case "Load Profile Interval Changed":
                case "Cellular Communications Stopped":
                case "Modem Reset":
                case "No Comms":
                case "Non-Transmit Battery Spike Count":
                case "Cell ID Change Count":
                case "Low Voltage Shutoff Started":
                case "Low Voltage Shutoff Ended":
                case "Low Temperature Shutoff Started":
                case "Low Temperature Shutoff Ended":
                case "Tilt Debounce Parameter Changed":
                case "High Flow Detected":
                case "High Flow Ended":
                case "PCOMP Changed":
                case "Encoder Micro Reset":
                case "Meter Quiet Mode Exit":
                case "Legally Relevant Event Log Nearly Full":
                case "Configuration operation failed Legally Relevant Event Log full":
                case "High Flow Threshold Configuration Changed":
                case "Fixed Network Connection Check Enable Changed":
                case "EDHOC Initiated":
                case "EDHOC Error":
                case "Replay Attack Detected":
                case "Tilt Detected":
                case "Magnetic Tamper Detected":
                case "Image Transfer Initiated":
                case "Image Transfer Canceled":
                case "Image Transfer Cancel Failed":
                case "Image Verification Initiated":
                case "Image Verification Succeeded":
                case "Image Verification Failed Authentication":
                case "Image Activation Initiated":
                case "Image Activation Succeeded":
                case "Image Activation Failed":
                case "Key Update Succeeded":
                case "Key Rollover Succeeded":
                case "Signing Key Update Succeeded":
                case "Takeover Package Accepted":
                case "Takeover Package Rejected":
                case "Valve OK":
                case "Valve No Comms":
                case "Inter Digit":
                case "Invalid Read":
                case "Consumer Leak Detected":
                case "Consumer Leak Ended":
                case "Reverse Flow Detected":
                case "Reverse Flow Ended":
                case "Leak Sensor OK":
                case "Leak Sensor No Comms":
                case "Leak Sensor Detached":
                case "Pulse Mismatch":
                case "Count Sample Debounce Changed":
                case "Count Sample Width Changed":
                case "Count Sample Rate Changed":
                case "Encoder Driver Type Changed":
                case "Prescaler Changed":
                case "Cut Cable Detected":
                case "Cut Cable Ended":
                case "Corrupt Alarm Data Received From Meter":
                case "Service Disconnect Succeeded":
                case "Service Disconnect Failed":
                case "Service Connect Succeeded":
                case "Flow Restriction Setting Changed":
                case "Flow Restriction Setting Failed":
                case "Service Connect Failed":
                case "Badger Meter - Tamper":
                case "Badger Meter - Register Removal":
                case "Badger Meter - Temperature Alarm":
                case "Badger Meter - EndofMeterLife":
                case "Badger Meter - Zero Consumption Detected":
                case "Badger Meter - Reverse Flow Meter":
                case "Badger Meter - Leak Meter Alarm":
                case "Badger Meter - Encoder Programming":
                case "Diehl Meter - Air in Tube Error":
                case "Diehl Meter - Leak Alarm":
                case "Diehl Meter - Checksum Error":
                case "Diehl Meter - Temperature Measurement Error":
                case "Diehl Meter -  Ultrasonic Hardware Error":
                case "Diehl Meter - Power Consumption out of limit Error":
                case "Diehl Meter - Overflow Error":
                case "Diehl Meter - Temperature-Hardware Error":
                case "Diehl Meter - Reverse Flow Alarm":
                case "Diehl Meter - No Usage Alarm":
                case "Diehl Meter - Measurement Alarm":
                case "Diehl Meter - Low Temperature Alarm":
                case "Diehl Meter -  Empty Pipe Alarm":
                case "Diehl Meter - Overflow Alarm":
                case "Diehl Meter - End of Life Alarm":
                case "Diehl Meter - High Temperature Alarm":
                case "Diehl Meter - Fallback Mode":
                case "Diehl Meter - Metrological Log Access":
                case "Intelis Meter - Backflow":
                case "Intelis Meter -  Empty Pipe":
                case "Intelis Meter - Meter Reversed":
                case "Intelis Meter - Low Battery":
                case "Intelis Meter - Leakage":
                case "Intelis Meter - Burst Flow/Broken Pipe":
                case "Intelis Meter - Tamper Alarm":
                case "Intelis Meter - Temperature Alarm":
                case "Kamstrup Meter - Active Reverse Flow":
                case "Kamstrup Meter -  Historic Reverse Flow":
                case "Kamstrup Meter - Active Empty Pipe":
                case "Kamstrup Meter - Historic Empty Pipe":
                case "Kamstrup Meter -  Active Burst":
                case "Kamstrup Meter - Historic Burst":
                case "Kamstrup Meter - Active Leak Alarm":
                case "Kamstrup Meter - Encoder Programming":
                case "Kamstrup Meter - Historic Leak Alarm":
                case "Kamstrup Meter -  Low Temperature Alarm":
                case "Kamstrup Meter - High Temperature Alarm":
                case "Kamstrup Meter - Zero Consumption Detected":
                    EventSelectionInLoggingTab(fieldName,select,true);
                    break;
                case "Wake-up Cycle":
                case "Paging Window":
                case "Time Zone":
                    editSectionDropdown(fieldName, value);
                    break;
                case "Times to Push":

                    SelectTimestoPushValue(value);
                    break;

                default: break;
            }
            ImplicitWait(5000);


        }

        public void SelectConfigParameterTab(string section)
        {
            IWebElement sectionElement = FindElement<IWebElement>(getLocatorFromResource("ConfigurationParam", "", "Logging"), true);
            scrolltoElement(sectionElement, driver);
            FindElement<IWebElement>(getLocatorFromResource("ConfigurationParam", "", section), true).Click();


        }

        public void editSectionTextInput(string fieldName, string value)
        {

            FindElement<IWebElement>(getLocatorFromResource("InputFields", "", fieldName), true).SendKeys(value);
            Validate_TextBoxvalues_entered(fieldName, value);
        }



        public void editSectionDropdown(string fieldName, string value)
        {
            FindElement<IWebElement>(getLocatorFromResource("DropDown", "", fieldName), true).Click();
            IList<IWebElement> timeZone = FindElement<IList<IWebElement>>("//ul[@aria-hidden='false']/li", true);

            foreach(IWebElement timeZoneElement in timeZone)
            {

                Console.WriteLine("Timezones:" + timeZoneElement.Text + "Text:");
            }
            FindElement<IWebElement>(getLocatorFromResource("Options", "", value), true).Click();

        }

        public void SelectTimestoPushValue(string value, string fieldName = null)
        {

            DateTime.TryParse(value.ToString(), out DateTime dateTimeValue);
            string timeOfDay = dateTimeValue.ToString("hh:mm tt");
            string shortTimeConverted = dateTimeValue.ToShortTimeString();  


            EditCheckBoxInConfigGroup("Enable Data Push", true, false);
           
            Validate_TimestoPush( fieldName, shortTimeConverted);
           
            IWebElement ele = FindElement<IWebElement>(getLocatorFromResource("TimesToPush", "", ""), true);



           // JSExecuter("document.getElementById('timeStr').value = '" + timeOfDayValue + "'");

            ele.SendKeys(timeOfDay);
            
      
              
          
            FindElement<IWebElement>(getLocatorFromResource("PlusButton", "", ""), true).Click();

        }
        public void EditCheckBoxInConfigGroup(string fieldName, bool select, bool ismain = false)
        {

            IWebElement? checkbox;
            if (ismain)
            {
                checkbox = FindElement<IList<IWebElement>>(getLocatorFromResource("GenericCheckBox", "", fieldName), true).FirstOrDefault(a => a.Size.Width > 0);
            }
            else
            {
                checkbox = FindElement<IList<IWebElement>>(getLocatorFromResource("GenericCheckBox1", "", fieldName), true).FirstOrDefault(a => a.Size.Width > 0);
            }
            //IWebElement checkbox = PageElement<IWebElement>("GenericCheckBox", true, 3000, fieldName);

            if (checkbox.Selected && !select || !checkbox.Selected && select)
            {
                checkbox.Click();
            }



        }
        public void EventSelectionInLoggingTab(string eventName, bool select, bool eventOrAlarm)
        {
           
            IWebElement? checkbox;
            if (eventOrAlarm)
            {
                checkbox = FindElement<IList<IWebElement>>(getLocatorFromResource("event_chkbx","", eventName), true).FirstOrDefault(a => a.Size.Width > 0);
            }
            else
            {
                checkbox = FindElement<IList<IWebElement>>(getLocatorFromResource("alarm_chkbx", "", eventName), true, 3000).FirstOrDefault(a => a.Size.Width > 0);
            }
            if (checkbox.Selected && !select || !checkbox.Selected && select)
            {
                checkbox.Click();
            }
            
        }
        public void Validate_TextBoxvalues_entered(string fieldName, string value)
        {
            int req_val = Convert.ToInt32(value);
            switch (fieldName)
            {
                case "Time Drift Lower Threshold":
                    Assert.IsTrue((req_val >= -600) || (req_val <= 0), req_val + " is not correct.");
                    break;
                case "Time Drift Upper Threshold":
                    Assert.IsTrue((req_val >= 0) || (req_val <= 0), req_val + " is not correct.");
                    break;
                case "Amount of Data to Push":
                    Assert.IsTrue((req_val >= 1) || (req_val <= 24), req_val + " is not correct.");
                    break;
                case "Push Window":
                    Assert.IsTrue((req_val >= 1) || (req_val <= 480), req_val + " is not correct.");
                    break;

            }
        }
        public void Validate_TimestoPush(string fieldName, string value)
        {

            DateTime.TryParse(value.ToString(), out DateTime dateTime);
            string timeOfDay = dateTime.ToShortTimeString();

            value= timeOfDay.ToString();
            IList<IWebElement> pushSchedules = FindElement<IList<IWebElement>>(getLocatorFromResource("TimesToPushElement", "", ""), true);

            foreach (IWebElement element in pushSchedules)


                if (element.Text.Equals(value))
                {

                Assert.Pass(value+" Is already Present");
                }

        }


    }

}



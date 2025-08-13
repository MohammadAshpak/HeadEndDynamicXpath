using HeadEndDynamicXpath.Pages;
using HeadEndDynamicXpath.Utilities;
using NUnit.Framework;
using System.ComponentModel.Design;
using System.Security.Policy;

namespace HeadEndDynamicXpath
{
    public class Tests : Base
    {


        [Test]

        public void CreateConfigurationGroup()
        {

            CreateConfig config = new CreateConfig();
            config.CreateConfiguration();


        }
        [Test]

        public void EditConfigurationGroup()
        {

            EditConfigGroup editConfig = new EditConfigGroup();
            editConfig.EditConfigurationGroup();


        }


        [Test]

        public void LogoutIDCM()
        {

            LogoutIDCMPage logoutIDCM = new LogoutIDCMPage();
            logoutIDCM.LogoutIDCM();


        }

    }

    public class FirmwareTests :CaasBase
    {

      

        [Test]
        public void FWUAddUpgrade()
        {
            FWUpgrade fWUpgrade = new FWUpgrade();


            fWUpgrade.AddUpgrade();
        }


        [Test]
        public void FWUAuditHistory()
        {
            FWUpgrade fWUpgrade = new FWUpgrade();

            fWUpgrade.AuditHistory("ATB_3_5_0_0_Audit_PI9_R1");
        }

    }

    public class CaasTest : CaasBase
    {

        [Test]

        public void MpcAddProgram()
        {
            MeterProgramConfig meterProgram = new MeterProgramConfig();

            meterProgram.AddProgram();



        }
        [Test]
        public void AMMAddDeviceGroup()
        {
            AMMDeviceGroup amm = new AMMDeviceGroup();

            amm.verifyDeviceGroupExists();



        }

        [Test]
        public void VerifyDevicesMacProject()
        {
            MeterProgramConfig meterProgram = new MeterProgramConfig();

            meterProgram.NavigatetoProject();
            meterProgram.VerifyDeviceProjectPage();
            meterProgram.VerifyDeviceStateMPC();



        }

        [Test]
        public void OndemandReadRegisterRead()
        {
            AMMDevicePage aMMDevicePage = new AMMDevicePage();
            aMMDevicePage.RegisterRead();


        }
        [Test]
        public void OndemandConfigurationRead()
        {
            AMMDevicePage aMMDevicePage = new AMMDevicePage();
            NavigatetoPortal("AMM");
            aMMDevicePage.NavigateToDeviceDeatilsPage("00:07:81:43:05:fe:94:1a");
            aMMDevicePage.ConfigurationRead("Write", "High Flow Threshold", null, "10");
            aMMDevicePage.ConfigurationRead("Write", "Fixed Network Connection Check", "Enable[1]");
            aMMDevicePage.ConfigurationRead("Write", "Extended Field Communications", "Disable[0]");
            aMMDevicePage.ConfigurationRead("Read", "High Flow Threshold");


        }
        [Test]
        public void AmmDeviceEvents()
        {


            DeviceEvents deviceEvents = new DeviceEvents();
            deviceEvents.VerifyEvent();


        }

        [Test]
        public void OndemandNDRRead()
        {
            AMMDevicePage aMMDevicePage = new AMMDevicePage();
            aMMDevicePage.NewDataRead();


        }
        [Test]
        public void DeviceReadReport()
        {
            AMMDevicePage aMMDevicePage = new AMMDevicePage();

            NavigatetoPortal("AMM");

            aMMDevicePage.NavigateToDeviceDeatilsPage("00:07:81:43:1d:cd:65:34");
            aMMDevicePage.ReadReport("TargetTimestamp: 3 / 13 / 2025 6:30am", "TargetTimestamp: 3 / 14 / 2025 01:55pm");


        }

        [Test]
        public void DeviceRSM()
        {
            AMMDevicePage aMMDevicePage = new AMMDevicePage();

            NavigatetoPortal("AMM");

            aMMDevicePage.NavigateToDeviceDeatilsPage("00:07:81:47:0b:eb:c3:13");
            aMMDevicePage.RSMOperation("Uknown");



        }
        [Test]
        public void NCImport()
        {
            NCDevicesPage nCDevicesPage = new NCDevicesPage();

            nCDevicesPage.ImportDevice();



        }
        [Test]
        public void VerifyCellularData()
        {
            NCDevicesPage nCDevicesPage = new NCDevicesPage();

            nCDevicesPage.VerifyCellSignalQuality();



        }



        [Test]
        public void Debug()
        {
            AMMDevicePage aMMDevicePage = new AMMDevicePage();

            aMMDevicePage.DebugGDTTest();



        }


        [Test]
        public void GetDatabaseData()
        {
            GetDbData getDbData = new GetDbData();

            getDbData.DBMeterDetails("BATTERY");



        }



    }
}
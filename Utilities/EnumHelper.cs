using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HeadEndDynamicXpath.Utilities
{
    public static class EnumHelper
    {


        public enum Config_Parameters
        {
            [Description("General")] General,
            [Description("Profiling")] Profiling,
            [Description("Logging")] Logging,
            [Description("Wake-up Schedule")] WakeupSchedule,
            [Description("Push Schedule")] PushSchedule


        }

        public enum CellSignalQuality
        {
            [Description("Reference signal received power (dBm):")] RSRP,
            [Description("Signal to interference plus noise ratio (dB):")] SINR,
            [Description("Average radio frequency environment score:")] ARFES,
            [Description("Reference signal received quality (dB):")] RSRQ,
            [Description("Current radio frequency environment score:")] CRFES,
            [Description("RSSI (dB):")] RSSI
        }

        public enum EventNames_AMM_Displayed_BPD
        {
            [Description("has started charging from its own internal non-rechargeable battery")] Powered_Internal_NonRechargeableBattery_Ev544,
            [Description("Solar panel short circuit on device")] Low_SolarPanel_Output_Start_Ev545,
            [Description("The device has detected that solar panel short circuit current has exceeded the minimum threshold")] Low_SolarPanel_Output_End_Ev546,
            [Description("has dropped to the value of 13107 mV equal or below the depleted voltage threshold")] Low_Rechargeable_Battery_Voltage_Start_Ev547,
            [Description("has risen to the value of 13107 mV that is above the recovery voltage threshold")] Low_Rechargeable_Battery_Voltage_End_Ev548,

            [Description("has stopped acting as a proxy for child devices")] Terminating_Proxy_Functionality_549,
            [Description("has resumed acting as a proxy for child devices")] Restarting_Proxy_Functionality_550,
            [Description("has failed to communicate with secondary micro")] Comm_Failure_Secondary_Micro_Start_551,
            [Description("has once again successfully communicated with the secondary micro")] Comm_Failure_Secondary_Micro_End_552,
            [Description("The HES configuration has changed in device")] Device_Reconfigured_Ev3000,
            [Description("Itron Event Log on device")] Event_Log_Cleared_Ev3001,
            [Description("DST calendar on device")] DST_Periods_Exhausted_Ev3002,
            [Description("A new HES configuration has been downloaded to device")] Configuration_Downloaded_Ev3003,
            [Description("has rebooted")] System_Reboot_Ev3004,
            [Description("has restarted. Reason code:")] System_Restart_Ev3005,
            [Description("could not adopt configuration")] Improper_Installation_Detected_Ev3006,
            [Description("Disconnect operation completed on device")] Service_Disconnect_Succeeded_Ev3007,
            //Event Name needs to be updated in UIQ
            [Description("Disconnect operation failed on device")] Service_Disconnect_Failed_Ev3008,
            [Description("Arm operation completed")] Arm_Completed_Ev3009,
            [Description("Arm operation failed on device")] Arm_Failed_Ev3010,
            [Description("Connect operation completed on device")] Service_Connect_Succeeded_Ev3011,
            [Description("Trap Notification: Remote Disconnect Activated successfully for meter")] RemoteDisconnectActivated_Ev3011,
            [Description("Trap Notification: Remote Connect Activated successfully for meter")] RemoteConnectActivated_Ev3011,
            //Event Name needs to be updated in UIQ
            [Description("Connect operation failed on device")] Service_Connect_Failed_Ev3012,
            //Event Name needs to be updated in UIQ
            [Description("Successfully changed flow restriction setting on device")] Flow_Restriction_Setting_Changed_Ev3013,
            //Event Name needs to be updated in UIQ
            [Description("Failed to change flow restriction setting on device")] Flow_Restriction_Setting_Failed_Ev3014,
            [Description("Decryption or Authentication failure with error code")] Decryption_or_Authentication_Failure_Ev3015,
            [Description("Access control failure with error code 1 for command SET on device")] Access_Control_Failed_Ev3016,
            [Description("Key rollover command received on device")] Key_Rollover_Succeeded_Ev3017,
            [Description("Certificate signing failure on device")] Signing_Key_Update_Success_Ev3018,
            [Description("Takeover package was presented and accepted by device")] Takeover_Package_Accepted_Ev3019,
            [Description("Takeover package was presented, but rejected by device")] Takeover_Package_Rejected_Ev3020,
            [Description("Replay attack detected on device")] Replay_Attack_Detected_Ev3021,
            [Description("A Signed Authorization issued to \"Itron-RMA\" has been")] RMA_Signed_Authorization_Received_Ev3022,
            [Description("Valve Life Nearing Maximum for device")] Valve_Life_Nearing_Maximum_Ev3024,
            [Description("Commissioning Completed on device")] Commissioning_Completed_Ev3025,
            [Description("Decommissioning Completed on device")] Decommissioning_Completed_Ev3026,
            [Description("An override operation occured on valve attached to device")] Valve_Operation_Override_Ev3027,
            [Description("Auto disconnect initiated on device")] AutoDisconnect_Initiated_Ev3028,
            [Description("Prover mode maximum time changed")] Prover_Mode_Maximum_Time_Configuration_Changed_Ev3031,

        }

        public enum EventSeverity
        {
            [Description("Information")] Powered_Internal_NonRechargeableBattery_Ev544,
            [Description("Warning")] Low_SolarPanel_Output_Start_Ev545,
            [Description("Clear")] Low_SolarPanel_Output_End_Ev546,
            [Description("Warning")] Low_Rechargeable_Battery_Voltage_Start_Ev547,
            [Description("Clear")] Low_Rechargeable_Battery_Voltage_End_Ev548,

        }

        public static string GetDescription(this Enum value)
        {
            string val;
            FieldInfo? field = value.GetType().GetField(value.ToString());

            DescriptionAttribute? attribute
                    = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute))
                        as DescriptionAttribute;
            val = attribute.Description;
            var classname = value.GetType().FullName.Split('+');
            if (classname.Length > 1)
            {
                string resvalue = "";// GetLocatorfromResource(value.ToString(), classname[0].Split('.')[jobtype]);
                val = string.IsNullOrEmpty(resvalue) ? attribute.Description : resvalue;
            }
            return attribute == null ? value.ToString() : val;
        }

    }
}

using HeadEndDynamicXpath.Database;
using HeadEndDynamicXpath.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeadEndDynamicXpath.Pages
{
    public class GetDbData:CaasBase
    {
        public void DBMeterDetails(string tableName)
        {
            Dictionary<string, string> meterDetails = new Dictionary<string, string>(); ;

            Database.Database dbInstance = DatabaseFactory.getInstance().CreateDatabase();
            IDbConnection conn = dbInstance.GetDbConnection();

            string sqlQuery = "SELECT * FROM " + tableName;// WHERE SERIALNUMBER = '" + mtrserialnum + "'";
            //string sqlQuery = "SELECT * FROM VIEWMETERDETAIL";
            IDbCommand command = dbInstance.GetDbCommand(sqlQuery, conn);

            using (var sqlReader = command.ExecuteReader())
            {
                while (sqlReader.Read())
                {
                    meterDetails.Add("Battery_ID", sqlReader["BATTERY_ID"].ToString());
                    meterDetails.Add("Mfg_Name", sqlReader["MFG_NAME"].ToString());
                    // meterDetails.Add("SECTORNAME", sqlReader["SECTORNAME"].ToString());

                    //meterDetails.Add("ESN", sqlReader["ESN"].ToString());
                    //meterDetails.Add("CONFIGGROUP", sqlReader["CONFIGGROUP"].ToString());
                    Console.WriteLine("The DB values are \n\n"+ meterDetails["Battery_ID"] + meterDetails["Mfg_Name"]);
                    // meterDetails.ForEach(Console.WriteLine);
                }
                sqlReader.Close();
                sqlReader.Dispose();
            }
            conn.Close();
            conn.Dispose();
            

        }
    }
}

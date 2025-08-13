using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeadEndDynamicXpath.Database
{
    public sealed class DatabaseFactory
    {
        public static readonly DatabaseFactory databaseFactory = new DatabaseFactory();
        private static string? OracleConnectionString;
        private static string? SQLConnectionString;
        private static string? dbType;
        private DatabaseFactory()
        {
            OracleConnectionString = UIQSettings.Default.OracleConecctionString;
            dbType = UIQSettings.Default.DBType;
        }

        public static DatabaseFactory getInstance()
        {
            return databaseFactory;
        }

        public Database CreateDatabase()
        {
            Database? createdDB = null;

            try
            {
                if (dbType.Equals("Oracle"))
                {
                    createdDB = new OracleDatabase(OracleConnectionString);

                }
                else
                {
                    createdDB = new SqlDatabase(SQLConnectionString);
                }

            }
            catch (SqlException ex)
            {
                throw new Exception("Error instantiating database " + ex.Message);
            }
            return createdDB;
        }
    }
}

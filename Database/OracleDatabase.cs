using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeadEndDynamicXpath.Database
{
    public class OracleDatabase:Database
    {
        string connectionString;
        public OracleDatabase(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IDbCommand GetDbCommand(string commandText, IDbConnection connection)
        {
            OracleCommand command = new OracleCommand();
            command.CommandText = commandText;
            command.Connection = (OracleConnection)connection;
            command.CommandType = CommandType.Text;
            return command;
        }

        public IDbConnection GetDbConnection()
        {
            OracleConnection dbConnection = new OracleConnection(connectionString);
            dbConnection.Open();
            return dbConnection;
        }
    }
}

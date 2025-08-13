using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeadEndDynamicXpath.Database
{
    public class SqlDatabase:Database
    {
        string connectionString;
    public SqlDatabase(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IDbCommand GetDbCommand(string commandText, IDbConnection connection)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = commandText;
            command.Connection = (SqlConnection)connection;
            command.CommandType = CommandType.Text;
            
            return command;
        }

        public IDbConnection GetDbConnection()
        {
            SqlConnection dbConnection = new SqlConnection(connectionString);
            dbConnection.Open();
            return dbConnection;
        }
    }
}

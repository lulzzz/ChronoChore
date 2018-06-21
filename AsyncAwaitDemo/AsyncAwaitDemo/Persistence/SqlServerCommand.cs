using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncAwaitDemo
{
    public class SqlServerCommand : IDisposable
    {
        private string ConnectionString = ConfigurationManager.AppSettings["sqlServerConnection"];
        private SqlConnection sqlConnection;

        public void Dispose()
        {
            if(sqlConnection != null)
                sqlConnection.Close();
            sqlConnection = null;
        }

        public SqlDataReader ExecuteQuery(string cmdText)
        {
            sqlConnection = new SqlConnection(ConnectionString);
            var command = new SqlCommand(cmdText, sqlConnection);
            command.CommandType = CommandType.Text;

            sqlConnection.Open();
            return command.ExecuteReader();
        }

        public void ExecuteNoQuery(string cmdText)
        {
            sqlConnection = new SqlConnection(ConnectionString);
            var command = new SqlCommand(cmdText, sqlConnection);
            command.CommandType = CommandType.Text;

            sqlConnection.Open();
            command.ExecuteNonQuery();
        }
    }
}

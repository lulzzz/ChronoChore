using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSourceAPIData.Persistence.Logic
{
    public class DBContextBase : IDisposable
    {
        private string Topic;
        private string connectionString;
        private IDbConnection connection;

        public DBContextBase(string topic)
        {
            Topic = topic;
            
        }

        public virtual void CreateDatabase(string databaseName) { }

        public virtual void ExecuteNonQuery(string query) { }

        public void Dispose()
        {
            if (connection != null) connection.Close();
            connection = null;
        }
    }
}

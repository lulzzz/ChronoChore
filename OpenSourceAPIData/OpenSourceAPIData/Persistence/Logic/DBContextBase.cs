using System;
using System.Data;

namespace OpenSourceAPIData.Persistence.Logic
{
    public class DBContextBase : IDisposable
    {
        protected string Topic;
        protected string connectionString;
        protected IDbConnection connection;

        public DBContextBase(string topic)
        {
            Topic = topic;
            
        }

        public virtual void Open() { }
        public virtual void Close() { }

        public virtual void CreateDatabase(string databaseName) { }

        public virtual void ExecuteNonQuery(string query) { }

        public void Dispose()
        {
            if (connection != null) connection.Close();
            connection = null;
        }

        public virtual void Insert(string query) { }
    }
}

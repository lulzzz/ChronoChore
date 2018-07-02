using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersistenceManagement
{
    public class DBContextBase : IDisposable
    {
        /// <summary>
        /// The connection string
        /// </summary>
        protected string connectionString;
        
        /// <summary>
        /// Open a connection
        /// </summary>
        public virtual void Open() { }

        /// <summary>
        /// Close
        /// </summary>
        public virtual void Close() { }

        /// <summary>
        /// Create a database
        /// </summary>
        /// <param name="databaseName"></param>
        public virtual void CreateDatabase(string databaseName, string path) { }

        /// <summary>
        /// Execute a DDL query
        /// </summary>
        /// <param name="query"></param>
        public virtual void ExecuteNonQuery(string query) { }

        /// <summary>
        /// Insert data
        /// </summary>
        /// <param name="query"></param>
        public virtual void Insert(string query) { }

        public void Dispose() { }
    }
}

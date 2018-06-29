using System;
using System.Data;

namespace OpenSourceAPIData.Persistence.Logic
{
    /// <summary>
    /// The base class for any data base type context.
    /// It helps to generalize the logic of database actions.
    /// </summary>
    public class DBContextBase : IDisposable
    {
        /// <summary>
        /// Provides the parent directory where the database local file is to be saved.
        /// </summary>
        protected string Topic;

        /// <summary>
        /// The connection string
        /// </summary>
        protected string connectionString;
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="topic"></param>
        public DBContextBase(string topic)
        {
            Topic = topic;
        }

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
        public virtual void CreateDatabase(string databaseName) { }

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

using System;

namespace OpenSourceAPIData.Persistence.Logic
{
    /// <summary>
    /// A store management class which forms basically a wrapper for creation, management and cleanup of the database(s) 
    /// used in this application.
    /// </summary>
    public class PersistenceManager : IDisposable
    {
        /// <summary>
        /// The Root folder
        /// </summary>
        private string Topic;

        /// <summary>
        /// A factory function to create database context
        /// </summary>
        private Func<string, DBContextBase> contextBuilder;

        /// <summary>
        /// The db context object created using the factory functio nmethod
        /// </summary>
        public DBContextBase context;

        /// <summary>
        /// The producer consumer logic behind the execute queue.
        /// regulates the number of queries to perform
        /// </summary>
        private DbExecuteQueue executeQueue;
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="topic"></param>
        /// <param name="contextBuilder"></param>
        public PersistenceManager(string topic, Func<string, DBContextBase> contextBuilder)
        {
            Topic = topic;
            this.contextBuilder = contextBuilder;
            context = contextBuilder(Topic);
            executeQueue = new DbExecuteQueue();
        }

        /// <summary>
        /// Create the store
        /// </summary>
        /// <param name="storeName"></param>
        public void CreateStore(string storeName) => context.CreateDatabase(storeName);

        /// <summary>
        /// Create a table
        /// </summary>
        /// <param name="query"></param>
        public void CreateTable(string query)
            => executeQueue.Enqueue(() => context.ExecuteNonQuery(query));

        /// <summary>
        /// Insert into a table
        /// </summary>
        /// <param name="query"></param>
        public void Insert(string query)
            => executeQueue.Enqueue(() => context.ExecuteNonQuery(query));
        
        public void Dispose()
        {
            Wait();
            if (context != null) context.Close();
        }

        public void Wait()
        {
            executeQueue.MarkCompleted = true;
            executeQueue.Wait();
        }
    }
}

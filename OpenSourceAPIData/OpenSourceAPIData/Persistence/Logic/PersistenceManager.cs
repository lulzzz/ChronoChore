using System;
using System.IO;

namespace OpenSourceAPIData.Persistence.Logic
{
    public class PersistenceManager : IDisposable
    {
        private string Topic;
        private Func<string, DBContextBase> contextBuilder;
        public DBContextBase context;
        private ExecuteSqliteQueriesQueue executeQueue;

        public PersistenceManager(string topic, Func<string, DBContextBase> contextBuilder)
        {
            Topic = topic;
            this.contextBuilder = contextBuilder;
            Directory.CreateDirectory(Topic);
            context = contextBuilder(Topic);
            executeQueue = new ExecuteSqliteQueriesQueue();
            executeQueue.Setup();
        }

        public void CreateStore(string storeName) => context.CreateDatabase(storeName);

        public void CreateTable(string query) => executeQueue.AddTask(() => context.ExecuteNonQuery(query));

        public void Insert(string query) => executeQueue.AddTask(() => context.Insert(query));

        public void Dispose()
        {
            if (context != null) context.Close();
        }

        private void ThreadSafeContextCall(Action actionMain)
        {
            try
            {
                context.Open();
                actionMain();
            }
            finally
            {
                context.Close();
            }
        }
    }
}

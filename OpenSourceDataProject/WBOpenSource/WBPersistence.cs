using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersistenceManagement;
using WBOpenSource.Model;
using System.Threading.Tasks.Dataflow;

namespace WBOpenSource
{
    public class WBPersistence
    {
        /// <summary>
        /// A factory function to create database context
        /// </summary>
        private Func<DBContextBase> contextBuilder;

        DBContextBase dBContext;

        public ActionBlock<string> taskQueue;

        public WBPersistence(Func<DBContextBase> contextBuilder)
        {
            this.contextBuilder = contextBuilder;
            dBContext = contextBuilder();

            taskQueue = new ActionBlock<string>(query => dBContext.ExecuteNonQuery(query));
        }

        internal void Create(string databaseName, string path)
        {
            dBContext.CreateDatabase(databaseName, path);

            taskQueue.Post(TopicsTable.CreateQuery());
        }

        internal void Insert(TopicsTable[] result)
        {
            var query = TopicsTable.InsertQuery(result);

            taskQueue.Post(query);
        }

        internal void Insert(IndicatorsTable[] result)
        {
            var query = IndicatorsTable.InsertQuery(result);

            taskQueue.Post(query);
        }

        internal List<string> GetTopicsIdList()
        {
            var query = TopicsTable.SelectTopicIds();

            var results = dBContext.ExecuteQuery(query);

            var topicsIds = new List<string>();
            for (int i = 0; i < results.Count; i++)
            {
                topicsIds.Add(results[i][0]);
            }

            return topicsIds;
        }

        internal void Wait()
        {
            taskQueue.Completion.Wait();
        }
    }
}

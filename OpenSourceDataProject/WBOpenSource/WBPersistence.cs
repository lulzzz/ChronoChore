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

        protected ActionBlock<string> taskQueue;

        public WBPersistence(Func<DBContextBase> contextBuilder)
        {
            this.contextBuilder = contextBuilder;
            dBContext = contextBuilder();

            taskQueue = new ActionBlock<string>(query => dBContext.ExecuteNonQuery(query));
        }

        internal void Create(string databaseName, string path)
        {
            dBContext.CreateDatabase(databaseName, path);

            taskQueue.SendAsync(TopicsTable.CreateQuery());
        }

        internal void Insert(TopicsTable[] result)
        {
            var query = TopicsTable.InsertQuery(result);

            taskQueue.SendAsync(query);
        }

        internal void Insert(IndicatorsTable[] result)
        {
            var query = IndicatorsTable.InsertQuery(result);

            taskQueue.SendAsync(query);
        }
    }
}

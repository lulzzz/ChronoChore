using log4net;
using OpenSourceAPIData.Persistence.Logic;
using OpenSourceAPIData.WorldBankData.Logic;
using OpenSourceAPIData.WorldBankData.Model;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Common.TaskQueue;

namespace OpenSourceAPIData
{
    class Program
    {
        protected static ILog logger = LogManager.GetLogger(typeof(Program));

        protected static ConcurrentBag<PersistenceManager> persistenceManagerList;
        protected static PersistenceManager persistenceManagerMain;
        protected static ConcurrentBag<Task> programAllTasks;
        protected static ProducerConsumerService tasksConsumerService;
        protected static WorldBankOrgOSDatabase databaseModel;

        static void Main(string[] args)
        {
            logger.Info("Start the Open API application");

            Process().Wait();

            logger.Info("End the Open API application");
        }

        public static async Task Process()
        {
            try
            {
                tasksConsumerService = new ProducerConsumerService(100);
                programAllTasks = new ConcurrentBag<Task>();
                persistenceManagerList = new ConcurrentBag<PersistenceManager>();
                persistenceManagerMain = new PersistenceManager("WorldBank",
                    (topic) => new SqlitePersistContext(topic));

                persistenceManagerList.Add(persistenceManagerMain);

                // Create the database
                databaseModel = new WorldBankOrgOSDatabase();
                databaseModel.Create(persistenceManagerMain);

                var wbTopics = new WBTopicsWebServiceRest(persistenceManagerMain, databaseModel, tasksConsumerService);
                wbTopics.Read();

                tasksConsumerService.Wait();

                //while (programAllTasks.Any(t => !t.IsCompleted))
                //{
                //    await Task.WhenAll(programAllTasks.ToArray());
                //}

                var persistenceTaskList = new List<Task>();
                foreach (var item in persistenceManagerList)
                {
                    persistenceTaskList.Add(Task.Factory.StartNew(() => item.Wait()));
                }

                await Task.WhenAll(persistenceTaskList.ToArray());
            }
            finally
            {

            }
        }

        public static void WBTopicsWebServiceRestCompleted(string uniqueName, ConcurrentBag<TopicsTable> Result)
        {
            logger.Info($"Completed '{uniqueName}' and count {Result.Count}");

            databaseModel.Topics.Save(Result, persistenceManagerMain);
        }

        public static void WBIndicatorsPerTopicWebServiceRestCompleted(string uniqueName, int topicId, ConcurrentBag<IndicatorsTable> Result)
        {
            logger.Info($"Completed '{uniqueName}', '{topicId}' and count {Result.Count}");

            databaseModel.Indicators.Save(Result, persistenceManagerMain);
        }
    }
}

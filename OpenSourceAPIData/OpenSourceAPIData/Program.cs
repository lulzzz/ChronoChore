using log4net;
using OpenSourceAPIData.Persistence.Logic;
using OpenSourceAPIData.WorldBankData.Logic;
using OpenSourceAPIData.WorldBankData.Model;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace OpenSourceAPIData
{
    class Program
    {
        protected static ILog logger = LogManager.GetLogger(typeof(Program));

        static void Main(string[] args)
        {
            logger.Info("Start the Open API application");

            Process().Wait();

            logger.Info("End the Open API application");
        }

        public static async Task Process()
        {
            using (var persistenceManager = new PersistenceManager("WorldBank",
                (topic) => new SqlitePersistContext(topic)))
            {
                // Create the database
                var databaseModel = new WorldBankOrgOSDatabase();
                databaseModel.Create(persistenceManager);

                var wbTopics = new WBTopicsWebServiceRest(persistenceManager, databaseModel);
                await wbTopics.Read();
            }
        }

        public static void WBWebServiceRestCompletionHandlerInMain<T>(string uniqueName, ConcurrentBag<T> Result)
        {
            logger.Info($"Completed '{uniqueName}' and count")
        }
    }
}

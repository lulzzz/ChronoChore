using log4net;
using OpenSourceAPIData.Persistence.Logic;
using OpenSourceAPIData.WorldBankData.Logic;
using OpenSourceAPIData.WorldBankData.Model;

namespace OpenSourceAPIData
{
    class Program
    {
        protected static ILog logger = LogManager.GetLogger(typeof(Program));

        static void Main(string[] args)
        {
            using (var persistenceManager = new PersistenceManager("WorldBank",
                (topic) => new SqlitePersistContext(topic)))
            {
                // Create the database
                var databaseModel = new WorldBankOrgOSDatabase();
                databaseModel.Create(persistenceManager);

                var wbTopics = new WBTopicsWebServiceRest();
                wbTopics.persistenceManager = persistenceManager;
                wbTopics.Database = databaseModel;
                wbTopics.Read();

                //databaseModel.Topics.Save(wbTopics.Result, persistenceManager);
            }
        }
    }
}

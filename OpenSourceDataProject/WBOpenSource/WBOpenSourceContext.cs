using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersistenceManagement;
using WBOpenSource.Model;

namespace WBOpenSource
{
    public class WBOpenSourceContext
    {
        WBPersistence persistence;

        public WBOpenSourceContext()
        {
            
        }

        public void Run()
        {
            CreateStore();

            var topicsRestService = new WBTopicsWebServiceRest((items) => persistence.Insert(items));
            topicsRestService.RequestCompleted += TopicsRestService_RequestCompleted;
            topicsRestService.Read();

            persistence.Wait();
        }

        private void CreateStore()
        {
            persistence = new WBPersistence(() => new SqlitePersistContext());
            Directory.CreateDirectory("Database");
            persistence.Create("WorldBankTopics", "Database");
        }

        private void TopicsRestService_RequestCompleted(WBWebCompletedArgs<TopicsTable> args)
        {
            var topicsIds = persistence.GetTopicsIdList();

            var indicatorsTasks = new List<Task>();
            foreach (var item in topicsIds)
            {
                var indicatorsWebRequest = new WBIndicatorsPerTopicWebServiceRest(
                    Convert.ToInt32(item), (items) => persistence.Insert(items));
                indicatorsWebRequest.RequestCompleted += IndicatorsWebRequest_RequestCompleted;
                indicatorsTasks.Add(Task.Factory.StartNew(() => indicatorsWebRequest.Read()));
            }

            Task.WaitAll(indicatorsTasks.ToArray());
        }

        private void IndicatorsWebRequest_RequestCompleted(WBWebCompletedArgs<IndicatorsTable> args)
        {
            persistence.taskQueue.Complete();
        }
    }
}

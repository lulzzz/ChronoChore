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
        }

        private void CreateStore()
        {
            persistence = new WBPersistence(() => new SqlitePersistContext());
            Directory.CreateDirectory("Database");
            persistence.Create("WorldBankTopics", "Database");
        }

        private void TopicsRestService_RequestCompleted(WBWebCompletedArgs<TopicsTable> args)
        {
            //persistence.Insert(args.Result);

            var indicatorsTasks = new List<Task>();
            foreach (var item in args.Result)
            {
                var indicatorsWebRequest = new WBIndicatorsPerTopicWebServiceRest(item.Id, (items) => persistence.Insert(items));
                indicatorsWebRequest.RequestCompleted += IndicatorsWebRequest_RequestCompleted;
                indicatorsTasks.Add(Task.Factory.StartNew(() => indicatorsWebRequest.Read()));
            }

            Task.WaitAll(indicatorsTasks.ToArray());
        }

        private void IndicatorsWebRequest_RequestCompleted(WBWebCompletedArgs<IndicatorsTable> args)
        {
            //persistence.Insert(args.Result);
        }
    }
}

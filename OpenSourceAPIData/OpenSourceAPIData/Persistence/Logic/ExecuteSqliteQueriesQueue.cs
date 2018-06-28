using AsyncAwaitDemo;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSourceAPIData.Persistence.Logic
{
    public class ExecuteSqliteQueriesQueue
    {
        protected static ILog logger = LogManager.GetLogger(typeof(ExecuteSqliteQueriesQueue));

        ConcurrentQueueService service;
        Task consumer;
        public bool AsyncMode { get; set; } = true;

        public void Setup()
        {
            if (AsyncMode)
            {
                service = new ConcurrentQueueService();
                service.maxParallelTasks = 1;
                consumer = Task.Factory.StartNew(service.Dequeue);
            }
        }

        public void Wait()
        {
            if (AsyncMode)
            {
                service.Completed = true;
                consumer.Wait();
            }
            logger.Info("Consumer completed");
        }

        public void AddTask(Action queryMethod)
        {
            logger.Debug($"Add execute task");

            if (AsyncMode)
                service.Enqueue(queryMethod);
            else
                queryMethod();
        }
    }
}

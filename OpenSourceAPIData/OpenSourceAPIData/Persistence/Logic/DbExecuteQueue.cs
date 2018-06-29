using Common.TaskQueue;
using System.Threading;

namespace OpenSourceAPIData.Persistence.Logic
{
    public class DbExecuteQueue : ProducerConsumerService
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="maxParallelism"></param>
        /// <param name="tokenSource"></param>
        public DbExecuteQueue(int maxParallelism = 50, CancellationTokenSource tokenSource = null)
            : base(maxParallelism, tokenSource) { }
    }
}

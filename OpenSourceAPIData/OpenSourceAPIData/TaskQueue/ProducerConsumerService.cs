using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Common.TaskQueue
{
    /// <summary>
    /// A common implementation of a producer consumer service class in
    /// asynchronous way.
    /// </summary>
    public class ProducerConsumerService
    {
        /// <summary>
        /// Using Microsoft's data structure
        /// </summary>
        ConcurrentQueue<QueueAction> _queue;

        /// <summary>
        /// A consumer
        /// </summary>
        Task consumer;

        /// <summary>
        /// A user interface handler
        /// </summary>
        public event QueueServiceEventHandler ProcessAction;

        /// <summary>
        /// A token used to cancel and stop the service queue
        /// </summary>
        public CancellationToken CancelToken { get; protected set; }

        /// <summary>
        /// The count of parallelism to achieve
        /// </summary>
        public int maxParallelTasks { get; protected set; } = 10;

        /// <summary>
        /// Set the completed request
        /// </summary>
        public bool MarkCompleted { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="maxParallelism"></param>
        /// <param name="tokenSource"></param>
        public ProducerConsumerService(int maxParallelism = 10, CancellationTokenSource tokenSource = null)
        {
            MarkCompleted = false;
            _queue = new ConcurrentQueue<QueueAction>();
            CancelToken = (tokenSource != null)? tokenSource.Token : CancellationToken.None;
            maxParallelTasks = maxParallelism;
            consumer = Task.Factory.StartNew(Dequeue);
        }
        
        /// <summary>
        /// Wait for the tasks in the queue to complete
        /// </summary>
        public void Wait() => consumer.Wait();

        /// <summary>
        /// Add new task to the queue
        /// </summary>
        /// <param name="action"></param>
        public void Enqueue(Action action)
        {
            _queue.Enqueue(new QueueAction()
            {
                ActionMethod = action,
                Id = _queue.Count
            });

            ProcessAction?.Invoke(new QueueServiceEventArgs(
                        _queue.Count, EQueueServiceState.Enqueue));
        }

        /// <summary>
        /// The main engine which executes as an infinite loop until it is cancelled or
        /// the queue becomes empty
        /// </summary>
        public void Dequeue()
        {
            while (true)
            {
                if (CancelToken.IsCancellationRequested)
                    CancelToken.ThrowIfCancellationRequested();

                var tasks = CollectActions();

                if (tasks.Count > 0)
                {
                    ProcessAction?.Invoke(new QueueServiceEventArgs(
                        tasks.Count, EQueueServiceState.BeforeExecute));

                    if (CancelToken.IsCancellationRequested)
                        CancelToken.ThrowIfCancellationRequested();

                    Parallel.ForEach(tasks, task => task.ActionMethod());

                    ProcessAction?.Invoke(new QueueServiceEventArgs(tasks.Count, 
                        EQueueServiceState.CompletedExecute));
                }
                else if (MarkCompleted && _queue.Count <= 0) break;
            }
        }

        /// <summary>
        /// Gather a list of actions to perform from the queue in FIFO order as per the max
        /// paralell count config.
        /// </summary>
        /// <returns></returns>
        private IList<QueueAction> CollectActions()
        {
            var tasks = new List<QueueAction>();
            for (int i = 0; i < maxParallelTasks; ++i)
                if (_queue.TryDequeue(out QueueAction action)) tasks.Add(action);

            return tasks;
        }
    }
}

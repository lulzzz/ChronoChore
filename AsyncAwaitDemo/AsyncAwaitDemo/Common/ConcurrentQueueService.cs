using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncAwaitDemo
{
    internal class QueueAction
    {
        public int Id { get; set; }
        public Action ActionMethod { get; set; }
    }

    public class ConcurrentQueueService
    {
        ConcurrentQueue<QueueAction> _queue;
        public event ProcessActionEventHandler ProcessAction;

        public bool Completed { get; internal set; } = false;
        public int maxParallelTasks { get; set; } = 10;

        public ConcurrentQueueService()
        {
            _queue = new ConcurrentQueue<QueueAction>();
        }

        public void Enqueue(Action action)
        {
            _queue.Enqueue(new QueueAction()
            {
                ActionMethod = action,
                Id = _queue.Count
            });
        }

        public void Dequeue()
        {
            while (true)
            {
                var tasks = new List<QueueAction>();
                for (int i = 0; i < maxParallelTasks; ++i)
                    if (_queue.TryDequeue(out QueueAction action)) tasks.Add(action);

                if (tasks.Count > 0)
                {
                    ProcessAction?.Invoke(new ProcessActionEventArgs(
                        tasks.Count, true));
                    Parallel.ForEach(tasks, task => task.ActionMethod());

                    ProcessAction?.Invoke(new ProcessActionEventArgs(tasks.Count, false));
                }
                else if (Completed && _queue.Count <= 0) break;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncAwaitDemo
{
    class TestConcurrentQueue
    {
        ConcurrentQueueService service = new ConcurrentQueueService();
        Random random = new Random();
          
        public void Run()
        {
            var t1 = Task.Factory.StartNew(() => ProduceAsync());
            var t2 = Task.Factory.StartNew(() => Consume());

            Task.WaitAll(t1, t2);
            Console.WriteLine("End of Program");
        }

        private void Consume()
        {
            service.Dequeue();
        }

        private void ProduceAsync()
        {
            for (int i = 0; i < 10; ++i)
            {
                service.Enqueue(async () => await DownloadFileAsync(i));
            }
            service.Completed = true;
        }

        private async Task<int> DownloadFileAsync(int i)
        {
            var value = (int)(random.NextDouble() * 10000);
            Console.WriteLine($"Start DownloadFileAsync ... waiting {value} miliseconds");
            var resultTask = Task.Delay(value);
            resultTask.Wait();
            Console.WriteLine($"Completed ... waiting {value} miliseconds");
            return value;
        }
    }
}

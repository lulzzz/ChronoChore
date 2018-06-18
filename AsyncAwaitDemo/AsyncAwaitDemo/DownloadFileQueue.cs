using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AsyncAwaitDemo
{
    public class DownloadFileQueue
    {
        protected static ILog logger = LogManager.GetLogger(typeof(DownloadFileQueue));

        ConcurrentQueueService service;

        IndexFile indexFile;
        
        Task consumer;

        public bool Redownload { get; set; } = false;
        public string Extension { get; set; }
        public bool AsyncMode { get; set; } = true;

        public void Setup(string local)
        {
            indexFile = new IndexFile(local);
            if (AsyncMode)
            {
                service = new ConcurrentQueueService();
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

        

        public void AddTask(string url)
        {
            logger.Debug($"Add download task ${url}");

            Uri uriNormalized = new Uri(url);
            

            if(!indexFile.Contains(uriNormalized.AbsoluteUri) ||
                (indexFile.Contains(uriNormalized.AbsoluteUri) && Redownload))
            {
                indexFile.Cache(uriNormalized.AbsoluteUri, Extension);

                if (AsyncMode)
                    service.Enqueue(() => Download(uriNormalized.AbsoluteUri,
                        indexFile.Value(uriNormalized.AbsoluteUri)));
                else
                    Download(uriNormalized.AbsoluteUri, indexFile.Value(uriNormalized.AbsoluteUri));
            }
        }

        private void Download(string url, string fileName)
        {
            logger.Debug($"Start download task ${url} to ${fileName}");
            using (var webclient = new WebClient())
            {
                ServicePointManager.SecurityProtocol =
                    SecurityProtocolType.Ssl3 |
                SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                ServicePointManager.ServerCertificateValidationCallback +=
                    (sender, cert, chain, error) =>
                    {
                        return true;
                    };
                
                webclient.DownloadFile(url, Path.Combine(indexFile.LocalFolder, fileName));
            }
            logger.Debug($"End download task ${url} to ${fileName}");
        }

        
    }
}

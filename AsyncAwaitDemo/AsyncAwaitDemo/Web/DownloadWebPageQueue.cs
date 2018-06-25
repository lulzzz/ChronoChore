using AngleSharp.Parser.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebScrap.Common;
using WebScrap.LibExtension.XPath;
using AngleSharp.Dom;
using log4net;

namespace AsyncAwaitDemo
{
    public class DownloadWebPageQueue
    {
        protected static ILog logger = LogManager.GetLogger(typeof(DownloadFileQueue));

        ConcurrentQueueService service;

        IndexFile indexFile;

        Task consumer;

        DownloadFileQueue downloadHtml;
        DownloadFileQueue downloadImges;
        DownloadFileQueue downloadJs;
        DownloadFileQueue downloadCss;

        public bool Redownload { get; set; } = false;
        public string Extension { get; set; }
        public bool AsyncMode { get; set; } = true;

        public void Setup(string local)
        {
            indexFile = new IndexFile(local);
            downloadHtml = new DownloadFileQueue()
            {
                Extension = ".html"
            };
            downloadHtml.Setup("WebPages");

            downloadImges = new DownloadFileQueue()
            {
                Extension = ".jpg"
            };
            downloadImges.Setup("images");

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


            if (!indexFile.Contains(uriNormalized.AbsoluteUri) ||
                (indexFile.Contains(uriNormalized.AbsoluteUri) && Redownload))
            {
                indexFile.Cache(uriNormalized.AbsoluteUri, Extension);

                if (AsyncMode)
                    service.Enqueue(() => Process(uriNormalized.AbsoluteUri,
                        indexFile.Value(uriNormalized.AbsoluteUri)));
                else
                    Process(uriNormalized.AbsoluteUri, indexFile.Value(uriNormalized.AbsoluteUri));
            }
        }

        private void Process(string url, string fileName)
        {
            var httpText = new HttpRequestAndLoad().Load(url);
            var rootElement = new HtmlParser().Parse(httpText).DocumentElement;

            var scriptTags = rootElement.SelectNodes("script");
            var aHrefTags = rootElement.SelectNodes("a[@href]");
            var imageTags = rootElement.SelectNodes("img[@src]");
            var linksTags = rootElement.SelectNodes("link");
            var objectTags = rootElement.SelectNodes("object"); // Svg
        }

        private void ProcessScript(string url, List<INode> nodes)
        {
            ConcurrentQueueService scriptService = new ConcurrentQueueService();

            foreach (IElement script in nodes)
            {
                if(script.HasAttribute("src"))
                {
                    var srcurl = script.GetAttribute("src");
                    var srcUrlNormalized = new Uri(new Uri(url), srcurl);
                }
            }
        }

        private void ProcessAHref(List<INode> nodes)
        {

        }

        private void ProcessImage(List<INode> nodes)
        {

        }

        private void ProcessLink(List<INode> nodes)
        {

        }
    }
}

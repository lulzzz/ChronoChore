using CommonHelpers.Logic;
using CommonHelpers.Parser;
using CommonHelpers.Web;
using log4net;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using System.Xml;

namespace WBOpenSource
{
    public class WBWebServiceRest<T>
    {
        private static ILog logger = LogManager.GetLogger(typeof(WBWebServiceRest<T>));

        /// <summary>
        /// Confguration
        /// </summary>
        protected WBWebServiceRestConfig config;
        
        protected ActionBlock<WBWebArgs> taskQueue;

        public delegate void WBCompletedHandler(WBWebCompletedArgs<T> args);

        public event WBCompletedHandler RequestCompleted;

        /// <summary>
        /// Saves the page number
        /// </summary>
        protected int TotalPages;

        /// <summary>
        /// Get the complete url
        /// </summary>
        public string AbsoluteUrl { get; set; }

        /// <summary>
        /// The result bag
        /// </summary>
        public ConcurrentBag<T> Result { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public WBWebServiceRest(WBWebServiceRestConfig config)
        {
            this.config = config;
            Result = new ConcurrentBag<T>();
            TotalPages = -1;
        }

        /// <summary>
        /// Get the api for the page
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        protected string GetApiPerPage(int page)
        {
            return $"{AbsoluteUrl}?page={page}&per_page={config.PerPageCount}";
        }

        /// <summary>
        /// Read the function
        /// </summary>
        public void Read()
        {
            // Request the first page
            var urlPage1 = GetApiPerPage(1);
            var xmlParser = RequestXmlResponse(urlPage1);

            // Read header
            xmlParser.Select(config.RootXPath);
            TotalPages = Convert.ToInt32(xmlParser.GetAttribute("pages"));

            var ApiRequestList = new List<string>();
            ForLoop.Run(2, TotalPages, (i) => ApiRequestList.Add(GetApiPerPage(i)));

            // Tasks
            xmlParser.Select(config.XPathDataNodes);
            taskQueue.SendAsync(new WBWebArgs()
            {
                urlPage = urlPage1,
                XmlParser = xmlParser
            });

            foreach (var api in ApiRequestList)
            {
                taskQueue.SendAsync(new WBWebArgs()
                {
                    urlPage = api
                });
            }

            taskQueue.Complete();
            taskQueue.Completion.Wait();

            OnCompleted();
        }

        protected virtual void OnCompleted()
        {
            var result = new WBWebCompletedArgs<T>
            {
                UniqueName = config.UniqueName,
                Result = new T[Result.Count]
            };
            Result.CopyTo(result.Result, 0);

            RequestCompleted?.Invoke(result);
        }

        /// <summary>
        /// Load html and read and parse response
        /// </summary>
        /// <param name="api"></param>
        protected virtual void LoadAndRead(WBWebArgs args)
        {
            if (args.XmlParser == null)
            {
                args.XmlParser = RequestXmlResponse(args.urlPage);
                args.XmlParser.Select(config.XPathDataNodes);
            }

            // Read Topics Nodes
            ReadNodes(args);
        }

        /// <summary>
        /// Read the list of nodes
        /// </summary>
        /// <param name="nodes"></param>
        protected virtual void ReadNodes(WBWebArgs args)
        {
            foreach (XmlNode node in args.XmlParser.nodes)
            {
                ReadNode(args.urlPage, node);
            }
        }

        /// <summary>
        /// Override the node
        /// </summary>
        /// <param name="node"></param>
        protected virtual void ReadNode(string api, XmlNode node) { }

        /// <summary>
        /// Download the resposne and parse into xml document
        /// </summary>
        /// <param name="api"></param>
        /// <returns></returns>
        protected virtual XmlParserWrapper RequestXmlResponse(string api)
        {
            logger.Info($"Request web resource from API '{api}'");
            var config = new HttpRequestConfiguration();

            var httpRequest = new HttpRequestAndLoad(config);
            var responseXml = httpRequest.Load(api);

            return new XmlParserWrapper(responseXml, "wb");
        }
    }
}

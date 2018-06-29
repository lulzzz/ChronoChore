using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml;
using CountryInformationDB;
using OpenSourceAPIData.Persistence.Logic;
using OpenSourceAPIData.WorldBankData.Model;
using WebScrap.Common;
using OpenSourceAPIData.Common;
using log4net;

namespace OpenSourceAPIData.WorldBankData.Logic
{
    /// <summary>
    /// World Bank API rest service base class
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class WBWebServiceRest<T> where T : class
    {
        protected static ILog logger = LogManager.GetLogger(typeof(WBWebServiceRest<T>));

        /// <summary>
        /// Confguration
        /// </summary>
        protected WBWebServiceRestConfig config;
        
        protected XmlNamespaceManager namespaceManager;

        public delegate void WBWebServiceRestCompletionHandler(string uniqueName, ConcurrentBag<T> Result);

        public event WBWebServiceRestCompletionHandler RequestCompleted;

        /// <summary>
        /// Saves the page number
        /// </summary>
        protected int TotalPages;

        /// <summary>
        /// Get the complete url
        /// </summary>
        public string Url
        {
            get
            {
                return new Uri(new Uri(config.BaseApi), config.RelativeUriPath).AbsoluteUri;
            }
        }
        
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
            RequestCompleted += Program.WBWebServiceRestCompletionHandlerInMain;
        }

        /// <summary>
        /// Get the api for the page
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        protected string GetApiPerPage(int page)
        {
            return $"{Url}?page={page}&per_page={config.PerPageCount}";
        }
        
        /// <summary>
        /// Read the function
        /// </summary>
        public async Task Read()
        {
            // Request the first page
            var urlPage1 = GetApiPerPage(1);
            var document = RequestXmlResponse(urlPage1);

            // Read header
            var rootNode = document.SelectSingleNode(config.RootXPath, namespaceManager);
            TotalPages = Convert.ToInt32(rootNode.Attributes["pages"].Value);

            var ApiRequestList = new List<string>();
            ForLoop.Run(2, TotalPages, (i) => ApiRequestList.Add(GetApiPerPage(i)));

            // Tasks
            var taskList = new List<Task>();
            taskList.Add(new Task(() => ReadNodes(urlPage1, document.SelectNodes(config.XPathDataNodes, namespaceManager))));
            foreach (var api in ApiRequestList)
            {
                taskList.Add(new Task(() => LoadAndRead(api)));
            }

            await Task.WhenAll(taskList.ToArray());
            //taskList.ForEach(task => task.Start());

            RequestCompleted?.Invoke(config.UniqueName);
            //Task.WaitAll(taskList.ToArray());
        }

        /// <summary>
        /// Load html and read and parse response
        /// </summary>
        /// <param name="api"></param>
        protected virtual void LoadAndRead(string api)
        {
            var document = RequestXmlResponse(api);

            // Read Topics Nodes
            ReadNodes(api, document.SelectNodes(config.XPathDataNodes, namespaceManager));
        }

        /// <summary>
        /// Read the list of nodes
        /// </summary>
        /// <param name="nodes"></param>
        protected virtual void ReadNodes(string api, XmlNodeList nodes)
        {
            foreach (XmlNode node in nodes)
            {
                ReadNode(api, node);
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
        protected virtual XmlDocument RequestXmlResponse(string api)
        {
            logger.Info($"Request web resource from API '{api}'");
            var config = new HttpRequestConfiguration();

            var httpRequest = new HttpRequestAndLoad(config);
            var responseXml = httpRequest.Load(api);

            var document = new XmlDocument();
            document.LoadXml(responseXml);
            namespaceManager = new XmlNamespaceManager(document.NameTable);
            namespaceManager.AddNamespace("wb", document.DocumentElement.NamespaceURI);

            return document;
        }
    }
}

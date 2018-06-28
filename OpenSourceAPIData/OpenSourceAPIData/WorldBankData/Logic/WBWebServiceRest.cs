using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml;
using CountryInformationDB;
using OpenSourceAPIData.Persistence.Logic;
using OpenSourceAPIData.WorldBankData.Model;
using WebScrap.Common;

namespace OpenSourceAPIData.WorldBankData.Logic
{
    public class WBWebServiceRest<T> where T : class
    {
        protected string Api { get; set; }
        protected string queryParamsForPage
        {
            get
            {
                return QueryParamsForPage(pagingConfig);
            }
        }
        protected string XpathNodes { get; set; }

        protected WBWebServicePaging pagingConfig;
        protected WBWebServicePaging pagingCounter;
        protected XmlNamespaceManager nsmgr;
        protected string RootXPath { get; set; }
        public PersistenceManager persistenceManager { get; set; }
        public WorldBankOrgOSDatabase Database { get; set; }

        public ConcurrentBag<T> Result;

        public WBWebServiceRest()
        {
            Result = new ConcurrentBag<T>();
            Initialize();
        }

        protected virtual void Initialize()
        {
            pagingConfig = new WBWebServicePaging()
            {
                PageNumber = 1,
                PerPageCount = 10
            };
            pagingCounter = pagingConfig.Clone();
        }

        protected string QueryParamsForPage(WBWebServicePaging pagingConfig)
        {
            var queryParams = new List<string>();

            if (pagingConfig.PageNumber > 0) queryParams.Add($"page={pagingConfig.PageNumber}");
            if (pagingConfig.PerPageCount > 0) queryParams.Add($"per_page={pagingConfig.PerPageCount}");

            return string.Join("&", queryParams);
        }

        public void Read()
        {
            SetApi();
            var doc = RequestXmlResponse(Api);
            var el = doc.DocumentElement;

            // Read header
            var rootNode = el.SelectSingleNode(RootXPath, nsmgr);
            SetPagingCounter(rootNode);

            var ApiRequestList = new List<string>();
            for (int i = 2; i <= pagingCounter.TotalPages; i++)
            {
                var paging = pagingCounter.Clone();
                paging.PageNumber = i;
                ApiRequestList.Add(GetApi(QueryParamsForPage(paging)));
            }

            // Tasks
            var taskList = new List<Task>();
            taskList.Add(new Task(() => ReadNodes(el.SelectNodes(XpathNodes, nsmgr))));
            foreach (var api in ApiRequestList)
            {
                taskList.Add(new Task(() => LoadAndRead(api)));
            }

            taskList.ForEach(task => task.Start());
            Task.WaitAll(taskList.ToArray());
        }

        protected virtual string GetApi(string queryParams)
        {
            return null;
        }

        protected virtual void LoadAndRead(string api)
        {
            var doc = RequestXmlResponse(api);
            var el = doc.DocumentElement;

            // Read Topics Nodes
            ReadNodes(el.SelectNodes(XpathNodes, nsmgr));
        }

        protected virtual void ReadNodes(XmlNodeList nodes)
        {
            foreach (XmlNode node in nodes)
            {
                ReadNode(node);
            }
        }

        protected virtual void ReadNode(XmlNode node)
        {
            
        }

        protected virtual void SetApi()
        {

        }

        protected virtual XmlDocument RequestXmlResponse(string api)
        {
            var config = new HttpRequestConfiguration();

            var httpRequest = new HttpRequestAndLoad(config);
            var responseXml = httpRequest.Load(api);

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(responseXml);
            XmlElement el = doc.DocumentElement; //TODO
            nsmgr = new XmlNamespaceManager(
                doc.NameTable);
            nsmgr.AddNamespace("wb", el.OwnerDocument.DocumentElement.NamespaceURI);

            return doc;
        }

        protected virtual void SetPagingCounter(XmlNode rootNode)
        {
            pagingCounter.PageNumber = Convert.ToInt32(rootNode.Attributes["page"].Value);
            pagingCounter.TotalPages = Convert.ToInt32(rootNode.Attributes["pages"].Value);
            pagingCounter.PerPageCount = Convert.ToInt32(rootNode.Attributes["per_page"].Value);
            pagingCounter.TotalCount = Convert.ToInt32(rootNode.Attributes["total"].Value);
        }
    }
}

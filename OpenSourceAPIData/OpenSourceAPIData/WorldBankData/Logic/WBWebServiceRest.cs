using AngleSharp.Dom;
using AngleSharp.Parser.Xml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.XPath;
using WebScrap.Common;
using WebScrap.LibExtension.XPath;
using CountryInformationDB;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;

namespace OpenSourceAPIData.WorldBankData.Logic
{
    public class WBWebServiceRest<T> where T : class
    {
        protected string Api { get; set; }
        protected string RootXPath { get; set; }

        protected int localCurrentPageIterator = 0;
        protected int currentPage = 0;
        protected int totalPages = 0;
        protected int perPage = 0;
        protected int totalCount = 0;
        protected XmlParser parser;

        public List<T> Result;

        public void Read()
        {
            do
            {
                localCurrentPageIterator++;
                SetApi();
                var config = new HttpRequestConfiguration();

                var httpRequest = new HttpRequestAndLoad(config);
                var responseXml = httpRequest.Load(Api);

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(responseXml);
                XmlElement el = doc.DocumentElement; //TODO
                XmlNamespaceManager nsmgr = new XmlNamespaceManager(
                    doc.NameTable);
                nsmgr.AddNamespace("wb", el.OwnerDocument.DocumentElement.NamespaceURI);

                // Read header
                IElement topicsNode = (IElement)el.SelectSingleNode(RootXPath);
                currentPage = Convert.ToInt32(topicsNode.GetAttribute("page"));
                totalPages = Convert.ToInt32(topicsNode.GetAttribute("pages"));
                perPage = Convert.ToInt32(topicsNode.GetAttribute("per_page"));
                totalCount = Convert.ToInt32(topicsNode.GetAttribute("total"));

                // Read Topics Nodes
                ReadNodes(el.SelectNodes("//wb:topic"));
            }
            while (perPage > 0 && currentPage < totalPages && localCurrentPageIterator != currentPage);
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
    }
}

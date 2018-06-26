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

namespace OpenSourceAPIData.WorldBankData.Logic
{
    public class WBWebServiceRest
    {
        protected string Api { get; set; }

        protected int currentPage = 1;
        protected int totalPages = 0;
        protected int perPage = 0;
        protected int totalCount = 0;
        protected XmlParser parser;

        public void Read()
        {
            do
            {
                var httpRequest = new HttpRequestAndLoad();
                var responseXml = httpRequest.LoadAsStream(Api);

                // Parse
                //parser = new XmlParser();
                //var rootElement = parser.Parse(responseXml).DocumentElement;

                XPathDocument document = new XPathDocument(XmlReader.Create(responseXml));
                XPathNavigator navigator = document.CreateNavigator();
                XmlNamespaceManager manager = new XmlNamespaceManager(navigator.NameTable);
                manager.AddNamespace("wb", "http://www.worldbank.org");

                XPathNodeIterator nodes = navigator.Select("//wb:topic", manager);

                if (nodes.MoveNext())
                {
                    // now nodes.Current points to the first selected node
                    XPathNavigator nodesNavigator = nodes.Current;

                    //select all the descendants of the current price node
                    XPathNodeIterator nodesText =
                       nodesNavigator.SelectDescendants(XPathNodeType.Text, false);

                    while (nodesText.MoveNext())
                    {
                        Console.WriteLine(nodesText.Current.Value);
                    }
                }

                // Read header
                //IElement topicsNode = (IElement)rootElement.SelectSingleNode("//topics");
                //currentPage = Convert.ToInt32(topicsNode.GetAttribute("page"));
                //totalPages = Convert.ToInt32(topicsNode.GetAttribute("pages"));
                //perPage = Convert.ToInt32(topicsNode.GetAttribute("per_page"));
                //totalCount = Convert.ToInt32(topicsNode.GetAttribute("total"));

                // Read Nodes
                //ReadNodes(rootElement.SelectNodes("//wb:topic"));
            }
            while (perPage > 0 && currentPage < totalPages);
        }

        protected virtual void ReadNodes(List<INode> list)
        {
            throw new NotImplementedException();
        }
    }
}

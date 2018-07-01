using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace CommonHelpers.Parser
{
    public class XmlParserWrapper
    {
        XmlDocument document;
        public IList<XmlNode> nodes;
        int currentIndex;
        XmlNamespaceManager namespaceManager;

        public XmlParserWrapper(string xmlString, string prefixNamespace)
        {
            document = new XmlDocument();
            document.LoadXml(xmlString);
            namespaceManager = new XmlNamespaceManager(document.NameTable);
            namespaceManager.AddNamespace("wb", document.DocumentElement.NamespaceURI);
            nodes = new List<XmlNode>() { document.DocumentElement };
            currentIndex = 0;
        }

        public IList<XmlNode> Select(string xpath)
        {
            var xmlNodes = document.SelectNodes(xpath, namespaceManager);

            if(xmlNodes != null)
            {
                nodes = xmlNodes.Cast<XmlNode>().ToList();
                currentIndex = 0;
            }
            else
                currentIndex = -1;

            return nodes;
        }

        public string GetAttribute(string attrName)
        {
            return nodes[currentIndex].Attributes[attrName]?.Value;
        }
    }
}

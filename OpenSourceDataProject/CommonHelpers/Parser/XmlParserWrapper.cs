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

        public XmlParserWrapper(XmlNode node)
        {
            document = node.OwnerDocument;
            namespaceManager = new XmlNamespaceManager(document.NameTable);
            namespaceManager.AddNamespace("wb", document.DocumentElement.NamespaceURI);
            nodes = new List<XmlNode>()
            {
                node.Clone()
            };
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

        public string GetAttribute(string xpath, string attrName)
        {
            return nodes[currentIndex].SelectSingleNode(xpath, namespaceManager).Attributes[attrName]?.Value;
        }

        public int GetAttributeInt(string attrName)
        {
            return Convert.ToInt32(GetAttribute(attrName));
        }

        public int GetAttributeInt(string xpath, string attrName)
        {
            return Convert.ToInt32(GetAttribute(xpath, attrName));
        }

        public string GetTextImmutable(string xpath)
        {
            return nodes[currentIndex].SelectSingleNode(xpath, namespaceManager)?.Value;
        }
    }
}

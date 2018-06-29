using System.Xml;

namespace OpenSourceAPIData.WorldBankData.Logic
{
    /// <summary>
    /// Parsing the specific World bank type formatted xml
    /// </summary>
    public class WBXmlResponseDocument : XmlDocument
    {
        protected XmlNamespaceManager nsmgr;

        /// <summary>
        /// Current node
        /// </summary>
        public XmlNode CurrentNode { get; set; }

        private XmlElement documentElement;
        public new XmlElement DocumentElement
        {
            get { return documentElement; }
        }

        /// <summary>
        /// Initialize constructor
        /// </summary>
        /// <param name="xmlString"></param>
        public WBXmlResponseDocument(string xmlString)
        {
            LoadXml(xmlString);
            documentElement = base.DocumentElement;
            Initialize(documentElement);
        }

        /// <summary>
        /// Initialize constructor
        /// </summary>
        /// <param name="xmlString"></param>
        public WBXmlResponseDocument(XmlNode node)
        {
            documentElement = node.OwnerDocument.DocumentElement;
            Initialize(node);
        }

        /// <summary>
        /// Initialize
        /// </summary>
        private void Initialize(XmlNode node)
        {
            nsmgr = new XmlNamespaceManager(this.NameTable);
            nsmgr.AddNamespace("wb", node.NamespaceURI);

            CurrentNode = node;
        }

        /// <summary>
        /// Select nodes
        /// </summary>
        /// <param name="xpath"></param>
        /// <returns></returns>
        public new XmlNodeList SelectNodes(string xpath)
        {
            return CurrentNode.SelectNodes(xpath, nsmgr);
        }

        /// <summary>
        /// Select nodes
        /// </summary>
        /// <param name="xpath"></param>
        /// <returns></returns>
        public new XmlNode SelectSingleNode(string xpath)
        {
            return CurrentNode.SelectSingleNode(xpath, nsmgr);
        }

        /// <summary>
        /// Get the attribute value
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GetAttribute(string name)
        {
            var attr = CurrentNode.Attributes[name];

            if (attr != null) return attr.Value;
            else return null;
        }

        /// <summary>
        /// Get the attribute value
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GetAttribute(string xpath, string name)
        {
            var node = SelectSingleNode(xpath);
            var attr = node.Attributes[name];

            if (attr != null) return attr.Value;
            else return null;
        }
    }
}

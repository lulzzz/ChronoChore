using AngleSharp.Dom.Html;
using System;
using System.Xml.XPath;

namespace CommonLibrary
{
    /// <summary>
    /// A class which provides a cursor model for navigating and editing HTML
    /// </summary>
    public class HtmlDocumentNavigable : IXPathNavigable
    {
        #region Fields
        
        /// <summary>
        /// A reference to the html document
        /// </summary>
        private readonly IHtmlDocument _document;

        #endregion Fields

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="document"></param>
        public HtmlDocumentNavigable(IHtmlDocument document)
        {
            if (document == null) throw new ArgumentNullException(nameof(document));
            _document = document;
        }

        #endregion Constructor

        #region IXPathNavigable

        /// <summary>
        /// Returns a new XPathNavigator object.
        /// </summary>
        /// <returns></returns>
        public XPathNavigator CreateNavigator()
        {
            return _document.CreateNavigator();
        }

        #endregion IXPathNavigable
    }
}

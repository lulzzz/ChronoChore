using AngleSharp.Dom;
using AngleSharp.Dom.Html;
using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.XPath;

namespace CommonLibrary
{
    /// <summary>
    /// An extension class for Angle Sharp <see cref="https://github.com/AngleSharp/AngleSharp"/> 
    /// parser which uses XPath 1.0 to navigate in a document
    /// NOTE: This class maybe removed later if AngleSharp provides the XPath implementation in the
    /// future
    /// </summary>
    public static class AngleSharp_XPath_Extension
    {
        /// <summary>
        /// Creates an XPathNavigator for navigating this object.
        /// The XPathNavigator provides read-only, random access to data.
        /// </summary>
        /// <param name="document">The <see cref="IDocument"/> object as 'this' pointer.</param>
        /// <returns></returns>
        public static XPathNavigator CreateNavigator(this IHtmlDocument document)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            return new HtmlDocumentNavigator(document, document.DocumentElement);
        }

        /// <summary>
        /// Get the named node pointer from the name table or add it to table if new
        /// </summary>
        /// <param name="table"></param>
        /// <param name="array"></param>
        /// <returns></returns>
        public static string GetOrAdd(this XmlNameTable table, string array)
        {
            var s = table.Get(array);

            if (s == null)
            {
                return table.Add(array);
            }

            return s;
        }

        /// <summary>
        /// using the xpath 1.0 string select only a single node
        /// </summary>
        /// <param name="element"></param>
        /// <param name="xpath"></param>
        /// <returns></returns>
        public static INode SelectSingleNode(this IElement element, string xpath)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            if (xpath == null)
            {
                throw new ArgumentNullException(nameof(xpath));
            }

            var nav = new HtmlDocumentNavigator(element.Owner, element);
            var it = nav.Select(xpath);

            if (!it.MoveNext())
            {
                return null;
            }

            var node = (HtmlDocumentNavigator)it.Current;
            return node.CurrentNode;
        }

        /// <summary>
        /// Selects a list of nodes matching the <see cref="XPath"/> expression.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="xpath">The XPath expression.</param>
        /// <returns>List of nodes matching <paramref name="xpath"/> query.</returns>
        /// <exception cref="ArgumentNullException">Throws if <paramref name="element"/> or <paramref name="xpath"/> is <c>null</c></exception>
        public static List<INode> SelectNodes(this IElement element, string xpath)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            if (xpath == null)
            {
                throw new ArgumentNullException(nameof(xpath));
            }

            var nav = new HtmlDocumentNavigator(element.Owner, element);
            var it = nav.Select(xpath);
            var result = new List<INode>();

            while (it.MoveNext())
            {
                var naviagtor = (HtmlDocumentNavigator)it.Current;
                var e = naviagtor.CurrentNode;
                result.Add(e);
            }

            return result;
        }
    }
}

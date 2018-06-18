using AngleSharp.Dom;
using System;
using System.Xml;
using System.Xml.XPath;

namespace WebScrap.LibExtension.XPath
{
    /// <summary>
    /// Provides a cursor model for navigating and editing HTML data
    /// </summary>
    public class HtmlDocumentNavigator : XPathNavigator
    {
        #region Fields

        /// <summary>
        /// Refers to the Html document
        /// </summary>
        private readonly IDocument _document;

        /// <summary>
        /// Saves the current node in iteration
        /// </summary>
        private INode _currentNode;

        /// <summary>
        /// Saves the attribute index
        /// </summary>
        private int _attrIndex;

        private readonly NameTable _nameTable;

        #endregion Fields

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="document"></param>
        /// <param name="currentNode"></param>
        public HtmlDocumentNavigator(IDocument document, INode currentNode)
        {
            if (document == null) throw new ArgumentNullException(nameof(document));
            if (currentNode == null) throw new ArgumentNullException(nameof(currentNode));

            _document = document;
            _currentNode = currentNode;
            _nameTable = new NameTable();
            _attrIndex = -1;
        }

        #endregion Constructor

        #region Properties

        /// <summary>
        /// Get the base URI of the html document
        /// </summary>
        public override string BaseURI => _document.BaseUri;

        /// <summary>
        /// Get the current node of the iteration
        /// </summary>
        public INode CurrentNode => _currentNode;

        /// <summary>
        /// Get the current element if the current node is an element.
        /// Else returns null.
        /// </summary>
        private IElement CurrentElement => CurrentNode as IElement;

        /// <summary>
        /// Check if the node is an element and it has attributes
        /// </summary>
        public override bool HasAttributes => CurrentElement != null && CurrentElement.Attributes.Length > 0;

        /// <summary>
        /// Check if the current node is a leaf node
        /// </summary>
        public override bool IsEmptyElement => !_currentNode.HasChildNodes;

        /// <summary>
        /// Get the local name of the node
        /// </summary>
        public override string LocalName
        {
            get
            {
                if (_attrIndex != -1)
                {
                    return NameTable.GetOrAdd(CurrentElement.Attributes[_attrIndex].Name);
                }

                if (CurrentNode is IElement)
                {
                    return NameTable.GetOrAdd(CurrentElement.LocalName);
                }

                return NameTable.GetOrAdd(CurrentNode.NodeName);
            }
        }

        /// <summary>
        /// get the name of the node
        /// </summary>
        public override string Name => NameTable.GetOrAdd(_currentNode.NodeName);
        
        /// <summary>
        /// get the reference Xml named table
        /// </summary>
        public override XmlNameTable NameTable => _nameTable;

        /// <summary>
        /// Get the converted equivalent System.Xml.XPath node type
        /// </summary>
        public override XPathNodeType NodeType
        {
            get
            {
                switch (_currentNode.NodeType)
                {
                    case AngleSharp.Dom.NodeType.Attribute:
                        return XPathNodeType.Attribute;

                    case AngleSharp.Dom.NodeType.CharacterData:
                        return XPathNodeType.Text;

                    case AngleSharp.Dom.NodeType.Comment:
                        return XPathNodeType.Comment;

                    case AngleSharp.Dom.NodeType.Document:
                        return XPathNodeType.Element;

                    //case AngleSharp.Dom.NodeType.DocumentFragment:
                    //	return XPathNodeType.;

                    //case AngleSharp.Dom.NodeType.DocumentType:
                    //	return XPathNodeType.;

                    case AngleSharp.Dom.NodeType.Element:
                        if (_attrIndex != -1)
                        {
                            return XPathNodeType.Attribute;
                        }

                        return XPathNodeType.Element;

                    //case AngleSharp.Dom.NodeType.Entity:
                    //	return XPathNodeType.en;

                    //case AngleSharp.Dom.NodeType.EntityReference:

                    //case AngleSharp.Dom.NodeType.Notation:
                    //	return XPathNodeType.;

                    case AngleSharp.Dom.NodeType.ProcessingInstruction:
                        return XPathNodeType.ProcessingInstruction;

                    case AngleSharp.Dom.NodeType.Text:
                        return XPathNodeType.Text;

                    default:
                        throw new NotImplementedException();
                }
            }
        }
        
        /// <summary>
        /// Get the value of the current node
        /// </summary>
        public override string Value
        {
            get
            {
                switch (_currentNode.NodeType)
                {
                    case AngleSharp.Dom.NodeType.Attribute:
                        var attr = (IAttr)_currentNode;
                        return attr.Value;

                    case AngleSharp.Dom.NodeType.CharacterData:
                        var cdata = (ICharacterData)_currentNode;
                        return cdata.Data;

                    case AngleSharp.Dom.NodeType.Comment:
                        var comment = (IComment)_currentNode;
                        return comment.Data;

                    case AngleSharp.Dom.NodeType.Document:
                        return _currentNode.TextContent;

                    case AngleSharp.Dom.NodeType.DocumentFragment:
                        return _currentNode.TextContent;

                    case AngleSharp.Dom.NodeType.DocumentType:
                        var documentType = (IDocumentType)_currentNode;
                        return documentType.Name;

                    case AngleSharp.Dom.NodeType.Element:
                        if (_attrIndex != -1)
                        {
                            return CurrentElement.Attributes[_attrIndex].Value;
                        }

                        return _currentNode.TextContent;

                    case AngleSharp.Dom.NodeType.Entity:
                        return _currentNode.TextContent;

                    case AngleSharp.Dom.NodeType.EntityReference:
                        return _currentNode.TextContent;

                    case AngleSharp.Dom.NodeType.Notation:
                        return _currentNode.TextContent;

                    case AngleSharp.Dom.NodeType.ProcessingInstruction:
                        var instruction = (IProcessingInstruction)_currentNode;
                        return instruction.Target;

                    case AngleSharp.Dom.NodeType.Text:
                        return _currentNode.TextContent;

                    default:
                        throw new NotImplementedException();
                }
            }
        }

        /// <summary>
        /// Not implemented
        /// </summary>
        public override string NamespaceURI
        {
            get
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Not implemented
        /// </summary>
        public override string Prefix
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Create and return a clone of the current navigator
        /// </summary>
        /// <returns></returns>
        public override XPathNavigator Clone()
        {
            return new HtmlDocumentNavigator(_document, _currentNode);
        }

        /// <summary>
        /// Check if the navigator passed as argument is at the same position
        /// as the current navigator object.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool IsSamePosition(XPathNavigator other)
        {
            var navigator = other as HtmlDocumentNavigator;
            if (navigator == null)
            {
                return false;
            }

            return navigator._currentNode == _currentNode;
        }

        /// <summary>
        /// Move to the XPath navigator node
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool MoveTo(XPathNavigator other)
        {
            var navigator = other as HtmlDocumentNavigator;
            if (navigator == null)
            {
                return false;
            }

            if (navigator._document == _document)
            {
                _currentNode = navigator._currentNode;
                _attrIndex = navigator._attrIndex;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Move to the first attribute
        /// </summary>
        /// <returns></returns>
        public override bool MoveToFirstAttribute()
        {
            if (HasAttributes)
            {
                _attrIndex = 0;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Move to the first child
        /// </summary>
        /// <returns></returns>
        public override bool MoveToFirstChild()
        {
            if (_currentNode.FirstChild == null)
            {
                return false;
            }

            _currentNode = _currentNode.FirstChild;
            return true;
        }

        /// <summary>
        /// Not implemented
        /// </summary>
        /// <param name="namespaceScope"></param>
        /// <returns></returns>
        public override bool MoveToFirstNamespace(XPathNamespaceScope namespaceScope)
        {
            return false;
        }

        /// <summary>
        /// Move to the node with the unique Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override bool MoveToId(string id)
        {
            IElement elementById = _document.GetElementById(id);

            if (elementById == null)
            {
                return false;
            }

            _currentNode = elementById;
            return true;
        }

        /// <summary>
        /// Move to next sibling
        /// </summary>
        /// <returns></returns>
        public override bool MoveToNext()
        {
            if (_currentNode.NextSibling == null)
            {
                return false;
            }

            _currentNode = _currentNode.NextSibling;
            return true;
        }

        /// <summary>
        /// Move to next attribute
        /// </summary>
        /// <returns></returns>
        public override bool MoveToNextAttribute()
        {
            if (CurrentElement == null)
            {
                return false;
            }

            if (_attrIndex >= CurrentElement.Attributes.Length - 1)
            {
                return false;
            }

            _attrIndex++;
            return true;
        }

        /// <summary>
        /// Not implemented
        /// </summary>
        /// <param name="namespaceScope"></param>
        /// <returns></returns>
        public override bool MoveToNextNamespace(XPathNamespaceScope namespaceScope)
        {
            return false;
        }

        /// <summary>
        /// Move to the parent node
        /// </summary>
        /// <returns></returns>
        public override bool MoveToParent()
        {
            if (_currentNode.Parent == null)
            {
                return false;
            }

            _currentNode = _currentNode.Parent;
            return true;
        }

        /// <summary>
        /// Move to previous sibling
        /// </summary>
        /// <returns></returns>
        public override bool MoveToPrevious()
        {
            if (_currentNode.PreviousSibling == null)
            {
                return false;
            }

            _currentNode = _currentNode.PreviousSibling;
            return true;
        }

        /// <summary>
        /// Move to the root node
        /// </summary>
        public override void MoveToRoot()
        {
            _currentNode = _document.DocumentElement;
        }

        #endregion Methods
    }
}

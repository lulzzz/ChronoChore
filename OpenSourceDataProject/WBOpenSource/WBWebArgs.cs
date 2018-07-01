using CommonHelpers.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace WBOpenSource
{
    public class WBWebArgs
    {
        public string urlPage { get; set; }
        public XmlParserWrapper XmlParser { get; set; }
    }
}

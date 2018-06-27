using AngleSharp.Dom;
using AngleSharp.Parser.Xml;
using OpenSourceAPIData.WorldBankData.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebScrap.Common;
using WebScrap.LibExtension.XPath;
using System.Xml;

namespace OpenSourceAPIData.WorldBankData.Logic
{
    public class WBTopicsWebServiceRest : WBWebServiceRest<TopicsTable>
    {
        public WBTopicsWebServiceRest()
        {
            RootXPath = @"//topics";
        }

        protected override void ReadNode(XmlNode node)
        {
            Result.Add(new TopicsTable
            {
                Id = Convert.ToInt32(node.Attributes["id"]),
                Value = node.SelectSingleNode(".//wb:value").Value,
                SourceNote = node.SelectSingleNode(".//wb:sourceNote").Value,
            });
        }

        protected override void SetApi()
        {
            Api = $"https://api.worldbank.org/v2/topics?page={localCurrentPageIterator}";
        }
    }
}

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

namespace OpenSourceAPIData.WorldBankData.Logic
{
    public class WBTopicsWebServiceRest : WBWebServiceRest
    {
        public WBTopicsWebServiceRest()
        {
            Api = $"http://api.worldbank.org/v2/topics?page={currentPage}";
        }

        protected override void ReadNodes(List<INode> list)
        {
            foreach (IElement item in list)
            {
                var topicsObj = new TopicsTable
                {
                    Id = Convert.ToInt32(item.GetAttribute("id")),
                    Value = item.SelectSingleNode(".//wb:value").TextContent,
                    SourceNote = item.SelectSingleNode(".//wb:sourceNote").TextContent,
                };
            }
        }
    }
}

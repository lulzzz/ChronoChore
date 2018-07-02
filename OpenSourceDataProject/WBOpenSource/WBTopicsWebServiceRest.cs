using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using System.Xml;
using WBOpenSource.Model;

namespace WBOpenSource
{
    /// <summary>
    /// Get the list of all topics which forms the first part of all the world bank type data
    /// </summary>
    public class WBTopicsWebServiceRest : WBWebServiceRest<TopicsTable>
    {
        private static ILog logger = LogManager.GetLogger(typeof(WBTopicsWebServiceRest));
        
        /// <summary>
        /// Initialize all the config values
        /// </summary>
        public WBTopicsWebServiceRest(Action<TopicsTable[]> action) : base(new WBWebServiceRestConfig(), action)
        {
            config.UniqueName = "topics";
            config.RelativeUriPath = config.UniqueName;
            config.RootXPath = $@"//wb:{config.UniqueName}";
            config.XPathDataNodes = ".//wb:topic";
            AbsoluteUrl = new Uri(new Uri(config.BaseApi), config.RelativeUriPath).AbsoluteUri;
        }

        /// <summary>
        /// Parse individual node and save into result
        /// </summary>
        /// <param name="node"></param>
        protected override void ReadNode(WBWebArgs args)
        {
            var id = args.XmlParser.GetAttributeInt("id");
            var valueText = args.XmlParser.GetTextImmutable(".//wb:value/text()");
            var sourceNoteText = args.XmlParser.GetTextImmutable(".//wb:sourceNote/text()");

            batchResultBlock.SendAsync(new TopicsTable
            {
                Id = id,
                Value = valueText,
                SourceNote = sourceNoteText,
            });
        }
    }
}

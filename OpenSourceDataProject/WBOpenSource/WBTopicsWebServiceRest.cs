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

        //public delegate void WBTopicsWebServiceRestCompletedHandler(string uniqueName, ConcurrentBag<TopicsTable> Result);
        

        /// <summary>
        /// Initialize all the config values
        /// </summary>
        public WBTopicsWebServiceRest(/*PersistenceManager manager, WorldBankOrgOSDatabase db,*/)
            : base(new WBWebServiceRestConfig())
        {
            config.UniqueName = "topics";
            config.RelativeUriPath = config.UniqueName;
            config.RootXPath = $@"//wb:{config.UniqueName}";
            config.XPathDataNodes = ".//wb:topic";
            AbsoluteUrl = new Uri(new Uri(config.BaseApi), config.RelativeUriPath).AbsoluteUri;
            //config.PersistenceManager = manager;
            //config.Database = db;

            //RequestCompleted += Program.WBTopicsWebServiceRestCompleted;
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

            Result.Add(new TopicsTable
            {
                Id = id,
                Value = valueText,
                SourceNote = sourceNoteText,
            });

            // For each topic fetch indicators
            //logger.Info($"Fetch all indicators for topic '{id}'");
            //var indicatorsRestObj = new WBIndicatorsPerTopicWebServiceRest(
            //    id, config.PersistenceManager, config.Database, tasksConsumerService);
            //indicatorsRestObj.Read();
        }
    }
}

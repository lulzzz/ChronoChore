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
        protected override void ReadNode(string api, XmlNode node)
        {
            var id = Convert.ToInt32(node.Attributes["id"].Value);
            var valueText = node.SelectSingleNode(".//wb:value/text()", namespaceManager);
            var sourceNoteText = node.SelectSingleNode(".//wb:sourceNote/text()", namespaceManager);

            Result.Add(new TopicsTable
            {
                Id = id,
                Value = (valueText == null) ? null : valueText.Value,
                SourceNote = (sourceNoteText == null) ? null : sourceNoteText.Value,
            });

            // For each topic fetch indicators
            //logger.Info($"Fetch all indicators for topic '{id}'");
            //var indicatorsRestObj = new WBIndicatorsPerTopicWebServiceRest(
            //    id, config.PersistenceManager, config.Database, tasksConsumerService);
            //indicatorsRestObj.Read();
        }
    }
}

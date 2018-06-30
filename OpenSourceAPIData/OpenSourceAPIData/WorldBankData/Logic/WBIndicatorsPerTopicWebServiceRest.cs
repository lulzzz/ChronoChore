using System;
using System.Xml;
using OpenSourceAPIData.WorldBankData.Model;
using log4net;
using OpenSourceAPIData.Persistence.Logic;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Common.TaskQueue;

namespace OpenSourceAPIData.WorldBankData.Logic
{
    /// <summary>
    /// Request for indicators data
    /// </summary>
    public class WBIndicatorsPerTopicWebServiceRest : WBWebServiceRest<IndicatorsTable>
    {
        protected static ILog logger = LogManager.GetLogger(typeof(WBIndicatorsPerTopicWebServiceRest));

        /// <summary>
        /// The topic id for which the indicators are fetched
        /// </summary>
        public int TopicId { get; set; }

        public delegate void WBIndicatorsPerTopicWebServiceRestCompletedHandler(string uniqueName, int topicId,
            ConcurrentBag<IndicatorsTable> Result);

        public event WBIndicatorsPerTopicWebServiceRestCompletedHandler RequestCompleted;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="manager"></param>
        /// <param name="db"></param>
        public WBIndicatorsPerTopicWebServiceRest(int topicId, PersistenceManager manager, WorldBankOrgOSDatabase db,
            ProducerConsumerService tasksConsumerService)
            : base(tasksConsumerService, new WBWebServiceRestConfig())
        {
            TopicId = topicId;
            config.UniqueName = "indicators";
            config.RelativeUriPath = $"topics/{TopicId}/{config.UniqueName}";
            config.RootXPath = $@"//wb:{config.UniqueName}";
            config.XPathDataNodes = ".//wb:indicator";
            config.PersistenceManager = manager;
            config.Database = db;

            RequestCompleted += Program.WBIndicatorsPerTopicWebServiceRestCompleted;
        }

        protected override void OnCompleted()
        {
            tasksConsumerService.Enqueue(() => RequestCompleted?.Invoke(config.UniqueName, TopicId, Result));
        }

        /// <summary>
        /// Read the node
        /// </summary>
        /// <param name="node"></param>
        protected override void ReadNode(string api, XmlNode node)
        {
            var nameText = node.SelectSingleNode("./wb:name/text()", namespaceManager);
            var sourceText = node.SelectSingleNode("./wb:source/text()", namespaceManager);
            var sourceNoteText = node.SelectSingleNode(".//wb:sourceNote/text()", namespaceManager);
            var sourceOrganizationText = node.SelectSingleNode(".//wb:sourceOrganization/text()", namespaceManager);

            var indicatorsData = new IndicatorsTable
            {
                Id = node.Attributes["id"].Value,
                TopicId = TopicId,
                Name = nameText?.Value,
                SourceId = Convert.ToInt32(node.SelectSingleNode("./wb:source", namespaceManager).Attributes["id"].Value),
                Source = sourceText?.Value,
                SourceNote = sourceNoteText?.Value,
                SourceOrgaisation = sourceOrganizationText?.Value,
            };

            logger.Info($"Read indicator id '{indicatorsData.Id} for Topic '{TopicId}' on request Uri '{api}'");

            Result.Add(indicatorsData);
        }
    }
}

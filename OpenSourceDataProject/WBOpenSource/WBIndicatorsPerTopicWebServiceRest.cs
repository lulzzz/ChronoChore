using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using WBOpenSource.Model;

namespace WBOpenSource
{
    public class WBIndicatorsPerTopicWebServiceRest : WBWebServiceRest<IndicatorsTable>
    {
        private static ILog logger = LogManager.GetLogger(typeof(WBTopicsWebServiceRest));

        /// <summary>
        /// The topic id for which the indicators are fetched
        /// </summary>
        public int TopicId { get; set; }

        /// <summary>
        /// Initialize all the config values
        /// </summary>
        public WBIndicatorsPerTopicWebServiceRest(int topicId, Action<IndicatorsTable[]> action) : base(new WBWebServiceRestConfig(), action)
        {
            TopicId = topicId;
            config.UniqueName = "indicators";
            config.RelativeUriPath = config.UniqueName;
            config.RootXPath = $@"//wb:{config.UniqueName}";
            config.XPathDataNodes = ".//wb:indicator";
            AbsoluteUrl = new Uri(new Uri(config.BaseApi), config.RelativeUriPath).AbsoluteUri;
        }

        /// <summary>
        /// Parse individual node and save into result
        /// </summary>
        /// <param name="node"></param>
        protected override void ReadNode(WBWebArgs args)
        {
            var nameText = args.XmlParser.GetTextImmutable("./wb:name/text()");
            var sourceText = args.XmlParser.GetTextImmutable("./wb:source/text()");
            var sourceNoteText = args.XmlParser.GetTextImmutable(".//wb:sourceNote/text()");
            var sourceOrganizationText = args.XmlParser.GetTextImmutable(".//wb:sourceOrganization/text()");

            var indicatorsData = new IndicatorsTable
            {
                Id = args.XmlParser.GetAttribute("id"),
                TopicId = TopicId,
                Name = nameText,
                SourceId = args.XmlParser.GetAttributeInt("./wb:source", "id"),
                Source = sourceText,
                SourceNote = sourceNoteText,
                SourceOrgaisation = sourceOrganizationText,
            };

            //logger.Info($"Read indicator id '{indicatorsData.Id} for Topic '{TopicId}' on request Uri '{args.urlPage}'");

            batchResultBlock.Post(indicatorsData);
        }
    }
}

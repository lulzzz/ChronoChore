using OpenSourceAPIData.WorldBankData.Model;
using System;
using System.Collections.Generic;
using System.Xml;
using System.Linq;
using log4net;
using OpenSourceAPIData.Persistence.Logic;

namespace OpenSourceAPIData.WorldBankData.Logic
{
    /// <summary>
    /// Get the list of all topics which forms the first part of all the world bank type data
    /// </summary>
    public class WBTopicsWebServiceRest : WBWebServiceRest<TopicsTable>
    {
        protected static ILog logger = LogManager.GetLogger(typeof(WBTopicsWebServiceRest));

        /// <summary>
        /// Initialize all the config values
        /// </summary>
        public WBTopicsWebServiceRest(PersistenceManager manager, WorldBankOrgOSDatabase db)
            : base(new WBWebServiceRestConfig())
        {
            config.UniqueName = "topics";
            config.RelativeUriPath = config.UniqueName;
            config.RootXPath = $@"//wb:{config.UniqueName}";
            config.XPathDataNodes = ".//wb:topic";
            config.PersistenceManager = manager;
            config.Database = db;
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
            
            //config.Database.Topics.Save(new TopicsTable
            //{
            //    Id = id,
            //    Value = (valueText == null) ? null : valueText.Value,
            //    SourceNote = (sourceNoteText == null) ? null : sourceNoteText.Value,
            //}, config.PersistenceManager);

            // For each topic fetch indicators
            logger.Info($"Fetch all indicators for topic '{id}'");
            var indicatorsRestObj = new WBIndicatorsPerTopicWebServiceRest(
                id, config.PersistenceManager, config.Database);
            indicatorsRestObj.Read();

            //// Topics and Indicators relation
            //var topicsIndicatorsList = new List<TopicsIndicatorsRelationTable>();
            //foreach (var item in indicatorsRestObj.Result)
            //{
            //    logger.Info($"Save indicator id '{item.Id}' for topic '{id}'");
            //    config.Database.Indicators.Save(item, config.PersistenceManager);
            //    topicsIndicatorsList.Add(new TopicsIndicatorsRelationTable()
            //    {
            //        IndicatorsId = item.Id,
            //        TopicsId = id
            //    });
            //}

            //var valueList = topicsIndicatorsList.Select(item => $"{item.IndicatorsId},{item.TopicsId}").ToList();
            //var valuesQuery = string.Join("\n", valueList);
            //logger.Info(valuesQuery);

            //config.Database.TopicsIndicators.Save(topicsIndicatorsList, config.PersistenceManager);
        }
    }
}

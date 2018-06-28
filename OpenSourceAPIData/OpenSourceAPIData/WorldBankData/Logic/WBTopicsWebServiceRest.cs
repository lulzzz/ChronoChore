using OpenSourceAPIData.WorldBankData.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Linq;
using log4net;

namespace OpenSourceAPIData.WorldBankData.Logic
{
    public class WBTopicsWebServiceRest : WBWebServiceRest<TopicsTable>
    {
        protected static ILog logger = LogManager.GetLogger(typeof(WBTopicsWebServiceRest));

        public WBTopicsWebServiceRest()
        {
            RootXPath = @"//wb:topics";
            XpathNodes = ".//wb:topic";
        }

        protected override void ReadNode(XmlNode node)
        {
            var id = Convert.ToInt32(node.Attributes["id"].Value);
            var valueText = node.SelectSingleNode(".//wb:value/text()", nsmgr);
            var sourceNoteText = node.SelectSingleNode(".//wb:sourceNote/text()", nsmgr);
            //Result.Add(new TopicsTable
            //{
            //    Id = Convert.ToInt32(node.Attributes["id"].Value),
            //    Value = (valueText == null)? null: valueText.Value,
            //    SourceNote = (sourceNoteText == null)? null:sourceNoteText.Value,
            //});

            Database.Topics.Save(new TopicsTable
            {
                Id = id,
                Value = (valueText == null) ? null : valueText.Value,
                SourceNote = (sourceNoteText == null) ? null : sourceNoteText.Value,
            }, persistenceManager);

            // For each topic fetch indicators
            var indicatorsRestObj = new WBIndicatorsPerTopicWebServiceRest();
            indicatorsRestObj.Database = Database;
            indicatorsRestObj.persistenceManager = persistenceManager;
            indicatorsRestObj.TopicId = id;
            indicatorsRestObj.Read();

            // Topics and Indicators relation
            var topicsIndicatorsList = new List<TopicsIndicatorsRelationTable>();
            foreach (var item in indicatorsRestObj.Result)
            {
                logger.Info($"Save indicator id '{item.Id}' for topic '{id}'");
                Database.Indicators.Save(item, persistenceManager);
                topicsIndicatorsList.Add(new TopicsIndicatorsRelationTable()
                {
                    IndicatorsId = item.Id,
                    TopicsId = id
                });
            }

            var valueList = topicsIndicatorsList.Select(item => $"{item.IndicatorsId},{item.TopicsId}").ToList();
            var valuesQuery = string.Join("\n", valueList);
            logger.Info(valuesQuery);

            Database.TopicsIndicators.Save(topicsIndicatorsList, persistenceManager);
        }

        protected override void SetApi()
        {
            Api = $"https://api.worldbank.org/v2/topics?{queryParamsForPage}";
        }

        protected override string GetApi(string queryParams)
        {
            return $"https://api.worldbank.org/v2/topics?{queryParams}";
        }
    }
}

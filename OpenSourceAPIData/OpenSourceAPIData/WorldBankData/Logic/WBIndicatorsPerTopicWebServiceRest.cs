using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using OpenSourceAPIData.WorldBankData.Model;
using log4net;

namespace OpenSourceAPIData.WorldBankData.Logic
{
    public class WBIndicatorsPerTopicWebServiceRest : WBWebServiceRest<IndicatorsTable>
    {
        protected static ILog logger = LogManager.GetLogger(typeof(WBIndicatorsPerTopicWebServiceRest));

        public int TopicId { get; set; }
        public WBIndicatorsPerTopicWebServiceRest()
        {
            RootXPath = @"//wb:indicators";
            XpathNodes = ".//wb:indicator";
        }

        protected override void ReadNode(XmlNode node)
        {
            var nameText = node.SelectSingleNode("./wb:name/text()", nsmgr);
            var sourceText = node.SelectSingleNode("./wb:source/text()", nsmgr);
            var sourceNoteText = node.SelectSingleNode(".//wb:sourceNote/text()", nsmgr);
            var sourceOrganizationText = node.SelectSingleNode(".//wb:sourceOrganization/text()", nsmgr);
            Result.Add(new IndicatorsTable
            {
                Id = node.Attributes["id"].Value,
                TopicId = TopicId,
                Name = nameText?.Value,
                SourceId = Convert.ToInt32(node.SelectSingleNode("./wb:source", nsmgr).Attributes["id"].Value),
                Source = sourceText?.Value,
                SourceNote = sourceNoteText?.Value,
                SourceOrgaisation = sourceOrganizationText?.Value,
            });
        }

        protected override void SetApi()
        {
            Api = $"https://api.worldbank.org/v2/topics/{TopicId}/indicators?{queryParamsForPage}";
        }

        protected override string GetApi(string queryParams)
        {
            return $"https://api.worldbank.org/v2/topics/{TopicId}/indicators?{queryParams}";
        }
    }
}

using OpenSourceAPIData.Persistence.Logic;
using System.Collections.Generic;
using System.Linq;

namespace OpenSourceAPIData.WorldBankData.Model
{
    public class TopicsIndicatorsRelationTable : BaseTable<TopicsIndicatorsRelationTable>
    {
        public int Id { get; set; }
        public string IndicatorsId { get; set; }
        public int TopicsId { get; set; }

        public override void Create(PersistenceManager persistenceManager)
        {
            string query = $"CREATE TABLE IF NOT EXISTS {nameof(TopicsIndicatorsRelationTable).Substring(0, nameof(TopicsIndicatorsRelationTable).Length - 5)} (" +
                $"{nameof(IndicatorsId)} TEXT, " +
                $"{nameof(TopicsId)} INT," +
                $"PRIMARY KEY ({nameof(IndicatorsId)}, {nameof(TopicsId)}))";

            persistenceManager.CreateTable(query);
        }

        public override void Save(IEnumerable<TopicsIndicatorsRelationTable> resultSet, PersistenceManager persistenceManager)
        {
            var valueList = resultSet.Select(item => $"('{item.IndicatorsId}',{item.TopicsId})").ToList();
            var valuesQuery = string.Join(",", valueList);
            var finalQuery = $"INSERT INTO {TableName} ({nameof(IndicatorsId)},{nameof(TopicsId)}) VALUES {valuesQuery}";

            persistenceManager.Insert(finalQuery);
        }
    }
}

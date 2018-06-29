using System.Collections.Generic;
using OpenSourceAPIData.Persistence.Logic;
using System.Linq;

namespace OpenSourceAPIData.WorldBankData.Model
{
    public class TopicsTable : BaseTable<TopicsTable>
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public string SourceNote { get; set; }

        public override void Create(PersistenceManager persistenceManager)
        {
            string query = $"CREATE TABLE IF NOT EXISTS {TableName} (" +
                $"{nameof(Id)} INT PRIMARY KEY, " +
                $"{nameof(Value)} TEXT NOT NULL, " +
                $"{nameof(SourceNote)} TEXT)";

            persistenceManager.CreateTable(query);
        }

        public override void Save(IEnumerable<TopicsTable> resultSet, PersistenceManager persistenceManager)
        {
            var valueList = resultSet.Select(item => $"({item.Id},'{NormalizeText(item.Value)}','{NormalizeText(item.SourceNote)}')").ToList();
            var valuesQuery = string.Join(",", valueList);
            var finalQuery = $"INSERT INTO {TableName} ({nameof(Id)},{nameof(Value)},{nameof(SourceNote)}) VALUES {valuesQuery}";

            persistenceManager.Insert(finalQuery);
        }

        public override void Save(TopicsTable result, PersistenceManager persistenceManager)
        {
            persistenceManager.Insert($"INSERT INTO {TableName} ({nameof(Id)},{nameof(Value)},{nameof(SourceNote)}) " +
                $"VALUES ({result.Id}, '{NormalizeText(result.Value)}', '{NormalizeText(result.SourceNote)}')");
        }
    }
}

using OpenSourceAPIData.Persistence.Logic;

namespace OpenSourceAPIData.WorldBankData.Model
{
    public class IndicatorsTable : BaseTable<IndicatorsTable>
    {
        public string Id { get; set; }
        public int TopicId { get; set; }
        public string Name { get; set; }
        public int SourceId { get; set; }
        public string Source { get; set; }
        public string SourceNote { get; set; }
        public string SourceOrgaisation { get; set; }

        public override void Create(PersistenceManager persistenceManager)
        {
            string query = $"CREATE TABLE IF NOT EXISTS {nameof(IndicatorsTable).Substring(0, nameof(IndicatorsTable).Length - 5)} (" +
                $"{nameof(Id)} TEXT, " +
                $"{nameof(TopicId)} INT, " +
                $"{nameof(Name)} TEXT NOT NULL, " +
                $"{nameof(SourceId)} INT," +
                $"{nameof(Source)} TEXT," +
                $"{nameof(SourceNote)} TEXT, " +
                $"{nameof(SourceOrgaisation)} TEXT," +
                $"PRIMARY KEY ({nameof(Id)}, {nameof(TopicId)}))";

            persistenceManager.CreateTable(query);
        }

        public override void Save(IndicatorsTable result, PersistenceManager persistenceManager)
        {
            persistenceManager.Insert($"INSERT INTO {TableName} ({nameof(Id)},{nameof(TopicId)},{nameof(Name)},{nameof(SourceId)},{nameof(Source)},{nameof(SourceNote)},{nameof(SourceOrgaisation)}) " +
                $"VALUES ('{result.Id}', {result.TopicId}, '{NormalizeText(result.Name)}', {result.SourceId}, '{NormalizeText(result.Source)}', '{NormalizeText(result.SourceNote)}', '{NormalizeText(result.SourceOrgaisation)}')");
        }
    }
}

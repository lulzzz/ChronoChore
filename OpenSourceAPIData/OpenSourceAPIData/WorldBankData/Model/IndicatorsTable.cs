using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenSourceAPIData.Persistence.Models;
using OpenSourceAPIData.Persistence.Logic;

namespace OpenSourceAPIData.WorldBankData.Model
{
    [DBTable]
    public class IndicatorsTable : BaseTable
    {
        [DBColumn(Primary = true, Nullable = false)]
        public string Id { get; set; }

        [DBColumn(Unique = true, Nullable = false)]
        public string Name { get; set; }
        public int SourceId { get; set; }
        public string Source { get; set; }
        public string SourceNote { get; set; }
        public string SourceOrgaisation { get; set; }

        public override void Create(SqlitePersistContext context)
        {
            string query = $"CREATE TABLE IF NOT EXISTS {nameof(IndicatorsTable).Substring(0, nameof(IndicatorsTable).Length - 5)} (" +
                $"{nameof(Id)} TEXT PRIMARY KEY, " +
                $"{nameof(Name)} TEXT NOT NULL, " +
                $"{nameof(SourceId)} INT," +
                $"{nameof(Source)} TEXT," +
                $"{nameof(SourceNote)} TEXT, " +
                $"{nameof(SourceOrgaisation)} TEXT)";

            context.ExecuteNonQuery(query);
        }
    }
}

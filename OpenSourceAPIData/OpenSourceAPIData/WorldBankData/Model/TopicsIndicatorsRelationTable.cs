using OpenSourceAPIData.Persistence.Logic;
using OpenSourceAPIData.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSourceAPIData.WorldBankData.Model
{
    [DBTable]
    public class TopicsIndicatorsRelationTable : BaseTable
    {
        [DBColumn(Primary = true, Nullable = false)]
        public int Id { get; set; }
        [DBColumn(Nullable = false)]
        public int IndicatorsId { get; set; }
        [DBColumn(Nullable = false)]
        public int TopicsId { get; set; }

        public override void Create(SqlitePersistContext context)
        {
            string query = $"CREATE TABLE IF NOT EXISTS {nameof(TopicsIndicatorsRelationTable).Substring(0, nameof(TopicsIndicatorsRelationTable).Length - 5)} (" +
                $"{nameof(IndicatorsId)} INT, " +
                $"{nameof(TopicsId)} INT," +
                $"PRIMARY KEY ({nameof(IndicatorsId)}, {nameof(TopicsId)}))";

            context.ExecuteNonQuery(query);
        }
    }
}

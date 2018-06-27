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
    public class TopicsTable : BaseTable
    {
        [DBColumn(Primary = true, Nullable = false)]
        public int Id { get; set; }

        [DBColumn(Unique = true, Nullable = false)]
        public string Value { get; set; }
        public string SourceNote { get; set; }

        public override void Create(SqlitePersistContext context)
        {
            string query = $"CREATE TABLE IF NOT EXISTS {nameof(TopicsTable).Substring(0, nameof(TopicsTable).Length - 5)} (" +
                $"{nameof(Id)} INT PRIMARY KEY, " +
                $"{nameof(Value)} TEXT NOT NULL, " +
                $"{nameof(SourceNote)} TEXT)";

            context.ExecuteNonQuery(query);
        }
    }
}

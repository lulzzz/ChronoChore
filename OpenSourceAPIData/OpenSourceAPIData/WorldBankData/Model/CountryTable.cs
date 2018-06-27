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
    public class CountryTable : BaseTable
    {
        [DBColumn(Primary = true, Nullable = false)]
        public string ISO2Code { get; set; }

        [DBColumn(Unique = true, Nullable = false)]
        public string Name { get; set; }
        public string ISO2CodeRegion { get; set; }
        public string IncomeLevel { get; set; }
        public string LendingType { get; set; }

        [DBColumn(Nullable = false)]
        public string MainCapitalCity { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public override void Create(SqlitePersistContext context)
        {
            string query = $"CREATE TABLE IF NOT EXISTS {nameof(CountryTable).Substring(0, nameof(CountryTable).Length - 5)} (" +
                $"{nameof(ISO2Code)} TEXT PRIMARY KEY, " +
                $"{nameof(Name)} TEXT NOT NULL, " +
                $"{nameof(ISO2CodeRegion)} TEXT," +
                $"{nameof(IncomeLevel)} TEXT," +
                $"{nameof(LendingType)} TEXT, " +
                $"{nameof(MainCapitalCity)} TEXT," +
                $"{nameof(Longitude)} REAL," +
                $"{nameof(Latitude)} REAL)";


            context.ExecuteNonQuery(query);
        }
    }
}

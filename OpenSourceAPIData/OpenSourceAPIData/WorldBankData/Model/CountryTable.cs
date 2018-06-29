using OpenSourceAPIData.Persistence.Logic;

namespace OpenSourceAPIData.WorldBankData.Model
{
    public class CountryTable : BaseTable<CountryTable>
    {
        public string ISO2Code { get; set; }
        public string Name { get; set; }
        public string ISO2CodeRegion { get; set; }
        public string IncomeLevel { get; set; }
        public string LendingType { get; set; }
        
        public string MainCapitalCity { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public override void Create(PersistenceManager persistenceManager)
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


            persistenceManager.CreateTable(query);
        }
    }
}

using OpenSourceAPIData.Persistence.Models;
using OpenSourceAPIData.Persistence.Logic;

namespace OpenSourceAPIData.WorldBankData.Model
{
    [DBDatabase]
    public class WorldBankOrgOSDatabase
    {
        public CountryTable Countries { get; set; }
        public TopicsTable Topics { get; set; }
        public IndicatorsTable Indicators { get; set; }
        public TopicsIndicatorsRelationTable TopicsIndicators { get; set; }

        public WorldBankOrgOSDatabase()
        {
            Countries = new CountryTable();
            Topics = new TopicsTable();
            Indicators = new IndicatorsTable();
            TopicsIndicators = new TopicsIndicatorsRelationTable();
        }

        public void Create(PersistenceManager persistenceManager)
        {
            persistenceManager.CreateStore("WorldBankOrganisation");
            Topics.Create(persistenceManager);
            Indicators.Create(persistenceManager);
            Countries.Create(persistenceManager);
            TopicsIndicators.Create(persistenceManager);
        }
    }
}

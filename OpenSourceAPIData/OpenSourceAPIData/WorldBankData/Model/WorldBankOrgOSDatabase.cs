using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenSourceAPIData.Persistence.Models;
using OpenSourceAPIData.Persistence.Logic;

namespace OpenSourceAPIData.WorldBankData.Model
{
    [DBDatabase]
    class WorldBankOrgOSDatabase
    {
        public CountryTable Countries { get; set; }
        public TopicsTable Topics { get; set; }
        public IndicatorsTable Indicators { get; set; }
        public TopicsIndicatorsRelationTable TopicsIndicators { get; set; }

        public void Create(SqlitePersistContext context)
        {
            context.CreateDatabase("WorldBankOrganisation");
            Topics.Create(context);
            Indicators.Create(context);
            Countries.Create(context);
            TopicsIndicators.Create(context);
        }
    }
}

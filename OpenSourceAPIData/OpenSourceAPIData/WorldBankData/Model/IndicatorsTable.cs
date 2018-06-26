using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenSourceAPIData.Persistence.Models;

namespace OpenSourceAPIData.WorldBankData.Model
{
    [DBTable]
    class IndicatorsTable
    {
        [DBColumn(Primary = true, Nullable = false)]
        public string Id { get; set; }

        [DBColumn(Unique = true, Nullable = false)]
        public string Name { get; set; }
        public int SourceId { get; set; }
        public string Source { get; set; }
        public string SourceNote { get; set; }
        public string SourceOrgaisation { get; set; }
    }
}

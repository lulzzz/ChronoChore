using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenSourceAPIData.Persistence.Models;

namespace OpenSourceAPIData.WorldBankData.Model
{
    [DBTable]
    class CountryTable
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
    }
}

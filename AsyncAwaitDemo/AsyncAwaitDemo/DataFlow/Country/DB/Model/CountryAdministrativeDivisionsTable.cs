using AsyncAwaitDemo.Persistence.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncAwaitDemo.DataFlow.Country.DB.Model
{
    [DBTable]
    class CountryAdministrativeDivisionsTable
    {
        public string CountryAlpha2Code { get; set; }
        public EAdministrativeDivisionType DivisionType { get; set; }
        public string NameInEnglish { get; set; }
        public int Count { get; set; }
    }
}

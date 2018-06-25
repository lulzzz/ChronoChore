using AsyncAwaitDemo.Persistence.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncAwaitDemo.DataFlow.Country.DB.Model
{
    [DBTable]
    class CountryInformation
    {
        public string Alpha2Code { get; set; }
        public string ShortName { get; set; }
        public string FullName { get; set; }
        public string Alpha3Code { get; set; }
        public string NumericCode { get; set; }
        public bool IsIndependent { get; set; }
    }
}

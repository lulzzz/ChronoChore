using AsyncAwaitDemo.Persistence.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncAwaitDemo.DataFlow.Country.DB.Model
{
    [DBTable]
    class CountryAdministrativeLanguages
    {
        public string Alpha2Code { get; set; }
        public string ISO639Alpha2Code { get; set; }
    }
}

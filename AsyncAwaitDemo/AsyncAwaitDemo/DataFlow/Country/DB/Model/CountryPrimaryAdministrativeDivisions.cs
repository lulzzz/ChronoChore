using AsyncAwaitDemo.Persistence.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncAwaitDemo.DataFlow.Country.DB.Model
{
    [DBTable]
    class CountryPrimaryAdministrativeDivisions
    {
        public string CountryAlpha2Code { get; set; }
        public string ISO3166_2Code { get; set; }
        public string SubdivisionName { get; set; }
        public string HASC { get; set; }
        public string PostalCodeType { get; set; }
        public int Population { get; set; }
        public int PopulationYear { get; set; }
        public double AreaKm2 { get; set; }
        public string Capital { get; set; }
    }
}

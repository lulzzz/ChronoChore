using AsyncAwaitDemo.Persistence.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncAwaitDemo.DataFlow.Country.DB.Model
{
    [DBTable]
    public class ISO3166MetadataTable
    {
        public IList<string> About { get; set; }
        public string Address { get; set; }
        public string EMail { get; set; }
        public string Telephone { get; set; }
        public string Alpha2Code { get; set; }
        public string Alpha3Code { get; set; }
        public string NumericCode { get; set; }
    }
}

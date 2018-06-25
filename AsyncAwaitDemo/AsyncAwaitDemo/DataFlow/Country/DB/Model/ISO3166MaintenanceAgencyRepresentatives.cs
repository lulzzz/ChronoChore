using AsyncAwaitDemo.Persistence.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncAwaitDemo.DataFlow.Country.DB.Model
{
    [DBTable]
    public class ISO3166MaintenanceAgencyRepresentatives
    {
        public string FullName { get; set; }
        public string Short { get; set; }
    }
}

using OpenSourceAPIData.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSourceAPIData.WorldBankData.Model
{
    [DBTable]
    class TopicsIndicatorsRelationTable
    {
        [DBColumn(Primary = true, Nullable = false)]
        public int Id { get; set; }
        [DBColumn(Nullable = false)]
        public int IndicatorsId { get; set; }
        [DBColumn(Nullable = false)]
        public int TopicsId { get; set; }
    }
}

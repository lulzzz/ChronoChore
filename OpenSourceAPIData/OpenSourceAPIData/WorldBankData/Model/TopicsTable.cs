using OpenSourceAPIData.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSourceAPIData.WorldBankData.Model
{
    [DBTable]
    class TopicsTable
    {
        [DBColumn(Primary = true, Nullable = false)]
        public int Id { get; set; }

        [DBColumn(Unique = true, Nullable = false)]
        public string Value { get; set; }
        public string SourceNote { get; set; }
    }
}

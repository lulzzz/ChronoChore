using System;

namespace OpenSourceAPIData.Persistence.Models
{
    class ColumnInformation
    {
        public string Name { get; set; }
        public Type DbType { get; set; }
        public bool IsNullable { get; set; }
        public bool IsPrimary { get; set; }
        public bool IsAutoIncrement { get; set; }
        public bool IsUnique { get; set; }
    }
}

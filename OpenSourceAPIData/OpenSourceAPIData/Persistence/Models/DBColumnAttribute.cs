using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSourceAPIData.Persistence.Models
{
    class DBColumnAttribute : Attribute
    {
        public const bool DefaultNullable = true;
        public const bool DefaultPrimary = false;
        public const bool DefaultAutoIncrement = false;
        public const bool DefaultUnique = false;

        public string Name { get; set; }
        public bool Nullable { get; set; }
        public bool Primary { get; set; }
        public bool AutoIncrement { get; set; }
        public bool Unique { get; set; }

        public DBColumnAttribute()
        {
            Nullable = DefaultNullable;
            Primary = DefaultPrimary;
            AutoIncrement = DefaultAutoIncrement;
            Unique = DefaultUnique;
        }
    }
}

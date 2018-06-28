using System;

namespace OpenSourceAPIData.Persistence.Models
{
    class DBDatabaseAttribute : Attribute
    {
        public string Name { get; set; }
    }
}

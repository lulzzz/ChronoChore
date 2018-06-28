using System;

namespace OpenSourceAPIData.Persistence.Models
{
    class DBTableAttribute : Attribute
    {
        public string Name { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncAwaitDemo.Persistence.Model
{
    class DBTableAttribute : Attribute
    {
        public string Name { get; set; }
        public bool IsMetadata { get; set; }
    }
}

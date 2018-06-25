using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncAwaitDemo.Persistence.Model
{
    public class DBDatabaseAttribute : Attribute
    {
        public string Name { get; set; }
    }
}

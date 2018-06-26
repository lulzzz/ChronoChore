using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSourceAPIData.Persistence.Models
{
    class DBTableAttribute : Attribute
    {
        public string Name { get; set; }
    }
}

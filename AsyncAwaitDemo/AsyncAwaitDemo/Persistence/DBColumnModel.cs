using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncAwaitDemo.Persistence
{
    class DBColumnModel
    {
        public string Name;
        public Type ColType;
        public bool IsPrimary;
        public bool IsNotNull;
        public bool IsAutoIncrement;
    }
}

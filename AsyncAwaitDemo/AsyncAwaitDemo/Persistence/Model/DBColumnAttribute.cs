﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncAwaitDemo.Persistence.Model
{
    class DBColumnAttribute
    {
        public string Name { get; set; }
        public bool IsNull { get; set; }
        public bool IsAutogenerated { get; set; }
        public bool IsPrimaryKey { get; set; }
    }
}

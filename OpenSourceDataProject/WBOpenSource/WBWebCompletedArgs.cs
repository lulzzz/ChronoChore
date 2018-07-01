using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBOpenSource
{
    public class WBWebCompletedArgs<T>
    {
        public string UniqueName { get; set; }
        public T[] Result { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncAwaitDemo
{
    public class ProcessActionEventArgs : EventArgs
    {
        public int ItemsToProcess { get; set; }
        public bool BeforeExecution { get; set; }

        public ProcessActionEventArgs(int items, bool beforeExecution)
        {
            ItemsToProcess = items;
            BeforeExecution = beforeExecution;
        }
    }

    public delegate void ProcessActionEventHandler(ProcessActionEventArgs);
}

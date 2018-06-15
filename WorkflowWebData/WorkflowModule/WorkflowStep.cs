using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowModule
{
    public class WorkflowStep
    {
        public string UniqueId { get; protected set; }

        public WorkflowStep()
        {
            UniqueId = Guid.NewGuid().ToString();
        }

        public virtual void Run() { }
    }
}

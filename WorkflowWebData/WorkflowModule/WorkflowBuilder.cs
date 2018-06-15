using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowModule
{
    public class WorkflowBuilder
    {
        private OrderedDictionary steps = new OrderedDictionary();

        public WorkflowBuilder StartWith(WorkflowStep step)
        {
            steps.Add(step.UniqueId, step);
            return this;
        }

        public WorkflowBuilder Then(WorkflowStep step)
        {
            steps.Add(step.UniqueId, step);
            return this;
        }
    }
}

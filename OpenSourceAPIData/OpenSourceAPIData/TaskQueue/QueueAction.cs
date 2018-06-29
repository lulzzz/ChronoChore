using System;

namespace Common.TaskQueue
{
    internal class QueueAction
    {
        public int Id { get; set; }
        public Action ActionMethod { get; set; }
    }
}

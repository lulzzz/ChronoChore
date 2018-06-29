using System;

namespace Common.TaskQueue
{
    /// <summary>
    /// The states for various Queue service activities
    /// </summary>
    public enum EQueueServiceState
    {
        None,
        Enqueue,
        BeforeExecute,
        CompletedExecute,
        CancelledExecute
    }

    /// <summary>
    /// Arguments to depict the cuttent state of the Queue execution
    /// </summary>
    public class QueueServiceEventArgs : EventArgs
    {
        /// <summary>
        /// Number of items to process
        /// </summary>
        public int ItemsToProcess { get; set; }

        /// <summary>
        /// The current state of the service
        /// </summary>
        public EQueueServiceState State { get; set; }

        public QueueServiceEventArgs(int items, EQueueServiceState state)
        {
            ItemsToProcess = items;
            State = state;
        }
    }

    /// <summary>
    /// Handler for queue service
    /// </summary>
    /// <param name="args"></param>
    public delegate void QueueServiceEventHandler(QueueServiceEventArgs args);
}

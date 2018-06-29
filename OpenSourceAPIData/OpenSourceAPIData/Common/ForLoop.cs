using System;

namespace OpenSourceAPIData.Common
{
    /// <summary>
    /// A generic for loop class
    /// </summary>
    public static class ForLoop
    {
        public static void Run(int start, int maxCount, Action<int> action)
        {
            for (int i = start; i < maxCount; i++)
            {
                action(i);
            }
        }
    }
}

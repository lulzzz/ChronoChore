using log4net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncAwaitDemo
{
    public class LogWrapTimer : IDisposable
    {
        protected static ILog logger = LogManager.GetLogger(typeof(LogWrapTimer));

        private Stopwatch stopwatch;

        public LogWrapTimer()
        {
            stopwatch = new Stopwatch();
            stopwatch.Start();
        }

        public void Dispose()
        {
            stopwatch.Stop();
            logger.InfoFormat("Time elapsed: {0}", stopwatch.Elapsed);
        }
    }
}

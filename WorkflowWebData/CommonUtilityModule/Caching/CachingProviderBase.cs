using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace CommonUtilityModule
{
    public class CachingProviderBase
    {
        private ILog logger = LogManager.GetLogger(typeof(CachingProviderBase));
        protected MemoryCache cache;
        static readonly object padlock;

        public CachingProviderBase()
        {
            cache = new MemoryCache("WorkflowCachingProvider");
        }

        protected virtual void AddItem(string key, object value)
        {
            lock (padlock)
            {
                cache.Add(key, value, DateTimeOffset.MaxValue);
            }
        }

        protected virtual void RemoveItem(string key)
        {
            lock (padlock)
            {
                cache.Remove(key);
            }
        }

        protected virtual object GetItem(string key, bool remove)
        {
            lock (padlock)
            {
                var res = cache[key];

                if (res != null)
                {
                    if (remove == true)
                        cache.Remove(key);
                }
                else
                {
                    logger.ErrorFormat("CachingProvider-GetItem: Don't contains key: {0}", key);
                }

                return res;
            }
        }
    }
}

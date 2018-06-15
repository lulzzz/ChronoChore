using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonUtilityModule
{
    public class GlobalCachingProvider : CachingProviderBase, IGlobalCachingProvider
    {
        #region Singleton 

        private static readonly Lazy<GlobalCachingProvider> lazy = new Lazy<GlobalCachingProvider>(() => new GlobalCachingProvider());

        public static GlobalCachingProvider Instance { get { return lazy.Value; } }

        private GlobalCachingProvider()
        {
        }

        #endregion

        #region ICachingProvider

        public virtual new void AddItem(string key, object value)
        {
            base.AddItem(key, value);
        }

        public virtual object GetItem(string key)
        {
            return base.GetItem(key, true);//Remove default is true because it's Global Cache!
        }

        public virtual new object GetItem(string key, bool remove)
        {
            return base.GetItem(key, remove);
        }

        #endregion
    }
}

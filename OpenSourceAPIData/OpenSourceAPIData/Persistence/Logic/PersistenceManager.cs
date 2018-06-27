using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSourceAPIData.Persistence.Logic
{
    public class PersistenceManager
    {
        private string Topic;
        private DBContextBase context;

        public PersistenceManager(string topic)
        {
            Topic = topic;
            Directory.CreateDirectory(Topic);
        }

        public void CreateStore(string storeName)
        {
            context.CreateDatabase(storeName);
        }

        public void Execute(string query)
        {
            using(context = new )
        }
    }
}

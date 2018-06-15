using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountryInformationDB
{
    class SingleWorkflowInSelenium
    {
        protected string rootWfDirectory;
        protected string rootDirectory;

        protected string wfUrl;
        protected string fullRootWfDirectory;

        protected SeleniumService service;

        public SingleWorkflowInSelenium(string rootWfDirectory)
        {
            this.rootWfDirectory = rootWfDirectory;
        }

        public void Initialize(SeleniumService service, string rootDirectory)
        {
            this.service = service;
            this.rootDirectory = rootDirectory;
            fullRootWfDirectory = Path.Combine(Path.GetFullPath(rootDirectory), rootWfDirectory);
        }

        public virtual void Load(string url) { }
        public virtual void Load() { }
    }
}

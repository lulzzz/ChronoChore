using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountryInformationDB
{
    class ContainerFactory
    {
        public static T Create<T>(SeleniumService service, string rootDirectory)
            where T : SingleWorkflowInSelenium, new()
        {
            var wfObj = new T();
            wfObj.Initialize(service, rootDirectory);
            return wfObj;
        }
    }
}

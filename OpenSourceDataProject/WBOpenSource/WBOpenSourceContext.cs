using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBOpenSource
{
    public class WBOpenSourceContext
    {
        WBPersistence persistence;

        public void Run()
        {
            var topicsRestService = new WBTopicsWebServiceRest();
            topicsRestService.RequestCompleted += TopicsRestService_RequestCompleted;
            topicsRestService.Read();
        }

        private void TopicsRestService_RequestCompleted(WBWebCompletedArgs<Model.TopicsTable> args)
        {
            
        }
    }
}

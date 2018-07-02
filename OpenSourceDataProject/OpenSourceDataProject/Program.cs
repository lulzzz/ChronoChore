﻿using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBOpenSource;

namespace OpenSourceDataProject
{
    class Program
    {
        protected static ILog logger = LogManager.GetLogger(typeof(Program));

        static void Main(string[] args)
        {
            logger.Info("Start the Open API application");

            var wbOpenSourceContext = new WBOpenSourceContext();
            wbOpenSourceContext.Run();
        }
    }
}

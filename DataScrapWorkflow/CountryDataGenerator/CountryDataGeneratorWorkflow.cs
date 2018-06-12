using CountryDataGenerator.CIA.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountryDataGenerator
{
    public class CountryDataGeneratorWorkflow
    {
        public void Run()
        {
            new AvailableCiaMaps().Run();
        }
    }
}

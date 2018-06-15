using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using CountryInformationDB.CIA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkflowModule;

namespace CountryInformationDB
{
    class Program
    {
        static void Main(string[] args)
        {
            var rootDirectory = "Country";
            using (var service = new SeleniumService())
            {
                var ciaFactbook = ContainerFactory.Create<CIAWorldFactbook>(service, rootDirectory);
                ciaFactbook.Load();
            }
        }
    }
}

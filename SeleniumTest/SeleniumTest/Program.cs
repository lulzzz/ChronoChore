using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using SeleniumTest.CIA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumTest
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var service = new SeleniumService())
            {
                var ciaFactbook = new CIAWorldFactbook(service);
                ciaFactbook.LoadFromGoogle();
            }
        }
    }
}

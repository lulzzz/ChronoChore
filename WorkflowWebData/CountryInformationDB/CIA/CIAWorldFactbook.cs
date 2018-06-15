using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CountryInformationDB.CIA
{
    class CIAWorldFactbook : SingleWorkflowInSelenium
    {
        RegionalAndWorldMaps worldMaps;
        FlagsOfTheWorld flagsOfTheWorld;

        public CIAWorldFactbook() : base(@"CIA") { }

        public override void Load()
        {
            wfUrl = service.GetRootUrlFromGoogle("world cia factbook");
            service.Navigate(wfUrl);
            ParseRequiredUrls();
        }
        
        private void ParseRequiredUrls()
        {
            foreach(var element in service.GetElementsXPath(@"//*[@id='buttons']/ul/li"))
            {
                var alinkhref = element.FindElement(By.XPath("a")).GetAttribute("href");
                var helpText = element.FindElement(By.XPath("a/img")).GetAttribute("alt");

                Process(helpText, alinkhref);
            }
        }

        private void Process(string helpText, string url)
        {
            if (helpText.Contains("Maps"))
            {
                worldMaps = ContainerFactory.Create<RegionalAndWorldMaps>(service, fullRootWfDirectory);
                worldMaps.Load(url);
            }
            else if (helpText.Contains("Flags"))
            {
                flagsOfTheWorld = ContainerFactory.Create<FlagsOfTheWorld>(service, fullRootWfDirectory);
                flagsOfTheWorld.Load(url);
            }
        }
    }
}

using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumTest.CIA
{
    class CIAWorldFactbook
    {
        SeleniumService service;
        string worldFactbookUrl;
        RegionalAndWorldMaps worldMaps;
        FlagsOfTheWorld flagsOfTheWorld;

        public CIAWorldFactbook(SeleniumService service)
        {
            this.service = service;
        }

        public void Run()
        {
            LoadFromGoogle();
            ReadButtonUrls();
        }

        private void LoadFromGoogle()
        {
            service.Navigate("http:www.google.com");
            var element = service.GetElementByName("q");

            element.SendKeys("world cia factbook");
            element.Submit();

            // Get the first link which is cia
            worldFactbookUrl = service.GetHref(@"//*[@id='rso']/div/div/div[1]/div/div/h3/a");

            service.Navigate(worldFactbookUrl);
        }

        private void ReadButtonUrls()
        {
            foreach(var element in service.GetElements(@"//*[@id='buttons']/ul/li"))
            {
                var alinkhref = element.FindElement(By.XPath("a")).GetAttribute("href");
                var helpText = element.FindElement(By.XPath("a/img")).GetAttribute("alt");
                var fullUrl = new Uri(new Uri(worldFactbookUrl), alinkhref);

                Process(helpText, alinkhref);
            }
        }

        private void Process(string helpText, string url)
        {
            if (helpText.Contains("Maps"))
            {
                worldMaps = new RegionalAndWorldMaps(service);
                worldMaps.Load(url);
            }
            else if (helpText.Contains("Flags"))
            {
                flagsOfTheWorld = new FlagsOfTheWorld(service);
                flagsOfTheWorld.Load(url);
            }
        }
    }
}

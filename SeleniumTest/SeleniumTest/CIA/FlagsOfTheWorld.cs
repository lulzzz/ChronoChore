using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumTest.CIA
{
    class FlagsOfTheWorld
    {
        SeleniumService service;
        string urlHome;

        public FlagsOfTheWorld(SeleniumService service)
        {
            this.service = service;
        }

        public void Load(string url)
        {
            urlHome = url;
            service.Navigate(url);

            foreach(var card in service.GetElements(@"//*[@id='GetAppendix_Z']/li"))
            {
                var flagImg = card.FindElement(By.XPath(@"table//tr[2]/td/img"));
                var flagUrl = flagImg.GetAttribute("src");
                var fullFlagUrl = new Uri(new Uri(urlHome), flagUrl).AbsolutePath;

                flagImg.Click();
            }
        }
    }
}

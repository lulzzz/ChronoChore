using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountryInformationDB.CIA
{
    class FlagsOfTheWorld : SingleWorkflowInSelenium
    {
        public FlagsOfTheWorld() : base(@"Flags") { }

        public override void Load(string url)
        {
            wfUrl = url;
            service.Navigate(wfUrl);

            foreach(var card in service.GetElementsXPath(@"//*[@id='GetAppendix_Z']/li"))
            {
                var flagImg = card.FindElement(By.XPath(@"table//tr[2]/td/img"));
                var flagUrl = flagImg.GetAttribute("src");
                var fullFlagUrl = new Uri(new Uri(wfUrl), flagUrl).AbsolutePath;

                flagImg.Click();
            }
        }
    }
}

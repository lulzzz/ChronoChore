using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountryInformationDB
{
    public class SeleniumService : IDisposable
    {
        private IWebDriver driver;

        public IWebDriver Driver { get { return driver; } }

        public SeleniumService()
        {
            Initialize();
        }

        public void Dispose()
        {
            if (driver != null) driver.Close();
        }

        public void Initialize()
        {
            driver = new ChromeDriver(@"D:\Google\chromedriver_win32");
        }

        public void Navigate(string url)
        {
            driver.Navigate().GoToUrl(url);
        }

        public string GetRootUrlFromGoogle(string searchText, int index = 1)
        {
            Navigate("http:www.google.com");
            var element = driver.FindElement(By.Name("q"));

            element.SendKeys("world cia factbook");
            element.Submit();

            // Get the first link which is cia
            var alinkElement = driver.FindElement(By.XPath(string.Format(@"//*[@id='rso']/div/div/div[{0}]/div/div/h3/a", index)));
            return alinkElement.GetAttribute("href");
        }

        public IList<IWebElement> GetElementsXPath(string xpath)
        {
            return driver.FindElements(By.XPath(xpath));
        }

        public IWebElement GetElementXPath(string xpath)
        {
            return driver.FindElement(By.XPath(xpath));
        }
    }
}

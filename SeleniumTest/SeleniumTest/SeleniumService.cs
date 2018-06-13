using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumTest
{
    public class SeleniumService : IDisposable
    {
        public IWebDriver driver;

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

        public IWebElement GetElementByName(string name)
        {
            return driver.FindElement(By.Name(name));
        }
        
        public string GetHref(string xpath)
        {
            var ciaLinkElement = driver.FindElement(By.XPath(xpath));
            return ciaLinkElement.GetAttribute("href");
        }

        public IList<IWebElement> GetElements(string xpath)
        {
            return driver.FindElements(By.XPath(xpath));
        }
    }
}

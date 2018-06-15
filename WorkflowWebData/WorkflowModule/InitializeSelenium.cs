using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Chrome;

namespace WorkflowModule
{
    public class InitializeSelenium : OnlineWorkflowStep
    {
        public override void Run()
        {
            Driver = new ChromeDriver(@"D:\Google\chromedriver_win32");
        }
    }
}

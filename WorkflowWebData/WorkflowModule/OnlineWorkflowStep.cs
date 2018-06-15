using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace WorkflowModule
{
    public class OnlineWorkflowStep : WorkflowStep
    {
        public IWebDriver Driver { get; protected set; }
    }
}

using AngleSharp.Parser.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebScrap.Common;
using WebScrap.LibExtension.XPath;

namespace AsyncAwaitDemo
{
    public class DownloadWebPage
    {
        public void Run(string url)
        {
            var httpText = new HttpRequestAndLoad().Load(url);
            var rootElement = new HtmlParser().Parse(httpText).DocumentElement;

            var scriptTags = rootElement.SelectNodes("script");
        }
    }
}

using AngleSharp.Dom;
using AngleSharp.Parser.Html;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebScrap.Common;
using WebScrap.LibExtension.XPath;

namespace AsyncAwaitDemo
{
    class Program
    {
        protected static ILog logger = LogManager.GetLogger(typeof(Program));

        static void Main(string[] args)
        {
            logger.Info("Logger started");

            var pageUrl = "https://en.wikipedia.org/wiki/Gallery_of_sovereign_state_flags";
            var httpDownload = new HttpRequestAndLoad();
            string httpText = httpDownload.Load(pageUrl);

            var httpParser = new HtmlParser();
            var element = httpParser.Parse(httpText).DocumentElement;

            var flagsHref = element.SelectNodes(@"//*[@id='mw-content-text']/div/table//table//tr[1]/td[1]/a/img");

            // Async Timer
            using (var timer = new LogWrapTimer())
            {
                var downloadQueue = new DownloadFileQueue()
                {
                    Extension = ".jpg",
                    LocalFolder = "Flags",
                    AsyncMode = false
                };

                downloadQueue.Setup();

                foreach (IElement flagHref in flagsHref)
                {
                    var urlRelative = flagHref.GetAttribute("src");
                    var urlAbsolute = new Uri(new Uri(pageUrl), urlRelative);

                    downloadQueue.AddTask(urlAbsolute.AbsoluteUri);
                }

                downloadQueue.Wait();
            }
        }
    }
}

using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumTest.CIA
{
    class RegionalAndWorldMaps
    {
        SeleniumService service;
        string url;
        List<Tuple<string, string>> countryMapsUrlHash;
        Dictionary<string, Tuple<string, string, string, string>> countryMaps;

        public RegionalAndWorldMaps(SeleniumService service)
        {
            this.service = service;
        }

        public void Load(string url)
        {
            service.Navigate(url);
            var mapsUrl = service.GetHref(@"//*[@id='refmaps']/div[1]/table/tbody/tr/td/div/a[2]");
            var fullMapsUrl = new Uri(new Uri(url), mapsUrl).AbsolutePath;
            service.Navigate(fullMapsUrl);

            countryMapsUrlHash = new List<Tuple<string, string>>();

            foreach(var element in 
                service.GetElements(@"//*[@id='ciaSelectPublication']/fieldset/select/option[position() > 1]"))
            {
                var urlcountryMap = element.GetAttribute("value");
                var fullurlcountryMap = new Uri(new Uri(fullMapsUrl), urlcountryMap).AbsolutePath;
                var country = element.Text;

                countryMapsUrlHash.Add(new Tuple<string, string>(
                    country, fullurlcountryMap));
            }

            ProcessMaps();
        }

        private void ProcessMaps()
        {
            countryMaps = new Dictionary<string, Tuple<string, string, string, string>>();
            foreach (var item in countryMapsUrlHash)
            {
                service.Navigate(item.Item2);
                var elements = service.GetElements(@"//*[@id='parent - fieldname - text - e8a628ac06aa9ff85df2e134189f0104']/table/tbody/tr[position() > 1]");

                foreach(var trow in elements)
                {
                    var tdataList = trow.FindElements(By.XPath(@"td"));
                    var jpegHref = tdataList[3].FindElement(By.XPath(@"b/a")).GetAttribute("href");
                    var fullJpegHref = new Uri(new Uri(url), jpegHref).AbsolutePath;

                    Directory.CreateDirectory(@"Country\CIA\Maps");
                    var download = new HttpDownloadFile(@"Country\CIA\Maps");
                    download.Download(fullJpegHref, true, ImageFormat.Jpeg);

                    var maps = new Tuple<string, string, string, string>(
                        tdataList[0].Text,
                        tdataList[1].Text, fullJpegHref, download.LocalFile
                    );

                    countryMaps.Add(item.Item1, maps);
                }
            }
        }
    }
}

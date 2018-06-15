using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountryInformationDB.CIA
{
    class RegionalAndWorldMaps : SingleWorkflowInSelenium
    {
        List<Tuple<string, string>> countryMapsUrlHash;
        Dictionary<string, List<Tuple<string, string, string, string>>> countryMaps;

        public RegionalAndWorldMaps() : base(@"Maps") { }
        
        public override void Load(string url)
        {
            if(!Directory.Exists(fullRootWfDirectory))Directory.CreateDirectory(fullRootWfDirectory);

            wfUrl = url;
            service.Navigate(url);

            // Navigate to available CIA Maps
            var aviableMapsUrl = service.GetElementXPath(@"//*[@id='refmaps']/div[1]/table/tbody/tr/td/div/a[2]").GetAttribute("href");
            service.Navigate(aviableMapsUrl);
            wfUrl = aviableMapsUrl;

            // Get list of country names and maps links
            GetListofCountryAndMapsLinks();
            
            ProcessMaps();
        }

        private void GetListofCountryAndMapsLinks()
        {
            countryMapsUrlHash = new List<Tuple<string, string>>();
            var optionsCountryMapsLinks = service.GetElementsXPath(@"//*[@id='ciaSelectPublication']/fieldset/select/option[position() > 1]");

            foreach (var element in optionsCountryMapsLinks)
            {
                countryMapsUrlHash.Add(new Tuple<string, string>(
                    element.Text, UriEx.Full(wfUrl, element.GetAttribute("value"))));
            }
        }

        private void ProcessMaps()
        {
            countryMaps = new Dictionary<string, List<Tuple<string, string, string, string>>>();
            foreach (var item in countryMapsUrlHash)
            {
                service.Navigate(item.Item2);
                var elements = service.GetElementsXPath(@"//*[@id='content-core']/div[2]/table/tbody/tr[position() > 1]");
                var listMaps = new List<Tuple<string, string, string, string>>();

                foreach (var trow in elements)
                {
                    var tdataList = trow.FindElements(By.XPath(@"td"));
                    var jpegHref = tdataList[4].FindElement(By.XPath(@".//a")).GetAttribute("href");
                    var fullJpegUrl = UriEx.Full(item.Item2, jpegHref);
                    
                    var download = new HttpDownloadFile(fullRootWfDirectory);
                    download.Download(fullJpegUrl, true, ImageFormat.Jpeg);

                    var maps = new Tuple<string, string, string, string>(
                        tdataList[0].Text,
                        tdataList[1].Text, fullJpegUrl, download.LocalFile
                    );

                    listMaps.Add(maps);
                }

                countryMaps.Add(item.Item1, listMaps);
            }
        }
    }
}

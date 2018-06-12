using AngleSharp.Dom;
using AngleSharp.Dom.Html;
using AngleSharp.Parser.Html;
using CommonLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountryDataGenerator.CIA.Maps
{
    public class AvailableCiaMaps
    {
        private string rootUrl = @"https://www.cia.gov/library/publications/resources/cia-maps-publications/index.html";
        IHtmlDocument htmlDocument;
        IElement currentElement;

        List<Tuple<string, string>> countryList;
        Dictionary<string, List<Tuple<string, string, string>>> countrySpecificMaps;

        public void Run()
        {
            LoadIndexPage();
            ReadCountryList();
            DownloadMaps();
        }

        private void LoadIndexPage()
        {
            string htmlText = new HttpRequestAndLoad().Load(rootUrl);
            var htmlParser = new HtmlParser();
            htmlDocument = htmlParser.Parse(htmlText);
        }

        private IElement LoadCountryMapPage(string countryUrl)
        {
            string htmlText = new HttpRequestAndLoad().Load(countryUrl);
            var htmlParser = new HtmlParser();
            return htmlParser.Parse(htmlText).DocumentElement;
        }

        private void ReadCountryList()
        {
            currentElement = htmlDocument.DocumentElement;
            var listOptions = 
                currentElement.SelectNodes(@"//*[@id='ciaSelectPublication']/fieldset/select/option[position()>1]");

            countryList = new List<Tuple<string, string>>();
            foreach (IElement option in listOptions)
            {
                var fullUri = new Uri(
                    new Uri(rootUrl), option.Attributes["value"].Value);
                var data = new Tuple<string, string>(
                    option.NodeValue,
                    fullUri.AbsoluteUri);
                countryList.Add(data);
            }
        }

        private void DownloadMaps()
        {
            countrySpecificMaps = new Dictionary<string, List<Tuple<string, string, string>>>();
            foreach (var optionCountry in countryList)
            {
                DownloadMapTypes(optionCountry);
            }
        }

        private void DownloadMapTypes(Tuple<string, string> optionCountry)
        {
            var element = LoadCountryMapPage(optionCountry.Item2);
            var maptablerowsElement = element.SelectNodes(
                @"//*[starts-with(@id, 'parent - fieldname - text - ')]/table//tr[position() > 1]");
        }
    }
}

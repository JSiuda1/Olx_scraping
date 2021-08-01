using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Olx_scraping
{
    class WebScrap
    {
        private const string url = "https://www.olx.pl/nieruchomosci/mieszkania/wroclaw/q-mieszkanie-3-pokojowe/?search%5Bfilter_float_price%3Ato%5D=3000";

        public List<RoomsInfo> GetAnnouncments3Rooms()
        {
            List<RoomsInfo> flats = new List<RoomsInfo>();
            
            var web = new HtmlWeb();
            var doc = web.Load(url);

            var boxs = doc.QuerySelectorAll("#offers_table > tbody tr").Skip(2);
            

            foreach (var elem in boxs)
            {
                try
                {
                    var title = elem.QuerySelector("td > div > table > tbody > tr:nth-child(1) > td.title-cell > div > h3 > a > strong").InnerText;
                    var price = elem.QuerySelector("td > div > table > tbody > tr:nth-child(1) > td.wwnormal.tright.td-price > div > p > strong").InnerText;
                    var region = elem.QuerySelector("td > div > table > tbody > tr:nth-child(2) > td.bottom-cell > div > p > small:nth-child(1) > span ").InnerText.Split(", ").Last();
                    var date = elem.QuerySelector("td > div > table > tbody > tr:nth-child(2) > td.bottom-cell > div > p > small:nth-child(2) > span").InnerText;
                    var url = elem.QuerySelector("td > div > table > tbody > tr:nth-child(1) > td.title-cell > div > h3 > a").Attributes["href"].Value;
                    flats.Add(new RoomsInfo(title, price, region, date, url));
                }
                catch
                {
                    //a po co
                }
            }

            return flats;
        }
    }
}

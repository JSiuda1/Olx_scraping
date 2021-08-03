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
        public List<string> urls { get; set; }
        
        public WebScrap(List<string> _urls)
        {
            urls = _urls;
        }

        public void AddUrl(string url)
        {
            urls.Add(url);
        }
        
        public List<RoomsInfo> GetAnnouncmentsRooms()
        {
            List<RoomsInfo> flats = new List<RoomsInfo>();
            
            foreach (var url_elem in urls)
            {

                var web = new HtmlWeb();
                var doc = web.Load(url_elem); 

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
            }

            return flats;
        }
    }
}

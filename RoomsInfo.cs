using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Olx_scraping
{
    class RoomsInfo
    {
        public string Title { get; set; }
        public string Price { get; set; }
        public string Region { get; set; } 
        public string Date { get; set; }
        public string Url { get; set; }

        public RoomsInfo(string title, string price, string region, string date, string url)
        {
            Title = title;
            Price = price;
            Region = region;
            Date = date;
            Url = url;
        }
    }
}

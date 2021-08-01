using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Olx_scraping
{
    class Program
    {
        static async Task Main(string[] args)
        {   
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Start!");

            WebScrap olx = new WebScrap();
            Flats flats = new Flats(olx.GetAnnouncments3Rooms());
            
            while (true)
            {
                flats.DisplayAll();
                await Task.Delay(new TimeSpan(0, 1, 0));
            }
        }
    }
}

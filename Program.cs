using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Olx_scraping
{
    class Program
    {
        static async Task Main(string[] args)
        {
            List<string> urls = new List<string>
            {
                "https://www.olx.pl/nieruchomosci/mieszkania/wroclaw/q-mieszkanie-3-pokojowe/?search%5Bfilter_float_price%3Ato%5D=3000",
                "https://www.olx.pl/nieruchomosci/mieszkania/wroclaw/q-mieszkanie-2-pokojowe/?search%5Bfilter_float_price%3Ato%5D=2000",
                "https://www.olx.pl/nieruchomosci/mieszkania/wynajem/wroclaw/?search[filter_enum_furniture][0]=yes&search[filter_enum_rooms][0]=three",
            };

            DateTime lastReloadTime = DateTime.Now;

            WebScrap olx = new WebScrap(urls);
            Flats flats = new Flats();

            flats.Flats_list = olx.GetAnnouncmentsRooms();
            flats.Flats_list = filter(flats);

            flats.DisplayAll();
            
            while (true)
            {
                await Task.Delay(new TimeSpan(0, 15, 0));

                Flats new_announcments = new Flats();
                List<RoomsInfo> new_rooms;
                
                DateTime now = DateTime.Now;
                if(lastReloadTime.Day != now.Day)
                {
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    Console.Write("Nowy dzień! Resetujemy pamięć");
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.WriteLine();
                    flats.Refresh();
                    flats.Flats_list = olx.GetAnnouncmentsRooms();
                    flats.Flats_list = filter(flats);
                    flats.DisplayAll();
                    new_rooms = null;
                }
                else
                {
                    new_announcments.Flats_list = olx.GetAnnouncmentsRooms();
                    new_announcments.Flats_list = filter(new_announcments);
                    new_rooms = flats.CheckNewAnnouncments(new_announcments.Flats_list);
                    Console.Write($"Ostatnia aktualizacja {now}");
                    if (new_rooms == null)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"\tNic nowego :/");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"\tLiczba nowych ogłoszeń: {new_rooms.Count}");
                        Flats.DisplayAll(new_rooms);
                    }
                }                
                lastReloadTime = now;

                Console.ForegroundColor = ConsoleColor.White;
            }
            
            static List<RoomsInfo> filter(Flats flats)
            {
                FlatsComparer flatsComparer = new FlatsComparer();

                flats.Flats_list = flats.Flats_list.Where(f => f.Date.Contains("dzisiaj") || f.Date.Contains("wczoraj"))  //Remove flats added later than 2 days
                 .OrderBy(f => f.Date.Split(" ").Last()) //Sorted time hh:mm
                 .OrderByDescending(f => f.Date.Split().First()) //Sorted name (dzisiaj, wczoraj)
                 .Distinct(flatsComparer)
                 .ToList();

                return flats.Flats_list;
            }
        }
    }
}

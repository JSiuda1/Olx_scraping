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
            const string url1 = "https://www.olx.pl/nieruchomosci/mieszkania/wroclaw/q-mieszkanie-3-pokojowe/?search%5Bfilter_float_price%3Ato%5D=3000";
            const string url2 = "https://www.olx.pl/nieruchomosci/mieszkania/wroclaw/q-mieszkanie-2-pokojowe/?search%5Bfilter_float_price%3Ato%5D=2000";
            const string url3 = "https://www.olx.pl/nieruchomosci/mieszkania/wynajem/wroclaw/?search[filter_enum_furniture][0]=yes&search[filter_enum_rooms][0]=three";

            WebScrap olx = new WebScrap();
            Flats flats = new Flats();

            flats.ConcatenateLists(olx.GetAnnouncmentsRooms(url1));
            flats.ConcatenateLists(olx.GetAnnouncmentsRooms(url2));
            flats.ConcatenateLists(olx.GetAnnouncmentsRooms(url3));

            //Some time filter
            flats.Flats_list = filter(flats);

            flats.DisplayAll();
            
            while (true)
            {
                await Task.Delay(new TimeSpan(0, 15, 0));

                Flats new_announcments = new Flats();
                new_announcments.ConcatenateLists(olx.GetAnnouncmentsRooms(url1));
                new_announcments.ConcatenateLists(olx.GetAnnouncmentsRooms(url2));
                new_announcments.ConcatenateLists(olx.GetAnnouncmentsRooms(url3));


                new_announcments.Flats_list = filter(new_announcments);

                List<RoomsInfo> new_rooms = flats.CheckNewAnnouncments(new_announcments.Flats_list);
                Console.Write($"Ostatnia aktualizacja {DateTime.Now}");
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
                Console.ForegroundColor = ConsoleColor.White;
            }
            
            static List<RoomsInfo> filter(Flats flats)
            {
                flats.Flats_list = flats.Flats_list.Where(f => f.Date.Contains("dzisiaj") || f.Date.Contains("wczoraj")) //Remove flats added later than 2 days
                 .OrderBy(f => f.Date.Split(" ").Last()) //Sorted time hh:mm
                 .OrderByDescending(f => f.Date.Split().First()) //Sorted name (dzisiaj, wczoraj)
                 .ToList();

                return flats.Flats_list;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Olx_scraping
{
    class Flats
    {
        public List<RoomsInfo> Flats_list { get; set; }

        public Flats()
        {
            Flats_list = new List<RoomsInfo>();
        }

        public Flats(List<RoomsInfo> flats)
        {
            Flats_list = flats;
        }

        public List<RoomsInfo> CheckNewAnnouncments(List<RoomsInfo> roomsInfos)
        {
            List<RoomsInfo> newFlats = new List<RoomsInfo>();
            //Linq except not work well :C
            if(roomsInfos.Count == Flats_list.Count)
            {
                return default;
            }
            else
            { 
                for(int i = Flats_list.Count; i < roomsInfos.Count; ++i)
                {
                    Flats_list.Add(roomsInfos[i]);
                    newFlats.Add(roomsInfos[i]);
                }
                return newFlats;
            }

        }

        public void Refresh()
        {
            Flats_list.Clear();
        }
        
        public void ConcatenateLists(List<RoomsInfo> second_list)
        {
            foreach (var elem in second_list)
            {
                Flats_list.Add(elem);
            }
        }

        public void DisplayAll()
        {
            Display(Flats_list);
        }

        public static void DisplayAll(List<RoomsInfo> infos)
        {
            Display(infos);
        }

        private static void Display(List<RoomsInfo> flats)
        {
            foreach (RoomsInfo flat in flats)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("----------------------------------------------------------------------");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Opis: ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(flat.Title);

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Cena: ");

                string[] priceElems = flat.Price.Split(" ");
                string cashstring = ""; 
                for (int i=0; i< priceElems.Length - 1; ++i)
                {
                    cashstring += priceElems[i];
                }
                if(int.Parse(cashstring) > 3000)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.Write(flat.Price);

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("\t Okolica: ");
                if(flat.Region.Contains("Śródmieście") ^ flat.Region.Contains("Stare Miasto"))
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                }

                Console.Write(flat.Region);
                Console.ForegroundColor = ConsoleColor.Yellow;

                Console.Write("\t Data dodania: ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(flat.Date);
                Console.ForegroundColor = ConsoleColor.Yellow;

                Console.Write("Link: ");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($"{flat.Url}");
            }
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("----------------------------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}

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

        public Flats(List<RoomsInfo> flats)
        {
            Flats_list = flats;
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
            foreach (RoomsInfo flat in Flats_list)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("----------------------------------------------------------------------");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Opis: ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(flat.Title);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Cena: ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(flat.Price);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("\t Okolica: ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(flat.Region);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("\t Data dodania: ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(flat.Date);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Link: ");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($" {flat.Url}");
            }
        }
    }
}

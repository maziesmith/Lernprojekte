using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Linq;

namespace Speisen
{
    class Program
    {
        static void Main()
        {
            // in diesem Beispiel werden Gruppen angezeigt u. innerhalb der Gruppe die Details
            Speise[] speisen = new Speise[]
            { 
                new Speise() { Name = "Broccoli gratiniert", KiloKalorien = 216 },
                new Speise() { Name = "Nudelsalat", KiloKalorien = 506 },
                new Speise() { Name = "Mozzarella mit Tomaten", KiloKalorien=300 },
                new Speise() { Name = "Schnitzel mit Pommes", KiloKalorien=518+320},
                new Speise() { Name = "Gefüllte Peperoni", KiloKalorien=182},
                new Speise() { Name = "Spaghetti mit Tomatensauce",KiloKalorien=333}
            };

            var katSpeisen = from s in speisen
                             group s by s.KiloKalorien > 300;

            foreach (var gruppen in katSpeisen)
            {
                Console.WriteLine(gruppen.Key ? "\nLeckere Speise" : "\nDiättaugliche Speise");
                foreach (var speise in gruppen)
                {
                    Console.WriteLine("{0}kcal\t{1}", speise.KiloKalorien, speise.Name);
                }
            }

            Console.ReadKey();
        }

        static void PrintNamesToConsole(List<string> names)
        {
            foreach (string name in names)
            {
                Console.WriteLine(name);
            }
        }
    }

    class Speise
    {
        public int KiloKalorien { get; set; }
        public string Name { get; set; }

    }
}
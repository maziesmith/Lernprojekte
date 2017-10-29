using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lernen1
{
    class Program
    {
        static void Main(string[] args)
        {
            Person p1 = new Person();
            Person p2 = new Person();
            p1.Name = "Name1";
            p2.Name = "Name2";
            p1.Walk(1500);
            p2.Walk(1800);
            p2.Walk(1300);
            Person[] persons = new Person[] { p1, p2 };
            foreach(Person p in persons)
                Console.WriteLine("Schritte von {0}={1}, km={2}", p.Name, p.Footsteps, p.WalkedKm);
            Console.WriteLine("Euer Durchschnitt={0} km", Person.AverageWalkedKm(persons));
            Console.ReadKey();
        }

        class Person
        {
            public string Name { get; set; }

            public float WalkedKm
            { get { return this.Footsteps * 0.75F / 1000F; } }

            public float Weight { get; set; }
            public int Footsteps { get; private set; }

            public void Walk(int Footsteps)
            { if (Footsteps > -1) this.Footsteps += Footsteps; }

            public static float AverageWalkedKm(Person[] persons)
            {
                int amount = persons.Length;
                float walkSum = 0;
                foreach (Person p in persons) walkSum += p.WalkedKm;
                return walkSum / (float)amount;
            }
        }
    }
}

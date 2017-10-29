using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lernen9
{
    class Program
    {
        static void Main(string[] args)
        {
            Ghost Geist = new Ghost("felix");
            Geist.Haunt();

            SlimeGhost Schleimi = new SlimeGhost("Simon");
            Schleimi.Haunt();
            
            SlimeGhost Smeargol = new SlimeGhost("Mammi");
            Smeargol.Size = 9;
            Smeargol.Haunt();
            Console.WriteLine(Smeargol);

            CanibalGhost fressi = new CanibalGhost("Deddi");
            fressi.Size = 5;
            fressi.Eat(ref Smeargol);
            Console.WriteLine("Die Größe von {0}={1}", fressi.Name, fressi.Size);
            if (Smeargol == null)
                Console.WriteLine("Mammi wurde gefressen!");
            else
                Console.WriteLine("{0} lebt noch!", Smeargol.Name);
            Console.ReadKey();
        }

        class Ghost
        {
            public Ghost(string _name)
            {
                Name = _name;
            }
            public string Name { get; set; }
            public int Size { get; set; }
            public virtual void Haunt()
            {
                Console.WriteLine("{0} sagt: 'Buh'", Name);
            }
        }

        class SlimeGhost : Ghost
        {
            public SlimeGhost(string _name) : base(_name) { }
            public void Slime()
            {
                Console.WriteLine("Schleim hinterlassen");
            }
            public override void Haunt()
            {
                Slime();
                base.Haunt();
            }
        }

        class CanibalGhost : Ghost
        {
            public CanibalGhost(string _name) : base(_name) { }
            public CanibalGhost() : base("") { }
            
            public void Eat(ref Ghost g)
            {
                IncreaseSize(10);
                g = null;
            }

            public void Eat(ref SlimeGhost g)
            {
                IncreaseSize(10);
                g = null;
            }

            public void Eat(ref CanibalGhost g)
            {
                IncreaseSize(10);
                g = null;
            }

            private void IncreaseSize(int size)
            {
                Size += size;
            }
        }
    }
}

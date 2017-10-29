using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Haus
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }

    public abstract class Ort
    {
        private string name;
        public Ort(string name)
        {
            this.name = name;
        }
        public Ort[] Ausgänge;
        public string Name
        {
            get { return name; }
        }
        public virtual string Beschreibung
        {
            get
            {
                string beschreibung = "Du stehst im" + name + ". Es gibt Türen zu folgenden Orten: ";
                for (int i = 0; i < Ausgänge.Length; i++)
                {
                    beschreibung += " " + Ausgänge[i].Name;
                    if (i != Ausgänge.Length - 1)
                        beschreibung += ",";
                }
                beschreibung += ".";
                return beschreibung;
            }
        }
    }

    public interface IHatAußentür
    {
        string TürBeschreibung { get; }
        Ort TürOrt { get; set; }
    }

    public class Zimmer : Ort
    {
        private string Dekoration;

        public Zimmer(string name, string dekoration) : base(name)
        {
            this.Dekoration = dekoration;
        }

        public override string Beschreibung
        {
            get { return base.Beschreibung + " Du siehst " + Dekoration + "."; }
        }
    }

    public class ZimmerMitTür : Zimmer, IHatAußentür
    {
        public ZimmerMitTür(string name, string dekoration, string türBeschreibung) : base(name, dekoration)
        {
            this.türBeschreibung = türBeschreibung;
        }

        private string türBeschreibung;

        public string TürBeschreibung
        {
            get { return türBeschreibung; }
        }

        private Ort türOrt;

        public Ort TürOrt
        {
            get { return türOrt; }
            set { türOrt = value; }
        }

        public class Gelände: Ort
        {
            private bool heiß;
            public Gelände(string name, bool heiß) : base(name)
            {
                this.heiß = heiß;
            }

            public override string Beschreibung
            {
                get
                {
                    string neueBeschreibung = base.Beschreibung;
                    if (heiß)
                        neueBeschreibung += " Es ist sehr heiß.";
                    return neueBeschreibung;
                }
            }
        }

        public class AußenMitTür : Gelände, IHatAußentür
        {
            public AußenMitTür(string name, bool heiß, string türBeschreibung) : base(name, heiß)
            {
                this.türBeschreibung = türBeschreibung;
            }

            private string türBeschreibung;
            public string TürBeschreibung
            { get { return türBeschreibung; } }

            private Ort türOrt;
            public Ort TürOrt
            {
                get { return türOrt; }
                set { türOrt = value; }
            }

            public override string Beschreibung
            {
                get { return base.Beschreibung + " Du siehst " + türBeschreibung + "."; }
            }
        }
    }


}

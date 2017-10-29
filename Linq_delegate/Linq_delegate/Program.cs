using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq_delegate
{
	class Program
	{
		static void Main(string[] args)
		{
			Bar bar = new Bar();
			Person besucher1 = new Person() { Name = "Walter" };
			Person besucher2 = new Person() { Name = "Sepp" };
			besucher1.Eintreten(bar);
			Console.WriteLine("{0} betritt die Bar", besucher1.Name);
			besucher2.Eintreten(bar);
			Console.WriteLine("{0} betritt die Bar", besucher2.Name);
			Console.WriteLine("Die Bar gibt eine Runde aus", besucher1.Name);
			bar.RundeAusgeben();
			Console.ReadKey();
			besucher1.Verlassen(bar);
			Console.WriteLine("{0} verlässt die Bar", besucher1.Name);
			Console.WriteLine("Die Bar gibt eine Runde aus", besucher1.Name);
			bar.RundeAusgeben();
			Console.ReadKey();
		}

		public class Bar
		{
			public event EventHandler EsWirdEineRundeAusgegeben;
			public void RundeAusgeben()
			{
				if (EsWirdEineRundeAusgegeben != null)
					EsWirdEineRundeAusgegeben(this, EventArgs.Empty);
			}
		}

		class Person
		{
			public string Name { get; set; }
			public void Eintreten(Bar bar)
			{
				bar.EsWirdEineRundeAusgegeben += RundeEmpfangen;
			}
			public void Verlassen(Bar bar)
			{
				bar.EsWirdEineRundeAusgegeben -= RundeEmpfangen;
			}
			public void RundeEmpfangen(object sender, EventArgs e)
			{
				Console.WriteLine("{0} freut sich über ein Getränk!", Name);
			}
		}
	}
}

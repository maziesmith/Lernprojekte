using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq_einfache_Gruppierung
{
	class Program
	{
		static void Main(string[] args)
		{
			Product[] products = new Product[] {
				new Product() { Name = "Schreckensmaske", CategoryId=2 },
				new Product() { Name = "Elitedrachen-Stulpenhandschuhe", CategoryId=2 },
				new Product() { Name = "Lindwurmhaut", CategoryId=1 },
				new Product() { Name = "Runenstoffstück", CategoryId=3 },
				new Product() { Name = "Luft-Zauberstab", CategoryId=4 }
			};

			Category[] cats = new Category[] {
				new Category() { Id=1, Name="Handwerkszeug" },
				new Category() { Id=2, Name="Rüstung" },
				new Category() { Id=3, Name="Kleidung" }
			};

			var result = from p in products
						 join c in cats on p.CategoryId equals c.Id
						 orderby p.Name
						 select new
						 {
							 ProductName = p.Name,
							 CategoryName = c.Name,
							 CategoryId = c.Id
						 };

			foreach (var item in result)
			{
				Console.WriteLine("Prodult: {0}; Kategorie: {1} (ID: {2})",
					item.ProductName, item.CategoryName, item.CategoryId);
			}

			/*Console.ReadKey(); */

			Console.WriteLine("\n\n\n");

			var result1 = from c in cats
						  join p in products on c.Id equals p.CategoryId into prodinf
						  select new
						  {
							  Category = c.Name,
							  Products = prodinf
						  };

			foreach (var item in result1)
			{
				Console.WriteLine("Kategorie: {0}", item.Category);
				foreach (Product p in item.Products)
					Console.WriteLine("\tProdukt: {0}", p.Name);
			}


			/*Console.ReadKey(); */

			Console.WriteLine("\n\n\n");

			var result2 = from p in products
						  join c in cats on p.CategoryId equals c.Id into catinfo
						  from item in catinfo.DefaultIfEmpty(new Category())
						  select new
						  {
							  ProductName = p.Name,
							  CategoryName = item.Name,
							  CategoryId = item.Id
						  };

			foreach (var item in result2)
			{
				Console.WriteLine("Kategorie: {0}\tProduct: {1}", item.CategoryName, item.ProductName);
			}

			Console.WriteLine("\n\n\n");

			var grouped = from rekord in result2
						  orderby rekord.ProductName
						  group rekord by rekord.ProductName[0];


			/*Console.ReadKey(); */

			var result3 = from p in products
						  orderby p.Name
						  group p by p.Name[0];

			foreach (var item in result3)
			{
				Console.WriteLine("Kategorie: {0}", item.Key);
				foreach (Product p in item)
				{
					Console.WriteLine("Produkt: {0}", p.Name);
				}
			}

			/*Console.ReadKey(); */

			Console.WriteLine("\n\n\n");

			int prodnr = 1;
			var result4 = from p in products
						  select new
						  {
							  nr = prodnr++,
							  p.Name
						  };

			foreach (var item in result4)
				Console.WriteLine("artnr: {0}\tprodukt: {1}", item.nr, item.Name);

			Console.ReadKey();




			/*Fragen: */
			/*- wie kann man das result auslesen ohne foreach */
























			Speise[] speisen = new Speise[] {
				new Speise() { Name = "Broccoli gratiniert", KiloKalorien = 216 },
				new Speise() { Name = "Nudelsalat", KiloKalorien = 506 },
				new Speise() { Name = "Mozzarella mit Tomaten", KiloKalorien = 500 },
				new Speise() { Name = "Schnitzel mit Pommes", KiloKalorien = 518 },
				new Speise() { Name = "Gefüllte Peperoni", KiloKalorien = 182 },
				new Speise() { Name = "Spaghetti mit Tomatensauce", KiloKalorien = 233 }
			};

			var katSpeisen = from s in speisen
							 group s by s.KiloKalorien.ToString().Substring(1, 1);

			foreach (var gruppe in katSpeisen)
			{
				/*Console.WriteLine(gruppe.Key ? "\nLeckere Speise" : "\nDiättaugliche Speise");*/
				Console.WriteLine(gruppe.Key.ToString());
				foreach (var speise in gruppe)
					Console.WriteLine("{0} kcal\t{1}", speise.KiloKalorien, speise.Name);
			}
		}
	}

	class Product
	{
		public string Name { get; set; }
		public int CategoryId { get; set; }
	}

	class Category
	{
		public int Id { get; set; }
		public string Name { get; set; }
	}

	class Speise
	{
		public int KiloKalorien { get; set; }
		public string Name { get; set; }
	}


}

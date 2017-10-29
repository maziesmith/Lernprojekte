using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.Xml.Linq;
using System.Xml;

namespace linq2xml
{
	class Program
	{
		static void Main(string[] args)
		{
			string xmlFileName = @"D:\Downloads\Rupi\t23976735_klettersteig bolver-lugli in der palagruppe.gpx";
			XDocument gpxXML = XDocument.Load(xmlFileName);

			var results = from gp in gpxXML.Root.Elements()
						  select gp; //.LastNode;


			var results2 = from gp in results.Last().Elements()
						   select gp;

			var results3 = from gp in results2.Last().Elements()
						   select gp;

			// in Kurzform: var results = from gp in gpxXML.Root.Elements().Last().Elements().Last().Elements() select gp;

			//foreach (var item in results3)
			//{
			//	Console.WriteLine("{0}\t{1}\t{2}", item.Attribute("lat").Value, item.Attribute("lon").Value, item.Value);
			//}
			//Console.ReadKey();

			XmlDocument gpx = new XmlDocument();
			gpx.Load(xmlFileName);

			// so ungefähr irgendwie ...
			var nodes = gpx.SelectNodes("descendant");
			foreach (XmlNode node in nodes)
			{
				Console.WriteLine("{0}\t{1}\t{2}", node.Attributes["lat"].InnerText, node.Attributes["lon"].InnerText, node.InnerText);
			}

			Console.ReadKey();






		}
	}
}

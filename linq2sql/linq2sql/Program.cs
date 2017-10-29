using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Data.SqlClient;


namespace linq2sql
{
	class Program
	{
		static void Main(string[] args)
		{
			// (false) nur lesen, (true) lesen und schreiben
			using (var context = new MyDataContext(true))
			{
				// Liste von Kunden
				List<Customers> liste = context.Customers.Where(x => x.CustomerID == "1" && x.Country == "bla").ToList();

				// einzelnes Feld
				string customername = context.Customers.Where(x => x.CustomerID == "ALFKI").SingleOrDefault().ContactName;

				string customername2 = (from c in context.Customers
										where c.CustomerID == "1"
										select c.ContactName).ToString();

				// update-Funktionalität
				//foreach (Customers cust in liste)
				//	cust.Region = "neu";
				//context.SubmitChangesAndResolveConflicts();
				// achtung: funktioniert so nicht, weil oben false bei using-Anweisung!!!


				// löschen-Funktionalität
				//foreach (Customers cust in liste)
				//	context.Customers.DeleteOnSubmit(cust);
				//// oder gesamte Liste löschen
				//context.Customers.DeleteAllOnSubmit(liste);
				//context.SubmitChangesAndResolveConflicts();

				////insert
				//Customers cust1 = new Customers();
				//cust1.Address = "kklk";
				////.....
				//context.Customers.InsertOnSubmit(cust1);
				//context.SubmitChangesAndResolveConflicts();


				//"SELECT t_cuno, LTRIM(t_nama) AS t_nama, LTRIM(CASE when t_fovn='' THEN t_crbu ELSE t_fovn END) AS t_fovn, " +
				//												"LTrim(t_ccty) As t_ccty, LTrim(t_name) As t_name, " +
				//				"LTrim(CASE LTRIM(t_clan) WHEN 'I' THEN 'IT' WHEN 'D' THEN 'DE' WHEN 'EN' THEN 'GB' ELSE LTRIM(t_clan) END) AS t_clan, " +
				//				"CASE when dbo.SearchText(t_txta, 'Email Rechnung:')='' THEN 'info@atzwanger.net' ELSE dbo.SearchText(t_txta, 'Email Rechnung:') END AS email, " +
				//				"CASE when dbo.SearchText(t_txta, 'Email Kanal:')='' THEN 'NOPEC' ELSE 'PEC' END AS typ, LTRIM(t_telp) AS t_telp " +
				//				"FROM ttccom010100 WHERE NOT([t_cuno] IN (SELECT dok_dat_feld_21 COLLATE database_default FROM [BAAN4\D3].d3p.dbo.firmen_spezifisch " +
				//				"WHERE kue_dokuart='AKUND'))

				var kunden = from c in context.Customers
							 where c.Country == "...."
							 select c;

				var result = from c in context.Customers
							 join o in context.Orders on c.CustomerID equals o.CustomerID into auftraege
							 from o2 in auftraege.DefaultIfEmpty()
							 join od in context.Order_Details on o2.OrderID equals od.OrderID
							 where o2 != null ? o2.OrderDate > new DateTime(1900, 1, 1) : false && !kunden.Contains(c)
							 select new
							 {
								 kundenname = c.ContactName,
								 c.CompanyName,
								 o2.OrderDate,
								 produkte = od.Products
							 };
				foreach (var item in result)
				{
					Console.WriteLine("{0} {1}", item.kundenname, item.OrderDate);
					
				}
				Console.ReadKey();
			}



			//string connString = @"Data Source=DESKTOPRUPI\SQLEXPRESS;Initial Catalog=Northwind;Integrated Security=True;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite";
			//SqlConnection nwindConn = new SqlConnection(connString);
			//nwindConn.Open();

			//Northwnd interop_db = new Northwnd(nwindConn);

			//SqlTransaction nwindTxn = nwindConn.BeginTransaction();

			//try
			//{
			//	SqlCommand cmd = new SqlCommand(
			//		"UPDATE Products SET QuantityPerUnit = 'single item' WHERE ProductID = 3");
			//	cmd.Connection = nwindConn;
			//	cmd.Transaction = nwindTxn;
			//	cmd.ExecuteNonQuery();

			//	interop_db.Transaction = nwindTxn;

			//	Product prod1 = interop_db.Products
			//		.First(p => p.ProductID == 4);
			//	Product prod2 = interop_db.Products
			//		.First(p => p.ProductID == 5);
			//	prod1.UnitsInStock -= 3;
			//	prod2.UnitsInStock -= 5;

			//	interop_db.SubmitChanges();

			//	nwindTxn.Commit();
			//}
			//catch (Exception e)
			//{
			//	Console.WriteLine(e.Message);
			//	Console.WriteLine("Error submitting changes... all changes rolled back.");
			//}

			//nwindConn.Close();
		}
	}
}

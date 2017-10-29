using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace linq2sql
{
	class MyDataContext : DBDataContext
	{
		private static MappingSource sharedMappingSource = new AttributeMappingSource();

		public static string DefaultConnectionString = @"Data Source=DESKTOPRUPI\SQLEXPRESS;Initial Catalog=Northwind;Integrated Security=True;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite";

		public MyDataContext(IDbConnection context, bool objectTrackingEnabled)
			: base(context, sharedMappingSource)
		{
			this.ObjectTrackingEnabled = objectTrackingEnabled;
			//this.Log = Console.Out;
		}

		public MyDataContext(bool objectTrackingEnabled)
			: base(DefaultConnectionString, sharedMappingSource)
		{
			this.ObjectTrackingEnabled = objectTrackingEnabled;
			//this.Log = Console.Out;
		}

		public void SubmitChangesAndResolveConflicts(RefreshMode mode)
		{
			try
			{
				this.SubmitChanges(ConflictMode.ContinueOnConflict);
			}
			catch (ChangeConflictException)
			{
				foreach (ObjectChangeConflict c in this.ChangeConflicts)
					c.Resolve(mode);
			}
			this.SubmitChanges(ConflictMode.FailOnFirstConflict);
		}

		public void SubmitChangesAndResolveConflicts()
		{
			// http://msdn.microsoft.com/en-us/library/bb386918(v=vs.110).aspx
			SubmitChangesAndResolveConflicts(RefreshMode.KeepChanges);
		}
	}
}

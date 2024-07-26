using System;
using System.Runtime.CompilerServices;

namespace Library.DualGlobe.ERP.Models
{
	public class Document
	{
		public string DocumentName
		{
			get;
			set;
		}

		public string DocumentUrl
		{
			get;
			set;
		}

		public virtual Library.DualGlobe.ERP.Models.Expense Expense
		{
			get;
			set;
		}

		public int Id
		{
			get;
			set;
		}

		public Document()
		{
		}
	}
}
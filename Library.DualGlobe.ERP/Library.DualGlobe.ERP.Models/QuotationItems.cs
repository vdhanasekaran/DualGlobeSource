using System;
using System.Runtime.CompilerServices;

namespace Library.DualGlobe.ERP.Models
{
	public class QuotationItems
	{
		public decimal? Amount
		{
			get;
			set;
		}

		public string Description
		{
			get;
			set;
		}

		public decimal? GST
		{
			get;
			set;
		}

		public int Id
		{
			get;
			set;
		}

		public int Quantity
		{
			get;
			set;
		}

		public int? QuotationId
		{
			get;
			set;
		}

		public decimal? UnitPrice
		{
			get;
			set;
		}

		public QuotationItems()
		{
		}
	}
}
using System;
using System.Runtime.CompilerServices;

namespace Library.DualGlobe.ERP.Models
{
	public class InvoicePhase
	{
		public string Description
		{
			get;
			set;
		}

		public decimal? DiscountedGSTAmount
		{
			get;
			set;
		}

		public decimal? DiscountedPhaseInvoiceAmount
		{
			get;
			set;
		}

		public decimal? GST
		{
			get;
			set;
		}

		public decimal? GSTAmount
		{
			get;
			set;
		}

		public int Id
		{
			get;
			set;
		}

		public int InvoiceId
		{
			get;
			set;
		}

		public decimal? Percentage
		{
			get;
			set;
		}

		public decimal? PhaseAmount
		{
			get;
			set;
		}

		public decimal? PhaseInvoiceAmount
		{
			get;
			set;
		}

		public int? Quantity
		{
			get;
			set;
		}

		public decimal? QuotationAmount
		{
			get;
			set;
		}

		public decimal? UnitPrice
		{
			get;
			set;
		}

		public InvoicePhase()
		{
		}
	}
}
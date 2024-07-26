using System;
using System.Runtime.CompilerServices;

namespace Library.DualGlobe.ERP.Models
{
	public class Payment
	{
		public decimal? Amount
		{
			get;
			set;
		}

		public int Id
		{
			get;
			set;
		}

		public int? InvoiceId
		{
			get;
			set;
		}

		public string PaymentDate
		{
			get;
			set;
		}

		public string PaymentMethod
		{
			get;
			set;
		}

		public decimal? TransactionFees
		{
			get;
			set;
		}

		public int? TransactionId
		{
			get;
			set;
		}

		public Payment()
		{
		}
	}
}
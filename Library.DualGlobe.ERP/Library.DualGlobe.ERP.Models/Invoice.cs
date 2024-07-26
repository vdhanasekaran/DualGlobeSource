using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Library.DualGlobe.ERP.Models
{
	public class Invoice
	{
		public string Address
		{
			get;
			set;
		}

		public string Attention
		{
			get;
			set;
		}

		public int? ClientId
		{
			get;
			set;
		}

		public string DiscountType
		{
			get;
			set;
		}

		public decimal? DiscountValue
		{
			get;
			set;
		}

		public string Email
		{
			get;
			set;
		}

		public DateTime? FromDate
		{
			get;
			set;
		}

		public int Id
		{
			get;
			set;
		}

		public decimal? InvoiceAmount
		{
			get;
			set;
		}

		public DateTime? InvoiceDate
		{
			get;
			set;
		}

		public virtual List<InvoicePhase> InvoicePhases
		{
			get;
			set;
		}

		public string IsProgressClaimRequired
		{
			get;
			set;
		}

		public virtual List<Payment> Payments
		{
			get;
			set;
		}

		public string PhaseName
		{
			get;
			set;
		}

		public string ProgressClaimStatus
		{
			get;
			set;
		}

		public int? ProjectId
		{
			get;
			set;
		}

		public int? QuotationId
		{
			get;
			set;
		}

		public string ReferenceNumber
		{
			get;
			set;
		}

		public string Status
		{
			get;
			set;
		}

		public DateTime? ToDate
		{
			get;
			set;
		}

		public decimal? TotalAmount
		{
			get;
			set;
		}

		public decimal? TotalDiscountedGST
		{
			get;
			set;
		}

		public decimal? TotalDiscountedPhaseInvoice
		{
			get;
			set;
		}

		public decimal? TotalGSTAmount
		{
			get;
			set;
		}

		public decimal? TotalPhaseAmount
		{
			get;
			set;
		}

		public Invoice()
		{
		}
	}
}
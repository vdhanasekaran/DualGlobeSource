using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Library.DualGlobe.ERP.Models
{
	public class Quotation
	{
		public int ClientId
		{
			get;
			set;
		}

		public string Content
		{
			get;
			set;
		}

		public DateTime? DateCreated
		{
			get;
			set;
		}

		public string Description
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

		public decimal? GST
		{
			get;
			set;
		}

		public string GSTType
		{
			get;
			set;
		}

		public int Id
		{
			get;
			set;
		}

		public string Notes
		{
			get;
			set;
		}

		public string PONumber
		{
			get;
			set;
		}

		public int ProjectId
		{
			get;
			set;
		}

		public virtual List<QuotationItems> quotationItems
		{
			get;
			set;
		}

		public decimal? QuotationValue
		{
			get;
			set;
		}

		public string ReferenceNumber
		{
			get;
			set;
		}

		public string Stage
		{
			get;
			set;
		}

		public string Subject
		{
			get;
			set;
		}

		public string TermsAndCondtions
		{
			get;
			set;
		}

		public string To
		{
			get;
			set;
		}

		public DateTime? ValidUntil
		{
			get;
			set;
		}

		public Quotation()
		{
		}
	}
}
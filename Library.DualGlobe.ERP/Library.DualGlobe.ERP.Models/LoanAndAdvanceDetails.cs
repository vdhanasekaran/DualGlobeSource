using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace Library.DualGlobe.ERP.Models
{
	public class LoanAndAdvanceDetails
	{
		public int Id
		{
			get;
			set;
		}

		public bool IsDetected
		{
			get;
			set;
		}

		public decimal LoanDetectionAmount
		{
			get;
			set;
		}

		[Column(TypeName="date")]
		public DateTime LoanDetectionDate
		{
			get;
			set;
		}

		public LoanAndAdvanceDetails()
		{
		}
	}
}
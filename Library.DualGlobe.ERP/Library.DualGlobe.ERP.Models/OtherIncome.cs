using System;
using System.Runtime.CompilerServices;

namespace Library.DualGlobe.ERP.Models
{
	public class OtherIncome
	{
		public string Description
		{
			get;
			set;
		}

		public int Id
		{
			get;
			set;
		}

		public DateTime IncomeDate
		{
			get;
			set;
		}

		public decimal? TotalAmount
		{
			get;
			set;
		}

		public OtherIncome()
		{
		}
	}
}
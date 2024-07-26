using System;
using System.Runtime.CompilerServices;

namespace Library.DualGlobe.ERP.Models
{
	public class Donation
	{
		public decimal? Contribution
		{
			get;
			set;
		}

		public string DonationType
		{
			get;
			set;
		}

		public int Id
		{
			get;
			set;
		}

		public decimal? LowerRange
		{
			get;
			set;
		}

		public string Nationality
		{
			get;
			set;
		}

		public string Religion
		{
			get;
			set;
		}

		public decimal? UpperRange
		{
			get;
			set;
		}

		public Donation()
		{
		}
	}
}
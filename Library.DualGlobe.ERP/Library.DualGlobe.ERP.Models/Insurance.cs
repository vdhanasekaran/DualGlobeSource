using System;
using System.Runtime.CompilerServices;

namespace Library.DualGlobe.ERP.Models
{
	public class Insurance
	{
		public DateTime? EndDate
		{
			get;
			set;
		}

		public int Id
		{
			get;
			set;
		}

		public string InsurancePolicyNumber
		{
			get;
			set;
		}

		public string InsuranceProviderName
		{
			get;
			set;
		}

		public string InsuranceType
		{
			get;
			set;
		}

		public DateTime? StartDate
		{
			get;
			set;
		}

		public Insurance()
		{
		}
	}
}
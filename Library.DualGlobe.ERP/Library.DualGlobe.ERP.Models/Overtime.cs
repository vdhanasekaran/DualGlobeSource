using System;
using System.Runtime.CompilerServices;

namespace Library.DualGlobe.ERP.Models
{
	public class Overtime
	{
		public string GroupName
		{
			get;
			set;
		}

		public int Id
		{
			get;
			set;
		}

		public string OTType
		{
			get;
			set;
		}

		public decimal? OTValue
		{
			get;
			set;
		}

		public Overtime()
		{
		}
	}
}
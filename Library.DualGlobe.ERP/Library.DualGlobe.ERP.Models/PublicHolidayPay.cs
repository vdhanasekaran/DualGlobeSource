using System;
using System.Runtime.CompilerServices;

namespace Library.DualGlobe.ERP.Models
{
	public class PublicHolidayPay
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

		public string PayType
		{
			get;
			set;
		}

		public decimal? PayValue
		{
			get;
			set;
		}

		public PublicHolidayPay()
		{
		}
	}
}
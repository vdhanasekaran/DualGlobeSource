using System;
using System.Runtime.CompilerServices;

namespace Library.DualGlobe.ERP.Models
{
	public class RestDayPay
	{
		public string FullRestDayPayType
		{
			get;
			set;
		}

		public decimal? FullRestDayPayValue
		{
			get;
			set;
		}

		public string GroupName
		{
			get;
			set;
		}

		public string HalfRestDayPayType
		{
			get;
			set;
		}

		public decimal? HalfRestDayPayValue
		{
			get;
			set;
		}

		public int Id
		{
			get;
			set;
		}

		public RestDayPay()
		{
		}
	}
}
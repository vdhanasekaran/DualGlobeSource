using System;
using System.Runtime.CompilerServices;

namespace Library.DualGlobe.ERP.Models
{
	public class RestDate
	{
		public DateTime? Date
		{
			get;
			set;
		}

		public int Id
		{
			get;
			set;
		}

		public int RestDayId
		{
			get;
			set;
		}

		public RestDate()
		{
		}
	}
}
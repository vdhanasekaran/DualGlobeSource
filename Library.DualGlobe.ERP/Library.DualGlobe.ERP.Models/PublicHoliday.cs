using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace Library.DualGlobe.ERP.Models
{
	public class PublicHoliday
	{
		[Column(TypeName="date")]
		public DateTime Date
		{
			get;
			set;
		}

		public int Id
		{
			get;
			set;
		}

		public string LeaveDescription
		{
			get;
			set;
		}

		public PublicHoliday()
		{
		}
	}
}
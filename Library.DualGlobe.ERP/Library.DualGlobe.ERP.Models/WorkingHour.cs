using System;
using System.Runtime.CompilerServices;

namespace Library.DualGlobe.ERP.Models
{
	public class WorkingHour
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

		public TimeSpan? InTime
		{
			get;
			set;
		}

		public TimeSpan? OutTime
		{
			get;
			set;
		}

		public decimal? TotalHour
		{
			get;
			set;
		}

		public decimal? WeeklyAvgWorkingDays
		{
			get;
			set;
		}

		public decimal? WeeklyAvgWorkingHour
		{
			get;
			set;
		}

		public WorkingHour()
		{
		}
	}
}
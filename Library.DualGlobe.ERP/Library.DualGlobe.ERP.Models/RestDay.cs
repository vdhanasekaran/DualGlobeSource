using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Library.DualGlobe.ERP.Models
{
	public class RestDay
	{
		public string FridayRestType
		{
			get;
			set;
		}

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

		public bool IsFridayRestDay
		{
			get;
			set;
		}

		public bool IsMondayRestDay
		{
			get;
			set;
		}

		public bool IsSaturdayRestDay
		{
			get;
			set;
		}

		public bool IsSundayRestDay
		{
			get;
			set;
		}

		public bool IsThursdayRestDay
		{
			get;
			set;
		}

		public bool IsTuesdayRestDay
		{
			get;
			set;
		}

		public bool IsWednesdayRestDay
		{
			get;
			set;
		}

		public string MondayRestType
		{
			get;
			set;
		}

		public virtual List<RestDate> RestDates
		{
			get;
			set;
		}

		public string RestDayType
		{
			get;
			set;
		}

		public string SaturdayRestType
		{
			get;
			set;
		}

		public string SundayRestType
		{
			get;
			set;
		}

		public string ThursdayRestType
		{
			get;
			set;
		}

		public string TuesdayRestType
		{
			get;
			set;
		}

		public string WednesdayRestType
		{
			get;
			set;
		}

		public RestDay()
		{
		}
	}
}
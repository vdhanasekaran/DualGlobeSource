using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace Library.DualGlobe.ERP.Models
{
	public class Timesheet
	{
		public int? ClientId
		{
			get;
			set;
		}

		public virtual Employee employee
		{
			get;
			set;
		}

		public int EmployeeId
		{
			get;
			set;
		}

		public int Id
		{
			get;
			set;
		}

		public bool IsLeave
		{
			get;
			set;
		}

		public bool IsPaid
		{
			get;
			set;
		}

		public bool IsPublicHoliday
		{
			get;
			set;
		}

		public bool IsRestday
		{
			get;
			set;
		}

		public bool IsSubmitted
		{
			get;
			set;
		}

		public double? OTHours
		{
			get;
			set;
		}

		public int? ProjectId
		{
			get;
			set;
		}

		public double? RegularHours
		{
			get;
			set;
		}

		public TimeSpan? TimeIn
		{
			get;
			set;
		}

		public TimeSpan? TimeOut
		{
			get;
			set;
		}

		[Column(TypeName="date")]
		public DateTime TimesheetDate
		{
			get;
			set;
		}

		public double? TotalHours
		{
			get;
			set;
		}

		public Timesheet()
		{
		}
	}
}
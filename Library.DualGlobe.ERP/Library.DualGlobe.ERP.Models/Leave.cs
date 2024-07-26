using System;
using System.Runtime.CompilerServices;

namespace Library.DualGlobe.ERP.Models
{
	public class Leave
	{
		public int EmployeeId
		{
			get;
			set;
		}

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

		public string LeaveType
		{
			get;
			set;
		}

		public decimal PaidLeave
		{
			get;
			set;
		}

		public DateTime? StartDate
		{
			get;
			set;
		}

		public decimal? TotalDays
		{
			get;
			set;
		}

		public decimal UnPaidLeave
		{
			get;
			set;
		}

		public Leave()
		{
		}
	}
}
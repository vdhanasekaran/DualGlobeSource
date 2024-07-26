using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace Library.DualGlobe.ERP.Models
{
	public class Allowance
	{
		[Column(TypeName="date")]
		public DateTime AllowanceDate
		{
			get;
			set;
		}

		public string AllowanceType
		{
			get;
			set;
		}

		public int ApprovedByEmployeeId
		{
			get;
			set;
		}

		public decimal? BonusAllowance
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

		public decimal? FoodAllowance
		{
			get;
			set;
		}

		public int Id
		{
			get;
			set;
		}

		public decimal? IncentiveAllowance
		{
			get;
			set;
		}

		public decimal? OtherAllowance
		{
			get;
			set;
		}

		public decimal? RoomRentalAllowance
		{
			get;
			set;
		}

		public decimal? TravelAllowance
		{
			get;
			set;
		}

		public Allowance()
		{
		}
	}
}
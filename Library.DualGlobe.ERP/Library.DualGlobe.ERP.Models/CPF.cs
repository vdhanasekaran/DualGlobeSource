using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace Library.DualGlobe.ERP.Models
{
	public class CPF
	{
		public decimal? AdditionalWages
		{
			get;
			set;
		}

		public decimal? CPFDonation
		{
			get;
			set;
		}

		[Column(TypeName="date")]
		public DateTime Date
		{
			get;
			set;
		}

		public string DonationType
		{
			get;
			set;
		}

		public decimal? EmployeeCPF
		{
			get;
			set;
		}

		public string EmployeeIC
		{
			get;
			set;
		}

		public int EmployeeId
		{
			get;
			set;
		}

		public decimal? EmployerCPF
		{
			get;
			set;
		}

		public decimal? FixedDecution
		{
			get;
			set;
		}

		public int Id
		{
			get;
			set;
		}

		public decimal? OrdinaryWages
		{
			get;
			set;
		}

		public decimal? TotalCPF
		{
			get;
			set;
		}

		public decimal? TotalWages
		{
			get;
			set;
		}

		public CPF()
		{
		}
	}
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace Library.DualGlobe.ERP.Models
{
	public class LoanAndAdvance
	{
		public int ApprovedByEmployeeId
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

		public decimal LoanAmount
		{
			get;
			set;
		}

		public virtual List<LoanAndAdvanceDetails> loanAndAdvanceDetails
		{
			get;
			set;
		}

		[Column(TypeName="date")]
		public DateTime LoanDate
		{
			get;
			set;
		}

		[Column(TypeName="date")]
		public DateTime LoanRepaymentEndDate
		{
			get;
			set;
		}

		[Column(TypeName="date")]
		public DateTime LoanRepaymentStartDate
		{
			get;
			set;
		}

		public string LoanStatus
		{
			get;
			set;
		}

		public string Mode
		{
			get;
			set;
		}

		public string RepaymentAmount
		{
			get;
			set;
		}

		public string RepaymentDuration
		{
			get;
			set;
		}

		public LoanAndAdvance()
		{
		}
	}
}
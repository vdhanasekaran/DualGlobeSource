using System;
using System.Runtime.CompilerServices;

namespace Library.DualGlobe.ERP.Models
{
	public class OperationExpense
	{
		public decimal? Amount
		{
			get;
			set;
		}

		public string BillRefNo
		{
			get;
			set;
		}

		public DateTime Date
		{
			get;
			set;
		}

		public string ExpenseCategory
		{
			get;
			set;
		}

		public int Id
		{
			get;
			set;
		}

		public string OtherExpense
		{
			get;
			set;
		}

		public string PaymentMethod
		{
			get;
			set;
		}

		public string PaymentStatus
		{
			get;
			set;
		}

		public string TransRefNo
		{
			get;
			set;
		}

		public OperationExpense()
		{
		}
	}
}
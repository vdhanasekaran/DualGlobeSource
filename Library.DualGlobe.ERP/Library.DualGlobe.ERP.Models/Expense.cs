using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Library.DualGlobe.ERP.Models
{
	public class Expense
	{
		public string BillRefNo
		{
			get;
			set;
		}

		public int ClientId
		{
			get;
			set;
		}

		public DateTime Date
		{
			get;
			set;
		}

		public string Description
		{
			get;
			set;
		}

		public virtual List<Document> Documents
		{
			get;
			set;
		}

		public string ExpenseCategory
		{
			get;
			set;
		}

		public decimal? ExpenseTotalValue
		{
			get;
			set;
		}

		public decimal? ExpenseValue
		{
			get;
			set;
		}

		public decimal? GST
		{
			get;
			set;
		}

		public int Id
		{
			get;
			set;
		}

		public bool IncludeGST
		{
			get;
			set;
		}

		public string MaterialPurchseCategory
		{
			get;
			set;
		}

		public string OperationExpenseCategory
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

		public int ProjectId
		{
			get;
			set;
		}

		public string Supplier
		{
			get;
			set;
		}

		public string TransRefNo
		{
			get;
			set;
		}

		public Expense()
		{
		}
	}
}
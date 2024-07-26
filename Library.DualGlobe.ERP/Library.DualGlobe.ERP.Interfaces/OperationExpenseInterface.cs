using Library.DualGlobe.ERP.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Library.DualGlobe.ERP.Interfaces
{
	public class OperationExpenseInterface
	{
		public OperationExpenseInterface()
		{
		}

		public static void Create(OperationExpense operationExpense)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<OperationExpense>(operationExpense).set_State(4);
				context.SaveChanges();
			}
		}

		public static void Delete(int id)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<OperationExpense>(context.OperationExpenses.Find(new object[] { id })).set_State(8);
				context.SaveChanges();
			}
		}

		public static OperationExpense[] Read()
		{
			OperationExpense[] array;
			using (ERPContext context = new ERPContext())
			{
				array = context.OperationExpenses.ToArray<OperationExpense>();
			}
			return array;
		}

		public static OperationExpense Read(int id)
		{
			OperationExpense operationExpense;
			using (ERPContext context = new ERPContext())
			{
				operationExpense = (
					from t in context.OperationExpenses
					where t.Id == id
					select t).FirstOrDefault<OperationExpense>();
			}
			return operationExpense;
		}

		public static OperationExpense[] ReadByMonthAndYear(int month, int year)
		{
			OperationExpense[] array;
			using (ERPContext context = new ERPContext())
			{
				array = (
					from i in context.OperationExpenses
					where i.Date.Month == month && i.Date.Year == year
					select i).ToArray<OperationExpense>();
			}
			return array;
		}

		public static OperationExpense[] ReadByStartDateEndDate(DateTime startDate, DateTime endDate)
		{
			OperationExpense[] array;
			using (ERPContext context = new ERPContext())
			{
				array = (
					from x in context.OperationExpenses
					where (x.Date >= startDate) && (x.Date <= endDate)
					select x).ToArray<OperationExpense>();
			}
			return array;
		}

		public static void Update(OperationExpense operationExpense)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<OperationExpense>(operationExpense).set_State(16);
				context.SaveChanges();
			}
		}
	}
}
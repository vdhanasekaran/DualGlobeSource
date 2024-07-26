using Library.DualGlobe.ERP.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Library.DualGlobe.ERP.Interfaces
{
	public class ExpenseCategoryInterface
	{
		public ExpenseCategoryInterface()
		{
		}

		public static void Create(ExpenseCategory expenseCategory)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<ExpenseCategory>(expenseCategory).set_State(4);
				context.SaveChanges();
			}
		}

		public static void Delete(int id)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<ExpenseCategory>(context.ExpenseCategorys.Find(new object[] { id })).set_State(8);
				context.SaveChanges();
			}
		}

		public static int GetCount()
		{
			int num;
			using (ERPContext context = new ERPContext())
			{
				num = context.ExpenseCategorys.Count<ExpenseCategory>();
			}
			return num;
		}

		public static ExpenseCategory[] Read()
		{
			ExpenseCategory[] array;
			using (ERPContext context = new ERPContext())
			{
				array = context.ExpenseCategorys.ToArray<ExpenseCategory>();
			}
			return array;
		}

		public static ExpenseCategory Read(int id)
		{
			ExpenseCategory expenseCategory;
			using (ERPContext context = new ERPContext())
			{
				expenseCategory = (
					from t in context.ExpenseCategorys
					where t.Id == id
					select t).FirstOrDefault<ExpenseCategory>();
			}
			return expenseCategory;
		}

		public static void Update(ExpenseCategory expenseCategory)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<ExpenseCategory>(expenseCategory).set_State(16);
				context.SaveChanges();
			}
		}
	}
}
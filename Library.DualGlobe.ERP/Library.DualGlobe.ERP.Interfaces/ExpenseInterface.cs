using Library.DualGlobe.ERP.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Library.DualGlobe.ERP.Interfaces
{
	public class ExpenseInterface
	{
		public ExpenseInterface()
		{
		}

		public static void Create(Expense materialPurchase)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<Expense>(materialPurchase).set_State(4);
				context.SaveChanges();
			}
		}

		public static void Delete(int id)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<Expense>(context.Expenses.Find(new object[] { id })).set_State(8);
				context.SaveChanges();
			}
		}

		public static void DeleteDocument(int id)
		{
			using (ERPContext context = new ERPContext())
			{
				Document document = context.Expenses.SelectMany<Expense, Document>((Expense t) => t.Documents).Where<Document>((Document j) => j.Id == id).FirstOrDefault<Document>();
				context.Entry<Document>(document).set_State(8);
				context.SaveChanges();
			}
		}

		public static void InsertDocument(Document record, Expense parent)
		{
			using (ERPContext context = new ERPContext())
			{
				record.Expense = parent;
				context.Entry<Expense>(parent).set_State(2);
				context.Entry<Document>(record).set_State(4);
				context.SaveChanges();
			}
		}

		public static Expense[] Read()
		{
			Expense[] array;
			using (ERPContext context = new ERPContext())
			{
				array = QueryableExtensions.Include<Expense, List<Document>>(context.Expenses, (Expense i) => i.Documents).ToArray<Expense>();
			}
			return array;
		}

		public static Expense Read(int id)
		{
			Expense expense;
			using (ERPContext context = new ERPContext())
			{
				expense = QueryableExtensions.Include<Expense, List<Document>>(
					from t in context.Expenses
					where t.Id == id
					select t, (Expense i) => i.Documents).FirstOrDefault<Expense>();
			}
			return expense;
		}

		public static Expense[] ReadByClientId(int clientId)
		{
			Expense[] array;
			using (ERPContext context = new ERPContext())
			{
				array = (
					from t in context.Expenses
					where t.ClientId == clientId
					select t into p
					orderby p.Id
					select p).ToArray<Expense>();
			}
			return array;
		}

		public static Expense[] ReadByMonthAndYear(int month, int year)
		{
			Expense[] array;
			using (ERPContext context = new ERPContext())
			{
				array = QueryableExtensions.Include<Expense, List<Document>>(
					from i in context.Expenses
					where i.Date.Month == month && i.Date.Year == year
					select i, (Expense i) => i.Documents).ToArray<Expense>();
			}
			return array;
		}

		public static Expense[] ReadByProjectId(int projectId)
		{
			Expense[] array;
			using (ERPContext context = new ERPContext())
			{
				array = (
					from t in context.Expenses
					where t.ProjectId == projectId
					select t into p
					orderby p.Id
					select p).ToArray<Expense>();
			}
			return array;
		}

		public static Expense[] ReadByStartDateEndDate(DateTime startDate, DateTime endDate)
		{
			Expense[] array;
			using (ERPContext context = new ERPContext())
			{
				array = QueryableExtensions.Include<Expense, List<Document>>(
					from x in context.Expenses
					where (x.Date >= startDate) && (x.Date <= endDate)
					select x, (Expense i) => i.Documents).ToArray<Expense>();
			}
			return array;
		}

		public static void Update(Expense materialPurchase)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<Expense>(materialPurchase).set_State(16);
				context.SaveChanges();
			}
		}
	}
}
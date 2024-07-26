using Library.DualGlobe.ERP.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Library.DualGlobe.ERP.Interfaces
{
	public class OtherIncomeInterface
	{
		public OtherIncomeInterface()
		{
		}

		public static void Create(OtherIncome otherIncome)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<OtherIncome>(otherIncome).set_State(4);
				context.SaveChanges();
			}
		}

		public static void Delete(int id)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<OtherIncome>(context.OtherIncomes.Find(new object[] { id })).set_State(8);
				context.SaveChanges();
			}
		}

		public static OtherIncome[] Read()
		{
			OtherIncome[] array;
			using (ERPContext context = new ERPContext())
			{
				array = context.OtherIncomes.ToArray<OtherIncome>();
			}
			return array;
		}

		public static OtherIncome Read(int id)
		{
			OtherIncome otherIncome;
			using (ERPContext context = new ERPContext())
			{
				otherIncome = (
					from t in context.OtherIncomes
					where t.Id == id
					select t).FirstOrDefault<OtherIncome>();
			}
			return otherIncome;
		}

		public static OtherIncome[] ReadByMonthAndYear(int month, int year)
		{
			OtherIncome[] array;
			using (ERPContext context = new ERPContext())
			{
				array = (
					from i in context.OtherIncomes
					where i.IncomeDate.Month == month && i.IncomeDate.Year == year
					select i).ToArray<OtherIncome>();
			}
			return array;
		}

		public static OtherIncome[] ReadByStartDateEndDate(DateTime startDate, DateTime endDate)
		{
			OtherIncome[] array;
			using (ERPContext context = new ERPContext())
			{
				array = (
					from x in context.OtherIncomes
					where (x.IncomeDate >= startDate) && (x.IncomeDate <= endDate)
					select x).ToArray<OtherIncome>();
			}
			return array;
		}

		public static void Update(OtherIncome otherIncome)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<OtherIncome>(otherIncome).set_State(16);
				context.SaveChanges();
			}
		}
	}
}
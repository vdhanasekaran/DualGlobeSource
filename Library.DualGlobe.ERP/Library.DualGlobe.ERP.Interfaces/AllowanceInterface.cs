using Library.DualGlobe.ERP.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Library.DualGlobe.ERP.Interfaces
{
	public class AllowanceInterface
	{
		public AllowanceInterface()
		{
		}

		public static void Create(Allowance allowance)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<Allowance>(allowance).set_State(4);
				context.SaveChanges();
			}
		}

		public static void Delete(int id)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<Allowance>(context.Allowances.Find(new object[] { id })).set_State(8);
				context.SaveChanges();
			}
		}

		public static Allowance[] Read()
		{
			Allowance[] array;
			using (ERPContext context = new ERPContext())
			{
				array = QueryableExtensions.Include<Allowance, Employee>(context.Allowances, (Allowance t) => t.employee).ToArray<Allowance>();
			}
			return array;
		}

		public static Allowance Read(int id)
		{
			Allowance allowance;
			using (ERPContext context = new ERPContext())
			{
				allowance = (
					from t in context.Allowances
					where t.Id == id
					select t).FirstOrDefault<Allowance>();
			}
			return allowance;
		}

		public static Allowance[] ReadByEmployeeMonthYear(int empId, int month, int year)
		{
			Allowance[] array;
			using (ERPContext context = new ERPContext())
			{
				array = (
					from t in context.Allowances
					where t.AllowanceDate.Month == month && t.AllowanceDate.Year == year & t.EmployeeId == empId
					select t).ToArray<Allowance>();
			}
			return array;
		}

		public static Allowance[] ReadByMonth(DateTime allowanceMonth)
		{
			Allowance[] array;
			using (ERPContext context = new ERPContext())
			{
				array = QueryableExtensions.Include<Allowance, Employee>(
					from t in context.Allowances
					where t.AllowanceDate.Month == allowanceMonth.Month && t.AllowanceDate.Year == allowanceMonth.Year
					select t, (Allowance t) => t.employee).ToArray<Allowance>();
			}
			return array;
		}

		public static void Update(Allowance allowance)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<Allowance>(allowance).set_State(16);
				context.SaveChanges();
			}
		}
	}
}
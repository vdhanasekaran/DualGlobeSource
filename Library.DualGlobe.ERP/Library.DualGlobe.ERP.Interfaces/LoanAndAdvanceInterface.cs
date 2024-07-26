using Library.DualGlobe.ERP.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Library.DualGlobe.ERP.Interfaces
{
	public class LoanAndAdvanceInterface
	{
		public LoanAndAdvanceInterface()
		{
		}

		public static void Create(LoanAndAdvance loanAndAdvance)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<LoanAndAdvance>(loanAndAdvance).set_State(4);
				context.SaveChanges();
			}
		}

		public static void Delete(int id)
		{
			using (ERPContext context = new ERPContext())
			{
				LoanAndAdvance loanAdvance = context.LoanAndAdvances.Find(new object[] { id });
				for (int i = 0; i < loanAdvance.loanAndAdvanceDetails.Count; i++)
				{
					LoanAndAdvanceDetails type = loanAdvance.loanAndAdvanceDetails[i];
					loanAdvance.loanAndAdvanceDetails.Remove(type);
				}
				context.Entry<LoanAndAdvance>(loanAdvance).set_State(8);
				context.SaveChanges();
			}
		}

		public static void InsertLoanAndAdvanceDetail(LoanAndAdvanceDetails loanAndAdvanceDetails)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<LoanAndAdvanceDetails>(loanAndAdvanceDetails).set_State(4);
				context.SaveChanges();
			}
		}

		public static LoanAndAdvance[] Read()
		{
			LoanAndAdvance[] array;
			using (ERPContext context = new ERPContext())
			{
				array = QueryableExtensions.Include<LoanAndAdvance, Employee>(QueryableExtensions.Include<LoanAndAdvance, List<LoanAndAdvanceDetails>>(context.LoanAndAdvances, (LoanAndAdvance t) => t.loanAndAdvanceDetails), (LoanAndAdvance t) => t.employee).ToArray<LoanAndAdvance>();
			}
			return array;
		}

		public static LoanAndAdvance Read(int id)
		{
			LoanAndAdvance loanAndAdvance;
			using (ERPContext context = new ERPContext())
			{
				loanAndAdvance = QueryableExtensions.Include<LoanAndAdvance, List<LoanAndAdvanceDetails>>(
					from t in context.LoanAndAdvances
					where t.Id == id
					select t, (LoanAndAdvance t) => t.loanAndAdvanceDetails).FirstOrDefault<LoanAndAdvance>();
			}
			return loanAndAdvance;
		}

		public static LoanAndAdvance[] ReadByEmployeeId(int id)
		{
			LoanAndAdvance[] array;
			using (ERPContext context = new ERPContext())
			{
				array = QueryableExtensions.Include<LoanAndAdvance, List<LoanAndAdvanceDetails>>(
					from t in context.LoanAndAdvances
					where t.EmployeeId == id
					select t, (LoanAndAdvance t) => t.loanAndAdvanceDetails).ToArray<LoanAndAdvance>();
			}
			return array;
		}

		public static LoanAndAdvance[] ReadByEmployeeMonthYear(int empId, int month, int year)
		{
			LoanAndAdvance[] array;
			using (ERPContext context = new ERPContext())
			{
				array = QueryableExtensions.Include<LoanAndAdvance, List<LoanAndAdvanceDetails>>(
					from t in context.LoanAndAdvances
					where t.LoanDate.Month == month && t.LoanDate.Year == year && t.EmployeeId == empId
					select t, (LoanAndAdvance t) => t.loanAndAdvanceDetails).ToArray<LoanAndAdvance>();
			}
			return array;
		}

		public static LoanAndAdvance[] ReadByMonth(DateTime loanDate)
		{
			LoanAndAdvance[] array;
			using (ERPContext context = new ERPContext())
			{
				array = QueryableExtensions.Include<LoanAndAdvance, Employee>(QueryableExtensions.Include<LoanAndAdvance, List<LoanAndAdvanceDetails>>(
					from t in context.LoanAndAdvances
					where t.LoanDate.Month == loanDate.Month && t.LoanDate.Year == loanDate.Year
					select t, (LoanAndAdvance t) => t.loanAndAdvanceDetails), (LoanAndAdvance t) => t.employee).ToArray<LoanAndAdvance>();
			}
			return array;
		}

		public static void Update(LoanAndAdvance loanAndAdvance)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<LoanAndAdvance>(loanAndAdvance).set_State(16);
				context.SaveChanges();
			}
		}

		public static void Update(LoanAndAdvanceDetails loanAndAdvanceDetails)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<LoanAndAdvanceDetails>(loanAndAdvanceDetails).set_State(16);
				context.SaveChanges();
			}
		}
	}
}
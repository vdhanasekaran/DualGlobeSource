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
	public class SalaryDetailInterface
	{
		public SalaryDetailInterface()
		{
		}

		public static void BulkInsert(List<SalaryDetail> salaryList)
		{
			using (ERPContext context = new ERPContext())
			{
				context.SalaryDetails.AddRange(salaryList);
				context.SaveChanges();
			}
		}

		public static void Create(Library.DualGlobe.ERP.Models.SalaryDetail SalaryDetail)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<Library.DualGlobe.ERP.Models.SalaryDetail>(SalaryDetail).set_State(4);
				context.SaveChanges();
			}
		}

		public static void Delete(int id)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<SalaryDetail>(context.SalaryDetails.Find(new object[] { id })).set_State(8);
				context.SaveChanges();
			}
		}

		public static SalaryDetail[] Read()
		{
			SalaryDetail[] array;
			using (ERPContext context = new ERPContext())
			{
				array = context.SalaryDetails.ToArray<SalaryDetail>();
			}
			return array;
		}

		public static SalaryDetail ReadByEmployeeId(int id)
		{
			SalaryDetail salaryDetail;
			using (ERPContext context = new ERPContext())
			{
				salaryDetail = (
					from x in context.SalaryDetails
					where x.EmployeeId == id
					select x).FirstOrDefault<SalaryDetail>();
			}
			return salaryDetail;
		}

		public static SalaryDetail ReadByEmployeeIdAndDate(int id, string month, string year)
		{
			SalaryDetail salaryDetail;
			using (ERPContext context = new ERPContext())
			{
				salaryDetail = (
					from x in context.SalaryDetails
					where x.EmployeeId == id && x.SalaryMonth == month && x.SalaryYear == year
					select x).FirstOrDefault<SalaryDetail>();
			}
			return salaryDetail;
		}

		public static SalaryDetail ReadById(int id)
		{
			SalaryDetail salaryDetail;
			using (ERPContext context = new ERPContext())
			{
				salaryDetail = (
					from t in context.SalaryDetails
					where t.Id == id
					select t).FirstOrDefault<SalaryDetail>();
			}
			return salaryDetail;
		}

		public static SalaryDetail[] ReadByMonthYear(string month, string year)
		{
			SalaryDetail[] array;
			using (ERPContext context = new ERPContext())
			{
				array = (
					from x in context.SalaryDetails
					where x.SalaryMonth == month && x.SalaryYear == year
					select x).ToArray<SalaryDetail>();
			}
			return array;
		}

		public static SalaryDetail[] ReadByStartDateEndDate(DateTime startDate, DateTime endDate)
		{
			SalaryDetail[] array;
			using (ERPContext context = new ERPContext())
			{
				array = (
					from x in context.SalaryDetails
					where (x.DateCreated >= startDate) && (x.DateCreated <= endDate)
					select x).ToArray<SalaryDetail>();
			}
			return array;
		}

		public static SalaryDetail[] ReadByStatusMonthYear(string month, string year, string workStatus)
		{
			SalaryDetail[] array;
			using (ERPContext context = new ERPContext())
			{
				array = (
					from x in context.SalaryDetails
					where x.SalaryMonth == month && x.SalaryYear == year && x.WorkStatus == workStatus
					select x).ToArray<SalaryDetail>();
			}
			return array;
		}

		public static SalaryDetail[] ReadByWorkStatus(DateTime startDate, DateTime endDate, string workStatus)
		{
			SalaryDetail[] array;
			using (ERPContext context = new ERPContext())
			{
				array = (
					from x in context.SalaryDetails
					where (x.DateCreated >= startDate) && (x.DateCreated <= endDate) && x.WorkStatus == workStatus
					select x).ToArray<SalaryDetail>();
			}
			return array;
		}

		public static void Update(Library.DualGlobe.ERP.Models.SalaryDetail SalaryDetail)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<Library.DualGlobe.ERP.Models.SalaryDetail>(SalaryDetail).set_State(16);
				context.SaveChanges();
			}
		}
	}
}
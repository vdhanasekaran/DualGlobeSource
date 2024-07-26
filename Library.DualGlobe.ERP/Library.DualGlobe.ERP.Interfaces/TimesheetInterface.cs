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
	public class TimesheetInterface
	{
		public TimesheetInterface()
		{
		}

		public static Timesheet[] BulkInsert(List<Timesheet> timesheet)
		{
			Timesheet[] array;
			using (ERPContext context = new ERPContext())
			{
				context.get_Configuration().set_AutoDetectChangesEnabled(false);
				context.Timesheets.AddRange(timesheet);
				context.SaveChanges();
				array = timesheet.ToArray();
			}
			return array;
		}

		public static void Create(Library.DualGlobe.ERP.Models.Timesheet Timesheet)
		{
			using (ERPContext context = new ERPContext())
			{
				context.get_Configuration().set_AutoDetectChangesEnabled(false);
				context.Entry<Library.DualGlobe.ERP.Models.Timesheet>(Timesheet).set_State(4);
				context.Entry<Employee>(Timesheet.employee).set_State(2);
				context.SaveChanges();
			}
		}

		public static void Delete(int id)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<Timesheet>(context.Timesheets.Find(new object[] { id })).set_State(8);
				context.SaveChanges();
			}
		}

		public static void Delete(int id, DateTime timesheetDate)
		{
			using (ERPContext context = new ERPContext())
			{
				IQueryable<Timesheet> data = 
					from t in context.Timesheets
					where t.EmployeeId == id && (t.TimesheetDate == timesheetDate)
					select t;
				context.Entry<IQueryable<Timesheet>>(data).set_State(8);
				context.SaveChanges();
			}
		}

		public static Timesheet[] ReadByClientStartDateEndDate(DateTime startDate, DateTime endDate, int clientId)
		{
			Timesheet[] array;
			using (ERPContext context = new ERPContext())
			{
				array = (
					from t in context.Timesheets
					where (t.TimesheetDate >= startDate) && (t.TimesheetDate <= endDate) && t.ClientId == (int?)clientId
					select t).ToArray<Timesheet>();
			}
			return array;
		}

		public static Timesheet[] ReadByDate(DateTime startDate)
		{
			Timesheet[] array;
			using (ERPContext context = new ERPContext())
			{
				array = (
					from t in context.Timesheets
					where t.TimesheetDate == startDate
					select t).ToArray<Timesheet>();
			}
			return array;
		}

		//public static Timesheet[] ReadByEmployeestatusProjectAndDate(string clientId, string projectId, DateTime timesheetDate, string status)
		//{
		//	TimesheetInterface.<>c__DisplayClass4_0 variable = null;
		//	Timesheet[] array;
		//	using (ERPContext context = new ERPContext())
		//	{
		//		if (!string.IsNullOrEmpty(status) && !string.IsNullOrEmpty(projectId))
		//		{
		//			int num = Convert.ToInt32(projectId);
		//			array = (
		//				from t in QueryableExtensions.Include<Timesheet, Employee>(context.Timesheets, (Timesheet t) => t.employee)
		//				where (t.TimesheetDate == variable.timesheetDate) && t.ProjectId == (int?)num && t.employee.WorkStatus == variable.status
		//				select t).ToArray<Timesheet>();
		//		}
		//		else if (!string.IsNullOrEmpty(status) && !string.IsNullOrEmpty(clientId))
		//		{
		//			int num1 = Convert.ToInt32(clientId);
		//			array = (
		//				from t in QueryableExtensions.Include<Timesheet, Employee>(context.Timesheets, (Timesheet t) => t.employee)
		//				where (t.TimesheetDate == variable.timesheetDate) && t.ClientId == (int?)num1 && t.employee.WorkStatus == variable.status
		//				select t).ToArray<Timesheet>();
		//		}
		//		else if (!string.IsNullOrEmpty(projectId))
		//		{
		//			int num2 = Convert.ToInt32(projectId);
		//			array = (
		//				from t in QueryableExtensions.Include<Timesheet, Employee>(context.Timesheets, (Timesheet t) => t.employee)
		//				where (t.TimesheetDate == variable.timesheetDate) && t.ProjectId == (int?)num2
		//				select t).ToArray<Timesheet>();
		//		}
		//		else if (string.IsNullOrEmpty(clientId))
		//		{
		//			array = (string.IsNullOrEmpty(status) ? (
		//				from t in context.Timesheets
		//				where t.TimesheetDate == timesheetDate
		//				select t).ToArray<Timesheet>() : (
		//				from t in QueryableExtensions.Include<Timesheet, Employee>(context.Timesheets, (Timesheet t) => t.employee)
		//				where (t.TimesheetDate == timesheetDate) && t.employee.WorkStatus == status
		//				select t).ToArray<Timesheet>());
		//		}
		//		else
		//		{
		//			int num3 = Convert.ToInt32(clientId);
		//			array = (
		//				from t in QueryableExtensions.Include<Timesheet, Employee>(context.Timesheets, (Timesheet t) => t.employee)
		//				where (t.TimesheetDate == variable.timesheetDate) && t.ClientId == (int?)num3
		//				select t).ToArray<Timesheet>();
		//		}
		//	}
		//	return array;
		//}

		public static Timesheet[] ReadByEmployeeYear(int empId, int year)
		{
			Timesheet[] array;
			using (ERPContext context = new ERPContext())
			{
				array = (
					from t in context.Timesheets
					where t.TimesheetDate.Year == year && t.EmployeeId == empId
					select t).ToArray<Timesheet>();
			}
			return array;
		}

		public static Timesheet[] ReadByEmployeMonthYear(int empId, int month, int year)
		{
			Timesheet[] array;
			using (ERPContext context = new ERPContext())
			{
				array = (
					from t in context.Timesheets
					where t.TimesheetDate.Month == month && t.TimesheetDate.Year == year && t.EmployeeId == empId
					select t).ToArray<Timesheet>();
			}
			return array;
		}

		public static Timesheet[] ReadByProjectStartDateEndDate(DateTime startDate, DateTime endDate, int projectId)
		{
			Timesheet[] array;
			using (ERPContext context = new ERPContext())
			{
				array = (
					from t in context.Timesheets
					where (t.TimesheetDate >= startDate) && (t.TimesheetDate <= endDate) && t.ProjectId == (int?)projectId
					select t).ToArray<Timesheet>();
			}
			return array;
		}

		public static Timesheet[] ReadByStartDateEndDate(DateTime startDate, DateTime endDate)
		{
			Timesheet[] array;
			using (ERPContext context = new ERPContext())
			{
				array = (
					from t in context.Timesheets
					where (t.TimesheetDate >= startDate) && (t.TimesheetDate <= endDate)
					select t).ToArray<Timesheet>();
			}
			return array;
		}

		public static Timesheet[] ReayByEmployeeDate(int empId, DateTime startDate)
		{
			Timesheet[] array;
			using (ERPContext context = new ERPContext())
			{
				array = (
					from t in context.Timesheets
					where t.EmployeeId == empId && (t.TimesheetDate == startDate)
					select t).ToArray<Timesheet>();
			}
			return array;
		}

		public static void Update(Library.DualGlobe.ERP.Models.Timesheet Timesheet)
		{
			using (ERPContext context = new ERPContext())
			{
				context.get_Configuration().set_AutoDetectChangesEnabled(false);
				context.Entry<Library.DualGlobe.ERP.Models.Timesheet>(Timesheet).set_State(16);
				context.SaveChanges();
			}
		}

		public static void UpdateLeave(int empId, DateTime startdate, DateTime endDate)
		{
			using (ERPContext context = new ERPContext())
			{
				List<Timesheet> timesheetArray = (
					from t in context.Timesheets
					where t.EmployeeId == empId && (t.TimesheetDate >= startdate) && (t.TimesheetDate <= endDate)
					select t).ToList<Timesheet>();
				if (timesheetArray != null & timesheetArray.Any<Timesheet>())
				{
					foreach (Timesheet timesheet in timesheetArray)
					{
						context.get_Configuration().set_AutoDetectChangesEnabled(false);
						context.Entry<Timesheet>(timesheet).set_State(8);
						context.SaveChanges();
					}
				}
			}
		}
	}
}
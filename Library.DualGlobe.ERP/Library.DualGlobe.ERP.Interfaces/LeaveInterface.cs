using Library.DualGlobe.ERP.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Library.DualGlobe.ERP.Interfaces
{
	public class LeaveInterface
	{
		public LeaveInterface()
		{
		}

		public static void Create(Leave leave)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<Leave>(leave).set_State(4);
				context.SaveChanges();
			}
		}

		public static void Delete(int id)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<Leave>(context.Leaves.Find(new object[] { id })).set_State(8);
				context.SaveChanges();
			}
		}

		public static Leave[] Read()
		{
			Leave[] array;
			using (ERPContext context = new ERPContext())
			{
				array = context.Leaves.ToArray<Leave>();
			}
			return array;
		}

		public static Leave Read(int id)
		{
			Leave leave;
			using (ERPContext context = new ERPContext())
			{
				leave = (
					from i in context.Leaves
					where i.Id == id
					select i).FirstOrDefault<Leave>();
			}
			return leave;
		}

		public static Leave ReadByEmployeeAndDate(int id, DateTime LeaveDate)
		{
			Leave leave;
			using (ERPContext context = new ERPContext())
			{
				leave = (
					from i in context.Leaves
					where i.EmployeeId == id && (i.StartDate <= (DateTime?)LeaveDate) && (i.EndDate >= (DateTime?)LeaveDate)
					select i).FirstOrDefault<Leave>();
			}
			return leave;
		}

		public static Leave[] ReadByEmployeeYearAndMonth(int empId, int year, int month)
		{
			Leave[] array;
			using (ERPContext context = new ERPContext())
			{
				array = (
					from i in context.Leaves
					where i.EmployeeId == empId
					select i).ToArray<Leave>().Where<Leave>((Leave i) => {
					if (i.StartDate.Value.Month == month && i.StartDate.Value.Year == year)
					{
						return true;
					}
					if (i.EndDate.Value.Month != month)
					{
						return false;
					}
					return i.EndDate.Value.Year == year;
				}).ToArray<Leave>();
			}
			return array;
		}

		public static Leave[] ReadByYearAndMonth(int year, int month)
		{
			Leave[] array;
			using (ERPContext context = new ERPContext())
			{
				array = (
					from i in context.Leaves
					where i.StartDate.Value.Month == month && i.StartDate.Value.Year == year || i.EndDate.Value.Month == month && i.EndDate.Value.Year == year
					select i).ToArray<Leave>();
			}
			return array;
		}

		public static void Update(Leave leave)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<Leave>(leave).set_State(16);
				context.SaveChanges();
			}
		}
	}
}
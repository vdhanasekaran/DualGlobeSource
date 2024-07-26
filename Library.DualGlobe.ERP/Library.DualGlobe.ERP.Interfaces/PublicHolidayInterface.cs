using Library.DualGlobe.ERP.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Library.DualGlobe.ERP.Interfaces
{
	public class PublicHolidayInterface
	{
		public PublicHolidayInterface()
		{
		}

		public static void Create(PublicHoliday publicHoliday)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<PublicHoliday>(publicHoliday).set_State(4);
				context.SaveChanges();
			}
		}

		public static void Delete(int id)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<PublicHoliday>(context.PublicHolidays.Find(new object[] { id })).set_State(8);
				context.SaveChanges();
			}
		}

		public static PublicHoliday[] Read()
		{
			PublicHoliday[] array;
			using (ERPContext context = new ERPContext())
			{
				array = context.PublicHolidays.ToArray<PublicHoliday>();
			}
			return array;
		}

		public static PublicHoliday ReadByDate(DateTime date)
		{
			PublicHoliday publicHoliday;
			using (ERPContext context = new ERPContext())
			{
				publicHoliday = (
					from t in context.PublicHolidays
					where t.Date == date
					select t).FirstOrDefault<PublicHoliday>();
			}
			return publicHoliday;
		}

		public static PublicHoliday[] ReadByMonthYear(int month, int year)
		{
			PublicHoliday[] array;
			using (ERPContext context = new ERPContext())
			{
				array = (
					from i in context.PublicHolidays
					where i.Date.Year == year && i.Date.Month == month
					select i).ToArray<PublicHoliday>();
			}
			return array;
		}

		public static PublicHoliday[] ReadByYear(int year)
		{
			PublicHoliday[] array;
			using (ERPContext context = new ERPContext())
			{
				array = (
					from i in context.PublicHolidays
					where i.Date.Year == year
					select i).ToArray<PublicHoliday>();
			}
			return array;
		}

		public static void Update(PublicHoliday publicHoliday)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<PublicHoliday>(publicHoliday).set_State(16);
				context.SaveChanges();
			}
		}
	}
}
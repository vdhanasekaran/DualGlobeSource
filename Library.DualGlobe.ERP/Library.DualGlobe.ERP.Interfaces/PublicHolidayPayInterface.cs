using Library.DualGlobe.ERP.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Library.DualGlobe.ERP.Interfaces
{
	public class PublicHolidayPayInterface
	{
		public PublicHolidayPayInterface()
		{
		}

		public static void Create(PublicHolidayPay publicHolidayPay)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<PublicHolidayPay>(publicHolidayPay).set_State(4);
				context.SaveChanges();
			}
		}

		public static void Delete(int id)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<PublicHolidayPay>(context.PublicHolidayPays.Find(new object[] { id })).set_State(8);
				context.SaveChanges();
			}
		}

		public static PublicHolidayPay[] Read()
		{
			PublicHolidayPay[] array;
			using (ERPContext context = new ERPContext())
			{
				array = context.PublicHolidayPays.ToArray<PublicHolidayPay>();
			}
			return array;
		}

		public static PublicHolidayPay Read(int id)
		{
			PublicHolidayPay publicHolidayPay;
			using (ERPContext context = new ERPContext())
			{
				publicHolidayPay = (
					from i in context.PublicHolidayPays
					where i.Id == id
					select i).FirstOrDefault<PublicHolidayPay>();
			}
			return publicHolidayPay;
		}

		public static void Update(PublicHolidayPay publicHolidayPay)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<PublicHolidayPay>(publicHolidayPay).set_State(16);
				context.SaveChanges();
			}
		}
	}
}
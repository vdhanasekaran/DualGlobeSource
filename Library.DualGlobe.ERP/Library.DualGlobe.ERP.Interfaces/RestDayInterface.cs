using Library.DualGlobe.ERP.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Library.DualGlobe.ERP.Interfaces
{
	public class RestDayInterface
	{
		public RestDayInterface()
		{
		}

		public static void Create(RestDay restDay)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<RestDay>(restDay).set_State(4);
				context.SaveChanges();
			}
		}

		public static void Delete(int id)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<RestDay>(context.RestDays.Find(new object[] { id })).set_State(8);
				context.SaveChanges();
			}
		}

		public static void DeleteRestDateDetail(int id)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<RestDate>(context.RestDates.Find(new object[] { id })).set_State(8);
				context.SaveChanges();
			}
		}

		public static void InsertRestDateDetail(RestDate restDates)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<RestDate>(restDates).set_State(4);
				context.SaveChanges();
			}
		}

		public static RestDay[] Read()
		{
			RestDay[] array;
			using (ERPContext context = new ERPContext())
			{
				array = QueryableExtensions.Include<RestDay, List<RestDate>>(context.RestDays, (RestDay i) => i.RestDates).ToArray<RestDay>();
			}
			return array;
		}

		public static RestDay Read(int id)
		{
			RestDay restDay;
			using (ERPContext context = new ERPContext())
			{
				restDay = QueryableExtensions.Include<RestDay, List<RestDate>>(
					from i in context.RestDays
					where i.Id == id
					select i, (RestDay i) => i.RestDates).FirstOrDefault<RestDay>();
			}
			return restDay;
		}

		public static void Update(RestDay restDay)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<RestDay>(restDay).set_State(16);
				context.SaveChanges();
			}
		}

		public static void UpdateRestDateDetail(RestDate restDates)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<RestDate>(restDates).set_State(16);
				context.SaveChanges();
			}
		}
	}
}
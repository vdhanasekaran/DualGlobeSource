using Library.DualGlobe.ERP.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Library.DualGlobe.ERP.Interfaces
{
	public class RestDayPayInterface
	{
		public RestDayPayInterface()
		{
		}

		public static void Create(RestDayPay restDayPay)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<RestDayPay>(restDayPay).set_State(4);
				context.SaveChanges();
			}
		}

		public static void Delete(int id)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<RestDayPay>(context.RestDayPays.Find(new object[] { id })).set_State(8);
				context.SaveChanges();
			}
		}

		public static RestDayPay[] Read()
		{
			RestDayPay[] array;
			using (ERPContext context = new ERPContext())
			{
				array = context.RestDayPays.ToArray<RestDayPay>();
			}
			return array;
		}

		public static RestDayPay Read(int id)
		{
			RestDayPay restDayPay;
			using (ERPContext context = new ERPContext())
			{
				restDayPay = (
					from i in context.RestDayPays
					where i.Id == id
					select i).FirstOrDefault<RestDayPay>();
			}
			return restDayPay;
		}

		public static RestDayPay ReadByGroupName(string groupName)
		{
			RestDayPay restDayPay;
			using (ERPContext context = new ERPContext())
			{
				restDayPay = (
					from i in context.RestDayPays
					where string.Compare(i.GroupName, groupName, StringComparison.OrdinalIgnoreCase) == 0
					select i).FirstOrDefault<RestDayPay>();
			}
			return restDayPay;
		}

		public static void Update(RestDayPay restDayPay)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<RestDayPay>(restDayPay).set_State(16);
				context.SaveChanges();
			}
		}
	}
}
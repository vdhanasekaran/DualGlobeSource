using Library.DualGlobe.ERP.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Library.DualGlobe.ERP.Interfaces
{
	public class OvertimeInterface
	{
		public OvertimeInterface()
		{
		}

		public static void Create(Overtime overtime)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<Overtime>(overtime).set_State(4);
				context.SaveChanges();
			}
		}

		public static void Delete(int id)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<Overtime>(context.Overtimes.Find(new object[] { id })).set_State(8);
				context.SaveChanges();
			}
		}

		public static Overtime[] Read()
		{
			Overtime[] array;
			using (ERPContext context = new ERPContext())
			{
				array = context.Overtimes.ToArray<Overtime>();
			}
			return array;
		}

		public static Overtime Read(int id)
		{
			Overtime overtime;
			using (ERPContext context = new ERPContext())
			{
				overtime = (
					from i in context.Overtimes
					where i.Id == id
					select i).FirstOrDefault<Overtime>();
			}
			return overtime;
		}

		public static void Update(Overtime overtime)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<Overtime>(overtime).set_State(16);
				context.SaveChanges();
			}
		}
	}
}
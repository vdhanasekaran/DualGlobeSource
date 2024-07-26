using Library.DualGlobe.ERP.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Library.DualGlobe.ERP.Interfaces
{
	public class WorkingHourInterface
	{
		public WorkingHourInterface()
		{
		}

		public static void Create(WorkingHour workingHour)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<WorkingHour>(workingHour).set_State(4);
				context.SaveChanges();
			}
		}

		public static void Delete(int id)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<WorkingHour>(context.WorkingHours.Find(new object[] { id })).set_State(8);
				context.SaveChanges();
			}
		}

		public static WorkingHour[] Read()
		{
			WorkingHour[] array;
			using (ERPContext context = new ERPContext())
			{
				array = context.WorkingHours.ToArray<WorkingHour>();
			}
			return array;
		}

		public static WorkingHour Read(int id)
		{
			WorkingHour workingHour;
			using (ERPContext context = new ERPContext())
			{
				workingHour = (
					from i in context.WorkingHours
					where i.Id == id
					select i).FirstOrDefault<WorkingHour>();
			}
			return workingHour;
		}

		public static void Update(WorkingHour workingHour)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<WorkingHour>(workingHour).set_State(16);
				context.SaveChanges();
			}
		}
	}
}
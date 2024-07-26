using Library.DualGlobe.ERP.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Library.DualGlobe.ERP.Interfaces
{
	public class WorkPermitAddressInterface
	{
		public WorkPermitAddressInterface()
		{
		}

		public static void Create(WorkPermitAddress workPermitAddress)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<WorkPermitAddress>(workPermitAddress).set_State(4);
				context.SaveChanges();
			}
		}

		public static void Delete(int id)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<WorkPermitAddress>(context.WorkPermitAddresses.Find(new object[] { id })).set_State(8);
				context.SaveChanges();
			}
		}

		public static WorkPermitAddress[] Read()
		{
			WorkPermitAddress[] array;
			using (ERPContext context = new ERPContext())
			{
				array = context.WorkPermitAddresses.ToArray<WorkPermitAddress>();
			}
			return array;
		}

		public static WorkPermitAddress Read(int id)
		{
			WorkPermitAddress workPermitAddress;
			using (ERPContext context = new ERPContext())
			{
				workPermitAddress = (
					from i in context.WorkPermitAddresses
					where i.Id == id
					select i).FirstOrDefault<WorkPermitAddress>();
			}
			return workPermitAddress;
		}

		public static void Update(WorkPermitAddress workPermitAddress)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<WorkPermitAddress>(workPermitAddress).set_State(16);
				context.SaveChanges();
			}
		}
	}
}
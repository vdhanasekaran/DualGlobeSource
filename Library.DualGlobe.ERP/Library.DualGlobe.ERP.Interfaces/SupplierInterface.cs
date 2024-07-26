using Library.DualGlobe.ERP.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Library.DualGlobe.ERP.Interfaces
{
	public class SupplierInterface
	{
		public SupplierInterface()
		{
		}

		public static void Create(Supplier supplier)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<Supplier>(supplier).set_State(4);
				context.SaveChanges();
			}
		}

		public static void Delete(int id)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<Supplier>(context.Suppliers.Find(new object[] { id })).set_State(8);
				context.SaveChanges();
			}
		}

		public static int GetCount()
		{
			int num;
			using (ERPContext context = new ERPContext())
			{
				num = context.Suppliers.Count<Supplier>();
			}
			return num;
		}

		public static Supplier[] Read()
		{
			Supplier[] array;
			using (ERPContext context = new ERPContext())
			{
				array = context.Suppliers.ToArray<Supplier>();
			}
			return array;
		}

		public static Supplier Read(int id)
		{
			Supplier supplier;
			using (ERPContext context = new ERPContext())
			{
				supplier = (
					from t in context.Suppliers
					where t.Id == id
					select t).FirstOrDefault<Supplier>();
			}
			return supplier;
		}

		public static void Update(Supplier supplier)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<Supplier>(supplier).set_State(16);
				context.SaveChanges();
			}
		}
	}
}
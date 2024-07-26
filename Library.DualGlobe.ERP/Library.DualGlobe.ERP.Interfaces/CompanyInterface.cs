using Library.DualGlobe.ERP.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Library.DualGlobe.ERP.Interfaces
{
	public class CompanyInterface
	{
		public CompanyInterface()
		{
		}

		public static void Create(Company company)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<Company>(company).set_State(4);
				context.SaveChanges();
			}
		}

		public static void Delete(int id)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<Company>(context.Companies.Find(new object[] { id })).set_State(8);
				context.SaveChanges();
			}
		}

		public static int GetCount()
		{
			int num;
			using (ERPContext context = new ERPContext())
			{
				num = context.Companies.Count<Company>((Company t) => t.Status);
			}
			return num;
		}

		public static Company[] Read()
		{
			Company[] array;
			using (ERPContext context = new ERPContext())
			{
				array = (
					from t in context.Companies
					where t.Status
					select t).ToArray<Company>();
			}
			return array;
		}

		public static Company Read(int id)
		{
			Company company;
			using (ERPContext context = new ERPContext())
			{
				company = (
					from t in context.Companies
					where t.Id == id && t.Status
					select t).FirstOrDefault<Company>();
			}
			return company;
		}

		public static void Update(Company company)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<Company>(company).set_State(16);
				context.SaveChanges();
			}
		}
	}
}
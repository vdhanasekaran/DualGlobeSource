using Library.DualGlobe.ERP.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Library.DualGlobe.ERP.Interfaces
{
	public class RoleInterface
	{
		public RoleInterface()
		{
		}

		public static void Create(Role role)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<Role>(role).set_State(4);
				context.SaveChanges();
			}
		}

		public static void Delete(int id)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<Role>(context.Roles.Find(new object[] { id })).set_State(8);
				context.SaveChanges();
			}
		}

		public static Role[] Read()
		{
			Role[] array;
			using (ERPContext context = new ERPContext())
			{
				array = context.Roles.ToArray<Role>();
			}
			return array;
		}

		public static Role Read(int id)
		{
			Role role;
			using (ERPContext context = new ERPContext())
			{
				role = (
					from t in context.Roles
					where t.Id == id
					select t).FirstOrDefault<Role>();
			}
			return role;
		}

		public static void Update(Role role)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<Role>(role).set_State(16);
				context.SaveChanges();
			}
		}
	}
}
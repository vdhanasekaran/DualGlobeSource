using Library.DualGlobe.ERP.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Library.DualGlobe.ERP.Interfaces
{
	public class UserRoleInterface
	{
		public UserRoleInterface()
		{
		}

		public static void Create(UserRole userRole)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<UserRole>(userRole).set_State(4);
				context.SaveChanges();
			}
		}

		public static void Delete(int id)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<UserRole>(context.UserRoles.Find(new object[] { id })).set_State(8);
				context.SaveChanges();
			}
		}

		public static UserRole[] Read()
		{
			UserRole[] array;
			using (ERPContext context = new ERPContext())
			{
				array = context.UserRoles.ToArray<UserRole>();
			}
			return array;
		}

		public static UserRole Read(int id)
		{
			UserRole userRole;
			using (ERPContext context = new ERPContext())
			{
				userRole = (
					from t in context.UserRoles
					where t.Id == id
					select t).FirstOrDefault<UserRole>();
			}
			return userRole;
		}

		public static UserRole[] ReadByUserRolename(string userId)
		{
			UserRole[] array;
			using (ERPContext context = new ERPContext())
			{
				array = QueryableExtensions.Include<UserRole, Role>(
					from x in context.UserRoles
					where x.UserId == userId
					select x, (UserRole x) => x.Role).ToArray<UserRole>();
			}
			return array;
		}

		public static void Update(UserRole userRole)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<UserRole>(userRole).set_State(16);
				context.SaveChanges();
			}
		}
	}
}
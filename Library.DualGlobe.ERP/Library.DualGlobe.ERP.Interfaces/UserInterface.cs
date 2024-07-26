using Library.DualGlobe.ERP.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Library.DualGlobe.ERP.Interfaces
{
	public class UserInterface
	{
		public UserInterface()
		{
		}

		public static void Create(User user)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<User>(user).set_State(4);
				context.SaveChanges();
			}
		}

		public static void Delete(int id)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<User>(context.Users.Find(new object[] { id })).set_State(8);
				context.SaveChanges();
			}
		}

		public static User[] Read()
		{
			User[] array;
			using (ERPContext context = new ERPContext())
			{
				array = QueryableExtensions.Include<User, Employee>(context.Users, (User x) => x.employee).ToArray<User>();
			}
			return array;
		}

		public static User Read(int id)
		{
			User user;
			using (ERPContext context = new ERPContext())
			{
				user = QueryableExtensions.Include<User, Employee>(
					from t in context.Users
					where t.Id == id
					select t, (User x) => x.employee).FirstOrDefault<User>();
			}
			return user;
		}

		public static User ReadByUsername(string username)
		{
			User user;
			using (ERPContext context = new ERPContext())
			{
				user = QueryableExtensions.Include<User, Employee>(
					from t in context.Users
					where t.UserId == username
					select t, (User x) => x.employee).FirstOrDefault<User>();
			}
			return user;
		}

		public static void Update(User user)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<User>(user).set_State(16);
				context.SaveChanges();
			}
		}
	}
}
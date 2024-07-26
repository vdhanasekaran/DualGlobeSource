using Library.DualGlobe.ERP.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Library.DualGlobe.ERP.Interfaces
{
	public class RoleMenuInterface
	{
		public RoleMenuInterface()
		{
		}

		public static void Create(RoleMenu roleMenu)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<RoleMenu>(roleMenu).set_State(4);
				context.SaveChanges();
			}
		}

		public static void Delete(int id)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<RoleMenu>(context.RoleMenus.Find(new object[] { id })).set_State(8);
				context.SaveChanges();
			}
		}

		public static List<RoleMenu> Read()
		{
			List<RoleMenu> list;
			using (ERPContext context = new ERPContext())
			{
				list = context.RoleMenus.ToList<RoleMenu>();
			}
			return list;
		}

		public static RoleMenu Read(int id)
		{
			RoleMenu roleMenu;
			using (ERPContext context = new ERPContext())
			{
				roleMenu = (
					from t in context.RoleMenus
					where t.Id == id
					select t).FirstOrDefault<RoleMenu>();
			}
			return roleMenu;
		}

		public static RoleMenu ReadByRoleId(int roleID)
		{
			RoleMenu roleMenu;
			using (ERPContext context = new ERPContext())
			{
				roleMenu = (
					from t in context.RoleMenus
					where t.RoleId == roleID
					select t).FirstOrDefault<RoleMenu>();
			}
			return roleMenu;
		}

		public static void Update(RoleMenu roleMenu)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<RoleMenu>(roleMenu).set_State(16);
				context.SaveChanges();
			}
		}
	}
}
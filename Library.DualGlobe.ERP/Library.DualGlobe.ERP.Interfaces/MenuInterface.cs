using Library.DualGlobe.ERP.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Library.DualGlobe.ERP.Interfaces
{
	public class MenuInterface
	{
		public MenuInterface()
		{
		}

		public static void Create(Menu menu)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<Menu>(menu).set_State(4);
				context.SaveChanges();
			}
		}

		public static void Delete(int id)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<Menu>(context.Menus.Find(new object[] { id })).set_State(8);
				context.SaveChanges();
			}
		}

		public static Menu[] Read()
		{
			Menu[] array;
			using (ERPContext context = new ERPContext())
			{
				array = context.Menus.ToArray<Menu>();
			}
			return array;
		}

		public static Menu Read(int id)
		{
			Menu menu;
			using (ERPContext context = new ERPContext())
			{
				menu = (
					from t in context.Menus
					where t.Id == id
					select t).FirstOrDefault<Menu>();
			}
			return menu;
		}

		public static void Update(Menu menu)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<Menu>(menu).set_State(16);
				context.SaveChanges();
			}
		}
	}
}
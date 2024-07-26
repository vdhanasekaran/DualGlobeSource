using Library.DualGlobe.ERP.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Library.DualGlobe.ERP.Interfaces
{
	public class ProjectInterface
	{
		public ProjectInterface()
		{
		}

		public static void Create(Project project)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<Project>(project).set_State(4);
				context.SaveChanges();
			}
		}

		public static void Delete(int id)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<Project>(context.Projects.Find(new object[] { id })).set_State(8);
				context.SaveChanges();
			}
		}

		public static int GetCount()
		{
			int num;
			using (ERPContext context = new ERPContext())
			{
				num = context.Projects.Count<Project>();
			}
			return num;
		}

		public static Project[] Read()
		{
			Project[] array;
			using (ERPContext context = new ERPContext())
			{
				array = QueryableExtensions.Include<Project, Client>(context.Projects, (Project c) => c.client).ToArray<Project>();
			}
			return array;
		}

		public static Project Read(int id)
		{
			Project project;
			using (ERPContext context = new ERPContext())
			{
				project = QueryableExtensions.Include<Project, Client>(
					from t in context.Projects
					where t.Id == id
					select t, (Project c) => c.client).FirstOrDefault<Project>();
			}
			return project;
		}

		public static Project[] ReadByClientId(int clientId)
		{
			Project[] array;
			using (ERPContext context = new ERPContext())
			{
				array = (
					from p in QueryableExtensions.Include<Project, Client>(
						from t in context.Projects
						where t.ClientId == clientId
						select t, (Project c) => c.client)
					orderby p.Id
					select p).ToArray<Project>();
			}
			return array;
		}

		public static void Update(Project project)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<Project>(project).set_State(16);
				context.SaveChanges();
			}
		}
	}
}
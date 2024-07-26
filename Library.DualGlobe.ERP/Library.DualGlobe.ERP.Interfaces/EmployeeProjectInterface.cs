using Library.DualGlobe.ERP.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Library.DualGlobe.ERP.Interfaces
{
	public class EmployeeProjectInterface
	{
		public EmployeeProjectInterface()
		{
		}

		public static bool Create(EmployeeProject employeeProject)
		{
			bool flag;
			using (ERPContext context = new ERPContext())
			{
				if (QueryableExtensions.Include<EmployeeProject, Project>(QueryableExtensions.Include<EmployeeProject, Employee>(context.EmployeeProjects, (EmployeeProject x) => x.employee), (EmployeeProject x) => x.project).ToArray<EmployeeProject>().FirstOrDefault<EmployeeProject>((EmployeeProject x) => {
					if (x.EmployeeId != employeeProject.EmployeeId)
					{
						return false;
					}
					return x.projectId == employeeProject.projectId;
				}) != null)
				{
					flag = false;
				}
				else
				{
					context.Entry<EmployeeProject>(employeeProject).set_State(4);
					context.SaveChanges();
					flag = true;
				}
			}
			return flag;
		}

		public static void Delete(int id)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<EmployeeProject>(context.EmployeeProjects.Find(new object[] { id })).set_State(8);
				context.SaveChanges();
			}
		}

		public static EmployeeProject[] Read()
		{
			EmployeeProject[] array;
			using (ERPContext context = new ERPContext())
			{
				array = QueryableExtensions.Include<EmployeeProject, Project>(QueryableExtensions.Include<EmployeeProject, Employee>(context.EmployeeProjects, (EmployeeProject x) => x.employee), (EmployeeProject x) => x.project).ToArray<EmployeeProject>();
			}
			return array;
		}

		public static EmployeeProject Read(int id)
		{
			EmployeeProject employeeProject;
			using (ERPContext context = new ERPContext())
			{
				employeeProject = (
					from t in context.EmployeeProjects
					where t.Id == id
					select t).FirstOrDefault<EmployeeProject>();
			}
			return employeeProject;
		}

		public static EmployeeProject[] ReadByEmployee(int id)
		{
			EmployeeProject[] array;
			using (ERPContext context = new ERPContext())
			{
				array = (
					from t in context.EmployeeProjects
					where t.EmployeeId == id
					select t).ToArray<EmployeeProject>();
			}
			return array;
		}

		public static int ReadByEmployeeId(int empId)
		{
			int num;
			using (ERPContext context = new ERPContext())
			{
				num = (
					from t in context.EmployeeProjects
					where t.EmployeeId == empId
					select t).FirstOrDefault<EmployeeProject>().projectId;
			}
			return num;
		}

		public static EmployeeProject[] ReadByProject(int id)
		{
			EmployeeProject[] array;
			using (ERPContext context = new ERPContext())
			{
				array = (
					from t in QueryableExtensions.Include<EmployeeProject, Employee>(context.EmployeeProjects, (EmployeeProject x) => x.employee)
					where t.projectId == id
					select t).ToArray<EmployeeProject>();
			}
			return array;
		}

		public static EmployeeProject[] ReadByProjectAndEmployeeStatus(int id, string employeeStatus)
		{
			EmployeeProject[] array;
			using (ERPContext context = new ERPContext())
			{
				array = (
					from t in QueryableExtensions.Include<EmployeeProject, Employee>(context.EmployeeProjects, (EmployeeProject x) => x.employee)
					where t.projectId == id && t.employee.WorkStatus == employeeStatus
					select t).ToArray<EmployeeProject>();
			}
			return array;
		}

		public static void Update(EmployeeProject employeeProject)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<EmployeeProject>(employeeProject).set_State(16);
				context.SaveChanges();
			}
		}
	}
}
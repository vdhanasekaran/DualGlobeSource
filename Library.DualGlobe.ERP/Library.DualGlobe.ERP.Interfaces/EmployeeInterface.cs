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
	public class EmployeeInterface
	{
		public EmployeeInterface()
		{
		}

		public static void Create(Employee employee)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<Employee>(employee).set_State(4);
				context.SaveChanges();
			}
		}

		public static void Delete(int id)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<Employee>(context.Employees.Find(new object[] { id })).set_State(8);
				context.SaveChanges();
			}
		}

		public static void DeleteDocument(int id)
		{
			using (ERPContext context = new ERPContext())
			{
				EmployeeDocument document = context.Employees.SelectMany<Employee, EmployeeDocument>((Employee t) => t.EmployeeDocuments).Where<EmployeeDocument>((EmployeeDocument j) => j.Id == id).FirstOrDefault<EmployeeDocument>();
				context.Entry<EmployeeDocument>(document).set_State(8);
				context.SaveChanges();
			}
		}

		public static int GetCount()
		{
			int num;
			using (ERPContext context = new ERPContext())
			{
				num = context.Employees.Count<Employee>();
			}
			return num;
		}

		public static void InsertDocument(EmployeeDocument record, Employee parent)
		{
			using (ERPContext context = new ERPContext())
			{
				record.Employee = parent;
				context.Entry<Employee>(parent).set_State(2);
				context.Entry<EmployeeDocument>(record).set_State(4);
				context.SaveChanges();
			}
		}

		public static Employee[] Read()
		{
			Employee[] array;
			using (ERPContext context = new ERPContext())
			{
				array = QueryableExtensions.Include<Employee, List<EmployeeDocument>>(
					from t in context.Employees
					where t.Status == "true"
					select t, (Employee t) => t.EmployeeDocuments).ToArray<Employee>();
			}
			return array;
		}

		public static Employee Read(int id)
		{
			Employee employee;
			using (ERPContext context = new ERPContext())
			{
				employee = QueryableExtensions.Include<Employee, List<EmployeeDocument>>(QueryableExtensions.Include<Employee, List<SalaryDetail>>(QueryableExtensions.Include<Employee, List<Timesheet>>(QueryableExtensions.Include<Employee, List<LoanAndAdvance>>(QueryableExtensions.Include<Employee, List<Allowance>>(
					from t in context.Employees
					where t.Id == id
					select t, (Employee x) => x.allowance), (Employee x) => x.loanAndAdvance), (Employee x) => x.timesheet), (Employee x) => x.salaryDetail), (Employee t) => t.EmployeeDocuments).FirstOrDefault<Employee>();
			}
			return employee;
		}

		public static Employee[] ReadAll()
		{
			Employee[] array;
			using (ERPContext context = new ERPContext())
			{
				array = QueryableExtensions.Include<Employee, List<EmployeeDocument>>(QueryableExtensions.Include<Employee, List<SalaryDetail>>(QueryableExtensions.Include<Employee, List<Timesheet>>(QueryableExtensions.Include<Employee, List<LoanAndAdvance>>(QueryableExtensions.Include<Employee, List<Allowance>>(context.Employees, (Employee x) => x.allowance), (Employee x) => x.loanAndAdvance), (Employee x) => x.timesheet), (Employee x) => x.salaryDetail), (Employee t) => t.EmployeeDocuments).ToArray<Employee>();
			}
			return array;
		}

		public static Employee ReadByEmpId(int id)
		{
			Employee employee;
			using (ERPContext context = new ERPContext())
			{
				employee = QueryableExtensions.Include<Employee, List<EmployeeDocument>>(
					from t in context.Employees
					where t.Id == id
					select t, (Employee t) => t.EmployeeDocuments).FirstOrDefault<Employee>();
			}
			return employee;
		}

		public static object ReadByEmployeeStatus(string employeeStatus)
		{
			throw new NotImplementedException();
		}

		public static Employee[] ReadByWorkStatus(string workStatus)
		{
			Employee[] array;
			using (ERPContext context = new ERPContext())
			{
				array = (
					from t in context.Employees
					where t.WorkStatus == workStatus
					select t).ToArray<Employee>();
			}
			return array;
		}

		public static List<Employee> ReadByWorkStatus()
		{
			List<Employee> list;
			using (ERPContext context = new ERPContext())
			{
				list = (
					from t in context.Employees
					where t.WorkStatus == "SingaporeCitizen" || t.WorkStatus == "SingaporePR"
					select t).ToList<Employee>();
			}
			return list;
		}

		public static void Update(Employee employee)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<Employee>(employee).set_State(16);
				context.SaveChanges();
			}
		}
	}
}
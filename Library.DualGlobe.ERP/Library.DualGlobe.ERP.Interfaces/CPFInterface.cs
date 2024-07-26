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
	public class CPFInterface
	{
		public CPFInterface()
		{
		}

		public static CPF[] BulkInsert(List<CPF> cpf)
		{
			CPF[] array;
			using (ERPContext context = new ERPContext())
			{
				context.CPFs.AddRange(cpf);
				context.SaveChanges();
				array = cpf.ToArray();
			}
			return array;
		}

		public static void Create(CPF cpf)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<CPF>(cpf).set_State(4);
				context.SaveChanges();
			}
		}

		public static void Delete(int id)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<CPF>(context.CPFs.Find(new object[] { id })).set_State(8);
				context.SaveChanges();
			}
		}

		public static CPF[] Read()
		{
			CPF[] array;
			using (ERPContext context = new ERPContext())
			{
				array = context.CPFs.ToArray<CPF>();
			}
			return array;
		}

		public static CPF Read(int id)
		{
			CPF cPF;
			using (ERPContext context = new ERPContext())
			{
				cPF = (
					from i in context.CPFs
					where i.Id == id
					select i).FirstOrDefault<CPF>();
			}
			return cPF;
		}

		public static CPF ReadByEmployee(int id)
		{
			CPF cPF;
			using (ERPContext context = new ERPContext())
			{
				cPF = (
					from i in context.CPFs
					where i.EmployeeId == id && i.Date.Month == DateTime.Today.Month && i.Date.Year == DateTime.Today.Year
					select i).FirstOrDefault<CPF>();
			}
			return cPF;
		}

		public static CPF ReadByEmployeeYearAndMonth(int empId, int year, int month)
		{
			CPF cPF;
			using (ERPContext context = new ERPContext())
			{
				cPF = (
					from i in context.CPFs
					where i.EmployeeId == empId && i.Date.Month == month && i.Date.Year == year
					select i).FirstOrDefault<CPF>();
			}
			return cPF;
		}

		public static CPF[] ReadByYearAndMonth(int year, int month)
		{
			CPF[] array;
			using (ERPContext context = new ERPContext())
			{
				array = (
					from i in context.CPFs
					where i.Date.Month == month && i.Date.Year == year
					select i).ToArray<CPF>();
			}
			return array;
		}

		public static CPFLookup[] ReadLookupValbyAgeYear(int prYearCount, int age)
		{
			CPFLookup[] array;
			using (ERPContext context = new ERPContext())
			{
				array = (
					from i in context.CPFLookups
					where i.CPFLookUpYear == (int?)prYearCount && (i.EffectiveDate <= DateTime.Today) && i.AgeMaxVal > (int?)age && i.AgeMinVal < (int?)age
					select i).ToArray<CPFLookup>();
			}
			return array;
		}

		public static void Update(CPF cpf)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<CPF>(cpf).set_State(16);
				context.SaveChanges();
			}
		}
	}
}
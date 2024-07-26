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
	public class InsuranceInterface
	{
		public InsuranceInterface()
		{
		}

		public static void Create(Insurance insurance)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<Insurance>(insurance).set_State(4);
				context.SaveChanges();
			}
		}

		public static void Delete(int id)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<Insurance>(context.Insurances.Find(new object[] { id })).set_State(8);
				context.SaveChanges();
			}
		}

		public static Insurance[] Read()
		{
			Insurance[] array;
			using (ERPContext context = new ERPContext())
			{
				array = context.Insurances.ToArray<Insurance>();
			}
			return array;
		}

		public static Insurance Read(int id)
		{
			Insurance insurance;
			using (ERPContext context = new ERPContext())
			{
				insurance = (
					from i in context.Insurances
					where i.Id == id
					select i).FirstOrDefault<Insurance>();
			}
			return insurance;
		}

		public static Insurance ReadByPolicyNumber(string policyNumber)
		{
			Insurance insurance;
			using (ERPContext context = new ERPContext())
			{
				insurance = (
					from i in context.Insurances
					where i.InsurancePolicyNumber == policyNumber
					select i).FirstOrDefault<Insurance>();
			}
			return insurance;
		}

		public static Insurance[] ReadByType(string type)
		{
			Insurance[] array;
			using (ERPContext context = new ERPContext())
			{
				array = (
					from i in context.Insurances
					where i.InsuranceType == type
					select i).ToArray<Insurance>();
			}
			return array;
		}

		public static Insurance[] ReadExpiringInsurance()
		{
			List<Insurance> insuranceList = new List<Insurance>();
			using (ERPContext context = new ERPContext())
			{
				foreach (Insurance data in context.Insurances.ToList<Insurance>())
				{
					if ((data.EndDate.Value - DateTime.Today).Days >= 30)
					{
						continue;
					}
					insuranceList.Add(data);
				}
			}
			return insuranceList.ToArray();
		}

		public static void Update(Insurance insurance)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<Insurance>(insurance).set_State(16);
				context.SaveChanges();
			}
		}
	}
}
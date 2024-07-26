using Library.DualGlobe.ERP.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Library.DualGlobe.ERP.Interfaces
{
	public class DonationInterface
	{
		public DonationInterface()
		{
		}

		public static void Create(Donation donation)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<Donation>(donation).set_State(4);
				context.SaveChanges();
			}
		}

		public static void Delete(int id)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<Donation>(context.Donations.Find(new object[] { id })).set_State(8);
				context.SaveChanges();
			}
		}

		public static Donation[] Read()
		{
			Donation[] array;
			using (ERPContext context = new ERPContext())
			{
				array = context.Donations.ToArray<Donation>();
			}
			return array;
		}

		public static Donation Read(int id)
		{
			Donation donation;
			using (ERPContext context = new ERPContext())
			{
				donation = (
					from t in context.Donations
					where t.Id == id
					select t).FirstOrDefault<Donation>();
			}
			return donation;
		}

		public static Donation[] ReadbyDonationType(string donationType)
		{
			Donation[] array;
			using (ERPContext context = new ERPContext())
			{
				array = (
					from t in context.Donations
					where t.DonationType == donationType
					select t).ToArray<Donation>();
			}
			return array;
		}

		public static Donation[] ReadbyNationality(string nationality)
		{
			Donation[] array;
			using (ERPContext context = new ERPContext())
			{
				array = (
					from t in context.Donations
					where t.Nationality == nationality
					select t).ToArray<Donation>();
			}
			return array;
		}

		public static Donation[] ReadbyReligion(string religion)
		{
			Donation[] array;
			using (ERPContext context = new ERPContext())
			{
				array = (
					from t in context.Donations
					where t.Religion == religion
					select t).ToArray<Donation>();
			}
			return array;
		}

		public static void Update(Donation donation)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<Donation>(donation).set_State(16);
				context.SaveChanges();
			}
		}
	}
}
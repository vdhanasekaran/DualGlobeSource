using Library.DualGlobe.ERP.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Library.DualGlobe.ERP.Interfaces
{
	public class PaymentInterface
	{
		public PaymentInterface()
		{
		}

		public static void Create(Payment payment)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<Payment>(payment).set_State(4);
				context.SaveChanges();
			}
		}

		public static void Delete(int id)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<Payment>(context.Payments.Find(new object[] { id })).set_State(8);
				context.SaveChanges();
			}
		}

		public static Payment[] Read()
		{
			Payment[] array;
			using (ERPContext context = new ERPContext())
			{
				array = context.Payments.ToArray<Payment>();
			}
			return array;
		}

		public static Payment Read(int id)
		{
			Payment payment;
			using (ERPContext context = new ERPContext())
			{
				payment = (
					from t in context.Payments
					where t.Id == id
					select t).FirstOrDefault<Payment>();
			}
			return payment;
		}

		public static Payment[] ReadByInvoice(int invoiceId)
		{
			Payment[] array;
			using (ERPContext context = new ERPContext())
			{
				array = (
					from t in context.Payments
					where t.InvoiceId == (int?)invoiceId
					select t).ToArray<Payment>();
			}
			return array;
		}

		public static void Update(Payment payment)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<Payment>(payment).set_State(16);
				context.SaveChanges();
			}
		}
	}
}
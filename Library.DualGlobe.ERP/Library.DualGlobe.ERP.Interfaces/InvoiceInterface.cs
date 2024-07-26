using Library.DualGlobe.ERP.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Library.DualGlobe.ERP.Interfaces
{
	public class InvoiceInterface
	{
		public InvoiceInterface()
		{
		}

		public static void Create(Invoice invoice)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<Invoice>(invoice).set_State(4);
				context.SaveChanges();
			}
		}

		public static void CreateInvoicePhase(InvoicePhase invoicePhase)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<InvoicePhase>(invoicePhase).set_State(4);
				context.SaveChanges();
			}
		}

		public static void CreateInvoicePhaseItem(InvoicePhase InvoicePhaseItem)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<InvoicePhase>(InvoicePhaseItem).set_State(4);
				context.SaveChanges();
			}
		}

		public static void Delete(int id)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<Invoice>(context.Invoices.Find(new object[] { id })).set_State(8);
				context.SaveChanges();
			}
		}

		public static void DeleteInvoicePhase(int id)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<InvoicePhase>(context.InvoicePhases.Find(new object[] { id })).set_State(8);
				context.SaveChanges();
			}
		}

		public static int GetCount()
		{
			int num;
			using (ERPContext context = new ERPContext())
			{
				num = context.Invoices.Count<Invoice>((Invoice t) => t.Status == "UnPaid");
			}
			return num;
		}

		public static Invoice[] Read()
		{
			Invoice[] array;
			using (ERPContext context = new ERPContext())
			{
				array = QueryableExtensions.Include<Invoice, List<InvoicePhase>>(context.Invoices, (Invoice t) => t.InvoicePhases).ToArray<Invoice>();
			}
			return array;
		}

		public static Invoice Read(int id)
		{
			Invoice invoice;
			using (ERPContext context = new ERPContext())
			{
				invoice = QueryableExtensions.Include<Invoice, List<Payment>>(QueryableExtensions.Include<Invoice, List<InvoicePhase>>(
					from t in context.Invoices
					where t.Id == id
					select t, (Invoice t) => t.InvoicePhases), (Invoice t) => t.Payments).FirstOrDefault<Invoice>();
			}
			return invoice;
		}

		public static Invoice[] ReadByQuotationId(int id)
		{
			Invoice[] array;
			using (ERPContext context = new ERPContext())
			{
				array = QueryableExtensions.Include<Invoice, List<Payment>>(QueryableExtensions.Include<Invoice, List<InvoicePhase>>(
					from i in context.Invoices
					where i.QuotationId == (int?)id
					select i, (Invoice t) => t.InvoicePhases), (Invoice t) => t.Payments).ToArray<Invoice>();
			}
			return array;
		}

		public static Invoice[] ReadByStartDateEndDate(DateTime startDate, DateTime endDate)
		{
			Invoice[] array;
			using (ERPContext context = new ERPContext())
			{
				array = QueryableExtensions.Include<Invoice, List<Payment>>(QueryableExtensions.Include<Invoice, List<InvoicePhase>>(
					from i in context.Invoices
					where (i.InvoiceDate.Value >= startDate) && (i.InvoiceDate.Value <= endDate)
					select i, (Invoice t) => t.InvoicePhases), (Invoice t) => t.Payments).ToArray<Invoice>();
			}
			return array;
		}

		public static InvoicePhase[] ReadInvoicePhaseByInvoice(int id)
		{
			InvoicePhase[] array;
			using (ERPContext context = new ERPContext())
			{
				array = (
					from i in context.InvoicePhases
					where i.InvoiceId == id
					select i).ToArray<InvoicePhase>();
			}
			return array;
		}

		public static void Update(Library.DualGlobe.ERP.Models.Invoice Invoice)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<Library.DualGlobe.ERP.Models.Invoice>(Invoice).set_State(16);
				context.SaveChanges();
			}
		}

		public static void UpdateInvoicePhase(InvoicePhase invoicePhase)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<InvoicePhase>(invoicePhase).set_State(16);
				context.SaveChanges();
			}
		}

		public static void UpdateInvoicePhaseItem(InvoicePhase InvoicePhaseItem)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<InvoicePhase>(InvoicePhaseItem).set_State(16);
				context.SaveChanges();
			}
		}
	}
}
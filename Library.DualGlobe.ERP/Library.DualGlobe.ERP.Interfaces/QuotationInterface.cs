using Library.DualGlobe.ERP.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Library.DualGlobe.ERP.Interfaces
{
	public class QuotationInterface
	{
		public QuotationInterface()
		{
		}

		public static void Create(Quotation quotation)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<Quotation>(quotation).set_State(4);
				context.SaveChanges();
			}
		}

		public static void Delete(int id)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<Quotation>(context.Quotations.Find(new object[] { id })).set_State(8);
				context.SaveChanges();
			}
		}

		public static void DeleteQuotationDetail(int id)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<QuotationItems>(context.QuotationItems.Find(new object[] { id })).set_State(8);
				context.SaveChanges();
			}
		}

		public static int GetCount()
		{
			int num;
			using (ERPContext context = new ERPContext())
			{
				num = context.Quotations.Count<Quotation>();
			}
			return num;
		}

		public static void InsertquotationDetail(QuotationItems quotationItems)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<QuotationItems>(quotationItems).set_State(4);
				context.SaveChanges();
			}
		}

		public static Quotation[] Read()
		{
			Quotation[] array;
			using (ERPContext context = new ERPContext())
			{
				array = QueryableExtensions.Include<Quotation, List<QuotationItems>>(context.Quotations, (Quotation t) => t.quotationItems).ToArray<Quotation>();
			}
			return array;
		}

		public static Quotation Read(int id)
		{
			Quotation quotation;
			using (ERPContext context = new ERPContext())
			{
				quotation = QueryableExtensions.Include<Quotation, List<QuotationItems>>(
					from t in context.Quotations
					where t.Id == id
					select t, (Quotation t) => t.quotationItems).FirstOrDefault<Quotation>();
			}
			return quotation;
		}

		public static Quotation[] ReadByClientId(int clientId)
		{
			Quotation[] array;
			using (ERPContext context = new ERPContext())
			{
				array = (
					from t in context.Quotations
					where t.ClientId == clientId
					select t into p
					orderby p.Id
					select p).ToArray<Quotation>();
			}
			return array;
		}

		public static Quotation[] ReadByProjectId(int projectId)
		{
			Quotation[] array;
			using (ERPContext context = new ERPContext())
			{
				array = (
					from t in context.Quotations
					where t.ProjectId == projectId
					select t into p
					orderby p.Id
					select p).ToArray<Quotation>();
			}
			return array;
		}

		public static void Update(Quotation quotation)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<Quotation>(quotation).set_State(16);
				context.SaveChanges();
			}
		}

		public static void UpdatequotationDetail(QuotationItems quotationItems)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<QuotationItems>(quotationItems).set_State(16);
				context.SaveChanges();
			}
		}
	}
}
using Library.DualGlobe.ERP.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Library.DualGlobe.ERP.Interfaces
{
	public class ClientInterface
	{
		public ClientInterface()
		{
		}

		public static void Create(Client client)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<Client>(client).set_State(4);
				context.SaveChanges();
			}
		}

		public static void Delete(int id)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<Client>(context.Clients.Find(new object[] { id })).set_State(8);
				context.SaveChanges();
			}
		}

		public static int GetCount()
		{
			int num;
			using (ERPContext context = new ERPContext())
			{
				num = context.Clients.Count<Client>((Client t) => t.Status);
			}
			return num;
		}

		public static Client[] Read()
		{
			Client[] array;
			using (ERPContext context = new ERPContext())
			{
				array = QueryableExtensions.Include<Client, List<Project>>(
					from t in context.Clients
					where t.Status
					select t, (Client p) => p.projects).ToArray<Client>();
			}
			return array;
		}

		public static Client Read(int id)
		{
			Client client;
			using (ERPContext context = new ERPContext())
			{
				client = QueryableExtensions.Include<Client, List<Project>>(
					from t in context.Clients
					where t.Id == id && t.Status
					select t, (Client p) => p.projects).FirstOrDefault<Client>();
			}
			return client;
		}

		public static void Update(Client client)
		{
			using (ERPContext context = new ERPContext())
			{
				context.Entry<Client>(client).set_State(16);
				context.SaveChanges();
			}
		}
	}
}
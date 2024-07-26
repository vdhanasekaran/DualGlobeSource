using Library.DualGlobe.ERP.Models;
using System;
using System.Data.Entity.Migrations;

namespace Library.DualGlobe.ERP.Migrations
{
	internal sealed class Configuration : DbMigrationsConfiguration<ERPContext>
	{
		public Configuration()
		{
			base.set_AutomaticMigrationsEnabled(false);
		}

		protected override void Seed(ERPContext context)
		{
		}
	}
}
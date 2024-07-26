using System;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Library.DualGlobe.ERP.Models
{
	public class ERPContext : DbContext
	{
		protected static string _connectionstring
		{
			get
			{
				return ConfigurationManager.ConnectionStrings["ERPConnection"].ConnectionString;
			}
		}

		public virtual DbSet<Allowance> Allowances
		{
			get;
			set;
		}

		public virtual DbSet<Client> Clients
		{
			get;
			set;
		}

		public virtual DbSet<Company> Companies
		{
			get;
			set;
		}

		public virtual DbSet<CPFLookup> CPFLookups
		{
			get;
			set;
		}

		public virtual DbSet<CPF> CPFs
		{
			get;
			set;
		}

		public virtual DbSet<Donation> Donations
		{
			get;
			set;
		}

		public virtual DbSet<EmployeeDocument> EmployeeDocuments
		{
			get;
			set;
		}

		public virtual DbSet<EmployeeProject> EmployeeProjects
		{
			get;
			set;
		}

		public virtual DbSet<Employee> Employees
		{
			get;
			set;
		}

		public virtual DbSet<ExpenseCategory> ExpenseCategorys
		{
			get;
			set;
		}

		public virtual DbSet<Expense> Expenses
		{
			get;
			set;
		}

		public virtual DbSet<Insurance> Insurances
		{
			get;
			set;
		}

		public virtual DbSet<InvoicePhase> InvoicePhases
		{
			get;
			set;
		}

		public virtual DbSet<Invoice> Invoices
		{
			get;
			set;
		}

		public virtual DbSet<Leave> Leaves
		{
			get;
			set;
		}

		public virtual DbSet<LevyLookup> LevyLookups
		{
			get;
			set;
		}

		public virtual DbSet<Library.DualGlobe.ERP.Models.LoanAndAdvanceDetails> LoanAndAdvanceDetails
		{
			get;
			set;
		}

		public virtual DbSet<LoanAndAdvance> LoanAndAdvances
		{
			get;
			set;
		}

		public virtual DbSet<Menu> Menus
		{
			get;
			set;
		}

		public virtual DbSet<OperationExpense> OperationExpenses
		{
			get;
			set;
		}

		public virtual DbSet<OtherIncome> OtherIncomes
		{
			get;
			set;
		}

		public virtual DbSet<Overtime> Overtimes
		{
			get;
			set;
		}

		public virtual DbSet<Payment> Payments
		{
			get;
			set;
		}

		public virtual DbSet<Project> Projects
		{
			get;
			set;
		}

		public virtual DbSet<PublicHolidayPay> PublicHolidayPays
		{
			get;
			set;
		}

		public virtual DbSet<PublicHoliday> PublicHolidays
		{
			get;
			set;
		}

		public virtual DbSet<Library.DualGlobe.ERP.Models.QuotationItems> QuotationItems
		{
			get;
			set;
		}

		public virtual DbSet<Quotation> Quotations
		{
			get;
			set;
		}

		public virtual DbSet<RestDate> RestDates
		{
			get;
			set;
		}

		public virtual DbSet<RestDayPay> RestDayPays
		{
			get;
			set;
		}

		public virtual DbSet<RestDay> RestDays
		{
			get;
			set;
		}

		public virtual DbSet<RoleMenu> RoleMenus
		{
			get;
			set;
		}

		public virtual DbSet<Role> Roles
		{
			get;
			set;
		}

		public virtual DbSet<SalaryDetail> SalaryDetails
		{
			get;
			set;
		}

		public virtual DbSet<Supplier> Suppliers
		{
			get;
			set;
		}

		public virtual DbSet<Timesheet> Timesheets
		{
			get;
			set;
		}

		public virtual DbSet<UserRole> UserRoles
		{
			get;
			set;
		}

		public virtual DbSet<User> Users
		{
			get;
			set;
		}

		public virtual DbSet<WorkingHour> WorkingHours
		{
			get;
			set;
		}

		public virtual DbSet<WorkPermitAddress> WorkPermitAddresses
		{
			get;
			set;
		}

		public ERPContext() : base(ERPContext._connectionstring)
		{
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			//mks
			//modelBuilder.Conventions().Remove<PluralizingTableNameConvention>();
			modelBuilder.Entity<Quotation>().Property((Quotation x) => x.QuotationValue).HasColumnType("Decimal").HasPrecision(16, 4);
			modelBuilder.Entity<Quotation>().Property((Quotation x) => x.GST).HasColumnType("Decimal").HasPrecision(16, 4);
			modelBuilder.Entity<Quotation>().Property((Quotation x) => x.DiscountValue).HasColumnType("Decimal").HasPrecision(16, 4);
			modelBuilder.Entity<Library.DualGlobe.ERP.Models.QuotationItems>().Property((Library.DualGlobe.ERP.Models.QuotationItems x) => x.UnitPrice).HasColumnType("Decimal").HasPrecision(16, 4);
			modelBuilder.Entity<Library.DualGlobe.ERP.Models.QuotationItems>().Property((Library.DualGlobe.ERP.Models.QuotationItems x) => x.GST).HasColumnType("Decimal").HasPrecision(16, 4);
			modelBuilder.Entity<Library.DualGlobe.ERP.Models.QuotationItems>().Property((Library.DualGlobe.ERP.Models.QuotationItems x) => x.Amount).HasColumnType("Decimal").HasPrecision(16, 4);
			modelBuilder.Entity<Invoice>().Property((Invoice x) => x.TotalAmount).HasColumnType("Decimal").HasPrecision(16, 4);
			modelBuilder.Entity<Invoice>().Property((Invoice x) => x.InvoiceAmount).HasColumnType("Decimal").HasPrecision(16, 4);
			modelBuilder.Entity<Invoice>().Property((Invoice x) => x.TotalGSTAmount).HasColumnType("Decimal").HasPrecision(16, 4);
			modelBuilder.Entity<Invoice>().Property((Invoice x) => x.TotalDiscountedGST).HasColumnType("Decimal").HasPrecision(16, 4);
			modelBuilder.Entity<Invoice>().Property((Invoice x) => x.TotalDiscountedPhaseInvoice).HasColumnType("Decimal").HasPrecision(16, 4);
			modelBuilder.Entity<Invoice>().Property((Invoice x) => x.DiscountValue).HasColumnType("Decimal").HasPrecision(16, 4);
			modelBuilder.Entity<Invoice>().Property((Invoice x) => x.TotalPhaseAmount).HasColumnType("Decimal").HasPrecision(16, 4);
			modelBuilder.Entity<InvoicePhase>().Property((InvoicePhase x) => x.UnitPrice).HasColumnType("Decimal").HasPrecision(16, 4);
			modelBuilder.Entity<InvoicePhase>().Property((InvoicePhase x) => x.GST).HasColumnType("Decimal").HasPrecision(16, 4);
			modelBuilder.Entity<InvoicePhase>().Property((InvoicePhase x) => x.QuotationAmount).HasColumnType("Decimal").HasPrecision(16, 4);
			modelBuilder.Entity<InvoicePhase>().Property((InvoicePhase x) => x.PhaseAmount).HasColumnType("Decimal").HasPrecision(16, 4);
			modelBuilder.Entity<InvoicePhase>().Property((InvoicePhase x) => x.GSTAmount).HasColumnType("Decimal").HasPrecision(16, 4);
			modelBuilder.Entity<InvoicePhase>().Property((InvoicePhase x) => x.PhaseInvoiceAmount).HasColumnType("Decimal").HasPrecision(16, 4);
			modelBuilder.Entity<InvoicePhase>().Property((InvoicePhase x) => x.DiscountedGSTAmount).HasColumnType("Decimal").HasPrecision(16, 4);
			modelBuilder.Entity<InvoicePhase>().Property((InvoicePhase x) => x.DiscountedPhaseInvoiceAmount).HasColumnType("Decimal").HasPrecision(16, 4);
		}
	}
}
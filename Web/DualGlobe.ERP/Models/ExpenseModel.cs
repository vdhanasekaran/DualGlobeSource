using System;
using System.Collections.Generic;
using System.Web.Mvc;
using DualGlobe.ERP.Utility;
using Library.DualGlobe.ERP.Models;

namespace DualGlobe.ERP.Models
{
    public class ExpenseModel
    {
        public ExpenseModel()
        {
            
        }

        public ExpenseModel(Expense[] expenseArr)
        {
            expenseArray = expenseArr;
        }

        public ExpenseModel(Expense expenseSelected)
        {
            expenseRecord = expenseSelected;
        }

        public Expense expenseRecord { get; set; }
        public Expense[] expenseArray { get; set; }
        public string SelectedMonth { get; set; }
        public string SelectedYear { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        
        public string SelectedFilter { get; set; }
        public string SelectedPaymentStatus { get; set; }
        public string SelectedSupplier { get; set; }
        
        //Dropdown
        public IEnumerable<SelectListItem> MonthList = DropdownBuilder.GetMonths();

        public IEnumerable<SelectListItem> YearList = DropdownBuilder.GetYears();

        public IEnumerable<SelectListItem> PaymentMethods = DropdownBuilder.GetAllPaymentMethods();

        public IEnumerable<SelectListItem> PaymentStatus = DropdownBuilder.GetPaymentStatus();

        public IEnumerable<SelectListItem> ProjectList = DropdownBuilder.GetAllProjects();

        public IEnumerable<SelectListItem> ClientList = DropdownBuilder.GetAllClients();

        public IEnumerable<SelectListItem> SupplierList = DropdownBuilder.GetAllSupplier();

        public IEnumerable<SelectListItem> Filters = DropdownBuilder.GetAllFilters();

        public IEnumerable<SelectListItem> GSTStatus = DropdownBuilder.GetStatus();

        public IEnumerable<SelectListItem> ExpenseCategories = DropdownBuilder.GetExpenseCategory();

        public IEnumerable<SelectListItem> Categories = DropdownBuilder.GetCategory();

        public string PageMode { get; set; }
    }
}
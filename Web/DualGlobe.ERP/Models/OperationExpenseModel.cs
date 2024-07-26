using System.Collections.Generic;
using System.Web.Mvc;
using DualGlobe.ERP.Utility;
using Library.DualGlobe.ERP.Models;

namespace DualGlobe.ERP.Models
{
    public class OperationExpenseModel
    {
        public OperationExpenseModel()
        {
            
        }

        public OperationExpenseModel(OperationExpense[] operationExpenseArr)
        {
            operationExpenseArray = operationExpenseArr;
        }

        public OperationExpenseModel(OperationExpense operationExpenseSeleted)
        {
            operationExpenseRecord = operationExpenseSeleted;
        }

        public OperationExpense operationExpenseRecord { get; set; }
        public OperationExpense[] operationExpenseArray { get; set; }

        public string SelectedMonth { get; set; }
        public string SelectedYear { get; set; }

        //Dropdown
        public IEnumerable<SelectListItem> MonthList = DropdownBuilder.GetMonths();

        public IEnumerable<SelectListItem> YearList = DropdownBuilder.GetYears();

        public IEnumerable<SelectListItem> PaymentMethods = DropdownBuilder.GetAllPaymentMethods();

        public IEnumerable<SelectListItem> PaymentStatus = DropdownBuilder.GetPaymentStatus();

        public IEnumerable<SelectListItem> ExpenseCategories = DropdownBuilder.GetExpenseCategory();

        public string PageMode { get; set; }
    }
}
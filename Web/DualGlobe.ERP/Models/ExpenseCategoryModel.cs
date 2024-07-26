using System.Collections.Generic;
using System.Web.Mvc;
using DualGlobe.ERP.Utility;
using Library.DualGlobe.ERP.Models;

namespace DualGlobe.ERP.Models
{
    public class ExpenseCategoryModel
    {
        public ExpenseCategoryModel()
        {
            
        }

        public ExpenseCategoryModel(ExpenseCategory[] expenseCategoryArr)
        {
            expenseCategoryArray = expenseCategoryArr;
        }

        public ExpenseCategoryModel(ExpenseCategory expenseCategorySeleted)
        {
            expenseCategoryRecord = expenseCategorySeleted;
        }

        public ExpenseCategory expenseCategoryRecord { get; set; }
        public ExpenseCategory[] expenseCategoryArray { get; set; }

        public string PageMode { get; set; }
    }
}
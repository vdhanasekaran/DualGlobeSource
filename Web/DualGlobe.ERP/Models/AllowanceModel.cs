using System.Collections.Generic;
using System.Web.Mvc;
using DualGlobe.ERP.Utility;
using Library.DualGlobe.ERP.Models;

namespace DualGlobe.ERP.Models
{
    public class AllowanceModel
    {
        public AllowanceModel()
        {

        }

        public AllowanceModel(Allowance[] allowanceList)
        {
            allowanceArray = allowanceList;
        }

        public AllowanceModel(Allowance allowanceSelected)
        {
            allowanceRecord = allowanceSelected;
        }

        public Allowance allowanceRecord { get; set; }
        public Allowance[] allowanceArray { get; set; }

        public string PageMode { get; set; }
        public string SelectedMonth { get; set; }
        public string SelectedYear { get; set; }

        public IEnumerable<SelectListItem> MonthList = DropdownBuilder.GetMonths();

        public IEnumerable<SelectListItem> YearList = DropdownBuilder.GetYears();

        public IEnumerable<SelectListItem> EmployeeList = DropdownBuilder.GetAllEmployees();

        public string EmployeeFullName { get; set; }

    }
}
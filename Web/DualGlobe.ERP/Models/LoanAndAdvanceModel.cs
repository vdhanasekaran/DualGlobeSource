using System;
using System.Collections.Generic;
using System.Web.Mvc;
using DualGlobe.ERP.Utility;
using Library.DualGlobe.ERP.Models;

namespace DualGlobe.ERP.Models
{
    public class LoanAndAdvanceModel
    {
        public LoanAndAdvanceModel()
        {

        }

        public LoanAndAdvanceModel(LoanAndAdvance[] loanAndAdvanceList)
        {
            loanAndAdvanceArray = loanAndAdvanceList;
        }

        public LoanAndAdvanceModel(LoanAndAdvance loanAndAdvanceSelected)
        {
            loanAndAdvanceRecord = loanAndAdvanceSelected;
        }

        public LoanAndAdvance loanAndAdvanceRecord { get; set; }
        public LoanAndAdvance[] loanAndAdvanceArray { get; set; }

        public string ReceivedByEmployee { get; set; }
        public string PaidByEmployee { get; set; }
        public string PageMode { get; set; }
        public string SelectedMonth { get; set; }
        public string SelectedYear { get; set; }
        public DateTime RepaymentDate { get; set; }

        public IEnumerable<SelectListItem> MonthList = DropdownBuilder.GetMonths();

        public IEnumerable<SelectListItem> YearList = DropdownBuilder.GetYears();

        public IEnumerable<SelectListItem> EmployeeList = DropdownBuilder.GetAllEmployees();
    }
}
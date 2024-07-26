using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DualGlobe.ERP.Utility;
using Library.DualGlobe.ERP.Models;

namespace DualGlobe.ERP.Models
{
    public class OtherIncomeModel
    {
        public OtherIncomeModel()
        {
            
        }

        public OtherIncomeModel(OtherIncome[] otherIncomeArr)
        {
            otherIncomeArray = otherIncomeArr;
        }

        public OtherIncomeModel(OtherIncome otherIncomeSeleted)
        {
            otherIncomeRecord = otherIncomeSeleted;
        }

        public OtherIncome otherIncomeRecord { get; set; }
        public OtherIncome[] otherIncomeArray { get; set; }

        public string SelectedMonth { get; set; }
        public string SelectedYear { get; set; }

        //Dropdown
        public IEnumerable<SelectListItem> MonthList = DropdownBuilder.GetMonths();

        public IEnumerable<SelectListItem> YearList = DropdownBuilder.GetYears();

        public string PageMode { get; set; }
    }
}
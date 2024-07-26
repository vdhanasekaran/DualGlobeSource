using System.Collections.Generic;
using System.Web.Mvc;
using Library.DualGlobe.ERP.Models;
using DualGlobe.ERP.Utility;
using System.ComponentModel.DataAnnotations;

namespace DualGlobe.ERP.Models
{
    public class LeaveModel
    {
        public LeaveModel()
        {

        }

        public LeaveModel(Leave[] leaveArr)
        {
            LeaveArray = leaveArr;
        }

        public LeaveModel(Leave leaveSeleted)
        {
            LeaveRecord = leaveSeleted;
        }

        public Leave LeaveRecord { get; set; }
        public Leave[] LeaveArray { get; set; }
        public string SelectedMonth { get; set; }
        public string SelectedYear { get; set; }

        public IEnumerable<SelectListItem> MonthList = DropdownBuilder.GetMonths();

        public IEnumerable<SelectListItem> YearList = DropdownBuilder.GetYears();

        public IEnumerable<SelectListItem> LeaveTypeList = DropdownBuilder.GetLeaveTypes();

        public IEnumerable<SelectListItem> EmployeeList = DropdownBuilder.GetAllProjectEmployees();

        public string PageMode { get; set; }

      
    }    
}
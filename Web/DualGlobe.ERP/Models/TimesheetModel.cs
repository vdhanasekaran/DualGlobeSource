using System;
using System.Collections.Generic;
using Library.DualGlobe.ERP.Models;
using System.Web.Mvc;
using DualGlobe.ERP.Utility;

namespace DualGlobe.ERP.Models
{
    public class TimesheetModel
    {
        public TimesheetModel()
        {

        }

        public TimesheetModel(Timesheet[] timesheetArr)
        {
            timesheetArray = timesheetArr;
        }

        public TimesheetModel(Timesheet timesheetSelected)
        {
            timesheetRecord = timesheetSelected;
        }

        public Timesheet timesheetRecord { get; set; }
        public Timesheet[] timesheetArray { get; set; }

        public string PageMode { get; set; }

        public string EmployeeName { get; set; }

        public string ProjectName { get; set; }
        public int? ProjectId { get; set; }
        public string ClientName { get; set; }
        public int? ClientId { get; set; }

        public string EmployeeStatus { get; set; }

        public DateTime TimesheetDate { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string IsAbset { get; set; }

        public bool IsPublicHoliday { get; set; }
        public bool IsRestHoliday { get; set; }
        public bool IsSelectAllEmployee { get; set; }

        public TimeSpan? HolidayInTime { get; set; }
        public TimeSpan? HolidayOutTime { get; set; }

        public IEnumerable<SelectListItem> projectList = new List<SelectListItem>();

        public IEnumerable<SelectListItem> clientList = DropdownBuilder.GetAllClients();

        public IEnumerable<SelectListItem> employeeStatusList = DropdownBuilder.GetEmployeeStatus();

        public IEnumerable<SelectListItem> employeeList = new List<SelectListItem>();

        public IEnumerable<SelectListItem> IsLeaveList = DropdownBuilder.GetStatus();

       
    }
}
using System.Collections.Generic;
using System.Web.Mvc;
using Library.DualGlobe.ERP.Models;
using DualGlobe.ERP.Utility;

namespace DualGlobe.ERP.Models
{
    public class EmployeeProjectModel
    {

        public EmployeeProjectModel()
        {

        }

        public EmployeeProjectModel(EmployeeProject[] empProjectList)
        {
            employeeProjectList = empProjectList;            
        }

        public EmployeeProjectModel(EmployeeProject empSelected)
        {
            employeeProjectRecord = empSelected;
        }

        public EmployeeProject[] employeeProjectList { get; set; }
        public EmployeeProject employeeProjectRecord { get; set; }
        public int[] employeeProjectRecords { get; set; }

        public IEnumerable<SelectListItem> projectList = DropdownBuilder.GetAllProjects();

        public IEnumerable<SelectListItem> EmployeeList = DropdownBuilder.GetAllEmployees();

        public string ProjectId { get; set; }
    }
}
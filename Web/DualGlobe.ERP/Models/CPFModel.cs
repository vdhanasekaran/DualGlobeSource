using System.Collections.Generic;
using System.Web.Mvc;
using Library.DualGlobe.ERP.Models;
using DualGlobe.ERP.Utility;
using System.ComponentModel.DataAnnotations;

namespace DualGlobe.ERP.Models
{
    public class CPFModel
    {
        public CPFModel()
        {

        }

        public CPFModel(CPF[]  cpfArr)
        {
            CPFArray = cpfArr;
        }

        public CPFModel(CPF cpfSeleted)
        {
            CPFRecord = cpfSeleted;
        }

        public CPF CPFRecord { get; set; }
        public CPF[] CPFArray { get; set; }

        public string SelectedMonth { get; set; }
        public string SelectedYear { get; set; }

        public IEnumerable<SelectListItem> MonthList = DropdownBuilder.GetMonths();

        public IEnumerable<SelectListItem> YearList = DropdownBuilder.GetYears();

        public IEnumerable<SelectListItem> EmployeeList = DropdownBuilder.GetAllEmployees();

        public IEnumerable<SelectListItem> DonationTypeList = DropdownBuilder.GetDonationTypes();

        public string PageMode { get; set; }

      
    }

    public class CFPdata
    {
        public double TotalCPF { get; set; }
        public double EmployeeCPF { get; set; }
        public double EmployerCPF { get; set; }
    }
}
using System;
using System.Collections.Generic;
using Library.DualGlobe.ERP.Models;
using System.Web.Mvc;
using DualGlobe.ERP.Utility;

namespace DualGlobe.ERP.Models
{
    public class EmployeeModel
    {

        public EmployeeModel()
        {
            
        }

        public EmployeeModel(Employee[] empList)
        {
            employeeList = empList;            
        }

        public EmployeeModel(Employee empSelected)
        {
            employeeRecord = empSelected;
        }

        public EmployeeModel(SalaryDetail[] salaryArray)
        {
            SalaryArray = salaryArray;
        }
        public Employee[] employeeList { get; set; }       
        public Employee employeeRecord { get; set; }

        public SalaryDetail[] SalaryArray { get; set; }
        public string PageMode { get; set; }
        public string SelectedMonth { get; set; }
        public string SelectedYear { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int? ProjectId { get; set; }

        public string WorkStatus { get; set; }


        //Dropdown
        public IEnumerable<SelectListItem> MonthList = DropdownBuilder.GetMonths();

        public IEnumerable<SelectListItem> YearList = DropdownBuilder.GetYears();

        public IEnumerable<SelectListItem> PaymentTypeList = DropdownBuilder.GetAllPaymentMethods();

        public IEnumerable<SelectListItem> EmploymentTypeList = DropdownBuilder.GetEmploymentTypes();

        public IEnumerable<SelectListItem> WorkTypeList = DropdownBuilder.GetWorkTypes();

        public IEnumerable<SelectListItem> SkillList = DropdownBuilder.GetAllSkills();

        public IEnumerable<SelectListItem> TierTypes = DropdownBuilder.GetAllTiers();

        public IEnumerable<SelectListItem> LicenceTypeList = DropdownBuilder.GetLicenceTypes();

        public IEnumerable<SelectListItem> DonationTypeList = DropdownBuilder.GetDonationTypes();

        public List<string> PayrollStatus = DropdownBuilder.GetPayrollStatus();

        public IEnumerable<SelectListItem> GenderList = DropdownBuilder.GetGender();

        public IEnumerable<SelectListItem> ReligionList = DropdownBuilder.GetReligion();

        public IEnumerable<SelectListItem> NationalityList = DropdownBuilder.GetCountry();

        public IEnumerable<SelectListItem> MaritalStatusList = DropdownBuilder.GetMaritalstatus();

        public IEnumerable<SelectListItem> EmployeeStatusList = DropdownBuilder.GetEmployeeStatus();

        public IEnumerable<SelectListItem> EmployeeLevyStatusList = DropdownBuilder.GetLevyEmployeeStatus();

        public IEnumerable<SelectListItem> StatusList = DropdownBuilder.GetStatus();

        public IEnumerable<SelectListItem> BankNameList = DropdownBuilder.GetBankNames();

        public IEnumerable<SelectListItem> OTGroupList = DropdownBuilder.GetOTGroups();

        public IEnumerable<SelectListItem> RestDayList = DropdownBuilder.GetRestDays();

        public IEnumerable<SelectListItem> WorkingHoursList = DropdownBuilder.GetWorkingHours();

        public IEnumerable<SelectListItem> PublicHolidayPayList = DropdownBuilder.GetPHGroups();

        public IEnumerable<SelectListItem> WorkPermitAddressList = DropdownBuilder.GetWorkPermitAddress();

        public IEnumerable<SelectListItem> MedicalInsuranceList = DropdownBuilder.GetInsuranceProviderName("MedicalInsurance");

        public IEnumerable<SelectListItem> SecurityBondList = DropdownBuilder.GetInsuranceProviderName("SecurityBond");

        public IEnumerable<SelectListItem> WorkInjuryInsuranceList = DropdownBuilder.GetInsuranceProviderName("WorkInjuryInsurance");

        public IEnumerable<SelectListItem> MedicalInsuranceNumbers = DropdownBuilder.GetInsurancePolicyNumbers("MedicalInsurance");

        public IEnumerable<SelectListItem> SecurityBondNumbers = DropdownBuilder.GetInsurancePolicyNumbers("SecurityBond");

        public IEnumerable<SelectListItem> WorkInjuryInsuranceNumbers = DropdownBuilder.GetInsurancePolicyNumbers("WorkInjuryInsurance");
    }
}
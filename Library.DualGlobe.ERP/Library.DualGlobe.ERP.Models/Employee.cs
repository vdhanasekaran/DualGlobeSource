using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Runtime.CompilerServices;

namespace Library.DualGlobe.ERP.Models
{
	public class Employee
	{
		public string Address
		{
			get;
			set;
		}

		public virtual List<Allowance> allowance
		{
			get;
			set;
		}

		public DateTime AppointmentDate
		{
			get;
			set;
		}

		public string BankAccountNumber
		{
			get;
			set;
		}

		public string BankerInsuranceEndDate
		{
			get;
			set;
		}

		public string BankerInsuranceStartDate
		{
			get;
			set;
		}

		public string BankName
		{
			get;
			set;
		}

		public decimal? BasicSalary
		{
			get;
			set;
		}

		public string BloodGroup
		{
			get;
			set;
		}

		public string CPFDonationType
		{
			get;
			set;
		}

		public DateTime? CSOCExpiryDate
		{
			get;
			set;
		}

		public DateTime DateOfBirth
		{
			get;
			set;
		}

		public string Designation
		{
			get;
			set;
		}

		public string DrivingLicense
		{
			get;
			set;
		}

		public string EmailId
		{
			get;
			set;
		}

		public decimal? EmployeeCPF
		{
			get;
			set;
		}

		public virtual List<EmployeeDocument> EmployeeDocuments
		{
			get;
			set;
		}

		[NotMapped]
		public string EmployeeId
		{
			get
			{
				string item = ConfigurationManager.AppSettings["EmployeeIdPrefix"];
				int id = this.Id;
				return string.Concat(item, id.ToString()).Replace(" ", "0");
			}
		}

		public decimal? EmployerCPF
		{
			get;
			set;
		}

		public string EmploymentType
		{
			get;
			set;
		}

		public DateTime? EPExpiryDate
		{
			get;
			set;
		}

		public bool? FinishedSafetySupervisorCource
		{
			get;
			set;
		}

		public string FINNumber
		{
			get;
			set;
		}

		public string FirstName
		{
			get;
			set;
		}

		public decimal? FixedAllowance
		{
			get;
			set;
		}

		public decimal? FixedDeduction
		{
			get;
			set;
		}

		public string Gender
		{
			get;
			set;
		}

		public string HighestEducation
		{
			get;
			set;
		}

		public decimal? HousingAllowance
		{
			get;
			set;
		}

		public decimal? HousingDeduction
		{
			get;
			set;
		}

		public string ICNumber
		{
			get;
			set;
		}

		public int Id
		{
			get;
			set;
		}

		public string InsuranceEndDate
		{
			get;
			set;
		}

		public string InsurancePolicyNumber
		{
			get;
			set;
		}

		public string InsuranceStartDate
		{
			get;
			set;
		}

		public string InsurerName
		{
			get;
			set;
		}

		public bool IsHavingAllowance
		{
			get;
			set;
		}

		public bool IsHavingDeduction
		{
			get;
			set;
		}

		public bool? IsPublicHolidayConsideredNormalDay
		{
			get;
			set;
		}

		public string LastName
		{
			get;
			set;
		}

		public DateTime? LastWorkingDate
		{
			get;
			set;
		}

		public decimal? Levy
		{
			get;
			set;
		}

		public virtual List<LoanAndAdvance> loanAndAdvance
		{
			get;
			set;
		}

		public string MaritalStatus
		{
			get;
			set;
		}

		public bool? MYE
		{
			get;
			set;
		}

		public string Nationality
		{
			get;
			set;
		}

		public string NRICNumber
		{
			get;
			set;
		}

		public string OADescription
		{
			get;
			set;
		}

		public string ODDescription
		{
			get;
			set;
		}

		public int? OTGroup
		{
			get;
			set;
		}

		public decimal? OtherAllowance
		{
			get;
			set;
		}

		public string OtherBankName
		{
			get;
			set;
		}

		public decimal? OtherDeduction
		{
			get;
			set;
		}

		public int? PaidLeaveCount
		{
			get;
			set;
		}

		public DateTime? PassportExpiryDate
		{
			get;
			set;
		}

		public string PassportNumber
		{
			get;
			set;
		}

		public string PaymentMethod
		{
			get;
			set;
		}

		public string PhoneNumber
		{
			get;
			set;
		}

		public string PhotoImagePath
		{
			get;
			set;
		}

		public DateTime? PREffectiveDate
		{
			get;
			set;
		}

		public int? PublicHolidayPay
		{
			get;
			set;
		}

		public string Religion
		{
			get;
			set;
		}

		public int? RestDay
		{
			get;
			set;
		}

		public virtual List<SalaryDetail> salaryDetail
		{
			get;
			set;
		}

		public string SecurityBondBankerName
		{
			get;
			set;
		}

		public string SecurityBondPolicyNumber
		{
			get;
			set;
		}

		public string Skill
		{
			get;
			set;
		}

		public DateTime? SPassExpiryDate
		{
			get;
			set;
		}

		public string Status
		{
			get;
			set;
		}

		public string TierType
		{
			get;
			set;
		}

		public virtual List<Timesheet> timesheet
		{
			get;
			set;
		}

		public decimal? TransportAllowance
		{
			get;
			set;
		}

		public decimal? TransportDeduction
		{
			get;
			set;
		}

		public int? WorkingHours
		{
			get;
			set;
		}

		public string WorkInjuryInsuranceEndDate
		{
			get;
			set;
		}

		public string WorkInjuryInsurancePolicyNumber
		{
			get;
			set;
		}

		public string WorkInjuryInsuranceStartDate
		{
			get;
			set;
		}

		public string WorkInjuryInsurerName
		{
			get;
			set;
		}

		public string WorkPermitAddress
		{
			get;
			set;
		}

		public DateTime? WorkPermitExpiryDate
		{
			get;
			set;
		}

		public string WorkPermitNumber
		{
			get;
			set;
		}

		public string WorkStatus
		{
			get;
			set;
		}

		public string WorkType
		{
			get;
			set;
		}

		public int? YearsofExperience
		{
			get;
			set;
		}

		public Employee()
		{
		}
	}
}
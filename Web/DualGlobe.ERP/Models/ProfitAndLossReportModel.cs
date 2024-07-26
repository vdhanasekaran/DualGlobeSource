using System;
using System.Collections.Generic;

namespace DualGlobe.ERP.Models
{
    public class ProfitLossReportModel
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public decimal? GrossSale { get; set; }
        public decimal? OtherIncome { get; set; }

        public decimal? Expense { get; set; }
        public decimal? AccountingAndLegalFee { get; set; }
        public decimal? Advertising { get; set; }
        public decimal? Utilitiy { get; set; }
        public decimal? Insurance { get; set; }
        public decimal? InterestAndBankCharge { get; set; }
        public decimal? Postage { get; set; }
        public decimal? PrintingAndStationary { get; set; }
        public decimal? ProfessionalMembership { get; set; }
        public decimal? PremiseRent { get; set; }
        public decimal? RepairAndMaintenance { get; set; }
        public decimal? Training { get; set; }
        public decimal? VehicleOperatingCost { get; set; }
        public decimal? WorkerCompensation { get; set; }
        public decimal? Depreciation { get; set; }

        public decimal? WagesAndSalary { get; set; }
        public decimal? EmployerCPF { get; set; }
        public decimal? WorkerLevy { get; set; }
        public decimal? SDL { get; set; }
        public List<OtherExpense> otherExpense { get; set; }
        public decimal? TotalAllowableExpense { get; set; }
    }

    public class OtherExpense
    {
        public string ExpenseDescription { get; set; }
        public decimal? ExpenseAmount { get; set; }
    }
}
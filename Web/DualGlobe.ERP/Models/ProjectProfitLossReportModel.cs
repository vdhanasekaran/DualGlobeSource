using System;
using System.Collections.Generic;
using System.Web.Mvc;
using DualGlobe.ERP.Utility;

namespace DualGlobe.ERP.Models
{
    public class ProjectProfitLossReportModel
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? ClientId { get; set; }
        public int? ProjectId { get; set; }
        public string ClientName { get; set; }
        public string ProjectName { get; set; }
        public decimal? ProjectValue { get; set; }
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
        public List<OtherExpense> otherExpense { get; set; }
        public decimal? ProjectExpense { get; set; }
        public decimal? GrossSale { get; set; }

        public IEnumerable<SelectListItem> ClientList = DropdownBuilder.GetAllClients();

        public IEnumerable<SelectListItem> ProjectList = null;
    }
}
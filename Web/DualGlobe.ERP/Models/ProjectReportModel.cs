using System;

namespace DualGlobe.ERP.Models
{
    public class ProjectReportModel
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string ClientName { get; set; }
        public decimal? ProjectValue { get; set; }
        public string Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal? ReceivedPayment { get; set; }
        public decimal? OutstandingAmount { get; set; }
        public int ProjectEmployees { get; set; }
    }
}
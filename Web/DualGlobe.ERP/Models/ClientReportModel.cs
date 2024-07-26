namespace DualGlobe.ERP.Models
{
    public class ClientReportModel
    {
        public int ClientID { get; set; }
        public string CompanyName { get; set; }
        public string ClientName { get; set; }
        public int OngoingProjects { get; set; }
        public int CompletedProjects { get; set; }
        public int DraftProjects { get; set; }
        public decimal? TotalValue { get; set; }
        public decimal? ReceivedPayment { get; set; }
        public decimal? OutstandingAmount { get; set; }
    }
}
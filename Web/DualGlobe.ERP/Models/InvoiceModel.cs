using DualGlobe.ERP.Utility;
using Library.DualGlobe.ERP.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace DualGlobe.ERP.Models
{
    public class InvoiceModel
    {

        public InvoiceModel()
        {
            
        }
        public InvoiceModel(Invoice[] invoiceList)
        {
            InvoiceArray = invoiceList;
        }

        public InvoiceModel(Invoice invoiceSelected)
        {
            InvoiceRecord = invoiceSelected;
        }

        public InvoiceModel(Payment paymentSelected)
        {
            PaymentRecord = paymentSelected;
        }

        public InvoiceModel(Payment[] paymentArr)
        {
            PaymentArray = paymentArr;
        }

        public Invoice InvoiceRecord { get; set; }

        public Quotation QuotationRecord { get; set; }

        public Invoice[] InvoiceArray { get; set; }

        public InvoicePhase[] InvoicePhaseArrayRecord { get; set; }
        public InvoicePhase InvoicePhaseRecord { get; set; }
        public Payment PaymentRecord { get; set; }
        public Payment[] PaymentArray { get; set; }

        public string PageMode { get; set; }
        public string ClientId { get; set; }
        public string QuotationId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public string SelectedPaymentStatus { get; set; }
        public string SelectedClient { get; set; }
        public string SelectedProject { get; set; }

        public IEnumerable<SelectListItem> ClientList = DropdownBuilder.GetAllClients();

        public IEnumerable<SelectListItem> ProjectList = DropdownBuilder.GetAllProjects();

        public IEnumerable<SelectListItem> QuotationList = DropdownBuilder.GetAllQuotation();

        public IEnumerable<SelectListItem> PaymentMethods = DropdownBuilder.GetAllPaymentMethods();

        public IEnumerable<SelectListItem> PaymentStatus = DropdownBuilder.GetPaymentStatus();

        public IEnumerable<SelectListItem> ProgressClaimStatus = DropdownBuilder.GetClaimStatus();

        public IEnumerable<SelectListItem> ClaimRequiredStatus = DropdownBuilder.GetStatus();

        public IEnumerable<SelectListItem> DiscountTypes = DropdownBuilder.GetDiscountTypes();
    }
}
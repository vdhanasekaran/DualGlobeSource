using System.Collections.Generic;
using System.Web.Mvc;
using DualGlobe.ERP.Utility;
using Library.DualGlobe.ERP.Models;

namespace DualGlobe.ERP.Models
{
    public class QuotationModel
    {
        public QuotationModel()
        {
            
        }

        public QuotationModel(Quotation[] quotationArr)
        {
            quotationArray = quotationArr;
        }

        public QuotationModel(Quotation quotationSeleted)
        {
            quotationRecord = quotationSeleted;
        }

        public Quotation quotationRecord { get; set; }
        public Quotation[] quotationArray { get; set; }

        public IEnumerable<SelectListItem> ClientList = DropdownBuilder.GetAllClients();

        public IEnumerable<SelectListItem> ProjectList = DropdownBuilder.GetAllProjects();

        public IEnumerable<SelectListItem> StatusList = DropdownBuilder.GetStages();

        public IEnumerable<SelectListItem> GSTStatus = DropdownBuilder.GetGSTStatus();

        public IEnumerable<SelectListItem> DiscountTypes = DropdownBuilder.GetDiscountTypes();

        public string PageMode { get; set; }
    }
}
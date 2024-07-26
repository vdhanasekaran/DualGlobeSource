using System.Collections.Generic;
using System.Web.Mvc;
using DualGlobe.ERP.Utility;
using Library.DualGlobe.ERP.Models;

namespace DualGlobe.ERP.Models
{
    public class CompanyModel
    {
        public CompanyModel()
        {
            
        }

        public CompanyModel(Company[] companyArr)
        {
            companyArray = companyArr;
        }

        public CompanyModel(Company companySeleted)
        {
            companyRecord = companySeleted;
        }

        public Company companyRecord { get; set; }
        public Company[] companyArray { get; set; }

        public IEnumerable<SelectListItem> CountryList = DropdownBuilder.GetCountry();

        public IEnumerable<SelectListItem> CurrencyList = DropdownBuilder.GetCurrency();

        public IEnumerable<SelectListItem> StatusList = DropdownBuilder.GetStatus();

        public string PageMode { get; set; }
    }
}
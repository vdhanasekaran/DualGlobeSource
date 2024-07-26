using System.Collections.Generic;
using System.Web.Mvc;
using DualGlobe.ERP.Utility;
using Library.DualGlobe.ERP.Models;

namespace DualGlobe.ERP.Models
{
    public class ClientModel
    {
        public ClientModel()
        {
            
        }

        public ClientModel(Client[] clientArr)
        {
            clientArray = clientArr;
        }

        public ClientModel(Client clientSeleted)
        {
            clientRecord = clientSeleted;
        }

        public Client clientRecord { get; set; }
        public Client[] clientArray { get; set; }

        public IEnumerable<SelectListItem> CountryList = DropdownBuilder.GetCountry();

        public IEnumerable<SelectListItem> CurrencyList = DropdownBuilder.GetCurrency();

        public IEnumerable<SelectListItem> StatusList = DropdownBuilder.GetStatus();

        public string PageMode { get; set; }
    }
}
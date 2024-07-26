using System.Collections.Generic;
using System.Web.Mvc;
using Library.DualGlobe.ERP.Models;
using DualGlobe.ERP.Utility;
using System.ComponentModel.DataAnnotations;

namespace DualGlobe.ERP.Models
{
    public class InsuranceModel
    {
        public InsuranceModel()
        {

        }

        public InsuranceModel(Insurance[] insuranceArr)
        {
            InsuranceArray = insuranceArr;
        }

        public InsuranceModel(Insurance insuranceSeleted)
        {
            InsuranceRecord = insuranceSeleted;
        }

        public Insurance InsuranceRecord { get; set; }
        public Insurance[] InsuranceArray { get; set; }

        public IEnumerable<SelectListItem> InsuranceTypeList = DropdownBuilder.GetInsuranceTypes();

        public string PageMode { get; set; }
    }
}
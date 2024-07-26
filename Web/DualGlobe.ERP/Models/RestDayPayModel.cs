using System.Collections.Generic;
using System.Web.Mvc;
using Library.DualGlobe.ERP.Models;
using DualGlobe.ERP.Utility;
using System.ComponentModel.DataAnnotations;

namespace DualGlobe.ERP.Models
{
    public class RestDayPayModel
    {
        public RestDayPayModel()
        {

        }

        public RestDayPayModel(RestDayPay[]  restDayPayArr)
        {
            RestDayPayArray = restDayPayArr;
        }

        public RestDayPayModel(RestDayPay restDayPaySeleted)
        {
            RestDayPayRecord = restDayPaySeleted;
        }

        public RestDayPay RestDayPayRecord { get; set; }
        public RestDayPay[] RestDayPayArray { get; set; }

        public IEnumerable<SelectListItem> PayTypeList = DropdownBuilder.GetOTTypes();

        public IEnumerable<SelectListItem> RestDayGroups = DropdownBuilder.GetRestDays();

        public string PageMode { get; set; }

      
    }    
}
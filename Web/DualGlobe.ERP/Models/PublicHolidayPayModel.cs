using System.Collections.Generic;
using System.Web.Mvc;
using Library.DualGlobe.ERP.Models;
using DualGlobe.ERP.Utility;
using System.ComponentModel.DataAnnotations;

namespace DualGlobe.ERP.Models
{
    public class PublicHolidayPayModel
    {
        public PublicHolidayPayModel()
        {

        }

        public PublicHolidayPayModel(PublicHolidayPay[]  publicHolidayPayArr)
        {
            PublicHolidayPayArray = publicHolidayPayArr;
        }

        public PublicHolidayPayModel(PublicHolidayPay publicHolidayPaySeleted)
        {
            PublicHolidayPayRecord = publicHolidayPaySeleted;
        }

        public PublicHolidayPay PublicHolidayPayRecord { get; set; }
        public PublicHolidayPay[] PublicHolidayPayArray { get; set; }

        public IEnumerable<SelectListItem> PayTypeList = DropdownBuilder.GetOTTypes();

        public string PageMode { get; set; }

      
    }    
}
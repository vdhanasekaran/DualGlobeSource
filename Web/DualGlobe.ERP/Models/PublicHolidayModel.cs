using System.Collections.Generic;
using System.Web.Mvc;
using Library.DualGlobe.ERP.Models;
using DualGlobe.ERP.Utility;
using System.ComponentModel.DataAnnotations;

namespace DualGlobe.ERP.Models
{
    public class PublicHolidayModel
    {
        public PublicHolidayModel()
        {

        }

        public PublicHolidayModel(PublicHoliday[]  dataArr)
        {
            PublicHolidayArray = dataArr;
        }

        public PublicHolidayModel(PublicHoliday dataSeleted)
        {
            PublicHolidayRecord = dataSeleted;
        }

        public PublicHoliday PublicHolidayRecord { get; set; }
        public PublicHoliday[] PublicHolidayArray { get; set; }

        public string SelectedYear { get; set; }

        public IEnumerable<SelectListItem> YearList = DropdownBuilder.GetYears();

        public string PageMode { get; set; }

      
    }    
}
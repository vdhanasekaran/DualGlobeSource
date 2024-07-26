using System.Collections.Generic;
using System.Web.Mvc;
using Library.DualGlobe.ERP.Models;
using DualGlobe.ERP.Utility;
using System.ComponentModel.DataAnnotations;

namespace DualGlobe.ERP.Models
{
    public class RestDayModel
    {
        public RestDayModel()
        {

        }

        public RestDayModel(RestDay[] restDayArr)
        {
            RestDayArray = restDayArr;
        }

        public RestDayModel(RestDay restDaySeleted)
        {
            RestDayRecord = restDaySeleted;
        }

        public RestDay RestDayRecord { get; set; }
        public RestDay[] RestDayArray { get; set; }

        public IEnumerable<SelectListItem> RestDayTypes = DropdownBuilder.GetRestDayTypes();

        public string PageMode { get; set; }
    }    
}
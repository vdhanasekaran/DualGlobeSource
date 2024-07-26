using System.Collections.Generic;
using System.Web.Mvc;
using Library.DualGlobe.ERP.Models;
using DualGlobe.ERP.Utility;
using System.ComponentModel.DataAnnotations;

namespace DualGlobe.ERP.Models
{
    public class OvertimeModel
    {
        public OvertimeModel()
        {

        }

        public OvertimeModel(Overtime[] overtimeArr)
        {
            OvertimeArray = overtimeArr;
        }

        public OvertimeModel(Overtime overtimeSeleted)
        {
            OvertimeRecord = overtimeSeleted;
        }

        public Overtime OvertimeRecord { get; set; }
        public Overtime[] OvertimeArray { get; set; }

        public IEnumerable<SelectListItem> OTTypeList = DropdownBuilder.GetOTTypes();

        public string PageMode { get; set; }

      
    }    
}
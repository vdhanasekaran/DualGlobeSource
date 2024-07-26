using System.Collections.Generic;
using System.Web.Mvc;
using Library.DualGlobe.ERP.Models;
using DualGlobe.ERP.Utility;
using System.ComponentModel.DataAnnotations;

namespace DualGlobe.ERP.Models
{
    public class WorkingHourModel
    {
        public WorkingHourModel()
        {

        }

        public WorkingHourModel(WorkingHour[]  workingHourArr)
        {
            WorkingHourArray = workingHourArr;
        }

        public WorkingHourModel(WorkingHour workingHourSeleted)
        {
            WorkingHourRecord = workingHourSeleted;
        }

        public WorkingHour WorkingHourRecord { get; set; }
        public WorkingHour[] WorkingHourArray { get; set; }        

        public string PageMode { get; set; }

      
    }    
}
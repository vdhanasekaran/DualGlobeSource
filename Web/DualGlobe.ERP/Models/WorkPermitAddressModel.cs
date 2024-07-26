using System.Collections.Generic;
using System.Web.Mvc;
using Library.DualGlobe.ERP.Models;
using DualGlobe.ERP.Utility;
using System.ComponentModel.DataAnnotations;

namespace DualGlobe.ERP.Models
{
    public class WorkPermitAddressModel
    {
        public WorkPermitAddressModel()
        {

        }

        public WorkPermitAddressModel(WorkPermitAddress[]  workPermitAddressArr)
        {
            WorkPermitAddressArray = workPermitAddressArr;
        }

        public WorkPermitAddressModel(WorkPermitAddress workPermitAddressSeleted)
        {
            WorkPermitAddressRecord = workPermitAddressSeleted;
        }

        public WorkPermitAddress WorkPermitAddressRecord { get; set; }
        public WorkPermitAddress[] WorkPermitAddressArray { get; set; }        

        public string PageMode { get; set; }

      
    }    
}
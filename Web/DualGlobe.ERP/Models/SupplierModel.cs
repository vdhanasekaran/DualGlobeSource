using System.Collections.Generic;
using System.Web.Mvc;
using DualGlobe.ERP.Utility;
using Library.DualGlobe.ERP.Models;

namespace DualGlobe.ERP.Models
{
    public class SupplierModel
    {
        public SupplierModel()
        {
            
        }

        public SupplierModel(Supplier[] supplierArr)
        {
            supplierArray = supplierArr;
        }

        public SupplierModel(Supplier supplierSeleted)
        {
            supplierRecord = supplierSeleted;
        }

        public Supplier supplierRecord { get; set; }
        public Supplier[] supplierArray { get; set; }

        public string PageMode { get; set; }
    }
}
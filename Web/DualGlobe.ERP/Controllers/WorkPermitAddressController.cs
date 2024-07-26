using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DualGlobe.ERP.Models;
using DualGlobe.ERP.Utility;
using Library.DualGlobe.ERP.Interfaces;

namespace DualGlobe.ERP.Controllers
{
    public class WorkPermitAddressController : BaseController
    {
        // GET: WorkPermitAddress
        public ActionResult Index()
        {
            var conf = WorkPermitAddressInterface.Read();
            var model = new WorkPermitAddressModel(conf);
            return View(model);
        }

        [AuthorizeUser(Roles = "Admin")]
        public ActionResult Submit(WorkPermitAddressModel workPermitAddressModel)
        {
            if (workPermitAddressModel.WorkPermitAddressArray != null)
            {
                foreach (var record in workPermitAddressModel.WorkPermitAddressArray)
                {
                    if (record.Id == 0)
                    {
                        WorkPermitAddressInterface.Create(record);
                    }
                    else
                    {
                        WorkPermitAddressInterface.Update(record);
                    }
                }
            }
            return RedirectToAction("Index");
        }

        public void RemoveAddressItem(int id)
        {
            if (id != 0)
                WorkPermitAddressInterface.Delete(id);
        }
    }
}
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
    public class RestDayPayController : BaseController
    {
        // GET: RestDayPay
        public ActionResult Index()
        {
            var conf = RestDayPayInterface.Read();
            var model = new RestDayPayModel(conf);
            return View(model);
        }

        public ActionResult RestDayPay(int? restDayPayId, string pageMode)
        {
            if (restDayPayId != null)
            {
                var conf = RestDayPayInterface.Read(restDayPayId.Value);
                var model = new RestDayPayModel(conf);

                model.PageMode = pageMode != null ? pageMode : string.Empty;
                return View(model);
            }
            else
            {
                return View(new RestDayPayModel());
            }
        }

        [AuthorizeUser(Roles = "Admin")]
        public ActionResult Submit(RestDayPayModel model)
        {
            if (model.RestDayPayRecord != null)
            {
                if (model.RestDayPayRecord.Id == 0)
                {
                    RestDayPayInterface.Create(model.RestDayPayRecord);
                }
                else
                {
                    RestDayPayInterface.Update(model.RestDayPayRecord);
                }
            }
            return RedirectToAction("Index");
        }
    }
}
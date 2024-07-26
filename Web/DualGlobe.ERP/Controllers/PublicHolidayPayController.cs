using System;
using System.Web.Mvc;
using DualGlobe.ERP.Models;
using DualGlobe.ERP.Utility;
using Library.DualGlobe.ERP.Interfaces;

namespace DualGlobe.ERP.Controllers
{
    public class PublicHolidayPayController : BaseController
    {
        // GET: PublicHolidayPay
        public ActionResult Index()
        {
            var conf = PublicHolidayPayInterface.Read();
            var model = new PublicHolidayPayModel(conf);
            return View(model);
        }

        public ActionResult PublicHolidayPay(int? publicHolidayPayId, string pageMode)
        {
            if (publicHolidayPayId != null)
            {
                var conf = PublicHolidayPayInterface.Read(publicHolidayPayId.Value);
                var model = new PublicHolidayPayModel(conf);

                model.PageMode = pageMode != null ? pageMode : string.Empty;
                return View(model);
            }
            else
            {
                return View(new PublicHolidayPayModel());
            }
        }

        [AuthorizeUser(Roles = "Admin")]
        public ActionResult Submit(PublicHolidayPayModel model)
        {
            if (model.PublicHolidayPayRecord != null)
            {
                if (model.PublicHolidayPayRecord.Id == 0)
                {
                    PublicHolidayPayInterface.Create(model.PublicHolidayPayRecord);
                }
                else
                {
                    PublicHolidayPayInterface.Update(model.PublicHolidayPayRecord);
                }
            }
            return RedirectToAction("Index");
        }
    }
}
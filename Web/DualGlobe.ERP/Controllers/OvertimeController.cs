using System;
using System.Web.Mvc;
using DualGlobe.ERP.Models;
using DualGlobe.ERP.Utility;
using Library.DualGlobe.ERP.Interfaces;

namespace DualGlobe.ERP.Controllers
{
    public class OvertimeController : BaseController
    {
        // GET: Overtime
        public ActionResult Index()
        {
            var conf = OvertimeInterface.Read();
            var model = new OvertimeModel(conf);
            return View(model);
        }

        public ActionResult Overtime(int? overTimeId, string pageMode)
        {
            if (overTimeId != null)
            {
                var conf = OvertimeInterface.Read(overTimeId.Value);
                var model = new OvertimeModel(conf);

                model.PageMode = pageMode != null ? pageMode : string.Empty;
                return View(model);
            }
            else
            {
                return View(new OvertimeModel());
            }
        }

        [AuthorizeUser(Roles = "Admin")]
        public ActionResult Submit(OvertimeModel model)
        {
            if (model.OvertimeRecord != null)
            {
                if (model.OvertimeRecord.Id == 0)
                {
                    OvertimeInterface.Create(model.OvertimeRecord);
                }
                else
                {
                    OvertimeInterface.Update(model.OvertimeRecord);
                }
            }
            return RedirectToAction("Index");
        }
    }
}
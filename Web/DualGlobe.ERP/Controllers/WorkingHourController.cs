using System;
using System.Web.Mvc;
using DualGlobe.ERP.Models;
using DualGlobe.ERP.Utility;
using Library.DualGlobe.ERP.Interfaces;

namespace DualGlobe.ERP.Controllers
{
    public class WorkingHourController : BaseController
    {
        // GET: WorkingHour
        public ActionResult Index()
        {
            var conf = WorkingHourInterface.Read();
            var model = new WorkingHourModel(conf);
            return View(model);
        }

        public ActionResult WorkingHour(int? workingHourId, string pageMode)
        {
            if (workingHourId != null)
            {
                var conf = WorkingHourInterface.Read(workingHourId.Value);
                var model = new WorkingHourModel(conf);

                model.PageMode = pageMode != null ? pageMode : string.Empty;
                return View(model);
            }
            else
            {
                return View(new WorkingHourModel());
            }
        }

        [AuthorizeUser(Roles = "Admin")]
        public ActionResult Submit(WorkingHourModel model)
        {
            if (model.WorkingHourRecord != null)
            {
                if (model.WorkingHourRecord.Id == 0)
                {
                    WorkingHourInterface.Create(model.WorkingHourRecord);
                }
                else
                {
                    WorkingHourInterface.Update(model.WorkingHourRecord);
                }
            }
            return RedirectToAction("Index");
        }
    }
}
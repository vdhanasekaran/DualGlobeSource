using System;
using System.Linq;
using System.Web.Mvc;
using DualGlobe.ERP.Models;
using DualGlobe.ERP.Utility;
using Library.DualGlobe.ERP.Interfaces;

namespace DualGlobe.ERP.Controllers
{
    public class InsuranceController : BaseController
    {
        // GET: Insurance
        public ActionResult Index(string expire)
        {
            if (!string.IsNullOrEmpty(expire))
            {
                var conf = InsuranceInterface.ReadExpiringInsurance();
                var model = new InsuranceModel(conf);
                return View(model);
            }
            else {
                var conf = InsuranceInterface.Read();
                var model = new InsuranceModel(conf);
                return View(model);
            }
        }

        public ActionResult Insurance(int? insuranceId, string pageMode)
        {
            if (insuranceId != null)
            {
                var conf = InsuranceInterface.Read(insuranceId.Value);
                var model = new InsuranceModel(conf);

                model.PageMode = pageMode != null ? pageMode : string.Empty;
                return View(model);
            }
            else
            {
                return View(new InsuranceModel());
            }
        }

        [AuthorizeUser(Roles = "Admin")]
        public ActionResult Submit(InsuranceModel model)
        {
            if (model.InsuranceRecord != null)
            {
                if (model.InsuranceRecord.Id == 0)
                {
                    InsuranceInterface.Create(model.InsuranceRecord);
                }
                else
                {
                    InsuranceInterface.Update(model.InsuranceRecord);
                }
            }
            return RedirectToAction("Index");
        }
    }
}
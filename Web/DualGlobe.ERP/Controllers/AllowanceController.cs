using System;
using System.Web.Mvc;
using DualGlobe.ERP.Models;
using Library.DualGlobe.ERP.Interfaces;
using DualGlobe.ERP.Utility;

namespace DualGlobe.ERP.Controllers
{
    public class AllowanceController : BaseController
    {
        // GET: Allowance
        public ActionResult Index(AllowanceModel allowanceModel)
        {
            if (string.IsNullOrEmpty(allowanceModel.SelectedMonth) && string.IsNullOrEmpty(allowanceModel.SelectedYear))
            {
                allowanceModel.SelectedMonth = DateTime.Today.Month.ToString();
                allowanceModel.SelectedYear = DateTime.Today.Year.ToString();
            }
            DateTime allowanceDate = new DateTime(Convert.ToInt32(allowanceModel.SelectedYear), Convert.ToInt32(allowanceModel.SelectedMonth), 1);
            var conf = AllowanceInterface.ReadByMonth(allowanceDate);
            var model = new AllowanceModel(conf);
            model.SelectedMonth = allowanceModel.SelectedMonth;
            model.SelectedYear = allowanceModel.SelectedYear;
            return View(model);
        }

        public ActionResult Allowance(int? allowanceId, string pageMode)
        {
            if (allowanceId != null)
            {
                var conf = AllowanceInterface.Read(allowanceId.Value);
                var model = new AllowanceModel(conf);

                model.PageMode = pageMode != null ? pageMode : string.Empty;
                return View(model);
            }
            else
            {
                var model = new AllowanceModel();
                return View(model);
            }
        }

        [AuthorizeUser(Roles = "Admin")]
        public ActionResult Submit(AllowanceModel allowanceModel)
        {

            if (allowanceModel.allowanceRecord != null)
            {
                if (allowanceModel.allowanceRecord.Id == 0)
                {
                    AllowanceInterface.Create(allowanceModel.allowanceRecord);
                }
                else
                {
                    AllowanceInterface.Update(allowanceModel.allowanceRecord);
                }

            }

            return RedirectToAction("Index");
        }

        public void RemoveItem(int id)
        {
            if (id != 0)
                AllowanceInterface.Delete(id);
        }

    }
}
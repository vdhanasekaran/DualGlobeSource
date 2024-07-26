using System.Web.Mvc;
using Library.DualGlobe.ERP.Interfaces;
using DualGlobe.ERP.Models;
using System;
using DualGlobe.ERP.Utility;

namespace DualGlobe.ERP.Controllers
{
    public class IncomeController : BaseController
    {
        public ActionResult OtherIncome(OtherIncomeModel incomeModel)
        {
            if (string.IsNullOrEmpty(incomeModel.SelectedMonth) && string.IsNullOrEmpty(incomeModel.SelectedYear))
            {
                incomeModel.SelectedMonth = DateTime.Today.Month.ToString();
                incomeModel.SelectedYear = DateTime.Today.Year.ToString();
            }
            var conf = OtherIncomeInterface.ReadByMonthAndYear(Convert.ToInt32(incomeModel.SelectedMonth), Convert.ToInt32(incomeModel.SelectedYear));
            var model = new OtherIncomeModel(conf);
            model.SelectedMonth = incomeModel.SelectedMonth;
            model.SelectedYear = incomeModel.SelectedYear;
            return View(model);
        }

        public ActionResult AddIncome(int? otherIncomeId, string pageMode)
        {
            if (otherIncomeId != null)
            {
                var conf = OtherIncomeInterface.Read(otherIncomeId.Value);
                var model = new OtherIncomeModel(conf);

                model.PageMode = pageMode != null ? pageMode : string.Empty;
                return View(model);
            }
            else
            {
                return View(new OtherIncomeModel());
            }
        }

        [AuthorizeUser(Roles = "Admin")]
        public ActionResult Submit(OtherIncomeModel model)
        {
            if (model.otherIncomeRecord != null)
            {
                if (model.otherIncomeRecord.Id == 0)
                {
                    OtherIncomeInterface.Create(model.otherIncomeRecord);
                }
                else
                {
                    OtherIncomeInterface.Update(model.otherIncomeRecord);
                }
            }

            return RedirectToAction("OtherIncome");
        }
    }
}
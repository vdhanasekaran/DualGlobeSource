using System;
using System.Web.Mvc;
using DualGlobe.ERP.Models;
using Library.DualGlobe.ERP.Interfaces;
using Library.DualGlobe.ERP.Models;
using System.Collections.Generic;
using DualGlobe.ERP.Utility;

namespace DualGlobe.ERP.Controllers
{
    public class LoanAndAdvanceController : BaseController
    {
        // GET: LoanAndAdvance
        public ActionResult Index(LoanAndAdvanceModel loanAndAdvanceModel)
        {
            if (string.IsNullOrEmpty(loanAndAdvanceModel.SelectedMonth) && string.IsNullOrEmpty(loanAndAdvanceModel.SelectedYear))
            {
                loanAndAdvanceModel.SelectedMonth = DateTime.Today.Month.ToString();
                loanAndAdvanceModel.SelectedYear = DateTime.Today.Year.ToString();
            }
            DateTime loanDate = new DateTime(Convert.ToInt32(loanAndAdvanceModel.SelectedYear), Convert.ToInt32(loanAndAdvanceModel.SelectedMonth), 1);
            var conf = LoanAndAdvanceInterface.ReadByMonth(loanDate);
            var model = new LoanAndAdvanceModel(conf);
            model.SelectedMonth = loanAndAdvanceModel.SelectedMonth;
            model.SelectedYear = loanAndAdvanceModel.SelectedYear;
            return View(model);
        }

        public ActionResult LoanAndAdvance(int? loanAndAdvanceId, string pageMode)
        {
            if (loanAndAdvanceId != null)
            {
                var conf = LoanAndAdvanceInterface.Read(loanAndAdvanceId.Value);
                var model = new LoanAndAdvanceModel(conf);

                model.PageMode = pageMode != null ? pageMode : string.Empty;
                return View(model);
            }
            else
            {
                return View(new LoanAndAdvanceModel());
            }
        }

        public ActionResult Submit(LoanAndAdvanceModel loanAndAdvanceModel)
        {

            if (loanAndAdvanceModel.loanAndAdvanceRecord != null)
            {
                List<LoanAndAdvanceDetails> loanAndAdvanceDetailList = new List<LoanAndAdvanceDetails>();
                if (string.Compare(loanAndAdvanceModel.loanAndAdvanceRecord.Mode, "Month", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    int totalMonths = Convert.ToInt32(loanAndAdvanceModel.loanAndAdvanceRecord.RepaymentDuration);
                    loanAndAdvanceModel.loanAndAdvanceRecord.LoanRepaymentEndDate = loanAndAdvanceModel.loanAndAdvanceRecord.LoanRepaymentStartDate.AddMonths(totalMonths - 1);


                    for (int i = 0; i < totalMonths; i++)
                    {
                        LoanAndAdvanceDetails loanAndAdvanceDetail = new LoanAndAdvanceDetails();
                        loanAndAdvanceDetail.LoanDetectionDate = loanAndAdvanceModel.loanAndAdvanceRecord.LoanRepaymentStartDate.AddMonths(i);
                        loanAndAdvanceDetail.LoanDetectionAmount = loanAndAdvanceModel.loanAndAdvanceRecord.LoanAmount / totalMonths;
                        loanAndAdvanceDetail.IsDetected = false;
                        loanAndAdvanceDetailList.Add(loanAndAdvanceDetail);
                    }


                }
                else if (string.Compare(loanAndAdvanceModel.loanAndAdvanceRecord.Mode, "Amount", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    decimal amount = Convert.ToDecimal(loanAndAdvanceModel.loanAndAdvanceRecord.RepaymentAmount);
                    int totalMonths = (int)(loanAndAdvanceModel.loanAndAdvanceRecord.LoanAmount / amount);
                    loanAndAdvanceModel.loanAndAdvanceRecord.LoanRepaymentEndDate = loanAndAdvanceModel.loanAndAdvanceRecord.LoanRepaymentStartDate.AddMonths(totalMonths - 1);

                    for (int i = 0; i < totalMonths; i++)
                    {
                        LoanAndAdvanceDetails loanAndAdvanceDetail = new LoanAndAdvanceDetails();
                        loanAndAdvanceDetail.LoanDetectionDate = loanAndAdvanceModel.loanAndAdvanceRecord.LoanRepaymentStartDate.AddMonths(i);
                        loanAndAdvanceDetail.LoanDetectionAmount = loanAndAdvanceModel.loanAndAdvanceRecord.LoanAmount / totalMonths;
                        loanAndAdvanceDetail.IsDetected = false;
                        loanAndAdvanceDetailList.Add(loanAndAdvanceDetail);
                    }
                }

                loanAndAdvanceModel.loanAndAdvanceRecord.LoanStatus = "Active";
                loanAndAdvanceModel.loanAndAdvanceRecord.loanAndAdvanceDetails = loanAndAdvanceDetailList;

                if (loanAndAdvanceModel.loanAndAdvanceRecord.Id == 0)
                {

                    LoanAndAdvanceInterface.Create(loanAndAdvanceModel.loanAndAdvanceRecord);
                }
                else
                {
                    LoanAndAdvanceInterface.Update(loanAndAdvanceModel.loanAndAdvanceRecord);
                }
            }
            ViewBag.SuccessMessage = "Load details added / updated";
            return RedirectToAction("LoanAndAdvance", new { loanAndAdvanceId = loanAndAdvanceModel.loanAndAdvanceRecord.Id, pageMode = "Edit" });
        }

        public ActionResult ViewDetail(int? id)
        {

            LoanAndAdvanceModel model = new LoanAndAdvanceModel();
            if (id != null)
            {
                var loanAdvanceRecord = LoanAndAdvanceInterface.Read(id.Value);
                model = new LoanAndAdvanceModel(loanAdvanceRecord);
            }

            return PartialView("EMIPreview", model.loanAndAdvanceRecord.loanAndAdvanceDetails);
        }

        public ActionResult LoanDetails(int? id)
        {

            LoanAndAdvanceModel model = new LoanAndAdvanceModel();
            if (id != null)
            {
                var loanAdvanceRecord = LoanAndAdvanceInterface.Read(id.Value);
                model = new LoanAndAdvanceModel(loanAdvanceRecord);
            }

            return View(model);
        }

        [AuthorizeUser(Roles = "Admin")]
        public ActionResult SubmitDetail(LoanAndAdvanceModel model)
        {

            if (model.loanAndAdvanceRecord.loanAndAdvanceDetails != null)
            {
                foreach (var detail in model.loanAndAdvanceRecord.loanAndAdvanceDetails)
                {
                    LoanAndAdvanceInterface.Update(detail);
                }
            }

            return RedirectToAction("LoanAndAdvance", new { @loanAndAdvanceId = model.loanAndAdvanceRecord.Id, @pageMode = "Edit"});
        }

        public void RemoveItem(int id)
        {
            if (id != 0)
                LoanAndAdvanceInterface.Delete(id);
        }
    }
}
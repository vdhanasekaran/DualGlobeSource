using System.Web.Mvc;
using Library.DualGlobe.ERP.Interfaces;
using Library.DualGlobe.ERP.Models;
using DualGlobe.ERP.Models;
using DualGlobe.ERP.Utility;
using System.Collections.Generic;
using System;
using System.Linq;

namespace DualGlobe.ERP.Controllers
{
    public class QuotationController : BaseController
    {
        // GET: Quotation
        public ActionResult Index()
        {
            var conf = QuotationInterface.Read();
            var model = new QuotationModel(conf);
            return View(model);
        }

        public ActionResult Quotation(int? quotationId, string pageMode)
        {
            if (quotationId != null)
            {
                var conf = QuotationInterface.Read(quotationId.Value);
                var model = new QuotationModel(conf);
                if (!string.IsNullOrEmpty(model.quotationRecord.Notes))
                {
                    model.quotationRecord.Notes = model.quotationRecord.Notes.Replace("<br />", "\r\n");
                    model.quotationRecord.Notes = model.quotationRecord.Notes.Replace("<br />", "\n");
                }
                model.quotationRecord.TermsAndCondtions = model.quotationRecord.TermsAndCondtions.Replace("<br />", "\r\n");
                if (model.quotationRecord.quotationItems != null && model.quotationRecord.quotationItems.Count > 0)
                {
                    for (int i = 0; i < model.quotationRecord.quotationItems.Count; i++)
                    {
                        model.quotationRecord.quotationItems[i].Description = model.quotationRecord.quotationItems[i].Description.Replace("<br />", "\r\n");
                    }
                }

                model.PageMode = pageMode != null ? pageMode : string.Empty;
                return View(model);
            }
            else
            {
                return View(new QuotationModel() { quotationRecord = new Quotation()});
            }
        }

        public JsonResult GetQuotationDetails(int id)
        {
            var quotation = QuotationInterface.Read(id);
            var model = new QuotationModel(quotation);
            var discountPercentage = Convert.ToDecimal(0.00);
            if (!string.IsNullOrEmpty(model.quotationRecord.DiscountType) && string.Compare(model.quotationRecord.DiscountType, "Amount", StringComparison.OrdinalIgnoreCase) == 0)
            {
                discountPercentage = (model.quotationRecord.DiscountValue.GetValueOrDefault(0) * 100) / ((model.quotationRecord.QuotationValue.GetValueOrDefault(1) - model.quotationRecord.GST.GetValueOrDefault(0)) + model.quotationRecord.DiscountValue.GetValueOrDefault(0));
            }

            //var result = new Quotation();
            //result = quotation;

            var result = new
            {
                QuotationId = model.quotationRecord.Id,
                Address = Utility.Utilities.GetClientAddress(model.quotationRecord.ClientId),
                Attention = model.quotationRecord.To,
                ProjectId = model.quotationRecord.ProjectId,
                Email = !string.IsNullOrEmpty(model.quotationRecord.Email) ? model.quotationRecord.Email : ClientInterface.Read(model.quotationRecord.ClientId).EmailAddress,
                FromDate = model.quotationRecord.DateCreated.Value.ToString("d"),
                ToDate = model.quotationRecord.ValidUntil != null ? model.quotationRecord.ValidUntil.Value.ToString("d") : "",
                ActualAmount = model.quotationRecord.ActualAmount,
                Amount = model.quotationRecord.QuotationValue,
                TotalGSTAmount = model.quotationRecord.GST,
                DiscountType = !string.IsNullOrEmpty(model.quotationRecord.DiscountType) ? model.quotationRecord.DiscountType : string.Empty,
                DiscountValue = model.quotationRecord.DiscountValue.GetValueOrDefault(0),
                //DiscountPercentage = discountPercentage,
                DiscountPercentage = model.quotationRecord.DiscountValue,
                GSTType = model.quotationRecord.GSTType,
                SubTotal = model.quotationRecord.SubTotal,
                DiscountAmoount = model.quotationRecord.DiscountAmount
            };

            //if (result.DateCreated != null && result.DateCreated.Value.Year < 2023 && DateTime.UtcNow.Year >= 2023)
            //{
            //    result.GST = result.ActualAmount * (8m / 100m);
            //    result.QuotationValue = result.ActualAmount + result.GST;
            //}

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetProject(string clientId)
        {
            int Id;
            List<SelectListItem> projectNames = new List<SelectListItem>();
            if (!string.IsNullOrEmpty(clientId))
            {
                Id = Convert.ToInt32(clientId);
                var projects = ProjectInterface.ReadByClientId(Id);
                foreach (var project in projects)
                {
                    projectNames.Add(new SelectListItem
                    {
                        Value = project.Id.ToString(),
                        Text = project.Id.ToString() + " : " + project.ProjectName
                    });
                }
            }
            return Json(projectNames, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetProjectForQuotation(string clientId)
        {
            int Id;
            List<SelectListItem> projectNames = new List<SelectListItem>();
            if (!string.IsNullOrEmpty(clientId))
            {
                Id = Convert.ToInt32(clientId);
                var projects = ProjectInterface.ReadByClientId(Id);
                var quotations = QuotationInterface.Read();
                foreach (var project in projects)
                {
                    if (!quotations.Any(i => i.ProjectId == project.Id))
                    {
                        projectNames.Add(new SelectListItem
                        {
                            Value = project.Id.ToString(),
                            Text = project.Id.ToString() + " : " + project.ProjectName
                        });
                    }
                }
            }
            return Json(projectNames, JsonRequestBehavior.AllowGet);
        }

        [AuthorizeUser(Roles = "Admin")]
        [ValidateInput(false)]
        public ActionResult Submit(QuotationModel model)
        {
            decimal amount = 0;

            if (model.quotationRecord != null)
            {
                var conf = ClientInterface.Read(model.quotationRecord.ClientId);
                model.quotationRecord.CompanyName = conf.CompanyName;

                if (!string.IsNullOrEmpty(model.quotationRecord.Notes))
                {
                    model.quotationRecord.Notes = model.quotationRecord.Notes.Replace("\r\n", "<br />");
                    model.quotationRecord.Notes = model.quotationRecord.Notes.Replace("\n", "<br />");
                }
                model.quotationRecord.TermsAndCondtions = model.quotationRecord.TermsAndCondtions.Replace("\r\n", "<br />");

                if (model.quotationRecord.Id == 0)
                {
                    for (int j = 0; j < model.quotationRecord.quotationItems.Count; j++)
                    {
                        model.quotationRecord.quotationItems[j].Description = model.quotationRecord.quotationItems[j].Description.Replace("\r\n", "<br />");
                        amount += model.quotationRecord.quotationItems[j].Quantity * model.quotationRecord.quotationItems[j].UnitPrice.GetValueOrDefault();
                    }
                    //calculateQuotationValue(model, amount);
                    QuotationInterface.Create(model.quotationRecord);
                }
                else
                {

                    if (model.quotationRecord.Stage == "Confirmed")
                    {
                        var project = ProjectInterface.Read(model.quotationRecord.ProjectId);
                        project.Status = "Ongoing";
                        ProjectInterface.Update(project);
                    }
                    if (model.quotationRecord.quotationItems != null && model.quotationRecord.quotationItems.Count > 0)
                    {
                        foreach (var quotationItem in model.quotationRecord.quotationItems)
                        {
                            quotationItem.Description = quotationItem.Description.Replace("\r\n", "<br />");
                            quotationItem.QuotationId = model.quotationRecord.Id;
                            if (quotationItem.Id == 0)
                                QuotationInterface.InsertquotationDetail(quotationItem);
                            else
                                QuotationInterface.UpdatequotationDetail(quotationItem);

                            amount += quotationItem.Quantity * quotationItem.UnitPrice.GetValueOrDefault();
                        }
                    }
                    //calculateQuotationValue(model, amount);
                    QuotationInterface.Update(model.quotationRecord);
                }
            }

            return RedirectToAction("Index");
        }

        private void calculateQuotationValue(QuotationModel model, decimal amount)
        {

            decimal actualAmount = 0;
            decimal finalAmount = 0;
            decimal gstAmount = 0;
            decimal discountVal = 0;
            decimal discountAmount = 0;

            if (model.quotationRecord.DiscountType != "")
            {
                if (model.quotationRecord.DiscountType == "Amount")
                {
                    if (model.quotationRecord.DiscountValue.GetValueOrDefault() != 0)
                    {
                        discountAmount = model.quotationRecord.DiscountValue.GetValueOrDefault();
                        discountVal = discountAmount;
                        actualAmount = amount - discountAmount;
                    }
                    else
                    {
                        discountVal = 0;
                        actualAmount = amount;
                    }
                }
                else if (model.quotationRecord.DiscountType == "Percentage")
                {
                    if (model.quotationRecord.DiscountValue.GetValueOrDefault() != 0)
                    {
                        var discountPercentage = model.quotationRecord.DiscountValue.GetValueOrDefault();
                        discountAmount = amount * discountPercentage / 100;
                        actualAmount = amount - discountAmount;
                        discountVal = discountPercentage;
                    }
                    else
                    {
                        discountVal = 0;
                        actualAmount = amount;
                    }
                }
                else
                {
                    actualAmount = amount;
                }
            }
            else
            {
                discountVal = 0;
                actualAmount = amount;
            }

            if (model.quotationRecord.GSTType == "E")
            {
                gstAmount = actualAmount * (decimal)0.07;
                finalAmount = actualAmount + gstAmount;
            }
            else if (model.quotationRecord.GSTType == "I")
            {
                gstAmount = actualAmount * (decimal)0.07;
                finalAmount = actualAmount - gstAmount;
            }
            else
            {
                gstAmount = 0;
                finalAmount = actualAmount;
            }

            // finally set calculated values to the model

            model.quotationRecord.SubTotal = amount;
            model.quotationRecord.DiscountAmount = discountAmount;
            model.quotationRecord.ActualAmount = actualAmount;

            model.quotationRecord.GST = gstAmount;
            model.quotationRecord.QuotationValue = finalAmount;


        }

        public ActionResult QuotationDetail(int? id)
        {
            QuotationModel model = new QuotationModel();
            if (id != null)
            {
                var quotationRecord = QuotationInterface.Read(id.Value);
                model = new QuotationModel(quotationRecord);
            }
            return PartialView("ViewQuotation", model);
        }

        public ActionResult PrintQuotation(int? id)
        {
            QuotationModel model = new QuotationModel();
            if (id != null)
            {
                var quotationRecord = QuotationInterface.Read(id.Value);
                //if (!string.IsNullOrEmpty(quotationRecord.Notes))
                //{
                //    quotationRecord.Notes = quotationRecord.Notes.Replace("<br />", "\r\n");
                //    quotationRecord.Notes = quotationRecord.Notes.Replace("<br />", "\n");
                //}
                model = new QuotationModel(quotationRecord);
            }
            return View(model);
        }



        public void RemoveQuotationItem(int id)
        {
            if (id != 0)
                QuotationInterface.DeleteQuotationDetail(id);
        }
    }
}
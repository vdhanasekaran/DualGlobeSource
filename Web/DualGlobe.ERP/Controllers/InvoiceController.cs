using DualGlobe.ERP.Models;
using DualGlobe.ERP.Utility;
using Library.DualGlobe.ERP.Interfaces;
using Library.DualGlobe.ERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace DualGlobe.ERP.Controllers
{
    public class InvoiceController : BaseController
    {
        // GET: Invoice
        public ActionResult Index()
        {
            var invoices = InvoiceInterface.Read();
            var model = new InvoiceModel(invoices);
            return View(model);
        }

        [ValidateInput(false)]
        public ActionResult Invoice(int? invoiceId, string pageMode)
        {
            if (invoiceId != null)
            {
                var invoices = InvoiceInterface.Read(invoiceId.Value);
                var model = new InvoiceModel(invoices);

                var quotation = QuotationInterface.Read(model.InvoiceRecord.QuotationId.Value);
                model.QuotationRecord = new Quotation();
                model.QuotationRecord = quotation;

                if (model.InvoiceRecord != null && model.InvoiceRecord.InvoicePhases != null && model.InvoiceRecord.InvoicePhases.Any())
                {
                    for (int i = 0; i < model.InvoiceRecord.InvoicePhases.Count; i++)
                    {
                        model.InvoiceRecord.InvoicePhases[i].Description = model.InvoiceRecord.InvoicePhases[i].Description.Replace("<br />", "\r\n");
                    }
                }
                model.PageMode = pageMode != null ? pageMode : string.Empty;
                return View(model);
            }
            else
            {
                var model = new InvoiceModel();
                return View(model);
            }

        }

        [HttpPost]
        public ActionResult GetQuotation(string clientId)
        {
            int Id;
            List<SelectListItem> quotationNames = new List<SelectListItem>();
            if (!string.IsNullOrEmpty(clientId))
            {
                Id = Convert.ToInt32(clientId);
                var quotations = QuotationInterface.ReadByClientId(Id).Where(i => i.Stage == "Confirmed");
                foreach (var quotation in quotations)
                {
                    var invoices = InvoiceInterface.ReadByQuotationId(quotation.Id);
                    if (invoices.AsEnumerable().Sum(j => j.InvoiceAmount) >= quotation.QuotationValue)
                    {
                        //do not add quotation to List
                    }
                    else
                    {
                        quotationNames.Add(new SelectListItem
                        {
                            Value = quotation.Id.ToString(),
                            Text = quotation.Description.ToString()
                        });
                    }
                }
            }
            return Json(quotationNames, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddPayment(int? invoiceId)
        {
            if (invoiceId != null)
            {
                var payments = PaymentInterface.ReadByInvoice(invoiceId.Value);
                var model = new InvoiceModel(payments);

                var invoice = InvoiceInterface.Read(invoiceId.Value);
                model.InvoiceRecord = invoice;

                model.PaymentRecord = new Payment();
                model.PaymentRecord.InvoiceId = invoiceId.Value;

                return View(model);
            }
            else
            {
                var model = new InvoiceModel();
                return View(model);
            }

        }

        public ActionResult SubmitPayment(InvoiceModel invoiceModel)
        {
            if (invoiceModel.PaymentRecord != null)
            {

                if (invoiceModel.PaymentRecord.Id == 0)
                {
                    PaymentInterface.Create(invoiceModel.PaymentRecord);
                }
                else
                {
                    PaymentInterface.Update(invoiceModel.PaymentRecord);

                }
            }

            var payments = PaymentInterface.ReadByInvoice(invoiceModel.PaymentRecord.InvoiceId.Value);
            var invoice = InvoiceInterface.Read(invoiceModel.PaymentRecord.InvoiceId.Value);
            var paidAmount = payments.AsEnumerable().Sum(i => i.Amount);
            if ((int)Math.Floor(paidAmount.Value) == (int)Math.Floor(invoice.InvoiceAmount.Value))
            {
                var model = new InvoiceModel(invoice);
                model.InvoiceRecord.Status = "Paid";
                InvoiceInterface.Update(model.InvoiceRecord);
            }

            //var quotation = QuotationInterface.Read(invoice.QuotationId.Value);
            //var invoices = InvoiceInterface.ReadByQuotationId(quotation.Id).Where(i => i.Status == "Paid");
            //var invoicePaidAmount = invoices.AsEnumerable().Sum(j => j.InvoiceAmount);

            //if ((int)Math.Floor(invoicePaidAmount.Value) == (int)Math.Floor(quotation.QuotationValue.Value))
            //{
            //    quotation.Stage = "Paid";
            //    QuotationInterface.Update(quotation);
            //}

            return RedirectToAction("Invoice", new { @invoiceId = invoiceModel.PaymentRecord.InvoiceId, @pageMode = "Edit" });
        }

        [AuthorizeUser(Roles = "Admin")]
        [ValidateInput(false)]
        public ActionResult Submit(InvoiceModel invoiceModel)
        {
            decimal amount = 0;
            string strGSTType = string.Empty;

            if (invoiceModel.InvoiceRecord != null)
            {
                var comp = ClientInterface.Read(invoiceModel.InvoiceRecord.ClientId.Value);
                invoiceModel.InvoiceRecord.CompanyName = comp.CompanyName;

                var quot = QuotationInterface.Read(invoiceModel.InvoiceRecord.QuotationId.Value);
                invoiceModel.InvoiceRecord.Subject = quot.Subject;
                strGSTType = quot.GSTType;

                invoiceModel.InvoiceRecord.Status = "UnPaid";

                //if (invoiceModel.InvoiceRecord.DiscountValue > 0)
                //{
                //    invoiceModel.InvoiceRecord.TotalDiscountedGST = invoiceModel.InvoiceRecord.TotalGSTAmount;
                //}

                if (invoiceModel.InvoiceRecord.Id == 0)
                {
                    if (invoiceModel.InvoiceRecord != null && invoiceModel.InvoiceRecord.InvoicePhases != null && invoiceModel.InvoiceRecord.InvoicePhases.Any())
                    {

                        invoiceModel.InvoiceRecord.PaymentInformation = invoiceModel.InvoiceRecord.PaymentInformation?.Replace("\r\n", "<br />");

                        for (int i = 0; i < invoiceModel.InvoiceRecord.InvoicePhases.Count; i++)
                        {
                            invoiceModel.InvoiceRecord.InvoicePhases[i].Description = invoiceModel.InvoiceRecord.InvoicePhases[i].Description.Replace("\r\n", "<br />");

                            if (invoiceModel.InvoiceRecord.InvoicePhases[i].Percentage.GetValueOrDefault() > 0)
                            {
                                invoiceModel.InvoiceRecord.InvoicePhases[i].PhaseAmount = invoiceModel.InvoiceRecord.InvoicePhases[i].QuotationAmount * invoiceModel.InvoiceRecord.InvoicePhases[i].Percentage / 100;

                                amount += invoiceModel.InvoiceRecord.InvoicePhases[i].PhaseAmount.GetValueOrDefault();
                            }
                        }
                    }

                    calculateInvoiceValue(invoiceModel, amount, strGSTType);

                    if (invoiceModel.InvoiceRecord.IsProgressClaimRequired == "true")
                    {
                        invoiceModel.InvoiceRecord.GSTType = strGSTType;
                        InvoiceInterface.Create(invoiceModel.InvoiceRecord);
                    }
                    else
                    {
                        var invoice = InvoiceInterface.ReadByQuotationId(Convert.ToInt32(invoiceModel.InvoiceRecord.QuotationId));

                        if (invoice == null || invoice.Length == 0)
                        {
                            invoiceModel.InvoiceRecord.GSTType = strGSTType;
                            InvoiceInterface.Create(invoiceModel.InvoiceRecord);
                        }
                    }

                    //var quotation = QuotationInterface.Read(invoiceModel.InvoiceRecord.QuotationId.Value);
                    //quotation.Stage = "UnPaid";
                    //QuotationInterface.Update(quotation);

                    return RedirectToAction("Index");
                }
                else
                {
                    invoiceModel.InvoiceRecord.PaymentInformation = invoiceModel.InvoiceRecord.PaymentInformation?.Replace("\r\n", "<br />");

                    int i = 0;

                    foreach (var phaseItem in invoiceModel.InvoiceRecord.InvoicePhases)
                    {
                        phaseItem.Description = phaseItem.Description.Replace("\r\n", "<br />");
                        InvoiceInterface.UpdateInvoicePhase(phaseItem);

                        if (invoiceModel.InvoiceRecord.InvoicePhases[i].Percentage.GetValueOrDefault() > 0)
                        {
                            invoiceModel.InvoiceRecord.InvoicePhases[i].PhaseAmount = invoiceModel.InvoiceRecord.InvoicePhases[i].QuotationAmount * invoiceModel.InvoiceRecord.InvoicePhases[i].Percentage / 100;

                            amount += invoiceModel.InvoiceRecord.InvoicePhases[i].PhaseAmount.GetValueOrDefault();
                        }
                        i++;
                    }

                    calculateInvoiceValue(invoiceModel, amount, strGSTType);
                    InvoiceInterface.Update(invoiceModel.InvoiceRecord);

                    return RedirectToAction("Invoice", new { @invoiceId = invoiceModel.InvoiceRecord.Id, @pageMode = "Edit" });
                }
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        private void calculateInvoiceValue(InvoiceModel model, decimal amount, string strGSTType)
        {

            decimal actualAmount = 0;
            decimal finalAmount = 0;
            decimal gstAmount = 0;
            decimal discountVal = 0;
            decimal discountAmount = 0;

            if (model.InvoiceRecord.DiscountType != "")
            {
                if (model.InvoiceRecord.DiscountType == "Amount")
                {
                    if (model.InvoiceRecord.DiscountValue.GetValueOrDefault() != 0)
                    {
                        discountAmount = model.InvoiceRecord.DiscountValue.GetValueOrDefault();
                        discountVal = discountAmount;
                        actualAmount = amount - discountAmount;
                    }
                    else
                    {
                        discountVal = 0;
                        actualAmount = amount;
                    }
                }
                else if (model.InvoiceRecord.DiscountType == "Percentage")
                {
                    if (model.InvoiceRecord.DiscountValue.GetValueOrDefault() != 0)
                    {
                        var discountPercentage = model.InvoiceRecord.DiscountValue.GetValueOrDefault();
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

            if (strGSTType == "E")
            {
                if (model.InvoiceRecord.InvoiceDate.HasValue && model.InvoiceRecord.InvoiceDate.Value.Year >= 2024)
                {
                    gstAmount = actualAmount * (decimal)0.09;
                }
                else
                {
                    gstAmount = actualAmount * (decimal)0.08;
                }
                finalAmount = actualAmount + gstAmount;
            }
            else if (strGSTType == "I")
            {
                if (model.InvoiceRecord.InvoiceDate.HasValue && model.InvoiceRecord.InvoiceDate.Value.Year >= 2024)
                {
                    actualAmount = (amount - discountAmount) / (1m + 0.09m);
                    gstAmount = actualAmount * (decimal)0.09;
                    finalAmount = amount - discountAmount;
                }
                else
                {
                    actualAmount = (amount - discountAmount) / (1m + 0.08m);
                    gstAmount = actualAmount * (decimal)0.08;
                    finalAmount = amount - discountAmount;
                }
            }
            else
            {
                gstAmount = 0;
                finalAmount = actualAmount;
            }

            // finally set calculated values to the model

            model.InvoiceRecord.TotalPhaseAmount = amount;
            model.InvoiceRecord.DiscountAmount = discountAmount;
            model.InvoiceRecord.ActualAmount = actualAmount;

            model.InvoiceRecord.TotalGSTAmount = gstAmount;
            model.InvoiceRecord.InvoiceAmount = finalAmount;

        }

        public ActionResult GetInvoicePhaseDetails(int id, bool isClaimRequired)
        {
            List<InvoicePhase> InvoicePhaseItems = new List<InvoicePhase>();

            var quotation = QuotationInterface.Read(id);
            foreach (var item in quotation.quotationItems)
            {
                InvoicePhaseItems.Add(new InvoicePhase
                {
                    Description = item.Description.Replace("<br />", "\r\n"),
                    QuotationAmount = item.Amount,
                    //GST = item.GST,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice
                }
                );
            }
            var model = new InvoiceModel();
            model.QuotationRecord = new Quotation();
            model.QuotationRecord = quotation;

            model.InvoiceRecord = new Invoice();
            model.InvoiceRecord.QuotationId = id;
            model.InvoiceRecord.FromDate = quotation.DateCreated;
            model.InvoiceRecord.ToDate = quotation.ValidUntil;
            model.InvoiceRecord.ClientId = quotation.ClientId;
            model.InvoiceRecord.CompanyName = quotation.CompanyName;
            model.InvoiceRecord.Subject = quotation.Subject;
            // MKS
            model.InvoiceRecord.DiscountType = quotation.DiscountType;
            model.InvoiceRecord.DiscountValue = Math.Round(quotation.DiscountValue.GetValueOrDefault(0));
            model.InvoiceRecord.TotalGSTAmount = quotation.GST.GetValueOrDefault(0);

            model.InvoiceRecord.DiscountAmount = quotation.DiscountAmount.GetValueOrDefault(0);
            model.InvoiceRecord.ActualAmount = quotation.ActualAmount.GetValueOrDefault(0);


            //if (!string.IsNullOrEmpty(quotation.DiscountType) && quotation.DiscountType == "Percentage")
            //{
            //    model.InvoiceRecord.DiscountValue = quotation.DiscountValue.GetValueOrDefault(0);
            //}
            //else if (!string.IsNullOrEmpty(quotation.DiscountType) && quotation.DiscountType == "Amount")
            //{
            //    var quotationAmount = (quotation.QuotationValue.GetValueOrDefault(0) - quotation.GST.GetValueOrDefault(0));
            //    var discountAmount = quotation.DiscountValue.GetValueOrDefault(0);
            //    model.InvoiceRecord.DiscountValue = (discountAmount * 100) / (quotationAmount + discountAmount);
            //    model.InvoiceRecord.TotalGSTAmount = quotation.GST.GetValueOrDefault(0);
            //    model.InvoiceRecord.DiscountValue = Math.Round(model.InvoiceRecord.DiscountValue.GetValueOrDefault(0));
            //}
            //else
            //{
            //    var quotationAmount = (quotation.QuotationValue.GetValueOrDefault(0) - quotation.GST.GetValueOrDefault(0));
            //    model.InvoiceRecord.DiscountValue = 0;
            //    model.InvoiceRecord.TotalGSTAmount = quotation.GST.GetValueOrDefault(0);
            //}

            model.InvoiceRecord.InvoicePhases = InvoicePhaseItems;

            if (isClaimRequired)     // Partial Invoice for the Quotation
            {
                return PartialView("EditPhase", model);
            }
            else
            {
                model.InvoiceRecord.InvoiceAmount = quotation.QuotationValue.GetValueOrDefault(0);
                model.InvoiceRecord.PhaseName = quotation.Description;

                for (int i = 0; i < model.InvoiceRecord.InvoicePhases.Count; i++)
                {
                    //model.InvoiceRecord.InvoicePhases[i].GSTAmount = model.InvoiceRecord.InvoicePhases[i].GST.GetValueOrDefault(0);
                    model.InvoiceRecord.InvoicePhases[i].PhaseAmount = model.InvoiceRecord.InvoicePhases[i].QuotationAmount.GetValueOrDefault(0);
                    model.InvoiceRecord.InvoicePhases[i].PhaseInvoiceAmount = model.InvoiceRecord.InvoicePhases[i].QuotationAmount.GetValueOrDefault(0);
                    model.InvoiceRecord.InvoicePhases[i].Percentage = 100;
                    //model.InvoiceRecord.InvoicePhases[i].DiscountedGSTAmount = 0;
                    //model.InvoiceRecord.InvoicePhases[i].DiscountedPhaseInvoiceAmount = 0;

                    //if (quotation.DiscountType == "Percentage" && model.InvoiceRecord.DiscountValue.GetValueOrDefault(0) > 0)
                    //{
                    //    //model.InvoiceRecord.InvoicePhases[i].DiscountedGSTAmount = model.InvoiceRecord.InvoicePhases[i].GSTAmount.GetValueOrDefault(0) - (model.InvoiceRecord.InvoicePhases[i].GSTAmount.GetValueOrDefault(0) * model.InvoiceRecord.DiscountValue.GetValueOrDefault(0) * Convert.ToDecimal(0.01));
                    //    model.InvoiceRecord.InvoicePhases[i].DiscountedPhaseInvoiceAmount = model.InvoiceRecord.InvoicePhases[i].PhaseInvoiceAmount.GetValueOrDefault(0) - (model.InvoiceRecord.InvoicePhases[i].PhaseInvoiceAmount.GetValueOrDefault(0) * model.InvoiceRecord.DiscountValue.GetValueOrDefault(0) * Convert.ToDecimal(0.01));
                    //    //model.InvoiceRecord.TotalDiscountedGST = model.InvoiceRecord.InvoicePhases.AsEnumerable().Sum(j => j.DiscountedGSTAmount.GetValueOrDefault(0));
                    //    model.InvoiceRecord.TotalDiscountedPhaseInvoice = model.InvoiceRecord.InvoicePhases.AsEnumerable().Sum(j => j.DiscountedPhaseInvoiceAmount.GetValueOrDefault(0));
                    //}
                }

                //model.InvoiceRecord.TotalGSTAmount = model.InvoiceRecord.InvoicePhases.AsEnumerable().Sum(j => j.GSTAmount.GetValueOrDefault(0));
                model.InvoiceRecord.TotalPhaseAmount = model.InvoiceRecord.InvoicePhases.AsEnumerable().Sum(j => j.PhaseInvoiceAmount.GetValueOrDefault(0));

                //if (quotation.DiscountType == "Amount" && model.InvoiceRecord.DiscountValue.GetValueOrDefault(0) > 0)
                //{
                //    model.InvoiceRecord.TotalDiscountedGST = quotation.GST.GetValueOrDefault(0);
                //    model.InvoiceRecord.TotalDiscountedPhaseInvoice = quotation.DiscountValue.GetValueOrDefault(0);
                //}
                return PartialView("EditPhaseNoClaim", model);
            }
        }

        public ActionResult ProgressClaimDetail(int? invoiceId)
        {
            InvoiceModel model = new InvoiceModel();
            if (invoiceId != null)
            {
                var invoiceRecord = InvoiceInterface.Read(invoiceId.Value);
                model = new InvoiceModel(invoiceRecord);

                var quotation = QuotationInterface.Read(invoiceRecord.QuotationId.Value);
                model.QuotationRecord = new Quotation();
                model.QuotationRecord = quotation;

            }
            return PartialView("ViewProgressClaim", model);
        }

        public ActionResult GetExistingInvoiceDetails(int id)
        {
            var invoice = InvoiceInterface.ReadByQuotationId(id);

            if (invoice != null && invoice.Length > 0)
            {
                var model = new InvoiceModel(invoice);
                if (model.InvoiceRecord != null && model.InvoiceRecord.InvoicePhases != null && model.InvoiceRecord.InvoicePhases.Any())
                {
                    for (int i = 0; i < model.InvoiceRecord.InvoicePhases.Count; i++)
                    {
                        model.InvoiceRecord.InvoicePhases[i].Description = model.InvoiceRecord.InvoicePhases[i].Description.Replace("<br />", "\r\n");
                    }
                }
                return PartialView("AddPhase", model);
            }
            else
            {
                return PartialView("AddPhase", new InvoiceModel());
            }
        }

        public void RemovePhase(int id)
        {
            if (id != 0)
                InvoiceInterface.DeleteInvoicePhase(id);
        }

        public ActionResult PrintProgressClaim(int? invoiceId)
        {
            InvoiceModel model = new InvoiceModel();
            if (invoiceId != null)
            {
                var invoiceRecord = InvoiceInterface.Read(invoiceId.Value);
                model = new InvoiceModel(invoiceRecord);
            }
            return View(model);
        }

        public ActionResult PrintInvoice(int? invoiceId)
        {
            InvoiceModel model = new InvoiceModel();
            if (invoiceId != null)
            {
                var invoiceRecord = InvoiceInterface.Read(invoiceId.Value);
                var invoices = InvoiceInterface.ReadByQuotationId(invoiceRecord.QuotationId.Value);

                List<Invoice> invoiceList = new List<Invoice>();
                invoiceList.Add(invoiceRecord);
                foreach (var item in invoices)
                {
                    if (!invoiceList.Exists(i => i.Id == item.Id))
                        invoiceList.Add(item);
                }

                model = new InvoiceModel(invoiceList.ToArray());
            }
            return View(model);
        }

        public ActionResult InvoiceDetail(int? invoiceId)
        {
            InvoiceModel model = new InvoiceModel();
            if (invoiceId != null)
            {
                var invoiceRecord = InvoiceInterface.Read(invoiceId.Value);
                var invoices = InvoiceInterface.ReadByQuotationId(invoiceRecord.QuotationId.Value);

                var quotation = QuotationInterface.Read(invoiceRecord.QuotationId.Value);
                model.QuotationRecord = new Quotation();
                model.QuotationRecord = quotation;

                List<Invoice> invoiceList = new List<Invoice>();
                invoiceList.Add(invoiceRecord);
                foreach (var item in invoices)
                {
                    if (!invoiceList.Exists(i => i.Id == item.Id))
                        invoiceList.Add(item);
                }

                model = new InvoiceModel(invoiceList.ToArray());
                if (model.InvoiceRecord != null && model.InvoiceRecord.InvoicePhases != null && model.InvoiceRecord.InvoicePhases.Any())
                {
                    for (int i = 0; i < model.InvoiceRecord.InvoicePhases.Count; i++)
                    {
                        model.InvoiceRecord.InvoicePhases[i].Description = model.InvoiceRecord.InvoicePhases[i].Description.Replace("<br />", "\r\n");
                    }
                }
            }
            return PartialView("ViewInvoice", model);
        }
    }
}
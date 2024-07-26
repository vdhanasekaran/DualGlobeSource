using System.Web.Mvc;
using DualGlobe.ERP.Models;
using Library.DualGlobe.ERP.Interfaces;
using DualGlobe.ERP.Utility;
using System;
using System.Web;
using System.Collections.Generic;
using System.IO;

namespace DualGlobe.ERP.Controllers
{
    public class ExpenseController : BaseController
    {
        //OperationExpense
        public ActionResult OperationExpense(OperationExpenseModel operationExpenseModel)
        {
            if (string.IsNullOrEmpty(operationExpenseModel.SelectedMonth) && string.IsNullOrEmpty(operationExpenseModel.SelectedYear))
            {
                operationExpenseModel.SelectedMonth = DateTime.Today.Month.ToString();
                operationExpenseModel.SelectedYear = DateTime.Today.Year.ToString();
            }
            var conf = OperationExpenseInterface.ReadByMonthAndYear(Convert.ToInt32(operationExpenseModel.SelectedMonth), Convert.ToInt32(operationExpenseModel.SelectedYear));
            var model = new OperationExpenseModel(conf);
            model.SelectedMonth = operationExpenseModel.SelectedMonth;
            model.SelectedYear = operationExpenseModel.SelectedYear;
            return View(model);
        }

        public ActionResult AddOperationExpense(int? operationExpenseId, string pageMode)
        {
            if (operationExpenseId != null)
            {
                var conf = OperationExpenseInterface.Read(operationExpenseId.Value);
                var model = new OperationExpenseModel(conf);

                model.PageMode = pageMode != null ? pageMode : string.Empty;
                return View(model);
            }
            else
            {
                return View(new OperationExpenseModel());
            }
        }

        [AuthorizeUser(Roles = "Admin")]
        public ActionResult ExpenseSubmit(OperationExpenseModel model)
        {
            if (model.operationExpenseRecord != null)
            {
                if (model.operationExpenseRecord.Id == 0)
                {
                    OperationExpenseInterface.Create(model.operationExpenseRecord);
                }
                else
                {
                    OperationExpenseInterface.Update(model.operationExpenseRecord);
                }
            }

            return RedirectToAction("OperationExpense");
        }


        //Expense
        public ActionResult Index(ExpenseModel expenseModel)
        {
            if (string.IsNullOrEmpty(expenseModel.SelectedMonth) && string.IsNullOrEmpty(expenseModel.SelectedYear))
            {
                expenseModel.SelectedMonth = DateTime.Today.Month.ToString();
                expenseModel.SelectedYear = DateTime.Today.Year.ToString();
            }
            var conf = ExpenseInterface.ReadByMonthAndYear(Convert.ToInt32(expenseModel.SelectedMonth), Convert.ToInt32(expenseModel.SelectedYear));
            var model = new ExpenseModel(conf);
            model.SelectedMonth = expenseModel.SelectedMonth;
            model.SelectedYear = expenseModel.SelectedYear;
            return View(model);
        }

        public ActionResult AddExpense(int? id, string pageMode)
        {
            if (id != null)
            {
                var conf = ExpenseInterface.Read(id.Value);
                var model = new ExpenseModel(conf);

                model.PageMode = pageMode != null ? pageMode : string.Empty;
                return View(model);
            }
            else
            {
                return View(new ExpenseModel());
            }
        }

        [AuthorizeUser(Roles = "Admin")]
        public ActionResult Submit(ExpenseModel model, IEnumerable<HttpPostedFileBase> documents)
        {
            if (model.expenseRecord != null)
            {
                int i = 0;
                if (model.expenseRecord.Documents != null && model.expenseRecord.Documents.Count > 0)
                {
                    i = model.expenseRecord.Documents.FindIndex(j => string.IsNullOrEmpty(j.DocumentUrl));
                }

                if (documents != null)
                {
                    foreach (var file in documents)
                    {
                        if (file != null && file.ContentLength > 0)
                        {
                            var fileName = Path.GetFileName(file.FileName);
                            var path = Path.Combine(Server.MapPath("~/Content/Expense_Documents"), fileName);
                            model.expenseRecord.Documents[i].DocumentUrl = "/Content/Expense_Documents/" + fileName;
                            file.SaveAs(path);
                        }
                        i++;
                    }
                }

                if (model.expenseRecord.Id == 0)
                {
                    ExpenseInterface.Create(model.expenseRecord);
                }
                else
                {
                    ExpenseInterface.Update(model.expenseRecord);
                    if (model.expenseRecord.Documents != null)
                    {
                        var records = model.expenseRecord.Documents.FindAll(d => d.Id == 0);
                        foreach (var record in records)
                        {
                            ExpenseInterface.InsertDocument(record, model.expenseRecord);
                        }
                    }
                }
            }

            return RedirectToAction("Index");
        }

        public void RemoveDocument(int id)
        {
            if (id != 0)
                ExpenseInterface.DeleteDocument(id);
        }
    }
}
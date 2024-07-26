using System.Web.Mvc;
using Library.DualGlobe.ERP.Interfaces;
using DualGlobe.ERP.Models;
using System.Configuration;
using System.Web;
using DualGlobe.ERP.Utility;
using Library.DualGlobe.ERP.Utilities;
using System.Collections.Generic;
using System.Linq;
using System;

namespace DualGlobe.ERP.Controllers
{
    public class EmployeeController : BaseController
    {
        // GET: Employee
        public ActionResult Index()
        {
            var conf = EmployeeInterface.Read();
            conf = conf.OrderBy(x => Convert.ToInt32(x.Id)).ToArray();
            var model = new EmployeeModel(conf);
            return View(model);
        }

        public ActionResult Employee(int? employeeId, string pageMode)
        {
            if (employeeId != null)
            {
                var conf = EmployeeInterface.Read(employeeId.Value);
                var model = new EmployeeModel(conf);

                model.PageMode = pageMode != null ? pageMode : string.Empty;
                return View(model);
            }
            else
            {
                var model = new EmployeeModel();
                model.PageMode = "Edit";
                return View(model);
            }
        }

        public JsonResult GetEmployeeName(int employeeId)
        {
            if (employeeId != 0)
            {
                var conf = EmployeeInterface.Read(employeeId);
                string employeeName = string.Empty;
                if (conf != null)
                    employeeName = conf.FirstName + " " + conf.LastName;
                return Json(employeeName, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetPolicyStartEndDates(string policyNumber)
        {
            if (!string.IsNullOrEmpty(policyNumber))
            {
                var conf = InsuranceInterface.ReadByPolicyNumber(policyNumber);
                string policyStartEndDate = string.Empty;
                if (conf != null)
                    policyStartEndDate = conf.StartDate.Value.ToShortDateString() + ":" + conf.EndDate.Value.ToShortDateString();
                return Json(policyStartEndDate, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [AuthorizeUser(Roles = "Admin")]
        public ActionResult Submit(EmployeeModel employeModel, HttpPostedFileBase upload, IEnumerable<HttpPostedFileBase> documents)
        {

            if (employeModel.employeeRecord != null)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    var fileName = System.IO.Path.GetFileName(upload.FileName);
                    var path = System.IO.Path.Combine(Server.MapPath("~/Content/EmployeePhoto"), fileName);

                    employeModel.employeeRecord.PhotoImagePath = "/Content/EmployeePhoto/" + fileName;
                    upload.SaveAs(path);
                }

                if (documents != null)
                {
                    int i = 0;
                    if (employeModel.employeeRecord.EmployeeDocuments != null && employeModel.employeeRecord.EmployeeDocuments.Count > 0)
                    {
                        i = employeModel.employeeRecord.EmployeeDocuments.FindIndex(j => string.IsNullOrEmpty(j.DocumentUrl));
                    }
                    foreach (var file in documents)
                    {
                        if (file != null && file.ContentLength > 0)
                        {
                            var fileName = System.IO.Path.GetFileName(file.FileName);
                            var path = System.IO.Path.Combine(Server.MapPath("~/Content/Employee_Documents"), fileName);
                            employeModel.employeeRecord.EmployeeDocuments[i].DocumentUrl = "/Content/Employee_Documents/" + fileName;
                            file.SaveAs(path);
                        }
                        i++;
                    }
                }
                if (employeModel.employeeRecord.Id == 0)
                {
                    var allEmp = EmployeeInterface.Read();
                    int maxLimit = SecurityHelper.DecryptLicense(ConfigurationManager.AppSettings["EmployeeLicence"]);
                    if (allEmp.Length <= maxLimit)
                    {
                        EmployeeInterface.Create(employeModel.employeeRecord);
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Cannot add more than " + maxLimit + " employees";
                        return View("Employee", employeModel);
                    }
                }
                else
                {
                    EmployeeInterface.Update(employeModel.employeeRecord);
                    if (employeModel.employeeRecord.EmployeeDocuments != null)
                    {
                        var records = employeModel.employeeRecord.EmployeeDocuments.FindAll(d => d.Id == 0 && !string.IsNullOrEmpty(d.DocumentName));
                        foreach (var record in records)
                        {
                            EmployeeInterface.InsertDocument(record, employeModel.employeeRecord);
                        }
                    }
                }

            }

            return RedirectToAction("Index");
        }
        public void RemoveDocument(int id)
        {
            if (id != 0)
                EmployeeInterface.DeleteDocument(id);
        }

        public double GetEmployeeLeaveCount(int empId, DateTime startdate, DateTime enddate)
        {
            double leavedays = 0;
            var employee = EmployeeInterface.ReadByEmpId(empId);

            var appliedLeave = (enddate - startdate).TotalDays + 1;
            var counter = (enddate - startdate).TotalDays + 1;

            for (int i = 0; i < counter; i++)
            {
                if (Utilities.IsPublicHoliday(startdate.AddDays(i)) && !employee.IsPublicHolidayConsideredNormalDay.GetValueOrDefault())
                {
                    appliedLeave = appliedLeave - 1;
                }
                else if (Utilities.IsFullRestDay(employee.RestDay.GetValueOrDefault(), startdate.AddDays(i)))
                {
                    appliedLeave = appliedLeave - 1;
                }
                else if (Utilities.IsHalfRestDay(employee.RestDay.GetValueOrDefault(), startdate.AddDays(i)))
                {
                    appliedLeave = appliedLeave - 0.5;
                }
            }
            leavedays = leavedays + appliedLeave;

            return leavedays;
        }
    }
}
using System;
using System.Web.Mvc;
using DualGlobe.ERP.Models;
using DualGlobe.ERP.Utility;
using Library.DualGlobe.ERP.Interfaces;
using System.Collections.Generic;

namespace DualGlobe.ERP.Controllers
{
    public class LeaveController : BaseController
    {
        // GET: Leave
        public ActionResult Index(int? year, int? month)
        {
            if (year == null)
            {
                year = DateTime.Today.Year;
                month = DateTime.Today.Month;
            }
            var conf = LeaveInterface.ReadByYearAndMonth(year.Value, month.Value);
            var model = new LeaveModel(conf);
            model.SelectedYear = year.ToString();
            model.SelectedMonth = month.ToString();
            return View(model);
        }

        public ActionResult Leave(int? leaveId, string pageMode, int? empId, DateTime? startDate)
        {
            if (leaveId != null)
            {
                var conf = LeaveInterface.Read(leaveId.Value);
                var model = new LeaveModel(conf);

                model.PageMode = pageMode != null ? pageMode : string.Empty;
                return View(model);
            }
            else if (empId != null && startDate != null)
            {
                var leaveRecord = new Library.DualGlobe.ERP.Models.Leave();
                leaveRecord.EmployeeId = empId.Value;
                leaveRecord.StartDate = startDate;
                var model = new LeaveModel(leaveRecord);
                return View(model);
            }
            else
            {
                return View(new LeaveModel());
            }
        }

        [AuthorizeUser(Roles = "Admin")]
        public ActionResult Submit(LeaveModel model)
        {
            if (model.LeaveRecord != null)
            {
                if (model.LeaveRecord.Id == 0)
                {
                    LeaveInterface.Create(model.LeaveRecord);
                }
                else
                {
                    var leaveRecord = LeaveInterface.Read(model.LeaveRecord.Id);
                    TimesheetInterface.UpdateLeave(leaveRecord.EmployeeId, leaveRecord.StartDate.Value, leaveRecord.EndDate.Value);
                    LeaveInterface.Update(model.LeaveRecord);
                }

                //delete from timesheet if record already exixts otherwise add timesheet with leave details
                double totalLeaveDays = (model.LeaveRecord.EndDate.Value - model.LeaveRecord.StartDate.Value).TotalDays;
                List<Library.DualGlobe.ERP.Models.Timesheet> timesheetList = new List<Library.DualGlobe.ERP.Models.Timesheet>();
                for (int i = 0; i <= totalLeaveDays; i++)
                {
                    Library.DualGlobe.ERP.Models.Timesheet timesheet = new Library.DualGlobe.ERP.Models.Timesheet();
                    timesheet.TimesheetDate = model.LeaveRecord.StartDate.Value.AddDays(i);
                    timesheet.EmployeeId = model.LeaveRecord.EmployeeId;
                    timesheet.ProjectId = Utilities.GetProjectId(model.LeaveRecord.EmployeeId);
                    timesheet.IsLeave = true;
                    timesheet.IsSubmitted = true;
                    //timesheet.employee = EmployeeInterface.ReadByEmpId(model.LeaveRecord.EmployeeId);
                    timesheetList.Add(timesheet);
                }

                TimesheetInterface.BulkInsert(timesheetList);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Search(LeaveModel model)
        {
            return RedirectToAction("Index", new { year = model.SelectedYear, month = model.SelectedMonth });
        }

        public void CancelLeave(int id)
        {
            if (id != 0)
            {
                var leaveRecord = LeaveInterface.Read(id);
                TimesheetInterface.UpdateLeave(leaveRecord.EmployeeId, leaveRecord.StartDate.Value, leaveRecord.EndDate.Value);
                LeaveInterface.Delete(id);
            }
        }
    }
}
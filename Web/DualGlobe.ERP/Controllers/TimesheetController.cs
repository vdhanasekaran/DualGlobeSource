using System.Web.Mvc;
using Library.DualGlobe.ERP.Interfaces;
using libraryModel = Library.DualGlobe.ERP.Models;
using DualGlobe.ERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using DualGlobe.ERP.Utility;
using DualGlobe.ERP.Helper;

namespace DualGlobe.ERP.Controllers
{
    public class TimesheetController : BaseController
    {
        // GET: Timesheet
        public ActionResult Index(DateTime? timesheetdate)
        {
            var model = new TimesheetModel();
            if (timesheetdate == null)
            {
                model.TimesheetDate = DateTime.Today;
                model.IsPublicHoliday = Utilities.IsPublicHoliday(model.TimesheetDate);
            }

            else
            {
                model.TimesheetDate = DateTime.Today;
                model.IsPublicHoliday = Utilities.IsPublicHoliday(model.TimesheetDate);
                model.timesheetArray = TimesheetInterface.ReadByDate(timesheetdate.Value);

            }
            return View(model);
        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "Add")]
        public ActionResult Add(TimesheetModel timesheetModel)
        {
            if (timesheetModel != null)
            {
                string projectId = timesheetModel.ProjectName;
                string clientId = timesheetModel.ClientName;
                string employeeStatus = timesheetModel.EmployeeStatus;
                DateTime timesheetDate = timesheetModel.TimesheetDate;
                List<libraryModel.Employee> empList = new List<libraryModel.Employee>();
                List<libraryModel.Timesheet> timesheetList = new List<libraryModel.Timesheet>();

                //1) check whether the input date belongs to any public holidays.
                timesheetModel.IsPublicHoliday = Utilities.IsPublicHoliday(timesheetDate);

                //2) checking if timesheet already exists
                //timesheetModel.timesheetArray = TimesheetInterface.ReadByEmployeestatusProjectAndDate(timesheetModel.ProjectName, timesheetDate, employeeStatus);
                timesheetModel.timesheetArray = TimesheetInterface.ReadByDate(timesheetDate);



                //3) Select employee based on the option selected in the dropdown
                var employeeList = Utilities.GetEmployees(timesheetModel.ClientName, timesheetModel.ProjectName, employeeStatus);


                if (timesheetModel.timesheetArray.Length > 0 && employeeList.Count > 0)
                {
                    empList = employeeList.Where(x => !timesheetModel.timesheetArray.Any(y => y.EmployeeId == x.Id)).ToList();
                }
                else
                {
                    empList = employeeList;
                }

                foreach (var empRecord in empList)
                {
                    if (empRecord.LastWorkingDate == null || empRecord.LastWorkingDate.Value >= timesheetDate)
                    {
                        //4) check employee joining date is lesser than current date
                        if (empRecord.AppointmentDate <= timesheetDate)
                        {
                            //5) check whether employee applied Leave
                            libraryModel.Leave empLeave = new libraryModel.Leave();
                            empLeave = LeaveInterface.ReadByEmployeeAndDate(empRecord.Id, timesheetDate);

                            //6) If no leave record found then create timesheet record for each employee
                            if (empLeave == null)
                            {
                                libraryModel.Timesheet timesheetRecord = new libraryModel.Timesheet();
                                if (!timesheetModel.IsPublicHoliday)
                                {
                                    timesheetRecord = FillTimesheet(empRecord, timesheetDate);
                                    timesheetRecord.IsPublicHoliday = false;
                                }
                                else
                                {
                                    //If public holiday
                                    timesheetRecord = FillTimesheet(empRecord, timesheetDate);
                                    timesheetRecord.IsPublicHoliday = true;
                                }
                                timesheetList.Add(timesheetRecord);
                            }
                        }
                    }
                }
                if (timesheetList != null && timesheetList.Count > 0)
                {
                    //Insert Record into Timesheet Table
                    timesheetModel.timesheetArray = TimesheetInterface.BulkInsert(timesheetList);
                }

                TempData["TimesheetArray"] = timesheetModel.timesheetArray;

                if (!string.IsNullOrEmpty(clientId))
                {
                    timesheetModel.projectList = DropdownBuilder.GetAllClientProjects(Convert.ToInt32(clientId));
                    if (!string.IsNullOrEmpty(projectId))
                        timesheetModel.ProjectName = projectId;
                }
                return View("Index", timesheetModel);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "Search")]
        public ActionResult Search(TimesheetModel model)
        {

            if (model != null)
            {
                model.IsPublicHoliday = Utilities.IsPublicHoliday(model.TimesheetDate);
                model.timesheetArray = TimesheetInterface.ReadByEmployeestatusProjectAndDate(model.ClientName, model.ProjectName, model.TimesheetDate, model.EmployeeStatus);
                if (!string.IsNullOrEmpty(model.ClientName))
                {
                    model.projectList = DropdownBuilder.GetAllClientProjects(Convert.ToInt32(model.ClientName));
                    if (!string.IsNullOrEmpty(model.ProjectName))
                        model.ProjectName = model.ProjectName;
                }
                if (model.timesheetArray.Length > 0)
                {
                    return View("Index", model);
                }
                else
                {

                    ModelState.AddModelError("TimesheetError", "Records Does not exists for this Data, Kindly add the data by clicking Add timesheet");
                    return View("Index", model);
                }
            }
            else
            {
                return RedirectToAction("Index");
            }

        }


        public ActionResult Edit(string clientId, string projectId, DateTime? timesheetDate, string status)
        {
            var model = new TimesheetModel();
            model.TimesheetDate = timesheetDate.Value.Date;
            model.ProjectName = projectId;
            model.ClientName = clientId;
            model.EmployeeStatus = status;
            model.PageMode = "Edit";
            model.IsPublicHoliday = Utilities.IsPublicHoliday(model.TimesheetDate);

            if (TempData["TimesheetArray"] != null)
            {
                model.timesheetArray = TempData["TimesheetArray"] as libraryModel.Timesheet[];
            }
            else
            {
                model.timesheetArray = TimesheetInterface.ReadByEmployeestatusProjectAndDate(clientId, projectId, timesheetDate.Value, status);
            }

            if (!string.IsNullOrEmpty(clientId))
            {
                model.projectList = DropdownBuilder.GetAllClientProjects(Convert.ToInt32(clientId));
                if (!string.IsNullOrEmpty(projectId))
                    model.ProjectName = projectId;
            }
            return View("Index", model);
        }

        [AuthorizeUser(Roles = "Admin")]
        public ActionResult Submit(TimesheetModel timesheetModel)
        {

            if (timesheetModel.timesheetArray != null)
            {

                foreach (var timesheetRecord in timesheetModel.timesheetArray)
                {
                    
                    //TODO if leave remove record
                    if (timesheetRecord.Id == 0)
                    {
                        TimesheetInterface.Create(timesheetRecord);
                    }
                    else
                    {
                        timesheetRecord.IsSubmitted = true;
                        TimesheetInterface.Update(timesheetRecord);
                    }
                }


            }
            return View("Index", timesheetModel);
        }

        [AuthorizeUser(Roles = "Admin")]
        [HttpPost]
        [MultipleButton(Name = "action", Argument = "SubmitHoliday")]
        public ActionResult SubmitHoliday(TimesheetModel timesheetModel)
        {
            if (timesheetModel != null && timesheetModel.timesheetArray != null)
            {
                foreach (var timesheetRecord in timesheetModel.timesheetArray)
                {
                    if (timesheetModel.IsSelectAllEmployee)
                    {
                        timesheetRecord.IsPublicHoliday = true;
                    }

                    if (timesheetRecord.Id > 0)
                    {
                        if (timesheetRecord.IsPublicHoliday == true)
                        {
                            timesheetRecord.IsLeave = false; 
                            timesheetRecord.IsSubmitted = true;
                            TimesheetInterface.Update(timesheetRecord);
                        }
                        else
                        {
                            TimesheetInterface.Delete(timesheetRecord.Id);
                        }
                    }
                }
                if (!string.IsNullOrEmpty(timesheetModel.ClientName))
                {
                    timesheetModel.projectList = DropdownBuilder.GetAllClientProjects(Convert.ToInt32(timesheetModel.ClientName));
                }
                timesheetModel.timesheetArray = TimesheetInterface.ReadByEmployeestatusProjectAndDate(timesheetModel.ClientName, timesheetModel.ProjectName, timesheetModel.TimesheetDate, timesheetModel.EmployeeStatus);
                TempData["TimesheetArray"] = timesheetModel.timesheetArray;

            }
            return View("Index", timesheetModel);
        }

        [AuthorizeUser(Roles = "Admin")]
        [HttpPost]
        [MultipleButton(Name = "action", Argument = "ApplyHoliday")]
        public ActionResult ApplyHoliday(TimesheetModel timesheetModel)
        {
            if (timesheetModel != null && timesheetModel.timesheetArray != null)
            {
                foreach (var timesheetRecord in timesheetModel.timesheetArray)
                {
                    if (timesheetModel.IsSelectAllEmployee)
                    {
                        timesheetRecord.IsPublicHoliday = true;
                    }

                    if (timesheetRecord.Id > 0)
                    {
                        if (timesheetRecord.IsPublicHoliday == true)
                        {
                            timesheetRecord.IsLeave = false;
                            timesheetRecord.TimeIn = timesheetModel.HolidayInTime;
                            timesheetRecord.TimeOut = timesheetModel.HolidayOutTime;
                            timesheetRecord.TotalHours = (timesheetModel.HolidayOutTime - timesheetModel.HolidayInTime).Value.Hours - 1;
                            double empWorkingHour = Convert.ToDouble(Utilities.GetWorkingHourByEmployee(timesheetRecord.EmployeeId));
                            timesheetRecord.RegularHours = (timesheetRecord.TotalHours > empWorkingHour) ? empWorkingHour : timesheetRecord.TotalHours;
                            timesheetRecord.OTHours = (timesheetRecord.TotalHours > empWorkingHour) ? timesheetRecord.TotalHours - empWorkingHour : 0;
                            timesheetRecord.IsSubmitted = true;
                            TimesheetInterface.Update(timesheetRecord);
                        }
                        else
                        {
                            TimesheetInterface.Delete(timesheetRecord.Id);
                        }
                    }
                }
                if (!string.IsNullOrEmpty(timesheetModel.ClientName))
                {
                    timesheetModel.projectList = DropdownBuilder.GetAllClientProjects(Convert.ToInt32(timesheetModel.ClientName));
                }
                timesheetModel.timesheetArray = TimesheetInterface.ReadByEmployeestatusProjectAndDate(timesheetModel.ClientName, timesheetModel.ProjectName, timesheetModel.TimesheetDate, timesheetModel.EmployeeStatus);
                TempData["TimesheetArray"] = timesheetModel.timesheetArray;

            }
            return View("Index", timesheetModel);
        }
        

        public ActionResult ApplyLeave(int? id)
        {
            return PartialView("ApplyLeave");
        }

        private libraryModel.Timesheet FillTimesheet(libraryModel.Employee empRecord, DateTime timesheetDate)
        {

            libraryModel.Timesheet timesheetRecord = new libraryModel.Timesheet();
            libraryModel.WorkingHour workhr = new libraryModel.WorkingHour();
            workhr = WorkingHourInterface.Read(empRecord.WorkingHours.GetValueOrDefault(0));
            timesheetRecord.IsRestday = Utilities.IsFullRestDay(empRecord.RestDay.Value, timesheetDate);
            libraryModel.EmployeeProject empProj = new libraryModel.EmployeeProject();
            empProj = EmployeeProjectInterface.ReadByEmployee(empRecord.Id).FirstOrDefault();
            var project = ProjectInterface.Read(empProj.projectId);
            
            if (Utilities.IsHalfRestDay(empRecord.RestDay.Value, timesheetDate))
            {
                timesheetRecord.IsRestday = true;
                timesheetRecord.TimeIn = new TimeSpan(8, 0, 0);
                timesheetRecord.TimeOut = new TimeSpan(13, 00, 0);
                timesheetRecord.RegularHours = Convert.ToDouble(4);
            }
            else if (timesheetRecord.IsRestday)
            {
                timesheetRecord.IsLeave = true;
            }
            else
            {
                if (workhr != null)
                {
                    timesheetRecord.TimeIn = workhr.InTime;
                    timesheetRecord.TimeOut = workhr.OutTime;
                    timesheetRecord.RegularHours = Convert.ToDouble(workhr.TotalHour);
                }
                else
                {
                    timesheetRecord.TimeIn = new TimeSpan(8, 0, 0);
                    timesheetRecord.TimeOut = new TimeSpan(17, 00, 0);
                    timesheetRecord.RegularHours = Convert.ToDouble(8);

                }
                timesheetRecord.OTHours = 0;
                timesheetRecord.TotalHours = timesheetRecord.RegularHours;
                timesheetRecord.IsLeave = false;
            }

            timesheetRecord.IsSubmitted = false;
            timesheetRecord.ProjectId = empProj != null ? empProj.projectId : 0;
            timesheetRecord.ClientId = project != null ? project.ClientId : 0;
            timesheetRecord.EmployeeId = empRecord.Id;
            timesheetRecord.TimesheetDate = timesheetDate;
            return timesheetRecord;
        }

        public void RemoveTimesheet(int id)
        {
            if (id != 0)
            {
                TimesheetInterface.Delete(id);
            }
        }

    }
}

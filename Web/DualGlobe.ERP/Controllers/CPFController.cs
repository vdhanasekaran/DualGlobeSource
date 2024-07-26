using System;
using System.Web.Mvc;
using DualGlobe.ERP.Models;
using DualGlobe.ERP.Utility;
using Library.DualGlobe.ERP.Interfaces;
using System.Collections.Generic;
using libraryModel = Library.DualGlobe.ERP.Models;
using DualGlobe.ERP.Helper;
using System.Linq;

namespace DualGlobe.ERP.Controllers
{
    public class CPFController : BaseController
    {
        // GET: CPF
        public ActionResult Index(int? year, int? month)
        {
            if (year == null && month == null)
            {
                year = DateTime.Today.Year;
                month = DateTime.Today.Month;
            }
            var conf = CPFInterface.ReadByYearAndMonth(year.Value, month.Value);
            var model = new CPFModel(conf);
            model.SelectedYear = year.ToString();
            model.SelectedMonth = month.ToString();
            return View(model);
        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "Add")]
        public ActionResult Add(CPFModel model)
        {
            if (model != null)
            {
                if (!string.IsNullOrEmpty(model.SelectedYear) && !string.IsNullOrEmpty(model.SelectedMonth))
                {
                    int year = Convert.ToInt16(model.SelectedYear);
                    int month = Convert.ToInt16(model.SelectedMonth);
                    var CPFArray = CPFInterface.ReadByYearAndMonth(year, month);

                    if (CPFArray != null && CPFArray.Length > 0)
                    {
                        model.CPFArray = CPFArray;
                    }
                    else
                    {
                        List<libraryModel.Employee> empList = new List<libraryModel.Employee>();
                        List<libraryModel.CPF> CPFList = new List<libraryModel.CPF>();
                        empList = EmployeeInterface.ReadByWorkStatus();

                        foreach (var emp in empList)
                        {
                            var empTimesheet = TimesheetInterface.ReadByEmployeMonthYear(emp.Id, month, year);
                            if (empTimesheet != null && empTimesheet.Any())
                            {                                 
                                libraryModel.CPF record = new libraryModel.CPF();
                                record.EmployeeId = emp.Id;
                                record.EmployeeIC = emp.ICNumber;
                                var fixedAllowance = emp.FixedAllowance == null ? 0 : emp.FixedAllowance;
                                record.FixedDecution = emp.FixedDeduction == null ? 0 : emp.FixedDeduction;
                                record.AdditionalWages = Utilities.GetTotalAllowance(emp.Id, month, year);
                                                                
                                var totalBasicSalary = record.AdditionalWages + fixedAllowance + emp.BasicSalary;

                                decimal avgWorkingHrs = Utilities.GetAvgWorkingHour(emp.WorkingHours.Value);
                                decimal avgWorkingDays = Utilities.GetAvgWorkingDays(emp.WorkingHours.Value);
                                decimal regularWorkingHrs = Utilities.GetWorkingHour(emp.WorkingHours.Value);
                                var hourlyRate = Utilities.GetHourlyRate(emp.BasicSalary.GetValueOrDefault(0), avgWorkingHrs);//check working hours 
                                var weeklyRate = Utilities.GetWeeklyRate(emp.BasicSalary.GetValueOrDefault(0), avgWorkingDays);

                                decimal OThours = 0;
                                decimal OTAllowance = 0;
                                decimal restDayAllowance = 0;
                                decimal fullRestDayAllowance = 0;
                                decimal halfRestDayAllowance = 0;
                                decimal publicHolidayAllowance = 0;
                                decimal restDayExtraAllowance = 0;

                                int fullRestDay = 0;
                                int halfRestDay = 0;
                                decimal publicHoliday = 0;

                                double noOfWorkingDays = Utilities.GetEmployeeWorkingDays(emp.Id, emp.RestDay.GetValueOrDefault(0), emp.IsPublicHolidayConsideredNormalDay.GetValueOrDefault(), month, year);
                                decimal restDayExtraHr = 0;

                                foreach (var timesheet in empTimesheet)
                                {
                                    if (!timesheet.IsLeave || (timesheet.IsLeave && timesheet.TotalHours.GetValueOrDefault(0) > 0))
                                    {
                                        var otHr = Convert.ToDecimal(timesheet.OTHours.GetValueOrDefault(0));
                                        var totalHr = Convert.ToDecimal(timesheet.TotalHours.GetValueOrDefault(0));
                                        var regularHr = Convert.ToDecimal(timesheet.RegularHours.GetValueOrDefault(0));

                                        if (Utilities.IsFullRestDay(emp.RestDay.Value, timesheet.TimesheetDate))
                                        {
                                            if (totalHr >= regularWorkingHrs)
                                            {
                                                fullRestDay += 1;
                                            }
                                            else
                                            {
                                                halfRestDay += 1;
                                            }

                                        }
                                        else if (Utilities.IsHalfRestDay(emp.RestDay.Value, timesheet.TimesheetDate))
                                        {
                                            if (regularHr > (regularWorkingHrs / 2))
                                            {
                                                restDayExtraHr = Convert.ToDecimal((regularHr - (regularWorkingHrs / 2)));
                                                restDayExtraAllowance += Utilities.GetRestDayExtraPay(emp.RestDay.GetValueOrDefault(0), weeklyRate, restDayExtraHr, regularWorkingHrs);
                                            }
                                        }
                                        else if (Utilities.IsPublicHoliday(timesheet.TimesheetDate))
                                        {
                                            if (regularHr > (regularWorkingHrs / 2))
                                            {
                                                publicHoliday += 1;
                                            }
                                            else {
                                                publicHoliday += Convert.ToDecimal(0.5);
                                            }

                                        }

                                        OThours = OThours + otHr;
                                    }
                                }

                                OTAllowance = Utilities.GetOvertimePay(emp.OTGroup.GetValueOrDefault(0), hourlyRate, OThours);
                                //restDayExtraAllowance = Utilities.GetRestDayExtraPay(emp.RestDay.GetValueOrDefault(0), weeklyRate, restDayExtraHr, regularWorkingHrs);

                                fullRestDayAllowance = Utilities.GetRestDayPay(emp.RestDay.GetValueOrDefault(0), weeklyRate, fullRestDay, true);
                                halfRestDayAllowance = Utilities.GetRestDayPay(emp.RestDay.GetValueOrDefault(0), weeklyRate, halfRestDay);

                                //OTAllowance = (OThours * hourlyRate) * Convert.ToDecimal(1.5); //get from Over time group
                                //fullRestDayAllowance = weeklyRate * fullRestDay * 2;
                                //halfRestDayAllowance = weeklyRate * halfRestDay;
                                restDayAllowance = fullRestDayAllowance + halfRestDayAllowance + restDayExtraAllowance;
                                publicHolidayAllowance = Utilities.GetHolidayPay(emp.PublicHolidayPay.GetValueOrDefault(0), weeklyRate, publicHoliday);

                                decimal employeeWorkingDays = 0;
                                var empWorkingDays = empTimesheet.Where(i => !i.IsRestday && !i.IsPublicHoliday && !i.IsLeave).Count();
                                employeeWorkingDays = Convert.ToDecimal(empWorkingDays);

                                var restDayRecords = empTimesheet.Where(i => i.IsRestday);
                                foreach (var restdayRecord in restDayRecords)
                                {
                                    if (!restdayRecord.IsLeave)
                                    {
                                        if (Utilities.IsHalfRestDay(emp.RestDay.GetValueOrDefault(0), restdayRecord.TimesheetDate))
                                        {
                                            employeeWorkingDays = employeeWorkingDays + Convert.ToDecimal(0.5);
                                        }
                                    }
                                }

                                var paidLeaveCount = Utilities.GetPaidLeaveCount(emp.Id, emp.RestDay.Value, month, year);

                                //prorated Salary
                                if (Convert.ToDouble(employeeWorkingDays) < noOfWorkingDays)
                                {
                                    if (emp.IsPublicHolidayConsideredNormalDay.GetValueOrDefault())
                                    {
                                        var phRecords = empTimesheet.Where(i => i.IsPublicHoliday);
                                        if (phRecords != null && phRecords.Any())
                                        {
                                            employeeWorkingDays = employeeWorkingDays + phRecords.Where(i=> !i.IsLeave).Count();
                                        }
                                    }

                                    var paidLeavePay = (totalBasicSalary * Convert.ToDecimal(paidLeaveCount)) / Convert.ToDecimal(noOfWorkingDays);

                                    totalBasicSalary = (totalBasicSalary * Convert.ToDecimal(employeeWorkingDays)) / Convert.ToDecimal(noOfWorkingDays);
                                    
                                    var proratedFixedSalary = (record.AdditionalWages * Convert.ToDecimal(employeeWorkingDays)) / Convert.ToDecimal(noOfWorkingDays);
                                    record.AdditionalWages = proratedFixedSalary;

                                    if (paidLeavePay > 0)
                                    {
                                        totalBasicSalary = totalBasicSalary + paidLeavePay;
                                        record.AdditionalWages = record.AdditionalWages + paidLeavePay;
                                    }
                                    record.FixedDecution = (record.FixedDecution * Convert.ToDecimal(employeeWorkingDays + paidLeaveCount)) / Convert.ToDecimal(noOfWorkingDays);
                                }

                                record.TotalWages = (totalBasicSalary + OTAllowance + restDayAllowance + publicHolidayAllowance);
                                record.OrdinaryWages = record.TotalWages.GetValueOrDefault(0) - record.AdditionalWages.GetValueOrDefault(0);

                                var cpfData = Utilities.CalculateCPF(Utilities.CalculateAge(emp.DateOfBirth), record.TotalWages.GetValueOrDefault(0), record.OrdinaryWages.GetValueOrDefault(0), record.AdditionalWages.GetValueOrDefault(0), Utilities.GetPRYear(emp));

                                record.EmployeeCPF = Convert.ToDecimal(cpfData.EmployeeCPF);
                                record.EmployerCPF = Convert.ToDecimal(cpfData.EmployerCPF);

                                record.EmployeeCPF = (int)Math.Floor(record.EmployeeCPF.Value);
                                record.EmployerCPF = (int)Math.Ceiling(record.EmployerCPF.Value);

                                if (!string.IsNullOrEmpty(emp.CPFDonationType))
                                {
                                    record.DonationType = emp.CPFDonationType;
                                    record.CPFDonation = Utilities.GetDonation(record.TotalWages.GetValueOrDefault(0), emp.CPFDonationType);
                                }
                                DateTime last_date = new DateTime(year, month, DateTime.DaysInMonth(year, month));
                                record.Date = last_date;

                                CPFList.Add(record);
                            }
                        }
                        //foreach loop ends
                        model.CPFArray = CPFInterface.BulkInsert(CPFList);
                    }
                }
            }
            return View("Index", model);
        }

        [AuthorizeUser(Roles = "Admin")]
        public ActionResult Submit(CPFModel model)
        {        
            if (model.CPFArray != null)
            {
                foreach (var cpf in model.CPFArray)
                {
                    if (cpf.Id > 0)
                    {
                        CPFInterface.Update(cpf);
                    }
                }
            }
            return View("Index", model);
        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "Search")]
        public ActionResult Search(CPFModel model)
        {
           
            model.CPFArray = CPFInterface.ReadByYearAndMonth(Convert.ToInt16(model.SelectedYear), Convert.ToInt16(model.SelectedMonth));
            if (model.CPFArray.Length > 0)
            {
                return View("Index", model);
            }
            else
            {

                ModelState.AddModelError("CPFError", "Records Does not exists for this Data, Kindly add the data by clicking Add button");
                return View("Index", model);
            }
        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "Regenerate")]
        public ActionResult Regenerate(CPFModel model)
        {
            if (model != null)
            {
                if (!string.IsNullOrEmpty(model.SelectedYear) && !string.IsNullOrEmpty(model.SelectedMonth))
                {
                    int year = Convert.ToInt16(model.SelectedYear);
                    int month = Convert.ToInt16(model.SelectedMonth);

                    List<libraryModel.Employee> empList = new List<libraryModel.Employee>();
                    List<libraryModel.CPF> CPFList = new List<libraryModel.CPF>();
                    empList = EmployeeInterface.ReadByWorkStatus();

                    foreach (var emp in empList)
                    {
                        var empTimesheet = TimesheetInterface.ReadByEmployeMonthYear(emp.Id, month, year);
                        if (empTimesheet != null && empTimesheet.Any())
                        {
                            var record = CPFInterface.ReadByEmployeeYearAndMonth(emp.Id, year, month);
                            if (record != null)
                            {
                                record.EmployeeId = emp.Id;
                                record.EmployeeIC = emp.ICNumber;
                                var fixedAllowance = emp.FixedAllowance == null ? 0 : emp.FixedAllowance;
                                record.AdditionalWages = Utilities.GetTotalAllowance(emp.Id, month, year);
                                record.FixedDecution = emp.FixedDeduction == null ? 0 : emp.FixedDeduction;

                                var totalBasicSalary = record.AdditionalWages + fixedAllowance + emp.BasicSalary;


                                decimal avgWorkingHrs = Utilities.GetAvgWorkingHour(emp.WorkingHours.Value);
                                decimal avgWorkingDays = Utilities.GetAvgWorkingDays(emp.WorkingHours.Value);
                                decimal regularWorkingHrs = Utilities.GetWorkingHour(emp.WorkingHours.Value);
                                var hourlyRate = Utilities.GetHourlyRate(emp.BasicSalary.GetValueOrDefault(0), avgWorkingHrs);//check working hours 
                                var weeklyRate = Utilities.GetWeeklyRate(emp.BasicSalary.GetValueOrDefault(0), avgWorkingDays);

                                decimal OThours = 0;
                                decimal OTAllowance = 0;
                                decimal restDayAllowance = 0;
                                decimal fullRestDayAllowance = 0;
                                decimal halfRestDayAllowance = 0;
                                decimal publicHolidayAllowance = 0;
                                decimal restDayExtraAllowance = 0;

                                int fullRestDay = 0;
                                int halfRestDay = 0;
                                decimal publicHoliday = 0;
                                decimal restDayExtraHr = 0;

                                double noOfWorkingDays = Utilities.GetEmployeeWorkingDays(emp.Id, emp.RestDay.GetValueOrDefault(0), emp.IsPublicHolidayConsideredNormalDay.GetValueOrDefault(), month, year);


                                foreach (var timesheet in empTimesheet)
                                {
                                    if (!timesheet.IsLeave || (timesheet.IsLeave && timesheet.TotalHours.GetValueOrDefault(0) > 0))
                                    {
                                        var otHr = Convert.ToDecimal(timesheet.OTHours.GetValueOrDefault(0));
                                        var totalHr = Convert.ToDecimal(timesheet.TotalHours.GetValueOrDefault(0));
                                        var regularHr = Convert.ToDecimal(timesheet.RegularHours.GetValueOrDefault(0));

                                        if (Utilities.IsFullRestDay(emp.RestDay.Value, timesheet.TimesheetDate))
                                        {
                                            if (totalHr >= regularWorkingHrs)
                                            {
                                                fullRestDay += 1;
                                            }
                                            else
                                            {
                                                halfRestDay += 1;
                                            }

                                        }
                                        else if (Utilities.IsHalfRestDay(emp.RestDay.Value, timesheet.TimesheetDate))
                                        {
                                            if (regularHr > (regularWorkingHrs / 2))
                                            {
                                                restDayExtraHr = Convert.ToDecimal((regularHr - (regularWorkingHrs / 2)));
                                                restDayExtraAllowance += Utilities.GetRestDayExtraPay(emp.RestDay.GetValueOrDefault(0), weeklyRate, restDayExtraHr, regularWorkingHrs);
                                            }
                                        }
                                        else if (Utilities.IsPublicHoliday(timesheet.TimesheetDate))
                                        {
                                            if (regularHr > (regularWorkingHrs / 2))
                                            {
                                                publicHoliday += 1;
                                            }
                                            else {
                                                publicHoliday += Convert.ToDecimal(0.5);
                                            }
                                        }
                                        OThours = OThours + otHr;
                                    }
                                }

                                OTAllowance = Utilities.GetOvertimePay(emp.OTGroup.GetValueOrDefault(0), hourlyRate, OThours);
                                //restDayExtraAllowance = Utilities.GetRestDayExtraPay(emp.RestDay.GetValueOrDefault(0), weeklyRate, restDayExtraHr, regularWorkingHrs);

                                fullRestDayAllowance = Utilities.GetRestDayPay(emp.RestDay.GetValueOrDefault(0), weeklyRate, fullRestDay, true);
                                halfRestDayAllowance = Utilities.GetRestDayPay(emp.RestDay.GetValueOrDefault(0), weeklyRate, halfRestDay);

                                //OTAllowance = (OThours * hourlyRate) * Convert.ToDecimal(1.5); //get from Over time group
                                //fullRestDayAllowance = weeklyRate * fullRestDay * 2;
                                //halfRestDayAllowance = weeklyRate * halfRestDay;
                                restDayAllowance = fullRestDayAllowance + halfRestDayAllowance + restDayExtraAllowance;
                                publicHolidayAllowance = Utilities.GetHolidayPay(emp.PublicHolidayPay.GetValueOrDefault(0), weeklyRate, publicHoliday);
                                
                                decimal employeeWorkingDays = 0;
                                var empWorkingDays = empTimesheet.Where(i => !i.IsRestday && !i.IsPublicHoliday && !i.IsLeave).Count();
                                employeeWorkingDays = Convert.ToDecimal(empWorkingDays);

                                var restDayRecords = empTimesheet.Where(i => i.IsRestday);
                                foreach (var restdayRecord in restDayRecords)
                                {
                                    if (!restdayRecord.IsLeave)
                                    {
                                        if (Utilities.IsHalfRestDay(emp.RestDay.GetValueOrDefault(0), restdayRecord.TimesheetDate))
                                        {
                                            employeeWorkingDays = employeeWorkingDays + Convert.ToDecimal(0.5);
                                        }
                                    }
                                }

                                var paidLeaveCount = Utilities.GetPaidLeaveCount(emp.Id, emp.RestDay.Value, month, year);

                                //prorated Salary
                                if (Convert.ToDouble(employeeWorkingDays) < noOfWorkingDays)
                                {
                                    if (emp.IsPublicHolidayConsideredNormalDay.GetValueOrDefault())
                                    {
                                        var phRecords = empTimesheet.Where(i => i.IsPublicHoliday);
                                        if (phRecords != null && phRecords.Any())
                                        {
                                            employeeWorkingDays = employeeWorkingDays + phRecords.Where(i=> !i.IsLeave).Count();
                                        }
                                    }
                                    var paidLeavePay = (totalBasicSalary * Convert.ToDecimal(paidLeaveCount)) / Convert.ToDecimal(noOfWorkingDays);

                                    totalBasicSalary = (totalBasicSalary * Convert.ToDecimal(employeeWorkingDays)) / Convert.ToDecimal(noOfWorkingDays);

                                    var proratedFixedSalary = (record.AdditionalWages * Convert.ToDecimal(employeeWorkingDays)) / Convert.ToDecimal(noOfWorkingDays);
                                    record.AdditionalWages = proratedFixedSalary;

                                    if (paidLeavePay > 0)
                                    {
                                        totalBasicSalary = totalBasicSalary + paidLeavePay;
                                        record.AdditionalWages = record.AdditionalWages + paidLeavePay;
                                    }
                                    record.FixedDecution = (record.FixedDecution * Convert.ToDecimal(employeeWorkingDays + paidLeaveCount)) / Convert.ToDecimal(noOfWorkingDays);
                                }

                                record.TotalWages = (totalBasicSalary + OTAllowance + restDayAllowance + publicHolidayAllowance);
                                record.OrdinaryWages = record.TotalWages.GetValueOrDefault(0) - record.AdditionalWages.GetValueOrDefault(0);

                                var cpfData = Utilities.CalculateCPF(Utilities.CalculateAge(emp.DateOfBirth), record.TotalWages.GetValueOrDefault(0), record.OrdinaryWages.GetValueOrDefault(0), record.AdditionalWages.GetValueOrDefault(0), Utilities.GetPRYear(emp));

                                record.EmployeeCPF = Convert.ToDecimal(cpfData.EmployeeCPF);
                                record.EmployerCPF = Convert.ToDecimal(cpfData.EmployerCPF);

                                record.EmployeeCPF = (int)Math.Floor(record.EmployeeCPF.Value);
                                record.EmployerCPF = (int)Math.Ceiling(record.EmployerCPF.Value);

                                if (!string.IsNullOrEmpty(emp.CPFDonationType))
                                {
                                    record.DonationType = emp.CPFDonationType;
                                    record.CPFDonation = Utilities.GetDonation(record.TotalWages.GetValueOrDefault(0), emp.CPFDonationType);
                                }
                                DateTime last_date = new DateTime(year, month, DateTime.DaysInMonth(year, month));
                                record.Date = last_date;
                                CPFList.Add(record);
                                CPFInterface.Update(record);
                            }
                        }
                    }
                    model.CPFArray = CPFList.ToArray();
                }
            }
            return View("Index", model);
        }

        public ActionResult Edit(string month, string year)
        {
            var model = new CPFModel();
            model.PageMode = "Edit";
            model.SelectedMonth = month;
            model.SelectedYear = year;

            if (TempData["CPFArray"] != null)
            {
                model.CPFArray = TempData["CPFArray"] as libraryModel.CPF[];
            }
            else
            {
                model.CPFArray = CPFInterface.ReadByYearAndMonth(Convert.ToInt16(year), Convert.ToInt16(month));
            }

            return View("Index", model);
        }
    }
}
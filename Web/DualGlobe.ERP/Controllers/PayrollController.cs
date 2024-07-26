using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Linq;
using DualGlobe.ERP.Models;
using DualGlobe.ERP.Utility;
using Library.DualGlobe.ERP.Interfaces;
using libraryModel = Library.DualGlobe.ERP.Models;
using System.Configuration;

namespace DualGlobe.ERP.Controllers
{
    public class PayrollController : BaseController
    {
        // GET: Payroll
        [AuthorizeUser(Roles = "Admin")]
        public ActionResult Index(EmployeeModel empModel, string month, string year)
        {
            if (string.IsNullOrEmpty(empModel.SelectedMonth) && string.IsNullOrEmpty(empModel.SelectedYear))
            {
                if (!string.IsNullOrEmpty(month) && !string.IsNullOrEmpty(year))
                {
                    empModel.SelectedMonth = month.ToString();
                    empModel.SelectedYear = year.ToString();
                }
                else {
                    empModel.SelectedMonth = DateTime.Today.Month.ToString();
                    empModel.SelectedYear = DateTime.Today.Year.ToString();
                }
            }
            var conf = EmployeeInterface.ReadAll();
            var empProjectList = EmployeeProjectInterface.Read();
            var empList = conf.Where(i => empProjectList.Any(j => j.EmployeeId == i.Id)).OrderBy(x => Convert.ToInt32(x.Id)).ToArray();
            var model = new EmployeeModel(empList);
            model.SelectedMonth = empModel.SelectedMonth;
            model.SelectedYear = empModel.SelectedYear;
            return View(model);
        }

        public ActionResult PaySlip(int empId, string month, string year)
        {
            var model = SalaryDetailInterface.ReadByEmployeeIdAndDate(empId, month, year);
            return View("PrintPaySlip", model);
        }

        public ActionResult ViewPaySlip(int empId, string month, string year)
        {
            var model = SalaryDetailInterface.ReadByEmployeeIdAndDate(empId, month, year);
            return PartialView("ViewPaySlip", model);
        }

        [HttpPost]
        [AuthorizeUser(Roles = "Admin")]
        public JsonResult GenerateSalary(int[] employeeList, string month, string year)
        {
            if (employeeList != null && employeeList.Length > 0)
            {
                var monthVal = Convert.ToInt16(month);
                var yearVal = Convert.ToInt16(year);
                List<string> errorList = new List<string>();
                List<libraryModel.SalaryDetail> salaryList = new List<libraryModel.SalaryDetail>();
                foreach (var empId in employeeList)
                {
                    if (empId > 0)
                    {

                        libraryModel.SalaryDetail salary = new libraryModel.SalaryDetail();
                        var empTimesheet = TimesheetInterface.ReadByEmployeMonthYear(empId, monthVal, yearVal);

                        //Generate salary only when the timesheet exists for employee                   
                        if (empTimesheet != null && empTimesheet.Any())
                        {
                            salary = GetSalaryDetail(empId, monthVal, yearVal, empTimesheet);
                            if (salary != null)
                            {
                                salary.SalaryMonth = month;
                                salary.SalaryYear = year;
                                salary.DateCreated = DateTime.Now;
                                salary.IsSalaryGenerated = true;
                                salary.Status = "UnPaid";
                                salaryList.Add(salary);
                            }
                        }

                    }
                }// for each ends
                SalaryDetailInterface.BulkInsert(salaryList);
            }
            var response = new { Month = month, Year = year, IsSuccess = true };
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AuthorizeUser(Roles = "Admin")]
        public JsonResult ReGenerateSalary(int[] employeeList, string month, string year)
        {
            if (employeeList != null && employeeList.Length > 0)
            {
                var monthVal = Convert.ToInt16(month);
                var yearVal = Convert.ToInt16(year);
                List<string> errorList = new List<string>();


                foreach (var empId in employeeList)
                {
                    if (empId > 0)
                    {
                        var empTimesheet = TimesheetInterface.ReadByEmployeMonthYear(empId, monthVal, yearVal);
                        if (empTimesheet != null && empTimesheet.Any())
                        {
                            var salary = SalaryDetailInterface.ReadByEmployeeIdAndDate(empId, month, year);

                            if (salary != null)
                            {
                                var empRecord = EmployeeInterface.ReadByEmpId(empId);
                                salary.EmployeeId = empRecord.Id;
                                salary.EmployeeName = empRecord.FirstName + " " + empRecord.LastName;
                                salary.WorkStatus = empRecord.WorkStatus;
                                salary.Skill = empRecord.Skill;
                                salary.IsMYE = empRecord.MYE.GetValueOrDefault();
                                salary.BasicSalary = empRecord.BasicSalary.Value;
                                salary.FixedAllowance = empRecord.FixedAllowance == null ? 0 : empRecord.FixedAllowance;
                                salary.FixedDeduction = empRecord.FixedDeduction == null ? 0 : empRecord.FixedDeduction;
                                salary.DeductedBasicSalary = 0;
                                salary.DeductedBonusAllowance = 0;
                                salary.DeductedIncentiveAllowance = 0;
                                salary.DeductedFixedAllowance = 0;

                                decimal avgWorkingHrs = Utilities.GetAvgWorkingHour(empRecord.WorkingHours.Value);
                                decimal avgWorkingDays = Utilities.GetAvgWorkingDays(empRecord.WorkingHours.Value);
                                decimal regularWorkingHrs = Utilities.GetWorkingHour(empRecord.WorkingHours.Value);
                                var hourlyRate = Utilities.GetHourlyRate(salary.BasicSalary, avgWorkingHrs);//check working hours 
                                var weeklyRate = Utilities.GetWeeklyRate(salary.BasicSalary, avgWorkingDays);//check working hours 

                                //Additions - Allowance, Advance, OT Allowance
                                //Allowance
                                var allowanceDictionary = Utilities.GetAllowance(empId, monthVal, yearVal);

                                salary.BonusAllowance = allowanceDictionary["Bonus"];
                                salary.FoodAllowance = allowanceDictionary["Food"];
                                salary.TravelAllowance = allowanceDictionary["Travel"];
                                salary.RoomRentalAllowance = allowanceDictionary["Rental"];
                                salary.OtherAllowance = allowanceDictionary["Others"];
                                salary.IncentiveAllowance = allowanceDictionary["Incentive"];

                                //OT Allowance
                                double noOfWorkingDays = Utilities.GetEmployeeWorkingDays(empId, empRecord.RestDay.GetValueOrDefault(0), empRecord.IsPublicHolidayConsideredNormalDay.GetValueOrDefault(), monthVal, yearVal);

                                //salary.PresentDays = empTimesheet.Count(x => x.IsLeave == false);
                                salary.LeaveDays = empTimesheet.Count(x => x.IsLeave == true);

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

                                foreach (var emp in empTimesheet)
                                {
                                    if (!emp.IsLeave || (emp.IsLeave && emp.TotalHours.GetValueOrDefault(0) > 0))
                                    {
                                        var otHr = Convert.ToDecimal(emp.OTHours.GetValueOrDefault(0));
                                        var totalHr = Convert.ToDecimal(emp.TotalHours.GetValueOrDefault(0));
                                        var regularHr = Convert.ToDecimal(emp.RegularHours.GetValueOrDefault(0));

                                        if (Utilities.IsFullRestDay(empRecord.RestDay.Value, emp.TimesheetDate))
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
                                        else if (Utilities.IsHalfRestDay(empRecord.RestDay.Value, emp.TimesheetDate))
                                        {
                                            if (regularHr > (regularWorkingHrs / 2))
                                            {
                                                restDayExtraHr = Convert.ToDecimal((regularHr - (regularWorkingHrs / 2)));
                                                restDayExtraAllowance += Utilities.GetRestDayExtraPay(empRecord.RestDay.GetValueOrDefault(0), weeklyRate, restDayExtraHr, regularWorkingHrs);
                                            }
                                        }
                                        else if (Utilities.IsPublicHoliday(emp.TimesheetDate))
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

                                OTAllowance = Utilities.GetOvertimePay(empRecord.OTGroup.GetValueOrDefault(0), hourlyRate, OThours);
                                //restDayExtraAllowance = Utilities.GetRestDayExtraPay(empRecord.RestDay.GetValueOrDefault(0), weeklyRate, restDayExtraHr, regularWorkingHrs);

                                fullRestDayAllowance = Utilities.GetRestDayPay(empRecord.RestDay.GetValueOrDefault(0), weeklyRate, fullRestDay, true);
                                halfRestDayAllowance = Utilities.GetRestDayPay(empRecord.RestDay.GetValueOrDefault(0), weeklyRate, halfRestDay);

                                //OTAllowance = (OThours * hourlyRate) * Convert.ToDecimal(1.5); //get from Over time group
                                //fullRestDayAllowance = weeklyRate * fullRestDay * 2;
                                //halfRestDayAllowance = weeklyRate * halfRestDay;
                                restDayAllowance = fullRestDayAllowance + halfRestDayAllowance + restDayExtraAllowance;
                                publicHolidayAllowance = Utilities.GetHolidayPay(empRecord.PublicHolidayPay.GetValueOrDefault(0), weeklyRate, publicHoliday);

                                salary.OTHourAllowance = OTAllowance;
                                salary.RestDayAllowance = restDayAllowance;
                                salary.PublicHolidayAllowance = publicHolidayAllowance;

                                //Unpaid Leaves - Detect from Salary                        
                                salary.UnPaidLeaveCount = Utilities.GetUnPaidLeaveCount(empRecord.Id, monthVal, yearVal);

                                //Loan or Advance - Credit
                                var loanPaymentAmount = Utilities.GetLoanAmount(empId, monthVal, yearVal);

                                //Detections - Loan, CPF, Donation
                                //Loan - Debit
                                var detectLoanamount = Utilities.DetectLoanAmount(empId, monthVal, yearVal);
                                salary.loanAmountDeposited = loanPaymentAmount;
                                salary.loanAmountDetected = detectLoanamount;

                                var totalBasicSalary = salary.BasicSalary + salary.FixedAllowance + allowanceDictionary["Total"];
                                
                                double employeeWorkingDays = 0;
                                var empWorkingDays = empTimesheet.Where(i => !i.IsRestday && !i.IsPublicHoliday && !i.IsLeave).Count();
                                employeeWorkingDays = Convert.ToDouble(empWorkingDays);

                                var restDayRecords = empTimesheet.Where(i => i.IsRestday);
                                int fullRestDayWorkCount = 0;
                                foreach (var restdayRecord in restDayRecords)
                                {
                                    if (!restdayRecord.IsLeave)
                                    {
                                        if (Utilities.IsHalfRestDay(empRecord.RestDay.GetValueOrDefault(0), restdayRecord.TimesheetDate))
                                        {
                                            employeeWorkingDays = employeeWorkingDays + 0.5;
                                        }
                                        if (Utilities.IsFullRestDay(empRecord.RestDay.GetValueOrDefault(0), restdayRecord.TimesheetDate))
                                        {
                                            fullRestDayWorkCount = fullRestDayWorkCount + 1;
                                        }
                                    }
                                }

                                //Levy
                                salary.Levy = 0;
                                if (empRecord.WorkStatus == "SPass" || empRecord.WorkStatus == "WorkPermit")
                                {
                                    salary.Levy = LevyLookupInterface.GetLevy(empRecord, new DateTime(yearVal,monthVal,1));
                                }
                                var paidLeaveCount = Utilities.GetPaidLeaveCount(empId, empRecord.RestDay.Value, monthVal, yearVal);

                                //prorated Salary
                                if (employeeWorkingDays < noOfWorkingDays)
                                {
                                    employeeWorkingDays = employeeWorkingDays + Convert.ToDouble(paidLeaveCount);
                                    if (empRecord.IsPublicHolidayConsideredNormalDay.GetValueOrDefault())
                                    {
                                        var phRecords = empTimesheet.Where(i => i.IsPublicHoliday);
                                        if (phRecords != null && phRecords.Any())
                                        {
                                            employeeWorkingDays = employeeWorkingDays + phRecords.Where(i=> !i.IsLeave).Count();
                                        }
                                    }

                                    totalBasicSalary = (totalBasicSalary * Convert.ToDecimal(employeeWorkingDays)) / Convert.ToDecimal(noOfWorkingDays);
                                    var proratedBasicSalary = (salary.BasicSalary * Convert.ToDecimal(employeeWorkingDays)) / Convert.ToDecimal(noOfWorkingDays);
                                    salary.DeductedBasicSalary = salary.BasicSalary - proratedBasicSalary;
                                    salary.BasicSalary = proratedBasicSalary;
                                    //var proratedFixedSalary = (salary.FixedAllowance * Convert.ToDecimal(employeeWorkingDays)) / Convert.ToDecimal(noOfWorkingDays);
                                    //salary.DeductedFixedAllowance = salary.FixedAllowance - proratedFixedSalary;
                                    //salary.FixedAllowance = proratedFixedSalary;
                                    //var proratedBonus = (salary.BonusAllowance.GetValueOrDefault(0) * Convert.ToDecimal(employeeWorkingDays)) / Convert.ToDecimal(noOfWorkingDays);
                                    //salary.DeductedBonusAllowance = salary.BonusAllowance - proratedBonus;
                                    //salary.BonusAllowance = proratedBonus;

                                    //var proratedIncentive = (salary.IncentiveAllowance.GetValueOrDefault(0) * Convert.ToDecimal(employeeWorkingDays)) / Convert.ToDecimal(noOfWorkingDays);
                                    //salary.DeductedIncentiveAllowance = salary.IncentiveAllowance - proratedIncentive;
                                    //salary.IncentiveAllowance = proratedIncentive;

                                    //salary.Levy = (salary.Levy * Convert.ToDecimal(employeeWorkingDays)) / Convert.ToDecimal(noOfWorkingDays);
                                    salary.FixedDeduction = (salary.FixedDeduction.GetValueOrDefault(0) * Convert.ToDecimal(employeeWorkingDays)) / Convert.ToDecimal(noOfWorkingDays);
                                }

                                if (!empRecord.IsPublicHolidayConsideredNormalDay.GetValueOrDefault())
                                {
                                    var publichHolidayRecords = empTimesheet.Where(i => i.IsPublicHoliday);
                                    if (publichHolidayRecords != null && publichHolidayRecords.Any())
                                    {
                                        employeeWorkingDays = employeeWorkingDays + publichHolidayRecords.Where(i => !i.IsLeave).Count();
                                    }
                                }

                                salary.PresentDays = Convert.ToDecimal(employeeWorkingDays) + fullRestDayWorkCount;


                                var totalSalary = totalBasicSalary + salary.OTHourAllowance + salary.RestDayAllowance + salary.PublicHolidayAllowance;

                                salary.SDL = 0;
                                salary.SDL = Utilities.GetSDL(totalSalary.GetValueOrDefault(0));
                                //CPF

                                salary.EmployeeCPF = 0;
                                salary.EmployerCPF = 0;
                                salary.TotalCPF = 0;
                                salary.Donation = 0;

                                var cpfData = CPFInterface.ReadByEmployeeYearAndMonth(empRecord.Id, Convert.ToInt32(year), Convert.ToInt32(month));
                                if (cpfData != null)
                                {

                                    salary.EmployeeCPF = cpfData.EmployeeCPF;
                                    salary.EmployerCPF = cpfData.EmployerCPF;
                                    salary.TotalCPF = cpfData.TotalCPF;
                                    salary.Donation = cpfData.CPFDonation;
                                }
                                //unpaid calculation 
                                //if (salary.UnPaidLeaveCount != null && salary.UnPaidLeaveCount > 0)
                                //{
                                //    salary.UnPaidLeaveSalaryDetect = salary.UnPaidLeaveCount * weeklyRate;
                                //}

                                salary.TotalAdditions = totalSalary + loanPaymentAmount;
                                salary.TotalDetectSalary = salary.loanAmountDetected + salary.EmployeeCPF + salary.FixedDeduction;

                                if (salary.Donation != null && salary.Donation.Value > 0)
                                {
                                    salary.TotalDetectSalary += salary.Donation.GetValueOrDefault(0);
                                }
                                if (salary.UnPaidLeaveSalaryDetect != null && salary.UnPaidLeaveSalaryDetect.Value > 0)
                                {
                                    salary.TotalDetectSalary += salary.UnPaidLeaveSalaryDetect.GetValueOrDefault(0);
                                }
                                salary.GrossSalary = salary.TotalAdditions.GetValueOrDefault(0) - salary.TotalDetectSalary.GetValueOrDefault(0);

                                salary.SalaryMonth = month;
                                salary.SalaryYear = year;
                                salary.DateCreated = DateTime.Now;
                                salary.IsSalaryGenerated = true;
                                salary.Status = "UnPaid";
                                SalaryDetailInterface.Update(salary);
                                try
                                {
                                    //SaveAsPdf(salary);
                                }
                                catch
                                {
                                    //do nothing - Loop for next data.
                                }
                            }
                        }
                    }
                }
            }
            var response = new { Month = month, Year = year, IsSuccess = true };
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// ViewSalaryReport
        /// </summary>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public ActionResult ViewSalary(string month, string year)
        {
            List<libraryModel.SalaryDetail> salaryDetails = new List<libraryModel.SalaryDetail>();
            if (!string.IsNullOrEmpty(month) && !string.IsNullOrEmpty(year))
            {
                var conf = SalaryDetailInterface.ReadByMonthYear(month, year);
                salaryDetails = conf.ToList();
            }
            return View(salaryDetails);
        }

        /// <summary>
        /// ViewLevyReport
        /// </summary>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public ActionResult ViewLevy(string month, string year)
        {
            List<libraryModel.SalaryDetail> salaryDetails = new List<libraryModel.SalaryDetail>();
            if (!string.IsNullOrEmpty(month) && !string.IsNullOrEmpty(year))
            {
                var conf = SalaryDetailInterface.ReadByMonthYear(month, year);
                salaryDetails = conf.Where(i=> i.WorkStatus == "SPass" || i.WorkStatus == "WorkPermit").ToList();
            }
            return View(salaryDetails);
        }

        public JsonResult UpdateSalary(string month, string year, string status, int id)
        {
            var salary = SalaryDetailInterface.ReadById(id);
            salary.Status = status;
            salary.DateCreated = DateTime.Now;
            SalaryDetailInterface.Update(salary);

            var response = new { Month = month, Year = year, IsSuccess = true };
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        private libraryModel.SalaryDetail GetSalaryDetail(int empId, int month, int year, libraryModel.Timesheet[] empTimesheet)
        {
            libraryModel.SalaryDetail salary = new libraryModel.SalaryDetail();

            decimal OThours = 0;
            decimal OTAllowance = 0;
            decimal restDayAllowance = 0;
            decimal fullRestDayAllowance = 0;
            decimal halfRestDayAllowance = 0;
            decimal restDayExtraAllowance = 0;
            decimal publicHolidayAllowance = 0;
            int fullRestDay = 0;
            int halfRestDay = 0;
            decimal publicHoliday = 0;
            double noOfWorkingDays = 0;

            var empRecord = EmployeeInterface.ReadByEmpId(empId);

            salary.EmployeeId = empRecord.Id;
            salary.EmployeeName = empRecord.FirstName + " " + empRecord.LastName;
            salary.WorkStatus = empRecord.WorkStatus;
            salary.Skill = empRecord.Skill;
            salary.IsMYE = empRecord.MYE.GetValueOrDefault();
            salary.BasicSalary = empRecord.BasicSalary.Value;
            salary.FixedAllowance = empRecord.FixedAllowance == null ? 0 : empRecord.FixedAllowance;
            salary.FixedDeduction = empRecord.FixedDeduction == null ? 0 : empRecord.FixedDeduction;
            //salary.PresentDays = empTimesheet.Count(x => x.IsLeave == false);
            salary.LeaveDays = empTimesheet.Count(x => x.IsLeave == true);

            decimal avgWorkingHrs = Utilities.GetAvgWorkingHour(empRecord.WorkingHours.Value);
            decimal avgWorkingDays = Utilities.GetAvgWorkingDays(empRecord.WorkingHours.Value);
            decimal regularWorkingHrs = Utilities.GetWorkingHour(empRecord.WorkingHours.Value);
            var hourlyRate = Utilities.GetHourlyRate(salary.BasicSalary, avgWorkingHrs);//check working hours 
            var weeklyRate = Utilities.GetWeeklyRate(salary.BasicSalary, avgWorkingDays);//check working hours 
            noOfWorkingDays = Utilities.GetEmployeeWorkingDays(empId, empRecord.RestDay.GetValueOrDefault(0), empRecord.IsPublicHolidayConsideredNormalDay.GetValueOrDefault(), month, year);

            //Additions - Allowance, Advance, OT Allowance
            //Allowance
            var allowanceDictionary = Utilities.GetAllowance(empId, month, year);
            salary.BonusAllowance = allowanceDictionary["Bonus"];
            salary.FoodAllowance = allowanceDictionary["Food"];
            salary.TravelAllowance = allowanceDictionary["Travel"];
            salary.RoomRentalAllowance = allowanceDictionary["Rental"];
            salary.OtherAllowance = allowanceDictionary["Others"];
            salary.IncentiveAllowance = allowanceDictionary["Incentive"];

            decimal restDayExtraHr = 0;
            //OT Allowance
            foreach (var emp in empTimesheet)
            {
                if (!emp.IsLeave || (emp.IsLeave && emp.TotalHours.GetValueOrDefault(0) > 0))
                {
                    var otHr = Convert.ToDecimal(emp.OTHours.GetValueOrDefault(0));
                    var totalHr = Convert.ToDecimal(emp.TotalHours.GetValueOrDefault(0));
                    var regularHr = Convert.ToDecimal(emp.RegularHours.GetValueOrDefault(0));

                    if (Utilities.IsFullRestDay(empRecord.RestDay.Value, emp.TimesheetDate))
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
                    else if (Utilities.IsHalfRestDay(empRecord.RestDay.Value, emp.TimesheetDate))
                    {
                        if (regularHr > (regularWorkingHrs / 2))
                        {
                            restDayExtraHr = Convert.ToDecimal((regularHr - (regularWorkingHrs / 2)));
                            restDayExtraAllowance += Utilities.GetRestDayExtraPay(empRecord.RestDay.GetValueOrDefault(0), weeklyRate, restDayExtraHr, regularWorkingHrs);
                        }
                    }
                    else if (Utilities.IsPublicHoliday(emp.TimesheetDate))
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


            // OTAllowance =  hourlyRate * OThours * Convert.ToDecimal(1.5); //get from Over time group
            OTAllowance = Utilities.GetOvertimePay(empRecord.OTGroup.GetValueOrDefault(0), hourlyRate, OThours);
            //restDayExtraAllowance = Utilities.GetRestDayExtraPay(empRecord.RestDay.GetValueOrDefault(0), weeklyRate, restDayExtraHr, regularWorkingHrs);
            fullRestDayAllowance = Utilities.GetRestDayPay(empRecord.RestDay.GetValueOrDefault(0), weeklyRate, fullRestDay, true);
            halfRestDayAllowance = Utilities.GetRestDayPay(empRecord.RestDay.GetValueOrDefault(0), weeklyRate, halfRestDay);

            restDayAllowance = fullRestDayAllowance + halfRestDayAllowance + restDayExtraAllowance;
            publicHolidayAllowance = Utilities.GetHolidayPay(empRecord.PublicHolidayPay.GetValueOrDefault(0), weeklyRate, publicHoliday); 

            salary.OTHourAllowance = OTAllowance;
            salary.RestDayAllowance = restDayAllowance;
            salary.PublicHolidayAllowance = publicHolidayAllowance;

            //Unpaid Leaves - Detect from Salary                        
            salary.UnPaidLeaveCount = Utilities.GetUnPaidLeaveCount(empRecord.Id, month, year);

            //Loan or Advance - Credit
            var loanPaymentAmount = Utilities.GetLoanAmount(empId, month, year);

            //Detections - Loan, CPF, Donation
            //Loan - Debit
            var detectLoanamount = Utilities.DetectLoanAmount(empId, month, year);
            salary.loanAmountDeposited = loanPaymentAmount;
            salary.loanAmountDetected = detectLoanamount;

            var totalBasicSalary = salary.BasicSalary + salary.FixedAllowance + allowanceDictionary["Total"];

            double employeeWorkingDays = 0;
            var empWorkingDays = empTimesheet.Where(i => !i.IsRestday && !i.IsPublicHoliday && !i.IsLeave).Count();
            employeeWorkingDays = Convert.ToDouble(empWorkingDays);

            var restDayRecords = empTimesheet.Where(i => i.IsRestday);
            int fullRestDayWorkCount = 0;
            foreach (var restdayRecord in restDayRecords)
            {
                if (!restdayRecord.IsLeave)
                {
                    if (Utilities.IsHalfRestDay(empRecord.RestDay.GetValueOrDefault(0), restdayRecord.TimesheetDate))
                    {
                        employeeWorkingDays = employeeWorkingDays + 0.5;
                    }
                    if (Utilities.IsFullRestDay(empRecord.RestDay.GetValueOrDefault(0), restdayRecord.TimesheetDate))
                    {
                        fullRestDayWorkCount = fullRestDayWorkCount + 1;
                    }
                }
            }
            //Levy
            salary.Levy = 0;
            if (empRecord.WorkStatus == "SPass" || empRecord.WorkStatus == "WorkPermit")
            {
                salary.Levy = LevyLookupInterface.GetLevy(empRecord, new DateTime(year, month, 1));
            }

            var paidLeaveCount = Utilities.GetPaidLeaveCount(empRecord.Id, empRecord.RestDay.Value, month, year);

            //prorated Salary
            if (employeeWorkingDays < noOfWorkingDays)
            {
                employeeWorkingDays = employeeWorkingDays + Convert.ToDouble(paidLeaveCount);
                if (empRecord.IsPublicHolidayConsideredNormalDay.GetValueOrDefault())
                {
                    var phRecords = empTimesheet.Where(i => i.IsPublicHoliday);
                    if (phRecords != null && phRecords.Any())
                    {
                        employeeWorkingDays = employeeWorkingDays + phRecords.Where(i => !i.IsLeave).Count();
                    }
                }
                totalBasicSalary = (totalBasicSalary * Convert.ToDecimal(employeeWorkingDays)) / Convert.ToDecimal(noOfWorkingDays);
                var proratedBasicSalary = (salary.BasicSalary * Convert.ToDecimal(employeeWorkingDays)) / Convert.ToDecimal(noOfWorkingDays);
                salary.DeductedBasicSalary = salary.BasicSalary - proratedBasicSalary;
                salary.BasicSalary = proratedBasicSalary;
                //var proratedFixedSalary = (salary.FixedAllowance * Convert.ToDecimal(employeeWorkingDays)) / Convert.ToDecimal(noOfWorkingDays);
                //salary.DeductedFixedAllowance = salary.FixedAllowance - proratedFixedSalary;
                //salary.FixedAllowance = proratedFixedSalary;
                //var proratedBonus = (salary.BonusAllowance.GetValueOrDefault(0) * Convert.ToDecimal(employeeWorkingDays)) / Convert.ToDecimal(noOfWorkingDays);
                //salary.DeductedBonusAllowance = salary.BonusAllowance - proratedBonus;
                //salary.BonusAllowance = proratedBonus;

                //var proratedIncentive = (salary.IncentiveAllowance.GetValueOrDefault(0) * Convert.ToDecimal(employeeWorkingDays)) / Convert.ToDecimal(noOfWorkingDays);
                //salary.DeductedIncentiveAllowance = salary.IncentiveAllowance - proratedIncentive;
                //salary.IncentiveAllowance = proratedIncentive;

                //salary.Levy = (salary.Levy * Convert.ToDecimal(employeeWorkingDays)) / Convert.ToDecimal(noOfWorkingDays);
                salary.FixedDeduction = (salary.FixedDeduction.GetValueOrDefault(0) * Convert.ToDecimal(employeeWorkingDays)) / Convert.ToDecimal(noOfWorkingDays);
            }

            if (!empRecord.IsPublicHolidayConsideredNormalDay.GetValueOrDefault())
            {
                var publichHolidayRecords = empTimesheet.Where(i => i.IsPublicHoliday);
                if (publichHolidayRecords != null && publichHolidayRecords.Any())
                {
                    employeeWorkingDays = employeeWorkingDays + publichHolidayRecords.Where(i => !i.IsLeave).Count();
                }
            }

            salary.PresentDays = Convert.ToDecimal(employeeWorkingDays) + fullRestDayWorkCount;

            var totalSalary = totalBasicSalary + salary.OTHourAllowance + salary.RestDayAllowance + salary.PublicHolidayAllowance;

            salary.SDL = 0;
            salary.SDL = Utilities.GetSDL(totalSalary.GetValueOrDefault(0));
            
            //CPF

            salary.EmployeeCPF = 0;
            salary.EmployerCPF = 0;
            salary.TotalCPF = 0;
            salary.Donation = 0;

            var cpfData = CPFInterface.ReadByEmployeeYearAndMonth(empRecord.Id, Convert.ToInt32(year), Convert.ToInt32(month));
            if (cpfData != null)
            {

                salary.EmployeeCPF = cpfData.EmployeeCPF;
                salary.EmployerCPF = cpfData.EmployerCPF;
                salary.TotalCPF = cpfData.TotalCPF;
                salary.Donation = cpfData.CPFDonation;
            }
            
            //unpaid calculation 
            //if (salary.UnPaidLeaveCount != null && salary.UnPaidLeaveCount > 0)
            //{
            //    salary.UnPaidLeaveSalaryDetect = salary.UnPaidLeaveCount * weeklyRate;
            //}

            salary.TotalAdditions = totalSalary + loanPaymentAmount;
            salary.TotalDetectSalary = salary.loanAmountDetected + salary.EmployeeCPF + salary.FixedDeduction;

            if (salary.Donation != null && salary.Donation.Value > 0)
            {
                salary.TotalDetectSalary += salary.Donation.GetValueOrDefault(0);
            }
            if (salary.UnPaidLeaveSalaryDetect != null && salary.UnPaidLeaveSalaryDetect.Value > 0)
            {
                salary.TotalDetectSalary += salary.UnPaidLeaveSalaryDetect.GetValueOrDefault(0);
            }
            salary.GrossSalary = salary.TotalAdditions.GetValueOrDefault(0) - salary.TotalDetectSalary.GetValueOrDefault(0);

            return salary;
        }

    }
}
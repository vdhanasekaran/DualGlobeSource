using System.Text;
using Library.DualGlobe.ERP.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System;
using Library.DualGlobe.ERP.Models;
using System.Data;
using DualGlobe.ERP.Models;

namespace DualGlobe.ERP.Utility
{
    public static class Utilities
    {
        public static int GetProjectByQuotationId(int id)
        {
            int projectId = 0;
            if (id != 0)
            {
                var conf = QuotationInterface.Read(id);

                if (conf != null)
                    projectId = conf.ProjectId;
            }
            return projectId;
        }

        public static string GetSubjectByQuotationId(int id)
        {
            string subject = string.Empty;
            if (id != 0)
            {
                var conf = QuotationInterface.Read(id);

                if (conf != null)
                    subject = conf.Subject;
            }
            return subject;
        }

        public static string GetNotesByQuotationId(int id)
        {
            string notes = string.Empty;
            if (id != 0)
            {
                var conf = QuotationInterface.Read(id);
                if (conf != null && !string.IsNullOrEmpty(conf.Notes))
                {
                    notes = conf.Notes.Replace("<br />", "\r\n");
                }
            }
            return notes;
        }

        public static string GetRefNumberByQuotationId(int id)
        {
            string refNumber = string.Empty;
            if (id != 0)
            {
                var conf = QuotationInterface.Read(id);

                if (conf != null)
                    refNumber = conf.ReferenceNumber;
            }
            return refNumber;
        }

        public static string GetPONumberByQuotationId(int id)
        {
            string poNumber = string.Empty;
            if (id != 0)
            {
                var conf = QuotationInterface.Read(id);

                if (conf != null)
                    poNumber = conf.PONumber;
            }
            return poNumber;
        }        

        public static string GetClientName(int id)
        {
            string clientName = string.Empty;
            if (id != 0)
            {
                var conf = ClientInterface.Read(id);

                if (conf != null)
                    clientName = conf.CompanyName;
            }
            return clientName;
        }

        public static Employee GetEmployee(int id)
        {
            Employee employee = null;
            if (id != 0)
            {
                employee = EmployeeInterface.Read(id);
            }
            return employee;
        }

        public static string GetSupplierName(string id)
        {
            string supplierName = string.Empty;
            if (!string.IsNullOrEmpty(id))
            {
                var conf = SupplierInterface.Read(Convert.ToInt32(id));

                if (conf != null)
                    supplierName = conf.SupplierName;
            }
            return supplierName;
        }

        public static string GetProjectName(int id)
        {
            string projectName = string.Empty;
            if (id != 0)
            {
                var conf = ProjectInterface.Read(id);

                if (conf != null)
                    projectName = conf.ProjectName;
            }
            return projectName;
        }

        public static int GetProjectId(int empId)
        {
            int projectId = 0;
            if (empId != 0)
            {
                projectId = EmployeeProjectInterface.ReadByEmployeeId(empId);
            }
            return projectId;
        }

        public static string GetClientAddress(int id)
        {
            string clientAddress = string.Empty;
            if (id != 0)
            {
                var conf = ClientInterface.Read(id);

                if (conf != null)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(conf.AddressLine1);
                    sb.Append(" \n");
                    sb.Append(conf.AddressLine2);
                    sb.Append(" ");
                    sb.Append(conf.City);
                    sb.Append(" ");
                    sb.Append(conf.State);
                    sb.Append(" ");
                    sb.Append(conf.Country);
                    sb.Append(" ");
                    sb.Append(conf.Zip);
                    sb.Append(" \n");
                    sb.Append("TEL: ");
                    sb.Append(conf.PhoneNumber);
                    if (!string.IsNullOrEmpty(conf.FaxNumber))
                    {
                        sb.Append(" |");
                        sb.Append("Fax: ");
                        sb.Append(conf.FaxNumber);
                    }
                    sb.Append(" \n");
                    sb.Append("Email: ");
                    sb.Append(conf.EmailAddress);

                    clientAddress = sb.ToString();
                }
            }
            return clientAddress;
        }

        public static string GetClientAddressHtml(int id, int quotationId)
        {
            string clientAddress = string.Empty;
            if (id != 0)
            {
                var conf = ClientInterface.Read(id);
                var quotation = QuotationInterface.Read(quotationId);

                if (conf != null && quotation != null)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(conf.AddressLine1);
                    sb.Append(" <br />");
                    sb.Append(conf.AddressLine2);
                    sb.Append(" ");
                    if (!string.IsNullOrEmpty(conf.City))
                    {
                        sb.Append(conf.City);
                    }
                    sb.Append(" ");
                    if (!string.IsNullOrEmpty(conf.State))
                    {
                        sb.Append(conf.State);
                    }
                    sb.Append(" <br />");
                    sb.Append(conf.Country);
                    sb.Append(" ");
                    sb.Append(conf.Zip);
                    sb.Append(" <br />");
                    sb.Append("TEL: ");
                    sb.Append(conf.PhoneNumber);
                    if (!string.IsNullOrEmpty(conf.FaxNumber))
                    {
                        sb.Append(" |");
                        sb.Append("Fax: ");
                        sb.Append(conf.FaxNumber);
                    }
                    sb.Append(" <br />");
                    sb.Append("Email: ");
                    if(!string.IsNullOrEmpty(quotation.Email))
                        sb.Append(quotation.Email);
                    else
                        sb.Append(conf.EmailAddress);
                    sb.Append(" <br />");
                    clientAddress = sb.ToString();
                }
            }
            return clientAddress;
        }

        public static List<Employee> GetEmployees(string clientId, string projectId, string employeeStatus)
        {
            List<Employee> empList = new List<Employee>();
            List<EmployeeProject> empProjectList = new List<EmployeeProject>();
            List<EmployeeProject> empProjectArray = new List<EmployeeProject>();
            List<Employee> empArray = new List<Employee>();
            
            if ((!string.IsNullOrEmpty(employeeStatus)))
            {
                empArray = EmployeeInterface.ReadByWorkStatus(employeeStatus).ToList();
            }
            else {
                empArray = EmployeeInterface.Read().ToList();
            }

            if (!string.IsNullOrEmpty(projectId))
            {
                int projectIdValue = Convert.ToInt32(projectId);
                empProjectArray = EmployeeProjectInterface.ReadByProjectAndEmployeeStatus(projectIdValue, employeeStatus).ToList();
                if (empArray != null && empProjectArray != null && empProjectArray.Count() > 0 && empArray.Count() > 0)
                {
                    empList = empArray.Where(x => empProjectArray.Any(y => y.EmployeeId == x.Id)).ToList();
                }
            }
            else if (!string.IsNullOrEmpty(clientId))
            {
                int clientIdValue = Convert.ToInt32(clientId);
                var allProjects = ProjectInterface.ReadByClientId(clientIdValue);
                foreach (var project in allProjects)
                {
                    empProjectArray = EmployeeProjectInterface.ReadByProjectAndEmployeeStatus(project.Id, employeeStatus).ToList();
                    if (empProjectArray != null)
                        empProjectList.AddRange(empProjectArray);
                }
                if (empArray != null && empProjectList != null && empProjectList.Count > 0 && empArray.Count() > 0)
                {
                    empList = empArray.Where(x => empProjectList.Any(y => y.EmployeeId == x.Id)).ToList();
                }
            }
            //Default
            empProjectArray = EmployeeProjectInterface.Read().ToList();

            if (empArray != null && empProjectArray != null && empProjectArray.Count() > 0 && empArray.Count() > 0)
            {
                empList = empArray.Where(x => empProjectArray.Any(y => y.EmployeeId == x.Id)).ToList();
            }
            return empList;
        }

        public static string GetEmployeeName(int employeeId)
        {
            string employeeName = string.Empty;
            if (employeeId != 0)
            {
                var conf = EmployeeInterface.Read(employeeId);
                if (conf != null)
                    employeeName = conf.FirstName + " " + conf.LastName;
            }
            return employeeName;
        }

        public static Dictionary<string, decimal> GetAllowance(int empId, int month, int year)
        {
            Dictionary<string, decimal> allowanceDictionary = new Dictionary<string, decimal>();

            //calculate Allowances - get all allowance for current month and year
            var employeeAllowanceList = AllowanceInterface.ReadByEmployeeMonthYear(empId, month, year);

            //Bonus Allowance
            var empBonusAllowance = employeeAllowanceList.Sum(x => x.BonusAllowance);
            allowanceDictionary.Add("Bonus", empBonusAllowance.GetValueOrDefault(0));

            //Food Allowance
            var empFoodAllowance = employeeAllowanceList.Sum(x => x.FoodAllowance);
            allowanceDictionary.Add("Food", empFoodAllowance.GetValueOrDefault(0));

            //Travel Allowance
            var empTravelAllowance = employeeAllowanceList.Sum(x => x.TravelAllowance);
            allowanceDictionary.Add("Travel", empTravelAllowance.GetValueOrDefault(0));

            //Room Rental Allowance
            var empRentalAllowance = employeeAllowanceList.Sum(x => x.RoomRentalAllowance);
            allowanceDictionary.Add("Rental", empRentalAllowance.GetValueOrDefault(0));

            //Other Allowance
            var empOtherAllowance = employeeAllowanceList.Sum(x => x.OtherAllowance);
            allowanceDictionary.Add("Others", empOtherAllowance.GetValueOrDefault(0));

            //Other Allowance
            var empIncentiveAllowance = employeeAllowanceList.Sum(x => x.IncentiveAllowance);
            allowanceDictionary.Add("Incentive", empIncentiveAllowance.GetValueOrDefault(0));

            var empTotalAllowance = empBonusAllowance + empFoodAllowance + empTravelAllowance + empRentalAllowance + empOtherAllowance + empIncentiveAllowance;
            allowanceDictionary.Add("Total", empTotalAllowance.GetValueOrDefault(0));

            return allowanceDictionary;
        }

        public static decimal GetTotalAllowance(int empId, int month, int year)
        {
            decimal totalAllowance = 0;
            Dictionary<string, decimal> totalAlowanceDictionary = new Dictionary<string, decimal>();
            totalAlowanceDictionary = GetAllowance(empId, month, year);
            totalAllowance = totalAlowanceDictionary["Total"];
            return totalAllowance;
        }

        public static decimal GetLoanAmount(int empId, int month, int year)
        {

            //calculate Loan requested for selected month
            decimal employeeLoanList = 0;
            employeeLoanList = LoanAndAdvanceInterface.ReadByEmployeeMonthYear(empId, month, year)
                                                            .Sum(x => x.LoanAmount);
            return employeeLoanList;
        }

        public static decimal DetectLoanAmount(int empId, int month, int year)
        {
            decimal loanDetectAmount = 0;
            //detect loan amount if exists
            var employeeLoanList = LoanAndAdvanceInterface.ReadByEmployeeId(empId)
                                                            .Where(x => x.LoanStatus == "Active");

            List<decimal> loanEmpList = new List<decimal>();

            foreach (var employeeLoan in employeeLoanList)
            {

                var empLoanDetails = employeeLoan.loanAndAdvanceDetails;

                loanEmpList.AddRange(empLoanDetails.Where(x => x.LoanDetectionDate.Month == month && x.LoanDetectionDate.Year == year)
                                                    .Select(x => x.LoanDetectionAmount));

                if (employeeLoan.LoanRepaymentEndDate.Month == month && employeeLoan.LoanRepaymentEndDate.Year == year)
                {
                    employeeLoan.LoanStatus = "Closed";
                    LoanAndAdvanceInterface.Update(employeeLoan);
                }

            }

            loanDetectAmount = loanEmpList.Sum(x => Convert.ToDecimal(x));

            return loanDetectAmount;
        }

        public static int GetPRYear(Employee emp)
        {
            int prYear = 3;

            switch (emp.WorkStatus)
            {
                case "SingaporeCitizen":
                    prYear = 3;
                    break;

                case "SingaporePR":
                    var prDuration = CalculateAge(emp.PREffectiveDate.GetValueOrDefault());
                    if (prDuration < 1)
                    {
                        prYear = 1;
                    }
                    else if (prDuration > 1 && prDuration < 2)
                    {
                        prYear = 2;
                    }
                    else if (prDuration > 2)
                    {
                        prYear = 3;
                    }      
                    break;

                default:
                    prYear = 3;
                    break;
            }

            return prYear;
        }

        public static decimal GetDonation(decimal salary, string donationType)
        {
            decimal? donation = 0;
            if (!string.IsNullOrEmpty(donationType))
            {
                var donationList = DonationInterface.ReadbyDonationType(donationType);
                if (donationList != null && donationList.Count() > 0)
                {
                    donation = donationList.Where(x => (salary >= x.LowerRange
                                                && salary <= x.UpperRange))
                                                   .Select(x => x.Contribution).FirstOrDefault();
                }
            }
            return donation.GetValueOrDefault(0);
        }

        public static decimal GetDonation(string nationality, string religion, decimal salary)
        {
            decimal? nationalDonation = 0;
            decimal? religionDonation = 0;

            if (!string.IsNullOrEmpty(nationality))
            {
                var donationList = DonationInterface.ReadbyNationality(nationality);
                if (donationList != null && donationList.Count() > 0)
                {
                    nationalDonation = donationList.Where(x => salary > x.LowerRange
                                                    && salary < x.UpperRange)
                                               .Select(x => x.Contribution).FirstOrDefault();
                }
            }
            if (!string.IsNullOrEmpty(religion))
            {
                var religionList = DonationInterface.ReadbyReligion(religion);
                if (religionList != null && religionList.Count() > 0)
                {
                    religionDonation = religionList.Where(x => salary > x.LowerRange
                                                && salary < x.UpperRange)
                                                   .Select(x => x.Contribution).FirstOrDefault();
                }
            }

            return (nationalDonation.GetValueOrDefault(0) + religionDonation.GetValueOrDefault(0));

        }

        public static bool IsPublicHoliday(DateTime inputDate)
        {
            var PublicHoliday = PublicHolidayInterface.ReadByDate(inputDate.Date);
            if (PublicHoliday != null)
            {
                return true;
            }
            //Calculate the rest day            
            else
            {
                return false;
            }
        }

        public static decimal GetAvgWorkingHour(int id)
        {
            var workinghr = WorkingHourInterface.Read(id);
            return workinghr != null ? workinghr.WeeklyAvgWorkingHour.Value : 44;
        }

        public static decimal GetAvgWorkingDays(int id)
        {
            var workinghr = WorkingHourInterface.Read(id);
            return workinghr != null ? workinghr.WeeklyAvgWorkingDays.Value : 5;
        }

        public static decimal GetWorkingHour(int id)
        {
            var workinghr = WorkingHourInterface.Read(id);
            return workinghr != null ? workinghr.TotalHour.Value : 8;
        }

        public static decimal GetWorkingHourByEmployee(int id)
        {
            var employee = EmployeeInterface.ReadByEmpId(id);
            var workinghr = WorkingHourInterface.Read(employee.WorkingHours.Value);
            return workinghr != null ? workinghr.TotalHour.Value : 8;
        }

        public static bool IsFullRestDay(int restDayval, DateTime timesheetDate)
        {
            var RestdayRecord = RestDayInterface.Read(restDayval);
            bool IsRestday = false;
            if (RestdayRecord.RestDayType == "Day")
            {
                if (RestdayRecord.IsMondayRestDay && !string.IsNullOrEmpty(RestdayRecord.MondayRestType) && RestdayRecord.MondayRestType == "Full" && timesheetDate.DayOfWeek == DayOfWeek.Monday)
                    IsRestday = true;
                if (RestdayRecord.IsTuesdayRestDay && !string.IsNullOrEmpty(RestdayRecord.TuesdayRestType) && RestdayRecord.TuesdayRestType == "Full" && timesheetDate.DayOfWeek == DayOfWeek.Tuesday)
                    IsRestday = true;
                if (RestdayRecord.IsWednesdayRestDay && !string.IsNullOrEmpty(RestdayRecord.WednesdayRestType) && RestdayRecord.WednesdayRestType == "Full" && timesheetDate.DayOfWeek == DayOfWeek.Wednesday)
                    IsRestday = true;
                if (RestdayRecord.IsThursdayRestDay && !string.IsNullOrEmpty(RestdayRecord.ThursdayRestType) && RestdayRecord.ThursdayRestType == "Full" && timesheetDate.DayOfWeek == DayOfWeek.Thursday)
                    IsRestday = true;
                if (RestdayRecord.IsFridayRestDay && !string.IsNullOrEmpty(RestdayRecord.FridayRestType) && RestdayRecord.FridayRestType == "Full" && timesheetDate.DayOfWeek == DayOfWeek.Friday)
                    IsRestday = true;
                if (RestdayRecord.IsSaturdayRestDay && !string.IsNullOrEmpty(RestdayRecord.SaturdayRestType) && RestdayRecord.SaturdayRestType == "Full" && timesheetDate.DayOfWeek == DayOfWeek.Saturday)
                    IsRestday = true;
                if (RestdayRecord.IsSundayRestDay && !string.IsNullOrEmpty(RestdayRecord.SundayRestType) && RestdayRecord.SundayRestType == "Full" && timesheetDate.DayOfWeek == DayOfWeek.Sunday)
                    IsRestday = true;
            }
            else if (RestdayRecord.RestDayType == "Date")
            {
                if (RestdayRecord.RestDates.Where(i => i.Date == timesheetDate) != null)
                {
                    IsRestday = true;
                }
            }
            return IsRestday;
        }

        public static decimal GetSDL(decimal totalSalary)
        {
            decimal sdl = 0;
            if (totalSalary <= 800)
            {
                sdl = 2;
            }
            else if (totalSalary > 800 && totalSalary < 4500)
            {
                sdl = Convert.ToDecimal(totalSalary * Convert.ToDecimal(0.0025));
             }
            else
            {
                sdl = Convert.ToDecimal(11.25);
            }
            return sdl;
        }

        public static bool IsHalfRestDay(int restDayval, DateTime timesheetDate)
        {
            var RestdayRecord = RestDayInterface.Read(restDayval);
            bool IsRestday = false;
            if (RestdayRecord.RestDayType == "Day")
            {
                if (RestdayRecord.IsMondayRestDay && !string.IsNullOrEmpty(RestdayRecord.MondayRestType) && RestdayRecord.MondayRestType == "Half" && timesheetDate.DayOfWeek == DayOfWeek.Monday)
                    IsRestday = true;
                if (RestdayRecord.IsTuesdayRestDay && !string.IsNullOrEmpty(RestdayRecord.TuesdayRestType) && RestdayRecord.TuesdayRestType == "Half" && timesheetDate.DayOfWeek == DayOfWeek.Tuesday)
                    IsRestday = true;
                if (RestdayRecord.IsWednesdayRestDay && !string.IsNullOrEmpty(RestdayRecord.WednesdayRestType) && RestdayRecord.WednesdayRestType == "Half" && timesheetDate.DayOfWeek == DayOfWeek.Wednesday)
                    IsRestday = true;
                if (RestdayRecord.IsThursdayRestDay && !string.IsNullOrEmpty(RestdayRecord.ThursdayRestType) && RestdayRecord.ThursdayRestType == "Half" && timesheetDate.DayOfWeek == DayOfWeek.Thursday)
                    IsRestday = true;
                if (RestdayRecord.IsFridayRestDay && !string.IsNullOrEmpty(RestdayRecord.FridayRestType) && RestdayRecord.FridayRestType == "Half" && timesheetDate.DayOfWeek == DayOfWeek.Friday)
                    IsRestday = true;
                if (RestdayRecord.IsSaturdayRestDay && !string.IsNullOrEmpty(RestdayRecord.SaturdayRestType) && RestdayRecord.SaturdayRestType == "Half" && timesheetDate.DayOfWeek == DayOfWeek.Saturday)
                    IsRestday = true;
                if (RestdayRecord.IsSundayRestDay && !string.IsNullOrEmpty(RestdayRecord.SundayRestType) && RestdayRecord.SundayRestType == "Half" && timesheetDate.DayOfWeek == DayOfWeek.Sunday)
                    IsRestday = true;
            }
            return IsRestday;
        }

        public static decimal GetPaidLeaveCount(int empId, int restDay, int month, int year)
        {
            var leaveArray = LeaveInterface.ReadByEmployeeYearAndMonth(empId, year, month);
            var count = leaveArray != null ? leaveArray.Sum(x => x.PaidLeave) : 0;
            return count;

            //double leave = 0;
            //double leaveCount = 0;
            //var leaveArray = LeaveInterface.ReadByEmployeeYearAndMonth(empId, year, month);
            //if (leaveArray != null && leaveArray.Length > 0)
            //{
            //    foreach (var record in leaveArray)
            //    {
            //        var appliedLeave = (record.EndDate.Value - record.StartDate.Value).TotalDays + 1;
            //        for (int i= 0; i < appliedLeave; i++)
            //        {
            //            if (IsFullRestDay(restDay, record.StartDate.Value.AddDays(i)))
            //            {
            //                appliedLeave = appliedLeave - 1;
            //            }
            //            else if (IsHalfRestDay(restDay, record.StartDate.Value.AddDays(i)))
            //            {
            //                appliedLeave = appliedLeave - 0.5;
            //            }
            //            else if (IsPublicHoliday(record.StartDate.Value.AddDays(i)))
            //            {
            //                appliedLeave = appliedLeave - 1;
            //            }
            //        }
            //        leave = leave + appliedLeave;
            //    }
            //    leaveCount = leave - leaveArray.Sum(x => x.UnPaidLeave);
            //}
            //return leaveCount;

        }

        public static decimal GetUnPaidLeaveCount(int id, int month, int year)
        {
            var leaveArray = LeaveInterface.ReadByEmployeeYearAndMonth(id, year, month);
            var count = leaveArray != null ? leaveArray.Sum(x => x.UnPaidLeave) : 0;
            return count;
        }

        public static decimal GetHourlyRate(decimal totalIncome, decimal workingHrs)
        {
            var hourlyRate = (12 * (totalIncome)) / (52 * workingHrs);
            return hourlyRate;
        }

        public static decimal GetWeeklyRate(decimal totalIncome, decimal workingDays)
        {
            var weeklyRate = (12 * (totalIncome)) / (52 * workingDays);
            return weeklyRate;
        }

        public static string GetRestDayGroupName(string id)
        {
            string groupName = string.Empty;
            if (!string.IsNullOrEmpty(id))
            {
                var conf = RestDayInterface.Read(Convert.ToInt32(id));

                if (conf != null)
                    groupName = conf.GroupName;
            }
            return groupName;
        }

        public static double GetEmployeeWorkingDays(int empId, int restDayVal, bool isphasnormalday, int month, int year)
        {

            double restDayCount = 0;
            double workingDays = 0;

            int noOfDaysinMonth = DateTime.DaysInMonth(year, month);
            DateTime beginingOfThisMonth = new DateTime(year, month, 1);

            var RestdayRecord = RestDayInterface.Read(restDayVal);
            double monday = 0; double tuesday = 0; double wednesday = 0; double thursday = 0; double friday = 0; double saturday = 0; double sunday = 0;

            if (RestdayRecord != null)
            {
                if (RestdayRecord.RestDayType == "Day")
                {
                    if (RestdayRecord.IsSundayRestDay)
                    {
                        for (int i = 0; i < noOfDaysinMonth; i++)
                        {
                            if (beginingOfThisMonth.AddDays(i).DayOfWeek == DayOfWeek.Sunday)
                            {
                                if (!string.IsNullOrEmpty(RestdayRecord.SundayRestType) && RestdayRecord.SundayRestType == "Half")
                                    sunday = sunday + 0.5;
                                else
                                    sunday++;
                            }
                        }
                    }

                    if (RestdayRecord.IsMondayRestDay)
                    {
                        for (int i = 0; i < noOfDaysinMonth; i++)
                        {
                            if (beginingOfThisMonth.AddDays(i).DayOfWeek == DayOfWeek.Monday)
                            {
                                if (!string.IsNullOrEmpty(RestdayRecord.MondayRestType) && RestdayRecord.MondayRestType == "Half")
                                    monday = monday + 0.5;
                                else
                                    monday++;
                            }
                        }
                    }

                    if (RestdayRecord.IsTuesdayRestDay)
                    {
                        for (int i = 0; i < noOfDaysinMonth; i++)
                        {
                            if (beginingOfThisMonth.AddDays(i).DayOfWeek == DayOfWeek.Tuesday)
                            {
                                if (!string.IsNullOrEmpty(RestdayRecord.TuesdayRestType) && RestdayRecord.TuesdayRestType == "Half")
                                    tuesday = tuesday + 0.5;
                                else
                                    tuesday++;
                            }
                        }
                    }

                    if (RestdayRecord.IsWednesdayRestDay)
                    {
                        for (int i = 0; i < noOfDaysinMonth; i++)
                        {
                            if (beginingOfThisMonth.AddDays(i).DayOfWeek == DayOfWeek.Wednesday)
                            {
                                if (!string.IsNullOrEmpty(RestdayRecord.WednesdayRestType) && RestdayRecord.WednesdayRestType == "Half")
                                    wednesday = wednesday + 0.5;
                                else
                                    wednesday++;
                            }
                        }
                    }

                    if (RestdayRecord.IsThursdayRestDay)
                    {
                        for (int i = 0; i < noOfDaysinMonth; i++)
                        {
                            if (beginingOfThisMonth.AddDays(i).DayOfWeek == DayOfWeek.Thursday)
                            {
                                if (!string.IsNullOrEmpty(RestdayRecord.ThursdayRestType) && RestdayRecord.ThursdayRestType == "Half")
                                    thursday = thursday + 0.5;
                                else
                                    thursday++;
                            }
                        }
                    }

                    if (RestdayRecord.IsFridayRestDay)
                    {
                        for (int i = 0; i < noOfDaysinMonth; i++)
                        {
                            if (beginingOfThisMonth.AddDays(i).DayOfWeek == DayOfWeek.Friday)
                            {
                                if (!string.IsNullOrEmpty(RestdayRecord.FridayRestType) && RestdayRecord.FridayRestType == "Half")
                                    friday = friday + 0.5;
                                else
                                    friday++;
                            }
                        }
                    }

                    if (RestdayRecord.IsSaturdayRestDay)
                    {
                        for (int i = 0; i < noOfDaysinMonth; i++)
                        {
                            if (beginingOfThisMonth.AddDays(i).DayOfWeek == DayOfWeek.Saturday)
                            {
                                if (!string.IsNullOrEmpty(RestdayRecord.SaturdayRestType) && RestdayRecord.SaturdayRestType == "Half")
                                    saturday = saturday + 0.5;
                                else
                                    saturday++;
                            }
                        }
                    }

                }
                else if (RestdayRecord.RestDayType == "Date")
                {
                    restDayCount = RestdayRecord.RestDates.Where(i => i.Date.Value.Month == month && i.Date.Value.Year == year).Count();
                }
            }

            var publicHolidays = PublicHolidayInterface.ReadByMonthYear(month, year);
            int publicHolidayCount = 0;

            if(publicHolidays != null && publicHolidays.Length > 0)
            {
                foreach (var holiday in publicHolidays)
                {
                    if (!IsFullRestDay(restDayVal, holiday.Date))
                    {
                        publicHolidayCount += 1;
                    }
                }
            }

            workingDays = noOfDaysinMonth - monday - tuesday - wednesday - thursday - friday - saturday - sunday - restDayCount;

            if (!isphasnormalday)
            {
                workingDays = workingDays - publicHolidayCount;
            }

            return workingDays;
        }

        public static decimal GetOvertimePay(int id, decimal hourlRate, decimal otHrs)
        {
            decimal overtimeVal = hourlRate * otHrs * Convert.ToDecimal(1.5);

            if (id > 0)
            {
                var data = OvertimeInterface.Read(id);
                if (data != null && !string.IsNullOrEmpty(data.OTType))
                {
                    switch (data.OTType)
                    {
                        case "Percentage":
                            overtimeVal = hourlRate * otHrs * data.OTValue.GetValueOrDefault(0);
                            break;

                        case "Flat-Rate":
                            overtimeVal = otHrs * data.OTValue.GetValueOrDefault(0);
                            break;

                        case "Fixed-Rate":
                            overtimeVal = data.OTValue.GetValueOrDefault(0);
                            break;

                        default:
                            overtimeVal = hourlRate * otHrs * Convert.ToDecimal(1.5);
                            break;

                    }
                }
            }
            return overtimeVal;
        }

        public static decimal GetRestDayPay(int id, decimal weeklyRate, int restDay, bool isFullRestDay = false)
        {
            decimal restdayVal = weeklyRate * restDay;

            if (isFullRestDay)
                restdayVal = weeklyRate * restDay * 2;

            if (id > 0)
            {
                var restDayGroup = RestDayInterface.Read(id);
                if (restDayGroup != null)
                {
                    var data = RestDayPayInterface.ReadByGroupName(restDayGroup.Id.ToString());
                    if (isFullRestDay && data != null && !string.IsNullOrEmpty(data.FullRestDayPayType))
                    {
                        switch (data.FullRestDayPayType)
                        {
                            case "Percentage":
                                restdayVal = weeklyRate * restDay * data.FullRestDayPayValue.GetValueOrDefault(0);
                                break;

                            case "Flat-Rate":
                                restdayVal = restDay * data.FullRestDayPayValue.GetValueOrDefault(0);
                                break;

                            case "Fixed-Rate":
                                restdayVal = data.FullRestDayPayValue.GetValueOrDefault(0);
                                break;
                        }
                    }
                    else if (data != null && !string.IsNullOrEmpty(data.HalfRestDayPayType))
                    {
                        switch (data.HalfRestDayPayType)
                        {
                            case "Percentage":
                                restdayVal = weeklyRate * restDay * data.HalfRestDayPayValue.GetValueOrDefault(0);
                                break;

                            case "Flat-Rate":
                                restdayVal = restDay * data.HalfRestDayPayValue.GetValueOrDefault(0);
                                break;

                            case "Fixed-Rate":
                                restdayVal = data.HalfRestDayPayValue.GetValueOrDefault(0);
                                break;
                        }
                    }
                }
            }
            return restdayVal;
        }

        public static decimal GetRestDayExtraPay(int id, decimal weeklyRate, decimal restHour, decimal normalWorkinghr)
        {
            var restDay = 1;
            var extraHour = Convert.ToDouble((restHour / normalWorkinghr));
            if(extraHour > 0.5 )
            {
                restDay = 2;
            }

            decimal restdayVal = weeklyRate * restDay;

            if (id > 0)
            {
                var restDayGroup = RestDayInterface.Read(id);
                if (restDayGroup != null)
                {
                    var data = RestDayPayInterface.ReadByGroupName(restDayGroup.Id.ToString());
                    if (data != null && !string.IsNullOrEmpty(data.HalfRestDayPayType))
                    {
                        switch (data.HalfRestDayPayType)
                        {
                            case "Percentage":
                                restdayVal = weeklyRate * restDay * data.HalfRestDayPayValue.GetValueOrDefault(0);
                                break;

                            case "Flat-Rate":
                                restdayVal = restDay * data.HalfRestDayPayValue.GetValueOrDefault(0);
                                break;

                            case "Fixed-Rate":
                                restdayVal = data.HalfRestDayPayValue.GetValueOrDefault(0);
                                break;

                            default:
                                restdayVal = weeklyRate * restDay;
                                break;
                        }
                    }
                }
            }
            return restdayVal;
        }

        public static decimal GetHolidayPay(int id, decimal weeklyRate, decimal publicHoliday)
        {
            decimal restdayVal = weeklyRate * publicHoliday;

            if (id > 0)
            {
                var phPayGroup = PublicHolidayPayInterface.Read(id);
                if (phPayGroup != null && !string.IsNullOrEmpty(phPayGroup.PayType))
                {
                    switch (phPayGroup.PayType)
                    {
                        case "Percentage":
                            restdayVal = weeklyRate * publicHoliday * phPayGroup.PayValue.GetValueOrDefault(0);
                            break;

                        case "Flat-Rate":
                            restdayVal = publicHoliday * phPayGroup.PayValue.GetValueOrDefault(0);
                            break;

                        case "Fixed-Rate":
                            restdayVal = phPayGroup.PayValue.GetValueOrDefault(0);
                            break;

                        default:
                            restdayVal = weeklyRate * publicHoliday;
                            break;
                    }
                }
            }
            return restdayVal;
        }

        public static CFPdata CalculateCPF(int age, decimal totalWage, decimal ordWage, decimal AddWage, int PRYearCount)
        {
            CFPdata cpf = new CFPdata();
            CPFLookup[] data = CPFInterface.ReadLookupValbyAgeYear(PRYearCount, age);

            if (data != null && data.Any())
            {
                var agedata = data.Where(x => x.AgeMaxVal > age && x.AgeMinVal < age);

                var wagedata = data.Where(x => (x.WageMaxVal != null && x.WageMaxVal > totalWage && x.WageMinVal < totalWage)
                                                    || (x.WageMaxVal == null && x.WageMinVal < totalWage)
                                                   ).FirstOrDefault();
                if (wagedata != null)
                {
                    cpf.TotalCPF = GetCPF(wagedata.TotalCPF, totalWage, ordWage, AddWage, wagedata.TotalOWMaxVal.GetValueOrDefault(0));
                    cpf.EmployeeCPF = GetCPF(wagedata.EmployeeCPF, totalWage, ordWage, AddWage, wagedata.EmployeeOWMaxVal.GetValueOrDefault(0));
                    cpf.EmployerCPF = cpf.TotalCPF - cpf.EmployeeCPF;
                }
            }
            return cpf;
        }

        public static List<string> GetUserRole(string userId)
        {
            List<string> roleList = new List<string>();
            var data = UserRoleInterface.ReadByUserRolename(userId);

            foreach (var role in data)
            {
                roleList.Add(role.Role.RoleName);
            }
            return roleList;
        }

        private static double GetCPF(string CPFVal, decimal wage, decimal? ordWage, decimal? addwage, decimal? maxval)
        {
            if (CPFVal.Length > 1)
            {
                CPFVal = CPFVal.Replace("TW", wage.ToString());

                if (ordWage != null && addwage != null && maxval != null)
                {
                    string owCheckString = CPFVal.Substring(0, CPFVal.IndexOf("(OW)") + 4);

                    double owVal = Evaluate(owCheckString.Replace("OW", ordWage.GetValueOrDefault(0).ToString()));
                    var owMaxVal = owVal > 0 ? Convert.ToDecimal(owVal) : 0;

                    owMaxVal = owMaxVal > maxval.GetValueOrDefault(0) ? maxval.GetValueOrDefault(0) : owMaxVal;

                    CPFVal = CPFVal.Replace(owCheckString, owVal.ToString());

                    CPFVal = CPFVal.Replace("AW", addwage.GetValueOrDefault(0).ToString());
                }

            }

            return Evaluate(CPFVal);
        }

        private static double Evaluate(string expression)
        {
            DataTable table = new DataTable();
            table.Columns.Add("expression", typeof(string), expression);
            DataRow row = table.NewRow();
            table.Rows.Add(row);
            return double.Parse((string)row["expression"]);
        }

        public static int CalculateAge(DateTime birthDay)
        {
            int years = DateTime.Now.Year - birthDay.Year;

            if ((birthDay.Month > DateTime.Now.Month) || (birthDay.Month == DateTime.Now.Month && birthDay.Day > DateTime.Now.Day))
                years--;

            return years;
        }
    }
}
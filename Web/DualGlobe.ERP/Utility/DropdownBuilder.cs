using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Library.DualGlobe.ERP.Interfaces;
using System.Globalization;

namespace DualGlobe.ERP.Utility
{
    public class DropdownBuilder
    {
        public static IEnumerable<SelectListItem> GetDiscountTypes()
        {
            return new SelectList(new List<SelectListItem> {
                                    new SelectListItem { Value = "Percentage", Text = "Percentage"},
                                    new SelectListItem { Value = "Amount", Text = "Amount"}
                                                         }, "Value", "Text");
        }

        public static IEnumerable<SelectListItem> GetAllPaymentMethods()
        {
            return new SelectList(new List<SelectListItem> {
                                    new SelectListItem { Value = "Cash", Text = "Cash"},
                                    new SelectListItem { Value = "Cheque", Text = "Cheque"},
                                    new SelectListItem { Value = "Draft", Text = "Draft"},
                                    new SelectListItem { Value = "GIRO", Text = "GIRO"},
                                    new SelectListItem { Value = "Paypal", Text = "Paypal"}
                                                         }, "Value", "Text");

        }

        public static IEnumerable<SelectListItem> GetExpenseCategory()
        {
            List<SelectListItem> clientList = new List<SelectListItem>();
            clientList = ExpenseCategoryInterface.Read()
                                          .Select(x => new SelectListItem()
                                          {
                                              Value = x.Id.ToString(),
                                              Text = x.Description
                                          })
                                          .OrderBy(x => x.Text)
                                          .ToList();
            return clientList;

        }

        public static IEnumerable<SelectListItem> GetAllFilters()
        {
            return new SelectList(new List<SelectListItem> {
                                    new SelectListItem { Value = "true", Text = "With GST"},
                                    new SelectListItem { Value = "false", Text = "Without GST"}
                                                         }, "Value", "Text");
        }

        //public static IEnumerable<SelectListItem> GetExpenseCategory()
        //{
        //    return new SelectList(new List<SelectListItem> {
        //                            new SelectListItem { Value = "Accounting and legal fee", Text = "Accounting and legal fee"},
        //                            new SelectListItem { Value = "Advertising", Text = "Advertising"},
        //                            new SelectListItem { Value = "Utilities", Text = "Utilities"},
        //                            new SelectListItem { Value = "Insurance", Text = "Insurance"},
        //                            new SelectListItem { Value = "Interest and bank charges", Text = "Interest and bank charges"},
        //                            new SelectListItem { Value = "Postage", Text = "Postage"},
        //                            new SelectListItem { Value = "Printing and Stationary", Text = "Printing and Stationary"},
        //                            new SelectListItem { Value = "Professional memberships", Text = "Professional memberships"},
        //                            new SelectListItem { Value = "Rent for premises", Text = "Rent for premises"},
        //                            new SelectListItem { Value = "Repair and maintenance", Text = "Repair and maintenance"},
        //                            new SelectListItem { Value = "Training", Text = "Training"},
        //                            new SelectListItem { Value = "Vehicle operating costs", Text = "Vehicle operating costs"},
        //                            new SelectListItem { Value = "Worker compensation", Text = "Worker compensation"},
        //                            new SelectListItem { Value = "Depreciation", Text = "Depreciation"},
        //                            new SelectListItem { Value = "Material Purchase", Text = "Material Purchase"},
        //                            new SelectListItem { Value = "Other", Text = "Other Expense"}
        //                                                 }, "Value", "Text").OrderBy(c => c.Text);

        //}

        public static IEnumerable<SelectListItem> GetCategory()
        {
            return new SelectList(new List<SelectListItem> {
                                            new SelectListItem { Value = "Project", Text = "Project Expense"},
                                            new SelectListItem { Value = "Operation", Text = "Operation Expense"}
                                                            }, "Value", "Text");
        }

        public static IEnumerable<SelectListItem> GetAllTiers()
        {
            return new SelectList(new List<SelectListItem> {
                                            new SelectListItem { Value = "Basic", Text = "Basic"},
                                            new SelectListItem { Value = "Tier2", Text = "Tier 2"}
                                                            }, "Value", "Text");
        }

        public static IEnumerable<SelectListItem> GetLeaveTypes()
        {
            return new SelectList(new List<SelectListItem> {
                                    new SelectListItem { Value = "Sick", Text = "Sick Leave"},
                                    new SelectListItem { Value = "Annual", Text = "Annual Leave"},
                                    new SelectListItem { Value = "ChildCare", Text = "Child Care Leave"},
                                    new SelectListItem { Value = "Maternity", Text = "Maternity Leave"},
                                    new SelectListItem { Value = "Paternity", Text = "Paternity Leave"},
                                    new SelectListItem { Value = "SharedParental", Text = "Shared Parental Leave"},
                                    new SelectListItem { Value = "Adoption", Text = "Adoption Leave"},
                                    new SelectListItem { Value = "UnpaidInfantCare", Text = "Unpaid Infant Care Leave"},
                                    new SelectListItem { Value = "Other", Text = "Other"}
                                                         }, "Value", "Text");

        }

        public static IEnumerable<SelectListItem> GetInsuranceTypes()
        {
            return new SelectList(new List<SelectListItem> {
                                    new SelectListItem { Value = "MedicalInsurance", Text = "Medical Insurance"},
                                    new SelectListItem { Value = "SecurityBond", Text = "Security Bond"},
                                    new SelectListItem { Value = "WorkInjuryInsurance", Text = "Work Injury Insurance"}
                                                         }, "Value", "Text");

        }


        public static IEnumerable<SelectListItem> GetAllProjects()
        {
            List<SelectListItem> projectList = new List<SelectListItem>();
            projectList = ProjectInterface.Read()
                                          .Select(x => new SelectListItem()
                                          {
                                              Value = x.Id.ToString(),
                                              Text = x.Id.ToString() + " : " + x.ProjectName
                                          })
                                          .OrderBy(x => Convert.ToInt32(x.Value))
                                          .ToList();
            return projectList;

        }

        public static IEnumerable<SelectListItem> GetAllClientProjects(int clientId)
        {
            List<SelectListItem> projectList = new List<SelectListItem>();
            projectList = ProjectInterface.Read().Where(i => i.ClientId == clientId)
                                          .Select(x => new SelectListItem()
                                          {
                                              Value = x.Id.ToString(),
                                              Text = x.Id.ToString() + " : " + x.ProjectName
                                          })
                                          .OrderBy(x => Convert.ToInt32(x.Value))
                                          .ToList();
            return projectList;

        }

        public static IEnumerable<SelectListItem> GetOTGroups()
        {
            List<SelectListItem> otGroupList = new List<SelectListItem>();
            otGroupList = OvertimeInterface.Read()
                                          .Select(x => new SelectListItem()
                                          {
                                              Value = x.Id.ToString(),
                                              Text = x.GroupName
                                          })
                                          .OrderBy(x => Convert.ToInt32(x.Value))
                                          .ToList();
            return otGroupList;
        }

        public static IEnumerable<SelectListItem> GetPHGroups()
        {
            List<SelectListItem> otGroupList = new List<SelectListItem>();
            otGroupList = PublicHolidayPayInterface.Read()
                                          .Select(x => new SelectListItem()
                                          {
                                              Value = x.Id.ToString(),
                                              Text = x.GroupName
                                          })
                                          .OrderBy(x => Convert.ToInt32(x.Value))
                                          .ToList();
            return otGroupList;
        }

        public static IEnumerable<SelectListItem> GetWorkPermitAddress()
        {
            List<SelectListItem> wpAddressList = new List<SelectListItem>();
            wpAddressList = WorkPermitAddressInterface.Read()
                                          .Select(x => new SelectListItem()
                                          {
                                              Value = x.Id.ToString(),
                                              Text = x.Address
                                          })
                                          .OrderBy(x => x.Text)
                                          .ToList();
            return wpAddressList;
        }

        public static IEnumerable<SelectListItem> GetInsuranceProviderName(string type)
        {
            List<SelectListItem> insuranceProviderList = new List<SelectListItem>();
            insuranceProviderList = InsuranceInterface.ReadByType(type)
                                          .Select(x => new SelectListItem()
                                          {
                                              Value = x.Id.ToString(),
                                              Text = x.InsuranceProviderName
                                          })
                                          .OrderBy(x => x.Text)
                                          .ToList();
            return insuranceProviderList;
        }

        public static IEnumerable<SelectListItem> GetInsurancePolicyNumbers(string type)
        {
            List<SelectListItem> insuranceProviderList = new List<SelectListItem>();
            insuranceProviderList = InsuranceInterface.ReadByType(type)
                                          .Select(x => new SelectListItem()
                                          {
                                              Value = x.InsurancePolicyNumber.ToString(),
                                              Text = x.InsurancePolicyNumber
                                          })
                                          .OrderBy(x => x.Text)
                                          .ToList();
            return insuranceProviderList;
        }


        public static IEnumerable<SelectListItem> GetRestDays()
        {
            List<SelectListItem> restDayList = new List<SelectListItem>();
            restDayList = RestDayInterface.Read()
                                          .Select(x => new SelectListItem()
                                          {
                                              Value = x.Id.ToString(),
                                              Text = x.GroupName
                                          })
                                          .OrderBy(x => x.Text)
                                          .ToList();
            return restDayList;
        }

        public static IEnumerable<SelectListItem> GetWorkingHours()
        {
            List<SelectListItem> workingHourList = new List<SelectListItem>();
            workingHourList = WorkingHourInterface.Read()
                                          .Select(x => new SelectListItem()
                                          {
                                              Value = x.Id.ToString(),
                                              Text = x.GroupName
                                          })
                                          .OrderBy(x => x.Text)
                                          .ToList();
            return workingHourList;
        }

        public static IEnumerable<SelectListItem> GetAllClients()
        {
            List<SelectListItem> clientList = new List<SelectListItem>();
            clientList = ClientInterface.Read()
                                          .Select(x => new SelectListItem()
                                          {
                                              Value = x.Id.ToString(),
                                              Text = x.CompanyName
                                          })
                                          .OrderBy(x => Convert.ToInt32(x.Value))
                                          .ToList();
            return clientList;
        }

        public static IEnumerable<SelectListItem> GetAllQuotation()
        {
            List<SelectListItem> clientList = new List<SelectListItem>();
            clientList = QuotationInterface.Read().Where(i => i.Stage == "Confirmed")
                                          .Select(x => new SelectListItem()
                                          {
                                              Value = x.Id.ToString(),
                                              Text = x.Description.ToString()
                                          })
                                          .OrderBy(x => Convert.ToInt32(x.Value))
                                          .ToList();
            return clientList;
        }

        public static IEnumerable<SelectListItem> GetAllEmployees()
        {
            List<SelectListItem> employeeList = new List<SelectListItem>();
            employeeList = EmployeeInterface.Read().Where(i => i.Status == "true")
                                          .Select(x => new SelectListItem()
                                          {
                                              Value = x.Id.ToString(),
                                              Text = x.EmployeeId.ToString() + " : " + x.FirstName + " " + x.LastName
                                          })
                                          .OrderBy(x => Convert.ToInt32(x.Value))
                                          .ToList();
            return employeeList;

        }

        public static IEnumerable<SelectListItem> GetAllEmployeeClientProjects(int? clientId, int? projectId)
        {
            List<SelectListItem> employeeList = new List<SelectListItem>();
            if (projectId != null && projectId.Value > 0)
            {
                var employees = EmployeeProjectInterface.ReadByProject(projectId.Value);
                foreach (var employee in employees)
                {
                    if (!employeeList.Any(i => i.Value == employee.EmployeeId.ToString()))
                    {
                        employeeList.Add(new SelectListItem
                        {
                            Value = employee.EmployeeId.ToString(),
                            Text = employee.EmployeeId.ToString() + " : " + employee.employee.FirstName + " " + employee.employee.LastName
                        });
                    }
                }
            }

            else if (clientId != null && clientId.Value > 0)
            {
                var projects = ProjectInterface.ReadByClientId(clientId.Value);
                foreach (var project in projects)
                {
                    var employees = EmployeeProjectInterface.ReadByProject(project.Id);
                    foreach (var employee in employees)
                    {
                        if (!employeeList.Any(i => i.Value == employee.EmployeeId.ToString()))
                        {
                            employeeList.Add(new SelectListItem
                            {
                                Value = employee.EmployeeId.ToString(),
                                Text = employee.EmployeeId.ToString() + " : " + employee.employee.FirstName + " " + employee.employee.LastName
                            });
                        }
                    }
                }
            }
            employeeList = employeeList.OrderBy(x => Convert.ToInt32(x.Value)).ToList();
            return employeeList;

        }

        public static IEnumerable<SelectListItem> GetAllProjectEmployees()
        {
            List<SelectListItem> employeeList = new List<SelectListItem>();
            var employees = EmployeeInterface.Read().Where(i => i.Status == "true").ToList();
            var projectEmployees = EmployeeProjectInterface.Read();
            employeeList = employees.Where(i => projectEmployees.Any(j => j.EmployeeId == i.Id))
                                          .Select(x => new SelectListItem()
                                          {
                                              Value = x.Id.ToString(),
                                              Text = x.EmployeeId.ToString() + " : " + x.FirstName + " " + x.LastName
                                          })
                                          .OrderBy(x => Convert.ToInt32(x.Value))
                                          .ToList();
            return employeeList;

        }

        public static IEnumerable<SelectListItem> GetMonths()
        {
            return DateTimeFormatInfo
                       .InvariantInfo
                       .MonthNames
                       .Select((monthName, index) => new SelectListItem
                       {
                           Value = (index + 1).ToString(),
                           Text = monthName
                       });

        }

        public static IEnumerable<SelectListItem> GetYears()
        {
            return new SelectList(Enumerable.Range(DateTime.Today.AddYears(-2).Year, 20).Select(x =>
                                       new SelectListItem()
                                       {
                                           Text = x.ToString(),
                                           Value = x.ToString()
                                       }), "Value", "Text");
        }

        public static IEnumerable<SelectListItem> GetEmploymentTypes()
        {
            return new SelectList(new List<SelectListItem> {
                                            new SelectListItem { Value = "Full-Time", Text = "Full-Time"},
                                            new SelectListItem { Value = "Part-Time", Text = "Part-Time"},
                                            new SelectListItem { Value = "Temporary", Text = "Temporary"},
                                            new SelectListItem { Value = "Contract", Text = "Contract"}
                                                            }, "Value", "Text");
        }

        public static IEnumerable<SelectListItem> GetOTTypes()
        {
            return new SelectList(new List<SelectListItem> {
                                            new SelectListItem { Value = "Percentage", Text = "Percentage"},
                                            new SelectListItem { Value = "Flat-Rate", Text = "Flat-Rate"},
                                            new SelectListItem { Value = "Fixed-Rate", Text = "Fixed-Rate"}
                                                            }, "Value", "Text");
        }

        public static IEnumerable<SelectListItem> GetRestDayTypes()
        {
            return new SelectList(new List<SelectListItem> {
                                            new SelectListItem { Value = "Full", Text = "Full Rest Day"},
                                            new SelectListItem { Value = "Half", Text = "Half Rest Day"}
                                                            }, "Value", "Text");
        }

        public static IEnumerable<SelectListItem> GetAllSkills()
        {
            return new SelectList(new List<SelectListItem> {
                                            new SelectListItem { Value = "Basic", Text = "Basic Skilled"},
                                            new SelectListItem { Value = "High", Text = "High Skilled"}
                                                            }, "Value", "Text");
        }

        public static IEnumerable<SelectListItem> GetWorkTypes()
        {
            return new SelectList(new List<SelectListItem> {
                                            new SelectListItem { Value = "Workman", Text = "Workman"},
                                            new SelectListItem { Value = "NonWorkman", Text = "Non-Workman"}
                                                            }, "Value", "Text");
        }

        public static IEnumerable<SelectListItem> GetLicenceTypes()
        {
            return new SelectList(new List<SelectListItem> {
                                            new SelectListItem { Value = "Class-3A", Text = "Class 3A"},
                                            new SelectListItem { Value = "Class-3", Text = "Class 3"},
                                            new SelectListItem { Value = "Class-4", Text = "Class 4"},
                                            new SelectListItem { Value = "Class-5", Text = "Class 5"},
                                            new SelectListItem { Value = "Nill", Text = "Nill"}
                                                            }, "Value", "Text");
        }

        public static IEnumerable<SelectListItem> GetDonationTypes()
        {
            return new SelectList(new List<SelectListItem> {
                                            new SelectListItem { Value = "CDAC", Text = "CDAC"},
                                            new SelectListItem { Value = "ECF", Text = "ECF"},
                                            new SelectListItem { Value = "MB", Text = "MB"},
                                            new SelectListItem { Value = "MF", Text = "MF"},
                                            new SelectListItem { Value = "SINDA", Text = "SINDA"}
                                                            }, "Value", "Text");
        }

        public static IEnumerable<SelectListItem> GetPaymentStatus()
        {
            return new SelectList(new List<SelectListItem> {
                                            new SelectListItem { Value = "Paid", Text = "Paid"},
                                            new SelectListItem { Value = "UnPaid", Text = "UnPaid"},
                                            new SelectListItem { Value = "OutStanding", Text = "OutStanding"}
                                                            }, "Value", "Text");
        }

        public static List<string> GetPayrollStatus()
        {
            return new List<string> { "OutStanding", "Paid", "UnPaid" };
        }

        public static IEnumerable<SelectListItem> GetGender()
        {
            return new SelectList(new List<SelectListItem> {
                                            new SelectListItem { Value = "Male", Text = "Male"},
                                            new SelectListItem { Value = "Female", Text = "Female"}
                                                            }, "Value", "Text");
        }

        public static IEnumerable<SelectListItem> GetReligion()
        {
            return new SelectList(new List<SelectListItem> {
                                    new SelectListItem { Value = "Christianity", Text = "Christianity"},
                                    new SelectListItem { Value = "Muslim", Text = "Muslim"},
                                    new SelectListItem { Value = "Hinduism", Text = "Hinduism"},
                                    new SelectListItem { Value = "Buddhism", Text = "Buddhism"},
                                    new SelectListItem { Value = "Sikhism", Text = "Sikhism"},
                                    new SelectListItem {Value="Judaism", Text="Judaism" },
                                    new SelectListItem {Value="Jainism", Text="Jainism" }
                                                         }, "Value", "Text");
        }

        public static IEnumerable<SelectListItem> GetNationality()
        {
            return new SelectList(new List<SelectListItem> {
                                    new SelectListItem { Value = "Chinese", Text = "Chinese"},
                                    new SelectListItem { Value = "Eurasian", Text = "Eurasian"},
                                    new SelectListItem { Value = "Indian", Text = "Indian"},
                                    new SelectListItem { Value = "Singapore", Text = "Singapore"},
                                                            }, "Value", "Text");
        }

        public static IEnumerable<SelectListItem> GetCountry()
        {
            return new SelectList(new List<SelectListItem> {
                                    new SelectListItem { Value ="AF", Text ="Afghanistan"},
                                    new SelectListItem { Value ="AX", Text ="Åland Islands"},
                                    new SelectListItem { Value ="AL", Text ="Albania"},
                                    new SelectListItem { Value ="DZ", Text ="Algeria"},
                                    new SelectListItem { Value ="AS", Text ="American Samoa"},
                                    new SelectListItem { Value ="AD", Text ="Andorra"},
                                    new SelectListItem { Value ="AO", Text ="Angola"},
                                    new SelectListItem { Value ="AI", Text ="Anguilla"},
                                    new SelectListItem { Value ="AQ", Text ="Antarctica"},
                                    new SelectListItem { Value ="AG", Text ="Antigua and Barbuda"},
                                    new SelectListItem { Value ="AR", Text ="Argentina"},
                                    new SelectListItem { Value ="AM", Text ="Armenia"},
                                    new SelectListItem { Value ="AW", Text ="Aruba"},
                                    new SelectListItem { Value ="AU", Text ="Australia"},
                                    new SelectListItem { Value ="AT", Text ="Austria"},
                                    new SelectListItem { Value ="AZ", Text ="Azerbaijan"},
                                    new SelectListItem { Value ="BS", Text ="Bahamas"},
                                    new SelectListItem { Value ="BH", Text ="Bahrain"},
                                    new SelectListItem { Value ="BD", Text ="Bangladesh"},
                                    new SelectListItem { Value ="BB", Text ="Barbados"},
                                    new SelectListItem { Value ="BY", Text ="Belarus"},
                                    new SelectListItem { Value ="BE", Text ="Belgium"},
                                    new SelectListItem { Value ="BZ", Text ="Belize"},
                                    new SelectListItem { Value ="BJ", Text ="Benin"},
                                    new SelectListItem { Value ="BM", Text ="Bermuda"},
                                    new SelectListItem { Value ="BT", Text ="Bhutan"},
                                    new SelectListItem { Value ="BO", Text ="Bolivia, Plurinational State of"},
                                    new SelectListItem { Value ="BQ", Text ="Bonaire, Sint Eustatius and Saba"},
                                    new SelectListItem { Value ="BA", Text ="Bosnia and Herzegovina"},
                                    new SelectListItem { Value ="BW", Text ="Botswana"},
                                    new SelectListItem { Value ="BV", Text ="Bouvet Island"},
                                    new SelectListItem { Value ="BR", Text ="Brazil"},
                                    new SelectListItem { Value ="IO", Text ="British Indian Ocean Territory"},
                                    new SelectListItem { Value ="BN", Text ="Brunei Darussalam"},
                                    new SelectListItem { Value ="BG", Text ="Bulgaria"},
                                    new SelectListItem { Value ="BF", Text ="Burkina Faso"},
                                    new SelectListItem { Value ="BI", Text ="Burundi"},
                                    new SelectListItem { Value ="KH", Text ="Cambodia"},
                                    new SelectListItem { Value ="CM", Text ="Cameroon"},
                                    new SelectListItem { Value ="CA", Text ="Canada"},
                                    new SelectListItem { Value ="CV", Text ="Cape Verde"},
                                    new SelectListItem { Value ="KY", Text ="Cayman Islands"},
                                    new SelectListItem { Value ="CF", Text ="Central African Republic"},
                                    new SelectListItem { Value ="TD", Text ="Chad"},
                                    new SelectListItem { Value ="CL", Text ="Chile"},
                                    new SelectListItem { Value ="CN", Text ="China"},
                                    new SelectListItem { Value ="CX", Text ="Christmas Island"},
                                    new SelectListItem { Value ="CC", Text ="Cocos (Keeling) Islands"},
                                    new SelectListItem { Value ="CO", Text ="Colombia"},
                                    new SelectListItem { Value ="KM", Text ="Comoros"},
                                    new SelectListItem { Value ="CG", Text ="Congo"},
                                    new SelectListItem { Value ="CD", Text ="Congo, the Democratic Republic of the"},
                                    new SelectListItem { Value ="CK", Text ="Cook Islands"},
                                    new SelectListItem { Value ="CR", Text ="Costa Rica"},
                                    new SelectListItem { Value ="CI", Text ="Côte d'Ivoire"},
                                    new SelectListItem { Value ="HR", Text ="Croatia"},
                                    new SelectListItem { Value ="CU", Text ="Cuba"},
                                    new SelectListItem { Value ="CW", Text ="Curaçao"},
                                    new SelectListItem { Value ="CY", Text ="Cyprus"},
                                    new SelectListItem { Value ="CZ", Text ="Czech Republic"},
                                    new SelectListItem { Value ="DK", Text ="Denmark"},
                                    new SelectListItem { Value ="DJ", Text ="Djibouti"},
                                    new SelectListItem { Value ="DM", Text ="Dominica"},
                                    new SelectListItem { Value ="DO", Text ="Dominican Republic"},
                                    new SelectListItem { Value ="EC", Text ="Ecuador"},
                                    new SelectListItem { Value ="EG", Text ="Egypt"},
                                    new SelectListItem { Value ="SV", Text ="El Salvador"},
                                    new SelectListItem { Value ="GQ", Text ="Equatorial Guinea"},
                                    new SelectListItem { Value ="ER", Text ="Eritrea"},
                                    new SelectListItem { Value ="EE", Text ="Estonia"},
                                    new SelectListItem { Value ="ET", Text ="Ethiopia"},
                                    new SelectListItem { Value ="FK", Text ="Falkland Islands (Malvinas)"},
                                    new SelectListItem { Value ="FO", Text ="Faroe Islands"},
                                    new SelectListItem { Value ="FJ", Text ="Fiji"},
                                    new SelectListItem { Value ="FI", Text ="Finland"},
                                    new SelectListItem { Value ="FR", Text ="France"},
                                    new SelectListItem { Value ="GF", Text ="French Guiana"},
                                    new SelectListItem { Value ="PF", Text ="French Polynesia"},
                                    new SelectListItem { Value ="TF", Text ="French Southern Territories"},
                                    new SelectListItem { Value ="GA", Text ="Gabon"},
                                    new SelectListItem { Value ="GM", Text ="Gambia"},
                                    new SelectListItem { Value ="GE", Text ="Georgia"},
                                    new SelectListItem { Value ="DE", Text ="Germany"},
                                    new SelectListItem { Value ="GH", Text ="Ghana"},
                                    new SelectListItem { Value ="GI", Text ="Gibraltar"},
                                    new SelectListItem { Value ="GR", Text ="Greece"},
                                    new SelectListItem { Value ="GL", Text ="Greenland"},
                                    new SelectListItem { Value ="GD", Text ="Grenada"},
                                    new SelectListItem { Value ="GP", Text ="Guadeloupe"},
                                    new SelectListItem { Value ="GU", Text ="Guam"},
                                    new SelectListItem { Value ="GT", Text ="Guatemala"},
                                    new SelectListItem { Value ="GG", Text ="Guernsey"},
                                    new SelectListItem { Value ="GN", Text ="Guinea"},
                                    new SelectListItem { Value ="GW", Text ="Guinea-Bissau"},
                                    new SelectListItem { Value ="GY", Text ="Guyana"},
                                    new SelectListItem { Value ="HT", Text ="Haiti"},
                                    new SelectListItem { Value ="HM", Text ="Heard Island and McDonald Islands"},
                                    new SelectListItem { Value ="VA", Text ="Holy See (Vatican City State)"},
                                    new SelectListItem { Value ="HN", Text ="Honduras"},
                                    new SelectListItem { Value ="HK", Text ="Hong Kong"},
                                    new SelectListItem { Value ="HU", Text ="Hungary"},
                                    new SelectListItem { Value ="IS", Text ="Iceland"},
                                    new SelectListItem { Value ="IN", Text ="India"},
                                    new SelectListItem { Value ="ID", Text ="Indonesia"},
                                    new SelectListItem { Value ="IR", Text ="Iran, Islamic Republic of"},
                                    new SelectListItem { Value ="IQ", Text ="Iraq"},
                                    new SelectListItem { Value ="IE", Text ="Ireland"},
                                    new SelectListItem { Value ="IM", Text ="Isle of Man"},
                                    new SelectListItem { Value ="IL", Text ="Israel"},
                                    new SelectListItem { Value ="IT", Text ="Italy"},
                                    new SelectListItem { Value ="JM", Text ="Jamaica"},
                                    new SelectListItem { Value ="JP", Text ="Japan"},
                                    new SelectListItem { Value ="JE", Text ="Jersey"},
                                    new SelectListItem { Value ="JO", Text ="Jordan"},
                                    new SelectListItem { Value ="KZ", Text ="Kazakhstan"},
                                    new SelectListItem { Value ="KE", Text ="Kenya"},
                                    new SelectListItem { Value ="KI", Text ="Kiribati"},
                                    new SelectListItem { Value ="KP", Text ="Korea, Democratic People's Republic of"},
                                    new SelectListItem { Value ="KR", Text ="Korea, Republic of"},
                                    new SelectListItem { Value ="KW", Text ="Kuwait"},
                                    new SelectListItem { Value ="KG", Text ="Kyrgyzstan"},
                                    new SelectListItem { Value ="LA", Text ="Lao People's Democratic Republic"},
                                    new SelectListItem { Value ="LV", Text ="Latvia"},
                                    new SelectListItem { Value ="LB", Text ="Lebanon"},
                                    new SelectListItem { Value ="LS", Text ="Lesotho"},
                                    new SelectListItem { Value ="LR", Text ="Liberia"},
                                    new SelectListItem { Value ="LY", Text ="Libya"},
                                    new SelectListItem { Value ="LI", Text ="Liechtenstein"},
                                    new SelectListItem { Value ="LT", Text ="Lithuania"},
                                    new SelectListItem { Value ="LU", Text ="Luxembourg"},
                                    new SelectListItem { Value ="MO", Text ="Macao"},
                                    new SelectListItem { Value ="MK", Text ="Macedonia, the former Yugoslav Republic of"},
                                    new SelectListItem { Value ="MG", Text ="Madagascar"},
                                    new SelectListItem { Value ="MW", Text ="Malawi"},
                                    new SelectListItem { Value ="MY", Text ="Malaysia"},
                                    new SelectListItem { Value ="MV", Text ="Maldives"},
                                    new SelectListItem { Value ="ML", Text ="Mali"},
                                    new SelectListItem { Value ="MT", Text ="Malta"},
                                    new SelectListItem { Value ="MH", Text ="Marshall Islands"},
                                    new SelectListItem { Value ="MQ", Text ="Martinique"},
                                    new SelectListItem { Value ="MR", Text ="Mauritania"},
                                    new SelectListItem { Value ="MU", Text ="Mauritius"},
                                    new SelectListItem { Value ="YT", Text ="Mayotte"},
                                    new SelectListItem { Value ="MX", Text ="Mexico"},
                                    new SelectListItem { Value ="FM", Text ="Micronesia, Federated States of"},
                                    new SelectListItem { Value ="MD", Text ="Moldova, Republic of"},
                                    new SelectListItem { Value ="MC", Text ="Monaco"},
                                    new SelectListItem { Value ="MN", Text ="Mongolia"},
                                    new SelectListItem { Value ="ME", Text ="Montenegro"},
                                    new SelectListItem { Value ="MS", Text ="Montserrat"},
                                    new SelectListItem { Value ="MA", Text ="Morocco"},
                                    new SelectListItem { Value ="MZ", Text ="Mozambique"},
                                    new SelectListItem { Value ="MM", Text ="Myanmar"},
                                    new SelectListItem { Value ="NA", Text ="Namibia"},
                                    new SelectListItem { Value ="NR", Text ="Nauru"},
                                    new SelectListItem { Value ="NP", Text ="Nepal"},
                                    new SelectListItem { Value ="NL", Text ="Netherlands"},
                                    new SelectListItem { Value ="NC", Text ="New Caledonia"},
                                    new SelectListItem { Value ="NZ", Text ="New Zealand"},
                                    new SelectListItem { Value ="NI", Text ="Nicaragua"},
                                    new SelectListItem { Value ="NE", Text ="Niger"},
                                    new SelectListItem { Value ="NG", Text ="Nigeria"},
                                    new SelectListItem { Value ="NU", Text ="Niue"},
                                    new SelectListItem { Value ="NF", Text ="Norfolk Island"},
                                    new SelectListItem { Value ="MP", Text ="Northern Mariana Islands"},
                                    new SelectListItem { Value ="NO", Text ="Norway"},
                                    new SelectListItem { Value ="OM", Text ="Oman"},
                                    new SelectListItem { Value ="PK", Text ="Pakistan"},
                                    new SelectListItem { Value ="PW", Text ="Palau"},
                                    new SelectListItem { Value ="PS", Text ="Palestinian Territory, Occupied"},
                                    new SelectListItem { Value ="PA", Text ="Panama"},
                                    new SelectListItem { Value ="PG", Text ="Papua New Guinea"},
                                    new SelectListItem { Value ="PY", Text ="Paraguay"},
                                    new SelectListItem { Value ="PE", Text ="Peru"},
                                    new SelectListItem { Value ="PH", Text ="Philippines"},
                                    new SelectListItem { Value ="PN", Text ="Pitcairn"},
                                    new SelectListItem { Value ="PL", Text ="Poland"},
                                    new SelectListItem { Value ="PT", Text ="Portugal"},
                                    new SelectListItem { Value ="PR", Text ="Puerto Rico"},
                                    new SelectListItem { Value ="QA", Text ="Qatar"},
                                    new SelectListItem { Value ="RE", Text ="Réunion"},
                                    new SelectListItem { Value ="RO", Text ="Romania"},
                                    new SelectListItem { Value ="RU", Text ="Russian Federation"},
                                    new SelectListItem { Value ="RW", Text ="Rwanda"},
                                    new SelectListItem { Value ="BL", Text ="Saint Barthélemy"},
                                    new SelectListItem { Value ="SH", Text ="Saint Helena, Ascension and Tristan da Cunha"},
                                    new SelectListItem { Value ="KN", Text ="Saint Kitts and Nevis"},
                                    new SelectListItem { Value ="LC", Text ="Saint Lucia"},
                                    new SelectListItem { Value ="MF", Text ="Saint Martin (French part)"},
                                    new SelectListItem { Value ="PM", Text ="Saint Pierre and Miquelon"},
                                    new SelectListItem { Value ="VC", Text ="Saint Vincent and the Grenadines"},
                                    new SelectListItem { Value ="WS", Text ="Samoa"},
                                    new SelectListItem { Value ="SM", Text ="San Marino"},
                                    new SelectListItem { Value ="ST", Text ="Sao Tome and Principe"},
                                    new SelectListItem { Value ="SA", Text ="Saudi Arabia"},
                                    new SelectListItem { Value ="SN", Text ="Senegal"},
                                    new SelectListItem { Value ="RS", Text ="Serbia"},
                                    new SelectListItem { Value ="SC", Text ="Seychelles"},
                                    new SelectListItem { Value ="SL", Text ="Sierra Leone"},
                                    new SelectListItem { Value ="SG", Text ="Singapore"},
                                    new SelectListItem { Value ="SX", Text ="Sint Maarten (Dutch part)"},
                                    new SelectListItem { Value ="SK", Text ="Slovakia"},
                                    new SelectListItem { Value ="SI", Text ="Slovenia"},
                                    new SelectListItem { Value ="SB", Text ="Solomon Islands"},
                                    new SelectListItem { Value ="SO", Text ="Somalia"},
                                    new SelectListItem { Value ="ZA", Text ="South Africa"},
                                    new SelectListItem { Value ="GS", Text ="South Georgia and the South Sandwich Islands"},
                                    new SelectListItem { Value ="SS", Text ="South Sudan"},
                                    new SelectListItem { Value ="ES", Text ="Spain"},
                                    new SelectListItem { Value ="LK", Text ="Sri Lanka"},
                                    new SelectListItem { Value ="SD", Text ="Sudan"},
                                    new SelectListItem { Value ="SR", Text ="Suriname"},
                                    new SelectListItem { Value ="SJ", Text ="Svalbard and Jan Mayen"},
                                    new SelectListItem { Value ="SZ", Text ="Swaziland"},
                                    new SelectListItem { Value ="SE", Text ="Sweden"},
                                    new SelectListItem { Value ="CH", Text ="Switzerland"},
                                    new SelectListItem { Value ="SY", Text ="Syrian Arab Republic"},
                                    new SelectListItem { Value ="TW", Text ="Taiwan, Province of China"},
                                    new SelectListItem { Value ="TJ", Text ="Tajikistan"},
                                    new SelectListItem { Value ="TZ", Text ="Tanzania, United Republic of"},
                                    new SelectListItem { Value ="TH", Text ="Thailand"},
                                    new SelectListItem { Value ="TL", Text ="Timor-Leste"},
                                    new SelectListItem { Value ="TG", Text ="Togo"},
                                    new SelectListItem { Value ="TK", Text ="Tokelau"},
                                    new SelectListItem { Value ="TO", Text ="Tonga"},
                                    new SelectListItem { Value ="TT", Text ="Trinidad and Tobago"},
                                    new SelectListItem { Value ="TN", Text ="Tunisia"},
                                    new SelectListItem { Value ="TR", Text ="Turkey"},
                                    new SelectListItem { Value ="TM", Text ="Turkmenistan"},
                                    new SelectListItem { Value ="TC", Text ="Turks and Caicos Islands"},
                                    new SelectListItem { Value ="TV", Text ="Tuvalu"},
                                    new SelectListItem { Value ="UG", Text ="Uganda"},
                                    new SelectListItem { Value ="UA", Text ="Ukraine"},
                                    new SelectListItem { Value ="AE", Text ="United Arab Emirates"},
                                    new SelectListItem { Value ="GB", Text ="United Kingdom"},
                                    new SelectListItem { Value ="US", Text ="United States"},
                                    new SelectListItem { Value ="UM", Text ="United States Minor Outlying Islands"},
                                    new SelectListItem { Value ="UY", Text ="Uruguay"},
                                    new SelectListItem { Value ="UZ", Text ="Uzbekistan"},
                                    new SelectListItem { Value ="VU", Text ="Vanuatu"},
                                    new SelectListItem { Value ="VE", Text ="Venezuela, Bolivarian Republic of"},
                                    new SelectListItem { Value ="VN", Text ="Viet Nam"},
                                    new SelectListItem { Value ="VG", Text ="Virgin Islands, British"},
                                    new SelectListItem { Value ="VI", Text ="Virgin Islands, U.S."},
                                    new SelectListItem { Value ="WF", Text ="Wallis and Futuna"},
                                    new SelectListItem { Value ="EH", Text ="Western Sahara"},
                                    new SelectListItem { Value ="YE", Text ="Yemen"},
                                    new SelectListItem { Value ="ZM", Text ="Zambia"},
                                    new SelectListItem { Value ="ZW", Text ="Zimbabwe"} }, "Value", "Text");
        }

        public static IEnumerable<SelectListItem> GetCurrency()
        {
            return new SelectList(new List<SelectListItem> {
                                                      new SelectListItem { Value = "1", Text = "AFN"},
                                                      new SelectListItem { Value = "2", Text = "ALL"},
                                                      new SelectListItem { Value = "3", Text = "DZD"},
                                                      new SelectListItem { Value = "4", Text = "USD"},
                                                      new SelectListItem { Value = "5", Text = "EUR"},
                                                      new SelectListItem { Value = "6", Text = "AOA"},
                                                      new SelectListItem { Value = "7", Text = "XCD"},
                                                      new SelectListItem { Value = "8", Text = "ARS"},
                                                      new SelectListItem { Value = "9", Text = "AMD"},
                                                      new SelectListItem { Value = "10", Text = "AWG"},
                                                      new SelectListItem { Value = "11", Text = "AUD"},
                                                      new SelectListItem { Value = "12", Text = "AZN"},
                                                      new SelectListItem { Value = "13", Text = "CAD"},
                                                      new SelectListItem { Value = "14", Text = "CNY"},
                                                      new SelectListItem { Value = "15", Text = "INR"},
                                                      new SelectListItem { Value = "16", Text = "JPY"},
                                                      new SelectListItem { Value = "17", Text = "MYR"},
                                                      new SelectListItem { Value = "18", Text = "MXN"},
                                                      new SelectListItem { Value = "19", Text = "SGD"}}, "Value", "Text");
        }

        public static IEnumerable<SelectListItem> GetMaritalstatus()
        {
            return new SelectList(new List<SelectListItem> {
                                        new SelectListItem { Value = "Single", Text = "Single"},
                                        new SelectListItem { Value = "Married", Text = "Married"},
                                        new SelectListItem { Value = "Divorced", Text = "Divorced"},
                                        new SelectListItem { Value = "Widowed", Text = "Widowed"}
                                                        }, "Value", "Text");
        }

        public static IEnumerable<SelectListItem> GetEmployeeStatus()
        {
            return new SelectList(new List<SelectListItem> {
                                                        new SelectListItem { Value = "SingaporeCitizen", Text = "Singapore Citizen"},
                                                        new SelectListItem { Value = "SingaporePR", Text = "Singapore PR"},
                                                        new SelectListItem { Value = "EmploymentPass", Text = "Employment Pass"},
                                                        new SelectListItem { Value = "SPass", Text = "S Pass"},
                                                        new SelectListItem { Value = "LTVP", Text = "LTVP"},
                                                        new SelectListItem { Value = "WorkPermit", Text = "Work Permit"},
                                                        new SelectListItem { Value = "LOC", Text = "LOC"},
                                                        new SelectListItem { Value = "OtherPass", Text = "Other Pass"}
                                                            }, "Value", "Text");
        }

        public static IEnumerable<SelectListItem> GetLevyEmployeeStatus()
        {
            return new SelectList(new List<SelectListItem> {
                                                        new SelectListItem { Value = "SPass", Text = "S Pass"},
                                                        new SelectListItem { Value = "WorkPermit", Text = "Work Permit"}
                                                               }, "Value", "Text");
        }

        public static IEnumerable<SelectListItem> GetBankNames()
        {
            return new SelectList(new List<SelectListItem> {
                                            new SelectListItem { Value = "Other", Text ="Other"},
                                            new SelectListItem { Value = "Oversea-Chinese Banking Corporation (OCBC)", Text ="Oversea-Chinese Banking Corporation (OCBC)"},
                                            new SelectListItem { Value = "DBS Bank Limited", Text ="DBS Bank Limited"},
                                            new SelectListItem { Value = "POSB Bank", Text ="POSB Bank"},
                                            new SelectListItem { Value = "United Overseas Bank Limited (UOB)", Text ="United Overseas Bank Limited (UOB)"},
                                            new SelectListItem { Value = "Citibank Singapore", Text ="Citibank Singapore"},
                                            new SelectListItem { Value = "Maybank Singapore", Text ="Maybank Singapore"},
                                            new SelectListItem { Value = "Standard Chartered Singapore", Text ="Standard Chartered Singapore"},
                                            new SelectListItem { Value = "SBI Singapore", Text ="SBI Singapore"},
                                            new SelectListItem { Value = "Bangkok bank Singapore", Text ="Bangkok bank Singapore"},
                                            new SelectListItem { Value = "CIMB Bank Singapore", Text ="CIMB Bank Singapore"},
                                            new SelectListItem { Value = "ICICI Singapore Bank", Text ="ICICI Singapore Bank"},
                                            new SelectListItem { Value = "RHB Singapore Bank", Text ="RHB Singapore Bank"},
                                            new SelectListItem { Value = "Indian Overseas Bank Singapore", Text ="Indian Overseas Bank Singapore"},
                                            new SelectListItem { Value = "ANZ Singapore", Text ="ANZ Singapore"},
                                            new SelectListItem { Value = "J.P. Morgan Singapore", Text ="J.P. Morgan Singapore"},
                                            new SelectListItem { Value = "Hong Leong Finance", Text ="Hong Leong Finance"},
                                            new SelectListItem { Value = "Bank of Singapore", Text ="Bank of Singapore"},
                                            new SelectListItem { Value = "Islamic Bank of Asia", Text ="Islamic Bank of Asia"}
                                                                                       }, "Value", "Text").OrderBy(i => i.Text);
        }

        public static IEnumerable<SelectListItem> GetStatus()
        {
            return new SelectList(new List<SelectListItem> {
                                            new SelectListItem { Value = "true", Text = "Yes"},
                                            new SelectListItem { Value = "false", Text = "No"}
                                                                                       }, "Value", "Text");
        }

        public static IEnumerable<SelectListItem> GetGSTStatus()
        {
            return new SelectList(new List<SelectListItem> {
                    new SelectListItem { Value = "E", Text = "Exclusive GST"},
                                            new SelectListItem { Value = "I", Text = "Inclusive GST"},
                                            new SelectListItem { Value = "N", Text = "No GST"}

                                                                                       }, "Value", "Text");
        }

        public static IEnumerable<SelectListItem> GetStages()
        {
            return new SelectList(new List<SelectListItem> {
                                            new SelectListItem { Value = "Draft", Text = "Draft"},
                                            new SelectListItem { Value = "Confirmed", Text = "Confirmed"}
                                                                                       }, "Value", "Text");
        }

        public static IEnumerable<SelectListItem> GetProjectStatus()
        {
            return new SelectList(new List<SelectListItem> {
                                            new SelectListItem { Value = "Draft", Text = "Draft"},
                                            new SelectListItem { Value = "Completed", Text = "Completed"},
                                            new SelectListItem { Value = "Ongoing", Text = "Ongoing"}
                                                                                       }, "Value", "Text");
        }

        public static IEnumerable<SelectListItem> GetUserGroup()
        {
            return new SelectList(new List<SelectListItem> {
                                                    new SelectListItem { Value = "Admin", Text = "Admin"},
                                                    new SelectListItem { Value = "Employee", Text = "Employee"}
                                            }, "Value", "Text");
        }

        public static IEnumerable<SelectListItem> GetClaimStatus()
        {
            return new SelectList(new List<SelectListItem> {
                                                    new SelectListItem { Value = "Submitted", Text = "Submitted"},
                                                    new SelectListItem { Value = "Approved", Text = "Approved"}
                                            }, "Value", "Text");
        }

        public static IEnumerable<SelectListItem> GetAllSupplier()
        {
            List<SelectListItem> supplierList = new List<SelectListItem>();
            supplierList = SupplierInterface.Read()
                                          .Select(x => new SelectListItem()
                                          {
                                              Value = x.Id.ToString(),
                                              Text = x.SupplierName
                                          })
                                          .OrderBy(x => Convert.ToInt32(x.Value))
                                          .ToList();
            return supplierList;
        }


    }
}
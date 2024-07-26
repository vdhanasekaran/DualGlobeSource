using System.Web.Mvc;
using Library.DualGlobe.ERP.Interfaces;
using Library.DualGlobe.ERP.Models;
using DualGlobe.ERP.Models;
using System.Collections.Generic;
using System.Linq;
using System;
using DualGlobe.ERP.Utility;

namespace DualGlobe.ERP.Controllers
{
    public class ReportController : BaseController
    {
        #region Private Const
        private const string _projectExpense = "Project";
        private const string _operationExpense = "Operation";
        private const string _accountingAndLegalFee = "Accounting and legal fee";
        private const string _advertising = "Advertising";
        private const string _utilitiy = "Utilities";
        private const string _insurance = "Insurance";
        private const string _interestAndBankCharge = "Interest and bank charges";
        private const string _postage = "Postage";
        private const string _printingAndStationary = "Printing and Stationary";
        private const string _professionalMembership = "Professional memberships";
        private const string _premiseRent = "Rent for premises";
        private const string _repairAndMaintenance = "Repair and maintenance";
        private const string _training = "Training";
        private const string _vehicleOperatingCost = "Vehicle operating costs";
        private const string _workerCompensation = "Worker compensation";
        private const string _depreciation = "Depreciation";
        private const string _other = "Other"; 
        #endregion

        // GET: Client
        public ActionResult ClientReport(string clientId, string projectStatus)
        {
            List<ClientReportModel> clientReportModelList = new List<ClientReportModel>();
            if (!string.IsNullOrEmpty(clientId))
            {
                var client = ClientInterface.Read(Convert.ToInt32(clientId));

                var clientRecord = GetClient(projectStatus, client);
                if (clientRecord != null)
                {
                    clientReportModelList.Add(clientRecord);
                }
            }
            else
            {
                var clients = ClientInterface.Read();
                foreach (var client in clients)
                {
                    var clientRecord = GetClient(projectStatus, client);
                    if (clientRecord != null)
                    {
                        clientReportModelList.Add(clientRecord);
                    }
                }
            }

            ViewBag.CientId = clientId;
            ViewBag.ProjectStatus = projectStatus;

            return View(clientReportModelList);
        }

        public ActionResult ProjectReport(string clientId, string projectId, string projectStatus)
        {
            List<ProjectReportModel> projectReportModelList = new List<ProjectReportModel>();
            if (!string.IsNullOrEmpty(projectId))
            {
                var project = ProjectInterface.Read(Convert.ToInt32(projectId));

                var projectRecord = GetProject(project);
                if (projectRecord != null)
                {
                    projectReportModelList.Add(projectRecord);
                }
            }
            else if (!string.IsNullOrEmpty(clientId))
            {
                var projects = ProjectInterface.ReadByClientId(Convert.ToInt32(clientId));
                if (projects != null && projects.Length > 0)
                {
                    if (!string.IsNullOrEmpty(projectStatus))
                    {
                        projects = projects.Where(s => s.Status == projectStatus).ToArray();
                    }
                    foreach (var project in projects)
                    {
                        var projectRecord = GetProject(project);
                        if (projectRecord != null)
                        {
                            projectReportModelList.Add(projectRecord);
                        }
                    }
                }
            }
            else
            {
                var projects = ProjectInterface.Read();
                if (projects != null && projects.Length > 0)
                {
                    if (!string.IsNullOrEmpty(projectStatus))
                    {
                        projects = projects.Where(s => s.Status == projectStatus).ToArray();
                    }
                    foreach (var project in projects)
                    {
                        var projectRecord = GetProject(project);
                        if (projectRecord != null)
                        {
                            projectReportModelList.Add(projectRecord);
                        }
                    }
                }
            }

            ViewBag.ProjectId = projectId;
            ViewBag.CientId = clientId;
            ViewBag.ProjectStatus = projectStatus;

            return View(projectReportModelList);
        }

        public ActionResult ProjectEmployeeReport(string projectId)
        {
            EmployeeModel model = new EmployeeModel();
            List<Employee> employeeList = new List<Employee>();
            if (!string.IsNullOrEmpty(projectId))
            {
                var conf = EmployeeProjectInterface.ReadByProject(Convert.ToInt32(projectId));
                foreach (var item in conf)
                {
                    var employee = EmployeeInterface.Read(item.EmployeeId);
                    employeeList.Add(employee);
                }
                model.employeeList = employeeList.ToArray();
            }
            else
            {
                model.employeeList = EmployeeInterface.Read();
            }

            ViewBag.ProjectId = projectId;

            return View(model);
        }

        public ActionResult EmployeeReport(EmployeeModel model)
        {
            if (model != null)
            {
                if (!string.IsNullOrEmpty(model.WorkStatus))
                {
                    model.employeeList = EmployeeInterface.ReadByWorkStatus(model.WorkStatus);
                }
                else
                {
                    model.employeeList = EmployeeInterface.Read();
                }
            }
            return View(model);
        }

        public ActionResult AttendanceReport(TimesheetModel model)
        {
            if (model != null && model.StartDate != null && model.EndDate != null)
            {
                if (model.ProjectId != null && model.ProjectId > 0)
                {
                    model.timesheetArray = TimesheetInterface.ReadByProjectStartDateEndDate(model.StartDate.Value, model.EndDate.Value, model.ProjectId.Value);
                }
                else if (model.ClientId != null && model.ClientId > 0)
                {
                    model.timesheetArray = TimesheetInterface.ReadByClientStartDateEndDate(model.StartDate.Value, model.EndDate.Value, model.ClientId.Value);
                }
                else
                {
                    model.timesheetArray = TimesheetInterface.ReadByStartDateEndDate(model.StartDate.Value, model.EndDate.Value);
                }

                if (!string.IsNullOrEmpty(model.EmployeeName))
                {
                    model.timesheetArray = model.timesheetArray.Where(i => i.EmployeeId == Convert.ToInt32(model.EmployeeName)).ToArray();
                }
                model.employeeList = DropdownBuilder.GetAllEmployeeClientProjects(model.ClientId, model.ProjectId);

                model.projectList = DropdownBuilder.GetAllClientProjects(model.ClientId.GetValueOrDefault());
            }
            else
            {
                model = new TimesheetModel();
            }
            return View(model);
        }

        public ActionResult SalaryReport(EmployeeModel model)
        {
            if (model != null && model.StartDate != null && model.EndDate != null)
            {
                if (!string.IsNullOrEmpty(model.WorkStatus))
                {
                    model.SalaryArray = SalaryDetailInterface.ReadByWorkStatus(model.StartDate.Value, model.EndDate.Value, model.WorkStatus);
                }
                else
                {
                    model.SalaryArray = SalaryDetailInterface.ReadByStartDateEndDate(model.StartDate.Value, model.EndDate.Value);
                }
            }
            else
            {
                model = new EmployeeModel();
            }
            return View(model);
        }

        public ActionResult InvoiceGSTReport(InvoiceModel model)
        {
            if (model != null && model.StartDate != null && model.EndDate != null)
            {
                model.InvoiceArray = InvoiceInterface.ReadByStartDateEndDate(model.StartDate.Value, model.EndDate.Value);
                if (!string.IsNullOrEmpty(model.SelectedPaymentStatus))
                    model.InvoiceArray = model.InvoiceArray.Where(i => i.Status == model.SelectedPaymentStatus).ToArray();
                if (!string.IsNullOrEmpty(model.SelectedClient))
                    model.InvoiceArray = model.InvoiceArray.Where(i => i.ClientId == Convert.ToInt32(model.SelectedClient)).ToArray();
                if (!string.IsNullOrEmpty(model.SelectedProject))
                    model.InvoiceArray = model.InvoiceArray.Where(i => i.ProjectId == Convert.ToInt32(model.SelectedProject)).ToArray();
            }
            else
            {
                model = new InvoiceModel();
            }
            return View(model);
        }
        public ActionResult PurchaseGSTReport(ExpenseModel model)
        {
            if (model != null && model.StartDate != null && model.EndDate != null)
            {
                model.expenseArray = ExpenseInterface.ReadByStartDateEndDate(model.StartDate.Value, model.EndDate.Value);
                if (!string.IsNullOrEmpty(model.SelectedFilter))
                {
                    if (model.SelectedFilter == "true")
                        model.expenseArray = model.expenseArray.Where(i => i.IncludeGST == true).ToArray();
                    else
                        model.expenseArray = model.expenseArray.Where(i => i.IncludeGST == false).ToArray();
                }
                if (!string.IsNullOrEmpty(model.SelectedPaymentStatus))
                {
                    model.expenseArray = model.expenseArray.Where(i => i.PaymentStatus == model.SelectedPaymentStatus).ToArray();
                }
                if (!string.IsNullOrEmpty(model.SelectedSupplier))
                {
                    model.expenseArray = model.expenseArray.Where(i => i.Supplier == model.SelectedSupplier).ToArray();
                }
            }
            else
            {
                model = new ExpenseModel();
            }
            return View(model);
        }

        public ActionResult PrintInvoiceGSTReport(DateTime? startDate, DateTime? endDate, string paymentStatus, string client, string project)
        {
            InvoiceModel model = new InvoiceModel();
            if (startDate != null && endDate != null)
            {
                model.InvoiceArray = InvoiceInterface.ReadByStartDateEndDate(startDate.Value, endDate.Value);
                if (!string.IsNullOrEmpty(paymentStatus))
                    model.InvoiceArray = model.InvoiceArray.Where(i => i.Payments.Any()).ToArray();
                if (!string.IsNullOrEmpty(client))
                    model.InvoiceArray = model.InvoiceArray.Where(i => i.ClientId == Convert.ToInt32(client)).ToArray();
                if (!string.IsNullOrEmpty(project))
                    model.InvoiceArray = model.InvoiceArray.Where(i => i.ProjectId == Convert.ToInt32(project)).ToArray();
            }
            else
            {
                model = new InvoiceModel();
            }
            return View(model);
        }

        public ActionResult PrintPurchaseGSTReport(DateTime? startDate, DateTime? endDate, string filter, string paymentStatus, string supplier)
        {
            ExpenseModel model = new ExpenseModel();
            if (startDate != null && endDate != null)
            {
                model.expenseArray = ExpenseInterface.ReadByStartDateEndDate(startDate.Value, endDate.Value);
                if (!string.IsNullOrEmpty(filter))
                {
                    if (filter == "true")
                        model.expenseArray = model.expenseArray.Where(i => i.IncludeGST == true).ToArray();
                    else
                        model.expenseArray = model.expenseArray.Where(i => i.IncludeGST == false).ToArray();
                }
                if (!string.IsNullOrEmpty(paymentStatus))
                {
                    model.expenseArray = model.expenseArray.Where(i => i.PaymentStatus == paymentStatus).ToArray();
                }
                if (!string.IsNullOrEmpty(supplier))
                {
                    model.expenseArray = model.expenseArray.Where(i => i.Supplier == supplier).ToArray();
                }
            }
            else
            {
                model = new ExpenseModel();
            }
            return View(model);
        }

        public ActionResult ProfitLossReport(ProfitLossReportModel model)
        {
            if (model != null && model.StartDate != null && model.EndDate != null)
            {
                model = GetProfitLossDetails(model.StartDate.Value, model.EndDate.Value);
            }
            else
            {
                model = new ProfitLossReportModel();
            }
            return View(model);
        }

        public ActionResult ProjectProfitLossReport(ProjectProfitLossReportModel model)
        {
            if (model != null && model.StartDate != null && model.EndDate != null)
            {
                model = GetProjectProfitLossDetails(model);
            }
            else
            {
                model = new ProjectProfitLossReportModel();
            }
            return View(model);
        }

        public ActionResult CPFReport(CPFModel model)
        {
            if (model != null && !string.IsNullOrEmpty(model.SelectedYear) && !string.IsNullOrEmpty(model.SelectedMonth))
            {
                model = GetCPFDetails(model.SelectedMonth, model.SelectedYear);
            }
            else
            {
                model = GetCPFDetails(DateTime.Now.Month.ToString(), DateTime.Now.Year.ToString());
            }
            return View(model);
        }

        public ActionResult LevyReport(EmployeeModel model)
        {
            if (model != null && model.SelectedMonth != null && model.SelectedYear != null)
            {
                if (!string.IsNullOrEmpty(model.WorkStatus))
                {
                    model.SalaryArray = SalaryDetailInterface.ReadByStatusMonthYear(model.SelectedMonth, model.SelectedYear, model.WorkStatus);
                }
                else
                {
                    model.SalaryArray = SalaryDetailInterface.ReadByMonthYear(model.SelectedMonth, model.SelectedYear);
                }
            }
            else
            {
                model.SalaryArray = SalaryDetailInterface.ReadByMonthYear(DateTime.Now.Month.ToString(), DateTime.Now.Year.ToString());
                model.SelectedMonth = DateTime.Now.Month.ToString();
                model.SelectedYear = DateTime.Now.Year.ToString();
            }
            model.SalaryArray = model.SalaryArray.Where(x => x.WorkStatus == "SPass" || x.WorkStatus == "WorkPermit").ToArray();

            return View(model);
        }

        public ActionResult SDLReport(EmployeeModel model)
        {
            if (model != null && model.SelectedMonth != null && model.SelectedYear != null)
            {
                 model.SalaryArray = SalaryDetailInterface.ReadByMonthYear(model.SelectedMonth, model.SelectedYear);
            }
            else
            {
                model.SalaryArray = SalaryDetailInterface.ReadByMonthYear(DateTime.Now.Month.ToString(), DateTime.Now.Year.ToString());
                model.SelectedMonth = DateTime.Now.Month.ToString();
                model.SelectedYear = DateTime.Now.Year.ToString();
            }

            return View(model);
        }

        public ActionResult PrintClientReport(string clientId, string projectStatus)
        {
            List<ClientReportModel> clientReportModelList = new List<ClientReportModel>();
            if (!string.IsNullOrEmpty(clientId))
            {
                var client = ClientInterface.Read(Convert.ToInt32(clientId));

                var clientRecord = GetClient(projectStatus, client);
                if (clientRecord != null)
                {
                    clientReportModelList.Add(clientRecord);
                }
            }
            else
            {
                var clients = ClientInterface.Read();
                foreach (var client in clients)
                {
                    var clientRecord = GetClient(projectStatus, client);
                    if (clientRecord != null)
                    {
                        clientReportModelList.Add(clientRecord);
                    }
                }
            }
            return View(clientReportModelList);
        }

        public ActionResult PrintProjectReport(string clientId, string projectId, string projectStatus)
        {
            List<ProjectReportModel> projectReportModelList = new List<ProjectReportModel>();
            if (!string.IsNullOrEmpty(projectId))
            {
                var project = ProjectInterface.Read(Convert.ToInt32(projectId));

                var projectRecord = GetProject(project);
                if (projectRecord != null)
                {
                    projectReportModelList.Add(projectRecord);
                }
            }
            else if (!string.IsNullOrEmpty(clientId))
            {
                var projects = ProjectInterface.ReadByClientId(Convert.ToInt32(clientId));
                if (projects != null && projects.Length > 0)
                {
                    if (!string.IsNullOrEmpty(projectStatus))
                    {
                        projects = projects.Where(s => s.Status == projectStatus).ToArray();
                    }
                    foreach (var project in projects)
                    {
                        var projectRecord = GetProject(project);
                        if (projectRecord != null)
                        {
                            projectReportModelList.Add(projectRecord);
                        }
                    }
                }
            }
            else
            {
                var projects = ProjectInterface.Read();
                if (projects != null && projects.Length > 0)
                {
                    if (!string.IsNullOrEmpty(projectStatus))
                    {
                        projects = projects.Where(s => s.Status == projectStatus).ToArray();
                    }
                    foreach (var project in projects)
                    {
                        var projectRecord = GetProject(project);
                        if (projectRecord != null)
                        {
                            projectReportModelList.Add(projectRecord);
                        }
                    }
                }
            }

            return View(projectReportModelList);
        }

        public ActionResult PrintProjectEmployeeReport(string projectId)
        {
            EmployeeModel model = new EmployeeModel();
            List<Employee> employeeList = new List<Employee>();
            if (!string.IsNullOrEmpty(projectId))
            {
                var conf = EmployeeProjectInterface.ReadByProject(Convert.ToInt32(projectId));
                foreach (var item in conf)
                {
                    var employee = EmployeeInterface.Read(item.EmployeeId);
                    employeeList.Add(employee);
                }
                model.employeeList = employeeList.ToArray();
            }
            else
            {
                model.employeeList = EmployeeInterface.Read();
            }
            return View(model);
        }

        public ActionResult PrintEmployeeReport(string workStatus)
        {
            EmployeeModel model = new EmployeeModel();
            if (!string.IsNullOrEmpty(workStatus))
            {
                model.employeeList = EmployeeInterface.ReadByWorkStatus(workStatus);
            }
            else
            {
                model.employeeList = EmployeeInterface.Read();
            }
            return View(model);
        }

        public ActionResult PrintAttendanceReport(DateTime? startDate, DateTime? endDate, int? projectId, int? clientId, string empId)
        {
            TimesheetModel model = new TimesheetModel();
            if (startDate != null && endDate != null)
            {
                if (projectId > 0)
                {
                    model.timesheetArray = TimesheetInterface.ReadByProjectStartDateEndDate(startDate.Value, endDate.Value, projectId.Value);
                }
                else if (clientId > 0)
                {
                    model.timesheetArray = TimesheetInterface.ReadByClientStartDateEndDate(startDate.Value, endDate.Value, clientId.Value);
                }
                else
                {
                    model.timesheetArray = TimesheetInterface.ReadByStartDateEndDate(startDate.Value, endDate.Value);
                }

                if (!string.IsNullOrEmpty(empId))
                {
                    model.timesheetArray = model.timesheetArray.Where(i => i.EmployeeId == Convert.ToInt32(empId)).ToArray();
                }
            }
            else
            {
                model = new TimesheetModel();
            }
            return View(model);
        }

        public ActionResult PrintSalaryReport(DateTime? startDate, DateTime? endDate, string workStatus)
        {
            EmployeeModel model = new EmployeeModel();
            if (startDate != null && endDate != null)
            {
                if (!string.IsNullOrEmpty(workStatus))
                {
                    model.SalaryArray = SalaryDetailInterface.ReadByWorkStatus(startDate.Value, endDate.Value, workStatus);
                }
                else
                {
                    model.SalaryArray = SalaryDetailInterface.ReadByStartDateEndDate(startDate.Value, endDate.Value);
                }
            }
            else
            {
                model = new EmployeeModel();
            }
            return View(model);
        }

        public ActionResult PrintLevyReport(string month, string year, string workStatus)
        {
            EmployeeModel model = new EmployeeModel();
            if (!string.IsNullOrEmpty(month) && !string.IsNullOrEmpty(year))
            {
                if (!string.IsNullOrEmpty(workStatus))
                {
                    model.SalaryArray = SalaryDetailInterface.ReadByStatusMonthYear(month, year, workStatus);
                }
                else
                {
                    model.SalaryArray = SalaryDetailInterface.ReadByMonthYear(month, year);
                }
                model.SelectedMonth = month;
                model.SelectedYear = year;
            }
            else
            {
                model.SalaryArray = SalaryDetailInterface.ReadByMonthYear(DateTime.Now.Month.ToString(), DateTime.Now.Year.ToString());
                model.SelectedMonth = DateTime.Now.Month.ToString();
                model.SelectedYear = DateTime.Now.Year.ToString();
            }
            model.SalaryArray = model.SalaryArray.Where(x => x.WorkStatus == "SPass" || x.WorkStatus == "WorkPermit").ToArray();
            return View(model);
        }

        public ActionResult PrintSDLReport(string month, string year)
        {
            EmployeeModel model = new EmployeeModel();
            if (!string.IsNullOrEmpty(month) && !string.IsNullOrEmpty(year))
            {
                model.SalaryArray = SalaryDetailInterface.ReadByMonthYear(month, year);
                model.SelectedMonth = month;
                model.SelectedYear = year;
            }
            else
            {
                model.SalaryArray = SalaryDetailInterface.ReadByMonthYear(DateTime.Now.Month.ToString(), DateTime.Now.Year.ToString());
                model.SelectedMonth = DateTime.Now.Month.ToString();
                model.SelectedYear = DateTime.Now.Year.ToString();
            }
           return View(model);
        }

        public ActionResult PrintCPFReport(string month, string year)
        {
            CPFModel model = new CPFModel();
            if (!string.IsNullOrEmpty(month) && !string.IsNullOrEmpty(year))
            {
                  model = GetCPFDetails(month, year);
            }
            else
            {
                model = GetCPFDetails(DateTime.Now.Month.ToString(), DateTime.Now.Year.ToString());
            }
            return View(model);
        }

        public ActionResult PrintProfitLossReport(DateTime? startDate, DateTime? endDate)
        {
            ProfitLossReportModel model = new ProfitLossReportModel();
            if (startDate != null && endDate != null)
            {
                model = GetProfitLossDetails(startDate.Value, endDate.Value);
            }
            else
            {
                model = new ProfitLossReportModel();
            }
            return View(model);
        }

        public ActionResult PrintProjectProfitLossReport(DateTime? startDate, DateTime? endDate, int? client, int? project)
        {
            ProjectProfitLossReportModel model = new ProjectProfitLossReportModel();
            if (startDate != null && endDate != null && client != null && project != null)
            {
                model.StartDate = startDate;
                model.EndDate = endDate;
                model.ProjectId = project;
                model.ClientId = client;
                model = GetProjectProfitLossDetails(model);
            }
            else
            {
                model = new ProjectProfitLossReportModel();
            }
            return View(model);
        }

        private static ClientReportModel GetClient(string projectStatus, Client client)
        {
            var quotations = QuotationInterface.ReadByClientId(client.Id);
            List<Invoice> invoiceList = new List<Invoice>();

            ClientReportModel reportModel = new ClientReportModel();
            reportModel.ClientID = client.Id;
            reportModel.ClientName = client.FirstName + " " + client.LastName;
            reportModel.CompanyName = client.CompanyName;

            if (!string.IsNullOrEmpty(projectStatus))
            {
                var projects = client.projects.Where(p => p.Status == projectStatus);
                var quotationList = quotations.Where(i => projects.Any(j => j.Id == i.ProjectId));
                foreach (var quotation in quotationList)
                {
                    var invoice = InvoiceInterface.ReadByQuotationId(quotation.Id);
                    if (invoice != null && invoice.Count() > 0)
                        invoiceList.AddRange(invoice);
                }

                reportModel.CompletedProjects = projects.Where(i => string.Compare(i.Status, "Completed", StringComparison.OrdinalIgnoreCase) == 0).Count();
                reportModel.OngoingProjects = projects.Where(i => string.Compare(i.Status, "Ongoing", StringComparison.OrdinalIgnoreCase) == 0).Count();
                reportModel.DraftProjects = projects.Where(i => string.Compare(i.Status, "Draft", StringComparison.OrdinalIgnoreCase) == 0).Count();
                reportModel.TotalValue = invoiceList.AsEnumerable().Sum(i => i.InvoiceAmount).GetValueOrDefault(0);
                reportModel.ReceivedPayment = invoiceList.AsEnumerable().Sum(i => i.Payments.AsEnumerable().Sum(j => j.Amount)).GetValueOrDefault(0);
            }
            else
            {
                foreach (var quotation in quotations)
                {
                    var invoice = InvoiceInterface.ReadByQuotationId(quotation.Id);
                    if (invoice != null && invoice.Count() > 0)
                        invoiceList.AddRange(invoice);
                }
                reportModel.CompletedProjects = client.projects.Where(i => string.Compare(i.Status, "Completed", StringComparison.OrdinalIgnoreCase) == 0).Count();
                reportModel.OngoingProjects = client.projects.Where(i => string.Compare(i.Status, "Ongoing", StringComparison.OrdinalIgnoreCase) == 0).Count();
                reportModel.DraftProjects = client.projects.Where(i => string.Compare(i.Status, "Draft", StringComparison.OrdinalIgnoreCase) == 0).Count();
                reportModel.TotalValue = invoiceList.AsEnumerable().Sum(i => i.InvoiceAmount).GetValueOrDefault(0);
                reportModel.ReceivedPayment = invoiceList.AsEnumerable().Sum(i => i.Payments.AsEnumerable().Sum(j => j.Amount)).GetValueOrDefault(0);
            }

            reportModel.OutstandingAmount = reportModel.TotalValue.Value - reportModel.ReceivedPayment.Value;
            return reportModel;
        }

        private static ProjectReportModel GetProject(Project project)
        {
            var projectEmployees = EmployeeProjectInterface.ReadByProject(project.Id);
            var quotations = QuotationInterface.ReadByProjectId(project.Id);
            List<Invoice> invoiceList = new List<Invoice>();
            foreach (var quotation in quotations.Where(x=> string.Compare(x.Stage, "Confirmed", StringComparison.OrdinalIgnoreCase) == 0))
            {
                var invoice = InvoiceInterface.ReadByQuotationId(quotation.Id);
                if (invoice != null && invoice.Count() > 0)
                    invoiceList.AddRange(invoice);
            }

            ProjectReportModel reportModel = new ProjectReportModel();
            reportModel.ProjectId = project.Id;
            reportModel.ProjectName = project.ProjectName;
            reportModel.ProjectEmployees = projectEmployees.Length;
            reportModel.ClientName = project.client.FirstName + " " + project.client.LastName;
            reportModel.Status = project.Status;
            reportModel.StartDate = project.StartDate;
            reportModel.EndDate = project.EndDate;
            reportModel.ProjectValue = quotations.AsEnumerable().Sum(i => i.QuotationValue).GetValueOrDefault(0);
            reportModel.ReceivedPayment = invoiceList.AsEnumerable().Sum(i => i.Payments.AsEnumerable().Sum(j => j.Amount)).GetValueOrDefault(0);
            reportModel.OutstandingAmount = reportModel.ProjectValue.Value - reportModel.ReceivedPayment.Value;
            return reportModel;
        }

        private static ProfitLossReportModel GetProfitLossDetails(DateTime startDate, DateTime endDate)
        {
            ProfitLossReportModel model = new ProfitLossReportModel();
            var invoiceDetails = InvoiceInterface.ReadByStartDateEndDate(startDate, endDate);
            var salryDetails = SalaryDetailInterface.ReadByStartDateEndDate(startDate, endDate);
            var incomeDetails = OtherIncomeInterface.ReadByStartDateEndDate(startDate, endDate);
            var expensesDetails = ExpenseInterface.ReadByStartDateEndDate(startDate, endDate);

            model.GrossSale = invoiceDetails.AsEnumerable().Sum(i => i.InvoiceAmount).GetValueOrDefault(0);

            model.EmployerCPF = salryDetails.AsEnumerable().Sum(i => i.EmployerCPF).GetValueOrDefault(0);
            model.WorkerLevy = salryDetails.AsEnumerable().Sum(i => i.Levy).GetValueOrDefault(0);
            model.SDL = salryDetails.AsEnumerable().Sum(i => i.SDL).GetValueOrDefault(0);
            model.WagesAndSalary = salryDetails.AsEnumerable().Sum(i => i.GrossSalary);

            model.OtherIncome = incomeDetails.AsEnumerable().Sum(i => i.TotalAmount).GetValueOrDefault(0);

            model.Expense = expensesDetails.AsEnumerable().Where(j => string.Compare(j.ExpenseCategory, _projectExpense, StringComparison.OrdinalIgnoreCase) == 0).Sum(i => i.ExpenseTotalValue).GetValueOrDefault(0);

            model.AccountingAndLegalFee = expensesDetails.AsEnumerable()
                                            .Where(j => string.Compare(j.ExpenseCategory, _operationExpense, StringComparison.OrdinalIgnoreCase) == 0)
                                            .Where(j => string.Compare(j.OperationExpenseCategory, _accountingAndLegalFee, StringComparison.OrdinalIgnoreCase) == 0)
                                            .Sum(i => i.ExpenseTotalValue).GetValueOrDefault(0);
            model.Advertising = expensesDetails.AsEnumerable()
                                    .Where(j => string.Compare(j.ExpenseCategory, _operationExpense, StringComparison.OrdinalIgnoreCase) == 0)
                                    .Where(j => string.Compare(j.OperationExpenseCategory, _advertising, StringComparison.OrdinalIgnoreCase) == 0)
                                    .Sum(i => i.ExpenseTotalValue).GetValueOrDefault(0);
            model.Insurance = expensesDetails.AsEnumerable()
                                .Where(j => string.Compare(j.ExpenseCategory, _operationExpense, StringComparison.OrdinalIgnoreCase) == 0)
                                .Where(j => string.Compare(j.OperationExpenseCategory, _insurance, StringComparison.OrdinalIgnoreCase) == 0)
                                .Sum(i => i.ExpenseTotalValue).GetValueOrDefault(0);
            model.InterestAndBankCharge = expensesDetails.AsEnumerable()
                                            .Where(j => string.Compare(j.ExpenseCategory, _operationExpense, StringComparison.OrdinalIgnoreCase) == 0)
                                            .Where(j => string.Compare(j.OperationExpenseCategory, _interestAndBankCharge, StringComparison.OrdinalIgnoreCase) == 0)
                                            .Sum(i => i.ExpenseTotalValue).GetValueOrDefault(0);
            model.Postage = expensesDetails.AsEnumerable()
                                            .Where(j => string.Compare(j.ExpenseCategory, _operationExpense, StringComparison.OrdinalIgnoreCase) == 0)
                                            .Where(j => string.Compare(j.OperationExpenseCategory, _postage, StringComparison.OrdinalIgnoreCase) == 0)
                                            .Sum(i => i.ExpenseTotalValue).GetValueOrDefault(0);
            model.PremiseRent = expensesDetails.AsEnumerable()
                                    .Where(j => string.Compare(j.ExpenseCategory, _operationExpense, StringComparison.OrdinalIgnoreCase) == 0)
                                    .Where(j => string.Compare(j.OperationExpenseCategory, _premiseRent, StringComparison.OrdinalIgnoreCase) == 0)
                                    .Sum(i => i.ExpenseTotalValue).GetValueOrDefault(0);
            model.PrintingAndStationary = expensesDetails.AsEnumerable()
                                            .Where(j => string.Compare(j.ExpenseCategory, _operationExpense, StringComparison.OrdinalIgnoreCase) == 0)
                                            .Where(j => string.Compare(j.OperationExpenseCategory, _printingAndStationary, StringComparison.OrdinalIgnoreCase) == 0)
                                            .Sum(i => i.ExpenseTotalValue).GetValueOrDefault(0);
            model.ProfessionalMembership = expensesDetails.AsEnumerable()
                                            .Where(j => string.Compare(j.ExpenseCategory, _operationExpense, StringComparison.OrdinalIgnoreCase) == 0)
                                            .Where(j => string.Compare(j.OperationExpenseCategory, _professionalMembership, StringComparison.OrdinalIgnoreCase) == 0)
                                            .Sum(i => i.ExpenseTotalValue).GetValueOrDefault(0);
            model.RepairAndMaintenance = expensesDetails.AsEnumerable()
                                            .Where(j => string.Compare(j.ExpenseCategory, _operationExpense, StringComparison.OrdinalIgnoreCase) == 0)
                                            .Where(j => string.Compare(j.OperationExpenseCategory, _repairAndMaintenance, StringComparison.OrdinalIgnoreCase) == 0)
                                            .Sum(i => i.ExpenseTotalValue).GetValueOrDefault(0);
            model.Training = expensesDetails.AsEnumerable()
                                .Where(j => string.Compare(j.ExpenseCategory, _operationExpense, StringComparison.OrdinalIgnoreCase) == 0)
                                .Where(j => string.Compare(j.OperationExpenseCategory, _training, StringComparison.OrdinalIgnoreCase) == 0)
                                .Sum(i => i.ExpenseTotalValue).GetValueOrDefault(0);
            model.Utilitiy = expensesDetails.AsEnumerable()
                                            .Where(j => string.Compare(j.ExpenseCategory, _operationExpense, StringComparison.OrdinalIgnoreCase) == 0)
                                            .Where(j => string.Compare(j.OperationExpenseCategory, _utilitiy, StringComparison.OrdinalIgnoreCase) == 0)
                                            .Sum(i => i.ExpenseTotalValue).GetValueOrDefault(0);
            model.VehicleOperatingCost = expensesDetails.AsEnumerable()
                                            .Where(j => string.Compare(j.ExpenseCategory, _operationExpense, StringComparison.OrdinalIgnoreCase) == 0)
                                            .Where(j => string.Compare(j.OperationExpenseCategory, _vehicleOperatingCost, StringComparison.OrdinalIgnoreCase) == 0)
                                            .Sum(i => i.ExpenseTotalValue).GetValueOrDefault(0);
            model.WorkerCompensation = expensesDetails.AsEnumerable()
                                                        .Where(j => string.Compare(j.ExpenseCategory, _operationExpense, StringComparison.OrdinalIgnoreCase) == 0)
                                                        .Where(j => string.Compare(j.OperationExpenseCategory, _workerCompensation, StringComparison.OrdinalIgnoreCase) == 0)
                                                        .Sum(i => i.ExpenseTotalValue).GetValueOrDefault(0);
            model.Depreciation = expensesDetails.AsEnumerable()
                                            .Where(j => string.Compare(j.ExpenseCategory, _operationExpense, StringComparison.OrdinalIgnoreCase) == 0)
                                            .Where(j => string.Compare(j.OperationExpenseCategory, _depreciation, StringComparison.OrdinalIgnoreCase) == 0)
                                            .Sum(i => i.ExpenseTotalValue).GetValueOrDefault(0);
            model.otherExpense = new List<OtherExpense>();
            foreach (var expense in expensesDetails.AsEnumerable()
                .Where(j => string.Compare(j.ExpenseCategory, _operationExpense, StringComparison.OrdinalIgnoreCase) == 0).Where(j => string.Compare(j.OperationExpenseCategory, _other, StringComparison.OrdinalIgnoreCase) == 0))
            {
                var otherExpense = new OtherExpense() { ExpenseDescription = expense.OtherExpense, ExpenseAmount = expense.ExpenseTotalValue };
                model.otherExpense.Add(otherExpense);
            }

            model.TotalAllowableExpense = expensesDetails.AsEnumerable()
                .Where(j => string.Compare(j.ExpenseCategory, _operationExpense, StringComparison.OrdinalIgnoreCase) == 0)
                .Sum(i => i.ExpenseTotalValue).GetValueOrDefault(0);
            model.TotalAllowableExpense = model.TotalAllowableExpense + model.WagesAndSalary + model.WorkerLevy + model.EmployerCPF;
            model.StartDate = startDate;
            model.EndDate = endDate;
            return model;
        }


        private ProjectProfitLossReportModel GetProjectProfitLossDetails(ProjectProfitLossReportModel model)
        {
            var invoiceDetails = InvoiceInterface.ReadByStartDateEndDate(model.StartDate.GetValueOrDefault(), model.EndDate.GetValueOrDefault());
            var expensesDetails = ExpenseInterface.ReadByClientId(model.ClientId.GetValueOrDefault());
            if (model.ProjectId.GetValueOrDefault() > 0)
            {
                expensesDetails = expensesDetails.Where(i => i.Date >= model.StartDate.GetValueOrDefault() && i.Date <= model.EndDate.GetValueOrDefault())
                     .Where(i => i.ProjectId == model.ProjectId.GetValueOrDefault()).ToArray();
            }

            model.ProjectValue = invoiceDetails.Where(j => j.ProjectId.GetValueOrDefault() == model.ProjectId).AsEnumerable().Sum(i => i.InvoiceAmount).GetValueOrDefault(0);

            model.GrossSale = invoiceDetails.Select(j=> j.Payments).AsEnumerable().Sum(i => i.AsEnumerable().Sum(k=> k.Amount)).GetValueOrDefault(0);
            
            model.AccountingAndLegalFee = expensesDetails.AsEnumerable()
                                            .Where(j => string.Compare(j.ExpenseCategory, _projectExpense, StringComparison.OrdinalIgnoreCase) == 0)
                                            .Where(j => string.Compare(j.MaterialPurchseCategory, _accountingAndLegalFee, StringComparison.OrdinalIgnoreCase) == 0)
                                            .Sum(i => i.ExpenseTotalValue).GetValueOrDefault(0);
            model.Advertising = expensesDetails.AsEnumerable()
                                    .Where(j => string.Compare(j.ExpenseCategory, _projectExpense, StringComparison.OrdinalIgnoreCase) == 0)
                                    .Where(j => string.Compare(j.MaterialPurchseCategory, _advertising, StringComparison.OrdinalIgnoreCase) == 0)
                                    .Sum(i => i.ExpenseTotalValue).GetValueOrDefault(0);
            model.Insurance = expensesDetails.AsEnumerable()
                                .Where(j => string.Compare(j.ExpenseCategory, _projectExpense, StringComparison.OrdinalIgnoreCase) == 0)
                                .Where(j => string.Compare(j.MaterialPurchseCategory, _insurance, StringComparison.OrdinalIgnoreCase) == 0)
                                .Sum(i => i.ExpenseTotalValue).GetValueOrDefault(0);
            model.InterestAndBankCharge = expensesDetails.AsEnumerable()
                                            .Where(j => string.Compare(j.ExpenseCategory, _projectExpense, StringComparison.OrdinalIgnoreCase) == 0)
                                            .Where(j => string.Compare(j.MaterialPurchseCategory, _interestAndBankCharge, StringComparison.OrdinalIgnoreCase) == 0)
                                            .Sum(i => i.ExpenseTotalValue).GetValueOrDefault(0);
            model.Postage = expensesDetails.AsEnumerable()
                                            .Where(j => string.Compare(j.ExpenseCategory, _projectExpense, StringComparison.OrdinalIgnoreCase) == 0)
                                            .Where(j => string.Compare(j.MaterialPurchseCategory, _postage, StringComparison.OrdinalIgnoreCase) == 0)
                                            .Sum(i => i.ExpenseTotalValue).GetValueOrDefault(0);
            model.PremiseRent = expensesDetails.AsEnumerable()
                                    .Where(j => string.Compare(j.ExpenseCategory, _projectExpense, StringComparison.OrdinalIgnoreCase) == 0)
                                    .Where(j => string.Compare(j.MaterialPurchseCategory, _premiseRent, StringComparison.OrdinalIgnoreCase) == 0)
                                    .Sum(i => i.ExpenseTotalValue).GetValueOrDefault(0);
            model.PrintingAndStationary = expensesDetails.AsEnumerable()
                                            .Where(j => string.Compare(j.ExpenseCategory, _projectExpense, StringComparison.OrdinalIgnoreCase) == 0)
                                            .Where(j => string.Compare(j.MaterialPurchseCategory, _printingAndStationary, StringComparison.OrdinalIgnoreCase) == 0)
                                            .Sum(i => i.ExpenseTotalValue).GetValueOrDefault(0);
            model.ProfessionalMembership = expensesDetails.AsEnumerable()
                                            .Where(j => string.Compare(j.ExpenseCategory, _projectExpense, StringComparison.OrdinalIgnoreCase) == 0)
                                            .Where(j => string.Compare(j.MaterialPurchseCategory, _professionalMembership, StringComparison.OrdinalIgnoreCase) == 0)
                                            .Sum(i => i.ExpenseTotalValue).GetValueOrDefault(0);
            model.RepairAndMaintenance = expensesDetails.AsEnumerable()
                                            .Where(j => string.Compare(j.ExpenseCategory, _projectExpense, StringComparison.OrdinalIgnoreCase) == 0)
                                            .Where(j => string.Compare(j.MaterialPurchseCategory, _repairAndMaintenance, StringComparison.OrdinalIgnoreCase) == 0)
                                            .Sum(i => i.ExpenseTotalValue).GetValueOrDefault(0);
            model.Training = expensesDetails.AsEnumerable()
                                .Where(j => string.Compare(j.ExpenseCategory, _projectExpense, StringComparison.OrdinalIgnoreCase) == 0)
                                .Where(j => string.Compare(j.MaterialPurchseCategory, _training, StringComparison.OrdinalIgnoreCase) == 0)
                                .Sum(i => i.ExpenseTotalValue).GetValueOrDefault(0);
            model.Utilitiy = expensesDetails.AsEnumerable()
                                            .Where(j => string.Compare(j.ExpenseCategory, _projectExpense, StringComparison.OrdinalIgnoreCase) == 0)
                                            .Where(j => string.Compare(j.MaterialPurchseCategory, _utilitiy, StringComparison.OrdinalIgnoreCase) == 0)
                                            .Sum(i => i.ExpenseTotalValue).GetValueOrDefault(0);
            model.VehicleOperatingCost = expensesDetails.AsEnumerable()
                                            .Where(j => string.Compare(j.ExpenseCategory, _projectExpense, StringComparison.OrdinalIgnoreCase) == 0)
                                            .Where(j => string.Compare(j.MaterialPurchseCategory, _vehicleOperatingCost, StringComparison.OrdinalIgnoreCase) == 0)
                                            .Sum(i => i.ExpenseTotalValue).GetValueOrDefault(0);
            model.WorkerCompensation = expensesDetails.AsEnumerable()
                                                        .Where(j => string.Compare(j.ExpenseCategory, _projectExpense, StringComparison.OrdinalIgnoreCase) == 0)
                                                        .Where(j => string.Compare(j.MaterialPurchseCategory, _workerCompensation, StringComparison.OrdinalIgnoreCase) == 0)
                                                        .Sum(i => i.ExpenseTotalValue).GetValueOrDefault(0);
            model.Depreciation = expensesDetails.AsEnumerable()
                                            .Where(j => string.Compare(j.ExpenseCategory, _projectExpense, StringComparison.OrdinalIgnoreCase) == 0)
                                            .Where(j => string.Compare(j.MaterialPurchseCategory, _depreciation, StringComparison.OrdinalIgnoreCase) == 0)
                                            .Sum(i => i.ExpenseTotalValue).GetValueOrDefault(0);
            model.otherExpense = new List<OtherExpense>();
            foreach (var expense in expensesDetails.AsEnumerable()
                .Where(j => string.Compare(j.ExpenseCategory, _projectExpense, StringComparison.OrdinalIgnoreCase) == 0).Where(j => string.Compare(j.MaterialPurchseCategory, _other, StringComparison.OrdinalIgnoreCase) == 0))
            {
                var otherExpense = new OtherExpense() { ExpenseDescription = expense.OtherExpense, ExpenseAmount = expense.ExpenseTotalValue };
                model.otherExpense.Add(otherExpense);
            }

            model.ProjectExpense = expensesDetails.AsEnumerable()
                .Where(j => string.Compare(j.ExpenseCategory, _projectExpense, StringComparison.OrdinalIgnoreCase) == 0)
                .Sum(i => i.ExpenseTotalValue).GetValueOrDefault(0);

            model.ProjectList = new List<SelectListItem>();
            model.ProjectList = ProjectInterface.ReadByClientId(model.ClientId.Value)
                                          .Select(x => new SelectListItem()
                                          {
                                              Value = x.Id.ToString(),
                                              Text = x.Id.ToString() + " : " + x.ProjectName
                                          })
                                          .OrderBy(x => x.Value)
                                          .ToList();

            model.ClientName = Utility.Utilities.GetClientName(model.ClientId.GetValueOrDefault(0));
            model.ProjectName = Utility.Utilities.GetProjectName(model.ProjectId.GetValueOrDefault(0));

            return model;
        }

        private CPFModel GetCPFDetails(string selectedMonth, string selectedYear)
        {
            var conf = CPFInterface.ReadByYearAndMonth(Convert.ToInt32(selectedYear), Convert.ToInt32(selectedMonth));
            var model = new CPFModel(conf);
            model.SelectedYear = selectedYear;
            model.SelectedMonth = selectedMonth;
            return model;
        }
    }
}
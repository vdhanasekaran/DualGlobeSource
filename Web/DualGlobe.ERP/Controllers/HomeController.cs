using System.Web.Mvc;
using DualGlobe.ERP.Models;
using Library.DualGlobe.ERP.Interfaces;
using System.Linq;
using System;

namespace DualGlobe.ERP.Controllers
{
    public class HomeController : BaseController
    {
        //Dashboard
        public ActionResult Index()
        {
            DashboardModel model = new DashboardModel();
            
            model.ClientCount = ClientInterface.GetCount();
            model.InvoiceCount = InvoiceInterface.GetCount();
            model.QuotationCount = QuotationInterface.GetCount();
            model.ProjectCount = ProjectInterface.GetCount();
            var employees = EmployeeInterface.Read();
            model.EmployeeCount = 0;
            model.ExpiringInsuranceCount = 0;
            model.ExpiringCsocCount = 0;
            model.ExpiringPassportCount = 0;
            model.ExpiringWPCount = 0;
            if (employees.Any())
            {
                model.EmployeeCount = employees.Count();
                foreach (var record in employees.ToList())
                {
                    if (!string.IsNullOrEmpty(record.PassportNumber) && record.PassportExpiryDate.HasValue && ((record.PassportExpiryDate.Value - DateTime.Today).Days < 30))
                        model.ExpiringPassportCount += 1;
                    if (record.WorkPermitExpiryDate.HasValue && ((record.WorkPermitExpiryDate.Value - DateTime.Today).Days < 30))
                        model.ExpiringWPCount += 1;
                    if (record.CSOCExpiryDate.HasValue && ((record.CSOCExpiryDate.Value - DateTime.Today).Days < 30))
                        model.ExpiringCsocCount += 1;
                }
            }
            
            var data = InsuranceInterface.ReadExpiringInsurance();
            if (data != null && data.Any())
            {
                model.ExpiringInsuranceCount = data.Length;
            }

            return View(model);
        }

        public DateTime SessionAlive(int delayTime)
        {
            return DateTime.Now.AddSeconds(delayTime);
        }
    }
}
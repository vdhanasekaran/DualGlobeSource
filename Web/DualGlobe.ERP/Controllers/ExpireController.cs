using System.Web.Mvc;
using DualGlobe.ERP.Models;
using Library.DualGlobe.ERP.Interfaces;
using System.Linq;
using System;
using Library.DualGlobe.ERP.Models;
using System.Collections.Generic;

namespace DualGlobe.ERP.Controllers
{
    public class ExpireController : BaseController
    {
        //CSOC
        public ActionResult CSOC()
        {
            var employees = EmployeeInterface.Read();
            List<Employee> empList = new List<Employee>();
            if (employees.Any())
            {
                foreach (var record in employees.ToList())
                {
                    if (record.CSOCExpiryDate.HasValue && ((record.CSOCExpiryDate.Value - DateTime.Today).Days < 30))
                        empList.Add(record);
                }
            }
            var model = new EmployeeModel(empList.ToArray());
            return View(model);
        }

        public ActionResult Passport()
        {
            var employees = EmployeeInterface.Read();
            List<Employee> empList = new List<Employee>();
            if (employees.Any())
            {
                foreach (var record in employees.ToList())
                {
                    if (!string.IsNullOrEmpty(record.PassportNumber) && record.PassportExpiryDate.HasValue && ((record.PassportExpiryDate.Value - DateTime.Today).Days < 30))
                        empList.Add(record);
                }
            }
            var model = new EmployeeModel(empList.ToArray());
            return View(model);
        }

        public ActionResult Workpermit()
        {
            var employees = EmployeeInterface.Read();
            List<Employee> empList = new List<Employee>();
            if (employees.Any())
            {
                foreach (var record in employees.ToList())
                {
                    if (record.WorkPermitExpiryDate.HasValue && ((record.WorkPermitExpiryDate.Value - DateTime.Today).Days < 30))
                        empList.Add(record);
                }
            }
            var model = new EmployeeModel(empList.ToArray());
            return View(model);
        }
    }
}
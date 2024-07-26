using System.Web.Mvc;
using Library.DualGlobe.ERP.Interfaces;
using DualGlobe.ERP.Models;
using System;
using DualGlobe.ERP.Utility;
using System.Collections.Generic;
using System.Linq;

namespace DualGlobe.ERP.Controllers
{
    public class EmployeeProjectController : BaseController
    {
        // GET: Employee Project
        public ActionResult Index(EmployeeProjectModel empProjmodel)
        {
            if (!string.IsNullOrEmpty(empProjmodel.ProjectId))
            {
                var conf = EmployeeProjectInterface.ReadByProject(Convert.ToInt16(empProjmodel.ProjectId));
                conf = conf.OrderBy(x => Convert.ToInt32(x.EmployeeId)).ToArray();
                var model = new EmployeeProjectModel(conf);
                model.ProjectId = empProjmodel.ProjectId;
                return View(model);
            }
            return View(new EmployeeProjectModel());
        }

        public ActionResult EmployeeProject(int? employeeProjectId)
        {
            if(employeeProjectId != null)
            {
                var conf = EmployeeProjectInterface.Read(employeeProjectId.Value);
                var model = new EmployeeProjectModel(conf);
                return View(model);
            }
            else
            {
                return View(new EmployeeProjectModel());
            }
        }

        public ActionResult RemoveEmployee(int id, int projectId)
        {
            EmployeeProjectInterface.Delete(id);

            var conf = EmployeeProjectInterface.ReadByProject(projectId);
            var model = new EmployeeProjectModel(conf);
            model.ProjectId = projectId.ToString();
            return View("Index", model);
        }

        [AuthorizeUser(Roles = "Admin")]

        public ActionResult Submit(EmployeeProjectModel employeeProjectModel)
        {
            bool isAdded = false;
            if (employeeProjectModel.employeeProjectRecord != null)
            {
                isAdded = EmployeeProjectInterface.Create(employeeProjectModel.employeeProjectRecord);
            }
            if (isAdded)
            {
                ViewBag.SuccessMessage = "Employee added to the Project";
            }
            else {
                ViewBag.ErrorMessage = "Employee already exist in the Project";
            }
            return View("EmployeeProject", new EmployeeProjectModel());
        }

        [HttpPost]
        public ActionResult GetEmployee(string projectId)
        {
            int Id;
            List<SelectListItem> employeeNames = new List<SelectListItem>();
            if (!string.IsNullOrEmpty(projectId))
            {
                Id = Convert.ToInt32(projectId);
                var employees = EmployeeProjectInterface.ReadByProject(Id);
                foreach (var employee in employees)
                {
                    if (!employeeNames.Any(i => i.Value == employee.EmployeeId.ToString()))
                    {
                        employeeNames.Add(new SelectListItem
                        {
                            Value = employee.EmployeeId.ToString(),
                            Text = employee.EmployeeId.ToString() + " : " + employee.employee.FirstName + " " + employee.employee.LastName
                        });
                    }
                }
            }
            return Json(employeeNames, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetClientBasedEmployee(string clientId)
        {
            int Id;
            List<SelectListItem> employeeNames = new List<SelectListItem>();
            if (!string.IsNullOrEmpty(clientId))
            {
                Id = Convert.ToInt32(clientId);
                var projects = ProjectInterface.ReadByClientId(Id);
                foreach (var project in projects)
                {
                    var employees = EmployeeProjectInterface.ReadByProject(project.Id);
                    foreach (var employee in employees)
                    {
                        if (!employeeNames.Any(i => i.Value == employee.EmployeeId.ToString()))
                        {
                            employeeNames.Add(new SelectListItem
                            {
                                Value = employee.EmployeeId.ToString(),
                                Text = employee.EmployeeId.ToString() + " : " + employee.employee.FirstName + " " + employee.employee.LastName
                            });
                        }
                    }
                }
            }
            return Json(employeeNames, JsonRequestBehavior.AllowGet);
        }

    }
}
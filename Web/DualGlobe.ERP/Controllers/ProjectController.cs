using System.Web.Mvc;
using Library.DualGlobe.ERP.Interfaces;
using Library.DualGlobe.ERP.Models;
using DualGlobe.ERP.Models;
using System;
using DualGlobe.ERP.Utility;

namespace DualGlobe.ERP.Controllers
{
    public class ProjectController : BaseController
    {
        // GET: Project
        public ActionResult Index(int? clientId)
        {
            if (clientId == null)
            {
                var model = new ProjectModel();
                return View(model);
            }
            else
            {
                var conf = ProjectInterface.ReadByClientId(clientId.Value);
                var model = new ProjectModel(conf);
                model.ClientId = clientId.Value;
                return View(model);
            }
        }

        public ActionResult Project(int? projectId, string pageMode)
        {
            if (projectId != null)
            {
                var conf = ProjectInterface.Read(projectId.Value);
                var model = new ProjectModel(conf);

                model.PageMode = pageMode != null ? pageMode : string.Empty;
                return View(model);
            }
            else
            {
                return View(new ProjectModel());
            }
        }

        public JsonResult GetProjectName(int id)
        {
            if (id != 0)
            {
                var conf = ProjectInterface.Read(id);
                string projectName = string.Empty;
                if (conf != null)
                    projectName = conf.ProjectName;
                return Json(projectName, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [AuthorizeUser(Roles = "Admin")]
        public ActionResult Submit(ProjectModel model)
        {
                if (model.projectRecord != null)
                {
                    if (model.projectRecord.Id == 0)
                    {
                        ProjectInterface.Create(model.projectRecord);
                    }
                    else
                    {
                        ProjectInterface.Update(model.projectRecord);
                    }

                }

            return RedirectToAction("Index", new { clientId = model.projectRecord.ClientId });
        }

        public ActionResult Search(ProjectModel projectModel)
        {
            if (projectModel.ClientId != 0)
            {
                int clientId = projectModel.ClientId;
                return RedirectToAction("Index", new { clientId = clientId });
            }
            return RedirectToAction("Index");
        }
    }
}
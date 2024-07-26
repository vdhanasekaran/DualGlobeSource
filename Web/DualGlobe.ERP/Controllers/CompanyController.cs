using System.Web.Mvc;
using Library.DualGlobe.ERP.Interfaces;
using DualGlobe.ERP.Models;
using System.Web;
using DualGlobe.ERP.Utility;

namespace DualGlobe.ERP.Controllers
{
    public class CompanyController : BaseController
    {
        // GET: Company
        public ActionResult Index()
        {
            var conf = CompanyInterface.Read();
            var model = new CompanyModel(conf);
            return View(model);
        }

        public ActionResult Company(int? companyId, string pageMode)
        {
            if (companyId != null)
            {
                var conf = CompanyInterface.Read(companyId.Value);
                var model = new CompanyModel(conf);

                model.PageMode = pageMode != null ? pageMode : string.Empty;
                return View(model);
            }
            else
            {
                return View(new CompanyModel());
            }
        }

        [AuthorizeUser(Roles = "Admin")]
        public ActionResult Submit(CompanyModel model, HttpPostedFileBase upload)
        {

            if (model.companyRecord != null)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    var fileName = System.IO.Path.GetFileName(upload.FileName);
                    var path = System.IO.Path.Combine(Server.MapPath("~/Content/Logo"), fileName);

                    model.companyRecord.CompanyLogo = "/Content/Logo/" + fileName;
                    upload.SaveAs(path);
                }

                if (model.companyRecord.Id == 0)
                {
                    CompanyInterface.Create(model.companyRecord);
                }
                else
                {
                    CompanyInterface.Update(model.companyRecord);
                }

            }

            return RedirectToAction("Index");
        }
    }
}
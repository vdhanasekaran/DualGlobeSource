using System.Web.Mvc;
using Library.DualGlobe.ERP.Interfaces;
using DualGlobe.ERP.Models;
using System.Web;
using DualGlobe.ERP.Utility;

namespace DualGlobe.ERP.Controllers
{
    public class ClientController : BaseController
    {
        // GET: Client
        public ActionResult Index()
        {
            var conf = ClientInterface.Read();
            var model = new ClientModel(conf);
            return View(model);
        }

        public ActionResult Client(int? clientId, string pageMode)
        {
            if (clientId != null)
            {
                var conf = ClientInterface.Read(clientId.Value);
                var model = new ClientModel(conf);

                model.PageMode = pageMode != null ? pageMode : string.Empty;
                return View(model);
            }
            else
            {
                return View(new ClientModel());
            }
        }

        public JsonResult GetClientName(int id)
        {
            if (id != 0)
            {
                var conf = ClientInterface.Read(id);
                string clientName = string.Empty;
                if (conf != null)
                    clientName = conf.FirstName + " " + conf.LastName;
                return Json(clientName, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [AuthorizeUser(Roles = "Admin")]
        public ActionResult Submit(ClientModel model, HttpPostedFileBase upload)
        {

            if (model.clientRecord != null)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    var fileName = System.IO.Path.GetFileName(upload.FileName);
                    var path = System.IO.Path.Combine(Server.MapPath("~/Content/Logo"), fileName);

                    model.clientRecord.LogoUrl = "/Content/Logo/" + fileName;
                    upload.SaveAs(path);
                }

                if (model.clientRecord.Id == 0)
                {
                    ClientInterface.Create(model.clientRecord);
                }
                else
                {
                    ClientInterface.Update(model.clientRecord);
                }

            }

            return RedirectToAction("Index");
        }
    }
}
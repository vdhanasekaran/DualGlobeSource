using System.Web.Mvc;
using Library.DualGlobe.ERP.Interfaces;
using DualGlobe.ERP.Models;
using System.Web;
using DualGlobe.ERP.Utility;

namespace DualGlobe.ERP.Controllers
{
    public class SupplierController : BaseController
    {
        // GET: Supplier
        public ActionResult Index()
        {
            var conf = SupplierInterface.Read();
            var model = new SupplierModel(conf);
            return View(model);
        }

        public ActionResult Supplier(int? supplierId, string pageMode)
        {
            if (supplierId != null)
            {
                var conf = SupplierInterface.Read(supplierId.Value);
                var model = new SupplierModel(conf);

                model.PageMode = pageMode != null ? pageMode : string.Empty;
                return View(model);
            }
            else
            {
                return View(new SupplierModel());
            }
        }

        [AuthorizeUser(Roles = "Admin")]
        public ActionResult Submit(SupplierModel model)
        {

            if (model.supplierRecord != null)
            {

                if (model.supplierRecord.Id == 0)
                {
                    SupplierInterface.Create(model.supplierRecord);
                }
                else
                {
                    SupplierInterface.Update(model.supplierRecord);
                }

            }

            return RedirectToAction("Index");
        }
    }
}
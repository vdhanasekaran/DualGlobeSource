using System.Web.Mvc;
using Library.DualGlobe.ERP.Interfaces;
using DualGlobe.ERP.Models;
using System.Web;
using DualGlobe.ERP.Utility;

namespace DualGlobe.ERP.Controllers
{
    public class ExpenseCategoryController : BaseController
    {
        // GET: ExpenseCategory
        public ActionResult Index()
        {
            var conf = ExpenseCategoryInterface.Read();
            var model = new ExpenseCategoryModel(conf);
            return View(model);
        }

        public ActionResult ExpenseCategory(int? expenseCategoryId, string pageMode)
        {
            if (expenseCategoryId != null)
            {
                var conf = ExpenseCategoryInterface.Read(expenseCategoryId.Value);
                var model = new ExpenseCategoryModel(conf);

                model.PageMode = pageMode != null ? pageMode : string.Empty;
                return View(model);
            }
            else
            {
                return View(new ExpenseCategoryModel());
            }
        }

        [AuthorizeUser(Roles = "Admin")]
        public ActionResult Submit(ExpenseCategoryModel model)
        {

            if (model.expenseCategoryRecord != null)
            {

                if (model.expenseCategoryRecord.Id == 0)
                {
                    ExpenseCategoryInterface.Create(model.expenseCategoryRecord);
                }
                else
                {
                    ExpenseCategoryInterface.Update(model.expenseCategoryRecord);
                }

            }

            return RedirectToAction("Index");
        }
    }
}
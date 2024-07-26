using DualGlobe.ERP.Utility;
using System.Web.Mvc;

namespace DualGlobe.ERP.Controllers
{
    [AuthorizeUser]
    public class BaseController : Controller
    {
        protected virtual new CustomPrincipal User
        {
            get { return HttpContext.User as CustomPrincipal; }
        }
    }
}
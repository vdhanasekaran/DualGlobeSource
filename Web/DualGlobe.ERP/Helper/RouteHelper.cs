using System.Web.Mvc;

namespace DualGlobe.ERP.Helper
{
    public static class RouteHelper
    {
        public static string IsActive(this HtmlHelper html, string control, string action )
        {
            var routeData = html.ViewContext.RouteData;
            var routeControl = (string)routeData.Values["controller"];
            var routeAction = (string)routeData.Values["action"];

            var returnActive = control == routeControl && action == routeAction;
            return returnActive ? "active" : "";
        }
    }
}
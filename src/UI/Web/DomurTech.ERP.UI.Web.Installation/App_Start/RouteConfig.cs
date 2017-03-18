using System.Web.Mvc;
using System.Web.Routing;

namespace DomurTech.ERP.UI.Web.Installation
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute("Default", "{controller}/{action}/{id}", new { controller = "Home", action = "Step1", id = UrlParameter.Optional });
        }
    }
}

using System.Web.Mvc;
using System.Web.Routing;

namespace DomurTech.ERP.UI.Web.Application
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Default", "{culture}/{controller}/{action}/{id}", new { controller = "Home", action = "Index", id = UrlParameter.Optional }, new { culture = "[a-z]{2}-[a-z]{2}" });
        }
    }
}

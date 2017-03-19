using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DomurTech.Installation.WebApplication
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("tr-TR");
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("tr-TR");
        }
    }
}

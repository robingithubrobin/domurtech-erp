using System.Web.Mvc;
using DomurTech.ERP.UI.Web.Common.Infrastructure;
using DomurTech.ERP.UI.Web.Common.Security;

namespace DomurTech.ERP.UI.Web.Application.Controllers
{
    public class HomeController : CustomController
    {
     //   [CustomSecurity]
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Country");
        }     
    }
}
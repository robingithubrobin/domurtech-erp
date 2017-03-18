using System.Globalization;
using System.Threading;
using System.Web.Mvc;
using DomurTech.ERP.UI.Web.Common.Entities;
using DomurTech.ERP.UI.Web.Common.Infrastructure;

namespace DomurTech.ERP.UI.Web.Application.Controllers
{
    public class RedirectionController : CustomController
    {
        public ViewResult Redirect()
        {
            var redirectionModel = (RedirectionModel)TempData["RedirectionModel"];
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(ControllerIdentity.LanguageCode);
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(ControllerIdentity.LanguageCode);
            return View(redirectionModel);
        }
        public ActionResult RedirectAction(string url, string message, int timeout, string cssClass)
        {
            TempData["RedirectionModel"] = new RedirectionModel
            {
                Url = url,
                Message = message,
                Timeout = timeout,
                CssClass = cssClass
            };

            return RedirectToAction("Redirect", "Redirection");
        }
    }
}
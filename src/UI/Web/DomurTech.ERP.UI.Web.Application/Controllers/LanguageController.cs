using System.Linq;
using System.Web.Mvc;
using DomurTech.ERP.UI.Web.Common.Infrastructure;

namespace DomurTech.ERP.UI.Web.Application.Controllers
{
    public class LanguageController : CustomController
    {
        public ActionResult Change(string culture, string returnUrl)
        {
            var path = returnUrl;
            var pathList = path.Split('/').ToList();
            pathList.Remove(pathList[0]);
            var newUrl = "";
            for (var i = 1; i < pathList.Count; i++)
            {
                newUrl += pathList[i] + "/";
            }
            return Redirect("/" + culture + "/" + newUrl);
        }



    }
}
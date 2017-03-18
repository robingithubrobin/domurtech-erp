using System.Linq;
using System.Web.Mvc;
using DomurTech.ERP.Business.Managers.Abstract;
using DomurTech.ERP.Business.Managers.Concrete;
using DomurTech.ERP.Data.Access.EntityFramework;
using DomurTech.ERP.Data.Access.EntityFramework.Abstract;
using DomurTech.ERP.Data.Entities.Concrete;
using DomurTech.ERP.UI.Web.Common.Infrastructure;

namespace DomurTech.ERP.UI.Web.Application.Controllers
{
    public class LanguageController : CustomController
    {
        private ILanguageManager Manager(IDatabaseContext context)
        {
            return new LanguageManager(new Repository<Language>(context));
        }
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
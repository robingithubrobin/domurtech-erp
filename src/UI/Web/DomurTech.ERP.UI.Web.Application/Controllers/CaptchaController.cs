using System.Web.Mvc;
using DomurTech.ERP.UI.Web.Common.Helpers;
using DomurTech.ERP.UI.Web.Common.Infrastructure;
using DomurTech.Providers;

namespace DomurTech.ERP.UI.Web.Application.Controllers
{
    public class CaptchaController : CustomController
    {
        public FileResult Get()
        {
            var imgPath = Server.MapPath(SystemSettings.CaptchaBackgroundImagePath);
            var captchaLenght = SystemSettings.CaptchaLenght;
            return File(CaptchaHelper.GetCaptchaBytes(imgPath, captchaLenght), "image/jpeg");
        }
    }
}
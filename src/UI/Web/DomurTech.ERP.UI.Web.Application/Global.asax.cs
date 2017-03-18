using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using DomurTech.Core.Security;
using DomurTech.ERP.UI.Web.Common.Entities;
using DomurTech.ERP.UI.Web.Common.Helpers;
using DomurTech.ERP.UI.Web.Common.Security.CookieBase;
using DomurTech.Providers;

namespace DomurTech.ERP.UI.Web.Application
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            var path = Request.RawUrl;
            var pathItemList = path.Split('/').ToList();
            var language = pathItemList[1];
            var defaultLanguage = SystemSettings.DefaultLanguage;
            if (language == "")
            {
                Response.Redirect("/" + defaultLanguage);
            }
            else
            {
                var languageCodeList = SystemLanguages.AllLanguageCodes;
                if (languageCodeList.Contains(language))
                {
                    Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(language);
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(language);
                }
                else
                {
                    Response.Redirect("/" + defaultLanguage);
                }
            }
        }

        protected void Application_Start()
        {
            ReCaptchaHelper.Set();
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }


        public override void Init()
        {
            PostAuthenticateRequest += MvcApplication_PostAuthenticateRequest;
            base.Init();
        }

        protected void MvcApplication_PostAuthenticateRequest(object sender, EventArgs e)
        {
            var webSecurityManager = new WebSecurityManager();
            var principle = webSecurityManager.Get(WebCommonConstants.PrivateKey);

            if (principle == null)
            {
                var identity = new Identity
                {
                    UserId = Guid.Empty,
                    IsAuthenticated = false,
                    Name = "Guest",
                    Username = "Guest",
                    LanguageCode = Thread.CurrentThread.CurrentCulture.ToString(),
                    RememberMe = false,
                    Roles = new List<string>()
                };
                principle = new Principal(identity);
            }
            Thread.CurrentPrincipal = principle;
            HttpContext.Current.User = principle;
        }


    }
}

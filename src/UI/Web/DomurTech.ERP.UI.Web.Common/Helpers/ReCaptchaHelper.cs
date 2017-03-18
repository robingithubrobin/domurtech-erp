using System.Collections.Generic;
using System.Linq;
using System.Web;
using DomurTech.ERP.Business.Entities.Models.AccountModels;
using DomurTech.ERP.UI.Web.Common.Globalization;
using DomurTech.Providers;
using hbehr.recaptcha;

namespace DomurTech.ERP.UI.Web.Common.Helpers
{
    public static class ReCaptchaHelper
    {
        public static bool HasError { get; set; }
        public static string ErrorMessage { get; set; }

        private const string PublicKey = "6LfsqxgUAAAAAOQPtbRmHFJ8Mco1rcCycaEiiAeK";
        private const string SecretKey = "6LfsqxgUAAAAAJIOjmHJR9dac7y0D9ohsiUhPcYI";

        public static void Set()
        {
            if (SystemSettings.UseLoginCaptcha)
            {
                ReCaptcha.Configure(PublicKey, SecretKey, ReCaptchaLanguage.Turkish);
            }
        }

        public static void Validate()
        {
            if (SystemSettings.UseLoginCaptcha)
            {
                var userResponse = HttpContext.Current.Request.Params["g-recaptcha-response"];
                var validCaptcha = ReCaptcha.ValidateCaptcha(userResponse);
                if (!validCaptcha)
                {
                    HasError = true;
                    ErrorMessage = WebResources.CaptchaFailed;
                }
                else
                {
                    HasError = false;
                }
            }
            else
            {
                HasError = false;
            }
        }

        public static LoginModel GetModel(LoginModel model)
        {
            model.Languages = SystemLanguages.AllLanguages.OrderBy(x => x.DisplayOrder).Select(language => new KeyValuePair<string, string>(language.LanguageCode, language.LanguageName)).ToList();
            return model;
        }
    }
}

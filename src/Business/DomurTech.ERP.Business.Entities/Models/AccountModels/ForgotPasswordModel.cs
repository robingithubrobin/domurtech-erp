using System.Collections.Generic;

namespace DomurTech.ERP.Business.Entities.Models.AccountModels
{
    public class ForgotPasswordModel : BaseModel
    {
        public string CaptchaValue { get; set; }
        public string Email { get; set; }
        public string LanguageCode { get; set; }
        public List<KeyValuePair<string, string>> Languages { get; set; }
    }
}

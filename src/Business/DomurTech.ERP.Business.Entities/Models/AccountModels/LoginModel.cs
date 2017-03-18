using System.Collections.Generic;

namespace DomurTech.ERP.Business.Entities.Models.AccountModels
{
    public class LoginModel : BaseModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public string CaptchaValue { get; set; }
        public string LanguageCode { get; set; }
        public List<KeyValuePair<string,string>> Languages { get; set; }
    }
}

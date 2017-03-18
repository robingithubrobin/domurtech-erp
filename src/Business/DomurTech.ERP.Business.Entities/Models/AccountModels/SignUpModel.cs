using System.Collections.Generic;

namespace DomurTech.ERP.Business.Entities.Models.AccountModels
{
    public class SignUpModel : BaseModel
    {
        public string CaptchaValue { get; set; }
        public string LanguageCode { get; set; }
        public List<KeyValuePair<string, string>> Languages { get; set; }
    }
}

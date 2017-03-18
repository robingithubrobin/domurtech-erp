using System.Collections.Generic;
using DomurTech.ERP.Data.Entities.Concrete;

namespace DomurTech.ERP.Business.Entities.Models.AccountModels
{
    public class UpdateInformationModel : BaseModel
    {
        public List<RoleLanguageLine> RoleLanguageLines { get; set; }
        public string LanguageCode { get; set; }
        public List<KeyValuePair<string, string>> Languages { get; set; }
    }
}

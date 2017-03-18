using System.Collections.Generic;
using DomurTech.ERP.Business.Entities.Models.AccountModels;

namespace DomurTech.ERP.UI.Web.Application.Models
{
    public class HeaderModel
    {
        public string ApplicationName { get; set; }
        public AccountModel Account { get; set; }
        public List<KeyValuePair<string, string>> Languages { get; set; }
    }
}

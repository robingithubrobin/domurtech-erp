using System.Collections.Generic;
using DomurTech.ERP.Data.Entities.Concrete;

namespace DomurTech.ERP.UI.Web.Installation.Infrastructure.DefaultDatas
{
    internal class CountryDatas
    {
        public List<CountryLanguageLine> CountryLanguageLines = new List<CountryLanguageLine>
        {
            new CountryLanguageLine {Country = new Country {CountryCode = "TUR"}, CountryName = "Türkiye"}
        };
    }
}
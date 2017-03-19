using System.Collections.Generic;
using DomurTech.ERP.Data.Entities.Concrete;

namespace DomurTech.Installation.Common.DefaultDatas
{
    internal class CountryDatas
    {
        public List<CountryLanguageLine> CountryLanguageLines = new List<CountryLanguageLine>
        {
            new CountryLanguageLine {Country = new Country {CountryCode = "TUR"}, CountryName = "Türkiye"}
        };
    }
}
using System.Collections.Generic;
using DomurTech.ERP.Data.Entities.Concrete;

namespace DomurTech.ERP.UI.Web.Installation.Infrastructure.DefaultDatas
{
    internal class LanguageDatas
    {
        public List<Language> Languages = new List<Language>
        {
            new Language {LanguageCode = "tr-TR", LanguageName = "Türkçe"}
        };
    }
}
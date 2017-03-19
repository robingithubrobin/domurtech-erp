using System.Collections.Generic;
using DomurTech.ERP.Data.Entities.Concrete;

namespace DomurTech.Installation.Common.DefaultDatas
{
    internal class LanguageDatas
    {
        public List<Language> Languages = new List<Language>
        {
            new Language {LanguageCode = "tr-TR", LanguageName = "Türkçe"},
             new Language {LanguageCode = "en-US", LanguageName = "English"}
        };
    }
}
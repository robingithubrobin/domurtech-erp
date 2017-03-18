using System.Collections.Generic;
using DomurTech.Providers.Entities;

namespace DomurTech.Providers
{
    public static class SystemLanguages
    {
        private static readonly LanguageProvider LanguageProvider = new LanguageProvider();
        public static List<string> AllLanguageCodes = LanguageProvider.GetAllLanguageCodes();
        public static List<Language> AllLanguages = LanguageProvider.GetAllLanguages();
   
    }
}
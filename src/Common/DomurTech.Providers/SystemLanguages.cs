using System.Collections.Generic;
using DomurTech.Providers.Entities;

namespace DomurTech.Providers
{
    public static class SystemLanguages
    {
        public static List<string> AllLanguageCodes
        {
            get
            {
                using (var provider = new LanguageProvider())
                {
                    return provider.GetAllLanguageCodes();
                }
            }
        }
        public static List<Language> AllLanguages
        {
            get
            {
                using (var provider = new LanguageProvider())
                {
                    return provider.GetAllLanguages();
                }
            }
        }
    }
}
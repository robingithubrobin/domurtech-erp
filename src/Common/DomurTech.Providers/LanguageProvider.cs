using System;
using System.Collections.Generic;
using System.Linq;
using DomurTech.Providers.Abstract;
using DomurTech.Providers.Caching;
using DomurTech.Providers.DataAccess.EntityFramework;
using DomurTech.Providers.Entities;

namespace DomurTech.Providers
{
    internal class LanguageProvider : IDisposable
    {
        private IRepository<Language> _repositoryLanguage;
        private readonly MemoryCacheManager _memoryCacheManager = new MemoryCacheManager();

        public List<string> GetAllLanguageCodes()
        {
            List<string> list;
            const string cacheKey = "DomurTech.Providers.LanguageProvider.GetAllLanguageCodes";
            if (!_memoryCacheManager.Exists(cacheKey))
            {
                list = GetAllLanguageCodesFromDatabase();
                _memoryCacheManager.Add(cacheKey, list);
            }
            else
            {
                list = _memoryCacheManager.Get<List<string>>(cacheKey);
            }
            return list;
        }

        private List<string> GetAllLanguageCodesFromDatabase()
        {
            using (var context = new DatabaseContext())
            {
                _repositoryLanguage = new Repository<Language>(context);
                var query = _repositoryLanguage.Get();
                if (query == null) throw new Exception();
                var list = query.Where(e => e.IsApproved).OrderBy(e => e.DisplayOrder).Select(t => t.LanguageCode).ToList();
                return list;
            }
        }

        public List<Language> GetAllLanguages()
        {
            List<Language> list;
            const string cacheKey = "DomurTech.Providers.LanguageProvider.GetAllLanguages";
            if (!_memoryCacheManager.Exists(cacheKey))
            {
                list = GetAllLanguagesFromDatabase();
                _memoryCacheManager.Add(cacheKey, list);
            }
            else
            {
                list = _memoryCacheManager.Get<List<Language>>(cacheKey);
            }
            return list;
        }


        private List<Language> GetAllLanguagesFromDatabase()
        {
            using (var context = new DatabaseContext())
            {
                _repositoryLanguage = new Repository<Language>(context);
                var query = _repositoryLanguage.Get();
                if (query == null) throw new Exception();
                var list = query.Where(e => e.IsApproved).OrderBy(e=>e.DisplayOrder).ToList();
                return list;
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}

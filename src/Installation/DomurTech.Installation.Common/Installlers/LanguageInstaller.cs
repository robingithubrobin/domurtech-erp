using System;
using System.Collections.Generic;
using System.Linq;
using DomurTech.ERP.Data.Access.Abstract;
using DomurTech.ERP.Data.Entities.Concrete;
using DomurTech.Installation.Common.DefaultDatas;

namespace DomurTech.Installation.Common.Installlers
{
    public class LanguageInstaller
    {
        private readonly IRepository<Language> _repositoryLanguage;

        public LanguageInstaller(IRepository<Language> repositoryLanguage)
        {
            _repositoryLanguage = repositoryLanguage;
        }

              
        public Language Add(Language language)
        {
            var result=_repositoryLanguage.Add(language);
            _repositoryLanguage.SaveChanges();
            return result;
        }

        public Language GetFirst()
        {
            return _repositoryLanguage.Get().FirstOrDefault(x=>x.DisplayOrder==1);
        }

        public List<Language> GetList()
        {
            var result = new List<Language>();
            var list = new LanguageDatas().Languages;
            for (var i = 0; i < list.Count; i++)
            {
                var displayOrder = i + 1;
                result.Add(new Language
                {
                    Id = Guid.NewGuid(),
                    LanguageCode = list[i].LanguageCode,
                    LanguageName = list[i].LanguageName,
                    DisplayOrder = displayOrder,
                    IsApproved = true
                });

            }
            return result;
        }

        public bool Exists()
        {
            return _repositoryLanguage.Get().Any();
        }
    }
}
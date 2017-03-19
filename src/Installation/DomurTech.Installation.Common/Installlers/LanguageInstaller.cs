using System;
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
        
        public bool Exists()
        {
            return _repositoryLanguage.Get().Any();
        }

        public void Set()
        {
            var list = new LanguageDatas().Languages;

            for (var i = 0; i < list.Count; i++)
            {
                _repositoryLanguage.Add(new Language
                {
                    Id = Guid.NewGuid(),
                    LanguageCode = list[i].LanguageCode,
                    LanguageName = list[i].LanguageName,
                    DisplayOrder = i+1,
                    IsApproved = true
                });
            }
            _repositoryLanguage.SaveChanges();
        }
    }
}
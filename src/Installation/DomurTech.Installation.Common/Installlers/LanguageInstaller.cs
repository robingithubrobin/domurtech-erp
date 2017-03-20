using System;
using System.Linq;
using System.Threading;
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

        public string Set()
        {
            var result = string.Empty;
            var thread = new Thread(() =>
            {
                var list = new LanguageDatas().Languages;
                var totalCount = list.Count;
                for (var i = 0; i < list.Count; i++)
                {
                    var displayOrder = i + 1;
                    _repositoryLanguage.Add(new Language
                    {
                        Id = Guid.NewGuid(),
                        LanguageCode = list[i].LanguageCode,
                        LanguageName = list[i].LanguageName,
                        DisplayOrder = displayOrder,
                        IsApproved = true
                    });
                    result += "İşlem " + displayOrder + " / " + totalCount + " "+ list[i].LanguageCode;
                }
                _repositoryLanguage.SaveChanges();
            });
            thread.Start();
            return result;
        }
    }
}
using System;
using System.Linq;
using DomurTech.ERP.Data.Access.Abstract;
using DomurTech.ERP.Data.Entities.Concrete;
using DomurTech.Installation.Common.DefaultDatas;

namespace DomurTech.Installation.Common.Installlers
{
    public class CountryInstaller
    {
        private readonly IRepository<Country> _repositoryCountry;
        private readonly IRepository<CountryHistory> _repositoryCountryHistory;
        private readonly IRepository<CountryLanguageLine> _repositoryCountryLanguageLine;
        private readonly IRepository<CountryLanguageLineHistory> _repositoryCountryLanguageLineHistory;
        private readonly IRepository<Language> _repositoryLanguage;
        private readonly IRepository<User> _repositoryUser;

        public CountryInstaller(IRepository<Country> repositoryCountry, IRepository<CountryHistory> repositoryCountryHistory, IRepository<CountryLanguageLine> repositoryCountryLanguageLine, IRepository<CountryLanguageLineHistory> repositoryCountryLanguageLineHistory, IRepository<Language> repositoryLanguage, IRepository<User> repositoryUser)
        {
            _repositoryCountry = repositoryCountry;
            _repositoryCountryHistory = repositoryCountryHistory;
            _repositoryCountryLanguageLine = repositoryCountryLanguageLine;
            _repositoryCountryLanguageLineHistory = repositoryCountryLanguageLineHistory;
            _repositoryLanguage = repositoryLanguage;
            _repositoryUser = repositoryUser;

        }

        public void Set()
        {
            var dataList = new CountryDatas().CountryLanguageLines;

            for (var i = 0; i < dataList.Count; i++)
            {
                _repositoryCountry.Add(new Country
                {
                    Id = Guid.NewGuid(),
                    CountryCode = dataList[i].Country.CountryCode,
                    DisplayOrder = i + 1,
                    IsApproved = true,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    CreatedBy = _repositoryUser.Get().FirstOrDefault(x => x.DisplayOrder == 1),
                    UpdatedBy = _repositoryUser.Get().FirstOrDefault(x => x.DisplayOrder == 1)
                });
            }
            _repositoryCountry.SaveChanges();
            var items = _repositoryCountry.Get().ToList();
            foreach (var item in items)
            {
                _repositoryCountryHistory.Add(new CountryHistory
                {
                    Id = Guid.NewGuid(),
                    CountryId = item.Id,
                    CountryCode = item.CountryCode,
                    DisplayOrder = item.DisplayOrder,
                    IsApproved = item.IsApproved,
                    CreateDate = DateTime.Now,
                    CreatedBy = item.CreatedBy.Id,
                    VersionNo = 1,
                    RestoreVersionNo = 0,
                    IsDeleted = false
                });
            }
            _repositoryCountryHistory.SaveChanges();

            var user = _repositoryUser.Get().FirstOrDefault(x => x.DisplayOrder == 1);


            for (var i = 0; i < items.Count; i++)
            {
                foreach (var language in _repositoryLanguage.Get().Where(x => x.IsApproved).OrderBy(x => x.DisplayOrder))
                {
                    _repositoryCountryLanguageLine.Add(new CountryLanguageLine
                    {
                        Id = Guid.NewGuid(),
                        Country = items[i],
                        Language = language,
                        CountryName = dataList[i].CountryName,
                        CreateDate = DateTime.Now,
                        CreatedBy = user,
                        UpdateDate = DateTime.Now,
                        UpdatedBy = user
                    });
                }



            }
            _repositoryCountryLanguageLine.SaveChanges();

            foreach (var itemLanguageLine in _repositoryCountryLanguageLine.Get().ToList())
            {
                _repositoryCountryLanguageLineHistory.Add(new CountryLanguageLineHistory
                {
                    Id = Guid.NewGuid(),
                    CountryLanguageLineId = itemLanguageLine.Id,
                    CountryId = itemLanguageLine.Country.Id,
                    LanguageId = itemLanguageLine.Language.Id,
                    CreateDate = DateTime.Now,
                    CreatedBy = itemLanguageLine.CreatedBy.Id,
                    VersionNo = 1,
                    RestoreVersionNo = 0,
                    IsDeleted = false
                });
            }
            _repositoryCountryLanguageLineHistory.SaveChanges();
        }
    }
}
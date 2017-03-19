using System;
using System.Linq;
using DomurTech.ERP.Data.Access.Abstract;
using DomurTech.ERP.Data.Entities.Concrete;
using DomurTech.Installation.Common.DefaultDatas;

namespace DomurTech.Installation.Common.Installlers
{
    public class CityInstaller
    {
        private readonly IRepository<City> _repositoryCity;
        private readonly IRepository<CityHistory> _repositoryCityHistory;
        private readonly IRepository<CityLanguageLine> _repositoryCityLanguageLine;
        private readonly IRepository<CityLanguageLineHistory> _repositoryCityLanguageLineHistory;
        private readonly IRepository<Language> _repositoryLanguage;
        private readonly IRepository<User> _repositoryUser;
        private readonly IRepository<Country> _repositoryCountry;

        public CityInstaller(IRepository<City> repositoryCity, IRepository<CityHistory> repositoryCityHistory, IRepository<CityLanguageLine> repositoryCityLanguageLine, IRepository<CityLanguageLineHistory> repositoryCityLanguageLineHistory, IRepository<Language> repositoryLanguage, IRepository<User> repositoryUser, IRepository<Country> repositoryCountry)
        {
            _repositoryCity = repositoryCity;
            _repositoryCityHistory = repositoryCityHistory;
            _repositoryCityLanguageLine = repositoryCityLanguageLine;
            _repositoryCityLanguageLineHistory = repositoryCityLanguageLineHistory;
            _repositoryLanguage = repositoryLanguage;
            _repositoryUser = repositoryUser;
            _repositoryCountry = repositoryCountry;
        }

        public void Set()
        {
            var dataList = new CityDatas().CityLanguageLines;

            for (var i = 0; i < dataList.Count; i++)
            {
                var item = new City
                {
                    Id = Guid.NewGuid(),
                    CityCode = dataList[i].City.CityCode,
                    DisplayOrder = i + 1,
                    IsApproved = true,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    CreatedBy = _repositoryUser.Get().FirstOrDefault(x => x.DisplayOrder == 1),
                    UpdatedBy = _repositoryUser.Get().FirstOrDefault(x => x.DisplayOrder == 1),
                };
                var code = dataList[i].City.Country.CountryCode;
                item.Country = _repositoryCountry.Get().FirstOrDefault(x => x.CountryCode == code);
                
                _repositoryCity.Add(item);
            }
            _repositoryCity.SaveChanges();
            var items = _repositoryCity.Get().ToList();
            foreach (var item in items)
            {
                _repositoryCityHistory.Add(new CityHistory
                {
                    Id = Guid.NewGuid(),
                    CityId = item.Id,
                    CityCode = item.CityCode,
                    DisplayOrder = item.DisplayOrder,
                    IsApproved = item.IsApproved,
                    CountryId = item.Country.Id,
                    CreateDate = DateTime.Now,
                    CreatedBy = item.CreatedBy.Id,
                    VersionNo = 1,
                    RestoreVersionNo = 0,
                    IsDeleted = false
                });
            }
            _repositoryCityHistory.SaveChanges();

            var user = _repositoryUser.Get().FirstOrDefault(x => x.DisplayOrder == 1);
            for (var i = 0; i < items.Count; i++)
            {
                foreach (var language in _repositoryLanguage.Get().Where(x=>x.IsApproved).OrderBy(x=>x.DisplayOrder))
                {
                    _repositoryCityLanguageLine.Add(new CityLanguageLine
                    {
                        Id = Guid.NewGuid(),
                        City = items[i],
                        Language = language,
                        CityName = dataList[i].CityName,
                        CreateDate = DateTime.Now,
                        CreatedBy = user,
                        UpdateDate = DateTime.Now,
                        UpdatedBy = user
                    });
                }


                
            }
            _repositoryCityLanguageLine.SaveChanges();

            foreach (var itemLanguageLine in _repositoryCityLanguageLine.Get().ToList())
            {
                _repositoryCityLanguageLineHistory.Add(new CityLanguageLineHistory
                {
                    Id = Guid.NewGuid(),
                    CityLanguageLineId = itemLanguageLine.Id,
                    CityId = itemLanguageLine.City.Id,
                    LanguageId = itemLanguageLine.Language.Id,
                    CreateDate = DateTime.Now,
                    CreatedBy = itemLanguageLine.CreatedBy.Id,
                    VersionNo = 1,
                    RestoreVersionNo = 0,
                    IsDeleted = false
                });
            }
            _repositoryCityLanguageLineHistory.SaveChanges();
        }
    }
}
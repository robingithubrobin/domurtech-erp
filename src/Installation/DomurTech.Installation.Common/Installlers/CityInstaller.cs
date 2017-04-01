using System;
using System.Collections.Generic;
using System.Linq;
using DomurTech.ERP.Data.Access.Abstract;
using DomurTech.ERP.Data.Entities.Concrete;
using DomurTech.Installation.Common.DefaultDatas;

namespace DomurTech.Installation.Common.Installlers
{
    public class CityInstaller
    {
        private readonly IRepository<User> _repositoryUser;
        private readonly IRepository<City> _repositoryCity;
        private readonly IRepository<CityHistory> _repositoryCityHistory;
        private readonly IRepository<CityLanguageLine> _repositoryCityLanguageLine;
        private readonly IRepository<CityLanguageLineHistory> _repositoryCityLanguageLineHistory;
        private readonly IRepository<Language> _repositoryLanguage;
        private readonly IRepository<Country> _repositoryCountry;
        public CityInstaller(IRepository<User> repositoryUser, IRepository<City> repositoryCity, IRepository<CityHistory> repositoryCityHistory, IRepository<CityLanguageLine> repositoryCityLanguageLine, IRepository<CityLanguageLineHistory> repositoryCityLanguageLineHistory, IRepository<Language> repositoryLanguage, IRepository<Country> repositoryCountry)
        {
            _repositoryUser = repositoryUser;
            _repositoryCity = repositoryCity;
            _repositoryCityHistory = repositoryCityHistory;
            _repositoryCityLanguageLine = repositoryCityLanguageLine;
            _repositoryCityLanguageLineHistory = repositoryCityLanguageLineHistory;
            _repositoryLanguage = repositoryLanguage;
            _repositoryCountry = repositoryCountry;
        }

        public List<City> GetCityList()
        {
            var result = new List<City>();
            var dataList = new CityDatas().CityLanguageLines;
            for (var i = 0; i < dataList.Count; i++)
            {
                var dataListItem = dataList[i];
                var country = _repositoryCountry.Get().FirstOrDefault(x => x.CountryCode == dataListItem.City.Country.CountryCode);
                var item = new City
                {
                    Id = Guid.NewGuid(),
                    CityCode = dataListItem.City.CityCode,
                    Country = country,
                    DisplayOrder = i + 1,
                    IsApproved = true,
                    CreateDate = DateTime.Now
                };
                result.Add(item);
            }
            return result;
        }

        public List<CityHistory> GetCityHistoryList(List<City> items)
        {
            var list = new List<CityHistory>();
            var user = _repositoryUser.Get().FirstOrDefault(x => x.DisplayOrder == 1);
            if (user == null)
            {
                return new List<CityHistory>();
            }


            foreach (var item in items)
            {
                var itemHistory = new CityHistory
                {
                    Id = Guid.NewGuid(),
                    CityId = item.Id,
                    CityCode = item.CityCode,
                    CountryId = item.Country.Id,
                    DisplayOrder = item.DisplayOrder,
                    IsApproved = item.IsApproved,
                    CreateDate = DateTime.Now,
                    CreatedBy = user.Id,
                    VersionNo = 1,
                    RestoreVersionNo = 0,
                    IsDeleted = false
                };
                list.Add(itemHistory);
            }

            return list;

        }

        public List<CityLanguageLine> GetCityLanguageLineList(List<City> items)
        {
            var result = new List<CityLanguageLine>();
            var languages = _repositoryLanguage.Get();
            var dataList = new CityDatas().CityLanguageLines;
            for (var i = 0; i < items.Count(); i++)
            {
                foreach (var language in languages)
                {
                    var dataListItem = dataList[i].City;
                    var item = new CityLanguageLine
                    {
                        Id = Guid.NewGuid(),
                        City = items.FirstOrDefault(x => x.CityCode == dataListItem.CityCode),
                        Language = language,
                        CityName = dataList[i].CityName,
                        CreateDate = DateTime.Now
                    };
                    result.Add(item);
                }
            }

            return result;
        }

        public List<CityLanguageLineHistory> GetCityLanguageLineHistoryList(List<CityLanguageLine> itemLanguageLines)
        {
            var result = new List<CityLanguageLineHistory>();
            var user = _repositoryUser.Get().FirstOrDefault(x => x.DisplayOrder == 1);
            if (user == null)
            {
                return new List<CityLanguageLineHistory>();
            }

            foreach (var itemLanguageLine in itemLanguageLines)
            {
                var item = new CityLanguageLineHistory
                {
                    Id = Guid.NewGuid(),
                    CityLanguageLineId = itemLanguageLine.Id,
                    CityId = itemLanguageLine.City.Id,
                    LanguageId = itemLanguageLine.Language.Id,
                    CreateDate = DateTime.Now,
                    CreatedBy = user.Id,
                    VersionNo = 1,
                    RestoreVersionNo = 0,
                    IsDeleted = false
                };
                result.Add(item);

            }
            return result;
        }

        public City AddCity(City item)
        {
            var result = _repositoryCity.Add(item);
            _repositoryCity.SaveChanges();
            return result;
        }

        public void AddCityHistory(CityHistory itemHistory)
        {
            _repositoryCityHistory.Add(itemHistory);
            _repositoryCityHistory.SaveChanges();
        }

        public CityLanguageLine AddCityLanguageLine(CityLanguageLine itemLanguageLine)
        {
            var result = _repositoryCityLanguageLine.Add(itemLanguageLine);
            _repositoryCityLanguageLine.SaveChanges();
            return result;
        }

        public void AddCityLanguageLineHistory(CityLanguageLineHistory itemLanguageLineHistory)
        {
            _repositoryCityLanguageLineHistory.Add(itemLanguageLineHistory);
            _repositoryCityLanguageLineHistory.SaveChanges();
        }

        public List<City> GetAllCities()
        {
            return _repositoryCity.Get().ToList();
        }
        public List<CityLanguageLine> GetAllCityLanguageLines()
        {
            return _repositoryCityLanguageLine.Get().ToList();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using DomurTech.ERP.Data.Access.Abstract;
using DomurTech.ERP.Data.Entities.Concrete;
using DomurTech.Installation.Common.DefaultDatas;

namespace DomurTech.Installation.Common.Installlers
{
    public class CountryInstaller
    {
        private readonly IRepository<User> _repositoryUser;
        private readonly IRepository<Country> _repositoryCountry;
        private readonly IRepository<CountryHistory> _repositoryCountryHistory;
        private readonly IRepository<CountryLanguageLine> _repositoryCountryLanguageLine;
        private readonly IRepository<CountryLanguageLineHistory> _repositoryCountryLanguageLineHistory;
        private readonly IRepository<Language> _repositoryLanguage;
        public CountryInstaller(IRepository<User> repositoryUser, IRepository<Country> repositoryCountry, IRepository<CountryHistory> repositoryCountryHistory, IRepository<CountryLanguageLine> repositoryCountryLanguageLine, IRepository<CountryLanguageLineHistory> repositoryCountryLanguageLineHistory, IRepository<Language> repositoryLanguage)
        {
            _repositoryUser = repositoryUser;
            _repositoryCountry = repositoryCountry;
            _repositoryCountryHistory = repositoryCountryHistory;
            _repositoryCountryLanguageLine = repositoryCountryLanguageLine;
            _repositoryCountryLanguageLineHistory = repositoryCountryLanguageLineHistory;
            _repositoryLanguage = repositoryLanguage;
        }

        public List<Country> GetCountryList()
        {
            var dataList = new CountryDatas().CountryLanguageLines;
            return dataList.Select((dataListItem, i) => new Country
            {
                Id = Guid.NewGuid(),
                CountryCode = dataListItem.Country.CountryCode,
                DisplayOrder = i + 1,
                IsApproved = true,
                CreateDate = DateTime.Now,
            }).ToList();
        }

        public List<CountryHistory> GetCountryHistoryList(List<Country> items)
        {
            var list = new List<CountryHistory>();
            var user = _repositoryUser.Get().FirstOrDefault(x => x.DisplayOrder == 1);
            if (user == null)
            {
                return new List<CountryHistory>();
            }


            foreach (var item in items)
            {
                var itemHistory = new CountryHistory
                {
                    Id = Guid.NewGuid(),
                    CountryId = item.Id,
                    CountryCode = item.CountryCode,
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

        public List<CountryLanguageLine> GetCountryLanguageLineList(List<Country> items)
        {
            var result = new List<CountryLanguageLine>();
            var languages = _repositoryLanguage.Get();
            var dataList = new CountryDatas().CountryLanguageLines;
            for (var i = 0; i < items.Count(); i++)
            {
                foreach (var language in languages)
                {
                    var dataListItem = dataList[i].Country;
                    var item = new CountryLanguageLine
                    {
                        Id = Guid.NewGuid(),
                        Country = items.FirstOrDefault(x => x.CountryCode == dataListItem.CountryCode),
                        Language = language,
                        CountryName = dataList[i].CountryName,
                        CreateDate = DateTime.Now
                    };
                    result.Add(item);
                }
            }

            return result;
        }

        public List<CountryLanguageLineHistory> GetCountryLanguageLineHistoryList(List<CountryLanguageLine> itemLanguageLines)
        {
            var result = new List<CountryLanguageLineHistory>();
            var user = _repositoryUser.Get().FirstOrDefault(x => x.DisplayOrder == 1);
            if (user == null)
            {
                return new List<CountryLanguageLineHistory>();
            }

            foreach (var itemLanguageLine in itemLanguageLines)
            {
                var item = new CountryLanguageLineHistory
                {
                    Id = Guid.NewGuid(),
                    CountryLanguageLineId = itemLanguageLine.Id,
                    CountryId = itemLanguageLine.Country.Id,
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

        public Country AddCountry(Country item)
        {
            var result = _repositoryCountry.Add(item);
            _repositoryCountry.SaveChanges();
            return result;
        }

        public void AddCountryHistory(CountryHistory itemHistory)
        {
            _repositoryCountryHistory.Add(itemHistory);
            _repositoryCountryHistory.SaveChanges();
        }

        public CountryLanguageLine AddCountryLanguageLine(CountryLanguageLine itemLanguageLine)
        {
            var result = _repositoryCountryLanguageLine.Add(itemLanguageLine);
            _repositoryCountryLanguageLine.SaveChanges();
            return result;
        }

        public void AddCountryLanguageLineHistory(CountryLanguageLineHistory itemLanguageLineHistory)
        {
            _repositoryCountryLanguageLineHistory.Add(itemLanguageLineHistory);
            _repositoryCountryLanguageLineHistory.SaveChanges();
        }

        public List<Country> GetAllCountries()
        {
            return _repositoryCountry.Get().ToList();
        }
        public List<CountryLanguageLine> GetAllCountryLanguageLines()
        {
            return _repositoryCountryLanguageLine.Get().ToList();
        }
    }
}
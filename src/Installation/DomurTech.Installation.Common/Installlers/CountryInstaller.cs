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

        public CountryInstaller(IRepository<User> repositoryUser)
        {
            _repositoryUser = repositoryUser;
        }

        public List<Country> GetList()
        {
            var dataList = new CountryDatas().CountryLanguageLines;
            return dataList.Select((t, i) => new Country
            {
                Id = Guid.NewGuid(),
                CountryCode = t.Country.CountryCode,
                DisplayOrder = i + 1,
                IsApproved = true,
                CreateDate = DateTime.Now,
            }).ToList();
        }

        public List<CountryHistory> GetList(List<Country> countries)
        {
            var user = _repositoryUser.Get().FirstOrDefault(x => x.DisplayOrder == 1);
            if (user == null)
            {
                return new List<CountryHistory>();
            }
            return countries.Select(item => new CountryHistory
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
            }).ToList();

        }

        public List<CountryLanguageLine> GetList(List<Country> items, List<Language> languages)
        {
            var dataList = new CountryDatas().CountryLanguageLines;
            var result = new List<CountryLanguageLine>();
            for (var i = 0; i < items.Count; i++)
            {
                result.AddRange(languages.Select(language => new CountryLanguageLine
                {
                    Id = Guid.NewGuid(),
                    Country = items[i],
                    Language = language,
                    CountryName = dataList[i].CountryName,
                    CreateDate = DateTime.Now
                }));
            }

            return result;
        }

        public List<CountryLanguageLineHistory> GetList(List<CountryLanguageLine> itemLanguageLines)
        {
            var user = _repositoryUser.Get().FirstOrDefault(x => x.DisplayOrder == 1);
            if (user == null)
            {
                return new List<CountryLanguageLineHistory>();
            }
            return itemLanguageLines.Select(itemLanguageLine => new CountryLanguageLineHistory
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
            }).ToList();
        }

    }
}
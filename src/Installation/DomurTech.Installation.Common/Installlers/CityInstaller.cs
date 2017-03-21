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
        private readonly IRepository<Country> _repositoryCountry;

        public CityInstaller(IRepository<User> repositoryUser, IRepository<Country> repositoryCountry)
        {
            _repositoryUser = repositoryUser;
            _repositoryCountry = repositoryCountry;
        }

        public List<City> GetList()
        {
            var result = new List<City>();
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
                };
                var code = dataList[i].City.Country.CountryCode;
                item.Country = _repositoryCountry.Get().FirstOrDefault(x => x.CountryCode == code);
                result.Add(item);
            }
            return result;
        }

        public List<CityHistory> GetList(List<City> items)
        {
            var user = _repositoryUser.Get().FirstOrDefault(x => x.DisplayOrder == 1);
            if (user == null)
            {
                return new List<CityHistory>();
            }
            return items.Select(item => new CityHistory
            {
                Id = Guid.NewGuid(),
                CityId = item.Id,
                CityCode = item.CityCode,
                DisplayOrder = item.DisplayOrder,
                IsApproved = item.IsApproved,
                CountryId = item.Country.Id,
                CreateDate = DateTime.Now,
                CreatedBy = user.Id,
                VersionNo = 1,
                RestoreVersionNo = 0,
                IsDeleted = false
            }).ToList();
            
        }

        public List<CityLanguageLine> GetList(List<City> items, List<Language> languages)
        {
            var dataList = new CityDatas().CityLanguageLines;
            var result=new List<CityLanguageLine>();
            for (var i = 0; i < items.Count; i++)
            {
                result.AddRange(languages.Select(language => new CityLanguageLine
                {
                    Id = Guid.NewGuid(),
                    City = items[i],
                    Language = language,
                    CityName = dataList[i].CityName,
                    CreateDate = DateTime.Now
                }));
            }
            
            return result;
        }

        public List<CityLanguageLineHistory> GetList(List<CityLanguageLine> itemLanguageLines)
        {
            var user = _repositoryUser.Get().FirstOrDefault(x => x.DisplayOrder == 1);
            if (user == null)
            {
                return new List<CityLanguageLineHistory>();
            }
            return itemLanguageLines.Select(itemLanguageLine => new CityLanguageLineHistory
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
            }).ToList();
        } 
        
    }
}
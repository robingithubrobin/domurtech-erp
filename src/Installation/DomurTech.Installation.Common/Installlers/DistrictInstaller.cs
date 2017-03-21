using System;
using System.Collections.Generic;
using System.Linq;
using DomurTech.ERP.Data.Access.Abstract;
using DomurTech.ERP.Data.Entities.Concrete;
using DomurTech.Installation.Common.DefaultDatas;

namespace DomurTech.Installation.Common.Installlers
{
    public class DistrictInstaller
    {
        private readonly IRepository<User> _repositoryUser;
        private readonly IRepository<City> _repositoryCity;

        public DistrictInstaller(IRepository<User> repositoryUser, IRepository<City> repositoryCity)
        {
            _repositoryUser = repositoryUser;
            _repositoryCity = repositoryCity;
        }

        public List<District> GetList()
        {
            var result = new List<District>();
            var dataList = new DistrictDatas().DistrictLanguageLines;
            for (var i = 0; i < dataList.Count; i++)
            {
                var item = new District
                {
                    Id = Guid.NewGuid(),
                    DistrictCode = dataList[i].District.DistrictCode,
                    DisplayOrder = i + 1,
                    IsApproved = true,
                    CreateDate = DateTime.Now,
                };
                var code = dataList[i].District.City.CityCode;
                item.City = _repositoryCity.Get().FirstOrDefault(x => x.CityCode == code);
                result.Add(item);
            }
            return result;
        }

        public List<DistrictHistory> GetList(List<District> items)
        {
            var user = _repositoryUser.Get().FirstOrDefault(x => x.DisplayOrder == 1);
            if (user == null)
            {
                return new List<DistrictHistory>();
            }
            return items.Select(item => new DistrictHistory
            {
                Id = Guid.NewGuid(),
                DistrictId = item.Id,
                DistrictCode = item.DistrictCode,
                DisplayOrder = item.DisplayOrder,
                IsApproved = item.IsApproved,
                CityId = item.City.Id,
                CreateDate = DateTime.Now,
                CreatedBy = user.Id,
                VersionNo = 1,
                RestoreVersionNo = 0,
                IsDeleted = false
            }).ToList();

        }

        public List<DistrictLanguageLine> GetList(List<District> items, List<Language> languages)
        {
            var dataList = new DistrictDatas().DistrictLanguageLines;
            var result = new List<DistrictLanguageLine>();
            for (var i = 0; i < items.Count; i++)
            {
                result.AddRange(languages.Select(language => new DistrictLanguageLine
                {
                    Id = Guid.NewGuid(),
                    District = items[i],
                    Language = language,
                    DistrictName = dataList[i].DistrictName,
                    CreateDate = DateTime.Now
                }));
            }

            return result;
        }

        public List<DistrictLanguageLineHistory> GetList(List<DistrictLanguageLine> itemLanguageLines)
        {
            var user = _repositoryUser.Get().FirstOrDefault(x => x.DisplayOrder == 1);
            if (user == null)
            {
                return new List<DistrictLanguageLineHistory>();
            }
            return itemLanguageLines.Select(itemLanguageLine => new DistrictLanguageLineHistory
            {
                Id = Guid.NewGuid(),
                DistrictLanguageLineId = itemLanguageLine.Id,
                DistrictId = itemLanguageLine.District.Id,
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
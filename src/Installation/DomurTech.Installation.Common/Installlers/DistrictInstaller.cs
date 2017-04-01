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
        private readonly IRepository<District> _repositoryDistrict;
        private readonly IRepository<DistrictHistory> _repositoryDistrictHistory;
        private readonly IRepository<DistrictLanguageLine> _repositoryDistrictLanguageLine;
        private readonly IRepository<DistrictLanguageLineHistory> _repositoryDistrictLanguageLineHistory;
        private readonly IRepository<Language> _repositoryLanguage;
        private readonly IRepository<City> _repositoryCity;
        public DistrictInstaller(IRepository<User> repositoryUser, IRepository<District> repositoryDistrict, IRepository<DistrictHistory> repositoryDistrictHistory, IRepository<DistrictLanguageLine> repositoryDistrictLanguageLine, IRepository<DistrictLanguageLineHistory> repositoryDistrictLanguageLineHistory, IRepository<Language> repositoryLanguage, IRepository<City> repositoryCity)
        {
            _repositoryUser = repositoryUser;
            _repositoryDistrict = repositoryDistrict;
            _repositoryDistrictHistory = repositoryDistrictHistory;
            _repositoryDistrictLanguageLine = repositoryDistrictLanguageLine;
            _repositoryDistrictLanguageLineHistory = repositoryDistrictLanguageLineHistory;
            _repositoryLanguage = repositoryLanguage;
            _repositoryCity = repositoryCity;
        }

        public List<District> GetDistrictList()
        {
            var result = new List<District>();
            var dataList = new DistrictDatas().DistrictLanguageLines;
            for (var i = 0; i < dataList.Count; i++)
            {
                var dataListItem = dataList[i];
                var city = _repositoryCity.Get().FirstOrDefault(x => x.CityCode == dataListItem.District.City.CityCode);
                var item = new District
                {
                    Id = Guid.NewGuid(),
                    DistrictCode = dataListItem.District.DistrictCode,
                    City = city,
                    DisplayOrder = i + 1,
                    IsApproved = true,
                    CreateDate = DateTime.Now
                };
                result.Add(item);
            }
            return result;
        }

        public List<DistrictHistory> GetDistrictHistoryList(List<District> items)
        {
            var list = new List<DistrictHistory>();
            var user = _repositoryUser.Get().FirstOrDefault(x => x.DisplayOrder == 1);
            if (user == null)
            {
                return new List<DistrictHistory>();
            }


            foreach (var item in items)
            {
                var itemHistory = new DistrictHistory
                {
                    Id = Guid.NewGuid(),
                    DistrictId = item.Id,
                    DistrictCode = item.DistrictCode,
                    CityId = item.City.Id,
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

        public List<DistrictLanguageLine> GetDistrictLanguageLineList(List<District> items)
        {
            var result = new List<DistrictLanguageLine>();
            var languages = _repositoryLanguage.Get();
            var dataList = new DistrictDatas().DistrictLanguageLines;
            for (var i = 0; i < items.Count(); i++)
            {
                foreach (var language in languages)
                {
                    var dataListItem = dataList[i].District;
                    var item = new DistrictLanguageLine
                    {
                        Id = Guid.NewGuid(),
                        District = items.FirstOrDefault(x => x.DistrictCode == dataListItem.DistrictCode),
                        Language = language,
                        DistrictName = dataList[i].DistrictName,
                        CreateDate = DateTime.Now
                    };
                    result.Add(item);
                }
            }

            return result;
        }

        public List<DistrictLanguageLineHistory> GetDistrictLanguageLineHistoryList(List<DistrictLanguageLine> itemLanguageLines)
        {
            var result = new List<DistrictLanguageLineHistory>();
            var user = _repositoryUser.Get().FirstOrDefault(x => x.DisplayOrder == 1);
            if (user == null)
            {
                return new List<DistrictLanguageLineHistory>();
            }

            foreach (var itemLanguageLine in itemLanguageLines)
            {
                var item = new DistrictLanguageLineHistory
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
                };
                result.Add(item);

            }
            return result;
        }

        public District AddDistrict(District item)
        {
            var result = _repositoryDistrict.Add(item);
            _repositoryDistrict.SaveChanges();
            return result;
        }

        public void AddDistrictHistory(DistrictHistory itemHistory)
        {
            _repositoryDistrictHistory.Add(itemHistory);
            _repositoryDistrictHistory.SaveChanges();
        }

        public DistrictLanguageLine AddDistrictLanguageLine(DistrictLanguageLine itemLanguageLine)
        {
            var result = _repositoryDistrictLanguageLine.Add(itemLanguageLine);
            _repositoryDistrictLanguageLine.SaveChanges();
            return result;
        }

        public void AddDistrictLanguageLineHistory(DistrictLanguageLineHistory itemLanguageLineHistory)
        {
            _repositoryDistrictLanguageLineHistory.Add(itemLanguageLineHistory);
            _repositoryDistrictLanguageLineHistory.SaveChanges();
        }

        public List<District> GetAllDistricts()
        {
            return _repositoryDistrict.Get().ToList();
        }
        public List<DistrictLanguageLine> GetAllDistrictLanguageLines()
        {
            return _repositoryDistrictLanguageLine.Get().ToList();
        }
    }
}
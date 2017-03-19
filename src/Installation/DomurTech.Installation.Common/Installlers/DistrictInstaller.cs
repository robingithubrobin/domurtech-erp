using System;
using System.Linq;
using DomurTech.ERP.Data.Access.Abstract;
using DomurTech.ERP.Data.Entities.Concrete;
using DomurTech.Installation.Common.DefaultDatas;

namespace DomurTech.Installation.Common.Installlers
{
    public class DistrictInstaller
    {
        private readonly IRepository<District> _repositoryDistrict;
        private readonly IRepository<DistrictHistory> _repositoryDistrictHistory;
        private readonly IRepository<DistrictLanguageLine> _repositoryDistrictLanguageLine;
        private readonly IRepository<DistrictLanguageLineHistory> _repositoryDistrictLanguageLineHistory;
        private readonly IRepository<Language> _repositoryLanguage;
        private readonly IRepository<User> _repositoryUser;
        private readonly IRepository<City> _repositoryCity;

        public DistrictInstaller(IRepository<District> repositoryDistrict, IRepository<DistrictHistory> repositoryDistrictHistory, IRepository<DistrictLanguageLine> repositoryDistrictLanguageLine, IRepository<DistrictLanguageLineHistory> repositoryDistrictLanguageLineHistory, IRepository<Language> repositoryLanguage, IRepository<User> repositoryUser, IRepository<City> repositoryCity)
        {
            _repositoryDistrict = repositoryDistrict;
            _repositoryDistrictHistory = repositoryDistrictHistory;
            _repositoryDistrictLanguageLine = repositoryDistrictLanguageLine;
            _repositoryDistrictLanguageLineHistory = repositoryDistrictLanguageLineHistory;
            _repositoryLanguage = repositoryLanguage;
            _repositoryUser = repositoryUser;
            _repositoryCity = repositoryCity;
        }

        public void Set()
        {
            var dataList = new DistrictDatas().DistrictLanguageLines;

            for (var i = 0; i < dataList.Count; i++)
            {
                var districtCode = dataList[i].District.DistrictCode;
                var cityCode = dataList[i].District.City.CityCode;
                var item = new District
                {
                    Id = Guid.NewGuid(),
                    DistrictCode = districtCode,
                    DisplayOrder = i + 1,
                    IsApproved = true,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    CreatedBy = _repositoryUser.Get().FirstOrDefault(x => x.DisplayOrder == 1),
                    UpdatedBy = _repositoryUser.Get().FirstOrDefault(x => x.DisplayOrder == 1),
                    City = _repositoryCity.Get().FirstOrDefault(x => x.CityCode == cityCode)
                };
                _repositoryDistrict.Add(item);
                _repositoryDistrict.SaveChanges();
            }
            

            var items = _repositoryDistrict.Get().ToList();
            foreach (var item in items)
            {
                _repositoryDistrictHistory.Add(new DistrictHistory
                {
                    Id = Guid.NewGuid(),
                    DistrictId = item.Id,
                    DistrictCode = item.DistrictCode,
                    DisplayOrder = item.DisplayOrder,
                    IsApproved = item.IsApproved,
                    CityId = item.City.Id,
                    CreateDate = DateTime.Now,
                    CreatedBy = item.CreatedBy.Id,
                    VersionNo = 1,
                    RestoreVersionNo = 0,
                    IsDeleted = false
                });
            }
            _repositoryDistrictHistory.SaveChanges();

            var user = _repositoryUser.Get().FirstOrDefault(x => x.DisplayOrder == 1);

           
            for (var i = 0; i < items.Count; i++)
            {

                foreach (var language in _repositoryLanguage.Get().Where(x => x.IsApproved).OrderBy(x => x.DisplayOrder))
                {
                    _repositoryDistrictLanguageLine.Add(new DistrictLanguageLine
                    {
                        Id = Guid.NewGuid(),
                        District = items[i],
                        Language = language,
                        DistrictName = dataList[i].DistrictName,
                        CreateDate = DateTime.Now,
                        CreatedBy = user,
                        UpdateDate = DateTime.Now,
                        UpdatedBy = user
                    });
                }

                    
            }
            _repositoryDistrictLanguageLine.SaveChanges();

            foreach (var itemLanguageLine in _repositoryDistrictLanguageLine.Get().ToList())
            {
                _repositoryDistrictLanguageLineHistory.Add(new DistrictLanguageLineHistory
                {
                    Id = Guid.NewGuid(),
                    DistrictLanguageLineId = itemLanguageLine.Id,
                    DistrictId = itemLanguageLine.District.Id,
                    LanguageId = itemLanguageLine.Language.Id,
                    CreateDate = DateTime.Now,
                    CreatedBy = itemLanguageLine.CreatedBy.Id,
                    VersionNo = 1,
                    RestoreVersionNo = 0,
                    IsDeleted = false
                });
            }
            _repositoryDistrictLanguageLineHistory.SaveChanges();
        }
    }
}
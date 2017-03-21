using System;
using System.Collections.Generic;
using System.Linq;
using DomurTech.ERP.Data.Access.Abstract;
using DomurTech.ERP.Data.Entities.Concrete;
using DomurTech.Installation.Common.DefaultDatas;

namespace DomurTech.Installation.Common.Installlers
{
    public class RoleInstaller
    {
        private readonly IRepository<User> _repositoryUser;

        public RoleInstaller(IRepository<User> repositoryUser)
        {
            _repositoryUser = repositoryUser;
        }

        public List<Role> GetList()
        {
            var dataList = new RoleDatas().RoleLanguageLines;
            return dataList.Select((t, i) => new Role
            {
                Id = Guid.NewGuid(),
                RoleCode = t.Role.RoleCode,
                DisplayOrder = i + 1,
                IsApproved = true,
                CreateDate = DateTime.Now,
            }).ToList();
        }

        public List<RoleHistory> GetList(List<Role> countries)
        {
            var user = _repositoryUser.Get().FirstOrDefault(x => x.DisplayOrder == 1);
            if (user == null)
            {
                return new List<RoleHistory>();
            }
            return countries.Select(item => new RoleHistory
            {
                Id = Guid.NewGuid(),
                RoleId = item.Id,
                RoleCode = item.RoleCode,
                DisplayOrder = item.DisplayOrder,
                IsApproved = item.IsApproved,
                CreateDate = DateTime.Now,
                CreatedBy = user.Id,
                VersionNo = 1,
                RestoreVersionNo = 0,
                IsDeleted = false
            }).ToList();

        }

        public List<RoleLanguageLine> GetList(List<Role> items, List<Language> languages)
        {
            var dataList = new RoleDatas().RoleLanguageLines;
            var result = new List<RoleLanguageLine>();
            for (var i = 0; i < items.Count; i++)
            {
                result.AddRange(languages.Select(language => new RoleLanguageLine
                {
                    Id = Guid.NewGuid(),
                    Role = items[i],
                    Language = language,
                    RoleName = dataList[i].RoleName,
                    CreateDate = DateTime.Now
                }));
            }

            return result;
        }

        public List<RoleLanguageLineHistory> GetList(List<RoleLanguageLine> itemLanguageLines)
        {
            var user = _repositoryUser.Get().FirstOrDefault(x => x.DisplayOrder == 1);
            if (user == null)
            {
                return new List<RoleLanguageLineHistory>();
            }
            return itemLanguageLines.Select(itemLanguageLine => new RoleLanguageLineHistory
            {
                Id = Guid.NewGuid(),
                RoleLanguageLineId = itemLanguageLine.Id,
                RoleId = itemLanguageLine.Role.Id,
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
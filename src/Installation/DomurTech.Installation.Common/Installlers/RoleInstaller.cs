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
        private readonly IRepository<Role> _repositoryRole;
        private readonly IRepository<RoleHistory> _repositoryRoleHistory;
        private readonly IRepository<RoleLanguageLine> _repositoryRoleLanguageLine;
        private readonly IRepository<RoleLanguageLineHistory> _repositoryRoleLanguageLineHistory;
        private readonly IRepository<Language> _repositoryLanguage;
        public RoleInstaller(IRepository<User> repositoryUser, IRepository<Role> repositoryRole, IRepository<RoleHistory> repositoryRoleHistory, IRepository<RoleLanguageLine> repositoryRoleLanguageLine, IRepository<RoleLanguageLineHistory> repositoryRoleLanguageLineHistory, IRepository<Language> repositoryLanguage)
        {
            _repositoryUser = repositoryUser;
            _repositoryRole = repositoryRole;
            _repositoryRoleHistory = repositoryRoleHistory;
            _repositoryRoleLanguageLine = repositoryRoleLanguageLine;
            _repositoryRoleLanguageLineHistory = repositoryRoleLanguageLineHistory;
            _repositoryLanguage = repositoryLanguage;
        }

        public List<Role> GetRoleList()
        {
            var dataList = new RoleDatas().RoleLanguageLines;
            return dataList.Select((dataListItem, i) => new Role
            {
                Id = Guid.NewGuid(),
                RoleCode = dataListItem.Role.RoleCode,
                DisplayOrder = i + 1,
                IsApproved = true,
                CreateDate = DateTime.Now
                
            }).ToList();
        }

        public List<RoleHistory> GetRoleHistoryList(List<Role> items)
        {
            var list=new List<RoleHistory>();
            var user = _repositoryUser.Get().FirstOrDefault(x => x.DisplayOrder == 1);
            if (user == null)
            {
                return new List<RoleHistory>();
            }


            foreach (var item in items)
            {
                var itemHistory=new RoleHistory
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
                };
                list.Add(itemHistory);
            }

            return list;

        }

        public List<RoleLanguageLine> GetRoleLanguageLineList(List<Role> items)
        {
            var result = new List<RoleLanguageLine>();
            var languages = _repositoryLanguage.Get();
            var dataList = new RoleDatas().RoleLanguageLines;
            for (var i = 0; i < items.Count(); i++)
            {
                foreach (var language in languages)
                {
                    var dataListItem = dataList[i].Role;
                    var item = new RoleLanguageLine
                    {
                        Id = Guid.NewGuid(),
                        Role = items.FirstOrDefault(x=>x.RoleCode==dataListItem.RoleCode),
                        Language = language,
                        RoleName = dataList[i].RoleName,
                        CreateDate = DateTime.Now
                    };
                    result.Add(item);
                }
            }

            return result;
        }

        public List<RoleLanguageLineHistory> GetRoleLanguageLineHistoryList(List<RoleLanguageLine> itemLanguageLines)
        {
            var result= new List<RoleLanguageLineHistory>();
            var user = _repositoryUser.Get().FirstOrDefault(x => x.DisplayOrder == 1);
            if (user == null)
            {
                return new List<RoleLanguageLineHistory>();
            }

            foreach (var itemLanguageLine in itemLanguageLines)
            {
                var item = new RoleLanguageLineHistory
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
                };
                result.Add(item);

            }
            return result;
        }

        public Role AddRole(Role role)
        {
            var result= _repositoryRole.Add(role);
            _repositoryRole.SaveChanges();
            return result;
        }

        public void AddRoleHistory(RoleHistory roleHistory)
        {
            _repositoryRoleHistory.Add(roleHistory);
            _repositoryRoleHistory.SaveChanges();
        }

        public RoleLanguageLine AddRoleLanguageLine(RoleLanguageLine roleLanguageLine)
        {
            var result = _repositoryRoleLanguageLine.Add(roleLanguageLine);
            _repositoryRoleLanguageLine.SaveChanges();
            return result;
        }

        public void AddRoleLanguageLineHistory(RoleLanguageLineHistory roleLanguageLineHistory)
        {
            _repositoryRoleLanguageLineHistory.Add(roleLanguageLineHistory);
            _repositoryRoleLanguageLineHistory.SaveChanges();
        }

        public List<Role> GetAllRoles()
        {
            return _repositoryRole.Get().ToList();
        }
        public List<RoleLanguageLine> GetAllRoleLanguageLines()
        {
            return _repositoryRoleLanguageLine.Get().ToList();
        }
    }
}
using System;
using System.Linq;
using DomurTech.ERP.Data.Access.Abstract;
using DomurTech.ERP.Data.Entities.Concrete;
using DomurTech.ERP.UI.Web.Installation.Infrastructure.DefaultDatas;

namespace DomurTech.ERP.UI.Web.Installation.Infrastructure.Installlers
{
    internal class RoleInstaller
    {
        private readonly IRepository<Role> _repositoryRole;
        private readonly IRepository<RoleHistory> _repositoryRoleHistory;
        private readonly IRepository<RoleLanguageLine> _repositoryRoleLanguageLine;
        private readonly IRepository<RoleLanguageLineHistory> _repositoryRoleLanguageLineHistory;
        private readonly IRepository<Language> _repositoryLanguage;
        private readonly IRepository<User> _repositoryUser;

        public RoleInstaller(IRepository<Role> repositoryRole, IRepository<RoleHistory> repositoryRoleHistory, IRepository<RoleLanguageLine> repositoryRoleLanguageLine, IRepository<RoleLanguageLineHistory> repositoryRoleLanguageLineHistory, IRepository<Language> repositoryLanguage, IRepository<User> repositoryUser)
        {
            _repositoryRole = repositoryRole;
            _repositoryRoleHistory = repositoryRoleHistory;
            _repositoryRoleLanguageLine = repositoryRoleLanguageLine;
            _repositoryRoleLanguageLineHistory = repositoryRoleLanguageLineHistory;
            _repositoryLanguage = repositoryLanguage;
            _repositoryUser = repositoryUser;
            
        }

        public void Set()
        {
            var dataList = new RoleDatas().RoleLanguageLines;

            for (var i = 0; i < dataList.Count; i++)
            {
                _repositoryRole.Add(new Role
                {
                    Id = Guid.NewGuid(),
                    RoleCode = dataList[i].Role.RoleCode,
                    DisplayOrder = i + 1,
                    IsApproved = true,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    CreatedBy = _repositoryUser.Get().FirstOrDefault(x => x.DisplayOrder == 1),
                    UpdatedBy = _repositoryUser.Get().FirstOrDefault(x => x.DisplayOrder == 1)
                });
            }
            _repositoryRole.SaveChanges();
            var items = _repositoryRole.Get().ToList();
            foreach (var item in items)
            {
                _repositoryRoleHistory.Add(new RoleHistory
                {
                    Id = Guid.NewGuid(),
                    RoleId = item.Id,
                    RoleCode = item.RoleCode,
                    DisplayOrder = item.DisplayOrder,
                    IsApproved = item.IsApproved,
                    CreateDate = DateTime.Now,
                    CreatedBy = item.CreatedBy.Id,
                    VersionNo = 1,
                    RestoreVersionNo = 0,
                    IsDeleted = false
                });
            }
            _repositoryRoleHistory.SaveChanges();

            var user = _repositoryUser.Get().FirstOrDefault(x => x.DisplayOrder == 1);

            for (var i = 0; i < items.Count; i++)
            {

                foreach (var language in _repositoryLanguage.Get().Where(x => x.IsApproved).OrderBy(x => x.DisplayOrder))
                {
                    _repositoryRoleLanguageLine.Add(new RoleLanguageLine
                    {
                        Id = Guid.NewGuid(),
                        Role = items[i],
                        Language = language,
                        RoleName = dataList[i].RoleName,
                        RoleDescription = dataList[i] + " "+ language.LanguageCode+" bilgisi",
                        CreateDate = DateTime.Now,
                        CreatedBy = user,
                        UpdateDate = DateTime.Now,
                        UpdatedBy = user
                    });
                }

                    
            }
            _repositoryRoleLanguageLine.SaveChanges();

            foreach (var itemLanguageLine in _repositoryRoleLanguageLine.Get().ToList())
            {
                _repositoryRoleLanguageLineHistory.Add(new RoleLanguageLineHistory
                {
                    Id = Guid.NewGuid(),
                    RoleLanguageLineId = itemLanguageLine.Id,
                    RoleId = itemLanguageLine.Role.Id,
                    LanguageId = itemLanguageLine.Language.Id,
                    CreateDate = DateTime.Now,
                    CreatedBy = itemLanguageLine.CreatedBy.Id,
                    VersionNo = 1,
                    RestoreVersionNo = 0,
                    IsDeleted = false
                });
            }
            _repositoryRoleLanguageLineHistory.SaveChanges();
        }
    }
}
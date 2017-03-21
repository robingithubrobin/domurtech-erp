using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using DomurTech.ERP.Data.Access.Abstract;
using DomurTech.ERP.Data.Entities.Concrete;
using DomurTech.Installation.Common.DefaultDatas;

namespace DomurTech.Installation.Common.Installlers
{
    public class RoleInstaller
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

        public List<string> Set()
        {
            var result=new List<string>();
            var thread = new Thread(() =>
            {
                var dataList = new RoleDatas().RoleLanguageLines;
                int displayOrder;
                var totalCount = dataList.Count;
                for (var i = 0; i < dataList.Count; i++)
                {
                    displayOrder = i + 1;
                    _repositoryRole.Add(new Role
                    {
                        Id = Guid.NewGuid(),
                        RoleCode = dataList[i].Role.RoleCode,
                        DisplayOrder = displayOrder,
                        IsApproved = true,
                        CreateDate = DateTime.Now,
                        UpdateDate = DateTime.Now,
                        CreatedBy = _repositoryUser.Get().FirstOrDefault(x => x.DisplayOrder == 1),
                        UpdatedBy = _repositoryUser.Get().FirstOrDefault(x => x.DisplayOrder == 1)
                    });

                    result.Add("İşlem " + displayOrder + " / " + totalCount+" "+ dataList[i].Role.RoleCode);
                }
                _repositoryRole.SaveChanges();
                var items = _repositoryRole.Get().ToList();
                totalCount = items.Count;
                foreach (var item in items)
                {
                    displayOrder = item.DisplayOrder;
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
                    result.Add("İşlem " + displayOrder + " / " + totalCount + " " + item.RoleCode);
                }
                _repositoryRoleHistory.SaveChanges();

                var user = _repositoryUser.Get().FirstOrDefault(x => x.DisplayOrder == 1);

                totalCount = items.Count* _repositoryLanguage.Get().Count(x => x.IsApproved);
                displayOrder = 1;
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
                            RoleDescription = dataList[i] + " " + language.LanguageCode + " bilgisi",
                            CreateDate = DateTime.Now,
                            CreatedBy = user,
                            UpdateDate = DateTime.Now,
                            UpdatedBy = user
                        });                       
                        result.Add("İşlem " + displayOrder + " / " + totalCount + " " + dataList[i].RoleName);
                        displayOrder++;
                    }


                }
                _repositoryRoleLanguageLine.SaveChanges();

                totalCount = items.Count * _repositoryRoleLanguageLine.Get().Count();
                displayOrder = 1;
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
                    result.Add("İşlem " + displayOrder + " / " + totalCount + " " + itemLanguageLine.Id+"-"+ itemLanguageLine.Role.Id);
                    displayOrder++;
                }
                _repositoryRoleLanguageLineHistory.SaveChanges();
            });
            thread.Start();
            return result;

            
        }
    }
}
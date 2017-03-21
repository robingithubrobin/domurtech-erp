using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using DomurTech.ERP.Data.Access.Abstract;
using DomurTech.ERP.Data.Entities.Concrete;

namespace DomurTech.Installation.Common.Installlers
{
    public class RoleUserLineInstaller
    {
        private readonly IRepository<RoleUserLine> _repositoryRoleUserLine;
        private readonly IRepository<RoleUserLineHistory> _repositoryRoleUserLineHistory;
        private readonly IRepository<Role> _repositoryRole;
      private readonly IRepository<User> _repositoryUser;

        public RoleUserLineInstaller(IRepository<RoleUserLine> repositoryRoleUserLine, IRepository<RoleUserLineHistory> repositoryRoleUserLineHistory, IRepository<Role> repositoryRole, IRepository<User> repositoryUser)
        {
            _repositoryRoleUserLine = repositoryRoleUserLine;
            _repositoryRoleUserLineHistory = repositoryRoleUserLineHistory;
            _repositoryRole = repositoryRole;
            _repositoryUser = repositoryUser;
        }

        public List<string> Set()
        {
            var result = new List<string>();
            var thread = new Thread(() =>
            {
                var user = _repositoryUser.Get().FirstOrDefault(x => x.DisplayOrder == 1);
                var displayOrder=1;
                var totalCount = _repositoryRole.Get().Count();

                foreach (var role in _repositoryRole.Get().ToList())
                {
                    _repositoryRoleUserLine.Add(new RoleUserLine
                    {
                        Id = Guid.NewGuid(),
                        Role = role,
                        User = user,
                        CreateDate = DateTime.Now,
                        CreatedBy = user,
                        UpdateDate = DateTime.Now,
                        UpdatedBy = user
                    });
                    result.Add("İşlem " + displayOrder + " / " + totalCount + " " + role.RoleCode);
                    displayOrder++;
                }
                _repositoryRoleUserLine.SaveChanges();


                displayOrder = 1;
                totalCount = _repositoryRoleUserLine.Get().Count();
                foreach (var roleUserLine in _repositoryRoleUserLine.Get().ToList())
                {
                    _repositoryRoleUserLineHistory.Add(new RoleUserLineHistory
                    {
                        Id = Guid.NewGuid(),
                        RoleUserLineId = roleUserLine.Id,
                        RoleId = roleUserLine.Role.Id,
                        UserId = roleUserLine.User.Id,
                        CreateDate = DateTime.Now,
                        CreatedBy = roleUserLine.CreatedBy.Id,
                        VersionNo = 1,
                        RestoreVersionNo = 0,
                        IsDeleted = false
                    });
                    result.Add("İşlem " + displayOrder + " / " + totalCount + " " + roleUserLine.Id + " "+ roleUserLine.Role.Id);
                    displayOrder++;
                }
                _repositoryRoleUserLineHistory.SaveChanges();
            });
            thread.Start();
            return result;          
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using DomurTech.ERP.Data.Access.Abstract;
using DomurTech.ERP.Data.Entities.Concrete;

namespace DomurTech.Installation.Common.Installlers
{
    public class RoleUserLineInstaller
    {
        private readonly IRepository<User> _repositoryUser;
        private readonly IRepository<RoleUserLine> _repositoryRoleUserLine;
        private readonly IRepository<RoleUserLineHistory> _repositoryRoleUserLineHistory;
        private readonly IRepository<Role> _repositoryRole;

        public RoleUserLineInstaller(IRepository<User> repositoryUser, IRepository<RoleUserLine> repositoryRoleUserLine, IRepository<RoleUserLineHistory> repositoryRoleUserLineHistory, IRepository<Role> repositoryRole)
        {
            _repositoryUser = repositoryUser;
            _repositoryRoleUserLine = repositoryRoleUserLine;
            _repositoryRoleUserLineHistory = repositoryRoleUserLineHistory;
            _repositoryRole = repositoryRole;
        }

        public RoleUserLine AddRoleUserLine(RoleUserLine roleUserLine)
        {
            var result = _repositoryRoleUserLine.Add(roleUserLine);
            _repositoryRoleUserLine.SaveChanges();
            return result;
        }

        public void AddRoleUserLineHistory(RoleUserLineHistory roleUserLineHistory)
        {
            _repositoryRoleUserLineHistory.Add(roleUserLineHistory);
            _repositoryRoleUserLineHistory.SaveChanges();
        }

        public List<RoleUserLine> GetAllRoleUserLines()
        {
            return _repositoryRoleUserLine.Get().ToList();
        }

        public List<RoleUserLine> GetRoleUserLineList()
        {
            var user = _repositoryUser.Get().FirstOrDefault(x => x.DisplayOrder == 1);
            var roles = _repositoryRole.Get().ToList();
            return roles.Select(role => new RoleUserLine
            {
                Id = Guid.NewGuid(),
                Role = role,
                User = user,
                CreateDate = DateTime.Now
            }).ToList();

        }

        public List<RoleUserLineHistory> GetRoleUserLineHistoryList()
        {
            var user = _repositoryUser.Get().FirstOrDefault(x => x.DisplayOrder == 1);
            if (user == null)
            {
                return new List<RoleUserLineHistory>();
            }
            return GetAllRoleUserLines().Select(roleUserLine => new RoleUserLineHistory
            {
                Id = Guid.NewGuid(),
                RoleUserLineId = roleUserLine.Id,
                RoleId = roleUserLine.Role.Id,
                UserId = roleUserLine.User.Id,
                CreateDate = DateTime.Now,
                CreatedBy = user.Id,
                VersionNo = 1,
                RestoreVersionNo = 0,
                IsDeleted = false
            }).ToList();
        } 
        
    }
}
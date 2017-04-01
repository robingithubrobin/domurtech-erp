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

        public RoleUserLineInstaller(IRepository<User> repositoryUser, IRepository<RoleUserLine> repositoryRoleUserLine, IRepository<RoleUserLineHistory> repositoryRoleUserLineHistory)
        {
            _repositoryUser = repositoryUser;
            _repositoryRoleUserLine = repositoryRoleUserLine;
            _repositoryRoleUserLineHistory = repositoryRoleUserLineHistory;
        }

        public RoleUserLine Add(RoleUserLine roleUserLine)
        {
            var result = _repositoryRoleUserLine.Add(roleUserLine);
            _repositoryRoleUserLine.SaveChanges();
            return result;
        }

        public void Add(RoleUserLineHistory roleUserLineHistory)
        {
            _repositoryRoleUserLineHistory.Add(roleUserLineHistory);
            _repositoryRoleUserLineHistory.SaveChanges();
        }

        public IQueryable<RoleUserLine> GetAl()
        {
            return _repositoryRoleUserLine.Get();
        }

        public List<RoleUserLine> GetList(IQueryable<Role> roles)
        {
            var user = _repositoryUser.Get().FirstOrDefault(x => x.DisplayOrder == 1);
            return roles.Select(role => new RoleUserLine
            {
                Id = Guid.NewGuid(),
                Role = role,
                User = user,
                CreateDate = DateTime.Now
            }).ToList();
        }

        public List<RoleUserLineHistory> GetList(List<RoleUserLine> rolesUserLines)
        {
            var user = _repositoryUser.Get().FirstOrDefault(x => x.DisplayOrder == 1);
            if (user == null)
            {
                return new List<RoleUserLineHistory>();
            }
            return rolesUserLines.Select(roleUserLine => new RoleUserLineHistory
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
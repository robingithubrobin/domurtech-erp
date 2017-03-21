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

        public RoleUserLineInstaller(IRepository<User> repositoryUser)
        {
            _repositoryUser = repositoryUser;
        }

        public List<RoleUserLine> GetList(List<Role> roles)
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
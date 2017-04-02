using System;
using System.Data.Entity;
using System.Linq;
using DomurTech.ERP.Business.Entities.Models.BaseModels;
using DomurTech.ERP.Business.Managers.Abstract;
using DomurTech.ERP.Data.Access.Abstract;
using DomurTech.ERP.Data.Entities.Concrete;
using DomurTech.Exceptions;
using DomurTech.Globalization;

namespace DomurTech.ERP.Business.Managers.Concrete
{
    public class RoleManager : IRoleManager
    {
        private bool _disposed;
        private readonly IRepository<Role> _repositoryRole;
        private readonly IRepository<RoleHistory> _repositoryRoleHistory;
        private readonly IRepository<User> _repositoryUser;
        private readonly IRepository<RoleLanguageLine> _repositoryRoleLanguageLine;

        public RoleManager(IRepository<Role> repositoryRole, IRepository<RoleHistory> repositoryRoleHistory, IRepository<User> repositoryUser, IRepository<RoleLanguageLine> repositoryRoleLanguageLine)
        {
            _repositoryRole = repositoryRole;
            _repositoryRoleHistory = repositoryRoleHistory;
            _repositoryUser = repositoryUser;
            _repositoryRoleLanguageLine = repositoryRoleLanguageLine;
        }
        
        public AddModel<Role> Add()
        {
            throw new NotImplementedException();
        }

        public void Add(AddModel<Role> model)
        {
            throw new NotImplementedException();
        }

        public UpdateModel<Role> Update(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Update(UpdateModel<Role> model)
        {
            throw new NotImplementedException();
        }

        public DetailModel<Role> Detail(Guid id)
        {
            var model = new DetailModel<Role>();
            var item = _repositoryRole.Get().FirstOrDefault(x => x.Id == id);
            if (item == null)
            {
                throw new NotFoundException(Messages.DangerRecordNotFound);
            }
            var itemLanguageLines = _repositoryRoleLanguageLine.Get().Include(x => x.Language).Include(x => x.Role).Where(x => x.Role.Id == id);
            if (itemLanguageLines.Any())
            {
                item.RoleLanguageLines = itemLanguageLines.ToList();
            }

            var firstVersion = _repositoryRoleHistory.Get().FirstOrDefault(x => x.VersionNo == 1 && x.RoleId == item.Id);
            var lastVersion = _repositoryRoleHistory.Get().OrderByDescending(x => x.VersionNo).FirstOrDefault(x => x.RoleId == item.Id);
            if (firstVersion == null)
            {
                throw new NotFoundException(Messages.DangerRecordNotFound);
            }
            if (lastVersion == null)
            {
                throw new NotFoundException(Messages.DangerRecordNotFound);
            }

            model.Item = item;
            model.CreateDate = firstVersion.CreateDate;
            var createdUser = _repositoryUser.Get().Include(x => x.Person).FirstOrDefault(x => x.Id == firstVersion.CreatedBy);
            if (createdUser == null)
            {
                throw new NotFoundException(Messages.DangerRecordNotFound);
            }
            createdUser.DisplayName = createdUser.Person.FirstName + " " + createdUser.Person.LastName;
            model.CreatedBy = createdUser;
            model.UpdateDate = lastVersion.CreateDate;
            var updatedBy = _repositoryUser.Get().Include(x => x.Person).FirstOrDefault(x => x.Id == lastVersion.CreatedBy);
            if (updatedBy == null)
            {
                throw new NotFoundException(Messages.DangerRecordNotFound);
            }
            updatedBy.DisplayName = updatedBy.Person.FirstName + " " + updatedBy.Person.LastName;
            model.UpdatedBy = updatedBy;
            return model;
        }

        public ListModel<Role> GetList(ListModel<Role> model)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }
        
        

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _repositoryRole.Dispose();
                    _repositoryRoleHistory.Dispose();
                    _repositoryUser.Dispose();
                    _repositoryRoleLanguageLine.Dispose();
                }
            }
            _disposed = true;
        }
    }
}

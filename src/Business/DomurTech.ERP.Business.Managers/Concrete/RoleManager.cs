using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using DomurTech.ERP.Business.Entities.Models.BaseModels;
using DomurTech.ERP.Business.Managers.Abstract;
using DomurTech.ERP.Data.Access.Abstract;
using DomurTech.ERP.Data.Entities.Concrete;
using DomurTech.Exceptions;
using DomurTech.Globalization;
using DomurTech.Helpers;
using DomurTech.Providers;

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
            var result = new DetailModel<Role>();

           
            var item = _repositoryRole.Get().FirstOrDefault(x => x.Id == id);
            if (item == null)
            {
                throw new NotFoundException(Messages.DangerRecordNotFound);
            }
            var itemLanguageLines = _repositoryRoleLanguageLine.Get().Include(x=>x.Language).Include(x=>x.Role).Where(x => x.Role.Id == id);
            if (itemLanguageLines.Any())
            {
                item.RoleLanguageLines = itemLanguageLines.ToList();
            }
            
            var firstVersion = _repositoryRoleHistory.Get().FirstOrDefault(x => x.VersionNo == 1 && x.RoleId==item.Id);
            var lastVersion = _repositoryRoleHistory.Get().OrderByDescending(x=>x.VersionNo).FirstOrDefault(x=>x.RoleId == item.Id);
            if (firstVersion == null)
            {
                throw new NotFoundException(Messages.DangerRecordNotFound);
            }
            if (lastVersion == null)
            {
                throw new NotFoundException(Messages.DangerRecordNotFound);
            }

            result.Item = item;
            result.CreateDate = firstVersion.CreateDate;
            var createdUser= _repositoryUser.Get().Include(x => x.Person).FirstOrDefault(x => x.Id == firstVersion.CreatedBy);
            if (createdUser == null)
            {
                throw new NotFoundException(Messages.DangerRecordNotFound);
            }
            createdUser.DisplayName = createdUser.Person.FirstName + " " + createdUser.Person.LastName;
            result.CreatedBy = createdUser;
            result.UpdateDate = lastVersion.CreateDate;
            var updatedBy = _repositoryUser.Get().Include(x => x.Person).FirstOrDefault(x => x.Id == lastVersion.CreatedBy);
            if (updatedBy == null)
            {
                throw new NotFoundException(Messages.DangerRecordNotFound);
            }
            updatedBy.DisplayName = updatedBy.Person.FirstName + " " + updatedBy.Person.LastName;
            result.UpdatedBy = updatedBy;
            return result;

        }

        public ListModel<Role> GetList(ListModel<Role> model)
        {
            Expression<Func<Role, bool>> expression;
            if (model.Status != -1)
            {
                var status = model.Status.ToString().ConvertToBoolean();
                if (model.Searched != null)
                {
                    if (status)
                    {
                        expression = c => c.IsApproved && c.RoleCode.Contains(model.Searched);
                    }
                    else
                    {
                        expression = c => c.IsApproved == false && c.RoleCode.Contains(model.Searched);
                    }
                }
                else
                {
                    if (status)
                    {
                        expression = c => c.IsApproved;
                    }
                    else
                    {
                        expression = c => c.IsApproved == false;
                    }
                }
            }
            else
            {
                if (model.Searched != null)
                {
                    expression = c => c.RoleCode.Contains(model.Searched);
                }
                else
                {
                    expression = c => c.Id != Guid.Empty;
                }
            }
            var startDate = model.StartDate.ResetTimeToStartOfDay();
            var endDate = model.EndDate.ResetTimeToEndOfDay();
            model.Where = expression.And(e => e.CreateDate >= startDate && e.CreateDate <= endDate);
            model.Paging.TotalItemCount = _repositoryRole.Get().Count(model.Where);
            IQueryable<Role> items;

            if (model.Paging.CurrentPageSize > 0)
            {
                var sortHelper = new SortHelper<Role>();
                sortHelper.OrderBy(t => t.DisplayOrder);
                var query = (IOrderedQueryable<Role>)_repositoryRole.Get().Where(model.Where);
                query = sortHelper.GenerateOrderedQuery(query);
                items = query.Skip((model.Paging.CurrentPage - 1) * model.Paging.CurrentPageSize).Take(model.Paging.CurrentPageSize);
            }
            else
            {
                items = _repositoryRole.Get().Where(model.Where);
            }

            model.Items = items.ToList();

            var pageSizeListLanguage = SystemSettings.PageSizeList;
            var pageSizeLanguageDescription = "10,25,50,100";
            if (pageSizeListLanguage != null)
            {
                pageSizeLanguageDescription = pageSizeListLanguage;
            }
            var pageSizeList = pageSizeLanguageDescription.Split(',').Select(t => new KeyValuePair<int, string>(t.ConvertToInt(), t)).ToList();
            pageSizeList.Insert(0, new KeyValuePair<int, string>(-1, "[" + Dictionaries.All + "]"));
            model.Paging.PageSizeList = pageSizeList;
            if (model.IdsForSort != null)
            {
                var ids = model.IdsForSort;
                ids = ids.Remove(ids.Length - 1);
                var idArray = ids.Split(',');
                var idList = idArray.Select(id => new Guid(id)).ToList();
                model.Items = SaveSort(idList);
            }
            model.IdsForSort = null;

            model.Paging.TotalPageCount = (int)Math.Ceiling((float)model.Paging.TotalItemCount / model.Paging.CurrentPageSize);
            return model;
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        private List<Role> SaveSort(List<Guid> idList)
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

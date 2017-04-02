using System;
using System.Collections.Generic;
using System.Linq;
using DomurTech.ERP.Business.Entities.ComplexTypes;
using DomurTech.ERP.Business.Entities.Models.BaseModels;
using DomurTech.ERP.Business.Managers.Abstract;
using DomurTech.ERP.Data.Entities.Concrete;
using DomurTech.ERP.Service.Common.Abstract;
using DomurTech.ERP.Service.Entities.Concrete.RoleModels;
using DomurTech.Exceptions;
using DomurTech.Globalization;

namespace DomurTech.ERP.Service.Common.Services
{
    public class RoleService : IRoleService
    {
        private bool _disposed;
        private readonly IRoleManager _roleManager;

        public RoleService(IRoleManager roleManager)
        {
            _roleManager = roleManager;
        }
        
        public DetailModel Detail(Guid roleId, Guid languageId)
        {
            var result = new DetailModel();
            var businessModel = _roleManager.Detail(roleId);
            var item = businessModel.Item;
            if (item == null)
            {
                throw new NotFoundException(Messages.DangerRecordNotFound);
            }
            var itemLanguageLine = item.RoleLanguageLines.FirstOrDefault(x => x.Role.Id == roleId && x.Language.Id == languageId);
            if (itemLanguageLine == null)
            {
                throw new NotFoundException(Messages.DangerRecordNotFound);
            }
            result.RoleId = item.Id;
            result.RoleCode = item.RoleCode;
            result.RoleName = itemLanguageLine.RoleName;
            result.RoleDescription = itemLanguageLine.RoleDescription;
            result.LanguageCode = itemLanguageLine.Language.LanguageCode;
            result.LanguageName = itemLanguageLine.Language.LanguageName;
            result.DisplayOrder = item.DisplayOrder;
            result.IsApproved = item.IsApproved;
            result.CreateDate = businessModel.CreateDate;
            result.CreatedBy = businessModel.CreatedBy.DisplayName;
            result.UpdateDate = businessModel.UpdateDate;
            result.UpdatedBy = businessModel.UpdatedBy.DisplayName;
            return result;
        }

        public ListModel List(ListModel filterModel)
        {
            var businessModel = _roleManager.GetList(new ListModel<Role>
            {
                Paging = new Paging
                {
                    PageNumber = filterModel.PageNumber,
                    PageSize = filterModel.PageSize
                },
                LanguageCode = filterModel.LanguageCode,
                StartDate = filterModel.StartDate,
                EndDate = filterModel.EndDate,
                Searched = filterModel.Searched,
                Status = filterModel.Status
                
            });


            var list = new List<DetailModel>();
            foreach (var item in businessModel.Items)
            {
                var itemLanguageLine = item.RoleLanguageLines.FirstOrDefault(x=>x.Language.LanguageCode==filterModel.LanguageCode);
                if (itemLanguageLine!=null)
                {
                    list.Add(new DetailModel
                    {
                        RoleId = item.Id,
                        RoleCode = item.RoleCode,
                        RoleName = itemLanguageLine.RoleName,
                        RoleDescription = itemLanguageLine.RoleDescription,
                        LanguageCode = filterModel.LanguageCode,
                        DisplayOrder = item.DisplayOrder,
                        IsApproved = item.IsApproved,
                        CreateDate = item.CreateDate
                    });
                }
            }
            
            return new ListModel
            {
                LanguageCode = filterModel.LanguageCode,
                Status = filterModel.Status,
                StartDate = filterModel.StartDate,
                EndDate = filterModel.EndDate,
                PageNumber = filterModel.PageNumber,
                PageSize = filterModel.PageSize,
                Items = list
            };
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
                    _roleManager.Dispose();
                }
            }
            _disposed = true;
        }
    }
}

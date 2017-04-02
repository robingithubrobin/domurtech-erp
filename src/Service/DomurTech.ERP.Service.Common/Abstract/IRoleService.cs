using System;
using DomurTech.ERP.Service.Entities.Concrete.RoleModels;

namespace DomurTech.ERP.Service.Common.Abstract
{
    public interface IRoleService: IDisposable
    {
        DetailModel Detail(Guid roleId, Guid languageId);
    }
}

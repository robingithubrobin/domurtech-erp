using System;
using DomurTech.Core.Abstract;
using DomurTech.ERP.Business.Entities.Models.BaseModels;

namespace DomurTech.ERP.Business.Managers.Abstract
{
    public interface IBaseManager<T> where T : class, IEntity, new()
    {
        AddModel<T> Add();
        void Add(AddModel<T> model);
        UpdateModel<T> Update(Guid id);
        void Update(UpdateModel<T> model);
        DetailModel<T> Detail(Guid id);
        ListModel<T> GetList(ListModel<T> model);
        void Delete(Guid id);
    }
}

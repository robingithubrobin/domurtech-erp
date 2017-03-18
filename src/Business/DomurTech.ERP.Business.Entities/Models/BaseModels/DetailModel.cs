using System.Collections.Generic;
using DomurTech.Core.Abstract;
using DomurTech.ERP.Data.Entities.Concrete;

namespace DomurTech.ERP.Business.Entities.Models.BaseModels
{
    public class DetailModel<T> : IBaseModel where T : class, IEntity, new()
    {
        public T Item { get; set; }
        public string Message { get; set; }
        public List<Language> Languages { get; set; }
    }
}

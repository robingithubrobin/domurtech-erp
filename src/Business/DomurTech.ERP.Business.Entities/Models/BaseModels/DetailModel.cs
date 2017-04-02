using System;
using DomurTech.Core.Abstract;
using DomurTech.ERP.Data.Entities.Concrete;

namespace DomurTech.ERP.Business.Entities.Models.BaseModels
{
    public class DetailModel<T> : IBaseModel
    {
        public T Item { get; set; }
        public DateTime CreateDate { get; set; }
        public User CreatedBy { get; set; }
        public DateTime UpdateDate { get; set; }
        public User UpdatedBy { get; set; }
        public string Message { get; set; }
    }
}

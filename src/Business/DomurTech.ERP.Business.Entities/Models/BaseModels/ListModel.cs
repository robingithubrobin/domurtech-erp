using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DomurTech.Core.Abstract;
using DomurTech.ERP.Business.Entities.ComplexTypes;
using DomurTech.ERP.Data.Entities.Concrete;

namespace DomurTech.ERP.Business.Entities.Models.BaseModels
{
    public class ListModel<T> : IBaseModel where T : class, IEntity, new()
    {
        public Paging Paging { get; set; }
        public List<T> Items { get; set; }
        public Expression<Func<T, bool>> Where { get; set; }
        public IOrderedQueryable<T> Sorting { get; set; }
        public string IdsForSort { get; set; }
        public string IdsForDelete { get; set; }
        public string IdsForApprove { get; set; }
        public string LanguageCode { get; set; }
        public List<Language> Languages { get; set; }
        public string Searched { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Status { get; set; }

    }
}

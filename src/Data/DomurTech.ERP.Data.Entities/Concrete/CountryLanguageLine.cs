using System;
using DomurTech.Core.Abstract;

namespace DomurTech.ERP.Data.Entities.Concrete
{
    
    public class CountryLanguageLine : IEntity
    {
        public Guid Id { get; set; }
        public string CountryName { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public virtual User CreatedBy { get; set; }
        public virtual User UpdatedBy { get; set; }
        public virtual Country Country { get; set; }
        public virtual Language Language { get; set; }
    }
}

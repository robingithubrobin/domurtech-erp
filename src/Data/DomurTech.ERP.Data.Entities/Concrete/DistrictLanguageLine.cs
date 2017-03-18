using System;
using DomurTech.Core.Abstract;

namespace DomurTech.ERP.Data.Entities.Concrete
{
    
    public class DistrictLanguageLine : IEntity
    {
        public Guid Id { get; set; }
        public string DistrictName { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public virtual User CreatedBy { get; set; }
        public virtual User UpdatedBy { get; set; }
        public virtual District District { get; set; }
        public virtual Language Language { get; set; }
    }
}

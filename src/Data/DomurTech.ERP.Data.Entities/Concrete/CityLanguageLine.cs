using System;
using DomurTech.Core.Abstract;

namespace DomurTech.ERP.Data.Entities.Concrete
{
    
    public class CityLanguageLine : IEntity
    {
        public Guid Id { get; set; }
        public string CityName { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public virtual User CreatedBy { get; set; }
        public virtual User UpdatedBy { get; set; }
        public virtual City City { get; set; }
        public virtual Language Language { get; set; }
    }
}

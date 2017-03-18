using System;
using DomurTech.Core.Abstract;

namespace DomurTech.ERP.Data.Entities.Concrete
{
    
    public class Session : IEntity
    {
        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; }
        public virtual User CreatedBy { get; set; }
     
    }
}

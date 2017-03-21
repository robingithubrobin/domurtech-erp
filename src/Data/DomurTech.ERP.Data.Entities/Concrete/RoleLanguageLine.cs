using System;
using DomurTech.Core.Abstract;

namespace DomurTech.ERP.Data.Entities.Concrete
{
    
    public class RoleLanguageLine : IEntity
    {
        public Guid Id { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public DateTime CreateDate { get; set; }
        public virtual Role Role { get; set; }
        public virtual Language Language { get; set; }
    }
}

using System;
using DomurTech.Core.Abstract;

namespace DomurTech.ERP.Data.Entities.Concrete
{
    
    public class RoleActionLine : IEntity
    {
        public Guid Id { get; set; }
        public virtual Role Role { get; set; }
        public virtual Action Action { get; set; }


    }
}

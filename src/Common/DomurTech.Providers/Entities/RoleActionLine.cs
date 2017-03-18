using System;
using DomurTech.Providers.Abstract;

namespace DomurTech.Providers.Entities
{
    
    public class RoleActionLine : IEntity
    {
        public Guid Id { get; set; }
        public virtual Role Role { get; set; }
        public virtual Action Action { get; set; }

    }

}

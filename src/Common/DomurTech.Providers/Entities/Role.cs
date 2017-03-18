using System;
using System.Collections.Generic;
using DomurTech.Providers.Abstract;

namespace DomurTech.Providers.Entities
{
    
    public class Role : IEntity
    {
        public Guid Id { get; set; }
        public string RoleCode { get; set; }
        public virtual IList<RoleActionLine> RoleActionLines { get; set; }

    }
}

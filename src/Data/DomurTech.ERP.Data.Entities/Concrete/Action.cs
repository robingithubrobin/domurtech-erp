using System;
using System.Collections.Generic;
using DomurTech.Core.Abstract;

namespace DomurTech.ERP.Data.Entities.Concrete
{
    
    public class Action : IEntity
    {
        public Guid Id { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public virtual ICollection<RoleActionLine> RoleActionLines { get; set; }

    }
}

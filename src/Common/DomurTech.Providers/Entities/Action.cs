using System;
using System.Collections.Generic;
using DomurTech.Providers.Abstract;

namespace DomurTech.Providers.Entities
{
    
    public class Action : IEntity
    {
        public Guid Id { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public virtual IList<RoleActionLine> RoleActionLines { get; set; }

    }

}

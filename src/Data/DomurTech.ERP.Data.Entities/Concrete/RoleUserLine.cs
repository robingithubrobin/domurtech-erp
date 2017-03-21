using System;
using DomurTech.Core.Abstract;

namespace DomurTech.ERP.Data.Entities.Concrete
{

    public class RoleUserLine : IEntity
    {
        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; }
        public virtual Role Role { get; set; }
        public virtual User User { get; set; }

    }

}

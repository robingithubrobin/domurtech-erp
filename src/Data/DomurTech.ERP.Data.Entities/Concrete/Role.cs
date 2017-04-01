using System;
using System.Collections.Generic;
using DomurTech.Core.Abstract;

namespace DomurTech.ERP.Data.Entities.Concrete
{
    
    public class Role : IEntity
    {
        public Guid Id { get; set; }
        public string RoleCode { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsApproved { get; set; }
        public DateTime CreateDate { get; set; }
        public virtual ICollection<RoleActionLine> RoleActionLines { get; set; }
        public virtual ICollection<RoleLanguageLine> RoleLanguageLines { get; set; }
        public virtual ICollection<RoleUserLine> RoleUserLines { get; set; }

    }

}

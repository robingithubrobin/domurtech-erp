using System;
using DomurTech.Core.Abstract;

namespace DomurTech.ERP.Data.Entities.Concrete
{
    
    public class RoleUserLineHistory : IEntity
    {
        public Guid Id { get; set; }
        public Guid RoleUserLineId { get; set; }
        public Guid RoleId { get; set; }
        public Guid UserId { get; set; }
        
        public DateTime CreateDate { get; set; }
        public Guid CreatedBy { get; set; }
        public int VersionNo { get; set; }
        public int RestoreVersionNo { get; set; }
        public bool IsDeleted { get; set; }

    }

}

using System;
using DomurTech.Core.Abstract;

namespace DomurTech.ERP.Data.Entities.Concrete
{
    
    public class SettingHistory : IEntity
    {
        public Guid Id { get; set; }
        public Guid SettingId { get; set; }
        public string SettingKey { get; set; }
        public string SettingValue { get; set; }
        public bool Erasable { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsApproved { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid CreatedBy { get; set; }
        public int VersionNo { get; set; }
        public int RestoreVersionNo { get; set; }
        public bool IsDeleted { get; set; }

    }

}
